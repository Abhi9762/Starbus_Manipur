using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class traveller_walletTopupStatus : BasePage 
{
    sbValidation _SecurityCheck = new sbValidation();
    sbSecurity _security = new sbSecurity();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["_moduleName"] = "Wallet";
            if (_security.isSessionExist(Session["STATUS"]) == true)
            {
                if (Session["STATUS"].ToString() == "S")
                {
                    string amt = Request.QueryString["amount"].TrimStart('0');
                    lblStatus.Text = "Congratulations !";
                    lblStatusMsg.Text = "<h4> Your wallet has successfully been recharged with ₹ " + amt + "</h4>";

                    if (Session["walletUpdateMsg"].ToString().ToUpper() == "DONE")
                    {
                           
                        lblWalletStatusMsg.Text = "<h4> For more detail please visit <a href="+ "wallet.aspx" + " style="+"color:green"+">Wallet Section</a> </h4>";
                        CommonSMSnEmail sms = new CommonSMSnEmail();
                      //  sms.WallletSucessMsg(Session["strTicketNo"].ToString(), Session["_UserCode"].ToString(), "");
                    }
                    else
                    {
                        lblWalletStatusMsg.Text = "You wallet balance has not been updated";
                    }

                }
                else if (Session["STATUS"].ToString() == "P")
                {
                    lblStatus.Text = "Pending !";
                    lblStatusMsg.Text = "Your transaction seems pending.<br>Don't worry, if your money is deducted feel free to contact helpdesk.";
                }
                else if (Session["STATUS"].ToString() == "F")
                {
                    lblStatus.Text = "Fail !";
                    lblStatusMsg.Text = "Your transaction has been failed.<br>Don't worry, if your money is deducted feel free to contact helpdesk.";
                }
                else
                {
                    lblStatus.Text = "Pending !";
                    lblStatusMsg.Text = "Your transaction seems pending.<br>we did not get any response from bank side. Don't worry, if your money is deducted feel free to contact helpdesk.";
                }
            }
            else
            {
                Response.Redirect("errorpage.aspx");
            }
        }
    }
}