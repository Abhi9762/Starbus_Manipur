using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_DieselFillEntry : System.Web.UI.Page
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    sbSecurity _security = new sbSecurity();
    string current_date = DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("yyyy");
    string current_date_10 = DateTime.Now.AddDays(-10).ToString("dd") + "/" + DateTime.Now.AddDays(-10).ToString("MM") + "/" + DateTime.Now.AddDays(-10).ToString("yyyy");
    private void checkForSecurity()
    {
        if (_security.isSessionExist(Session["_UserCode"]) == true)
        {
            Session["_UserCode"] = Session["_UserCode"];
        }
        else
        {
            Response.Redirect("../sessionTimeout.aspx");
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        checkForSecurity();             
        Session["_moduleName"] = "Diesel Fill Entry";
        if (IsPostBack == false)
        {
            tbFromDate.Text = current_date_10;
            tbToDate.Text = current_date;
            getTankWiseAvailability();
            loadPumpYn();
            lbtnNewBus.Visible = true;
            divWaybillDetails.Visible = true;
        }
        getLastFilledDieselDetails();
        getPendingWaybills();
        //loadDataTable(gvDefectsGroup);
        //ScriptManager.RegisterClientScriptBlock(this, GetType(), "cal", "loaddatepicker();", true);
       
    }

    #region Methods

    private void loadMappingPump()
    {
        try
        {
            string officeid = Session["_LDepotCode"].ToString();
            ddlMappingPump.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_mapped_pump");
            MyCommand.Parameters.AddWithValue("p_office", officeid);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlMappingPump.DataSource = dt;
                    ddlMappingPump.DataTextField = "pumpname";
                    ddlMappingPump.DataValueField = "pumpid";
                    ddlMappingPump.DataBind();
                }
            }
            else
            {
                Errormsg(dt.TableName);
            }
            ddlMappingPump.Items.Insert(0, "Select");
            ddlMappingPump.Items[0].Value = "0";
            ddlMappingPump.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlMappingPump.Items.Insert(0, "Select");
            ddlMappingPump.Items[0].Value = "0";
            ddlMappingPump.SelectedIndex = 0;
            Errormsg(ex.Message);
        }
    }
    public void loadPumpYn()//M3
    {
        try
        {
            string officeid = Session["_LDepotCode"].ToString();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_pumpyn");
            MyCommand.Parameters.AddWithValue("p_ofcid", officeid);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    Session["PumpStatus"] = "D"; //dt.Rows[0]["pump_yn"].ToString();
                }
                else
                {
                    Session["PumpStatus"] = "0";
                }
            }
            else
            {
                Errormsg(dt.TableName + "1");
            }
        }
        catch (Exception ex)
        {

            Errormsg(ex.Message.ToString());
        }
    }
    public void loadDataTable(GridView grd)
    {
        if (grd.Rows.Count > 0)
        {
            grd.UseAccessibleHeader = true;
            grd.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
    public void LoadFillingStation(DropDownList ddl)//M2
    {
        try
        {
            string officeid = Session["_LDepotCode"].ToString();
            ddl.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_tank_filling_station");
            MyCommand.Parameters.AddWithValue("p_officeid", officeid);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddl.DataSource = dt;
                    ddl.DataTextField = "fillingstn_name";
                    ddl.DataValueField = "fillingstn_id";
                    ddl.DataBind();
                }
            }
            else
            {
                _common.ErrorLog("DepAdminPumpMgmt-M2", dt.TableName);
                Errormsg(dt.TableName);
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
            _common.ErrorLog("DieselFill-M2", ex.Message.ToString());
        }
    }
    public void loadTank()//M3
    {
        try
        {
            Int32 fillingstn2 = Convert.ToInt32(ddlFillingStation.SelectedValue);
            string officeid = Session["_LDepotCode"].ToString();
            ddlTank.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_officetank");
            MyCommand.Parameters.AddWithValue("p_fillingstn", fillingstn2);
            MyCommand.Parameters.AddWithValue("p_office", officeid);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {

                if (dt.Rows.Count > 0)
                {
                    ddlTank.DataSource = dt;
                    ddlTank.DataTextField = "tank_no";
                    ddlTank.DataValueField = "tank_no";
                    ddlTank.DataBind();
                }
            }
            else
            {
                Errormsg(dt.TableName + "1");
            }

            ddlTank.Items.Insert(0, "SELECT");
            ddlTank.Items[0].Value = "0";
            ddlTank.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlTank.Items.Insert(0, "SELECT");
            ddlTank.Items[0].Value = "0";
            ddlTank.SelectedIndex = 0;
            _common.ErrorLog("DieselFill-M3", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
        }
    }
    public void getTankWiseAvailability()//M5
    {
        try
        {
            string officeid = Session["_LDepotCode"].ToString();
            gvTankAvaibility.Visible = false;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_tankwiseavailability");
            MyCommand.Parameters.AddWithValue("p_officeid", officeid);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    gvTankAvaibility.Visible = true;
                    gvTankAvaibility.DataSource = dt;
                    gvTankAvaibility.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("DieselFill-M5", ex.Message.ToString());
        }
    }
    public void getPendingWaybills()//M6
    {
        try
        {
            string officeid = Session["_LDepotCode"].ToString();
            pnlNoRecord.Visible = true;
            gvWayBillDue.Visible = false;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_getpendingwaybillfordiesel");
            MyCommand.Parameters.AddWithValue("p_officeid", officeid);
            MyCommand.Parameters.AddWithValue("p_searchtext", tbSearchWaybill.Text.ToString());
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    gvWayBillDue.DataSource = dt;
                    gvWayBillDue.DataBind();
                    gvWayBillDue.Visible = true;
                    pnlNoRecord.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("DieselFill-M6", ex.Message.ToString());
        }
    }
    public void getLastFilledDieselDetails()//M7
    {
        try
        {
            string status = "R";
            if (rbtnRunningBus.Checked == true)
            {
                status = "R";
            }
            else
            {
                status = "N";
            }

            string officeid = Session["_LDepotCode"].ToString();
            gvLastFilled.Visible = false;
            pnlNoLastFilled.Visible = true;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_lastfilldieseldetails");
            MyCommand.Parameters.AddWithValue("p_officeid", officeid);
            MyCommand.Parameters.AddWithValue("p_status", status);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    gvLastFilled.DataSource = dt;
                    gvLastFilled.DataBind();
                    pnlNoLastFilled.Visible = false;
                    gvLastFilled.Visible = true;
                }
            }
            else
            {
                Errormsg(dt.TableName);
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("DieselFill-M7", ex.Message.ToString());
        }
    }
    protected void lbtnViewDieselDetails_Click(object sender, EventArgs e)
    {
        pnlLastDieselFilled.Visible = true;
        pnlBusDieselEntry.Visible = false;
        pnlDefectsEntry.Visible = false;
        if (rbtnRunningBus.Checked == true)
        {
            ddlBusNewRunning.SelectedValue = "R";
        }
        else
        {
            ddlBusNewRunning.SelectedValue = "N";
        }
        //getAllFilledDieselDetails();
    }
    protected void gvLastFilled_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (rbtnRunningBus.Checked == true)
                gvLastFilled.Columns[1].Visible = true;
            else
                gvLastFilled.Columns[1].Visible = false;
        }
    }
    public void getAllFilledDieselDetails()//M8
    {
        try
        {
            string officeid = Session["_LDepotCode"].ToString();
            string busregno = tbBusNo.Text.ToString();
            string waybillno = tbWaybillNo.Text.ToString();
            string fromdate = tbFromDate.Text.ToString();
            string todate = tbToDate.Text.ToString();
            string status = ddlBusNewRunning.SelectedValue.ToString();
            gvFilledDieselDtls.Visible = false;
            pnlNoLastFilledDtls.Visible = true;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_allfilledieseldetails");
            MyCommand.Parameters.AddWithValue("p_depotcode", officeid);
            MyCommand.Parameters.AddWithValue("p_busregno", busregno);
            MyCommand.Parameters.AddWithValue("p_waybillno", waybillno);
            MyCommand.Parameters.AddWithValue("p_fromdate", fromdate);
            MyCommand.Parameters.AddWithValue("p_todate", todate);
            MyCommand.Parameters.AddWithValue("p_status", status);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    gvFilledDieselDtls.DataSource = dt;
                    gvFilledDieselDtls.DataBind();
                    pnlNoLastFilledDtls.Visible = false;
                    gvFilledDieselDtls.Visible = true;
                }
            }
            else
            {
                Errormsg(dt.TableName);
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("DieselFill-M8", ex.Message.ToString());
        }
    }
    public void loadpump(string tank, DropDownList ddlpump)//M9
    {
        try
        {
            string officeid = Session["_LDepotCode"].ToString();
            ddlpump.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_pump");
            MyCommand.Parameters.AddWithValue("p_tank_no", tank);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {

                if (dt.Rows.Count > 0)
                {
                    ddlpump.DataSource = dt;
                    ddlpump.DataTextField = "pump_no";
                    ddlpump.DataValueField = "pump_no";
                    ddlpump.DataBind();
                }
            }
            else
            {
                _common.ErrorLog("DepAdminPumpMgmt-M3", dt.TableName);
                Errormsg(dt.TableName);
            }

            ddlpump.Items.Insert(0, "SELECT");
            ddlpump.Items[0].Value = "0";
            ddlpump.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlpump.Items.Insert(0, "SELECT");
            ddlpump.Items[0].Value = "0";
            ddlpump.SelectedIndex = 0;
            _common.ErrorLog("DieselFill-M9", ex.Message.ToString());
        }
    }
    public void LoadOwnershipType()//M11
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
            ddlBusType.Items.Insert(0, "Select");
            ddlBusType.Items[0].Value = "0";
            ddlBusType.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlBusType.Items.Insert(0, "Select");
            ddlBusType.Items[0].Value = "0";
            ddlBusType.SelectedIndex = 0;
        }
    }
    public void LoadBuses()//M12
    {
        try
        {
            string officeid = Session["_LDepotCode"].ToString();
            ddlBus.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_buses_list");
            MyCommand.Parameters.AddWithValue("p_office", officeid);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlBus.DataSource = dt;
                    ddlBus.DataTextField = "busregistrationno";
                    ddlBus.DataValueField = "busregistrationno";
                    ddlBus.DataBind();
                }
            }
            else
            {
                _common.ErrorLog("DieselFillEntry-M", dt.TableName);
                Errormsg(dt.TableName);
            }
            ddlBus.Items.Insert(0, "SELECT");
            ddlBus.Items[0].Value = "0";
            ddlBus.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("DieselFill-M12", ex.Message.ToString());
        }
    }
    private void clickFunc()//M12
    {
        try
        {
            pnlDieselEntry.Visible = true;
            getWaybillDetails();
            pnlFirst.Visible = false;
            pnlBusDetails.Visible = true;
            divPvtFilling.Visible = true;
            //divDefectYN.Visible = true;
            divWaybillDetails.Visible = true;
            chkPvtStatn.Checked = false;
            getOdrDepotFuelDetails();
            tbODOMeterReading.Enabled = true;
            divBusDetails.Visible = false;
            divTotalKM.Visible = true;
            LoadFillingStation(ddlFillingStation);
            loadTank();
            loadpump(ddlTank.SelectedValue, ddlPump);
            getPendingWaybills();
        }
        catch (Exception ex)
        {
            _common.ErrorLog("DieselFill-M12", ex.Message.ToString());
        }
    }
    public void getWaybillDetails()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_dutywaybilldetails");
            MyCommand.Parameters.AddWithValue("p_waybillrefno", Session["waybillNo"].ToString());
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {

                lblServiceName.Text = dt.Rows[0]["servicename"].ToString();
                lblDutyTime.Text = dt.Rows[0]["deprttime"].ToString();
                // loadBuses()
                // ddlBus.SelectedValue = dt.Rows[0]["BUSNO").ToString
                // ddlBus.Enabled = False
                // Me.ddlBus_SelectedIndexChanged(Me.ddlBus, System.EventArgs.Empty)
                //txtODOMeter.Text = dt.Rows[0]["ODOMETERREADING"].ToString();
                lblWaybillBusNo.Text = dt.Rows[0]["busno"].ToString();
                lblWaybillBusType.Text = dt.Rows[0]["bustype"].ToString();
                Session["_BusType"] = dt.Rows[0]["bustypeid"].ToString();
                lblWaybillBusTypeId.Text = dt.Rows[0]["bustypeid"].ToString();
                lblWaybillBusNo2.Text = dt.Rows[0]["busno"].ToString();
                lblWaybillBusType2.Text = dt.Rows[0]["bustype"].ToString();
                lblDepot.Text = dt.Rows[0]["office_name"].ToString();
                lblWaybillOdometer.Text = dt.Rows[0]["odometerreading"].ToString();
                lblWaybillFinalOdometer.Text = dt.Rows[0]["lastodometerreading"].ToString();
                lblWaybillDriver1.Text = dt.Rows[0]["driver1name"].ToString();
                if (dt.Rows[0]["driver2name"].ToString() == "N/A")
                {
                    lblWaybillDriver1.Text = dt.Rows[0]["driver1name"].ToString();
                }
                else
                {
                    lblWaybillDriver1.Text = dt.Rows[0]["driver1name"].ToString() + " , " + dt.Rows[0]["driver2name"].ToString();
                }
                if (dt.Rows[0]["conductor2name"].ToString() == "N/A")
                {
                    lblWaybillConductor1.Text = dt.Rows[0]["conductor1name"].ToString();
                }
                else
                {
                    lblWaybillConductor1.Text = dt.Rows[0]["conductor1name"].ToString() + " , " + dt.Rows[0]["conductor2name"].ToString();
                }
                getOdrDepotFuelDetails();
                divWaybillDetails.Visible = true;
                pnlBusDetails.Visible = true;
                pnlFirst.Visible = false;

                if (Session["_LDepotCode"].ToString() == dt.Rows[0]["depot"].ToString())
                {
                    //divDefectYN.Visible = true;
                    divPvtFilling.Visible = true;
                    divOtherDepotDetails.Visible = false;
                }
            }
            else
            {
                pnlBusDetails.Visible = false;
                pnlFirst.Visible = true;
                Errormsg("Please enter correct Waybill No");
            }
        }
        catch (Exception ex)
        {
            Errormsg("Please check" + ex.Message);
        }
    }
    private bool IsValidValues()
    {
        try
        {
            string msg = "";
            int msgcnt = 0;
            //if (Session["_busNewRegular"].ToString() == "R")
            //{
            //    if (ddlWaybills.SelectedValue == "0")
            //    {
            //        msgcnt = msgcnt + 1;
            //        msg = msg + msgcnt.ToString() + ". WayBill Number.<br>";
            //    }
            //    if (lblWaybillBusTypeId.Text == "")
            //    {
            //        msgcnt = msgcnt + 1;
            //        msg = msg + msgcnt.ToString() + ". Select Bus Type.<br>";
            //    }
            //}
            //if (Session["_busNewRegular"].ToString() == "N")
            //{
            //    if (ddlBus.SelectedValue == "0")
            //    {
            //        msgcnt = msgcnt + 1;
            //        msg = msg + msgcnt.ToString() + ". Select Bus.<br>";
            //    }

            //    if (_validation.IsValidString(tbODOMeterReading.Text.Trim(), 1, tbODOMeterReading.MaxLength) == false)
            //    {
            //        msgcnt = msgcnt + 1;
            //        msg = msg + msgcnt.ToString() + ". Odometer Number.<br>";
            //    }
            //}
            if (Session["_busNewRegular"].ToString() == "N")
            {
                if (ddlBus.SelectedValue == "0")
                {
                    msgcnt = msgcnt + 1;
                    msg = msg + msgcnt.ToString() + ". Select Bus.<br>";
                }
                if (ddlBusType.SelectedValue == "0")
                {
                    msgcnt = msgcnt + 1;
                    msg = msg + msgcnt.ToString() + ". Select Bus Type.<br>";
                }
                if (tbODOMeterReading.Text == "")
                {
                    msgcnt = msgcnt + 1;
                    msg = msg + msgcnt.ToString() + ". Enter ODO Meter Reading.<br>";
                }
                if (tbtotalKm.Text == "")
                {
                    msgcnt = msgcnt + 1;
                    msg = msg + msgcnt.ToString() + ". Enter Total Km.<br>";
                }
            }
            if (chkOwnFilling.Checked == true)
            {
                if (ddlFillingStation.SelectedValue == "0")
                {
                    msgcnt = msgcnt + 1;
                    msg = msg + msgcnt.ToString() + ". Select Filling Station.<br>";
                }
                if (ddlTank.SelectedValue == "0")
                {
                    msgcnt = msgcnt + 1;
                    msg = msg + msgcnt.ToString() + ". Select Tank.<br>";
                }
                if (ddlPump.SelectedValue == "0")
                {
                    msgcnt = msgcnt + 1;
                    msg = msg + msgcnt.ToString() + ". select Pump Number.<br>";
                }
                if (_validation.IsValidString(tbPumpReading.Text.Trim(), 1, tbPumpReading.MaxLength) == false)
                {
                    msgcnt = msgcnt + 1;
                    msg = msg + msgcnt.ToString() + ". Enter Pump Reading.<br>";
                }
                if (tbFuelQuantity.Text == "")
                {
                    msgcnt = msgcnt + 1;
                    msg = msg + msgcnt.ToString() + ". Enter Fuel Quantity.<br>";
                }
                else if (_validation.isValideDecimalNumber(tbFuelQuantity.Text.Trim(), 1, tbFuelQuantity.MaxLength) == false)
                {
                    msgcnt = msgcnt + 1;
                    msg = msg + msgcnt.ToString() + ". Enter  Decimal Values Only(Fuel Quantity).<br>";
                }
            }
            if (chkMappingFilling.Checked == true)
            {
                if (tbdreceiptno.Text == "")
                {
                    msgcnt = msgcnt + 1;
                    msg = msg + msgcnt.ToString() + ".Enter Mapped Pump Receipt Number.<br>";
                }
                if (tbdquantity.Text == "")
                {
                    msgcnt = msgcnt + 1;
                    msg = msg + msgcnt.ToString() + ".Enter Fuel Quantity at Mapped Station.<br>";
                }
                else if (_validation.isValideDecimalNumber(tbdquantity.Text.Trim(), 1, tbdquantity.MaxLength) == false)
                {
                    msgcnt = msgcnt + 1;
                    msg = msg + msgcnt.ToString() + ". Enter Decimal Values Only (Fuel Quantity at Mapped Station).<br>";
                }
                if (tbdamtpaid.Text == "")
                {
                    msgcnt = msgcnt + 1;
                    msg = msg + msgcnt.ToString() + ".Enter Amount Paid at Mapped Station.<br>";
                }
                else if (_validation.isValideDecimalNumber(tbdamtpaid.Text.Trim(), 1, tbdamtpaid.MaxLength) == false)
                {
                    msgcnt = msgcnt + 1;
                    msg = msg + msgcnt.ToString() + ". Enter Decimal Values Only (Amount Paid at Mapped Station).<br>";
                }
                if (ddlMappingPump.SelectedValue == "0")
                {
                    msgcnt = msgcnt + 1;
                    msg = msg + msgcnt.ToString() + ". Select Mapping Station.<br>";
                }
            }
            if (chkPvtStatn.Checked == true)
            {

                if (_validation.IsValidString(tbPvtPumpReceiptNo.Text.Trim(), 1, tbPvtPumpReceiptNo.MaxLength) == false)
                {
                    msgcnt = msgcnt + 1;
                    msg = msg + msgcnt.ToString() + ". Enter Private Pump Receipt Number.<br>";
                }
                if (tbPvtFilledQty.Text == "")
                {
                    msgcnt = msgcnt + 1;
                    msg = msg + msgcnt.ToString() + ".Enter Fuel Quantity at Private Station.<br>";
                }
                else if (_validation.isValideDecimalNumber(tbPvtFilledQty.Text.Trim(), 1, tbPvtFilledQty.MaxLength) == false)
                {
                    msgcnt = msgcnt + 1;
                    msg = msg + msgcnt.ToString() + ". Enter Decimal Values Only (Fuel Quantity at Private Station).<br>";
                }
                if (tbPvtPumpAmtPaid.Text == "")
                {
                    msgcnt = msgcnt + 1;
                    msg = msg + msgcnt.ToString() + ".Enter Amount Paid at Private Station.<br>";
                }
                else if (_validation.isValideDecimalNumber(tbPvtPumpAmtPaid.Text.Trim(), 1, tbPvtPumpAmtPaid.MaxLength) == false)
                {
                    msgcnt = msgcnt + 1;
                    msg = msg + msgcnt.ToString() + ". Enter Decimal Values Only (Amount Paid at Private Station).<br>";
                }
                if (_validation.IsValidString(tbPvtPumpName.Text.Trim(), 1, tbPvtPumpName.MaxLength) == false)
                {
                    msgcnt = msgcnt + 1;
                    msg = msg + msgcnt.ToString() + ". Enter Private Pump Name.<br>";
                }
                if (_validation.IsValidAddress(tbPvtPumpAddress.Text.Trim(), 1, tbPvtPumpAddress.MaxLength) == false)
                {
                    msgcnt = msgcnt + 1;
                    msg = msg + msgcnt.ToString() + ". Enter Private Pump Address.<br>";
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
    public void SaveValues()//M13
    {
        try
        {
            if (chkOwnFilling.Checked == false && chkMappingFilling.Checked == false && chkPvtStatn.Checked == false)
            {
                Errormsg("Select Fuel Station");
                return;
            }

            StringWriter swStringWriter = new StringWriter();
            using (swStringWriter)
            {
                DataTable dt = null/* TODO Change to default(_) if this is not a reference type */;
                DataSet ds = new DataSet("root");
                if (ViewState["Defects"] != null)
                {
                    dt = (DataTable)ViewState["Defects"];
                    dt.TableName = "timeTable";
                    ds.Tables.Add(dt);
                }
                ds.WriteXml(swStringWriter);

                //Session["_BusType"] = "1";
                string officeid = Session["_LDepotCode"].ToString();
                string busCondtion = Session["_busNewRegular"].ToString();
                string busNo = lblWaybillBusNo.Text;
                string busType = "";
                string odometerReading = lblWaybillFinalOdometer.Text;
                if (lblWaybillFinalOdometer.Text == "")
                {
                    odometerReading = tbODOMeterReading.Text.ToString();
                }
                string waybillno = "";
                if (busCondtion == "N")
                {
                    busCondtion = "New";
                    busNo = ddlBus.SelectedValue;
                    busType = ddlBusType.SelectedValue;
                    odometerReading = tbODOMeterReading.Text.ToString();
                }
                else
                {
                    busType = Session["_BusType"].ToString();
                    waybillno = Session["waybillNo"].ToString();
                    busCondtion = "Running";
                }
                string defectYN = "N";
                string filledAtType = Session["PumpStatus"].ToString();

                int fillingst = Convert.ToInt16(ddlFillingStation.SelectedValue);
                string tankno = ddlTank.SelectedValue.ToString();
                string pumpno = ddlPump.SelectedValue.ToString();
                string pumpreading = Convert.ToString((Convert.ToInt32(tbPumpReading.Text) + Convert.ToInt32(tbFuelQuantity.Text)));
                string qtyfilled = tbFuelQuantity.Text.ToString();
                string fillingDate = DateTime.Now.ToString("dd/MM/yyyy");
                string pvtpumpname = tbPvtPumpName.Text.ToString();
                string pvtpumpaddress = tbPvtPumpAddress.Text.ToString();
                string pvtpumprecno = tbPvtPumpReceiptNo.Text.ToString();
                string pvtpumpamtpaid = tbPvtPumpAmtPaid.Text.ToString();
                string pvtpumpqty = tbPvtFilledQty.Text.ToString();
                string ipaddress = HttpContext.Current.Request.UserHostAddress;
                string UpdatedBy = Session["_UserCode"].ToString();

                string depot_pump = "N";
                string private_mapped_pump = "N";
                string private_pump = "N";
                Int32 dpump_id = Convert.ToInt32(ddlMappingPump.SelectedValue);
                string dpvtpumpreceiptno = tbdreceiptno.Text;
                string dpvtpumpamtpaid = tbdamtpaid.Text;
                string dpvtpumpqty = tbdquantity.Text;
                if (chkOwnFilling.Checked == true)
                {
                    depot_pump = "Y";
                }
                else
                {
                    depot_pump = "N";
                    fillingst = 0;
                    tankno = "";
                    pumpno = "";
                    pumpreading = "0";
                    qtyfilled = "0";
                }
                if (chkMappingFilling.Checked == true)
                {
                    private_mapped_pump = "Y";
                }
                else
                {
                    private_mapped_pump = "N";
                    dpvtpumpqty = "0";
                    dpvtpumpamtpaid = "0";
                    dpvtpumpreceiptno = "";
                    dpump_id = 0;
                }
                if (chkPvtStatn.Checked == true)
                {
                    private_pump = "Y";
                }
                else
                {
                    private_pump = "N";
                    pvtpumpname = "";
                    pvtpumpaddress = "";
                    pvtpumprecno = "";
                    pvtpumpamtpaid = "0";
                    pvtpumpqty = "0";
                }

                NpgsqlCommand MyCommand = new NpgsqlCommand();
                MyCommand.Parameters.Clear();
                //MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_insert_dieselfilldetails"); with jobcard_s_new1(defect list)
                //MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_insert_dieselfilldetails_new");
                MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_insert_dieselfilldetails_new1");
                MyCommand.Parameters.AddWithValue("p_officecode", officeid);
                MyCommand.Parameters.AddWithValue("p_waybillno", waybillno);
                MyCommand.Parameters.AddWithValue("p_busregno", busNo);
                MyCommand.Parameters.AddWithValue("p_bustype", busType);
                MyCommand.Parameters.AddWithValue("p_buscondition", busCondtion);
                MyCommand.Parameters.AddWithValue("p_fillingdate", fillingDate);
                MyCommand.Parameters.AddWithValue("p_filledattype", filledAtType);
                MyCommand.Parameters.AddWithValue("p_odometerreading", odometerReading);
                MyCommand.Parameters.AddWithValue("p_fillingstation", fillingst);
                MyCommand.Parameters.AddWithValue("p_tankno", tankno);
                MyCommand.Parameters.AddWithValue("p_pumpno", pumpno);
                MyCommand.Parameters.AddWithValue("p_pumpreading", pumpreading);
                MyCommand.Parameters.AddWithValue("p_qtyfilled", qtyfilled);
                MyCommand.Parameters.AddWithValue("p_filledbyempcode", UpdatedBy);
                MyCommand.Parameters.AddWithValue("p_pvtpumpname", pvtpumpname);
                MyCommand.Parameters.AddWithValue("p_pvtpumpaddress", pvtpumpaddress);
                MyCommand.Parameters.AddWithValue("p_pvtpumpreceiptno", pvtpumprecno);
                MyCommand.Parameters.AddWithValue("p_pvtpumpamtpaid", pvtpumpamtpaid);
                MyCommand.Parameters.AddWithValue("p_pvtpumpqty", pvtpumpqty);
                //MyCommand.Parameters.AddWithValue("p_defectstatus", defectYN);
                //MyCommand.Parameters.AddWithValue("p_defectslist", swStringWriter.ToString());
                MyCommand.Parameters.AddWithValue("p_updatedby", ipaddress);
                MyCommand.Parameters.AddWithValue("p_ipaddress", ipaddress);
                MyCommand.Parameters.AddWithValue("p_dpump_id", dpump_id);
                MyCommand.Parameters.AddWithValue("p_dpvtpumpreceiptno", dpvtpumpreceiptno);
                MyCommand.Parameters.AddWithValue("p_dpvtpumpamtpaid", dpvtpumpamtpaid);
                MyCommand.Parameters.AddWithValue("p_dpvtpumpqty", dpvtpumpqty);
                MyCommand.Parameters.AddWithValue("p_depot_pump", depot_pump);
                MyCommand.Parameters.AddWithValue("p_private_mapped_pump", private_mapped_pump);
                MyCommand.Parameters.AddWithValue("p_private_pump", private_pump);

                dt = bll.SelectAll(MyCommand);
                if (dt.TableName == "Success")
                {
                    Successmsg("Diesel Fill Entry Details updated successfully");
                    ResetValue();
                    getLastFilledDieselDetails();
                    getPendingWaybills();
                    //lbtnNewBus.Visible = true;
                }
                else
                {
                    _common.ErrorLog("DepDieselFillEntry-m", dt.TableName);
                    Errormsg(dt.TableName);
                }
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("DieselFill-M13", ex.Message.ToString());
        }
    }
    private void ResetValue()//M14
    {
        try
        {
            LoadFillingStation(ddlFillingStation);
            loadTank();
            LoadBuses();
            loadpump(ddlTank.SelectedValue, ddlPump);
            lbtnNewBus.Visible = true;
            ddlBus.SelectedValue = "0";
            ddlBus.Enabled = true;
            ddlBusType.Enabled = true;
            tbODOMeterReading.ReadOnly = false;
            ddlBusType.SelectedValue = "0";
            ddlBusType.Enabled = true;
            tbODOMeterReading.Text = "0";
            tbPvtPumpAddress.Text = "";
            tbPvtPumpAmtPaid.Text = "";
            tbPvtPumpName.Text = "";
            tbPvtPumpReceiptNo.Text = "";
            tbPvtFilledQty.Text = "";
            tbPumpReading.Text = "";
            ddlTank.SelectedValue = "0";
            ddlPump.SelectedValue = "0";
            tbFuelQuantity.Text = "";
            ddlFillingStation.SelectedValue = "0";
            ViewState["Defects"] = null;
            ViewState["DefectsNew"] = null;
            loadDefectGroupItems(0);
            rptrMarkedDefects.DataSource = null;
            rptrMarkedDefects.DataBind();
            pnlMarkedDefects.Visible = true;
            pnlFirst.Visible = true;
            pnlBusDetails.Visible = false;
            chkPvtStatn.Checked = false;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("DieselFill-M14", ex.Message.ToString());
        }
    }
    private void ExportToExcel()
    {
        Response.Clear();
        Response.Buffer = true;
        gvFilledDieselDtls.AllowPaging = false;
        this.getAllFilledDieselDetails();
        if (gvFilledDieselDtls.Rows.Count > 0)
        {
            Response.AddHeader("content-disposition", "attachment;filename=DieselDetails.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                // To Export all pages

                gvFilledDieselDtls.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in gvFilledDieselDtls.HeaderRow.Cells)
                    cell.BackColor = gvFilledDieselDtls.HeaderStyle.BackColor;
                foreach (GridViewRow row in gvFilledDieselDtls.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                            cell.BackColor = gvFilledDieselDtls.AlternatingRowStyle.BackColor;
                        else
                            cell.BackColor = gvFilledDieselDtls.RowStyle.BackColor;
                        cell.CssClass = "textmode";
                    }
                }

                gvFilledDieselDtls.RenderControl(hw);
                // style to format numbers to string
                string style = "<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
        else
        {
            Errormsg("Sorry, no record is available.");
            return;
        }
    }
    private void InfoMsg(string msg)
    {
        string popup = _popup.modalPopupSmall("I", "Information", msg, "Close");
        Response.Write(popup);
    }
    private void ConfirmMsg(string msg)
    {
        lblConfirmation.Text = msg;
        mpConfirmation.Show();
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
    #endregion

    #region Events
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        SaveValues();
    }
    protected void lbtnsearch_Click(object sender, EventArgs e)
    {
        getAllFilledDieselDetails();
    }
    protected void lbtnview_Click(object sender, EventArgs e)
    {
    }
    protected void lbtnNewBus_Click(object sender, EventArgs e)
    {
        Session["PumpStatus"] = "B";
        Session["_busNewRegular"] = "N";
        Session["waybillNo"] = "";
        LoadBuses();
        LoadFillingStation(ddlFillingStation);
        loadTank();
        loadpump(ddlTank.SelectedValue, ddlPump);
        LoadOwnershipType();
        loadMappingPump();
        // ResetValue();
        if (Session["PumpStatus"].ToString() == "D")
        {
            lbtnNewBus.Visible = false;
            divWaybillDetails.Visible = false;
            divBusDetails.Visible = true;
            divTotalKM.Visible = true;
            divPvtFilling.Visible = false;
            //divDefectYN.Visible = false;
            pnlFirst.Visible = false;
            pnlBusDetails.Visible = true;
            divPumpMapping.Visible = false;
            divDepotFilling.Visible = true;
            chkOwnFilling.Checked = true;
            chkOwnFilling.Enabled = false;
            divdepotfillingfield.Visible = true;
        }
        if (Session["PumpStatus"].ToString() == "P" || Session["PumpStatus"].ToString() == "0")
        {
            lbtnNewBus.Visible = false;
            divWaybillDetails.Visible = false;
            divBusDetails.Visible = true;
            divTotalKM.Visible = true;
            divPvtFilling.Visible = false;
            //divDefectYN.Visible = false;
            pnlFirst.Visible = false;
            pnlBusDetails.Visible = true;
            divDepotFilling.Visible = false;
            divPumpMapping.Visible = true;
            chkOwnFilling.Checked = false;
            chkMappingFilling.Checked = true;
            chkMappingFilling.Enabled = false;
            divMappingFields.Visible = true;
        }
        if (Session["PumpStatus"].ToString() == "B")
        {
            lbtnNewBus.Visible = false;
            divWaybillDetails.Visible = false;
            divBusDetails.Visible = true;
            divTotalKM.Visible = true;
            divPvtFilling.Visible = false;
            //divDefectYN.Visible = false;
            pnlFirst.Visible = false;
            pnlBusDetails.Visible = true;
            divPumpMapping.Visible = true;
            divDepotFilling.Visible = true;
            //divPvtFilling.Visible = true;
        }
    }



    protected void chkMappingFilling_CheckedChanged(object sender, EventArgs e)
    {
        if (chkMappingFilling.Checked == true)
        {
            divMappingFields.Visible = true;

        }
        else
        {
            divMappingFields.Visible = false;

        }
    }
    protected void chkOwnFilling_CheckedChanged(object sender, EventArgs e)
    {

        if (chkOwnFilling.Checked == true)
        {
            divdepotfillingfield.Visible = true;

        }
        else
        {
            divdepotfillingfield.Visible = false;

        }
    }
    protected void ddlPump_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_tankpumpreading");
            MyCommand.Parameters.AddWithValue("p_tankno", ddlTank.SelectedValue);
            MyCommand.Parameters.AddWithValue("p_pumpno", ddlPump.SelectedValue);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                tbPumpReading.Text = dt.Rows[0]["pumpreading"].ToString();
            }
        }
        catch (Exception ex)
        {
        }
    }

    public void ddlBus_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string busregno = ddlBus.SelectedValue.ToString();
            ddlBusType.Enabled = true;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_busownertype");
            MyCommand.Parameters.AddWithValue("p_busregno", busregno);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlBusType.SelectedValue = dt.Rows[0]["ownertype"].ToString();
                    ddlBusType.Enabled = false;
                    tbODOMeterReading.Enabled = true;
                    tbODOMeterReading.Text = dt.Rows[0]["initodometerreading"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            ddlBusType.SelectedIndex = 0;
        }
    }
    protected void lbtnBacktoEntry_Click(object sender, EventArgs e)
    {
        pnlLastDieselFilled.Visible = false;
        divWaybillDetails.Visible = false;
        pnlBusDetails.Visible = false;
        lbtnNewBus.Visible = true;
        // getWaybillsListforDieselEntry();
        pnlBusDieselEntry.Visible = true;
        pnlFirst.Visible = true;
    }
    protected void ddlFillingStation_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadTank();

    }
    protected void ddlTank_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadpump(ddlTank.SelectedValue, ddlPump);

    }
    protected void lbtnHelp_Click(object sender, EventArgs e)
    {
        string msg = "";
        msg = msg + "1. Select depot for which you want to map bus with service.<br/>";
        msg = msg + "2. If you are entering diesel details for same bus of depot as login depot then you can also enter private filling details.<br/>";
        msg = msg + "3. If diesel is also filled at other depots then that detail is also seen in the grid.<br/>";
        msg = msg + "4. You can also mark defects of bus during running bus diesel entry.<br/>";
        msg = msg + "5. Pending waybill buses list and last filled diesel details is also shown at the left panel.<br/>";
        InfoMsg(msg);
    }
    protected void lbtnSave_Click(object sender, EventArgs e)
    {
        if (IsValidValues() == false)
        {
            return;
        }
        ConfirmMsg("Do you want to save Diesel Fill Details?");
    }
    protected void lbtnReset_Click(object sender, EventArgs e)
    {
        ResetValue();
    }
    protected void gvWayBillDue_RowCommand(object sender, GridViewCommandEventArgs e)
    {
 try
        {

            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "pendingWaybill")
            {
                GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                int i = oItem.RowIndex;
                string DUTYREFNO = gvWayBillDue.DataKeys[i]["dutyrefno"].ToString();
                Session["waybillNo"] = DUTYREFNO;
                Session["_busNewRegular"] = "R";
                pnlFirst.Visible = true;
                divWaybillDetails.Visible = true;
                pnlBusDetails.Visible = false;
                lbtnNewBus.Visible = true;
                clickFunc();

                Session["PumpStatus"] = "B";
                LoadBuses();
                LoadFillingStation(ddlFillingStation);
                loadTank();
                loadpump(ddlTank.SelectedValue, ddlPump);
                LoadOwnershipType();
                loadMappingPump();
                // ResetValue();
                if (Session["PumpStatus"].ToString() == "D")
                {
                    
                    divPumpMapping.Visible = false;
                    divDepotFilling.Visible = true;
                    chkOwnFilling.Checked = true;
                    chkOwnFilling.Enabled = false;
                    divdepotfillingfield.Visible = true;
                }
                if (Session["PumpStatus"].ToString() == "P" || Session["PumpStatus"].ToString() == "0")
                {
                    divDepotFilling.Visible = false;
                    divPumpMapping.Visible = true;
                    chkOwnFilling.Checked = false;
                    chkMappingFilling.Checked = true;
                    chkMappingFilling.Enabled = false;
                    divMappingFields.Visible = true;
                }
                if (Session["PumpStatus"].ToString() == "B")
                {
                    pnlFirst.Visible = false;
                    pnlBusDetails.Visible = true;
                    divPumpMapping.Visible = true;
                    divDepotFilling.Visible = true;
                    
                }
            }
	else
            {
                Errormsg("sdfv");
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message);
        }
    }
    protected void lnkbtnSearch_Click(object sender, EventArgs e)
    {
        //if (ddlWaybills.SelectedValue == "0")
        //{
        //    //Reset();
        //    Errormsg("Please enter correct Waybill No");
        //    return;
        //}
        clickFunc();
    }
    protected void lbtnAddDefects_Click(object sender, EventArgs e)
    {
        pnlDefectsEntry.Visible = true;
        pnlDieselEntry.Visible = false;
        getDefectsList();
        loadDefectGroupItems(0);
        DataTable dt = (DataTable)ViewState["Defects"];
        if (dt.Rows.Count > 0)
        {
            rptrMarkedDefects.DataSource = dt;
            rptrMarkedDefects.DataBind();
        }
    }
    protected void chkDefectsYN_CheckedChanged(object sender, EventArgs e)
    {
        getDefectsList();
        if (chkDefectsYN.Checked == true)
        {
            pnlDefectsEntry.Visible = true;
            pnlDieselEntry.Visible = false;
        }
        else
        {
            pnlDefectsEntry.Visible = false;
            pnlDieselEntry.Visible = true;
            ViewState["Defects"] = null;
            ViewState["DefectsNew"] = null;
            loadDefectGroupItems(0);
            rptrMarkedDefects.DataSource = null;
            rptrMarkedDefects.DataBind();
            pnlMarkedDefects.Visible = true;
        }
    }
    protected void chkDefectItems_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            pnlMarkedDefects.Visible = true;
            if (ViewState["Defects"] == null)
            {
                DataTable dt1 = new DataTable("timeTable");
                dt1.Columns.Add("groupID", typeof(Int32));
                dt1.Columns.Add("DefectID", typeof(Int32));
                dt1.Columns.Add("itemName", typeof(string));
                ViewState["DefectsNew"] = dt1;
            }
            else
                ViewState["DefectsNew"] = (DataTable)ViewState["Defects"];

            DataTable dt = (DataTable)ViewState["DefectsNew"];
            foreach (ListItem item in chkDefectItems.Items)
            {
                if (item.Selected)
                {
                    item.Enabled = false;
                    dt.Rows.Add(Session["_groupID"], item.Value, item.Text);
                }
            }
            dt = dt.DefaultView.ToTable(true, "groupID", "DefectID", "itemName");
            if (dt.Rows.Count > 0)
            {
                ViewState["Defects"] = dt;
                rptrMarkedDefects.DataSource = dt;
                rptrMarkedDefects.DataBind();
                pnlMarkedDefects.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message);
        }
    }
    protected void gvDefectsGroup_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "markDefects")
        {
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int i = oItem.RowIndex;
            Int32 groupID = Convert.ToInt32(gvDefectsGroup.DataKeys[i]["RPGP_ID"]);
            Session["_groupID"] = groupID;
            DefectsHead.Text = "Select Defects of " + gvDefectsGroup.DataKeys[i]["GROUP_DESC_EN"];
            getDefectsList();
            loadDefectGroupItems(groupID);
        }
    }
    protected void chkPvtStatn_CheckedChanged(object sender, EventArgs e)
    {
        divPvtDetails.Visible = false;
        if (chkPvtStatn.Checked == true)
        {
            divPvtDetails.Visible = true;
        }
    }
    protected void lbtnBack_Click(object sender, EventArgs e)
    {
        pnlDefectsEntry.Visible = false;
        pnlDieselEntry.Visible = true;
        divMarkedDefects.Visible = false;
        //divDefectYN.Visible = true;
        if (ViewState["Defects"] != null)
        {
            DataTable dt = (DataTable)ViewState["Defects"];
            if (dt.Rows.Count > 0)
            {
                divMarkedDefects.Visible = true;
                rptrMarkedDefects2.DataSource = dt;
                rptrMarkedDefects2.DataBind();
            }
            else
                chkDefectsYN.Checked = false;
        }
        else
            chkDefectsYN.Checked = false;
    }
    protected void lbtnResetFilter_Click(object sender, EventArgs e)
    {
        tbWaybillNo.Text = "";
        tbBusNo.Text = "";
        getAllFilledDieselDetails();
    }
    protected void lbtnDownloadExcel_Click(object sender, EventArgs e)
    {
        ExportToExcel();
    }
    #endregion

    protected void rptrMarkedDefects2_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "deleteDefect")
        {
            Label lblGroupID = (Label)e.Item.FindControl("lblGroupID");
            Label lblDefectID = (Label)e.Item.FindControl("lblDefectID");
            Int32 rowID = e.Item.ItemIndex;
            if (ViewState["Defects"] != null)
            {
                DataTable dt = (DataTable)ViewState["Defects"];
                dt.Rows.Remove(dt.Rows[rowID]);
                ViewState["Defects"] = dt;
                rptrMarkedDefects2.DataSource = dt;
                rptrMarkedDefects2.DataBind();
            }
        }
    }

    protected void getOdrDepotFuelDetails()
    {
        try
        {
            gvFuelFilledOtherDepots.Visible = false;
            pnlNoOtherDepotRecord.Visible = true;

            string officeid = Session["_LDepotCode"].ToString();
            string dutyrefno = Session["waybillNo"].ToString();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_odrdepotfueldetails");
            MyCommand.Parameters.AddWithValue("p_dutyrefno", dutyrefno);
            MyCommand.Parameters.AddWithValue("p_depotcode", officeid);
            DataTable dt2 = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                gvFuelFilledOtherDepots.DataSource = dt2;
                gvFuelFilledOtherDepots.DataBind();
                gvFuelFilledOtherDepots.Visible = true;
                pnlNoOtherDepotRecord.Visible = false;
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("User Management-E1", ex.Message.ToString());

            gvFuelFilledOtherDepots.Visible = false;

        }
    }
    protected void getDefectsList()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_defectsgroup");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    gvDefectsGroup.DataSource = dt;
                    gvDefectsGroup.DataBind();
                    gvDefectsGroup.Visible = true;
                }
            }

        }
        catch (Exception ex)
        {
            _common.ErrorLog("User Management-E1", ex.Message.ToString());

            gvDefectsGroup.Visible = false;

        }
    }
    protected void loadDefectGroupItems(int groupid)
    {
        try
        {
            tbSearch.Visible = false;
            pnlNoDefectForMark.Visible = true;
            chkDefectItems.Items.Clear();

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_repairableitems");
            MyCommand.Parameters.AddWithValue("p_groupid", groupid);
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                chkDefectItems.DataSource = dt;
                chkDefectItems.DataTextField = "REPAIR_NAME_EN";
                chkDefectItems.DataValueField = "REPR_ID";
                chkDefectItems.DataBind();
                pnlNoDefectForMark.Visible = false;
                tbSearch.Visible = true;

                if (ViewState["Defects"] != null)
                {
                    DataTable dt2 = (DataTable)ViewState["Defects"];
                    Int32 i = 0;
                    foreach (DataRow row in dt2.Rows)
                    {
                        foreach (ListItem item in chkDefectItems.Items)
                        {
                            if (item.Value == dt2.Rows[i]["DefectID"].ToString() & Session["_groupID"].ToString() == dt2.Rows[i]["groupID"].ToString())
                            {
                                item.Selected = true;
                                item.Enabled = false;
                            }
                        }
                        i = i + 1;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("User Management-E1", ex.Message.ToString());
            gvDefectsGroup.Visible = false;

        }
    }

    protected void gvFilledDieselDtls_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvFilledDieselDtls.PageIndex = e.NewPageIndex;
        this.getAllFilledDieselDetails();
    }

    protected void rbtnRunningBus_CheckedChanged(object sender, EventArgs e)
    {
        getLastFilledDieselDetails();
    }
    public void getDFOdrDepotFuelDetails()
    {
        try
        {
            pnlDFNoOtherDepotRecord.Visible = true;
            gvDFFuelFilledOtherDepots.Visible = false;


            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_odrdepotfueldetails");
            MyCommand.Parameters.AddWithValue("p_dutyrefno", Session["waybillNo"].ToString());
            MyCommand.Parameters.AddWithValue("p_depotcode", Session["_LDepotCode"].ToString());
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    gvDFFuelFilledOtherDepots.DataSource = dt;
                    gvDFFuelFilledOtherDepots.DataBind();
                    pnlDFNoOtherDepotRecord.Visible = false;
                    gvDFFuelFilledOtherDepots.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void getDFDefectsList()
    {
        try
        {
            pnlDFNoDefectsList.Visible = true;
            rptrDFDefectsList.Visible = false;

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_dfdefectslist");
            MyCommand.Parameters.AddWithValue("p_dieselrefno", Session["DIESELREFNo"].ToString());
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    rptrDFDefectsList.DataSource = dt;
                    rptrDFDefectsList.DataBind();
                    pnlDFNoDefectsList.Visible = false;
                    rptrDFDefectsList.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void gvFilledDieselDtls_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (ddlBusNewRunning.SelectedValue == "R")
            {
                gvFilledDieselDtls.Columns[1].Visible = true;
            }
            else
            {
                gvFilledDieselDtls.Columns[1].Visible = false;
            }
        }
    }
    protected void gvLastFilled_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "viewDetails")
            {
                GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                int RowIndex = oItem.RowIndex;
                Session["DIESELREFNo"] = gvLastFilled.DataKeys[index].Values["dieselrefno"].ToString();
                lblDFBusNo.Text = gvLastFilled.DataKeys[index].Values["busregno"].ToString();
                if (rbtnRunningBus.Checked == true)
                {
                    Session["waybillNo"] = gvLastFilled.DataKeys[index].Values["waybillno"].ToString();
                    lblDFServiceName.Text = gvLastFilled.DataKeys[index].Values["servicename"].ToString();
                    lblDFDutyDateTime.Text = gvLastFilled.DataKeys[index].Values["dutydatetime"].ToString();
                    getDFOdrDepotFuelDetails();
                    divDFBusDetails.Visible = false;
                    divDFWaybillBusDetails.Visible = true;
                    divDFOdrDepots.Visible = false;
                }
                else
                {
                    divDFOdrDepots.Visible = false;
                    divDFDefects.Visible = false;
                    divDFBusDetails.Visible = true;
                    divDFWaybillBusDetails.Visible = false;
                    lblDFBusRegNo.Text = gvLastFilled.DataKeys[index].Values["busregno"].ToString();
                    lblDFBusType.Text = gvLastFilled.DataKeys[index].Values["bustype"].ToString();
                    lblDFOdometerReading.Text = gvLastFilled.DataKeys[index].Values["odometerreading"].ToString();
                }
                //getDFDefectsList();
               // divDFDefects.Visible = false;
               string depotpump= gvLastFilled.DataKeys[index].Values["depotpump_yn"].ToString();
                string mappedpump = gvLastFilled.DataKeys[index].Values["mappedpump_yn"].ToString();
                string privatepump = gvLastFilled.DataKeys[index].Values["privatepump_yn"].ToString();
                divdepotfilled.Visible = false;
                divmappedfilled.Visible = false;
                divDFPvtFillingDtls.Visible = false;
                if (depotpump == "Y")
                {
                    divdepotfilled.Visible = true;
                    lblDFFillingSt.Text = gvLastFilled.DataKeys[index].Values["fillingstationname"].ToString();
                    lblDFTank.Text = gvLastFilled.DataKeys[index].Values["tankno"].ToString();
                    lblDFPump.Text = gvLastFilled.DataKeys[index].Values["pumpno"].ToString();
                    lblDFPumpReading.Text = gvLastFilled.DataKeys[index].Values["pumpreading"].ToString();
                    lblDFQuantity.Text = gvLastFilled.DataKeys[index].Values["qty_filled"].ToString();
                }
                if (mappedpump == "Y")
                {
                    divmappedfilled.Visible = true;
                    lblmappedname.Text = gvLastFilled.DataKeys[index].Values["mappedpumpname"].ToString();
                    lblmappedreceiptno.Text = gvLastFilled.DataKeys[index].Values["mappedreceiptno"].ToString();
                    lblmappedamt.Text = gvLastFilled.DataKeys[index].Values["mappedamtpaid"].ToString();
                    lblmappedquqntity.Text = gvLastFilled.DataKeys[index].Values["mappedqty"].ToString();
                }
                if (privatepump == "Y")
                {
                    divDFPvtFillingDtls.Visible = true;
                    lblDFPvtReceiptNo.Text = gvLastFilled.DataKeys[index].Values["pvtpumpreceiptno"].ToString();
                    lblDFPvtQuantity.Text = gvLastFilled.DataKeys[index].Values["pvtfilledqty"].ToString();
                    lblDFPvtAmt.Text = gvLastFilled.DataKeys[index].Values["pvtpumpamtpaid"].ToString();
                    lblDFPvtPump.Text = gvLastFilled.DataKeys[index].Values["pvtpumpname"].ToString();
                    lblDFPvtAddress.Text = gvLastFilled.DataKeys[index].Values["pvtpumpaddress"].ToString();
                }

                
                pnlDieselEntry.Visible = false;
                pnlViewBusDieselDetails.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message);
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }
    protected void lbtnCloseDieselDetails_Click(object sender, EventArgs e)
    {
        pnlDieselEntry.Visible = true;
        pnlViewBusDieselDetails.Visible = false;
    }
    protected void lbtnSearchPendingWaybill_Click(object sender, EventArgs e)
    {
        getPendingWaybills();
    }



    protected void LinkButtonInfo_Click(object sender, EventArgs e)
    {
        Successmsg("Coming Soon");
    }

    protected void gvFilledDieselDtls_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "viewDetails")
            {
                GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                int RowIndex = oItem.RowIndex;
                Session["DIESELREFNo"] = gvFilledDieselDtls.DataKeys[index].Values["dieselrefno"].ToString();
                lblDFBusNo.Text = gvFilledDieselDtls.DataKeys[index].Values["busregno"].ToString();
                //if (rbtnRunningBus.Checked == true)
                //{
                //    Session["waybillNo"] = gvFilledDieselDtls.DataKeys[index].Values["waybillno"].ToString();
                //    lblDFServiceName.Text = gvFilledDieselDtls.DataKeys[index].Values["servicename"].ToString();
                //    lblDFDutyDateTime.Text = gvFilledDieselDtls.DataKeys[index].Values["dutydatetime"].ToString();
                //    getDFOdrDepotFuelDetails();
                //    divDFBusDetails.Visible = false;
                //    divDFWaybillBusDetails.Visible = true;
                //    divDFOdrDepots.Visible = true;
                //}
                //else
                //{
                //    divDFOdrDepots.Visible = false;
                //    divDFDefects.Visible = false;
                //    divDFBusDetails.Visible = true;
                //    divDFWaybillBusDetails.Visible = false;
                //    lblDFBusRegNo.Text = gvFilledDieselDtls.DataKeys[index].Values["busregno"].ToString();
                //    lblDFBusType.Text = gvFilledDieselDtls.DataKeys[index].Values["bustype"].ToString();
                //    lblDFOdometerReading.Text = gvFilledDieselDtls.DataKeys[index].Values["odometerreading"].ToString();
                //}
                //getDFDefectsList();
                // divDFDefects.Visible = false;
                divDFOdrDepots.Visible = false;
                string depotpump = gvFilledDieselDtls.DataKeys[index].Values["depotpump_yn"].ToString();
                string mappedpump = gvFilledDieselDtls.DataKeys[index].Values["mappedpump_yn"].ToString();
                string privatepump = gvFilledDieselDtls.DataKeys[index].Values["privatepump_yn"].ToString();
                divdepotfilled.Visible = false;
                divmappedfilled.Visible = false;
                divDFPvtFillingDtls.Visible = false;
                if (depotpump == "Y")
                {
                    divdepotfilled.Visible = true;
                    lblDFFillingSt.Text = gvFilledDieselDtls.DataKeys[index].Values["fillingstationname"].ToString();
                    lblDFTank.Text = gvFilledDieselDtls.DataKeys[index].Values["tankno"].ToString();
                    lblDFPump.Text = gvFilledDieselDtls.DataKeys[index].Values["pumpno"].ToString();
                    lblDFPumpReading.Text = gvFilledDieselDtls.DataKeys[index].Values["pumpreading"].ToString();
                    lblDFQuantity.Text = gvFilledDieselDtls.DataKeys[index].Values["qty_filled"].ToString();
                }
                if (mappedpump == "Y")
                {
                    divmappedfilled.Visible = true;
                    lblmappedname.Text = gvFilledDieselDtls.DataKeys[index].Values["mappedpumpname"].ToString();
                    lblmappedreceiptno.Text = gvFilledDieselDtls.DataKeys[index].Values["mappedreceiptno"].ToString();
                    lblmappedamt.Text = gvFilledDieselDtls.DataKeys[index].Values["mappedamtpaid"].ToString();
                    lblmappedquqntity.Text = gvFilledDieselDtls.DataKeys[index].Values["mappedqty"].ToString();
                }
                if (privatepump == "Y")
                {
                    divDFPvtFillingDtls.Visible = true;
                    lblDFPvtReceiptNo.Text = gvFilledDieselDtls.DataKeys[index].Values["pvtpumpreceiptno"].ToString();
                    lblDFPvtQuantity.Text = gvFilledDieselDtls.DataKeys[index].Values["pvtfilledqty"].ToString();
                    lblDFPvtAmt.Text = gvFilledDieselDtls.DataKeys[index].Values["pvtpumpamtpaid"].ToString();
                    lblDFPvtPump.Text = gvFilledDieselDtls.DataKeys[index].Values["pvtpumpname"].ToString();
                    lblDFPvtAddress.Text = gvFilledDieselDtls.DataKeys[index].Values["pvtpumpaddress"].ToString();
                }


                pnlDieselEntry.Visible = false;
                pnlViewBusDieselDetails.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message);
        }
    }
}
