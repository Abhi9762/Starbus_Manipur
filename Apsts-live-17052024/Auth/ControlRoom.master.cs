using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_ControlRoom : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            lblHeaderText.Text= Session["MasterPageHeaderText"].ToString(); ;
            //lblModuleName.Text = Session["_moduleName"].ToString();
            //lblHeaderText.Text = Session["MasterPageHeaderText"].ToString();
        }
    }

    protected void lbtnCatalogue_ServerClick(object sender, EventArgs e)
    {

    }

    protected void lbtnlogout_ServerClick(object sender, EventArgs e)
    {

    }

    //protected void lbtnLogout_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("Logout.aspx");
    //}
    protected void lbtnLogout_Click(object sender, EventArgs e)
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
    protected void lbtnHome_Click(object sender, EventArgs e)
    {
        Response.Redirect("ControlRoomDash.aspx");
    }



    protected void lbtnChangePass_Click(object sender, EventArgs e)
    {
        mpGrievance.Show();
    }
}
