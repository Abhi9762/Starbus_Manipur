using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Npgsql;
public partial class traveller_seatPayment : BasePage
{
    NpgsqlCommand MyCommand;
    sbBLL bll = new sbBLL();
    sbValidation _SecurityCheck = new sbValidation();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    private sbCommonFunc _common = new sbCommonFunc();
    string commonerror = "There is some error. Please contact to helpdesk or try again after some time.";
    HDFC hdfc = new HDFC();
    wsClass obj = new wsClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        checkUser();
        if (!IsPostBack)
        {
            Random randomclass = new Random();
            Session["rndNoCheck"] = randomclass.Next().ToString();
            hidtoken.Value = Session["rndNoCheck"].ToString();
            Session["_moduleName"] = "Payment";
            onload();
        }
    }

    #region "Motheds"
    public void onload()
    {
        try
        {
            if (_security.isSessionExist(Session["TicketNumber"]) == true)
            {
                hfWalletBalance.Value = "0";
                hfTotal.Value = "0";
                loadJourneyDetails(Session["TicketNumber"].ToString());
                loadPassengerDetails(Session["TicketNumber"].ToString());
                loadTaxes(Session["TicketNumber"].ToString());
                loadWallet(Session["_UserCode"].ToString());
                DataTable dtt = obj.gettermsCondition();
                lblTermsConditions.Text = System.Net.WebUtility.HtmlDecode(dtt.Rows[0]["termconditiondtls"].ToString());
                loadPG();
            }
            else
            {
                Response.Redirect("errorpage.aspx");
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.ToString());
            _common.ErrorLog("seatPayment.aspx-0001", ex.Message);
        }
    }
    private void checkUser()
    {
        if (Session["_RoleCode"] == null)
        {
            Response.Redirect("../errorpage.aspx");
            return;
        }
        if (_security.isSessionExist(Session["_UserCode"]) == true)
        {
            Session["_UserCode"] = Session["_UserCode"];
        }
        else
        {
            Response.Redirect("errorpage.aspx");
        }
    }
    private void CheckTokan()
    {
        if (Session["rndNoCheck"] == null || Session["rndNoCheck"].ToString() == "")
        {
            Response.Redirect("../errorpage.aspx");
        }

        if (Session["rndNoCheck"].ToString() != hidtoken.Value.ToString())
        {
            Response.Redirect("../errorpage.aspx");
        }
    }
    private void Errormsg(string msg)
    {
        string popup = _popup.modalPopupSmall("W", "Information", msg, "Close");
        Response.Write(popup);
    }
    private void loadJourneyDetails(string ticketNo)//M1
    {
        try
        {
            DataTable dt = obj.getJourneyDetails(ticketNo, "R");
		
            if (dt.Rows.Count > 0)
            {

                lblFromStation.Text = dt.Rows[0]["fromstn_name"].ToString();
                lblToStation.Text = dt.Rows[0]["tostn_name"].ToString();
                lblDate.Text = dt.Rows[0]["journeydate"].ToString();
                lblDeparture.Text = dt.Rows[0]["trip_time"].ToString();
                lblServiceType.Text = dt.Rows[0]["service_type_name"].ToString();

                lblFareAmt.Text = dt.Rows[0]["amount_fare"].ToString();
                lblReservationCharge.Text = dt.Rows[0]["amount_onl_reservation"].ToString();
                lblTotal.Text = dt.Rows[0]["amount_total"].ToString();
                hfTotal.Value = dt.Rows[0]["amount_total"].ToString();

                lblBoardingStation.Text = dt.Rows[0]["boardingstn_name"].ToString();
            }
            else
            {
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("seatPayment.aspx-0002", ex.Message);
        }
    }
    private void loadPassengerDetails(string ticketNo)//M2
    {
        try
        {
            DataTable dt = obj.getPassengerDetails(ticketNo, "R");

            if (dt.Rows.Count > 0)
            {
                gvPassengers.DataSource = dt;
                gvPassengers.DataBind();

                gvPassengers.Visible = true;
            }
            else
            {
                gvPassengers.Visible = false;
            }
        }
        catch (Exception ex)
        {
            gvPassengers.Visible = false;
            _common.ErrorLog("seatPayment.aspx-0003", ex.Message);
        }
    }
    private void loadTaxes(string ticketNo)//M3
    {
        try
        {
           
            DataTable dt = obj.getTaxDetails(ticketNo);
            if (dt.Rows.Count > 0)
            {
                grdtax.DataSource = dt;
                grdtax.DataBind();
            }
        }
        catch (Exception ex)
        {

            _common.ErrorLog("seatPayment.aspx-0004", ex.Message);
        }
    }
    private void loadWallet(string userId)//M4
    {
        try
        {
           DataTable dt_wallet = new DataTable();
            dt_wallet = obj.getWalletDetail_dt(userId);
            if (dt_wallet.Rows.Count > 0)
            {
                lblWalletBalance.Text = dt_wallet.Rows[0]["amount"].ToString();
                hfWalletBalance.Value = dt_wallet.Rows[0]["amount"].ToString();
            }
            else
            {
                lblWalletBalance.Text = "NA";
                hfWalletBalance.Value = "0";
            }

        }
        catch (Exception ex)
        {
            hfWalletBalance.Value = "0";
            _common.ErrorLog("seatPayment.aspx-0005", ex.Message);
        }
    }
    public void getOffers()
    {
        sbOffers obj = new sbOffers();
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
    private void loadPG()
    {
        try
        {
            lblNoPG.Visible = true;
            rptrPG.Visible = false;
            DataTable dt_pg = new DataTable();
            dt_pg = hdfc.get_PG();

            if (dt_pg.Rows.Count > 0)
            {
                rptrPG.DataSource = dt_pg;
                rptrPG.DataBind();
                lblNoPG.Visible = false;
                rptrPG.Visible = true;
            }
        }
        catch (Exception ex)
        {
            lblNoPG.Visible = true;
            rptrPG.Visible = false;
            _common.ErrorLog("seatPayment.aspx-0006", ex.Message);
        }
    }
    public void openSubDetailsWindow(string MModuleName)
    {
        try
        {
            string murl;
            murl = MModuleName + "?rt=" + DateTime.Now.ToString();
            string b = Request.Browser.Type.Substring(0, 2);
            if (b.ToUpper().Trim() == "IE")
                Response.Write("<SCRIPT language='javascript'>window.showModalDialog('" + murl + "','name','dialogWidth:830px;dialogHeight:900px');</script>");
            else
            {
                // Dim url As String = "GenQrySchStages.aspx"
                string fullURL = "window.open('" + murl + "', '_blank', 'height=900,width=830,status=no,toolbar=no,menubar=no,location=no,scrollbars=no,resizable=no,titlebar=no' );";
                // ScriptManager.RegisterStartupScript(Me, GetType(String), "OPEN_WINDOW", fullURL, True)
                string script = "window.open('" + fullURL + "','')";
                if ((ClientScript.IsClientScriptBlockRegistered("NewWindow") == false))
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "NewWindow", fullURL, true); // 
            }
        }
        catch (Exception ex)
        {
        }
    }
    #endregion

    #region "Events"
    protected void lvOffers_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ListViewDataItem dataItem = (ListViewDataItem)e.Item;
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            string couponID = lvOffers.DataKeys[dataItem.DataItemIndex]["couponid"].ToString();
            string imgUrl = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + "/starbusarn_v1/DBimg/offers/" + couponID + "_W.png";
            Image img = (Image)dataItem.FindControl("imgWeb");
            img.ImageUrl = imgUrl;
        }
    }
    protected void lvOffers_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        CheckTokan();
        if (String.Equals(e.CommandName, "VIEWDETAILS"))
        {
            ListViewDataItem dataItem = (ListViewDataItem)e.Item;
            string couponID = lvOffers.DataKeys[dataItem.DataItemIndex]["couponid"].ToString();
            string couponCode = lvOffers.DataKeys[dataItem.DataItemIndex]["couponcode"].ToString();


            sbOffers objOffer = new sbOffers();
            DataTable dt_Offers = new DataTable();
            dt_Offers = objOffer.applyOffer(Session["_UserCode"].ToString(), Session["TicketNumber"].ToString(), couponCode);
            if (dt_Offers.Rows.Count > 0)
            {
                string totalPayableAmount = dt_Offers.Rows[0]["p_payable"].ToString();
                string coupon = dt_Offers.Rows[0]["p_coupone_code"].ToString();
                string offerAmount = dt_Offers.Rows[0]["p_offeramount"].ToString();
                lblDiscountAmnt.Text = "-" + offerAmount;
                lblOfferCode.Text = coupon;
                lblTotal.Text = totalPayableAmount;
                hfTotal.Value = totalPayableAmount;
                lbtnViewOffers.Visible = false;
                lbtnRemoveOffers.Visible = true;
            }
            else
            {
                Errormsg("Coupon didn't apply. Please try again.");
            }



            mpOffers.Hide();
        }
    }
    protected void lbtnViewOffers_Click(Object sender, EventArgs e)
    {
        CheckTokan();
        getOffers();
        mpOffers.Show();
    }
    protected void lbtnRemoveOffers_Click(Object sender, EventArgs e)
    {
        CheckTokan();
        sbOffers objOffer = new sbOffers();
        DataTable dt_Offers = new DataTable();
        dt_Offers = objOffer.removeOffer(Session["TicketNumber"].ToString(), lblOfferCode.Text);
        if (dt_Offers.Rows.Count > 0)
        {
            string totalPayableAmount = dt_Offers.Rows[0]["p_payable"].ToString();
            string coupon = dt_Offers.Rows[0]["p_coupone_code"].ToString();
            string offerAmount = dt_Offers.Rows[0]["p_offeramount"].ToString();
            lblDiscountAmnt.Text = offerAmount;
            lblOfferCode.Text = coupon;
            lblTotal.Text = totalPayableAmount;
            hfTotal.Value = totalPayableAmount;
            lbtnViewOffers.Visible = true;
            lbtnRemoveOffers.Visible = false;
        }
        else
        {
            Errormsg("Coupon didn't remove. Please try again.");
        }
        //lblDiscountAmnt.Text = "0";
        //lbtnViewOffers.Visible = true;
        //lbtnRemoveOffers.Visible = false;
        //lblOfferCode.Text = "";
    }
    protected void lbtnProceedWallet_Click(object sender, EventArgs e)
    {
        CheckTokan();
        if (chkTOC.Checked == false)
        {
            Errormsg("Please Check Terms and Condition.");
            return;
        }
        double walletBalance = Convert.ToDouble(hfWalletBalance.Value.ToString());
        double totalAmount = Convert.ToDouble(hfTotal.Value.ToString());
        if (walletBalance <= 0)
        {
            Errormsg("Insufficient balance in your wallet");
            return;
        }
        if (totalAmount <= 0)
        {
            Errormsg("Invalid Payable amount.");
            return;
        }

        if (_security.isSessionExist(Session["TicketNumber"]) == true)
        {
            DataTable dt_ = new DataTable();
            dt_ = obj.updateTicket(Session["TicketNumber"].ToString(), "W");

            if (dt_.Rows[0]["p_rslt"].ToString() == "DONE")
            {
                Session["_RNDIDENTIFIERSTRVL"] = _security.GeneratePassword(10, 5);
                Response.Redirect("ticketStatus.aspx");
            }
            else if (dt_.Rows[0]["p_rslt"].ToString() == "EXCEPTION")
            {
                Errormsg(commonerror);
            }
            else if (dt_.Rows[0]["p_rslt"].ToString() == "INVALIDBOOKINGAMOUNT")
            {
                Errormsg("Insufficient balance in your wallet");
            }
            else if (dt_.Rows[0]["p_rslt"].ToString() == "WALLETNOTUPDATE")
            {
                Errormsg("Something went wrong with wallet update. Please contact to helpdesk");
            }
            else
            {
                Errormsg(commonerror + dt_.TableName);
                _common.ErrorLog("trvlConfrm-E1", dt_.TableName);
            }
        }
        else
        {
            Response.Redirect("errorpage.aspx");
        }

    }
    protected void rptrPG_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        CheckTokan();
        if (e.CommandName == "PAYNOW")
        {
            if (chkTOC.Checked == false)
            {
                Errormsg("Please Check Terms and Condition.");
                return;
            }
            double totalAmount = Convert.ToDouble(hfTotal.Value.ToString());
            if (totalAmount <= 0)
            {
                Errormsg("Invalid Payable amount.");
                return;
            }
            if (_security.isSessionExist(Session["TicketNumber"]) == true)
            {
                HiddenField rptHdPGId = e.Item.FindControl("rptHdPGId") as HiddenField;
                HiddenField REQURL = e.Item.FindControl("hd_pgurl") as HiddenField;
                string resposeurl = REQURL.Value.ToString();
                Session["_RNDIDENTIFIERSTRVL"] = _security.GeneratePassword(10, 5);
                byte[] base64str = System.Text.Encoding.UTF8.GetBytes("1");
                Response.Redirect("../PG/" + resposeurl + "?RequestId=" + System.Convert.ToBase64String(base64str));
            }
            else
            {
                Errormsg("Something went wrong with Payment Gateway redirection. Please contact to helpdesk.");
            }
        }
        else
        {
            Errormsg("Something went wrong with Payment Gateway redirection. Please contact to helpdesk.");
        }

    }
    protected void lbtntermsNcondition_ServerClick(object sender, EventArgs e)
    {
        PageOpen("Terms & Conditions", "../termsNcondition.aspx");
        //openSubDetailsWindow("../termsNcondition.aspx");
    }
    void PageOpen(string title, string src)
    {
        lblTitle.InnerText = title;
        embedPage.Src = src;
        mpePage.Show();
    }
    #endregion

   


}