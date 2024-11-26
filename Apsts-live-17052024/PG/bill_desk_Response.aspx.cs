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
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BILLDESK_PG_bill_desk_Response : System.Web.UI.Page
{
    HDFC hdfc = new HDFC();
    BILLDESK BL = new BILLDESK();
    sbSecurity _security = new sbSecurity();
    string TxnAmount;

    string merchntidnew = "ARUNACUAT";
    string secretkeynew = "bnuEN7aXpEolZxlllNdZQ0cLAaFbXOLx";
    string clientidnew = "arunacuat";
    public string MurlNew = "https://uat1.billdesk.com/u2/payments/ve1_2/transactions/get";
    public string MrefundurlNew = "https://uat1.billdesk.com/u2/payments/ve1_2/refunds/create";
    protected async void Page_Load(object sender, EventArgs e)
    {

        ScriptManager.RegisterStartupScript(this, GetType(), "CloseWindow", "window.close();", true);

        if (!IsPostBack)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            string url = "https://uat1.billdesk.com/u2/payments/ve1_2/transactions/get";
            String merchatId = merchntidnew;
            //string pay_orderid = Session["pay_orderid"].ToString();
            string pay_orderid = Request.QueryString["OID"].ToString();
            string txnid1 = DateTime.Now.ToString("yyyyMMddhhmmss");
            DateTime Ddate = DateTime.Now;
            String secretKey = secretkeynew;
            string jsone_header = "{\r\n\"alg\":\"HS256\",\r\n\"clientid\":\""+clientidnew+"\"\r\n}";
            string json_payload = "{\r\n\"mercid\":\""+merchntidnew+"\",\r\n\"orderid\":\"" + pay_orderid +
            "\"\r\n}";
            var encodedPayload = Base64UrlEncoder.Encode(json_payload);
            var encodedHeader = Base64UrlEncoder.Encode(jsone_header);
            var sha256 = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey));
            string combinedData = encodedHeader + "." + encodedPayload;
            var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(combinedData));
            string hash = Convert.ToBase64String(hashBytes).Replace('+', '-').Replace('/', '_').Replace("=", "");
            var jwt = encodedHeader + "." + encodedPayload + "." + hash;
            HttpClientHandler handler = new HttpClientHandler();
            handler.AutomaticDecompression = DecompressionMethods.None;
            HttpClient client = new HttpClient(handler);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Add("accept", "application/jose");
            request.Headers.Add("BD-Traceid", txnid1);
            request.Headers.Add("BD-Timestamp", Ddate.ToString("yyyyMMddhhmmss"));
            request.Content = new StringContent(jwt);
            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/jose");
            HttpResponseMessage response = await client.SendAsync(request);
            string responseBody = await response.Content.ReadAsStringAsync();
            var stream = responseBody; // responseBody is encoded return
            string[] parts = stream.Split('.');
            string header = parts[0];
            string Resp_payload = parts[1];
            string signature = parts[2];
            var trim_tokes = Base64UrlEncoder.Decode(Resp_payload);
            var parsed = JObject.Parse(trim_tokes);
            string transaction_error_desc = (string)parsed.SelectToken("transaction_error_desc");
            string transactionid = (string)parsed.SelectToken("transactionid");
            string additional_info2 = (string)parsed.SelectToken("additional_info.additional_info2");
            string additional_info1 = (string)parsed.SelectToken("additional_info.additional_info1");
            string val1 = (string)parsed.SelectToken("transactionid");
            string transDate = (string)parsed.SelectToken("transaction_date");
            string bankID = (string)parsed.SelectToken("bankid");
            string bankRefNo = (string)parsed.SelectToken("bank_ref_no");
            string amount = (string)parsed.SelectToken("charge_amount");
            TxnAmount = amount;
            string paymentType = (string)parsed.SelectToken("payment_method_type");
            string processType = (string)parsed.SelectToken("txn_process_type");
            string errorType = (string)parsed.SelectToken("transaction_error_type");
            string additionalInfo1 = (string)parsed.SelectToken("additional_info.additional_info1");
            string currency = (string)parsed.SelectToken("currency");
            string auth_Status = (string)parsed.SelectToken("auth_status");

            DataTable dt = new DataTable();
            dt = BL.BILLDESKResponseNew(merchatId, pay_orderid, transactionid, bankRefNo, amount, bankID, merchatId, processType, currency, "", "", "", "", transDate
                , auth_Status, paymentType, errorType, transaction_error_desc, "", errorType, secretKey, "");
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToDecimal(dt.Rows[0]["amt"].ToString()) != Convert.ToDecimal(amount))
                {
                    Session["_ErrorMsg"] = "Request and Response Amount Not Same";
                    Response.Redirect("../errorpage.aspx");
                    return;
                }
                pay_orderid = dt.Rows[0]["order_no"].ToString();
                string requestId = dt.Rows[0]["request_no"].ToString();
                if (auth_Status == "0300")
                {
                    SuccessPageRedirect(pay_orderid, requestId);
                }
                else
                {
                    FailedPageRedirect(pay_orderid, requestId, additionalInfo1);
                }

            }
            else
            {
                Response.Redirect("../errorpage.aspx");
            }
            //if (auth_Status == "0300")
            //{
            //    string script = "alert('Transaction completed successfully !');";
            //    ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
            //}
            //else if(auth_Status == "0002")
            //{
            //    string script = "alert('Transaction is pending for authorization !');";
            //    ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);

            //}
            //else if(auth_Status == "0399")
            //{
            //    string script = "alert('Transaction failed !');";
            //    ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
            //}
        }
    }
    private void SuccessPageRedirect(string orderId, string requestId)
    {
        try
        {
            DataTable dtUser = hdfc.getUserDetails_after_PG(orderId, requestId);
            if (dtUser.Rows.Count > 0)
            {
                if (requestId == "1")
                {
                    Session["_moduleName"] = "Ticket Print";
                    Session["TicketNumber"] = orderId;
                    Session["_UserName"] = dtUser.Rows[0]["p_username"].ToString();
                    Session["_UserCode"] = dtUser.Rows[0]["p_usercode"].ToString();
                    Session["_UserROLECODE"] = dtUser.Rows[0]["p_userrolecode"].ToString();
                    Session["_UserType"] = dtUser.Rows[0]["p_usertype"].ToString();
                    Session["_acctStatus"] = dtUser.Rows[0]["p_useraccstatus"].ToString();
                    Session["Mobileno"] = dtUser.Rows[0]["p_usermobile"].ToString();
                    CreateCookie(Session["_UserCode"].ToString());
                    Session["_RoleCode"] = "102";
                    Session["_RNDIDENTIFIERSTRVL"] = _security.GeneratePassword(10, 5);
                    Response.Redirect("../traveller/ticketStatus.aspx", false);
                }
                else if (requestId == "11")
                {
                    wsClass obj = new wsClass();
                    DataTable mytable = new DataTable();
                    Session["strTicketNo"] = orderId;
                    Session["_UserName"] = dtUser.Rows[0]["p_username"].ToString();
                    Session["_UserCode"] = dtUser.Rows[0]["p_usercode"].ToString();
                    Session["_UserROLECODE"] = dtUser.Rows[0]["p_userrolecode"].ToString();
                    Session["_UserType"] = dtUser.Rows[0]["p_usertype"].ToString();
                    Session["_acctStatus"] = dtUser.Rows[0]["p_useraccstatus"].ToString();
                    Session["Mobileno"] = dtUser.Rows[0]["p_usermobile"].ToString();

                    //  mytable = obj.saveWalletTxn(Session["_UserCode"].ToString(), bank_ref_no, "", "T", Session["strTicketNo"].ToString(), amount.ToString());
                    //if (mytable.Rows.Count > 0)
                    //{
                    Session["walletUpdateMsg"] = "DONE";
                    //}
                    //else
                    //{
                    //    Session["walletUpdateMsg"] = "No Data Found";
                    //}
                    CreateCookie(Session["_UserCode"].ToString());
                    Session["STATUS"] = "S";
                    Session["_RoleCode"] = "102";
                    Session["_RNDIDENTIFIERSTRVL"] = _security.GeneratePassword(10, 5);
                    Response.Redirect("../traveller/walletTopupStatus.aspx?amount=" + TxnAmount, false);
                }
                else if (requestId == "111")
                {
                    Session["TicketNumber"] = orderId;
                    Response.Redirect("../pathikwebpage/ticketstatus.aspx?tk=" + orderId, false);
                }
                else if (requestId == "1111")
                {

                    Session["_UserCode"] = "";
                    Session["AMOUNT"] = "";
                    Session["TxnReferenceNumber"] = orderId;
                    Session["STATUS"] = "S";
                    Response.Redirect("../pathikwebpage/walletTopupStatus.aspx?amount=" + TxnAmount, false);
                }
                else if (requestId == "2")//Agent Topup wallet
                {
                    //Session["_UserRoleCode"] = dtUser.Rows[0]["p_userrolecode"].ToString();
                    string UserCode = dtUser.Rows[0]["p_usercode"].ToString();
                    Session["_UserCode"] = UserCode;
                    Session["_UserName"] = dtUser.Rows[0]["p_username"].ToString();
                    Session["MobileNo"] = dtUser.Rows[0]["p_usermobile"].ToString();
                    Session["Emailid"] = dtUser.Rows[0]["p_useremail"].ToString();
                    Session["_LLoginDTTime"] = DateTime.Now.ToString();
                    Session["transrefno"] = orderId;
                    Session["_acctStatus"] = "A";
                    CreateCookie(Session["_UserCode"].ToString());

                    if (dtUser.Rows[0]["p_agenttype"].ToString() == "3")
                    {
                        Response.Redirect("../Auth/MainCscWallet.aspx", false);
                    }
                    else
                    {
                        Response.Redirect("../Auth/AGAuthPrintReceipt.aspx", false);
                    }
                }
else if (requestId == "222")//Agent Topup wallet From ETM App
                {
                    Response.RedirectPermanent("../pathikwebpage/agent_walletTopupStatus.aspx", false);
                }
                else if (requestId == "22")//Agent Security Amount Deposit
                {

                    Session["_UserCode"] = dtUser.Rows[0]["p_usercode"].ToString();
                    Session["_UserName"] = dtUser.Rows[0]["p_username"].ToString();
                    Session["MobileNo"] = dtUser.Rows[0]["p_usermobile"].ToString();
                    Session["Emailid"] = dtUser.Rows[0]["p_useremail"].ToString();
                    Response.Redirect("../OnlAgPaymentSuccess.aspx", false);
                }
                else if (requestId == "3")//Bus Passes
                {
                    Session["transrefno"] = orderId;
                    Session["Passno"] = dtUser.Rows[0]["p_passno"].ToString();
                    Session["currtranrefno"] = dtUser.Rows[0]["p_trnstype"].ToString();
                    Session["IssuanceType"] = dtUser.Rows[0]["p_issuetype"].ToString();
                    Session["MobileNo"] = dtUser.Rows[0]["p_usermobile"].ToString();
                    Response.Redirect("../PassStatus.aspx", false);
                }
                else
                {
                    Session["_ErrorMsg"] = "You have wrong request ID";
                    Response.Redirect("../Bkng/bkngError.aspx");
                }
            }
            else
            {
                Session["_ErrorMsg"] = "You have wrong request ID";
                Response.Redirect("../errorpage.aspx");
            }
        }
        catch (Exception ex)
        {
            Session["_ErrorMsg"] = ex.Message.ToString();
            Response.Redirect("../errorpage.aspx");
        }
    }
    private void FailedPageRedirect(string orderId, string requestId, string failure_message)
    {
        try
        {
            DataTable dtUser = hdfc.getUserDetails_after_PG(orderId, requestId);
            if (requestId == "1" || requestId == "11")
            {

                if (dtUser.Rows.Count > 0)
                {
                    Session["_ErrorMsg"] = failure_message;
                    Session["_UserCode"] = dtUser.Rows[0]["p_usercode"].ToString();
                    CreateCookie(Session["_UserCode"].ToString());
                    Response.Redirect("../Traveller/travellerPmtError.aspx", false);
                }
                else
                {
                    Session["_ErrorMsg"] = "You have wrong request ID";
                    Response.Redirect("../errorpage.aspx");
                }

            }
            else if (requestId == "111")
            {
                Response.Redirect("../pathikWebPage/error.aspx", false);
            }
            else if (requestId == "1111")
            {
                Response.Redirect("../pathikWebPage/error.aspx", false);
            }
 else if (requestId == "222")
            {
                Response.Redirect("../pathikWebPage/error.aspx", false);
            }
            else if (requestId == "2")
            {
                if (dtUser.Rows.Count > 0)
                {
                    string UserCode = dtUser.Rows[0]["p_usercode"].ToString();
                    Session["_UserCode"] = UserCode;
                    Session["_UserName"] = dtUser.Rows[0]["p_username"].ToString();
                    Session["MobileNo"] = dtUser.Rows[0]["p_useremail"].ToString();
                    Session["Emailid"] = dtUser.Rows[0]["userEmail"].ToString();
                    Session["_LLoginDTTime"] = DateTime.Now.ToString();
                    CreateCookie(Session["_UserCode"].ToString());
                    Session["_ErrorMsg"] = failure_message;


                    Response.Redirect("../Auth/AGPmtError.aspx", false);
                }
                else
                {
                    Session["_ErrorMsg"] = "You have wrong request ID";
                    Response.Redirect("../errorpage.aspx");
                }
            }


            else if (requestId == "22")//Agent Security Amount Deposit
            {
                Session["_ErrorMsg"] = "You have wrong request ID";
                Response.Redirect("../errorpage.aspx");
            }

            else if (requestId == "3")
            {
                Session["_ErrorMsg"] = failure_message;
                Response.Redirect("../passpmterror.aspx", false);
            }
            else
            {
                Session["_ErrorMsg"] = "You have wrong request ID";
                Response.Redirect("../errorpage.aspx");
            }
        }
        catch (Exception ex)
        {
            Session["_ErrorMsg"] = ex.Message.ToString();
            Response.Redirect("../errorpage.aspx");
        }
    }
    private void CreateCookie(string mobileNo)
    {
        try
        {
            Session["_UserCode"] = mobileNo;
            System.Security.Cryptography.MD5CryptoServiceProvider SecMD5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            Random _rndmNo = new Random();

            FormsAuthentication.Initialize();
            FormsAuthenticationTicket tkt;
            string cookiestr;
            HttpCookie ck;

            tkt = new FormsAuthenticationTicket(1, "etktFormsAspx", DateTime.Now, DateTime.Now.AddSeconds(5), false, mobileNo); // , lstDistrict.SelectedValue)
            cookiestr = FormsAuthentication.Encrypt(tkt);
            ck = new HttpCookie(FormsAuthentication.FormsCookieName, cookiestr);
            ck.Path = FormsAuthentication.FormsCookiePath;
            Response.Cookies.Remove(FormsAuthentication.FormsCookieName);
            Response.Cookies.Add(ck);

            HttpCookie cookie = Request.Cookies[".etktFormsAspx"];
            Session["_eTktTicketID"] = cookie.Value;
            HttpCookie MRIIdentifierCookie = new HttpCookie("CRIIdentifier", BitConverter.ToString(SecMD5.ComputeHash(Encoding.ASCII.GetBytes(_rndmNo.Next().ToString()))));
            Response.Cookies.Add(MRIIdentifierCookie);

            FormsAuthentication.Initialize();
            FormsAuthenticationTicket a = new FormsAuthenticationTicket(1, mobileNo, DateTime.Now, DateTime.Now.AddMinutes(20), false, mobileNo, FormsAuthentication.FormsCookiePath);
            HttpCookie ck1 = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(a));
            Response.Cookies.Add(ck1);
            Session["_eTktTicketID"] = ck1.Value;
            Response.Redirect("Rating.aspx", false);
        }
        catch (Exception ex)
        {
            Session["_ErrorMsg"] = ex.Message.ToString();
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