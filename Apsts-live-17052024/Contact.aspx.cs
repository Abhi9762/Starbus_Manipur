using System;
using System;
using System.Data;
using Npgsql;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

public partial class Contact : System.Web.UI.Page
{
    NpgsqlCommand MyCommand = new NpgsqlCommand();
    private sbBLL bll = new sbBLL();
    DataTable dt = new DataTable();
    sbValidation _validation = new sbValidation();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    private sbCommonFunc _common = new sbCommonFunc();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["Heading"] = "Contact Us";
            loadGenralData();
            loadofcwisecontact(10);
        }
    }

    private void loadGenralData()//M1
    {
        try
        {
            int count = 0;
            sbXMLdata obj = new sbXMLdata();
            this.Title = obj.loadtitle();
            lbldepartmentname.Text = obj.loadDeptName();

        }
        catch (Exception ex)
        {
            _common.ErrorLog("Contact-M1", ex.Message.ToString());
        }
    }

    private void loadofcwisecontact(int ofclvl)//M2
    {
        DataTable dt = new DataTable();
        dt=_common.getofficecontact(ofclvl);
        lbladdress.Text = "";
        for (int i=0; i < dt.Rows.Count; i++)
        {
sbXMLdata obj = new sbXMLdata();
            lbladdress.Text = dt.Rows[i]["adrs"].ToString();
            lblstatedistrict.Text = dt.Rows[i]["stname"].ToString() + ", " + dt.Rows[i]["distname"].ToString() + ", India";
            lblcontact.Text = obj.loadtollfree();
            lblemail.Text = obj.loadhelpdeskemail();

        }
       
    }
}