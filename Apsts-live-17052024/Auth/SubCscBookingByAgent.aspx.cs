using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class Auth_SubCscBookingByAgent : System.Web.UI.Page
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
          
            txtDateF.Text = DateTime.Now.AddDays(-30).Date.ToString("dd/MM/yyyy");
            txtDateT.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            ShowTransactionDetails(Session["_UserCode"].ToString(), txtDateF.Text);
        }
    }
    #region "Methods"


    private void Errormsg(string message)
    {
        lblerrmsg.Text = message;
        mpError.Show();
    }

    public DataTable ShowTransactionDetails(string p_csccode, string p_date)
    {
        try
        {
            lbtndownload.Visible = false;
            lbtnexcel.Visible = false;
            grdmsg.Visible = true;
            grdtransactionDetails.Visible = false;


            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "csc.subcscregister");
            MyCommand.Parameters.AddWithValue("@p_maincsc", Session["_CSCMainAg"].ToString());
            MyCommand.Parameters.AddWithValue("p_agentcode", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("p_fdate", txtDateF.Text);
            MyCommand.Parameters.AddWithValue("p_tdate", txtDateT.Text);
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
            // Handle the exception here
            return null;
        }
    }


    private void LoadReport()
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "csc.subcscregister");
        MyCommand.Parameters.AddWithValue("@p_maincsc", Session["_CSCMainAg"].ToString());
        MyCommand.Parameters.AddWithValue("p_agentcode", Session["_UserCode"].ToString());
        MyCommand.Parameters.AddWithValue("p_fdate", txtDateF.Text);
        MyCommand.Parameters.AddWithValue("p_tdate", txtDateT.Text);
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
                txtcsc.Text = Session["_UserCode"].ToString();

                //TextObject txtdatee = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txtdate"];
                //txtdatee.Text = txtDateF.Text;


                TextObject txtObj = (TextObject)cryrpt.ReportDefinition.ReportObjects["txturl"];
                txtObj.Text = "Downloaded From : " + Request.Url.ToString();


                cryrpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "Transactiontype" + DateTime.Now.ToString() + DateTime.Now);
            }
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
            Response.AddHeader("content-disposition", "attachment;filename=Sub Csc Transaction Details-" + DateTime.Now.ToString() + ".xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                grdtransactionDetails.AllowPaging = false;
                DataTable dt = ShowTransactionDetails(Session["_UserCode"].ToString(), txtDateF.Text);
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


    #region "Event"
    protected void lbtnSearch_Click(object sender, EventArgs e)
    {
        // ShowTransactionCount(ddlagent.SelectedValue, txtDateF.Text);
        ShowTransactionDetails(Session["_UserCode"].ToString(), txtDateF.Text);
    }
    protected void grdtransactionDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdtransactionDetails.PageIndex = e.NewPageIndex;
        ShowTransactionDetails(Session["_UserCode"].ToString(), txtDateF.Text);
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