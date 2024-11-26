using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_SysadmEmployeeDetails : BasePage 
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    byte[] PhotoImage = null;
  
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["_moduleName"] = "Employee Management";
        if (!IsPostBack)
        {
            Random randomclass = new Random();
            Session["rndNoCheck"] = randomclass.Next().ToString();
            hidtoken.Value = Session["rndNoCheck"].ToString();
            loadbloodgroup(ddlBloodGroup);
            loadDesignation(ddldesignation);
            loadState(ddlState);
            loadofficelvl(ddlOfficeLevel);
            loadofc(ddlOfficeLevel.SelectedValue, ddlOfficeType);

            loadPostingofc(ddlOfficeType.SelectedValue, ddlPostingoffice);
            loadEmpClass(ddlEmpClass);
            loadEmpDutyType(ddlEmpdutyType);
            loadEmpType(ddlEmployeetype);
            Session["PhotoImg"] = null;
        }
		loadEmployeeCount();
	}

    #region "Method"
    private void CsrfTokenValidate()
    {
        if (hidtoken.Value != Session["rndNoCheck"].ToString())
        {
            Response.Redirect("errorpage.aspx");
            return;
        }
    }
    private void initpnl()
    {
        
        pnlAddNewEmployee.Visible = false;
        pnlupdate.Visible = false;
        pnlVerifiedEmp.Visible = false;
        lbtnUpdate.Visible = false;
        lbtnSave.Visible = true;
        lbtnRest.Visible = true;

    }
    
    private void loadbloodgroup(DropDownList ddlBloodGroup)//M1
    {
        try
        {
            ddlBloodGroup.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_bloodgrp");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlBloodGroup.DataSource = dt;
                    ddlBloodGroup.DataTextField = "groupname";
                    ddlBloodGroup.DataValueField = "groupid";
                    ddlBloodGroup.DataBind();
                }
            }
            else
            {
                _common.ErrorLog("SysadmEmployeeDetails.aspx-0001", dt.TableName);
            }            
            ddlBloodGroup.Items.Insert(0, "SELECT");
            ddlBloodGroup.Items[0].Value = "0";
            ddlBloodGroup.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlBloodGroup.Items.Insert(0, "SELECT");
            ddlBloodGroup.Items[0].Value = "0";
            ddlBloodGroup.SelectedIndex = 0;
            Errormsg("There is some error.");
            _common.ErrorLog("SysadmEmployeeDetails.aspx-0002", ex.Message.ToString());
        }
    }
    private void loadDesignation(DropDownList ddldesignation)//M2
    {
        try
        {
            ddldesignation.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_designation");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddldesignation.DataSource = dt;
                    ddldesignation.DataTextField = "designationname";
                    ddldesignation.DataValueField = "designationcode";
                    ddldesignation.DataBind();
                }
            }
            else
            {
                _common.ErrorLog("SysadmEmployeeDetails.aspx-0003", dt.TableName);
            }
            ddldesignation.Items.Insert(0, "SELECT");
            ddldesignation.Items[0].Value = "0";
            ddldesignation.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddldesignation.Items.Insert(0, "SELECT");
            ddldesignation.Items[0].Value = "0";
            ddldesignation.SelectedIndex = 0;
            Errormsg("There is some error.");
            _common.ErrorLog("SysadmEmployeeDetails.aspx-0004", ex.Message.ToString());
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
                _common.ErrorLog("SysadmEmployeeDetails.aspx-0005", dt.TableName);
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
            Errormsg("There is some error.");
            _common.ErrorLog("SysadmEmployeeDetails.aspx-0006", ex.Message.ToString());
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
                _common.ErrorLog("SysadmEmployeeDetails.aspx-0007", dt.TableName);
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
            Errormsg("There is some error.");
            _common.ErrorLog("SysadmEmployeeDetails.aspx-0008", ex.Message.ToString());
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
            if(dt.TableName == "Success")
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
                _common.ErrorLog("SysadmEmployeeDetails.aspx-0009", dt.TableName);
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
            Errormsg("There is some error.");
            _common.ErrorLog("SysadmEmployeeDetails.aspx-0010", ex.Message.ToString());
        }
    }
    private void loadEmployeeCount()//M6
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_employeecount");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName=="Success")
            {
                if (dt.Rows.Count > 0)
                {
                    lblTotalEmp.Text = "Total Employees - " + dt.Rows[0]["tot_emp"].ToString();
                    lblVerified.Text = dt.Rows[0]["verify_emp"].ToString();
                    lblNotVerified.Text = dt.Rows[0]["nonverify_emp"].ToString();
                    lblDriverP.Text = dt.Rows[0]["pendlicense_drv"].ToString();
                    lblConductorP.Text = dt.Rows[0]["pendlicense_cond"].ToString();
                    lblDriverExpSoon.Text = dt.Rows[0]["exprsoondlicense_drv"].ToString();
                    lblConductorExpSoon.Text = dt.Rows[0]["exrdsoonlicense_cond"].ToString();
                    lblDriverExp.Text = dt.Rows[0]["exprdlicense_drv"].ToString();
                    lblConductorExp.Text = dt.Rows[0]["exrdlicense_cond"].ToString();
                }
            }
            else
            {
                _common.ErrorLog("SysadmEmployeeDetails.aspx-0011", dt.TableName);
            }
            
        }
        catch (Exception ex)
        {
            Errormsg("There is some error.");
            _common.ErrorLog("SysadmEmployeeDetails.aspx-0012", ex.Message.ToString());
        }
    }
    private bool validvalue()//M7
    {
        try
        {
            int msgcount = 0;
            string msg = "";

            if (_validation.IsValidString(tbFirstName.Text, 1, 50) == false)
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Enter Valid First Name.<br/>";
            }
            if (_validation.IsValidString(tbDob.Text, 1, 10) == false)
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Enter Valid Date of Birth.<br/>";
            }
            if (ddlGender.SelectedValue == "0")
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount + ". Select Gender <br/>";
            }
            if (_validation.isValidMobileNumber(tbMobile.Text) == false)
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Enter Valid Mobile Number.<br/>";
            }
           else if (_validation.IsValidString(tbMobile.Text, 10, 10) == false)
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Enter Valid Mobile Number.<br/>";
            }
            if (_validation.IsValidString(tbEmergency.Text, 10, 10) == false)
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Enter Valid Emergency Number.<br/>";
            }
            if (_validation.IsValidString(tbCity.Text, 1, 50) == false)
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Enter Valid City.<br/>";
            }
            if (ddlState.SelectedValue == "0")
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount + ". Select State <br/>";
            }
            if (ddlOfficeLevel.SelectedValue == "0")
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount + ". Select Office Level <br/>";
            }
            if (ddlOfficeType.SelectedValue == "0")
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount + ". Select Office <br/>";
            }
            if (ddlPostingoffice.SelectedValue == "0")
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount + ". Select Posting Office <br/>";
            }
            if (ddlEmpClass.SelectedValue == "0")
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount + ". Select Employee Class <br/>";
            }
            if (ddlEmpdutyType.SelectedValue == "0")
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount + ". Select Employee Duty type <br/>";
            }
            if (ddlEmployeetype.SelectedValue == "0")
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount + ". Select Employee Type <br/>";
            }
            if (ddldesignation.SelectedValue == "0")
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount + ". Select Employee Designation <br/>";
            }
            
            if (msgcount > 0)
            {
                Errormsg(msg);
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("SysadmEmployeeDetails-M7", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
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
            Errormsg("There is some error.");
            _common.ErrorLog("SysadmEmployeeDetails.aspx-0013", ex.Message.ToString());
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
    public string GetImage(object img)
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
    private void SaveEmployeeDetails()//M9
    {
        try
        {
           
            string fname, mname, lname, dob, fthrname, gendr, emilid, mobno, lndline, emgncyno, addrs, pin, city, stcode, ofcid, dobjoin, dobjoinofc, emptype, wkrstday,licenseno,licensedate,postinofc,postingdate, dutytype;
            int bldgrp, ofclvl, desination,empclass;
            byte[] photo = null;
            photo = Session["PhotoImg"] as byte[];
            fname = tbFirstName.Text;
            mname = tbMiddleName.Text;
            lname = tbLastName.Text;
            dob = tbDob.Text.ToString();
            fthrname = tbFatherName.Text;
            gendr = ddlGender.SelectedValue.ToString();
            bldgrp = Convert.ToInt16(ddlBloodGroup.SelectedValue.ToString());
            emilid = tbEmail.Text;
            mobno = tbMobile.Text;
            lndline = tbLandline.Text;
            emgncyno = tbEmergency.Text;
            addrs = tbAddress.Text;
            pin = tbPinCode.Text;
            city = tbCity.Text;
            stcode = ddlState.SelectedValue.ToString();
            ofclvl = Convert.ToInt32(ddlOfficeLevel.SelectedValue.ToString());
            ofcid = ddlOfficeType.SelectedValue.ToString();
           

            if (tbDateOfJoining.Text == "")
            { dobjoin = ""; }
            else
            { dobjoin = tbDateOfJoining.Text; }

            if (tbDOJOffice.Text == "")
            { dobjoinofc = ""; }
            else
            { dobjoinofc = tbDOJOffice.Text; }

            emptype = ddlEmployeetype.SelectedValue.ToString();
            wkrstday = ddlWeeklyRestDay.SelectedValue.ToString();
            desination = Convert.ToInt32(ddldesignation.SelectedValue.ToString());
            licenseno = tblicenseNoS.Text;
            if (tblicensevalidupto.Text == "")
            { licensedate = ""; }
            else
            { licensedate = tblicensevalidupto.Text; }

            
            postinofc = ddlPostingoffice.SelectedValue;
            

            if (tbDOPoffice.Text == "")
            { postingdate = ""; }
            else
            { postingdate = tbDOPoffice.Text; }

            empclass = Convert.ToInt32(ddlEmpClass.SelectedValue);
            dutytype = ddlEmpdutyType.SelectedValue;

            string ipaddress = HttpContext.Current.Request.UserHostAddress;
            if (Session["Action"].ToString() == "S")
            {
                Session["Empid"] = 0;
            }
            

            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            string Mresult = "";
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_employee_insertupdate");
            MyCommand.Parameters.AddWithValue("p_action", Session["Action"].ToString());
            MyCommand.Parameters.AddWithValue("p_empid",Convert.ToInt32(Session["Empid"].ToString()));
            MyCommand.Parameters.AddWithValue("p_fname", fname);
            MyCommand.Parameters.AddWithValue("p_mname", mname);
            MyCommand.Parameters.AddWithValue("p_lname", lname);
            MyCommand.Parameters.AddWithValue("p_dob", dob);
            MyCommand.Parameters.AddWithValue("p_fthrname", fthrname);
            MyCommand.Parameters.AddWithValue("p_gendr", gendr);
            MyCommand.Parameters.AddWithValue("p_bldgrp", bldgrp);
            MyCommand.Parameters.AddWithValue("p_emilid", emilid);
            MyCommand.Parameters.AddWithValue("p_mobno", mobno);
            MyCommand.Parameters.AddWithValue("p_lndline", lndline);
            MyCommand.Parameters.AddWithValue("p_emgncyno", emgncyno);
            MyCommand.Parameters.AddWithValue("p_addrs", addrs);
            MyCommand.Parameters.AddWithValue("p_pin", pin);
            MyCommand.Parameters.AddWithValue("p_city", city);
            MyCommand.Parameters.AddWithValue("p_stcode", stcode);
            MyCommand.Parameters.AddWithValue("p_ofclvl", ofclvl);
            MyCommand.Parameters.AddWithValue("p_ofcid", ofcid);
            MyCommand.Parameters.AddWithValue("p_dobjoin", dobjoin);
            MyCommand.Parameters.AddWithValue("p_dobjoinofc", dobjoinofc);
            MyCommand.Parameters.AddWithValue("p_emptype", emptype);
            MyCommand.Parameters.AddWithValue("p_wkrstday", wkrstday);
            MyCommand.Parameters.AddWithValue("p_desination", desination);
            
            MyCommand.Parameters.AddWithValue("p_updateby", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("p_ipaddress", ipaddress);
            MyCommand.Parameters.AddWithValue("p_licenseno", licenseno );
            MyCommand.Parameters.AddWithValue("p_licensedate", licensedate);
            MyCommand.Parameters.AddWithValue("p_postingofc", postinofc);
            MyCommand.Parameters.AddWithValue("p_postingdate", postingdate);
            MyCommand.Parameters.AddWithValue("p_class", empclass);
            MyCommand.Parameters.AddWithValue("p_dutytype", dutytype );
            MyCommand.Parameters.AddWithValue("p_photo", photo);
            Mresult = bll.UpdateAll(MyCommand);
            if (Mresult == "Success")
            {
                Successmsg("Employee Details Successfully Saved");
               
				loadEmployeeCount();
                resetcntrl();

            }
            else
            {
                _common.ErrorLog("SysadmEmployeeDetails.aspx-0014", Mresult);
                Errormsg(Mresult);
            }
        }
        catch (Exception ex)
        {
            Errormsg("There is some error.");
            _common.ErrorLog("SysadmEmployeeDetails.aspx-0015", ex.Message.ToString());
        }
    }
    private void resetcntrl()
    {
        Session["PhotoImg"] = null;
        Session["Action"] = null;
        Session["Empid"] = null;
        tbFirstName.Text = "";
        tbMiddleName.Text = "";
        tbLastName.Text = "";
        tbDob.Text = "";
        tbFatherName.Text = "";
        ddlGender.SelectedValue = "0";
        ddlBloodGroup.SelectedValue = "0";
        ddlEmpClass.SelectedValue = "0";
        ddlEmployeetype.SelectedValue = "0";
        ddlEmpdutyType.SelectedValue = "0";
        tbMobile.Text = "";
        tbLandline.Text = "";
        tbEmergency.Text = "";
        tbAddress.Text = "";
        tbPinCode.Text = "";
        tbCity.Text = "";
        tbDateOfJoining.Text = "";
        tbDOJOffice.Text = "";
        ddlEmployeetype.SelectedValue = "0";
        ddldesignation.SelectedValue = "0";
        ddlWeeklyRestDay.SelectedValue = "0";
        ddlEmpdutyType.SelectedValue = "0";
        ddlEmpClass.SelectedValue = "0";
        Session["PhotoImg"] = null;
        loadState(ddlState);
        loadofficelvl(ddlOfficeLevel);
        loadofc(ddlOfficeLevel.SelectedValue, ddlOfficeType);
        loadPostingofc(ddlOfficeType.SelectedValue, ddlPostingoffice);
        rbtnAdd.Checked = true;
        rbtnUpdate.Checked = false;
        rbtnVerUpdate.Checked = false;
        initpnl();
        pnlAddNewEmployee.Visible = true;
        tblicenseNoS.Text = "";
        tblicensevalidupto.Text = "";
        divlicense.Visible = false;
        divlicensevlaid.Visible = false;
        imgPhoto.Visible = false;
        tbEmail.Text = "";
    }
    private void loadDraftEmp()//M10
    {
        try
        {

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_draftemp");
            MyCommand.Parameters.AddWithValue("p_emptype","D");
            MyCommand.Parameters.AddWithValue("p_ofclvlid", Convert.ToInt16(ddlOfficeLvl2.SelectedValue.ToString()));
            MyCommand.Parameters.AddWithValue("p_ofcid", ddlOffice2.SelectedValue.ToString());
            MyCommand.Parameters.AddWithValue("p_designation", Convert.ToInt16(ddlDepotWiseDesig.SelectedValue.ToString()));
            MyCommand.Parameters.AddWithValue("p_lincensestatus", ddlLicenseStatus.SelectedValue.ToString());
            MyCommand.Parameters.AddWithValue("p_employeename", tbEmloyeeName.Text.ToString());
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName=="Success")
            {
                if (dt.Rows.Count > 0)
                {
                    grvEmployees.DataSource = dt;
                    grvEmployees.DataBind();
                    grvEmployees.Visible = true;
                    lblNoEmpData.Visible = false;
                }
                else
                {
                    grvEmployees.Visible = false;
                    lblNoEmpData.Visible = true;
                }
            }
            else
            {
                Errormsg("There is some error.");
                _common.ErrorLog("SysadmEmployeeDetails.aspx-0016", dt.TableName);
                grvEmployees.Visible = false;
                lblNoEmpData.Visible = true;
            }

        }
        catch (Exception ex)
        {
            grvEmployees.Visible = false;
            lblNoEmpData.Visible = true;
            Errormsg(ex.Message.ToString());
            Errormsg("There is some error.");
            _common.ErrorLog("SysadmEmployeeDetails.aspx-0017", ex.Message.ToString());
        }
    }
    private void VerifyEmployeeDetails()//M11
    {
        try
        {
            string ipaddress = HttpContext.Current.Request.UserHostAddress;
            string PasswordForEmp = RandomString(8);//"test@123";//
            string PASSWORD = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(PasswordForEmp, "SHA1");
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            string Mresult = "";
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_employee_verify");
            MyCommand.Parameters.AddWithValue("p_empid", Session["Empid"].ToString());
            MyCommand.Parameters.AddWithValue("p_emppwd", PASSWORD);
            MyCommand.Parameters.AddWithValue("p_updateby", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("p_ipaddress", ipaddress);
            Mresult = bll.UpdateAll(MyCommand);
            if (Mresult == "Success")
            {
                resetcntrl();
                initpnl();
                pnlAddNewEmployee.Visible = true;
                Successmsg("Employee details successfully verifyed");
				loadEmployeeCount();
			}
            else
            {
                _common.ErrorLog("SysadmEmployeeDetails.aspx-0018", Mresult);
                Errormsg("Unable to Verify Employee Details"+ Mresult);
            }
        }
        catch (Exception ex)
        {
            Errormsg("There is some error.");
            _common.ErrorLog("SysadmEmployeeDetails.aspx-0019", ex.Message.ToString());
        }
    }
    public string RandomString(int length)
    {
        Random ran = new Random();

        String b = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789@#";
      
       String random = "";
        for (int i = 0; i < length; i++)
        {
            int a = ran.Next(b.Length); //string.Lenght gets the size of string
            random = random + b.ElementAt(a);
        }
      
        return "test@123";//random;
            //Random random = new Random();
        //char[] charOutput = new char[length - 1 + 1];
        //for (int i = 0; i <= length - 1; i++)
        //{
        //    int selector = random.Next(65, 122);
        //    if (selector > 90 & selector < 97)
        //        selector += 10;
        //    else if (selector > 110 & selector < 121)
        //        selector = 64;
        //    charOutput[i] = Convert.ToChar(selector);
        //}
        //charOutput = charOutput + "@" + DateTime.Now.Millisecond;//System.DateTime.Now().Millisecond.ToString();
        //return new string(charOutput);
    }
    private void loadverifyemp()//M12
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_draftemp");
            MyCommand.Parameters.AddWithValue("p_emptype", "V");
            MyCommand.Parameters.AddWithValue("p_ofclvlid", Convert.ToInt16(ddlOfficeLvl2.SelectedValue.ToString()));
            MyCommand.Parameters.AddWithValue("p_ofcid", ddlOffice2.SelectedValue.ToString());
            MyCommand.Parameters.AddWithValue("p_designation", Convert.ToInt16(ddlDepotWiseDesig.SelectedValue.ToString()));
            MyCommand.Parameters.AddWithValue("p_lincensestatus", ddlLicenseStatus.SelectedValue.ToString());
            MyCommand.Parameters.AddWithValue("p_employeename", tbEmloyeeName.Text.ToString());
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName=="Success")
            {
                if (dt.Rows.Count > 0)
                {
                    gvVerifiedEmployees.DataSource = dt;
                    gvVerifiedEmployees.DataBind();
                    gvVerifiedEmployees.Visible = true;
                    lblNoEmpData.Visible = false;
                }
                else
                {
                    gvVerifiedEmployees.Visible = false;
                    lblNoEmpData.Visible = true;
                }
            }
            else
            {
                gvVerifiedEmployees.Visible = false;
                lblNoEmpData.Visible = true;
                _common.ErrorLog("SysadmEmployeeDetails.aspx-0020", dt.TableName);
            }
           
        }
        catch (Exception ex)
        {
            gvVerifiedEmployees.Visible = false;
            lblNoEmpData.Visible = true;
            Errormsg("There is some error.");
            _common.ErrorLog("SysadmEmployeeDetails.aspx-0021", ex.Message.ToString());
        }
    }
    private void initemppnl()
    {
        pnlServiceStatus.Visible = false;
        pnlPersonal.Visible = false;
        pnlContact.Visible = false;
        pnlOfficeDetails.Visible = false;
        pnlUpdatePhoto.Visible = false;
        pnlLicense.Visible = false;
        pnlWeeklyRest.Visible = false;
        pnlDutyType.Visible = false;
        pnlServiceStatus.Visible = false;
        pnlupdate.Visible = true;
        pnlAddNewEmployee.Visible = false;
        pnlVerifiedEmp.Visible = true;        
        pnlButton.Visible = true;
        loadverifyemp();
        Session["PhotoImgUpdate"] = null;
    }
    private bool validvaluePersonalUpdate()//M13
    {
        try
        {

            int msgcount = 0;
            string msg = "";
            if (_validation.IsValidString(tbFirstNameUpdate.Text, 1, 50) == false)
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Enter Valid First Name.<br/>";
            }
            if (ddlGenderUpdate.SelectedValue == "0")
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount + ". Select Gender <br/>";
            }
            if (_validation.IsValidString(tbDobUpdate.Text, 1, 10) == false)
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Enter Valid Date of Birth.<br/>";
            }
            if (msgcount > 0)
            {
                Errormsg(msg);
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            Errormsg("There is some error.");
            _common.ErrorLog("SysadmEmployeeDetails.aspx-0022", ex.Message.ToString());
            return false;
        }

    }
    private void InsertPersonalDetails()//M14
    {
        try
        {
            string fname, mname, lname, dob, fthrname, gendr;
            int bldgrp;
            fname = tbFirstNameUpdate.Text;
            mname = tbMiddleNameUpdate.Text;
            lname = tbLastNameUpdate.Text;
            fthrname = tbFatherNameUpdate.Text;
            gendr = ddlGenderUpdate.SelectedValue;
            bldgrp = Convert.ToInt16(ddlBloodGroupUpdate.SelectedValue.ToString());
            dob = tbDobUpdate.Text;
            string ipaddress = HttpContext.Current.Request.UserHostAddress;
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            string Mresult = "";
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_employee_personaldetails");
            MyCommand.Parameters.AddWithValue("p_action", Session["Action"].ToString());
            MyCommand.Parameters.AddWithValue("p_savetype", Session["SaveType"].ToString());
            MyCommand.Parameters.AddWithValue("p_empcode", Session["Empid"]);
            MyCommand.Parameters.AddWithValue("p_fname", fname);
            MyCommand.Parameters.AddWithValue("p_mname", mname);
            MyCommand.Parameters.AddWithValue("p_lname", lname);
            MyCommand.Parameters.AddWithValue("p_fthrname", fthrname);
            MyCommand.Parameters.AddWithValue("p_gendr", gendr);
            MyCommand.Parameters.AddWithValue("p_bldgrp", bldgrp);
            MyCommand.Parameters.AddWithValue("p_dob", dob);
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
                _common.ErrorLog("SysadmEmployeeDetails.aspx-0023", Mresult);
            }
        }
        catch (Exception ex)
        {
            Errormsg("There is some error.");
            _common.ErrorLog("SysadmEmployeeDetails.aspx-0024", ex.Message.ToString());
        }
    }
    private bool validvalueContactUpdate()//M15
    {
        try
        {
            int msgcount = 0;
            string msg = "";
            if (_validation.isValidMobileNumber(tbMobileNumberUpdate.Text) == false)
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Enter Valid Mobile Number.<br/>";
            }
            else if (_validation.IsValidString(tbMobileNumberUpdate.Text, 10, 10) == false)
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Enter Valid Mobile Number.<br/>";
            }
            if (_validation.IsValidString(tbEmergencyNoUpdate.Text, 10, 10) == false)
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Enter Valid Emergency Number.<br/>";
            }
            if (_validation.IsValidString(tbCityUpdate.Text, 1, 50) == false)
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Enter Valid City.<br/>";
            }
            if (ddlStateUpdate.SelectedValue == "0")
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount + ". Select State <br/>";
            }            
            if (msgcount > 0)
            {
                Errormsg(msg);
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("SysadmEmployeeDetails.aspx-0025", ex.Message.ToString());
            return false;
        }

    }
    private void InsertContactDetails()//M16
    {
        try
        {
            string emilid, mobno, lndline, emgncyno, addrs, pin, city, stcode;
            emilid = tbEmailUpdate.Text;
            mobno = tbMobileNumberUpdate.Text;
            lndline = tbLandlineNumberUpdate.Text;
            emgncyno = tbEmergencyNoUpdate.Text;
            addrs = tbAddressUpdate.Text;
            pin = tbPinCodeUpdate.Text;
            city = tbCityUpdate.Text;
            stcode = ddlStateUpdate.SelectedValue.ToString();
            string ipaddress = HttpContext.Current.Request.UserHostAddress;
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            string Mresult = "";
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_employee_contactdetails");
            MyCommand.Parameters.AddWithValue("p_action", Session["Action"].ToString());
            MyCommand.Parameters.AddWithValue("p_savetype", Session["SaveType"].ToString());
            MyCommand.Parameters.AddWithValue("p_empcode", Session["Empid"]);
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
                _common.ErrorLog("SysadmEmployeeDetails.aspx-0025", Mresult);
            }
        }
        catch (Exception ex)
        {
            Errormsg("There is some error.");
            _common.ErrorLog("SysadmEmployeeDetails.aspx-0026", ex.Message.ToString());
        }
    }
    private bool validvalueOfficialUpdate()//M17
    {
        try
        {
            int msgcount = 0;
            string msg = "";
            if (ddlOfficeLevelUpdate.SelectedValue == "0")
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount + ". Select Office Level <br/>";
            }
            if (ddlOffice.SelectedValue == "0")
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount + ". Select Office <br/>";
            }
            if (String.IsNullOrEmpty(tbDoJupdate.Text)==false)
            {
                if (_validation.IsDate(tbDoJupdate.Text)==false)
                {
                    msgcount = msgcount + 1;
                    msg = msg + msgcount + ". Enter Date Of Joining <br/>";
                }
            }
            if (String.IsNullOrEmpty(tbDoJupdateOf.Text) == false)
            {
                if (_validation.IsDate(tbDoJupdateOf.Text)==false)
                {
                    msgcount = msgcount + 1;
                    msg = msg + msgcount + ". Enter Date of Joining at Office <br/>";
                }
            }
            if (ddlEmployeeTypeUpdate.SelectedValue=="0")
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount + ". Select Employee type <br/>";
            }
            if (ddlDesignationUpdate.SelectedValue == "0")
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount + ". Select Designation <br/>";
            }
            if (msgcount > 0)
            {
                Errormsg(msg);
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("SysadmEmployeeDetails.aspx-0027", ex.Message.ToString());
            return false;
        }

    }
    private void InsertOfficeDetails()//M18
    {
        try
        {
            string ofcid, dobjoin, dobjoinofc, emptype,postingofc,dateofposting;
            int ofclvl,designation;
            ofclvl = Convert.ToInt16(ddlOfficeLevelUpdate.SelectedValue.ToString());
            ofcid = ddlOffice.SelectedValue.ToString();
            dobjoin = tbDoJupdate.Text;
            dobjoinofc = tbDoJupdateOf.Text;
            emptype = ddlEmployeeTypeUpdate.SelectedValue.ToString();
            designation = Convert.ToInt16(ddlDesignationUpdate.SelectedValue.ToString());
            postingofc = ddlpostingOfficeUpdate.SelectedValue.ToString();
            dateofposting = tbPostingDateUpdate.Text;
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
            MyCommand.Parameters.AddWithValue("p_empcode", Session["Empid"]);
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
                _common.ErrorLog("SysadmEmployeeDetails.aspx-0028", Mresult);
            }
        }
        catch (Exception ex)
        {
            Errormsg("There is some error.");
            _common.ErrorLog("SysadmEmployeeDetails.aspx-0029", ex.Message.ToString());
        }
    }
    private bool validvaluePhotoUpdate()//M19
    {
        try
        {
            int msgcount = 0;
            string msg = "";
            if (Session["PhotoImgUpdate"] ==null || Session["PhotoImgUpdate"] =="")
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount + ". Select Employee Photo <br/>";
            }
           
            if (msgcount > 0)
            {
                Errormsg(msg);
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("SysadmEmployeeDetails-M19", ex.Message.ToString());
            return false;
        }

    }
    private void InsertPhotoDetails()//M20
    {
        try
        {
            string ipaddress = HttpContext.Current.Request.UserHostAddress;
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            string Mresult = "";
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_employee_photodetails");
            MyCommand.Parameters.AddWithValue("p_action", Session["Action"].ToString());
            MyCommand.Parameters.AddWithValue("p_savetype", Session["SaveType"].ToString());
            MyCommand.Parameters.AddWithValue("p_empcode", Session["Empid"]);
            MyCommand.Parameters.AddWithValue("p_photo", Session["PhotoImgUpdate"]);
            MyCommand.Parameters.AddWithValue("p_updateby", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("p_ipaddress", ipaddress);
            Mresult = bll.UpdateAll(MyCommand);
            if (Mresult == "Success")
            {
                initemppnl();
                Successmsg("Employee Photo successfully saved");
            }
            else
            {
                Errormsg("Unable to Update Employee Photo");
                _common.ErrorLog("SysadmEmployeeDetails.aspx-0030", Mresult);
            }
        }
        catch (Exception ex)
        {
            Errormsg("There is some error.");
            _common.ErrorLog("SysadmEmployeeDetails.aspx-0031", ex.Message.ToString());
        }
    }
    private bool validvalueLicenceUpdate()//M21
    {
        try
        {
            int msgcount = 0;
            string msg = "";
            if (_validation.IsValidString(tbLicenseNo.Text, 10, 12) == false)
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Enter Valid License Number.<br/>";
            }
            if (_validation.IsDate(tbDateOfLicense.Text) == false)
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount + ". Enter Valid License Validity Date <br/>";
            }
            if (msgcount > 0)
            {
                Errormsg(msg);
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("SysadmEmployeeDetails-M21", ex.Message.ToString());
            return false;
        }
    }
    private void InsertLicenseDetails()//M22
    {
        try
        {
            string licenseno, licensedate;
            licenseno = tbLicenseNo.Text;
            licensedate = tbDateOfLicense.Text;
            string ipaddress = HttpContext.Current.Request.UserHostAddress;
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            string Mresult = "";
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_employee_licensedetails");
            MyCommand.Parameters.AddWithValue("p_action", Session["Action"].ToString());
            MyCommand.Parameters.AddWithValue("p_savetype", Session["SaveType"].ToString());
            MyCommand.Parameters.AddWithValue("p_empcode", Session["Empid"]);
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
              _common.ErrorLog("SysadmEmployeeDetails.aspx-0032", Mresult);
                Errormsg("Unable to Update Employee License Details");
            }
        }
        catch (Exception ex)
        {
            Errormsg("There is some error.");
            _common.ErrorLog("SysadmEmployeeDetails.aspx-0033", ex.Message.ToString());
        }
    }
    private bool validvalueWeekrestUpdate()//M23
    {
        try
        {
            int msgcount = 0;
            string msg = "";
            if (ddlweekrestdays.SelectedValue=="0")
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Select Valid Week Rest Day.<br/>";
            }
            if (msgcount > 0)
            {
                Errormsg(msg);
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("SysadmEmployeeDetails-M23", ex.Message.ToString());
            return false;
        }
    }
    private void InsertWeekrestdayDetails()//M24
    {
        try
        {
            string  wkrstday;
            wkrstday = ddlweekrestdays.SelectedValue;
            string ipaddress = HttpContext.Current.Request.UserHostAddress;
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            string Mresult = "";
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_employee_weekrestdetails");
            MyCommand.Parameters.AddWithValue("p_action", Session["Action"].ToString());
            MyCommand.Parameters.AddWithValue("p_savetype", Session["SaveType"].ToString());
            MyCommand.Parameters.AddWithValue("p_empcode", Session["Empid"].ToString());
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
                _common.ErrorLog("SysadmEmployeeDetails.aspx-0034", Mresult);
                Errormsg("Unable to Update Employee Weekrest day");
            }
        }
        catch (Exception ex)
        {
            Errormsg("There is some error.");
            _common.ErrorLog("SysadmEmployeeDetails.aspx-0035", ex.Message.ToString());
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
                _common.ErrorLog("SysadmEmployeeDetails.aspx-0036", dt.TableName);
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
            Errormsg("There is some error.");
            _common.ErrorLog("SysadmEmployeeDetails.aspx-0037", ex.Message.ToString());
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


                    ddlEmployeeTypeUpdate.DataSource = dt;
                    ddlEmployeeTypeUpdate.DataTextField = "typename";
                    ddlEmployeeTypeUpdate.DataValueField = "typeid";
                    ddlEmployeeTypeUpdate.DataBind();
                    
                }
            }
            else
            {
                _common.ErrorLog("SysadmEmployeeDetails.aspx-0038", dt.TableName);
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
            Errormsg("There is some error.");
            _common.ErrorLog("SysadmEmployeeDetails.aspx-0039", ex.Message.ToString());
        }
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
                _common.ErrorLog("SysadmEmployeeDetails.aspx-0040", dt.TableName);
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
            Errormsg("There is some error.");
            _common.ErrorLog("SysadmEmployeeDetails.aspx-0041", ex.Message.ToString());
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
                _common.ErrorLog("SysadmEmployeeDetails.aspx-0042", dt.TableName);
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
            Errormsg("There is some error.");
            _common.ErrorLog("SysadmEmployeeDetails.aspx-0043", ex.Message.ToString());
        }
    }
    private void Successmsg(string msg)
    {
        string popup = _popup.modalPopupSmall("S", "Success", msg, "Close");
        Response.Write(popup);
    }
    private void Errormsg(string msg)
    {
        string popup = _popup.modalPopupSmall("D", "Please Check", msg, "Close");
        Response.Write(popup);
    }
    private bool validvalueDutyTypeUpdate()
    {
        try
        {
            int msgcount = 0;
            string msg = "";
            if (ddlempclassupdate.SelectedValue == "0")
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount + ". Select Employee Class <br/>";
            }
            if (ddldutytypeupdate.SelectedValue == "0")
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Select Duty Type.<br/>";
            }
            if (msgcount > 0)
            {
                Errormsg(msg);
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("SysadmEmployeeDetails-M23", ex.Message.ToString());
            return false;
        }
    }
    private void InsertDutyTypeDetails()//29
    {
        try
        {
            string dutytype;
            int empclass;
            dutytype = ddldutytypeupdate.SelectedValue;
            empclass =Convert.ToInt32( ddlempclassupdate.SelectedValue);
            string ipaddress = HttpContext.Current.Request.UserHostAddress;
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            string Mresult = "";
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_employee_dutytype");
            MyCommand.Parameters.AddWithValue("p_action", Session["Action"].ToString());
            MyCommand.Parameters.AddWithValue("p_savetype", Session["SaveType"].ToString());
            MyCommand.Parameters.AddWithValue("p_empcode", Session["Empid"]);
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
                _common.ErrorLog("SysadmEmployeeDetails.aspx-0044", Mresult);
                Errormsg("Unable to Update Employee DutyType Details");
            }
        }
        catch (Exception ex)
        {
            Errormsg("There is some error.");
            _common.ErrorLog("SysadmEmployeeDetails.aspx-0045", ex.Message.ToString());
        }
    }
    private bool validvalueServiceStatusUpdate()
    {
        try
        {
            int msgcount = 0;
            string msg = "";
            if (ddlservicestatusUpdate.SelectedValue == "0")
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount + ". Select Employee Service Status <br/>";
            }
            if (tbOrderDateUpdate.Text == "")
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Enter Order Date.<br/>";
            }
            if (tbofficeorderUpdate.Text == "")
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Enter Order No.<br/>";
            }
            if (_validation.IsValidString(tbofficeorderUpdate.Text, 5, 20) == false)
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Enter Valid License Number.<br/>";
            }
            if (_validation.IsDate(tbOrderDateUpdate.Text) == false)
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount + ". Enter Valid Order Date <br/>";
            }
            if (msgcount > 0)
            {
                Errormsg(msg);
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("SysadmEmployeeDetails-M23", ex.Message.ToString());
            return false;
        }
    }
    //private void loadServiceStatus(DropDownList ddldesignation);
    
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
                _common.ErrorLog("SysadmEmployeeDetails.aspx-0046", dt.TableName);
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
            Errormsg("There is some error.");
            _common.ErrorLog("SysadmEmployeeDetails.aspx-0047", ex.Message.ToString());
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
            MyCommand.Parameters.AddWithValue("p_empcode", Session["Empid"]);
            MyCommand.Parameters.AddWithValue("p_servicestatus",Convert.ToInt32( ddlservicestatusUpdate.SelectedValue) );
            MyCommand.Parameters.AddWithValue("p_orderno", tbofficeorderUpdate.Text );
            MyCommand.Parameters.AddWithValue("p_orderdate", tbOrderDateUpdate.Text );
            MyCommand.Parameters.AddWithValue("p_remark", tbremarkupdate.Text );
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
                _common.ErrorLog("SysadmEmployeeDetails.aspx-0048", Mresult);
                Errormsg("Unable to Update Employee DutyType Details");
            }
        }
        catch (Exception ex)
        {
            Errormsg("There is some error.");
            _common.ErrorLog("SysadmEmployeeDetails.aspx-0049", ex.Message.ToString());
        }
    }
    #endregion

    #region "Events"
    protected void ddldesignation_SelectedIndexChanged(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        divlicensevlaid.Visible = false;
        divlicense.Visible = false;
        if (ddldesignation.SelectedValue == "1" || ddldesignation.SelectedValue == "2")
        {
            divlicensevlaid.Visible = true;
            divlicense.Visible = true;
        }
    }
    protected void rbtnAdd_CheckedChanged(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        resetcntrl();
        initpnl();
        pnlAddNewEmployee.Visible = true;
    }
    protected void rbtnUpdate_CheckedChanged(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        initpnl();
        pnlupdate.Visible = true;
        loadofficelvl(ddlOfficeLvl2);
        loadofc(ddlOfficeLvl2.SelectedValue, ddlOffice2);
        loadDesignation(ddlDepotWiseDesig);
        loadDraftEmp();
    }
    protected void rbtnVerUpdate_CheckedChanged(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        initpnl();
        pnlupdate.Visible = true;
        pnlVerifiedEmp.Visible = true;
        loadofficelvl(ddlOfficeLvl2);
        loadofc(ddlOfficeLvl2.SelectedValue, ddlOffice2);
        loadDesignation(ddlDepotWiseDesig);
        grvEmployees.Visible = false;
        loadverifyemp();
    }
    protected void ddlOfficeLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        loadofc(ddlOfficeLevel.SelectedValue, ddlOfficeType);
    }
	protected void ddlOfficeLevelUpdate_SelectedIndexChanged(object sender, EventArgs e)
	{
        CsrfTokenValidate();
        loadofc(ddlOfficeLevelUpdate.SelectedValue, ddlOffice);
	}
	protected void btnUploadImage_Click(object sender, EventArgs e)//E1
    {
        try
        {
            if (!fuImage.HasFile)
            {
                Errormsg("Please select file first");
                return;
            }
            if (!checkFileExtention(fuImage))
            {
                Errormsg("File must be png, jpg, jpeg type");
                return;
            }
            string _fileFormat = GetMimeDataOfFile(fuImage.PostedFile);
            if ((_fileFormat == "image/png") || (_fileFormat == "image/jpeg"))
            {
            }
            else
            {
                Errormsg("File must png, jpg, jpeg type");
                return;
            }
            decimal size = Math.Round((Convert.ToDecimal(fuImage.PostedFile.ContentLength) / Convert.ToDecimal(1024)), 2);
            if (size > 100 || size < 10)
            {
                Errormsg("File size must be between 10 kb to 100 kb");
                return;
            }
            byte[] PhotoImage = null;
            PhotoImage = convertByteFile(fuImage);
            imgPhoto.ImageUrl = GetImage(PhotoImage);
            imgPhoto.Visible = true;
            Session["PhotoImg"] = fuImage.FileBytes;


            //byte[] PhotoImage = null;
            //PhotoImage = convertByteFile(FileWebPortal);
            //ImgWebPortal.ImageUrl = GetImage(PhotoImage);
            //ImgWebPortal.Visible = true;
            //lbtncloseWebImage.Visible = true;
            //Session["Web"] = FileWebPortal.FileBytes;
            //Session["webcount"] = "W";



        }
        catch (Exception ex)
        {
            _common.ErrorLog("SysadmEmployeeDetails-E1", ex.Message.ToString());
        }
    }
    protected void lbtnSave_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (validvalue() == false)
        {
            return;
        }
        Session["Action"] = "S";
        lblConfirmation.Text = "Do you want to Save Employee Details ?";
        mpConfirmation.Show();

    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (Session["Action"].ToString() == "S")
        {
            SaveEmployeeDetails();
        }
        else if (Session["Action"].ToString() == "U")
        {
            SaveEmployeeDetails();
        }
        else if (Session["Action"].ToString() == "D")
        {
            SaveEmployeeDetails();
        }
        else if (Session["Action"].ToString() == "V")
        {
           VerifyEmployeeDetails();
        }
        else if (Session["Action"].ToString() == "P")
        {
            InsertPersonalDetails();
        }
        else if (Session["Action"].ToString() == "C")
        {
            InsertContactDetails();
        }
        else if (Session["Action"].ToString() == "O")
        {
            InsertOfficeDetails();
        }
        else if (Session["Action"].ToString() == "I")
        {
            InsertPhotoDetails();
        }
        else if (Session["Action"].ToString() == "L")
        {
            InsertLicenseDetails();
        }
        else if (Session["Action"].ToString() == "R")
        {
            InsertWeekrestdayDetails();
        }
        else if (Session["Action"].ToString() == "DC")
        {
            InsertDutyTypeDetails();
        }
        else if (Session["Action"].ToString() == "ST")
        {
            InsertServiceStatusDetails();
        }
        
    }


    protected void ddlOffice_SelectedIndexChanged(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        loadPostingofc(ddlOffice.SelectedValue, ddlpostingOfficeUpdate);
    }
    protected void ddlOfficeLvl2_SelectedIndexChanged(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        loadofc(ddlOfficeLvl2.SelectedValue, ddlOffice2);
    }
    protected void lbtnsearch_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (rbtnUpdate.Checked == true)
        {
            loadDraftEmp();
        }
        if (rbtnVerUpdate.Checked == true)
        {
            loadverifyemp();
        }       
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (rbtnUpdate.Checked == true)
        {
            initpnl();
            pnlupdate.Visible = true;
            loadofficelvl(ddlOfficeLvl2);
            loadofc(ddlOfficeLvl2.SelectedValue, ddlOffice2);
            loadDesignation(ddlDepotWiseDesig);
			tbEmloyeeName.Text = "";
			ddlLicenseStatus.SelectedValue = "0";
			loadDraftEmp();
        }
        if (rbtnVerUpdate.Checked == true)
        {
			tbEmloyeeName.Text = "";
			initpnl();
            pnlupdate.Visible = true;
            pnlVerifiedEmp.Visible = true;
            loadofficelvl(ddlOfficeLvl2);
            loadofc(ddlOfficeLvl2.SelectedValue, ddlOffice2);
            loadDesignation(ddlDepotWiseDesig);
			tbEmloyeeName.Text = "";
			ddlLicenseStatus.SelectedValue = "0";
			grvEmployees.Visible = false;
            loadverifyemp();
        }
    }
    protected void lbtnEmpReports_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Errormsg("comming soon");
    }
    protected void grvEmployees_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        CsrfTokenValidate();
        try
        {
            if (e.CommandName == "UpdateEmployee")
            {
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                int i = row.RowIndex;
                tbFirstName.Text = grvEmployees.DataKeys[i]["e_fname"].ToString();
                tbMiddleName.Text = grvEmployees.DataKeys[i]["e_mname"].ToString();
                tbLastName.Text = grvEmployees.DataKeys[i]["e_lname"].ToString();
                tbDob.Text = grvEmployees.DataKeys[i]["e_dob"].ToString();
                tbFatherName.Text = grvEmployees.DataKeys[i]["e_fathername"].ToString();
                ddlGender.SelectedValue = grvEmployees.DataKeys[i]["e_gender"].ToString();
                ddlBloodGroup.SelectedValue = grvEmployees.DataKeys[i]["e_blood_group_code"].ToString();
                tbEmail.Text = grvEmployees.DataKeys[i]["e_email_id"].ToString();
                tbMobile.Text = grvEmployees.DataKeys[i]["e_mobile_number"].ToString();
                tbLandline.Text = grvEmployees.DataKeys[i]["e_landline"].ToString();
                tbEmergency.Text = grvEmployees.DataKeys[i]["e_emergency_number"].ToString();
                tbAddress.Text = grvEmployees.DataKeys[i]["e_address"].ToString();
                tbPinCode.Text = grvEmployees.DataKeys[i]["e_pin_code"].ToString();
                tbCity.Text = grvEmployees.DataKeys[i]["e_city"].ToString();
                tbDateOfJoining.Text = grvEmployees.DataKeys[i]["e_date_of_joining"].ToString();
                tbDOJOffice.Text = grvEmployees.DataKeys[i]["e_date_of_assigned_depot"].ToString();
                ddlEmployeetype.SelectedValue = grvEmployees.DataKeys[i]["e_emp_type"].ToString();
                loadDesignation(ddldesignation);
                ddldesignation.SelectedValue = grvEmployees.DataKeys[i]["e_designation_code"].ToString();
                ddlWeeklyRestDay.SelectedValue = grvEmployees.DataKeys[i]["e_weekrestday"].ToString();
                loadState(ddlState);
                ddlState.SelectedValue = grvEmployees.DataKeys[i]["e_state_code"].ToString();
                loadofficelvl(ddlOfficeLevel);
                ddlOfficeLevel.SelectedValue = grvEmployees.DataKeys[i]["e_ofclvl_id"].ToString();
                loadofc(ddlOfficeLevel.SelectedValue, ddlOfficeType);
                ddlOfficeType.SelectedValue = grvEmployees.DataKeys[i]["e_officeid"].ToString();
                loadPostingofc(ddlOfficeType.SelectedValue, ddlPostingoffice);
                ddlPostingoffice.SelectedValue = grvEmployees.DataKeys[i]["e_posting_ofc"].ToString();
                tbDOPoffice.Text= grvEmployees.DataKeys[i]["e_date_of_posting"].ToString();
                
                ddlEmpClass.SelectedValue= grvEmployees.DataKeys[i]["e_empclass"].ToString();
                
                ddlEmpdutyType.SelectedValue = grvEmployees.DataKeys[i]["e_dutytype"].ToString();

                if (grvEmployees.DataKeys[i]["e_photo"].ToString() == "")
                {
                    imgPhoto.Visible = false;
                }
                else
                {
                    Session["PhotoImg"] = grvEmployees.DataKeys[i]["e_photo"];
                    // imgPhoto.ImageUrl = GetImage( grvEmployees.DataKeys[i]["e_photo"].ToString());
                    Byte[] imgbytes = (Byte[])Session["PhotoImg"];
                    imgPhoto.ImageUrl= GetImage(imgbytes);
                    imgPhoto.Visible = true;




                    //Session["Web"] = gvPmtgatewaystatus.DataKeys[row.RowIndex]["img_web"];
                    //Byte[] imgbytes = (Byte[])Session["Web"];
                    //ImgWebPortal.ImageUrl = GetImage(imgbytes);
                }
            
                


                initpnl();
                pnlAddNewEmployee.Visible = true;
                lbtnUpdate.Visible = true;
                lbtnSave.Visible = false;
                Session["Action"] = "U";
                Session["Empid"] = grvEmployees.DataKeys[i]["e_id"].ToString();
            }
            if (e.CommandName == "VerifyEmployee")
            {
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                int i = row.RowIndex;
                Session["Action"] = "V";
                Session["Empid"] = grvEmployees.DataKeys[i]["e_empcode"].ToString();
                lblConfirmation.Text = "Do you want to Verify Employee Details ?";
                mpConfirmation.Show();
            }
            if (e.CommandName == "DeleteEmployee")
            {
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                int i = row.RowIndex;
                Session["Action"] = "D";
                Session["Empid"] = grvEmployees.DataKeys[i]["e_id"].ToString();

                if (grvEmployees.DataKeys[i]["e_photo"].ToString() == "")
                {
                    imgPhoto.Visible = false;
                }
                else
                {
                    Session["PhotoImg"] = grvEmployees.DataKeys[i]["e_photo"];
                    // imgPhoto.ImageUrl = GetImage( grvEmployees.DataKeys[i]["e_photo"].ToString());
                    Byte[] imgbytes = (Byte[])Session["PhotoImg"];
                    imgPhoto.ImageUrl = GetImage(imgbytes);
                    imgPhoto.Visible = true;




                    //Session["Web"] = gvPmtgatewaystatus.DataKeys[row.RowIndex]["img_web"];
                    //Byte[] imgbytes = (Byte[])Session["Web"];
                    //ImgWebPortal.ImageUrl = GetImage(imgbytes);
                }

                lblConfirmation.Text = "Do you want to Delete Employee Details ?";

                mpConfirmation.Show();

            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("Sysadmservicetype-E1", ex.Message.ToString());
            return;
        }
    }
    protected void lbtnUpdate_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (validvalue() == false)
        {
            return;
        }
        Session["Action"] = "U";
        lblConfirmation.Text = "Do you want to Update Employee Details ?";
        mpConfirmation.Show();
    }
    protected void lbtnRest_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        resetcntrl();
    }
    protected void gvVerifiedEmployees_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        CsrfTokenValidate();
        if (e.CommandName == "UpdatePersonal")
        {
            initemppnl();
            pnlupdate.Visible = false;
            pnlVerifiedEmp.Visible = false;
            pnlButton.Visible = false;        
            pnlPersonal.Visible = true;
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int i = row.RowIndex;
            tbEmployeeCode.Text= gvVerifiedEmployees.DataKeys[i]["e_code"].ToString();
            tbEmployeeCode.ReadOnly = true;
            Session["Empid"]= gvVerifiedEmployees.DataKeys[i]["e_code"].ToString();
            tbFirstNameUpdate.Text = gvVerifiedEmployees.DataKeys[i]["e_fname"].ToString();
            tbMiddleNameUpdate.Text = gvVerifiedEmployees.DataKeys[i]["e_mname"].ToString();
            tbLastNameUpdate.Text = gvVerifiedEmployees.DataKeys[i]["e_lname"].ToString();
            tbFatherNameUpdate.Text = gvVerifiedEmployees.DataKeys[i]["e_fathername"].ToString();
            ddlGenderUpdate.SelectedValue = gvVerifiedEmployees.DataKeys[i]["e_gender"].ToString();
            loadbloodgroup(ddlBloodGroupUpdate);
            ddlBloodGroupUpdate.SelectedValue= gvVerifiedEmployees.DataKeys[i]["e_blood_group_code"].ToString();
            tbDobUpdate.Text = gvVerifiedEmployees.DataKeys[i]["e_dob"].ToString();
        }
        if (e.CommandName == "UpdateContact")
        {
            initemppnl();
            pnlupdate.Visible = false;
            pnlVerifiedEmp.Visible = false;
            pnlButton.Visible = false;
            pnlContact.Visible = true;
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int i = row.RowIndex;
            Session["Empid"] = gvVerifiedEmployees.DataKeys[i]["e_code"].ToString();
            tbEmailUpdate.Text = gvVerifiedEmployees.DataKeys[i]["e_email_id"].ToString();
            tbMobileNumberUpdate.Text = gvVerifiedEmployees.DataKeys[i]["e_mobile_number"].ToString();
            tbLandlineNumberUpdate.Text = gvVerifiedEmployees.DataKeys[i]["e_landline"].ToString(); ;
            tbEmergencyNoUpdate.Text= gvVerifiedEmployees.DataKeys[i]["e_emergency_number"].ToString();
            tbAddressUpdate.Text = gvVerifiedEmployees.DataKeys[i]["e_address"].ToString();
            tbPinCodeUpdate.Text = gvVerifiedEmployees.DataKeys[i]["e_pin_code"].ToString();
            tbCityUpdate.Text = gvVerifiedEmployees.DataKeys[i]["e_city"].ToString(); ;
            loadState(ddlStateUpdate);
            ddlStateUpdate.SelectedValue = gvVerifiedEmployees.DataKeys[i]["e_state_code"].ToString();
        }
        if (e.CommandName == "UpdateOfficial")
        {
            initemppnl();
            pnlupdate.Visible = false;
            pnlVerifiedEmp.Visible = false;
            pnlButton.Visible = false;
            pnlOfficeDetails.Visible = true;
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int i = row.RowIndex;
            Session["Empid"] = gvVerifiedEmployees.DataKeys[i]["e_code"].ToString();
            loadofficelvl(ddlOfficeLevelUpdate);
            ddlOfficeLevelUpdate.SelectedValue= gvVerifiedEmployees.DataKeys[i]["e_ofclvl_id"].ToString();
            loadofc(ddlOfficeLevelUpdate.SelectedValue, ddlOffice);
            ddlOffice.SelectedValue = gvVerifiedEmployees.DataKeys[i]["e_officeid"].ToString();
            tbDoJupdate.Text= gvVerifiedEmployees.DataKeys[i]["e_date_of_joining"].ToString();
            tbDoJupdateOf.Text = gvVerifiedEmployees.DataKeys[i]["e_date_of_assigned_depot"].ToString();
            ddlEmployeeTypeUpdate.SelectedValue = gvVerifiedEmployees.DataKeys[i]["e_emp_type"].ToString();
            loadPostingofc(ddlOffice.SelectedValue, ddlpostingOfficeUpdate);
            ddlpostingOfficeUpdate.SelectedValue= gvVerifiedEmployees.DataKeys[i]["e_posting_ofc"].ToString();
            tbPostingDateUpdate.Text= gvVerifiedEmployees.DataKeys[i]["e_date_of_posting"].ToString();

            loadDesignation(ddlDesignationUpdate);
            ddlDesignationUpdate.SelectedValue= gvVerifiedEmployees.DataKeys[i]["e_designation_code"].ToString();
        }
        if (e.CommandName == "UpdatePhoto")
        {
            initemppnl();
            pnlupdate.Visible = false;
            pnlVerifiedEmp.Visible = false;
            pnlButton.Visible = false;
            pnlUpdatePhoto.Visible = true;
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int i = row.RowIndex;
            Session["Empid"] = gvVerifiedEmployees.DataKeys[i]["e_code"].ToString();
            if (!DBNull.Value.Equals(gvVerifiedEmployees.DataKeys[i]["e_photo"]))
            {
                imgImageUpdate.ImageUrl = GetImage(gvVerifiedEmployees.DataKeys[i]["e_photo"]);
                imgImageUpdate.Visible = true;
                Session["PhotoImgUpdate"] = gvVerifiedEmployees.DataKeys[i]["e_photo"];
            }
            else
            {
                imgImageUpdate.Visible = false;
                Session["PhotoImgUpdate"] = null;
            }
           

        }
        if (e.CommandName == "UpdateLicense")
        {
            initemppnl();
            pnlupdate.Visible = false;
            pnlVerifiedEmp.Visible = false;
            pnlButton.Visible = false;
            pnlLicense.Visible = true;
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int i = row.RowIndex;
            Session["Empid"] = gvVerifiedEmployees.DataKeys[i]["e_code"].ToString();
            tbLicenseNo.Text= gvVerifiedEmployees.DataKeys[i]["e_licenseno"].ToString();
            tbDateOfLicense.Text = gvVerifiedEmployees.DataKeys[i]["e_licensedate"].ToString();

        }
        if (e.CommandName == "UpdateWeeklyRest")
        {
            initemppnl();
            pnlupdate.Visible = false;
            pnlVerifiedEmp.Visible = false;
            pnlButton.Visible = false;
            pnlWeeklyRest.Visible = true;
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int i = row.RowIndex;
            Session["Empid"] = gvVerifiedEmployees.DataKeys[i]["e_code"].ToString();
            ddlweekrestdays.SelectedValue= gvVerifiedEmployees.DataKeys[i]["e_weekrestday"].ToString();
        }
        if (e.CommandName == "updateDutyType")
        {
            initemppnl();
            pnlupdate.Visible = false;
            pnlVerifiedEmp.Visible = false;
            pnlButton.Visible = false;
            pnlDutyType.Visible = true;
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int i = row.RowIndex;
            Session["Empid"] = gvVerifiedEmployees.DataKeys[i]["e_code"].ToString();
            loadEmpClass(ddlempclassupdate);
            loadEmpDutyType(ddldutytypeupdate);
            ddldutytypeupdate.SelectedValue = gvVerifiedEmployees.DataKeys[i]["e_dutytype"].ToString();
            ddlempclassupdate.SelectedValue = gvVerifiedEmployees.DataKeys[i]["e_empclass"].ToString();

        }
        if (e.CommandName == "UpdateServiceStatus")
        {
            initemppnl();
            pnlupdate.Visible = false;
            pnlVerifiedEmp.Visible = false;
            pnlButton.Visible = false;
            pnlServiceStatus.Visible = true;
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int i = row.RowIndex;
            Session["Empid"] = gvVerifiedEmployees.DataKeys[i]["e_code"].ToString();
            loadServiceStatus(ddlservicestatusUpdate);
            string sstatus= gvVerifiedEmployees.DataKeys[i]["e_service_status"].ToString();
            ddlservicestatusUpdate.SelectedValue = sstatus;
            if (sstatus == "2" || sstatus == "3")
            {
                ddlservicestatusUpdate.Enabled = false;
                tbofficeorderUpdate.Enabled = false;
                tbOrderDateUpdate.Enabled = false;
                tbremarkupdate.Enabled = false;
                lbtnServicestatusUpdate.Enabled = false;
            }
            else
            {
                ddlservicestatusUpdate.Enabled = true;
                tbofficeorderUpdate.Enabled = true;
                tbOrderDateUpdate.Enabled = true;
                tbremarkupdate.Enabled = true;
                lbtnServicestatusUpdate.Enabled = true;
            }
            //ddlempclassupdate.SelectedValue = gvVerifiedEmployees.DataKeys[i]["e_empclass"].ToString();

        }
    }
    protected void lbtnPdraft_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (validvaluePersonalUpdate() == false)
        {
            return;
        }
        Session["Action"] = "P";
        Session["SaveType"] = "D";
        lblConfirmation.Text = "Do you want to Update Employee Personal Details as a Draft ?";
        mpConfirmation.Show();
    }
    protected void lbtnPlock_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (validvaluePersonalUpdate() == false)
        {
            return;
        }
        Session["Action"] = "P";
        Session["SaveType"] = "L";
        lblConfirmation.Text = "Do you want to Lock Employee Personal Details ?";
        mpConfirmation.Show();
    }
    protected void lbtnCdraft_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (validvalueContactUpdate() == false)
        {
            return;
        }
        Session["Action"] = "C";
        Session["SaveType"] = "D";
        lblConfirmation.Text = "Do you want to Update Employee Contact Details as a Draft ?";
        mpConfirmation.Show();
    }
    protected void lbtnClock_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (validvalueContactUpdate() == false)
        {
            return;
        }
        Session["Action"] = "C";
        Session["SaveType"] = "L";
        lblConfirmation.Text = "Do you want to Lock Employee Contact Details ?";
        mpConfirmation.Show();
    }
    protected void lbtnOdraft_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (validvalueOfficialUpdate() == false)
        {
            return;
        }
        Session["Action"] = "O";
        Session["SaveType"] = "D";
        lblConfirmation.Text = "Do you want to Update Employee Office Details as a Draft ?";
        mpConfirmation.Show();
       
    }
    protected void lbtnOlock_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (validvalueOfficialUpdate() == false)
        {
            return;
        }
        Session["Action"] = "O";
        Session["SaveType"] = "L";
        lblConfirmation.Text = "Do you want to Lock Employee Office Details ?";
        mpConfirmation.Show();
    }
    protected void btnImageUpdate_Click(object sender, EventArgs e)//E2
    {
        CsrfTokenValidate();
        try
        {
            if (!fuImageUpdate.HasFile)
            {
                Errormsg("Please select report first");
                return;
            }
            if (!checkFileExtention(fuImageUpdate))
            {
                Errormsg("File must be png, jpg, jpeg type");
                return;
            }
            string _fileFormat = GetMimeDataOfFile(fuImageUpdate.PostedFile);
            if ((_fileFormat == "image/png") || (_fileFormat == "image/jpeg"))
            {
            }
            else
            {
                Errormsg("File must png, jpg, jpeg type");
                return;
            }
            decimal size = Math.Round((Convert.ToDecimal(fuImageUpdate.PostedFile.ContentLength) / Convert.ToDecimal(1024)), 2);
            if (size > 100 || size < 10)
            {
                Errormsg("File size must be between 10 kb to 100 kb");
                return;
            }
            byte[] PhotoImage = null;
            PhotoImage = convertByteFile(fuImageUpdate);
            imgImageUpdate.ImageUrl = GetImage(PhotoImage);
            imgImageUpdate.Visible = true;
            Session["PhotoImgUpdate"] = PhotoImage;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("SysadmEmployeeDetails-E2", ex.Message.ToString());
        }
    }
    protected void lbtnIdraft_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (validvaluePhotoUpdate() == false)
        {
            return;
        }
        Session["Action"] = "I";
        Session["SaveType"] = "D";
        lblConfirmation.Text = "Do you want to Update Employee Photo as a Draft ?";
        mpConfirmation.Show();
    }
    protected void lbtnIlock_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (validvaluePhotoUpdate() == false)
        {
            return;
        }
        Session["Action"] = "I";
        Session["SaveType"] = "L";
        lblConfirmation.Text = "Do you want to Lock Employee Photo ?";
        mpConfirmation.Show();
    }
    protected void lbtnLdraft_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (validvalueLicenceUpdate() == false)
        {
            return;
        }
        Session["Action"] = "L";
        Session["SaveType"] = "D";
        lblConfirmation.Text = "Do you want to Update Employee License Details as a Draft ?";
        mpConfirmation.Show();
   }
    protected void lbtnLlock_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (validvalueLicenceUpdate() == false)
        {
            return;
        }
        Session["Action"] = "L";
        Session["SaveType"] = "L";
        lblConfirmation.Text = "Do you want to Lock Employee License Details ?";
        mpConfirmation.Show();
    }
    protected void lbtnRdraft_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (validvalueWeekrestUpdate() == false)
        {
            return;
        }
        Session["Action"] = "R";
        Session["SaveType"] = "D";
        lblConfirmation.Text = "Do you want to Update Employee Week Rest Day as a Draft ?";
        mpConfirmation.Show();
    }
    protected void lbtnRlock_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (validvalueWeekrestUpdate() == false)
        {
            return;
        }
        Session["Action"] = "R";
        Session["SaveType"] = "L";
        lblConfirmation.Text = "Do you want to Lock Employee Week Rest Day ?";
        mpConfirmation.Show();
    }

    protected void ddlOfficeType_SelectedIndexChanged(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        loadPostingofc(ddlOfficeType.SelectedValue, ddlPostingoffice);
    }

    protected void lbtnInfo_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Errormsg("Coming Soon");
    }
    protected void lbtnClose_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        initemppnl();
        //pnlAddNewEmployee.Visible = false;
        //pnlupdate.Visible = false;
        //pnlVerifiedEmp.Visible = true;
        //LabelHeaderUpdate.Visible = false;
        //LabelHeader.Visible = false;
        //lbtnUpdate.Visible = false;
        //lbtnSave.Visible = false;
        //pnlPersonal.Visible = false;
        //pnlContact.Visible = false;
        //pnlOfficeDetails.Visible = false;
        //pnlUpdatePhoto.Visible = false;
        //pnlLicense.Visible = false;
        //pnlWeeklyRest.Visible = false;
        //pnlPostingUnit.Visible = false;
        //pnlButton.Visible = true;
        //pnlEmloyeeList.Visible = false;
    }

	protected void gvVerifiedEmployees_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		gvVerifiedEmployees.PageIndex = e.NewPageIndex;
		loadverifyemp();
	}
    protected void lbtnDlock_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (validvalueDutyTypeUpdate() == false)
        {
            return;
        }
        Session["Action"] = "DC";
        Session["SaveType"] = "L";
        lblConfirmation.Text = "Do you want to Lock Employee Duty Type ?";
        mpConfirmation.Show();
    }
    protected void lbtnServicestatusUpdate_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (validvalueServiceStatusUpdate() == false)
        {
            return;
        }
        Session["Action"] = "ST";
        Session["SaveType"] = "L";
        lblConfirmation.Text = "Do you want to Lock Employee Service Status ?";
        mpConfirmation.Show();
    }
    #endregion
}