using System;
using System.Collections.Generic;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Auth_SysAdmLayoutSeatView : System.Web.UI.Page
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    protected void Page_Init(object sender, System.EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.AddHeader("Cache-control", "no-store, must-revalidate,private,no-cache");
        Response.AddHeader("PRAGMA", "NO-Cache");
        Response.Expires = -1;
        Response.Expires = 0;
        Response.Cache.SetNoStore();
        Response.Cache.SetExpires(DateTime.Now.AddDays(-2));
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["_moduleName"] = "View Bus Layout";
        if (!IsPostBack)
        {
            lblLayoutName.Text = "Bus Seat Layout " + Session["_layoutName"].ToString();
            lblTitleSeatsU.Visible = false;
            lblTitleSeatsL.Visible = false;


            if (Session["_layoutCategory"].ToString() == "S")
                pnlUpperSeats.Visible = true;
            else
            {
                lblTitleSeatsL.Visible = false;
                lblTitleSeatsU.Visible = false;
                pnlUpperSeats.Visible = false;
            }
          
            CreateDynamicTableLower(Convert.ToInt32(Session["_LayoutCode"].ToString()));
            CreateDynamicTableBtnUpper(Convert.ToInt32(Session["_LayoutCode"].ToString()));

        }

    }

    #region "Method"
    private void CreateDynamicTableLower(int MlayoutCode)//M1
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_bus_layout_getlist");
            MyCommand.Parameters.AddWithValue("p_layoutcode", MlayoutCode);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count <= 0)
                {
                    return;
                }
            }
            else
            {
                return;
            }

            int Mrows = Convert.ToInt32(Session["_rows"].ToString());
            int Mcols = Convert.ToInt32(Session["_cols"].ToString());
            var height = 1;
            var padding = 10;
            int SeatNo = 1;
            string MBseatQuotaCode;
            int MRowNumber;
            int MColNumber;
            string[] MBusSeatDetails;

            for (MRowNumber = 1; MRowNumber <= Mrows; MRowNumber++)
            {
                TableRow dr = new TableRow();
                for (MColNumber = 1; MColNumber <= Mcols; MColNumber++)
                {
                    TableCell tc = new TableCell();
                    Button btn = new Button();
                    btn.ID = "iBtn-" + MRowNumber.ToString() + "-" + MColNumber.ToString();
                    if (MRowNumber == 1 & MColNumber == Mcols)
                    {
                        btn.Style["background-image"] = "url('../assets/img/Seats/seatDriver.png')";
                        btn.CssClass = "form-control seatTextboxDrv";
                    }
                    else
                    {
                        MBseatQuotaCode = getTravellerTypeofSeat(MlayoutCode, MRowNumber, MColNumber);
                        MBusSeatDetails = MBseatQuotaCode.Split('-');//.split("-");

                        // SEATYN || '-' || to_char(seatno)   ||'-' || TRAVELLERTYPECODE || '-' || seatavailforonlbooking
                        btn.Style["background-image"] = "url('../assets/img/Seats/seatGeneral.png')";

                        switch (MBusSeatDetails[2])
                        {
                            case "G": // General
                                {
                                    btn.CssClass = "form-control seatTextbox";
                                    break;
                                }

                            case "F": // Women
                                {
                                    btn.CssClass = "form-control seatTextboxWomen";
                                    break;
                                }

                            case "C":
                                {
                                    btn.CssClass = "form-control seatTextboxConductor";
                                    break;
                                }

                            case "M": // Male             
                                {
                                    btn.CssClass = "form-control seatTextboxMale";
                                    break;
                                }
                        }

                        if (MBusSeatDetails[3] == "N")
                        {
                            if (MBusSeatDetails[2] != "C")
                                btn.CssClass = "form-control seatTextboxRes";
                        }

                        // If isSeat(MlayoutCode, MRowNumber, MColNumber) = False Then
                        if (MBusSeatDetails[0] == "N")
                            btn.Visible = false;
                        else
                            btn.Text = MBusSeatDetails[1];// getSeatNo(MlayoutCode, MRowNumber, MColNumber)

                        btn.Attributes.Add("rowNumber", MRowNumber.ToString());
                        btn.Attributes.Add("colNumber", MColNumber.ToString());
                    }
                    btn.Width = 43;
                    btn.Height = 50;
                    tc.Controls.Add(btn);
                    tc.Style.Add("padding", "5px");
                    dr.Cells.Add(tc);
                }
                Table1.Rows.Add(dr);
                Table1.EnableViewState = true;
                ViewState["tblSeatType"] = true;
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("SysAdmLayoutSeatView-M1", ex.Message.ToString());
        }
    }
    private void CreateDynamicTableBtnUpper(int MlayoutCode)//M2
    {
        try
        {
            if (Session["_layoutCategory"].ToString() == "S")
            {
                int Mrow = Convert.ToInt32(Session["_rows"].ToString());
                int Mcols = Convert.ToInt32(Session["_cols"].ToString());

                int MrowU = Convert.ToInt32(Session["_rowsU"].ToString());
                int McolsU = Convert.ToInt32(Session["_colsU"].ToString());


                int j;
                // Dim chk = New CheckBox(12) {}
                var height = 1;
                var padding = 10;
                HtmlTable dt = new HtmlTable();
                int MtotRows;
                int MRowNumber;
                int MColNumber;
                MtotRows = Mrow + MrowU;
                string MBseatQuotaCode;
                string[] MBusSeatDetails;

                for (MRowNumber = Mrow + 1; MRowNumber <= MtotRows; MRowNumber++)
                {
                    TableRow dr = new TableRow();
                    for (MColNumber = 1; MColNumber <= McolsU; MColNumber++)
                    {
                        TableCell dc = new TableCell();
                        // -------------------------------------------
                        Button btn = new Button();
                        btn.ID = "iBtn-" + MRowNumber.ToString() + "-" + MColNumber.ToString();

                        if (MRowNumber == 1 & MColNumber == 1)
                            btn.CssClass = "form-control seatTextboxDrv";
                        else
                        {
                            // ------------------------------
                            MBseatQuotaCode = getTravellerTypeofSeat(MlayoutCode, MRowNumber, MColNumber);
                            MBusSeatDetails = MBseatQuotaCode.Split('-');

                            // SEATYN || '-' || to_char(seatno)   ||'-' || TRAVELLERTYPECODE || '-' || seatavailforonlbooking
                            btn.Style["background-image"] = "url('../assets/img/Seats/seatSleeper.png')";
                            btn.CssClass = "form-control seatTextboxSleeper";
                            switch (MBusSeatDetails[2])
                            {
                                case "G": // General

                                    {
                                        btn.CssClass = "form-control seatTextbox";
                                        break;
                                    }

                                case "F":// Women
                                    {
                                        btn.CssClass = "form-control seatTextboxWomen";
                                        break;
                                    }

                                case "C":
                                    {
                                        btn.CssClass = "form-control seatTextboxConductor";
                                        break;
                                    }

                                case "M": // Male

                                    {
                                        btn.CssClass = "form-control seatTextboxMale";
                                        break;
                                    }
                            }

                            if (MBusSeatDetails[3] == "N")
                            {
                                if (MBusSeatDetails[2] != "C")
                                    btn.CssClass = "form-control seatTextboxRes";
                            }

                            if (MBusSeatDetails[0] == "N")
                                btn.Visible = false;
                            else
                                btn.Text = MBusSeatDetails[1];// getSeatNo(MlayoutCode, MRowNumber, MColNumber)


                            btn.Attributes.Add("rowNumber", MRowNumber.ToString());
                            btn.Attributes.Add("colNumber", MColNumber.ToString());
                        }
                        btn.Width = 96;
                        btn.Height = 50;
                        dc.Controls.Add(btn);
                        dc.Style.Add("padding", "10px");
                        dr.Cells.Add(dc);
                    }
                    Table2.Rows.Add(dr);
                    Table2.EnableViewState = true;
                    ViewState["tblSeatType"] = true;
                }
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("SysAdmLayoutSeatView-M2", ex.Message.ToString());
        }
    }
    private string getTravellerTypeofSeat(int mlayoutCode, int mRowNumber, int mColNumber)//M3
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_bus_layout_get_trvltypeofseat");
            MyCommand.Parameters.AddWithValue("p_layoutcode", mlayoutCode);
            MyCommand.Parameters.AddWithValue("p_rownumber", mRowNumber);
            MyCommand.Parameters.AddWithValue("p_columnnumber", mColNumber);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0][0].ToString();
                }
            }
            return "";
        }
        catch (Exception ex)
        {
            _common.ErrorLog("SysAdmLayoutSeatView-M3", ex.Message.ToString());
            return "";
        }
    }
    protected override object SaveViewState()
    {
        object[] newViewState = new object[2];
        List<string> chkBox = new List<string>();
        // ----------------------------------------------------------
        foreach (TableRow row in Table1.Controls)
        {
            foreach (TableCell cell in row.Controls)
            {
                if (cell.Controls[0] is Button)
                {
                    Button btn = (Button)cell.Controls[0];
                    chkBox.Add(btn.Text);
                }

            }
        }
        // ----------------------------------------------------------
        if (Session["_layoutCategory"].ToString() == "S")
        {
            foreach (TableRow row in Table2.Controls)
            {
                foreach (TableCell cell in row.Controls)
                {
                    if (cell.Controls[0] is Button)
                    {
                        Button btn = (Button)cell.Controls[0];
                        chkBox.Add(btn.Text);
                    }

                }
            }
        }
        // ----------------------------------------------------------
        newViewState[0] = chkBox.ToArray();
        newViewState[1] = base.SaveViewState();
        return newViewState;
    }
    protected override void LoadViewState(object savedState)
    {
        if (savedState is object[] && ((object[])savedState).Length == 2 && ((object[])savedState)[0] is string[])
        {
            object[] newViewState = (object[])savedState;
            string[] chkBox = (string[])(newViewState[0]);

            if (chkBox.Length > 0)
            {
                CreateDynamicTableLower(Convert.ToInt32(Session["_LayoutCode"].ToString()));

                int i = 0;

                foreach (TableRow row in Table1.Controls)
                {
                    foreach (TableCell cell in row.Controls)
                    {
                        if (cell.Controls[0] is Button && i < chkBox.Length)
                        {
                            Button btn = (Button)cell.Controls[0];
                            btn.Text = chkBox[Math.Min(System.Threading.Interlocked.Increment(ref i), i - 1)];
                        }
                    }
                }

                if (Session["_layoutCategory"].ToString() == "S")
                {
                    CreateDynamicTableBtnUpper(Convert.ToInt32(Session["_layoutCode"].ToString()));
                    int j = 0;
                    foreach (TableRow row in Table2.Controls)
                    {
                        foreach (TableCell cell in row.Controls)
                        {
                            if (cell.Controls[0] is Button && j < chkBox.Length)
                            {
                                Button btn = (Button)cell.Controls[0];
                                btn.Text = chkBox[Math.Min(System.Threading.Interlocked.Increment(ref j), j - 1)];
                            }
                        }
                    }
                }
            }

            base.LoadViewState(newViewState[1]);
        }
        else
            base.LoadViewState(savedState);
    }
    #endregion


    #region "Event"
    protected void lbtnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("SysAdmLayoutMain.aspx");
    }
    #endregion



}