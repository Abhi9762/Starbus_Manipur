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

public partial class Auth_dashetmSubmission : System.Web.UI.Page
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    string current_date = DateTime.Now.AddDays(1).ToString("dd") + "/" + DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("yyyy");
    string current_date_100 = DateTime.Now.AddDays(-10).ToString("dd") + "/" + DateTime.Now.AddDays(-10).ToString("MM") + "/" + DateTime.Now.AddDays(-10).ToString("yyyy");

    protected void Page_Load(object sender, EventArgs e)
    {
        checkForSecurity();
        if (IsPostBack == false)
        {
            txttodate.Text = current_date;
            txtfromdate.Text = current_date_100;
            LoadBusServices();
            LoadRoutes();
            LoadBusType();
            LoadSubmittedEtm();
        }
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
    #region"Methods"
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

    protected void LoadSubmittedEtm()
    {
        try
        {
            lbtnDownloadExcel.Visible = false;
            pnlNoRecord1.Visible = true;
            gvSubmittedEtm.Visible = false;
            string officeid = Session["_LDepotCode"].ToString();
            int servicetype = Convert.ToInt16(ddlServiceType.SelectedValue.ToString());
            int route = Convert.ToInt16(ddlRoutes.SelectedValue.ToString());
            string bustype = ddlBusType.SelectedValue.ToString();
            string fromdate = txtfromdate.Text.ToString();
            string todate = txttodate.Text.ToString();

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_submitted_waybillslist");
            MyCommand.Parameters.AddWithValue("p_busdepotcode", officeid);
            MyCommand.Parameters.AddWithValue("p_service", servicetype);
            MyCommand.Parameters.AddWithValue("p_bustype", ddlBusType.SelectedValue);
            MyCommand.Parameters.AddWithValue("p_fromdate", fromdate);
            MyCommand.Parameters.AddWithValue("p_todate", todate);
            MyCommand.Parameters.AddWithValue("p_route", Convert.ToInt32(ddlRoutes.SelectedValue));
            dt = bll.SelectAll(MyCommand);

            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    lbtnDownloadExcel.Visible = false;
                    gvSubmittedEtm.DataSource = dt;
                    gvSubmittedEtm.DataBind();
                    pnlNoRecord1.Visible = false;
                    gvSubmittedEtm.Visible = true;
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
    
    private void InfoMsg(string msg)
    {
        string popup = _popup.modalPopupLarge("I", "Information", msg, "Close");
        Response.Write(popup);
    }
    #endregion

    #region"Events"
    protected void lbtnsearch_Click(object sender, EventArgs e)
    {
        LoadSubmittedEtm();
    }

    protected void lbtnResetFilter_Click(object sender, EventArgs e)
    {
        LoadSubmittedEtm();
    }

    protected void lbtnview_Click(object sender, EventArgs e)
    {

    }

    protected void gvSubmittedEtm_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "viewcashslip")
        {
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            Session["_WAYBILLID"] = gvSubmittedEtm.DataKeys[RowIndex].Values["dutyrefno"];
            eDepositedSlip.Text = "<embed src = \"cashdepositslip.aspx\" style=\"min-height: 70vh; width: 100%\" />";
            mpDepositedConductor.Show();
        }
        if (e.CommandName == "viewdutyslip")
        {
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            Session["dutySlipRefno"] = gvSubmittedEtm.DataKeys[RowIndex].Values["dutyrefno"];
            //openSubDetailsWindow("DutySlip.aspx");
            eSlip.Text = "<embed src = \"DutySlip.aspx\" style=\"height: 80vh; width: 100%\" />";
            mpShowDutySlip.Show();
        }
        if (e.CommandName == "viewWaybill")
        {
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            Session["DUTYREFNO"] = gvSubmittedEtm.DataKeys[index].Values["dutyrefno"].ToString();
            //openSubDetailsWindow("Waybill.aspx");
            eWaybill.Text = "<embed src = \"Waybill.aspx\" style=\"height: 80vh; width: 100%\" />";
            mpShowWaybill.Show();
        }
    }
    #endregion


    protected void gvSubmittedEtm_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HtmlGenericControl DivCash;
            DivCash = (HtmlGenericControl)e.Row.FindControl("divCashStatus") as HtmlGenericControl;
            DivCash.Visible = false;

            DataRowView rowView = (DataRowView)e.Row.DataItem;

            Label lblcash = (Label)e.Row.FindControl("lblCashStatus");


            if (rowView["status_"].ToString() == "N")
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
}