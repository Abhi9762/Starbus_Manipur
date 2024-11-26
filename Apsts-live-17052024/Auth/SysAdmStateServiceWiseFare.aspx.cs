using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_SysAdmStateServiceWiseFare : BasePage
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
        Session["_moduleName"] = "State service wise fare";
        if (IsPostBack == false)
        {
            Random randomclass = new Random();
            Session["rndNoCheck"] = randomclass.Next().ToString();
            hidtoken.Value = Session["rndNoCheck"].ToString();
            hdnTotSlabs.Value = "0";
            loadStates();
            loadServicetype();
            SetInitialRow();
            getServiceWiseFareList();
            FareCount();
        }
    }

    #region "Method"
    private void CsrfTokenValidate()
    {
        if (hidtoken.Value != Session["rndNoCheck"].ToString())
        {
            Response.Redirect("errorpage.aspx");
            return;
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
    public void loadStates() //M1
    {
        try
        {
            ddlState.Items.Clear();
            ddlSState.Items.Clear();

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


            Errormsg("There is some error" + ex.Message);
            _common.ErrorLog("SysAdmStateServiceWiseFare.aspx-0001", ex.Message.ToString());
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

            Errormsg("There is some error" + ex.Message);
            _common.ErrorLog("SysAdmStateServiceWiseFare.aspx-0002", ex.Message.ToString());
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
            if (dt.Rows.Count > 0)
            {
                ddlFareType.SelectedValue = dt.Rows[0]["fare"].ToString();

            }
        }
        catch (Exception ex)
        {
            Errormsg("There is some error" + ex.Message);
            _common.ErrorLog("SysAdmStateServiceWiseFare.aspx-0003", ex.Message.ToString());
        }
    }
    public DataTable getSlab_table()//M4
    {
        // Create a new DataTable.
        DataTable dtTimeTable = new DataTable("timeTable");
        dtTimeTable.Columns.Add("fr_km", typeof(string));
        dtTimeTable.Columns.Add("to_km", typeof(string));
        dtTimeTable.Columns.Add("rate_hill", typeof(string));
        dtTimeTable.Columns.Add("rate_plain", typeof(string));
        string frmkm, tokm, hill, Plain;
        // Loop through the GridView and copy rows.
        foreach (GridViewRow row in gvSlab.Rows)
        {
            TextBox tbFrom = row.FindControl("tbFromkm") as TextBox;
            frmkm = tbFrom.Text;

            TextBox tbTo = row.FindControl("tbtokm") as TextBox;
            tokm = tbTo.Text;

            TextBox tbHill = row.FindControl("tbHillRateSlab") as TextBox;
            hill = tbHill.Text;

            TextBox tbPlain = row.FindControl("tbPlainRateslab") as TextBox;
            Plain = tbPlain.Text;

            if (!(frmkm.Trim().Length <= 0 || tokm.Trim().Length <= 0 || hill.Trim().Length <= 0 || Plain.Trim().Length <= 0))
            {
                dtTimeTable.Rows.Add(frmkm, tokm, hill, Plain);
            }

        }

        return dtTimeTable;
    }
    public void makeDataTable(int rownumber)//M9
    {

        // Create a new DataTable.
        DataTable dtTimeTable = new DataTable("timeTable");
        dtTimeTable.Columns.Add("fromKM", typeof(string));
        dtTimeTable.Columns.Add("toKm", typeof(string));
        dtTimeTable.Columns.Add("rate_hill", typeof(string));
        dtTimeTable.Columns.Add("rate_plain", typeof(string));
        string frmkm, tokm, hill, Plain;

        // Loop through the GridView and copy rows.
        foreach (GridViewRow row in gvSlab.Rows)
        {
            if (rownumber >= 0)
            {
                if (row.RowIndex != rownumber)
                {
                    TextBox tbFrom = row.FindControl("tbFromkm") as TextBox;
                    frmkm = tbFrom.Text;

                    TextBox tbTo = row.FindControl("tbtokm") as TextBox;
                    tokm = tbTo.Text;


                    TextBox tbHill = row.FindControl("tbHillRateSlab") as TextBox;
                    hill = tbHill.Text;

                    TextBox tbPlain = row.FindControl("tbPlainRateslab") as TextBox;
                    Plain = tbPlain.Text;
                    dtTimeTable.Rows.Add(frmkm, tokm, hill, Plain);



                }
            }
            if (rownumber == -1)
            {
                TextBox tbFrom = row.FindControl("tbFromkm") as TextBox;
                frmkm = tbFrom.Text;

                TextBox tbTo = row.FindControl("tbtokm") as TextBox;
                tokm = tbTo.Text;

                TextBox tbHill = row.FindControl("tbHillRateSlab") as TextBox;
                hill = tbHill.Text;

                TextBox tbPlain = row.FindControl("tbPlainRateslab") as TextBox;
                Plain = tbPlain.Text;
                dtTimeTable.Rows.Add(frmkm, tokm, hill, Plain);
            }
        }
        gvSlab.DataSource = dtTimeTable;
        gvSlab.DataBind();
        //return dtTimeTable;
    }
    public void insertStab_Table() //M5
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

                string status = "A", HillRate = "0", PlainRate = "0";

                string State = ddlState.SelectedValue;
                string ServiceType = ddlServiceType.SelectedValue;

                if (Session["Action"].ToString() == "Deactive")
                {
                    State = Session["state_Id_"].ToString();
                    ServiceType = Session["service_type_Id_"].ToString();
                    status = "D";
                }
                if (Session["Action"].ToString() == "Active")
                {
                    State = Session["state_Id_"].ToString();
                    ServiceType = Session["service_type_Id_"].ToString();
                    status = "A";
                }

                if (!String.IsNullOrEmpty(tbHillRate.Text))
                {
                    HillRate = tbHillRate.Text;
                }
                if (!String.IsNullOrEmpty(tbPlainRate.Text))
                {
                    PlainRate = tbPlainRate.Text;
                }

                string FareType = ddlFareType.SelectedValue;
                string Date = tbDate.Text;
                string UpdatedBy = Session["_UserCode"].ToString();
                string IpAddress = HttpContext.Current.Request.UserHostAddress;

                MyCommand = new NpgsqlCommand();
                MyCommand.Parameters.Clear();
                MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_fr_ssw_fare_insert");
                MyCommand.Parameters.AddWithValue("p_skmr", Convert.ToInt64(0));
                MyCommand.Parameters.AddWithValue("p_action", Session["Action"].ToString());
                MyCommand.Parameters.AddWithValue("p_faretype", FareType);
                MyCommand.Parameters.AddWithValue("p_xmltext", swStringWriter.ToString());
                MyCommand.Parameters.AddWithValue("p_hillrate", Convert.ToDecimal(HillRate));
                MyCommand.Parameters.AddWithValue("p_plainrate", Convert.ToDecimal(PlainRate));
                MyCommand.Parameters.AddWithValue("p_statecode", State);
                MyCommand.Parameters.AddWithValue("p_srtp", Convert.ToInt16(ServiceType));
                MyCommand.Parameters.AddWithValue("p_status", status);
                MyCommand.Parameters.AddWithValue("p_date", Date);
                MyCommand.Parameters.AddWithValue("p_updatedby", UpdatedBy);
                MyCommand.Parameters.AddWithValue("p_ipaddress", IpAddress);
                dt = bll.SelectAll(MyCommand);

                if (dt.TableName == "Success")
                {
                    if (dt.Rows[0]["resultstr"].ToString() == "DONE")
                    {
                        if (Session["Action"].ToString() == "Active")
                        {
                            Successmsg("Service Wise Fare Activated");
                        }
                        if (Session["Action"].ToString() == "Deactive")
                        {
                            Successmsg("Service Wise Fare Deactivated");
                        }
                        if (Session["Action"].ToString() == "S")
                        {
                            Successmsg("Service Wise Fare Saved");
                        }
                        if (Session["Action"].ToString() == "U")
                        {
                            Successmsg("Service Wise Fare Updated");
                        }
                        resetcontrols();
                        FareCount();
                        getServiceWiseFareList();
                    }
                    else if (dt.Rows[0]["resultstr"].ToString() == "ALREADY")
                    {
                        Errormsg("State service wise fare already save for selected 'State' and 'Service Type'");
                    }
                    else if (dt.Rows[0]["resultstr"].ToString() == "EXCEPTION")
                    {
                        Errormsg(commonerror);
                    }
                    else
                    {
                        Errormsg(commonerror);
                        _common.ErrorLog("SysAdmStateServiceWiseFare.aspx-0006", dt.TableName);
                    }
                }
                else
                {
                    Errormsg(commonerror);
                    _common.ErrorLog("SysAdmStateServiceWiseFare.aspx-0005", dt.TableName);
                }
            }
        }
        catch (Exception ex)
        {
            Errormsg("There is some error" + ex.Message);
            _common.ErrorLog("SysAdmStateServiceWiseFare.aspx-0004", ex.Message.ToString());
        }
    }
    private void SetInitialRow()//M6
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("fr_km", typeof(string)));
        dt.Columns.Add(new DataColumn("to_km", typeof(string)));
        dt.Columns.Add(new DataColumn("rate_hill", typeof(string)));
        dt.Columns.Add(new DataColumn("rate_plain", typeof(string)));

        dr = dt.NewRow();
        dr["RowNumber"] = 1;
        dr["fr_km"] = string.Empty;
        dr["to_km"] = string.Empty;
        dr["rate_hill"] = string.Empty;
        dr["rate_plain"] = string.Empty;
        dt.Rows.Add(dr);

        //dr = dt.NewRow();
        //Store the DataTable in ViewState
        ViewState["CurrentTable"] = dt;
        gvSlab.DataSource = dt;
        gvSlab.DataBind();

    }
    private void AddNewRowToGrid()//M7
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
                    TextBox box1 = (TextBox)gvSlab.Rows[rowIndex].Cells[1].FindControl("tbFromkm");
                    TextBox box2 = (TextBox)gvSlab.Rows[rowIndex].Cells[2].FindControl("tbtokm");
                    TextBox box3 = (TextBox)gvSlab.Rows[rowIndex].Cells[3].FindControl("tbHillRateSlab");
                    TextBox box4 = (TextBox)gvSlab.Rows[rowIndex].Cells[4].FindControl("tbPlainRateslab");
                    drCurrentRow = dtCurrentTable.NewRow();

                    drCurrentRow["RowNumber"] = i + 1;
                    dtCurrentTable.Rows[i - 1]["fr_km"] = box1.Text;
                    dtCurrentTable.Rows[i - 1]["to_km"] = box2.Text;
                    dtCurrentTable.Rows[i - 1]["rate_hill"] = box3.Text;
                    dtCurrentTable.Rows[i - 1]["rate_plain"] = box4.Text;
                    rowIndex++;
                }
                dtCurrentTable.Rows.Add(drCurrentRow);
                ViewState["CurrentTable"] = dtCurrentTable;
                gvSlab.DataSource = dtCurrentTable;
                gvSlab.DataBind();
                hdnTotSlabs.Value = gvSlab.Rows.Count.ToString();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }

        SetPreviousData();

    }
    private void SetPreviousData()//M8
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TextBox box1 = (TextBox)gvSlab.Rows[rowIndex].Cells[1].FindControl("tbFromkm");
                    TextBox box2 = (TextBox)gvSlab.Rows[rowIndex].Cells[2].FindControl("tbtokm");
                    TextBox box3 = (TextBox)gvSlab.Rows[rowIndex].Cells[3].FindControl("tbHillRateSlab");
                    TextBox box4 = (TextBox)gvSlab.Rows[rowIndex].Cells[3].FindControl("tbPlainRateslab");
                    box1.Text = dt.Rows[i]["fr_km"].ToString();
                    box2.Text = dt.Rows[i]["to_km"].ToString();
                    box3.Text = dt.Rows[i]["rate_hill"].ToString();
                    box4.Text = dt.Rows[i]["rate_plain"].ToString();
                    rowIndex++;
                }
            }
        }
    }
    private void Load()//M9
    {
        if (ddlState.SelectedValue != "0" && ddlServiceType.SelectedValue != "0")
        {
            ddlFareType.Enabled = false;
            loadStateFareType();
            if (ddlFareType.SelectedValue == "0")
            {
                pnlPerKM.Visible = false;
                pnlSlab.Visible = false;
                pnlShow.Visible = false;

            }
            if (ddlFareType.SelectedValue == "P")
            {
                pnlPerKM.Visible = true;
                pnlSlab.Visible = false;
                pnlShow.Visible = true;
            }
            if (ddlFareType.SelectedValue == "S")
            {
                pnlPerKM.Visible = false;
                pnlSlab.Visible = true;
                pnlShow.Visible = true;
            }
        }
    }
    public void getServiceWiseFareList()//M10
    {
        try
        {
            string State = ddlSState.SelectedValue;
            string ServiceType = ddlSServiceType.SelectedValue;

            gvServiceWiseFare.Visible = false;
            pnlNoFare.Visible = true;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_fr_ssw_fare_list_get");
            MyCommand.Parameters.AddWithValue("p_statecode", State);
            MyCommand.Parameters.AddWithValue("p_srtp", Convert.ToInt16(ServiceType));
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                gvServiceWiseFare.DataSource = dt;
                gvServiceWiseFare.DataBind();
                gvServiceWiseFare.Visible = true;
                pnlNoFare.Visible = false;

            }
        }
        catch (Exception ex)
        {
            Errormsg(commonerror);
            _common.ErrorLog("SysAdmStateServiceWiseFare.aspx-0006", ex.Message.ToString());
        }
    }
    private void FareCount()//M11
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_fr_ssw_fare_summary");
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                lblTotalFare.Text = dt.Rows[0]["total"].ToString();
                lblSlab.Text = dt.Rows[0]["slab"].ToString();
                lblFare.Text = dt.Rows[0]["Pkm"].ToString();
                lblFare.Text = dt.Rows[0]["Pkm"].ToString();

            }
        }
        catch (Exception ex)
        {
            Errormsg(commonerror);
            _common.ErrorLog("SysAdmStateServiceWiseFare.aspx-0007", ex.Message.ToString());
        }
    }
    private void resetcontrols()
    {
        lblFareHead.Visible = true;
        lblFareUpdate.Visible = false;
        ddlState.SelectedValue = "0";
        ddlServiceType.SelectedValue = "0";
        ddlFareType.SelectedValue = "0";
        ddlState.Enabled = true;
        ddlServiceType.Enabled = true;
        ddlFareType.Enabled = true;
        tbHillRate.Text = "";
        tbPlainRate.Text = "";
        tbDate.Text = "";
        ddlFareType.Enabled = true;
        ddlState.Enabled = true;
        lbtnUpdate.Visible = false;
        lbtnSave.Visible = true;
        lbtnReset.Visible = true;
        lbtnCancel.Visible = false;
        pnlSlab.Visible = false;
        pnlPerKM.Visible = false;
        pnlShow.Visible = false;

        SetInitialRow();
        for (int i = 0; i < gvSlab.Rows.Count; i++)
        {
            GridViewRow row = gvSlab.Rows[i];
            TextBox tbFromkm = row.FindControl("tbFromkm") as TextBox;
            tbFromkm.Text = "";
            TextBox tbtokm = row.FindControl("tbtokm") as TextBox;
            tbtokm.Text = "";
            TextBox tbHillRateSlab = row.FindControl("tbHillRateSlab") as TextBox;
            tbHillRateSlab.Text = "";
            TextBox tbPlainRateslab = row.FindControl("tbPlainRateslab") as TextBox;
            tbPlainRateslab.Text = "";
        }
    }
    private void RemoveRowToGrid(int rowindex)//M6
    {
        if (ViewState["CurrentTable"] != null)
        {

            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            int rowCount = dtCurrentTable.Rows.Count;
            if (rowCount > 0)
            {
                dtCurrentTable.Rows.RemoveAt(rowindex);

                ViewState["CurrentTable"] = dtCurrentTable;
                gvSlab.DataSource = dtCurrentTable;
                gvSlab.DataBind();

                hdnTotSlabs.Value = gvSlab.Rows.Count.ToString();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        //Set Previous Data on Postbacks
        SetPreviousData();
    }
    private Boolean ValidaionSlab(string from, string to, string HillRate, string PlainRate)
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

                if (_validation.isValideDecimalNumber(HillRate, 1, 5) == false)
                {
                    count = count + 1;
                    msg = msg + count.ToString() + ". Enter Hill Rate.<br/>";
                }
                if (_validation.isValideDecimalNumber(PlainRate, 1, 5) == false)
                {
                    count = count + 1;
                    msg = msg + count.ToString() + ". Enter Plain Rate.<br/>";
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
            _common.ErrorLog("SysAdmStateServiceWiseFare.aspx-0008", ex.Message.ToString());
            Errormsg("There is invalid values in slab. Please enter valid values.");
            return false;
        }
    }

    #endregion

    #region "Events"
    protected void lbtnview_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        InfoMsg("Coming Soon");

    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        insertStab_Table();
    }
    protected void lbtnUpdate_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Session["Action"] = "U";
        ConfirmMsg("Do you want to Update State service wise fare ?");
    }
    protected void lbtnSave_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (_validation.IsValidString(tbDate.Text, 10, 10) == false)
        {
            Errormsg("Please enter valid 'Effect From Date'");
            return;
        }

        Session["Action"] = "S";
        ConfirmMsg("Do you want to save State service wise fare ?");

    }
    protected void ddlServiceType_SelectedIndexChanged(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Load();
    }
    protected void lbtnAddNewSlab_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        AddNewRowToGrid();
    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Load();

    }
    protected void gvServiceWiseFare_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbtnActivate = (LinkButton)e.Row.FindControl("lbtnActivate");
            LinkButton lbtnDiscontinue = (LinkButton)e.Row.FindControl("lbtnDiscontinue");
            LinkButton lbtnUpdateFare = (LinkButton)e.Row.FindControl("lbtnUpdateFare");
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
                lbtnUpdateFare.Visible = false;
            }


        }
    }
    protected void gvServiceWiseFare_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvServiceWiseFare.PageIndex = e.NewPageIndex;
        getServiceWiseFareList();
    }
    protected void gvServiceWiseFare_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        CsrfTokenValidate();
        if (e.CommandName == "updateFare")
        {
            lblFareHead.Visible = false;
            lblFareUpdate.Visible = true;
            lbtnUpdate.Visible = true;
            lbtnSave.Visible = false;
            lbtnReset.Visible = false;
            lbtnCancel.Visible = true;
            ddlState.Enabled = false;
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            string ID = "";// gvServiceWiseFare.DataKeys[row.RowIndex]["skmrid"].ToString();
            Session["skmrid"] = ID;
            lblFareUpdate.Text = "Update State Service Wise Fare-  (" + gvServiceWiseFare.DataKeys[row.RowIndex]["statename"].ToString() + ")";
            string state = gvServiceWiseFare.DataKeys[row.RowIndex]["stateid"].ToString();
            string Servicetype = gvServiceWiseFare.DataKeys[row.RowIndex]["srtp"].ToString();
            string Faretype = gvServiceWiseFare.DataKeys[row.RowIndex]["faretype"].ToString();
            string Date = gvServiceWiseFare.DataKeys[row.RowIndex]["eff"].ToString();
            ddlState.SelectedValue = state;
            ddlServiceType.SelectedValue = Servicetype;
            ddlServiceType.Enabled = false;
            ddlFareType.SelectedValue = Faretype;
            tbDate.Text = Date;


            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_fr_ssw_fare_only_get");
            MyCommand.Parameters.AddWithValue("p_statecode", state);
            MyCommand.Parameters.AddWithValue("p_srtp", Convert.ToInt16(Servicetype));
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                dt.Columns.Add("RowNumber");

                dt.Columns[0].ColumnName = "fr_km";
                dt.Columns[1].ColumnName = "to_km";
                dt.Columns[2].ColumnName = "rate_hill";
                dt.Columns[3].ColumnName = "rate_plain";

                if (Faretype == "S")
                {
                    ViewState["CurrentTable"] = dt;
                    gvSlab.DataSource = dt;
                    gvSlab.DataBind();
                    SetPreviousData();
                    AddNewRowToGrid();
                }
                else if (Faretype == "P")
                {
                    tbHillRate.Text = dt.Rows[0]["rate_hill"].ToString();
                    tbPlainRate.Text = dt.Rows[0]["rate_plain"].ToString();
                }
                else { }
            }

            Load();
        }
        if (e.CommandName == "activate")
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            string state = gvServiceWiseFare.DataKeys[row.RowIndex]["stateid"].ToString();
            string Servicetype = gvServiceWiseFare.DataKeys[row.RowIndex]["srtp"].ToString();
            Session["state_Id_"] = state;
            Session["service_type_Id_"] = Servicetype;
            Session["Action"] = "Active";
            ConfirmMsg("Do you want to activate State service wise fare?");
        }
        if (e.CommandName == "deactivate")
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            string state = gvServiceWiseFare.DataKeys[row.RowIndex]["stateid"].ToString();
            string Servicetype = gvServiceWiseFare.DataKeys[row.RowIndex]["srtp"].ToString();
            Session["state_Id_"] = state;
            Session["service_type_Id_"] = Servicetype;
            Session["Action"] = "Deactive";
            ConfirmMsg("Do you want to deactivate State service wise fare?");
        }

    }
    protected void gvSlab_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbtnAddNewSlab = (LinkButton)e.Row.FindControl("lbtnAddNewSlab");
            LinkButton lbtnRemoveNewSlab = (LinkButton)e.Row.FindControl("lbtnRemoveNewSlab");

            TextBox tbFromkm = (TextBox)e.Row.FindControl("tbFromkm");
            TextBox tbtokm = (TextBox)e.Row.FindControl("tbtokm");
            TextBox tbhillrate = (TextBox)e.Row.FindControl("tbHillRateSlab");
            TextBox tbPlainRate = (TextBox)e.Row.FindControl("tbPlainRateslab");

            int rindex = int.Parse(hdnTotSlabs.Value.ToString());
            lbtnAddNewSlab.Visible = false;
            lbtnRemoveNewSlab.Visible = false;
            tbFromkm.Enabled = false;
            tbtokm.Enabled = false;
            tbhillrate.Enabled = false;
            tbPlainRate.Enabled = false;


            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];

            if (e.Row.RowIndex == dtCurrentTable.Rows.Count - 1)
            {
                lbtnAddNewSlab.Visible = true;
                lbtnRemoveNewSlab.Visible = false;
                tbFromkm.Enabled = true;
                tbtokm.Enabled = true;
                tbhillrate.Enabled = true;
                tbPlainRate.Enabled = true;

            }
            else if (e.Row.RowIndex == dtCurrentTable.Rows.Count - 2)
            {
                lbtnAddNewSlab.Visible = false;
                lbtnRemoveNewSlab.Visible = true;
            }

        }

    }
    protected void gvSlab_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        CsrfTokenValidate();
        int index = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "Add")
        {
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            TextBox txtFromKM = (TextBox)gvSlab.Rows[RowIndex].FindControl("tbFromkm");
            TextBox txtToKM = (TextBox)gvSlab.Rows[RowIndex].FindControl("tbtokm");
            TextBox txtHillrate = (TextBox)gvSlab.Rows[RowIndex].FindControl("tbHillRateSlab");
            TextBox txtPlainRate = (TextBox)gvSlab.Rows[RowIndex].FindControl("tbPlainRateslab");

            bool validation = ValidaionSlab(txtFromKM.Text, txtToKM.Text, txtHillrate.Text, txtPlainRate.Text);

            if (validation == true)
            {
                if (gvSlab.Rows.Count > 1 && RowIndex >= 1)
                {
                    TextBox txtprevToKM = (TextBox)gvSlab.Rows[RowIndex - 1].FindControl("tbtokm");
                    Int32 FromKM = Convert.ToInt32(txtFromKM.Text.ToString());
                    Int32 prevKM = Convert.ToInt32(txtprevToKM.Text.ToString());
                    if (FromKM < prevKM)
                    {
                        Errormsg("From KM shouldnt be less than previous to km");
                        txtFromKM.Text = "";
                        txtToKM.Text = "";
                        txtHillrate.Text = "";
                        txtPlainRate.Text = "";
                        return;
                    }
                }
                AddNewRowToGrid();
                txtFromKM.Enabled = true;
                txtToKM.Enabled = true;
                txtHillrate.Enabled = true;
                txtPlainRate.Enabled = true;
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
    protected void lbtnSearch_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        getServiceWiseFareList();

    }
    protected void lbtnResetSearch_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        ddlSState.SelectedValue = "0";
        ddlSServiceType.SelectedValue = "0";
        getServiceWiseFareList();
    }
    protected void lbtnReset_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        resetcontrols();
    }
    protected void lbtnCancel_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        resetcontrols();
    }

    #endregion


}
