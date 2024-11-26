using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class traveller_trvlrMaster : System.Web.UI.MasterPage
{
    sbValidation _SecurityCheck = new sbValidation();
    sbSecurity _security = new sbSecurity();
    protected void Page_Init(object sender, System.EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.AddHeader("Cache-control", "no-store, must-revalidate,private,no-cache");
        Response.AddHeader("PRAGMA", "NO-Cache");
        Response.Expires = -1;
        Response.Expires = 0;
        Response.Cache.SetNoStore();
        Response.Cache.SetExpires(DateTime.Now.AddDays(-2));
        // --------------------------------------------
        Response.Cache.SetLastModified(DateTime.Now);
        Response.Cache.SetAllowResponseInBrowserHistory(false);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        checkForSecurity();
        if (!IsPostBack)
        {
            if (_security.isSessionExist(Session["_moduleName"]) == true)
            {
                lblModuleName.Text = Session["_moduleName"].ToString();
                Session["_moduleName"] = null;
                lblUserID.Text = Session["_UserCode"].ToString();
            }
            else
            {
                lblModuleName.Text = "Not Set";
            }
            loadxml();

        }
checkMobileNo(Session["_UserCode"].ToString());
    }

private void checkMobileNo(string mobileNumber) //M1
    {
        try
        {
            
            wsClass obj = new wsClass();
            DataTable dt = new DataTable();
            dt = obj.traveller_dt(Session["_UserCode"].ToString());
            if (dt.Rows.Count > 0)
            {
                string mobileNo = dt.Rows[0]["mobilenumber"].ToString();
                string userName = dt.Rows[0]["username"].ToString();
                string alreadyReg = dt.Rows[0]["already_yn"].ToString();
                if (alreadyReg == "Y")
                {

                }
                else
                {
                    Response.Redirect("../sessiontimeout.aspx");
                }
            }
            
           
        }
        catch (Exception ex)
        {
           
        }
    }

    private void loadxml()
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(Server.MapPath("../CommonData.xml"));
        XmlNodeList managedby = doc.GetElementsByTagName("managed_by");
      //  txtpoweredby.Text=managedby.Item(0).InnerXml;
        XmlNodeList deptname = doc.GetElementsByTagName("title_txt_en");
        XmlNodeList vername = doc.GetElementsByTagName("Ver_Name");
        XmlNodeList tollfree = doc.GetElementsByTagName("tollfreeno");
        lbldepartmentname.Text=deptname.Item(0).InnerXml ;
        lbldeptname.Text= deptname.Item(0).InnerXml;
        lblversion.Text = vername.Item(0).InnerXml;
        lbltollfree.Text = tollfree.Item(0).InnerXml;
    }





    private void checkForSecurity()
    {
        if (_security.isSessionExist(Session["_UserCode"]) == true)
        {
            Session["_UserCode"] = Session["_UserCode"];
        }
        else
        {
            Response.Redirect("../sessionTimeout.aspx");
        }

        if (_security.checkvalidation() == false)
        {
            Response.Redirect("../Errorpage.aspx");
        }
       
    }

    protected void lbtnlogout_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("logout.aspx");
    }
}
