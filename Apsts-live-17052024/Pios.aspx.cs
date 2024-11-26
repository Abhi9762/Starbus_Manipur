using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pios : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["heading"] = "PIOS-APIOS";
        loadGenralData();
    }
    private void loadGenralData()//M1
    {
        try
        {
            sbXMLdata obj = new sbXMLdata();
            if (obj.loadpios() == "")
            {
                empios.Visible = false;
                imgpios.Visible = true;
            }
            else
            {
                empios.Src = "manuals/" + obj.loadpios();
                empios.Visible = true;
                imgpios.Visible = false;
            }
               
        }
        catch (Exception ex)
        {

        }
    }
}