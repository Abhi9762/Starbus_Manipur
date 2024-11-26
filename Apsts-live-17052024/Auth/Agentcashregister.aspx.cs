using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_Agentcashregister : System.Web.UI.Page
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadyear();
            ddlyear.SelectedValue = Session["Year"].ToString();
            loadmonth(ddlyear.SelectedValue);
            ddlmonth.SelectedValue = Session["Month"].ToString();
            loaddate(ddlyear.SelectedValue, ddlmonth.SelectedValue);
            ddldate.SelectedValue = Session["Date"].ToString();
            lbldate.Text = "Booking/Cancellation/Deposit Details of " + Session["Date"].ToString();
            accountdetails(ddldate.SelectedValue);
            agbooking(ddldate.SelectedValue);
            agcancel(ddldate.SelectedValue);
            agdeposit(ddldate.SelectedValue);
        }
    }

    #region "Methods"
    private void Errormsg(string msg)
    {
        string popup = _popup.modalPopupSmall("D", "Please Check", msg, "Close");
        Response.Write(popup);
    }
    private void loadyear()
    {
        try
        {

            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "agent.fn_get_agen_cash_date");
            MyCommand.Parameters.AddWithValue("p_agentcode", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("p_year", "0");
            MyCommand.Parameters.AddWithValue("p_month", "0");
            MyCommand.Parameters.AddWithValue("p_action", "Y");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlyear.DataSource = dt;
                    ddlyear.DataValueField = "value_";
                    ddlyear.DataTextField = "value_";
                    ddlyear.DataBind();
                }
            }
            ddlyear.Items.Insert(0, "--Select--");
            ddlyear.Items[0].Value = "0";
            ddlyear.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlyear.Items.Insert(0, "--Select--");
            ddlyear.Items[0].Value = "0";
            ddlyear.SelectedIndex = 0;
        }
    }
    private void loadmonth(string yy)
    {
        try
        {

            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "agent.fn_get_agen_cash_date");
            MyCommand.Parameters.AddWithValue("p_agentcode", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("p_year", yy);
            MyCommand.Parameters.AddWithValue("p_month", "0");
            MyCommand.Parameters.AddWithValue("p_action", "M");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlmonth.DataSource = dt;
                    ddlmonth.DataValueField = "value_";
                    ddlmonth.DataTextField = "name_";
                    ddlmonth.DataBind();
                }
            }
            ddlmonth.Items.Insert(0, "--Select--");
            ddlmonth.Items[0].Value = "0";
            ddlmonth.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlmonth.Items.Insert(0, "--Select--");
            ddlmonth.Items[0].Value = "0";
            ddlmonth.SelectedIndex = 0;
        }
    }
    private void loaddate(string yy, string mm)
    {
        try
        {

            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "agent.fn_get_agen_cash_date");
            MyCommand.Parameters.AddWithValue("p_agentcode", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("p_year", yy);
            MyCommand.Parameters.AddWithValue("p_month", mm);
            MyCommand.Parameters.AddWithValue("p_action", "D");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddldate.DataSource = dt;
                    ddldate.DataValueField = "value_";
                    ddldate.DataTextField = "value_";
                    ddldate.DataBind();
                }
            }
            ddldate.Items.Insert(0, "--Select--");
            ddldate.Items[0].Value = "0";
            ddldate.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddldate.Items.Insert(0, "--Select--");
            ddldate.Items[0].Value = "0";
            ddldate.SelectedIndex = 0;
        }
    }
    private void accountdetails(string date)
    {
        try
        {

            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "agent.fn_get_agent_cashregister");
            MyCommand.Parameters.AddWithValue("p_agentcode", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("sp_date", date);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    lblopenblnc.Text = dt.Rows[0]["open_bal"].ToString(); //dr.Rows(0)("OPENINGBALANCE")
                    lbltrnsamt.Text = dt.Rows[0]["txn_amt"].ToString();// dr.Rows(0)("TRANSACTIONAMOUNT")
                    lbldepositamt.Text = dt.Rows[0]["depo_amt"].ToString();// dr.Rows(0)("DEPOSITAMOUNT")
                    lblcloseblnc.Text = dt.Rows[0]["closin_bal"].ToString();// dr.Rows(0)("CLOSINGBALANCE")
                }
            }

        }
        catch (Exception ex)
        {

        }
    }
    private void agbooking(string date)
    {
        try
        {

            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "agent.fn_get_agent_txndatebooking");
            MyCommand.Parameters.AddWithValue("p_agentcode", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("sp_date", date);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    grdagbooking.DataSource = dt;
                    grdagbooking.DataBind();
                    grdagbooking.Visible = true;
                    grdagbookingmsg.Visible = false;
                }
                else
                {
                    grdagbooking.Visible = false;
                    grdagbookingmsg.Visible = true;
                    grdagbookingmsg.Text = "Sorry,You haven't made any booking";
                }
            }

        }
        catch (Exception ex)
        {
            grdagbooking.Visible = false;
            grdagbookingmsg.Visible = true;
            grdagbookingmsg.Text = "Sorry,You haven't made any booking";
        }
    }
    private void agcancel(string date)
    {
        try
        {

            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "agent.fn_get_agent_txndatecancel");
            MyCommand.Parameters.AddWithValue("p_agentcode", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("sp_date", date);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    grdagcancel.DataSource = dt;
                    grdagcancel.DataBind();
                    grdagcancel.Visible = true;
                    grdagcancelmsg.Visible = false;
                }
                else
                {
                    grdagcancel.Visible = false;
                    grdagcancelmsg.Visible = true;
                    grdagcancelmsg.Text = "Sorry,You haven't made any Cancellation";
                }
            }

        }
        catch (Exception ex)
        {
            grdagcancel.Visible = false;
            grdagcancelmsg.Visible = true;
            grdagcancelmsg.Text = "Sorry,You haven't made any Cancellation";
        }
    }
    private void agdeposit(string date)
    {
        try
        {

        }
        catch (Exception ex)
        {
           
        }
    }
    

    #endregion

    #region "Events"
    protected void btnshow_Click(object sender, EventArgs e)
    {
        if (ddlyear.SelectedValue == "0")
        {
            Errormsg("Please select valid Year");
            return;
        }
        if (ddlmonth.SelectedValue == "0")
        {
            Errormsg("Please select valid Month");
            return;
        }
        if (ddldate.SelectedValue == "0")
        {
            Errormsg("Please select valid Date");
            return;
        }
        tblaccdetails.Visible = true;
        lblacdetailsmsg.Visible = false;
        bokcandetails.Visible = true;
        lblbokcandetails.Visible = false;


        agbooking(ddldate.SelectedValue);
        agcancel(ddldate.SelectedValue);
        accountdetails(ddldate.SelectedValue);
        lbldate.Text = "Booking/Cancellation/Deposit Details of " + ddldate.SelectedValue;
    }
    protected void btnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("Agentdashboard.aspx", true);
    }
    protected void grdagdeposit_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdagdeposit.PageIndex = e.NewPageIndex;
        agdeposit(ddldate.SelectedValue);
    }
    protected void grdagcancel_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdagcancel.PageIndex = e.NewPageIndex;
        agcancel(ddldate.SelectedValue);
    }
    protected void grdagbooking_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdagbooking.PageIndex = e.NewPageIndex;
        agbooking(ddldate.SelectedValue);
    }
    #endregion
}