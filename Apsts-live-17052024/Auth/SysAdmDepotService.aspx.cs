using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Npgsql;
using System.IO;
using System.Xml;
using System.Collections;
using System.Xml.XPath;

public partial class Auth_SysAdmDepotService : BasePage
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    string commonerror = "There is some error. Please contact the administrator or try again after some time.";
    protected void Page_Load(object sender, EventArgs e)
    {
        lblSummary.Text = "Summary as on Date " + DateTime.Now.ToString("dd/MM/yyyy hh:ss tt");
        Session["_moduleName"] = "Depot Service";
        if (!IsPostBack)
        {
            Random randomclass = new Random();
            Session["rndNoCheck"] = randomclass.Next().ToString();
            hidtoken.Value = Session["rndNoCheck"].ToString();
            getDepotServicesList("");
            fillDropDownOffices(ddlDepot);
            fillDropDownServiceTypes(ddlServiceType);
            fillDropDownBusTypes(ddlBusType);
            fillDropDownRoutes(ddlRoute);
            fillDropDownNightAlloedCat(ddlNightAllowedCat);
            fillDropDownLayout(ddlLayout);
            fillDropDownNightAlloedCat(ddlNightAllowedCatUp);
            fillDropDownLayout(ddlLayoutUp);
            rest_addDepotServiceFields();
            filldays();
        }
    }
    private void filldays()
    {
        try
        {
            DataTable MyTable = new DataTable();
            MyTable.Columns.Add("value", typeof(int));
            MyTable.Columns.Add("text", typeof(string));

            MyTable.Rows.Add(1, "Monday");
            MyTable.Rows.Add(2, "Tuesday");
            MyTable.Rows.Add(3, "Wednesday");
            MyTable.Rows.Add(4, "Thursday");
            MyTable.Rows.Add(5, "Friday");
            MyTable.Rows.Add(6, "Saturday");
            MyTable.Rows.Add(0, "Sunday");
            if (MyTable.Rows.Count > 0)
            {
                ddlDays.DataSource = MyTable;
                ddlDays.DataTextField = "text";
                ddlDays.DataValueField = "value";
                ddlDays.DataBind();
	
                ddlTripDays.DataSource = MyTable;
                ddlTripDays.DataTextField = "text";
                ddlTripDays.DataValueField = "value";
                ddlTripDays.DataBind();

                //ddlDaysUpdate.DataSource = MyTable;
                //ddlDaysUpdate.DataTextField = "text";
                //ddlDaysUpdate.DataValueField = "value";
                //ddlDaysUpdate.DataBind();
            }
        }
        catch (Exception ex)
        {
        }
    }

    #region "Methods"
    private void CsrfTokenValidate()
    {
        if (hidtoken.Value != Session["rndNoCheck"].ToString())
        {
            Response.Redirect("errorpage.aspx");
            return;
        }
    }
    public void fillDropDownOffices(DropDownList ddl)//M1
    {
        try
        {
            ddl.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_ofclvlwiseoffice");
            MyCommand.Parameters.AddWithValue("p_ofclvlid", 30);
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                ddl.DataSource = dt;
                ddl.DataTextField = "officename";
                ddl.DataValueField = "officeid";
                ddl.DataBind();
            }
            ddl.Items.Insert(0, "SELECT");
            ddl.Items[0].Value = "0";
            ddl.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddl.Items.Insert(0, "SELECT");
            ddl.Items[0].Value = "0";
            ddl.SelectedIndex = 0;
            _common.ErrorLog("SysAdmDepotService.aspx-0001", ex.Message.ToString());
        }
    }
    public void fillDropDownServiceTypes(DropDownList ddl)//M2
    {
        try
        {
            ddl.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_servicetype_getlist");
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                ddl.DataSource = dt;
                ddl.DataTextField = "servicetype_name_en";
                ddl.DataValueField = "srtpid";
                ddl.DataBind();
            }
            ddl.Items.Insert(0, "SELECT");
            ddl.Items[0].Value = "0";
            ddl.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddl.Items.Insert(0, "SELECT");
            ddl.Items[0].Value = "0";
            ddl.SelectedIndex = 0;
            _common.ErrorLog("SysAdmDepotService.aspx-0002", ex.Message.ToString());
        }
    }
    public void fillDropDownBusTypes(DropDownList ddl)//M3
    {
        try
        {
            ddl.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_bustype");
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                ddl.DataSource = dt;
                ddl.DataTextField = "bustype_name";
                ddl.DataValueField = "bustype_id";
                ddl.DataBind();
            }
            ddl.Items.Insert(0, "SELECT");
            ddl.Items[0].Value = "0";
            ddl.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddl.Items.Insert(0, "SELECT");
            ddl.Items[0].Value = "0";
            ddl.SelectedIndex = 0;
            _common.ErrorLog("SysAdmDepotService.aspx-0003", ex.Message.ToString());
        }
    }
    public void fillDropDownRoutes(DropDownList ddl)//M4
    {
        try
        {
            ddl.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_routes");
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                ddl.DataSource = dt;
                ddl.DataTextField = "routename";
                ddl.DataValueField = "routeid";
                ddl.DataBind();
            }
            ddl.Items.Insert(0, "SELECT");
            ddl.Items[0].Value = "0";
            ddl.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddl.Items.Insert(0, "SELECT");
            ddl.Items[0].Value = "0";
            ddl.SelectedIndex = 0;
            _common.ErrorLog("SysAdmDepotService.aspx-0004", ex.Message.ToString());
        }
    }
    public void fillDropDownNightAlloedCat(DropDownList ddl)//M5
    {
        try
        {
            ddl.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_night_allowed_category");
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                ddl.DataSource = dt;
                ddl.DataTextField = "categorytype";
                ddl.DataValueField = "categoryid";
                ddl.DataBind();
            }
            ddl.Items.Insert(0, "SELECT");
            ddl.Items[0].Value = "0";
            ddl.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddl.Items.Insert(0, "SELECT");
            ddl.Items[0].Value = "0";
            ddl.SelectedIndex = 0;
            _common.ErrorLog("SysAdmDepotService.aspx-0005", ex.Message.ToString());
        }
    }
    public void fillDropDownLayout(DropDownList ddl)//M6
    {
        try
        {
            ddl.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_layout");
            MyCommand.Parameters.AddWithValue("p_layoutstatus", "L");
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                ddl.DataSource = dt;
                ddl.DataTextField = "layoutname";
                ddl.DataValueField = "layoutcode";
                ddl.DataBind();
            }
            ddl.Items.Insert(0, "SELECT");
            ddl.Items[0].Value = "0";
            ddl.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddl.Items.Insert(0, "SELECT");
            ddl.Items[0].Value = "0";
            ddl.SelectedIndex = 0;
            _common.ErrorLog("SysAdmDepotService.aspx-0006", ex.Message.ToString());
        }
    }
    private void saveDepotService()//M7
    {
        try
        {
            if (isValidDepotService() == false)
            {
                return;
            }

            string depotId = ddlDepot.SelectedValue.ToString();
            string serviceTypeId = ddlServiceType.SelectedValue.ToString();
            string busType = ddlBusType.SelectedValue.ToString();
            string routeId = ddlRoute.SelectedValue.ToString();
            string trips = tbTrips.Text.Trim();
            string publishyn = "N";

            DateTime d = DateTime.Parse(tbDeptTime.Text);
            string dept_time = d.ToString("hh:mm tt");

            string serviceDuration = tbServiceDuration.Text.Trim();
            string dutyDays = tbDutyDays.Text.Trim();

            string restApplicableYN = "N";
            string restDays = "0";
            if (cbRestApplicable.Checked == true)
            {
                restApplicableYN = "Y";
                restDays = tbRestDays.Text.Trim();
            }
            string nightAllowedYN = "N";
            string nightCatgory = "0";
            if (cbNightAllowed.Checked == true)
            {
                nightAllowedYN = "Y";
                nightCatgory = ddlNightAllowedCat.SelectedValue.ToString();
            }
            string overtimeAllowedYN = "N";
            string overtimeHours = "0";
            if (cbOvertimeAllowed.Checked == true)
            {
                overtimeAllowedYN = "Y";
                overtimeHours = tbOvertimeHours.Text.Trim();
            }

            string drivers = tbDrivers.Text.Trim();
            string conductors = tbConductors.Text.Trim();

            string layoutCode = ddlLayout.SelectedValue.ToString();
            string serviceFrom = tbServiceFrom.Text.ToString();
            string serviceTo = tbServiceTo.Text.ToString();



            string fareYN = "N";
            if (cbApplicableFare.Checked == true)
            {
                fareYN = "Y";
            }
            string reservationYN = "N";
            if (cbApplicableReservation.Checked == true)
            {
                reservationYN = "Y";
            }
            if (chkpublish.Checked == true)
            {
                publishyn = "Y";
            }
            string dailyWeekly = "";
            string WeekDays = "";
            if (rbtdaily.Checked == true)
            {
                dailyWeekly = "D";
                WeekDays = "";
            }
            if (rbtweekly.Checked == true)
            {
                dailyWeekly = "W";
                if (ddlDays.Items.Count > 0)
                {
                    for (int i = 0; i < ddlDays.Items.Count; i++)
                    {
                        if (ddlDays.Items[i].Selected)
                        {
                            WeekDays += ddlDays.Items[i].Value + ",";
                        }
                    }
                    if (WeekDays == "")
                    {
                        Errormsg("Please Select weekly Days");
                        return;
                    }
                }
            }


            string UpdatedBy = Session["_UserCode"].ToString();
            string IpAddress = HttpContext.Current.Request.UserHostAddress;
            //string action = Session["_action"].ToString();
            string result = "";

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_depotservice_insert");
            MyCommand.Parameters.AddWithValue("p_depotid", depotId);
            MyCommand.Parameters.AddWithValue("p_servicetypeid", Convert.ToInt16(serviceTypeId));
            MyCommand.Parameters.AddWithValue("p_bustypeid", busType);
            MyCommand.Parameters.AddWithValue("p_routeid", Convert.ToInt64(routeId));
            MyCommand.Parameters.AddWithValue("p_nooftrips", Convert.ToInt16(trips));
            MyCommand.Parameters.AddWithValue("p_dept_time", dept_time);

            MyCommand.Parameters.AddWithValue("p_serviceduration", Convert.ToInt16(serviceDuration));
            MyCommand.Parameters.AddWithValue("p_noofdutydays", Convert.ToInt16(dutyDays));
            MyCommand.Parameters.AddWithValue("p_dutyrestapplicableyn", restApplicableYN);
            MyCommand.Parameters.AddWithValue("p_dutyrestdays", Convert.ToInt16(restDays));
            MyCommand.Parameters.AddWithValue("p_nightallowedyn", nightAllowedYN);
            MyCommand.Parameters.AddWithValue("p_night_category", nightCatgory);
            MyCommand.Parameters.AddWithValue("p_overtimeallowedyn", overtimeAllowedYN);
            MyCommand.Parameters.AddWithValue("p_overtime_hours", Convert.ToInt16(overtimeHours));
            MyCommand.Parameters.AddWithValue("p_noofdriver", Convert.ToInt16(drivers));
            MyCommand.Parameters.AddWithValue("p_noofconductor", Convert.ToInt16(conductors));

            MyCommand.Parameters.AddWithValue("p_layoutcode", Convert.ToInt16(layoutCode));
            MyCommand.Parameters.AddWithValue("p_servicestartdate", serviceFrom);
            MyCommand.Parameters.AddWithValue("p_serviceenddate", serviceTo);
            MyCommand.Parameters.AddWithValue("p_fareyn", fareYN);
            MyCommand.Parameters.AddWithValue("p_reservationyn", reservationYN);
            MyCommand.Parameters.AddWithValue("p_dailyweeklytype", dailyWeekly);
            MyCommand.Parameters.AddWithValue("p_weeklydays", WeekDays);
            MyCommand.Parameters.AddWithValue("p_publishyn", publishyn);

            MyCommand.Parameters.AddWithValue("p_updatedby", UpdatedBy);
            MyCommand.Parameters.AddWithValue("p_ipaddress", IpAddress);
            result = bll.InsertAll(MyCommand);
            if (result == "Success")
            {
                Successmsg("Depot service has been saved successfully. Please proceed to the relevant steps.");
                getDepotServicesList("");
            }
            else
            {
                Errormsg(commonerror);
                _common.ErrorLog("SysAdmDepotService.aspx-0007", result);
            }
        }
        catch (Exception ex)
        {
            Errormsg(commonerror);
            _common.ErrorLog("SysAdmDepotService.aspx-0008", ex.Message.ToString());
        }
    }
    private void getDepotServicesList(string searchText)//M8
    {
        try
        {
            gvDepotServices.Visible = false;
            pnlNoDepotService.Visible = true;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_depotservice_getlist");
            MyCommand.Parameters.AddWithValue("p_search_text", searchText);
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                gvDepotServices.DataSource = dt;
                gvDepotServices.DataBind();

                lblTotalDepotService.Text = dt.Rows[0]["totalds"].ToString();
                lblActive.Text = dt.Rows[0]["activeds"].ToString();
                lblDeactive.Text = dt.Rows[0]["deactiveds"].ToString();

                gvDepotServices.Visible = true;
                pnlNoDepotService.Visible = false;

                ddlDepotServiceRpt.DataSource = dt;
                ddlDepotServiceRpt.DataTextField = "servicename";
                ddlDepotServiceRpt.DataValueField = "dsvcid";
                ddlDepotServiceRpt.DataBind();

            }
            ddlDepotServiceRpt.Items.Insert(0, "SELECT");
            ddlDepotServiceRpt.Items[0].Value = "0";
            ddlDepotServiceRpt.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            Errormsg(commonerror);
            _common.ErrorLog("SysAdmDepotService.aspx-0009", ex.Message.ToString());
        }
    }
    public bool isValidDepotService()
    {
        int msgcount = 0;
        string msg = "";

        if (ddlDepot.SelectedValue == "0")
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Select depot.<br/>";
        }
        if (ddlServiceType.SelectedValue == "0")
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Select service type.<br/>";
        }

        try
        {
            DateTime d = DateTime.Parse(tbDeptTime.Text);

            if (d.ToString("hh:mm tt").Length < 8)
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Enter valid Departure Time.<br/>";
            }
        }
        catch
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Enter valid Departure Time.<br/>";
        }


        if (_validation.IsValidInteger(tbTrips.Text, 1, 2) == false)
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Enter valid no. of trips.<br/>";
        }
        if (ddlBusType.SelectedValue == "0")
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Select bus type.<br/>";
        }
        if (ddlRoute.SelectedValue == "0")
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Select route.<br/>";
        }

        if (_validation.IsValidInteger(tbServiceDuration.Text, 1, 2) == false)
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Enter valid Service Duration (no. of days).<br/>";
        }
        if (_validation.IsValidInteger(tbDutyDays.Text, 1, 2) == false)
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Enter valid no. of duty days.<br/>";
        }
        if (cbRestApplicable.Checked == true)
        {
            if (_validation.IsValidInteger(tbRestDays.Text, 1, 2) == false)
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". You selected 'Rest Applicable'. Enter valid no. of rest days.<br/>";
            }
        }
        if (cbNightAllowed.Checked == true)
        {
            if (ddlNightAllowedCat.SelectedValue == "0")
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". You selected 'Nigth Allowed'. Select Night Allowed Category.<br/>";
            }
        }
        if (cbOvertimeAllowed.Checked == true)
        {
            if (_validation.IsValidInteger(tbOvertimeHours.Text, 1, 2) == false)
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". You selected 'Overtime Allowed'. Enter valid no. of overtime hours.<br/>";
            }
        }

        if (_validation.IsValidInteger(tbDrivers.Text, 1, 2) == false)
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Enter valid no. of Drivers ( At least 1).<br/>";
        }
        if (_validation.IsValidInteger(tbConductors.Text, 1, 2) == false)
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Enter valid no. of Conductors ( At least 1).<br/>";
        }




        string serviceFrom = tbServiceFrom.Text.ToString();
        string serviceTo = tbServiceTo.Text.ToString();

        if (serviceFrom.Length > 0)
        {
            if (serviceTo.Length != 10 || serviceFrom.Length != 10)
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Enter valid Service Period (From Date and To Date) in ONLINE-RELATED DETAILS. <br/>";
            }
            else
            {
                try
                {
                    DateTime dtFrom = DateTime.ParseExact(serviceFrom, "dd/MM/yyyy", null/* TODO Change to default(_) if this is not a reference type */);
                    DateTime dtTo = DateTime.ParseExact(serviceTo, "dd/MM/yyyy", null/* TODO Change to default(_) if this is not a reference type */);
                    DateTime todayDate = DateTime.Now.Date;

                    if (dtFrom < todayDate || dtTo < dtFrom)
                    {
                        msgcount = msgcount + 1;
                        msg = msg + msgcount.ToString() + ". Enter valid Service Period (From Date and To Date) in ONLINE-RELATED DETAILS. 'Service Period From Date' will be greater than Today and 'Service Period From Date' will be less than or equal to 'Service Period To Date'. <br/>";
                    }
                }
                catch (Exception e)
                {
                    msgcount = msgcount + 1;
                    msg = msg + msgcount.ToString() + ". Enter valid Service Period (From Date and To Date) in ONLINE-RELATED DETAILS. <br/>";
                }
            }
        }




        if (msgcount > 0)
        {
            Errormsg(msg);
            return false;
        }

        return true;
    }
    public void rest_addDepotServiceFields()
    {
        tbSearchDepotService.Text = "";

        ddlDepot.SelectedValue = "0";
        ddlServiceType.SelectedValue = "0";
        ddlBusType.SelectedValue = "0";
        ddlRoute.SelectedValue = "0";
        tbTrips.Text = "";

        tbServiceDuration.Text = "";
        tbDutyDays.Text = "";

        cbRestApplicable.Checked = false;
        tbRestDays.Text = "";
        tbRestDays.Visible = false;
        cbNightAllowed.Checked = false;
        ddlNightAllowedCat.SelectedValue = "0";
        ddlNightAllowedCat.Visible = false;
        cbOvertimeAllowed.Checked = false;
        tbOvertimeHours.Text = "";
        tbOvertimeHours.Visible = false;

        tbDrivers.Text = "";
        tbConductors.Text = "";

        ddlLayout.SelectedValue = "0";
        tbServiceFrom.Text = "";
        tbServiceTo.Text = "";
        cbApplicableFare.Checked = true;
        cbApplicableReservation.Checked = true;

        pnlTripsDepotService.Visible = false;
        pnlViewDepotService.Visible = false;
        pnlUpdateDepotService.Visible = false;
        pnlTimeTableDepotService.Visible = false;
        pnlTimeTableViewDepotService.Visible = false;
        pnlAmenitiesDepotService.Visible = false;
        pnlTimingDepotService.Visible = false;
        pnlAddDepotService.Visible = true;

        ViewState["_actionType"] = "0";
        ViewState["_actionDSVCID"] = "0";
        ViewState["_actionSRTPID"] = "0";
        hfLayoutYNTrip.Value = "0";
        ViewState["_gvTripIndex"] = "X";

    }

    // Update Depot Service
    public bool isValidDepotServiceUpdate()
    {
        int msgcount = 0;
        string msg = "";

        if (_validation.IsValidInteger(tbTripsUp.Text, 1, 2) == false)
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Enter valid no. of trips.<br/>";
        }

        if (int.Parse(tbTripsUp.Text.ToString()) < int.Parse(lblTripsUp.Text.ToString()))
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Enter valid no. of trips. (Please enter more than or equal current to trips)<br/>";
        }

        if (_validation.IsValidInteger(tbServiceDurationUp.Text, 1, 2) == false)
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Enter valid Service Duration (no. of days).<br/>";
        }
        if (_validation.IsValidInteger(tbDutyDaysUp.Text, 1, 2) == false)
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Enter valid no. of duty days.<br/>";
        }
        if (cbRestApplicableUp.Checked == true)
        {
            if (_validation.IsValidInteger(tbRestDaysUp.Text, 1, 2) == false)
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". You selected 'Rest Applicable'. Enter valid no. of rest days.<br/>";
            }
        }
        if (cbNightAllowedUp.Checked == true)
        {
            if (ddlNightAllowedCatUp.SelectedValue == "0")
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". You selected 'Nigth Allowed'. Select Night Allowed Category.<br/>";
            }
        }
        if (cbOvertimeAllowedUp.Checked == true)
        {
            if (_validation.IsValidInteger(tbOvertimeHoursUp.Text, 1, 2) == false)
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". You selected 'Overtime Allowed'. Enter valid no. of overtime hours.<br/>";
            }
        }

        if (_validation.IsValidInteger(tbDriversUp.Text, 1, 2) == false)
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Enter valid no. of Drivers ( At least 1).<br/>";
        }
        if (_validation.IsValidInteger(tbConductorsUp.Text, 1, 2) == false)
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Enter valid no. of Conductors ( At least 1).<br/>";
        }


        string serviceFrom = tbServiceFromUp.Text.ToString();
        string serviceTo = tbServiceToUp.Text.ToString();

        if (serviceFrom.Length > 0)
        {
            if (serviceTo.Length != 10 || serviceFrom.Length != 10)
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Enter valid Service Period (From Date and To Date) in ONLINE-RELATED DETAILS. <br/>";
            }
            else
            {
                try
                {
                    DateTime dtFrom = DateTime.ParseExact(serviceFrom, "dd/MM/yyyy", null/* TODO Change to default(_) if this is not a reference type */);
                    DateTime dtTo = DateTime.ParseExact(serviceTo, "dd/MM/yyyy", null/* TODO Change to default(_) if this is not a reference type */);
                    DateTime todayDate = DateTime.Now.Date;

                    if (dtTo < dtFrom)
                    {
                        msgcount = msgcount + 1;
                        msg = msg + msgcount.ToString() + ". Enter valid Service Period (From Date and To Date) in ONLINE-RELATED DETAILS. 'Service Period From Date' will be greater than Today and 'Service Period From Date' will be less than or equal to 'Service Period To Date'. <br/>";
                    }
                }
                catch (Exception e)
                {
                    msgcount = msgcount + 1;
                    msg = msg + msgcount.ToString() + ". Enter valid Service Period (From Date and To Date) in ONLINE-RELATED DETAILS. <br/>";
                }
            }
        }



        if (msgcount > 0)
        {
            Errormsg(msg);
            return false;
        }

        return true;
    }
    private void updateDepotService(string dsvcId)//M21
    {
        try
        {
            if (isValidDepotServiceUpdate() == false)
            {
                return;
            }

            string trips = tbTripsUp.Text.Trim();

            string serviceDuration = tbServiceDurationUp.Text.Trim();
            string dutyDays = tbDutyDaysUp.Text.Trim();

            string restApplicableYN = "N";
            string restDays = "0";
            if (cbRestApplicableUp.Checked == true)
            {
                restApplicableYN = "Y";
                restDays = tbRestDaysUp.Text.Trim();
            }
            string nightAllowedYN = "N";
            string nightCatgory = "0";
            if (cbNightAllowedUp.Checked == true)
            {
                nightAllowedYN = "Y";
                nightCatgory = ddlNightAllowedCatUp.SelectedValue.ToString();
            }
            string overtimeAllowedYN = "N";
            string overtimeHours = "0";
            if (cbOvertimeAllowedUp.Checked == true)
            {
                overtimeAllowedYN = "Y";
                overtimeHours = tbOvertimeHoursUp.Text.Trim();
            }

            string drivers = tbDriversUp.Text.Trim();
            string conductors = tbConductorsUp.Text.Trim();

            string layoutCode = ddlLayoutUp.SelectedValue.ToString();
            string serviceFrom = tbServiceFromUp.Text.Trim();
            string serviceTo = tbServiceToUp.Text.Trim();

            string fareYN = "N";
            if (cbApplicableFareUp.Checked == true)
            {
                fareYN = "Y";
            }
            string reservationYN = "N";
            if (cbApplicableReservationUp.Checked == true)
            {
                reservationYN = "Y";
            }


            string UpdatedBy = Session["_UserCode"].ToString();
            string IpAddress = HttpContext.Current.Request.UserHostAddress;
            //string action = Session["_action"].ToString();

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_depotservice_update");
            MyCommand.Parameters.AddWithValue("p_dsvcid", Convert.ToInt64(dsvcId));

            MyCommand.Parameters.AddWithValue("p_nooftrips", Convert.ToInt16(trips));

            MyCommand.Parameters.AddWithValue("p_serviceduration", Convert.ToInt16(serviceDuration));
            MyCommand.Parameters.AddWithValue("p_noofdutydays", Convert.ToInt16(dutyDays));
            MyCommand.Parameters.AddWithValue("p_dutyrestapplicableyn", restApplicableYN);
            MyCommand.Parameters.AddWithValue("p_dutyrestdays", Convert.ToInt16(restDays));
            MyCommand.Parameters.AddWithValue("p_nightallowedyn", nightAllowedYN);
            MyCommand.Parameters.AddWithValue("p_night_category", nightCatgory);
            MyCommand.Parameters.AddWithValue("p_overtimeallowedyn", overtimeAllowedYN);
            MyCommand.Parameters.AddWithValue("p_overtime_hours", Convert.ToInt16(overtimeHours));
            MyCommand.Parameters.AddWithValue("p_noofdriver", Convert.ToInt16(drivers));
            MyCommand.Parameters.AddWithValue("p_noofconductor", Convert.ToInt16(conductors));

            MyCommand.Parameters.AddWithValue("p_layoutcode", Convert.ToInt16(layoutCode));
            MyCommand.Parameters.AddWithValue("p_servicestartdate", serviceFrom);
            MyCommand.Parameters.AddWithValue("p_serviceenddate", serviceTo);
            MyCommand.Parameters.AddWithValue("p_fareyn", fareYN);
            MyCommand.Parameters.AddWithValue("p_reservationyn", reservationYN);

            MyCommand.Parameters.AddWithValue("p_updatedby", UpdatedBy);
            MyCommand.Parameters.AddWithValue("p_ipaddress", IpAddress);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows[0]["resultstr"].ToString() == "DONE")
                {
                    Successmsg("Depot Service has been updated successfully.");
                    rest_addDepotServiceFields();
                    getDepotServicesList("");
                }
                else if (dt.Rows[0]["resultstr"].ToString() == "ERROR")
                {
                    Errormsg(commonerror + dt.TableName);
                }
                else
                {
                    Errormsg(commonerror + dt.TableName);
                    _common.ErrorLog("SysAdmDepotService.aspx-0009", dt.TableName);
                }
            }
            else
            {
                Errormsg(commonerror + dt.TableName);
                _common.ErrorLog("SysAdmDepotService.aspx-0010", dt.TableName);
            }
        }
        catch (Exception ex)
        {
            Errormsg(commonerror + ex.ToString());
            _common.ErrorLog("SysAdmDepotService.aspx-0011", ex.Message.ToString());
        }
    }

    // Depot Service change Status
    private void saveDepotServiceStatus(string actionType, string dsvcID)//M9
    {
        try
        {
            string UpdatedBy = Session["_UserCode"].ToString();
            string IpAddress = HttpContext.Current.Request.UserHostAddress;


            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_depotservice_change_status");
            MyCommand.Parameters.AddWithValue("p_actiontype", actionType);
            MyCommand.Parameters.AddWithValue("p_dsvcid", Convert.ToInt64(dsvcID));
            MyCommand.Parameters.AddWithValue("p_updatedby", UpdatedBy);
            MyCommand.Parameters.AddWithValue("p_ipaddress", IpAddress);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows[0]["resultstr"].ToString() == "DONE")
                {
                    Successmsg("Depot Service Status has been changed successfully");
                    getDepotServicesList("");
                }
                else if (dt.Rows[0]["resultstr"].ToString() == "ERROR")
                {
                    Errormsg(commonerror);
                }
                else if (dt.Rows[0]["resultstr"].ToString() == "ACTIVETRIPS")
                {
                    Errormsg("Active Trips are available with this Depot Service. Please Deactive all Trips before deactive the Depot Service.");
                }
                else
                {
                    Errormsg(commonerror);
                    _common.ErrorLog("SysAdmDepotService.aspx-0012", dt.TableName);
                }
            }
            else
            {
                Errormsg(commonerror);
                _common.ErrorLog("SysAdmDepotService.aspx-0013", dt.TableName);
            }
        }
        catch (Exception ex)
        {
            Errormsg(commonerror);
            _common.ErrorLog("SysAdmDepotService.aspx-0014", ex.Message.ToString());
        }
    }

    // Add trips
    public void fillDropDownRoutesStatios(DropDownList ddl, string routeid)//M10
    {
        try
        {
            ddl.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_depotservice_gettrip_stations");
            MyCommand.Parameters.AddWithValue("p_routeid", Convert.ToInt64(routeid));
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                ddl.DataSource = dt;
                ddl.DataTextField = "ston_name";
                ddl.DataValueField = "ston_id";
                ddl.DataBind();
            }
            ddl.Items.Insert(0, "SELECT");
            ddl.Items[0].Value = "0";
            ddl.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddl.Items.Insert(0, "SELECT");
            ddl.Items[0].Value = "0";
            ddl.SelectedIndex = 0;
            _common.ErrorLog("SysAdmDepotService.aspx-0015", ex.Message.ToString());
        }
    }
    private void saveTripWithDepotService(string dsvcid)//M11
    {
        try
        {
            if (isValidTrip() == false)
            {
                return;
            }

            string WeekDays = "";
            if (ViewState["trip_daily_weekly"].ToString().ToUpper() == "W")
            {
                WeekDays = ddlTripDays.SelectedValue.ToString();
            }

            string fromStationId = ddlFromStationTrip.SelectedValue.ToString();
            string[] fromStnId = fromStationId.Split(new char[] { ',' });
            fromStationId = fromStnId[0];
            string fromstateid = fromStnId[1];

            string toStationId = ddlToStationTrip.SelectedValue.ToString();
            string[] toStnId = toStationId.Split(new char[] { ',' });
            toStationId = toStnId[0];
            string tostateid = toStnId[1];

            DateTime dS = DateTime.Parse(tbStartTimeTrip.Text);
            string start_time = dS.ToString("hh:mm tt");

            DateTime dE = DateTime.Parse(tbEndTimeTrip.Text);
            string end_time = dE.ToString("hh:mm tt");

            string direction = ddlDirectTrip.SelectedValue.ToString();
            string onlineYN = ddlOnlineTrip.SelectedValue.ToString();
            string midStationYN = cbMiddleStationOnline.Checked == true ? "Y" : "N";

            string UpdatedBy = Session["_UserCode"].ToString();
            string IpAddress = HttpContext.Current.Request.UserHostAddress;

            string fromStationName = ddlFromStationTrip.SelectedItem.ToString();
            string[] fromStnName = fromStationName.Split(new char[] { ',' });
            fromStationName = fromStnName[0];

            string toStationName = ddlToStationTrip.SelectedItem.ToString();
            string[] toStnName = toStationName.Split(new char[] { ',' });
            toStationName = toStnName[0];

            string viaStationName = "";
            if (ddlviastation.SelectedValue == "0")
            {
                viaStationName = "";
            }
            else
            {
                viaStationName = ddlviastation.SelectedItem.ToString();
                string[] viaStnName = viaStationName.Split(new char[] { ',' });
                viaStationName = " - Via - " + viaStnName[0];
            }

            string tripname = fromStationName + " - to - " + toStationName + viaStationName;

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_depotservice_trip_insert");
            MyCommand.Parameters.AddWithValue("p_dsvcid", Convert.ToInt64(dsvcid));
            MyCommand.Parameters.AddWithValue("p_fr_ston_id", Convert.ToInt64(fromStationId));
            MyCommand.Parameters.AddWithValue("p_to_ston_id", Convert.ToInt64(toStationId));
            MyCommand.Parameters.AddWithValue("p_fr_state_id", Convert.ToInt64(fromstateid));
            MyCommand.Parameters.AddWithValue("p_to_state_id", Convert.ToInt64(tostateid));
            MyCommand.Parameters.AddWithValue("p_tripname", tripname);
            MyCommand.Parameters.AddWithValue("p_starttime", start_time);
            MyCommand.Parameters.AddWithValue("p_endtime", end_time);
            MyCommand.Parameters.AddWithValue("p_direction", direction);
            MyCommand.Parameters.AddWithValue("p_onlineyn", onlineYN);
            MyCommand.Parameters.AddWithValue("p_midstationyn", midStationYN);
            MyCommand.Parameters.AddWithValue("p_updatedby", UpdatedBy);
            MyCommand.Parameters.AddWithValue("p_ipaddress", IpAddress);
            MyCommand.Parameters.AddWithValue("p_weekdays", WeekDays);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows[0]["resultstr"].ToString() == "DONE")
                {
                    Successmsg("Trip has been saved successfully.");
                    getTripList__inDepotService(dsvcid);
                    rest_addTripFields();
                }
                else if (dt.Rows[0]["resultstr"].ToString() == "DEACTIVATED")
                {
                    Errormsg("Cannot save trip in deactivated depot service. Please activate Depot Service before adding trips.");
                }
                else if (dt.Rows[0]["resultstr"].ToString() == "ALREADY")
                {
                    Errormsg("You have already added all the trips offered in the depot service.");
                }
                else if (dt.Rows[0]["resultstr"].ToString() == "NOTONLINE")
                {
                    Errormsg("Please set layout in the depot service for creating an online trip.");
                }
                else if (dt.Rows[0]["resultstr"].ToString() == "ERROR")
                {
                    Errormsg(commonerror);
                }
                else
                {
                    Errormsg(commonerror);
                    _common.ErrorLog("SysAdmDepotService.aspx-0015", dt.TableName);
                }
            }
            else
            {
                Errormsg(commonerror);
                _common.ErrorLog("SysAdmDepotService.aspx-0016", dt.TableName);
            }
        }
        catch (Exception ex)
        {
            Errormsg(commonerror);
            _common.ErrorLog("SysAdmDepotService.aspx-0017", ex.Message.ToString());
        }
    }
    private void getTripList__inDepotService(string dsvcID)//M12
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_depotservice_gettrip");
            MyCommand.Parameters.AddWithValue("p_dsvcid", Convert.ToInt64(dsvcID));
            dt = bll.SelectAll(MyCommand);
            gvTrips.DataSource = dt;
            gvTrips.DataBind();
            if (dt.Rows.Count > 0)
            {
                lblNoTripMsg.Text = "";
            }
            else
            {
                lblNoTripMsg.Text = "Service Trip not available. Please add Trip.";
            }

        }
        catch (Exception ex)
        {
            Errormsg(commonerror);
            _common.ErrorLog("SysAdmDepotService.aspx-0018", ex.Message.ToString());
        }
    }
    public bool isValidTrip()
    {
        int msgcount = 0;
        string msg = "";

        if (ddlFromStationTrip.SelectedValue == "0")
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Select From Station.<br/>";
        }
        if (ddlToStationTrip.SelectedValue == "0")
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Select To Station.<br/>";
        }
        // if (ddlviastation.SelectedValue == "0")
        // {
        //     msgcount = msgcount + 1;
        //     msg = msg + msgcount.ToString() + ". Select Via Station.<br/>";
        // }

        if (ddlFromStationTrip.SelectedValue == ddlToStationTrip.SelectedValue)
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". From Station and To Station can not be same.<br/>";
        }

        try
        {
            DateTime d = DateTime.Parse(tbStartTimeTrip.Text);

            if (d.ToString("hh:mm tt").Length < 8)
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Enter valid Start Time.<br/>";
            }
        }
        catch
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Enter valid Start Time.<br/>";
        }

        try
        {
            DateTime d = DateTime.Parse(tbEndTimeTrip.Text);

            if (d.ToString("hh:mm tt").Length < 8)
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Enter valid End Time.<br/>";
            }
        }
        catch
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Enter valid End Time.<br/>";
        }


        if (ddlDirectTrip.SelectedValue == "0")
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Select Trip Direction.<br/>";
        }
        if (ddlOnlineTrip.SelectedValue == "0")
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Select Online status.<br/>";
        }

        //if(ViewState["trip_daily_weekly"].ToString().ToUpper()=="W"  )
        //{
        //    if (ddlTripDays.SelectedValue == "99")
        //    {
        //        msgcount = msgcount + 1;
        //        msg = msg + msgcount.ToString() + ". Select Week Day.<br/>";
        //    }            
        //}

        if (msgcount > 0)
        {
            Errormsg(msg);
            return false;
        }

        return true;
    }
    public void rest_addTripFields()
    {
        ddlFromStationTrip.SelectedValue = "0";
        ddlToStationTrip.SelectedValue = "0";
        tbStartTimeTrip.Text = "";
        tbEndTimeTrip.Text = "";
        ddlOnlineTrip.SelectedValue = "0";
        ddlDirectTrip.SelectedValue = "0";

    }
    private void saveTripStatus(string gvIndex)//M13
    {
        try
        {
            int index = Convert.ToInt32(gvIndex);
            string strpid = gvTrips.DataKeys[index].Values["strpid"].ToString();

            string dsvcID = gvTrips.DataKeys[index].Values["dsvcid"].ToString();

            CheckBox cbonlineYN = (CheckBox)gvTrips.Rows[index].FindControl("cbOnlineYN");
            CheckBox cbMidStationYN = (CheckBox)gvTrips.Rows[index].FindControl("cbMidStationYN");
            CheckBox cbActiveYN = (CheckBox)gvTrips.Rows[index].FindControl("cbActiveYN");

            string onlineYN = cbonlineYN.Checked == true ? "Y" : "N";
            string midStationYN = cbMidStationYN.Checked == true ? "Y" : "N";
            string activeYN = cbActiveYN.Checked == true ? "A" : "D";

            string UpdatedBy = Session["_UserCode"].ToString();
            string IpAddress = HttpContext.Current.Request.UserHostAddress;

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_depotservice_trip_update");
            MyCommand.Parameters.AddWithValue("p_strpid", Convert.ToInt64(strpid));
            MyCommand.Parameters.AddWithValue("p_dsvcid", Convert.ToInt64(dsvcID));
            MyCommand.Parameters.AddWithValue("p_onlineyn", onlineYN);
            MyCommand.Parameters.AddWithValue("p_midstationyn", midStationYN);
            MyCommand.Parameters.AddWithValue("p_status_ad", activeYN);
            MyCommand.Parameters.AddWithValue("p_updatedby", UpdatedBy);
            MyCommand.Parameters.AddWithValue("p_ipaddress", IpAddress);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows[0]["resultstr"].ToString() == "DONE")
                {
                    Successmsg("Trip Status has been changed successfully");
                    getTripList__inDepotService(dsvcID);
                }
                else if (dt.Rows[0]["resultstr"].ToString() == "DEACTIVATED")
                {
                    Errormsg("Cannot activate trip in deactivated depot service. Please activate Depot Service before activate trips.");
                }
                else if (dt.Rows[0]["resultstr"].ToString() == "ERROR")
                {
                    Errormsg(commonerror);
                }
                else
                {
                    Errormsg(commonerror);
                    _common.ErrorLog("SysAdmDepotService.aspx-0018", dt.TableName);
                }
            }
            else
            {
                Errormsg(commonerror);
                _common.ErrorLog("SysAdmDepotService.aspx-0019", dt.TableName);
            }
        }
        catch (Exception ex)
        {
            Errormsg(commonerror);
            _common.ErrorLog("SysAdmDepotService.aspx-0020", ex.Message.ToString());
        }
    }
    public void getDhabaList(string dsvcID) //M19
    {
        try
        {
            lvDhabas.DataSource = null;
            lvDhabas.DataBind();
            lblNoDhabaMsg.Text = "";

            //MyCommand = new NpgsqlCommand();
            //MyCommand.Parameters.Clear();
            //MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_depotservice_get_timetable_view");
            //MyCommand.Parameters.AddWithValue("p_dsvcid", Convert.ToInt64(dsvcID));
            //dt = bll.SelectAll(MyCommand);

            DataTable table = new DataTable();
            table.Columns.Add("id", typeof(string));
            table.Columns.Add("name", typeof(string));
            table.Columns.Add("address", typeof(string));

            table.Rows.Add("1", "Dhaba 1", "Address dhaba1");
            table.Rows.Add("2", "Dhaba 2", "Address dhaba2");
            table.Rows.Add("3", "Dhaba 3", "Address dhaba3");
            table.Rows.Add("4", "Dhaba 4", "Address dhaba4");
            table.Rows.Add("5", "Dhaba 5", "Address dhaba5");
            table.Rows.Add("6", "Dhaba 6", "Address dhaba6");


            if (table.Rows.Count > 0)
            {
                lvDhabas.DataSource = table;
                lvDhabas.DataBind();
                lblNoDhabaMsg.Text = "";
            }
            else
            {
                lblNoDhabaMsg.Text = "Dhaba list not available";
            }
        }
        catch (Exception ex)
        {
            Errormsg(commonerror);
            _common.ErrorLog("SysAdmDepotService.aspx-0021", ex.Message.ToString());
        }
    }
    public void saveDhaba() //M20
    {
        ArrayList a = new ArrayList();
        foreach (ListViewItem li in lvDhabas.Items)
        {
            Label lblDhabaId = (Label)li.FindControl("lblDhabaId");
            CheckBox cb = (CheckBox)li.FindControl("cbDhaba");
            if (cb.Checked)
            {
                a.Add(lblDhabaId.Text);
            }
        }

        string[] ar = (string[])a.ToArray(typeof(string));
        //if (ar.Length <= 0)
        //{
        //    Errormsg("Please select dhaba");
        //    return;
        //}

        string strpid = lblDhabaStrpID.Text;

        string UpdatedBy = Session["_UserCode"].ToString();
        string IpAddress = HttpContext.Current.Request.UserHostAddress;

        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_depotservice_dhaba_marge");
        MyCommand.Parameters.AddWithValue("p_strpid", Convert.ToInt64(strpid));
        MyCommand.Parameters.AddWithValue("p_dhaba_ids", ar);
        MyCommand.Parameters.AddWithValue("p_updatedby", UpdatedBy);
        MyCommand.Parameters.AddWithValue("p_ipaddress", IpAddress);
        dt = bll.SelectAll(MyCommand);
        if (dt.TableName == "Success")
        {
            if (dt.Rows[0]["resultstr"].ToString() == "DONE")
            {
                Successmsg("Dhaba(s) have been saved successfully");
                mpAddDhaba.Hide();
            }
            else if (dt.Rows[0]["resultstr"].ToString() == "ERROR")
            {
                Errormsg(commonerror + dt.TableName);
            }
            else
            {
                Errormsg(commonerror + dt.TableName);
                _common.ErrorLog("SysAdmDepotService.aspx-0021", dt.TableName);
            }
        }
        else
        {
            Errormsg(commonerror + dt.TableName);
            _common.ErrorLog("SysAdmDepotService.aspx-0022", dt.TableName);
        }


    }
    private void resetControlAddtrips()
    {
        ddlFromStationTrip.Items.Clear();
        ddlToStationTrip.Items.Clear();
        ddlviastation.Items.Clear();

        ddlDirectTrip.SelectedValue = "0";

        ddlFromStationTrip.Items.Insert(0, "SELECT");
        ddlFromStationTrip.Items[0].Value = "0";
        ddlFromStationTrip.SelectedIndex = 0;

        ddlToStationTrip.Items.Insert(0, "SELECT");
        ddlToStationTrip.Items[0].Value = "0";
        ddlToStationTrip.SelectedIndex = 0;

        ddlviastation.Items.Insert(0, "SELECT");
        ddlviastation.Items[0].Value = "0";
        ddlviastation.SelectedIndex = 0;


    }
    private void fill_trip_stations(DropDownList ddl, string dsvcid, string direction, string from_id, string to_id, string via_id)
    {
        try
        {
            ddl.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_depotservice_gettrip_stations_new");
            MyCommand.Parameters.AddWithValue("p_dsvcid", Convert.ToInt64(dsvcid));
            MyCommand.Parameters.AddWithValue("p_direction", direction);
            MyCommand.Parameters.AddWithValue("p_fr_ston_id", from_id);
            MyCommand.Parameters.AddWithValue("p_to_ston_id", to_id);
            MyCommand.Parameters.AddWithValue("p_via_ston_id", via_id);
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                ddl.DataSource = dt;
                ddl.DataTextField = "ston_name";
                ddl.DataValueField = "station_id";
                ddl.DataBind();
            }
            ddl.Items.Insert(0, "SELECT");
            ddl.Items[0].Value = "0";
            ddl.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddl.Items.Insert(0, "SELECT");
            ddl.Items[0].Value = "0";
            ddl.SelectedIndex = 0;

        }
    }


    // Timetable Generate and set station stops
    private DataTable getServiceTripsDataTable(string dsvcID)//M14
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_depotservice_gettrip");
            MyCommand.Parameters.AddWithValue("p_dsvcid", Convert.ToInt64(dsvcID));
            DataTable dtnew = new DataTable();
            dtnew = bll.SelectAll(MyCommand);
            return dtnew;

        }
        catch (Exception ex)
        {
            _common.ErrorLog("SysAdmDepotService.aspx-0023", ex.Message);
            return null;
        }
    }
    public void getStationStops(string dsvcID) //M15
    {
        try
        {
            gvTimeTable.DataSource = null;
            gvTimeTable.DataBind();
            PanelNoRecordTimeTable.Visible = true;
            gvTimeTable.Visible = false;

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            //MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_depotservice_get_timetable");
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_depotservice_get_timetable_n");
            MyCommand.Parameters.AddWithValue("p_dsvcid", Convert.ToInt64(dsvcID));
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {

                gvTimeTable.DataSource = dt;
                gvTimeTable.DataBind();
                PanelNoRecordTimeTable.Visible = false;
                gvTimeTable.Visible = true;
                //if (dt.Rows[0]["already_yn"].ToString().Trim().ToUpper() == "Y")
                //{
                //    DataTable dtTrip = new DataTable();
                //    dtTrip = getServiceTripsDataTable(dsvcID);
                //    string first_trip_idd = dtTrip.Rows[0]["strpid"].ToString();
                //    string first_directionn = dtTrip.Rows[0]["direction"].ToString();

                //    DataTable distinctDT = dt.DefaultView.ToTable(true, "strpid");

                //    if (dtTrip.Rows.Count > distinctDT.Rows.Count)
                //    {
                //        string strArr = "";
                //        for (int index = 0; index <= distinctDT.Rows.Count - 1; index++)
                //            strArr = distinctDT.Rows[index][0].ToString() + "," + strArr;
                //        string strArrr = strArr.Remove(strArr.Length - 1, 1);

                //        DataRow[] result = dtTrip.Select("strpid not in (" + strArrr + ")");
                //        int rsltCount = result.Count();

                //        for (int index = 0; index <= result.Count() - 1; index++)
                //        {
                //            string trip_idd = result[index]["strpid"].ToString();
                //            string directionn = result[index]["direction"].ToString();

                //            DataTable dtSingleTrip = new DataTable();
                //            // create copy of original datatable (structure)
                //            dtSingleTrip = dt.Clone();
                //            int tripId = 1;
                //            DataRow[] drSingleTrip = dt.Select("strpid = '" + first_trip_idd + "'");
                //            foreach (DataRow dtrow in drSingleTrip)
                //            { dtSingleTrip.ImportRow(dtrow); }

                //            if (directionn.Trim().ToUpper() != first_directionn.Trim().ToUpper())
                //            {
                //                dtSingleTrip.DefaultView.Sort = "stationseq DESC ";
                //                dtSingleTrip = dtSingleTrip.DefaultView.ToTable();
                //            }

                //            foreach (DataRow dtrow in dtSingleTrip.Rows)
                //            {
                //                dtrow["tripno"] = distinctDT.Rows.Count + 1;
                //                dtrow["already_yn"] = "N";
                //                dtrow["strpid"] = trip_idd;
                //                dtrow["ston_stop_yn"] = "N";
                //                dtrow["crew_change_yn"] = "N";
                //                dtrow["dhaba_yn"] = "N";
                //                dtrow["onl_ston"] = "N";
                //                dtrow["onl_boarding"] = "N";
                //                dt.ImportRow(dtrow);
                //            }
                //            // gvTimeTable();
                //        }
                //    }

                //    gvTimeTable.DataSource = dt;
                //    gvTimeTable.DataBind();
                //    PanelNoRecordTimeTable.Visible = false;
                //    gvTimeTable.Visible = true;
                //}
                //else
                //{
                //    DataTable dtTrip = new DataTable();
                //    dtTrip = getServiceTripsDataTable(dsvcID);

                //    if (dtTrip.Rows.Count > 0)
                //    {
                //        DataTable dtCopy = new DataTable();
                //        // create copy of original datatable (structure)
                //        dtCopy = dt.Clone();
                //        int tripId = 1;
                //        foreach (DataRow row in dtTrip.Rows)
                //        {

                //            // Dim tripId As String = row("").ToString
                //            string tripDirection = row["direction"].ToString();
                //            if (tripDirection.Trim().ToUpper() == "I")
                //            {
                //                dt.DefaultView.Sort = "stationseq DESC ";
                //                dt = dt.DefaultView.ToTable();
                //            }
                //            else
                //            {
                //                dt.DefaultView.Sort = "stationseq ASC ";
                //                dt = dt.DefaultView.ToTable();
                //            }

                //            foreach (DataRow dtrow in dt.Rows)
                //            {
                //                dtrow["tripno"] = tripId;
                //                dtrow["strpid"] = row["strpid"].ToString();

                //                dtrow["from_ston_id"] = row["fr_stonid"].ToString();
                //                dtrow["to_ston_id"] = row["to_stonid"].ToString();

                //                dtCopy.ImportRow(dtrow);
                //            }
                //            tripId = tripId + 1;
                //        }

                //        gvTimeTable.DataSource = dtCopy;
                //        gvTimeTable.DataBind();

                //        PanelNoRecordTimeTable.Visible = false;
                //        gvTimeTable.Visible = true;
                //    }
                //    else
                //    {
                //        PanelNoRecordTimeTable.Visible = true;
                //        gvTimeTable.Visible = false;
                //    }
                //}
            }
            else
            {
                PanelNoRecordTimeTable.Visible = true;
                gvTimeTable.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Errormsg(commonerror + " - " + ex.ToString());
            _common.ErrorLog("SysAdmDepotService.aspx-0024", ex.Message);
        }
    }
    public DataTable getStationStops_table()
    {

        // Create a new DataTable.
        DataTable dtTimeTable = new DataTable("timeTable");
        dtTimeTable.Columns.Add("tssm_id", typeof(string));
        dtTimeTable.Columns.Add("ston_id", typeof(string));
        dtTimeTable.Columns.Add("station_seq", typeof(string));
        dtTimeTable.Columns.Add("trip_no", typeof(string));
        dtTimeTable.Columns.Add("include_ston", typeof(string));
        dtTimeTable.Columns.Add("crew_change", typeof(string));
        dtTimeTable.Columns.Add("include_dhaba", typeof(string));
        dtTimeTable.Columns.Add("strp_id", typeof(string));
        dtTimeTable.Columns.Add("dsvc_id", typeof(string));
        dtTimeTable.Columns.Add("already_yn", typeof(string));
        dtTimeTable.Columns.Add("onl_ston", typeof(string));
        dtTimeTable.Columns.Add("bor_ston", typeof(string));

        string tssmId, tripNo, stationId, StationName, addStationYN, addDhabaYN, changeCrewYN, strpId, dsvcId, alreadyYN, online_ston, bor_ston;
        int seq = 0;
        // Loop through the GridView and copy rows.
        foreach (GridViewRow row in gvTimeTable.Rows)
        {
            Label lblTssmId = row.FindControl("lblmpGVTssmId") as Label;
            tssmId = lblTssmId.Text;

            Label lblTripNo = row.FindControl("lblmpGVTripNo") as Label;
            tripNo = lblTripNo.Text;

            Label lblStationId = row.FindControl("lblmpGVStationId") as Label;
            stationId = lblStationId.Text;

            Label lblStationName = row.FindControl("lblmpGVStationName") as Label;
            StationName = lblStationName.Text;



            CheckBox cbAddStation = row.FindControl("CheckBoxAddStation") as CheckBox;
            CheckBox cbChangeCrew = row.FindControl("CheckBoxChangeCrew") as CheckBox;
            CheckBox cbMidStation = row.FindControl("CheckBoxMidStation") as CheckBox;
            CheckBox cbborStation = row.FindControl("CheckBoxBoardingStation") as CheckBox;

            Label lblStrpId = row.FindControl("lblmpGVStrpId") as Label;
            strpId = lblStrpId.Text;

            Label lblDsvcId = row.FindControl("lblmpGVDsvcId") as Label;
            dsvcId = lblDsvcId.Text;

            Label lblAlreadyYN = row.FindControl("lblmpGVAlreadyYN") as Label;
            alreadyYN = lblAlreadyYN.Text;

            if (cbAddStation.Checked)
                addStationYN = "Y";
            else
                addStationYN = "N";

            if (cbChangeCrew.Checked)
                changeCrewYN = "Y";
            else
                changeCrewYN = "N";

            addDhabaYN = "N";

            if (cbMidStation.Checked)
                online_ston = "Y";
            else
                online_ston = "N";

            if (cbborStation.Checked)
                bor_ston = "Y";
            else
                bor_ston = "N";

            seq = seq + 1;

            dtTimeTable.Rows.Add(tssmId, stationId, seq.ToString(), tripNo, addStationYN, changeCrewYN, addDhabaYN, strpId, dsvcId, alreadyYN, online_ston, bor_ston);
        }

        return dtTimeTable;
    }
    public void insertStationStops_TimeTable() //M16
    {
        try
        {
            StringWriter swStringWriter = new StringWriter();
            using (swStringWriter)
            {
                var dt = getStationStops_table();
                dt.TableName = "timeTable";
                DataSet ds = new DataSet("Root");
                ds.Tables.Add(dt);
                ds.WriteXml(swStringWriter);

                Label lblDsvcId = gvTimeTable.Rows[0].FindControl("lblmpGVDsvcId") as Label;
                string dsvcId = lblDsvcId.Text;

                string UpdatedBy = Session["_UserCode"].ToString();
                string IpAddress = HttpContext.Current.Request.UserHostAddress;

                MyCommand = new NpgsqlCommand();
                MyCommand.Parameters.Clear();
                MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_depotservice_timetable_insert");
                MyCommand.Parameters.AddWithValue("p_xmltext", swStringWriter.ToString());
                MyCommand.Parameters.AddWithValue("p_dsvcid", Convert.ToInt64(dsvcId));
                MyCommand.Parameters.AddWithValue("p_updatedby", UpdatedBy);
                MyCommand.Parameters.AddWithValue("p_ipaddress", IpAddress);
                dt = bll.SelectAll(MyCommand);
                if (dt.TableName == "Success")
                {
                    if (dt.Rows[0]["resultstr"].ToString() == "DONE")
                    {
                        Successmsg("Timetable has been generated successfully");
                        getStationStops(dsvcId);
                    }
                    else if (dt.Rows[0]["resultstr"].ToString() == "ERROR")
                    {
                        Errormsg(commonerror + dt.TableName);
                    }
                    else
                    {
                        Errormsg(commonerror + dt.TableName);
                        _common.ErrorLog("SysAdmDepotService.aspx-0025", dt.TableName);
                    }
                }
                else
                {
                    Errormsg(commonerror + dt.TableName);
                    _common.ErrorLog("SysAdmDepotService.aspx-0026", dt.TableName);
                }
            }
        }
        catch (Exception ex)
        {
            Errormsg(commonerror + ex.Message.ToString());
            _common.ErrorLog("SysAdmDepotService.aspx-0027", ex.Message);
        }
    }

    // View Timetable
    public void getTimetable(string dsvcID) //M17
    {
        try
        {
            lvTimetableView.DataSource = null;
            lvTimetableView.DataBind();
            PanelNoRecordTimeTableView.Visible = true;
            lvTimetableView.Visible = false;

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_depotservice_get_timetable_view");
            MyCommand.Parameters.AddWithValue("p_dsvcid", Convert.ToInt64(dsvcID));
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                lvTimetableView.DataSource = dt;
                lvTimetableView.DataBind();
                PanelNoRecordTimeTableView.Visible = false;
                lvTimetableView.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Errormsg(commonerror);
            _common.ErrorLog("SysAdmDepotService.aspx-0028", ex.Message);
        }
    }

    // Amenities
    public void getAmenities(string dsvcID, string srtpID) //M18
    {
        try
        {
            lvAmenities.DataSource = null;
            lvAmenities.DataBind();
            PanelNoRecordAmenities.Visible = true;
            lvAmenities.Visible = false;

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_depotservice_aminities_get");
            MyCommand.Parameters.AddWithValue("p_dsvcid", Convert.ToInt64(dsvcID));
            MyCommand.Parameters.AddWithValue("p_srtp_id", Convert.ToInt16(srtpID));

            DataTable table = new DataTable();
            table = bll.SelectAll(MyCommand);

            if (table.Rows.Count > 0)
            {
                lvAmenities.DataSource = table;
                lvAmenities.DataBind();
                PanelNoRecordAmenities.Visible = false;
                lvAmenities.Visible = true;
            }

        }
        catch (Exception ex)
        {
            Errormsg(commonerror);
            _common.ErrorLog("SysAdmDepotService.aspx-0029", ex.Message);
        }
    }
    public void saveAmenities(string dsvcID, string srtpID) //M19
    {
        ArrayList a = new ArrayList();
        foreach (ListViewItem li in lvAmenities.Items)
        {
            Label lblAmenityId = (Label)li.FindControl("lblAmenityId");
            CheckBox cb = (CheckBox)li.FindControl("cbAmenity");
            if (cb.Checked)
            {
                a.Add(lblAmenityId.Text);
            }
        }

        string[] ar = (string[])a.ToArray(typeof(string));
        if (ar.Length <= 0)
        {
            Errormsg("Please select amenities");
            return;
        }


        string UpdatedBy = Session["_UserCode"].ToString();
        string IpAddress = HttpContext.Current.Request.UserHostAddress;

        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_depotservice_aminities_marge");
        MyCommand.Parameters.AddWithValue("p_dsvcid", Convert.ToInt64(dsvcID));
        MyCommand.Parameters.AddWithValue("p_amenity_ids", ar);
        MyCommand.Parameters.AddWithValue("p_updatedby", UpdatedBy);
        MyCommand.Parameters.AddWithValue("p_ipaddress", IpAddress);
        dt = bll.SelectAll(MyCommand);
        if (dt.TableName == "Success")
        {
            if (dt.Rows[0]["resultstr"].ToString() == "DONE")
            {
                Successmsg("Amenities have been saved successfully");
                getAmenities(dsvcID, srtpID);
            }
            else if (dt.Rows[0]["resultstr"].ToString() == "ERROR")
            {
                Errormsg(commonerror + dt.TableName);
            }
            else
            {
                Errormsg(commonerror + dt.TableName);
                _common.ErrorLog("SysAdmDepotService.aspx-0031", dt.TableName);
            }
        }
        else
        {
            Errormsg(commonerror + dt.TableName);
            _common.ErrorLog("SysAdmDepotService.aspx-0030", dt.TableName);

        }


    }

    // Timing
    private void getTripList__inDepotServiceTiming(string dsvcID, string deptTime)//M20
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_depotservice_gettrip");
            MyCommand.Parameters.AddWithValue("p_dsvcid", Convert.ToInt64(dsvcID));
            dt = bll.SelectAll(MyCommand);
            gvTimingTrips.DataSource = dt;
            gvTimingTrips.DataBind();
            if (dt.Rows.Count > 0)
            {
                lblNoTripMsg1.Text = "";
                lblTimingStep2Heading.Visible = true;
                lbtnTimingUpdate.Visible = true;

                try
                {
                    (gvTimingTrips.Rows[0].FindControl("tbStartTimeTrip") as TextBox).Attributes.Remove("type");
                    (gvTimingTrips.Rows[0].FindControl("tbStartTimeTrip") as TextBox).Text = deptTime;
                    (gvTimingTrips.Rows[0].FindControl("tbStartTimeTrip") as TextBox).Enabled = false;
                }
                catch (Exception e)
                {
                    Errormsg(e.ToString());
                }
            }
            else
            {
                lblNoTripMsg1.Text = "Service Trip not available.";
                lblTimingStep2Heading.Visible = false;
                lbtnTimingUpdate.Visible = false;
            }

        }
        catch (Exception ex)
        {
            Errormsg(commonerror + "" + ex);
            _common.ErrorLog("SysAdmDepotService.aspx-0032", ex.Message.ToString());
        }
    }
    public DataTable getNewTiming_table()
    {
        // Create a new DataTable.
        DataTable dtTimeTable = new DataTable("timeTable");
        dtTimeTable.Columns.Add("dsvc_id", typeof(string));
        dtTimeTable.Columns.Add("strp_id", typeof(string));
        dtTimeTable.Columns.Add("start_time", typeof(string));
        dtTimeTable.Columns.Add("end_time", typeof(string));

        foreach (GridViewRow row in gvTimingTrips.Rows)
        {
            string strpid = gvTimingTrips.DataKeys[row.RowIndex]["strpid"].ToString();
            string dsvcid = gvTimingTrips.DataKeys[row.RowIndex]["dsvcid"].ToString();
            TextBox tbStartTimeTrip = row.FindControl("tbStartTimeTrip") as TextBox;
            string start_time = tbStartTimeTrip.Text;

            try
            {
                DateTime d = DateTime.Parse(start_time);
                if (d.ToString("hh:mm tt").Length < 8)
                {
                    Errormsg("Invalid Trip Start/End Time. Please check.");
                    return null;
                }
                start_time = d.ToString("hh:mm tt");
            }
            catch
            {
                Errormsg("Invalid Trip Start/End Time. Please check.");
                return null;
            }

            TextBox tbEndTimeTrip = row.FindControl("tbEndTimeTrip") as TextBox;
            string end_time = tbEndTimeTrip.Text;
            try
            {
                DateTime d = DateTime.Parse(tbEndTimeTrip.Text);
                if (d.ToString("hh:mm tt").Length < 8)
                {
                    Errormsg("Invalid Trip Start/End Time. Please check.");
                    return null;
                }
                end_time = d.ToString("hh:mm tt");
            }
            catch
            {
                Errormsg("Invalid Trip Start/End Time. Please check.");
                return null;
            }

            dtTimeTable.Rows.Add(dsvcid, strpid, start_time, end_time);
        }

        return dtTimeTable;
    }
    public void insertTiming(string dsvcId) //M21
    {
        try
        {
            StringWriter swStringWriter = new StringWriter();
            using (swStringWriter)
            {
                var dt = getNewTiming_table();
                dt.TableName = "timeTable";
                DataSet ds = new DataSet("Root");
                ds.Tables.Add(dt);
                ds.WriteXml(swStringWriter);

                string deptTime = tbNewDepartureTime.Text;
                try
                {
                    DateTime d = DateTime.Parse(deptTime);
                    if (d.ToString("hh:mm tt").Length < 8)
                    {
                        Errormsg("Invalid Depot Service Departure Time.");
                        return;
                    }
                    deptTime = d.ToString("hh:mm tt");
                }
                catch
                {
                    Errormsg("Invalid Depot Service Departure Time.");
                    return;
                }

                string UpdatedBy = Session["_UserCode"].ToString();
                string IpAddress = HttpContext.Current.Request.UserHostAddress;

                MyCommand = new NpgsqlCommand();
                MyCommand.Parameters.Clear();
                MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_depotservice_timing_change");
                MyCommand.Parameters.AddWithValue("p_xmltext", swStringWriter.ToString());
                MyCommand.Parameters.AddWithValue("p_dsvcid", Convert.ToInt64(dsvcId));
                MyCommand.Parameters.AddWithValue("p_dept_time", deptTime);
                MyCommand.Parameters.AddWithValue("p_updatedby", UpdatedBy);
                MyCommand.Parameters.AddWithValue("p_ipaddress", IpAddress);
                dt = bll.SelectAll(MyCommand);
                if (dt.TableName == "Success")
                {
                    if (dt.Rows[0]["resultstr"].ToString() == "DONE")
                    {
                        Successmsg("Timing has been changed successfully");
                        getDepotServiceUpcomingTickets(dsvcId);
                       // getDepotServiceCancelTicket(dsvcId);
                        getDepotServicesList("");
                        getTripList__inDepotServiceTiming(dsvcId, deptTime);
                    }
                    else if (dt.Rows[0]["resultstr"].ToString() == "ERROR")
                    {
                        Errormsg(commonerror + dt.TableName);
                    }
                    else
                    {
                        Errormsg(commonerror + dt.TableName);
                        _common.ErrorLog("SysAdmDepotService.aspx-0033", dt.TableName);
                    }
                }
                else
                {
                    Errormsg(commonerror + dt.TableName);
                    _common.ErrorLog("SysAdmDepotService.aspx-0034", dt.TableName);
                }
            }
        }
        catch (Exception ex)
        {
            Errormsg(commonerror + ex.Message.ToString());
            _common.ErrorLog("SysAdmDepotService.aspx-0035", ex.Message.ToString());
        }
    }
    private void getDepotServiceUpcomingTickets(string dsvcID)//M22
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_depotservice_upcoming_tickets");
            MyCommand.Parameters.AddWithValue("p_dsvc_id", Convert.ToInt64(dsvcID));
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    CommonSMSnEmail sms = new CommonSMSnEmail();
                    sms.dsTimeChange (row["fromstn_name"].ToString(), row["tostn_name"].ToString(), row["journeydate"].ToString() , row["journeydate"].ToString() + " " + row["trip_time"].ToString(), row["traveller_mobile_no"].ToString());
                   
                }
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("SysAdmDepotService.aspx-0036", ex.Message.ToString());
        }
    }
    private void getDepotServiceCancelTicket(string dsvcID)//M23
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_depotservice_spcl_cancellation");
            MyCommand.Parameters.AddWithValue("p_dsvc_id", Convert.ToInt64(dsvcID));
            MyCommand.Parameters.AddWithValue("p_cancel_by", "");
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
               
                foreach (DataRow row in dt.Rows)
                {
                    CommonSMSnEmail sms = new CommonSMSnEmail();
                    sms.ServiceBlockCancel(row["book_by_type_code"].ToString(), row["mobile"].ToString(), row["ticketno"].ToString());                    
                }
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("SysAdmDepotService.aspx-00M23", ex.Message.ToString());
        }
    }

    // messages
    private void Errormsg(string msg)
    {
        string popup = _popup.modalPopupSmall("D", "Please Check", msg, "Close");
        Response.Write(popup);
    }
    private void Successmsg(string msg)
    {
        string popup = _popup.modalPopupSmall("S", "Congratulations", msg, "Close");
        Response.Write(popup);
    }
    private void ConfirmMsg(string msg)
    {
        lblConfirmation.Text = msg;
        mpConfirmation.Show();
    }
    private void InfoMsg(string msg)
    {
        string popup = _popup.modalPopupSmall("I", "Information", msg, "Close");
        Response.Write(popup);
    }
    #endregion

    #region "Events"

    // New Depot Service
    protected void cbRestApplicable_CheckedChanged(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (cbRestApplicable.Checked == true)
        {
            tbRestDays.Visible = true;
        }
        else
        {
            tbRestDays.Visible = false;
        }
    }
    protected void cbNightAllowed_CheckedChanged(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (cbNightAllowed.Checked == true)
        {
            ddlNightAllowedCat.Visible = true;
        }
        else
        {
            ddlNightAllowedCat.Visible = false;
        }
    }
    protected void cbOvertimeAllowed_CheckedChanged(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (cbOvertimeAllowed.Checked == true)
        {
            tbOvertimeHours.Visible = true;
        }
        else
        {
            tbOvertimeHours.Visible = false;
        }
    }
    protected void lbtnSaveNewDepotService_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (isValidDepotService() == false)
        {
            return;
        }
        ViewState["_actionType"] = "S";
        ConfirmMsg("Do you want to save Depot Service ?");
    }
    protected void lbtnResetNewDepotService_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        ViewState["_actionType"] = "0";
        rest_addDepotServiceFields();
    }
    protected void rbtweekly_CheckedChanged(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        ddlDays.Visible = true;
    }

    protected void rbtdaily_CheckedChanged(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        ddlDays.Visible = false;
    }

    // View Depot Service
    protected void lbtnClose_pnlViewDepotService_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        rest_addDepotServiceFields();
    }

    // Update Depot Service
    protected void lbtnClose_pnlUpdateDepotService_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        rest_addDepotServiceFields();
    }
    protected void lbtnUpdateDepotService_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (isValidDepotServiceUpdate() == false)
        {
            return;
        }
        ViewState["_actionType"] = "U";
        ConfirmMsg("Do you want to update Depot Service ?");
    }
    protected void cbRestApplicableUp_CheckedChanged(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (cbRestApplicableUp.Checked == true)
        {
            tbRestDaysUp.Visible = true;
        }
        else
        {
            tbRestDaysUp.Visible = false;
        }
    }
    protected void cbNightAllowedUp_CheckedChanged(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (cbNightAllowedUp.Checked == true)
        {
            ddlNightAllowedCatUp.Visible = true;
        }
        else
        {
            ddlNightAllowedCatUp.Visible = false;
        }
    }
    protected void cbOvertimeAllowedUp_CheckedChanged(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (cbOvertimeAllowedUp.Checked == true)
        {
            tbOvertimeHoursUp.Visible = true;
        }
        else
        {
            tbOvertimeHours.Visible = false;
        }
    }

    // Trips with Depot Service
    protected void lbtnClose_pnlTripsDepotService_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        ViewState["_actionType"] = "0";
        ViewState["_actionDSVCID"] = "0";
        hfLayoutYNTrip.Value = "0";
        ViewState["_gvTripIndex"] = "X";
        rest_addDepotServiceFields();
    }
    protected void lbtnSaveNewTrip_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (isValidTrip() == false)
        {
            return;
        }

        if (hfLayoutYNTrip.Value.ToString() == "N" && ddlOnlineTrip.SelectedValue == "Y")
        {
            Errormsg("Please set layout in the depot service for creating an online trip.");
            return;
        }

        ViewState["_actionType"] = "ST";
        ConfirmMsg("Do you want to save Trip with Depot Service ?");
    }
    protected void gvTrips_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView rowView = (DataRowView)e.Row.DataItem;
            CheckBox cbonlineYN = (CheckBox)e.Row.FindControl("cbOnlineYN");
            CheckBox cbMidStationYN = (CheckBox)e.Row.FindControl("cbMidStationYN");
            CheckBox cbActiveYN = (CheckBox)e.Row.FindControl("cbActiveYN");


            string onlineYN = rowView["onlineyn"].ToString();
            string midStationOnlineYN = rowView["midstation_onlineyn"].ToString();
            string ActiveYN = rowView["statusyn"].ToString();

            if (onlineYN == "Y")
            {
                cbonlineYN.Checked = true;
            }
            else
            {
                cbonlineYN.Checked = false;
            }

            if (midStationOnlineYN == "Y")
            {
                cbMidStationYN.Checked = true;
            }
            else
            {
                cbMidStationYN.Checked = false;
            }

            if (ActiveYN == "A")
            {
                cbActiveYN.Checked = true;
            }
            else
            {
                cbActiveYN.Checked = false;
            }

            if (hfLayoutYNTrip.Value.ToString() == "N")
            {
                cbonlineYN.Checked = false;
                cbonlineYN.Enabled = false;
            }
        }
    }
    protected void gvTrips_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        CsrfTokenValidate();
        ViewState["_gvTripIndex"] = "X";
        if (e.CommandName == "UPDATETRIP")
        {
            int index = Convert.ToInt32(e.CommandArgument);

            //strpid,dsvcid,fr_stonid,fr_stonname,to_stonid,to_stonname,starttime,endtime,direction,pln_km,hill_km,onlineyn,statusyn

            string strpid = gvTrips.DataKeys[index].Values["strpid"].ToString();
            string dsvcID = gvTrips.DataKeys[index].Values["dsvcid"].ToString();

            string current_onlineYN = gvTrips.DataKeys[index].Values["onlineyn"].ToString();
            string current_midstation_onlineYN = gvTrips.DataKeys[index].Values["midstation_onlineyn"].ToString();
            string current_activeYN = gvTrips.DataKeys[index].Values["statusyn"].ToString();

            string from_station = gvTrips.DataKeys[index].Values["fr_stonname"].ToString();
            string to_station = gvTrips.DataKeys[index].Values["to_stonname"].ToString();

            CheckBox cbonlineYN = (CheckBox)gvTrips.Rows[index].FindControl("cbOnlineYN");
            CheckBox cbMidStationYN = (CheckBox)gvTrips.Rows[index].FindControl("cbMidStationYN");
            CheckBox cbActiveYN = (CheckBox)gvTrips.Rows[index].FindControl("cbActiveYN");

            string onlineYN = cbonlineYN.Checked == true ? "Y" : "N";
            string midStationYN = cbMidStationYN.Checked == true ? "Y" : "N";
            string activeYN = cbActiveYN.Checked == true ? "A" : "D";

            ViewState["_gvTripIndex"] = "X";
            if (current_activeYN == activeYN && current_onlineYN == onlineYN && current_midstation_onlineYN == midStationYN)
            {
                return;
            }
            else
            {
                string msg = "";
                msg = "Do you want to Update<br>Trip : " + from_station + " - " + to_station + "";
                if (current_activeYN != activeYN)
                {
                    string curr_active_ = current_activeYN == "A" ? "Activate" : "Deactivate";
                    string active_ = activeYN == "A" ? "Activate" : "Deactivate";
                    msg = msg + "<br>" + curr_active_ + " TO <b>" + active_ + "</b>";
                }
                if (current_onlineYN != onlineYN)
                {
                    string curr_online_ = current_onlineYN == "Y" ? "Online" : "Offline";
                    string online_ = onlineYN == "Y" ? "Online" : "Offline";
                    msg = msg + "<br>" + curr_online_ + " TO <b>" + online_ + "</b>";
                }


                ViewState["_actionType"] = "UT";
                ViewState["_gvTripIndex"] = index.ToString();
                ConfirmMsg(msg);
                return;
            }

        }
        if (e.CommandName == "DHABATRIP")
        {
            int index = Convert.ToInt32(e.CommandArgument);

            //strpid,dsvcid,fr_stonid,fr_stonname,to_stonid,to_stonname,starttime,endtime,direction,pln_km,hill_km,onlineyn,statusyn

            string strpid = gvTrips.DataKeys[index].Values["strpid"].ToString();
            string dsvcID = gvTrips.DataKeys[index].Values["dsvcid"].ToString();

            string from_station = gvTrips.DataKeys[index].Values["fr_stonname"].ToString();
            string to_station = gvTrips.DataKeys[index].Values["to_stonname"].ToString();

            string start_time = gvTrips.DataKeys[index].Values["starttime"].ToString();
            string end_time = gvTrips.DataKeys[index].Values["endtime"].ToString();


            lblDhabaStrpID.Text = strpid;
            lblDhabaHeaderTripDetails.Text = from_station + " - " + to_station + " (" + start_time + ")";
            getDhabaList(dsvcID);
            mpAddDhaba.Show();
            return;

        }

    }
    protected void lbtnSaveDhaba_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        saveDhaba();
    }
    protected void ddlFromStationTrip_SelectedIndexChanged(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        ddlviastation.Items.Insert(0, "SELECT");
        ddlviastation.Items[0].Value = "0";
        ddlviastation.SelectedIndex = 0;

        string id = Session["_DSVCID"].ToString();
        string direction = ddlDirectTrip.SelectedValue.ToString();
        string fr_ston_id = ddlFromStationTrip.SelectedValue.ToString();
        string[] fromStnId = fr_ston_id.Split(new char[] { ',' });
        string fromStationId = fromStnId[0];

        fill_trip_stations(ddlToStationTrip, id, direction, fromStationId, "0", "");
    }
    protected void ddlToStationTrip_SelectedIndexChanged(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        ddlviastation.Items.Insert(0, "SELECT");
        ddlviastation.Items[0].Value = "0";
        ddlviastation.SelectedIndex = 0;

        string id = Session["_DSVCID"].ToString();
        string direction = ddlDirectTrip.SelectedValue.ToString();
        string fr_ston_id = ddlFromStationTrip.SelectedValue.ToString();
        string[] fromStnId = fr_ston_id.Split(new char[] { ',' });
        string fromStationId = fromStnId[0];

        string to_ston_id = ddlToStationTrip.SelectedValue.ToString();
        string[] toStnId = to_ston_id.Split(new char[] { ',' });
        string toStationId = toStnId[0];

        fill_trip_stations(ddlviastation, id, direction, fromStationId, toStationId, "0");
    }
    protected void ddlDirectTrip_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDirectTrip.SelectedValue == "0")
        {
            resetControlAddtrips();
        }
        else
        {
            ddlFromStationTrip.Items.Clear();
            ddlToStationTrip.Items.Clear();
            ddlviastation.Items.Clear();

            ddlFromStationTrip.Items.Insert(0, "SELECT");
            ddlFromStationTrip.Items[0].Value = "0";
            ddlFromStationTrip.SelectedIndex = 0;

            ddlToStationTrip.Items.Insert(0, "SELECT");
            ddlToStationTrip.Items[0].Value = "0";
            ddlToStationTrip.SelectedIndex = 0;

            ddlviastation.Items.Insert(0, "SELECT");
            ddlviastation.Items[0].Value = "0";
            ddlviastation.SelectedIndex = 0;

            string id = Session["_DSVCID"].ToString();
            string direction = ddlDirectTrip.SelectedValue.ToString();
            fill_trip_stations(ddlFromStationTrip, id, direction, "0", "", "");
        }
    }

    // Station Stops and Time table Depot Service
    protected void lbtnClose_pnlTimeTableDepotService_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        rest_addDepotServiceFields();
    }
    protected void gvTimeTable_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblFromStationId = e.Row.FindControl("lblmpGVFromStationId") as Label;
            Label lblToStationId = e.Row.FindControl("lblmpGVToStationId") as Label;

            Label lblAddStation = e.Row.FindControl("lblmpGVAddStation") as Label;
            Label lblAddDhaba = e.Row.FindControl("lblmpGVAddDhaba") as Label;
            Label lblChangeCrew = e.Row.FindControl("lblmpGVChangeCrew") as Label;
            Label lnlGVonl_ston = e.Row.FindControl("lblmpGVMidStation") as Label;
            Label lnlGVbor_ston = e.Row.FindControl("lblmpGVBoardingStation") as Label;

            CheckBox cbAddStation = e.Row.FindControl("CheckBoxAddStation") as CheckBox;
            CheckBox cbMidStation = e.Row.FindControl("CheckBoxMidStation") as CheckBox;
            CheckBox cbChangeCrew = e.Row.FindControl("CheckBoxChangeCrew") as CheckBox;
            CheckBox cbborstation = e.Row.FindControl("CheckBoxBoardingStation") as CheckBox;

            Label lblStationId = e.Row.FindControl("lblmpGVStationId") as Label;

            if (lblAddStation.Text.Trim() == "Y")
                cbAddStation.Checked = true;
            else
                cbAddStation.Checked = false;

            if (lblChangeCrew.Text.Trim() == "Y")
                cbChangeCrew.Checked = true;
            else
                cbChangeCrew.Checked = false;

            if (lnlGVonl_ston.Text.Trim() == "Y")
                cbMidStation.Checked = true;
            else
                cbMidStation.Checked = false;

            if (lnlGVbor_ston.Text.Trim() == "Y")
                cbborstation.Checked = true;
            else
                cbborstation.Checked = false;


            if (lblStationId.Text.ToUpper().Trim() == lblFromStationId.Text.ToUpper().Trim())
            {
                cbborstation.Checked = true;
            }

            if (lblStationId.Text.ToUpper().Trim() == lblFromStationId.Text.ToUpper().Trim() || lblStationId.Text.ToUpper().Trim() == lblToStationId.Text.ToUpper().Trim())
            {
                cbAddStation.Checked = true;
                cbAddStation.Enabled = false;

                cbMidStation.Checked = true;
                cbMidStation.Enabled = false;

                cbborstation.Enabled = false;

            }

            //if (lblStationId.Text.ToUpper().Trim() == lblToStationId.Text.ToUpper().Trim())
            //{
            //    cbborstation.Checked = false;
            //    cbborstation.Enabled = false;
            //}


        }
    }
    protected void CheckBoxAll_CheckedChanged(object sender, EventArgs _e)
    {
        CheckBox ChkBoxHeader = (CheckBox)gvTimeTable.HeaderRow.FindControl("CheckBoxHeaderAll");
        foreach (GridViewRow row in gvTimeTable.Rows)
        {
            CheckBox ChkBoxRows = (CheckBox)row.FindControl("CheckBoxAddStation");
            if (ChkBoxRows.Enabled == true)
            {
                if ((ChkBoxHeader.Checked == true))
                    ChkBoxRows.Checked = true;
                else
                    ChkBoxRows.Checked = false;
            }
        }
    }
    protected void lbtnSaveTimeTable_Click(object sender, System.EventArgs e)
    {
        CsrfTokenValidate();
        insertStationStops_TimeTable();
    }

    // View Time table Depot Service
    protected void lbtnClose_pnlTimeTableViewDepotService_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        rest_addDepotServiceFields();
    }
    protected void lvTimetableView_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ListViewDataItem dataItem = (ListViewDataItem)e.Item;
        DataRowView drv = (DataRowView)dataItem.DataItem;
        if (lvTimetableView.EditItem == null)
        {
            Label lblInTime = (Label)e.Item.FindControl("lblInTime");
            Label lblOutTime = (Label)e.Item.FindControl("lblOutTime");
            if (lblInTime.Text.Length <= 0)
            {
                lblInTime.Text = "Trip Start";
                lblInTime.ForeColor = System.Drawing.Color.Green;
                lblInTime.BackColor = System.Drawing.Color.Yellow;
            }
            else
            {
                lblInTime.Text = "<span class='font-weight-600' style='font-size:.5rem !important'>IN </span>" + lblInTime.Text;
            }
        }
    }

    // Amenities Depot Service
    protected void lbtnClose_pnlAmenitiesDepotService_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        rest_addDepotServiceFields();
    }
    protected void lbtnSaveAmenities_Click(object sender, System.EventArgs e)
    {
        CsrfTokenValidate();
        ArrayList a = new ArrayList();
        foreach (ListViewItem li in lvAmenities.Items)
        {
            Label lblAmenityId = (Label)li.FindControl("lblAmenityId");
            CheckBox cb = (CheckBox)li.FindControl("cbAmenity");
            if (cb.Checked)
            {
                a.Add(lblAmenityId.Text);
            }
        }

        string[] ar = (string[])a.ToArray(typeof(string));
        if (ar.Length <= 0)
        {
            Errormsg("Please select amenities");
            return;
        }

        ViewState["_actionType"] = "SA";
        ConfirmMsg("Do you want to save Amenities with Depot Service ?");
    }

    protected void lvAmenities_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ListViewDataItem dataItem = (ListViewDataItem)e.Item;
        DataRowView drv = (DataRowView)dataItem.DataItem;
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            CheckBox cb = (CheckBox)e.Item.FindControl("cbAmenity");
            Label YN = (Label)e.Item.FindControl("lblamenity_yn");
            if (YN.Text.Trim().ToUpper() == "Y")
            {
                cb.Checked = true;
            }
        }
    }
    // Search Depot Service 
    protected void lbtnSearchDepotService_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        string searchtext = tbSearchDepotService.Text;
        if (searchtext.Length < 3)
        {
            return;
        }

        getDepotServicesList(searchtext);
    }
    protected void lbtnRestSearchDepotService_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        tbSearchDepotService.Text = "";
        getDepotServicesList("");
    }

    // Depot Service Depot List
    protected void gvDepotServices_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        CsrfTokenValidate();
        pnlAddDepotService.Visible = false;
        pnlViewDepotService.Visible = false;
        pnlUpdateDepotService.Visible = false;
        pnlTripsDepotService.Visible = false;
        pnlTimeTableDepotService.Visible = false;
        pnlTimeTableViewDepotService.Visible = false;
        pnlAmenitiesDepotService.Visible = false;
        pnlTimingDepotService.Visible = false;

        ViewState["_actionType"] = "0";
        ViewState["_actionDSVCID"] = "0";
        hfLayoutYNTrip.Value = "0";

        if (e.CommandName == "VIEWDETAILS")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string dsvcID = gvDepotServices.DataKeys[index].Values["dsvcid"].ToString();
            string servicename = gvDepotServices.DataKeys[index].Values["servicename"].ToString();
            string officeid = gvDepotServices.DataKeys[index].Values["officeid"].ToString();
            string srtpid = gvDepotServices.DataKeys[index].Values["srtpid"].ToString();
            string bustype = gvDepotServices.DataKeys[index].Values["bustype"].ToString();
            string routid = gvDepotServices.DataKeys[index].Values["routid"].ToString();
            string nooftrips = gvDepotServices.DataKeys[index].Values["nooftrips"].ToString();
            string depttime = gvDepotServices.DataKeys[index].Values["depttime"].ToString();
            string servicedurationdays = gvDepotServices.DataKeys[index].Values["servicedurationdays"].ToString();
            string noofdutydays = gvDepotServices.DataKeys[index].Values["noofdutydays"].ToString();
            string dutyrestapplicable = gvDepotServices.DataKeys[index].Values["dutyrestapplicable"].ToString();
            string dutyrestdays = gvDepotServices.DataKeys[index].Values["dutyrestdays"].ToString();
            string nightallowedyn = gvDepotServices.DataKeys[index].Values["nightallowedyn"].ToString();
            string nightallowedcat = gvDepotServices.DataKeys[index].Values["nightallowedcat"].ToString();
            string overtimeallowed = gvDepotServices.DataKeys[index].Values["overtimeallowed"].ToString();
            string overtimehours = gvDepotServices.DataKeys[index].Values["overtimehours"].ToString();
            string noofdriver = gvDepotServices.DataKeys[index].Values["noofdriver"].ToString();
            string noofconductor = gvDepotServices.DataKeys[index].Values["noofconductor"].ToString();
            string servicestartdate = gvDepotServices.DataKeys[index].Values["servicestartdate"].ToString();
            string serviceexpiredate = gvDepotServices.DataKeys[index].Values["serviceexpiredate"].ToString();
            string layout_code = gvDepotServices.DataKeys[index].Values["layout_code"].ToString();
            string fare_yn = gvDepotServices.DataKeys[index].Values["fare_yn"].ToString();
            string reservation_yn = gvDepotServices.DataKeys[index].Values["reservation_yn"].ToString();
            string daily_weekly = gvDepotServices.DataKeys[index].Values["daily_weekly"].ToString();
            string weekdaysno = gvDepotServices.DataKeys[index].Values["weekdaysno"].ToString();
            string statusyn = gvDepotServices.DataKeys[index].Values["statusyn"].ToString();

            string officename = gvDepotServices.DataKeys[index].Values["officename"].ToString();
            string servicetypename = gvDepotServices.DataKeys[index].Values["servicetypename"].ToString();
            string bustypename = gvDepotServices.DataKeys[index].Values["bustypename"].ToString();
            string routename = gvDepotServices.DataKeys[index].Values["routename"].ToString();
            string layoutname = gvDepotServices.DataKeys[index].Values["layoutname"].ToString();


            lblDepotServicevw.Text = servicename;
            lblRoutevw.Text = routename;
            lblDepotvw.Text = officename;
            lblServiceTypevw.Text = servicetypename;
            lblBusTypevw.Text = bustypename;
            lblTripsvw.Text = nooftrips;

            lblDepttimevw.Text = depttime;

            lblServiceDurationvw.Text = servicedurationdays;
            lblDutyDaysvw.Text = noofdutydays;

            dutyrestapplicable = dutyrestapplicable == "Y" ? "Yes" : "NO";

            lblResApplicablevw.Text = dutyrestapplicable + " - " + dutyrestdays + " Days";
            nightallowedyn = nightallowedyn == "Y" ? "Yes" : "NO";
            lblNightAllowedvw.Text = nightallowedyn + " - " + nightallowedcat;
            overtimeallowed = overtimeallowed == "Y" ? "Yes" : "NO";
            lblOvertimeAllowedvw.Text = overtimeallowed + " - " + overtimehours + " Hours";
            lblDriversvw.Text = noofdriver;
            lblConductorsvw.Text = noofconductor;

            lblServicePeriodvw.Text = servicestartdate + " - " + serviceexpiredate;
            lblLayoutvw.Text = layoutname;

            fare_yn = fare_yn == "Y" ? "Yes" : "NO";
            lblApplicableFarevw.Text = fare_yn;
            reservation_yn = reservation_yn == "Y" ? "Yes" : "NO";
            lblApplicableReservationvw.Text = reservation_yn;

            statusyn = statusyn == "A" ? "Activate" : "Deactivate";
            lblStatusvw.Text = statusyn;

