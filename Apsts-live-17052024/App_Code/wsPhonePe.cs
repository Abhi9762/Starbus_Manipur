using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

/// <summary>
/// Summary description for wsPhonePe
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class wsPhonePe : System.Web.Services.WebService
{
    DataTable dt = new DataTable();
    classPhonePe obj = new classPhonePe();
    public wsPhonePe()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    #region "Phone Pe QR Code Request"
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void qrCodeRequest(string transactionId, string storeId, string amount, string requestFrom, string token)
    {
        try
        {
            int expiresInSeconds = 60;

            if (token.ToUpper() != "C7B21607E40B96E142889B56354089DA92A12A34")
            {
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                string jsonstrr = finalJSON("999", "Result", DataTableToJSON(dt), "Invalid Token.");
                Context.Response.Write(jsonstrr);
                return;
            }
            string code = "";
            string msg = "";
            string QRResponse = "";
            Int32 amnt;
            bool validAmount = int.TryParse(amount, out amnt );
            if (validAmount == false)
            {
                code = "101";
                msg = "Invalid Transaction Amount";
            }
            else
            {
                QRResponse = obj.SendPaymentRequest(transactionId, storeId, amnt, expiresInSeconds, requestFrom);

                if (QRResponse.Length > 0)
                {
                    msg = "";
                    code = "100";
                }
            }
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Result", QRResponse, "ExpiresInSeconds", expiresInSeconds.ToString(), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON("900", "Result", DataTableToJSON(dt), "Something went wrong. Please try again. " + ex.ToString());
            Context.Response.Write(jsonstr);
        }
    }
    #endregion

    #region "Check Transaction Status Our Server"
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void checkStatusUTC(string transactionId, string token)
    {
        try
        {
            if (token.ToUpper() != "C7B21607E40B96E142889B56354089DA92A12A34")
            {
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                string jsonstrr = finalJSON("999", "Result", DataTableToJSON(dt), "Invalid Token.");
                Context.Response.Write(jsonstrr);
                return;
            }

            dt = new DataTable();
            
            dt = obj.check_status(transactionId);

            string code = "";
            string msg = "";
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["p_status"].ToString().ToUpper() == "SUCCESS")
                {
                    code = "100";
                    msg = "Payment is successful for QR scan";
                }
                else if (dt.Rows[0]["p_status"].ToString().ToUpper() == "ERROR")
                {
                    code = "102";
                    msg = "Payment failed for QR scan";
                }
                else if (dt.Rows[0]["p_status"].ToString().ToUpper() == "DECLINED")
                {
                    code = "102";
                    msg = "Payment declined by customer";
                }
                else if (dt.Rows[0]["p_status"].ToString().ToUpper() == "CANCELLED")
                {
                    code = "102";
                    msg = "Payment cancelled by merchant";
                }
                else
                {
                    code = "102";
                    msg = "Undefined status available.";
                }
            }
            else
            {
                code = "101";
                msg = "Data Not Found";
            }
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Result", DataTableToJSON(dt), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON("900", "Result", DataTableToJSON(dt), "Something went wrong. Please try again. " + ex.ToString());
            Context.Response.Write(jsonstr);
        }
    }

    #endregion

    #region "Json Create Functions"
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
    public DataTable JSONToDataTable(string jsonString)
    {
        JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
        DataTable dataTable = new DataTable();
        List<Dictionary<string, object>> rows = jsSerializer.Deserialize<List<Dictionary<string, object>>>(jsonString);

        if (rows.Count > 0)
        {
            foreach (string columnName in rows[0].Keys)
                dataTable.Columns.Add(columnName, typeof(string));
        }

        foreach (var row in rows)
        {
            DataRow dataRow = dataTable.NewRow();

            foreach (string columnName in row.Keys)
            {
                dataRow[columnName] = row[columnName];
            }
            dataTable.Rows.Add(dataRow);
        }
        return dataTable;
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
    public string finalJSON(string code, string data1Name, string data1, string data2Name, string data2, string data3Name, string data3, string data4Name, string data4, string data5Name, string data5, string data6Name, string data6, string data7Name, string data7, string errorMessage)
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
        final = final + "\"" + data7Name + "\":" + data7 + ",";
        final = final + "\"msg\":\"" + errorMessage + "\"";
        final = final + "}";
        return final;
    }

    #endregion

    #region"Common Function"
    private void invalidparameters(string msg)
    {
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        string jsonstr = finalJSON("888", "Result", DataTableToJSON(dt), "Invalid Parameters. " + msg);
        Context.Response.Write(jsonstr);
    }
    private void invalidTokenMessage()
    {
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        string jsonstr = finalJSON("999", "Result", DataTableToJSON(dt), "Invalid Token. ");
        Context.Response.Write(jsonstr);
    }
    public string get6DigitRandomNumber()
    {
        Random random = new Random();
        return "123456";//random.Next(0, 999999).ToString("D6");
    }
    #endregion

}
