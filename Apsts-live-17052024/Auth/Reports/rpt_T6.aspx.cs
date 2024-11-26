
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

public partial class Auth_Reports_rpt_T6 : BasePage
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    ReportDocument cryrpt = new ReportDocument();
    string rpt = System.Web.HttpContext.Current.Server.MapPath("~/Auth/Reports/rpt_T6.rpt");

    protected void Page_Load(object sender, EventArgs e)
    {
        checkForSecurity();
        Session["_moduleName"] = "Daily Business Report";
        if (!IsPostBack)
        {

            txtfromdate.Text = DateTime.Now.AddDays(-15).ToString("dd") + "/" + DateTime.Now.AddDays(-15).ToString("MM") + "/" + DateTime.Now.AddDays(-15).ToString("yyyy");//DateTime.Now.AddDays(-30).ToString("dd/MM/yyyy");
            txttodate.Text = DateTime.Now.AddDays(0).ToString("dd") + "/" + DateTime.Now.AddDays(0).ToString("MM") + "/" + DateTime.Now.AddDays(0).ToString("yyyy");//DateTime.Now.AddDays(0).ToString("dd/MM/yyyy");

            loadReportType(Session["_RoleCode"].ToString());
            if (Session["_ReportType"] != null && Session["_ReportType"].ToString() != "")
            {
                ddlReportType.SelectedValue = "4";
                loadReport(Session["_RoleCode"].ToString(), ddlReportType.SelectedValue.ToString());
                ddlReport.SelectedValue = Session["_Report"].ToString();
                fromdate.Visible = true;
                Todate.Visible = true;
                lblagent.Visible = true;
                ddlagent.Visible = true;
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
            _common.ErrorLog("rpt_T6.aspx-001", ex.Message.ToString());

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
            _common.ErrorLog("rpt_T6.aspx-002", ex.Message.ToString());
        }

    }
    public DataTable LoadGrid()
    {
        try
        {
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "reports.f_get_rpt_t6");
            MyCommand.Parameters.AddWithValue("p_from_date", txtfromdate.Text);
            MyCommand.Parameters.AddWithValue("p_to_date", txttodate.Text);
            MyCommand.Parameters.AddWithValue("p_agent", ddlagent.SelectedValue);


            DataTable dt = new DataTable();
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                lblsmry.Text = "Total Record <b> " + dt.Rows.Count.ToString() + " </b>" + " | Total Booking Amount <b>" + dt.Rows[0]["tot_booking_amt"].ToString() + " ₹ </b>" + " | Total Cancellation Amount <b>" + dt.Rows[0]["cancel_amt"].ToString() + " ₹ </b>";


                gvOnlineBooking.DataSource = dt;
                gvOnlineBooking.DataBind();
                pnlMsg.Visible = false;
                pnlReport.Visible = true;
                Test();
                return dt;
            }
            else
            {
                pnlMsg.Visible = true;
                pnlReport.Visible = false;
                errorMassage("No Record Found");
                return null;
            }

        }

        catch (Exception ex)
        {
            errorMassage("Something went wrong. Please contact to tech team.");
            _common.ErrorLog("rpt_T6.aspx-003", ex.Message.ToString());
            return null;
        }
    }
    private void downloadReport()
    {
        try
        {
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "reports.f_get_rpt_t6");
            MyCommand.Parameters.AddWithValue("p_from_date", txtfromdate.Text);
            MyCommand.Parameters.AddWithValue("p_to_date", txttodate.Text);
            MyCommand.Parameters.AddWithValue("p_agent", ddlagent.SelectedValue);


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
                objtxtRPTNUMBER.Text = "4.6";
                CrystalDecisions.CrystalReports.Engine.TextObject objtxtRPTNAME = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txtreportname"];
                objtxtRPTNAME.Text = ddlReportType.SelectedItem.Text;

                CrystalDecisions.CrystalReports.Engine.TextObject objtxtRPTCenterNAME = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txtCenterReportName"];
                objtxtRPTCenterNAME.Text = ddlReport.SelectedItem.Text;
                CrystalDecisions.CrystalReports.Engine.TextObject objtxtRPTFromDate = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txtfromdate"];
                objtxtRPTFromDate.Text = txtfromdate.Text;
                CrystalDecisions.CrystalReports.Engine.TextObject objtxtRPTToDate = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txttodate"];
                objtxtRPTToDate.Text = txttodate.Text;
                CrystalDecisions.CrystalReports.Engine.TextObject objtxtRPTAgent = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txtagent"];
                objtxtRPTAgent.Text = ddlagent.SelectedItem.Text;
               
                CrystalDecisions.CrystalReports.Engine.TextObject objtxtRPTURL = (TextObject)cryrpt.ReportDefinition.Sections[4].ReportObjects["txturl"];
                objtxtRPTURL.Text = "Downloaded From -" + Request.Url.AbsoluteUri;
                cryrpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Context.Response, true, "Daily Business Report-" + DateTime.Now.ToString() + DateTime.Now); ;
            }
            else
                errorMassage("No Record Found");
        }
        catch (Exception ex)
        {
            errorMassage("Something went wrong. Please contact to tech team.");
            _common.ErrorLog("rpt_T6.aspx-004", ex.Message.ToString());
        }
    }
    private bool IsValidValue()
    {
        try
        {
            DateTime dtFrom;
            DateTime dtTo;

            if (!DateTime.TryParseExact(txtfromdate.Text.Trim(), "dd/MM/yyyy", null, DateTimeStyles.None, out dtFrom))
            {
                errorMassage("Select Valid From Date");
                return false;
            }
            else if (!DateTime.TryParseExact(txttodate.Text.Trim(), "dd/MM/yyyy", null, DateTimeStyles.None, out dtTo))
            {
                errorMassage("Select Valid To Date");
                return false;
            }
            else if (dtTo < dtFrom)
            {
                errorMassage("Please Enter Valid From Date");
                return false;
            }
            else if ((dtTo - dtFrom).Days > 15)
            {
                errorMassage("Please Note:- Reports can only be generated for 15 days at a time.");
                return false;
            }

            return true;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("rpt_T6.aspx-005", ex.Message.ToString());

            return false;

        }
    }
    private void ExportGridToExcel()
    {
        try
        {
            if (gvOnlineBooking.Rows.Count <= 0)
            {
                errorMassage("No Record");
                return;
            }

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Daily Business Report -" + DateTime.Now.ToString() + ".xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                // To Export all pages
                gvOnlineBooking.AllowPaging = false;
                DataTable dt = LoadGrid();
                if (dt.Rows.Count > 0)
                {
                    gvOnlineBooking.DataSource = dt;
                    gvOnlineBooking.DataBind();
                }

                gvOnlineBooking.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in gvOnlineBooking.HeaderRow.Cells)
                {
                    cell.BackColor = gvOnlineBooking.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in gvOnlineBooking.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = gvOnlineBooking.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = gvOnlineBooking.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                gvOnlineBooking.RenderControl(hw);


                string style = "<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }

        catch (Exception ex)
        {
            _common.ErrorLog("rpt_T6.aspx-006", ex.Message.ToString());

        }

    }
    private void Test()
    {
        try
        {
            if (gvOnlineBooking.Rows.Count > 0)
            {
                gvOnlineBooking.UseAccessibleHeader = true;
                gvOnlineBooking.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("rpt_T6.aspx-007", ex.Message.ToString());

        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

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
            fromdate.Visible = false;
            Todate.Visible = false;

            lblagent.Visible = false;
            ddlagent.Visible = false;
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

        fromdate.Visible = false;
            Todate.Visible = false;
            lbtnsearch.Visible = false;

            lblagent.Visible = false;
            ddlagent.Visible = false;
            pnlMsg.Visible = true;
            pnlReport.Visible = false;
            ddlReport.Items.Clear();
            ddlReport.Items.Insert(0, "Select");
            ddlReport.Items[0].Value = "0";
            ddlReport.SelectedIndex = 0;
        

        loadReport(Session["_RoleCode"].ToString(), ddlReportType.SelectedValue.ToString());
    }
    protected void gvOnlineBooking_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvOnlineBooking.PageIndex = e.NewPageIndex;
        this.LoadGrid();
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
    protected void lbtnSearch_Click(object sender, EventArgs e)
    {
        if (hidtoken.Value != Session["rndNoCheck"].ToString())
        {
            Response.Redirect("errorpage.aspx");
        }
        if (!IsValidValue())
        {
            gvOnlineBooking.DataSource = null;
            gvOnlineBooking.DataBind();
            pnlReport.Visible = false;
            pnlMsg.Visible = true;
            return;
        }

        else
        {
            LoadGrid();
        }

    }
    #endregion

}


