
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

public partial class Auth_Reports_rpt_P3 : BasePage
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    ReportDocument cryrpt = new ReportDocument();
    string rpt = System.Web.HttpContext.Current.Server.MapPath("~/Auth/Reports/rpt_P3.rpt");

    protected void Page_Load(object sender, EventArgs e)
    {
        checkForSecurity();
        Session["_moduleName"] = "Refund Initiation";
        if (!IsPostBack)
        {
            loadpaymentgateway();
            txtfrmdate.Text = DateTime.Now.AddDays(-30).ToString("dd") + "/" + DateTime.Now.AddDays(-30).ToString("MM") + "/" + DateTime.Now.AddDays(-30).ToString("yyyy");//DateTime.Now.AddDays(-30).ToString("dd/MM/yyyy");

            txttodate.Text = DateTime.Now.AddDays(0).ToString("dd") + "/" + DateTime.Now.AddDays(0).ToString("MM") + "/" + DateTime.Now.AddDays(0).ToString("yyyy");//DateTime.Now.AddDays(0).ToString("dd/MM/yyyy");

            loadReportType(Session["_RoleCode"].ToString());
            if (Session["_ReportType"] != null && Session["_ReportType"].ToString() != "")
            {
                ddlReportType.SelectedValue = "3";
                loadReport(Session["_RoleCode"].ToString(), ddlReportType.SelectedValue.ToString());
                ddlReport.SelectedValue = Session["_Report"].ToString();
                fromdate.Visible = true;
                todate.Visible = true;
                ddlpayment.Visible = true;
                lblpayment.Visible = true;
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
    private void loadpaymentgateway()
    {
        try
        {
            ddlpayment.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_pmt_gatewayliststatus");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlpayment.DataSource = dt;
                    ddlpayment.DataTextField = "gatewayname";
                    ddlpayment.DataValueField = "gatewayid";
                    ddlpayment.DataBind();
                }
            }

            ddlpayment.Items.Insert(0, "All");
            ddlpayment.Items[0].Value = "0";
            ddlpayment.SelectedIndex = 0;



        }
        catch (Exception ex)
        {
            ddlpayment.Items.Insert(0, "SELECT");
            ddlpayment.Items[0].Value = "0";
            ddlpayment.SelectedIndex = 0;
            _common.ErrorLog("rpt_P3.aspx-001", ex.Message.ToString());
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
            _common.ErrorLog("rpt_P3.aspx-002", ex.Message.ToString());
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
            _common.ErrorLog("rpt_P3.aspx-003", ex.Message.ToString());
        }

    }
    public DataTable LoadGrid()
    {
        try
        {
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "reports.f_get_rpt_p3");
            MyCommand.Parameters.AddWithValue("p_gateway", Convert.ToInt32(ddlpayment.SelectedValue));
            MyCommand.Parameters.AddWithValue("p_fromdate", txtfrmdate.Text);
            MyCommand.Parameters.AddWithValue("p_todate", txttodate.Text);

            DataTable dt = new DataTable();
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                lblsmry.Text = "Total Record <b> " + dt.Rows.Count.ToString() + " </b>" + " | Transaction Amount <b> " + dt.Rows[0]["total_txn_amt"].ToString() + " ₹ </b>" + " | Cancelled Amount <b> " + dt.Rows[0]["cancel_amt"].ToString() + " ₹ </b>" + " | Refund Amount <b> " + dt.Rows[0]["cancel_amt"].ToString() + " ₹ </b>";
                gvPendingRefund.DataSource = dt;
                gvPendingRefund.DataBind();
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
            _common.ErrorLog("rpt_P3.aspx-004", ex.Message.ToString());
            return null;
        }
    }
    private void downloadReport()
    {
        try
        {
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "reports.f_get_rpt_p3");
            MyCommand.Parameters.AddWithValue("p_gateway", Convert.ToInt32(ddlpayment.SelectedValue));
            MyCommand.Parameters.AddWithValue("p_fromdate", txtfrmdate.Text);
            MyCommand.Parameters.AddWithValue("p_todate", txttodate.Text);

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
                objtxtRPTNUMBER.Text = "3.3";
                CrystalDecisions.CrystalReports.Engine.TextObject objtxtRPTNAME = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txtreportname"];
                objtxtRPTNAME.Text = ddlReportType.SelectedItem.Text;

                CrystalDecisions.CrystalReports.Engine.TextObject objtxtRPTCenterNAME = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txtCenterReportName"];
                objtxtRPTCenterNAME.Text = ddlReport.SelectedItem.Text;
                CrystalDecisions.CrystalReports.Engine.TextObject objtxtRPTPayment = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txtpayment"];
                objtxtRPTPayment.Text = ddlpayment.SelectedItem.Text;
                CrystalDecisions.CrystalReports.Engine.TextObject objtxtRPTFromDate = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txtfromdate"];
                objtxtRPTFromDate.Text = txtfrmdate.Text;
                CrystalDecisions.CrystalReports.Engine.TextObject objtxtRPTToDate = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txttodate"];
                objtxtRPTToDate.Text = txttodate.Text;
                CrystalDecisions.CrystalReports.Engine.TextObject objtxtRPTURL = (TextObject)cryrpt.ReportDefinition.Sections[4].ReportObjects["txturl"];
                objtxtRPTURL.Text = "Downloaded From -" + Request.Url.AbsoluteUri;
                cryrpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Context.Response, true, "Pending Refund Initiated-" + DateTime.Now.ToString() + DateTime.Now); ;
            }
            else
                errorMassage("No Record Found");
        }
        catch (Exception ex)
        {
            _common.ErrorLog("rpt_P3.aspx-005", ex.Message.ToString());
        }
    }
    private void Test()
    {
        try
        {
            if (gvPendingRefund.Rows.Count > 0)
            {
                gvPendingRefund.UseAccessibleHeader = true;
                gvPendingRefund.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("rpt_P3.aspx-006", ex.Message.ToString());

        }
    }
    private bool IsValidValue()
    {
        try
        {
            DateTime dtFrom;
            DateTime dtTo;

            if (!DateTime.TryParseExact(txtfrmdate.Text.Trim(), "dd/MM/yyyy", null, DateTimeStyles.None, out dtFrom))
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
            else if ((dtTo - dtFrom).Days > 30)
            {
                errorMassage("Please Note:- Reports can only be generated for 30 days at a time.");
                return false;
            }

            return true;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("rpt_P3.aspx-007", ex.Message.ToString());
            return false;

        }
    }


    private void ExportGridToExcel()
    {
        try
        {
            if (gvPendingRefund.Rows.Count <= 0)
            {
                errorMassage("No Record");
                return;
            }

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Payment Gateway-" + DateTime.Now.ToString() + ".xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                gvPendingRefund.AllowPaging = false;
                DataTable dt = LoadGrid();
                if (dt.Rows.Count > 0)
                {
                    gvPendingRefund.DataSource = dt;
                    gvPendingRefund.DataBind();
                }

                gvPendingRefund.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in gvPendingRefund.HeaderRow.Cells)
                {
                    cell.BackColor = gvPendingRefund.HeaderStyle.BackColor;
                }

                foreach (GridViewRow row in gvPendingRefund.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = gvPendingRefund.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = gvPendingRefund.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                gvPendingRefund.RenderControl(hw);
                string style = "<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }

        catch (Exception ex)
        {
            _common.ErrorLog("rpt_P3.aspx-010", ex.Message.ToString());

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
            lblpayment.Visible = false;
            ddlpayment.Visible = false;
            fromdate.Visible = false;
            todate.Visible = false;
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
        todate.Visible = false;
        ddlpayment.Visible = false;
        lblpayment.Visible = false;
        lbtnsearch.Visible = false;
        pnlMsg.Visible = true;
        pnlReport.Visible = false;
        ddlReport.Items.Clear();
        ddlReport.Items.Insert(0, "Select");
        ddlReport.Items[0].Value = "0";
        ddlReport.SelectedIndex = 0;

        loadReport(Session["_RoleCode"].ToString(), ddlReportType.SelectedValue.ToString());
    }
    protected void gvPendingRefund_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvPendingRefund.PageIndex = e.NewPageIndex;
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
    protected void lbtnSearch_Click(object sender, EventArgs e)
    {

        if (hidtoken.Value != Session["rndNoCheck"].ToString())
        {
            Response.Redirect("errorpage.aspx");
        }
        if (!IsValidValue())
        {
            gvPendingRefund.DataSource = null;
            gvPendingRefund.DataBind();
            pnlReport.Visible = false;
            pnlMsg.Visible = true;
            return;
        }

        else
        {
            LoadGrid();
        }

    }
   
    protected void lbtnEXCEL_Click(object sender, EventArgs e)
    {
        if (hidtoken.Value != Session["rndNoCheck"].ToString())
        {
            Response.Redirect("errorpage.aspx");
        }
        ExportGridToExcel();
    }

    #endregion
}


