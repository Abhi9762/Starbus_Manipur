using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_AgentRechrgeTopup : System.Web.UI.Page
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
        Session["agmodule"] = "Agent Recharge Account";
        Session["HDAgname"] = Session["_UserName"] + " ( " + Session["_UserCode"] + " )";

        if (!IsPostBack)
        {
            if (Session["_UserCode"] == null)
            {
                Response.Redirect("errorpage.aspx", true);
            }

            string userID = Session["_UserCode"].ToString();
            loadWallet(userID);
            loadPG();
            loadTxn(userID);
            loadTopupTxn(userID);
            getmaxlimit();

            txtDateF.Text = DateTime.Now.Date.AddDays(-29).ToString("dd/MM/yyyy");
            txtDateT.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            Session["DateF"] = txtDateF.Text;
            Session["DateT"] = txtDateT.Text;
            agpassbook();
        }

    }

    private void agpassbook()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "agent_wallet.agentpassbook");
            MyCommand.Parameters.AddWithValue("@sp_agentcode", Session["_UserCode"]);
            MyCommand.Parameters.AddWithValue("@sp_datefrom", Session["DateF"]);
            MyCommand.Parameters.AddWithValue("@sp_dateto", Session["DateT"]);
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                grdagpassbook.DataSource = dt;
                grdagpassbook.DataBind();
                pnlNoPassbook.Visible = false;
            }


        }
        catch (Exception ex)
        {
            pnlNoPassbook.Visible = true;
        }
    }

    private void loadWallet(string userId)
    {
        try
        {
            classWalletAgent obj = new classWalletAgent();
            DataTable dtWallet = obj.GetWalletDetailDt(userId);
            if (dtWallet.Rows.Count > 0)
            {
                lblWalletBalance.Text = "Your Wallet Balance <b>" + dtWallet.Rows[0]["currentbalanceamount"].ToString() + "</b>";
                if ((int)dtWallet.Rows[0]["val_amount"] == 0)
                {
                    lblWalletLastUpdate.Text = "( Recharge your wallet first time )";
                }
                else
                {
                    lblWalletLastUpdate.Text = "( Last Wallet Recharge " + dtWallet.Rows[0]["d_date"].ToString() + " with ₹" + dtWallet.Rows[0]["val_amount"].ToString() + " <i class='fa fa-rupee-sign'></i> )";
                }
            }
            else
            {
                lblWalletBalance.Text = "NA";
                lblWalletLastUpdate.Text = "NA (Please refresh page)";
            }
        }
        catch (Exception)
        {
            // Handle the exception here if needed
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

    private void loadTxn(string userId)
    {
        try
        {
            classWalletAgent obj = new classWalletAgent();
            DataTable dtWalletTxn = new DataTable();
            dtWalletTxn = obj.getWalletTransactions_dt(userId, "100", "L");

            if (dtWalletTxn.Rows.Count > 0)
            {
                gvTransactions.DataSource = dtWalletTxn;
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
            lblNoTransactionMsg.Text = "Oops! Something happened with your ticket loading process.<br>Please feel free to contact the helpdesk.";
            pnlNoTransaction.Visible = true;
        }
    }

    private void loadTopupTxn(string userID)
    {
        try
        {
            classWalletAgent obj = new classWalletAgent();
            DataTable dtTopupTxn = new DataTable();
            gvtopuptransaction.Visible = false;
            pnltopuptransaction.Visible = true;
            dtTopupTxn = obj.gettopupTransactions_dt(userID);

            if (dtTopupTxn.Rows.Count > 0)
            {
                gvtopuptransaction.DataSource = dtTopupTxn;
                gvtopuptransaction.DataBind();
                gvtopuptransaction.Visible = true;
                pnltopuptransaction.Visible = false;
            }
            else
            {
                gvtopuptransaction.Visible = true;
                pnltopuptransaction.Visible = false;
            }
        }
        catch (Exception ex)
        {
            gvtopuptransaction.Visible = true;
            pnltopuptransaction.Visible = false;
        }
    }




    protected void grdagpassbook_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdagpassbook.PageIndex = e.NewPageIndex;
        agpassbook();
    }

    protected void lbtnshow_Click(object sender, EventArgs e)
    {
        grdagpassbook.Visible = false;

        //if (!ValidValue())
        //{
        //    return;
        //}

        Session["DateF"] = txtDateF.Text;
        Session["DateT"] = txtDateT.Text;
        agpassbook();

    }

    //private bool ValidValue()
    //{
    //    if (DateTime.Parse(txtDateF.Text.Trim()) > DateTime.Now.Date)
    //    {
    //        Alert("Please select a date less or equal to the From date");
    //        return false;
    //    }

    //    if (DateTime.Parse(txtDateT.Text.Trim()) > DateTime.Now.Date)
    //    {
    //        Alert("Please select a date less or equal to the To date");
    //        return false;
    //    }

    //    if (!_validation.IsValidString(txtDateF.Text.Trim(), 8, 10))
    //    {
    //        Alert("Please Enter correct From Date");
    //        return false;
    //    }
    //    //else if (!DateTime.TryParse(txtDateF.Text, out _))
    //    //{
    //    //    Alert("Please Enter correct From Date");
    //    //    return false;
    //    //}

    //    if (!_validation.IsValidString(txtDateT.Text.Trim(), 8, 10))
    //    {
    //        Alert("Please Enter correct To Date");
    //        return false;
    //    }
    //    //else if (!DateTime.TryParse(txtDateT.Text, out _))
    //    //{
    //    //    Alert("Please Enter correct To Date");
    //    //    return false;
    //    //}

    //    DateTime dtFrom = DateTime.ParseExact(txtDateF.Text.Trim(), "dd/MM/yyyy", null);
    //    DateTime dtTo = DateTime.ParseExact(txtDateT.Text.Trim(), "dd/MM/yyyy", null);

    //    if ((dtTo - dtFrom).TotalDays >= 30)
    //    {
    //        Alert("Please Note :- At a time, Account of only 30 days can be generated");
    //        return false;
    //    }

    //    if (dtTo < dtFrom)
    //    {
    //        Alert("Please To date greater or equal to From date");
    //        return false;
    //    }

    //    return true;
    //}

    private void Alert(string message)
    {
        // Implement your alert mechanism here
        // You can use MessageBox.Show() for Windows Forms or any other method
        // to display the alert message to the user.
    }


    protected void grdagpassbook_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string trnsdate;
        decimal openblnc;
        decimal trnsamt;
        decimal depositamt;
        decimal closeblnc;
        int index = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "View")
        {
            Label lDate = grdagpassbook.Rows[index].FindControl("lblagtransdate") as Label;
            trnsdate = lDate.Text;

            DateTime ddd;

            if (DateTime.TryParseExact(trnsdate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out ddd))
            {

                openblnc = Convert.ToDecimal(grdagpassbook.DataKeys[index].Values["OPENINGBALANCE"]);
                trnsamt = Convert.ToDecimal(grdagpassbook.DataKeys[index].Values["TRANSACTIONAMOUNT"]);
                depositamt = Convert.ToDecimal(grdagpassbook.DataKeys[index].Values["DEPOSITAMOUNT"]);
                closeblnc = Convert.ToDecimal(grdagpassbook.DataKeys[index].Values["CLOSINGBALANCE"]);

                int myYear = ddd.Year;
                int myMonth = ddd.Month;

                Session["Month"] = ddd.ToString("MM");
                Session["Year"] = ddd.Year;
                Session["Date"] = trnsdate;
                Response.Redirect("Agentcashregister.aspx");
            }
            else
            {

                Response.Write("Invalid date format.");
            }
        }
    }


    private void Errormsg(string header, string message)
    {
        LabelModalErrorHeader.Text = header;
        LabelModalErrorMessage.Text = message;
        ModalPopupError.Show();
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
                lblmaxlimit.Text = "You can deposit <b> ₹ " + dt.Rows[0]["agent_topup_limit"].ToString() + " </b> with a single transaction.";
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

                string amount = tbAmount.Text.Trim();                        
                if (!_SecurityCheck.IsValidInteger(amount, 1, 5))
                {
                    Errormsg("Please Check", "Enter Valid Amount<br>(Minimum amount ₹ 100, Maximum amount ₹ " + Session["Maxlimit"].ToString() + ")");
                    return;
                }

                if (int.Parse(Session["Maxlimit"].ToString()) < int.Parse(amount) || int.Parse(amount) < 100)
                {
                    Errormsg("Please Check", "Enter Valid Amount<br>(Minimum amount ₹ 100, Maximum amount ₹ " + Session["Maxlimit"].ToString() + ")");
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
                lblmsg.Text = "You have initiated a top-up request of Rs <b>" + Session["depositamt"].ToString() + "</b>.<br/> Please note this Reference number for this transaction is <b>" + Session["transrefno"].ToString() + "</b> for all future needs.";
                mpconfirmation.Show();
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
        string userId = Session["_UserCode"].ToString();
        string resposeurl = hdpgurl.Value.ToString();
        Session["_RNDIDENTIFIERCTZAUTHC"] = _SecurityCheck.GeneratePassword(10, 5);
        byte[] base64str = System.Text.Encoding.UTF8.GetBytes("2");
        Response.Redirect("../PG/" + resposeurl + "?RequestId=" + System.Convert.ToBase64String(base64str));
    }

    protected void gvtopuptransaction_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvtopuptransaction.PageIndex = e.NewPageIndex;
        loadTopupTxn(Session["_UserCode"].ToString());
    }

    protected void gvTransactions_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTransactions.PageIndex = e.NewPageIndex;
        loadTxn(Session["_UserCode"].ToString());
    }
}

