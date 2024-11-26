using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Npgsql;
using System.IO;
using System.Xml;
using System.Collections;

public partial class Auth_SysAdmRouteMgmt : BasePage
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
        lblSummary.Text = "Summary as on Date " + DateTime.Now.ToString("dd/MM/yyyy hh:ss tt");
        Session["_moduleName"] = "Route Management";
        if (!IsPostBack)
        {
            Random randomclass = new Random();
            Session["rndNoCheck"] = randomclass.Next().ToString();
            hidtoken.Value = Session["rndNoCheck"].ToString();
            divNoRecord.Visible = true;
            divNoSearchRecord.Visible = false;
            getRoutes();
            loadStates(ddlFromState);
            loadStations(ddlFromState, ddlFromStation, ddlToStation);
           loadStates(ddlToState);
            loadStations(ddlToState, ddlToStation, ddlFromStation);
            loadStates(ddlViaState);
            loadStations(ddlViaState, ddlViaStation, null);
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
    public void loadStates(DropDownList ddl)//M1
    {
        try
        {
            ddl.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.f_get_stateslist");
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                ddl.DataSource = dt;
                ddl.DataTextField = "stname";
                ddl.DataValueField = "stcode";
                ddl.DataBind();
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
            Errormsg("There is some error" + ex.Message);
            _common.ErrorLog("SysAdmRouteMgmt.aspx-0001", ex.Message.ToString());
        }
    }
    public void loadStations(DropDownList ddlState, DropDownList ddlStn, DropDownList ddlodrstn)//M2
    {
        try
        {
            string state = ddlState.SelectedValue.ToString();
            Int32 odrstation = 0;
            if (ddlodrstn != null)
            {
                if (ddlodrstn.SelectedValue.ToString() == "")
                {
                    odrstation = 0;
                }
                else
                {
                    odrstation = Convert.ToInt32(ddlodrstn.SelectedValue.ToString());
                }
            }
            ddlStn.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_statewisestations");
            MyCommand.Parameters.AddWithValue("p_statecode", state);
            MyCommand.Parameters.AddWithValue("p_odrstation", odrstation);
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                ddlStn.DataSource = dt;
                ddlStn.DataTextField = "stationname";
                ddlStn.DataValueField = "stonid";
                ddlStn.DataBind();
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
            Errormsg("There is some error" + ex.Message);
            _common.ErrorLog("SysAdmRouteMgmt.aspx-0002", ex.Message.ToString());
        }
    }
    public bool validRoute()
    {
        int msgcount = 0;
        string msg = "";

        if (ddlFromState.SelectedValue == "0")
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Select From State.<br/>";
        }
        if (ddlFromStation.SelectedValue == "0")
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Select From Station.<br/>";
        }
        if (ddlToState.SelectedValue == "0")
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Select To State.<br/>";
        }
        if (ddlToStation.SelectedValue == "0")
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Select To Station.<br/>";
        }
        if (ddlFromStation.SelectedValue != "0" && ddlToStation.SelectedValue != "0")
        {
            if (ddlFromState.SelectedValue == ddlToState.SelectedValue && ddlFromStation.SelectedValue == ddlToStation.SelectedValue)
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". From Station and To Station Cannot be same.<br/>";
            }
        }
        //if (ddlViaState.SelectedValue == "0")
        //{
        //	msgcount = msgcount + 1;
        //	msg = msg + msgcount.ToString() + ". Select Via State.<br/>";
        //}
        //if (ddlViaStation.SelectedValue == "0")
        //{
        //	msgcount = msgcount + 1;
        //	msg = msg + msgcount.ToString() + ". Select Via Station.<br/>";
        //}
        if (msgcount > 0)
        {
            Errormsg(msg);
            return false;
        }

        return true;
    }
    public bool validStationRoute()
    {
        int msgcount = 0;
        string msg = "";

        if (ddlRouteFromStation.SelectedValue == "0")
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Select Route From Station.<br/>";
        }
        if (ddlRouteToStation.SelectedValue == "0")
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Select Route To Station.<br/>";
        }
        if (ddlRouteFromStation.SelectedValue != "0" && ddlRouteToStation.SelectedValue != "0")
        {
            if (ddlRouteFromStation.SelectedValue == ddlRouteToStation.SelectedValue)
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Route From Station and To Station Cannot be same.<br/>";
            }
        }
        if (msgcount > 0)
        {
            Errormsg(msg);
            return false;
        }

        return true;
    }
    public void clearRouteData()
    {
        ddlFromState.SelectedValue = "0";
        ddlToState.SelectedValue = "0";
        ddlViaState.SelectedValue = "0";
        loadStations(ddlFromState, ddlFromStation, ddlToStation);
        loadStations(ddlToState, ddlToStation, ddlFromStation);
        loadStations(ddlViaState, ddlViaStation, null);
        lbtnSave.Visible = true;
        lbtnUpdate.Visible = false;
        lbtncancel.Visible = false;
        lbtnReset.Visible = true;
    }
    private void saveRoute()//M3
    {
        try
        {
            Int32 routeCode = Convert.ToInt32(Session["_routeCode"].ToString());
            string fromState = ddlFromState.SelectedValue.ToString();
            Int32 fromStation = Convert.ToInt32(ddlFromStation.SelectedValue.ToString());
            string toState = ddlToState.SelectedValue.ToString();
            Int32 toStation = Convert.ToInt32(ddlToStation.SelectedValue.ToString());

            string viaState = ddlViaState.SelectedValue.ToString();
            Int32 viaStation = Convert.ToInt32(ddlViaStation.SelectedValue.ToString());
            string viastationname = "";
            if (ddlViaStation.SelectedValue.ToString() == "0")
            {
                viastationname = "";
            }
            else
            {
                viastationname = "- Via - " + ddlViaStation.SelectedItem.ToString();
            }

            string routename = ddlFromStation.SelectedItem.ToString() + " - " + ddlToStation.SelectedItem.ToString() + viastationname;
            string UpdatedBy = Session["_UserCode"].ToString();
            string IpAddress = HttpContext.Current.Request.UserHostAddress;
            string action = Session["_action"].ToString();
            string result = "";

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_route_insertupdate_notvia");
            MyCommand.Parameters.AddWithValue("p_action", action);
            MyCommand.Parameters.AddWithValue("p_routecode", routeCode);
            MyCommand.Parameters.AddWithValue("p_routename", routename);
            MyCommand.Parameters.AddWithValue("p_fromstate", fromState);
            MyCommand.Parameters.AddWithValue("p_fromstation", fromStation);
            MyCommand.Parameters.AddWithValue("p_tostate", toState);
            MyCommand.Parameters.AddWithValue("p_tostation", toStation);
            MyCommand.Parameters.AddWithValue("p_viastate", viaState);
            MyCommand.Parameters.AddWithValue("p_viastation", viaStation);
            MyCommand.Parameters.AddWithValue("p_updatedby", UpdatedBy);
            MyCommand.Parameters.AddWithValue("p_ipaddress", IpAddress);
            //result = bll.InsertAll(MyCommand);

            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["f_route_insertupdate_notvia"].ToString() == "00")
                {
                    Errormsg("Route Aleardy Exist.");
                }
                else
                {
                    if (Session["_action"].ToString() == "D")
                    {
                        Successmsg("Route Deactivated Successfully");
                    }
                    else if (Session["_action"].ToString() == "A")
                    {
                        Successmsg("Route Activated Successfully");
                    }
                    else
                    {
                        Successmsg("Route created successfully");
                    }
                    Session["_routeCode"] = "";
                    Session["_action"] = "";
                    clearRouteData();
                    getRoutes();
                }

            }
            else
            {
                Errormsg("There is some error.");
            }
        }
        catch (Exception ex)
        {
            Errormsg("There is some error" + ex.Message);
            _common.ErrorLog("SysAdmRouteMgmt.aspx-0004", ex.Message.ToString());
        }
    }
    private void getRoutes()//M4
    {
        try
        {
            string searchText = tbSearch.Text.Trim();
            gvRoutes.Visible = false;
            pnlNoRoute.Visible = true;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_routes");
            MyCommand.Parameters.AddWithValue("p_route", searchText);
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                gvRoutes.DataSource = dt;
                gvRoutes.DataBind();
                lblTotalRoute.Text = dt.Rows[0]["totalroute"].ToString();
                lblActive.Text = dt.Rows[0]["actroute"].ToString();
                lblDeactive.Text = dt.Rows[0]["deactroute"].ToString();
                gvRoutes.Visible = true;
                pnlNoRoute.Visible = false;


            }
            else
            {
                gvRoutes.DataSource = dt;
                gvRoutes.DataBind();
            }
        }
        catch (Exception ex)
        {
            gvRoutes.DataSource = dt;
            gvRoutes.DataBind();
            Errormsg("There is some error" + ex.Message);
            _common.ErrorLog("SysAdmRouteMgmt.aspx-0005", ex.Message.ToString());
        }
    }
    private void getNearestStations()//M5
    {
        try
        {
            Int32 routeid = Convert.ToInt32(Session["_routeCode"]);
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_neareststation_forroute");
            MyCommand.Parameters.AddWithValue("p_routeid", routeid);
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                string routeDone = dt.Rows[0]["checkyn"].ToString();
                if (routeDone == "Y")
                {
                    LabelRouteStationDoneInfo.Text = "You have already added all stations ('From Station' to 'To Station') in this route. (for modify/update, firstly delete stations one by one)";
                    PanelRouteStationAddControls.Visible = false;

                    ddlRouteFromStation.Items.Insert(0, "-SELECT-");
                    ddlRouteFromStation.Items[0].Value = "0";
                    ddlRouteFromStation.SelectedIndex = 0;

                    ddlRouteToStation.Items.Insert(0, "-SELECT-");
                    ddlRouteToStation.Items[0].Value = "0";
                    ddlRouteToStation.SelectedIndex = 0;
                }
                else
                {
                    PanelRouteStationAddControls.Visible = true;
                    LabelRouteStationDoneInfo.Text = "";
                    ddlRouteFromStation.Items.Insert(0, dt.Rows[0]["fr_station"].ToString());
                    ddlRouteFromStation.Items[0].Value = dt.Rows[0]["fr_stonid"].ToString();
                    ddlRouteFromStation.SelectedIndex = 0;

                    ddlRouteToStation.DataSource = dt;
                    ddlRouteToStation.DataTextField = "to_station";
                    ddlRouteToStation.DataValueField = "to_stonid";
                    ddlRouteToStation.DataBind();

                    ddlRouteToStation.Items.Insert(0, "-SELECT-");
                    ddlRouteToStation.Items[0].Value = "0";
                    ddlRouteToStation.SelectedIndex = 0;
                }
            }
            else
            {
                LabelRouteStationDoneInfo.Text = "No Station available for last station you added in this route. Please add stations distance.";
                PanelRouteStationAddControls.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Errormsg("There is some error" + ex.Message);
            _common.ErrorLog("SysAdmRouteMgmt.aspx-0006", ex.Message.ToString());
        }
    }
    private void getRouteStations()//M6
    {
        try
        {
            gvRouteStations.Visible = false;
            PanelNoRouteStation.Visible = true;
            Int32 routeid = Convert.ToInt32(Session["_routeCode"]);
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_routestations");
            MyCommand.Parameters.AddWithValue("p_routeid", routeid);
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                gvRouteStations.DataSource = dt;
                gvRouteStations.DataBind();
                gvRouteStations.Visible = true;
                PanelNoRouteStation.Visible = false;
                showDeleteButtonInGridViewRouteStations();
                int total = dt.AsEnumerable().Sum(row => row.Field<int>("totdist"));
                lbltottaldistancekm.Text = "Total Km " + total.ToString();
            }
        }
        catch (Exception ex)
        {
            Errormsg("There is some error" + ex.Message);
            _common.ErrorLog("SysAdmRouteMgmt.aspx-0007", ex.Message.ToString());
        }
    }
    public void showDeleteButtonInGridViewRouteStations()
    {
        Int32 rowCount = gvRouteStations.Rows.Count;
        if (rowCount > 0)
        {
            LinkButton lbtnStationDelete = (LinkButton)gvRouteStations.Rows[rowCount - 1].FindControl("lbtnStationDelete");
            lbtnStationDelete.Visible = true;
        }
    }
    private void getRouteAllStations()//M6
    {
        try
        {
            gvStations.Visible = false;
            PanelNoStation.Visible = true;
            Int32 routeid = Convert.ToInt32(Session["_routeCode"]);
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_routestations");
            MyCommand.Parameters.AddWithValue("p_routeid", routeid);
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                gvStations.DataSource = dt;
                gvStations.DataBind();
                gvStations.Visible = true;
                int total = dt.AsEnumerable().Sum(row => row.Field<int>("totdist"));
                totkm.Text = "Total Km " + total.ToString();
                PanelNoStation.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Errormsg("There is some error" + ex.Message);
            _common.ErrorLog("SysAdmRouteMgmt.aspx-0008", ex.Message.ToString());
        }
    }
    private void saveRouteStation()//M7
    {
        try
        {
            Int32 routeCode = Convert.ToInt32(Session["_routeCode"].ToString());
            Int32 stnSeq = Convert.ToInt32(Session["_stnSeq"].ToString());

            string[] fromStn = ddlRouteFromStation.SelectedValue.Split(new char[] { ',' });
            string[] toStn = ddlRouteToStation.SelectedValue.Split(new char[] { ',' });
            Int32 toStation = 0;
            string toState = "0";
            Int32 fromStation = 0;
            string fromState = "0";


            if (Session["_action"].ToString() == "D")
            {

            }
            else
            {

                if (ddlRouteFromStation.SelectedValue != "0")
                {
                    fromStation = Convert.ToInt32(fromStn[0].ToString());
                    fromState = fromStn[1].ToString();
                }
                if (ddlRouteToStation.SelectedValue != "0")
                {
                    toStation = Convert.ToInt32(toStn[0].ToString());
                    toState = toStn[1].ToString();
                }
            }

            string UpdatedBy = Session["_UserCode"].ToString();
            string IpAddress = HttpContext.Current.Request.UserHostAddress;
            string action = Session["_action"].ToString();
            string result = "";
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_routestation_insertupdate");
            MyCommand.Parameters.AddWithValue("p_action", action);
            MyCommand.Parameters.AddWithValue("p_routecode", routeCode);
            MyCommand.Parameters.AddWithValue("p_stnseq", stnSeq);
            MyCommand.Parameters.AddWithValue("p_fromstation", fromStation);
            MyCommand.Parameters.AddWithValue("p_fromstate", fromState);
            MyCommand.Parameters.AddWithValue("p_tostation", toStation);
            MyCommand.Parameters.AddWithValue("p_tostate", toState);
            MyCommand.Parameters.AddWithValue("p_updatedby", UpdatedBy);
            MyCommand.Parameters.AddWithValue("p_ipaddress", IpAddress);
            result = bll.InsertAll(MyCommand);
            if (result == "Success")
            {
                if (Session["_action"].ToString() == "S")
                {
                    Successmsg("Route station added successfully");
                }
                else
                {
                    Successmsg("Route station deleted successfully");
                }
                getNearestStations();
                getRouteStations();
            }
            else
            {
                Errormsg("There is some error.");
            }
        }
        catch (Exception ex)
        {
            Errormsg("There is some error" + ex.Message);
            _common.ErrorLog("SysAdmRouteMgmt.aspx-0009", ex.Message.ToString());
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
    #endregion

    #region "Events"
    protected void gvRoutes_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        CsrfTokenValidate();
        if (e.CommandName == "activate")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            Session["_routeCode"] = gvRoutes.DataKeys[index].Values["routeid"].ToString();
            string routeName = gvRoutes.DataKeys[index].Values["routename"].ToString();
            Session["_action"] = "activate";
            ConfirmMsg("Do you want to Activate <b>" + routeName + "</b> ?");
        }
        if (e.CommandName == "deactivate")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            Session["_routeCode"] = gvRoutes.DataKeys[index].Values["routeid"].ToString();
            string routeName = gvRoutes.DataKeys[index].Values["routename"].ToString();
            Session["_action"] = "deactivate";
            ConfirmMsg("Do you want to Deactivate <b>" + routeName + "</b> ?");
        }
        if (e.CommandName == "AddStation")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            Session["_routeCode"] = gvRoutes.DataKeys[index].Values["routeid"].ToString();
            lblRoute.Text = gvRoutes.DataKeys[index].Values["routename"].ToString();
            getNearestStations();
            getRouteStations();
            pnlAddRoute.Visible = false;
            pnlStation.Visible = true;
            pnlViewStations.Visible = false;
        }
        if (e.CommandName == "viewRouteStations")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            Session["_routeCode"] = gvRoutes.DataKeys[index].Values["routeid"].ToString();
            lblStnRouteName.Text = gvRoutes.DataKeys[index].Values["routename"].ToString();
            getRouteAllStations();
            pnlAddRoute.Visible = false;
            pnlStation.Visible = false;
            pnlViewStations.Visible = true;
        }
    }
    protected void gvRoutes_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbtnActivate = (LinkButton)e.Row.FindControl("lbtnActivate");
            LinkButton lbtnDiscontinue = (LinkButton)e.Row.FindControl("lbtnDiscontinue");
            LinkButton lbtnDelete = (LinkButton)e.Row.FindControl("lbtnDeleteRoute");
            DataRowView rowView = (DataRowView)e.Row.DataItem;
            lbtnActivate.Visible = false;
            lbtnDiscontinue.Visible = false;
            if (rowView["deleteyn"].ToString() == "Y")
            {
                lbtnDiscontinue.Visible = true;
            }
            if (rowView["status"].ToString() == "D")
            {
                lbtnActivate.Visible = true;
            }
        }
    }
    protected void gvRouteStations_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        CsrfTokenValidate();
        if (e.CommandName == "deleteStation")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            Session["_routeCode"] = gvRouteStations.DataKeys[index].Values["routeid"].ToString();
            Session["_stnSeq"] = gvRouteStations.DataKeys[index].Values["stn_seq"].ToString();
            Session["_action"] = "deleteStation";
            ConfirmMsg("Do you want to Delete Station ?");
        }
    }
    protected void ddlFromState_SelectedIndexChanged(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        loadStations(ddlFromState, ddlFromStation, ddlToStation);
    }
    protected void ddlToState_SelectedIndexChanged(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        loadStations(ddlToState, ddlToStation, ddlFromStation);
    }
    protected void ddlViaState_SelectedIndexChanged(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        loadStations(ddlViaState, ddlViaStation, null);
    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (Session["_action"].ToString() == "S")
        {
            saveRoute();
        }
        else if (Session["_action"].ToString() == "activate")
        {
            Session["_action"] = "A";
            saveRoute();

        }
        else if (Session["_action"].ToString() == "deactivate")
        {
            Session["_action"] = "D";
            saveRoute();
        }
        else if (Session["_action"].ToString() == "station")
        {
            Session["_action"] = "S";
            Session["_stnSeq"] = 0;
            saveRouteStation();
        }
        else if (Session["_action"].ToString() == "deleteStation")
        {
            Session["_action"] = "D";
            saveRouteStation();
        }
    }
    public void lbtnAddStationCancel_Click(object sender, System.EventArgs e)
    {
        CsrfTokenValidate();
        clearRouteData();
        pnlAddRoute.Visible = true;
        pnlStation.Visible = false;
        pnlViewStations.Visible = false;
        Session["_routeCode"] = "";
        Session["_stnSeq"] = "";
        Session["_action"] = "";
    }
    protected void saveRoute_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (validRoute() == false)
        {
            return;
        }
        Session["_routeCode"] = "0";
        Session["_action"] = "S";
        ConfirmMsg("Do you want to save Route?");
    }
    protected void updateRoute_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Session["_action"] = "U";
        ConfirmMsg("Do you want to update Route?");
    }
    protected void addRouteStation_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (validStationRoute() == false)
        {
            return;
        }
        Session["_action"] = "station";
        ConfirmMsg("Do you want to add station in this route?");
    }
    protected void resetRoute_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        clearRouteData();
    }
    protected void cancelRoute_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        clearRouteData();
    }
    protected void lbtnSearchRoute_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        getRoutes();
        if (gvRoutes.Rows.Count == 0)
        {
            divNoRecord.Visible = false;
            divNoSearchRecord.Visible = true;
        }
    }
    protected void lbtnResetRoute_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        tbSearch.Text = "";
        getRoutes();
    }
    protected void lbtnview_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        string msg = "";
        msg = msg + "1. Here you can Add/Update route and in between stations.<br/>";
        InfoMsg(msg);
    }
    protected void gvRoutes_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvRoutes.PageIndex = e.NewPageIndex;
getRoutes();
    }
    protected void lbtndownload_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Errormsg("Coming Soon");
    }

    protected void Unnamed_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Errormsg("Coming Soon");
    }
protected void gvStations_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvStations.PageIndex = e.NewPageIndex;
        getRouteAllStations();
    }
    #endregion
    

}