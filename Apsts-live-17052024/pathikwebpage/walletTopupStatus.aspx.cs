using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class pathikwebpage_walletTopupStatus : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblAmount.Text= Request.QueryString["amount"].TrimStart('0');
    }
}