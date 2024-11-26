using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class Auth_helpdesk : System.Web.UI.Page
{
    private sbCommonFunc _common = new sbCommonFunc();
    sbValidation _SecurityCheck = new sbValidation();
    NpgsqlCommand MyCommand;
    sbBLL bll = new sbBLL();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    sbSecurity _security = new sbSecurity();
    CommonSMSnEmail sms = new CommonSMSnEmail();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (_security.isSessionExist(Session["_UserCode"]) == true)
        {

            Session["_moduleName"] = "Help Desk";
            if (!IsPostBack)
            {
                Random randomclass = new Random();
                Session["rndNoCheck"] = randomclass.Next().ToString();
                hidtoken.Value = Session["rndNoCheck"].ToString();
                loadfaq("TICKET");
                loadBusServiceType();
                getAdvanceDaysBooking();
                loadOfclvl();
            }
        }
        else
        {
            Response.Redirect("sessiontimeout.aspx");
            return;

        }
    }


    #region"Methods"
    private void CsrfTokenValidate()
    {
        if (hidtoken.Value != Session["rndNoCheck"].ToString())
        {
            Response.Redirect("errorpage.aspx");
            return;
        }
    }
    private void Errormsg(string msg)
    {
        string popup = _popup.modalPopupSmall("W", "Information", msg, "Close");
        Response.Write(popup);
    }
    private void loadOfclvl()
    {
        try
        {
            DataTable dt = new DataTable();
            ddlOffce.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_officelvl");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlOffce.DataSource = dt;
                    ddlOffce.DataTextField = "ofclvl_name";
                    ddlOffce.DataValueField = "ofclvl_id";
                    ddlOffce.DataBind();
                }
            }
            else
            {

            }
            ddlOffce.Items.Insert(0, "SELECT");
            ddlOffce.Items[0].Value = "0";
            ddlOffce.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlOffce.Items.Insert(0, "SELECT");
            ddlOffce.Items[0].Value = "0";
            ddlOffce.SelectedIndex = 0;
            _common.ErrorLog("helpdesk.aspx-0001", ex.Message.ToString());
        }
    }
    private void getAdvanceDaysBooking()
    {
        try
        {
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_advance_days");
            DataTable MyTable = bll.SelectAll(MyCommand);
            if (MyTable.TableName == "Success")
            {
                if (MyTable.Rows.Count > 0)
                {
                    Session["MaxDate"] = Convert.ToInt32(MyTable.Rows[0]["days"].ToString());
                    hdmaxdate.Value = MyTable.Rows[0]["days"].ToString();
                }
                else
                {
                    Session["MaxDate"] = "30";
                    hdmaxdate.Value = "30";
                }
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("helpdesk.aspx-0002", ex.Message.ToString());
        }
    }
    private void loadBusServiceType()
    {
        try
        {
            DataTable dt = new DataTable();
            ddlServiceType.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_servicetype_getlist");
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                ddlServiceType.DataSource = dt;
                ddlServiceType.DataTextField = "servicetype_name_en";
                ddlServiceType.DataValueField = "srtpid";
                ddlServiceType.DataBind();
            }
            ddlServiceType.Items.Insert(0, "All");
            ddlServiceType.Items[0].Value = "0";
            ddlServiceType.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlServiceType.Items.Insert(0, "All");
            ddlServiceType.Items[0].Value = "0";
            ddlServiceType.SelectedIndex = 0;
            _common.ErrorLog("helpdesk.aspx-0003", ex.Message.ToString());
        }
    }
    private void pnlDetailsShowHide(Panel pnl)
    {
        pnlTicket.Visible = false;
        pnlBusPasses.Visible = false;
        pnlCourier.Visible = false;
        pnlChartedBus.Visible = false;
        pnl.Visible = true;
    }
    private void btnCssView(LinkButton btn)
    {
        lbtnTicket.CssClass = "nav-item nav-link";
        lbtnBusPass.CssClass = "nav-item nav-link";
        lbtnCourier.CssClass = "nav-item nav-link";
        lbtnChartedBus.CssClass = "nav-item nav-link";
        btn.CssClass = "nav-item nav-link active";
    }
    private void loadfaq(string v)
    {
        try
        {
            DataTable dt = new DataTable();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_faq");
            MyCommand.Parameters.AddWithValue("p_categorycode", v);
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                rptfaqcategory.DataSource = dt;
                rptfaqcategory.DataBind();
                int id = Convert.ToInt32(dt.Rows[0]["faqid"].ToString());
                loadFaqs(id);
            }

        }
        catch (Exception ex)
        {
            Errormsg("Something Went Wrong");
            _common.ErrorLog("helpdesk.aspx-0004", ex.Message.ToString());
        }
    }

    private void loadFaqs(int id)
    {
        try
        {
            DataTable dt = new DataTable();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_GETFAQQUESTION");
            MyCommand.Parameters.AddWithValue("categoryid", id);
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                rptrFAQ.DataSource = dt;
                rptrFAQ.DataBind();
            }

        }
        catch (Exception ex)
        {
            _common.ErrorLog("helpdesk.aspx-0005", ex.Message.ToString());
            Errormsg("Something Went Wrong");
        }
    }

    private void resetTicketDetails()
    {
        tbTicketDetailMobile.Text = "";
        tbTicketJourneyDate.Text = "";
        tbTicketBookingDate.Text = "";
        gvTicketDetails.Visible = false;
    }
    private void loadServices(string fromStationName, string toStationName, string date)//M2
    {
        try
        {
            wsClass obj = new wsClass();
            DataTable dt = new DataTable();
            dt = obj.search_services_dt(fromStationName, toStationName, ddlServiceType.SelectedValue, date);
            if (dt.Rows.Count > 0)
            {
                RepterDetails.DataSource = dt;
                RepterDetails.DataBind();
                RepterDetails.Visible = true;

                LabelSearchStations.Text = fromStationName + " - " + toStationName;
                LabelSearchDateService.Text = gmdpJDate.Text + " ( " + ddlServiceType.SelectedItem.Text + " )";

                LabelSearchStations.Visible = true;
                LabelSearchDateService.Visible = true;
                //PanelSearch.Visible = False
                SearchTicketDetails.Visible = true;
            }
            else
            {
                LabelSearchStations.Visible = false;
                LabelSearchDateService.Visible = false;
                RepterDetails.Visible = false;
                Errormsg("Currently No Service is Available for Online Booking , Between Selected Stations. Please Try Other Combinations");


            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("helpdesk.aspx-0006", ex.Message.ToString());
            Errormsg("Something Went Wrong");
        }
    }
    private void resetTicketServices()
    {
        ddlPlaceFrom.Text = "";
        ddlPlaceTo.Text = "";
        gmdpJDate.Text = "";
        RepterDetails.Visible = false;
        LabelSearchStations.Visible = false;
        LabelSearchDateService.Visible = false;
    }
    private void resetBusPass()
    {

    }
    [WebMethod()]
    public static List<string> searchStations(string stationText, string fromTo)//M1
    {
        try
        {
            wsClass obj = new wsClass();
            return obj.search_station_web(stationText, fromTo);
            //return obj.search_station_web(stationText);
        }
        catch (Exception ex)
        {
            sbCommonFunc _common = new sbCommonFunc();
            _common.ErrorLog("DashWeb-M1", ex.Message);
            return null;
        }
    }
    private void loadTicketDetails()
    {
        try
        {
            DataTable dt = new DataTable();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_get_ticketdetails");
            MyCommand.Parameters.AddWithValue("p_search", tbTicketDetailMobile.Text);
            MyCommand.Parameters.AddWithValue("p_booking_date", tbTicketBookingDate.Text);
            MyCommand.Parameters.AddWithValue("p_journey_date", tbTicketJourneyDate.Text);
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                gvTicketDetails.DataSource = dt;
                gvTicketDetails.DataBind();
                gvTicketDetails.Visible = true;
                totserach.Text = "Total Result Found " + dt.Rows.Count + "<hr style='margin-top: 4px;' />";
                totserach.Visible = true;
            }
            else
            {
                dt.Clear();
                totserach.Visible = false;
                gvTicketDetails.DataSource = dt;
                gvTicketDetails.Visible = false;
                gvTicketDetails.DataBind();
                Errormsg("Data not available, please enter correct details");
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("helpdesk.aspx-0007", ex.Message.ToString());
            Errormsg("Something Went Wrong");
        }
    }
    private void getDataAccordingToRadioButton()
    {
        if (rdbOfficeSearch.Checked == true)
        {
            SearchOffice();
        }
        if (rdbBusSearch.Checked == true)
        {
            searchBus();
        }
        if (rdbEmployeeSearch.Checked == true)
        {
            searchEmployee();
        }
    }
    private void searchEmployee()
    {
        try
        {
            DataTable dt = new DataTable();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_empdetails");
            MyCommand.Parameters.AddWithValue("p_search", txtAllSearch.Text);
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                gvEmployeeSearchData.DataSource = dt;
                gvEmployeeSearchData.DataBind();
                mpEmployeeSearch.Show();
            }
            else
            {
                Errormsg("No Data Found, Please input correct details");
            }

        }
        catch (Exception ex)
        {
            _common.ErrorLog("helpdesk.aspx-0008", ex.Message.ToString());
            Errormsg("Something Went Wrong");
        }
    }
    private void searchBus()
    {
        try
        {
            DataTable dt = new DataTable();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_bus_details");
            MyCommand.Parameters.AddWithValue("p_search", txtAllSearch.Text);
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                gvBusSearch.DataSource = dt;
                gvBusSearch.DataBind();

                mpBusData.Show();
            }
            else
            {
                Errormsg("No Data Found, Please input correct details");
            }

        }
        catch (Exception ex)
        {
            _common.ErrorLog("helpdesk.aspx-0009", ex.Message.ToString());
            Errormsg("Something Went Wrong");
        }
    }
    private void SearchOffice()
    {
        try
        {
            DataTable dt = new DataTable();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_office_details");
            MyCommand.Parameters.AddWithValue("p_ofclevelid", Convert.ToInt32(ddlOffce.SelectedValue));
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                gvOfficeSearch.DataSource = dt;
                gvOfficeSearch.DataBind();
                mpOfficeData.Show();
            }
            else
            {
                Errormsg("No Data Found, Please input correct details");
            }

        }
        catch (Exception ex)
        {
            _common.ErrorLog("helpdesk.aspx-0010", ex.Message.ToString());
            Errormsg("Something Went Wrong");
        }
    }
    void PageOpen(string title, string src)
    {
        lblTitle.InnerText = title;
        embedPage.Src = src;
        mpePage.Show();
    }
    #endregion


    #region"Events"
    protected void rptfaqcategory_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        CsrfTokenValidate();
        if (e.CommandName == "FAQ")
        {
            HiddenField hdFAQCATID = (HiddenField)e.Item.FindControl("hdFAQCATID");

            foreach (RepeaterItem item in rptfaqcategory.Items)
            {
                LinkButton lbtnphotocatgy = (LinkButton)item.FindControl("lbtnFAQCATNAME");
                if (item.Equals(e.Item))
                    lbtnphotocatgy.CssClass = "btn btn-sm btn-primary ml-1";
                else
                    lbtnphotocatgy.CssClass = "btn btn-sm btn btn-warning ml-1";
            }
            loadFaqs(Convert.ToInt32(hdFAQCATID.Value.ToString()));
        }
    }

    protected void rptfaqcategory_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

    }

    protected void lbtnFaqs_Click(object sender, EventArgs e)
    {
        mpFaq.Show();
    }

    protected void lbtnSearchOffice_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (rdbBusSearch.Checked == false && rdbEmployeeSearch.Checked == false && rdbOfficeSearch.Checked == false)
        {
            Errormsg("Please Check Some Category");
        }
        else
        {
            getDataAccordingToRadioButton();
        }
    }



    protected void rdbEmployeeSearch_CheckedChanged(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        lblOffice.Visible = false;
        txtAllSearch.Visible = true;
        ddlOffce.Visible = false;
        txtAllSearch.Text = "";
        txtAllSearch.Attributes.Add("Placeholder", "Enter Name/Mobile/Email Id");
    }

    protected void rdbOfficeSearch_CheckedChanged(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        lblOffice.Visible = true;
        ddlOffce.Visible = true;
        txtAllSearch.Visible = false;
        txtAllSearch.Text = "";

    }

    protected void rdbBusSearch_CheckedChanged(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        lblOffice.Visible = false;
        ddlOffce.Visible = false;
        txtAllSearch.Visible = true;
        txtAllSearch.Attributes.Add("Placeholder", "Enter Bus No.");
        txtAllSearch.Text = "";
    }

    protected void gvOfficeSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvOfficeSearch.PageIndex = e.NewPageIndex;
        SearchOffice();
    }

    protected void gvBusSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvBusSearch.PageIndex = e.NewPageIndex;
        searchBus();
    }

    protected void gvEmployeeSearchData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvEmployeeSearchData.PageIndex = e.NewPageIndex;
        searchEmployee();
    }
    protected void lbtnTicket_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        pnlDetailsShowHide(pnlTicket);
        btnCssView(lbtnTicket);
        lbtnFaqs.Visible = true;
        loadfaq("TICKET");
        resetTicketServices();
        resetTicketDetails();
    }
    protected void lbtnBusPass_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        pnlDetailsShowHide(pnlBusPasses);
        btnCssView(lbtnBusPass);
        lbtnFaqs.Visible = false;
    }
    protected void lbtnReset_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        resetTicketServices();
    }
    protected void lbtnCourier_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        pnlDetailsShowHide(pnlCourier);
        btnCssView(lbtnCourier);
        lbtnFaqs.Visible = false;
    }
    protected void lbtnChartedBus_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        pnlDetailsShowHide(pnlChartedBus);
        btnCssView(lbtnChartedBus);
        lbtnFaqs.Visible = false;
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        string frSton = ddlPlaceFrom.Text.Trim().ToUpper();
        string toSton = ddlPlaceTo.Text.Trim().ToUpper();
        string date = gmdpJDate.Text.Trim().ToUpper();

        if (_SecurityCheck.IsValidString(frSton, 2, 50) == false)
        {
            Errormsg("Enter valid From Station Name");
            return;
        }
        if (_SecurityCheck.IsValidString(toSton, 2, 50) == false)
        {
            Errormsg("Enter valid To Station Name");
            return;
        }
        if (_SecurityCheck.IsValidString(date, 10, 10) == false)
        {
            Errormsg("Enter valid Date");
            return;
        }

        // lblSearchStations.Text = frSton + " - " + toSton;
        loadServices(frSton, toSton, date);
    }
    protected void btntoday_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        string frSton = ddlPlaceFrom.Text.Trim().ToUpper();
        string toSton = ddlPlaceTo.Text.Trim().ToUpper();
        string date = DateTime.Now.ToString("dd-MM-yyyy");

        if (_SecurityCheck.IsValidString(frSton, 2, 50) == false)
        {
            Errormsg("Enter valid From Station Name");
            return;
        }
        if (_SecurityCheck.IsValidString(toSton, 2, 50) == false)
        {
            Errormsg("Enter valid To Station Name");
            return;
        }
        if (_SecurityCheck.IsValidString(date, 10, 10) == false)
        {
            Errormsg("Enter valid Date");
            return;
        }

        // lblSearchStations.Text = frSton + " - " + toSton;
        loadServices(frSton, toSton, date);
    }
    protected void btntomorrow_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        string frSton = ddlPlaceFrom.Text.Trim().ToUpper();
        string toSton = ddlPlaceTo.Text.Trim().ToUpper();
        string date = DateTime.Now.AddDays(1).ToString("dd-MM-yyyy");

        if (_SecurityCheck.IsValidString(frSton, 2, 50) == false)
        {
            Errormsg("Enter valid From Station Name");
            return;
        }
        if (_SecurityCheck.IsValidString(toSton, 2, 50) == false)
        {
            Errormsg("Enter valid To Station Name");
            return;
        }
        if (_SecurityCheck.IsValidString(date, 10, 10) == false)
        {
            Errormsg("Enter valid Date");
            return;
        }

        // lblSearchStations.Text = frSton + " - " + toSton;
        loadServices(frSton, toSton, date);
    }
    protected void lbtnTicketdetailsSearch_Click(object sender, EventArgs e)
    {
        
        CsrfTokenValidate();
        loadTicketDetails();
    }
    protected void gvTicketDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblTicketNo = e.Row.FindControl("lblticket") as Label;

                Label isprint = e.Row.FindControl("lblprint") as Label;
                Label TripStatus = e.Row.FindControl("lbltripstatus") as Label;
                Label emailid = e.Row.FindControl("lblemail") as Label;
                Label deptime = e.Row.FindControl("departuretime") as Label;

                LinkButton btnprint = e.Row.FindControl("lbtnPrint") as LinkButton;
                LinkButton btnresnd = e.Row.FindControl("lbtnresendsms") as LinkButton;
                // int isprint = Convert.ToInt32(gvTicketDetails.DataKeys(e.Row.RowIndex).Values(1));
                //string TripStatus = gvTicketDetails.DataKeys(e.Row.RowIndex).Values("TRIPSTATUS").ToString();

                LinkButton btnresendemail = e.Row.FindControl("lbtnresendmail") as LinkButton;
                LinkButton btnresendwhatsapp = e.Row.FindControl("lbtnresendwhatsapp") as LinkButton;
                // Dim btnBusDetail As LinkButton = TryCast(e.Row.FindControl("lbtnBusDetail"), LinkButton)
                //   string emailid = gvTicketDetails.DataKeys(e.Row.RowIndex).Values(3).ToString();
                Label laldepttime = e.Row.FindControl("lbljouyneydate") as Label;
                //  string deptime = gvTicketDetails.DataKeys(e.Row.RowIndex).Values(2).ToString();


                if (Convert.ToInt32(isprint.Text) == 0)
                {
                    btnprint.Enabled = false;
                    btnprint.Style.Add("background-color", "#808080 !important");
                    btnprint.Style.Add("border-color", "#808080 !important");
                    btnprint.Style.Add("cursor", "default");

                    btnresnd.Enabled = false;
                    btnresnd.Style.Add("background-color", "#808080 !important");
                    btnresnd.Style.Add("cursor", "default");
                    btnresnd.Style.Add("border-color", "#808080 !important");
                    btnresendemail.Enabled = false;
                    btnresendemail.Style.Add("background-color", "#808080 !important");
                    btnresendemail.Style.Add("cursor", "default");
                    btnresendemail.Style.Add("border-color", "#808080 !important");

                }
                else if (Convert.ToInt32(isprint.Text) == 1)
                {
                    // Dim McurrentDateTime As Date
                    // McurrentDateTime = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")
                    // Dim MserviceDateTime As Date
                    // MserviceDateTime = FormatDateTime(laldepttime.Text.Trim, DateFormat.ShortDate).ToString + " " + FormatDateTime(deptime, DateFormat.ShortTime).ToString
                    // If DateDiff(DateInterval.Minute, McurrentDateTime, MserviceDateTime) > 120 Then
                    // btnBusDetail.Enabled = False
                    // btnBusDetail.Style.Add("background-color", "#808080 !important")
                    // btnBusDetail.Style.Add("cursor", "default")
                    // btnBusDetail.Style.Add("border-color", "#808080 !important")
                    // Else
                    btnresnd.Enabled = true;
                    // btnresnd.Style.Add("background-color", "#808080 !important");
                    // btnresnd.Style.Add("cursor", "default");
                    //btnresnd.Style.Add("border-color", "#808080 !important");
                    //btnresendwhatsapp.Enabled = false;


                    if (emailid.Text == "")
                    {
                        btnresendemail.Enabled = false;
                        btnresendemail.Style.Add("background-color", "#808080 !important");
                        btnresendemail.Style.Add("cursor", "default");
                        btnresendemail.Style.Add("border-color", "#808080 !important");
                    }
                }
                else if (Convert.ToInt32(isprint.Text) == 2)
                {
                    btnprint.Enabled = false;
                    btnprint.Style.Add("background-color", "#808080 !important");
                    btnprint.Style.Add("cursor", "default");
                    btnprint.Style.Add("border-color", "#808080 !important");
                    btnresendemail.Enabled = false;
                    btnresendemail.Style.Add("background-color", "#808080 !important");
                    btnresendemail.Style.Add("cursor", "default");
                    btnresendemail.Style.Add("border-color", "#808080 !important");

                    btnresnd.Enabled = false;
                    btnresnd.Style.Add("background-color", "#808080 !important");
                    btnresnd.Style.Add("cursor", "default");
                    btnresnd.Style.Add("border-color", "#808080 !important");
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void gvTicketDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTicketDetails.PageIndex = e.NewPageIndex;
        loadTicketDetails();
    }
    protected void gvTicketDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        CsrfTokenValidate();
        int index = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "print")
        {
            Session["p_ticketNo"] = gvTicketDetails.DataKeys[index].Values["ticket_no"].ToString();
            PageOpen("e-Ticket", "../E_ticket.aspx");
        }
        if (e.CommandName == "resendsms")
        {
            string ticket = gvTicketDetails.DataKeys[index].Values["ticket_no"].ToString();
            sms.sendTicketConfirm_SMSnEMAIL(ticket, "", "");
            Errormsg("Message Send Successfully");
        }
        if (e.CommandName == "resendemail")
        {
            Errormsg("Coming Soon");
        }

        if (e.CommandName == "view")
        {
            Session["_ticketNo"] = gvTicketDetails.DataKeys[index].Values["ticket_no"].ToString();

            mpTicket.Show();
        }
    }
    #endregion


}