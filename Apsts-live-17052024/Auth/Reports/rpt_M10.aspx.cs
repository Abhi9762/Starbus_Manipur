
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_Reports_rpt_M10 : BasePage
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
        Session["_moduleName"] = "Bus Report";
        if (!IsPostBack)
        {

            Session["Depot"] = null;
            Session["Layout"] = null;
            Session["Service"] = null;

            loadReportType(Session["_RoleCode"].ToString());
            loadServiceType();
            loadBusDepot();
            if (Session["_ReportType"] != null && Session["_ReportType"].ToString() != "")
            {
                ddlReportType.SelectedValue = "1";
                loadReport(Session["_RoleCode"].ToString(), ddlReportType.SelectedValue.ToString());
                ddlReport.SelectedValue = Session["_Report"].ToString();
                lblDepo.Visible = true;
                lblServiceType.Visible = true;
                ddlStripId.Visible = true;
                ddlDepo.Visible = true;
                lbtnSearch.Visible = true;
                lblstatus.Visible = true;
                ddlstatus.Visible = true;
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
    private void loadServiceType()
    {
        try
        {
            ddlStripId.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_servicetype");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlStripId.DataSource = dt;
                    ddlStripId.DataTextField = "service_type_nameen";
                    ddlStripId.DataValueField = "srtpid";
                    ddlStripId.DataBind();
                }
            }

            ddlStripId.Items.Insert(0, "All");
            ddlStripId.Items[0].Value = "0";
            ddlStripId.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlStripId.Items.Insert(0, "SELECT");
            ddlStripId.Items[0].Value = "0";
            ddlStripId.SelectedIndex = 0;
            _common.ErrorLog("rpt_M10.aspx-001", ex.Message.ToString());
        }

    }
    private void loadBusDepot()
    {
        try
        {
            ddlDepo.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_depot_list");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlDepo.DataSource = dt;
                    ddlDepo.DataTextField = "depot_name";
                    ddlDepo.DataValueField = "depot_code";
                    ddlDepo.DataBind();
                }
            }

            ddlDepo.Items.Insert(0, "All");
            ddlDepo.Items[0].Value = "0";
            ddlDepo.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlDepo.Items.Insert(0, "SELECT");
            ddlDepo.Items[0].Value = "0";
            ddlDepo.SelectedIndex = 0;
            _common.ErrorLog("rpt_M10.aspx-002", ex.Message.ToString());
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
            _common.ErrorLog("rpt_M10.aspx-003", ex.Message.ToString());
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
            _common.ErrorLog("rpt_M10.aspx-004", ex.Message.ToString());
        }

    }
    public void loadBus()
    {
        try
        {
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "reports.f_get_rpt_m10");
            MyCommand.Parameters.AddWithValue("p_strpid", Convert.ToInt32(ddlStripId.SelectedValue));
            MyCommand.Parameters.AddWithValue("p_depotcode", ddlDepo.SelectedValue);
            MyCommand.Parameters.AddWithValue("p_status", ddlstatus.SelectedValue);
            DataTable dt = new DataTable();
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                lbltotalbus.Text = dt.Rows[0]["total_bus"].ToString();
                lblactive.Text = dt.Rows[0]["active_bus"].ToString();
                lbldiscontinue.Text = dt.Rows[0]["discontinue_bus"].ToString();

                DataRow[] Activecount = dt.Select("currentstatus='Active'");
                lblActiveSearch.Text = Activecount.Length.ToString();
                DataRow[] Discontinuecount = dt.Select("currentstatus='Discontinue'");
                lbldiscontinue.Text = Discontinuecount.Length.ToString();
                lblTotalSearch.Text = dt.Rows.Count.ToString();
                gvBus.DataSource = dt;
                gvBus.DataBind();
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
            _common.ErrorLog("rpt_M10.aspx-005", ex.Message.ToString());
        }
    }
    private void Test()
    {
        try
        {
            if (gvBus.Rows.Count > 0)
            {
                gvBus.UseAccessibleHeader = true;
                gvBus.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
        catch (Exception ex)
        {

            _common.ErrorLog("rpt_M10.aspx-006", ex.Message.ToString());
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
            lblServiceType.Visible = false;
            ddlStripId.Visible = false;
            lblDepo.Visible = false;
            ddlDepo.Visible = false;
            lblstatus.Visible = false;
            ddlstatus.Visible = false;
            pnlMsg.Visible = true;
            pnlReport.Visible = false;
            Session["Depot"] = null;
            Session["Layout"] = null;
            Session["Service"] = null;
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
        Session["Depot"] = null;
        Session["Layout"] = null;
        Session["Service"] = null;

        loadBus();
    }
    protected void ddlReportType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (hidtoken.Value != Session["rndNoCheck"].ToString())
        {
            Response.Redirect("errorpage.aspx");
        }
        lbtnSearch.Visible = false;
        lblDepo.Visible = false;
        ddlDepo.Visible = false;
        lblServiceType.Visible = false;
        ddlStripId.Visible = false;
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
    protected void gvBus_RowDataBound(object sender, GridViewRowEventArgs e)
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

                if (Session["Depot"] != null)
                {
                    if (Session["Depot"].ToString().ToUpper() == rowView["DEPOT_NAME"].ToString().ToUpper())
                        e.Row.Cells[1].Text = "";
                    else
                    {
                        Session["Depot"] = rowView["DEPOT_NAME"].ToString();

                        e.Row.Cells[1].Visible = true;
                    }
                }
                else
                    Session["Depot"] = rowView["DEPOT_NAME"].ToString();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }
    #endregion


}


