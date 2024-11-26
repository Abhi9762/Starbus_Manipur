using Npgsql;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI.WebControls;
using System.Xml;

public partial class Auth_ProjectAdmGenInfo : BasePage
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    byte[] PhotoImage = null;
    [System.Runtime.InteropServices.DllImport("urlmon.dll")]
    public static extern int FindMimeFromData(IntPtr pBC, [MarshalAs(UnmanagedType.LPWStr)] string pwzUrl, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1, SizeParamIndex = 3)] byte[] pBuffer, int cbSize, [MarshalAs(UnmanagedType.LPWStr)] string pwzMimeProposed, int dwMimeFlags, [MarshalAs(UnmanagedType.U4)] ref int ppwzMimeOut, int dwReserved);
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["_moduleName"] = "Project Configuration";
            loadGenralData();
        }
    }

    #region "Method"
    private void loadGenralData()//M1
    {
        try
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("../CommonData.xml"));
            XmlNodeList deptlogo = doc.GetElementsByTagName("dept_logo_url");
            if (deptlogo.Item(0).InnerXml != "")
            {
                ImgDepartmentLogo.ImageUrl = deptlogo.Item(0).InnerXml;
                ImgDepartmentLogo.Visible = true;             
                Session["DeptLogocount"] = 1;
                Session["DeptLogoImgURL"] = deptlogo.Item(0).InnerXml;
            }
            XmlNodeList govtlogo = doc.GetElementsByTagName("govt_logo_url");
            if (govtlogo.Item(0).InnerXml != "")
            {
                imgGovLogo.ImageUrl = govtlogo.Item(0).InnerXml;
                imgGovLogo.Visible = true;
                Session["GovtLogocount"] = 1;
                Session["GovtLogoImgURL"] = govtlogo.Item(0).InnerXml;
            }
            XmlNodeList favicon = doc.GetElementsByTagName("fav_icon_url");
            if (favicon.Item(0).InnerXml != "")
            {
                imgFavicon.ImageUrl = favicon.Item(0).InnerXml;
                imgFavicon.Visible = true;
                Session["FaviconCount"] = 1;
                Session["FaviconImgURL"] = favicon.Item(0).InnerXml;
            }

            XmlNodeList dept_en = doc.GetElementsByTagName("dept_Name_en");
            tbDepartmentNameEn.Text = dept_en.Item(0).InnerXml;
            XmlNodeList dept_local = doc.GetElementsByTagName("dept_Name_local");
            tbDepartmentNameL.Text = dept_local.Item(0).InnerXml;
            XmlNodeList dept_abbr = doc.GetElementsByTagName("dept_Abbr_en");
            tbDepartmentAbbrEn.Text = dept_abbr.Item(0).InnerXml;
            XmlNodeList dept_abbr_local = doc.GetElementsByTagName("dept_Abbr_local");
            tbDepartmentAbbreviationLocal.Text = dept_abbr_local.Item(0).InnerXml;
            XmlNodeList app_title_en = doc.GetElementsByTagName("title_txt_en");
            tbApplication.Text = app_title_en.Item(0).InnerXml;
            XmlNodeList app_title_local = doc.GetElementsByTagName("title_txt_local");
            tbAppliactionLocal.Text = app_title_local.Item(0).InnerXml;
            XmlNodeList version = doc.GetElementsByTagName("Ver_Name"); 
            tbVersion.Text = version.Item(0).InnerXml;
            XmlNodeList footer = doc.GetElementsByTagName("managed_by"); 
            tbFooterEn.Text = footer.Item(0).InnerXml;
            XmlNodeList footer_local = doc.GetElementsByTagName("managed_by_local");
            tbFooterLocal.Text = footer_local.Item(0).InnerXml;

        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminGenInfo-M1", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
        }
    }
    private bool validValue()//M5
    {
        try
        {
            int msgcnt = 0;
            string msg = "";
            if (Convert.ToInt32(Session["DeptLogocount"].ToString()) == 0)
            {
                if (Session["DeptLogoImg"] == null)
                {
                    msgcnt = msgcnt + 1;
                    msg = msg + msgcnt.ToString() + ". Department Logo.<br>";
                }
            }
            if (Convert.ToInt32(Session["GovtLogocount"].ToString()) == 0)
            {
                if (Session["GovtLogoImg"] == null)
                {
                    msgcnt = msgcnt + 1;
                    msg = msg + msgcnt.ToString() + ". Government Logo.<br>";
                }
            }
            if (Convert.ToInt32(Session["FaviconCount"].ToString()) == 0)
            {
                if (Session["FaviconImg"] == null)
                {
                    msgcnt = msgcnt + 1;
                    msg = msg + msgcnt.ToString() + ". Favicon.<br>";
                }
            }
            if (_validation.IsValidString(tbApplication.Text, 3, tbApplication.MaxLength) == false)
            {
                msgcnt = msgcnt + 1;
                msg = msg + msgcnt.ToString() + ". Valid Application Title in English.<br>";
            }
            if (_validation.IsValidString(tbDepartmentAbbrEn.Text, 3, tbDepartmentAbbrEn.MaxLength) == false)
            {
                msgcnt = msgcnt + 1;
                msg = msg + msgcnt.ToString() + ". Valid Department Abbrevation in English.<br>";
            }
            if (tbDepartmentNameEn.Text == "")
            {
                msgcnt = msgcnt + 1;
                msg = msg + msgcnt.ToString() + ". Department Name in English.<br>";
            }
            if (tbVersion.Text == "")
            {
                msgcnt = msgcnt + 1;
                msg = msg + msgcnt.ToString() + ". Version.<br>";
            }
            if (tbFooterEn.Text == "")
            {
                msgcnt = msgcnt + 1;
                msg = msg + msgcnt.ToString() + ". Footer.<br>";
            }

            if (rbtnLocalization.SelectedValue == "Y")
            {
                if (ddllocalLanguage.SelectedIndex == 0)
                {
                    msgcnt = msgcnt + 1;
                    msg = msg + msgcnt.ToString() + ". Select Local Language.<br>";
                }
                if (_validation.IsValidString(tbAppliactionLocal.Text, 3, tbAppliactionLocal.MaxLength) == false)
                {
                    msgcnt = msgcnt + 1;
                    msg = msg + msgcnt.ToString() + ". Valid Application Title in Local.<br>";
                }
                if (_validation.IsValidString(tbDepartmentAbbreviationLocal.Text, 3, tbDepartmentAbbreviationLocal.MaxLength) == false)
                {
                    msgcnt = msgcnt + 1;
                    msg = msg + msgcnt.ToString() + ". Valid Department Abbrevation in Local.<br>";
                }
                if (tbDepartmentNameL.Text == "")
                {
                    msgcnt = msgcnt + 1;
                    msg = msg + msgcnt.ToString() + ". Department Name in Local.<br>";
                }
                if (_validation.IsValidString(tbFooterLocal.Text, 3, tbFooterLocal.MaxLength))
                {
                    msgcnt = msgcnt + 1;
                    msg = msg + msgcnt.ToString() + ". Footer in Local.<br>";
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
            _common.ErrorLog("PAdminGenInfo-M5", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
            return false;
        }


    }
    private void saveGeneralInformation()//M6
    {
        try
        {
            string ipaddress = HttpContext.Current.Request.UserHostAddress;
            string deptlogo = "", govlogo = "", favicon = "";
            if (Convert.ToInt32(Session["DeptLogocount"].ToString()) == 0)
            {
                deptlogo = "../Logo/DeptLogo.png";
            }
            else
            {
                deptlogo = Session["DeptLogoImgURL"].ToString();
            }
            if (Convert.ToInt32(Session["GovtLogocount"].ToString()) == 0)
            {
                govlogo = "../Logo/GovtLogo.png";
            }
            else
            {
                govlogo = Session["GovtLogoImgURL"].ToString();
            }
            if (Convert.ToInt32(Session["FaviconCount"].ToString()) == 0)
            {
                favicon = "../Logo/Favicon.png";
            }
            else
            {
                favicon = Session["FaviconImgURL"].ToString();
            }
            string title = tbAppliactionLocal.Text;
            string titleLocal = tbApplication.Text;
            string DeptAbbr = tbDepartmentAbbrEn.Text;
            string DeptAbbrLocal = tbDepartmentAbbreviationLocal.Text;
            string DeptNameEn = tbDepartmentNameEn.Text;
            string DeptNameLocal = tbDepartmentNameL.Text;
            string IPAddress = HttpContext.Current.Request.UserHostAddress;
            string Footer = tbFooterEn.Text;
            string FooterLocal = tbFooterLocal.Text;
            string Ver = "Ver" + tbVersion.Text;
            string Locallang = ddllocalLanguage.SelectedValue;
            string LocalizationYN = rbtnLocalization.SelectedValue;
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.portal_general_datas");
            MyCommand.Parameters.AddWithValue("p_title_en", title);
            MyCommand.Parameters.AddWithValue("p_title_local", titleLocal);
            MyCommand.Parameters.AddWithValue("p_dept_abbr_en", DeptAbbr);
            MyCommand.Parameters.AddWithValue("p_dept_abbr_local", DeptAbbrLocal);
            MyCommand.Parameters.AddWithValue("p_dept_name_en", DeptNameEn);
            MyCommand.Parameters.AddWithValue("p_dept_name_local", DeptNameLocal);
            MyCommand.Parameters.AddWithValue("p_deplogo", deptlogo);
            MyCommand.Parameters.AddWithValue("p_govlogo", govlogo);
            MyCommand.Parameters.AddWithValue("p_favicon", favicon);
            MyCommand.Parameters.AddWithValue("p_updatedby", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("p_ipaddress", IPAddress);
            MyCommand.Parameters.AddWithValue("p_footer", Footer);
            MyCommand.Parameters.AddWithValue("p_ver", Ver);
            MyCommand.Parameters.AddWithValue("p_footerlocal", FooterLocal);
            MyCommand.Parameters.AddWithValue("p_localization", LocalizationYN);
            MyCommand.Parameters.AddWithValue("p_locallang", Locallang);

            string Mresult = bll.UpdateAll(MyCommand);
            if (Mresult == "Success")
            {
                string saveDirectory = "../Logo";
                string fileName = "";
                string fileSavePath = "";
                XmlDocument doc = new XmlDocument();
                doc.Load(Server.MapPath("../CommonData.xml"));
                XmlNodeList dlogo = doc.GetElementsByTagName("dept_logo_url");
                XmlNodeList glogo = doc.GetElementsByTagName("govt_logo_url");
                XmlNodeList ficon = doc.GetElementsByTagName("fav_icon_url");

                if (Convert.ToInt32(Session["DeptLogocount"].ToString()) == 0)
                {
                    fileName = "DeptLogo.png";
                    fileSavePath = Path.Combine(saveDirectory, fileName);
                    //  ((FileUpload)Session["DEPTLOGO"]).PostedFile.SaveAs(Server.MapPath(fileSavePath));
                    ((System.Drawing.Image)Session["DEPTLOGO"]).Save(Server.MapPath(fileSavePath));

                    dlogo.Item(0).InnerXml = saveDirectory + "/" + fileName;
                }
                if (Convert.ToInt32(Session["GovtLogocount"].ToString()) == 0)
                {
                    fileName = "GovtLogo.png";
                    fileSavePath = Path.Combine(saveDirectory, fileName);
                    // ((FileUpload)Session["GOVTLOGO"]).PostedFile.SaveAs(Server.MapPath(fileSavePath));
                    ((System.Drawing.Image)Session["GOVTLOGO"]).Save(Server.MapPath(fileSavePath));
                    glogo.Item(0).InnerXml = saveDirectory + "/" + fileName;
                }
                if (Convert.ToInt32(Session["FaviconCount"].ToString()) == 0)
                {
                    fileName = "Favicon.png";
                    fileSavePath = Path.Combine(saveDirectory, fileName);
                    //    ((FileUpload)Session["Faviconlogo"]).PostedFile.SaveAs(Server.MapPath(fileSavePath));
                    ((System.Drawing.Image)Session["Faviconlogo"]).Save(Server.MapPath(fileSavePath));
                    ficon.Item(0).InnerXml = saveDirectory + "/" + fileName;
                }

                doc.Save(Server.MapPath("../CommonData.xml"));
                updateXml();
                SuccessMsg("Details have been Successfully saved !");
                loadGenralData();
            }
            else
            {
                _common.ErrorLog("PAdminGenInfo-M6", Mresult);
                Errormsg(Mresult);
            }

        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminGenInfo-M6", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
        }
    }
    private void updateXml()
    {
        try
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("../CommonData.xml"));
            XmlNodeList title_txt_xml = doc.GetElementsByTagName("title_txt_en");
            title_txt_xml.Item(0).InnerXml = tbApplication.Text;
            XmlNodeList title_txt_local_xml = doc.GetElementsByTagName("title_txt_local");
            title_txt_local_xml.Item(0).InnerXml = tbAppliactionLocal.Text;
            XmlNodeList dept_abbr_en = doc.GetElementsByTagName("dept_Abbr_en");
            dept_abbr_en.Item(0).InnerXml = tbDepartmentAbbrEn.Text;
            XmlNodeList dept_abbr_local = doc.GetElementsByTagName("dept_Abbr_local");
            dept_abbr_local.Item(0).InnerXml = tbDepartmentAbbreviationLocal.Text;
            XmlNodeList dept_Name_en_xml = doc.GetElementsByTagName("dept_Name_en");
            dept_Name_en_xml.Item(0).InnerXml = HttpUtility.HtmlEncode(tbDepartmentNameEn.Text);
            XmlNodeList dept_Name_local_xml = doc.GetElementsByTagName("dept_Name_local");
            dept_Name_local_xml.Item(0).InnerXml = HttpUtility.HtmlEncode(tbDepartmentNameL.Text);

            XmlNodeList Footer = doc.GetElementsByTagName("managed_by");
            Footer.Item(0).InnerXml = HttpUtility.HtmlEncode(tbFooterEn.Text);
            XmlNodeList Version = doc.GetElementsByTagName("Ver_Name");
            Version.Item(0).InnerXml = HttpUtility.HtmlEncode(tbVersion.Text);
            XmlNodeList footer_local = doc.GetElementsByTagName("managed_by_local");
            footer_local.Item(0).InnerXml = HttpUtility.HtmlEncode(tbDepartmentNameL.Text);
            doc.Save(Server.MapPath("../CommonData.xml"));
            Session["DeptLogocount"] = 0;
            Session["GovtLogocount"] = 0;
            Session["FaviconCount"] = 0;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminGenInfo-M1", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
        }
    }
    public bool checkFileExtention(System.Web.UI.WebControls.FileUpload fuFileUpload)//M2
    {
        try
        {
            decimal size = Math.Round((Convert.ToDecimal(fuFileUpload.PostedFile.ContentLength) / Convert.ToDecimal(1024)), 2);
            System.Drawing.Image img = System.Drawing.Image.FromStream(fuFileUpload.PostedFile.InputStream);
            int height = img.Height;
            int width = img.Width;
            if (size > 100)
            {
                return false;
            }

            bool fileExtensionOK = false;
            string fileExtension;
            fileExtension = System.IO.Path.GetExtension(fuFileUpload.FileName).ToLower();
            string[] allowedExtensions = new[] { ".png" };
            for (int i = 0; i <= allowedExtensions.Length - 1; i++)
            {
                if (fileExtension == allowedExtensions[i])
                    fileExtensionOK = true;
            }
            return fileExtensionOK;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminGenInfo-M2", ex.Message.ToString());
                      return default(Boolean);
        }
    }
    public byte[] convertByteFile(System.Web.UI.WebControls.FileUpload fuFileUpload)//M3
    {
        try
        {
            int intFileLength;
            byte[] byteData = null;
            if (fuFileUpload.HasFile)
            {
                // Check File Extention
                if (checkFileExtention(fuFileUpload) == true)
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
        catch(Exception ex)
        {
            _common.ErrorLog("PAdminGenInfo-M3", ex.Message.ToString());
            return null;

        }
       
    }
    public string GetImage(object img)//M4
    {
        try
        {
            return "data: Image/png;base64," + Convert.ToBase64String((byte[])img);
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminGenInfo-M4", ex.Message.ToString());
            return "";
        }
    }
  

    private void Errormsg(string msg)
    {
        string popup = _popup.modalPopupSmall("W", "Please Note", msg, "Close");
        Response.Write(popup);
    }

    private void Infomsg(string msg)
    {
        string popup = _popup.modalPopupSmall("I", "Please Note", msg, "Close");
        Response.Write(popup);

    }
    private void SuccessMsg(string msg)
    {
        string popup = _popup.modalPopupSmall("S", "Success", msg, "OK");
        Response.Write(popup);
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
    protected void GeneralHistoryList()
    {
        try
        {
            lblNoHistory.Visible = true;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_general_history");
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                gvHistory.DataSource = dt;
                gvHistory.DataBind();
                gvHistory.Visible = true;
                lblNoHistory.Visible = false;
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminGenInfo-M3", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
        }
    }

    #endregion

    #region "Event"
    protected void rbtnLocalization_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtnLocalization.SelectedValue == "Y")
        {
            ddllocalLanguage.Visible = true;
            tbAppliactionLocal.Visible = true;
            tbDepartmentNameL.Visible = true;
            tbDepartmentAbbreviationLocal.Visible = true;
            tbAppliactionLocal.Visible = true;
            tbFooterLocal.Visible = true;
            lblLocalLanguage.Visible = true;
            lbldeptName.Visible = true;
            lblDeptabbrLocal.Visible = true;
            lblAppTitleLocal.Visible = true;
            lblfooterLocal.Visible = true;
        }
        else
        {
            ddllocalLanguage.Visible = false;
            tbAppliactionLocal.Visible = false;
            tbDepartmentNameL.Visible = false;
            tbDepartmentAbbreviationLocal.Visible = false;
            tbAppliactionLocal.Visible = false;
            tbFooterLocal.Visible = false;
            lblLocalLanguage.Visible = false;
            lbldeptName.Visible = false;
            lblDeptabbrLocal.Visible = false;
            lblAppTitleLocal.Visible = false;
            lblfooterLocal.Visible = false;
        }
    }
    protected void btnDeotLogo_Click(object sender, EventArgs e)
    {
        if (checkFileExtention(fuDeptLogo) == false)
        {
            Errormsg("Department logo must be in .png format and size must not exceed 100 KB.");
            return;
        }
        PhotoImage = convertByteFile(fuDeptLogo);
        ImgDepartmentLogo.ImageUrl = GetImage(PhotoImage);
        Session["DeptLogoImg"] = PhotoImage;
        HttpPostedFile file = fuDeptLogo.PostedFile;
        System.Drawing.Image image = Bitmap.FromStream(file.InputStream);
        Session["DEPTLOGO"] = image;
        Session["DeptLogocount"] = 0;
        ImgDepartmentLogo.Visible = true;
    }
    protected void btnGovLogo_Click(object sender, EventArgs e)
    {
        if (checkFileExtention(fuGovtLogo) == false)
        {
            Errormsg("Department logo must be in .png format and size must not exceed 100 KB.");
            return;
        }
        PhotoImage = convertByteFile(fuGovtLogo);
        imgGovLogo.ImageUrl = GetImage(PhotoImage);
        Session["GovtLogoImg"] = PhotoImage;
        HttpPostedFile file = fuGovtLogo.PostedFile;
        System.Drawing.Image image = Bitmap.FromStream(file.InputStream);
        Session["GOVTLOGO"] = image;
        //Session["GOVTLOGO"] = fuGovtLogo.PostedFile;
        imgGovLogo.Visible = true;
        Session["GovtLogocount"] = 0;
    }
    protected void btnFavicon_Click(object sender, EventArgs e)
    {
        if (checkFileExtention(fuFavIcon) == false)
        {
            Errormsg("Department logo must be in .png format and size must not exceed 100 KB.");
            return;
        }
        PhotoImage = convertByteFile(fuFavIcon);
        imgFavicon.ImageUrl = GetImage(PhotoImage);
        Session["FaviconImg"] = PhotoImage;
        HttpPostedFile file = fuFavIcon.PostedFile;
        System.Drawing.Image image = Bitmap.FromStream(file.InputStream);
        Session["Faviconlogo"] = image;
        Session["FaviconCount"] = 0;
        imgFavicon.Visible = true;

    }
    protected void lbtnSave_Click(object sender, EventArgs e)
    {
        if (validValue() == false)
        {
            return;
        }
        lblConfirmation.Text = "Are you sure you want to update General Info?";
        mpConfirmation.Show();
        Session["Action"] = "S";
    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        if (Session["Action"].ToString() == "S")
        {
            saveGeneralInformation();
        }

    }

    protected void lbtnReset_Click(object sender, EventArgs e)
    {
        tbFooterEn.Text = "";
        tbFooterLocal.Text = "";
        tbVersion.Text = "";
        ddllocalLanguage.SelectedIndex = 0;
        rbtnLocalization.SelectedValue = null;
        tbAppliactionLocal.Text = "";
        tbApplication.Text = "";
        tbDepartmentAbbrEn.Text = "";
        tbDepartmentAbbreviationLocal.Text = "";
        tbDepartmentNameEn.Text = "";
        tbDepartmentNameL.Text = "";
        tbDepartmentAbbrEn.Text = "";
        tbApplication.Text = "";
        imgFavicon.ImageUrl = "";
        imgFavicon.Visible = false;
        imgGovLogo.ImageUrl = "";
        imgGovLogo.Visible = false;
        ImgDepartmentLogo.ImageUrl = "";
        ImgDepartmentLogo.Visible = false;

    }

    protected void lbtnHelp_Click(object sender, EventArgs e)
    {
        Infomsg("<span class=' font-weight-bold text-md'>Department Logo</span><br/><span class=' text-sm'>1. Image to be uploaded should be only in.png format with transaprent background.<br/> 2. Logo size should not exceed 100 KB.<br/> 3. Resolution of logo must be 60px X 60px</span><br/><span class=' font-weight-bold text-md'>Government Logo</span><br/><span class=' text-sm'>1. Image to be uploaded should be only in.png format with transaprent background.<br/> 2. Logo size should not exceed 100 KB.<br/> 3. Resolution of logo must be 60px X 60px.</span><br/><span class=' font-weight-bold text-md'>Favicon</span><br/><span class=' text-sm'>1. Image to be uploaded should be only in.png format with transaprent background.<br/> 2. Favicon size should not exceed 100 KB.<br/> 3. Resolution of logo must be 60px X 60px</span><br/><span class=' font-weight-bold text-md'> Department Name</span><br/><span class=' text-sm'>1. Department Name is the name of department and will be visible in Home page and other pages. <br/>2. Department Name length should not exceed 100 characters.</span><br/><span class=' font-weight-bold text-md'> Application Title</span><br/><span class=' text-sm'>1. Application Title will be visible at the top of the browser window.<br/> 2. Application Title should be of Max. 20 characters.</span></p>");
    }
    protected void lbtnViewHistory_Click(object sender, EventArgs e)
    {
        mplimithistory.Show();
        GeneralHistoryList();
    }
    protected void gvHistory_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
    {
        gvHistory.PageIndex = e.NewPageIndex;
        GeneralHistoryList();
        mplimithistory.Show();
    }
 protected void lbtnManual_Click(object sender, EventArgs e)
    {
        
    }


   
    #endregion





   
}