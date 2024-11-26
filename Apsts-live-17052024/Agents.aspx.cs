using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Agents : outsidebasepage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (sbXMLdata.checkModuleCategory("70") == false)
            {
                Response.Redirect("Errorpage.aspx");
            }
            Session["heading"] = "Authorised Agents";
        }
    }
}