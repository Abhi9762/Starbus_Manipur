using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_PAdminDiscountandOffer : BasePage 
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
        if (!IsPostBack)
        {
            Random randomclass = new Random();
            Session["rndNoCheck"] = randomclass.Next().ToString();
            hidtoken.Value = Session["rndNoCheck"].ToString();
            lblSummary.Text = "Summary As on Date " + DateTime.Now;
            Session["_moduleName"] = "Offer/Discount";
            OfferDraftList();
            OfferList();
            OfferCount();

        }
    }

    #region "Method"
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
    private void SuccessMsg(string msg)
    {
        string popup = _popup.modalPopupSmall("S", "Success", msg, "OK");
        Response.Write(popup);
    }
    private void InfoMsg(string msg)
    {
        string popup = _popup.modalPopupSmall("I", "information", msg, "OK");
        Response.Write(popup);
    }
    private void publishOffer()//M1
    {
        try
        {
            int coupon = Convert.ToInt32(Session["Coupon"].ToString());
            string IPAddress = HttpContext.Current.Request.UserHostAddress;
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_discount_publish");
            MyCommand.Parameters.AddWithValue("p_couponid", coupon);
            MyCommand.Parameters.AddWithValue("p_updatedby", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("p_ipaddress", IPAddress);
            string Mresult = bll.UpdateAll(MyCommand);
            if (Mresult == "Success")
            {
                SuccessMsg("Offer Published !");
                OfferDraftList();
                OfferList();
                OfferCount();
            }
            else
            {
                Errormsg("Error,while saving");
            }
        }
        catch (Exception ex)
        {

            _common.ErrorLog("PAdminDiscountandOffer.aspx-0001", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
        }

    }
    private void ValidityOffer()//M2
    {
        try
        {
            int coupon = Convert.ToInt32(Session["C"].ToString());
            DateTime? FromValidity = null;
            if (tbFrom.Text.Trim() != null || tbFromValidity.Text.Trim() != "")
            {
                FromValidity = Convert.ToDateTime(tbFromValidity.Text);
            }
            DateTime? toValidity = null;
            if (tbTo.Text.Trim() != null || tbToValidity.Text.Trim() != "")
            {
                toValidity = Convert.ToDateTime(tbToValidity.Text);
            }
            string IPAddress = HttpContext.Current.Request.UserHostAddress;
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_discount_validity_change");
            MyCommand.Parameters.AddWithValue("p_couponid", coupon);
            MyCommand.Parameters.AddWithValue("p_validfrom", FromValidity);
            MyCommand.Parameters.AddWithValue("p_validto", toValidity);
            MyCommand.Parameters.AddWithValue("p_updatedby", "" /*Session["_UserCode"].ToString()*/);
            MyCommand.Parameters.AddWithValue("p_ipaddress", IPAddress);
            string Mresult = bll.UpdateAll(MyCommand);
            if (Mresult == "Success")
            {
                SuccessMsg("Validity Changed !");
                OfferDraftList();
                OfferList();
                OfferCount();
            }
            else
            {
                Errormsg("Error,while saving");
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminDiscountandOffer.aspx-0002", ex.Message.ToString());
            Errormsg(ex.Message.ToString());

        }

    }
    private void deleteOffer()//M3
    {
        try
        {
            int coupon = Convert.ToInt32(Session["Coupon"].ToString());
            string IPAddress = HttpContext.Current.Request.UserHostAddress;
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_discount_delete");
            MyCommand.Parameters.AddWithValue("p_couponid", coupon);
            MyCommand.Parameters.AddWithValue("p_updatedby", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("p_ipaddress", IPAddress);
            string Mresult = bll.UpdateAll(MyCommand);
            if (Mresult == "Success")
            {
                SuccessMsg("Offer Deleted!");
                OfferDraftList();
                OfferList();
                OfferCount();
            }
            else
            {
                Errormsg("Error,while saving");
            }

        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminDiscountandOffer.aspx-0003", ex.Message.ToString());
            Errormsg(ex.Message.ToString());

        }

    }
    private void DiscontinueOffer()//M4
    {
        try
        {
            string coupon = Session["C"].ToString();
            string IPAddress = HttpContext.Current.Request.UserHostAddress;
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_discount_discontinue");
            MyCommand.Parameters.AddWithValue("p_couponid", coupon);
            MyCommand.Parameters.AddWithValue("p_updatedby", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("p_ipaddress", IPAddress);
            string Mresult = bll.UpdateAll(MyCommand);
            if (Mresult == "Success")
            {
                SuccessMsg("Offer Discontinue !");
                OfferDraftList();
                OfferList();
                OfferCount();
            }
            else
            {
                Errormsg("Error,while saving");
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminDiscountandOffer.aspx-0004", ex.Message.ToString());
            Errormsg(ex.Message.ToString());

        }

    }
    private void saveDiscountDetailDraft()//M5
    {
        try
        {
            byte[] Fileweb = null;
            Fileweb = Session["Web"] as byte[];
            byte[] Filemob = null;
            Filemob = Session["Mobile"] as byte[];
            string OfferCode = tbOfferCode.Text;
            string title = tbTitle.Text;
            string Description = tbDescription.Text;
            string DiscountType = rbtnDiscountType.SelectedValue;
            string ApplicationTo = rbtnDiscountApplicable.SelectedValue;
            string DiscountAmt = tbDiscountAmt.Text;
            string MaxDiscountAmt = tbMaxDiscountAmt.Text;
            string FromValidity = tbFrom.Text;
            string toValidity = tbTo.Text;
            string IPAddress = HttpContext.Current.Request.UserHostAddress;
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_discount_insert_draft");
            MyCommand.Parameters.AddWithValue("p_action", Session["Action"].ToString());
            MyCommand.Parameters.AddWithValue("p_couponcode", OfferCode.ToString());
            MyCommand.Parameters.AddWithValue("p_coupontitle", title.ToString());
            MyCommand.Parameters.AddWithValue("p_discountdesc", Description.ToString());
            MyCommand.Parameters.AddWithValue("p_discounttype", DiscountType.ToString());
            MyCommand.Parameters.AddWithValue("p_discounton", ApplicationTo.ToString());
            MyCommand.Parameters.AddWithValue("p_discountamt", Convert.ToInt32(DiscountAmt.ToString()));
            MyCommand.Parameters.AddWithValue("p_maxdiscount", Convert.ToInt32(MaxDiscountAmt.ToString()));
            MyCommand.Parameters.AddWithValue("p_validfrom", FromValidity);
            MyCommand.Parameters.AddWithValue("p_validto", toValidity);
            MyCommand.Parameters.AddWithValue("p_web", Fileweb);
            MyCommand.Parameters.AddWithValue("p_mob", Filemob);
            MyCommand.Parameters.AddWithValue("p_updatedby", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("p_ipaddress", IPAddress);
            DataTable dtt = bll.SelectAll(MyCommand);
            if (dtt.Rows.Count >0)
            {
                if (Session["Action"].ToString() == "Save")
                {
                    SuccessMsg("Offer Created Successfully!");
                    updateImages(dtt.Rows[0][0].ToString());
                }
                if (Session["Action"].ToString() == "Update")
                {
                    SuccessMsg("Offer Updated Successfully!");
                }                
                resetcontrol();
                OfferDraftList();
                OfferList();
                OfferCount();
            }
            else
            {
                Errormsg(dtt.TableName );
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminDiscountandOffer.aspx-0005", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
        }
    }
    private void saveDiscountDetail()//M6
    {
        try
        {
            byte[] Fileweb = null;
            Fileweb = Session["Web"] as byte[];
            byte[] Filemob = null;
            Filemob = Session["Mobile"] as byte[];
            string OfferCode = tbOfferCode.Text;
            string title = tbTitle.Text;
            string Description = tbDescription.Text;
            string DiscountType = rbtnDiscountType.SelectedValue;
            string ApplicationTo = rbtnDiscountApplicable.SelectedValue;
            string DiscountAmt = tbDiscountAmt.Text;
            string MaxDiscountAmt = tbMaxDiscountAmt.Text;
            string FromValidity = tbFrom.Text;
            string toValidity = tbTo.Text;

            string IPAddress = HttpContext.Current.Request.UserHostAddress;
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_discount_insert");
            MyCommand.Parameters.AddWithValue("p_couponcode", OfferCode.ToString());
            MyCommand.Parameters.AddWithValue("p_coupontitle", title.ToString());
            MyCommand.Parameters.AddWithValue("p_discountdesc", Description.ToString());
            MyCommand.Parameters.AddWithValue("p_discounttype", DiscountType.ToString());
            MyCommand.Parameters.AddWithValue("p_discounton", ApplicationTo.ToString());
            MyCommand.Parameters.AddWithValue("p_discountamt", Convert.ToInt32(DiscountAmt.ToString()));
            MyCommand.Parameters.AddWithValue("p_maxdiscount", Convert.ToInt32(MaxDiscountAmt.ToString()));
            MyCommand.Parameters.AddWithValue("p_validfrom", FromValidity);
            MyCommand.Parameters.AddWithValue("p_validto", toValidity);
            MyCommand.Parameters.AddWithValue("p_web", Fileweb);
            MyCommand.Parameters.AddWithValue("p_mob", Filemob);
            MyCommand.Parameters.AddWithValue("p_updatedby", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("p_ipaddress", IPAddress);
            DataTable dtt = bll.SelectAll(MyCommand);
            if (dtt.Rows.Count > 0)
            {
                updateImages(dtt.Rows[0][0].ToString() );
                SuccessMsg("Added Successfully !");
                OfferDraftList();
                OfferList();
                OfferCount();
            }
            else
            {
                Errormsg(dtt.TableName );
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminDiscountandOffer.aspx-0006", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
        }
    }
    protected void OfferDraftList()//M7
    {
        try
        {
            lblnorecord.Visible = true;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_discount_get_draft_list");
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                gvOfferDraft.DataSource = dt;
                gvOfferDraft.DataBind();
                gvOfferDraft.Visible = true;
                lblnorecord.Visible = false;
            }

        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminDiscountandOffer.aspx-0007", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
        }
    }
    protected void OfferList()//M8
    {
        try
        {
            lblNoActiveoffer.Visible = true;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_discount_get_list");
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                gvOffer.DataSource = dt;
                gvOffer.DataBind();
                lblNoActiveoffer.Visible = false;
                gvOffer.Visible = true;
                lblFromDate.Text = dt.Rows[0]["validfrom_date"].ToString();
                lblToDate.Text = dt.Rows[0]["validto_date"].ToString();

            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminDiscountandOffer.aspx-0008", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
        }
    }
    private void OfferCount()//M9
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_offercount");
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                lbltotalOffers.Text = dt.Rows[0]["total"].ToString();
                lblPublished.Text = dt.Rows[0]["published"].ToString();
                lblPending.Text = dt.Rows[0]["pending"].ToString();
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminDiscountandOffer.aspx-0009", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
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
    private bool CheckFileExtention(FileUpload FileMobileApp)//M10
    {
        try
        {
            bool fileExtensionOK = false;
            string fileExtension;
            fileExtension = System.IO.Path.GetExtension(FileMobileApp.FileName).ToLower();
            string[] allowedExtensions = new[] { ".jpg", ".png", ".jpeg" };
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
            _common.ErrorLog("PAdminDiscountandOffer.aspx-0010", ex.Message.ToString());
            Errormsg("Something went wrong with image (allow only .jpg, .png, .jpeg image). ");
            return default(Boolean);
        }
    }
    private bool CheckFileExtentionWeb(FileUpload FileWebPortal)//M11
    {
        try
        {
            bool fileExtensionOK = false;
            string fileExtension;
            fileExtension = System.IO.Path.GetExtension(FileWebPortal.FileName).ToLower();
            string[] allowedExtensions = new[] { ".jpg", ".png", ".jpeg" };
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
            _common.ErrorLog("PAdminDiscountandOffer.aspx-0011", ex.Message.ToString());
            Errormsg("Something went wrong with image (allow only .jpg, .png, .jpeg image). ");
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
            return null;
        }
    }

    private void updateImages(string OfferId)//M12
    {
        try
        {
            byte[] Fileweb = null;
            byte[] Filemob = null;
            string saveDirectory = "../DBImg/Offers/";
            string fileName = "";
            string fileSavePath = "";
            if (Session["Web"] != null | Session["Web"].ToString() == "")
            {
                fileName = OfferId + "_W.png";
                fileSavePath = Path.Combine(saveDirectory, fileName);
                Fileweb = (byte[])Session["Web"];
                System.IO.File.WriteAllBytes(Server.MapPath(fileSavePath), Fileweb);
            }

            if (Session["Mobile"] != null | Session["Mobile"].ToString() == "")
            {
                fileName = OfferId + "_M.png";
                fileSavePath = Path.Combine(saveDirectory, fileName);
                Filemob = (byte[])Session["Mobile"];
                System.IO.File.WriteAllBytes(Server.MapPath(fileSavePath), Filemob);

            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminDiscountandOffer.aspx-0012", ex.Message.ToString());
            Errormsg("Error,while saving Uploaded File");
            return;
        }
    }

    public void resetcontrol()
    {
        tbDescription.Text = "";
        tbDiscountAmt.Text = "";
        tbFrom.Text = "";
        tbTo.Text = "";
        tbOfferCode.Text = "";
        tbTitle.Text = "";
        rbtnDiscountType.SelectedValue = "0";
        rbtnDiscountApplicable.SelectedValue = "0";
        tbMaxDiscountAmt.Text = "";
        Session["Web"] = null;
        Session["Mobile"] = null;
        imgMobileApp.Visible = false;
        ImgWebPortal.Visible = false;
    }

    private Boolean Validaion()//M12
    {
        try
        {
            int count = 0;
            string msg = "";


            if (_validation.IsValidString(tbOfferCode.Text, 5, 20) == false)
            {
                count++;
                msg = msg + count + ". Enter Valid Offer Code<br/>";
            }

            if (_validation.IsValidString(tbTitle.Text, 5, tbTitle.MaxLength) == false)
            {
                count++;
                msg = msg + count + ".  Enter Valid Title<br/>";
            }

            if (_validation.IsValidString(tbDescription.Text, 6, 200) == false)
            {
                count++;
                msg = msg + count + ".  Enter Valid Description<br/>";
            }
           
            if (tbFrom.Text == "" || tbTo.Text == "")
            {
                count++;
                msg = msg + count + ". Enter Valid From Date  and To date<br/>";
            }
            else
            {
                DateTime ValidFrom = DateTime.ParseExact(tbFrom.Text, "dd/MM/yyyy", null);
                DateTime Validto = DateTime.ParseExact(tbTo.Text, "dd/MM/yyyy", null);
                if (ValidFrom > Validto)
                {
                    count = count + 1;
                    msg = msg + count.ToString() + ". To Date should be greater than From date.<br/>";
                }
            }
            if (_validation.IsValidString(tbMaxDiscountAmt.Text, 2, tbMaxDiscountAmt.MaxLength  ) == false)
            {
                count++;
                msg = msg + count + ". Enter Valid Maximum Discount Amount <br/>";
            }
            if (_validation.IsValidString(tbDiscountAmt.Text, 1, tbDiscountAmt.MaxLength ) == false)
            {
                count++;
                msg = msg + count + ". Enter Valid Discount Amount/Percentage (min 1 digit )<br/>";
            }
            if (rbtnDiscountApplicable.SelectedValue == "0")
            {
                count++;
                msg = msg + count + ". Select Discount Applicable <br/>";
            }
            if (Session["Web"] == null)
            {
                count = count + 1;
                msg = msg + count.ToString() + ". Web Portal.<br>";
            }
            if (Session["Mobile"] == null)
            {
                count = count + 1;
                msg = msg + count.ToString() + ". Mobile Application Image.<br>";
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
            _common.ErrorLog("PAdminDiscountandOffer.aspx-0013", ex.Message.ToString());
            return false;
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

    #endregion

    #region "Event"
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (Session["Action"].ToString() == "Save")
        {
            saveDiscountDetailDraft();
        }
        if (Session["Action"].ToString() == "Update")
        {
            saveDiscountDetailDraft();
        }
        if (Session["Action"].ToString() == "Publish")
        {
            saveDiscountDetail();

        }
        if (Session["Action"].ToString() == "P")
        {
            publishOffer();

        }
        if (Session["Action"].ToString() == "Delete")
        {
            deleteOffer();

        }
        if (Session["Action"].ToString() == "Validity")
        {
            ValidityOffer();
        }
        if (Session["Action"].ToString() == "Discontinue")
        {
            DiscontinueOffer();
        }
    }
    protected void lbtnReset_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        resetcontrol();

    }
    protected void btnUploadWebPortal_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (!FileWebPortal.HasFile)
        {

            Errormsg("Please select report first");

            return;
        }
        string _fileFormat = GetMimeDataOfFile(FileWebPortal.PostedFile);
        if ((_fileFormat == "image/png") || (_fileFormat == "image/jpeg"))
        {
        }
        else
        {
            Errormsg("File must png, jpg, jpeg type");

            return;
        }
        if (!CheckFileExtentionWeb(FileWebPortal))
        {
            Errormsg("File must be png, jpg, jpeg type");

            return;
        }

        decimal size = Math.Round((Convert.ToDecimal(FileWebPortal.PostedFile.ContentLength) / Convert.ToDecimal(1024)), 2);
        System.Drawing.Image img = System.Drawing.Image.FromStream(FileWebPortal.PostedFile.InputStream);
        int height = img.Height;
        int width = img.Width;
        string dimension = width.ToString() + "*" + height.ToString();
        if (size > 20 && dimension == "600*300")
        {
            Errormsg("File size must be 20 KB & pixel size should be 600*300 pixels");

            return;
        }
        byte[] PhotoImage = null;
        PhotoImage = convertByteFile(FileWebPortal);
        ImgWebPortal.ImageUrl = GetImage(PhotoImage);
        ImgWebPortal.Visible = true;
        Session["Web"] = FileWebPortal.FileBytes;
    }
    protected void btnUploadMobileApp_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (!FileMobileApp.HasFile)
        {

            Errormsg("Please select report first");
            return;
        }
        string _fileFormat = GetMimeDataOfFile(FileMobileApp.PostedFile);
        if ((_fileFormat == "image/png") || (_fileFormat == "image/jpeg"))
        {
        }
        else
        {
            Errormsg("File must be png, jpg, jpeg type");
            return;
        }
        if (!CheckFileExtention(FileMobileApp))
        {
            Errormsg("File must be png, jpg, jpeg type");
            return;
        }
        decimal size = Math.Round((Convert.ToDecimal(FileWebPortal.PostedFile.ContentLength) / Convert.ToDecimal(1024)), 2);
        System.Drawing.Image img = System.Drawing.Image.FromStream(FileMobileApp.PostedFile.InputStream);
        int height = img.Height;
        int width = img.Width;
        string dimension = width.ToString() + "*" + height.ToString();
        if (size > 10 && dimension == "155*18")
        {
            Errormsg("File size must be  10 KB & pixel size should be 155*18 pixels");

            return;
        }




        byte[] PhotoImage = null;
        PhotoImage = convertByteFile(FileMobileApp);
        imgMobileApp.ImageUrl = GetImage(PhotoImage);
        imgMobileApp.Visible = true;
        Session["Mobile"] = FileMobileApp.FileBytes;
    }
    protected void lbtnSave_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (Validaion() == false)
        {
            return;
        }
        lblConfirmation.Text = "Do you want to Save the Offer/Discount?";
        mpConfirmation.Show();
        Session["Action"] = "Save";
    }
    protected void lbtnSaveandPublish_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (Validaion() == false)
        {
            return;
        }
        lblConfirmation.Text = "Do you want to Save and Publish the Offer/Discount?";
        mpConfirmation.Show();
        Session["Action"] = "Publish";

    }
    protected void gvOfferDraft_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvOfferDraft.PageIndex = e.NewPageIndex;
        OfferDraftList();
    }
    protected void gvOffer_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvOffer.PageIndex = e.NewPageIndex;
        OfferList();

    }
    protected void gvOfferDraft_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        CsrfTokenValidate();
        if (e.CommandName == "Modify")
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            lblEdit.Visible = true;
            lblCreate.Visible = false;
            lbtnUpdate.Visible = true;
            lbtnSave.Visible = false;
            lbtnSaveandPublish.Visible = false;
            lbtnReset.Visible = false;
            lbtnCancelD.Visible = true;
            tbOfferCode.Enabled = false;
            string CouponCode = gvOfferDraft.DataKeys[row.RowIndex]["couponcode"].ToString();
            string CouponTitle = gvOfferDraft.DataKeys[row.RowIndex]["coupontitle"].ToString();
            string DiscountDesc = gvOfferDraft.DataKeys[row.RowIndex]["discountdescription"].ToString();
            string DiscType = gvOfferDraft.DataKeys[row.RowIndex]["discounttype"].ToString();
            string DiscOn = gvOfferDraft.DataKeys[row.RowIndex]["discounton"].ToString();
            string DiscAmt = gvOfferDraft.DataKeys[row.RowIndex]["discountamount"].ToString();
            string MaxAmt = gvOfferDraft.DataKeys[row.RowIndex]["maxdiscount_amount"].ToString();
            string ValidFrom = gvOfferDraft.DataKeys[row.RowIndex]["validfrom_date"].ToString();
            string ValidTo = gvOfferDraft.DataKeys[row.RowIndex]["validto_date"].ToString();
            Session["Web"] = gvOfferDraft.DataKeys[row.RowIndex]["img_web"];
            Byte[] imgbytes = (Byte[])Session["Web"];
            ImgWebPortal.ImageUrl = GetImage(imgbytes);
            ImgWebPortal.Visible = true;

            Session["Mobile"] = gvOfferDraft.DataKeys[row.RowIndex]["img_app"];
            Byte[] mobile = (Byte[])Session["Mobile"];
            imgMobileApp.ImageUrl = GetImage(mobile);
            imgMobileApp.Visible = true;
            tbOfferCode.Text = CouponCode;
            tbTitle.Text = CouponTitle;
            tbDescription.Text = DiscountDesc;
            rbtnDiscountType.SelectedValue = DiscType;
            rbtnDiscountApplicable.SelectedValue = DiscOn;
            tbDiscountAmt.Text = DiscAmt;
            tbMaxDiscountAmt.Text = MaxAmt;
            tbFrom.Text = ValidFrom;
            tbTo.Text = ValidTo;




        }
        if (e.CommandName == "Publish")
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            string Couponid = gvOfferDraft.DataKeys[row.RowIndex]["couponid"].ToString();
            Session["Coupon"] = Couponid;
            lblConfirmation.Text = "Do you want to Publish the Offer/Discount?";
            mpConfirmation.Show();
            Session["Action"] = "P";


        }
        if (e.CommandName == "Remove")
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            string Couponid = gvOfferDraft.DataKeys[row.RowIndex]["couponid"].ToString();
            Session["Coupon"] = Couponid;
            lblConfirmation.Text = "Do you want to delete the Offer/Discount?";
            mpConfirmation.Show();
            Session["Action"] = "Delete";


        }
    }
    protected void lbtnSaveValidity_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        lblConfirmation.Text = "Do you want to change the validity of offer/Discount";
        mpConfirmation.Show();
        Session["Action"] = "Validity";

    }
    protected void gvOffer_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        CsrfTokenValidate();
        if (e.CommandName == "change_validity")
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            string Couponid = gvOffer.DataKeys[row.RowIndex]["couponid"].ToString();
            Session["C"] = Couponid;
            mpChangeValidity.Show();
        }
        if (e.CommandName == "Discontinue")
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            string Couponid = gvOffer.DataKeys[row.RowIndex]["couponcode"].ToString();
            Session["C"] = Couponid;
            lblConfirmation.Text = "Do you want to Discontinue the validity of offer/Discount";
            mpConfirmation.Show();
            Session["Action"] = "Discontinue";
        }
        if (e.CommandName == "viewoffer")
        {
            pnlView.Visible = true;
            pnladd.Visible = false;
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            string Couponid = gvOffer.DataKeys[row.RowIndex]["couponid"].ToString();
            string CouponCode = gvOffer.DataKeys[row.RowIndex]["couponcode"].ToString();
            string CouponTitle = gvOffer.DataKeys[row.RowIndex]["coupontitle"].ToString();
            string DiscountDesc = gvOffer.DataKeys[row.RowIndex]["discountdescription"].ToString();
            string DiscType = gvOffer.DataKeys[row.RowIndex]["discounttype"].ToString();
            string DiscOn = gvOffer.DataKeys[row.RowIndex]["discounton"].ToString();
            string DiscAmt = gvOffer.DataKeys[row.RowIndex]["discountamount"].ToString();
            string MaxAmt = gvOffer.DataKeys[row.RowIndex]["maxdiscount_amount"].ToString();
            string ValidFrom = gvOffer.DataKeys[row.RowIndex]["validfrom_date"].ToString();
            string ValidTo = gvOffer.DataKeys[row.RowIndex]["validto_date"].ToString();


           // imgWeb.ImageUrl = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + "/starbusnew/DBimg/offers/" + Couponid + "_W.png";
            imgWeb.ImageUrl =  "../DBimg/offers/" + Couponid + "_W.png";
            imgWeb.Visible = true;
           
            imgApp.ImageUrl ="../DBimg/offers/" + Couponid + "_M.png";
            imgApp.Visible = true;
            lblOffercode.Text = CouponCode;
            lblofferTitle.Text = CouponTitle;
            lblOfferDescription.Text = DiscountDesc;

            if (DiscType == "P")
            {
                lblofferType.Text = "Percentage";
            }
            else if (DiscType == "A")
            {
                lblofferType.Text = "Amoount";
            }
            if (DiscOn == "S")
            {
                lblOfferApplicableTo.Text = "Per Seat";
            }
            else if (DiscOn == "B")
            {
                lblOfferApplicableTo.Text = "Per Bus";
            }
            lblofferamount.Text = DiscAmt;
            lblMaxDiscount.Text = MaxAmt;
            lblofferFromDate.Text = ValidFrom;
            lblofferToDate.Text = ValidTo;


        }
    }
    protected void gvOfferDraft_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header,
                                                        DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "Title";
            HeaderCell.ColumnSpan = 1;
            HeaderGridRow.Cells.Add(HeaderCell);
            HeaderCell.CssClass = "headerCss";



            HeaderCell = new TableCell();
            HeaderCell.Text = "Validity";
            HeaderCell.ColumnSpan = 2;
            HeaderGridRow.Cells.Add(HeaderCell);
            HeaderCell.CssClass = "headerCss";

            HeaderCell = new TableCell();
            HeaderCell.Text = "Action";
            HeaderCell.ColumnSpan = 1;
            HeaderGridRow.Cells.Add(HeaderCell);
            HeaderCell.CssClass = "headerCss";


            gvOfferDraft.Controls[0].Controls.AddAt(0, HeaderGridRow);
        }
    }
    protected void gvOffer_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header,
                                                        DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "Title";
            HeaderCell.ColumnSpan = 1;
            HeaderGridRow.Cells.Add(HeaderCell);
            HeaderCell.CssClass = "headerCss";


            HeaderCell = new TableCell();
            HeaderCell.Text = "Validity";
            HeaderCell.ColumnSpan = 2;
            HeaderGridRow.Cells.Add(HeaderCell);
            HeaderCell.CssClass = "headerCss";

            HeaderCell = new TableCell();
            HeaderCell.Text = "Action";
            HeaderCell.ColumnSpan = 1;
            HeaderGridRow.Cells.Add(HeaderCell);
            HeaderCell.CssClass = "headerCss";
            gvOffer.Controls[0].Controls.AddAt(0, HeaderGridRow);
        }
    }
    protected void lbtnUpdate_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        mpConfirmation.Show();
        lblConfirmation.Text = "Do you want to update Offer?";
        Session["Action"] = "Update";

    }
    protected void lbtnCancel_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();

        pnladd.Visible = true;
        pnlView.Visible = false;
    }
    protected void lbtnview_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        InfoMsg("1. Offer Code Will be AlphaNumeric.<br>2.Offer Code cannot be edited.");
    }
    protected void lbtnAddNewOffer_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        pnlView.Visible = false;
        pnladd.Visible = true;
    }
    #endregion
    
}