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

public partial class Auth_SubCscCashregister: System.Web.UI.Page
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    ReportDocument cryrpt = new ReportDocument();
    string rptcscaccount = System.Web.HttpContext.Current.Server.MapPath("Reports/rpt_CS3.rpt");
    protected void Page_Load(object sender, EventArgs e)
    {

        Session["moduleName"] = "Sub CSC Dashboard" + " - (" + Session["_UserName"] + " " + Session["_UserCode"] + ")";

        if (!IsPostBack)
        {
            
            Session["DateF"] = txtDateF.Text;
            Session["DateT"] = txtDateT.Text;
            txtDateF.Text = DateTime.Now.Date.AddDays(-30).ToString("dd/MM/yyyy");
            txtDateT.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

            ShowTransactionDetails(Session["_UserCode"].ToString(), txtDateF.Text, txtDateT.Text);
        }

    }

    #region "Methods"
    
    private DataTable ShowTransactionDetails(string agcode, string fdate, string tdate)
    {
        try
        {
            lbtndownload.Visible = false;
            lbtnexcel.Visible = false;
            grdmsg.Visible = true;
            grdtransactionDetails.Visible = false;


            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "csc.subcscpassbook");
            MyCommand.Parameters.AddWithValue("@sp_agentcode", agcode);
            MyCommand.Parameters.AddWithValue("@sp_datefrom", fdate);
            MyCommand.Parameters.AddWithValue("@sp_dateto", tdate);
            MyCommand.Parameters.AddWithValue("@sp_maincsc", "0");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
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
            return null;
            // Handle the exception here
        }
    }
    private void Errormsg(string msg)
    {
        lblerrormsg.Text = msg;
        mperror.Show();
    }
    private bool validvalue()
    {
        try
        {
            DateTime dtFrom;
            DateTime dtTo;

            if (!DateTime.TryParseExact(txtDateF.Text.Trim(), "dd/MM/yyyy", null, DateTimeStyles.None, out dtFrom))
            {
                Errormsg("Select Valid From Date");
                return false; // Return is not necessary; you can simply exit the method or continue processing.
            }
            if (!DateTime.TryParseExact(txtDateT.Text.Trim(), "dd/MM/yyyy", null, DateTimeStyles.None, out dtTo))
            {
                Errormsg("Select Valid To Date");
                return false;
            }

            if (dtTo < dtFrom)
            {
                Errormsg("Please Enter Valid From Date");
                return false;
            }
            if ((dtTo - dtFrom).TotalDays > 30)
            {
                Errormsg("Please Note:- Reports can only be generated for 30 days at a time.");
                return false;
            }

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }



    private void ExportGridToExcel()
    {
        try
        {


            if (grdtransactionDetails.Rows.Count <= 0)
            {
                Errormsg("No Record");
                return;
            }

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Sub Csc Account Details-" + DateTime.Now.ToString() + ".xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                grdtransactionDetails.AllowPaging = false;
                DataTable dt = ShowTransactionDetails(Session["_UserCode"].ToString(), txtDateF.Text, txtDateT.Text);
                if (dt.Rows.Count > 0)
                {
                    grdtransactionDetails.DataSource = dt;
                    grdtransactionDetails.DataBind();
                }

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
                string style = "<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }

        catch (Exception ex)
        {
            _common.ErrorLog("rpt_P2.aspx-008", ex.Message.ToString());

        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    #endregion

    #region Event
    protected void grdtransactionDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdtransactionDetails.PageIndex = e.NewPageIndex;
        ShowTransactionDetails(Session["_UserCode"].ToString(), txtDateF.Text, txtDateT.Text);
    }
    protected void lbtnSearch_Click(object sender, EventArgs e)
    {
        if (!validvalue())
        {
            return;
        }
        ShowTransactionDetails(Session["_UserCode"].ToString(), txtDateF.Text, txtDateT.Text);
    }
    protected void lbtndownload_Click(object sender, EventArgs e)
    {
        LoadReport();
    }

    private void LoadReport()
    {

        try
        {
            string agcode = Session["_UserCode"].ToString();
            string fdate = txtDateF.Text.ToString();
            string tdate = txtDateT.Text.ToString();

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "csc.subcscpassbook");
            MyCommand.Parameters.AddWithValue("@sp_agentcode", agcode);
            MyCommand.Parameters.AddWithValue("@sp_datefrom", fdate);
            MyCommand.Parameters.AddWithValue("@sp_dateto", tdate);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    cryrpt.Load(rptcscaccount);
                    cryrpt.SetDataSource(dt);
                    XmlDocument doc = new XmlDocument();
                    doc.Load(Server.MapPath("../CommonData.xml"));
                    XmlNodeList deptname = doc.GetElementsByTagName("dept_Name_en");
                    CrystalDecisions.CrystalReports.Engine.TextObject objtxtorgname = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txtorgname"];
                    objtxtorgname.Text = deptname.Item(0).InnerXml;

                    CrystalDecisions.CrystalReports.Engine.TextObject objtxtRPTNUMBER = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txtreportnumber"];
                    objtxtRPTNUMBER.Text = "10.3";

                    CrystalDecisions.CrystalReports.Engine.TextObject objtxtRPTNAME = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txtreportname"];
                    objtxtRPTNAME.Text = "CSC Report";

                    CrystalDecisions.CrystalReports.Engine.TextObject objtxtRPTCenterNAME = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txtCenterReportName"];
                    objtxtRPTCenterNAME.Text = "CS3 - Sub CSC Account Detail";

                    TextObject txtcsc = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txtcsc"];
                    txtcsc.Text = Session["_UserCode"].ToString();

                    TextObject txtfdate = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txtfdate"];
                    txtfdate.Text = fdate;

                    TextObject txttdate = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txttdate"];
                    txttdate.Text = tdate;


                    TextObject txtObj = (TextObject)cryrpt.ReportDefinition.ReportObjects["txturl"];
                    txtObj.Text = "Downloaded From : " + Request.Url.ToString();


                    cryrpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "CSCAccount" + DateTime.Now.ToString() + DateTime.Now);





                }

            }
        }
        catch (Exception ex)
        {

        }
    }
  


    protected void lbtnexcel_Click(object sender, EventArgs e)
    {
        ExportGridToExcel();
    }

    #endregion
}