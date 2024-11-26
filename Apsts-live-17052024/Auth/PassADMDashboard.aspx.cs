using System;
using Npgsql;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Globalization;
using CrystalDecisions.CrystalReports.Engine;
using System.Xml;
using CrystalDecisions.Shared;

public partial class Auth_PassADMDashboard : System.Web.UI.Page
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    ReportDocument cryrpt = new ReportDocument();
    string Pass_report1 = System.Web.HttpContext.Current.Server.MapPath("~/Auth/Reports/rptmstdetail.rpt");
    string Pass_Details = System.Web.HttpContext.Current.Server.MapPath("~/Auth/Reports/Pass_Transaction_Details.rpt");

    //Dim Pass_report2 As String = Server.MapPath("../CitizenAuth/CntrRpt/rptmstdetailall.rpt")

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtfromdate.Text = DateTime.Now.AddDays(-15).ToString("dd/MM/yyyy");

            txttodate.Text = DateTime.Now.AddDays(0).ToString("dd/MM/yyyy");
            txtapplydate.Text = DateTime.Now.AddDays(0).ToString("dd/MM/yyyy");
            txtissunacedate.Text = DateTime.Now.AddDays(0).ToString("dd/MM/yyyy");
           
            LoadPassRequestSummary();
            BusPassTypeList(ddlPassType);
            BusPassTypeList(ddlPendingPasstype);
            BusPassTypeList(ddlsearchpasstype);
            CategoriesTypeList();
            LoadPendingPassRequest();
            Session["currtranrefno"] = "";
        }
        LoadDataTable();
    }


    #region "Method"
    private void LoadPassRequestSummary()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            DataTable dt;
            string transdate = DateTime.Now.Date.ToString("dd/MM/yyyy");
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "buspass.sp_pass_summary");
            MyCommand.Parameters.AddWithValue("@p_transdate", transdate);

            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {

                    // lblrequest.Text = dt.Rows[0]["p_total"].ToString;
                    lbltotalnew.Text = dt.Rows[0]["p_totalNew"].ToString();
                    lblpendingnew.Text = dt.Rows[0]["p_newpending"].ToString();
                    lblacceptNew.Text = dt.Rows[0]["p_newaccept"].ToString();
                    lblrejectNew.Text = dt.Rows[0]["p_newreject"].ToString();

                    lbltotalrenew.Text = dt.Rows[0]["p_totalrenew"].ToString();
                    lblpendingrenew.Text = dt.Rows[0]["p_renewpending"].ToString();
                    lblacceptRenew.Text = dt.Rows[0]["p_renewaccept"].ToString();
                    lblrejectRenew.Text = dt.Rows[0]["p_renewreject"].ToString();

                    lblintantissue.Text = dt.Rows[0]["p_instantissue"].ToString();
                    lblduplicatedwnload.Text = dt.Rows[0]["p_duplicatedownload"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            // Handle exception
        }
    }
    private void LoadPendingPassRequest()
    {
        try
        {
            dvPassRequest.Visible = true;
            gvPassRequest.Visible = false;
            lbltotalrequest.Text = "0";
            lblnewrequest.Text = "0";
            lblrenewrequest.Text = "0";

            string applyDate = txtapplydate.Text;
            string currentRefNo = txtRefNo.Text;


            MyCommand = new NpgsqlCommand();
            DataTable dt;
            string transdate = DateTime.Now.Date.ToString("dd/MM/yyyy");
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "buspass.pending_for_verification");
            MyCommand.Parameters.AddWithValue("@p_requesttype", ddlPendingRequesttype.SelectedValue);
            MyCommand.Parameters.AddWithValue("@p_passtype", ddlPendingPasstype.SelectedValue);
            MyCommand.Parameters.AddWithValue("@p_applydate", applyDate);
            MyCommand.Parameters.AddWithValue("@p_currentref", currentRefNo);

            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {

                    lbltotalrequest.Text = dt.Rows[0]["total_request"].ToString();
                    lblnewrequest.Text = dt.Rows[0]["new_request"].ToString();
                    lblrenewrequest.Text = dt.Rows[0]["renew_request"].ToString();

                    gvPassRequest.DataSource = dt;
                    gvPassRequest.DataBind();
                    dvPassRequest.Visible = false;
                    gvPassRequest.Visible = true;
                    LoadDataTable();
                }
            }
        }
        catch (Exception ex)
        {
            // Handle exception
        }
    }
    public void LoadDataTable()
    {
        if (gvPassRequest.Rows.Count > 0)
        {
            gvPassRequest.UseAccessibleHeader = true;
            gvPassRequest.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        if (grdsearchpass.Rows.Count > 0)
        {
            grdsearchpass.UseAccessibleHeader = true;
            grdsearchpass.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
    private void ErrorMessage(string msg)
    {
        lblerrormsg.Text = msg;
        mperror.Show();
    }
    private void BusPassTypeList(DropDownList ddllist)
    {
        try
        {
            ddllist.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "bus_pass_type_list_bustypeid");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {


                    ddllist.DataSource = dt;
                    ddllist.DataTextField = "buspasstypename";
                    ddllist.DataValueField = "buspasstypeid";
                    ddllist.DataBind();
                }
            }

            ddllist.Items.Insert(0, "Select");
            ddllist.Items[0].Value = "0";
            ddllist.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddllist.Items.Insert(0, "Select");
            ddllist.Items[0].Value = "0";
            ddllist.SelectedIndex = 0;
        }
    }
    private void CategoriesTypeList()
    {
        try
        {
            ddlPassCategory.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.bus_pass_category_list");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {


                    ddlPassCategory.DataSource = dt;
                    ddlPassCategory.DataTextField = "buspass_categoryname";
                    ddlPassCategory.DataValueField = "buspass_categoryid";
                    ddlPassCategory.DataBind();
                }
            }

            ddlPassCategory.Items.Insert(0, "Select");
            ddlPassCategory.Items[0].Value = "0";
            ddlPassCategory.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlPassCategory.Items.Insert(0, "Select");
            ddlPassCategory.Items[0].Value = "0";
            ddlPassCategory.SelectedIndex = 0;
        }
    }

    private void LoadSearchPass()
    {
        try
        {
            DataTable dt = DtPass();
            if (dt.Rows.Count > 0)
            {
                grdsearchpass.DataSource = dt;
                grdsearchpass.DataBind();
                grdsearchpass.Visible = true;
                dvsearchpass.Visible = false;
                lbtnsearchDownload.Visible = true;
                LoadDataTable();
            }
            else
            {
                grdsearchpass.Visible = false;
                dvsearchpass.Visible = true;
                lbtnsearchDownload.Visible = false;
            }
        }
        catch (Exception ex)
        {
            return;
        }
    }

    private DataTable DtPass()
    {
        try
        {
            string applydate = txtissunacedate.Text;
            string issuanceType = ddlIssuanceType.SelectedValue;
            string passType = ddlsearchpasstype.SelectedValue;
            string currntRefno = txtapprovalrefno.Text.ToString();
            string refno = "";
            string value = "";

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "buspass.get_search_pass");
            MyCommand.Parameters.AddWithValue("@p_applydate", applydate);
            MyCommand.Parameters.AddWithValue("@p_issuancetype", issuanceType);
            MyCommand.Parameters.AddWithValue("@p_passtype", passType);
            MyCommand.Parameters.AddWithValue("@p_currntref", currntRefno);
            dt = bll.SelectAll(MyCommand);
            return dt;

        }
        catch (Exception ex)
        {
            return null;
        }
    }

    private bool ValidValue()
    {
        try
        {
            DateTime fromDate;
            DateTime toDate;
            if (!_validation.IsValidString(txtfromdate.Text.Trim(), 8, 10))
            {
                ErrorMessage("Please Enter correct From Date");
                return false;
            }


            if (!_validation.IsValidString(txttodate.Text.Trim(), 8, 10))
            {
                ErrorMessage("Please Enter correct To Date");
                return false;
            }


            if (DateTime.ParseExact(txtfromdate.Text.Trim(), "dd/MM/yyyy", null) > DateTime.Now.Date)
            {
                ErrorMessage("Please enter from date which is less or equal to the current date");
                return false;
            }

            DateTime dtFrom = DateTime.ParseExact(txtfromdate.Text.Trim(), "dd/MM/yyyy", null);
            DateTime dtTo = DateTime.ParseExact(txttodate.Text.Trim(), "dd/MM/yyyy", null);

            if ((dtTo - dtFrom).Days > 15)
            {
                ErrorMessage("At a time, Account of only 15 days can be generated");
                return false;
            }

            if (dtTo < dtFrom)
            {
                ErrorMessage("Please Enter Valid Dates");
                return false;
            }
        }
        catch (Exception ex)
        {
            ErrorMessage(ex.Message);
            return false;
        }
        return true;
    }


    private void LoadPassReport1()
    {
        try
        {

            string type = "A";

            int passCategoryId = Convert.ToInt32(ddlPassCategory.SelectedValue);
            int passTypeId = Convert.ToInt32(ddlPassType.SelectedValue);
            string status = ddlstatus.SelectedValue.ToString();
            string fromDate = txtfromdate.Text;
            string toDate = txttodate.Text;
            type = ddlrequesttype.SelectedValue.ToString();

            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "buspass.get_pass_reports1");
            MyCommand.Parameters.AddWithValue("p_frmdate", fromDate);
            MyCommand.Parameters.AddWithValue("p_todate", toDate);
            MyCommand.Parameters.AddWithValue("p_type", type);
            MyCommand.Parameters.AddWithValue("p_passtype", passCategoryId);
            MyCommand.Parameters.AddWithValue("p_psngrtype", passTypeId);
            MyCommand.Parameters.AddWithValue("p_status", status);

            DataTable dt = new DataTable();
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                cryrpt.Load(Pass_report1);
                cryrpt.SetDataSource(dt);
                XmlDocument doc = new XmlDocument();
                doc.Load(Server.MapPath("../CommonData.xml"));
                XmlNodeList title = doc.GetElementsByTagName("dept_Name_en");
                CrystalDecisions.CrystalReports.Engine.TextObject objtxtorgname = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txtorgname"];
                objtxtorgname.Text = title.Item(0).InnerXml;

                CrystalDecisions.CrystalReports.Engine.TextObject objtxtRPTNUMBER = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txtreportnumber"];
                objtxtRPTNUMBER.Text = "1.1";
                CrystalDecisions.CrystalReports.Engine.TextObject objtxtRPTNAME = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txtreportname"];
                objtxtRPTNAME.Text = "Pass Report ";

                CrystalDecisions.CrystalReports.Engine.TextObject objtxtApplyType = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txtApplyType"];
                objtxtApplyType.Text = "Pass Issue Type :- " + ddlrequesttype.SelectedItem.ToString();

                CrystalDecisions.CrystalReports.Engine.TextObject objtxtapplydate = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txtapplydate"];
                objtxtapplydate.Text = "Apply Date :- " + fromDate + "-" + toDate;

                CrystalDecisions.CrystalReports.Engine.TextObject objtxtpasstype = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txtpasstype"];
                objtxtpasstype.Text = "Pass Category :- " + ddlPassCategory.SelectedItem.ToString() + ", Pass Type :- " + ddlPassType.SelectedItem.ToString();



                CrystalDecisions.CrystalReports.Engine.TextObject objtxtRPTURL = (TextObject)cryrpt.ReportDefinition.Sections[4].ReportObjects["txturl"];
                objtxtRPTURL.Text = "Downloaded From -" + Request.Url.AbsoluteUri;
                cryrpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Context.Response, true, "PassReport-" + DateTime.Now.ToString() + DateTime.Now); ;
            }
            else
                ErrorMessage("Sorry, No Record Available for selected parameter");
        }
        catch (Exception ex)
        {
            ErrorMessage("Error, No Record Available for selected perameter");
        }
    }


    #endregion
    #region "Event"
    protected void lbtnapplytxn_Click(object sender, EventArgs e)
    {
        LoadPendingPassRequest();
    }
    protected void lbtnresetapplytxn_Click(object sender, EventArgs e)
    {
        ddlPendingRequesttype.SelectedValue = "N";
        ddlPendingPasstype.SelectedValue = "0";
        txtapplydate.Text = "";
        txtRefNo.Text = "";
        LoadPendingPassRequest();
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        grdsearchpass.Visible = false;
        dvsearchpass.Visible = true;

        if (txtapplydate.Text.Length <= 0 && txtapprovalrefno.Text.Length <= 0)
        {
            ErrorMessage("Select At least  Pass Apply Date or Enter Refrence No./Name/Mobile/Email");
            return;
        }

        LoadSearchPass();

    }
    protected void lbtnsearchDownload_Click(object sender, EventArgs e)
    {
        DataTable dt = DtPass();

        if (dt.Rows.Count > 0)
        {
            ReportDocument cryRpt = new ReportDocument();
            cryRpt.Load(Pass_Details);
            cryRpt.SetDataSource(dt);
            cryRpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Context.Response, true, "Bus Depot " + DateTime.Now.ToString());
        }
        else
        {

        }
    }
    protected void grdsearchpass_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "PrintPass")
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            string p_PASSNUMBER = grdsearchpass.DataKeys[row.RowIndex]["pass_no"].ToString();
            Session["Passno"] = p_PASSNUMBER;
            mpPass.Show();
            // openSubDetailsWindow("Bus_Pass.aspx");
        }

        if (e.CommandName == "PrintReceipt")
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            string p_CURRTRANREFNO = grdsearchpass.DataKeys[row.RowIndex]["currtranref_no"].ToString();
            Session["currtranrefno"] = p_CURRTRANREFNO;
            mppassreceipt.Show();
            // openSubDetailsWindow("Pass_reciept.aspx");
        }

        if (e.CommandName == "View")
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            string p_CURRTRANREFNO = grdsearchpass.DataKeys[row.RowIndex]["currtranref_no"].ToString();
            Session["_RefNo"] = p_CURRTRANREFNO;
            mpPassInfo.Show();
        }

    }
    protected void lbtndownload_Click(object sender, EventArgs e)
    {
        if (!ValidValue())
        {
            return;
        }

        LoadPassReport1();

    }
    protected void lbtnresetsearch_Click(object sender, EventArgs e)
    {
        txtapplydate.Text = "";
        ddlIssuanceType.SelectedValue = "0";
        ddlsearchpasstype.SelectedValue = "0";
        txtapprovalrefno.Text = "";
        grdsearchpass.Visible = false;
        dvsearchpass.Visible = true;
        lbtnsearchDownload.Visible = false;
    }
    protected void gvPassRequest_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        GridViewRow row = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer);
        int index = row.RowIndex;

        if (e.CommandName == "View")
        {
            Session["currtranrefno"] = gvPassRequest.DataKeys[index]["currtranrefno"].ToString();
            Response.Write(Session["currtranrefno"]);
            Response.Redirect("PassADMVerification.aspx", false);
        }

        if (e.CommandName == "PrintReceipt")
        {
            string p_CURRTRANREFNO = gvPassRequest.DataKeys[index]["currtranrefno"].ToString();
            Session["currtranrefno"] = p_CURRTRANREFNO;
            mppassreceipt.Show();
        }
    }
    #endregion
}
