using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Auth_SysAdmLayoutSeatsYN : System.Web.UI.Page
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
        Session["_moduleName"] = "Create Bus Layout";
        if (!IsPostBack)
        {
            lblLayoutName.Text = Session["_layoutName"].ToString();
            lblNoOfColumns.Text = Session["_cols"].ToString();
            lblNoOfRows.Text = Session["_rows"].ToString();
            lblLayoutCategory.Text = Session["_layoutCategoryName"].ToString();

            if (Session["_layoutCategory"].ToString() == "S")
            {
                pnl1.Visible = true;
            }
            else
            {
                lblTitleSeatsL.Visible = false;
                lblTitleSeatsU.Visible = false;
                pnl1.Visible = false;
            }

            CreateDynamicTableBtn();
            CreateDynamicTableBtnUpper();
        }
    }

    #region "Method"
    private void CreateDynamicTableBtnUpper()//M1
    {
        try
        {      
        if (Session["_layoutCategory"].ToString() == "S")
        {
            int Mrow = Convert.ToInt16(Session["_rowsU"].ToString());
            int Mcols = Convert.ToInt16(Session["_colsU"].ToString());
            var chk = new CheckBox[13];
            var height = 1;
            var padding = 10;
            HtmlTable dt = new HtmlTable();
            for (var j = 1; j <= Mrow; j++)
            {
                TableRow dr = new TableRow();

                for (var i = 1; i <= Mcols; i++)
                {
                    TableCell dc = new TableCell();
                    chk[i] = new CheckBox();
                    chk[i].ID = "cbN" + j.ToString() + "_" + i.ToString();
                    chk[i].Text = " ";
                    chk[i].Checked = true;
                    chk[i].Width = 95;
                    chk[i].Height = 50;
                    //dc.Style("padding") = "10px";
                    dc.Controls.Add(chk[i]);
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
            _common.ErrorLog("SysAdmLayoutSeatsYN-M1", ex.Message.ToString());
        }
    }
    private void CreateDynamicTableBtn()//M2
    {
        try
        {

        
        int Mrow = Convert.ToInt16(Session["_rows"].ToString());
        int Mcols = Convert.ToInt16(Session["_cols"].ToString());



        var chk = new CheckBox[14];
        var height = 1;
        var padding = 10;
        HtmlTable dt = new HtmlTable();
        for (var j = 1; j <= Mrow; j++)
        {
            TableRow dr = new TableRow();

            for (var i = 1; i <= Mcols; i++)
            {
                TableCell dc = new TableCell();
                if (j == 1 & i == Mcols)
                {
                    ImageButton ibtn = new ImageButton();
                    ibtn.ImageUrl = "../assets/img/Seats/seatDriver.png";
                    ibtn.Width = 42;
                    ibtn.Height = 50;
                    //dc.Style("padding") = "5px";
                    dc.Controls.Add(ibtn);
                }
                else
                {
                    chk[i] = new CheckBox();
                    chk[i].ID = "cb" + j.ToString() + "_" + i.ToString();

                    chk[i].Text = " ";
                    chk[i].Checked = true;

                    chk[i].Width = 58;
                    chk[i].Height = 61;
                    //dc.Style("padding") = "0px";
                    dc.Controls.Add(chk[i]);
                }
                dr.Cells.Add(dc);
                height += 10;
            }
            Table1.Rows.Add(dr);
            Table1.EnableViewState = true;
            ViewState["Table1"] = true;
        }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("SysAdmLayoutSeatsYN-M2", ex.Message.ToString());
        }
    }

    private int saveLayout()//M3
    {
        try
        {
        string ipaddress = HttpContext.Current.Request.UserHostAddress;
        int Mlayoutcode=0;
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_bus_layout_create");
        MyCommand.Parameters.AddWithValue("p_layoutname", Session["_layoutName"].ToString());
        MyCommand.Parameters.AddWithValue("p_noofrows", Convert.ToInt32(Session["_rows"].ToString()));
        MyCommand.Parameters.AddWithValue("p_noofcolumn", Convert.ToInt32(Session["_cols"].ToString()));
        MyCommand.Parameters.AddWithValue("p_noofrowsu", Convert.ToInt32(Session["_rowsU"].ToString()));
        MyCommand.Parameters.AddWithValue("p_noofcolumnu", Convert.ToInt32(Session["_colsU"].ToString()));
        MyCommand.Parameters.AddWithValue("p_layoutcategory", Session["_layoutCategory"].ToString());
        MyCommand.Parameters.AddWithValue("p_updatedby", Session["_UserCode"].ToString());
        MyCommand.Parameters.AddWithValue("p_ipaddress", ipaddress);
        dt = bll.SelectAll(MyCommand);
        if (dt.TableName == "Success")
        {
            if (dt.Rows.Count > 0)
            {
                Mlayoutcode = Convert.ToInt32(dt.Rows[0]["p_layoutCode"].ToString());
                return Mlayoutcode;               
            }
        }
        return Mlayoutcode;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("SysAdmLayoutSeatsYN-M3", ex.Message.ToString());
           // Errormsg(ex.Message.ToString());
            return 0;
        }
    }
    private void saveSeats(int layoutcode, int mrowNumber, int mcolNumber, bool checkedd, string layoutCategory)//M4
    {
        try
        {
            string ipaddress = HttpContext.Current.Request.UserHostAddress;
            string MseatYn = "N";            
            if (checkedd == true)
            {
                MseatYn = "Y";
            }
           
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_bus_layout_insert_seats");
            MyCommand.Parameters.AddWithValue("p_layoutcode",layoutcode);
            MyCommand.Parameters.AddWithValue("p_rowsno", mrowNumber);
            MyCommand.Parameters.AddWithValue("p_columnno", mcolNumber);
            MyCommand.Parameters.AddWithValue("p_seatyn", MseatYn);
            MyCommand.Parameters.AddWithValue("p_layoutcategory", layoutCategory);
            MyCommand.Parameters.AddWithValue("p_updatedby", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("p_ipaddress", ipaddress);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
               
            }
           
        }
        catch (Exception ex)
        {
            _common.ErrorLog("SysAdmLayoutSeatsYN-M4", ex.Message.ToString());
           
        }
    }
    private void generateSeatNo(int layoutcode)//M5
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
            _common.ErrorLog("SysAdmLayoutSeatsYN-M5", ex.Message.ToString());
            // Errormsg(ex.Message.ToString());
          
        }
    }
    protected override object SaveViewState()
    {
        object[] newViewState = new object[2];
        List<bool> chkBox = new List<bool>();

        foreach (TableRow row in Table1.Controls)
        {
            foreach (TableCell cell in row.Controls)
            {
                if (cell.Controls[0] is CheckBox)
                {
                    CheckBox chk = (CheckBox)cell.Controls[0];
                    chkBox.Add(chk.Checked);
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
                        CheckBox chk = (CheckBox)cell.Controls[0];
                        chkBox.Add(chk.Checked);
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
        if (savedState is object[] && ((object[])savedState).Length == 2 && ((object[])savedState)[0] is bool[])
        {
            object[] newViewState = (object[])savedState;
            bool[] chkBox = (bool[])(newViewState[0]);

            if (chkBox.Length > 0)
            {
                CreateDynamicTableBtn();

                int i = 0;

                foreach (TableRow row in Table1.Controls)
                {
                    foreach (TableCell cell in row.Controls)
                    {
                        if (cell.Controls[0] is CheckBox && i < chkBox.Length)
                        {
                            CheckBox chk = (CheckBox)cell.Controls[0];
                            chk.Checked = chkBox[Math.Min(System.Threading.Interlocked.Increment(ref i), i - 1)];
                        }                            
                    }
                }
            }

            if (Session["_layoutCategory"].ToString() == "S")
            {
                CreateDynamicTableBtnUpper();
                int j = 0;

                foreach (TableRow row in Table2.Controls)
                {
                    foreach (TableCell cell in row.Controls)
                    {
                        if (cell.Controls[0] is CheckBox && j < chkBox.Length)
                        {
                            CheckBox chk = (CheckBox)cell.Controls[0];
                            chk.Checked = chkBox[Math.Min(System.Threading.Interlocked.Increment(ref j), j - 1)];
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
    protected void lbtnSave_Click(object sender, EventArgs e)
    {
        int i;
        int j;
        int MlayoutCode;
        string MlayoutCategory;

        // ---------------------------------------------------
        MlayoutCode = saveLayout();
        if (MlayoutCode > 0)
            Session["_layoutCode"] = MlayoutCode;
        // -----------------
        int MTotalCols =Convert.ToInt16(Session["_cols"].ToString());
        // ---------------------------------------------------
        int MrowNumber;
        int McolNumber;
        MrowNumber = 0;
        foreach (TableRow tr in Table1.Controls)
        {
            MrowNumber = MrowNumber + 1;
            McolNumber = 0;

            foreach (TableCell tc in tr.Controls)
            {
                McolNumber = McolNumber + 1;
                if (MrowNumber == 1 & McolNumber == MTotalCols)
                    saveSeats(Convert.ToInt32(Session["_layoutCode"].ToString()), MrowNumber, McolNumber, true, Session["_layoutCategory"].ToString());
                else if (tc.Controls[0] is CheckBox)
                {
                    CheckBox chk = (CheckBox)tc.Controls[0];
                    if ((chk.Checked == true))
                        saveSeats(Convert.ToInt32(Session["_layoutCode"].ToString()), MrowNumber, McolNumber, true, Session["_layoutCategory"].ToString());
                    else
                        saveSeats(Convert.ToInt32(Session["_layoutCode"].ToString()), MrowNumber, McolNumber, false, Session["_layoutCategory"].ToString());
                }
            }
        }

        // -----------------------------------------------------
        if (Session["_layoutCategory"].ToString() == "S")
        {
            foreach (TableRow tr in Table2.Controls)
            {
                MrowNumber = MrowNumber + 1;
                McolNumber = 0;

                foreach (TableCell tc in tr.Controls)
                {
                    McolNumber = McolNumber + 1;
                    if (tc.Controls[0] is CheckBox)
                    {
                        CheckBox chk = (CheckBox)tc.Controls[0];
                        if ((chk.Checked == true))
                            saveSeats(Convert.ToInt32(Session["_layoutCode"].ToString()), MrowNumber, McolNumber, true, Session["_layoutCategory"].ToString());
                        else
                            saveSeats(Convert.ToInt32(Session["_layoutCode"].ToString()), MrowNumber, McolNumber, false, Session["_layoutCategory"].ToString());
                    }
                }
            }
        }
        // ----------------------------------------------------- 

        // -------------------------Seat Numbering Update-----------------
        generateSeatNo(Convert.ToInt16 (Session["_layoutCode"].ToString()));
        // ------------------------------------------------------
       
        Response.Redirect("SysAdmLayoutSeatNumbering.aspx");
    }
     protected void lbtnReset_Click(object sender, EventArgs e)
    {
        Response.Redirect("SysAdmLayoutMain.aspx");
    }
    #endregion



}