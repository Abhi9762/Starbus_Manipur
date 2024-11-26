using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CancelBusTicket : System.Web.UI.Page
{
    private sbCommonFunc _common = new sbCommonFunc();
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["heading"] = "Cancel Bus Ticket";
        lblcancellationpolicy.Text = _common.getCancellationpolicy();
        loadGenralData();
    }

    protected void lbtnlogin_Click(object sender, EventArgs e)
    {
        Response.Redirect("traveller/trvlLogin.aspx");
    }
    private void loadGenralData()//M1
    {
        try
        {
          
            sbXMLdata obj = new sbXMLdata();
            this.Title = obj.loadtitle();
          
            lblemail.Text = obj.loadEmail();
            lblcontact.Text = obj.loadContact();
            




            
        }
        catch (Exception ex)
        {
           
            _common.ErrorLog("CancelTicket-M1", ex.Message.ToString());
           // Errormsg(ex.Message.ToString());
        }
    }
}