using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Auth_SysAdmLayoutSeatTypes : System.Web.UI.Page
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
        Session["_moduleName"] = "Seat Type Configuration";
        if (!IsPostBack)
        {
            loadPaymentCategory();
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
            lblLayoutCategory.Text = Session["_layoutCategoryName"].ToString();
            lblLayoutName.Text = Session["_layoutName"].ToString();
            lblNoOfColumns.Text = Session["_cols"].ToString();
            lblNoOfRows.Text = Session["_rows"].ToString();

            CreateDynamicTableLower(Convert.ToInt32(Session["_LayoutCode"].ToString()));
            CreateDynamicTableBtnUpper(Convert.ToInt32(Session["_LayoutCode"].ToString()));

        }
    }
    private void loadPaymentCategory()//M1
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_bus_layout_get_pmttypelist");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlPaymentCategory.DataSource = dt;
                    ddlPaymentCategory.DataTextField = "paymentcategory_name";
                    ddlPaymentCategory.DataValueField = "paymentcategory_code";
                    ddlPaymentCategory.DataBind();

                    ddlPaymentCategory.SelectedIndex = 0;
                }
            }

        }
        catch (Exception ex)
        {
            ddlPaymentCategory.Items.Clear();
            ddlPaymentCategory.Enabled = false;
            _common.ErrorLog("SysAdmLayoutSeatTypes-M1", ex.Message.ToString());
        }
    }
    private void CreateDynamicTableLower(int MlayoutCode)//M2
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
                        btn.Click += ItemSelectBtn_click;
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
            _common.ErrorLog("SysAdmLayoutSeatTypes-M2", ex.Message.ToString());
        }
    }
    private void CreateDynamicTableBtnUpper(int MlayoutCode)//M3
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
                                case "G" // General
                               :
                                    {
                                        btn.CssClass = "form-control seatTextbox";
                                        break;
                                    }

                                case "F" // Women
                         :
                                    {
                                        btn.CssClass = "form-control seatTextboxWomen";
                                        break;
                                    }

                                case "C":
                                    {
                                        btn.CssClass = "form-control seatTextboxConductor";
                                        break;
                                    }

                                case "M" // Male
                         :
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
                            btn.Click += ItemSelectBtn_click;
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
            _common.ErrorLog("SysAdmLayoutSeatTypes-M3", ex.Message.ToString());
        }
    }
    private void loadTravellerType()//M5
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_bus_layout_get_trvltypelist");
            MyCommand.Parameters.AddWithValue("p_layoutcode", Convert.ToInt32(Session["_LayoutCode"].ToString()));
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            { 
                if (dt.Rows.Count > 0)
                {
                    ddlSeatType.DataSource = dt;
                    ddlSeatType.DataTextField = "travellertype_name";
                    ddlSeatType.DataValueField = "travellertype_code";
                    ddlSeatType.DataBind();

                    ddlSeatType.SelectedIndex = 0;
                }
            }

        }
        catch (Exception ex)
        {

            _common.ErrorLog("SysAdmLayoutSeatTypes-M5", ex.Message.ToString());
        }
    }
    private string getTravellerTypeofSeat(int mlayoutCode, int mRowNumber, int mColNumber)//M6
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
            return "";
        }
    }
    private void InsertSeatConfig()//M7
    {
        try
        {


            string MTravellerTypeCode = ddlSeatType.SelectedValue;
            string MSEATAVAILFORONLBOOKING = rblAvailabilityStatus.SelectedValue;
            string MSEATPAYMENTCATEGORYCODE = ddlPaymentCategory.SelectedValue;
            int mlayout = Convert.ToInt32(Session["_LayoutCode"].ToString());
            int mrownumber = Convert.ToInt32(Session["_rowNumber"].ToString());
            int mcolumnnumber = Convert.ToInt32(Session["_colNumber"].ToString());
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_bus_layout_seat_config");//SPBUSLAYOUTPARAMETERUPDATES
            MyCommand.Parameters.AddWithValue("p_layoutcode", mlayout);
            MyCommand.Parameters.AddWithValue("p_rowsno", mrownumber);
            MyCommand.Parameters.AddWithValue("p_columnno", mcolumnnumber);
            MyCommand.Parameters.AddWithValue("p_travellertypecode", MTravellerTypeCode);
            MyCommand.Parameters.AddWithValue("p_seatavailforonlbooking", MSEATAVAILFORONLBOOKING);
            MyCommand.Parameters.AddWithValue("p_seatpaymentcategorycode", MSEATPAYMENTCATEGORYCODE);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    Response.Redirect("SysAdmLayoutSeatTypes.aspx");
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    private void LayoutLock()//M8
    {
        try
        {
            int MLayoutCode;
            if (!(Session["_LayoutCode"] == null))
            {
                MLayoutCode = Convert.ToInt32(Session["_LayoutCode"].ToString());
                MyCommand = new NpgsqlCommand();
                MyCommand.Parameters.Clear();
                MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_bus_layout_lock");
                MyCommand.Parameters.AddWithValue("p_layoutcode", MLayoutCode);
                dt = bll.SelectAll(MyCommand);
                if (dt.TableName == "Success")
                {
                    if (dt.Rows.Count > 0)
                    {
                        Successmsg("Layout has successfully been locked, Now You can attach this layout with a bus");
                    }
                    else
                    {

                    }
                }
                else
                {

                }
            }
        }
        catch (Exception ex)
        {

        }

    }
    private void Successmsg(string msg)
    {
        string popup = _popup.modalPopupSmall("S", "Success", msg, "Close");
        Response.Write(popup);
    }
    private void Errormsg(string msg)
    {
        string popup = _popup.modalPopupSmall("D", "Please Check", msg, "Close");
        Response.Write(popup);
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


    #region "Method"
    private void ItemSelectBtn_click(object sender, EventArgs e)//ME1
    {
        try
        {
            Button Cbtn = (Button)sender;
            string MrowNumber;
            string MColumn;
            MrowNumber = Cbtn.Attributes["rowNumber"].ToString();
            MColumn = Cbtn.Attributes["colNumber"].ToString();

            string mSenderID;
            mSenderID = Cbtn.ID;

            Session["_rowNumber"] = Convert.ToInt16(MrowNumber);
            Session["_colNumber"] = Convert.ToInt16(MColumn);

            loadTravellerType();
            rblAvailabilityStatus.SelectedIndex = 0;
            ddlPaymentCategory.Enabled = true;
            lblSelectedSeat.Text = "You Can Change Here The Details of Seat No " + Cbtn.Text.Trim();
            mpseatconfig.Show();
        }
        catch (Exception ex)
        {
            _common.ErrorLog("SysAdmLayoutSeatTypes-E1", ex.Message.ToString());
        }
    }
    protected void ddlSeatType_SelectedIndexChanged(object sender, EventArgs e)
    {
        rblAvailabilityStatus.SelectedIndex = 0;
        ddlPaymentCategory.SelectedIndex = 0;
        ddlPaymentCategory.Enabled = true;
        rblAvailabilityStatus.Enabled = true;
        if (ddlSeatType.SelectedValue == "C")
        {
            ddlPaymentCategory.Enabled = false;
            rblAvailabilityStatus.Enabled = false;
            rblAvailabilityStatus.SelectedIndex = 1;
        }
        mpseatconfig.Show();
    }
    protected void lbtnyes_Click(object sender, EventArgs e)
    {
        InsertSeatConfig();
    }
    protected void lbtnSaveNLock_Click(object sender, EventArgs e)
    {
        lblConfirmation.Text = "Once locked the layout will not be available for editing. Want to proceed with locking the layout?";
        mpConfirmation.Show();
        Session["_Action"] = "L";
    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        if (Session["_Action"].ToString() == "L")
        {
            LayoutLock();
        }
    }
    protected void lbtnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("SysAdmLayoutMain.aspx");
    }

    #endregion









   
}