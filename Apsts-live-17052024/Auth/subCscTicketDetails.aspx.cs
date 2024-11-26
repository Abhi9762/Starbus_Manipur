using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_subCscTicketDetails : System.Web.UI.Page
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    string msg = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        Session["_ModuleName"] = "Ticket Details" + " - (" + Session["_UserName"] + " " + Session["_UserCode"] + ")";

        if (!IsPostBack)
        {
            CalendarExtenderBookingDT.EndDate = DateTime.Now.Date;

            if (Session["pSearch"] != null && Session["pSearch"].ToString() != "")
            {
                txtSearch.Text = Session["pSearch"].ToString();
            }

            if (Session["pJourneyDt"] != null && Session["pJourneyDt"].ToString() != "")
            {
                txtJourneyDt.Text = Session["pJourneyDt"].ToString();
            }

            if (Session["pBookDt"] != null && Session["pBookDt"].ToString() != "")
            {
                txtBookingDT.Text = Session["pBookDt"].ToString();
            }

            LoadTicketDetails(txtSearch.Text, txtBookingDT.Text, txtJourneyDt.Text);
        }

    }


    #region Method
    private void LoadTicketDetails(string pSearch, string pBookDt, string pJourneyDt)
    {
        try
        {
            gvTicketDetails.Visible = false;
            lblmsg.Visible = true;
            string name;
            string bookingDate;
            string journeyDate;

            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                name = "";
            }
            else
            {
                name = txtSearch.Text;
            }

            if (string.IsNullOrEmpty(txtJourneyDt.Text))
            {
                journeyDate = "0";
            }
            else
            {
                journeyDate = txtJourneyDt.Text;
            }

            if (string.IsNullOrEmpty(txtBookingDT.Text))
            {
                bookingDate = "0";
            }
            else
            {
                bookingDate = txtBookingDT.Text;
            }
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "csc.getticketdetails");
            MyCommand.Parameters.AddWithValue("@p_usercode", Session["_UserCode"]);
            MyCommand.Parameters.AddWithValue("@p_search", name);
            MyCommand.Parameters.AddWithValue("@p_booking_date", bookingDate);
            MyCommand.Parameters.AddWithValue("@p_journey_date", journeyDate);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    gvTicketDetails.DataSource = dt;
                    gvTicketDetails.DataBind();
                    gvTicketDetails.Visible = true;
                    lblmsg.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            ErrorModalPopup("Something Went Wrong", ex.Message + " in M8");
        }
    }
    public void ErrorModalPopup(string header, string message)
    {
        LabelModalErrorHeader.Text = header;
        LabelModalErrorMessage.Text = message;
        ModalPopupError.Show();
    }

    #endregion


    #region Event
    protected void lbtnSearch_Click(object sender, EventArgs e)
    {
        gvTicketDetails.Visible = false;
        lblmsg.Visible = true;
        if (string.IsNullOrEmpty(txtSearch.Text) && string.IsNullOrEmpty(txtBookingDT.Text) && string.IsNullOrEmpty(txtJourneyDt.Text))
        {
            ErrorModalPopup("Please Check", "Please fill at least 1 field");
        }
        else
        {
            LoadTicketDetails(txtSearch.Text, txtBookingDT.Text, txtJourneyDt.Text);
        }
    }

    protected void gvTicketDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTicketDetails.PageIndex = e.NewPageIndex;
        LoadTicketDetails(txtSearch.Text, txtBookingDT.Text, txtJourneyDt.Text);
    }

    protected void gvTicketDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblTicketNo = e.Row.FindControl("lblticket") as Label;
                LinkButton btnprint = e.Row.FindControl("lbtnPrint") as LinkButton;
                LinkButton btncancel = e.Row.FindControl("lbtnCancel") as LinkButton;
                LinkButton btnresendwhatsapp = e.Row.FindControl("lbtnresendwhatsapp") as LinkButton;
                LinkButton btnresnd = e.Row.FindControl("lbtnresendsms") as LinkButton;
                LinkButton btnresendemail = e.Row.FindControl("lbtnresendmail") as LinkButton;
                Label laldepttime = e.Row.FindControl("lbljouyneydate") as Label;

                string deptime = gvTicketDetails.DataKeys[e.Row.RowIndex]["deptitme"].ToString();
                string emailid = gvTicketDetails.DataKeys[e.Row.RowIndex]["emailid"].ToString();
                string CURRENT_STATUS_CODE = gvTicketDetails.DataKeys[e.Row.RowIndex]["CURRENT_STATUS_CODE"].ToString();

                if (CURRENT_STATUS_CODE == "A")
                {
                    DateTime McurrentDateTime = DateTime.Now;
                    DateTime MserviceDateTime = DateTime.ParseExact(laldepttime.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture)
                    .Add(DateTime.ParseExact(deptime, "hh:mm tt", CultureInfo.InvariantCulture).TimeOfDay);

                    if ((MserviceDateTime - McurrentDateTime).TotalMinutes < 120)
                    {
                        btnresnd.Enabled = false;
                        btnresnd.Style.Add("background-color", "#808080 !important");
                        btnresnd.Style.Add("cursor", "default");
                        btnresnd.Style.Add("border-color", "#808080 !important");
                        btnresendwhatsapp.Enabled = false;
                        btnresendwhatsapp.Style.Add("background-color", "#808080 !important");
                        btnresendwhatsapp.Style.Add("cursor", "default");
                        btnresendwhatsapp.Style.Add("border-color", "#808080 !important");
                        btncancel.Enabled = false;
                        btncancel.Style.Add("background-color", "#808080 !important");
                        btncancel.Style.Add("cursor", "default");
                        btncancel.Style.Add("border-color", "#808080 !important");
                    }

                    if (string.IsNullOrEmpty(emailid))
                    {
                        btnresendemail.Enabled = false;
                        btnresendemail.Style.Add("background-color", "#808080 !important");
                        btnresendemail.Style.Add("cursor", "default");
                        btnresendemail.Style.Add("border-color", "#808080 !important");
                    }
                }
                else if (CURRENT_STATUS_CODE == "C")
                {
                    btnprint.Enabled = false;
                    btnprint.Style.Add("background-color", "#808080 !important");
                    btnprint.Style.Add("cursor", "default");
                    btnprint.Style.Add("border-color", "#808080 !important");
                    btnresendemail.Enabled = false;
                    btnresendemail.Style.Add("background-color", "#808080 !important");
                    btnresendemail.Style.Add("cursor", "default");
                    btnresendemail.Style.Add("border-color", "#808080 !important");
                    btnresnd.Enabled = false;
                    btnresnd.Style.Add("background-color", "#808080 !important");
                    btnresnd.Style.Add("cursor", "default");
                    btnresnd.Style.Add("border-color", "#808080 !important");
                    btnresendemail.Enabled = false;
                    btnresendemail.Style.Add("background-color", "#808080 !important");
                    btnresendemail.Style.Add("cursor", "default");
                    btnresendemail.Style.Add("border-color", "#808080 !important");
                    btncancel.Enabled = false;
                    btncancel.Style.Add("background-color", "#808080 !important");
                    btncancel.Style.Add("cursor", "default");
                    btncancel.Style.Add("border-color", "#808080 !important");
                    btnresendwhatsapp.Enabled = false;
                    btnresendwhatsapp.Style.Add("background-color", "#808080 !important");
                    btnresendwhatsapp.Style.Add("cursor", "default");
                    btnresendwhatsapp.Style.Add("border-color", "#808080 !important");
                }
                else
                {
                    btnprint.Enabled = false;
                    btnprint.Style.Add("background-color", "#808080 !important");
                    btnprint.Style.Add("border-color", "#808080 !important");
                    btnprint.Style.Add("cursor", "default");
                    btnresnd.Enabled = false;
                    btnresnd.Style.Add("background-color", "#808080 !important");
                    btnresnd.Style.Add("cursor", "default");
                    btnresnd.Style.Add("border-color", "#808080 !important");
                    btnresendemail.Enabled = false;
                    btnresendemail.Style.Add("background-color", "#808080 !important");
                    btnresendemail.Style.Add("cursor", "default");
                    btnresendemail.Style.Add("border-color", "#808080 !important");
                    btnresendwhatsapp.Enabled = false;
                    btnresendwhatsapp.Style.Add("background-color", "#808080 !important");
                    btnresendwhatsapp.Style.Add("cursor", "default");
                    btnresendwhatsapp.Style.Add("border-color", "#808080 !important");
                    btncancel.Enabled = false;
                    btncancel.Style.Add("background-color", "#808080 !important");
                    btncancel.Style.Add("cursor", "default");
                    btncancel.Style.Add("border-color", "#808080 !important");
                }
            }
        }
        catch (Exception ex)
        {
            ErrorModalPopup("Pleases Check", ex.Message);
        }
    }

    protected void gvTicketDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        string tktno;

        if (e.CommandName == "print")
        {
            tktno = gvTicketDetails.DataKeys[index]["ticket_no"].ToString();
            Session["p_ticketNo"] = tktno;
            openpage("../E_Ticket.aspx");
        }
        else if (e.CommandName == "Cancel")
        {
            tktno = gvTicketDetails.DataKeys[index]["ticket_no"].ToString();
            Session["p_ticketNo"] = tktno;
            Response.Redirect("subCscTicketCancellation.aspx");
        }
        else if (e.CommandName == "resendwhatsapp")
        {
            tktno = gvTicketDetails.DataKeys[index]["ticket_no"].ToString();
            // string mobile = "8126562803";
            // comnWhatsapp.sendTestWhatsappSms(mobile); // testing template hello world
            mpresendWhatsapp.Show();
        }
        else if (e.CommandName == "resendsms")
        {
            tktno = gvTicketDetails.DataKeys[index]["ticket_no"].ToString();
            //comn.sendTicketConfirm_SMSnEMAIL(tktno, "S", 2);
            mpsmssend.Show();
        }
        else if (e.CommandName == "resendemail")
        {
            tktno = gvTicketDetails.DataKeys[index]["ticket_no"].ToString();
            //comn.sendTicketConfirm_SMSnEMAIL(tktno, "E", 2);
            mpresendEmail.Show();
        }
        else if (e.CommandName == "view")
        {
            tktno = gvTicketDetails.DataKeys[index]["ticket_no"].ToString();
            Session["_ticketNo"] = tktno;
            mpTicket.Show();
        }
    }

    private void openpage(string PageName)
    {
        tkt.Src = PageName;
        mpEtkt.Show();
    }
    #endregion

}
