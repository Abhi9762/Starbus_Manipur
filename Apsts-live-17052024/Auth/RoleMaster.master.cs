using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Npgsql;
using System.Data;
using System.Xml;

public partial class Auth_RoleMaster : System.Web.UI.MasterPage
{
    sbValidation _SecurityCheck = new sbValidation();
    sbSecurity _security = new sbSecurity();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["_UserName"] == null || Session["_UserName"].ToString() == "")
            {
                Response.Redirect("../sessionTimeout.aspx");
            }
            LoadPgaeXML();
            lblModuleName.Text = Session["MasterPageHeaderText"].ToString();
            lblempname.Text = Session["_UserName"].ToString();


            //if ((int)Session["_RoleCode"] == 99)
            //{
            //    lbtnCatalogue.Visible = false;

            //}



        }
    }

    protected void LoadPgaeXML()
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(Server.MapPath("../CommonData.xml"));
        XmlNodeList title = doc.GetElementsByTagName("title_txt");
        //title_name.Text = title.Item(0).InnerXml;
        XmlNodeList vername = doc.GetElementsByTagName("Ver_Name");
        //lblvername.Text = vername.Item(0).InnerXml;
    }



    //protected void LinkButtonChngPwd_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("PAchangePassword.aspx", false);
    //}

    protected void lbtnprofile_Click(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeDash.aspx", false);
    }

    protected void lbtnlogout_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("Logout.aspx");
    }

    protected void lbtnCatalogue_Click(object sender, EventArgs e)
    {
        if (Session["_RoleCode"] != null)
        {
            int userRoleCode = Convert.ToInt32(Session["_RoleCode"]);

            if (userRoleCode == 26)
            {
                Response.Redirect("ForeManDB.aspx", false);
            }
            else if (userRoleCode == 23)
            {
                Response.Redirect("SSIDB.aspx", false);
            }
            else if (userRoleCode == 10)
            {
                Response.Redirect("DivManagerDB.aspx", false);
            }
            else if (userRoleCode == 3 || userRoleCode==2|| userRoleCode==99)
            {
                Response.Redirect("EmployeeDash.aspx", false);
            }
            else if (userRoleCode == 5)
            {
                Response.Redirect("DepotDashboard.aspx", false);
            }
            else if (userRoleCode == 9)
            {
                Response.Redirect("TimeKeeperCatalogue.aspx", false);
            }
            else if (userRoleCode == 1)
            {
                Response.Redirect("Sysadmcatalogue.aspx", false);
            }
            else if (userRoleCode == 8)
            {
                Response.Redirect("StInchargeCatalogue.aspx", false);
            }
            // else if (userRoleCode == 99)
            //{

            // Response.Redirect("EmployeeDash.aspx", false);
            //  }
        }
    }

    protected void lbtnChangePwd_ServerClick(object sender, EventArgs e)
    {
        mpchnagePass.Show();
    }
}
