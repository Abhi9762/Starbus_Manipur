using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using Npgsql;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
/// <summary>
/// Summary description for ClassDoubleVerification_Refund
/// </summary>
public class ClassDoubleVerification_Refund
{
    DataTable dt1 = new DataTable();
    private sbBLL bll = new sbBLL();
    private NpgsqlCommand MyCommand;
    BilldeskSha billdesk = new BilldeskSha();
    //Billdesk
	string merchntidnew = "ARUNACUAT";
    string secretkeynew = "bnuEN7aXpEolZxlllNdZQ0cLAaFbXOLx";
    string clientidnew = "arunacuat";
    public string MdblurlNew = "https://uat1.billdesk.com/u2/payments/ve1_2/transactions/get";
    public string MrefundurlNew = "https://uat1.billdesk.com/u2/payments/ve1_2/refunds/create";
    string MrefundstatusurlNew = "https://uat1.billdesk.com/u2/payments/ve1_2/refunds/get";


    string MERCHANTID = "BDSKUATY";
    string SECURITYID = "bdskuaty";
  //  public string Murl = "https://uat.billdesk.com/pgidsk/PGIQueryController";
    public string Murl = "https://www.billdesk.com/pgidsk/PGIQueryController";


//private const string PHONEPE_STAGE_BASE_URL = "https://mercury-uat.phonepe.com/enterprise-sandbox";

private const string PHONEPE_STAGE_BASE_URL = "https://mercury-t2.phonepe.com";
    
    private string merchantKey = System.Configuration.ConfigurationManager.AppSettings["phonePe_checksumSaltKey"];
    private string merchantKeyIndex = System.Configuration.ConfigurationManager.AppSettings["phonePe_checksumSaltIndex"];
    private string merchantId = System.Configuration.ConfigurationManager.AppSettings["phonePe_merchantId"];
    private string terminalId = "terminal1";
    ErrorLog _err = new ErrorLog();

