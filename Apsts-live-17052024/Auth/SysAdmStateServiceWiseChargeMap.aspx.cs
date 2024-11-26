using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_SysAdmServiceWiseChargeMap : BasePage
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    string commonerror = "There is some error. Please contact the administrator or try again after some time.";
    
    protected void Page_Load(object sender, EventArgs e)
    {
        lblSummary.Text = "Summary as on Date " + DateTime.Now.ToString("dd/MM/yyyy hh:ss tt");
        Session["_moduleName"] = "State Service Wise Charge Map";
        if (IsPostBack == false)
        {
            loadStates();
            loadCharges();
            getServiceWiseChargeMapList();
            chargeMapCount();
        }
    }
    #region "Method"
    private void Errormsg(string msg)
    {
        string popup = _popup.modalPopupSmall("D", "Please Check", msg, "Close");
        Response.Write(popup);
    }
    private void Successmsg(string msg)
    {
        string popup = _popup.modalPopupSmall("S", "Congratulations", msg, "Close");
        Response.Write(popup);
    }
    private void ConfirmMsg(string msg)
    {
        lblConfirmation.Text = msg;
        mpConfirmation.Show();
    }
    private void InfoMsg(string msg)
    {
        string popup = _popup.modalPopupSmall("I", "Information", msg, "Close");
        Response.Write(popup);
    }
    public void resetcontrols()
    {
        ddlFromState.SelectedValue = "0";
        ddlFromState.Enabled = true;
        ddlToState.SelectedValue = "0";
        ddlToState.Enabled = true;
        ddlChargeType.SelectedValue = "0";
        cbCombined.Checked = false;
        tbDate.Text = "";
        lblFareHead.Visible = true;
        lblUpdate.Visible = false;
        lbtnUpdate.Visible = false;
        lbtnSave.Visible = true;
        lbtnReset.Visible = true;
        lbtnCancel.Visible = false;
        
    }
    public string getStateFareType(string stateID)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.f_get_stateslist");
            DataTable ddtt = new DataTable();
            ddtt = bll.SelectAll(MyCommand);
            if (ddtt.Rows.Count > 0)
            {
                DataRow[] dr = ddtt.Select(" stcode = '" + stateID + "'");
                string faretype = dr[0]["fare"].ToString();
                return faretype;
            }
            else
            {
                return "0";
            }
        }
        catch (Exception ex)
        {
            return "0" + ex.ToString();

        }
    }
    public void loadStates()//M1
    {
        try
        {
            ddlFromState.Items.Clear(); 

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.f_get_stateslist");
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                ddlFromState.DataSource = dt;
                ddlFromState.DataTextField = "stname";
                ddlFromState.DataValueField = "stcode";
                ddlFromState.DataBind();
            }
            ddlFromState.Items.Insert(0, "SELECT");
            ddlFromState.Items[0].Value = "0";
            ddlFromState.SelectedIndex = 0;   
        }
        catch (Exception ex)
        {
            ddlFromState.Items.Insert(0, "SELECT");
            ddlFromState.Items[0].Value = "0";
            ddlFromState.SelectedIndex = 0;
            _common.ErrorLog("ChargeMap-M1", ex.Message.ToString());
        }
    }  
    public void loadCharges()//M2
    {
        try
        {
            ddlChargeType.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_fr_ssw_charge_map_charges");
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                ddlChargeType.DataSource = dt;
                ddlChargeType.DataTextField = "charge_type";
                ddlChargeType.DataValueField = "charge_id";
                ddlChargeType.DataBind();

            }
            ddlChargeType.Items.Insert(0, "SELECT");
            ddlChargeType.Items[0].Value = "0";
            ddlChargeType.SelectedIndex = 0;

        }
        catch (Exception ex)
        {
            ddlChargeType.Items.Insert(0, "SELECT");
            ddlChargeType.Items[0].Value = "0";
            ddlChargeType.SelectedIndex = 0;
            _common.ErrorLog("ChargeMap-M2", ex.Message.ToString());
        }
    }   
    private void saveServiceWiseChargeMap()//M3
    {
        try
        {
            string combined = " ", status = "A";
            long scsmm = Convert.ToInt64(0);
            string FromState = ddlFromState.SelectedValue;
            string ToState = ddlToState.SelectedValue;
            string chargeType = ddlChargeType.SelectedValue;
            if (cbCombined.Checked == true)
            {
                combined = "Y";
            }
            else
            {
                combined = "N";
            }
            string Date = tbDate.Text;
            string IPAddress = HttpContext.Current.Request.UserHostAddress;
            if (Session["Action"].ToString() == "U")
            {
                scsmm = Convert.ToInt64(Session["scsmm"].ToString());
            }
            if (Session["Action"].ToString() == "Deactive")
            {
                scsmm = Convert.ToInt64(Session["scsmm"].ToString());
                status = "D";
            }
            if (Session["Action"].ToString() == "Active")
            {
                scsmm = Convert.ToInt64(Session["scsmm"].ToString());
                status = "A";
            }
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_fr_ssw_charge_map_insert");
            MyCommand.Parameters.AddWithValue("p_scsmm", scsmm);
            MyCommand.Parameters.AddWithValue("p_action", Session["Action"].ToString());
            MyCommand.Parameters.AddWithValue("p_statefromcode", FromState);
            MyCommand.Parameters.AddWithValue("p_statetocode", ToState);
            MyCommand.Parameters.AddWithValue("p_chargetype", Convert.ToInt16(chargeType));
            MyCommand.Parameters.AddWithValue("p_combined", combined);
            MyCommand.Parameters.AddWithValue("p_status", status);
            MyCommand.Parameters.AddWithValue("p_date", Date);
            MyCommand.Parameters.AddWithValue("p_updatedby", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("p_ipaddress", IPAddress);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {

                if (dt.Rows[0]["resultstr"].ToString() == "DONE")
                {
                    if (Session["Action"].ToString() == "U")
                    {
                        Successmsg("Updated Successfully !");
                    }
                    if (Session["Action"].ToString() == "S")
                    {
                        Successmsg("Saved Successfully !");
                    }
                    if (Session["Action"].ToString() == "Deactive")
                    {
                        Successmsg("Deactivated Successfully !");
                    }
                    if (Session["Action"].ToString() == "Active")
                    {
                        Successmsg("Activated Successfully !");
                    }
                    getServiceWiseChargeMapList();
                    chargeMapCount();
                    resetcontrols();
                }
                else if (dt.Rows[0]["resultstr"].ToString() == "ALREADY")
                {
                    Errormsg("State charges already mapped for selected 'States'");
                }
                else if (dt.Rows[0]["resultstr"].ToString() == "EXCEPTION")
                {
                    Errormsg(commonerror);
                }
                else
                {
                    Errormsg(commonerror);
                    _common.ErrorLog("sysAdminServiceWiseCharge-M6", dt.TableName);
                }
            }
            else
            {
                Errormsg(commonerror);
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("ChargeMap-M3", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
        }
    }
    public void getServiceWiseChargeMapList()//M4
    {
        try
        {
            string from = "0";
            string to = "0";
            string charge = "0";

            gvServiceWiseChargeMap.Visible = false;
            pnlNoFare.Visible = true;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_fr_ssw_charge_map_list_get");
            MyCommand.Parameters.AddWithValue("p_from", from);
            MyCommand.Parameters.AddWithValue("p_to", to);
            MyCommand.Parameters.AddWithValue("p_chargeid", Convert.ToInt16(charge));
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                gvServiceWiseChargeMap.DataSource = dt;
                gvServiceWiseChargeMap.DataBind();
                gvServiceWiseChargeMap.Visible = true;
                pnlNoFare.Visible = false;

            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("ChargeMap-M3", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
        }
    }
    private void chargeMapCount()//M5
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_fr_ssw_charge_map_summary");
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                lblTotalFare.Text = dt.Rows[0]["total"].ToString();
                lblMergeState.Text =   dt.Rows[0]["merges"].ToString();
                lblActivateFare.Text =  dt.Rows[0]["active"].ToString();
                lblDeactFare.Text =  dt.Rows[0]["deactive"].ToString();

            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("ChargeMap-M5", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
        }
    }
    private Boolean Validaion()
    {
        try
        {
            int count = 0;
            string msg = "";         
            

            if (ddlFromState.SelectedValue == "0")
            {
                count++;
                msg = msg + count + ". Select From State <br/>";
            }
            if (ddlToState.SelectedValue == "0")
            {
                count++;
                msg = msg + count + ". Select To State <br/>";
            }
            if (ddlChargeType.SelectedValue == "0")
            {
                count++;
                msg = msg + count + ". Select charge Type <br/>";
            }

            if (tbDate.Text ==  "")
            {
                
                    count = count + 1;
                    msg = msg + count.ToString() + ". Enter Effective from Date.<br/>";
                
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
    private void Load()
    {
        try
        {
            string ft = getStateFareType(ddlFromState.SelectedValue.ToString());
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_fr_ssw_charge_map_state_same");
            MyCommand.Parameters.AddWithValue("p_fare", ft);
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                ddlToState.DataSource = dt;
                ddlToState.DataTextField = "stname";
                ddlToState.DataValueField = "stcode";
                ddlToState.DataBind();
            }
            ddlToState.Items.Insert(0, "SELECT");
            ddlToState.Items[0].Value = "0";
            ddlToState.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlToState.Items.Insert(0, "SELECT");
            ddlToState.Items[0].Value = "0";
            ddlToState.SelectedIndex = 0;
            _common.ErrorLog("ChargeMap-E1", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
        }
    }

    #endregion

    #region "Event"

    protected void gvServiceWiseChargeMap_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvServiceWiseChargeMap.PageIndex = e.NewPageIndex;
        getServiceWiseChargeMapList();
    }
    protected void gvServiceWiseChargeMap_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbtnActivate = (LinkButton)e.Row.FindControl("lbtnActivate");
            LinkButton lbtnDiscontinue = (LinkButton)e.Row.FindControl("lbtnDiscontinue");
            LinkButton lbtnUpdateChargeMap = (LinkButton)e.Row.FindControl("lbtnUpdateChargeMap");
            DataRowView rowView = (DataRowView)e.Row.DataItem;
            lbtnActivate.Visible = false;
            lbtnDiscontinue.Visible = false;

            if (rowView["statusyn"].ToString() == "A")
            {
                lbtnDiscontinue.Visible = true;
            }
            else if (rowView["statusyn"].ToString() == "D")
            {
                lbtnActivate.Visible = true;
              //  lbtnUpdateChargeMap.Visible = false;
            }


        }
    }
    protected void gvServiceWiseChargeMap_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "updateFare")
        {   
            lblUpdate.Visible = true;
            lblFareHead.Visible = false;
            lbtnSave.Visible = false;
            lbtnUpdate.Visible = true;
            lbtnReset.Visible = false;
            lbtnCancel.Visible = true;
            ddlToState.Enabled = false;
            ddlFromState.Enabled = false;
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            string ID = gvServiceWiseChargeMap.DataKeys[row.RowIndex]["scsmm"].ToString();
            Session["scsmm"] = ID;
            lblUpdate.Text = "Update State Service Wise Charge Map-  (" + gvServiceWiseChargeMap.DataKeys[row.RowIndex]["fromst"].ToString() + " TO " + gvServiceWiseChargeMap.DataKeys[row.RowIndex]["tost"].ToString() + ")";
            string fromState = gvServiceWiseChargeMap.DataKeys[row.RowIndex]["from_s"].ToString();
            string TOState = gvServiceWiseChargeMap.DataKeys[row.RowIndex]["to_s"].ToString();
            string ChargeId = gvServiceWiseChargeMap.DataKeys[row.RowIndex]["chargeid"].ToString();
            string combinedflag = gvServiceWiseChargeMap.DataKeys[row.RowIndex]["combinedflag"].ToString();
            string Date = gvServiceWiseChargeMap.DataKeys[row.RowIndex]["up_datedt"].ToString();
            ddlFromState.SelectedValue = fromState;
            Load();
            ddlToState.SelectedValue = TOState;
            ddlChargeType.SelectedValue = ChargeId;
            tbDate.Text = Date;
            if (combinedflag == "Y")
            {
                cbCombined.Checked = true;
            }
            else
            {
                cbCombined.Checked = false;
            }

        }
        if (e.CommandName == "activate")
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            string ID = gvServiceWiseChargeMap.DataKeys[row.RowIndex]["scsmm"].ToString();
            Session["scsmm"] = ID;
            Session["Action"] = "Active";
            ConfirmMsg("Do you want to activate service charge map?");
        }
        if (e.CommandName == "deactivate")
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            string ID = gvServiceWiseChargeMap.DataKeys[row.RowIndex]["scsmm"].ToString();
            Session["scsmm"] = ID;
            Session["Action"] = "Deactive";
            ConfirmMsg("Do you want to deactivate service charge map?");
        }

    }
    protected void lbtnUpdate_Click(object sender, EventArgs e)
    {
        if (Validaion() == false)
        {
            return;
        }
        if (ddlFromState.SelectedValue == ddlToState.SelectedValue)
        {
            Errormsg("Same State Cannot Be Merged");
        }
        else
        {
            Session["Action"] = "U";
            ConfirmMsg("Do you Want to Update Service Wise Charge Map?");
        }

    }
    protected void lbtnSave_Click(object sender, EventArgs e)
    {
        if (Validaion() == false)
        {
            return;
        }
        if (ddlFromState.SelectedValue == ddlToState.SelectedValue)
        {
            Errormsg("Same State Cannot Be Merged");
            resetcontrols();
        }
        else
        {
            Session["Action"] = "S";
            ConfirmMsg("Do you Want to Save Service Wise Charge Map?");
        }

    }
    protected void lbtnReset_Click(object sender, EventArgs e)
    {
        resetcontrols();
    }
    protected void lbtnCancel_Click(object sender, EventArgs e)
    {
        resetcontrols();
    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        saveServiceWiseChargeMap();

    }
    protected void lbtnview_Click(object sender, EventArgs e)
    {
        InfoMsg("Here you can add state service wise charge map.");
    }
   
    protected void ddlFromState_SelectedIndexChanged(object sender, EventArgs e)//E1
    {

        Load();

    }

    #endregion

    protected void lbtndownload_Click(object sender, EventArgs e)
    {
        Errormsg("Coming Soon");
    }

    protected void Unnamed_Click(object sender, EventArgs e)
    {
        Errormsg("Coming Soon");
    }
}