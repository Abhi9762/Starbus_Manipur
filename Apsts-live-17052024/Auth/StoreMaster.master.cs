using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Npgsql;
using System.Data;

public partial class StoreMaster : System.Web.UI.MasterPage
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
			lblempname.Text = Session["StoreEmp"].ToString();
			lbloffice.Text = Session["StoreName"].ToString();			
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
        if (_security.isSessionExist(Session["_RNDIDENTIFIERSTORE"]) == true)
        {
            Session["_RNDIDENTIFIERSTORE"] = Session["_RNDIDENTIFIERSTORE"];
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
		Response.Redirect("StoreCatalogue.aspx");
	}
    protected void lbtnChangePwd_ServerClick(object sender, EventArgs e)
    {
        mpchnagePass.Show();
    }
}
