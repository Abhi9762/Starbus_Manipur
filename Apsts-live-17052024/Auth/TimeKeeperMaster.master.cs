using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Npgsql;
using System.Data;

public partial class Auth_TimeKeeperMaster : System.Web.UI.MasterPage
{
    sbValidation _SecurityCheck = new sbValidation();
    sbSecurity _security = new sbSecurity();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    protected void Page_Init(object sender, System.EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.AddHeader("Cache-control", "no-store, must-revalidate,private,no-cache");
        Response.AddHeader("PRAGMA", "NO-Cache");
        Response.Expires = -1;
        Response.Expires = 0;
        Response.Cache.SetNoStore();
        Response.Cache.SetExpires(DateTime.Now.AddDays(-2));
        // --------------------------------------------
        Response.Cache.SetLastModified(DateTime.Now);
        Response.Cache.SetAllowResponseInBrowserHistory(false);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        checkForSecurity();
        if (IsPostBack == false)
        {
            lblModuleName.Text = Session["_moduleName"].ToString();
           loadDepotdetails(Session["_UserCode"].ToString());
        }      
      
    }
    private void loadDepotdetails(string usercode)
    {
        try
        {
			Int32 role = Convert.ToInt32(Session["_RoleCode"].ToString()); ;
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

                    //lblcntrname.Text = dt.Rows[0]["cntr_name"].ToString().ToUpper() + " (" + dt.Rows[0]["station_name"].ToString() + ")";
                    lblempname.Text = dt.Rows[0]["emp_name"].ToString();
                    lbloffice.Text = dt.Rows[0]["depot_name"].ToString();
                    Session["_LDepotCode"] =  dt.Rows[0]["reporting_office"].ToString(); //"1010100000"; 

                }
                else
                {
                    _common.ErrorLog("TimeKeeperMaster-M1", "No record Found!!");
                    Response.Redirect("../Errorpage.aspx");
                }
            }
            else
            {
                _common.ErrorLog("TimeKeeperMaster-M2", dt.TableName.ToString());
                Response.Redirect("../Errorpage.aspx");
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("TimeKeeperMaster-M1", ex.Message.ToString());
            Response.Redirect("../Errorpage.aspx");
        }
    }
    private void checkForSecurity()
    {
        //if (Session.IsNewSession == false || Request.ServerVariables["HTTP_REFERER"].Length < 1)
        //{
        //    Response.Redirect("../Errorpage.aspx");
        //}

        if (_security.isSessionExist(Session["_UserCode"]) == true)
        {
            Session["_UserCode"] = Session["_UserCode"];
        }
        else
        {
            Response.Redirect("../sessionTimeout.aspx");
        }
        if (_security.isSessionExist(Session["_RNDIDENTIFIERTK"]) == true)
        {
            Session["_RNDIDENTIFIERTK"] = Session["_RNDIDENTIFIERTK"];
        }
        else
        {
            Response.Redirect("../sessionTimeout.aspx");
        }
        if (_security.checkvalidation() == false)
        {
            Response.Redirect("../Errorpage.aspx");
        }
        if (_security.CheckOldUserLogin(Session["_UserCode"].ToString()) == false)
        {
            Response.Redirect("../Errorpage.aspx");
        }
    }
	protected void lbtnCatalogue_ServerClick(object sender, EventArgs e)
	{
		Response.Redirect("TimeKeeperCatalogue.aspx");
	}
    protected void lbtnlogout_ServerClick(object sender, EventArgs e)
    {
        mpConfirmationLogout.Show();
    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        Response.Redirect("Logout.aspx");
    }
    protected void lbtnNoConfirmation_Click(object sender, EventArgs e)
    {
        mpConfirmationLogout.Hide();
    }
    protected void lbtnChangePwd_ServerClick(object sender, EventArgs e)
    {
        mpchnagePass.Show();
    }
}
