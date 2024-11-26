using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_AgentDashboard : System.Web.UI.Page
{
    sbSecurity _security = new sbSecurity();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    protected void Page_Load(object sender, EventArgs e)
    {
        lblagname.Text = Session["_UserName"] + " ( " + Session["_UserCode"] + " )";
       
        lbltodaysummary.Text = "Today Summary as on time " + DateTime.Now.ToString("hh:mm tt");
        if (!IsPostBack)
        {
            Session["_moduleName"] = " Agent Wallet";
            tbjourneydate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            fillDropDownServiceTypes(ddlServiceType);
            LoadAgentDetails();
            LoadWallet(Session["_UserCode"].ToString());
           
            getAdvanceBookingDays();
        }
    }

    #region "Methods"
    private void getAdvanceBookingDays()
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
                    Session["PmtDays"] = Convert.ToInt32(MyTable.Rows[0]["pmt_days"].ToString());
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
        { }
    }
    
    public void Errormsg(string msg)
    {
        lblerrmsg.Text = msg;
        mpError.Show();
    }
    private void LoadAgentDetails()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "agent.fun_getagentdetails");
            MyCommand.Parameters.AddWithValue("spusercode", Session["_UserCode"].ToString());
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    

                    if (dt.Rows[0]["Expired_YN"].ToString() == "Y")
                    {
                        lbtnRenew1.Visible = false;
                        lblaccvalidity.Text = "Your account validity has been expired please renew your account validity before " + dt.Rows[0]["Renew_date"].ToString() + ".<br/>therefore your account login automatically expired.";
                        pnlbook.Visible = false;
                        pnlvalidityExpire.Visible = true;
                        lbtnonlineRecharge.Visible = false;
                        lbtnTripChart.Visible = false;
                        lbtnCurrentBooking.Visible = false;

                        if (!Convert.IsDBNull(dt.Rows[0]["Renew_Refno"]))
                        {
                            lbtnRenew.Visible = false;

                            MyCommand = new NpgsqlCommand();
                            MyCommand.Parameters.Clear();
                            MyCommand.Parameters.AddWithValue("@StoredProcedure", "agent.fun_agrequeststatus");
                            MyCommand.Parameters.AddWithValue("p_referenceno", dt.Rows[0]["Renew_Refno"].ToString());
                            MyCommand.Parameters.AddWithValue("p_mobile", dt.Rows[0]["ag_mobileno"].ToString());
                            DataTable dt1 = new DataTable();
                            dt1 = bll.SelectAll(MyCommand);
                            if (dt1.TableName == "Success")
                            {
                                if (dt1.Rows.Count > 0)
                                {
                                    lblReferenceNo.Text = "Your renew request has already been generated with reference number. " + dt1.Rows[0]["reference_no"].ToString();
                                    lbtnRenew.Visible = false;
                                }
                            }
                        }
                    }

                    if (dt.Rows[0]["Rewnew_YN"].ToString() == "Y")
                    {
                        lbtnRenew1.Visible = true;
                    }

                    lblvalidto.Text = dt.Rows[0]["valid_to"].ToString();
                    lblagemail.Text = dt.Rows[0]["ag_emailid"].ToString();
                    lblagmob.Text = dt.Rows[0]["ag_mobileno"].ToString();

                    lbltodaybooking.Text = dt.Rows[0]["today_tkt"].ToString();
                    lbltodaybookingamt.Text = (Convert.ToDecimal(dt.Rows[0]["today_tktamt"]) + Convert.ToDecimal(dt.Rows[0]["today_taxamt"])).ToString();
                    lbltodaycomamt.Text = (Convert.ToDecimal(dt.Rows[0]["today_Resamt"]) + Convert.ToDecimal(dt.Rows[0]["today_cnrtResamt"])).ToString();
                    lbltodayrefamt.Text = dt.Rows[0]["today_refund"].ToString();

                    lblcurrticket.Text = dt.Rows[0]["today_cnrt_tkt"].ToString();
                    lblcurrtripamt.Text = dt.Rows[0]["today_cnrt_tktamt"].ToString();

                    Session["Emailid"] = dt.Rows[0]["ag_emailid"].ToString();
                    Session["MobileNo"] = dt.Rows[0]["ag_mobileno"].ToString();

                    if (Convert.IsDBNull(dt.Rows[0]["Booking_mode"]) || dt.Rows[0]["Booking_mode"].ToString()=="")
                    {
                        lbtnTripChart.Enabled = false;
                        lbtnTripChart.OnClientClick = "";
                        lbtnTripChart.BackColor = System.Drawing.Color.Gray;
                        lbtnTripChart.BorderColor = System.Drawing.Color.Gray;
                        lbtnCurrentBooking.Enabled = false;
                        lbtnCurrentBooking.OnClientClick = "";
                        lbtnCurrentBooking.BackColor = System.Drawing.Color.Gray;
                        lbtnCurrentBooking.BorderColor = System.Drawing.Color.Gray;
                    }
                    else
                    {
                        lbtnCurrentBooking.Enabled = true;
                        lbtnTripChart.Enabled = true;
                        Session["_LStationName"] = dt.Rows[0]["Stn_Name"].ToString();

                        if (dt.Rows[0]["Booking_mode"].ToString() == "B")
                        {
                            Session["BookingMode"] = dt.Rows[0]["Booking_mode"].ToString();
                            Session["_LStationCode"] = dt.Rows[0]["Stn_ID"].ToString();
                            Session["_LDepotCode"] = dt.Rows[0]["Depot_Code"].ToString();
                        }
                        else if (dt.Rows[0]["Booking_mode"].ToString() == "O")
                        {
                            Session["BookingMode"] = dt.Rows[0]["Booking_mode"].ToString();
                            Session["StationId"] = dt.Rows[0]["Stn_ID"].ToString();
                            Session["_LDepotCode"] = dt.Rows[0]["Depot_Code"].ToString();
                            lbtnCurrentBooking.BackColor = System.Drawing.Color.Gray;
                            lbtnCurrentBooking.BorderColor = System.Drawing.Color.Gray;
                            lbtnCurrentBooking.Enabled = false;
                            lbtnCurrentBooking.OnClientClick = "";
                        }
                        else if (dt.Rows[0]["Booking_mode"].ToString() == "C")
                        {
                            Session["BookingMode"] = dt.Rows[0]["Booking_mode"].ToString();
                            Session["_LStationCode"] = dt.Rows[0]["Stn_ID"].ToString();
                            Session["_LDepotCode"] = dt.Rows[0]["Depot_Code"].ToString();
                            lbtnTripChart.Enabled = false;
                            lbtnTripChart.OnClientClick = "";
                            lbtnTripChart.BackColor = System.Drawing.Color.Gray;
                            lbtnTripChart.BorderColor = System.Drawing.Color.Gray;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Session["_ErrorMsg"] = "Something went wrong. <br/>Please contact the software admin with error code AgntDash13-" + ex.Message;
            Response.Redirect("errorpage.aspx");
        }

    }
    public void fillDropDownServiceTypes(DropDownList ddl)//M1
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
            ddl.Items.Insert(0, "All");
            ddl.Items[0].Value = "0";
            ddl.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddl.Items.Insert(0, "All");
            ddl.Items[0].Value = "0";
            ddl.SelectedIndex = 0;
            _common.ErrorLog("CntrDashboard-M1", ex.Message.ToString());
        }
    }


    [WebMethod()]
    public static List<string> source_destination(string stationText, string fromTo)//M2
    {
        List<string> empResult = new List<string>();
        try
        {
            wsClass obj = new wsClass();
            return obj.search_station_web(stationText, fromTo);
        }
        catch (Exception ex)
        {
            sbCommonFunc _common = new sbCommonFunc();
            _common.ErrorLog("AgentDashboard-M2", ex.Message);
            return null;
        }
    }
    private void successmsg(string sucessmsg)
    {
        lblsucessmsg.Text = sucessmsg;
        mpconfirm.Show();
    }
    private void GenerateRenewRequest()
    {
        try
        {
            string IPAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(IPAddress))
            {
                IPAddress = Request.ServerVariables["REMOTE_ADDR"];
            }

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "agent.agrenewrequest");
            MyCommand.Parameters.AddWithValue("@p_agentcode", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("@p_ipaddress", IPAddress);
            MyCommand.Parameters.AddWithValue("@p_updatedby", Session["MobileNo"].ToString());
            MyCommand.Parameters.AddWithValue("@p_emailid", Session["Emailid"].ToString());

            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                successmsg("Renew Request has been successfully Generated. Please note Reference No <b>" + dt.Rows[0]["val_reference_no"].ToString() + "</b> for future reference");
                LoadAgentDetails();
            }
            else
            {
                Errormsg("Something Went Wrong");
            }
        }

        catch (Exception ex)
        {
            
        }

    }
    //private bool isValidValues()
    //{
    //try
    //{
    //    gvService.Visible = false;
    //    pnlNoService.Visible = false;
    //    string msg = "";
    //    int msgcount = 0;

    //    if (tbfromstation.Text.Length <= 0)
    //    {
    //        msgcount++;
    //        msg += msgcount.ToString() + ". Please Enter From Station. <br/>";
    //    }
    //    if (tbtostation.Text.Length <= 0)
    //    {
    //        msgcount++;
    //        msg += msgcount.ToString() + ". Please Enter To Station. <br/>";
    //    }
    //    if (tbjourneydate.Text.Length > 0)
    //    {
    //        if (!_SecurityCheck.IsValidString(tbjourneydate.Text.Trim(), 8, 10) == false)
    //        {
    //            msgcount++;
    //            msg += msgcount.ToString() + ". Invalid Journey Date. <br/>";
    //        }
    //        else if (!DateTime.TryParse(tbjourneydate.Text.Trim(), out DateTime journeyDate))
    //        {
    //            msgcount++;
    //            msg += msgcount.ToString() + ". Invalid Journey Date. <br/>";
    //        }
    //        if (DateTime.Now.Date.Subtract(journeyDate).Days > (int)Session["MaxDate"])
    //        {
    //            msgcount++;
    //            msg += msgcount.ToString() + ". Only " + Session["MaxDate"].ToString() + " days advance booking service is available, please change your Journey Date selection. <br/>";
    //            lblNoServiceMsg.Text = "Sorry, Currently No Service is Available for Online Booking, Between Selected Stations and " + tbjourneydate.Text.Trim() + ". <br/><br/><b>Please Try Other Combinations..</b>";
    //        }
    //    }
    //    else
    //    {
    //        msgcount++;
    //        msg += msgcount.ToString() + ". Invalid Journey Date. <br/>";
    //    }
    //    if (msgcount > 0)
    //    {
    //        Errormsg(msg);
    //        return false;
    //    }
    //    return true;
    //}
    //catch (Exception ex)
    //{
    //    Errormsg(ex.Message);
    //    return false;
    //}

    //}
    private void LoadWallet(string userId)
    {
        try
        {
            classWalletAgent obj = new classWalletAgent();
            DataTable dtWallet = obj.GetWalletDetailDt(userId);
            if (dtWallet.Rows.Count > 0)
            {
                lblWalletBalance.Text = "Your Wallet Balance <b>" + " ₹" + dtWallet.Rows[0]["currentbalanceamount"].ToString() + "</b>";
                if (dtWallet.Rows[0]["val_amount"].ToString() == "0")
                {
                    lblWalletLastUpdate.Text = "( Recharge your wallet first time )";
                }
                else
                {
                    lblWalletLastUpdate.Text = "( Last Wallet Recharge " + dtWallet.Rows[0]["d_date"].ToString() + " with ₹" + dtWallet.Rows[0]["val_amount"].ToString() +" )";
                }
            }
            else
            {
                lblWalletBalance.Text = "NA";
                lblWalletLastUpdate.Text = "NA (Please refresh page)";
            }
        }
        catch (Exception ex)
        {
           
        }
    }
    private void loadServices(string fromStationName, string toStationName, string date)//M3
    {
        try
        {
            wsClass obj = new wsClass();
            DataTable dt = new DataTable();
            dt = obj.search_services_dt(fromStationName, toStationName, ddlServiceType.SelectedValue, date);
            if (dt.Rows.Count > 0)
            {
                gvService.DataSource = dt;
                gvService.DataBind();

                lblNoServiceMsg.Text = "";
                gvService.Visible = true;

                pnlNoService.Visible = false;
            }
            else
            {
                lblNoServiceMsg.Text = "Currently No Service is Available for Online Booking , Between Selected Stations.";
                pnlNoService.Visible = true;
                gvService.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblNoServiceMsg.Text = "Oops! Something happened with your ticket loading process.<br> Please feel free to contact the helpdesk";
            pnlNoService.Visible = true;
            _common.ErrorLog("AgentDashboard-M3", ex.Message);
        }
    }

    protected bool IsValidBalance(string strAgentCode, string fare)
    {
        try
        {
            classWalletAgent obj = new classWalletAgent();
            DataTable dt_wallet = new DataTable();
            dt_wallet = obj.GetWalletDetailDt(strAgentCode);

            if (dt_wallet.Rows.Count > 0)
            {
                decimal currentBalance = Convert.ToDecimal(dt_wallet.Rows[0]["currentbalanceamount"].ToString());

                if (currentBalance > Convert.ToDecimal(fare) && currentBalance != 0)
                {
                    return true;
                }
            }

            return false;
        }
        catch (Exception ex)
        {
            Errormsg("Something went wrong. <br/>Please contact the software admin with error code AgntSeatSlctn10-");
            return false;
        }
    }
    #endregion

    #region "Event"
    protected void lbtnSearchService_Click(object sender, EventArgs e)
    {

        loadServices(tbfromstation.Text.Trim().ToUpper(), tbtostation.Text.Trim().ToUpper(), tbjourneydate.Text);
    }

    private void search_servicesList(string deptcode, string arrcode, short mbuseServiceTypeCode, string journeyDate)
    {

    }
    protected void lbtnonlineRecharge_Click(object sender, EventArgs e)
    {
        Response.Redirect("AgentRechrgeTopup.aspx");
    }
    protected void lbtnTripChart_Click(object sender, EventArgs e)
    {
        Session["HDAgname"] = Session["_UserName"] + " ( " + Session["_UserCode"] + " )";
        Response.Redirect("agtripchart.aspx");
    }
    protected void lbtnCurrentBooking_Click(object sender, EventArgs e)
    {
        Session["HDAgname"] = Session["_UserName"] + " ( " + Session["_UserCode"] + " )";
        Response.Redirect("AgCurrentBookingDashboard.aspx");
    }
    protected void lbtnCancellation_Click(object sender, EventArgs e)
    {
        Response.Redirect("AgTicketCancel.aspx");
    }
    protected void lbtnspcialcancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("AgsplTicketCancel.aspx");
    }
    protected void lbtnquery_Click(object sender, EventArgs e)
    {
        Response.Redirect("agTicketQuery.aspx");
    }
    protected void lbtnReport_Click(object sender, EventArgs e)
    {
        Response.Redirect("AgentReport.aspx");
    }
   
    protected void lbtndailyaccount_Click(object sender, EventArgs e)
    {
        Response.Redirect("agBookingAndCancellationSummary.aspx");
    }
    protected void lbtnVerificationYes_Click(object sender, EventArgs e)
    {
        GenerateRenewRequest();
    }
    protected void lbtnRenew_Click(object sender, EventArgs e)
    {
        lblconfirmmsg.Text = "Dou you want generate Renew Request ?";
        mpAgentVerification.Show();
    }
    protected void lbtnVerificationNo_Click(object sender, EventArgs e)
    {
        GenerateRenewRequest();
    }

    protected void gvService_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "BOOKTICKET")
        {
            int rowIndex = int.Parse(e.CommandArgument.ToString());
            string openclose = gvService.DataKeys[rowIndex]["openclose"].ToString();

            if (openclose == "C")
            {
                Errormsg("Ticket Booking Time Close.");
                return;
            }

           



            string dsvcid = gvService.DataKeys[rowIndex]["dsvcid"].ToString();
            string strpid = gvService.DataKeys[rowIndex]["strpid"].ToString();
            string depttime = gvService.DataKeys[rowIndex]["depttime"].ToString();
            string arrtime = gvService.DataKeys[rowIndex]["arrtime"].ToString();
            string tripdirection = gvService.DataKeys[rowIndex]["tripdirection"].ToString();
            string servicetypename = gvService.DataKeys[rowIndex]["servicetypename"].ToString();
            string totalfare = gvService.DataKeys[rowIndex]["fare"].ToString();
            if (!IsValidBalance(Session["_usercode"].ToString(), totalfare))
            {
                Errormsg("Balance is not sufficient; please deposit amount");
                return;
            }
            string layout = gvService.DataKeys[rowIndex]["layout"].ToString();
            string midstations = gvService.DataKeys[rowIndex]["midstations"].ToString();
            string frstonid = gvService.DataKeys[rowIndex]["frstonid"].ToString();
            string fromstation = gvService.DataKeys[rowIndex]["from_station_name"].ToString();
            string tostonid = gvService.DataKeys[rowIndex]["tostonid"].ToString();
            string tostation = gvService.DataKeys[rowIndex]["to_station_name"].ToString();

            Dictionary<string, string> dct = new Dictionary<string, string>();
            dct.Add("dsvcid", dsvcid);
            dct.Add("strpid", strpid);
            dct.Add("depttime", depttime);
            dct.Add("arrtime", arrtime);
            dct.Add("tripdirection", tripdirection);
            dct.Add("servicetypename", servicetypename);
            dct.Add("totalfare", totalfare);
            dct.Add("layout", layout);
            dct.Add("midstations", midstations);
            dct.Add("frstonid", frstonid);
            dct.Add("fromstation", fromstation);
            dct.Add("tostonid", tostonid);
            dct.Add("tostation", tostation);
            dct.Add("date", tbjourneydate.Text);

            Session["SearchParameters"] = dct;
            Session["_UserType"] = "A";
            Session["_ctzAuthNoSeats"] = "0";
            Response.Redirect("AGSeatSelection.aspx");

        }
    }
  



    protected void gvService_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbtnopenclose = (LinkButton)e.Row.FindControl("lbtnclose");
            LinkButton lbtnbook = (LinkButton)e.Row.FindControl("lbtnOrgView");
            DataRowView rowView = (DataRowView)e.Row.DataItem;
            lbtnbook.Visible = false;
            lbtnopenclose.Visible = false;

            int availableseat = 0;

            if (int.TryParse(rowView["totalavailablseats"].ToString(), out availableseat) == true)
            {
                availableseat = int.Parse(rowView["totalavailablseats"].ToString());
            }

            if (rowView["openclose"].ToString() == "O" && availableseat > 0)
            {
                lbtnbook.Visible = true;
                lbtnopenclose.Visible = false;
            }
            else
            {
                lbtnbook.Visible = false;
                lbtnopenclose.Visible = true;
            }
        }
    }

    #endregion
}