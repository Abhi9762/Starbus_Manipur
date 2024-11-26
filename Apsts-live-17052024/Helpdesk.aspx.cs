using System.Data;
using Npgsql;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System;

public partial class Helpdesk : System.Web.UI.Page
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
            Session["Heading"] = " Help Desk";
            loadGenralData();
            loadofcwisecontact(40);
        }
    }
    private void loadGenralData()//M1
    {
        try
        {
            int count = 0;
            sbXMLdata obj = new sbXMLdata();
            this.Title = obj.loadtitle();          
            lblhelp1.Text = obj.loadtollfree();

            lblemailid.Text = obj.loadhelpdeskemail();

        }
        catch (Exception ex)
        {
            _common.ErrorLog("Helpdesk-M1", ex.Message.ToString());         
        }
    }
    private void loadofcwisecontact(int ofclvl)//M2
    {
        DataTable dt = new DataTable();
        dt = _common.getofficecontact(ofclvl);
        rptstationcontact.DataSource = dt;
        rptstationcontact.DataBind();

    }

    protected void lbtnsearch_Click(object sender, EventArgs e)
    {
        try
        {

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_officecontacthelpdesk");
            MyCommand.Parameters.AddWithValue("p_ofclvl", 40);
            MyCommand.Parameters.AddWithValue("p_officename", txtsearch.Text);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    rptstationcontact.DataSource = dt;
                    rptstationcontact.DataBind();
                }
            }
            
        }
        catch (Exception ex)
        {
            
        }
    }
}