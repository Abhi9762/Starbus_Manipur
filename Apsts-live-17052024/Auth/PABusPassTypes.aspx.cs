using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_PABusPassTypes : System.Web.UI.Page
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
            GSTTypeList();
            ServiceTypeList();
            CategoriesTypeList();
            IDProofTypeList();
            AddProofTypeList();
            ChargesTypeList();
            BusPassTypeList();
            BussPassTypesCount();
            BusPassTypeListDD();
            LoadDuplicateReason();
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
    private void GSTTypeList()
    {
        try
        {

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "buspass.f_get_gst_type");

            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {

                    ddlgsttype.DataSource = dt;
                    ddlgsttype.DataTextField = "val_tax_name";
                    ddlgsttype.DataValueField = "taxid";
                    ddlgsttype.DataBind();
                }
            }
            else
            {
                ddlgsttype.Items.Insert(0, "SELECT");
                ddlgsttype.Items[0].Value = "0";
                ddlgsttype.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ddlgsttype.Items.Insert(0, "SELECT");
            ddlgsttype.Items[0].Value = "0";
            ddlgsttype.SelectedIndex = 0;
        }
    }
    private void ServiceTypeList()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "buspass.f_get_servicetypelist");

            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlServiceType.DataSource = dt;
                    ddlServiceType.DataTextField = "servicetypename_en";
                    ddlServiceType.DataValueField = "val_srtpid";
                    ddlServiceType.DataBind();
                }
            }
            else
            {
                ddlServiceType.Items.Insert(0, "SELECT");
                ddlServiceType.Items[0].Value = "0";
                ddlServiceType.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ddlServiceType.Items.Insert(0, "SELECT");
            ddlServiceType.Items[0].Value = "0";
            ddlServiceType.SelectedIndex = 0;
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
                    ddlPassCategory.DataSource = dt;
                    ddlPassCategory.DataTextField = "buspass_categoryname";
                    ddlPassCategory.DataValueField = "buspass_categoryid";
                    ddlPassCategory.DataBind();
                }
            }

            ddlPassCategory.Items.Insert(0, "Select");
            ddlPassCategory.Items[0].Value = "0";
            ddlPassCategory.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlPassCategory.Items.Insert(0, "Select");
            ddlPassCategory.Items[0].Value = "0";
            ddlPassCategory.SelectedIndex = 0;
        }
    }
    private void BussPassTypesCount()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.buspasstype_count");

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
    private void BusPassTypeListDD()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.bus_pass_type_list_bustypeid");

            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlBusPassType.DataSource = dt;
                    ddlBusPassType.DataTextField = "buspasstypename";
                    ddlBusPassType.DataValueField = "buspasstypeid";
                    ddlBusPassType.DataBind();
                }
            }

            ddlBusPassType.Items.Insert(0, "Select");
            ddlBusPassType.Items[0].Value = "0";
            ddlBusPassType.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlBusPassType.Items.Insert(0, "Select");
            ddlBusPassType.Items[0].Value = "0";
            ddlBusPassType.SelectedIndex = 0;


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
                    ddlApplicableCharges.DataSource = dt;
                    ddlApplicableCharges.DataTextField = "chargetype_name";
                    ddlApplicableCharges.DataValueField = "charge_typeid";
                    ddlApplicableCharges.DataBind();
                }
            }

        }
        catch (Exception ex)
        {
            ddlApplicableCharges.Items.Insert(0, "Select");
            ddlApplicableCharges.Items[0].Value = "0";
            ddlApplicableCharges.SelectedIndex = 0;
        }
    }
    private void BusPassTypeList()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_bus_pass_type");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    gvBusPassTypes.DataSource = dt;
                    gvBusPassTypes.DataBind();
                    gvBusPassTypes.Visible = true;
                    pnlnoRecordfound.Visible = false;
                }
                else
                {
                    gvBusPassTypes.Visible = false;
                    pnlnoRecordfound.Visible = true;
                }
            }
        }

        catch (Exception ex)
        {
            Errormsg(ex.Message.ToString());
        }

    }
    private void IDProofTypeList()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_bus_pass_document_id_proof");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlDocumentIdProof.DataSource = dt;
                    ddlDocumentIdProof.DataTextField = "documenttype_name";
                    ddlDocumentIdProof.DataValueField = "document_typeid";
                    ddlDocumentIdProof.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message);
        }
    }
    private void AddProofTypeList()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", " auth.f_bus_pass_document_add_proof");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlDocumentAddProof.DataSource = dt;
                    ddlDocumentAddProof.DataTextField = "documenttype_name";
                    ddlDocumentAddProof.DataValueField = "document_typeid";
                    ddlDocumentAddProof.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message);
        }
    }
    private void LoadDuplicateReason()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", " auth.f_get_duplicate_reason");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    // Assuming ddlDuplicateReason is the ID of your DropDownList control.
                    // Uncomment the following lines if you're using ASP.NET WebForms.
                    // ddlDuplicateReason.DataSource = dt;
                    // ddlDuplicateReason.DataTextField = "REASON_NAME";
                    // ddlDuplicateReason.DataValueField = "REASON_CODE";
                    // ddlDuplicateReason.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            // Handle the exception, e.g., logging or displaying an error message.
        }
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
                msg += count + ". Enter Valid Bus Pass Type Name In English.<br/>";
            }

            if (!_validation.IsValidString(tbabbr.Text, 2, tbabbr.MaxLength))
            {
                count++;
                msg += count + ". Enter Valid Bus Pass Type Abbreviation.<br/>";
            }

            if (!_validation.IsValidString(tbDescriptionEn.Text, 3, tbDescriptionEn.MaxLength))
            {
                count++;
                msg += count + ".  Enter Valid Description .<br/>";
            }

            if (!_validation.IsValidString(tbValidity.Text, 1, tbValidity.MaxLength))
            {
                count++;
                msg += count + ".  Enter Validity in Days .<br/>";
            }

            if (ddlPassCategory.SelectedValue == "0")
            {
                count++;
                msg += count + ".Select Bus Pass Category<br/>";
            }

            if (chkAgeGroup.Checked)
            {
                if (string.IsNullOrEmpty(tbMinAge.Text) && string.IsNullOrEmpty(tbMaxAge.Text))
                {
                    count++;
                    msg += count + ".Enter Minimum or Maximum Age.<br/>";
                }
            }

            if (chkgst.Checked)
            {
                if (string.IsNullOrEmpty(ddlgsttype.SelectedValue))
                {
                    count++;
                    msg += count + ". Select GST Type.<br/>";
                }
            }

            if (chkGender.Checked)
            {
                if (string.IsNullOrEmpty(ddlGender.SelectedValue))
                {
                    count++;
                    msg += count + ".Select at least One gender. <br/>";
                }
            }

            if (chkKms.Checked)
            {
                if (string.IsNullOrEmpty(tbKms.Text))
                {
                    count++;
                    msg += count + ".Enter Number of KMs. <br/>";
                }
            }

            if (chkServiceType.Checked)
            {
                if (string.IsNullOrEmpty(ddlServiceType.SelectedValue))
                {
                    count++;
                    msg += count + ".Select Service Type<br/>";
                }
            }

            if (chkState.Checked)
            {
                if (!chkWithinState.Checked && !chkOutsideState.Checked)
                {
                    count++;
                    msg += count + ".Select State  type<br/>";
                }
            }

            if (chkRenewYN.Checked)
            {
                if (string.IsNullOrEmpty(tbRenewNoofdays.Text))
                {
                    count++;
                    msg += count + ". Enter No. of Days before Pass Expiry<br/>";
                }
                else
                {
                    int noofrenewaldays = Convert.ToInt32(tbRenewNoofdays.Text);
                    int Validity = Convert.ToInt32(tbValidity.Text);

                    if (noofrenewaldays > Validity)
                    {
                        count++;
                        msg += count + ". No of pass Renewal Days cannot be greater than Validity of pass. <br/>";
                    }
                }
            }

            if (!_validation.IsValidInteger(tbconcessionfare.Text, 1, 3))
            {
                count++;
                msg += count + ". Enter Valid Concession On Fare<br/>";
            }

            if (!_validation.IsValidInteger(tbconcessiontax.Text, 1, 3))
            {
                count++;
                msg += count + ". Enter Valid Concession On Tax<br/>";
            }

            if (ddlApplicableCharges.SelectedValue == null)
            {
                count++;
                msg += count + ". Select Applicable charges type.<br/>";
            }

            if (!string.IsNullOrEmpty(ddlDocumentIdProof.SelectedValue))
            {
                if (!chkNewPass.Checked && !chkRenew.Checked)
                {
                    count++;
                    msg += count + ". Select Document ID Proof required for Pass Type.<br/>";
                }
            }

            if (!string.IsNullOrEmpty(ddlDocumentAddProof.SelectedValue))
            {
                if (!chkAddNew.Checked && !chkAddRenew.Checked)
                {
                    count++;
                    msg += count + ".Select Document Address required for Pass Type.<br/>";
                }
            }

            if (rbtadvancefare.SelectedValue == "Y")
            {
                string ServiceType = "";
                if (ddlServiceType.SelectedValue != "0")
                {
                    foreach (ListItem item in ddlServiceType.Items)
                    {
                        if (item.Selected) ServiceType += item.Value + ",";
                    }
                    if (ServiceType.Length - 1 < 1)
                    {
                        count++;
                        msg += count + ".Select one service type only if concession and advance fare is applied .<br/>";
                    }
                }
            }
            if (chkphotoNew.Checked == false)
            {
                count++;
                msg += count + ".Photo is required. .<br/>";
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
    private void ResetControl()
    {
        lbtnUpdate.Visible = false;
        lbtnSave.Visible = true;
        tbName.Enabled = true;
        tbabbr.Enabled = true;
        tbNameLocal.Enabled = true;
        ddlPassCategory.SelectedValue = "0";
        tbName.Text = "";
        tbabbr.Text = "";
        tbabbrold.Text = "";
        tbNameLocal.Text = "";
        tbValidity.Text = "";
        ddlmonths.SelectedIndex = 0;
        tbDescriptionEn.Text = "";
        tbDescriptionLocal.Text = "";
        tbKms.Text = "";
        tbMaxAge.Text = "";
        tbMinAge.Text = "";
        ddlServiceType.SelectedValue = null;
        ddlGender.SelectedValue = null;
        ddlApplicableCharges.SelectedValue = null;
        tbconcessionfare.Text = "0";
        tbconcessiontax.Text = "0";
        ddlDocumentAddProof.SelectedValue = null;
        ddlDocumentIdProof.SelectedValue = null;
        chkAddNew.Checked = false;
        chkAgeGroup.Checked = false;
        chkGender.Checked = false;
        chkKms.Checked = false;
        chkNewPass.Checked = false;
        chkOutsideState.Checked = false;
        chkphotoNew.Checked = false;
        chkRenew.Checked = false;
        chkServiceType.Checked = false;
        chkState.Checked = false;
        chkWithinState.Checked = false;
        pnlAddress.Visible = false;
        pnlAge.Visible = false;
        pnlState.Visible = false;
        pnlDocumentAsIDProof.Visible = false;
        pnlKm.Visible = false;
        ddlGender.Visible = false;
        ddlServiceType.Visible = false;
        rbtadvancefare.SelectedValue = "N";
        tbconcessionfare.Text = "100";
        tbconcessiontax.Text = "100";
        dvadvancefare.Visible = false;
        chkRenewYN.Checked = false;
        chkDBTid.Checked = false;
        rbtIssuance.SelectedValue = null;
        pnlRenewDays.Visible = false;
        tbRenewNoofdays.Text = "";
        chkgst.Checked = false;
        ddlgsttype.Visible = false;
        ddlgsttype.SelectedValue = null;
    }
    private void SaveBusPassType()
    {
        try
        {
            string Status = "A", BusPassTypeName = "", BusPassAbbr = "", BusPassTypeNameL = "", BusPassTypeDesEn = "", BusPassTypeDesL = "",
                BusPassConcessionType = "", specificGenderYN = "", GenderTypes = "", SpecificAgeGroupYN = "N", ServiceTypeYN = "N", Servicetypes = "",
                WithinState = "N", OutsideState = "N", TravelNoOfKmsYN = "N", DocumentIdProofForNewYN = "N", DocumentIdProofForNewTypes = "",
                DocumentIdProofForRenewYN = "N", DocumentIdProofForRenewTypes = "", DocumentAddressProofForNewYN = "N", DocumentAddressProofForNewTypes = "",
                DocumentAddressProofForRenewYN = "N", DocumentAddressProofForRenewTypes = "", PhotorequireNewYN = "N", PhotorequireRenewYN = "N",
                PhotorequireDuplicateYN = "N", ChargeID = "", AddressProof = "", IdProof = "", AdvancefareYN = "N", RenewYN = "N",
                Issuance = "N", IssuenceType = "", DBT_YN = "N", DuplicateYN = "N", Duplicatetypes = "", restrcedmonth = "0", ApplicableOnlineYN = "N";

            decimal PassCategory = 0, BusPassId = 0, concessionperfare = 0, concessionpertax = 0, MinAge = 0, MaxAge = 0, NoofKms = 0;
            int validity = 0, Noofdays = 0;
            string enroutechkdoc = "";
            string GSTTypes = "";
            string GSTYN = "N";

            PassCategory = Convert.ToDecimal(ddlPassCategory.SelectedValue);
            BusPassTypeName = tbName.Text;
            BusPassAbbr = tbabbr.Text;
            BusPassTypeNameL = tbNameLocal.Text;
            BusPassTypeDesEn = tbDescriptionEn.Text;
            BusPassTypeDesL = tbDescriptionLocal.Text;

            if (tbconcessionfare.Text.ToString() == "0" && tbconcessiontax.Text.ToString() == "0")
            {
                BusPassConcessionType = "F";
            }
            else
            {
                BusPassConcessionType = "C";
            }

            if (!string.IsNullOrEmpty(tbValidity.Text))
            {
                validity = Convert.ToInt32(tbValidity.Text);
            }

            restrcedmonth = ddlmonths.SelectedValue.ToString();

            IssuenceType = rbtIssuance.SelectedValue;

            if (chkphotoNew.Checked)
            {
                PhotorequireNewYN = "Y";
            }

            if (chkphotoRenew.Checked)
            {
                PhotorequireRenewYN = "Y";
            }

            if (chkgst.Checked)
            {
                GSTYN = "Y";
                if (ddlgsttype.SelectedValue != "0")
                {
                    foreach (ListItem item in ddlgsttype.Items)
                    {
                        if (item.Selected)
                            GSTTypes += item.Value + ",";
                    }
                    if (GSTTypes.Length > 1)
                        GSTTypes = GSTTypes.Substring(0, GSTTypes.Length - 1);
                }
            }

            if (chkGender.Checked)
            {
                specificGenderYN = "Y";
                if (ddlGender.SelectedValue != "0")
                {
                    foreach (ListItem item in ddlGender.Items)
                    {
                        if (item.Selected)
                            GenderTypes += item.Value + ",";
                    }
                    if (GenderTypes.Length > 1)
                        GenderTypes = GenderTypes.Substring(0, GenderTypes.Length - 1);
                }
            }

            if (chkAgeGroup.Checked)
            {
                SpecificAgeGroupYN = "Y";
                if (!string.IsNullOrEmpty(tbMinAge.Text))
                {
                    MinAge = Convert.ToDecimal(tbMinAge.Text);
                }

                if (!string.IsNullOrEmpty(tbMaxAge.Text))
                {
                    MaxAge = Convert.ToDecimal(tbMaxAge.Text);
                }
            }

            if (chkState.Checked)
            {
                if (chkWithinState.Checked)
                {
                    WithinState = "Y";
                }

                if (chkOutsideState.Checked)
                {
                    OutsideState = "Y";
                }
            }

            if (chkKms.Checked)
            {
                TravelNoOfKmsYN = "Y";

                if (!string.IsNullOrEmpty(tbKms.Text))
                {
                    NoofKms = Convert.ToDecimal(tbKms.Text);
                }
            }

            if (rbtadvancefare.SelectedValue == "Y")
            {
                AdvancefareYN = "Y";
            }

            if (rbtapplicableonline.SelectedValue == "Y")
            {
                ApplicableOnlineYN = "Y";
            }

            if (chkRenewYN.Checked)
            {
                RenewYN = "Y";
                Noofdays = Convert.ToInt32(tbRenewNoofdays.Text);
            }

            if (chkDBTid.Checked)
            {
                DBT_YN = "Y";
            }

            if (chkServiceType.Checked)
            {
                ServiceTypeYN = "Y";

                if (ddlServiceType.SelectedValue != "0")
                {
                    foreach (ListItem item in ddlServiceType.Items)
                    {
                        if (item.Selected)
                        {
                            Servicetypes += item.Value + ",";
                        }
                    }

                    if (Servicetypes.Length > 1)
                    {
                        Servicetypes = Servicetypes.Substring(0, Servicetypes.Length - 1);
                    }
                }
            }

            if (ddlApplicableCharges.SelectedValue != "0")
            {
                foreach (ListItem item in ddlApplicableCharges.Items)
                {
                    if (item.Selected)
                        ChargeID += item.Value + ",";
                }

                if (ChargeID.Length > 1)
                    ChargeID = ChargeID.Substring(0, ChargeID.Length - 1);
            }

            concessionperfare = Convert.ToDecimal(tbconcessionfare.Text.ToString());
            concessionpertax = Convert.ToDecimal(tbconcessiontax.Text.ToString());

            if (ddlDocumentAddProof.SelectedValue != "0")
            {
                foreach (ListItem item in ddlDocumentAddProof.Items)
                {
                    if (item.Selected)
                        AddressProof += item.Value + ",";
                }

                if (AddressProof.Length > 1)
                    AddressProof = AddressProof.Substring(0, AddressProof.Length - 1);

                if (chkAddNew.Checked)
                {
                    DocumentAddressProofForNewYN = "Y";
                    DocumentAddressProofForNewTypes = AddressProof;
                }

                if (chkAddRenew.Checked)
                {
                    DocumentAddressProofForRenewYN = "Y";
                    DocumentAddressProofForRenewTypes = AddressProof;
                }
            }

            if (ddlDocumentIdProof.SelectedValue != "0")
            {
                foreach (ListItem item in ddlDocumentIdProof.Items)
                {
                    if (item.Selected)
                        IdProof += item.Value + ",";
                }

                if (IdProof.Length > 1)
                    IdProof = IdProof.Substring(0, IdProof.Length - 1);

                if (chkNewPass.Checked)
                {
                    DocumentIdProofForNewYN = "Y";
                    DocumentIdProofForNewTypes = IdProof;
                }

                if (chkRenew.Checked)
                {
                    DocumentIdProofForRenewYN = "Y";
                    DocumentIdProofForRenewTypes = IdProof;
                }
            }

            enroutechkdoc = ddlenroutechkdoc.SelectedValue.ToString();

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
                BusPassId = Convert.ToDecimal(Session["buspasstypeid"].ToString());
            }

            string IPAddress = HttpContext.Current.Request.UserHostAddress;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", " auth.f_buspasstype_insertupdate");
            MyCommand.Parameters.AddWithValue("@p_action", Session["_action"].ToString());
            MyCommand.Parameters.AddWithValue("@p_updatedby", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("@p_ipaddress", IPAddress);
            MyCommand.Parameters.AddWithValue("@p_id", BusPassId);
            MyCommand.Parameters.AddWithValue("@p_bus_pass_name", BusPassTypeName);
            MyCommand.Parameters.AddWithValue("@p_bus_pass_abbr", BusPassAbbr);
            MyCommand.Parameters.AddWithValue("@p_bus_pass_name_local", BusPassTypeNameL);
            MyCommand.Parameters.AddWithValue("@p_bus_category_id", PassCategory);
            MyCommand.Parameters.AddWithValue("@p_description", BusPassTypeDesEn);
            MyCommand.Parameters.AddWithValue("@p_description_local", BusPassTypeDesL);
            MyCommand.Parameters.AddWithValue("@p_concessiontype", BusPassConcessionType);
            MyCommand.Parameters.AddWithValue("@p_concession_per_fare", concessionperfare);
            MyCommand.Parameters.AddWithValue("@p_concession_per_tax", concessionpertax);
            MyCommand.Parameters.AddWithValue("@p_gender_yn", specificGenderYN);
            MyCommand.Parameters.AddWithValue("@p_gender_type", GenderTypes);
            MyCommand.Parameters.AddWithValue("@p_age_yn", SpecificAgeGroupYN);
            MyCommand.Parameters.AddWithValue("@p_age_max", MaxAge);
            MyCommand.Parameters.AddWithValue("@p_age_min", MinAge);
            MyCommand.Parameters.AddWithValue("@p_kms", TravelNoOfKmsYN);
            MyCommand.Parameters.AddWithValue("@p_no_of_kms", NoofKms);
            MyCommand.Parameters.AddWithValue("@p_servicetype_yn", ServiceTypeYN);
            MyCommand.Parameters.AddWithValue("@p_servicetype_types", Servicetypes);
            MyCommand.Parameters.AddWithValue("@p_in_state_yn", WithinState);
            MyCommand.Parameters.AddWithValue("@p_outside_state_yn", OutsideState);
            MyCommand.Parameters.AddWithValue("@p_renewyn", RenewYN);
            MyCommand.Parameters.AddWithValue("@p_noofdays", Noofdays);
            MyCommand.Parameters.AddWithValue("@p_restrictedmonth", restrcedmonth);
            MyCommand.Parameters.AddWithValue("@p_issuenceyn", Issuance);
            MyCommand.Parameters.AddWithValue("@p_issuencetype", IssuenceType);
            MyCommand.Parameters.AddWithValue("@p_dbtyn", DBT_YN);
            MyCommand.Parameters.AddWithValue("@p_idproof_new_yn", DocumentIdProofForNewYN);
            MyCommand.Parameters.AddWithValue("@p_idproof_new_types", DocumentIdProofForNewTypes);
            MyCommand.Parameters.AddWithValue("@p_idproof_renew_yn", DocumentIdProofForRenewYN);
            MyCommand.Parameters.AddWithValue("@p_idproof_renew_types", DocumentIdProofForRenewTypes);
            MyCommand.Parameters.AddWithValue("@p_addproof_new_yn", DocumentAddressProofForNewYN);
            MyCommand.Parameters.AddWithValue("@p_addproof_new_types", DocumentAddressProofForNewTypes);
            MyCommand.Parameters.AddWithValue("@p_addproof_renew_yn", DocumentAddressProofForRenewYN);
            MyCommand.Parameters.AddWithValue("@p_addproof_renew_types", DocumentAddressProofForRenewTypes);
            MyCommand.Parameters.AddWithValue("@p_validity_in_days", validity);
            MyCommand.Parameters.AddWithValue("@p_photorequire_new_yn", PhotorequireNewYN);
            MyCommand.Parameters.AddWithValue("@p_photorequire_renew_yn", PhotorequireRenewYN);
            MyCommand.Parameters.AddWithValue("@p_photorequire_duplicate_yn", PhotorequireDuplicateYN);
            MyCommand.Parameters.AddWithValue("@p_charge_id", ChargeID);
            MyCommand.Parameters.AddWithValue("@p_advancefareyn", AdvancefareYN);
            MyCommand.Parameters.AddWithValue("@p_applicableonlineyn", ApplicableOnlineYN);
            MyCommand.Parameters.AddWithValue("@p_duplicateyn", DuplicateYN);
            MyCommand.Parameters.AddWithValue("@p_duplicatetypes", Duplicatetypes);
            MyCommand.Parameters.AddWithValue("@p_status", Status);
            MyCommand.Parameters.AddWithValue("@p_enroutechkdoc", enroutechkdoc);
            MyCommand.Parameters.AddWithValue("@p_gstyn", GSTYN);
            MyCommand.Parameters.AddWithValue("@p_gsttype", GSTTypes);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {

                    if (Session["_action"].ToString() == "S")
                    {
                        Successmsg("Bus Pass Type Added Successfully.");
                    }

                    if (Session["_action"].ToString() == "U")
                    {
                        Successmsg("Bus Pass Type Updated Successfully.");
                    }

                    if (Session["_action"].ToString() == "activate")
                    {
                        Successmsg("Bus Pass Type Activated.");
                    }

                    if (Session["_action"].ToString() == "deactivate")
                    {
                        Successmsg("Bus Pass Type Discontinued.");
                    }

                    lbtnSave.Visible = true;
                    lbtnUpdate.Visible = false;
                    lbtnReset.Visible = true;
                    lbtnCancel.Visible = false;
                    ResetControl();
                    BusPassTypeList();
                    ChargesTypeList();
                    BussPassTypesCount();
                    BusPassTypeListDD();
                }
            }
        }

        catch (Exception ex)
        {
            
        }
    }

    #endregion

    #region "Event"
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        SaveBusPassType();
    }
    protected void lbtnview_Click(object sender, EventArgs e)
    {
        InfoMsg("Bus Pass Type Name cannot be edited after creation");
    }
    protected void lbtnUpdate_Click(object sender, EventArgs e)
    {
        if (!Validation())
        {
            return;
        }

        ConfirmMsg("Do you want to Update Bus Pass Type?");
        Session["_action"] = "U";

    }
    protected void lbtnSave_Click(object sender, EventArgs e)
    {
        if (!Validation())
        {
            return;
        }

        ConfirmMsg("Do you want to save Bus Pass Type?");
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
    protected void gvBusPassTypes_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Deactivate")
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            string chargeid = gvBusPassTypes.DataKeys[row.RowIndex]["buspass_type_id"].ToString();
            ConfirmMsg("Do you want to Discontinue Bus Pass Type?");
            Session["_action"] = "deactivate";
            Session["buspasstypeid"] = chargeid;
        }

        if (e.CommandName == "Activate")
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            string chargeid = gvBusPassTypes.DataKeys[row.RowIndex]["buspass_type_id"].ToString();
            ConfirmMsg("Do you want to Activate Bus Pass Type?");
            Session["_action"] = "activate";
            Session["buspasstypeid"] = chargeid;

            if (Convert.IsDBNull(gvBusPassTypes.DataKeys[row.RowIndex]["bus_pass_abbr"]))
            {
                tbabbr.Text = "";
            }
            else
            {
                tbabbr.Text = gvBusPassTypes.DataKeys[row.RowIndex]["bus_pass_abbr"].ToString();
                tbabbr.Enabled = false;
            }
        }


        if (e.CommandName == "updateBusPassTypes")
        {
            lbtnCancel.Visible = true;
            lbtnUpdate.Visible = true;
            lbtnSave.Visible = false;
            lbtnReset.Visible = false;
            tbName.Enabled = false;
            tbNameLocal.Enabled = false;
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            string chargeid = gvBusPassTypes.DataKeys[row.RowIndex]["buspass_type_id"].ToString();
            string buspasstypename = gvBusPassTypes.DataKeys[row.RowIndex]["buspass_type_name"].ToString();
            string buspasstypenamelocal = gvBusPassTypes.DataKeys[row.RowIndex]["buspass_type_name_local"].ToString();
            string buspass_categoryid = gvBusPassTypes.DataKeys[row.RowIndex]["bus_pass_category_id"].ToString();
            string buspasstypedescription = gvBusPassTypes.DataKeys[row.RowIndex]["val_description"].ToString();
            string buspasstype_descriptionlocal = gvBusPassTypes.DataKeys[row.RowIndex]["val_description_local"].ToString();
            string buspasstypeconcessiontype = gvBusPassTypes.DataKeys[row.RowIndex]["CONCESSION_TYPE"].ToString();
            string restrictedtogenderyn = gvBusPassTypes.DataKeys[row.RowIndex]["RESTRICTED_TOGENDER_YN"].ToString();
            string restrictedtogendertype = gvBusPassTypes.DataKeys[row.RowIndex]["RESTRICTED_TOGENDER_TYPE"].ToString();
            string restrictedtoageyn = gvBusPassTypes.DataKeys[row.RowIndex]["RESTRICTED_TOAGE_YN"].ToString();
            string restrictedagemax = gvBusPassTypes.DataKeys[row.RowIndex]["RESTRICTED_AGE_MAX"].ToString();
            string restrictedagemin = gvBusPassTypes.DataKeys[row.RowIndex]["RESTRICTED_AGE_MIN"].ToString();
            string restrictedkmsyn = gvBusPassTypes.DataKeys[row.RowIndex]["RESTRICTED_KMS_YN"].ToString();
            string restrictednoofkm = gvBusPassTypes.DataKeys[row.RowIndex]["RESTRICTED_NO_OF_KMS"].ToString();
            string applicabletobusservicetypesyn = gvBusPassTypes.DataKeys[row.RowIndex]["BUS_SERVICE_TYPES_YN"].ToString();
            string applicabletobusservicetype = gvBusPassTypes.DataKeys[row.RowIndex]["TO_BUS_SERVICE_TYPES"].ToString();
            string journeyallowedinstateyn = gvBusPassTypes.DataKeys[row.RowIndex]["ALLOWED_INSTATE_YN"].ToString();
            string journeyallowedoutsidestateyn = gvBusPassTypes.DataKeys[row.RowIndex]["ALLOWED_OUTSIDE_STATE_YN"].ToString();
            string documentidprooffornewyn = gvBusPassTypes.DataKeys[row.RowIndex]["DOCID_NEW_YN"].ToString();
            string documentidprooffornewtypes = gvBusPassTypes.DataKeys[row.RowIndex]["DOCID_NEW_TYPES"].ToString();
            string documentidproofforrenewyn = gvBusPassTypes.DataKeys[row.RowIndex]["DOCID_RENEW_YN"].ToString();
            string documentidproofforrenewtypes = gvBusPassTypes.DataKeys[row.RowIndex]["DOCID_RENEW_TYPES"].ToString();
            string documentidproofforduplicateyn = gvBusPassTypes.DataKeys[row.RowIndex]["DOCID_DUPLICATE_YN"].ToString();
            string documentidproofforduplicatetypes = gvBusPassTypes.DataKeys[row.RowIndex]["DOCID_DUPLICATE_TYPES"].ToString();
            string documentaddressfornewyn = gvBusPassTypes.DataKeys[row.RowIndex]["DOCADD_NEW_YN"].ToString();
            string documentaddressfornewtypes = gvBusPassTypes.DataKeys[row.RowIndex]["DOCADD_NEW_TYPES"].ToString();
            string documentaddressforrenewyn = gvBusPassTypes.DataKeys[row.RowIndex]["DOCADD_RENEW_YN"].ToString();
            string documentaddressforrenewtypes = gvBusPassTypes.DataKeys[row.RowIndex]["doc_add_renew_types"].ToString();
            string documentaddressforduplicateyn = gvBusPassTypes.DataKeys[row.RowIndex]["DOCADD_DUPLICATE_YN"].ToString();
            string documentaddressforduplicatetypes = gvBusPassTypes.DataKeys[row.RowIndex]["DOCADD_DUPLICATE_TYPES"].ToString();
            string validityinday = gvBusPassTypes.DataKeys[row.RowIndex]["VALIDITY_INDAYS"].ToString();
            string photorequire_newyn = gvBusPassTypes.DataKeys[row.RowIndex]["PHOTO_NEWYN"].ToString();
            string photorequire_renewyn = gvBusPassTypes.DataKeys[row.RowIndex]["PHOTO_RENEWYN"].ToString();
            string photorequire_duplicateyn = gvBusPassTypes.DataKeys[row.RowIndex]["PHOTO_DUPLICATEYN"].ToString();
            string buspasscharges_id = gvBusPassTypes.DataKeys[row.RowIndex]["BUSPASS_CHARGESID"].ToString();
            string advancefyn = gvBusPassTypes.DataKeys[row.RowIndex]["ADVANCE_FAREYN"].ToString();
            string renew_yn = gvBusPassTypes.DataKeys[row.RowIndex]["RENEW_YN"].ToString();
            string noof_days = gvBusPassTypes.DataKeys[row.RowIndex]["NO_OF_DAYS"].ToString();
            string issuenceyn = gvBusPassTypes.DataKeys[row.RowIndex]["ISSUENCE_YN"].ToString();
            string issuencetype = gvBusPassTypes.DataKeys[row.RowIndex]["ISSUENCE_TYPE"].ToString();
            string dbt_yn = gvBusPassTypes.DataKeys[row.RowIndex]["dbt_yn"].ToString();
            string duplicate_yn = gvBusPassTypes.DataKeys[row.RowIndex]["DUPLICATE_YN"].ToString();
            string duplicate_types = gvBusPassTypes.DataKeys[row.RowIndex]["DUPLICATE_TYPES"].ToString();

            string concessionper_Fare = gvBusPassTypes.DataKeys[row.RowIndex]["concession_per_fare"].ToString();
            string concessionper_Tax = gvBusPassTypes.DataKeys[row.RowIndex]["concession_per_tax"].ToString();
            string Validitymonth = gvBusPassTypes.DataKeys[row.RowIndex]["validity_month"].ToString();

            string applicableConcessionyn = gvBusPassTypes.DataKeys[row.RowIndex]["applicable_online_concession_yn"].ToString();
            string enroutechkdocument = gvBusPassTypes.DataKeys[row.RowIndex]["enroutecheck_document"].ToString();
            Session["buspasstypeid"] = chargeid;
            tbName.Text = buspasstypename;
            if (Convert.IsDBNull(gvBusPassTypes.DataKeys[row.RowIndex]["bus_pass_abbr"]))
            {
                tbabbr.Text = "";
            }
            else
            {
                tbabbr.Text = gvBusPassTypes.DataKeys[row.RowIndex]["bus_pass_abbr"].ToString();
            }

            if (Convert.IsDBNull(gvBusPassTypes.DataKeys[row.RowIndex]["buspass_abbrold"]))
            {
                tbabbrold.Text = "";
            }
            else
            {
                tbabbrold.Text = gvBusPassTypes.DataKeys[row.RowIndex]["buspass_abbrold"].ToString();
                tbabbrold.Enabled = false;
            }

            tbNameLocal.Text = buspasstypenamelocal;
            ddlPassCategory.SelectedValue = buspass_categoryid;
            tbDescriptionEn.Text = buspasstypedescription;
            tbDescriptionLocal.Text = buspasstypedescription;
            rbtIssuance.ClearSelection();
            rbtIssuance.SelectedValue = issuencetype;
            tbconcessionfare.Text = concessionper_Fare;
            tbconcessiontax.Text = concessionper_Tax;

            string gstyn = gvBusPassTypes.DataKeys[row.RowIndex]["gst_yn"].ToString();
            string gsttype = gvBusPassTypes.DataKeys[row.RowIndex]["gst_type"].ToString();

            if (gstyn == "Y")
            {
                chkgst.Checked = true;
                ddlgsttype.Visible = true;

                string[] gstTypeCode;
                if (!string.IsNullOrEmpty(gsttype))
                {
                    gstTypeCode = gsttype.Split(',');
                    foreach (ListItem item in ddlgsttype.Items)
                    {
                        foreach (string str in gstTypeCode)
                        {
                            if (item.Value == str)
                            {
                                item.Selected = true;
                            }
                        }
                    }
                }
            }

            if (restrictedtogenderyn == "Y")
            {
                chkGender.Checked = true;
                ddlGender.Visible = true;

                string[] TypeCode;
                if (!string.IsNullOrEmpty(restrictedtogendertype))
                {
                    TypeCode = restrictedtogendertype.Split(',');

                    foreach (ListItem item in ddlGender.Items)
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

            if (restrictedtoageyn == "Y")
            {
                chkAgeGroup.Checked = true;
                pnlAge.Visible = true;
                tbMaxAge.Text = restrictedagemax;
                tbMinAge.Text = restrictedagemin;
            }

            if (restrictedkmsyn == "Y")
            {
                chkKms.Checked = true;
                pnlKm.Visible = true;
                tbKms.Text = restrictednoofkm;
            }

            if (applicabletobusservicetypesyn == "Y")
            {
                chkServiceType.Checked = true;
                ddlServiceType.Visible = true;

                string[] TypeCode;
                if (!string.IsNullOrEmpty(applicabletobusservicetype))
                {
                    TypeCode = applicabletobusservicetype.Split(',');

                    foreach (ListItem item in ddlServiceType.Items)
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

            if (journeyallowedinstateyn == "Y" || journeyallowedoutsidestateyn == "Y")
            {
                pnlState.Visible = true;
                chkState.Checked = true;

                if (journeyallowedinstateyn == "Y")
                {
                    chkWithinState.Checked = true;
                }

                if (journeyallowedoutsidestateyn == "Y")
                {
                    chkOutsideState.Checked = true;
                }
            }

            if (advancefyn == "Y")
            {
                rbtadvancefare.SelectedValue = "Y";
                dvadvancefare.Visible = true;
            }
            else
            {
                rbtadvancefare.SelectedValue = "N";
                dvadvancefare.Visible = false;
            }

            rbtapplicableonline.SelectedValue = applicableConcessionyn;

            if (renew_yn == "Y")
            {
                pnlRenewDays.Visible = true;
                chkRenewYN.Checked = true;
                tbRenewNoofdays.Text = noof_days;
            }

            if (dbt_yn == "Y")
            {
                chkDBTid.Checked = true;
            }

            string[] chargeCode;

            if (!string.IsNullOrEmpty(buspasscharges_id))
            {
                chargeCode = buspasscharges_id.Split(',');

                foreach (ListItem item in ddlApplicableCharges.Items)
                {
                    foreach (string str in chargeCode)
                    {
                        if (item.Value == str)
                        {
                            item.Selected = true;
                        }
                    }
                }
            }

            if (photorequire_newyn == "Y")
            {
                chkphotoNew.Checked = true;
            }

            if (photorequire_renewyn == "Y")
            {
                chkphotoRenew.Checked = true;
            }

            tbValidity.Text = validityinday;
            ddlmonths.SelectedValue = Validitymonth;
            string[] IdnewProofCode;

            if (!string.IsNullOrEmpty(documentidprooffornewtypes))
            {
                pnlDocumentAsIDProof.Visible = true;
                chkNewPass.Checked = true;
                IdnewProofCode = documentidprooffornewtypes.Split(',');

                foreach (ListItem item in ddlDocumentIdProof.Items)
                {
                    foreach (string str in IdnewProofCode)
                    {
                        if (item.Value == str)
                        {
                            item.Selected = true;
                        }
                    }
                }
            }

            string[] IdRenewProofCode;

            if (!string.IsNullOrEmpty(documentidproofforrenewtypes))
            {
                pnlDocumentAsIDProof.Visible = true;
                chkRenew.Checked = true;
                IdRenewProofCode = documentidproofforrenewtypes.Split(',');

                foreach (ListItem item in ddlDocumentIdProof.Items)
                {
                    foreach (string str in IdRenewProofCode)
                    {
                        if (item.Value == str)
                        {
                            item.Selected = true;
                        }
                    }
                }
            }

            string[] IdDuplicateProofCode;

            if (!string.IsNullOrEmpty(documentidproofforduplicatetypes))
            {
                pnlDocumentAsIDProof.Visible = true;
                IdDuplicateProofCode = documentidproofforduplicatetypes.Split(',');

                foreach (ListItem item in ddlDocumentIdProof.Items)
                {
                    foreach (string str in IdDuplicateProofCode)
                    {
                        if (item.Value == str)
                        {
                            item.Selected = true;
                        }
                    }
                }
            }

            string[] AddnewProofCode;

            if (!string.IsNullOrEmpty(documentaddressfornewtypes))
            {
                pnlAddress.Visible = true;
                chkAddNew.Checked = true;
                AddnewProofCode = documentaddressfornewtypes.Split(',');

                foreach (ListItem item in ddlDocumentAddProof.Items)
                {
                    foreach (string str in AddnewProofCode)
                    {
                        if (item.Value == str)
                        {
                            item.Selected = true;
                        }
                    }
                }
            }

            string[] AddRenewProofCode;

            if (!string.IsNullOrEmpty(documentaddressforrenewtypes))
            {
                pnlAddress.Visible = true;
                chkAddRenew.Checked = true;
                AddRenewProofCode = documentaddressforrenewtypes.Split(',');

                foreach (ListItem item in ddlDocumentAddProof.Items)
                {
                    foreach (string str in AddRenewProofCode)
                    {
                        if (item.Value == str)
                        {
                            item.Selected = true;
                        }
                    }
                }
            }

            string[] AddDuplicateProofCode;

            if (!string.IsNullOrEmpty(documentaddressforduplicatetypes))
            {
                pnlAddress.Visible = true;
                AddDuplicateProofCode = documentaddressforduplicatetypes.Split(',');

                foreach (ListItem item in ddlDocumentAddProof.Items)
                {
                    foreach (string str in AddDuplicateProofCode)
                    {
                        if (item.Value == str)
                        {
                            item.Selected = true;
                        }
                    }
                }
            }

            if (!string.IsNullOrEmpty(enroutechkdocument))
            {
                ddlenroutechkdoc.SelectedValue = enroutechkdocument;
            }
            else
            {
                ddlenroutechkdoc.SelectedValue = "N";
            }
        }
    }
    protected void chkgst_CheckedChanged(object sender, EventArgs e)
    {
        if (chkgst.Checked)
        {
            ddlgsttype.Visible = true;
        }
        else
        {
            ddlgsttype.Visible = false;
        }
    }
    protected void chkGender_CheckedChanged(object sender, EventArgs e)
    {
        if (chkGender.Checked)
        {
            ddlGender.Visible = true;
        }
        else
        {
            ddlGender.Visible = false;
        }
    }
    protected void chkAgeGroup_CheckedChanged(object sender, EventArgs e)
    {
        if (chkAgeGroup.Checked)
        {
            pnlAge.Visible = true;
        }
        else
        {
            pnlAge.Visible = false;
        }
    }
    protected void chkServiceType_CheckedChanged(object sender, EventArgs e)
    {
        if (chkServiceType.Checked)
        {
            ddlServiceType.Visible = true;
        }
        else
        {
            ddlServiceType.Visible = false;
        }
    }
    protected void chkState_CheckedChanged(object sender, EventArgs e)
    {
        if (chkState.Checked)
        {
            pnlState.Visible = true;
        }
        else
        {
            pnlState.Visible = false;
        }
    }
    protected void chkKms_CheckedChanged(object sender, EventArgs e)
    {
        if (chkKms.Checked)
        {
            pnlKm.Visible = true;
        }
        else
        {
            pnlKm.Visible = false;
        }
    }
    protected void chkRenewYN_CheckedChanged(object sender, EventArgs e)
    {
        if (chkRenewYN.Checked)
        {
            pnlRenewDays.Visible = true;
        }
        else
        {
            pnlRenewDays.Visible = false;
        }
    }
    protected void ddlDocumentIdProof_SelectedIndexChanged(object sender, EventArgs e)
    {
        int count = 0;

        if (ddlDocumentIdProof.SelectedValue != "0")
        {
            foreach (ListItem item in ddlDocumentIdProof.Items)
            {
                if (item.Selected)
                {
                    pnlDocumentAsIDProof.Visible = true;
                    count++;
                }
            }

            if (count == 0)
            {
                pnlDocumentAsIDProof.Visible = false;
            }
        }

    }
    protected void ddlDocumentAddProof_SelectedIndexChanged(object sender, EventArgs e)
    {
        int count = 0;

        if (ddlDocumentAddProof.SelectedValue != "0")
        {
            foreach (ListItem item in ddlDocumentAddProof.Items)
            {
                if (item.Selected)
                {
                    pnlAddress.Visible = true;
                    count++;
                }
            }

            if (count == 0)
            {
                pnlAddress.Visible = false;
            }
        }

    }
    protected void gvBusPassTypes_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbtnActivate = (LinkButton)e.Row.FindControl("lbtnActivate");
            LinkButton lbtnDeactivate = (LinkButton)e.Row.FindControl("lbtnDeactivate");
            LinkButton lbtnUpdateBusPassTypes = (LinkButton)e.Row.FindControl("lbtnUpdateBusPassTypes");
            DataRowView rowView = (DataRowView)e.Row.DataItem;

            lbtnActivate.Visible = false;
            lbtnDeactivate.Visible = false;
            lbtnUpdateBusPassTypes.Visible = false;

            if (rowView["current_status"].ToString() == "A")
            {
                lbtnDeactivate.Visible = true;
                lbtnUpdateBusPassTypes.Visible = true;
            }
            else if (rowView["current_status"].ToString() == "D")
            {
                lbtnActivate.Visible = true;
            }
        }

    }
    protected void rbtadvancefare_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtadvancefare.SelectedValue == "Y")
        {
            dvadvancefare.Visible = true;
        }
        else
        {
            dvadvancefare.Visible = false;
        }
    }
    protected void gvBusPassTypes_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvBusPassTypes.PageIndex = e.NewPageIndex;
        BusPassTypeList();
        ResetControl();
    }
    #endregion
}