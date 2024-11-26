using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Logout : BasePage
{
    private sbBLL bll = new sbBLL();
    sbSecurity _security = new sbSecurity();
    protected void Page_Init(object sender, System.EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.AddHeader("Cache-control", "no-store, must-revalidate,private,no-cache");
        Response.AddHeader("PRAGMA", "NO-Cache");
        Response.Expires = -1;
        Response.Expires = 0;
        Response.Cache.SetNoStore();
        Response.Cache.SetExpires(DateTime.Now.AddDays(-2));
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["_UserCode"] != null)
        {
            _security.RemoveUserLogin(Session["_UserCode"].ToString());
            if (_security.checkvalidation() == false)
            {
                Response.Redirect("Errorpage.aspx");
            }
            updateLogoutDetails();
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
        }
        
    }

    private void updateLogoutDetails()
    {
        try
        {
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_insert_logout_log");
            MyCommand.Parameters.AddWithValue("p_logid",Convert.ToInt32( Session["_LogID"]));
            string Mresult = bll.UpdateAll(MyCommand);
            if (Mresult == "Success")
            {

            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Errorpage.aspx");
        }

    }

    #region"Events"
    protected void lbtnvisitagain_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Home.aspx");
    }
    #endregion

}