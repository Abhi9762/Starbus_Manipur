using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_AgTicketCancel : System.Web.UI.Page
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    string msg = "";
    string commonerror = "There is some error. Please contact the helpdesk or try again after sometime.";
    wsClass obj = new wsClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
          //  txtcancellationdate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            lblcancellationpolicy.Text = _common.getCancellationpolicy();

            if (Session["p_ticketNo"] != null && Session["p_ticketNo"].ToString() != "")
            {
                txtcancelledtktno.Text = Session["p_ticketNo"].ToString();
              //  txtcancellationdate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            }
            getcancelledtkt(txtcancelledtktno.Text.ToString());

        }

    }
    #region "Cancelled Ticket"
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
    protected void btnsearchcancelledtkt_Click(object sender, EventArgs e)
    {
       

        getcancelledtkt(txtcancelledtktno.Text.ToString());

    }
    private void alert(string msg)
    {
        lblerro.Text = msg;
        mperror.Show();
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
                    alert(commonerror);
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
    private void resetcntrl()
    {
        lblValidationMsg.Text = "!! Search and Cancel Ticket !!";
        pnlnoticketdetails.Visible = true;
        pnlticketdetails.Visible = false;
        txtticketno.Text = "";
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
    private void getAvailableTicket(string ticketno)
    {
        DataTable dt = new DataTable();
        wsClass obj = new wsClass();
        dt = obj.getAvailableTicket("0", ticketno, "A");
        if (dt.Rows.Count > 0)
        {
            lblTicketNo.Text = dt.Rows[0]["ticket_no"].ToString();
            lblSource.Text = dt.Rows[0]["src"].ToString();
            lblDestination.Text = dt.Rows[0]["dest"].ToString();
            lblJourneyDate.Text = dt.Rows[0]["journey_date"].ToString();
            lblJourneyTime.Text = dt.Rows[0]["depart"].ToString();
            lblServiceType.Text = dt.Rows[0]["busservice_name"].ToString();
            GetPassenger(dt.Rows[0]["ticket_no"].ToString());
            pnlticketdetails.Visible = true;
            pnlnoticketdetails.Visible = false;
        }
        else
        {
            pnlticketdetails.Visible = false;
            pnlnoticketdetails.Visible = true;
            lblValidationMsg.Text = " !! Sorry , Not Valid Ticket !!";
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
    private bool validvalue()//M3
    {
        CheckBox CchkBox;
        string seatNo = "";
      
        string seatamt = "";
        int i = 0;
       

        foreach (GridViewRow mygridViewRow in grdticketpassenger.Rows)
        {
            if (mygridViewRow.RowType == DataControlRowType.DataRow)
            {
                CchkBox = mygridViewRow.FindControl("CheckBox1") as CheckBox;

                if (CchkBox.Checked == true)
                {
                    seatNo = grdticketpassenger.DataKeys[mygridViewRow.RowIndex]["seatno"].ToString() + "," + seatNo;
                    seatamt = grdticketpassenger.DataKeys[mygridViewRow.RowIndex]["amountrefunded"].ToString() + "," + seatamt;
                    i = i + 1;
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
    #endregion


    #region "Event"
    protected void btnCancelTicket_Click(object sender, EventArgs e)
    {
        if (!validvalue())
        {
            alert(msg);
            return;
        }
        mp2.Show();

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        resetcntrl();
    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        try
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
            MP_CANCELLEDBY = Session["_usercode"].ToString().Trim();
            DataTable dt = new DataTable();
            dt = obj.cancelTicket(MP_TicketNo, seatNo, seatamt, i, MP_CANCELLEDBY, IpAddress, "A","W");

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["cancellationrefno"].ToString() == "ERROR")
                {
                    msg = "Ticket cannot be cancelled now.";
                    alert(msg);
                    return;
                }
                else
                {
                    Session["CANCELLATIONREFNO"] = dt.Rows[0]["cancellationrefno"];
                    Session["TicketNumber"] = MP_TicketNo;
                    resetcntrl();
                    obj.refundtransactionnew(dt.Rows[0]["cancellationrefno"].ToString(), MP_TicketNo, "A", Session["_usercode"].ToString(), Session["_usercode"].ToString());

                    Session["Cancel_voucher"] = dt.Rows[0]["cancellationrefno"].ToString();
                    getcancelledtkt(txtcancelledtktno.Text.ToString());
                    openSubDetailsWindow("../E_cancellationvoucher.aspx");
                }
            }
            else
            {
                msg = "Ticket Can not Be cancelled now. Please Try again"; // + ex.Message
                alert(msg);
                return;
            }
        }
        catch (Exception ex)
        {
            msg = "Ticket Can not Be cancelled now. Please Try again"; // + ex.Message
            alert(msg);
            return;
        }

    }
    protected void btninfo_Click(object sender, EventArgs e)
    {
        mpcancellationpolicy.Show();
    }
    protected void lbtnsearchticket_Click(object sender, EventArgs e)
    {
        if (_validation.IsValidString(txtticketno.Text, 10, 20) == false)

        {
            msg = "Enter Valid Ticket";
            alert(msg);
            return;
        }
        getAvailableTicket(txtticketno.Text);

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
                openSubDetailsWindow("../E_cancellationvoucher.aspx");
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
    protected void grdcancelledtkt_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbtnRefund = (LinkButton)e.Row.FindControl("lbtnRefund");
            DataRowView rowView = (DataRowView)e.Row.DataItem;
            if (rowView.Row.IsNull("refund_ref_no"))
            {
                lbtnRefund.Visible = true;
            }
            else
            {
                lbtnRefund.Visible = false;
            }
        }

    }
    protected void grdcancelledtkt_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdcancelledtkt.PageIndex = e.NewPageIndex;
        getcancelledtkt(txtcancelledtktno.Text.ToString());
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
    protected void lbtnresetticket_Click(object sender, EventArgs e)
    {
        resetcntrl();
    }
    #endregion
}