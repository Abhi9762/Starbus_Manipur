using CaptchaDLL;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class Login : System.Web.UI.Page
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    wsClass obj = new wsClass();
    protected void Page_Init(object sender, System.EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.AddHeader("Cache-control", "no-store, must-revalidate,private,no-cache");
        Response.AddHeader("PRAGMA", "NO-Cache");
        Response.Expires = -1;
        Response.Expires = 0;
        Response.Cache.SetNoStore();
        Response.Cache.SetExpires(DateTime.Now.AddDays(-2));
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)

        {
            tbuserid.Focus();
            generateSeed();
            Random randomclass = new Random();
            hidtoken.Value = randomclass.Next().ToString();
            loadGenralData();
            RefreshCaptcha();
            //lbtnlogin.Attributes.Add("onclick", "javascript:setfocusSHA1(" + Session["Authseed"] + ");");
        }
    }



    #region "Method"
    private void generateSeed()
    {
        Random _rndmNo = new Random();
        Session["Authseed"] = _rndmNo.Next();
        hidSeed.Value = Session["Authseed"].ToString();
    }
    private void Errormsg(string msg)
    {
        string popup = _popup.modalPopupSmall("W", "Information", msg, "Close");
        Response.Write(popup);
        RefreshCaptcha();
    }
    private void loadGenralData()//M1
    {
        int count = 0;

        sbXMLdata obj = new sbXMLdata();
        this.Title = obj.loadtitle();

    }
    public void RefreshCaptcha()
    {
        tbcaptchacode.Text = "";
        Session["CaptchaImage"] = CaptchaText(); //CaptchaImage.GenerateRandomCode(CaptchaType.AlphaNumeric, 6);
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
    public string CaptchaText()
    {
        string alphabets = "ABCDEFGHIJKLMNPQRSTUVWXYZ";
        string numbers = "123456789";
        string characters = numbers;
        characters += alphabets + numbers;
        int length = 6;
        string otp = string.Empty;
        for (int i = 0; i < length; i++)
        {
            string character = string.Empty;
            do
            {
                int index = new Random().Next(0, characters.Length);
                character = characters.ToCharArray()[index].ToString();
            } while (otp.IndexOf(character) != -1);
            otp += character;
        }
        return otp;
    }
    private bool validvalue()
    {
        try
        {
            //if (!(Session["CaptchaImage"] != null && tbcaptchacode.Text.ToLower() == Session["CaptchaImage"].ToString().ToLower()))
            //{
            //    generateSeed();
            //    Errormsg("Invalid Security Code(Shown in Image). Please Try Again");
            //    return false;
            //}
            if (_validation.IsValidString(tbuserid.Text.Trim().ToString(), 1, 25) == false)
            {
                generateSeed();
                Errormsg("Invalid User ID/Password");
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
    private void loadDeptUserDetails(string mloginID)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_UserDetails");
            MyCommand.Parameters.AddWithValue("p_userid", mloginID);
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToInt16(dt.Rows[0]["uattempt"].ToString()) == 5)
                {
                    generateSeed();
                    Errormsg("Sorry your account is locked for day. Please try again tommorow or contact helpdesk.");
                    return;
                }
                if (checkPassword(dt.Rows[0]["upwd"].ToString()) == true)
                {
                    Session["_UserCode"] = mloginID;

if (dt.Rows[0]["change_pass_yn"].ToString() == "N")
                    {
                        Session["_Passwdstatus"] = "F";
                        Response.Redirect("auth/changepassfirsttime.aspx", false);
                        return;
                    }

                    if (dt.Rows[0]["passchange_days"].ToString() == "Y")
                    {
                        Session["_Passwdstatus"] = "E";
                        Response.Redirect("auth/changepassfirsttime.aspx", false);
                        return;
                    }

                    Session["uofclvlid"] = dt.Rows[0]["uofclvlid"].ToString();
                    string rolecode = dt.Rows[0]["urole"].ToString();
                    if (rolecode == "4")
                    {
                        if (dt.Rows[0]["userlogintype"].ToString() == "E")
                        {
                            generateSeed();
                            Errormsg("You are not eligible to login on the web. You can login only the ETM machine.");
                            tbcaptchacode.Text = "";
                            RefreshCaptcha();
                            return;
                        }
                    }
                    if (rolecode == "14")
                    {
                        //Bus Pass
                        if (sbXMLdata.checkModuleCategory("71") == false)
                        {
                            Errormsg("BusPass Admin Role Not Started Yet, Please Contact Helpdesk");
                            return;
                        }
                    }
                    if (rolecode == "19")
                    {
                        //Agent & CSC
                        
                        if (agentvalidation() == 0)
                        {
                            if (Session["_Agtype"].ToString() == "1")
                            {
                                if (sbXMLdata.checkModuleCategory("70") == false)
                                {
                                    Errormsg("Agent Role Not Started Yet, Please Contact Helpdesk");
                                    return;
                                }
                            }
                            else if (Session["_Agtype"].ToString() == "3" || Session["_Agtype"].ToString() == "4")
                            {
                                if (Session["_CSCMainAg"].ToString() != "0")
                                {
                                    if (sbXMLdata.checkModuleCategory("72") == false)
                                    {
                                        Errormsg("CSC Role Not Started Yet, Please Contact Helpdesk");
                                        return;
                                    }
                                }
                                else
                                {
                                    if (sbXMLdata.checkModuleCategory("72") == false)
                                    {
                                        Errormsg("CSC Role Not Started Yet, Please Contact Helpdesk");
                                        return;
                                    }
                                }
                            }
                        }
                    }
                    if (UpdateLoginLog(mloginID) == true)
                    {
                        ChecksingleLogin(mloginID);
                    }
                    else
                    {
                        generateSeed();
                        Errormsg("Unable To update Login Log. Please contact helpdesk");
                        return;
                    }
                }
                else
                {
                    int attempt = Convert.ToInt16(dt.Rows[0]["uattempt"].ToString()) + 1;
                    Errormsg("Invalid User ID/Password(This is login attempt number " + attempt.ToString() + " of 5, your account will be locked on 5 unsuccessfull attempts.");
                    generateSeed();
                    if (UpdateUnAuthLoginLog(mloginID) == false)
                    {
                        Errormsg("Unable To update Login Log. Please contact helpdesk");
                        return;
                    }
                }
            }
            else
            {
                generateSeed();
                Errormsg("Invalid User ID/Password");
                return;
            }

        }
        catch (Exception ex)
        {
            generateSeed();
            _common.ErrorLog("Login-M4", ex.Message.ToString());
        }
    }
    private int agentvalidation()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "agent.getagentvalid");
            MyCommand.Parameters.AddWithValue("p_agentcode", Session["_UserCode"].ToString());
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                Session["_ValidTo"] = dt.Rows[0]["valid_to"];
                Session["_Agtype"] = dt.Rows[0]["agent_type"];
                Session["_CSCMainAg"] = dt.Rows[0]["csp_agent"];
                Session["_Username"] = dt.Rows[0]["agent_name"];
                return Convert.ToInt16(dt.Rows[0]["Sp_cnt"].ToString());
            }
            else
            {
                return 0;
            }
        }
        catch (Exception ex)
        {
            return 0;
        }
    }
    private void ChecksingleLogin(string MloginID)
    {
        try
        {
            if (_security.alreadyLogin(MloginID) == true)
            {
                lblConfirmation.Text = "User is already logged in, Do you want to continue here?";
                mpConfirmation.Show();
                RefreshCaptcha();
                return;
            }
            else
            {
                CreateCookie(MloginID);
            }
        }
        catch (Exception ex)
        {

        }
    }
    private bool checkPassword(string passworddatabase)
    {
        try
        {
            string passwordHash;
            passwordHash = Session["UsrPwd"].ToString();
            passworddatabase = hidSeed.Value.ToString() + passworddatabase.ToLower().ToString();
            passworddatabase = FormsAuthentication.HashPasswordForStoringInConfigFile(passworddatabase.ToLower().ToString(), "SHA1");
            return (0 == string.Compare(passworddatabase, passwordHash.ToUpper(), false));
        }
        catch (Exception ex)
        {
            _common.ErrorLog("Login-M5", ex.Message.ToString());
            return false;
        }
    }

    private bool UpdateLoginLog(string mloginID)
    {
        try
        {
            string ipaddress = HttpContext.Current.Request.UserHostAddress;
            string logid = obj.UpdateLoginLog(mloginID, ipaddress, "W", "", "", "");
            if (logid != "")
            {
                Session["_LogID"] = logid;
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("Login-M6", ex.Message.ToString());
            return false;
        }
    }
    private bool UpdateUnAuthLoginLog(string mloginID)
    {
        try
        {
            string ipaddress = HttpContext.Current.Request.UserHostAddress;
            bool result = obj.UpdateUnAuthLoginLog(mloginID, ipaddress, "W", "", "", "");
            return result;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("Login-M7", ex.Message.ToString());
            return false;
        }
    }
    private void CreateCookie(string MloginID)
    {
        try
        {
            System.Security.Cryptography.MD5CryptoServiceProvider SecMD5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            Random _rndmNo = new Random();

            FormsAuthentication.Initialize();
            FormsAuthenticationTicket tkt;
            string cookiestr;
            HttpCookie ck;

            tkt = new FormsAuthenticationTicket(1, "etktFormsAspx", DateTime.Now, DateTime.Now.AddSeconds(5), false, MloginID); // , lstDistrict.SelectedValue)
            cookiestr = FormsAuthentication.Encrypt(tkt);
            ck = new HttpCookie(FormsAuthentication.FormsCookieName, cookiestr);
            ck.Path = FormsAuthentication.FormsCookiePath;
            Response.Cookies.Remove(FormsAuthentication.FormsCookieName);
            Response.Cookies.Add(ck);

            HttpCookie cookie = Request.Cookies[".etktFormsAspx"];
            Session["_eTktTicketID"] = cookie.Value;
            HttpCookie MRIIdentifierCookie = new HttpCookie("CRIIdentifier", BitConverter.ToString(SecMD5.ComputeHash(Encoding.ASCII.GetBytes(_rndmNo.Next().ToString()))));
            Response.Cookies.Add(MRIIdentifierCookie);

            FormsAuthentication.Initialize();
            FormsAuthenticationTicket a = new FormsAuthenticationTicket(1, MloginID, DateTime.Now, DateTime.Now.AddMinutes(20), false, MloginID, FormsAuthentication.FormsCookiePath);
            HttpCookie ck1 = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(a));
            Response.Cookies.Add(ck1);
            Session["_eTktTicketID"] = ck1.Value;
            Response.Redirect("Auth/DLDDefault.aspx", false);
        }
        catch (Exception ex)
        {
            _common.ErrorLog("Login-M8", ex.Message.ToString());
            Errormsg("Unable to Create Cookie");
            return;
        }
    }
    #endregion

    #region "Event"
    protected void lbtnRefresh_Click(object sender, EventArgs e)
    {
        RefreshCaptcha();
    }
    protected void lbtnlogin_Click(object sender, EventArgs e)
    {
        if (validvalue() == false)
        {
            return;
        }
        String MloginID = tbuserid.Text;
        Session["UsrPwd"] = hidHash.Value.ToString().Trim();
        hidHash.Value = "";
        loadDeptUserDetails(MloginID);
    }
    protected void lblforgetpass_Click(object sender, EventArgs e)
    {

    }
    protected void lbtnreset_Click(object sender, EventArgs e)
    {
        tbcaptchacode.Text = "";
        tbuserid.Text = "";
        tbpassword.Text = "";
    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        _security.RemoveUserLogin(Session["_UserCode"].ToString());
        ChecksingleLogin(Session["_UserCode"].ToString());
    }
    #endregion











}