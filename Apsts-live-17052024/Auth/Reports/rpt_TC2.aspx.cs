using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Npgsql;
using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class Auth_Reports_rpt_TC2 : System.Web.UI.Page
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    ReportDocument cryrpt = new ReportDocument();
    string rpt = System.Web.HttpContext.Current.Server.MapPath("~/Auth/Reports/rpt_TC2.rpt");
    protected void Page_Load(object sender, EventArgs e)
    {
        checkForSecurity();
        Session["_moduleName"] = "GST On Ticket Booking";
        if (!IsPostBack)
        {


            txtfromdate.Text = DateTime.Now.ToString("dd") +"/"+DateTime.Now.ToString("MM")+"/"+DateTime.Now.ToString("yyyy");


            loadReportType(Session["_RoleCode"].ToString());
            if (Session["_ReportType"] != null && Session["_ReportType"].ToString() != "")
            {
                ddlReportType.SelectedValue = "8";
                loadReport(Session["_RoleCode"].ToString(), ddlReportType.SelectedValue.ToString());
                ddlReport.SelectedValue = Session["_Report"].ToString();
                dvperameter.Visible = true;
                lbtnsearch.Visible = true;
                Session.Remove("_ReportType");
                Session.Remove("_Report");
            }

            loadservicetype(ddlServicetype);
            loaddepot(ddldepot);
            loadService(ddlServicetype.SelectedValue, ddldepot.SelectedValue, ddlservice);
            loadroute(ddlServicetype.SelectedValue, ddlroute);
            loadagent(ddlagent);
            Random randomclass = new Random();
            Session["rndNoCheck"] = randomclass.Next().ToString();
            hidtoken.Value = Session["rndNoCheck"].ToString();
        }
        lblReportName.Text = "Report " + ddlReport.SelectedItem.Text;
        //Test();
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
            _common.ErrorLog("rpt_TC1.aspx-001", ex.Message.ToString());
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
            _common.ErrorLog("rpt_TC1.aspx-002", ex.Message.ToString());
        }

    }
    private void loadservicetype(DropDownList ddlservicetype)
    {
        try
        {
            ddlservicetype.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            DataTable dt;
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_servicetype");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlservicetype.DataSource = dt;
                    ddlservicetype.DataTextField = "service_type_nameen";
                    ddlservicetype.DataValueField = "srtpid";
                    ddlservicetype.DataBind();
                }
            }
            ddlservicetype.Items.Insert(0, "All");
            ddlservicetype.Items[0].Value = "0";
            ddlservicetype.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlservicetype.Items.Insert(0, "All");
            ddlservicetype.Items[0].Value = "0";
            ddlservicetype.SelectedIndex = 0;
        }
    }
    private void loaddepot(DropDownList ddldepot)
    {
        try
        {
            ddldepot.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            DataTable dt;
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
            ddldepot.Items.Insert(0, "All");
            ddldepot.Items[0].Value = "0";
            ddldepot.SelectedIndex = 0;
        }
    }
    private void loadService(string servicetypecode, string depotcode, DropDownList ddlservice)
    {
        try
        {
            ddlservice.Items.Clear();

            ddlservice.Items.Insert(0, "All");
            ddlservice.Items[0].Value = "0";
            ddlservice.SelectedIndex = 0;
        }
        catch (Exception ex)
        {

        }
    }
    private void loadroute(string selectedValue, DropDownList ddlroute)
    {
        try
        {
            ddlroute.Items.Clear();

            ddlroute.Items.Insert(0, "All");
            ddlroute.Items[0].Value = "0";
            ddlroute.SelectedIndex = 0;
        }
        catch (Exception ex)
        {

        }
    }



    private void loadagent(DropDownList ddlagent)
    {
        try
        {
            ddlagent.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            DataTable dt;
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_agent");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlagent.DataSource = dt;
                    ddlagent.DataTextField = "agentname_";
                    ddlagent.DataValueField = "agentcode_";
                    ddlagent.DataBind();
                }
            }
            ddlagent.Items.Insert(0, "All");
            ddlagent.Items[0].Value = "0";
            ddlagent.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlagent.Items.Insert(0, "All");
            ddlagent.Items[0].Value = "0";
            ddlagent.SelectedIndex = 0;
        }
    }



    private DataTable LoadData()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            DataTable dt;
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "reports.f_get_rpt_tc2");
            MyCommand.Parameters.AddWithValue("@p_bookdate", txtfromdate.Text);
            MyCommand.Parameters.AddWithValue("@p_srtp", Convert.ToInt32(ddlServicetype.SelectedValue));
            MyCommand.Parameters.AddWithValue("@p_depot", ddldepot.SelectedValue.ToString());
            MyCommand.Parameters.AddWithValue("@p_dsvc", ddlservice.SelectedValue.ToString());
            MyCommand.Parameters.AddWithValue("@p_route", ddlroute.SelectedValue.ToString());
            MyCommand.Parameters.AddWithValue("@p_user", ddlagent.SelectedValue.ToString());
            dt = bll.SelectAll(MyCommand);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    private void downloadReport()
    {
        try
        {
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "reports.f_get_rpt_tc2");
            MyCommand.Parameters.AddWithValue("@p_bookdate", txtfromdate.Text);
            MyCommand.Parameters.AddWithValue("@p_srtp", Convert.ToInt32(ddlServicetype.SelectedValue));
            MyCommand.Parameters.AddWithValue("@p_depot", ddldepot.SelectedValue.ToString());
            MyCommand.Parameters.AddWithValue("@p_dsvc", ddlservice.SelectedValue.ToString());
            MyCommand.Parameters.AddWithValue("@p_route", ddlroute.SelectedValue.ToString());
            MyCommand.Parameters.AddWithValue("@p_user", ddlagent.SelectedValue.ToString());
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                cryrpt.Load(rpt);
                cryrpt.SetDataSource(dt);
                XmlDocument doc = new XmlDocument();
                doc.Load(Server.MapPath("../../CommonData.xml"));
                XmlNodeList title = doc.GetElementsByTagName("dept_Name_en");
                CrystalDecisions.CrystalReports.Engine.TextObject objtxtorgname = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txtorgname"];
                objtxtorgname.Text = title.Item(0).InnerXml;

                CrystalDecisions.CrystalReports.Engine.TextObject objtxtRPTNUMBER = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txtreportnumber"];
                objtxtRPTNUMBER.Text = "5.2";
                CrystalDecisions.CrystalReports.Engine.TextObject objtxtRPTNAME = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txtreportname"];
                objtxtRPTNAME.Text = ddlReportType.SelectedItem.Text;

                CrystalDecisions.CrystalReports.Engine.TextObject objtxtRPTCenterNAME = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txtCenterReportName"];
                objtxtRPTCenterNAME.Text = ddlReport.SelectedItem.Text;
                CrystalDecisions.CrystalReports.Engine.TextObject objtxtRPTFromDate = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txtfromdate"];
                objtxtRPTFromDate.Text = txtfromdate.Text;
                CrystalDecisions.CrystalReports.Engine.TextObject objtxtRPTService = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txtservice"];
                objtxtRPTService.Text = ddlServicetype.SelectedItem.Text;

                CrystalDecisions.CrystalReports.Engine.TextObject objtxtRPTDepot = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txtdepot"];
                objtxtRPTDepot.Text = ddldepot.SelectedItem.Text;

                CrystalDecisions.CrystalReports.Engine.TextObject objtxtRPTCounter = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txtcounter"];
                objtxtRPTCounter.Text = ddlagent.SelectedItem.Text;

                CrystalDecisions.CrystalReports.Engine.TextObject objtxtRPTURL = (TextObject)cryrpt.ReportDefinition.Sections[4].ReportObjects["txturl"];
                objtxtRPTURL.Text = "Downloaded From -" + Request.Url.AbsoluteUri;
                cryrpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Context.Response, true, "Agent Daily Current Booking Report-" + DateTime.Now.ToString() + DateTime.Now); ;
            }
            else
                errorMassage("No Record Found");
        }
        catch (Exception ex)
        {
            errorMassage("Something went wrong. Please contact to tech team.");
            _common.ErrorLog("rpt_T8.aspx-004", ex.Message.ToString());
        }
    }
    private void ExportGridToExcel()
    {
        if (gvcurntbookingreport.Rows.Count <= 0)
        {
            errorMassage("No Record");
            return;
        }

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=Counter Daily Current Booking-" + DateTime.Now.ToString() + ".xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);


            gvcurntbookingreport.AllowPaging = false;
            DataTable dt = LoadData();
            if (dt.Rows.Count > 0)
            {
                gvcurntbookingreport.DataSource = dt;
                gvcurntbookingreport.DataBind();
            }

            gvcurntbookingreport.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in gvcurntbookingreport.HeaderRow.Cells)
            {
                cell.BackColor = gvcurntbookingreport.HeaderStyle.BackColor;
            }

            foreach (GridViewRow row in gvcurntbookingreport.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = gvcurntbookingreport.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = gvcurntbookingreport.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            gvcurntbookingreport.RenderControl(hw);


            string style = "<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }


    public override void VerifyRenderingInServerForm(Control control)
    {

    }


    private bool IsValidValue()
    {
        try
        {
            DateTime dtFrom;
            if (!DateTime.TryParseExact(txtfromdate.Text.Trim(), "dd/MM/yyyy", null, DateTimeStyles.None, out dtFrom))
            {
                errorMassage("Select Valid Booking Date");
                return false;
            }
            else if (dtFrom > DateTime.Now)
            {
                errorMassage("Please Enter Valid Booking Date");
                return false;
            }

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    #endregion

    #region Event
    protected void ddlReportType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (hidtoken.Value != Session["rndNoCheck"].ToString())
        {
            Response.Redirect("errorpage.aspx");
        }

        dvperameter.Visible = false;
        lbtnsearch.Visible = false;
        pnlMsg.Visible = true;
        pnlReport.Visible = false;
        ddlReport.Items.Clear();
        ddlReport.Items.Insert(0, "Select");
        ddlReport.Items[0].Value = "0";
        ddlReport.SelectedIndex = 0;

        loadReport(Session["_RoleCode"].ToString(), ddlReportType.SelectedValue.ToString());
    }
    protected void ddlReport_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (hidtoken.Value != Session["rndNoCheck"].ToString())
        {
            Response.Redirect("errorpage.aspx");
        }
        if (ddlReport.SelectedIndex == 0)
        {
            lbtnsearch.Visible = false;
            dvperameter.Visible = false;
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

    protected void lbtnsearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (!IsValidValue())
            {
                return;
            }
            pnlReport.Visible = false;
            pnlMsg.Visible = true;
            DataTable MyTable = LoadData();
            if (MyTable.Rows.Count > 0)
            {
                gvcurntbookingreport.DataSource = MyTable;
                gvcurntbookingreport.DataBind();
                pnlReport.Visible = true;
                pnlMsg.Visible = false;
            }
            else
            {
                errorMassage("Sorry No Record available");
                return;
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void lbtnPDF_Click(object sender, EventArgs e)
    {
        if (hidtoken.Value != Session["rndNoCheck"].ToString())
        {
            Response.Redirect("errorpage.aspx");
        }
        downloadReport();
    }
    protected void lbtnEXCEL_Click(object sender, EventArgs e)
    {
        ExportGridToExcel();
    }
    private void CitizenAuth_rpt_TC1_Unload(object sender, EventArgs e)
    {
        if (cryrpt != null)
        {
            cryrpt.Close();
            cryrpt.Dispose();
        }
    }
    protected void gvcurntbookingreport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvcurntbookingreport.PageIndex = e.NewPageIndex;
        DataTable dt = LoadData();
        gvcurntbookingreport.DataSource = dt;
        gvcurntbookingreport.DataBind();
    }


    protected void ddldepot_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadService(ddlServicetype.SelectedValue, ddldepot.SelectedValue, ddlservice);
    }
    #endregion
}