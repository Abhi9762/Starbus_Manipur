using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class E_ticket : System.Web.UI.Page
{
    sbSecurity _security = new sbSecurity();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    string html = "";
    protected void Page_Load(object sender, EventArgs e)//M1
    {
        //Session["p_ticketNo"] = "1F12803202210004";
        

        if (Session["p_ticketNo"] != null || Session["p_ticketNo"] != "")
        {
            try
            {
                sbTicket dd = new sbTicket();
                string ss = dd.GetTicket(Session["p_ticketNo"].ToString());
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
                panelError.Visible = true;
            }
        }
        else
        {
            panelError.Visible = true;
        }        
    }


}