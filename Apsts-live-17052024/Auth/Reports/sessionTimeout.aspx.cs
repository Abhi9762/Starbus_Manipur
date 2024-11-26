using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class sessionTimeout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session.Clear();
        Session.RemoveAll();
        Session.Abandon();
        loadGenralData();
    }
    private void loadGenralData()//M1
    {
        try
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("../../CommonData.xml"));
            XmlNodeList title = doc.GetElementsByTagName("title_txt_en");
            this.Title = title.Item(0).InnerXml;


        }
        catch (Exception ex)
        {

        }
    }

    #region"Event"
    protected void lbtnhome_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Home.aspx");
    }


    #endregion
}