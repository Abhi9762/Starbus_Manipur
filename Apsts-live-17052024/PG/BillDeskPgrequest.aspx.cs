using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PG_BillDeskPgrequest : System.Web.UI.Page
{
    public string passwordHashSha1;
    BilldeskSha billdesk = new BilldeskSha();
    BILLDESK bill = new BILLDESK();
    protected void Page_Load(object sender, EventArgs e)
    {
        /////////declare param
        string MERCHANTID="", order_id="", amount="", CURRENCYTYPE = "", TYPEFIELD1 = "", SECURITYID = "", TYPEFIELD2 = "";
        string billing_email = "", PG = "",  checksumkey = "", CheckSum = "", MString1 = "", respoURL = "", billing_tel = "", billing_name = "";
        ///
        string PathAndQuery = Request.Url.PathAndQuery;
        string url_host = Request.Url.AbsoluteUri.Replace(PathAndQuery, "");
        //Response.Write(url_host);
        //return;
        //respoURL = url_host + "/starbus_manipur/PG/BillDeskPgresponse.aspx";
        respoURL = url_host + "/PG/BillDeskPgresponse.aspx";

        MERCHANTID = "BDSKUATY"; //"APSTSBUS";
        SECURITYID = "bdskuaty";// "apstsbus";
        CURRENCYTYPE = "INR";
        TYPEFIELD1="R";
        TYPEFIELD2 = "F";
        checksumkey = "G3eAmyVkAzKp8jFq0fqPEqxF4agynvtJ";// "M4RjgVXBHUFV";
        PG = "PG";

        string requestidEncrypt = Request.QueryString["RequestId"].ToString();
        byte[] bty = System.Convert.FromBase64String(requestidEncrypt);
        string requestid = System.Text.Encoding.UTF8.GetString(bty);

        if (requestid == "1")//Ticket Booking 
        {
            wsClass obj = new wsClass(); 
            DataTable dt = new DataTable();
            dt = obj.getJourneyDetails(Session["TicketNumber"].ToString(), "R");

            if (dt.Rows.Count > 0)
            {
                order_id = dt.Rows[0]["_ticketno"].ToString();
                billing_name = "Ticket Booking";
                billing_tel = dt.Rows[0]["traveller_mobile_no"].ToString();
                billing_email = dt.Rows[0]["traveller_email_id"].ToString();
                decimal amount1 =Convert.ToDecimal( dt.Rows[0]["amount_total"].ToString());
                amount = amount1.ToString("0.##");
            }
        }
        else if (requestid == "11")//Traveller Topup wallet 
        {
            order_id = Session["transrefno"].ToString();
            decimal amount1 = Convert.ToDecimal(Session["depositamt"].ToString());
            amount = amount1.ToString("0.##");
            billing_name = "Traveller Wallet Topup";
            billing_tel = Session["_UserCode"].ToString();
            billing_email = "";
        }
        else if (requestid == "111")//Ticket Booking From Mobile App
        {
            order_id = Session["strTicketNo"].ToString();
            decimal amount1 = Convert.ToDecimal(Session["_UTCTicketFinalAmount"].ToString());
            amount = amount1.ToString("0.##");
            billing_name = "Ticket Booking";
            billing_tel = Session["_pmtCTZMobile"].ToString();
            billing_email = Session["_pmtCTZEMAIL"].ToString();
        }
        else if (requestid == "1111")//Traveller Topup wallet  From Mobile App
        {
            order_id = Session["transrefno"].ToString();
            decimal amount1 = Convert.ToDecimal(Session["depositamt"].ToString());
            amount = amount1.ToString("0.##");
            billing_name = "Traveller Wallet Topup";
            billing_tel = Session["_UserCode"].ToString();
            billing_email = "";
        }
        else if (requestid == "2")//Agent Topup wallet
        {
            order_id = Session["transrefno"].ToString();
            decimal amount1 = Convert.ToDecimal(Session["depositamt"].ToString());
            amount = amount1.ToString("0.##");
            billing_name = "Agent Recharge Account";
            billing_tel = Session["MobileNo"].ToString();
            billing_email = Session["Emailid"].ToString();
        }
        else if (requestid == "222")//Agent Topup wallet ETM
        {
            order_id = Session["transrefno"].ToString();
            decimal amount1 = Convert.ToDecimal(Session["depositamt"].ToString());
            amount = amount1.ToString("0.##");
            billing_name = "Agent Recharge Account";
            billing_tel = Session["MobileNo"].ToString();
            billing_email = Session["Emailid"].ToString();
        }
        else if (requestid == "3")//Bus Passes
        {
            order_id = Session["currtranrefno"].ToString();
            decimal amount1 = Convert.ToDecimal(Session["AMOUNT"].ToString());
            amount = amount1.ToString("0.##");
            billing_name = "Bus Pass";
            billing_tel = Session["MobileNo"].ToString();
            billing_email = Session["Emailid"].ToString();
        }

        else if (requestid == "22")//Agent Pay Security Fee
        {
            order_id = Session["transrefno"].ToString();
            amount = Convert.ToDecimal(Session["depositamt"].ToString()).ToString();
            billing_name = "Agent Pay Security Fee";
            billing_tel = Session["MobileNo"].ToString();
            billing_email = Session["Emailid"].ToString();
        }
        else
        {
            Session["_ErrorMsg"] = "Invalid Transaction";
            Response.Redirect("../errorpage.aspx");
        }
        MString1 = MERCHANTID + "|" + order_id + "|NA|" + amount + "|NA|NA|NA|"+CURRENCYTYPE+"|NA|"+TYPEFIELD1+"|"+ SECURITYID + "|NA|NA|"+TYPEFIELD2+"|NA|NA|NA|NA|NA|NA|NA|" + respoURL;

        string Mhash;
        Mhash = billdesk.GetHMACSHA256(MString1, checksumkey);
        CheckSum = Mhash;
        if (bill.BILLDESKRequest(requestid, MERCHANTID, order_id, amount, CURRENCYTYPE, TYPEFIELD1,
            SECURITYID, TYPEFIELD2, billing_email, billing_tel,  billing_name, PG,  respoURL, checksumkey, CheckSum,MString1) ==false )
        {
            Session["_ErrorMsg"] = "Invalid Transaction -";
            Response.Redirect("../errorpage.aspx");
        }
       passwordHashSha1 = MString1 + "|" + Mhash.ToUpper().Trim();
    }
}