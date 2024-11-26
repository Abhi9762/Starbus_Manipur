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

public partial class Auth_AddLeave : System.Web.UI.Page
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
        Session["_moduleName"] = "Leave Marking";
        if (!IsPostBack)
        {
            Random randomclass = new Random();
            Session["rndNoCheck"] = randomclass.Next().ToString();
            hidtoken.Value = Session["rndNoCheck"].ToString();

            loadEmpDutyType(ddldutytype);
            loadEmpType(ddlEmptype);
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
            _common.ErrorLog("AddLeave.aspx-01", ex.Message.ToString());
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
            _common.ErrorLog("AddLeave.aspx-02", ex.Message.ToString());
        }
    }
    private void LoadLeaveTypes()
    {
        try
        {
            ddlLeaveType.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_leavetypes");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlLeaveType.DataSource = dt;
                    ddlLeaveType.DataTextField = "leave_typename";
                    ddlLeaveType.DataValueField = "leave_type";
                    ddlLeaveType.DataBind();
                }
            }
            ddlLeaveType.Items.Insert(0, "SELECT");
            ddlLeaveType.Items[0].Value = "0";
            ddlLeaveType.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlLeaveType.Items.Insert(0, "SELECT");
            ddlLeaveType.Items[0].Value = "0";
            ddlLeaveType.SelectedIndex = 0;
            Errormsg("There is some error.");
            _common.ErrorLog("AddLeave.aspx-03", ex.Message.ToString());
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
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_addleave");
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

                    lbtnTotalEmployee.Text = dt.Rows.Count.ToString();
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
            _common.ErrorLog("AddLeave.aspx-03", ex.Message.ToString());
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
    private bool IsValidValues()
    {
        try
        {
            int msgcnt = 0;
            string msg = "";

            if (ddlLeaveType.SelectedValue == "0")
            {
                msgcnt++;
                msg += msgcnt.ToString() + ". Select Leave Type<br/>";
            }
            DateTime parsedDate;
            if (txtFromdate.Text.Trim().Length > 0)
            {
                if (!DateTime.TryParseExact(txtFromdate.Text, "dd/MM/yyyy", null, DateTimeStyles.None, out parsedDate))
                {
                    msgcnt++;
                    msg += msgcnt.ToString() + ". Invalid Leave Start Date<br/>";
                }
                else
                {
                    DateTime fromDate = DateTime.ParseExact(txtFromdate.Text, "dd/MM/yyyy", null);
                    DateTime currDate = DateTime.Now.Date;

                    if (currDate > fromDate)
                    {
                        msgcnt++;
                        msg += msgcnt.ToString() + ". Check Leave Start Date should be greater than Today's Date.<br/>";
                    }
                }
            }
            else
            {
                msgcnt++;
                msg += msgcnt.ToString() + ". Select Leave Start Date.<br/>";
            }
            DateTime parsedDate1;
            if (txtToDate.Text.Trim().Length > 0)
            {
                if (!DateTime.TryParseExact(txtToDate.Text, "dd/MM/yyyy", null, DateTimeStyles.None, out parsedDate1))
                {
                    msgcnt++;
                    msg += msgcnt.ToString() + ". Invalid Leave End Date.<br/>";
                }
                else
                {
                    DateTime fromDate = DateTime.ParseExact(txtFromdate.Text, "dd/MM/yyyy", null);
                    DateTime toDate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", null);

                    if (toDate < fromDate)
                    {
                        msgcnt++;
                        msg += msgcnt.ToString() + ". Check Leave End Date should be Greater than Leave Start Date.<br/>";
                    }
                }
            }
            else
            {
                msgcnt++;
                msg += msgcnt.ToString() + ". Select Leave End Date.<br/>";
            }

            if (txtRemark.Text.Length > 0)
            {
                if (!_validation.IsValidString(txtRemark.Text, 1, txtRemark.MaxLength))
                {
                    msgcnt++;
                    msg += msgcnt.ToString() + ". Reason of Leave<br>";
                }
            }

           
            if (msgcnt > 0)
            {
                ErrorMessage(msg);
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
            if (grvEmployees.Rows.Count <= 0)
            {
                ErrorMessage("No Record");
                return;
            }

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Leave Marking  -" + DateTime.Now.ToString() + ".xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                // To Export all pages
                grvEmployees.AllowPaging = false;
                LoadAllEmployees();
                if (dt.Rows.Count > 0)
                {
                    grvEmployees.DataSource = dt;
                    grvEmployees.DataBind();
                }

                grvEmployees.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in grvEmployees.HeaderRow.Cells)
                {
                    cell.BackColor = grvEmployees.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in grvEmployees.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = grvEmployees.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = grvEmployees.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                grvEmployees.RenderControl(hw);


                string style = "<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }

        catch (Exception ex)
        {
            _common.ErrorLog("AddLeave.aspx-04", ex.Message.ToString());

        }

    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    private bool IsValidPdf(FileUpload fudocfileRegCopy)
    {

        string _fileFormat = GetpdfMimeDataOfFile(fudocfileRegCopy.PostedFile);
        if (_fileFormat == "application/pdf")
        {
            return true;
        }
        else
        {
            ErrorMessage("Invalid file (Not a PDF)");
            return false;
        }
    }
    public static string GetpdfMimeDataOfFile(HttpPostedFile file)
    {
        IntPtr mimeout = default(IntPtr);
        int MaxContent = Convert.ToInt32(file.ContentLength);
        if (MaxContent > 4096)
        {
            MaxContent = 4096;
        }

        byte[] buf = new byte[MaxContent - 1 + 1];
        file.InputStream.Read(buf, 0, MaxContent);
        int MimeSampleSize = 256;


        string mimeType = System.Web.MimeMapping.GetMimeMapping(file.FileName);
        return mimeType;
    }
    public void Applyleave()
    {
        try
        {
            string UPDATEDBY = Session["_UserCode"].ToString();
            string IPAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(IPAddress))
            {
                IPAddress = Request.ServerVariables["REMOTE_ADDR"];
            }

            DateTime? FromDate = null;
            DateTime? ToDate = null;

            if (!string.IsNullOrEmpty(txtFromdate.Text))
            {
                FromDate = DateTime.ParseExact(txtFromdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }

            if (!string.IsNullOrEmpty(txtToDate.Text))
            {
                ToDate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            byte[] bytes = null;
            bytes = Session["pdf"] as byte[];
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_applyleave");
            MyCommand.Parameters.AddWithValue("spaction", Session["action"].ToString());
            MyCommand.Parameters.AddWithValue("spleaverefno", Session["_REFNO"].ToString());
            MyCommand.Parameters.AddWithValue("p_empcode", Session["_EMPID"].ToString());
            MyCommand.Parameters.AddWithValue("p_leavetype", ddlLeaveType.SelectedValue);
            MyCommand.Parameters.AddWithValue("p_startdate", txtFromdate.Text);
            MyCommand.Parameters.AddWithValue("p_enddate", txtToDate.Text);
            MyCommand.Parameters.AddWithValue("p_reason", txtRemark.Text);
            MyCommand.Parameters.AddWithValue("p_document", (object)bytes ?? DBNull.Value);
            MyCommand.Parameters.AddWithValue("p_updatedby", UPDATEDBY);
            MyCommand.Parameters.AddWithValue("p_ipaddress", IPAddress);
            MyCommand.Parameters.AddWithValue("p_successyn", "20");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                LoadAllEmployees();
                SuccessMessage("Apply Leave has successfully updated");
                Reset();
            }
            else
            {
                ErrorMessage("Leave cannot be marked for selected dates as the Employee is already on a Mark Attendance");
            }
        }

        catch (Exception ex)
        {
            ErrorMessage("Error occurred while Updation. " + ex.Message);
        }
    }
    public void CancelLeave()
    {
        try
        {
            string empcode = Session["_EMPID"].ToString();
            string leaverefno = Session["_REFNO"].ToString();
            string leaveReason = txtCancellationReason.Text.ToString();
            string leaveStartdate = txtFromdate.Text.ToString();
            string leaveEnddate = txtToDate.Text.ToString();
            byte[] uploadedFile = (byte[])Session["pdf"];

            string UpdatedBy = Session["_UserCode"].ToString();
            string IpAddress = HttpContext.Current.Request.UserHostAddress;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_cancelmarkleave");
            MyCommand.Parameters.AddWithValue("spleaverefno", leaverefno);
            MyCommand.Parameters.AddWithValue("p_empcode", empcode);
            MyCommand.Parameters.AddWithValue("p_startdate", leaveStartdate);
            MyCommand.Parameters.AddWithValue("p_enddate", leaveEnddate);
            MyCommand.Parameters.AddWithValue("p_reason", leaveReason);
            MyCommand.Parameters.AddWithValue("p_document", (object)uploadedFile ?? DBNull.Value);
            MyCommand.Parameters.AddWithValue("p_updatedby", UpdatedBy);
            MyCommand.Parameters.AddWithValue("p_ipaddress", IpAddress);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                SuccessMessage("Leave has cancelled successfully");
                LoadAllEmployees();
                Reset();
            }
            else
            {
                _common.ErrorLog("AttendanceManagement-M6", dt.TableName);
                Errormsg(dt.TableName);
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("AttendanceManagement-M6", ex.Message);
            Errormsg(ex.Message);
        }
    }
    public void loadLeave()
    {
        try
        {
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_pendingleaveemp");
            MyCommand.Parameters.AddWithValue("status", Session["Status"].ToString());
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                LoadAllEmployees();
                SuccessMessage("Apply Leave has successfully updated");
                Reset();
            }
            else
            {
                ErrorMessage("Leave cannot be marked for selected dates as the Employee is already on a Mark Attendance");
            }
        }

        catch (Exception ex)
        {
            ErrorMessage("Error occurred while Updation. " + ex.Message);
        }
    }
    public void Reset()
    {
        txtRemark.Text = "";
        txtCancellationReason.Text = "";
        txtFromdate.Text = "";
        txtToDate.Text = "";
        Session["pdf"] = null;
 
    }
    #endregion

    #region "Event"
    protected void grvEmployees_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbtnAddLeave = (LinkButton)e.Row.FindControl("lbtnAddLeave");
            LinkButton lbtnUpdateLeave = (LinkButton)e.Row.FindControl("lbtnUpdateLeave");
            LinkButton lbtnCancelLeave = (LinkButton)e.Row.FindControl("lbtnCancelLeave");
            Label lblStatus = (Label)e.Row.FindControl("lblStatus");
            DataRowView rowView = (DataRowView)e.Row.DataItem;

            lbtnAddLeave.Enabled = true;
            lbtnUpdateLeave.Visible = false;
            lbtnCancelLeave.Visible = false;

            if (!Convert.IsDBNull(rowView["draft_"]))
            {
                draft++;
                lblStatus.Text = rowView["draft_"].ToString();
                lbtnAddLeave.Visible = false;
                lbtnCancelLeave.Visible = true;
                lbtnUpdateLeave.Visible = true;
                if (Convert.ToDateTime(rowView["leavestartdate_"]) < DateTime.Now)
                {
                    lbtnUpdateLeave.Visible = false;
                }
            }
            if (!Convert.IsDBNull(rowView["approved_"]))
            {
                approved++;
                lblStatus.Text = rowView["approved_"].ToString();
                lbtnAddLeave.Visible = false;
                lbtnCancelLeave.Visible = true;
                lbtnUpdateLeave.Visible = false;
            }
        }
    }
    protected void grvEmployees_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "markLeave")
        {
            LoadLeaveTypes();
            txtFromdate.Text = "";
            txtToDate.Text = "";
            txtRemark.Text = "";
            Session["pdf"] = null;
            Session["_EMPID"] = grvEmployees.DataKeys[index].Values["empcode_"];
            Session["_REFNO"] = "";
            txtFromdate.ReadOnly = false;
            lblEmpCode.Text = grvEmployees.DataKeys[index].Values["empcode_"].ToString();
            lblEmpName.Text = grvEmployees.DataKeys[index].Values["empname_"].ToString();
            lblDesigantion.Text = grvEmployees.DataKeys[index].Values["designation_name_"].ToString();
            Session["action"] = "L";
            mpMarkLeave.Show();
        }

        if (e.CommandName == "updateLeave")
        {
            try
            {
                Session["_EMPID"] = grvEmployees.DataKeys[index].Values["empcode_"];
                Session["_REFNO"] = grvEmployees.DataKeys[index].Values["leaverefno_"];
                lblEmpCode.Text = grvEmployees.DataKeys[index].Values["empcode_"].ToString();
                lblEmpName.Text = grvEmployees.DataKeys[index].Values["empname_"].ToString();
                lblDesigantion.Text = grvEmployees.DataKeys[index].Values["designation_name_"].ToString();
                txtFromdate.Text = Convert.ToDateTime(grvEmployees.DataKeys[index].Values["leavestartdate_"]).Date.ToString("dd/MM/yyyy");
                txtFromdate.ReadOnly = true;
                txtToDate.Text = Convert.ToDateTime(grvEmployees.DataKeys[index].Values["leaveenddate"]).Date.ToString("dd/MM/yyyy");
                txtRemark.Text = grvEmployees.DataKeys[index].Values["reason_"].ToString();

                if (grvEmployees.DataKeys[index].Values["attachment_"] != DBNull.Value)
                {
                    byte[] attachment = (byte[])grvEmployees.DataKeys[index].Values["attachment_"];
                    int pdfindex = Array.IndexOf(attachment, (byte)1);

                    if (pdfindex == -1)
                    {
                        Session["pdf"] = null;
                        lbtnPdf.Visible = false;
                    }
                    else
                    {
                        Session["pdf"] = attachment;
                        lbtnPdf.Visible = true;
                        lbtnPdf.Text = "View Document";
                    }
                }
                else
                {
                    Session["pdf"] = null;
                    lbtnPdf.Visible = false;
                }

                LoadLeaveTypes();
                ddlLeaveType.SelectedValue = grvEmployees.DataKeys[index].Values["leavetypeid_"].ToString();
                Session["action"] = "U";
                mpMarkLeave.Show();
            }
            catch (Exception ex)
            {

                return;
            }
        }
        else if (e.CommandName == "cancelLeave")
        {
            Session["_EMPID"] = grvEmployees.DataKeys[index].Values["empcode_"];
            Session["_EMPDESIG"] = Convert.ToInt32(grvEmployees.DataKeys[index].Values["empdesignationcode"]);
            Session["_REFNO"] = grvEmployees.DataKeys[index].Values["leaverefno_"];
            lblEmployee.Text = grvEmployees.DataKeys[index].Values["empname_"].ToString() + " (" + grvEmployees.DataKeys[index].Values["designation_name_"].ToString() + ")";
            lblLeavePeriod.Text = Convert.ToDateTime(grvEmployees.DataKeys[index].Values["leavestartdate_"]).Date.ToString("dd/MM/yyyy") + " To " + Convert.ToDateTime(grvEmployees.DataKeys[index].Values["leaveenddate"]).Date.ToString("dd/MM/yyyy");
            lblLeaveType.Text = grvEmployees.DataKeys[index].Values["leave_typename_"].ToString();

            DateTime dateOne = DateTime.Now;
            DateTime dateTwo = Convert.ToDateTime(grvEmployees.DataKeys[index].Values["leaveenddate"]).Date;
            TimeSpan diff = dateTwo.Subtract(dateOne);
            Session["_LeaveStartDate"] = grvEmployees.DataKeys[index].Values["leavestartdate_"];
            Session["action"] = "C";
            mpCancelLeave.Show();
        }
        else if (e.CommandName == "viewDashboard")
        {
            Session["empCode_chartDash"] = grvEmployees.DataKeys[index].Values["empcode_"];
            Session["empDesigName_chartDash"] = grvEmployees.DataKeys[index].Values["designation_name_"];
            Session["empName_chartDash"] = grvEmployees.DataKeys[index].Values["empname_"];
            Response.Redirect("../DutyAllocation/empStatusCal.aspx");
        }

    }
    protected void lbtnMarkLeave_Click(object sender, EventArgs e)
    {
        try
        {
            if (!IsValidValues())
            {
                return;
            }

            if (Session["action"].ToString() == "L")
            {
                ConfirmMessage("Do you want to Apply Leave ?");
            }
            else if (Session["action"].ToString() == "U")
            {
                ConfirmMessage("Do you want to update Leave details?");
            }
        }
        catch (Exception ex)
        {

        }

    }
    protected void ddlEmptype_SelectedIndexChanged(object sender, EventArgs e)
    {

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
    protected void lbtnDownloadExcel_Click(object sender, EventArgs e)
    {
        ExportGridToExcel();
    }
    protected void LinkButtonInfo_Click(object sender, EventArgs e)
    {

    }
    protected void btnUploadImage_Click(object sender, EventArgs e)
    {
        Session["pdf"] = "";
        if (IsValidPdf(docfile))
        {
            if (docfile.HasFile)
            {
                if (Convert.ToInt32(docfile.FileBytes.Length) < 2097152 && docfile.FileName.Length > 2)
                {
                    if (docfile.FileName.Length <= 50)
                    {
                        Session["pdf"] = docfile.FileBytes;
                        lbtnPdf.Text = docfile.FileName;
                        Session["pdfName"] = docfile.FileName;
                        mpMarkLeave.Show();
                    }
                }
                else
                {
                    ErrorMessage("Please select a file less than 2 MB");
                }
            }
        }

    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        if (Session["action"].ToString() == "L" || Session["action"].ToString() == "U")
        {
            Applyleave();
        }
        else if (Session["action"].ToString() == "C")
        {
            CancelLeave();

        }

    }
    protected void lbtnCancelLeave_Click(object sender, EventArgs e)
    {
        if (txtCancellationReason.Text == "")
        {
            Errormsg("Enter Reason of Cancellation");
            return;
        }
        CancelLeave();
    }
    protected void btnUploadImage2_Click(object sender, EventArgs e)
    {
        Session["pdf"] = "";
        if (IsValidPdf(fileDoc2) == true)
        {
            if (fileDoc2.HasFile == true)
            {
                if (Convert.ToInt32(fileDoc2.FileBytes.Length) < 2097152 & fileDoc2.FileName.Length > 2)
                {
                    if (fileDoc2.FileName.Length <= 50)
                    {
                        Session["pdf"] = fileDoc2.FileBytes;
                        lbtnpdf2.Text = fileDoc2.FileName;
                        Session["pdfName"] = fileDoc2.FileName;
                        mpCancelLeave.Show();
                    }
                }

                else
                {
                    Errormsg("Please select file less than 2 MB");
                }
                    
            }
        }
    }
    protected void lbtnTotalEmployee_Click(object sender, EventArgs e)
    {
        Session["Status"] = "Pending For Approval";
        loadLeave();
    }

    #endregion

}