using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Npgsql;

public partial class traveller_wallet : BasePage 
{
    NpgsqlCommand MyCommand;
    sbBLL bll = new sbBLL();
    sbValidation _SecurityCheck = new sbValidation();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    private sbCommonFunc _common = new sbCommonFunc();
    HDFC hdfc = new HDFC();
    wsClass obj = new wsClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        checkUser();
        if (!IsPostBack)
        {
            Random randomclass = new Random();
            Session["rndNoCheck"] = randomclass.Next().ToString();
            hidtoken.Value = Session["rndNoCheck"].ToString();
            Session["_moduleName"] = "Wallet";
            onload();
            loadPG();
        }
    }

    #region "Methods"
    private void checkUser()
    {
        if (_security.isSessionExist(Session["_UserCode"]) == true)
        {
            Session["_UserCode"] = Session["_UserCode"];
        }
        else
        {
            Response.Redirect("errorpage.aspx");
        }
    }
    private void CheckTokan()
    {
        if (Session["rndNoCheck"] == null || Session["rndNoCheck"].ToString() == "")
        {
            Response.Redirect("../errorpage.aspx");
        }

        if (Session["rndNoCheck"].ToString() != hidtoken.Value.ToString())
        {
            Response.Redirect("../errorpage.aspx");
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
            _common.ErrorLog("wallet.aspx-0001", ex.Message);
        }
    }
    public void onload()
    {
        try
        {
            if (_security.isSessionExist(Session["_UserCode"]) == true)
            {
                loadWallet(Session["_UserCode"].ToString());
                loadTxn(Session["_UserCode"].ToString());
                loadTopupTxn(Session["_UserCode"].ToString());
            }
            else
            {
                Response.Redirect("errorpage.aspx");
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.ToString());
            _common.ErrorLog("wallet.aspx-0002", ex.Message);
        }
    }
    private void Errormsg(string msg)
    {
        string popup = _popup.modalPopupSmall("W", "Information", msg, "Close");
        Response.Write(popup);
    }
    private void loadWallet(string userId)//M1
    {
        try
        {
            DataTable dt_wallet = new DataTable();
            dt_wallet = obj.getWalletDetail_dt(userId);
            if (dt_wallet.Rows.Count > 0)
            {
                lblWalletBalance.Text = dt_wallet.Rows[0]["amount"].ToString();
                lblWalletLastUpdate.Text = dt_wallet.Rows[0]["lastupdate"].ToString();
                lblWalletOpenDate.Text = dt_wallet.Rows[0]["opendate"].ToString();
            }
            else
            {
                lblWalletBalance.Text = "NA";
                lblWalletLastUpdate.Text = "NA (Please refresh page)";
                lblWalletOpenDate.Text = "NA";
            }

        }
        catch (Exception ex)
        {
            _common.ErrorLog("wallet.aspx-0003", ex.Message);
        }
    }
    private void loadTxn(string userId)//M2
    {
        try
        {
            DataTable dt_walletTxn = new DataTable();
            dt_walletTxn = obj.getWalletTransactions_dt(userId, "10", "L");


            if (dt_walletTxn.Rows.Count > 0)
            {
                gvTransactions.DataSource = dt_walletTxn;
                gvTransactions.DataBind();

                lblNoTransactionMsg.Text = "";
                pnlNoTransaction.Visible = false;
            }
            else
            {
                lblNoTransactionMsg.Text = "We are waiting for your first transaction";
                pnlNoTransaction.Visible = true;
            }

        }
        catch (Exception ex)
        {
            lblNoTransactionMsg.Text = "Oops! Something happened with your ticket loading process.<br> Please feel free to contact the helpdesk";
            pnlNoTransaction.Visible = true;
            _common.ErrorLog("wallet.aspx-0004", ex.Message);
        }
    }
    private void loadTopupTxn(string userId)//M2
    {
        try
        {
           
            DataTable dt_walletTxn = new DataTable();
            dt_walletTxn = obj.walletTopup_txn(userId);
            if (dt_walletTxn.Rows.Count > 0)
            {
                grdTopupTransaction.DataSource = dt_walletTxn;
                grdTopupTransaction.DataBind();

                lbltopup.Text = "";
                pnltopup.Visible = false;
            }
            else
            {
                lbltopup.Text = "We are waiting for your first transaction";
                pnltopup.Visible = true;
            }

        }
        catch (Exception ex)
        {
            lbltopup.Text = "Oops! Something happened with your ticket loading process.<br> Please feel free to contact the helpdesk";
            pnltopup.Visible = true;
            _common.ErrorLog("wallet.aspx-0005", ex.Message);
        }
    }

    
    #endregion

    #region "Events"
    protected void rptrPG_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        CheckTokan();
        if (e.CommandName == "PAYNOW")
        {
            string amount = tbAmount.Text.Trim();
            if (_SecurityCheck.IsValidInteger(amount, 1, 5) == false)
            {
                Errormsg("Enter valid Amount");
                return;
            }
            DataTable dttt = new DataTable();
            string userId = Session["_UserCode"].ToString();
            dttt = obj.walletTopup_start_completed("0", userId, amount, "F");
            if (dttt.Rows[0]["p_rslt"].ToString()!= "EXCEPTION")
            {
                Session["transrefno"] = dttt.Rows[0]["p_rslt"].ToString();
                Session["depositamt"] = amount;

                HiddenField rptHdPGId = e.Item.FindControl("rptHdPGId") as HiddenField;
                hdpgid.Value = rptHdPGId.Value.ToString();
                lblmsg.Text = "You have initiated a topup request of Rs <b>" + Session["depositamt"].ToString() + "</b>.<br/> Please note this Reference number for this transaction is <b>" + Session["transrefno"].ToString() + "</b> for all future needs";
                mpconfirmation.Show();
                HiddenField REQURL = e.Item.FindControl("hd_pgurl") as HiddenField;
                hdpgurl.Value = REQURL.Value.ToString();
            }
            else
            {
                Errormsg("Something went wrong. Try again. If you get this again and again feel free to contact helpdesk.");
                return;
            }
        }
        else
        {
            Errormsg("Something went wrong with Payment Gateway redirection. Please contact to helpdesk.");
        }
    }
    protected void lbtnyes_Click(object sender, EventArgs e)
    {
        CheckTokan();
        string userId = Session["_UserCode"].ToString();
        string resposeurl = hdpgurl.Value.ToString();
        Session["_RNDIDENTIFIERSTRVL"] = _security.GeneratePassword(10, 5);
        byte[] base64str = System.Text.Encoding.UTF8.GetBytes("11");
        Response.Redirect("../PG/" + resposeurl + "?RequestId=" + System.Convert.ToBase64String(base64str));
    }
    protected void grdTopupTransaction_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdTopupTransaction.PageIndex = e.NewPageIndex;
        loadTopupTxn(Session["_UserCode"].ToString());
    }
    protected void gvTransactions_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTransactions.PageIndex = e.NewPageIndex;
        loadTxn(Session["_UserCode"].ToString());
    }
    #endregion






}