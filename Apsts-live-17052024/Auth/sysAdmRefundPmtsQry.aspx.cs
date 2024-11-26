using Newtonsoft.Json.Linq;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_sysAdmRefundPmtsQry : System.Web.UI.Page
{
    private NpgsqlCommand MyCommand;
    sbValidation _SecurityCheck = new sbValidation();
    sbSecurity _security = new sbSecurity();
    HDFC hdfc = new HDFC();
    sbValidation _validation = new sbValidation();
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    ClassDoubleVerification_Refund dbl_refund = new ClassDoubleVerification_Refund();
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["_moduleName"] = "Refund Transaction Settlement";
        checkForSecurity();
        if (!IsPostBack)
        {
            Session["Query"] = "";
            if (Session["MPMTGATEWAYID"] != null && Session["MPMTGATEWAYID"].ToString() != "" && Session["Mtxndate"] != null && Session["Mtxndate"].ToString() != "")
            {
                lblTxnDate.Text = Session["Mtxndate"].ToString();
                lblPg.Text = Session["MPMTGATEWAYNAME"].ToString();
                RefundTransactions(Session["MPMTGATEWAYID"].ToString(), Session["Mtxndate"].ToString());
            }

        }
    }

    #region "Common Methods"
    private void checkForSecurity()
    {
        //if (Session.IsNewSession == false || Request.ServerVariables["HTTP_REFERER"].Length < 1)
        //{
        //    Response.Redirect("../Errorpage.aspx");
        //}

        if (_security.isSessionExist(Session["_UserCode"]) == true)
        {
            Session["_UserCode"] = Session["_UserCode"];
        }
        else
        {
            Response.Redirect("sessionTimeout.aspx");
        }
        if (_security.isSessionExist(Session["_RNDIDENTIFIERSADM"]) == true)
        {
            Session["_RNDIDENTIFIERSADM"] = Session["_RNDIDENTIFIERSADM"];
        }
        else
        {
            Response.Redirect("sessionTimeout.aspx");
        }
        if (_security.checkvalidation() == false)
        {
            Response.Redirect("Errorpage.aspx");
        }
        if (_security.CheckOldUserLogin(Session["_UserCode"].ToString()) == false)
        {
            Response.Redirect("Errorpage.aspx");
        }
    }
    private void Successmsg(string msg)
    {
        string popup = _popup.modalPopupSmall("S", "Success", msg, "Close");
        Response.Write(popup);
    }
    private void Errormsg(string msg)
    {
        string popup = _popup.modalPopupSmall("D", "Please Check", msg, "Close");
        Response.Write(popup);
    }
    #endregion

    #region "Methods"   
    private void RefundTransactions(string pgid, string date)
    {
        try
        {
            gvRefundTrans.Visible = false;
            dvRefund.Visible = true;
            btnStart.Visible = false;

            dt = dbl_refund.getRefundTxnDtls(pgid, date);
            if (dt.Rows.Count > 0)
            {
                gvRefundTrans.DataSource = dt;
                gvRefundTrans.DataBind();
                gvRefundTrans.Visible = true;
                dvRefund.Visible = false;
                btnStart.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Errormsg("Something Went Wrong");
        }
    }
    private async void StartQuery()
    {
        string MfinalStatus = "Error";
        string Mtranno, MCancelRef, bankTransactionID, Mtranamt, txndate,MRefundnamt;
        try
        {
            int selectedIndex = int.Parse(hdgrdindex.Value.ToString());
            Mtranno = (gvRefundTrans.DataKeys[selectedIndex]["txn_refno"]).ToString();
            MCancelRef = (gvRefundTrans.DataKeys[selectedIndex]["txn_cancel_refno"]).ToString();
            bankTransactionID = (gvRefundTrans.DataKeys[selectedIndex]["bank_txn_refno"]).ToString();
            Mtranamt = (gvRefundTrans.DataKeys[selectedIndex]["txn_actual_amt"]).ToString();
            txndate = (gvRefundTrans.DataKeys[selectedIndex]["txndatee"]).ToString();
            MRefundnamt = (gvRefundTrans.DataKeys[selectedIndex]["txn_amt"]).ToString();

            

            if (Session["MPMTGATEWAYID"].ToString() == "1")
            {
                //***** Phone Pay Refund
                MfinalStatus = dbl_refund.PhonePeRefundAPI(Mtranno, bankTransactionID, MRefundnamt, MCancelRef, Session["_UserCode"].ToString());

                if (MfinalStatus.ToUpper() == "SUCCESS")
                {
                    Errormsg("Refund request has successfully been initiated.");
                }
                else
                {
                    Errormsg("Refund Not Initiated");
                }
            }
            if (Session["MPMTGATEWAYID"].ToString() == "4")
            {
                string inputDate = txndate;

                // Parse the input string into a DateTime object
                DateTime dateTime = DateTime.ParseExact(inputDate, "MM/dd/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

                // Format the DateTime object into ISO 8601 format
                string iso8601DateTime = dateTime.ToString("yyyy-MM-dd'T'HH:mm:sszzz");
                //***** Billdesk Refund
                MfinalStatus =await dbl_refund.BilldeskRefundAPI(bankTransactionID, MCancelRef, Mtranno, Mtranamt, Session["_UserCode"].ToString(), iso8601DateTime, MRefundnamt);
               
                if (MfinalStatus.ToUpper() == "SUCCESS")
                {
                    Errormsg("Refund request has successfully been initiated.");
                }
                else
                {
                    Errormsg("Refund Not Initiated");
                }
            }
            RefundTransactions(Session["MPMTGATEWAYID"].ToString(), Session["Mtxndate"].ToString());
        }
        catch (Exception ex)
        {
            Errormsg("Refund Not Initiated" + ex.Message);
        }
    }
    private async void StartQueryProcess()
    {
        string MfinalStatus = "Error";
        string Mtranno, MCancelRef, bankTransactionID, Mtranamt,txndate, MRefundnamt;

        if (gvRefundTrans.Rows.Count <= 0)
        {
            Errormsg ("Sorry, no transaction is available for verification");
            return;
        }
        int j = gvRefundTrans.Rows.Count;
        int k = 0;
        for (var i = 0; i <= gvRefundTrans.Rows.Count - 1; i++)
        {
            Mtranno = (gvRefundTrans.DataKeys[i]["txn_refno"]).ToString();
            MCancelRef = (gvRefundTrans.DataKeys[i]["txn_cancel_refno"]).ToString();
            bankTransactionID = (gvRefundTrans.DataKeys[i]["bank_txn_refno"]).ToString();
            Mtranamt = (gvRefundTrans.DataKeys[i]["txn_actual_amt"]).ToString();
            txndate = (gvRefundTrans.DataKeys[i]["txndatee"]).ToString();
            MRefundnamt = (gvRefundTrans.DataKeys[i]["txn_amt"]).ToString();


            string inputDate = txndate;

            // Parse the input string into a DateTime object
            DateTime dateTime = DateTime.ParseExact(inputDate, "MM/dd/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

            // Format the DateTime object into ISO 8601 format
            string iso8601DateTime = dateTime.ToString("yyyy-MM-dd'T'HH:mm:sszzz");

            // ----------------------------
            //if (dbl_refund.checkRefundRequest(Session["MPMTGATEWAYID"], Mtranno, MCancelRef) == true)
            //    continue;

            if (Session["MPMTGATEWAYID"].ToString() == "1")
            {
                //***** Phone Pay Refund
                MfinalStatus = dbl_refund.PhonePeRefundAPI(Mtranno, bankTransactionID, MRefundnamt, MCancelRef, Session["_UserCode"].ToString());               
            }
            if (Session["MPMTGATEWAYID"].ToString() == "4")
            {
                //***** Billdesk Refund
                MfinalStatus = await dbl_refund.BilldeskRefundAPI(bankTransactionID, MCancelRef, Mtranno, Mtranamt, Session["_UserCode"].ToString(), iso8601DateTime, MRefundnamt);

          
            }


            if (MfinalStatus == "Success")
            {
                k = k + 1;
               // comm.refundIniate(Mtranno, Mtranamt, 26);
            }
        }

         RefundTransactions(Session["MPMTGATEWAYID"].ToString(), Session["Mtxndate"].ToString());
        string mm = "<b>" + k.ToString() + "</b> out of <b>" + j.ToString() + "</b> transasctions Refund request has successfully been initiated.";
        Errormsg (mm);
    }


    #endregion

    #region "Events"
    protected void lbtnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("sysAdmPmtGateway.aspx");
    }
    protected void gvRefundTrans_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvRefundTrans.PageIndex = e.NewPageIndex;
        RefundTransactions(Session["MPMTGATEWAYID"].ToString(), Session["Mtxndate"].ToString());
    }
    protected void gvRefundTrans_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Refund")
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int i = row.RowIndex;
            hdgrdindex.Value = i.ToString();
            Session["Query"] = "";
            lblConfirmation.Text = "Do you want to proceed for Refund the transactions ?";
            mpConfirmation.Show();
        }
    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        if (Session["Query"] == "Q")
        {
            StartQueryProcess();
        }
        else
        {
            StartQuery();
        }
    }
    protected void btnStart_Click(object sender, EventArgs e)
    {
        Session["Query"] = "Q";
        lblConfirmation.Text = "Do you want to proceed for Refund the transactions ?";
        mpConfirmation.Show();
    }




    #endregion



    //public async Task<string> BilldeskRefundAPI(string transaction_id, string cancelation_ref_no, string order_id, string txn_amount, string refundby)
    //{


    //    Random rnd = new Random();
    //    int txnID = rnd.Next(100, 100000);
    //    cancelation_ref_no = "REFUND" + txnID.ToString();
    //    HttpRequest requestNew = HttpContext.Current.Request;
    //    txn_amount = txn_amount + ".00";
    //    Get the browser capabilities
    //    HttpBrowserCapabilities browser = requestNew.Browser;

    //    string browserType = browser.Type;
    //    string browserBrowser = browser.Browser;
    //    string browserVerison = browser.Version;
    //    int MajorVersion = browser.MajorVersion;
    //    double MinorVersion = browser.MinorVersion;
    //    bool isMobile = browser.IsMobileDevice;


    //    string osName = Environment.OSVersion.Platform.ToString();

    //    Get OS version
    //    string osVersion = Environment.OSVersion.Version.ToString();

    //    Get device type(assuming Windows / Linux for simplicity)
    //        string deviceType = Environment.OSVersion.Platform == PlatformID.Win32NT ? "PC" : "Unknown";
    //    string PathAndQuery = Request.Url.PathAndQuery;
    //    string url_host = Request.Url.AbsoluteUri.Replace(PathAndQuery, "");
    //    string resUrl = url_host + "/BILLDESK_PG/bill_desk_Response.aspx?OID=REFTS14RTEST" + txnID + "";

    //    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
    //    SHA256Managed hashString = new SHA256Managed();
    //    DateTime Ddate = DateTime.Now;
    //    String merchatId = "ARUNACUAT";
    //    String clientId = "arunacuat";
    //    String returnUrl = resUrl;
    //    String secretKey = "bnuEN7aXpEolZxlllNdZQ0cLAaFbXOLx";
    //    String billdesk_url = "https://uat1.billdesk.com/u2/payments/ve1_2/refunds/create";
    //    string refund_ref_no = "REFTS14R" + txnID;

    //    refundPayload refundPayload = new refundPayload();
    //    refundPayload.transactionid = transaction_id;
    //    refundPayload.orderid = order_id;
    //    refundPayload.mercid = merchatId;
    //    refundPayload.transaction_date = "2024-06-10T14:17:08+05:30";// DateTime.Now.ToString("yyyy-MM-dd\"T\"HH:mm:sszzz"); ///2024-06-10T16:10:00+05:30

    //    06 / 10 / 2024 14:17:08

    //    refundPayload.txn_amount = txn_amount;
    //    refundPayload.refund_amount = txn_amount;
    //    refundPayload.currency = "356";

    //    refundPayload.merc_refund_ref_no = cancelation_ref_no;


    //    var json_payload = Newtonsoft.Json.JsonConvert.SerializeObject(refundPayload);
    //    string jsone_header = "{\r\n\"alg\":\"HS256\",\r\n\"clientid\":\"arunacuat\"\r\n}";

    //    string encodedPayload = Encode(json_payload);

    //    string encodedHeader = Encode(jsone_header);
    //    var sha256 = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey));

    //    string combinedData = encodedHeader + "." + encodedPayload;
    //    var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(combinedData));
    //    string hash = Convert.ToBase64String(hashBytes).Replace('+', '-').Replace('/', '_').Replace("=", "");
    //    var jwt = encodedHeader + "." + encodedPayload + "." + hash;

    //    HttpClientHandler handler = new HttpClientHandler();
    //    handler.AutomaticDecompression = DecompressionMethods.None;
    //    HttpClient client = new HttpClient(handler);
    //    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, billdesk_url);
    //    request.Headers.Add("accept", "application/jose");
    //    request.Headers.Add("BD-Traceid", cancelation_ref_no);
    //    request.Headers.Add("BD-Timestamp", Ddate.ToString("yyyyMMddhhmmss"));
    //    request.Content = new StringContent(jwt);
    //    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/jose");
    //    HttpResponseMessage response = await client.SendAsync(request);
    //    string responseBody = await response.Content.ReadAsStringAsync();
    //    var stream = responseBody;
    //    string[] parts = stream.Split('.');
    //    string header = parts[0];
    //    string Resp_payload = parts[1];
    //    string signature = parts[2];

    //    string trim_tokes = Decode(Resp_payload);


    //    var parsed = JObject.Parse(trim_tokes);
    //    string refund_status = (string)parsed.SelectToken("refund_status") ?? "N/A";
    //    string refund_id = (string)parsed.SelectToken("refundid") ?? "N/A";
    //    string transactionid = (string)parsed.SelectToken("transactionid") ?? "N/A";
    //    string order_idd = (string)parsed.SelectToken("orderid") ?? "N/A";
    //    string transDate = (string)parsed.SelectToken("transaction_date") ?? "N/A";
    //    string txn_amountt = (string)parsed.SelectToken("txn_amount") ?? "N/A";
    //    string refund_amount = (string)parsed.SelectToken("refund_amount") ?? "N/A";
    //    string currency = (string)parsed.SelectToken("currency") ?? "N/A";
    //    string refund_date = (string)parsed.SelectToken("refund_date") ?? "N/A";
    //    string merc_refund_ref_no = (string)parsed.SelectToken("merc_refund_ref_no") ?? "N/A";

    //    refundStatus = await BL.RefundResponseAPI(cancelation_ref_no);
    //    BilldeskRefundRequestt(refundPayload.transactionid, refundPayload.orderid, merchatId, refundPayload.transaction_date, refundPayload.txn_amount, refundPayload.refund_amount, refundPayload.currency, refundPayload.merc_refund_ref_no, "", refundby);

    //    BilldeskRefundResponsee(refund_id, transactionid, order_id, merchatId, transDate, txn_amount, refund_amount, currency, merc_refund_ref_no, refund_status, refundby);

    //    return refund_status;
    //}

    //public string Encode(string plainText)
    //{
    //    byte[] plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
    //    return Convert.ToBase64String(plainTextBytes)
    //        .Replace('+', '-')
    //        .Replace('/', '_')
    //        .Replace("=", "");
    //}
    //public string Decode(string base64UrlString)
    //{
    //    string paddedBase64UrlString = base64UrlString.PadRight(base64UrlString.Length + (4 - base64UrlString.Length % 4) % 4, '=');
    //    byte[] base64UrlBytes = Convert.FromBase64String(paddedBase64UrlString.Replace('-', '+').Replace('_', '/'));
    //    return System.Text.Encoding.UTF8.GetString(base64UrlBytes);
    //}

}