using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class traveller_profile : System.Web.UI.Page
{
    sbBLL bll = new sbBLL();
    sbValidation _SecurityCheck = new sbValidation();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    private sbCommonFunc _common = new sbCommonFunc();
    protected void Page_Load(object sender, EventArgs e)
    {
        checkUser();
        if (!IsPostBack)
        {
            Session["_moduleName"] = "Profile";
        }
    }
    private void checkUser()
    {
        if (_security.isSessionExist(Session["_UserCode"]) == true)
        {
            Session["_UserCode"] = Session["_UserCode"];
            lblUserID.Text = Session["_UserCode"].ToString();
        }
        else
        {
            Response.Redirect("errorpage.aspx");
        }
    }
}