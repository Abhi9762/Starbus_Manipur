using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class Auth_MainCscBookingByAgent : System.Web.UI.Page
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    ReportDocument cryrpt = new ReportDocument();
    string rpttransactiontype = System.Web.HttpContext.Current.Server.MapPath("Reports/rpt_CS2.rpt");
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["moduleName"] = "Common Service Centre - Sub CSC Daily Transaction Details";

        if (!IsPostBack)
        {
            GetCSCSubAgent();
            txtDateF.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

            if (Session["agcode"] != null && Session["agcode"].ToString() != "")
            {
                ddlagent.SelectedValue = Session["agcode"].ToString();
                txtDateF.Text = Session["FromDate"].ToString();
                txtDateT.Text = Session["ToDate"].ToString();
            }

            ShowTransactionDetails(ddlagent.SelectedValue, txtDateF.Text,txtDateT.Text);
        }

    }

    #region "Methods"


    private void Errormsg(string message)
    {
        lblerrormsg.Text = message;
        mperror.Show();
    }
    public DataTable ShowTransactionDetails(string p_csccode, string p_fdate,string p_tdate)
    {
        try
        {
            lbtndownload.Visible = false;
            lbtnexcel.Visible = false;
            grdmsg.Visible = true;
            grdtransactionDetails.Visible = false;
            pnlreport.Visible = false;

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", " csc.subcscregister");
            MyCommand.Parameters.AddWithValue("p_maincsc", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("p_agentcode", p_csccode);
            MyCommand.Parameters.AddWithValue("p_fdate", p_fdate);
            MyCommand.Parameters.AddWithValue("p_tdate", p_tdate);



            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    pnlreport.Visible = true;
                    lblsmry.Text = "Total Record <b> " + dt.Rows.Count.ToString() + " </b>";
                    grdtransactionDetails.DataSource = dt;
                    grdtransactionDetails.DataBind();
                    grdmsg.Visible = false;
                    grdtransactionDetails.Visible = true;
                    lbtndownload.Visible = true;
                    lbtnexcel.Visible = true;
                    return dt;
                }
            }
            return null;
        }
        catch (Exception ex)
        {
            // Handle the exception here
            return null;
        }
    }





    private void GetCSCSubAgent()
    {
        try
        {


            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "csc.subagentdetails");
            MyCommand.Parameters.AddWithValue("p_mainagent", Session["_UserCode"].ToString());
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {


                    ddlagent.DataSource = dt;
                    ddlagent.DataTextField = "agent_name";
                    ddlagent.DataValueField = "val_agent_code";
                    ddlagent.DataBind();
                }
            }


            ddlagent.Items.Insert(0, "Select");
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


    private void LoadReport()
    {
        string p_fdate = txtDateF.Text.ToString();
        string p_tdate = txtDateT.Text.ToString();

        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "csc.subcscregister");
        MyCommand.Parameters.AddWithValue("p_maincsc", Session["_UserCode"].ToString());
        MyCommand.Parameters.AddWithValue("p_agentcode", ddlagent.SelectedValue);
        MyCommand.Parameters.AddWithValue("p_fdate", p_fdate);
        MyCommand.Parameters.AddWithValue("p_tdate", p_tdate);
        dt = bll.SelectAll(MyCommand);
        if (dt.TableName == "Success")
        {
            if (dt.Rows.Count > 0)
            {
                cryrpt.Load(rpttransactiontype);
                cryrpt.SetDataSource(dt);
                XmlDocument doc = new XmlDocument();
                doc.Load(Server.MapPath("../CommonData.xml"));
                XmlNodeList deptname = doc.GetElementsByTagName("dept_Name_en");
                CrystalDecisions.CrystalReports.Engine.TextObject objtxtorgname = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txtorgname"];
                objtxtorgname.Text = deptname.Item(0).InnerXml;

                CrystalDecisions.CrystalReports.Engine.TextObject objtxtRPTNUMBER = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txtreportnumber"];
                objtxtRPTNUMBER.Text = "10.2";

                CrystalDecisions.CrystalReports.Engine.TextObject objtxtRPTNAME = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txtreportname"];
                objtxtRPTNAME.Text = "CSC Report";

                CrystalDecisions.CrystalReports.Engine.TextObject objtxtRPTCenterNAME = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txtCenterReportName"];
                objtxtRPTCenterNAME.Text = "CS2 - Sub CSC Datewise Transaction Details";

                TextObject txtcsc = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txtcsc"];
                txtcsc.Text = ddlagent.SelectedItem.Text;

                TextObject txtfromdate = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txtfromdate"];
                txtfromdate.Text = txtDateF.Text;

                TextObject txttodate = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txttodate"];
                txttodate.Text = txtDateT.Text;


                TextObject txtObj = (TextObject)cryrpt.ReportDefinition.ReportObjects["txturl"];
                txtObj.Text = "Downloaded From : " + Request.Url.ToString();


                cryrpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "CSC Booking & Cancellation" + DateTime.Now.ToString() + DateTime.Now);
            }
        }
    }


   


    private void ExportGridToExcel()
    {
        if (grdtransactionDetails.Rows.Count <= 0)
        {
            Errormsg("No account details available");
            return;
        }

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=CSC Booking & Cancellation -" + DateTime.Now.ToString() + ".xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            // To Export all pages

            ShowTransactionDetails(ddlagent.SelectedValue, txtDateF.Text, txtDateT.Text);
            grdtransactionDetails.AllowPaging = false;

            grdtransactionDetails.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in grdtransactionDetails.HeaderRow.Cells)
            {
                cell.BackColor = grdtransactionDetails.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in grdtransactionDetails.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = grdtransactionDetails.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = grdtransactionDetails.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            grdtransactionDetails.RenderControl(hw);
            // style to format numbers to string
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
    #endregion


    #region "Event"
    protected void lbtnSearch_Click(object sender, EventArgs e)
    {

        if (ddlagent.SelectedValue == "0")
        {
            Errormsg("Select at least One Valid CSC");
            return;
        }

        DateTime dtFrom;
        DateTime dtTo;

        if (!DateTime.TryParseExact(txtDateF.Text.Trim(), "dd/MM/yyyy", null, DateTimeStyles.None, out dtFrom))
        {
            Errormsg("Select Valid From Date");
            return;
        }
        if (!DateTime.TryParseExact(txtDateT.Text.Trim(), "dd/MM/yyyy", null, DateTimeStyles.None, out dtTo))
        {
            Errormsg("Select Valid To Date");
            return;
        }

        if (dtTo < dtFrom)
        {
            Errormsg("Please Enter Valid From Date");
            return;
        }

        if ((dtTo - dtFrom).TotalDays > 30)
        {
            Errormsg("Please Note:- Reports can only be generated for 30 days at a time.");
            return;
        }
        ShowTransactionDetails(ddlagent.SelectedValue, txtDateF.Text,txtDateT.Text);
    }
    protected void grdtransactionDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdtransactionDetails.PageIndex = e.NewPageIndex;
        ShowTransactionDetails(ddlagent.SelectedValue, txtDateF.Text,txtDateT.Text);
    }
    protected void grdtransactionDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    Label lblTXN_AMOUNT = e.Row.FindControl("lblTXN_AMOUNT") as Label;
        //    LinkButton lbtnView = e.Row.FindControl("lbtnView") as LinkButton;
        //    DataRowView rowView = e.Row.DataItem as DataRowView;
        //    if (rowView["wallettxntypecode"].ToString() == "C")
        //    {
        //        lblTXN_AMOUNT.ForeColor = System.Drawing.Color.Green;
        //    }
        //    else
        //    {
        //        lblTXN_AMOUNT.ForeColor = System.Drawing.Color.Red;
        //    }
        //    if (rowView["wallettxntypecode"].ToString() == "T")
        //    {
        //        lbtnView.Visible = false;
        //    }
        //}
    }
    protected void grdtransactionDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //if (e.CommandName == "View")
        //{
        //    string tkt;
        //    GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
        //    string WALLET_TXN_TYPE_CODE = grdtransactionDetails.DataKeys[row.RowIndex]["wallettxntypecode"].ToString();
        //    tkt = grdtransactionDetails.DataKeys[row.RowIndex]["txnrefno"].ToString();
        //    if (WALLET_TXN_TYPE_CODE == "B" || WALLET_TXN_TYPE_CODE == "C")
        //    {
        //        Session["_ticketNo"] = tkt;
        //        mpTicket.Show();
        //    }
        //    if (WALLET_TXN_TYPE_CODE == "P")
        //    {
        //        Session["_RefNo"] = tkt;
        //        mpBusPass.Show();
        //    }
        //    if (WALLET_TXN_TYPE_CODE == "T")
        //    {
        //        Session["_RefNo"] = tkt;
        //        mpagentwallet.Show();
        //    }
        //}
    }
    protected void lbtndownload_Click(object sender, EventArgs e)
    {
        LoadReport();
    }


 


    protected void lbtnexcel_Click(object sender, EventArgs e)
    {
        ExportGridToExcel();
    }

    #endregion
}