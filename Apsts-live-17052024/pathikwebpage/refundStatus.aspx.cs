using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class pathikwebpage_refundStatus : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string pn = Request.QueryString["PN"] == null/* TODO Change to default(_) if this is not a reference type */? "" : Request.QueryString["PN"];
            string cn = Request.QueryString["CN"] == null/* TODO Change to default(_) if this is not a reference type */? "" : Request.QueryString["CN"];
            string rn = Request.QueryString["RN"] == null/* TODO Change to default(_) if this is not a reference type */? "" : Request.QueryString["RN"];

            //byte[] b = Convert.FromBase64String(pn);
            //string ticketNo = System.Text.Encoding.UTF8.GetString(b);

            //byte[] bb = Convert.FromBase64String(cn);
            //string cancellationRefNo = System.Text.Encoding.UTF8.GetString(bb);

            //byte[] bbb = Convert.FromBase64String(rn);
            //string refundRefNo = System.Text.Encoding.UTF8.GetString(bbb);

            AesEncrptDecrpt aes = new AesEncrptDecrpt();
            string ticketno = aes.DecryptStringAES(pn);
            string cancellationRefNo = aes.DecryptStringAES(cn);
            string refundRefNo = aes.DecryptStringAES(rn);
        }
    }
}