if (daily_weekly == "D")
            {
                lblRunningAsVw.Text = "Daily";
            }
            else if (daily_weekly == "W")
            {
                lblRunningAsVw.Text = "Weekly";
            }
            string daynames = "";
            if (!String.IsNullOrEmpty(weekdaysno))
            {
                
                string Items = weekdaysno;
                string[] TypeCode;
                if (!String.IsNullOrEmpty(Items))
                {
                    TypeCode = Items.Split(',');

                    foreach (ListItem item in ddlDays.Items)
                    {
                       
                        foreach (string str in TypeCode)
                        {
                            if (item.Value == str)
                            {
                                daynames = daynames + ", " + item.Text;
                            }
                            
                        }
                    }
                }
            }
            lblRunningAsVw.Text = lblRunningAsVw.Text +" "+ daynames.TrimStart(',');
            pnlViewDepotService.Visible = true;
        }
        if (e.CommandName == "TRIPS")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string dsvcID = gvDepotServices.DataKeys[index].Values["dsvcid"].ToString();
            string servicename = gvDepotServices.DataKeys[index].Values["servicename"].ToString();
            string routid = gvDepotServices.DataKeys[index].Values["routid"].ToString();
            string nooftrips = gvDepotServices.DataKeys[index].Values["nooftrips"].ToString();
            string depttime = gvDepotServices.DataKeys[index].Values["depttime"].ToString();
            string statusyn = gvDepotServices.DataKeys[index].Values["statusyn"].ToString();
            string officename = gvDepotServices.DataKeys[index].Values["officename"].ToString();
            string layout_code = gvDepotServices.DataKeys[index].Values["layout_code"].ToString();

            ViewState["_actionDSVCID"] = dsvcID;
            lblDepotServiceTrip.Text = servicename;
            lblDepotTrip.Text = officename;
            lblTripsTrip.Text = nooftrips;
            lblDepttimeTrip.Text = depttime;
            statusyn = statusyn == "A" ? "Activate" : "Deactivate";
            lblStatusTrip.Text = statusyn;

            if (layout_code == "0")
            {
                hfLayoutYNTrip.Value = "N";
            }
            else
            {
                hfLayoutYNTrip.Value = "Y";
            }
            resetControlAddtrips();
            fillDropDownRoutesStatios(ddlFromStationTrip, routid);
            fillDropDownRoutesStatios(ddlToStationTrip, routid);
            Session["_DSVCID"] = dsvcID;
            getTripList__inDepotService(dsvcID);
            rest_addTripFields();
	
            string daily_weekly = gvDepotServices.DataKeys[index].Values["daily_weekly"].ToString();
            string weekdaysno = gvDepotServices.DataKeys[index].Values["weekdaysno"].ToString();
            ViewState["trip_daily_weekly"] = daily_weekly;
            ddlTripDays.Items.Clear();
            pnlTripDays .Visible = false;
            if (daily_weekly == "W")
            {
                DataTable dt_days = new DataTable();
                dt_days.Columns.Add("value", typeof(int));
                dt_days.Columns.Add("text", typeof(string));
                string[] days = weekdaysno.Split(',');
                for (int i = 0; i < days.Length; i++)
                {
                    string val1 = days[i].ToString();
                    switch (val1)
                    {
                        case "1":
                            dt_days.Rows.Add(1, "Monday");
                            break;
                        case "2":
                            dt_days.Rows.Add(2, "Tuesday");
                            break;
                        case "3":
                            dt_days.Rows.Add(3, "Wednesday");
                            break;
                        case "4":
                            dt_days.Rows.Add(4, "Thursday");
                            break;
                        case "5":
                            dt_days.Rows.Add(5, "Friday");
                            break;
                        case "6":
                            dt_days.Rows.Add(6, "Saturday");
                            break;
                        case "0":
                            dt_days.Rows.Add(0, "Sunday");
                            break;
                        default: break;
                    }
                }
                ddlTripDays.DataSource = dt_days;
                ddlTripDays.DataTextField = "text";
                ddlTripDays.DataValueField = "value";
                ddlTripDays.DataBind();
                ddlTripDays.Items.Insert(0, "All");
                ddlTripDays.Items[0].Value = "99";
                ddlTripDays.SelectedValue = "99";
                
                pnlTripDays.Visible = true;
            }
                
            try
            {
                DateTime d = DateTime.Parse(depttime);
                tbStartTimeTrip.Text = d.ToString("HH:mm");
            }
            catch
            {
            }


            pnlTripsDepotService.Visible = true;
        }
        if (e.CommandName == "UPDATEDETAILS")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string dsvcID = gvDepotServices.DataKeys[index].Values["dsvcid"].ToString();
            string servicename = gvDepotServices.DataKeys[index].Values["servicename"].ToString();
            string officeid = gvDepotServices.DataKeys[index].Values["officeid"].ToString();
            string srtpid = gvDepotServices.DataKeys[index].Values["srtpid"].ToString();
            string bustype = gvDepotServices.DataKeys[index].Values["bustype"].ToString();
            string routid = gvDepotServices.DataKeys[index].Values["routid"].ToString();
            string nooftrips = gvDepotServices.DataKeys[index].Values["nooftrips"].ToString();
            string depttime = gvDepotServices.DataKeys[index].Values["depttime"].ToString();
            string servicedurationdays = gvDepotServices.DataKeys[index].Values["servicedurationdays"].ToString();
            string noofdutydays = gvDepotServices.DataKeys[index].Values["noofdutydays"].ToString();
            string dutyrestapplicable = gvDepotServices.DataKeys[index].Values["dutyrestapplicable"].ToString();
            string dutyrestdays = gvDepotServices.DataKeys[index].Values["dutyrestdays"].ToString();
            string nightallowedyn = gvDepotServices.DataKeys[index].Values["nightallowedyn"].ToString();
            string nightallowedcat = gvDepotServices.DataKeys[index].Values["nightallowedcat"].ToString();
            string overtimeallowed = gvDepotServices.DataKeys[index].Values["overtimeallowed"].ToString();
            string overtimehours = gvDepotServices.DataKeys[index].Values["overtimehours"].ToString();
            string noofdriver = gvDepotServices.DataKeys[index].Values["noofdriver"].ToString();
            string noofconductor = gvDepotServices.DataKeys[index].Values["noofconductor"].ToString();
            string servicestartdate = gvDepotServices.DataKeys[index].Values["servicestartdate"].ToString();
            string serviceexpiredate = gvDepotServices.DataKeys[index].Values["serviceexpiredate"].ToString();
            string layout_code = gvDepotServices.DataKeys[index].Values["layout_code"].ToString();
            string fare_yn = gvDepotServices.DataKeys[index].Values["fare_yn"].ToString();
            string reservation_yn = gvDepotServices.DataKeys[index].Values["reservation_yn"].ToString();
            string daily_weekly = gvDepotServices.DataKeys[index].Values["daily_weekly"].ToString();
            string weekdaysno = gvDepotServices.DataKeys[index].Values["weekdaysno"].ToString();
            string statusyn = gvDepotServices.DataKeys[index].Values["statusyn"].ToString();

            string officename = gvDepotServices.DataKeys[index].Values["officename"].ToString();
            string servicetypename = gvDepotServices.DataKeys[index].Values["servicetypename"].ToString();
            string bustypename = gvDepotServices.DataKeys[index].Values["bustypename"].ToString();
            string routename = gvDepotServices.DataKeys[index].Values["routename"].ToString();
            string layoutname = gvDepotServices.DataKeys[index].Values["layoutname"].ToString();

            ViewState["_actionDSVCID"] = dsvcID;
            lblDepotServiceUp.Text = servicename;
            lblRouteUp.Text = routename;
            lblDepotUp.Text = officename;
            lblServiceTypeUp.Text = servicetypename;
            lblBusTypeUp.Text = bustypename;
            lblTripsUp.Text = nooftrips;
            tbTripsUp.Text = nooftrips;
            lblDepttimeUp.Text = depttime;
            statusyn = statusyn == "A" ? "Activate" : "Deactivate";
            lblStatusUp.Text = statusyn;

            tbServiceDurationUp.Text = servicedurationdays;
            tbDutyDaysUp.Text = noofdutydays;

            if (dutyrestapplicable == "Y")
            {
                cbRestApplicableUp.Checked = true;
                tbRestDaysUp.Text = dutyrestdays;
                tbRestDaysUp.Visible = true;
            }
            else
            {
                cbRestApplicableUp.Checked = false;
                tbRestDaysUp.Text = "";
                tbRestDaysUp.Visible = false;
            }
            if (nightallowedyn == "Y")
            {
                cbNightAllowedUp.Checked = true;
                ddlNightAllowedCatUp.SelectedValue = nightallowedcat;
                ddlNightAllowedCatUp.Visible = true;
            }
            else
            {
                cbNightAllowedUp.Checked = false;
                ddlNightAllowedCatUp.SelectedValue = "0";
                ddlNightAllowedCatUp.Visible = false;
            }
            if (overtimeallowed == "Y")
            {
                cbOvertimeAllowedUp.Checked = true;
                tbOvertimeHoursUp.Text = dutyrestdays;
                tbOvertimeHoursUp.Visible = true;
            }
            else
            {
                cbOvertimeAllowedUp.Checked = false;
                tbOvertimeHoursUp.Text = "";
                tbOvertimeHoursUp.Visible = false;
            }

            tbDriversUp.Text = noofdriver;
            tbConductorsUp.Text = noofconductor;


            ddlLayoutUp.SelectedValue = layout_code;
            tbServiceFromUp.Text = servicestartdate;
            tbServiceToUp.Text = serviceexpiredate;


            pnlUpdateDepotService.Visible = true;
        }
        if (e.CommandName == "TIMETABLE")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string dsvcID = gvDepotServices.DataKeys[index].Values["dsvcid"].ToString();
            string servicename = gvDepotServices.DataKeys[index].Values["servicename"].ToString();
            string officeid = gvDepotServices.DataKeys[index].Values["officeid"].ToString();
            string srtpid = gvDepotServices.DataKeys[index].Values["srtpid"].ToString();
            string bustype = gvDepotServices.DataKeys[index].Values["bustype"].ToString();
            string routid = gvDepotServices.DataKeys[index].Values["routid"].ToString();
            string nooftrips = gvDepotServices.DataKeys[index].Values["nooftrips"].ToString();
            string depttime = gvDepotServices.DataKeys[index].Values["depttime"].ToString();
            string servicedurationdays = gvDepotServices.DataKeys[index].Values["servicedurationdays"].ToString();
            string noofdutydays = gvDepotServices.DataKeys[index].Values["noofdutydays"].ToString();
            string dutyrestapplicable = gvDepotServices.DataKeys[index].Values["dutyrestapplicable"].ToString();
            string dutyrestdays = gvDepotServices.DataKeys[index].Values["dutyrestdays"].ToString();
            string nightallowedyn = gvDepotServices.DataKeys[index].Values["nightallowedyn"].ToString();
            string nightallowedcat = gvDepotServices.DataKeys[index].Values["nightallowedcat"].ToString();
            string overtimeallowed = gvDepotServices.DataKeys[index].Values["overtimeallowed"].ToString();
            string overtimehours = gvDepotServices.DataKeys[index].Values["overtimehours"].ToString();
            string noofdriver = gvDepotServices.DataKeys[index].Values["noofdriver"].ToString();
            string noofconductor = gvDepotServices.DataKeys[index].Values["noofconductor"].ToString();
            string servicestartdate = gvDepotServices.DataKeys[index].Values["servicestartdate"].ToString();
            string serviceexpiredate = gvDepotServices.DataKeys[index].Values["serviceexpiredate"].ToString();
            string layout_code = gvDepotServices.DataKeys[index].Values["layout_code"].ToString();
            string fare_yn = gvDepotServices.DataKeys[index].Values["fare_yn"].ToString();
            string reservation_yn = gvDepotServices.DataKeys[index].Values["reservation_yn"].ToString();
            string daily_weekly = gvDepotServices.DataKeys[index].Values["daily_weekly"].ToString();
            string weekdaysno = gvDepotServices.DataKeys[index].Values["weekdaysno"].ToString();
            string statusyn = gvDepotServices.DataKeys[index].Values["statusyn"].ToString();

            string officename = gvDepotServices.DataKeys[index].Values["officename"].ToString();
            string servicetypename = gvDepotServices.DataKeys[index].Values["servicetypename"].ToString();
            string bustypename = gvDepotServices.DataKeys[index].Values["bustypename"].ToString();
            string routename = gvDepotServices.DataKeys[index].Values["routename"].ToString();
            string layoutname = gvDepotServices.DataKeys[index].Values["layoutname"].ToString();


            lblDepotServiceTT.Text = servicename;
            lblRouteTT.Text = routename;
            lblDepotTT.Text = officename;
            lblServiceTypeTT.Text = servicetypename;
            lblBusTypeTT.Text = bustypename;
            lblTripsTT.Text = nooftrips;

            lblDepttimeTT.Text = depttime;

            statusyn = statusyn == "A" ? "Activate" : "Deactivate";
            lblStatusTT.Text = statusyn;

            getStationStops(dsvcID);

            pnlTimeTableDepotService.Visible = true;
        }
        if (e.CommandName == "VIEWTIMETABLE")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string dsvcID = gvDepotServices.DataKeys[index].Values["dsvcid"].ToString();
            string servicename = gvDepotServices.DataKeys[index].Values["servicename"].ToString();
            string nooftrips = gvDepotServices.DataKeys[index].Values["nooftrips"].ToString();
            string depttime = gvDepotServices.DataKeys[index].Values["depttime"].ToString();

            string statusyn = gvDepotServices.DataKeys[index].Values["statusyn"].ToString();
            string officename = gvDepotServices.DataKeys[index].Values["officename"].ToString();
            string servicetypename = gvDepotServices.DataKeys[index].Values["servicetypename"].ToString();
            string bustypename = gvDepotServices.DataKeys[index].Values["bustypename"].ToString();
            string routename = gvDepotServices.DataKeys[index].Values["routename"].ToString();
            string layoutname = gvDepotServices.DataKeys[index].Values["layoutname"].ToString();


            lblDepotServiceTTV.Text = servicename;
            lblRouteTTV.Text = routename;
            lblDepotTTV.Text = officename;
            lblServiceTypeTTV.Text = servicetypename;
            lblBusTypeTTV.Text = bustypename;
            lblTripsTTV.Text = nooftrips;

            lblDepttimeTTV.Text = depttime;

            statusyn = statusyn == "A" ? "Activate" : "Deactivate";
            lblStatusTTV.Text = statusyn;

            getTimetable(dsvcID);

            pnlTimeTableViewDepotService.Visible = true;
        }
        if (e.CommandName == "AMENITIES")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string dsvcID = gvDepotServices.DataKeys[index].Values["dsvcid"].ToString();
            string srtpID = gvDepotServices.DataKeys[index].Values["srtpid"].ToString();


            string servicename = gvDepotServices.DataKeys[index].Values["servicename"].ToString();
            string nooftrips = gvDepotServices.DataKeys[index].Values["nooftrips"].ToString();
            string depttime = gvDepotServices.DataKeys[index].Values["depttime"].ToString();

            string statusyn = gvDepotServices.DataKeys[index].Values["statusyn"].ToString();
            string officename = gvDepotServices.DataKeys[index].Values["officename"].ToString();
            string servicetypename = gvDepotServices.DataKeys[index].Values["servicetypename"].ToString();
            string bustypename = gvDepotServices.DataKeys[index].Values["bustypename"].ToString();
            string routename = gvDepotServices.DataKeys[index].Values["routename"].ToString();
            string layoutname = gvDepotServices.DataKeys[index].Values["layoutname"].ToString();

            ViewState["_actionDSVCID"] = dsvcID;
            ViewState["_actionSRTPID"] = srtpID;
            lblDepotServiceA.Text = servicename;
            lblRouteA.Text = routename;
            lblDepotA.Text = officename;
            lblServiceTypeA.Text = servicetypename;
            lblBusTypeA.Text = bustypename;
            lblTripsA.Text = nooftrips;

            lblDepttimeA.Text = depttime;

            statusyn = statusyn == "A" ? "Activate" : "Deactivate";
            lblStatusA.Text = statusyn;

            getAmenities(dsvcID, srtpID);

            pnlAmenitiesDepotService.Visible = true;
        }
        if (e.CommandName == "ACTIVATE")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string dsvcID = gvDepotServices.DataKeys[index].Values["dsvcid"].ToString();
            ViewState["_actionDSVCID"] = dsvcID;
            string servicename = gvDepotServices.DataKeys[index].Values["servicename"].ToString();
            ViewState["_actionType"] = "A";
            ConfirmMsg("Do you want to Activate<br><b>" + servicename + "</b> ?");
            pnlAddDepotService.Visible = true;
        }
        if (e.CommandName == "DEACTIVATE")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string dsvcID = gvDepotServices.DataKeys[index].Values["dsvcid"].ToString();
            ViewState["_actionDSVCID"] = dsvcID;
            string servicename = gvDepotServices.DataKeys[index].Values["servicename"].ToString();
            ViewState["_actionType"] = "D";
            ConfirmMsg("Do you want to Deactivate<br><b>" + servicename + "</b> ?");
            pnlAddDepotService.Visible = true;
        }

        if (e.CommandName == "TIMING")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string dsvcID = gvDepotServices.DataKeys[index].Values["dsvcid"].ToString();
            string servicename = gvDepotServices.DataKeys[index].Values["servicename"].ToString();
            string officeid = gvDepotServices.DataKeys[index].Values["officeid"].ToString();
            string srtpid = gvDepotServices.DataKeys[index].Values["srtpid"].ToString();
            string bustype = gvDepotServices.DataKeys[index].Values["bustype"].ToString();
            string routid = gvDepotServices.DataKeys[index].Values["routid"].ToString();
            string nooftrips = gvDepotServices.DataKeys[index].Values["nooftrips"].ToString();
            string depttime = gvDepotServices.DataKeys[index].Values["depttime"].ToString();
            string statusyn = gvDepotServices.DataKeys[index].Values["statusyn"].ToString();
            string officename = gvDepotServices.DataKeys[index].Values["officename"].ToString();
            string servicetypename = gvDepotServices.DataKeys[index].Values["servicetypename"].ToString();
            string bustypename = gvDepotServices.DataKeys[index].Values["bustypename"].ToString();
            string routename = gvDepotServices.DataKeys[index].Values["routename"].ToString();

            lblTimingName.Text = servicename;
            lblTimingRoute.Text = routename;
            lblTimingDepot.Text = officename;
            lblTimingServiceType.Text = servicetypename;
            lblTimingBusType.Text = bustypename;
            lblTimingtrips.Text = nooftrips;
            lblTimingDepartureTime.Text = depttime;
            statusyn = statusyn == "A" ? "Activate" : "Deactivate";
            lblTimingStatus.Text = statusyn;
            ViewState["_actionDSVCID"] = dsvcID;
            tbNewDepartureTime.Text = "";
            try
            {
                DateTime d = DateTime.Parse(depttime);
                tbStartTimeTrip.Text = d.ToString("HH:mm");
            }
            catch
            {
            }


            gvTimingTrips.DataSource = new DataTable();
            gvTimingTrips.DataBind();
            tbNewDepartureTime.Text = "";
            tbNewDepartureTime.Enabled = true;
            lblTimingStep2Heading.Visible = false;
            lbtnTimingUpdate.Visible = false;
            lbtnTimingProceed.Text = "PROCEED";


            pnlTimingDepotService.Visible = true;
        }

    }
    protected void gvDepotServices_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbtnActivate = (LinkButton)e.Row.FindControl("lbtnActivate");
            LinkButton lbtnDiscontinue = (LinkButton)e.Row.FindControl("lbtnDiscontinue");
            DataRowView rowView = (DataRowView)e.Row.DataItem;
            lbtnActivate.Visible = false;
            lbtnDiscontinue.Visible = false;
            if (rowView["statusyn"].ToString() == "A")
            {
                lbtnDiscontinue.Visible = true;
            }
            else if (rowView["statusyn"].ToString() == "D")
            {
                lbtnActivate.Visible = true;
            }
        }
    }
    protected void gvRoutes_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDepotServices.PageIndex = e.NewPageIndex;
        getDepotServicesList(tbSearchDepotService.Text);
        rest_addDepotServiceFields();
    }


    // All Action Confirmation Yes
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        string actionType = ViewState["_actionType"].ToString();
        if (actionType == "0")
        {
            return;
        }
        else if (actionType == "S")
        {
            saveDepotService();
            return;
        }
        else if (actionType == "U")
        {
            string dsvcID = ViewState["_actionDSVCID"].ToString();
            if (dsvcID == "0")
            {
                Errormsg(commonerror);
                return;
            }
            updateDepotService(dsvcID);
            return;
        }
        else if (actionType == "A" || actionType == "D")
        {
            string dsvcID = ViewState["_actionDSVCID"].ToString();
            if (dsvcID == "0")
            {
                Errormsg(commonerror);
                return;
            }
            saveDepotServiceStatus(actionType, dsvcID);
            return;
        }
        else if (actionType == "ST")
        {
            string dsvcID = ViewState["_actionDSVCID"].ToString();
            if (dsvcID == "0")
            {
                Errormsg(commonerror);
                return;
            }
            saveTripWithDepotService(dsvcID);
            return;
        }
        else if (actionType == "UT")
        {
            string gvIndex = ViewState["_gvTripIndex"].ToString();
            if (gvIndex == "X")
            {
                Errormsg(commonerror);
                return;
            }
            saveTripStatus(gvIndex);
            return;
        }
        else if (actionType == "SA")
        {
            string dsvcID = ViewState["_actionDSVCID"].ToString();
            string srtpID = ViewState["_actionSRTPID"].ToString();

            if (dsvcID == "0")
            {
                Errormsg(commonerror);
                return;
            }
            saveAmenities(dsvcID, srtpID);
            return;
        }
        else if (actionType == "TIMCHNG")
        {
            string dsvcID = ViewState["_actionDSVCID"].ToString();

            if (dsvcID == "0")
            {
                Errormsg(commonerror);
                return;
            }
            insertTiming(dsvcID);
            return;
        }
        else
        {
            return;
        }
    }

    // Other 
    protected void lbtnview_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        string msg = "";
        msg = msg + "1. Here you can Add Depot Service.<br/>";
        msg = msg + "2. Here you can Update Depot Service details.<br/>";
        msg = msg + "3. Here you can Add Trips with Depot Service.<br/>";
        msg = msg + "4. Here you can Add Stops .<br/>";
        InfoMsg(msg);
    }

    // Timing


    protected void lbtnTimingProceed_Click(object sender, System.EventArgs e)
    {
        if (lbtnTimingProceed.Text.ToUpper() == "PROCEED")
        {
            string dsvcID = ViewState["_actionDSVCID"].ToString();
            if (dsvcID == "0")
            {
                Errormsg("Something went wrong. Please try again");
                return;
            }

            string newDeptTime = tbNewDepartureTime.Text;
            try
            {
                DateTime d = DateTime.Parse(newDeptTime);
                if (d.ToString("hh:mm tt").Length < 8)
                {
                    Errormsg("Please enter valid Departure Time.");
                    return;
                }
                newDeptTime = d.ToString("hh:mm tt");
            }
            catch
            {
                Errormsg("Please enter valid Departure Time.");
                return;
            }

            tbNewDepartureTime.Enabled = false;
            getTripList__inDepotServiceTiming(dsvcID, newDeptTime);
            lbtnTimingProceed.Text = "CHANGE";
        }
        else if (lbtnTimingProceed.Text.ToUpper() == "CHANGE")
        {
            gvTimingTrips.DataSource = new DataTable();
            gvTimingTrips.DataBind();
            tbNewDepartureTime.Text = "";
            tbNewDepartureTime.Enabled = true;
            lblTimingStep2Heading.Visible = false;
            lbtnTimingUpdate.Visible = false;
            lbtnTimingProceed.Text = "PROCEED";
        }

    }

    protected void lbtnTimingUpdate_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        string newDeptTime = tbNewDepartureTime.Text;
        try
        {
            DateTime d = DateTime.Parse(newDeptTime);
            if (d.ToString("hh:mm tt").Length < 8)
            {
                Errormsg("Please enter valid Departure Time.");
                return;
            }
            newDeptTime = d.ToString("hh:mm tt");
        }
        catch
        {
            Errormsg("Please enter valid Departure Time.");
            return;
        }

        DataTable dt = getNewTiming_table();
        if (dt == null)
        {

            string dsvcID = ViewState["_actionDSVCID"].ToString();
            if (dsvcID == "0")
            {
                Errormsg(commonerror);
                return;
            }
            getTripList__inDepotServiceTiming(dsvcID, newDeptTime);

            return;
        }

        ViewState["_actionType"] = "TIMCHNG";
        ConfirmMsg("Do you want to save Depot Service Timing ?");

    }

    #endregion

}