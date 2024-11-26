using Npgsql;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_tkDutySlip : System.Web.UI.Page
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    string current_date = DateTime.Now.AddDays(1).ToString("dd") + "/" + DateTime.Now.AddDays(1).ToString("MM") + "/" + DateTime.Now.AddDays(1).ToString("yyyy");

    protected void Page_Load(object sender, EventArgs e)
    {
        checkForSecurity();


        if (IsPostBack == false)
        {
            checkBusyAllocation();
            Session["_moduleName"] = "Duty Slip";
            Session["Action"] = "A";
            Session["dutyRefno"] = "";
            tbFromDate.Visible = false;
            tbDutyDate.Visible = true;
            tbDutyDate.Text = current_date;
            //divToDate.Visible = false;
            LoadRoutes();
            LoadBusServices();
            LoadBusType();
            //         if (ddlAllocationStatusType.SelectedValue.ToString() == "N")
            //{
            //	LoadAllotedServices();
            //}
            //else
            //{
            //	LoadAllDutySlips("0");
            //}
            LoadAllotedServices();
            LoadAllDutySlips("0");
        }
        loadSummaryCount();
    }
    private void checkForSecurity()
    {
        //if (Session.IsNewSession == false || Request.ServerVariables["HTTP_REFERER"].Length < 1)
        //{
        //    Response.Redirect("../Errorpage.aspx");
        //}

        if (_security.isSessionExist(Session["_UserCode"]) == true)
        {
            Session["_UserCode"] = Session["_UserCode"];
        }
        else
        {
            Response.Redirect("../sessionTimeout.aspx");
        }
        if (_security.isSessionExist(Session["_RNDIDENTIFIERTK"]) == true)
        {
            Session["_RNDIDENTIFIERTK"] = Session["_RNDIDENTIFIERTK"];
        }
        else
        {
            Response.Redirect("../sessionTimeout.aspx");
        }
        if (_security.checkvalidation() == false)
        {
            Response.Redirect("../Errorpage.aspx");
        }
        if (_security.CheckOldUserLogin(Session["_UserCode"].ToString()) == false)
        {
            Response.Redirect("../Errorpage.aspx");
        }
    }
    protected void checkBusyAllocation()
    {
        try
        {
            string officeid = Session["_LDepotCode"].ToString();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_pendingdutyslipsforfree");
            MyCommand.Parameters.AddWithValue("p_depotcode", officeid);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dt.Rows[0]["count"]) > 0)
                    {
                        lblStatus.Text = "Some duty slips are pending for free. Please free Bus-Crew before proceeding";
                        mpShowStatus.Show();
                    }
                }
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void loadSummaryCount()
    {
        try
        {
            string officeid = Session["_OfficeId"].ToString();
            string depotid = Session["_LDepotCode"].ToString();
            Int32 etmbranchid = 0;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_dutyallocationcounts");
            MyCommand.Parameters.AddWithValue("p_officecode", officeid);
            MyCommand.Parameters.AddWithValue("p_depotcode", depotid);
            MyCommand.Parameters.AddWithValue("p_etmbranchid", etmbranchid);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    lblTotalSlip.Text = dt.Rows[0]["totaldutyslips"].ToString();
                    lblPendingDutyaAllocation.Text = dt.Rows[0]["pendingdutyslips"].ToString();
                    lblTotBus.Text = dt.Rows[0]["totalBus"].ToString();
                    lbltotDrivers.Text = dt.Rows[0]["totDriver"].ToString();
                    lblOnDutyDriver.Text = dt.Rows[0]["onDutyDriver"].ToString();
                    lblBusonDuty.Text = dt.Rows[0]["OnDutyBus"].ToString();
                    lbltotConductors.Text = dt.Rows[0]["totConductor"].ToString();
                    lblOnDutyConductor.Text = dt.Rows[0]["onDutyConductor"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
    public void LoadRoutes()
    {
        try
        {
            ddlRoutes.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_routes");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlRoutes.DataSource = dt;
                    ddlRoutes.DataTextField = "routename";
                    ddlRoutes.DataValueField = "routeid";
                    ddlRoutes.DataBind();
                }
            }
            ddlRoutes.Items.Insert(0, "All");
            ddlRoutes.Items[0].Value = "0";
            ddlRoutes.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlRoutes.Items.Insert(0, "All");
            ddlRoutes.Items[0].Value = "0";
            ddlRoutes.SelectedIndex = 0;
        }
    }
    public void LoadBusServices()
    {
        try
        {
            ddlServiceType.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_servicetype_getlist");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlServiceType.DataSource = dt;
                    ddlServiceType.DataTextField = "servicetype_name_en";
                    ddlServiceType.DataValueField = "srtpid";
                    ddlServiceType.DataBind();
                }
            }
            ddlServiceType.Items.Insert(0, "All");
            ddlServiceType.Items[0].Value = "0";
            ddlServiceType.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlServiceType.Items.Insert(0, "All");
            ddlServiceType.Items[0].Value = "0";
            ddlServiceType.SelectedIndex = 0;
        }
    }
    public void LoadBusType()
    {
        try
        {
            ddlBusType.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_bustype");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlBusType.DataSource = dt;
                    ddlBusType.DataTextField = "bustype_name";
                    ddlBusType.DataValueField = "bustype_id";
                    ddlBusType.DataBind();
                }
            }
            ddlBusType.Items.Insert(0, "All");
            ddlBusType.Items[0].Value = "0";
            ddlBusType.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlBusType.Items.Insert(0, "All");
            ddlBusType.Items[0].Value = "0";
            ddlBusType.SelectedIndex = 0;
        }
    }
    #region Method
    protected void LoadAllotedServices()
    {
        try
        {
            pnlNoRecord1.Visible = true;
            gvAllotedDuties.Visible = false;

            lbtnDownload1.Visible = false;

            string officeid = Session["_LDepotCode"].ToString();
            int servicetype = Convert.ToInt16(ddlServiceType.SelectedValue.ToString());
            int route = Convert.ToInt16(ddlRoutes.SelectedValue.ToString());
            string bustype = ddlBusType.SelectedValue.ToString();
            string fromdate = tbDutyDate.Text.ToString();
            string todate = "";//tbToDate.Text.ToString();

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_provisionaldutieslist");
            MyCommand.Parameters.AddWithValue("p_action", "S");
            MyCommand.Parameters.AddWithValue("p_busdepotcode", officeid);
            MyCommand.Parameters.AddWithValue("p_servicetype", servicetype);
            MyCommand.Parameters.AddWithValue("p_route", route);
            MyCommand.Parameters.AddWithValue("p_bustype", bustype);
            MyCommand.Parameters.AddWithValue("p_fromdate", fromdate);
            MyCommand.Parameters.AddWithValue("p_todate", todate);
            MyCommand.Parameters.AddWithValue("p_dutystatus", "D");
            dt = bll.SelectAll(MyCommand);

            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    gvAllotedDuties.DataSource = dt;
                    gvAllotedDuties.DataBind();
                    pnlNoRecord1.Visible = false;
                    gvAllotedDuties.Visible = true;
                    lbtnDownload1.Visible = true;

                }
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message);
        }
    }
    protected void LoadAllDutySlips(string refno)
    {
        try
        {
            pnlNoRecord2.Visible = true;
            //gvAllotedDuties.Visible = false;
            gvDutySlips.Visible = false;
            lbtndownload2.Visible = false;
            pnlDutySlips.Visible = false;
            string officeid = Session["_LDepotCode"].ToString();
            int servicetype = Convert.ToInt16(ddlServiceType.SelectedValue.ToString());
            int route = Convert.ToInt16(ddlRoutes.SelectedValue.ToString());
            string bustype = ddlBusType.SelectedValue.ToString();
            string fromdate = tbDutyDate.Text.ToString();
            string todate = "0";//tbToDate.Text.ToString();

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_dutyslipslist");
            MyCommand.Parameters.AddWithValue("p_busdepotcode", officeid);
            MyCommand.Parameters.AddWithValue("p_dutysliprefno", refno);
            MyCommand.Parameters.AddWithValue("p_service", servicetype);
            MyCommand.Parameters.AddWithValue("p_routeid", route);
            MyCommand.Parameters.AddWithValue("p_bustype", bustype);
            MyCommand.Parameters.AddWithValue("p_fromdate", fromdate);
            MyCommand.Parameters.AddWithValue("p_todate", todate);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    pnlNoRecord2.Visible = false;
                    gvDutySlips.Visible = true;
                    gvDutySlips.DataSource = dt;
                    gvDutySlips.DataBind();
                    pnlDutySlips.Visible = true;
                    lbtndownload2.Visible = true;

                }
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message);
            gvDutySlips.Visible = false;
            pnlNoRecord2.Visible = true;
        }
    }
    public void generateDutySlip()
    {
        try
        {
            string driver, conductor, altdriver, altconductor, driverempcode, conductorempcode, altdriverempcode, altconductorempcode, depttime, busno;
            Int32 servicecode, dutydays, dutyrestdays, routeid;

            string updatedby = Session["_UserCode"].ToString();
            string IpAddress = HttpContext.Current.Request.UserHostAddress;

            int RowIndex = Convert.ToInt32(hdnRowIndex.Value);
            servicecode = Convert.ToInt32(gvAllotedDuties.DataKeys[RowIndex]["dsvc_id"].ToString());
            routeid = Convert.ToInt32(gvAllotedDuties.DataKeys[RowIndex]["rout_id"].ToString());
            dutydays = Convert.ToInt32(gvAllotedDuties.DataKeys[RowIndex]["dutydays"]);
            dutyrestdays = Convert.ToInt32(gvAllotedDuties.DataKeys[RowIndex]["dutyrestdays"]);
            depttime = gvAllotedDuties.DataKeys[RowIndex]["departuretime"].ToString();
            string routename = gvAllotedDuties.DataKeys[RowIndex]["route_name"].ToString();
            string servicename = gvAllotedDuties.DataKeys[RowIndex]["service_name_en"].ToString();
            string dutystartdate = gvAllotedDuties.DataKeys[RowIndex]["fromdate"].ToString();
            string dutyenddate = gvAllotedDuties.DataKeys[RowIndex]["todate"].ToString();
            string drvr1lcenseno = gvAllotedDuties.DataKeys[RowIndex]["drvr1lcenseno"].ToString();
            string drvr2lcenseno = gvAllotedDuties.DataKeys[RowIndex]["drvr2lcenseno"].ToString();
            string cond1lcenseno = gvAllotedDuties.DataKeys[RowIndex]["cond1lcenseno"].ToString();
            string cond2lcenseno = gvAllotedDuties.DataKeys[RowIndex]["cond2lcenseno"].ToString();
            string drvr1licensedate = gvAllotedDuties.DataKeys[RowIndex]["drvr1licensedate"].ToString();
            string drvr2licensedate = gvAllotedDuties.DataKeys[RowIndex]["drvr2licensedate"].ToString();
            string cond1licensedate = gvAllotedDuties.DataKeys[RowIndex]["cond1licensedate"].ToString();
            string cond2licensedate = gvAllotedDuties.DataKeys[RowIndex]["cond2licensedate"].ToString();
            string fitnessno = gvAllotedDuties.DataKeys[RowIndex]["fitnessno"].ToString();
            string permitno = gvAllotedDuties.DataKeys[RowIndex]["permitno"].ToString();
            string pollutioncheckno = gvAllotedDuties.DataKeys[RowIndex]["pollutioncheckno"].ToString();
            string fitnessvalidity = gvAllotedDuties.DataKeys[RowIndex]["fitnessvalidity"].ToString();
            string permitvalidity = gvAllotedDuties.DataKeys[RowIndex]["permitvalidity"].ToString();
            string pollutionvalidity = gvAllotedDuties.DataKeys[RowIndex]["pollutionvalidity"].ToString();
            string insuranceno = gvAllotedDuties.DataKeys[RowIndex]["insuranceno"].ToString();
            string insurancevalidity = gvAllotedDuties.DataKeys[RowIndex]["insurancevalidupto"].ToString();
            string dutytime = gvAllotedDuties.DataKeys[RowIndex]["dutytime"].ToString();

            busno = gvAllotedDuties.DataKeys[RowIndex]["busno"].ToString();
            driver = gvAllotedDuties.DataKeys[RowIndex]["empdriver1"].ToString();
            conductor = gvAllotedDuties.DataKeys[RowIndex]["empconductor1"].ToString();
            altdriver = gvAllotedDuties.DataKeys[RowIndex]["empdriver2"].ToString();
            altconductor = gvAllotedDuties.DataKeys[RowIndex]["empconductor2"].ToString();
            driverempcode = gvAllotedDuties.DataKeys[RowIndex]["driver1"].ToString();
            conductorempcode = gvAllotedDuties.DataKeys[RowIndex]["conductor1"].ToString();
            altdriverempcode = gvAllotedDuties.DataKeys[RowIndex]["driver2"].ToString();
            altconductorempcode = gvAllotedDuties.DataKeys[RowIndex]["conductor2"].ToString();
            if (driverempcode == "0")
                driverempcode = null;
            if (conductorempcode == "0")
                conductorempcode = null;
            if (altdriverempcode == "0")
                altdriverempcode = null;
            if (altconductorempcode == "0")
                altconductorempcode = null;
            string depotid = Session["_ldepotcode"].ToString();
            string dutyrefno = Session["dutyrefno"].ToString();

            string odometerreading = lblOdometerReading.Text.ToString();
            string targetdieselavg = lblTargetDieselAvg.Text.ToString();
            string schedulekm = lblScheduleKM.Text.ToString();
            string changestation = lblChangeStation.Text.ToString();
            string targetincome = lblTargetIncome.Text.ToString();


            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@storedprocedure", "dutyallocation.f_generatedutyslip");
            MyCommand.Parameters.AddWithValue("p_dutyrefno", dutyrefno);
            MyCommand.Parameters.AddWithValue("p_busdepotcode", depotid);
            MyCommand.Parameters.AddWithValue("p_servicecode", servicecode);
            MyCommand.Parameters.AddWithValue("p_dutystartdate", dutystartdate);
            MyCommand.Parameters.AddWithValue("p_dutyenddate", dutyenddate);
            MyCommand.Parameters.AddWithValue("p_dutytime", dutytime);
            MyCommand.Parameters.AddWithValue("p_servicename", servicename);
            MyCommand.Parameters.AddWithValue("p_routename", routename);
            MyCommand.Parameters.AddWithValue("p_routeid", routeid);
            MyCommand.Parameters.AddWithValue("p_busno", busno);
            MyCommand.Parameters.AddWithValue("p_conductor1empcode", conductorempcode);
            MyCommand.Parameters.AddWithValue("p_conductor2empcode", altconductorempcode);
            MyCommand.Parameters.AddWithValue("p_driver1empcode", driverempcode);
            MyCommand.Parameters.AddWithValue("p_driver2empcode", altdriverempcode);
            MyCommand.Parameters.AddWithValue("p_conductor1", conductor);
            MyCommand.Parameters.AddWithValue("p_conductor2", altconductor);
            MyCommand.Parameters.AddWithValue("p_driver1", driver);
            MyCommand.Parameters.AddWithValue("p_driver2", altdriver);
            MyCommand.Parameters.AddWithValue("p_drvr1lcenseno", drvr1lcenseno);
            MyCommand.Parameters.AddWithValue("p_drvr2lcenseno", drvr2lcenseno);
            MyCommand.Parameters.AddWithValue("p_cond1lcenseno", cond1lcenseno);
            MyCommand.Parameters.AddWithValue("p_cond2lcenseno", cond2lcenseno);
            MyCommand.Parameters.AddWithValue("p_drvr1licensedate", drvr1licensedate);
            MyCommand.Parameters.AddWithValue("p_drvr2licensedate", drvr2licensedate);
            MyCommand.Parameters.AddWithValue("p_cond1licensedate", cond1licensedate);
            MyCommand.Parameters.AddWithValue("p_cond2licensedate", cond2licensedate);
            MyCommand.Parameters.AddWithValue("p_insuranceno", insuranceno);
            MyCommand.Parameters.AddWithValue("p_insurancevalidity", insurancevalidity);
            MyCommand.Parameters.AddWithValue("p_fitnessno", fitnessno);
            MyCommand.Parameters.AddWithValue("p_permitno", permitno);
            MyCommand.Parameters.AddWithValue("p_pollutioncheckno", pollutioncheckno);
            MyCommand.Parameters.AddWithValue("p_fitnessvalidity", fitnessvalidity);
            MyCommand.Parameters.AddWithValue("p_permitvalidity", permitvalidity);
            MyCommand.Parameters.AddWithValue("p_pollutionvalidity", pollutionvalidity);

            MyCommand.Parameters.AddWithValue("p_odometerreading", odometerreading);
            MyCommand.Parameters.AddWithValue("p_targetincome", targetincome);
            MyCommand.Parameters.AddWithValue("p_targetdieselaverage", targetdieselavg);
            MyCommand.Parameters.AddWithValue("p_schedulekm", schedulekm);
            MyCommand.Parameters.AddWithValue("p_changestation", changestation);
            MyCommand.Parameters.AddWithValue("p_jrinchargename", updatedby);
            MyCommand.Parameters.AddWithValue("p_updatedby", updatedby);
            MyCommand.Parameters.AddWithValue("p_ipaddress", IpAddress);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                string result = dt.Rows[0]["p_refno"].ToString();
                if (result == "EXCEPTION")
                {
                    Errormsg("Exception occurred while Saving. ");
                    return;
                }
                else
                {
                    Session["dutySlipRefno"] = dt.Rows[0]["p_refno"].ToString();
                    LoadAllotedServices();
                    LoadAllDutySlips("0");
                    hdnRowIndex.Value = "0";
                    //Successmsg("Duty Slip Generated successfully");
                    //openSubDetailsWindow("DutySlip.aspx");
                    loadSummaryCount();
                    pnlAddDutySlips.Visible = true;
                    pnlSummary.Visible = true;

                    pnlUpdateDutyList.Visible = false;
                    eSlip.Text = "<embed src = \"DutySlip.aspx\" style=\"height: 80vh; width: 100%\" />";
                    mpShowDuty.Show();
                }
            }
            else
            {
                Errormsg("Error occurred while Saving. ");
                return;
            }
        }
        catch (Exception ex)
        {
            Errormsg("Error occurred while Saving. " + ex.Message);
        }
    }
    public void LoadServiceOtherDetails()
    {
        try
        {
            int serviceID = Convert.ToInt32(Session["_DSVC_ID"].ToString());
            string dutyrefno = Session["dutyRefno"].ToString();
            MyCommand = new NpgsqlCommand();

            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_serviceodrdtlsbydsvcid");
            MyCommand.Parameters.AddWithValue("p_dsvcid", serviceID);
            MyCommand.Parameters.AddWithValue("p_dutyrefno", dutyrefno);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    gvServiceTrips.DataSource = dt;
                    gvServiceTrips.DataBind();
                    lblScheduleKM.Text = dt.Rows[0]["TOTAL_KM"].ToString();
                    lblOdometerReading.Text = dt.Rows[0]["LASTODOMETERREADING"].ToString();
                    lblTargetDieselAvg.Text = dt.Rows[0]["TARGETDIESELAVG"].ToString();
                    lblTargetIncome.Text = dt.Rows[0]["TARGETINCOME"].ToString();
                    lblChangeStation.Text = dt.Rows[0]["ChangeStationName"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message);
        }
    }
    public void cancelDuty()
    {
        try
        {
            string updatedby = Session["_UserCode"].ToString();
            string IpAddress = HttpContext.Current.Request.UserHostAddress;
            string dutyrefno = Session["dutyrefno"].ToString();

            string procName = "";
            if (Session["_CANCELTYPE"].ToString() == "DUTYSLIP")
            {
                procName = "dutyallocation.f_canceldutyslip";
            }
            else if (Session["_CANCELTYPE"].ToString() == "DUTYALLOTMENT")
            {
                procName = "dutyallocation.f_canceldutyallotment";
            }
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", procName);
            MyCommand.Parameters.AddWithValue("p_dutyrefno", dutyrefno);
            MyCommand.Parameters.AddWithValue("p_updatedby", updatedby);
            MyCommand.Parameters.AddWithValue("p_ipaddress", IpAddress);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {

                if (Session["_CANCELTYPE"].ToString() == "DUTYALLOTMENT")
                {
                    if (dt.Rows[0]["p_successyn"].ToString() == "EXCEPTION")
                    {
                        Errormsg("Something Went Wrong");
                    }
                    else
                    {
                        LoadAllDutySlips("0");
                        LoadAllotedServices();
                        loadSummaryCount();
                        Successmsg("Duty cancelled successfully");
                    }
                }
                if (Session["_CANCELTYPE"].ToString() == "DUTYSLIP")
                {
                    if (dt.Rows[0]["p_result"].ToString() == "EXCEPTION")
                    {
                        Errormsg("Something Went Wrong");
                    }
                    else
                    {
                        LoadAllDutySlips("0");
                        LoadAllotedServices();
                        loadSummaryCount();
                        Successmsg("Duty Slip cancelled successfully");
                    }
                }


            }
            else
            {
                Errormsg("Error occurred while Updation. " + dt.TableName);
                return;
            }
        }
        catch (Exception ex)
        {
            Errormsg("Error occurred while Updation. " + ex.Message);
        }
    }
    private void ExportGridToExcel()
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=GeneratedDutySlip.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            // To Export all pages
            gvDutySlips.AllowPaging = false;
            this.LoadAllDutySlips("0");
            gvDutySlips.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in gvDutySlips.HeaderRow.Cells)
                cell.BackColor = gvDutySlips.HeaderStyle.BackColor;
            foreach (GridViewRow row in gvDutySlips.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                        cell.BackColor = gvDutySlips.AlternatingRowStyle.BackColor;
                    else
                        cell.BackColor = gvDutySlips.RowStyle.BackColor;
                    cell.CssClass = "Textmode";
                }
            }

            gvDutySlips.RenderControl(hw);
            // style to format numbers to string
            string style = "<style> .Textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }
    private void ExportGridAllotedDutyToExcel()
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=AllotedDuties.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            // To Export all pages
            gvAllotedDuties.AllowPaging = false;
            this.LoadAllotedServices();
            gvAllotedDuties.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in gvAllotedDuties.HeaderRow.Cells)
                cell.BackColor = gvAllotedDuties.HeaderStyle.BackColor;
            foreach (GridViewRow row in gvAllotedDuties.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                        cell.BackColor = gvAllotedDuties.AlternatingRowStyle.BackColor;
                    else
                        cell.BackColor = gvAllotedDuties.RowStyle.BackColor;
                    cell.CssClass = "Textmode";
                }
            }

            gvAllotedDuties.RenderControl(hw);
            // style to format numbers to string
            string style = "<style> .Textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }
    private void Errormsg(string msg)
    {
        string popup = _popup.modalPopupSmall("W", "Please Note", msg, "Close");
        Response.Write(popup);
    }
    private void Successmsg(string msg)
    {
        string popup = _popup.modalPopupSmall("S", "Success", msg, "OK");
        Response.Write(popup);
    }
    private void ConfirmMsg(string msg)
    {
        lblConfirmation.Text = msg;
        mpConfirmation.Show();
    }
    private void InfoMsg(string msg)
    {
        string popup = _popup.modalPopupLarge("I", "Information", msg, "Close");
        Response.Write(popup);
    }
    public void openSubDetailsWindow(string MModuleName)
    {
        try
        {
            string murl;
            murl = MModuleName + "?rt=" + DateTime.Now.ToString();

            if (Request.Browser.Type.Substring(0, 2).ToUpper() == "IE")
            {
                Response.Write("<SCRIPT anguage='javascript'>window.showModalDialog('" + murl + "','name','dialogWidth:830px;dialogHeight:900px');</script>");
            }
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

    #region Events
    //protected void ddlAllocationStatusType_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //	if (ddlAllocationStatusType.SelectedValue == "N")
    //	{
    //		hdnGrdHeading.InnerHtml = "List of provisional duty allotment pending for duty slip";
    //		Session["Action"] = "A";
    //		//divToDate.Visible = false;
    //		tbFromDate.Visible = false;
    //		tbDutyDate.Visible = true;
    //		//lblDate.Text = "Duty Date";
    //		tbDutyDate.Text = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy");
    //		pnlAllotDuty.Visible = true;
    //		pnlDutySlips.Visible = false;
    //		LoadAllotedServices();
    //	}
    //	else if (ddlAllocationStatusType.SelectedValue == "P")
    //	{
    //		hdnGrdHeading.InnerHtml = "List of generated duty slips";
    //		Session["Action"] = "U";
    //		//lblDate.Text = "From Date";
    //		//divToDate.Visible = true;
    //		tbFromDate.Visible = true;
    //		tbDutyDate.Visible = false;
    //		tbFromDate.Text = DateTime.Now.AddDays(-7).ToString("dd/MM/yyyy");
    //		//tbToDate.Text = DateTime.Now.AddDays(+1).ToString("dd/MM/yyyy");
    //		pnlAllotDuty.Visible = false;
    //		pnlDutySlips.Visible = true;
    //		LoadAllDutySlips("0");
    //	}
    //}
    protected void lbtnsearch_Click(object sender, EventArgs e)
    {
        //if (ddlAllocationStatusType.SelectedValue == "N")
        //{
        //	pnlAllotDuty.Visible = true;
        //	pnlDutySlips.Visible = false;

        //	LoadAllotedServices();
        //}
        //else if (ddlAllocationStatusType.SelectedValue == "P")
        //{
        //	pnlAllotDuty.Visible = false;
        //	pnlDutySlips.Visible = true;
        //	LoadAllDutySlips("0");
        //}
        LoadAllotedServices();
        LoadAllDutySlips("0");
    }
    protected void lbtnResetFilter_Click(object sender, EventArgs e)
    {
        pnlAllotDuty.Visible = true;
        pnlDutySlips.Visible = false;
        //ddlAllocationStatusType.SelectedValue = "N";
        tbDutyDate.Text = "";
        //tbToDate.Text = "";
        ddlBusType.SelectedValue = "0";
        ddlServiceType.SelectedValue = "0";
        ddlRoutes.SelectedValue = "0";
        //if (ddlAllocationStatusType.SelectedValue == "N")
        //{
        //	LoadAllotedServices();
        //}
        //else
        //{
        //	LoadAllDutySlips("0");
        //}
        LoadAllotedServices();
        LoadAllDutySlips("0");
    }
    protected void lbtnDownloadExcel_Click(object sender, EventArgs e)
    {
        ExportGridAllotedDutyToExcel();
    }
    protected void lbtndownload2_Click(object sender, EventArgs e)
    {
        ExportGridToExcel();
    }
    protected void lbtnview_Click(object sender, EventArgs e)
    {
        string msg = "";
        msg = msg + "1. Duty slip against provisional duty allotment of Bus/Driver/Conductor will be done from here.<br/>";
        msg = msg + "2. If there is any change in availability of Bus/Crew then you can generate new provisional allotment and generate duty slip for it.<br/>";
        msg = msg + "3. Duty slip cannot be changed. If after generation of Bus/Crew there is need of any change in Bus/Crew, then cancel it from generated duty slips list and create a new duty allotment for that service and then generate duty slip.<br/>";
        msg = msg + "4. Only those duty slips can be cancelled whose waybills haven't been generated and the service time is over.<br/>";
        msg = msg + "5. After cancellation of duty slip, Bus and Crew will be marked as free.<br/>";
        InfoMsg(msg);
    }
    protected void gvAllotedDuties_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "generateDutySlip")
        {
            //Errormsg("Something Went Wrong");
            //return;
            Session["Action"] = "A";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            hdnRowIndex.Value = RowIndex.ToString();
            if (gvAllotedDuties.DataKeys[index].Values["dutyrefno"].ToString() == "")
            {

            }
            else
            {

            }
            Session["dutyRefno"] = gvAllotedDuties.DataKeys[index].Values["dutyrefno"].ToString();
            lblRefNo.Text = gvAllotedDuties.DataKeys[index].Values["dutyrefno"].ToString();
            Session["_DSVC_ID"] = gvAllotedDuties.DataKeys[index].Values["dsvc_id"];
            lblServiceName.Text = gvAllotedDuties.DataKeys[index].Values["service_name_en"].ToString();
            lblDeptTime.Text = gvAllotedDuties.DataKeys[index].Values["departuretime"].ToString();
            lblDSConductor.Text = gvAllotedDuties.DataKeys[index].Values["empconductor1"].ToString();
            lblDSDriver.Text = gvAllotedDuties.DataKeys[index].Values["driver1"].ToString();
            lblDSDriver.Text = gvAllotedDuties.DataKeys[index].Values["empdriver1"].ToString();
            if (gvAllotedDuties.DataKeys[index].Values["driver2"].ToString() != "")
            {
                lblDSAltDriver.Text = gvAllotedDuties.DataKeys[index].Values["driver2"].ToString();
            }
            if (gvAllotedDuties.DataKeys[index].Values["empdriver2"].ToString() != "")
            {
                lblDSAltDriver.Text = gvAllotedDuties.DataKeys[index].Values["empdriver2"].ToString();
            }
            if (gvAllotedDuties.DataKeys[index].Values["conductor2"].ToString() != "")
            {
                lblDSAltConductor.Text = gvAllotedDuties.DataKeys[index].Values["conductor2"].ToString();
            }
            if (gvAllotedDuties.DataKeys[index].Values["empconductor2"].ToString() != "")
            {
                lblDSAltConductor.Text = gvAllotedDuties.DataKeys[index].Values["empconductor2"].ToString();
            }

            LoadServiceOtherDetails();
            pnlSummary.Visible = false;
            tblGenerateDuty.Visible = true;
            pnlUpdateDutyList.Visible = true;
            pnlAddDutySlips.Visible = false;
            lbtnGenerateDutySlip.Visible = true;
            gvServiceTrips.Visible = true;
        }
        else if (e.CommandName == "cancelDutySlip")
        {
            Session["Action"] = "C";
            Session["_CANCELTYPE"] = "DUTYALLOTMENT";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            hdnRowIndex.Value = RowIndex.ToString();
            Session["dutyRefno"] = gvAllotedDuties.DataKeys[index].Values["dutyrefno"];
            ConfirmMsg("Do you want to cancel duty?");
            // Errormsg("Something Went Wrong");
        }
    }
    protected void gvDutySlips_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "viewDuty")
        {
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            Session["dutySlipRefno"] = gvDutySlips.DataKeys[RowIndex].Values["DUTYREFNO"];
            //openSubDetailsWindow("DutySlip.aspx");
            eSlip.Text = "<embed src = \"DutySlip.aspx\" style=\"height: 80vh; width: 100%\" />";
            mpShowDuty.Show();
        }
        else if (e.CommandName == "cancelDuty")
        {
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            Session["Action"] = "C";
            Session["_CANCELTYPE"] = "DUTYSLIP";
            hdnRowIndex.Value = RowIndex.ToString();
            Session["dutyRefno"] = gvDutySlips.DataKeys[index].Values["DUTYREFNO"];
            ConfirmMsg("Do you want to cancel duty?");
        }
    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        if (Session["Action"].ToString() == "A")
        {
            generateDutySlip();
        }
        else if (Session["Action"].ToString() == "C")
        {
            cancelDuty();
        }
    }
    protected void lbtnCancel_Click(object sender, EventArgs e)
    {
        pnlUpdateDutyList.Visible = false;
        pnlAddDutySlips.Visible = true;
        pnlSummary.Visible = true;

    }
    protected void gvAllotedDuties_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvAllotedDuties.PageIndex = e.NewPageIndex;
        LoadAllotedServices();
    }
    protected void gvdutySlips_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDutySlips.PageIndex = e.NewPageIndex;
        LoadAllDutySlips("0");
    }
    protected void lbtnGenerateDutySlip_Click(object sender, EventArgs e)
    {
        Session["Action"] = "A";
        // Errormsg("Something Went Wrong");
        ConfirmMsg("Do you want to Generate Duty Slip?");
    }
    protected void lbtnFreeBusCrew_Click(object sender, EventArgs e)
    {
        try
        {
            string officeid = Session["_LDepotCode"].ToString();
            string UpdatedBy = Session["_UserCode"].ToString();
            string IpAddress = HttpContext.Current.Request.UserHostAddress;

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_freedutyslipsandcrew");
            MyCommand.Parameters.AddWithValue("p_depotcode", officeid);
            MyCommand.Parameters.AddWithValue("p_updatedby", UpdatedBy);
            MyCommand.Parameters.AddWithValue("p_ipaddress", IpAddress);
            dt = bll.SelectAll(MyCommand);


            if (dt.TableName == "Success")
            {
                string result = dt.Rows[0]["p_result"].ToString();
                if (result == "EXCEPTION")
                {
                    Errormsg("Error occurred while updation.");
                }
                else
                {
                    Successmsg("Bus/Crew status has been successfully marked as free");
                    mpShowStatus.Hide();
                    loadSummaryCount();
                }
            }
            else
            {
                Errormsg("Error occurred while updation. " + dt.TableName);
                return;
            }
        }
        catch (Exception ex)
        {
            Errormsg("Error occurred while Saving. " + ex.Message);
        }
    }
    public void deleteDutyAllotment()
    {
        try
        {
            string officeid = Session["_LDepotCode"].ToString();
            string dutyrefno = Session["dutyRefno"].ToString();
            string UpdatedBy = Session["_UserCode"].ToString();
            string IpAddress = HttpContext.Current.Request.UserHostAddress;

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.t_dutyallocatetransactions");
            MyCommand.Parameters.AddWithValue("p_dutyrefno", dutyrefno);
            MyCommand.Parameters.AddWithValue("P_UPDATEDBY", UpdatedBy);
            MyCommand.Parameters.AddWithValue("P_IPAddress", IpAddress);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                LoadAllotedServices();
                Successmsg("Duty Allocation cancelled successfully");
            }
            else
            {
                Errormsg("Error occurred while cancellation. " + dt.TableName);
                //mpUpdateDuty.Show();
                return;
            }
        }
        catch (Exception ex)
        {
            Errormsg("Error occurred while cancellation. " + ex.Message);
            //mpUpdateDuty.Show();
        }
    }


    #endregion

    protected void lbtnarchive_Click(object sender, EventArgs e)
    {
        eDash.Text = "<embed src = \"dashDutySlip.aspx\" style=\"height: 80vh; width: 100%\" />";
        mpDuty.Show();
    }
}