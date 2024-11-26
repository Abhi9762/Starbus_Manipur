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

public partial class ETMBranchSubmission : System.Web.UI.Page
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    string current_date = DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("yyyy");

    protected void Page_Load(object sender, EventArgs e)
    {
        lblSummary.Text = "Summary as on Date " + DateTime.Now.ToString("dd/MM/yyyy hh:ss tt");
        if (IsPostBack == false)
        {

            tbDutyDate.Text = current_date;
            Session["_moduleName"] = "ETM Submission";
            fillDropDownServiceTypes(ddlServiceType);
            fillDropDownRoutes(ddlRoutes);
            LoadBusType();
            LoadWaybillsList();
            loadSubmittedWaybills();

        }
        LoadDashboardCounts();
    }

    #region Method
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
    private void loadSubmittedWaybills()
    {
        try
        {
            lbtnDownload2.Visible = false;
            pnlNoRecord2.Visible = true;

            gvSubmittedWaybills.Visible = false;

            string officeid = Session["_LDepotCode"].ToString();
            int servicetype = Convert.ToInt16(ddlServiceType.SelectedValue.ToString());

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_submitted_waybillslist");
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
                    gvSubmittedWaybills.DataSource = dt;
                    gvSubmittedWaybills.DataBind();
                    pnlNoRecord2.Visible = false;
                    gvSubmittedWaybills.Visible = true;
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
            lbtnDownload1.Visible = false;
            pnlNoRecord.Visible = true;

            gvWaybills.Visible = false;

            string officeid = Session["_LDepotCode"].ToString();
            int servicetype = Convert.ToInt16(ddlServiceType.SelectedValue.ToString());

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_waybillslist_for_validate");
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
                    lbtnDownload1.Visible = true;
                    gvWaybills.DataSource = dt;
                    gvWaybills.DataBind();
                    pnlNoRecord.Visible = false;
                    gvWaybills.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message);
        }
    }
    protected void LoadWaybillDetails()
    {
        try
        {
            gvServiceTrips.Visible = false;
            string waybillid = Session["_WAYBILLID"].ToString();

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_dutytripforetmsubmission");
            MyCommand.Parameters.AddWithValue("p_waybillno", waybillid);
            dt = bll.SelectAll(MyCommand);

            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    tbWaybillNo.Text = dt.Rows[0]["DUTYREFNO"].ToString();
                    lblServiceName.Text = dt.Rows[0]["service_name"].ToString();
                    lblBusNo.Text = dt.Rows[0]["BUSNO"].ToString();
                    lblETMNo.Text = dt.Rows[0]["etmserialno"].ToString();
                    lblDriver.Text = dt.Rows[0]["DRIVER"].ToString();
                    lblConductor.Text = dt.Rows[0]["CONDUCTOR"].ToString();
                    pnlNoRecord.Visible = false;
                    loadAmountFields(tbWaybillNo.Text);
                    loadWaybillTrips(tbWaybillNo.Text);
                }
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message);
        }
    }
    private void loadWaybillTrips(string waybill)
    {
        gvServiceTrips.Visible = false;
        pnlNoRecordtrip.Visible = false;
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "etm.f_get_trip_info_waybill");
        MyCommand.Parameters.AddWithValue("p_waybill_no", waybill);
        dt = bll.SelectAll(MyCommand);

        if (dt.TableName == "Success")
        {
            if (dt.Rows.Count > 0)
            {
                gvServiceTrips.DataSource = dt;
                gvServiceTrips.DataBind();
                gvServiceTrips.Visible = true;
                pnlNoRecordtrip.Visible = false;
            }
            else
            {
                gvServiceTrips.Visible = false;
                pnlNoRecordtrip.Visible = true;
            }
        }
    }
    private void loadAmountFields(string waybill)
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "etm.f_get_validate_waybill_details");
        MyCommand.Parameters.AddWithValue("p_waybill_no", waybill);
        dt = bll.SelectAll(MyCommand);

        if (dt.TableName == "Success")
        {
            if (dt.Rows.Count > 0)
            {
                tbOnlineTktamt.Text = dt.Rows[0]["online_ticket_amount"].ToString();
                tbEnrouteTktamt.Text = dt.Rows[0]["enroute_amount"].ToString();
                tbDhabaAmt.Text = dt.Rows[0]["dhaba_amount"].ToString();
                tbOtherEarningAmt.Text = dt.Rows[0]["extra_earning"].ToString();
                tbTotalEarningAmt.Text = dt.Rows[0]["total_earning"].ToString();
                tbLuggageAmt.Text = dt.Rows[0]["luggage_amount"].ToString();
                tbTollpaid.Text = dt.Rows[0]["toll_amount"].ToString();
                tbParking.Text = dt.Rows[0]["parking_amount"].ToString();
                tbOtherExp.Text = dt.Rows[0]["other_exp_amount"].ToString();
                tbTotalExpenses.Text = dt.Rows[0]["total_expense"].ToString();
                decimal amt = Convert.ToDecimal(tbTotalEarningAmt.Text) - Convert.ToDecimal(tbTotalExpenses.Text);
                tbAmountDeposited.Text = amt.ToString();
            }
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
            ddl.Items.Insert(0, "SELECT");
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
            ddl.Items.Insert(0, "SELECT");
            ddl.Items[0].Value = "0";
            ddl.SelectedIndex = 0;
            _common.ErrorLog("DS mgmt-M4", ex.Message.ToString());
        }
    }
    private bool IsValidValues(TextBox passengerNo, TextBox amount, TextBox actualKmHill, TextBox actualKmPlain, int rowno)
    {
        try
        {
            int msgcnt = 0;
            string msg = "";
            if (amount.Text.ToString() == "")
            {
                msgcnt = msgcnt + 1;
                msg = msg + msgcnt.ToString() + ". Amount at row " + rowno.ToString() + " <br>";
            }
            if (passengerNo.Text.ToString() == "")
            {
                msgcnt = msgcnt + 1;
                msg = msg + msgcnt.ToString() + ". Passenger No at row " + rowno.ToString() + " <br>";
            }
            if (actualKmHill.Text.ToString() == "")
            {
                msgcnt = msgcnt + 1;
                msg = msg + msgcnt.ToString() + ". Actual Hill KM at row " + rowno.ToString() + " <br>";
            }
            if (actualKmPlain.Text.ToString() == "")
            {
                msgcnt = msgcnt + 1;
                msg = msg + msgcnt.ToString() + ". Actual Plain KM at row " + rowno.ToString() + " <br>";
            }
            //if (tbTollPaid.Text.ToString() == "")
            //{
            //	msgcnt = msgcnt + 1;
            //	msg = msg + msgcnt.ToString() + ". Toll Paid Amount<br>";
            //}
            //if (tbDhabaCollection.Text.ToString() == "")
            //{
            //	msgcnt = msgcnt + 1;
            //	msg = msg + msgcnt.ToString() + ". Dhaba Collection Amount<br>";
            //}
            //if (tbTotalAmt.Text.ToString() == "" || tbTotalAmt.Text.ToString() == "0")
            //{
            //	msgcnt = msgcnt + 1;
            //	msg = msg + msgcnt.ToString() + ". Total Amount<br>";
            //}
            if (msgcnt > 0)
            {
                Errormsg(msg);
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("AdmETMSubmission-M", ex.Message.ToString());
            return false;
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }
    private void saveValuesnew()
    {
        Session["_approveStatus"] = "A";
        decimal actual_km_sum = 0;
        decimal subjected_km_sum = 0;
        for (int i = 0; i < gvServiceTrips.Rows.Count; i++)
        {
            if ((gvServiceTrips.Rows[i].FindControl("tbActualKm") as TextBox).Text == "")
            {
                Errormsg("Please Enter Actual Km.");
                return;
            }
            String totalactual = (gvServiceTrips.Rows[i].FindControl("tbActualKm") as TextBox).Text;
            actual_km_sum += Convert.ToDecimal(totalactual);
            String totalsubjected = (gvServiceTrips.Rows[i].FindControl("tbSubKm") as TextBox).Text;
            subjected_km_sum += Convert.ToDecimal(totalsubjected);
        }
        if (subjected_km_sum != actual_km_sum)
        {
            Session["_approveStatus"] = "P";
        }
        decimal extrakm = actual_km_sum - subjected_km_sum;

        StringWriter swStringWriter = new StringWriter();
        using (swStringWriter)
        {
            var dt = getTable();
            dt.TableName = "timeTable";

            DataSet ds = new DataSet("Root");
            ds.Tables.Add(dt);

            ds.WriteXml(swStringWriter);
            string status = Session["_approveStatus"].ToString();

            string officeid = Session["_LDepotCode"].ToString();
            string waybillid = Session["_WAYBILLID"].ToString();
            string etmID = Session["_ETMID"].ToString();
            string UpdatedBy = Session["_UserCode"].ToString();
            string IpAddress = HttpContext.Current.Request.UserHostAddress;

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "etm.etm_submission");
            MyCommand.Parameters.AddWithValue("p_table", swStringWriter.ToString());
            MyCommand.Parameters.AddWithValue("p_busdepotcode", officeid);
            MyCommand.Parameters.AddWithValue("p_etmid", etmID);
            MyCommand.Parameters.AddWithValue("p_waybillno", waybillid);
            MyCommand.Parameters.AddWithValue("p_total_earning", Convert.ToDecimal(tbTotalEarningAmt.Text));
            MyCommand.Parameters.AddWithValue("p_total_expenses", Convert.ToDecimal(tbTotalExpenses.Text));
            MyCommand.Parameters.AddWithValue("p_status", status);
            MyCommand.Parameters.AddWithValue("p_updatedby", UpdatedBy);
            MyCommand.Parameters.AddWithValue("p_ipaddress", IpAddress);
            MyCommand.Parameters.AddWithValue("p_schedulekm", Convert.ToDecimal(subjected_km_sum));
            MyCommand.Parameters.AddWithValue("p_actual_km", Convert.ToDecimal(actual_km_sum));
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows[0]["p_refno"].ToString()== "DONE")
                {
                    pnlEtmSubmitDetails.Visible = false;
                    pnlWaybillDetails.Visible = true;
                    pnlAddDutySlips.Visible = true;
                    LoadWaybillsList();
                    LoadDashboardCounts();
                    loadSubmittedWaybills();
                    eDepositedSlip.Text = "<embed src = \"cashdepositslip.aspx\" style=\"min-height: 70vh; width: 100%\" />";
                    mpDepositedConductor.Show();
                }
                else if (dt.Rows[0]["p_refno"].ToString() == "Data Not Uploaded")
                {
                    Errormsg("Data Not Uploaded.");
                    return;
                }
                else if (dt.Rows[0]["p_refno"].ToString() == "ERROR")
                {
                    Errormsg("Error occurred while Submission. " + dt.TableName);
                    return;
                }
                else
                {
                    Errormsg("Error occurred while Submission. " + dt.TableName);
                    return;
                }
            }
            else
            {
                Errormsg("Error occurred while Updation. " + dt.TableName);
                return;
            }
        }


    }
    public void saveValues()
    {
        try
        {
            StringWriter swStringWriter = new StringWriter();
            using (swStringWriter)
            {
                var dt = getTable();
                dt.TableName = "timeTable";

                DataSet ds = new DataSet("Root");
                ds.Tables.Add(dt);

                ds.WriteXml(swStringWriter);
                string status = Session["_approveStatus"].ToString();

                string officeid = Session["_LDepotCode"].ToString();
                string etmbranchid = Session["_etmbranchid"].ToString();
                string waybillid = Session["_WAYBILLID"].ToString();
                string etmID = Session["_ETMID"].ToString();
                double totAmt, tollPaid, dhabaCharge;
                //	totAmt = Convert.ToDouble(tbTotalAmt.Text.ToString());
                //tollPaid = Convert.ToDouble(tbTollPaid.Text.ToString());
                //dhabaCharge = Convert.ToDouble(tbDhabaCollection.Text.ToString());
                string UpdatedBy = Session["_UserCode"].ToString();
                string IpAddress = HttpContext.Current.Request.UserHostAddress;

                MyCommand = new NpgsqlCommand();
                MyCommand.Parameters.Clear();
                MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_submit_etmdetails");
                MyCommand.Parameters.AddWithValue("p_table", swStringWriter.ToString());
                MyCommand.Parameters.AddWithValue("p_busdepotcode", officeid);
                MyCommand.Parameters.AddWithValue("p_etmbranchid", officeid);
                MyCommand.Parameters.AddWithValue("p_etmid", etmID);
                MyCommand.Parameters.AddWithValue("p_waybillno", waybillid);
                //	MyCommand.Parameters.AddWithValue("p_totalamt", totAmt);
                //	MyCommand.Parameters.AddWithValue("p_tollpaid", tollPaid);
                //	MyCommand.Parameters.AddWithValue("p_dhabacharge", dhabaCharge);
                MyCommand.Parameters.AddWithValue("p_status", status);
                MyCommand.Parameters.AddWithValue("p_updatedby", UpdatedBy);
                MyCommand.Parameters.AddWithValue("p_ipaddress", IpAddress);
                dt = bll.SelectAll(MyCommand);
                if (dt.TableName == "Success")
                {
                    if (Session["_approveStatus"].ToString() == "P")
                    {
                        Successmsg("ETM Details Submitted Successfully and Sent for Verification");
                    }
                    else
                    {
                        Successmsg("ETM Details Approved & Submitted Successfully");
                    }
                    pnlEtmSubmitDetails.Visible = false;
                    pnlWaybillDetails.Visible = true;
                    LoadWaybillsList();
                    //tbTollPaid.Text = "";
                    //tbDhabaCollection.Text = "";
                    //tbTotalAmt.Text = "";
                    LoadDashboardCounts();
                }
                else
                {
                    Errormsg("Error occurred while Updation. " + dt.TableName);
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            Errormsg("Error occurred while Updation. " + ex.Message);
        }
    }
    private void validateWaybill()
    {
        string UpdatedBy = Session["_UserCode"].ToString();
        string IpAddress = HttpContext.Current.Request.UserHostAddress;
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "etm.f_validate_waybill");
        MyCommand.Parameters.AddWithValue("p_waybill_no", Session["_WAYBILLID"].ToString());
        MyCommand.Parameters.AddWithValue("p_updatedby", UpdatedBy);
        MyCommand.Parameters.AddWithValue("p_ipaddress", IpAddress);
        dt = bll.SelectAll(MyCommand);

        if (dt.TableName == "Success")
        {
            if (dt.Rows[0]["status"].ToString() == "Done")
            {
                pnlEtmSubmitDetails.Visible = true;
                pnlAddDutySlips.Visible = false;
                pnlWaybillDetails.Visible = false;
                divtextBox.Visible = true;
                lbtnSave.Visible = true;
                LoadWaybillDetails();
            }
            else
            {
                Errormsg("Something Went Wrong");
            }
        }
        else
        {
            Errormsg("Something Went Wrong");
        }

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
            gvSubmittedWaybills.AllowPaging = false;
            this.LoadWaybillsList();
            gvSubmittedWaybills.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in gvSubmittedWaybills.HeaderRow.Cells)
                cell.BackColor = gvSubmittedWaybills.HeaderStyle.BackColor;
            foreach (GridViewRow row in gvSubmittedWaybills.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                        cell.BackColor = gvSubmittedWaybills.AlternatingRowStyle.BackColor;
                    else
                        cell.BackColor = gvSubmittedWaybills.RowStyle.BackColor;
                    cell.CssClass = "Textmode";
                }
            }

            gvSubmittedWaybills.RenderControl(hw);
            // style to format numbers to string
            string style = "<style> .Textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }
    private void ExportGridToExcel_waybills()
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
            gvWaybills.AllowPaging = false;
            this.LoadWaybillsList();
            gvWaybills.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in gvWaybills.HeaderRow.Cells)
                cell.BackColor = gvWaybills.HeaderStyle.BackColor;
            foreach (GridViewRow row in gvWaybills.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                        cell.BackColor = gvWaybills.AlternatingRowStyle.BackColor;
                    else
                        cell.BackColor = gvWaybills.RowStyle.BackColor;
                    cell.CssClass = "Textmode";
                }
            }

            gvWaybills.RenderControl(hw);
            // style to format numbers to string
            string style = "<style> .Textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }
    #endregion

    #region Events
    public DataTable getTable()
    {

        // Create a new DataTable.
        DataTable dtTimeTable = new DataTable("timeTable");
        dtTimeTable.Columns.Add("tripno", typeof(string));
        dtTimeTable.Columns.Add("subkm", typeof(string));
        dtTimeTable.Columns.Add("actkm", typeof(string));

        Int32 i = 0;
        foreach (GridViewRow row in gvServiceTrips.Rows)
        {
            int index = row.RowIndex;
            Label TripNo = (Label)gvServiceTrips.Rows[i].FindControl("lblTripNumber");
            TextBox SubKmHill = (TextBox)gvServiceTrips.Rows[i].FindControl("tbSubKm");
            TextBox ActualKmPlain = (TextBox)gvServiceTrips.Rows[i].FindControl("tbActualKm");

            dtTimeTable.Rows.Add(TripNo.Text, SubKmHill.Text, ActualKmPlain.Text);
            i = i + 1;
        }

        return dtTimeTable;
    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        //saveValues();
        saveValuesnew();
    }
    public void LoadDashboardCounts()
    {
        try
        {
            string officeid = Session["_LDepotCode"].ToString();
            Int32 etmbranchid = Convert.ToInt32(Session["_etmbranchid"].ToString());
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_dutyallocationcounts");
            MyCommand.Parameters.AddWithValue("p_officecode", "");
            MyCommand.Parameters.AddWithValue("p_depotcode", officeid);
            MyCommand.Parameters.AddWithValue("p_etmbranchid", etmbranchid);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    lblTotalETM.Text = dt.Rows[0]["spetmtotal"].ToString();
                    lblOnDutyETM.Text = dt.Rows[0]["spondutyetm"].ToString();
                    lblTotalETMSubmitted.Text = dt.Rows[0]["spsubmittedetmwaybill"].ToString();
                    lblETMSubmissionPending.Text = dt.Rows[0]["sppendingwaybilletm"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message);
        }
    }
    protected void gvWaybills_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "ValidateWaybill")
            {
                GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                int RowIndex = oItem.RowIndex;
                Session["_WAYBILLID"] = gvWaybills.DataKeys[RowIndex].Values["dutyrefno"];
                Session["_ETMID"] = gvWaybills.DataKeys[RowIndex].Values["etmrefno"];

                validateWaybill();
            }
            if (e.CommandName == "viewdutyslip")
            {
                GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                int RowIndex = oItem.RowIndex;
                Session["dutySlipRefno"] = gvWaybills.DataKeys[RowIndex].Values["dutyrefno"];
                //openSubDetailsWindow("DutySlip.aspx");
                eSlip.Text = "<embed src = \"DutySlip.aspx\" style=\"height: 80vh; width: 100%\" />";
                mpShowDutySlip.Show();
            }
            if (e.CommandName == "viewWaybill")
            {
                GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                int RowIndex = oItem.RowIndex;
                Session["DUTYREFNO"] = gvWaybills.DataKeys[index].Values["dutyrefno"].ToString();
                //openSubDetailsWindow("Waybill.aspx");
                eWaybill.Text = "<embed src = \"Waybill.aspx\" style=\"height: 80vh; width: 100%\" />";
                mpShowWaybill.Show();
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message);
            return;
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
    protected void gvWaybills_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //if (rowView["status_"].ToString() == "E" || rowView["status_"].ToString() == "G")
            //{
            //    DivCash.Visible = true;
            //    lblcash.Text = "Pending";
            //    lblcash.CssClass = "text-danger";
            //}

        }
    }
    protected void gvServiceTrips_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            //LinkButton lbtnETMSubmit = (LinkButton)e.Row.FindControl("lbtnETMSubmit");
            //LinkButton lbtnViewETM = (LinkButton)e.Row.FindControl("lbtnViewETM");
            //TextBox tbPassengerNo = (TextBox)e.Row.FindControl("tbPassengerNo");
            //TextBox tbAmount = (TextBox)e.Row.FindControl("tbAmount");
            //TextBox tbSubKmHill = (TextBox)e.Row.FindControl("tbSubKmHill");
            //TextBox tbSubKmPlain = (TextBox)e.Row.FindControl("tbSubKmPlain");
            //TextBox tbActualKmHill = (TextBox)e.Row.FindControl("tbActualKmHill");
            //TextBox tbActualKmPlain = (TextBox)e.Row.FindControl("tbActualKmPlain");
            //TextBox tbTollPaid = (TextBox)e.Row.FindControl("tbTollPaid");
            //TextBox tbDhabaCollection = (TextBox)e.Row.FindControl("tbDhabaCollection");
            //TextBox tbTotalAmt = (TextBox)e.Row.FindControl("tbTotalAmt");
            //Label lblPassengerNo = (Label)e.Row.FindControl("lblPassengerNo");
            //Label lblAmount = (Label)e.Row.FindControl("lblAmount");
            //Label lblSubKmHIll = (Label)e.Row.FindControl("lblSubKmHIll");
            //Label lblSubKmPlain = (Label)e.Row.FindControl("lblSubKmPlain");
            //Label lblActualKmHill = (Label)e.Row.FindControl("lblActualKmHill");
            //Label lblActualKmPlain = (Label)e.Row.FindControl("lblActualKmPlain");
            //Label lblTollPaid = (Label)e.Row.FindControl("lblTollPaid");
            //Label lblDhabaCollection = (Label)e.Row.FindControl("lblDhabaCollection");
            //Label lblTotalAmount = (Label)e.Row.FindControl("lblTotalAmount");
            //tbPassengerNo.Visible = false;
            //tbAmount.Visible = false;
            //tbSubKmHill.Visible = false;
            //tbSubKmPlain.Visible = false;
            //tbActualKmHill.Visible = false;
            //tbActualKmPlain.Visible = false;

            //if (ddlAllocationStatusType.SelectedValue == "N")
            //{
            //	//lbtnETMSubmit.Visible = true;
            //tbPassengerNo.Visible = true;
            //tbAmount.Visible = true;
            //tbSubKmHill.Visible = true;
            //tbSubKmPlain.Visible = true;
            //tbActualKmHill.Visible = true;
            //tbActualKmPlain.Visible = true;
            //}
            //else
            //{
            //	lblPassengerNo.Visible = true;
            //	lblAmount.Visible = true;
            //	lblSubKmHIll.Visible = true;
            //	lblSubKmPlain.Visible = true;
            //	lblActualKmHill.Visible = true;
            //	lblActualKmPlain.Visible = true;
            //}

        }

    }
    protected void gvSubmittedWaybills_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "viewcashslip")
        {
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            Session["_WAYBILLID"] = gvSubmittedWaybills.DataKeys[RowIndex].Values["dutyrefno"];
            eDepositedSlip.Text = "<embed src = \"cashdepositslip.aspx\" style=\"min-height: 70vh; width: 100%\" />";
            mpDepositedConductor.Show();
        }
        if (e.CommandName == "viewdutyslip")
        {
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            Session["dutySlipRefno"] = gvSubmittedWaybills.DataKeys[RowIndex].Values["dutyrefno"];
            //openSubDetailsWindow("DutySlip.aspx");
            eSlip.Text = "<embed src = \"DutySlip.aspx\" style=\"height: 80vh; width: 100%\" />";
            mpShowDutySlip.Show();
        }
        if (e.CommandName == "viewWaybill")
        {
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            Session["DUTYREFNO"] = gvSubmittedWaybills.DataKeys[index].Values["dutyrefno"].ToString();
            //openSubDetailsWindow("Waybill.aspx");
            eWaybill.Text = "<embed src = \"Waybill.aspx\" style=\"height: 80vh; width: 100%\" />";
            mpShowWaybill.Show();
        }
        if (e.CommandName == "ViewExpenditure")
        {
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            Session["DUTYREFNO"] = gvSubmittedWaybills.DataKeys[index].Values["dutyrefno"].ToString();
            eJourneyDetails.Text = "<embed src = \"dashEtmCollection.aspx\" style=\"height: 80vh; width: 100%\" />";
            mpExpenditureDetails.Show();

        }

    }
    protected void gvSubmittedWaybills_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HtmlGenericControl DivCash;
            DivCash = (HtmlGenericControl)e.Row.FindControl("divCashStatus") as HtmlGenericControl;
            DivCash.Visible = false;

            DataRowView rowView = (DataRowView)e.Row.DataItem;

            Label lblcash = (Label)e.Row.FindControl("lblCashStatus");


            if (rowView["status_"].ToString() == "E")
            {
                DivCash.Visible = true;
                lblcash.Text = "Pending";
                lblcash.CssClass = "text-danger";
            }
            if (rowView["status_"].ToString() == "C")
            {
                DivCash.Visible = true;
                lblcash.Text = "Deposited";
                lblcash.CssClass = "text-success";
            }
        }
    }
    protected void tbAmount_TextChanged(object sender, EventArgs e)
    {
        //	TextBox txtAmount = (TextBox)sender;

        //	Int32 i = 0;
        //	tbTotalAmt.Text = "0";
        //	foreach (GridViewRow row in this.gvServiceTrips.Rows)
        //	{
        //		TextBox tbAmt = (TextBox)gvServiceTrips.Rows[i].FindControl("tbAmount");
        //		tbTotalAmt.Text = (Convert.ToDouble(tbAmt.Text.ToString()) + Convert.ToDouble(tbTotalAmt.Text.ToString())).ToString();
        //		i = i + 1;
        //	}
        //	tbTotalAmt.Text = (Convert.ToDouble(tbTotalAmt.Text.ToString()) - Convert.ToDouble(tbTollPaid.Text.ToString()) - Convert.ToDouble(tbDhabaCollection.Text.ToString())).ToString();
    }
    protected void lbtnsearch_Click(object sender, EventArgs e)
    {
        LoadWaybillsList();
        loadSubmittedWaybills();
    }
    protected void lbtnResetFilter_Click(object sender, EventArgs e)
    {


        tbDutyDate.Text = "";

        ddlServiceType.SelectedValue = "0";
        ddlRoutes.SelectedValue = "0";
    }
    protected void lbtnDownloadExcel_Click(object sender, EventArgs e)
    {
        if (gvWaybills.Rows.Count > 0)
            ExportGridToExcel();
        else
            Errormsg("No Record Found");
    }
    protected void lbtnview_Click(object sender, EventArgs e)
    {
        string msg = "";
        msg = msg + "1. After completion of journey, trip details will be saved here.<br/>";
        msg = msg + "2. If there is any change in subjected and actual KM then it will be sent for verification to Station Incharge.<br/>";
        msg = msg + "3. If Station Incharge approve it then updated details will save and ETM mark as free.";
        InfoMsg(msg);
    }
    protected void lbtnCancel_Click(object sender, EventArgs e)
    {
        pnlEtmSubmitDetails.Visible = false;
        pnlAddDutySlips.Visible = true;
        pnlWaybillDetails.Visible = true;
    }
    protected void lbtnSave_Click(object sender, EventArgs e)
    {
        //Int32 i = 0;
        //foreach (GridViewRow row in gvServiceTrips.Rows)
        //{
        //	TextBox PassengerNo = (TextBox)gvServiceTrips.Rows[i].FindControl("tbPassengerNo");
        //	TextBox Amount = (TextBox)gvServiceTrips.Rows[i].FindControl("tbAmount");
        //	TextBox ActualKmHill = (TextBox)gvServiceTrips.Rows[i].FindControl("tbActualKmHill");
        //	TextBox ActualKmPlain = (TextBox)gvServiceTrips.Rows[i].FindControl("tbActualKmPlain");
        //	if (IsValidValues(PassengerNo, Amount, ActualKmHill, ActualKmPlain, i + 1) == false)
        //	{
        //		return;
        //	}
        //	i = i + 1;
        //}
        Session["_approveStatus"] = "SAVE";
        ConfirmMsg("Do you want to submit ETM details?");
    }
    protected void lbtnArchive_Click(object sender, EventArgs e)
    {
        eDash.Text = "<embed src = \"dashetmSubmission.aspx\" style=\"height: 80vh; width: 100%\" />";
        mpDuty.Show();
    }
    protected void lbtnDownload2_Click(object sender, EventArgs e)
    {
        ExportGridToExcel();
    }
    protected void lbtnDownload1_Click(object sender, EventArgs e)
    {
        ExportGridToExcel_waybills();
    }
    #endregion

}