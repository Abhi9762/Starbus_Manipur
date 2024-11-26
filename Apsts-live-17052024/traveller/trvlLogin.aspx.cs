using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Npgsql;

public partial class traveller_trvlLogin : System.Web.UI.Page
{
    sbValidation _SecurityCheck = new sbValidation();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    private sbCommonFunc _common = new sbCommonFunc();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            offers();
            loadGenralData();
            RefreshCaptcha();
            CheckTravellerBooking();
        }

    }

    #region "Methods"
    private void CheckTravellerBooking()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            DataTable dt = new DataTable();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_travellerbooking");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {

                    if (dt.Rows[0]["status"].ToString() == "N")
                    {
                        dvbook.Visible = false;
                        dvstatus.Visible = true;
                    }
                    else
                    {
                        dvbook.Visible = true;
                        dvstatus.Visible = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {

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
            _common.ErrorLog("trvlLogin.aspx-0001", ex.Message.ToString());
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
            _common.ErrorLog("trvlLogin.aspx-0002", ex.Message.ToString());
            return null;
        }
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
            _common.ErrorLog("trvlLogin.aspx-0003", ex.Message.ToString());
        }
    }


    private void Errormsg(string msg)
    {
        string popup = _popup.modalPopupSmall("W", "Information", msg, "Close");
        Response.Write(popup);
    }

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
        return "123456";//otp;
    }
    #endregion
    #region "Events"
    protected void imgOffer_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("../offersAll.aspx");
    }
    protected void lbtnRefresh_Click(object sender, EventArgs e)
    {
        RefreshCaptcha();
    }
    protected void lbtnNext_Click(object sender, EventArgs e)
    {
        string mobileNo = tbMobile.Text;
        if (_SecurityCheck.IsValidInteger(mobileNo, 10, 10) == false)
        {
            Errormsg("Invalid Mobile Number");
            RefreshCaptcha();
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



        CommonSMSnEmail sms = new CommonSMSnEmail();
        string otp = getRandom();
        //sms.sendOtp(otp, mobileNo);
        Session["_Otp"] = otp;
        Session["_MobileNumber"] = mobileNo;
        Response.Redirect("trvlLoginOTP.aspx");
    }
    #endregion
}
