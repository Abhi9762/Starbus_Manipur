using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_PAdminPolicy : BasePage
{
    NpgsqlCommand MyCommand = new NpgsqlCommand();
    private sbBLL bll = new sbBLL();
    DataTable dt = new DataTable();
    sbValidation _validation = new sbValidation();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    private sbCommonFunc _common = new sbCommonFunc();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["_moduleName"] = "Agent Configuration";
            initpnl();
            initagtopuplimit();
            loadAgentType(ddlagenttype1);
            pnlAgentTopUpLimit.Visible = true;
        }
    }

    #region Method
    private void initagtopuplimit()
    {
        loadAgentTopUpLimit();
        tbMaxAmount.Text = "";
        tbAlertAmount.Text = "";
    }
    private void initpnl()
    {
        pnlAgentSecurityFee.Visible = false;
        pnlAgentTopUpLimit.Visible = false;
        pnlAgentCommission.Visible = false;
        pnlAgentSecurityFee.Visible = false;
        pnlAgentValidity.Visible = false;
        pnlAgentQuota.Visible = false;
        pnlAgentLoginconfig.Visible = false;
    }
    private void Errormsg(string msg)
    {

        string popup = _popup.modalPopupSmall("W", "Please Check", msg, "Close");
        Response.Write(popup);
    }
    private void Successmsg(string msg)
    {
        string popup = _popup.modalPopupSmall("S", "Success", msg, "Close");
        Response.Write(popup);
    }
    public void confirmmsg(string message)
    {
        lblConfirmation.Text = message;
        mpConfirmation.Show();
    }
    private void InfoMsg(string msg)
    {
        string popup = _popup.modalPopupSmall("I", "Information", msg, "Close");
        Response.Write(popup);
    }

    #endregion

    #region Events
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        if (Session["Action"].ToString() == "1")
        {
            SaveAmountValue();

        }
        if (Session["Action"].ToString() == "2")
        {
            SaveFeeValue();

        }
        if (Session["Action"].ToString() == "3")
        {
            saveAgentValidity();

        }
        if (Session["Action"].ToString() == "4")

        {
            saveAgentCommission();

        }
        if (Session["Action"].ToString() == "5")

        {
            ConfigStation("C");

        }
        if (Session["Action"].ToString() == "6")

        {
            ConfigStation("R");

        }
        if (Session["Action"].ToString() == "7")
        {
            ConfigStation("U");
        }
        if (Session["Action"].ToString() == "8" || Session["Action"].ToString() == "9")
        {
            ConfigAgLogin();

        }

    }
    #endregion

    #region "Agent Topup limit"
    #region Method
    private bool validvalueAmount()//m1
    {
        try
        {

            int msgcount = 0;
            string msg = "";
            if (tbMaxAmount.Text == "")
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Enter max Amount.<br/>";
            }
            else if (_validation.isValideDecimalNumber(tbMaxAmount.Text, 1, 5) == false)
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Valid max Amount.<br/>";
            }
            if (tbAlertAmount.Text == "")
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Enter alert Amount.<br/>";
            }
            else if (_validation.isValideDecimalNumber(tbAlertAmount.Text, 1, 5) == false)
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Valid alert Amount.<br/>";
            }
            if (msgcount > 0)
            {
                Errormsg(msg);
                return false;
            }

            return true;
        }
        catch (Exception ex)
        {

            _common.ErrorLog("PadminPolicy-M1", ex.Message.ToString());
            return false;
        }
    }
    private void SaveAmountValue()//m2
    {
        try
        {

            int agentType;
            string ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (string.IsNullOrEmpty(ipAddress))
            {
                ipAddress = Request.ServerVariables["REMOTE_ADDR"];
            }

            agentType = Convert.ToInt32(ddlagenttype1.SelectedValue);


            string UpdatedBy = Session["_UserCode"].ToString();
            string IPAddress = HttpContext.Current.Request.UserHostAddress;
            string Mresult = "";
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_agent_topup_insertupdate");
            MyCommand.Parameters.AddWithValue("p_agenttype", agentType);
            MyCommand.Parameters.AddWithValue("p_amountlimit", Convert.ToDecimal(tbMaxAmount.Text));
            MyCommand.Parameters.AddWithValue("p_alertamt", Convert.ToDecimal(tbAlertAmount.Text));
            MyCommand.Parameters.AddWithValue("p_updatedby", UpdatedBy);
            MyCommand.Parameters.AddWithValue("p_ipaddress", IPAddress);
            Mresult = bll.UpdateAll(MyCommand);
            if (Mresult == "Success")
            {
                Successmsg("Agent Top Up Limit Value Successfully Save");
                loadAgentTopUpLimit();
                tbMaxAmount.Text = "";
                tbAlertAmount.Text = "";
            }
            else
            {
                _common.ErrorLog("PadminPolicy-M2", Mresult);
                Errormsg("Oops! You're data not saved." + Mresult);
            }

        }
        catch (Exception ex)
        {
            _common.ErrorLog("PadminPolicy-M2", ex.Message.ToString());
            Errormsg(ex.Message);
        }
    }
    private void loadAgentTopUpLimit()//m3
    {
        try
        {
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_agent_topup_limit");
            DataTable MyTable = bll.SelectAll(MyCommand);
            if (MyTable.TableName == "Success")
            {
                if (MyTable.Rows.Count > 0)
                {
                    gvAgentTopuplimit.DataSource = MyTable;
                    gvAgentTopuplimit.DataBind();
                    pnlgvCurrentAgentTopUpNoRecord.Visible = false;
                    gvAgentTopuplimit.Visible = true;
                }
            }
            else
            {
                _common.ErrorLog("Padmin-Policy-M3", MyTable.TableName);
                Errormsg(MyTable.TableName);
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminPolicy-M3", ex.Message.ToString());
            Errormsg(ex.Message);
        }
    }

    private void loadAgentType(DropDownList ddl_)//M5
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            DataTable dt = new DataTable();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_agent_type");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {

                    ddl_.DataSource = dt;
                    ddl_.DataTextField = "typename";
                    ddl_.DataValueField = "typeid";
                    ddl_.DataBind();


                }
            }
            else
            {
                _common.ErrorLog("PAdminPolicy-M5", dt.TableName);
            }
            ddl_.Items.Insert(0, "Select");
            ddl_.Items[0].Value = "0";
            ddl_.SelectedIndex = 0;
            ddl_.SelectedValue = "1";
            ddl_.Enabled = false;

        }
        catch (Exception ex)
        {
            ddl_.Items.Insert(0, "-Select-");
            ddl_.Items[0].Value = "0";
            ddl_.SelectedIndex = 0;

        }
    }

    #endregion

    #region Event
    protected void lbtnSaveAmount_Click(object sender, EventArgs e)
    {
        Session["Action"] = "1";
        if (validvalueAmount() == false)
        {
            return;
        }
        confirmmsg("Do you want to Save Amount Value ?");
    }
    protected void lbtnResetAmount_Click(object sender, EventArgs e)
    {
        tbMaxAmount.Text = "";
        tbAlertAmount.Text = "";
        ddlagenttype1.SelectedValue = "0";
    }
    protected void lbtnAgentTopupLimit_Click(object sender, EventArgs e)
    {

        initpnl();
        lblmhd.Text = "Agent Topup limit";
        pnlAgentTopUpLimit.Visible = true;
        pnlAgentCommission.Visible = false;
        pnlAgentSecurityFee.Visible = false;
        pnlAgentValidity.Visible = false;
        pnlAgentQuota.Visible = false;
        initagtopuplimit();
    }
    protected void lbtnViewHelp_Click(object sender, EventArgs e)
    {
        string msg = "";
        msg = msg + "1. Set Agent Top Up Limit.";
        InfoMsg(msg);

    }
    #endregion
    #endregion

    #region Agent Security Fee


    private void initagentsecurityfee()
    {
        loadagentsecurityfee();
        loadAgentType(ddlagenttype2);
        tbSecutityAmount.Text = "";
        tbRenewalFee.Text = "";
    }

    private bool validvalueAgentSecurityFee()//m5
    {
        try
        {

            int msgcount = 0;
            string msg = "";

            if (ddlagenttype1.SelectedValue == "0")
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Select Agent Type.<br/>";
            }
            if (tbSecutityAmount.Text == "")
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Enter Security Fee.<br/>";
            }
            else if (_validation.isValideDecimalNumber(tbSecutityAmount.Text, 1, 5) == false)
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Valid Security Fee.<br/>";
            }

            if (string.IsNullOrEmpty(tbRenewalFee.Text))
            {
                msgcount++;
                msg += msgcount.ToString() + ". Renewal Fee, It can be Rs. 0 <br/>";
            }
            if (msgcount > 0)
            {
                Errormsg(msg);
                return false;
            }

            return true;
        }
        catch (Exception ex)
        {

            _common.ErrorLog("PadminPolicy-M5", ex.Message.ToString());
            return false;
        }
    }
    private void SaveFeeValue()//m6
    {
        try
        {
            string UpdatedBy = Session["_UserCode"].ToString();
            string IPAddress = HttpContext.Current.Request.UserHostAddress;
            string Mresult = "";
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_agent_securityfee_insertupdate");
            MyCommand.Parameters.AddWithValue("p_agenttype", Convert.ToInt32(ddlagenttype2.SelectedValue.ToString()));
            MyCommand.Parameters.AddWithValue("p_securityfee", Convert.ToDecimal(tbSecutityAmount.Text));
            MyCommand.Parameters.AddWithValue("p_renewalfee", Convert.ToDecimal(tbRenewalFee.Text));
            MyCommand.Parameters.AddWithValue("p_updatedby", UpdatedBy);
            MyCommand.Parameters.AddWithValue("p_ipaddress", IPAddress);
            Mresult = bll.UpdateAll(MyCommand);
            if (Mresult == "Success")
            {
                Successmsg("Agent Security Fee Value Successfully Save");
                initagentsecurityfee();
                loadagentsecurityfee();
            }
            else
            {
                _common.ErrorLog("PadminPolicy-M6", Mresult);
                Errormsg("Oops! You're data not saved." + Mresult);
            }

        }
        catch (Exception ex)
        {
            _common.ErrorLog("PadminPolicy-M6", ex.Message.ToString());
            Errormsg(ex.Message);
        }

    }
    private void loadagentsecurityfee()//m7
    {
        try
        {
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_agent_security_fee");
            DataTable MyTable = bll.SelectAll(MyCommand);
            if (MyTable.TableName == "Success")
            {
                if (MyTable.Rows.Count > 0)
                {
                    gvAgentSecurityFee.DataSource = MyTable;
                    gvAgentSecurityFee.DataBind();
                    pnlAgentCurrentSecurityAmountNoRecord.Visible = false;
                    gvAgentSecurityFee.Visible = true;
                }
            }
            else
            {
                _common.ErrorLog("PadminPolicy- M7", MyTable.TableName);
                Errormsg(MyTable.TableName);
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminPolicy-M7", ex.Message.ToString());
            Errormsg(ex.Message);
            ;

        }
    }


    #endregion

    #region Agent Security Fee

    #region Method

    #endregion


    #region Event
    protected void lbtnSaveAgentSecurityFee_Click(object sender, EventArgs e)
    {
        try
        {
            if (validvalueAgentSecurityFee() == false)
            {
                return;
            }
            Session["Action"] = 2;
            lblConfirmation.Text = "Do you want Save Agent Security Fee?";
            mpConfirmation.Show();


        }

        catch (Exception ex)
        {

        }

    }
    protected void lbtnResetAgentSecurityFee_Click(object sender, EventArgs e)
    {
        ddlagenttype2.SelectedValue = "0";
        tbSecutityAmount.Text = "";
    }
    protected void lbtnAgentSecurityFee_Click(object sender, EventArgs e)
    {

        initpnl();
        initagentsecurityfee();
        lblmhd.Text = "Agent Security/Renewal Fee";
        pnlAgentSecurityFee.Visible = true;
        pnlAgentCommission.Visible = false;
        pnlAgentTopUpLimit.Visible = false;
        pnlAgentValidity.Visible = false;
        pnlAgentQuota.Visible = false;
    }

    protected void lbtnhelp_Click(object sender, EventArgs e)
    {
        string msg = "";
        msg = msg + "1. Set Agent security fee.";
        InfoMsg(msg);
    }

    #endregion

    #endregion

    #region  Agent Validity
    #region Methods
    private void LoadAgentValidity()
    {
        try
        {


            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", " auth.getagentvalidity");
            DataTable MyTable = bll.SelectAll(MyCommand);
            if (MyTable.TableName == "Success")
            {
                if (MyTable.Rows.Count > 0)
                {

                    grdAgentValidity.DataSource = MyTable;
                    grdAgentValidity.DataBind();
                    grdAgentValidity.Visible = true;
                    // lblAgentSecurityFeeMsg.Visible = false;
                    // lbtnAgentSecurityFeeHistory.Enabled = true;
                }
                else
                {
                    grdAgentValidity.Visible = false;
                    // lblAgentSecurityFeeMsg.Visible = true;
                    // lbtnAgentSecurityFeeHistory.Enabled = false;
                }
            }
            else
            {
                grdAgentValidity.Visible = false;
                // lblAgentSecurityFeeMsg.Visible = true;
                // lbtnAgentSecurityFeeHistory.Enabled = false;
            }
        }
        catch (Exception ex)
        {

        }
    }
    private void initagentvalidity()
    {
        LoadAgentValidity();
        loadAgentType(ddlagenttype3);
        tbaccountValidity.Text = "";
        tbafterexpire.Text = "";
        tbrenewdays.Text = "";
    }
    private bool ValidAgentValidityDetails()
    {
        int msgcount = 0;
        string msg = "";



        if (!_validation.IsValidInteger(tbaccountValidity.Text, 1, tbaccountValidity.MaxLength))
        {
            msgcount++;
            msg += msgcount.ToString() + ". Account Validity.<br/>";
        }

        if (!_validation.IsValidInteger(tbafterexpire.Text, 1, tbafterexpire.MaxLength))
        {
            msgcount++;
            msg += msgcount.ToString() + ". Login Validity.<br/>";
        }

        if (!_validation.IsValidInteger(tbrenewdays.Text, 0, tbrenewdays.MaxLength))
        {
            msgcount++;
            msg += msgcount.ToString() + ". Renewal Before Validity.<br/>";
        }

        if (msgcount > 0)
        {
            Errormsg(msg);
            return false;
        }

        return true;
    }
    private void saveAgentValidity()
    {
        try
        {
            int agentType;
            decimal accountValidity, loginValidity, renewDays;
            agentType = Convert.ToInt32(ddlagenttype3.SelectedValue);
            accountValidity = Convert.ToDecimal(tbaccountValidity.Text);
            loginValidity = Convert.ToDecimal(tbafterexpire.Text);
            renewDays = Convert.ToDecimal(tbrenewdays.Text);

            string UpdatedBy = Session["_UserCode"].ToString();
            string IPAddress = HttpContext.Current.Request.UserHostAddress;
            string Mresult = "";
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_agentvalidity_insertupdate");
            MyCommand.Parameters.AddWithValue("p_agenttype", agentType);
            MyCommand.Parameters.AddWithValue("p_accountvalidity", accountValidity);
            MyCommand.Parameters.AddWithValue("p_loginvalidity", loginValidity);
            MyCommand.Parameters.AddWithValue("p_renewdays", renewDays);
            MyCommand.Parameters.AddWithValue("p_updatedby", UpdatedBy);
            MyCommand.Parameters.AddWithValue("p_ipaddress", IPAddress);
            Mresult = bll.UpdateAll(MyCommand);
            if (Mresult == "Success")
            {
                Successmsg("Agent Validity Fee details Successfully Save/Update");
                initagentvalidity();
                LoadAgentValidity();
            }
            else
            {
                _common.ErrorLog("PadminPolicy-M2", Mresult);
                Errormsg("Oops! You're data not saved." + Mresult);
            }
        }
        catch (Exception ex)
        {

            Errormsg("Error," + ex.Message);
        }
    }
    #endregion

    #region Events
    protected void lbtnagentValidity_Click(object sender, EventArgs e)
    {
        initpnl();
        initagentvalidity();
        lblmhd.Text = "Agent Validity";
        pnlAgentValidity.Visible = true;
        pnlAgentCommission.Visible = false;
        pnlAgentTopUpLimit.Visible = false;
        pnlAgentSecurityFee.Visible = false;
        pnlAgentQuota.Visible = false;
    }
    protected void lbtnAgentValiditySave_Click(object sender, EventArgs e)
    {
        try
        {
            if (!ValidAgentValidityDetails())
            {
                return;
            }

            Session["Action"] = 3;
            lblConfirmation.Text = "Do you want Save Agent Validity ?";
            mpConfirmation.Show();
        }
        catch (Exception ex)
        {
        }

    }
    #endregion
    #endregion

    #region Agent Commission

    #region Method

    private void loadAgentServicecommission()
    {
        try
        {
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.getagentcommissionlist");
            DataTable MyTable = bll.SelectAll(MyCommand);
            if (MyTable.TableName == "Success")
            {
                if (MyTable.Rows.Count > 0)
                {
                    gvAgentCommission.DataSource = MyTable;
                    gvAgentCommission.DataBind();
                    gvAgentCommission.Visible = true;

                }
                else
                {
                    gvAgentCommission.Visible = false;

                }
            }
            else
            {
                gvAgentCommission.Visible = false;

            }
        }
        catch (Exception ex)
        {
            gvAgentCommission.Visible = false;
            Errormsg("Error," + ex.Message);
        }
    }

    private bool ValidAgentCommission()
    {
        int msgcount = 0;

        for (int i = 0; i < gvAgentCommission.Rows.Count; i++)
        {
            TextBox onlineBookingTextBox = gvAgentCommission.Rows[i].FindControl("tbOnlineBooking") as TextBox;
            TextBox currentBookingTextBox = gvAgentCommission.Rows[i].FindControl("tbCurrentBooking") as TextBox;

            if (onlineBookingTextBox == null || string.IsNullOrEmpty(onlineBookingTextBox.Text))
            {
                msgcount++;
            }

            if (currentBookingTextBox == null || string.IsNullOrEmpty(currentBookingTextBox.Text))
            {
                msgcount++;
            }
        }

        if (msgcount > 0)
        {
            Errormsg("Online and Current Commission Fields Cannot be Empty. It can be 0");
            return false;
        }

        return true;
    }

    private void saveAgentCommission()
    {
        try
        {
            string UpdatedBy = Session["_UserCode"].ToString();
            string IPAddress = HttpContext.Current.Request.UserHostAddress;
            string Mresult = "";

            for (int i = 0; i < gvAgentCommission.Rows.Count; i++)
            {


                MyCommand = new NpgsqlCommand();
                MyCommand.Parameters.Clear();
                MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_agentcommission_insertupdate");
                MyCommand.Parameters.AddWithValue("p_srtp", Convert.ToInt32((gvAgentCommission.Rows[i].FindControl("lblsrtp") as Label).Text));

                MyCommand.Parameters.AddWithValue("p_onlinecommission", Convert.ToDecimal((gvAgentCommission.Rows[i].FindControl("tbOnlineBooking") as TextBox).Text));
                MyCommand.Parameters.AddWithValue("p_currentcommission", Convert.ToDecimal((gvAgentCommission.Rows[i].FindControl("tbCurrentBooking") as TextBox).Text));
                MyCommand.Parameters.AddWithValue("p_ipaddress", IPAddress);
                MyCommand.Parameters.AddWithValue("p_updatedby", UpdatedBy);

                Mresult = bll.UpdateAll(MyCommand);
                if (Mresult == "Success")
                {

                }
            }


            Successmsg("Agent Online/Current Commission Successfully Save/Update");
            loadAgentServicecommission();
        }
        catch (Exception ex)
        {

            Errormsg("Error," + ex.Message);
        }
    }


    #endregion

    #region Event
    protected void lbtnagentcommission_Click(object sender, EventArgs e)
    {
        initpnl();
        loadAgentServicecommission();
        lblmhd.Text = "Agent Commission";
        pnlAgentCommission.Visible = true;
        pnlAgentTopUpLimit.Visible = false;
        pnlAgentSecurityFee.Visible = false;
        pnlAgentValidity.Visible = false;
        pnlAgentQuota.Visible = false;
    }


    protected void lbtnSaveAgentCommission_Click(object sender, EventArgs e)
    {
        try
        {
            if (!ValidAgentCommission())
            {
                return;
            }

            Session["Action"] = 4;
            lblConfirmation.Text = "Do you want Save Agent Commission ?";
            mpConfirmation.Show();
        }
        catch (Exception ex)
        {

            Errormsg("Error," + ex.Message);
        }

    }
    #endregion

    #endregion

    #region Agent Quota

    #region Method


    private void loadstate(DropDownList ddlstate)//M3
    {
        try
        {
            ddlstate.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.f_get_states");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlstate.DataSource = dt;
                    ddlstate.DataTextField = "stname";
                    ddlstate.DataValueField = "stcode";
                    ddlstate.DataBind();
                }
            }
            ddlstate.Items.Insert(0, "All");
            ddlstate.Items[0].Value = "0";
            ddlstate.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlstate.Items.Insert(0, "All");
            ddlstate.Items[0].Value = "0";
            ddlstate.SelectedIndex = 0;
            _common.ErrorLog("Agent", ex.Message.ToString());
        }
    }
    private void loaddistrict(string State_code, DropDownList ddldistrict)
    {
        try
        {
            ddldistrict.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.f_get_district");
            MyCommand.Parameters.AddWithValue("p_statecode", State_code);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddldistrict.DataSource = dt;
                    ddldistrict.DataTextField = "distname";
                    ddldistrict.DataValueField = "distcode";
                    ddldistrict.DataBind();
                }
            }


            ddldistrict.Items.Insert(0, "All");
            ddldistrict.Items[0].Value = "0";
            ddldistrict.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddldistrict.Items.Insert(0, "All");
            ddldistrict.Items[0].Value = "0";
            ddldistrict.SelectedIndex = 0;
            _common.ErrorLog("Agent", ex.Message.ToString());
        }
    }
    private void loadStation(string stateCode, string districtCode)
    {
        try
        {
            grdstation.Visible = false;
            pnlnostation.Visible = true;

            DataTable dt;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_getallstations");
            MyCommand.Parameters.AddWithValue("p_statecode", stateCode);
            MyCommand.Parameters.AddWithValue("p_districtcode", districtCode);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    grdstation.DataSource = dt;
                    grdstation.DataBind();
                    grdstation.Visible = true;
                    pnlnostation.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            // Handle the exception, e.g., log it or show an error message
        }
    }
    private void loadConfigStation(string stateCode, string districtCode)
    {
        try
        {
            grdconfigstation.Visible = false;
            pnlconfigstation.Visible = true;

            DataTable dt;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_getconfiguredstations");
            MyCommand.Parameters.AddWithValue("p_statecode", stateCode);
            MyCommand.Parameters.AddWithValue("p_districtcode", districtCode);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    grdconfigstation.DataSource = dt;
                    grdconfigstation.DataBind();
                    grdconfigstation.Visible = true;
                    pnlconfigstation.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            // Handle the exception, e.g., log it or show an error message
        }
    }
    private void ConfigStation(string _Type)
    {
        try
        {
            decimal quota = 0;
            if (_Type == "C" || _Type == "U")
            {
                quota = Convert.ToDecimal(Session["quota"]);
            }

            DataTable dt;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_agentconfigquota");
            MyCommand.Parameters.AddWithValue("p_type", _Type);
            MyCommand.Parameters.AddWithValue("p_state", Session["stateID"].ToString());
            MyCommand.Parameters.AddWithValue("p_district", Session["districtID"].ToString());
            MyCommand.Parameters.AddWithValue("p_station", Convert.ToInt32(Session["stationID"].ToString()));
            MyCommand.Parameters.AddWithValue("p_no_of_quota", quota);
            MyCommand.Parameters.AddWithValue("p_updatedby", Session["_UserCode"]);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {

                if (_Type == "C")
                {
                    Successmsg("Station successfully configured for current booking agent quota");
                }
                else if (_Type == "R")
                {
                    Successmsg("Successfully remove station configured for current booking agent quota");
                }
                else if (_Type == "U")
                {
                    Successmsg("Successfully Update station current booking agent quota");
                }

                loadStation(ddlstate.SelectedValue, ddldistrict.SelectedValue);
                loadConfigStation(ddlconfigstate.SelectedValue, ddlconfigdistrict.SelectedValue);
            }

            Session["stationID"] = null;
            Session["stateID"] = null;
            Session["districtID"] = null;
            Session["quota"] = null;
        }
        catch (Exception ex)
        {
            // Handle the exception
        }
    }
    #endregion

    #region Event
    protected void lbtnagentquota_Click(object sender, EventArgs e)
    {
        initpnl();
        loadstate(ddlstate);
        loadstate(ddlconfigstate);
        loaddistrict(ddlstate.SelectedValue, ddldistrict);
        loaddistrict(ddlconfigstate.SelectedValue, ddlconfigdistrict);
        loadStation(ddlstate.SelectedValue, ddldistrict.SelectedValue);
        loadConfigStation(ddlconfigstate.SelectedValue, ddlconfigdistrict.SelectedValue);
        lblmhd.Text = "Agent Quota Configuration";
        pnlAgentQuota.Visible = true;
        pnlAgentCommission.Visible = false;
        pnlAgentTopUpLimit.Visible = false;
        pnlAgentSecurityFee.Visible = false;
        pnlAgentValidity.Visible = false;

    }
    protected void ddlstate_SelectedIndexChanged(object sender, EventArgs e)
    {
        loaddistrict(ddlstate.SelectedValue, ddldistrict);
        loadStation(ddlstate.SelectedValue, ddldistrict.SelectedValue);
    }
    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadStation(ddlstate.SelectedValue, ddldistrict.SelectedValue);
    }
    protected void ddlconfigstate_SelectedIndexChanged(object sender, EventArgs e)
    {
        loaddistrict(ddlconfigstate.SelectedValue, ddlconfigdistrict);
        loadConfigStation(ddlconfigstate.SelectedValue, ddlconfigdistrict.SelectedValue);
    }
    protected void ddlconfigdistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadConfigStation(ddlconfigstate.SelectedValue, ddlconfigdistrict.SelectedValue);
    }
    protected void grdstation_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        string stationID, stateID, districtID;

        if (e.CommandName == "ADDCONFIG")
        {
            // Reference the GridView Row.
            GridViewRow row = grdstation.Rows[index];

            // Find the TextBox control.
            TextBox tbQouta = row.FindControl("tbQouta") as TextBox;

            if (string.IsNullOrEmpty(tbQouta.Text) || Convert.ToInt32(tbQouta.Text) <= 0)
            {
                Errormsg("Quota can not be empty and also not 0, Please enter valid quota");
                return;
            }

            Session["Action"] = 5;
            stationID = grdstation.DataKeys[index].Values["stationcode_"].ToString();
            stateID = grdstation.DataKeys[index].Values["statecode_"].ToString();
            districtID = grdstation.DataKeys[index].Values["districtcode_"].ToString();

            Session["stationID"] = stationID;
            Session["stateID"] = stateID;
            Session["districtID"] = districtID;
            Session["quota"] = tbQouta.Text;

            lblConfirmation.Text = "Do you want to station quota configured";
            mpConfirmation.Show();
        }

    }
    //protected void grdstation_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        LinkButton lbtnremoveconfig = e.Row.FindControl("lbtnremoveconfig") as LinkButton;
    //        LinkButton lbtnupdatequota = e.Row.FindControl("lbtnupdatequota") as LinkButton;
    //        TextBox tbQouta = e.Row.FindControl("tbQouta") as TextBox;
    //        Label lblquota = e.Row.FindControl("lblquota") as Label;

    //        DataRowView rowView = e.Row.DataItem as DataRowView;

    //        lbtnremoveconfig.Visible = false;
    //        lbtnupdatequota.Visible = false;
    //        tbQouta.Visible = false;
    //        lblquota.Visible = true;

    //        if (rowView["RemoveYN"].ToString() == "Y")
    //        {
    //            lbtnremoveconfig.Visible = true;
    //        }

    //        if (rowView["UpdateYN"].ToString() == "Y")
    //        {
    //            lbtnupdatequota.Visible = true;
    //            tbQouta.Visible = true;
    //            lblquota.Visible = false;
    //        }
    //    }

    //}
    protected void grdconfigstation_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        string stationID, stateID, districtID;

        if (e.CommandName == "REMOVECONFIG")
        {
            Session["Action"] = 6;
            stationID = grdconfigstation.DataKeys[index].Values["stationcode_"].ToString();
            stateID = grdconfigstation.DataKeys[index].Values["statecode_"].ToString();
            districtID = grdconfigstation.DataKeys[index].Values["districtcode_"].ToString();

            Session["stationID"] = stationID;
            Session["stateID"] = stateID;
            Session["districtID"] = districtID;

            lblConfirmation.Text = "Do you want to remove station quota configured";
            mpConfirmation.Show();
        }

        if (e.CommandName == "UPDATEQUOTA")
        {
            // Reference the GridView Row.
            GridViewRow row = grdconfigstation.Rows[index];

            // Find the TextBox control.
            TextBox tbQouta = row.FindControl("tbQouta") as TextBox;

            Session["Action"] = 7;
            stationID = grdconfigstation.DataKeys[index].Values["stationcode_"].ToString();
            stateID = grdconfigstation.DataKeys[index].Values["statecode_"].ToString();
            districtID = grdconfigstation.DataKeys[index].Values["districtcode_"].ToString();
            int noofquota = Convert.ToInt32(grdconfigstation.DataKeys[index].Values["no_of_quota_"].ToString());
            int processquota = Convert.ToInt32(grdconfigstation.DataKeys[index].Values["in_process_quota_"].ToString());
            int assignedquota = Convert.ToInt32(grdconfigstation.DataKeys[index].Values["assigned_quota_"].ToString());

            if ((processquota + assignedquota) != 0)
            {
                if (tbQouta.Text.ToString() == "0")
                {
                    Errormsg("As allocation is already there quota can not be decresed ");
                    tbQouta.Text = noofquota.ToString();
                    return;
                }
                if ((processquota + assignedquota) > Convert.ToInt32(tbQouta.Text.ToString()))
                {
                    Errormsg("As allocation is already there quota can not be decresed");
                    tbQouta.Text = noofquota.ToString();
                    return;
                }
            }
            else if (tbQouta.Text.ToString() == "0")
            {
                Session["Action"] = 6;
                lblConfirmation.Text = "Do you want to remove this station from agent current booking list ?";
            }


            Session["stationID"] = stationID;
            Session["stateID"] = stateID;
            Session["districtID"] = districtID;
            Session["quota"] = tbQouta.Text;

            lblConfirmation.Text = "Do you want to Update station quota";
            mpConfirmation.Show();
        }

    }
    protected void grdconfigstation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbtnremoveconfig = e.Row.FindControl("lbtnremoveconfig") as LinkButton;
            LinkButton lbtnupdatequota = e.Row.FindControl("lbtnupdatequota") as LinkButton;
            // TextBox tbQouta = e.Row.FindControl("tbQouta") as TextBox;
            // Label lblquota = e.Row.FindControl("lblquota") as Label;
            DataRowView rowView = e.Row.DataItem as DataRowView;

            lbtnremoveconfig.Visible = true;
            lbtnupdatequota.Visible = true;
            //tbQouta.Visible = false;
            //lblquota.Visible = true;

            if (rowView["remove_yn"].ToString() == "Y")
            {
                lbtnremoveconfig.Visible = true;
            }
            else
            {
                lbtnremoveconfig.Visible = false;
            }
            if (rowView["update_yn"].ToString() == "Y")
            {
                //lbtnupdatequota.Visible = true;
                //tbQouta.Visible = true;
                //lblquota.Visible = false;
            }
        }

    }

    #endregion

    #endregion

    #region Agent Login Configuration
    #region Method
    private void loadagent(GridView grdagentlist)
    {
        try
        {
            grdagentlist.Visible = false;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "agent.fn_get_agent_list");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    grdagentlist.DataSource = dt;
                    grdagentlist.DataBind();
                    grdagentlist.Visible = true;
                }
            }

        }
        catch (Exception ex)
        {

            _common.ErrorLog("Agent", ex.Message.ToString());
        }
    }
    private void ConfigAgLogin()
    {
        try
        {
            string ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ipAddress))
            {
                ipAddress = Request.ServerVariables["REMOTE_ADDR"];
            }
            string UpdatedBy = Session["_UserCode"].ToString();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "agent.fn_config_agent_login");
            MyCommand.Parameters.AddWithValue("p_action", Session["Action"].ToString());
            MyCommand.Parameters.AddWithValue("p_agentid", Session["AgentId"].ToString());
            MyCommand.Parameters.AddWithValue("p_ipaddress", ipAddress);
            MyCommand.Parameters.AddWithValue("p_updateby", UpdatedBy);

            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows[0]["status"].ToString() == "DONE")
                {
                    if (Session["Action"].ToString() == "8")
                    {
                        Successmsg("Agent Login successfully enabled");
                    }
                    if (Session["Action"].ToString() == "9")
                    {
                        Successmsg("Agent Login successfully disabled");
                    }
                    loadagent(grdagentlist);
                    Session["Action"] = null;
                    Session["AgentId"] = null;
                }
                else
                {

                }
            }
        }
        catch (Exception e)
        { }
    }
    #endregion

    #region Event
    protected void lbtnAgentloginconf_Click(object sender, EventArgs e)
    {
        initpnl();
        loadagent(grdagentlist);
        pnlAgentLoginconfig.Visible = true;
        lblmhd.Text = "Agent Login Enable and disable";
    }
    protected void grdagentlist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbtnEnableLogin = (LinkButton)e.Row.FindControl("lbtnEnableLogin");
            LinkButton lbtnDisableLogin = (LinkButton)e.Row.FindControl("lbtnDisableLogin");
            DataRowView rowView = (DataRowView)e.Row.DataItem;
            lbtnEnableLogin.Visible = false;
            lbtnDisableLogin.Visible = false;

            if (rowView["agcurrentstatus"].ToString() == "A")
            {
                lbtnDisableLogin.Visible = true;
            }
            else
            {
                lbtnEnableLogin.Visible = true;
            }
        }
    }
    protected void grdagentlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdagentlist.PageIndex = e.NewPageIndex;
        loadagent(grdagentlist);
    }
    protected void grdagentlist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "ENABLELOGIN")
        {
            // Reference the GridView Row.
            GridViewRow row = grdagentlist.Rows[index];
            Session["Action"] = 8;
            Session["AgentId"] = grdagentlist.DataKeys[index].Values["agcode"].ToString();
            lblConfirmation.Text = "Do you want to login Enable  for " + grdagentlist.DataKeys[index].Values["agname"].ToString() + "(" + grdagentlist.DataKeys[index].Values["agcode"].ToString() + ") Agent ?";
            mpConfirmation.Show();
        }
        if (e.CommandName == "DISABLELOGIN")
        {
            // Reference the GridView Row.
            GridViewRow row = grdagentlist.Rows[index];
            Session["Action"] = 9;
            Session["AgentId"] = grdagentlist.DataKeys[index].Values["agcode"].ToString();
            lblConfirmation.Text = "Do you want to login Disable  for " + grdagentlist.DataKeys[index].Values["agname"].ToString() + "(" + grdagentlist.DataKeys[index].Values["agcode"].ToString() + ") Agent ?";
            mpConfirmation.Show();
        }
    }
    #endregion
    #endregion


}
