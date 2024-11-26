using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PG_PhonepeTestResponse : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string requestData= "{'response':'eyJzdWNjZXNzIjpmYWxzZSwiY29kZSI6IlBBWU1FTlRfRVJST1IiLCJtZXNzYWdlIjoiUGF5bWVudCBGYWlsZWQiLCJkYXRhIjp7Im1lcmNoYW50SWQiOiJBUFNUU0RRUlVBVCIsInRyYW5zYWN0aW9uSWQiOiIxMTExMjMyMzIzMjNBMjIiLCJhbW91bnQiOjEwMDAwLCJwYXltZW50U3RhdGUiOiJGQUlMRUQiLCJwYXlSZXNwb25zZUNvZGUiOiJUWE5fQVVUT19GQUlMRUQifX0 = '}";
        wsPhonePe objWS = new wsPhonePe();
        DataTable dt = new DataTable();
        dt = objWS.JSONToDataTable("[" + requestData + "]");

        if (dt.Rows.Count > 0)
        {
            string respo = dt.Rows[0]["response"].ToString();

            byte[] data = Convert.FromBase64String(respo);
            string decodedString = System.Text.Encoding.UTF8.GetString(data);

            Response.Write(decodedString);
            
        }
    }
}