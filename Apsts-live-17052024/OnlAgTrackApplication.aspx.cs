using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OnlAgTrackApplication : outsidebasepage
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
            if (sbXMLdata.checkModuleCategory("70") == false)
            {
                Response.Redirect("Errorpage.aspx");
            }
            Session["Heading"] = "Agent Application";
            Session["Heading1"] = "You can register, as an agent to become part of APSTS family";
            pnlDetails.Visible = false;
            pnlForm.Visible = true;
            //LoadRegistrtaionDateFeeDetails();
            txtmobileno.Text = "";
            txtReferenceNo.Text = "";
            RefreshCaptcha();
        }
    }


    #region "Method" 
    private void RefreshCaptcha()
    {
        tbcaptchacode.Text = "";
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
    public void errormsg(string errormsg)
    {
        lblerrmsg.Text = errormsg;
        mpError.Show();
    }

    private void SuccessMsg(string successMsg)
    {
        lblsucessmsg.Text = successMsg;
        mpconfirm.Show();
    }

    private void GetDetails()
    {
        try
        {
            pnlDetails.Visible = false;
            pnlForm.Visible = true;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "agent.fun_AgRequestStatus");
            MyCommand.Parameters.AddWithValue("@p_referenceno", txtReferenceNo.Text);
            MyCommand.Parameters.AddWithValue("@p_mobile", txtmobileno.Text);
            dt = bll.SelectAll(MyCommand);

            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    step1.Attributes.Add("class", "stepper-item");
                    step2.Attributes.Add("class", "stepper-item");
                    step3.Attributes.Add("class", "stepper-item");
                    step4.Attributes.Add("class", "stepper-item");
                    step5.Attributes.Add("class", "stepper-item");
                    step6.Attributes.Add("class", "stepper-item");

                    lblReferenceNo.Text = dt.Rows[0]["reference_no"].ToString();
                    grvRequest.DataSource = dt;
                    grvRequest.DataBind();
                    pnlDetails.Visible = true;
                    pnlForm.Visible = false;
                    txtReferenceNo.Text = "";
                    txtmobileno.Text = "";

                    lblpstatus.Text = dt.Rows[0]["p_status"].ToString();

                    if (dt.Rows[0]["current_status"].ToString() == "E")
                    {
                        step1.Attributes.Add("class", "stepper-item completed");
                        step2.Attributes.Add("class", "stepper-item active");
                        lblpendingsince.Text = dt.Rows[0]["registration_date"].ToString();
                    }
                    else if (dt.Rows[0]["current_status"].ToString() == "V")
                    {
                        step1.Attributes.Add("class", "stepper-item completed");
                        step2.Attributes.Add("class", "stepper-item completed");
                        lblpendingsince.Text = dt.Rows[0]["updation_datetime"].ToString();
                    }
                    else if (dt.Rows[0]["current_status"].ToString() == "R")
                    {
                        step1.Attributes.Add("class", "stepper-item completed");
                        step4.Attributes.Add("class", "stepper-item completed");
                        step3.Visible = false;
                        step2.Visible = false;
                        step4.Visible = true;
                        lblpending.Text = "Rejected On";
                        lblpendingsince.Text = dt.Rows[0]["updation_datetime"].ToString();
                    }
                    else if (dt.Rows[0]["current_status"].ToString() == "C")
                    {
                        step1.Attributes.Add("class", "stepper-item completed");
                        step5.Attributes.Add("class", "stepper-item completed");
                        step3.Visible = false;
                        step2.Visible = false;
                        step4.Visible = false;
                        step5.Visible = true;
                        lblpending.Text = "Cancelled On";
                        lblpendingsince.Text = dt.Rows[0]["updation_datetime"].ToString();
                    }
                    else if (dt.Rows[0]["current_status"].ToString() == "D")
                    {
                        lblpending.Text = "Deactivated On";
                        lblpendingsince.Text = dt.Rows[0]["updation_datetime"].ToString();
                        step1.Attributes.Add("class", "stepper-item completed");
                        step2.Attributes.Add("class", "stepper-item completed");
                        step3.Attributes.Add("class", "stepper-item completed");
                        step6.Attributes.Add("class", "stepper-item completed");
                        step4.Visible = false;
                        step5.Visible = false;
                        step6.Visible = true;
                    }
                    else
                    {
                        lblpending.Text = "Approved On";
                        lblpendingsince.Text = dt.Rows[0]["updation_datetime"].ToString();
                        step1.Attributes.Add("class", "stepper-item completed");
                        step2.Attributes.Add("class", "stepper-item completed");
                        step3.Attributes.Add("class", "stepper-item completed");
                    }

                    lblStatus.Text = dt.Rows[0]["app_status"].ToString();
                    if (dt.Rows[0]["current_status"].ToString() == "R" || dt.Rows[0]["current_status"].ToString() == "C")
                    {
                        lblRejectReason.Visible = true;
                        lblStatus.Visible = false;
                        lblRejectReason.Text = "Reason- " + dt.Rows[0]["status_reason"].ToString();
                    }
                    else
                    {
                        lblRejectReason.Visible = false;
                    }

                    if (dt.Rows[0]["current_status"].ToString() == "V")
                    {
                        paymentDiv.Visible = true;
                        feeLabel.Visible = true;
                    }
                    else
                    {
                        paymentDiv.Visible = false;
                        feeLabel.Visible = false;
                    }

                    if (dt.Rows[0]["current_status"].ToString() == "A")
                    {
                        lbtngetpwd.Text = " You can get password click here <i class='icon icon-external-link'></i>";
                        lbtngetpwd.Visible = true;
                    }
                    else
                    {
                        lbtngetpwd.Visible = false;
                    }

                    lblAmount.Text = dt.Rows[0]["security_fee"].ToString();
                    Session["MobileNo"] = dt.Rows[0]["mobile_no"].ToString();
                    Session["referenceNo"] = dt.Rows[0]["reference_no"].ToString();
                }
                else
                {
                    errormsg("Invalid Reference/Mobile No");
                }
            }
            else
            {
                errormsg("Invalid Reference/Mobile No");
            }
        }
        catch (Exception ex)
        {
            pnlDetails.Visible = false;
            pnlForm.Visible = true;
            errormsg("Something went wrong" + ex.Message);
        }
    }
    private bool IsValidValues()
    {
        try
        {
            if (!(Session["CaptchaImage"] != null && tbcaptchacode.Text.ToLower() == Session["CaptchaImage"].ToString().ToLower()))
            {
                errormsg("Invalid Security Code(Shown in Image). Please Try Again");
                RefreshCaptcha();
                return false;
            }

            int msgcount = 0;
            string msg = "Enter Valid <br/>";

            if (!_validation.IsValidString(txtReferenceNo.Text.Trim().ToString(), 1, txtReferenceNo.MaxLength))
            {
                msgcount++;
                msg = msg + msgcount.ToString() + ". Reference No.<br/>";
            }
            if (!_validation.IsValidInteger(txtmobileno.Text, 10, 10))
            {
                msgcount++;
                msg = msg + msgcount.ToString() + ". Mobile Number.<br/>";
            }
            if (msgcount > 0)
            {
                errormsg(msg);
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            errormsg("Please check values Errorcode admagentdetails05");
            return false;
        }
    }
    public void Reset()
    {
        txtReferenceNo.Text = "";
        txtmobileno.Text = "";
        RefreshCaptcha();
        pnlDetails.Visible = false;
        pnlForm.Visible = true;
    }
    //public void LoadRegistrtaionDateFeeDetails()
    //{
    //    try
    //    {
    //        MyCommand = new NpgsqlCommand();
    //        MyCommand.Parameters.Clear();
    //        MyCommand.Parameters.AddWithValue("@StoredProcedure", "agent.fun_get_onlagentregsdatefee");
    //        dt = bll.SelectAll(MyCommand);
    //        if (dt.TableName == "Success")
    //        {
    //            if (dt.Rows.Count > 0)
    //            {
    //                // lblSecurityFee.Text = dt.Rows[0]["securityFee"].ToString();
    //                // lblFromdate.Text = dt.Rows[0]["frDate"].ToString();
    //                // lblToDate.Text = dt.Rows[0]["todate"].ToString();
    //                // lblProcFromdate.Text = dt.Rows[0]["procfrDate"].ToString();
    //                // lblProcTodate.Text = dt.Rows[0]["proctodate"].ToString();
    //                lblAmount.Text = dt.Rows[0]["security_fee"].ToString();
    //                Session["securityFee"] = dt.Rows[0]["security_fee"].ToString();
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        // Handle the exception
    //    }
    //}

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

    private void AgentVerification()
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
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "agent.onlagentverification");
            MyCommand.Parameters.AddWithValue("@p_refno", Session["referenceNo"]);
            MyCommand.Parameters.AddWithValue("@p_verifyby", Session["MobileNo"]);
            MyCommand.Parameters.AddWithValue("@p_ipaddress", IPAddress);
            MyCommand.Parameters.AddWithValue("@p_pwd", PASSWORD);
            dt = bll.SelectAll(MyCommand);

            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    // comm.sendDeptNewUserConfirmation_SMS(Session["_UserName"].ToString(), MyTable.Rows[0]["AgCode"].ToString(), PasswordForDB, Session["MobileNo"].ToString(), 13);
                    // comm.sendDeptNewUserConfirmation_EMAIL(MyTable.Rows[0]["AgCode"].ToString(), PasswordForDB, Session["Emailid"].ToString());
                }
            }
        }
        catch (Exception ex)
        {
            
        }

    }

    #endregion

    #region "Events"
    protected void btnsave_Click(object sender, EventArgs e)
    {
        if (!IsValidValues())
        {
            return;
        }

        GetDetails();

    }
    protected void lbtnRefresh_Click(object sender, EventArgs e)
    {
        RefreshCaptcha();
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        Reset();
    }
    protected void lbtnCancel_Click(object sender, EventArgs e)
    {
        Reset();
    }
    protected void lbtnProceed_Click(object sender, EventArgs e)
    {
        Session["_RNDIDENTIFIERONLAG"] = _security.GeneratePassword(10, 5);
        Response.Redirect("OnlAgApplicationFeePayment.aspx");

    }
    protected void lbtngetpwd_Click(object sender, EventArgs e)
    {
        AgentVerification();
    }

    #endregion
}