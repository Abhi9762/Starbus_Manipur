
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Auth_AgCurrentBookingDashboard : System.Web.UI.Page
{
    int tottripCount = 0;
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    protected void Page_Init(object sender, System.EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.AddHeader("Cache-control", "no-store, must-revalidate,private,no-cache");
        Response.AddHeader("PRAGMA", "NO-Cache");
        Response.Expires = -1;
        Response.Expires = 0;
        Response.Cache.SetNoStore();
        Response.Cache.SetExpires(DateTime.Now.AddDays(-2));
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            resetcntrl();
            Session["_moduleName"] = "Agent - Current Bookings";
            lbltriplistdate.Text = DateTime.Now.ToString();
            txtFromDate.Text = DateTime.Now.Date.AddDays(-7).ToString("dd/MM/yyyy");
            txtToDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            loadCntrOfflinetkttoday();
        }

        checktripopenornot();

        LoadTripDetails();
    }



    #region "method"
    private void resetcntrl()
    {
        Session["OpenTripID"] = null;
        Session["OpenSERVICE_CODE"] = null;
        Session["OpenREGISTRATIONNUMBER"] = null;
        Session["OpenCONDUCTOREMPID"] = null;
        Session["OpenWaybillnumber"] = null;
        Session["OpenJourneyType"] = null;
    }
    private void checktripopenornot()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_get_opentrip");
            MyCommand.Parameters.AddWithValue("p_cntrid", Session["_UserCode"].ToString());
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    rptopentrip.DataSource = dt;
                    rptopentrip.DataBind();

                    if (dt.Rows.Count == 4)
                    {
                        if (tottripCount == 0)
                        {
                            lbtnnewtrip.Visible = false;
                            rptopentrip.Visible = true;
                            lblnotripmsg.Text = "At a time only 4 Trip for Current Booking, for New Trip Close at least one Trip.";
                            pnlNotrip.Visible = false;
                        }
                        else
                        {
                            lbtnnewtrip.Visible = false;
                            rptopentrip.Visible = true;
                            lblnotripmsg.Text = "At a time only 4 Trip for Current Booking, Please Close Expired Trip to Book More Tickets.";
                            pnlNotrip.Visible = false;
                        }
                    }
                    if (dt.Rows.Count < 4)
                    {
                        if (tottripCount > 0)
                        {
                            lbtnnewtrip.Visible = false;
                            rptopentrip.Visible = true;
                            lblnotripmsg.Text = "Trip Departure Time Expired Please Close Expired Trip, For Book More Tickets.";
                            pnlNotrip.Visible = false;
                        }
                        else
                        {
                            lbtnnewtrip.Visible = true;
                            rptopentrip.Visible = true;
                            pnlNotrip.Visible = false;
                        }
                    }
                }
                else
                {
                    lbtnnewtrip.Visible = true;
                    pnlNotrip.Visible = true;
                    rptopentrip.Visible = false;
                    lblnotripmsg.Visible = false;
                }
            }
            else
            {
                lbtnnewtrip.Visible = true;
                pnlNotrip.Visible = true;
                rptopentrip.Visible = false;
            }


        }
        catch (Exception ex)
        {

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

    private void loadCntrOfflinetkttoday()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "current_booking.f_get_trip_summary");
            MyCommand.Parameters.AddWithValue("sp_date", DateTime.Now.Date.ToString("dd/MM/yyyy"));
            MyCommand.Parameters.AddWithValue("sp_cntr_id", Session["_UserCode"].ToString());
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    lbltodayttkt.Text = dt.Rows[0]["sp_TodayTrip"].ToString();
                    lbltotalttkt.Text = dt.Rows[0]["sp_TotalTrip"].ToString();
                    lbltodaytktamt.Text = dt.Rows[0]["sp_TodayFare"].ToString();
                    lbltotaltktamt.Text = dt.Rows[0]["sp_TotalFare"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            NoDataLabel.Visible = true;
            grdTripDetails.Visible = false;
        }
    }
    private void LoadTripDetails()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "current_booking.f_get_trips");
            MyCommand.Parameters.AddWithValue("sp_user_id", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("sp_cntr_id", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("sp_service_type", "0");
            MyCommand.Parameters.AddWithValue("sp_bus", "0");
            MyCommand.Parameters.AddWithValue("sp_route", "0");
            MyCommand.Parameters.AddWithValue("sp_fromdate", txtFromDate.Text);
            MyCommand.Parameters.AddWithValue("sp_todate", txtToDate.Text);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    grdTripDetails.DataSource = dt;
                    grdTripDetails.DataBind();
                    grdTripDetails.Visible = true;
                    NoDataLabel.Visible = false;

                }
                else
                {
                    NoDataLabel.Visible = true;
                    grdTripDetails.Visible = false;
                }
            }
            else
            {
                NoDataLabel.Visible = true;
                grdTripDetails.Visible = false;
            }


        }
        catch (Exception ex)
        {
            NoDataLabel.Visible = true;
            grdTripDetails.Visible = false;
        }
    }

    private void openpage(string pagename)
    {
        tkt.Src = pagename;
        mpTripchart.Show();
    }

    #endregion
    #region "Event"
    protected void lbtnnewtrip_Click(object sender, EventArgs e)
    {
        Session.Remove("p_tripcode");
        Response.Redirect("AgCurrentBooking.aspx");
    }
    protected void rptopentrip_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) | (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            HiddenField hdTripMin = (HiddenField)e.Item.FindControl("hdtripmin");
            LinkButton lbtnCloseTrip = (LinkButton)e.Item.FindControl("lbtnCloseTrip");
            LinkButton lbtnBookTrip = (LinkButton)e.Item.FindControl("lbtnBookTrip");
            Label lblStatus = (Label)e.Item.FindControl("lblStatus");
            decimal TripMin = Convert.ToDecimal(hdTripMin.Value.ToString());
            if (TripMin > -120)
            {
                lbtnBookTrip.Visible = true;
                lblStatus.Text = "Active";
                lblStatus.ForeColor = Color.Green;
            }
            else
            {
                lbtnBookTrip.Visible = false;
                lblStatus.Text = "Expired";
                lblStatus.ForeColor = Color.Red;
                tottripCount = tottripCount + 1;
            }
        }
    }
    protected void rptopentrip_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "CloseTrip")
        {
            HiddenField hdtrip_code = (HiddenField)e.Item.FindControl("hdtripcode");
            HiddenField hdSPECIALTRIPYN = (HiddenField)e.Item.FindControl("hdSPECIALTRIPYN");

            Session["p_tripcode"] = hdtrip_code.Value.ToString();
            Session["SPECIALTRIPYN"] = hdSPECIALTRIPYN.Value.ToString();
            Session["_pUpdatedby"] = Session["_UserCode"].ToString();
            lblConfirmation.Text = "Do you want generate tirp Chart/Closing Booking ?";
            mpConfirmation.Show();
            //CloseTrip(hdtrip_code.Value.ToString());
        }
        if (e.CommandName == "OpenTrip")
        {
            //if (Session["lblmandatoryamt"].ToString() != "0")
            //{
            //    Label1.Text = "To Proceed Further Please Deposit Pending Mandatory Amount Rs. " + Session["lblmandatoryamt"].ToString() + " Till Date :" + DateTime.Now.Date.AddDays(-2).ToString("dd/MM/yyyy");
            //    ModalPopupExtenderFirst.Show();
            //    return;
            //}
            //HiddenField hdSERVICE_CODE = (HiddenField)e.Item.FindControl("hdSERVICE_CODE");
            //HiddenField hdREGISTRATIONNUMBER = (HiddenField)e.Item.FindControl("hdREGISTRATIONNUMBER");
            //HiddenField hdCONDUCTOREMPID = (HiddenField)e.Item.FindControl("hdCONDUCTOREMPID");
            //HiddenField hdWaybillnumber = (HiddenField)e.Item.FindControl("hdWaybillnumber");
            //HiddenField hdjourneyType = (HiddenField)e.Item.FindControl("hdjourneyType");
            //Session["OpenTripID"] = e.CommandArgument.ToString();
            //Session["OpenSERVICE_CODE"] = hdSERVICE_CODE.Value;
            //Session["OpenREGISTRATIONNUMBER"] = hdREGISTRATIONNUMBER.Value;
            //Session["OpenCONDUCTOREMPID"] = hdCONDUCTOREMPID.Value;
            //Session["OpenWaybillnumber"] = hdWaybillnumber.Value;
            //Session["OpenJourneyType"] = hdjourneyType.Value;
            HiddenField hdtrip_code = (HiddenField)e.Item.FindControl("hdtripcode");
            Session["p_tripcode"] = hdtrip_code.Value.ToString();
            Response.Redirect("AgCurrentBooking.aspx");
        }
    }

    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        if (Session["SPECIALTRIPYN"].ToString() == "Y")
        {
            openpage("../E_Spcltripchart_current.aspx");
        }
        else
        {
            openSubDetailsWindow("../E_tripchart_current.aspx");
        }


        checktripopenornot();
        LoadTripDetails();
    }

    protected void grdTripDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "ViewDetails")
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int i = row.RowIndex;
            string tripcode = grdTripDetails.DataKeys[i]["tripcode"].ToString();
            string specialtrip = grdTripDetails.DataKeys[i]["specialtrip_yn"].ToString();
            Session["p_tripcode"] = tripcode;
            Session["SPECIALTRIPYN"] = specialtrip;
            if (specialtrip == "Y")
            {
                Session["_pUpdatedby"] = Session["_UserCode"];
                openpage("../E_Spcltripchart_current.aspx");
            }
            else
            {
                Session["_pUpdatedby"] = Session["_UserCode"];
                openSubDetailsWindow("../E_tripchart_current.aspx");
            }
        }
    }

    protected void grdTripDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdTripDetails.PageIndex = e.NewPageIndex;
        LoadTripDetails();
    }

    #endregion

}