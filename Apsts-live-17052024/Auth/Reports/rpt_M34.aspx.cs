
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_Reports_rpt_M34 : BasePage
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
        Session["_moduleName"] = "Registered SMS Report";
        if (!IsPostBack)
        {
            loadReportType(Session["_RoleCode"].ToString());


            if (Session["_ReportType"] != null && Session["_ReportType"].ToString() != "")
            {
                ddlReportType.SelectedValue = "1";
                loadReport(Session["_RoleCode"].ToString(), ddlReportType.SelectedValue.ToString());
                ddlReport.SelectedValue = Session["_Report"].ToString();

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

    public void errorMassage(string msg)
    {
        lblerrmsg.Text = msg;
        mpError.Show();
    }
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
            _common.ErrorLog("rpt_M34.aspx-001", ex.Message.ToString());
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
            _common.ErrorLog("rpt_M34.aspx-002", ex.Message.ToString());
        }

    }
    public void loadGrid()
    {
        try
        {
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "reports.f_get_rpt_m34");
            DataTable dt = new DataTable();
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                gvSms.DataSource = dt;
                gvSms.DataBind();

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
            _common.ErrorLog("rpt_M34.aspx-003", ex.Message.ToString());
        }
    }
    private void Test()
    {
        try
        {
            if (gvSms.Rows.Count > 0)
            {
                gvSms.UseAccessibleHeader = true;
                gvSms.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        catch (Exception ex)
        {
            _common.ErrorLog("rpt_M34.aspx-004", ex.Message.ToString());
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


            lbtnSearch.Visible = false;
            pnlMsg.Visible = true;
            pnlReport.Visible = false;

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
        loadGrid();
    }
    protected void ddlReportType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (hidtoken.Value != Session["rndNoCheck"].ToString())
        {
            Response.Redirect("errorpage.aspx");
        }
        lbtnSearch.Visible = false;
        pnlMsg.Visible = true;
        pnlReport.Visible = false;
        ddlReport.Items.Clear();
        ddlReport.Items.Insert(0, "Select");
        ddlReport.Items[0].Value = "0";
        ddlReport.SelectedIndex = 0;
        loadReport(Session["_RoleCode"].ToString(), ddlReportType.SelectedValue.ToString());
    }

    #endregion
}





