using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class traveller_trvlrRedirection : System.Web.UI.Page
{
 sbSecurity _security = new sbSecurity();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            checkUser();
            if (Session["calledFrom"] == null)
            {
                Response.Redirect("errorpage.aspx");
            }
  Session["_RoleCode"] = "102";
            Session["_RNDIDENTIFIERSTRVL"] = _security.GeneratePassword(10, 5);
            string calledFrom = Session["calledFrom"].ToString();
            switch (calledFrom)
            {
                case "L":
                    {
                        checkForRating(Session["_UserCode"].ToString());
                        break;
                    }
                case "B":
                    {
                        Response.Redirect("seatPayment.aspx");
                        break;
                    }
                case "G":
                    {
                        Response.Redirect("grievance.aspx");
                        break;
                    }
                default:
                    {
                        Response.Redirect("errorpage.aspx");
                        break;
                    }
            }
        }
    }
    private void checkUser()
    {
        if (Session["_UserCode"] == null)
            Response.Redirect("bkngError.aspx", true);
        else
            Session["_UserCode"] = Session["_UserCode"];
    }

    private void checkForRating(string userId)
    {
        try
        {
            wsClass obj = new wsClass();
            DataTable dt = obj.getRatingTickets(userId);
            if (dt.Rows.Count > 0)
            {
                Response.Redirect("Rating.aspx");
            }
            else
            {
                Response.Redirect("dashboard.aspx");
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("dashboard.aspx");
        }
    }

}