using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_GetEtmDetails : System.Web.UI.Page
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    protected void Page_Load(object sender, EventArgs e)
    {
        loadEtmDetails();
    }

    public void loadEtmDetails()//M6
    {
        try
        {
            string etmRefNo = Session["_etmRefNo"].ToString();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_getetmdetail");
            MyCommand.Parameters.AddWithValue("p_refno", etmRefNo);
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                lblETMName.Text = dt.Rows[0]["etmtypename"].ToString() + "-" + dt.Rows[0]["etmserialno"].ToString();
                lblmakemodel.Text = dt.Rows[0]["makemodel"].ToString();
                if (dt.Rows[0]["purchasemode"].ToString() == "P")
                {
                    lblPurchaseMode.Text = "Purchase";
                }
                else
                {
                    lblPurchaseMode.Text = "Rent";
                }
                lblIMEI1.Text = dt.Rows[0]["imeino1"].ToString();
                lblIMEI2.Text = dt.Rows[0]["imeino2"].ToString();
                lblAgency.Text = dt.Rows[0]["agency"].ToString();
                lblInvoiceNo.Text = dt.Rows[0]["invoice_date"].ToString();
                lblInvoiceDate.Text = dt.Rows[0]["invoice_date"].ToString();
                lblAmount.Text = dt.Rows[0]["invoice_amount"].ToString();
                lblRecStore.Text = dt.Rows[0]["rec_officename"].ToString();
                lblReceivedBy.Text = dt.Rows[0]["receivedby"].ToString();
                lblReceiveOn.Text = dt.Rows[0]["receivingdate"].ToString();
               // lblissuestore.Text = dt.Rows[0]["issued_office"].ToString();
            }
        }
        catch (Exception ex)
        {
            
        }
    }


    protected void lbtnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdmETMRegistration.aspx");
    }

}