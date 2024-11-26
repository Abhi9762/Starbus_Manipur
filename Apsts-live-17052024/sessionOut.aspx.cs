﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class sessionOut : System.Web.UI.Page
{
    sbSecurity _security = new sbSecurity();
    private sbCommonFunc _common = new sbCommonFunc();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (IsPostBack == false)
            {
                // _security.RemoveUserLogin(Session["_UserCode"].ToString());
            }
            Session.Abandon();
            Session.Clear();
            loadGenralData();
        }
        catch (Exception ex)
        {
            _common.ErrorLog("sessionTimeout-M1", ex.Message.ToString());
        }

    }
    private void loadGenralData()//M1
    {
        try
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("CommonData.xml"));
            XmlNodeList title = doc.GetElementsByTagName("title_txt_en");
            this.Title = title.Item(0).InnerXml;


        }
        catch (Exception ex)
        {
            _common.ErrorLog("Errorpage.aspx-0002", ex.Message.ToString());
        }
    }
    #region"Event"
    protected void lbtnhome_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }


    #endregion
}
