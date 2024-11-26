using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class traveller_ticketCancel : BasePage
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    string commonerror = "There is some error. Please contact the helpdesk or try again after sometime.";
    wsClass obj = new wsClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        checkUser();
        Session["_moduleName"] = "Ticket Cancellation";
        if (!IsPostBack)
        {
            Random randomclass = new Random();
            Session["rndNoCheck"] = randomclass.Next().ToString();
            hidtoken.Value = Session["rndNoCheck"].ToString();
            onload();
            loadConfirmedTickets(Session["_UserCode"].ToString());
            CancelledTickets();
            lblcancellationpolicy.Text = _common.getCancellationpolicy();
        }
    }

    #region "Method"
    private void Errormsg(string msg)
    {
        string popup = _popup.modalPopupSmall("D", "Please Check", msg, "Close");
        Response.Write(popup);
    }
    private void Successmsg(string msg)
    {
        string popup = _popup.modalPopupSmall("S", "Congratulations", msg, "Close");
        Response.Write(popup);
    }
    private void checkUser()
    {
        if (_security.isSessionExist(Session["_UserCode"]) == true)
        {
            Session["_UserCode"] = Session["_UserCode"];
        }
        else
        {
            Response.Redirect("errorpage.aspx");
        }
    }
    private void CheckTokan()
    {
        if (Session["rndNoCheck"] == null || Session["rndNoCheck"].ToString() == "")
        {
            Response.Redirect("../errorpage.aspx");
        }

        if (Session["rndNoCheck"].ToString() != hidtoken.Value.ToString())
        {
            Response.Redirect("../errorpage.aspx");
        }
    }
    public void onload()
    {
        try
        {
            if (_security.isSessionExist(Session["TicketNumber"]) == true)
            {
                loadTicketDetails(Session["TicketNumber"].ToString());

            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.ToString());
            _common.ErrorLog("ticketCancel.aspx-0001", ex.Message);
        }
    }
    private void loadTicketDetails(string ticketno)  //M1
    {
        try
        {
            pnltktdetails.Visible = false;
            pnlNoRecord.Visible = true;

            DataTable dt_jrnyDtl = obj.getJourneyDetails(ticketno, "A");

            if (dt_jrnyDtl.Rows.Count > 0)
            {
                lblUsr.Text = dt_jrnyDtl.Rows[0]["booked_by"].ToString();
                lblTotal.Text = dt_jrnyDtl.Rows[0]["amount_total"].ToString();
                lblPNRNO.Text = dt_jrnyDtl.Rows[0]["_ticketno"].ToString();
                lblService.Text = dt_jrnyDtl.Rows[0]["depot_servicecode"].ToString() + dt_jrnyDtl.Rows[0]["service_type_name"].ToString();
                lblDistance.Text = dt_jrnyDtl.Rows[0]["total_distance"].ToString();
                lblScheduledDeparture.Text = dt_jrnyDtl.Rows[0]["trip_time"].ToString();
                lblJourneyDate.Text = dt_jrnyDtl.Rows[0]["journeydate"].ToString();
                lblFare.Text = dt_jrnyDtl.Rows[0]["amount_fare"].ToString();
                lblFrom.Text = dt_jrnyDtl.Rows[0]["fromstn_name"].ToString();
                lblScheduledArrival.Text = dt_jrnyDtl.Rows[0]["tripend_time"].ToString();
                lblRes.Text = dt_jrnyDtl.Rows[0]["amount_onl_reservation"].ToString();
                lblTo.Text = dt_jrnyDtl.Rows[0]["tostn_name"].ToString();
                lblBookingDate.Text = dt_jrnyDtl.Rows[0]["bookingdatetime"].ToString();
                lbltottax.Text = dt_jrnyDtl.Rows[0]["amount_tax"].ToString();
                pnltktdetails.Visible = true;
                pnlNoRecord.Visible = false;
            }
            else
            {
                pnltktdetails.Visible = false;
                pnlNoRecord.Visible = true;
                return;
            }

            DataTable dt_psngr = obj.getPassengerDetailsForCancel(ticketno, "A");
            if (dt_psngr.Rows.Count > 0)
            {
                gvTicketseatDetails.DataSource = dt_psngr;
                gvTicketseatDetails.DataBind();
            }
            else
            {
                pnltktdetails.Visible = false;
                pnlNoRecord.Visible = true;
                return;
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("ticketCancel.aspx-0002", ex.Message);

        }
    }
    private void cancelTicket() //M2
    {
        try
        {
            if (_security.isSessionExist(Session["TicketNumber"]) == false)
            {
                Response.Redirect("errorpage.aspx");
                return;
            }
            if (_security.isSessionExist(Session["_UserCode"]) == false)
            {
                Response.Redirect("errorpage.aspx");
                return;
            }

            string IpAddress = wsClass.getIPAddress();

            CheckBox CchkBox;
            string seatamt = "";
            string seatNo = "";
            int i = 0;
            foreach (GridViewRow row in gvTicketseatDetails.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CchkBox = (CheckBox)row.FindControl("chkseats");
                    if (CchkBox.Checked == true)
                    {
                        seatNo = gvTicketseatDetails.DataKeys[row.RowIndex]["seatno"].ToString() + "," + seatNo;
                        seatamt = gvTicketseatDetails.DataKeys[row.RowIndex]["amountrefunded"].ToString() + "," + seatamt;
                        i = i + 1;
                    }
                }
            }

            string MP_TicketNo;
            string MP_CANCELLEDBY;
            MP_TicketNo = Session["TicketNumber"].ToString();
            MP_CANCELLEDBY = Session["_UserCode"].ToString();


            DataTable dataTable = obj.cancelTicket(MP_TicketNo, seatNo, seatamt, i, MP_CANCELLEDBY, IpAddress, "T","W");
            if (dataTable.Rows.Count > 0)
            {
                string p_cancellationrefno = dataTable.Rows[0]["cancellationrefno"].ToString();
                pnltktdetails.Visible = false;
                pnlNoRecord.Visible = true;
                Successmsg("Ticket Cancellation Successfully!");
                Session["Cancel_voucher"] = p_cancellationrefno;
                CommonSMSnEmail sms = new CommonSMSnEmail();
                sms.sendTicketCancel_SMSnEMAIL(MP_TicketNo, Session["_UserCode"].ToString(), seatNo, seatamt, p_cancellationrefno);

                //if (refundtransaction(p_cancellationrefno, MP_CANCELLEDBY) == true)
                //{
                //}

                openSubDetailsWindow("../E_cancellationvoucher.aspx");
                loadTicketDetails(Session["TicketNumber"].ToString());
                CancelledTickets();
            }
            else
            {
                Errormsg(commonerror);
            }

        }
        catch (Exception ex)
        {
            Errormsg(commonerror);
            _common.ErrorLog("ticketCancel.aspx-0003", ex.Message);
        }
    }
    private void CancelledTickets()
    {
        try
        {
            string cancelby = Session["_UserCode"].ToString();
            dt = obj.getCancelledTickets(cancelby, tbcancelledpnr.Text);
            if (dt.Rows.Count > 0)
            {
                grdcancelledtkt.DataSource = dt;
                grdcancelledtkt.DataBind();
                grdcancelledtkt.Visible = true;
                pnlNocancelledtkt.Visible = false;
            }
            else
            {
                grdcancelledtkt.Visible = false;
                pnlNocancelledtkt.Visible = true;
            }
        }
        catch (Exception ex)
        {
            grdcancelledtkt.Visible = false;
            pnlNocancelledtkt.Visible = true;
            _common.ErrorLog("ticketCancel.aspx-0004", ex.Message);
        }
    }
    public void openSubDetailsWindow(string MModuleName)
    {
        try
        {
            string murl;
            murl = MModuleName + "?rt=" + DateTime.Now.ToString();
            string b = Request.Browser.Type.Substring(0, 2);
            if (b.ToUpper().Trim() == "IE")
                Response.Write("<SCRIPT language='javascript'>window.showModalDialog('" + murl + "','name','dialogWidth:830px;dialogHeight:900px');</script>");
            else
            {
                // Dim url As String = "GenQrySchStages.aspx"
                string fullURL = "window.open('" + murl + "', '_blank', 'height=900,width=830,status=no,toolbar=no,menubar=no,location=no,scrollbars=no,resizable=no,titlebar=no' );";
                // ScriptManager.RegisterStartupScript(Me, GetType(String), "OPEN_WINDOW", fullURL, True)
                string script = "window.open('" + fullURL + "','')";
                if ((ClientScript.IsClientScriptBlockRegistered("NewWindow") == false))
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "NewWindow", fullURL, true); // 
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("ticketCancel.aspx-0005", ex.Message);
        }
    }
    private bool validvalue()
    {
        try
        {
            string msg = "";
            CheckBox CchkBox;
            Label lblamt;
            string seatNo = "";
            int i = 0;
            foreach (GridViewRow row in gvTicketseatDetails.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CchkBox = (CheckBox)row.FindControl("chkseats");
                    seatNo = gvTicketseatDetails.DataKeys[row.RowIndex]["amountrefunded"].ToString();
                    if (CchkBox.Checked == true)
                    {
                        i = i + 1;
                    }
                }
            }
            if (i <= 0)
            {
                msg = "Please select seat(s) to cancel";
                Errormsg(msg);
                return false;
            }

            return true;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("ticketCancel.aspx-0006", ex.Message);
            return false;
        }
    }
    private bool refundtransaction(string cancel_refNo, string refundedby)
    {
        try
        {
            string IpAddress = wsClass.getIPAddress();
            DataTable dataTable = obj.refundtransaction(cancel_refNo, refundedby, refundedby, IpAddress);
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("ticketCancel.aspx-0007", ex.Message);
            return false;
        }
    }

    private void loadConfirmedTickets(string userId)//M2
    {
        try
        {
              DataTable dt = new DataTable();
            dt = obj.getAvailableTicket(userId, "0","T");
           // DataRow[] dr = dt.Select("current_status = 'A' and for_cancel='Y'");
           // DataTable dttt = dr.CopyToDataTable();

            if (dt.Rows.Count > 0)
            {
                gvTickets.DataSource = dt;
                gvTickets.DataBind();

                lblNoTicketsMsg.Text = "";
                pnlNoTickets.Visible = false;
            }
            else
            {
                lblNoTicketsMsg.Text = "We are waiting for your first booking";
                pnlNoTickets.Visible = true;
            }
        }
        catch (Exception ex)
        {
            lblNoTicketsMsg.Text = "Oops! Something happened with your ticket loading process.<br> Please feel free to contact the helpdesk";
            pnlNoTickets.Visible = true;
            _common.ErrorLog("ticketCancel.aspx-0008", ex.Message);
        }
    }
    #endregion

    #region "Event"  
    protected void gvTicketseatDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {


        }
    }

    protected void lbtncanceltkt_Click(object sender, EventArgs e)
    {
        CheckTokan();
        if (validvalue() == false)
        {
            return;
        }
        mpConfirmation.Show();
    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        CheckTokan();
        cancelTicket();
    }

    protected void grdcancelledtkt_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        CheckTokan();
        if (e.CommandName == "PrintVoucher")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string p_cancellationrefno = grdcancelledtkt.DataKeys[index].Values["cancellation_ref_no"].ToString();
            Session["Cancel_voucher"] = p_cancellationrefno;
            openSubDetailsWindow("../E_cancellationvoucher.aspx");
        }
        //if (e.CommandName == "Refund")
        //{
        //    int index = Convert.ToInt32(e.CommandArgument);
        //    string MP_CANCELLEDBY = Session["_UserCode"].ToString();
        //    string p_cancellationrefno = grdcancelledtkt.DataKeys[index].Values["cancellation_ref_no"].ToString();
        //    if (refundtransaction(p_cancellationrefno, MP_CANCELLEDBY) == true)
        //    {
        //        Successmsg("Refund Successfully");
        //    }
        //    CancelledTickets();
        //}
    }

    protected void grdcancelledtkt_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdcancelledtkt.PageIndex = e.NewPageIndex;
        CancelledTickets();
    }

    protected void lbtncacnelledpnrsearch_Click(object sender, EventArgs e)
    {
        CancelledTickets();
    }

    protected void lbtncacnelledpnrreset_Click(object sender, EventArgs e)
    {
        CheckTokan();
        tbcancelledpnr.Text = "";
        CancelledTickets();
    }

    protected void grdcancelledtkt_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    // tickets list
    protected void gvTickets_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        CheckTokan();
        if (e.CommandName == "CANCELLATION")
        {
            int rowIndex = int.Parse(e.CommandArgument.ToString());
            string ticketNo = gvTickets.DataKeys[rowIndex]["ticket_no"].ToString();
            Session["TicketNumber"] = ticketNo;
            loadTicketDetails(Session["TicketNumber"].ToString());
        }
    }
    protected void gvTickets_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
    }
    protected void gvTickets_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTickets.PageIndex = e.NewPageIndex;
        loadConfirmedTickets(Session["_UserCode"].ToString());
    }

    #endregion


}