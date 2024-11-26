using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class testsms : System.Web.UI.Page
{
    string entityid = "1401525010000018738";
    protected void Page_Load(object sender, EventArgs e)
    {
        string otp=getRandom();
        //sendOtpBySMS("123444", "8126562803");
CommonSMSnEmail sms = new CommonSMSnEmail();
        sms.sendTicketCancel_SMSnEMAIL("123", "8126562803", "4", "55", "88");

        sms.TripChartForConductor("ad", "8126562803", "erag", "are", "reag", "aref", "raeg");

        sms.TripChartForTraveller("cda", "8126562803", "raf", "rae", "rgf", "reg", "rg");
    }
    public void sendOtpBySMS(string otp, string MmobNo)
    {
        try
        {
            MmobNo = "+91" + MmobNo;

            HttpWebRequest request;
            HttpWebResponse response1;
            Stream dataStream;
            StreamReader reader;
            string responseFromServer;
            string Mmsg;
	    Mmsg = "PNR No: "+"123"+" Cancellation Ref No: "+"123"+" Seats Cancelled: "+"123"+" , Refund Amount Rs: "+"123"+". APSTS";

           // Mmsg = otp + " Is the OTP To complete e-Verification process.";

//PNR: {#var#},DOJ: {#var#} ,DP: {#var#} , {#var#} -{#var#} , Seat(s) No.: {#var#} , Total Amount: {#var#}


	    ServicePointManager.ServerCertificateValidationCallback =
                     ((sender, certification, chain, sslPolicyErrors) => true);
            string API = "https://smsgw.sms.gov.in/failsafe/HttpLink?username=apsts.sms&pin=B2%40qE8%23vY7&message=" + Mmsg + "&mnumber=" + MmobNo + "&signature=ArPSTS&dlt_entity_id=" + entityid + "&dlt_template_id=1407168862513864447";

            //txtmsgLog.txtmsgLog_track(Mmsg1, MmobNo, muserCode, 1);
            request = (HttpWebRequest)WebRequest.Create(API);
            response1 = (HttpWebResponse)request.GetResponse();
            dataStream = response1.GetResponseStream();
            reader = new StreamReader(dataStream);
            responseFromServer = reader.ReadToEnd();
            reader.Close();
            dataStream.Close();
            response1.Close();
Response.Write(responseFromServer);
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
           
            return;
        }
    }
    public string getRandom()
    {
        Random random = new Random();
        int i;
        string _random="";
        for (i = 0; i <= 5; i++)
            _random += random.Next(0, 9).ToString();
        return _random;
    }
}