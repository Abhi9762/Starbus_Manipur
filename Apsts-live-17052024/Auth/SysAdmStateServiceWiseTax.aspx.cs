using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_SysAdmServicewiseTax : BasePage
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
        Session["_moduleName"] = "State Service wise Tax";
        if (IsPostBack == false)
        {
            hdnTotSlabs.Value = "0";
            loadStates();
            loadServicetype();
            getServiceWiseTaxList();
            TaxCount();
            loadTaxBasedOn();

        }
    }

    #region "Methods"
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
    public void loadTaxBasedOn()//M0
    {
        try
        {
            ddlTaxBasedOn.Items.Clear();

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_fr_ssw_tax_based_get");
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                ddlTaxBasedOn.DataSource = dt;
                ddlTaxBasedOn.DataTextField = "taxbased_type";
                ddlTaxBasedOn.DataValueField = "taxbased_id";
                ddlTaxBasedOn.DataBind();


            }
            ddlTaxBasedOn.Items.Insert(0, "SELECT");
            ddlTaxBasedOn.Items[0].Value = "0";
            ddlTaxBasedOn.SelectedIndex = 0;

        }
        catch (Exception ex)
        {
            ddlTaxBasedOn.Items.Insert(0, "SELECT");
            ddlTaxBasedOn.Items[0].Value = "0";
            ddlTaxBasedOn.SelectedIndex = 0;

            _common.ErrorLog("ServicewiseTax-M0", ex.Message.ToString());
        }
    }
    public void loadStates()//M1
    {
        try
        {
            ddlState.Items.Clear();

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.f_get_stateslist");
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                ddlState.DataSource = dt;
                ddlState.DataTextField = "stname";
                ddlState.DataValueField = "stcode";
                ddlState.DataBind();
                ddlSState.DataSource = dt;
                ddlSState.DataTextField = "stname";
                ddlSState.DataValueField = "stcode";
                ddlSState.DataBind();


            }
            ddlState.Items.Insert(0, "SELECT");
            ddlState.Items[0].Value = "0";
            ddlState.SelectedIndex = 0;
            ddlSState.Items.Insert(0, "SELECT");
            ddlSState.Items[0].Value = "0";
            ddlSState.SelectedIndex = 0;

        }
        catch (Exception ex)
        {
            ddlState.Items.Insert(0, "SELECT");
            ddlState.Items[0].Value = "0";
            ddlState.SelectedIndex = 0;
            ddlSState.Items.Insert(0, "SELECT");
            ddlSState.Items[0].Value = "0";
            ddlSState.SelectedIndex = 0;

            _common.ErrorLog("ServicewiseTax-M1", ex.Message.ToString());
        }
    }
    public void loadServicetype()//M2
    {
        try
        {
            ddlServiceType.Items.Clear();
            ddlSServiceType.Items.Clear();

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_servicetype_getlist");
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                ddlServiceType.DataSource = dt;
                ddlServiceType.DataTextField = "servicetype_name_en";
                ddlServiceType.DataValueField = "srtpid";
                ddlServiceType.DataBind();
                ddlSServiceType.DataSource = dt;
                ddlSServiceType.DataTextField = "servicetype_name_en";
                ddlSServiceType.DataValueField = "srtpid";
                ddlSServiceType.DataBind();


            }
            ddlServiceType.Items.Insert(0, "SELECT");
            ddlServiceType.Items[0].Value = "0";
            ddlServiceType.SelectedIndex = 0;
            ddlSServiceType.Items.Insert(0, "SELECT");
            ddlSServiceType.Items[0].Value = "0";
            ddlSServiceType.SelectedIndex = 0;

        }
        catch (Exception ex)
        {
            ddlServiceType.Items.Insert(0, "SELECT");
            ddlServiceType.Items[0].Value = "0";
            ddlServiceType.SelectedIndex = 0;
            ddlSServiceType.Items.Insert(0, "SELECT");
            ddlSServiceType.Items[0].Value = "0";
            ddlSServiceType.SelectedIndex = 0;

            _common.ErrorLog("ServicewiseTax-M2", ex.Message.ToString());
        }
    }
    private void TaxCount()//M8
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_service_wise");
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                lblTotalFare.Text = dt.Rows[0]["total"].ToString();
                lblSlab.Text = dt.Rows[0]["slab"].ToString();
                lblFare.Text = dt.Rows[0]["Pkm"].ToString();

            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("ServiceWiseTax-M8", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
        }
    }
    public void insertStab_Table() //M10
    {
        try
        {

          
            long swtr = Convert.ToInt64(0);

            string State = ddlState.SelectedValue;
            string value = tbTaxValue.Text;
            string date = tbDate.Text;
            string ServiceType = ddlServiceType.SelectedValue;
            string FareType = "";
            string Date = tbDate.Text;
            string taxbasedOn = ddlTaxBasedOn.SelectedValue;
            string taxType = ddlTaxType.SelectedValue;
            string UpdatedBy = Session["_UserCode"].ToString();
            string IpAddress = HttpContext.Current.Request.UserHostAddress;
            if (tbFromkm.Text == "")
                tbFromkm.Text = "0";
            if (tbtokm.Text == "")
                tbtokm.Text = "0";
            string fromkm = tbFromkm.Text, tokm = tbtokm.Text;



            if (Session["Action"].ToString() == "Deactive" || Session["Action"].ToString() == "Active" || Session["Action"].ToString() == "U")
            {
                State = Session["state_id_"].ToString();
                ServiceType = Session["service_type_id_"].ToString();
                taxType = Session["tax_type_hill_plain_"].ToString();
                swtr= Convert.ToInt64( Session["swtrid"].ToString());
               
            }


            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_fr_ssw_tax_insert_new");
            MyCommand.Parameters.AddWithValue("p_swtr", swtr);
            MyCommand.Parameters.AddWithValue("p_action", Session["Action"].ToString());
          
           
            MyCommand.Parameters.AddWithValue("p_statecode", State);
            MyCommand.Parameters.AddWithValue("p_taxbased", taxbasedOn);
            MyCommand.Parameters.AddWithValue("p_value", value);
            MyCommand.Parameters.AddWithValue("p_date", Date);
            MyCommand.Parameters.AddWithValue("p_tax", taxType);
            MyCommand.Parameters.AddWithValue("p_srtp", Convert.ToInt16(ServiceType));
            MyCommand.Parameters.AddWithValue("p_fromkm", Convert.ToInt16(fromkm));
            MyCommand.Parameters.AddWithValue("p_tokm", Convert.ToInt16(tokm));
            MyCommand.Parameters.AddWithValue("p_updatedby", UpdatedBy);
            MyCommand.Parameters.AddWithValue("p_ipaddress", IpAddress);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows[0]["resultstr"].ToString() == "DONE")
                {
                    if (Session["Action"].ToString() == "Active")
                    {
                        Successmsg("State Service Wise Tax Activated");
                    }
                    if (Session["Action"].ToString() == "Deactive")
                    {
                        Successmsg("State Service Wise Tax Deactivated");
                    }
                    if (Session["Action"].ToString() == "S")
                    {
                        Successmsg("State Service Wise Tax Saved");
                    }
                    if (Session["Action"].ToString() == "U")
                    {
                        Successmsg("State Service Wise Tax Updated");
                    }
                    getServiceWiseTaxList();
                    TaxCount();
                    resetControls();
                }
                else if (dt.Rows[0]["resultstr"].ToString() == "ALREADY")
                {
                    Errormsg("State service wise tax already save for selected 'State', 'Service Type' and 'Tax Type'");
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
                Errormsg(commonerror + dt.TableName);
                _common.ErrorLog("ServiceWiseTax-M10", dt.TableName);
            }

        }
        catch (Exception ex)
        {
            Errormsg(commonerror + ex.Message.ToString());
            _common.ErrorLog("ServiceWiseTax-M10", ex.Message.ToString());
        }
    }
    public void getServiceWiseTaxList()//M11
    {
        try
        {
            string State = ddlSState.SelectedValue;
            string ServiceType = ddlSServiceType.SelectedValue;
            gvServiceWiseTax.Visible = false;
            pnlNoFare.Visible = true;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_fr_ssw_tax_list_get");
            MyCommand.Parameters.AddWithValue("p_statecode", State);
            MyCommand.Parameters.AddWithValue("p_srtp", Convert.ToInt16(ServiceType));
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                gvServiceWiseTax.DataSource = dt;
                gvServiceWiseTax.DataBind();
                gvServiceWiseTax.Visible = true;
                pnlNoFare.Visible = false;

            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("ServiceWiseTax-M11", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
        }
    }
    private Boolean Validaion(TextBox FromKM, TextBox ToKM, TextBox Value)
    {
        try
        {
            int count = 0;
            string msg = "";


            if (ddlState.SelectedValue == "0")
            {
                count++;
                msg = msg + count + ". Select State <br/>";
            }
            if (ddlServiceType.SelectedValue == "0")
            {
                count++;
                msg = msg + count + ". Select Service Type <br/>";
            }
           
           
            if (tbDate.Text == "")
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
    private Boolean ValidaionTax()
    {
        try
        {
            int count = 0;
            string msg = "";


            if (ddlState.SelectedValue == "0")
            {
                count++;
                msg = msg + count + ". Select State <br/>";
            }
            if (ddlServiceType.SelectedValue == "0")
            {
                count++;
                msg = msg + count + ". Select Service Type <br/>";
            }
           
            if (ddlTaxBasedOn.SelectedValue == "0")
            {
                count++;
                msg = msg + count + ". Select Tax Based On <br/>";
            }
            if (ddlTaxType.SelectedValue == "0")
            {
                count++;
                msg = msg + count + ". Select Tax Type <br/>";
            }
            
            if (tbDate.Text == "")
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
            Errormsg("Please check values.");
            return false;
        }
    }
    
  

    #endregion

    #region "Events"
    protected void lbtnview_Click(object sender, EventArgs e)
    {
        InfoMsg("Coming Soon");

    }
    protected void lbtnUpdate_Click(object sender, EventArgs e)
    {
        if (ValidaionTax() == false)
        {
            return;
        }
        Session["Action"] = "U";
        ConfirmMsg("Do you want to save state service wise Tax?");
    }
    protected void lbtnSave_Click(object sender, EventArgs e)
    {
        if (ValidaionTax() == false)
        {
            return;
        }
        
        Session["Action"] = "S";
        ConfirmMsg("Do you want to save state service wise Tax?");

    }
    protected void lbtnReset_Click(object sender, EventArgs e)
    {
        resetControls();
    }

    private void resetControls()
    {
        ddlState.Enabled = true;
        ddlState.SelectedValue = "0";
        ddlServiceType.Enabled = true;
        ddlServiceType.SelectedValue = "0";
        ddlTaxBasedOn.Enabled = true;
        ddlTaxBasedOn.SelectedValue = "0";
        ddlTaxType.Enabled = true;
        ddlTaxType.SelectedValue = "0";
        tbFromkm.Text = "";
        tbtokm.Text = "";
        tbTaxValue.Text = "";
        tbDate.Text = "";
        lbtnSave.Visible = true;
        lbtnUpdate.Visible = false;
    }

    protected void lbtnCancel_Click(object sender, EventArgs e)
    {
        resetControls();

    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        insertStab_Table();
    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    protected void lbtnAddNewSlab_Click(object sender, EventArgs e)
    {
        
    }
    protected void gvServiceWiseTax_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbtnUpdateTax = (LinkButton)e.Row.FindControl("lbtnUpdateTax");
            LinkButton lbtnActivate = (LinkButton)e.Row.FindControl("lbtnActivate");
            LinkButton lbtnDiscontinue = (LinkButton)e.Row.FindControl("lbtnDiscontinue");
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
               // lbtnUpdateTax.Visible = false;
            }


        }

    }
    protected void gvServiceWiseTax_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvServiceWiseTax.PageIndex = e.NewPageIndex;
        getServiceWiseTaxList();

    }
    protected void gvServiceWiseTax_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "updateTax")
        {
            lblTaxHead.Visible = false;
            lblTaxUpdate.Visible = true;
            lbtnUpdate.Visible = true;
            lbtnSave.Visible = false;
            lbtnReset.Visible = false;
            lbtnCancel.Visible = true;
            ddlState.Enabled = false;

            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            string state = gvServiceWiseTax.DataKeys[row.RowIndex]["stateid"].ToString();
            string Servicetype = gvServiceWiseTax.DataKeys[row.RowIndex]["srtpid"].ToString();
            string TaxType = gvServiceWiseTax.DataKeys[row.RowIndex]["taxtype"].ToString();
            string ID = gvServiceWiseTax.DataKeys[row.RowIndex]["id"].ToString();
            Session["swtrid"] = ID;
            Session["state_id_"] = state;
            Session["service_type_id_"] = Servicetype;
            Session["tax_type_hill_plain_"] = TaxType;
            lblTaxUpdate.Text = "Update State Service Wise Tax-  (" + gvServiceWiseTax.DataKeys[row.RowIndex]["state_name"].ToString() + ")";
          
            string TaxBasedon = gvServiceWiseTax.DataKeys[row.RowIndex]["taxbasedon"].ToString();
            string FareType = gvServiceWiseTax.DataKeys[row.RowIndex]["faretype"].ToString();
            
            string Date = gvServiceWiseTax.DataKeys[row.RowIndex]["eff"].ToString();
            string fromkm= gvServiceWiseTax.DataKeys[row.RowIndex]["fromkm"].ToString();
            string tokm = gvServiceWiseTax.DataKeys[row.RowIndex]["tokm"].ToString();
            string value = gvServiceWiseTax.DataKeys[row.RowIndex]["val"].ToString();
            ddlState.SelectedValue = state;
           
            ddlServiceType.SelectedValue = Servicetype;
            ddlTaxBasedOn.SelectedValue = TaxBasedon;
            ddlTaxType.SelectedValue = TaxType;
            tbDate.Text = Date;
            tbFromkm.Text = fromkm;
            tbtokm.Text = tokm;
            tbTaxValue.Text = value;
            ddlState.Enabled = false;
            ddlServiceType.Enabled = false;
           
            ddlTaxBasedOn.Enabled = false;
            ddlTaxType.Enabled = false;
            Session["Action"] = "U";



        }
        if (e.CommandName == "activate")
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;

            string state = gvServiceWiseTax.DataKeys[row.RowIndex]["stateid"].ToString();
            string Servicetype = gvServiceWiseTax.DataKeys[row.RowIndex]["srtpid"].ToString();
            string TaxType = gvServiceWiseTax.DataKeys[row.RowIndex]["taxtype"].ToString();
            string ID = gvServiceWiseTax.DataKeys[row.RowIndex]["id"].ToString();
            Session["swtrid"] = ID;
            Session["state_id_"] = state;
            Session["service_type_id_"] = Servicetype;
            Session["tax_type_hill_plain_"] = TaxType;

            Session["Action"] = "Active";
            ConfirmMsg("Do you want to activate service fare?");
        }
        if (e.CommandName == "deactivate")
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            string state = gvServiceWiseTax.DataKeys[row.RowIndex]["stateid"].ToString();
            string Servicetype = gvServiceWiseTax.DataKeys[row.RowIndex]["srtpid"].ToString();
            string TaxType = gvServiceWiseTax.DataKeys[row.RowIndex]["taxtype"].ToString();
            string ID = gvServiceWiseTax.DataKeys[row.RowIndex]["id"].ToString();
            Session["swtrid"] = ID;
            Session["state_id_"] = state;
            Session["service_type_id_"] = Servicetype;
            Session["tax_type_hill_plain_"] = TaxType;
           
            Session["Action"] = "Deactive";
            ConfirmMsg("Do you want to deactivate service fare?");
        }

    }
    protected void lbtnSearch_Click(object sender, EventArgs e)
    {
        getServiceWiseTaxList();
    }
    protected void lbtnResetSearch_Click(object sender, EventArgs e)
    {
        ddlSServiceType.SelectedValue = "0";
        ddlSState.SelectedValue = "0";
        getServiceWiseTaxList();
    }
    
    protected void lbtnAddNewSlab_Click1(object sender, EventArgs e)
    {

        //  Errormsg("Please Enter Slab Values");
    }

    #endregion

}