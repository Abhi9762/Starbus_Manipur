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

public partial class Auth_SysAdminCatelogue : System.Web.UI.Page
{
    private NpgsqlCommand MyCommand;
    DataTable MyTable = new DataTable();
    private sbBLL bll = new sbBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["_RoleCode"].ToString() == "1")
            {
                Session["MasterPageHeaderText"] = "System Admin |";
            }
            else
            {
                Session["MasterPageHeaderText"] = "HQ Employee |";
            }
            GetModuleGroup(Session["_UserCode"].ToString(), rptgroup, "G", 0);
            Session["_moduleName"] = "Catalogue";
        }
    }

    private void GetModuleGroup(string usercode, Repeater rptgroup, string flag, int groupid)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.f_get_module_group");
            MyCommand.Parameters.AddWithValue("p_userid", usercode);
            MyTable = bll.SelectAll(MyCommand);

            if (MyTable.TableName == "Success")
            {
                if (MyTable.Rows.Count > 0)
                {
                    rptgroup.DataSource = MyTable;
                    rptgroup.DataBind();
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void rptgroup_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) | (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            Label lblgroupid = (Label)e.Item.FindControl("lblgroupid");
            Repeater rptmodule = (Repeater)e.Item.FindControl("rptgroupmodule");

            GetModule(Session["_UserCode"].ToString(), rptmodule, "M", Convert.ToInt16(lblgroupid.Text.ToString()));
        }
    }
    private void GetModule(string usercode, Repeater rptmodule, string flag, int groupid)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.f_get_catelogue_module");
            MyCommand.Parameters.AddWithValue("p_userid", usercode);
            MyCommand.Parameters.AddWithValue("p_groupid", groupid);
            MyTable = bll.SelectAll(MyCommand);
            if (MyTable.TableName == "Success")
            {
                if (MyTable.Rows.Count > 0)
                {
                    rptmodule.DataSource = MyTable;
                    rptmodule.DataBind();
                }
            }
        }
        catch(Exception ex)
        {

        }
    }

    protected void rptgroupmodule_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            if (e.CommandName == "View")
            {
                Label lblmoduleurl = (Label)e.Item.FindControl("lblmoduleurl");
                //Int16 ModuleID = e.CommandArgument;
                Response.Redirect(lblmoduleurl.Text);
            }
        }
    }
}