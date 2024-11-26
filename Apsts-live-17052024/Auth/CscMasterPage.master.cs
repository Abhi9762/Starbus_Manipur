using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

public partial class Auth_CscMasterPage : System.Web.UI.MasterPage
{
    sbSecurity _security = new sbSecurity();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    string current_date = DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("yyyy");


    string current_date30 = DateTime.Now.AddDays(-30).ToString("dd") + "/" + DateTime.Now.AddDays(-30).ToString("MM") + "/" + DateTime.Now.AddDays(-30).ToString("yyyy");

    protected void Page_Load(object sender, EventArgs e)
    {
       
        //lblHeaderText.Text = Session["moduleName"].ToString();
        if (!IsPostBack)
        {
            lblHeaderText.Text = Session["moduleName"].ToString();
            GetAgentDetails();
            LoadWallet(Session["_UserCode"].ToString());
            GetCSCSubAgent();
            txtmyaccfrom.Text = current_date30;
            txtmyaccto.Text = current_date;

            // Booking & Cancellation
            txtfromdate.Text = current_date30;
            txttodate.Text = current_date;

            // Sub CSC Daily Account
            txtDateF.Text = current_date30;
            txtDateT.Text = current_date;

        }
    }
    #region "Methods"
    private void GetAgentDetails()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "agent.fun_getagentdetails");
            MyCommand.Parameters.AddWithValue("spusercode", Session["_UserCode"].ToString());
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    lblcscname.Text = dt.Rows[0]["ag_name"].ToString()  + " (" + Session["_UserCode"] + ")";
                    lblcscaddress.Text = dt.Rows[0]["ag_address"].ToString();
                    lblcscmobile.Text = dt.Rows[0]["ag_mobileno"].ToString();
                    Session["MobileNo"] = dt.Rows[0]["ag_mobileno"].ToString();
                    lblcscemail.Text = dt.Rows[0]["ag_emailid"].ToString();
                    Session["Emailid"] = dt.Rows[0]["ag_emailid"].ToString();
                    // lblUsername.Text = dt.Rows[0]["AGENTNAME"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            // Handle the exception as needed
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
                lblWalletBalance.Text = "Current Wallet Balance <b>" + dtWallet.Rows[0]["currentbalanceamount"].ToString()  + "</b>";
               
               
            }
            else
            {
                lblWalletBalance.Text =   "NA";
            }
        }
        catch (Exception ex)
        {

        }
    }
    private void GetCSCSubAgent()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "csc.subagentdetails");
            MyCommand.Parameters.AddWithValue("p_mainagent", Session["_UserCode"].ToString());
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlSubAgent.DataSource = dt;
                    ddlSubAgent.DataTextField = "agent_name";
                    ddlSubAgent.DataValueField = "val_agent_code";
                    ddlSubAgent.DataBind();

                    ddlagent.DataSource = dt;
                    ddlagent.DataTextField = "agent_name";
                    ddlagent.DataValueField = "val_agent_code";
                    ddlagent.DataBind();
                }
            }
            ddlSubAgent.Items.Insert(0, "All");
            ddlSubAgent.Items[0].Value = "0";
            ddlSubAgent.SelectedIndex = 0;

            ddlagent.Items.Insert(0, "Select");
            ddlagent.Items[0].Value = "0";
            ddlagent.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlSubAgent.Items.Insert(0, "ALL");
            ddlSubAgent.Items[0].Value = "0";
            ddlSubAgent.SelectedIndex = 0;

            ddlagent.Items.Insert(0, "Select");
            ddlagent.Items[0].Value = "0";
            ddlagent.SelectedIndex = 0;
        }
    }
    private void Errormsg(string msg)
    {
        lblWarningMsg.Text = msg;
        MpPaymentConfirm.Show();
    }
    private bool ValidValue()
    {
        try
        {
            DateTime dtFrom;
            DateTime dtTo;

            if (!DateTime.TryParseExact(txtmyaccfrom.Text.Trim(), "dd/MM/yyyy", null, DateTimeStyles.None, out dtFrom))
            {
                Errormsg("Select Valid From Date");
                return false; // Return is not necessary; you can simply exit the method or continue processing.
            }
            if (!DateTime.TryParseExact(txtmyaccto.Text.Trim(), "dd/MM/yyyy", null, DateTimeStyles.None, out dtTo))
            {
                Errormsg("Select Valid To Date");
                return false;
            }

            if (dtTo < dtFrom)
            {
                Errormsg("Please Enter Valid From Date");
                return false;
            }
            if ((dtTo - dtFrom).TotalDays > 30)
            {
                Errormsg("Please Note:- Reports can only be generated for 30 days at a time.");
                return false;
            }

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    #endregion

    #region "Event"
    protected void lbtnDashboard_Click(object sender, EventArgs e)
    {
        Response.Redirect("SubCscMgmt.aspx");
    }
    protected void lbtnWallet_Click(object sender, EventArgs e)
    {
        Response.Redirect("MainCscWallet.aspx");
    }
    protected void lbtnCatalogue_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("SubCscMgmt.aspx");
    }
    protected void lbtnChangePwd_ServerClick(object sender, EventArgs e)
    {

        mpchnagePass.Show();
    }
    protected void lbtnlogout_ServerClick(object sender, EventArgs e)
    {
        mpConfirmationLogout.Show();
    }
   
    protected void lbtnCscAccount_Click(object sender, EventArgs e)
    {
        try
        {
            if (ValidValue() == false)
            {
                return;
            }

            Response.Redirect("MainCscAccount.aspx");
        }
        catch (Exception ex)
        {
            // Handle any exception that might occur during date parsing or redirection.
        }

    }
    protected void lbtnBookingAndCancellation_Click(object sender, EventArgs e)
    {
        if (ddlagent.SelectedValue == "0")
        {
            Errormsg("Select at least One Valid CSC");
            return;
        }

        DateTime dtFrom;
        DateTime dtTo;

        if (!DateTime.TryParseExact(txtfromdate.Text.Trim(), "dd/MM/yyyy", null, DateTimeStyles.None, out dtFrom))
        {
            Errormsg("Select Valid From Date");
            return;
        }
        if (!DateTime.TryParseExact(txttodate.Text.Trim(), "dd/MM/yyyy", null, DateTimeStyles.None, out dtTo))
        {
            Errormsg("Select Valid To Date");
            return;
        }

        if (dtTo < dtFrom)
        {
            Errormsg("Please Enter Valid From Date");
            return;
        }

        if ((dtTo - dtFrom).TotalDays > 30)
        {
            Errormsg("Please Note:- Reports can only be generated for 30 days at a time.");
            return;
        }

        Session["FromDate"] = txtfromdate.Text;
        Session["ToDate"] = txttodate.Text;
        Session["agcode"] = ddlagent.SelectedValue;
        Response.Redirect("MainCscBookingByAgent.aspx");

    }
    protected void lbtnDailyAccount_Click(object sender, EventArgs e)
    {
        DateTime dtFrom;
        DateTime dtTo;

        if (!DateTime.TryParseExact(txtDateF.Text.Trim(), "dd/MM/yyyy", null, DateTimeStyles.None, out dtFrom))
        {
            Errormsg("Select Valid From Date");
            return;
        }
        if (!DateTime.TryParseExact(txtDateT.Text.Trim(), "dd/MM/yyyy", null, DateTimeStyles.None, out dtTo))
        {
            Errormsg("Select Valid To Date");
            return;
        }

        if (dtTo < dtFrom)
        {
            Errormsg("Please Enter Valid From Date");
            return;
        }

        if ((dtTo - dtFrom).TotalDays > 30)
        {
            Errormsg("Please Note:- Reports can only be generated for 30 days at a time.");
            return;
        }

        Session["FromDate"] = txtDateF.Text;
        Session["ToDate"] = txtDateT.Text;
        Session["agcode"] = ddlSubAgent.SelectedValue;
        Response.Redirect("MainCscBookingAndCancel.aspx");

    }

   

    protected void lbtncscstatus_Click(object sender, EventArgs e)
    {
        Session["_Status"] = ddlcscstatus.SelectedValue;
        Response.Redirect("Cscstatusreport.aspx");
    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        Response.Redirect("Logout.aspx");
    }
    protected void lbtnNoConfirmation_Click(object sender, EventArgs e)
    {
        mpConfirmationLogout.Hide();
    }

    #endregion
}
