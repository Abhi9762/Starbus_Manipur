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

public partial class Auth_sysAdmFCIGenaration : BasePage
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
        lblSummary.Text = "Summary as on Date " + DateTime.Now.ToString("dd/MM/yyyy hh:ss tt");
        Session["_moduleName"] = "FCI Generation";
        if (!IsPostBack)
        {
            loadServicetype();
            getRoutes();
            loadDepot();
        }
    }

    #region"Methods"
    public void loadServicetype()//M1
    {
        try
        {
            ddlsServiceTYpe.Items.Clear();
            ddlServicetype.Items.Clear();

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_servicetype_getlist");
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                ddlsServiceTYpe.DataSource = dt;
                ddlsServiceTYpe.DataTextField = "servicetype_name_en";
                ddlsServiceTYpe.DataValueField = "srtpid";
                ddlsServiceTYpe.DataBind();
                ddlServicetype.DataSource = dt;
                ddlServicetype.DataTextField = "servicetype_name_en";
                ddlServicetype.DataValueField = "srtpid";
                ddlServicetype.DataBind();


            }
            ddlsServiceTYpe.Items.Insert(0, "SELECT");
            ddlsServiceTYpe.Items[0].Value = "0";
            ddlsServiceTYpe.SelectedIndex = 0;
            ddlServicetype.Items.Insert(0, "SELECT");
            ddlServicetype.Items[0].Value = "0";
            ddlServicetype.SelectedIndex = 0;
            ddldepotservice.Items.Insert(0, "SELECT");
            ddldepotservice.Items[0].Value = "0";
            ddldepotservice.SelectedIndex = 0;


        }
        catch (Exception ex)
        {
            ddlsServiceTYpe.Items.Insert(0, "SELECT");
            ddlsServiceTYpe.Items[0].Value = "0";
            ddlsServiceTYpe.SelectedIndex = 0;
            ddlServicetype.Items.Insert(0, "SELECT");
            ddlServicetype.Items[0].Value = "0";
            ddlServicetype.SelectedIndex = 0;

            _common.ErrorLog("FareGeneration-M1", ex.Message.ToString());
        }
    }
    private void getRoutes()//M2
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_routes");
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                ddlRoute.DataSource = dt;
                ddlRoute.DataTextField = "routename";
                ddlRoute.DataValueField = "routeid";
                ddlRoute.DataBind();

                ddlsRoute.DataSource = dt;
                ddlsRoute.DataTextField = "routename";
                ddlsRoute.DataValueField = "routeid";
                ddlsRoute.DataBind();
            }
            ddlRoute.Items.Insert(0, "SELECT");
            ddlRoute.Items[0].Value = "0";
            ddlRoute.SelectedIndex = 0;
            ddlsRoute.Items.Insert(0, "SELECT");
            ddlsRoute.Items[0].Value = "0";
            ddlsRoute.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            Errormsg("There is some error" + ex.Message);
            _common.ErrorLog("FareGeneration-M2", ex.Message.ToString());
        }
    }
    public void loadDepot()//M3
    {
        try
        {
            Int32 OfcLvl = 30;
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
                _common.ErrorLog("SysAdmStationMgmt-M3", dt.TableName);
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
            _common.ErrorLog("SysAdmStationMgmt-M3", ex.Message.ToString());
        }
    }
    private void loadDepotService()//M4
    {
        try
        {
            ddldepotservice.Items.Clear();
            //Int32 OfcLvl = 30;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_depotservice_getlist_test");
            //MyCommand.Parameters.AddWithValue("p_ofclvlid", OfcLvl);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddldepotservice.DataSource = dt;
                    ddldepotservice.DataTextField = "servicename";
                    ddldepotservice.DataValueField = "dsvcid";
                    ddldepotservice.DataBind();
                }
            }
            else
            {
                _common.ErrorLog("SysAdmStationMgmt-M4", dt.TableName);
            }
            ddldepotservice.Items.Insert(0, "SELECT");
            ddldepotservice.Items[0].Value = "0";
            ddldepotservice.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddldepotservice.Items.Insert(0, "SELECT");
            ddldepotservice.Items[0].Value = "0";
            ddldepotservice.SelectedIndex = 0;
            _common.ErrorLog("SysAdmStationMgmt-M4", ex.Message.ToString());
        }
    }
    private void GenerateFCI()//M5
    {
        try
        {
            Successmsg("FCI Generate Successfully");
        }
        catch (Exception e)
        {
            Errormsg("SysAdmStationMgmt-M4 " + e.Message.ToString());
        }
    }
    private bool CheckFareGenerate()//M6
    {
        return true;
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
    #endregion

    #region"Events"
    protected void lbtndownload_Click(object sender, EventArgs e)
    {
        InfoMsg("Coming Soon");
    }
    protected void lbtnview_Click(object sender, EventArgs e)
    {
        InfoMsg("Coming Soon");
    }
    protected void lbtndwnldinst_Click(object sender, EventArgs e)
    {
        InfoMsg("Coming Soon");
    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        if (Session["Action"].ToString() == "S")
        {
            GenerateFCI();
        }

    }
    protected void ddlRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadDepotService();
    }
    protected void Unnamed_Click(object sender, EventArgs e)
    {

        if (ddlDepot.SelectedValue == "0")
        {
            Errormsg("Please Select Depot");
            return;
        }
        if (ddlServicetype.SelectedValue == "0")
        {
            Errormsg("Please Select Service Type");
            return;
        }
        if (ddlRoute.SelectedValue == "0")
        {
            Errormsg("Please Select Route");
            return;
        }
        if (ddldepotservice.SelectedValue == "0")
        {
            Errormsg("Please Select Depot Service");
            return;
        }
        if (CheckFareGenerate() == false)
        {
            Errormsg("Fare Not Generated for this route and Service Type.Please Generate Fare and then Retry.");
            return;
        }
        Session["Action"] = "S";
        ConfirmMsg("Do you want to generate FCI?");
    }
    protected void lbtnSave_Click(object sender, EventArgs e)
    {
        ddlDepot.SelectedValue = "0";
        ddldepotservice.SelectedValue = "0";
        ddlRoute.SelectedValue = "0";
        ddlServicetype.SelectedValue = "0";
    }
    #endregion












}