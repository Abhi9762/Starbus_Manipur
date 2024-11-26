using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class traveller_TrackmyBus : System.Web.UI.Page
{
    sbBLL bll = new sbBLL();
    sbValidation _SecurityCheck = new sbValidation();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    private sbCommonFunc _common = new sbCommonFunc();
    protected void Page_Load(object sender, EventArgs e)
    {

        openiFrame(Session["busno"].ToString());
        
    }

   
    
    private void openiFrame(string busno)
    {
        Literal l1 = new Literal();
       
        string accesstoken = "7a9bef82c27db5be6ea4e9beff576639";
        string url = "https://t-location.intangles.com/"+ busno + "?vendor-access-token="+ accesstoken + "";
        l1.Text = "<iframe name='we' id='12'  frameborder='no' scrolling='auto' height='700px' width='100%'  src='" + url + "' style='left:0; background-color:#B8B8B8;'></iframe>";
        div1.Controls.Add(l1);
    }

    private void Errormsg(string msg)
    {
        string popup = _popup.modalPopupSmall("W", "Information", msg, "Close");
        Response.Write(popup);
    }
}