using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_CSPRegistration : System.Web.UI.Page
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    CommonSMSnEmail comm = new CommonSMSnEmail();
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["_moduleName"] = "Common Sevice Provider Registration";

        if (!IsPostBack)
        {

            loadState(ddlstate);
            loadDistrict(ddlstate.SelectedValue, ddlDistrict);
            loadCity();
            LoadProofTypes(ddlAddressProofType);
            LoadProofTypes(ddlIdProofType);
            LoadCSP();
        }

    }


    #region "Methods"

    private void loadState(DropDownList ddlstate)
    {
        try
        {
            ddlstate.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.f_get_states");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlstate.DataSource = dt;
                    ddlstate.DataTextField = "stname";
                    ddlstate.DataValueField = "stcode";
                    ddlstate.DataBind();
                }
            }
            else
            {
                _common.ErrorLog("AgentRegistration-M3", dt.TableName);
            }
            ddlstate.Items.Insert(0, "SELECT");
            ddlstate.Items[0].Value = "0";
            ddlstate.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlstate.Items.Insert(0, "SELECT");
            ddlstate.Items[0].Value = "0";
            ddlstate.SelectedIndex = 0;
            _common.ErrorLog("AgentRegistration-M3", ex.Message.ToString());
        }
    }
    private void loadDistrict(string State_code, DropDownList ddldistrict)
    {
        try
        {
            ddldistrict.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.f_get_district");
            MyCommand.Parameters.AddWithValue("p_statecode", State_code);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddldistrict.DataSource = dt;
                    ddldistrict.DataTextField = "distname";
                    ddldistrict.DataValueField = "distcode";
                    ddldistrict.DataBind();
                }
            }
            else
            {
                _common.ErrorLog("SysAdmAgentRegistration", dt.TableName);
            }

            ddldistrict.Items.Insert(0, "SELECT");
            ddldistrict.Items[0].Value = "0";
            ddldistrict.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddldistrict.Items.Insert(0, "SELECT");
            ddldistrict.Items[0].Value = "0";
            ddldistrict.SelectedIndex = 0;
            _common.ErrorLog("SysAdmAgentRegistration", ex.Message.ToString());
        }
    }
    public void loadCity()
    {
        try
        {
            ddlcity.Items.Clear();
            string State = ddlstate.SelectedValue.ToString();
            string District = ddlDistrict.SelectedValue.ToString();

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.f_get_city");
            MyCommand.Parameters.AddWithValue("p_statecode", State);
            MyCommand.Parameters.AddWithValue("p_distcode", District);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlcity.DataSource = dt;
                    ddlcity.DataTextField = "ctyname";
                    ddlcity.DataValueField = "ctycode";
                    ddlcity.DataBind();
                }
            }
            else
            {
                _common.ErrorLog("SysAdmAgentRegistration", dt.TableName);
            }
            ddlcity.Items.Insert(0, "SELECT");
            ddlcity.Items[0].Value = "0";
            ddlcity.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlcity.Items.Insert(0, "SELECT");
            ddlcity.Items[0].Value = "0";
            ddlcity.SelectedIndex = 0;
            _common.ErrorLog("SysAdmAgentRegistration", ex.Message.ToString());
        }
    }


    private void LoadProofTypes(DropDownList ddlType)
    {
        try
        {
            ddlType.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "agent.fun_mgetaddressprooftypes");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlType.DataSource = dt;
                    ddlType.DataTextField = "proof_name";
                    ddlType.DataValueField = "proof_id";
                    ddlType.DataBind();
                }
            }
            else
            {
                _common.ErrorLog("AgentRegistration", dt.TableName);
            }
            ddlType.Items.Insert(0, "SELECT");
            ddlType.Items[0].Value = "0";
            ddlType.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlType.Items.Insert(0, "SELECT");
            ddlType.Items[0].Value = "0";
            ddlType.SelectedIndex = 0;
            _common.ErrorLog("AgentRegistration", ex.Message.ToString());
        }
    }


    private void loadstation(DropDownList ddlstn)
    {
        try
        {
            ddlstn.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "agent.fun_get_cntrstation");
            MyCommand.Parameters.AddWithValue("@p_depotocde", "0");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlstn.DataSource = dt;
                    ddlstn.DataTextField = "station_name";
                    ddlstn.DataValueField = "station_code";
                    ddlstn.DataBind();
                }
            }
            else
            {
                _common.ErrorLog("AgentRegistration", dt.TableName);
            }
            ddlstn.Items.Insert(0, "SELECT");
            ddlstn.Items[0].Value = "0";
            ddlstn.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlstn.Items.Insert(0, "SELECT");
            ddlstn.Items[0].Value = "0";
            ddlstn.SelectedIndex = 0;
            _common.ErrorLog("AgentRegistration", ex.Message.ToString());
        }
    }

    private bool IsValidValue()
    {
        string msg = "";
        int msgcount = 0;

        if (!_validation.IsValidString(txtAgentName.Text.Trim(), 1, 50))
        {
            msgcount++;
            msg += msgcount.ToString() + ". Agency/Agent Name <br/>";
        }

        if (!_validation.IsValidString(txtContactName.Text.Trim(), 1, 50))
        {
            msgcount++;
            msg += msgcount.ToString() + ". Contact Person Name<br/>";
        }

        if (!_validation.IsValidInteger(txtmobileno.Text, 10, 10))
        {
            msgcount++;
            msg += msgcount.ToString() + ". Mobile Number <br/>";
        }

        if (string.IsNullOrEmpty(txtemail.Text))
        {
            msgcount++;
            msg += msgcount.ToString() + ". e-Mail ID<br/>";
        }
        else if (!_validation.isValideMailID(txtemail.Text))
        {
            msgcount++;
            msg += msgcount.ToString() + ". e-Mail ID <br/>";
        }

        if (ddlstate.SelectedIndex <= 0)
        {
            msgcount++;
            msg += msgcount.ToString() + ". State <br/>";
        }
        else
        {
            if (!_validation.IsValidInteger(ddlstate.SelectedValue, 1, 2))
            {
                msgcount++;
                msg += msgcount.ToString() + ". State <br/>";
            }
        }

        if (ddlDistrict.SelectedIndex <= 0)
        {
            msgcount++;
            msg += msgcount.ToString() + ". District <br/>";
        }
        else
        {
            if (!_validation.IsValidInteger(ddlDistrict.SelectedValue, 1, 5))
            {
                msgcount++;
                msg += msgcount.ToString() + ". District <br/>";
            }
        }

        if (ddlcity.SelectedIndex <= 0)
        {
            msgcount++;
            msg += msgcount.ToString() + ". City <br/>";
        }
        else
        {
            if (!_validation.IsValidInteger(ddlcity.SelectedValue, 1, 5))
            {
                msgcount++;
                msg += msgcount.ToString() + ". City <br/>";
            }
        }

        if (!_validation.IsValidString(txtAddress.Text.Trim(), 1, 100))
        {
            msgcount++;
            msg += msgcount.ToString() + ". Agent Address <br/>";
        }

        if (!_validation.IsValidInteger(txtPinCode.Text, 6, 6))
        {
            msgcount++;
            msg += msgcount.ToString() + ". Pincode.<br/>";
        }

        if (!_validation.IsValidString(txtPanNo.Text.Trim(), 1, 10))
        {
            msgcount++;
            msg += msgcount.ToString() + ". PAN Number <br/>";
        }

        if (chksecurity.Checked)
        {
            if (!_validation.IsValidInteger(txtSecurityAmount.Text.Trim(), 1, 10))
            {
                msgcount++;
                msg += msgcount.ToString() + ". Security Amount <br/>";
            }

            if (ddlDepositType.SelectedIndex == 0)
            {
                msgcount++;
                msg += msgcount.ToString() + ". Deposit Type <br/>";
            }

            if (!_validation.IsValidString(txtBank.Text.Trim(), 1, 50))
            {
                msgcount++;
                msg += msgcount.ToString() + ". Bank Name <br/>";
            }
        }

        if (!_validation.IsValidInteger(ddlAddressProofType.SelectedValue, 1, 2))
        {
            msgcount++;
            msg += msgcount.ToString() + ". Address Proof.<br/>";
        }
        else
        {
            if (ddlAddressProofType.SelectedValue == "0")
            {
                msgcount++;
                msg += msgcount.ToString() + ". Address Proof.<br/>";
            }
            else
            {
                if (Session["Addressprooffile"] == null)
                {
                    msgcount++;
                    msg += msgcount.ToString() + ". Copy of Address for proof.<br/>";
                }
            }
        }

        if (!_validation.IsValidInteger(ddlIdProofType.SelectedValue, 1, 2))
        {
            msgcount++;
            msg += msgcount.ToString() + ". ID Proof.<br/>";
        }
        else
        {
            if (ddlIdProofType.SelectedValue == "0")
            {
                msgcount++;
                msg += msgcount.ToString() + ". ID Proof.<br/>";
            }
            else
            {
                if (Session["Idprooffile"] == null)
                {
                    msgcount++;
                    msg += msgcount.ToString() + ". Copy of ID for proof.<br/>";
                }
            }
        }

        if (chkcurnt.Checked)
        {
            if (ddlstation.SelectedValue == "0")
            {
                msgcount++;
                msg += msgcount.ToString() + ". Station for Current Booking Facility.<br/>";
            }
        }

        if (msgcount > 0)
        {
            ErrorMessage(msg);
            return false;
        }

        return true;
    }

    private void InsertUpdateAgent()
    {
        try
        {
            string Maction = "";
            string strAgentCcode = "";

            if (Session["Action"].ToString() == "A" || Session["Action"].ToString() == "S")
            {
                strAgentCcode = "";
                Maction = "A";
            }
            if (Session["Action"].ToString() == "U")
            {
                strAgentCcode = Session["Agcode"].ToString();
                Maction = "U";
            }

            string IPAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(IPAddress))
            {
                IPAddress = Request.ServerVariables["REMOTE_ADDR"];
            }

            string name = txtAgentName.Text;
            string contactPerName = txtContactName.Text;
            string MobileNo = txtmobileno.Text;
            string emailID = txtemail.Text;
            int state = Convert.ToInt32(ddlstate.SelectedValue);
            int district = Convert.ToInt32(ddlDistrict.SelectedValue);
            int city = Convert.ToInt32(ddlcity.SelectedValue);
            string address = txtAddress.Text;
            string pincode = txtPinCode.Text;
            string panNo = txtPanNo.Text;
            //Session["RegCopyfile"] = null;
            int addressprooftype = Convert.ToInt32(ddlAddressProofType.SelectedValue);
            //Session["Addressprooffile"] = null;
            int idprooftype = Convert.ToInt32(ddlIdProofType.SelectedValue);
            byte[] Idprooffile = null;
            Idprooffile = Session["Idprooffile"] as byte[];
            byte[] Addressprooffile = null;
            Addressprooffile = Session["Addressprooffile"] as byte[];
            string Facility = "";
            int stoncode = 0;

            if (chkcurnt.Checked)
            {
                Facility = "B";
                stoncode = Convert.ToInt32(ddlstation.SelectedValue);
            }

            decimal securityAmount = 0;

            if (!string.IsNullOrEmpty(txtSecurityAmount.Text))
            {
                securityAmount = decimal.Parse(txtSecurityAmount.Text.Trim());
            }

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "csc.cscdetailsinsert");
            MyCommand.Parameters.AddWithValue("p_action", Maction);
            MyCommand.Parameters.AddWithValue("p_agentcode", strAgentCcode);
            MyCommand.Parameters.AddWithValue("p_name", name);
            MyCommand.Parameters.AddWithValue("p_contactpername", contactPerName);
            MyCommand.Parameters.AddWithValue("p_mobile", MobileNo);
            MyCommand.Parameters.AddWithValue("p_email", emailID);
            MyCommand.Parameters.AddWithValue("p_state", state);
            MyCommand.Parameters.AddWithValue("p_district", district);
            MyCommand.Parameters.AddWithValue("p_city", city);
            MyCommand.Parameters.AddWithValue("p_address", address);
            MyCommand.Parameters.AddWithValue("p_pincode", pincode);
            MyCommand.Parameters.AddWithValue("p_panno", panNo);
            MyCommand.Parameters.AddWithValue("p_servcietaxno", txtServiceTax.Text);
            MyCommand.Parameters.AddWithValue("p_securityamount", securityAmount);
            MyCommand.Parameters.AddWithValue("p_deposittype", ddlDepositType.SelectedValue);
            MyCommand.Parameters.AddWithValue("p_bank", txtBank.Text);
            MyCommand.Parameters.AddWithValue("p_refno", txtRefNo.Text);
            MyCommand.Parameters.AddWithValue("p_addprooftype", addressprooftype);
            MyCommand.Parameters.AddWithValue("p_addproofdoc", Addressprooffile);
            MyCommand.Parameters.AddWithValue("p_idprooftype", idprooftype);
            MyCommand.Parameters.AddWithValue("p_idproofdoc", Idprooffile);
            MyCommand.Parameters.AddWithValue("p_facility", Facility);
            MyCommand.Parameters.AddWithValue("p_stoncode", stoncode);
            MyCommand.Parameters.AddWithValue("p_ipaddress", IPAddress);
            MyCommand.Parameters.AddWithValue("p_updatedby", MobileNo);

            dt = bll.SelectAll(MyCommand);

            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {

                    if (Session["Action"].ToString() == "A")
                    {
                        Session["Agcode"] = dt.Rows[0]["val_agentcode"].ToString();
                        SuccessMessage("Application details have been successfully saved vide Reference No <b>" + Session["Agcode"].ToString() + "</b>.");
                    }
                    if (Session["Action"].ToString() == "S")
                    {
                        Session["Agcode"] = dt.Rows[0]["val_agentcode"].ToString();
                        Session["AGENTNAME"] = name;
                        Session["MobileNo"] = MobileNo;
                        Session["EMAIL"] = emailID;
                        VerifyAgent();
                    }
                    // Send Sms and Email
                    InitDetails();
                }
                else
                {
                    ErrorMessage("Error occurred while saving details.");
                }
                LoadCSP();
            }
            else
            {
                ErrorMessage("Error occurred while saving details." + dt.TableName);
            }
        }
        catch (Exception ex)
        {
            ErrorMessage("Error occurred while saving details." + ex.Message);
            _common.ErrorLog("CSPRegistration-006", ex.Message.ToString());
        }
    }

    private void VerifyAgent()
    {

        try
        {
            string IPAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(IPAddress))
            {
                IPAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }

            string PasswordForDB = "test@123";
            //string PasswordForDB = RandomString(6);
            string PASSWORD = FormsAuthentication.HashPasswordForStoringInConfigFile(PasswordForDB, "SHA1");
            

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "csc.agentverification");
            MyCommand.Parameters.AddWithValue("p_agentcode", Session["Agcode"].ToString());
            MyCommand.Parameters.AddWithValue("p_verifyby", Session["_UserCode"]);
            MyCommand.Parameters.AddWithValue("p_ipaddress", IPAddress);
            MyCommand.Parameters.AddWithValue("p_pwd", PASSWORD);
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                SuccessMessage("Common Service Provider  " + (Session["Agcode"].ToString()) + "  has been Verified Successfully.<br>Login Credential have been Shared On Registered email And Registered Mobile Number.");
                //comm.sendDeptNewUserConfirmation_SMS(Session["AGENTNAME"].ToString(), Session["Agcode"].ToString(), PasswordForDB, Session["MobileNo"].ToString(), 13);
                //comm.sendDeptNewUserConfirmation_EMAIL(Session["Agcode"].ToString(), PasswordForDB, Session["EMAIL"].ToString());
            }

        }

        catch (Exception ex)
        {
            _common.ErrorLog("CSPRegistration-006", ex.Message.ToString());
        }
    }


    public string RandomString(int length)
    {
        Random random = new Random();
        char[] charOutput = new char[length];

        for (int i = 0; i < length; i++)
        {
            int selector = random.Next(65, 91); // ASCII values for A to Z
            if (selector > 90)
            {
                selector += random.Next(6, 10); // Add a random number between 6 and 9
            }
            charOutput[i] = (char)selector;
        }

        return new string(charOutput);
    }



    private void InitDetails()
    {
        txtAgentName.Text = "";
        txtContactName.Text = "";
        txtmobileno.Text = "";
        txtemail.Text = "";
        loadState(ddlstate);
        loadDistrict(ddlstate.SelectedValue, ddlDistrict);
        loadCity();
        LoadProofTypes(ddlAddressProofType);
        LoadProofTypes(ddlIdProofType);
        Session["Addressprooffile"] = null;
        Session["Idprooffile"] = null;
        lblPDFIdproof.Text = "";
        lblPDFAddressproof.Text = "";
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
    private bool IsValidPdf(FileUpload fudocfileRegCopy)
    {

        string _fileFormat = GetMimeDataOfFile(fudocfileRegCopy.PostedFile);
        if (_fileFormat == "application/pdf")
        {
            return true;
        }
        else
        {
            ErrorMessage("Invalid file (Not a PDF)");
            return false;
        }
    }

    public byte[] convertByteFilePDF(FileUpload fuFileUpload)
    {
        int intFileLength;
        byte[] byteData = null;

        if (fuFileUpload.HasFile) // File Selected or Not
        {
            // Check File Extension
            if (CheckFileExtensionPDF(fuFileUpload, ".pdf") == true)
            {
                intFileLength = fuFileUpload.PostedFile.ContentLength;
                byteData = new byte[intFileLength];
                byteData = fuFileUpload.FileBytes;
            }
        }
        else
        {
            intFileLength = fuFileUpload.PostedFile.ContentLength;
            byteData = new byte[intFileLength];
            byteData = fuFileUpload.FileBytes;
        }
        return byteData;
    }

    private bool CheckFileExtensionPDF(FileUpload fuFileUpload, string allowedExtension)
    {

        bool fileExtensionOK = false;
        string fileExtension = System.IO.Path.GetExtension(fuFileUpload.FileName).ToLower();
        string[] allowedExtensions = { ".pdf", ".PDF" };

        for (int i = 0; i < allowedExtensions.Length; i++)
        {
            if (fileExtension == allowedExtensions[i])
            {
                fileExtensionOK = true;
                break; // No need to continue if found
            }
        }
        return fileExtensionOK;
    }


    private void LoadCSP()
    {
        try
        {
            grvAgents.Visible = false;
            pnlNoAgent.Visible = true;
            DataTable dt;
            string searchText = txtSearchAgentMIS.Text.Trim();

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "csc.getallcsp");
            MyCommand.Parameters.AddWithValue("p_serachtext", searchText);
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {

                grvAgents.DataSource = dt;
                grvAgents.DataBind();
                grvAgents.Visible = true;
                pnlNoAgent.Visible = false;
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("CSPRegistration-006", ex.Message.ToString());
        }
    }


    private void openpage(string PageName)
    {
        tkt.Src = PageName;
        mpdocment.Show();
    }


    private void changePwd()
    {
        try
        {
            string ipaddress = HttpContext.Current.Request.UserHostAddress;
            string PasswordForEmp = RandomString(8);
            string PASSWORD = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(PasswordForEmp, "SHA1");
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            string Mresult = "";
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_chnage_userstatus");
            MyCommand.Parameters.AddWithValue("p_status", Session["Action"].ToString());
            MyCommand.Parameters.AddWithValue("p_usercode", Session["Agcode"].ToString());
            MyCommand.Parameters.AddWithValue("p_userpwd", PASSWORD);
            MyCommand.Parameters.AddWithValue("p_updateby", Session["Agcode"].ToString());
            MyCommand.Parameters.AddWithValue("p_ipaddress", ipaddress);
            Mresult = bll.UpdateAll(MyCommand);
            if (Mresult == "Success")
            {
                //comm.ChngePwdbyadmin(Session["Agcode"].ToString(), PasswordForEmp, Session["MOBILENUMBER"].ToString());
                SuccessMessage("Agent Password successfully Changed");
            }
            else
            {
                _common.ErrorLog("CSPRegistration.aspx", Mresult);
            }
        }
        catch (Exception ex)
        {
            ErrorMessage("There is some error" + ex.Message);
            _common.ErrorLog("CSPRegistration.aspx", ex.Message.ToString());
        }

    }

    #endregion


    #region M E S S A G E   B O X
    public void ErrorMessage(string message)
    {
        lblerrormsg.Text = message;
        mperror.Show();
    }

    public void SuccessMessage(string message)
    {
        lblsuccessmsg.Text = message;
        mpsuccess.Show();
    }

    public void ConfirmMessage(string message)
    {
        lblConfirmation.Text = message;
        mpConfirmation.Show();
    }



    #endregion

    #region "Events"
    protected void ddlstate_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadDistrict(ddlstate.SelectedValue, ddlDistrict);
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadCity();
    }


    protected void chksecurity_CheckedChanged(object sender, EventArgs e)
    {
        if (chksecurity.Checked == true)
        {
            pnlsecurity.Visible = true;

        }
        else
        {
            pnlsecurity.Visible = false;

        }

    }

    protected void chkcurnt_CheckedChanged(object sender, EventArgs e)
    {
        if (chkcurnt.Checked == true)
        {
            loadstation(ddlstation);
            dvstation.Visible = true;
        }
        else
        {
            dvstation.Visible = false;
        }

    }
    protected void lbtnsaveverify_Click(object sender, EventArgs e)
    {
        try
        {
            if (IsValidValue() == false)
            {
                return;
            }
            Session["Agcode"] = "";
            Session["Action"] = "S";
            lblConfirmation.Text = "Do you want to Register & Verify Common Service Provider Details ?";
            mpConfirmation.Show();
        }
        catch (Exception ex)
        {
            ErrorMessage("Error, Something went wrong errorcode Save 1" + ex.Message);
            return;
        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (!IsValidValue())
            {
                return;
            }
            Session["Agcode"] = "";
            Session["Action"] = "A";
            lblConfirmation.Text = "Do you want to Register Common Service Provider Details ?";
            mpConfirmation.Show();
        }
        catch (Exception ex)
        {
            ErrorMessage("Error, Something went wrong errorcode Save 1" + ex.Message);
            return;
        }

    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        InitDetails();
    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        if (Session["Action"].ToString() == "A" || Session["Action"].ToString() == "U" || Session["Action"].ToString() == "S")
        {
            InsertUpdateAgent();
           
        }
        if(Session["Action"].ToString() == "V" )
        {
            VerifyAgent();
        }
        if (Session["Action"].ToString() == "P")
        {
            changePwd();
        }
    }
    protected void btnUploadpdfAddressproof_Click(object sender, EventArgs e)
    {
        if (IsValidPdf(fudocfileAddressproof) == true)
        {
            if (fudocfileAddressproof.HasFile)
            {
                string _fileFormat = GetMimeDataOfFile(fudocfileAddressproof.PostedFile);
                if (fudocfileAddressproof.FileName.Length <= 50)
                {
                    if (Convert.ToInt32(fudocfileAddressproof.FileBytes.Length) < 2097152 && fudocfileAddressproof.FileName.Length > 2)
                    {
                        string _NewFileName = fudocfileAddressproof.FileName;
                        if (_fileFormat == "application/pdf")
                        {
                            _NewFileName += ".pdf";
                        }
                        else if (_fileFormat == "application/octet-stream")
                        {
                            _NewFileName += ".pdf";
                        }
                        else
                        {
                            ErrorMessage("File format not allowed.");
                            return;
                        }
                    }
                    else
                    {
                        ErrorMessage("Address Proof file less than 2 MB");
                        return;
                    }
                }
            }
        }
        else
        {
            ErrorMessage("Invalid address proof file (Either not a pdf file or file size is more than 2 MB");
            return;
        }
        Session["Addressprooffile"] = convertByteFilePDF(fudocfileAddressproof);
        lblPDFAddressproof.Text = fudocfileAddressproof.FileName;
        lblPDFAddressproof.Visible = true;

    }
    protected void btnUploadpdfIDproof_Click(object sender, EventArgs e)
    {
        if (IsValidPdf(fudocfileIdproof) == true)
        {
            if (fudocfileIdproof.HasFile)
            {
                string _fileFormat = GetMimeDataOfFile(fudocfileIdproof.PostedFile);
                if (fudocfileIdproof.FileName.Length <= 50)
                {
                    if (Convert.ToInt32(fudocfileIdproof.FileBytes.Length) < 2097152 && fudocfileIdproof.FileName.Length > 2)
                    {
                        string _NewFileName = fudocfileIdproof.FileName;
                        if (_fileFormat == "application/pdf")
                        {
                            _NewFileName += ".pdf";
                        }
                        else if (_fileFormat == "application/octet-stream")
                        {
                            _NewFileName += ".pdf";
                        }
                        else
                        {
                            ErrorMessage("File format not allowed.");
                            return;
                        }
                    }
                    else
                    {
                        ErrorMessage("Id Proof file less than 2 MB");
                        return;
                    }
                }
            }
        }
        else
        {
            ErrorMessage("Invalid id proof file (Either not a pdf file or file size is more than 2 MB");
            return;
        }
        Session["Idprooffile"] = convertByteFilePDF(fudocfileIdproof);
        lblPDFIdproof.Text = fudocfileIdproof.FileName;
        lblPDFIdproof.Visible = true;

    }
    protected void ddlDepositType_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtRefNo.Text = String.Empty;
        txtBank.Enabled = true;
        txtRefNo.Enabled = true;
        txtBank.BackColor = System.Drawing.Color.White;
        txtRefNo.BackColor = System.Drawing.Color.White;

        if (ddlDepositType.SelectedItem.Text == "CASH")
        {
            txtRefNo.Text = String.Empty;
            txtRefNo.Enabled = false;
            txtRefNo.BackColor = System.Drawing.Color.DarkGray;
        }

    }
    protected void linkbtnSearchAgentMIS_Click(object sender, EventArgs e)
    {
        string val = txtSearchAgentMIS.Text.Trim();
        if (val.Length < 3)
        {
            ErrorMessage("Enter Minimun 3 characters for search");
            return;
        }
        LoadCSP();

    }

    protected void grvAgents_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvAgents.PageIndex = e.NewPageIndex;
        LoadCSP();
    }



    protected void grvAgents_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        byte[] FileInvoice;
        string base64String = "";

        if (e.CommandName == "ViewIDProof") 
        {
            if (Convert.IsDBNull(grvAgents.DataKeys[index].Values["id_proof_file"]) == true)
            {
                ErrorMessage("File id proof not found");
                return;
            }

            FileInvoice = (byte[])grvAgents.DataKeys[index].Values["id_proof_file"];
            base64String = Convert.ToBase64String(FileInvoice, 0, FileInvoice.Length);
            Session["Base64String"] = base64String;

            openpage("../Pass/ViewDocument.aspx");
        }

        if (e.CommandName == "ViewAddressProof") 
        {
            if (Convert.IsDBNull(grvAgents.DataKeys[index].Values["address_proof_file"]) == true)
            {
                ErrorMessage("File address proof not found");
                return;
            }

            FileInvoice = (byte[])grvAgents.DataKeys[index].Values["address_proof_file"];
            base64String = Convert.ToBase64String(FileInvoice, 0, FileInvoice.Length);
            Session["Base64String"] = base64String;
            openpage("../Pass/ViewDocument.aspx");
        }
        if (e.CommandName == "Verify")
        {
            Session["Agcode"] = grvAgents.DataKeys[index].Values["agent_code"];
            Session["Action"] = "V";
            lblConfirmation.Text = "Do you want to Verify Common Service Provider Details ?";
            mpConfirmation.Show();
        }

        if (e.CommandName == "Deactivate")
        {
            Session["Agcode"] = grvAgents.DataKeys[index].Values["agent_code"];
            mpDeactivation.Show();
        }

        if (e.CommandName == "Refund")
        {
            lblmsg.Text = "";
            lblmsg.Visible = false;
            Session["Agcode"] = grvAgents.DataKeys[index].Values["agent_code"];
            Session["CancelRefno"] = grvAgents.DataKeys[index].Values["cancel_refno"];
            lblponumber.Text = grvAgents.DataKeys[index].Values["payment_orderno"].ToString();
            lbldactivatedt.Text = grvAgents.DataKeys[index].Values["statuson_datetime"].ToString();
            lblrefundamt.Text = grvAgents.DataKeys[index].Values["val_amount"].ToString();
            //loadBank(ddlbank);
            mpRefund.Show();
        }

        if (e.CommandName == "ChndPwd")
        {
            Session["Agcode"] = grvAgents.DataKeys[index].Values["agent_code"];
            Session["MOBILENUMBER"] = grvAgents.DataKeys[index].Values["mobile_no"];
            Session["EMAIL"] = grvAgents.DataKeys[index].Values["val_email"];
            Session["Action"] = "P";
            lblConfirmation.Text = "Do you want to Change Common Service Provider Password ?";
            mpConfirmation.Show();
        }

    }




    protected void linkbtnAllAgentMIS_Click(object sender, EventArgs e)
    {
        string val = txtSearchAgentMIS.Text;
        txtSearchAgentMIS.Text = string.Empty;
        LoadCSP();
    }

  

    protected void grvAgents_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbtnverify = (LinkButton)e.Row.FindControl("lbtnverify");
            LinkButton lbtnDeactivate = (LinkButton)e.Row.FindControl("lbtnDeactivate");
            LinkButton lbtnpwd = (LinkButton)e.Row.FindControl("lbtnpwd");
            DataRowView rowView = (DataRowView)e.Row.DataItem;

            lbtnverify.Visible = false;
            lbtnDeactivate.Visible = false;
            lbtnpwd.Visible = false;

            if (rowView["val_status"].ToString() == "E")
            {
                lbtnverify.Visible = true;
            }
            else if (rowView["val_status"].ToString() == "A")
            {
                lbtnDeactivate.Visible = true;
                lbtnpwd.Visible = true;
            }
        }

    }
    #endregion


}