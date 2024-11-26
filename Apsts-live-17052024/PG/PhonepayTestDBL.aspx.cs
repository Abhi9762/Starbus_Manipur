using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PG_PhonepayTestDBL : System.Web.UI.Page
{
    private const string PHONEPE_STAGE_BASE_URL = "https://mercury-t2.phonepe.com";
    private string merchantKey = System.Configuration.ConfigurationManager.AppSettings["phonePe_checksumSaltKey"];
    private string merchantKeyIndex = System.Configuration.ConfigurationManager.AppSettings["phonePe_checksumSaltIndex"];
    private string merchantId = System.Configuration.ConfigurationManager.AppSettings["phonePe_merchantId"];
    private string terminalId = "terminal1";
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }

    public void PhonePeStatusAPI(string transactionId)
    {
       
        string headerString = string.Format("/v3/transaction/{0}/{1}/status{2}", merchantId, transactionId, merchantKey);
      
        string checksum = GenerateSha256ChecksumFromBase64Json("", headerString);
        checksum = checksum + "###" + merchantKeyIndex;
     
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
                Response.Write(responseData);
            }
        }
        catch (Exception e)
        {
            Response.Write(e.Message);
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
  protected void btncheck_Click(object sender, EventArgs e)
    {
        string tid = txtid.Text.ToString();
        PhonePeStatusAPI(tid);
    }
}