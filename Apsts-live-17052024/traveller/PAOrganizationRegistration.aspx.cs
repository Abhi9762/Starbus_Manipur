using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Npgsql;
using System.IO;
using System.Xml;
using System.Collections;

public partial class Auth_PAOrganizationRegistration : System.Web.UI.Page
{
    NpgsqlCommand MyCommand;
    sbBLL bll = new sbBLL();
    sbValidation _SecurityCheck = new sbValidation();
    sbSecurity _security = new sbSecurity();

    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    private sbCommonFunc _common = new sbCommonFunc();
    protected void Page_Init(object sender, System.EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.AddHeader("Cache-control", "no-store, must-revalidate,private,no-cache");
        Response.AddHeader("PRAGMA", "NO-Cache");
        Response.Expires = -1;
        Response.Expires = 0;
        Response.Cache.SetNoStore();
        Response.Cache.SetExpires(DateTime.Now.AddDays(-2));
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["_moduleName"] = "Organization";
            loadAllOrg();
            loadOrgType();
            loadStates();
            loadAllServices();
        }
    }

    #region "Methods"
    private void Errormsg(string msg)
    {
        string popup = _popup.modalPopupSmall("W", "Information", msg, "Close");
        Response.Write(popup);
    }
    private void loadOrgType() //M1
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            ddlOrgType.Items.Clear();
            DataTable dt = new DataTable();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.f_get_orgtype");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                ddlOrgType.DataSource = dt;
                ddlOrgType.DataTextField = "orgtypee";
                ddlOrgType.DataValueField = "orgtypecodee";
                ddlOrgType.DataBind();
            }
            ddlOrgType.Items.Insert(0, "SELECT");
            ddlOrgType.Items[0].Value = "0";
            ddlOrgType.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAOrgReg-M1", ex.Message.ToString());
            ddlOrgType.Items.Insert(0, "SELECT");
            ddlOrgType.Items[0].Value = "0";
            ddlOrgType.SelectedIndex = 0;
        }
    }
    private void loadStates()//M2
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            ddlOrgState.Items.Clear();
            ddlOrgAddrState.Items.Clear();
            DataTable dt = new DataTable();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.f_get_states");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                ddlOrgState.DataSource = dt;
                ddlOrgState.DataTextField = "stname";
                ddlOrgState.DataValueField = "stcode";
                ddlOrgState.DataBind();


                ddlOrgAddrState.DataSource = dt;
                ddlOrgAddrState.DataTextField = "stname";
                ddlOrgAddrState.DataValueField = "stcode";
                ddlOrgAddrState.DataBind();

            }
            ddlOrgState.Items.Insert(0, "SELECT");
            ddlOrgState.Items[0].Value = "0";
            ddlOrgState.SelectedIndex = 0;


            ddlOrgAddrState.Items.Insert(0, "SELECT");
            ddlOrgAddrState.Items[0].Value = "0";
            ddlOrgState.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            Errormsg(ex.ToString());
            _common.ErrorLog("PAOrgReg-M2", ex.Message.ToString());
            ddlOrgState.Items.Insert(0, "SELECT");
            ddlOrgState.Items[0].Value = "0";
            ddlOrgState.SelectedIndex = 0;

            ddlOrgAddrState.Items.Insert(0, "SELECT");
            ddlOrgAddrState.Items[0].Value = "0";
            ddlOrgAddrState.SelectedIndex = 0;
        }
    }
    private void loadDistricts(string stateCode) //M3
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            ddlOrgAddrDistrict.Items.Clear();
            DataTable dt = new DataTable();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.f_get_district");
            MyCommand.Parameters.AddWithValue("p_statecode", stateCode);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                ddlOrgAddrDistrict.DataSource = dt;
                ddlOrgAddrDistrict.DataTextField = "distname";
                ddlOrgAddrDistrict.DataValueField = "distcode";
                ddlOrgAddrDistrict.DataBind();
            }
            ddlOrgAddrDistrict.Items.Insert(0, "SELECT");
            ddlOrgAddrDistrict.Items[0].Value = "0";
            ddlOrgAddrDistrict.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAOrgReg-M3", ex.Message.ToString());
            ddlOrgAddrDistrict.Items.Insert(0, "SELECT");
            ddlOrgAddrDistrict.Items[0].Value = "0";
            ddlOrgAddrDistrict.SelectedIndex = 0;
        }
    }

    private void saveOrgDetail()//M4
    {
        try
        {
            string orgType, orgState, orgName, orgAbbr, orgAppName, orgAddr, orgAddrState, orgAddrDistrict, orgAddrPIN;
            string n1Name, n1Designation, n1MobileNo, n1LandlineNo, n1EmailId;
            string n2Name, n2Designation, n2MobileNo, n2LandlineNo, n2EmailId;
            string OfficeOrderDate, OfficeOrderNo, PossibleGoLiveDate, PossibleGoLiveURL;

            orgAbbr = tbAbbr.Text.Trim().ToUpper();

            if (Session["fuGovtLogo"] != null && (!fuGovtLogo.HasFile))
            {
                fuGovtLogo = (FileUpload)Session["fuGovtLogo"];
            }
            if (Session["fuDepartmentLogo"] != null && (!fuDepartmentLogo.HasFile))
            {
                fuDepartmentLogo = (FileUpload)Session["fuDepartmentLogo"];
            }
            if (Session["fuFavIcon"] != null && (!fuFavIcon.HasFile))
            {
                fuFavIcon = (FileUpload)Session["fuFavIcon"];
            }


            if (!fuGovtLogo.HasFile)
            {
                Errormsg("Please select Govt Logo.");
                return;
            }
            if (!fuDepartmentLogo.HasFile)
            {
                Errormsg("Please select Department Logo.");
                return;
            }
            if (!fuFavIcon.HasFile)
            {
                Errormsg("Please select Fav Icon.");
                return;
            }


            var folderPath = Server.MapPath("~/Auth/uploadLogos/" + orgAbbr);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string govtLogoExtension = Path.GetExtension(fuGovtLogo.PostedFile.FileName);
            string govtLogoFileName = "govtLogo" + govtLogoExtension;
            string govtlogoFullPath = folderPath + "/" + govtLogoFileName;
            string govtlogoDBPath = "Auth/uploadLogos/" + orgAbbr + "/" + govtLogoFileName;
            if (!File.Exists(govtlogoFullPath))
            {
                fuGovtLogo.PostedFile.SaveAs(govtlogoFullPath);
            }

            string deptLogoExtension = Path.GetExtension(fuDepartmentLogo.PostedFile.FileName);
            string deptLogoFileName = "deptLogo" + deptLogoExtension;
            string deptlogoFullPath = folderPath + "/" + deptLogoFileName;
            string deptlogoDBPath = "Auth/uploadLogos/" + orgAbbr + "/" + deptLogoFileName;
            if (!File.Exists(deptlogoFullPath))
            {
                fuDepartmentLogo.PostedFile.SaveAs(deptlogoFullPath);
            }

            string favIconExtension = Path.GetExtension(fuFavIcon.PostedFile.FileName);
            string favIconFileName = "favIcon" + favIconExtension;
            string favIconFullPath = folderPath + "/" + favIconFileName;
            string favIconDBPath = "Auth/uploadLogos/" + orgAbbr + "/" + favIconFileName;
            if (!File.Exists(favIconFullPath))
            {
                fuFavIcon.PostedFile.SaveAs(favIconFullPath);
            }

            createXML(folderPath);

            orgType = ddlOrgType.SelectedValue.ToString();
            orgState = ddlOrgState.SelectedValue.ToString();
            orgName = tbOrgName.Text.Trim().ToUpper();

            orgAppName = tbAppName.Text.Trim().ToUpper();
            orgAddr = tbOrgAddr.Text.Trim().ToUpper();
            orgAddrState = ddlOrgAddrState.SelectedValue.ToString();
            orgAddrDistrict = ddlOrgAddrDistrict.SelectedValue.ToString();
            orgAddrPIN = tbOrgPin.Text.Trim().ToUpper();

            n1Name = tbNodal1Name.Text.Trim().ToUpper();
            n1Designation = tbNodal1Desig.Text.Trim().ToUpper();
            n1MobileNo = tbNodal1MobileNo.Text.Trim();
            n1LandlineNo = tbNodal1LandlineNo.Text.Trim();
            n1EmailId = tbNodal1EmailId.Text.Trim().ToUpper();

            n2Name = tbNodal2Name.Text.Trim().ToUpper();
            n2Designation = tbNodal2Desig.Text.Trim().ToUpper();
            n2MobileNo = tbNodal2MobileNo.Text.Trim().ToUpper();
            n2LandlineNo = tbNodal2LandlineNo.Text.Trim().ToUpper();
            n2EmailId = tbNodal2EmailId.Text.Trim().ToUpper();

            OfficeOrderDate = tbOrderDate.Text.Trim();
            OfficeOrderNo = tbOrderNo.Text.Trim().ToUpper();
            PossibleGoLiveDate = tbGoLiveDate.Text.Trim();
            PossibleGoLiveURL = tbGoLiveURL.Text.Trim().ToUpper();

            //string UpdatedBy = Session["_UserCode"].ToString();
            //string IpAddress = HttpContext.Current.Request.UserHostAddress;

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.fun_org_reg");
            MyCommand.Parameters.AddWithValue("p_orgtypecode", int.Parse(orgType));
            MyCommand.Parameters.AddWithValue("p_statecode", orgState);
            MyCommand.Parameters.AddWithValue("p_orgname", orgName);
            MyCommand.Parameters.AddWithValue("p_orgabbr", orgAbbr);
            MyCommand.Parameters.AddWithValue("p_orgappname", orgAppName);

            MyCommand.Parameters.AddWithValue("p_orgofcaddress", orgAddr);
            MyCommand.Parameters.AddWithValue("p_orgofcstate", orgAddrState);
            MyCommand.Parameters.AddWithValue("p_orgofcdistrict", orgAddrDistrict);
            MyCommand.Parameters.AddWithValue("p_orgofcpin", orgAddrPIN);

            MyCommand.Parameters.AddWithValue("p_nodalofficer_1_name", n1Name);
            MyCommand.Parameters.AddWithValue("p_nodalofficer_1_desig", n1Designation);
            MyCommand.Parameters.AddWithValue("p_nodalofficer_1_mobileno", n1MobileNo);
            MyCommand.Parameters.AddWithValue("p_nodalofficer_1_landlineno", n1LandlineNo);
            MyCommand.Parameters.AddWithValue("p_nodalofficer_1_email", n1EmailId);

            MyCommand.Parameters.AddWithValue("p_nodalofficer_2_name", n2Name);
            MyCommand.Parameters.AddWithValue("p_nodalofficer_2_desig", n2Designation);
            MyCommand.Parameters.AddWithValue("p_nodalofficer_2_mobileno", n2MobileNo);
            MyCommand.Parameters.AddWithValue("p_nodalofficer_2_landlineno", n2LandlineNo);
            MyCommand.Parameters.AddWithValue("p_nodalofficer_2_email", n2EmailId);

            MyCommand.Parameters.AddWithValue("p_office_orderdate", OfficeOrderDate);
            MyCommand.Parameters.AddWithValue("p_office_orderno", OfficeOrderNo);
            MyCommand.Parameters.AddWithValue("p_possiblegolivedate", PossibleGoLiveDate);
            MyCommand.Parameters.AddWithValue("p_possibleurl", PossibleGoLiveURL);

            MyCommand.Parameters.AddWithValue("p_govtlogopath", govtlogoDBPath);
            MyCommand.Parameters.AddWithValue("p_deptlogopath", deptlogoDBPath);
            MyCommand.Parameters.AddWithValue("p_faviconpath", favIconDBPath);
            DataTable dt = new DataTable();
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                string pu = _popup.modalPopupSmall("S", "Success", "Organization Registration done successfully. Please map service with this Organization.", "Ok");
                Response.Write(pu);
                loadAllOrg();
            }
            else
            {
                string pu = _popup.modalPopupSmall("D", "Fail", "Organization Registration not done." + dt.TableName, "Ok");
                Response.Write(pu);
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.ToString());
            _common.ErrorLog("PAOrgReg-M4", ex.Message.ToString());
        }
    }
    public void clearOrgDetails()
    {
        ddlOrgType.SelectedValue = "0";
        ddlOrgState.SelectedValue = "0";
        tbOrgName.Text = "";

        tbAppName.Text = "";
        tbOrgAddr.Text = "";
        ddlOrgAddrState.SelectedValue = "0";
        tbOrgPin.Text = "";

        tbNodal1Name.Text = "";
        tbNodal1Desig.Text = "";
        tbNodal1MobileNo.Text = "";
        tbNodal1LandlineNo.Text = "";
        tbNodal1EmailId.Text = "";

        tbNodal2Name.Text = "";
        tbNodal2Desig.Text = "";
        tbNodal2MobileNo.Text = "";
        tbNodal2LandlineNo.Text = "";
        tbNodal2EmailId.Text = "";

        tbOrderDate.Text = "";
        tbOrderNo.Text = "";
        tbGoLiveDate.Text = "";
        tbGoLiveURL.Text = "";

    }

    public bool isValidOrgDetails()
    {
        int msgcount = 0;
        string msg = "";

        if (ddlOrgType.SelectedValue == "0")
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Select Organization Type.<br/>";
        }
        if (ddlOrgState.SelectedValue == "0")
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Select Organization State.<br/>";
        }
        if (_SecurityCheck.IsValidString(tbOrgName.Text, 5, tbOrgName.MaxLength) == false)
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Enter Organization Name.<br/>";
        }
        if (_SecurityCheck.IsValidString(tbAbbr.Text, 2, tbAbbr.MaxLength) == false)
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Enter Organization Abbreviation.<br/>";
        }
        if (_SecurityCheck.IsValidString(tbAppName.Text, 2, tbAppName.MaxLength) == false)
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Enter Organization App Name.<br/>";
        }

        if (_SecurityCheck.IsValidString(tbOrgAddr.Text, 2, tbOrgAddr.MaxLength) == false)
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Enter Organization Address.<br/>";
        }

        if (ddlOrgAddrState.SelectedValue == "0")
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Select Organization Address State.<br/>";
        }
        if (ddlOrgAddrDistrict.SelectedValue == "0")
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Select Organization Address District.<br/>";
        }
        if (_SecurityCheck.IsValidString(tbOrgPin.Text, tbOrgPin.MaxLength, tbOrgPin.MaxLength) == false)
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Enter Organization Address PIN.<br/>";
        }

        if (_SecurityCheck.IsValidString(tbNodal1Name.Text, 2, tbNodal1Name.MaxLength) == false)
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Enter First Nodal Officer Name.<br/>";
        }
        if (_SecurityCheck.IsValidString(tbNodal1Desig.Text, 2, tbNodal1Desig.MaxLength) == false)
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Enter First Nodal Officer Designation.<br/>";
        }
        if (_SecurityCheck.IsValidString(tbNodal1MobileNo.Text, tbNodal1MobileNo.MaxLength, tbNodal1MobileNo.MaxLength) == false)
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Enter First Nodal Officer Mobile No.<br/>";
        }
        if (_SecurityCheck.IsValidString(tbNodal1LandlineNo.Text, 0, tbNodal1LandlineNo.MaxLength) == false)
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Enter First Nodal Officer Landline No.<br/>";
        }
        //if (_SecurityCheck.IsValidString(tbNodal1EmailId.Text, 5, tbNodal1EmailId.MaxLength) == false)
        //{
        //    msgcount = msgcount + 1;
        //    msg = msg + msgcount.ToString() + ". Enter First Nodal Officer Email Id.<br/>";
        //}

        if (Session["fuGovtLogo"] == null)
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Select Government Logo.<br/>";
        }
        if (Session["fuDepartmentLogo"] == null)
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Select Department Logo.<br/>";
        }
        if (Session["fuFavIcon"] == null)
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Select Fav Icon.<br/>";
        }


        if (_SecurityCheck.IsValidString(tbOrderDate.Text, tbOrderDate.MaxLength, tbOrderDate.MaxLength) == false)
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Enter Office Order Date.<br/>";
        }
        if (_SecurityCheck.IsValidString(tbGoLiveDate.Text, tbGoLiveDate.MaxLength, tbGoLiveDate.MaxLength) == false)
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Enter Possible Go Live Date.<br/>";
        }

        if (msgcount > 0)
        {
            Errormsg(msg);
            return false;
        }

        return true;
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
    public void createXML(string path)
    {
        //Start writer
        XmlTextWriter writer = new XmlTextWriter(path + "/CommonData.xml", System.Text.Encoding.UTF8);

        //Start XM DOcument
        writer.WriteStartDocument(true);
        writer.Formatting = Formatting.Indented;
        writer.Indentation = 2;

        //ROOT Element
        writer.WriteStartElement("Data");

        //call create nodes method
        createNode(writer);

        writer.WriteEndElement();

        //End XML Document
        writer.WriteEndDocument();

        //Close writer
        writer.Close();
    }

    private void createNode(XmlTextWriter writer)
    {
        //parent node start

        writer.WriteStartElement("Page");

        writer.WriteStartElement("title_txt");
        writer.WriteString(tbAppName.Text.Trim().ToUpper());
        writer.WriteEndElement();

        writer.WriteStartElement("Ver_Name");
        writer.WriteString("Ver 1.0");
        writer.WriteEndElement();

        writer.WriteStartElement("logo_url");
        writer.WriteString("");
        writer.WriteEndElement();

        writer.WriteStartElement("govtLogo_url");
        writer.WriteString("");
        writer.WriteEndElement();

        writer.WriteStartElement("dept_Name");
        writer.WriteString(tbOrgName.Text.Trim().ToUpper());
        writer.WriteEndElement();

        writer.WriteStartElement("managed_by");
        writer.WriteString("National Informatics Centre, State Unit Dehradun, Uttarakhand (INDIA)");
        writer.WriteEndElement();

        writer.WriteStartElement("email");
        writer.WriteString("");
        writer.WriteEndElement();

        writer.WriteStartElement("tollfreeno");
        writer.WriteString("");
        writer.WriteEndElement();

        writer.WriteStartElement("facebooklink");
        writer.WriteString("");
        writer.WriteEndElement();

        writer.WriteStartElement("youtubelink");
        writer.WriteString("");
        writer.WriteEndElement();

        writer.WriteStartElement("contactNo1");
        writer.WriteString("");
        writer.WriteEndElement();

        writer.WriteStartElement("contactNo2");
        writer.WriteString("");
        writer.WriteEndElement();

        writer.WriteStartElement("contactNo2");
        writer.WriteString("");
        writer.WriteEndElement();

        writer.WriteStartElement("contactNo3");
        writer.WriteString("");
        writer.WriteEndElement();

        writer.WriteStartElement("contactNo4");
        writer.WriteString("");
        writer.WriteEndElement();

        writer.WriteStartElement("contactOfc1");
        writer.WriteString("");
        writer.WriteEndElement();

        writer.WriteStartElement("contactOfc2");
        writer.WriteString("");
        writer.WriteEndElement();

        writer.WriteStartElement("contactOfc3");
        writer.WriteString("");
        writer.WriteEndElement();

        writer.WriteStartElement("contactOfc4");
        writer.WriteString("");
        writer.WriteEndElement();

        writer.WriteStartElement("landlineno");
        writer.WriteString("");
        writer.WriteEndElement();

        writer.WriteStartElement("faxno");
        writer.WriteString("");
        writer.WriteEndElement();

        writer.WriteStartElement("stdcode");
        writer.WriteString("");
        writer.WriteEndElement();

        writer.WriteStartElement("developed_by");
        writer.WriteString("National Informatics Centre, State Unit Dehradun, Uttarakhand (INDIA)");
        writer.WriteEndElement();

        writer.WriteStartElement("pios");
        writer.WriteString("");
        writer.WriteEndElement();

        writer.WriteStartElement("rti_manual1");
        writer.WriteString("");
        writer.WriteEndElement();

        writer.WriteStartElement("rti_manual2");
        writer.WriteString("");
        writer.WriteEndElement();

        writer.WriteStartElement("rti_manual3");
        writer.WriteString("");
        writer.WriteEndElement();

        writer.WriteStartElement("homepage_img1_url");
        writer.WriteString("");
        writer.WriteEndElement();

        writer.WriteStartElement("homepage_img2_url");
        writer.WriteString("");
        writer.WriteEndElement();

        writer.WriteStartElement("homepage_img3_url");
        writer.WriteString("");
        writer.WriteEndElement();

        writer.WriteStartElement("homepage_img4_url");
        writer.WriteString("");
        writer.WriteEndElement();

        writer.WriteStartElement("homepage_img5_url");
        writer.WriteString("");
        writer.WriteEndElement();

        writer.WriteStartElement("homepage_img6_url");
        writer.WriteString("");
        writer.WriteEndElement();

        writer.WriteStartElement("mentor1image");
        writer.WriteString("");
        writer.WriteEndElement();

        writer.WriteStartElement("mentor1name");
        writer.WriteString("");
        writer.WriteEndElement();

        writer.WriteStartElement("mentor1designation");
        writer.WriteString("");
        writer.WriteEndElement();

        writer.WriteStartElement("mentor2image");
        writer.WriteString("");
        writer.WriteEndElement();

        writer.WriteStartElement("mentor2name");
        writer.WriteString("");
        writer.WriteEndElement();

        writer.WriteStartElement("mentor2designation");
        writer.WriteString("");
        writer.WriteEndElement();

        writer.WriteStartElement("mentor3image");
        writer.WriteString("");
        writer.WriteEndElement();

        writer.WriteStartElement("mentor3name");
        writer.WriteString("");
        writer.WriteEndElement();

        writer.WriteStartElement("mentor3designation");
        writer.WriteString("");
        writer.WriteEndElement();

        writer.WriteStartElement("mentor4image");
        writer.WriteString("");
        writer.WriteEndElement();

        writer.WriteStartElement("mentor4name");
        writer.WriteString("");
        writer.WriteEndElement();

        writer.WriteStartElement("mentor4designation");
        writer.WriteString("");
        writer.WriteEndElement();

    }

    private void loadAllOrg()//M5
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            DataTable dt = new DataTable();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.fun_get_org");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    gvOrg.DataSource = dt;
                    gvOrg.DataBind();
                    pnlNoOrg.Visible = false;
                }
                else
                {
                    pnlNoOrg.Visible = true;
                }
            }
            else
            {
                Errormsg("Organization didn't load. ");
                _common.ErrorLog("PAOrgReg-M5", dt.TableName.ToString());
            }
        }
        catch (Exception ex)
        {
            Errormsg("Organization didn't load. " + ex.ToString());
            _common.ErrorLog("PAOrgReg-M5", ex.Message.ToString());
        }
    }

    private void loadAllServices()//M6
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            DataTable dt = new DataTable();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.fun_get_allservices");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    lvAllServices.DataSource = dt;
                    lvAllServices.DataBind();

                    pnlAllService.Visible = true;
                    pnlNoAllService.Visible = false;
                }
                else
                {
                    pnlAllService.Visible = false;
                    pnlNoAllService.Visible = true;
                }
            }
            else
            {
                Errormsg("All Services didn't load. ");
                _common.ErrorLog("PAOrgReg-M6", dt.TableName.ToString());
            }
        }
        catch (Exception ex)
        {
            Errormsg("All Services didn't load. ");
            _common.ErrorLog("PAOrgReg-M6", ex.Message.ToString());
        }
    }

    private void loadOrgServices(string orgCode)//M7
    {
        try
        {
            lvODServices.DataSource = null;
            lvODServices.DataBind();
            lblODServiceListHeader.Text = "";
            lblODNoService.Text = "Services are not Mapped";

            MyCommand = new NpgsqlCommand();
            DataTable dt = new DataTable();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.fun_get_orgservices");
            MyCommand.Parameters.AddWithValue("p_orgcode", orgCode);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    lvODServices.DataSource = dt;
                    lvODServices.DataBind();
                    lblODNoService.Text = "";
                    lblODServiceListHeader.Text = "Mapped Services";
                }               
            }
            else
            {
                Errormsg("Organization Services didn't load. "+ dt.TableName);
                _common.ErrorLog("PAOrgReg-M7", dt.TableName.ToString());
            }
        }
        catch (Exception ex)
        {
            Errormsg("Organization Services didn't load. "+ ex.Message.ToString());
            _common.ErrorLog("PAOrgReg-M7", ex.Message.ToString());
        }
    }

    private void saveOrgServices(string orgCode, string[] serviceArr)//M8
    {
        try
        {
            if (_security.isSessionExist(Session["_UserCode"]) == true)
            {
                Session["_UserCode"] = Session["_UserCode"];
            }
            else
            {
                Response.Redirect("errorpage.aspx");
            }

            MyCommand = new NpgsqlCommand();
            DataTable dt = new DataTable();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.fun_org_reg_service");
            MyCommand.Parameters.AddWithValue("p_orgcode", orgCode);
            MyCommand.Parameters.AddWithValue("p_sercatcode", serviceArr);
            MyCommand.Parameters.AddWithValue("p_updatedby", Session["_UserCode"].ToString());
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                string pu = _popup.modalPopupSmall("S", "Success", "Services have been mapped successfully.", "Ok");
                Response.Write(pu);
                lblHeaderOrgName.Text = "";
                hfOrgCodeForSerMap.Value = "0";
                allPanelsHide();
                pnlOrgReg.Visible = true;
            }
            else
            {
                Errormsg("Services cannot be mapped with this organization due to a technical issue");
                _common.ErrorLog("PAOrgReg-M8", dt.TableName.ToString());
            }
        }
        catch (Exception ex)
        {
            Errormsg("Services cannot be mapped with this organization due to a technical issue");
            _common.ErrorLog("PAOrgReg-M8", ex.Message.ToString());
        }
    }

    #endregion

    #region "Events"

    // --------------- Org List Start --------------------
    protected void gvOrg_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "VIEWORG")
        {
            int rowIndex = int.Parse(e.CommandArgument.ToString());
            string orgCode = (string)this.gvOrg.DataKeys[rowIndex]["orcode"];
            string orgAppName = (string)this.gvOrg.DataKeys[rowIndex]["orappname"];
            string orgName = (string)this.gvOrg.DataKeys[rowIndex]["orname"];
            string orgtype = (string)this.gvOrg.DataKeys[rowIndex]["ortype"];
            string regDate = (string)this.gvOrg.DataKeys[rowIndex]["orregdate"];

            lblODAppName.Text = orgAppName + " (" + orgtype + ")";
            lblODDepartment.Text = orgName;
            lblODRegDate.Text = regDate;

            string orgAddr = (string)this.gvOrg.DataKeys[rowIndex]["orofcaddress"];
            string orgAddState = (string)this.gvOrg.DataKeys[rowIndex]["orofcaddstname"];
            string orgAddDistrict = (string)this.gvOrg.DataKeys[rowIndex]["orofcdistrictname"];
            string orgAddPin = (string)this.gvOrg.DataKeys[rowIndex]["orofcpin"];

            lblODAddress.Text = orgAddr + ", " + orgAddState + ", " + orgAddDistrict + ", " + orgAddPin;

            string n1Name = (string)this.gvOrg.DataKeys[rowIndex]["n1name"];
            string n1Desig = (string)this.gvOrg.DataKeys[rowIndex]["n1desig"];
            string n1Mobile = (string)this.gvOrg.DataKeys[rowIndex]["n1mobileno"];
            string n1Landline = (string)this.gvOrg.DataKeys[rowIndex]["no1landlineno"];
            string n1Email = (string)this.gvOrg.DataKeys[rowIndex]["n1email"];



            lblODNO1NameDesig.Text = n1Name + " (" + n1Desig + ")";
            string n1mobileLandline = n1Mobile;
            if (n1Landline.Length > 0)
            {
                n1mobileLandline = n1mobileLandline + " | " + n1Landline;
            }
            lblODNO1MobileLandline.Text = n1mobileLandline;
            lblODNO1Email.Text = n1Email;


            string n2Name = (string)this.gvOrg.DataKeys[rowIndex]["n2name"];
            string n2Desig = (string)this.gvOrg.DataKeys[rowIndex]["n2desig"];
            string n2Mobile = (string)this.gvOrg.DataKeys[rowIndex]["n2mobileno"];
            string n2Landline = (string)this.gvOrg.DataKeys[rowIndex]["n2landlineno"];
            string n2Email = (string)this.gvOrg.DataKeys[rowIndex]["n2email"];

            if (n2Name.Length > 0)
            {
                string n2NameDesig = n2Name;
                if (n2Name.Length > 0)
                {
                    n2NameDesig = n2NameDesig + " (" + n2Desig + ")";
                }
                lblODNO2NameDesig.Text = n2NameDesig;

                string n2mobileLandline = "NA";

                if (n2Landline.Length > 0)
                {
                    n2mobileLandline = n2Mobile;

                    if (n2Landline.Length > 0)
                    {
                        n2mobileLandline = n2mobileLandline + " | " + n2Landline;
                    }
                }
                lblODNO2MobileLandline.Text = n2mobileLandline;

                if (n2Email.Length > 0)
                {
                    lblODNO2Email.Text = n2Email;
                }
                else
                {
                    lblODNO2Email.Text = "NA";
                }
            }
            else
            {
                lblODNO2NameDesig.Text = "NA";
            }


            string orgLogoUrl = (string)this.gvOrg.DataKeys[rowIndex]["orlogo"];
            string orgGovtLogoUrl = (string)this.gvOrg.DataKeys[rowIndex]["orgovtlogo"];

            //orcode,ortypecode,ortype,orstcode,orstname,orname,orabbr,orappname,
            //orofcaddress,orofcstate,orofcaddstname,orofcdistrict,orofcdistrictname,orofcpin,
            //n1name,n1desig,n1mobileno,no1landlineno,n1email,
            //n2name,n2desig,n2mobileno,n2landlineno,n2email,
            //orregdate,orpsbllivedate,psblurl,ofcorderdate,ofcorderno,orlogo,orgovtlogo,orfavicon

            imgODGovtLogo.ImageUrl = "../" + orgGovtLogoUrl;
            imgODLogo.ImageUrl = "../" + orgLogoUrl;

            string orderDate = (string)this.gvOrg.DataKeys[rowIndex]["ofcorderdate"];
            string orderNo = (string)this.gvOrg.DataKeys[rowIndex]["ofcorderno"];
            string possibleLiveDate = (string)this.gvOrg.DataKeys[rowIndex]["orpsbllivedate"];
            string possibleLiveURL = (string)this.gvOrg.DataKeys[rowIndex]["psblurl"];


            lblODOderDate.Text = orderDate;
            if (orderNo.Length <= 0)
            {
                orderNo = "NA";
            }
            lblODOderNo.Text = orderNo;
            lblODPossibleLiveDate.Text = possibleLiveDate;
            if (possibleLiveURL.Length <= 0)
            {
                possibleLiveURL = "NA";
            }
            lblODPossibleLiveURL.Text = possibleLiveURL;

            loadOrgServices(orgCode);

            allPanelsHide();
            pnlOrgDetailView.Visible = true;

        }
        else if (e.CommandName == "MAPSERVICE")
        {
            int rowIndex = int.Parse(e.CommandArgument.ToString());
            string orgCode = (string)this.gvOrg.DataKeys[rowIndex]["orcode"];
            string orgAppName = (string)this.gvOrg.DataKeys[rowIndex]["orappname"];
            string orgName = (string)this.gvOrg.DataKeys[rowIndex]["orname"];

            // Errormsg("MAPSERVICE - " + orgCode + " - " + orgAppName + " - " + orgName);
            lblHeaderOrgName.Text = orgAppName + " (" + orgName + ")";
            hfOrgCodeForSerMap.Value = orgCode;

            allPanelsHide();
            pnlOrgServiceMap.Visible = true;

        }
        else if (e.CommandName == "UPDATEORG")
        {
            int rowIndex = int.Parse(e.CommandArgument.ToString());
            string orgCode = (string)this.gvOrg.DataKeys[rowIndex]["orcode"];
            string orgAppName = (string)this.gvOrg.DataKeys[rowIndex]["orappname"];
            string orgName = (string)this.gvOrg.DataKeys[rowIndex]["orname"];

            Errormsg("UPDATE - " + orgCode + " - " + orgAppName + " - " + orgName);

        }
        else
        {
            Errormsg("Something went wrong on click");
            _common.ErrorLog("PAOrgReg-EvntGV", "Invalid CommandName in gvOrg GridView");
        }
    }

    // --------------- Org List End --------------------


    // --------------- Org Registration Start --------------------
    protected void lbtnHelp_Click(Object sender, EventArgs e)
    {
        string msg = "* fields are mandatory.<br/>";
        string pu = _popup.modalPopupSmall("S", "Note", msg, "Ok");
        Response.Write(pu);
    }

    protected void ddlOrgAddrState_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        loadDistricts(ddlOrgAddrState.SelectedValue);
    }

    protected void lbtnSave_Click(Object sender, EventArgs e)
    {

        if (isValidOrgDetails() == false)
        {
            return;
        }
        lblConfirmMsg.Text = "Do you want to save records ?";
        mpConfirm.Show();


        //string pu = _popup.modalPopupSmall("S", "Success", "Organization Registration done successfully. Please map service with this Organization.", "Ok");
        //Response.Write(pu);
        //pnlOrgReg.Visible = false;
        //pnlOrgServiceMap.Visible = true;

    }
    protected void lbtnReset_Click(Object sender, EventArgs e)
    {
        clearOrgDetails();
    }
    protected void lbtnSavempConfirm_Click(Object sender, EventArgs e)
    {


        saveOrgDetail();
    }

    protected void btnfuGovtLogo_Click(object sender, EventArgs e)
    {
        try
        {
            if (!fuGovtLogo.HasFile)
            {
                Errormsg("Please select report first");
                return;
            }
            if (!checkFileExtention(fuGovtLogo))
            {
                Errormsg("File must be png, jpg, jpeg type");
                return;
            }
            string _fileFormat = GetMimeDataOfFile(fuGovtLogo.PostedFile);
            if ((_fileFormat == "image/png") || (_fileFormat == "image/jpeg"))
            {
            }
            else
            {
                Errormsg("File must png, jpg, jpeg type");
                return;
            }
            decimal size = Math.Round((Convert.ToDecimal(fuGovtLogo.PostedFile.ContentLength) / Convert.ToDecimal(1024)), 2);
            if (size > 500 || size < 5)
            {
                Errormsg("File size must be between 5 kb to 500 kb");
                return;
            }
            byte[] PhotoImage = null;
            PhotoImage = convertByteFile(fuGovtLogo);
            imgGovtLogo.ImageUrl = GetImage(PhotoImage);
            imgGovtLogo.Visible = true;

            Session["fuGovtLogo"] = fuGovtLogo;

        }
        catch (Exception ex)
        {

        }
    }
    protected void btnfuDepartmentLogo_Click(object sender, EventArgs e)
    {
        try
        {
            if (!fuDepartmentLogo.HasFile)
            {
                Errormsg("Please select report first");
                return;
            }
            if (!checkFileExtention(fuDepartmentLogo))
            {
                Errormsg("File must be png, jpg, jpeg type");
                return;
            }
            string _fileFormat = GetMimeDataOfFile(fuDepartmentLogo.PostedFile);
            if ((_fileFormat == "image/png") || (_fileFormat == "image/jpeg"))
            {
            }
            else
            {
                Errormsg("File must png, jpg, jpeg type");
                return;
            }
            decimal size = Math.Round((Convert.ToDecimal(fuDepartmentLogo.PostedFile.ContentLength) / Convert.ToDecimal(1024)), 2);
            if (size > 500 || size < 5)
            {
                Errormsg("File size must be between 5 kb to 500 kb");
                return;
            }
            byte[] PhotoImage = null;
            PhotoImage = convertByteFile(fuDepartmentLogo);
            imgDepartmentLogo.ImageUrl = GetImage(PhotoImage);
            imgDepartmentLogo.Visible = true;

            Session["fuDepartmentLogo"] = fuDepartmentLogo;

        }
        catch (Exception ex)
        {

        }
    }
    protected void btnFavIcon_Click(object sender, EventArgs e)
    {
        try
        {
            if (!fuFavIcon.HasFile)
            {
                Errormsg("Please select report first");
                return;
            }
            if (!checkFileExtention(fuFavIcon))
            {
                Errormsg("File must be png, jpg, jpeg type");
                return;
            }
            string _fileFormat = GetMimeDataOfFile(fuFavIcon.PostedFile);
            if ((_fileFormat == "image/png") || (_fileFormat == "image/jpeg"))
            {
            }
            else
            {
                Errormsg("File must png, jpg, jpeg type");
                return;
            }
            decimal size = Math.Round((Convert.ToDecimal(fuFavIcon.PostedFile.ContentLength) / Convert.ToDecimal(1024)), 2);
            if (size > 500 || size < 5)
            {
                Errormsg("File size must be between 5 kb to 200 kb");
                return;
            }
            byte[] PhotoImage = null;
            PhotoImage = convertByteFile(fuFavIcon);
            imgFavIcon.ImageUrl = GetImage(PhotoImage);
            imgFavIcon.Visible = true;

            Session["fuFavIcon"] = fuFavIcon;

        }
        catch (Exception ex)
        {

        }
    }

    // --------------- Org Registration End --------------------

    // --------------- Org Map Service Start --------------------
    protected void lbtnSaveMapService_Click(Object sender, EventArgs e)
    {
        ArrayList a=new ArrayList();        
        foreach (ListViewItem li in lvAllServices.Items)
        {
            Label lblSerCatCode = (Label)li.FindControl("lblServiceCatCode");
            Label lblSerCode = (Label)li.FindControl("lblServiceCode");
            CheckBox cb = (CheckBox)li.FindControl("cbService");
            if (cb.Checked)
            {
                a.Add(lblSerCatCode.Text + "_" + lblSerCode.Text);
            }
        }

        string[] ar = (string[])a.ToArray(typeof(string));
        if (ar.Length <= 0)
        {
            Errormsg("Please select services");
            return;
        }
        if (hfOrgCodeForSerMap.Value == "0")
        {
            Errormsg("Something went wrong. Please try again.");
            return;
        }

        saveOrgServices(hfOrgCodeForSerMap.Value.ToString(), ar);



    }
    protected void lbtnCancelMapService_Click(Object sender, EventArgs e)
    {
        lblHeaderOrgName.Text = "";
        hfOrgCodeForSerMap.Value = "0";
        allPanelsHide();
        pnlOrgReg.Visible = true;
    }

    // --------------- Org Map Service End --------------------

    // --------------- Org View Details Start --------------------

    protected void lbtnCloseOrgDetail_Click(Object sender, EventArgs e)
    {
        allPanelsHide();
        pnlOrgReg.Visible = true;

    }

    // --------------- Org View Details End --------------------

    private void allPanelsHide()
    {
        pnlOrgReg.Visible = false;
        pnlOrgServiceMap.Visible = false;
        pnlOrgDetailView.Visible = false;
    }

    #endregion
}