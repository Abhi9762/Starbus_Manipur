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

public partial class Auth_CntrTicketCancellation : BasePage
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    string commonerror = "There is some error. Please contact the helpdesk or try again after sometime.";

    protected void Page_Load(object sender, EventArgs e)
    {
        Session["_moduleName"] = "Ticket Cancellation";
        if (!IsPostBack)
        {
            Random randomclass = new Random();
            Session["rndNoCheck"] = randomclass.Next().ToString();
            hidtoken.Value = Session["rndNoCheck"].ToString();
            lblcancellationpolicy.Text = _common.getCancellationpolicy();

            if (Session["p_ticketNo"] != null && Session["p_ticketNo"].ToString() != "")
            {
               
                tbticketno.Text = Session["p_ticketNo"].ToString();
                LoadTicketDetails(Session["p_ticketNo"].ToString().Trim());
            }
            CancelledTickets(tbcancelledpnr.Text.ToString());

        }
    }


    #region "Method"
    private void CsrfTokenValidate()
    {
        if (hidtoken.Value != Session["rndNoCheck"].ToString())
        {
            Response.Redirect("errorpage.aspx");
            return;
        }
    }
    private void CancelledTickets(string tktno)//M1
    {
        try
        {
            string cancelby = Session["_UserCntrID"].ToString();
            wsClass obj = new wsClass();
            dt = obj.getCancelledTickets(cancelby, tktno);
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
            _common.ErrorLog("CntrTicketCancellation.aspx-0001", ex.Message.ToString());
        }
    }

    private void LoadTicketDetails(string ticketno)//M2
    {
        try
        {
            pnltktdetails.Visible = false;
            pnlNoRecord.Visible = true;
            wsClass obj = new wsClass();
            
            DataTable dt = obj.getAvailableTicketForCancel(ticketno, "A","C");
            if (dt.Rows.Count > 0)
            {
                lblUsr.Text = dt.Rows[0]["booked_by"].ToString()+"("+ dt.Rows[0]["bookbyname"].ToString()+")";
                lblTotal.Text = dt.Rows[0]["amount_total"].ToString();
                lblPNRNO.Text = dt.Rows[0]["_ticketno"].ToString();
                lblService.Text = dt.Rows[0]["depot_servicecode"].ToString() + dt.Rows[0]["service_type_name"].ToString();
                lblDistance.Text = dt.Rows[0]["total_distance"].ToString();
                lblScheduledDeparture.Text = dt.Rows[0]["trip_time"].ToString();
                lblJourneyDate.Text = dt.Rows[0]["journeydate"].ToString();
                lblFare.Text = dt.Rows[0]["amount_fare"].ToString();
                lblFrom.Text = dt.Rows[0]["fromstn_name"].ToString();
                lblScheduledArrival.Text = dt.Rows[0]["tripend_time"].ToString();
                lblRes.Text = dt.Rows[0]["amount_onl_reservation"].ToString();
                lblTo.Text = dt.Rows[0]["tostn_name"].ToString();
                lblBookingDate.Text = dt.Rows[0]["bookingdatetime"].ToString();
                lbltottax.Text = dt.Rows[0]["amount_tax"].ToString();
                pnltktdetails.Visible = true;
                pnlNoRecord.Visible = false;
                // lbltotrefamt.Text = "0";
            }

            else
            {
                pnltktdetails.Visible = false;
                pnlNoRecord.Visible = true;
                return;
            }

            DataTable dtt = obj.getPassengerDetailsForCancel(ticketno, "A");
            if (dtt.Rows.Count > 0)
            {
                gvTicketseatDetails.DataSource = dtt;
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
            _common.ErrorLog("CntrTicketCancellation.aspx-0002", ex.Message.ToString());
            pnltktdetails.Visible = false;
            pnlNoRecord.Visible = true;
            return;
        }
    }
    private bool validvalue()//M3
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
            _common.ErrorLog("CntrTicketCancellation-M3", ex.Message.ToString());
            return false;
        }
    }

    private void cancelTicket()//M4
    {
        try
        {

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
           
            MP_TicketNo = lblPNRNO.Text;
            MP_CANCELLEDBY = Session["_UserCntrID"].ToString();

            wsClass obj = new wsClass();
            DataTable dataTable = obj.cancelTicket(MP_TicketNo, seatNo, seatamt, i, MP_CANCELLEDBY, IpAddress, "C","W");
            if (dataTable.Rows.Count > 0)
            {
                string p_cancellationrefno = dataTable.Rows[0]["cancellationrefno"].ToString();

                if (p_cancellationrefno == "ERROR" || p_cancellationrefno == "EXCEPTION")
                {
                    Errormsg(commonerror);
                    return;
                }
                else
                {
                   DataTable dt =obj.refundtransactionnew(p_cancellationrefno, MP_TicketNo, "C", Session["_UserCntrID"].ToString());
                    pnltktdetails.Visible = false;
                    pnlNoRecord.Visible = true;
                    Successmsg("Ticket Cancellation Successfully!");
                    Session["Cancel_voucher"] = p_cancellationrefno;
                    openSubDetailsWindow("../E_cancellationvoucher.aspx");
                    LoadTicketDetails(Session["p_ticketNo"].ToString());
                    //refundtransaction(p_cancellationrefno, MP_CANCELLEDBY);
                    CancelledTickets("");
                }


            }
            else
            {
                Errormsg(commonerror);
                _common.ErrorLog("CntrTicketCancellation.aspx-0003", dataTable.TableName);
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("CntrTicketCancellation.aspx-0004", ex.Message.ToString());
        }
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
                    Errormsg(commonerror);
                    return;
                }
                else
                {
                    Successmsg("Refund Successfully");
                    CancelledTickets(tbcancelledpnr.Text.ToString());
                }
            }
            else
            {
                _common.ErrorLog("CntrTicketCancellation.aspx-0005", dt.TableName);
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("CntrTicketCancellation.aspx-0006", ex.Message.ToString());
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
            _common.ErrorLog("CntrTicketCancellation.aspx-0007", ex.Message.ToString());
        }
    }
    #endregion

    #region "Event"
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
    protected void lbtnSearchTicketNo_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (_validation.IsValidString(tbticketno.Text, 10, tbticketno.MaxLength) == false)
        {
            Errormsg("Enter Valid Ticket Number");
            return;
        }
        Session["p_ticketNo"] = tbticketno.Text.ToString().Trim();
        LoadTicketDetails(Session["p_ticketNo"].ToString());
    }
    protected void gvTicketseatDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {


        }
    }
    protected void chkseats_CheckedChanged(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        CheckBox chk = (CheckBox)sender;
        GridViewRow row = (GridViewRow)chk.NamingContainer;
        Int64 refmat = 0;
        refmat = Convert.ToInt64(gvTicketseatDetails.DataKeys[row.RowIndex]["amountrefunded"].ToString());
        //if (chk.Checked == true)
        //{
        //    lbltotrefamt.Text = (Convert.ToInt64(lbltotrefamt.Text.ToString()) + refmat).ToString();
        //}
        //else
        //{
        //    lbltotrefamt.Text = (Convert.ToInt64(lbltotrefamt.Text.ToString()) - refmat).ToString();
        //}
    }
    protected void lbtnRestTicketNo_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        tbticketno.Text = "";
        pnltktdetails.Visible = false;
        pnlNoRecord.Visible = true;
    }
    protected void lbtncanceltkt_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (validvalue() == false)
        {
            return;
        }
        lblConfirmation.Text = "Are You sure, You Want To Cancel This Ticket";
        mpConfirmation.Show();
    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        cancelTicket();
    }
    protected void grdcancelledtkt_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        CsrfTokenValidate();
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
            string MP_CANCELLEDBY = Session["_CntrCode"].ToString();
            string p_cancellationrefno = grdcancelledtkt.DataKeys[index].Values["cancellation_ref_no"].ToString();
            refundtransaction(p_cancellationrefno, MP_CANCELLEDBY);
        }

    }
    protected void grdcancelledtkt_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdcancelledtkt.PageIndex = e.NewPageIndex;
        CancelledTickets(tbcancelledpnr.Text.ToString());
    }
    protected void lbtncacnelledpnrsearch_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (_validation.IsValidString(tbcancelledpnr.Text, 10, tbcancelledpnr.MaxLength) == false)
        {
            Errormsg("Enter Valid Ticket Number");
            return;
        }
        CancelledTickets(tbcancelledpnr.Text.ToString());
    }
    protected void lbtncacnelledpnrreset_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        tbcancelledpnr.Text = "";
        CancelledTickets(tbcancelledpnr.Text.ToString());
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
                lbtnrefund.Visible = false;
            }
        }
    }
    protected void lbtnok_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        tbticketno.Text = "";
        pnltktdetails.Visible = false;
        pnlNoRecord.Visible = true;
        Session["Cancel_voucher"] = null;
    }
    #endregion






}