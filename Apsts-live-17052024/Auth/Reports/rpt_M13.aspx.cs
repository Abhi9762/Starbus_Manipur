
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_Reports_rpt_M13 : BasePage
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
        Session["_moduleName"] = "Station Distance Reports";
        if (!IsPostBack)
        {

            Session["State"] = null;
            Session["District"] = null;
            Session["Bus"] = null;
            Session["Station"] = null;
            Session["Office"] = null;
            Session["ToStation"] = null;
            loadReportType(Session["_RoleCode"].ToString());
            loadState(ddlState);
            loadDistrict(ddlState, ddlDistrict);
            if (Session["_ReportType"] != null && Session["_ReportType"].ToString() != "")
            {
                ddlReportType.SelectedValue = "1";
                loadReport(Session["_RoleCode"].ToString(), ddlReportType.SelectedValue.ToString());
                ddlReport.SelectedValue = Session["_Report"].ToString();
                ddlState.Visible = true;
                ddlDistrict.Visible = true;
                lblstatus.Visible = true;
                lblstate.Visible = true;
                lbldistrict.Visible = true;
                ddlstatus.Visible = true;
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
    private void loadState(DropDownList ddlState)
    {
        try
        {
            ddlState.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.f_get_states");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlState.DataSource = dt;
                    ddlState.DataTextField = "stname";
                    ddlState.DataValueField = "stcode";
                    ddlState.DataBind();
                }
            }

            ddlState.Items.Insert(0, "All");
            ddlState.Items[0].Value = "0";
            ddlState.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlState.Items.Insert(0, "SELECT");
            ddlState.Items[0].Value = "0";
            ddlState.SelectedIndex = 0;
            _common.ErrorLog("rpt_M13.aspx-001", ex.Message.ToString());
        }
    }
    private void loadDistrict(DropDownList ddlState, DropDownList ddlDistrict)
    {
        try
        {
            ddlDistrict.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.f_get_district");
            MyCommand.Parameters.AddWithValue("p_statecode", ddlState.SelectedValue);
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

            ddlDistrict.Items.Insert(0, "All");
            ddlDistrict.Items[0].Value = "0";
            ddlDistrict.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlDistrict.Items.Insert(0, "SELECT");
            ddlDistrict.Items[0].Value = "0";
            ddlDistrict.SelectedIndex = 0;
            _common.ErrorLog("rpt_M13.aspx-002", ex.Message.ToString());
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
            _common.ErrorLog("rpt_M13.aspx-003", ex.Message.ToString());
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
            _common.ErrorLog("rpt_M13.aspx-004", ex.Message.ToString());
        }

    }
    public void loadStation()
    {
        try
        {
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "reports.f_get_rpt_m13");
            MyCommand.Parameters.AddWithValue("p_stateid", ddlState.SelectedValue.ToString());
            MyCommand.Parameters.AddWithValue("p_districtid", ddlDistrict.SelectedValue.ToString());
            MyCommand.Parameters.AddWithValue("p_status", ddlstatus.SelectedValue);
            DataTable dt = new DataTable();
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {

                gvdistance.DataSource = dt;
                gvdistance.DataBind();
                Test();
                pnlMsg.Visible = false;
                pnlReport.Visible = true;
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
            _common.ErrorLog("rpt_M13.aspx-005", ex.Message.ToString());
        }
    }
    private void Test()
    {
        try
        {
            if (gvdistance.Rows.Count > 0)
            {
                gvdistance.UseAccessibleHeader = true;
                gvdistance.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        catch (Exception ex)
        {
            _common.ErrorLog("rpt_M13.aspx-006", ex.Message.ToString());
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
            ddlDistrict.Visible = false;
            ddlState.Visible = false;
            lbldistrict.Visible = false;
            lblstate.Visible = false;
            lblstatus.Visible = false;
            ddlstatus.Visible = false;
            pnlMsg.Visible = false;
            pnlReport.Visible = false;
            Session["Bus"] = null;
            Session["Station"] = null;
            Session["Office"] = null;
            lblstatus.Visible = false;
            ddlstatus.Visible = false;
            lbtnsearch.Visible = false;
        }
        else
        {
            Session["_ReportType"] = ddlReportType.SelectedValue;
            Session["_Report"] = ddlReport.SelectedValue;
            Response.Redirect(ddlReport.SelectedValue);
        }

    }
    protected void lbtnSearch_Click(object sender, EventArgs e)
    {
        if (hidtoken.Value != Session["rndNoCheck"].ToString())
        {
            Response.Redirect("errorpage.aspx");
        }
        Session["Bus"] = null;
        Session["Station"] = null;
        Session["Office"] = null;
        loadStation();
    }
    protected void ddlReportType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (hidtoken.Value != Session["rndNoCheck"].ToString())
        {
            Response.Redirect("errorpage.aspx");
        }

        lbtnsearch.Visible = false;
        ddlDistrict.Visible = false;
        ddlState.Visible = false;
        lbldistrict.Visible = false;
        lblstate.Visible = false;
        lblstatus.Visible = false;
        ddlstatus.Visible = false;
        pnlMsg.Visible = false;
        pnlReport.Visible = false;
        ddlReport.Items.Clear();
        ddlReport.Items.Insert(0, "Select");
        ddlReport.Items[0].Value = "0";
        ddlReport.SelectedIndex = 0;

        loadReport(Session["_RoleCode"].ToString(), ddlReportType.SelectedValue.ToString());
    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (hidtoken.Value != Session["rndNoCheck"].ToString())
        {
            Response.Redirect("errorpage.aspx");
        }
        loadDistrict(ddlState, ddlDistrict);

    }
    protected void gvdistance_RowDataBound(object sender, GridViewRowEventArgs e)
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

                if (Session["State"] != null)
                {
                    if (Session["State"].ToString().ToUpper() == rowView["from_state_name"].ToString().ToUpper())
                        e.Row.Cells[1].Text = "";
                    else
                    {
                        Session["State"] = rowView["from_state_name"].ToString();

                        e.Row.Cells[1].Visible = true;
                    }
                }
                else
                {
                    Session["State"] = rowView["from_state_name"].ToString();
                }




                if (Session["District"] != null)
                {
                    if (Session["District"].ToString().ToUpper() == rowView["district_name"].ToString().ToUpper())
                        e.Row.Cells[2].Text = "";
                    else
                    {
                        Session["District"] = rowView["district_name"].ToString();

                        e.Row.Cells[2].Visible = true;
                    }
                }
                else
                {
                    Session["District"] = rowView["district_name"].ToString();
                }

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }

    #endregion
}


