using System;
using Npgsql;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CaptchaDLL;
using System.IO;

public partial class AgentRegistration : outsidebasepage
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    byte[] PhotoImage = null;
    [DllImport("urlmon.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = false)]
    static extern int FindMimeFromData(IntPtr pBC,
       [MarshalAs(UnmanagedType.LPWStr)] string pwzUrl,
       [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1, SizeParamIndex = 3)] byte[] pBuffer,
       int cbSize,
       [MarshalAs(UnmanagedType.LPWStr)] string pwzMimeProposed,
       int dwMimeFlags, out IntPtr ppwzMimeOut, int dwReserved);
    protected void Page_Load(object sender, EventArgs e)
    {

        Session["Heading"] = "Agent Registration";
        Session["Heading1"] = "Agent Registration ";
        if (!IsPostBack)
        {
            if (sbXMLdata.checkModuleCategory("70") == false)
            {
                Response.Redirect("Errorpage.aspx");
            }
            RefreshCaptcha();
            loadState(ddlstate);
            loadDistrict(ddlstate.SelectedValue, ddlDistrict);
            loadCity();
            LoadProofTypes(ddlAddressProofType);
            LoadProofTypes(ddlIdProofType);
            Session["Statusfile"] = null;
            Session["RegCopyfile"] = null;
            Session["Addressprooffile"] = null;
            Session["Idprooffile"] = null;
            lblTC.Text = LoadTersnCndition();
            loadstation(ddlstation); ;
        }
    }

    private void loadstation(DropDownList ddlstn)
    {
        try
        {
            point3.Visible = false;
            point7.Visible = false;
            rbtno.Visible = false;
            rbtyes.Visible = false;
            ddlstn.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "agent.fun_getagent_configstation");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlstn.DataSource = dt;
                    ddlstn.DataTextField = "station_name";
                    ddlstn.DataValueField = "station_code";
                    ddlstn.DataBind();
                    point3.Visible = true;
                    point7.Visible = true;
                    rbtyes.Visible = true;
                    rbtno.Visible = true;
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

    #region "Method"
    private string LoadTersnCndition()
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "agent.fun_get_ag_termscondition");
        dt = bll.SelectAll(MyCommand);

        if (dt.TableName == "Success")
        {
            if (dt.Rows.Count > 0)
            {
                string termConditionDtls = HttpUtility.HtmlDecode(dt.Rows[0]["term_condition_dtls"].ToString());
                return termConditionDtls;
            }
            return string.Empty;
        }

        return string.Empty;
    }





    private void LoadProofTypes(DropDownList ddlType)//M3
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
    private void loadState(DropDownList ddlstate)//M3
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
    private void RefreshCaptcha()
    {
        tbcaptchacode.Text = "";
        Session["CaptchaImage"] = getRandom();
    }

    public string getRandom()
    {
        Random random = new Random();
        const string src = "0123456789";
        int i;
        string _random = "";
        for (i = 0; i <= 5; i++)
        {
            _random += src[random.Next(0, src.Length)];//random.Next(0, 9).ToString();
        }

        return _random;
    }
    private void errormsg(string errorMessage)
    {
        lblerrmsg.Text = errorMessage;
        mpError.Show();
    }
    private void SuccessMessage(string successMessage)
    {
        lblsucessmsg.Text = successMessage;
        mpconfirm.Show();
    }
    protected void btnUploadpdfRegCopy_Click(object sender, EventArgs e)
    {
        if (IsValidPdf(fudocfileRegCopy) == true)
        {
            if (fudocfileRegCopy.HasFile)
            {
                string _fileFormat = GetMimeDataOfFile(fudocfileRegCopy);
                if (fudocfileRegCopy.FileName.Length <= 50)
                {
                    if (Convert.ToInt32(fudocfileRegCopy.FileBytes.Length) < 2097152 && fudocfileRegCopy.FileName.Length > 2)
                    {
                        string _NewFileName = fudocfileRegCopy.FileName;
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
                            errormsg("File format not allowed.");
                            return;
                        }
                    }
                    else
                    {
                        errormsg("Copy of registration for proof file less than 2 MB");
                        return;
                    }
                }
            }
        }
        else
        {
            errormsg("Invalid Copy of registration for proof file (Either not a pdf file or file size is more than 2 MB");
            return;
        }

        Session["RegCopyfile"] = convertByteFilePDF(fudocfileRegCopy);
        lblPDFRegCopy.Text = fudocfileRegCopy.FileName;
        lblPDFRegCopy.Visible = true;

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

    public static string GetMimeDataOfFile(FileUpload file1)
    {
        HttpPostedFile file = file1.PostedFile;
        IntPtr mimeout;

        int MaxContent = (int)file.ContentLength;
        if (MaxContent > 4096) MaxContent = 4096;

        byte[] buf = new byte[MaxContent];
        file.InputStream.Read(buf, 0, MaxContent);
        int result = FindMimeFromData(IntPtr.Zero, file.FileName, buf, MaxContent, null, 0, out mimeout, 0);

        if (result != 0)
        {
            Marshal.FreeCoTaskMem(mimeout);
            return "";
        }

        string mime = Marshal.PtrToStringUni(mimeout);
        Marshal.FreeCoTaskMem(mimeout);

        return mime.ToLower();
    }

    private bool IsValidPdf(FileUpload fudocfileRegCopy)
    {

        string _fileFormat = GetMimeDataOfFile(fudocfileRegCopy);
        if (_fileFormat == "application/pdf")
        {
            return true;
        }
        else
        {
            errormsg("Invalid file (Not a PDF)");
            return false;
        }
    }
    public void Reset()
    {
        txtname.Text = "";
        txtContactName.Text = "";
        txtmobileno.Text = "";
        txtemail.Text = "";
        ddlstate.SelectedValue = "0";
        ddlDistrict.SelectedValue = "0";
        ddlcity.SelectedValue = "0";
        txtaddress.Text = "";
        txtPinCode.Text = "";
        txtPanNo.Text = "";
        ddlstatus.SelectedValue = "I";
        dvcopy.Visible = false;
        lblPDF.Text = "";
        Session["Statusfile"] = null;
        rbtexperience.SelectedValue = "Y";
        dvexperience.Visible = true;
        txtnoofyear.Text = "";
        lblPDFRegCopy.Text = "";
        Session["RegCopyfile"] = null;
        ddlAddressProofType.SelectedValue = "0";
        lblPDFAddressproof.Text = "";
        Session["Addressprooffile"] = null;
        ddlIdProofType.SelectedValue = "0";
        lblPDFIdproof.Text = "";
        Session["Idprooffile"] = null;
        RefreshCaptcha();
        chkTOC.Checked = false;
    }

    private void saveDetails()//M7
    {
        try
        {
            string IPAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(IPAddress))
            {
                IPAddress = Request.ServerVariables["REMOTE_ADDR"];
            }
            string name = txtname.Text;
            string contactPerName = txtContactName.Text;
            string MobileNo = txtmobileno.Text;
            string emailID = txtemail.Text;
            string state = ddlstate.SelectedValue.Trim();
            string district = ddlDistrict.SelectedValue.Trim();
            string city = ddlcity.SelectedValue.Trim();
            string address = txtaddress.Text;
            string pincode = txtPinCode.Text;
            string panNo = txtPanNo.Text;
            string legalstatus = ddlstatus.SelectedValue;
            string bookingexperience = rbtexperience.SelectedValue;
            string noofyear = "0";
            if (!string.IsNullOrEmpty(txtnoofyear.Text))
            {
                noofyear = txtnoofyear.Text;
            }
            string addressprooftype = ddlAddressProofType.SelectedValue.Trim();
            string idprooftype = ddlIdProofType.SelectedValue.Trim();

            string Facility = "";
            string stoncode = "0";
            if (rbtyes.Checked==true)
            {
                Facility = "B";
                stoncode = ddlstation.SelectedValue;
            }
            byte[] Statusfile = null;
            Statusfile = Session["Statusfile"] as byte[];
            byte[] RegCopyfile = null;
            RegCopyfile = Session["RegCopyfile"] as byte[];
            byte[] Addressprooffile = null;
            Addressprooffile = Session["Addressprooffile"] as byte[];
            byte[] Idprooffile = null;
            Idprooffile = Session["Idprooffile"] as byte[];
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "agent.fun_agrequestinsert");
            MyCommand.Parameters.AddWithValue("p_name", name);
            MyCommand.Parameters.AddWithValue("p_contactperson", contactPerName);
            MyCommand.Parameters.AddWithValue("p_mobile", MobileNo);
            MyCommand.Parameters.AddWithValue("p_email", emailID);
            MyCommand.Parameters.AddWithValue("p_state", state);
            MyCommand.Parameters.AddWithValue("p_district", district);
            MyCommand.Parameters.AddWithValue("p_city", city);
            MyCommand.Parameters.AddWithValue("p_address", address);
            MyCommand.Parameters.AddWithValue("p_pincode", pincode);
            MyCommand.Parameters.AddWithValue("p_panno", panNo);
            MyCommand.Parameters.AddWithValue("p_legalstatus", legalstatus);
            MyCommand.Parameters.AddWithValue("p_statusdoc", (object)Statusfile ?? DBNull.Value);
            MyCommand.Parameters.AddWithValue("p_experience", bookingexperience);
            MyCommand.Parameters.AddWithValue("p_noofyear", noofyear);
            MyCommand.Parameters.AddWithValue("p_regdoc", (object)RegCopyfile ?? DBNull.Value);
            MyCommand.Parameters.AddWithValue("p_addprooftype", addressprooftype);
            MyCommand.Parameters.AddWithValue("p_addproofdoc", (object)Addressprooffile ?? DBNull.Value);
            MyCommand.Parameters.AddWithValue("p_idprooftype", idprooftype);
            MyCommand.Parameters.AddWithValue("p_idproofdoc", (object)Idprooffile ?? DBNull.Value);
            MyCommand.Parameters.AddWithValue("p_ipaddress", IPAddress);
            MyCommand.Parameters.AddWithValue("p_updatedby", MobileNo);
            MyCommand.Parameters.AddWithValue("p_facility", Facility);
            MyCommand.Parameters.AddWithValue("p_stoncode", stoncode);
           
            dt = bll.SelectAll(MyCommand);

            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    Session["referenceNo"] = dt.Rows[0]["val_refno"].ToString();
                    SuccessMessage("Application details have been successfully Submitted. <br /> Please note Reference No. <b>" + Session["referenceNo"].ToString() + "</b> for future reference.");
                    Reset();
                }
                else
                {
                    errormsg("An error occurred while saving details.");
                }
            }
            else
            {
                errormsg("An error occurred while saving details: " + dt.TableName);
            }

        }


        catch (Exception ex)
        {
            errormsg("There is some error.");
            _common.ErrorLog("SysAdmETM-M7", ex.Message.ToString());
        }
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {

            if (!IsValidValues())
            {
                return;
            }
            lblConfirmation.Text = "Do you want to submit details?";
            mpConfirmation.Show();
        }
        catch (Exception ex)
        {
            errormsg("Error while saving1");
        }

    }
    private bool IsValidValues()
    {
        try
        {
            if (chkTOC.Checked == false)
            {
                errormsg("Please Check Terms & Condition");
                return false;
            }

            //if (!((Session["CaptchaImage"] != null) && (tbcaptchacode.Text.ToLower() == Session["CaptchaImage"].ToString().ToLower())))
            //{
            //    errormsg("Invalid Security Code (Shown in Image). Please Try Again");
            //    RefreshCaptcha();
            //    return false;
            //}

            int msgcount = 0;
            string msg = "Enter Valid <br/>";

            if (!_validation.IsValidString(txtname.Text.Trim(), 1, txtname.MaxLength))
            {
                msgcount++;
                msg += msgcount.ToString() + ". Agency/Agent Name.<br/>";
            }

            if (!_validation.IsValidString(txtContactName.Text.Trim(), 1, txtContactName.MaxLength))
            {
                msgcount++;
                msg += msgcount.ToString() + ". Contact Person Name.<br/>";
            }

            if (!_validation.IsValidInteger(txtmobileno.Text, 10, 10))
            {
                msgcount++;
                msg += msgcount.ToString() + ". Mobile Number.<br/>";
            }

            if (!_validation.isValideMailID(txtemail.Text.Trim()))
            {
                msgcount++;
                lblerrmsg.Text = ". Email ID.";
            }

            if (!_validation.IsValidInteger(ddlstate.SelectedValue, 1, 2))
            {
                msgcount++;
                msg += msgcount.ToString() + ". State.<br/>";
            }

            if (!_validation.IsValidInteger(ddlDistrict.SelectedValue, 1, 5))
            {
                msgcount++;
                msg += msgcount.ToString() + ". District.<br/>";
            }

            if (!_validation.IsValidInteger(ddlcity.SelectedValue, 1, 5))
            {
                msgcount++;
                msg += msgcount.ToString() + ". City.<br/>";
            }

            if (!_validation.IsValidAddress(txtaddress.Text.Trim(), 1, txtaddress.MaxLength))
            {
                msgcount++;
                msg += msgcount.ToString() + ". Address.<br/>";
            }

            if (!_validation.IsValidInteger(txtPinCode.Text, 6, 6))
            {
                msgcount++;
                msg += msgcount.ToString() + ". Pincode.<br/>";
            }

            if (!_validation.IsValidString(txtPanNo.Text.Trim(), 1, txtname.MaxLength))
            {
                msgcount++;
                msg += msgcount.ToString() + ". PAN No.<br/>";
            }
            else
            {
                if (!isAlphanumericCharacters(txtPanNo.Text.ToUpper().Trim()))
                {
                    msgcount++;
                    msg += msgcount.ToString() + ". PAN No.<br/>";
                }
            }

            if (ddlstatus.SelectedValue == "P")
            {
                if (Session["Statusfile"] == null)
                {
                    msgcount++;
                    msg += msgcount.ToString() + ". Attach certified copy for Status.<br/>";
                }
            }

            if (rbtexperience.SelectedValue == "Y")
            {
                if (!_validation.IsValidInteger(txtnoofyear.Text, 1, 2))
                {
                    msgcount++;
                    msg += msgcount.ToString() + ". Number Of Years Experience.<br/>";
                }
                if (Session["RegCopyfile"] == null)
                {
                    msgcount++;
                    msg += msgcount.ToString() + ". Copy of registration for proof.<br/>";
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

            if (rbtyes.Checked == true)
            {
                if (ddlstation.SelectedValue == "0")
                {
                    msgcount++;
                    msg += msgcount.ToString() + ". Station for Current Booking Facility.<br/>";
                }
            }

            if (msgcount > 0)
            {
                errormsg(msg);
                return false;
            }
            return true;

        }
        catch (Exception)
        {
            errormsg("Please check values Errorcode AgentRegistration");
            return false;
        }

    }
    private bool isAlphanumericCharacters(string valChk)
    {

        for (int iLoop = 0; iLoop < valChk.Length; iLoop++)
        {
            char strChk = valChk[iLoop];
            int strKey = (int)strChk;

            if (!((strKey >= 65 && strKey <= 90) || (strKey >= 97 && strKey <= 122) || (strKey >= 48 && strKey <= 57)))
            {
                return false;
            }
        }
        return true;
    }
    private bool FileContainsMaliciousCode(byte[] bytes)
    {
        string fileContent = "";

        using (MemoryStream stream = new MemoryStream(bytes))
        {
            using (StreamReader reader = new StreamReader(stream))
            {
                fileContent = reader.ReadToEnd();
            }
        }
        string[] maliciousPatterns = { "<script>", "javascript:", "javascript", "eval(", "src=", "onmouseover=", "<iframe>", "<object>", "<embed>", "<form>", "<svg>", "XSS" };
        foreach (string pattern in maliciousPatterns)
        {
            if (fileContent.ToUpper().Contains(pattern.ToUpper()))
            {
                return true;
            }
        }
        return false;
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
    protected void lbtnRefresh_Click(object sender, EventArgs e)
    {
        RefreshCaptcha();
    }
    
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        saveDetails();
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        Reset();
    }


    protected void rbtexperience_SelectedIndexChanged(object sender, EventArgs e)
    {
        dvexperience.Visible = true;
        if (rbtexperience.SelectedValue == "Y")
        {
            dvexperience.Visible = true;
        }
        else
        {
            dvexperience.Visible = false;
        }

    }


    protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        dvcopy.Visible = false;
        if (ddlstatus.SelectedValue == "I")
        {
            dvcopy.Visible = false;
        }
        else if (ddlstatus.SelectedValue == "P")
        {
            dvcopy.Visible = true;
        }
    }


    protected void btnUploadpdf_Click(object sender, EventArgs e)
    {
        if (IsValidPdf(fudocfile) == true)
        {
            if (fudocfile.HasFile)
            {
                Byte[] bytes = fudocfile.FileBytes;
                if (FileContainsMaliciousCode(bytes) == true)
                {
                    errormsg("Invalid File. Please upload a different file.");
                    return;
                }

                string _fileFormat = GetMimeDataOfFile(fudocfile);
                if (fudocfile.FileName.Length <= 50)
                {
                   
                    if (fudocfile.FileBytes.Length < 2097152 && fudocfile.FileName.Length > 2)
                    {
                        string _NewFileName = fudocfile.FileName;
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
                            errormsg("File format not allowed.");
                            return;
                        }
                    }
                    else
                    {
                        errormsg("Attach certified copy file less than 2 MB");
                        return;
                    }
                }
            }
        }
        else
        {
            errormsg("Invalid Attach certified copy file (Either not a pdf file or file size is more than 2 MB");
            return;
        }

        Session["Statusfile"] = convertByteFilePDF(fudocfile);
        lblPDF.Text = fudocfile.FileName;
        lblPDF.Visible = true;

    }
    protected void btnUploadpdfAddressproof_Click(object sender, EventArgs e)
    {
        if (IsValidPdf(fudocfileAddressproof) == true)
        {
            if (fudocfileAddressproof.HasFile)
            {
                Byte[] bytes = fudocfileAddressproof.FileBytes;
                if (FileContainsMaliciousCode(bytes) == true)
                {
                    errormsg("Invalid File. Please upload a different file.");
                    return;
                }
                string _fileFormat = GetMimeDataOfFile(fudocfileAddressproof);
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
                            errormsg("File format not allowed.");
                            return;
                        }
                    }
                    else
                    {
                        errormsg("Address Proof file less than 2 MB");
                        return;
                    }
                }
            }
        }
        else
        {
            errormsg("Invalid address proof file (Either not a pdf file or file size is more than 2 MB");
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
                Byte[] bytes = fudocfileIdproof.FileBytes;
                if (FileContainsMaliciousCode(bytes) == true)
                {
                    errormsg("Invalid File. Please upload a different file.");
                    return;
                }
                string _fileFormat = GetMimeDataOfFile(fudocfileIdproof);
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
                            errormsg("File format not allowed.");
                            return;
                        }
                    }
                    else
                    {
                        errormsg("Id Proof file less than 2 MB");
                        return;
                    }
                }
            }
        }
        else
        {
            errormsg("Invalid id proof file (Either not a pdf file or file size is more than 2 MB");
            return;
        }
        Session["Idprooffile"] = convertByteFilePDF(fudocfileIdproof);
        lblPDFIdproof.Text = fudocfileIdproof.FileName;
        lblPDFIdproof.Visible = true;

    }

    
    protected void rbtyes_CheckedChanged(object sender, EventArgs e)
    {
        loadstation(ddlstation);
        dvstation.Visible = true;
    }

    protected void rbtno_CheckedChanged(object sender, EventArgs e)
    {
        loadstation(ddlstation);
        dvstation.Visible = false;
    }
    #endregion









}