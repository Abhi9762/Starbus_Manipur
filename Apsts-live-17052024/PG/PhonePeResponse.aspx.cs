using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PG_PhonePeResponse : System.Web.UI.Page
{
    classPhonePe obj = new classPhonePe();
    ErrorLog _err = new ErrorLog(); 
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                string requestData;

                using (StreamReader reader = new StreamReader(Request.InputStream))
                {
                    requestData = reader.ReadToEnd();
                }
                               
                //_err.Error_Log("pg-phonepeResponse.aspx", "black");
               // _err.Error_Log("pg-phonepeResponse.aspx", requestData);

                wsPhonePe objWS = new wsPhonePe();
                DataTable dt = new DataTable();
                dt = objWS.JSONToDataTable("[" + requestData + "]");
//_err.Error_Log("pg-phonepeResponse.aspx1", dt.Rows.Count.ToString());
                if (dt.Rows.Count > 0)
                {
                    string respo = dt.Rows[0]["response"].ToString() ;
//_err.Error_Log("pg-phonepeResponse.aspx2", respo);
                    byte[] data = Convert.FromBase64String(respo);
                    string decodedString = System.Text.Encoding.UTF8.GetString(data);
_err.Error_Log("pg-phonepeResponse.aspx3", decodedString);
                    obj.saveCallbackResponse(decodedString);
                }
                else{
                    Response.Write(AppDomain.CurrentDomain.BaseDirectory + @"Errors\" + DateTime.Today.ToString("dd-MM-yy") + ".txt"+"   ====  "+System.Web.HttpContext.Current.Server.MapPath("~/Errors/" + DateTime.Today.ToString("dd-MM-yy") + ".txt").ToString());
                    _err.Error_Log("pg-phonepeResponse.aspx", "No record");
}
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
            _err.Error_Log("pg-phonepeResponse.aspx", ex.Message);
        }
    }


}