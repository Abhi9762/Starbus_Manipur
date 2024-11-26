using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Auth_SysAdmLayoutSeatNumbering : System.Web.UI.Page
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
        Session["_moduleName"] = "Layout Seats Numbering";
        if (!IsPostBack)
        {

            if (Session["_layoutCategory"].ToString() == "S")
                pnl1.Visible = true;
            else
            {
                lblTitleSeatsL.Visible = false;
                lblTitleSeatsU.Visible = false;
                pnl1.Visible = false;
            }
            // lblLayoutName.Text = Session("_layoutCategoryName").ToString 'Session("_layoutName").ToString
            lblLayoutCategory.Text = Session["_layoutCategoryName"].ToString();

            lblLayoutName.Text = Session["_layoutName"].ToString();
            lblNoOfColumns.Text = Session["_cols"].ToString();
            lblNoOfRows.Text = Session["_rows"].ToString();
            CreateDynamicTableBtnLower(Convert.ToInt32(Session["_layoutCode"].ToString()));
            CreateDynamicTableBtnUpper(Convert.ToInt32(Session["_layoutCode"].ToString()));
        }
    }

    #region "Method"
    private void CreateDynamicTableBtnLower(int MlayoutCode)//M1
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

            int MrowNumber;

            int McolNumber;
            for (MrowNumber = 1; MrowNumber <= Mrows; MrowNumber++)
            {
                TableRow dr = new TableRow();
                for (McolNumber = 1; McolNumber <= Mcols; McolNumber++)
                {
                    TableCell tc = new TableCell();
                    if (MrowNumber == 1 & McolNumber == Mcols)
                    {
                        ImageButton ibtn = new ImageButton();
                        ibtn.ImageUrl = "../assets/img/Seats/seatDriver.png";
                        ibtn.Width = 43;
                        ibtn.Height = 50;
                        tc.Controls.Add(ibtn);
                    }
                    else
                    {
                        TextBox txtBox = new TextBox();
                        txtBox.ID = "txt-" + MrowNumber.ToString() + "-" + McolNumber.ToString();
                        txtBox.CssClass = "form-control seatTextbox";
                        txtBox.MaxLength = 2;
                        txtBox.Width = 44;
                        tc.Controls.Add(txtBox);
                        if (isSeat(MlayoutCode, MrowNumber, McolNumber) == false)
                            txtBox.Visible = false;
                        else
                        {
                            txtBox.AutoPostBack = true;
                            txtBox.Text = getSeatNo(MlayoutCode, MrowNumber, McolNumber);
                            txtBox.Attributes.Add("rowNumber", MrowNumber.ToString());
                            txtBox.Attributes.Add("colNumber", McolNumber.ToString());
                            txtBox.TextChanged += new EventHandler(textBox_TextChanged); //txtBox_click;
                        }
                    }
                    tc.Style.Add("padding", "5px");
                    dr.Cells.Add(tc);
                }
                Table1.Rows.Add(dr);
                Table1.EnableViewState = true;
                ViewState["tblNumber"] = true;
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("SysAdmLayoutSeatNumbering-M1", ex.Message.ToString());
        }
    }
    private string getSeatNo(int mlayoutCode, int mrowNumber, int mcolNumber)//M2
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_bus_layout_get_seatno");
            MyCommand.Parameters.AddWithValue("p_layoutcode", mlayoutCode);
            MyCommand.Parameters.AddWithValue("p_rownumber", mrowNumber);
            MyCommand.Parameters.AddWithValue("p_columnnumber", mcolNumber);
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
            _common.ErrorLog("SysAdmLayoutSeatNumbering-M2", ex.Message.ToString());
            return "";
        }
    }
    private bool isSeat(int mlayoutCode, int mrowNumber, int mcolNumber)//M3
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_bus_layout_isseat");
            MyCommand.Parameters.AddWithValue("p_layoutcode", mlayoutCode);
            MyCommand.Parameters.AddWithValue("p_rownumber", mrowNumber);
            MyCommand.Parameters.AddWithValue("p_columnnumber", mcolNumber);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0][0].ToString() == "Y")
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("SysAdmLayoutSeatNumbering-M3", ex.Message.ToString());
            return false;
        }
    }
    protected void textBox_TextChanged(object sender, EventArgs e)//M4
    {
        try
        {

            TextBox CtextBox = (TextBox)sender;
            string MrowNumber;
            string MColumn;
            MrowNumber = CtextBox.Attributes["rowNumber"].ToString();
            MColumn = CtextBox.Attributes["colNumber"].ToString();

            string mSenderID;
            mSenderID = CtextBox.ID;

            Session["_rowNumber"] = Convert.ToInt16(MrowNumber);
            Session["_colNumber"] = Convert.ToInt16(MColumn);
        }
        catch (Exception ex)
        {
            _common.ErrorLog("SysAdmLayoutSeatNumbering-M4", ex.Message.ToString());
        }

    }
    private void CreateDynamicTableBtnUpper(int mlayoutcode)//M5
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
                var height = 1;
                var padding = 10;
                HtmlTable dt = new HtmlTable();
                int MtotRows;
                int MRowNumber;
                int MColNumber;
                MtotRows = Mrow + MrowU;
                for (MRowNumber = Mrow + 1; MRowNumber <= MtotRows; MRowNumber++)
                {
                    TableRow dr = new TableRow();
                    for (MColNumber = 1; MColNumber <= McolsU; MColNumber++)
                    {
                        TableCell dc = new TableCell();
                        // -------------------------------------------
                        TextBox txtBox = new TextBox();
                        txtBox.ID = "txt-" + MRowNumber.ToString() + "-" + MColNumber.ToString();
                        // txtBox.Text = SeatNo.ToString
                        txtBox.CssClass = "form-control seatTextboxSleeper";
                        txtBox.MaxLength = 2;
                        txtBox.Width = 96;
                        txtBox.Height = 50;
                        // txtBox.Attributes.Add("class", "seatTextbox")
                        dc.Controls.Add(txtBox);
                        if (isSeat(mlayoutcode, MRowNumber, MColNumber) == false)
                            txtBox.Visible = false;
                        else
                        {
                            txtBox.AutoPostBack = true;
                            txtBox.Text = getSeatNo(mlayoutcode, MRowNumber, MColNumber);
                            txtBox.Attributes.Add("rowNumber", MRowNumber.ToString());
                            txtBox.Attributes.Add("colNumber", MColNumber.ToString());
                            txtBox.TextChanged += new EventHandler(textBox_TextChanged);// txtBox_click;
                        }
                        dc.Style["padding"] = "10px";
                        dc.Controls.Add(txtBox);

                        dr.Cells.Add(dc);
                        height += 10;
                    }
                    Table2.Rows.Add(dr);
                    Table2.EnableViewState = true;
                    Table2.Width = Table1.Width;
                    ViewState["Table2"] = true;
                }
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("SysAdmLayoutSeatNumbering-M5", ex.Message.ToString());
        }
    }
    private void generateSeatNo(int layoutcode)//M6
    {
        try
        {
            string ipaddress = HttpContext.Current.Request.UserHostAddress;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_bus_layout_generate_seatnos");
            MyCommand.Parameters.AddWithValue("p_layoutcode", layoutcode);
            MyCommand.Parameters.AddWithValue("p_updatedby", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("p_ipaddress", ipaddress);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {

            }

        }
        catch (Exception ex)
        {
            _common.ErrorLog("SysAdmLayoutSeatNumbering-M6", ex.Message.ToString());
            // Errormsg(ex.Message.ToString());

        }
    }
    private void saveNProceed()//M7
    {
        try
        {
            int i;
            int MTotalCols =Convert.ToInt32(Session["_cols"].ToString());
            string MseatNo="0";
            int MRowNumber;
            int MColNumber;
            MRowNumber = 0;

            foreach (TableRow tr in Table1.Controls)
            {
                MRowNumber = MRowNumber + 1;
                MColNumber = 0;
                foreach (TableCell tc in tr.Controls)
                {
                    MColNumber = MColNumber + 1;
                    if (MRowNumber == 1 & MColNumber == MTotalCols)
                    {
                        UpdateSeats(Convert.ToInt32(Session["_layoutCode"].ToString()), MRowNumber, MColNumber,Convert.ToInt32(MseatNo.ToString()));
                    }
                    else if (tc.Controls[0] is TextBox)
                    {
                        TextBox txtBox = new TextBox();
                        txtBox =(TextBox)tc.Controls[0];
                        if (txtBox.Visible == true)
                        {
                            MseatNo = txtBox.Text.Trim();
                            UpdateSeats(Convert.ToInt32(Session["_layoutCode"].ToString()), MRowNumber, MColNumber, Convert.ToInt32(MseatNo.ToString()));
                        }
                    }
                }
            }
            if (Session["_layoutCategory"].ToString() == "S")
            {
                foreach (TableRow tr1 in Table2.Controls)
                {
                    MRowNumber = MRowNumber + 1;
                    MColNumber = 0;
                    foreach (TableCell tc in tr1.Controls)
                    {
                        MColNumber = MColNumber + 1;
                        if (tr1.Controls[0] is TextBox)
                        {
                            TextBox txtBox = new TextBox();
                            txtBox =(TextBox)tr1.Controls[0];
                            if (txtBox.Visible == true)
                            {
                                MseatNo = txtBox.Text.Trim();
                                UpdateSeats(Convert.ToInt32(Session["_layoutCode"].ToString()), MRowNumber, MColNumber, Convert.ToInt32(MseatNo.ToString()));
                            }
                        }
                    }
                }
            }
            Response.Redirect("SysAdmLayoutSeatTypes.aspx");
        }
        catch(Exception ex)
        {
            _common.ErrorLog("SysAdmLayoutSeatNumbering-M7", ex.Message.ToString());
        }
    }
    private void UpdateSeats(int layoutcode, int mRowNumber, int mColNumber, int mseatNo)//M8
    {
        try
        {
            string Mresult;
            if (AlreadyExists(mRowNumber, mColNumber, mseatNo) == true)
            {
                return;
            }
            string ipaddress = HttpContext.Current.Request.UserHostAddress;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_bus_layout_update_seatnos");
            MyCommand.Parameters.AddWithValue("p_layoutcode", layoutcode);
            MyCommand.Parameters.AddWithValue("p_rownumber", mRowNumber);
            MyCommand.Parameters.AddWithValue("p_columnnumber", mColNumber);
            MyCommand.Parameters.AddWithValue("p_seatno", mseatNo);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("SysAdmLayoutSeatNumbering-M8", ex.Message.ToString());
            return;
        }
    }
    private bool AlreadyExists(int rowNumber, int columnNo, int mseatsNo)//M9
    {
        try
        {
            int MTotalCols =Convert.ToInt32(Session["_cols"].ToString());
            int i;
            string MseatNo;
            int MRowNumber;
            int MColNumber;
            MRowNumber = 0;
            foreach (TableRow tr in Table1.Controls)
            {
                MRowNumber = MRowNumber + 1;
                MColNumber = 0;
                foreach (TableCell tc in tr.Controls)
                {
                    MColNumber = MColNumber + 1;
                    if (MRowNumber == rowNumber && MColNumber == columnNo)
                    {
                    }
                    else if (tc.Controls[0] is TextBox)
                    {
                        TextBox txtBox = new TextBox();
                        txtBox = (TextBox)tc.Controls[0];
                        if (txtBox.Visible == true)
                        {
                            MseatNo = txtBox.Text.Trim();
                            if (MseatNo == mseatsNo.ToString())
                            {
                                return true;
                            }                              
                        }
                    }
                }
            }
            if (Session["_layoutCategory"].ToString() == "S")
            {
                foreach (TableRow tr1 in Table2.Controls)
                {
                    MRowNumber = MRowNumber + 1;
                    MColNumber = 0;
                    foreach (TableCell tc in tr1.Controls)
                    {
                        MColNumber = MColNumber + 1;
                        if (MRowNumber == 1 & MColNumber == 1)
                        {
                        }
                        else if (tr1.Controls[0] is TextBox)
                        {
                            TextBox txtBox = new TextBox();
                            txtBox =(TextBox)tr1.Controls[0];
                            if (txtBox.Visible == true)
                            {
                                MseatNo = txtBox.Text.Trim();
                                if (MseatNo == mseatsNo.ToString())
                                {
                                    return true;
                                }                                 
                            }
                        }
                    }
                }
            }
            return false;
        }
        catch(Exception ex)
        {
            _common.ErrorLog("SysAdmLayoutSeatNumbering-M9", ex.Message.ToString());
            return false;
        }
    }
    protected override object SaveViewState()
    {
        object[] newViewState = new object[2] ;
        List<string> chkBox = new List<string>();

        foreach (TableRow row in Table1.Controls)
        {
            foreach (TableCell cell in row.Controls)
            {
              if (cell.Controls[0] is TextBox)
                {
                    TextBox txtbox = (TextBox)cell.Controls[0];
                    chkBox.Add(txtbox.Text);
                }
            }
        }

        if (Session["_layoutCategory"].ToString() == "S")
        {
            foreach (TableRow row in Table2.Controls)
            {
                foreach (TableCell cell in row.Controls)
                {

                    if (cell.Controls[0] is CheckBox)
                    {
                        TextBox txtbox = (TextBox)cell.Controls[0];
                        chkBox.Add(txtbox.Text);
                    }
                }
            }
        }
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
                CreateDynamicTableBtnLower(Convert.ToInt32(Session["_layoutCode"].ToString()));
                int i = 0;
                foreach (TableRow row in Table1.Controls)
                {
                    foreach (TableCell cell in row.Controls)
                    {
                        if (cell.Controls[0] is TextBox && i < chkBox.Length)
                        {
                            TextBox txtbox = (TextBox)cell.Controls[0];
                            txtbox.Text = chkBox[Math.Min(System.Threading.Interlocked.Increment(ref i), i - 1)];
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
                            if (cell.Controls[0] is TextBox && j < chkBox.Length)
                            {
                                TextBox txtbox = (TextBox)cell.Controls[0];
                                txtbox.Text = chkBox[Math.Min(System.Threading.Interlocked.Increment(ref j), j - 1)];
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
    protected void lbtnAutoNo_Click(object sender, EventArgs e)
    {
        lblConfirmation.Text = "Do you want to auto generate the seat numbers ?";
        mpConfirmation.Show();
        Session["_Action"] = "A";
    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        if (Session["_Action"].ToString() == "A")
        {
            generateSeatNo(Convert.ToInt16(Session["_layoutCode"].ToString()));
            Response.Redirect("SysAdmLayoutSeatNumbering.aspx");
        }
        if (Session["_Action"].ToString() == "S")
        {
            saveNProceed();
        }
    }
    protected void lbtnbtnSaveNProceed_Click(object sender, EventArgs e)
    {
        lblConfirmation.Text = "Do you want to save and proceed for Seat Type Configrations ?";
        mpConfirmation.Show();
        Session["_Action"] = "S";
    }
    protected void lbtnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("SysAdmLayoutMain.aspx");
    }
    
    #endregion

}