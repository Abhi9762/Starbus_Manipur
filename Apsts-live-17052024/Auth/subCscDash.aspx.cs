using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_subCscDash : System.Web.UI.Page
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    string msg = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        //Session["_moduleName"] = "Common Sevice Provider Registration";
        Session["moduleName"] = "Sub CSC Dashboard" + " - (" + Session["_UserName"] + " " + Session["_UserCode"] + ")";


        if (!IsPostBack)
        {
            txtSearch.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            LoadDailyCount(Session["_UserCode"].ToString(), txtSearch.Text);
            LoadWallet(Session["_UserCode"].ToString());
            LoadDailyRegister(Session["_UserCode"].ToString(), txtSearch.Text);
            fillDropDownServiceTypes(ddlServiceType);

            lbladvancedays.Text = "You can online booking up to <b>" + getAdvanceDaysBooking() + " days </b> in advance.";


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
            _common.ErrorLog("subCscDash.aspx", ex.Message);
            return null;
        }
    }
    #region "Method"

    private string getAdvanceDaysBooking()
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
                    return MyTable.Rows[0]["days"].ToString();
                }
                else
                {
                    Session["MaxDate"] = "30";
                    hdmaxdate.Value = "30";
                    return "30";
                }
            }
        }
        catch (Exception ex)
        {

        }
        return "30";

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

    private void LoadWallet(string userId)
    {
        try
        {
            classWalletAgent obj = new classWalletAgent();
            DataTable dtWallet = obj.GetWalletDetailDt(userId);
            if (dtWallet.Rows.Count > 0)
            {
                lblWalletBalance.Text = "Your Wallet Balance <b>" +  dtWallet.Rows[0]["currentbalanceamount"].ToString() + "</b>";
                if (dtWallet.Rows[0]["val_amount"] == "0")
                {
                    lblWalletLastUpdate.Text = "( Recharge your wallet first time )";
                }
                else
                {
                    lblWalletLastUpdate.Text = "( Last Wallet Recharge " + dtWallet.Rows[0]["d_date"].ToString() + " with ₹" + dtWallet.Rows[0]["val_amount"].ToString() +  ")";
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

    private bool IsValidValues(string fromStation, string toStation, string journeyDate)
    {
        //if (!_validation.IsValidString(fromStation.Trim(), 4, 100) == false)
        //{
        //    msg = "Please Enter From Station";
        //    return false;
        //}
        //if (!_validation.IsValidString(toStation.Trim(), 4, 100) == false)
        //{
        //    msg = "Please Enter To Station";
        //    return false;
        //}
        //if (!_validation.IsValidString(journeyDate.Trim(), 10, 10) == false)
        //{
        //    msg = "Invalid Journey Date(Please enter date in DD/MM/YYYY format e.g. for 1st Jan 2015 enter 01/01/2015)";
        //    return false;
        //}
        //else if (DateTime.TryParse(journeyDate.Trim(), out DateTime parsedDate) == false)
        //{
        //    msg = "Invalid Journey Date(Please enter date in DD/MM/YYYY format e.g. for 1st Jan 2015 enter 01/01/2015)";
        //    return false;
        //}

        //if ((parsedDate - DateTime.Now).Days > (int)Session["MaxDate"])
        //{
        //    msg = "Only " + Session["MaxDate"].ToString() + " days advance booking service is available, please change your Journey Date selection";
        //    return false;
        //}
        return true;
    }

    private void LoadDailyCount(string p_User, string p_Date)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "csc.getsubcscdailycount");
            MyCommand.Parameters.AddWithValue("@p_agentcode", p_User);
            MyCommand.Parameters.AddWithValue("@p_date", p_Date);

            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    lblemail.Text = dt.Rows[0]["val_email"].ToString();
                    lblmobile.Text = dt.Rows[0]["val_mobile"].ToString();
                    lblopenblnc.Text = dt.Rows[0]["val_opening"].ToString();
                    lblcloseblnc.Text = dt.Rows[0]["val_closing"].ToString();
                    lblTotalBooking.Text = dt.Rows[0]["val_ticket"].ToString();
                    lblBookingAmt.Text = dt.Rows[0]["val_ticketamt"].ToString();
                    lblCancelledBooking.Text = dt.Rows[0]["val_cancellation"].ToString();
                    lblCancelamt.Text = dt.Rows[0]["val_cancellation_amount"].ToString();
                    lblTotalPass.Text = dt.Rows[0]["val_passapply"].ToString();
                    lblPassAmount.Text = dt.Rows[0]["val_passamount"].ToString();
                    lblCommissionAmt.Text = dt.Rows[0]["val_commissionamt"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            string mmm = ex.Message;
        }
    }


    private void LoadDailyRegister(string p_User, string p_Date)
    {
        try
        {

            gvDailyRegister.Visible = false;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "csc.getagentdailyregister");
            MyCommand.Parameters.AddWithValue("@p_agentcode", p_User);
            MyCommand.Parameters.AddWithValue("@p_date", p_Date);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    gvDailyRegister.DataSource = dt;
                    gvDailyRegister.DataBind();
                    gvDailyRegister.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            gvDailyRegister.Visible = false;
            // historymsg.Visible = true
        }
    }
    public void ErrorModalPopup(string header, string message)
    {
        LabelModalErrorHeader.Text = header;
        LabelModalErrorMessage.Text = message;
        ModalPopupError.Show();
    }
    protected bool IsValidBalance(string strAgentCode, string fare)
    {
        try
        {

            classWalletAgent obj = new classWalletAgent();
            DataTable dtWallet = obj.GetWalletDetailDt(strAgentCode);

            if (dtWallet.Rows.Count > 0)
            {
                if (Convert.ToDecimal(dtWallet.Rows[0]["currentbalanceamount"]) > Convert.ToDecimal(fare))
                {
                    return true;
                }
            }
            return false;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    private void loadServices(string fromStationName, string toStationName, string date)//M3
    {
        try
        {

            Session["_ctzStationFromName"] = fromStationName;
            Session["_ctzStationToName"] = toStationName;
            Session["_ctzJourneyDate"] = date;
            wsClass obj = new wsClass();
            DataTable dt = new DataTable();
            dt = obj.search_services_dt(fromStationName, toStationName, ddlServiceType.SelectedValue, date);
            if (dt.Rows.Count > 0)
            {
                RepterDetails.DataSource = dt;
                RepterDetails.DataBind();
                RepterDetails.Visible = true;

                LabelSearchStations.Text = fromStationName.ToUpper() + " - " + toStationName.ToUpper();
                //LabelSearchDateService.Text = gmdpJDate.Text.Trim() + " ( " + serviceName + " )";

                LabelSearchStations.Visible = true;
                LabelSearchDateService.Visible = true;
                // PanelSearch.Visible = false;
                SearchTicketDetails.Visible = true;
            }
            else
            {
                LabelSearchStations.Visible = false;
                LabelSearchDateService.Visible = false;
                RepterDetails.Visible = false;
                ErrorModalPopup("Please Check", "Currently No Service is Available for Online Booking , Between Selected Stations. Please Try Other Combinations");
            }
        }
        catch (Exception ex)
        {
            ErrorModalPopup("Something Went Wrong", ex.Message);
        }
    }
    private void ResetTicketDetails()
    {
        gmdpJDate.Text = "";
        ddlPlaceTo.Text = "";
        ddlPlaceFrom.Text = "";
        RepterDetails.Visible = false;
        LabelSearchStations.Visible = false;
        LabelSearchDateService.Visible = false;
    }

    #endregion


    #region "Event"

    protected void gvDailyRegister_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDailyRegister.PageIndex = e.NewPageIndex;
        LoadDailyRegister(Session["_UserCode"].ToString(), txtSearch.Text);
    }
    protected void gvDailyRegister_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblTXN_AMOUNT = e.Row.FindControl("lblTXN_AMOUNT") as Label;
            LinkButton lbtnView = e.Row.FindControl("lbtnView") as LinkButton;
            DataRowView rowView = e.Row.DataItem as DataRowView;

            if (rowView["val_wallet_txn_type_code"].ToString() == "C")
            {
                lblTXN_AMOUNT.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblTXN_AMOUNT.ForeColor = System.Drawing.Color.Red;
            }

            if (rowView["val_wallet_txn_type_code"].ToString() == "T")
            {
                lbtnView.Visible = false;
            }
        }
    }
    protected void gvDailyRegister_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "View")
        {
            string tkt;
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            string WALLET_TXN_TYPE_CODE = gvDailyRegister.DataKeys[row.RowIndex]["val_wallet_txn_type_code"].ToString();
            tkt = gvDailyRegister.DataKeys[row.RowIndex]["val_txn_ref_no"].ToString();

            if (WALLET_TXN_TYPE_CODE == "B" || WALLET_TXN_TYPE_CODE == "C")
            {
                Session["_ticketNo"] = tkt;
                mpTicket.Show();
            }

            if (WALLET_TXN_TYPE_CODE == "P")
            {
                Session["_RefNo"] = tkt;
                mpBusPass.Show();
            }
        }
    }
    protected void lbtnSearch_Click(object sender, EventArgs e)
    {
        LoadDailyRegister(Session["_UserCode"].ToString(), txtSearch.Text);
        LoadDailyCount(Session["_UserCode"].ToString(), txtSearch.Text);
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {



        loadServices(ddlPlaceFrom.Text.Trim().ToUpper(), ddlPlaceTo.Text.Trim().ToUpper(), gmdpJDate.Text);
    }
    protected void RepterDetails_ItemCommand(object source, RepeaterCommandEventArgs e)
    {


        string servicecode = e.CommandArgument.ToString();
        Label lblservicetypecode = (Label)e.Item.FindControl("lblservicetypecode");
        Label busservicecode = (Label)e.Item.FindControl("lblservicecode");
        Label lblservicetypename = (Label)e.Item.FindControl("lblservicetypename");
        Label lblServiceTripCode = (Label)e.Item.FindControl("lblservicetripcode");

        if (e.CommandName == "book")
        {
            string journeyDate = Session["_ctzJourneyDate"].ToString();
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
            string serviccode = commandArgs[0];
            string serviceRorJ = commandArgs[1];
            string Fareamt = commandArgs[2];
            string jdtime = commandArgs[4];
            string totdistance = commandArgs[3];

            //DateTime _tripdt = Convert.ToDateTime(journeyDate.Trim());
           // int _minutes = IsBookingTimeAvailable(_tripdt, lblServiceTripCode.Text);

            // Checking Balance
            if (!IsValidBalance(Session["_usercode"].ToString(), Fareamt))
            {
                ErrorModalPopup("Insufficient Balance", "Balance is not sufficient for ticket booking,<br> please Deposit amount");
                return;
            }

           // if (_minutes > 120)
           // {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    Label lblbusservicetypename = (Label)e.Item.FindControl("lblservicetypename");
                    Label lbldepattime = (Label)e.Item.FindControl("lbldepttime");
                    Label lblaritime = (Label)e.Item.FindControl("lblarritime");
                    Label lbltotalfare = (Label)e.Item.FindControl("lblfare");
                    Label lbltotseat = (Label)e.Item.FindControl("lblseattobook");

                    Label fromStationCode = (Label)e.Item.FindControl("LabelRepteFromStationCode");
                    Label toStationCode = (Label)e.Item.FindControl("LabelRepteToStationCode");

                    Session["_RNDIDENTIFIERCTZAUTHC"] = _validation.GeneratePassword(10, 5);

                    Dictionary<string, string> dct = new Dictionary<string, string>();
                    dct.Add("dsvcid", serviccode);
                    dct.Add("strpid", lblServiceTripCode.Text);
                    dct.Add("depttime", lbldepattime.Text);
                    dct.Add("arrtime", lblaritime.Text);
                    dct.Add("tripdirection", serviceRorJ);
                    dct.Add("servicetypename", lblservicetypename.Text);
                    dct.Add("servicetypeCode", lblservicetypecode.Text);
                    dct.Add("frstonid", fromStationCode.Text);
                    dct.Add("fromstation", Session["_ctzStationFromName"].ToString());
                    dct.Add("tostonid", toStationCode.Text);
                    dct.Add("tostation", Session["_ctzStationToName"].ToString());
                    dct.Add("date", journeyDate.Trim());
                    dct.Add("farePerSeat", lbltotalfare.Text.Trim());

                    Session["SearchParameters"] = dct;
                    Session["_UserType"] = "A";
                    Session["_ctzAuthNoSeats"] = "0";

                    Response.Redirect("CSCSeatSelection.aspx");
                }
            //}
            //else
            //{
            //    return;
            //}
        }

    }
    protected void lbtnSearchTicketDetails_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtTicketSearch.Text) && string.IsNullOrEmpty(txtBookingDT.Text) && string.IsNullOrEmpty(txtJourneyDt.Text))
        {
            ErrorModalPopup("Please Check", "Please fill at least 1 field");
        }
        else
        {
            Session["pSearch"] = txtTicketSearch.Text;
            Session["pJourneyDt"] = txtJourneyDt.Text;
            Session["pBookDt"] = txtBookingDT.Text;
            Response.Redirect("subCscTicketDetails.aspx");
        }

    }
    protected void btntoday_Click(object sender, EventArgs e)
    {
        gmdpJDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        if (!IsValidValues(ddlPlaceFrom.Text.Trim().ToUpper(), ddlPlaceTo.Text.Trim().ToUpper(), gmdpJDate.Text))
        {
            ErrorModalPopup("Please Check", msg);
            return;
        }
        loadServices(ddlPlaceFrom.Text.Trim().ToUpper(), ddlPlaceTo.Text.Trim().ToUpper(), gmdpJDate.Text);

    }
    protected void btntomorrow_Click(object sender, EventArgs e)
    {
        gmdpJDate.Text = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy");
        if (!IsValidValues(ddlPlaceFrom.Text.Trim().ToUpper(), ddlPlaceTo.Text.Trim().ToUpper(), gmdpJDate.Text))
        {
            ErrorModalPopup("Please Check", msg);
            return;
        }
        loadServices(ddlPlaceFrom.Text.Trim().ToUpper(), ddlPlaceTo.Text.Trim().ToUpper(), gmdpJDate.Text);

    }
    protected void lbtnReset_Click(object sender, EventArgs e)
    {
        ResetTicketDetails();
    }
    protected void lbtnTimeTable_Click(object sender, EventArgs e)
    {
        Response.Redirect("subCscTimetable.aspx");
    }
    protected void lbtncancelticket_Click(object sender, EventArgs e)
    {
        Session["p_ticketNo"] = "0";
        Response.Redirect("subCscTicketCancellation.aspx");
    }
    protected void lbtndailycashregister_Click(object sender, EventArgs e)
    {
        Response.Redirect("SubCscCashregister.aspx");
    }
    protected void lbtnbookingcancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("SubCscBookingByAgent.aspx");
    }
    protected void lbtnspecialrefund_Click(object sender, EventArgs e)
    {
        Response.Redirect("SubCscsplrefund.aspx");
    }

    #endregion
}