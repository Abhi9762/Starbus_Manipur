using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;

/// <summary>
/// Summary description for CommonSMSnEmail
/// </summary>
public class CommonSMSnEmail
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    string entityid = "1401525010000018738";

    public CommonSMSnEmail()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    //SMS Request Call
    private void sendSmsByRequest(string msg, string mobile,string Templateid)
    {
//        HttpWebRequest request;
//        HttpWebResponse response1;
//        Stream dataStream;
//        StreamReader reader;
//        string responseFromServer;
//ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
//        string API = "https://smsgw.sms.gov.in/failsafe/HttpLink?username=apsts.sms&pin=B2%40qE8%23vY7&message=" + msg + "&mnumber=" + mobile + "&signature=ArPSTS&dlt_entity_id=" + entityid + "&dlt_template_id="+Templateid+"";
//        ServicePointManager.ServerCertificateValidationCallback = ((sender, certification, chain, sslPolicyErrors) => true);
//        request = (HttpWebRequest)WebRequest.Create(API);
//        response1 = (HttpWebResponse)request.GetResponse();
//        dataStream = response1.GetResponseStream();
//        reader = new StreamReader(dataStream);
//        responseFromServer = reader.ReadToEnd();
//        reader.Close();
//        dataStream.Close();
//        response1.Close();
    }

    #region"Traveller Otp Verification"
    public void sendOtp(string otp, string mobile)
    {
        try
        {
string minss = "2";
            string templateid = "1407168784904156529";
            mobile = "+91" + mobile;
            string msg = "Your OTP is "+otp+" to complete e-Verification process. The OTP is valid for "+minss+" mins. APSTS";
                sendSmsByRequest(msg, mobile, templateid);
        }
        catch (Exception ex)
        { }
    }
    #endregion
    #region"Ticket Confirmation"
    public void sendTicketConfirm_SMSnEMAIL(string ticket, string mobile, string modulemsgcode)
    {
        string templateid = "1407168862494460138";
        string pnr = "";
        string doj = "";
        string dep = "";
        string route = "";
        string seatno = "";
        string amount = "";
        
        MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.SPTICKETDETAILS_FORPRINT");
        MyCommand.Parameters.AddWithValue("p_ticket", ticket);
        dt = bll.SelectAll(MyCommand);
	mobile = "+91" + dt.Rows[0]["ctz_mobile"].ToString();
        pnr = ticket;
        doj = dt.Rows[0]["journey_date"].ToString();
        dep = dt.Rows[0]["depart_time"].ToString();
        amount = dt.Rows[0]["total_charge"].ToString();
        route = dt.Rows[0]["source_abbr"].ToString() + "-" + dt.Rows[0]["destination_abbr"].ToString();

        wsClass obj = new wsClass();
        DataTable dtPassengerDetails = new DataTable();
        dtPassengerDetails = obj.getTicketDetails_byTicket(ticket);
        
        int i = 0;
        int totalPassangers = dtPassengerDetails.Rows.Count;
        foreach (DataRow drow in dtPassengerDetails.Rows)
        {
            seatno = seatno + drow["seatno"].ToString().Trim() + ",";   
        }
        seatno = seatno.Substring(0, seatno.Length - 1);

        string msg = "Dear Traveler,Your PNR is:" + pnr + ". DOJ:" + doj + ". DEP:" + dep + " Route:" + route + ".Seat(s) No.:" + seatno + ". Total Amount:" + amount + ". APSTS";
        sendSmsByRequest(msg, mobile,templateid);
    }
    #endregion
    #region"Ticket Cancellation"
    public void sendTicketCancel_SMSnEMAIL(string ticket, string mobile,string seatNo,string seatamt,string cancellationrefno)
    {

wsClass obj = new wsClass();
        DataTable dt = obj.getTicketDetails_byTicket(ticket);
        mobile = "+91" + dt.Rows[0]["trvlr_mob"].ToString();
        string templateid = "1407168862513864447";
        
        string msg = "PNR No: " + ticket + " Cancellation Ref No: " + cancellationrefno + " Seats Cancelled: " + seatNo.TrimEnd(',') + " , Refund Amount Rs: " + seatamt.TrimEnd(',') + ". APSTS";
       sendSmsByRequest(msg, mobile,templateid);
    }
    #endregion
    #region"TopUp Wallet Successfully"
    public void WallletSucessMsg(string MRefNo, string mobile, string MAmt)
    {
        string templateid = "1407168862519526849";
        mobile = "+91" + mobile;
        string msg = "Your wallet is recharged successfully vide Ref. Number: "+ MRefNo + ". APSTS";
        sendSmsByRequest(msg, mobile, templateid);
    }
    #endregion
