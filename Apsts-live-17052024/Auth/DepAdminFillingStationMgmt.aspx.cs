using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Drawing;
using System.IO;
using Npgsql;
using System.Web.UI.WebControls;

public partial class Auth_DepAdminFillingStationMgmt : System.Web.UI.Page
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
        Session["_moduleName"] = "Filling Station Management";
        if (!IsPostBack)
        {

            loadStates(ddlstate);
            loadDistrict(ddlstate.SelectedValue.ToString(), ddlDistrict);
            loadOffice(ddlDepotfilling);
            ddlDepotfilling.SelectedValue = Session["_LDepotCode"].ToString();
            ddlDepotfilling.Enabled = false;

            LoadFillingStation();

            // ddlDepotfilling.SelectedValue = Session["_UserDepotCode"].ToString();
        }
    }

    #region Methods
    private void loadStates(DropDownList ddlstate)//M1
    {
        try
        {
            ddlstate.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.f_get_states");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlstate.DataSource = dt;
                    ddlstate.DataTextField = "stname";
                    ddlstate.DataValueField = "stcode";
                    ddlstate.DataBind();
                }
            }
            else
            {
                _common.ErrorLog("DepAdminFillingStationMgmt-M1", dt.TableName);
            }

            ddlstate.Items.Insert(0, "SELECT");
            ddlstate.Items[0].Value = "0";
            ddlstate.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlstate.Items.Insert(0, "SELECT");
            ddlstate.Items[0].Value = "0";
            ddlstate.SelectedIndex = 0;
            _common.ErrorLog("DepAdminFillingStationMgmt-M1", ex.Message.ToString());
        }
    }
    public void loadDistrict(string statecode, DropDownList ddlDistrict)//M2
    {
        try
        {
            ddlDistrict.Items.Clear();
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.f_get_district");
            MyCommand.Parameters.AddWithValue("p_statecode", statecode);
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
                _common.ErrorLog("DepAdminFillingStationMgmt-M2", dt.TableName);
                Errormsg(dt.TableName);
            }

            ddlDistrict.Items.Insert(0, "SELECT");
            ddlDistrict.Items[0].Value = "0";
            ddlDistrict.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlDistrict.Items.Insert(0, "SELECT");
            ddlDistrict.Items[0].Value = "0";
            ddlDistrict.SelectedIndex = 0;
            _common.ErrorLog("DepAdminFillingStationMgmt-M2", ex.Message.ToString());
        }
    }
    public void loadOffice(DropDownList ddlDepotfilling)//M3
    {
        try
        {
            Int32 OfcLvl = 30;
            ddlDepotfilling.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_ofclvlwiseoffice");
            MyCommand.Parameters.AddWithValue("p_ofclvlid", OfcLvl);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlDepotfilling.DataSource = dt;
                    ddlDepotfilling.DataTextField = "officename";
                    ddlDepotfilling.DataValueField = "officeid";
                    ddlDepotfilling.DataBind();
                }
            }
            else
            {
                _common.ErrorLog("DepAdminFillingStationMgmt-M3", dt.TableName);
                Errormsg(dt.TableName);
            }
            ddlDepotfilling.Items.Insert(0, "SELECT");
            ddlDepotfilling.Items[0].Value = "0";
            ddlDepotfilling.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlDepotfilling.Items.Insert(0, "SELECT");
            ddlDepotfilling.Items[0].Value = "0";
            ddlDepotfilling.SelectedIndex = 0;
            _common.ErrorLog("DepAdminFillingStationMgmt-M3", ex.Message.ToString());
        }
    }
    private bool IsValidValues()//M4
    {
        try
        {
            string msg = "";
            int msgcont = 0;
            if (ddlDepotfilling.SelectedValue == "0")
            {
                msgcont = msgcont + 1;
                msg = msg + msgcont.ToString() + ". Depot.<br>";
            }
            if (_validation.IsValidString(tbName.Text.Trim(), 1, tbName.MaxLength) == false)
            {
                msgcont = msgcont + 1;
                msg = msg + msgcont.ToString() + ". Filling Station Name.<br>";
            }
            if (ddlWorkshop.SelectedValue == "0")
            {
                msgcont = msgcont + 1;
                msg = msg + msgcont.ToString() + ". Premises of workshop.<br>";
            }
            else
            {
                if (ddlWorkshop.SelectedValue == "Y")
                {
                    if (ddlWorkshoplist.SelectedValue == "0")
                    {
                        msgcont = msgcont + 1;
                        msg = msg + msgcont.ToString() + ". workshop.<br>";
                    }
                }
            }
            if (_validation.isValidMobileNumber(tbContactNumber1.Text.Trim()) == false)
            {
                msgcont = msgcont + 1;
                msg = msg + msgcont.ToString() + ". Contact Number 1.<br>";
            }
            if (String.IsNullOrEmpty(tbContactNumber2.Text) == false)
            {
                if (_validation.isValidMobileNumber(tbContactNumber2.Text.Trim()) == false)
                {
                    msgcont = msgcont + 1;
                    msg = msg + msgcont.ToString() + ". Contact Number 2.<br>";
                }
            }
            if (ddlstate.SelectedValue == "0")
            {
                msgcont = msgcont + 1;
                msg = msg + msgcont.ToString() + ". Select State.<br>";
            }
            if (ddlDistrict.SelectedValue == "0")
            {
                msgcont = msgcont + 1;
                msg = msg + msgcont.ToString() + ". Select District.<br>";
            }
            if (_validation.IsValidAddress(tbAddress.Text.Trim(), 5, tbAddress.MaxLength) == false)
            {
                msgcont = msgcont + 1;
                msg = msg + msgcont.ToString() + ". Address.<br>";
            }

            if (msgcont > 0)
            {
                Errormsg(msg);
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("DepAdminFillingStationMgmt-M4", ex.Message.ToString());
            return false;
        }
    }
    private void insertFillingStation()//M5
    {
        try
        {
            int stnid = 0;
            string ipaddress = HttpContext.Current.Request.UserHostAddress;
            string UpdatedBy = Session["_UserCode"].ToString();
            string Action = Session["Action"].ToString();

            if(Action != "S")
            {
                stnid = Convert.ToInt32(Session["Stn_id"].ToString());
            }

            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            string Mresult = "";
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_fillingstationmgmt_insertupdate");
            MyCommand.Parameters.AddWithValue("p_action", Action);
            MyCommand.Parameters.AddWithValue("p_office_id", ddlDepotfilling.SelectedValue);
            MyCommand.Parameters.AddWithValue("p_stnid", stnid);
            MyCommand.Parameters.AddWithValue("p_stnen", tbName.Text.ToString());
            MyCommand.Parameters.AddWithValue("p_workshoppermisesyn ", ddlWorkshop.SelectedValue.ToString());
            MyCommand.Parameters.AddWithValue("p_workshopofficeid", ddlWorkshoplist.SelectedValue.ToString());
            MyCommand.Parameters.AddWithValue("p_contactnumber1", tbContactNumber1.Text.ToString());
            MyCommand.Parameters.AddWithValue("p_contactnumber2", tbContactNumber2.Text.ToString());
            MyCommand.Parameters.AddWithValue("p_longitude", tbLongitude.Text.ToString());
            MyCommand.Parameters.AddWithValue("p_latitude", tbLatitude.Text.ToString());
            MyCommand.Parameters.AddWithValue("p_state_code", ddlstate.SelectedValue.ToString());
            MyCommand.Parameters.AddWithValue("p_district", ddlDistrict.SelectedValue.ToString());
            MyCommand.Parameters.AddWithValue("p_address", tbAddress.Text.ToString());        
            MyCommand.Parameters.AddWithValue("p_updatedby", UpdatedBy);
            MyCommand.Parameters.AddWithValue("p_ipaddress", ipaddress);
            Mresult = bll.UpdateAll(MyCommand);
            if (Mresult == "Success")
            {
                Successmsg("Filling Station Details Successfully Saved");
                LoadFillingStation();
                Reset();
            }
            else
            {
                _common.ErrorLog("DepAdminFillingStationMgmt-M2", Mresult);
                Errormsg("Oops! You're data not saved." + Mresult);
            }
            if (Session["Action"].ToString() == "S" || Session["Action"].ToString() == "U" || Session["Action"].ToString() == "D")
            {
               
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("DepAdminFillingStationMgmt-M2", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
        }
    }
    public void LoadFillingStation()//M6 
    {
        try
        {
			string officeid = Session["_LDepotCode"].ToString();
			pnlNoRecord.Visible = true;
            DataTable dt = new DataTable();
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_filling_station");
			MyCommand.Parameters.AddWithValue("p_officeid", officeid);
			dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    gvFillingStation.Visible = true;
                    gvFillingStation.DataSource = dt;
                    gvFillingStation.DataBind();
                    lblFillingStationListCount.Text = dt.Rows.Count.ToString();
                    pnlNoRecord.Visible = false;
                }
            }
            else
            {
                _common.ErrorLog("DepAdminFillingStationMgmt-M6", dt.TableName);
                Errormsg(dt.TableName);
            }
        }
        catch (Exception ex)
        {
            gvFillingStation.Visible = false;
            _common.ErrorLog("DepAdminFillingStationMgmt-M6", ex.Message.ToString());
            Errormsg(ex.Message);

        }
    }
    public void Reset()
    {
        lblAddNewFS.Visible = true;
        lblUpdateFS.Visible = false;
        lbtnFSUpdate.Visible = false;
        lbtnFSSave.Visible = true;
        tbName.Text = "";
        tbName.ReadOnly = false;
        ddlWorkshop.SelectedValue = "0";
        ddlWorkshoplist.SelectedValue = "0";
        tbContactNumber1.Text = "";
        tbContactNumber2.Text = "";
        tbLatitude.Text = "";
        tbLongitude.Text = "";
        loadStates(ddlstate);
        loadDistrict(ddlstate.SelectedValue, ddlDistrict);
        tbAddress.Text = "";
       
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
    protected void ddlstate_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadDistrict(ddlstate.SelectedValue.ToString(), ddlDistrict);
    }
	protected void ddlWorkshop_SelectedIndexChanged(object sender, EventArgs e)
	{
		divWorkshops.Visible = false;
		if (ddlWorkshop.SelectedValue=="Y")
		{
			divWorkshops.Visible = true;
		}
	}
	protected void lbtnFSSave_Click(object sender, EventArgs e)
    {
        if (IsValidValues() == false)
        {
            return;
        }
        Session["Action"] = "S";
        confirmmsg("Do you want to save filling station details?");
    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        insertFillingStation();
        LoadFillingStation();
    }
    protected void gvFillingStation_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvFillingStation.PageIndex = e.NewPageIndex;
        LoadFillingStation();
    }
    protected void gvFillingStation_RowCommand(object sender, GridViewCommandEventArgs e)//E1
    {
        try
        {
            if (e.CommandName == "gvEdit")
            {
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                tbName.Text = gvFillingStation.DataKeys[row.RowIndex]["fillingstn_name"].ToString();
                tbName.ReadOnly = true;
                ddlWorkshop.SelectedValue= gvFillingStation.DataKeys[row.RowIndex]["workshoppermises_yn"].ToString();
                if (!DBNull.Value.Equals(gvFillingStation.DataKeys[row.RowIndex]["workshop_office_id"]))
                {
                    ddlWorkshoplist.SelectedValue = gvFillingStation.DataKeys[row.RowIndex]["workshop_office_id"].ToString();
                }
                tbContactNumber1.Text = gvFillingStation.DataKeys[row.RowIndex]["contactno1"].ToString();
                if (!DBNull.Value.Equals(gvFillingStation.DataKeys[row.RowIndex]["contactno2"]))
                {
                    tbContactNumber2.Text = gvFillingStation.DataKeys[row.RowIndex]["contactno2"].ToString();
                }
                if (!DBNull.Value.Equals(gvFillingStation.DataKeys[row.RowIndex]["longi_tude"]))
                {
                    tbLongitude.Text = gvFillingStation.DataKeys[row.RowIndex]["longi_tude"].ToString();
                }
                if (!DBNull.Value.Equals(gvFillingStation.DataKeys[row.RowIndex]["lati_tude"]))
                {
                    tbLatitude.Text = gvFillingStation.DataKeys[row.RowIndex]["lati_tude"].ToString();
                }
                loadStates(ddlstate);
                ddlstate.SelectedValue = gvFillingStation.DataKeys[row.RowIndex]["statecode"].ToString();
                loadDistrict(ddlstate.SelectedValue, ddlDistrict);
                ddlDistrict.SelectedValue= gvFillingStation.DataKeys[row.RowIndex]["districtcode"].ToString();
                tbAddress.Text= gvFillingStation.DataKeys[row.RowIndex]["adrs"].ToString();
                Session["Stn_id"] = gvFillingStation.DataKeys[row.RowIndex]["fillingstn_id"].ToString();
              
                lblAddNewFS.Visible = false;
                lblUpdateFS.Visible = true;
                lbtnFSUpdate.Visible = true;
                lbtnFSSave.Visible = false;
               
            }
            if (e.CommandName == "gvDelete")
            {
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                int i = row.RowIndex;
                Session["Action"] = "D";
                Session["Stn_id"] = gvFillingStation.DataKeys[row.RowIndex]["fillingstn_id"].ToString();
                confirmmsg("Do you want to Delete Filling Station Details ?");
           }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("DepAdminFillingStationMgmt-E1", ex.Message.ToString());
        }
    }
    protected void lbtnFSUpdate_Click(object sender, EventArgs e)
    {
        if (IsValidValues() == false)
        {
            return;
        }
        Session["Action"] = "U";
        confirmmsg("Do you want to update filling station details?");
    }
    protected void lbtnview_Click(object sender, EventArgs e)
    {
        string msg = "";
        msg = msg + "1. Add depot wise filling station.<br/>";
        msg = msg + "2. Depot mananger can create filling station of its depot only.<br/>";
        msg = msg + "3. If filling station is used, it cannot be deleted.<br/>";
        msg = msg + "4. Except filling station name, other details can be updated.<br/>";
        InfoMsg(msg);
    }
    protected void lbtnFSReset_Click(object sender, EventArgs e)
    {
        Reset();
    }
  
    #endregion





}