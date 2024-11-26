using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class Auth_CntrDashboard : BasePage
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    sbValidation _SecurityCheck = new sbValidation();
    protected void Page_Load(object sender, EventArgs e)
    {

        lbltodaysummary.Text = "Today Summary as on time " + DateTime.Now.ToString("hh:mm tt");
        if (!IsPostBack)
        {
            Random randomclass = new Random();
            Session["rndNoCheck"] = randomclass.Next().ToString();
            hidtoken.Value = Session["rndNoCheck"].ToString();
            Session["_moduleName"] = "Dashboard";
            getAdvanceDaysBooking();
            loadAccountSummary();
            loadCntrOfflinetkttoday();
            loadCntrtktpasstoday();
            fillDropDownServiceTypes(ddlServiceType);
            checkAdditionalModules();
        }
    }

    #region "Method"
    private void CsrfTokenValidate()
    {
        if (hidtoken.Value != Session["rndNoCheck"].ToString())
        {
            Response.Redirect("errorpage.aspx");
            return;
        }
    }
    private void checkAdditionalModules()
    {
        divbuspass.Visible = false;
        if (sbXMLdata.checkModuleCategory("71") == true)
        {
            divbuspass.Visible = true;
        }
    }
    private void loadCntrtktpasstoday()
    {
        try
        {
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_get_cntr_passtkt_today");
            MyCommand.Parameters.AddWithValue("p_counter_id", Session["_UserCntrID"].ToString());
            //Session["_UserCntrID"]
            DataTable MyTable = bll.SelectAll(MyCommand);
            if (MyTable.TableName == "Success")
            {
                if (MyTable.Rows.Count > 0)
                {
                    lbltotalticket.Text = MyTable.Rows[0]["todaytkt"].ToString();
                    lblticketamount.Text = MyTable.Rows[0]["todaytktamt"].ToString() + " <i class='fa fa-rupee-sign'></i>";



                }
                else
                {
                }
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("CntrDashboard.aspx-0001", ex.Message.ToString());

        }
    }

    private void loadCntrOfflinetkttoday()
    {
        try
        {
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_get_cntr_offline_tkt_today");
            MyCommand.Parameters.AddWithValue("p_counter_id", Session["_UserCntrID"].ToString());
            MyCommand.Parameters.AddWithValue("p_tripdate", DateTime.Now.Date.ToString("dd/MM/yyyy"));
            //Session["_UserCntrID"]
            DataTable MyTable = bll.SelectAll(MyCommand);
            if (MyTable.TableName == "Success")
            {
                if (MyTable.Rows.Count > 0)
                {
                    lbltotaltrip.Text = MyTable.Rows[0]["TodayTrip"].ToString();
                    lbltotaltripamt.Text = MyTable.Rows[0]["TodayFare"].ToString() + " <i class='fa fa-rupee-sign'></i>";


                }
                else
                {
                }
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("CntrDashboard.aspx-0002", ex.Message.ToString());

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
                    Session["PmtDays"] = Convert.ToInt32(MyTable.Rows[0]["pmt_days"].ToString());
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
        { _common.ErrorLog("CntrDashboard.aspx-0003", ex.Message.ToString()); }
    }
    private void loadAccountSummary()
    {
        try
        {
            DateTime MTime = DateTime.Now;
            string s = MTime.ToString("hh:mm  tt");
            lblmanamtdate.Text = "Mandatory Amount Pending for Deposit upto " + DateTime.Now.Date.AddDays(-2).ToShortDateString();
            lblpndgdepodate.Text = "Total Pending Amount To Deposit Till  " + DateTime.Now;

            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.cntr_account");
            MyCommand.Parameters.AddWithValue("p_counter_id", Session["_UserCntrID"].ToString());
            //Session["_UserCntrID"]
            DataTable MyTable = bll.SelectAll(MyCommand);
            if (MyTable.TableName == "Success")
            {
                if (MyTable.Rows.Count > 0)
                {
                    lbltotpendingamt.Text = MyTable.Rows[0]["BALANCEtill"].ToString() + " <i class='fa fa-rupee'></i>";
                    if (Convert.ToDecimal(MyTable.Rows[0]["MENDATROYAMT"].ToString()) > 0)
                        lblmandatoryamt.Text = MyTable.Rows[0]["MENDATROYAMT"].ToString() + " <i class='fa fa-rupee'></i>";
                    else
                        lblmandatoryamt.Text = "0" + " <i class='fa fa-rupee'></i>";
                    Session["lblbalance"] = MyTable.Rows[0]["BALANCEtill"].ToString();
                    Session["lbldeponotverfy"] = MyTable.Rows[0]["deponotverfiy"].ToString();
                    Session["lblDeposited"] = MyTable.Rows[0]["DEPOSITverify"].ToString();
                    Session["lblmandatoryamt"] = MyTable.Rows[0]["MENDATROYAMT"].ToString();
                    Session["lblmandatoryamt"] = "0";
                    if (Convert.ToDecimal(MyTable.Rows[0]["MENDATROYAMT"].ToString()) > 0)
                    {
                        lblfirstmsg.Text = "To Proceed Further Please Deposit Pending Amount <br/> ₹ " + MyTable.Rows[0]["MENDATROYAMT"].ToString() + "<br/>Till Date :" + DateTime.Now.Date.AddDays(-2).ToString("dd/MM/yyyy");
                        mpFirst.Show();
                        return;
                    }
                }
                else
                {
                    lbltotpendingamt.Text = "0";
                    lblmandatoryamt.Text = "0";
                }
            }
        }
        catch (Exception ex)
        {
            lbltotpendingamt.Text = "0";
            lblmandatoryamt.Text = "0";
            _common.ErrorLog("CntrDashboard.aspx-0004", ex.Message.ToString());
        }
    }
    public void fillDropDownServiceTypes(DropDownList ddl)//M1
    {
        try
        {
            ddl.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_servicetype_getlist");
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                ddl.DataSource = dt;
                ddl.DataTextField = "servicetype_name_en";
                ddl.DataValueField = "srtpid";
                ddl.DataBind();
            }
            ddl.Items.Insert(0, "All");
            ddl.Items[0].Value = "0";
            ddl.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddl.Items.Insert(0, "All");
            ddl.Items[0].Value = "0";
            ddl.SelectedIndex = 0;
            _common.ErrorLog("CntrDashboard.aspx-0005", ex.Message.ToString());
        }
    }
    [WebMethod()]
    public static List<string> source_destination(string stationText, string fromTo)//M2
    {
        List<string> empResult = new List<string>();
        try
        {
            wsClass obj = new wsClass();
            return obj.search_station_web(stationText, fromTo);
        }
        catch (Exception ex)
        {
            sbCommonFunc _common = new sbCommonFunc();
            _common.ErrorLog("CntrDashboard.aspx-0006", ex.Message.ToString());
            return null;
        }
    }
    private void loadServices(string fromStationName, string toStationName, string date)//M3
    {
        try
        {
            wsClass obj = new wsClass();
            DataTable dt = new DataTable();
            dt = obj.search_services_dt(fromStationName, toStationName, ddlServiceType.SelectedValue, date);
            if (dt.Rows.Count > 0)
            {
                gvService.DataSource = dt;
                gvService.DataBind();

                lblNoServiceMsg.Text = "";
                gvService.Visible = true;

                pnlNoService.Visible = false;
            }
            else
            {
                lblNoServiceMsg.Text = "Currently No Service is Available for Online Booking , Between Selected Stations.";
                pnlNoService.Visible = true;
                gvService.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblNoServiceMsg.Text = "Oops! Something happened with your ticket loading process.<br> Please feel free to contact the helpdesk";
            pnlNoService.Visible = true;
            _common.ErrorLog("CntrDashboard.aspx-0007", ex.Message.ToString());
        }
    }
    private void Errormsg(string msg)
    {
        string popup = _popup.modalPopupSmall("W", "Please Note", msg, "Close");
        Response.Write(popup);
    }
    #endregion

    #region "Event"
    protected void lbtnSearchService_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        string frSton = tbfromstation.Text.Trim().ToUpper();
        string toSton = tbtostation.Text.Trim().ToUpper();
        string date = tbjourneydate.Text.Trim().ToUpper();

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
        loadServices(tbfromstation.Text.Trim().ToUpper(), tbtostation.Text.Trim().ToUpper(), tbjourneydate.Text);
    }
    //protected void rptService_ItemDataBound(object sender, RepeaterItemEventArgs e)
    //{
    //    if (e.Item.ItemType == ListItemType.Header)
    //    {
    //        RepeaterItem item = e.Item;

    //        //Reference the Controls.
    //        (item.FindControl("lbltotbuses") as Label).Text =(Convert.ToInt32((item.FindControl("lbltotbuses") as Label).Text.ToString())+1).ToString() ;
    //        (item.FindControl("lblfromsation") as Label).Text = tbfromstation.Text;
    //        (item.FindControl("lbltostation") as Label).Text = tbtostation.Text;

    //    }
    //}
    //protected void rptService_ItemCommand(object source, RepeaterCommandEventArgs e)
    //{
    //    if (e.CommandName == "select")
    //    {
    //        RepeaterItem item = e.Item;





    //        HttpCookie _cookie = new HttpCookie("searchperameter");

    //        _cookie.Values.Add("servicecode", (item.FindControl("hddsvcid") as HiddenField).Value);
    //        _cookie.Values.Add("layoutcode", (item.FindControl("hdlayout") as HiddenField).Value);            
    //        _cookie.Values.Add("servicetypename", (item.FindControl("lblservicetypename") as Label).Text );
    //        _cookie.Values.Add("tripid",(item.FindControl("hdsrtpid") as HiddenField).Value);
    //        _cookie.Values.Add("tripdirection", (item.FindControl("lbltripdirection") as Label).Text);
    //        _cookie.Values.Add("fromstationname", tbfromstation.Text);
    //        _cookie.Values.Add("tostationname",tbtostation.Text);
    //        _cookie.Values.Add("fromstationcode", (item.FindControl("hdfrstonid") as HiddenField).Value);
    //        _cookie.Values.Add("tostationcode", (item.FindControl("hdtostonid") as HiddenField).Value);
    //        _cookie.Values.Add("journeydate", tbjourneydate.Text);
    //        _cookie.Values.Add("departuretime", (item.FindControl("lbldepttime") as Label).Text);

    //        Response.Cookies.Add(_cookie);
    //        Response.Redirect("CntrSeatSelection.aspx");
    //    }
    //}


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
        CsrfTokenValidate();
        if (e.CommandName == "BOOKTICKET")
        {
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
            dct.Add("date", tbjourneydate.Text);

            Session["SearchParameters"] = dct;

            Response.Redirect("CntrSeatSelection.aspx");

        }

    }


    protected void lbtncanceltkt_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Session["p_ticketNo"] = "";
        Response.Redirect("CntrTicketCancellation.aspx");
    }
    protected void lbtncashregister_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Response.Redirect("CntrCashregister.aspx");

    }
    protected void lbtntripchart_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Response.Redirect("tripchartdash.aspx");
    }
    protected void lbtnCurrentbooking_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Response.Redirect("CntrCurrentBookingDashboard.aspx");
    }
    protected void lbtnSearchTicketNo_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (tbticketno.Text.Length <= 0)
        {
            Errormsg("Please Enter Valid Ticket No");
            return;
        }
        Session["p_ticketNo"] = tbticketno.Text;
        Response.Redirect("CntrTicketQuery.aspx");
    }
    protected void lbtnquery_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Response.Redirect("CntrTicketQuery.aspx");
    }
    protected void lbtnspcialcancel_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Session["p_ticketNo"] = "";
        Response.Redirect("CntrSpclTicketCancellation.aspx");
    }
    protected void lbtnbuspasses_Click(object sender, EventArgs e)
    {
        if (Convert.ToDecimal(Session["lblmandatoryamt"].ToString()) > 0)
        {
            lblfirstmsg.Text = "To Proceed Further Please Deposit Pending Amount <br/> ₹ " + Session["lblmandatoryamt"].ToString() + "<br/>Till Date :" + DateTime.Now.Date.AddDays(-2).ToString("dd/MM/yyyy");
            mpFirst.Show();
            return;
        }
        CsrfTokenValidate();
        Response.Redirect("CntrBusPasses.aspx");
    }
    protected void lbtntranreport_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        //Response.Redirect("reports/rpt_CA1.aspx");
        Server.Transfer("reports/rpt_CA1.aspx");
}
    protected void lbtnRestTicketNo_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        tbticketno.Text = "";
    }
    protected void lbtnRestSearchService_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        pnlNoService.Visible = true;
        gvService.Visible = false;
        tbfromstation.Text = "";
        tbtostation.Text = "";
        ddlServiceType.SelectedValue = "0";
        tbjourneydate.Text = "";
    }
    #endregion












}