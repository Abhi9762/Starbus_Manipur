using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PassStatus : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["Heading"] = "Status Pass Request";
            Session["Heading"] = "Status Pass Request";
            lblrefno.Text = Session["currtranrefno"].ToString();
            if (Session["IssuanceType"].ToString() == "I")
            {
                btnprintpass.Visible = true;
                btnprintrecipt.Visible = false;
                //comm.BusPass_Approved_SMS(Session["currtranrefno"].ToString(), Session["MobileNo"], 22);
            }
            else
            {
                btnprintpass.Visible = false;
                btnprintrecipt.Visible = true;
                //comm.BusPass_Accept_SMS("", Session["currtranrefno"].ToString(), Session["MobileNo"], 21);
            }
        }

    }

    public void OpenSubDetailsWindow(string MModuleName)
    {
        try
        {
            string murl = MModuleName + "?rt=" + DateTime.Now.ToString();

            if (Request.Browser.Type.ToUpper().Trim().Substring(0, 2) == "IE")
            {
                Response.Write("<script language='javascript'>window.showModalDialog('" + murl + "','name','dialogWidth:100px;dialogHeight:100px');</script>");
            }
            else
            {
                string fullURL = "window.open('" + murl + "', '_blank', 'height=520,width=520,status=no,toolbar=no,menubar=no,location=no,scrollbars=no,resizable=no,titlebar=no' );";
                string script = "window.open('" + fullURL + "','')";

                if (!ClientScript.IsClientScriptBlockRegistered("NewWindow"))
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "NewWindow", fullURL, true);
                }
            }
        }
        catch (Exception ex)
        {
            
        }
    }

    protected void btnprintpass_Click(object sender, EventArgs e)
    {
        mpPass.Show();
    }

    protected void btnprintrecipt_Click(object sender, EventArgs e)
    {
      mppassreceipt.Show();
    }
}