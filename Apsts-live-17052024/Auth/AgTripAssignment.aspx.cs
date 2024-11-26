using System;
using System.Data;
using Npgsql;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

public partial class Auth_AgTripAssignment : System.Web.UI.Page
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
            Session["agmodule"] = "Agent Trip Chart";
            Session["HDAgname"] = Session["_UserName"] + " ( " + Session["_UserCode"] + " )";
            Random randomclass = new Random();
            Session["rndNoCheck"] = randomclass.Next().ToString();
            hidtoken.Value = Session["rndNoCheck"].ToString();

            if (!LoadAgStation())
            {
                pnltripN.Visible = true;
                pnltripY.Visible = false;
                return;
            }
            else
            {
                pnltripN.Visible = false;
                pnltripY.Visible = true;
            }

           bookingChart_Prepared();
           bookingChart_ReadyToPrint();
           bookingChart_UpcomingTrip();
           bookingChart_TimeoverTrip();
        }

    }

    #region "Methods"
    private void bookingChart_TimeoverTrip()
    {
        try
        {

            string depotCode = Session["depot_code"].ToString();
            string dattime = DateTime.Now.Date.ToString("dd/MM/yyyy");
            wsClass obj = new wsClass();

            DataTable dt = obj.gettrip(depotCode, "0", "O", "T", dattime);
            DataRow[] dtrow = dt.Select("minutes>=startminute and minutes <=endminute");
            if (dtrow.Length > 0)
            {
                lblTimeOverTripError.Visible = false;
                grdTimeoverTrip.Visible = true;
                grdTimeoverTrip.DataSource = dtrow.CopyToDataTable();
                grdTimeoverTrip.DataBind();
                LabelTimeOverCountUP.Text = dtrow.Length.ToString();
            }
            else
            {
                LabelTimeOverCountUP.Text = "0";
                lblTimeOverTripError.Visible = true;
                grdTimeoverTrip.Visible = false;
            }
        }
        catch (Exception ex)
        {

        }
    }
    private void bookingChart_UpcomingTrip()
    {
        try
        {
            string depotCode = Session["depot_code"].ToString();
            string dattime = DateTime.Now.Date.ToString("dd/MM/yyyy");
            wsClass obj = new wsClass();

            DataTable dt = obj.gettrip(depotCode, "0", "O", "U", dattime);
            DataRow[] dtrow = dt.Select("minutes>=startminute and minutes <=endminute");
            if (dtrow.Length > 0)
            {



                lblUpcomingTripError.Visible = false;
                grdUpcomingTrip.Visible = true;
                grdUpcomingTrip.DataSource = dtrow.CopyToDataTable();
                grdUpcomingTrip.DataBind();
                LabelUpcomingCountUP.Text = dtrow.Length.ToString();
            }
            else
            {

                LabelUpcomingCountUP.Text = "0";
                lblUpcomingTripError.Visible = true;
                grdUpcomingTrip.Visible = false;


            }
        }
        catch (Exception ex)
        {

        }
    }
    private void bookingChart_Prepared()
    {
        try
        {
            string depotCode = Session["depot_code"].ToString();
            string dattime = DateTime.Now.Date.ToString("dd/MM/yyyy");
            wsClass obj = new wsClass();
            DataTable dt = obj.gettrip(depotCode, "0", "O", "P", dattime);
            DataRow[] dtrow = dt.Select("minutes>=startminute and minutes <=endminute");
            if (dtrow.Length > 0)
            {
                lblprtriperror.Visible = false;
                grdpreparedtrip.Visible = true;
                grdpreparedtrip.DataSource = dtrow.CopyToDataTable();
                grdpreparedtrip.DataBind();
                LabelPreparedCountP.Text = dtrow.Length.ToString();

            }
            else
            {
                LabelPreparedCountP.Text = "0";
                lblprtriperror.Visible = true;
                grdpreparedtrip.Visible = false;
            }
        }
        catch (Exception ex)
        {

        }

    }
    private void bookingChart_ReadyToPrint()
    {
        try
        {
            string depotCode = Session["depot_code"].ToString();
            string dattime = DateTime.Now.Date.ToString("dd/MM/yyyy");
            wsClass obj = new wsClass();
            DataTable dt = obj.gettrip(depotCode, "0", "O", "A", dattime);
            DataRow[] dtrow = dt.Select("minutes>=startminute and minutes <=endminute");
            if (dtrow.Length > 0)
            {

                lbluntriperror.Visible = false;
                grdUnpreparedtrip.Visible = true;
                grdUnpreparedtrip.DataSource = dtrow.CopyToDataTable();
                grdUnpreparedtrip.DataBind();
                LabelReadyToPrintCountUP.Text = dtrow.Length.ToString();
            }
            else
            {


                LabelReadyToPrintCountUP.Text = "0";
                lbluntriperror.Visible = true;
                grdUnpreparedtrip.Visible = false;
            }
        }
        catch (Exception ex)
        {
            // Handle the exception here
        }
    }
    private bool LoadAgStation()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "agent.getagentstation");
            MyCommand.Parameters.AddWithValue("p_usercode", Session["_UserCode"].ToString());
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    if (!Convert.IsDBNull(dt.Rows[0]["station_code"]))
                    {
                        if (dt.Rows[0]["trip_yn"].ToString().Trim() == "Y")
                        {
                            Session["_LStationCode"] = dt.Rows[0]["station_code"];
                            Session["depot_code"] = dt.Rows[0]["depot_code"];
                            return true;
                        }
                    }
                }


            }
            return false;
        }
        catch (Exception ex)
        {
            return false;
        }

    }
    private string GetDepotYN()
    {
        try
        {
            DataTable dt;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "current_booking.f_get_depotcurntyn");
            MyCommand.Parameters.AddWithValue("spcntrid", Session["_UserCode"]);
            MyCommand.Parameters.AddWithValue("spaction", "C");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["currentyn"].ToString();
                }
            }
            return "N";
        }
        catch (Exception ex)
        {
            return "N";
        }
    }
    public void GetWaybillsList()
    {
        try
        {
            ddlWaybills.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "agent.getwaybill");
            MyCommand.Parameters.AddWithValue("p_office", "0");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlWaybills.DataSource = dt;
                    ddlWaybills.DataValueField = "waybill_id";
                    ddlWaybills.DataTextField = "waybill_id";
                    ddlWaybills.DataBind();
                }
            }

            ddlWaybills.Items.Insert(0, new ListItem("SELECT", "0"));
            ddlWaybills.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlWaybills.Items.Clear();
            ddlWaybills.Items.Insert(0, new ListItem("SELECT", "0"));
            ddlWaybills.SelectedIndex = 0;
        }
    }
   

    private void openpage(string pagename)
    {
        tkt.Src = pagename;
        mpTripchart.Show();
    }


    public void loadBuses()
    {
        try
        {
            drpBuss.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "current_booking.get_buses");
            MyCommand.Parameters.AddWithValue("p_depot", Session["_LDepotCode"]);
            MyCommand.Parameters.AddWithValue("p_servicetypeid", Convert.ToInt16(Session["_BusServiceTypeCode"]));
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    drpBuss.DataSource = dt;
                    drpBuss.DataTextField = "busregistrationno";
                    drpBuss.DataValueField = "busregistrationNo";
                    drpBuss.DataBind();
                }
            }
            drpBuss.Items.Insert(0, "SELECT");
            drpBuss.Items[0].Value = "0";
            drpBuss.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            drpBuss.Items.Insert(0, "SELECT");
            drpBuss.Items[0].Value = "0";
            drpBuss.SelectedIndex = 0;
        }
    }


    public void LoadDrivers()
    {
        try
        {
            drpDriver.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "current_booking.get_driver");
            MyCommand.Parameters.AddWithValue("p_depotid", Session["_LDepotCode"].ToString());
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    drpDriver.DataSource = dt;
                    drpDriver.DataTextField = "EMPNAME";
                    drpDriver.DataValueField = "EMPCODE";
                    drpDriver.DataBind();
                }
            }

            drpDriver.Items.Insert(0, "SELECT");
            drpDriver.Items[0].Value = "0";
            drpDriver.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            drpDriver.Items.Insert(0, "SELECT");
            drpDriver.Items[0].Value = "0";
            drpDriver.SelectedIndex = 0;
        }
    }

    private void LoadConductros()
    {
        try
        {
            drpConductor.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "current_booking.get_conductor");
            MyCommand.Parameters.AddWithValue("p_depotid", Session["_LDepotCode"]);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    drpConductor.DataSource = dt;
                    drpConductor.DataTextField = "EMPNAME";
                    drpConductor.DataValueField = "EMPCODE";
                    drpConductor.DataBind();
                }
            }

            drpConductor.Items.Insert(0, "SELECT");
            drpConductor.Items[0].Value = "0";
            drpConductor.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            drpConductor.Items.Insert(0, "SELECT");
            drpConductor.Items[0].Value = "0";
            drpConductor.SelectedIndex = 0;
        }
    }


    public void alert(string msg)
    {
        lblerrmsg.Text = msg;
        mpError.Show();
    }


    public bool IsValidValues()
    {
        try
        {
            if (!_validation.IsValidString(lblSvCode.Text.Trim(), 1, 25))
            {
                alert("Invalid Values");
                return false;
            }

            if (drpBuss.SelectedValue != "0")
            {
                if (!_validation.IsValidString(drpBuss.SelectedValue, 1, 20))
                {
                    alert("Invalid Bus Selection");
                    return false;
                }
            }
            else
            {
                alert("Invalid Bus Selection");
                return false;
            }

            if (drpDriver.SelectedValue != "0")
            {
                if (!_validation.IsValidString(drpDriver.SelectedValue, 1, 10))
                {
                    alert("Invalid Driver Selection");
                    return false;
                }
            }
            else
            {
                alert("Invalid Driver Selection");
                return false;
            }

            if (drpConductor.SelectedValue != "0")
            {
                if (!_validation.IsValidString(drpConductor.SelectedValue, 1, 10))
                {
                    alert("Invalid Conductor Selection");
                    return false;
                }
            }
            else
            {
                alert("Invalid Conductor Selection");
                return false;
            }

            return true;
        }
        catch (Exception ex)
        {
            alert("Something went wrong, please contact the software admin with error code CntrTrpAsgn11");
            return false;
        }
    }

    #endregion


    #region "Events"
    protected void grdpreparedtrip_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Print")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            Session["TRIPCODE"] = grdpreparedtrip.DataKeys[index].Values["trip_code"];
            Session["_TriprptType"] = "P";
            Session["TRIPDATE"] = DateTime.Now.Date.ToShortDateString();
            Session["p_tripcode"] = Session["TRIPCODE"];
            openpage("tripchart.aspx");
        }

    }
    protected void grdpreparedtrip_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdpreparedtrip.PageIndex = e.NewPageIndex;
        bookingChart_Prepared();
    }
    protected void grdUnpreparedtrip_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdUnpreparedtrip.PageIndex = e.NewPageIndex;
        bookingChart_UpcomingTrip();
    }
    protected void grdTimeoverTrip_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdTimeoverTrip.PageIndex = e.NewPageIndex;
        bookingChart_TimeoverTrip();
    }
    protected void grdUpcomingTrip_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdUpcomingTrip.PageIndex = e.NewPageIndex;
        bookingChart_UpcomingTrip();
    }
    protected void grdUnpreparedtrip_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "proceed")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            Session["_LDepotCode"] = grdUnpreparedtrip.DataKeys[index].Values["depo_code"];
            string MtripCode = grdUnpreparedtrip.DataKeys[index].Values["trip_code"].ToString();
            Session["TRIPCODE"] = MtripCode;
            Session["_BusServiceTypeCode"] = grdUnpreparedtrip.DataKeys[index].Values["busservicetype_code"];
            Session["_DEPARTURETIME"] = grdUnpreparedtrip.DataKeys[index].Values["dept_time"];
            lblSvCode.Text = MtripCode.Trim();
            loadBuses();
            LoadDrivers();
            LoadConductros();
            Session["depotCurrentyn"] = GetDepotYN();
            if (Session["depotCurrentyn"].ToString() == "Y")
            {
                GetWaybillsList();
                ddlWaybills.Visible = true;
                txtwaybills.Visible = false;
            }
            else
            {
                ddlWaybills.Visible = false;
                txtwaybills.Visible = true;
            }

            Session["_Source"] = grdUnpreparedtrip.DataKeys[index].Values["fstation_name"].ToString();
            Session["_Destination"] = grdUnpreparedtrip.DataKeys[index].Values["tstation_name"].ToString();
            Session["_SourceAbbr"] = grdUnpreparedtrip.DataKeys[index].Values["fstation_abb"].ToString();
            Session["_DestinationAbbr"] = grdUnpreparedtrip.DataKeys[index].Values["tstation_abb"].ToString();

            mpTripAssign.Show();
        }

    }

  

    protected void grdTimeoverTrip_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "print")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            Session["TRIPCODE"] = grdTimeoverTrip.DataKeys[index].Values["trip_code"];
            Session["_TriprptType"] = "A";
            Session["TRIPDATE"] = DateTime.Now.Date.ToShortDateString();

            Session["p_tripcode"] = Session["TRIPCODE"];
            openpage("tripchart.aspx");
         
        }

    }


    protected void ButtonAllTripReadyToPrepared_Click(object sender, EventArgs e)
    {
        TextBoxSearchTripCode.Text = "";
        bookingChart_ReadyToPrint();
    }


    

    protected void ButtonSearch_Click(object sender, EventArgs e)
    {
        try
        {
            int StationCode = Convert.ToInt32(Session["_LStationCode"].ToString());
            string dattime = DateTime.Now.Date.ToString("dd/MM/yyyy");
            string _value = TextBoxSearchTripCode.Text.Trim();
            DataTable MyTable = new DataTable();

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "agent.gettriptodaysearch");
            MyCommand.Parameters.AddWithValue("fromstationcode",StationCode);
            MyCommand.Parameters.AddWithValue("p_date", dattime);
            MyCommand.Parameters.AddWithValue("triporservicecode", _value);
            DataRow[] dtrow = dt.Select("minutes>=startminute and minutes <=endminute");
            //dt = bll.SelectAll(MyCommand);
            //if (dtrow.Length == "Success")
            //{
                if (dtrow.Length > 0)
                {
                    lbluntriperror.Visible = false;
                    grdUnpreparedtrip.Visible = true;
                    grdUnpreparedtrip.DataSource = dtrow;
                    grdUnpreparedtrip.DataBind();
                }
           
        }
        catch (Exception ex)
        {
            
        }

    }
    #endregion

    protected void btnSaveTripAssign_Click(object sender, EventArgs e)
    {
        try
        {
            string p_waybill = "";
            string waybillValidMsg = "";

            if (Session["depotCurrentyn"].ToString() == "Y")
            {
                p_waybill = ddlWaybills.SelectedValue.ToString();
                waybillValidMsg = "Select valid waybill number";
            }
            else
            {
                p_waybill = txtwaybills.Text.ToString();
                waybillValidMsg = "Enter valid waybill number";
            }

            if (p_waybill.Length < 10)
            {
                mpTripAssign.Show();
                alert(waybillValidMsg);
                return;
            }

            if (!IsValidValues())
            {
                mpTripAssign.Show();
                return;
            }

            string tripCode = lblSvCode.Text.Trim();
            string busNo = drpBuss.SelectedValue;
            string driverCode = drpDriver.SelectedValue;
            string conductorCode = drpConductor.SelectedValue;

            wsClass ws = new wsClass();
            DataTable dt = ws.generate_trip_chart("Web", tripCode, p_waybill, "0", DateTime.Now.ToString("dd/mm/yyyy"), busNo,
                driverCode, "0", conductorCode, "0", "", "", "", Session["_UserCode"].ToString());
            if (dt.Rows.Count > 0)
            {
                string status = dt.Rows[0]["status"].ToString();

                if (status == "DONE")
                {
                    bookingChart_Prepared();
                    bookingChart_ReadyToPrint();
                    mpTripAssign.Hide();

                    txtwaybills.Text = "";
                    ddlWaybills.SelectedValue = "0";
                    //GetConductorMobileNubmers(drpConductor.SelectedValue);
                    //GetCTZMobileNubmers(lblSvCode.Text.Trim());
                    mpDownloadPrepared.Show();
                    drpBuss.SelectedValue = "0";
                    drpDriver.SelectedValue = "0";
                    drpConductor.SelectedValue = "0";
                }
                else if (status == "TIMECHECK")
                {
                    mpTripAssign.Show();
                    alert("Please check trip time");
                }
                else
                {
                    mpTripAssign.Show();
                    alert("Something went wrong. Please contact the admin");
                }
            }
            else
            {
                mpTripAssign.Show();
                alert("Something went wrong. Please contact the admin");
            }
        }
        catch (Exception ex)
        {
            mpTripAssign.Show();
            alert("Something went wrong. Please contact the admin");
        }

    }
}
