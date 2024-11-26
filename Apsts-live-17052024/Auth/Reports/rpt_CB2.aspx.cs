using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class Auth_Reports_rpt_CB2 : BasePage
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    ReportDocument cryrpt = new ReportDocument();
    string rpt = System.Web.HttpContext.Current.Server.MapPath("rpt_CB2.rpt");
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["MasterPageHeaderText"] = "Counter Reports";
        Session["_moduleName"] = "";
        if (!IsPostBack)
        {
            Random randomclass = new Random();
            Session["rndNoCheck"] = randomclass.Next().ToString();
            hidtoken.Value = Session["rndNoCheck"].ToString();
            Session["Fare"] = null;
            loadReportType(Session["_RoleCode"].ToString());
            if (Session["_ReportType"] != null && Session["_ReportType"].ToString() != "")
            {
                ddlReportType.SelectedValue = "6";
                loadReport(Session["_RoleCode"].ToString(), ddlReportType.SelectedValue.ToString());
                ddlReport.SelectedValue = Session["_Report"].ToString();
                fromdate.Visible = true;

                txtBookingdate.Text = DateTime.Now.ToString("dd")+"/"+DateTime.Now.ToString("MM")+"/"+DateTime.Now.ToString("yyyy");


                lbtnsearch.Visible = true;
                Session.Remove("_ReportType");
                Session.Remove("_Report");
            }


        }
        lblReportName.Text = "Report " + ddlReport.SelectedItem.Text;
    }
    
    #region Methods

    private void Errormsg(string msg)
    {
        string popup = _popup.modalPopupSmall("D", "Please Check", msg, "Close");
        Response.Write(popup);
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
            _common.ErrorLog("rpt_CB2.aspx-0001", ex.Message);
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
            _common.ErrorLog("rpt_CB2.aspx-0002", ex.Message);
        }

    }
    private void LoadGrid()
    {
        try
        {
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "counter_reports.f_get_cntr_cancelled_ticket");
            MyCommand.Parameters.AddWithValue("p_counter_id", Session["_UserCntrID"].ToString());
            MyCommand.Parameters.AddWithValue("p_cancel_date", txtBookingdate.Text);

            DataTable dt = new DataTable();
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                gvOnlineBooking.DataSource = dt;
                gvOnlineBooking.DataBind();
                pnlMsg.Visible = false;
                pnlReport.Visible = true;
            }
            else
            {
                pnlMsg.Visible = true;
                pnlReport.Visible = false;
                Errormsg("No Record is available");
            }
        }
        catch (Exception ex)
        {
            pnlMsg.Visible = true;
            pnlReport.Visible = false;
            Errormsg("No Record is available");
            _common.ErrorLog("rpt_CB2.aspx-0003", ex.Message);
        }
    }
    private void downloadReport()
    {
        // string CounterName = Session["OfficeName"].ToString();
        string Counter = Session["_UserCntrID"].ToString();
        try
        {
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "counter_reports.f_get_cntr_cancelled_ticket");
            MyCommand.Parameters.AddWithValue("p_counter_id", Session["_UserCntrID"].ToString());
            MyCommand.Parameters.AddWithValue("p_cancel_date", txtBookingdate.Text);

            DataTable dt = new DataTable();
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                cryrpt.Load(rpt);
                cryrpt.SetDataSource(dt);
                XmlDocument doc = new XmlDocument();
                doc.Load(Server.MapPath("../../CommonData.xml"));
                XmlNodeList deptname = doc.GetElementsByTagName("dept_Name_en");
                CrystalDecisions.CrystalReports.Engine.TextObject objtxtorgname = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txtorgname"];
                objtxtorgname.Text = deptname.Item(0).InnerXml;

                CrystalDecisions.CrystalReports.Engine.TextObject objtxtRPTNUMBER = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txtreportnumber"];
                objtxtRPTNUMBER.Text = "6.2";
                CrystalDecisions.CrystalReports.Engine.TextObject objtxtRPTNAME = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txtreportname"];
                objtxtRPTNAME.Text = ddlReportType.SelectedItem.Text;

                CrystalDecisions.CrystalReports.Engine.TextObject objtxtRPTCenterNAME = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txtCenterReportName"];
                objtxtRPTCenterNAME.Text = ddlReport.SelectedItem.Text;
                CrystalDecisions.CrystalReports.Engine.TextObject objtxtRPTFromDate = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txtfromdate"];
                objtxtRPTFromDate.Text = txtBookingdate.Text;


                CrystalDecisions.CrystalReports.Engine.TextObject objtxtRPTURL = (TextObject)cryrpt.ReportDefinition.Sections[4].ReportObjects["txturl"];
                objtxtRPTURL.Text = "Downloaded From -" + Request.Url.AbsoluteUri;
                cryrpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Context.Response, true, "Daily Online Cancellation Report-" + DateTime.Now.ToString() + DateTime.Now);


                ///////////////////
            }
            else
                Errormsg("No Record is available");
        }
        catch (Exception ex)
        {
            _common.ErrorLog("rpt_CB2.aspx-0004", ex.Message);
            return;
        }
    }
    private void CheckTokan()
    {
        if (Session["rndNoCheck"] == null || Session["rndNoCheck"].ToString() == "")
        {
            Response.Redirect("../errorpage.aspx");
        }

        if (Session["rndNoCheck"].ToString() != hidtoken.Value.ToString())
        {
            Response.Redirect("../errorpage.aspx");
        }
    }
    #endregion

    #region Events
    protected void ddlReport_SelectedIndexChanged(object sender, EventArgs e)
    {
        CheckTokan();
        if (ddlReport.SelectedIndex == 0)
        {
            lbtnsearch.Visible = false;
            fromdate.Visible = false;



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
    protected void ddlReportType_SelectedIndexChanged(object sender, EventArgs e)
    {
        CheckTokan();
        if (ddlReportType.SelectedIndex == 0)
        {
            ddlReport.Items.Clear();
            ddlReport.Items.Insert(0, "Select");
            ddlReport.Items[0].Value = "0";
            ddlReport.SelectedIndex = 0;
        }

        loadReport(Session["_RoleCode"].ToString(), ddlReportType.SelectedValue.ToString());
    }
    protected void gvOnlineBooking_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvOnlineBooking.PageIndex = e.NewPageIndex;
        LoadGrid();
    }
    protected void lbtnsearch_Click(object sender, EventArgs e)
    {
        CheckTokan();
        LoadGrid();
    }
    protected void lbtnDownload_Click(object sender, EventArgs e)
    {
        CheckTokan();
        downloadReport();
    }
    #endregion

    
}