using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pass_reciept : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["currtranrefno"] != null || !string.IsNullOrEmpty(Session["currtranrefno"].ToString()))
        {
            try
            {
                BusPass dd = new BusPass();
                string ss = dd.GetReceiptHTMLOutside(Session["currtranrefno"].ToString());

                if (ss.Trim().Length > 0)
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
                Response.Write(ex.Message);
                panelError.Visible = true;
            }
        }
        else
        {
            panelError.Visible = true;
        }

    }
}