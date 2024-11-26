using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PG_pgRedirection : System.Web.UI.Page
{
    wsClass objBooking = new wsClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            AesEncrptDecrpt aes = new AesEncrptDecrpt();
            


            string token = Request.QueryString["token"] == null/* TODO Change to default(_) if this is not a reference type */? "" : Request.QueryString["token"];
            if (token != "8A5CE72749C4FEAAB9D5D1EBD34735D0FD32442F")
            {
                Session["errorMessage"] = "Invalid Request (Token). If you face this problem again and again feel free to helpdesk.";
                Response.Redirect("error.aspx", false);
                return;
            }
           

            string pgId_e = Request.QueryString["pg"] == null/* TODO Change to default(_) if this is not a reference type */? "" : Request.QueryString["pg"];
            string pgId = aes.DecryptStringAES(pgId_e);
            if (pgId.Length <= 0)
            {
                Session["errorMessage"] = "Invalid Payment Gateway. Please try again. If you face this problem again and again feel free to helpdesk.";
                Response.Redirect("error.aspx", false);
                return;
            }

            string user_e = Request.QueryString["us"] == null/* TODO Change to default(_) if this is not a reference type */? "" : Request.QueryString["us"];
            string user = aes.DecryptStringAES(user_e);
            if (user.Length != 10)
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

            string trxType_e = Request.QueryString["BW"] == null/* TODO Change to default(_) if this is not a reference type */? "" : Request.QueryString["BW"];
            string trxType = aes.DecryptStringAES(trxType_e);
            if (trxType == "B")
            {
                string ticket_e = Request.QueryString["tk"] == null/* TODO Change to default(_) if this is not a reference type */? "" : Request.QueryString["tk"];
              string ticket = aes.DecryptStringAES(ticket_e);
                if (ticket.Length <= 12)
                {
                    Session["errorMessage"] = "Invalid Ticket. If you face this problem again and again feel free to helpdesk.";
                    Response.Redirect("error.aspx", false);
                    return;
                }

                GetFareDetails(ticket, user);
                string resposeurl = dt.Rows[0]["request_url"].ToString();
                byte[] base64str = System.Text.Encoding.UTF8.GetBytes("111");
                Response.Redirect("" + resposeurl + "?RequestId=" + System.Convert.ToBase64String(base64str));
            }
            else if (trxType == "W")
            {
                string ticket_e = Request.QueryString["tk"] == null/* TODO Change to default(_) if this is not a reference type */? "" : Request.QueryString["tk"];
               string ticket = aes.DecryptStringAES(ticket_e);

                if (ticket.Length < 12)
                {
                    Session["errorMessage"] = "Invalid Amount. If you face this problem again and again feel free to helpdesk.";
                    Response.Redirect("error.aspx", false);
                    return;
                }



                wsClass objWallet = new wsClass();
                DataTable dtWlt = objWallet.walletTopup_txn_status(ticket, user);


                if (dtWlt.Rows.Count <= 0)
                {
                    Session["errorMessage"] = "Payment Gateway details are not available. Please try again. If you face this problem again and again feel free to helpdesk.";
                    Response.Redirect("error.aspx", false);
                    return;
                }


                int  amount;//= int.Parse(dtWlt.Rows[0]["txnamount"].ToString());
                if(int.TryParse(dtWlt.Rows[0]["amount"].ToString(), out amount ) == false)
                {
                    Session["errorMessage"] = "Invalid Amount for Wallet Topup. Please try again. If you face this problem again and again feel free to helpdesk.";
                    Response.Redirect("error.aspx", false);
                }
                
                Session["transrefno"] = ticket;
                Session["depositamt"] = amount;
                Session["_UserCode"] = user;

                string resposeurl = dt.Rows[0]["request_url"].ToString();
                byte[] base64str = System.Text.Encoding.UTF8.GetBytes("1111");
                Response.Redirect("" + resposeurl + "?RequestId=" + System.Convert.ToBase64String(base64str));
            }
            else
            {
                Session["errorMessage"] = "Invalid Transaction Type. If you face this problem again and again feel free to helpdesk.";
                Response.Redirect("error.aspx", false);
            }
        }
    }
    protected void GetFareDetails(string strTicketNo, string user)
    {
        try
        {
            DataTable dt;
            dt = objBooking.getFareDetails_byTicket(strTicketNo);
            if (dt.Rows.Count > 0)
            {
                Session["strTicketNo"] = strTicketNo;
                Session["_UTCTicketFinalAmount"] = dt.Rows[0]["net_fare"];
                Session["_pmtCTZMobile"] = user;
                Session["_pmtCTZEMAIL"] = "";
            }
            else
            {
                Session["errorMessage"] = "Fare details are not available for this ticket. Please try again. If you face this problem again and again feel free to helpdesk.";
                Response.Redirect("error.aspx", false);
            }
        }
        catch (Exception ex)
        {
            Session["errorMessage"] = ex.ToString() + "Fare details are not available for this ticket. Please try again. If you face this problem again and again feel free to helpdesk.";
            Response.Redirect("error.aspx", false);
        }
    }
}