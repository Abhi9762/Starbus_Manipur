using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

/// <summary>
/// Summary description for wsConductorApp
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class wsConductorApp : System.Web.Services.WebService
{

    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    public wsConductorApp()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void login(string username, string pwd)
    {
        try
        {
            DataTable dtDetails = new DataTable("details");
            dtDetails.Columns.Add("Name", typeof(string));
            dtDetails.Columns.Add("Mobile", typeof(string));
            dtDetails.Columns.Add("Depot", typeof(string));

            string code = "", msg = "";
            if (username.ToUpper() == "CONDUCTOR" && pwd == "Test@123")
            {
                code = "100";
                msg = "";
                dtDetails.Rows.Add("Deepak Bhandari", "9012345678","Dehradun Hill");
            }
            else
            {
                code = "101";
                msg = "Username/Password does not match. Please try again.";
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Details", DataTableToJSON(dtDetails), msg);
            Context.Response.Write(jsonstr);            
        }
        catch (Exception e) {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON("900", "Details", "NA", "Something went wrong. Please try again.");
            Context.Response.Write(jsonstr);
        }
    }
    

    // Extra methods
    public string DataTableToJSON(DataTable table)
    {
        JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
        List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
        Dictionary<string, object> childRow;
        foreach (DataRow row in table.Rows)
        {
            childRow = new Dictionary<string, object>();
            foreach (DataColumn col in table.Columns)
            {
                childRow.Add(col.ColumnName, row[col]);
            }
            parentRow.Add(childRow);
        }
        return jsSerializer.Serialize(parentRow);
    }
    public string finalJSON(string code, string data1Name, string data1, string errorMessage)
    {
        string final = "";
        final = final + "{";
        final = final + "\"code\":\"" + code + "\",";
        final = final + "\"" + data1Name + "\":" + data1 + ",";
        final = final + "\"msg\":\"" + errorMessage + "\"";
        final = final + "}";
        return final;
    }
    public string finalJSON(string code, string data1Name, string data1, string data2Name, string data2, string errorMessage)
    {
        string final = "";
        final = final + "{";
        final = final + "\"code\":\"" + code + "\",";
        final = final + "\"" + data1Name + "\":" + data1 + ",";
        final = final + "\"" + data2Name + "\":" + data2 + ",";
        final = final + "\"msg\":\"" + errorMessage + "\"";
        final = final + "}";
        return final;
    }
    public string finalJSON(string code, string data1Name, string data1, string data2Name, string data2, string data3Name, string data3, string errorMessage)
    {
        string final = "";
        final = final + "{";
        final = final + "\"code\":\"" + code + "\",";
        final = final + "\"" + data1Name + "\":" + data1 + ",";
        final = final + "\"" + data2Name + "\":" + data2 + ",";
        final = final + "\"" + data3Name + "\":" + data3 + ",";
        final = final + "\"msg\":\"" + errorMessage + "\"";
        final = final + "}";
        return final;
    }
    public string finalJSON(string code, string data1Name, string data1, string data2Name, string data2, string data3Name, string data3, string data4Name, string data4, string errorMessage)
    {
        string final = "";
        final = final + "{";
        final = final + "\"code\":\"" + code + "\",";
        final = final + "\"" + data1Name + "\":" + data1 + ",";
        final = final + "\"" + data2Name + "\":" + data2 + ",";
        final = final + "\"" + data3Name + "\":" + data3 + ",";
        final = final + "\"" + data4Name + "\":" + data4 + ",";
        final = final + "\"msg\":\"" + errorMessage + "\"";
        final = final + "}";
        return final;
    }
    public string finalJSON(string code, string data1Name, string data1, string data2Name, string data2, string data3Name, string data3, string data4Name, string data4, string data5Name, string data5, string errorMessage)
    {
        string final = "";
        final = final + "{";
        final = final + "\"code\":\"" + code + "\",";
        final = final + "\"" + data1Name + "\":" + data1 + ",";
        final = final + "\"" + data2Name + "\":" + data2 + ",";
        final = final + "\"" + data3Name + "\":" + data3 + ",";
        final = final + "\"" + data4Name + "\":" + data4 + ",";
        final = final + "\"" + data5Name + "\":" + data5 + ",";
        final = final + "\"msg\":\"" + errorMessage + "\"";
        final = final + "}";
        return final;
    }
    public string finalJSON(string code, string data1Name, string data1, string data2Name, string data2, string data3Name, string data3, string data4Name, string data4, string data5Name, string data5, string data6Name, string data6, string errorMessage)
    {
        string final = "";
        final = final + "{";
        final = final + "\"code\":\"" + code + "\",";
        final = final + "\"" + data1Name + "\":" + data1 + ",";
        final = final + "\"" + data2Name + "\":" + data2 + ",";
        final = final + "\"" + data3Name + "\":" + data3 + ",";
        final = final + "\"" + data4Name + "\":" + data4 + ",";
        final = final + "\"" + data5Name + "\":" + data5 + ",";
        final = final + "\"" + data6Name + "\":" + data6 + ",";
        final = final + "\"msg\":\"" + errorMessage + "\"";
        final = final + "}";
        return final;
    }


}