#region"Trip Chart"
    public void TripChartForConductor(string tripcode, string mobile, string from ,string to, string dept,string bus,string totalseat)
    {
        string templateid = "1407159584575977450";
        mobile = "+91" + mobile;
        string msg = "Trip Chart generated for trip code " + tripcode + " " + from + " to " + to + ".Departure Time " + dept + " Bus No. " + bus + ".Total seats booked " + totalseat + "";
        sendSmsByRequest(msg, mobile, templateid);
    }
    public void TripChartForTraveller(string pnr, string mobile, string from, string to, string dept, string bus, string conductor)
    {
        string templateid = "1407159584583031754";
        mobile = "+91" + mobile;
        string msg = "Trip Chart generated. PNR "+pnr+ ",( " + from + " - " + to + "), DP:  " + dept + ",  Bus No. " + bus + ",  Conductor Mob No " + conductor + ".";
        sendSmsByRequest(msg, mobile, templateid);
    }
    #endregion
#region"Service Block & Cancel"
    public void ServiceBlockCancel(string bookby, string mobile, string ticket)
    {
        if (bookby == "T")
        {
            string templateid = "1407159584613893923";
            mobile = "+91" + mobile;
            string msg = "Service Against PNR Number "+ticket+" has been cancelled, Booking Amt will be refunded by respective bank.";
            sendSmsByRequest(msg, mobile, templateid);
        }
        if (bookby == "C")
        {
            string templateid = "1407159584609575826";
            mobile = "+91" + mobile;
            string msg = "Service Against PNR Number "+ticket+" has been cancelled, please collect your refund amount from any APSTS Booking Counter.";
            sendSmsByRequest(msg, mobile, templateid);
        }
        if (bookby == "A")
        {
            string templateid = "1407159584619395493";
            mobile = "+91" + mobile;
            string msg = "Service against PNR Number "+ticket+" has been cancelled, please collect your refund amt from concerned Agent";
            sendSmsByRequest(msg, mobile, templateid);
        }
    }
    #endregion
 #region"Passwrd Change by Admin"
    public void ChngePwdbyadmin(string Userid, string Pwd, string mobile)
    {
        string templateid = "1407168862641122511";
        mobile = "+91" + mobile;
        string msg = "Your password has been changed successfully for USER ID "+ Userid + ". Kindly note your New Password is "+ Pwd + ". Never share this information with anybody. APSTS";
        sendSmsByRequest(msg, mobile, templateid);
    }
    #endregion

    #region"Depot Service Time Change"
    public void dsTimeChange(string fromSton, string toSton, string fromtime, string toTime, string mobile)
    {
        string templateid = "1407170079894194033";
        mobile = "+91" + mobile;
        string msg = "Dear Traveler, We are sorry to inform that the scheduled departure time for your trip from "+fromSton+" to "+toSton +" on date "+fromtime +" is changed to "+toTime +".Inconvenience is regretted. APSTS";
        sendSmsByRequest(msg, mobile, templateid);
    }
    #endregion
 #region"Seat Blocked"
    public void seatblocked(string mobile, string journeydate, string servicetype, string fromstation,string tostation, string seatno)
    {
        string templateid = "1407159584628377082";
        mobile = "+91" + mobile;
        string msg = "Seat(s) have been booked for "+ servicetype + " , Bus Service No. "+ fromstation + "  to "+ tostation + " , DP: "+ journeydate + " , Seat Nos(s): "+ seatno + " , Payment shall be collected by conductor at the time of journey.";
        sendSmsByRequest(msg, mobile, templateid);
    }
    #endregion
}