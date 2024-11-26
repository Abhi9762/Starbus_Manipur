using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_PUsermapping : BasePage
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

        if (!IsPostBack)
        {
            Session["_moduleName"] = "User Module Mapping";

            ddlgroup.Items.Insert(0, "All Group");
            ddlgroup.Items[0].Value = "0";
            ddlgroup.SelectedIndex = 0;
        }
    }

    #region "Methods"
    private void getmodulegroup()
    {
        try
        {
            ddlgroup.Items.Clear();
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.f_get_module_group");
            MyCommand.Parameters.AddWithValue("p_userid", "0");

            DataTable MyTable = bll.SelectAll(MyCommand);
            if (MyTable.TableName == "Success")
            {
                if (MyTable.Rows.Count > 0)
                {
                    ddlgroup.DataSource = MyTable;
                    ddlgroup.DataTextField = "group_name";
                    ddlgroup.DataValueField = "group_id";
                    ddlgroup.DataBind();
                }
            }
            ddlgroup.Items.Insert(0, "All Group");
            ddlgroup.Items[0].Value = "0";
            ddlgroup.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlgroup.Items.Insert(0, "-select-");
            ddlgroup.Items[0].Value = "-1";
        }
    }
    private void getgroupmodule(Int16 Groupid)
    {
        try
        {
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.f_get_module");
            MyCommand.Parameters.AddWithValue("p_userid", txtempid.Text);
            MyCommand.Parameters.AddWithValue("p_groupid", Convert.ToInt32(Groupid));

            DataTable MyTable = bll.SelectAll(MyCommand);
            if (MyTable.TableName == "Success")
            {
                if (MyTable.Rows.Count > 0)
                {
                    grdavailableforassign.DataSource = MyTable;
                    grdavailableforassign.DataBind();
                    grdavailableforassign.Visible = true;
                    pnlavailableforassignNoRecord.Visible = false;
                }
                else
                {
                    grdavailableforassign.Visible = false;
                    pnlavailableforassignNoRecord.Visible = true;
                }
            }
            else
            {
                grdavailableforassign.Visible = false;
                pnlavailableforassignNoRecord.Visible = true;
            }
        }
        catch (Exception ex)
        {
        }
    }
    private bool validuser()
    {
        try
        {
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_modulegrouphqemp");
            MyCommand.Parameters.AddWithValue("p_userid", txtempid.Text);

            DataTable MyTable = bll.SelectAll(MyCommand);
            if (MyTable.TableName == "Success")
            {
                if (MyTable.Rows.Count > 0)
                {
                    lblempname.Text = MyTable.Rows[0]["emp_f_name"].ToString();
                    lblempoffice.Text = MyTable.Rows[0]["ofclvlname"] + "(" + MyTable.Rows[0]["office_name"] + ")";
                    lblempmobile.Text = MyTable.Rows[0]["mobile_number"].ToString();
                    lblempdesignation.Text = MyTable.Rows[0]["designation_name"].ToString();
                    lblempemailid.Text = MyTable.Rows[0]["emprmailid"].ToString();
                    lblempemrgencyno.Text = MyTable.Rows[0]["emergencyno"].ToString();

                    pnldetails.Visible = true;
                    pnldetailsnodata.Visible = false;
                    return true;
                }
                else
                {
                    pnldetails.Visible = false;
                    pnldetailsnodata.Visible = true;
                    return false;
                }
            }
            else
            {
                pnldetails.Visible = false;
                pnldetailsnodata.Visible = true;
                return false;
            }
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    private void getassignedmodule(string UserId)
    {
        try
        {
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_assigngroupmodule");
            MyCommand.Parameters.AddWithValue("p_userid", UserId);

            DataTable MyTable = bll.SelectAll(MyCommand);
            if (MyTable.TableName == "Success")
            {
                if (MyTable.Rows.Count > 0)
                {
                    grdassignedmodule.DataSource = MyTable;
                    grdassignedmodule.DataBind();
                    grdassignedmodule.Visible = true;
                    pnlassignedmoduleNoRecord.Visible = false;
                }
                else
                {
                    grdassignedmodule.Visible = false;
                    pnlassignedmoduleNoRecord.Visible = true;
                }
            }
            else
            {
                grdassignedmodule.Visible = false;
                pnlassignedmoduleNoRecord.Visible = true;
            }
        }
        catch (Exception ex)
        {
        }
    }
    private string checkmoduleassign(Int16 moduleid, Int16 groupid)
    {
        try
        {
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_assigngroupmodulecheck");
            MyCommand.Parameters.AddWithValue("p_groupid", groupid);
            MyCommand.Parameters.AddWithValue("p_moduleid", moduleid);

            DataTable MyTable = bll.SelectAll(MyCommand);
            if (MyTable.TableName == "Success")
            {
                if (MyTable.Rows.Count > 0)
                {
                    if (DBNull.Value.Equals(MyTable.Rows[0]["user_id"]))
                        return "Module not assign any other user <br/>";
                    else
                        return MyTable.Rows[0]["assigned"] + "<br/>" + MyTable.Rows[0]["user_id"];
                }
                return "0";
            }

            return "0";
        }
        catch (Exception ex)
        {
            return "0";
        }
    }
    #endregion

    #region "Events"
    protected void ddlgroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        getgroupmodule(Convert.ToInt16(ddlgroup.SelectedValue));
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        if (validuser() == false)
        {
            lblerrormsg.Text = "Invalid HQ Employee.";
            mperror.Show();
            return;
        }
        pnldetails.Visible = true;
        pnldetailsnodata.Visible = false;

        getmodulegroup();
        getgroupmodule(Convert.ToInt16(ddlgroup.SelectedValue));
        getassignedmodule(txtempid.Text);
    }
    protected void btnreset_Click(object sender, EventArgs e)
    {
        txtempid.Text = "";
        pnldetails.Visible = false;
        pnldetailsnodata.Visible = true;
    }
    protected void grdavailableforassign_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "Assign")
        {
            Session["ModuleId"] = grdavailableforassign.DataKeys[index].Values["module_id"];
            Session["GroupId"] = grdavailableforassign.DataKeys[index].Values["group_id"];
            string user = checkmoduleassign(Convert.ToInt16(Session["ModuleId"]), Convert.ToInt16(Session["GroupId"]));

            lblConfirmation.Text = user + "<br/><span style='color:red;'> Do you want to Assign module ? </sapn>";
            Session["ModuleAction"] = "A";

            mpConfirmation.Show();
        }
    }
    protected void grdassignedmodule_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {

        int index = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "Remove")
        {
            lblConfirmation.Text = "Do you want Remove Assigned Module ?";
            Session["ModuleAction"] = "R";
            Session["ModuleId"] = grdassignedmodule.DataKeys[index].Values["module_id"];
            Session["GroupId"] = grdassignedmodule.DataKeys[index].Values["group_id"];
            mpConfirmation.Show();
        }
    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        try
        {
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            string Mresult = "";
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_groupmodulemaping_insertupdate");
            MyCommand.Parameters.AddWithValue("p_moduleaction", Session["ModuleAction"].ToString().Trim());
            MyCommand.Parameters.AddWithValue("p_groupid", Convert.ToInt32(Session["GroupId"].ToString()));
            MyCommand.Parameters.AddWithValue("p_moduleid", Convert.ToInt32(Session["ModuleId"]));
            MyCommand.Parameters.AddWithValue("p_userid", txtempid.Text.Trim());
            MyCommand.Parameters.AddWithValue("p_assignby", Session["_UserCode"]);
            Mresult = bll.UpdateAll(MyCommand);
            if (Mresult == "Success")
            {
                if (Session["ModuleAction"].ToString() == "A")
                    lblsuccessmsg.Text = "module assignment Sucessfully ";
                if (Session["ModuleAction"].ToString() == "R")
                    lblsuccessmsg.Text = "module Remove Sucessfully ";
                mpsuccess.Show();
                getgroupmodule(Convert.ToInt16(ddlgroup.SelectedValue));
                getassignedmodule(txtempid.Text);
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void lbtnNoConfirmation_Click(object sender, EventArgs e)
    {
        mpConfirmation.Hide();
    }
    #endregion
}