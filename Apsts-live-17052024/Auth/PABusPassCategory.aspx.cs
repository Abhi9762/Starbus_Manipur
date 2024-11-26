using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_PABusPassCategory : System.Web.UI.Page
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["_moduleName"] = "Bus Pass Configuration";
        lblSummary.Text = "Summary as on Date " + DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");

        if (!IsPostBack)
        {
            BusPassCategoryList();
            BusPassCategoryCount();
            CategoriesTypeList();
        }

    }

    #region "Method"

    private void Errormsg(string msg)
    {
        lblerrormsg.Text = msg;
        mperror.Show();
    }

    private void Successmsg(string msg)
    {
        lblsuccessmsg.Text = msg;
        mpsuccess.Show();
    }

    private void ConfirmMsg(string msg)
    {
        lblConfirmation.Text = msg;
        mpConfirmation.Show();
    }

    private void InfoMsg(string msg)
    {
        lblinstruction.Text = msg;
        mpInfo.Show();
    }


    protected void BusPassCategoryList()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.bus_pass_category_list");

            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    gvBusPassCategories.DataSource = dt;
                    gvBusPassCategories.DataBind();
                    gvBusPassCategories.Visible = true;
                    pnlnoRecordfound.Visible = false;
                }
                else
                {
                    gvBusPassCategories.Visible = false;
                    pnlnoRecordfound.Visible = true;
                }
            }
        }

        catch (Exception ex)
        {
            Errormsg(ex.Message.ToString());
        }
    }



    private void BusPassCategoryCount()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_bus_pass_category_count");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    lblTotal.Text = dt.Rows[0]["total"].ToString();
                    lblActivate.Text = dt.Rows[0]["active"].ToString();
                    lblDeactive.Text = dt.Rows[0]["deactive"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message.ToString());
        }
    }


    private void CategoriesTypeList()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.bus_pass_category_list");

            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlBusPassCategory.DataSource = dt;
                    ddlBusPassCategory.DataTextField = "buspass_categoryname";
                    ddlBusPassCategory.DataValueField = "buspass_categoryid";
                    ddlBusPassCategory.DataBind();
                }
            }

            ddlBusPassCategory.Items.Insert(0, "Select");
            ddlBusPassCategory.Items[0].Value = "0";
            ddlBusPassCategory.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlBusPassCategory.Items.Insert(0, "Select");
            ddlBusPassCategory.Items[0].Value = "0";
            ddlBusPassCategory.SelectedIndex = 0;
        }
    }


    private void ResetControl()
    {
        tbName.Enabled = true;
        tbNameLocal.Enabled = true;
        tbRemark.Enabled = true;
        tbName.Text = "";
        tbNameLocal.Text = "";
        tbRemark.Text = "";
    }

    private bool Validation()
    {
        try
        {
            int count = 0;
            string msg = "";

            if (_validation.IsValidString(tbName.Text, 3, tbName.MaxLength) == false)
            {
                count += 1;
                msg = msg + count + ". Enter Valid Bus Pass Category Name In English.<br/>";
            }

            if (_validation.IsValidString(tbRemark.Text, 3, tbRemark.MaxLength) == false)
            {
                count += 1;
                msg = msg + count + ". Enter Valid Remark.<br/>";
            }

            if (count > 0)
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



    private void SaveBusPassCategory()
    {
        try
        {
            string status = "A", categoryName = "", categoryNameL = "", remark = "";
            decimal categoryId = 0;
            categoryName = tbName.Text;
            categoryNameL = tbNameLocal.Text;
            remark = tbRemark.Text;

            if (Session["_action"].ToString() == "activate")
            {
                status = "A";
            }

            if (Session["_action"].ToString() == "deactivate")
            {
                status = "D";
            }

            if (Session["_action"].ToString() == "U" || Session["_action"].ToString() == "activate" || Session["_action"].ToString() == "deactivate")
            {
                categoryId = Convert.ToDecimal(Session["categoryid"].ToString());
            }

            string ipAddress = HttpContext.Current.Request.UserHostAddress;

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_buspasscategory_insertupdate");
            MyCommand.Parameters.AddWithValue("@p_action", Session["_action"].ToString());
            MyCommand.Parameters.AddWithValue("@p_updatedby", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("@p_ipaddress", ipAddress);
            MyCommand.Parameters.AddWithValue("@p_category_type_id", categoryId);
            MyCommand.Parameters.AddWithValue("@p_category_name", categoryName);
            MyCommand.Parameters.AddWithValue("@p_category_name_local", categoryNameL);
            MyCommand.Parameters.AddWithValue("@p_remark", remark);
            MyCommand.Parameters.AddWithValue("@p_status", status);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    if (Session["_action"].ToString() == "S")
                    {
                        Successmsg("Bus Pass Category Added Successfully.");
                    }

                    if (Session["_action"].ToString() == "U")
                    {
                        Successmsg("Bus Pass Category Updated Successfully.");
                    }

                    if (Session["_action"].ToString() == "activate")
                    {
                        Successmsg("Bus Pass Category Activated.");
                    }

                    if (Session["_action"].ToString() == "deactivate")
                    {
                        Successmsg("Bus Pass Category Discontinued.");
                    }

                    lbtnSave.Visible = true;
                    lbtnUpdate.Visible = false;
                    lbtnReset.Visible = true;
                    lbtnCancel.Visible = false;
                    ResetControl();
                    BusPassCategoryList();
                    BusPassCategoryCount();
                    CategoriesTypeList();
                }
            }
            else
            {
                Errormsg("Something Went Wrong!");
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message.ToString());
        }
    }

    #endregion


    #region "Event"
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        SaveBusPassCategory();
    }

    protected void lbtnview_Click(object sender, EventArgs e)
    {
        InfoMsg("Bus Pass Category Name and remark can be edited if it is Active");
    }

 

    protected void lbtnUpdate_Click(object sender, EventArgs e)
    {
        if (Validation() == false)
        {
            return;
        }

        ConfirmMsg("Do you want to update Bus Pass Category Details?");
        Session["_action"] = "U";

    }

    protected void lbtnSave_Click(object sender, EventArgs e)
    {
        if (Validation() == false)
        {
            return;
        }

        ConfirmMsg("Do you want to save Bus Pass Category Details?");
        Session["_action"] = "S";

    }

    protected void lbtnReset_Click(object sender, EventArgs e)
    {
        ResetControl();
    }

    protected void lbtnCancel_Click(object sender, EventArgs e)
    {
        lbtnSave.Visible = true;
        lbtnUpdate.Visible = false;
        lbtnReset.Visible = true;
        lbtnCancel.Visible = false;
        ResetControl();
    }

    protected void gvBusPassCategories_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvBusPassCategories.PageIndex = e.NewPageIndex;
        BusPassCategoryList();
    }

    protected void gvBusPassCategories_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Deactivate")
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            string categoryid = gvBusPassCategories.DataKeys[row.RowIndex]["buspass_categoryid"].ToString();
            ConfirmMsg("Do you want to Discontinue Bus Pass Category?");
            Session["_action"] = "deactivate";
            Session["categoryid"] = categoryid;
        }

        if (e.CommandName == "Activate")
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            string categoryid = gvBusPassCategories.DataKeys[row.RowIndex]["buspass_categoryid"].ToString();
            ConfirmMsg("Do you want to Activate Bus Pass Category?");
            Session["_action"] = "activate";
            Session["categoryid"] = categoryid;
        }

        if (e.CommandName == "updateBusPassCategory")
        {
            lbtnCancel.Visible = true;
            lbtnUpdate.Visible = true;
            lbtnSave.Visible = false;
            lbtnReset.Visible = false;
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            string categoryid = gvBusPassCategories.DataKeys[row.RowIndex]["buspass_categoryid"].ToString();
            string Categoryname = gvBusPassCategories.DataKeys[row.RowIndex]["buspass_categoryname"].ToString();
            string CategorynameL = gvBusPassCategories.DataKeys[row.RowIndex]["buspass_category_namelocal"].ToString();
            string remark = gvBusPassCategories.DataKeys[row.RowIndex]["remark_"].ToString();
            Session["categoryid"] = categoryid;
            tbName.Text = Categoryname;
            tbNameLocal.Text = CategorynameL;
            tbRemark.Text = remark;
        }

    }

    protected void gvBusPassCategories_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbtnActivate = (LinkButton)e.Row.FindControl("lbtnActivate");
            LinkButton lbtnDeactivate = (LinkButton)e.Row.FindControl("lbtnDeactivate");
            LinkButton lbtnUpdateBusPassCategory = (LinkButton)e.Row.FindControl("lbtnUpdateBusPassCategory");
            DataRowView rowView = (DataRowView)e.Row.DataItem;
            lbtnActivate.Visible = false;
            lbtnDeactivate.Visible = false;
            lbtnUpdateBusPassCategory.Visible = false;

            if (rowView["currentstatus"].ToString() == "A")
            {
                lbtnDeactivate.Visible = true;
                lbtnUpdateBusPassCategory.Visible = true;
            }
            else if (rowView["currentstatus"].ToString() == "D")
            {
                lbtnActivate.Visible = true;
            }
        }

    }

    #endregion
}