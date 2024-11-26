using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_SysAdmStationMgmt : BasePage
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
        Session["_moduleName"] = "Station Management";
        lblSummary.Text = "Summary as on Date " + DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
        if (!IsPostBack)
        {
            loadStates(ddlSState);
            loadDistrict(ddlSDistrict, ddlSState);
            getStationsList();
            loadStates(ddlStationState);
            loadDistrict(ddlStationDistrict, ddlStationState);
            loadCity();
        }
    }

    #region "Method"
    public void getStationsList()//M1
    {
        try
        {
            string state = ddlSState.SelectedValue.ToString();
            string district = ddlSDistrict.SelectedValue.ToString();
            string searchText = tbSearch.Text.Trim();
            gvStation.Visible = false;
            pnlNoRecord.Visible = true;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_stations");
            MyCommand.Parameters.AddWithValue("p_state", state);
            MyCommand.Parameters.AddWithValue("p_district", district);
            MyCommand.Parameters.AddWithValue("p_station", searchText);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    gvStation.DataSource = dt;
                    gvStation.DataBind();
                    lblTotStation.Text = dt.Rows[0]["totalstation"].ToString();
                    lblBSStation.Text = dt.Rows[0]["busstandstation"].ToString();
                    lblActivateStation.Text = dt.Rows[0]["actstation"].ToString();
                    lblDeactStation.Text = dt.Rows[0]["deactstation"].ToString();
                    gvStation.Visible = true;
                    pnlNoRecord.Visible = false;


                }
            }
            else
            {
                _common.ErrorLog("SysAdmStationMgmt-M1", dt.TableName);
            }
        }
        catch (Exception ex)
        {
            Errormsg("There is some error" + ex.Message);
            _common.ErrorLog("SysAdmStationMgmt-M1", ex.Message.ToString());
        }
    }
    public void loadStates(DropDownList ddl)//M2
    {
        try
        {
            ddl.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.f_get_stateslist");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddl.DataSource = dt;
                    ddl.DataTextField = "stname";
                    ddl.DataValueField = "stcode";
                    ddl.DataBind();
                }
            }
            else
            {
                _common.ErrorLog("SysAdmStationMgmt-M2", dt.TableName);
            }
            ddl.Items.Insert(0, "SELECT");
            ddl.Items[0].Value = "0";
            ddl.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddl.Items.Insert(0, "SELECT");
            ddl.Items[0].Value = "0";
            ddl.SelectedIndex = 0;
            _common.ErrorLog("SysAdmStationMgmt-M2", ex.Message.ToString());
        }
    }
    public void loadStations(DropDownList ddlState, DropDownList ddlStn, DropDownList ddlodrstn)//M3
    {
        try
        {
            string state = ddlState.SelectedValue.ToString();
            Int32 odrstation = Convert.ToInt32(ddlodrstn.SelectedValue.ToString());
            ddlStn.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_statewisestations");
            MyCommand.Parameters.AddWithValue("p_statecode", state);
            MyCommand.Parameters.AddWithValue("p_odrstation", odrstation);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlStn.DataSource = dt;
                    ddlStn.DataTextField = "stationname";
                    ddlStn.DataValueField = "stonid";
                    ddlStn.DataBind();
                }
            }
            else
            {
                _common.ErrorLog("SysAdmStationMgmt-M3", dt.TableName);
            }
            ddlStn.Items.Insert(0, "SELECT");
            ddlStn.Items[0].Value = "0";
            ddlStn.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlStn.Items.Insert(0, "SELECT");
            ddlStn.Items[0].Value = "0";
            ddlStn.SelectedIndex = 0;
            _common.ErrorLog("SysAdmStationMgmt-M3", ex.Message.ToString());
        }
    }
    public void loadDistrict(DropDownList ddlDistrict, DropDownList ddlState)//M4
    {
        try
        {
            ddlDistrict.Items.Clear();
            string State = ddlState.SelectedValue.ToString();

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.f_get_district");
            MyCommand.Parameters.AddWithValue("p_statecode", State);
            dt = bll.SelectAll(MyCommand);
         
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlDistrict.DataSource = dt;
                    ddlDistrict.DataTextField = "distname";
                    ddlDistrict.DataValueField = "distcode";
                    ddlDistrict.DataBind();
                }
            }
            else
            {
                _common.ErrorLog("SysAdmStationMgmt-M4", dt.TableName);
            }
            ddlDistrict.Items.Insert(0, "SELECT");
            ddlDistrict.Items[0].Value = "0";
            ddlDistrict.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