    public ClassDoubleVerification_Refund()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #region "Double Verification"
    public DataTable getOrphanTransactionCount(string pgid, string transdate)
    {
        DataTable dt = new DataTable();
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "pg.f_GetOrphanTrans");
        MyCommand.Parameters.AddWithValue("p_pmtgateway", Convert.ToInt32(pgid));
        MyCommand.Parameters.AddWithValue("p_transdate", transdate);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }  
    public DataTable getOrphanTxnDtl(string pgid, string transdate)
    {
        DataTable dt = new DataTable();
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "pg.f_GetOrphanTrans_dtls");
        MyCommand.Parameters.AddWithValue("p_pmtgateway", Convert.ToInt32(pgid));
        MyCommand.Parameters.AddWithValue("p_transdate", transdate);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    public DataTable dblstartday(string TxnDate, string pmtid, int pnooftrasaction, string usercode, string Doubleverifyby)
    {
        try
        {
            DataTable dt = new DataTable();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "pg.f_dbl_start_for_a_day");
            MyCommand.Parameters.AddWithValue("p_transdate", TxnDate);
            MyCommand.Parameters.AddWithValue("p_pmtgateway",Convert.ToInt32( pmtid));
            MyCommand.Parameters.AddWithValue("p_nooftransactions",pnooftrasaction );
            MyCommand.Parameters.AddWithValue("p_updatedby",usercode );
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["status"].ToString() == "Done")
                    {
                        return dt;
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
            return dt1;
        }
        catch (Exception ex)
        {
            return dt1;
        }
    }
    public string dblRequest(string order_id, Int64 dblogid, string Doubleverifyby)
    {
        try
        {
            string Db_verification = "";
            DataTable dt = new DataTable();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "pg.f_dbl_verify_insert");
            MyCommand.Parameters.AddWithValue("p_dbllogid",Convert.ToInt64(dblogid));
            MyCommand.Parameters.AddWithValue("p_transrefnumber", order_id);
            MyCommand.Parameters.AddWithValue("p_doubleverifyby", Doubleverifyby);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["dbl_ver_refno"].ToString() == "" || dt.Rows[0]["status"].ToString()== "EXCEPTION")
                        return Db_verification;
                    Db_verification = dt.Rows[0]["dbl_ver_refno"].ToString();
                }
            }
            return Db_verification;
        }
        catch (Exception ex)
        {
            return "";
        }
    }
    #region "Billdesk"
    public string BillDeskStatusAPI(string order_id, Int64 dblogid, string Db_verification, string Doubleverifyby)
    {
        string finalstatus= "tranerrors";
        try
        {
           
            DataTable dtt;
            string MString;
             string MRequestType = "0122";
            string MMerchantID = "APSTSBUS";
            string checksumkey = "M4RjgVXBHUFV";
            string MCustomerID = order_id;
            string McurrentDateTime;
            string McheckSum;
            McurrentDateTime = DateTime.Now.ToString("yyyymmdd24hhmmss");
            //Check_SSL_Certificate.Check_SSL_Certificate();
            MString = MRequestType + "|" + MMerchantID + "|" + MCustomerID + "|" + McurrentDateTime ;

            string Mhash;
            Mhash = billdesk.GetHMACSHA256(MString, checksumkey);
            MString = MString + "|" + Mhash.ToUpper().Trim();

            billdeskRequest(order_id, dblogid, Db_verification, Murl, MString, MRequestType, MMerchantID, checksumkey);


            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            WebRequest wrequest = WebRequest.Create(Murl);
            wrequest.Method = "Post";
            wrequest.Credentials = CredentialCache.DefaultCredentials;
            wrequest.Timeout = 10000; // 10 seconds


            System.Net.ServicePointManager.SecurityProtocol = (SecurityProtocolType)(0xC0 | 0x300 | 0xC00);
            string postData = string.Format("msg={0}", MString);
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            wrequest.ContentType = "application/x-www-form-urlencoded";
            wrequest.ContentLength = byteArray.Length;
            Stream dataStream = wrequest.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse response = wrequest.GetResponse();

            Stream dataStream1 = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream1);
            string responseFromServer = reader.ReadToEnd();
            reader.Close();
            dataStream.Close();
            response.Close();

            string[] BillDeskValues = responseFromServer.Split('|');

            string MRequestTypePG ;
                string MTxnReferenceNo ;
                string MBankReferenceNo ;
                string MTxnAmount ;
                string MBankID ;

                string MBankMerchantID ;
                string MTxnTypem ;
                string MCurrencyName ;
                string MItemCode ;
                string MSecurityType ;
                string MSecurityID ;
                string MSecurityPassword ;
                string MTxnDate ;
                string MAuthStatus ;
                string MSettlementType ;

          

            string MErrorStatus ;
                string MErrorDescription ;
                string MFiller1 ;
                string MRefundStatus ;
                string MTotalRefundAmount ;
                string MLastRefundDate ;
                string MLastRefundRefNo ;
                string MQueryStatus ;
                string MCheckSumRecvdFromPG ;
                string MstringForHash ;
                string MCheckSumResponse ;

                MRequestTypePG = BillDeskValues[0].ToString().Trim();
                MMerchantID = BillDeskValues[1].ToString().Trim();
                MCustomerID = BillDeskValues[2].ToString().Trim();
                MTxnReferenceNo = BillDeskValues[3].ToString().Trim();
                MBankReferenceNo = BillDeskValues[4].ToString().Trim();
                MTxnAmount = BillDeskValues[5].ToString().Trim();
                MBankID = BillDeskValues[6].ToString().Trim();

                MBankMerchantID = BillDeskValues[7].ToString().Trim();
                MTxnTypem = BillDeskValues[8].ToString().Trim();
                MCurrencyName = BillDeskValues[9].ToString().Trim();
                MItemCode = BillDeskValues[10].ToString().Trim();
                MSecurityType = BillDeskValues[11].ToString().Trim();
                MSecurityID = BillDeskValues[12].ToString().Trim();
                MSecurityPassword = BillDeskValues[13].ToString().Trim();
                MTxnDate = BillDeskValues[14].ToString().Trim();
                MAuthStatus = BillDeskValues[15].ToString().Trim();
                MSettlementType = BillDeskValues[16].ToString().Trim();

                MErrorStatus = BillDeskValues[24].ToString().Trim();
                MErrorDescription = BillDeskValues[25].ToString().Trim();
                MFiller1 = BillDeskValues[26].ToString().Trim();
                MRefundStatus = BillDeskValues[27].ToString().Trim();
                MTotalRefundAmount = BillDeskValues[28].ToString().Trim();
                MLastRefundDate = BillDeskValues[29].ToString().Trim();
                MLastRefundRefNo = BillDeskValues[30].ToString().Trim();
                MQueryStatus = BillDeskValues[31].ToString().Trim();
                MCheckSumRecvdFromPG = BillDeskValues[32].ToString().Trim();

            billDeskResponse(MCustomerID, MMerchantID,MRequestTypePG, MTxnReferenceNo, MBankReferenceNo, MTxnAmount, MBankID, MBankMerchantID, MTxnTypem,
               MCurrencyName, MItemCode, MSecurityType, MSecurityID, MSecurityPassword, MTxnDate, MAuthStatus, MSettlementType,
               MErrorStatus, MErrorDescription, MFiller1, MRefundStatus, MTotalRefundAmount, MLastRefundDate, MLastRefundRefNo,
               MQueryStatus, MCheckSumRecvdFromPG,dblogid,Db_verification);

            return finalstatus;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    private void billDeskResponse(string MCustomerID,string MMerchantID,string mRequestTypePG, string mTxnReferenceNo, string mBankReferenceNo, string mTxnAmount,
        string mBankID, string mBankMerchantID, string mTxnTypem, string mCurrencyName, string mItemCode, string mSecurityType,
        string mSecurityID, string mSecurityPassword, string mTxnDate, string mAuthStatus, string mSettlementType, string mErrorStatus,
        string mErrorDescription, string mFiller1, string mRefundStatus, string mTotalRefundAmount, string mLastRefundDate,
        string mLastRefundRefNo, string mQueryStatus, string mCheckSumRecvdFromPG,Int64 dblogid,string Db_verification)
    {
        try
        {
            DataTable dt = new DataTable();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "pg.f_dbl_billdesk_response");
            MyCommand.Parameters.AddWithValue("p_order_id", MCustomerID);
            MyCommand.Parameters.AddWithValue("p_request_type_pg", mRequestTypePG);
            MyCommand.Parameters.AddWithValue("p_merchant_id", MMerchantID);
            MyCommand.Parameters.AddWithValue("p_txn_refrence_no",mTxnReferenceNo);
            MyCommand.Parameters.AddWithValue("p_bank_refrence_no", mBankReferenceNo);
            MyCommand.Parameters.AddWithValue("p_txn_amount", mTxnAmount);
            MyCommand.Parameters.AddWithValue("p_bank_id", mBankID);
            MyCommand.Parameters.AddWithValue("p_bank_merchant_id", mBankMerchantID);
            MyCommand.Parameters.AddWithValue("p_txn_type", mTxnTypem);
            MyCommand.Parameters.AddWithValue("p_currency_name", mCurrencyName);
            MyCommand.Parameters.AddWithValue("p_item_code", mItemCode);
            MyCommand.Parameters.AddWithValue("p_security_type", mSecurityType);
            MyCommand.Parameters.AddWithValue("p_security_id", mSecurityID);
            MyCommand.Parameters.AddWithValue("p_security_password", mSecurityPassword);
            MyCommand.Parameters.AddWithValue("p_txn_date", mTxnDate);
            MyCommand.Parameters.AddWithValue("p_auth_status", mAuthStatus);
            MyCommand.Parameters.AddWithValue("p_settlement_type", mSettlementType);
            MyCommand.Parameters.AddWithValue("p_error_status", mErrorStatus);
            MyCommand.Parameters.AddWithValue("p_error_description", mErrorDescription);
            MyCommand.Parameters.AddWithValue("p_filler", mFiller1);
            MyCommand.Parameters.AddWithValue("p_refund_status", mRefundStatus);
            MyCommand.Parameters.AddWithValue("p_total_refund_amount", mRefundStatus);
            MyCommand.Parameters.AddWithValue("p_last_refund_date", mLastRefundDate);
            MyCommand.Parameters.AddWithValue("p_last_refund_refno", mLastRefundRefNo);
            MyCommand.Parameters.AddWithValue("p_query_status", mQueryStatus);
            MyCommand.Parameters.AddWithValue("p_checksum", mCheckSumRecvdFromPG);
            MyCommand.Parameters.AddWithValue("p_dblogid",dblogid.ToString());
            MyCommand.Parameters.AddWithValue("p_dblverification_refno", Db_verification);
            
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {

            }
        }
        catch (Exception ex)
        { }
    }

    private void billdeskRequest(string order_id, Int64 dblogid, string db_verification,string url,string MString,string MRequestType,
      string MMerchantID,string checksumkey)
    {
        try
        {
            DataTable dt = new DataTable();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "pg.f_dbl_billdesk_request");
            MyCommand.Parameters.AddWithValue("p_order_id", order_id);
            MyCommand.Parameters.AddWithValue("p_dblogid", dblogid);
            MyCommand.Parameters.AddWithValue("p_dblverificationrefno", db_verification);
            MyCommand.Parameters.AddWithValue("p_url", url);
            MyCommand.Parameters.AddWithValue("p_mstring", MString);
            MyCommand.Parameters.AddWithValue("p_requesttype", MRequestType);
            MyCommand.Parameters.AddWithValue("p_merchantid", MMerchantID);
            MyCommand.Parameters.AddWithValue("p_checksumkey", checksumkey);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                
            }
        }
        catch (Exception ex)
        { }
    }
    #endregion

	#region "Billdesk New"
    public async Task<string> BillDeskStatusAPINew(string order_id, string dblogid, string Db_verification, string Doubleverifyby)
    {

        ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
        string url = MdblurlNew;
        string MRequestType = "0122";
        string MString;
        String merchatId = merchntidnew;
        string MCustomerID = order_id;
        //string pay_orderid = Session["pay_orderid"].ToString();
        string McurrentDateTime = DateTime.Now.ToString("yyyyMMddhhmmss");
        DateTime Ddate = DateTime.Now;
        String secretKey = secretkeynew;
        string jsone_header = "{\r\n\"alg\":\"HS256\",\r\n\"clientid\":\""+ clientidnew+"\"\r\n}";
        string json_payload = "{\r\n\"mercid\":\""+merchntidnew+"\",\r\n\"orderid\":\"" + order_id +
        "\"\r\n}";
        var encodedPayload = Encode(json_payload);
        var encodedHeader = Encode(jsone_header);
        var sha256 = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey));
        string combinedData = encodedHeader + "." + encodedPayload;
        var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(combinedData));
        string hash = Convert.ToBase64String(hashBytes).Replace('+', '-').Replace('/', '_').Replace("=", "");
        var jwt = encodedHeader + "." + encodedPayload + "." + hash;

        MString = MRequestType + "|" + merchatId + "|" + MCustomerID + "|" + McurrentDateTime + "|" + secretKey;
        billdeskRequestNew(order_id, Convert.ToInt64(dblogid), Db_verification, url, MString, MRequestType, merchatId, secretKey);



        HttpClientHandler handler = new HttpClientHandler();
        handler.AutomaticDecompression = DecompressionMethods.None;
        HttpClient client = new HttpClient(handler);
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
        request.Headers.Add("accept", "application/jose");
        request.Headers.Add("BD-Traceid", McurrentDateTime);
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
        var trim_tokes = Decode(Resp_payload);
        var parsed = JObject.Parse(trim_tokes);
        string transaction_error_desc = (string)parsed.SelectToken("transaction_error_desc") ?? "N/A";
        string transactionid = (string)parsed.SelectToken("transactionid") ?? "N/A";
        string additional_info2 = (string)parsed.SelectToken("additional_info.additional_info2") ?? "N/A";
        string additional_info1 = (string)parsed.SelectToken("additional_info.additional_info1") ?? "N/A";
        string val1 = (string)parsed.SelectToken("transactionid") ?? "N/A";
        string transDate = (string)parsed.SelectToken("transaction_date") ?? "N/A";
        string bankID = (string)parsed.SelectToken("bankid") ?? "N/A";
        string bankRefNo = (string)parsed.SelectToken("bank_ref_no") ?? "N/A";
        string amount = (string)parsed.SelectToken("charge_amount") ?? "N/A";

        string paymentType = (string)parsed.SelectToken("payment_method_type") ?? "N/A";
        string processType = (string)parsed.SelectToken("txn_process_type") ?? "N/A";
        string errorType = (string)parsed.SelectToken("transaction_error_type") ?? "N/A";
        string additionalInfo1 = (string)parsed.SelectToken("additional_info.additional_info1") ?? "N/A";
        string currency = (string)parsed.SelectToken("currency") ?? "N/A";
        string itemcode = (string)parsed.SelectToken("itemcode") ?? "N/A";
        string auth_Status = (string)parsed.SelectToken("auth_status") ?? "N/A";
        string status = (string)parsed.SelectToken("status") ?? "N/A";
        string message = (string)parsed.SelectToken("message") ?? "N/A";
        string errorcode= (string)parsed.SelectToken("error_code") ?? "N/A"; 
        if (auth_Status == "N/A")
        {
            auth_Status = status;
        }


        billDeskResponse_new(MCustomerID, merchatId, "N/A", transactionid, bankRefNo, amount, bankID, "N/A", "N/A",
             currency, itemcode, "N/A", "N/A", "N/A", transDate, auth_Status, "",
             errorcode, transaction_error_desc, "N/A", "N/A", "N/A", "N/A", "N/A",
             message, "N/A", Convert.ToInt64(dblogid), Db_verification);



        return auth_Status;

    }

 private void billDeskResponse_new(string MCustomerID, string MMerchantID, string mRequestTypePG, string mTxnReferenceNo, string mBankReferenceNo, string mTxnAmount, string mBankID, string mBankMerchantID, string mTxnTypem, string mCurrencyName, string mItemCode, string mSecurityType, string mSecurityID, string mSecurityPassword, string mTxnDate, string mAuthStatus, string mSettlementType, string mErrorStatus, string mErrorDescription, string mFiller1, string mRefundStatus, string mTotalRefundAmount, string mLastRefundDate, string mLastRefundRefNo, string mQueryStatus, string mCheckSumRecvdFromPG, Int64 dblogid, string Db_verification)
    {
        try
        {
            DataTable dt = new DataTable();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "pg.f_dbl_billdesk_response_new");
            MyCommand.Parameters.AddWithValue("p_order_id", MCustomerID);
            MyCommand.Parameters.AddWithValue("p_request_type_pg", mRequestTypePG);
            MyCommand.Parameters.AddWithValue("p_merchant_id", MMerchantID);
            MyCommand.Parameters.AddWithValue("p_txn_refrence_no", mTxnReferenceNo);
            MyCommand.Parameters.AddWithValue("p_bank_refrence_no", mBankReferenceNo);
            MyCommand.Parameters.AddWithValue("p_txn_amount", mTxnAmount);
            MyCommand.Parameters.AddWithValue("p_bank_id", mBankID);
            MyCommand.Parameters.AddWithValue("p_bank_merchant_id", mBankMerchantID);
            MyCommand.Parameters.AddWithValue("p_txn_type", mTxnTypem);
            MyCommand.Parameters.AddWithValue("p_currency_name", mCurrencyName);
            MyCommand.Parameters.AddWithValue("p_item_code", mItemCode);
            MyCommand.Parameters.AddWithValue("p_security_type", mSecurityType);
            MyCommand.Parameters.AddWithValue("p_security_id", mSecurityID);
            MyCommand.Parameters.AddWithValue("p_security_password", mSecurityPassword);
            MyCommand.Parameters.AddWithValue("p_txn_date", mTxnDate);
            MyCommand.Parameters.AddWithValue("p_auth_status", mAuthStatus);
            MyCommand.Parameters.AddWithValue("p_settlement_type", mSettlementType);
            MyCommand.Parameters.AddWithValue("p_error_status", mErrorStatus);
            MyCommand.Parameters.AddWithValue("p_error_description", mErrorDescription);
            MyCommand.Parameters.AddWithValue("p_filler", mFiller1);
            MyCommand.Parameters.AddWithValue("p_refund_status", mRefundStatus);
            MyCommand.Parameters.AddWithValue("p_total_refund_amount", mRefundStatus);
            MyCommand.Parameters.AddWithValue("p_last_refund_date", mLastRefundDate);
            MyCommand.Parameters.AddWithValue("p_last_refund_refno", mLastRefundRefNo);
            MyCommand.Parameters.AddWithValue("p_query_status", mQueryStatus);
            MyCommand.Parameters.AddWithValue("p_checksum", mCheckSumRecvdFromPG);
            MyCommand.Parameters.AddWithValue("p_dblogid", dblogid.ToString());
            MyCommand.Parameters.AddWithValue("p_dblverification_refno", Db_verification);

            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {

            }
        }
        catch (Exception ex)
        { }
    }
    private void billdeskRequestNew(string order_id, Int64 dblogid, string db_verification, string url, string MString, string MRequestType, string MMerchantID, string checksumkey)
    {
        try
        {
            DataTable dt = new DataTable();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "pg.f_dbl_billdesk_request_new");
            MyCommand.Parameters.AddWithValue("p_order_id", order_id);
            MyCommand.Parameters.AddWithValue("p_dblogid", dblogid);
            MyCommand.Parameters.AddWithValue("p_dblverificationrefno", db_verification);
            MyCommand.Parameters.AddWithValue("p_url", url);
            MyCommand.Parameters.AddWithValue("p_mstring", MString);
            MyCommand.Parameters.AddWithValue("p_requesttype", MRequestType);
            MyCommand.Parameters.AddWithValue("p_merchantid", MMerchantID);
            MyCommand.Parameters.AddWithValue("p_checksumkey", checksumkey);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {

            }
        }
        catch (Exception ex)
        { }
    }


    public string Encode(string plainText)
    {
        byte[] plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return Convert.ToBase64String(plainTextBytes)
            .Replace('+', '-')
            .Replace('/', '_')
            .Replace("=", "");
    }
    public string Decode(string base64UrlString)
    {
        string paddedBase64UrlString = base64UrlString.PadRight(base64UrlString.Length + (4 - base64UrlString.Length % 4) % 4, '=');
        byte[] base64UrlBytes = Convert.FromBase64String(paddedBase64UrlString.Replace('-', '+').Replace('_', '/'));
        return System.Text.Encoding.UTF8.GetString(base64UrlBytes);
    }
    #endregion

    #region "PhonePe"
    public class PhonePeStatusResponseBody
    {
        public bool success;
        public string code;
        public string message;
        public PaymentDetails data;
    }
    public class DataStatusResponse
    {
        public string merchantId;
        public string transactionId;
        public int amount;
        public string merchantOrderId;
        public string paymentState;
        public string payResponseCode;
    }
    public class PaymentDetails
    {
        public string transactionId { get; set; }
        public string merchantId { get; set; }
        public string providerReferenceId { get; set; }
        public int amount { get; set; }
        public string paymentState { get; set; }
        public string payResponseCode { get; set; }
        public List<PaymentMode> paymentModes { get; set; }
        public TransactionContext transactionContext { get; set; }
    }
    public class PaymentMode
    {
        public string mode { get; set; }
        public int amount { get; set; }
        public string utr { get; set; }
    }
    public class TransactionContext
    {
        public string storeId { get; set; }
        public string terminalId { get; set; }
    }
    public string PhonePeStatusAPI(string transactionId, string dblogid, string Db_verification, string Doubleverifyby, string txn_date)
    {
        string resp = "Error";
        string headerString = string.Format("/v3/transaction/{0}/{1}/status{2}", merchantId, transactionId, merchantKey);
        // Console.WriteLine("headerString: " & headerString)
        string checksum = GenerateSha256ChecksumFromBase64Json("", headerString);
        checksum = checksum + "###" + merchantKeyIndex;
        // Console.WriteLine(checksum)
        bool b = PhonePe_status_Request(dblogid, Db_verification, merchantId, transactionId, merchantKey, checksum);
        string txnURL = PHONEPE_STAGE_BASE_URL;
        string urlSuffix = string.Format("/v3/transaction/{0}/{1}/status", merchantId, transactionId);
        txnURL = txnURL + urlSuffix;
        try
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(txnURL);
            webRequest.Method = "GET";
            webRequest.Headers.Add("x-verify", checksum);
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string responseData = string.Empty;
            using (StreamReader responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream()))
            {
                responseData = responseReader.ReadToEnd();
                _err.Error_Log("PhonePeStatusAPI-01", responseData);
                if (responseData.Length > 0)
                {
                    PhonePeStatusResponseBody responseBody = JsonConvert.DeserializeObject<PhonePeStatusResponseBody>(responseData);
                    string success_str = "";
                    bool success = responseBody.success;
                    if (success == true)
                        success_str = "true";
                    else
                        success_str = "false";
                    string code = responseBody.code;
                    string message = responseBody.message;
                    PaymentDetails data = responseBody.data;
                    string data_merchantId = data.merchantId;
                    string data_transactionId = data.transactionId;
                    string providerReferenceId = "";
                    if (data.providerReferenceId != null)
                    {
                        providerReferenceId = data.providerReferenceId;
                    }
                    int data_amount = data.amount;
                    string paymentState = data.paymentState;
                    string payResponseCode = data.payResponseCode;
                    string PM_mode = "";
                    string PM_amount = "";
                    string PM_utr = "";
                    if (data.paymentModes != null)
                    {
                        foreach (PaymentMode paymentMode in data.paymentModes)
                        {
                            PM_mode = paymentMode.mode;
                            PM_amount = paymentMode.amount.ToString();
                            PM_utr = paymentMode.utr;
                        }
                    }
                    string storeId = "";
                    string terminalId = "";
                    if (data.transactionContext != null)
                    {
                        TransactionContext datatransactionContext = data.transactionContext;
                        storeId = datatransactionContext.storeId;
                        terminalId = datatransactionContext.terminalId;
                    }
                    resp = PhonePe_status_Response(dblogid, transactionId, success_str, code, message, data_merchantId, data_transactionId, data_amount.ToString(), providerReferenceId, paymentState, payResponseCode, Db_verification, Doubleverifyby, txn_date, PM_mode, PM_amount, PM_utr, storeId, terminalId);
                }
            }
            return resp;
        }
        catch (Exception e)
        {
            _err.Error_Log("PhonePeStatusAPI-02", e.Message);
            return "Error" + e.Message;
        }
    }
    private string GenerateSha256ChecksumFromBase64Json(string base64JsonString, string jsonSuffixString)
    {
        string checksum = null;
        System.Security.Cryptography.SHA256 sha256 = System.Security.Cryptography.SHA256.Create();
        string checksumString = base64JsonString + jsonSuffixString;
        byte[] checksumBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(checksumString));
        foreach (byte b in checksumBytes)
            // checksum &= $"{b:x2}"
            // checksum += $"{b}"
            // checksum &= b.ToString("X2")
            checksum += string.Format("{0:x2}", b);
        return checksum;
    }
    public bool PhonePe_status_Request(string dbl_logid, string dblverificationrefno, string merchant_id, string order_id, string merchant_key, string checksum)
    {
        try
        {
            DataTable dt = new DataTable();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "pg.f_dbl_phonepe_request");
            MyCommand.Parameters.AddWithValue("p_order_id", order_id);
            MyCommand.Parameters.AddWithValue("p_dbl_logid", Convert.ToInt64(dbl_logid));
            MyCommand.Parameters.AddWithValue("p_dblverificationrefno", dblverificationrefno);
            MyCommand.Parameters.AddWithValue("p_merchant_id", merchant_id);
            MyCommand.Parameters.AddWithValue("p_merchant_key", merchant_key);
            MyCommand.Parameters.AddWithValue("p_checksum", checksum);
            dt = bll.SelectAll(MyCommand);
            _err.Error_Log("PhonePe_status_Request-01", dt.TableName);
            if (dt.TableName == "Success")
            {
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            _err.Error_Log("PhonePe_status_Request-02", ex.Message);
            return false;
        }
    }
    public string PhonePe_status_Response(string dblogid, string order_id, string status_succes, string status_code, string status_message, string data_merchantid, string data_txnid, string data_amount, string data_providerrefid, string data_paymentstate, string data_payresponsecode, string doubleverificationno, string doubleverifyby, string txn_date, string data_pmtmodesdata_mode, string data_pmtmodesdata_amount, string data_pmtmodesdata_utr, string data_txncontext_storeid, string data_txncontext_terminalid)
    {
        try
        {
            DataTable dt = new DataTable();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "pg.f_dbl_phonepe_response");
            MyCommand.Parameters.AddWithValue("p_dblogid", Convert.ToInt64(dblogid));
            MyCommand.Parameters.AddWithValue("p_order_id", order_id);
            MyCommand.Parameters.AddWithValue("p_status_success", status_succes);
            MyCommand.Parameters.AddWithValue("p_status_code", status_code);
            MyCommand.Parameters.AddWithValue("p_status_message", status_message);
            MyCommand.Parameters.AddWithValue("p_data_merchantid", data_merchantid);
            MyCommand.Parameters.AddWithValue("p_data_txnid", data_txnid);
            MyCommand.Parameters.AddWithValue("p_data_amount", data_amount);
            MyCommand.Parameters.AddWithValue("p_data_providerrefid", data_providerrefid);
            MyCommand.Parameters.AddWithValue("p_data_paymentstate", data_paymentstate);
            MyCommand.Parameters.AddWithValue("p_data_payresponsecode", data_payresponsecode);
            MyCommand.Parameters.AddWithValue("p_doubleverificationno", doubleverificationno);
            MyCommand.Parameters.AddWithValue("p_doubleverifyby", doubleverifyby);
            MyCommand.Parameters.AddWithValue("p_txn_date", txn_date);
            MyCommand.Parameters.AddWithValue("p_data_pmtmodesdata_mode", data_pmtmodesdata_mode);
            MyCommand.Parameters.AddWithValue("p_data_pmtmodesdata_amount", data_pmtmodesdata_amount);
            MyCommand.Parameters.AddWithValue("p_data_pmtmodesdata_utr", data_pmtmodesdata_utr);
            MyCommand.Parameters.AddWithValue("p_data_txncontext_storeid", data_txncontext_storeid);
            MyCommand.Parameters.AddWithValue("p_data_txncontext_terminalid", data_txncontext_terminalid);
            dt = bll.SelectAll(MyCommand);
            _err.Error_Log("PhonePe_status_Response-01", dt.TableName);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                    return dt.Rows[0]["status"].ToString();
                else
                    return "Error";
            }
            else
                return "Error";
        }
        catch (Exception ex)
        {
            _err.Error_Log("PhonePe_status_Response-02", ex.Message);
            return "Error";
        }
    }
    #endregion
    #endregion

    #region "Refund Initition"
    public DataTable getRefundTxnCount(string pgid, string transdate)
    {
        DataTable dt = new DataTable();
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "pg.f_GetRefundTrans");
        MyCommand.Parameters.AddWithValue("p_pmtgateway", Convert.ToInt32(pgid));
        MyCommand.Parameters.AddWithValue("p_transdate", transdate);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    public DataTable getRefundTxnDtls(string pgid, string transdate)
    {
        DataTable dt = new DataTable();
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "pg.f_getrefundtrans_dtls");
        MyCommand.Parameters.AddWithValue("p_pmtgateway", Convert.ToInt32(pgid));
        MyCommand.Parameters.AddWithValue("p_transdate", transdate);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }

