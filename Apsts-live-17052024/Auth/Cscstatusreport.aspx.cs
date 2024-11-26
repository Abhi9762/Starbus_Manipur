using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Npgsql;
using System;
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

public partial class Auth_Cscstatusreport : System.Web.UI.Page
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    ReportDocument cryrpt = new ReportDocument();
    string rpt = System.Web.HttpContext.Current.Server.MapPath("Reports/CscDetails.rpt");
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["moduleName"] = "Common Service Centre - CSC Status Wise Details";

        if (!IsPostBack)
        {
            if (Session["_Status"] != null && Session["_Status"].ToString() != "")
            {
                ddlcscstatus.SelectedValue = Session["_Status"].ToString();
            }
            ShowCSCDetails(ddlcscstatus.SelectedValue);
        }
    }

    #region "Method"

    private void Errormsg(string msg)
    {
        lblerrmsg.Text = msg;
        mpError.Show();
    }

    private void ShowCSCDetails(string status)
        {
            try
            {
                lbtnexcel.Visible = false;
                lbtndownload.Visible = false;
                grdmsg.Visible = true;
                grdcscDetails.Visible = false;
                pnlreport.Visible = false;

                MyCommand = new NpgsqlCommand();
                MyCommand.Parameters.Clear();
                MyCommand.Parameters.AddWithValue("@StoredProcedure", "csc.cscstatusdetails");
                MyCommand.Parameters.AddWithValue("p_mainagent", Session["_UserCode"].ToString());
                MyCommand.Parameters.AddWithValue("p_status", status);
                DataTable dt = new DataTable();
                dt = bll.SelectAll(MyCommand);
                if (dt.TableName == "Success")
                {
                    if (dt.Rows.Count > 0)
                    {

                        pnlreport.Visible = true;
                        lblsmry.Text = "Total Record <b> " + dt.Rows.Count.ToString() + " </b>";
                        grdcscDetails.DataSource = dt;
                        grdcscDetails.DataBind();
                        grdmsg.Visible = false;
                        grdcscDetails.Visible = true;
                        lbtndownload.Visible = true;
                        lbtnexcel.Visible = true;

                    }
                    else
                    {
                        grdmsg.Visible = true;
                        grdcscDetails.Visible = false;

                    }
                }

            }

            catch (Exception ex)
            {
                string dd = ex.Message;

            }
        }


    private void ExportGridToExcel()
    {
        if (grdcscDetails.Rows.Count <= 0)
        {
            Errormsg("No account details available");
            return;
        }

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=" + ddlcscstatus.SelectedItem.ToString() + " CSC -" + DateTime.Now.ToString() + ".xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            // To Export all pages
            grdcscDetails.AllowPaging = false;
            ShowCSCDetails(ddlcscstatus.SelectedValue);

            grdcscDetails.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in grdcscDetails.HeaderRow.Cells)
            {
                cell.BackColor = grdcscDetails.HeaderStyle.BackColor;
            }

            foreach (GridViewRow row in grdcscDetails.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = grdcscDetails.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = grdcscDetails.RowStyle.BackColor;
                    }

                    cell.CssClass = "textmode";
                }
            }

            grdcscDetails.RenderControl(hw);

            // Style to format numbers to string
            string style = "<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }


    private void LoadReport()
    {
        try
        {
            String status = ddlcscstatus.SelectedValue.ToString();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "csc.cscstatusdetails");
            MyCommand.Parameters.AddWithValue("p_mainagent", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("p_status", status);
            DataTable dt = new DataTable();
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    cryrpt.Load(rpt);
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
                    objtxtRPTCenterNAME.Text = "CS3 - CSC Detail";

                    CrystalDecisions.CrystalReports.Engine.TextObject objtxtRPTFromDate = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txtcscstatus"];
                    objtxtRPTFromDate.Text = ddlcscstatus.SelectedItem.Text.ToString() + " CSC Details";





                    TextObject txtObj = (TextObject)cryrpt.ReportDefinition.ReportObjects["txturl"];
                    txtObj.Text = "Downloaded From : " + Request.Url.ToString();


                    cryrpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "CSC Details" + DateTime.Now.ToString() + DateTime.Now);


                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    #endregion

    #region "Event"

    public override void VerifyRenderingInServerForm(Control control)
    {
        // Verifies that the control is rendered
    }


    

    protected void lbtnexcel_Click(object sender, EventArgs e)
    {
        ExportGridToExcel();
    }

    protected void lbtndownload_Click(object sender, EventArgs e)
    {
        LoadReport();
    }

    protected void grdcscDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void grdcscDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblcurrentStatus = e.Row.FindControl("lblcurrentStatus") as Label;
            DataRowView rowView = e.Row.DataItem as DataRowView;
            if (rowView["status_"].ToString() == "A")
            {
                lblcurrentStatus.CssClass = "text-success";
            }
            else if (rowView["status_"].ToString() == "D")
            {
                lblcurrentStatus.CssClass = "text-danger";
            }
        }
    }

    protected void grdcscDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdcscDetails.PageIndex = e.NewPageIndex;
        ShowCSCDetails(ddlcscstatus.SelectedValue);
    }

    protected void lbtnSearch_Click(object sender, EventArgs e)
    {
        ShowCSCDetails(ddlcscstatus.SelectedValue);
    }

    #endregion
}