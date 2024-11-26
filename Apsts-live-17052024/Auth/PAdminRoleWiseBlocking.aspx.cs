using System;
using System.Data;
using Npgsql;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_PAdminRoleWiseBlocking : BasePage 
{
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    private sbCommonFunc _common = new sbCommonFunc();
    sbValidation _validation = new sbValidation();
    NpgsqlCommand MyCommand = new NpgsqlCommand();
    private sbBLL bll = new sbBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Random randomclass = new Random();
            Session["rndNoCheck"] = randomclass.Next().ToString();
            hidtoken.Value = Session["rndNoCheck"].ToString();
            Session["_moduleName"] = "Portal Access Control";
           
            loadBlockedRoles();
            loadRoles();
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
    private void loadRoles()//M1
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_portal_rolewise_getrole");
            DataTable MyTable = bll.SelectAll(MyCommand);
            if (MyTable.TableName == "Success")
            {
                if (MyTable.Rows.Count > 0)
                {
                    ddlUserRole.DataSource = MyTable;
                    ddlUserRole.DataTextField = "rolenam";
                    ddlUserRole.DataValueField = "rolecod";
                    ddlUserRole.DataBind();
                }
            }
            else
            {
                _common.ErrorLog("PAdminRoleWiseBlocking.aspx-0001", MyTable.TableName);
            }

            ddlUserRole.Items.Insert(0, "-Select-");
                ddlUserRole.Items[0].Value = "0";
                ddlUserRole.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminRoleWiseBlocking.aspx-0002", ex.Message.ToString());
            ddlUserRole.Items.Insert(0, "-SELECT-");
            ddlUserRole.Items[0].Value = "0";
            ddlUserRole.SelectedIndex = 0;
        }
    } 
    private void loadBlockedRoles()//M2
    {
        try
        {
            NpgsqlCommand MyCommand = new NpgsqlCommand();

            noBlockedRoles.Visible = false;
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_getblocked_role");
            DataTable MyTable = bll.SelectAll(MyCommand);
            if (MyTable.TableName == "Success")
            {
                if (MyTable.Rows.Count > 0)
                {
                    gvBlockedRoles.DataSource = MyTable;
                    gvBlockedRoles.DataBind();
                    gvBlockedRoles.Visible = true;
                    noBlockedRoles.Visible = false;
                }
                else
                {
                    gvBlockedRoles.Visible = false;
                    noBlockedRoles.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminRoleWiseBlocking.aspx-0002", ex.Message.ToString());
            gvBlockedRoles.Visible = false;
            noBlockedRoles.Visible = true;
        }
    }
    private void loadRoleBlockHistory()// M3
    {
        try
        {
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_getrole_blockhistory");
            DataTable MyTable = bll.SelectAll(MyCommand);
            if (MyTable.TableName == "Success")
            {
                if (MyTable.Rows.Count > 0)
                {
                    gvRoleBlockHistory.DataSource = MyTable;
                    gvRoleBlockHistory.DataBind();
                    pnlNoRecord.Visible = false;
                }
                else {
                    gvRoleBlockHistory.Visible = false;
                    pnlNoRecord.Visible = true;
                }
                  
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminRoleWiseBlocking.aspx-0003", ex.Message.ToString());
            gvRoleBlockHistory.Visible = false;
        }
    }
    private bool validValuesRoleStatus()// M4
    {
        try
        {
            string msg = "";
            int msgcont = 0;
 
            if (_validation.IsValidString(ddlUserRole.SelectedValue, 1, 100) == false | ddlUserRole.SelectedValue == "0")
            {
                msgcont = msgcont + 1;
                msg = msg + msgcont.ToString() + ". Please Select Role.<br>";
               
            }
            if (tbBlockFromDate.Text.ToString() == "")
            {
                msgcont = msgcont + 1;
                msg = msg + msgcont.ToString() + ". Please select from date.<br>";
              
            }
            if (tbBlockToDate.Text.ToString() == "")
            {
                msgcont = msgcont + 1;
                msg = msg + msgcont.ToString() + ". Please select To date.<br>";
            }
  DateTime FDate = DateTime.ParseExact(tbBlockFromDate.Text, "dd/MM/yyyy", null);

            if (FDate < DateTime.Now.Date)
            {
                msgcont = msgcont + 1;
                msg = msg + msgcont.ToString() + ". Invalid From Date.<br>";
            }

          
             DateTime TDate = DateTime.ParseExact(tbBlockToDate.Text, "dd/MM/yyyy", null);

            if (TDate < DateTime.Now.Date)
            {
                msgcont = msgcont + 1;
                msg = msg + msgcont.ToString() + ". Invalid To Date.<br>";
            }

            if (TDate < FDate)
            {
                msgcont = msgcont + 1;
                msg = msg + msgcont.ToString() + ". To Date Cannot Smaller Than From Date.<br>";
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
            _common.ErrorLog("PAdminRoleWiseBlocking.aspx-0004", ex.Message.ToString());
            return false;
        }
       
       
    }
    private bool saveRoleBlockDetail()//M5

    {
        try
        {
            string fromDate = tbBlockFromDate.Text.ToString();
            string toDate = tbBlockToDate.Text.ToString();
            string ipaddress = HttpContext.Current.Request.UserHostAddress;
            string UpdatedBy = Session["_UserCode"].ToString();
            string Action = Session["Action"].ToString();
            int roleid = 0;
            if (Action != "S")
            {
                roleid = Convert.ToInt16( Session["roleid"]);
            }
            else
            {
                roleid = Convert.ToInt16(ddlUserRole.SelectedValue.ToString());

            }
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_insert_roleblock");
            MyCommand.Parameters.AddWithValue("p_action", Action);
            MyCommand.Parameters.AddWithValue("p_roleid", roleid);
            MyCommand.Parameters.AddWithValue("p_fromdate", fromDate);
            MyCommand.Parameters.AddWithValue("p_todate", toDate);
            MyCommand.Parameters.AddWithValue("p_updatedby", UpdatedBy);
            MyCommand.Parameters.AddWithValue("p_ipaddress", ipaddress);
            string Mresult = bll.UpdateAll(MyCommand);      
            if (Mresult == "Success")
            {
                Successmsg("Role has been Successfully Block portal access");
                loadBlockedRoles();
                loadRoles();
                loadRoleBlockHistory();
                ddlUserRole.SelectedValue = "0";
                tbBlockFromDate.Text = "";
                tbBlockToDate.Text = "";
              
                return true;
            }
            else
            {
                _common.ErrorLog("PAdminRoleWiseBlocking.aspx-0005", Mresult);
                Errormsg("Error occurred while Updation. " + Mresult);
                return false;
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminRoleWiseBlocking.aspx-0006", ex.Message.ToString());
            return false;
        }
    }
    private void InfoMsg(string msg)
    {
        string popup = _popup.modalPopupLarge("I", "Information", msg, "Close");
        Response.Write(popup);
    }
    protected void Successmsg(string msg)
    {

        string popup = _popup.modalPopupSmall("S", "Congratulations", msg, "Close");
        Response.Write(popup);
    }
    protected void Errormsg(string msg)
    {

        string popup = _popup.modalPopupSmall("D", "Please Check", msg, "Close");
        Response.Write(popup);
    }
    private void ConfirmMsg(string msg)
    {
        lblConfirmation.Text = msg;
        mpConfirmation.Show();
    }
    #endregion

    #region "Events"
    protected void lbtnViewInstruction_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        string msg = "";
        msg = msg + "1. Access of various User Roles can be Blocked here.<br/>";
        msg = msg + "2. Access of various User Roles can be Unblocked here.<br/>";
       
        InfoMsg(msg);
    }
    protected void lbtnViewHistory_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        mpRoleBlockHistory.Show();
        loadRoleBlockHistory();
    }
    protected void lbtnResetRole_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        ddlUserRole.SelectedValue = "0";
        tbBlockFromDate.Text = "";
        tbBlockToDate.Text = "";
    }
    protected void gvRoleBlockHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        CsrfTokenValidate();
        gvRoleBlockHistory.PageIndex = e.NewPageIndex;
        loadRoleBlockHistory();
        mpRoleBlockHistory.Show();

    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        saveRoleBlockDetail();
    }
    protected void lbtnUpdateRoleStatus_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (validValuesRoleStatus() == false)
        {
            return;
        }
        Session["Action"] = "S";
        ConfirmMsg("Are you want to block portal acces to " +  ddlUserRole.SelectedItem.Text + " role?");
       
    }
    protected void gvBlockedRoles_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        CsrfTokenValidate();
        int index = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "RoleUpdation")
        {
            Session["Action"] = "D";
            Session["roleid"] = Convert.ToInt32(gvBlockedRoles.DataKeys[index].Values["roleid"]);
            ConfirmMsg("you want to portal access has been allowed to " + gvBlockedRoles.DataKeys[index].Values["Role"] + " Role?");
        }
    }
    protected void gvBlockedRoles_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvBlockedRoles.PageIndex = e.NewPageIndex;
        loadBlockedRoles();
    }
    #endregion


}