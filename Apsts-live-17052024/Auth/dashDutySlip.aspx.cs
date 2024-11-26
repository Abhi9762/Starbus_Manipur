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


public partial class Auth_dashDutySlip : System.Web.UI.Page
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    string current_date = DateTime.Now.AddDays(1).ToString("dd") + "/" + DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("yyyy");
    string current_date_7 = DateTime.Now.AddDays(-7).ToString("dd") + "/" + DateTime.Now.AddDays(-7).ToString("MM") + "/" + DateTime.Now.AddDays(-7).ToString("yyyy");

    protected void Page_Load(object sender, EventArgs e)
    {
        checkForSecurity();
        if (IsPostBack == false)
        {
            txttodate.Text = current_date;
            txtfromdate.Text = current_date_7;
            LoadBusServices();
            LoadRoutes();
            LoadBusType();
            LoadAllotedServices();
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
        if (_security.isSessionExist(Session["_RNDIDENTIFIERTK"]) == true)
        {
            Session["_RNDIDENTIFIERTK"] = Session["_RNDIDENTIFIERTK"];
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
    protected void LoadAllotedServices()
    {
        try
        {
            lbtnDownloadExcel.Visible = false;
            pnlNoRecord1.Visible = true;
            gvAllotedDuties.Visible = false;
            string officeid = Session["_LDepotCode"].ToString();
            int servicetype = Convert.ToInt16(ddlServiceType.SelectedValue.ToString());
            int route = Convert.ToInt16(ddlRoutes.SelectedValue.ToString());
            string bustype = ddlBusType.SelectedValue.ToString();
            string fromdate = txtfromdate.Text.ToString();
            string todate = txttodate.Text.ToString();

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_dutyslipslist");
            MyCommand.Parameters.AddWithValue("p_busdepotcode", officeid);
            MyCommand.Parameters.AddWithValue("p_dutysliprefno", "0");
            MyCommand.Parameters.AddWithValue("p_service", servicetype);
            MyCommand.Parameters.AddWithValue("p_routeid", route);
            MyCommand.Parameters.AddWithValue("p_bustype", bustype);
            MyCommand.Parameters.AddWithValue("p_fromdate", fromdate);
            MyCommand.Parameters.AddWithValue("p_todate", todate);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    lbtnDownloadExcel.Visible = false;
                    gvAllotedDuties.DataSource = dt;
                    gvAllotedDuties.DataBind();
                    pnlNoRecord1.Visible = false;
                    gvAllotedDuties.Visible = true;
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
    private void ConfirmMsg(string msg)
    {
        lblConfirmErrorMsg.Text = msg;
        mpConfirmation.Show();
    }
    private void InfoMsg(string msg)
    {
        string popup = _popup.modalPopupLarge("I", "Information", msg, "Close");
        Response.Write(popup);
    }
    private void ExportGridAllotedDutyToExcel()
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=AllotedDutyList.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            // To Export all pages
            gvAllotedDuties.AllowPaging = false;
            this.LoadAllotedServices();
            gvAllotedDuties.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in gvAllotedDuties.HeaderRow.Cells)
                cell.BackColor = gvAllotedDuties.HeaderStyle.BackColor;
            foreach (GridViewRow row in gvAllotedDuties.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                        cell.BackColor = gvAllotedDuties.AlternatingRowStyle.BackColor;
                    else
                        cell.BackColor = gvAllotedDuties.RowStyle.BackColor;
                    cell.CssClass = "textmode";
                }
            }

            gvAllotedDuties.RenderControl(hw);
            // style to format numbers to string
            string style = "<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }
    #endregion
    #region"Events"
    protected void lbtnDownloadExcel_Click(object sender, EventArgs e)
    {
        ExportGridAllotedDutyToExcel();
    }

    protected void lbtnview_Click(object sender, EventArgs e)
    {
        string msg = "";
        msg = msg + "1. Provisional duty allotment of Bus/Driver/Conductor will be done from here.<br/>";
        msg = msg + "2. Only those Buses will be available which are free and whose diesel and inspection are done.<br/>";
        msg = msg + "3. Only those Crew will be available who are free and attendance are marked. Attendance of 1 day prior to Duty date must be marked for provisional duty allotment.<br/>";
        msg = msg + "4. By default mapped bus and crew of the service will be shown in grid. If it shows N/A then it means no bus or crew is mapped. In this case click on update button and select bus and crew.<br/>";
        msg = msg + "5. If you want to update Bus or Crew then click on update button and change the Bus Crew accordingly.<br/>";
        msg = msg + "6. If you want to do extra duty allotment, then set current date as Duty Date and click on search. Select service, mark Bus crew and allot duty.<br/>";
        msg = msg + "7. Once provisional duty allotment generated, it cannot be cancelled.<br/>";
        msg = msg + "8. You can also view all previously generated provional duty allocations.<br/>";
        InfoMsg(msg);
    }
    protected void lbtnResetFilter_Click(object sender, EventArgs e)
    {
        LoadAllotedServices();
    }
    protected void lbtnsearch_Click1(object sender, EventArgs e)
    {
        LoadAllotedServices();
    }
    protected void gvAllotedDuties_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "viewDuty")
        {
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            Session["dutySlipRefno"] = gvAllotedDuties.DataKeys[index].Values["dutyrefno"].ToString();
            // openSubDetailsWindow("DutyAllotmentSlip.aspx");
            eSlip.Text = "<embed src = \"DutySlip.aspx\" style=\"height: 80vh; width: 100%\" />";
            mpShowDuty.Show();
        }
    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {

    }
    #endregion

    
}