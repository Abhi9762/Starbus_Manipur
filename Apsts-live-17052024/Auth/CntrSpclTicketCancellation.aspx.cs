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

public partial class Auth_CntrSpclTicketCancellation : BasePage
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
        Session["_moduleName"] = "Special Ticket Cancellation";
        if (!IsPostBack)
        {
            Random randomclass = new Random();
            Session["rndNoCheck"] = randomclass.Next().ToString();
            hidtoken.Value = Session["rndNoCheck"].ToString();

            lblcancellationpolicy.Text = _common.getCancellationpolicy();

            if (Session["p_ticketNo"] != null && Session["p_ticketNo"].ToString() != "")
            {
                tbticketno.Text = Session["p_ticketNo"].ToString();
                // LoadTicketDetails(Session["p_ticketNo"].ToString().Trim());
            }
            // CancelledTickets(tbcancelledpnr.Text.ToString());
            // SpecialTicketCancel(tbticketno.Text.ToString());
        }
    }

    #region "Event"
    protected void btnRefundTicket_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        lblConfirmation.Text = "Are you sure, you want to refund ticket amount?";
        mpConfirmation.Show();
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
            if (!DBNull.Value.Equals(rowView["refund_refno"]))
            {
                lbtnrefund.Visible = false;
            }
            else
            {
                lbtnrefund.Visible = true;
            }
        }
    }
    protected void grdcancelledtkt_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        CsrfTokenValidate();
        if (e.CommandName == "PrintVoucher")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string p_cancellationrefno = grdcancelledtkt.DataKeys[index].Values["cancel_ref_no"].ToString();
            Session["Cancel_voucher"] = p_cancellationrefno;
            openSubDetailsWindow("../E_cancellationvoucher.aspx");

        }


    }
    protected void grdcancelledtkt_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdcancelledtkt.PageIndex = e.NewPageIndex;
        CancelledTickets(tbcancelledpnr.Text.ToString());
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
        SpecialTicketCancel(Session["p_ticketNo"].ToString());

    }


    protected void lbtnback_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        pnlticketdetails.Visible = false;
        SpecialTicketCancel(tbticketno.Text.ToString());
    }
    protected void lbtnRestTicketNo_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        tbticketno.Text = "";
        pnlticketdetails.Visible = false;
        SpecialTicketCancel(tbticketno.Text.ToString());

    }
    protected void gvTicketseatDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void lbtncanceltkt_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        mpConfirmation.Show();
    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        string MP_CANCELLEDBY = Session["_UserCntrID"].ToString();
        string p_cancellationrefno = Session["Cancel_ref_no"].ToString();
        //    refundtransaction(p_cancellationrefno, MP_CANCELLEDBY);
        wsClass obj = new wsClass();
        DataTable dt = obj.refundtransactionnew(p_cancellationrefno, Session["Ticket_no"].ToString(), "C", MP_CANCELLEDBY, Session["_UserCode"].ToString());
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["cancellationrefno"].ToString() != "EXCEPTION")
            {
                pnlticketdetails.Visible = false;
                pnlNoRecord.Visible = true;
                tbticketno.Text = "";
                Successmsg("Ticket Amount Refunded Successfully");
            }
            else
            {
                Errormsg("Something Went Wrong");
            }
        }
        else
        {
            Errormsg("Something Went Wrong");
        }

    }
    protected void lbtnok_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
    }
    #endregion

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
            dt = obj.getRefundedTicket(cancelby, tktno);
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
            _common.ErrorLog("CntrSpclTicketCancellation.aspx-0001", ex.Message.ToString());
        }
    }
    private void refundtransaction(string cancel_refNo, string refundedby)//M2
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
                    pnlticketdetails.Visible = false;
                    SpecialTicketCancel(tbticketno.Text.ToString());

                }
            }
            else
            {

                _common.ErrorLog("CntrSpclTicketCancellation.aspx-0002", dt.TableName);
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("CntrSpclTicketCancellation.aspx-0003", ex.Message.ToString());
        }
    }
    private void SpecialTicketCancel(string ticketno)//M3
    {
        try
        {
            pnlticketdetails.Visible = false;
            string bookby = Session["_UserCntrID"].ToString();
            wsClass obj = new wsClass();
            dt = obj.getSplCancelledTicketsNew("0", ticketno, "C");

            if (dt.Rows.Count > 0)
            {
                lblTicketNo.Text = dt.Rows[0]["ticket_no"].ToString();
                lblSource.Text = dt.Rows[0]["src"].ToString();
                lblDestination.Text = dt.Rows[0]["dest"].ToString();
                lblJourneyDate.Text = dt.Rows[0]["journey_date"].ToString();
                lblJourneyTime.Text = dt.Rows[0]["departure_time"].ToString();
                lblServiceType.Text = dt.Rows[0]["busservice_name"].ToString();
                lblcancellationdt.Text = dt.Rows[0]["cancel_date"].ToString();
                lblcancelledby.Text = dt.Rows[0]["cancelledby"].ToString();
                lblrefundamt.Text = dt.Rows[0]["cancel_amt"].ToString();
                Session["Cancel_ref_no"] = dt.Rows[0]["cancel_refno"].ToString();
                Session["Ticket_no"] = dt.Rows[0]["ticket_no"].ToString();
                dt = obj.getPassengerDetails(lblTicketNo.Text, "C");
                grdticketpassenger.DataSource = dt;
                grdticketpassenger.DataBind();
                pnlticketdetails.Visible = true;

                pnlNoRecord.Visible = false;
            }
            else
            {
Errormsg("Invalid Ticket, May Be Ticket Has Been Already Cancelled/Refund");
                pnlticketdetails.Visible = false;
                pnlNoRecord.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
            pnlNoRecord.Visible = true;
            _common.ErrorLog("CntrSpclTicketCancellation.aspx-0004", ex.Message.ToString());
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
            _common.ErrorLog("CntrSpclTicketCancellation.aspx-0005", ex.Message.ToString());
        }
    }
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
    #endregion




    
}