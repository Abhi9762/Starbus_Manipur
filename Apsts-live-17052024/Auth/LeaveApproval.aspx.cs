using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_LeaveApproval : System.Web.UI.Page
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    int draft = 0;
    int approved = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["_moduleName"] = "Leave Approval";
        if (!IsPostBack)
        {
            Random randomclass = new Random();
            Session["rndNoCheck"] = randomclass.Next().ToString();
            hidtoken.Value = Session["rndNoCheck"].ToString();

            loadEmpDutyType(ddldutytype);
            loadEmpType(ddlEmptype);
            if(Session["_RoleCode"].ToString() == "1")
            {
                Session["_OfficeId"] = "1000000000";
            }
            LoadAllEmployees();
        }
        Test();
    }


    #region "Popup"

    private void Errormsg(string msg)
    {
        string popup = _popup.modalPopupSmall("D", "Please Check", msg, "Close");
        Response.Write(popup);
    }
    public void ErrorMessage(string message)
    {
        lblerrormsg.Text = message;
        mperror.Show();
    }
    public void SuccessMessage(string message)
    {
        lblsuccessmsg.Text = message;
        mpsuccess.Show();
    }
    public void ConfirmMessage(string message)
    {
        lblConfirmation.Text = message;
        mpConfirmation.Show();
    }

    #endregion

    #region "Method"
    private void loadEmpType(DropDownList ddlemptype)//M26
    {
        try
        {
            ddlemptype.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_emptype");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlemptype.DataSource = dt;
                    ddlemptype.DataTextField = "typename";
                    ddlemptype.DataValueField = "typeid";
                    ddlemptype.DataBind();
                }
            }
            ddlemptype.Items.Insert(0, "All");
            ddlemptype.Items[0].Value = "0";
            ddlemptype.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlemptype.Items.Insert(0, "SELECT");
            ddlemptype.Items[0].Value = "0";
            ddlemptype.SelectedIndex = 0;
            Errormsg("There is some error.");
            _common.ErrorLog("LeaveApproval.aspx-01", ex.Message.ToString());
        }
    }
    private void loadEmpDutyType(DropDownList ddldutyType)//M27
    {
        try
        {
            ddldutyType.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_emp_dutytype");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddldutyType.DataSource = dt;
                    ddldutyType.DataTextField = "typename";
                    ddldutyType.DataValueField = "typeid";
                    ddldutyType.DataBind();
                }
            }
            ddldutyType.Items.Insert(0, "All");
            ddldutyType.Items[0].Value = "0";
            ddldutyType.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddldutyType.Items.Insert(0, "SELECT");
            ddldutyType.Items[0].Value = "0";
            ddldutyType.SelectedIndex = 0;
            Errormsg("There is some error.");
            _common.ErrorLog("LeaveApproval.aspx-02", ex.Message.ToString());
        }
    }
    private void Test()
    {
        if (grvEmployees.Rows.Count > 0)
        {
            grvEmployees.UseAccessibleHeader = true;
            grvEmployees.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
    public void LoadAllEmployees()
    {
        try
        {
            grvEmployees.Visible = false;
            pnlNoRecord.Visible = true;
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_approveleave");
            MyCommand.Parameters.AddWithValue("p_ofclvlid",Convert.ToInt32(Session["_OfficeLevel"].ToString()));
            MyCommand.Parameters.AddWithValue("p_officeid", Session["_OfficeId"].ToString());
            MyCommand.Parameters.AddWithValue("p_emptype", ddlEmptype.SelectedValue);
            MyCommand.Parameters.AddWithValue("p_dutytype", ddldutytype.SelectedValue);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    grvEmployees.DataSource = dt;
                    grvEmployees.DataBind();

                    //lbtnTotalEmployee.Text = dt.Rows.Count.ToString();
                    lbtnLeaveApplied.Text = (draft + approved).ToString();

                    lbtnPendingForApproval.Text = draft.ToString();
                    lbtnApprovedLeave.Text = approved.ToString();
                    LoadEmployeesSummary(draft + approved, draft, approved);

                    grvEmployees.Visible = true;
                    pnlNoRecord.Visible = false;
                    Test();
                }
            }
        }

        catch (Exception ex)
        {
            _common.ErrorLog("LeaveApproval.aspx-03", ex.Message.ToString());

        }
    }
    private void LoadEmployeesSummary(int tottal, int draft, int approved)
    {
        DataTable dtTimeTable = new DataTable("timeTable");
        dtTimeTable.Columns.Add("statusType", typeof(string));
        dtTimeTable.Columns.Add("count", typeof(string));

        dtTimeTable.Rows.Add("Leave Applied", tottal);
        dtTimeTable.Rows.Add("Pending For Approval", draft);
        dtTimeTable.Rows.Add("Approved Leave", approved);

        string chart = "<canvas id='bookpiechartmode' width='100%' height='90' ></canvas><script>";

        chart += "new Chart(document.getElementById('bookpiechartmode'), { type: 'pie', data: {labels: [";

        for (int i = 0; i < dtTimeTable.Rows.Count; i++)
        {
            chart += "'" + dtTimeTable.Rows[i]["statusType"].ToString() + "',";
        }

        chart = chart.Substring(0, chart.Length - 1);
        chart += "],datasets: [{ data: [";

        string value = "";

        for (int i = 0; i < dtTimeTable.Rows.Count; i++)
        {
            value += dtTimeTable.Rows[i]["count"].ToString() + ",";
        }

        value = value.Substring(0, value.Length - 1);
        chart += value;
        chart += "],backgroundColor: ['#f3545d', '#11af4b','#fdaf4b',  '#1d7af3'],borderWidth: 0}";
        chart += "]},options : {responsive: true, maintainAspectRatio: false, legend: {position: 'bottom',labels : {fontColor:'rgb(0, 0, 0)', fontSize: 11, usePointStyle : true, padding: 8 }},";
        chart += "pieceLabel: {render: 'percentage',fontColor: 'white',fontSize: 11,},tooltips: {bodySpacing: 4,mode: 'nearest', intersect: 0, position: 'nearest', xPadding: 10, yPadding: 10, caretPadding: 10 }, layout: { padding: {left: 0, right: 0, top: 10, bottom: 0 } } } });";
        chart += "</script>";
        ImgpieChartBookingModeNOdata.Visible = false;
        ltpieChartBookingMode.Text = chart;
    }
    private void ApproveLeave()
    {
        try
        {
            //string Mresult;
            string UPDATEDBY = Session["_UserCode"].ToString();
            string IPAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(IPAddress))
            {
                IPAddress = Request.ServerVariables["REMOTE_ADDR"];
            }
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_approvedleave");
            MyCommand.Parameters.AddWithValue("spaction", Session["action"].ToString());
            MyCommand.Parameters.AddWithValue("spleaverefno", Session["_REFNO"].ToString());
            MyCommand.Parameters.AddWithValue("p_empcode", Session["_EMPID"].ToString());
            MyCommand.Parameters.AddWithValue("p_updatedby", UPDATEDBY);
            MyCommand.Parameters.AddWithValue("p_ipaddress", IPAddress);
            MyCommand.Parameters.AddWithValue("p_successyn", "");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {

                    LoadAllEmployees();
                    SuccessMessage("Leave has successfully Approved");
                }
                else
                {
                    ErrorMessage("Leave cannot be marked for selected dates as the Employee is already on a trip");
                }
            }
            else
            {
                ErrorMessage("Error occurred while Updation.");
                return;
            }
        }
        catch (Exception ex)
        {
            // Handle the exception as needed
        }
    }
    #endregion


    #region "Event"
    protected void grvEmployees_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "ApproveLeave")
        {
            Session["_EMPID"] = grvEmployees.DataKeys[index].Values["empcode_"];
            Session["_REFNO"] = grvEmployees.DataKeys[index].Values["leaverefno_"];
            lblEmpCode.Text = grvEmployees.DataKeys[index].Values["empcode_"].ToString();
            lblEmpName.Text = grvEmployees.DataKeys[index].Values["empname_"].ToString();
            lblDesigantion.Text = grvEmployees.DataKeys[index].Values["designation_name_"].ToString();
            lblLeaveStartDate.Text = Convert.ToDateTime(grvEmployees.DataKeys[index].Values["leavestartdate_"]).Date.ToString("dd/MM/yyyy");
            lblLeaveEndDate.Text = Convert.ToDateTime(grvEmployees.DataKeys[index].Values["leaveenddate"]).Date.ToString("dd/MM/yyyy");
            lblRemark.Text = grvEmployees.DataKeys[index].Values["reason_"].ToString();

            if (!Convert.IsDBNull(grvEmployees.DataKeys[index].Values["attachment_"]))
            {
                //int pdfindex = Array.IndexOf<byte>(grvEmployees.DataKeys[index].Values["attachment_"], 1);
                byte[] attachment = (byte[])grvEmployees.DataKeys[index].Values["attachment_"];
                int pdfindex = Array.IndexOf(attachment, (byte)1);

                if (pdfindex == -1)
                {
                    Session["pdf"] = null;
                    lbtnPdf.Visible = false;
                    lblpdf.Visible = true;
                }
                else
                {
                    Session["pdf"] = grvEmployees.DataKeys[index].Values["attachment_"];
                    lbtnPdf.Visible = true;
                    lbtnPdf.Text = "View Document"; // docfile.FileName
                    lblpdf.Visible = false;
                }
            }
            else
            {
                Session["pdf"] = null;
                lbtnPdf.Visible = false;
                lblpdf.Visible = true;
            }

            lblLeavetype.Text = grvEmployees.DataKeys[index].Values["leavetypeid_"].ToString();
            lbtnApproveLeave.Visible = true;
            lbtnRejectLeave.Visible = false;
            trreson.Visible = false;
            mpApproveLeave.Show();
        }
        else if (e.CommandName == "RejectLeave")
        {
            Session["_EMPID"] = grvEmployees.DataKeys[index].Values["empcode_"];
            Session["_REFNO"] = grvEmployees.DataKeys[index].Values["leaverefno_"];
            lblEmpCode.Text = grvEmployees.DataKeys[index].Values["empcode_"].ToString();
            lblEmpName.Text = grvEmployees.DataKeys[index].Values["empname_"].ToString();
            lblDesigantion.Text = grvEmployees.DataKeys[index].Values["designation_name_"].ToString();
            lblLeaveStartDate.Text = Convert.ToDateTime(grvEmployees.DataKeys[index].Values["leavestartdate_"]).Date.ToString("dd/MM/yyyy");
            lblLeaveEndDate.Text = Convert.ToDateTime(grvEmployees.DataKeys[index].Values["leaveenddate"]).Date.ToString("dd/MM/yyyy");
            lblRemark.Text = grvEmployees.DataKeys[index].Values["reason_"].ToString();

           if (!Convert.IsDBNull(grvEmployees.DataKeys[index].Values["attachment_"]))
            {
                //int pdfindex = Array.IndexOf<byte>(grvEmployees.DataKeys[index].Values["attachment_"], 1);
                byte[] attachment = (byte[])grvEmployees.DataKeys[index].Values["attachment_"];
                int pdfindex = Array.IndexOf(attachment, (byte)1);
                if (pdfindex == -1)
                {
                    Session["pdf"] = null;
                    lbtnPdf.Visible = false;
                    lblpdf.Visible = true;
                }
                else
                {
                    Session["pdf"] = grvEmployees.DataKeys[index].Values["attachment_"];
                    lbtnPdf.Visible = true;
                    lbtnPdf.Text = "View Document"; // docfile.FileName
                    lblpdf.Visible = false;
                }
            }
            else
            {
                Session["pdf"] = null;
                lbtnPdf.Visible = false;
                lblpdf.Visible = true;
            }

            lblLeavetype.Text = grvEmployees.DataKeys[index].Values["leavetypeid_"].ToString();
            lbtnApproveLeave.Visible = false;
            lbtnRejectLeave.Visible = true;
            trreson.Visible = true;
            mpApproveLeave.Show();
        }
        else if (e.CommandName == "viewDashboard")
        {
            Session["empCode_chartDash"] = grvEmployees.DataKeys[index].Values["empcode_"];
            Session["empDesigName_chartDash"] = grvEmployees.DataKeys[index].Values["designation_name_"];
            Session["empName_chartDash"] = grvEmployees.DataKeys[index].Values["empname_"];
            Response.Redirect("../DutyAllocation/empStatusCal.aspx");
        }

    }
    protected void grvEmployees_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbtnApprove = (LinkButton)e.Row.FindControl("lbtnApprove");
            LinkButton lbtnReject = (LinkButton)e.Row.FindControl("lbtnReject");
            Label lblStatus = (Label)e.Row.FindControl("lblStatus");
            DataRowView rowView = (DataRowView)e.Row.DataItem;

            if (!Convert.IsDBNull(rowView["draft_"]))
            {
                draft = draft + 1;
                lblStatus.Text = rowView["draft_"].ToString();
                lbtnApprove.Visible = true;
                lbtnReject.Visible = true;
            }

            if (!Convert.IsDBNull(rowView["approved_"]))
            {
                approved = approved + 1;
                lblStatus.Text = rowView["approved_"].ToString();
                lbtnApprove.Visible = false;
                lbtnReject.Visible = false;
            }
        }

    }
    protected void lbtnPdf_Click(object sender, EventArgs e)
    {
        try
        {
            string docType = "Leave Document";
            byte[] fileInvoice = (byte[])Session["pdf"];
            string base64String = Convert.ToBase64String(fileInvoice, 0, fileInvoice.Length);

            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + docType + ".pdf");
            Response.BinaryWrite(fileInvoice);
            Response.End();
        }
        catch (Exception ex)
        {
            
        }

    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        if(Session["action"] == "L")
        {
            ApproveLeave();
        }
        if (Session["action"] == "C")
        {
            ApproveLeave();
        }
    }
    protected void lbtnRejectLeave_Click(object sender, EventArgs e)
    {
        Session["action"] = "C";
        ConfirmMessage("Do you want Reject Leave ?");
    }
    protected void lbtnApproveLeave_Click(object sender, EventArgs e)
    {
        Session["action"] = "L";
        ConfirmMessage("Do you want Approve Leave ?");
    }
    protected void ddlEmptype_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadAllEmployees();
    }
    protected void ddldutytype_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadAllEmployees();
    }
    protected void lbtnSearch_Click(object sender, EventArgs e)
    {
        LoadAllEmployees();
    }
    protected void lbtnResetFilter_Click(object sender, EventArgs e)
    {
        loadEmpType(ddlEmptype);
        loadEmpDutyType(ddldutytype);
        LoadAllEmployees();
    }

    #endregion
}