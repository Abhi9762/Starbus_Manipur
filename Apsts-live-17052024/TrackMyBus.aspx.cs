using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TrackMyBus : System.Web.UI.Page
{
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    sbValidation _validation = new sbValidation();
    sbBLL bll = new sbBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            RefreshCaptcha();
            Session["heading"] = "Track My Bus";
        }
    }

    #region "Methods"
    private void Errormsg(string msg)
    {
        string popup = _popup.modalPopupSmall("W", "Information", msg, "Close");
        Response.Write(popup);
    }

    private void information(string msg)
    {
        string popup = _popup.modalPopupLarge("S", "Information", msg, "OK");
        Response.Write(popup);
    }

    private void loadticketdetail(string _TicketNo)
    {
        pnlsearch.Visible = true;
        pnldetails.Visible = false;
        NpgsqlCommand MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.get_active_tickets_for_track");
        MyCommand.Parameters.AddWithValue("p_userid", tbmobile.Text);
        MyCommand.Parameters.AddWithValue("p_ticket_no", _TicketNo);
        DataTable dt = bll.SelectAll(MyCommand);
        if (dt.TableName == "Success")
        {

            if (dt.Rows.Count > 0)
            {
               if (dt.Rows[0]["bus_no"].ToString() != "NA" && dt.Rows[0]["gps_yn"].ToString() == "Y")
                {
                    Session["busno"] = dt.Rows[0]["bus_no"].ToString();
                    openiFrame();
                    pnlsearch.Visible = false;
                    pnldetails.Visible = true;
                    lblpnr.Text = _TicketNo;
                    lbljourneydate.Text = dt.Rows[0]["journey_date"].ToString() + " " + dt.Rows[0]["depart"].ToString();
                    lblbusno.Text =  dt.Rows[0]["bus_no"].ToString();
                    lblsrc.Text = dt.Rows[0]["src"].ToString() + "-" + dt.Rows[0]["dest"].ToString();
                }
                else
                {
                    Errormsg("Sorry, Bus Details Not Available");
                }

            }
            else
            {
                Errormsg("Invalid Active Ticket Number.");
            }
        }
    }

    private void openiFrame()
    {
        Literal l1 = new Literal();
        string busno = Session["busno"].ToString();
        busno = busno.Replace("-", "");
        string accesstoken = "7a9bef82c27db5be6ea4e9beff576639";
        string url = "https://t-location.intangles.com/" + busno + "?vendor-access-token=" + accesstoken + "";
        l1.Text = "<iframe name='we' id='12'  frameborder='no' scrolling='auto' height='700px' width='100%'  src='" + url + "' style='left:0; background-color:#B8B8B8;'></iframe>";
        div1.Controls.Add(l1);
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
    public void RefreshCaptcha()
    {
        tbcaptchacode.Text = "";
        Session["CaptchaImage"] = CaptchaText(); //CaptchaImage.GenerateRandomCode(CaptchaType.AlphaNumeric, 6);
    }
    #endregion

    #region "Events"
    protected void lbtnRefresh_Click(object sender, EventArgs e)
    {
        RefreshCaptcha();
    }
    protected void lbtnchange_Click(object sender, EventArgs e)
    {
        Response.Redirect("trackmybus.aspx");
    }
    protected void lbtntrackbus_Click(object sender, EventArgs e)
    {
        if (tbpnr.Text == "")
        {
            Errormsg("Enter PNR No.");
            RefreshCaptcha();
            return;
        }
        if (tbmobile.Text == "")
        {
            Errormsg("Enter Mobile No.");
            RefreshCaptcha();
            return;
        }
        if (tbmobile.Text.Length < 10)
        {
            Errormsg("Invalid Mobile No.");
            RefreshCaptcha();
            return;
        }
        if (tbcaptchacode.Text == "")
        {
            Errormsg("Enter Captcha Code.");
            RefreshCaptcha();
            return;
        }

        if (_validation.IsValidPnr(tbpnr.Text, 15, 20) == false)
        {
            Errormsg("Invalid PNR");
            RefreshCaptcha();
            return;
        }
        if (Session["CaptchaImage"].ToString().ToUpper() != tbcaptchacode.Text.ToUpper())
        {
            Errormsg("Invalid Captcha");
            RefreshCaptcha();
            return;
        }
        loadticketdetail(tbpnr.Text.ToString());
    }
    #endregion
  
}