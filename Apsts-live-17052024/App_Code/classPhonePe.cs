using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Text;
using System.Security.Cryptography;
using System.Net;
using System.IO;
/// <summary>
/// Summary description for classPhonePe
/// </summary>
public class classPhonePe
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private const string PHONEPE_STAGE_BASE_URL = "https://mercury-uat.phonepe.com/enterprise-sandbox";

    private string merchantKey = System.Configuration.ConfigurationManager.AppSettings["phonePe_checksumSaltKey"];
    private string merchantKeyIndex = System.Configuration.ConfigurationManager.AppSettings["phonePe_checksumSaltIndex"];
    private string merchantId = System.Configuration.ConfigurationManager.AppSettings["phonePe_merchantId"];
    private string terminalId = "terminal1";
    ErrorLog _err = new ErrorLog();

    public classPhonePe()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public string SendPaymentRequest(string transactionId, string storeId, int amount, int expiresIn, string requestFromID)
    {
        PhonePeCollectRequest phonePeCollectRequest = new PhonePeCollectRequest();
        phonePeCollectRequest.merchantId = merchantId;
        phonePeCollectRequest.transactionId = transactionId;
        phonePeCollectRequest.merchantOrderId = transactionId;
        phonePeCollectRequest.amount = amount;
        phonePeCollectRequest.expiresIn = expiresIn;
        phonePeCollectRequest.storeId = storeId;
        phonePeCollectRequest.terminalId = terminalId;
        string jsonStr = JsonConvert.SerializeObject(phonePeCollectRequest);
        Console.WriteLine(jsonStr);
        string base64Json = ConvertStringToBase64(jsonStr);
        Console.WriteLine(base64Json);
        string jsonSuffixString = "/v3/qr/init" + merchantKey;
        string checksum = GenerateSha256ChecksumFromBase64Json(base64Json, jsonSuffixString);
        checksum = checksum + "###" + merchantKeyIndex;
        Console.WriteLine(checksum);
        string txnURL = PHONEPE_STAGE_BASE_URL + "/v3/qr/init";
        // Console.WriteLine("txnURL : " & txnURL)
        string callbackURL = "https://utcdemo.uk.gov.in/starbusarn_audit/PG/PhonePeResponse.aspx";

        try
        {
            bool b = QRInit_Request(requestFromID, merchantId, transactionId, transactionId, amount, expiresIn,
              storeId, terminalId, callbackURL, txnURL, checksum, merchantKey, merchantKeyIndex);

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(txnURL);
            webRequest.Method = "POST";
            webRequest.ContentType = "application/json";
            webRequest.Headers.Add("x-verify", checksum);
            webRequest.Headers.Add("X-CALLBACK-URL", callbackURL);

            PhonePeCollectApiRequestBody phonePeCollectApiRequestBody = new PhonePeCollectApiRequestBody();
            phonePeCollectApiRequestBody.request = base64Json;
            string jsonBody = JsonConvert.SerializeObject(phonePeCollectApiRequestBody);

            using (StreamWriter requestWriter = new StreamWriter(webRequest.GetRequestStream()))
            {
                requestWriter.Write(jsonBody);
            }

            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
 	    ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

            string responseData = string.Empty;
            using (StreamReader responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream()))
            {
                responseData = responseReader.ReadToEnd();
                if (responseData.Length > 0)
                {
                    PhonePeQRInitResponseBody responseBody = JsonConvert.DeserializeObject<PhonePeQRInitResponseBody>(responseData);

                    string success_str = "";
                    bool success = responseBody.success;
                    if (success == true)
                        success_str = "true";
                    else
                        success_str = "false";

                    string code = responseBody.code;
                    string message = responseBody.message;
                    DataQRInitResponse data = responseBody.data;

                    string data_merchantId = data.merchantId;
                    string data_transactionId = data.transactionId;
                    int data_amount = data.amount;
                    string data_qrString = data.qrString;

                    bool bb = QRInit_Response(transactionId, success_str, code, message, data_transactionId, data_amount.ToString(), data_merchantId, data_qrString, requestFromID);
                }
            }
            return responseData.ToString();
        }
        catch (Exception ex)
        { return "" + ex.ToString(); }

    }
    public string ConvertStringToBase64(string inputString)
    {
        string base64Json = null;
        byte[] requestBytes = Encoding.UTF8.GetBytes(inputString);
        base64Json = Convert.ToBase64String(requestBytes);
        return base64Json;
    }
    private string GenerateSha256ChecksumFromBase64Json(string base64JsonString, string jsonSuffixString)
    {
        string checksum = null;
        SHA256 sha256 = SHA256.Create();
        string checksumString = base64JsonString + jsonSuffixString;
        byte[] checksumBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(checksumString));

        foreach (byte b in checksumBytes)

            checksum += string.Format("{0:x2}", b);

        return checksum;
    }
    public bool QRInit_Request(string P_REQUEST_FROM_ID, string P_MERCHANT_ID, string P_ORDER_ID, string P_TXN_ID, int P_AMOUNT, int P_EXPIRES_SECONDS, string P_STORE_ID, string P_TERMINAL_ID, string P_CALLBACK_URL, string P_TXN_URL, string P_CHECKSUM, string P_MERCHANT_KEY, string P_MERCHANT_KEY_INDEX)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            DataTable dt = new DataTable();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "phonepe.f_phonepe_qrinit_request");
            MyCommand.Parameters.AddWithValue("p_request_from_id", Convert.ToInt32(P_REQUEST_FROM_ID));
            MyCommand.Parameters.AddWithValue("p_merchant_id", P_MERCHANT_ID);
            MyCommand.Parameters.AddWithValue("p_order_id", P_ORDER_ID);
            MyCommand.Parameters.AddWithValue("p_txn_id", P_TXN_ID);
            MyCommand.Parameters.AddWithValue("p_amount", P_AMOUNT.ToString());
            MyCommand.Parameters.AddWithValue("p_expires_second", P_EXPIRES_SECONDS.ToString());
            MyCommand.Parameters.AddWithValue("p_store_id", P_STORE_ID);
            MyCommand.Parameters.AddWithValue("p_terminal_id", P_TERMINAL_ID);
            MyCommand.Parameters.AddWithValue("p_callback_url", P_CALLBACK_URL);
            MyCommand.Parameters.AddWithValue("p_txn_url", P_TXN_URL);
            MyCommand.Parameters.AddWithValue("p_checksum", P_CHECKSUM);
            MyCommand.Parameters.AddWithValue("p_merchant_key", P_MERCHANT_KEY);
            MyCommand.Parameters.AddWithValue("p_merchant_key_index", P_MERCHANT_KEY_INDEX);
            dt = bll.SelectAll(MyCommand);

            if (dt.TableName == "Success")
                return true;
            return false;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    public bool QRInit_Response(string P_ORDER_ID, string P_STATUS_SUCCESS, string P_STATUS_CODE, string P_STATUS_MESSAGE, string P_DATA_TXNID, string P_DATA_AMOUNT, string P_DATA_MERCHANTID, string P_DATA_QRSTRING, string P_REQUEST_FROM_ID)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            DataTable dt = new DataTable();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "phonepe.f_phonepe_qrinit_response");
            MyCommand.Parameters.AddWithValue("p_order_id", P_ORDER_ID);
            MyCommand.Parameters.AddWithValue("p_status_success", P_STATUS_SUCCESS);
            MyCommand.Parameters.AddWithValue("p_status_code", P_STATUS_CODE);
            MyCommand.Parameters.AddWithValue("p_status_message", P_STATUS_MESSAGE);
            MyCommand.Parameters.AddWithValue("p_data_txnid", P_DATA_TXNID);
            MyCommand.Parameters.AddWithValue("p_data_amount", P_DATA_AMOUNT);
            MyCommand.Parameters.AddWithValue("p_data_merchant_id", P_DATA_MERCHANTID);
            MyCommand.Parameters.AddWithValue("p_data_qrstring", P_DATA_QRSTRING);
            MyCommand.Parameters.AddWithValue("p_request_from_id", Convert.ToInt32(P_REQUEST_FROM_ID));
            dt = bll.SelectAll(MyCommand);

            if (dt.TableName == "Success")
                return true;
            return false;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    public DataTable check_status(string p_txn_id)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            DataTable dt = new DataTable();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "phonepe.f_phonepe_txn_status_check");
            MyCommand.Parameters.AddWithValue("p_txn_id", p_txn_id);
            dt = bll.SelectAll(MyCommand);

            return dt;
        }
        catch (Exception ex)
        {
            return dt;
        }
    }

    #region"Classes"
    public class PhonePeCollectRequest
    {
        public string merchantId;
        public string transactionId;
        public string merchantOrderId;
        public int amount;
        public int expiresIn;
        public string storeId;
        public string terminalId;
    }
    public class PhonePeCollectApiRequestBody
    {
        public string request;
    }
    public class PhonePeQRInitResponseBody
    {
        public bool success;
        public string code;
        public string message;
        public DataQRInitResponse data;
    }
    public class DataQRInitResponse
    {
        public string merchantId;
        public string transactionId;
        public int amount;
        public string qrString;
    }

    #endregion
    #region "Callback Save"

    public class PaymentData
    {
        public bool success { get; set; }
        public string code { get; set; }
        public string message { get; set; }
        public PaymentDetails data { get; set; }
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
    public void saveCallbackResponse(string jsonData)
    {
        try
        {

            // Create an instance of JavaScriptSerializer
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            // Deserialize the JSON data into your custom class
            PaymentData paymentData = serializer.Deserialize<PaymentData>(jsonData);

            string success_str = "";
            bool success = paymentData.success;
            if (success == true)
                success_str = "true";
            else
                success_str = "false";

            string code = paymentData.code;
            string msg = paymentData.message;

            // Extract data from the deserialized object and add to the DataTable
            PaymentDetails data = paymentData.data;

            string PM_mode = "";
            string PM_amount = "";
            string PM_utr = "";
	    string PM_providerReferenceId = "";

            if (data.providerReferenceId != null)
            {
                PM_providerReferenceId = data.providerReferenceId;
            }

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
                if (datatransactionContext.storeId != null)
                {
                    storeId = datatransactionContext.storeId;
                }
                if (datatransactionContext.terminalId != null)
                {
                    terminalId = datatransactionContext.terminalId;
                }
            }

            bool b = callback_Response(success_str, code, msg, data.transactionId, data.merchantId, PM_providerReferenceId, data.amount.ToString(), data.paymentState, data.payResponseCode, PM_mode, PM_amount, PM_utr, storeId, terminalId);

        }
        catch (Exception ex)
        {
            _err.Error_Log("ClassPhonePe-saveCallbackResponse", ex.Message);
        }
    }

    public bool callback_Response(string P_STATUS_SUCCESS, string P_STATUS_CODE, string P_STATUS_MESSAGE, string P_DATA_TXNID, string P_DATA_MERCHANTID, string P_DATA_PROVIDERREFID, string P_DATA_AMOUNT, string P_DATA_PAYMENTSTATE, string P_DATA_PAYRESPONSECODE, string P_DATA_PMTMODESDATA_MODE, string P_DATA_PMTMODESDATA_AMOUNT, string P_DATA_PMTMODESDATA_UTR, string P_DATA_TXNCONTEXT_STOREID, string P_DATA_TXNCONTEXT_TERMINALID)
    {
        try
        {

            MyCommand = new NpgsqlCommand();
            DataTable dt = new DataTable();

            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "phonepe.f_phonepe_callback_response");
            MyCommand.Parameters.AddWithValue("p_status_success", P_STATUS_SUCCESS);
            MyCommand.Parameters.AddWithValue("p_status_code", P_STATUS_CODE);
            MyCommand.Parameters.AddWithValue("p_status_message", P_STATUS_MESSAGE);
            MyCommand.Parameters.AddWithValue("p_data_txnid", P_DATA_TXNID);
            MyCommand.Parameters.AddWithValue("p_data_merchantid", P_DATA_MERCHANTID);

            MyCommand.Parameters.AddWithValue("p_data_providerrefid", P_DATA_PROVIDERREFID);
            MyCommand.Parameters.AddWithValue("p_data_amount", P_DATA_AMOUNT);
            MyCommand.Parameters.AddWithValue("p_data_paymentstate", P_DATA_PAYMENTSTATE);
            MyCommand.Parameters.AddWithValue("p_data_payresponsecode", P_DATA_PAYRESPONSECODE);

            MyCommand.Parameters.AddWithValue("p_data_pmtmodesdata_mode", P_DATA_PMTMODESDATA_MODE);
            MyCommand.Parameters.AddWithValue("p_data_pmtmodesdata_amount", P_DATA_PMTMODESDATA_AMOUNT);
            MyCommand.Parameters.AddWithValue("p_data_pmtmodesdata_utr", P_DATA_PMTMODESDATA_UTR);

            MyCommand.Parameters.AddWithValue("p_data_txncontext_storeid", P_DATA_TXNCONTEXT_STOREID);
            MyCommand.Parameters.AddWithValue("p_data_txncontext_terminalid", P_DATA_TXNCONTEXT_TERMINALID);

            dt = bll.SelectAll(MyCommand);
            _err.Error_Log("ClassPhonePe-callback_Response-TableName", dt.TableName);
            if (dt.TableName == "Success")
                return true;
            return false;
        }
        catch (Exception ex)
        {
            _err.Error_Log("ClassPhonePe-callback_Response", ex.Message);
            return false;
        }
    }

    #endregion
}