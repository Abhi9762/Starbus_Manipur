using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;

public partial class mainMasterPage : System.Web.UI.MasterPage
{
    
    private sbCommonFunc _common = new sbCommonFunc();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            checkAdditionalModules();
            loadGenralData();
            lblHeading.Text = Session["Heading"].ToString();
            lblHeading1.Text = Session["Heading"].ToString();
            loadofcwisecontact(10);
        }

    }
    private void checkAdditionalModules()
    {
        lblagents.Visible = false;
        lblcsccentre.Visible = false;
        lblchartedbooking.Visible = false;
        lblparcelbooking.Visible = false;
        divpass.Visible = false;
        divagent.Visible = false;
        if (sbXMLdata.checkModuleCategory("70") == true)
        {
            lblagents.Visible = true;
            divagent.Visible = true;
        }
        if (sbXMLdata.checkModuleCategory("71") == true)
        {
            divpass.Visible = true;
        }
        if (sbXMLdata.checkModuleCategory("72") == true)
        {
            lblcsccentre.Visible = true;
        }
    }
    private void loadGenralData()//M1
    {
        try
        {
            
            

           

            int count = 0;
            sbXMLdata obj = new sbXMLdata();
            Page.Title = obj.loadtitle();

            ImgDepartmentLogo.ImageUrl = "Logo/" + obj.loadDeptLogo();
            lblDeptName.Text = obj.loadDeptNameAbbr();
            lblversion.Text = obj.loadVersion();
            //lblemail.Text = obj.loadEmail();
            //lblcontact.Text = obj.loadContact();
            //lblHelpine.Text = obj.loadtollfree();
           lbldepartmentname.Text = obj.loadDeptName();


        }
        catch (Exception ex)
        {
            _common.ErrorLog("mainMasterPage-M1", ex.Message.ToString());
        }
    }
    private void loadofcwisecontact(int ofclvl)//M2
    {
        //DataTable dt = new DataTable();
        //dt = _common.getofficecontact(ofclvl);
        //lbladdress.Text = "";
        //for (int i = 0; i < dt.Rows.Count; i++)
        //{
        //    lbladdress.Text = dt.Rows[i]["adrs"].ToString() + ", " + dt.Rows[i]["stname"].ToString() + ", " + dt.Rows[i]["distname"].ToString() + ", India";
        //    lblcontact.Text = dt.Rows[i]["mob"].ToString();
        //    lblemail.Text = dt.Rows[i]["eml"].ToString();

        //}

    }
}