//Response.Write(ex.Message.ToString());
            ddlDistrict.Items.Insert(0, "SELECT");
            ddlDistrict.Items[0].Value = "0";
            ddlDistrict.SelectedIndex = 0;
            _common.ErrorLog("SysAdmStationMgmt-M4", ex.Message.ToString());
        }
    }
    public void loadCity()//M5
    {
        try
        {
            ddlStationCity.Items.Clear();
            string State = ddlStationState.SelectedValue.ToString();
            string District = ddlStationDistrict.SelectedValue.ToString();

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.f_get_city");
            MyCommand.Parameters.AddWithValue("p_statecode", State);
            MyCommand.Parameters.AddWithValue("p_distcode", District);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlStationCity.DataSource = dt;
                    ddlStationCity.DataTextField = "ctyname";
                    ddlStationCity.DataValueField = "ctycode";
                    ddlStationCity.DataBind();
                }
            }
            else
            {
                _common.ErrorLog("SysAdmStationMgmt-M5", dt.TableName);
            }
            ddlStationCity.Items.Insert(0, "SELECT");
            ddlStationCity.Items[0].Value = "0";
            ddlStationCity.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlStationCity.Items.Insert(0, "SELECT");
            ddlStationCity.Items[0].Value = "0";
            ddlStationCity.SelectedIndex = 0;
            _common.ErrorLog("SysAdmStationMgmt-M5", ex.Message.ToString());
        }
    }
    public void loadOffices()//M6
    {
        try
        {
            ddlStationOffice.Items.Clear();
            Int32 OfcLvl = 40;
            string District = ddlStationDistrict.SelectedValue.ToString();

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_ofclvlwiseoffice");
            MyCommand.Parameters.AddWithValue("p_ofclvlid", OfcLvl);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlStationOffice.DataSource = dt;
                    ddlStationOffice.DataTextField = "officename";
                    ddlStationOffice.DataValueField = "officeid";
                    ddlStationOffice.DataBind();
                }
            }
            else
            {
                _common.ErrorLog("SysAdmStationMgmt-M6", dt.TableName);
            }
            ddlStationOffice.Items.Insert(0, "SELECT");
            ddlStationOffice.Items[0].Value = "0";
            ddlStationOffice.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlStationOffice.Items.Insert(0, "SELECT");
            ddlStationOffice.Items[0].Value = "0";
            ddlStationOffice.SelectedIndex = 0;
            _common.ErrorLog("SysAdmStationMgmt-M6", ex.Message.ToString());
        }
    }
    private void saveStation()//M7
    {
        try
        {
            Int32 stationID = Convert.ToInt32(Session["_stationID"]);
            string stationEng = tbStationNameEng.Text.ToString();
            string stationLocal = tbStationNameLocal.Text.ToString();
            string state = ddlStationState.SelectedValue.ToString();
            string district = ddlStationDistrict.SelectedValue.ToString();
            string city = ddlStationCity.SelectedValue.ToString();
            string address = tbAddress.Text.ToString();
            string office = "0";
            string latitude = tbStationLatitude.Text.ToString();
            string longitude = tbStationLongitude.Text.ToString();
            string UpdatedBy = Session["_UserCode"].ToString();
            string IpAddress = HttpContext.Current.Request.UserHostAddress;
            string action = Session["_action"].ToString();
            string busstandYN = "N";
            if (cbStationBusStandYN.Checked == true)
            {
                busstandYN = "Y";
                office = ddlStationOffice.SelectedValue.ToString();
            }
            string result = "";
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_station_insertupdate");
            MyCommand.Parameters.AddWithValue("p_action", action);
            MyCommand.Parameters.AddWithValue("p_stationid", stationID);
            MyCommand.Parameters.AddWithValue("p_stationeng", stationEng);
            MyCommand.Parameters.AddWithValue("p_stationlocal", stationLocal);
            MyCommand.Parameters.AddWithValue("p_statecode", state);
            MyCommand.Parameters.AddWithValue("p_districtcode", district);
            MyCommand.Parameters.AddWithValue("p_citycode", city);
            MyCommand.Parameters.AddWithValue("p_address", address);
            MyCommand.Parameters.AddWithValue("p_busstandyn", busstandYN);
            MyCommand.Parameters.AddWithValue("p_officeid", office);
            MyCommand.Parameters.AddWithValue("p_latitude", latitude);
            MyCommand.Parameters.AddWithValue("p_longitude", longitude);
            MyCommand.Parameters.AddWithValue("p_updatedby", UpdatedBy);
            MyCommand.Parameters.AddWithValue("p_ipaddress", IpAddress);
            result = bll.InsertAll(MyCommand);
            if (result == "Success")
            {
                if (Session["_action"].ToString() == "D")
                {
                    Successmsg("Station Deactivated Successfully");
                }
                else if (Session["_action"].ToString() == "A")
                {
                    Successmsg("Station Activated Successfully");
                }
                else if (Session["_action"].ToString() == "R")
                {
                    Successmsg("Station Deleted Successfully");
                }
                else if (Session["_action"].ToString() == "U")
                {
                    Successmsg("Station details updated successfully");
                }
                else
                {
                    Successmsg("Details saved successfully");
                }
                Session["_stationID"] = "";
                Session["_action"] = "";
                clearStationData();
                getStationsList();
            }
            else
            {
                Errormsg("There is some error.");
                _common.ErrorLog("SysAdmStationMgmt-M7", result);
            }
        }
        catch (Exception ex)
        {
            Errormsg("There is some error.");
            _common.ErrorLog("SysAdmStationMgmt-M7", ex.Message.ToString());
        }
    }
    private void saveStationDistance()//M8
    {
        try
        {
            Int32 stdistID, fromStaion, toStation, fromHillDist, fromPlainDist, toHillDist, toPlainDist, tollSingle, tollDouble, parkingSingle, parkingDouble, haltTime;
            string action = Session["_action"].ToString();
            stdistID = Convert.ToInt32(Session["_stdist_ID"]);
            string fromState = ddlSDFromState.SelectedValue.ToString();
            fromStaion = Convert.ToInt32(ddlSDFromStation.SelectedValue.ToString());
            string toState = ddlSDToState.SelectedValue.ToString();
            toStation = Convert.ToInt32(ddlSDToStation.SelectedValue.ToString());
            if (string.IsNullOrEmpty(tbSDFromHillDist.Text.ToString()))
            {
                fromHillDist = 0;
            }
            else
            {
                fromHillDist = Convert.ToInt32(tbSDFromHillDist.Text.ToString());
            }
            if (string.IsNullOrEmpty(tbSDFromPlainDist.Text.ToString()))
            {
                fromPlainDist = 0;
            }
            else
            {
                fromPlainDist = Convert.ToInt32(tbSDFromPlainDist.Text.ToString());
            }
            if (string.IsNullOrEmpty(tbSDToHillDist.Text.ToString()))
            {
                toHillDist = 0;
            }
            else
            {
                toHillDist = Convert.ToInt32(tbSDToHillDist.Text.ToString());
            }
            if (string.IsNullOrEmpty(tbSDToPlainDist.Text.ToString()))
            {
                toPlainDist = 0;
            }
            else
            {
                toPlainDist = Convert.ToInt32(tbSDToPlainDist.Text.ToString());
            }
            if (string.IsNullOrEmpty(tbSDTollSingle.Text.ToString()))
            {
                tollSingle = 0;
            }
            else
            {
                tollSingle = Convert.ToInt32(tbSDTollSingle.Text.ToString());
            }
            if (string.IsNullOrEmpty(tbSDTollDouble.Text.ToString()))
            {
                tollDouble = 0;
            }
            else
            {
                tollDouble = Convert.ToInt32(tbSDTollDouble.Text.ToString());
            }
            if (string.IsNullOrEmpty(tbSDParkingSingle.Text.ToString()))
            {
                parkingSingle = 0;
            }
            else
            {
                parkingSingle = Convert.ToInt32(tbSDParkingSingle.Text.ToString());
            }
            if (string.IsNullOrEmpty(tbSDParkingDouble.Text.ToString()))
            {
                parkingDouble = 0;
            }
            else
            {
                parkingDouble = Convert.ToInt32(tbSDParkingDouble.Text.ToString());
            }
            if (string.IsNullOrEmpty(tbSDHaltTime.Text.ToString()))
            {
                haltTime = 0;
            }
            else
            {
                haltTime = Convert.ToInt32(tbSDHaltTime.Text.ToString());
            }
            double totalDist = Convert.ToDouble(fromHillDist) + Convert.ToDouble(fromPlainDist) + Convert.ToDouble(toHillDist) + Convert.ToDouble(toPlainDist);

            string UpdatedBy = Session["_UserCode"].ToString();
            string IpAddress = HttpContext.Current.Request.UserHostAddress;

            string result = "";

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_stationdistance_insertupdate");
            MyCommand.Parameters.AddWithValue("p_action", action);
            MyCommand.Parameters.AddWithValue("p_stationdistid", stdistID);
            MyCommand.Parameters.AddWithValue("p_fromstate", fromState);
            MyCommand.Parameters.AddWithValue("p_fromstation", fromStaion);
            MyCommand.Parameters.AddWithValue("p_tostate", toState);
            MyCommand.Parameters.AddWithValue("p_tostation", toStation);
            MyCommand.Parameters.AddWithValue("p_fromhilldsit", fromHillDist);
            MyCommand.Parameters.AddWithValue("p_fromplaindist", fromPlainDist);
            MyCommand.Parameters.AddWithValue("p_tohilldist", toHillDist);
            MyCommand.Parameters.AddWithValue("p_toplaindist", toPlainDist);
            MyCommand.Parameters.AddWithValue("p_totaldist", totalDist);
            MyCommand.Parameters.AddWithValue("p_tollsingle", tollSingle);
            MyCommand.Parameters.AddWithValue("p_tolldouble", tollDouble);
            MyCommand.Parameters.AddWithValue("p_parkingsingle", parkingSingle);
            MyCommand.Parameters.AddWithValue("p_parkingdouble", parkingDouble);
            MyCommand.Parameters.AddWithValue("p_halttime", haltTime);
            MyCommand.Parameters.AddWithValue("p_updatedby", UpdatedBy);
            MyCommand.Parameters.AddWithValue("p_ipaddress", IpAddress);
            result = bll.InsertAll(MyCommand);
            if (result == "Success")
            {
                if (Session["_action"].ToString() == "S")
                {
                    Successmsg("Station Distance Added successfully");
                   
                }
                else if (Session["_action"].ToString() == "U")
                   
                {
                    Successmsg("Station Distance updated successfully");
                }
                else if (Session["_action"].ToString() == "D")
                {
                    Successmsg("Station Distance Deleted successfully");
                }
                Session["_action"] = "";
                clearStationDistData();
                pnlAddStationDistance.Visible = true;
                pnlAddStation.Visible = false;
                getStationDistanceList();
                getStationsList();
            }
            else
            {
                _common.ErrorLog("SysAdmStationMgmt-M8", result);
                Errormsg("There is some error.");
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("SysAdmStationMgmt-M8", ex.Message.ToString());
        }
    }
    public void getStationDistanceList()//M9
    {
        try
        {
            pnlNoStationDist.Visible = true;
            gvStationDistance.Visible = false;
            Int32 station = Convert.ToInt32(Session["_stationID"]);
            string state = Session["_statecode"].ToString();
            string search = tbSDSearch.Text.Trim();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_stationdistance");
            MyCommand.Parameters.AddWithValue("p_stonid", station);
            MyCommand.Parameters.AddWithValue("p_statecode", state);
            MyCommand.Parameters.AddWithValue("p_station", search);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    gvStationDistance.DataSource = dt;
                    gvStationDistance.DataBind();
                    pnlNoStationDist.Visible = false;
                    gvStationDistance.Visible = true;
                }
            }
            else
            {
                _common.ErrorLog("SysAdmStationMgmt-M9", dt.TableName);
            }
        }
        catch (Exception ex)
        {
            Errormsg("There is some error" + ex.Message);
            _common.ErrorLog("SysAdmStationMgmt-M9", ex.Message.ToString());
        }
    }
    public void resetStations()
    {
        //ddlSDFromStation.Items.Clear();
        ddlSDToStation.Items.Clear();

        //ddlSDFromStation.Items.Insert(0, "SELECT");
        //ddlSDFromStation.Items[0].Value = "0";
        //ddlSDFromStation.SelectedIndex = 0;

        ddlSDToStation.Items.Insert(0, "SELECT");
        ddlSDToStation.Items[0].Value = "0";
        ddlSDToStation.SelectedIndex = 0;
        lbtncancel.Visible = false;
    }
    public void clearStationDistData()
    {
        resetStations();

        ddlSDToState.SelectedValue = "0";
        ddlSDToStation.SelectedValue = "0";

        tbSDFromHillDist.Text = "";
        tbSDFromPlainDist.Text = "";
        tbSDToHillDist.Text = "";
        tbSDToPlainDist.Text = "";

        tbSDTollSingle.Text = "";
        tbSDTollDouble.Text = "";
        tbSDParkingSingle.Text = "";
        tbSDParkingDouble.Text = "";
        tbSDHaltTime.Text = "";

        ddlSDToState.Enabled = true;
        ddlSDToStation.Enabled = true;
        lbtnSDUpdate.Visible = false;
        lbtnSDSave.Visible = true;
        lbtnSDReset.Visible = true;

    }
    public void clearStationData()
    {
        tbStationNameEng.Text = "";
        tbStationNameLocal.Text = "";
        tbStationLatitude.Text = "";
        tbStationLongitude.Text = "";
        tbAddress.Text = "";
        ddlStationState.SelectedValue = "0";
        ddlStationDistrict.SelectedValue = "0";
        ddlStationCity.SelectedValue = "0";
        cbStationBusStandYN.Checked = false;
        divOffice.Visible = false;
        ddlStationOffice.SelectedValue = "0";
        lbtncancel.Visible = false;
        lbtnSave.Visible = true;
        lbtnUpdate.Visible = false;
        lbtnReset.Visible = true;
        lblAddStationHeader.InnerText = "Add Station";

    }
    public bool validStationDistance()
    {
        int msgcount = 0;
        string msg = "";

        if (ddlSDFromState.SelectedValue == "0")
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Select From State.<br/>";
        }
        if (ddlSDFromStation.SelectedValue == "0")
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Select From Station.<br/>";
        }
        if (ddlSDToState.SelectedValue == "0")
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Select To State.<br/>";
        }
        if (ddlSDToStation.SelectedValue == "0")
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Select To Station.<br/>";
        }
        if (ddlSDFromStation.SelectedValue != "0" && ddlSDToStation.SelectedValue != "0")
        {
            if (ddlSDFromState.SelectedValue == ddlSDToState.SelectedValue && ddlSDFromStation.SelectedValue == ddlSDToStation.SelectedValue)
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". From Station and To Station Cannot be same.<br/>";
            }
        }
        if (tbSDFromHillDist.Text != "0")
        {
            if (_validation.isValideDecimalNumber(tbSDFromHillDist.Text, 1, tbSDFromHillDist.MaxLength) == false)
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Enter Valid From State Hill Distance.<br/>";
            }
        }
        if (tbSDFromPlainDist.Text != "0")
        {
            if (_validation.isValideDecimalNumber(tbSDFromPlainDist.Text, 1, tbSDFromPlainDist.MaxLength) == false)
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Enter Valid From State Plain Distance.<br/>";
            }
        }
        if (tbSDToHillDist.Text != "0")
        {
            if (_validation.isValideDecimalNumber(tbSDToHillDist.Text, 1, tbSDToHillDist.MaxLength) == false)
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Enter Valid To State Hill Distance.<br/>";
            }
        }
        if (tbSDToPlainDist.Text != "0")
        {
            if (_validation.isValideDecimalNumber(tbSDToPlainDist.Text, 1, tbSDToPlainDist.MaxLength) == false)
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Enter Valid To State Plain Distance.<br/>";
            }
        }
        if (tbSDTollSingle.Text != "0")
        {
            if (_validation.isValideDecimalNumber(tbSDTollSingle.Text, 1, tbSDTollSingle.MaxLength) == false)
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Enter Valid Toll Charges Single.<br/>";
            }
        }
        if (tbSDTollDouble.Text != "0")
        {
            if (_validation.isValideDecimalNumber(tbSDTollDouble.Text, 1, tbSDTollDouble.MaxLength) == false)
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Enter Valid Toll Charges Double.<br/>";
            }
        }
        if (tbSDParkingSingle.Text != "0")
        {
            if (_validation.isValideDecimalNumber(tbSDParkingSingle.Text, 1, tbSDParkingSingle.MaxLength) == false)
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Enter Valid Parking Charges Single.<br/>";
            }
        }
        if (tbSDParkingDouble.Text != "0")
        {
            if (_validation.isValideDecimalNumber(tbSDParkingDouble.Text, 1, tbSDParkingDouble.MaxLength) == false)
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Enter Valid Parking Charges Double.<br/>";
            }
        }
        if (tbSDHaltTime.Text != "0")
        {
            if (_validation.isValideDecimalNumber(tbSDHaltTime.Text, 1, tbSDHaltTime.MaxLength) == false)
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Enter Valid Halt Time.<br/>";
            }
        }
        if (msgcount > 0)
        {
            Errormsg(msg);
            return false;
        }

        return true;
    }
    private bool validStation()
    {
        int msgcount = 0;
        string msg = "";

        if (_validation.IsValidString(tbStationNameEng.Text, 1, tbStationNameEng.MaxLength) == false)
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Enter Station Name (English).<br/>";
        }
        if (_validation.IsValidString(tbStationNameLocal.Text, 0, tbStationNameLocal.MaxLength) == false)
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Enter Station Name (Local).<br/>";
        }
        if (ddlStationState.SelectedValue == "0")
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Select Station State.<br/>";
        }
        if (ddlStationDistrict.SelectedValue == "0")
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Select Station District.<br/>";
        }
        if (ddlStationCity.SelectedValue == "0")
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Select Station City.<br/>";
        }
        if (_validation.IsValidString(tbAddress.Text, 1, tbAddress.MaxLength) == false)
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Enter Address.<br/>";
        }
        if (cbStationBusStandYN.Checked == true)
        {
            if (ddlStationOffice.SelectedValue == "0")
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Select Bus Stand Office.<br/>";
            }
        }
        if (msgcount > 0)
        {
            Errormsg(msg);
            return false;
        }

        return true;
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
    #endregion

    #region "Events"
    protected void resetStationData_Click(object sender, EventArgs e)
    {
        clearStationData();
    }
    protected void resetStationDistData_Click(object sender, EventArgs e)
    {
        clearStationDistData();
    }
    protected void cbStationBusStandYN_CheckedChange(object sender, EventArgs e)
    {
        if (cbStationBusStandYN.Checked == true)
        {
            loadOffices();
            divOffice.Visible = true;
        }
        else
        {
            divOffice.Visible = false;
        }
    }
    protected void gvStation_RowCommand(object sender, GridViewCommandEventArgs e)//E1
    {
        try
        {
            if (e.CommandName == "activate")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                Session["_stationID"] = gvStation.DataKeys[index].Values["stonid"].ToString();
                string stationName = gvStation.DataKeys[index].Values["stationnameeng"].ToString();
                Session["_action"] = "activate";
                ConfirmMsg("Do you want to Activate " + stationName + " ?");
            }
            if (e.CommandName == "deactivate")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                Session["_stationID"] = gvStation.DataKeys[index].Values["stonid"].ToString();
                string stationName = gvStation.DataKeys[index].Values["stationnameeng"].ToString();
                Session["_action"] = "deactivate";
                ConfirmMsg("Do you want to Deactivate " + stationName + " ?");
            }
            if (e.CommandName == "deleteStation")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                Session["_stationID"] = gvStation.DataKeys[index].Values["stonid"].ToString();
                string stationName = gvStation.DataKeys[index].Values["stationnameeng"].ToString();
                Session["_action"] = "delete";
                ConfirmMsg("Do you want to Delete " + stationName + " ?");
            }
            if (e.CommandName == "UpdateStation")
            {
lbtncancel.Visible = true;
                lbtnReset.Visible = false;
                lbtnSave.Visible = false;
                lbtnUpdate.Visible = true;
                pnlAddStation.Visible = true;
                pnlAddStationDistance.Visible = false;
                int index = Convert.ToInt32(e.CommandArgument);
                Session["_stationID"] = gvStation.DataKeys[index].Values["stonid"].ToString();
                string stationName = gvStation.DataKeys[index].Values["stationnameeng"].ToString();
                lblAddStationHeader.InnerText = "Update Station Details of " + stationName.ToUpper();
                tbStationNameEng.Text = gvStation.DataKeys[index].Values["stationnameeng"].ToString();
                tbStationNameLocal.Text = gvStation.DataKeys[index].Values["stationnamelocal"].ToString();
                tbAddress.Text = gvStation.DataKeys[index].Values["address"].ToString();
                tbStationLatitude.Text = gvStation.DataKeys[index].Values["latitude"].ToString();
                tbStationLongitude.Text = gvStation.DataKeys[index].Values["longittude"].ToString();
                loadStates(ddlStationState);
                ddlStationState.SelectedValue = gvStation.DataKeys[index].Values["statecode"].ToString();
                loadDistrict(ddlStationDistrict, ddlStationState);
                ddlStationDistrict.SelectedValue = gvStation.DataKeys[index].Values["districtcode"].ToString();
                loadCity();
                ddlStationCity.SelectedValue = gvStation.DataKeys[index].Values["citycode"].ToString();
                if (gvStation.DataKeys[index].Values["busstandflag"].ToString() == "Y")
                {
                    cbStationBusStandYN.Checked = true;
                    loadOffices();
                    ddlStationOffice.SelectedValue = gvStation.DataKeys[index].Values["office_id"].ToString();
                    divOffice.Visible = true;
                }
		if (gvStation.DataKeys[index].Values["deleteyn"].ToString() == "N")
                {
                    tbStationNameEng.Enabled = false;
                    tbStationNameLocal.Enabled = false;
                    ddlStationState.Enabled = false;
                    ddlStationDistrict.Enabled = false;
                    ddlStationCity.Enabled = false;
                }
                else
                {
                    tbStationNameEng.Enabled = true;
                    tbStationNameLocal.Enabled = true;
                    ddlStationState.Enabled = true;
                    ddlStationDistrict.Enabled = true;
                    ddlStationCity.Enabled = true;
                }

                
            }
            if (e.CommandName == "addstationdistance")
            {
 
                pnlStationDetails.Visible = false;
                pnlAddStation.Visible = false;
                pnlAddStationDistance.Visible = true;
                clearStationDistData();
                int index = Convert.ToInt32(e.CommandArgument);
                Session["_stationID"] = gvStation.DataKeys[index].Values["stonid"].ToString();
                Session["_statecode"] = gvStation.DataKeys[index].Values["statecode"].ToString();
                loadStates(ddlSDFromState);
                loadStates(ddlSDToState);
                resetStations();
                ddlSDFromState.SelectedValue = gvStation.DataKeys[index].Values["statecode"].ToString();
                loadStations(ddlSDFromState, ddlSDFromStation, ddlSDToStation);

                ddlSDFromStation.SelectedValue = Session["_stationID"].ToString();
                lblSDStationName.Text = (gvStation.DataKeys[index].Values["stationnameeng"].ToString() + "," + gvStation.DataKeys[index].Values["state"].ToString()).ToUpper();
		getStationDistanceList();
               
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("SysAdmStationMgmt-E1", ex.Message.ToString());
        }

    }
    protected void gvStationDistance_RowCommand(object sender, GridViewCommandEventArgs e)//E2
    {
        try
        {
            if (e.CommandName == "deleteStationDistance")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                Session["_stdist_ID"] = gvStationDistance.DataKeys[index].Values["stdtid"].ToString();
                string frstationName = gvStationDistance.DataKeys[index].Values["fromstation"].ToString();
                string tostationName = gvStationDistance.DataKeys[index].Values["tostation"].ToString();
                Session["_action"] = "deleteSD";
                ConfirmMsg("Do you want to Delete Station Distance between <b>" + frstationName + "</b> and <b>" + tostationName + "</b>?");
            }
            if (e.CommandName == "UpdateStationDistance")
            {
                clearStationDistData();
                //stdtid,,,,,,,,,totdistancekm,,,
                //,,,dhabahalttime,dhabaid,status,fromstate,tostate,fromstation,tostation
                int index = Convert.ToInt32(e.CommandArgument);
                Session["_stdist_ID"] = gvStationDistance.DataKeys[index].Values["stdtid"].ToString();

                Session["_fromstatecode"] = gvStationDistance.DataKeys[index].Values["frstate_id"].ToString();
                Session["_fromstationID"] = gvStationDistance.DataKeys[index].Values["frston_id"].ToString();
                loadStates(ddlSDToState);
                resetStations();

                ddlSDToState.SelectedValue = gvStationDistance.DataKeys[index].Values["tostateid"].ToString();
                loadStations(ddlSDToState, ddlSDToStation, ddlSDFromStation);
                ddlSDToStation.SelectedValue = gvStationDistance.DataKeys[index].Values["tostonid"].ToString();

                tbSDFromHillDist.Text = gvStationDistance.DataKeys[index].Values["frstatehillkm"].ToString();
                tbSDToHillDist.Text = gvStationDistance.DataKeys[index].Values["tostatehillkm"].ToString();
                tbSDFromPlainDist.Text = gvStationDistance.DataKeys[index].Values["frstateplainkm"].ToString();
                tbSDToPlainDist.Text = gvStationDistance.DataKeys[index].Values["tostateplainkm"].ToString();
                tbSDTollSingle.Text = gvStationDistance.DataKeys[index].Values["tollchargesingle"].ToString();
                tbSDTollDouble.Text = gvStationDistance.DataKeys[index].Values["tollchargedouble"].ToString();
                tbSDParkingSingle.Text = gvStationDistance.DataKeys[index].Values["parkingchargesingle"].ToString();
                tbSDParkingDouble.Text = gvStationDistance.DataKeys[index].Values["parkingchargedouble"].ToString();
                tbSDHaltTime.Text = gvStationDistance.DataKeys[index].Values["halttimemin"].ToString();

                lblAddSDHeader.InnerText = "Update Station Distance";
                ddlSDToState.Enabled = false;
                ddlSDToStation.Enabled = false;
                lbtnSDSave.Visible = false;
                lbtnSDUpdate.Visible = true;
                lbtnSDReset.Visible = false;

            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("SysAdmStationMgmt-E2", ex.Message.ToString());
        }

    }
    protected void gvStation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbtnActivate = (LinkButton)e.Row.FindControl("lbtnActivate");
            LinkButton lbtnDiscontinue = (LinkButton)e.Row.FindControl("lbtnDiscontinue");
            LinkButton lbtnDelete = (LinkButton)e.Row.FindControl("lbtnDeleteStation");
            DataRowView rowView = (DataRowView)e.Row.DataItem;
            lbtnActivate.Visible = false;
            lbtnDiscontinue.Visible = false;
            lbtnDelete.Visible = false;


            if (rowView["discontinue_yn"].ToString() == "Y")
            {
                lbtnDiscontinue.Visible = true;
            }

            if (rowView["status"].ToString() == "D")
            {
                lbtnActivate.Visible = true;
            }
            if (rowView["deleteyn"].ToString() == "Y")
            {
                lbtnDiscontinue.Visible = false;
                lbtnDelete.Visible = true;
            }
        }
    }
    protected void gvStation_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvStation.PageIndex = e.NewPageIndex;
        getStationsList();
    }
    protected void gvStationDistance_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvStationDistance.PageIndex = e.NewPageIndex;
        getStationDistanceList();
    }
    protected void saveStation_Click(object sender, EventArgs e)
    {
        if (validStation() == false)
        {
            return;
        }
        Session["_stationID"] = 0;
        Session["_action"] = "S";
        ConfirmMsg("Do you want to save station?");
    }
    protected void updateStation_Click(object sender, EventArgs e)
    {
        if (validStation() == false)
        {
            return;
        }
        Session["_action"] = "U";
        ConfirmMsg("Do you want to update station?");
    }
    protected void saveStationDistance_Click(object sender, EventArgs e)
    {
        if (validStationDistance() == false)
        {
            return;
        }
        Session["_stdist_ID"] = 0;
        Session["_action"] = "saveSD";
        ConfirmMsg("Do you want to save Station Distance?");
    }
    protected void updateStationDistance_Click(object sender, EventArgs e)
    {
        if (validStationDistance() == false)
        {
            return;
        }
        Session["_action"] = "UpdateSD";
        ConfirmMsg("Do you want to update station Distance Details?");
    }
    protected void ddlStationState_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadDistrict(ddlStationDistrict, ddlStationState);
    }
    protected void ddlSState_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadDistrict(ddlSDistrict, ddlSState);
    }
    protected void ddlStationDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadCity();
    }
    protected void ddlSDFromState_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadStations(ddlSDFromState, ddlSDFromStation, ddlSDToState);
    }
    protected void ddlSDToState_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadStations(ddlSDToState, ddlSDToStation, ddlSDFromStation);
    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        if (Session["_action"].ToString() == "S")
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.fn_check_name_duplicacy");
            MyCommand.Parameters.AddWithValue("p_search", tbStationNameEng.Text);
            MyCommand.Parameters.AddWithValue("p_action", "Station");
            DataTable MyTable = bll.SelectAll(MyCommand);
            if (MyTable.Rows.Count > 0)
            {
                if (MyTable.Rows[0]["status"].ToString() == "Exist")
                {
                    ConfirmMsg("Similar Station Name Already Exist <br /><span style=" + "color:green" + ">" + MyTable.Rows[0]["list"].ToString() + "</span> <br />Do you still want to save ?");
                    Session["_Action"] = "SS";
                   
                    return;
                }
                if (MyTable.Rows[0]["status"].ToString() == "Not Exist")
                {
                    saveStation();
                }
            }
        }
        else if (Session["_action"].ToString() == "SS")
        {
            Session["_action"] = "S";
            saveStation();
        }
        else if (Session["_action"].ToString() == "U")
        {
            saveStation();
        }
        else if (Session["_action"].ToString() == "activate")
        {
            Session["_action"] = "A";
            saveStation();

        }
        else if (Session["_action"].ToString() == "deactivate")
        {
            Session["_action"] = "D";
            saveStation();
        }
        else if (Session["_action"].ToString() == "delete")
        {
            Session["_action"] = "R";
            saveStation();
        }
        else if (Session["_action"].ToString() == "saveSD")
        {
            Session["_action"] = "S";
            saveStationDistance();
        }
        else if (Session["_action"].ToString() == "UpdateSD")
        {
            Session["_action"] = "U";
            saveStationDistance();
        }
        else if (Session["_action"].ToString() == "deleteSD")
        {
            Session["_action"] = "D";
            saveStationDistance();
        }
    }
    protected void lbtnSearchStation_Click(object sender, EventArgs e)
    {
        getStationsList();
    }
    protected void lbtnResetSearch_Click(object sender, EventArgs e)
    {
        loadStates(ddlSState);
        loadDistrict(ddlSDistrict, ddlSState);
        tbSearch.Text = "";
        getStationsList();
    }
    protected void lbtnSearchSD_Click(object sender, EventArgs e)
    {
        getStationDistanceList();
    }
    protected void lbtnResetSDSearch_Click(object sender, EventArgs e)
    {
        tbSDSearch.Text = "";
        getStationDistanceList();
    }
    protected void lbtncancel_Click(object sender, EventArgs e)
    {
        clearStationData();
        pnlAddStation.Visible = true;
        pnlAddStationDistance.Visible = false;
    }
    protected void lbtnClose_Click(object sender, EventArgs e)
    {
        pnlStationDetails.Visible = true;
        pnlAddStationDistance.Visible = false;
        pnlAddStation.Visible = true;
        clearStationData();
        clearStationDistData();
    }
    #endregion

    protected void lbtndownload_Click(object sender, EventArgs e)
    {
        Errormsg("Coming Soon");
    }

    protected void Unnamed_Click(object sender, EventArgs e)
    {
        Errormsg("Coming Soon");
    }

    protected void gvStationDistance_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbtndelete = (LinkButton)e.Row.FindControl("lbtnDeleteStation");
           
            DataRowView rowView = (DataRowView)e.Row.DataItem;
            lbtndelete.Visible = false;
            
            if (rowView["delete_yn"].ToString() == "Y")
            {

                lbtndelete.Visible = true;
            }
        }
    }
}