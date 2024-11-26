using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class traveller_ticketDetail : System.Web.UI.Page
{
    sbBLL bll = new sbBLL();
    sbValidation _SecurityCheck = new sbValidation();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    private sbCommonFunc _common = new sbCommonFunc();
    string commonerror = "There is some error. Please contact to helpdesk or try again after some time.";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["_moduleName"] = "Ticket Detail";
            onload();
        }
    }

    #region "Motheds"
    void PageOpen(string title, string src)
    {
        lblTitle.InnerText = title;
        embedPage.Src = src;
        mpePage.Show();
    }
    private void Errormsg(string msg)
    {
        string popup = _popup.modalPopupSmall("W", "Information", msg, "Close");
        Response.Write(popup);
    }

    public void onload()
    {
        try
        {
            if (_security.isSessionExist(Session["TicketNumber"]) == true)
            {
                loadJourneyDetails(Session["TicketNumber"].ToString());
                loadPassengerDetails(Session["TicketNumber"].ToString());
                CancelledTickets(Session["TicketNumber"].ToString());
                LoadTicketLog(Session["TicketNumber"].ToString());
            }
            else
            {
                Response.Redirect("errorpage.aspx");
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.ToString());
            _common.ErrorLog("trvlrSeatConfrm-M1", ex.ToString());
        }
    }
    private void loadJourneyDetails(string ticketNo)//M1
    {
        try
        {
            lbtnEmail.Visible = false;
            lbtnSMS.Visible = false;
            lbtnTrack.Visible = false;
            lbtnPrintTicket.Visible = false;

            wsClass obj = new wsClass();
            DataTable dt = obj.getJourneyDetails(ticketNo, "0");

            if (dt.Rows.Count > 0)
            {
                lblTicketNo.Text = dt.Rows[0]["_ticketno"].ToString();
                string ticketStatus = dt.Rows[0]["current_status"].ToString();
                if (ticketStatus == "A")
                {
                    lblTicketStatus.Text = "Confirm";
                    lblTicketStatus.ForeColor = Color.LimeGreen;

                    lbtnEmail.Visible = true;
                    lbtnSMS.Visible = true;
                    lbtnTrack.Visible = true;
                    lbtnPrintTicket.Visible = true;
                }
                else if (ticketStatus == "C")
                {
                    lblTicketStatus.Text = "Cancelled";
                    lblTicketStatus.ForeColor = Color.OrangeRed;
                }
                else
                {
                    lblTicketStatus.Text = "Fail";
                    lblTicketStatus.ForeColor = Color.OrangeRed;
                }

                lblFromStation.Text = dt.Rows[0]["fromstn_name"].ToString();
                lblToStation.Text = dt.Rows[0]["tostn_name"].ToString();
                lblDate.Text = dt.Rows[0]["journeydate"].ToString();
                lblDeparture.Text = dt.Rows[0]["trip_time"].ToString();
                lblServiceType.Text = dt.Rows[0]["service_type_name"].ToString();

                lblFareAmt.Text = dt.Rows[0]["amount_fare"].ToString();
                lblReservationCharge.Text = dt.Rows[0]["amount_onl_reservation"].ToString();
                lblTotal.Text = dt.Rows[0]["amount_total"].ToString();

                lblBoardingStation.Text = dt.Rows[0]["boardingstn_name"].ToString();
            }
            else
            {
                Errormsg("Ticket details are not available. Please try again.");
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("trvlrConfirm-M1", ex.Message.ToString());
        }
    }
    private void loadPassengerDetails(string ticketNo)//M2
    {
        try
        {
            wsClass obj = new wsClass();
            DataTable dt = obj.getPassengerDetails(ticketNo, "0");

            if (dt.Rows.Count > 0)
            {
                gvPassengers.DataSource = dt;
                gvPassengers.DataBind();
                loadTaxes(ticketNo);

                gvPassengers.Visible = true;
                pnlNoRecordDetail.Visible = false;
            }
            else
            {
                gvPassengers.Visible = false;
                pnlNoRecordDetail.Visible = true;
            }
        }
        catch (Exception ex)
        {
            gvPassengers.Visible = false;
            pnlNoRecordDetail.Visible = true;
            _common.ErrorLog("trvlrConfirm-M2", ex.Message.ToString());
        }
    }
    private void loadTaxes(string ticketNo)//M3
    {
        try
        {
            wsClass obj = new wsClass();
            DataTable dt = obj.getTaxDetails(ticketNo);
            if (dt.Rows.Count > 0)
            {
                grdtax.DataSource = dt;
                grdtax.DataBind();
            }
        }
        catch (Exception ex)
        {

            _common.ErrorLog("trvlrConfirm-M3", ex.Message.ToString());
        }
    }
    private void LoadTicketLog(string p_ticketno)//M4
    {
        try
        {
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_get_ticket_log");
            MyCommand.Parameters.AddWithValue("p_ticketno", p_ticketno);
           DataTable  dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    gvticketlog.DataSource = dt;
                    gvticketlog.DataBind();

                    pnlNoTicketLog.Visible = false;
                }
                else
                {
                    pnlNoTicketLog.Visible = true;
                }
            }
            else
            {
                pnlNoTicketLog.Visible = true;
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("CntrTicketQuery-M4", ex.Message.ToString());
        }
    }
    private void CancelledTickets(string ticketNo)
    {
        try
        {
            string cancelby = Session["_UserCode"].ToString();
            wsClass obj = new wsClass();
            DataTable dt = obj.getCancelledTickets(cancelby, ticketNo);

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
        }
    }   
    private bool refundtransaction(string cancel_refNo, string refundedby)
    {
        try
        {
            //string IpAddress = HttpContext.Current.Request.UserHostAddress;
            //MyCommand = new NpgsqlCommand();
            //MyCommand.Parameters.Clear();
            //MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_ticket_refund");
            //MyCommand.Parameters.AddWithValue("p_cancel_refno", cancel_refNo);
            //MyCommand.Parameters.AddWithValue("p_refundedby", refundedby);
            //MyCommand.Parameters.AddWithValue("p_updatedby", Session["_UserCode"].ToString());
            //MyCommand.Parameters.AddWithValue("p_ipaddress", IpAddress);
            //dt = bll.SelectAll(MyCommand);
            //if (dt.TableName == "Success")
            //{
            //    if (dt.Rows.Count > 0)
            //    {
            //        return true;
            //    }
            //}
            return false;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    #endregion
    
    #region "Motheds"
    protected void lbtnSearch_Click(object sender, EventArgs e)
    {
        string ticketNo = tbTicketSearch.Text;
        if (ticketNo.Length < 10)
        {
            Errormsg("Please enter valid ticket Number.");
            return;
        }
        Session["TicketNumber"] = ticketNo;
        onload();
    }
    protected void grdcancelledtkt_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "PrintVoucher")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string p_cancellationrefno = grdcancelledtkt.DataKeys[index].Values["cancellation_ref_no"].ToString();
            Session["Cancel_voucher"] = p_cancellationrefno;
            openSubDetailsWindow("../E_cancellationvoucher.aspx");

        }
        if (e.CommandName == "Refund")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string MP_CANCELLEDBY = Session["_UserCode"].ToString();
            string p_cancellationrefno = grdcancelledtkt.DataKeys[index].Values["cancellation_ref_no"].ToString();
            if (refundtransaction(p_cancellationrefno, MP_CANCELLEDBY) == true)
            {
                //Successmsg("Refund Successfully");
            }
        }

    }
    protected void grdcancelledtkt_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView rowView = (DataRowView)e.Row.DataItem;
            LinkButton lbtnrefund = (LinkButton)e.Row.FindControl("lbtnrefund");
            if (!DBNull.Value.Equals(rowView["refund_ref_no"]))
            {
                lbtnrefund.Visible = false;
            }
            else
            {
                lbtnrefund.Visible = true;
            }
        }
    }
    #endregion

    protected void lbtnPrintTicket_Click(object sender, EventArgs e)
    {
        if (_security.isSessionExist(Session["TicketNumber"]) == true)
        {
            Session["p_ticketNo"] = Session["TicketNumber"];
            PageOpen("e-Ticket", "../E_ticket.aspx");
            //openSubDetailsWindow("../E_ticket.aspx");
        }
        else
        {
            Response.Redirect("errorpage.aspx");
        }
    }

protected void lbtnSMS_Click(object sender, EventArgs e)
    {
CommonSMSnEmail sms = new CommonSMSnEmail();
                sms.sendTicketConfirm_SMSnEMAIL(Session["TicketNumber"].ToString(), Session["_UserCode"].ToString(), "");

}

}