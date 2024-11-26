using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Npgsql;
using System.Data;

public partial class ETMBranchMaster : System.Web.UI.MasterPage
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
			loadETMBranchdetails(Session["_UserCode"].ToString());
        }      
      
    }
    private void loadETMBranchdetails(string usercode)
    {
        try
        {
			int role = Convert.ToInt16(Session["_RoleCode"].ToString());
			MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_etmbranchuserdetails");
            MyCommand.Parameters.AddWithValue("p_userid", usercode);
            MyCommand.Parameters.AddWithValue("p_role", role);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {

                    //lblcntrname.Text = dt.Rows[0]["cntr_name"].ToString().ToUpper() + " (" + dt.Rows[0]["station_name"].ToString() + ")";
                    lblempname.Text = dt.Rows[0]["empname"].ToString() + "(" + Session["_UserCode"].ToString() + ")";
                    lbloffice.Text = dt.Rows[0]["branchname"].ToString();// +"("+ dt.Rows[0]["branchid"].ToString() + ")";
                    Session["_LDepotCode"] = dt.Rows[0]["depot_code"].ToString();
					Session["_etmbranchid"] = dt.Rows[0]["branchid"].ToString();
					Session["_etmbranchname"] = dt.Rows[0]["branchname"].ToString();
					//Session["_storeid"] ="0";
				}
                else
                {
                    _common.ErrorLog("ETMBranchMaster-M1", "No record Found!!");
                    Response.Redirect("../Errorpage.aspx");
                }
            }
            else
            {
                _common.ErrorLog("ETMBranchMaster-M2", dt.TableName.ToString());
                Response.Redirect("../Errorpage.aspx");
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("ETMBranchMaster-M1", ex.Message.ToString());
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
        if (_security.isSessionExist(Session["_RNDIDENTIFIEREB"]) == true)
        {
            Session["_RNDIDENTIFIEREB"] = Session["_RNDIDENTIFIEREB"];
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

    protected void lbtnChangePwd_ServerClick(object sender, EventArgs e)
    {
        mpchnagePass.Show();
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
		Response.Redirect("ETMBranchCatalogue.aspx");
	}
}
