using System;
using System.Data;
using Npgsql;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;


public partial class Auth_CntrTicketQuery : BasePage
{
    NpgsqlCommand MyCommand = new NpgsqlCommand();
    private sbBLL bll = new sbBLL();
    DataTable dt = new DataTable();
    sbValidation _validation = new sbValidation();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    private sbCommonFunc _common = new sbCommonFunc();
    string commonerror = "There is some error. Please contact the helpdesk or try again after sometime.";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                Random randomclass = new Random();
                Session["rndNoCheck"] = randomclass.Next().ToString();
                hidtoken.Value = Session["rndNoCheck"].ToString();
                //Session["p_ticketNo"] = null;
                Session["_moduleName"] = "Ticket Query";
                tbbookingdate.Text = DateTime.Now.Date.AddDays(-1).ToString("dd/MM/yyyy");
                // tbjourneydate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
                ViewTicket(gvtickets);
            }
            catch (Exception ex)
            {

            }

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
    private void ViewTicket(GridView gvtickets)//M1
    {
        try
        {
            string psearch = "", bookdate = "", journeydate = "";
            if (Session["p_ticketNo"] != null)
            {
                psearch = Session["p_ticketNo"].ToString();
                tbvalue.Text = Session["p_ticketNo"].ToString();
                bookdate = "";
                journeydate = "";
                tbbookingdate.Text = "";
                tbjourneydate.Text = "";
            }
            else
            {
                psearch = tbvalue.Text.ToString();
                bookdate = tbbookingdate.Text.ToString();
                journeydate = tbjourneydate.Text.ToString();
            }
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_get_qry_ticket");
            MyCommand.Parameters.AddWithValue("p_bookingdate", bookdate);
            MyCommand.Parameters.AddWithValue("p_journeydate", journeydate);
            MyCommand.Parameters.AddWithValue("p_value", psearch);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    gvtickets.DataSource = dt;
                    gvtickets.DataBind();
                    gvtickets.Visible = true;
                    pnlnotickets.Visible = false;
                }
                else
                {
                    gvtickets.Visible = false;
                    pnlnotickets.Visible = true;
                }
            }
            else
            {
                gvtickets.Visible = false;
                pnlnotickets.Visible = true;
            }
            Session["p_ticketNo"] = null;
        }
        catch (Exception ex)
        {
            gvtickets.Visible = false;
            pnlnotickets.Visible = true;
            _common.ErrorLog("CntrTicketQuery-M1", ex.Message.ToString());
        }
    }

    private void LoadJourneyDetails(string p_ticketno)//M2
    {
        try
        {
            wsClass obj = new wsClass();
            DataTable dt = obj.getJourneyDetails(p_ticketno, "0");
            if (dt.Rows.Count > 0)
            {
                Session["p_ticketNo"] = dt.Rows[0]["_ticketno"].ToString();
                lblservice.Text = dt.Rows[0]["service_name"].ToString();
                lblservicetype.Text = dt.Rows[0]["depot_servicecode"].ToString() + dt.Rows[0]["trip_type"].ToString() + " " + dt.Rows[0]["service_type_name"].ToString();
                lbljourneydatetime.Text = dt.Rows[0]["journeydate"].ToString();
                lbldepaturetime.Text = dt.Rows[0]["trip_time"].ToString();
                lblarrivaltime.Text = dt.Rows[0]["tripend_time"].ToString();
                lblstations.Text = dt.Rows[0]["fromstn_name"].ToString() + "-" + dt.Rows[0]["tostn_name"].ToString();
                lblbookingdatetime.Text = dt.Rows[0]["bookingdatetime"].ToString();
                lblboardingstation.Text = dt.Rows[0]["boardingstn_name"].ToString();
                lblmobileno.Text = dt.Rows[0]["traveller_mobile_no"].ToString();
                lblemail.Text = dt.Rows[0]["traveller_email_id"].ToString();
                lbltotamount.Text = dt.Rows[0]["amount_total"].ToString();
                LoadPassengerDetails(dt.Rows[0]["_ticketno"].ToString());
                initbtn();
                if (dt.Rows[0]["current_status"].ToString().Trim() == "A")
                {
                    lbtnprint.Visible = true;
                    lbtnresendsms.Visible = true;
                    if (dt.Rows[0]["for_cncl"].ToString().Trim() == "Y")
                    {
                        lbtncancelticket.Visible = true;
                    }

                    if (dt.Rows[0]["journey_done"].ToString().Trim() == "Y")
                    {
                        lbtnresendsms.Visible = false;
                        lbtnresendemail.Visible = false;
                    }
                    else
                    {
                        lbtnresendsms.Visible = true;
                        lbtnresendemail.Visible = true;
                    }
                    if (dt.Rows[0]["traveller_email_id"].ToString() == "NA")
                    {
                        lbtnresendemail.Visible = false;
                    }
                }
                else if (dt.Rows[0]["current_status"].ToString().Trim() == "C")
                {
                    lbtnprintcancelvoucher.Visible = true;
                    Session["Cancel_voucher"] = dt.Rows[0]["cancellation_ref_no"].ToString();
                    if (!DBNull.Value.Equals(dt.Rows[0]["cancellation_ref_no"].ToString().Trim()) == true)
                    {
                        lbtnspecialrefund.Visible = true;
                    }

                    if (!DBNull.Value.Equals(dt.Rows[0]["refund_ref_no"].ToString()) == true)
                    {
                        lbtnspecialrefund.Visible = false;
                    }

                }
                pnlticketdetails.Visible = true;
                pnlnoticketdetails.Visible = false;
            }
            else
            {
                pnlticketdetails.Visible = false;
                pnlnoticketdetails.Visible = true;
            }

        }
        catch (Exception ex)
        {
            pnlticketdetails.Visible = false;
            pnlnoticketdetails.Visible = true;
            _common.ErrorLog("CntrTicketQuery-M2", ex.Message.ToString());
        }
    }
    private void initbtn()
    {
        lbtnprint.Visible = false;
        lbtnresendsms.Visible = false;
        lbtnresendemail.Visible = false;
        lbtncancelticket.Visible = false;
        lbtnspecialrefund.Visible = false;
        lbtnprintcancelvoucher.Visible = false;
    }

    private void LoadPassengerDetails(string p_ticketno)//M3
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_get_passengerDetails");
            MyCommand.Parameters.AddWithValue("p_ticketno", p_ticketno);
            MyCommand.Parameters.AddWithValue("p_status", "0");

            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    gvpassengerdetails.DataSource = dt;
                    gvpassengerdetails.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("CntrTicketQuery-M3", ex.Message.ToString());
        }
    }
    private void LoadTicketLog(string p_ticketno)//M4
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_get_ticket_log");
            MyCommand.Parameters.AddWithValue("p_ticketno", p_ticketno);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    gvticketlog.DataSource = dt;
                    gvticketlog.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("CntrTicketQuery-M4", ex.Message.ToString());
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
    private void refundtransaction(string cancel_refNo, string refundedby)//M5
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_ticket_refund");
            MyCommand.Parameters.AddWithValue("p_cancel_refno", cancel_refNo);
            MyCommand.Parameters.AddWithValue("p_refundedby", refundedby);
            MyCommand.Parameters.AddWithValue("p_updatedby", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("p_ipaddress", ":::1");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
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
                        ViewTicket(gvtickets);
                        pnlticketdetails.Visible = false;
                        pnlnoticketdetails.Visible = true;
                    }
                }
            }
            else
            {
                _common.ErrorLog("CntrTicketQuery-M5", dt.TableName);
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("CntrTicketQuery-M5", ex.Message.ToString());
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

    #region "Event"
    protected void grdtax_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label Clbltax = (e.Row.FindControl("lblFTotalTax") as Label);
            Clbltax.Text = Session["tottax"].ToString();
        }
    }
    protected void lbtnprint_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        openSubDetailsWindow("../E_ticket.aspx");
    }
    protected void lbtnprintcancelvoucher_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        string dd = Session["Cancel_voucher"].ToString();
        openSubDetailsWindow("../E_cancellationvoucher.aspx");
    }
    protected void lbtncancelticket_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Response.Redirect("CntrTicketCancellation.aspx");
    }
    protected void lbtnsearch_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        ViewTicket(gvtickets);
        pnlticketdetails.Visible = false;
        pnlnoticketdetails.Visible = true;
    }
    protected void gvtickets_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvtickets.PageIndex = e.NewPageIndex;
        ViewTicket(gvtickets);
    }
    protected void gvtickets_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView rowView = (DataRowView)e.Row.DataItem;
            Label lblticket_status = (Label)e.Row.FindControl("lblticket_status");
            if (rowView["ticket_status_code"].ToString() == "A")
            {
                lblticket_status.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblticket_status.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
    protected void gvtickets_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        CsrfTokenValidate();
        if (e.CommandName == "viewTicket")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string ticket_no = gvtickets.DataKeys[index].Values["ticket_no"].ToString();
            lbtncancelticket.Visible = false;
            if (gvtickets.DataKeys[index].Values["booked_by_type"].ToString() == "Counter")
            {
                lbtncancelticket.Visible = true;
            }
            Session["TicketNumber"] = ticket_no;
            lblticktloghd.Text = "Transaction Log of Ticket No. " + ticket_no;
            LoadJourneyDetails(ticket_no);
            LoadTicketLog(ticket_no);
        }
    }
    protected void lbtnlog_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        mpticketlog.Show();
    }
    protected void lbtnreset_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        tbbookingdate.Text = DateTime.Now.Date.AddDays(-1).ToString("dd/MM/yyyy");
        tbjourneydate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
        tbvalue.Text = "";
        ViewTicket(gvtickets);
        pnlticketdetails.Visible = false;
        pnlnoticketdetails.Visible = true;
    }
    protected void lbtnspecialrefund_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        refundtransaction(Session["Cancel_voucher"].ToString(), Session["_CntrCode"].ToString());
    }
    protected void lbtnresendsms_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        // CommonSMSnEmail sms = new CommonSMSnEmail();
        //  sms.sendTicketConfirm_SMSnEMAIL(Session["TicketNumber"].ToString(), Session["_UserCode"].ToString(), "");
    }


    #endregion














}