using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class traveller_trvlrLoginDisable : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            offers();
            loadGenralData();
        }
    }
    private void loadGenralData()//M1
    {
        try
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("../CommonData.xml"));
            XmlNodeList deptlogo = doc.GetElementsByTagName("dept_logo_url");
            if (deptlogo.Item(0).InnerXml != "")
            {
                ImgDepartmentLogo.ImageUrl = deptlogo.Item(0).InnerXml;
                ImgDepartmentLogo.Visible = true;
            }
            XmlNodeList dept_en = doc.GetElementsByTagName("title_txt_en");
            lblDeptName.Text = dept_en.Item(0).InnerXml;
            XmlNodeList version = doc.GetElementsByTagName("Ver_Name");
            lblversion.Text = version.Item(0).InnerXml;
            XmlNodeList poweredBy = doc.GetElementsByTagName("managed_by");
            lblPoweredBy.Text = poweredBy.Item(0).InnerXml;
        }
        catch (Exception ex)
        {
        }
    }
    #region "Offers"
    public void offers()
    {
        sbOffers obj = new sbOffers();
        string date = DateTime.Now.Date.ToString("dd/MM/yyyy");
        DataTable dtOffer = obj.getOffers(date, "1", "L");
        pnlOffers.Visible = false;
        pnlNoOffer.Visible = true;
        if (dtOffer.Rows.Count > 0)
        {
            string imgUrl = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + "/starbusnew/DBimg/offers/" + dtOffer.Rows[0]["couponid"].ToString() + "_W.png";
            imgOffer.ImageUrl = imgUrl;// GetImage((byte[])dtOffer.Rows[0]["webimg"]);
            pnlOffers.Visible = true;
            pnlNoOffer.Visible = false;
        }
    }
    public string GetImage(object img)
    {
        try
        {
            return "data:image/jpg;base64," + Convert.ToBase64String((byte[])img);
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    protected void imgOffer_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("../offersAll.aspx");
    }
    #endregion
}