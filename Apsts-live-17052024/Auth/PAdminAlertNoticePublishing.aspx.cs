using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_PAdminAlertNoticePublishing : BasePage
{


    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    [DllImport("urlmon.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = false)]
    static extern int FindMimeFromData(IntPtr pBC,
      [MarshalAs(UnmanagedType.LPWStr)] string pwzUrl,
      [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1, SizeParamIndex = 3)] byte[] pBuffer,
      int cbSize,
      [MarshalAs(UnmanagedType.LPWStr)] string pwzMimeProposed,
      int dwMimeFlags, out IntPtr ppwzMimeOut, int dwReserved);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Random randomclass = new Random();
            Session["rndNoCheck"] = randomclass.Next().ToString();
            hidtoken.Value = Session["rndNoCheck"].ToString();
            lblSummary.Text = "Summary as on Date " + DateTime.Now.ToString("dd/MM/yyyy hh:ss tt");
            Session["_moduleName"] = "Notice/News Publishing";
            LoadNoticeNews();
            getNoticeNewsDraftDetails();
            getNoticeNewsDetails();
            getNoticeNewsCount();
            Session["img1"] = null;
            Session["img2"] = null;
            Session["pdf"] = null;

        }
    }

    #region Methods
    private void CsrfTokenValidate()
    {
        if (hidtoken.Value != Session["rndNoCheck"].ToString())
        {
            Response.Redirect("errorpage.aspx");
            return;
        }
    }
    public void LoadNoticeNews()//M1
    {
        try
        {
            ddlTypes.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_notice_news");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlTypes.DataSource = dt;
                    ddlTypes.DataTextField = "noticecategory_name";
                    ddlTypes.DataValueField = "notice_category";
                    ddlTypes.DataBind();

                    ddlAlertNotice.DataSource = dt;
                    ddlAlertNotice.DataTextField = "noticecategory_name";
                    ddlAlertNotice.DataValueField = "notice_category";
                    ddlAlertNotice.DataBind();
                }
            }
            else
            {
                _common.ErrorLog("PAdminAlertNoticePublishing.aspx-0001", dt.TableName);
                Errormsg(dt.TableName);
            }
            ddlTypes.Items.Insert(0, "SELECT");
            ddlTypes.Items[0].Value = "0";
            ddlTypes.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminAlertNoticePublishing.aspx-0002", ex.Message.ToString());
            Errormsg(ex.Message);
        }
    }
    private void ResetValues()
    {
        ddlTypes.SelectedValue = "0";
        tbEnglishSub.Text = "";
        tbLocalLangSub.Text = "";
        tbDescriEng.Text = "";
        tbDescrLocal.Text = "";
        tbUrl.Text = "";
        tbValidityFromDate.Text = "";
        tbValidityTo.Text = "";
        lbtnSaveDraft.Visible = true;
        lbtnCancel.Visible = false;
        lbtnReset.Visible = true;
        lbtnDocument.Visible = false;
        lbtnSavePublish.Visible = true;
        lbtnUpdate.Visible = false;
        lblAlertNoticeHead.Visible = true;
        lblAlertNoticeUpdate.Visible = false;
        lblPDF.Visible = false;
        Img1.Visible = false;
        Img2.Visible = false;
        lbtncloseImage1.Visible = false;
        lbtncloseImage2.Visible = false;
        Session["img1"] = null;
        Session["img2"] = null;
        Session["pdf"] = null;
        lblPDF.Text = "";
        divImage.Visible = true;

    }
    private bool ValidValue()//M2
    {
        try
        {
            int msgcnt = 0;
            string msg = "";

            if (ddlTypes.SelectedValue == "0")
            {
                msgcnt = msgcnt + 1;
                msg = msg + msgcnt.ToString() + ". Select Types.<br>";
            }
            if (ddlTypes.SelectedValue == "1")
            {
                if (tbEnglishSub.Text == "")
                {
                    msgcnt = msgcnt + 1;
                    msg = msg + msgcnt.ToString() + ". Enter Subject English Language <br>";
                }
                //if (tbLocalLangSub.Text == "")
                //{
                //    msgcnt = msgcnt + 1;
                //    msg = msg + msgcnt.ToString() + ". Enter Subject Local Language <br>";
                //}
                if (tbDescriEng.Text == "")
                {
                    msgcnt = msgcnt + 1;
                    msg = msg + msgcnt.ToString() + ". Enter Description English Language <br>";
                }
                //if (tbDescrLocal.Text == "")
                //{
                //    msgcnt = msgcnt + 1;
                //    msg = msg + msgcnt.ToString() + ". Enter Description Local Language <br>";
                //}
                if (Session["img1"] == null)
                {
                    msgcnt = msgcnt + 1;
                    msg = msg + msgcnt.ToString() + ". Please Choose Image1.<br>";
                }
                if (Session["img2"] == null)
                {
                    msgcnt = msgcnt + 1;
                    msg = msg + msgcnt.ToString() + ". Please Choose Image2.<br>";
                }
                if (Session["pdf"] != null)
                {
                    if (Session["pdf"] == null)
                    {
                        msgcnt = msgcnt + 1;
                        msg = msg + msgcnt.ToString() + ". Please Choose Document File.<br>";
                    }
                }
                if (tbUrl.Text != "")
                {
                    if (_validation.isValidURL(tbUrl.Text) == false)
                    {
                        msgcnt = msgcnt + 1;
                        msg = msg + msgcnt + ". Enter Valid URL. <br/>";
                    }
                }
                if (tbValidityFromDate.Text.Length > 0)
                {
                    if (_validation.IsDate(tbValidityFromDate.Text) == false)
                    {
                        msgcnt = msgcnt + 1;
                        msg = msg + msgcnt.ToString() + ". Invalid Leave Start Date<br/>";
                    }
                    else
                    {
                        DateTime fromDate = DateTime.ParseExact(tbValidityFromDate.Text, "dd/MM/yyyy", null/* TODO Change to default(_) if this is not a reference type */);
                        DateTime currDate = DateTime.Now.Date;
                        if (currDate > fromDate)
                        {
                            msgcnt = msgcnt + 1;
                            msg = msg + msgcnt.ToString() + ". Check Validity From Date. From Date should be greater than Today's Date.<br/>";
                        }
                    }
                }
                else
                {
                    msgcnt = msgcnt + 1;
                    msg = msg + msgcnt.ToString() + ". Validity From Date.<br/>";
                }
                if (tbValidityTo.Text.Length > 0)
                {
                    if (_validation.IsDate(tbValidityTo.Text) == false)
                    {
                        msgcnt = msgcnt + 1;
                        msg = msg + msgcnt.ToString() + ". Invalid Validity To Date.<br/>";
                    }
                    else
                    {
                        DateTime fromDate = DateTime.ParseExact(tbValidityFromDate.Text, "dd/MM/yyyy", null);
                        DateTime ToDate = DateTime.ParseExact(tbValidityTo.Text, "dd/MM/yyyy", null);
                        if (ToDate < fromDate)
                        {
                            msgcnt = msgcnt + 1;
                            msg = msg + msgcnt.ToString() + ". Check To Date. Validity To Date should be Greater than Validity From Date.<br/>";
                        }
                    }
                }
                else
                {
                    msgcnt = msgcnt + 1;
                    msg = msg + msgcnt.ToString() + ". Validity To Date.<br/>";
                }
            }
            else if (ddlTypes.SelectedValue == "2")
            {

                if (tbEnglishSub.Text == "")
                {
                    msgcnt = msgcnt + 1;
                    msg = msg + msgcnt.ToString() + ". Enter Subject English Language <br>";
                }
                //if (tbLocalLangSub.Text == "")
                //{
                //    msgcnt = msgcnt + 1;
                //    msg = msg + msgcnt.ToString() + ". Enter Subject Local Language <br>";
                //}
                if (Session["img1"] == null)
                {
                    msgcnt = msgcnt + 1;
                    msg = msg + msgcnt.ToString() + ". Please Choose Image1.<br>";
                }
                if (Session["img2"] == null)
                {
                    msgcnt = msgcnt + 1;
                    msg = msg + msgcnt.ToString() + ". Please Choose  Image2.<br>";
                }
                if (tbUrl.Text != "")
                {
                    if (_validation.isValidURL(tbUrl.Text) == false)
                    {
                        msgcnt = msgcnt + 1;
                        msg = msg + msgcnt + ". Enter Valid URL. <br/>";
                    }
                }
                if (tbValidityFromDate.Text.Length > 0)
                {
                    if (_validation.IsDate(tbValidityFromDate.Text) == false)
                    {
                        msgcnt = msgcnt + 1;
                        msg = msg + msgcnt.ToString() + ". Invalid Leave Start Date<br/>";
                    }
                    else
                    {
                        DateTime fromDate = DateTime.ParseExact(tbValidityFromDate.Text, "dd/MM/yyyy", null/* TODO Change to default(_) if this is not a reference type */);
                        DateTime currDate = DateTime.Now.Date;
                        if (currDate > fromDate)
                        {
                            msgcnt = msgcnt + 1;
                            msg = msg + msgcnt.ToString() + ". Check Validity From Date. From Date should be greater than Today's Date.<br/>";
                        }
                    }
                }
                else
                {
                    msgcnt = msgcnt + 1;
                    msg = msg + msgcnt.ToString() + ". Validity From Date.<br/>";
                }
                if (tbValidityTo.Text.Length > 0)
                {
                    if (_validation.IsDate(tbValidityTo.Text) == false)
                    {
                        msgcnt = msgcnt + 1;
                        msg = msg + msgcnt.ToString() + ". Invalid Validity To Date.<br/>";
                    }
                    else
                    {
                        DateTime fromDate = DateTime.ParseExact(tbValidityFromDate.Text, "dd/MM/yyyy", null);
                        DateTime ToDate = DateTime.ParseExact(tbValidityTo.Text, "dd/MM/yyyy", null);
                        if (ToDate < fromDate)
                        {
                            msgcnt = msgcnt + 1;
                            msg = msg + msgcnt.ToString() + ". Check To Date. Validity To Date should be Greater than Validity From Date.<br/>";
                        }
                    }
                }
                else
                {
                    msgcnt = msgcnt + 1;
                    msg = msg + msgcnt.ToString() + ". Validity To Date.<br/>";
                }
            }
            else if (ddlTypes.SelectedValue == "3")
            {
                if (tbEnglishSub.Text == "")
                {
                    msgcnt = msgcnt + 1;
                    msg = msg + msgcnt.ToString() + ". Enter Subject English Language <br>";
                }
                //if (tbLocalLangSub.Text == "")
                //{
                //    msgcnt = msgcnt + 1;
                //    msg = msg + msgcnt.ToString() + ". Enter Subject Local Language <br>";
                //}
                if (tbDescriEng.Text == "")
                {
                    msgcnt = msgcnt + 1;
                    msg = msg + msgcnt.ToString() + ". Enter Description English Language <br>";
                }
                //if (tbDescrLocal.Text == "")
                //{
                //    msgcnt = msgcnt + 1;
                //    msg = msg + msgcnt.ToString() + ". Enter Description Local Language <br>";
                //}
                if (tbUrl.Text != "")
                {
                    if (_validation.isValidURL(tbUrl.Text) == false)
                    {
                        msgcnt = msgcnt + 1;
                        msg = msg + msgcnt + ". Enter Valid URL. <br/>";
                    }
                }
                if (tbValidityFromDate.Text.Length > 0)
                {
                    if (_validation.IsDate(tbValidityFromDate.Text) == false)
                    {
                        msgcnt = msgcnt + 1;
                        msg = msg + msgcnt.ToString() + ". Invalid Leave Start Date<br/>";
                    }
                    else
                    {
                        DateTime fromDate = DateTime.ParseExact(tbValidityFromDate.Text, "dd/MM/yyyy", null/* TODO Change to default(_) if this is not a reference type */);
                        DateTime currDate = DateTime.Now.Date;
                        if (currDate > fromDate)
                        {
                            msgcnt = msgcnt + 1;
                            msg = msg + msgcnt.ToString() + ". Check Validity From Date. From Date should be greater than Today's Date.<br/>";
                        }
                    }
                }
                else
                {
                    msgcnt = msgcnt + 1;
                    msg = msg + msgcnt.ToString() + ". Validity From Date.<br/>";
                }
                if (tbValidityTo.Text.Length > 0)
                {
                    if (_validation.IsDate(tbValidityTo.Text) == false)
                    {
                        msgcnt = msgcnt + 1;
                        msg = msg + msgcnt.ToString() + ". Invalid Validity To Date.<br/>";
                    }
                    else
                    {
                        DateTime fromDate = DateTime.ParseExact(tbValidityFromDate.Text, "dd/MM/yyyy", null);
                        DateTime ToDate = DateTime.ParseExact(tbValidityTo.Text, "dd/MM/yyyy", null);
                        if (ToDate < fromDate)
                        {
                            msgcnt = msgcnt + 1;
                            msg = msg + msgcnt.ToString() + ". Check To Date. Validity To Date should be Greater than Validity From Date.<br/>";
                        }
                    }
                }
                else
                {
                    msgcnt = msgcnt + 1;
                    msg = msg + msgcnt.ToString() + ". Validity To Date.<br/>";
                }
            }
            else if (ddlTypes.SelectedValue == "6")
            {
                if (tbEnglishSub.Text == "")
                {
                    msgcnt = msgcnt + 1;
                    msg = msg + msgcnt.ToString() + ". Enter Subject English Language <br>";
                }
                //if (tbLocalLangSub.Text == "")
                //{
                //    msgcnt = msgcnt + 1;
                //    msg = msg + msgcnt.ToString() + ". Enter Subject Local Language <br>";
                //}
                if (tbDescriEng.Text == "")
                {
                    msgcnt = msgcnt + 1;
                    msg = msg + msgcnt.ToString() + ". Enter Description English Language <br>";
                }
                //if (tbDescrLocal.Text == "")
                //{
                //    msgcnt = msgcnt + 1;
                //    msg = msg + msgcnt.ToString() + ". Enter Description Local Language <br>";
                //}
                if (Session["img1"] == null)
                {
                    msgcnt = msgcnt + 1;
                    msg = msg + msgcnt.ToString() + ". Please Choose Image1.<br>";
                }
                if (Session["img2"] == null)
                {
                    msgcnt = msgcnt + 1;
                    msg = msg + msgcnt.ToString() + ". Please Choose Image2.<br>";
                }
                if (Session["pdf"] != null)
                {
                    if (Session["pdf"] == null)
                    {
                        msgcnt = msgcnt + 1;
                        msg = msg + msgcnt.ToString() + ". Please Choose Document File.<br>";
                    }
                }
                if (tbUrl.Text != "")
                {
                    if (_validation.isValidURL(tbUrl.Text) == false)
                    {
                        msgcnt = msgcnt + 1;
                        msg = msg + msgcnt + ". Enter Valid URL. <br/>";
                    }
                }
                if (tbValidityFromDate.Text.Length > 0)
                {
                    if (_validation.IsDate(tbValidityFromDate.Text) == false)
                    {
                        msgcnt = msgcnt + 1;
                        msg = msg + msgcnt.ToString() + ". Invalid Leave Start Date<br/>";
                    }
                    else
                    {
                        DateTime fromDate = DateTime.ParseExact(tbValidityFromDate.Text, "dd/MM/yyyy", null/* TODO Change to default(_) if this is not a reference type */);
                        DateTime currDate = DateTime.Now.Date;
                        if (currDate > fromDate)
                        {
                            msgcnt = msgcnt + 1;
                            msg = msg + msgcnt.ToString() + ". Check Validity From Date. From Date should be greater than Today's Date.<br/>";
                        }
                    }
                }
                else
                {
                    msgcnt = msgcnt + 1;
                    msg = msg + msgcnt.ToString() + ". Validity From Date.<br/>";
                }
                if (tbValidityTo.Text.Length > 0)
                {
                    if (_validation.IsDate(tbValidityTo.Text) == false)
                    {
                        msgcnt = msgcnt + 1;
                        msg = msg + msgcnt.ToString() + ". Invalid Validity To Date.<br/>";
                    }
                    else
                    {
                        DateTime fromDate = DateTime.ParseExact(tbValidityFromDate.Text, "dd/MM/yyyy", null);
                        DateTime ToDate = DateTime.ParseExact(tbValidityTo.Text, "dd/MM/yyyy", null);
                        if (ToDate < fromDate)
                        {
                            msgcnt = msgcnt + 1;
                            msg = msg + msgcnt.ToString() + ". Check To Date. Validity To Date should be Greater than Validity From Date.<br/>";
                        }
                    }
                }
                else
                {
                    msgcnt = msgcnt + 1;
                    msg = msg + msgcnt.ToString() + ". Validity To Date.<br/>";
                }
            }

            if (msgcnt > 0)
            {
                Errormsg(msg);
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminAlertNoticePublishing.aspx-0003", ex.Message.ToString());
            return false;
        }
    }
    public bool IsValidPdf(FileUpload fileupload)
    {
        //string _fileFormat = GetMimeDataOfFile(fileupload.PostedFile);
        //if (((_fileFormat == "application/pdf")))
        //{
        //}
        //else
        //{
        //    Errormsg("Invalid file (Not a PDF)");
        //    return false;
        //}
        return true;
    }
    private void updateImages(string category)//M3
    {
        try
        {
            string saveDirectory = "../DBImages/AlertNotice/";
            string fileName = "";
            string fileSavePath = "";

            if (Session["img1"] != null && Session["img1"].ToString() != "")
            {
                fileName = category + "_W.png";
                fileSavePath = Path.Combine(saveDirectory, fileName);
                byte[] image1 = (byte[])Session["img1"];
                System.IO.File.WriteAllBytes(Server.MapPath(fileSavePath), image1);
            }
            if (Session["img2"] != null && Session["img2"].ToString() != "")
            {
                fileName = category + "_M.png";
                fileSavePath = Path.Combine(saveDirectory, fileName);
                byte[] image2 = (byte[])Session["img2"];
                System.IO.File.WriteAllBytes(Server.MapPath(fileSavePath), image2);

            }
            if (Session["pdf"] != null && Session["pdf"].ToString() != "")
            {
                fileName = category + ".pdf";
                fileSavePath = Path.Combine(saveDirectory, fileName);
                byte[] documentfile = (byte[])Session["pdf"];
                System.IO.File.WriteAllBytes(Server.MapPath(fileSavePath), documentfile);
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminAlertNoticePublishing.aspx-0004", ex.Message.ToString());
            return;
        }
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
    private bool CheckFileExtention(FileUpload fuimage1)//M4
    {
        try
        {
            bool fileExtensionOK = false;
            string fileExtension;
            fileExtension = System.IO.Path.GetExtension(fuimage1.FileName).ToLower();
            string[] allowedExtensions = new[] { ".png" };
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
            _common.ErrorLog("PAdminAlertNoticePublishing.aspx-0006", ex.Message.ToString());
            Errormsg("Something went wrong with image (allow only  .png image). ");
            return default(Boolean);
        }
    }
    private bool CheckFileExtentionWeb(FileUpload fuimage2)//M5
    {
        try
        {
            bool fileExtensionOK = false;
            string fileExtension;
            fileExtension = System.IO.Path.GetExtension(fuimage2.FileName).ToLower();
            string[] allowedExtensions = new[] { ".png" };
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
            _common.ErrorLog("PAdminAlertNoticePublishing.aspx-0007", ex.Message.ToString());
            Errormsg("Something went wrong with image (allow only .png image). ");
            return default(Boolean);
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
            _common.ErrorLog("PAdminAlertNoticePublishing.aspx-0008", ex.Message.ToString());
            return null;
        }
    }
    public void getDocument(String docType)//M6
    {
        try
        {
            byte[] FileInvoice = (byte[])Session["pdf"];
            string base64String = Convert.ToBase64String(FileInvoice, 0, FileInvoice.Length);
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + ddlTypes.SelectedValue.ToString() + ".pdf");
            Response.BinaryWrite(FileInvoice);
            Response.End();
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "applicantDetailModel", "$('#applicantDetailModel').modal();", true);
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminAlertNoticePublishing.aspx-0009", ex.Message.ToString());
        }
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
    private void SaveNoticeNewsDraftDetails()//M7
    {
        try
        {
            string ipaddress = HttpContext.Current.Request.UserHostAddress;
            string UpdatedBy = Session["_UserCode"].ToString();
            string Action = Session["Action"].ToString();
            Int64 noticeid = 0;
            if (Action != "S")
            {
                noticeid = Convert.ToInt64(Session["tempnotice_id"]);
            }

            Int64 category = Convert.ToInt64(ddlTypes.SelectedValue);
            byte[] documentfile = (byte[])Session["pdf"];

            byte[] image1 = null;
            image1 = Session["img1"] as byte[];

            byte[] image2 = null;
            image2 = Session["img2"] as byte[];
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            string Mresult = "";
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_notice_news_draftinsert");
            MyCommand.Parameters.AddWithValue("p_action", Action);
            MyCommand.Parameters.AddWithValue("p_temp_notice_id", noticeid);
            MyCommand.Parameters.AddWithValue("p_category_code", category);
            MyCommand.Parameters.AddWithValue("p_subject", tbEnglishSub.Text.ToString());
            MyCommand.Parameters.AddWithValue("p_subject_local", tbLocalLangSub.Text.ToString());
            MyCommand.Parameters.AddWithValue("p_description", tbDescriEng.Text.ToString());
            MyCommand.Parameters.AddWithValue("p_description_local", tbDescrLocal.Text.ToString());
            MyCommand.Parameters.AddWithValue("p_url", tbUrl.Text.ToString());
            MyCommand.Parameters.AddWithValue("p_valid_from_dt", tbValidityFromDate.Text.ToString());
            MyCommand.Parameters.AddWithValue("p_valid_to_dt", tbValidityTo.Text.ToString());
            MyCommand.Parameters.AddWithValue("p_updatedby", UpdatedBy);
            MyCommand.Parameters.AddWithValue("p_ipaddress", ipaddress);
            MyCommand.Parameters.AddWithValue("p_image_1", (object)image1 ?? DBNull.Value);
            MyCommand.Parameters.AddWithValue("p_image_2", (object)image2 ?? DBNull.Value);
            MyCommand.Parameters.AddWithValue("p_doc", (object)documentfile ?? DBNull.Value);
            Mresult = bll.UpdateAll(MyCommand);
            if (Mresult == "Success")
            {
                if (Session["Action"].ToString() == "S")
                {

                    Successmsg("Draft Details Successfully Saved");
                }
                else if (Session["Action"].ToString() == "U")
                {
                    Successmsg("Draft Details Update Successfully ");
                }
                else if (Session["Action"].ToString() == "P")
                {
                    Successmsg("Draft Details Publish Successfully ");

                }
                updateImages(ddlTypes.SelectedValue);
                getNoticeNewsDraftDetails();
                getNoticeNewsDetails();
                ResetValues();
            }
            else
            {
                _common.ErrorLog("PAdminAlertNotice-M7", Mresult);
                Errormsg("Oops! You're data not saved." + Mresult);
            }

        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminAlertNoticePublishing.aspx-0010", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
        }
    }
    private void SaveNoticeNewsDetails()//M8
    {
        try
        {
            string ipaddress = HttpContext.Current.Request.UserHostAddress;
            string UpdatedBy = Session["_UserCode"].ToString();

            string Action = Session["Action"].ToString();
            Int64 noticeid = 0;
            if (Action != "SP")
            {
                noticeid = Convert.ToInt64(Session["noticeid"]);
            }
            Int64 category = Convert.ToInt64(ddlTypes.SelectedValue);
            byte[] documentfile = (byte[])Session["pdf"];

            byte[] image1 = null;
            image1 = Session["img1"] as byte[];

            byte[] image2 = null;
            image2 = Session["img2"] as byte[];
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            string Mresult = "";
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_notice_news_detailsinsert");
            MyCommand.Parameters.AddWithValue("p_action", Action);
            MyCommand.Parameters.AddWithValue("p_temp_notice_id", noticeid);
            MyCommand.Parameters.AddWithValue("p_category_code", category);
            MyCommand.Parameters.AddWithValue("p_subject", tbEnglishSub.Text.ToString());
            MyCommand.Parameters.AddWithValue("p_subject_local", tbLocalLangSub.Text.ToString());
            MyCommand.Parameters.AddWithValue("p_description", tbDescriEng.Text.ToString());
            MyCommand.Parameters.AddWithValue("p_description_local", tbDescrLocal.Text.ToString());
            MyCommand.Parameters.AddWithValue("p_image_1", (object)image1 ?? DBNull.Value);
            MyCommand.Parameters.AddWithValue("p_image_2", (object)image2 ?? DBNull.Value);
            MyCommand.Parameters.AddWithValue("p_doc", (object)documentfile ?? DBNull.Value);
            MyCommand.Parameters.AddWithValue("p_url", tbUrl.Text.ToString());
            MyCommand.Parameters.AddWithValue("p_valid_from_dt", tbValidityFromDate.Text.ToString());
            MyCommand.Parameters.AddWithValue("p_valid_to_dt", tbValidityTo.Text.ToString());
            MyCommand.Parameters.AddWithValue("p_updatedby", UpdatedBy);
            MyCommand.Parameters.AddWithValue("p_ipaddress", ipaddress);
            Mresult = bll.UpdateAll(MyCommand);
            if (Mresult == "Success")
            {
                if (Session["Action"].ToString() == "SP")
                {
                    Successmsg("Details Successfully Saved");

                }
                if (Session["Action"].ToString() == "D")
                {
                    Successmsg("Details Discountinue Successfully");
                }
                updateImages(ddlTypes.SelectedValue);
                getNoticeNewsDetails();
                getNoticeNewsDraftDetails();
                getNoticeNewsCount();
                ResetValues();
            }
            else
            {
                _common.ErrorLog("PAdminAlertNotice-M8", Mresult);
                Errormsg("Oops! You're data not saved." + Mresult);
            }

        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminAlertNoticePublishing.aspx-0011", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
        }
    }
    private void getNoticeNewsDraftDetails()//M9
    {
        try
        {
            pnlnoRecordfound.Visible = true;
            gvPengingPublishing.Visible = false;
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_getnotice_news_draftdetails");
            DataTable MyTable = bll.SelectAll(MyCommand);
            if (MyTable.TableName == "Success")
            {
                if (MyTable.Rows.Count > 0)
                {
                    gvPengingPublishing.DataSource = MyTable;
                    gvPengingPublishing.DataBind();
                    pnlnoRecordfound.Visible = false;
                    gvPengingPublishing.Visible = true;
                }

            }
            else
            {
                _common.ErrorLog("PAdminAlertNoticePublishing.aspx-0012", MyTable.TableName);
                Errormsg(MyTable.TableName);
                pnlnoRecordfound.Visible = true;
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminAlertNoticePublishing.aspx-0013", ex.Message.ToString());
            Errormsg(ex.Message);

        }
    }
    private void getNoticeNewsDetails()//M10
    {
        try
        {
            pnlActivenoRecordfound.Visible = true;
            gvCurrentlyActive.Visible = false;
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_getnotice_news_details");
            DataTable MyTable = bll.SelectAll(MyCommand);
            if (MyTable.TableName == "Success")
            {
                if (MyTable.Rows.Count > 0)
                {
                    gvCurrentlyActive.DataSource = MyTable;

                    gvCurrentlyActive.DataBind();
                    pnlActivenoRecordfound.Visible = false;
                    gvCurrentlyActive.Visible = true;
                }

            }
            else
            {
                _common.ErrorLog("PAdminAlertNoticePublishing.aspx-0014", MyTable.TableName);
                Errormsg(MyTable.TableName);

            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminAlertNoticePublishing.aspx-0015", ex.Message.ToString());
            Errormsg(ex.Message);

        }
    }
    private void getNoticeNewsDetailsHistory()//M11
    {
        try
        {
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_getnotice_news_details_history");
            DataTable MyTable = bll.SelectAll(MyCommand);
            if (MyTable.TableName == "Success")
            {
                if (MyTable.Rows.Count > 0)
                {
                    gvHistory.DataSource = MyTable;
                    gvHistory.DataBind();
                    pnlNoRecord.Visible = false;
                    gvHistory.Visible = true;
                }

            }
            else
            {
                _common.ErrorLog("PAdminAlertNoticePublishing.aspx-0016", MyTable.TableName);
                Errormsg(MyTable.TableName);

            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminAlertNoticePublishing.aspx-0017", ex.Message.ToString());
            Errormsg(ex.Message);

        }
    }
    public void getNoticeNewsCount()//M12
    {
        try
        {
            DataTable dt = new DataTable();
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_noticenews_count");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    lblTotalAlertNotice.Text = dt.Rows[0]["activetotal"].ToString();
                    lblActiveEvents.Text = dt.Rows[0]["activeevent"].ToString();
                    lblActiveAlert.Text = dt.Rows[0]["activealert"].ToString();
                    lblActiveNotice.Text = dt.Rows[0]["activenoticee"].ToString();
                }
                else
                {
                    _common.ErrorLog("PAdminAlertNoticePublishing.aspx-0018", dt.TableName.ToString());
                    Errormsg(dt.TableName);

                }
            }

        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminAlertNoticePublishing.aspx-0019", ex.Message.ToString());
            Errormsg(ex.Message);

        }
    }
    private void ddlTypesChanged()
    {
        if (ddlTypes.SelectedValue == "1")
        {
            lblUrlnotMandatory.Visible = true;
            lblurl.Visible = false;
            lbluploadDocument.Visible = false;
            lbluploadDocumentnotMan.Visible = true;
            divImage.Visible = true;
            lblimage1.Visible = true;
            lblimage2.Visible = true;
            lblimage1NotMan.Visible = false;
            lblImage2NotMan.Visible = false;
            lblDescription.Visible = true;
            lblDescriptionNotMan.Visible = false;
        }
        else if (ddlTypes.SelectedValue == "2")
        {
            lblUrlnotMandatory.Visible = true;
            lblurl.Visible = false;
            lbluploadDocument.Visible = false;
            lbluploadDocumentnotMan.Visible = true;
            lblDescription.Visible = false;
            lblDescriptionNotMan.Visible = true;
            divImage.Visible = true;
            lblimage1.Visible = true;
            lblimage2.Visible = true;
            lblImage2NotMan.Visible = false;
            lblimage1NotMan.Visible = false;
        }

        else if (ddlTypes.SelectedValue == "3")
        {
            lblUrlnotMandatory.Visible = true;
            lblurl.Visible = false;
            lbluploadDocument.Visible = false;
            lbluploadDocumentnotMan.Visible = true;
            lblimage1.Visible = false;
            lblimage1NotMan.Visible = true;
            lblimage2.Visible = false;
            lblImage2NotMan.Visible = true;
            divImage.Visible = false;
            lblDescription.Visible = true;
            lblDescriptionNotMan.Visible = false;
        }
        else if (ddlTypes.SelectedValue == "6")
        {
            lblUrlnotMandatory.Visible = true;
            lblurl.Visible = false;
            lbluploadDocument.Visible = false;
            lbluploadDocumentnotMan.Visible = true;
            divImage.Visible = true;
            lblimage1.Visible = true;
            lblimage2.Visible = true;
            lblimage1NotMan.Visible = false;
            lblImage2NotMan.Visible = false;
            lblDescription.Visible = true;
            lblDescriptionNotMan.Visible = false;
        }
    }
    private void Errormsg(string msg)
    {
        string popup = _popup.modalPopupSmall("W", "Please Note", msg, "Close");
        Response.Write(popup);
    }
    private void Successmsg(string msg)
    {
        string popup = _popup.modalPopupSmall("S", "Success", msg, "OK");
        Response.Write(popup);
    }
    private void ConfirmMsg(string msg)
    {
        lblConfirmation.Text = msg;
        mpConfirmation.Show();
    }
    private void InfoMsg(string msg)
    {
        string popup = _popup.modalPopupLarge("I", "Information", msg, "Close");
        Response.Write(popup);
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

    #region Events
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (Session["Action"].ToString() == "S" || Session["Action"].ToString() == "U" || Session["Action"].ToString() == "P")
        {
            SaveNoticeNewsDraftDetails();

        }
        if (Session["Action"].ToString() == "SP" || Session["Action"].ToString() == "D")
        {
            SaveNoticeNewsDetails();
        }
    }
    protected void lbtndownloadReport_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
    }
    protected void lbtnview_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        string msg = "";
        msg = msg + "1. Events:URL/Pdf are Not Mandatory.<br/>";
        msg = msg + "2. Alert:Subject & Image is Mandatory.<br/>";
        msg = msg + "3. Notice:Image Field will not be Visible.<br/>";
        InfoMsg(msg);
    }
    protected void btnUploadimage1_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (!fuimage1.HasFile)
        {

            Errormsg("Please select report first");

            return;
        }
        string _fileFormat = GetMimeDataOfFile(fuimage1);
        if ((_fileFormat == "image/png")|| (_fileFormat == "image/x-png"))
        {
        }
        else
        {
            Errormsg("File must png type");

            return;
        }
        if (!CheckFileExtentionWeb(fuimage1))
        {
            Errormsg("File must be png type");

            return;
        }

        decimal size = Math.Round((Convert.ToDecimal(fuimage1.PostedFile.ContentLength) / Convert.ToDecimal(1024)), 2);
        if (size > 1024 || size < 10)
        {
            Errormsg("File size must be between 10 kb to 1MB ");

            return;
        }
        byte[] PhotoImage = null;
        PhotoImage = convertByteFile(fuimage1);
        Img1.ImageUrl = GetImage(PhotoImage);
        Img1.Visible = true;
        lbtncloseImage1.Visible = true;
        Session["img1"] = fuimage1.FileBytes;
    }
    protected void btnUploadimage2_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (!fuimage2.HasFile)
        {

            Errormsg("Please select report first");
            return;
        }
        string _fileFormat = GetMimeDataOfFile(fuimage2);
        if ((_fileFormat == "image/png") || (_fileFormat == "image/x-png"))
        {
        }
        else
        {
            Errormsg("File must be png type");
            return;
        }
        if (!CheckFileExtention(fuimage2))
        {
            Errormsg("File must be png type");
            return;
        }

        decimal size = Math.Round((Convert.ToDecimal(fuimage2.PostedFile.ContentLength) / Convert.ToDecimal(1024)), 2);
        if (size > 1024 || size < 10)
        {
            Errormsg("File size must be between 10 kb to 1MB");
            return;
        }
        byte[] PhotoImage = null;
        PhotoImage = convertByteFile(fuimage2);
        Img2.ImageUrl = GetImage(PhotoImage);
        Img2.Visible = true;
        lbtncloseImage2.Visible = true;
        Session["img2"] = fuimage2.FileBytes;
    }
    protected void btnUploadpdf_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();

        Byte[] bytes = fudocfile.FileBytes;
        if (FileContainsMaliciousCode(bytes) == true)
        {
            Errormsg("Invalid File. Please upload a different file.");
            return;
        }

        string _fileFormat = GetMimeDataOfFile(fudocfile);
        if (((_fileFormat == "application/pdf")))
        {
        }
        else
        {
            Errormsg("Invalid file (Not a PDF)");
            return ;
        }

        if (fudocfile.HasFile == true)
        {
            if (Convert.ToInt32(fudocfile.FileBytes.Length) < 2097152 & fudocfile.FileName.Length > 2)
            {
                if (fudocfile.FileName.Length <= 50)
                {
                    Session["pdf"] = fudocfile.FileBytes;
                    lblPDF.Text = fudocfile.FileName;
                    Session["pdfName"] = fudocfile.FileName;
                    lblPDF.Visible = true;
                }
            }
            else
            {
                Errormsg("Please select file less than 2 MB");
            }
        }


    }
    protected void lbtncloseImage2_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Session["img2"] = null;
        Img2.Visible = false;
        lbtncloseImage2.Visible = false;
    }
    protected void lbtncloseImage1_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Session["img1"] = null;
        Img1.Visible = false;
        lbtncloseImage1.Visible = false;

    }
    protected void ddlTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        ddlTypesChanged();
    }
    protected void lbtnUpdate_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (ValidValue() == false)
            return;
        Session["Action"] = "U";
        ConfirmMsg("Do you want to update Alert Notice Details ?");
    }
    protected void lbtnSaveDraft_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (ValidValue() == false)
            return;
        Session["Action"] = "S";
        ConfirmMsg("Do you want Save Draft Alert Notice Details ?");
    }
    protected void lbtnSavePublish_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (ValidValue() == false)
            return;
        Session["Action"] = "SP";
        ConfirmMsg("Do you want Save Alert Notice Details ?");
    }
    protected void lbtnReset_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        ResetValues();
    }
    protected void lbtnCancel_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        ResetValues();
    }
    protected void gvPengingPublishing_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        CsrfTokenValidate();
        if (e.CommandName == "PendingUpdate")
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            Session["categorycode"] = gvPengingPublishing.DataKeys[row.RowIndex]["categorycode"];
            Session["tempnotice_id"] = gvPengingPublishing.DataKeys[row.RowIndex]["tempnotice_id"];
            string noticecategory_name = gvPengingPublishing.DataKeys[row.RowIndex]["noticecategory_name"].ToString();
            string sub_ject = gvPengingPublishing.DataKeys[row.RowIndex]["sub_ject"].ToString();
            string subjectlocal = gvPengingPublishing.DataKeys[row.RowIndex]["subjectlocal"].ToString();
            string description = gvPengingPublishing.DataKeys[row.RowIndex]["description"].ToString();
            string description_local = gvPengingPublishing.DataKeys[row.RowIndex]["description_local"].ToString();
            string urllink = gvPengingPublishing.DataKeys[row.RowIndex]["urllink"].ToString();
            string valid_fromdt = gvPengingPublishing.DataKeys[row.RowIndex]["valid_fromdt"].ToString();
            string valid_todt = gvPengingPublishing.DataKeys[row.RowIndex]["valid_todt"].ToString();
            LoadNoticeNews();
            ddlTypes.SelectedValue = Session["categorycode"].ToString();
            tbEnglishSub.Text = sub_ject;
            tbLocalLangSub.Text = subjectlocal;
            tbDescriEng.Text = description;
            tbDescrLocal.Text = description_local;
            if (gvPengingPublishing.DataKeys[row.RowIndex].Values["image1"].ToString() != "" && gvPengingPublishing.DataKeys[row.RowIndex].Values["image1"].ToString() != null)
            {
                Session["img1"] = gvPengingPublishing.DataKeys[row.RowIndex]["image1"];
                Byte[] img1 = (Byte[])Session["img1"];
                Img1.ImageUrl = GetImage(img1);
                Img1.Visible = true;
                lbtncloseImage1.Visible = true;
            }
            if (gvPengingPublishing.DataKeys[row.RowIndex].Values["image2"].ToString() != "" && gvPengingPublishing.DataKeys[row.RowIndex].Values["image2"].ToString() != null)
            {
                Session["img2"] = gvPengingPublishing.DataKeys[row.RowIndex]["image2"];
                Byte[] img2 = (Byte[])Session["img2"];
                Img2.ImageUrl = GetImage(img2);
                Img2.Visible = true;
                lbtncloseImage2.Visible = true;
            }
            if (gvPengingPublishing.DataKeys[row.RowIndex].Values["documt"].ToString() != "" && gvPengingPublishing.DataKeys[row.RowIndex].Values["documt"].ToString() != null)
            {
                byte[] bytes = (byte[])gvPengingPublishing.DataKeys[row.RowIndex].Values["documt"];
                Session["pdf"] = bytes;
                lbtnDocument.Visible = true;
                lbtnDocument.Text = gvPengingPublishing.DataKeys[row.RowIndex].Values["categorycode"].ToString() + ".pdf";
            }
            tbUrl.Text = urllink;
            tbValidityFromDate.Text = valid_fromdt;
            tbValidityTo.Text = valid_todt;
            ddlTypesChanged();
            lbtnSaveDraft.Visible = false;
            lbtnCancel.Visible = true;
            lbtnReset.Visible = false;
            lbtnSavePublish.Visible = false;
            lbtnUpdate.Visible = true;
            lblAlertNoticeHead.Visible = false;
            lblAlertNoticeUpdate.Visible = true;
            Session["Action"] = "U";
        }

        if (e.CommandName == "PendingPublish")
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            Session["categorycode"] = gvPengingPublishing.DataKeys[row.RowIndex]["categorycode"];
            Session["tempnotice_id"] = gvPengingPublishing.DataKeys[row.RowIndex]["tempnotice_id"];
            string noticecategory_name = gvPengingPublishing.DataKeys[row.RowIndex]["noticecategory_name"].ToString();
            string sub_ject = gvPengingPublishing.DataKeys[row.RowIndex]["sub_ject"].ToString();
            string subjectlocal = gvPengingPublishing.DataKeys[row.RowIndex]["subjectlocal"].ToString();
            string description = gvPengingPublishing.DataKeys[row.RowIndex]["description"].ToString();
            string description_local = gvPengingPublishing.DataKeys[row.RowIndex]["description_local"].ToString();
            string urllink = gvPengingPublishing.DataKeys[row.RowIndex]["urllink"].ToString();
            string valid_fromdt = gvPengingPublishing.DataKeys[row.RowIndex]["valid_fromdt"].ToString();
            string valid_todt = gvPengingPublishing.DataKeys[row.RowIndex]["valid_todt"].ToString();
            LoadNoticeNews();
            ddlTypes.SelectedValue = Session["categorycode"].ToString();
            tbEnglishSub.Text = sub_ject;
            tbLocalLangSub.Text = subjectlocal;
            tbDescriEng.Text = description;
            tbDescrLocal.Text = description_local;
            if (gvPengingPublishing.DataKeys[row.RowIndex].Values["image1"].ToString() != "" && gvPengingPublishing.DataKeys[row.RowIndex].Values["image1"].ToString() != null)
            {
                Session["img1"] = gvPengingPublishing.DataKeys[row.RowIndex]["image1"];
                Byte[] img1 = (Byte[])Session["img1"];
                Img1.ImageUrl = GetImage(img1);
                Img1.Visible = true;
                lbtncloseImage1.Visible = true;
            }
            if (gvPengingPublishing.DataKeys[row.RowIndex].Values["image2"].ToString() != "" && gvPengingPublishing.DataKeys[row.RowIndex].Values["image2"].ToString() != null)
            {
                Session["img2"] = gvPengingPublishing.DataKeys[row.RowIndex]["image2"];
                Byte[] img2 = (Byte[])Session["img2"];
                Img2.ImageUrl = GetImage(img2);
                Img2.Visible = true;
                lbtncloseImage2.Visible = true;
            }
            if (gvPengingPublishing.DataKeys[row.RowIndex].Values["documt"].ToString() != "" && gvPengingPublishing.DataKeys[row.RowIndex].Values["documt"].ToString() != null)
            {
                byte[] bytes = (byte[])gvPengingPublishing.DataKeys[row.RowIndex].Values["documt"];
                Session["pdf"] = bytes;
                lbtnDocument.Visible = true;
                lbtnDocument.Text = "A_N " + gvPengingPublishing.DataKeys[row.RowIndex].Values["categorycode"].ToString() + ".pdf";
            }
            tbUrl.Text = urllink;
            tbValidityFromDate.Text = valid_fromdt;
            tbValidityTo.Text = valid_todt;
            ddlTypesChanged();
            lbtnSaveDraft.Visible = false;
            lbtnCancel.Visible = true;
            lbtnReset.Visible = false;
            lbtnSavePublish.Visible = false;
            lbtnUpdate.Visible = true;
            lblAlertNoticeHead.Visible = false;
            lblAlertNoticeUpdate.Visible = true;
            ConfirmMsg("Do you want Publish Alert Notice Details ?");
            Session["Action"] = "P";
        }
    }
    protected void gvPengingPublishing_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvPengingPublishing.PageIndex = e.NewPageIndex;
        getNoticeNewsDraftDetails();
    }
    protected void gvCurrentlyActive_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        CsrfTokenValidate();
        if (e.CommandName == "Discontinuenotice")
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;

            Session["categorycode"] = gvCurrentlyActive.DataKeys[row.RowIndex]["categorycode"];
            Session["noticeid"] = gvCurrentlyActive.DataKeys[row.RowIndex]["noticeid"];
            // LoadNoticeNews();
            // ddlTypes.SelectedValue = Session["categorycode"].ToString();
            ConfirmMsg("Do you want Discountinue Alert Notice Details ?");
            Session["Action"] = "D";
        }
    }
    protected void gvHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvHistory.PageIndex = e.NewPageIndex;
        mpViewHistory.Show();
        getNoticeNewsDetailsHistory();
    }
    protected void lbtnViewHistory_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        mpViewHistory.Show();
        getNoticeNewsDetailsHistory();
    }
    protected void gvCurrentlyActive_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvCurrentlyActive.PageIndex = e.NewPageIndex;
        getNoticeNewsDetails();
    }
    #endregion
}