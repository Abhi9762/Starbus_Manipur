using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_Reports_rpt_M3 : BasePage
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
        Session["_moduleName"] = "Office Reports";
        if (!IsPostBack)
        {
            Session["Report"] = null;
            Session["OfficeLvl"] = null;
            Session["Office"] = null;
            Session["District"] = null;
            loadReportType(Session["_RoleCode"].ToString());
            loadOfficeLevel();

            if (Session["_ReportType"] != null && Session["_ReportType"].ToString() != "")
            {
                ddlReportType.SelectedValue = "1";
                loadReport(Session["_RoleCode"].ToString(), ddlReportType.SelectedValue.ToString());
                ddlReport.SelectedValue = Session["_Report"].ToString();
                lbloffice.Visible = true;
                ddlofficelevel.Visible = true;
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
            _common.ErrorLog("rpt_M3.aspx-001", ex.Message.ToString());
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
            _common.ErrorLog("rpt_M3.aspx-002", ex.Message.ToString());
        }

    }
    private void loadOfficeLevel()
    {
        ddlofficelevel.Items.Clear();
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_officelvl");
            dt = bll.SelectAll(MyCommand);

            if (dt.Rows.Count > 0)
            {
                ddlofficelevel.DataSource = dt;
                ddlofficelevel.DataTextField = "ofclvl_name";
                ddlofficelevel.DataValueField = "ofclvl_id";
                ddlofficelevel.DataBind();
            }
            ddlofficelevel.Items.Insert(0, "All");
            ddlofficelevel.Items[0].Value = "0";
            ddlofficelevel.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlofficelevel.Items.Insert(0, "All");
            ddlofficelevel.Items[0].Value = "0";
            ddlofficelevel.SelectedIndex = 0;

            _common.ErrorLog("rpt_M3.aspx-003", ex.Message.ToString());
        }
    }
    private void loadOffice()
    {
        try
        {
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "reports.f_get_rpt_m3");

            MyCommand.Parameters.AddWithValue("p_office_level", Convert.ToInt32(ddlofficelevel.SelectedValue));


            DataTable dt = new DataTable();
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                gvoffice.DataSource = dt;
                gvoffice.DataBind();
                DataRow[] WorkShopcount = dt.Select("val_ofclvlname='Workshop'");
                lblWorkshopCount.Text = WorkShopcount.Length.ToString();
                DataRow[] HQCOUNT = dt.Select("val_ofclvlname='HQ'");
                lblHqCount.Text = HQCOUNT.Length.ToString();
                DataRow[] DivCount = dt.Select("val_ofclvlname='Division'");
                lblDivisionCount.Text = DivCount.Length.ToString();
                DataRow[] DepotCount = dt.Select("val_ofclvlname='Depot'");
                lblDepotCount.Text = DepotCount.Length.ToString();
                DataRow[] SsiCount = dt.Select("val_ofclvlname='SSI'");
                lblSsiCount.Text = SsiCount.Length.ToString();
                Test();
                pnlmsg.Visible = false;
                pnlreport.Visible = true;
            }
            else
            {
                pnlmsg.Visible = true;
                pnlreport.Visible = false;
                errorMassage("No Record Found");

            }

        }

        catch (Exception ex)
        {
            _common.ErrorLog("rpt_M3.aspx-004", ex.Message.ToString());
        }
    }
    private void Test()
    {
        try
        {

            if (gvoffice.Rows.Count > 0)
            {
                gvoffice.UseAccessibleHeader = true;
                gvoffice.HeaderRow.TableSection = TableRowSection.TableHeader;
            }

        }

        catch (Exception ex)
        {
            _common.ErrorLog("rpt_M3.aspx-005", ex.Message.ToString());
        }

    }
    #endregion 

    #region "Event"
    protected void ddlReport_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlReport.SelectedIndex == 0)
        {
            lbtnsearch.Visible = false;
            ddlofficelevel.Visible = false;
            pnlmsg.Visible = true;
            pnlreport.Visible = false;
            lbloffice.Visible = false;
        }
        else
        {
            Session["_ReportType"] = ddlReportType.SelectedValue;
            Session["_Report"] = ddlReport.SelectedValue;
            Response.Redirect(ddlReport.SelectedValue);
        }

    }
    protected void lbtnsearch_Click(object sender, EventArgs e)
    {
        if (hidtoken.Value != Session["rndNoCheck"].ToString())
        {
            Response.Redirect("errorpage.aspx");
        }
        Session["Report"] = null;
        Session["OfficeLvl"] = null;
        Session["Office"] = null; ;
        Session["District"] = null;
        loadOffice();
    }
    protected void ddlReportType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (hidtoken.Value != Session["rndNoCheck"].ToString())
        {
            Response.Redirect("errorpage.aspx");
        }
        lbtnsearch.Visible = false;
        lbloffice.Visible = false;
        ddlofficelevel.Visible = false;
        pnlmsg.Visible = true;
        pnlreport.Visible = false;
        ddlReport.Items.Clear();
        ddlReport.Items.Insert(0, "Select");
        ddlReport.Items[0].Value = "0";
        ddlReport.SelectedIndex = 0;

        loadReport(Session["_RoleCode"].ToString(), ddlReportType.SelectedValue.ToString());
    }
    protected void gvoffice_RowDataBound(object sender, GridViewRowEventArgs e)
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

                if (Session["Office"] != null)
                {
                    if (Session["Office"].ToString().ToUpper() == rowView["val_officename"].ToString().ToUpper())
                        e.Row.Cells[2].Text = "";
                    else
                    {
                        Session["Office"] = rowView["val_officename"].ToString();
                        e.Row.Cells[2].Visible = true;
                    }
                }
                else
                    Session["Office"] = rowView["val_officename"].ToString();

                if (Session["OfficeLvl"] != null)
                {
                    if (Session["OfficeLvl"].ToString().ToUpper() == rowView["val_ofclvlname"].ToString().ToUpper())
                        e.Row.Cells[1].Text = "";
                    else
                    {
                        Session["OfficeLvl"] = rowView["val_ofclvlname"].ToString();
                        e.Row.Cells[1].Visible = true;
                    }
                }
                else
                    Session["OfficeLvl"] = rowView["val_ofclvlname"].ToString();
                if (Session["Report"] != null)
                {
                    if (Session["Report"].ToString().ToUpper() == rowView["val_reportingofficename"].ToString().ToUpper())
                        e.Row.Cells[3].Text = "";
                    else
                    {
                        Session["Report"] = rowView["val_reportingofficename"].ToString();
                        e.Row.Cells[3].Visible = true;
                    }
                }
                else
                    Session["Report"] = rowView["val_reportingofficename"].ToString();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }

    #endregion
}


