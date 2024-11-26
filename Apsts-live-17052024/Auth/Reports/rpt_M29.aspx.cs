
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_Reports_rpt_M29 : BasePage
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    protected void Page_Load(object sender, EventArgs e)
    {
        checkForSecurity();
        Session["_moduleName"] = "Bus Layout Report";
        if (!IsPostBack)
        {
            Session["Designation"] = null;
            Session["pOffice"] = null;
            Session["Office"] = null;
            Session["Lvl"] = null;

            loadOfficeLevel();
            loadOffice();
            LoadPostingofc();
            loadDesignation();

            loadReportType(Session["_RoleCode"].ToString());


            if (Session["_ReportType"] != null && Session["_ReportType"].ToString() != "")
            {
                ddlReportType.SelectedValue = "1";
                loadReport(Session["_RoleCode"].ToString(), ddlReportType.SelectedValue.ToString());
                ddlReport.SelectedValue = Session["_Report"].ToString();

                lblofclvl.Visible = true;
                lbldesignation.Visible = true;
                lblofc.Visible = true;
                lblPostingOfc.Visible = true;
                ddlOfclvl.Visible = true;
                ddlofc.Visible = true;
                ddlpostingOFC.Visible = true;
                ddldesignation.Visible = true;
                lblstatus.Visible = true;
                ddlstatus.Visible = true;
                lbtnSearch.Visible = true;
                Session.Remove("_ReportType");
                Session.Remove("_Report");
            }

            Random randomclass = new Random();
            Session["rndNoCheck"] = randomclass.Next().ToString();
            hidtoken.Value = Session["rndNoCheck"].ToString();
        }
        lblReportName.Text = "Report " + ddlReport.SelectedItem.Text;
        Test();
    }

    #region "Method"
    private void checkForSecurity()
    {
        //if (Session.IsNewSession == false || Request.ServerVariables["HTTP_REFERER"].Length < 1)
        //{
        //    Response.Redirect("../Errorpage.aspx");
        //}

        if (_security.isSessionExist(Session["_UserCode"]) == true)
        {
            Session["_UserCode"] = Session["_UserCode"];
        }
        else
        {
            Response.Redirect("sessionTimeout.aspx");
        }
        if (_security.isSessionExist(Session["_RNDIDENTIFIERSADM"]) == true)
        {
            Session["_RNDIDENTIFIERSADM"] = Session["_RNDIDENTIFIERSADM"];
        }
        else
        {
            Response.Redirect("sessionTimeout.aspx");
        }
        if (_security.checkvalidation() == false)
        {
            Response.Redirect("Errorpage.aspx");
        }
        if (_security.CheckOldUserLogin(Session["_UserCode"].ToString()) == false)
        {
            Response.Redirect("Errorpage.aspx");
        }
    }
    public void errorMassage(string msg)
    {
        lblerrmsg.Text = msg;
        mpError.Show();
    }
    private void loadOfficeLevel()
    {
        ddlOfclvl.Items.Clear();
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_officelvl");
            dt = bll.SelectAll(MyCommand);

            if (dt.Rows.Count > 0)
            {
                ddlOfclvl.DataSource = dt;
                ddlOfclvl.DataTextField = "ofclvl_name";
                ddlOfclvl.DataValueField = "ofclvl_id";
                ddlOfclvl.DataBind();
            }
            ddlOfclvl.Items.Insert(0, "All");
            ddlOfclvl.Items[0].Value = "0";
            ddlOfclvl.SelectedIndex = 0;

        }
        catch (Exception ex)
        {
            ddlOfclvl.Items.Insert(0, "All");
            ddlOfclvl.Items[0].Value = "0";
            ddlOfclvl.SelectedIndex = 0;

            _common.ErrorLog("rpt_M29.aspx-001", ex.Message.ToString());
        }
    }
    private void loadOffice()
    {
        ddlofc.Items.Clear();
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_office");
            dt = bll.SelectAll(MyCommand);

            if (dt.Rows.Count > 0)
            {
                ddlofc.DataSource = dt;
                ddlofc.DataTextField = "officename";
                ddlofc.DataValueField = "officeid";
                ddlofc.DataBind();
            }
            ddlofc.Items.Insert(0, "All");
            ddlofc.Items[0].Value = "0";
            ddlofc.SelectedIndex = 0;
            lblofclvl.Visible = true;
            lbldesignation.Visible = true;
            lblofc.Visible = true;
            lblPostingOfc.Visible = true;
            ddlOfclvl.Visible = true;
            ddlofc.Visible = true;
            ddlpostingOFC.Visible = true;
            ddldesignation.Visible = true;
            lbtnSearch.Visible = true;
        }
        catch (Exception ex)
        {
            ddlofc.Items.Insert(0, "All");
            ddlofc.Items[0].Value = "0";
            ddlofc.SelectedIndex = 0;

            _common.ErrorLog("rpt_M29.aspx-002", ex.Message.ToString());
        }
    }
    private void LoadPostingofc()
    {
        ddlpostingOFC.Items.Clear();
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_emp_unitoffice");
            MyCommand.Parameters.AddWithValue("p_officeid", ddlofc.SelectedValue);
            dt = bll.SelectAll(MyCommand);

            if (dt.Rows.Count > 0)
            {
                ddlpostingOFC.DataSource = dt;
                ddlpostingOFC.DataTextField = "officename";
                ddlpostingOFC.DataValueField = "officeid";
                ddlpostingOFC.DataBind();
            }
            ddlpostingOFC.Items.Insert(0, "All");
            ddlpostingOFC.Items[0].Value = "0";
            ddlpostingOFC.SelectedIndex = 0;

        }
        catch (Exception ex)
        {
            ddldesignation.Items.Insert(0, "All");
            ddldesignation.Items[0].Value = "0";
            ddldesignation.SelectedIndex = 0;

            _common.ErrorLog("rpt_M29.aspx-003", ex.Message.ToString());
        }
    }
    private void loadDesignation()
    {
        ddldesignation.Items.Clear();
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_designation");
            dt = bll.SelectAll(MyCommand);

            if (dt.Rows.Count > 0)
            {
                ddldesignation.DataSource = dt;
                ddldesignation.DataTextField = "designationname";
                ddldesignation.DataValueField = "designationcode";
                ddldesignation.DataBind();
            }
            ddldesignation.Items.Insert(0, "All");
            ddldesignation.Items[0].Value = "0";
            ddldesignation.SelectedIndex = 0;

        }
        catch (Exception ex)
        {
            ddldesignation.Items.Insert(0, "All");
            ddldesignation.Items[0].Value = "0";
            ddldesignation.SelectedIndex = 0;

            _common.ErrorLog("rpt_M29.aspx-004", ex.Message.ToString());
        }
    }
    private void loadReport(string rolecode, string _rptype)
    {
        try
        {
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "reports.f_get_report");
            MyCommand.Parameters.AddWithValue("p_role", rolecode);
            MyCommand.Parameters.AddWithValue("p_reportcategoryid", Convert.ToInt32(_rptype));
            DataTable dt = new DataTable();
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                ddlReport.DataSource = dt;
                ddlReport.DataTextField = "val_report_name";
                ddlReport.DataValueField = "val_page_url";
                ddlReport.DataBind();
            }

            ddlReport.Items.Insert(0, "Select");
            ddlReport.Items[0].Value = "0";
            ddlReport.SelectedIndex = 0;




        }



        catch (Exception ex)
        {
            ddlReport.Items.Insert(0, "Select");
            ddlReport.Items[0].Value = "0";
            ddlReport.SelectedIndex = 0;
            _common.ErrorLog("rpt_M29.aspx-005", ex.Message.ToString());
        }

    }
    private void loadReportType(string rolecode)
    {
        try
        {
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "reports.f_get_report");
            MyCommand.Parameters.AddWithValue("p_role", rolecode);
            MyCommand.Parameters.AddWithValue("p_reportcategoryid", 0);
            DataTable dt = new DataTable();
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                ddlReportType.DataSource = dt;
                ddlReportType.DataTextField = "val_report_category_name";
                ddlReportType.DataValueField = "val_report_category_id";
                ddlReportType.DataBind();
            }

            ddlReportType.Items.Insert(0, "Select");
            ddlReportType.Items[0].Value = "0";
            ddlReportType.SelectedIndex = 0;

            ddlReport.Items.Insert(0, "Select");
            ddlReport.Items[0].Value = "0";
            ddlReport.SelectedIndex = 0;



        }



        catch (Exception ex)
        {
            ddlReportType.Items.Insert(0, "Select");
            ddlReportType.Items[0].Value = "0";
            ddlReportType.SelectedIndex = 0;

            ddlReport.Items.Insert(0, "Select");
            ddlReport.Items[0].Value = "0";
            ddlReport.SelectedIndex = 0;
            _common.ErrorLog("rpt_M29.aspx-006", ex.Message.ToString());
        }

    }
    public void loadGrid()
    {
        try
        {
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "reports.f_get_rpt_m29");
            MyCommand.Parameters.AddWithValue("p_officeid", ddlofc.SelectedValue.ToString());
            MyCommand.Parameters.AddWithValue("p_unitoffice", ddlOfclvl.SelectedValue.ToString());
            MyCommand.Parameters.AddWithValue("p_status", ddlstatus.SelectedValue.ToString());
            MyCommand.Parameters.AddWithValue("p_designation", Convert.ToInt32(ddldesignation.SelectedValue));
            MyCommand.Parameters.AddWithValue("p_officelvl", Convert.ToInt32(ddlOfclvl.SelectedValue));
            DataTable dt = new DataTable();
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                lbltotalemployee.Text = dt.Rows[0]["total_employee"].ToString();
                lblactive.Text = dt.Rows[0]["actuve_employee"].ToString();
                lbldiscontinue.Text = dt.Rows[0]["discontinue_employee"].ToString();
                DataRow[] Activecount = dt.Select("emp_status='ACTIVE'");
                lblActiveSearch.Text = Activecount.Length.ToString();
                DataRow[] Discontinuecount = dt.Select("emp_status='DEACTIVE'");
                lblDiscontinueSearch.Text = Discontinuecount.Length.ToString();
                lblTotalSearch.Text = dt.Rows.Count.ToString();
                gvEmployee.DataSource = dt;
                gvEmployee.DataBind();
                pnlMsg.Visible = false;
                pnlReport.Visible = true;
                Test();
            }
            else
            {
                pnlMsg.Visible = true;
                pnlReport.Visible = false;
                errorMassage("No Record Found");
            }

        }

        catch (Exception ex)
        {
            _common.ErrorLog("rpt_M29.aspx-007", ex.Message.ToString());
        }
    }
    private void Test()
    {
        try
        {

            if (gvEmployee.Rows.Count > 0)
            {
                gvEmployee.UseAccessibleHeader = true;
                gvEmployee.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        catch (Exception ex)
        {
            _common.ErrorLog("rpt_M29.aspx-008", ex.Message.ToString());
        }
    }
    #endregion 

    #region "Event"
    protected void ddlReport_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (hidtoken.Value != Session["rndNoCheck"].ToString())
        {
            Response.Redirect("errorpage.aspx");
        }
        if (ddlReport.SelectedIndex == 0)
        {
            lblofclvl.Visible = false;
            lbldesignation.Visible = false;
            lblofc.Visible = false;
            lblPostingOfc.Visible = false;
            ddlOfclvl.Visible = false;
            ddlofc.Visible = false;
            ddlpostingOFC.Visible = false;
            ddldesignation.Visible = false;
            lbtnSearch.Visible = false;
            lblstatus.Visible = false;
            ddlstatus.Visible = false;
            pnlMsg.Visible = true;
            pnlReport.Visible = false;
            Session["Designation"] = null;
            Session["pOffice"] = null;
            Session["Office"] = null;
            Session["Lvl"] = null;
        }
        else
        {
            Session["_ReportType"] = ddlReportType.SelectedValue;
            Session["_Report"] = ddlReport.SelectedValue;
            Response.Redirect(ddlReport.SelectedValue);
        }

    }
    protected void lbtnSearch_Click(object sender, EventArgs e)
    {
        if (hidtoken.Value != Session["rndNoCheck"].ToString())
        {
            Response.Redirect("errorpage.aspx");
        }
        Session["Designation"] = null;
        Session["pOffice"] = null;
        Session["Office"] = null;
        Session["Lvl"] = null;
        loadGrid();
    }
    protected void ddlReportType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (hidtoken.Value != Session["rndNoCheck"].ToString())
        {
            Response.Redirect("errorpage.aspx");
        }
        lblofclvl.Visible = false;
        lbldesignation.Visible = false;
        lblofc.Visible = false;
        lblPostingOfc.Visible = false;
        ddlOfclvl.Visible = false;
        ddlofc.Visible = false;
        ddlpostingOFC.Visible = false;
        ddldesignation.Visible = false;
        lbtnSearch.Visible = false;
        lblstatus.Visible = false;
        ddlstatus.Visible = false;
        pnlMsg.Visible = true;
        pnlReport.Visible = false;
        ddlReport.Items.Clear();
        ddlReport.Items.Insert(0, "Select");
        ddlReport.Items[0].Value = "0";
        ddlReport.SelectedIndex = 0;

        loadReport(Session["_RoleCode"].ToString(), ddlReportType.SelectedValue.ToString());
    }
    protected void ddlOfclvl_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (hidtoken.Value != Session["rndNoCheck"].ToString())
        {
            Response.Redirect("errorpage.aspx");
        }
        Session["Designation"] = null;
        Session["pOffice"] = null;
        Session["Office"] = null;
        Session["Lvl"] = null;
        if (ddlOfclvl.SelectedItem.Value == "0")
        {
            return;
        }

        loadOffice();
    }
    protected void ddlofc_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (hidtoken.Value != Session["rndNoCheck"].ToString())
        {
            Response.Redirect("errorpage.aspx");
        }
        Session["Designation"] = null;
        Session["pOffice"] = null;
        Session["Office"] = null;
        Session["Lvl"] = null;
        LoadPostingofc();
    }
    protected void ddlpostingOFC_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (hidtoken.Value != Session["rndNoCheck"].ToString())
        {
            Response.Redirect("errorpage.aspx");
        }
        Session["Designation"] = null;
        Session["pOffice"] = null;
        Session["Office"] = null;
        Session["Lvl"] = null;
        loadDesignation();
    }
    protected void gvEmployee_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (hidtoken.Value != Session["rndNoCheck"].ToString())
        {
            Response.Redirect("errorpage.aspx");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                DataRowView rowView = (DataRowView)e.Row.DataItem;

                if (Session["Lvl"] != null)
                {
                    if (Session["Lvl"].ToString().ToUpper() == rowView["emp_officelvlname"].ToString().ToUpper())
                        e.Row.Cells[1].Text = "";
                    else
                    {
                        Session["Lvl"] = rowView["emp_officelvlname"].ToString();

                        e.Row.Cells[1].Visible = true;
                    }
                }
                else
                    Session["Lvl"] = rowView["emp_officelvlname"].ToString();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }
    #endregion
}


