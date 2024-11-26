using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BasePage
/// </summary>
public class BasePage : System.Web.UI.Page
{

        
    //public BasePage()
    //{
    protected void Page_Init(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(20));
        Response.Cache.SetNoStore();
        Response.AddHeader("Cache-control", "no-store, must-revalidate,private,no-cache");
        Response.AddHeader("PRAGMA", "NO-Cache");
        Response.Expires = -1;
        Response.Cache.SetValidUntilExpires(false);
  CheckSecurity();
    }

   private void CheckSecurity()
    {
        try
        {
            if (Session["_RoleCode"] == null)
            {
                Response.Redirect("../errorpage.aspx");
                return;
            }
            else if (Session["_RoleCode"].ToString() == "")
            {
                Response.Redirect("../errorpage.aspx");
                return;
            }
            if (Session["_RoleCode"].ToString() == "102")
            {
                if (Session["_RNDIDENTIFIERSTRVL"] == null || Session["_RNDIDENTIFIERSTRVL"].ToString() == "")
                {
                    Response.Redirect("../errorpage.aspx");
                }
            }
            if (Session["_RoleCode"].ToString() == "1")
            {
                if (Session["_RNDIDENTIFIERSADM"] == null || Session["_RNDIDENTIFIERSADM"].ToString() == "")
                {
                    Response.Redirect("../errorpage.aspx");
                }
            }
 	    if (Session["_RoleCode"].ToString() == "4")
            {
                if (Session["_RNDIDENTIFIERCNTR"] == null || Session["_RNDIDENTIFIERCNTR"].ToString() == "")
                {
                    Response.Redirect("../errorpage.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("errorpage.aspx");
        }
    }

//}
    //
    // TODO: Add constructor logic here
    //
}
