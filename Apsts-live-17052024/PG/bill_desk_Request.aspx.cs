using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BILLDESK_PG_bill_desk_Request : System.Web.UI.Page
{
    BILLDESK bill = new BILLDESK();
    BilldeskSha billdesk = new BilldeskSha();
    payload payload = new payload();

    string merchntidnew = "ARUNACUAT";
    string secretkeynew = "bnuEN7aXpEolZxlllNdZQ0cLAaFbXOLx";
    string clientidnew = "arunacuat";
    public string MurlNew = "https://uat1.billdesk.com/u2/payments/ve1_2/orders/create";

    protected async void Page_Load(object sender, EventArgs e)
    {
        
        string billing_email = "", billing_tel = "", billing_name = "";


        string requestidEncrypt = Request.QueryString["RequestId"].ToString();
        byte[] bty = System.Convert.FromBase64String(requestidEncrypt);
        string requestid = System.Text.Encoding.UTF8.GetString(bty);
        string txnID = "";
        if (requestid == "1")//Ticket Booking 
        {
            txnID = Session["TicketNumber"].ToString();
        }
        else if (requestid == "11")//Traveller Topup wallet 
        {
            txnID = Session["transrefno"].ToString();
        }
        else if (requestid == "111")//Ticket Booking From Mobile App
        {
            txnID = Session["strTicketNo"].ToString();
        }
        else if (requestid == "1111")//Traveller Topup wallet  From Mobile App
        {
            txnID = Session["transrefno"].ToString();
        }
        else if (requestid == "2")//Agent Topup wallet
        {
            txnID = Session["transrefno"].ToString();
        }
        else if (requestid == "3")//Bus Passes
        {
            txnID = Session["currtranrefno"].ToString();
        }

        else if (requestid == "22")//Agent Pay Security Fee
        {
            txnID = Session["transrefno"].ToString();
        }
        else
        {
            Session["_ErrorMsg"] = "Invalid Transaction";
            Response.Redirect("../errorpage.aspx");
        }


        

        //HttpRequest requestNew = HttpContext.Current.Request;
        HttpContext context = HttpContext.Current;
        // Get the browser capabilities
        string AcceptHeader = context.Request.Headers["Accept"];
        string InitChannel = context.Request.Headers["init_channel"];
        string IP = context.Request.UserHostAddress;
        string UserAgent = context.Request.UserAgent;
        string BrowserLanguage = context.Request.UserLanguages[0];
        bool IsJavaScriptEnabled = true;


        string PathAndQuery = Request.Url.PathAndQuery;
        string url_host = Request.Url.AbsoluteUri.Replace(PathAndQuery, "");
        string resUrl = url_host + "/starbusarn_audit/PG/bill_desk_Response.aspx?OID=" + txnID + "";
        //string resUrl = url_host + "/PG/bill_desk_Response.aspx";

        ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
        SHA256Managed hashString = new SHA256Managed();
        DateTime Ddate = DateTime.Now;
        String merchatId = merchntidnew;
        String clientId = clientidnew;
        String returnUrl = resUrl;
        String secretKey = secretkeynew;
        String billdesk_url = MurlNew;
        string txnid1 = txnID;

        Device device_obj = new Device();
        device_obj.init_channel = "internet";
        device_obj.ip = IP;
        device_obj.user_agent = UserAgent;
        device_obj.accept_header = "text/html";
        device_obj.fingerprintid = "61b12c18b5d0cf901be34a23ca64bb19";
        device_obj.browser_tz = "-330";
        device_obj.browser_color_depth = "32";
        device_obj.browser_javascript_enabled = "true";
        device_obj.browser_java_enabled = "false";
        device_obj.browser_language = BrowserLanguage;
        device_obj.browser_screen_height = "601";
        device_obj.browser_screen_width = "657";//657


        AdditionalInfo additionalInfo = new AdditionalInfo();
        additionalInfo.additional_info1 = "xyz";
        additionalInfo.additional_info2 = "abc";
        additionalInfo.additional_info3 = "abc";
        payload Payload = new payload();

        if (requestid == "1")//Ticket Booking 
        {
            wsClass obj = new wsClass();
            DataTable dt = new DataTable();
            dt = obj.getJourneyDetails(Session["TicketNumber"].ToString(), "R");

            if (dt.Rows.Count > 0)
            {
                Payload.orderid = dt.Rows[0]["_ticketno"].ToString();
                billing_name = "Ticket Booking";
                billing_tel = dt.Rows[0]["traveller_mobile_no"].ToString();
                billing_email = dt.Rows[0]["traveller_email_id"].ToString();
                decimal amount1 = Convert.ToDecimal(dt.Rows[0]["amount_total"].ToString());
                Payload.amount = amount1.ToString("0.##");
            }
        }
        else if (requestid == "11")//Traveller Topup wallet 
        {
            Payload.orderid = Session["transrefno"].ToString();
            decimal amount1 = Convert.ToDecimal(Session["depositamt"].ToString());
            Payload.amount = amount1.ToString("0.##");
            billing_name = "Traveller Wallet Topup";
            billing_tel = Session["_UserCode"].ToString();
            billing_email = "";
        }
        else if (requestid == "111")//Ticket Booking From Mobile App
        {
            Payload.orderid = Session["strTicketNo"].ToString();
            decimal amount1 = Convert.ToDecimal(Session["_UTCTicketFinalAmount"].ToString());
            Payload.amount = amount1.ToString("0.##");
            billing_name = "Ticket Booking";
            billing_tel = Session["_pmtCTZMobile"].ToString();
            billing_email = Session["_pmtCTZEMAIL"].ToString();
        }
        else if (requestid == "1111")//Traveller Topup wallet  From Mobile App
        {
            Payload.orderid = Session["transrefno"].ToString();
            decimal amount1 = Convert.ToDecimal(Session["depositamt"].ToString());
            Payload.amount = amount1.ToString("0.##");
            billing_name = "Traveller Wallet Topup";
            billing_tel = Session["_UserCode"].ToString();
            billing_email = "";
        }
        else if (requestid == "2")//Agent Topup wallet
        {
            Payload.orderid = Session["transrefno"].ToString();
            decimal amount1 = Convert.ToDecimal(Session["depositamt"].ToString());
            Payload.amount = amount1.ToString("0.##");
            billing_name = "Agent Recharge Account";
            billing_tel = Session["MobileNo"].ToString();
            billing_email = Session["Emailid"].ToString();
        }
else if (requestid == "222")//Agent Topup wallet ETM
        {
            Payload.orderid = Session["transrefno"].ToString();
            decimal amount1 = Convert.ToDecimal(Session["depositamt"].ToString());
            Payload.amount = amount1.ToString("0.##");
            billing_name = "Agent Recharge Account";
            billing_tel = Session["MobileNo"].ToString();
            billing_email = Session["Emailid"].ToString();
        }
        else if (requestid == "3")//Bus Passes
        {
            Payload.orderid = Session["currtranrefno"].ToString();
            decimal amount1 = Convert.ToDecimal(Session["AMOUNT"].ToString());
            Payload.amount = amount1.ToString("0.##");
            billing_name = "Bus Pass";
            billing_tel = Session["MobileNo"].ToString();
            billing_email = Session["Emailid"].ToString();
        }

        else if (requestid == "22")//Agent Pay Security Fee
        {
            Payload.orderid = Session["transrefno"].ToString();
            Payload.amount = Convert.ToDecimal(Session["depositamt"].ToString()).ToString();
            billing_name = "Agent Pay Security Fee";
            billing_tel = Session["MobileNo"].ToString();
            billing_email = Session["Emailid"].ToString();
        }
        else
        {
            Session["_ErrorMsg"] = "Invalid Transaction";
            Response.Redirect("../errorpage.aspx");
        }



        Payload.mercid = merchatId;
        //Payload.orderid = txnID;
        //Payload.amount = "123";
        Payload.order_date = DateTime.Now.ToString("yyyy-MM-dd\"T\"HH:mm:sszzz");
        Payload.currency = "356";
        Payload.ru = returnUrl;
        Payload.itemcode = "DIRECT";
        Payload.device = device_obj;
        Payload.additional_info = additionalInfo;

        //DataTable dtt = BL.PaymentStart(Payload.orderid, "3", "P", Payload.amount, "");


        //if (BL.BILLDESKRequest("",merchatId, Payload.orderid,"", Payload.amount, Payload.currency, Payload.order_date,additionalInfo.additional_info1,additionalInfo.additional_info2,
        //    additionalInfo.additional_info3,"","","","2",returnUrl, secretKey,"") == false)
        //{
        //    Session["_ErrorMsg"] = "Invalid Transaction -";
        //    //Response.Redirect("../errorpage.aspx");
        //}

        //Session["pay_orderid"] = Payload.orderid.ToString();
        var json_payload = Newtonsoft.Json.JsonConvert.SerializeObject(Payload);
        string jsone_header = "{\r\n\"alg\":\"HS256\",\r\n\"clientid\":\""+clientidnew+"\"\r\n}";
        string encodedPayload = Base64UrlEncoder.Encode(json_payload);
        string encodedHeader = Base64UrlEncoder.Encode(jsone_header);
        var sha256 = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey));
        string combinedData = encodedHeader + "." + encodedPayload;
        var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(combinedData));
        string hash = Convert.ToBase64String(hashBytes).Replace('+', '-').Replace('/', '_').Replace("=", "");
        var jwt = encodedHeader + "." + encodedPayload + "." + hash;



        




        HttpClientHandler handler = new HttpClientHandler();
        handler.AutomaticDecompression = DecompressionMethods.None;
        HttpClient client = new HttpClient(handler);
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, billdesk_url);
        request.Headers.Add("accept", "application/jose");
        request.Headers.Add("BD-Traceid", txnid1);
        request.Headers.Add("BD-Timestamp", Ddate.ToString("yyyyMMddhhmmss"));
        request.Content = new StringContent(jwt);
        request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/jose");
        HttpResponseMessage response = await client.SendAsync(request);
        string responseBody = await response.Content.ReadAsStringAsync();
        var stream = responseBody;
        string[] parts = stream.Split('.');
        string header = parts[0];
        string Resp_payload = parts[1];
        string signature = parts[2];
        //string aa = Base64UrlEncoder.Decode(Resp_payload);

        string trim_tokes = Base64UrlEncoder.Decode(Resp_payload);



        var parsed = JObject.Parse(trim_tokes);
        HiddenField1.Value = (string)parsed.SelectToken("bdorderid");
        HiddenField2.Value = (string)parsed.SelectToken("links[1].headers.authorization");
        HiddenField3.Value = returnUrl;
//Response.Write(Payload.order_date);
        if (bill.BILLDESKRequestNew(requestid, merchatId, Payload.orderid, Payload.amount, Payload.currency, HiddenField1.Value.ToString(),
             "N/A", Ddate.ToString("yyyyMMddhhmmss"), billing_email, billing_tel, billing_name, "PG", resUrl, secretKey, hash, "N/A") == false)
        {
            Session["_ErrorMsg"] = "Invalid Transaction -";
            Response.Redirect("../errorpage.aspx");
        }

    }


    public class Base64UrlEncoder
    {
        public static string Encode(string plainText)
        {
            byte[] plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes)
                .Replace('+', '-')
                .Replace('/', '_')
                .Replace("=", "");
        }

        public static string Decode(string base64UrlString)
        {
            string paddedBase64UrlString = base64UrlString.PadRight(base64UrlString.Length + (4 - base64UrlString.Length % 4) % 4, '=');
            byte[] base64UrlBytes = Convert.FromBase64String(paddedBase64UrlString.Replace('-', '+').Replace('_', '/'));
            return System.Text.Encoding.UTF8.GetString(base64UrlBytes);
        }
    }
}