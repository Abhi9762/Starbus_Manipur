using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Auth_tkDutyAllocation : BasePage
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    string current_date = DateTime.Now.AddDays(1).ToString("dd") + "/" + DateTime.Now.AddDays(1).ToString("MM") + "/" + DateTime.Now.AddDays(1).ToString("yyyy");
    string current_date_7 = DateTime.Now.AddDays(-7).ToString("dd") + "/" + DateTime.Now.AddDays(-7).ToString("MM") + "/" + DateTime.Now.AddDays(-7).ToString("yyyy");


    protected void Page_Load(object sender, EventArgs e)
    {
        checkForSecurity();
        if (IsPostBack == false)
        {
            // lblSummary.Text = "Summary as on Date " + DateTime.Now.ToString("dd/MM/yyyy hh:ss tt");
            Session["_moduleName"] = "Duty Allocation";
            Session["Action"] = "A";
            //tbFromDate.Visible = false;
            tbDutyDate.Visible = true;
            tbDutyDate.Text = current_date;
            //divToDate.Visible = false;
            loadDepotdetails(Session["_UserCode"].ToString());
            // checkAttendanceStatus();
            checkBusyAllocation();

            LoadBusServices();
            LoadRoutes();
            LoadBusType();
            Session["IsCrewAdded"] = "Not Added";
            LoadAllServices();
            LoadAllotedServices();



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

    #region Methods
    private void loadDepotdetails(string usercode)
    {
        try
        {
            Int32 role = Convert.ToInt32(Session["_RoleCode"].ToString()); ;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_loggeduserdetails");
            MyCommand.Parameters.AddWithValue("p_userid", usercode);
            MyCommand.Parameters.AddWithValue("p_role", role);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {

                    //lblcntrname.Text = dt.Rows[0]["cntr_name"].ToString().ToUpper() + " (" + dt.Rows[0]["station_name"].ToString() + ")";

                    Session["_LDepotCode"] = dt.Rows[0]["reporting_office"].ToString(); //"1010100000"; 

                }
                else
                {
                    _common.ErrorLog("TimeKeeperMaster-M1", "No record Found!!");
                    Response.Redirect("../Errorpage.aspx");
                }
            }
            else
            {
                _common.ErrorLog("TimeKeeperMaster-M2", dt.TableName.ToString());
                Response.Redirect("../Errorpage.aspx");
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("TimeKeeperMaster-M1", ex.Message.ToString());
            Response.Redirect("../Errorpage.aspx");
        }
    }
    protected void checkAttendanceStatus()
    {
        try
        {
            string officeid = Session["_OfficeId"].ToString();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_empattendencecount");
            MyCommand.Parameters.AddWithValue("p_depotcode", officeid);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dt.Rows[0]["notMarkedEmployee"]) == 0)
                    {
                        lblAttendanceStatus.Text = "Mark attendance of all employees before proceeding for duty allocation";
                        lbtnAttendance.Visible = true;
                        lbtnFreeBusCrew.Visible = false;
                        mpShowStatus.Show();
                    }
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void checkBusyAllocation()
    {
        try
        {
            string officeid = Session["_LDepotCode"].ToString();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_pendingalllocationsforfree");
            MyCommand.Parameters.AddWithValue("p_depotcode", officeid);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dt.Rows[0]["count"]) > 0)
                    {
                        lblAttendanceStatus.Text = "Some allocation are pending for free. Please free Bus-Crew before proceeding";
                        lbtnFreeBusCrew.Visible = true;
                        lbtnAttendance.Visible = false;
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
            string depotcode = Session["_LDepotCode"].ToString();
            Int32 etmbranchid = 0;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_dutyallocationcounts");
            MyCommand.Parameters.AddWithValue("p_officecode", officeid);
            MyCommand.Parameters.AddWithValue("p_depotcode", depotcode);
            MyCommand.Parameters.AddWithValue("p_etmbranchid", etmbranchid);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    lblTotalAllocations.Text = dt.Rows[0]["totalAllocations"].ToString();
                    lblTotBus.Text = dt.Rows[0]["totalBus"].ToString();
                    lbltotDrivers.Text = dt.Rows[0]["totDriver"].ToString();
                    lblOnDutyDriver.Text = dt.Rows[0]["onDutyDriver"].ToString();
                    lblBusonDuty.Text = dt.Rows[0]["OnDutyBus"].ToString();
                    lbltotConductors.Text = dt.Rows[0]["totConductor"].ToString();
                    lblOnDutyConductor.Text = dt.Rows[0]["onDutyConductor"].ToString();
                    lblTotService.Text = dt.Rows[0]["spTotalService"].ToString();
                    lblPendingService.Text = dt.Rows[0]["spPendingTargetDiesel"].ToString();
                    lblProvisionalAllocations.Text = dt.Rows[0]["pendingallocations"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
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
    protected void LoadAllServices()
    {
        try
        {
            pnlNoRecord1.Visible = true;
            gvServicesList.Visible = false;
            lbtnDownloadExcel.Visible = false;
            string depotcode = Session["_LDepotCode"].ToString();
            int servicetype = Convert.ToInt16(ddlServiceType.SelectedValue.ToString());
            int route = Convert.ToInt16(ddlRoutes.SelectedValue.ToString());
            string bustype = ddlBusType.SelectedValue.ToString();
            string dutydate = tbDutyDate.Text.ToString();

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();

            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_serviceslistfordutyallocation");
            MyCommand.Parameters.AddWithValue("p_busdepotcode", depotcode);
            MyCommand.Parameters.AddWithValue("p_servicetype", servicetype);
            MyCommand.Parameters.AddWithValue("p_route", route);
            MyCommand.Parameters.AddWithValue("p_bustype", bustype);
            MyCommand.Parameters.AddWithValue("p_dutydate", dutydate);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    lbtnDownloadExcel.Visible = true;
                    pnlNoRecord1.Visible = false;
                    gvServicesList.Visible = true;
                    gvServicesList.DataSource = dt;
                    gvServicesList.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message);
            gvServicesList.Visible = false;
            pnlNoRecord1.Visible = true;
        }
    }
    public void LoadBuses(DropDownList ddl)
    {
        try
        {
            if (Session["Action"].ToString() == "U")
            {
                //Session["_DSVC_ID"] = "0";
            }
            string depotcode = Session["_LDepotCode"].ToString();

            int dsvcid = Convert.ToInt16(Session["_DSVC_ID"].ToString());
            ddl.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_servicesmappedbuslist");
            MyCommand.Parameters.AddWithValue("p_busdepotcode", depotcode);
            MyCommand.Parameters.AddWithValue("p_serviceid", dsvcid);
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                ddl.DataSource = dt;
                ddl.DataTextField = "busregistrationno";
                ddl.DataValueField = "busregistrationno";
                ddl.DataBind();
            }
            else if (Session["Action"].ToString() == "U")
            {
                ddl.Items.Insert(0, "Select");
                ddl.Items[0].Value = "0";
                ddl.SelectedIndex = 0;
            }
            else
            {
                ddl.Items.Insert(0, "N/A");
                ddl.Items[0].Value = "";
                ddl.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            if (Session["Action"].ToString() == "U")
            {
                ddl.Items.Insert(0, "Select");
                ddl.Items[0].Value = "0";
                ddl.SelectedIndex = 0;
            }
            else
            {
                ddl.Items.Insert(0, "N/A");
                ddl.Items[0].Value = "";
                ddl.SelectedIndex = 0;
            }
        }
    }
    public void LoadMappedBuses(Label lblBusNo, HiddenField hdnBusNo)
    {
        try
        {
            if (Session["Action"].ToString() == "U")
            {
                Session["_DSVC_ID"] = "0";
            }
            string officeid = Session["_LDepotCode"].ToString();
            int dsvcid = Convert.ToInt16(Session["_DSVC_ID"].ToString());
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_servicesmappedbuslist");
            MyCommand.Parameters.AddWithValue("p_busdepotcode", officeid);
            MyCommand.Parameters.AddWithValue("p_serviceid", dsvcid);
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                lblBusNo.Text = dt.Rows[0]["busregistrationno"].ToString();
            }
            else
            {
                lblBusNo.Text = "Not Available";
            }
        }
        catch (Exception ex)
        {
            lblBusNo.Text = "Not Available";
        }
    }
    public void loadAllMappedCrew(Label emp, HiddenField empcode, string empcode2, HiddenField hdnBusNo, string emptype)
    {
        try
        {
            // ddl.Enabled = False
            string busNo = hdnBusNo.Value.ToString();
            if (Session["Action"].ToString() == "U")
            {
                busNo = "0";
            }
            string officeid = Session["_LDepotCode"].ToString();
            string dutyDate = tbDutyDate.Text.ToString();
            string bus = hdnBusNo.Value.ToString();

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_mappedcrewlistforda");
            MyCommand.Parameters.AddWithValue("p_busdepotcode", officeid);
            MyCommand.Parameters.AddWithValue("p_busno", bus);
            MyCommand.Parameters.AddWithValue("p_dutydate", dutyDate);
            MyCommand.Parameters.AddWithValue("p_emptype", emptype);
            MyCommand.Parameters.AddWithValue("p_empcode", empcode2);
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                //lblBusNo.Text = dt.Rows[0]["empname"].ToString();
                emp.Text = dt.Rows[0]["empname"].ToString();
                empcode.Value = dt.Rows[0]["empcode"].ToString();
            }
            else
            {
                emp.Text = "Not Available";
            }
        }
        catch (Exception ex)
        {
            emp.Text = "Not Available";
        }
    }
    public void loadMappedCrew(DropDownList ddl, string empcode, string ddlbus, string emptype)
    {
        try
        {
            // ddl.Enabled = False
            string busNo = ddlbus;
            if (Session["Action"].ToString() == "U")
            {
                busNo = "0";
            }
            string officeid = Session["_OfficeId"].ToString();
            string dutyDate = tbDutyDate.Text.ToString();
            string bus = ddlbus;
            string crew = ddl.SelectedValue;
            ddl.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_mappedcrewlistforda");
            MyCommand.Parameters.AddWithValue("p_busdepotcode", officeid);
            MyCommand.Parameters.AddWithValue("p_busno", bus);
            MyCommand.Parameters.AddWithValue("p_dutydate", dutyDate);
            MyCommand.Parameters.AddWithValue("p_emptype", emptype);
            MyCommand.Parameters.AddWithValue("p_empcode", empcode);
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {

                ddl.DataSource = dt;
                ddl.DataTextField = "empname";
                ddl.DataValueField = "empcode";
                ddl.DataBind();
                ddl.SelectedValue = crew;

            }
            else if (Session["Action"].ToString() == "U")
            {
                ddl.Items.Insert(0, "Select");
                ddl.Items[0].Value = "0";
                ddl.SelectedIndex = 0;
            }
            else
            {
                ddl.Items.Insert(0, "N/A");
                ddl.Items[0].Value = "0";
                ddl.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            if (Session["Action"].ToString() == "U")
            {
                ddl.Items.Insert(0, "Select");
                ddl.Items[0].Value = "0";
                ddl.SelectedIndex = 0;
            }
            else
            {
                ddl.Items.Insert(0, "N/A");
                ddl.Items[0].Value = "0";
                ddl.SelectedIndex = 0;
            }
        }
    }
    public void saveValues()
    {
        try
        {

            string officeid = Session["_LDepotCode"].ToString();
            string UpdatedBy = Session["_UserCode"].ToString();
            string IpAddress = HttpContext.Current.Request.UserHostAddress;
            int RowIndex = Convert.ToInt32(Session["RowIndex"]);

            int serviceCode = Convert.ToInt32(gvServicesList.DataKeys[RowIndex]["dsvc_id"].ToString());
            int dutyDays = Convert.ToInt32(gvServicesList.DataKeys[RowIndex]["dutyDays"]);
            int dutyRestDays = Convert.ToInt32(gvServicesList.DataKeys[RowIndex]["dutyRestDays"]);
            string deptTime = gvServicesList.DataKeys[RowIndex]["deptTime"].ToString();
            string dutyStartDate = tbDutyDate.Text.ToString();

            Label lblBus = (Label)gvServicesList.Rows[RowIndex].FindControl("lblBus");
            Label lblDriver = (Label)gvServicesList.Rows[RowIndex].FindControl("lblDriver");
            Label lblConductor = (Label)gvServicesList.Rows[RowIndex].FindControl("lblConductor");
            Label lblDriver2 = (Label)gvServicesList.Rows[RowIndex].FindControl("lblDriver2");
            Label lblConductor2 = (Label)gvServicesList.Rows[RowIndex].FindControl("lblConductor2");
            string driver, conductor, altDriver, altConductor, busNo;
            busNo = lblBus.Text;
            driver = lblDriver.Text;
            if (driver == "0")
                driver = "";
            altDriver = lblDriver2.Text;
            if (altDriver == "0")
                altDriver = "";
            conductor = lblConductor.Text;
            if (conductor == "0")
                conductor = "";
            altConductor = lblConductor2.Text;
            if (altConductor == "0")
                altConductor = "";

            MyCommand = new NpgsqlCommand();

            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_generate_dutyallotment");
            MyCommand.Parameters.AddWithValue("p_depotcode", officeid);
            MyCommand.Parameters.AddWithValue("p_servicecode", serviceCode);
            MyCommand.Parameters.AddWithValue("p_dutystartdate", dutyStartDate);
            MyCommand.Parameters.AddWithValue("p_departuretime", deptTime);
            MyCommand.Parameters.AddWithValue("p_busno", busNo);
            MyCommand.Parameters.AddWithValue("p_conductor1", conductor);
            MyCommand.Parameters.AddWithValue("p_conductor2", altConductor);
            MyCommand.Parameters.AddWithValue("p_driver1", driver);
            MyCommand.Parameters.AddWithValue("p_driver2", altDriver);
            MyCommand.Parameters.AddWithValue("p_dutydays", dutyDays);
            MyCommand.Parameters.AddWithValue("p_dutyrestdays", dutyRestDays);
            MyCommand.Parameters.AddWithValue("p_updatedby", UpdatedBy);
            MyCommand.Parameters.AddWithValue("p_ipaddress", IpAddress);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success") //Wecd@123#$$
            {
                if (dt.Rows[0]["p_refno"].ToString() == "EXCEPTION")
                {
                    Errormsg("Error occurred while Saving.");
                    return;
                }
                else
                {
                    Session["dutyRefno"] = dt.Rows[0]["p_refno"].ToString();
                    Session["IsCrewAdded"] = "Not Added";
                    LoadAllServices();
                    LoadAllotedServices();
                    loadSummaryCount();

                    // Successmsg("Duty Allocated successfully");
                    // openSubDetailsWindow("DutyAllotmentSlip.aspx");
                    eSlip.Text = "<embed src = \"DutyAllotmentSlip.aspx\" style=\"height: 80vh; width: 100%\" />";
                    mpShowDuty.Show();
                }
            }
            else
            {
                Errormsg("Error occurred while Saving. " + dt.TableName);
                return;
            }
        }
        catch (Exception ex)
        {
            Errormsg("Error occurred while Saving. " + ex.Message);
        }
    }
    protected void LoadAllotedServices()
    {
        try
        {
            lbtndownloadexcelalloted.Visible = false;
            pnlNoRecord2.Visible = true;
            gvAllotedDuties.Visible = false;
            string officeid = Session["_LDepotCode"].ToString();
            int servicetype = Convert.ToInt16(ddlServiceType.SelectedValue.ToString());
            int route = Convert.ToInt16(ddlRoutes.SelectedValue.ToString());
            string bustype = ddlBusType.SelectedValue.ToString();
            string fromdate = tbDutyDate.Text.ToString();
            //string todate = tbToDate.Text.ToString();

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_provisionaldutieslist");
            MyCommand.Parameters.AddWithValue("p_action", "N");
            MyCommand.Parameters.AddWithValue("p_busdepotcode", officeid);
            MyCommand.Parameters.AddWithValue("p_servicetype", servicetype);
            MyCommand.Parameters.AddWithValue("p_route", route);
            MyCommand.Parameters.AddWithValue("p_bustype", bustype);
            MyCommand.Parameters.AddWithValue("p_fromdate", fromdate);
            MyCommand.Parameters.AddWithValue("p_todate", '0');
            MyCommand.Parameters.AddWithValue("p_dutystatus", "0");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    lbtndownloadexcelalloted.Visible = true;
                    gvAllotedDuties.DataSource = dt;
                    gvAllotedDuties.DataBind();
                    pnlNoRecord2.Visible = false;
                    gvAllotedDuties.Visible = true;
                }
            }
            else
            {
                Errormsg(dt.TableName);
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
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "NewWindow", fullURL, true); // 
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
    private void ExportGridToExcel()
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=ServiceList.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            // To Export all pages
            gvServicesList.AllowPaging = false;
            this.LoadAllServices();
            gvServicesList.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in gvServicesList.HeaderRow.Cells)
                cell.BackColor = gvServicesList.HeaderStyle.BackColor;
            foreach (GridViewRow row in gvServicesList.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                        cell.BackColor = gvServicesList.AlternatingRowStyle.BackColor;
                    else
                        cell.BackColor = gvServicesList.RowStyle.BackColor;
                    cell.CssClass = "textmode";
                }
            }

            gvServicesList.RenderControl(hw);
            // style to format numbers to string
            string style = "<style> .textmode { } </style>";
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
        Response.AddHeader("content-disposition", "attachment;filename=AllotedDutyList.xls");
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
                    cell.CssClass = "textmode";
                }
            }

            gvAllotedDuties.RenderControl(hw);
            // style to format numbers to string
            string style = "<style> .textmode { } </style>";
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
        lblConfirmErrorMsg.Text = msg;
        mpConfirmation.Show();
    }
    private void InfoMsg(string msg)
    {
        string popup = _popup.modalPopupLarge("I", "Information", msg, "Close");
        Response.Write(popup);
    }
    public bool IsValidServiceCrew(string dsvcid, string servicename, string Driver1empcode, string Driver2empcode, string Conductor1empcode, string Conductor2empcode, string Driver1, string Driver2, string Conductor1, string Conductor2)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_check_validdaservicecrew");
            MyCommand.Parameters.AddWithValue("p_dsvcid", dsvcid);
            MyCommand.Parameters.AddWithValue("p_driver1", Driver1empcode);
            MyCommand.Parameters.AddWithValue("p_driver2", Driver2empcode);
            MyCommand.Parameters.AddWithValue("p_conductor1", Conductor1empcode);
            MyCommand.Parameters.AddWithValue("p_conductor2", Conductor2empcode);
            dt = bll.SelectAll(MyCommand);
            lblConfirmErrorMsg.Text = "";
            string msg = "";
            int msgcnt = 0;
            if (dt.Rows[0]["validservice"].ToString() == "N")
            {
                msgcnt = msgcnt + 1;
                msg = msg + msgcnt.ToString() + ". Service Target Diesel is pending. </br>";
            }
            if (dt.Rows[0]["validdriver1"].ToString() == "N")
            {
                msgcnt = msgcnt + 1;
                msg = msg + msgcnt.ToString() + ". License details of " + Driver1 + " is pending. </br>";
            }
            if (dt.Rows[0]["validdriver2"].ToString() == "N")
            {
                msgcnt = msgcnt + 1;
                msg = msg + msgcnt.ToString() + ". License details of " + Driver2 + " is pending. </br>";
            }
            if (dt.Rows[0]["validconductor1"].ToString() == "N")
            {
                msgcnt = msgcnt + 1;
                msg = msg + msgcnt.ToString() + ". License details of " + Conductor1 + " is pending. </br>";
            }
            if (dt.Rows[0]["validconductor2"].ToString() == "N")
            {
                msgcnt = msgcnt + 1;
                msg = msg + msgcnt.ToString() + ". License details of " + Conductor2 + " is pending. </br>";
            }
            if (msgcnt > 0)
            {
                ConfirmMsg(msg);
                return true;
            }
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    #endregion
    #region Events
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        if (Session["Action"].ToString() == "A")
        {
            saveValues();
        }
        else if (Session["Action"].ToString() == "D")
        {
            //deleteDutyAllotment();
        }
    }
    protected void gvServices_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        HtmlGenericControl DivCrew2, DivCrew1;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView rowView = (DataRowView)e.Row.DataItem;

            DivCrew1 = (HtmlGenericControl)e.Row.FindControl("divcrew1")
            as HtmlGenericControl;
            DivCrew1.Visible = false;

            DivCrew2 = (HtmlGenericControl)e.Row.FindControl("divcrew2")
            as HtmlGenericControl;
            DivCrew2.Visible = false;

            if (Session["IsCrewAdded"].ToString() == "Not Added")
            {
                DivCrew1.Visible = false;
                DivCrew2.Visible = false;
            }
            else
            {
                if (Session["_DSVC_ID_"].ToString() == rowView["dsvc_id"].ToString() && Session["IsCrewAdded"].ToString() == "Added")
                {
                    Label lblServCode = (Label)e.Row.FindControl("lblServCode");
                    Session["Rout_Id"] = rowView["rout_id"];
                    Session["_DSVC_ID_"] = rowView["dsvc_id"];
                    Label lblbus = (Label)e.Row.FindControl("lblbus");
                    Label lblDriver = (Label)e.Row.FindControl("lblDriver");
                    Label lblConductor = (Label)e.Row.FindControl("lblConductor");
                    Label lblDriver2 = (Label)e.Row.FindControl("lblDriver2");
                    Label lblConductor2 = (Label)e.Row.FindControl("lblConductor2");
                    //LoadBuses(ddlBus);
                    lblbus.Text = ddlUBus.SelectedValue;
                    Session["UBus"] = ddlUBus.SelectedValue;
                    lblDriver2.Text = "N/A";
                    lblConductor2.Text = "N/A";
                    //loadMappedCrew(ddlDriver, "0", lblbus.Text, "D");
                    if (rowView["no_of_driver"].ToString() == "1")
                    {
                        lblDriver.Text = ddlUDriver.SelectedValue;
                        Session["UDriver"] = ddlUDriver.SelectedValue;
                    }
                    if (rowView["no_of_driver"].ToString() == "0")
                    {
                        lblDriver.Text = "N/A";
                        Session["UDriver"] = "0";
                    }
                    if (rowView["no_of_conductor"].ToString() == "1")
                    {
                        lblConductor.Text = ddlUConductor.SelectedValue;
                        Session["UConductor"] = ddlUConductor.SelectedValue;
                    }
                    if (rowView["no_of_conductor"].ToString() == "0")
                    {
                        lblConductor.Text = "N/A";
                        Session["UConductor"] = "0";
                    }

                    //loadMappedCrew(ddlConductor, "0", ddlBus, "C");
                    if (rowView["no_of_conductor"].ToString() == "2")
                    {
                        lblConductor2.Text = ddlUAltConductor.SelectedValue;
                    }
                    if (rowView["no_of_driver"].ToString() == "2")
                    {
                        lblDriver2.Text = ddlUAltDriver.SelectedValue;
                    }

                    

                    DivCrew1.Visible = true;
                    DivCrew2.Visible = true;
                }
            }
        }
    }
    protected void gvServicesList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "allotDuty")
        {
            try
            {
                if (Session["IsCrewAdded"].ToString() == "Not Added")
                {
                    Errormsg("First you have to add crew in this service.");
                    return;
                }
                GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                int RowIndex = oItem.RowIndex;
                Session["_SERVICECODE"] = gvServicesList.DataKeys[index].Values["dsvc_id"].ToString();
                if (Session["_DSVC_ID_"].ToString() != Session["_SERVICECODE"].ToString())
                {
                    Errormsg("First you have to add crew in this service.");
                    return;
                }


                string busType = gvServicesList.DataKeys[index].Values["bustype"].ToString();
                Session["_NOOFCREW"] = Convert.ToInt32(gvServicesList.DataKeys[index].Values["totalcrew"].ToString());
                Int32 crewNo = Convert.ToInt32(gvServicesList.DataKeys[index].Values["totalcrew"].ToString());

                hdnDutyDays.Value = gvServicesList.DataKeys[index].Values["dutyDays"].ToString();
                Session["_NOOFCREW"] = Convert.ToInt32(gvServicesList.DataKeys[index].Values["totalcrew"].ToString());

                Session["dutyDays"] = Convert.ToInt32(gvServicesList.DataKeys[index].Values["dutyDays"].ToString());

                Label lblDriver = (Label)gvServicesList.Rows[RowIndex].FindControl("lblDriver");
                Label lblConductor = (Label)gvServicesList.Rows[RowIndex].FindControl("lblConductor");
                Label lblDriver2 = (Label)gvServicesList.Rows[RowIndex].FindControl("lblDriver2");
                Label lblConductor2 = (Label)gvServicesList.Rows[RowIndex].FindControl("lblConductor2");
                Label lblBus = (Label)gvServicesList.Rows[RowIndex].FindControl("lblBus");

                string service = gvServicesList.DataKeys[index].Values["SERVICE_NAME_EN"].ToString();
                string driver1empcode = lblDriver.Text;
                string driver2empcode = "";
                string conductor1empcode = lblConductor.Text;
                string conductor2empcode = "";
                string driver1 = lblDriver.Text.ToString();
                string driver2 = "";
                string conductor1 = lblConductor.Text.ToString();
                string conductor2 = "";

                if (crewNo.ToString() == "2")
                {
                    driver2empcode = "0";
                    conductor2empcode = "0";
                    driver2 = "";
                    conductor2 = "";
                }
                else if (crewNo.ToString() == "3")
                {
                    conductor2empcode = lblConductor2.Text;
                    conductor2 = lblConductor2.Text.ToString();
                    conductor2empcode = "0";
                    conductor2 = "";
                }
                else if (crewNo.ToString() == "4")
                {
                    driver2empcode = lblDriver2.Text;
                    driver2 = lblDriver2.Text.ToString();
                    conductor2empcode = "0";
                    conductor2 = "";
                }
                if (busType == "HIRED")
                {
                    driver1empcode = "0";
                    driver1 = "";
                    driver2empcode = "0";
                    driver2 = "";
                }

                if (IsValidValues(busType, lblBus.Text, lblDriver.Text, lblConductor.Text, lblDriver2.Text, lblConductor2.Text, crewNo) == false)
                {
                    return;
                }
                else
                {
                    if (IsValidServiceCrew(Session["_SERVICECODE"].ToString(), service, driver1empcode, driver2empcode, conductor1empcode, conductor2empcode, driver1, driver2, conductor1, conductor2) == false)
                        return;

                    Session["Action"] = "A";
                    Session["RowIndex"] = RowIndex;
                    mpConfirmation.Show();
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }
        else if (e.CommandName == "updateDuty")
        {
            Session["Action"] = "U";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            Session["RowIndex"] = RowIndex;
            // If e.Row.RowType = DataControlRowType.DataRow Then
            LinkButton btn = (LinkButton)e.CommandSource;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            // ---------

            Session["Rout_Id"] = gvServicesList.DataKeys[index].Values["Rout_Id"].ToString();

            Session["_DSVC_ID"] = gvServicesList.DataKeys[index].Values["dsvc_id"].ToString();
            Session["_DSVC_ID_"] = gvServicesList.DataKeys[index].Values["dsvc_id"].ToString();
            Session["_driver_count"] = gvServicesList.DataKeys[index].Values["no_of_driver"].ToString();
            Session["_counductor_count"] = gvServicesList.DataKeys[index].Values["no_of_conductor"].ToString();


            lblServiceName.InnerText = gvServicesList.DataKeys[index].Values["SERVICE_NAME_EN"].ToString();
            lblServiceType.Text = gvServicesList.DataKeys[index].Values["Service_Type_Name_En"].ToString();
            lblBusType.Text = gvServicesList.DataKeys[index].Values["bustype"].ToString();
            lblRoute.Text = gvServicesList.DataKeys[index].Values["route_name"].ToString();
            lblCrewNo.Text = gvServicesList.DataKeys[index].Values["totalcrew"].ToString();
            LoadBuses(ddlUBus);
            loadMappedCrew(ddlUDriver, "0", "", "D");
            loadMappedCrew(ddlUAltDriver, ddlUDriver.SelectedValue, "", "D");
            loadMappedCrew(ddlUConductor, "0", "", "C");
            loadMappedCrew(ddlUAltConductor, ddlUAltConductor.SelectedValue, "", "C");
            trDriver1.Visible = false;
            trDriver2.Visible = false;
            trConductor2.Visible = false;
            trConductor1.Visible = false;
            if (gvServicesList.DataKeys[index].Values["no_of_driver"].ToString() == "1")
            {
                trDriver1.Visible = true;
                trDriver2.Visible = false;
            }
            if (gvServicesList.DataKeys[index].Values["no_of_driver"].ToString() == "2")
            {
                trDriver1.Visible = true;
                trDriver2.Visible = true;
            }
            if (gvServicesList.DataKeys[index].Values["no_of_conductor"].ToString() == "1")
            {
                trConductor2.Visible = false;
                trConductor1.Visible = true;
            }
            if (gvServicesList.DataKeys[index].Values["no_of_conductor"].ToString() == "2")
            {
                trConductor2.Visible = true;
                trConductor1.Visible = true;
            }

            //if (gvServicesList.DataKeys[index].Values["totalcrew"].ToString() == "3")
            //{
            //    trDriver1.Visible = true;
            //    trConductor1.Visible = true;
            //    trDriver2.Visible = true;
            //    trConductor2.Visible = false;
            //}
            //else if (gvServicesList.DataKeys[index].Values["totalcrew"].ToString() == "4")
            //{
            //    trDriver1.Visible = true;
            //    trConductor1.Visible = true;
            //    trDriver2.Visible = true;
            //    trConductor2.Visible = true;

            //}
            //else if (gvServicesList.DataKeys[index].Values["totalcrew"].ToString() == "2")
            //{
            //    trDriver1.Visible = true;
            //    trConductor1.Visible = true;
            //    trDriver2.Visible = false;
            //    trConductor2.Visible = false;
            //}
            if (gvServicesList.DataKeys[index].Values["bustype"].ToString() == "HIRED")
            {
                trDriver1.Visible = false;
                trDriver2.Visible = false;
            }
            mpUpdateDuty.Show();
        }
    }

    protected void lbtnsearch_Click(object sender, EventArgs e)
    {

        LoadAllServices();
        LoadAllotedServices();
    }
    protected void lbtnResetFilter_Click(object sender, EventArgs e)
    {
        //ddlAllocationStatusType.SelectedValue = "N";
        tbDutyDate.Text = "";
        //tbToDate.Text = "";
        ddlBusType.SelectedValue = "0";
        ddlServiceType.SelectedValue = "0";
        ddlRoutes.SelectedValue = "0";
    }
    protected void lbtnDownloadExcel_Click(object sender, EventArgs e)
    {
        ExportGridToExcel();

    }
    protected void lbtndownloadexcelalloted_Click(object sender, EventArgs e)
    {
        ExportGridAllotedDutyToExcel();
    }

    protected void lbtnview_Click(object sender, EventArgs e)
    {
        string msg = "";
        msg = msg + "1. Provisional duty allotment of Bus/Driver/Conductor will be done from here.<br/>";
        msg = msg + "2. Only those Buses will be available which are free and whose diesel and inspection are done.<br/>";
        msg = msg + "3. Only those Crew will be available who are free and attendance are marked. Attendance of 1 day prior to Duty date must be marked for provisional duty allotment.<br/>";
        msg = msg + "4. By default mapped bus and crew of the service will be shown in grid. If it shows N/A then it means no bus or crew is mapped. In this case click on update button and select bus and crew.<br/>";
        msg = msg + "5. If you want to update Bus or Crew then click on update button and change the Bus Crew accordingly.<br/>";
        msg = msg + "6. If you want to do extra duty allotment, then set current date as Duty Date and click on search. Select service, mark Bus crew and allot duty.<br/>";
        msg = msg + "7. Once provisional duty allotment generated, it cannot be cancelled.<br/>";
        msg = msg + "8. You can also view all previously generated provional duty allocations.<br/>";
        InfoMsg(msg);
    }

    public bool IsValidValues(string busType, string Bus, string Driver, string Conductor, string Driver2, string Conductor2, Int32 crewNo)
    {
        try
        {
            int msgcnt = 0;
            string msg = "";
            if (Bus == "" || Bus == "0")
            {
                msgcnt = msgcnt + 1;
                msg = msg + msgcnt.ToString() + ". Select Bus<br>";
            }
            if (busType == "CORPORATION")
            {
                if (Driver == "" || Driver == "0")
                {
                    msgcnt = msgcnt + 1;
                    msg = msg + msgcnt.ToString() + ". Select Driver<br>";
                }
            }

            if (Session["_counductor_count"].ToString() != "0")
            {
                if (Session["_counductor_count"].ToString() == "1")
                {
                    if (Conductor == "" || Conductor == "0")
                    {
                        msgcnt = msgcnt + 1;
                        msg = msg + msgcnt.ToString() + ". Select Conductor<br>";
                    }
                }
                if (Session["_counductor_count"].ToString() == "2")
                {
                    if (Conductor2 == "" || Conductor2 == "0")
                    {
                        msgcnt = msgcnt + 1;
                        msg = msg + msgcnt.ToString() + ". Select Alternate Conductor<br>";
                    }
                }
            }
            if (Session["_driver_count"].ToString() != "0")
            {
                if (Session["_driver_count"].ToString() == "1")
                {
                    if (Driver == "" || Driver == "0")
                    {
                        msgcnt = msgcnt + 1;
                        msg = msg + msgcnt.ToString() + ". Select Driver<br>";
                    }
                }
                if (Session["_driver_count"].ToString() == "2")
                {
                    if (Driver2 == "" || Driver2 == "0")
                    {
                        msgcnt = msgcnt + 1;
                        msg = msg + msgcnt.ToString() + ". Select Alternate Driver<br>";
                    }
                }
            }


            if (crewNo == 4)
            {
                if (Driver2 == "" || Driver2 == "0")
                {
                    msgcnt = msgcnt + 1;
                    msg = msg + msgcnt.ToString() + ". Select Alternate Driver<br>";
                }
            }
            
            if (msgcnt > 0)
            {
                Errormsg(msg);
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    protected void lbtnUpdateDuty_Click(object sender, EventArgs e)
    {
        string busType = lblBusType.Text.ToString();
        int crewNo = Convert.ToInt32(lblCrewNo.Text); ;
        if (IsValidValues(busType, ddlUBus.SelectedValue, ddlUDriver.SelectedValue, ddlUConductor.SelectedValue, ddlUAltDriver.SelectedValue, ddlUAltConductor.SelectedValue, crewNo) == false)
        {
            return;
        }
        else
        {
            int RowIndex = Convert.ToInt32(Session["RowIndex"]);
            Label lblbus = (Label)gvServicesList.Rows[RowIndex].FindControl("lblbus");
            Label lblDriver = (Label)gvServicesList.Rows[RowIndex].FindControl("lblDriver");
            Label lblConductor = (Label)gvServicesList.Rows[RowIndex].FindControl("lblConductor");
            Label lblDriver2 = (Label)gvServicesList.Rows[RowIndex].FindControl("lblDriver2");
            Label lblConductor2 = (Label)gvServicesList.Rows[RowIndex].FindControl("lblConductor2");
            //LoadBuses(ddlUBus);
            lblbus.Text = ddlUBus.SelectedValue;
            Session["UBus"] = ddlUBus.SelectedValue;

            //loadMappedCrew(ddlDriver, "0", lblbus.Text, "D");
            lblDriver.Text = ddlUDriver.SelectedValue;
            Session["UDriver"] = ddlUDriver.SelectedValue;

            //loadMappedCrew(ddlConductor, "0", ddlBus, "C");
            lblConductor.Text = ddlUConductor.SelectedValue;
            Session["UConductor"] = ddlUConductor.SelectedValue;

            //loadMappedCrew(ddlConductor2, ddlConductor.SelectedValue, ddlBus, "C");

            if (busType.ToUpper() == "Nigam")
            {
                //loadMappedCrew(ddlDriver2, ddlDriver.SelectedValue, ddlBus, "D");
                lblDriver2.Text = ddlUAltDriver.SelectedValue;
            }
            if (crewNo == 4)
            {
                //loadMappedCrew(ddlDriver2, ddlDriver.SelectedValue, ddlBus, "D");
                lblDriver2.Text = ddlUAltDriver.SelectedValue;
            }
            if (crewNo == 3)
            {
                lblConductor2.Text = ddlUAltConductor.SelectedValue;
            }
            Session["IsCrewAdded"] = "Added";
            LoadAllServices();
        }
    }

    protected void gvAllotedDuties_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "viewDuty")
        {
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            Session["dutyRefno"] = gvAllotedDuties.DataKeys[index].Values["dutyrefno"].ToString();
            // openSubDetailsWindow("DutyAllotmentSlip.aspx");
            eSlip.Text = "<embed src = \"DutyAllotmentSlip.aspx\" style=\"height: 80vh; width: 100%\" />";
            mpShowDuty.Show();
        }
        else if (e.CommandName == "cancelDuty")
        {
            Session["Action"] = "D";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            Session["dutyRefno"] = gvAllotedDuties.DataKeys[index].Values["dutyrefno"].ToString();
            ConfirmMsg("Do you want to cancel Duty Allotment?");
        }
    }
    #region "Dropdown select change"
    //protected void ddlAllocationStatusType_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //	if (ddlAllocationStatusType.SelectedValue == "N")
    //	{
    //		hdnGrdHeading.InnerHtml = "List of services for provisional duty allotment";
    //		Session["Action"] = "A";
    //		divToDate.Visible = false;
    //		tbFromDate.Visible = false;
    //		tbDutyDate.Visible = true;
    //		lblDate.Text = "Duty Date";
    //		tbDutyDate.Text = current_date;
    //		pnlAllotDuty.Visible = true;
    //		pnlViewDuty.Visible = false;
    //		LoadAllServices();
    //	}
    //	else if (ddlAllocationStatusType.SelectedValue == "P")
    //	{
    //		hdnGrdHeading.InnerHtml = "List of generated provisional duty allotment";
    //		Session["Action"] = "U";
    //		lblDate.Text = "From Date";
    //		divToDate.Visible = true;
    //		tbFromDate.Visible = true;
    //		tbDutyDate.Visible = false;
    //		tbFromDate.Text = current_date_7;
    //		tbToDate.Text =current_date;
    //		pnlAllotDuty.Visible = false;
    //		pnlViewDuty.Visible = true;
    //		LoadAllotedServices();
    //	}
    //}
    protected void ddlBus_SelectedIndexChange(object sender, EventArgs e)
    {
        loadMappedCrew(ddlUDriver, "0", "", "D");
        loadMappedCrew(ddlUAltDriver, ddlUDriver.SelectedValue, "", "D");

        loadMappedCrew(ddlUConductor, "0", "", "C");
        loadMappedCrew(ddlUAltConductor, ddlUConductor.SelectedValue, "", "C");
    }
    protected void ddlUDriver_SelectedIndexChanged(object sender, EventArgs e)
    {
        string driver1 = ddlUDriver.SelectedValue;
        string driver2 = ddlUAltDriver.SelectedValue;
        loadMappedCrew(ddlUAltDriver, driver1, "", "D");
    }
    protected void ddlUAltDriver_SelectedIndexChanged(object sender, EventArgs e)
    {
        string driver1 = ddlUDriver.SelectedValue;
        string driver2 = ddlUAltDriver.SelectedValue;
        loadMappedCrew(ddlUDriver, driver2, "", "D");
    }
    protected void ddlUConductor_SelectedIndexChanged(object sender, EventArgs e)
    {
        string conductor1 = ddlUConductor.SelectedValue;
        string conductor2 = ddlUAltConductor.SelectedValue;
        loadMappedCrew(ddlUAltConductor, conductor1, "", "C");
    }
    protected void ddlUAltConductor_SelectedIndexChanged(object sender, EventArgs e)
    {
        string conductor1 = ddlUConductor.SelectedValue;
        string conductor2 = ddlUAltConductor.SelectedValue;
        loadMappedCrew(ddlUConductor, conductor2, "", "C");
    }
    #endregion
    public bool sendSMSToEmployee(string MmobileNo, string MMsg)
    {
        try
        {
            // Dim CDACSMSCALL As New SMSHttpPostClient
            // CDACSMSCALL.GetSMScallMethod(MmobileNo, MMsg, "ccbss-701")
            // txtmsgLog.txtmsgLog_track(MMsg, MmobileNo, "", 2)
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
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
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_freebuscrew");
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
                mpUpdateDuty.Show();
                return;
            }
        }
        catch (Exception ex)
        {
            Errormsg("Error occurred while cancellation. " + ex.Message);
            mpUpdateDuty.Show();
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        eDash.Text = "<embed src = \"dashDuty.aspx\" style=\"height: 80vh; width: 100%\" />";
        mpDuty.Show();
    }
    protected void gvServicesList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvServicesList.PageIndex = e.NewPageIndex;
        LoadAllServices();


    }
    protected void gvAllotedDuties_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvAllotedDuties.PageIndex = e.NewPageIndex;
        LoadAllotedServices();

    }
    #endregion









}