
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_Reports_rpt_M26 : BasePage
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
        checkForSecurity();
        Session["_moduleName"] = "Pump Reports";
        if (!IsPostBack)
        {


            loadReportType(Session["_RoleCode"].ToString());
            loadDepot();
            loadFillingStation();
            loadTank();
            if (Session["_ReportType"] != null && Session["_ReportType"].ToString() != "")
            {
                ddlReportType.SelectedValue = "1";
                loadReport(Session["_RoleCode"].ToString(), ddlReportType.SelectedValue.ToString());
                ddlReport.SelectedValue = Session["_Report"].ToString();
                ddldepot.Visible = true;
                ddlfngstation.Visible = true;
                ddltank.Visible = true;
                lbldepot.Visible = true;
                lbltank.Visible = true;
                lblstation.Visible = true;

                lbtnsearch.Visible = true;
                Session.Remove("_ReportType");
                Session.Remove("_Report");
            }

            Random randomclass = new Random();
            Session["rndNoCheck"] = randomclass.Next().ToString();
            hidtoken.Value = Session["rndNoCheck"].ToString();
        }
        lblReportName.Text = "Report " + ddlReport.SelectedItem.Text;
        Test();
    }

    #region "Method"
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
            Response.Redirect("sessionTimeout.aspx");
        }
        if (_security.isSessionExist(Session["_RNDIDENTIFIERSADM"]) == true)
        {
            Session["_RNDIDENTIFIERSADM"] = Session["_RNDIDENTIFIERSADM"];
        }
        else
        {
            Response.Redirect("sessionTimeout.aspx");
        }
        if (_security.checkvalidation() == false)
        {
            Response.Redirect("Errorpage.aspx");
        }
        if (_security.CheckOldUserLogin(Session["_UserCode"].ToString()) == false)
        {
            Response.Redirect("Errorpage.aspx");
        }
    }
    public void errorMassage(string msg)
    {
        lblerrmsg.Text = msg;
        mpError.Show();
    }
    private void loadDepot()
    {
        try
        {
            ddldepot.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_depot_list");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddldepot.DataSource = dt;
                    ddldepot.DataTextField = "depot_name";
                    ddldepot.DataValueField = "depot_code";
                    ddldepot.DataBind();
                }
            }

            ddldepot.Items.Insert(0, "All");
            ddldepot.Items[0].Value = "0";
            ddldepot.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddldepot.Items.Insert(0, "SELECT");
            ddldepot.Items[0].Value = "0";
            ddldepot.SelectedIndex = 0;
            _common.ErrorLog("rpt_M26.aspx-001", ex.Message.ToString());
        }
    }
    private void loadTank()
    {
        try
        {
            ddltank.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation2.f_get_tank");
            MyCommand.Parameters.AddWithValue("p_fillingstn", Convert.ToInt32(ddlfngstation.SelectedValue));
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddltank.DataSource = dt;
                    ddltank.DataTextField = "tank_no";
                    ddltank.DataValueField = "tank_no";
                    ddltank.DataBind();
                }
            }

            ddltank.Items.Insert(0, "All");
            ddltank.Items[0].Value = "0";
            ddltank.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddltank.Items.Insert(0, "SELECT");
            ddltank.Items[0].Value = "0";
            ddltank.SelectedIndex = 0;
            _common.ErrorLog("rpt_M26.aspx-002", ex.Message.ToString());
        }
    }
    private void loadFillingStation()
    {
        try
        {
            ddlfngstation.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_tank_filling_station");
            MyCommand.Parameters.AddWithValue("p_officeid", ddldepot.SelectedValue);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlfngstation.DataSource = dt;
                    ddlfngstation.DataTextField = "fillingstn_name";
                    ddlfngstation.DataValueField = "fillingstn_id";
                    ddlfngstation.DataBind();
                }
            }

            ddlfngstation.Items.Insert(0, "All");
            ddlfngstation.Items[0].Value = "0";
            ddlfngstation.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlfngstation.Items.Insert(0, "SELECT");
            ddlfngstation.Items[0].Value = "0";
            ddlfngstation.SelectedIndex = 0;
            _common.ErrorLog("rpt_M26.aspx-003", ex.Message.ToString());
        }
    }
    private void loadReport(string rolecode, string _rptype)
    {
        try
        {
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "reports.f_get_report");
            MyCommand.Parameters.AddWithValue("p_role", rolecode);
            MyCommand.Parameters.AddWithValue("p_reportcategoryid", Convert.ToInt32(_rptype));
            DataTable dt = new DataTable();
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                ddlReport.DataSource = dt;
                ddlReport.DataTextField = "val_report_name";
                ddlReport.DataValueField = "val_page_url";
                ddlReport.DataBind();
            }

            ddlReport.Items.Insert(0, "Select");
            ddlReport.Items[0].Value = "0";
            ddlReport.SelectedIndex = 0;




        }



        catch (Exception ex)
        {
            ddlReport.Items.Insert(0, "Select");
            ddlReport.Items[0].Value = "0";
            ddlReport.SelectedIndex = 0;
            _common.ErrorLog("rpt_M26.aspx-004", ex.Message.ToString());
        }

    }
    private void loadReportType(string rolecode)
    {
        try
        {
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "reports.f_get_report");
            MyCommand.Parameters.AddWithValue("p_role", rolecode);
            MyCommand.Parameters.AddWithValue("p_reportcategoryid", 0);
            DataTable dt = new DataTable();
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                ddlReportType.DataSource = dt;
                ddlReportType.DataTextField = "val_report_category_name";
                ddlReportType.DataValueField = "val_report_category_id";
                ddlReportType.DataBind();
            }

            ddlReportType.Items.Insert(0, "Select");
            ddlReportType.Items[0].Value = "0";
            ddlReportType.SelectedIndex = 0;

            ddlReport.Items.Insert(0, "Select");
            ddlReport.Items[0].Value = "0";
            ddlReport.SelectedIndex = 0;



        }



        catch (Exception ex)
        {
            ddlReportType.Items.Insert(0, "Select");
            ddlReportType.Items[0].Value = "0";
            ddlReportType.SelectedIndex = 0;

            ddlReport.Items.Insert(0, "Select");
            ddlReport.Items[0].Value = "0";
            ddlReport.SelectedIndex = 0;
            _common.ErrorLog("rpt_M26.aspx-005", ex.Message.ToString());
        }

    }
    public void loadGrid()
    {
        try
        {
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "reports.f_get_rpt_m26");
            MyCommand.Parameters.AddWithValue("p_fillingstation", Convert.ToInt32(ddldepot.SelectedValue));
            MyCommand.Parameters.AddWithValue("p_depot_id", ddlfngstation.SelectedValue);
            MyCommand.Parameters.AddWithValue("p_tank", ddltank.SelectedValue);
            DataTable dt = new DataTable();
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {

                gvpump.DataSource = dt;
                gvpump.DataBind();
                pnlMsg.Visible = false;
                pnlReport.Visible = true;
                Test();
            }
            else
            {
                pnlMsg.Visible = true;
                pnlReport.Visible = false;
                errorMassage("No Record Found");
            }

        }

        catch (Exception ex)
        {
            _common.ErrorLog("rpt_M26.aspx-006", ex.Message.ToString());
        }
    }
    private void Test()
    {
        try
        {
            if (gvpump.Rows.Count > 0)
            {
                gvpump.UseAccessibleHeader = true;
                gvpump.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        catch (Exception ex)
        {
            _common.ErrorLog("rpt_M26.aspx-007", ex.Message.ToString());
        }
    }
    #endregion 

    #region "Event"
    protected void ddlReport_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (hidtoken.Value != Session["rndNoCheck"].ToString())
        {
            Response.Redirect("errorpage.aspx");
        }
        if (ddlReport.SelectedIndex == 0)
        {
            lbtnsearch.Visible = false;
            ddldepot.Visible = false;
            ddlfngstation.Visible = false;
            ddltank.Visible = false;
            pnlMsg.Visible = true;
            pnlReport.Visible = false;
            lbldepot.Visible = false;
            lbltank.Visible = false;
            lblstation.Visible = false;

        }
        else
        {
            Session["_ReportType"] = ddlReportType.SelectedValue;
            Session["_Report"] = ddlReport.SelectedValue;
            Response.Redirect(ddlReport.SelectedValue);
        }

    }
    protected void ddldepo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (hidtoken.Value != Session["rndNoCheck"].ToString())
        {
            Response.Redirect("errorpage.aspx");
        }
        loadFillingStation();
    }
    protected void lbtnsearch_Click(object sender, EventArgs e)
    {
        if (hidtoken.Value != Session["rndNoCheck"].ToString())
        {
            Response.Redirect("errorpage.aspx");
        }
        loadGrid();
    }
    protected void ddlReportType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (hidtoken.Value != Session["rndNoCheck"].ToString())
        {
            Response.Redirect("errorpage.aspx");
        }
        lbtnsearch.Visible = false;
        ddldepot.Visible = false;
        ddlfngstation.Visible = false;
        ddltank.Visible = false;
        pnlMsg.Visible = true;
        pnlReport.Visible = false;
        lbldepot.Visible = false;
        lbltank.Visible = false;
        lblstation.Visible = false;
        ddlReport.Items.Clear();
        ddlReport.Items.Insert(0, "Select");
        ddlReport.Items[0].Value = "0";
        ddlReport.SelectedIndex = 0;
        loadReport(Session["_RoleCode"].ToString(), ddlReportType.SelectedValue.ToString());
    }
    protected void ddlfngstation_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (hidtoken.Value != Session["rndNoCheck"].ToString())
        {
            Response.Redirect("errorpage.aspx");
        }
        loadTank();
    }
    protected void ddldepot_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (hidtoken.Value != Session["rndNoCheck"].ToString())
        {
            Response.Redirect("errorpage.aspx");
        }
        loadFillingStation();
    }
    #endregion


}


