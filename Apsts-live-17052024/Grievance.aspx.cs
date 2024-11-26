using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;


public partial class Grievance : System.Web.UI.Page
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    sbValidation _SecurityCheck = new sbValidation();
    sbSummary obj = new sbSummary();
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["Heading"] = "Grievance";
        loadGenralData();
    }
 protected void lbtnRegister_Click(object sender, EventArgs e)
    {
        Session["calledFrom"] = "G";
        Response.Redirect("traveller/trvlLogin.aspx", false);
    }
    private void loadGenralData()//M1
    {
        try
        {

            sbXMLdata obj = new sbXMLdata();
            this.Title = obj.loadtitle();

           

            DataTable dt = new DataTable();
            dt = _common.getofficecontact(10);
            lbladdress.Text = "-NA-";

             lbladdress.Text = dt.Rows[0]["adrs"].ToString() + ", " + dt.Rows[0]["stname"].ToString() + ", " + dt.Rows[0]["distname"].ToString() + ", India";
              
                lblemail.Text = dt.Rows[0]["eml"].ToString().Replace("@", "[at]").Replace(".", "[dot]");





        }
        catch (Exception ex)
        {

            _common.ErrorLog("CancelTicket-M1", ex.Message.ToString());
            // Errormsg(ex.Message.ToString());
        }
    }
}