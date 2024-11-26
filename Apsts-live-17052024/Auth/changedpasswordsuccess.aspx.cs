using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class Auth_changedpasswordsuccess : System.Web.UI.Page
{
    sbSecurity _security = new sbSecurity();
    protected void Page_Load(object sender, EventArgs e)
    {
        loaddata();
        _security.RemoveUserLogin(Session["_UserCode"].ToString());
        Session.Clear();
        Session.RemoveAll();
        Session.Abandon();
        
    }
    private void loaddata()
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(Server.MapPath("../CommonData.xml"));
        XmlNodeList deptlogo = doc.GetElementsByTagName("dept_logo_url");
        XmlNodeList lbldept = doc.GetElementsByTagName("managed_by");
        lbldeptname.Text = lbldept.Item(0).InnerXml;
        XmlNodeList lbldeveloped = doc.GetElementsByTagName("developed_by");
        lbldevelopedby.Text = lbldeveloped.Item(0).InnerXml;

        //sbXMLdata obj = new sbXMLdata();
        XmlNodeList dept_Abbr_en = doc.GetElementsByTagName("dept_Abbr_en");
        XmlNodeList Ver_Name = doc.GetElementsByTagName("Ver_Name");
        XmlNodeList dept_logo_url = doc.GetElementsByTagName("dept_logo_url");
        ImgDepartmentLogo.ImageUrl = dept_logo_url.Item(0).InnerXml;
        lblDeptName1.Text = dept_Abbr_en.Item(0).InnerXml;
        lblversion.Text = Ver_Name.Item(0).InnerXml;

    }
}