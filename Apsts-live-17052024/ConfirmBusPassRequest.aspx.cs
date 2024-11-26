using System;
using Npgsql;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Globalization;

public partial class ConfirmBusPassRequest : System.Web.UI.Page
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
            Session["OTPCount"] = 0;
            Session["ResendOTPCount"] = 0;
            Session["headerText"] = "Confirm to Proceed";
            // Label lblPageName = (Label)Page.Master.FindControl("lblPageName");
            // lblPageName.Text = "Pass Request - Confirm to Proceed";
            RefreshCaptcha();
            LoadTransDetails((string)Session["currtranrefno"]);
        }

    }

    #region "Method"
    private void RefreshCaptcha()
    {
        tbcaptchOTP.Text = "";
        Session["CaptchaImage"] = getRandom();
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


    private void LoadTransDetails(string CurrentRefNo)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "buspass.getconfirmpassdetails");
            MyCommand.Parameters.AddWithValue("@p_currentrefno", CurrentRefNo);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {

                    // lbltransrefno.Text = dt.Rows[0]["CURRTRANREFNO"].ToString();
                    //lblPassType.Text = dt.Rows[0]["cardtypename"].ToString();
                    //lblPassenger.Text = dt.Rows[0]["psngrtypename"].ToString();
                    lblpassrequest.Text = "You are applying for " + dt.Rows[0]["cardtypename_"].ToString() + " (" + dt.Rows[0]["psngr_type_name"].ToString() + ") with the following details.";

                    //-------------------------------Personal Details
                    lblname.Text = dt.Rows[0]["ctz_name"].ToString();
                    lblfname.Text = dt.Rows[0]["f_name"].ToString();
                    lblgender.Text = dt.Rows[0]["gender_"].ToString();
                    lbldob.Text = dt.Rows[0]["dob_"].ToString();

                    //--------------------------Journey Details
                    if (Convert.IsDBNull(dt.Rows[0]["routename"]) == true)
                    {
                        lblRoute.Text = "All Routes";
                    }
                    else
                    {
                        lblRoute.Text = dt.Rows[0]["routename"].ToString();
                    }

                    if (Convert.IsDBNull(dt.Rows[0]["start_station_name"]) == true)
                    {
                        lblstations.Text = "All Stations";
                    }
                    else
                    {
                        lblstations.Text = dt.Rows[0]["start_station_name"].ToString() + " to " + dt.Rows[0]["end_station_name"].ToString();
                    }
                    if (Convert.IsDBNull(dt.Rows[0]["service_type_name"]) == true)
                    {
                        lblservicetype.Text = "All Services";
                    }
                    else
                    {
                        lblservicetype.Text = dt.Rows[0]["service_type_name"].ToString();
                    }

                    lblvalidity.Text = dt.Rows[0]["period_from"].ToString() + " to " + dt.Rows[0]["period_to"].ToString();

                    //----------------------------Contact Details
                    lblmobileno.Text = dt.Rows[0]["mobile_no"].ToString();
                    string mask = new string('*', dt.Rows[0]["mobile_no"].ToString().Length - 4);
                    lblMobileOTP.Text = "Enter 6 digit OTP(One Time Password) sent to your Mobile Number " + mask + dt.Rows[0]["mobile_no"].ToString().Substring(dt.Rows[0]["mobile_no"].ToString().Length - 4, 4) + "<br/>";

                    lblemail.Text = dt.Rows[0]["email_id"].ToString();
                    lblstate.Text = dt.Rows[0]["statename_"].ToString();
                    lbldistrict.Text = dt.Rows[0]["distname_"].ToString();
                    lbladdress.Text = dt.Rows[0]["address_"].ToString() + ", " + dt.Rows[0]["pincode_"].ToString();

                    //-------------------------------Amount Details
                    lblPassamount.Text = dt.Rows[0]["pass_amount"].ToString() + " <i class='fa fa-rupee' ></i>";
                    if (Convert.ToInt32(dt.Rows[0]["extra_amt"]) == 0)
                    {
                        lblExtra_Charges.Text = "Extra_Charges";
                        lblExtrachrge.Text = dt.Rows[0]["extra_amt"].ToString() + " <i class='fa fa-rupee' ></i>";
                    }
                    else
                    {
                        lblExtra_Charges.Text = "Extra_Charges <br/> <span style='font-size:9pt;'>(" + dt.Rows[0]["p_extrachrgname_"].ToString() + ")</span>";
                        lblExtrachrge.Text = dt.Rows[0]["extra_amt"].ToString() + " <i class='fa fa-rupee' ></i>";
                    }
                    lbltaxamt.Text = dt.Rows[0]["totaltax_"].ToString() + " <i class='fa fa-rupee' ></i>";
                    lblAmountToPay.Text = (Convert.ToDouble(dt.Rows[0]["pass_amount"]) + Convert.ToDouble(dt.Rows[0]["extra_amt"]) + Convert.ToDouble(dt.Rows[0]["totaltax_"])).ToString();
                    Session["AMOUNT"] = lblAmountToPay.Text;
                    Session["MobileNo"] = dt.Rows[0]["mobile_no"].ToString();
                    Session["Emailid"] = dt.Rows[0]["email_id"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }


    private void Errormsg(string msg)
    {
        lblerrormsg.Text = msg;
        mperror.Show();
    }

    private void ConfirmMsg(string msg)
    {
        lblConfirmation.Text = msg;
        mpConfirmation.Show();
    }

    #endregion


    #region "Events"
    protected void lbtnRefresh_Click(object sender, EventArgs e)
    {
        RefreshCaptcha();
    }

    protected void lbtnProceedOTP_Click(object sender, EventArgs e)
    {
        if ((int)Session["OTPCount"] == 1)
        {
            if (!(Session["CaptchaImage"] != null && tbcaptchOTP.Text.ToLower() == Session["CaptchaImage"].ToString().ToLower()))
            {
                Errormsg("Invalid Security Code(Shown in Image). Please Try Again");
                RefreshCaptcha();
                return;
            }
        }

        if (tbOTP.Text.Length < 6)
        {
            Errormsg("Invalid OTP(Sent to your Mobile Number)");
            RefreshCaptcha();
            return;
        }
        else
        {
            //if (tbOTP.Text.Trim() == (string)Session["_otp"])
                if (tbOTP.Text.Trim() == "123456")
                {
                ConfirmMsg("Do you want to verify details for bus pass request ?");
                return;
            }
            else
            {
                Session["OTPCount"] = (int)Session["OTPCount"] + 1;
                Errormsg("Invalid OTP(This is OTP attempt number " + Session["OTPCount"].ToString() + " of 3, Maximum 3 OTP Verification Attempts.)");
                RefreshCaptcha();
                if ((int)Session["OTPCount"] == 1)
                {
                    pnlotpcaptcha.Visible = true;
                }
                if ((int)Session["OTPCount"] == 3)
                {
                    Session["OTPCount"] = 0;
                    Session["ResendOTPCount"] = 0;
                    Response.Redirect("NewBusPassRegistration.aspx");
                }
                tbOTP.Text = "";
                return;
            }
        }

    }

    protected void lbtnResendOTP_Click(object sender, EventArgs e)
    {
        //Session["_otp"] = getRandom();
        Session["_otp"] = "123456";
        //comm.sendOTP_SMS((string)Session["_otp"], (string)Session["MobileNo"], 1);
        // com.sendOTPbyemailPass((string)Session["_otp"], txtEmailReg.Text);
        Session["ResendOTPCount"] = (int)Session["ResendOTPCount"] + 1;

        if ((int)Session["ResendOTPCount"] == 3)
        {
            lbtnResendOTP.Visible = false;
        }
        else
        {
            lbtnResendOTP.Visible = true;
        }

        RefreshCaptcha();

    }

    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        string IPAddress = HttpContext.Current.Request.UserHostAddress;
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "buspass.insert_t_pass_txn_log");
        MyCommand.Parameters.AddWithValue("@p_tranrefno", Session["currtranrefno"]);
        MyCommand.Parameters.AddWithValue("@p_updateby", Session["MobileNo"]);
        MyCommand.Parameters.AddWithValue("@p_ipaddress", IPAddress);
        MyCommand.Parameters.AddWithValue("@p_txn_status", 2);
        dt = bll.SelectAll(MyCommand);
        if (dt.TableName == "Success")
        {
            if (dt.Rows.Count > 0)
            {
                Session["_RNDIDENTIFIERMSTAUTH"] = _security.GeneratePassword(10, 5);
                Session["PageStep1"] = 2;
                Response.Redirect("PassProceedPayment.aspx");
            }
            else
            {

            }
        }
    }
    #endregion
}