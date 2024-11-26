using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class pathikwebpage_ticketstatus : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string ticket = Request.QueryString["tk"] == null/* TODO Change to default(_) if this is not a reference type */? "" : Request.QueryString["tk"];
            if (ticket.Length <= 12)
            {
                Session["errorMessage"] = "Invalid Ticket. If you face this problem again and again feel free to helpdesk.";
                Response.Redirect("error.aspx");
                return;
            }
            lbltktno.Text = ticket;
        }
    }
}