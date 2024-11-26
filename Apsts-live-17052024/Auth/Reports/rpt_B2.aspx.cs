
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Npgsql;
using System;

using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;
using System.Xml;

public partial class Auth_Reports_rpt_B2 : BasePage
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    ReportDocument cryrpt = new ReportDocument();
    string rpt = System.Web.HttpContext.Current.Server.MapPath("~/Auth/Reports/rpt_B2.rpt");
    protected void Page_Load(object sender, EventArgs e)
    {
        checkForSecurity();
        Session["_moduleName"] = "Bus Pass Issuance Report";
        if (!IsPostBack)
        {
            LoadMonth();
            LoadYear();
            loadCategory();
            loadReportType(Session["_RoleCode"].ToString());
            if (Session["_ReportType"] != null && Session["_ReportType"].ToString() != "")
            {
                ddlReportType.SelectedValue = "2";
                loadReport(Session["_RoleCode"].ToString(), ddlReportType.SelectedValue.ToString());
                ddlReport.SelectedValue = Session["_Report"].ToString();
                lblCategory.Visible = true;
                lblMonth.Visible = true;
                lblPasstype.Visible = true;
                lblYear.Visible = true;
                lblApplyMode.Visible = true;
                ddlApplyMode.Visible = true;
                ddlBusPassCategory.Visible = true;
                ddlMonth.Visible = true;
                ddlpasstype.Visible = true;
                ddlYear.Visible = true;
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
    private void loadCategory()
    {
        try
        {
            ddlBusPassCategory.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.bus_pass_category_list");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlBusPassCategory.DataSource = dt;
                    ddlBusPassCategory.DataTextField = "buspass_categoryname";
                    ddlBusPassCategory.DataValueField = "buspass_categoryid";
                    ddlBusPassCategory.DataBind();
                }
            }

            ddlBusPassCategory.Items.Insert(0, "All");
            ddlBusPassCategory.Items[0].Value = "0";
            ddlBusPassCategory.SelectedIndex = 0;


            ddlpasstype.Items.Insert(0, "All");
            ddlpasstype.Items[0].Value = "0";
            ddlpasstype.SelectedIndex = 0;

            ddlMonth.Items.Insert(0, "All");
            ddlMonth.Items[0].Value = "0";
            ddlMonth.SelectedIndex = 0;

            ddlYear.Items.Insert(0, "All");
            ddlYear.Items[0].Value = "0";
            ddlYear.SelectedIndex = 0;


            ddlApplyMode.Items.Insert(0, "All");
            ddlApplyMode.Items[0].Value = "0";
            ddlApplyMode.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlBusPassCategory.Items.Insert(0, "SELECT");
            ddlBusPassCategory.Items[0].Value = "0";
            ddlBusPassCategory.SelectedIndex = 0;
            _common.ErrorLog("rpt_B2.aspx-001", ex.Message.ToString());
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
            _common.ErrorLog("rpt_B2.aspx-002", ex.Message.ToString());
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
            _common.ErrorLog("rpt_B2.aspx-003", ex.Message.ToString());
        }

    }
    public void loadGrid()
    {
        try
        {
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "reports.f_get_rpt_b2");
            MyCommand.Parameters.AddWithValue("p_category", Convert.ToInt32(ddlBusPassCategory.SelectedValue));
            MyCommand.Parameters.AddWithValue("p_type", Convert.ToInt32(ddlpasstype.SelectedValue));
            MyCommand.Parameters.AddWithValue("p_month", ddlMonth.SelectedValue);
            MyCommand.Parameters.AddWithValue("p_year", ddlYear.SelectedValue);
            MyCommand.Parameters.AddWithValue("p_applymode", ddlApplyMode.SelectedValue);
            DataTable dt = new DataTable();
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {

                gvBuspass.DataSource = dt;
                gvBuspass.DataBind();
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
            _common.ErrorLog("rpt_B2.aspx-004", ex.Message.ToString());
        }
    }
    private void Test()
    {
        try
        {
            if (gvBuspass.Rows.Count > 0)
            {
                gvBuspass.UseAccessibleHeader = true;
                gvBuspass.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        catch (Exception ex)
        {
            _common.ErrorLog("rpt_B2.aspx-005", ex.Message.ToString());
        }
    }
    private void LoadYear()
    {
        try
        {
            int year = DateTime.Now.Year;
            for (int i = 2022; i <= year; i++)
            {
                ddlYear.Items.Add(i.ToString());
            }
        }

        catch (Exception ex)
        {
            _common.ErrorLog("rpt_B2.aspx-006", ex.Message.ToString());
        }
    }
    private void LoadMonth()
    {
        try
        {
            for (int month = 1; month <= 12; month++)
            {
                string monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
                ddlMonth.Items.Add(new ListItem(monthName, month.ToString().PadLeft(2, '0')));
            }
        }

        catch (Exception ex)
        {
            _common.ErrorLog("rpt_B2.aspx-007", ex.Message.ToString());
        }
    }
    private void downloadReport()
    {
        try
        {
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "reports.f_get_rpt_b2");
            MyCommand.Parameters.AddWithValue("p_category", Convert.ToInt16(ddlBusPassCategory.SelectedValue));
            MyCommand.Parameters.AddWithValue("p_type", Convert.ToInt16(ddlpasstype.SelectedValue));
            MyCommand.Parameters.AddWithValue("p_month", ddlMonth.SelectedValue);
            MyCommand.Parameters.AddWithValue("p_year", ddlYear.SelectedValue);
            MyCommand.Parameters.AddWithValue("p_applymode", ddlApplyMode.SelectedValue);
            DataTable dt = new DataTable();
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
                objtxtRPTNUMBER.Text = "2.2";
                CrystalDecisions.CrystalReports.Engine.TextObject objtxtRPTNAME = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txtreportname"];
                objtxtRPTNAME.Text = ddlReportType.SelectedItem.Text;

                CrystalDecisions.CrystalReports.Engine.TextObject objtxtRPTCenterNAME = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txtCenterReportName"];
                objtxtRPTCenterNAME.Text = ddlReport.SelectedItem.Text;
                CrystalDecisions.CrystalReports.Engine.TextObject objtxtRPTCategory = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txtCategory"];
                objtxtRPTCategory.Text = ddlBusPassCategory.SelectedItem.Text;
                CrystalDecisions.CrystalReports.Engine.TextObject objtxtRPTType = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txttype"];
                objtxtRPTType.Text = ddlpasstype.SelectedItem.Text;
                CrystalDecisions.CrystalReports.Engine.TextObject objtxtRPTYear = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txtYear"];
                objtxtRPTYear.Text = ddlYear.SelectedItem.Text;
                CrystalDecisions.CrystalReports.Engine.TextObject objtxtRPTMonth = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txtMonth"];
                objtxtRPTMonth.Text = ddlMonth.SelectedItem.Text;
                CrystalDecisions.CrystalReports.Engine.TextObject objtxtRPTApplyMode = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txtApplyMode"];
                objtxtRPTApplyMode.Text = ddlApplyMode.SelectedItem.Text;
                CrystalDecisions.CrystalReports.Engine.TextObject objtxtRPTURL = (TextObject)cryrpt.ReportDefinition.Sections[4].ReportObjects["txturl"];
                objtxtRPTURL.Text = "Downloaded From -" + Request.Url.AbsoluteUri;
                cryrpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Context.Response, true, "BussPassIssuance-" + DateTime.Now.ToString() + DateTime.Now); ;
            }
            else

                errorMassage("No Record Found");
        }
        catch (Exception ex)
        {
            _common.ErrorLog("rpt_B2.aspx-008", ex.Message.ToString());
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
            lblApplyMode.Visible = false;
            lblCategory.Visible = false;
            lblMonth.Visible = false;
            lblPasstype.Visible = false;
            lblYear.Visible = false;
            ddlApplyMode.Visible = false;
            ddlBusPassCategory.Visible = false;
            ddlMonth.Visible = false;
            ddlpasstype.Visible = false;
            ddlYear.Visible = false;
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
        if (hidtoken.Value != Session["rndNoCheck"].ToString())
        {
            Response.Redirect("errorpage.aspx");
        }
        lbtnSearch.Visible = false;
        lblCategory.Visible = false;
        lblApplyMode.Visible = false;

        lblMonth.Visible = false;
        lblPasstype.Visible = false;
        lblYear.Visible = false;
        ddlApplyMode.Visible = false;
        ddlBusPassCategory.Visible = false;
        ddlMonth.Visible = false;
        ddlpasstype.Visible = false;
        ddlYear.Visible = false;
        pnlMsg.Visible = true;
        pnlReport.Visible = false;
        ddlReport.Items.Clear();
        ddlReport.Items.Insert(0, "Select");
        ddlReport.Items[0].Value = "0";
        ddlReport.SelectedIndex = 0;

        loadReport(Session["_RoleCode"].ToString(), ddlReportType.SelectedValue.ToString());
    }
    protected void gvBuspass_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvBuspass.PageIndex = e.NewPageIndex;
        this.loadGrid();
    }
    protected void ddlBusPassCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (hidtoken.Value != Session["rndNoCheck"].ToString())
        {
            Response.Redirect("errorpage.aspx");
        }
        try
        {
            ddlpasstype.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_bus_pass_type_list_bustypeid");
            MyCommand.Parameters.AddWithValue("p_bustypeid", Convert.ToInt16(ddlBusPassCategory.SelectedValue));
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlpasstype.DataSource = dt;
                    ddlpasstype.DataTextField = "buspasstypename";
                    ddlpasstype.DataValueField = "buspasstypeid";
                    ddlpasstype.DataBind();
                }
            }

            ddlpasstype.Items.Insert(0, "All");
            ddlpasstype.Items[0].Value = "0";
            ddlpasstype.SelectedIndex = 0;



        }
        catch (Exception ex)
        {
            ddlpasstype.Items.Insert(0, "SELECT");
            ddlpasstype.Items[0].Value = "0";
            ddlpasstype.SelectedIndex = 0;
            _common.ErrorLog("rpt_B2.aspx-009", ex.Message.ToString());
        }
    }
    protected void lbtnDownload_Click(object sender, EventArgs e)
    {
        if (hidtoken.Value != Session["rndNoCheck"].ToString())
        {
            Response.Redirect("errorpage.aspx");
        }
        downloadReport();
    }
    protected void lbtnSearch_Click(object sender, EventArgs e)
    {
        if (hidtoken.Value != Session["rndNoCheck"].ToString())
        {
            Response.Redirect("errorpage.aspx");
        }
        loadGrid();
    }
    #endregion


}


