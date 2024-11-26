using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Runtime.InteropServices;

public partial class Auth_PAdminRTI : BasePage
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    private sbBLL bll = new sbBLL();
    private string msg = "";
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    private byte[] PhotoImage = null;
    [DllImport("urlmon.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = false)]
    static extern int FindMimeFromData(IntPtr pBC,
    [MarshalAs(UnmanagedType.LPWStr)] string pwzUrl,
    [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1, SizeParamIndex = 3)] byte[] pBuffer,
    int cbSize,
    [MarshalAs(UnmanagedType.LPWStr)] string pwzMimeProposed,
    int dwMimeFlags, out IntPtr ppwzMimeOut, int dwReserved);
    private byte[] documentFilePIOS = null;
    private byte[] documentFileManual1 = null;
    private byte[] documentFileManual2 = null;
    private byte[] documentFileManual3 = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        Session["_moduleName"] = "RTI/PIOS Manuals";
        if (!IsPostBack)
        {
            Random randomclass = new Random();
            Session["rndNoCheck"] = randomclass.Next().ToString();
            hidtoken.Value = Session["rndNoCheck"].ToString();

            Session["PIOSCount"] = 0;
            Session["Manual1Count"] = 0;
            Session["Manual2Count"] = 0;
            Session["Manual3Count"] = 0;
            Session["PIOSfile"] = null;
            Session["Manual1file"] = null;
            loadRTIData();
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
    private void loadRTIData()//M1
    {
        try
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("../CommonData.xml"));
            XmlNodeList pios = doc.GetElementsByTagName("pios");
            if (pios.Item(0).InnerXml != "")
            {
                lblPIOS.Text = pios.Item(0).InnerXml;
                lbtndownlaodPIOS.Visible = true;
                lbtndeletePIOS.Visible = true;
                lblPIOS.Visible = true;
                string path = Server.MapPath("../manuals/" + pios.Item(0).InnerXml);

                // Calling the ReadAllBytes() function
                byte[] PIOSfile = File.ReadAllBytes(path);
                Session["PIOSfile"] = PIOSfile;
            }
            else
            {
                lbtndownlaodPIOS.Visible = false;
                lbtndeletePIOS.Visible = false;
                lblPIOS.Visible = false;
            }
            XmlNodeList rti_manual1 = doc.GetElementsByTagName("rti_manual1");
            if (rti_manual1.Item(0).InnerXml != "")
            {
                lblManual1.Text = rti_manual1.Item(0).InnerXml;
                lbtndownlaodManual1.Visible = true;
                lbtndeleteManual1.Visible = true;
                lblManual1.Visible = true;
                string path = Server.MapPath("../manuals/" + rti_manual1.Item(0).InnerXml);

                // Calling the ReadAllBytes() function
                byte[] Manual1file = File.ReadAllBytes(path);
                Session["Manual1file"] = Manual1file;
            }
            else
            {
                lbtndownlaodManual1.Visible = false;
                lbtndeleteManual1.Visible = false;
                lblManual1.Visible = false;
            }
            XmlNodeList rti_manual2 = doc.GetElementsByTagName("rti_manual2");
            if (rti_manual2.Item(0).InnerXml != "")
            {
                lblManual2.Text = rti_manual2.Item(0).InnerXml;
                lbtndownlaodManual2.Visible = true;
                lbtndeleteManual2.Visible = true;
                lblManual2.Visible = true;
                string path = Server.MapPath("../manuals/" + rti_manual2.Item(0).InnerXml);

                // Calling the ReadAllBytes() function
                byte[] Manual2file = File.ReadAllBytes(path);
                Session["Manual2file"] = Manual2file;
            }
            else
            {
                lbtndownlaodManual2.Visible = false;
                lbtndeleteManual2.Visible = false;
                lblManual2.Visible = false;
            }
            XmlNodeList rti_manual3 = doc.GetElementsByTagName("rti_manual3");
            if (rti_manual3.Item(0).InnerXml != "")
            {
                lblManual3.Text = rti_manual3.Item(0).InnerXml;
                lbtndownlaodManual3.Visible = true;
                lbtndeleteManual3.Visible = true;
                lblManual3.Visible = true;
                string path = Server.MapPath("../manuals/" + rti_manual3.Item(0).InnerXml);

                // Calling the ReadAllBytes() function
                byte[] Manual3file = File.ReadAllBytes(path);
                Session["Manual3file"] = Manual3file;
            }
            else
            {
                lbtndownlaodManual3.Visible = false;
                lbtndeleteManual3.Visible = false;
                lblManual3.Visible = false;
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminRTI.aspx-0001", ex.Message.ToString());
        }
    }
    public bool IsValidPdf(FileUpload fileupload)//M2
    {
        try
        {
            string _fileFormat = GetMimeDataOfFile(fileupload);
            if (_fileFormat != "application/pdf")
            {
                Errormsg("Invalid file (Not a PDF)");
                return false;
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminRTI.aspx-0002", ex.Message.ToString());
            return false;
        }
        return true;
    }
    public static string GetMimeDataOfFile(FileUpload files)
    {
        HttpPostedFile file = files.PostedFile;
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
    public byte[] convertByteFilePDF(System.Web.UI.WebControls.FileUpload fuFileUpload)
    {
        int intFileLength;
        byte[] byteData = null;
        if (fuFileUpload.HasFile)
        {
            // Check File Extention
            if (checkFileExtentionPDF(fuFileUpload, ".pdf") == true)
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
    public bool checkFileExtentionPDF(System.Web.UI.WebControls.FileUpload fuFileUpload, string allowedExtention)
    {
        bool fileExtensionOK = false;
        string fileExtension;
        fileExtension = System.IO.Path.GetExtension(fuFileUpload.FileName).ToLower();
        string[] allowedExtensions = new[] { ".pdf", ".PDF" };
        for (int i = 0; i <= allowedExtensions.Length - 1; i++)
        {
            if (fileExtension == allowedExtensions[i])
                fileExtensionOK = true;
        }
        return fileExtensionOK;
    }
    private void Warningmsg(string msg)
    {
        string popup = _popup.modalPopupSmall("W", "Please Note", msg, "Close");
        Response.Write(popup);
    }
    private bool Validvalue()//M3
    {
        try
        {
            string msg = "";
            int msgcont = 0;

            if (Session["PIOSfile"] == null)
            {
                msgcont = msgcont + 1;
                msg = msg + msgcont.ToString() + ". Upload  PIOS .pdf File." + "<br />";

            }
            if (Session["Manual1file"] == null)
            {
                msgcont = msgcont + 1;
                msg = msg + msgcont.ToString() + ". Upload Valid Manual1 .pdf File" + "<br />";

            }
            if (msgcont > 0)
            {
                Errormsg(msg);
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminRTI.aspx-0004", ex.Message.ToString());
            Errormsg(ex.Message);
            return false;
        }
    }
    private void insertRTIPIOSFIle()//M4
    {
        try
        {
            string ipaddress = HttpContext.Current.Request.UserHostAddress;
            string aRtiPios = "";
            string aRtiManual1 = "";
            string aRtiManual2 = "";
            string aRtiManual3 = "";

            if (Session["PIOSCount"].ToString() == "PC")
            {
                byte[] FilePIOS = (byte[])Session["PIOSfile"];
                System.IO.File.WriteAllBytes(Server.MapPath("../manuals/PIO.pdf"), FilePIOS);
                aRtiPios = "~/manuals/PIOS.pdf";
            }
            if (Session["Manual1Count"].ToString() == "MC1")
            {
                byte[] Manual1file = (byte[])Session["Manual1file"];
                System.IO.File.WriteAllBytes(Server.MapPath("../manuals/Manual_1.pdf"), Manual1file);
                aRtiManual1 = "~/manuals/Manual_1.pdf";
            }
            if (Session["Manual2Count"].ToString() == "MC2")
            {
                byte[] Manual2file = (byte[])Session["Manual2file"];
                System.IO.File.WriteAllBytes(Server.MapPath("../manuals/Manual_2.pdf"), Manual2file);
                aRtiManual2 = "~/manuals/Manual_2.pdf";
            }
            if (Session["Manual3Count"].ToString() == "MC3")
            {
                byte[] Manual3file = (byte[])Session["Manual3file"];
                System.IO.File.WriteAllBytes(Server.MapPath("../manuals/Manual_3.pdf"), Manual3file);
                aRtiManual3 = "~/manuals/Manual_3.pdf";
            }
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            string Mresult = "";
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_rtipios_insertupdate");
            MyCommand.Parameters.AddWithValue("p_piosapios", aRtiPios);
            MyCommand.Parameters.AddWithValue("p_rtimanual1", aRtiManual1);
            MyCommand.Parameters.AddWithValue("p_rtimanual2", aRtiManual2);
            MyCommand.Parameters.AddWithValue("p_rtimanual3", aRtiManual3);
            MyCommand.Parameters.AddWithValue("p_updatedby", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("p_ipaddress", ipaddress);
            Mresult = bll.UpdateAll(MyCommand);
            if (Mresult == "Success")
            {

                updateXmlRTIData();
                loadRTIData();
                successMessage("Updated Successfully !");
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminRTI.aspx-0005", ex.Message.ToString());
        }
    }
    private void DeletePIOS()//M5
    {
        string path = "";
        try
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("../CommonData.xml"));
            XmlNodeList pios_xml = doc.GetElementsByTagName("pios");
            pios_xml.Item(0).InnerXml = "";
            doc.Save(Server.MapPath("../CommonData.xml"));

            path = Server.MapPath("../manuals/PIO.pdf");
            FileInfo file = new FileInfo(path);
            if (file.Exists)
            {
                file.Delete();
                successMessage("PIOS file deleted successfully");
            }
            else
            {
                Errormsg("PIOS file does not exists.");
            }
            loadRTIData();
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message);
            _common.ErrorLog("PAdminRTI.aspx-0006", ex.Message.ToString());
        }
    }
    private void DeleteRTI1()//M6
    {
        try
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("../CommonData.xml"));
            XmlNodeList mobno_xml = doc.GetElementsByTagName("rti_manual1");
            mobno_xml.Item(0).InnerXml = "";
            doc.Save(Server.MapPath("../CommonData.xml"));

            string path = Server.MapPath("../manuals/Manual_1.pdf");
            FileInfo file = new FileInfo(path);
            if (file.Exists)
            {
                file.Delete();
                successMessage("RTI Manual 1 file deleted successfully");

            }
            else
            {
                Errormsg("RTI Manual 1 file does not exists.");
            }
            loadRTIData();
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminRTI.aspx-0007", ex.Message.ToString());
            Errormsg(ex.Message);
        }
    }
    private void DeleteRTI2()//M7
    {
        try
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("../CommonData.xml"));
            XmlNodeList mobno_xml = doc.GetElementsByTagName("rti_manual2");
            mobno_xml.Item(0).InnerXml = "";
            doc.Save(Server.MapPath("../CommonData.xml"));
            string path = Server.MapPath("../manuals/Manual_2.pdf");
            FileInfo file = new FileInfo(path);
            if (file.Exists)
            {
                file.Delete();
                successMessage("RTI Manual 2 file deleted successfully");
            }
            else
            {
                Errormsg("RTI Manual 2 file does not exists.");
            }
            loadRTIData();
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminRTI.aspx-0008", ex.Message.ToString());
            Errormsg(ex.Message);
        }
    }
    private void DeleteRTI3()//M8
    {
        try
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("../CommonData.xml"));
            XmlNodeList mobno_xml = doc.GetElementsByTagName("rti_manual3");
            mobno_xml.Item(0).InnerXml = "";
            doc.Save(Server.MapPath("../CommonData.xml"));
            string path = Server.MapPath("../manuals/Manual_3.pdf");
            FileInfo file = new FileInfo(path);
            if (file.Exists)
            {
                file.Delete();
                successMessage("RTI Manual 3 file deleted successfully");
            }
            else
            {
                Errormsg("RTI Manual 2 file does not exists.");
            }
            loadRTIData();
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminRTI.aspx-0009", ex.Message.ToString());
            Errormsg(ex.Message);
        }
    }
    private void updateXmlRTIData()//M5
    {
        try
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("../CommonData.xml"));
            if (Session["PIOSCount"].ToString() == "PC")
            {
                XmlNodeList pios = doc.GetElementsByTagName("pios");
                pios.Item(0).InnerXml = "PIO.pdf";
            }
            if (Session["Manual1Count"].ToString() == "MC1")
            {
                XmlNodeList mobno_xml = doc.GetElementsByTagName("rti_manual1");
                mobno_xml.Item(0).InnerXml = "Manual_1.pdf";
            }
            if (Session["Manual2Count"].ToString() == "MC2")
            {
                XmlNodeList rti_manual2 = doc.GetElementsByTagName("rti_manual2");
                rti_manual2.Item(0).InnerXml = "Manual_2.pdf";
            }
            if (Session["Manual3Count"].ToString() == "MC3")
            {
                XmlNodeList rti_manual3 = doc.GetElementsByTagName("rti_manual3");
                rti_manual3.Item(0).InnerXml = "Manual_3.pdf";
            }
            doc.Save(Server.MapPath("../CommonData.xml"));
            Session["PIOSCount"] = 0;
            Session["Manual1Count"] = 0;
            Session["Manual2Count"] = 0;
            Session["Manual3Count"] = 0;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminRTI.aspx-0010", ex.Message.ToString());
        }
    }
    private void Errormsg(string msg)
    {
        string popup = _popup.modalPopupSmall("D", "Please Check", msg, "Close");
        Response.Write(popup);
    }
    public void successMessage(string msg)
    {
        lblsuccessmsg.Text = msg;
        mpsuccess.Show();
    }// E8
    private void InfoMsg(string msg)
    {
        string popup = _popup.modalPopupLarge("I", "Information", msg, "Close");
        Response.Write(popup);
    }
    public void confirmmsg(string message)
    {
        lblConfirmation.Text = message;
        mpConfirmation.Show();
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
    protected void btnFilePIOS_Click(object sender, EventArgs e)//E1
    {
        CsrfTokenValidate();
        try
        {
            Byte[] bytes = FilePIOS.FileBytes;
            if (FileContainsMaliciousCode(bytes) == true)
            {
                Errormsg("Invalid File. Please upload a different file.");
                return;
            }
            string _fileFormat = GetMimeDataOfFile(FilePIOS);
            int length = FilePIOS.PostedFile.ContentLength / 1024;

            if (length == 0)
            {
                Errormsg("Document has not been uploaded.");
                return;
            }
            if (length > 5120)
            {
                Errormsg("File size should be less than 1MB.");
                return;
            }
            if (FilePIOS.FileName.Length <= 50)
            {

                documentFilePIOS = convertByteFilePDF(FilePIOS);

                var _NewFileName = Session["_PIOSdocfileName"];
                if (_fileFormat == "application/pdf")
                { _NewFileName += ".pdf"; }
                else if (_fileFormat == "application/octet-stream")
                { _NewFileName += ".pdf"; }
                else
                {
                    Errormsg("PIOS File format not allowed.");
                    return;
                }

                Session["PIOSfile"] = documentFilePIOS;
                Session["FilePIOS"] = FilePIOS.PostedFile;
                lblPIOS.Visible = true;
                lblPIOS.Text = FilePIOS.FileName;
                Session["PIOSCount"] = "PC";

            }
            else
            {
                Errormsg("File Name To Large.");
                return;
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminRTI-E1", ex.Message.ToString());
        }

    }
    protected void btnFileManual1_Click(object sender, EventArgs e)//E2
    {
        CsrfTokenValidate();
        try
        {

            if (FileManual1.HasFile)
            {
                Byte[] bytes = FileManual1.FileBytes;
                if (FileContainsMaliciousCode(bytes) == true)
                {
                    Errormsg("Invalid File. Please upload a different file.");
                    return;
                }
                string _fileFormat = GetMimeDataOfFile(FileManual1);

                int length = FileManual1.PostedFile.ContentLength / 1024;

                if (length == 0)
                {
                    Errormsg("Document has not been uploaded.");
                    return;
                }
                if (length > 5120)
                {
                    Errormsg("File size should be less than 1MB.");
                    return;
                }


                if (FileManual1.FileName.Length <= 50)
                {

                    documentFileManual1 = convertByteFilePDF(FileManual1);

                    var _NewFileName = Session["_Manual1docfileName"];
                    if (_fileFormat == "application/pdf")
                    { _NewFileName += ".pdf"; }
                    else if (_fileFormat == "application/octet-stream")
                    { _NewFileName += ".pdf"; }
                    else
                    {
                        Errormsg("Manual1 File format not allowed.");
                        return;
                    }
                    Session["Manual1file"] = documentFileManual1;
                    Session["FileManual1"] = FileManual1.PostedFile;
                    lblManual1.Visible = true;
                    lblManual1.Text = FileManual1.FileName;
                    Session["Manual1Count"] = "MC1";
                }
                else
                {
                    Errormsg("File Name Too Large");
                    return;
                }
            }

        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminRTI-E2", ex.Message.ToString());
        }
    }
    protected void btnFileManual2_Click(object sender, EventArgs e)//E3
    {
        CsrfTokenValidate();
        try
        {

            if (IsValidPdf(FileManual2) == true)
            {
                if (FileManual2.HasFile)
                {
                    Byte[] bytes = FileManual2.FileBytes;
                    if (FileContainsMaliciousCode(bytes) == true)
                    {
                        Errormsg("Invalid File. Please upload a different file.");
                        return;
                    }

                    string _fileFormat = GetMimeDataOfFile(FileManual2);

                    int length = FileManual2.PostedFile.ContentLength / 1024;

                    if (length == 0)
                    {
                        Errormsg("Document has not been uploaded.");
                        return;
                    }
                    if (length > 5120)
                    {
                        Errormsg("File size should be less than 1MB.");
                        return;
                    }


                    if (FileManual2.FileName.Length <= 50)
                    {

                        documentFileManual2 = convertByteFilePDF(FileManual2);
                        var _NewFileName = Session["_Manual2docfileName"];
                        if (_fileFormat == "application/pdf")
                        { _NewFileName += ".pdf"; }
                        else if (_fileFormat == "application/octet-stream")
                        { _NewFileName += ".pdf"; }
                        else
                        {
                            Errormsg("Manual2 File format not allowed.");
                            return;
                        }
                        Session["Manual2file"] = documentFileManual2;
                        Session["FileManual2"] = FileManual2.PostedFile;
                        lblManual2.Visible = true;
                        lblManual2.Text = FileManual2.FileName;

                        Session["Manual2Count"] = "MC2";
                    }
                    else
                    {
                        Errormsg("File Name Too Large");
                        return;
                    }
                }
            }
            else
            {
                Errormsg("Invalid Manual2 file (Either not a pdf file or file size is more than 1 MB");
                return;
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminRTI-E3", ex.Message.ToString());
        }
    }
    protected void btnFileManual3_Click(object sender, EventArgs e)//E4
    {
        CsrfTokenValidate();
        try
        {

            if (IsValidPdf(FileManual3) == true)
            {

                if (FileManual3.HasFile)
                {
                    Byte[] bytes = FileManual3.FileBytes;
                    if (FileContainsMaliciousCode(bytes) == true)
                    {
                        Errormsg("Invalid File. Please upload a different file.");
                        return;
                    }

                    string _fileFormat = GetMimeDataOfFile(FileManual3);

                    int length = FileManual3.PostedFile.ContentLength / 1024;

                    if (length == 0)
                    {
                        Errormsg("Document has not been uploaded.");
                        return;
                    }
                    if (length > 5120)
                    {
                        Errormsg("File size should be less than 1MB.");
                        return;
                    }

                    if (FileManual3.FileName.Length <= 50)
                    {
                        
                            documentFileManual3 = convertByteFilePDF(FileManual3);
                            var _NewFileName = Session["_Manual3docfileName"];
                            if (_fileFormat == "application/pdf")
                                _NewFileName += ".pdf";
                            else if (_fileFormat == "application/octet-stream")
                                _NewFileName += ".pdf";
                            else
                            {
                                Errormsg("File format not allowed.");
                                return;
                            }
                            Session["Manual3file"] = documentFileManual3;
                            Session["FileManual3"] = FileManual3.PostedFile;
                            lblManual3.Visible = true;
                            lblManual3.Text = FileManual3.FileName;
                            Session["Manual3Count"] = "MC3";
                    }
                    else
                    {
                        Errormsg("File Name Too Large");
                        return;
                    }
                }
            }
            else
            {
                Errormsg("Invalid file (Either not a pdf file or file size is more than 1 MB");
                return;
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminRTI-E4", ex.Message.ToString());
        }
    }
    protected void lbtnSaveRTIData_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (Validvalue() == false)
        {
            return;
        }

        Session["Action"] = "S";
        lblConfirmation.Text = "Do you want to update RTI/PIOS Files?";
        mpConfirmation.Show();
    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)//E5
    {
        CsrfTokenValidate();
        if (Session["Action"].ToString() == "S")
        {
            insertRTIPIOSFIle();
        }
        if (Session["Action"].ToString() == "D1")
        {
            DeletePIOS();
        }
        if (Session["Action"].ToString() == "D2")
        {
            DeleteRTI1();
        }
        if (Session["Action"].ToString() == "D3")
        {
            DeleteRTI2();
        }
        if (Session["Action"].ToString() == "D4")
        {
            DeleteRTI3();
        }
    }
    protected void lbtndeletePIOS_Click(object sender, EventArgs e)//E6
    {
        CsrfTokenValidate();
        Session["Action"] = "D1";
        confirmmsg("Do you want to Delete PIOS File ?");

    }
    protected void lbtndeleteManual1_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Session["Action"] = "D2";
        lblConfirmation.Text = "Do you want to Delete RTI Manual 1 File ?";
        mpConfirmation.Show();
    }
    protected void lbtndeleteManual2_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Session["Action"] = "D3";
        confirmmsg("Do you want to Delete RTI Manual 2 File ? ");

    }
    protected void lbtndeleteManual3_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Session["Action"] = "D4";
        confirmmsg("Do you want to Delete RTI Manual 3 File ?");

    }
    protected void lbtndownlaodPIOS_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Response.Clear();
        Response.ContentType = "application/pdf";
        Response.AppendHeader("Content-Disposition", "attachment; filename=PIO.pdf");
        Response.TransmitFile("../manuals/PIO.pdf");
        Response.End();
    }
    protected void lbtndownlaodManual1_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Response.Clear();
        Response.ContentType = "application/pdf";
        Response.AppendHeader("Content-Disposition", "attachment; filename=Manual_1.pdf");
        Response.TransmitFile("../manuals/Manual_1.pdf");
        Response.End();
    }
    protected void lbtndownlaodManual2_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Response.Clear();
        Response.ContentType = "application/pdf";
        Response.AppendHeader("Content-Disposition", "attachment; filename=Manual_2.pdf");
        Response.TransmitFile("../manuals/Manual_2.pdf");
        Response.End();
    }
    protected void lbtndownlaodManual3_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Response.Clear();
        Response.ContentType = "application/pdf";
        Response.AppendHeader("Content-Disposition", "attachment; filename=Manual_3.pdf");
        Response.TransmitFile("../manuals/Manual_3.pdf");
        Response.End();
    }
    protected void lbtnHelp_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        string msg = "";
        msg = msg + "1. RTI/PIOS manual will be visible on RTI section of the sites.<br/>";
        InfoMsg(msg);

    }
    protected void lbtnRTIPIOSViewHistory_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Errormsg("<h2>Comming Soon </h2><h5></h5>");

    }
    #endregion





}
