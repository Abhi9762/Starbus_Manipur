using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DownloadE_ticket : System.Web.UI.Page
{
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    private sbCommonFunc _common = new sbCommonFunc();
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["heading"] = "Download E-Ticket";
        if (!IsPostBack)
        {
            RefreshCaptcha();
        }
    }

    private void Errormsg(string msg)
    {

        string popup = _popup.modalPopupSmall("W", "Please Check", msg, "Close");
        Response.Write(popup);
    }
    private void Successmsg(string msg)
    {
        string popup = _popup.modalPopupSmall("S", "Success", msg, "OK");
        Response.Write(popup);
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
   

    protected void lbtntdownload_Click(object sender, EventArgs e)
    {
        if (Session["CaptchaImage"].ToString() == tbcaptchacode.Text)
        {
            wsClass obj = new wsClass();
            DataTable dt = new DataTable();
            dt = obj.traveller_dt(tbMob.Text);
            if (dt.Rows.Count > 0)
            {
                string alreadyReg = dt.Rows[0]["already_yn"].ToString();
                ViewState["vsAlreadyReg"] = alreadyReg;
                if (alreadyReg == "Y")
                {
                    Session["p_ticketNo"] = tbPnr.Text;
                    PageOpen("e-Ticket", "E_ticket.aspx");
                    resetControl();
                }
                else
                {
                    Errormsg("Invalid Mobile Number");
                }
            }
        }
        else
        {
            Errormsg("Invalid Captcha Code");
        }
    }

    private void resetControl()
    {
        tbPnr.Text = "";
        tbMob.Text = "";
        RefreshCaptcha();
        tbcaptchacode.Text = "";
    }

    void PageOpen(string title, string src)
    {
        lblTitle.InnerText = title;
        embedPage.Src = src;
        mpePage.Show();
    }
    protected void lbtnRefresh_Click(object sender, EventArgs e)
    {
        RefreshCaptcha();
    }
}