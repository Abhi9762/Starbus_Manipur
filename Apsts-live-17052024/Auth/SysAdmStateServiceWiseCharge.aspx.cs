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

public partial class Auth_SysAdminStateServiceWiseCharge : System.Web.UI.Page
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    string Mresult = "";
    string commonerror = "There is some error. Please contact the administrator or try again after some time.";
    protected void Page_Init(object sender, System.EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.AddHeader("Cache-control", "no-store, must-revalidate,private,no-cache");
        Response.AddHeader("PRAGMA", "NO-Cache");
        Response.Expires = -1;
        Response.Expires = 0;
        Response.Cache.SetNoStore();
        Response.Cache.SetExpires(DateTime.Now.AddDays(-2));
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        lblSummary.Text = "Summary as on Date " + DateTime.Now.ToString("dd/MM/yyyy hh:ss tt");
        Session["_moduleName"] = "State service wise Charge";
        if (IsPostBack == false)
        {
            hdnTotSlabs.Value = "0";
            loadStates();
            loadServicetype();
            StSvctotalCount();
            SetInitialRow();
            getStatesvcCharge();
        }
    }

    #region "Methods"
    public void loadStates()//M1
    {
        try
        {
            ddlState.Items.Clear();
            ddlstateSearch.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.f_get_stateslist");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlState.DataSource = dt;
                    ddlState.DataTextField = "stname";
                    ddlState.DataValueField = "stcode";
                    ddlState.DataBind();

                    ddlstateSearch.DataSource = dt;
                    ddlstateSearch.DataTextField = "stname";
                    ddlstateSearch.DataValueField = "stcode";
                    ddlstateSearch.DataBind();

                }
                else
                {
                    _common.ErrorLog("sysAdminServiceWiseCharge-M1", dt.TableName);
                    Errormsg(dt.TableName);
                }
                ddlState.Items.Insert(0, "SELECT");
                ddlState.Items[0].Value = "0";
                ddlState.SelectedIndex = 0;

                ddlstateSearch.Items.Insert(0, "SELECT");
                ddlstateSearch.Items[0].Value = "0";
                ddlstateSearch.SelectedIndex = 0;
            }

        }
        catch (Exception ex)
        {
            _common.ErrorLog("sysAdminServiceWiseCharge-M1", ex.Message.ToString());
            Errormsg(ex.Message);
        }
    }
    public void loadServicetype()//M2
    {
        try
        {
            ddlServiceType.Items.Clear();

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_servicetype_getlist");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlServiceType.DataSource = dt;
                    ddlServiceType.DataTextField = "servicetype_name_en";
                    ddlServiceType.DataValueField = "srtpid";
                    ddlServiceType.DataBind();

                    ddlServiceTypeSearch.DataSource = dt;
                    ddlServiceTypeSearch.DataTextField = "servicetype_name_en";
                    ddlServiceTypeSearch.DataValueField = "srtpid";
                    ddlServiceTypeSearch.DataBind();
                }
                else
                {
                    _common.ErrorLog("sysAdminServiceWiseCharge-M2", dt.TableName);
                    Errormsg(dt.TableName);
                }
                ddlServiceType.Items.Insert(0, "SELECT");
                ddlServiceType.Items[0].Value = "0";
                ddlServiceType.SelectedIndex = 0;

                ddlServiceTypeSearch.Items.Insert(0, "SELECT");
                ddlServiceTypeSearch.Items[0].Value = "0";
                ddlServiceTypeSearch.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("sysAdminServiceWiseCharge-M2", ex.Message.ToString());
            Errormsg(ex.Message);
        }
    }
    public void loadStateFareType()//M3
    {
        try
        {
            string fareType = ddlState.SelectedValue.ToString();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_statewisefaretype");
            MyCommand.Parameters.AddWithValue("p_state", fareType);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlFareType.SelectedValue = dt.Rows[0]["fare"].ToString();

                }
                else
                {
                    _common.ErrorLog("sysAdminServiceWiseCharge-M3", dt.TableName);
                    Errormsg(dt.TableName);
                }

            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("sysAdminServiceWiseCharge-M3", ex.Message.ToString());
            Errormsg(ex.Message);
        }
    }
    private bool isvalidvalueUpdate()//M4
    {
        try
        {
            int msgcount = 0;
            string msg = "";
            if (_validation.IsValidInteger(ddlState.SelectedValue, 1, 20) == false)
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Select Valid State.<br/>";
            }
            if (_validation.IsValidInteger(ddlServiceType.SelectedValue, 1, 20) == false)
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Select Valid Service Type.<br/>";
            }
            if (ddlFareType.SelectedValue == "0")
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Select Valid Fare Type.<br/>";
            }

            if (ddlFareType.SelectedValue == "P")
            {
                if (tbAccedentSc.Text == "")
                {
                    msgcount = msgcount + 1;
                    msg = msg + msgcount.ToString() + ". Enter Accident Surcharge.<br/>";
                }
                if (_validation.isValideDecimalNumber(tbAccedentSc.Text.ToString(), 0, 5) == false)
                {
                    msgcount = msgcount + 1;
                    msg = msg + msgcount.ToString() + ". Enter Valid Accident Surcharge.<br/>";
                }
                if (tbPassengerSC.Text == "")
                {
                    msgcount = msgcount + 1;
                    msg = msg + msgcount.ToString() + ". Enter Passenger Surcharge.<br/>";
                }
                if (_validation.isValideDecimalNumber(tbPassengerSC.Text.ToString(), 0, 5) == false)
                {
                    msgcount = msgcount + 1;
                    msg = msg + msgcount.ToString() + ". Enter Valid Passenger Surcharge.<br/>";
                }
                if (tbITSc.Text == "")
                {
                    msgcount = msgcount + 1;
                    msg = msg + msgcount.ToString() + ". Enter IT Surcharge.<br/>";
                }
                if (_validation.isValideDecimalNumber(tbITSc.Text.ToString(), 0, 5) == false)
                {
                    msgcount = msgcount + 1;
                    msg = msg + msgcount.ToString() + ". Enter Valid IT Surcharge.<br/>";
                }
                if (tbOtherSc.Text == "")
                {
                    msgcount = msgcount + 1;
                    msg = msg + msgcount.ToString() + ". Enter Other Surcharge.<br/>";
                }
                if (_validation.isValideDecimalNumber(tbOtherSc.Text.ToString(), 0, 5) == false)
                {
                    msgcount = msgcount + 1;
                    msg = msg + msgcount.ToString() + ". Enter Valid Other Surcharge.<br/>";
                }

            }
            if (tbDate.Text == "")
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Enter Effective from Date. <br/>";
            }
            //if (_validation.IsValidateDateTime(tbDate.Text) == true)
            //{
            //    DateTime EffectiveDate = DateTime.ParseExact(tbDate.Text, "dd/MM/yyyy", null);
            //    if (EffectiveDate > DateTime.Now.Date)
            //    {
            //        msgcount = msgcount + 1;
            //        msg = msg + msgcount.ToString() + ". Enter Valid Effective from Date greater than Today Date.<br/>";
            //    }
            //}
            if (msgcount > 0)
            {
                Errormsg(msg);
                return false;
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("sysAdminServiceWiseCharge-M4", ex.Message.ToString());
            Errormsg(ex.Message);
            return false;
        }
        return true;
    }
    private Boolean ValidaionSlab(string from, string to, string accendent, string passenger, string it, string other)//M5
    {
        try
        {
            int count = 0;
            string msg = "";

            if (ddlFareType.SelectedValue == "S")
            {
                if (_validation.IsValidString(from, 1, 4) == false)
                {
                    count = count + 1;
                    msg = msg + count.ToString() + ". Enter From Km.<br/>";
                }
                if (_validation.IsValidString(to, 1, 4) == false)
                {
                    count = count + 1;
                    msg = msg + count.ToString() + ". Enter To Km.<br/>";
                }

                int FromKM = Convert.ToInt32(from);
                int ToKM = Convert.ToInt32(to);
                if (FromKM > ToKM)
                {
                    count = count + 1;
                    msg = msg + count.ToString() + ". To Km value must be greater than From.<br/>";
                }

                if (_validation.isValideDecimalNumber(accendent, 1, 5) == false)
                {
                    count = count + 1;
                    msg = msg + count.ToString() + ". Enter Accendent Surcharges.<br/>";
                }
                if (_validation.isValideDecimalNumber(passenger, 1, 5) == false)
                {
                    count = count + 1;
                    msg = msg + count.ToString() + ". Enter passenger Surcharges.<br/>";
                }
                if (_validation.isValideDecimalNumber(it, 1, 5) == false)
                {
                    count = count + 1;
                    msg = msg + count.ToString() + ". Enter It Surcharges.<br/>";
                }
                if (_validation.isValideDecimalNumber(other, 1, 5) == false)
                {
                    count = count + 1;
                    msg = msg + count.ToString() + ". Enter Other Surcharges.<br/>";
                }
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
            _common.ErrorLog("sysAdminServiceWiseCharge-M5", ex.ToString());
            Errormsg("There is invalid values in slab. Please enter valid values.");
            return false;
        }
    }
    public DataTable getSlab_table()
    {

        // Create a new DataTable.
        DataTable dtTimeTable = new DataTable("timeTable");
        dtTimeTable.Columns.Add("fr_km", typeof(string));
        dtTimeTable.Columns.Add("to_km", typeof(string));
        dtTimeTable.Columns.Add("adnt_s_charge", typeof(string));
        dtTimeTable.Columns.Add("pngr_s_charge", typeof(string));
        dtTimeTable.Columns.Add("it_s_charge", typeof(string));
        dtTimeTable.Columns.Add("othr_s_charge", typeof(string));
        string tbFromkm, tbtokm, tbAccedentSCSlab, tbPassengerSCslab, tbItScSlab, tbOtherScSlab;
        // Loop through the GridView and copy rows.
        foreach (GridViewRow row in gvaddSlab.Rows)
        {
            TextBox tbFromkm1 = row.FindControl("tbFromkm1") as TextBox;
            tbFromkm = tbFromkm1.Text;

            TextBox tbtokm1 = row.FindControl("tbtokm1") as TextBox;
            tbtokm = tbtokm1.Text;

            TextBox tbAccedentSCSlab1 = row.FindControl("tbAccedentSCSlab1") as TextBox;
            tbAccedentSCSlab = tbAccedentSCSlab1.Text;

            TextBox tbPassengerSCslab1 = row.FindControl("tbPassengerSCslab1") as TextBox;
            tbPassengerSCslab = tbPassengerSCslab1.Text;

            TextBox tbItScSlab1 = row.FindControl("tbItScSlab1") as TextBox;
            tbItScSlab = tbItScSlab1.Text;

            TextBox tbOtherScSlab1 = row.FindControl("tbOtherScSlab1") as TextBox;
            tbOtherScSlab = tbOtherScSlab1.Text;
            if (!(tbFromkm.Trim().Length <= 0 || tbtokm.Trim().Length <= 0 || tbAccedentSCSlab.Trim().Length <= 0 || tbPassengerSCslab.Trim().Length <= 0 || tbItScSlab.Trim().Length <= 0 || tbOtherScSlab.Trim().Length <= 0))
            {
                dtTimeTable.Rows.Add(tbFromkm, tbtokm, tbAccedentSCSlab, tbPassengerSCslab, tbItScSlab, tbOtherScSlab);
            }
        }

        return dtTimeTable;
    }
    public void makeDataTable(int rownumber)
    {

        // Create a new DataTable.
        DataTable dtTimeTable = new DataTable("timeTable");
        dtTimeTable.Columns.Add("fr_km", typeof(string));
        dtTimeTable.Columns.Add("to_km", typeof(string));
        dtTimeTable.Columns.Add("adnt_s_charge", typeof(string));
        dtTimeTable.Columns.Add("pngr_s_charge", typeof(string));
        dtTimeTable.Columns.Add("it_s_charge", typeof(string));
        dtTimeTable.Columns.Add("othr_s_charge", typeof(string));
        string frmkm, tokm, accendent, passenger, it, other;

        // Loop through the GridView and copy rows.
        foreach (GridViewRow row in gvaddSlab.Rows)
        {
            if (rownumber >= 0)
            {
                if (row.RowIndex != rownumber)
                {

                    TextBox tbFrom = row.FindControl("tbFromkm1") as TextBox;
                    frmkm = tbFrom.Text;

                    TextBox tbTo = row.FindControl("tbtokm1") as TextBox;
                    tokm = tbTo.Text;

                    TextBox tbAccedent = row.FindControl("tbAccedentSCSlab1") as TextBox;
                    accendent = tbAccedent.Text;

                    TextBox tbPassenger = row.FindControl("tbPassengerSCslab1") as TextBox;
                    passenger = tbPassenger.Text;

                    TextBox tbIt = row.FindControl("tbItScSlab1") as TextBox;
                    it = tbIt.Text;

                    TextBox tbOther = row.FindControl("tbOtherScSlab1") as TextBox;
                    other = tbOther.Text;

                    dtTimeTable.Rows.Add(frmkm, tokm, accendent, passenger, it, other);
                }
            }
            if (rownumber == -1)
            {
                TextBox tbFrom = row.FindControl("tbFromkm1") as TextBox;
                frmkm = tbFrom.Text;

                TextBox tbTo = row.FindControl("tbtokm1") as TextBox;
                tokm = tbTo.Text;

                TextBox tbAccedent = row.FindControl("tbAccedentSCSlab1") as TextBox;
                accendent = tbAccedent.Text;

                TextBox tbPassenger = row.FindControl("tbPassengerSCslab1") as TextBox;
                passenger = tbPassenger.Text;

                TextBox tbIt = row.FindControl("tbItScSlab1") as TextBox;
                it = tbIt.Text;

                TextBox tbOther = row.FindControl("tbOtherScSlab1") as TextBox;
                other = tbOther.Text;

                dtTimeTable.Rows.Add(frmkm, tokm, accendent, passenger, it, other);
            }
        }
        gvaddSlab.DataSource = dtTimeTable;
        gvaddSlab.DataBind();
        //return dtTimeTable;
    }
    private void SaveValue()//M6
    {
        try
        {
            StringWriter swStringWriter = new StringWriter();
            using (swStringWriter)
            {
                var dt = getSlab_table();
                dt.TableName = "timeTable";
                DataSet ds = new DataSet("Root");
                ds.Tables.Add(dt);
                ds.WriteXml(swStringWriter);

                decimal AccedentsSC = 0, PassengerSc = 0, ITSc = 0, OtherSc = 0;
                long skcrid = Convert.ToInt64(0);
                string ipaddress = HttpContext.Current.Request.UserHostAddress;
                string UpdatedBy = Session["_UserCode"].ToString();
                string Action = Session["Action"].ToString();
                string Status = "A";
                string state = ddlState.SelectedValue.ToString();
                long serviceType = Convert.ToInt64(ddlServiceType.SelectedValue.ToString());

                if (Session["Action"].ToString() == "D")
                {
                    state = Session["state_Id_"].ToString();
                    serviceType = Convert.ToInt64(Session["service_type_Id_"].ToString());
                    Status = "D";
                }
                if (Session["Action"].ToString() == "A")
                {
                    state = Session["state_Id_"].ToString();
                    serviceType = Convert.ToInt64(Session["service_type_Id_"].ToString());
                    Status = "A";
                }

                string FareType = ddlFareType.SelectedValue;

                if (!String.IsNullOrEmpty(tbAccedentSc.Text))
                {
                    AccedentsSC = Convert.ToDecimal(tbAccedentSc.Text);
                }
                if (!String.IsNullOrEmpty(tbPassengerSC.Text))
                {
                    PassengerSc = Convert.ToDecimal(tbPassengerSC.Text.Trim());
                }
                if (!String.IsNullOrEmpty(tbITSc.Text))
                {
                    ITSc = Convert.ToDecimal(tbITSc.Text.Trim());
                }
                if (!String.IsNullOrEmpty(tbOtherSc.Text))
                {
                    OtherSc = Convert.ToDecimal(tbOtherSc.Text.Trim());
                }
                string EffectiveFrDate = tbDate.Text.ToString();
                NpgsqlCommand MyCommand = new NpgsqlCommand();
                MyCommand.Parameters.Clear();
                string Mresult = "";
                MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_fr_ssw_charge_insert");
                MyCommand.Parameters.AddWithValue("p_action", Action);
                MyCommand.Parameters.AddWithValue("p_skcr_id", skcrid);
                MyCommand.Parameters.AddWithValue("p_state_id", state);
                MyCommand.Parameters.AddWithValue("p_srtp_id", serviceType);
                MyCommand.Parameters.AddWithValue("p_fare_type", FareType);
                MyCommand.Parameters.AddWithValue("p_xmltext", swStringWriter.ToString());
                MyCommand.Parameters.AddWithValue("p_adnt_s_charge", AccedentsSC);
                MyCommand.Parameters.AddWithValue("p_pngr_s_charge", PassengerSc);
                MyCommand.Parameters.AddWithValue("p_it_s_charge", ITSc);
                MyCommand.Parameters.AddWithValue("p_othr_s_charge", OtherSc);
                MyCommand.Parameters.AddWithValue("p_status", Status);
                MyCommand.Parameters.AddWithValue("p_eff_from_dt", EffectiveFrDate);
                MyCommand.Parameters.AddWithValue("p_updatedby", UpdatedBy);
                MyCommand.Parameters.AddWithValue("p_ipaddress", ipaddress);
                dt = bll.SelectAll(MyCommand);
                if (dt.TableName == "Success")
                {
                    if (dt.Rows[0]["resultstr"].ToString() == "DONE")
                    {
                        if (Session["Action"].ToString() == "S")
                        {
                            Successmsg("Record has succefully been saved");
                        }
                        if (Session["Action"].ToString() == "U")
                        {
                            Successmsg("State Service wise Charge Updated Successfully.");
                        }

                        if (Session["Action"].ToString() == "A")
                        {
                            Successmsg("State Service wise Charge Activated.");
                        }
                        if (Session["Action"].ToString() == "D")
                        {
                            Successmsg("State Service wise Charge Deactivated.");
                        }

                        getStatesvcCharge();
                        StSvctotalCount();
                        ResetServiceCharge();
                    }
                    else if (dt.Rows[0]["resultstr"].ToString() == "ALREADY")
                    {
                        Errormsg("State service wise charges already save for selected 'State' and 'Service Type'");
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
                    _common.ErrorLog("sysAdminServiceWiseCharge-M6", Mresult);
                    Errormsg(Mresult);
                }
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("sysAdminServiceWiseCharge-M6", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
        }
    }
    public void StSvctotalCount()//M7
    {
        try
        {
            DataTable dt = new DataTable();
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_fr_ssw_charge_summary");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    lblTotalCharge.Text = dt.Rows[0]["totalchrg"].ToString();
                    lblActivateCharge.Text = dt.Rows[0]["activechrg"].ToString();
                    lblDeactCharge.Text = dt.Rows[0]["deactchrg"].ToString();
                }
                else
                {
                    _common.ErrorLog("sysAdminServiceWiseCharge-M7", dt.TableName.ToString());
                    Errormsg(dt.TableName);

                }
            }

        }
        catch (Exception ex)
        {
            gvCharge.Visible = false;
            _common.ErrorLog("sysAdminServiceWiseCharge-M7", ex.Message.ToString());
            Errormsg(ex.Message);

        }
    }
    public void getStatesvcCharge()//M8
    {
        try
        {
            pnlNoCharge.Visible = true;
            string state = ddlstateSearch.SelectedValue.ToString();
            long servicetype = Convert.ToInt64(ddlServiceTypeSearch.SelectedValue);
            DataTable dt = new DataTable();
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_fr_ssw_charge_list_get");
            MyCommand.Parameters.AddWithValue("p_state_id", state);
            MyCommand.Parameters.AddWithValue("p_service_type", servicetype);

            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    gvCharge.Visible = true;
                    gvCharge.DataSource = dt;
                    gvCharge.DataBind();
                    // lblTotalCharge.Text = dt.Rows[0]["skcrid"].ToString();
                    pnlNoCharge.Visible = false;
                }
                else
                {
                    gvCharge.Visible = false;
                    pnlNoCharge.Visible = true;

                }
            }
            else
            {
                _common.ErrorLog("sysAdminServiceWiseCharge-M8", dt.TableName.ToString());
                Errormsg(dt.TableName);

            }
        }
        catch (Exception ex)
        {
            gvCharge.Visible = false;
            _common.ErrorLog("sysAdminServiceWiseCharge-M8", ex.Message.ToString());
            Errormsg(ex.Message);

        }
    }
    private void ChangeFareType()
    {
        if (ddlState.SelectedValue != "0" && ddlServiceType.SelectedValue != "0")
        {
            ddlFareType.Enabled = false;
           // loadStateFareType();
            if (ddlFareType.SelectedValue == "0")
            {
                pnlPerKM.Visible = false;
                pnlSlab.Visible = false;
                pnlEffectiveDAte.Visible = false;
            }
            if (ddlFareType.SelectedValue == "P")
            {
                pnlPerKM.Visible = true;
                pnlSlab.Visible = false;
                pnlEffectiveDAte.Visible = true;
            }
            if (ddlFareType.SelectedValue == "S")
            {
                pnlPerKM.Visible = false;
                pnlSlab.Visible = true;
                pnlEffectiveDAte.Visible = true;
            }
        }
    }
    private void ResetServiceCharge()
    {
        ddlState.SelectedValue = "0";
        ddlServiceType.SelectedValue = "0";
        ddlFareType.SelectedValue = "0";
        ddlState.Enabled = true;
        ddlServiceType.Enabled = true;
        ddlFareType.Enabled = true;
        tbAccedentSc.Text = "";
        tbPassengerSC.Text = "";
        tbITSc.Text = "";
        tbOtherSc.Text = "";
        tbDate.Text = "";
        ddlFareType.Enabled = true;
        ddlState.Enabled = true;
        lblChargeHead.Visible = true;
        lblChargeUpdate.Visible = false;
        lbtnUpdate.Visible = false;
        lbtnSave.Visible = true;
        lbtnReset.Visible = true;
        lbtnCancel.Visible = false;
        pnlSlab.Visible = false;
        pnlPerKM.Visible = false;
        pnlEffectiveDAte.Visible = false;

        SetInitialRow();

        for (int i = 0; i < gvaddSlab.Rows.Count; i++)
        {
            GridViewRow row = gvaddSlab.Rows[i];
            TextBox tbFromkm1 = row.FindControl("tbFromkm1") as TextBox;
            tbFromkm1.Text = "";
            TextBox tbtokm1 = row.FindControl("tbtokm1") as TextBox;
            tbtokm1.Text = "";
            TextBox tbAccedentSCSlab1 = row.FindControl("tbAccedentSCSlab1") as TextBox;
            tbAccedentSCSlab1.Text = "";
            TextBox tbPassengerSCslab1 = row.FindControl("tbPassengerSCslab1") as TextBox;
            tbPassengerSCslab1.Text = "";
            TextBox tbItScSlab1 = row.FindControl("tbItScSlab1") as TextBox;
            tbItScSlab1.Text = "";
            TextBox tbOtherScSlab1 = row.FindControl("tbOtherScSlab1") as TextBox;
            tbOtherScSlab1.Text = "";

        }
    }
    private void SetInitialRow()

    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("fr_km", typeof(string)));
        dt.Columns.Add(new DataColumn("to_km", typeof(string)));
        dt.Columns.Add(new DataColumn("adnt_s_charge", typeof(string)));
        dt.Columns.Add(new DataColumn("pngr_s_charge", typeof(string)));
        dt.Columns.Add(new DataColumn("it_s_charge", typeof(string)));
        dt.Columns.Add(new DataColumn("othr_s_charge", typeof(string)));
        dr = dt.NewRow();
        dr["RowNumber"] = 1;
        dr["fr_km"] = string.Empty;
        dr["to_km"] = string.Empty;
        dr["adnt_s_charge"] = string.Empty;
        dr["pngr_s_charge"] = string.Empty;
        dr["it_s_charge"] = string.Empty;
        dr["othr_s_charge"] = string.Empty;

        dt.Rows.Add(dr);

        //dr = dt.NewRow();
        //Store the DataTable in ViewState

        ViewState["CurrentTable"] = dt;
        gvaddSlab.DataSource = dt;
        gvaddSlab.DataBind();
    }
    private void AddNewRowToGrid()

    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)

        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)

                {
                    //extract the TextBox values

                    TextBox box1 = (TextBox)gvaddSlab.Rows[rowIndex].Cells[1].FindControl("tbFromkm1");
                    TextBox box2 = (TextBox)gvaddSlab.Rows[rowIndex].Cells[2].FindControl("tbtokm1");
                    TextBox box3 = (TextBox)gvaddSlab.Rows[rowIndex].Cells[3].FindControl("tbAccedentSCSlab1");
                    TextBox box4 = (TextBox)gvaddSlab.Rows[rowIndex].Cells[4].FindControl("tbPassengerSCslab1");
                    TextBox box5 = (TextBox)gvaddSlab.Rows[rowIndex].Cells[5].FindControl("tbItScSlab1");
                    TextBox box6 = (TextBox)gvaddSlab.Rows[rowIndex].Cells[6].FindControl("tbOtherScSlab1");
                    drCurrentRow = dtCurrentTable.NewRow();

                    drCurrentRow["RowNumber"] = i + 1;
                    dtCurrentTable.Rows[i - 1]["fr_km"] = box1.Text;
                    dtCurrentTable.Rows[i - 1]["to_km"] = box2.Text;
                    dtCurrentTable.Rows[i - 1]["adnt_s_charge"] = box3.Text;
                    dtCurrentTable.Rows[i - 1]["pngr_s_charge"] = box4.Text;
                    dtCurrentTable.Rows[i - 1]["it_s_charge"] = box5.Text;
                    dtCurrentTable.Rows[i - 1]["othr_s_charge"] = box6.Text;
                    rowIndex++;

                }
                dtCurrentTable.Rows.Add(drCurrentRow);
                ViewState["CurrentTable"] = dtCurrentTable;
                gvaddSlab.DataSource = dtCurrentTable;
                gvaddSlab.DataBind();
                hdnTotSlabs.Value = gvaddSlab.Rows.Count.ToString();

            }

        }
        else
        {
            Errormsg("ViewState is null");
        }
        //Set Previous Data on Postbacks

        SetPreviousData();

    }
    private void RemoveRowToGrid(int rowindex)
    {
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            if (dtCurrentTable.Rows.Count > 0)
            {
                dtCurrentTable.Rows.RemoveAt(rowindex);
                ViewState["CurrentTable"] = dtCurrentTable;
                gvaddSlab.DataSource = dtCurrentTable;
                gvaddSlab.DataBind();

                hdnTotSlabs.Value = gvaddSlab.Rows.Count.ToString();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        //Set Previous Data on Postbacks
        SetPreviousData();
    }
    private void SetPreviousData()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TextBox box1 = (TextBox)gvaddSlab.Rows[rowIndex].Cells[1].FindControl("tbFromkm1");
                    TextBox box2 = (TextBox)gvaddSlab.Rows[rowIndex].Cells[2].FindControl("tbtokm1");
                    TextBox box3 = (TextBox)gvaddSlab.Rows[rowIndex].Cells[3].FindControl("tbAccedentSCSlab1");
                    TextBox box4 = (TextBox)gvaddSlab.Rows[rowIndex].Cells[4].FindControl("tbPassengerSCslab1");
                    TextBox box5 = (TextBox)gvaddSlab.Rows[rowIndex].Cells[5].FindControl("tbItScSlab1");
                    TextBox box6 = (TextBox)gvaddSlab.Rows[rowIndex].Cells[6].FindControl("tbOtherScSlab1");
                    box1.Text = dt.Rows[i]["fr_km"].ToString();
                    box2.Text = dt.Rows[i]["to_km"].ToString();
                    box3.Text = dt.Rows[i]["adnt_s_charge"].ToString();
                    box4.Text = dt.Rows[i]["pngr_s_charge"].ToString();
                    box5.Text = dt.Rows[i]["it_s_charge"].ToString();
                    box6.Text = dt.Rows[i]["othr_s_charge"].ToString();
                    rowIndex++;

                }

            }

        }

    }
    private void InfoMsg(string msg)
    {
        string popup = _popup.modalPopupSmall("I", "Information", msg, "Close");
        Response.Write(popup);
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

    #endregion

    #region "Events"
    
    protected void lbtnUpdate_Click(object sender, EventArgs e)
    {
        if (isvalidvalueUpdate() == false)
        {
            return;
        }
        Session["Action"] = "U";
        ConfirmMsg("Do you want to Update State Details ?");

    }
    protected void lbtnSave_Click(object sender, EventArgs e)
    {
        if (isvalidvalueUpdate() == false)
        {
            return;
        }
        //Session["skcrid"] = 0;
        Session["Action"] = "S";
        ConfirmMsg("Do you want to Save State Details ?");
    }
    protected void lbtnReset_Click(object sender, EventArgs e)
    {
        ResetServiceCharge();
    }
    protected void lbtnCancel_Click(object sender, EventArgs e)
    {
        ResetServiceCharge();
    }
    protected void lbtnAddNewSlab_Click(object sender, EventArgs e)
    {
        //  AddNewRowToGrid();
    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        if (Session["Action"].ToString() == "S")
        {
            SaveValue();
        }
        if (Session["Action"].ToString() == "U")
        {
            SaveValue();
        }
        if (Session["Action"].ToString() == "D")
        {
            SaveValue();
        }
        if (Session["Action"].ToString() == "A")
        {
            SaveValue();
        }

    }
    protected void gvCharge_RowCommand(object sender, GridViewCommandEventArgs e)//E1
    {
        try
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string skcrid;
            ResetServiceCharge();
            if (e.CommandName == "updateCharge")
            {
                lbtnSave.Visible = false;
                lbtnUpdate.Visible = true;
                lbtnReset.Visible = false;
                lbtnCancel.Visible = true;
                lblChargeHead.Visible = false;
                lblChargeUpdate.Visible = true;

                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                Session["p_skcr_id"] = "";// gvCharge.DataKeys[row.RowIndex]["skcrid"].ToString();
                lblChargeUpdate.Text = "Update State service wise Charge-  (" + gvCharge.DataKeys[row.RowIndex]["stateid"].ToString() + ")";

                string state = gvCharge.DataKeys[row.RowIndex]["stateid"].ToString();
                string Servicetype = gvCharge.DataKeys[row.RowIndex]["srtpid"].ToString();
                string Faretype = gvCharge.DataKeys[row.RowIndex]["faretype"].ToString();
                string Date = gvCharge.DataKeys[row.RowIndex]["efffrom_dt"].ToString();


                ddlState.SelectedValue = state;
                ddlState.Enabled = false;
                ddlServiceType.SelectedValue = Servicetype;
                ddlServiceType.Enabled = false;
                ddlFareType.SelectedValue = Faretype;
                ddlFareType.Enabled = false;
                tbDate.Text = Date;

                MyCommand = new NpgsqlCommand();
                MyCommand.Parameters.Clear();
                MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_fr_ssw_charge_only_get");
                MyCommand.Parameters.AddWithValue("p_state_id", state);
                MyCommand.Parameters.AddWithValue("p_service_type", Convert.ToInt16(Servicetype));
                DataTable dtt = new DataTable();
                dtt = bll.SelectAll(MyCommand);
                if (dtt.Rows.Count > 0)
                {
                    dtt.Columns.Add("RowNumber");
                    dtt.Columns[0].ColumnName = "fr_km";
                    dtt.Columns[1].ColumnName = "to_km";
                    dtt.Columns[2].ColumnName = "adnt_s_charge";
                    dtt.Columns[3].ColumnName = "pngr_s_charge";
                    dtt.Columns[4].ColumnName = "it_s_charge";
                    dtt.Columns[5].ColumnName = "othr_s_charge";

                    if (Faretype == "S")
                    {
                        ViewState["CurrentTable"] = dtt;
                        gvaddSlab.DataSource = dtt;
                        gvaddSlab.DataBind();
                        SetPreviousData();
                        AddNewRowToGrid();
                    }
                    else if (Faretype == "P")
                    {
                        tbAccedentSc.Text = dtt.Rows[0]["adnt_s_charge"].ToString();
                        tbPassengerSC.Text = dtt.Rows[0]["pngr_s_charge"].ToString();
                        tbITSc.Text = dtt.Rows[0]["it_s_charge"].ToString();
                        tbOtherSc.Text = dtt.Rows[0]["othr_s_charge"].ToString();
                        ddlFareType.SelectedValue = Faretype;
                    }
                    else { }
                }

                Session["Action"] = "U";
                ChangeFareType();
            }
            if (e.CommandName == "activate")
            {
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                string state = gvCharge.DataKeys[row.RowIndex]["stateid"].ToString();
                string Servicetype = gvCharge.DataKeys[row.RowIndex]["srtpid"].ToString();
                Session["state_Id_"] = state;
                Session["service_type_Id_"] = Servicetype;
                Session["Action"] = "A";
                ConfirmMsg("Do you want to activate Service Charge?");
            }
            if (e.CommandName == "deactivate")
            {
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                string state = gvCharge.DataKeys[row.RowIndex]["stateid"].ToString();
                string Servicetype = gvCharge.DataKeys[row.RowIndex]["srtpid"].ToString();
                Session["state_Id_"] = state;
                Session["service_type_Id_"] = Servicetype;
                Session["Action"] = "D";
                ConfirmMsg("Do you want to deactivate Service Charge?");
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("sysAdminServiceWiseCharge-E1", ex.Message.ToString());
            Errormsg(ex.Message);
        }

    }
    protected void gvCharge_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbtnActivate = (LinkButton)e.Row.FindControl("lbtnActivate");
            LinkButton lbtnDiscontinue = (LinkButton)e.Row.FindControl("lbtnDiscontinue");
            LinkButton lbtnUpdategency = (LinkButton)e.Row.FindControl("lbtnUpdategency");
            DataRowView rowView = (DataRowView)e.Row.DataItem;
            lbtnActivate.Visible = false;
            lbtnDiscontinue.Visible = false;

            if (rowView["satus"].ToString() == "A")
            {
                lbtnDiscontinue.Visible = true;
            }
            else if (rowView["satus"].ToString() == "D")
            {
                lbtnActivate.Visible = true;
                lbtnUpdategency.Visible = false;
            }
        }
    }
    protected void gvCharge_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvCharge.PageIndex = e.NewPageIndex;
        getStatesvcCharge();

    }
    protected void gvaddSlab_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "Add")
        {
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            TextBox txtFromKM = (TextBox)gvaddSlab.Rows[RowIndex].FindControl("tbFromkm1");
            TextBox txtToKM = (TextBox)gvaddSlab.Rows[RowIndex].FindControl("tbtokm1");
            TextBox txtAccedent = (TextBox)gvaddSlab.Rows[RowIndex].FindControl("tbAccedentSCSlab1");
            TextBox txtpassenger = (TextBox)gvaddSlab.Rows[RowIndex].FindControl("tbPassengerSCslab1");
            TextBox txtItSlab = (TextBox)gvaddSlab.Rows[RowIndex].FindControl("tbItScSlab1");
            TextBox txtOther = (TextBox)gvaddSlab.Rows[RowIndex].FindControl("tbOtherScSlab1");
            bool validation = ValidaionSlab(txtFromKM.Text, txtToKM.Text, txtAccedent.Text, txtpassenger.Text, txtItSlab.Text, txtOther.Text);

            if (validation == true)
            {
                if (gvaddSlab.Rows.Count > 1 && RowIndex >= 1)
                {
                    TextBox txtprevToKM = (TextBox)gvaddSlab.Rows[RowIndex - 1].FindControl("tbtokm1");
                    Int32 FromKM = Convert.ToInt32(txtFromKM.Text.ToString());
                    Int32 prevKM = Convert.ToInt32(txtprevToKM.Text.ToString());
                    if (FromKM < prevKM)
                    {
                        Errormsg("From KM shouldnt be less than previous to km");
                        txtFromKM.Text = "";
                        txtToKM.Text = "";
                        txtAccedent.Text = "";
                        txtpassenger.Text = "";
                        txtItSlab.Text = "";
                        txtOther.Text = "";
                        return;
                    }
                }
                AddNewRowToGrid();
                txtFromKM.Enabled = true;
                txtToKM.Enabled = true;
                txtAccedent.Enabled = true;
                txtpassenger.Enabled = true;
                txtItSlab.Enabled = true;
                txtOther.Enabled = true;
            }
            else
            {
                return;
            }
            
        }
        if (e.CommandName == "Remove")
        {
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            RemoveRowToGrid(index);
            //makeDataTable(RowIndex);
        }
    }
    protected void gvaddSlab_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            LinkButton lbtnAddNewSlab = (LinkButton)e.Row.FindControl("lbtnAddNewSlab");
            LinkButton lbtnRemoveNewSlab = (LinkButton)e.Row.FindControl("lbtnRemoveNewSlab");

            TextBox tbFrom = (TextBox)e.Row.FindControl("tbFromkm1") as TextBox;

            TextBox tbTo = (TextBox)e.Row.FindControl("tbtokm1") as TextBox;

            TextBox tbAccedent = (TextBox)e.Row.FindControl("tbAccedentSCSlab1") as TextBox;

            TextBox tbPassenger = (TextBox)e.Row.FindControl("tbPassengerSCslab1") as TextBox;

            TextBox tbIt = (TextBox)e.Row.FindControl("tbItScSlab1") as TextBox;

            TextBox tbOther = (TextBox)e.Row.FindControl("tbOtherScSlab1") as TextBox;


            int rindex = int.Parse(hdnTotSlabs.Value.ToString());
            lbtnAddNewSlab.Visible = false;
            lbtnRemoveNewSlab.Visible = false;
            tbFrom.Enabled = false;
            tbTo.Enabled = false;
            tbAccedent.Enabled = false;
            tbPassenger.Enabled = false;
            tbIt.Enabled = false;
            tbOther.Enabled = false;


            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            if (e.Row.RowIndex == dtCurrentTable.Rows.Count - 1)
            {
                lbtnAddNewSlab.Visible = true;
                lbtnRemoveNewSlab.Visible = false;
                tbFrom.Enabled = true;
                tbTo.Enabled = true;
                tbAccedent.Enabled = true;
                tbPassenger.Enabled = true;
                tbIt.Enabled = true;
                tbOther.Enabled = true;

            }
            else if (e.Row.RowIndex == dtCurrentTable.Rows.Count - 2)
            {
                lbtnAddNewSlab.Visible = false;
                lbtnRemoveNewSlab.Visible = true;

            }

        }
    }
    protected void lbtnSearchCharge_Click(object sender, EventArgs e)
    {
        getStatesvcCharge();
    }
    protected void lbtnResetSearchCharge_Click(object sender, EventArgs e)
    {
        ddlstateSearch.SelectedValue = "0";
        ddlServiceTypeSearch.SelectedValue = "0";
        getStatesvcCharge();
    }
    protected void lbtnview_Click(object sender, EventArgs e)
    {

        InfoMsg("Comming Soon");
    }
    #endregion



    protected void ddlFareType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFareType.SelectedValue == "0")
        {
            pnlPerKM.Visible = false;
            pnlSlab.Visible = false;
            pnlEffectiveDAte.Visible = false;
        }
        if (ddlFareType.SelectedValue == "P")
        {
            pnlPerKM.Visible = true;
            pnlSlab.Visible = false;
            pnlEffectiveDAte.Visible = true;
        }
        if (ddlFareType.SelectedValue == "S")
        {
            pnlPerKM.Visible = false;
            pnlSlab.Visible = true;
            pnlEffectiveDAte.Visible = true;
        }
    }
}