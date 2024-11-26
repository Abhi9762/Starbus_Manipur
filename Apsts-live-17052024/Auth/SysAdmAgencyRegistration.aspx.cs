using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Npgsql;
using System.IO;
using System.Xml;
using System.Collections;

public partial class Auth_SysAdmAgencyRegistration : BasePage
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    string Mresult = "";

    protected void Page_Load(object sender, EventArgs e)
    { 
        lblSummary.Text = "Summary as on Date " + DateTime.Now.ToString("dd/MM/yyyy hh:ss tt");
        Session["_moduleName"] = "Agency Registration";
        if (IsPostBack == false)
        {
            Random randomclass = new Random();
            Session["rndNoCheck"] = randomclass.Next().ToString();
            hidtoken.Value = Session["rndNoCheck"].ToString();
            LoadItemsProvided();
            LoadServicesProvided();
            getAgenciesList();
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
    public void LoadItemsProvided()//M1
    {
        try
        {
            ddlItemsProvided.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_agencyitems");
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                ddlItemsProvided.DataSource = dt;
                ddlItemsProvided.DataTextField = "itemname";
                ddlItemsProvided.DataValueField = "itemid";
                ddlItemsProvided.DataBind();
            }
            else
            {
                ddlItemsProvided.Items.Insert(0, "SELECT");
                ddlItemsProvided.Items[0].Value = "0";
                ddlItemsProvided.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("SysAdmAgencyRegistration.aspx-0001", ex.Message.ToString());
            ddlItemsProvided.Items.Insert(0, "SELECT");
            ddlItemsProvided.Items[0].Value = "0";
            ddlItemsProvided.SelectedIndex = 0;
        }
    }
    public void LoadServicesProvided()//M2
    {
        try
        {
            ddlServicesProvided.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_agencyservices");
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                ddlServicesProvided.DataSource = dt;
                ddlServicesProvided.DataTextField = "servicename";
                ddlServicesProvided.DataValueField = "serviceid";
                ddlServicesProvided.DataBind();
            }
            else
            {
                ddlServicesProvided.Items.Insert(0, "SELECT");
                ddlServicesProvided.Items[0].Value = "0";
                ddlServicesProvided.SelectedIndex = 0;
            }
        }

        catch (Exception ex)
        {
            _common.ErrorLog("SysAdmAgencyRegistration.aspx-0002", ex.Message.ToString());
            ddlServicesProvided.Items.Insert(0, "SELECT");
            ddlServicesProvided.Items[0].Value = "0";
            ddlServicesProvided.SelectedIndex = 0;
        }
    }
    public void getAgenciesList()//M3
    {
        try
        {
            string searchText = tbSearch.Text.Trim();
            gvAgencies.Visible = false;
            pnlNoAgency.Visible = true;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_agencies");
            MyCommand.Parameters.AddWithValue("p_agency", searchText);
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                gvAgencies.DataSource = dt;
                gvAgencies.DataBind();
                lblTotalAgency.Text = dt.Rows[0]["total_agency"].ToString();
                lblActivateAgency.Text = dt.Rows[0]["act_agency"].ToString();
                lblDeactAgency.Text = dt.Rows[0]["deact_agency"].ToString();

                gvAgencies.Visible = true;
                pnlNoAgency.Visible = false;


            }
            else
            {
                gvAgencies.DataSource = dt;
                gvAgencies.DataBind();
            }

        }
        catch (Exception ex)
        {
            Errormsg("There is some error" + ex.Message);
            _common.ErrorLog("SysAdmAgencyRegistration.aspx-0003", ex.Message.ToString());
        }
    }
    public bool validAgency()
    {
        int msgcount = 0;
        string msg = "";

        if (_validation.IsValidString(tbAgencyName.Text, 1, tbAgencyName.MaxLength) == false)
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Enter Valid Agency Name.<br/>";
        }
        if (ddlItemsProvided.SelectedValue == "0")
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Select Items Provied by Agency.<br/>";
        }
        if (ddlServicesProvided.SelectedValue == "0")
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Select Services Provied by Agency.<br/>";
        }
        if (_validation.isValideMailID(tbEmailID.Text) == false)
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Enter Valid Agency Email ID.<br/>";
        }
        if (_validation.IsValidString(tbContact1.Text, 1, tbContact1.MaxLength) == false)
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Enter Valid Agency Contact No. 1.<br/>";
        }
        if (_validation.IsValidString(tbContact2.Text, 0, tbContact2.MaxLength) == false)
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Enter Valid Agency Contact No. 2.<br/>";
        }
        if (_validation.IsValidString(tbCPName.Text, 1, tbCPName.MaxLength) == false)
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Enter Valid Contact Person Name.<br/>";
        }
        if (_validation.isValideMailID(tbCPEmailID.Text) == false)
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Enter Valid Contact Person Email ID.<br/>";
        }
        if (_validation.IsValidString(tbCPMobileNo.Text, 1, tbCPMobileNo.MaxLength) == false)
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Enter Valid Contact Person Mobile No.<br/>";
        }
        if (_validation.IsValidString(tbCPLLNo.Text, 0, tbCPLLNo.MaxLength) == false)
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Enter Valid Contact Person Landline No.<br/>";
        }
        if (_validation.IsValidString(tbE1Name.Text, 0, tbE1Name.MaxLength) == false)
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Enter Valid Escalation Level-1 Name.<br/>";
        }
        if (tbE1EmailID.Text.Length > 0)
        {
            if (_validation.isValideMailID(tbE1EmailID.Text) == false)
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Enter Valid Escalation Level-1 Email ID.<br/>";
            }
        }
        if (_validation.IsValidString(tbE1MobileNo.Text, 0, tbE1MobileNo.MaxLength) == false)
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Enter Valid Escalation Level-1 Mobile No.<br/>";
        }
        if (_validation.IsValidString(tbE1LLNo.Text, 0, tbE1LLNo.MaxLength) == false)
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Enter Valid Escalation Level-1 Landline No.<br/>";
        }
        if (_validation.IsValidString(tbE2Name.Text, 0, tbE2Name.MaxLength) == false)
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Enter Valid Escalation Level-2 Name.<br/>";
        }
        if (tbE2EmailID.Text.Length > 0)
        {
            if (_validation.isValideMailID(tbE2EmailID.Text) == false)
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Enter Valid Escalation Level-2 Email ID.<br/>";
            }
        }
        if (_validation.IsValidString(tbE2MobileNo.Text, 0, tbE2MobileNo.MaxLength) == false)
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Enter Valid Escalation Level-2 Mobile No.<br/>";
        }
        if (_validation.IsValidString(tbE2LLNo.Text, 0, tbE2LLNo.MaxLength) == false)
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Enter Valid Escalation Level-2 Landline No.<br/>";
        }
        if (_validation.IsValidString(tbE3Name.Text, 0, tbE3Name.MaxLength) == false)
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Enter Valid Escalation Level-3 Name.<br/>";
        }
        if (tbE3EmailID.Text.Length > 0)
        {
            if (_validation.isValideMailID(tbE3EmailID.Text) == false)
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Enter Valid Escalation Level-3 Email ID.<br/>";
            }
        }
        if (_validation.IsValidString(tbE3MobileNo.Text, 0, tbE3MobileNo.MaxLength) == false)
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Enter Valid Escalation Level-3 Mobile No.<br/>";
        }
        if (_validation.IsValidString(tbE3LLNo.Text, 0, tbE3LLNo.MaxLength) == false)
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Enter Valid Escalation Level-3 Landline No.<br/>";
        }
        if (msgcount > 0)
        {
            Errormsg(msg);
            return false;
        }

        return true;
    }
    public void clearAgencyData()
    {
        lblAgencyHead.Text = "Add Agency";
        tbAgencyName.Text = "";
        LoadItemsProvided();
        LoadServicesProvided();
        tbEmailID.Text = "";
        tbContact1.Text = "";
        tbContact2.Text = "";
        tbAddress.Text = "";
        tbCPName.Text = "";
        tbCPEmailID.Text = "";
        tbCPMobileNo.Text = "";
        tbCPLLNo.Text = "";
        tbE1Name.Text = "";
        tbE1EmailID.Text = "";
        tbE1MobileNo.Text = "";
        tbE1LLNo.Text = "";
        tbE2Name.Text = "";
        tbE2EmailID.Text = "";
        tbE2MobileNo.Text = "";
        tbE2LLNo.Text = "";
        tbE3Name.Text = "";
        tbE3EmailID.Text = "";
        tbE3MobileNo.Text = "";
        tbE3LLNo.Text = "";
        lbtnSave.Visible = true;
        lbtnUpdate.Visible = false;
        lbtnReset.Visible = true;
        lbtnCancel.Visible = false;
        pnlAddAgency.Visible = true;
        pnlViewAgency.Visible = false;
        Session["_agencyID"] = "0";
        Session["_action"] = "";
    }
    private void saveAgency()//M4
    {
        try
        {
            Int32 agencyID = Convert.ToInt32(Session["_agencyID"].ToString());
            string providedItems = "";
            string providedServices = "";
            foreach (ListItem item in ddlItemsProvided.Items)
            {
                if (item.Selected)
                    providedItems += item.Value + ",";
            }
            if (providedItems.Length > 0)
            {
                providedItems = providedItems.Substring(0, providedItems.Length - 1);
            }

            foreach (ListItem item in ddlServicesProvided.Items)
            {
                if (item.Selected)
                    providedServices += item.Value + ",";
            }
            if (providedServices.Length > 0)
            {
                providedServices = providedServices.Substring(0, providedServices.Length - 1);
            }
            if (Session["_action"].ToString() == "S" || Session["_action"].ToString() == "U")
            {
                if (providedItems == "")
                {
                    Errormsg("Please Select Item Provided");
                    return;
                }
                if (providedServices == "")
                {
                    Errormsg("Please Select Service Provided");
                    return;
                }
            }


            string agencyname = tbAgencyName.Text.ToString();
            string agencyemail = tbEmailID.Text.ToString();
            string contact1 = tbContact1.Text.ToString();
            string contact2 = tbContact2.Text.ToString();
            string address = tbAddress.Text.ToString();
            string cpname = tbCPName.Text.ToString();
            string cpemail = tbCPEmailID.Text.ToString();
            string cpmobile = tbCPMobileNo.Text.ToString();
            string cpllno = tbCPLLNo.Text.ToString();
            string esc1name = tbE1Name.Text.ToString();
            string esc1email = tbE1EmailID.Text.ToString();
            string esc1mobile = tbE1MobileNo.Text.ToString();
            string esc1llno = tbE1LLNo.Text.ToString();
            string esc2name = tbE2Name.Text.ToString();
            string esc2email = tbE2EmailID.Text.ToString();
            string esc2mobile = tbE2MobileNo.Text.ToString();
            string esc2llno = tbE2LLNo.Text.ToString();
            string esc3name = tbE3Name.Text.ToString();
            string esc3email = tbE3EmailID.Text.ToString();
            string esc3mobile = tbE3MobileNo.Text.ToString();
            string esc3llno = tbE3LLNo.Text.ToString();
            string UpdatedBy = Session["_UserCode"].ToString();
            string IpAddress = HttpContext.Current.Request.UserHostAddress;
            string action = Session["_action"].ToString();
            string result = "";
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_agency_insertupdate");
            MyCommand.Parameters.AddWithValue("p_action", action);
            MyCommand.Parameters.AddWithValue("p_agencyid", agencyID);
            MyCommand.Parameters.AddWithValue("p_agencyname", agencyname);
            MyCommand.Parameters.AddWithValue("p_agencyitems", providedItems);
            MyCommand.Parameters.AddWithValue("p_agencyservices", providedServices);
            MyCommand.Parameters.AddWithValue("p_agencyemail", agencyemail);
            MyCommand.Parameters.AddWithValue("p_contact1", contact1);
            MyCommand.Parameters.AddWithValue("p_contact2", contact2);
            MyCommand.Parameters.AddWithValue("p_address", address);
            MyCommand.Parameters.AddWithValue("p_cpname", cpname);
            MyCommand.Parameters.AddWithValue("p_cpemail", cpemail);
            MyCommand.Parameters.AddWithValue("p_cpmobile", cpmobile);
            MyCommand.Parameters.AddWithValue("p_cpllno", cpllno);
            MyCommand.Parameters.AddWithValue("p_esc1name", esc1name);
            MyCommand.Parameters.AddWithValue("p_esc1email", esc1email);
            MyCommand.Parameters.AddWithValue("p_esc1mobile", esc1mobile);
            MyCommand.Parameters.AddWithValue("p_esc1llno", esc1llno);
            MyCommand.Parameters.AddWithValue("p_esc2name", esc2name);
            MyCommand.Parameters.AddWithValue("p_esc2email", esc2email);
            MyCommand.Parameters.AddWithValue("p_esc2mobile", esc2mobile);
            MyCommand.Parameters.AddWithValue("p_esc2llno", esc2llno);
            MyCommand.Parameters.AddWithValue("p_esc3name", esc3name);
            MyCommand.Parameters.AddWithValue("p_esc3email", esc3email);
            MyCommand.Parameters.AddWithValue("p_esc3mobile", esc3mobile);
            MyCommand.Parameters.AddWithValue("p_esc3llno", esc3llno);
            MyCommand.Parameters.AddWithValue("p_updatedby", UpdatedBy);
            MyCommand.Parameters.AddWithValue("p_ipaddress", IpAddress);

            result = bll.InsertAll(MyCommand);
            if (result == "Success")
            {
                if (Session["_action"].ToString() == "D")
                {
                    Successmsg("Agency Deactivated Successfully");
                }
                else if (Session["_action"].ToString() == "A")
                {
                    Successmsg("Agency Activated Successfully");
                }
                else if (Session["_action"].ToString() == "R")
                {
                    Successmsg("Agency Deleted Successfully");
                }
                else
                {
                    Successmsg("Agency created successfully");
                }
                Session["_agencyID"] = "";
                Session["_action"] = "";
                clearAgencyData();
                getAgenciesList();
            }
            else
            {
                Errormsg("There is some error.");
            }
        }
        catch (Exception ex)
        {
            Errormsg("There is some error.");
            _common.ErrorLog("SysAdmAgencyRegistration.aspx-0004", ex.Message.ToString());
        }
    }
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
    #endregion

    #region "Events"
    protected void lbtndwnldinst_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        string msg = "";
        msg = msg + "Coming Soon";
        InfoMsg(msg);
    }
    protected void gvAgencies_RowCommand(object sender, GridViewCommandEventArgs e)//E1
    {
        CsrfTokenValidate();
        try
        {
            int index = Convert.ToInt32(e.CommandArgument);
            Int32 agencyID;
            clearAgencyData();
            if (e.CommandName == "updateAgency")
            {
                agencyID = Convert.ToInt32(gvAgencies.DataKeys[index].Values["agencyid"]);
                Session["_agencyID"] = agencyID;
                lblAgencyHead.Text = "Update Agency Details of " + gvAgencies.DataKeys[index].Values["name"].ToString();
                tbAgencyName.Text = gvAgencies.DataKeys[index].Values["name"].ToString();
                tbEmailID.Text = gvAgencies.DataKeys[index].Values["email"].ToString();
                tbContact1.Text = gvAgencies.DataKeys[index].Values["contactno1"].ToString();
                tbContact2.Text = gvAgencies.DataKeys[index].Values["contactno2"].ToString();
                tbAddress.Text = gvAgencies.DataKeys[index].Values["address"].ToString();
                tbCPName.Text = gvAgencies.DataKeys[index].Values["contactname"].ToString();
                tbCPEmailID.Text = gvAgencies.DataKeys[index].Values["contactemail"].ToString();
                tbCPMobileNo.Text = gvAgencies.DataKeys[index].Values["contactmobile"].ToString();
                tbCPLLNo.Text = gvAgencies.DataKeys[index].Values["contactllno"].ToString();
                tbE1Name.Text = gvAgencies.DataKeys[index].Values["esc1name"].ToString();
                tbE1EmailID.Text = gvAgencies.DataKeys[index].Values["esc1email"].ToString();
                tbE1MobileNo.Text = gvAgencies.DataKeys[index].Values["esc1mobile"].ToString();
                tbE1LLNo.Text = gvAgencies.DataKeys[index].Values["esc1landlineno"].ToString();
                tbE2Name.Text = gvAgencies.DataKeys[index].Values["esc2name"].ToString();
                tbE2EmailID.Text = gvAgencies.DataKeys[index].Values["esc2email"].ToString();
                tbE2MobileNo.Text = gvAgencies.DataKeys[index].Values["esc2mobile"].ToString();
                tbE2LLNo.Text = gvAgencies.DataKeys[index].Values["esc2landlineno"].ToString();
                tbE3Name.Text = gvAgencies.DataKeys[index].Values["esc3name"].ToString();
                tbE3EmailID.Text = gvAgencies.DataKeys[index].Values["esc3email"].ToString();
                tbE3MobileNo.Text = gvAgencies.DataKeys[index].Values["esc3mobile"].ToString();
                tbE3LLNo.Text = gvAgencies.DataKeys[index].Values["esc3landlineno"].ToString();
                if (gvAgencies.DataKeys[index].Values["itemid"].ToString() != null)
                {
                    string Items = gvAgencies.DataKeys[index].Values["itemid"].ToString();
                    string[] TypeCode;
                    if ((Items == null & Items.Length != 0) == false)
                    {
                        TypeCode = Items.Split(new Char[] { ',' });
                        foreach (ListItem item in ddlItemsProvided.Items)
                        {
                            foreach (string str in TypeCode)
                            {
                                if (item.Value == str)
                                {
                                    item.Selected = true;
                                }
                            }
                        }
                    }
                }
                else
                {
                    foreach (ListItem item in ddlItemsProvided.Items)
                        item.Selected = false;
                }
                if (gvAgencies.DataKeys[index].Values["agencyserviceprovide"].ToString() != null)
                {
                    string Items = gvAgencies.DataKeys[index].Values["agencyserviceprovide"].ToString();
                    string[] TypeCode;
                    if ((Items == null & Items.Length != 0) == false)
                    {
                        TypeCode = Items.Split(new Char[] { ',' });
                        foreach (ListItem item in ddlServicesProvided.Items)
                        {
                            foreach (string str in TypeCode)
                            {
                                if (item.Value == str)
                                {
                                    item.Selected = true;
                                }
                            }
                        }
                    }
                }
                else
                {
                    foreach (ListItem item in ddlItemsProvided.Items)
                        item.Selected = false;
                }
                Session["_action"] = "U";
                lbtnSave.Visible = false;
                lbtnUpdate.Visible = true;
                lbtnReset.Visible = false;
                lbtnCancel.Visible = true;
                pnlViewAgency.Visible = false;
                pnlAddAgency.Visible = true;
            }
            else if (e.CommandName == "viewAgency")
            {
                agencyID = Convert.ToInt32(gvAgencies.DataKeys[index].Values["agencyid"]);
                Session["_agencyID"] = agencyID;

                if (gvAgencies.DataKeys[index].Values["name"].ToString() != "")
                {
                    lblAgencyName.Text = "Details Of " + gvAgencies.DataKeys[index].Values["name"].ToString();
                }
                if (gvAgencies.DataKeys[index].Values["email"].ToString() != "")
                {
                    lblEmailID.Text = gvAgencies.DataKeys[index].Values["email"].ToString();
                }
                if (gvAgencies.DataKeys[index].Values["contactno1"].ToString() != "")
                {
                    lblContact1.Text = gvAgencies.DataKeys[index].Values["contactno1"].ToString();
                }
                if (gvAgencies.DataKeys[index].Values["contactno2"].ToString() != "")
                {
                    lblContact1.Text = "," + gvAgencies.DataKeys[index].Values["contactno2"].ToString();
                }
                if (gvAgencies.DataKeys[index].Values["address"].ToString() != "")
                {
                    lblAddress.Text = gvAgencies.DataKeys[index].Values["address"].ToString();
                }
                if (gvAgencies.DataKeys[index].Values["contactname"].ToString() != "")
                {
                    lblCPName.Text = gvAgencies.DataKeys[index].Values["contactname"].ToString();
                }
                if (gvAgencies.DataKeys[index].Values["contactemail"].ToString() != "")
                {
                    lblCPEmailID.Text = gvAgencies.DataKeys[index].Values["contactemail"].ToString();
                }
                if (gvAgencies.DataKeys[index].Values["contactmobile"].ToString() != "")
                {
                    lblCPMobileNo.Text = gvAgencies.DataKeys[index].Values["contactmobile"].ToString();
                }
                if (gvAgencies.DataKeys[index].Values["contactllno"].ToString() != "")
                {
                    lblCPLLNo.Text = gvAgencies.DataKeys[index].Values["contactllno"].ToString();
                }
                if (gvAgencies.DataKeys[index].Values["esc1name"].ToString() != "")
                {
                    lblE1Name.Text = gvAgencies.DataKeys[index].Values["esc1name"].ToString();
                }
                if (gvAgencies.DataKeys[index].Values["esc1email"].ToString() != "")
                {
                    lblE1EmailID.Text = gvAgencies.DataKeys[index].Values["esc1email"].ToString();
                }
                if (gvAgencies.DataKeys[index].Values["esc1mobile"].ToString() != "")
                {
                    lblE1MobileNo.Text = gvAgencies.DataKeys[index].Values["esc1mobile"].ToString();
                }
                if (gvAgencies.DataKeys[index].Values["esc1landlineno"].ToString() != "")
                {
                    lblE1LLNo.Text = gvAgencies.DataKeys[index].Values["esc1landlineno"].ToString();
                }
                if (gvAgencies.DataKeys[index].Values["esc2name"].ToString() != "")
                {
                    lblE2Name.Text = gvAgencies.DataKeys[index].Values["esc2name"].ToString();
                }
                if (gvAgencies.DataKeys[index].Values["esc2email"].ToString() != "")
                {
                    lblE2EmailID.Text = gvAgencies.DataKeys[index].Values["esc2email"].ToString();
                }
                if (gvAgencies.DataKeys[index].Values["esc2mobile"].ToString() != "")
                {
                    lblE2MobileNo.Text = gvAgencies.DataKeys[index].Values["esc2mobile"].ToString();
                }
                if (gvAgencies.DataKeys[index].Values["esc2landlineno"].ToString() != "")
                {
                    lblE2LLNo.Text = gvAgencies.DataKeys[index].Values["esc2landlineno"].ToString();
                }
                if (gvAgencies.DataKeys[index].Values["esc3name"].ToString() != "")
                {
                    lblE3Name.Text = gvAgencies.DataKeys[index].Values["esc3name"].ToString();
                }
                if (gvAgencies.DataKeys[index].Values["esc3email"].ToString() != "")
                {
                    lblE3EmailID.Text = gvAgencies.DataKeys[index].Values["esc3email"].ToString();
                }
                if (gvAgencies.DataKeys[index].Values["esc3mobile"].ToString() != "")
                {
                    lblE3MobileNo.Text = gvAgencies.DataKeys[index].Values["esc3mobile"].ToString();
                }
                if (gvAgencies.DataKeys[index].Values["esc3landlineno"].ToString() != "")
                {
                    lblE3LLNo.Text = gvAgencies.DataKeys[index].Values["esc3landlineno"].ToString();
                }
                string AgencyItems = "";
                string AgencyServices = "";
                if (gvAgencies.DataKeys[index].Values["itemid"].ToString() != null)
                {
                    string Items = gvAgencies.DataKeys[index].Values["itemid"].ToString();
                    string[] TypeCode;
                    if ((Items == null & Items.Length != 0) == false)
                    {
                        TypeCode = Items.Split(new Char[] { ',' });
                        foreach (ListItem item in ddlItemsProvided.Items)
                        {
                            foreach (string str in TypeCode)
                            {
                                if (item.Value == str)
                                {
                                    AgencyItems = AgencyItems + item.Text.ToString() + ",";
                                }
                            }
                        }
                    }
                }
                AgencyItems = AgencyItems.Substring(0, AgencyItems.Length - 1);
                lblProvidedItems.Text = AgencyItems;
                if (gvAgencies.DataKeys[index].Values["agencyserviceprovide"].ToString() != null)
                {
                    string Items = gvAgencies.DataKeys[index].Values["agencyserviceprovide"].ToString();
                    string[] TypeCode;
                    if ((Items == null & Items.Length != 0) == false)
                    {
                        TypeCode = Items.Split(new Char[] { ',' });
                        foreach (ListItem item in ddlServicesProvided.Items)
                        {
                            foreach (string str in TypeCode)
                            {
                                if (item.Value == str)
                                {
                                    AgencyServices = AgencyServices + item.Text.ToString() + ",";
                                }
                            }
                        }
                    }
                }
                AgencyServices = AgencyServices.Substring(0, AgencyServices.Length - 1);
                lblProvidedServices.Text = AgencyServices;
                pnlViewAgency.Visible = true;
                pnlAddAgency.Visible = false;
            }
            else if (e.CommandName == "deleteAgency")
            {
                agencyID = Convert.ToInt32(gvAgencies.DataKeys[index].Values["agencyid"]);
                Session["_agencyID"] = agencyID;
                Session["_action"] = "delete";
                ConfirmMsg("Do you want to delete agency details?");
            }
            else if (e.CommandName == "activate")
            {
                agencyID = Convert.ToInt32(gvAgencies.DataKeys[index].Values["agencyid"]);
                Session["_agencyID"] = agencyID;
                Session["_action"] = "activate";
                ConfirmMsg("Do you want to activate agency details?");
            }
            else if (e.CommandName == "deactivate")
            {
                agencyID = Convert.ToInt32(gvAgencies.DataKeys[index].Values["agencyid"]);
                Session["_action"] = "deactivate";
                Session["_agencyID"] = agencyID;
                ConfirmMsg("Do you want to deactivate agency details?");
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("Agency Registration-E1", ex.Message.ToString());
        }
    }
    protected void gvAgencies_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbtnActivate = (LinkButton)e.Row.FindControl("lbtnActivate");
            LinkButton lbtnDiscontinue = (LinkButton)e.Row.FindControl("lbtnDiscontinue");
            LinkButton lbtnDelete = (LinkButton)e.Row.FindControl("lbtnDeletegency");
            Label lblGvAgencyItems = (Label)e.Row.FindControl("lblGvAgencyItems");
            DataRowView rowView = (DataRowView)e.Row.DataItem;
            lbtnActivate.Visible = false;
            lbtnDiscontinue.Visible = false;
            lbtnDelete.Visible = false;
            if (rowView["status"].ToString() == "A")
            {
                lbtnDiscontinue.Visible = true;
            }
            else if (rowView["status"].ToString() == "D")
            {
                lbtnActivate.Visible = true;
            }
            if (rowView["deleteyn"].ToString() == "Y")
            {
                lbtnDelete.Visible = true;
            }
            string AgencyItems = "";
            if (rowView["itemid"].ToString() != null)
            {
                string Items = rowView["itemid"].ToString();
                string[] TypeCode;
                if ((Items == null & Items.Length != 0) == false)
                {
                    TypeCode = Items.Split(new Char[] { ',' });
                    foreach (ListItem item in ddlItemsProvided.Items)
                    {
                        foreach (string str in TypeCode)
                        {
                            if (item.Value == str)
                            {
                                AgencyItems = AgencyItems + item.Text.ToString() + ",";
                            }
                        }
                    }
                }
                AgencyItems = AgencyItems.Substring(0, AgencyItems.Length - 1);
                lblGvAgencyItems.Text = AgencyItems;
            }

        }
    }
    protected void gvAgencies_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvAgencies.PageIndex = e.NewPageIndex;
        getAgenciesList();
    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (Session["_action"].ToString() == "S" || Session["_action"].ToString() == "U")
        {
            saveAgency();
        }
        else if (Session["_action"].ToString() == "activate")
        {
            Session["_action"] = "A";
            saveAgency();

        }
        else if (Session["_action"].ToString() == "deactivate")
        {
            Session["_action"] = "D";
            saveAgency();
        }
        else if (Session["_action"].ToString() == "delete")
        {
            Session["_action"] = "R";
            saveAgency();
        }
    }
    protected void saveAgency_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (validAgency() == false)
        {
            return;
        }
        Session["_agencyID"] = "0";
        Session["_action"] = "S";
        ConfirmMsg("Do you want to save Agency?");
    }
    protected void updateAgency_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Session["_action"] = "U";
        ConfirmMsg("Do you want to update Agency?");
    }
    protected void resetAgency_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        clearAgencyData();
    }
    protected void lbtnSearchAgency_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        getAgenciesList();
        if (gvAgencies.Rows.Count == 0)
        {
            divNoRecord.Visible = false;
            divNoSearchRecord.Visible = true;
        }
    }
    protected void lbtnResetAgency_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        tbSearch.Text = "";
        getAgenciesList();
    }
    protected void lbtnview_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        string msg = "";
        msg = msg + "1.  Agencies can be register here.<br/>";
        msg = msg + "2.  Multiple Sevices and Items can be provided by agency<br/>";
        msg = msg + "3.  If Item of that agency is registered in the system then it cannot be deleted.<br/>";
        msg = msg + "4.  It can be Activated/Deactivated only if its not in use.<br/>";
        msg = msg + "5.  Top panel shows the Summary of Agencies<br/>";
        InfoMsg(msg);
    }
    #endregion


}
