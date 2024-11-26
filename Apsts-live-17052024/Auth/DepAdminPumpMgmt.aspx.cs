using System;
using System.Data;
using Npgsql;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_DepAdminPumpMgmt : System.Web.UI.Page
{
    NpgsqlCommand MyCommand = new NpgsqlCommand();
    private sbBLL bll = new sbBLL();
    DataTable dt = new DataTable();
    sbValidation _validation = new sbValidation();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    private sbCommonFunc _common = new sbCommonFunc();
    protected void Page_Load(object sender, EventArgs e)
    {
        lblSummary.Text = "Summary as on Date " + DateTime.Now.ToString("dd/MM/yyyy hh:ss tt");

        Session["_moduleName"] = "Pump Management";
        if (IsPostBack == false)
        {
            loadPumps();
            loadOffice(ddlDepot);
            ddlDepot.SelectedValue = Session["_LDepotCode"].ToString();
            ddlDepot.Enabled = false;
            LoadFillingStation(ddlFillingStation);
            loadTank(Convert.ToInt16(ddlFillingStation.SelectedValue.ToString()), ddlTank);
        }
    }

    #region Methods
    public void loadOffice(DropDownList ddlDepot)//M1
    {
        try
        {
            Int32 OfcLvl = 30;
            ddlDepot.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_ofclvlwiseoffice");
            MyCommand.Parameters.AddWithValue("p_ofclvlid", OfcLvl);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlDepot.DataSource = dt;
                    ddlDepot.DataTextField = "officename";
                    ddlDepot.DataValueField = "officeid";
                    ddlDepot.DataBind();
                }
            }
            else
            {
                _common.ErrorLog("DepAdminPumpMgmt-M1", dt.TableName);
                Errormsg(dt.TableName);
            }
            ddlDepot.Items.Insert(0, "SELECT");
            ddlDepot.Items[0].Value = "0";
            ddlDepot.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlDepot.Items.Insert(0, "SELECT");
            ddlDepot.Items[0].Value = "0";
            ddlDepot.SelectedIndex = 0;
            _common.ErrorLog("DepAdminPumpMgmt-M1", ex.Message.ToString());
        }
    }
    public void LoadFillingStation(DropDownList ddlFillingStation)//M2
    {
        try
        {
            string officeid = Session["_LDepotCode"].ToString();
            ddlFillingStation.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_tank_filling_station");
            MyCommand.Parameters.AddWithValue("p_officeid", officeid);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlFillingStation.DataSource = dt;
                    ddlFillingStation.DataTextField = "fillingstn_name";
                    ddlFillingStation.DataValueField = "fillingstn_id";
                    ddlFillingStation.DataBind();
                }
            }
            else
            {
                _common.ErrorLog("DepAdminPumpMgmt-M2", dt.TableName);
                Errormsg(dt.TableName);
            }
            ddlFillingStation.Items.Insert(0, "SELECT");
            ddlFillingStation.Items[0].Value = "0";
            ddlFillingStation.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlFillingStation.Items.Insert(0, "SELECT");
            ddlFillingStation.Items[0].Value = "0";
            ddlFillingStation.SelectedIndex = 0;
            _common.ErrorLog("DepAdminPumpMgmt-M2", ex.Message.ToString());
        }
    }
    public void loadTank(int fillingstn, DropDownList ddlTank)//M3
    {
        try
        {
            ddlTank.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_tank");
            MyCommand.Parameters.AddWithValue("p_fillingstn", fillingstn);
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
                _common.ErrorLog("DepAdminPumpMgmt-M3", dt.TableName);
                Errormsg(dt.TableName);
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
            _common.ErrorLog("DepAdminPumpMgmt-M3", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
        }
    }
    private bool IsValidValues()//M4
    {
        try
        {
            int msgcnt = 0;
            string msg = "";
            if (ddlDepot.SelectedValue == "0")
            {
                msgcnt = msgcnt + 1;
                msg = msg + msgcnt.ToString() + ". Depot.<br>";
            }
            if (ddlFillingStation.SelectedValue == "0")
            {
                msgcnt = msgcnt + 1;
                msg = msg + msgcnt.ToString() + ". Filling Station.<br>";
            }
            if (ddlTank.SelectedValue == "0")
            {
                msgcnt = msgcnt + 1;
                msg = msg + msgcnt.ToString() + ". Tank.<br>";
            }
            if (_validation.IsValidString(tbPumpNumber.Text.Trim(), 1, tbPumpNumber.MaxLength) == false)
            {
                msgcnt = msgcnt + 1;
                msg = msg + msgcnt.ToString() + ". Pump Number.<br>";
            }
            if (tbStartedOnDate.Text.Length == 0 | tbStartedOnDate.Text.ToString() == "")
            {
                msgcnt = msgcnt + 1;
                msg = msg + msgcnt.ToString() + ". Started on Date.<br>";
            }
            //else if (_validation.IsValidateDateTime(tbStartedOnDate.Text) == false)
            //{
            //    msgcnt = msgcnt + 1;
            //    msg = msg + msgcnt.ToString() + ". Started on Date.<br>";
            //}
            if (_validation.IsValidString(tbInitialReading.Text.Trim(), 1, tbInitialReading.MaxLength) == false)
            {
                msgcnt = msgcnt + 1;
                msg = msg + msgcnt.ToString() + ". Initial Reading.<br>";
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
            _common.ErrorLog("DepAdminPumpMgmt-M1", ex.Message.ToString());

            return false;
        }
    }
    private void savePumps()//M5
    {
        try
        {
            string ipaddress = HttpContext.Current.Request.UserHostAddress;
            string UpdatedBy = Session["_UserCode"].ToString();
            string Action = Session["Action"].ToString();
            string pumpNo = "";
            string tank = "";
            if (Action != "S")
            {
                pumpNo = Session["pumpnumber"].ToString();
                tank = Session["tanknumber"].ToString();
            }

            else
            {
                pumpNo = tbPumpNumber.Text.ToString();
                tank = ddlTank.SelectedValue.ToString();
            }

            string installledDate = tbStartedOnDate.Text.ToString();
            string initialReading = tbInitialReading.Text.ToString();
            string FillingStation = ddlFillingStation.SelectedValue.ToString();
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            string Mresult = "";
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_pumpmgmt_insertupdate");
            MyCommand.Parameters.AddWithValue("p_action", Action);
            MyCommand.Parameters.AddWithValue("p_office_id", ddlDepot.SelectedValue.ToString());
            MyCommand.Parameters.AddWithValue("p_tank_number", tank);
            MyCommand.Parameters.AddWithValue("p_pump_number", pumpNo);
            MyCommand.Parameters.AddWithValue("p_installed_on_date", installledDate);
            MyCommand.Parameters.AddWithValue("p_initial_reading", initialReading);
            MyCommand.Parameters.AddWithValue("p_filling_stn", Convert.ToInt16(FillingStation));
            MyCommand.Parameters.AddWithValue("p_updatedby", UpdatedBy);
            MyCommand.Parameters.AddWithValue("p_ipaddress", ipaddress);
            Mresult = bll.UpdateAll(MyCommand);
            if (Mresult == "Success")
            {
                Successmsg("Record has succefully updated");
                loadPumps();
                Reset();
            }
            else
            {
                _common.ErrorLog("DepAdminPumpMgmt-M5", Mresult);
                Errormsg(Mresult);
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("DepAdminPumpMgmt-M5", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
        }
    }
    public void loadPumps()//M6
    {
        try
        {
            string officeid = Session["_LDepotCode"].ToString();
            pnlNoRecord.Visible = true;
            DataTable dt = new DataTable();
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_bind_pump_management");
            MyCommand.Parameters.AddWithValue("p_officeid", officeid);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    gvPumpFillingStation.Visible = true;
                    gvPumpFillingStation.DataSource = dt;
                    gvPumpFillingStation.DataBind();
                    lblPumpListCount.Text = dt.Rows.Count.ToString();
                    pnlNoRecord.Visible = false;
                }
            }
            else
            {
                _common.ErrorLog("DepAdminPumpMgmt-M6", dt.TableName);
                Errormsg(dt.TableName);
            }
        }
        catch (Exception ex)
        {
            gvPumpFillingStation.Visible = false;
            _common.ErrorLog("DepAdminPumpMgmt-M6", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
        }
    }
    public void Reset()
    {
        LoadFillingStation(ddlFillingStation);
        loadTank(Convert.ToInt16(ddlFillingStation.SelectedValue.ToString()), ddlTank);
        tbPumpNumber.Text = "";
        tbInitialReading.Text = "";
        tbStartedOnDate.Text = "";
        lbtnPMSave.Visible = true;
        lbtnPMUpdate.Visible = false;
        tbPumpNumber.ReadOnly = false;
        lblAddPump.Visible = true;
        lblUpdatePump.Visible = false;
    }
    private void Errormsg(string msg)
    {

        string popup = _popup.modalPopupSmall("W", "Please Check", msg, "Close");
        Response.Write(popup);
    }
    private void Successmsg(string msg)
    {
        string popup = _popup.modalPopupSmall("S", "Success", msg, "Close");
        Response.Write(popup);
    }
    public void confirmmsg(string message)
    {
        lblConfirmation.Text = message;
        mpConfirmation.Show();
    }
    private void InfoMsg(string msg)
    {
        string popup = _popup.modalPopupSmall("I", "Information", msg, "Close");
        Response.Write(popup);
    }
    #endregion

    #region Events
    protected void ddlFillingStation_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadTank(Convert.ToInt16(ddlFillingStation.SelectedValue.ToString()), ddlTank);
    }
    protected void lbtnPMSave_Click(object sender, EventArgs e)
    {
        if (IsValidValues() == false)
        {
            return;
        }
        Session["Action"] = "S";
        confirmmsg("Do you want to save diesel tank pump details?");
    }
    protected void lbtnPMUpdate_Click(object sender, EventArgs e)
    {
        if (IsValidValues() == false)
        {
            return;
        }
        Session["Action"] = "U";
        confirmmsg("Do you want to update diesel tank pump details?");
    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        savePumps();
    }
    protected void gvPumpFillingStation_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvPumpFillingStation.PageIndex = e.NewPageIndex;
        loadPumps();
    }
    protected void gvPumpFillingStation_RowCommand(object sender, GridViewCommandEventArgs e)//E1
    {
        try
        {
            if (e.CommandName == "gvEdit")
            {
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                int i = row.RowIndex;
                LoadFillingStation(ddlFillingStation);
                ddlFillingStation.SelectedValue = gvPumpFillingStation.DataKeys[row.RowIndex]["fillingstn"].ToString();
                loadTank(Convert.ToInt16(ddlFillingStation.SelectedValue.ToString()), ddlTank);
                ddlTank.SelectedValue = gvPumpFillingStation.DataKeys[row.RowIndex]["tanknumber"].ToString();
                Session["tanknumber"] = gvPumpFillingStation.DataKeys[i]["tanknumber"].ToString();
                Session["pumpnumber"] = gvPumpFillingStation.DataKeys[i]["pumpnumber"].ToString();
                tbPumpNumber.Text = Session["pumpnumber"].ToString();
                tbPumpNumber.ReadOnly = true;
                tbStartedOnDate.Text = gvPumpFillingStation.DataKeys[row.RowIndex]["installedon_date"].ToString();
                tbInitialReading.Text = gvPumpFillingStation.DataKeys[row.RowIndex]["initialreading"].ToString();
                lblAddPump.Visible = false;
                lblUpdatePump.Visible = true;
                lbtnPMSave.Visible = false;
                lbtnPMUpdate.Visible = true;
            }
            if (e.CommandName == "gvDelete")
            {
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                int i = row.RowIndex;
                Session["Action"] = "D";
                Session["tanknumber"] = gvPumpFillingStation.DataKeys[i]["tanknumber"].ToString();
                Session["pumpnumber"] = gvPumpFillingStation.DataKeys[i]["pumpnumber"].ToString();
                confirmmsg("Do you want to Delete Pump Management Details ?");
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("DepAdminPumpMgmt-M1", ex.Message);
            Errormsg(dt.TableName);
        }
    }
    protected void lbtnPMReset_Click(object sender, EventArgs e)
    {
        Reset();
    }
    protected void lbtnview_Click(object sender, EventArgs e)
    {
        string msg = "";
        msg = msg + "1.To create pump,first select depot, filling station and tank and then enter pump details.<br/>";
        msg = msg + "2.Depot mananger can create pump of its depot only.<br/>";
        msg = msg + "3.If pump is used then it cannot be deleted.<br/>";
        msg = msg + "4.Except tank number, other details can be updated.<br/>";
        InfoMsg(msg);
    }
    protected void ddlDepot_SelectedIndexChanged(object sender, EventArgs e)
    {
        // LoadFillingStation();
    }
    #endregion





}