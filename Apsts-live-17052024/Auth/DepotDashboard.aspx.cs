using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class Auth_DepotDashboard : System.Web.UI.Page
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    string current_date = DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("yyyy");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["_moduleName"] = "Catalogue";
            lblsummarydate.Text = DateTime.Now.ToString();
            loadDepotdetails(Session["_UserCode"].ToString());
            loadReadyTrip();
            loadUpcoming();
            loadPrepared();

            LoadCurrntBookinfSummart(DateTime.Now.ToString("dd/MM/yyy"), Session["_LDepotCode"].ToString());

            LoadOnlineTotalSmry(DateTime.Now.ToString("dd/MM/yyy"), Session["_LDepotCode"].ToString(),"0", lblonltpsngr, lblonltamt);
            LoadOnlineTotalSmry(DateTime.Now.ToString("dd/MM/yyy"), Session["_LDepotCode"].ToString(), "T", lblonlpsngr, lblonlamt);
            LoadOnlineTotalSmry(DateTime.Now.ToString("dd/MM/yyy"), Session["_LDepotCode"].ToString(), "C", lblcntrpsngr, lblcntramt);
            LoadOnlineTotalSmry(DateTime.Now.ToString("dd/MM/yyy"), Session["_LDepotCode"].ToString(), "A", lblagntpsngr, lblagntamt);

        }

    }
    

  

    #region "Methods"
    private void loadDepotdetails(string usercode)
    {
        try
        {
            int role = Convert.ToInt16(Session["_RoleCode"].ToString());
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_loggeduserdetails");
            MyCommand.Parameters.AddWithValue("p_userid", usercode);
            MyCommand.Parameters.AddWithValue("p_role", role);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {

                    Session["_LDepotCode"] = dt.Rows[0]["depot_code"].ToString();
Session["_DepotCodeM"] = dt.Rows[0]["depot_code"].ToString();
                }
                else
                {
                    _common.ErrorLog("Cntrmaster-M1", "No record Found!!");
                    Response.Redirect("../Errorpage.aspx");
                }
            }
            else
            {
                _common.ErrorLog("Cntrmaster-M2", dt.TableName.ToString());
                Response.Redirect("../Errorpage.aspx");
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("Cntrmaster-M1", ex.Message.ToString());
            Response.Redirect("../Errorpage.aspx");
        }
    }
    private void loadReadyTrip()
    {
        try
        {
            wsClass obj = new wsClass();
            DataTable dt = obj.gettrip(Session["_LDepotCode"].ToString(), "0", "O", "A", current_date);
            DataRow[] dtrow = dt.Select("minutes>=startminute and minutes <=endminute");
            //DataRow[] dtrow = dt.Select();
            if (dtrow.Length > 0)
            {
                lblReadycount.Text = dtrow.Length.ToString();
            }
            else
            {
                lblReadycount.Text = "0";
            }
        }
        catch (Exception ex)
        { }
    }
    private void loadUpcoming()
    {
        try
        {
            wsClass obj = new wsClass();
            DataTable dt = obj.gettrip(Session["_LDepotCode"].ToString(), "0", "O", "U", current_date);
            DataRow[] dtrow = dt.Select("minutes>=startminute and minutes <=endminute");
            if (dtrow.Length > 0)
            {
                lblUpcomingCount.Text = dtrow.Length.ToString();
            }
            else
            {
                lblUpcomingCount.Text = "0";
            }
        }
        catch (Exception ex)
        { }
    }
    private void loadPrepared()
    {
        try
        {
            wsClass obj = new wsClass();
            DataTable dt = obj.gettrip(Session["_LDepotCode"].ToString(), "0", "O", "P", current_date);
            DataRow[] dtrow = dt.Select("minutes>=startminute and minutes <=endminute");
            if (dtrow.Length > 0)
            {
                lblpreparedcount.Text = dtrow.Length.ToString();
            }
            else
            {
                lblpreparedcount.Text = "0";
            }
        }
        catch (Exception ex)
        { }
    }
    private void LoadOnlineTotalSmry(string trans_date, string depot_code, string book_type, Label lblpsngr, Label lblamt)
    {
        try
        {
            lblpsngr.Text = "0";
            lblamt.Text = "0";
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_get_onlinebooking_smry");
            MyCommand.Parameters.AddWithValue("sp_date", trans_date);
            MyCommand.Parameters.AddWithValue("sp_depotcode", depot_code);
            MyCommand.Parameters.AddWithValue("sp_mode", book_type);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    lblpsngr.Text = dt.Rows[0]["psngr"].ToString();
                    lblamt.Text = dt.Rows[0]["totalamt"].ToString();
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    private void LoadCurrntBookinfSummart(string trans_date, string depot_code)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_get_currntbooking_smry");
            MyCommand.Parameters.AddWithValue("sp_date", trans_date);
            MyCommand.Parameters.AddWithValue("sp_depotcode", depot_code);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    lblttrip.Text = dt.Rows[0]["trip"].ToString();
                    if (dt.Rows[0]["psngr"].ToString() == "")
                    {
                        lbltpsngr.Text = "0";
                    }
                    else
                    {
                        lbltpsngr.Text = dt.Rows[0]["psngr"].ToString();
                    }
                    if (dt.Rows[0]["amt"].ToString() == "")
                    {
                        lbltamt.Text = "0";
                    }
                    else
                    {
                        lbltamt.Text = dt.Rows[0]["amt"].ToString();
                    }

                   
                    lbltunderprocess.Text = dt.Rows[0]["underprocess"].ToString();
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    #endregion
    #region "Events"
    protected void tricpchart_Click(object sender, EventArgs e)
    {
        loadReadyTrip();
        loadUpcoming();
        loadPrepared();
    }
    #endregion

}