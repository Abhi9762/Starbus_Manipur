using System;
using System.Xml;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Data;
using Npgsql;
using System.IO;
using System.Xml.XPath;

public partial class Auth_PAdminPhotoGallery : BasePage
{
    XmlDocument doc = new XmlDocument();
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    DataTable MyTable = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    [System.Runtime.InteropServices.DllImport("urlmon.dll")]
    public static extern int FindMimeFromData(IntPtr pBC, [MarshalAs(UnmanagedType.LPWStr)] string pwzUrl, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1, SizeParamIndex = 3)] byte[] pBuffer, int cbSize, [MarshalAs(UnmanagedType.LPWStr)] string pwzMimeProposed, int dwMimeFlags, [MarshalAs(UnmanagedType.U4)] ref int ppwzMimeOut, int dwReserved);
    byte[] PhotoImage = null;


    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {

            Random randomclass = new Random();
            Session["rndNoCheck"] = randomclass.Next().ToString();
            hidtoken.Value = Session["rndNoCheck"].ToString();
            Session["_moduleName"] = "Photo Gallery images/Category";
            loadCategoryData();
            loadPhotoData();
            //  updateImageXmlCategoryNew(2,"Holi","HoliHindi");

        }
        if (pnlphoto.Visible == true)
        {

            if ((Session["fuAttach1"] == null) && (FileUploadImage.HasFile))
            { Session["fuAttach1"] = FileUploadImage; }
            else if ((Session["fuAttach1"] != null) && (FileUploadImage.HasFile == false))
            {

                FileUploadImage = ((FileUpload)(Session["fuAttach1"]));

            }
            else if (FileUploadImage.HasFile)
            {
                Session["fuAttach1"] = FileUploadImage;
                lbtnfuAttach1Clear.Visible = true;
              //  lblfuFileName.Visible = true;
              //  lblfuFileName.Text= FileUploadImage.FileName;
            }

        }


    }


    #region "Common Methods"
    private void CsrfTokenValidate()
    {
        if (hidtoken.Value != Session["rndNoCheck"].ToString())
        {
            Response.Redirect("errorpage.aspx");
            return;
        }
    }
    protected void lbtnCtgry_Click(object sender, EventArgs e)
    {
        pnlctgryHeading.Visible = true;
        pnlphotoHeading.Visible = false;
        pnlctgry.Visible = true;
        pnlphoto.Visible = false;
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
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        LinkButton btn = (LinkButton)(sender);
        string yourValue = btn.CommandArgument;
        switch (yourValue)
        {
            case "cmdSaveCategory":
                saveCategory();
                break;
            case "cmdDelCategory":
                deleteCategory();
                break;
            case "cmdSaveImage":
                saveImages();
                break;
            case "cmdDelImage":
                DeletePhoto();
                break;

        }
        //if (Session["Action"].ToString() == "S")
        //{
        //    Session["id"] = "0";
        //    saveCategory();
        //}
        //if (Session["Action"].ToString() == "U")
        //{
        //    saveCategory();
        //}
        //if (Session["Action"].ToString() == "P")
        //{
        //    saveImages();
        //}
        //if (Session["Action"].ToString() == "D")
        //{
        //    DeletePhoto();
        //}
    }
    #endregion

    #region "Category"
    private void loadCategoryData()//M3
    {
        try
        {
            pnlCategoryList.Visible = true;
            gvCategoryList.Visible = false;
            MyCommand = new NpgsqlCommand();
            DataTable dt = new DataTable();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_photocategory");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlCategoryName.Items.Clear();
                    ddlCategoryName.DataSource = dt;
                    ddlCategoryName.DataTextField = "category_name";
                    ddlCategoryName.DataValueField = "category_id";
                    ddlCategoryName.DataBind();
                    ddlCategoryName.Items.Insert(0, "SELECT");
                    ddlCategoryName.Items[0].Value = "0";
                    ddlCategoryName.SelectedIndex = 0;



                    ddlImageList.Items.Clear();
                    ddlImageList.DataSource = dt;
                    ddlImageList.DataTextField = "category_name";
                    ddlImageList.DataValueField = "category_id";
                    ddlImageList.DataBind();
                    ddlImageList.Items.Insert(0, "All");
                    ddlImageList.Items[0].Value = "0";
                    ddlImageList.SelectedIndex = 0;


                    gvCategoryList.DataSource = dt;
                    gvCategoryList.DataBind();
                    pnlCategoryList.Visible = false;
                    gvCategoryList.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            pnlCategoryList.Visible = false;
            gvCategoryList.Visible = true;
            _common.ErrorLog("PAdminPhotoGallery.aspx-0001", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
        }
    }
    private bool validValue()//M1
    {
        try
        {
            int msgcnt = 0;
            string msg = "";
            if (_validation.IsValidString(tbCategoryName.Text, 3, tbCategoryName.MaxLength) == false)
            {
                msgcnt = msgcnt + 1;
                msg = msg + msgcnt.ToString() + ". Category Name.<br>";
            }
            if (_validation.IsValidString(tbCategoryNameLocal.Text, 3, tbCategoryNameLocal.MaxLength) == false)
            {
                msgcnt = msgcnt + 1;
                msg = msg + msgcnt.ToString() + ". Category Name(Local).<br>";
            }

            if (msgcnt > 0)
            {
                Errormsg(msg);
            }
            return true;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminPhotoGallery-M1", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
            return false;
        }

    }
    protected void lbtnSaveCategory_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (validValue() == false)
        {
            return;
        }
        lbtnYesConfirmation.CommandArgument = "cmdSaveCategory";
        confirmmsg("Do you want to save this Category?");

    }
    private void saveCategory()//M2
    {
        try
        {
            string IPAddress = HttpContext.Current.Request.UserHostAddress;
            Int16 categoryId;
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_insert_photocategory");
            MyCommand.Parameters.AddWithValue("p_categoryname", tbCategoryName.Text);
            MyCommand.Parameters.AddWithValue("p_categoryname_local", tbCategoryNameLocal.Text);
            MyCommand.Parameters.AddWithValue("p_updatedby", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("p_ipaddress", IPAddress);
            MyTable = bll.SelectAll(MyCommand);
            if (MyTable.Rows.Count > 0)
            {
                categoryId = Int16.Parse(MyTable.Rows[0][0].ToString());
                successmsg("Congratulations, Photo Gallary Category has successfully  been saved");
                XML_insertCategory(categoryId, tbCategoryName.Text, tbCategoryNameLocal.Text);

                loadCategoryData();
                ResetCategory();
            }
            else
            {
                _common.ErrorLog("PAdminPhotoGallery.aspx-0002", MyTable.TableName);
                Errormsg("Error Occured!");
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminPhotoGallery.aspx-0003", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
        }
    }
    private void XML_insertCategory(Int16 categoryID, string categoryNameEng, string categoryNameHindi)//M4
    {
        try
        {
            //string path = @"..\\CommonDataPhoto.xml";
            string path = Server.MapPath("../CommonDataPhoto.xml");
            if (!File.Exists(path))
            {

                XDocument document = new XDocument
                (
                    new XDeclaration("1.0", "utf-8", "yes"),
                    new XComment("XML for Photogallary"),
                    new XElement("Categories",
                    new XElement("Category",
                        new XAttribute("Id", categoryID),
                        new XElement("NameEng", categoryNameEng),
                        new XElement("NameLocal", categoryNameHindi)

                        ))
                );
                document.Save(Server.MapPath("../CommonDataPhoto.xml"));
            }
            else

            {
                XDocument xDoc = XDocument.Load(path);
                XElement PhotographsXle = xDoc.XPathSelectElement("Categories");
                XElement name = new XElement("Category",
                    new XAttribute("Id", categoryID),
                    new XElement("NameEng", categoryNameEng),
                    new XElement("NameLocal", categoryNameHindi)

                    );

                PhotographsXle.Add(name);
                xDoc.Save(Server.MapPath("../CommonDataPhoto.xml"));

            }



        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminPhotoGallery.aspx-0004", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
        }
    }
    private void ResetCategory()
    {
        lbtnSaveCategory.Visible = true;
        lbtnPStatusreset.Visible = true;

        lblSaveCategory.Visible = true;
        lblUpdateCategory.Visible = false;
        tbCategoryName.Text = "";
        tbCategoryNameLocal.Text = "";

    }
    protected void gvCategoryList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvCategoryList.PageIndex = e.NewPageIndex;
        loadCategoryData();
    }
    protected void gvCategoryList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        CsrfTokenValidate();
        if (e.CommandName == "gvEdit")
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int i = row.RowIndex;
            string id = gvCategoryList.DataKeys[row.RowIndex]["category_id"].ToString();
            Session["_categoryID"] = id;
            Int16 photocnt;
            //--------------------Check if photographs are there----------------------
            NpgsqlCommand MyCommand = new NpgsqlCommand();
  
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_ifexists_photos_of_category");
            MyCommand.Parameters.AddWithValue("p_category_id", id);

            MyTable = bll.SelectAll(MyCommand);
            if (MyTable.Rows.Count > 0)
            {
                photocnt = Int16.Parse(MyTable.Rows[0][0].ToString());
                if (photocnt > 0)
                {
                    Errormsg("Sorry, photographs of this category exists, catgeory having photograph can not be removed"); return;
                }
            }
            //---------------------------------------------------------
            lbtnYesConfirmation.CommandArgument = "cmdDelCategory";
            confirmmsg("Do you want to delete this Category?");

        }
    }
    void deleteCategory()
    {
        try
        {
            Int16 categoryID;
            categoryID = Int16.Parse(Session["_categoryID"].ToString());
            string IPAddress = HttpContext.Current.Request.UserHostAddress;







            //-----------------------------------------------------------------------

            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_delete_photocategory");
            MyCommand.Parameters.AddWithValue("p_category_id", categoryID);
            MyCommand.Parameters.AddWithValue("p_updatedby", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("p_ipaddress", IPAddress);
            string result = bll.UpdateAll(MyCommand);
            if (result == "Success")
            {
                removeXMLCategory(categoryID);
                loadCategoryData();
                successmsg("Photo Gallary Category has successfully been deleted");
            }
            else
            {
                _common.ErrorLog("PAdminPhotoGallery.aspx-0005", result);
                Errormsg("Sorry error occcured, while deleting");
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminPhotoGallery.aspx-0006", ex.Message.ToString());
            Errormsg(ex.Message.ToString());


        }



    }

    void removeXMLCategory(Int16 categoryID)
    {
        //Method 1
        //string path1 = Server.MapPath("../CommonDataPhoto.xml");
        //XDocument xDoc = XDocument.Load(path1);
        //xDoc.Descendants("Category").Where(p => p.Attribute("Id").Value == "1").FirstOrDefault().Remove();
        //xDoc.Save(Server.MapPath("../CommonDataPhoto.xml"));

        //Method 2
        XmlDocument xDoc = new XmlDocument();
        xDoc.Load(Server.MapPath("../CommonDataPhoto.xml"));
        XmlNode CategotyNode = xDoc.SelectSingleNode("//Category[@Id='"+categoryID+"']") as XmlNode;
        if (CategotyNode != null)
        {
            CategotyNode.ParentNode.RemoveChild(CategotyNode);
            xDoc.Save(Server.MapPath("../CommonDataPhoto.xml"));
        }
    }
    void removeXMLPhoto(Int16 categoryID, Int16 photoID )
    {
        //Method 1
        //string path1 = Server.MapPath("../CommonDataPhoto.xml");
        //XDocument xDoc = XDocument.Load(path1);
        //xDoc.Descendants("Category").Where(p => p.Attribute("Id").Value == "1").FirstOrDefault().Remove();
        //xDoc.Save(Server.MapPath("../CommonDataPhoto.xml"));

        //Method 2
        XmlDocument xDoc = new XmlDocument();
        
        
        xDoc.Load(Server.MapPath("../CommonDataPhoto.xml"));
        XmlNode CategotyNode = xDoc.SelectSingleNode("//Category[@Id='"+ categoryID.ToString()+"']//Photo[@Id='"+ photoID.ToString()+"']") as XmlNode;
        if (CategotyNode != null)
        {
            CategotyNode.ParentNode.RemoveChild(CategotyNode);
            xDoc.Save(Server.MapPath("../CommonDataPhoto.xml"));
        }
    }
    protected void lbtnCategoryhelp_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Errormsg("You can add Maximum 8 photos in a Category.");

    }
    protected void lbtnPStatusreset_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        ResetCategory();
    }
    #endregion

    #region "Photographs"

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
    bool isValidFile(System.Web.UI.WebControls.FileUpload fuFileUpload)
    {
        string _fileFormat = GetMimeDataOfFile(fuFileUpload.PostedFile);
        //if (!(_fileFormat == "image/jpeg") | (_fileFormat == "image/x-png") | (_fileFormat == "image/jpg") | (_fileFormat == "image/png"))
        //{
        //    return false;

        //}
        decimal filesize = Math.Round((Convert.ToDecimal(fuFileUpload.PostedFile.ContentLength) / Convert.ToDecimal(1024)), 2);

        //if (filesize > 2048)
        //{
        //    return false;
        //}
        System.Drawing.Image img = System.Drawing.Image.FromStream(fuFileUpload.PostedFile.InputStream);
        int height = img.Height;
        int width = img.Width;
       // if ((height!=1200) && (width!=720))
            //{ return false; }
        return true;
    }
    protected void lbtnPhotoupload_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        pnlctgryHeading.Visible = false;
        pnlphotoHeading.Visible = true;
        pnlctgry.Visible = false;
        pnlphoto.Visible = true;
        loadCategoryData();
    }
    private void DeletePhoto()
    {
        Int16 categoryid;
        Int16 photoid;

        photoid= Int16.Parse(Session["_dPhotoId"].ToString());
       categoryid = Int16.Parse(Session["_dCategoryID"].ToString());



        string IPAddress = HttpContext.Current.Request.UserHostAddress;
        NpgsqlCommand MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_delete_photo");
        MyCommand.Parameters.AddWithValue("p_catgegoryid", categoryid);
        MyCommand.Parameters.AddWithValue("p_photoid", photoid);
        MyCommand.Parameters.AddWithValue("p_updatedby", Session["_UserCode"].ToString());
        MyCommand.Parameters.AddWithValue("p_ipaddress", IPAddress);
        string Mresult = bll.UpdateAll(MyCommand);
        if (Mresult == "Success")
        {
            successmsg("Photo have been Successfully Deleted !");
            var filePath = Server.MapPath(Session["ImgUrl"].ToString());
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            removeXMLPhoto(categoryid, photoid);
            loadPhotoData();
            ResetPhoto();
        }
        else
        {
            Errormsg("Error Occured!");
        }



    }
    protected void btnUploadImage_Click(object sender, EventArgs e)
    {
       // Errormsg("File size must not exceed 2MB.");
        //if ((FileUploadImage.HasFile == true))
        //{
        //    if (isValidFile(FileUploadImage) == false)
        //    {
        //        Errormsg("File size must not exceed 2MB.");
        //        return;
        //    }


        //    FileUploadImage.Visible = true;
        // //   lbtnfuAttach1Clear.Visible = true;
        //    Session["fuAttach1Bytes"] = FileUploadImage.FileBytes;
        //    Session["fuAttach1"] = FileUploadImage;
        //  //  lbtnImage.Visible = true;
        //  //  lbtnImage.Text = FileUploadImage.FileName;
        //}



    }
    protected void lbtnfuAttach1Clear_Click(object sender, System.EventArgs e)
    {
        CsrfTokenValidate();
        lbtnfuAttach1Clear.Visible = false;
        Session["fuAttach1"] = null;
        Session["fuAttach1Bytes"] = null;
    }
    protected void lbtnSaveImage_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (validPhotoGallery() == false)
        {
            return;
        }
        confirmmsg("Do you want to Save Image?");

        lbtnYesConfirmation.CommandArgument = "cmdSaveImage";
        Session["Action"] = "P";
    }
    protected void lbtnResetImage_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        ddlCategoryName.Text = "";
        tbTitle.Text = "";
    }
    private bool validPhotoGallery()
    {
        int msgcnt = 0;
        string msg = "";

        if (ddlCategoryName.SelectedValue == "")
        {

            msgcnt = msgcnt + 1;
            msg = msg + msgcnt.ToString() + ". Please Select Category.<br>";
        }

        if (tbTitle.Text == "")
        {
            msgcnt = msgcnt + 1;
            msg = msg + msgcnt.ToString() + ". Please Enter  title.<br>";

        }
        else if (tbTitle.Text.Length < 3)
        {
            msgcnt = msgcnt + 1;
            msg = msg + msgcnt.ToString() + ". Please enter valid title (min 3 char).<br>";

        }
        if (FileUploadImage.HasFile == false)
        {
            msgcnt = msgcnt + 1;
            msg = msg + msgcnt.ToString() + ". Select Image.<br>";
            lblfuFileName.Text = "";
        }
        else if (isValidFile(FileUploadImage) == false)
        {
            msgcnt = msgcnt + 1;
            msg = msg + msgcnt.ToString() + ". Please check for image  of size (2MB) and format png/jpeg/jpg .<br>";
            lblfuFileName.Text = "";

        }
 System.Drawing.Image img = System.Drawing.Image.FromStream(FileUploadImage.PostedFile.InputStream);
        int height = img.Height;
        int width = img.Width;
        if ((height != 720) && (width != 1200))
        {
            msgcnt = msgcnt + 1;
            msg = msg + msgcnt.ToString() + ". Please check, Image of only dimension 1200X720 are allowed.<br>";
            lblfuFileName.Text = "";
        }
        if (msgcnt > 0)
        {
            Errormsg(msg);
            return false;
        }
        return true;
    }
    private void saveImages()//M5
    {
        try
        {
            Int16 categoryid;
            string imagetitle = "";
            string imagetitle_local = "";
            Int16 photoid;
            categoryid = Int16.Parse(ddlCategoryName.SelectedValue.ToString());
            imagetitle = tbTitle.Text.ToString();
            imagetitle_local = tbImageTitleLocal.Text.ToString();

            string filename = "Photo" + categoryid.ToString() + "_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + System.IO.Path.GetExtension(FileUploadImage.FileName);
            string filepath = "PhotoGallery/" + filename;



            string IPAddress = HttpContext.Current.Request.UserHostAddress;
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_insert_photo");
            MyCommand.Parameters.AddWithValue("p_categyid", categoryid);
            MyCommand.Parameters.AddWithValue("p_imagetitle", imagetitle);
            MyCommand.Parameters.AddWithValue("p_imagetitle_local", imagetitle_local);
            MyCommand.Parameters.AddWithValue("p_imageurl", filepath);
            MyCommand.Parameters.AddWithValue("p_updatedby", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("p_ipaddress", IPAddress);
            MyTable = bll.SelectAll(MyCommand);
            if (MyTable.Rows.Count > 0)
            {
                photoid = Int16.Parse(MyTable.Rows[0][0].ToString());
                XML_insertPhotograph(categoryid, photoid, imagetitle, imagetitle_local, filepath);

                successmsg("Photo have been Successfully Saved !");
                ResetPhoto();
            }
            else
            {
                _common.ErrorLog("PAdminPhotoGallery.aspx-0007", MyTable.TableName);
                Errormsg("Error Occured, Please contact Admin with Error Code PAdminPhotoGallery-M5");
            }
        }

        catch (Exception ex)
        {
            _common.ErrorLog("PAdminPhotoGallery.aspx-0008", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
        }
    }
    void XML_insertPhotograph(Int16 catgoryID, Int16 photoid, string title, string titleLocal, string filepath)
    {

        //---------------------------Save Image in file system--------------------//
        string filepathforupload= "~/" + filepath;
        FileUploadImage.SaveAs(Server.MapPath(filepathforupload));

        //--------------------------------------------//
        XmlDocument xDoc1 = new XmlDocument();
        xDoc1.Load(Server.MapPath("../CommonDataPhoto.xml"));
        XmlNode CategotyNode = xDoc1.SelectSingleNode("//Category[@Id='" + catgoryID.ToString() + "']//Photo[@Id='" + photoid.ToString() + "']") as XmlNode;
        if (CategotyNode != null)
        {
            CategotyNode.ParentNode.RemoveChild(CategotyNode);
            xDoc1.Save(Server.MapPath("../CommonDataPhoto.xml"));
        }

        //------------------------------------Find a Node and Child
        XmlDocument xDoc = new XmlDocument();

        // Load Xml
        xDoc.Load(Server.MapPath("../CommonDataPhoto.xml"));
        XmlNode PhotographsXle = xDoc.SelectSingleNode("//Category[@Id='" + catgoryID.ToString() + "']") as XmlNode;

        if (PhotographsXle != null)
        {

            XmlElement name = xDoc.CreateElement("Photo");
            name.InnerText = filepath;
            name.SetAttribute("Id", photoid.ToString());
            name.SetAttribute("title", title.ToString());
            name.SetAttribute("titleLocal", titleLocal.ToString());
            PhotographsXle.AppendChild(name);
            xDoc.Save(Server.MapPath("../CommonDataPhoto.xml"));

        }

    }
    private void ResetPhoto()
    {
        loadPhotoData();
        ddlCategoryName.SelectedIndex = 0;
        tbTitle.Text = "";
        tbImageTitleLocal.Text = "";
        //lbtnfuAttach1Clear.Visible = false;
        //lbtnImage.Visible = false;
        
    }
    private void loadPhotoData()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            DataTable dt = new DataTable();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_photo");
            MyCommand.Parameters.AddWithValue("p_categoryid", Convert.ToInt64(ddlImageList.SelectedValue.ToString()));
            MyCommand.Parameters.AddWithValue("p_limit", Convert.ToInt32("100"));
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    rptImageList.DataSource = dt;
                    rptImageList.DataBind();
                    rptImageList.Visible = true;
                    pnlPhotoNoRecord.Visible = false;
                }
                else
                {
                    rptImageList.Visible = false;
                    pnlPhotoNoRecord.Visible = true;
                }
            }

        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminPhotoGallery.aspx-0009", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
        }

    }
    protected void rptImageList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Image img = (Image)e.Item.FindControl("img");
            HiddenField hdphotourl = (HiddenField)e.Item.FindControl("hdphotourl");
            img.ImageUrl = hdphotourl.Value.ToString();

        }
    }

    protected void rptImageList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        CsrfTokenValidate();
        if (e.CommandName == "deletephoto")
        {
            HiddenField hdnphotoid = (HiddenField)e.Item.FindControl("hdnphotoid");
            HiddenField hdphotourl = (HiddenField)e.Item.FindControl("hdphotourl");
            HiddenField hdcategoryid = (HiddenField)e.Item.FindControl("hdcategoryid");


            Session["_dPhotoId"] = hdnphotoid.Value.ToString();
            Session["_dCategoryID"] = hdcategoryid.Value.ToString();
            Session["Action"] = "D";
            Session["ImgUrl"] = hdphotourl.Value.ToString();
            confirmmsg("Do you want to Delete Image ?");
            lbtnYesConfirmation.CommandArgument = "cmdDelImage";
        }
    }

    protected void ddlImageList_SelectedIndexChanged(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        loadPhotoData();
    }
    protected void lbtnVideoUpload_Click(object sender, EventArgs e)
    {

    }
    protected void lbtnImage_Click(object sender, EventArgs e)
    {

    }
    #endregion

}

