using System;
using System.Data;
using Npgsql;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_DepAdminTankMgmt : System.Web.UI.Page
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

        Session["_moduleName"] = "Tank Management";
        if (IsPostBack == false)
        {
            loadOffice(ddlDepot);
            ddlDepot.SelectedValue = Session["_LDepotCode"].ToString();
            ddlDepot.Enabled = false;
            LoadFillingStation(ddlFillingStation);
            loadAgency(ddlAgency);
            BindGridTankManagement();
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
                _common.ErrorLog("DepAdminTankManagement-M1", dt.TableName);
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
            _common.ErrorLog("DepAdminTankManagement-M1", ex.Message.ToString());
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
                _common.ErrorLog("DepAdminTankManagement-M2", dt.TableName);
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
            _common.ErrorLog("DepAdminTankManagement-M2", ex.Message.ToString());
        }
    }
    public void loadAgency(DropDownList ddlAgency)//M3
    {
        try
        {
            ddlAgency.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_agencies_fordropdown");
            MyCommand.Parameters.AddWithValue("p_itemid", "3");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlAgency.DataSource = dt;
                    ddlAgency.DataTextField = "agencyname";
                    ddlAgency.DataValueField = "agency_id";
                    ddlAgency.DataBind();
                }
            }
            else
            {
                _common.ErrorLog("DepAdminTankManagement-M3", dt.TableName);
                Errormsg(dt.TableName);
            }
            ddlAgency.Items.Insert(0, "SELECT");
            ddlAgency.Items[0].Value = "0";
            ddlAgency.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlAgency.Items.Insert(0, "SELECT");
            ddlAgency.Items[0].Value = "0";
            ddlAgency.SelectedIndex = 0;
            _common.ErrorLog("DepAdminTankManagement-M3", ex.Message.ToString());
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
            if (_validation.IsValidString(tbTankNumber.Text.Trim(), 1, tbTankNumber.MaxLength) == false)
            {
                msgcnt = msgcnt + 1;
                msg = msg + msgcnt.ToString() + ". Tank Number.<br>";
            }
            if (ddlAgency.SelectedValue == "0")
            {
                msgcnt = msgcnt + 1;
                msg = msg + msgcnt.ToString() + ". Agency.<br>";
            }
            if (_validation.IsValidString(tbCapacity.Text.Trim(), 1, tbCapacity.MaxLength) == false)
            {
                msgcnt = msgcnt + 1;
                msg = msg + msgcnt.ToString() + ". Tank Capacity.<br>";
            }
            if (tbInstalledOnDate.Text.Length == 0 | tbInstalledOnDate.Text.ToString() == "")
            {
                msgcnt = msgcnt + 1;
                msg = msg + msgcnt.ToString() + ". Installed/Stared on Date.<br>";
            }
            //else if (_validation.IsValidateDateTime(tbInstalledOnDate.Text) == false)
            //{
            //    msgcnt = msgcnt + 1;
            //    msg = msg + msgcnt.ToString() + ". Installed/Stared on Date.<br>";
            //}
            if (_validation.IsValidString(tbInitialFuel.Text.Trim(), 1, tbInitialFuel.MaxLength) == false)
            {
                msgcnt = msgcnt + 1;
                msg = msg + msgcnt.ToString() + ". Initial Fuel.<br>";
            }
            if (_validation.IsValidString(tbNoOfPumps.Text.Trim(), 0, tbNoOfPumps.MaxLength) == false)
            {
                msgcnt = msgcnt + 1;
                msg = msg + msgcnt.ToString() + ". Number of Pumps.<br>";
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
            _common.ErrorLog("DepAdminTankManagement-M4", ex.Message.ToString());
            return false;
        }
    }
    private void insertTankManagement()//M5
    {
        try
        {
            string ipaddress = HttpContext.Current.Request.UserHostAddress;
            string UpdatedBy = Session["_UserCode"].ToString();
            string Action = Session["Action"].ToString();
            string tank = tbTankNumber.Text.ToString();
            Int32 agencyid = Convert.ToInt32(ddlAgency.SelectedValue.ToString());

            Int16 fillingstation = Convert.ToInt16(ddlFillingStation.SelectedValue.ToString());
            if (Action != "S")
            {
                tank = Session["tanknumber"].ToString();
                fillingstation=Convert.ToInt16( Session["fillingst"].ToString());
            }
            string installledDate = tbInstalledOnDate.Text.ToString();
            Int16 noofpump = 0;
            if (tbNoOfPumps.Text.Length > 0)
            {
                noofpump = Convert.ToInt16(tbNoOfPumps.Text.ToString());
            }
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            string Mresult = "";
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_tankmgmt_insertupdate");
            MyCommand.Parameters.AddWithValue("p_action", Action);
            MyCommand.Parameters.AddWithValue("p_office_id", ddlDepot.SelectedValue.ToString());
            MyCommand.Parameters.AddWithValue("p_tank_number", tank);
            MyCommand.Parameters.AddWithValue("p_agency_id", agencyid);
            MyCommand.Parameters.AddWithValue("p_capacity ", tbCapacity.Text.ToString());
            MyCommand.Parameters.AddWithValue("p_installed_on_date", installledDate);
            MyCommand.Parameters.AddWithValue("p_initialqty", tbInitialFuel.Text.ToString());
            MyCommand.Parameters.AddWithValue("p_no_of_pumps", noofpump);
            MyCommand.Parameters.AddWithValue("p_filling_stn", fillingstation);
            MyCommand.Parameters.AddWithValue("p_updatedby", UpdatedBy);
            MyCommand.Parameters.AddWithValue("p_ipaddress", ipaddress);
            Mresult = bll.UpdateAll(MyCommand);
            if (Mresult == "Success")
            {
                Successmsg("Record has succefully been saved");
                BindGridTankManagement();
                Reset();
            }
            else
            {
                Errormsg("Oops! You're data not saved." + Mresult);
                _common.ErrorLog("DepAdminTankManagement-M5", Mresult);

            }

        }
        catch (Exception ex)
        {
            _common.ErrorLog("DepAdminTankManagement-M5", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
            return;
        }
    }
    public void BindGridTankManagement()//M6
    {
        try
        {
            pnlNoRecord.Visible = true;
            DataTable dt = new DataTable();
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            string officeid = Session["_LDepotCode"].ToString();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_bindtank_management");
            MyCommand.Parameters.AddWithValue("p_officeid", officeid);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    gvTankFillingStation.Visible = true;
                    gvTankFillingStation.DataSource = dt;
                    gvTankFillingStation.DataBind();
                    lblTanksListCount.Text = dt.Rows.Count.ToString();
                    pnlNoRecord.Visible = false;
                }
            }
            else
            {
                _common.ErrorLog("DepAdminTankManagement-M6", dt.TableName.ToString());
                Errormsg(dt.TableName);

            }
        }
        catch (Exception ex)
        {
            gvTankFillingStation.Visible = false;
            _common.ErrorLog("DepAdminTankManagement-M6", ex.Message.ToString());
            Errormsg(dt.TableName);

        }
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
    public void Reset()
    {
        ddlFillingStation.SelectedIndex = 0;
        ddlAgency.SelectedIndex = 0;
        tbTankNumber.Text = "";
        tbInitialFuel.Text = "";
        tbInstalledOnDate.Text = "";
        tbCapacity.Text = "";
        tbNoOfPumps.Text = "";
        tbTankNumber.ReadOnly = false;
        lbtnTMSave.Visible = true;
        lbtnTMUpdate.Visible = false;
        lblAddNewTank.Visible = true;
        lblUpdateTank.Visible = false;
    }

    #endregion

    #region Events
    protected void ddlDepot_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadFillingStation(ddlFillingStation);
    }
    protected void lbtnTMSave_Click(object sender, EventArgs e)
    {
        if (IsValidValues() == false)
        {
            return;
        }
        Session["Action"] = "S";
        confirmmsg("Do you want to save new tank entries details?");

    }
    protected void lbtnTMUpdate_Click(object sender, EventArgs e)
    {
        if (IsValidValues() == false)
        {
            return;
        }
        Session["Action"] = "U";
        confirmmsg("Do you want to update new tank entries details?");
    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        insertTankManagement();
    }
    protected void gvTankFillingStation_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "gvEdit")
            {
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                int i = row.RowIndex;
                Session["tanknumber"] = gvTankFillingStation.DataKeys[i]["tanknumber"].ToString();
                LoadFillingStation(ddlFillingStation);
                ddlFillingStation.SelectedValue = gvTankFillingStation.DataKeys[row.RowIndex]["fillingstn"].ToString();
                Session["fillingst"] = gvTankFillingStation.DataKeys[i]["fillingstn"].ToString();
                tbTankNumber.Text = gvTankFillingStation.DataKeys[row.RowIndex]["tanknumber"].ToString();
                tbTankNumber.ReadOnly = true;
                loadAgency(ddlAgency);
                ddlAgency.SelectedValue = gvTankFillingStation.DataKeys[row.RowIndex]["agencyid"].ToString();
                tbCapacity.Text = gvTankFillingStation.DataKeys[row.RowIndex]["cpcity"].ToString();
                tbInstalledOnDate.Text = gvTankFillingStation.DataKeys[row.RowIndex]["statedondate"].ToString();
                tbInitialFuel.Text = gvTankFillingStation.DataKeys[row.RowIndex]["p_initialqty"].ToString();
                if (!DBNull.Value.Equals(gvTankFillingStation.DataKeys[row.RowIndex]["noofpumps"]))
                {
                    tbNoOfPumps.Text = gvTankFillingStation.DataKeys[row.RowIndex]["noofpumps"].ToString();
                }
                lblAddNewTank.Visible = false;
                lblUpdateTank.Visible = true;
                lbtnTMSave.Visible = false;
                lbtnTMUpdate.Visible = true;
            }


            if (e.CommandName == "gvDelete")
            {
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                int i = row.RowIndex;
                Session["Action"] = "D";
                Session["tanknumber"] = gvTankFillingStation.DataKeys[i]["tanknumber"].ToString();

                Session["fillingst"] = gvTankFillingStation.DataKeys[i]["fillingstn"].ToString();
                confirmmsg("Do you want to Delete Tank Management Details ?");

            }

        }
        catch (Exception ex)
        {
            _common.ErrorLog("DepAdminTankManagement-E1", ex.Message.ToString());
        }

    }//E1
    protected void gvTankFillingStation_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTankFillingStation.PageIndex = e.NewPageIndex;
        BindGridTankManagement();
    }
    protected void lbtnview_Click(object sender, EventArgs e)
    {
        string msg = "";
        msg = msg + "1.To create tank,first select depot and filling station and then enter tank details.<br/>";
        msg = msg + "2.Depot mananger can create tanks of its depot only.<br/>";
        msg = msg + "3.If tank is used then it cannot be deleted.<br/>";
        msg = msg + "4.Except tank number, other details can be updated.<br/>";
        InfoMsg(msg);
    }
    protected void lbtnTMReset_Click(object sender, EventArgs e)
    {
        Reset();
    }

    #endregion

}