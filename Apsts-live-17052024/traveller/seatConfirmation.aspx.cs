using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Npgsql;

public partial class traveller_seatConfirmation : System.Web.UI.Page
{
    NpgsqlCommand MyCommand;
    sbBLL bll = new sbBLL();
    sbValidation _SecurityCheck = new sbValidation();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    private sbCommonFunc _common = new sbCommonFunc();
    string commonerror = "There is some error. Please contact to helpdesk or try again after some time.";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["_moduleName"] = "Confirmation";
            onload();
        }
    }

    #region "Motheds"
    private void Errormsg(string msg)
    {
        string popup = _popup.modalPopupSmall("W", "Information", msg, "Close");
        Response.Write(popup);
    }

    public void onload()
    {
        try
        {
            if (_security.isSessionExist(Session["TicketNumber"]) == true)
            {
                loadJourneyDetails(Session["TicketNumber"].ToString());
                loadPassengerDetails(Session["TicketNumber"].ToString());
             
            }
            else
            {
                Response.Redirect("errorpage.aspx");
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.ToString());
            _common.ErrorLog("trvlrSeatConfrm-M1", ex.ToString());
        }
    }
    private void loadJourneyDetails(string ticketNo)//M1
    {
        try
        {
            wsClass obj = new wsClass();
            DataTable dt = obj.getJourneyDetails(ticketNo, "R");

            if (dt.Rows.Count > 0)
            {

                lblFromStation.Text = dt.Rows[0]["fromstn_name"].ToString();
                lblToStation.Text = dt.Rows[0]["tostn_name"].ToString();
                lblDate.Text = dt.Rows[0]["journeydate"].ToString();
                lblDeparture.Text = dt.Rows[0]["trip_time"].ToString();
                lblServiceType.Text = dt.Rows[0]["service_type_name"].ToString();

                lblBoardingStation.Text = dt.Rows[0]["boardingstn_name"].ToString();
                lblFareAmt.Text = dt.Rows[0]["amount_fare"].ToString(); 

            }
            else
            {
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("trvlrConfirm-M1", ex.Message.ToString());
        }
    }

    private void loadPassengerDetails(string ticketNo)//M2
    {
        try
        {
            wsClass obj = new wsClass();
            DataTable dt = obj.getPassengerDetails(ticketNo, "R");

            if (dt.Rows.Count > 0)
            {
                gvPassengers.DataSource = dt;
                gvPassengers.DataBind();

                gvPassengers.Visible = true;
            }
            else
            {
                gvPassengers.Visible = false;
            }
        }
        catch (Exception ex)
        {
            gvPassengers.Visible = false;
            _common.ErrorLog("trvlrConfirm-M2", ex.Message.ToString());
        }
    }
  
    #endregion

    #region "Events"
    protected void lbtnProceed_Click(object sender, EventArgs e)
    {
        Response.Redirect("seatPayment.aspx");
    }
    protected void lbtnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("dashboard.aspx");
    }
    #endregion



}