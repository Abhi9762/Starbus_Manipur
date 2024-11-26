using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using ClosedXML.Excel;

public partial class test : System.Web.UI.Page
{
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    sbValidation _validation = new sbValidation();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    ReportDocument cryrpt = new ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
       if (!IsPostBack)
        {
            validuser();

        }
    }
    private void validuser()
    {
        try
        {
            DataTable dt = new DataTable();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_emp");
 	    MyCommand.Parameters.AddWithValue("p_ofclvlid", Convert.ToInt32("30"));
            MyCommand.Parameters.AddWithValue("p_ofcid", "1010100000");
            MyCommand.Parameters.AddWithValue("p_designation", Convert.ToInt32("0"));
            MyCommand.Parameters.AddWithValue("p_lincensestatus", "0");
            MyCommand.Parameters.AddWithValue("p_employeename", "APSTS0004");

            dt = bll.SelectAll(MyCommand);
Response.Write(dt.TableName);
           // errorMassage(dt.TableName);
            return;
            
        }
        catch (Exception ex)
        {

        }
    }

}