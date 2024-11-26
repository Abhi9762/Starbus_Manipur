using System;
using Npgsql;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Globalization;
using CaptchaDLL;

public partial class RenewBusPass : outsidebasepage
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
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#staticBackdrop').modal('show');</script>", false);
        Session["headerText"] = "Renew Bus Pass";
        Session["headerTextMessage"] = "Renew Bus Pass";
        if (!IsPostBack)
        {
            if (sbXMLdata.checkModuleCategory("71") == false)
            {
                Response.Redirect("Errorpage.aspx");
            }
            resetctrl();
        }

    }


    #region "Method"
    private void Errormsg(string msg)
    {
        lblerrormsg.Text = msg;
        mperror.Show();
    }
    private void SuccessMsg(string msg)
    {
        lblsuccessmsg.Text = msg;
        mpsuccess.Show();
    }
    private void ConfirmMsg(string msg)
    {
        lblConfirmation.Text = msg;
        mpConfirmation.Show();
    }
    private void resetctrl()
    {
        tbPassnNo.Text = "";
        tbDate.Text = "";
        RefreshCaptcha();
    }
    public void RefreshCaptcha()
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
    private static DataTable GetIDproof(string flag, string documentIDProofForNewTypes)
    {
        string[] ss = documentIDProofForNewTypes.Split(',');
        DataTable table = new DataTable();
        table.Columns.Add("document_type_id", typeof(int));
        table.Columns.Add("document_type_name", typeof(string));

        for (int i = 0; i < ss.Length; i++)
        {


            DataTable dt = new DataTable();
            sbBLL bll = new sbBLL();
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "buspass.f_get_bus_pass_details");
            MyCommand.Parameters.AddWithValue("@p_flag", flag);
            MyCommand.Parameters.AddWithValue("@p_code", ss[i]);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    table.Rows.Add(ss[i], dt.Rows[0]["name"]);
                }
            }
        }

        return table;
    }
    private static DataTable GetAddProof(string flag, string documentAddressForNewTypes)
    {
        string[] ss = documentAddressForNewTypes.Split(',');
        DataTable table = new DataTable();
        table.Columns.Add("document_type_id", typeof(int));
        table.Columns.Add("document_type_name", typeof(string));

        for (int i = 0; i < ss.Length; i++)
        {
            DataTable dt = new DataTable();
            sbBLL bll = new sbBLL();
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "buspass.f_get_bus_pass_details");
            MyCommand.Parameters.AddWithValue("@p_flag", flag);
            MyCommand.Parameters.AddWithValue("@p_code", ss[i]);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    table.Rows.Add(ss[i], dt.Rows[0]["name"]);
                }
            }
        }

        return table;
    }
    private void LoadPassDetails()
    {
        try
        {
            pnlPassDetail.Visible = false;
            pnlPassNorecord.Visible = false;

            string PassNo, PHOTO_RENEW_YN, DOC_ADD_RENEW_TYPES, DOC_ID_RENEW_TYPES;
            PassNo = tbPassnNo.Text;
            string DOB = tbDate.Text;
            MyCommand = new NpgsqlCommand();
            DataTable dt;
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "buspass.getrenewpassdetails");
            MyCommand.Parameters.AddWithValue("@p_passno", PassNo);
            MyCommand.Parameters.AddWithValue("@p_dob", DOB);

            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {

                    if (dt.Rows[0]["renewyn"].ToString() == "N")
                    {
                        Errormsg("Your Bus Pass - " + dt.Rows[0]["passenger_type"].ToString() + " can't be renewed.");
                        lblmsg.Text = "Your Bus Pass - " + dt.Rows[0]["passenger_type"].ToString() + " can't be renewed.";
                        pnlPassDetail.Visible = false;
                        pnlPassNorecord.Visible = true;
                        return;
                    }

                    DateTime dt_PeriodTo = Convert.ToDateTime(dt.Rows[0]["periodto"].ToString());
                    string ssd = (dt_PeriodTo - DateTime.Now.Date).TotalDays.ToString();
                    int p_days = Convert.ToInt32(dt.Rows[0]["noofdays"]);

                    if (Convert.ToInt32(ssd) > p_days)
                    {
                        Errormsg("Your Bus Pass - " + dt.Rows[0]["passenger_type"].ToString() + " can be renewed only before " + p_days.ToString() + " Days");
                        lblmsg.Text = "Your Bus Pass - " + dt.Rows[0]["passenger_type"].ToString() + " can be renewed only before " + p_days.ToString() + " Days";
                        pnlPassDetail.Visible = false;
                        pnlPassNorecord.Visible = true;
                        return;
                    }

                    if (dt_PeriodTo < DateTime.Now)
                    {
                        Errormsg("Your Bus Pass - " + dt.Rows[0]["passenger_type"].ToString() + " has been expired you can't apply for renewal pass now.");
                        lblmsg.Text = "Your Bus Pass - " + dt.Rows[0]["passenger_type"].ToString() + " has been expired you can't apply for renewal pass now.";
                        pnlPassDetail.Visible = false;
                        pnlPassNorecord.Visible = true;
                        return;
                    }

                    lblBusPassType.Text = dt.Rows[0]["cardtypename"] + "(" + dt.Rows[0]["passenger_type"] + ")";
                    lblName.Text = dt.Rows[0]["ctzname"].ToString();
                    lblGender.Text = dt.Rows[0]["gender_"].ToString();
                    lblDOB.Text = dt.Rows[0]["dob"].ToString();
                    lblfname.Text = dt.Rows[0]["f_name"].ToString();

                    if (Convert.IsDBNull(dt.Rows[0]["route_name"]))
                    {
                        lblRoute.Text = "All Station";
                    }
                    else
                    {
                        lblRoute.Text = dt.Rows[0]["route_name"].ToString();
                    }

                    if (Convert.IsDBNull(dt.Rows[0]["startstationname"]))
                    {
                        lblFrom.Text = "All Statoins";
                    }
                    else
                    {
                        lblFrom.Text = dt.Rows[0]["startstationname"].ToString() + " - " + dt.Rows[0]["endstationname"].ToString();
                    }

                    Session["Mobileno"] = dt.Rows[0]["mobileno"];
                    lblMobileNo.Text = dt.Rows[0]["mobileno"].ToString();
                    lblEmail.Text = dt.Rows[0]["emailid"].ToString();
                    lblstateName.Text = dt.Rows[0]["statename"].ToString();
                    lblDistrict.Text = dt.Rows[0]["districtname"].ToString();
                    lblCity.Text = dt.Rows[0]["city"].ToString();
                    lblAddress.Text = dt.Rows[0]["address"].ToString();
                    lblPincode.Text = dt.Rows[0]["pincode"].ToString();
                    lblValidityFrom.Text = dt.Rows[0]["periodform"].ToString();
                    lblValidTo.Text = dt.Rows[0]["periodto"].ToString();

                    if (!Convert.IsDBNull(dt.Rows[0]["service_type_name"]))
                    {
                        lblServiceTypeName.Text = dt.Rows[0]["service_type_name"].ToString();
                    }
                    else
                    {
                        lblServiceTypeName.Text = "All Services";
                    }

                    if (dt.Rows[0]["doc_id_renew_yn"].ToString() == "N" && dt.Rows[0]["doc_add_renew_yn"].ToString() == "N")
                    {
                        pnlDocument.Visible = false;
                    }
                    else
                    {
                        pnlDocument.Visible = true;
                    }

                    if (dt.Rows[0]["doc_id_renew_yn"].ToString() == "Y")
                    {
                        Session["Renew_doc_ID_YN"] = "Y";
                        DOC_ID_RENEW_TYPES = dt.Rows[0]["doc_id_renew_types"].ToString();
                        pnlIDProofNew.Visible = true;

                        if (DOC_ID_RENEW_TYPES != null)
                        {
                            GetIDproof("1", DOC_ID_RENEW_TYPES);
                            string[] ss = DOC_ID_RENEW_TYPES.Split(',');

                            string idProof;
                            string a = "";

                            for (int i = 0; i < ss.Length; i++)
                            {
                                idProof = GetIDproof("1", DOC_ID_RENEW_TYPES).Rows[i]["document_type_name"].ToString();

                                if (!string.IsNullOrEmpty(a))
                                {
                                    a = a + "," + idProof;
                                }
                                else
                                {
                                    a = idProof;
                                }

                                lblIDDocuments.Text = "<br> Applicable Id Proof Document : " + a;
                                rbtnIdProof.DataSource = GetIDproof("1", DOC_ID_RENEW_TYPES);
                                rbtnIdProof.DataTextField = "document_type_name";
                                rbtnIdProof.DataValueField = "document_type_id";
                                rbtnIdProof.DataBind();
                            }
                        }
                    }
                    else
                    {
                        pnlIDProofNew.Visible = false;
                    }

                    if (dt.Rows[0]["doc_add_renew_yn"].ToString() == "Y")
                    {
                        Session["Renew_doc_Add_YN"] = "Y";
                        DOC_ADD_RENEW_TYPES = dt.Rows[0]["doc_add_renew_types"].ToString();
                        pnlAddProofNew.Visible = true;

                        if (DOC_ADD_RENEW_TYPES != null)
                        {
                            GetAddProof("2", DOC_ADD_RENEW_TYPES);
                            string[] ss = DOC_ADD_RENEW_TYPES.Split(',');

                            string addProof;
                            string a = "";

                            for (int i = 0; i < ss.Length; i++)
                            {
                                addProof = GetAddProof("2", DOC_ADD_RENEW_TYPES).Rows[i]["document_type_name"].ToString();

                                if (!string.IsNullOrEmpty(a))
                                {
                                    a = a + "," + addProof;
                                }
                                else
                                {
                                    a = addProof;
                                }

                                lbladdDocuments.Text = "<br> Applicable Address Proof Document : " + a;
                                rbtnAddressProof.DataSource = GetAddProof("2", DOC_ADD_RENEW_TYPES);
                                rbtnAddressProof.DataTextField = "document_type_name";
                                rbtnAddressProof.DataValueField = "document_type_id";
                                rbtnAddressProof.DataBind();
                            }
                        }
                    }
                    else
                    {
                        pnlAddProofNew.Visible = false;
                    }

                    PHOTO_RENEW_YN = dt.Rows[0]["photo_renew_yn"].ToString();
                    Session["Renew_Photo_YN"] = PHOTO_RENEW_YN;

                    if (PHOTO_RENEW_YN == "Y")
                    {
                        pnlPhoto.Visible = true;
                    }
                    else
                    {
                        pnlPhoto.Visible = false;
                    }

                    pnlPassDetail.Visible = true;
                }
                else
                {
                    pnlPassDetail.Visible = false;
                    Errormsg("Bus Pass Doesn't Exist");
                }
            }
            else
            {
                pnlPassDetail.Visible = false;
                Errormsg("Bus Pass Doesn't Exist");
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message);
        }
    }
    private bool ValidValue()
    {
        try
        {
            string msg = "";
            int msgcount = 0;

            if ((Session["CaptchaImage"] == null || tbcaptchacode.Text.ToLower() != Session["CaptchaImage"].ToString().ToLower()))
            {
                msgcount++;
                msg += msgcount + ". Invalid Security Code(Shown in Image). Please Try Again";
                return false;
            }
            else if (tbPassnNo.Text.Length <= 0)
            {
                msgcount++;
                msg += msgcount + ". Enter Valid Pass Number.";
            }
            if (tbDate.Text == "")
            {
                msgcount++;
                msg += msgcount + ". Enter Valid Date of birth";
            }
            

            if (msgcount > 0)
            {
                Errormsg(msg);
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message);
            return false;
        }
    }
    private bool CheckFileExtention(FileUpload FileMobileApp)
    {
        try
        {
            bool fileExtensionOK = false;
            string fileExtension = System.IO.Path.GetExtension(FileMobileApp.FileName).ToLower();
            string[] allowedExtensions = { ".jpg", ".png", ".jpeg" };

            for (int i = 0; i < allowedExtensions.Length; i++)
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
            return false;
        }
    }
    private bool Validaion()
    {
        try
        {
            if (Session["Renew_doc_ID_YN"] == "Y")
            {
                if (Session["IDProof"] == null || Session["IDProof"].ToString() == "")
                {
                    Errormsg("Please Select Id Proof Document");
                    return false;
                }
            }

            if (Session["Renew_doc_Add_YN"] == "Y")
            {
                if (Session["AddProof"] == null || Session["AddProof"].ToString() == "")
                {
                    Errormsg("Please Select Address Proof Document");
                    return false;
                }
            }

            if (Session["Renew_Photo_YN"] == "Y")
            {
                if (Session["Photo"] == null || Session["Photo"].ToString() == "")
                {
                    Errormsg("Please Select Valid Photograph");
                    return false;
                }
            }

            return true;
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message);
            return false;
        }
    }
    private void SaveRenewPassRequest()
    {
        byte[] Idproof = (byte[])Session["IDProof"];
        byte[] AddProof = (byte[])Session["AddProof"];
        byte[] Photo = (byte[])Session["Photo"];

        int idproofid = 0;
        if (!(Session["IDProof"] == null || Session["IDProof"].ToString() == ""))
        {
            idproofid = Convert.ToInt16(rbtnIdProof.SelectedValue);
        }

        int addproofid = 0;
        if (!(Session["AddProof"] == null || Session["AddProof"].ToString() == ""))
        {
            addproofid = Convert.ToInt16(rbtnAddressProof.SelectedValue);
        }

        string IPAddress = HttpContext.Current.Request.UserHostAddress;
       
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", " buspass.bus_pass_renew_request");
        MyCommand.Parameters.AddWithValue("@p_passno", tbPassnNo.Text);
        MyCommand.Parameters.AddWithValue("@p_idproof", (object)Idproof ?? DBNull.Value);
        MyCommand.Parameters.AddWithValue("@p_addproof", (object)AddProof ?? DBNull.Value);
        MyCommand.Parameters.AddWithValue("@p_idproofid", idproofid);
        MyCommand.Parameters.AddWithValue("@p_addproofid", addproofid);
        MyCommand.Parameters.AddWithValue("@p_photo", (object)Photo ?? DBNull.Value);
        MyCommand.Parameters.AddWithValue("@p_applytype", "T");
        MyCommand.Parameters.AddWithValue("@p_applytypeby", Session["Mobileno"].ToString());
        MyCommand.Parameters.AddWithValue("@p_ipaddress", IPAddress);
        dt = bll.SelectAll(MyCommand);
        if (dt.TableName == "Success")
        {
            if (dt.Rows.Count > 0)
            {

                Session["_RNDIDENTIFIERMSTAUTH"] = _validation.GeneratePassword(10, 5);
                //Session["_otp"] = getRandom();
                Session["_otp"] = "123456";
                Session["currtranrefno"] = dt.Rows[0]["sp_curr_tran_ref"];
                Response.Redirect("ConfirmBusPassRequest.aspx");
                RefreshCaptcha();
            }
        }
    }

    public byte[] convertByteFile(System.Web.UI.WebControls.FileUpload fuFileUpload)
    {
        int intFileLength;
        byte[] byteData = null;

        if (fuFileUpload.HasFile)
        {
            if (CheckFileExtension(fuFileUpload) == true)
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


    private bool CheckFileExtension(FileUpload fuFileUpload)
    {
        try
        {
            bool fileExtensionOK = false;
            string fileExtension = System.IO.Path.GetExtension(fuFileUpload.FileName).ToLower();
            string[] allowedExtensions = { ".jpg", ".png", ".jpeg" };

            for (int i = 0; i < allowedExtensions.Length; i++)
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
            Errormsg("Something went wrong with image (allow only .jpg, .png, .jpeg image).");
            return false; // or handle the exception as needed
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

    public byte[] ConvertByteFile(System.Web.UI.WebControls.FileUpload fuFileUpload)
    {
        int intFileLength;
        byte[] byteData = null;

        if (fuFileUpload.HasFile)
        {
            if (CheckFileExtention(fuFileUpload))
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
    public bool IsValidPdf(FileUpload fileupload)
    {
        string fileFormat = GetMimeDataOfFile(fileupload.PostedFile);
        if (fileFormat == "application/pdf")
        {
            return true;
        }
        else
        {
            Errormsg("Invalid file (Not a PDF)");
            return false;
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
    public byte[] ConvertByteFilePDF(FileUpload fuFileUpload)
    {
        int intFileLength;
        byte[] byteData = null;

        if (fuFileUpload.HasFile)
        {
            // Check File Extension
            if (CheckFileExtentionPDF(fuFileUpload, ".pdf"))
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
    public bool CheckFileExtentionPDF(FileUpload fuFileUpload, string allowedExtention)
    {
        bool fileExtensionOK = false;
        string fileExtension = System.IO.Path.GetExtension(fuFileUpload.FileName).ToLower();
        string[] allowedExtensions = { ".pdf", ".PDF" };

        for (int i = 0; i < allowedExtensions.Length; i++)
        {
            if (fileExtension == allowedExtensions[i])
            {
                fileExtensionOK = true;
            }
        }

        return fileExtensionOK;
    }
    #endregion

    #region "Event"
    protected void lbtnRefresh_Click(object sender, EventArgs e)
    {
        RefreshCaptcha();
    }
    protected void lbtnrest_Click(object sender, EventArgs e)
    {
        resetctrl();
        pnlPassDetail.Visible = false;
        pnlPassNorecord.Visible = true;
    }
    protected void lbtnSearch_Click(object sender, EventArgs e)
    {
        if (ValidValue() == false)
        {
            RefreshCaptcha();
            return;
        }

        LoadPassDetails();

    }
    protected void rbtnIdProof_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtnIdProof.SelectedValue != null)
        {
            pnlIdProof.Visible = true;
        }
        else
        {
            pnlIdProof.Visible = false;
        }
    }
    protected void rbtnAddressProof_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtnAddressProof.SelectedValue != null)
        {
            pnlAddProof.Visible = true;
        }
        else
        {
            pnlAddProof.Visible = false;
        }
    }
   

 


    protected void lbtnSave_Click(object sender, EventArgs e)
    {
        if (Validaion() == false)
        {
            return;
        }

        RefreshCaptcha();
        Session["Action"] = "S";
        ConfirmMsg("Do You Want To Proceed For Renewing Bus Pass request ?");

    }

    #endregion

    protected void lbtncloseWebImage_Click(object sender, EventArgs e)
    {
        Session["Photo"] = null;
        ImgWebPortal.Visible = false;
        lbtncloseWebImage.Visible = false;
    }



    protected void btnUploadDoc_Click(object sender, EventArgs e)
    {
        if (fileUpload.HasFile)
        {
            Session["IDProof"] = null;
            string _fileFormat = GetMimeDataOfFile(fileUpload.PostedFile);
            string _NewFileName = "";

            if (fileUpload.FileName.Length <= 50)
            {
                Session["IDProof"] = fileUpload.FileBytes;
                lblIDpdf.Text = fileUpload.FileName;
                Session["pdfName"] = fileUpload.FileName;
                lblIDpdf.Visible = true;
            }
            else
            {
                Errormsg("File Name Should be less than 50 Char.");
                return;
            }

            if (_fileFormat == "application/pdf")
            {
                _NewFileName += ".pdf";
            }
            else
            {
                Errormsg("Please select Pdf file only.");
                return;
            }

            int cnt = fileUpload.PostedFile.FileName.Split('.').Length - 1;

            if (cnt > 1)
            {
                Errormsg("Please select Pdf file only.");
                return;
            }

            int length = fileUpload.PostedFile.ContentLength;

            if (length == 0)
            {
                Errormsg("Document has not been uploaded.");
                return;
            }

            if (length < 1024)
            {
                Errormsg("File size should be less than 1MB.");
                return;
            }

            Session["IDProof"] = fileUpload.FileBytes;
        }
    }

    protected void btnAddproof_Click(object sender, EventArgs e)
    {
        if (fileaddproof.HasFile)
        {
            Session["AddProof"] = null;
            string _fileFormat = GetMimeDataOfFile(fileaddproof.PostedFile);
            string _NewFileName = "";

            if (fileaddproof.FileName.Length <= 50)
            {
                Session["AddProof"] = fileaddproof.FileBytes;
                lbladd.Text = fileaddproof.FileName;
                Session["pdfName"] = fileaddproof.FileName;
                lbladd.Visible = true;
            }
            else
            {
                Errormsg("File Name Should be less than 50 Char.");
                return;
            }

            if (_fileFormat == "application/pdf")
            {
                _NewFileName += ".pdf";
            }
            else
            {
                Errormsg("Please select Pdf file only.");
                return;
            }

            int cnt = fileaddproof.PostedFile.FileName.Split('.').Length - 1;

            if (cnt > 1)
            {
                Errormsg("Please select Pdf file only.");
                return;
            }

            int length = fileaddproof.PostedFile.ContentLength;

            if (length == 0)
            {
                Errormsg("Document has not been uploaded.");
                return;
            }

            if (length < 1024)
            {
                Errormsg("File size should be less than 1MB.");
                return;
            }

            Session["AddProof"] = fileaddproof.FileBytes;
        }
    }

    protected void btnUploadWebPortal_Click(object sender, EventArgs e)
    {
        if (!FileWebPortal.HasFile)
        {
            Errormsg("Please select Web Image first");
            return;
        }

        string _fileFormat = GetMimeDataOfFile(FileWebPortal.PostedFile);

        if (_fileFormat == "image/png" || _fileFormat != "image/jpg" || _fileFormat != "image/jpeg")
        {
            if (!CheckFileExtension(FileWebPortal))
            {
                Errormsg("File must be of type PNG/jpg/jpeg");
                return;
            }

            decimal size = Math.Round((Convert.ToDecimal(FileWebPortal.PostedFile.ContentLength) / Convert.ToDecimal(1024)), 2);

            if (size > 1024 || size < 10)
            {
                Errormsg("File size must be between 10 kb to 1 Mb");
                return;
            }

            byte[] PhotoImage = convertByteFile(FileWebPortal);
            ImgWebPortal.ImageUrl = GetImage(PhotoImage);
            ImgWebPortal.Visible = true;
            lbtncloseWebImage.Visible = true;
            Session["Photo"] = FileWebPortal.FileBytes;
            Session["webcount"] = "W";
        }

        else
        {
            Errormsg("File must be of type PNG");
            return;
        }

    }

    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        SaveRenewPassRequest();
    }
}