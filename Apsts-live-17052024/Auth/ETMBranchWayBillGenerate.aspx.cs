using Npgsql;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ETMBranchWayBillGenerate : BasePage
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    string current_date = DateTime.Now.AddDays(1).ToString("dd") + "/" + DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("yyyy");

    protected void Page_Load(object sender, EventArgs e)
    {
        checkForSecurity();
        if (IsPostBack == false)
        {
            Session["_moduleName"] = "WayBill Generation";
            Session["Action"] = "A";
            Session["dutyRefno"] = "";
            tbDutyDate.Visible = true;
            tbDutyDate.Text = current_date;
            //divToDate.Visible = false;
            LoadRoutes();
            LoadBusServices();
            LoadBusType();
            LoadAllotedDuties();
            LoadWaybillsList();
            fillDropDownServiceTypes(ddlServiceType);
            fillDropDownRoutes(ddlRoutes);

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
        if (_security.isSessionExist(Session["_RNDIDENTIFIEREB"]) == true)
        {
            Session["_RNDIDENTIFIEREB"] = Session["_RNDIDENTIFIEREB"];
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

    #region Method
    protected void loadSummaryCount()
    {
        try
        {
            string officeid = Session["_OfficeId"].ToString();
            string depotid = Session["_LDepotCode"].ToString();
            Int32 etmbranchid = Convert.ToInt32(Session["_etmbranchid"]);
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
                    lblTotalSlip.Text = dt.Rows[0]["sppendingwaybilletm"].ToString();
                    lblPendingDutyaAllocation.Text = dt.Rows[0]["pendingdutyslips"].ToString();
                    lblTotBus.Text = dt.Rows[0]["totalBus"].ToString();
                    lbltotDrivers.Text = dt.Rows[0]["totDriver"].ToString();
                    lblOnDutyDriver.Text = dt.Rows[0]["onDutyDriver"].ToString();
                    lblBusonDuty.Text = dt.Rows[0]["OnDutyBus"].ToString();
                    lbltotConductors.Text = dt.Rows[0]["totConductor"].ToString();
                    lblOnDutyConductor.Text = dt.Rows[0]["onDutyConductor"].ToString();
                    lblETMAllocated.Text = dt.Rows[0]["spondutyetm"].ToString();
                    lblETMTotal.Text = dt.Rows[0]["spetmtotal"].ToString();
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
    public void LoadServiceOtherDetails()
    {
        try
        {
            gvServiceTrips.Visible = false;
            int serviceID = Convert.ToInt32(Session["_DSVC_ID"].ToString());
            string dutyrefno = Session["DUTYREFNO"].ToString();
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
                    gvServiceTrips.Visible = true;

                }
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message);
        }
    }
    protected void LoadETMs(DropDownList ddl)
    {
        try
        {
            ddl.Items.Clear();
            Int32 etmbranchid = Convert.ToInt32(Session["_etmbranchid"]);
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_etmlistforissue");
            MyCommand.Parameters.AddWithValue("p_etmbranchid", etmbranchid);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddl.DataSource = dt;
                    ddl.DataTextField = "etmserialno";
                    ddl.DataValueField = "etmid";
                    ddl.DataBind();
                }
            }
            ddl.Items.Insert(0, "Select");
            ddl.Items[0].Value = "0";
            ddl.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message);
            ddl.Items.Insert(0, "All");
            ddl.Items[0].Value = "0";
            ddl.SelectedIndex = 0;
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
            else if (Session["_CANCELTYPE"].ToString() == "WAYBILL")
            {
                procName = "dutyallocation.f_cancelwaybill";
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
                if (dt.Rows[0]["p_result"].ToString() == "EXCEPTION")
                {
                    Errormsg("Something Went Wrong");
                }
                else
                {
                    LoadWaybillsList();
                    LoadAllotedDuties();
                    Successmsg("Duty cancelled successfully");
                    loadSummaryCount();
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
    public void saveValues()
    {
        try
        {
            string denominationBook;
            int ManualTicketStart = 0;
            int ManualTicketEnd = 0;
            int Deno100Start = 0;
            int Deno100End = 0;
            int Deno100Total = 0;
            int Deno50Total = 0;
            int Deno50Start = 0;
            int Deno50End = 0;
            int Deno20Total = 0;
            int Deno20Start = 0;
            int Deno20End = 0;
            int Deno10Start = 0;
            int Deno10End = 0;
            int Deno10Total = 0;
            int Deno5Total = 0;
            int Deno5Start = 0;
            int Deno5End = 0;
            int Deno2Total = 0;
            int Deno2Start = 0;
            int Deno2End = 0;
            int Deno1Start = 0;
            int Deno1End = 0;
            int Deno1Total = 0;
            if (tbFromSerialNo.Text.Length > 0)
            {
                ManualTicketStart = Convert.ToInt32(tbFromSerialNo.Text.ToString());
            }
            if (tbToSerialNo.Text.Length > 0)
            {
                ManualTicketEnd = Convert.ToInt32(tbToSerialNo.Text.ToString());
            }
            if (tb100StartNo.Text.Length > 0)
            {
                Deno100Start = Convert.ToInt32(tb100StartNo.Text.ToString());
            }
            if (tb100EndNo.Text.Length > 0)
            {
                Deno100End = Convert.ToInt32(tb100EndNo.Text.ToString());
            }
            if (tb100Total.Text.Length > 0)
            {
                Deno100Total = Convert.ToInt32(tb100Total.Text.ToString());
            }
            if (tb50StartNo.Text.Length > 0)
            {
                Deno50Start = Convert.ToInt32(tb50StartNo.Text.ToString());
            }
            if (tb50EndNo.Text.Length > 0)
            {
                Deno50End = Convert.ToInt32(tb50EndNo.Text.ToString());
            }
            if (tb50Total.Text.Length > 0)
            {
                Deno50Total = Convert.ToInt32(tb50Total.Text.ToString());
            }
            if (tb20StartNo.Text.Length > 0)
            {
                Deno20Start = Convert.ToInt32(tb20StartNo.Text.ToString());
            }
            if (tb20EndNo.Text.Length > 0)
            {
                Deno20End = Convert.ToInt32(tb20EndNo.Text.ToString());
            }
            if (tb20Total.Text.Length > 0)
            {
                Deno20Total = Convert.ToInt32(tb20Total.Text.ToString());
            }
            if (tb10StartNo.Text.Length > 0)
            {
                Deno10Start = Convert.ToInt32(tb10StartNo.Text.ToString());
            }
            if (tb10EndNo.Text.Length > 0)
            {
                Deno10End = Convert.ToInt32(tb10EndNo.Text.ToString());
            }
            if (tb10Total.Text.Length > 0)
            {
                Deno10Total = Convert.ToInt32(tb10Total.Text.ToString());
            }
            if (tb5StartNo.Text.Length > 0)
            {
                Deno5Start = Convert.ToInt32(tb5StartNo.Text.ToString());
            }
            if (tb5EndNo.Text.Length > 0)
            {
                Deno5End = Convert.ToInt32(tb5EndNo.Text.ToString());
            }
            if (tb5Total.Text.Length > 0)
            {
                Deno5Total = Convert.ToInt32(tb5Total.Text.ToString());
            }
            if (tb2StartNo.Text.Length > 0)
            {
                Deno2Start = Convert.ToInt32(tb2StartNo.Text.ToString());
            }
            if (tb2EndNo.Text.Length > 0)
            {
                Deno2End = Convert.ToInt32(tb2EndNo.Text.ToString());
            }
            if (tb2Total.Text.Length > 0)
            {
                Deno2Total = Convert.ToInt32(tb2Total.Text.ToString());
            }
            if (tb1StartNo.Text.Length > 0)
            {
                Deno1Start = Convert.ToInt32(tb1StartNo.Text.ToString());
            }
            if (tb1EndNo.Text.Length > 0)
            {
                Deno1End = Convert.ToInt32(tb1EndNo.Text.ToString());
            }
            if (tb1Total.Text.Length > 0)
            {
                Deno1Total = Convert.ToInt32(tb1Total.Text.ToString());
            }

            string UpdatedBy = Session["_UserCode"].ToString();
            string IpAddress = HttpContext.Current.Request.UserHostAddress;
            if (trDenominationBook.Visible == true)
            {
                denominationBook = "Y";
            }
            else
            {
                denominationBook = "N";
            }
            ClassETM obj = new ClassETM();
            dt = obj.assignWaybill(Session["DUTYREFNO"].ToString(), Session["ETMREFNO"].ToString(), ManualTicketStart, ManualTicketEnd, denominationBook, Deno100Start, Deno100End, Deno100Total,
                Deno50Start, Deno50End, Deno50Total, Deno20Start, Deno20End, Deno20Total, Deno10Start, Deno10End, Deno10Total,
                Deno5Start, Deno5End, Deno5Total, Deno2Start, Deno2End, Deno2Total, Deno1Start, Deno1End, Deno1Total, UpdatedBy, IpAddress, "",
               "", "W", "");
            if (dt.TableName == "Success")
            {
                if (dt.Rows[0]["sprefno"].ToString()== "EXCEPTION")
                {
                    Errormsg("Something Went Wrong.");
                }
                else
                {
                    Session["WAYBILLID"] = "";
                    pnlUpdateDutySlips.Visible = false;
                    pnlAddDutySlips.Visible = true;
                    reset();
                    LoadAllotedDuties();
                    LoadWaybillsList();
                    pnlSummary.Visible = true;
                    loadSummaryCount();

                    eSlip.Text = "<embed src = \"Waybill.aspx\" style=\"height: 80vh; width: 100%\" />";
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
    protected void LoadAllotedDuties()
    {
        try
        {
            pnlNorecord1.Visible = true;
            gvAllotedDuties.Visible = false;

            lbtnDownload1.Visible = false;
            string officeid = Session["_LDepotCode"].ToString();
            int servicetype = Convert.ToInt16(ddlServiceType.SelectedValue.ToString());
            //string searchText = tbSearch.Text.ToString();
            string dutydate = tbDutyDate.Text.ToString();

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();

            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_dutyslipforwaybill");
            MyCommand.Parameters.AddWithValue("p_busdepotcode", officeid);
            MyCommand.Parameters.AddWithValue("p_servicetype", servicetype);
            MyCommand.Parameters.AddWithValue("p_route", Convert.ToInt32(ddlRoutes.SelectedValue));
            MyCommand.Parameters.AddWithValue("p_bustype", ddlBusType.SelectedValue);
            MyCommand.Parameters.AddWithValue("p_dutydate", dutydate);
            dt = bll.SelectAll(MyCommand);

            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    lbtnDownload1.Visible = true;
                    gvAllotedDuties.DataSource = dt;
                    gvAllotedDuties.DataBind();
                    pnlNorecord1.Visible = false;
                    gvAllotedDuties.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message);
        }
    }
    protected void LoadWaybillsList()
    {
        try
        {
            lbtnDownload2.Visible = false;
            pnlNoRecord2.Visible = true;

            gvWaybillList.Visible = false;

            string officeid = Session["_LDepotCode"].ToString();
            int servicetype = Convert.ToInt16(ddlServiceType.SelectedValue.ToString());
            //string searchText = tbSearch.Text.ToString();
            //string fromdate = tbFromDate.Text.ToString();
            //string todate = tbToDate.Text.ToString();

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_dutywaybillslist");
            MyCommand.Parameters.AddWithValue("p_busdepotcode", officeid);
            MyCommand.Parameters.AddWithValue("p_service", servicetype);
            MyCommand.Parameters.AddWithValue("p_bustype", ddlBusType.SelectedValue);
            MyCommand.Parameters.AddWithValue("p_fromdate", tbDutyDate.Text);
            MyCommand.Parameters.AddWithValue("p_todate", "0");
            MyCommand.Parameters.AddWithValue("p_route", Convert.ToInt32(ddlRoutes.SelectedValue));
            dt = bll.SelectAll(MyCommand);

            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    lbtnDownload2.Visible = true;
                    gvWaybillList.DataSource = dt;
                    gvWaybillList.DataBind();
                    pnlNoRecord2.Visible = false;
                    gvWaybillList.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message);
        }
    }
    public void fillDropDownServiceTypes(DropDownList ddl)//M2
    {
        try
        {
            ddl.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_servicetype_getlist");
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                ddl.DataSource = dt;
                ddl.DataTextField = "servicetype_name_en";
                ddl.DataValueField = "srtpid";
                ddl.DataBind();
            }
            ddl.Items.Insert(0, "All");
            ddl.Items[0].Value = "0";
            ddl.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddl.Items.Insert(0, "All");
            ddl.Items[0].Value = "0";
            ddl.SelectedIndex = 0;
            _common.ErrorLog("DS mgmt-M2", ex.Message.ToString());
        }
    }
    public void fillDropDownRoutes(DropDownList ddl)//M4
    {
        try
        {
            ddl.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_routes");
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                ddl.DataSource = dt;
                ddl.DataTextField = "routename";
                ddl.DataValueField = "routeid";
                ddl.DataBind();
            }
            ddl.Items.Insert(0, "All");
            ddl.Items[0].Value = "0";
            ddl.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddl.Items.Insert(0, "All");
            ddl.Items[0].Value = "0";
            ddl.SelectedIndex = 0;
            _common.ErrorLog("DS mgmt-M4", ex.Message.ToString());
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
            this.LoadAllotedDuties();
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
    private void ExportGridToExcel()
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=WayBillGenerate.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            // To Export all pages
            gvWaybillList.AllowPaging = false;
            this.LoadWaybillsList();
            gvWaybillList.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in gvWaybillList.HeaderRow.Cells)
                cell.BackColor = gvWaybillList.HeaderStyle.BackColor;
            foreach (GridViewRow row in gvWaybillList.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                        cell.BackColor = gvWaybillList.AlternatingRowStyle.BackColor;
                    else
                        cell.BackColor = gvWaybillList.RowStyle.BackColor;
                    cell.CssClass = "textmode";
                }
            }

            gvWaybillList.RenderControl(hw);
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
        lblConfirmation.Text = msg;
        mpConfirmation.Show();
    }
    private void InfoMsg(string msg)
    {
        string popup = _popup.modalPopupLarge("I", "Information", msg, "Close");
        Response.Write(popup);
    }
    #endregion
    #region Events
    protected void ddlAllocationStatusType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlAllocationStatusType.SelectedValue == "N")
        //{
        //	hdnGrdHeading.InnerHtml = "List of services for provisional duty allotment";
        //	Session["Action"] = "A";
        //	divToDate.Visible = false;
        //	tbFromDate.Visible = false;
        //	tbDutyDate.Visible = true;
        //	lblDate.Text = "Duty Date";
        //	tbDutyDate.Text = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy");
        //	pnlAllotDuty.Visible = true;
        //	pnlDutySlips.Visible = false;
        //	LoadAllotedDuties();
        //}
        //else if (ddlAllocationStatusType.SelectedValue == "P")
        //{
        //	hdnGrdHeading.InnerHtml = "List of generated waybills";
        //	Session["Action"] = "U";
        //	lblDate.Text = "From Date";
        //	tbFromDate.Text = DateTime.Now.AddDays(-7).ToString("dd/MM/yyyy");
        //	tbToDate.Text = DateTime.Now.AddDays(+1).ToString("dd/MM/yyyy");
        //	tbFromDate.Visible = true;
        //	tbDutyDate.Visible = false;
        //	divToDate.Visible = true;
        //	pnlAllotDuty.Visible = false;
        //	pnlDutySlips.Visible = true;
        //	LoadWaybillsList();
        //}

    }
    protected void lbtnsearch_Click(object sender, EventArgs e)
    {
        LoadAllotedDuties();
        LoadWaybillsList();
        //if (ddlAllocationStatusType.SelectedValue == "N")
        //{
        //	pnlAllotDuty.Visible = true;
        //	pnlDutySlips.Visible = false;
        //	LoadAllotedDuties();
        //}
        //else if (ddlAllocationStatusType.SelectedValue == "P")
        //{
        //	pnlAllotDuty.Visible = false;
        //	pnlDutySlips.Visible = true;
        //	LoadWaybillsList();
        //}

    }
    protected void lbtnResetFilter_Click(object sender, EventArgs e)
    {

        pnlAllotDuty.Visible = true;
        pnlDutySlips.Visible = true;
        //	ddlAllocationStatusType.SelectedValue = "N";
        tbDutyDate.Text = "";
        //tbToDate.Text = "";
        //tbSearch.Text = "";
        ddlBusType.SelectedValue = "0";
        ddlServiceType.SelectedValue = "0";
        ddlRoutes.SelectedValue = "0";
        LoadAllotedDuties();
        LoadWaybillsList();
    }
    protected void lbtnDownloadExcel_Click(object sender, EventArgs e)
    {
        //if (ddlAllocationStatusType.SelectedValue == "N")
        //{
        //	ExportGridAllotedDutyToExcel();

        //}
        //if (ddlAllocationStatusType.SelectedValue == "P")
        //{
        //	ExportGridToExcel();
        //}

    }
    protected void lbtnDownload1_Click(object sender, EventArgs e)
    {
        ExportGridAllotedDutyToExcel();
    }

    protected void lbtnDownload2_Click(object sender, EventArgs e)
    {
        ExportGridToExcel();
    }
    protected void lbtnview_Click(object sender, EventArgs e)
    {
        string msg = "";
        msg = msg + "1. Here you can issue ETM to Duty slip and generate waybill.<br/>";
        msg = msg + "2. Only those ETM's will come which are free and ok.<br/>";
        msg = msg + "3. If there is any change in duty slip then you can cancel it from here also,after cancellation Bus and Crew will be marked as free.<br/>";
        msg = msg + "4. After generating waybill you can view generated waybills and cancel waybill if required.<br/>";
        msg = msg + "5. Waybill will cancel only if its time is not over.After cancellation both waybill and duty slip will cancel and etm mark as free.<br/>";
        InfoMsg(msg);
    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        if (Session["Action"].ToString() == "A")
        {
            saveValues();
        }
        else if (Session["Action"].ToString() == "C")
        {
            cancelDuty();
        }
    }
    protected void reset()
    {
        tbFromSerialNo.Text = "";
        tbToSerialNo.Text = "";
        tb100StartNo.Text = "";
        tb100EndNo.Text = "";
        tb100Total.Text = "";
        tb50StartNo.Text = "";
        tb50EndNo.Text = "";
        tb50Total.Text = "";
        tb20StartNo.Text = "";
        tb20EndNo.Text = "";
        tb20Total.Text = "";
        tb10StartNo.Text = "";
        tb10EndNo.Text = "";
        tb10Total.Text = "";
        tb5StartNo.Text = "";
        tb5EndNo.Text = "";
        tb5Total.Text = "";
        tb2StartNo.Text = "";
        tb2EndNo.Text = "";
        tb2Total.Text = "";
        tb1StartNo.Text = "";
        tb1EndNo.Text = "";
        tb1Total.Text = "";

    }
    protected void gvAllotedDuties_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "generateWaybill")
        {
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            Session["RowIndex"] = RowIndex;
            LinkButton btn = (LinkButton)e.CommandSource;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            DropDownList ddlETM = row.FindControl("ddlETM") as DropDownList;
            if (ddlETM.SelectedValue == "0")
            {
                Errormsg("Please Select ETM.");
                return;
            }

            Session["DUTYREFNO"] = gvAllotedDuties.DataKeys[index].Values["DUTYREFNO"].ToString();
            //lblRefNo.Text = Session["DUTYREFNO"].ToString();
            lblservicecode.Text = gvAllotedDuties.DataKeys[index].Values["DSVC_ID"].ToString();
            lblServiceName.Text = gvAllotedDuties.DataKeys[index].Values["SERVICENAME"].ToString();
            lblBus.Text = gvAllotedDuties.DataKeys[index].Values["BUSNO"].ToString();
            lblBusType.Text = gvAllotedDuties.DataKeys[index].Values["Bustype"].ToString();
            lblRoute.Text = gvAllotedDuties.DataKeys[index].Values["ROUTE"].ToString();
            lblDeptTime.Text = gvAllotedDuties.DataKeys[index].Values["DUTYDATE"].ToString();

            Session["ETMREFNO"] = ddlETM.SelectedValue;
            lblAllotedEtm.Text = ddlETM.SelectedItem.ToString();
            lblConductor.Text = gvAllotedDuties.DataKeys[index].Values["EMPCONDUCTOR1"].ToString();
            if (DBNull.Value.Equals(gvAllotedDuties.DataKeys[index].Values["DRIVER1EMPCODE"]) == false)
            {
                lblDriver.Text = gvAllotedDuties.DataKeys[index].Values["EMPDRIVER1"].ToString();
            }
            if (DBNull.Value.Equals(gvAllotedDuties.DataKeys[index].Values["DRIVER2EMPCODE"]) == false)
            {
                lblAltDriver.Text = gvAllotedDuties.DataKeys[index].Values["EMPDRIVER2"].ToString();
            }
            if (DBNull.Value.Equals(gvAllotedDuties.DataKeys[index].Values["CONDUCTOR2EMPCODE"]) == false)
            {
                lblAltConductor.Text = gvAllotedDuties.DataKeys[index].Values["EMPCONDUCTOR2"].ToString();
                lblChangeStation.Text = gvAllotedDuties.DataKeys[index].Values["CHANGESTATION"].ToString();
            }
            if (DBNull.Value.Equals(gvAllotedDuties.DataKeys[index].Values["ODOMETERREADING"]) == false)
            {
                lblOdometerReading.Text = gvAllotedDuties.DataKeys[index].Values["ODOMETERREADING"].ToString();
            }
            if (DBNull.Value.Equals(gvAllotedDuties.DataKeys[index].Values["SCHEDULEKM"]))
            {
                lblSchKm.Text = gvAllotedDuties.DataKeys[index].Values["SCHEDULEKM"].ToString();
            }
            if (DBNull.Value.Equals(gvAllotedDuties.DataKeys[index].Values["TARGETINCOME"]) == false)
            {
                lblTargetIncome.Text = gvAllotedDuties.DataKeys[index].Values["TARGETINCOME"].ToString();
            }
            if (DBNull.Value.Equals(gvAllotedDuties.DataKeys[index].Values["TARGETDIESELAVERAGE"]) == false)
            {
                lblTargetDieselAverage.Text = gvAllotedDuties.DataKeys[index].Values["TARGETDIESELAVERAGE"].ToString();
            }
            Session["_DSVC_ID"] = gvAllotedDuties.DataKeys[index].Values["DSVC_ID"].ToString();
            LoadServiceOtherDetails();
            pnlUpdateDutySlips.Visible = true;
            pnlAddDutySlips.Visible = false;
            pnlSummary.Visible = false;
            //trManualTicketBook.Visible = true;
            //trDenominationBook.Visible = true;
        }
        else if (e.CommandName == "cancelDutySlip")
        {
            Session["_CANCELTYPE"] = "DUTYSLIP";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            Session["dutySlipRefno"] = gvAllotedDuties.DataKeys[index].Values["DUTYREFNO"].ToString();
            Session["dutyRefno"] = gvAllotedDuties.DataKeys[index].Values["DUTYREFNO"].ToString();
            Session["Action"] = "C";
            Session["RowIndex"] = RowIndex;
            ConfirmMsg("Do you want to cancel duty?");
        }
    }
    protected void lbtnGenerateWaybill_Click(object sender, EventArgs e)
    {
        if (IsValidValues() == false)
        {
            return;
        }
        Session["Action"] = "A";
        ConfirmMsg("Do you want to generate waybill?");
    }
    public bool IsValidValues()
    {
        try
        {
            int msgcnt = 0;
            string msg = "";
            if (trManualTicketBook.Visible == true)
            {
                if (tbFromSerialNo.Text == "")
                {
                    msgcnt = msgcnt + 1;
                    msg = msg + msgcnt.ToString() + ". Enter Ticket Book Start Serial No.<br>";
                }
                if (tbToSerialNo.Text == "")
                {
                    msgcnt = msgcnt + 1;
                    msg = msg + msgcnt.ToString() + ". Enter Ticket Book End Serial No.<br>";
                }
            }
            if (trDenominationBook.Visible == true)
            {
                if (tb100StartNo.Text == "" & tb50StartNo.Text == "" & tb20StartNo.Text == "" & tb10StartNo.Text == "" & tb5StartNo.Text == "" & tb2StartNo.Text == "" & tb1StartNo.Text == "")
                {
                    msgcnt = msgcnt + 1;
                    msg = msg + msgcnt.ToString() + ". Enter Denomination Ticket Book Detail for atleast 1 Denomination<br>";
                }

                if (tb100StartNo.Text.Length > 0 || tb100EndNo.Text.Length > 0)
                {
                    if (tb100StartNo.Text == "")
                    {
                        msgcnt = msgcnt + 1;
                        msg = msg + msgcnt.ToString() + ". Enter Denomination 100 Start Serial No.<br>";
                    }
                    else if (tb100EndNo.Text == "")
                    {
                        msgcnt = msgcnt + 1;
                        msg = msg + msgcnt.ToString() + ". Enter Denomination 100 End Serial No.<br>";
                    }
                }
                if (tb50StartNo.Text.Length > 0 || tb50EndNo.Text.Length > 0)
                {
                    if (tb50StartNo.Text == "")
                    {
                        msgcnt = msgcnt + 1;
                        msg = msg + msgcnt.ToString() + ". Enter Denomination 50 Start Serial No.<br>";
                    }
                    else if (tb50EndNo.Text == "")
                    {
                        msgcnt = msgcnt + 1;
                        msg = msg + msgcnt.ToString() + ". Enter Denomination 50 End Serial No.<br>";
                    }
                }
                if (tb20StartNo.Text.Length > 0 || tb20EndNo.Text.Length > 0)
                {
                    if (tb20StartNo.Text == "")
                    {
                        msgcnt = msgcnt + 1;
                        msg = msg + msgcnt.ToString() + ". Enter Denomination 20 Start Serial No.<br>";
                    }
                    else if (tb20EndNo.Text == "")
                    {
                        msgcnt = msgcnt + 1;
                        msg = msg + msgcnt.ToString() + ". Enter Denomination 20 End Serial No.<br>";
                    }
                }
                if (tb10StartNo.Text.Length > 0 || tb10EndNo.Text.Length > 0)
                {
                    if (tb10StartNo.Text == "")
                    {
                        msgcnt = msgcnt + 1;
                        msg = msg + msgcnt.ToString() + ". Enter Denomination 10 Start Serial No.<br>";
                    }
                    else if (tb10EndNo.Text == "")
                    {
                        msgcnt = msgcnt + 1;
                        msg = msg + msgcnt.ToString() + ". Enter Denomination 10 End Serial No.<br>";
                    }
                }
                if (tb5StartNo.Text.Length > 0 || tb5EndNo.Text.Length > 0)
                {
                    if (tb5StartNo.Text == "")
                    {
                        msgcnt = msgcnt + 1;
                        msg = msg + msgcnt.ToString() + ". Enter Denomination 5 Start Serial No.<br>";
                    }
                    else if (tb5EndNo.Text == "")
                    {
                        msgcnt = msgcnt + 1;
                        msg = msg + msgcnt.ToString() + ". Enter Denomination 5 End Serial No.<br>";
                    }
                }
                if (tb2StartNo.Text.Length > 0 || tb2EndNo.Text.Length > 0)
                {
                    if (tb2StartNo.Text == "")
                    {
                        msgcnt = msgcnt + 1;
                        msg = msg + msgcnt.ToString() + ". Enter Denomination 2 Start Serial No.<br>";
                    }
                    else if (tb2EndNo.Text == "")
                    {
                        msgcnt = msgcnt + 1;
                        msg = msg + msgcnt.ToString() + ". Enter Denomination 2 End Serial No.<br>";
                    }
                }
                if (tb1StartNo.Text.Length > 0 || tb1EndNo.Text.Length > 0)
                {
                    if (tb1StartNo.Text == "")
                    {
                        msgcnt = msgcnt + 1;
                        msg = msg + msgcnt.ToString() + ". Enter Denomination 1 Start Serial No.<br>";
                    }
                    else if (tb1EndNo.Text == "")
                    {
                        msgcnt = msgcnt + 1;
                        msg = msg + msgcnt.ToString() + ". Enter Denomination 1 End Serial No.<br>";
                    }
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
    protected void gvAllotedDuties_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvAllotedDuties.PageIndex = e.NewPageIndex;
        LoadAllotedDuties();
    }
    protected void gvWaybillList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvAllotedDuties.PageIndex = e.NewPageIndex;
        LoadWaybillsList();
    }
    protected void gvAllotedDuties_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView rowView = (DataRowView)e.Row.DataItem;
            DropDownList ddlETM = (DropDownList)e.Row.FindControl("ddlETM");
            LoadETMs(ddlETM);
        }
    }
    protected void lbtnCancel_Click(object sender, EventArgs e)
    {
        pnlUpdateDutySlips.Visible = false;
        pnlAddDutySlips.Visible = true;
        pnlSummary.Visible = true;
    }
    protected void gvWaybillList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "viewWaybill")
        {
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            Session["DUTYREFNO"] = gvWaybillList.DataKeys[index].Values["DUTYREFNO"].ToString();
            //openSubDetailsWindow("Waybill.aspx");
            eSlip.Text = "<embed src = \"Waybill.aspx\" style=\"height: 80vh; width: 100%\" />";
            mpShowDuty.Show();
        }
        else if (e.CommandName == "cancelWaybill")
        {
            Session["Action"] = "C";
            Session["_CANCELTYPE"] = "WAYBILL";
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            Session["RowIndex"] = RowIndex;
            Session["DUTYREFNO"] = gvWaybillList.DataKeys[index].Values["DUTYREFNO"].ToString();
            ConfirmMsg("Do you want to cancel Waybill?");
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
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "NewWindow", fullURL, true); // 
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void gvWaybillList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbtncancelWaybill = (LinkButton)e.Row.FindControl("lbtncancelWaybill");
            DataRowView rowView = (DataRowView)e.Row.DataItem;
            lbtncancelWaybill.Enabled = true;
            lbtncancelWaybill.CssClass = "btn btn-sm btn-danger";
            if ((DBNull.Value.Equals(rowView["deleteWaybillYN"]) == false))
            {
                if (rowView["deleteWaybillYN"].ToString() == "N")
                {
                    lbtncancelWaybill.Enabled = false;
                    lbtncancelWaybill.CssClass = "btn btn-sm btn-danger disabled";
                }
            }
        }
    }
    private void tbFromSerialNo_TextChanged(object sender, EventArgs e)
    {
        TextBox list = (TextBox)sender;
        if (tbFromSerialNo.Text.Length > 0 & tbToSerialNo.Text.Length > 0)
        {
            Int64 startNo = Convert.ToInt64(tbFromSerialNo.Text.ToString());
            Int64 endNo = Convert.ToInt64(tbToSerialNo.Text.ToString());
            if (startNo > endNo)
            {
                Errormsg("Please enter valid Serial Start no and End No");
                list.Text = "";
                return;
            }
        }
    }
    private void tb100_TextChanged(object sender, EventArgs e)
    {
        TextBox list = (TextBox)sender;
        tb100Total.Text = "";
        if (tb100StartNo.Text.Length > 0 & tb100EndNo.Text.Length > 0)
        {
            Int64 startNo = Convert.ToInt64(tb100StartNo.Text.ToString());
            Int64 endNo = Convert.ToInt64(tb100EndNo.Text.ToString());
            if (startNo >= endNo)
            {
                Errormsg("Please enter valid Serial Start No and End No");
                list.Text = "";
                return;
            }
            else
            {
                tb100Total.Text = (100 * (endNo - startNo + 1)).ToString();
            }
        }
    }
    private void tb50_TextChanged(object sender, EventArgs e)
    {
        TextBox list = (TextBox)sender;
        tb50Total.Text = "";
        if (tb50StartNo.Text.Length > 0 & tb50EndNo.Text.Length > 0)
        {
            Int64 startNo = Convert.ToInt64(tb50StartNo.Text.ToString());
            Int64 endNo = Convert.ToInt64(tb50EndNo.Text.ToString());
            if (startNo >= endNo)
            {
                Errormsg("Please enter valid Serial Start No and End No");
                list.Text = "";
                return;
            }
            else
            {
                tb50Total.Text = (50 * (endNo - startNo + 1)).ToString();
            }
        }
    }
    private void tb20_TextChanged(object sender, EventArgs e)
    {
        TextBox list = (TextBox)sender;
        tb20Total.Text = "";
        if (tb20StartNo.Text.Length > 0 & tb20EndNo.Text.Length > 0)
        {
            Int64 startNo = Convert.ToInt64(tb20StartNo.Text.ToString());
            Int64 endNo = Convert.ToInt64(tb20EndNo.Text.ToString());
            if (startNo >= endNo)
            {
                Errormsg("Please enter valid Serial Start No and End No");
                list.Text = "";
                return;
            }
            else
            {
                tb20Total.Text = (20 * (endNo - startNo + 1)).ToString();
            }
        }
    }
    private void tb10_TextChanged(object sender, EventArgs e)
    {
        TextBox list = (TextBox)sender;
        tb10Total.Text = "";
        if (tb10StartNo.Text.Length > 0 & tb10EndNo.Text.Length > 0)
        {
            Int64 startNo = Convert.ToInt64(tb10StartNo.Text.ToString());
            Int64 endNo = Convert.ToInt64(tb10EndNo.Text.ToString());
            if (startNo >= endNo)
            {
                Errormsg("Please enter valid Serial Start No and End No");
                list.Text = "";
                return;
            }
            else
            {
                tb10Total.Text = (10 * (endNo - startNo + 1)).ToString();
            }
        }
    }
    private void tb5_TextChanged(object sender, EventArgs e)
    {
        TextBox list = (TextBox)sender;
        tb5Total.Text = "";
        if (tb5StartNo.Text.Length > 0 & tb5EndNo.Text.Length > 0)
        {
            Int64 startNo = Convert.ToInt64(tb5StartNo.Text.ToString());
            Int64 endNo = Convert.ToInt64(tb5EndNo.Text.ToString());
            if (startNo >= endNo)
            {
                Errormsg("Please enter valid Serial Start No and End No");
                list.Text = "";
                return;
            }
            else
            {
                tb5Total.Text = (5 * (endNo - startNo + 1)).ToString();
            }
        }
    }
    private void tb2_TextChanged(object sender, EventArgs e)
    {
        TextBox list = (TextBox)sender;
        tb2Total.Text = "";
        if (tb2StartNo.Text.Length > 0 & tb2EndNo.Text.Length > 0)
        {
            Int64 startNo = Convert.ToInt64(tb2StartNo.Text.ToString());
            Int64 endNo = Convert.ToInt64(tb2EndNo.Text.ToString());
            if (startNo >= endNo)
            {
                Errormsg("Please enter valid Serial Start No and End No");
                list.Text = "";
                return;
            }
            else
            {
                tb2Total.Text = (2 * (endNo - startNo + 1)).ToString();
            }
        }
    }
    private void tb1_TextChanged(object sender, EventArgs e)
    {
        TextBox list = (TextBox)sender;
        tb1Total.Text = "";
        if (tb1StartNo.Text.Length > 0 & tb1EndNo.Text.Length > 0)
        {
            Int64 startNo = Convert.ToInt64(tb1StartNo.Text.ToString());
            Int64 endNo = Convert.ToInt64(tb1EndNo.Text.ToString());
            if (startNo >= endNo)
            {
                Errormsg("Please enter valid Serial Start No and End No");
                list.Text = "";
                return;
            }
            else
            {
                tb1Total.Text = (1 * (endNo - startNo + 1)).ToString();
            }
        }
    }
    protected void lbtnArchive_Click(object sender, EventArgs e)
    {
        eDash.Text = "<embed src = \"dashwaybill.aspx\" style=\"height: 80vh; width: 100%\" />";
        mpDuty.Show();
    }
    #endregion
}