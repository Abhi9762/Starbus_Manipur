using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class offersAll : System.Web.UI.Page
{
    sbOffers obj = new sbOffers();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getOffers();
            Session["Heading"] = "Offers";
        }
    }
    private void Errormsg(string msg)
    {
        string popup = _popup.modalPopupSmall("W", "Please Note", msg, "Close");
        Response.Write(popup);
    }
    public void getOffers()
    {
        string date = DateTime.Now.Date.ToString("dd/MM/yyyy");
        DataTable dt = obj.getOffers(date, "100", "L");
        lvOffers.Visible = false;
        pnlNoOffer.Visible = true;
        if (dt.Rows.Count > 0)
        {
            lvOffers.DataSource = dt;
            lvOffers.DataBind();
            lvOffers.Visible = true;
            pnlNoOffer.Visible = false;
        }
        
    }
    public void getOfferDetail(string couponId)
    {
        DataTable dt = obj.getOfferDetail(couponId);
        lblTitle.Text = dt.Rows[0]["coupontitle"].ToString();
        lblCode.Text = dt.Rows[0]["couponcode"].ToString();
        lblDescription.Text = dt.Rows[0]["discountdescription"].ToString();

        string dType = dt.Rows[0]["discounttype"].ToString() == "A" ? "₹" : "%";
        string dOn = dt.Rows[0]["discounton"].ToString() == "S" ? "Per Seat" : "Per Ticket";

        lblDiscountAmount.Text = dt.Rows[0]["discountamount"].ToString() + " " + dType;
        lblDiscountAmountMax.Text = dt.Rows[0]["maxdiscount_amount"].ToString() + " " + dType;
        lblDiscountOn.Text = dOn;
        DateTime dateTime = (DateTime)dt.Rows[0]["validto_date"];
        lblValidUpto.Text = dateTime.ToString("dd/MM/yyyy");
        string imgUrl = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + "/starbusarn_v1/DBimg/offers/" + couponId + "_W.png";
        img.ImageUrl = imgUrl;
    }
       
    protected void lvOffers_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ListViewDataItem dataItem = (ListViewDataItem)e.Item;
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            string couponID = lvOffers.DataKeys[dataItem.DataItemIndex]["couponid"].ToString();
            string imgUrl = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + "/starbusarn_v1/DBimg/offers/" + couponID + "_W.png";
            Image img = (Image)dataItem.FindControl("imgWeb");
            img.ImageUrl =  imgUrl;
        }
    }

    protected void lvOffers_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        if (String.Equals(e.CommandName, "VIEWDETAILS"))
        {           
            ListViewDataItem dataItem = (ListViewDataItem)e.Item;
            string couponID = lvOffers.DataKeys[dataItem.DataItemIndex]["couponid"].ToString();
            getOfferDetail(couponID);
            mpOfferDetail.Show();
        }
    }
}