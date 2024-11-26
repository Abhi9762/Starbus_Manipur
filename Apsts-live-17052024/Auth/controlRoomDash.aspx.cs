using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_controlRoomDash : BasePage
{
    sbValidation _SecurityCheck = new sbValidation();
    NpgsqlCommand MyCommand;
    sbBLL bll = new sbBLL();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    classAlarm obj = new classAlarm();
    sbSecurity _security = new sbSecurity();
    DataTable dt = new DataTable();
    private sbCommonFunc _common = new sbCommonFunc();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (_security.isSessionExist(Session["_RoleCode"]) == true)
        {
            Session["_RoleCode"] = Session["_RoleCode"];
        }
        else
        {
            Response.Redirect("../sessionTimeout.aspx");
        }
        if (IsPostBack == false)
        {
            Random randomclass = new Random();
            Session["rndNoCheck"] = randomclass.Next().ToString();
            hidtoken.Value = Session["rndNoCheck"].ToString();
            Session["Module"] = "NA";
            // Session["_moduleName"] = "Dashboard";
            Session["MasterPageHeaderText"] = "Dashboard";
            loadalerts();
            loadAlertsList(0, 10, "Alerts (0-10 minutes)", "#db6623");
            loadgrievanceCount();
            loadFleetCrewCount();

        }
        loadDatatable();
    }

    private void loadDatatable()
    {
        if (gvBusList.Rows.Count > 0)
        {
            gvBusList.UseAccessibleHeader = true;
            gvBusList.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        if (gvEmpList.Rows.Count > 0)
        {
            gvEmpList.UseAccessibleHeader = true;
            gvEmpList.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
    private void CsrfTokenValidate()
    {
        if (hidtoken.Value != Session["rndNoCheck"].ToString())
        {
            Response.Redirect("errorpage.aspx");
            return;
        }
    }
    private void Errormsg(string msg)
    {
        string popup = _popup.modalPopupSmall("W", "Information", msg, "Close");
        Response.Write(popup);
    }

    #region"Alerts"
    private void loadalerts()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = obj.getAlarmCounts_CR();
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    lbtnAlertCount_10Min.Text = dt.Rows[0]["p_0_10_min_"].ToString();
                    lbtnAlertCount_2Hours.Text = dt.Rows[0]["p_0_2_hours_"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("controlRoomDash.aspx-0001", ex.Message.ToString());

        }
    }
    private void loadAlertsList(int startminute, int endminute, string headertext, string backcolor)
    {
        try
        {
            loadRadio();
            DataTable dt = new DataTable();
            dt = obj.getAlarm_CR(startminute, endminute);
            if (dt.TableName == "Success")
            {
                gvAlarms.DataSource = dt;
                gvAlarms.DataBind();
                gvEmpList.DataSource = null;
                gvEmpList.DataBind();
                gvGrievance.DataSource = null;
                gvGrievance.DataBind();
                gvBusList.DataSource = null;
                gvBusList.DataBind();
                pnlNoRecord.Visible = true;
                if (dt.Rows.Count > 0)
                {
                    pnlNoRecord.Visible = false;
                }
                lblListHeader.Text = headertext;
                //listHeader.Attributes.Add("style", "background-color:" + backcolor);
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("controlRoomDash.aspx-0002", ex.Message.ToString());
        }
    }
    protected void gvAlarms_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        CsrfTokenValidate();
        if (e.CommandName == "ACTION")
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;

            string refNo = gvAlarms.DataKeys[row.RowIndex]["report_refno"].ToString();
            Session["_alRefNo"] = refNo;
            hfmpAckRefNo.Value = refNo;
            lblmpAckRefNo.Text = refNo;
            tbmpAckremark.Text = "";
            mpAck.Show();
        }
        if (e.CommandName == "VIEWMAP")
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            string busno = gvAlarms.DataKeys[row.RowIndex]["busno"].ToString();

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_bus_list");
            MyCommand.Parameters.AddWithValue("p_status", "F");
            MyCommand.Parameters.AddWithValue("p_busno", busno);
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                

                string depot = dt.Rows[0]["office"].ToString();
                string servicetype = dt.Rows[0]["service"].ToString();
                string gps = dt.Rows[0]["gpsyn_"].ToString();
                
                Session["_busno"] = busno;
                Session["_depot"] = depot;
                Session["_servicetype"] = servicetype;
                Session["_gps"] = gps;
                Session["BUS_OR_EMP"] = "Bus";
                eTrack.Text = "<embed src = \"dashTrackbus.aspx\" style=\"height: 80vh; width: 100%\" />";
                mpTrack.Show();
            }
            else
            {
                Errormsg("Something Went Wrong");
            }



            
        }
    }
    protected void gvAlarms_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbtnviewmap = (LinkButton)e.Row.FindControl("lbtnviewmap");
            DataRowView rowView = (DataRowView)e.Row.DataItem;
            lbtnviewmap.Visible = false;

            if (rowView["busno"].ToString() == "NA")
            {
                lbtnviewmap.Visible = true;
            }
            else
            {
                lbtnviewmap.Visible = true;
            }
        }
    }
    protected void lbtnAlertCount_10Min_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Session["Module"] = "NA";
        loadAlertsList(0, 10, "Alerts (0-10 minutes)", "#db6623");
    }
    protected void lbtnAlertCount_2Hours_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Session["Module"] = "NA";
        loadAlertsList(0, 120, "Alerts (0-2 hours)", "#3e5eb3");
    }
    #endregion

    #region"Grievance"
    private void loadgrievanceCount()
    {
        try
        {

            DataTable dt = new DataTable();
            dt = obj.getGrievanceCount_CR();
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    lbtnGrievanceCount_24Hours.Text = dt.Rows[0]["p_0_24_hour"].ToString();
                    lbtnGrievanceCount_pending.Text = dt.Rows[0]["total_pending"].ToString();
                    lbtnGrievanceCount_assigned.Text = dt.Rows[0]["totalAssigned"].ToString();
                    lbtnGrievanceCount_returned.Text = dt.Rows[0]["total_returned"].ToString();
                }
                else
                {
                    lbtnGrievanceCount_24Hours.Text = "NA";
                    lbtnGrievanceCount_pending.Text = "NA";
                    lbtnGrievanceCount_assigned.Text = "NA";
                    lbtnGrievanceCount_returned.Text = "NA";
                }
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("controlRoomDash.aspx-0003", ex.Message.ToString());

        }
    }
    private void loadGrievancesList(string status, string headerText, string backColor)
    {
        try
        {
            loadRadio();
            DataTable dt = new DataTable();
            dt = obj.getGrievances_CR(status);
            if (dt.TableName == "Success")
            {
                gvGrievance.DataSource = dt;
                gvGrievance.DataBind();
                gvEmpList.DataSource = null;
                gvEmpList.DataBind();
                gvAlarms.DataSource = null;
                gvAlarms.DataBind();
                gvBusList.DataSource = null;
                gvBusList.DataBind();
                pnlNoRecord.Visible = true;
                if (dt.Rows.Count > 0)
                {
                    pnlNoRecord.Visible = false;
                }
                lblListHeader.Text = headerText;
                //listHeader.Attributes.Add("style", "background-color:" + backcolor);
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("controlRoomDash.aspx-0004", ex.Message.ToString());

        }
    }
    protected void lbtnClosempGrievancee_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
    }
    protected void lbtnGrievanceCount_24Hours_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Session["Module"] = "NA";
        loadGrievancesList("24", "Pending Grievances (0-24 hours)", "#bdb235");
    }
    protected void lbtnGrievanceCount_pending_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Session["Module"] = "NA";
        loadGrievancesList("E", "Pending Grievances", "#5271c2");
    }
    protected void lbtnGrievanceCount_assigned_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Session["Module"] = "NA";
        loadGrievancesList("A", "Assigned Grievances", "#35a541");
    }
    protected void lbtnGrievanceCount_returned_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Session["Module"] = "NA";
        loadGrievancesList("R", "Retured Grievances", "#aa9e5c");
    }
    protected void gvGrievance_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        CsrfTokenValidate();
        if (e.CommandName == "ACTION")
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;

            string refNo = gvGrievance.DataKeys[row.RowIndex]["g_refno"].ToString();
            Session["_grRefNo"] = refNo;
            Session["_LOGINUSER"] = "C";
            eDash.Text = "<embed src = \"dashGrievance.aspx\" style=\"height: 80vh; width: 100%\" />";
            mpGrievance.Show();
        }
    }

    #endregion

    #region"Fleet & Crew"
    private void loadFleetCrewCount()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_fleetcrew_count");
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                lbtnTotalBus.Text = dt.Rows[0]["sp_total_bus"].ToString();
                lbtnOndutyBus.Text = dt.Rows[0]["sp_bus_onduty"].ToString();
                lbtnFreeBus.Text = dt.Rows[0]["sp_bus_free"].ToString();
                lbtnTotalDriver.Text = dt.Rows[0]["sp_total_driver_count"].ToString();
                lbtnOndutyDRiver.Text = dt.Rows[0]["sp_driver_count_onduty"].ToString();
                lbtnFreeDriver.Text = dt.Rows[0]["sp_free_driver_count"].ToString();
                lbtnTotalConductor.Text = dt.Rows[0]["sp_total_conductor_count"].ToString();
                lbtnOndutyConductor.Text = dt.Rows[0]["sp_conductor_count_onduty"].ToString();
                lbtnFreeConductor.Text = dt.Rows[0]["sp_free_conductor_count"].ToString();
            }
            else
            {

                lbtnTotalBus.Text = "0";
                lbtnOndutyBus.Text = "0";
                lbtnFreeBus.Text = "0";
                lbtnTotalDriver.Text = "0";
                lbtnOndutyDRiver.Text = "0";
                lbtnFreeDriver.Text = "0";
                lbtnTotalConductor.Text = "0";
                lbtnOndutyConductor.Text = "0";
                lbtnFreeConductor.Text = "0";
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("controlRoomDash.aspx-0005", ex.Message.ToString());
        }
    }
    private void loadBuses(string Status, string text)
    {
        lblListHeader.Text = text;
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_bus_list");
            MyCommand.Parameters.AddWithValue("p_status", Status);
            MyCommand.Parameters.AddWithValue("p_busno", "");
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {

                gvBusList.DataSource = dt;
                gvBusList.DataBind();
                gvEmpList.DataSource = null;
                gvEmpList.DataBind();
                gvAlarms.DataSource = null;
                gvAlarms.DataBind();
                gvGrievance.DataSource = null;
                gvGrievance.DataBind();
                pnlNoRecord.Visible = false;

                divRadio.Visible = true;
                loadDatatable();
            }
            else
            {
                divRadio.Visible = false;
                pnlNoRecord.Visible = true;
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("controlRoomDash.aspx-0006", ex.Message.ToString());
        }
    }
    private void loadCrew(string crewtype, string Status, string text)
    {
        lblListHeader.Text = text;
        try
        {
            divRadio.Visible = false;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_emp_list");
            MyCommand.Parameters.AddWithValue("p_status", Status);
            MyCommand.Parameters.AddWithValue("p_role", crewtype);
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                gvEmpList.DataSource = dt;
                gvEmpList.DataBind();
                gvBusList.DataSource = null;
                gvBusList.DataBind();
                gvAlarms.DataSource = null;
                gvAlarms.DataBind();
                gvGrievance.DataSource = null;
                gvGrievance.DataBind();
                pnlNoRecord.Visible = false;

                divRadio.Visible = true;
                loadDatatable();
            }
            else
            {
                divRadio.Visible = false;
                pnlNoRecord.Visible = true;
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("controlRoomDash.aspx-0007", ex.Message.ToString());
        }
    }
    protected void lbtnOndutyBus_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        chkFreeDuty.Checked = true;
        Session["Module"] = "Bus";
        loadRadio();
        loadBuses("D", "On Duty Buses List");
    }
    private void loadRadio()
    {
        divRadio.Visible = false;
        if (Session["Module"].ToString() == "Bus" || Session["Module"].ToString() == "Driver" || Session["Module"].ToString() == "Conductor")
        {
            divRadio.Visible = true;
        }

    }
    protected void lbtnFreeBus_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        chkFreeDuty.Checked = false; ;
        Session["Module"] = "Bus";
        loadRadio();
        loadBuses("F", "Free Buses List");
    }
    protected void lbtnOndutyDRiver_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        chkFreeDuty.Checked = true;
        Session["Module"] = "Driver";
        loadRadio();
        loadCrew("D", "D", "On Duty Driver List");
    }
    protected void lbtnFreeDriver_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        chkFreeDuty.Checked = false; ;
        Session["Module"] = "Driver";
        loadRadio();
        loadCrew("D", "F", "Free Driver List");
    }
    protected void lbtnOndutyConductor_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        chkFreeDuty.Checked = true;

        Session["Module"] = "Conductor";
        loadRadio();
        loadCrew("C", "D", "On Duty Conductor List");
    }
    protected void lbtnFreeConductor_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        chkFreeDuty.Checked = false; ;
        Session["Module"] = "Conductor";
        loadRadio();
        loadCrew("C", "F", "Free Conductor List");
    }
    protected void gvBusList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        CsrfTokenValidate();
        if (e.CommandName == "ACTION")
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            string busNo = gvBusList.DataKeys[row.RowIndex]["busno"].ToString();
            string depot = gvBusList.DataKeys[row.RowIndex]["office"].ToString();
            string servicetype = gvBusList.DataKeys[row.RowIndex]["service"].ToString();
            string gps = gvBusList.DataKeys[row.RowIndex]["gpsyn_"].ToString();
            Session["_busno"] = busNo;
            Session["_depot"] = depot;
            Session["_servicetype"] = servicetype;
            Session["_gps"] = gps;
            Session["BUS_OR_EMP"] = "Bus";
            eTrack.Text = "<embed src = \"dashTrackbus.aspx\" style=\"height: 80vh; width: 100%\" />";
            mpTrack.Show();
        }
    }
    protected void gvEmpList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        CsrfTokenValidate();
        if (e.CommandName == "ACTION")
        {
            //empcode_,name_, mobile_ ,gender ,reportingoffice_,postingoffice,designation

            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            string empcode = gvEmpList.DataKeys[row.RowIndex]["empcode_"].ToString();
            string empname = gvEmpList.DataKeys[row.RowIndex]["name_"].ToString();
            string designation = gvEmpList.DataKeys[row.RowIndex]["designation"].ToString();
            string reportingoffice = gvEmpList.DataKeys[row.RowIndex]["reportingoffice_"].ToString();
            string postingoffice = gvEmpList.DataKeys[row.RowIndex]["postingoffice"].ToString();
            string mobile = gvEmpList.DataKeys[row.RowIndex]["mobile_"].ToString();
            Session["_empcode"] = empcode;

            Session["_empname"] = empname + ", " + mobile;
            Session["_repoffice"] = reportingoffice;
            Session["_posoffice"] = postingoffice;
            Session["BUS_OR_EMP"] = "Emp";
            eTrack.Text = "<embed src = \"dashTrackbus.aspx\" style=\"height: 80vh; width: 100%\" />";
            mpTrack.Show();
        }
    }
    protected void gvBusList_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbtnTrack = (LinkButton)e.Row.FindControl("lbtnTrack");

            DataRowView rowView = (DataRowView)e.Row.DataItem;

            //if (rowView["dutystatus_"].ToString() == "On Duty")
            //{
            //    lbtnTrack.Visible = true;
            //}
            //else
            //{
            //    lbtnTrack.Visible = false;
            //}
        }
    }
    protected void gvEmpList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbtnTrack = (LinkButton)e.Row.FindControl("lbtnTrack");

            DataRowView rowView = (DataRowView)e.Row.DataItem;

            if (rowView["dutystatus_"].ToString() == "On Duty")
            {
                lbtnTrack.Visible = true;
            }
            else
            {
                lbtnTrack.Visible = false;
            }
        }
    }
    protected void cbConfirmTickets_CheckedChanged(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        divRadio.Visible = false;
        if (chkFreeDuty.Checked)
        {
            if (Session["Module"].ToString() == "Bus")
            {
                divRadio.Visible = true;
                loadBuses("D", "On Duty Buses List");
            }
            if (Session["Module"].ToString() == "Conductor")
            {
                loadCrew("C", "D", "On Duty Conductor List");
                divRadio.Visible = true;
            }
            if (Session["Module"].ToString() == "Driver")
            {
                divRadio.Visible = true;
                loadCrew("D", "D", "On Duty Driver List");
            }
        }
        else
        {
            if (Session["Module"].ToString() == "Bus")
            {
                divRadio.Visible = true;
                loadBuses("F", "Free Buses List");
            }
            if (Session["Module"].ToString() == "Conductor")
            {
                divRadio.Visible = true;
                loadCrew("C", "F", "Free Conductor List");
            }
            if (Session["Module"].ToString() == "Driver")
            {
                divRadio.Visible = true;
                loadCrew("D", "F", "Free Driver List");
            }
        }
    }
    #endregion



}