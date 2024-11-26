using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_MainCscWallet : System.Web.UI.Page
{
    NpgsqlCommand MyCommand; 
     sbBLL bll = new sbBLL();
    sbValidation _SecurityCheck = new sbValidation();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    private sbCommonFunc _common = new sbCommonFunc();
    HDFC hdfc = new HDFC();
    private DataTable dt;
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["moduleName"] = "Common Service Centre - Wallet";
        if (!IsPostBack)
        {
            getmaxlimit();
            loadPG();
            LoadWallet(Session["_UserCode"].ToString());
            loadTopupTxn(Session["_UserCode"].ToString());
        }
    }


    private void LoadWallet(string userId)
    {
        try
        {
            classWalletAgent obj = new classWalletAgent();
            DataTable dtWallet = obj.GetWalletDetailDt(userId);
            if (dtWallet.Rows.Count > 0)
            {
                lblWalletBalance.Text = "Your Wallet Balance <b>" + " ₹" + dtWallet.Rows[0]["currentbalanceamount"].ToString() + "</b>";
                if (dtWallet.Rows[0]["val_amount"] == "0")
                {
                    lblWalletLastUpdate.Text = "( Recharge your wallet first time )";
                }
                else
                {
                    lblWalletLastUpdate.Text = "( Last Wallet Recharge " + dtWallet.Rows[0]["d_date"].ToString() + " with ₹" + dtWallet.Rows[0]["val_amount"].ToString();
                }
            }
            else
            {
                lblWalletBalance.Text = "NA";
                lblWalletLastUpdate.Text = "NA (Please refresh page)";
            }
        }
        catch (Exception ex)
        {

        }
    }

    private void loadPG()
    {
        try
        {
            lblNoPG.Visible = true;
            rptrPG.Visible = false;
            DataTable dt_pg = new DataTable();
            dt_pg = hdfc.get_PG();

            if (dt_pg.Rows.Count > 0)
            {
                rptrPG.DataSource = dt_pg;
                rptrPG.DataBind();
                lblNoPG.Visible = false;
                rptrPG.Visible = true;
            }
        }
        catch (Exception ex)
        {
            lblNoPG.Visible = true;
            rptrPG.Visible = false;
        }
    }
    private void getmaxlimit()
    {
        try
        {

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "agent_wallet.getagentmaxlimit");
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                Session["Maxlimit"] = dt.Rows[0]["agent_topup_limit"].ToString();
                lblmaxlimit.Text = "You can deposit <b> ₹ " + dt.Rows[0]["agent_topup_limit"].ToString() + " </b> with a single transaction <br/>To proceed with account recharge please select Payment Gateway";
            }
            else
            {
                lblmaxlimit.Text = "The agent deposit limit is not set. Please contact the helpdesk.";
            }
        }
        catch (Exception ex)
        {
            lblmaxlimit.Text = "The agent deposit limit is not set. Please contact the helpdesk.";
        }
    }
    private void Errormsg(string header, string message)
    {
        LabelModalErrorHeader.Text = header;
        LabelModalErrorMessage.Text = message;
        ModalPopupError.Show();
    }
    private void SuccessMesgBox(string message)
    {
        lblsuccessmsg.Text = message;
        mpsuccess.Show();
    }
    protected void rptrPG_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "PAYNOW")
        {
            try
            {
                if (Session["Maxlimit"] == null)
                {
                    Response.Redirect("errorpage.aspx", true);
                }

                string amount = txtAmount.Text.Trim();
                if (!_SecurityCheck.IsValidInteger(amount, 1, 5))
                {
                    Errormsg("Please Check", "Enter valid Amount");
                    return;
                }

                if (int.Parse(Session["Maxlimit"].ToString()) < int.Parse(amount))
                {
                    Errormsg("Please Check", "Entered amount cannot be greater than deposit limit. Please enter a valid amount.");
                    return;
                }

                string userID = Session["_UserCode"].ToString();
                classWalletAgent obj = new classWalletAgent();
                DataTable dt = obj.walletTopup_start_completed("0", userID, amount, "F");
                if (dt.Rows.Count <= 0)
                {
                    Errormsg("Please Check", "Something went wrong. Try again. If you get this again and again feel free to contact helpdesk.1");
                    return;
                }

                Session["transrefno"] = dt.Rows[0]["spstatus"].ToString();
                Session["depositamt"] = amount;
                HiddenField rptHdPGId = (HiddenField)e.Item.FindControl("rptHdPGId");
                hdpgid.Value = rptHdPGId.Value.ToString();

                lblTopupRequest.Text = "You have initiated a topup request of Rs <b>" + Session["depositamt"].ToString() + "</b>.<br/> Please note this Reference number for this transaction <br/> is <b>" + Session["transrefno"].ToString() + "</b> for all future needs.";

                MpPaymentConfirm.Show();
                HiddenField REQURL = e.Item.FindControl("hd_pgurl") as HiddenField;
                hdpgurl.Value = REQURL.Value.ToString();
            }
            catch (Exception ex)
            {
                // Handle the exception here
            }
        }
    }


    protected void lbtnyes_Click(object sender, EventArgs e)
    {
        
    }


    private void loadTopupTxn(string userID)
    {
        try
        {
            classWalletAgent obj = new classWalletAgent();
            DataTable dtTopupTxn = new DataTable();
            gvtopuptransaction.Visible = false;
            grdmsg1.Visible = true;
            dtTopupTxn = obj.gettopupTransactions_dt(userID);

            if (dtTopupTxn.Rows.Count > 0)
            {
                gvtopuptransaction.DataSource = dtTopupTxn;
                gvtopuptransaction.DataBind();
                gvtopuptransaction.Visible = true;
                grdmsg1.Visible = false;
            }
            else
            {
                gvtopuptransaction.Visible = false;
                grdmsg1.Visible = true;
            }
        }
        catch (Exception ex)
        {
            gvtopuptransaction.Visible = false;
            
        }
    }

    protected void gvtopuptransaction_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvtopuptransaction.PageIndex = e.NewPageIndex;
        loadTopupTxn(Session["_UserCode"].ToString());

    }

    protected void gvtopuptransaction_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void lbtnPaymentYes_Click(object sender, EventArgs e)
    {
        string userId = Session["_UserCode"].ToString();
        string resposeurl = hdpgurl.Value.ToString();
        Session["_RNDIDENTIFIERCTZAUTHC"] = _SecurityCheck.GeneratePassword(10, 5);
        byte[] base64str = System.Text.Encoding.UTF8.GetBytes("2");
        Response.Redirect("../PG/" + resposeurl + "?RequestId=" + System.Convert.ToBase64String(base64str));
    }
}