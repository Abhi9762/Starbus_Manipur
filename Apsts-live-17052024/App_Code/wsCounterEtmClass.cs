using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

/// <summary>
/// Summary description for wsCounterEtmClass
/// </summary>
public class wsCounterEtmClass
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    public wsCounterEtmClass()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #region "App is Activate or Not"
    public DataTable isappactive()
    {
        MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.isappactive");
        MyCommand.Parameters.AddWithValue("p_appid", 3);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    public bool isDeviceRegistered(string imei)
    {
        MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.fn_get_cntr_device_by_imei");
        MyCommand.Parameters.AddWithValue("p_imie", imei);
        dt = bll.SelectAll(MyCommand);
        if (dt.Rows.Count > 0)
        { return true; }
        else
        { return false; }
    }
    #endregion

    #region "Advance Days"
    public DataTable getAdvanceDays()
    {
        MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_advance_days");
        dt = bll.SelectAll(MyCommand);
        return dt;
    }

    #endregion

    #region "Login"
    public DataTable userdetail(string userid)
    {
        MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_UserDetails");
        MyCommand.Parameters.AddWithValue("p_userid", userid);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    public DataTable updateLogoutDetails(string logid)
    {
        MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_insert_logout_log");
        MyCommand.Parameters.AddWithValue("p_logid", Convert.ToInt32(logid));
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    #endregion

    #region "Counter Details & Daily Account & Cash Register"
    public DataTable loadCountorUserDetails(string userid, string counterid)
    {
        MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_cntrDetails");
        MyCommand.Parameters.AddWithValue("p_userid", userid);
        MyCommand.Parameters.AddWithValue("p_counterid", counterid);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    public DataTable loadAccountSummary(string counterid)
    {
        MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.cntr_account");
        MyCommand.Parameters.AddWithValue("p_counter_id", counterid);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    public DataTable loadOnltktpasstoday(string counterid)
    {
        MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_get_cntr_passtkt_today");
        MyCommand.Parameters.AddWithValue("p_counter_id", counterid);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    public DataTable loadOfflinetkttoday(string counterid, string date)
    {
        MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_get_cntr_offline_tkt_today");
        MyCommand.Parameters.AddWithValue("p_counter_id", counterid);
        MyCommand.Parameters.AddWithValue("p_tripdate", date);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    public DataTable loadOnlCashtregister(string counterid, string date)
    {
        MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.fn_get_etm_cntr_onl_cashregister");
        MyCommand.Parameters.AddWithValue("p_counterid", counterid);
        MyCommand.Parameters.AddWithValue("p_date", date);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    public DataTable getCrntCashRegister(string counterid, string date)
    {
        MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.fn_get_etm_cntr_current_cashregister");
        MyCommand.Parameters.AddWithValue("p_counterid", counterid);
        MyCommand.Parameters.AddWithValue("p_date", date);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    #endregion

    #region"Trip Chart"
    public DataTable load_OpenTrips(string counterid)
    {
        MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_get_opentrip");
        MyCommand.Parameters.AddWithValue("p_cntrid", counterid);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    public DataTable loadTrip_OpenClose(string userId, string cntrId, string serviceType, string busNo, string routeId, string fromDate, string toDate)
    {
        MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "current_booking.f_get_trips");
        MyCommand.Parameters.AddWithValue("sp_user_id", userId);
        MyCommand.Parameters.AddWithValue("sp_cntr_id", cntrId);
        MyCommand.Parameters.AddWithValue("sp_service_type", serviceType);
        MyCommand.Parameters.AddWithValue("sp_bus", busNo);
        MyCommand.Parameters.AddWithValue("sp_route", routeId);
        MyCommand.Parameters.AddWithValue("sp_fromdate", fromDate);
        MyCommand.Parameters.AddWithValue("sp_todate", toDate);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    public DataTable loadTripChart(string tripcode, string updatedby, string actionType)
    {
        MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "current_booking.fn_get_current_trip_dtls");
        MyCommand.Parameters.AddWithValue("sp_tripcode", tripcode);
        MyCommand.Parameters.AddWithValue("sp_updatedby", updatedby);
        MyCommand.Parameters.AddWithValue("sp_action_type", actionType);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    public DataTable loadTripChartSeat(string tripcode)
    {
        MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "current_booking.f_get_current_trip_seat_dtls");
        MyCommand.Parameters.AddWithValue("sp_tripcode", tripcode);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    #endregion

    #region"Ticket"
    public DataTable CheckTicketStaus(string ticketno, string bookingmode)
    {
        MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_check_ticket_status");
        MyCommand.Parameters.AddWithValue("spticketno", ticketno);
        MyCommand.Parameters.AddWithValue("spbookingmode", bookingmode);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    public DataTable loadTicket(string ticketno)
    {
        MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "current_booking.f_get_current_tkt_dtls");
        MyCommand.Parameters.AddWithValue("p_ticketno", ticketno);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    public DataTable loadTktQuery(string booking_date, string journey_date, string search, string bookbytype)
    {
        MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_ticket_Query");
        MyCommand.Parameters.AddWithValue("p_bookingdate", booking_date);
        MyCommand.Parameters.AddWithValue("p_journeydate", journey_date);
        MyCommand.Parameters.AddWithValue("p_search", search);
        MyCommand.Parameters.AddWithValue("p_bookbytype", bookbytype);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }

    #endregion

    #region"Ticket Cancel Details"
    public DataTable get_cancel_tkt_details(string cancl_refno, string ticketno)
    {
        MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.fn_get_cancel_tkt_details");
        MyCommand.Parameters.AddWithValue("p_cancl_refno", cancl_refno);
        MyCommand.Parameters.AddWithValue("p_ticketno", ticketno);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    public DataTable get_cancel_tkt_seats(string cancl_refno, string ticketno)
    {
        MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.fn_get_cancel_tkt_seats");
        MyCommand.Parameters.AddWithValue("p_cancl_refno", cancl_refno);
        MyCommand.Parameters.AddWithValue("p_ticketno", ticketno);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    #endregion

    #region"Search Station"
    public DataTable search_station_app(string stationText, string flag_F_T)
    {
        MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_search_stations");
        MyCommand.Parameters.AddWithValue("p_search_text", stationText);
        MyCommand.Parameters.AddWithValue("p_from_to", flag_F_T);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    #endregion

    #region"Update Counter Ticket"
    public DataTable updateTkt(string ticketno, string counterid, string usercode, string pmt_mode)
    {
        MyCommand = new NpgsqlCommand();
        DataTable dt1 = new DataTable();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.cntr_payment_done");
        MyCommand.Parameters.AddWithValue("p_ticket_no", ticketno);
        MyCommand.Parameters.AddWithValue("p_counter_id", counterid);
        MyCommand.Parameters.AddWithValue("p_user_id", usercode);
        MyCommand.Parameters.AddWithValue("p_pmtmode", pmt_mode);
        dt = bll.SelectAll(MyCommand);
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["p_rslt"].ToString() == "DONE")
            {
                DataTable dtTicket = new DataTable("dtTicket");
                dtTicket.Columns.Add("TicketNo", typeof(string));
                dtTicket.Rows.Add(ticketno);
                return dtTicket;
            }
            else
            {
                return dt1;
            }
        }
        else
        {
            return dt1;
        }

    }
    #endregion

    #region"Layout"
    public DataTable GetRowColumn(string registration_no, string tripdate, string stationId)
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        DataTable dt;
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "current_booking.f_get_rowcolumn_counter");
        MyCommand.Parameters.AddWithValue("p_registration_no", registration_no);
        MyCommand.Parameters.AddWithValue("p_tripdate", tripdate);
        MyCommand.Parameters.AddWithValue("p_stationid", stationId);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    #endregion

    #region"Trip"
    public DataTable loadtrip_Dtls(string tripcode, string tripdate)
    {
        MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.fn_get_trip_details");
        MyCommand.Parameters.AddWithValue("p_tripcode", tripcode);
        MyCommand.Parameters.AddWithValue("p_tripdate", tripdate);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    #endregion

    #region"Agent ETM API Function"
    public DataTable getAccount_Smry_dt(string usercode, string fromdate, string todate)
    {
        MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "agent_wallet.agentpassbook");
        MyCommand.Parameters.AddWithValue("@sp_agentcode", usercode);
        MyCommand.Parameters.AddWithValue("@sp_datefrom", fromdate);
        MyCommand.Parameters.AddWithValue("@sp_dateto", todate);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    public DataTable getagTrans_Smry_dt(string usercode, string fromdate, string todate)
    {
        MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "agent.f_get_agent_transaction_by_date");
        MyCommand.Parameters.AddWithValue("@p_agentcode", usercode);
        MyCommand.Parameters.AddWithValue("@p_fdate", fromdate);
        MyCommand.Parameters.AddWithValue("@p_tdate", todate);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    public DataTable agentdetail(string usercode)
    {
        MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "agent.fn_get_agent_details");
        MyCommand.Parameters.AddWithValue("p_userid", usercode);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    public DataTable Generate_RenewRequest(string usercode, string IPAddress, string UPDATEDBY, string EmailId)
    {
        MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "agent.agrenewrequest");
        MyCommand.Parameters.AddWithValue("@p_agentcode", usercode);
        MyCommand.Parameters.AddWithValue("@p_ipaddress", IPAddress);
        MyCommand.Parameters.AddWithValue("@p_updatedby", UPDATEDBY);
        MyCommand.Parameters.AddWithValue("@p_emailid", EmailId);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    public DataTable loadAgentBookingDetails(string userId, string StonId)
    {
        MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "agent.fn_get_agent_smry_details");
        MyCommand.Parameters.AddWithValue("@p_userid", userId);
        MyCommand.Parameters.AddWithValue("@p_ston_id", Convert.ToInt32(StonId));
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    public Boolean agent_updateTkt(string ticketno, string usercode)
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "agent_wallet.sp_agentpaymentdone_new");
        MyCommand.Parameters.AddWithValue("@p_ticketno", ticketno);
        MyCommand.Parameters.AddWithValue("@p_agentcode", usercode);
        string dt = bll.UpdateAll(MyCommand);
        if (dt == "Success")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public DataTable getTicketDetails_byCrntTicket(string tickeno)
    {
        MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "agent.fn_get_ticket_details");
        MyCommand.Parameters.AddWithValue("p_ticketno", tickeno);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    public DataTable ChngePassword(string userid, string newpass)
    {
        MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.change_user_pwd");
        MyCommand.Parameters.AddWithValue("sp_userid", userid);
        MyCommand.Parameters.AddWithValue("sp_pass", newpass);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    #endregion
}