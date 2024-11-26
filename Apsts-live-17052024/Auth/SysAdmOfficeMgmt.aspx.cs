using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Npgsql;

public partial class Auth_SysAdmOfficeMgmt : BasePage
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
        Session["_moduleName"] = "Office Management";
        {

            if (IsPostBack == false)
            {
                Random randomclass = new Random();
                Session["rndNoCheck"] = randomclass.Next().ToString();
                hidtoken.Value = Session["rndNoCheck"].ToString();
                lblDateTime.Text = "Summary as on Date : " + DateTime.Now.ToString("dd-MM-yyyy h:mmtt");
                loadState(ddlstate);
                loadDistrict(ddlstate.SelectedValue, ddldistrict);
                loadofficelvl(ddllofficeLevel);
                loadofficelvl(ddlSOfficeLevel);

                loadrptoffice(ddllofficeLevel.SelectedValue, ddlreportingofc);
                loadOffices();
                CountBusOffice();

            }

        }

    }

    #region "Methods"
    private void loadState(DropDownList ddlstate)//M1
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
                Errormsg("Something Went Wrong");
                _common.ErrorLog("SysAdmOfficeMgmt.aspx-0001", dt.TableName);
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
            Errormsg("Something Went Wrong");
            _common.ErrorLog("SysAdmOfficeMgmt.aspx-0002", ex.Message.ToString());
        }
    }
    private void loadDistrict(string State_code, DropDownList ddldistrict)//M2
    {
        try
        {
            ddldistrict.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.f_get_district");
            MyCommand.Parameters.AddWithValue("p_statecode", State_code);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddldistrict.DataSource = dt;
                    ddldistrict.DataTextField = "distname";
                    ddldistrict.DataValueField = "distcode";
                    ddldistrict.DataBind();
                }
            }
            else
            {
                Errormsg("Something Went Wrong");
                _common.ErrorLog("SysAdmOfficeMgmt.aspx-0003", dt.TableName);
            }

            ddldistrict.Items.Insert(0, "SELECT");
            ddldistrict.Items[0].Value = "0";
            ddldistrict.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddldistrict.Items.Insert(0, "SELECT");
            ddldistrict.Items[0].Value = "0";
            ddldistrict.SelectedIndex = 0;
            Errormsg("Something Went Wrong");
            _common.ErrorLog("SysAdmOfficeMgmt.aspx-0004", ex.Message.ToString());
        }
    }
    private void loadofficelvl(DropDownList ddllofficeLevel)//M3
    {
        try
        {
            ddllofficeLevel.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_officelvl");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddllofficeLevel.DataSource = dt;
                    ddllofficeLevel.DataTextField = "ofclvl_name";
                    ddllofficeLevel.DataValueField = "ofclvl_id";
                    ddllofficeLevel.DataBind();
                }
            }
            else
            {
                Errormsg("Something Went Wrong");
                _common.ErrorLog("SysAdmOfficeMgmt.aspx-0005", dt.TableName);
            }
            ddllofficeLevel.Items.Insert(0, "SELECT");
            ddllofficeLevel.Items[0].Value = "0";
            ddllofficeLevel.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddllofficeLevel.Items.Insert(0, "SELECT");
            ddllofficeLevel.Items[0].Value = "0";
            ddllofficeLevel.SelectedIndex = 0;
            Errormsg("Something Went Wrong");
            _common.ErrorLog("SysAdmOfficeMgmt.aspx-0006", ex.Message.ToString());
        }
    }
    private void loadrptoffice(string ofclvlid, DropDownList ddlreportingofc)//M4
    {
        try
        {
            ddlreportingofc.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_rptoffice");
            MyCommand.Parameters.AddWithValue("p_ofclvlid", Convert.ToInt32(ofclvlid));
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlreportingofc.DataSource = dt;
                    ddlreportingofc.DataTextField = "officename";
                    ddlreportingofc.DataValueField = "officeid";
                    ddlreportingofc.DataBind();
                }
            }
            else
            {
                Errormsg("Something Went Wrong");
                _common.ErrorLog("SysAdmOfficeMgmt.aspx-0007", dt.TableName);
            }
            ddlreportingofc.Items.Insert(0, "SELECT");
            ddlreportingofc.Items[0].Value = "0";
            ddlreportingofc.SelectedIndex = 0;

            if (ddllofficeLevel.SelectedValue.ToString() == "10")
            {
                ddlreportingofc.Items.Clear();
                ddlreportingofc.Items.Insert(0, "None");
                ddlreportingofc.Items[0].Value = "0";
                ddlreportingofc.SelectedIndex = 0;
                ddlreportingofc.Enabled = false;
            }
            else
            {
                ddlreportingofc.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            ddlreportingofc.Items.Insert(0, "SELECT");
            ddlreportingofc.Items[0].Value = "0";
            ddlreportingofc.SelectedIndex = 0;
            Errormsg("Something Went Wrong");
            _common.ErrorLog("SysAdmOfficeMgmt.aspx-0008", ex.Message.ToString());
        }
    }
    public bool validvalue()//M5
    {
        try
        {
            string msg = "";
            int msgcont = 0;

            if (_validation.IsValidString(tbofficename.Text, 1, tbofficename.MaxLength) == false)
            {
                msgcont = msgcont + 1;
                msg = msg + msgcont.ToString() + ". Enter Valid Office Name" + "<br />";
            }
            if (_validation.IsValidString(tbaddress.Text, 0, tbaddress.MaxLength) == false)
            {
                msgcont = msgcont + 1;
                msg = msg + msgcont.ToString() + ". Enter Valid Office Address" + "<br />";
            }
            if (ddlstate.SelectedValue == "0" | ddlstate.SelectedValue == "")
            {
                msgcont = msgcont + 1;
                msg = msg + msgcont.ToString() + ". Select State" + "<br />";
            }
            if (ddldistrict.SelectedValue == "0" | ddldistrict.SelectedValue == "")
            {
                msgcont = msgcont + 1;
                msg = msg + msgcont.ToString() + ". Select District" + "<br />";
            }
            if (ddllofficeLevel.SelectedValue == "0" | ddllofficeLevel.SelectedValue == "")
            {
                msgcont = msgcont + 1;
                msg = msg + msgcont.ToString() + ". Select Office Level" + "<br />";
            }
            if (ddllofficeLevel.SelectedValue != "10")
            {
                if (ddlreportingofc.SelectedValue == "0" | ddlreportingofc.SelectedValue == "")
                {
                    msgcont = msgcont + 1;
                    msg = msg + msgcont.ToString() + ". Select Reporting Office" + "<br />";
                }
            }
            if (tblandline1.Text != "")
            {
                if (_validation.isValidLandLineNo(tblandline1.Text) == false)
                {
                    msgcont = msgcont + 1;
                    msg = msg + msgcont.ToString() + ". Enter Valid Landline Number 1 i.e. 05460-756574" + "<br />";
                }
            }
            if (tblandline2.Text != "")
            {
                if (_validation.isValidLandLineNo(tblandline2.Text) == false)
                {
                    msgcont = msgcont + 1;
                    msg = msg + msgcont.ToString() + ". Enter Valid Landline Number 2 i.e. 05460-756574" + "<br />";
                }
            }
            if (tbmobileno.Text != "")
            {
                if (_validation.isValidMobileNumber(tbmobileno.Text) == false)
                {
                    msgcont = msgcont + 1;
                    msg = msg + msgcont.ToString() + ". Enter Valid Mobile i.e. 9999999999" + "<br />";
                }
            }
            if (tbemail.Text != "")
            {
                if (_validation.isValideMailID(tbemail.Text) == false)
                {
                    msgcont = msgcont + 1;
                    msg = msg + msgcont.ToString() + ". Enter Valid Email Address" + "<br />";
                }
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
            _common.ErrorLog("SysAdmOfficeMgmt-M5", ex.Message.ToString());
            return false;
        }
    }
    public void reset()
    {
        tbofficename.Text = "";
        tbaddress.Text = "";
        loadState(ddlstate);
        loadDistrict(ddlstate.SelectedValue, ddldistrict);
        loadofficelvl(ddllofficeLevel);
        loadrptoffice(ddllofficeLevel.SelectedValue, ddlreportingofc);
        tblandline1.Text = "";
        tblandline2.Text = "";
        tbmobileno.Text = "";
        tbemail.Text = "";
        lbtnsaveoffice.Visible = true;
        lbtnupdateoffice.Visible = false;
        lbtnresetbutton.Visible = true;
        lbladdnewofficeHeading.Visible = true;
        lblUpdateofficeHeading.Visible = false;
        lbtnCancel.Visible = false;
        lbtnUnitSaveOffice.Visible = true;
        lbtnUnitUpdateOffice.Visible = false;
        //lblAddUnitOffice.Text = "Add Unit Office For " + Session["_officeName"].ToString();

        tbUnitaddress.Text = "";
        tbUnitemail.Text = "";
        tbUnitlandline1.Text = "";
        tbUnitlandline2.Text = "";
        tbUnitmobileno.Text = "";
        tbUnitofficename.Text = "";

        tbDeadKm.Text = "";

    }
    private void Errormsg(string msg)
    {

        string popup = _popup.modalPopupSmall("D", "Please Check", msg, "Close");
        Response.Write(popup);
    }
    private void Successmsg(string msg)
    {
        string popup = _popup.modalPopupSmall("S", "Success", msg, "Close");
        Response.Write(popup);
    }
    public void savedetailsOffice()//M6
    {
        try
        {
            string ipaddress = HttpContext.Current.Request.UserHostAddress;
            string p_action = Session["P_Action"].ToString();
            string p_officeid = "0";
            if (p_action != "S")
            {
                p_officeid = Session["officeId"].ToString();
            }

            string p_officename = tbofficename.Text;
            string p_officeaddress = tbaddress.Text;
            string p_statecode = ddlstate.SelectedValue;
            string p_districtcode = ddldistrict.SelectedValue;
            string p_reportingofc = ddlreportingofc.SelectedValue;
            string p_landline1 = tblandline1.Text;
            string p_landline2 = tblandline2.Text;
            string p_mobileno = tbmobileno.Text;
            string p_email = tbemail.Text;
            int Deadkm = 0;
            if (tbDeadKm.Text == "")
            {
                Deadkm = 0;
            }
            else
            {
                Deadkm = Convert.ToInt32(tbDeadKm.Text.ToString());
            }



            int p_ofclvl = Convert.ToInt16(ddllofficeLevel.SelectedValue.ToString());
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            string Mresult = "";
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_office_insertupdate");
            MyCommand.Parameters.AddWithValue("p_action", p_action);
            MyCommand.Parameters.AddWithValue("p_officeid", p_officeid);
            MyCommand.Parameters.AddWithValue("p_officename", p_officename);
            MyCommand.Parameters.AddWithValue("p_officeaddress", p_officeaddress);
            MyCommand.Parameters.AddWithValue("p_statecode", p_statecode);
            MyCommand.Parameters.AddWithValue("p_districtcode", p_districtcode);
            MyCommand.Parameters.AddWithValue("p_ofclvl", p_ofclvl);
            MyCommand.Parameters.AddWithValue("p_reportingofc", p_reportingofc);
            MyCommand.Parameters.AddWithValue("p_landline1", p_landline1);
            MyCommand.Parameters.AddWithValue("p_landline2", p_landline2);
            MyCommand.Parameters.AddWithValue("p_mobileno", p_mobileno);
            MyCommand.Parameters.AddWithValue("p_email", p_email);
            MyCommand.Parameters.AddWithValue("p_deadkm", Deadkm);
            MyCommand.Parameters.AddWithValue("p_activeyn", "");
            MyCommand.Parameters.AddWithValue("p_updatedby", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("p_ipaddress", ipaddress);
            dt = bll.SelectAll(MyCommand);
            //Response.Write(dt.TableName);
            if (dt.TableName == "Success")
            {
                if (dt.Rows[0]["pstatus"].ToString() == "NOT DELETE")
                {
                    Errormsg("Office Not Delete");
                }
                if (dt.Rows[0]["pstatus"].ToString() == "Y")
                {
                    if (p_action == "S")
                    {
                        Successmsg("Office Details Successfully Saved");
                    }
                    if (p_action == "U")
                    {
                        Successmsg("Office Details Successfully Updated");
                    }
                    if (p_action == "R")
                    {
                        Successmsg("Office Details Successfully Deleted");
                    }
                    if (p_action == "D")
                    {
                        Successmsg("Office Discontinue Successfully");
                    }
                    if (p_action == "A")
                    {
                        Successmsg("Office Activate Successfully");
                    }
                    loadOffices();
                    CountBusOffice();
                    reset();
                    Session["officeId"] = null;
                }
                else
                {
                    _common.ErrorLog("SysAdmOfficeMgmt.aspx-0009", dt.TableName);
                    Errormsg("Depot cannot be deleted as chest is assigned to the depot.");
                    reset();
                }
            }
            else
            {
                _common.ErrorLog("SysAdmOfficeMgmt.aspx-0010", dt.TableName);
                Errormsg("Unable to Save Office Details. Please try after Some time.");
                reset();
            }
        }
        catch (Exception ex)
        {
            Errormsg("Something Went Wrong");
            _common.ErrorLog("SysAdmOfficeMgmt.aspx-0011", ex.Message.ToString());
        }

    }
    protected void loadOffices()//M7
    {
        try
        {
            string ofcName = tbSOfcName.Text.Trim();
            Int32 ofclvl = Convert.ToInt32(ddlSOfficeLevel.SelectedValue);
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_office");
            MyCommand.Parameters.AddWithValue("p_office", ofcName);
            MyCommand.Parameters.AddWithValue("p_officelevel_id", ofclvl);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    gvOffices.DataSource = dt;
                    gvOffices.DataBind();
                    gvOffices.Visible = true;
                    imgNorecord.Visible = false;
                }
                else
                {
                    gvOffices.Visible = false;
                    imgNorecord.Visible = true;
                }
            }
            else
            {
                Errormsg(dt.TableName.ToString());
                _common.ErrorLog("SysAdmOfficeMgmt.aspx-0012", dt.TableName);
            }

        }
        catch (Exception ex)
        {
            _common.ErrorLog("SysAdmOfficeMgmt.aspx-0012", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
            gvOffices.Visible = false;
            imgNorecord.Visible = true;
        }
    }
    private void CountBusOffice()//M8
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_officecount");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    lblhq.Text = dt.Rows[0]["hqofc"].ToString();
                    lblDivision.Text = dt.Rows[0]["divofc"].ToString();
                    lblDepot.Text = dt.Rows[0]["dptofc"].ToString();
                    lblStation.Text = dt.Rows[0]["stnd"].ToString();
                }
            }
            else
            {
                _common.ErrorLog("SysAdmOfficeMgmt.aspx-0013", dt.TableName);
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("SysAdmOfficeMgmt.aspx-0014", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
        }
    }
    private void ExportGridToExcel_officeReport()
    {
        try
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                // To Export all pages
                gvOffices.AllowPaging = false;
                this.loadOffices();

                gvOffices.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in gvOffices.HeaderRow.Cells)
                    cell.BackColor = gvOffices.HeaderStyle.BackColor;
                foreach (GridViewRow row in gvOffices.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                            cell.BackColor = gvOffices.AlternatingRowStyle.BackColor;
                        else
                            cell.BackColor = gvOffices.RowStyle.BackColor;
                        cell.CssClass = "textmode";
                    }
                }

                gvOffices.RenderControl(hw);
                // style to format numbers to string
                string style = "<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }


        }
        catch (Exception ex)
        {
            _common.ErrorLog("Office mgmt-M10", ex.Message.ToString());

            return;
        }

    }   // Error Code - M10
    private void InfoMsg(string msg)
    {
        string popup = _popup.modalPopupSmall("I", "Information", msg, "Close");
        Response.Write(popup);
    }
    public string ReportingOfficeUseYN(string officeid)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.SPOFFICEUSES");
            MyCommand.Parameters.AddWithValue("p_officeid", officeid);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows[0]["p_useyn"].ToString() == "Y")
                {
                    return "Y";
                }
            }
            return "N";
        }
        catch (Exception ex)
        {
            return "N";
        }
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
    protected void lbtnAddNewOffice_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        PnlRegistration.Visible = true;
        pnlOffice.Visible = true;
    }
    protected void ddlstate_SelectedIndexChanged(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        loadDistrict(ddlstate.SelectedValue, ddldistrict);
    }
    protected void ddllofficeLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        loadrptoffice(ddllofficeLevel.SelectedValue, ddlreportingofc);
    }
    protected void lbtnsaveoffice_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (validvalue() == false)
        {
            return;
        }
        Session["P_Action"] = "S";
        lblConfirmation.Text = "Do you want to Save Office Details?";
        mpConfirmation.Show();
    }
    protected void lbtnupdateoffice_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (validvalue() == false)
            return;
        Session["P_Action"] = "U";
        lblConfirmation.Text = "Do you want to Update Office Details?";
        mpConfirmation.Show();
    }
    protected void resetbutton_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        reset();
    }
    protected void lbtnCancel_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        reset();
    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (Session["P_Action"].ToString() == "S")
        {
            Session["officeId"] = null;
            savedetailsOffice();
        }
        if (Session["P_Action"].ToString() == "U")
        {
            savedetailsOffice();
        }
        if (Session["P_Action"].ToString() == "R")
        {
            savedetailsOffice();
        }
        if (Session["P_Action"].ToString() == "A")
        {
            savedetailsOffice();
        }
        if (Session["P_Action"].ToString() == "D")
        {
            savedetailsOffice();
        }
        if (Session["P_Action"].ToString() == "Unit Activate" || Session["P_Action"].ToString() == "Unit Deactivate" || Session["P_Action"].ToString() == "Unit Save" || Session["P_Action"].ToString() == "Unit Update" || Session["P_Action"].ToString() == "Unit Delete")
        {
            SaveUpdateUnitOffice();
        }
    }
    protected void grvOffices_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvOffices.PageIndex = e.NewPageIndex;
        loadOffices();
    }
    protected void gvOffices_RowCommand(object sender, GridViewCommandEventArgs e)//E1
    {
        CsrfTokenValidate();
        try
        {

            if (e.CommandName == "editoffice")
            {
                Session["P_Action"] = "U";
                lbladdnewofficeHeading.Visible = false;
                lblUpdateofficeHeading.Visible = true;
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                Session["officeId"] = gvOffices.DataKeys[row.RowIndex]["officeid"].ToString();
                tbofficename.Text = gvOffices.DataKeys[row.RowIndex]["officename"].ToString();
                tbaddress.Text = gvOffices.DataKeys[row.RowIndex]["adrs"].ToString();
                loadState(ddlstate);
                ddlstate.SelectedValue = gvOffices.DataKeys[row.RowIndex]["statecode"].ToString();
                loadDistrict(ddlstate.SelectedValue, ddldistrict);
                ddldistrict.SelectedValue = gvOffices.DataKeys[row.RowIndex]["districtcode"].ToString();
                loadofficelvl(ddllofficeLevel);
                ddllofficeLevel.SelectedValue = gvOffices.DataKeys[row.RowIndex]["oflvlid"].ToString();
                loadrptoffice(ddllofficeLevel.SelectedValue, ddlreportingofc);
                ddlreportingofc.SelectedValue = gvOffices.DataKeys[row.RowIndex]["reportingoofcid"].ToString();
                tblandline1.Text = gvOffices.DataKeys[row.RowIndex]["landl1"].ToString();
                tblandline2.Text = gvOffices.DataKeys[row.RowIndex]["landl2"].ToString();
                tbmobileno.Text = gvOffices.DataKeys[row.RowIndex]["mob"].ToString();
                tbemail.Text = gvOffices.DataKeys[row.RowIndex]["eml"].ToString();
                tbDeadKm.Text = gvOffices.DataKeys[row.RowIndex]["km"].ToString();
                PnlRegistration.Visible = true;
                lblUpdateofficeHeading.Visible = true;
                lbtnupdateoffice.Visible = true;
                lbtnsaveoffice.Visible = false;
                lbtnresetbutton.Visible = false;
                pnlUnitCreation.Visible = false;
                lbtnCancel.Visible = true;
            }
            if (e.CommandName == "deleteoffice")
            {
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                Session["P_Action"] = "R";
                Session["officeId"] = gvOffices.DataKeys[row.RowIndex]["officeid"].ToString();

                string ofcUse = gvOffices.DataKeys[row.RowIndex]["delete_yn"].ToString();
                if (ofcUse == "N")
                    Errormsg("You can not delete this Office.<br/>This Office may be<br/>1.This Office is a Reporting office ");
                else
                {
                    lblConfirmation.Text = "Do you want Delete Office Details?";
                    mpConfirmation.Show();
                }
            }
            if (e.CommandName == "activateoffice")
            {
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                Session["P_Action"] = "A";
                Session["officeId"] = gvOffices.DataKeys[row.RowIndex]["officeid"].ToString();
                lblConfirmation.Text = "Do you want Activate Office?";
                mpConfirmation.Show();
            }
            if (e.CommandName == "deactivateoffice")
            {
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                Session["P_Action"] = "D";
                Session["officeId"] = gvOffices.DataKeys[row.RowIndex]["officeid"].ToString();
                lblConfirmation.Text = "Do you want Deactivate Office?";
                mpConfirmation.Show();
            }
            if (e.CommandName == "addUnit")
            {
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                Session["officeId"] = gvOffices.DataKeys[row.RowIndex]["officeid"].ToString();
                Session["_MainOfficeId"] = gvOffices.DataKeys[row.RowIndex]["officeid"].ToString();
                Session["_MainOfficeLvlId"] = gvOffices.DataKeys[row.RowIndex]["oflvlid"].ToString();
                Session["_officeName"] = gvOffices.DataKeys[row.RowIndex]["officename"].ToString();
                lblAddUnitOffice.Text = "Add Unit Office For " + Session["_officeName"].ToString();
                loadUnitOfficeTypes(ddlUnitType);
                loadUnitOfficeTypes(ddlSUnitOficeType);
                loadUnitOffices();
                loadState(ddlUnitstate);
                loadDistrict(ddlUnitstate.SelectedValue, ddlUnitdistrict);
                pnlOfficeList.Visible = true;
                PnlRegistration.Visible = false;
                pnlUnitCreation.Visible = true;
                pnlOfficeList.Visible = false;
                pnlUnitInstruction.Visible = true;
                pnlStoreTypes.Visible = false;
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("SysAdmOfficeMgmt-E1", ex.Message.ToString());
        }
    }
    //protected void lbtnEditdOffice_Click(object sender, EventArgs e)
    //{

    //    lbtnsaveoffice.Visible = false;
    //    lbtnupdateoffice.Visible = true;
    //    lbtnresetbutton.Visible = false;
    //    lbtnCancel.Visible = true;
    //    pnlUnitCreation.Visible = false;
    //    pnlUnitList.Visible = false;
    //    PnlRegistration.Visible = true;
    //    pnlOffice.Visible = true;
    //    pnlNoRecord.Visible = false;
    //    lbladdnewofficeHeading.Visible = false;
    //    lblUpdateofficeHeading.Visible = true;

    //}  // Error Code - E1
    //protected void lbtnAddUnit_Click(object sender, EventArgs e)
    //{
    //    // Response.Redirect("AdmUnitOfficeMgmt.aspx");

    //}
    protected void lbtnHelpInfo_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Errormsg("Coming soon");
    }
    protected void lbtnExport_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        string msg = "";
        msg = msg + "Coming Soon";
        InfoMsg(msg);
    }
    private DataTable LoadPassReport1()
    {

        DataTable dt;
        NpgsqlCommand MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.sprptm3_offices");
        dt = bll.SelectAll(MyCommand);
        if (dt.TableName == "Success")
        {
            if (dt.Rows.Count > 0)
            {

            }
            else
            {
                Errormsg("No Data Found");
            }
        }
        return dt;
    }
    protected void lbtnview_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        string msg = "";
        msg = msg + "1.  Here you can add Office along with its basic details.<br/>";
        msg = msg + "2.  You can also update office details.<br/>";
        msg = msg + "3.  You Can add unit office also under the main office.<br/>";
        InfoMsg(msg);
    }
    protected void lbtnsearch_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        loadOffices();
    }
    protected void lbtnResetList_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        tbSOfcName.Text = "";
        ddlSOfficeLevel.SelectedValue = "0";
        loadOffices();
    }

    #endregion

    #region "unit office"
    private void loadstation()
    {
        try
        {
            string mainOffice = Session["_MainOfficeId"].ToString();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_bus_stand_list");

            MyCommand.Parameters.AddWithValue("p_ofcid", mainOffice);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlbusStation.DataSource = dt;
                    ddlbusStation.DataTextField = "station_name";
                    ddlbusStation.DataValueField = "station_id";
                    ddlbusStation.DataBind();
                }
                else
                {
                    ddlbusStation.Items.Insert(0, "Select Station");
                    ddlbusStation.Items[0].Value = "0";
                    ddlbusStation.SelectedIndex = 0;
                }
                ddlbusStation.Items.Insert(0, "Select Station");
                ddlbusStation.Items[0].Value = "0";
                ddlbusStation.SelectedIndex = 0;
            }
        }

        catch (Exception ex)
        {
            _common.ErrorLog("SysAdmOfficeMgmt.aspx-0015", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
        }

    }
    private void loadUnitOfficeTypes(DropDownList ddl)
    {
        try
        {
            int ofclvlid = Convert.ToInt32(Session["_MainOfficeLvlId"].ToString());
            ddl.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_officeunittypes");
            MyCommand.Parameters.AddWithValue("p_ofclvlid", ofclvlid);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddl.DataSource = dt;
                    ddl.DataTextField = "unitname";
                    ddl.DataValueField = "unitid";
                    ddl.DataBind();
                }
            }
            else
            {
                _common.ErrorLog("SysAdmOfficeMgmt.aspx-0016", dt.TableName);
            }

            ddl.Items.Insert(0, "SELECT");
            ddl.Items[0].Value = "0";
            ddl.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddl.Items.Insert(0, "SELECT");
            ddl.Items[0].Value = "0";
            ddl.SelectedIndex = 0;
            _common.ErrorLog("SysAdmOfficeMgmt.aspx-0017", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
        }
    }
    protected void loadUnitOffices()
    {
        try
        {
            pnlNoUnitOffice.Visible = true;
            gvUnitOffices.Visible = false;

            int unitType = Convert.ToInt16(ddlSUnitOficeType.SelectedValue);
            string mainOffice = Session["_MainOfficeId"].ToString();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_unitofficeslist");
            MyCommand.Parameters.AddWithValue("p_unittype", unitType);
            MyCommand.Parameters.AddWithValue("p_mainoffice", mainOffice);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    gvUnitOffices.DataSource = dt;
                    gvUnitOffices.DataBind();
                    gvUnitOffices.Visible = true;
                    pnlNoUnitOffice.Visible = false;
                }
                else
                {
                    gvUnitOffices.Visible = false;
                    pnlNoUnitOffice.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("SysAdmOfficeMgmt.aspx-0018", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
            gvUnitOffices.Visible = false;
            pnlNoUnitOffice.Visible = true;
            return;
        }
    }
    public void SaveUpdateUnitOffice()
    {
        try
        {
            string ipaddress = HttpContext.Current.Request.UserHostAddress;
            string p_action = Session["P_Action"].ToString();
            string p_officeid = "";
            string Active = "";
            if (p_action == "Unit Save")
            {
                p_action = "S";
            }
            if (p_action == "Unit Update")
            {
                p_action = "U";
                p_officeid = Session["officeId"].ToString();
            }
            if (p_action == "Unit Delete")
            {
                p_action = "R";
                p_officeid = Session["officeId"].ToString();
            }
            if (p_action == "Unit Activate")
            {
                p_action = "A";
                p_officeid = Session["officeId"].ToString();
            }
            if (p_action == "Unit Deactivate")
            {
                p_action = "D";
                p_officeid = Session["officeId"].ToString();
            }
            string storeTypesId = "";

            if (ddlUnitType.SelectedValue == "7")
            {
                for (Int16 i = 0; i <= cbl_storeTypes.Items.Count - 1; i++)
                {
                    if (cbl_storeTypes.Items[i].Selected == true)
                        storeTypesId = storeTypesId + cbl_storeTypes.Items[i].Value + ",";
                }

                if (storeTypesId.Trim().Length > 1)
                { storeTypesId = storeTypesId.Remove(storeTypesId.Length - 1, 1); }
                else
                {
                    Errormsg("Select Store Type(s)"
                     ); return;
                }
            }




            string updatedby = Session["_UserCode"].ToString();
            int mainOfcLvl = Convert.ToInt32(Session["_MainOfficeLvlId"].ToString());
            int unitType = Convert.ToInt32(ddlUnitType.SelectedValue);
            string state = ddlUnitstate.SelectedValue.ToString();
            string district = ddlUnitdistrict.SelectedValue.ToString();
            string mainOfcID = Session["_MainOfficeId"].ToString();
            int stationid = 0;
            string specialbooking = "N";
            if (ddlUnitType.SelectedValue == "9")
            {
                stationid = Convert.ToInt32(ddlbusStation.SelectedValue);
                specialbooking = ddlSpecialBooking.SelectedValue;
            }
            string logintype = ddllogintype.SelectedValue;
            
           

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_insertupdate_unitoffices");
            MyCommand.Parameters.AddWithValue("p_mainofclvl", mainOfcLvl);
            MyCommand.Parameters.AddWithValue("p_mainofc", mainOfcID);
            MyCommand.Parameters.AddWithValue("p_officeid", p_officeid);
            MyCommand.Parameters.AddWithValue("p_action", p_action);
            MyCommand.Parameters.AddWithValue("p_ofctype", unitType);
            MyCommand.Parameters.AddWithValue("p_ofcname", tbUnitofficename.Text);
            MyCommand.Parameters.AddWithValue("p_address", tbUnitaddress.Text);
            MyCommand.Parameters.AddWithValue("p_state", state);
            MyCommand.Parameters.AddWithValue("p_district", district);
            MyCommand.Parameters.AddWithValue("p_landline1", tbUnitlandline1.Text);
            MyCommand.Parameters.AddWithValue("p_landline2", tbUnitlandline2.Text);
            MyCommand.Parameters.AddWithValue("p_mobile", tbUnitmobileno.Text);
            MyCommand.Parameters.AddWithValue("p_email", tbUnitemail.Text);
            MyCommand.Parameters.AddWithValue("p_active_yn", Active);
            MyCommand.Parameters.AddWithValue("p_ipaddress", ipaddress);
            MyCommand.Parameters.AddWithValue("p_updatedby", updatedby);
            MyCommand.Parameters.AddWithValue("p_stonid", stationid);
            MyCommand.Parameters.AddWithValue("p_storetype", storeTypesId);
            
            MyCommand.Parameters.AddWithValue("p_specialbooking", specialbooking);
            MyCommand.Parameters.AddWithValue("p_logintype", logintype);

            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (p_action == "S")
                {
                    Successmsg("Record has successfully been saved");
                }
                if (p_action == "R")
                {
                    if (dt.Rows[0]["spresult"].ToString() == "NOT DELETE")
                    {
                        Errormsg("Office Not Deleted");
                    }
                    else
                    {
                        Successmsg("Unit Office successfully Deleted");
                    }
                }
                if (p_action == "U")
                {
                    Successmsg("Record has successfully updated");
                }
                if (p_action == "A")
                {
                    Successmsg("Unit Office successfully Activated");
                }
                if (p_action == "D")
                {
                    Successmsg("Unit Office successfully Deactivated");
                }
                loadUnitOffices();
                resetUnit();
            }
            else
            {
                Errormsg("Error occurred while Updation. " + dt.TableName);
                return;
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("SysAdmOfficeMgmt.aspx-0019", ex.Message.ToString());
            Errormsg(ex.Message.ToString());

            return;
        }
    }
    protected void gvUnitOffices_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        CsrfTokenValidate();
        try
        {
            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "updateOffice")
            {
                cbl_storeTypes.Items.Clear();
                cbl_storeTypes.DataBind();
                pnlStoreTypes.Visible = false;
                dvstation.Visible = false;
                dvcounterSpecialbooking.Visible = false;
                dvcounterlogintype.Visible = false;
                string bustation = gvUnitOffices.DataKeys[index].Values["station_id"].ToString();


                
                string specialbooking = gvUnitOffices.DataKeys[index].Values["specialbooking"].ToString();
                string logintype = gvUnitOffices.DataKeys[index].Values["logintype"].ToString();

                
                Session["officeId"] = gvUnitOffices.DataKeys[index].Values["office_id"].ToString();
                ddlUnitType.SelectedValue = gvUnitOffices.DataKeys[index].Values["unitid"].ToString();
                if (DBNull.Value.Equals(gvUnitOffices.DataKeys[index].Values["office_name"]) == false)
                {
                    tbUnitofficename.Text = gvUnitOffices.DataKeys[index].Values["office_name"].ToString();
                }
                else
                {
                    tbUnitofficename.Text = "";
                }
                lblAddUnitOffice.Text = "Update " + gvUnitOffices.DataKeys[index].Values["office_name"].ToString();
                if (DBNull.Value.Equals(gvUnitOffices.DataKeys[index].Values["address"]) == false)
                {
                    tbUnitaddress.Text = gvUnitOffices.DataKeys[index].Values["address"].ToString();
                }
                else
                {
                    tbUnitaddress.Text = "";
                }
                if (DBNull.Value.Equals(gvUnitOffices.DataKeys[index].Values["landline1"]) == false)
                {
                    tbUnitlandline1.Text = gvUnitOffices.DataKeys[index].Values["landline1"].ToString();
                }
                else
                {
                    tbUnitlandline1.Text = "";
                }
                if (DBNull.Value.Equals(gvUnitOffices.DataKeys[index].Values["landline2"]) == false)
                {
                    tbUnitlandline2.Text = gvUnitOffices.DataKeys[index].Values["landline2"].ToString();
                }
                else
                {
                    tbUnitlandline2.Text = "";
                }
                if (DBNull.Value.Equals(gvUnitOffices.DataKeys[index].Values["mobile"]) == false)
                {
                    tbUnitmobileno.Text = gvUnitOffices.DataKeys[index].Values["mobile"].ToString();
                }
                else
                {
                    tbUnitmobileno.Text = "";
                }
                if (DBNull.Value.Equals(gvUnitOffices.DataKeys[index].Values["email"]) == false)
                {
                    tbUnitemail.Text = gvUnitOffices.DataKeys[index].Values["email"].ToString();
                }
                else
                {
                    tbUnitemail.Text = "";
                }

                ddlUnitstate.SelectedValue = gvUnitOffices.DataKeys[index].Values["stateid"].ToString();
                this.ddlUnitstate_SelectedIndexChanged(this.ddlUnitstate, System.EventArgs.Empty);
                ddlUnitdistrict.SelectedValue = gvUnitOffices.DataKeys[index].Values["districtid"].ToString();
                if (ddlUnitType.SelectedValue == "9")
                {
                    dvstation.Visible = true;
                    dvcounterlogintype.Visible = true;
                    dvcounterSpecialbooking.Visible = true;
                    loadstation();
                    ddlbusStation.SelectedValue = bustation;
                    ddllogintype.SelectedValue = logintype;
                    ddlSpecialBooking.SelectedValue = specialbooking;
                    
                }
                if (ddlUnitType.SelectedValue == "7")
                {
                    string store_type_status = "";
                    store_type_status = gvUnitOffices.DataKeys[index].Values["store_type_status"].ToString();
                    string[] substr_status = store_type_status.Split(',');

                    string storeid = "";
                    storeid = gvUnitOffices.DataKeys[index].Values["store_type_id"].ToString();

                    string[] substr = storeid.Split(',');
                    loadstore();
                    pnlStoreTypes.Visible = true;
                    for (Int16 i = 0; i < substr.Length; i++)
                    {
                        string cb = cbl_storeTypes.Items[i].Value;
                        string sb = substr[i].TrimStart(' ');
                        if (cbl_storeTypes.Items[i].Value == substr[i].TrimStart(' ') && substr_status[i] == "Y")
                        {
                            cbl_storeTypes.Items[i].Selected = true;
                        }

                    }
                    //ddlbusStation.SelectedValue = bustation;
                }
                Session["P_Action"] = "Unit Update";
                lbtnUnitUpdateOffice.Visible = true;
                lbtnUnitSaveOffice.Visible = false;
                lbtnUnitReset.Visible = false;
            }
            if (e.CommandName == "deleteUnitOffice")
            {
                string ofcID = gvUnitOffices.DataKeys[index].Values["office_id"].ToString();
                string delete = gvUnitOffices.DataKeys[index].Values["delete_yn"].ToString();
                if (delete == "N")
                    Errormsg("You can not delete this Office.<br/>This Office may be<br/>1.Reporting office of an employee.<br/>2. Bus Stand");
                else
                {
                    Session["P_Action"] = "Unit Delete";
                    lblConfirmation.Text = "Do you want Delete Unit Office Details?";
                    mpConfirmation.Show();
                    Session["officeId"] = ofcID.Trim();
                }
            }
            if (e.CommandName == "activateUnitOffice")
            {
                string ofcID = gvUnitOffices.DataKeys[index].Values["office_id"].ToString();

                Session["P_Action"] = "Unit Activate";
                lblConfirmation.Text = "Do you want to Activate Unit Office?";
                mpConfirmation.Show();
                Session["officeId"] = ofcID.Trim();
            }
            if (e.CommandName == "deactivateUnitOffice")
            {
                string ofcID = gvUnitOffices.DataKeys[index].Values["office_id"].ToString();

                Session["P_Action"] = "Unit Deactivate";
                lblConfirmation.Text = "Do you want to Deactivate Unit Office?";
                mpConfirmation.Show();
                Session["officeId"] = ofcID.Trim();
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message);
        }
    }
    protected void gvUnitOffices_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvUnitOffices.PageIndex = e.NewPageIndex;
        loadUnitOffices();
    }
    public string officeUseYN(string officeid)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.SPOFFICEUSES");
            MyCommand.Parameters.AddWithValue("p_officeid", officeid);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows[0]["p_useyn"].ToString() == "Y")
                {
                    return "Y";
                }
            }
            return "N";
        }
        catch (Exception ex)
        {
            return "N";
        }
    }
    protected void ddlUnitstate_SelectedIndexChanged(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        loadDistrict(ddlUnitstate.SelectedValue, ddlUnitdistrict);
    }
    protected void ddlSUnitOficeType_SelectedIndexChanged(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        loadUnitOffices();
    }
    protected void lbtnUnitBack_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Response.Redirect("SysAdmOfficeMgmt.aspx");
    }
    protected void lbtnUnitSaveOffice_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (validUnitvalue() == false)
        {
            return;
        }
        Session["P_Action"] = "Unit Save";
        lblConfirmation.Text = "Do you want to Save Unit Office Details?";
        mpConfirmation.Show();
    }
    protected void lbtnUnitUpdateOffice_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (validUnitvalue() == false)
        {
            return;
        }
        Session["P_Action"] = "Unit Update";
        lblConfirmation.Text = "Do you want to Update Office Details?";
        mpConfirmation.Show();
    }
    public bool validUnitvalue()
    {
        try
        {
            string msg = "";
            int msgcont = 0;
            if (ddlUnitType.SelectedValue == "0" | ddlUnitType.SelectedValue == "")
            {
                msgcont = msgcont + 1;
                msg = msg + msgcont.ToString() + ". Select Unit Type" + "<br />";
            }
            if (_validation.IsValidString(tbUnitofficename.Text, 1, tbUnitofficename.MaxLength) == false)
            {
                msgcont = msgcont + 1;
                msg = msg + msgcont.ToString() + ". Enter Valid Office Name" + "<br />";
            }
            if (ddlUnitstate.SelectedValue == "0")
            {
                msgcont = msgcont + 1;
                msg = msg + msgcont.ToString() + ". Select Valid State" + "<br />";
            }
            if (ddlUnitdistrict.SelectedValue == "0")
            {
                msgcont = msgcont + 1;
                msg = msg + msgcont.ToString() + ". Select Valid District" + "<br />";
            }
            if (tbUnitaddress.Text.Length > 0)
            {
                if (_validation.IsValidString(tbUnitaddress.Text, 1, tbUnitaddress.MaxLength) == false)
                {
                    msgcont = msgcont + 1;
                    msg = msg + msgcont.ToString() + ". Enter Valid Office Address" + "<br />";
                }
            }
            if (ddlUnitType.SelectedValue == "9")
            {
                if (ddlbusStation.SelectedValue == "0")
                {
                    msgcont = msgcont + 1;
                    msg = msg + msgcont.ToString() + ". Select Bus Station" + "<br />";

                }
            }






            if (tblandline1.Text != "")
            {
                if (_validation.isValidLandLineNo(tbUnitlandline1.Text) == false)
                {
                    msgcont = msgcont + 1;
                    msg = msg + msgcont.ToString() + ". Enter Valid Landline Number 1 i.e. 05460-756574" + "<br />";
                }
            }
            if (tbUnitlandline2.Text != "")
            {
                if (_validation.isValidLandLineNo(tbUnitlandline2.Text) == false)
                {
                    msgcont = msgcont + 1;
                    msg = msg + msgcont.ToString() + ". Enter Valid Landline Number 2 i.e. 05460-756574" + "<br />";
                }
            }

            if (tbUnitmobileno.Text != "")
            {
                if (_validation.isValidMobileNumber(tbUnitmobileno.Text) == false)
                {
                    msgcont = msgcont + 1;
                    msg = msg + msgcont.ToString() + ". Enter Valid Mobile i.e. 9999999999" + "<br />";
                }
            }
            if (tbUnitemail.Text != "")
            {
                if (_validation.isValideMailID(tbUnitemail.Text) == false)
                {
                    msgcont = msgcont + 1;
                    msg = msg + msgcont.ToString() + ". Enter Valid Email Address" + "<br />";
                }
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
            return false;
        }
    }
    protected void lbtnUnitReset_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        resetUnit();
    }
    public void resetUnit()
    {
        tbUnitofficename.Text = "";
        tbUnitaddress.Text = "";
        tbUnitlandline1.Text = "";
        tbUnitlandline2.Text = "";
        tbUnitmobileno.Text = "";
        tbUnitemail.Text = "";
        ddlUnitstate.SelectedValue = "0";
        ddlUnitdistrict.SelectedValue = "0";
        ddlUnitType.SelectedValue = "0";
        pnlStoreTypes.Visible = false;
    }
    protected void lbtnDownloadInst_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        string msg = "";
        msg = msg + "Coming Soon";
        InfoMsg(msg);
    }
    protected void gvOffices_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        CsrfTokenValidate();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblStatus = e.Row.FindControl("lblstatus") as Label;
            LinkButton btnActive = e.Row.FindControl("ActivateOffice") as LinkButton;
            LinkButton btnDeactive = e.Row.FindControl("DeactivateOffice") as LinkButton;
            if (lblStatus.Text == "A")
            {
                btnActive.Visible = false;
                btnDeactive.Visible = false;
            }
            if (lblStatus.Text == "D")
            {
                btnActive.Visible = false;
                btnDeactive.Visible = false;
            }
        }
    }
    protected void lbtnGoback_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        PnlRegistration.Visible = true;
        pnlUnitCreation.Visible = false;
        pnlUnitInstruction.Visible = false;
        pnlOfficeList.Visible = true;
    }
    protected void gvUnitOffices_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblStatus = e.Row.FindControl("lblact") as Label;
            LinkButton btnActive = e.Row.FindControl("lbtnactivateUnitOffice") as LinkButton;
            LinkButton btnDeactive = e.Row.FindControl("lbtndeactivateUnitOffice") as LinkButton;
            if (lblStatus.Text == "A")
            {
                btnActive.Visible = false;
                btnDeactive.Visible = true;
            }
            if (lblStatus.Text == "D")
            {
                btnActive.Visible = true;
                btnDeactive.Visible = false;
            }
        }
    }
    protected void ddlUnitType_SelectedIndexChanged(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        dvstation.Visible = false;
        dvcounterlogintype.Visible = false;
        dvcounterSpecialbooking.Visible = false;
        if (ddlUnitType.SelectedValue == "9")
        {
            loadstation();
            dvstation.Visible = true;
            dvcounterlogintype.Visible = true;
            dvcounterSpecialbooking.Visible = true;
        }
        cbl_storeTypes.Items.Clear();
        cbl_storeTypes.DataBind();
        pnlStoreTypes.Visible = false;
        if (ddlUnitType.SelectedValue == "7")
        {
            loadstore();
            pnlStoreTypes.Visible = true;
        }
    }

    private void loadstore()
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_storetypes");
        dt = bll.SelectAll(MyCommand);
        if (dt.TableName == "Success")
        {
            if (dt.Rows.Count > 0)
            {
                cbl_storeTypes.DataSource = dt;
                cbl_storeTypes.DataTextField = "store_typename";
                cbl_storeTypes.DataValueField = "store_typeid";
                cbl_storeTypes.DataBind();
            }
            else
            {
                Errormsg("Something Went Wrong");
            }

        }
    }
    #endregion




}