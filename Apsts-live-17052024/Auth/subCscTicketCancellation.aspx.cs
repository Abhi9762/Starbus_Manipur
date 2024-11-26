using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_subCscTicketCancellation : System.Web.UI.Page
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    string msg = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {

            Session["moduleName"] = "Ticket Cancellation" + " - (" + Session["_UserName"] + " " + Session["_UserCode"] + ")";
            //txtcancellationdate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            GetAvailableTicket();
            getcancelledtkt(txtcancelledtktno.Text.ToString());


            if (Session["p_ticketNo"].ToString() != "0")
            {
                DataTable dt = new DataTable();
                wsClass obj = new wsClass();
                dt = obj.getAvailableTicket(Session["_UserCode"].ToString(), Session["p_ticketNo"].ToString(), "A");
                lblTicketNo.Text = dt.Rows[0]["ticket_no"].ToString();

                lblSource.Text = dt.Rows[0]["src"].ToString();
                lblDestination.Text = dt.Rows[0]["dest"].ToString();
                lblJourneyDate.Text = dt.Rows[0]["journey_date"].ToString();
                lblJourneyTime.Text = dt.Rows[0]["depart"].ToString();
                lblServiceType.Text = dt.Rows[0]["busservice_name"].ToString();
                GetPassenger(Session["p_ticketNo"].ToString());
                pnlticketdetails.Visible = true;
                pnlticket.Visible = false;
            }
        }


    }

    #region "Method"




    private void GetAvailableTicket()
    {
        DataTable dt = new DataTable();
        wsClass obj = new wsClass();
        dt = obj.getAvailableTicket(Session["_UserCode"].ToString(), Session["p_ticketNo"].ToString(), "A");
        if (dt.Rows.Count > 0)
        {
            gvTicketCancelList.DataSource = dt;
            gvTicketCancelList.DataBind();
            gvTicketCancelList.Visible = true;
            lblValidationMsg.Visible = false;
        }
        else
        {
            gvTicketCancelList.Visible = false;
            lblValidationMsg.Visible = true;
            lblValidationMsg.Text = " !! Sorry, No Ticket is available for Cancellation !!";
        }
    }

    private void GetPassenger(string ticketno)
    {
        try
        {
            DataTable dt = new DataTable();
            wsClass obj = new wsClass();
            dt = obj.getPassengerDetailsForCancel(ticketno, "A");
            if (dt.Rows.Count > 0)
            {
                grdticketpassenger.DataSource = dt;
                grdticketpassenger.DataBind();
            }
        }
        catch (Exception ex)
        {

        }

    }

    private bool ValidValue()
    {
        CheckBox CchkBox;
        string seatNo = "";
        int i = 0;
        foreach (GridViewRow mygridViewRow in grdticketpassenger.Rows)
        {
            if (mygridViewRow.RowType == DataControlRowType.DataRow)
            {
                CchkBox = mygridViewRow.FindControl("CheckBox1") as CheckBox;
                if (CchkBox.Checked)
                {
                    i++;
                }
            }
        }
        if (i <= 0)
        {
            msg = "Please select seat(s) to cancel";
            return false;
        }
        return true;
    }
    private void refundtransaction(string cancel_refNo, string refundedby)//M5
    {
        try
        {
            string IpAddress = wsClass.getIPAddress();
            wsClass obj = new wsClass();
            dt = obj.refundtransaction(cancel_refNo, refundedby, Session["_UserCode"].ToString(), IpAddress);
            if (dt.Rows.Count > 0)
            {
                string p_Refrefno = dt.Rows[0]["refund_ref_no"].ToString();

                if (p_Refrefno == "ERROR" || p_Refrefno == "EXCEPTION")
                {
                    alert(msg);
                    return;
                }
                else
                {
                    alert("Refund Successfully");
                    getcancelledtkt(txtcancelledtktno.Text.ToString());
                }
            }
            else
            {
                _common.ErrorLog("CntrTicketCancellation-M5", dt.TableName);
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("CntrTicketCancellation-M5", ex.Message.ToString());
        }
    }


    public void OpenSubDetailsWindow(string MModuleName)
    {
        try
        {
            mp2.Hide();
            string murl = MModuleName + "?rt=" + DateTime.Now.ToString();
            if (Request.Browser.Type.ToUpper().Substring(0, 2).Trim() == "IE")
            {
                Response.Write("<SCRIPT language='javascript'>window.showModalDialog('" + murl + "','name','dialogWidth:830px;dialogHeight:550px');</script>");
            }
            else
            {
                string fullURL = "window.open('" + murl + "', '_blank', 'height=550,width=830,status=no,toolbar=no,menubar=no,location=no,scrollbars=no,resizable=no,titlebar=no' );";
                string script = "window.open('" + fullURL + "','')";
                if (!ClientScript.IsClientScriptBlockRegistered("NewWindow"))
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "NewWindow", fullURL, true);
                }
            }
        }
        catch (Exception ex)
        {
            return;
        }
    }
    private void alert(string msg)
    {
        lblerro.Text = msg;
        mperror.Show();
    }
    private void getcancelledtkt(string tktno)//M1
    {
        try
        {
            string cancelby = Session["_UserCode"].ToString();
            wsClass obj = new wsClass();
            dt = obj.getCancelledTickets(cancelby, tktno);
            if (dt.Rows.Count > 0)
            {
                grdcancelledtkt.DataSource = dt;
                grdcancelledtkt.DataBind();
                grdcancelledtkt.Visible = true;

            }
            else
            {
                grdcancelledtkt.Visible = false;

            }
        }
        catch (Exception ex)
        {
            grdcancelledtkt.Visible = false;


        }
    }

    private void cancelticket()
    {
        CheckBox CchkBox;
        Label lblseat;
        string seatNo = "";
        string IpAddress = wsClass.getIPAddress();
        string seatamt = "";
        int i = 0;
        foreach (GridViewRow mygridViewRow in grdticketpassenger.Rows)
        {
            if (mygridViewRow.RowType == DataControlRowType.DataRow)
            {
                CchkBox = mygridViewRow.FindControl("CheckBox1") as CheckBox;
                lblseat = mygridViewRow.FindControl("grdLabelSeat") as Label;

                if (CchkBox.Checked == true)
                {
                    seatNo = grdticketpassenger.DataKeys[mygridViewRow.RowIndex]["seatno"].ToString() + "," + seatNo;
                    seatamt = grdticketpassenger.DataKeys[mygridViewRow.RowIndex]["amountrefunded"].ToString() + "," + seatamt;
                    i = i + 1;
                    //seatNo = lblseat.Text.Trim() + "," + seatNo;
                    //i = i + 1;
                }
            }
        }

        string MP_TicketNo;
        string MP_CANCELLEDBY;
        MP_TicketNo = lblTicketNo.Text.Trim();
        MP_CANCELLEDBY = Session["_UserCode"].ToString().Trim();
        DataTable dt = new DataTable();
        wsClass obj = new wsClass();
        dt = obj.cancelTicket(MP_TicketNo, seatNo, seatamt, i, MP_CANCELLEDBY, IpAddress, "A", "W");

        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["cancellationrefno"].ToString() == "ERROR")
            {
                msg = "Ticket cannot be cancelled now.";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "ErrorMessage('" + msg + "');", true);
                return;
            }
            else
            {
                Session["CANCELLATIONREFNO"] = dt.Rows[0]["cancellationrefno"];
                Session["TicketNumber"] = MP_TicketNo;
                pnlticket.Visible = true;
                pnlticketdetails.Visible = false;
                wsClass obj1 = new wsClass();
                obj.refundtransactionnew(dt.Rows[0]["cancellationrefno"].ToString(), MP_TicketNo, "A", MP_CANCELLEDBY, MP_CANCELLEDBY);
                Session["Cancel_voucher"] = dt.Rows[0]["cancellationrefno"].ToString();
                GetAvailableTicket();
                OpenSubDetailsWindow("../E_cancellationvoucher.aspx");
            }
        }
        else
        {
            msg = "Ticket cannot be cancelled now. Please try again.";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "ErrorMessage('" + msg + "');", true);
            return;
        }
    }
    public string GetRandomOTP()
    {
        Random random = new Random();
        const string src = "0123456789";
        int i;
        string _random = "";

        for (i = 0; i < 6; i++)
        {
            _random += src[random.Next(0, src.Length)];
        }

        return "123456"; // You might want to change this to return _random instead
    }


    private void RefreshCaptcha()
    {
        tbcaptchacode.Text = "";
        Session["CaptchaImage"] = GetRandomOTP();
    }




    #endregion

    #region "Event"
    protected void gvTicketCancelList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "View")
        {
            lblTicketNo.Text = gvTicketCancelList.DataKeys[index].Values["ticket_no"].ToString();
            lblSource.Text = gvTicketCancelList.DataKeys[index].Values["src"].ToString();
            lblDestination.Text = gvTicketCancelList.DataKeys[index].Values["dest"].ToString();
            lblJourneyDate.Text = gvTicketCancelList.DataKeys[index].Values["journey_date"].ToString();
            lblJourneyTime.Text = gvTicketCancelList.DataKeys[index].Values["depart"].ToString();
            lblServiceType.Text = gvTicketCancelList.DataKeys[index].Values["busservice_name"].ToString();
            GetPassenger(gvTicketCancelList.DataKeys[index].Values["ticket_no"].ToString());
            Session["_mobile"] = gvTicketCancelList.DataKeys[index].Values["traveller_mobile_no_"];
            pnlticketdetails.Visible = true;
            pnlticket.Visible = false;
        }

    }
    protected void btnCancelTicket_Click(object sender, EventArgs e)
    {
        if (!ValidValue())
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "ErrorMessage('" + msg + "');", true);
            return;
        }

        lblerror.Text = "";
        string mobileNo = Session["_mobile"].ToString();
        CommonSMSnEmail comm = new CommonSMSnEmail();
        string otp = "123456";// GetRandomOTP();
        //comm.SendOTP_SMS(otp, mobileNo, 1);
        Session["_otp"] = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(otp, "SHA1");
        string submob = mobileNo.Substring(7, 3);
        string mm = "XXXXXXX" + submob;
        lblOtpMsg.Text = "Please enter 6 digit OTP which has been sent to Mobile No. ( " + mm + " )";
        mp2.Show();


    }


    protected void yes_Click(object sender, EventArgs e)
    {

    }
    protected void no_Click(object sender, EventArgs e)
    {
        mp2.Hide();
    }
    protected void btnCancelTicket_Click1(object sender, EventArgs e)
    {

        if (!ValidValue())
        {
            return;
        }

        lblerror.Text = "";
        string mobileNo = Session["_mobile"].ToString();
        CommonSMSnEmail comm = new CommonSMSnEmail();
        string otp = GetRandomOTP();
        //comm.SendOTP_SMS(otp, mobileNo, 1);
        Session["_otp"] = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(otp, "SHA1");
        string submob = mobileNo.Substring(7, 3);
        string mm = "XXXXXXX" + submob;
        lblOtpMsg.Text = "Please enter 6 digit OTP which has been sent to Mobile No. ( " + mm + " )";
        mp2.Show();
    }
    protected void btnsearchcancelledtkt_Click(object sender, EventArgs e)
    {
        
        GetAvailableTicket();
        getcancelledtkt(txtcancelledtktno.Text.ToString());

        if (grdcancelledtkt.Rows.Count <= 0)
        {
            alert("No Cancelled Ticket Available");
        }
    }
    protected void btnresetcancelledtkt_Click(object sender, EventArgs e)
    {
        txtcancelledtktno.Text = "";
        getcancelledtkt(txtcancelledtktno.Text.ToString());
        if (grdcancelledtkt.Rows.Count <= 0)
        {
            msg = "No Cancelled Ticket Available";
            alert(msg);
            return;
        }
    }
    protected void grdcancelledtkt_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "PrintVoucher")
            {
                Session["CANCELLATIONREFNO"] = grdcancelledtkt.DataKeys[index].Values["cancellation_ref_no"];
                Session["TicketNumber"] = grdcancelledtkt.DataKeys[index].Values["ticket_no"];
                Session["Cancel_voucher"] = Session["CANCELLATIONREFNO"];
                OpenSubDetailsWindow("../E_cancellationvoucher.aspx");
                getcancelledtkt(txtcancelledtktno.Text.ToString());
            }
            if (e.CommandName == "Refund")
            {

                string MP_CANCELLEDBY = Session["_usercode"].ToString();
                string p_cancellationrefno = grdcancelledtkt.DataKeys[index].Values["cancellation_ref_no"].ToString();
                refundtransaction(p_cancellationrefno, MP_CANCELLEDBY);


            }
        }
        catch (Exception ex)
        {
            alert(ex.Message);
        }
    }



    protected void btnBack_Click(object sender, EventArgs e)
    {
        Session["p_ticketNo"] = "0";
        Response.Redirect("subCscTicketCancellation.aspx");
    }

    protected void lbtnRefresh_Click(object sender, EventArgs e)
    {
        mp2.Show();
        RefreshCaptcha();
    }

    protected void yes_Click1(object sender, EventArgs e)
    {
        string otp = tbOTP.Text;

        if (_validation.IsValidInteger(otp, 6, 6) == false)
        {
            RefreshCaptcha();
            lblerror.Text = "Enter Valid 6 Digit OTP.";
            mp2.Show();
            return;
        }

        if (tbcaptchacode.Text.Length < 6)
        {
            RefreshCaptcha();
            lblerror.Text = "Enter Valid Security Code(Shown in Image).";
            mp2.Show();
            return;
        }

        if (!(Session["CaptchaImage"] != null && tbcaptchacode.Text.ToLower() == Session["CaptchaImage"].ToString().ToLower()))
        {
            RefreshCaptcha();
            tbcaptchacode.Text = "";
            lblerror.Text = "Invalid Security Code(Shown in Image).";
            mp2.Show();
            return;
        }

        string otpEncrypt = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(otp, "SHA1");

        if (otpEncrypt == Session["_otp"].ToString())
        {
            cancelticket();
        }
        else
        {
            lblerror.Text = "You entered the wrong OTP. Please enter a valid OTP.";
            mp2.Show();
        }

    }

    protected void no_Click1(object sender, EventArgs e)
    {
        mp2.Hide();
    }

    #endregion
}