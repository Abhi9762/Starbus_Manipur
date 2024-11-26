using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_Dashboard : System.Web.UI.Page
{
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["MasterPageHeaderText"] = "System Admin |";
           
            Session["_moduleName"] = "Dashboard";
        }
    }

    private void Errormsg(string msg)
    {
        string popup = _popup.modalPopupSmall("W", "Information", msg, "Close");
        Response.Write(popup);
    }

   
    protected void lbtnlivedata_click(object sender, EventArgs e)
    {
        Response.Redirect("../Auth/LiveDataDashboard.aspx");
      
    }

protected void lbtnWalletInfo_Click(object sender, EventArgs e)
    {
        Response.Redirect("admwallettransaction.aspx");
    }
}