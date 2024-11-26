using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class E_cancellationvoucher : System.Web.UI.Page
{
    sbSecurity _security = new sbSecurity();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    string html = "";
    protected void Page_Load(object sender, EventArgs e)
    {
       // Session["Cancel_voucher"] = "1F12002202220001_2";

        if (Session["Cancel_voucher"] != null || Session["Cancel_voucher"] != "")
        {
            try
            {
                sbCancellationVoucher dd = new sbCancellationVoucher();
                string ss = dd.GetCancellationVoucher(Session["Cancel_voucher"].ToString());
                if (ss.Trim().Length > 0)
                {
                    panelError.Visible = false;
                    Response.Write(ss);
                }
                else
                    panelError.Visible = true;
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