using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_AGPmtError : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblerror.Text = Session["_ErrorMsg"].ToString();
    }


    protected void btnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("AgentDashboard.aspx");
    }
}