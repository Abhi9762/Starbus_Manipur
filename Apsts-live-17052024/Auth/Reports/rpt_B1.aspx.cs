
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Npgsql;
using System;

using System.Data;
using System.Web.UI.WebControls;
using System.Xml;

public partial class Auth_Reports_rpt_B1 : BasePage
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    ReportDocument cryrpt = new ReportDocument();
    string rpt = System.Web.HttpContext.Current.Server.MapPath("~/Auth/Reports/rpt_B1.rpt");

    protected void Page_Load(object sender, EventArgs e)
    {
        checkForSecurity();
        Session["_moduleName"] = "Active Bus Pass Reports";
        if (!IsPostBack)
        {

            loadCategory();
            LoadBusPass();
            loadReportType(Session["_RoleCode"].ToString());
            if (Session["_ReportType"] != null && Session["_ReportType"].ToString() != "")
            {
                ddlReportType.SelectedValue = "2";
                loadReport(Session["_RoleCode"].ToString(), ddlReportType.SelectedValue.ToString());
                ddlReport.SelectedValue = Session["_Report"].ToString();
                lblcategory.Visible = true;
                ddlcategory.Visible = true;
                lblbuspasstype.Visible = true;
                ddlbuspasstype.Visible = true;
                lbtnsearch.Visible = true;
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
            ddlcategory.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.bus_pass_category_list");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlcategory.DataSource = dt;
                    ddlcategory.DataTextField = "buspass_categoryname";
                    ddlcategory.DataValueField = "buspass_categoryid";
                    ddlcategory.DataBind();
                }
            }

            ddlcategory.Items.Insert(0, "All");
            ddlcategory.Items[0].Value = "0";
            ddlcategory.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlcategory.Items.Insert(0, "SELECT");
            ddlcategory.Items[0].Value = "0";
            ddlcategory.SelectedIndex = 0;
            _common.ErrorLog("rpt_B1.aspx-001", ex.Message.ToString());
        }
    }

    private void LoadBusPass()
    {
        try
        {
            ddlbuspasstype.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_bus_pass_type_list");

            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlbuspasstype.DataSource = dt;
                    ddlbuspasstype.DataTextField = "buspasstypename";
                    ddlbuspasstype.DataValueField = "buspasstypeid";
                    ddlbuspasstype.DataBind();
                }
            }

            ddlbuspasstype.Items.Insert(0, "All");
            ddlbuspasstype.Items[0].Value = "0";
            ddlbuspasstype.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlbuspasstype.Items.Insert(0, "SELECT");
            ddlbuspasstype.Items[0].Value = "0";
            ddlbuspasstype.SelectedIndex = 0;
            _common.ErrorLog("rpt_B1.aspx-002", ex.Message.ToString());

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
            _common.ErrorLog("rpt_B1.aspx-003", ex.Message.ToString());
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
            _common.ErrorLog("rpt_B1.aspx-004", ex.Message.ToString());
        }

    }
    public void loadGrid()
    {
        try
        {
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "reports.f_get_rpt_b1");
            MyCommand.Parameters.AddWithValue("p_category", Convert.ToInt16(ddlcategory.SelectedValue));
            MyCommand.Parameters.AddWithValue("p_bus_type_pass", Convert.ToInt16(ddlbuspasstype.SelectedValue));
            DataTable dt = new DataTable();
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {

                gvActiveBusPass.DataSource = dt;
                gvActiveBusPass.DataBind();
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
            _common.ErrorLog("rpt_B1.aspx-005", ex.Message.ToString());
        }
    }


    private void downloadReport()
    {
        try
        {
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "reports.f_get_rpt_b1");
            MyCommand.Parameters.AddWithValue("p_category", Convert.ToInt16(ddlcategory.SelectedValue));
            MyCommand.Parameters.AddWithValue("p_bus_type_pass", Convert.ToInt16(ddlbuspasstype.SelectedValue));
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
                objtxtRPTNUMBER.Text = "2.1";
                CrystalDecisions.CrystalReports.Engine.TextObject objtxtRPTNAME = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txtreportname"];
                objtxtRPTNAME.Text = ddlReportType.SelectedItem.Text;

                CrystalDecisions.CrystalReports.Engine.TextObject objtxtRPTCenterNAME = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txtCenterReportName"];
                objtxtRPTCenterNAME.Text = ddlReport.SelectedItem.Text;
                CrystalDecisions.CrystalReports.Engine.TextObject objtxtRPTCategory = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txtCategory"];
                objtxtRPTCategory.Text = ddlcategory.SelectedItem.Text;
                CrystalDecisions.CrystalReports.Engine.TextObject objtxtRPTBusTypePass = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txtbuspasstype"];
                objtxtRPTBusTypePass.Text = ddlbuspasstype.SelectedItem.Text;
                CrystalDecisions.CrystalReports.Engine.TextObject objtxtRPTURL = (TextObject)cryrpt.ReportDefinition.Sections[1].ReportObjects["txturl"];
                objtxtRPTURL.Text = "Downloaded From -" + Request.Url.AbsoluteUri;
                cryrpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Context.Response, true, "ActiveBusPass-" + DateTime.Now.ToString() + DateTime.Now);
            }
            else

                errorMassage("No Record Found");
        }
        catch (Exception ex)
        {
            _common.ErrorLog("rpt_B1.aspx-006", ex.Message.ToString());
        }
    }


    private void Test()
    {
        try
        {

            if (gvActiveBusPass.Rows.Count > 0)
            {
                gvActiveBusPass.UseAccessibleHeader = true;
                gvActiveBusPass.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        catch (Exception ex)
        {
            _common.ErrorLog("rpt_B1.aspx-007", ex.Message.ToString());
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

            lbtnsearch.Visible = false;
            lblcategory.Visible = false;
            ddlcategory.Visible = false;
            lblbuspasstype.Visible = false;
            ddlbuspasstype.Visible = false;
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
        lbtnsearch.Visible = false;
        lblcategory.Visible = false;
        ddlcategory.Visible = false;
        lblbuspasstype.Visible = false;
        ddlbuspasstype.Visible = false;
        lbtnsearch.Visible = false;
        pnlMsg.Visible = true;
        pnlReport.Visible = false;

        ddlReport.Items.Clear();
        ddlReport.Items.Insert(0, "Select");
        ddlReport.Items[0].Value = "0";
        ddlReport.SelectedIndex = 0;

        loadReport(Session["_RoleCode"].ToString(), ddlReportType.SelectedValue.ToString());
    }

    protected void gvActiveBusPass_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvActiveBusPass.PageIndex = e.NewPageIndex;
        this.loadGrid();
    }
    protected void ddlcategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (hidtoken.Value != Session["rndNoCheck"].ToString())
        {
            Response.Redirect("errorpage.aspx");
        }
        LoadBusPass();
    }
    protected void lbtnDownload_Click(object sender, EventArgs e)
    {
        if (hidtoken.Value != Session["rndNoCheck"].ToString())
        {
            Response.Redirect("errorpage.aspx");
        }
        downloadReport();
    }
    protected void lbtnsearch_Click(object sender, EventArgs e)
    {
        if (hidtoken.Value != Session["rndNoCheck"].ToString())
        {
            Response.Redirect("errorpage.aspx");
        }
        loadGrid();
    }
    #endregion
}


