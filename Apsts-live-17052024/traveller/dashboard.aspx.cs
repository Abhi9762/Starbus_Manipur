using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Npgsql;

public partial class traveller_dashboard : BasePage
{
    NpgsqlCommand MyCommand;
    sbBLL bll = new sbBLL();
    sbValidation _SecurityCheck = new sbValidation();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    private sbCommonFunc _common = new sbCommonFunc();
    sbOffers obj = new sbOffers();

    protected void Page_Load(object sender, EventArgs e)
    {
        checkUser();
        if (!IsPostBack)
        {
            Random randomclass = new Random();
            Session["rndNoCheck"] = randomclass.Next().ToString();
            hidtoken.Value = Session["rndNoCheck"].ToString();

            Session["_moduleName"] = "Dashboard";
            loadTickets(Session["_UserCode"].ToString());
            loadWallet(Session["_UserCode"].ToString());
            lblUser.Text = Session["_UserCode"].ToString();
            lblLoginTime.Text = "Login " + DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
            getOffers();
            loadBusServiceType();
            getAdvanceDaysBooking();
            loadPopularRoutes();
            getpg();
        }
    }

    [WebMethod()]
    public static List<string> searchStations(string stationText, string fromTo)//M1
    {
        try
        {
            wsClass obj = new wsClass();
            return obj.search_station_web(stationText, fromTo);
            //  return obj.search_station_web(stationText);
        }
        catch (Exception ex)
        {
            sbCommonFunc _common = new sbCommonFunc();
            _common.ErrorLog("dashboard.aspx-0001", ex.Message);
            return null;
        }
    }

