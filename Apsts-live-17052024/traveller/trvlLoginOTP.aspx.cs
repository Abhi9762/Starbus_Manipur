using Npgsql;
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

public partial class traveller_trvlLoginOTP : System.Web.UI.Page
{
    sbLoaderNdPopup popup = new sbLoaderNdPopup();
    NpgsqlCommand MyCommand;
    sbBLL bll = new sbBLL();
    sbValidation _SecurityCheck = new sbValidation();
    sbSecurity _security = new sbSecurity();
    private sbCommonFunc _common = new sbCommonFunc();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            checkMobile();
            offers();
            loadGenralData();
            RefreshCaptcha();
            Session["OTPverifycount"] = "0";

        }
    }
    #region "Methods"
    public void RefreshCaptcha()
    {
        tbcaptchacode.Text = "";
        Session["CaptchaImage"] = CaptchaText(); //CaptchaImage.GenerateRandomCode(CaptchaType.AlphaNumeric, 6);
    }
    public string CaptchaText()
    {
        string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string numbers = "1234567890";
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
    public string getRandom()
    {

        string numbers = "1234567890";

        string characters = numbers;
        characters += numbers;

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
    private void loadGenralData()//M1
    {
        try
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("../CommonData.xml"));
            XmlNodeList deptlogo = doc.GetElementsByTagName("dept_logo_url");
            if (deptlogo.Item(0).InnerXml != "")
            {
                ImgDepartmentLogo.ImageUrl = deptlogo.Item(0).InnerXml;
                ImgDepartmentLogo.Visible = true;
            }
            XmlNodeList dept_en = doc.GetElementsByTagName("dept_Abbr_en");
            lblDeptName.Text = dept_en.Item(0).InnerXml;
            XmlNodeList version = doc.GetElementsByTagName("Ver_Name");
            lblversion.Text = version.Item(0).InnerXml;
           
        }
        catch (Exception ex)
        {
            _common.ErrorLog("trvlLoginOTP.aspx-0001", ex.Message.ToString());
        }
    }
    private void checkMobile()
    {
        if (_security.isSessionExist(Session["_MobileNumber"]) == true)
        {
            checkMobileNo(Session["_MobileNumber"].ToString());
        }
        else
        {
            Response.Redirect("errorpage.aspx");
        }
    }
    private void Errormsg(string header, string msg)
    {
        string popupStr = popup.modalPopupSmall("D", header, msg, "Close");
        Response.Write(popupStr);
    }
    private void Successmsg(string header, string msg)
    {
        string popupStr = popup.modalPopupSmall("S", header, msg, "OK");
        Response.Write(popupStr);
    }
    private void checkMobileNo(string mobileNumber) //M1
    {
        try
        {
            ViewState["vsAlreadyReg"] = "N";
            wsClass obj = new wsClass();
            DataTable dt = new DataTable();
            dt = obj.traveller_dt(mobileNumber);
            if (dt.Rows.Count > 0)
            {
                string mobileNo = dt.Rows[0]["mobilenumber"].ToString();
                string userName = dt.Rows[0]["username"].ToString();
                string alreadyReg = dt.Rows[0]["already_yn"].ToString();
                ViewState["vsAlreadyReg"] = alreadyReg;
                if (alreadyReg == "Y")
                {
                    pnlName.Visible = false;
                }
            }
            else
            {
                Errormsg("Sorry", dt.TableName.ToString());
                return;
            }
            string submob = mobileNumber.Substring(7, 3);
            string mm = "XXXXXXX" + submob;
            lblMessage.Text = "Please enter the 6 digit OTP which has been sent to Mobile No. ( " + mm + " )";
        }
        catch (Exception ex)
        {
            _common.ErrorLog("trvlLoginOTP.aspx-0002", ex.Message.ToString());
        }
    }
    private void loginFirst(string mobileNo, string userName)//M2
    {
        try
        {

            string ipAddress = wsClass.getIPAddress();
            wsClass obj = new wsClass();
            DataTable dt = new DataTable();
            dt = obj.traveller_loginFirst_dt(mobileNo, userName, "W", ipAddress);
            if (dt.Rows.Count > 0)
            {
                CreateCookie(mobileNo);
            }
            else
            {
                Errormsg("Sorry", dt.TableName.ToString());
            }
        }
        catch (Exception ex)
        {
            Errormsg("ex", ex.Message.ToString());
            _common.ErrorLog("trvlLoginOTP.aspx-0003", ex.Message.ToString());
        }
    }
    private void loginSuccess(string mobileNo)//M3
    {
        try
        {
            string ipAddress = wsClass.getIPAddress();
            wsClass obj = new wsClass();
            DataTable dt = new DataTable();
            dt = obj.traveller_loginSuccess_dt(mobileNo, "W", ipAddress);
            if (dt.Rows.Count > 0)
            {
                CreateCookie(mobileNo);
            }
            else
            {
                Errormsg("Sorry", dt.TableName.ToString());
            }
        }
        catch (Exception ex)
        {
            Errormsg("", ex.Message.ToString());
            _common.ErrorLog("trvlLoginOTP.aspx-0004", ex.Message.ToString());
        }
    }
    private void loginFail(string mobileNo)//M4
    {
        try
        {
            string ipAddress = wsClass.getIPAddress();
            wsClass obj = new wsClass();
            DataTable dt = new DataTable();
            dt = obj.traveller_loginFail_dt(mobileNo, "W", ipAddress);
            if (dt.Rows.Count > 0)
            {
                RefreshCaptcha();
                Errormsg("Please Check", "You entered wrong OTP. Please enter valid OTP.");
            }
            else
            {
                Errormsg("Please Check", dt.TableName.ToString());
            }
        }
        catch (Exception ex)
        {
            Errormsg("", ex.Message.ToString());
            _common.ErrorLog("trvlLoginOTP.aspx-0005", ex.Message.ToString());
        }
    }
    private void CreateCookie(string mobileNo)
    {
        try
        {
            Session["_UserCode"] = mobileNo;
            System.Security.Cryptography.MD5CryptoServiceProvider SecMD5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            Random _rndmNo = new Random();

            FormsAuthentication.Initialize();
            FormsAuthenticationTicket tkt;
            string cookiestr;
            HttpCookie ck;

            tkt = new FormsAuthenticationTicket(1, "etktFormsAspx", DateTime.Now, DateTime.Now.AddSeconds(5), false, mobileNo); // , lstDistrict.SelectedValue)
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
            FormsAuthenticationTicket a = new FormsAuthenticationTicket(1, mobileNo, DateTime.Now, DateTime.Now.AddMinutes(20), false, mobileNo, FormsAuthentication.FormsCookiePath);
            HttpCookie ck1 = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(a));
            Response.Cookies.Add(ck1);
            Session["_eTktTicketID"] = ck1.Value;
            if (Session["calledFrom"] == null)
            {
                Session["calledFrom"] = "L";
            }
            Response.Redirect("trvlrRedirection.aspx", false);
        }
        catch (Exception ex)
        {
            _common.ErrorLog("trvlLoginOTP.aspx-0006", ex.Message.ToString());
            Errormsg("", "Unable to Create Cookie");
            return;
        }
    }
    public void offers()
    {
        try
        {
            sbOffers obj = new sbOffers();
            string date = DateTime.Now.Date.ToString("dd/MM/yyyy");
            DataTable dtOffer = obj.getOffers(date, "1", "L");
            pnlOffers.Visible = false;
            pnlNoOffer.Visible = true;
            if (dtOffer.Rows.Count > 0)
            {
                string imgUrl = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + "/starbusarn_v1/DBimg/offers/" + dtOffer.Rows[0]["couponid"].ToString() + "_W.png";
                imgOffer.ImageUrl = imgUrl;// GetImage((byte[])dtOffer.Rows[0]["webimg"]);
                pnlOffers.Visible = true;
                pnlNoOffer.Visible = false;
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("trvlLoginOTP.aspx-0007", ex.Message.ToString());
        }

    }
    public string GetImage(object img)
    {
        try
        {
            return "data:image/jpg;base64," + Convert.ToBase64String((byte[])img);
        }
        catch (Exception ex)
        {
            _common.ErrorLog("trvlLoginOTP.aspx-0008", ex.Message.ToString());
            return null;
        }
    }
    #endregion

    #region "Events"
    protected void imgOffer_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("../offersAll.aspx");
    }
    protected void lbtnResendOTP_Click(object sender, EventArgs e)
    {
        int resendCount = int.Parse(hfResendCount.Value.ToString());
        if (resendCount >= 2)
        {
            Errormsg("Please Check", "You have been already ");
        }
        else
        {
            resendCount++;
            hfResendCount.Value = resendCount.ToString();
        }
        RefreshCaptcha();
        // Resend OTP Code here . .  . .
        CommonSMSnEmail sms = new CommonSMSnEmail();
        string otp = getRandom();
        sms.sendOtp(otp, Session["_MobileNumber"].ToString());
        Session["_Otp"] = otp;
        RefreshCaptcha();
    }
    protected void lbtnVerify_Click(object sender, EventArgs e)
    {
        string otp = tbOTP.Text;
        string userName = tbName.Text;
        if (_SecurityCheck.IsValidInteger(otp, 6, 6) == false)
        {
            RefreshCaptcha();
            Errormsg("Please Check", "Enter Valid 6 Digit OTP.");
            return;
        }

        if (tbcaptchacode.Text.Length < 6)
        {
            RefreshCaptcha();
            Errormsg("Please Check", "Enter Valid Security Code(Shown in Image).");
            return;
        }
        if (!(Session["CaptchaImage"] != null && tbcaptchacode.Text.ToLower() == Session["CaptchaImage"].ToString().ToLower()))
        {
            RefreshCaptcha();
            tbcaptchacode.Text = "";
            Errormsg("Please Check", "Invalid Security Code(Shown in Image).");
            return;
        }


        checkMobile();
        string mobileno = Session["_MobileNumber"].ToString();

        // Check OTP
        if (otp == Session["_Otp"].ToString())
        {
            if (ViewState["vsAlreadyReg"].ToString() == "Y")
            {
                loginSuccess(mobileno);
            }
            else
            {
                if (userName.Length < 2)
                {
                    string popupStr = popup.modalPopupSmall("D", "Please Check", "Enter Valid Name", "OK");
                    Response.Write(popupStr);
                    return;
                }
                loginFirst(mobileno, userName);
            }
        }
        else
        {
            if (Convert.ToInt32(Session["OTPverifycount"]) < 2)
            {
                Session["OTPverifycount"] = Convert.ToInt32(Session["OTPverifycount"]) + 1;
                loginFail(mobileno);
            }
            else
            {
                lblMsg.Text = "OTP Limit Exceed";
                mpOtpLimit.Show();
            }
        }
    }
    protected void lbtnRefresh_Click(object sender, EventArgs e)
    {
        RefreshCaptcha();
    }
    protected void lbtnRedirect_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Home.aspx");
    }
    #endregion



}