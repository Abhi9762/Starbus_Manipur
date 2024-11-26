using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_AGAuthPrintReceipt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblJourney.Text = "Your receipt For Online payment is " + Session["transrefno"];
    }

    public void openSubDetailsWindow(string MModuleName)
    {
        try
        {
            string murl = MModuleName;

            if (Request.Browser.Type.ToUpper().Trim().StartsWith("IE"))
            {
                Response.Write("<SCRIPT language='javascript'>window.showModalDialog('" + murl + "','name','dialogWidth:830px;dialogHeight:900px');</script>");
            }
            else
            {
                string fullURL = "window.open('" + murl + "', '_blank', 'height=900,width=830,status=no,toolbar=no,menubar=no,location=no,scrollbars=no,resizable=no,titlebar=no' );";
                string script = "window.open('" + fullURL + "','')";
                if (!ClientScript.IsClientScriptBlockRegistered("NewWindow"))
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "NewWindow", fullURL, true);
                }
            }
        }
        catch (Exception ex)
        {
            Session["_ErrorMsg"] = "Something went wrong. <br/>Please contact the software admin with errorcode CntrPassCnfrm05-";
            Response.Redirect("errorpage.aspx");
        }
    }

    protected void lbtnprntreceipt_Click(object sender, EventArgs e)
    {
        dvprint.Visible = false;
        dvprntYN.Visible = true;
        //Session("_RNDIDENTIFIERAGENTAUTH") = _SecurityCheck.GeneratePassword(10, 5);
        openSubDetailsWindow("AGe_reciept.aspx");
    }

    protected void lbtnno_Click(object sender, EventArgs e)
    {
        dvprint.Visible = true;
        dvprntYN.Visible = false;
    }

    protected void lbtnyes_Click(object sender, EventArgs e)
    {
        Response.Redirect("AgentRechrgeTopup.aspx");
    }
}