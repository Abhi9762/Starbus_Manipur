﻿
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_Reports_rpt_M25 : BasePage
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
        Session["_moduleName"] = "Tank Report";
        if (!IsPostBack)
        {

            Session["Depot"] = null;
            loadReportType(Session["_RoleCode"].ToString());
            loadDepot();
            loadFillingStation();
            if (Session["_ReportType"] != null && Session["_ReportType"].ToString() != "")
            {
                ddlReportType.SelectedValue = "1";
                loadReport(Session["_RoleCode"].ToString(), ddlReportType.SelectedValue.ToString());
                ddlReport.SelectedValue = Session["_Report"].ToString();

                lbtnSearch.Visible = true;
                lbldepo.Visible = true;
                lblStation.Visible = true;
                ddlstation.Visible = true;
                ddldepo.Visible = true;
                lbtnSearch.Visible = true;
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
            ddldepo.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_depot_list");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddldepo.DataSource = dt;
                    ddldepo.DataTextField = "depot_name";
                    ddldepo.DataValueField = "depot_code";
                    ddldepo.DataBind();
                }
            }

            ddldepo.Items.Insert(0, "All");
            ddldepo.Items[0].Value = "0";
            ddldepo.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddldepo.Items.Insert(0, "SELECT");
            ddldepo.Items[0].Value = "0";
            ddldepo.SelectedIndex = 0;
            _common.ErrorLog("rpt_M25.aspx-001", ex.Message.ToString());
        }
    }
    private void loadFillingStation()
    {
        try
        {
            ddlstation.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_tank_filling_station");
            MyCommand.Parameters.AddWithValue("p_officeid", ddldepo.SelectedValue);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlstation.DataSource = dt;
                    ddlstation.DataTextField = "fillingstn_name";
                    ddlstation.DataValueField = "fillingstn_id";
                    ddlstation.DataBind();
                }
            }

            ddlstation.Items.Insert(0, "All");
            ddlstation.Items[0].Value = "0";
            ddlstation.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlstation.Items.Insert(0, "SELECT");
            ddlstation.Items[0].Value = "0";
            ddlstation.SelectedIndex = 0;
            _common.ErrorLog("rpt_M25.aspx-002", ex.Message.ToString());
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
            _common.ErrorLog("rpt_M25.aspx-003", ex.Message.ToString());
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
            _common.ErrorLog("rpt_M25.aspx-004", ex.Message.ToString());
        }

    }
    public void loadGrid()
    {
        try
        {
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "reports.f_get_rpt_m25");
            MyCommand.Parameters.AddWithValue("p_depot", ddldepo.SelectedValue);
            MyCommand.Parameters.AddWithValue("p_stationid", Convert.ToInt32(ddlstation.SelectedValue));
            DataTable dt = new DataTable();
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {

                gvTank.DataSource = dt;
                gvTank.DataBind();
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
            _common.ErrorLog("rpt_M25.aspx-005", ex.Message.ToString());
        }
    }
    private void Test()
    {
        try
        {
            if (gvTank.Rows.Count > 0)
            {
                gvTank.UseAccessibleHeader = true;
                gvTank.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        catch (Exception ex)
        {
            _common.ErrorLog("rpt_M25.aspx-006", ex.Message.ToString());
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


            pnlMsg.Visible = true;
            pnlReport.Visible = false;
            lbldepo.Visible = false;
            lblStation.Visible = false;
            ddlstation.Visible = false;
            ddldepo.Visible = false;
            lbtnSearch.Visible = false;
            Session["Depot"] = null;
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
    protected void lbtnSearch_Click(object sender, EventArgs e)
    {
        if (hidtoken.Value != Session["rndNoCheck"].ToString())
        {
            Response.Redirect("errorpage.aspx");
        }
        Session["Depot"] = null;
        loadGrid();
    }
    protected void ddlReportType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (hidtoken.Value != Session["rndNoCheck"].ToString())
        {
            Response.Redirect("errorpage.aspx");
        }
        pnlMsg.Visible = true;
        pnlReport.Visible = false;
        lbldepo.Visible = false;
        lblStation.Visible = false;
        ddlstation.Visible = false;
        ddldepo.Visible = false;
        lbtnSearch.Visible = false;

        ddlReport.Items.Clear();
        ddlReport.Items.Insert(0, "Select");
        ddlReport.Items[0].Value = "0";
        ddlReport.SelectedIndex = 0;

        loadReport(Session["_RoleCode"].ToString(), ddlReportType.SelectedValue.ToString());
    }
    protected void gvTank_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (hidtoken.Value != Session["rndNoCheck"].ToString())
        {
            Response.Redirect("errorpage.aspx");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                DataRowView rowView = (DataRowView)e.Row.DataItem;

                if (Session["Depot"] != null)
                {
                    if (Session["Depot"].ToString().ToUpper() == rowView["val_depot"].ToString().ToUpper())
                        e.Row.Cells[1].Text = "";
                    else
                    {
                        Session["Depot"] = rowView["val_depot"].ToString();

                        e.Row.Cells[1].Visible = true;
                    }
                }
                else
                    Session["Depot"] = rowView["val_depot"].ToString();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }
    #endregion




}


