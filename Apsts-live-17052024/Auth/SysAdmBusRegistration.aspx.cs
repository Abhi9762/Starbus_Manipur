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
using System.Drawing;

public partial class Auth_SysAdmBusRegistration : BasePage
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
        lblSummary.Text = "Summary as on Date " + DateTime.Now.ToString("dd/MM/yyyy hh:ss tt");
        Session["_moduleName"] = "Bus Registration";
        if (!IsPostBack)
        {
            Random randomclass = new Random();
            Session["rndNoCheck"] = randomclass.Next().ToString();
            hidtoken.Value = Session["rndNoCheck"].ToString();
            Session["Invoice"] = null;
            LoadAgency();
            ServiceType();
            LoadBusLayout();
            LoadStore();
            LoadOffice();
            LoadGPS();
            LoadMakeModel();
            LoadOwnershipType();
            LoadBusList();
            BusCount();
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
    private void BusCount()//M1
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_bus_count");
            MyCommand.Parameters.AddWithValue("p_recofc", 0);
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                lblTotalBus.Text = dt.Rows[0]["total"].ToString();
                lblActivateBus.Text = dt.Rows[0]["active"].ToString();
                lblDeactBus.Text = dt.Rows[0]["deactive"].ToString();
                lbtnPermitPending.Text = dt.Rows[0]["permit_p"].ToString();
                lbtnPollutionPending.Text = dt.Rows[0]["pollution_p"].ToString();
                lbtnFitnessPending.Text = dt.Rows[0]["fitness_p"].ToString();
                lbtnInsurancePending.Text = dt.Rows[0]["insurance_p"].ToString();
                lbtnPermitExpired.Text = dt.Rows[0]["permit_e"].ToString();
                lbtnPollutionExpired.Text = dt.Rows[0]["pollution_e"].ToString();
                lbtnFitnessExpired.Text = dt.Rows[0]["fitness_e"].ToString();
                lbtnInsuranceExpired.Text = dt.Rows[0]["insurance_e"].ToString();
                lbtnPermitToBeExpired.Text = dt.Rows[0]["permit_es"].ToString();
                lbtnPollutionToBeExpired.Text = dt.Rows[0]["pollution_es"].ToString();
                lbtnFitnessToBeExpired.Text = dt.Rows[0]["fitness_es"].ToString();
                lbtnInsuranceToBeInsurance.Text = dt.Rows[0]["insurance_es"].ToString();
            }
        }
        catch (Exception ex)
        {
            Errormsg("There is some error" + ex.Message);
            _common.ErrorLog("SysAdmBusRegistration.aspx-0001", ex.Message.ToString());
        }
    }
    private void clearBusData()
    {
        Session["Invoice"] = null;
        Session["_action"] = null;
        Session["_BusID"] = null;
        ImgInvoice.Visible = false;
        tbRegistrationNo.Text = "";
        tbRegistrationNoChar.Text = "";
        tbEngineNO.Text = "";
        tbChasisNo.Text = "";
        tbWheelBase.Text = "";
        tbOdometerReading.Text = "";
        ddlBusServiceType.SelectedValue = "0";
        ddlLayout.SelectedValue = "0";
        ddlOwnerType.SelectedValue = "0";
        ddlMakeModel.SelectedValue = "0";
        ddlGpsYN.SelectedValue = "0";
        ddlGPSCompany.SelectedValue = "0";
        ddlAgency.SelectedValue = "0";
        tbinvoiceNo.Text = "";
        tbinvoiceamt.Text = "";
        ddlStore.SelectedValue = "0";
        ddlDepot.SelectedValue = "0";
        tbInvoiceDate.Text = "";
        tbRecDate.Text = "";
        pnlAddBus.Visible = true;
        pnlViewBus.Visible = false;
        lbtnUpdate.Visible = false;
        lbtnReset.Visible = false;
        lbtnSave.Visible = true;
        lbtnCancel.Visible = true;

    }
    private void saveBus()//M2
    {
        try
        {
            int Odometer = 0, ServiceType = 0, Layout = 0, MakeModel = 0, GPScompany = 0, Agency = 0, InvoiceAmt = 0, Store = 0;
            string registration = "", invoicedate = "", ReceiveOn = "", Status = "A";
            string RegistratedNo = tbRegistrationNo.Text;
            string RegistratedNoChar = tbRegistrationNoChar.Text, Ownership = "0";

            if (Session["_action"].ToString() == "activate")
            {
                Status = "A";
            }
            if (Session["_action"].ToString() == "deactivate")
            {
                Status = "D";
            }
            if (Session["_action"].ToString() == "S")
            {
                registration = RegistratedNoChar + "-" + RegistratedNo;
            }
            if (Session["_action"].ToString() == "U" || Session["_action"].ToString() == "D" || Session["_action"].ToString() == "activate" || Session["_action"].ToString() == "deactivate")
            {
                registration = Session["_BusID"].ToString();
            }
            string EngineNo = tbEngineNO.Text;
            string ChasisNo = tbChasisNo.Text;
            string WheelBase = tbWheelBase.Text;
            string initiOdometer = tbOdometerReading.Text;
            string GPSyn = ddlGpsYN.SelectedValue;
            string Invoice = tbinvoiceNo.Text;
            string Issued = ddlDepot.SelectedValue;
            string IPAddress = HttpContext.Current.Request.UserHostAddress;
            byte[] FileInvoice = (byte[])Session["Invoice"];
            if (!String.IsNullOrEmpty(tbRecDate.Text))
            {
                ReceiveOn = tbRecDate.Text;
            }
            if (!String.IsNullOrEmpty(tbInvoiceDate.Text))
            {
                invoicedate = tbInvoiceDate.Text;
            }
            if (!String.IsNullOrEmpty(tbOdometerReading.Text))
            {
                Odometer = Convert.ToInt32(tbOdometerReading.Text);
            }
            if (!String.IsNullOrEmpty(ddlBusServiceType.SelectedValue))
            {
                ServiceType = Convert.ToInt32(ddlBusServiceType.SelectedValue);
            }
            if (!String.IsNullOrEmpty(ddlLayout.SelectedValue))
            {
                Layout = Convert.ToInt32(ddlLayout.SelectedValue);
            }
            if (!String.IsNullOrEmpty(ddlOwnerType.SelectedValue))
            {
                Ownership = ddlOwnerType.SelectedValue;
            }
            if (!String.IsNullOrEmpty(ddlMakeModel.SelectedValue))
            {
                MakeModel = Convert.ToInt32(ddlMakeModel.SelectedValue);
            }
            if (!String.IsNullOrEmpty(ddlGPSCompany.SelectedValue))
            {
                GPScompany = Convert.ToInt32(ddlGPSCompany.SelectedValue);
            }
            if (!String.IsNullOrEmpty(ddlAgency.SelectedValue))
            {
                Agency = Convert.ToInt32(ddlAgency.SelectedValue);
            }
            if (!String.IsNullOrEmpty(tbinvoiceamt.Text))
            {
                InvoiceAmt = Convert.ToInt32(tbinvoiceamt.Text);
            }
            if (!String.IsNullOrEmpty(ddlStore.SelectedValue))
            {
                Store = Convert.ToInt32(ddlStore.SelectedValue);
            }
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_busregistration_insertupdate");
            MyCommand.Parameters.AddWithValue("p_action", Session["_action"].ToString());
            MyCommand.Parameters.AddWithValue("p_updatedby", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("p_ipaddress", IPAddress);
            MyCommand.Parameters.AddWithValue("p_rno", registration);
            MyCommand.Parameters.AddWithValue("p_engone", EngineNo);
            MyCommand.Parameters.AddWithValue("p_chasisno", ChasisNo);
            MyCommand.Parameters.AddWithValue("p_busservicetype", ServiceType);
            MyCommand.Parameters.AddWithValue("p_layoutcode", Layout);
            MyCommand.Parameters.AddWithValue("p_depot", Issued);
            MyCommand.Parameters.AddWithValue("p_busmake", MakeModel);
            MyCommand.Parameters.AddWithValue("p_gps", GPSyn);
            MyCommand.Parameters.AddWithValue("p_gpscompany", GPScompany);
            MyCommand.Parameters.AddWithValue("p_ownershipcode", Ownership);
            MyCommand.Parameters.AddWithValue("p_initodometerreading", initiOdometer);
            MyCommand.Parameters.AddWithValue("p_odometerreading", Odometer);
            MyCommand.Parameters.AddWithValue("p_wheelbase", WheelBase);
            MyCommand.Parameters.AddWithValue("p_receivingoffice", Store);
            MyCommand.Parameters.AddWithValue("p_receivedondate", ReceiveOn);
            MyCommand.Parameters.AddWithValue("p_buscompany", "1");
            MyCommand.Parameters.AddWithValue("p_agencycode", Agency);
            MyCommand.Parameters.AddWithValue("p_invoiceno", Invoice);
            MyCommand.Parameters.AddWithValue("p_invoicedate", invoicedate);
            MyCommand.Parameters.AddWithValue("p_invoiceamt", InvoiceAmt);
            MyCommand.Parameters.AddWithValue("p_status", Status);
            MyCommand.Parameters.AddWithValue("p_invoiceimg", (object)FileInvoice ?? DBNull.Value);
            string Mresult = bll.UpdateAll(MyCommand);
            if (Mresult == "Success")
            {
                if (Session["_action"].ToString() == "S")
                {
                    Successmsg("Bus Added Successfully.");
                }
                if (Session["_action"].ToString() == "U")
                {
                    Successmsg("Bus Updated Successfully.");
                }
                if (Session["_action"].ToString() == "D")
                {
                    Successmsg("Bus Deleted Successfully.");
                }
                if (Session["_action"].ToString() == "activate")
                {
                    Successmsg("Bus Activated.");
                }
                if (Session["_action"].ToString() == "deactivate")
                {
                    Successmsg("Bus Deactivated.");
                }
                LoadBusList();
                BusCount();
                clearBusData();
            }
            else
            {
                Errormsg(Mresult);
            }
        }
        catch (Exception ex)
        {
            Errormsg("There is some error" + ex.Message);
            _common.ErrorLog("SysAdmBusRegistration.aspx-0002", ex.Message.ToString());
        }
    }
    private void saveOtherBusDetails()
    {
        try
        {
            string busregistrationno, servicetype, usercode, IPAddress, permitNo, permitFrom, permitTo, insurancePolicy, insuranceupto, fitnessCertificate, fitnessfrom, fitnessto, PollutionCertificate, pollutionfrom, pollutionTo;

            usercode = Session["_UserCode"].ToString();
            busregistrationno = Session["_BusID"].ToString();
            servicetype = Session["servicetype"].ToString();
            IPAddress = HttpContext.Current.Request.UserHostAddress;
            permitNo = tbPermitNo.Text;
            permitFrom = tbValidFrom.Text;
            permitTo = tbValidTo.Text;
            insurancePolicy = tbinsuranceno.Text;
            insuranceupto = tbValiduptoInsurance.Text;
            fitnessCertificate = tbCertificateNoFitness.Text;
            fitnessfrom = tbValidFromFitness.Text;
            fitnessto = tbValidtoFitness.Text;
            PollutionCertificate = tbPollutionNo.Text;
            pollutionfrom = tbValidFromPollution.Text;
            pollutionTo = tbValidtoPollution.Text;

            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_busregistration_p_f_i_pc");
            MyCommand.Parameters.AddWithValue("p_action", Session["_action"].ToString());
            MyCommand.Parameters.AddWithValue("p_updatedby", usercode);
            MyCommand.Parameters.AddWithValue("p_ipaddress", IPAddress);
            MyCommand.Parameters.AddWithValue("p_rno", busregistrationno);
            MyCommand.Parameters.AddWithValue("p_entryno", 1);
            MyCommand.Parameters.AddWithValue("p_servicetype", Convert.ToInt32(servicetype));
            MyCommand.Parameters.AddWithValue("p_buspermit", permitNo);
            MyCommand.Parameters.AddWithValue("p_pvalidfrom", permitFrom);
            MyCommand.Parameters.AddWithValue("p_pvalidto", permitTo);
            MyCommand.Parameters.AddWithValue("p_insurancepolicy", insurancePolicy);
            MyCommand.Parameters.AddWithValue("p_ivalidupto", insuranceupto);
            MyCommand.Parameters.AddWithValue("p_fcertificate", fitnessCertificate);
            MyCommand.Parameters.AddWithValue("p_fvalidfrom", fitnessfrom);
            MyCommand.Parameters.AddWithValue("p_fvalidto", fitnessto);
            MyCommand.Parameters.AddWithValue("p_pc_certificate", PollutionCertificate);
            MyCommand.Parameters.AddWithValue("p_pcvalidfrom", pollutionfrom);
            MyCommand.Parameters.AddWithValue("p_pcvalidto", pollutionTo);

            string Mresult = bll.UpdateAll(MyCommand);
            if (Mresult == "Success")
            {
                if (Session["_action"].ToString() == "P")
                {
                    PermitList();
                    Successmsg("Bus Permit Details Added Successfully.");
                }
                if (Session["_action"].ToString() == "I")
                {
                    InsuranceList();
                    Successmsg("Bus Insurance Details Added Successfully.");
                }
                if (Session["_action"].ToString() == "F")
                {
                    FitnessList();
                    Successmsg("Bus Fitness Details Added Successfully.");
                }
                if (Session["_action"].ToString() == "PC")
                {
                    PollutionList();
                    Successmsg("Bus Pollution Details Added Successfully.");
                }
                clearPUCData();
                BusCount();
                LoadBusList();
            }
            else
            {
                Errormsg(Mresult);
            }
        }
        catch (Exception ex)
        {
            Errormsg("There is some error" + ex.Message);
            _common.ErrorLog("SysAdmBusRegistration.aspx-0003", ex.Message.ToString());
        }
    }
    public void LoadAgency()
    {
        try
        {
            ddlAgency.Items.Clear();

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_agencies_fordropdown");
            MyCommand.Parameters.AddWithValue("p_itemid", "2");
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                ddlAgency.DataSource = dt;
                ddlAgency.DataTextField = "agencyname";
                ddlAgency.DataValueField = "agency_id";
                ddlAgency.DataBind();
            }
            ddlAgency.Items.Insert(0, "SELECT");
            ddlAgency.Items[0].Value = "0";
            ddlAgency.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlAgency.Items.Insert(0, "SELECT");
            ddlAgency.Items[0].Value = "0";
            ddlAgency.SelectedIndex = 0;
            Errormsg("There is some error" + ex.Message);
            _common.ErrorLog("SysAdmBusRegistration.aspx-0004", ex.Message.ToString());
        }
    }
    public void LoadMakeModel()
    {
        try
        {
            ddlMakeModel.Items.Clear();

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_makemodel");

            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                ddlMakeModel.DataSource = dt;
                ddlMakeModel.DataTextField = "make_name";
                ddlMakeModel.DataValueField = "make_id";
                ddlMakeModel.DataBind();
            }
            ddlMakeModel.Items.Insert(0, "SELECT");
            ddlMakeModel.Items[0].Value = "0";
            ddlMakeModel.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlMakeModel.Items.Insert(0, "SELECT");
            ddlMakeModel.Items[0].Value = "0";
            ddlMakeModel.SelectedIndex = 0;
            Errormsg("There is some error" + ex.Message);
            _common.ErrorLog("SysAdmBusRegistration.aspx-0005", ex.Message.ToString());
        }
    }
    public void LoadOwnershipType()
    {
        try
        {
            ddlOwnerType.Items.Clear();

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_bustype");

            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                ddlOwnerType.DataSource = dt;
                ddlOwnerType.DataTextField = "bustype_name";
                ddlOwnerType.DataValueField = "bustype_id";
                ddlOwnerType.DataBind();
            }
            ddlOwnerType.Items.Insert(0, "SELECT");
            ddlOwnerType.Items[0].Value = "0";
            ddlOwnerType.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlOwnerType.Items.Insert(0, "SELECT");
            ddlOwnerType.Items[0].Value = "0";
            ddlOwnerType.SelectedIndex = 0;
            Errormsg("There is some error" + ex.Message);
            _common.ErrorLog("SysAdmBusRegistration.aspx-0006", ex.Message.ToString());
        }
    }
    public void LoadGPS()
    {
        try
        {
            ddlGPSCompany.Items.Clear();

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_gpslist");

            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                ddlGPSCompany.DataSource = dt;
                ddlGPSCompany.DataTextField = "gpscompany_name";
                ddlGPSCompany.DataValueField = "gpscompany_id";
                ddlGPSCompany.DataBind();
            }
            ddlGPSCompany.Items.Insert(0, "SELECT");
            ddlGPSCompany.Items[0].Value = "0";
            ddlGPSCompany.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlGPSCompany.Items.Insert(0, "SELECT");
            ddlGPSCompany.Items[0].Value = "0";
            ddlGPSCompany.SelectedIndex = 0;
            Errormsg("There is some error" + ex.Message);
            _common.ErrorLog("SysAdmBusRegistration.aspx-0007", ex.Message.ToString());
        }
    }
    public void LoadBusLayout()
    {
        try
        {
            ddlLayout.Items.Clear();

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_buslayout");

            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                ddlLayout.DataSource = dt;
                ddlLayout.DataTextField = "layoutname";
                ddlLayout.DataValueField = "layoutcode";
                ddlLayout.DataBind();
            }
            ddlLayout.Items.Insert(0, "SELECT");
            ddlLayout.Items[0].Value = "0";
            ddlLayout.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlLayout.Items.Insert(0, "SELECT");
            ddlLayout.Items[0].Value = "0";
            ddlLayout.SelectedIndex = 0;
            Errormsg("There is some error" + ex.Message);
            _common.ErrorLog("SysAdmBusRegistration.aspx-0008", ex.Message.ToString());
        }

    }
    public void LoadOffice()
    {
        try
        {
            ddlDepot.Items.Clear();
            ddlSDepot.Items.Clear();
            Int32 ofcLvl = 30;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_ofclvlwiseoffice");
            MyCommand.Parameters.AddWithValue("p_ofclvlid", ofcLvl);
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                ddlDepot.DataSource = dt;
                ddlDepot.DataTextField = "officename";
                ddlDepot.DataValueField = "officeid";
                ddlDepot.DataBind();
                ddlSDepot.DataSource = dt;
                ddlSDepot.DataTextField = "officename";
                ddlSDepot.DataValueField = "officeid";
                ddlSDepot.DataBind();
            }
            ddlDepot.Items.Insert(0, "SELECT");
            ddlDepot.Items[0].Value = "0";
            ddlDepot.SelectedIndex = 0;
            ddlSDepot.Items.Insert(0, "SELECT");
            ddlSDepot.Items[0].Value = "0";
            ddlSDepot.SelectedIndex = 0;

        }
        catch (Exception ex)
        {
            ddlDepot.Items.Insert(0, "SELECT");
            ddlDepot.Items[0].Value = "0";
            ddlDepot.SelectedIndex = 0;
            ddlSDepot.Items.Insert(0, "SELECT");
            ddlSDepot.Items[0].Value = "0";
            ddlSDepot.SelectedIndex = 0;
            Errormsg("There is some error" + ex.Message);
            _common.ErrorLog("SysAdmBusRegistration.aspx-0009", ex.Message.ToString());
        }

    }
    public void LoadStore()
    {
        try
        {
            ddlStore.Items.Clear();

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_store_fordropdown");
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                ddlStore.DataSource = dt;
                ddlStore.DataTextField = "store_name";
                ddlStore.DataValueField = "store_id";
                ddlStore.DataBind();
            }
            ddlStore.Items.Insert(0, "SELECT");
            ddlStore.Items[0].Value = "0";
            ddlStore.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlStore.Items.Insert(0, "SELECT");
            ddlStore.Items[0].Value = "0";
            ddlStore.SelectedIndex = 0;
            Errormsg("There is some error" + ex.Message);
            _common.ErrorLog("SysAdmBusRegistration.aspx-0010", ex.Message.ToString());
        }

    }
    public void ServiceType()
    {
        try
        {
            ddlBusServiceType.Items.Clear();
            ddlSServiceType.Items.Clear();

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_servicetype");

            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                ddlBusServiceType.DataSource = dt;
                ddlBusServiceType.DataTextField = "service_type_nameen";
                ddlBusServiceType.DataValueField = "srtpid";
                ddlBusServiceType.DataBind();
                ddlSServiceType.DataSource = dt;
                ddlSServiceType.DataTextField = "service_type_nameen";
                ddlSServiceType.DataValueField = "srtpid";
                ddlSServiceType.DataBind();
            }
            ddlBusServiceType.Items.Insert(0, "SELECT");
            ddlBusServiceType.Items[0].Value = "0";
            ddlBusServiceType.SelectedIndex = 0;
            ddlSServiceType.Items.Insert(0, "All");
            ddlSServiceType.Items[0].Value = "0";
            ddlSServiceType.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlBusServiceType.Items.Insert(0, "SELECT");
            ddlBusServiceType.Items[0].Value = "0";
            ddlBusServiceType.SelectedIndex = 0;
            ddlSServiceType.Items.Insert(0, "All");
            ddlSServiceType.Items[0].Value = "0";
            ddlSServiceType.SelectedIndex = 0;
            Errormsg("There is some error" + ex.Message);
            _common.ErrorLog("SysAdmBusRegistration.aspx-0011", ex.Message.ToString());
        }
    }
    protected void LoadBusList()
    {
        try
        {
            string Search = "", Depot = "", status = "", statusType = "";
            int ServiceType = 0;
            Search = tbSSerialNo.Text;
            ServiceType = Convert.ToInt32(ddlSServiceType.SelectedValue);
            Depot = ddlSDepot.SelectedValue;
            status = ddlSStatus.SelectedValue;
            statusType = ddlSType.SelectedValue;


            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_buseslist");
            MyCommand.Parameters.AddWithValue("p_depotcode", Depot);
            MyCommand.Parameters.AddWithValue("p_recofc", "0");
            MyCommand.Parameters.AddWithValue("p_service", ServiceType);
            MyCommand.Parameters.AddWithValue("p_searchtxt", Search);
            MyCommand.Parameters.AddWithValue("p_status", status);
            MyCommand.Parameters.AddWithValue("p_type", statusType);

            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                gvBusDetails.DataSource = dt;
                gvBusDetails.DataBind();
                gvBusDetails.Visible = true;
                pnlnoRecordfound.Visible = false;
            }
            else
            {
                gvBusDetails.Visible = false;
                pnlnoRecordfound.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Errormsg("There is some error" + ex.Message);
            _common.ErrorLog("SysAdmBusRegistration.aspx-0012", ex.Message.ToString());
        }
    }
    protected void PermitList()//M13
    {
        try
        {
            string busRegNo = Session["_BusID"].ToString();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_permitdetails");
            MyCommand.Parameters.AddWithValue("p_busregno", busRegNo);
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                gvBusPermit.DataSource = dt;
                gvBusPermit.DataBind();
                gvBusPermit.Visible = true;
                pnlNoPermitRecord.Visible = false;
            }
            else
            {
                gvBusPermit.Visible = false;
                pnlNoPermitRecord.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Errormsg("There is some error" + ex.Message);
            _common.ErrorLog("SysAdmBusRegistration.aspx-0013", ex.Message.ToString());
        }
    }
    protected void FitnessList()
    {
        try
        {
            string busRegNo = Session["_BusID"].ToString();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_fitnessdetails");
            MyCommand.Parameters.AddWithValue("p_busregno", busRegNo);
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                gvBusFitness.DataSource = dt;
                gvBusFitness.DataBind();
                gvBusFitness.Visible = true;
                pnlNoFitnessRecord.Visible = false;
            }
            else
            {
                gvBusFitness.Visible = false;
                pnlNoFitnessRecord.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Errormsg("There is some error" + ex.Message);
            _common.ErrorLog("SysAdmBusRegistration.aspx-0014", ex.Message.ToString());
        }
    }
    protected void InsuranceList()
    {
        try
        {
            string busRegNo = Session["_BusID"].ToString();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_insurancedetails");
            MyCommand.Parameters.AddWithValue("p_busregno", busRegNo);
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                gvBusInsurance.DataSource = dt;
                gvBusInsurance.DataBind();
                gvBusInsurance.Visible = true;
                pnlNoInsuranceRecord.Visible = false;
            }
            else
            {
                gvBusInsurance.Visible = false;
                pnlNoInsuranceRecord.Visible = true;
            }
        }
        catch (Exception ex)
        {
           
            _common.ErrorLog("SysAdmBusRegistration.aspx-0015", ex.Message.ToString());
            Errormsg("There is some error" + ex.Message);
        }
    }
    protected void PollutionList()
    {
        try
        {
            string busRegNo = Session["_BusID"].ToString();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_pollutiondetails");
            MyCommand.Parameters.AddWithValue("p_busregno", busRegNo);
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                gvBusPollution.DataSource = dt;
                gvBusPollution.DataBind();
                gvBusPollution.Visible = true;
                pnlNoPollutionFitness.Visible = false;
            }
            else
            {
                gvBusPollution.Visible = false;
                pnlNoPollutionFitness.Visible = true;
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("SysAdmBusRegistration.aspx-0016", ex.Message.ToString());
            Errormsg("There is some error" + ex.Message);
        }
    }
    public void clearPUCData()
    {
        tbPermitNo.Text = "";
        tbPollutionNo.Text = "";
        tbCertificateNoFitness.Text = "";
        tbinsuranceno.Text = "";
        tbValidFrom.Text = "";
        tbValidFromFitness.Text = "";
        tbValidFromPollution.Text = "";
        tbValidTo.Text = "";
        tbValidtoFitness.Text = "";
        tbValiduptoInsurance.Text = "";
        tbValidtoPollution.Text = "";
        tbValidFrom.Text = "";
    }
    public byte[] convertByteFile(System.Web.UI.WebControls.FileUpload fuFileUpload)
    {
        int intFileLength;
        byte[] byteData = null;
        if (fuFileUpload.HasFile)
        {

            // Check File Extention
            if (CheckFileExtention(fuFileUpload) == true)
            {
                intFileLength = fuFileUpload.PostedFile.ContentLength;
                byteData = new byte[intFileLength + 1];
                byteData = fuFileUpload.FileBytes;
            }
        }
        else
        {
            intFileLength = fuFileUpload.PostedFile.ContentLength;
            byteData = new byte[intFileLength + 1];
            byteData = fuFileUpload.FileBytes;
        }
        return byteData;
    }
    private void ExportRefundTransToExcel()
    {
        Response.Clear();
        Response.Buffer = true;
        gvBusDetails.AllowPaging = false;
        this.LoadBusList();
        if (gvBusDetails.Rows.Count > 0)
        {
            Response.AddHeader("content-disposition", "attachment;filename=BusList.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                // To Export all pages

                gvBusDetails.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in gvBusDetails.HeaderRow.Cells)
                    cell.BackColor = gvBusDetails.HeaderStyle.BackColor;
                foreach (GridViewRow row in gvBusDetails.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                            cell.BackColor = gvBusDetails.AlternatingRowStyle.BackColor;
                        else
                            cell.BackColor = gvBusDetails.RowStyle.BackColor;
                        cell.CssClass = "textmode";
                    }
                }

                gvBusDetails.RenderControl(hw);
                // style to format numbers to string
                string style = "<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
        else
        {
            Errormsg("Sorry, no record is available.");
            return;
        }
    }
    public string GetImage(object img)
    {
        try
        {
            return "data:image/jpg;base64," + Convert.ToBase64String((byte[])img);
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public static string GetMimeDataOfFile(HttpPostedFile file)
    {
        IntPtr mimeout = default(IntPtr);
        int MaxContent = Convert.ToInt32(file.ContentLength);
        if (MaxContent > 4096)
        {
            MaxContent = 4096;
        }

        byte[] buf = new byte[MaxContent - 1 + 1];
        file.InputStream.Read(buf, 0, MaxContent);
        int MimeSampleSize = 256;


        string mimeType = System.Web.MimeMapping.GetMimeMapping(file.FileName);
        return mimeType;
    }
    private bool CheckFileExtention(FileUpload FileInvoice)
    {
        try
        {
            bool fileExtensionOK = false;
            string fileExtension;
            fileExtension = System.IO.Path.GetExtension(FileInvoice.FileName).ToLower();
            string[] allowedExtensions = new[] { ".jpg", ".png", ".jpeg" };
            for (int i = 0; i <= allowedExtensions.Length - 1; i++)
            {
                if (fileExtension == allowedExtensions[i])
                {
                    fileExtensionOK = true;
                }
            }
            return fileExtensionOK;
        }
        catch (Exception ex)
        {
            Errormsg("Something went wrong with image (allow only .jpg, .png, .jpeg image). ");
            return default(Boolean);
        }
    }
    private Boolean Validaion()
    {
        try
        {
            int count = 0;
            string msg = "";


            if (_validation.IsValidString(tbRegistrationNoChar.Text, 3, 6) == false)
            {
                count++;
                msg = msg + count + ". Enter Valid Registration No <br/>";
            }
            if (_validation.IsValidString(tbRegistrationNo.Text, 4, 4) == false)
            {
                count++;
                msg = msg + count + ". Enter Valid Registration No <br/>";
            }

            if (_validation.IsValidString(tbChasisNo.Text, 6, 20) == false)
            {
                count++;
                msg = msg + count + ".  Enter Valid Chasis No.<br/>";
            }
            if (tbInvoiceDate.Text == "")
            { }
            else
            {
                if (_validation.IsDate(tbInvoiceDate.Text) == false)
                {
                    count++;
                    msg = msg + count + ". Enter Valid invoice Date <br/>";
                }
            }
            if (_validation.IsDate(tbRecDate.Text) == false)
            {
                count++;
                msg = msg + count + ". Enter Valid Receiving Date <br/>";
            }
            if (_validation.IsValidString(tbWheelBase.Text, 2, 10) == false)
            {
                count++;
                msg = msg + count + ". Enter Valid Wheel Base <br/>";
            }
            if (_validation.IsValidString(tbOdometerReading.Text, 2, 10) == false)
            {
                count++;
                msg = msg + count + ". Enter Odometer Reading <br/>";
            }
            if (_validation.IsValidString(tbinvoiceamt.Text, 0, 10) == false)
            {
                count++;
                msg = msg + count + ". Enter Invoice Amt <br/>";
            }
            if (_validation.IsValidString(tbinvoiceNo.Text, 0, 10) == false)
            {
                count++;
                msg = msg + count + ". Enter Invoice No. <br/>";
            }
            if (ddlLayout.SelectedValue == "0")
            {
                count++;
                msg = msg + count + ". Select Bus Layout Type <br/>";
            }
            if (ddlBusServiceType.SelectedValue == "0")
            {
                count++;
                msg = msg + count + ". Select Bus Service Type <br/>";
            }
            if (ddlOwnerType.SelectedValue == "0")
            {
                count++;
                msg = msg + count + ". Select Ownership Type <br/>";
            }
            if (ddlMakeModel.SelectedValue == "0")
            {
                count++;
                msg = msg + count + ". Select Make Model <br/>";
            }
            if (ddlGpsYN.SelectedValue == "0")
            {
                count++;
                msg = msg + count + ". Select if GPS(Yes/No) <br/>";
            }
            if (ddlGpsYN.SelectedValue == "Y")
            {
                if (ddlGPSCompany.SelectedValue == "0")
                {
                    count++;
                    msg = msg + count + ". Select if GPS company Name <br/>";
                }
            }
            //if (ddlAgency.SelectedValue == "0")
            //{
            //    count++;
            //    msg = msg + count + ". Select Agency Name <br/>";
            //}
            if (ddlStore.SelectedValue == "0")
            {
                count++;
                msg = msg + count + ". Select Store Name <br/>";
            }

            if (ddlDepot.SelectedValue == "0")
            {
                count++;
                msg = msg + count + ". Select Depot Name <br/>";
            }
            if (_validation.IsDate(tbRecDate.Text) == true)
            {
                DateTime receivedOnDate = DateTime.ParseExact(tbRecDate.Text, "dd/MM/yyyy", null);
                if (receivedOnDate > DateTime.Now.Date)
                {
                    count = count + 1;
                    msg = msg + count.ToString() + ". Receiving Date should be less than today's date.<br/>";
                }
            }
            if (tbInvoiceDate.Text == "")
            {

            }
            else
            {
                if (_validation.IsDate(tbInvoiceDate.Text) == true)
                {
                    DateTime invoiceDate = DateTime.ParseExact(tbInvoiceDate.Text, "dd/MM/yyyy", null);
                    if (invoiceDate > DateTime.Now.Date)
                    {
                        count = count + 1;
                        msg = msg + count.ToString() + ". Invoice Date should be less than today's date.<br/>";
                    }
                }
            }
            if (_validation.IsDate(tbRecDate.Text) == true && _validation.IsDate(tbInvoiceDate.Text) == true)
            {
                DateTime receivedOnDate = DateTime.ParseExact(tbRecDate.Text, "dd/MM/yyyy", null);
                DateTime invoiceDate = DateTime.ParseExact(tbInvoiceDate.Text, "dd/MM/yyyy", null);
                if (invoiceDate > receivedOnDate)
                {
                    count = count + 1;
                    msg = msg + count.ToString() + ". Receiving Date should be less than invoice date.<br/>";
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

            return false;
        }
    }
    private Boolean ValidPermit()
    {
        try
        {
            int count = 0;
            string msg = "";


            if (_validation.IsValidString(tbPermitNo.Text, 6, 20) == false)
            {
                count++;
                msg = msg + count + ". Enter Valid Permit No <br/>";
            }

            if (_validation.IsDate(tbValidFrom.Text) == false)
            {
                count = count + 1;
                msg = msg + count.ToString() + ". Valid from date.<br/>";
            }
            if (_validation.IsDate(tbValidTo.Text) == false)
            {
                count = count + 1;
                msg = msg + count.ToString() + ". Valid to date.<br/>";
            }
            else
            {
                DateTime ValidFrom = DateTime.ParseExact(tbValidFrom.Text, "dd/MM/yyyy", null);
                DateTime Validto = DateTime.ParseExact(tbValidTo.Text, "dd/MM/yyyy", null);


                if (ValidFrom > Validto)
                {
                    count = count + 1;
                    msg = msg + count.ToString() + ". To Date should be greater than From date.<br/>";
                }
                DateTime currDate = DateTime.Now.Date;
                if (currDate > Validto)
                {
                    count = count + 1;
                    msg = msg + count.ToString() + ". Check  Valid to Date.  Valid to should be greater than Today's Date.<br/>";
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

            return false;
        }

    }
    private Boolean ValidFitness()
    {
        try
        {
            int count = 0;
            string msg = "";


            if (_validation.IsValidString(tbCertificateNoFitness.Text, 6, 20) == false)
            {
                count++;
                msg = msg + count + ". Enter Valid Fitness Certificate No <br/>";
            }

            if (_validation.IsDate(tbValidFromFitness.Text) == false)
            {
                count = count + 1;
                msg = msg + count.ToString() + ". Valid from date.<br/>";
            }
            if (_validation.IsDate(tbValidtoFitness.Text) == false)
            {
                count = count + 1;
                msg = msg + count.ToString() + ". Valid to date.<br/>";
            }
            else
            {
                DateTime ValidFrom = DateTime.ParseExact(tbValidFromFitness.Text, "dd/MM/yyyy", null);
                DateTime Validto = DateTime.ParseExact(tbValidtoFitness.Text, "dd/MM/yyyy", null);


                if (ValidFrom > Validto)
                {
                    count = count + 1;
                    msg = msg + count.ToString() + ". To Date should be greater than From date.<br/>";
                }
                DateTime currDate = DateTime.Now.Date;
                if (currDate > Validto)
                {
                    count = count + 1;
                    msg = msg + count.ToString() + ". Check  Valid to Date.  Valid to should be greater than Today's Date.<br/>";
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

            return false;
        }

    }
    private Boolean ValidPollution()
    {
        try
        {
            int count = 0;
            string msg = "";


            if (_validation.IsValidString(tbPollutionNo.Text, 6, 20) == false)
            {
                count++;
                msg = msg + count + ". Enter Valid Pollution No <br/>";
            }

            if (_validation.IsDate(tbValidFromPollution.Text) == false)
            {
                count = count + 1;
                msg = msg + count.ToString() + ". Valid from date.<br/>";
            }
            if (_validation.IsDate(tbValidtoPollution.Text) == false)
            {
                count = count + 1;
                msg = msg + count.ToString() + ". Valid to date.<br/>";
            }
            else
            {
                DateTime ValidFrom = DateTime.ParseExact(tbValidFromPollution.Text, "dd/MM/yyyy", null);
                DateTime Validto = DateTime.ParseExact(tbValidtoPollution.Text, "dd/MM/yyyy", null);

                if (ValidFrom > Validto)
                {
                    count = count + 1;
                    msg = msg + count.ToString() + ". To Date should be greater than From date.<br/>";
                }
                DateTime currDate = DateTime.Now.Date;
                if (currDate > Validto)
                {
                    count = count + 1;
                    msg = msg + count.ToString() + ". Check  Valid to Date.  Valid to should be greater than Today's Date.<br/>";
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

            return false;
        }

    }
    private Boolean ValidInsurance()
    {
        try
        {
            int count = 0;
            string msg = "";


            if (_validation.IsValidString(tbinsuranceno.Text, 6, 20) == false)
            {
                count++;
                msg = msg + count + ". Enter Valid Insurance Certificate No <br/>";
            }

            if (_validation.IsDate(tbValiduptoInsurance.Text) == false)
            {
                count = count + 1;
                msg = msg + count.ToString() + ". Valid to date.<br/>";
            }
            else
            {
                DateTime validuptodate = DateTime.ParseExact(tbValiduptoInsurance.Text, "dd/MM/yyyy", null);
                DateTime currDate = DateTime.Now.Date;
                if (currDate > validuptodate)
                {
                    count = count + 1;
                    msg = msg + count.ToString() + ". Check  Valid to Date.  Valid to should be greater than Today's Date.<br/>";
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

            return false;
        }

    }
    #endregion
    #region "Events"
    protected void gvBusDetails_RowCommand(object sender, GridViewCommandEventArgs e)//E1
    {
        CsrfTokenValidate();
        try
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string BusID;
            clearBusData();
            if (e.CommandName == "updateBus")
            {
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                BusID = gvBusDetails.DataKeys[row.RowIndex]["busregistration_no"].ToString();
                Session["_BusID"] = BusID;
                lblBusUpdate.Text = "Update details of Bus- " + BusID + " (" + gvBusDetails.DataKeys[row.RowIndex]["servicetype_nameen"].ToString() + ")";

              String[] arrbus = BusID.Split('-');
                tbRegistrationNoChar.Text = arrbus[0].ToString();//BusID.Substring(0, 6);
                tbRegistrationNo.Text = arrbus[1].ToString();//BusID.Substring(7, BusID.Length - 7);
                tbEngineNO.Text = gvBusDetails.DataKeys[row.RowIndex]["engine_no"].ToString();
                tbChasisNo.Text = gvBusDetails.DataKeys[row.RowIndex]["chasis_no"].ToString();
                tbWheelBase.Text = gvBusDetails.DataKeys[row.RowIndex]["wheel_base"].ToString();
                tbOdometerReading.Text = gvBusDetails.DataKeys[row.RowIndex]["init_odometerreading"].ToString();
                ddlBusServiceType.SelectedValue = gvBusDetails.DataKeys[row.RowIndex]["srtpid"].ToString();
                ddlLayout.SelectedValue = gvBusDetails.DataKeys[row.RowIndex]["layoutcode"].ToString();
                ddlOwnerType.SelectedValue = gvBusDetails.DataKeys[row.RowIndex]["ownertype"].ToString();
                ddlMakeModel.SelectedValue = gvBusDetails.DataKeys[row.RowIndex]["makeid"].ToString();
                ddlGpsYN.SelectedValue = gvBusDetails.DataKeys[row.RowIndex]["gps_yn"].ToString();
                if (ddlGpsYN.SelectedValue == "Y")
                {
                    ddlGPSCompany.SelectedValue = gvBusDetails.DataKeys[row.RowIndex]["gpscompanyid"].ToString();
                }
                ddlAgency.SelectedValue = gvBusDetails.DataKeys[row.RowIndex]["agency"].ToString();
                tbinvoiceNo.Text = gvBusDetails.DataKeys[row.RowIndex]["invoice_no"].ToString();
                tbInvoiceDate.Text = gvBusDetails.DataKeys[row.RowIndex]["invoice_date"].ToString();
                tbinvoiceamt.Text = gvBusDetails.DataKeys[row.RowIndex]["invoice_amt"].ToString();
                ddlStore.SelectedValue = gvBusDetails.DataKeys[row.RowIndex]["store"].ToString();
                tbRecDate.Text = gvBusDetails.DataKeys[row.RowIndex]["receivedon_date"].ToString();
                ddlDepot.SelectedValue = gvBusDetails.DataKeys[row.RowIndex]["officeid"].ToString();
                if (ddlGpsYN.SelectedValue.ToString() == "Y")
                {
                    lblGPSCompany.Visible = true;
                    ddlGPSCompany.Visible = true;
                    ddlGPSCompany.SelectedValue = gvBusDetails.DataKeys[row.RowIndex]["gpscompanyid"].ToString();
                }
                else
                {
                    lblGPSCompany.Visible = false;
                    ddlGPSCompany.Visible = false;
                }
                Session["_action"] = "U";
                lbtnSave.Visible = false;
                lbtnUpdate.Visible = true;
                lbtnReset.Visible = false;
                lbtnCancel.Visible = true;
                pnlViewBus.Visible = false;
                pnlAddBus.Visible = true;
                lblBusHead.Visible = false;
                lblBusUpdate.Visible = true;
                pnlViewBusDetails.Visible = false;
                pnlUpdatePermit.Visible = false;
                pnlUpdateFitness.Visible = false;
                pnlUpdateInsurance.Visible = false;
                pnlUpdatePollution.Visible = false;

            }
            if (e.CommandName == "deleteBus")
            {
                pnlViewBusDetails.Visible = false;
                pnlAddBus.Visible = true;
                pnlUpdatePermit.Visible = false;
                pnlUpdateFitness.Visible = false;
                pnlUpdateInsurance.Visible = false;
                pnlUpdatePollution.Visible = false;
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                BusID = gvBusDetails.DataKeys[row.RowIndex]["busregistration_no"].ToString();
                Session["_BusID"] = BusID;
                Session["_action"] = "delete";
                Session["Invoice"] = null;
                ConfirmMsg("Do you want to delete Bus details?");
            }
            if (e.CommandName == "activate")
            {
                pnlViewBusDetails.Visible = false;
                pnlAddBus.Visible = true;
                pnlUpdatePermit.Visible = false;
                pnlUpdateFitness.Visible = false;
                pnlUpdateInsurance.Visible = false;
                pnlUpdatePollution.Visible = false;
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                BusID = gvBusDetails.DataKeys[row.RowIndex]["busregistration_no"].ToString();
                Session["_BusID"] = BusID;
                Session["_action"] = "activate";
                ConfirmMsg("Do you want to activate Bus details?");
            }
            if (e.CommandName == "deactivate")
            {
                pnlViewBusDetails.Visible = false;
                pnlAddBus.Visible = true;
                pnlUpdatePermit.Visible = false;
                pnlUpdateFitness.Visible = false;
                pnlUpdateInsurance.Visible = false;
                pnlUpdatePollution.Visible = false;
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                BusID = gvBusDetails.DataKeys[row.RowIndex]["busregistration_no"].ToString();
                Session["_action"] = "deactivate";
                Session["_BusID"] = BusID;
                ConfirmMsg("Do you want to deactivate Bus details?");
            }
            if (e.CommandName == "Permit")
            {
                pnlViewBusDetails.Visible = false;
                pnlAddBus.Visible = false;
                pnlUpdatePermit.Visible = true;
                pnlUpdateFitness.Visible = false;
                pnlUpdateInsurance.Visible = false;
                pnlUpdatePollution.Visible = false;
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                BusID = gvBusDetails.DataKeys[row.RowIndex]["busregistration_no"].ToString();
                int BusService = Convert.ToInt32(gvBusDetails.DataKeys[row.RowIndex]["srtpid"].ToString());
                lblPermit.Text = "Permit details of Bus- " + BusID + " (" + gvBusDetails.DataKeys[row.RowIndex]["servicetype_nameen"].ToString() + ")";

                Session["_BusID"] = BusID;
                Session["servicetype"] = BusService;
                PermitList();

            }
            if (e.CommandName == "Fitness")
            {
                pnlViewBusDetails.Visible = false;
                pnlAddBus.Visible = false;
                pnlUpdatePermit.Visible = false;
                pnlUpdateFitness.Visible = true;
                pnlUpdateInsurance.Visible = false;
                pnlUpdatePollution.Visible = false;
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                BusID = gvBusDetails.DataKeys[row.RowIndex]["busregistration_no"].ToString();
                int BusService = Convert.ToInt32(gvBusDetails.DataKeys[row.RowIndex]["srtpid"].ToString());
                lblfitness.Text = "Fitness details of Bus- " + BusID + " (" + gvBusDetails.DataKeys[row.RowIndex]["servicetype_nameen"].ToString() + ")";

                Session["_BusID"] = BusID;
                Session["servicetype"] = BusService;
                FitnessList();


            }
            if (e.CommandName == "Insurance")
            {

                pnlViewBusDetails.Visible = false;
                pnlAddBus.Visible = false;
                pnlUpdatePermit.Visible = false;
                pnlUpdateFitness.Visible = false;
                pnlUpdateInsurance.Visible = true;
                pnlUpdatePollution.Visible = false;
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                BusID = gvBusDetails.DataKeys[row.RowIndex]["busregistration_no"].ToString();
                int BusService = Convert.ToInt32(gvBusDetails.DataKeys[row.RowIndex]["srtpid"].ToString());
                lblInsurance.Text = "Insurance details of Bus- " + BusID + " (" + gvBusDetails.DataKeys[row.RowIndex]["servicetype_nameen"].ToString() + ")";

                Session["servicetype"] = BusService;
                Session["_BusID"] = BusID;
                InsuranceList();
            }
            if (e.CommandName == "Pollution")
            {
                pnlViewBusDetails.Visible = false;
                pnlAddBus.Visible = false;
                pnlUpdatePermit.Visible = false;
                pnlUpdateFitness.Visible = false;
                pnlUpdateInsurance.Visible = false;
                pnlUpdatePollution.Visible = true;

                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                BusID = gvBusDetails.DataKeys[row.RowIndex]["busregistration_no"].ToString();
                int BusService = Convert.ToInt32(gvBusDetails.DataKeys[row.RowIndex]["srtpid"].ToString());
                lblPollution.Text = "Pollution details of Bus- " + BusID + " (" + gvBusDetails.DataKeys[row.RowIndex]["servicetype_nameen"].ToString() + ")";

                Session["servicetype"] = BusService;
                Session["_BusID"] = BusID;
                PollutionList();

            }
            if (e.CommandName == "viewBus")
            {
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                BusID = gvBusDetails.DataKeys[row.RowIndex]["busregistration_no"].ToString();
                Session["_BusID"] = BusID;
                lblViewBusHead.Text = "View details of Bus- " + BusID + " (" + gvBusDetails.DataKeys[row.RowIndex]["servicetype_nameen"].ToString() + ")";
                lblEngineNo.Text = gvBusDetails.DataKeys[row.RowIndex]["engine_no"].ToString();
                lblChasisNo.Text = gvBusDetails.DataKeys[row.RowIndex]["chasis_no"].ToString();
                lblWheelBase.Text = gvBusDetails.DataKeys[row.RowIndex]["wheel_base"].ToString();
                lblInitialOdometer.Text = gvBusDetails.DataKeys[row.RowIndex]["init_odometerreading"].ToString();
                lblLayout.Text = gvBusDetails.DataKeys[row.RowIndex]["layoutname"].ToString();
                lblOwnership.Text = gvBusDetails.DataKeys[row.RowIndex]["bus_type"].ToString();
                lblMakeModel.Text = gvBusDetails.DataKeys[row.RowIndex]["make_name"].ToString();
                string GPSyn = gvBusDetails.DataKeys[row.RowIndex]["gps_yn"].ToString();
                if (GPSyn == "Y")
                {
                    divGps.Visible = true;
                    lblGpsYN.Text = "Yes";
                    lblGPSCompany.Text = gvBusDetails.DataKeys[row.RowIndex]["gpscompany_name"].ToString();
                }
                else
                {
                    divGps.Visible = false;
                    lblGpsYN.Text = "No";
                    lblGPSCompany.Text = "";
                }
                lblAgency.Text = gvBusDetails.DataKeys[row.RowIndex]["name"].ToString();
                lblInvoiceNo.Text = gvBusDetails.DataKeys[row.RowIndex]["invoice_no"].ToString();
                lblInvoiceDate.Text = gvBusDetails.DataKeys[row.RowIndex]["invoice_date"].ToString();
                lblInvoiceAmt.Text = gvBusDetails.DataKeys[row.RowIndex]["invoice_amt"].ToString();
                lblReceivingStore.Text = gvBusDetails.DataKeys[row.RowIndex]["store_name"].ToString();
                lblReceiveOn.Text = gvBusDetails.DataKeys[row.RowIndex]["receivedon_date"].ToString();
                lblDepot.Text = gvBusDetails.DataKeys[row.RowIndex]["officename"].ToString();
                Session["_action"] = "U";
                pnlUpdatePermit.Visible = false;
                pnlAddBus.Visible = false;
                pnlUpdateFitness.Visible = false;
                pnlUpdateInsurance.Visible = false;
                pnlUpdatePollution.Visible = false;
                pnlViewBusDetails.Visible = true;
            }

        }
        catch (Exception ex)
        {
            Errormsg(ex.Message);
            _common.ErrorLog("Bus Registration-E1", ex.Message.ToString());
        }
    }
    protected void gvBusDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvBusDetails.PageIndex = e.NewPageIndex;
        LoadBusList();
    }
    protected void gvBusDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbtnActivate = (LinkButton)e.Row.FindControl("lbtnActivate");
            LinkButton lbtnDiscontinue = (LinkButton)e.Row.FindControl("lbtnDiscontinue");
            LinkButton lbtndelete = (LinkButton)e.Row.FindControl("lbtnDeletegency");
            DataRowView rowView = (DataRowView)e.Row.DataItem;
            lbtnActivate.Visible = false;
            lbtnDiscontinue.Visible = false;
            lbtndelete.Visible = false;
            if (rowView["current_status"].ToString() == "A")
            {
                lbtnDiscontinue.Visible = true;
            }
            if (rowView["current_status"].ToString() == "D")
            {
                lbtnActivate.Visible = true;
            }
            if (rowView["is_delete"].ToString() == "Y")
            {
                lbtndelete.Visible = true;
                lbtnDiscontinue.Visible = false;
            }


        }
    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (Session["_action"].ToString() == "S" || Session["_action"].ToString() == "U")
        {
            saveBus();
        }
        else if (Session["_action"].ToString() == "activate")
        {
            saveBus();
        }
        else if (Session["_action"].ToString() == "deactivate")
        {
            saveBus();
        }
        else if (Session["_action"].ToString() == "delete")
        {
            Session["_action"] = "D";
            saveBus();
        }
        else if (Session["_action"].ToString() == "Pollution")
        {
            Session["_action"] = "PC";
            saveOtherBusDetails();
        }
        else if (Session["_action"].ToString() == "Insurance")
        {
            Session["_action"] = "I";
            saveOtherBusDetails();
        }
        else if (Session["_action"].ToString() == "Permit")
        {
            Session["_action"] = "P";
            saveOtherBusDetails();
        }
        else if (Session["_action"].ToString() == "Fitness")
        {
            Session["_action"] = "F";
            saveOtherBusDetails();
        }
    }
    protected void saveBusDetails_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (Validaion() == false)
        {
            return;
        }
        Session["_BusID"] = "0";
        Session["_action"] = "S";
        ConfirmMsg("Do you want to save Bus?");
    }
    protected void updateBusDetails_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (Validaion() == false)
        {
            return;
        }
        Session["_action"] = "U";
        ConfirmMsg("Do you want to update Bus?");
    }
    protected void resetDetails_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        clearBusData();
    }
    protected void lbtnResetBus_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        tbSearch.Text = "";
        //getAgenciesList();
    }
    protected void lbtnview_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        string msg = "";
        msg = msg + "1.  Add/Update Bus details.<br/>";
        msg = msg + "2.  Update Bus PUC/Permit/Fitness/Insurance details.<br/>";
        msg = msg + "3.  If bus is use in any trip, then it cannot be deleted.<br/>";
        msg = msg + "4.  Bus can be discontinue only if it is not in any active trip.<br />";
        InfoMsg(msg);
    }
    protected void btnUploadInvoice_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (!FileInvoice.HasFile)
        {

            Errormsg("Please select report first");
            lblwrongimage.Visible = true;

            return;
        }
        string _fileFormat = GetMimeDataOfFile(FileInvoice.PostedFile);
        if ((_fileFormat == "image/png") || (_fileFormat == "image/jpeg"))
        {
        }
        else
        {
            Errormsg("File must png, jpg, jpeg type");
            lblwrongimage.Visible = true;

            return;
        }
        if (!CheckFileExtention(FileInvoice))
        {
            Errormsg("File must be png, jpg, jpeg type");
            lblwrongimage.Visible = true;

            return;
        }

        decimal size = Math.Round((Convert.ToDecimal(FileInvoice.PostedFile.ContentLength) / Convert.ToDecimal(1024)), 2);
        if (size > 1024 || size < 10)
        {
            Errormsg("File size must be between 10 kb to 200 kb");
            lblwrongimage.Visible = true;

            return;
        }
        byte[] PhotoImage = null;
        PhotoImage = convertByteFile(FileInvoice);
        ImgInvoice.ImageUrl = GetImage(PhotoImage);
        ImgInvoice.Visible = true;
        Session["Invoice"] = FileInvoice.FileBytes;
    }
    protected void ddlGpsYN_SelectedIndexChanged(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (ddlGpsYN.SelectedValue != "0")
        {

            ddlGPSCompany.Visible = false;

            if (ddlGpsYN.SelectedValue == "Y")
            {
                lblGPSCompany.Visible = true;
                lblGPSCompany.Text = "Select GPS company";
                ddlGPSCompany.Visible = true;
                LoadGPS();
            }
            else if (ddlGpsYN.SelectedValue == "N")
            {
                lblGPSCompany.Visible = false;
                ddlGPSCompany.Visible = false;

            }
        }
    }
    protected void lbtnSavePermit_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (ValidPermit() == false)
        {
            return;
        }

        Session["_action"] = "Permit";
        ConfirmMsg("Do you want to save Bus Permit Details?");

    }
    protected void lbtnSavFitness_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (ValidFitness() == false)
        {
            return;
        }
        Session["_action"] = "Fitness";
        ConfirmMsg("Do you want to save Bus Permit Details?");
    }
    protected void lbtnSavInsurance_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (ValidInsurance() == false)
        {
            return;
        }
        Session["_action"] = "Insurance";
        ConfirmMsg("Do you want to save Bus Permit Details?");

    }
    protected void lbtnSavPollution_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (ValidPollution() == false)
        {
            return;
        }
        Session["_action"] = "Pollution";
        ConfirmMsg("Do you want to save Bus Permit Details?");

    }
    protected void lbtnCloseViewBus_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        clearPUCData();
        pnlAddBus.Visible = true;
        pnlUpdatePermit.Visible = false;
        pnlUpdateFitness.Visible = false;
        pnlUpdateInsurance.Visible = false;
        pnlUpdatePollution.Visible = false;
        pnlViewBusDetails.Visible = false;
    }
    protected void lbtnCancelPUC_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        clearPUCData();
        pnlAddBus.Visible = true;
        pnlUpdatePermit.Visible = false;
        pnlUpdateFitness.Visible = false;
        pnlUpdateInsurance.Visible = false;
        pnlUpdatePollution.Visible = false;
    }
    protected void lbtnsearch_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        LoadBusList();
        if (gvBusDetails.Rows.Count == 0)
        {
            divNoRecord.Visible = false;
            divNoSearchRecord.Visible = true;
        }

    }
    protected void gvBusPermit_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvBusPermit.PageIndex = e.NewPageIndex;
        PermitList();
    }
    protected void gvBusFitness_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvBusFitness.PageIndex = e.NewPageIndex;
        FitnessList();
    }
    protected void gvBusInsurance_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvBusInsurance.PageIndex = e.NewPageIndex;
        InsuranceList();

    }
    protected void gvBusPollution_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvBusPollution.PageIndex = e.NewPageIndex;
        PollutionList();

    }
    protected void lbtnResetList_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        ddlSServiceType.SelectedValue = "0";
        ddlSDepot.SelectedValue = "0";
        ddlSType.SelectedValue = "0";
        ddlSStatus.SelectedValue = "0";
        tbSSerialNo.Text = "";
        LoadBusList();
    }
    protected void lbtnDownloadExcel_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        ExportRefundTransToExcel();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }
    protected void lbtndwnldinst_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        string msg = "";
        msg = msg + "Coming Soon";
        InfoMsg(msg);
    }
    #endregion
}