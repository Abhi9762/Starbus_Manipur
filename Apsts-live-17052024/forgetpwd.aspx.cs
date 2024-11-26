using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class forgetpwd : System.Web.UI.Page
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
        if (!IsPostBack)
        {
            RefreshCaptcha();
            loaddata();
            //pnlDetails.Visible = false;
           // pnlOtp.Visible = true;
            regenerateSeed();
               lbtnchangepass.Attributes.Add("onclick", "javascript:return setfocusSHA256('" + Session["cmrfseed"].ToString() + "');");
            


            resetcontrol();

        }
    }

    private void loaddata()
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(Server.MapPath("CommonData.xml"));
        XmlNodeList deptlogo = doc.GetElementsByTagName("dept_logo_url");
        XmlNodeList lbldept = doc.GetElementsByTagName("managed_by");
        lbldeptname.Text = lbldept.Item(0).InnerXml;
        XmlNodeList lbldeveloped = doc.GetElementsByTagName("developed_by");
        lbldevelopedby.Text = lbldeveloped.Item(0).InnerXml;
        if (deptlogo.Item(0).InnerXml != "")
        {
            ImgDepartmentLogo.ImageUrl = deptlogo.Item(0).InnerXml;
        }

    }

    #region"Methods"
    void regenerateSeed()
    {
        Random randomclass1 = new Random();
        Session["cmrfseed"] = randomclass1.Next().ToString();
        lbtnchangepass.Attributes.Add("onclick", "javascript:return setfocusSHA256('" + Session["cmrfseed"].ToString() + "');");
    }
    public void RefreshCaptcha()
    {
        tbcaptchacode.Text = "";
        Session["CaptchaImage"] = getRandom(); //CaptchaImage.GenerateRandomCode(CaptchaType.AlphaNumeric, 6);
    }
    public string getRandom()
    {
        Random random = new Random();
        const string src = "0123456789";
        int i;
        string _random = "";
        for (i = 0; i <= 5; i++)
        {
            _random += src[random.Next(0, src.Length)];//random.Next(0, 9).ToString();
        }

        return _random;
    }
    private void Errormsg(string msg)
    {
        string popup = _popup.modalPopupSmall("W", "Information", msg, "Close");
        Response.Write(popup);
        RefreshCaptcha();
    }
    private void Successmsg(string msg)
    {
        string popup = _popup.modalPopupSmall("S", "Success", msg, "Close");
        Response.Write(popup);
    }
    private void checkUser()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            DataTable dt;
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.checkuser");
            MyCommand.Parameters.AddWithValue("sp_userid", tbuserid.Text);
            MyCommand.Parameters.AddWithValue("sp_mobile", tbmobile.Text);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows[0]["sp_status"].ToString() == "Y")
                {
                    pnlDetails.Visible = false;
                    Session["_otp"] = "123456";//getRandom();
                    Session["_puserid"] = tbuserid.Text;
                    Successmsg("Otp has been sent to your mobile number.");
                    pnlOtp.Visible = true;
                }
                else
                {
                    Errormsg("Invalid Userid And Mobile");
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
            if (tbotp.Text.ToUpper() != Session["_otp"].ToString().ToUpper())
            {
                Errormsg("Invalid Otp");
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
    private void ChangePassword()
    {
        try
        {
            string PasswordForEmp = tbconfirmpass.Text;
            string PASSWORD = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(PasswordForEmp, "SHA1");
            string IpAddress = HttpContext.Current.Request.UserHostAddress;
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
                    pnlDetails.Visible = true;
                    Session.Remove("_otp");
                    Session.Remove("_puserid");
                    Successmsg("Password changed Successfully");
                    pnlOtp.Visible = false;
                    resetcontrol();
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
    private void resetcontrol()
    {
        tbcaptchacode.Text = "";
        tbconfirmpass.Text = "";
        tbmobile.Text = "";
        tbotp.Text = "";
        tbpasss.Text = "";
        tbuserid.Text = "";
    }
    #endregion

    #region"Events"
    protected void lbtnRefresh_Click(object sender, EventArgs e)
    {
        RefreshCaptcha();
    }
    protected void Continue_Click(object sender, EventArgs e)
    {
        if (_validation.IsValidString(tbuserid.Text, 6, 20) == false)
        {
            Errormsg("Invalid User ID");
            return;
        }
        if (_validation.isValidMobileNumber(tbmobile.Text) == false)
        {
            Errormsg("Invalid Mobile No.");
            return;
        }
        if (_validation.IsValidString(tbcaptchacode.Text, 6, 6) == false)
        {
            Errormsg("Invalid Captacha Code");
            return;
        }
        if (!(Session["CaptchaImage"] != null && tbcaptchacode.Text.ToLower() == Session["CaptchaImage"].ToString().ToLower()))
        {

            Errormsg("Invalid Security Code(Shown in Image). Please Try Again");
            return;
        }
        checkUser();
    }
    protected void lbtnchangepass_Click(object sender, EventArgs e)
    {
        if (validvalue() == false)
        {
            return;
        }
       
        ChangePassword();
    }


    protected void lbtnreset_Click(object sender, EventArgs e)
    {

        tbcaptchacode.Text = "";
        tbuserid.Text = "";
        tbmobile.Text = "";

    }
    #endregion






}