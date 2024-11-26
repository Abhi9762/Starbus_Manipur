using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class termsNcondition : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        wsClass obj = new wsClass();
        DataTable dtt = obj.gettermsCondition();
        lbltermscondition.Text = System.Net.WebUtility.HtmlDecode(dtt.Rows[0]["termconditiondtls"].ToString());
    }
}