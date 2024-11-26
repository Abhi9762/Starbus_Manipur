using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class Auth_PAdminHomePage : BasePage
{
    [System.Runtime.InteropServices.DllImport("urlmon.dll")]
    public static extern int FindMimeFromData(IntPtr pBC, [MarshalAs(UnmanagedType.LPWStr)] string pwzUrl, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1, SizeParamIndex = 3)] byte[] pBuffer, int cbSize, [MarshalAs(UnmanagedType.LPWStr)] string pwzMimeProposed, int dwMimeFlags, [MarshalAs(UnmanagedType.U4)] ref int ppwzMimeOut, int dwReserved);
    private sbCommonFunc _common = new sbCommonFunc();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    bool checkImage;
    private byte[] PhotoImage = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Random randomclass = new Random();
            Session["rndNoCheck"] = randomclass.Next().ToString();
            hidtoken.Value = Session["rndNoCheck"].ToString();
            Session["_moduleName"] = "Home Page Background Images";
            LoadPhotos();
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
        string popup = _popup.modalPopupSmall("W", "Please Note", msg, "Close");
        Response.Write(popup);
    }
    private void successmsg(string msg)
    {
        string popup = _popup.modalPopupSmall("S", "Success", msg, "OK");
        Response.Write(popup);
    }
    private void InfoMsg(string msg)
    {
        string popup = _popup.modalPopupLarge("I", "Information", msg, "Close");
        Response.Write(popup);
    }
    private void ConfirmMsg(string msg)
    {
        lblConfirmation.Text = msg;
        mpConfirmation.Show();
    }
    protected void LoadPhotos()
    {
        try
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("../CommonData.xml"));
            XmlNodeList photo_url1 = doc.GetElementsByTagName("homepage_img1_url");
            XmlNodeList photo_url2 = doc.GetElementsByTagName("homepage_img2_url");
            XmlNodeList photo_url3 = doc.GetElementsByTagName("homepage_img3_url");
            XmlNodeList photo_url4 = doc.GetElementsByTagName("homepage_img4_url");
            XmlNodeList photo_url5 = doc.GetElementsByTagName("homepage_img5_url");
            XmlNodeList photo_url6 = doc.GetElementsByTagName("homepage_img6_url");
            if (photo_url1.Item(0).InnerXml != "")
            {
                img1.ImageUrl = "../HomeImage/" + photo_url1.Item(0).InnerXml;
                lblFileUpload1.Text = photo_url1.Item(0).InnerXml;
                lbtnDeleteImage1.Visible = true;
            }
            else
            {
                lblFileUpload1.Text = "Upload Image";
                img1.ImageUrl = "../HomeImage/no_image.png";
                lbtnDeleteImage1.Visible = false;
            }
            if (photo_url2.Item(0).InnerXml != "")
            {
                Img2.ImageUrl = "../HomeImage/" + photo_url2.Item(0).InnerXml;
                lblFileUpload2.Text = photo_url2.Item(0).InnerXml;
                lbtnDeleteImage2.Visible = true;
            }
            else
            {
                lblFileUpload2.Text = "Upload Image";
                Img2.ImageUrl = "../HomeImage/no_image.png";
                lbtnDeleteImage2.Visible = false;

            }
            if (photo_url3.Item(0).InnerXml != "")
            {
                Img3.ImageUrl = "../HomeImage/" + photo_url3.Item(0).InnerXml;
                lblFileUpload3.Text = photo_url3.Item(0).InnerXml;
                lbtnDeleteImage3.Visible = true;
            }
            else
            {
                lblFileUpload3.Text = "Upload Image";
                Img3.ImageUrl = "../HomeImage/no_image.png";
                lbtnDeleteImage3.Visible = false;

            }
            if (photo_url4.Item(0).InnerXml != "")
            {
                Img4.ImageUrl = "../HomeImage/" + photo_url4.Item(0).InnerXml;
                lblFileUpload4.Text = photo_url4.Item(0).InnerXml;
                lbtnDeleteImage4.Visible = true;
            }
            else
            {
                lblFileUpload4.Text = "Upload Image";
                Img4.ImageUrl = "../HomeImage/no_image.png";
                lbtnDeleteImage4.Visible = false;

            }
            if (photo_url5.Item(0).InnerXml != "")
            {
                Img5.ImageUrl = "../HomeImage/" + photo_url5.Item(0).InnerXml;
                lblFileUpload5.Text = photo_url5.Item(0).InnerXml;
                lbtnDeleteImage5.Visible = true;
            }
            else
            {
                lblFileUpload5.Text = "Upload Image";
                Img5.ImageUrl = "../HomeImage/no_image.png";
                lbtnDeleteImage5.Visible = false;
            }
            if (photo_url6.Item(0).InnerXml != "")
            {
                Img6.ImageUrl = "../HomeImage/" + photo_url6.Item(0).InnerXml;
                lblFileUpload6.Text = photo_url6.Item(0).InnerXml;
                lbtnDeleteImage6.Visible = true;
            }
            else
            {
                Img6.ImageUrl = "../HomeImage/no_image.png";
                lbtnDeleteImage6.Visible = false;
                lblFileUpload6.Text = "Upload Image";
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminHomePage.aspx-0001", ex.Message.ToString());
        }

    }//M1
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
    public bool checkFileExtention(System.Web.UI.WebControls.FileUpload fuFileUpload)
    {
        try
        {
            bool fileExtensionOK = false;
            string fileExtension;
            decimal size = Math.Round((Convert.ToDecimal(fuFileUpload.PostedFile.ContentLength) / Convert.ToDecimal(1024)), 2);
            if (size > 2048)
            {
                Session["image"] = null;
                Errormsg("File size must not exceed 2MB");
                return false;

            }
            System.Drawing.Image img = System.Drawing.Image.FromStream(fuFileUpload.PostedFile.InputStream);
            int height = img.Height;
            int width = img.Width;
            if (!(height == 640 | width == 1350))
            {
                Session["image"] = null;
                Errormsg("Image Height and Width must be 640px and 1350px");
                return false;

            }
            fileExtension = System.IO.Path.GetExtension(fuFileUpload.FileName).ToLower();
            string[] allowedExtensions = new[] { ".jpg", ".png", ".jpeg" };
            for (int i = 0; i <= allowedExtensions.Length - 1; i++)
            {
                if (fileExtension == allowedExtensions[i])
                    fileExtensionOK = true;
            }
            return fileExtensionOK;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminHomePage.aspx-0002", ex.Message.ToString());
            Errormsg("Something went wrong with image (allow only .jpg, .png, .jpeg image). ");
            return default(Boolean);
        }
    }
    protected void uploadPhoto(string photoId)
    {
        try
        {
              string photoName = "Home" + photoId;
            //  string strFileType = System.IO.Path.GetExtension(Session["image"].ToString());
            //((System.Drawing.Image)Session["image"]).Save(Server.MapPath("~/HomeImage/" + photoName + strFileType));
            //  string path = photoName + strFileType;
            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("../CommonData.xml"));
            if (photoId == "1")
            {
                string strFileType = System.IO.Path.GetExtension(FileUpload1.FileName);
                FileUpload1.SaveAs(Server.MapPath("~/HomeImage/"+ photoName+ strFileType));
                XmlNodeList photo_url = doc.GetElementsByTagName("homepage_img1_url");
                photo_url.Item(0).InnerXml = photoName + strFileType;//"Home1.jpeg";
            }
            else if (photoId == "2")
            {
                string strFileType = System.IO.Path.GetExtension(FileUpload2.FileName);
                FileUpload2.SaveAs(Server.MapPath("~/HomeImage/" + photoName + strFileType));
               // FileUpload2.SaveAs(Server.MapPath("~/HomeImage/Home2.jpg"));
                XmlNodeList photo_url = doc.GetElementsByTagName("homepage_img2_url");
                photo_url.Item(0).InnerXml = photoName + strFileType;
            }
            else if (photoId == "3")
            {
                string strFileType = System.IO.Path.GetExtension(FileUpload3.FileName);
                FileUpload3.SaveAs(Server.MapPath("~/HomeImage/" + photoName + strFileType));
                XmlNodeList photo_url = doc.GetElementsByTagName("homepage_img3_url");
                photo_url.Item(0).InnerXml = photoName + strFileType;
            }
            else if (photoId == "4")
            {
                string strFileType = System.IO.Path.GetExtension(FileUpload4.FileName);
                FileUpload4.SaveAs(Server.MapPath("~/HomeImage/" + photoName + strFileType));
                XmlNodeList photo_url = doc.GetElementsByTagName("homepage_img4_url");
                photo_url.Item(0).InnerXml= photoName + strFileType;
            }
            else if (photoId == "5")
            {
                string strFileType = System.IO.Path.GetExtension(FileUpload5.FileName);
                FileUpload5.SaveAs(Server.MapPath("~/HomeImage/" + photoName + strFileType));
                XmlNodeList photo_url = doc.GetElementsByTagName("homepage_img5_url");
                photo_url.Item(0).InnerXml = photoName + strFileType;
            }
            else if (photoId == "6")
            {
                string strFileType = System.IO.Path.GetExtension(FileUpload6.FileName);
                FileUpload6.SaveAs(Server.MapPath("~/HomeImage/" + photoName + strFileType));
                XmlNodeList photo_url = doc.GetElementsByTagName("homepage_img6_url");
                photo_url.Item(0).InnerXml = photoName + strFileType;
            }
            successmsg("Home Page Background Image Successfully updated");
            doc.Save(Server.MapPath("../CommonData.xml"));

        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminHomePage.aspx-0003", ex.Message.ToString());
        }
    }//M2
    public byte[] PHOTOconvertTObyte(System.Web.UI.WebControls.FileUpload fuFileUpload)

    {
        try
        {
            int intFileLength;
            byte[] byteData = null;

            if (fuFileUpload.HasFile)
            {
                if (checkFileExtention(fuFileUpload) == true)
                {
                    intFileLength = fuFileUpload.PostedFile.ContentLength;
                    byteData = new byte[intFileLength + 1];
                    byteData = fuFileUpload.FileBytes;
                }
                string _fileFormat = GetMimeDataOfFile(fuFileUpload.PostedFile);
                if ((_fileFormat == "image/pjpeg") | (_fileFormat == "image/x-png"))
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
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminHomePage.aspx-0004", ex.Message.ToString());
            return null;
        }
    }
    #endregion

    #region "Events"
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        XmlDocument doc = new XmlDocument();
        doc.Load(Server.MapPath("../CommonData.xml"));
        if (Session["PhotoNo"].ToString() == "1")
        {
            XmlNodeList photo_url1 = doc.GetElementsByTagName("homepage_img1_url");
            if (photo_url1.Item(0).InnerXml != "")
            {
                deletePhoto("../HomeImage/" + photo_url1.Item(0).InnerXml);
                photo_url1.Item(0).InnerXml = "";
                doc.Save(Server.MapPath("../CommonData.xml"));
            }
            lblFileUpload1.Visible = true;
            lblFileUpload1.Text = "Upload Image";
        }
        if (Session["PhotoNo"].ToString() == "2")
        {
            XmlNodeList photo_url2 = doc.GetElementsByTagName("homepage_img2_url");
            if (photo_url2.Item(0).InnerXml != "")
            {
                deletePhoto("../HomeImage/" + photo_url2.Item(0).InnerXml);
                photo_url2.Item(0).InnerXml = "";
                doc.Save(Server.MapPath("../CommonData.xml"));
            }
        }
        if (Session["PhotoNo"].ToString() == "3")
        {
            XmlNodeList photo_url3 = doc.GetElementsByTagName("homepage_img3_url");
            if (photo_url3.Item(0).InnerXml != "")
            {
                deletePhoto("../HomeImage/" + photo_url3.Item(0).InnerXml);
                photo_url3.Item(0).InnerXml = "";
                doc.Save(Server.MapPath("../CommonData.xml"));
            }
           
        }
        if (Session["PhotoNo"].ToString() == "4")
        {
            XmlNodeList photo_url4 = doc.GetElementsByTagName("homepage_img4_url");
            if (photo_url4.Item(0).InnerXml != "")
            {
                deletePhoto("../HomeImage/" + photo_url4.Item(0).InnerXml);
                photo_url4.Item(0).InnerXml = "";
                doc.Save(Server.MapPath("../CommonData.xml"));
            }
            
        }
        if (Session["PhotoNo"].ToString() == "5")
        {
            XmlNodeList photo_url5 = doc.GetElementsByTagName("homepage_img5_url");
            if (photo_url5.Item(0).InnerXml != "")
            {
                deletePhoto("../HomeImage/" + photo_url5.Item(0).InnerXml);
                photo_url5.Item(0).InnerXml = "";
                doc.Save(Server.MapPath("../CommonData.xml"));
            }
           
        }
        if (Session["PhotoNo"].ToString() == "6")
        {
            XmlNodeList photo_url6 = doc.GetElementsByTagName("homepage_img6_url");
            if (photo_url6.Item(0).InnerXml != "")
            {
                deletePhoto("../HomeImage/" + photo_url6.Item(0).InnerXml);
                photo_url6.Item(0).InnerXml = "";
                doc.Save(Server.MapPath("../CommonData.xml"));
            }
           
        }

        LoadPhotos();
        successmsg("Home Page Background Image Successfully Deleted");

    }
    protected void btnUploadImage1_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        checkImage = checkFileExtention(FileUpload1);
        if (checkImage == true)
        {
            Session["image"] = FileUpload1;
            lblFileUpload1.Visible = true;
            lblFileUpload1.Text = FileUpload1.FileName;
            uploadPhoto("1");
            LoadPhotos();
        }
    }
    protected void btnUploadImage2_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        checkImage = checkFileExtention(FileUpload2);
        if (checkImage == true)
        {
            Session["image"] = FileUpload2;
            uploadPhoto("2");
            LoadPhotos();
        }
    }
    protected void btnUploadImage3_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        checkImage = checkFileExtention(FileUpload3);
        if (checkImage == true)
        {
            Session["image"] = FileUpload3;
            uploadPhoto("3");
            LoadPhotos();
        }
    }
    protected void btnUploadImage4_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        checkImage = checkFileExtention(FileUpload4);
        if (checkImage == true)
        {
            Session["image"] = FileUpload4;
            uploadPhoto("4");
            LoadPhotos();
        }
    }
    protected void btnUploadImage5_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        checkImage = checkFileExtention(FileUpload5);
        if (checkImage == true)
        {
            Session["image"] = FileUpload5;
            uploadPhoto("5");
            LoadPhotos();
        }
    }
    protected void btnUploadImage6_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        checkImage = checkFileExtention(FileUpload6);
        if (checkImage == true)
        {
            Session["image"] = FileUpload6;
            uploadPhoto("6");
            LoadPhotos();
        }
    }
    protected void deletePhoto(string photo_url)
    {
        System.IO.File.Delete(Server.MapPath(photo_url));
    }
    protected void lbtnDeleteImage1_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        XmlDocument doc = new XmlDocument();
        doc.Load(Server.MapPath("../CommonData.xml"));
        XmlNodeList photo_url1 = doc.GetElementsByTagName("homepage_img1_url");
        if (photo_url1.Item(0).InnerXml != "")
        {
            Session["PhotoNo"] = "1";
            ConfirmMsg(" Do You Want to Delete Home Page Background Image ?");
        }
        else
        {
            Errormsg("Background image not available");
        }
    }
    protected void lbtnDeleteImage2_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        XmlDocument doc = new XmlDocument();
        doc.Load(Server.MapPath("../CommonData.xml"));
        XmlNodeList photo_url2 = doc.GetElementsByTagName("homepage_img2_url");
        if (photo_url2.Item(0).InnerXml != "")
        {
            Session["PhotoNo"] = "2";
            ConfirmMsg(" Do You Want to Delete Home Page Background Image ?");
        }
        else
            Errormsg("Background image not available");
    }
    protected void lbtnDeleteImage3_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        XmlDocument doc = new XmlDocument();
        doc.Load(Server.MapPath("../CommonData.xml"));
        XmlNodeList photo_url3 = doc.GetElementsByTagName("homepage_img3_url");
        if (photo_url3.Item(0).InnerXml != "")
        {
            Session["PhotoNo"] = "3";
            ConfirmMsg(" Do You Want to Delete Home Page Background Image ?");

           
        }
        else
            Errormsg("Background image not available");
    }
    protected void lbtnDeleteImage4_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        XmlDocument doc = new XmlDocument();
        doc.Load(Server.MapPath("../CommonData.xml"));
        XmlNodeList photo_url4 = doc.GetElementsByTagName("homepage_img4_url");
        if (photo_url4.Item(0).InnerXml != "")
        {
            Session["PhotoNo"] = "4";
            ConfirmMsg(" Do You Want to Delete Home Page Background Image ?");

            //successmsg("Home Page Background Image Successfully Deleted");
            //LoadPhotos();
            //lblFileUpload4.Visible = true;
            //lblFileUpload4.Text = "Upload Image";
        }
        else
            Errormsg("Background image not available");
    }
    protected void lbtnDeleteImage5_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        XmlDocument doc = new XmlDocument();
        doc.Load(Server.MapPath("../CommonData.xml"));
        XmlNodeList photo_url5 = doc.GetElementsByTagName("homepage_img5_url");
        if (photo_url5.Item(0).InnerXml != "")
        {
            Session["PhotoNo"] = "5";
            ConfirmMsg(" Do You Want to Delete Home Page Background Image ?");

            //successmsg("Home Page Background Image Successfully Deleted");
            //LoadPhotos();
            //lblFileUpload5.Visible = true;
            //lblFileUpload5.Text = "Upload Image";
        }
        else
            Errormsg("Background image not available");
    }
    protected void lbtnDeleteImage6_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        XmlDocument doc = new XmlDocument();
        doc.Load(Server.MapPath("../CommonData.xml"));
        XmlNodeList photo_url6 = doc.GetElementsByTagName("homepage_img6_url");
        if (photo_url6.Item(0).InnerXml != "")
        {
            Session["PhotoNo"] = "6";
            ConfirmMsg(" Do You Want to Delete Home Page Background Image ?");

            //successmsg("Home Page Background Image Successfully Deleted");
            //LoadPhotos();
            //lblFileUpload6.Visible = true;
            //lblFileUpload6.Text = "Upload Image";
        }
        else
            Errormsg("Background image not available");
    }
    protected void lbtnHelp_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        string msg = "";
        msg = msg + "1. You can add Maximum 6 Image at a time.<br/>";
        msg = msg + "2. Image size must be less than 2 MB.<br/>";
        msg = msg + "3. Height and Width of Image must be 640 x 1350.<br/>";
        InfoMsg(msg);
    }
    #endregion

}