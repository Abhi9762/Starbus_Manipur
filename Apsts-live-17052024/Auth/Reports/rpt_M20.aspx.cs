
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_Reports_rpt_M20 : BasePage
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
        Session["_moduleName"] = "Bus Service Type Other Charges Reports";
        if (!IsPostBack)
        {

            Session["Office"] = null;
            loadReportType(Session["_RoleCode"].ToString());
            loadServiceType();
            loadBusDepot();
            loadroute();
            if (Session["_ReportType"] != null && Session["_ReportType"].ToString() != "")
            {
                ddlReportType.SelectedValue = "1";
                loadReport(Session["_RoleCode"].ToString(), ddlReportType.SelectedValue.ToString());
                ddlReport.SelectedValue = Session["_Report"].ToString();
                ddldepot.Visible = true;
                ddlService.Visible = true;
                ddlroute.Visible = true;
                lbldepot.Visible = true;
                lblservice.Visible = true;
                lblroute.Visible = true;
                ddlstatus.Visible = true;
                lblstatus.Visible = true;
                lbtnSearch.Visible = true;
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
    private void loadServiceType()
    {
        try
        {
            ddlService.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_servicetype");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlService.DataSource = dt;
                    ddlService.DataTextField = "service_type_nameen";
                    ddlService.DataValueField = "srtpid";
                    ddlService.DataBind();
                }
            }

            ddlService.Items.Insert(0, "All");
            ddlService.Items[0].Value = "0";
            ddlService.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlService.Items.Insert(0, "SELECT");
            ddlService.Items[0].Value = "0";
            ddlService.SelectedIndex = 0;
            _common.ErrorLog("rpt_M20.aspx-001", ex.Message.ToString());
        }

    }
    private void loadBusDepot()
    {
        try
        {
            ddldepot.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_depot_list");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddldepot.DataSource = dt;
                    ddldepot.DataTextField = "depot_name";
                    ddldepot.DataValueField = "depot_code";
                    ddldepot.DataBind();
                }
            }

            ddldepot.Items.Insert(0, "All");
            ddldepot.Items[0].Value = "0";
            ddldepot.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddldepot.Items.Insert(0, "SELECT");
            ddldepot.Items[0].Value = "0";
            ddldepot.SelectedIndex = 0;
            _common.ErrorLog("rpt_M20.aspx-002", ex.Message.ToString());
        }
    }
    private void loadroute()
    {
        try
        {

            ddlroute.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_routes");
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                ddlroute.DataSource = dt;
                ddlroute.DataTextField = "routename";
                ddlroute.DataValueField = "routeid";
                ddlroute.DataBind();
            }
            ddlroute.Items.Insert(0, "All");
            ddlroute.Items[0].Value = "0";
            ddlroute.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlroute.Items.Insert(0, "SELECT");
            ddlroute.Items[0].Value = "0";
            ddlroute.SelectedIndex = 0;
            _common.ErrorLog("rpt_M20.aspx-003", ex.Message.ToString());
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
            _common.ErrorLog("rpt_M20.aspx-004", ex.Message.ToString());
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
            _common.ErrorLog("rpt_M20.aspx-005", ex.Message.ToString());
        }

    }
    public void loadGrid()
    {
        try
        {
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "reports.f_get_rpt_m20");
            MyCommand.Parameters.AddWithValue("p_depot", ddldepot.SelectedValue);
            MyCommand.Parameters.AddWithValue("p_servicetypeid", Convert.ToInt16(ddlService.SelectedValue));
            MyCommand.Parameters.AddWithValue("p_routeid", Convert.ToInt32(ddlroute.SelectedValue));
            MyCommand.Parameters.AddWithValue("p_status", ddlstatus.SelectedValue);
            DataTable dt = new DataTable();
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                lbltotaldepot.Text = dt.Rows[0]["total_depot"].ToString();
                lblactive.Text = dt.Rows[0]["active_depot"].ToString();
                lbldiscontinue.Text = dt.Rows[0]["discontinue_depot"].ToString();
                DataRow[] Activecount = dt.Select("val_status='A'");
                lblActiveSearch.Text = Activecount.Length.ToString();
                DataRow[] Discontinuecount = dt.Select("val_status='D'");
                lblDiscontinueSearch.Text = Discontinuecount.Length.ToString();
                lblTotalSearch.Text = dt.Rows.Count.ToString();
                gvdepot.DataSource = dt;
                gvdepot.DataBind();
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
            _common.ErrorLog("rpt_M20.aspx-006", ex.Message.ToString());
        }
    }
    private void Test()
    {
        try
        {
            if (gvdepot.Rows.Count > 0)
            {
                gvdepot.UseAccessibleHeader = true;
                gvdepot.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        catch (Exception ex)
        {
            _common.ErrorLog("rpt_M20.aspx-007", ex.Message.ToString());
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
            ddldepot.Visible = false;
            ddlService.Visible = false;
            ddlroute.Visible = false;
            pnlMsg.Visible = true;
            pnlReport.Visible = false;
            lbldepot.Visible = false;
            lblservice.Visible = false;
            lblroute.Visible = false;
            ddlstatus.Visible = false;
            lblstatus.Visible = false;
            Session["Office"] = null;
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
        Session["Office"] = null;
        loadGrid();
    }
    protected void ddlReportType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (hidtoken.Value != Session["rndNoCheck"].ToString())
        {
            Response.Redirect("errorpage.aspx");
        }
        lbtnSearch.Visible = false;
        ddldepot.Visible = false;
        ddlService.Visible = false;
        ddlroute.Visible = false;
        pnlMsg.Visible = true;
        pnlReport.Visible = false;
        lbldepot.Visible = false;
        lblservice.Visible = false;
        lblroute.Visible = false;
        ddlstatus.Visible = false;
        lblstatus.Visible = false;
        ddlReport.Items.Clear();
        ddlReport.Items.Insert(0, "Select");
        ddlReport.Items[0].Value = "0";
        ddlReport.SelectedIndex = 0;
        loadReport(Session["_RoleCode"].ToString(), ddlReportType.SelectedValue.ToString());
    }
    protected void gvdepot_RowDataBound(object sender, GridViewRowEventArgs e)
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

                if (Session["Office"] != null)
                {
                    if (Session["Office"].ToString().ToUpper() == rowView["officename"].ToString().ToUpper())
                        e.Row.Cells[1].Text = "";
                    else
                    {
                        Session["Office"] = rowView["officename"].ToString();

                        e.Row.Cells[1].Visible = true;
                    }
                }
                else
                    Session["Office"] = rowView["officename"].ToString();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }
    #endregion

}


