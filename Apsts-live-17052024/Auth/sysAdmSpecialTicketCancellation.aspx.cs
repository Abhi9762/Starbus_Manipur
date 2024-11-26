using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_sysAdmSpecialTicketCancellation : BasePage
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
        Session["_moduleName"] = "Special Ticket Cancellation";
        getSpclcancelTkt(Session["_UserCode"].ToString());
        if (!IsPostBack)
        {
            Random randomclass = new Random();
            Session["rndNoCheck"] = randomclass.Next().ToString();
            hidtoken.Value = Session["rndNoCheck"].ToString();
        }
    }

    #region "Methods"
    private void CsrfTokenValidate()
    {
        if (hidtoken.Value != Session["rndNoCheck"].ToString())
        {
            Response.Redirect("errorpage.aspx");
            return;
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
    private void ConfirmMsg(string msg)
    {
        lblConfirmation.Text = msg;
        mpConfirmation.Show();
    }
    private void InfoMsg(string msg)
    {
        string popup = _popup.modalPopupSmall("I", "Information", msg, "Close");
        Response.Write(popup);
    }
    private void getticketdetails(string tktno)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_get_get_tkt_spclcancel");
            MyCommand.Parameters.AddWithValue("p_ticketno", tktno);
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["Current_status"].ToString() != "A")
                {
                    Errormsg("Invalid Ticket Number or Ticket has already been cancelled");
                    return;
                }
                else
                {
                    lblTicketNo.Text = dt.Rows[0]["ticketno"].ToString();
                    lblSource.Text = dt.Rows[0]["src"].ToString();
                    lblDestination.Text = dt.Rows[0]["dest"].ToString();
                    lblJourneyDate.Text = dt.Rows[0]["journeyDate"].ToString();
                    lblJourneyTime.Text = dt.Rows[0]["depttime"].ToString();
                    lblServiceType.Text = dt.Rows[0]["servicename"].ToString();
                    lblbookedby.Text = dt.Rows[0]["bookedby"].ToString();
                    Session["ctzmobileno"] = dt.Rows[0]["travellermobile"].ToString();
                    Session["bookedbyuserid"] = dt.Rows[0]["bookybyusercode"].ToString();
                    dt.Clear();
                    MyCommand = new NpgsqlCommand();
                    MyCommand.Parameters.Clear();
                    MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_get_tkt_spclcancelseats");
                    MyCommand.Parameters.AddWithValue("p_ticketno", lblTicketNo.Text);
                    dt = bll.SelectAll(MyCommand);
                    if (dt.Rows.Count > 0)
                    {
                        grdticketpassenger.DataSource = dt;
                        grdticketpassenger.DataBind();
                        pnlnoticketdetails.Visible = false;
                        pnlticketdetails.Visible = true;
                    }
                    else
                    {
                        Errormsg("Invalid Ticket Number or Ticket has already been cancelled");
                    }
                }
            }
            else
            {
                Errormsg("Invalid Ticket Number.");
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("sysAdmSpecialTicketCancellation.aspx-0001", ex.Message.ToString());
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
            _common.ErrorLog("sysAdmSpecialTicketCancellation.aspx-0002", ex.Message.ToString());
        }
    }
    private void getSpclcancelTkt(string usercode)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.get_spclledtkt_cancel");
            MyCommand.Parameters.AddWithValue("p_usercode", usercode);
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                gvspclcancelledtkt.DataSource = dt;
                gvspclcancelledtkt.DataBind();
                dvnospclcancelledtkt.Visible = false;
                gvspclcancelledtkt.Visible = true;
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("sysAdmSpecialTicketCancellation.aspx-0003", ex.Message.ToString());
        }
        
    }
    #endregion

    #region "Events"
    protected void ChkSelect_CheckedChanged(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        CheckBox chkbox = sender as CheckBox;
        if (chkbox.Checked == true)
        {
            GridViewRow parentRow = chkbox.NamingContainer as GridViewRow;
            int i = parentRow.RowIndex;
            TextBox txtRefund = (TextBox)grdticketpassenger.Rows[i].FindControl("txtRefAmt");
            // txtRefund.Attributes.Add("onKeyPress", "return KeyPressNumbersonly(event);")
            // Dim ReqValid As FilteredTextBoxExtender = DirectCast(grdticketpassenger.Rows(i).FindControl("FE_RefAmt"), FilteredTextBoxExtender)
            txtRefund.Visible = true;
            // ReqValid.Visible = True
            txtRefund.Focus();
        }
        else
        {
            GridViewRow parentRow = chkbox.NamingContainer as GridViewRow;
            int i = parentRow.RowIndex;
            TextBox txtRefund = (TextBox)grdticketpassenger.Rows[i].FindControl("txtRefAmt");
            txtRefund.Visible = false;
            txtRefund.Text = "";
        }
    }
    protected void lbtnsearchticket_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (_validation.IsValidString(txtticketno.Text, 10, 20) == false)
        {
            Errormsg("Enter Valid Ticket No.");
        }
        else
        {
            getticketdetails(txtticketno.Text);
        }

    }
    protected void lbtnresetticket_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
    }
    protected void btnCancelTicket_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        CheckBox CchkBox;
        TextBox txtRefund;
        int i = 0;
        int j = 0;
        foreach (GridViewRow row in grdticketpassenger.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CchkBox = (CheckBox)row.FindControl("ChkSelect");
                if (CchkBox.Checked == true)
                {
                    i = i + 1;
                    txtRefund = (TextBox)row.FindControl("txtRefAmt");
                    if (txtRefund.Text == "")
                    {
                        j = j + 1;
                    }
                }
            }
        }

        if (i <= 0)
        {
            Errormsg("Please select seat to cancel");
            return;
        }
        if (j > 0)
        {
            Errormsg("Please enter refund amount");
            return;
        }
        if (txtReson.Text.Length < 4)
        {
            Errormsg("Enter Valid Cancellation Reason before proceeding");
            return;
        }
        lblConfirmation.Text = "Do you want to proceed special cancelllation ?";
        mpConfirmation.Show();
    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        try
        {
            int MP_seatNo1 = 0;
            int MP_seatNo2 = 0;
            int MP_seatNo3 = 0;
            int MP_seatNo4 = 0;
            int MP_seatNo5 = 0;
            int MP_seatNo6 = 0;

            decimal MP_RefundAmt1 = 0;
            decimal MP_RefundAmt2 = 0;
            decimal MP_RefundAmt3 = 0;
            decimal MP_RefundAmt4 = 0;
            decimal MP_RefundAmt5 = 0;
            decimal MP_RefundAmt6 = 0;

            String MP_CANCELLEDBY = "";
            String McancellationReason = txtReson.Text;
            MP_CANCELLEDBY = Session["_UserCode"].ToString();
            for (var i = 0; i <= grdticketpassenger.Rows.Count - 1; i += 1)
            {
                CheckBox chkBx = (CheckBox)grdticketpassenger.Rows[i].FindControl("ChkSelect");
                if (chkBx != null && chkBx.Checked)
                {
                    TextBox txtRefund = (TextBox)grdticketpassenger.Rows[i].FindControl("txtRefAmt");
                    Label lblseatno = (Label)grdticketpassenger.Rows[i].FindControl("lblseatno");
                    Label lblAMOUNT_FARE = (Label)grdticketpassenger.Rows[i].FindControl("lblAMOUNT_FARE");
                    Int16 intSeatNo = Convert.ToInt16(lblseatno.Text.ToString());

                    string amnt = grdticketpassenger.DataKeys[i]["fare_res"].ToString();

                    if (Convert.ToDecimal(txtRefund.Text) > Convert.ToDecimal(amnt))
                    {
                        Errormsg("Refund amount should be less than fare amount");
                        txtRefund.Focus();
                        return;
                    }
                    switch (i)
                    {
                        case 0:
                            {
                                MP_seatNo1 = intSeatNo;
                                MP_RefundAmt1 = Convert.ToInt32(txtRefund.Text);
                                break;
                            }

                        case 1:
                            {
                                MP_seatNo2 = intSeatNo;
                                MP_RefundAmt2 = Convert.ToInt32(txtRefund.Text);
                                break;
                            }

                        case 2:
                            {
                                MP_seatNo3 = intSeatNo;
                                MP_RefundAmt3 = Convert.ToInt32(txtRefund.Text);
                                break;
                            }

                        case 3:
                            {
                                MP_seatNo4 = intSeatNo;
                                MP_RefundAmt4 = Convert.ToInt32(txtRefund.Text);
                                break;
                            }

                        case 4:
                            {
                                MP_seatNo5 = intSeatNo;
                                MP_RefundAmt5 = Convert.ToInt32(txtRefund.Text);
                                break;
                            }

                        case 5:
                            {
                                MP_seatNo6 = intSeatNo;
                                MP_RefundAmt6 = Convert.ToInt32(txtRefund.Text);
                                break;
                            }
                    }
                }
            }

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_tkt_spclcancel_insert");
            MyCommand.Parameters.AddWithValue("p_ticketno", txtticketno.Text);
            MyCommand.Parameters.AddWithValue("p_seatno1", MP_seatNo1);
            MyCommand.Parameters.AddWithValue("p_seatno2", MP_seatNo2);
            MyCommand.Parameters.AddWithValue("p_seatno3", MP_seatNo3);
            MyCommand.Parameters.AddWithValue("p_seatno4", MP_seatNo4);
            MyCommand.Parameters.AddWithValue("p_seatno5", MP_seatNo5);
            MyCommand.Parameters.AddWithValue("p_seatno6", MP_seatNo6);
            MyCommand.Parameters.AddWithValue("p_refundamt1", MP_RefundAmt1);
            MyCommand.Parameters.AddWithValue("p_refundamt2", MP_RefundAmt2);
            MyCommand.Parameters.AddWithValue("p_refundamt3", MP_RefundAmt3);
            MyCommand.Parameters.AddWithValue("p_refundamt4", MP_RefundAmt4);
            MyCommand.Parameters.AddWithValue("p_refundamt5", MP_RefundAmt5);
            MyCommand.Parameters.AddWithValue("p_refundamt6", MP_RefundAmt6);
            MyCommand.Parameters.AddWithValue("p_cancelbytype", "S");
            MyCommand.Parameters.AddWithValue("p_cancelledby", MP_CANCELLEDBY);
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["cancel_ref_no"].ToString() == "EXCEPTION")
                {
                    Errormsg("Something Went Wrong, Please contact Admin");
                }
                else
                {

                    pnlnoticketdetails.Visible = true;
                    pnlticketdetails.Visible = false;
                    txtReson.Text = "";
                    txtticketno.Text = "";
                    Successmsg("Ticket Cancelled Successfully");
                    Session["Cancel_voucher"] = dt.Rows[0]["cancel_ref_no"].ToString();
                    openSubDetailsWindow("../E_cancellationvoucher.aspx");
                    getSpclcancelTkt(Session["_UserCode"].ToString());
                }
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("sysAdmSpecialTicketCancellation.aspx-0004", ex.Message.ToString());
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        pnlnoticketdetails.Visible = true;
        pnlticketdetails.Visible = false;
        txtReson.Text = "";
        txtticketno.Text = "";
    }
    protected void gvspclcancelledtkt_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        CsrfTokenValidate();
        if (e.CommandName == "PrintVoucher")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string p_cancellationrefno = gvspclcancelledtkt.DataKeys[index].Values["cancelrefno"].ToString();
            Session["Cancel_voucher"] = p_cancellationrefno;
            openSubDetailsWindow("../E_cancellationvoucher.aspx");

        }
    }
protected void gvspclcancelledtkt_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvspclcancelledtkt.PageIndex = e.NewPageIndex;
        getSpclcancelTkt(Session["_UserCode"].ToString());
    }
    #endregion

}