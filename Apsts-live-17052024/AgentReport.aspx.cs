using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_AgentReport : System.Web.UI.Page
{
    sbSecurity _security = new sbSecurity();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    protected void Page_Load(object sender, EventArgs e)
    {
        txtfromdate.Text = DateTime.Now.Date.AddDays(-14).ToString("dd/MM/yyyy");
        txttodate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
        txttransdate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
    }
    #region "Method"
    public void errorMessage(string msg)
    {
        lblerrmsg.Text = msg;
        mpError.Show();
    }

    private bool IsValidAccountSummary()
    {
        try
        {
            DateTime dtFrom;
            DateTime dtTo;

            if (!DateTime.TryParseExact(txtfromdate.Text.Trim(), "dd/MM/yyyy", null, DateTimeStyles.None, out dtFrom))
            {
                errorMessage("Select Valid From Date");
                return false;
            }

            if (!DateTime.TryParseExact(txttodate.Text.Trim(), "dd/MM/yyyy", null, DateTimeStyles.None, out dtTo))
            {
                errorMessage("Select Valid To Date");
                return false;
            }

            if (dtTo < dtFrom)
            {
                errorMessage("Please Enter Valid From Date");
                return false;
            }

            if ((dtTo - dtFrom).Days >= 15)
            {
                errorMessage("Please Note:- Reports can only be generated for up to 15 days at a time.");
                return false;
            }

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    private void LoadAccountSummary()
    {
        try
        {
            gvAccountsmryreport.Visible = false;
            pnlAccontsmryMsg.Visible = true;
            DataTable dt = new DataTable();

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "agent_wallet.agentpassbook");
            MyCommand.Parameters.AddWithValue("sp_agentcode", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("sp_datefrom", txtfromdate.Text);
            MyCommand.Parameters.AddWithValue("sp_dateto", txttodate.Text);

            dt = bll.SelectAll(MyCommand);

            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    gvAccountsmryreport.DataSource = dt;
                    gvAccountsmryreport.DataBind();
                    gvAccountsmryreport.Visible = true;
                    pnlAccontsmryMsg.Visible = false;
                }
                else
                {
                    errorMessage("No Record Available for Selected Date Range");
                }
            }
            else
            {
                errorMessage("No Record Available for Selected Date Range -" + dt.TableName);
            }
        }
        catch (Exception ex)
        {
            gvAccountsmryreport.Visible = false;
            pnlAccontsmryMsg.Visible = true;
            errorMessage("No Record Available for Selected Date Range -" + ex.Message);
        }
    }


    private bool IsvalidTransactionSummary()
    {
        try
        {
            DateTime dtFrom;

            if (!DateTime.TryParseExact(txttransdate.Text.Trim(), "dd/MM/yyyy", null, DateTimeStyles.None, out dtFrom))
            {
                errorMessage("Select valid transaction date");
                return false;
            }

            if (dtFrom.Date > DateTime.Now.Date)
            {
                errorMessage("Transaction date should be the current date");
                return false;
            }

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }



    private void LoadTransactionSummary()
    {
        try
        {
            gvTransactionreport.Visible = false;
            pnlTransactionmsg.Visible = true;
            DataTable dt = new DataTable();

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "agent.f_get_agent_transaction_new");
            MyCommand.Parameters.AddWithValue("p_agentcode", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("p_date", txttransdate.Text);

            dt = bll.SelectAll(MyCommand);

            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    gvTransactionreport.DataSource = dt;
                    gvTransactionreport.DataBind();
                    gvTransactionreport.Visible = true;
                    pnlTransactionmsg.Visible = false;
                }
                else
                {
                    errorMessage("No Record Available for Selected Date");
                }
            }
            else
            {
                errorMessage("No Record Available for Selected Date" + dt.TableName);
            }
        }
        catch (Exception ex)
        {
            gvTransactionreport.Visible = false;
            pnlTransactionmsg.Visible = true;
            errorMessage("No Record Available for Selected Date" + ex.Message);
        }
    }

    #endregion

    #region "Events"
    protected void lbtnAccountSummary_Click(object sender, EventArgs e)
    {
        pnlAccontsmryMsg.Visible = true;
        gvAccountsmryreport.Visible = false;
        if (!IsValidAccountSummary())
        {
            return;
        }
        LoadAccountSummary();
    }

    protected void lbtnresetAccountSummary_Click(object sender, EventArgs e)
    {
        pnlAccontsmryMsg.Visible = true;
        gvAccountsmryreport.Visible = false;
        txtfromdate.Text = "";
        txttodate.Text = "";
    }

    protected void lbtnTransaction_Click(object sender, EventArgs e)
    {
        pnlTransactionmsg.Visible = true;
        gvTransactionreport.Visible = false;
        if (!IsvalidTransactionSummary())
        {
            return;
        }
        LoadTransactionSummary();
    }

    protected void lbtnresetTransaction_Click(object sender, EventArgs e)
    {
        pnlTransactionmsg.Visible = true;
        gvTransactionreport.Visible = false;
        txttransdate.Text = "";
    }

    protected void gvTransactionreport_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
    {
        gvTransactionreport.PageIndex = e.NewPageIndex;
        LoadTransactionSummary();
    }


    #endregion


}