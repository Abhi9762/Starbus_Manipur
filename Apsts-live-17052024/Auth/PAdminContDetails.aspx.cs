using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Text.RegularExpressions;

public partial class Auth_PAdminContDetails : BasePage 
{

    private Regex _alphaNumericPattern;
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
   
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["_moduleName"] = "Contact Details";
        if (!IsPostBack)
        {
            Random randomclass = new Random();
            Session["rndNoCheck"] = randomclass.Next().ToString();
            hidtoken.Value = Session["rndNoCheck"].ToString();
            loadContactData();
            loadSocialData();
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
    private void Successmsg(string msg)
    {
        string popup = _popup.modalPopupSmall("S", "Success", msg, "Close");
        Response.Write(popup);
    }
    private void loadContactData()//M1
    {
        try
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("../CommonData.xml"));
            XmlNodeList email = doc.GetElementsByTagName("email");
            tbEmailID.Text = email.Item(0).InnerXml.Replace("[at]", "@").Replace("[dot]", ".");
            XmlNodeList Contact1 = doc.GetElementsByTagName("contactNo1");
            tbContact1.Text = Contact1.Item(0).InnerXml;
            XmlNodeList contactOfc1 = doc.GetElementsByTagName("contactOfc1");
            tbOfficeNameContact1.Text = contactOfc1.Item(0).InnerXml;

            XmlNodeList contactOfc1Local = doc.GetElementsByTagName("contactOfc1Local");
            tbOfficeNameContact1L.Text = contactOfc1Local.Item(0).InnerXml;
            XmlNodeList Contact2 = doc.GetElementsByTagName("contactNo2");
            tbContact2.Text = Contact2.Item(0).InnerXml;
            XmlNodeList contactOfc2 = doc.GetElementsByTagName("contactOfc2");
            tbOfficeNameContact2.Text = contactOfc2.Item(0).InnerXml;

            XmlNodeList contactOfc2Local = doc.GetElementsByTagName("contactOfc2Local");
            tbOfficeNameContact2L.Text = contactOfc2Local.Item(0).InnerXml;
            XmlNodeList tollfreeno = doc.GetElementsByTagName("tollfreeno");
            tbTollFree.Text = tollfreeno.Item(0).InnerXml;
            XmlNodeList landlineno = doc.GetElementsByTagName("landlineno");
            tbLandline.Text = landlineno.Item(0).InnerXml;
            XmlNodeList faxno = doc.GetElementsByTagName("faxno");
            tbFaxNo.Text = faxno.Item(0).InnerXml;
            XmlNodeList webinfomanagername = doc.GetElementsByTagName("webinfomanagername");
            tbManagerName.Text = webinfomanagername.Item(0).InnerXml;
            XmlNodeList webinfomanagercontact = doc.GetElementsByTagName("webinfomanagercontact");
            tbManagerContact.Text = webinfomanagercontact.Item(0).InnerXml;
            XmlNodeList webinfomanagermail = doc.GetElementsByTagName("webinfomanagermail");
            tbManagermail.Text = webinfomanagermail.Item(0).InnerXml.Replace("[at]", "@").Replace("[dot]", ".");

            
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminContDetails.aspx-0001", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
        }
    }
    private void updateXmlContactData()//M2
    {
        try
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("../CommonData.xml"));
            XmlNodeList email = doc.GetElementsByTagName("email");
            email.Item(0).InnerXml = tbEmailID.Text.Replace("@", "[at]").Replace(".", "[dot]");
            XmlNodeList Contact1 = doc.GetElementsByTagName("contactNo1");
            Contact1.Item(0).InnerXml = tbContact1.Text;
            XmlNodeList Contact2 = doc.GetElementsByTagName("contactNo2");
            Contact2.Item(0).InnerXml = tbContact2.Text;
            XmlNodeList contactOfc1_xml = doc.GetElementsByTagName("contactOfc1");
            contactOfc1_xml.Item(0).InnerXml = tbOfficeNameContact1.Text;

            XmlNodeList contactOfc1L_xml = doc.GetElementsByTagName("contactOfc1Local");
            contactOfc1L_xml.Item(0).InnerXml = tbOfficeNameContact1L.Text;


            XmlNodeList contactOfc2_xml = doc.GetElementsByTagName("contactOfc2");
            contactOfc2_xml.Item(0).InnerXml = tbOfficeNameContact2.Text;

            XmlNodeList contactOfc2loacl_xml = doc.GetElementsByTagName("contactOfc2Local");
            contactOfc2loacl_xml.Item(0).InnerXml = tbOfficeNameContact2L.Text;


            XmlNodeList tollfreeno_xml = doc.GetElementsByTagName("tollfreeno");
            tollfreeno_xml.Item(0).InnerXml = tbTollFree.Text;
            XmlNodeList landlineno = doc.GetElementsByTagName("landlineno");
            landlineno.Item(0).InnerXml = tbLandline.Text;
            XmlNodeList faxno = doc.GetElementsByTagName("faxno");
            faxno.Item(0).InnerXml = tbFaxNo.Text;

            XmlNodeList webinfomanagername = doc.GetElementsByTagName("webinfomanagername");
            webinfomanagername.Item(0).InnerXml = tbManagerName.Text;
            XmlNodeList webinfomanagercontact = doc.GetElementsByTagName("webinfomanagercontact");
            webinfomanagercontact.Item(0).InnerXml = tbManagerContact.Text;


            XmlNodeList webinfomanagermail = doc.GetElementsByTagName("webinfomanagermail");
            webinfomanagermail.Item(0).InnerXml = tbManagermail.Text.Replace("@", "[at]").Replace(".", "[dot]");

            //email.Item(0).InnerXml = tbEmailID.Text.Replace("@", "[at]").Replace(".", "[dot]");

            doc.Save(Server.MapPath("../CommonData.xml"));
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminContDetails.aspx-0002", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
        }
    }
    protected void ContactHistoryList()//M3
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_contact_history");
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                gvcontactHistory.DataSource = dt;
                gvcontactHistory.DataBind();
                gvcontactHistory.Visible = true;
                lblNocontacthistory.Visible = false;
            }
            else
            {
                gvcontactHistory.Visible = false;
                lblNocontacthistory.Visible = true;
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminContDetails.aspx-0003", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
            gvcontactHistory.Visible = false;
            lblNocontacthistory.Visible = true;
        }
    }
    protected void loadSocialHistory()//M4
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_social_history");
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                gvSocialHistory.DataSource = dt;
                gvSocialHistory.DataBind();
                gvSocialHistory.Visible = true;
                lblNosocialhistory.Visible = false;
            }
            else
            {
                gvSocialHistory.Visible = false;
                lblNosocialhistory.Visible = true;
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminContDetails.aspx-0004", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
            gvSocialHistory.Visible = false;
            lblNosocialhistory.Visible = true;
        }
    }
    private void loadSocialData()//M5
    {
        try
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("../CommonData.xml"));
            XmlNodeList facebooklink = doc.GetElementsByTagName("facebooklink");
            tbFacebooklink.Text = facebooklink.Item(0).InnerXml;
            XmlNodeList youtubelink = doc.GetElementsByTagName("youtubelink");
            tbYoutubeLink.Text = youtubelink.Item(0).InnerXml;
            XmlNodeList instalink = doc.GetElementsByTagName("instalink");
            tbinstagram.Text = instalink.Item(0).InnerXml;
            XmlNodeList twitterlink = doc.GetElementsByTagName("twitterlink");
            tbtwitter.Text = twitterlink.Item(0).InnerXml;

        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminContDetails.aspx-0005", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
        }
    }
    private void updateXmlSocialData()//M6
    {
        try
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("../CommonData.xml"));
            XmlNodeList facebooklink = doc.GetElementsByTagName("facebooklink");
            facebooklink.Item(0).InnerXml = tbFacebooklink.Text;
            XmlNodeList youtubelink = doc.GetElementsByTagName("youtubelink");
            youtubelink.Item(0).InnerXml = tbYoutubeLink.Text;
            XmlNodeList instalink = doc.GetElementsByTagName("instalink");
            instalink.Item(0).InnerXml = tbinstagram.Text;
            XmlNodeList twitterlink = doc.GetElementsByTagName("twitterlink");
            twitterlink.Item(0).InnerXml = tbtwitter.Text;
            doc.Save(Server.MapPath("../CommonData.xml"));
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminContDetails.aspx-0006", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
        }
    }
    private bool validValueContact()//M7
    {
        try
        {
            int msgcount = 0;
            string msg = "";
            if (_validation.isValideMailID(tbEmailID.Text) == false)
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Enter Valid Email ID.<br/>";
            }
            if (tbLandline.Text.Length>0)
            {
                if (_validation.IsValidString(tbLandline.Text, 11, tbLandline.MaxLength) == false)
                {
                    msgcount = msgcount + 1;
                    msg = msg + msgcount.ToString() + ". Enter Valid Landline No.<br/>";

                }

            }
            
            if (_validation.IsValidString(tbContact1.Text, 10, tbContact1.MaxLength) == false)
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Enter Valid Primary Contact Number<br/>";
            }
            if (_validation.IsValidString(tbOfficeNameContact1.Text, 3, tbOfficeNameContact1.MaxLength) == false)
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Enter valid office name for Primary contact Number<br/>";
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
            _common.ErrorLog("PAdminContDetails.aspx-0007", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
            return false;
        }
    }
    protected void insertContact()//M8
    {
        try
        {
            string Mresult = "";
            string contNo1 = tbContact1.Text.Trim();
            string contOfc1 = tbOfficeNameContact1.Text.Trim();
            string contOfc1Local = tbOfficeNameContact1L.Text.Trim();
            string contNo2 = tbContact2.Text.Trim();
            string contOfc2 = tbOfficeNameContact2.Text.Trim();
            string contOfc2Local = tbOfficeNameContact2L.Text.Trim();
            string ManagerName = tbManagerName.Text.Trim();
            string ManagerContact = tbManagerContact.Text.Trim();
            string ManagerEmail = tbManagermail.Text.Trim();
            string tollfreeno = tbTollFree.Text.Trim();
            string STDCode = "0";
            string landlineno = tbLandline.Text.Trim();
            string faxno = tbFaxNo.Text.Trim();
            string email = tbEmailID.Text.Trim();
            string IPAddress = HttpContext.Current.Request.UserHostAddress;
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.portal_contactdatas");
            MyCommand.Parameters.AddWithValue("p_email", email);
            MyCommand.Parameters.AddWithValue("p_contactno1", contNo1);
            MyCommand.Parameters.AddWithValue("p_contoffice1", contOfc1);
            MyCommand.Parameters.AddWithValue("p_contoffice1local", contOfc1Local);
            MyCommand.Parameters.AddWithValue("p_contactno2", contNo2);
            MyCommand.Parameters.AddWithValue("p_contoffice2", contOfc2);
            MyCommand.Parameters.AddWithValue("p_contoffice2local", contOfc2Local);
            MyCommand.Parameters.AddWithValue("p_manager_name", ManagerName);
            MyCommand.Parameters.AddWithValue("p_manager_contact", ManagerContact);
            MyCommand.Parameters.AddWithValue("p_manager_email", ManagerEmail);
            MyCommand.Parameters.AddWithValue("p_tollfreeno", tollfreeno);
            MyCommand.Parameters.AddWithValue("p_std_code", STDCode);
            MyCommand.Parameters.AddWithValue("p_landlineno", landlineno);
            MyCommand.Parameters.AddWithValue("p_faxno", faxno);
            MyCommand.Parameters.AddWithValue("p_updatedby", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("p_ipaddress", IPAddress);
            Mresult = bll.UpdateAll(MyCommand);
            if (Mresult == "Success")
            {
                updateXmlContactData();
                Successmsg("Details have been saved successfully");
            }
            else
            {
                Errormsg("Error Occurred");
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminContDetails.aspx-0008", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
        }
    }
    protected void insertSocial()//M9
    {
        try
        {
            string Mresult = "";
            string youtubelink = tbYoutubeLink.Text.Trim();
            string facebooklink = tbFacebooklink.Text.Trim();
            string insta = tbinstagram.Text.Trim();
            string twitter = tbtwitter.Text.Trim();
            string IPAddress = HttpContext.Current.Request.UserHostAddress;
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.portal_socialdatas");
            MyCommand.Parameters.AddWithValue("p_ytlink", youtubelink);
            MyCommand.Parameters.AddWithValue("p_fblink", facebooklink);
            MyCommand.Parameters.AddWithValue("p_insta", insta);
            MyCommand.Parameters.AddWithValue("p_twitter", twitter);
            MyCommand.Parameters.AddWithValue("p_updatedby", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("p_ipaddress", IPAddress);


            Mresult = bll.UpdateAll(MyCommand);
            if (Mresult == "Success")
            {
                updateXmlSocialData();
                Successmsg("Data inserted Successfully");
            }
            else
            {
                Errormsg("Error Occurred");
            }


        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminContDetails.aspx-0009", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
        }

    }
    private void resetContact()
    {
        tbContact1.Text = "";
        tbContact2.Text = "";
        tbEmailID.Text = "";
        tbFaxNo.Text = "";
        tbOfficeNameContact1.Text = "";
        tbOfficeNameContact2.Text = "";
        tbTollFree.Text = "";
        tbLandline.Text = "";
        tbManagermail.Text = "";
        tbManagerName.Text = "";
        tbManagermail.Text = "";
        tbOfficeNameContact1L.Text = "";
        tbOfficeNameContact2L.Text = "";

    }
    private void resetSocial()
    {
        tbFacebooklink.Text = "";
        tbYoutubeLink.Text = "";
        tbinstagram.Text = "";
        tbtwitter.Text = "";

    }
    #endregion
    #region "Event"
    protected void lbtnHelpConatct_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Errormsg("All marked * fields are mandatory<br/>1. Email - ID will be use for contact and queries.<br/> 2. Contact Numbers must be of Max. 10 characters length and accept only numbers,<br/> 3. Toll Free Number must be of Max. 16 characters length and accept only numbers");
    }
    protected void lbtnHelpSocial_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Errormsg("All marked * fields are mandatory<br/>Facebook Link<br/>1.Facebook link will be use for sidebar Social links.<br/>2.Related Department Facebook page.<br/>Youtube Link<br/>1. Youtube link will be use for sidebar Social links.<br/>2. Related Department Youtube Channel.<br/>Instagram Link<br/>1. Instagram link will be use for sidebar Social links.<br/>2. Related Department Instagram page.<br/>Twitter Link<br/>1. Twitter link will be use for sidebar Social links.<br/>2.Related Department Twitter page.");
    }
    protected void lbtnViewHistoryContact_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        mpcontactHistory.Show();
        ContactHistoryList();
    }

    public bool isValidLink(string valCheck)
    {
        try
        {
            if ((valCheck.Length == 0))
            {
                return false;
            }

            if ((valCheck.Length > 50))
            {
                return false;
            }

            _alphaNumericPattern = new Regex("%" + valCheck + "%");
            return _alphaNumericPattern.IsMatch(valCheck);
        }
        catch (Exception ex)
        {
            return false;
        }

    }
    protected void lbtnSaveSocial_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        try
        {

            if (tbFacebooklink.Text.Length < 5)
            {
                Errormsg("Enter valid Facebook Link");
            }
            else if (tbYoutubeLink.Text.Length < 5)
            {
                Errormsg("Enter valid Youtube Link");
            }
            else if (tbinstagram.Text.Length < 5)
            {
                Errormsg("Enter valid Instagram Link");
            }
            else if (tbtwitter.Text.Length < 5)
            {
                Errormsg("Enter valid Twitter Link");
            }
            else
            {
                Session["action"] = 'S';
                lblConfirmation.Text = "Are you sure you want to update Social Media Links?";
                mpConfirmation.Show();
            }

        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminContDetails-E1", ex.Message.ToString());
            Errormsg(ex.Message.ToString());

        }
    }
    protected void lbtnContactSave_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (validValueContact() == false)
        {
            return;
        }
        Session["action"] = 'C';
        lblConfirmation.Text = "Do you want Update Contact Helpdesk/Support Contact Details?";
        mpConfirmation.Show();
    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (Session["action"].ToString() == "S")
        {
            insertSocial();
        }

        if (Session["action"].ToString() == "C")
        {
            insertContact();
        }
    }
    protected void lbtnContactReset_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        resetContact();

    }
    protected void lbtnResetSocial_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        resetSocial();
    }
    protected void gvcontactHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvcontactHistory.PageIndex = e.NewPageIndex;
        ContactHistoryList();
        mpcontactHistory.Show();
    }
    protected void lbtnHistorySocial_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        loadSocialHistory();
        mpsocialhistory.Show();

    }
    protected void gvSocialHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSocialHistory.PageIndex = e.NewPageIndex;
        loadSocialHistory();
        mpsocialhistory.Show();
    }
    #endregion
}