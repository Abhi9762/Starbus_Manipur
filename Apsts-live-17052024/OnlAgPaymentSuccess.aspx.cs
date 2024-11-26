using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class OnlAgPaymentSuccess : System.Web.UI.Page
{
    sbValidation _SecurityCheck = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["Heading"] = "Agent Application";
        Session["Heading1"] = "You can register, as an agent to become part of APSTS family";

        if (Session["_UserName"] == null || Session["_UserName"].ToString() == "")
        {
            Session["_ErrorMsg"] = "";
            Response.Redirect("errorpage.aspx");
        }

        AgentVerification();
        lblName.Text = Session["_UserName"].ToString();

    }


    public void AgentVerification()
    {
        try
        {
            string IPAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(IPAddress))
            {
                IPAddress = Request.ServerVariables["REMOTE_ADDR"];
            }

            string PasswordForDB = RandomString(6);
            string PASSWORD = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(PasswordForDB, "SHA1");

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "agent.ONLAGENTVERIFICATION");
            MyCommand.Parameters.AddWithValue("@p_refno", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("@p_verifyby", Session["transrefno"].ToString());
            MyCommand.Parameters.AddWithValue("@p_ipaddress", IPAddress);
            MyCommand.Parameters.AddWithValue("@p_pwd", PASSWORD);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {

                    //comm.sendDeptNewUserConfirmation_SMS(Session["_UserName"].ToString(), dt.Rows[0]["AgCode"].ToString(), PasswordForDB, Session["MobileNo"].ToString(), 13);
                    //comm.sendDeptNewUserConfirmation_EMAIL(dt.Rows[0]["AgCode"].ToString(), PasswordForDB, Session["Emailid"].ToString());
                }
            }
        }
        catch (Exception ex)
        {
            
            return; 
        }


    }

    public string RandomString(int length)
    {
        Random random = new Random();
        char[] charOutput = new char[length];

        for (int i = 0; i < length; i++)
        {
            int selector = random.Next(65, 122);

            if (selector > 90 && selector < 97)
            {
                selector += 10;
            }
            else if (selector > 110 && selector < 121)
            {
                selector = 64;
            }

            charOutput[i] = (char)selector;
        }

    
        return new string(charOutput);
    }



    protected void btnprint_Click(object sender, EventArgs e)

    {
        Session["_RNDIDENTIFIERCTZAUTHC"] = _SecurityCheck.GeneratePassword(10, 5);
        openSubDetailsWindow("OnlAgReciept.aspx");
    }

    private void openSubDetailsWindow(string MModuleName)
    {
        try
        {
            string murl = MModuleName;

            if (Request.Browser.Type.ToUpper().Trim().StartsWith("IE"))
            {
                Response.Write("<script language='javascript'>window.showModalDialog('" + murl + "','name','dialogWidth:830px;dialogHeight:900px');</script>");
            }
            else
            {
                string fullURL = "window.open('" + murl + "', '_blank', 'height=900,width=830,status=no,toolbar=no,menubar=no,location=no,scrollbars=no,resizable=no,titlebar=no' );";
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
}