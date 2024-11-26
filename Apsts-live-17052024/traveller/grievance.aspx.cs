using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Npgsql;

public partial class traveller_grievance : BasePage
{
    NpgsqlCommand MyCommand;
    sbBLL bll = new sbBLL();
    sbValidation _SecurityCheck = new sbValidation();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    private sbCommonFunc _common = new sbCommonFunc();
    string commonerror = "Something went wrong. Please feel free to contact the helpdesk or try again after some time.";
    protected void Page_Load(object sender, EventArgs e)
    {
        checkUser();
        if (!IsPostBack)
        {
            Random randomclass = new Random();
            Session["rndNoCheck"] = randomclass.Next().ToString();
            hidtoken.Value = Session["rndNoCheck"].ToString();
            Session["_moduleName"] = "Grievance";
            loadGrievances(Session["_UserCode"].ToString());
            loadCategories();
        }
    }

    #region "Methods"
    private void checkUser()
    {
        if (_security.isSessionExist(Session["_UserCode"]) == true)
        {
            Session["_UserCode"] = Session["_UserCode"];
        }
        else
        {
            Response.Redirect("errorpage.aspx");
        }
    }
    private void CheckTokan()
    {
        if (Session["rndNoCheck"] == null || Session["rndNoCheck"].ToString() == "")
        {
            Response.Redirect("../errorpage.aspx");
        }

        if (Session["rndNoCheck"].ToString() != hidtoken.Value.ToString())
        {
            Response.Redirect("../errorpage.aspx");
        }
    }
    private void Errormsg(string msg)
    {
        string popup = _popup.modalPopupSmall("W", "Information", msg, "Close");
        Response.Write(popup);
    }
    private void ConfirmMsg(string msg)
    {
        lblConfirmation.Text = msg;
        mpConfirmation.Show();
    }
    protected void Successmsg(string msg)
    {

        string popup = _popup.modalPopupSmall("S", "Congratulations", msg, "Close");
        Response.Write(popup);
    }
    private void loadGrievances(string userId)//M1
    {
        try
        {
            wsClass obj = new wsClass();
            DataTable dt = obj.getGrievances(userId, 1000, "L");
            pnlNoGrivance.Visible = true;
            if (dt.Rows.Count > 0)
            {
                gvTickets.DataSource = dt;
                gvTickets.DataBind();
                pnlNoGrivance.Visible = false;
            }
        }
        catch (Exception ex)
        {
            pnlNoGrivance.Visible = false;
            _common.ErrorLog("grievance.aspx-0001", ex.Message);
        }
    }
    private void loadCategories()//M2
    {
        try
        {
            ddlCategory.Items.Clear();
            ddlSubCategory.Items.Clear();
            wsClass obj = new wsClass();
            DataTable dt_ = obj.getGrievanceCategories();
            if (dt_.Rows.Count > 0)
            {
                DataView view = new DataView(dt_);
                DataTable distinctValues = view.ToTable(true, "catid", "catname");
                ddlCategory.DataSource = distinctValues;
                ddlCategory.DataTextField = "catname";
                ddlCategory.DataValueField = "catid";
                ddlCategory.DataBind();
                ddlCategory.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlCategory.Items.Add(new ListItem("Select", "0"));
            }
            ddlSubCategory.Items.Add(new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ddlCategory.Items.Add(new ListItem("Select", "0"));
            _common.ErrorLog("grievance.aspx-0002", ex.Message);
        }
    }

    private void loadSubCategories(string categoryId)//M3
    {
        try
        {
            ddlSubCategory.Items.Clear();
            wsClass obj = new wsClass();
            DataTable dt_ = obj.getGrievanceCategories();
            if (dt_.Rows.Count > 0)
            {
                DataTable tblFiltered = dt_.AsEnumerable().Where(row => row.Field<int>("catid") == int.Parse(categoryId)).CopyToDataTable();

                ddlSubCategory.DataSource = tblFiltered;
                ddlSubCategory.DataTextField = "subcatname";
                ddlSubCategory.DataValueField = "subcatid";
                ddlSubCategory.DataBind();
                ddlSubCategory.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlSubCategory.Items.Add(new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            ddlSubCategory.Items.Add(new ListItem("Select", "0"));
            _common.ErrorLog("grievance.aspx-0003", ex.Message);
        }
    }
    private void saveGrievance()//M4
    {
        try
        {
            string category, subCategory;
            string busNo = "", ticketNo = "", description;
            byte[] pic1, pic2;

            category = ddlCategory.SelectedValue.ToString();
            subCategory = ddlSubCategory.SelectedValue.ToString();

            description = tbDescription.Text.Trim().ToUpper();

            if (category == "0")
            {
                Errormsg("Please select 'Issue Reported'");
                return;
            }
            if (subCategory == "0")
            {
                Errormsg("Please select 'Want to report upon'");
                return;
            }

            if (Session["fuPic1"] != null && (!fuPic1.HasFile))
            {
                fuPic1 = (FileUpload)Session["fuPic1"];
            }
            if (Session["fuPic2"] != null && (!fuPic2.HasFile))
            {
                fuPic2 = (FileUpload)Session["fuPic2"];
            }

            if (!fuPic1.HasFile)
            {
                Errormsg("Please select Pic1.");
                return;
            }
            if (!fuPic2.HasFile)
            {
                Errormsg("Please select Pic2.");
                return;
            }

            if (category == "1")
            {
                ticketNo = tbBusTicketNo.Text.Trim().ToUpper();
                if (ticketNo.Length < 10)
                {
                    Errormsg("Enter Valid Ticket Number.");
                    return;
                }
            }
            else
            {
                busNo = tbBusTicketNo.Text.Trim().ToUpper();
                if (busNo.Length < 10)
                {
                    Errormsg("Enter Valid Bus Number.");
                    return;
                }
            }

            if (description.Length < 10)
            {
                Errormsg("Enter Valid Description.");
                return;
            }

            pic1 = fuPic1.FileBytes;
            pic2 = fuPic2.FileBytes;
            String pic1Base64 = Convert.ToBase64String(pic1);
            String pic2Base64 = Convert.ToBase64String(pic2);
            string UpdatedBy = Session["_UserCode"].ToString();
            string IpAddress = HttpContext.Current.Request.UserHostAddress;

            wsClass obj = new wsClass();
            DataTable dt = obj.saveGrievances(category, subCategory, busNo, ticketNo, description, pic1Base64, pic2Base64, "", "", UpdatedBy, IpAddress);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["p_rslt"].ToString() == "DONE")
                {
                    Successmsg("Grievance has been registered successfully.");
                    loadGrievances(UpdatedBy);
                    reset();
                }
                else
                {
                    Errormsg(commonerror);
                    _common.ErrorLog("trvlrGrvnc-M4", dt.TableName);
                }
            }
            else
            {
                Errormsg(commonerror);
                _common.ErrorLog("trvlrGrvnc-M4", dt.TableName);
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.ToString());
            _common.ErrorLog("grievance.aspx-0004", ex.Message);
        }
    }



    public byte[] convertByteFile(System.Web.UI.WebControls.FileUpload fuFileUpload)
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
    public string GetImage(object img)
    {
        try
        {
            return "data:image/jpg;base64," + Convert.ToBase64String((byte[])img);
        }
        catch (Exception ex)
        {
            _common.ErrorLog("grievance.aspx-0005", ex.Message);
            return null;
        }
    }
    public bool checkFileExtention(System.Web.UI.WebControls.FileUpload fuFileUpload)
    {
        try
        {
            bool fileExtensionOK = false;
            string fileExtension;
            fileExtension = System.IO.Path.GetExtension(fuFileUpload.FileName).ToLower();
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
            _common.ErrorLog("grievance.aspx-0006", ex.Message);
            Errormsg("Something went wrong with image (allow only .jpg, .png, .jpeg image). ");
            return default(Boolean);
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
    public void reset()
    {
        ddlCategory.SelectedValue = "0";
        ddlSubCategory.SelectedValue = "0";
        tbBusTicketNo.Text = "";
        tbDescription.Text = "";
        Session["fuPic1"] = null;
        Session["fuPic2"] = null;
        imgPic1.ImageUrl = null;
        imgPic2.ImageUrl = null;
        imgPic1.Visible = false;
        imgPic2.Visible = false;
    }
    #endregion

    #region "Events"
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        string catId = ddlCategory.SelectedValue.ToString();
        if (catId == "0")
        {
            ddlSubCategory.Items.Clear();
            ddlSubCategory.Items.Add(new ListItem("Select", "0"));
        }
        else
        {
            lblBus_Ticket_No_header.Text = catId == "1" ? "Ticket No." : "Bus No.";
            loadSubCategories(catId);
        }
    }

    protected void btnProceed_Click(object sender, EventArgs e)
    {
        CheckTokan();
        ConfirmMsg("Do you want to register grievance ?");
    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        CheckTokan();
        saveGrievance();
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        CheckTokan();
        reset();
    }
    protected void gvTickets_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        CheckTokan();
        if (e.CommandName == "VIEWDETAIL")
        {
            int rowIndex = int.Parse(e.CommandArgument.ToString());
            string refNo = gvTickets.DataKeys[rowIndex]["refno"].ToString();
            Session["_grRefNo"] = refNo;
            Session["_LOGINUSER"] = "T";
            eDash.Text = "<embed src = \"../Auth/dashGrievance.aspx\" style=\"height: 80vh; width: 100%\" />";
            mpGrievance.Show();
        }
    }

    protected void gvTickets_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTickets.PageIndex = e.NewPageIndex;
        loadGrievances(Session["_UserCode"].ToString());
    }


