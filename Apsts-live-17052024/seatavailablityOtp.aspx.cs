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

public partial class seatavailablityOtp : System.Web.UI.Page
{
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    private sbCommonFunc _common = new sbCommonFunc();
    NpgsqlCommand MyCommand;
    sbBLL bll = new sbBLL();
    sbValidation _SecurityCheck = new sbValidation();
    sbSecurity _security = new sbSecurity();

    DataTable dt = new DataTable();
    sbValidation _validation = new sbValidation();
    int intPassengerNo = 0;
    string commonerror = "There is some error. Please contact the administrator or try again after some time.";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            onLoad();
            loadGenralData(); 
        }
        if ( Session["MobileNo"].ToString() != "")
        {
            tbMobileNo.Text = Session["MobileNo"].ToString();
            tbMobileNo.Enabled = false;
            tbMobileNo.ReadOnly = true;
            tbMobileNo.BackColor = System.Drawing.Color.LightGray; //Drawing.Color.DarkGray;
            lblmsg.Text = "This is Concession ticket, Mobile number change not allowed";
        }
    }

    #region "Methods"
    private void loadGenralData()//M1
    {
        try
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("CommonData.xml"));
            XmlNodeList deptlogo = doc.GetElementsByTagName("dept_logo_url");
            if (deptlogo.Item(0).InnerXml != "")
            {
                ImgDepartmentLogo.ImageUrl = "Logo/" + deptlogo.Item(0).InnerXml;
                ImgDepartmentLogo.Visible = true;
            }
            XmlNodeList dept_en = doc.GetElementsByTagName("title_txt_en");
            lblDeptName.Text = dept_en.Item(0).InnerXml;
            XmlNodeList version = doc.GetElementsByTagName("Ver_Name");
            lblversion.Text = version.Item(0).InnerXml;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminGenInfo-M1", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
        }
    }
    public void onLoad()
    {
        try
        {
            if (_security.isSessionExist(Session["SearchParameters"]) == true)
            {
                Dictionary<string, string> dct = new Dictionary<string, string>();
                dct = (Dictionary<string, string>)Session["SearchParameters"];
                string dsvcid = dct["dsvcid"];
                string strpid = dct["strpid"];
                string depttime = dct["depttime"];
                string arrtime = dct["arrtime"];
                string tripdirection = dct["tripdirection"];
                string servicetypename = dct["servicetypename"];
                string totalfare = dct["totalfare"];
                string frstonid = dct["frstonid"];
                string tostonid = dct["tostonid"];
                string frstonName = dct["fromstation"];
                string tostonName = dct["tostation"];
                string date = dct["date"];
                string psngr = dct["passengers"];
                string boardingName = dct["boardingName"];

                lblServiceType.Text = servicetypename;
                lblFromStationName.Text = frstonName;
                lblToStationName.Text = tostonName;
                lblDateTime.Text = date + " " + depttime;
                lblBoarding.Text = boardingName;
                                
                DataTable dtPsngrs = new DataTable();
                dtPsngrs.Columns.Add("seatno");
                dtPsngrs.Columns.Add("travellername");
                dtPsngrs.Columns.Add("travellergender");
                dtPsngrs.Columns.Add("travellerage");
                string[] CustomerList = psngr.Split('|');
                foreach (string customer in CustomerList)
                {
                    string[] customerDetail = customer.Split(',');
                    string SeatNo = customerDetail[0];
                    string custName = customerDetail[1].ToUpper().Trim();
                    string custGender = customerDetail[2];
                    string custAge = customerDetail[3].Trim();

                    dtPsngrs.Rows.Add(SeatNo, custName, custGender, custAge);
                }
                lblFareAmt.Text = (double.Parse(totalfare)*double.Parse(dtPsngrs.Rows.Count.ToString())).ToString();
                if (dtPsngrs.Rows.Count > 0)
                {
                    gvPassengers.DataSource = dtPsngrs;
                    gvPassengers.DataBind();
                }
            }
            else
            {
                Response.Redirect("sessionTimeout.aspx");
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.ToString());
            _common.ErrorLog("trvlrSeatslct-M4", ex.ToString());
        }
    }
    
    private void SaveSeats(string userId, string Mobile, string email) //M4
    {
        try
        {
            if (_security.isSessionExist(Session["SearchParameters"]) == true)
            {
                Dictionary<string, string> dct = new Dictionary<string, string>();
                dct = (Dictionary<string, string>)Session["SearchParameters"];
                string IPAddress = HttpContext.Current.Request.UserHostAddress;

                string dsvcid = dct["dsvcid"];
                string strpid = dct["strpid"];
                string tripdirection = dct["tripdirection"];
                string frstonid = dct["frstonid"];
                string tostonid = dct["tostonid"];
                string date = dct["date"];
                string psngr = dct["passengers"];
                string boardingStations = dct["boardingId"];
                

                wsClass obj = new wsClass();
                DataTable dtp = obj.SaveSeats(dsvcid, tripdirection, strpid, date, frstonid, tostonid, "T", userId, Mobile, email, boardingStations, psngr, IPAddress,"W");

                if (dtp.TableName == "Success")
                {
                    string ticketNo = dtp.Rows[0]["p_ticketnumber"].ToString();
                    if (ticketNo == "ERROR" || ticketNo == "EXCEPTION")
                    {
                        Errormsg(commonerror);
                    }
                    else
                    {
                        Session["TicketNumber"] = ticketNo;
                        CreateCookie(Mobile);
                    }
                }
                else
                {
                    Errormsg(commonerror);
                }
            }
            else
            {
                Response.Redirect("sessionTimeout.aspx");
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.ToString());
            _common.ErrorLog("trvlrSeatslct-M4", ex.ToString());
        }
    }
    #endregion
    protected void lbtnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("home.aspx");
    }
    private void Errormsg(string msg)
    {
        string popup = _popup.modalPopupSmall("D", "Please Check", msg, "Close");
        Response.Write(popup);
    }

    #region "OTP Related"
    protected void lbtnProceed_Click(object sender, EventArgs e)
    {
        try
        {
            string userMobile = tbMobileNo.Text.Trim();
            string userEmail = tbEmailid.Text.Trim();

            if (_validation.IsValidInteger(userMobile, 10, 10) == false)
            {
                Errormsg("Please enter correct mobile number");
                return;
            }

            if (userEmail.Length > 0)
            {
                if (_validation.isValideMailID(userEmail) == false)
                {
                    Errormsg("Please enter valid email id ");
                    return;
                }
            }
            resetOTPPnl();
            RefreshCaptcha();
            checkMobileNo(userMobile);
        }
        catch (Exception ex)
        {
            Errormsg(ex.ToString());
            _common.ErrorLog("trvlrSeatslct-M4", ex.ToString());
        }
    }

    public void resetOTPPnl()
    {
        tbName.Text = "";
        tbOTP.Text = "";
        tbcaptchacode.Text = "";
    }

    private void checkMobileNo(string mobileNumber)
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
                tbName.Visible = true;
                if (alreadyReg == "Y")
                {
                    tbName.Visible = false;
                }
                CommonSMSnEmail sms = new CommonSMSnEmail();
                string otp =  getRandom();
                sms.sendOtp(otp, mobileNo);
                Session["_Otp"] = otp;
                string submob = mobileNumber.Substring(7, 3);
                RefreshCaptcha();
                string mm = "XXXXXXX" + submob;
                lblOtpMsg.Text = "Please enter the 6 digit OTP which has been sent to Mobile No. ( " + mm + " )";

                pnlOTP.Visible = true;
                pnlLayout.Visible = false;
            }
            else
            {
                Errormsg("Sorry");
                return;
            }
        }
        catch (Exception ex)
        {
            Errormsg("Sorry");
        }
    }

    private void loginFirst(string mobileNo, string userName, string userEmail)
    {
        try
        {
            string ipAddress = wsClass.getIPAddress();
            wsClass obj = new wsClass();
            DataTable dt = new DataTable();
            dt = obj.traveller_loginFirst_dt(mobileNo, userName, "W", ipAddress);
            if (dt.Rows.Count > 0)
            {
                Session["_UserCode"] = mobileNo;
                SaveSeats(mobileNo, mobileNo, userEmail);
            }
            else
            {
                Errormsg(dt.TableName.ToString());
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message.ToString());
        }
    }

    private void loginSuccess(string mobileNo, string userEmail)
    {
        try
        {
            string ipAddress = wsClass.getIPAddress();
            wsClass obj = new wsClass();
            DataTable dt = new DataTable();
            dt = obj.traveller_loginSuccess_dt(mobileNo, "W", ipAddress);
            if (dt.Rows.Count > 0)
            {
                Session["_UserCode"] = mobileNo;
                SaveSeats(mobileNo, mobileNo, userEmail);
            }
            else
            {
                Errormsg(dt.TableName.ToString());
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message.ToString());
        }
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

        return "123456";// _random;
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
                Errormsg("You entered wrong OTP. Please enter valid OTP.");
            }
            else
            {
                Errormsg(dt.TableName.ToString());
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message.ToString());
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
            Session["calledFrom"] = "B";
            Response.Redirect("traveller/trvlrRedirection.aspx", false);
        }
        catch (Exception ex)
        {
            Errormsg("Unable to Create Cookie");
            return;
        }
    }
     
    protected void lbtnVerifypnlOTP_Click(object sender, EventArgs e)
    {
        string otp = tbOTP.Text;
        string userName = tbName.Text;
        if (_SecurityCheck.IsValidInteger(otp, 6, 6) == false)
        {
            RefreshCaptcha();
            Errormsg("Enter Valid 6 Digit OTP.");
            return;
        }

        if (tbcaptchacode.Text.Length < 6)
        {
            RefreshCaptcha();
            Errormsg("Enter Valid Security Code(Shown in Image).");
            return;
        }
        if (!(Session["CaptchaImage"] != null && tbcaptchacode.Text.ToLower() == Session["CaptchaImage"].ToString().ToLower()))
        {
            RefreshCaptcha();
            tbcaptchacode.Text = "";
            Errormsg("Invalid Security Code(Shown in Image).");
            return;
        }

        string userMobile = tbMobileNo.Text.Trim();
        string userEmail = tbEmailid.Text.Trim();

        // Check OTP
        if (otp == Session["_Otp"].ToString())
        {
            if (ViewState["vsAlreadyReg"].ToString() == "Y")
            {
                loginSuccess(userMobile, userEmail);
            }
            else
            {
                if (userName.Length < 2)
                {
                    Errormsg("Enter Valid Name");
                    return;
                }
                loginFirst(userMobile, userName, userEmail);
            }
        }
        else
        {
            loginFail(userMobile);
        }
    }
    protected void lbtnRefresh_Click(object sender, EventArgs e)
    {
        RefreshCaptcha();
    }
    protected void lbtnClosepnlOTP_Click(object sender, EventArgs e)
    {
        Response.Redirect("home.aspx");
    }
    #endregion




   
}