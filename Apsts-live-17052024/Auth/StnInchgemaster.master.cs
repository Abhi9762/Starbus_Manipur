using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Npgsql;
using System.Data;

public partial class Auth_StnInchgemaster : System.Web.UI.MasterPage
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
			MyCommand = new NpgsqlCommand();
			MyCommand.Parameters.Clear();
			MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_logged_st_incharge_details");
			MyCommand.Parameters.AddWithValue("p_userid", usercode);
			dt = bll.SelectAll(MyCommand);
			if (dt.TableName == "Success")
			{
				if (dt.Rows.Count > 0)
				{
					lblempname.Text = dt.Rows[0]["emp_name"].ToString();
					lbloffice.Text = dt.Rows[0]["officename"].ToString();
					Session["_LDepotCode"] = dt.Rows[0]["depot_code"].ToString();
                    Session["_LOfficeid"] = dt.Rows[0]["officeid"].ToString();
                    if (Session["_RoleCode"].ToString() == "99")
                    {
                        Session["_LDepotCode"] = dt.Rows[0]["officeid"].ToString();
                    }
                    if (Session["_RoleCode"].ToString() == "5" )
                    {
                        Session["_LDepotCode"] = Session["_DepotCodeM"].ToString();
                    }
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
        if (Session["_RoleCode"].ToString() == "99")
        {
            if (_security.isSessionExist(Session["_RNDIDENTIFIEREMPDASH"]) == true)
            {
                Session["_RNDIDENTIFIEREMPDASH"] = Session["_RNDIDENTIFIEREMPDASH"];
            }
            else
            {
                Response.Redirect("sessionTimeout.aspx");
            }
        }
        if (Session["_RoleCode"].ToString() == "8")
        {
            if (_security.isSessionExist(Session["_RNDIDENTIFIERSTINCHARGE"]) == true)
            {
                Session["_RNDIDENTIFIERSTINCHARGE"] = Session["_RNDIDENTIFIERSTINCHARGE"];
            }
            else
            {
                Response.Redirect("../sessionTimeout.aspx");
            }
        }
        if (Session["_RoleCode"].ToString() == "5")
        {
            if (_security.isSessionExist(Session["_RNDIDENTIFIERDOPT"]) == true)
            {
                Session["_RNDIDENTIFIERDOPT"] = Session["_RNDIDENTIFIERDOPT"];
            }
            else
            {
                Response.Redirect("sessionTimeout.aspx");
            }
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
    protected void lbtnCatalogue_ServerClick(object sender, EventArgs e)
	{
        if (Session["_RoleCode"].ToString() == "8")
        {
            Response.Redirect("StInchargeCatalogue.aspx");
        }
        if (Session["_RoleCode"].ToString() == "5")
        {
            Response.Redirect("DepotDashboard.aspx");
        }
        if (Session["_RoleCode"].ToString() == "99")
        {
            Response.Redirect("StInchargeCatalogue.aspx");
        }
    }
    protected void lbtnprofile_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeDash.aspx");
    }
    protected void lbtnChangePwd_ServerClick(object sender, EventArgs e)
    {
        mpchnagePass.Show();
    }
}
