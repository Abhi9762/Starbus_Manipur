
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_SubCscsplrefund : System.Web.UI.Page
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
        Session["_ModuleName"] = "Ticket Special Refund" + " - (" + Session["_UserName"] + " " + Session["_UserCode"] + ")";

        if (!IsPostBack)
        {
            txtcancellationdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            
            GetRefundedTkt();
           // ClassCancellationPolicy cncpolicy = new ClassCancellationPolicy();
            //lblcancellationpolicy.Text = cncpolicy.GetPolicy();
        }

    }


    #region "Methods"
   private void OpenPage(string pageName)
    {
        tkt.Src = pageName;
        mpticket.Show();
    }

    public void ErrorMessage(string msg)
    {
        lblerrormsg.Text = msg;
        mperror.Show();
    }


    private void GetRefundedTkt()
    {
        try
        {
            DataTable dtCancelled = new DataTable();
            wsClass obj = new wsClass();
            dtCancelled = obj.getRefundedTicket(Session["_UserCode"].ToString(), txtcancelledtktno.Text.Trim());
            if (dtCancelled.Rows.Count > 0)
            {
                grdcancelledtkt.DataSource = dtCancelled;
                grdcancelledtkt.DataBind();
                grdcancelledtkt.Visible = true;
                lblcancelledtktmsg.Visible = false;
            }
            else
            {
                grdcancelledtkt.Visible = false;
                lblcancelledtktmsg.Visible = true;
            }
        }
        catch (Exception ex)
        {
            grdcancelledtkt.Visible = false;
            lblcancelledtktmsg.Visible = true;
        }
    }


    private void GetAvailableTicket(string ticketNo)
    {
        DataTable dt = new DataTable();
        wsClass obj = new wsClass();
        dt = obj.getSplCancelledTicketsNew(Session["_UserCode"].ToString(), ticketNo, "A");
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
            Session["Cancel_ref_no"] = dt.Rows[0]["cancel_refno"];
            GetPassenger(dt.Rows[0]["ticket_no"].ToString());
            pnlticketdetails.Visible = true;
            pnlnoticketdetails.Visible = false;

        }
        else
        {
            pnlticketdetails.Visible = false;
            pnlnoticketdetails.Visible = true;
            lblValidationMsg.Text = "!! Sorry, Not Valid Ticket !!";
        }
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

    #endregion



    #region "Events"
    protected void lbtnsearchticket_Click(object sender, EventArgs e)
    {
        

        GetAvailableTicket(txtticketno.Text);

    }

    protected void lbtnresetticket_Click(object sender, EventArgs e)
    {
        txtticketno.Text = "";
        Session["Cancel_ref_no"] = "";
        pnlticketdetails.Visible = false;
        pnlnoticketdetails.Visible = true;
    }


  

    protected void btnsearchcancelledtkt_Click(object sender, EventArgs e)
    {
        if (!_validation.IsValidString(txtcancellationdate.Text, 10, 10))
        {
            ErrorMessage("Enter Valid Ticket Cancellation Date");
            return;
        }
       
        GetRefundedTkt();

        if (grdcancelledtkt.Rows.Count <= 0)
        {
            ErrorMessage("No Cancelled Ticket Available");
            return;
        }

    }

    protected void btnresetcancelledtkt_Click(object sender, EventArgs e)
    {
        txtcancellationdate.Text = DateTime.Now.ToString();
        txtcancelledtktno.Text = "";
        GetRefundedTkt();
    }


  

    protected void grdcancelledtkt_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdcancelledtkt.PageIndex = e.NewPageIndex;
        GetRefundedTkt();
    }

    

    protected void grdcancelledtkt_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "PrintVoucher")
            {
                Session["CANCELLATIONREFNO"] = grdcancelledtkt.DataKeys[index].Values["CANCELLATION_REF_NO"];
                Session["TicketNumber"] = grdcancelledtkt.DataKeys[index].Values["TICKET_NO"];
                OpenPage("../E_cancellationvoucher.aspx");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage(ex.Message);
        }

    }

    protected void btnRefundTicket_Click(object sender, EventArgs e)
    {
        mpconfirmation.Show();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        txtticketno.Text = "";
        Session["Cancel_ref_no"] = "";
        pnlticketdetails.Visible = false;
        pnlnoticketdetails.Visible = true;
    }

    protected void lbtnyes_Click(object sender, EventArgs e)
    {
        try
        {
            string MP_TicketNo;
            string MP_REFUNDDBY;
            string CANCELLATION_REF_NO;

            MP_TicketNo = lblTicketNo.Text.Trim();
            MP_REFUNDDBY = Session["_UserCode"].ToString().Trim();
            CANCELLATION_REF_NO = Session["Cancel_ref_no"].ToString().Trim();
            wsClass obj = new wsClass();
            dt = obj.refundtransactionnew(CANCELLATION_REF_NO, MP_TicketNo, "A", Session["_UserCode"].ToString(), Session["_UserCode"].ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                ErrorMessage("Special refund successfully proceed");
                GetRefundedTkt();
                txtticketno.Text = "";
                Session["Cancel_ref_no"] = "";
                pnlticketdetails.Visible = false;
                pnlnoticketdetails.Visible = true;
            }
        }
        catch (Exception ex)
        {
            ErrorMessage("Ticket Cannot Be Cancelled Now. Please Try Again"); // + ex.Message
            return;
        }

    }

    #endregion
}