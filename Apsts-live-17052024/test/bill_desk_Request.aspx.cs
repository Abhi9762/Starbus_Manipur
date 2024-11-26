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
    BilldeskSha billdesk = new BilldeskSha();
    payload payload = new payload();
    BILLDESK BL=new BILLDESK();
    protected async void Page_Load(object sender, EventArgs e)
    {

        Random rnd = new Random();
        int txnID = rnd.Next(1000, 10000);  

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
        string resUrl = url_host + "/BILLDESK_PG/bill_desk_Response.aspx?OID=TSSGF4"+txnID +"";

        ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
        SHA256Managed hashString = new SHA256Managed();
        DateTime Ddate = DateTime.Now;
        String merchatId = "ARUNACUAT";
        String clientId = "arunacuat";
        String returnUrl = resUrl;
        String secretKey = "bnuEN7aXpEolZxlllNdZQ0cLAaFbXOLx";
        String billdesk_url = "https://uat1.billdesk.com/u2/payments/ve1_2/orders/create";
        string txnid1 = "TSSGF4" + txnID;

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
        device_obj.browser_screen_width = "657";


        AdditionalInfo additionalInfo = new AdditionalInfo();
        additionalInfo.additional_info1 = "xyz";
        additionalInfo.additional_info2 = "abc";
        additionalInfo.additional_info3 = "abc";


        payload Payload = new payload();
        Payload.mercid = merchatId;
        Payload.orderid = "TSSGF4"+ txnID;
        Payload.amount = "123";
        Payload.order_date = DateTime.Now.ToString("yyyy-MM-dd\"T\"HH:mm:sszzz");
        Payload.currency = "356";
        Payload.ru = returnUrl;
        Payload.itemcode = "DIRECT";
        Payload.device = device_obj;
        Payload.additional_info = additionalInfo;

        //DataTable dtt = BL.PaymentStart(Payload.orderid, "3", "P", Payload.amount, "");


        //if (BL.BILLDESKRequest("",merchatId, Payload.orderid,"", Payload.amount, Payload.currency, Payload.order_date,additionalInfo.additional_info1,additionalInfo.additional_info2,
        //    additionalInfo.additional_info3,"","","","2",returnUrl, secretKey,"") == false)
       // {
        //    Session["_ErrorMsg"] = "Invalid Transaction -";
            //Response.Redirect("../errorpage.aspx");
       // }

        Session["pay_orderid"] = Payload.orderid.ToString();
        var json_payload = Newtonsoft.Json.JsonConvert.SerializeObject(Payload);
        string jsone_header = "{\r\n\"alg\":\"HS256\",\r\n\"clientid\":\"arunacuat\"\r\n}";
        //var encodedPayload = Base64UrlEncoder.Encode(json_payload);
        //byte[] btyPayload = System.Convert.FromBase64String(json_payload);

        //var btyPayload = Encoding.UTF8.GetBytes(json_payload);

        // Convert the bytes to Base64 string
        //string base64String = Convert.ToBase64String(jsonBytes);


        string encodedPayload = Base64UrlEncoder.Encode(json_payload);

        //var encodedHeader = jsone_header;
        //var btyHeader = Encoding.UTF8.GetBytes(jsone_header);
        string encodedHeader = Base64UrlEncoder.Encode(jsone_header);
        //string encodedHeader = System.Text.Encoding.UTF8.GetString(btyHeader);


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
Response.Write(Payload.order_date);
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