#region"Billdesk New"
    public async Task<string> BilldeskRefundAPI(string transaction_id, string cancelation_ref_no, string order_id, string txn_amount, string refundby, string txndate, string refundamt)
    {


        Random rnd = new Random();
        int txnID = rnd.Next(100, 100000);

        HttpRequest requestNew = HttpContext.Current.Request;

        // Get the browser capabilities
        HttpBrowserCapabilities browser = requestNew.Browser;

        string browserType = browser.Type;
        string browserBrowser = browser.Browser;
        string browserVerison = browser.Version;
        int MajorVersion = browser.MajorVersion;
        double MinorVersion = browser.MinorVersion;
        bool isMobile = browser.IsMobileDevice;


        string osName = Environment.OSVersion.Platform.ToString();

        // Get OS version
        string osVersion = Environment.OSVersion.Version.ToString();

        // Get device type (assuming Windows/Linux for simplicity)
        string deviceType = Environment.OSVersion.Platform == PlatformID.Win32NT ? "PC" : "Unknown";
        // string PathAndQuery = Request.Url.PathAndQuery;
        //string url_host = Request.Url.AbsoluteUri.Replace(PathAndQuery, "");
        // string resUrl = url_host + "/BILLDESK_PG/bill_desk_Response.aspx?OID=REFTS14RTEST" + txnID + "";

        ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
        SHA256Managed hashString = new SHA256Managed();
        DateTime Ddate = DateTime.Now;
        String merchatId = merchntidnew;
        String clientId = clientidnew;
        //String returnUrl = resUrl;
        String secretKey = secretkeynew;
        String billdesk_url = MrefundurlNew;
        //string refund_ref_no = "REFTS14R" + txnID;

        refundPayload refundPayload = new refundPayload();
        refundPayload.mercid = merchatId;
        refundPayload.transactionid = transaction_id;
        refundPayload.orderid = order_id;
        refundPayload.txn_amount = txn_amount;
        refundPayload.refund_amount = refundamt;
        refundPayload.currency = "356";
        refundPayload.transaction_date = txndate;
        refundPayload.merc_refund_ref_no = cancelation_ref_no;

        string txnrefundrefno = BilldeskRefundRequestt(refundPayload.transactionid, refundPayload.orderid, merchatId, refundPayload.transaction_date, refundPayload.txn_amount, refundPayload.refund_amount, refundPayload.currency, refundPayload.merc_refund_ref_no, "", refundby);




        var json_payload = Newtonsoft.Json.JsonConvert.SerializeObject(refundPayload);
        string jsone_header = "{\r\n\"alg\":\"HS256\",\r\n\"clientid\":\""+clientidnew+"\"\r\n}";

        string encodedPayload = Encode(json_payload);

        string encodedHeader = Encode(jsone_header);
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
        request.Headers.Add("BD-Traceid", cancelation_ref_no);
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

        string trim_tokes = Decode(Resp_payload);


        var parsed = JObject.Parse(trim_tokes);
        string refund_status = (string)parsed.SelectToken("refund_status") ?? "N/A";
        string refund_id = (string)parsed.SelectToken("refundid") ?? "N/A";
        string transactionid = (string)parsed.SelectToken("transactionid") ?? "N/A";
        string order_idd = (string)parsed.SelectToken("orderid") ?? "N/A";
        string transDate = (string)parsed.SelectToken("transaction_date") ?? "";
        string txn_amountt = (string)parsed.SelectToken("txn_amount") ?? "0";
        string refund_amount = (string)parsed.SelectToken("refund_amount") ?? "0";
        string currency = (string)parsed.SelectToken("currency") ?? "N/A";
        string refund_date = (string)parsed.SelectToken("refund_date") ?? "";
        string merc_refund_ref_no = (string)parsed.SelectToken("merc_refund_ref_no") ?? "N/A";

        string errorstatus = (string)parsed.SelectToken("status") ?? "N/A";
        string error_type = (string)parsed.SelectToken("error_type") ?? "N/A";
        string error_code = (string)parsed.SelectToken("error_code") ?? "N/A";
        string errormessage = (string)parsed.SelectToken("message") ?? "N/A";

        if (refund_status == "N/A")
        {
            refund_status = errorstatus;
        }


        //refundStatus = await BL.RefundResponseAPI(cancelation_ref_no);

        BilldeskRefundResponsee(refund_id, transactionid, order_id, merchatId, transDate, txn_amount, refund_amount, currency, merc_refund_ref_no, refund_status, refundby,
            txnrefundrefno,errorstatus,error_type,error_code,errormessage);

        return refund_status;
    }
    public string BilldeskRefundRequestt(string transaction_id, string order_id, string merchant_id, string transaction_date, string txn_amount, string refund_amount, string currency, string merc_refund_ref_no,
      string refund_status, string refunded_by)
    {
        try
        {
            string refund_ref_no = "";
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "pg.f_refund_billdesk_request");

            MyCommand.Parameters.AddWithValue("p_transaction_id", transaction_id);
            MyCommand.Parameters.AddWithValue("p_order_id", order_id);
            MyCommand.Parameters.AddWithValue("p_merchant_id", merchant_id);
            MyCommand.Parameters.AddWithValue("p_transaction_date", transaction_date);
            MyCommand.Parameters.AddWithValue("p_txn_amount", Convert.ToDecimal(txn_amount));
            MyCommand.Parameters.AddWithValue("p_refund_amount", Convert.ToDecimal(refund_amount));
            MyCommand.Parameters.AddWithValue("p_currency", currency);
            MyCommand.Parameters.AddWithValue("p_merc_refund_ref_no", merc_refund_ref_no);
            MyCommand.Parameters.AddWithValue("p_refund_status", refund_status);
            MyCommand.Parameters.AddWithValue("p_refunded_by", refunded_by);
            DataTable dtt = new DataTable();
            dtt = bll.SelectAll(MyCommand);
            if (dtt.TableName == "Success")
            {
                if (dtt.Rows[0]["status"].ToString() == "SUCCESS")
                {
                    refund_ref_no = dtt.Rows[0]["refrefno"].ToString();
                    return refund_ref_no;
                }
                else
                {
                    return refund_ref_no;
                }
            }
            return refund_ref_no;
        }
        catch (Exception ex)
        {
            return "";
        }
    }

    public string BilldeskRefundResponsee(string refund_id, string transaction_id, string order_id, string merchant_id, string transaction_date, string txn_amount, string refund_amount, string currency, string merc_refund_ref_no,
      string refund_status, string refunded_by, string txnrefundrefno, string errorstatus, string error_type, string error_code, string errormessage)
    {
        try
        {
            string refund_ref_no = "";
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "pg.f_refund_billdesk_response");

            MyCommand.Parameters.AddWithValue("p_refund_id", refund_id);
            MyCommand.Parameters.AddWithValue("p_transaction_id", transaction_id);
            MyCommand.Parameters.AddWithValue("p_order_id", order_id);
            MyCommand.Parameters.AddWithValue("p_merchant_id", merchant_id);
            MyCommand.Parameters.AddWithValue("p_transaction_date", transaction_date);
            MyCommand.Parameters.AddWithValue("p_txn_amount", Convert.ToDecimal(txn_amount));
            MyCommand.Parameters.AddWithValue("p_refund_amount", Convert.ToDecimal(refund_amount));
            MyCommand.Parameters.AddWithValue("p_currency", currency);
            MyCommand.Parameters.AddWithValue("p_merc_refund_ref_no", merc_refund_ref_no);
            MyCommand.Parameters.AddWithValue("p_refund_status", refund_status);
            MyCommand.Parameters.AddWithValue("p_refunded_by", refunded_by);
            MyCommand.Parameters.AddWithValue("p_txnrefundrefno", txnrefundrefno);
            MyCommand.Parameters.AddWithValue("p_errorcode", error_code);
            MyCommand.Parameters.AddWithValue("p_errortype", error_type);
            MyCommand.Parameters.AddWithValue("p_errormessage", errormessage);
            MyCommand.Parameters.AddWithValue("p_errorstatus", errorstatus);
            DataTable dtt = new DataTable();
            dtt = bll.SelectAll(MyCommand);
            if (dtt.TableName == "Success")
            {
                if (dtt.Rows[0]["status"].ToString() == "SUCCESS")
                {
                    refund_ref_no = dtt.Rows[0]["refstatus"].ToString();
                    return refund_ref_no;
                }
                else
                {
                    return refund_ref_no;
                }
            }
            return refund_ref_no;
        }
        catch (Exception ex)
        {
            return "";
        }
    }

    #endregion

    #region "PhonePay"
    public class PhonePeRefundRequestParam
    {
        public string merchantId;
        public string transactionId;
        public string originalTransactionId;
        public int amount;
        public string merchantOrderId;
        public string subMerchant;
        public string message;
    }
    public class PhonePeCollectApiRequestBody
    {
        public string request;
    }
    public class PhonePeRefundResponseBody
    {
        public bool success;
        public string code;
        public string message;
        public DataRefundResponse data;
    }
    public class DataRefundResponse
    {
        public string transactionId;
        public string merchantId;
        public int amount;
        public string status;
        public string mobileNumber;
        public string providerReferenceId;
        public string payResponseCode;
    }
    public string PhonePeRefundAPI(string transactionId, string providerRefNo, string amount, string cancelref, string refundby)
    {
        string responseData = string.Empty;
        string refundRefNo = "";
        string resp = "";
        bool success = false;
        string success_str = "";
        success_str = "false";
        string code = "";
        string message = "";

        string data_merchantId = "";
        string data_transactionId = "";
        int data_amount = 0;
        string data_status = "";
        string data_mobileNumber = "";
        string data_providerReferenceId = "";
        string data_payResponseCode = "";

        resp = "Error";
        refundRefNo = PhonePeRefundRequestt("S", merchantId, "", transactionId, amount, transactionId, "", "", "", merchantKey, merchantKeyIndex, cancelref, refundby);
        if (refundRefNo == "")
        {
            return "Error";
        }
        // _err.Error_Log("PhonePeRefundAPI-S01", refundRefNo);
        PhonePeRefundRequestParam phonePeRefundRequest = new PhonePeRefundRequestParam();
        phonePeRefundRequest.merchantId = merchantId;
        phonePeRefundRequest.transactionId = cancelref;
        phonePeRefundRequest.originalTransactionId = transactionId;
        phonePeRefundRequest.amount = Convert.ToInt32(amount.ToString());
        phonePeRefundRequest.merchantOrderId = transactionId;
        phonePeRefundRequest.subMerchant = "";
        phonePeRefundRequest.message = "";
        string jsonStr = JsonConvert.SerializeObject(phonePeRefundRequest);
        string base64Json = ConvertStringToBase64(jsonStr);
        string jsonSuffixString = "/v3/credit/backToSource" + merchantKey;
        string checksum = GenerateSha256ChecksumFromBase64Json(base64Json, jsonSuffixString);
        checksum = checksum + "###" + merchantKeyIndex;
        string txnURL = PHONEPE_STAGE_BASE_URL + "/v3/credit/backToSource";
        string requestbodyyy = "";
        string respppp = "";
        // _err.Error_Log("PhonePeRefundAPI-merchantId", merchantId);
        // _err.Error_Log("PhonePeRefundAPI-transactionId", refundRefNo);
        // _err.Error_Log("PhonePeRefundAPI-transactionId", Convert.ToInt32(amount.ToString()).ToString());
        // _err.Error_Log("PhonePeRefundAPI-originalTransactionId", transactionId);
        // _err.Error_Log("PhonePeRefundAPI-merchantOrderId", transactionId);
        try
        {
            refundRefNo = PhonePeRefundRequestt("U", "", refundRefNo, "", "", transactionId, "", "", checksum, "", "", cancelref, "");
            //_err.Error_Log("PhonePeRefundAPI-checksum", checksum);
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(txnURL);
            webRequest.Method = "POST";
            webRequest.ContentType = "application/json";
            webRequest.Headers.Add("x-verify", checksum);
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            //_err.Error_Log("PhonePeRefundAPI-base64Json", base64Json);
            PhonePeCollectApiRequestBody phonePeCollectApiRequestBody = new PhonePeCollectApiRequestBody();
            phonePeCollectApiRequestBody.request = base64Json;
            string jsonBody = JsonConvert.SerializeObject(phonePeCollectApiRequestBody);
            using (StreamWriter requestWriter = new StreamWriter(webRequest.GetRequestStream()))
            {
                requestWriter.Write(jsonBody);
            }
            requestbodyyy = jsonBody;
            // _err.Error_Log("PhonePeRefundAPI-requestbodyyy", requestbodyyy);
            responseData = string.Empty;
            using (StreamReader responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream()))
            {
                responseData = responseReader.ReadToEnd();
                _err.Error_Log("PhonePeRefundAPI-responseData", responseData);
                respppp = responseData;
                if (responseData.Length > 0)
                {
                    PhonePeRefundResponseBody responseBody = JsonConvert.DeserializeObject<PhonePeRefundResponseBody>(responseData);
                    success_str = "";
                    success = responseBody.success;
                    if (success == true)
                        success_str = "true";
                    else
                        success_str = "false";
                    code = responseBody.code;
                    message = responseBody.message;
                    DataRefundResponse data = responseBody.data;
                    data_merchantId = data.merchantId;
                    data_transactionId = data.transactionId == null ? "" : data.transactionId;
                    data_amount = data.amount;
                    data_status = data.status;
                    data_mobileNumber = data.mobileNumber == null ? "" : data.mobileNumber;
                    data_providerReferenceId = data.providerReferenceId == null ? "" : data.providerReferenceId;
                    data_payResponseCode = data.payResponseCode == null ? "" : data.payResponseCode;
                    resp = PhonePeRefundResponsee(success_str, code, message, data_transactionId, data_merchantId, data_amount.ToString(), data_status, data_mobileNumber, data_providerReferenceId, data_payResponseCode, refundRefNo, cancelref, refundby, transactionId);
                }
            }
            return resp;
        }
        catch (Exception ex)
        {
            _err.Error_Log("PhonePeRefundAPI-R01--", ex.Message);
            return "Error";
        }
    }
    public string ConvertStringToBase64(string inputString)
    {
        string base64Json = null;
        byte[] requestBytes = System.Text.Encoding.UTF8.GetBytes(inputString);
        base64Json = Convert.ToBase64String(requestBytes);
        return base64Json;
    }
    private string PhonePeRefundRequestt(string action, string merchant_id, string transaction_id, string provider_ref_id, string amount, string merchant_order_id, string sub_merchant, string message, string checksum, string merchantkey, string merchantkeyindex, string cancelref, string refundby)
    {
        try
        {
            string p_RefundRefNo = "";
            DataTable dt = new DataTable();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "pg.f_refund_phonepe_request");
            MyCommand.Parameters.AddWithValue("p_action", action);
            MyCommand.Parameters.AddWithValue("p_merchant_id", merchant_id);
            MyCommand.Parameters.AddWithValue("p_transaction_id", transaction_id);
            MyCommand.Parameters.AddWithValue("p_provider_ref_id", provider_ref_id);
            MyCommand.Parameters.AddWithValue("p_amount", amount.ToString());
            MyCommand.Parameters.AddWithValue("p_merchant_order_id", merchant_order_id);
            MyCommand.Parameters.AddWithValue("p_sub_merchant", sub_merchant);
            MyCommand.Parameters.AddWithValue("p_message", message);
            MyCommand.Parameters.AddWithValue("p_checksum", checksum);
            MyCommand.Parameters.AddWithValue("p_merchantkey", merchantkey);
            MyCommand.Parameters.AddWithValue("p_merchantkeyindex", merchantkeyindex);
            MyCommand.Parameters.AddWithValue("p_cancelref", cancelref);
            MyCommand.Parameters.AddWithValue("p_refundby", refundby);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    p_RefundRefNo = dt.Rows[0]["refund_refno"].ToString();
                    return p_RefundRefNo;
                }
            }
            return p_RefundRefNo;
        }
        catch (Exception ex)
        {
            _err.Error_Log("PhonePeRefundRequestt-01", ex.Message);
            return "";
        }
    }
    private string PhonePeRefundResponsee(string success, string code, string message, string transactionid, string merchantid, string amount, string status, string mobilenumber, string providerreferenceid, string payresponsecode, string refundrefno, string cancelref, string refundby, string order_id)
    {
        try
        {
            DataTable mydt = new DataTable();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "pg.f_refund_phonepe_response");
            MyCommand.Parameters.AddWithValue("p_success", success);
            MyCommand.Parameters.AddWithValue("p_code", code);
            MyCommand.Parameters.AddWithValue("p_message", message);
            MyCommand.Parameters.AddWithValue("p_transactionid", transactionid);
            MyCommand.Parameters.AddWithValue("p_merchantid", merchantid);
            MyCommand.Parameters.AddWithValue("p_amount", amount);
            MyCommand.Parameters.AddWithValue("p_status", status);
            MyCommand.Parameters.AddWithValue("p_mobilenumber", mobilenumber);
            MyCommand.Parameters.AddWithValue("p_providerreferenceid", providerreferenceid);
            MyCommand.Parameters.AddWithValue("p_payresponsecode", payresponsecode);
            MyCommand.Parameters.AddWithValue("p_refundrefno", refundrefno);
            MyCommand.Parameters.AddWithValue("p_cancelref", cancelref);
            MyCommand.Parameters.AddWithValue("p_refundby", refundby);
            MyCommand.Parameters.AddWithValue("p_order_id", order_id);
            mydt = bll.SelectAll(MyCommand);
            if (mydt.TableName == "Success")
            {
                if (mydt.Rows.Count > 0)
                    return "Success";
                else
                    return "Error";
            }
            else
                return "Error";
        }
        catch (Exception ex)
        {
            _err.Error_Log("PhonePeRefundResponsee-001", ex.Message);
            return "Error";
        }
    }
    #endregion
    #endregion

    #region "Refund Status"
    public DataTable getRefundedTxnCount(string pgid, string transdate)
    {
        DataTable dt = new DataTable();
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "pg.f_getrefundedtrans");
        MyCommand.Parameters.AddWithValue("p_pmtgateway", Convert.ToInt32(pgid));
        MyCommand.Parameters.AddWithValue("p_transdate", transdate);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    public DataTable getRefundedTxnDtls(string pgid, string transdate)
    {
        DataTable dt = new DataTable();
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "pg.f_getrefundedtrans_dtls");
        MyCommand.Parameters.AddWithValue("p_pmtgateway", Convert.ToInt32(pgid));
        MyCommand.Parameters.AddWithValue("p_transdate", transdate);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }

 #region"Billdesk New"
    public async Task<string> RefundStatusResponseAPI(string refund_ref_no, string orderid, string txndate, string txnamt, string refamt, string updatedby)
    {
        string url = MrefundstatusurlNew;
        String merchatId = merchntidnew;
        //string pay_orderid = Session["pay_orderid"].ToString();
        //string refund_ref_no = Request.QueryString["RFNNO"].ToString();
        string txnid1 = "1236" + DateTime.Now.ToString("yyyyMMddhhmmss");
        DateTime Ddate = DateTime.Now;
        String secretKey = secretkeynew;
        string jsone_header = "{\r\n\"alg\":\"HS256\",\r\n\"clientid\":\""+clientidnew+"\"\r\n}";

        refundStatusPayload refundPayload = new refundStatusPayload();
        refundPayload.mercid = merchatId;
        refundPayload.merc_refund_ref_no = refund_ref_no;

        var json_payload = Newtonsoft.Json.JsonConvert.SerializeObject(refundPayload);


        //string json_payload = "{\r\n\"mercid\":\"ARUNACUAT\",\r\n\"merc_refund_ref_no\":\"" + refund_ref_no +
        //"\"\r\n}";
        var encodedPayload = Encode(json_payload);
        var encodedHeader = Encode(jsone_header);
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

        string refundstatusrefno = BilldeskRefundStatusRequestt(merchatId, hash, refund_ref_no, orderid, txndate, txnamt, refamt, updatedby, secretKey);


        ServicePointManager.Expect100Continue = true;
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
               | SecurityProtocolType.Tls11
               | SecurityProtocolType.Tls12
               | SecurityProtocolType.Ssl3;

        HttpResponseMessage response = await client.SendAsync(request);
        string responseBody = await response.Content.ReadAsStringAsync();
        var stream = responseBody; // responseBody is encoded return
        string[] parts = stream.Split('.');
        string header = parts[0];
        string Resp_payload = parts[1];
        string signature = parts[2];
        var trim_tokes = Decode(Resp_payload);
        var parsed = JObject.Parse(trim_tokes);
        string refund_id = (string)parsed.SelectToken("refundid") ?? "N/A"; 
        string transactionid = (string)parsed.SelectToken("transactionid") ?? "N/A";
        string order_id = (string)parsed.SelectToken("orderid") ?? "N/A";
        string transDate = (string)parsed.SelectToken("transaction_date") ?? "N/A";
        string txn_amount = (string)parsed.SelectToken("txn_amount") ?? "0";
        string refund_amount = (string)parsed.SelectToken("refund_amount") ?? "0"; 
        string currency = (string)parsed.SelectToken("currency") ?? "N/A"; 
        string refund_date = (string)parsed.SelectToken("refund_date") ?? "";
        string merc_refund_ref_no = (string)parsed.SelectToken("merc_refund_ref_no") ?? "N/A";

        string refund_status = (string)parsed.SelectToken("refund_status") ?? "N/A";

        string errorstatus = (string)parsed.SelectToken("status") ?? "N/A";
        string error_type = (string)parsed.SelectToken("error_type") ?? "N/A";
        string error_code = (string)parsed.SelectToken("error_code") ?? "N/A";
        string errormessage = (string)parsed.SelectToken("message") ?? "N/A";
        string status = "";
        if (refund_status == "N/A")
        {
            refund_status = errorstatus;
        }


        status= BilldeskRefundStatusResponsee(refund_id, transactionid, order_id, merchatId, transDate, txn_amount, refund_amount, currency, merc_refund_ref_no, refund_status, updatedby, refundstatusrefno, refund_date
            ,errorstatus,error_type,error_code,errormessage);

        //if (BILLDESKRefundResponse(refund_id, transactionid, order_id, merchatId, transDate, txn_amount, refund_amount, currency, merc_refund_ref_no, refund_status, "") == false)
        //{
        //    status = "Fail";
        //}
        //else
        //{
        //    status = "Success";
        //}
        return status;

    }

    private string BilldeskRefundStatusRequestt(string merchatId, string hash, string refund_ref_no, string orderid, string txndate,
        string txnamt, string refamt, string updatedby,string sercuritykey)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "pg.f_refundstatus_billdesk_request");
            MyCommand.Parameters.AddWithValue("p_merchatid", merchatId);
            MyCommand.Parameters.AddWithValue("p_hash", hash);
            MyCommand.Parameters.AddWithValue("p_refund_ref_no", refund_ref_no);
            MyCommand.Parameters.AddWithValue("p_orderid", orderid);
            MyCommand.Parameters.AddWithValue("p_txndate", txndate);
            MyCommand.Parameters.AddWithValue("p_txnamt", txnamt);
            MyCommand.Parameters.AddWithValue("p_refund_amount", refamt);
            MyCommand.Parameters.AddWithValue("p_updatedby", updatedby);
            MyCommand.Parameters.AddWithValue("p_sercuritykey", sercuritykey);
            DataTable dtt = new DataTable();
            dtt = bll.SelectAll(MyCommand);
            if (dtt.TableName == "Success")
            {
                if (dtt.Rows[0]["status"].ToString() == "SUCCESS")
                {
                    refund_ref_no = dtt.Rows[0]["refrefno"].ToString();
                    return refund_ref_no;
                }
                else
                {
                    return refund_ref_no;
                }
            }
            return refund_ref_no;
        }
        catch (Exception ex)
        {
            return "";
        }
    }

    public string BilldeskRefundStatusResponsee(string refund_id, string transaction_id, string order_id, string merchant_id, string transaction_date, string txn_amount, string refund_amount, string currency, string merc_refund_ref_no,
      string refund_status, string refunded_by, string txnrefundstatusrefno,string refunddate, string errorstatus, string error_type, string error_code, string errormessage)
    {
        try
        {
            string refund_ref_no = "";
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "pg.f_refundstatus_billdesk_response");

            MyCommand.Parameters.AddWithValue("p_refund_id", refund_id);
            MyCommand.Parameters.AddWithValue("p_transaction_id", transaction_id);
            MyCommand.Parameters.AddWithValue("p_order_id", order_id);
            MyCommand.Parameters.AddWithValue("p_merchant_id", merchant_id);
            MyCommand.Parameters.AddWithValue("p_transaction_date", transaction_date);
            MyCommand.Parameters.AddWithValue("p_txn_amount", txn_amount);
            MyCommand.Parameters.AddWithValue("p_refund_amount",refund_amount);
            MyCommand.Parameters.AddWithValue("p_currency", currency);
            MyCommand.Parameters.AddWithValue("p_merc_refund_ref_no", merc_refund_ref_no);
            MyCommand.Parameters.AddWithValue("p_refund_status", refund_status);
            MyCommand.Parameters.AddWithValue("p_refunded_by", refunded_by);
            MyCommand.Parameters.AddWithValue("p_txnrefundstatusrefno", txnrefundstatusrefno);
            MyCommand.Parameters.AddWithValue("p_refunddate", refunddate);

            MyCommand.Parameters.AddWithValue("p_errorcode", error_code);
            MyCommand.Parameters.AddWithValue("p_errortype", error_type);
            MyCommand.Parameters.AddWithValue("p_errormessage", errormessage);
            MyCommand.Parameters.AddWithValue("p_errorstatus", errorstatus);
            DataTable dtt = new DataTable();
            dtt = bll.SelectAll(MyCommand);
            if (dtt.TableName == "Success")
            {
                if (dtt.Rows[0]["status"].ToString() == "SUCCESS")
                {
                    refund_ref_no = dtt.Rows[0]["status"].ToString();
                    return refund_ref_no;
                }
                else
                {
                    return refund_ref_no;
                }
            }
            return refund_ref_no;
        }
        catch (Exception ex)
        {
            return "";
        }
    }


    #endregion

    #region "Phonepe"
    public class PhonePeRefundStatusResponseBody
    {
        public bool success { get; set; }
        public string code { get; set; }
        public string message { get; set; }
        public PhonePeRefundStatusData data { get; set; }
    }
    public class PhonePeRefundStatusData
    {
        public string merchantId { get; set; }
        public string transactionId { get; set; }
        public string providerReferenceId { get; set; }
        public int amount { get; set; }
        public string merchantOrderId { get; set; }
        public string paymentState { get; set; }
        public string payResponseCode { get; set; }
        public List<PhonePeRefundStatusPaymentMode> paymentModes { get; set; }
        public PhonePeRefundStatusTransactionContext transactionContext { get; set; }
    }
    public class PhonePeRefundStatusPaymentMode
    {
        public string mode { get; set; }
        public string transactionState { get; set; }
        public string transactionResponseCode { get; set; }
        public int flags { get; set; }
        public int amount { get; set; }
        public int actualAmount { get; set; }
        public string sourceExternalId { get; set; }
        public string instrumentId { get; set; }
        public string type { get; set; }
        public string accountHolderName { get; set; }
        public string utr { get; set; }
        public string upiTransactionId { get; set; }
        public string accountAuthMode { get; set; }
        public string accountType { get; set; }
    }
    public class PhonePeRefundStatusTransactionContext
    {
        public string storeId { get; set; }
        public string terminalId { get; set; }
    }
    public string PhonePeRefundStatusAPI(string transactionId, string mCancelRef, string mRefundRef, string mtranamt, string _statusby)
    {
        string resp = "Error";
        string headerString = string.Format("/v3/transaction/{0}/{1}/status{2}", merchantId, mCancelRef, merchantKey);
        // Console.WriteLine("headerString: " & headerString)
        string checksum = GenerateSha256ChecksumFromBase64Json("", headerString);
        checksum = checksum + "###" + merchantKeyIndex;
        // Console.WriteLine(checksum)
        string refundStatusRefNo = PhonePe_refundStatus_Request(merchantId, transactionId, merchantKey, checksum, mCancelRef, mRefundRef, _statusby);
        if (refundStatusRefNo == "")
        {
            return resp;
        }
        string txnURL = PHONEPE_STAGE_BASE_URL;
        string urlSuffix = string.Format("/v3/transaction/{0}/{1}/status", merchantId, mCancelRef);
        txnURL = txnURL + urlSuffix;
        try
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(txnURL);
            webRequest.Method = "GET";
            webRequest.Headers.Add("x-verify", checksum);
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string responseData = string.Empty;
            using (StreamReader responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream()))
            {
                responseData = responseReader.ReadToEnd();
                _err.Error_Log("PhonePeRefundStatusAPI-responseData", responseData);
                if (responseData.Length > 0)
                {
                    PhonePeRefundStatusResponseBody responseBody = JsonConvert.DeserializeObject<PhonePeRefundStatusResponseBody>(responseData);
                    string success_str = "";
                    bool success = responseBody.success;
                    if (success == true)
                        success_str = "true";
                    else
                        success_str = "false";
                    string code = responseBody.code;
                    string message = responseBody.message;
                    PhonePeRefundStatusData data = responseBody.data;
                    string data_merchantId = data.merchantId;
                    string data_transactionId = data.transactionId;
                    string providerReferenceId = data.providerReferenceId;
                    int data_amount = data.amount;
                    string paymentState = data.paymentState;
                    string payResponseCode = data.payResponseCode;
                    string PM_mode = "";
                    string PM_amount = "";
                    string PM_utr = "";
                    if (data.paymentModes != null)
                    {
                        foreach (PhonePeRefundStatusPaymentMode paymentMode in data.paymentModes)
                        {
                            PM_mode = paymentMode.mode;
                            PM_amount = paymentMode.amount.ToString();
                            PM_utr = paymentMode.utr;
                        }
                    }
                    string storeId = "";
                    string terminalId = "";
                    if (data.transactionContext != null)
                    {
                        PhonePeRefundStatusTransactionContext datatransactionContext = data.transactionContext;
                        storeId = datatransactionContext.storeId;
                        terminalId = datatransactionContext.terminalId;
                    }
                    resp = PhonePe_refundStatus_Response(refundStatusRefNo, transactionId, success_str, code, message, data_merchantId,
                        data_transactionId, data_amount.ToString(), providerReferenceId, paymentState, payResponseCode, PM_mode, PM_amount,
                        PM_utr, storeId, terminalId, mCancelRef, mRefundRef, _statusby);
                }
            }
            return resp;
        }
        catch (Exception ex)
        {
            _err.Error_Log("PhonePeRefundStatusAPI-ex", ex.Message);
            return "Error";
        }
    }
    private string PhonePe_refundStatus_Request(string merchantId, string transactionId, string merchantKey, string checksum, string mCancelRef, string mRefundRef, string _statusby)
    {
        try
        {
            string p_RefundStatusRefNo = "";
            DataTable dt = new DataTable();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "pg.f_refundstatus_phonepe_request");
            MyCommand.Parameters.AddWithValue("p_merchant_id", merchantId);
            MyCommand.Parameters.AddWithValue("p_order_id", transactionId);
            MyCommand.Parameters.AddWithValue("p_merchant_key", merchantKey);
            MyCommand.Parameters.AddWithValue("p_checksum", checksum);
            MyCommand.Parameters.AddWithValue("p_cancelref", mCancelRef);
            MyCommand.Parameters.AddWithValue("p_refundrefno", mRefundRef);
            MyCommand.Parameters.AddWithValue("p_updatedby", _statusby);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    p_RefundStatusRefNo = dt.Rows[0]["refundstatus_refno"].ToString();
                    return p_RefundStatusRefNo;
                }
            }
            return p_RefundStatusRefNo;
        }
        catch (Exception ex)
        {
            _err.Error_Log("PhonePe_refundStatus_Request-01", ex.Message);
            return "";
        }
    }
    private string PhonePe_refundStatus_Response(string refundStatusRefNo, string order_id, string success_str, string code, string message, string data_merchantId, string data_transactionId, string data_amount, string providerReferenceId, string paymentState, string payResponseCode, string pM_mode, string pM_amount, string pM_utr, string storeId, string terminalId, string CancelRef, string RefundRef, string _statusby)
    {
        try
        {
            string p_finalStatus = "Error";
            DataTable dt = new DataTable();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "pg.f_refundstatus_phonepe_response");
            MyCommand.Parameters.AddWithValue("p_refundstatusrefno", refundStatusRefNo);
            MyCommand.Parameters.AddWithValue("p_order_id", order_id);
            MyCommand.Parameters.AddWithValue("p_success_str", success_str);
            MyCommand.Parameters.AddWithValue("p_code", code);
            MyCommand.Parameters.AddWithValue("p_message", message);
            MyCommand.Parameters.AddWithValue("p_data_merchantid", data_merchantId);
            MyCommand.Parameters.AddWithValue("p_data_transactionid", data_transactionId);
            MyCommand.Parameters.AddWithValue("p_data_amount", data_amount);
            MyCommand.Parameters.AddWithValue("p_providerreferenceid", providerReferenceId);
            MyCommand.Parameters.AddWithValue("p_paymentstate", paymentState);
            MyCommand.Parameters.AddWithValue("p_payresponsecode", payResponseCode);
            MyCommand.Parameters.AddWithValue("p_pm_mode", pM_mode);
            MyCommand.Parameters.AddWithValue("p_pm_amount", pM_amount);
            MyCommand.Parameters.AddWithValue("p_pm_utr", pM_utr);
            MyCommand.Parameters.AddWithValue("p_storeid", storeId);
            MyCommand.Parameters.AddWithValue("p_terminalid", terminalId);
            MyCommand.Parameters.AddWithValue("p_cancelref", CancelRef);
            MyCommand.Parameters.AddWithValue("p_refundref", RefundRef);
            MyCommand.Parameters.AddWithValue("p_updatedby", _statusby);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    p_finalStatus = dt.Rows[0]["final_status"].ToString();
                    return p_finalStatus;
                }
            }
            return p_finalStatus;
        }
        catch (Exception ex)
        {
            _err.Error_Log("PhonePe_refundStatus_Response-01", ex.Message);
            return "Error";
        }
    }
    #endregion
    #endregion
 #region "Exception"
    public DataTable ExceptionCounts()
    {
        DataTable dt = new DataTable();
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "pg.fn_get_pg_exception_count");
      
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    #endregion
}