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

public partial class Auth_MainCscAccount : System.Web.UI.Page
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    ReportDocument cryrpt = new ReportDocument();
    string rpt = System.Web.HttpContext.Current.Server.MapPath("Reports/rpt_CS1.rpt");
    string current_date = DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("yyyy");


    string current_date30 = DateTime.Now.AddDays(-30).ToString("dd") + "/" + DateTime.Now.AddDays(-30).ToString("MM") + "/" + DateTime.Now.AddDays(-30).ToString("yyyy");
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["moduleName"] = "Common Service Centre Account";

        if (!IsPostBack)
        {
            lbtndownload.Visible = false;
            txtfromdate.Text = current_date30;
            txttodate.Text = current_date;
            CSCpassbook();
        }

    }

    #region "Methods"
    private void Errormsg(string message)
    {
        lblerrormsg.Text = message;
        mperror.Show();
    }
    private bool ValidValue()
    {
        try
        {
            DateTime dtFrom;
            DateTime dtTo;

            if (!DateTime.TryParseExact(txtfromdate.Text.Trim(), "dd/MM/yyyy", null, DateTimeStyles.None, out dtFrom))
            {
                Errormsg("Select Valid From Date");
                return false; // Return is not necessary; you can simply exit the method or continue processing.
            }
            if (!DateTime.TryParseExact(txttodate.Text.Trim(), "dd/MM/yyyy", null, DateTimeStyles.None, out dtTo))
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
    private void CSCpassbook()
    {
        try
        {
            grdmsg.Visible = true;
            grdagpassbook.Visible = false;
            lbtndownload.Visible = false;
            lbtnEXCEL.Visible = false;

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "csc.agentpassbook");
            MyCommand.Parameters.AddWithValue("sp_agentcode", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("sp_datefrom", txtfromdate.Text);
            MyCommand.Parameters.AddWithValue("sp_dateto", txttodate.Text);
            DataTable dt = new DataTable();
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {

                    grdagpassbook.DataSource = dt;
                    grdagpassbook.DataBind();
                    grdmsg.Visible = false;
                    grdagpassbook.Visible = true;
                    lbtndownload.Visible = true;
                    lbtnEXCEL.Visible = true;
                  
                }
                else
                {
                    grdmsg.Visible = true;
                    grdagpassbook.Visible = false;
                   
                }
            }
  
        }
        
        catch (Exception ex)
        {
            string dd = ex.Message;
          
        }
    }
    private void LoadTopup()
    {
        try
        {

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "csc.subcsctopup");
            MyCommand.Parameters.AddWithValue("sp_maincode", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("sp_date", Session["TRANSDATE"]);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                grvtopup.DataSource = dt;
                grvtopup.DataBind();
                MpTopup.Show();
            }
        }

        catch (Exception ex)
        {
            MpTopup.Hide();
        }
    }


    private void LoadReport()
    {
        try
        {


            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "csc.agentpassbook");
            MyCommand.Parameters.AddWithValue("sp_agentcode", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("sp_datefrom", txtfromdate.Text);
            MyCommand.Parameters.AddWithValue("sp_dateto", txttodate.Text);
            dt = bll.SelectAll(MyCommand);

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "csc.subcsctopup");
            MyCommand.Parameters.AddWithValue("sp_maincode", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("sp_date", "");
            DataTable dt1 = new DataTable();
            dt1 = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {

                cryrpt.Load(rpt);
                cryrpt.SetDataSource(dt);
                cryrpt.Subreports[0].SetDataSource(dt1);
                XmlDocument doc = new XmlDocument();
                doc.Load(Server.MapPath("../CommonData.xml"));
                XmlNodeList deptname = doc.GetElementsByTagName("dept_Name_en");
                CrystalDecisions.CrystalReports.Engine.TextObject objtxtorgname = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txtorgname"];
                objtxtorgname.Text = deptname.Item(0).InnerXml;

                CrystalDecisions.CrystalReports.Engine.TextObject objtxtRPTNUMBER = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txtreportnumber"];
                objtxtRPTNUMBER.Text = "10.1";

                CrystalDecisions.CrystalReports.Engine.TextObject objtxtRPTNAME = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txtreportname"];
                objtxtRPTNAME.Text = "CSC Report";

                CrystalDecisions.CrystalReports.Engine.TextObject objtxtRPTCenterNAME = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txtCenterReportName"];
                objtxtRPTCenterNAME.Text = "CS1 - Main CSC Account Details";

                CrystalDecisions.CrystalReports.Engine.TextObject objtxtRPTFromDate = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txtfromdate"];
                objtxtRPTFromDate.Text = txtfromdate.Text;

                CrystalDecisions.CrystalReports.Engine.TextObject objtxtRPTToDate = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txttodate"];
                objtxtRPTToDate.Text = txttodate.Text;

                CrystalDecisions.CrystalReports.Engine.TextObject objtxtcsc = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txtcsc"];
                objtxtcsc.Text = Session["_UserCode"].ToString();


                TextObject txtObj = (TextObject)cryrpt.ReportDefinition.ReportObjects["txturl"];
                txtObj.Text = "Downloaded From : " + Request.Url.ToString();


                cryrpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "Account" + DateTime.Now.ToString() + DateTime.Now);


            }
        }
        catch (Exception ex)
        {
           
        }

    }


    private void ExportGridToExcel()
    {
        try
        {

            if (grdagpassbook.Rows.Count <= 0)
            {
                Errormsg("No Record");
                return;
            }

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Csc Account-" + DateTime.Now.ToString() + ".xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                grdagpassbook.AllowPaging = false;
                CSCpassbook();
               

                grdagpassbook.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in grdagpassbook.HeaderRow.Cells)
                {
                    cell.BackColor = grdagpassbook.HeaderStyle.BackColor;
                }

                foreach (GridViewRow row in grdagpassbook.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = grdagpassbook.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = grdagpassbook.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                grdagpassbook.RenderControl(hw);
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
    protected void grdagpassbook_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdagpassbook.PageIndex = e.NewPageIndex;
        CSCpassbook();
    }
    protected void grdagpassbook_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "ViewAllocation")
        {
            Session["TRANSDATE"] = grdagpassbook.DataKeys[index]["tras_date"].ToString();
            LoadTopup();
        }
    }
    protected void lbtnSearch_Click(object sender, EventArgs e)
    {
        if (ValidValue() == false)
        {
            return;
        }
        CSCpassbook();
    }
    protected void lbtndownload_Click(object sender, EventArgs e)
    {
        LoadReport();
    }



    protected void grvtopup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvtopup.PageIndex = e.NewPageIndex;
        LoadTopup();
    }


    protected void lbtnEXCEL_Click(object sender, EventArgs e)
    {
       
        ExportGridToExcel();
    }
    #endregion

}