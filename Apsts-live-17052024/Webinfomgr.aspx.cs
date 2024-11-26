using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class Webinfomgr : System.Web.UI.Page
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
        Session["heading"] = "Web Information Manager";
        loadContactData();
    }
    private void loadContactData()//M1
    {
        try
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("CommonData.xml"));
           
            XmlNodeList webinfomanagername = doc.GetElementsByTagName("webinfomanagername");
            lblname.Text = webinfomanagername.Item(0).InnerXml;
            XmlNodeList webinfomanagercontact = doc.GetElementsByTagName("webinfomanagercontact");
            lblcontact.Text = webinfomanagercontact.Item(0).InnerXml;
            XmlNodeList webinfomanagermail = doc.GetElementsByTagName("webinfomanagermail");
            lblemail.Text = webinfomanagermail.Item(0).InnerXml;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminContDetails-M1", ex.Message.ToString());
            //Errormsg(ex.Message.ToString());
        }
    }

}