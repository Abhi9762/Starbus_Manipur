using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AgentRegistrationForm : outsidebasepage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["Heading"] = "Become Authorized Agent";
        Session["Heading1"] = "You can register, as an agent to become part of APSTS family";
        if (!IsPostBack)
        {
            if (sbXMLdata.checkModuleCategory("70") == false)
            {
                Response.Redirect("Errorpage.aspx");
            }
        }
    }

    protected void lbtnProceed_Click(object sender, EventArgs e)
    {
        if (!IsValidValues())
        {
            return;
        }
        Response.Redirect("AgentRegistration.aspx");
    }

    private void errormsg(string errorMessage)
    {
        lblerrmsg.Text = errorMessage;
        mpError.Show();
    }
    private bool IsValidValues()
    {
        try
        {
            if (chkTOC.Checked == false)
            {
                errormsg("Please Check Terms & Condition");
                return false;
            }
            return true;

        }
        catch (Exception)
        {
            errormsg("Please check values Errorcode AgentRegistration");
            return false;
        }

    }
}