using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_AgentValidityExpire : System.Web.UI.Page
{
    sbSecurity _security = new sbSecurity();
    protected void Page_Load(object sender, EventArgs e)
    {
        expiredt.Text = "Your account validity has expired on " + Session["_ValidTo"].ToString();
    }

    protected void lbtnHome_Click(object sender, EventArgs e)
    {
        _security.RemoveUserLogin(Session["_UserCode"].ToString());
        Session.Clear();
        Session.RemoveAll();
        Session.Abandon();
        Response.Redirect("../Home.aspx");
    }
}