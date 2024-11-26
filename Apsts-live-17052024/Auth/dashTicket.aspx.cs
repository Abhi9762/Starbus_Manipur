using System;
using System.Collections.Generic;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class Auth_dashTicket : System.Web.UI.Page
{
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    sbSecurity _security = new sbSecurity();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string ticketNo="" ;
            //Session["_ticketNo"] = "1F11904202320012";

            if (_security.isSessionExist(Session["_ticketNo"]) == true)
            {
                Session["_ticketNo"] = Session["_ticketNo"];
                ticketNo = Session["_ticketNo"].ToString();
            }
            else
            {
                
            }

            ticketDetails(ticketNo);
            ticketAmount(ticketNo);
            ticketPassangers(ticketNo);
            ticketTransactions(ticketNo);
            ticketCancellation(ticketNo);
            ticketAlarms(ticketNo);
            ticketGrievances(ticketNo);
        }
    }
    private void ticketDetails(string ticketNo)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_sp_ticket_detail");
            MyCommand.Parameters.AddWithValue("p_ticket", ticketNo);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    lblTicketNo.Text = dt.Rows[0]["ticket_no"].ToString();
                    lblTicketStatus.Text = dt.Rows[0]["current_status_code"].ToString();

                    lblJourneyDT.Text = dt.Rows[0]["journey_date"] + " " + dt.Rows[0]["start_time"].ToString().ToUpper();
                    lblBookingDT.Text = dt.Rows[0]["booking_datetime"].ToString().ToUpper();
                    lblFrom.Text = dt.Rows[0]["from_station"].ToString();
                    lblTo.Text = dt.Rows[0]["to_station"].ToString();
                    lblBoarding.Text = dt.Rows[0]["boarding_stn"].ToString();

                    lblBus.Text = dt.Rows[0]["service_type_name_en"].ToString();
                    lblService.Text = dt.Rows[0]["depot_service_code"].ToString() + "" + dt.Rows[0]["trip_direction"].ToString() + "" + dt.Rows[0]["srtp_id"].ToString() + ", " + dt.Rows[0]["service_name_en"].ToString();
                    lblBookingMode.Text = dt.Rows[0]["BOOKING_MODE"].ToString();

                    lblUserId.Text = dt.Rows[0]["booked_by_user_code"] + "(" + dt.Rows[0]["booked_by_type_code"] + ")";
                    lblMobileNo.Text = dt.Rows[0]["traveller_mobile_no"].ToString();
                    lblEmail.Text = dt.Rows[0]["traveller_email_id"].ToString();

                    string paymentMode = dt.Rows[0]["payment_mode"].ToString();

                    if ((paymentMode.ToUpper() == "Payment Gateway".ToUpper()))
                        lblPaymentMode.Text = dt.Rows[0]["payment_mode"].ToString() + " (" + dt.Rows[0]["gateway_name"].ToString() + ")";
                    else
                        lblPaymentMode.Text = dt.Rows[0]["payment_mode"].ToString();
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    private void ticketAmount(string ticketNo)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_sp_ticket_amount");
            MyCommand.Parameters.AddWithValue("p_ticket", ticketNo);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    lblAmountFare.Text = dt.Rows[0]["amount_fare"].ToString();
                    lblAmountReservation.Text = dt.Rows[0]["amount_onl_reservation"].ToString();
                    lblAmountConssion.Text = dt.Rows[0]["amount_concession"].ToString();

                    lblAmountCommission.Text = dt.Rows[0]["amount_commission"].ToString();
                    lblAmountTax.Text = dt.Rows[0]["amount_tax"].ToString();
                    lblAmountDiscount.Text = dt.Rows[0]["amount_offer"].ToString();

                    lblAmountTotal.Text = dt.Rows[0]["amount_total"].ToString();
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    private void ticketPassangers(string ticketNo)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_sp_ticket_passengers");
            MyCommand.Parameters.AddWithValue("p_ticket", ticketNo);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    gvPassangers.DataSource = dt;
                    gvPassangers.DataBind();
                    lblNoPassanger.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    private void ticketTransactions(string ticketNo)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_sp_ticket_transactions");
            MyCommand.Parameters.AddWithValue("p_ticket", ticketNo);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    gvTicketTransactions.DataSource = dt;
                    gvTicketTransactions.DataBind();
                    lblNoTransaction.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    private void ticketCancellation(string ticketNo)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_sp_ticket_cancelled");
            MyCommand.Parameters.AddWithValue("p_ticket", ticketNo);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    gvCancellation.DataSource = dt;
                    gvCancellation.DataBind();
                    lblNoCancellation.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    private void ticketAlarms(string ticketNo)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_sp_ticket_alarms");
            MyCommand.Parameters.AddWithValue("p_ticket", ticketNo);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    gvAlarms.DataSource = dt;
                    gvAlarms.DataBind();
                    lblNoAlarm.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    private void ticketGrievances(string ticketNo)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_sp_ticket_grievances");
            MyCommand.Parameters.AddWithValue("p_ticket", ticketNo);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    gvGrievance.DataSource = dt;
                    gvGrievance.DataBind();
                    lblNoGrievance.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
}