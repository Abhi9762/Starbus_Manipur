using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CCA.Util;

public partial class PG_ccavRequestHandler : System.Web.UI.Page
{
    CCACrypto ccaCrypto = new CCACrypto();
    public string workingKey = "";
    string ccaRequest = "";
    public string strEncRequest = "";
    public string strAccessCode = "";// put the access key in the quotes provided here.
    protected void Page_Load(object sender, EventArgs e)
    {
         workingKey = System.Configuration.ConfigurationManager.AppSettings["workingKey"]; // workingKey = "86A87D152DE821494591399605B78EB5";
         strAccessCode = System.Configuration.ConfigurationManager.AppSettings["AccessCode"]; // strAccessCode = "AVDN79JD98CL76NDLC";
        // if (!IsPostBack)
        //{
        foreach (string name in Request.Form)
        {
            if (name != null)
            {
                if (!name.StartsWith("_"))
                {
                    ccaRequest = ccaRequest + name + "=" + Request.Form[name] + "&";
                   // Response.Write(name + "=" + Request.Form[name]+"<br/>");
                    //Response.Write("</br>");
                }
            }
        }
        strEncRequest = ccaCrypto.Encrypt(ccaRequest, workingKey);
        //}
    }
}