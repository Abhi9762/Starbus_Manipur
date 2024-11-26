using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_AgsplTicketCancel : System.Web.UI.Page
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
        if (!IsPostBack)
        {

            lblcancellationpolicy.Text = _common.getCancellationpolicy();
            cecancellationdate.EndDate = DateTime.Now.Date;
            txtcancellationdate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

            GetAvailableTicket(txtticketno.Text);
        }
    }

    #region "Methods"
    private void GetAvailableTicket(string ticketNo)
    {
        DataTable dt = new DataTable();
        wsClass obj = new wsClass();
        //dt = obj.GetAvailableTicketForRefund("0", ticketNo, "A");
        //if (dt.Rows.Count > 0)
        //{
        //    lblTicketNo.Text = dt.Rows[0]["ticket_no"].ToString();
        //    lblSource.Text = dt.Rows[0]["src"].ToString();
        //    lblDestination.Text = dt.Rows[0]["dest"].ToString();
        //    lblJourneyDate.Text = dt.Rows[0]["journey_date"].ToString();
        //    lblJourneyTime.Text = dt.Rows[0]["depart"].ToString();
        //    lblServiceType.Text = dt.Rows[0]["busservice_name"].ToString();
        //    lblcancellationdt.Text = dt.Rows[0]["cancel_date"].ToString();
        //    lblcancelledby.Text = dt.Rows[0]["cancelledby"].ToString();
        //    lblrefundamt.Text = dt.Rows[0]["cancellationamount"].ToString();
        //    Session["Cancel_ref_no"] = dt.Rows[0]["cancellationrefno"];
        //    GetPassenger(dt.Rows[0]["ticket_no"].ToString());
        //    pnlticketdetails.Visible = true;
        //    pnlnoticketdetails.Visible = false;
        //}
        //else
        //{
        //    pnlticketdetails.Visible = false;
        //    pnlnoticketdetails.Visible = true;
        //    lblValidationMsg.Text = "!! Sorry, Not Valid Ticket !!";
        //}
    }
    private void GetPassenger(string ticketNo)
    {
        try
        {
            DataTable dt = new DataTable();
            wsClass obj = new wsClass();
            dt = obj.getPassengerDetails(ticketNo, "C");
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
    private void Alert(string msg)
    {
        lblerro.Text = msg;
        mperror.Show();
    }
    private void ResetControl()
    {
        lblValidationMsg.Text = "!! Search and Cancel Ticket !!";
        pnlnoticketdetails.Visible = true;
        pnlticketdetails.Visible = false;
        txtticketno.Text = "";
    }
    public void OpenSubDetailsWindow(string MModuleName)
    {
        try
        {
           
            string murl = MModuleName + "?rt=" + DateTime.Now.ToString();

            if (Request.Browser.Type.ToUpper().Trim().PadLeft(2) == "IE")
            {
                Response.Write("<SCRIPT language='javascript'>window.showModalDialog('" + murl + "','name','dialogWidth:830px;dialogHeight:550px');</script>");
            }
            else
            {
                string fullURL = "window.open('" + murl + "', '_blank', 'height=550,width=830,status=no,toolbar=no,menubar=no,location=no,scrollbars=no,resizable=no,titlebar=no' );";
                string script = "window.open('" + fullURL + "','');";
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
    private void SpecialTicketCancel(string ticketno)//M3
    {
        try
        {
            pnlticketdetails.Visible = false;
            string bookby = Session["_usercode"].ToString();
            wsClass obj = new wsClass();
            dt = obj.getSplCancelledTicketsNew(bookby, ticketno, "A");

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

              //  pnlNoRecord.Visible = false;
            }
            else
            {

                pnlticketdetails.Visible = false;
              //  pnlNoRecord.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
           // pnlNoRecord.Visible = true;
            
        }
    }
    private void CancelledTickets(string tktno)//M1
    {
        try
        {
            string cancelby = Session["_usercode"].ToString();
            wsClass obj = new wsClass();
            dt = obj.getRefundedTicket(cancelby, tktno);
            if (dt.Rows.Count > 0)
            {
                grdcancelledtkt.DataSource = dt;
                grdcancelledtkt.DataBind();
                grdcancelledtkt.Visible = true;
                pnlancelledtktmsg.Visible = false;
            }
            else
            {
                grdcancelledtkt.Visible = false;
                pnlancelledtktmsg.Visible = true;
            }
        }
        catch (Exception ex)
        {
            grdcancelledtkt.Visible = false;
            pnlancelledtktmsg.Visible = true;
           
        }
    }
    #endregion

    #region "Event"
    protected void btninfo_Click(object sender, EventArgs e)
    {
        mpcancellationpolicy.Show();
    }
    protected void grdcancelledtkt_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdcancelledtkt.PageIndex = e.NewPageIndex;
        CancelledTickets(txtcancelledtktno.Text);
    }
    protected void grdcancelledtkt_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "PrintVoucher")
            {
                Session["CANCELLATIONREFNO"] = grdcancelledtkt.DataKeys[index].Values["cancel_ref_no"];
                Session["TicketNumber"] = grdcancelledtkt.DataKeys[index].Values["ticketno"];
                Session["Cancel_voucher"] = Session["CANCELLATIONREFNO"];
                OpenSubDetailsWindow("../E_cancellationvoucher.aspx");
            }
        }
        catch (Exception ex)
        {
            Alert(ex.Message);
        }
    }
    protected void btnsearchcancelledtkt_Click(object sender, EventArgs e)
    {
        if (_validation.IsValidString(txtcancellationdate.Text, 10, txtcancellationdate.MaxLength) == false)
        {
            string msg = "Enter Ticket Cancellation Date";
            Alert(msg);
            return;
        }

        
        CancelledTickets(txtcancelledtktno.Text.ToString());

        //GetRefundedTkt(txtcancelledtktno.Text);
    }
    protected void btnresetcancelledtkt_Click(object sender, EventArgs e)
    {
        txtcancelledtktno.Text = "";
        CancelledTickets(txtcancelledtktno.Text);
    }
    
    protected void lbtnresetticket_Click(object sender, EventArgs e)
    {
        ResetControl();
    }
    protected void lbtnsearchticket_Click(object sender, EventArgs e)
    {
        if (!_validation.IsValidString(txtticketno.Text, 15, 20))
        {
            string msg = "Enter Valid Ticket";
            Alert(msg);
            return;
        }
        Session["p_ticketNo"] = txtticketno.Text.ToString().Trim();
        SpecialTicketCancel(Session["p_ticketNo"].ToString());
    }
    protected void btnRefundTicket_Click(object sender, EventArgs e)
    {
        lblConfirmation.Text = "Are you sure, you want to refund ticket amount?";
        mpConfirmations.Show();
    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        string MP_CANCELLEDBY = Session["_usercode"].ToString();
        string p_cancellationrefno = Session["Cancel_ref_no"].ToString();
        //    refundtransaction(p_cancellationrefno, MP_CANCELLEDBY);
        wsClass obj = new wsClass();
        DataTable dt = obj.refundtransactionnew(p_cancellationrefno, Session["Ticket_no"].ToString(), "A", MP_CANCELLEDBY, Session["_UserCode"].ToString());
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["cancellationrefno"].ToString() != "EXCEPTION")
            {
                pnlticketdetails.Visible = false;
                pnlnoticketdetails.Visible = true;
                txtticketno.Text = "";
                Alert("Ticket Amount Refunded Successfully");
            }
            else
            {
                Alert("Something Went Wrong");
            }
        }
        else
        {
            Alert("Something Went Wrong");
        }

    }
    #endregion


}