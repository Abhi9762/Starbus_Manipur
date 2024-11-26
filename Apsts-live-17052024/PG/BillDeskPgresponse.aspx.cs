using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PG_BillDeskPgresponse : System.Web.UI.Page
{
    BilldeskSha billdesk = new BilldeskSha();
    BILLDESK bill = new BILLDESK();
    HDFC hdfc = new HDFC();
    sbSecurity _security = new sbSecurity();
    string TxnAmount;
    protected void Page_Load(object sender, EventArgs e)
    {
         string Responsemsg = Request.Form["msg"];
        if (Responsemsg == null || Responsemsg == "")
        {
            Response.Redirect("../errorpage.aspx");
        }
        string[] param = Responsemsg.Split('|');
        string MerchantID = param[0].ToString().Trim();
        string OrderID = param[1].ToString().Trim();
        string TxnReferenceNo = param[2].ToString().Trim();
        string BankReferenceNo = param[3].ToString().Trim();
         TxnAmount = param[4].ToString().Trim();
        string BankID = param[5].ToString().Trim();
        string BankMerchantID = param[6].ToString().Trim();
        string TxnType = param[7].ToString().Trim();
        string CurrencyName = param[8].ToString().Trim();
        string ItemCode = param[9].ToString().Trim();
        string SecurityType = param[10].ToString().Trim();
        string SecurityID = param[11].ToString().Trim();
        string SecurityPassword = param[12].ToString().Trim();
        string TxnDate = param[13].ToString().Trim();
        string AuthStatus = param[14].ToString().Trim();
        string SettlementType = param[15].ToString().Trim();
        string ErrorStatus = param[23].ToString().Trim();
        string ErrorDescription = param[24].ToString().Trim();
        string CheckSum = param[25].ToString().Trim();
        int aa = param.Length;

        string checksumkey = "G3eAmyVkAzKp8jFq0fqPEqxF4agynvtJ";// "M4RjgVXBHUFV";

        string MstringForCheckSum = "";

        for (var i = 0; i <= param.Length - 3; i++)
        {
            MstringForCheckSum = MstringForCheckSum + param[i].ToString().Trim() + "|";

        }

        MstringForCheckSum = MstringForCheckSum + param[24];

        string MourCheckSum;
       
        MourCheckSum = billdesk.GetHMACSHA256(MstringForCheckSum, checksumkey);

        if (MourCheckSum.ToUpper() == CheckSum.ToUpper())
        {
            DataTable dt = new DataTable();
            dt = bill.BILLDESKResponse(MerchantID,
         OrderID, TxnReferenceNo, BankReferenceNo, TxnAmount, BankID, BankMerchantID, TxnType, CurrencyName, ItemCode, SecurityType,
         SecurityID, SecurityPassword, TxnDate, AuthStatus, SettlementType, ErrorStatus, ErrorDescription, CheckSum, Responsemsg, checksumkey, MstringForCheckSum);
            if (dt.Rows.Count>0 )
            {
                
                if (Convert.ToDecimal(dt.Rows[0]["amt"].ToString()) != Convert.ToDecimal(TxnAmount))
                {
                    Session["_ErrorMsg"] = "Request and Response Amount Not Same";
                    Response.Redirect("../errorpage.aspx");
                    return;
                }
                OrderID = dt.Rows[0]["order_no"].ToString();
                string requestId = dt.Rows[0]["request_no"].ToString();

                if ( AuthStatus == "0300")
                {
                    SuccessPageRedirect(OrderID, requestId);
                }
                else
                {
                    FailedPageRedirect(OrderID, requestId, ErrorDescription);
                }
            }
            else
            {
                Session["_ErrorMsg"] = "Table has zero row.";
                Response.Redirect("../errorpage.aspx");
            }
        }
        else
        {
            Session["errorMessage"] = "Transaction  rejected/not completed by Payment Gateway. Please try again. If any Amount deducted, please contact your Bank";

            Response.Redirect("Error.aspx");
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
                    Response.Redirect("../pathikwebpage/walletTopupStatus.aspx?amount="+TxnAmount, false);
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
		else if (requestId == "22")//Agent Security Amount Deposit
                {

                    Session["_UserCode"] = dtUser.Rows[0]["p_usercode"].ToString();
                    Session["_UserName"] = dtUser.Rows[0]["p_username"].ToString();
                    Session["MobileNo"] = dtUser.Rows[0]["p_usermobile"].ToString();
                    Session["Emailid"] = dtUser.Rows[0]["p_useremail"].ToString();
                    Response.Redirect("../OnlAgPaymentSuccess.aspx", false);
                }
else if (requestId == "222")//Agent Topup wallet From ETM App
                {
                    Response.RedirectPermanent("../pathikwebpage/agent_walletTopupStatus.aspx", false);
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
}