    #region "Motheds"
    private void Errormsg(string msg)
    {
        string popup = _popup.modalPopupSmall("W", "Information", msg, "Close");
        Response.Write(popup);
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

        if (Session["rndNoCheck"].ToString () != hidtoken.Value.ToString())
        {
            Response.Redirect("../errorpage.aspx");
        }       
    }
    private void getAdvanceDaysBooking()
    {
        try
        {
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_advance_days");
            DataTable MyTable = bll.SelectAll(MyCommand);
            if (MyTable.TableName == "Success")
            {
                if (MyTable.Rows.Count > 0)
                {
                    Session["MaxDate"] = Convert.ToInt32(MyTable.Rows[0]["days"].ToString());
                    hdmaxdate.Value = MyTable.Rows[0]["days"].ToString();
                }
                else
                {
                    Session["MaxDate"] = "30";
                    hdmaxdate.Value = "30";
                }
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("dashboard.aspx-0002", ex.Message);
        }
    }

    private void loadBusServiceType()//M0
    {
        try
        {
            DataTable dt = new DataTable();
            ddlservices.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_servicetype_getlist");
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                ddlservices.DataSource = dt;
                ddlservices.DataTextField = "servicetype_name_en";
                ddlservices.DataValueField = "srtpid";
                ddlservices.DataBind();
            }
            ddlservices.Items.Insert(0, "All");
            ddlservices.Items[0].Value = "0";
            ddlservices.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlservices.Items.Insert(0, "All");
            ddlservices.Items[0].Value = "0";
            ddlservices.SelectedIndex = 0;
            _common.ErrorLog("dashboard.aspx-0003", ex.Message);
        }
    }
    private void loadTickets(string userId)//M1
    {
        try
        {
            wsClass obj = new wsClass();
            DataTable dt = new DataTable();
            dt = obj.getTickets(userId, 100);

            if (dt.Rows.Count > 0)
            {
                gvTickets.DataSource = dt;
                gvTickets.DataBind();

                lblNoTicketsMsg.Text = "";
                pnlNoTickets.Visible = false;
                gvTickets.Visible = true;
            }
            else
            {
                lblNoTicketsMsg.Text = "Ticket details not available";
                pnlNoTickets.Visible = true;
                gvTickets.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblNoTicketsMsg.Text = "Oops! Something happened with your ticket loading process.<br> Please feel free to contact the helpdesk";
            pnlNoTickets.Visible = true;
            _common.ErrorLog("dashboard.aspx-0004", ex.Message);
        }
    }
    private void loadConfirmedTickets(string userId)//M2
    {
        try
        {
            wsClass obj = new wsClass();
            DataTable dt = new DataTable();
            dt = obj.getTickets(userId, 100);
            DataRow[] dr = dt.Select("current_status = 'A'");

            DataTable dttt = dr.CopyToDataTable();

            if (dttt.Rows.Count > 0)
            {
                gvTickets.DataSource = dttt;
                gvTickets.DataBind();

                lblNoTicketsMsg.Text = "";
                pnlNoTickets.Visible = false;
                gvTickets.Visible = true;
            }
            else
            {
                lblNoTicketsMsg.Text = "Ticket details not available";
                pnlNoTickets.Visible = true;
                gvTickets.Visible = false ;
            }
        }
        catch (Exception ex)
        {
            lblNoTicketsMsg.Text = "Ticket details not available";
            pnlNoTickets.Visible = true;
            gvTickets.Visible = false;
            _common.ErrorLog("dashboard.aspx-0005", ex.Message);
        }
    }
    private void loadServices(string fromStationName, string toStationName, string date)//M2
    {
        try
        {
            wsClass obj = new wsClass();
            DataTable dt = new DataTable();
            dt = obj.search_services_dt(fromStationName, toStationName, ddlservices.SelectedValue, date);
            if (dt.Rows.Count > 0)
            {
                gvService.DataSource = dt;
                gvService.DataBind();

                pnlServices.Visible = true;
                lblSearchTotalRecord.Text = dt.Rows.Count.ToString() + " Bus Found";

                lblNoServiceMsg.Text = "";
                pnlNoService.Visible = false;
            }
            else
            {
                pnlServices.Visible = false;
                lblNoServiceMsg.Text = "";
                lblNoServiceMsg.Text = "Currently No Service is Available for Online Booking , Between Selected Stations.";
                pnlNoService.Visible = true;
            }
        }
        catch (Exception ex)
        {
            pnlServices.Visible = false;
            pnlNoService.Visible = true;
            lblNoServiceMsg.Text = "";
            lblNoServiceMsg.Text = "Oops! Something happened with your bus search process.<br> Please feel free to contact the helpdesk";
            lblNoServiceMsg.Visible = true;
            _common.ErrorLog("dashboard.aspx-0006", ex.Message);
        }
    }
    private void loadWallet(string userId)//M3
    {
        try
        {
            wsClass obj = new wsClass();
            DataTable dt_wallet = new DataTable();
            dt_wallet = obj.getWalletDetail_dt(userId);
            if (dt_wallet.Rows.Count > 0)
            {
                lblWalletBalance.Text = dt_wallet.Rows[0]["amount"].ToString();
            }
            else
            {
                lblWalletBalance.Text = "NA";
            }

        }
        catch (Exception ex)
        {
            _common.ErrorLog("dashboard.aspx-0007", ex.Message);
        }
    }
    private void loadPopularRoutes()
    {
        sbSummary obj = new sbSummary();
        DataTable dt = obj.getRouteBooking(2, "T");
        gvroutes.DataSource = dt;
        gvroutes.DataBind();

    }
    private void getpg()
    {
        HDFC obj = new HDFC();
        DataTable dt = obj.get_PG();
        gvPaymentGateway.DataSource = dt;
        gvPaymentGateway.DataBind();

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
        string imgUrl = "../DBimg/offers/" + couponId + "_W.png";
        img.ImageUrl = imgUrl;
    }

 private void loadticketdetail(string _TicketNo)
    {
        try
        {


            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.get_active_tickets_for_track");
            MyCommand.Parameters.AddWithValue("p_userid", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("p_ticket_no", _TicketNo);
            DataTable dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["bus_no"].ToString() != "NA" && dt.Rows[0]["gps_yn"].ToString() == "Y")
                    {
                        Session["busno"] = dt.Rows[0]["bus_no"].ToString();
                        Response.Redirect("trackmybus.aspx");
                    }
                    else
                    {
                        Errormsg("Sorry, Bus Details Not Available");
                    }

                }
                else
                {
                    Errormsg("Invalid Ticket No.");
                }
            }
        }
        catch (Exception ex)
        {
 _common.ErrorLog("dashboard.aspx-0008", ex.Message);
        }
    }

    #endregion

    #region "Events"
 
    protected void lbtnSearchServices_Click(object sender, EventArgs e)
    {
        CheckTokan();

        string frSton = tbFrom.Text.Trim().ToUpper();
        string toSton = tbTo.Text.Trim().ToUpper();
        string date = tbDate.Text.Trim().ToUpper();

        if (_SecurityCheck.IsValidString(frSton, 2, 50) == false)
        {
            Errormsg("Enter valid From Station Name");
            return;
        }
        if (_SecurityCheck.IsValidString(toSton, 2, 50) == false)
        {
            Errormsg("Enter valid To Station Name");
            return;
        }
        if (_SecurityCheck.IsValidString(date, 10, 10) == false)
        {
            Errormsg("Enter valid Date");
            return;
        }
        if (frSton == toSton)
        {
            Errormsg("Station Name Cannot Be Same");
            return;
        }
        lblSearchStations.Text = frSton + " - " + toSton;
        loadServices(frSton, toSton, date);

    }

  protected void gvService_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbtnopenclose = (LinkButton)e.Row.FindControl("lbtnclose");
            LinkButton lbtnbook = (LinkButton)e.Row.FindControl("lbtnOrgView");
            DataRowView rowView = (DataRowView)e.Row.DataItem;
            lbtnbook.Visible = false;
            lbtnopenclose.Visible = false;

            int availableseat=0;

            if (int.TryParse(rowView["totalavailablseats"].ToString(), out availableseat)== true)
            {
                availableseat = int.Parse(rowView["totalavailablseats"].ToString());
            }

            if (rowView["openclose"].ToString() == "O" && availableseat >0)
            {
                lbtnbook.Visible = true;
                lbtnopenclose.Visible = false;
            }
            else
            {
                lbtnbook.Visible = false;
                lbtnopenclose.Visible = true;
            }
        }
    }
    protected void gvService_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "BOOKTICKET")
        {
            CheckTokan();
            int rowIndex = int.Parse(e.CommandArgument.ToString());

string openclose = gvService.DataKeys[rowIndex]["openclose"].ToString();

            if (openclose == "C")
            {
                Errormsg("Ticket Booking Time Close.");
                return;
            }
            string dsvcid = gvService.DataKeys[rowIndex]["dsvcid"].ToString();
            string strpid = gvService.DataKeys[rowIndex]["strpid"].ToString();
            string depttime = gvService.DataKeys[rowIndex]["depttime"].ToString();
            string arrtime = gvService.DataKeys[rowIndex]["arrtime"].ToString();
            string tripdirection = gvService.DataKeys[rowIndex]["tripdirection"].ToString();
            string servicetypename = gvService.DataKeys[rowIndex]["servicetypename"].ToString();
            string totalfare = gvService.DataKeys[rowIndex]["fare"].ToString();

            string layout = gvService.DataKeys[rowIndex]["layout"].ToString();
            string midstations = gvService.DataKeys[rowIndex]["midstations"].ToString();
            string frstonid = gvService.DataKeys[rowIndex]["frstonid"].ToString();
            string fromstation = gvService.DataKeys[rowIndex]["from_station_name"].ToString();
            string tostonid = gvService.DataKeys[rowIndex]["tostonid"].ToString();
            string tostation = gvService.DataKeys[rowIndex]["to_station_name"].ToString();

            Dictionary<string, string> dct = new Dictionary<string, string>();
            dct.Add("dsvcid", dsvcid);
            dct.Add("strpid", strpid);
            dct.Add("depttime", depttime);
            dct.Add("arrtime", arrtime);
            dct.Add("tripdirection", tripdirection);
            dct.Add("servicetypename", servicetypename);
            dct.Add("totalfare", totalfare);
            dct.Add("layout", layout);
            dct.Add("midstations", midstations);
            dct.Add("frstonid", frstonid);
            dct.Add("fromstation", fromstation);
            dct.Add("tostonid", tostonid);
            dct.Add("tostation", tostation);
            dct.Add("date", tbDate.Text);

            Session["SearchParameters"] = dct;
            Session["_RNDIDENTIFIERSTRVL"] = _security.GeneratePassword(10, 5);
            Response.Redirect("seatSelection.aspx");

        }

    }
    protected void gvTickets_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "VIEWDETAIL")
        {
            CheckTokan();
            int rowIndex = int.Parse(e.CommandArgument.ToString());
            string ticketNo = gvTickets.DataKeys[rowIndex]["_ticketno"].ToString();
            Session["TicketNumber"] = ticketNo;
            Response.Redirect("ticketDetail.aspx");
        }
        else if (e.CommandName == "CANCELLATION")
        {
            CheckTokan();
            int rowIndex = int.Parse(e.CommandArgument.ToString());
            string ticketNo = gvTickets.DataKeys[rowIndex]["_ticketno"].ToString();
            Session["TicketNumber"] = ticketNo;
            Response.Redirect("ticketCancel.aspx");
        }
    }
    protected void gvTickets_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbtnCancel = (LinkButton)e.Row.FindControl("lbtnCancel");
            DataRowView rowView = (DataRowView)e.Row.DataItem;
            lbtnCancel.Visible = false;
            if (rowView["current_status"].ToString() == "A" && rowView["for_cancel"].ToString() == "Y")
            {
                lbtnCancel.Visible = true;
            }
        }
    }
    protected void gvTickets_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTickets.PageIndex = e.NewPageIndex;
        if (cbConfirmTickets.Checked)
        {
            loadConfirmedTickets(Session["_UserCode"].ToString());
        }
        else
        {
            loadTickets(Session["_UserCode"].ToString());
        }
    }
    protected void cbConfirmTickets_CheckedChanged(object sender, EventArgs e)
    {
        CheckTokan();
        if (cbConfirmTickets.Checked)
        {
            loadConfirmedTickets(Session["_UserCode"].ToString());
        }
        else
        {
            loadTickets(Session["_UserCode"].ToString());
        }
    }
    protected void lvOffers_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ListViewDataItem dataItem = (ListViewDataItem)e.Item;
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            Panel pnl = (Panel)dataItem.FindControl("pnlInnerOffer");
            if (dataItem.DataItemIndex == 0)
            {
                pnl.CssClass = "carousel-item active";
            }
            else
            {
                pnl.CssClass = "carousel-item";
            }



            string couponID = lvOffers.DataKeys[dataItem.DataItemIndex]["couponid"].ToString();
            string imgUrl = "../DBimg/offers/" + couponID + "_W.png";
            Image img = (Image)dataItem.FindControl("imgWeb");
            img.ImageUrl = imgUrl;
        }
    }
    protected void lvOffers_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        if (String.Equals(e.CommandName, "VIEWDETAILS"))
        {
            CheckTokan();
            ListViewDataItem dataItem = (ListViewDataItem)e.Item;
            string couponID = lvOffers.DataKeys[dataItem.DataItemIndex]["couponid"].ToString();
            getOfferDetail(couponID);
            mpOfferDetail.Show();
        }
    }
    protected void Unnamed_Click(object sender, EventArgs e)
    {
        CheckTokan();
        wsClass obj = new wsClass();
        DataTable dt = obj.getRatingTicketsNew(Session["_UserCode"].ToString());
        if (dt.Rows.Count > 0)
        {
            Response.Redirect("rateus.aspx");
        }
        else
        {
            Errormsg("Tickets Not Avaliable For Rating....");
        }

    }
 protected void btn_Click(object sender, EventArgs e)
    {
        if (_SecurityCheck.IsValidString(tbs.Text, 5, tbs.MaxLength) == false)
        {
            Errormsg("Invalid PNR");
            return;
        }
        loadticketdetail(tbs.Text.ToString());
    }

    protected void lbtnCancelTkt_Click(object sender, EventArgs e)
    {
        Session["TicketNumber"] = null;
        Response.Redirect("ticketCancel.aspx");
    }
    #endregion


}