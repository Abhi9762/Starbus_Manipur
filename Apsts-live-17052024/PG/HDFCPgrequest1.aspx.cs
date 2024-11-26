using CCA.Util;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PG_HDFCPgrequest : System.Web.UI.Page
{
    public string merchant_id, order_id, currency, amount, redirect_url, cancel_url, language;
    public string billing_name, billing_address, billing_city, billing_state, billing_zip, billing_country, billing_tel, billing_email;
    public string delivery_name, delivery_address, delivery_city, delivery_state, delivery_zip, delivery_country, delivery_tel;
    public string merchant_param1, merchant_param2, merchant_param3, merchant_param4, merchant_param5, promo_code, customer_identifier;
    string strEncRequest = "";
    NpgsqlCommand MyCommand;
    sbBLL bll = new sbBLL();
    HDFC hdfc = new HDFC();
    CCACrypto ccaCrypto = new CCACrypto();
    string workingKey = System.Configuration.ConfigurationManager.AppSettings["workingKey"]; // workingKey = "86A87D152DE821494591399605B78EB5";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            language = "";
            billing_address = "";
            billing_zip = "";
            billing_city = "";
            billing_state = "";
            billing_country = "";
            delivery_name = "";
            delivery_address = "";
            delivery_city = "";
            delivery_state = "";
            delivery_zip = "";
            delivery_country = "";
            delivery_tel = "";
            merchant_param1 = "";
            merchant_param2 = "";
            merchant_param3 = "";
            merchant_param4 = "";
            merchant_param5 = "";
            promo_code = "";
            customer_identifier = "";
            merchant_id = System.Configuration.ConfigurationManager.AppSettings["merchant_id"]; //merchant_id = "909823";

            string PathAndQuery = Request.Url.PathAndQuery;
            string url_host = Request.Url.AbsoluteUri.Replace(PathAndQuery, "");
            string respoURL = url_host + "/starbusarn_v1/PG/HDFCPgresponse.aspx";

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
                    amount = dt.Rows[0]["amount_total"].ToString();
                }
            }
            else if (requestid == "11")//Traveller Topup wallet 
            {
                order_id = Session["transrefno"].ToString();
                amount = Convert.ToDecimal(Session["depositamt"].ToString()).ToString();
                billing_name = "Traveller Wallet Topup";
                billing_tel = Session["_UserCode"].ToString();
                billing_email = "";
            }
            else if (requestid == "111")//Ticket Booking From Mobile App
            {
                order_id = Session["strTicketNo"].ToString();
                amount = Convert.ToDecimal(Session["_UTCTicketFinalAmount"].ToString()).ToString();
                billing_name = "Ticket Booking";
                billing_tel = Session["_pmtCTZMobile"].ToString();
                billing_email = Session["_pmtCTZEMAIL"].ToString();
            }
            else if (requestid == "1111")//Traveller Topup wallet  From Mobile App
            {
                order_id = Session["transrefno"].ToString();
                amount = Convert.ToDecimal(Session["depositamt"].ToString()).ToString();
                billing_name = "Traveller Wallet Topup";
                billing_tel = Session["_UserCode"].ToString();
                billing_email = "";
            }
            else if (requestid == "2")//Agent Topup wallet
            {
                order_id = Session["transrefno"].ToString();
                amount = Convert.ToDecimal(Session["depositamt"].ToString()).ToString();
                billing_name = "Agent Recharge Account";
                billing_tel = Session["MobileNo"].ToString();
                billing_email = Session["Emailid"].ToString();
            }
            else if (requestid == "3")//Bus Passes
            {
                order_id = Session["currtranrefno"].ToString();
                amount = Convert.ToDecimal(Session["AMOUNT"].ToString()).ToString();
                billing_name = "Bus Pass";
                billing_tel = Session["MobileNo"].ToString();
                billing_email = Session["Emailid"].ToString();
            }
            else
            {
                Session["_ErrorMsg"] = "Invalid Transaction";
                Response.Redirect("../errorpage.aspx");
            }
            currency = "INR";
            redirect_url = respoURL;
            cancel_url = respoURL;
            string ccaRequest = "";
            foreach (string name in Request.Form)
            {
                if (name != null)
                {
                    if (!name.StartsWith("_"))
                    {
                        ccaRequest = ccaRequest + name + "=" + Request.Form[name] + "&";
                    }
                }
            }
            strEncRequest = "";//ccaCrypto.Encrypt(ccaRequest, workingKey);

            if (hdfc.HDFCRequest(requestid, merchant_id, order_id, currency, amount, redirect_url, cancel_url, language, billing_name, billing_address,
                billing_city, billing_state, billing_zip, billing_country, billing_tel, billing_email, delivery_name, delivery_address, delivery_city, delivery_state,
                delivery_zip, delivery_country, delivery_tel, merchant_param1, merchant_param2, merchant_param3, merchant_param4, merchant_param5, promo_code, customer_identifier, strEncRequest) == false)
            {
                Session["_ErrorMsg"] = "Invalid Transaction -";
                Response.Redirect("../errorpage.aspx");
            }
        }
        catch (Exception ex)
        {
            Session["_ErrorMsg"] = "Invalid Transaction -" + ex.ToString();
            Response.Redirect("../errorpage.aspx");
        }
    }
}

