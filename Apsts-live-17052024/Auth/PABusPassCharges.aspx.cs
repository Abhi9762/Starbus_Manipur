using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_PABusPassCharges : System.Web.UI.Page
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblSummary.Text = "Summary as on Date " + DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
            Session["ModuleName"] = "Bus Pass Configuration";
            BusPassChargeList();
            BussPassChargesCount();
            ChargesTypeList();
        }

    }

    #region "Methods"
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
    protected void BusPassChargeList()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_bus_pass_charges_list");

            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    gvBusPassCharges.DataSource = dt;
                    gvBusPassCharges.DataBind();
                    gvBusPassCharges.Visible = true;
                    pnlnoRecordfound.Visible = false;
                }
                else
                {
                    gvBusPassCharges.Visible = false;
                    pnlnoRecordfound.Visible = true;
                }
            }
        }

        catch (Exception ex)
        {
            Errormsg(ex.Message.ToString());
        }
    }
    private void BussPassChargesCount()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_bus_pass_charge_count");
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
    private void ChargesTypeList()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_bus_pass_charges_list");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlBusPassCharges.DataSource = dt;
                    ddlBusPassCharges.DataTextField = "chargetype_name";
                    ddlBusPassCharges.DataValueField = "charge_typeid";
                    ddlBusPassCharges.DataBind();
                }
            }

            ddlBusPassCharges.Items.Insert(0, "Select");
            ddlBusPassCharges.Items[0].Value = "0";
            ddlBusPassCharges.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlBusPassCharges.Items.Insert(0, "Select");
            ddlBusPassCharges.Items[0].Value = "0";
            ddlBusPassCharges.SelectedIndex = 0;
        }
    }
    private void resetcontrol()
    {
        tbName.Enabled = true;
        tbNameLocal.Enabled = true;
        tbRemark.Enabled = true;
        tbAbbreviation.Enabled = true;
        tbAbbrLocal.Enabled = true;
        tbName.Text = "";
        tbNameLocal.Text = "";
        tbRemark.Text = "";
        tbChargeAmt.Text = "";
        tbAbbreviation.Text = "";
        tbAbbrLocal.Text = "";
    }
    private bool Validation()
    {
        try
        {
            int count = 0;
            string msg = "";

            if (!_validation.IsValidString(tbName.Text, 3, tbName.MaxLength))
            {
                count++;
                msg += count + ". Enter Valid Bus Pass Charges Name In English.<br/>";
            }

            if (!_validation.IsValidString(tbRemark.Text, 3, tbRemark.MaxLength))
            {
                count++;
                msg += count + ". Enter Valid Remark.<br/>";
            }

            if (!_validation.IsValidString(tbChargeAmt.Text, 1, tbChargeAmt.MaxLength))
            {
                count++;
                msg += count + ". Enter Valid Charge Amount.<br/>";
            }

            if (!_validation.IsValidString(tbAbbreviation.Text, 1, tbAbbreviation.MaxLength))
            {
                count++;
                msg += count + ". Enter Valid Charge Abbreviation.<br/>";
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
    private void SaveBusPassCharge()
    {
        try
        {
            string Status = "A", chargeName = "", chargeNameL = "", Abbr = "", Remark = "", AbbrLocal = "";
            decimal chargeamt = 0, chargeid = 0;
            chargeName = tbName.Text;
            chargeNameL = tbNameLocal.Text;
            Abbr = tbAbbreviation.Text;
            AbbrLocal = tbAbbrLocal.Text;
            Remark = tbRemark.Text;

            if (!string.IsNullOrEmpty(tbChargeAmt.Text))
            {
                chargeamt = Convert.ToDecimal(tbChargeAmt.Text);
            }

            if (Session["_action"].ToString() == "activate")
            {
                Status = "A";
            }

            if (Session["_action"].ToString() == "deactivate")
            {
                Status = "D";
            }

            if (Session["_action"].ToString() == "U" || Session["_action"].ToString() == "activate" || Session["_action"].ToString() == "deactivate")
            {
                chargeid = Convert.ToDecimal(Session["chargeid"].ToString());
            }

            string IPAddress = HttpContext.Current.Request.UserHostAddress;

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_buspasscharges_insertupdate");
            MyCommand.Parameters.AddWithValue("@p_action", Session["_action"].ToString());
            MyCommand.Parameters.AddWithValue("@p_updatedby", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("@p_ipaddress", IPAddress);
            MyCommand.Parameters.AddWithValue("@p_charge_type_id", chargeid);
            MyCommand.Parameters.AddWithValue("@p_charge_name", chargeName);
            MyCommand.Parameters.AddWithValue("@p_charge_name_local", chargeNameL);
            MyCommand.Parameters.AddWithValue("@p_charge_abbr", Abbr);
            MyCommand.Parameters.AddWithValue("@p_charge_abbr_local", AbbrLocal);
            MyCommand.Parameters.AddWithValue("@p_remark", Remark);
            MyCommand.Parameters.AddWithValue("@p_charge_amt", chargeamt);
            MyCommand.Parameters.AddWithValue("@p_status", Status);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    if (Session["_action"].ToString() == "S")
                    {
                        Successmsg("Bus Pass Charges Added Successfully.");
                    }

                    if (Session["_action"].ToString() == "U")
                    {
                        Successmsg("Bus Pass Charges Updated Successfully.");
                    }

                    if (Session["_action"].ToString() == "activate")
                    {
                        Successmsg("Bus Pass Charges Activated.");
                    }

                    if (Session["_action"].ToString() == "deactivate")
                    {
                        Successmsg("Bus Pass Charges Discontinued.");
                    }

                    lbtnSave.Visible = true;
                    lbtnUpdate.Visible = false;
                    lbtnReset.Visible = true;
                    lbtnCancel.Visible = false;
                    ChargesTypeList();
                    resetcontrol();
                    BusPassChargeList();
                    BussPassChargesCount();
                }
                else
                {
                    Errormsg("Something went Wrong!");
                }
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message.ToString());
        }
    }
    #endregion

    #region "Event"
    protected void gvBusPassCharges_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Deactivate")
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            string chargeid = gvBusPassCharges.DataKeys[row.RowIndex]["charge_typeid"].ToString();
            ConfirmMsg("Do you want to Discontinue Bus Pass Charge?");
            Session["_action"] = "deactivate";
            Session["chargeid"] = chargeid;
        }

        if (e.CommandName == "Activate")
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            string chargeid = gvBusPassCharges.DataKeys[row.RowIndex]["charge_typeid"].ToString();
            ConfirmMsg("Do you want to Activate Bus Pass Charge?");
            Session["_action"] = "activate";
            Session["chargeid"] = chargeid;
        }

        if (e.CommandName == "updateBusPassCharges")
        {
            lbtnCancel.Visible = true;
            lbtnUpdate.Visible = true;
            lbtnSave.Visible = false;
            lbtnReset.Visible = false;
            tbName.Enabled = false;
            tbNameLocal.Enabled = false;
            tbRemark.Enabled = false;
            tbAbbreviation.Enabled = false;
            tbAbbrLocal.Enabled = false;
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            string chargeid = gvBusPassCharges.DataKeys[row.RowIndex]["charge_typeid"].ToString();
            string chargename = gvBusPassCharges.DataKeys[row.RowIndex]["chargetype_name"].ToString();
            string chargenameL = gvBusPassCharges.DataKeys[row.RowIndex]["chargetype_name_local"].ToString();
            string chargeabbr = gvBusPassCharges.DataKeys[row.RowIndex]["chargetype_abbr"].ToString();
            string charge_type_abbrlocal = gvBusPassCharges.DataKeys[row.RowIndex]["charge_type_abbrlocal"].ToString();
            string remark = gvBusPassCharges.DataKeys[row.RowIndex]["remarks"].ToString();
            string amt = gvBusPassCharges.DataKeys[row.RowIndex]["chargeamt"].ToString();
            Session["chargeid"] = chargeid;
            tbName.Text = chargename;
            tbNameLocal.Text = chargenameL;
            tbAbbreviation.Text = chargeabbr;
            tbAbbrLocal.Text = charge_type_abbrlocal;
            tbRemark.Text = remark;
            tbChargeAmt.Text = amt;
        }

    }

    protected void gvBusPassCharges_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbtnActivate = (LinkButton)e.Row.FindControl("lbtnActivate");
            LinkButton lbtnDeactivate = (LinkButton)e.Row.FindControl("lbtnDeactivate");
            LinkButton lbtnUpdateBusPassCharges = (LinkButton)e.Row.FindControl("lbtnUpdateBusPassCharges");
            DataRowView rowView = (DataRowView)e.Row.DataItem;
            lbtnActivate.Visible = false;
            lbtnDeactivate.Visible = false;
            lbtnUpdateBusPassCharges.Visible = false;

            if (rowView["currentstatus"].ToString() == "A")
            {
                lbtnDeactivate.Visible = true;
                lbtnUpdateBusPassCharges.Visible = true;
            }
            else if (rowView["currentstatus"].ToString() == "D")
            {
                lbtnActivate.Visible = true;
            }
        }

    }

    protected void lbtnCancel_Click(object sender, EventArgs e)
    {
        lbtnSave.Visible = true;
        lbtnUpdate.Visible = false;
        lbtnReset.Visible = true;
        lbtnCancel.Visible = false;
        resetcontrol();
    }

    protected void lbtnReset_Click(object sender, EventArgs e)
    {
        resetcontrol();
    }

    protected void lbtnSave_Click(object sender, EventArgs e)
    {
        if (Validation() == false)
        {
            return;
        }

        ConfirmMsg("Do you want to save Bus Pass Charges Details?");
        Session["_action"] = "S";

    }

    protected void lbtnUpdate_Click(object sender, EventArgs e)
    {
        if (Validation() == false)
        {
            return;
        }

        ConfirmMsg("Do you want to Update Bus Pass Charges Details?");
        Session["_action"] = "U";

    }

  

    protected void lbtnview_Click(object sender, EventArgs e)
    {
        InfoMsg("Bus Pass Charges name,remark,abbrevation cannot be edited after creation");
    }

    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        SaveBusPassCharge();
    }

    protected void gvBusPassCharges_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvBusPassCharges.PageIndex = e.NewPageIndex;
        BusPassChargeList();
    }
    #endregion
}