    protected void btnfuPic1_Click(object sender, EventArgs e)
    {
        CheckTokan();
        try
        {
            if (!fuPic1.HasFile)
            {
                Errormsg("Please select report first");
                return;
            }
            if (!checkFileExtention(fuPic1))
            {
                Errormsg("File must be png, jpg, jpeg type");
                return;
            }
            string _fileFormat = GetMimeDataOfFile(fuPic1.PostedFile);
            if ((_fileFormat == "image/png") || (_fileFormat == "image/jpeg"))
            {
            }
            else
            {
                Errormsg("File must png, jpg, jpeg type");
                return;
            }
            decimal size = Math.Round((Convert.ToDecimal(fuPic1.PostedFile.ContentLength) / Convert.ToDecimal(1024)), 2);
            if (size > 500 || size < 5)
            {
                Errormsg("File size must be between 5 kb to 500 kb");
                return;
            }
            byte[] PhotoImage = null;
            PhotoImage = convertByteFile(fuPic1);
            imgPic1.ImageUrl = GetImage(PhotoImage);
            imgPic1.Visible = true;

            Session["fuPic1"] = fuPic1;

        }
        catch (Exception ex)
        {
            _common.ErrorLog("grievance.aspx-0006", ex.Message);
        }
    }
    protected void btnfuPic2_Click(object sender, EventArgs e)
    {
        CheckTokan();
        try
        {
            if (!fuPic2.HasFile)
            {
                Errormsg("Please select report first");
                return;
            }
            if (!checkFileExtention(fuPic2))
            {
                Errormsg("File must be png, jpg, jpeg type");
                return;
            }
            string _fileFormat = GetMimeDataOfFile(fuPic2.PostedFile);
            if ((_fileFormat == "image/png") || (_fileFormat == "image/jpeg"))
            {
            }
            else
            {
                Errormsg("File must png, jpg, jpeg type");
                return;
            }
            decimal size = Math.Round((Convert.ToDecimal(fuPic2.PostedFile.ContentLength) / Convert.ToDecimal(1024)), 2);
            if (size > 500 || size < 5)
            {
                Errormsg("File size must be between 5 kb to 500 kb");
                return;
            }
            byte[] PhotoImage = null;
            PhotoImage = convertByteFile(fuPic2);
            imgPic2.ImageUrl = GetImage(PhotoImage);
            imgPic2.Visible = true;

            Session["fuPic2"] = fuPic2;

        }
        catch (Exception ex)
        {
            _common.ErrorLog("grievance.aspx-0007", ex.Message);
        }
    }
    protected void lbtnClosempGrievancee_Click(object sender, EventArgs e)
    {

    }
    #endregion




}