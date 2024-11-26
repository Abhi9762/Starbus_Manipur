using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;

public partial class Auth_sysAdmBlockSeat : System.Web.UI.Page
{
    private sbBLL bll = new sbBLL();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    string current_date = DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("yyyy");
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    sbValidation _validation = new sbValidation();
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["_moduleName"] = "Seat Blocking";
        if (!IsPostBack)
        {
            getAdvanceDaysBooking();
            LoadBusServices();
            fromStaionList(ddlBusServiceTypes.SelectedValue, "0");
            toStaionList(ddlBusServiceTypes.SelectedValue, "0");
            txtdate.Text = current_date;
            lblMessageJ.Text = "Please Fill All The Fields For Seat Blocking";
            lblMessageJ.Visible = true;
        }
    }

    #region"Methods"
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
        { }
    }
    public void LoadBusServices()
    {
        try
        {
            ddlBusServiceTypes.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_servicetype_getlist");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlBusServiceTypes.DataSource = dt;
                    ddlBusServiceTypes.DataTextField = "servicetype_name_en";
                    ddlBusServiceTypes.DataValueField = "srtpid";
                    ddlBusServiceTypes.DataBind();
                }
            }
            ddlBusServiceTypes.Items.Insert(0, "Select");
            ddlBusServiceTypes.Items[0].Value = "0";
            ddlBusServiceTypes.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlBusServiceTypes.Items.Insert(0, "Select");
            ddlBusServiceTypes.Items[0].Value = "0";
            ddlBusServiceTypes.SelectedIndex = 0;
        }
    }
    private void toStaionList(string servicetype, string otherStationCode)
    {
        ddlPlaceTo.Items.Clear();
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.fn_get_station_name");
        MyCommand.Parameters.AddWithValue("p_station_for", "T");
        MyCommand.Parameters.AddWithValue("p_other_station_code", otherStationCode);
        MyCommand.Parameters.AddWithValue("p_service_type", servicetype);
        dt = bll.SelectAll(MyCommand);
        if (dt.TableName == "Success")
        {
            if (dt.Rows.Count > 0)
            {
                ddlPlaceTo.DataSource = dt;
                ddlPlaceTo.DataTextField = "stationname";
                ddlPlaceTo.DataValueField = "sp_status";
                ddlPlaceTo.DataBind();
            }
            ddlPlaceTo.Items.Insert(0, "Select");
            ddlPlaceTo.Items[0].Value = "0";
            ddlPlaceTo.SelectedIndex = 0;
        }
    }
    private void fromStaionList(string servicetype, string otherStationCode)
    {
        ddlPlaceFrom.Items.Clear();
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.fn_get_station_name");
        MyCommand.Parameters.AddWithValue("p_station_for", "F");
        MyCommand.Parameters.AddWithValue("p_other_station_code", otherStationCode);
        MyCommand.Parameters.AddWithValue("p_service_type", servicetype);
        dt = bll.SelectAll(MyCommand);
        if (dt.TableName == "Success")
        {
            if (dt.Rows.Count > 0)
            {
                ddlPlaceFrom.DataSource = dt;
                ddlPlaceFrom.DataTextField = "stationname";
                ddlPlaceFrom.DataValueField = "sp_status";
                ddlPlaceFrom.DataBind();
            }
            ddlPlaceFrom.Items.Insert(0, "Select");
            ddlPlaceFrom.Items[0].Value = "0";
            ddlPlaceFrom.SelectedIndex = 0;
        }
    }
    private void restall()
    {
        pnldetails.Visible = false;
        ddlBusServiceTypes.SelectedIndex = 0;
        ddlPlaceFrom.SelectedIndex = 0;
        ddlPlaceTo.SelectedIndex = 0;
        grvJDetails.DataSource = null;
        txtdate.Text = current_date;
        lblMessageJ.Text = "Please Fill All The Fields For Seat Blocking";
        lblMessageJ.Visible = true;
        Session["_ctzAuthServiceCodeUP"] = null;
        Session["_ctzAuthSTRP_ID"] = null;
        Session["_ctzAuthJType"] = null;
        Session["_ctzAuthJDate"] = null;
    }
    private void Errormsg(string msg)
    {
        string popup = _popup.modalPopupSmall("W", "Please Note", msg, "Close");
        Response.Write(popup);
    }
    private bool isvalidValues()
    {
        try
        {
            if (ddlBusServiceTypes.SelectedIndex <= 0)
            {
                Errormsg("Please select bus service type");
                return false;
            }
            if (ddlPlaceFrom.SelectedIndex <= 0)
            {
                Errormsg("Please select Departure From");
                return false;
            }
            if (ddlPlaceTo.SelectedIndex <= 0)
            {
                Errormsg("Please select Arrival To");
                return false;
            }
            if (_validation.IsValidInteger(ddlBusServiceTypes.SelectedValue, 1, 5) == false)
            {
                Errormsg("Please select bus service type");
                return false;
            }
            if (_validation.IsValidInteger(ddlPlaceFrom.SelectedValue, 1, 5) == false)
            {
                Errormsg("Please select Departure From");
                return false;
            }
            if (_validation.IsValidInteger(ddlPlaceTo.SelectedValue, 1, 5) == false)
            {
                Errormsg("Please select Arrival To");
                return false;
            }
            if (_validation.IsValidString(txtdate.Text.Trim(), 10, 10) == false)
            {
                Errormsg("Invalid Journey Date");
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    private void search_servicesList(int mbusStationFrom, int mbusStationTo, int mbuseServiceTypeCode, string journeyDate)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.fn_get_services_list");
            MyCommand.Parameters.AddWithValue("p_from_station", mbusStationFrom);
            MyCommand.Parameters.AddWithValue("p_to_station", mbusStationTo);
            MyCommand.Parameters.AddWithValue("p_servicetype_id", mbuseServiceTypeCode);
            MyCommand.Parameters.AddWithValue("p_date", journeyDate);
            dt = bll.SelectAll(MyCommand);
            lblSearchResult.Text = "Search Result as on " + DateTime.Now.ToString();
            DataRow[] dr = dt.Select("minutes>closingminutes");
            int count = dr.Length;
            if (count == 0)
            {
                grvJDetails.Visible = false;
                lblMessageJ.Visible = true;
                lblMessageJ.Text = "Sorry No Servie is Available for this selection. ";
                pnldetails.Visible = false;
                Errormsg("Sorry, Currently no " + ddlBusServiceTypes.SelectedItem.Text.Trim() + " Service is Available Between Selected Stations");
            }
            else
            {
                dt = dr.CopyToDataTable();
                grvJDetails.DataSource = dt;
                grvJDetails.DataBind();
                grvJDetails.Visible = true;
                lblMessageJ.Visible = false;
                pnldetails.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Errormsg("Something Went Wrong");
        }
    }
    private void setLayout(string dsvcId, string strpId, string tripDirection, string datee)
    {
        try
        {
            wsClass obj = new wsClass();


            DataTable dtTotalRowCol = obj.getLayoutTotRowColumn(dsvcId);
            string StrSeatStructUp = "";
            foreach (DataRow dRowCol in dtTotalRowCol.Rows)
                StrSeatStructUp = dRowCol["noofrows"] + "-" + dRowCol["noofcolumns"] + "-500";

            DataTable dtLayout = obj.getLayoutRowColumn(strpId, tripDirection, dsvcId, datee);
            foreach (DataRow drow in dtLayout.Rows)
                StrSeatStructUp = StrSeatStructUp + "," + drow["colnumber"] + "-" + drow["rownumber"] + "-" + drow["seatno"] + "-" + drow["seatyn"] + "-" + drow["travellertypecode"] + "-" + drow["seatavailforonlbooking"] + "-" + drow["status"];

            hfSeatStructUp.Value = StrSeatStructUp;

            pnlseatlayout.Visible = true;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "getSeatStruct('up','" + StrSeatStructUp + "');", true);
        }

        catch (Exception ex)
        {
        }
    }
    private void blockseat()
    {
        string strUserId = Session["_UserCode"].ToString();
        string chTripTypeUp = Session["_ctzAuthJType"].ToString();
        string IPAddress = HttpContext.Current.Request.UserHostAddress;

        int[] MseatNo = new int[7];
        Int32 i;
        for (i = 0; i <= 5; i++)
            MseatNo[i] = 0;
        Session["SeatNo"] = "";
        string[] CustomerList = hfCustomerData.Value.Split('|');
        int Index = 0;
        foreach (string customer in CustomerList)
        {
            string[] customerDetail = customer.Split(',');
            MseatNo[Index] = Convert.ToInt32(customerDetail[0]);
            Session["SeatNo"] = Session["SeatNo"].ToString() + MseatNo[Index].ToString() + ",";
            Index = Index + 1;
        }
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.fn_seat_block");
        MyCommand.Parameters.AddWithValue("p_dsvcid", Convert.ToInt32(Session["_ctzAuthServiceCodeUP"]));
        MyCommand.Parameters.AddWithValue("p_triptype", Session["_ctzAuthJType"].ToString());
        MyCommand.Parameters.AddWithValue("p_trip", Convert.ToInt32(Session["_ctzAuthSTRP_ID"]));
        MyCommand.Parameters.AddWithValue("p_journey_date", Session["_ctzAuthJDate"].ToString());
        MyCommand.Parameters.AddWithValue("p_seatno_1", MseatNo[0]);
        MyCommand.Parameters.AddWithValue("p_seatno_2", MseatNo[1]);
        MyCommand.Parameters.AddWithValue("p_seatno_3", MseatNo[2]);
        MyCommand.Parameters.AddWithValue("p_seatno_4", MseatNo[3]);
        MyCommand.Parameters.AddWithValue("p_seatno_5", MseatNo[4]);
        MyCommand.Parameters.AddWithValue("p_seatno_6", MseatNo[5]);
        MyCommand.Parameters.AddWithValue("p_blockeduserid", strUserId);
        MyCommand.Parameters.AddWithValue("p_reasonofblock", txtReason.Text);
        MyCommand.Parameters.AddWithValue("p_mobilenumber", txtvipmobileno.Text);
        MyCommand.Parameters.AddWithValue("p_ipaddress", IPAddress);
        dt = bll.SelectAll(MyCommand);
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["presult"].ToString().Equals("Done") == true)
            {
                Successmsg("Seats Have Been Successfully Blocked ");
                Session["SeatNo"] = Session["SeatNo"].ToString().Substring(0, Session["SeatNo"].ToString().Length - 1);
 CommonSMSnEmail comm = new CommonSMSnEmail();
                //Seat(s) have been blocked For {#var#} , Bus Service {#var#}, {#var#} To {#var#} , DP: {#var#} , Seat Nos(s): {#var#} , Fare will be collected by conductor in bus. Uttarakhand Transport Corporation.
                //comm.sendSeatBlock_SMS(txtvipmobileno.Text.Trim, txtReason.Text.Trim, ddlBusServiceTypes.SelectedItem.Text, ddlPlaceFrom.SelectedItem.Text, ddlPlaceTo.SelectedItem.Text, Session("_ctzAuthUpJourneyDateTime"), Session("SeatNo"), 6);
                 comm.seatblocked(txtvipmobileno.Text.Trim(), Session["_ctzAuthUpJourneyDateTime"].ToString(), ddlBusServiceTypes.SelectedItem.Text, ddlPlaceFrom.SelectedItem.Text, ddlPlaceTo.SelectedItem.Text, Session["SeatNo"].ToString());
               
restall();
                pnlseatlayout.Visible = false;
                txtReason.Text = "";
                txtvipmobileno.Text = "";

            }
            else
                Errormsg("Something went wrong. Please refresh page and try again.");
        }
        else
            Errormsg("Something went wrong. Please refresh page and try again.");
    }
    private void Successmsg(string msg)
    {
        string popup = _popup.modalPopupSmall("S", "Congratulations", msg, "Close");
        Response.Write(popup);
    }
    private bool isvalidblockseat()
    {
        if (_validation.IsValidString(txtReason.Text, 3, 30) == false)
        {
            Errormsg("Please Enter VIP Name.");
            return false;
        }
        if (_validation.IsValidString(txtvipmobileno.Text, 10, 10) == false)
        {
            Errormsg("Please Enter Valid Mobile No Name.");
            return false;
        }
        string[] strSeat = hfCustomerData.Value.Split('|');
        int intSeatLength = strSeat[0].Length;
        if (intSeatLength == 0)
        {
            Errormsg("Please Select Seat.");
            return false;
        }
        if (isvalidValues1() == false)
        {
            Errormsg("Invalid Values, Please correct and proceed");
            return false;
        }
        wsClass obj = new wsClass();
        //if (obj.IsTicketStillAvaialable(Session["_ctzAuthUpJourneyDateTime"].ToString().Substring(0, 10).Trim(), "UP", Session["_ctzAuthServiceCodeUP"].ToString(), Session["_ctzAuthJType"].ToString()) == false)
        if (obj.IsTicketStillAvaialable(hfCustomerData.Value, Session["_ctzAuthUpJourneyDateTime"].ToString().Substring(0, 10).Trim(), "UP", Session["_ctzAuthServiceCodeUP"].ToString()) == false)

        {
            Errormsg("Sorry, You are late, Booking has already been made for the selected seat(s)");
            Session["_errorMessage"] = "Sorry, You are late, Booking has already been made for the selected seat(s)";
            return false;
        }
        return true;
    }
    public bool isvalidValues1()
    {
        try
        {
            if (_validation.IsValidString(Session["_UserCode"].ToString().Trim(), 5, 10) == false)
                return false;
            if (_validation.IsValidString(Session["_ctzAuthUpJourneyDateTime"].ToString(), 1, 50) == false)
                return false;

            if (!(Session["_ctzAuthJType"].ToString() == "F" | Session["_ctzAuthJType"].ToString() == "I"))
                return false;
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    #endregion

    #region"Events"
    protected void btnSeatReset_Click(object sender, EventArgs e)
    {
        txtvipmobileno.Text = "";
        txtReason.Text = "";
        setLayout(Session["_ctzAuthServiceCodeUP"].ToString(), Session["_ctzAuthSTRP_ID"].ToString(), Session["_ctzAuthJType"].ToString(), Session["_ctzAuthJDate"].ToString());

    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        LoadBusServices();
        fromStaionList(ddlBusServiceTypes.SelectedValue, "0");
        toStaionList(ddlBusServiceTypes.SelectedValue, "0");
        restall();
        pnldetails.Visible = false;
        pnlseatlayout.Visible = false;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (isvalidValues() == false)
        {
            return;
        }
        int MbusStationFrom;
        int MbusStationTo;
        int mbuseServiceTypeCode;

        string MbusStationFromName;
        string MbusStationToName;
        string mbuseServiceTypeName;
        string journeyDate;

        MbusStationFrom = Convert.ToInt32(ddlPlaceFrom.SelectedValue);
        MbusStationTo = Convert.ToInt32(ddlPlaceTo.SelectedValue);
        mbuseServiceTypeCode = Convert.ToInt32(ddlBusServiceTypes.SelectedValue);

        MbusStationFromName = ddlPlaceFrom.SelectedItem.Text;
        MbusStationToName = ddlPlaceTo.SelectedItem.Text;
        mbuseServiceTypeName = ddlBusServiceTypes.SelectedItem.Text;
        journeyDate = txtdate.Text;

        lblDepSNameData.Text = MbusStationFromName;
        lblArvlSNameData.Text = MbusStationToName;
        lblServTPNameData.Text = mbuseServiceTypeName;
        lblJrnyDateData.Text = journeyDate;

        pnlseatlayout.Visible = false;
        search_servicesList(MbusStationFrom, MbusStationTo, mbuseServiceTypeCode, journeyDate);
    }
    protected void grvJDetails_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in grvJDetails.Rows)
        {
            if (row.RowIndex == grvJDetails.SelectedIndex)
                row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
            else
                row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
        }
    }
    protected void ddlPlaceFrom_SelectedIndexChanged(object sender, EventArgs e)
    {
        toStaionList(ddlBusServiceTypes.SelectedValue, ddlPlaceFrom.SelectedValue);
    }
    protected void ddlBusServiceTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        fromStaionList(ddlBusServiceTypes.SelectedValue, "0");
        toStaionList(ddlBusServiceTypes.SelectedValue, "0");
    }
    protected void btnProceed_Click(object sender, EventArgs e)
    {
        if (isvalidblockseat() == false)
        {
            setLayout(Session["_ctzAuthServiceCodeUP"].ToString(), Session["_ctzAuthSTRP_ID"].ToString(), Session["_ctzAuthJType"].ToString(), Session["_ctzAuthJDate"].ToString());

            return;
        }
        Session["SeatNo"] = "";
        int[] MseatNo = new int[7];
        string seats = "";
        string[] CustomerList = hfCustomerData.Value.Split('|');
        int Index = 0;
        foreach (string customer in CustomerList)
        {
            string[] customerDetail = customer.Split(',');
            MseatNo[Index] = Convert.ToInt32(customerDetail[0]);
            seats = seats+customerDetail[0]+",";
             
        }
        setLayout(Session["_ctzAuthServiceCodeUP"].ToString(), Session["_ctzAuthSTRP_ID"].ToString(), Session["_ctzAuthJType"].ToString(), Session["_ctzAuthJDate"].ToString());

        seats = seats.Substring (0, seats.Length -1);
        lblConfirmation.Text = "Seat no "+ seats +" have been blocked for "+txtReason.Text+"("+txtvipmobileno.Text+").<br>Do you want to continue?";
        mpConfirmation.Show();
    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        blockseat();
    }
    protected void grvJDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "Select")
        {
            txtReason.Text = "";
            txtvipmobileno.Text = "";
            Session["_ctzAuthServiceCodeUP"] = grvJDetails.DataKeys[index].Values["dsvcid"].ToString();
            Session["_ctzAuthUpJourneyDateTime"] = txtdate.Text.Trim().ToString() + " " + grvJDetails.DataKeys[index].Values["depttime"].ToString();
            // Session("_ctzAuthJTime") = grvJDetails.DataKeys(index).Values("departuretime").ToString
            Session["_ctzAuthJDate"] = txtdate.Text.Trim().ToString();
            Session["_ctzAuthJType"] = grvJDetails.DataKeys[index].Values["tripdirection"];

            Session["_ctzAuthSTRP_ID"] = grvJDetails.DataKeys[index].Values["strpid"];

            if (_validation.IsValidInteger(Session["_ctzAuthServiceCodeUP"].ToString(), 1, 6) == false)
            {
                Errormsg("Please select service from the list to proceed further");
                return;
            }

            // Session["_RNDIDENTIFIERCTZAUTHADM"] = _SecurityCheck.GeneratePassword(10, 5);
            setLayout(Session["_ctzAuthServiceCodeUP"].ToString(), Session["_ctzAuthSTRP_ID"].ToString(), Session["_ctzAuthJType"].ToString(), Session["_ctzAuthJDate"].ToString());
        }
    }
    protected void lbtnNoConfirmation_Click(object sender, EventArgs e)
    {
        setLayout(Session["_ctzAuthServiceCodeUP"].ToString(), Session["_ctzAuthSTRP_ID"].ToString(), Session["_ctzAuthJType"].ToString(), Session["_ctzAuthJDate"].ToString());

    }
    #endregion





}