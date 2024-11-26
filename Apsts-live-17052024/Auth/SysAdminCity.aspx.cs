using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_SysAdminCity : BasePage
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

        Session["_moduleName"] = "City";
        if (!IsPostBack)
        {
            Random randomclass = new Random();
            Session["rndNoCheck"] = randomclass.Next().ToString();
            hidtoken.Value = Session["rndNoCheck"].ToString();
            loadcitylist();
            loadStates();
            loadDistrict();
        }
    }

    #region "Methods"
    public void loadStates()//M1
    {
        try
        {
            ddlCityState.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.f_get_stateslist");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlCityState.DataSource = dt;
                    ddlCityState.DataTextField = "stname";
                    ddlCityState.DataValueField = "stcode";
                    ddlCityState.DataBind();
                }
            }
            else
            {
                Errormsg("Something Went Wrong");
                _common.ErrorLog("SysAdminCity.aspx-0001", dt.TableName);
            }

            ddlCityState.Items.Insert(0, "SELECT");
            ddlCityState.Items[0].Value = "0";
            ddlCityState.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlCityState.Items.Insert(0, "SELECT");
            ddlCityState.Items[0].Value = "0";
            ddlCityState.SelectedIndex = 0;
            Errormsg("Something Went Wrong");
            _common.ErrorLog("SysAdminCity.aspx-0002", ex.Message.ToString());
        }
    }
    public void loadDistrict()//M2
    {
        try
        {
            ddlCityDistrict.Items.Clear();
            string State = ddlCityState.SelectedValue.ToString();

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.f_get_district");
            MyCommand.Parameters.AddWithValue("p_statecode", State);
            dt = bll.SelectAll(MyCommand);

            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlCityDistrict.DataSource = dt;
                    ddlCityDistrict.DataTextField = "distname";
                    ddlCityDistrict.DataValueField = "distcode";
                    ddlCityDistrict.DataBind();
                }
            }
            else
            {
                Errormsg("Something Went Wrong");
                _common.ErrorLog("SysAdminCity.aspx-0003", dt.TableName);
            }

            ddlCityDistrict.Items.Insert(0, "SELECT");
            ddlCityDistrict.Items[0].Value = "0";
            ddlCityDistrict.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlCityDistrict.Items.Insert(0, "SELECT");
            ddlCityDistrict.Items[0].Value = "0";
            ddlCityDistrict.SelectedIndex = 0;
            Errormsg("Something Went Wrong");
            _common.ErrorLog("SysAdminCity.aspx-0004", ex.Message.ToString());
        }
    }
    private void saveCity()//M3
    {
        try
        {
            string cityCode = Session["_cityCode"].ToString();
            string cityEng = tbCityNameEn.Text.ToString();
            string cityLocal = tbCityNameLocal.Text.ToString();
            string state = ddlCityState.SelectedValue.ToString();
            string district = ddlCityDistrict.SelectedValue.ToString();
            string UpdatedBy = Session["_UserCode"].ToString();
            string IpAddress = HttpContext.Current.Request.UserHostAddress;
            string action = Session["_action"].ToString();
            string result = "";
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.f_city_insertupdate");
            MyCommand.Parameters.AddWithValue("p_action", action);
            MyCommand.Parameters.AddWithValue("p_citycode", cityCode);
            MyCommand.Parameters.AddWithValue("p_cityeng", cityEng);
            MyCommand.Parameters.AddWithValue("p_citylocal", cityLocal);
            MyCommand.Parameters.AddWithValue("p_statecode", state);
            MyCommand.Parameters.AddWithValue("p_districtcode", district);
            MyCommand.Parameters.AddWithValue("p_updatedby", UpdatedBy);
            MyCommand.Parameters.AddWithValue("p_ipaddress", IpAddress);
            result = bll.InsertAll(MyCommand);
            if (result == "Success")
            {
                if (Session["_action"].ToString() == "D")
                {
                    Successmsg("City Deactivated Successfully");
                }
                else if (Session["_action"].ToString() == "A")
                {
                    Successmsg("City Activated Successfully");
                }
                else if (Session["_action"].ToString() == "R")
                {
                    Successmsg("City Deleted Successfully");
                }
                else if (Session["_action"].ToString() == "U")
                {
                    Successmsg("City details updated successfully");
                }
                else
                {
                    Successmsg("Details saved successfully");
                }
                Session["_cityCode"] = "";
                Session["_action"] = "";
                clearCityData();
                loadcitylist();
            }
            else
            {
                Errormsg("Something Went Wrong");
                _common.ErrorLog("SysAdminCity.aspx-0005", result);
            }
        }
        catch (Exception ex)
        {
            Errormsg("There is some error.");
            _common.ErrorLog("SysAdminCity.aspx-0006", ex.Message.ToString());
        }
    }
    private void loadcitylist()//M4
    {
        try
        {
            string searchText = tbSearch.Text.Trim();
            gvCity.Visible = false;
            pnlNoRecord.Visible = true;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.f_get_cities");
            MyCommand.Parameters.AddWithValue("p_city", searchText);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    gvCity.DataSource = dt;
                    gvCity.DataBind();
                    lblTotalCity.Text = dt.Rows[0]["totalcity"].ToString();
                    lblActive.Text = dt.Rows[0]["actcity"].ToString();
                    lblDeactive.Text = dt.Rows[0]["deactcity"].ToString();
                    //lblDeactStation.Text = dt.Rows[0]["deactstation"].ToString();
                    gvCity.Visible = true;
                    pnlNoRecord.Visible = false;
                   
                }
            }
            else
            {
                Errormsg("There is some error.");
                _common.ErrorLog("SysAdminCity.aspx-0007", dt.TableName);
            }

        }
        catch (Exception ex)
        {
            Errormsg("There is some error" + ex.Message);
            _common.ErrorLog("SysAdminCity.aspx-0008", ex.Message.ToString());
        }
    }
    private bool isCityExists()//M5
    {
        try
        {
            string searchText = tbCityNameEn.Text.Trim();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.f_get_cities");
            MyCommand.Parameters.AddWithValue("p_city", searchText);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    string msg = "City already exists for <b>" + dt.Rows[0]["state"].ToString() + "," + dt.Rows[0]["district"].ToString() + "</b>. Do you still want to save?";
                    ConfirmMsg(msg);
                    return true;
                }
            }
            else
            {
                Errormsg("There is some error");
                _common.ErrorLog("SysAdminCity.aspx-0009", dt.TableName);
            }

            return false;
        }
        catch (Exception ex)
        {
            Errormsg("There is some error");
            _common.ErrorLog("SysAdminCity.aspx-0009", ex.Message.ToString());
            return false;
        }
    }
    private bool validCity()//M6
    {
        try
        {
            int msgcount = 0;
            string msg = "";

            if (_validation.IsValidString(tbCityNameEn.Text, 1, tbCityNameEn.MaxLength) == false)
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Enter City Name (English).<br/>";
            }
            if (_validation.IsValidString(tbCityNameLocal.Text, 0, tbCityNameLocal.MaxLength) == false)
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Enter City Name (Local).<br/>";
            }
            if (ddlCityState.SelectedValue == "0")
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Select City State.<br/>";
            }
            if (ddlCityDistrict.SelectedValue == "0")
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Select City District.<br/>";
            }
            if (msgcount > 0)
            {
                Errormsg(msg);
                return false;
            }

            return true;
        }
        catch (Exception ex)
        {
            Errormsg("There is some error");
            _common.ErrorLog("SysAdminCity.aspx-0010", ex.Message.ToString());
            
            return false;
        }
    }
    public void clearCityData()
    {
        tbCityNameEn.Text = "";
        tbCityNameLocal.Text = "";
        loadStates();
        loadDistrict();
        ddlCityState.SelectedValue = "0";
        ddlCityDistrict.SelectedValue = "0";
        lblAddCityHeader.Visible = true;
        lblUpdateCityHeader.Visible = false;
        lbtnSave.Visible = true;
        lbtnUpdate.Visible = false;
        lbtnReset.Visible = true;
        lbtncancel.Visible = false;
ddlCityState.Enabled=true;
            ddlCityDistrict.Enabled=true;
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
    private void CsrfTokenValidate()
    {
        if (hidtoken.Value != Session["rndNoCheck"].ToString())
        {
            Response.Redirect("errorpage.aspx");
            return;
        }
    }
    #endregion

    #region "Events"
    protected void lbtndwnlinst_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        string msg = "";
        msg = msg + "Coming Soon";
        InfoMsg(msg);
    }
    protected void lbtnAddCity_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        pnlAddCity.Visible = true;
        loadDistrict();

    }
    protected void gvCity_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        CsrfTokenValidate();
        if (e.CommandName == "UpdateCity")
        {
            try
            {
                int index = Convert.ToInt32(e.CommandArgument);
                Session["_cityCode"] = gvCity.DataKeys[index].Values["city_code"].ToString();
                tbCityNameEn.Text = gvCity.DataKeys[index].Values["city"].ToString();
                tbCityNameLocal.Text = gvCity.DataKeys[index].Values["citylocal"].ToString();
                loadStates();
                ddlCityState.SelectedValue = gvCity.DataKeys[index].Values["state_code"].ToString();
                loadDistrict();
                ddlCityDistrict.SelectedValue = gvCity.DataKeys[index].Values["district_code"].ToString();
                lblAddCityHeader.Visible = false;
                lblUpdateCityHeader.Visible = true;
                pnlAddCity.Visible = true;
                lbtnSave.Visible = false;
                lbtnReset.Visible = false;
                lbtncancel.Visible = true;
                lbtnUpdate.Visible = true;
ddlCityState.Enabled=false;
            ddlCityDistrict.Enabled=false;
            }
            catch (Exception ex)
            {
                Errormsg("");
            }
        }
        if (e.CommandName == "activate")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            Session["_cityCode"] = gvCity.DataKeys[index].Values["city_code"].ToString();
            string cityName = gvCity.DataKeys[index].Values["city"].ToString();
            Session["_action"] = "activate";
            ConfirmMsg("Do you want to Activate " + cityName + " ?");
        }
        if (e.CommandName == "deactivate")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            Session["_cityCode"] = gvCity.DataKeys[index].Values["city_code"].ToString();
            string cityName = gvCity.DataKeys[index].Values["city"].ToString();
            Session["_action"] = "deactivate";
            ConfirmMsg("Do you want to Deactivate " + cityName + " ?");
        }
        if (e.CommandName == "deleteCity")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            Session["_cityCode"] = gvCity.DataKeys[index].Values["city_code"].ToString();
            string cityName = gvCity.DataKeys[index].Values["city"].ToString();
            Session["_action"] = "delete";
            ConfirmMsg("Do you want to Delete " + cityName + " ?");
        }
    }
    protected void gvCity_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbtnActivate = (LinkButton)e.Row.FindControl("lbtnActivate");
            LinkButton lbtnDiscontinue = (LinkButton)e.Row.FindControl("lbtnDiscontinue");
            LinkButton lbtnDelete = (LinkButton)e.Row.FindControl("lbtnDeleteCity");
            DataRowView rowView = (DataRowView)e.Row.DataItem;
            lbtnActivate.Visible = false;
            lbtnDiscontinue.Visible = false;
            lbtnDelete.Visible = false;
            if (rowView["discontinueyn"].ToString() == "Y")
            {
                lbtnDiscontinue.Visible = true;
            }
             if (rowView["status"].ToString() == "D")
            {
                lbtnDiscontinue.Visible = false;
                lbtnActivate.Visible = true;
            }
            if (rowView["deleteyn"].ToString() == "Y")
            {
                lbtnDiscontinue.Visible = false;
                lbtnDelete.Visible = true;
                lbtnActivate.Visible = false;
            }
        }
    }
    protected void ddlCityState_SelectedIndexChanged(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        loadDistrict();
    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (Session["_action"].ToString() == "S" || Session["_action"].ToString() == "U")
        {
            saveCity();
        }
        else if (Session["_action"].ToString() == "activate")
        {
            Session["_action"] = "A";
            saveCity();

        }
        else if (Session["_action"].ToString() == "deactivate")
        {
            Session["_action"] = "D";
            saveCity();
        }
        else if (Session["_action"].ToString() == "delete")
        {
            Session["_action"] = "R";
            saveCity();
        }
    }
    protected void saveCity_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (validCity() == false)
        {
            return;
        }
        Session["_cityCode"] = "0";
        Session["_action"] = "S";
        if (isCityExists() == true)
        {
            return;
        }
        ConfirmMsg("Do you want to save city?");
    }
    protected void updateCity_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (validCity() == false)
        {
            return;
        }
        Session["_action"] = "U";
        ConfirmMsg("Do you want to update city?");
    }
    protected void resetCity_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        clearCityData();
    }
    protected void cancelCity_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        clearCityData();
        pnlAddCity.Visible = true;
        lblAddCityHeader.Visible = true;
        lblUpdateCityHeader.Visible = false;
    }
    protected void lbtnSearchCity_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        loadcitylist();
    }
    protected void lbtnResetCity_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        tbSearch.Text = "";
        loadcitylist();
    }
    protected void lbtnview_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        string msg = "";
        msg = msg + "1. Here you can Add/Update state wise city.<br/>";
        InfoMsg(msg);
    }
    protected void gvCity_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvCity.PageIndex = e.NewPageIndex;
        loadcitylist();
    }
    #endregion

   
}