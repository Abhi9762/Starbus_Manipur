using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_SysAdmFareGeneration : System.Web.UI.Page
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    string Mresult = "";
    string commonerror = "There is some error. Please contact the administrator or try again after some time.";
    protected void Page_Load(object sender, EventArgs e)
    {
        lblSummary.Text = "Summary as on Date " + DateTime.Now.ToString("dd/MM/yyyy hh:ss tt");
        Session["_moduleName"] = "Fare Generation";
        if (IsPostBack == false)
        {
            getRoutes("");
            loadServicetype();
            getFareList();
            hfRouteId.Value = "0";
            hfServiceTypeId.Value = "0";
            pnlAddFare.Visible = true;
            pnlViewDetails.Visible = false;
        }
    }

    #region "Methods"

    private void getRoutes(string searchText)//M1
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_routes");
            MyCommand.Parameters.AddWithValue("p_route", searchText);
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                ddlRoutes.DataSource = dt;
                ddlRoutes.DataTextField = "routename";
                ddlRoutes.DataValueField = "routeid";
                ddlRoutes.DataBind();
            }
            ddlRoutes.Items.Insert(0, "SELECT");
            ddlRoutes.Items[0].Value = "0";
            ddlRoutes.SelectedIndex = 0;

        }
        catch (Exception ex)
        {
            Errormsg("There is some error" + ex.Message);
            _common.ErrorLog("FareGeneration-M1", ex.Message.ToString());
        }
    }
    public void loadServicetype()//M2
    {
        try
        {
            ddlServiceType.Items.Clear();
            ddlSServiceType.Items.Clear();

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_servicetype_getlist");
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                ddlServiceType.DataSource = dt;
                ddlServiceType.DataTextField = "servicetype_name_en";
                ddlServiceType.DataValueField = "srtpid";
                ddlServiceType.DataBind();
                ddlSServiceType.DataSource = dt;
                ddlSServiceType.DataTextField = "servicetype_name_en";
                ddlSServiceType.DataValueField = "srtpid";
                ddlSServiceType.DataBind();


            }
            ddlServiceType.Items.Insert(0, "SELECT");
            ddlServiceType.Items[0].Value = "0";
            ddlServiceType.SelectedIndex = 0;
            ddlSServiceType.Items.Insert(0, "SELECT");
            ddlSServiceType.Items[0].Value = "0";
            ddlSServiceType.SelectedIndex = 0;

        }
        catch (Exception ex)
        {
            ddlServiceType.Items.Insert(0, "SELECT");
            ddlServiceType.Items[0].Value = "0";
            ddlServiceType.SelectedIndex = 0;
            ddlSServiceType.Items.Insert(0, "SELECT");
            ddlSServiceType.Items[0].Value = "0";
            ddlSServiceType.SelectedIndex = 0;

            _common.ErrorLog("FareGeneration-M2", ex.Message.ToString());
        }
    }

    private void saveFare()//M3
    {
        try
        {
            string routeId = ddlRoutes.SelectedValue.ToString();
            string serviceTypeId = ddlServiceType.SelectedValue.ToString();
            string UpdatedBy = Session["_UserCode"].ToString();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_fr_fare_generation");
            MyCommand.Parameters.AddWithValue("p_userid", UpdatedBy);
            MyCommand.Parameters.AddWithValue("p_culture", "");
            MyCommand.Parameters.AddWithValue("p_route_id", Int32.Parse(routeId));
            MyCommand.Parameters.AddWithValue("p_srtp_id", Int32.Parse(serviceTypeId));
            DataTable dtt = new DataTable();
            dtt = bll.SelectAll(MyCommand);
            if (dtt.TableName == "Success")
            {
                if (dtt.Rows[0]["resultstr"].ToString() == "DONE")
                {
                    Successmsg("Fare Generated successfully.");
getFareList();
                }
                else if (dtt.Rows[0]["resultstr"].ToString() == "EXCEPTION")
                {
                    Errormsg(commonerror);
                }
                else
                {
                    Errormsg(commonerror+ dtt.Rows[0]["resultstr"].ToString());
                    _common.ErrorLog("FareGeneration-M3", dtt.TableName);
                }
            }
            else
            {
                Errormsg(commonerror);
                _common.ErrorLog("FareGeneration-M3", dtt.TableName);
            }
        }
        catch (Exception ex)
        {
            Errormsg(commonerror);
            _common.ErrorLog("FareGeneration-M3", ex.Message.ToString());
        }
    }
    public void getFareList()//M4
    {
        try
        {
            string ServiceType = ddlSServiceType.SelectedValue;
            string routeText = tbSRoute.Text;

            gvFare.Visible = false;
            pnlNoFare.Visible = true;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_fr_fare_get");
            MyCommand.Parameters.AddWithValue("p_route", routeText);
            MyCommand.Parameters.AddWithValue("p_service_type", Convert.ToInt16(ServiceType));
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                gvFare.DataSource = dt;
                gvFare.DataBind();
                gvFare.Visible = true;
                pnlNoFare.Visible = false;
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("FareGeneration-M4", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
        }
    }


    public void getFareDetailList()//M5
    {
        try
        {
            lblNoDetail.Text = "";

            string routeId = hfRouteId.Value.ToString();
            string serviceTypeId = hfServiceTypeId.Value.ToString();

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_fr_fare_get_detail");
            MyCommand.Parameters.AddWithValue("p_route_id", Int64.Parse(routeId));
            MyCommand.Parameters.AddWithValue("p_service_type", Convert.ToInt16(serviceTypeId));
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                gvFareDetail.DataSource = dt;
                gvFareDetail.DataBind();
            }
            else
            {
                lblNoDetail.Text = "No Record";
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("FareGeneration-M5", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
        }
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


    #endregion

    #region "Events"

    protected void lbtnSearch_Click(object sender, EventArgs e)
    {
        if (tbSRoute.Text.Trim().Length < 3 && tbSRoute.Text.Trim().Length > 1)
        {
            Errormsg("Please enter minimum 3 characters");
            return;
        }

        getFareList();
    }
    protected void lbtnResetSearch_Click(object sender, EventArgs e)
    {
        tbSRoute.Text = "";
        ddlSServiceType.SelectedValue = "0";
        getFareList();
    }
    protected void lbtnSave_Click(object sender, EventArgs e)
    {
        if (ddlRoutes.SelectedValue == "0")
        {
            Errormsg("Select Route");
            return;
        }
        if (ddlServiceType.SelectedValue == "0")
        {
            Errormsg("Select Service Type");
            return;
        }

        ConfirmMsg("Do you want to Generate Fare ?");
    }
    protected void lbtnReset_Click(object sender, EventArgs e)
    {
        ddlRoutes.SelectedValue = "0";
        ddlServiceType.SelectedValue = "0";
    }
    protected void lbtnview_Click(object sender, EventArgs e)
    {
        InfoMsg("Comming Soon");
    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        saveFare();
    }

    protected void gvFare_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

        }
    }
    protected void gvFare_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvFare.PageIndex = e.NewPageIndex;
        getFareList();
    }
    protected void gvFare_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewFare")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            
            string routeId = gvFare.DataKeys[index].Values["routeid"].ToString();
            string srtpId = gvFare.DataKeys[index].Values["srtpid"].ToString();
            
            string routeName = gvFare.DataKeys[index].Values["routename"].ToString();
            string srtpName = gvFare.DataKeys[index].Values["servicetype_name_en"].ToString();

            lblRouteName.Text = routeName;
            lblServiceTypeName.Text = srtpName;

            hfRouteId.Value = routeId;
            hfServiceTypeId.Value = srtpId;

            getFareDetailList();
            pnlAddFare.Visible = false;
            pnlViewDetails.Visible = true;

        }
    }
    protected void lbtnCloseDetail_Click(object sender, EventArgs e)
    {
        gvFareDetail.DataSource = null;
        gvFareDetail.DataBind();
        hfRouteId.Value = "0";
        hfServiceTypeId.Value = "0";
        pnlAddFare.Visible = true;
        pnlViewDetails.Visible = false;
    }
    protected void gvFareDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvFareDetail.PageIndex = e.NewPageIndex;
        getFareDetailList();
    }

    #endregion
}