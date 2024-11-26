using CCA.Util;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class PG_HDFCPgresponse : System.Web.UI.Page
{
    NpgsqlCommand MyCommand;
    sbBLL bll = new sbBLL();
    HDFC hdfc = new HDFC();
    CCACrypto ccaCrypto = new CCACrypto();

    string order_id, tracking_id, bank_ref_no, order_status, failure_message, payment_mode, card_name, status_code, status_message, currency, amount;
    string billing_name, billing_address, billing_city, billing_state, billing_zip, billing_country, billing_tel, billing_email;
    string delivery_name, delivery_address, delivery_city, delivery_state, delivery_zip, delivery_country, delivery_tel;
    string merchant_param1, merchant_param2, merchant_param3, merchant_param4, merchant_param5;
    string vault, offer_type, offer_code, discount_value, mer_amount, eci_value, retry, response_code, billing_notes, trans_date, bin_country, token_eligibility;


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            string workingKey = System.Configuration.ConfigurationManager.AppSettings["workingKey"]; // workingKey = "86A87D152DE821494591399605B78EB5";
            CCACrypto ccaCrypto = new CCACrypto();
            string encResponse = ccaCrypto.Decrypt(Request.Form["encResp"], workingKey);
            NameValueCollection Params = new NameValueCollection();
            string[] segments = encResponse.Split('&');
            foreach (string seg in segments)
            {
                string[] parts = seg.Split('=');
                if (parts.Length > 0)
                {
                    string Key = parts[0].Trim();
                    string Value = parts[1].Trim();
                    Params.Add(Key, Value);
                }
            }
            //for (int i = 0; i < Params.Count; i++)
            //{
            //    Response.Write(Params.Keys[i] + " = " + Params[i] + "<br>");
            //}
            order_id = Params["order_id"].ToString();
            tracking_id = String.IsNullOrEmpty(Params["tracking_id"].ToString()) == true ? "" : Params["tracking_id"].ToString();
            bank_ref_no = String.IsNullOrEmpty(Params["bank_ref_no"].ToString()) == true ? "" : Params["bank_ref_no"].ToString();
            order_status = String.IsNullOrEmpty(Params["order_status"].ToString()) == true ? "" : Params["order_status"].ToString();
            failure_message = String.IsNullOrEmpty(Params["failure_message"].ToString()) == true ? "" : Params["failure_message"].ToString();
            payment_mode = String.IsNullOrEmpty(Params["payment_mode"].ToString()) == true ? "" : Params["payment_mode"].ToString();
            card_name = String.IsNullOrEmpty(Params["card_name"].ToString()) == true ? "" : Params["card_name"].ToString();
            status_code = String.IsNullOrEmpty(Params["status_code"].ToString()) == true ? "" : Params["status_code"].ToString();
            status_message = String.IsNullOrEmpty(Params["status_message"].ToString()) == true ? "" : Params["status_message"].ToString();
            currency = String.IsNullOrEmpty(Params["currency"].ToString()) == true ? "" : Params["currency"].ToString();
            amount = String.IsNullOrEmpty(Params["amount"].ToString()) == true ? "" : Params["amount"].ToString();
            billing_name = String.IsNullOrEmpty(Params["billing_name"].ToString()) == true ? "" : Params["billing_name"].ToString();
            billing_address = String.IsNullOrEmpty(Params["billing_address"].ToString()) == true ? "" : Params["billing_address"].ToString();
            billing_city = String.IsNullOrEmpty(Params["billing_city"].ToString()) == true ? "" : Params["billing_city"].ToString();
            billing_state = String.IsNullOrEmpty(Params["billing_state"].ToString()) == true ? "" : Params["billing_state"].ToString();
            billing_zip = String.IsNullOrEmpty(Params["billing_zip"].ToString()) == true ? "" : Params["billing_zip"].ToString();
            billing_country = String.IsNullOrEmpty(Params["billing_country"].ToString()) == true ? "" : Params["billing_country"].ToString();
            billing_tel = String.IsNullOrEmpty(Params["billing_tel"].ToString()) == true ? "" : Params["billing_tel"].ToString();
            billing_email = String.IsNullOrEmpty(Params["billing_email"].ToString()) == true ? "" : Params["billing_email"].ToString();
            delivery_name = String.IsNullOrEmpty(Params["delivery_name"].ToString()) == true ? "" : Params["delivery_name"].ToString();
            delivery_address = String.IsNullOrEmpty(Params["delivery_address"].ToString()) == true ? "" : Params["delivery_address"].ToString();
            delivery_city = String.IsNullOrEmpty(Params["delivery_city"].ToString()) == true ? "" : Params["delivery_city"].ToString();
            delivery_state = String.IsNullOrEmpty(Params["delivery_state"].ToString()) == true ? "" : Params["delivery_state"].ToString();
            delivery_zip = String.IsNullOrEmpty(Params["delivery_zip"].ToString()) == true ? "" : Params["delivery_zip"].ToString();
            delivery_country = String.IsNullOrEmpty(Params["delivery_country"].ToString()) == true ? "" : Params["delivery_country"].ToString();
            delivery_tel = String.IsNullOrEmpty(Params["delivery_tel"].ToString()) == true ? "" : Params["delivery_tel"].ToString();
            merchant_param1 = String.IsNullOrEmpty(Params["merchant_param1"].ToString()) == true ? "" : Params["merchant_param1"].ToString();
            merchant_param2 = String.IsNullOrEmpty(Params["merchant_param2"].ToString()) == true ? "" : Params["merchant_param2"].ToString();
            merchant_param3 = String.IsNullOrEmpty(Params["merchant_param3"].ToString()) == true ? "" : Params["merchant_param3"].ToString();
            merchant_param4 = String.IsNullOrEmpty(Params["merchant_param4"].ToString()) == true ? "" : Params["merchant_param4"].ToString();
            merchant_param5 = String.IsNullOrEmpty(Params["merchant_param5"].ToString()) == true ? "" : Params["merchant_param5"].ToString();
            vault = String.IsNullOrEmpty(Params["vault"].ToString()) == true ? "" : Params["vault"].ToString();
            offer_type = String.IsNullOrEmpty(Params["offer_type"].ToString()) == true ? "" : Params["offer_type"].ToString();
            offer_code = String.IsNullOrEmpty(Params["offer_code"].ToString()) == true ? "" : Params["offer_code"].ToString();
            discount_value = String.IsNullOrEmpty(Params["discount_value"].ToString()) == true ? "" : Params["discount_value"].ToString();
            mer_amount = String.IsNullOrEmpty(Params["mer_amount"].ToString()) == true ? "" : Params["mer_amount"].ToString();
            eci_value = String.IsNullOrEmpty(Params["eci_value"].ToString()) == true ? "" : Params["eci_value"].ToString();
            retry = String.IsNullOrEmpty(Params["retry"].ToString()) == true ? "" : Params["retry"].ToString();
            response_code = String.IsNullOrEmpty(Params["response_code"].ToString()) == true ? "" : Params["response_code"].ToString();
            billing_notes = String.IsNullOrEmpty(Params["billing_notes"].ToString()) == true ? "" : Params["billing_notes"].ToString();
            trans_date = String.IsNullOrEmpty(Params["trans_date"].ToString()) == true ? "" : Params["trans_date"].ToString();
            bin_country = String.IsNullOrEmpty(Params["bin_country"].ToString()) == true ? "" : Params["bin_country"].ToString();

            if (Params["token_eligibility"] != null)
            {
                token_eligibility = String.IsNullOrEmpty(Params["token_eligibility"].ToString()) == true ? "" : Params["token_eligibility"].ToString();
            }
            else
            {
                token_eligibility = "";
            }

            DataTable dt = new DataTable();
            dt = hdfc.HDFCResponse(order_id, tracking_id, bank_ref_no, order_status, failure_message, payment_mode, card_name, status_code, status_message,
                currency, amount, billing_name, billing_address, billing_city, billing_state, billing_zip, billing_country, billing_tel, billing_email, delivery_name,
                delivery_address, delivery_city, delivery_state, delivery_zip, delivery_country, delivery_tel, merchant_param1, merchant_param2, merchant_param3,
                merchant_param4, merchant_param5, vault, offer_type, offer_code, discount_value, mer_amount, eci_value, retry, response_code, billing_notes, trans_date, bin_country, token_eligibility, encResponse);
            string orderId = "";
            string requestId = "";
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToDecimal(dt.Rows[0]["p_req_amount"].ToString()) != Convert.ToDecimal(amount))
                {
                    Session["_ErrorMsg"] = "Request and Response Amount Not Same";
                    Response.Redirect("../errorpage.aspx");
                    return;
                }
                orderId = dt.Rows[0]["orderid"].ToString();
                requestId = dt.Rows[0]["request_id"].ToString();

                if (order_status.ToUpper() == "SUCCESS")
                {
                    SuccessPageRedirect(orderId, requestId);
                }
                //else if (order_status.ToUpper() == "AWAITED")
                //{
                //    DataTable mytable = new DataTable();
                //    string MfinalStatus = "";
                //    mytable = hdfc.dblstartday(DateTime.Now.ToString("dd/MM/yyyy"), 4, 1, "", "P");
                //    int MDBLVERIFICATIONLOGID = Convert.ToInt16(mytable.Rows[0]["pDBLVERIFICATIONLOGID"].ToString());
                //    if (mytable.Rows.Count > 0)
                //    {
                //        MfinalStatus = hdfc.HDFCStatusAPI(dt.Rows[0]["orderId"].ToString(), MDBLVERIFICATIONLOGID, "P");
                //        if (MfinalStatus.ToUpper() == "SUCCESS")
                //        {
                //            SuccessPageRedirect(orderId, requestId);
                //        }
                //        else
                //        {
                //            FailedPageRedirect(orderId, requestId, failure_message);
                //        }
                //    }
                //    else
                //    {
                //        Session["_ErrorMsg"] = "order_status does not SUCCESS";
                //        Response.Redirect("../errorpage.aspx");
                //    }
                //}
                else
                {
                    FailedPageRedirect(orderId, requestId, failure_message);
                }
            }
            else
            {
                Session["_ErrorMsg"] = "Table has zero row.";
                Response.Redirect("../errorpage.aspx");
            }

        }
        catch (Exception ex)
        {
            Session["_ErrorMsg"] = ex.Message.ToString();
            Response.Redirect("../errorpage.aspx");
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
                    // Session["_RNDIDENTIFIERCTZAUTHC"] = _SecurityCheck.GeneratePassword(10, 5);
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
                    Response.Redirect("../traveller/walletTopupStatus.aspx", false);
                }
                else if (requestId == "111")
                {
                    Session["TicketNumber"] = orderId;
                    Response.Redirect("../pathikwebpage/ticketstatus.aspx?tk="+ orderId, false);
                }
                else if (requestId == "1111")
                {

                    Session["_UserCode"] = billing_tel.ToString();
                    Session["AMOUNT"] = amount.ToString();
                    Session["TxnReferenceNumber"] = orderId ;
                    Session["STATUS"] = "S";
                    Response.Redirect("../pathikwebpage/walletTopupStatus.aspx?amount="+ amount.ToString(), false);
                }
                //else if (requestId == "2")
                //{
                //    Session["_UserRoleCode"] = dtUser.Rows[0]["userRoleCode"].ToString();
                //    string UserCode = dtUser.Rows[0]["userCode"].ToString();
                //    Session["_UserCode"] = UserCode;
                //    Session["_UserName"] = dtUser.Rows[0]["userName"].ToString();
                //    Session["MobileNo"] = dtUser.Rows[0]["userMobile"].ToString();
                //    Session["Emailid"] = dtUser.Rows[0]["userEmail"].ToString();
                //    Session["_LLoginDTTime"] = DateTime.Now.ToString();
                //    Session["transrefno"] = orderId;
                //    Session["_acctStatus"] = "A";
                //    CreateCookie(Session["_UserCode"].ToString());
                //    Session["_RNDIDENTIFIERAGENTAUTH"] = _SecurityCheck.GeneratePassword(10, 5);

                //    if (UserCode.Length > 0)
                //    {
                //        Dictionary<string, string> d = (Dictionary<string, string>)Application["_utcUL"];
                //        if (d != null)
                //        {
                //            d.Remove(UserCode);
                //        }
                //        d.Add(UserCode, HttpContext.Current.Session.SessionID);
                //    }

                //    Response.Redirect("../CitizenAuth/AGAuthPrintReceipt.aspx", false);
                //}
                else if (requestId == "3")
                {
                    Session["Passno"] = dtUser.Rows[0]["p_passno"].ToString();
                    Session["currtranrefno"] = orderId;
                    Session["IssuanceType"] = dtUser.Rows[0]["p_issuetype"].ToString();
                    Response.Redirect("../BusPass/PassStatus.aspx",false);
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
            if (requestId == "1" || requestId == "11")
            {
                DataTable dtUser = hdfc.getUserDetails_after_PG(orderId, requestId);
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

            //else if (requestId == "2")
            //{
            //    classBooking obj = new classBooking();
            //    DataTable dtUser = obj.getUserDetails_after_PG(orderId, requestId);
            //    if (dtUser.Rows.Count > 0)
            //    {
            //        Session["_UserRoleCode"] = dtUser.Rows[0]["userRoleCode"].ToString();
            //        string UserCode = dtUser.Rows[0]["userCode"].ToString();
            //        Session["_UserCode"] = UserCode;
            //        Session["_UserName"] = dtUser.Rows[0]["userName"].ToString();
            //        Session["MobileNo"] = dtUser.Rows[0]["userMobile"].ToString();
            //        Session["Emailid"] = dtUser.Rows[0]["userEmail"].ToString();
            //        Session["_LLoginDTTime"] = DateTime.Now.ToString();
            //        CreateCookie(Session["_UserCode"].ToString());
            //        Session["_RNDIDENTIFIERAGENTAUTH"] = _SecurityCheck.GeneratePassword(10, 5);
            //        Session["_ErrorMsg"] = failure_message;
            //        if (UserCode.Length > 0)
            //        {
            //            Dictionary<string, string> d = (Dictionary<string, string>)Application["_utcUL"];
            //            if (d != null)
            //            {
            //                d.Remove(UserCode);
            //            }
            //            d.Add(UserCode, HttpContext.Current.Session.SessionID);
            //        }

            //        Response.Redirect("../CitizenAuth/AGPmtError.aspx", false);
            //    }
            //    else
            //    {
            //        Session["_ErrorMsg"] = "You have wrong request ID";
            //        Response.Redirect("../errorpage.aspx");
            //    }
            //}
            //else if (requestId == "3")
            //{
            //    Session["_ErrorMsg"] = failure_message;
            //    Response.Redirect("../PassUTC/PassPmtError.aspx");
            //}
            //else
            //{
            //    Session["_ErrorMsg"] = "You have wrong request ID";
            //    Response.Redirect("../errorpage.aspx");
            //}
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


}