using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Logout : BasePage
{

    protected void Page_Load(object sender, EventArgs e)
    {
        Session.Clear();
        Session.RemoveAll();
        Session.Abandon();
    }

    protected void lbtnvisitagain_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Home.aspx");
    }
}