using System;
using System;
using System.Data;
using Npgsql;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;


public partial class Auth_CntrTicketConfirmaton : BasePage
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
            Random randomclass = new Random();
            Session["rndNoCheck"] = randomclass.Next().ToString();
            hidtoken.Value = Session["rndNoCheck"].ToString();
            Session["_moduleName"] = "Ticket Confirmaton";
            if (Session["p_ticketNo"] != null || Session["p_ticketNo"] == "")
            {
                LoadJourneyDetails(Session["p_ticketNo"].ToString());
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
    private void LoadJourneyDetails(string p_ticketno)
    {
        try
        {
            wsClass obj = new wsClass();
            DataTable dt = obj.getJourneyDetails(p_ticketno, "R");
            if (dt.Rows.Count > 0)
            {

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

                lblbookingAmt.Text = dt.Rows[0]["amount_fare"].ToString();
                lblReservationCharge.Text = dt.Rows[0]["amount_onl_reservation"].ToString();
                lblother.Text = dt.Rows[0]["amount_concession"].ToString();


                Session["tottax"] = dt.Rows[0]["amount_tax"].ToString();
                lblTotal.Text = dt.Rows[0]["amount_total"].ToString();

                LoadTaxDetails(dt.Rows[0]["_ticketno"].ToString());
                LoadPassengerDetails(dt.Rows[0]["_ticketno"].ToString());
            }

        }
        catch (Exception ex)
        {
            _common.ErrorLog("CntrTicketConfirmaton.aspx-0001", ex.Message.ToString());
        }
    }
    private void LoadTaxDetails(string p_ticketno)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_get_taxDetails");
            MyCommand.Parameters.AddWithValue("p_ticketno", p_ticketno);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    grdtax.DataSource = dt;
                    grdtax.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("CntrTicketConfirmaton.aspx-0002", ex.Message.ToString());
        }
    }
    private void LoadPassengerDetails(string p_ticketno)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_get_passengerDetails");
            MyCommand.Parameters.AddWithValue("p_ticketno", p_ticketno);
            MyCommand.Parameters.AddWithValue("p_status", "R");
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
            _common.ErrorLog("CntrTicketConfirmaton.aspx-0003", ex.Message.ToString());
        }
    }
    private bool Updateticket(string tktno, string counterid, string userid)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.cntr_payment_done");
            MyCommand.Parameters.AddWithValue("p_ticket_no", tktno);
            MyCommand.Parameters.AddWithValue("p_counter_id", counterid);
            MyCommand.Parameters.AddWithValue("p_user_id", userid);
            MyCommand.Parameters.AddWithValue("p_pmtmode", "C");
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["p_rslt"].ToString() == "DONE")
                {
                  //CommonSMSnEmail sms = new CommonSMSnEmail();
                  //sms.sendTicketConfirm_SMSnEMAIL(tktno, Session["_UserCode"].ToString(), "");
                  return true;
                }
            }
            return false;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("CntrTicketConfirmaton.aspx-0004", ex.Message.ToString());
            return false;
        }
    }
    //public void openSubDetailsWindow(string MModuleName)
    //{
    //    try
    //    {
    //        string murl;
    //        murl = MModuleName + "?rt=" + DateTime.Now.ToString();
    //        string b = Request.Browser.Type.Substring(0, 2);
    //        if (b.ToUpper().Trim() == "IE")
    //            Response.Write("<SCRIPT language='javascript'>window.showModalDialog('" + murl + "','name','dialogWidth:830px;dialogHeight:900px');</script>");
    //        else
    //        {
    //            // Dim url As String = "GenQrySchStages.aspx"
    //            string fullURL = "window.open('" + murl + "', '_blank', 'height=900,width=830,status=no,toolbar=no,menubar=no,location=no,scrollbars=no,resizable=no,titlebar=no' );";
    //            // ScriptManager.RegisterStartupScript(Me, GetType(String), "OPEN_WINDOW", fullURL, True)
    //            string script = "window.open('" + fullURL + "','')";
    //            if ((ClientScript.IsClientScriptBlockRegistered("NewWindow") == false))
    //                ClientScript.RegisterClientScriptBlock(this.GetType(), "NewWindow", fullURL, true); // 
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        _common.ErrorLog("CntrTicketConfirmaton.aspx-0005", ex.Message.ToString());
    //    }
    //}

    void PageOpen(string title, string src)
    {
        lblTitle.InnerText = title;
        embedPage.Src = src;
        mpePage.Show();
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
    protected void btnproceed_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (Updateticket(Session["p_ticketNo"].ToString(), Session["_UserCntrID"].ToString(), Session["_UserCode"].ToString()) == true)
        {
            //openSubDetailsWindow("../E_ticket.aspx");
            mpConfirmation.Show();
            PageOpen("e-Ticket", "../E_ticket.aspx");
        }
        
    }
    protected void btnprint_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        mpConfirmation.Show();
        PageOpen("e-Ticket", "../E_ticket.aspx");
        
    }
    protected void lbtnNoConfirmation_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        mpConfirmation.Hide();
        btnproceed.Visible = false;
        btnprint.Visible = true;
    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Response.Redirect("CntrDashboard.aspx");
    }
    #endregion


}