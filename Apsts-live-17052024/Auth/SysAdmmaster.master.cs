using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_SysAdmmaster : System.Web.UI.MasterPage
{
    sbValidation _SecurityCheck = new sbValidation();
    sbSecurity _security = new sbSecurity();
    protected void Page_Init(object sender, System.EventArgs e)
    {
        //Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //Response.AddHeader("Cache-control", "no-store, must-revalidate,private,no-cache");
        //Response.AddHeader("PRAGMA", "NO-Cache");
        //Response.Expires = -1;
        //Response.Expires = 0;
        //Response.Cache.SetNoStore();
        //Response.Cache.SetExpires(DateTime.Now.AddDays(-2));
        //// --------------------------------------------
        //Response.Cache.SetLastModified(DateTime.Now);
        //Response.Cache.SetAllowResponseInBrowserHistory(false);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        
        checkForSecurity();
        if (_security.isSessionExist(Session["_RoleCode"]) == true)
        {

        }
        else
        {
            Response.Redirect("sessionTimeout.aspx");
        }
        if (IsPostBack == false)
        {
            lblModuleName.Text = Session["_moduleName"].ToString();
             //lblModuleuser.Text= Session["MasterPageHeaderText"].ToString();
            if (Session["_RoleCode"].ToString() == "1")
            {
                lblUserName.Text = "System Admin";
                lblModuleuser.Text = Session["MasterPageHeaderText"].ToString();
		lbtnprofile.Visible=false;
            }
            if (Session["_RoleCode"].ToString() == "5")
            {
                lblModuleuser.Text = "Depot Manager -";
                lblUserName.Text=Session["_empdepo"].ToString();
		lbtndashboard.Visible=false;
		lbtnprofile.Visible=false;
            }
            if (Session["_RoleCode"].ToString() == "4")
            {
                lblModuleuser.Text = "Counter -";
                lblUserName.Text = Session["_empcntr"].ToString();
		lbtndashboard.Visible=false;
		lbtnprofile.Visible=false;
            }
	   if (Session["_RoleCode"].ToString() == "6")
            {
                lblModuleuser.Text = "Helpdesk -";
                lblUserName.Text = Session["_UserName"].ToString();
		lbtndashboard.Visible=false;
		lbtnprofile.Visible=false;
 
            }

	   if (Session["_RoleCode"].ToString() == "99")
            {
                lblModuleuser.Text = "System Admin -";
                lblUserName.Text = Session["_UserName"].ToString();
                lbtndashboard.Visible = false;
                lbtnCatalogue.Visible = true;
		lbtnprofile.Visible=true;
            }  
  
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
            Response.Redirect("sessionTimeout.aspx");
        }

        if (_security.checkvalidation() == false)
        {
            Response.Redirect("Errorpage.aspx");
        }
        if (_security.CheckOldUserLogin(Session["_UserCode"].ToString()) == false)
        {
            Response.Redirect("Errorpage.aspx");
        }
        if (Session["_RoleCode"].ToString() == "1")
        {
            if (_security.isSessionExist(Session["_RNDIDENTIFIERSADM"]) == true)
            {
                Session["_RNDIDENTIFIERSADM"] = Session["_RNDIDENTIFIERSADM"];
            }
            else
            {
                Response.Redirect("sessionTimeout.aspx");
            }
        }
        if (Session["_RoleCode"].ToString() == "4")
        {
            if (_security.isSessionExist(Session["_RNDIDENTIFIERCNTR"]) == true)
            {
                Session["_RNDIDENTIFIERCNTR"] = Session["_RNDIDENTIFIERCNTR"];
            }
            else
            {
                Response.Redirect("sessionTimeout.aspx");
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
	if (Session["_RoleCode"].ToString() == "6")
        {
            if (_security.isSessionExist(Session["_RNDIDENTIFIERHELPDESK"]) == true)
            {
                Session["_RNDIDENTIFIERHELPDESK"] = Session["_RNDIDENTIFIERHELPDESK"];
            }
            else
            {
                Response.Redirect("sessionTimeout.aspx");
            }
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

    protected void lbtndashboard_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("~/Auth/Dashboard.aspx");
    }

protected void lbtnprofile_ServerClick(object sender, EventArgs e)
	{
		Response.Redirect("EmployeeDash.aspx");
	}

    protected void lbtnCatalogue_ServerClick(object sender, EventArgs e)
    {

        if (Session["_RoleCode"].ToString() == "1")
        {
            Response.Redirect("SysAdmCatalogue.aspx");
        }
        if (Session["_RoleCode"].ToString() == "5")
        {
            Response.Redirect("DepotDashboard.aspx");
        }
        if (Session["_RoleCode"].ToString() == "4")
        {
            Response.Redirect("CntrDashboard.aspx");
        }
 if (Session["_RoleCode"].ToString() == "6")
        {
            Response.Redirect("Helpdesk.aspx");
        }
 if (Session["_RoleCode"].ToString() == "99")
        {
            Response.Redirect("EmployeeDash.aspx");
        }
    }
    protected void lbtnChangePwd_ServerClick(object sender, EventArgs e)
    {
        mpchnagePass.Show();
    }

    
}
