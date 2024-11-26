using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_tkAttendanceManagement : System.Web.UI.Page
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    string current_date = DateTime.Now.AddDays(-1).ToString("dd") + "/" + DateTime.Now.AddDays(-1).ToString("MM") + "/" + DateTime.Now.AddDays(-1).ToString("yyyy");


    [System.Runtime.InteropServices.DllImport("urlmon.dll")]
    public static extern int FindMimeFromData(IntPtr pBC, [MarshalAs(UnmanagedType.LPWStr)] string pwzUrl, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1, SizeParamIndex = 3)] byte[] pBuffer, int cbSize, [MarshalAs(UnmanagedType.LPWStr)] string pwzMimeProposed, int dwMimeFlags, [MarshalAs(UnmanagedType.U4)] ref int ppwzMimeOut, int dwReserved);

    protected void Page_Load(object sender, EventArgs e)
    {

        checkForSecurity();
        Session["_moduleName"] = "Attendance Management";
        if (IsPostBack == false)
        {
            tbAttendanceDate.Text = current_date;
            LoadDesignation();
            loadEmployees();
            LoadEmployeesSummary();
        }
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
            Response.Redirect("../sessionTimeout.aspx");
        }
        if (_security.isSessionExist(Session["_RNDIDENTIFIERTK"]) == true)
        {
            Session["_RNDIDENTIFIERTK"] = Session["_RNDIDENTIFIERTK"];
        }
        else
        {
            Response.Redirect("../sessionTimeout.aspx");
        }
        if (_security.checkvalidation() == false)
        {
            Response.Redirect("../Errorpage.aspx");
        }
        if (_security.CheckOldUserLogin(Session["_UserCode"].ToString()) == false)
        {
            Response.Redirect("../Errorpage.aspx");
        }
    }
    #region "Methods"
    public void LoadDesignation()
    {
        try
        {
            ddlRole.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_designationforattendance");
            MyCommand.Parameters.AddWithValue("p_role",Convert.ToInt64( Session["_RoleCode"].ToString()));
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlRole.DataSource = dt;
                    ddlRole.DataTextField = "rolename";
                    ddlRole.DataValueField = "rolecode";
                    ddlRole.DataBind();
                }
            }
            else
            {
                _common.ErrorLog("AttendanceManagement-M1", dt.TableName);
                Errormsg(dt.TableName);
            }
        }
        catch (Exception ex)
        {
            ddlRole.Items.Insert(0, "SELECT");
            ddlRole.Items[0].Value = "0";
            ddlRole.SelectedIndex = 0;
            _common.ErrorLog("AttendanceManagement-M1", ex.Message.ToString());
        }
    }
    public void loadEmployees()//M2
    {
        try
        {
            string officeid = Session["_OfficeId"].ToString();
            int role = Convert.ToInt16(ddlRole.SelectedValue);
            string status = ddlStatusType.Text.ToString();
            string searchtext = tbSearchText.Text.ToString();
            string attendancedate = tbAttendanceDate.Text.ToString();
            pnlNoRecord.Visible = true;
            gvEmployees.Visible = false;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_empforattendance");
            MyCommand.Parameters.AddWithValue("p_officeid", officeid);
            MyCommand.Parameters.AddWithValue("p_status", status);
            MyCommand.Parameters.AddWithValue("p_attendancedate", attendancedate);
            MyCommand.Parameters.AddWithValue("p_searchtext", searchtext);
            MyCommand.Parameters.AddWithValue("p_rolecode", role);

            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    gvEmployees.DataSource = dt;
                    gvEmployees.DataBind();
                    pnlNoRecord.Visible = false;
                    gvEmployees.Visible = true;
                }
            }
            else
            {
                _common.ErrorLog("AttendanceManagement-M2", dt.TableName);
                Errormsg(dt.TableName);
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("AttendanceManagement-M2", ex.Message.ToString());
        }
    }
    public void LoadLeaveTypes()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            ddlLeaveType.Items.Clear();
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
            else
            {
                _common.ErrorLog("AttendanceManagement-M3", dt.TableName);
                Errormsg(dt.TableName);
            }
            ddlLeaveType.Items.Insert(0, "Select");
            ddlLeaveType.Items[0].Value = "0";
            ddlLeaveType.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlLeaveType.Items.Insert(0, "Select");
            ddlLeaveType.Items[0].Value = "0";
            ddlLeaveType.SelectedIndex = 0;
            _common.ErrorLog("AttendanceManagement-M3", ex.Message.ToString());
        }
    }

    public void LoadEmployeesSummary()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            ImgpieChartBookingModeNOdata.Visible = true;
            string officeid = Session["_OfficeId"].ToString();
            int role = Convert.ToInt16(ddlRole.SelectedValue);
            string attendancedate = tbAttendanceDate.Text.ToString();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_dailyattendancesummary");
            MyCommand.Parameters.AddWithValue("p_officeid", officeid);
            MyCommand.Parameters.AddWithValue("p_attendancedate", attendancedate);
            MyCommand.Parameters.AddWithValue("p_rolecode", role);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    lbtnTotalEmployee.Text = dt.Rows[0]["totalemployee"].ToString();
                    lbtnNotMarked.Text = dt.Rows[0]["notmarkedemployee"].ToString();
                    lbtnPresent.Text = dt.Rows[0]["presentemployee"].ToString();
                    lbtnAbsent.Text = dt.Rows[0]["absentemployee"].ToString();
                    lbtnOnDuty.Text = dt.Rows[0]["ondutyemployee"].ToString();
                    lbtnOnLeave.Text = dt.Rows[0]["leaveemployee"].ToString();

                    DataTable dtTimeTable = new DataTable("timeTable");
                    dtTimeTable.Columns.Add("statusType", typeof(string));
                    dtTimeTable.Columns.Add("count", typeof(string));

                    dtTimeTable.Rows.Add("Present", dt.Rows[0]["presentemployee"].ToString());
                    dtTimeTable.Rows.Add("Absent", dt.Rows[0]["absentemployee"].ToString());
                    dtTimeTable.Rows.Add("On Leave", dt.Rows[0]["leaveemployee"].ToString());
                    dtTimeTable.Rows.Add("On Duty", dt.Rows[0]["ondutyemployee"].ToString());
                    dtTimeTable.Rows.Add("Not Marked", dt.Rows[0]["notmarkedemployee"].ToString());

                    string chart = "";

                    chart = "<canvas id='bookpiechartmode' width='100%' height='90' ></canvas><script>";

                    chart = chart + "new Chart(document.getElementById('bookpiechartmode'),   { type: 'pie', data: {labels: [ ";


                    for (int i = 0; i <= dtTimeTable.Rows.Count - 1; i++)
                        chart += "'" + dtTimeTable.Rows[i]["statusType"].ToString() + "',";
                    chart = chart.Substring(0, chart.Length - 1);
                    chart = chart + "],datasets: [{ data: [";
                    string value = "";

                    for (int i = 0; i <= dtTimeTable.Rows.Count - 1; i++)
                        value += dtTimeTable.Rows[i]["count"].ToString() + ",";
                    value = value.Substring(0, value.Length - 1);
                    chart = chart + value;
                    chart = chart + "],backgroundColor: ['#11af4b','#f3545d','#fdaf4b',  '#1d7af3'],borderWidth: 0}";
                    chart = chart + "]},options : {responsive: true, maintainAspectRatio: false, legend: {position: 'bottom',labels : {fontColor:'rgb(0, 0, 0)', fontSize: 11, usePointStyle : true, padding: 8	}},";
                    chart = chart + "pieceLabel: {render: 'percentage',fontColor: 'white',fontSize: 11,},tooltips: {bodySpacing: 4,mode: 'nearest', intersect: 0, position: 'nearest', xPadding: 10, yPadding: 10, caretPadding: 10 }, layout: { padding: {left: 0, right: 0, top: 10, bottom: 0 } } }	});";
                    chart = chart + "</script>";
                    ImgpieChartBookingModeNOdata.Visible = false;
                    ltpieChartBookingMode.Text = chart;
                }
            }
            else
            {
                _common.ErrorLog("AttendanceManagement-M3", dt.TableName);
                Errormsg(dt.TableName);
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("AttendanceManagement-M3", ex.Message.ToString());
            Errormsg(ex.Message);
        }
    }
    public void saveAttendance()
    {
        try
        {
            if (ddlAttendanceType.SelectedValue == "0")
            {
                Errormsg("Select Attendance Status");
                return;
            }
            MyCommand = new NpgsqlCommand();

            string empcode = Session["_EMPCODE"].ToString();
            string status = ddlAttendanceType.Text.ToString();
            string attendancedate = tbAttendanceDate.Text.ToString();

            string UpdatedBy = Session["_UserCode"].ToString();
            string IpAddress = HttpContext.Current.Request.UserHostAddress;

            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_markattendance");
            MyCommand.Parameters.AddWithValue("p_empcode", empcode);
            MyCommand.Parameters.AddWithValue("p_attendancedate", attendancedate);
            MyCommand.Parameters.AddWithValue("p_status", status);
            MyCommand.Parameters.AddWithValue("p_updatedby", UpdatedBy);
            MyCommand.Parameters.AddWithValue("p_ipaddress", IpAddress);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                string Maction = dt.Rows[0]["p_successyn"].ToString();
                if (Maction == "Y")
                {
                    loadEmployees();
                    LoadEmployeesSummary();
                    ddlAttendanceType.SelectedValue = "0";
                    Successmsg("Attendance has successfully marked");
                }
                else
                {
                    Errormsg("Please mark attendance of previous date before continue");
                }
            }
            else
            {
                _common.ErrorLog("AttendanceManagement-M4", dt.TableName);
                Errormsg(dt.TableName);
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("AttendanceManagement-M4", ex.Message);
            Errormsg(ex.Message);
        }
    }
    public void init()
    {
        tbRemark.Text = "";
        tbCancellationReason.Text = "";
        Session["pdf"] = null;
        Session["pdfName"] = null;
        tbLeaveStartDate.Text = "";
        tbLeaveEndDate.Text = "";
        ddlLeaveType.SelectedValue = "0";
        ddlStatusType.SelectedValue = "0";
    }
    private bool IsValidValues()
    {
        try
        {
            int msgcnt = 0;
            string msg = "";
            if (ddlLeaveType.SelectedValue == "0")
            {
                msgcnt = msgcnt + 1;
                msg = msg + msgcnt.ToString() + ". Select Leave Type<br/>";
            }
            if (tbLeaveStartDate.Text.Length > 0)
            {
                if (_validation.IsDate(tbLeaveStartDate.Text) == false)
                {
                    msgcnt = msgcnt + 1;
                    msg = msg + msgcnt.ToString() + ". Invalid Leave Start Date<br/>";
                }
                else
                {
                    DateTime fromDate = DateTime.ParseExact(tbLeaveStartDate.Text, "dd/MM/yyyy", null/* TODO Change to default(_) if this is not a reference type */);
                    DateTime currDate = DateTime.Now.Date;
                    //if (currDate > fromDate)
                    //{
                    //    msgcnt = msgcnt + 1;
                    //    msg = msg + msgcnt.ToString() + ". Check Leave Start Date. Start Date should be greater than Today's Date.<br/>";
                    //}
                }
            }
            else
            {
                msgcnt = msgcnt + 1;
                msg = msg + msgcnt.ToString() + ". Leave Start Date.<br/>";
            }
            if (tbLeaveEndDate.Text.Length > 0)
            {
                if (_validation.IsDate(tbLeaveEndDate.Text) == false)
                {
                    msgcnt = msgcnt + 1;
                    msg = msg + msgcnt.ToString() + ". Invalid Leave End Date.<br/>";
                }
                else
                {
                    DateTime fromDate = DateTime.ParseExact(tbLeaveStartDate.Text, "dd/MM/yyyy", null);
                    DateTime ToDate = DateTime.ParseExact(tbLeaveEndDate.Text, "dd/MM/yyyy", null);
                    if (ToDate < fromDate)
                    {
                        msgcnt = msgcnt + 1;
                        msg = msg + msgcnt.ToString() + ". Check To Date. Leave End Date should be Greater than Leave Start Date.<br/>";
                    }
                }
            }
            else
            {
                msgcnt = msgcnt + 1;
                msg = msg + msgcnt.ToString() + ". Leave End Date.<br/>";
            }
            if (tbRemark.Text.Length > 0)
            {
                if (_validation.IsValidString(tbRemark.Text, 1, tbRemark.MaxLength) == false)
                {
                    msgcnt = msgcnt + 1;
                    msg = msg + msgcnt.ToString() + ". Reason of Leave<br>";
                }
            }
            if (msgcnt > 0)
            {
                Errormsg(msg);
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    public bool IsValidPdf(FileUpload fileupload)
    {
        string _fileFormat = GetpdfMimeDataOfFile(fileupload.PostedFile);
        if (((_fileFormat == "application/pdf")))
        {
        }
        else
        {
            Errormsg("Invalid file (Upload PDF only)");
            return false;
        }
        return true;
    }
    public void Markleave()
    {
        try
        {
            MyCommand = new NpgsqlCommand();

            string empcode = Session["_EMPCODE"].ToString();
            string action = Session["action"].ToString();
            string leaverefno = Session["_REFNO"].ToString();
            int role = Convert.ToInt16(Session["_EMPDESIG"].ToString());
            int leavetype = Convert.ToInt16(ddlLeaveType.SelectedValue.ToString());
            string leaveReason = tbRemark.Text.ToString();
            string leaveStartdate = tbLeaveStartDate.Text.ToString();
            string leaveEnddate = tbLeaveEndDate.Text.ToString();
            byte[] uploadedFile = (byte[])Session["pdf"];

            string UpdatedBy = Session["_UserCode"].ToString();
            string IpAddress = HttpContext.Current.Request.UserHostAddress;

            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_markempleave");
            MyCommand.Parameters.AddWithValue("spaction", action);
            MyCommand.Parameters.AddWithValue("spleaverefno", leaverefno);
            MyCommand.Parameters.AddWithValue("p_empcode", empcode);
            MyCommand.Parameters.AddWithValue("p_empdesig", role);
            MyCommand.Parameters.AddWithValue("p_leavetype", leavetype);
            MyCommand.Parameters.AddWithValue("p_startdate", leaveStartdate);
            MyCommand.Parameters.AddWithValue("p_enddate", leaveEnddate);
            MyCommand.Parameters.AddWithValue("p_reason", leaveReason);
            MyCommand.Parameters.AddWithValue("p_document", (object)uploadedFile ?? DBNull.Value);
            MyCommand.Parameters.AddWithValue("p_updatedby", UpdatedBy);
            MyCommand.Parameters.AddWithValue("p_ipaddress", IpAddress);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                string Maction = dt.Rows[0]["p_successyn"].ToString();
                if (Maction == "EXCEPTION")
                {
                    Errormsg("Error occurred while Updation.");
                    return;
                }
                else if (Maction == "Y")
                {
                    loadEmployees();
                    LoadEmployeesSummary();
                    Successmsg("Leave has successfully updated");
                    init();
                }
                else if (Maction == "N")
                {
                    Errormsg("Leave cannot be mark for selected dates as Employee is already on a trip or attendance already marked");
                }
            }
            else
            {
                _common.ErrorLog("AttendanceManagement-M5", dt.TableName);
                Errormsg(dt.TableName);
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("AttendanceManagement-M5", ex.Message);
            Errormsg(ex.Message);
        }
    }
    public void CancelLeave()
    {
        try
        {
            string empcode = Session["_EMPCODE"].ToString();
            string leaverefno = Session["_REFNO"].ToString();
            string leaveReason = tbRemark.Text.ToString();
            string leaveStartdate = tbLeaveStartDate.Text.ToString();
            string leaveEnddate = tbLeaveEndDate.Text.ToString();
            byte[] uploadedFile = (byte[])Session["pdf"];

            string UpdatedBy = Session["_UserCode"].ToString();
            string IpAddress = HttpContext.Current.Request.UserHostAddress;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_cancelempleave");
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
                Successmsg("Leave has cancelled successfully");
                loadEmployees();
                LoadEmployeesSummary();
                init();
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
    private void Errormsg(string msg)
    {
        string popup = _popup.modalPopupSmall("W", "Please Note", msg, "Close");
        Response.Write(popup);
    }
    private void Successmsg(string msg)
    {
        string popup = _popup.modalPopupSmall("S", "Success", msg, "OK");
        Response.Write(popup);
    }
    private void ConfirmMsg(string msg)
    {
        lblConfirmation.Text = msg;
        mpConfirmation.Show();
    }
    private void InfoMsg(string msg)
    {
        string popup = _popup.modalPopupLarge("I", "Information", msg, "Close");
        Response.Write(popup);
    }
    #endregion
    #region "Events"
    protected void gvEmployees_RowCommand(object sender, GridViewCommandEventArgs e)//E1
    {
        try
        {
            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "markAttendence")
            {
                GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                int RowIndex = oItem.RowIndex;
                Session["_EMPCODE"] = gvEmployees.DataKeys[index].Values["empcode"];
                Session["_EMPDESIG"] = Convert.ToInt32(gvEmployees.DataKeys[index].Values["designationcode"]);
                Session["_REFNO"] = gvEmployees.DataKeys[index].Values["leaverefno"];
                string empName = gvEmployees.DataKeys[index].Values["empname"].ToString();
                Session["RowIndex"] = RowIndex;
                Session["action"] = "A";
                lblAttendance.Text = empName + " (" + ddlRole.SelectedItem.ToString() + ")";
                mpMarkAttendance.Show();
            }
            else if (e.CommandName == "markLeave")
            {
                try
                {
                    LoadLeaveTypes();
                    Session["_EMPCODE"] = gvEmployees.DataKeys[index].Values["empcode"];
                    Session["_EMPDESIG"] = Convert.ToInt32(gvEmployees.DataKeys[index].Values["designationcode"]);
                    Session["_REFNO"] = gvEmployees.DataKeys[index].Values["leaverefno"];
                    tbLeaveStartDate.ReadOnly = false;
                    tbLeaveStartDate.Enabled = true;
                    string empName = gvEmployees.DataKeys[index].Values["empname"].ToString();
                    lblEmp.Text = empName + " (" + ddlRole.SelectedItem.ToString() + ")";
                    Session["action"] = "S";
                    mpMarkLeave.Show();
                }
                catch (Exception ex)
                {
                    return;
                }
            }
            else if (e.CommandName == "updateLeave")
            {
                try
                {
                    Session["_EMPCODE"] = gvEmployees.DataKeys[index].Values["empcode"];
                    Session["_EMPDESIG"] = Convert.ToInt32(gvEmployees.DataKeys[index].Values["designationcode"]);
                    Session["_REFNO"] = gvEmployees.DataKeys[index].Values["leaverefno"];

                    tbLeaveStartDate.Text = gvEmployees.DataKeys[index].Values["leavestartdate"].ToString();
                    tbLeaveStartDate.ReadOnly = true;
                    tbLeaveStartDate.Enabled = false;
                    string empName = gvEmployees.DataKeys[index].Values["empname"].ToString();
                    lblEmp.Text = empName + " (" + ddlRole.SelectedItem.ToString() + ")";
                    tbLeaveEndDate.Text = gvEmployees.DataKeys[index].Values["leaveenddate"].ToString();
                    tbRemark.Text = gvEmployees.DataKeys[index].Values["leavereason"].ToString();
                    if (gvEmployees.DataKeys[index].Values["attached_doc"].ToString() != "")
                    {
                        Session["pdf"] = gvEmployees.DataKeys[index].Values["attached_doc"];
                        lbtnPdf.Visible = true;
                        lbtnPdf.Text = "View Document";
                    }
                    else
                    {
                        Session["pdf"] = null;
                        lbtnPdf.Visible = false;
                    }
                    LoadLeaveTypes();
                    ddlLeaveType.SelectedValue = gvEmployees.DataKeys[index].Values["leavetypeid"].ToString();
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
                Session["_EMPCODE"] = gvEmployees.DataKeys[index].Values["empcode"];
                Session["_EMPDESIG"] = Convert.ToInt32(gvEmployees.DataKeys[index].Values["designationcode"]);
                Session["_REFNO"] = gvEmployees.DataKeys[index].Values["leaverefno"];
                string empName = gvEmployees.DataKeys[index].Values["empname"].ToString();
                lblLeaveEmp.Text = empName + " (" + ddlRole.SelectedItem.ToString() + ")";
                lblLeavePeriod.Text = gvEmployees.DataKeys[index].Values["leavestartdate"] + " To " + gvEmployees.DataKeys[index].Values["leaveenddate"];
                lblLeaveType.Text = gvEmployees.DataKeys[index].Values["leavetype"].ToString();

                DateTime dateOne = DateTime.Now;
                DateTime dateTwo = DateTime.ParseExact(gvEmployees.DataKeys[index].Values["leaveenddate"].ToString(), "dd/MM/yyyy", null);
                TimeSpan diff = dateTwo.Subtract(dateOne);
                Session["_LeaveStartDate"] = gvEmployees.DataKeys[index].Values["leavestartdate"];
                Session["action"] = "C";
                mpCancelLeave.Show();
            }
            else if (e.CommandName == "viewDashboard")
            {
                Session["empCode_chartDash"] = gvEmployees.DataKeys[index].Values["empcode"];
                Session["empDesigName_chartDash"] = gvEmployees.DataKeys[index].Values["designation"];
                Session["empName_chartDash"] = gvEmployees.DataKeys[index].Values["empname"];
                Response.Redirect("tkEmpStatusCalendar.aspx");
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message);
            _common.ErrorLog("SysAdmETM-E1", ex.Message.ToString());
        }
    }
    protected void gvEmployees_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string ss = "fdv";
            LinkButton lbtnAttendance = (LinkButton)e.Row.FindControl("lbtnAttendance");
            LinkButton lbtnAddLeave = (LinkButton)e.Row.FindControl("lbtnAddLeave");
            LinkButton lbtnUpdateLeave = (LinkButton)e.Row.FindControl("lbtnUpdateLeave");
            LinkButton lbtnCancelLeave = (LinkButton)e.Row.FindControl("lbtnCancelLeave");
            DataRowView rowView = (DataRowView)e.Row.DataItem;
            lbtnUpdateLeave.Visible = false;
            lbtnCancelLeave.Visible = false;
            lbtnAttendance.Visible = true;
            lbtnAddLeave.Enabled = true;
            lbtnAttendance.Enabled = true;
            lbtnAddLeave.CssClass = "btn btn-sm btn-danger";
            lbtnAttendance.CssClass = "btn btn-sm btn-success";

            if (rowView["status"].ToString() == "L")
            {
                lbtnCancelLeave.Visible = true;
                lbtnUpdateLeave.Visible = true;
                lbtnAddLeave.Visible = false;
                lbtnAttendance.Visible = false;
            }
            else if (rowView["status"].ToString() == "P" || rowView["status"].ToString() == "A")
            {
                lbtnAddLeave.Enabled = false;
                lbtnAttendance.Enabled = false;
                lbtnAddLeave.CssClass = "btn btn-sm btn-default";
                lbtnAttendance.CssClass = "btn btn-sm btn-default";
            }
            ddlAttendanceType.Items.FindByValue("P").Enabled = true;
            string service_start_date = DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("yyyy");
            string search_date = tbAttendanceDate.Text;

            DateTime st = DateTime.ParseExact(service_start_date, "dd/MM/yyyy", null);
            DateTime sd = DateTime.ParseExact(search_date, "dd/MM/yyyy", null);

            //string st = Convert.ToDateTime(service_start_date).ToString();
            // string sd = Convert.ToDateTime(search_date).ToString("dd-MM-yyyy");
            double cc = (st - sd).TotalDays;

            if (cc > 1)
            {
                lbtnCancelLeave.Visible = false;
                lbtnUpdateLeave.Visible = false;
                lbtnAddLeave.Visible = true;
                lbtnAttendance.Visible = true;
                ddlAttendanceType.Items.FindByValue("P").Enabled = false; 
            }
            if (rowView["status"].ToString() == "L")
            {
                lbtnCancelLeave.Visible = true;
                lbtnUpdateLeave.Visible = true;
                lbtnAddLeave.Visible = false;
                lbtnAttendance.Visible = false;
            }
            else if (rowView["status"].ToString() == "P" || rowView["status"].ToString() == "A")
            {
                lbtnAddLeave.Enabled = false;
                lbtnAttendance.Enabled = false;
                lbtnAddLeave.CssClass = "btn btn-sm btn-default";
                lbtnAttendance.CssClass = "btn btn-sm btn-default";
            }

        }
    }

    protected void btnUploadImage_Click(object sender, System.EventArgs e)
    {
        Session["pdf"] = "";
        if (IsValidPdf(docfile) == true)
        {
            if (docfile.HasFile == true)
            {
                if (Convert.ToInt32(docfile.FileBytes.Length) < 2097152 & docfile.FileName.Length > 2)
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
                    Errormsg("Please select file less than 2 MB");
            }
        }
    }
    protected void btnUploadImage2_Click(object sender, System.EventArgs e)
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
                    Errormsg("Please select file less than 2 MB");
            }
        }
    }
    public string GetpdfMimeDataOfFile(HttpPostedFile file)
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
    protected void lbtnMarkLeave_Click(object sender, EventArgs e)
    {
        try
        {
            if (IsValidValues() == false)
            {
                return;
            }
            if (Session["action"].ToString() == "S")
            {
                ConfirmMsg("Do you want to mark Leave?");
            }
            else if (Session["action"].ToString() == "U")
            {
                ConfirmMsg("Do you want to update Leave details?");
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("SysAdmETM-E2", ex.Message.ToString());
        }
    }
    protected void lbtnSaveAttendance_Click(object sender, EventArgs e)
    {
        saveAttendance();
    }
    protected void lbtnCancelLeave_Click(object sender, EventArgs e)
    {
        if (tbCancellationReason.Text == "")
        {
            Errormsg("Enter Reason of Cancellation");
            return;
        }
        CancelLeave();
    }
    protected void lbtnPdf_Click(object sender, EventArgs e)
    {
        try
        {
            byte[] bytes = new byte[1];
            bytes = (byte[])Session["pdf"];
            string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + "PDF" + DateTime.Now.Date.ToString("ddMMyyyy") + ".pdf");
            Response.BinaryWrite(bytes);
            Response.End();
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message);
        }
    }
    protected void gvEmployees_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvEmployees.PageIndex = e.NewPageIndex;
        loadEmployees();
    }
    protected void lbtnViewEmp_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        string status = btn.CommandName;
        ddlStatusType.SelectedValue = status;
        loadEmployees();
    }
    protected void lbtnDownload_Click(object sender, EventArgs e)
    {
        ExportGridToExcel();
    }
    private void ExportGridToExcel()
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=EmployeesList.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            // To Export all pages
            gvEmployees.AllowPaging = false;
            this.loadEmployees();

            gvEmployees.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in gvEmployees.HeaderRow.Cells)
                cell.BackColor = gvEmployees.HeaderStyle.BackColor;
            foreach (GridViewRow row in gvEmployees.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                        cell.BackColor = gvEmployees.AlternatingRowStyle.BackColor;
                    else
                        cell.BackColor = gvEmployees.RowStyle.BackColor;
                    cell.CssClass = "textmode";
                }
            }

            gvEmployees.RenderControl(hw);
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
    protected void lbtnSearch_Click(object sender, EventArgs e)
    {
        loadEmployees();
        LoadEmployeesSummary();
    }
    protected void lbtnResetList_Click(object sender, EventArgs e)
    {
        tbSearchText.Text = "";
        ddlStatusType.SelectedValue = "N";
        loadEmployees();
        LoadEmployeesSummary();
    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        if (Session["action"].ToString() == "S" | Session["action"].ToString() == "U")
        {
            Markleave();
        }
        else if (Session["action"].ToString() == "C")
        {
            CancelLeave();
        }
    }

    #endregion
}