using System;
using System;
using System.Data;
using Npgsql;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;


public partial class Auth_CntrCashregister : BasePage 
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
            Random randomclass = new Random();
            Session["rndNoCheck"] = randomclass.Next().ToString();
            hidtoken.Value = Session["rndNoCheck"].ToString();
            loadAccountSummary();
            lbltodaysummary.Text = "Today Summary as on time " + DateTime.Now.ToString("hh:mm tt");
            Session["_moduleName"] = "Cash Register";
            loadCntrtktpasstoday();
            loadCntrOfflinetkttoday();
            loadyear(ddlyear, "Y");
            loadmonth(ddlmonth, "M", ddlyear.SelectedValue.ToString());
            loadtransactionDetails();
        }
    }
    //new code by ayush
    #region "Methods"
    private void CsrfTokenValidate()
    {
        if (hidtoken.Value != Session["rndNoCheck"].ToString())
        {
            Response.Redirect("errorpage.aspx");
            return;
        }
    }
    private void loadAccountSummary()
    {
        try
        {
            DateTime MTime = DateTime.Now;
            string s = MTime.ToString("hh:mm  tt");
            lblmandatry.Text = "Mandatory Amount Pending for Deposit upto " + DateTime.Now.Date.AddDays(-2).ToShortDateString();
            lblpending.Text = "Total Pending Amount To Deposit Till  " + DateTime.Now;

            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.cntr_account");
            MyCommand.Parameters.AddWithValue("p_counter_id", Session["_UserCntrID"].ToString());
            //Session["_UserCntrID"]
            DataTable MyTable = bll.SelectAll(MyCommand);
            if (MyTable.TableName == "Success")
            {
                if (MyTable.Rows.Count > 0)
                {
                    lbltotpendingamt.Text = MyTable.Rows[0]["BALANCEtill"].ToString() + " <i class='fa fa-rupee-sign'></i>";
                    if (Convert.ToDecimal(MyTable.Rows[0]["MENDATROYAMT"].ToString()) > 0)
                        lblmandatoryamt.Text = MyTable.Rows[0]["MENDATROYAMT"].ToString() + " <i class='fa fa-rupee-sign'></i>";
                    else
                        lblmandatoryamt.Text = "0" + " <i class='fa fa-rupee'></i>";
                   
                }
                else
                {
                    lbltotpendingamt.Text = "0";
                    lblmandatoryamt.Text = "0";
                }
            }
        }
        catch (Exception ex)
        {
            lbltotpendingamt.Text = "0";
            lblmandatoryamt.Text = "0";
            _common.ErrorLog("CntrCashregister.aspx-0001", ex.Message.ToString());
        }
    }

    private void loadCntrtktpasstoday()
    {
        try
        {            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_get_cntr_passtkt_today");
            MyCommand.Parameters.AddWithValue("p_counter_id", Session["_UserCntrID"].ToString());
            //Session["_UserCntrID"]
            DataTable MyTable = bll.SelectAll(MyCommand);
            if (MyTable.TableName == "Success")
            {
                if (MyTable.Rows.Count > 0)
                {
                    lbltodaybookingamt.Text = MyTable.Rows[0]["todaytktamt"].ToString() + " <i class='fa fa-rupee-sign'></i>";
                    lbltodaypassamt.Text = MyTable.Rows[0]["todaypassamt"].ToString() + " <i class='fa fa-rupee-sign'></i>";
                    lbltodayCancelamt.Text = MyTable.Rows[0]["todayrefund"].ToString() + " <i class='fa fa-rupee-sign'></i>";

                }
                else
                {
                }
            }
        }
        catch (Exception ex)
        {

            _common.ErrorLog("CntrCashregister.aspx-0002", ex.Message.ToString());
        }
    }

    private void loadCntrOfflinetkttoday()
    {
        try
        {
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_get_cntr_offline_tkt_today");
            MyCommand.Parameters.AddWithValue("p_counter_id", Session["_UserCntrID"].ToString());
            MyCommand.Parameters.AddWithValue("p_tripdate", DateTime.Now.Date.ToString("dd/MM/yyyy"));
            //Session["_UserCntrID"]
            DataTable MyTable = bll.SelectAll(MyCommand);
            if (MyTable.TableName == "Success")
            {
                if (MyTable.Rows.Count > 0)
                {
                    lblcurrtripamt.Text = MyTable.Rows[0]["TodayFare"].ToString()+ " <i class='fa fa-rupee-sign'></i>";

                }
                else
                {
                }
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("CntrCashregister.aspx-0003", ex.Message.ToString());

        }
    }

    private void LoadCashRegister(string date)
    {
        try
        {
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_get_cntr_cashregister");
            MyCommand.Parameters.AddWithValue("p_counter_id", Session["_UserCntrID"].ToString());
            MyCommand.Parameters.AddWithValue("p_date", date);
            //Session["_UserCntrID"]
            DataTable MyTable = bll.SelectAll(MyCommand);
            if (MyTable.TableName == "Success")
            {
                if (MyTable.Rows.Count > 0)
                {
                    gvcashregisterdtls.DataSource = MyTable;
                    gvcashregisterdtls.DataBind();
                    mpcashRegisterdtls.Show();

                }
                else
                {
                }
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("CntrCashregister.aspx-0004", ex.Message.ToString());

        }
    }

    private void loadyear(DropDownList ddlyear, string action)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_get_TransactionDetails");
            MyCommand.Parameters.AddWithValue("p_action", action);
            MyCommand.Parameters.AddWithValue("p_cntr_code", Session["_UserCntrID"].ToString());
            MyCommand.Parameters.AddWithValue("p_year", "0");
            MyCommand.Parameters.AddWithValue("p_month", "0");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlyear.DataSource = dt;
                    ddlyear.DataTextField = "p_text";
                    ddlyear.DataValueField = "p_value";
                    ddlyear.DataBind();

                    ddlyear.SelectedIndex = 0;
                }
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("CntrCashregister.aspx-0005", ex.Message.ToString());
        }
    }

    private void loadmonth(DropDownList ddlmonth, string action, string p_year)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_get_TransactionDetails");
            MyCommand.Parameters.AddWithValue("p_action", action);
            MyCommand.Parameters.AddWithValue("p_cntr_code", Session["_UserCntrID"].ToString());
            MyCommand.Parameters.AddWithValue("p_year", p_year);
            MyCommand.Parameters.AddWithValue("p_month", "0");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlmonth.DataSource = dt;
                    ddlmonth.DataTextField = "p_text";
                    ddlmonth.DataValueField = "p_value";
                    ddlmonth.DataBind();

                    ddlmonth.SelectedIndex = 0;
                }
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("CntrCashregister.aspx-0006", ex.Message.ToString());
        }
    }

    private void loadtransactionDetails()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_get_cashregisterdetails");
            MyCommand.Parameters.AddWithValue("p_cntr_code", Session["_UserCntrID"].ToString());
            MyCommand.Parameters.AddWithValue("p_month", ddlmonth.SelectedValue);
            MyCommand.Parameters.AddWithValue("p_year", ddlyear.SelectedValue);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    grdcashregister.DataSource = dt;
                    grdcashregister.DataBind();
                    grdcashregister.Visible = true;
                    pnlNocashregister.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("CntrCashregister.aspx-0007", ex.Message.ToString());
        }
    }

    #endregion

    #region Events
    protected void ddlyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        loadmonth(ddlmonth, "M", ddlyear.SelectedValue.ToString());
    }
    protected void ddlmonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        CsrfTokenValidate();
    }
    protected void lbtnSearch_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        loadtransactionDetails();
        
    }
    protected void grdcashregister_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        CsrfTokenValidate();
        if (e.CommandName == "View")
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            string date = grdcashregister.DataKeys[row.RowIndex]["date_"].ToString();
            Session["_date"] = date;
            lblcashregisterdtls.Text = "Account Details On " + date;
            lblclosingbalce.Text = "Closing Balance " + grdcashregister.DataKeys[row.RowIndex]["closing_amt"].ToString() + " <i class='fa fa-rupee-sign'></i>";
            LoadCashRegister(date);
        }
    }
    protected void lbtnReset_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        loadyear(ddlyear, "Y");
        
        loadmonth(ddlmonth, "M", ddlyear.SelectedValue.ToString());
        loadtransactionDetails();
    }
 protected void gvcashregisterdtls_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvcashregisterdtls.PageIndex = e.NewPageIndex;
        LoadCashRegister(Session["_date"].ToString());
    }
    #endregion
}