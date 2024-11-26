using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PG_TESTDBLVilldesk : System.Web.UI.Page
{
    BilldeskSha billdesk = new BilldeskSha();
    public string Murl = "https://www.billdesk.com/pgidsk/PGIQueryController";
    protected void Page_Load(object sender, EventArgs e)
    {
        DBl_BillDESK("700564566820231124153443");
    }

    private void DBl_BillDESK(string order_id)
    {
        try
        {
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

            Response.Write(MString);
            Response.Write("<br/>");

            string Mhash;
            Mhash = billdesk.GetHMACSHA256(MString, checksumkey);
            MString = MString + "|" + Mhash.ToUpper().Trim();

            Response.Write(MString);
            Response.Write("<br/>");

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

            Response.Write(responseFromServer);
            Response.Write("<br/>");



        }
        catch(Exception ex)
        {
 Response.Write(ex.Message);
            Response.Write("<br/>");
        }
    }
}