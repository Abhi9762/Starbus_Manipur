using System;
using System.Data;
using Npgsql;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Globalization;

public partial class Auth_agTicketQuery : System.Web.UI.Page
{
    NpgsqlCommand MyCommand = new NpgsqlCommand();
    private sbBLL bll = new sbBLL();
    DataTable dt = new DataTable();
    sbValidation _validation = new sbValidation();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    private sbCommonFunc _common = new sbCommonFunc();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                //Session["p_ticketNo"] = null;
               // Session["_moduleName"] = "Ticket Query";
                tbbookingdate.Text = DateTime.Now.Date.AddDays(-1).ToString("dd/MM/yyyy");
                tbjourneydate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

                ViewTicket();
            }
            catch (Exception ex)
            {

            }

        }
    }
    #region "Method"


    private void Errormsg( String msg)
    {
        lblerrmsg.Text = msg;
        mpError.Show();
    }

    private void ViewTicket()
    {
        try
        {
            pnlticketdetails.Visible = false;
            pnlnoticketdetails.Visible = true;
            string bookingdate;
            string journeydate;
            string p_value;

            if (tbbookingdate.Text.Length > 0)
            {
                bookingdate = tbbookingdate.Text;
            }
            else
            {
                bookingdate = "0";
            }

            if (tbjourneydate.Text.Length > 0)
            {
                journeydate = tbjourneydate.Text;
            }
            else
            {
                journeydate = "0";
            }

            p_value = tbvalue.Text;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_ticket_Query");
            MyCommand.Parameters.AddWithValue("p_bookingdate", bookingdate);
            MyCommand.Parameters.AddWithValue("p_journeydate", journeydate);
            MyCommand.Parameters.AddWithValue("p_search", p_value);
            MyCommand.Parameters.AddWithValue("p_bookbytype", "A");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    gvTickets.DataSource = dt;
                    gvTickets.DataBind();
                    pnlticketdetails.Visible = true;
                    pnlnoticketdetails.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
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

    #endregion


    protected void lbtnreset_Click(object sender, EventArgs e)
    {
        Session["_ticketNo"] = null;
        tbvalue.Text = "";
        tbbookingdate.Text = "";
        tbjourneydate.Text = "";
        pnlticketdetails.Visible = false;
        pnlnoticketdetails.Visible = true;
    }


    protected void gvTickets_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView rowView = (DataRowView)e.Row.DataItem;
            LinkButton lbtnPrint = (LinkButton)e.Row.FindControl("lbtnPrint");
            LinkButton lbtnresendsms = (LinkButton)e.Row.FindControl("lbtnresendsms");
            LinkButton lbtnresendwhtsup = (LinkButton)e.Row.FindControl("lbtnresendwhtsup");
            LinkButton lbtnresendmail = (LinkButton)e.Row.FindControl("lbtnresendmail");
            LinkButton lbtncancel = (LinkButton)e.Row.FindControl("lbtncancel");
            LinkButton lbtnspcialcancel = (LinkButton)e.Row.FindControl("lbtnspcialcancel");

            lbtnPrint.Visible = false;
            lbtnresendsms.Visible = false;
            lbtnresendwhtsup.Visible = false;
            lbtnresendmail.Visible = false;
            lbtncancel.Visible = false;
            lbtnspcialcancel.Visible = false;



            string journeyDateStr = rowView["journey_date"].ToString(); // The column alias from your SQL query
            string departureTimeStr = rowView["departure_time"].ToString(); // The column alias from your SQL query

            DateTime journeyDate;
            DateTime departureTime;


            if (Convert.ToInt32(rowView["is_print"]) == 1)
            {
                if (DateTime.TryParseExact(journeyDateStr, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out journeyDate) &&
                DateTime.TryParseExact(departureTimeStr, "hh:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out departureTime))
                {
                    lbtnPrint.Visible = true;
                    DateTime McurrentDateTime = DateTime.Now;
                    DateTime MserviceDateTime = journeyDate + departureTime.TimeOfDay;


                    if (McurrentDateTime < MserviceDateTime.AddMinutes(120))
                    {
                        lbtnresendsms.Enabled = true;
                        lbtnresendwhtsup.Enabled = true;
                    }

                    if (rowView["travelleremailid"].ToString() != "NA")
                    {
                        lbtnresendmail.Visible = true;
                    }
                }
            }
        }

       

    }

    private void OpenPage(string PageName)
    {
        tkt.Src = PageName;
        mpticket.Show();
    }

    protected void gvTickets_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "print")
        {
            Session["TicketNumber"] = gvTickets.DataKeys[index].Values["ticket_no"];
            OpenPage("../E_Ticket.aspx");
        }
        else if (e.CommandName == "view")
        {
            Session["_ticketNo"] = gvTickets.DataKeys[index].Values["ticket_no"];
            mpTicketDtls.Show();
        }
        else if (e.CommandName == "resendsms" || e.CommandName == "whtsup")
        {
            string tktno = gvTickets.DataKeys[index].Values["ticket_no"].ToString();
            //comn.sendTicketConfirm_SMSnEMAIL(tktno, "S", 13);
        }
        else if (e.CommandName == "resendmail")
        {
            string tktno = gvTickets.DataKeys[index].Values["ticket_no"].ToString();
            //comn.sendTicketConfirm_SMSnEMAIL(tktno, "E", 13);
        }
    }

    protected void lbtnerrorclose_Click(object sender, EventArgs e)
    {
        mpError.Hide();
    }




    protected void lbtnsearch_Click(object sender, EventArgs e)
    {
        pnlticketdetails.Visible = false;
        pnlnoticketdetails.Visible = true;
        if( tbbookingdate.Text.Length <= 0 && tbjourneydate.Text.Length <= 0 && tbvalue.Text.Length <= 0)
        {
            Errormsg("At least any One filter enter (Booking Date/Journey date and Search Value) for search ticket query");
        }
           
        ViewTicket();
    }
}