using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI.WebControls;
using System.Xml;

public partial class Auth_PAdminMentor : BasePage 
{
    

    [System.Runtime.InteropServices.DllImport("urlmon.dll")]
    public static extern int FindMimeFromData(IntPtr pBC, [MarshalAs(UnmanagedType.LPWStr)] string pwzUrl, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1, SizeParamIndex = 3)] byte[] pBuffer, int cbSize, [MarshalAs(UnmanagedType.LPWStr)] string pwzMimeProposed, int dwMimeFlags, [MarshalAs(UnmanagedType.U4)] ref int ppwzMimeOut, int dwReserved);
    private sbCommonFunc _common = new sbCommonFunc();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    sbValidation _validation = new sbValidation();
    bool checkImage;
    private byte[] PhotoImage = null;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            Random randomclass = new Random();
            Session["rndNoCheck"] = randomclass.Next().ToString();
            hidtoken.Value = Session["rndNoCheck"].ToString();
            Session["mentor1image"] = null;
            Session["mentor2image"] = null;
            Session["mentor3image"] = null;
            Session["mentor4image"] = null;
            initsession();
            Session["_moduleName"] = "Home Page Background Images";
            loadMentors();
            Session["Mentor1"] = null;
            Session["RMentor1"] = null;

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
    public string GetImage(object img)
    {
        try
        {
            return "data:image/jpg;base64," + Convert.ToBase64String((byte[])img);
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminMentor.aspx-0001", ex.Message.ToString());
            return null;
        }
    }
    private void initsession()
    {
        Session["Mentor1"] = null;
        Session["RMentor1"] = null;

    }
    protected void loadMentors()
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(Server.MapPath("../CommonData.xml"));
        XmlNodeList mentor1_image = doc.GetElementsByTagName("mentor1image");
        XmlNodeList mentor1_name = doc.GetElementsByTagName("mentor1name");
        XmlNodeList mentor1_LocalLanguage = doc.GetElementsByTagName("mentor1_LocalLanguage");
        XmlNodeList mentor1_designation = doc.GetElementsByTagName("mentor1designation");
        XmlNodeList mentor1_URL = doc.GetElementsByTagName("mentor1_URL");

        if (mentor1_image.Item(0).InnerXml != "")
        {
            imgmentor1img.ImageUrl = "../HomeImage/mentors/" + mentor1_image.Item(0).InnerXml;
            lblmentor1imgname.Text = mentor1_image.Item(0).InnerXml;
            //lblmentor1imgname.Visible = true;
            lbtnremovementor1.Visible = true;
            lblmentor1imgname.Visible = false;
            lbtnResetMentor1.Visible = false;
            lbtnsubmitmentor1.Visible = false;
            FileUploadMentor1.Visible = false;
            tbnameCM.Text = mentor1_name.Item(0).InnerXml;
            tbLocalLngCM.Text = mentor1_LocalLanguage.Item(0).InnerXml;
            tbdesignationCM.Text = mentor1_designation.Item(0).InnerXml;
            tbCMUrl.Text = mentor1_URL.Item(0).InnerXml;
        }
        else
        {
            imgmentor1img.ImageUrl = "../HomeImage/mentors/Nophoto.png";
            lbtnremovementor1.Visible = false;
            lblmentor1imgname.Visible = false;
            FileUploadMentor1.Visible = true;

        }
        XmlNodeList mentor2_image = doc.GetElementsByTagName("mentor2image");
        XmlNodeList mentor2_name = doc.GetElementsByTagName("mentor2name");
        XmlNodeList mentor2_LocalLanguage = doc.GetElementsByTagName("mentor2_LocalLanguage");
        XmlNodeList mentor2_designation = doc.GetElementsByTagName("mentor2designation");
        XmlNodeList mentor2_URL = doc.GetElementsByTagName("mentor2_URL");

        if (mentor2_image.Item(0).InnerXml != "")
        {
            imgmentor2.ImageUrl = "../HomeImage/mentors/" + mentor2_image.Item(0).InnerXml;
            lblmentor2imgname.Text = mentor2_image.Item(0).InnerXml;
            lbtnremovementor2.Visible = true;
            lblmentor2imgname.Visible = false;
            lbtnResetMentor2.Visible = false;
            lbtnsubmitmentor2.Visible = false;
            FUmentor2.Visible = false;
            tbTMmentorName.Text = mentor2_name.Item(0).InnerXml;
            tbMentorLoacalLang.Text = mentor2_LocalLanguage.Item(0).InnerXml;
            tbTMdesignation.Text = mentor2_designation.Item(0).InnerXml;
            tbMentor2URL.Text = mentor2_URL.Item(0).InnerXml;
        }
        else
        {
            imgmentor2.ImageUrl = "../HomeImage/mentors/Nophoto.png";
            lblmentor2imgname.Visible = false;
            lbtnremovementor2.Visible = false;
            FUmentor2.Visible = true;

        }
        XmlNodeList mentor3_image = doc.GetElementsByTagName("mentor3image");
        XmlNodeList mentor3_name = doc.GetElementsByTagName("mentor3name");
        XmlNodeList mentor3_LocalLanguage = doc.GetElementsByTagName("mentor3_LocalLanguage");
        XmlNodeList mentor3_designation = doc.GetElementsByTagName("mentor3designation");
        XmlNodeList mentor3_URL = doc.GetElementsByTagName("mentor3_URL");


        if (mentor3_image.Item(0).InnerXml != "")
        {
            imgMentor3.ImageUrl = "../HomeImage/mentors/" + mentor3_image.Item(0).InnerXml;
            lblmentor3imgname.Text = mentor3_image.Item(0).InnerXml;

            lbtnremovementor3.Visible = true;
            lblmentor2imgname.Visible = false;
            lbtnResetMentor3.Visible = false;
            lbtnsubmitmentor3.Visible = false;
            FuSecretary.Visible = false;
            tbSecretaryName.Text = mentor3_name.Item(0).InnerXml;
            tbSecretaryLocalName.Text = mentor3_LocalLanguage.Item(0).InnerXml;
            tbSecretaryDgn.Text = mentor3_designation.Item(0).InnerXml;
            tbSecURL.Text = mentor3_URL.Item(0).InnerXml;
        }
        else
        {
            imgMentor3.ImageUrl = "../HomeImage/mentors/Nophoto.png";
            lblmentor3imgname.Visible = false;
            lbtnremovementor3.Visible = false;
            FuSecretary.Visible = true;
        }
        XmlNodeList mentor4_image = doc.GetElementsByTagName("mentor4image");
        XmlNodeList mentor4_name = doc.GetElementsByTagName("mentor4name");
        XmlNodeList mentor4_LocalLanguage = doc.GetElementsByTagName("mentor4_LocalLanguage");
        XmlNodeList mentor4_designation = doc.GetElementsByTagName("mentor4designation");
        XmlNodeList mentor4_URL = doc.GetElementsByTagName("mentor4_URL");


        if (mentor4_image.Item(0).InnerXml != "")
        {
            imgmentor4.ImageUrl = "../HomeImage/mentors/" + mentor4_image.Item(0).InnerXml;
            lblmentor4imgname.Text = mentor4_image.Item(0).InnerXml;
            //lblmentor4imgname.Visible = true;
            //ltnremovementor4.Visible = true;
            lbtnremovementor4.Visible = true;
            lblmentor4imgname.Visible = false;
            lbtnResetMentor4.Visible = false;
            lbtnsubmitmentor4.Visible = false;
            FuMDirector.Visible = false;
            tbMDirectorName.Text = mentor4_name.Item(0).InnerXml;
            tbMDirectorLocalLang.Text = mentor4_LocalLanguage.Item(0).InnerXml;
            tbMDirectorDgn.Text = mentor4_designation.Item(0).InnerXml;
            tbDirectorURL.Text = mentor4_URL.Item(0).InnerXml;
        }
        else
        {
            imgmentor4.ImageUrl = "../HomeImage/mentors/Nophoto.png";
            lblmentor4imgname.Visible = false;
            lbtnremovementor4.Visible = false;
            FuMDirector.Visible = true;
        }

    }
    public byte[] convertByteFile(System.Web.UI.WebControls.FileUpload fuFileUpload)
    {
        int intFileLength;
        byte[] byteData;
        intFileLength = fuFileUpload.PostedFile.ContentLength;
        byteData = new byte[intFileLength + 1];
        byteData = fuFileUpload.FileBytes;
        return byteData;
    }
    private void SaveMentor(string photoName, FileUpload image, string mentor)//M1
    {
        try
        {
            string strFileType = System.IO.Path.GetExtension(image.FileName).ToString().ToLower();
            string strPath = Server.MapPath("../HomeImage/mentors/" + photoName + strFileType);
            //image.SaveAs(strPath);

            string path = photoName + strFileType;
            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("../CommonData.xml"));
            if (strFileType != "")
            {
                XmlNodeList photo_url = null/* TODO Change to default(_) if this is not a reference type */;
                XmlNodeList mentor1_name = null/* TODO Change to default(_) if this is not a reference type */;
                XmlNodeList mentor1_LocalLanguage = null/* TODO Change to default(_) if this is not a reference type */;
                XmlNodeList mentor1_designation = null/* TODO Change to default(_) if this is not a reference type */;
                XmlNodeList mentor1_URL = null/* TODO Change to default(_) if this is not a reference type */;
                if (Session["Mentor1"].ToString() == "1")
                {
                    image.SaveAs(strPath);
                    photo_url = doc.GetElementsByTagName("mentor1image");
                    mentor1_name = doc.GetElementsByTagName("mentor1name");
                    mentor1_LocalLanguage = doc.GetElementsByTagName("mentor1_LocalLanguage");
                    mentor1_designation = doc.GetElementsByTagName("mentor1designation");
                    mentor1_URL = doc.GetElementsByTagName("mentor1_URL");
                    photo_url.Item(0).InnerXml = path;
                    mentor1_name.Item(0).InnerXml = tbnameCM.Text;
                    mentor1_LocalLanguage.Item(0).InnerXml = tbLocalLngCM.Text;
                    mentor1_designation.Item(0).InnerXml = tbdesignationCM.Text;
                    mentor1_URL.Item(0).InnerXml = tbCMUrl.Text;
                }
                if (Session["Mentor1"].ToString() == "2")
                {
                    image.SaveAs(strPath);
                    photo_url = doc.GetElementsByTagName("mentor2image");
                    mentor1_name = doc.GetElementsByTagName("mentor2name");
                    mentor1_LocalLanguage = doc.GetElementsByTagName("mentor2_LocalLanguage");
                    mentor1_designation = doc.GetElementsByTagName("mentor2designation");
                    mentor1_URL = doc.GetElementsByTagName("mentor2_URL");
                    photo_url.Item(0).InnerXml = path;
                    mentor1_name.Item(0).InnerXml = tbTMmentorName.Text;
                    mentor1_LocalLanguage.Item(0).InnerXml = tbMentorLoacalLang.Text;
                    mentor1_designation.Item(0).InnerXml = tbTMdesignation.Text;
                    mentor1_URL.Item(0).InnerXml = tbMentor2URL.Text;
                }
                if (Session["Mentor1"].ToString() == "3")
                {
                    image.SaveAs(strPath);
                    photo_url = doc.GetElementsByTagName("mentor3image");
                    mentor1_name = doc.GetElementsByTagName("mentor3name");
                    mentor1_LocalLanguage = doc.GetElementsByTagName("mentor3_LocalLanguage");
                    mentor1_designation = doc.GetElementsByTagName("mentor3designation");
                    mentor1_URL = doc.GetElementsByTagName("mentor3_URL");
                    photo_url.Item(0).InnerXml = path;
                    mentor1_name.Item(0).InnerXml = tbSecretaryName.Text;
                    mentor1_LocalLanguage.Item(0).InnerXml = tbSecretaryLocalName.Text;
                    mentor1_designation.Item(0).InnerXml = tbSecretaryDgn.Text;
                    mentor1_URL.Item(0).InnerXml = tbSecURL.Text;
                }
                if (Session["Mentor1"].ToString() == "4")
                {
                    image.SaveAs(strPath);
                    photo_url = doc.GetElementsByTagName("mentor4image");
                    mentor1_name = doc.GetElementsByTagName("mentor4name");
                    mentor1_LocalLanguage = doc.GetElementsByTagName("mentor4_LocalLanguage");
                    mentor1_designation = doc.GetElementsByTagName("mentor4designation");
                    mentor1_URL = doc.GetElementsByTagName("mentor4_URL");
                    photo_url.Item(0).InnerXml = path;
                    mentor1_name.Item(0).InnerXml = tbMDirectorName.Text;
                    mentor1_LocalLanguage.Item(0).InnerXml = tbMDirectorLocalLang.Text;
                    mentor1_designation.Item(0).InnerXml = tbMDirectorDgn.Text;
                    mentor1_URL.Item(0).InnerXml = tbDirectorURL.Text;
                }
            }
            successmsg(mentor + " Photo and Information Successfully updated");
            doc.Save(Server.MapPath("../CommonData.xml"));
            loadMentors();
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminMentor.aspx-0002", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
        }
    }
    private void removementor(string photoName)//M2
    {
        try
        {

            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("../CommonData.xml"));

            XmlNodeList photo_url = null/* TODO Change to default(_) if this is not a reference type */;
            XmlNodeList mentor_name = null/* TODO Change to default(_) if this is not a reference type */;
            XmlNodeList mentor_LocalLanguage = null/* TODO Change to default(_) if this is not a reference type */;
            XmlNodeList mentor_designation = null/* TODO Change to default(_) if this is not a reference type */;
            XmlNodeList mentor_URL = null/* TODO Change to default(_) if this is not a reference type */;
            if (Session["RMentor1"].ToString() == "1")
            {

                photo_url = doc.GetElementsByTagName("mentor1image");
                mentor_name = doc.GetElementsByTagName("mentor1name");
                mentor_LocalLanguage = doc.GetElementsByTagName("mentor1_LocalLanguage");
                mentor_designation = doc.GetElementsByTagName("mentor1designation");
                mentor_URL = doc.GetElementsByTagName("mentor1_URL");
                tbnameCM.Text = "";
                tbLocalLngCM.Text = "";
                tbdesignationCM.Text = "";
                tbCMUrl.Text = "";

            }
            if (Session["RMentor1"].ToString() == "2")
            {
                photo_url = doc.GetElementsByTagName("mentor2image");
                mentor_name = doc.GetElementsByTagName("mentor2name");
                mentor_LocalLanguage = doc.GetElementsByTagName("mentor2_LocalLanguage");
                mentor_designation = doc.GetElementsByTagName("mentor2designation");
                mentor_URL = doc.GetElementsByTagName("mentor2_URL");

                tbTMmentorName.Text = "";
                tbMentorLoacalLang.Text = "";
                tbTMdesignation.Text = "";
                tbMentor2URL.Text = "";
            }
            if (Session["RMentor1"].ToString() == "3")
            {
                photo_url = doc.GetElementsByTagName("mentor3image");
                mentor_name = doc.GetElementsByTagName("mentor3name");
                mentor_LocalLanguage = doc.GetElementsByTagName("mentor3_LocalLanguage");
                mentor_designation = doc.GetElementsByTagName("mentor3designation");
                mentor_URL = doc.GetElementsByTagName("mentor3_URL");

                tbSecretaryName.Text = "";
                tbSecretaryLocalName.Text = "";
                tbSecretaryDgn.Text = "";
                tbSecURL.Text = "";
            }
            if (Session["RMentor1"].ToString() == "4")
            {
                photo_url = doc.GetElementsByTagName("mentor4image");
                mentor_name = doc.GetElementsByTagName("mentor4name");
                mentor_LocalLanguage = doc.GetElementsByTagName("mentor4_LocalLanguage");
                mentor_designation = doc.GetElementsByTagName("mentor4designation");
                mentor_URL = doc.GetElementsByTagName("mentor4_URL");

                tbMDirectorName.Text = "";
                tbMDirectorLocalLang.Text = "";
                tbMDirectorDgn.Text = "";
                tbDirectorURL.Text = "";
            }
            photo_url.Item(0).InnerXml = "";
            mentor_name.Item(0).InnerXml = "";
            mentor_LocalLanguage.Item(0).InnerXml = "";
            mentor_designation.Item(0).InnerXml = "";
            mentor_URL.Item(0).InnerXml = "";

            string path = Server.MapPath("../HomeImage/mentors/" + photoName );
      //      string path = Server.MapPath("../HomeImage/mentors/" + lblmentor1imgname.Text);
            FileInfo file = new FileInfo(path);
            if (file.Exists)
            {
                file.Delete();
                if (Session["RMentor1"].ToString() == "1")
                {
                    successmsg(" Mentor1 Photo and Information Successfully deleted ");
                }
                if (Session["RMentor1"].ToString() == "2")
                {
                    successmsg(" Mentor 2 Photo and Information Successfully deleted ");
                }
                if (Session["RMentor1"].ToString() == "3")
                {
                    successmsg(" Mentor 3 Photo and Information Successfully deleted ");
                }
                if (Session["RMentor1"].ToString() == "4")
                {
                    successmsg(" Mentor 4 Photo and Information Successfully deleted ");
                }
            }
            else
            {
                Errormsg(" Mentor1 Photo and Information does not exists ");
                return;
            }
            doc.Save(Server.MapPath("../CommonData.xml"));
            loadMentors();
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminMentor.aspx-0003", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
        }
    }
    public bool checkFileExtentionMentor(System.Web.UI.WebControls.FileUpload fuFileUpload)//M3
    {
        try
        {
            bool fileExtensionOK = false;
            string fileExtension;
            decimal size = Math.Round((Convert.ToDecimal(fuFileUpload.PostedFile.ContentLength) / Convert.ToDecimal(1024)), 2);
            if (size > 50)
            {
                Session["image"] = "";
                Errormsg("File size must not exceed 50 KB");
                return false;
            }
            System.Drawing.Image img = System.Drawing.Image.FromStream(fuFileUpload.PostedFile.InputStream);
            int height = img.Height;
            int width = img.Width;
            //if (!(height == 400 | width == 504))
            //{
            //    Session["image"] = "";
            //    Errormsg("Image Height and Width must be 400px and 504px.");
            //    return false;
            //}
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
           
            _common.ErrorLog("PAdminMentor.aspx-0004", ex.Message.ToString());
            Errormsg("Something went wrong with image (allow only .jpg, .png, .jpeg image). ");
            return false;
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
    public void confirmmsg(string message)
    {
        lblConfirmation.Text = message;
        mpConfirmation.Show();
    }
    #endregion

    #region "Events"
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)//E1
    {
        CsrfTokenValidate();
        try
        {
            if (Session["Mentor1"] != null)
            {
                if (Session["Mentor1"].ToString() == "1")
                {
                    string photoName = "Mentor_1";
                    SaveMentor(photoName, (FileUpload)Session["mentor1image"], "Chief Minister");

                }
                if (Session["Mentor1"].ToString() == "2")
                {
                    string photoName = "Mentor_2";
                    SaveMentor(photoName, (FileUpload)Session["mentor2image"], "Transport Minister");

                }
                if (Session["Mentor1"].ToString() == "3")
                {
                    string photoName = "Mentor_3";
                    SaveMentor(photoName, (FileUpload)Session["mentor3image"], "Secretary");

                }
                if (Session["Mentor1"].ToString() == "4")
                {
                    string photoName = "Mentor_4";
                    SaveMentor(photoName, (FileUpload)Session["mentor4image"], "Managing Director");

                }
            }

            if (Session["RMentor1"] != null)
            {
                if (Session["RMentor1"].ToString() == "1")
                {
                  //  string photoName = "Mentor_1";
                    removementor(lblmentor1imgname.Text);

                }
                if (Session["RMentor1"].ToString() == "2")
                {
                  //  string photoName = "Mentor_2";
                    removementor(lblmentor2imgname.Text);

                }
                if (Session["RMentor1"].ToString() == "3")
                {
                 //   string photoName = "Mentor_3";
                    removementor(lblmentor3imgname.Text);

                }
                if (Session["RMentor1"].ToString() == "4")
                {
                   // string photoName = "Mentor_4";
                    removementor(lblmentor4imgname.Text);

                }
            }

        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminMentor.aspx-0005", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
        }

    }
    protected void lbtnsubmitmentor1_Click(object sender, EventArgs e)//E2
    {
        CsrfTokenValidate();
        try
        {
            string msg = "";
            int msgcnt = 0;
            if (tbnameCM.Text == "")
            {
                msgcnt = msgcnt + 1;
                msg = msg + msgcnt.ToString() + ". Chief Minister Name.<br>";
            }
            if (tbLocalLngCM.Text == "")
            {
                msgcnt = msgcnt + 1;
                msg = msg + msgcnt.ToString() + ". Chief Minister Local Language Name.<br>";

            }
            if (tbdesignationCM.Text == "")
            {
                msgcnt = msgcnt + 1;
                msg = msg + msgcnt.ToString() + ". Designation (ex: Chief Minister,UK).<br>";

            }
            if (tbCMUrl.Text != "")
            {
                if (_validation.isValidURL(tbCMUrl.Text) == false)
                {
                    msgcnt = msgcnt + 1;
                    msg = msg + msgcnt + ". Enter Valid URL. <br/>";
                }
            }
            if (msgcnt > 0)
            {
                Errormsg(msg);
                return;
            }

            Session["Mentor1"] = "1";
            Session["RMentor1"] = null;
            confirmmsg("Do you want Update  Chief Minister Photo and information?");

        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminMentor.aspx-0006", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
        }
    }
    protected void lbtnsubmitmentor2_Click(object sender, EventArgs e)//E3
    {
        CsrfTokenValidate();
        try
        {
            string msg = "";
            int msgcnt = 0;
            if (tbTMmentorName.Text == "")
            {
                msgcnt = msgcnt + 1;
                msg = msg + msgcnt.ToString() + ". Transport Minister Name.<br>";
            }
            if (tbMentorLoacalLang.Text == "")
            {
                msgcnt = msgcnt + 1;
                msg = msg + msgcnt.ToString() + ". Transport Minister Local Language Name.<br>";
            }
            if (tbTMdesignation.Text == "")
            {
                msgcnt = msgcnt + 1;
                msg = msg + msgcnt.ToString() + ". Designation (ex: Transport Minister,UK).<br>";
            }
            if (tbMentor2URL.Text != "")
            {
                if (_validation.isValidURL(tbMentor2URL.Text) == false)
                {
                    msgcnt = msgcnt + 1;
                    msg = msg + msgcnt + ". Enter Valid URL. <br/>";
                }
            }

            if (msgcnt > 0)
            {
                Errormsg(msg);
                return;
            }
            Session["Mentor1"] = "2";
            Session["RMentor1"] = null;
            confirmmsg("Do you want Update Transport Minister Photo and information?");

        }

        catch (Exception ex)
        {
            _common.ErrorLog("PAdminMentor.aspx-0007", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
        }
    }
    protected void lbtnsubmitmentor3_Click(object sender, EventArgs e)//E4
    {
        CsrfTokenValidate();
        try
        {
            string msg = "";
            int msgcnt = 0;
            if (tbSecretaryName.Text == "")
            {
                msgcnt = msgcnt + 1;
                msg = msg + msgcnt.ToString() + ". Enter Secretary Name.<br>";
            }
            if (tbSecretaryLocalName.Text == "")
            {
                msgcnt = msgcnt + 1;
                msg = msg + msgcnt.ToString() + ". Enter Secretary Local  Language Name.<br>";
            }
            if (tbSecretaryDgn.Text == "")
            {
                msgcnt = msgcnt + 1;
                msg = msg + msgcnt.ToString() + ". Enter Designation (ex: Secretary,Starbus).<br>";
            }
            if (tbSecURL.Text != "")
            {
                if (_validation.isValidURL(tbSecURL.Text) == false)
                {
                    msgcnt = msgcnt + 1;
                    msg = msg + msgcnt + ". Enter Valid URL. <br/>";
                }
            }
            if (msgcnt > 0)
            {
                Errormsg(msg);
                return;
            }
            Session["Mentor1"] = "3";
            Session["RMentor1"] = null;
            confirmmsg("Do you want Update Secretary Photo and information?");

        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminMentor.aspx-0008", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
        }
    }
    protected void lbtnsubmitmentor4_Click(object sender, EventArgs e)//E5
    {
        CsrfTokenValidate();
        try
        {
            string msg = "";
            int msgcnt = 0;
            if (tbMDirectorName.Text == "")
            {
                msgcnt = msgcnt + 1;
                msg = msg + msgcnt.ToString() + ". Enter Managing Director Name.<br>";
            }
            if (tbMDirectorLocalLang.Text == "")
            {
                msgcnt = msgcnt + 1;
                msg = msg + msgcnt.ToString() + ". Enter Managing Director Local Language Name.<br>";
            }
            if (tbMDirectorDgn.Text == "")
            {
                msgcnt = msgcnt + 1;
                msg = msg + msgcnt.ToString() + ". Enter Designation (ex: Managing Director,Starbus).<br>";
            }
            if (tbDirectorURL.Text != "")
            {
                if (_validation.isValidURL(tbDirectorURL.Text) == false)
                {
                    msgcnt = msgcnt + 1;
                    msg = msg + msgcnt + ". Enter Valid URL. <br/>";
                }
            }
            Session["Mentor1"] = "4";
            Session["RMentor1"] = null;
            confirmmsg("Do you want Update Managing Director Photo and information?");

        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminMentor.aspx-0009", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
        }
    }
    protected void lbtnremovementor1_Click(object sender, EventArgs e)//E6
    {
        CsrfTokenValidate();
        try
        {
            Session["RMentor1"] = "1";
            Session["Mentor1"] = null;
            confirmmsg("Do you want Remove Chief Minister Photo and information ? ");

        }

        catch (Exception ex)
        {
            _common.ErrorLog("PAdminMentor.aspx-0010", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
        }
    }
    protected void lbtnremovementor2_Click(object sender, EventArgs e)//E7
    {
        CsrfTokenValidate();
        try
        {
            Session["RMentor1"] = "2";
            Session["Mentor1"] = null;
            confirmmsg("Do you want Remove Transport Minister Photo and information?");

        }

        catch (Exception ex)
        {
            _common.ErrorLog("PAdminMentor.aspx-0011", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
        }
    }
    protected void ltnremovementor3_Click(object sender, EventArgs e)//E8
    {
        CsrfTokenValidate();
        try
        {
            Session["RMentor1"] = "3";
            Session["Mentor1"] = null;
            confirmmsg("Do you want Remove Secretary Photo and information?");
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminMentor.aspx-0012", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
        }
    }
    protected void ltnremovementor4_Click(object sender, EventArgs e)//E9
    {
        CsrfTokenValidate();
        try
        {
            Session["RMentor1"] = "4";
            Session["Mentor1"] = null;
            confirmmsg("Do you want Remove Managing Director Photo and information?");

        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminMentor.aspx-0013", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
        }
    }
    protected void btnmentor1_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        checkImage = checkFileExtentionMentor(FileUploadMentor1);
        if (checkImage == true)
        {
            PhotoImage = convertByteFile(FileUploadMentor1);
            imgmentor1img.ImageUrl = GetImage(PhotoImage);
            lblmentor1imgname.Text = FileUploadMentor1.FileName;
            lbtnsubmitmentor1.Visible = true;
            lbtnResetMentor1.Visible = true;
            Session["mentor1image"] = FileUploadMentor1;

        }
    }
    protected void btnmentor2_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        checkImage = checkFileExtentionMentor(FUmentor2);
        if (checkImage == true)
        {
            Session["mentor2image"] = FUmentor2;
            PhotoImage = convertByteFile(FUmentor2);
            imgmentor2.ImageUrl = GetImage(PhotoImage);
            lblmentor2imgname.Text = FUmentor2.FileName;
            lbtnsubmitmentor2.Visible = true;
            lbtnResetMentor2.Visible = true;

        }
    }
    protected void btnmentor3_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        checkImage = checkFileExtentionMentor(FuSecretary);
        if (checkImage == true)
        {
            Session["mentor3image"] = FuSecretary;
            PhotoImage = convertByteFile(FuSecretary);
            imgMentor3.ImageUrl = GetImage(PhotoImage);
            lblmentor3imgname.Text = FuSecretary.FileName;
            lbtnsubmitmentor3.Visible = true;
            lbtnResetMentor3.Visible = true;

        }
    }
    protected void btnmentor4_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        checkImage = checkFileExtentionMentor(FuMDirector);
        if (checkImage == true)
        {
            Session["mentor4image"] = FuMDirector;
            PhotoImage = convertByteFile(FuMDirector);
            imgmentor4.ImageUrl = GetImage(PhotoImage);
            lblmentor4imgname.Text = FuMDirector.FileName;
            lbtnsubmitmentor4.Visible = true;
            lbtnResetMentor4.Visible = true;
        }
    }
    protected void lbtnHelp_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Errormsg("Comming Soon");
    }
    protected void lbtnResetMentor1_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        tbnameCM.Text = "";
        tbLocalLngCM.Text = "";
        tbdesignationCM.Text = "";
        tbCMUrl.Text = "";
        Session["mentor1image"] = null;
        Session["mentor1image"] = null;

    }
    protected void lbtnResetMentor2_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        tbTMmentorName.Text = "";
        tbMentorLoacalLang.Text = "";
        tbTMdesignation.Text = "";
        tbMentor2URL.Text = "";
        Session["mentor2image"] = null;
    }
    protected void lbtnResetMentor3_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        tbSecretaryName.Text = "";
        tbSecretaryLocalName.Text = "";
        tbSecretaryDgn.Text = "";
        tbSecURL.Text = "";
        Session["mentor3image"] = null;
    }
    protected void lbtnMentor4_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        tbMDirectorName.Text = "";
        tbMDirectorLocalLang.Text = "";
        tbMDirectorDgn.Text = "";
        tbDirectorURL.Text = "";
        Session["mentor4image"] = null;
    }
    #endregion


}