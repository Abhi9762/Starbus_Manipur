using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class pathikwebpage_agent_pgRedirection : System.Web.UI.Page
{
    public classTokenCounterEtm SoapHeader = new classTokenCounterEtm();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            string token = Request.QueryString["token"] == null/* TODO Change to default(_) if this is not a reference type */? "" : Request.QueryString["token"];
            SoapHeader.UserAuthenticationToken = token;
            if (SoapHeader == null)
            {
                Session["errorMessage"] = "Invalid Request (Token). If you face this problem again and again feel free to helpdesk.";
                Response.Redirect("error.aspx", false);
                return;
            }
            if (!(SoapHeader.IsUserCredentialsValid(SoapHeader)))
            {
                Session["errorMessage"] = "Invalid Request (Token). If you face this problem again and again feel free to helpdesk.";
                Response.Redirect("error.aspx", false);
                return;
            }

            string pgId = Request.QueryString["pg"] == null/* TODO Change to default(_) if this is not a reference type */? "" : Request.QueryString["pg"];
            if (pgId.Length <= 0)
            {
                Session["errorMessage"] = "Invalid Payment Gateway. Please try again. If you face this problem again and again feel free to helpdesk.";
                Response.Redirect("error.aspx", false);
                return;
            }

            string user = Request.QueryString["us"] == null/* TODO Change to default(_) if this is not a reference type */? "" : Request.QueryString["us"];
            if (user.Length < 5)
            {
                Session["errorMessage"] = "Invalid User. If you face this problem again and again feel free to helpdesk.";
                Response.Redirect("error.aspx", false);
                return;
            }
            
            HDFC obj = new HDFC();
            DataTable dt = new DataTable();
            dt = obj.get_PG_requestURL(Convert.ToInt32(pgId));
            if (dt.Rows.Count <= 0)
            {
                Session["errorMessage"] = "Payment Gateway details are not available for this ticket. Please try again. If you face this problem again and again feel free to helpdesk.";
                Response.Redirect("error.aspx", false);
                return;
            }
            string mobile = Request.QueryString["mb"] == null/* TODO Change to default(_) if this is not a reference type */? "" : Request.QueryString["mb"];

            string email = Request.QueryString["em"] == null/* TODO Change to default(_) if this is not a reference type */? "" : Request.QueryString["em"];

            string trxType = Request.QueryString["ty"] == null/* TODO Change to default(_) if this is not a reference type */? "" : Request.QueryString["ty"];
            if (trxType == "W")
            {
                string ticket = Request.QueryString["tk"] == null/* TODO Change to default(_) if this is not a reference type */? "" : Request.QueryString["tk"];

                if (ticket.Length < 12)
                {
                    Session["errorMessage"]= "Invalid Refrence Number. If you face this problem again and again feel free to helpdesk.";
                    Response.Redirect("error.aspx", false);
                    return;
                }
                classWalletAgent objWallet = new classWalletAgent();
                DataTable dtWlt = objWallet.gettopupTransactions_status( user, ticket);
                if (dtWlt.Rows.Count <= 0)
                {
                   
                    Session["errorMessage"] = "Payment Gateway details are not available. Please try again. If you face this problem again and again feel free to helpdesk.";
                    Response.Redirect("error.aspx", false);
                    return;
                }
                int amount;
                if (int.TryParse(dtWlt.Rows[0]["txnamt"].ToString(), out amount) == false)
                {
                    Session["errorMessage"] = "Invalid Amount for Wallet Topup. Please try again. If you face this problem again and again feel free to helpdesk.";
                    Response.Redirect("error.aspx", false);
                    return;
                }
                if (amount < 100)
                {
                   
                    Session["errorMessage"] = "Invalid Amount for Wallet Topup. Please try again. If you face this problem again and again feel free to helpdesk.";
                    Response.Redirect("error.aspx", false);
                    return;
                }
                Session["transrefno"] = ticket;
                Session["depositamt"] = amount;
                Session["_UserCode"] = user;
                Session["MobileNo"] = mobile;
                Session["Emailid"] = email;
                string resposeurl = dt.Rows[0]["request_url"].ToString();
                byte[] base64str = System.Text.Encoding.UTF8.GetBytes("222");
                Response.Redirect("../PG/" + resposeurl + "?RequestId=" + System.Convert.ToBase64String(base64str));
            }
            else
            {
                Session["errorMessage"] = "Invalid Transaction Type. If you face this problem again and again feel free to helpdesk.";
                Response.Redirect("error.aspx", false);
            }
        
    }

}
}