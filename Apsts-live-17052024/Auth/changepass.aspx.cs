using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Npgsql;
using System.Data;

public partial class Auth_changepass : System.Web.UI.Page
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    protected void Page_Load(object sender, EventArgs e)
    {
        // Session["_UserCode"] = "STR0025";
        if (_security.isSessionExist(Session["_UserCode"]) == true)
        {
            Session["_UserCode"] = Session["_UserCode"];            
        }
        else
        {
            Response.Redirect("sessionTimeout.aspx");
        }
        

    }

    #region"Methods"
    
    private void Errormsg(string msg)
    {
        string popup = _popup.modalPopupSmall("W", "Information", msg, "Close");
        Response.Write(popup);

    }
    private void Successmsg(string msg)
    {
        string popup = _popup.modalPopupSmall("S", "Success", msg, "Close");
        Response.Write(popup);
    }
    private void ChangePassword()
    {
        try
        {
            string OLDPASSWORD = hdoldpass.Value.ToString();
            string PASSWORD = hdconfirmnewpass.Value.ToString();
            string IpAddress = HttpContext.Current.Request.UserHostAddress;

            if (OLDPASSWORD == PASSWORD)
            {
                Errormsg("Old And New Password cannot be same.");
                return;
            }

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            DataTable dt;
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.fn_change_user_pwd");
            MyCommand.Parameters.AddWithValue("sp_userid", Session["_UserCode"]);
            MyCommand.Parameters.AddWithValue("sp_pass", PASSWORD);
            MyCommand.Parameters.AddWithValue("sp_ipaddress", IpAddress);
            MyCommand.Parameters.AddWithValue("sp_updatedby", Session["_UserCode"]);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows[0]["sp_status"].ToString() == "Y")
                {
                    Response.Redirect("changedpasswordsuccess.aspx");
                }
                else if (dt.Rows[0]["sp_status"].ToString() == "USED PASS")
                {
                    Errormsg("You have entered previously used password, please enter new password");
                    return;
                }
                else
                {
                    Errormsg("Something Went Wrong");
                    return;
                }
            }
            else
            {
                Errormsg("Something Went Wrong");
            }
        }
        catch (Exception ex)
        {
            Errormsg("Something Went Wrong");
        }
    }
    private bool validvalue()
    {
        try
        {
            if (_security.isSessionExist(Session["_UserCode"]) == false)
            {
                Errormsg("Invalid User");
                return false;
            }
           
            return true;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("Login-M3", ex.Message.ToString());
            return false;
        }
    }
    private void checkuser()
    {
        try
        {
            string PASSWORD = hdoldpass.Value.ToString();


            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            DataTable dt;
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.checkuser_pass");
            MyCommand.Parameters.AddWithValue("sp_userid", Session["_UserCode"]);
            MyCommand.Parameters.AddWithValue("sp_oldpwd", PASSWORD);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows[0]["sp_status"].ToString() == "Y")
                {
                    ChangePassword();
                }
                else
                {
                    Errormsg("Invalid Old Password");
                    return;
                }
            }
            else
            {
                Errormsg("Something Went Wrong");
            }
        }
        catch (Exception ex)
        {
            Errormsg("Something Went Wrong");
        }
    }


    #endregion

    #region"Events"
    protected void lbtnChangePwd_Click(object sender, EventArgs e)
    {
        if (validvalue() == false)
        {
            return;
        }
        checkuser();
    }
    #endregion
}