using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Bus_Pass : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        //Session["Passno"] = "YA2505000024";
        if (Session["Passno"] != null || !string.IsNullOrEmpty(Session["Passno"] as string))
        {
            try
            {
                BusPass dd = new BusPass();
                string ss = dd.GetPassHTMLOutside(Session["Passno"] as string);
                if (!string.IsNullOrEmpty(ss.Trim()))
                {
                    panelError.Visible = false;
                    Response.Write(ss);
                }
                else
                {
                    panelError.Visible = true;
                }
            }
            catch (Exception ex)
            {
                // Response.Write(ex.Message);
                panelError.Visible = true;
            }
        }
        else
        {
            // Response.Write("Error");
            panelError.Visible = true;
        }

    }
}