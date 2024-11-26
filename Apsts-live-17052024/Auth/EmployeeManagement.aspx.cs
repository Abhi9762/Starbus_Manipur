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

public partial class Auth_EmployeeManagement : System.Web.UI.Page
{
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    sbValidation _validation = new sbValidation();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    protected void Page_Load(object sender, EventArgs e)
    {
        //checkForSecurity();

        Session["_moduleName"] = "Employee Management";
        if (!IsPostBack)
        {
            loadofficelvl(ddlOfficeLvl2);
            ddlOfficeLvl2.SelectedValue ="30"; //Session["uofclvlid"].ToString();
            ddlOfficeLvl2.Enabled = false;
            loadofc(ddlOfficeLvl2.SelectedValue, ddlOffice2);
            ddlOffice2.SelectedValue = Session["_LDepotCode"].ToString();
            ddlOffice2.Enabled = false;
            loadDesignaiton(ddlDepotWiseDesig);
            LoadAllEmployees();
            hideAllDiv();
            pnlView.Visible = true;

        }

        loadEmployeeCount();

    }

    #region "Method"
    private void loadEmployeeCount()//M6
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_employee_count");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    divExpiredLicense.Visible = true;
                    divToExpLicense.Visible = true;
                    lblTotalEmp.Text = dt.Rows[0]["tot_emp"].ToString();
                    lblTotalVEmp.Text = dt.Rows[0]["verify_emp"].ToString();
                    lblTotalNVEmp.Text = dt.Rows[0]["nonverify_emp"].ToString();
                    lbtnPendingDriver.Text = dt.Rows[0]["pendlicense_drv"].ToString();
                    lbtnPendingConductor.Text = dt.Rows[0]["pendlicense_cond"].ToString();
                    lbtnExpSoonDrv.Text = dt.Rows[0]["exprsoondlicense_drv"].ToString();
                    lbtnExpSoonCond.Text = dt.Rows[0]["exrdsoonlicense_cond"].ToString();
                    lbtnExpDriver.Text = dt.Rows[0]["exprdlicense_drv"].ToString();
                    lbtnExpConductor.Text = dt.Rows[0]["exrdlicense_cond"].ToString();
                }
            }
            else
            {
                _common.ErrorLog("SysadmEmployeeDetails-M6", dt.TableName);
            }

        }
        catch (Exception ex)
        {
            _common.ErrorLog("SysadmEmployeeDetails-M6", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
        }
    }
    private void loadPostingofc(string office, DropDownList ddlPostingOffice)//M28
    {
        try
        {
            ddlPostingOffice.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_emp_unitoffice");
            MyCommand.Parameters.AddWithValue("p_officeid", office);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlPostingOffice.DataSource = dt;
                    ddlPostingOffice.DataTextField = "officename";
                    ddlPostingOffice.DataValueField = "officeid";
                    ddlPostingOffice.DataBind();
                }
            }
            else
            {
                _common.ErrorLog("SysadmEmployeeDetails-M28", dt.TableName);
            }

            ddlPostingOffice.Items.Insert(0, "SELECT");
            ddlPostingOffice.Items[0].Value = "0";
            ddlPostingOffice.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlPostingOffice.Items.Insert(0, "SELECT");
            ddlPostingOffice.Items[0].Value = "0";
            ddlPostingOffice.SelectedIndex = 0;
            _common.ErrorLog("SysadmEmployeeDetails-M28", ex.Message.ToString());
        }
    }
    private void hideAllDiv()
    {
        pnlView.Visible = false;
        pnlupdateVerified.Visible = false;

        dvPersonalDetails.Visible = false;
        dvContactDetails.Visible = false;
        dvOfficeDetails.Visible = false;
        dvLicenseDetails.Visible = false;
        dvWeekrestDeails.Visible = false;
        dvDutyTypeDetails.Visible = false;
        dvServiceStatus.Visible = false;
    }
    private void loadDesignaiton(DropDownList ddlDesignation)
    {

        try
        {
            ddlDesignation.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_designation");

            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                ddlDesignation.DataSource = dt;
                ddlDesignation.DataTextField = "designationname";
                ddlDesignation.DataValueField = "designationcode";
                ddlDesignation.DataBind();
            }

            ddlDesignation.Items.Insert(0, "All");
            ddlDesignation.Items[0].Value = "0";
            ddlDesignation.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlDepotWiseDesig.Items.Insert(0, "-All-");
            ddlDepotWiseDesig.Items[0].Value = "0";
            ddlDepotWiseDesig.SelectedIndex = 0;
        }

    }
    private void loadofficelvl(DropDownList ddllofficeLevel)//M4
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
                _common.ErrorLog("SysadmEmployeeDetails-M4", dt.TableName);
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
            _common.ErrorLog("SysadmEmployeeDetails-M4", ex.Message.ToString());
        }
    }
    private void loadofc(string ofclvlid, DropDownList ddlOffice)//M5
    {
        try
        {
            ddlOffice.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_ofclvlwiseoffice");
            MyCommand.Parameters.AddWithValue("p_ofclvlid", Convert.ToInt32(ofclvlid));
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlOffice.DataSource = dt;
                    ddlOffice.DataTextField = "officename";
                    ddlOffice.DataValueField = "officeid";
                    ddlOffice.DataBind();
                }
            }
            else
            {
                _common.ErrorLog("SysadmEmployeeDetails-M5", dt.TableName);
            }

            ddlOffice.Items.Insert(0, "SELECT");
            ddlOffice.Items[0].Value = "0";
            ddlOffice.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlOffice.Items.Insert(0, "SELECT");
            ddlOffice.Items[0].Value = "0";
            ddlOffice.SelectedIndex = 0;
            _common.ErrorLog("SysadmEmployeeDetails-M5", ex.Message.ToString());
        }
    }
    private void loadEmpType(DropDownList ddlemptype)//M26
    {
        try
        {
            ddlemptype.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_emptype");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlemptype.DataSource = dt;
                    ddlemptype.DataTextField = "typename";
                    ddlemptype.DataValueField = "typeid";
                    ddlemptype.DataBind();


                    ddlEmpType2.DataSource = dt;
                    ddlEmpType2.DataTextField = "typename";
                    ddlEmpType2.DataValueField = "typeid";
                    ddlEmpType2.DataBind();

                }
            }
            else
            {
                _common.ErrorLog("SysadmEmployeeDetails-26", dt.TableName);
            }
            ddlemptype.Items.Insert(0, "SELECT");
            ddlemptype.Items[0].Value = "0";
            ddlemptype.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlemptype.Items.Insert(0, "SELECT");
            ddlemptype.Items[0].Value = "0";
            ddlemptype.SelectedIndex = 0;
            _common.ErrorLog("SysadmEmployeeDetails-26", ex.Message.ToString());
        }
    }
    private void LoadAllEmployees()//M12
    {
        try
        {

            string strVal = "";
            string empCodeOrName;
            empCodeOrName = TextBoxSearch.Text.Trim();

            if (empCodeOrName.Trim().Length > 0)
            {
                strVal = empCodeOrName;
            }

            divLabel.Visible = false;
            grvVerifiedEmployees.Visible = false;
            lblNoEmpData.Visible = true;
            lbtnEmpReports.Visible = false;



            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_emp");

            MyCommand.Parameters.AddWithValue("p_ofclvlid", Convert.ToInt32(ddlOfficeLvl2.SelectedValue.ToString()));
            MyCommand.Parameters.AddWithValue("p_ofcid", ddlOffice2.SelectedValue);
            MyCommand.Parameters.AddWithValue("p_designation", Convert.ToInt32(ddlDepotWiseDesig.SelectedValue.ToString()));

            MyCommand.Parameters.AddWithValue("p_lincensestatus", ddlLicenseStatus.SelectedValue);
            MyCommand.Parameters.AddWithValue("p_employeename", strVal);

            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    grvVerifiedEmployees.DataSource = dt;
                    grvVerifiedEmployees.DataBind();
                    lblNoEmpData.Visible = false;
                    lbtnEmpReports.Visible = true;
                    divLabel.Visible = true;
                    grvVerifiedEmployees.Visible = true;
                  
                
                }
                else
                {
                    grvVerifiedEmployees.Visible = false;
                    lblNoEmpData.Visible = true;
                }
            }
            else
            {
                grvVerifiedEmployees.Visible = false;
                lblNoEmpData.Visible = true;
                _common.ErrorLog("SysadmEmployeeDetails-M12", dt.TableName);
            }

        }
        catch (Exception ex)
        {

            divLabel.Visible = false;
            grvVerifiedEmployees.Visible = false;
            lblNoEmpData.Visible = true;
            lbtnEmpReports.Visible = false;


            //Errormsg(ex.Message.ToString());
            _common.ErrorLog("SysadmEmployeeDetails-M12", ex.Message.ToString());
        }
    }
    private void CommandAction(int index, string _action)
    {
        fillemployee(index, _action);
        closePnl.Visible = true;
    }
    private void loadEmpDutyType(DropDownList ddldutyType)//M27
    {
        try
        {
            ddldutyType.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_emp_dutytype");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddldutyType.DataSource = dt;
                    ddldutyType.DataTextField = "typename";
                    ddldutyType.DataValueField = "typeid";
                    ddldutyType.DataBind();
                }
            }
            else
            {
                _common.ErrorLog("SysadmEmployeeDetails-27", dt.TableName);
            }
            ddldutyType.Items.Insert(0, "SELECT");
            ddldutyType.Items[0].Value = "0";
            ddldutyType.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddldutyType.Items.Insert(0, "SELECT");
            ddldutyType.Items[0].Value = "0";
            ddldutyType.SelectedIndex = 0;
            _common.ErrorLog("SysadmEmployeeDetails-27", ex.Message.ToString());
        }
    }
    private void loadEmpClass(DropDownList ddlempclass)//M25
    {
        try
        {
            ddlempclass.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_empclass");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlempclass.DataSource = dt;
                    ddlempclass.DataTextField = "classname";
                    ddlempclass.DataValueField = "classid";
                    ddlempclass.DataBind();
                }
            }
            else
            {
                _common.ErrorLog("SysadmEmployeeDetails-25", dt.TableName);
            }
            ddlempclass.Items.Insert(0, "SELECT");
            ddlempclass.Items[0].Value = "0";
            ddlempclass.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlempclass.Items.Insert(0, "SELECT");
            ddlempclass.Items[0].Value = "0";
            ddlempclass.SelectedIndex = 0;
            _common.ErrorLog("SysadmEmployeeDetails-25", ex.Message.ToString());
        }
    }
    private void loadServiceStatus(DropDownList ddlstatus)//M30
    {
        try
        {
            ddlstatus.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_emp_servicestatus");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlstatus.DataSource = dt;
                    ddlstatus.DataTextField = "servicename";
                    ddlstatus.DataValueField = "serviceid";
                    ddlstatus.DataBind();
                }
            }
            else
            {
                _common.ErrorLog("SysadmEmployeeDetails-M30", dt.TableName);
            }
            ddlstatus.Items.Insert(0, "SELECT");
            ddlstatus.Items[0].Value = "0";
            ddlstatus.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlstatus.Items.Insert(0, "SELECT");
            ddlstatus.Items[0].Value = "0";
            ddlstatus.SelectedIndex = 0;
            _common.ErrorLog("SysadmEmployeeDetails-M30", ex.Message.ToString());
        }
    }
    private void ExportGridToAllotedDutyExcel()
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Charset = "";
        string FileName = "EmployeeManagement" + DateTime.Now + ".xls";
        StringWriter strwritter = new StringWriter();
        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
        grvVerifiedEmployees.AllowPaging = false;
        LoadAllEmployees();
        grvVerifiedEmployees.GridLines = GridLines.Both;
        grvVerifiedEmployees.HeaderStyle.Font.Bold = true;
        grvVerifiedEmployees.RenderControl(htmltextwrtter);
        Response.Write(strwritter.ToString());
        Response.End();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void fillemployee(int index, string action)
    {
        Session["_empCode"] = grvVerifiedEmployees.DataKeys[index].Values["e_empcode"];
        Session["_employeeName"] = grvVerifiedEmployees.DataKeys[index].Values["e_fname"].ToString() + " " + grvVerifiedEmployees.DataKeys[index].Values["e_mname"].ToString() + " " + grvVerifiedEmployees.DataKeys[index].Values["e_lname"].ToString().Trim();


        if (action == "P")
        {
            txtEmpcode1.Text = grvVerifiedEmployees.DataKeys[index].Values["e_empcode"].ToString();
            txtEmpcode1.Enabled = false;

            txtFname1.Text = grvVerifiedEmployees.DataKeys[index].Values["e_fname"].ToString();
            if (!Convert.IsDBNull(grvVerifiedEmployees.DataKeys[index].Values["e_mname"]) && grvVerifiedEmployees.DataKeys[index].Values["e_mname"].ToString() != "&nbsp;" && grvVerifiedEmployees.DataKeys[index].Values["e_mname"].ToString().Trim() != "&amp;nbsp;")
                txtMName1.Text = grvVerifiedEmployees.DataKeys[index].Values["e_mname"].ToString();
            else
                txtMName1.Text = "";

            if (!Convert.IsDBNull(grvVerifiedEmployees.DataKeys[index].Values["e_lname"]) && grvVerifiedEmployees.DataKeys[index].Values["e_lname"].ToString() != "&nbsp;" && grvVerifiedEmployees.DataKeys[index].Values["e_lname"].ToString().Trim() != "&amp;nbsp;")
                txtlastname1.Text = grvVerifiedEmployees.DataKeys[index].Values["e_lname"].ToString();
            else
                txtlastname1.Text = "";

            if (!Convert.IsDBNull(grvVerifiedEmployees.DataKeys[index].Values["e_fathername"]))
                txtFatherNAme1.Text = grvVerifiedEmployees.DataKeys[index].Values["e_fathername"].ToString();
            else
                txtFatherNAme1.Text = "";

            if (!Convert.IsDBNull(grvVerifiedEmployees.DataKeys[index].Values["e_gender"]))
                ddlVerGender.SelectedValue = grvVerifiedEmployees.DataKeys[index].Values["e_gender"].ToString();
            else
                ddlVerGender.SelectedValue = "0";

            loadBloodGrp(ddlbloodgrp2);
            if (!Convert.IsDBNull(grvVerifiedEmployees.DataKeys[index].Values["e_blood_group_code"]))
                ddlbloodgrp2.SelectedValue = grvVerifiedEmployees.DataKeys[index].Values["e_blood_group_code"].ToString();
            else
                ddlbloodgrp2.SelectedValue = "0";

            if (!Convert.IsDBNull(grvVerifiedEmployees.DataKeys[index].Values["e_dob"]))
                txtDob1.Text = grvVerifiedEmployees.DataKeys[index].Values["e_dob"].ToString();
            else
                txtDob1.Text = "";

            if (Convert.IsDBNull(grvVerifiedEmployees.DataKeys[index].Values["e_photo"]))
                imgImageUpdate.Visible = false;
            else
            {
                int imageindex = Array.IndexOf((byte[])grvVerifiedEmployees.DataKeys[index].Values["e_photo"], (byte)1);
                if (imageindex == -1)
                    imgImageUpdate.Visible = false;
                else
                {
                    imgImageUpdate.Visible = true;
                    imgImageUpdate.ImageUrl = GetImage((byte[])grvVerifiedEmployees.DataKeys[index].Values["e_photo"]);
                    Session["PhotoImgUpdate"] = grvVerifiedEmployees.DataKeys[index].Values["e_photo"];
                }
            }
        }

        if (action == "C")
        {
            if (Convert.IsDBNull(grvVerifiedEmployees.DataKeys[index].Values["e_email_id"]) || grvVerifiedEmployees.DataKeys[index].Values["e_email_id"].ToString().Trim() == "&nbsp;")
            {
                txtEmailId1.Text = "";
            }
            else
            {
                txtEmailId1.Text = grvVerifiedEmployees.DataKeys[index].Values["e_email_id"].ToString();
            }

            if (Convert.IsDBNull(grvVerifiedEmployees.DataKeys[index].Values["e_mobile_number"]))
            {
                txtMobileNo1.Text = "";
            }
            else
            {
                txtMobileNo1.Text = grvVerifiedEmployees.DataKeys[index].Values["e_mobile_number"].ToString();
            }

            if (Convert.IsDBNull(grvVerifiedEmployees.DataKeys[index].Values["e_landline"]))
            {
                txtLandlineno1.Text = "";
            }
            else
            {
                txtLandlineno1.Text = grvVerifiedEmployees.DataKeys[index].Values["e_landline"].ToString();
            }

            if (Convert.IsDBNull(grvVerifiedEmployees.DataKeys[index].Values["e_emergency_number"]))
            {
                txtEmergencyNo2.Text = "";
            }
            else
            {
                txtEmergencyNo2.Text = grvVerifiedEmployees.DataKeys[index].Values["e_emergency_number"].ToString();
            }

            if (Convert.IsDBNull(grvVerifiedEmployees.DataKeys[index].Values["e_city"]))
            {
                txtCity1.Text = "";
            }
            else
            {
                txtCity1.Text = grvVerifiedEmployees.DataKeys[index].Values["e_city"].ToString();
            }

            if (Convert.IsDBNull(grvVerifiedEmployees.DataKeys[index].Values["e_pin_code"]))
            {
                txtpin1.Text = "";
            }
            else
            {
                txtpin1.Text = grvVerifiedEmployees.DataKeys[index].Values["e_pin_code"].ToString();
            }

            loadState(ddlStates1);

            if (!Convert.IsDBNull(grvVerifiedEmployees.DataKeys[index].Values["e_state_code"]))
            {
                ddlStates1.SelectedValue = grvVerifiedEmployees.DataKeys[index].Values["e_state_code"].ToString();
            }

            if (Convert.IsDBNull(grvVerifiedEmployees.DataKeys[index].Values["e_address"]))
            {
                txtAddress1.Text = "";
            }
            else
            {
                txtAddress1.Text = grvVerifiedEmployees.DataKeys[index].Values["e_address"].ToString();
            }
        }

        if (action == "O")
        {

            loadofficelvl(ddlOfcLvl1);
            ddlOfcLvl1.Enabled = false;


            if (!Convert.IsDBNull(grvVerifiedEmployees.DataKeys[index].Values["e_ofclvl_id"]))
            {
                ddlOfcLvl1.SelectedValue = grvVerifiedEmployees.DataKeys[index].Values["e_ofclvl_id"].ToString();
            }

            loadofc(ddlOfcLvl1.SelectedValue, ddlOffice1);

            if (!Convert.IsDBNull(grvVerifiedEmployees.DataKeys[index].Values["e_officeid"]))
            {
                try
                {
                    ddlOffice1.SelectedValue = grvVerifiedEmployees.DataKeys[index].Values["e_officeid"].ToString();
                }
                catch (Exception ex)
                {
                    // Handle exception
                }
            }

            loadPostingofc(ddlOffice1.SelectedValue, ddlpostingofc1);

            if (!Convert.IsDBNull(grvVerifiedEmployees.DataKeys[index].Values["e_posting_ofc"]))
            {
                string unitofcid = grvVerifiedEmployees.DataKeys[index].Values["e_posting_ofc"].ToString();

                try
                {
                    ddlpostingofc1.SelectedValue = unitofcid;
                }
                catch (Exception ex)
                {
                    // Handle exception
                }
            }

            if (Convert.IsDBNull(grvVerifiedEmployees.DataKeys[index].Values["e_date_of_posting"]))
            {
                txtPostingDate1.Text = "";
            }
            else
            {
                txtPostingDate1.Text = grvVerifiedEmployees.DataKeys[index].Values["e_date_of_posting"].ToString();
            }

            if (Convert.IsDBNull(grvVerifiedEmployees.DataKeys[index].Values["e_date_of_joining"]))
            {
                txtDateOfJoining1.Text = "";
            }
            else
            {
                txtDateOfJoining1.Text = grvVerifiedEmployees.DataKeys[index].Values["e_date_of_joining"].ToString();
            }

            if (Convert.IsDBNull(grvVerifiedEmployees.DataKeys[index].Values["e_date_of_assigned_depot"]))
            {
                txtDepotJoiningDate1.Text = "";
            }
            else
            {
                txtDepotJoiningDate1.Text = grvVerifiedEmployees.DataKeys[index].Values["e_date_of_assigned_depot"].ToString();
            }

            loadEmpType(ddlEmpType2);

            if (Convert.IsDBNull(grvVerifiedEmployees.DataKeys[index].Values["e_emp_type"]))
            {
                ddlEmpType2.SelectedValue = "R";
            }
            else
            {
                ddlEmpType2.SelectedValue = grvVerifiedEmployees.DataKeys[index].Values["e_emp_type"].ToString();
            }
            loadDesignaiton(drpDesignation1);


            if (!Convert.IsDBNull(grvVerifiedEmployees.DataKeys[index].Values["e_designation_code"]))
            {
                drpDesignation1.SelectedValue = grvVerifiedEmployees.DataKeys[index].Values["e_designation_code"].ToString();
            }
        }

        if (action == "L")
        {
            if (Convert.IsDBNull(grvVerifiedEmployees.DataKeys[index].Values["e_licenseno"]))
            {
                txtLicenseNo2.Text = "";
            }
            else
            {
                txtLicenseNo2.Text = grvVerifiedEmployees.DataKeys[index].Values["e_licenseno"].ToString();
            }

            if (Convert.IsDBNull(grvVerifiedEmployees.DataKeys[index].Values["e_licensedate"]))
            {
                txtLicenceDt2.Text = "";
            }
            else
            {
                txtLicenceDt2.Text = grvVerifiedEmployees.DataKeys[index].Values["e_licensedate"].ToString();
            }
        }

        if (action == "R")
        {
            ddlWeekDays.SelectedValue = "0";

            if (!Convert.IsDBNull(grvVerifiedEmployees.DataKeys[index].Values["e_weekrestday"]))
            {
                string weeklyrestday = grvVerifiedEmployees.DataKeys[index].Values["e_weekrestday"].ToString();

                try
                {
                    ddlWeekDays.SelectedValue = weeklyrestday;
                }
                catch (Exception ex)
                {
                    // Handle exception
                }
            }
        }

        if (action == "D")
        {
            loadEmpClass(ddlempclass1);


            if (!Convert.IsDBNull(grvVerifiedEmployees.DataKeys[index].Values["e_empclass"]))
            {
                string empclass = grvVerifiedEmployees.DataKeys[index].Values["e_empclass"].ToString();

                try
                {
                    ddlempclass1.SelectedValue = empclass;
                }
                catch (Exception ex)
                {
                    // Handle exception
                }
            }
            loadEmpDutyType(ddldutytype1);


            if (!Convert.IsDBNull(grvVerifiedEmployees.DataKeys[index].Values["e_dutytype"]))
            {
                string dutytype = grvVerifiedEmployees.DataKeys[index].Values["e_dutytype"].ToString();

                try
                {
                    ddldutytype1.SelectedValue = dutytype;
                }
                catch (Exception ex)
                {
                    // Handle exception
                }
            }
        }

        if (action == "S")
        {
            loadServiceStatus(ddlservicestatus);

            if (grvVerifiedEmployees.DataKeys[index].Values["e_service_status"].ToString() == "2" || grvVerifiedEmployees.DataKeys[index].Values["e_service_status"].ToString() == "3")
            {
                ddlservicestatus.Enabled = false;
                txtofcorderno.Enabled = false;
                txtorderdate.Enabled = false;
                txtremark.Enabled = false;
                lbtnUpdatedServieStatus.Enabled = false;
            }
            else
            {
                ddlservicestatus.Enabled = true;
                txtofcorderno.Enabled = true;
                txtorderdate.Enabled = true;
                txtremark.Enabled = true;
                lbtnUpdatedServieStatus.Enabled = true;
            }

            ddlservicestatus.SelectedValue = grvVerifiedEmployees.DataKeys[index].Values["e_service_status"].ToString();

            if (!Convert.IsDBNull(grvVerifiedEmployees.DataKeys[index].Values["e_orderno"]))
            {
                txtofcorderno.Text = grvVerifiedEmployees.DataKeys[index].Values["e_orderno"].ToString();
            }
            else
            {
                txtofcorderno.Text = "";
            }

            if (!Convert.IsDBNull(grvVerifiedEmployees.DataKeys[index].Values["e_orderdate"]))
            {
                txtorderdate.Text = grvVerifiedEmployees.DataKeys[index].Values["e_orderdate"].ToString();
            }
            else
            {
                txtorderdate.Text = "";
            }

            if (!Convert.IsDBNull(grvVerifiedEmployees.DataKeys[index].Values["e_orderremark"]))
            {
                txtremark.Text = grvVerifiedEmployees.DataKeys[index].Values["e_orderremark"].ToString();
            }
            else
            {
                txtremark.Text = "";
            }
        }

        //if (!Convert.IsDBNull(grvVerifiedEmployees.DataKeys[index].Values["STATUS"]))
        //{
        //     drpStatus.SelectedValue = grvVerifiedEmployees.DataKeys[index].Values["STATUS"];
        //}


    }
    private string GetImage(byte[] img)
    {
        try
        {
            return "data:image/jpg;base64," + Convert.ToBase64String((byte[])img);
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    private void loadState(DropDownList ddlstate)//M3
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
                _common.ErrorLog("SysadmEmployeeDetails-M3", dt.TableName);
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
            _common.ErrorLog("SysadmEmployeeDetails-M3", ex.Message.ToString());
        }
    }
    private void loadBloodGrp(DropDownList ddlbloodgrp)
    {
        try
        {
            ddlbloodgrp.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_bloodgrp");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlbloodgrp.DataSource = dt;
                    ddlbloodgrp.DataTextField = "groupname";
                    ddlbloodgrp.DataValueField = "groupid";
                    ddlbloodgrp.DataBind();
                }
            }
            else
            {
                //_common.ErrorLog("SysadmEmployeeDetails-M1", dt.TableName);
            }
            ddlbloodgrp.Items.Insert(0, "SELECT");
            ddlbloodgrp.Items[0].Value = "0";
            ddlbloodgrp.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlbloodgrp.Items.Insert(0, "SELECT");
            ddlbloodgrp.Items[0].Value = "0";
            ddlbloodgrp.SelectedIndex = 0;
            _common.ErrorLog("SysadmEmployeeDetails-M1", ex.Message.ToString());
        }
    }
    private void errormsg(string errormsg)
    {
        lblerrmsg.Text = errormsg;
        mpError.Show();
    }
    private void InsertOfficeDetails()//M18
    {
        try
        {
            string ofcid, dobjoin, dobjoinofc, emptype, postingofc, dateofposting;
            int ofclvl, designation;
            ofclvl = Convert.ToInt16(ddlOfficeLvl2.SelectedValue.ToString());
            ofcid = ddlOffice1.SelectedValue.ToString();
            dobjoin = txtDateOfJoining1.Text;
            dobjoinofc = txtDepotJoiningDate1.Text;
            emptype = ddlEmpType2.SelectedValue.ToString();
            designation = Convert.ToInt16(drpDesignation1.SelectedValue.ToString());
            postingofc = ddlpostingofc1.SelectedValue.ToString();
            dateofposting = txtPostingDate1.Text;
            if (dateofposting == "")
            {
                dateofposting = "";
            }
            string ipaddress = HttpContext.Current.Request.UserHostAddress;
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            string Mresult = "";
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_employee_officedetails");
            MyCommand.Parameters.AddWithValue("p_action", Session["Action"].ToString());
            MyCommand.Parameters.AddWithValue("p_savetype", Session["SaveType"].ToString());
            MyCommand.Parameters.AddWithValue("p_empcode", Session["_empCode"]);
            MyCommand.Parameters.AddWithValue("p_ofclvl", ofclvl);
            MyCommand.Parameters.AddWithValue("p_ofcid", ofcid);
            MyCommand.Parameters.AddWithValue("p_dobjoin", dobjoin);
            MyCommand.Parameters.AddWithValue("p_dobjoinofc", dobjoinofc);
            MyCommand.Parameters.AddWithValue("p_emptype", emptype);
            MyCommand.Parameters.AddWithValue("p_designation", designation);
            MyCommand.Parameters.AddWithValue("p_posting_ofc", postingofc);
            MyCommand.Parameters.AddWithValue("p_posting_date", dateofposting);
            MyCommand.Parameters.AddWithValue("p_updateby", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("p_ipaddress", ipaddress);
            Mresult = bll.UpdateAll(MyCommand);
            if (Mresult == "Success")
            {
                initemppnl();
                Successmsg("Employee Office details successfully saved");
            }
            else
            {
                Errormsg("Unable to Update Employee Office details");
                _common.ErrorLog("SysadmEmployeeDetails-M18", Mresult);
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message.ToString());
            _common.ErrorLog("SysadmEmployeeDetails-M18", ex.Message.ToString());
        }
    }
    public bool IsValidPersonalValues()
    {
        try
        {
            if (txtFname1.Text.Trim().Length <= 0)
            {
                errormsg("Please Enter First Name");
                return false;
            }
            if (!_validation.IsValidString(txtMName1.Text.Trim(), 0, txtMName1.MaxLength))
            {
                errormsg("Please Enter Middle Name or leave it blank");
                return false;
            }
            if (!_validation.IsValidString(txtlastname1.Text.Trim(), 0, txtlastname1.MaxLength))
            {
                errormsg("Please Enter Last Name or leave it blank");
                return false;
            }
            if (!_validation.IsValidString(txtFatherNAme1.Text.Trim(), 0, txtFatherNAme1.MaxLength))
            {
                errormsg("Please Enter Father Name or leave it blank");
                return false;
            }
            if (ddlVerGender.SelectedValue == "0")
            {
                errormsg("Select Gender");
                return false;
            }
            if (ddlbloodgrp2.SelectedValue == "0")
            {
                errormsg("Select Blood Group");
                return false;
            }
            if (txtDob1.Text.Trim().Length > 0)
            {
                 DateTime DOB = DateTime.ParseExact(txtDob1.Text, "dd/MM/yyyy", null);
                if (DOB > DateTime.Now.Date)
                {
                    errormsg("Invalid Date Of Birth;");
                    return false;
                }
            }
            if (Session["PhotoImgUpdate"] == null)
            {
                errormsg("Please Upload Photo");
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            errormsg("Error, Update Personal Details" + ex.Message);
            return false;
        }
    }
    private void InsertLicenseDetails()//M22
    {
        try
        {
            string licenseno, licensedate;
            licenseno = txtLicenseNo2.Text;
            licensedate = txtLicenceDt2.Text;
            string ipaddress = HttpContext.Current.Request.UserHostAddress;
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            string Mresult = "";
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_employee_licensedetails");
            MyCommand.Parameters.AddWithValue("p_action", Session["Action"].ToString());
            MyCommand.Parameters.AddWithValue("p_savetype", Session["SaveType"].ToString());
            MyCommand.Parameters.AddWithValue("p_empcode", Session["_empCode"]);
            MyCommand.Parameters.AddWithValue("p_licenseno", licenseno);
            MyCommand.Parameters.AddWithValue("p_licensedate", licensedate);
            MyCommand.Parameters.AddWithValue("p_updateby", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("p_ipaddress", ipaddress);
            Mresult = bll.UpdateAll(MyCommand);
            if (Mresult == "Success")
            {
                initemppnl();
                Successmsg("Employee License Details successfully saved");
            }
            else
            {
                _common.ErrorLog("SysadmEmployeeDetails-M22", Mresult);
                Errormsg("Unable to Update Employee License Details");
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message.ToString());
            _common.ErrorLog("SysadmEmployeeDetails-M22", ex.Message.ToString());
        }
    }
    private void InsertPersonalDetails()//M14
    {
        try
        {
            string fname, mname, lname, dob, fthrname, gendr;
            int bldgrp;
            byte[] photo = null;
            photo = Session["PhotoImgUpdate"] as byte[];
            fname = txtFname1.Text;
            mname = txtMName1.Text;
            lname = txtlastname1.Text;
            fthrname = txtFatherNAme1.Text;
            gendr = ddlVerGender.SelectedValue;
            bldgrp = Convert.ToInt16(ddlbloodgrp2.SelectedValue.ToString());
            dob = txtDob1.Text;
            string ipaddress = HttpContext.Current.Request.UserHostAddress;
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            string Mresult = "";
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_employee_personal_details_update");
            MyCommand.Parameters.AddWithValue("p_action", Session["Action"].ToString());
            MyCommand.Parameters.AddWithValue("p_savetype", Session["SaveType"].ToString());
            MyCommand.Parameters.AddWithValue("p_empcode", Session["_empCode"]);
            MyCommand.Parameters.AddWithValue("p_fname", fname);
            MyCommand.Parameters.AddWithValue("p_mname", mname);
            MyCommand.Parameters.AddWithValue("p_lname", lname);
            MyCommand.Parameters.AddWithValue("p_fthrname", fthrname);
            MyCommand.Parameters.AddWithValue("p_gendr", gendr);
            MyCommand.Parameters.AddWithValue("p_bldgrp", bldgrp);
            MyCommand.Parameters.AddWithValue("p_dob", dob);
            MyCommand.Parameters.AddWithValue("p_photo", photo);
            MyCommand.Parameters.AddWithValue("p_updateby", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("p_ipaddress", ipaddress);
            Mresult = bll.UpdateAll(MyCommand);
            if (Mresult == "Success")
            {
                initemppnl();
                Successmsg("Employee Personal details successfully saved");

            }
            else
            {
                Errormsg("Unable to Update Employee Personal Details");
                _common.ErrorLog("SysadmEmployeeDetails-M14", Mresult);
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message.ToString());
            _common.ErrorLog("SysadmEmployeeDetails-M14", ex.Message.ToString());
        }
    }
    private void initemppnl()
    {

        LoadAllEmployees();
        Session["PhotoImgUpdate"] = null;
    }
    private void InsertContactDetails()//M16
    {
        try
        {
            string emilid, mobno, lndline, emgncyno, addrs, pin, city, stcode;
            emilid = txtEmailId1.Text;
            mobno = txtMobileNo1.Text;
            lndline = txtLandlineno1.Text;
            emgncyno = txtEmergencyNo2.Text;
            addrs = txtAddress1.Text;
            pin = txtpin1.Text;
            city = txtCity1.Text;
            stcode = ddlStates1.SelectedValue.ToString();
            string ipaddress = HttpContext.Current.Request.UserHostAddress;
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            string Mresult = "";
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_employee_contactdetails");
            MyCommand.Parameters.AddWithValue("p_action", Session["Action"].ToString());
            MyCommand.Parameters.AddWithValue("p_savetype", Session["SaveType"].ToString());
            MyCommand.Parameters.AddWithValue("p_empcode", Session["_empCode"]);
            MyCommand.Parameters.AddWithValue("p_emilid", emilid);
            MyCommand.Parameters.AddWithValue("p_mobno", mobno);
            MyCommand.Parameters.AddWithValue("p_lndline", lndline);
            MyCommand.Parameters.AddWithValue("p_emgncyno", emgncyno);
            MyCommand.Parameters.AddWithValue("p_addrs", addrs);
            MyCommand.Parameters.AddWithValue("p_pin", pin);
            MyCommand.Parameters.AddWithValue("p_city", city);
            MyCommand.Parameters.AddWithValue("p_stcode", stcode);
            MyCommand.Parameters.AddWithValue("p_updateby", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("p_ipaddress", ipaddress);
            Mresult = bll.UpdateAll(MyCommand);
            if (Mresult == "Success")
            {
                initemppnl();
                Successmsg("Employee Contact details successfully saved");
            }
            else
            {
                Errormsg("Unable to Update Employee Contact details");
                _common.ErrorLog("SysadmEmployeeDetails-M16", Mresult);
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message.ToString());
            _common.ErrorLog("SysadmEmployeeDetails-M16", ex.Message.ToString());
        }
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
    private bool IsValidContactValues()
    {
        try
        {
            if (txtEmailId1.Text.Length > 0)
            {
                if (!_validation.isValideMailID(txtEmailId1.Text.Trim()))
                {
                    errormsg("Please enter valid email id or leave it blank.");
                    return false;
                }
            }
            if (!_validation.IsValidInteger(txtMobileNo1.Text, 10, 10))
            {
                errormsg("Please enter a valid 10 digit Mobile Number");
                return false;
            }
            if (txtLandlineno1.Text.Trim().Length > 0)
            {
                if (!_validation.IsValidInteger(txtLandlineno1.Text.Trim(), 10, 12))
                {
                    errormsg("Please enter a valid Landline Number in the format STDCODELANDLINE NO e.g. 01352713739 or leave it blank");
                    return false;
                }
            }
            if (!_validation.IsValidInteger(txtEmergencyNo2.Text, 10, 10))
            {
                errormsg("Please enter a valid 10 digit Emergency Number");
                return false;
            }
            if (!_validation.IsValidString(txtCity1.Text.Trim(), 0, txtCity1.MaxLength))
            {
                errormsg("Please enter valid value in the field City or leave it blank");
                return false;
            }
            if (txtpin1.Text.Trim().Length > 0)
            {
                if (!_validation.IsValidInteger(txtpin1.Text, 6, 6))
                {
                    errormsg("Please enter valid Pincode or leave it blank");
                    return false;
                }
            }
            if (!_validation.IsValidInteger(ddlStates1.SelectedValue, 1, 2))
            {
                errormsg("Please corerct the invalid State Selection");
                return false;
            }
            if (!_validation.IsValidAddress(txtAddress1.Text.Trim(), 0, txtAddress1.MaxLength))
            {
                errormsg("Please enter a valid Address  or leave it blank");
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            errormsg("Error, Update Contact Details" + ex.Message);
            return false;
        }

    }
    private bool IsValidOfficial()
    {
        try
        {
            if (ddlOfcLvl1.SelectedValue != "0")
            {
                if (!_validation.IsValidInteger(ddlOfcLvl1.SelectedValue, 1, 2))
                {
                    errormsg("Please Select Office Level");
                    return false;
                }
            }
            else
            {
                errormsg("Please Select Office Level");
                return false;
            }

            if (ddlOffice1.SelectedValue != "0")
            {
                if (!_validation.IsValidInteger(ddlOffice1.SelectedValue, 1, 20))
                {
                    errormsg("Please Select Office");
                    return false;
                }
            }
            else
            {
                errormsg("Please Select Office");
                return false;
            }

            if (ddlpostingofc1.SelectedValue != "0")
            {
                if (!_validation.IsValidInteger(ddlpostingofc1.SelectedValue, 1, 20))
                {
                    errormsg("Please Select Posting Office");
                    return false;
                }
            }
            else
            {
                errormsg("Please Select Posting Office");
                return false;
            }



            if (!_validation.IsValidInteger(drpDesignation1.SelectedValue, 1, 5))
            {
                errormsg("Please select Designation");
                return false;
            }





            return true;
        }
        catch (Exception ex)
        {
            errormsg("Error, Update Office Details" + ex.Message);
            return false;
        }

    }
    private bool IsValidLicense()
    {
        try
        {
            if (!_validation.IsValidString(txtLicenseNo2.Text.Trim(), 1, txtLicenseNo2.MaxLength))
            {
                lblerrmsg.Text = "Please enter valid License No";
                mpError.Show();
                return false;
            }

            //if (!IsDate(txtLicenceDt2.Text))
            //{
            //    lblerrmsg.Text = "Please Enter correct License Valid Upto Date";
            //    mpError.Show();
            //    return false;
            //}
            //else
            //{
            //    if (Convert.ToDateTime(txtLicenceDt2.Text.Trim()) < DateTime.Now.Date)
            //    {
            //        lblerrmsg.Text = "Please enter License Valid Upto Date. Date should be greater than today";
            //        mpError.Show();
            //        return false;
            //    }
            //}

            return true;
        }
        catch (Exception ex)
        {
            errormsg("Error, Update License Details" + ex.Message);
            return false;
        }

    }
    private void InsertWeekrestdayDetails()//M24
    {
        try
        {
            string wkrstday;
            wkrstday = ddlWeekDays.SelectedValue;
            string ipaddress = HttpContext.Current.Request.UserHostAddress;
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            string Mresult = "";
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_employee_weekrestdetails");
            MyCommand.Parameters.AddWithValue("p_action", Session["Action"].ToString());
            MyCommand.Parameters.AddWithValue("p_savetype", Session["SaveType"].ToString());
            MyCommand.Parameters.AddWithValue("p_empcode", Session["_empCode"].ToString());
            MyCommand.Parameters.AddWithValue("p_wkrstday", wkrstday);
            MyCommand.Parameters.AddWithValue("p_updateby", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("p_ipaddress", ipaddress);
            Mresult = bll.UpdateAll(MyCommand);
            if (Mresult == "Success")
            {
                initemppnl();
                Successmsg("Employee Weekrest day successfully saved");
            }
            else
            {
                _common.ErrorLog("SysadmEmployeeDetails-M24", Mresult);
                Errormsg("Unable to Update Employee Weekrest day");
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message.ToString());
            _common.ErrorLog("SysadmEmployeeDetails-M24", ex.Message.ToString());
        }
    }
    private void InsertDutyTypeDetails()//29
    {
        try
        {
            string dutytype;
            int empclass;
            dutytype = ddldutytype1.SelectedValue;
            empclass = Convert.ToInt32(ddlempclass1.SelectedValue);
            string ipaddress = HttpContext.Current.Request.UserHostAddress;
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            string Mresult = "";
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_employee_dutytype");
            MyCommand.Parameters.AddWithValue("p_action", Session["Action"].ToString());
            MyCommand.Parameters.AddWithValue("p_savetype", Session["SaveType"].ToString());
            MyCommand.Parameters.AddWithValue("p_empcode", Session["_empCode"]);
            MyCommand.Parameters.AddWithValue("p_dutytype", dutytype);
            MyCommand.Parameters.AddWithValue("p_empclass", empclass);
            MyCommand.Parameters.AddWithValue("p_updateby", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("p_ipaddress", ipaddress);
            Mresult = bll.UpdateAll(MyCommand);
            if (Mresult == "Success")
            {
                initemppnl();
                Successmsg("Employee DutyType Details successfully saved");
            }
            else
            {
                _common.ErrorLog("SysadmEmployeeDetails-M29", Mresult);
                Errormsg("Unable to Update Employee DutyType Details");
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message.ToString());
            _common.ErrorLog("SysadmEmployeeDetails-M29", ex.Message.ToString());
        }
    }
    private bool IsValidDutytype()
    {
        try
        {
            if (ddlempclass1.SelectedValue == "0")
            {
                errormsg("Please Select valid Employee Class");
                return false;
            }

            if (ddldutytype1.SelectedValue == "0")
            {
                errormsg("Please Select valid Duty Type Class");
                return false;
            }

            return true;
        }
        catch (Exception ex)
        {
            errormsg("Error, Update Duty Type Details" + ex.Message);
            return false;
        }

    }
    private void InsertServiceStatusDetails()//31
    {
        try
        {

            string ipaddress = HttpContext.Current.Request.UserHostAddress;
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            string Mresult = "";
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_employee_servicestatusdetails");
            MyCommand.Parameters.AddWithValue("p_action", Session["Action"].ToString());
            MyCommand.Parameters.AddWithValue("p_savetype", Session["SaveType"].ToString());
            MyCommand.Parameters.AddWithValue("p_empcode", Session["_empCode"]);
            MyCommand.Parameters.AddWithValue("p_servicestatus", Convert.ToInt32(ddlservicestatus.SelectedValue));
            MyCommand.Parameters.AddWithValue("p_orderno", txtofcorderno.Text);
            MyCommand.Parameters.AddWithValue("p_orderdate", txtorderdate.Text);
            MyCommand.Parameters.AddWithValue("p_remark", txtremark.Text);
            MyCommand.Parameters.AddWithValue("p_updateby", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("p_ipaddress", ipaddress);
            Mresult = bll.UpdateAll(MyCommand);
            if (Mresult == "Success")
            {
                initemppnl();
                Successmsg("Service Status Details successfully saved");
            }
            else
            {
                _common.ErrorLog("SysadmEmployeeDetails-M29", Mresult);
                Errormsg("Unable to Update Service Status Details");
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message.ToString());
            _common.ErrorLog("SysadmEmployeeDetails-M29", ex.Message.ToString());
        }
    }
    private bool IsValidServiceStatus()
    {
        try
        {
            if (ddlservicestatus.SelectedValue == "0")
            {
                errormsg("Please Select valid Service Status");
                return false;
            }

            if (string.IsNullOrEmpty(txtofcorderno.Text))
            {
                errormsg("Please Enter Office Order Number");
                return false;
            }


            return true;
        }
        catch (Exception ex)
        {
            errormsg("Error, Update Service Status Details" + ex.Message);
            return false;
        }

    }
    public bool checkFileExtention(System.Web.UI.WebControls.FileUpload fuFileUpload)//M8
    {
        try
        {
            bool fileExtensionOK = false;
            string fileExtension;
            fileExtension = System.IO.Path.GetExtension(fuFileUpload.FileName).ToLower();
            string[] allowedExtensions = new[] { ".jpg", ".png", ".jpeg" };
            for (int i = 0; i <= allowedExtensions.Length - 1; i++)
            {
                if (fileExtension == allowedExtensions[i])
                {
                    fileExtensionOK = true;
                }
            }
            return fileExtensionOK;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("SysadmEmployeeDetails-M8", ex.Message.ToString());
            return default(Boolean);
        }
    }
    public static string GetMimeDataOfFile(HttpPostedFile file)
    {
        IntPtr mimeout = default(IntPtr);
        int MaxContent = Convert.ToInt32(file.ContentLength);
        if (MaxContent > 4096)
        {
            MaxContent = 4096;
        }

        byte[] buf = new byte[MaxContent - 1 + 1];
        file.InputStream.Read(buf, 0, MaxContent);
        int MimeSampleSize = 256;


        string mimeType = System.Web.MimeMapping.GetMimeMapping(file.FileName);
        return mimeType;
    }
    public byte[] convertByteFile(System.Web.UI.WebControls.FileUpload fuFileUpload)
    {
        int intFileLength;
        byte[] byteData = null;
        if (fuFileUpload.HasFile)
        {

            // Check File Extention
            if (checkFileExtention(fuFileUpload) == true)
            {
                intFileLength = fuFileUpload.PostedFile.ContentLength;
                byteData = new byte[intFileLength + 1];
                byteData = fuFileUpload.FileBytes;
            }
        }
        else
        {
            intFileLength = fuFileUpload.PostedFile.ContentLength;
            byteData = new byte[intFileLength + 1];
            byteData = fuFileUpload.FileBytes;
        }
        return byteData;
    }
    #endregion

    #region "Events"
    protected void lbtnserch_Click(object sender, EventArgs e)
    {
        LoadAllEmployees();
    }
    protected void grvVerifiedEmployees_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "ViewProfile")
        {
            Session["_empCode"] = grvVerifiedEmployees.DataKeys[index].Values["e_empcode"];
            mpEmpProfile.Show();
            return;
        }
        hideAllDiv();
        pnlupdateVerified.Visible = true;
        if (e.CommandName == "UpdatePersonal")
        {
            CommandAction(index, "P");
            dvPersonalDetails.Visible = true;
        }
        if (e.CommandName == "UpdateContact")
        {
            CommandAction(index, "C");
            dvContactDetails.Visible = true;
        }
        if (e.CommandName == "UpdateOfficial")
        {
            CommandAction(index, "O");
            dvOfficeDetails.Visible = true;
        }
        if (e.CommandName == "UpdateLicense")
        {
            CommandAction(index, "L");
            dvLicenseDetails.Visible = true;
        }
        if (e.CommandName == "UpdateWeeklyRest")
        {
            CommandAction(index, "R");
            dvWeekrestDeails.Visible = true;
        }
        if (e.CommandName == "updateDutyType")
        {
            CommandAction(index, "D");
            dvDutyTypeDetails.Visible = true;
        }
        if (e.CommandName == "updateServiceStatus")
        {
            loadServiceStatus(ddlservicestatus);
            CommandAction(index, "S");
            dvServiceStatus.Visible = true;
        }

    } 
    protected void lbtnerrorclose1_Click(object sender, EventArgs e)
    {
        mpError.Hide();
    }
    protected void lbtnVerifyPersonal_Click(object sender, EventArgs e)
    {
        if (!_validation.IsValidString(txtEmpcode1.Text, 1, 20))
        {
            errormsg("Please Select Employee Code");
            return;
        }

        if (!IsValidPersonalValues())
        {
            return;
        }

        lblVMessage.Text = "Are you sure you want to Update & Verify Personal Details of" + "\n Mr/Ms " + Session["_employeeName"] + "(Emp Code " + Session["_empCode"] + ")";
        mpVConfirm.Show();
        Session["actionType"] = "V";
        Session["action"] = "Personal";
        Session["SaveType"] = "L";

    } 
    protected void lbtnOkVerification_Click(object sender, EventArgs e)
    {
        if (Session["Action"].ToString() == "Personal")
        {
            InsertPersonalDetails();
        }
        else if (Session["action"].ToString() == "Contact")
        {
            InsertContactDetails();
        }
        else if (Session["action"].ToString() == "Office")
        {
            InsertOfficeDetails();
        }
        else if (Session["action"].ToString() == "License")
        {
            InsertLicenseDetails();
        }
        else if (Session["action"].ToString() == "weekDay")
        {
            InsertWeekrestdayDetails();
        }
        else if (Session["action"].ToString() == "Dutytype")
        {
            InsertDutyTypeDetails();
        }
        else if (Session["action"].ToString() == "ServiceStatus")
        {
            InsertServiceStatusDetails();
        }
        //string IPAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        //if (string.IsNullOrEmpty(IPAddress))
        //{
        //    IPAddress = Request.ServerVariables["REMOTE_ADDR"];
        //}

        //if (Session["action"].ToString() == "Personal")
        //{
        //    updatePersonal(Session["actionType"].ToString(), IPAddress);
        //}
        //
        //
        //




        hideAllDiv();
        pnlView.Visible = true;
        LoadAllEmployees();

    }
    protected void lbtnVerifyContact_Click(object sender, EventArgs e)
    {
        if (!IsValidContactValues())
        {
            return;
        }

        lblVMessage.Text = "Are you sure you want to Update & Verify Contact Details of" + "\r\n" + " Mr/Ms " + Session["_employeeName"] + "(Emp Code " + Session["_empCode"] + ")";
        mpVConfirm.Show();
        Session["actionType"] = "V";
        Session["action"] = "Contact";
        Session["SaveType"] = "L";





    }
    protected void lbtnVerifyOfficial_Click(object sender, EventArgs e)
    {
        if (!IsValidOfficial())
        {
            return;
        }

        lblVMessage.Text = "Are you sure you want to Update & Verify Office Details of" + "\r\n" + " Mr/Ms " + Session["_employeeName"] + "(Emp Code " + Session["_empCode"] + ")";
        mpVConfirm.Show();
        Session["actionType"] = "V";
        Session["SaveType"] = "L";
        Session["action"] = "Office";
    }
    protected void ddlOffice1_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadPostingofc(ddlOffice1.SelectedValue, ddlpostingofc1);
    }
    protected void lbtnSaveLicenseDetails_Click(object sender, EventArgs e)
    {
        if (IsValidLicense() == false)
        {
            return;
        }

        lblVMessage.Text = "Are you sure you want to Update & Verify License Details of" + "\n" + " Mr/Ms " + Session["_employeeName"] + "(Emp Code " + Session["_empCode"] + ")";
        mpVConfirm.Show();
        Session["actionType"] = "V";
        Session["SaveType"] = "L";
        Session["action"] = "License";

    }  
    protected void lbtnSaveWeeklyRest_Click(object sender, EventArgs e)
    {
        if (ddlWeekDays.SelectedValue == "0")
        {
            errormsg("Select Weekly Rest Day");
            return;
        }

        lblVMessage.Text = "Are you sure you want to Update & Verify Weekly Rest Detail Of" + "\n" + " Mr/Ms " + Session["_employeeName"] + "(Emp Code " + Session["_empCode"] + ")";
        mpVConfirm.Show();
        Session["actionType"] = "V";
        Session["action"] = "weekDay";
        Session["SaveType"] = "L";

    }
    protected void lbtnUpdatedDutyType_Click(object sender, EventArgs e)
    {
        if (IsValidDutytype() == false)
        {
            return;
        }

        lblVMessage.Text = "Are you sure you want to Update & Verify Duty Type Details of" + "\n" + " Mr/Ms " + Session["_employeeName"] + "(Emp Code " + Session["_empCode"] + ")";
        mpVConfirm.Show();
        Session["actionType"] = "V";
        Session["action"] = "Dutytype";
        Session["SaveType"] = "L";

    }
    protected void lbtnUpdatedServieStatus_Click(object sender, EventArgs e)
    {
        if (IsValidServiceStatus() == false)
        {
            return;
        }

        lblVMessage.Text = "Are you sure you want to Update & Verify Service Status Details of" + "\n" + " Mr/Ms " + Session["_employeeName"] + "(Emp Code " + Session["_empCode"] + ")";
        mpVConfirm.Show();
        Session["actionType"] = "S";
        Session["action"] = "ServiceStatus";
        Session["SaveType"] = "L";

    }
    protected void ddlOfcLvl1_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadofc(ddlOfcLvl1.SelectedValue, ddlOffice1);
    }
    protected void lbtnEmpReports_Click(object sender, EventArgs e)
    {
        ExportGridToAllotedDutyExcel();
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {

        loadofficelvl(ddlOfficeLvl2);
        ddlOfficeLvl2.SelectedValue ="30";// Session["uofclvlid"].ToString();
        ddlOfficeLvl2.Enabled = false;

        loadofc(ddlOfficeLvl2.SelectedValue, ddlOffice2);
        ddlOffice2.SelectedValue = Session["_LDepotCode"].ToString();
        ddlOffice2.Enabled = false;
        
        pnlView.Visible = true;
        TextBoxSearch.Text = "";
        ddlLicenseStatus.SelectedValue = "0";
        initemppnl();


    }
    protected void closePnl_Click(object sender, EventArgs e)
    {
        pnlupdateVerified.Visible = false;
        pnlView.Visible = true;
    }
    protected void btnUploadImage1_Click(object sender, EventArgs e)
    {
        try
        {
            if (!ImageUpdate.HasFile)
            {
                Errormsg("Please select report first");
                return;
            }
            if (!checkFileExtention(ImageUpdate))
            {
                Errormsg("File must be png, jpg, jpeg type");
                return;
            }
            string _fileFormat = GetMimeDataOfFile(ImageUpdate.PostedFile);
            if ((_fileFormat == "image/png") || (_fileFormat == "image/jpeg"))
            {
            }
            else
            {
                Errormsg("File must png, jpg, jpeg type");
                return;
            }
            decimal size = Math.Round((Convert.ToDecimal(ImageUpdate.PostedFile.ContentLength) / Convert.ToDecimal(1024)), 2);
            if (size > 100 || size < 10)
            {
                Errormsg("File size must be between 10 kb to 100 kb");
                return;
            }
            byte[] PhotoImage = null;
            PhotoImage = convertByteFile(ImageUpdate);
            imgImageUpdate.ImageUrl = GetImage(PhotoImage);
            imgImageUpdate.Visible = true;
            Session["PhotoImgUpdate"] = PhotoImage;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("SysadmEmployeeDetails-E2", ex.Message.ToString());
        }
    }



    #endregion

    protected void grvVerifiedEmployees_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvVerifiedEmployees.PageIndex = e.NewPageIndex;
        LoadAllEmployees();
    }
}
