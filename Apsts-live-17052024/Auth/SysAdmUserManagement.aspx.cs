using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Npgsql;


public partial class Auth_SysUserManagement : BasePage
{

    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    CommonSMSnEmail comm = new CommonSMSnEmail();
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["_moduleName"] = "User Management";
        //security check
        if (!IsPostBack)
        {
            Random randomclass = new Random();
            Session["rndNoCheck"] = randomclass.Next().ToString();
            hidtoken.Value = Session["rndNoCheck"].ToString();
            if (Session["_RoleCode"].ToString() == "1")
            {
                loadofficelvl(ddlOfficeLevel);
                loadofc(ddlOfficeLevel.SelectedValue, ddlOffice);
                loadDesignation(ddlDesignation);
                loadRole(ddlRole);
                loadViewEmployee();
                loadlockeduser();
                loadDesignation(ddlDesignationLP);
                loadRole(ddlRoleLP);
                loadRoleUser();
            }
            if (Session["_RoleCode"].ToString() == "5")
            {

                loadofficelvl(ddlOfficeLevel);
                ddlOfficeLevel.SelectedValue = "30";
                ddlOfficeLevel.Enabled = false;
                loadofc(ddlOfficeLevel.SelectedValue, ddlOffice);
                ddlOffice.SelectedValue = Session["_LDepotCode"].ToString();
                ddlOffice.Enabled = false;
                loadDesignation(ddlDesignation);
                loadRole(ddlRole);
                loadViewEmployee();
                loadlockeduserDepot();
                loadDesignation(ddlDesignationLP);
                loadRole(ddlRoleLP);
                loadRoleUserdepot();
            }
        }
    }

    #region "Methods"
    private void CsrfTokenValidate()
    {
        if (hidtoken.Value != Session["rndNoCheck"].ToString())
        {
            Response.Redirect("errorpage.aspx");
            return;
        }
    }
    private void loadofficelvl(DropDownList ddllofficeLevel)//M1
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
                _common.ErrorLog("SysUserManagement.aspx-0001", dt.TableName);
            }
            ddllofficeLevel.Items.Insert(0, "All");
            ddllofficeLevel.Items[0].Value = "0";
            ddllofficeLevel.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddllofficeLevel.Items.Insert(0, "All");
            ddllofficeLevel.Items[0].Value = "0";
            ddllofficeLevel.SelectedIndex = 0;
            Errormsg("There is some error" + ex.Message);
            _common.ErrorLog("SysUserManagement.aspx-0002", ex.Message.ToString());
        }
    }
    private void loadofc(string ofclvlid, DropDownList ddlOffice)//M2
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
                _common.ErrorLog("SysUserManagement.aspx-0003", dt.TableName);
            }
            ddlOffice.Items.Insert(0, "All");
            ddlOffice.Items[0].Value = "0";
            ddlOffice.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlOffice.Items.Insert(0, "All");
            ddlOffice.Items[0].Value = "0";
            ddlOffice.SelectedIndex = 0;
            Errormsg("There is some error" + ex.Message);
            _common.ErrorLog("SysUserManagement.aspx-0004", ex.Message.ToString());
        }
    }
    private void loadDesignation(DropDownList ddldesignation)//M3
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
                _common.ErrorLog("SysUserManagement.aspx-0005", dt.TableName);
            }
            ddldesignation.Items.Insert(0, "All");
            ddldesignation.Items[0].Value = "0";
            ddldesignation.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddldesignation.Items.Insert(0, "All");
            ddldesignation.Items[0].Value = "0";
            ddldesignation.SelectedIndex = 0;
            Errormsg("There is some error" + ex.Message);
            _common.ErrorLog("SysUserManagement.aspx-0006", ex.Message.ToString());
        }
    }
    private void loadRole(DropDownList ddlrole)//M4
    {
        try
        {
            ddlrole.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_role");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlrole.DataSource = dt;
                    ddlrole.DataTextField = "role_name";
                    ddlrole.DataValueField = "roleid";
                    ddlrole.DataBind();
                }
            }
            else
            {
                _common.ErrorLog("SysUserManagement.aspx-0007", dt.TableName);
            }
            ddlrole.Items.Insert(0, "All");
            ddlrole.Items[0].Value = "0";
            ddlrole.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlrole.Items.Insert(0, "All");
            ddlrole.Items[0].Value = "0";
            ddlrole.SelectedIndex = 0;
            Errormsg("There is some error" + ex.Message);
            _common.ErrorLog("SysUserManagement.aspx-0008", ex.Message.ToString());
        }
    }
    protected void loadViewEmployee()//M5
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_viewemployee");
            MyCommand.Parameters.AddWithValue("p_ofclvlid", Convert.ToInt16(ddlOfficeLevel.SelectedValue.ToString()));
            MyCommand.Parameters.AddWithValue("p_ofcid", ddlOffice.SelectedValue.ToString());
            MyCommand.Parameters.AddWithValue("p_designation", Convert.ToInt16(ddlDesignation.SelectedValue.ToString()));
            MyCommand.Parameters.AddWithValue("p_rolecode", Convert.ToInt16(ddlRole.SelectedValue.ToString()));
            MyCommand.Parameters.AddWithValue("p_employeecode", tbSearchEmployee.Text.ToString());
            dt = bll.SelectAll(MyCommand);
            DataRow[] drow = dt.Select("e_code<>'"+ Session["_UserCode"].ToString() + "'");
            if (dt.TableName == "Success")
            {
                if (drow.Length > 0)
                {
                    lblTotEmployees.Text = "Total Employees " + drow.Length;
                    gvEmployee.DataSource = drow.CopyToDataTable();
                    gvEmployee.DataBind();
                    gvEmployee.Visible = true;
                    lblNoEmpData.Visible = false;
                }
                else
                {
                    gvEmployee.Visible = false;
                    lblNoEmpData.Visible = true;
                }
            }
            else
            {
                _common.ErrorLog("SysUserManagement.aspx-0009", dt.TableName);
            }
        }
        catch (Exception ex)
        {
            gvEmployee.Visible = false;
            lblNoEmpData.Visible = true;
            Errormsg("There is some error" + ex.Message);
            _common.ErrorLog("SysUserManagement.aspx-0010", ex.Message.ToString());
        }
    }
    private void resetcntrl()
    {
        if (Session["_RoleCode"].ToString() == "5")
        {
            loadofficelvl(ddlOfficeLevel);
            ddlOfficeLevel.SelectedValue = "30";
            ddlOfficeLevel.Enabled = false;
            loadofc(ddlOfficeLevel.SelectedValue, ddlOffice);
            ddlOffice.SelectedValue = Session["_LDepotCode"].ToString();
            ddlOffice.Enabled = false;
        }
        if (Session["_RoleCode"].ToString() == "1")
        {
            loadofficelvl(ddlOfficeLevel);
            loadofc(ddlOfficeLevel.SelectedValue, ddlOffice);
        }
        loadDesignation(ddlDesignation);
        loadRole(ddlRole);
        tbSearchEmployee.Text = "";
    }
    private void ExportGridToExcel()//M6
    {
        try
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=EmployeeList.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                // To Export all pages
                gvEmployee.AllowPaging = false;
                // this.SearchEmployee();

                gvEmployee.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in gvEmployee.HeaderRow.Cells)
                    cell.BackColor = gvEmployee.HeaderStyle.BackColor;
                foreach (GridViewRow row in gvEmployee.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                            cell.BackColor = gvEmployee.AlternatingRowStyle.BackColor;
                        else
                            cell.BackColor = gvEmployee.RowStyle.BackColor;
                        cell.CssClass = "textmode";
                    }
                }

                gvEmployee.RenderControl(hw);
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
            _common.ErrorLog("SysAdmUserManagement-M6", ex.Message.ToString());
        }
    }
    private void loadlockeduser()//M7
    {
        try
        {

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_lockeduser");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    lbtnUnlockAllUser.Visible = true;
                    lbtnUnlockAllUser.Visible = false;
                    gvLockedUser.DataSource = dt;
                    gvLockedUser.DataBind();
                    gvLockedUser.Visible = true;
                    lblLockedUserNodate.Visible = false;
                }
                else
                {
                    lbtnUnlockAllUser.Visible = false;
                    gvLockedUser.Visible = false;
                    lblLockedUserNodate.Visible = true;
                }
            }
            else
            {
                _common.ErrorLog("SysUserManagement.aspx-0011", dt.TableName);
                gvLockedUser.Visible = false;
                lblLockedUserNodate.Visible = true;
            }
        }
        catch (Exception ex)
        {
            gvLockedUser.Visible = false;
            lblLockedUserNodate.Visible = true;
            Errormsg("There is some error" + ex.Message);
            _common.ErrorLog("SysUserManagement.aspx-0012", ex.Message.ToString());
        }
    }
    private void UnlockUser(string usercode)//M8
    {
        try
        {
            string ipaddress = HttpContext.Current.Request.UserHostAddress;
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            string Mresult = "";
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_unlock_user");
            MyCommand.Parameters.AddWithValue("p_usercode", usercode);
            MyCommand.Parameters.AddWithValue("p_updateby", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("p_ipaddress", ipaddress);
            Mresult = bll.UpdateAll(MyCommand);
            if (Mresult == "Success")
            {
                loadlockeduser();
                Successmsg("User Unlock successfully");
            }
            else
            {
                _common.ErrorLog("SysUserManagement.aspx-0013", Mresult);
            }
        }
        catch (Exception ex)
        {
            Errormsg("There is some error" + ex.Message);
            _common.ErrorLog("SysUserManagement.aspx-0014", ex.Message.ToString());
        }

    }
    protected void loadRoleUser()//M9
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_rolewiseuser");
            MyCommand.Parameters.AddWithValue("p_designationcode", Convert.ToInt32(ddlDesignationLP.SelectedValue.ToString()));
            MyCommand.Parameters.AddWithValue("p_rolecode", Convert.ToInt32(ddlRoleLP.SelectedValue.ToString()));
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {

                    lbtnRoleWiseUserPrintAll.Visible = false;
                    gvRolewiseUser.DataSource = dt;
                    gvRolewiseUser.DataBind();
                    gvRolewiseUser.Visible = true;
                    lblRolewiseUserNodata.Visible = false;
                }
                else
                {
                    lbtnRoleWiseUserPrintAll.Visible = false;
                    gvRolewiseUser.Visible = false;
                    lblRolewiseUserNodata.Visible = true;
                }
            }
            else
            {
                _common.ErrorLog("SysUserManagement.aspx-0015", dt.TableName);
            }
        }
        catch (Exception ex)
        {
            gvRolewiseUser.Visible = false;
            lblRolewiseUserNodata.Visible = true;
            Errormsg(ex.Message.ToString());
            Errormsg("There is some error" + ex.Message);
            _common.ErrorLog("SysUserManagement.aspx-0016", ex.Message.ToString());
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
    private void changestatus()//M10
    {
        try
        {
            string ipaddress = HttpContext.Current.Request.UserHostAddress;
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            string Mresult = "";
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_chnage_userstatus");
            MyCommand.Parameters.AddWithValue("p_status", Session["status"].ToString());
            MyCommand.Parameters.AddWithValue("p_usercode", Session["usercode"].ToString());
            MyCommand.Parameters.AddWithValue("p_userpwd", "");
            MyCommand.Parameters.AddWithValue("p_updateby", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("p_ipaddress", ipaddress);
            Mresult = bll.UpdateAll(MyCommand);
            if (Mresult == "Success")
            {
                loadViewEmployee();
                Successmsg("User Status successfully Updated");
            }
            else
            {
                _common.ErrorLog("SysUserManagement.aspx-0017", Mresult);
            }
        }
        catch (Exception ex)
        {
            Errormsg("There is some error" + ex.Message);
            _common.ErrorLog("SysUserManagement.aspx-0018", ex.Message.ToString());
        }

    }
    private void changePwd()//M11
    {
        try
        {
            string ipaddress = HttpContext.Current.Request.UserHostAddress;
            string PasswordForEmp = RandomString(8);
            string PASSWORD = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(PasswordForEmp, "SHA1");
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            string Mresult = "";
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_chnage_userstatus");
            MyCommand.Parameters.AddWithValue("p_status", Session["status"].ToString());
            MyCommand.Parameters.AddWithValue("p_usercode", Session["usercode"].ToString());
            MyCommand.Parameters.AddWithValue("p_userpwd", PASSWORD);
            MyCommand.Parameters.AddWithValue("p_updateby", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("p_ipaddress", ipaddress);
            Mresult = bll.UpdateAll(MyCommand);
            if (Mresult == "Success")
            {
                loadViewEmployee();
                comm.ChngePwdbyadmin(Session["usercode"].ToString(), PasswordForEmp, Session["usermobile"].ToString());
                Successmsg("User Password successfully Changed");
            }
            else
            {
                _common.ErrorLog("SysUserManagement.aspx-0019", Mresult);
            }
        }
        catch (Exception ex)
        {
            Errormsg("There is some error" + ex.Message);
            _common.ErrorLog("SysUserManagement.aspx-0020", ex.Message.ToString());
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

        return random;
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

    //private void AssignRole(int newrole, string attacheddepot)//M13
    //{
    //    try
    //    {
    //        string ipaddress = HttpContext.Current.Request.UserHostAddress;
    //        NpgsqlCommand MyCommand = new NpgsqlCommand();
    //        MyCommand.Parameters.Clear();
    //        string Mresult = "";
    //        MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_assignrole");
    //        MyCommand.Parameters.AddWithValue("p_newrole", newrole);
    //        MyCommand.Parameters.AddWithValue("p_attacheddepot", attacheddepot);
    //        MyCommand.Parameters.AddWithValue("p_empcode", Session["usercode"]);
    //        MyCommand.Parameters.AddWithValue("p_updatedby", Session["_UserCode"].ToString());
    //        MyCommand.Parameters.AddWithValue("p_ipaddress", ipaddress);
    //        Mresult = bll.UpdateAll(MyCommand);
    //        if (Mresult == "Success")
    //        {
    //            Successmsg("Employee SuccessFully Attached");
    //            loadViewEmployee();
    //            loadRoleUser();
    //            pnluserlist.Visible = true;
    //            pnlroleassignment.Visible = false;
    //        }
    //        else
    //        {
    //            _common.ErrorLog("SysAdmUserManagement-M13", Mresult);
    //            Errormsg(Mresult);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        _common.ErrorLog("SysAdmUserManagement-M13", ex.Message.ToString());
    //        Errormsg(ex.Message);
    //    }
    //}

    private void loadWorkshop(string usercode, DropDownList dropDownList)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            dropDownList.Items.Clear();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_getofcworkshops");
            MyCommand.Parameters.AddWithValue("p_empcode", usercode);
            DataTable dt = new DataTable();
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                dropDownList.Visible = true;
                dropDownList.DataSource = dt;
                dropDownList.DataTextField = "officename";
                dropDownList.DataValueField = "officeid";
                dropDownList.DataBind();
            }
            else
            {
                Errormsg("Employee not attached to any workshop. Please mark employee posting office as workshop");
            }
            dropDownList.Items.Insert(0, "Select Workshop");
            dropDownList.Items[0].Value = "0";
            dropDownList.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            dropDownList.Items.Insert(0, "Select Workshop");
            dropDownList.Items[0].Value = "0";
            dropDownList.SelectedIndex = 0;
            Errormsg("There is some error" + ex.Message);
            _common.ErrorLog("SysUserManagement.aspx-0021", ex.Message.ToString());
        }
    }
    private void loadUsers(string role, string action)//M15
    {
        try
        {
            object obj = role;
            int rr = Convert.ToInt32(obj);
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_rolewiseuserdetails");
            MyCommand.Parameters.AddWithValue("p_rolecode", rr);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    gvUserByRoleLP.DataSource = dt;
                    gvUserByRoleLP.DataBind();
                    gvUserByRoleLP.Visible = true;
                    mpRoleWiseUserLP.Show();
                }
            }
            else
            {
                _common.ErrorLog("SysUserManagement.aspx-0022", dt.TableName);
            }
        }
        catch (Exception ex)
        {
            Errormsg("There is some error" + ex.Message);
            _common.ErrorLog("SysUserManagement.aspx-0023", ex.Message.ToString());
        }
    }
    private void loadofclvlwiseRole(DropDownList ddlrole, int ofclvl)//M16
    {
        try
        {
            ddlrole.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_ofclvl_wise_role");
            MyCommand.Parameters.AddWithValue("p_ofclvl", ofclvl);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlrole.DataSource = dt;
                    ddlrole.DataTextField = "role_name";
                    ddlrole.DataValueField = "roleid";
                    ddlrole.DataBind();
                }
            }
            else
            {
                _common.ErrorLog("SysUserManagement.aspx-0024", dt.TableName);
            }
            ddlrole.Items.Insert(0, "All");
            ddlrole.Items[0].Value = "0";
            ddlrole.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlrole.Items.Insert(0, "All");
            ddlrole.Items[0].Value = "0";
            ddlrole.SelectedIndex = 0;
            Errormsg("There is some error" + ex.Message);
            _common.ErrorLog("SysUserManagement.aspx-0025", ex.Message.ToString());
        }
    }
    private void grdassignedrole(string user, string role, GridView grdassignedroleusr)//17
    {
        try
        {
            grdassignedroleusr.Visible = false;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_assignedrole");
            MyCommand.Parameters.AddWithValue("p_empcode", user);
            MyCommand.Parameters.AddWithValue("p_rolecode", Convert.ToInt32(role));
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    grdassignedroleusr.Visible = true;
                    pnlNorecord.Visible = false;
                    grdassignedroleusr.DataSource = dt;
                    grdassignedroleusr.DataBind();
                }
            }
            else
            {
                grdassignedroleusr.Visible = false;
                pnlNorecord.Visible = true;
                _common.ErrorLog("SysUserManagement.aspx-0026", dt.TableName);
            }
        }
        catch (Exception ex)
        {
            grdassignedroleusr.Visible = false;
            pnlNorecord.Visible = true;
            Errormsg("There is some error" + ex.Message);
            _common.ErrorLog("SysUserManagement.aspx-0027", ex.Message.ToString());
        }
    }

    private void checkRoleAssignYN(string user, string role)//M18
    {
        try
        {
            lblmsg.Text = "";
            lblmsg.Visible = false;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_check_assignedrole");
            MyCommand.Parameters.AddWithValue("p_empcode", user);
            MyCommand.Parameters.AddWithValue("p_rolecode", Convert.ToInt32(role));
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["msg"].ToString() != "0")
                    {

                        lblmsg.Visible = true;
                        ddlassignedRole.SelectedValue = hdn_currRole.Value;
                        lblmsg.Text = "<br/>(" + dt.Rows[0]["msg"].ToString() + ")";
                    }
                }
            }
            else
            {
                _common.ErrorLog("SysUserManagement.aspx-0028", dt.TableName);
            }
        }
        catch (Exception ex)
        {
            Errormsg("There is some error" + ex.Message);
            _common.ErrorLog("SysUserManagement.aspx-0029", ex.Message.ToString());
        }
    }
    private bool AssignRole()//19
    {
        try
        {
            string ipaddress = HttpContext.Current.Request.UserHostAddress;
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            string Mresult = "";
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_assignrole");
            MyCommand.Parameters.AddWithValue("p_newrole", Convert.ToInt32(ddlassignedRole.SelectedValue.ToString()));
            MyCommand.Parameters.AddWithValue("p_empcode", Session["USER_CODE"].ToString());
            MyCommand.Parameters.AddWithValue("p_updatedby", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("p_ipaddress", ipaddress);
            Mresult = bll.UpdateAll(MyCommand);
            if (Mresult == "Success")
            {

                return true;
            }
            else
            {
                _common.ErrorLog("SysUserManagement.aspx-0030", "Something Went Wrong");
                return false;
            }

        }
        catch (Exception ex)
        {
            Errormsg("There is some error" + ex.Message);
            _common.ErrorLog("SysUserManagement.aspx-0031", ex.Message.ToString());
            return false;
        }
    }

    private void loadlockeduserDepot()
    {
        try
        {

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_depot_lockeduser");
            MyCommand.Parameters.AddWithValue("p_depot", ddlOffice.SelectedValue);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    lbtnUnlockAllUser.Visible = true;
                    lbtnUnlockAllUser.Visible = false;
                    gvLockedUser.DataSource = dt;
                    gvLockedUser.DataBind();
                    gvLockedUser.Visible = true;
                    lblLockedUserNodate.Visible = false;
                }
                else
                {
                    lbtnUnlockAllUser.Visible = false;
                    gvLockedUser.Visible = false;
                    lblLockedUserNodate.Visible = true;
                }
            }
            else
            {
                _common.ErrorLog("SysUserManagement.aspx-0032", dt.TableName);
                gvLockedUser.Visible = false;
                lblLockedUserNodate.Visible = true;
            }
        }
        catch (Exception ex)
        {
            gvLockedUser.Visible = false;
            lblLockedUserNodate.Visible = true;
            Errormsg("There is some error" + ex.Message);
            _common.ErrorLog("SysUserManagement.aspx-0033", ex.Message.ToString());
        }
    }
    protected void loadRoleUserdepot()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_depot_rolewiseuser");
            MyCommand.Parameters.AddWithValue("p_designationcode", Convert.ToInt32(ddlDesignationLP.SelectedValue.ToString()));
            MyCommand.Parameters.AddWithValue("p_rolecode", Convert.ToInt32(ddlRoleLP.SelectedValue.ToString()));
            MyCommand.Parameters.AddWithValue("p_depotcode", ddlOffice.SelectedValue);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {

                if (dt.Rows.Count > 0)
                {

                    lbtnRoleWiseUserPrintAll.Visible = false;
                    gvRolewiseUser.DataSource = dt;
                    gvRolewiseUser.DataBind();
                    gvRolewiseUser.Visible = true;
                    lblRolewiseUserNodata.Visible = false;
                }
                else
                {
                    lbtnRoleWiseUserPrintAll.Visible = false;
                    gvRolewiseUser.Visible = false;
                    lblRolewiseUserNodata.Visible = true;
                }
            }
            else
            {
                _common.ErrorLog("SysUserManagement.aspx-0034", dt.TableName);
            }
        }
        catch (Exception ex)
        {
            gvRolewiseUser.Visible = false;
            lblRolewiseUserNodata.Visible = true;
            Errormsg(ex.Message.ToString());
            Errormsg("There is some error" + ex.Message);
            _common.ErrorLog("SysUserManagement.aspx-0035", ex.Message.ToString());
        }
    }
    private void loadUsersdepot(string role, string action)//M15
    {
        try
        {
            object obj = role;
            int rr = Convert.ToInt32(obj);
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_depot_rolewiseuserdetails");
            MyCommand.Parameters.AddWithValue("p_rolecode", rr);
            MyCommand.Parameters.AddWithValue("p_depotcode", ddlOffice.SelectedValue);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    gvUserByRoleLP.DataSource = dt;
                    gvUserByRoleLP.DataBind();
                    gvUserByRoleLP.Visible = true;
                    mpRoleWiseUserLP.Show();
                }
            }
            else
            {
                _common.ErrorLog("SysUserManagement.aspx-0036", dt.TableName);
            }
        }
        catch (Exception ex)
        {
            Errormsg("There is some error" + ex.Message);
            _common.ErrorLog("SysUserManagement.aspx-0037", ex.Message.ToString());
        }
    }
    #endregion

    #region "Events"
    protected void lbtnSearchEmployee_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        loadViewEmployee();
    }
    protected void lbtnLoadAllEmployee_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        resetcntrl();
        loadViewEmployee();
    }
    protected void lbtnDownloadExcel_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Successmsg("Coming Soon");
    }
    protected void gvLockedUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvLockedUser.PageIndex = e.NewPageIndex;
        if (Session["_RoleCode"].ToString() == "1")
        {
            loadlockeduser();
        }
        if (Session["_RoleCode"].ToString() == "5")
        {
            loadlockeduserDepot();
        }
    }
    protected void gvLockedUser_RowCommand(object sender, GridViewCommandEventArgs e)//E1
    {
        CsrfTokenValidate();
        try
        {
            if (e.CommandName == "UnlockUser")
            {
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                int i = row.RowIndex;
                UnlockUser(gvLockedUser.DataKeys[i]["user_code"].ToString());
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("SysAdmUserManagement-M7", ex.Message.ToString());
        }
    }
    protected void lbtnUnlockAllUser_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        UnlockUser("0");
    }
    protected void gvRolewiseUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvRolewiseUser.PageIndex = e.NewPageIndex;
       
        if (Session["_RoleCode"].ToString() == "1")
        {
            loadRoleUser();
        }
        if (Session["_RoleCode"].ToString() == "5")
        {
            loadRoleUserdepot();
        }
    }
    protected void gvRolewiseUser_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        CsrfTokenValidate();
        if (e.CommandName == "RoleWiseAllUser")
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int i = row.RowIndex;
            string role = gvRolewiseUser.DataKeys[i]["role_code"].ToString();
            
            if (Session["_RoleCode"].ToString() == "1")
            {
                loadUsers(role, "View");
            }
            if (Session["_RoleCode"].ToString() == "5")
            {
                loadUsersdepot(role, "View");
            }
        }
        if (e.CommandName == "RoleWisePrint")
        {
            Successmsg("Coming Soon");
        }

    }
    protected void ddlDesignationLP_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["_RoleCode"].ToString() == "1")
        {
            loadRoleUser();
        }
        if (Session["_RoleCode"].ToString() == "5")
        {
            loadRoleUserdepot();
        }
    }
    protected void ddlRoleLP_SelectedIndexChanged(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (Session["_RoleCode"].ToString() == "1")
        {
            loadRoleUser();
        }
        if (Session["_RoleCode"].ToString() == "5")
        {
            loadRoleUserdepot();
        }
    }
    protected void gvEmployee_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvEmployee.PageIndex = e.NewPageIndex;
        loadViewEmployee();
    }
    protected void gvEmployee_RowCommand(object sender, GridViewCommandEventArgs e)//E2
    {
        CsrfTokenValidate();
        try
        {
            if (e.CommandName == "activeyn")
            {
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                int i = row.RowIndex;
                Session["status"] = gvEmployee.DataKeys[i]["e_status"].ToString();
                Session["usercode"] = gvEmployee.DataKeys[i]["e_code"].ToString();
                if (Session["status"].ToString() == "A")
                {
                    lblConfirmation.Text = "Do you want to Deactivate User ?";
                }
                else if (Session["status"].ToString() == "D")
                {
                    lblConfirmation.Text = "Do you want to Activate User ?";
                }
                else if (Session["status"].ToString() == "L")
                {
                    lblConfirmation.Text = "Do you want to Unlock User ?";
                }
                mpConfirmation.Show();

            }
            if (e.CommandName == "changepasswd")
            {
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                int i = row.RowIndex;
                Session["status"] = "P";
                Session["usercode"] = gvEmployee.DataKeys[i]["e_code"].ToString();
                Session["usermobile"] = gvEmployee.DataKeys[i]["e_mobile_number"].ToString();
                lblConfirmation.Text = "Do you want to Change User Password ?";
                mpConfirmation.Show();
            }
            if (e.CommandName == "assignrole")
            {
                lblmsg.Visible = false;
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                int i = row.RowIndex;
                string unitoffice = gvEmployee.DataKeys[i]["unit_office"].ToString();
                string unitofficename = gvEmployee.DataKeys[i]["unitofficename"].ToString();
                string officename = gvEmployee.DataKeys[i]["e_office_name"].ToString();
                if (unitoffice == "")
                {
                    unitoffice = "";
                }
                string rolecode = gvEmployee.DataKeys[i]["rolecode"].ToString();
                string rolename = gvEmployee.DataKeys[i]["e_role"].ToString();
                string empname = gvEmployee.DataKeys[i]["e_empname"].ToString();
                string designationname = gvEmployee.DataKeys[i]["e_designation_name"].ToString();
                Session["Ofc_ID"] = gvEmployee.DataKeys[i]["ofcid"].ToString();
                string usercode = gvEmployee.DataKeys[i]["e_code"].ToString();
                Session["USER_CODE"] = usercode;
                int ofclvlid = Convert.ToInt32(gvEmployee.DataKeys[i]["ofclvlid"].ToString());
                loadofclvlwiseRole(ddlassignedRole, ofclvlid);
                ddlassignedRole.SelectedValue = rolecode;
                ddlassignedRole.Items.RemoveAt(0);
                lblAssignRoleName_Designation.Text = "ASSIGN ROLE TO " + empname + " (" + designationname + ")";
                grdassignedrole(usercode, ddlassignedRole.SelectedValue.ToString(), grdassignedroleusr);
                hdn_currRole.Value = rolecode;
                lblpostingnotice.Visible = true;
                if (unitoffice == "")
                {
                    lblpostingnotice.Text = "(Please Post Employee in Sub Office of  " + gvEmployee.DataKeys[i]["e_office_name"].ToString() + " Office.)";
                    lblpostingnotice.ForeColor = Color.Red;
                    ddlassignedRole.Enabled = false;
                    lnkAssignRole.Enabled = false;
                    pnlassign.Visible = false;
                }
                else
                {
                    lblpostingnotice.Text = "(Your are post in Sub Office " + unitofficename + "(" + unitoffice + ") of  " + officename + " Office.)";
                    lblpostingnotice.ForeColor = Color.Green;
                    pnlassign.Visible = true;
                    ddlassignedRole.Enabled = true;
                    lnkAssignRole.Enabled = true;
                }
                lblCurrRole.Text = "Your Current Role  <b>" + rolename + "</b>";
                hdn_currRole.Value = rolecode;
                mpAssignRole.Show();
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("SysAdmUserManagement-M7", ex.Message.ToString());
            Errormsg("SysAdmUserManagement - M7 " + ex.Message.ToString());
        }
    }
    protected void ddlOfficeLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        loadofc(ddlOfficeLevel.SelectedValue, ddlOffice);
    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (Session["status"].ToString() == "A" || Session["status"].ToString() == "D" || Session["status"].ToString() == "L")
        {
            changestatus();
        }
        if (Session["status"].ToString() == "P")
        {
            changePwd();
        }
        if (Session["status"].ToString() == "R")
        {
            if (AssignRole() == true)
            {
                loadViewEmployee();
                Successmsg("User Role Assigned Successfully");
            }
            //string depotCode = ddlAttachedDepot.SelectedValue.ToString();
            //if (ddlAssignRole.SelectedValue == "10")
            //{
            //    depotCode = ddlAttachedWorkshop.SelectedValue.ToString();
            //}
            //AssignRole(Convert.ToInt32(ddlAssignRole.SelectedValue.ToString()), depotCode.Trim());
        }
    }
    protected void ddlassignedRole_SelectedIndexChanged(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        string user = Session["USER_CODE"].ToString();
        checkRoleAssignYN(user, ddlassignedRole.SelectedValue.ToString());
        grdassignedrole(user, ddlassignedRole.SelectedValue.ToString(), grdassignedroleusr);
        mpAssignRole.Show();
    }
    protected void gvEmployee_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label lblStatus = e.Row.FindControl("lbblstatus") as Label;
            LinkButton btnActive = e.Row.FindControl("lbtnActiveYN") as LinkButton;
            LinkButton btnassign = e.Row.FindControl("lbtnAssignRole") as LinkButton;
            btnassign.Visible = true;
            if (lblStatus.Text == "D")
            {
                btnActive.CssClass = "btn btn-success btn-sm";
                btnActive.ToolTip = "Activate Employee";
            }
            if (lblStatus.Text == "A")
            {
                btnActive.CssClass = "btn btn-danger btn-sm";
                btnActive.ToolTip = "Deactivate Employee";
            }
            if (lblStatus.Text == "L")
            {
                btnActive.CssClass = "btn btn-github btn-sm";
                btnActive.ToolTip = "Unlock Employee";
            }
            //if (Session["_RoleCode"].ToString() == "5")
            //{
            //    btnassign.Visible = false;
            //}
            //if (Session["_RoleCode"].ToString() == "1")
            //{
            //    btnassign.Visible = true;
            //}

        }
    }
    protected void lnkAssignRole_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Session["status"] = "R";
        lblConfirmation.Text = "Do you want to Assign <b>" + ddlassignedRole.SelectedItem.Text + "</b> Role ?";
        mpConfirmation.Show();
    }
    #endregion

}