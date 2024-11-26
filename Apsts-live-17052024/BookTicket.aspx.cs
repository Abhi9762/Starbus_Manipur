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


public partial class BookTicket : System.Web.UI.Page
{
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    sbValidation _SecurityCheck = new sbValidation();
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["heading"] = "Book Ticket";
        if (!IsPostBack)
        {
           // GetServiceType();
            loadPopularRoutes();
            loadFaq("TICKET");
            getAdvanceDaysBooking();
            loadOffers();
            CheckTravellerBooking();
        }
    }
    private void CheckTravellerBooking()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            DataTable dt = new DataTable();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_travellerbooking");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {

                    if (dt.Rows[0]["status"].ToString() == "N")
                    {
                        dvbook.Visible = false;
                        dvbook1.Visible = false;
                    }
                    else
                    {
                        dvbook.Visible = true;
                        dvbook1.Visible = true ;
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    private void loadOffers()
    {
        sbOffers obj = new sbOffers();
        string date = DateTime.Now.Date.ToString("dd/MM/yyyy");
        DataTable dtOffer = obj.getOffers(date, "10", "L");
        if (dtOffer.Rows.Count > 0)
        {
            rptOffers.Visible = true;
            rptOffers.DataSource = dtOffer;
            rptOffers.DataBind();
            pnlNoOffer.Visible = false;
        }
        else
        {
            pnlNoOffer.Visible = true;
            rptOffers.Visible = false;
        }
    }

    private void Errormsg(string msg)
    {
        string popup = _popup.modalPopupSmall("S", "Information", msg, "Close");
        Response.Write(popup);
    }
    ////private void GetServiceType()//M1
    ////{
    ////    try
    ////    {
    ////        ddlserviretype.Items.Clear();
    ////        MyCommand = new NpgsqlCommand();
    ////        DataTable dt = new DataTable();
    ////        MyCommand.Parameters.Clear();
    ////        MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_servicetype_getlist");
    ////        dt = bll.SelectAll(MyCommand);
    ////        if (dt.TableName == "Success")
    ////        {
    ////            if (dt.Rows.Count > 0)
    ////            {
    ////                ddlserviretype.DataSource = dt;
    ////                ddlserviretype.DataValueField = "srtpid";
    ////                ddlserviretype.DataTextField = "servicetype_name_en";
    ////                ddlserviretype.DataBind();
    ////            }
    ////        }
    ////        ddlserviretype.Items.Insert(0, "All");
    ////        ddlserviretype.Items[0].Value = "0";
    ////        ddlserviretype.SelectedIndex = 0;
    ////    }
    ////    catch (Exception ex)
    ////    {
    ////        ddlserviretype.Items.Insert(0, "All");
    ////        ddlserviretype.Items[0].Value = "0";
    ////        ddlserviretype.SelectedIndex = 0;
    ////        _common.ErrorLog("BookTicket-M1", ex.Message.ToString());
    ////    }
    ////}
    //protected void lbtnSearchServices_Click(object sender, EventArgs e)
    //{
    //    string frSton = tbFrom.Text.Trim().ToUpper();
    //    string toSton = tbTo.Text.Trim().ToUpper();
    //    string date = tbDate.Text.Trim().ToUpper();

    //    if (_SecurityCheck.IsValidString(frSton, 2, 50) == false)
    //    {
    //        Errormsg("Enter valid From Station Name");
    //        return;
    //    }
    //    if (_SecurityCheck.IsValidString(toSton, 2, 50) == false)
    //    {
    //        Errormsg("Enter valid To Station Name");
    //        return;
    //    }
    //    if (_SecurityCheck.IsValidString(date, 10, 10) == false)
    //    {
    //        Errormsg("Enter valid Date");
    //        return;
    //    }
    //    loadServices(frSton, toSton, date);
    //}

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
        { }
    }
    private void loadServices(string fromStationName, string toStationName, string date)//M2
    {
        try
        {
            wsClass obj = new wsClass();
            DataTable dt = new DataTable();
            dt = obj.search_services_dt(fromStationName, toStationName, "0", date);
            if (dt.Rows.Count > 0)
            {
                Session["fromStationName"] = fromStationName;
                Session["toStationName"] = toStationName;
                Session["date"] = date;
                Response.Redirect("seatavailablity.aspx");
            }
            else
            {
                Errormsg("Sorry, Services are not available now, try with changing your selection ");
            }
        }
        catch (Exception ex)
        {
            Errormsg("Sorry, Services are not available now, try with changing your selection ");
            _common.ErrorLog("BookTicket-M2", ex.Message.ToString());
        }
    }
    private void loadPopularRoutes()
    {
        sbSummary obj = new sbSummary();
        DataTable dt = obj.getRouteBooking(5, "T");
        if (dt.Rows.Count > 0)
        {
            pnlNoRoute.Visible = false;
            rptrTopRoute.DataSource = dt;
            rptrTopRoute.DataBind();
        }

        else
        {
            pnlNoRoute.Visible = true;
            rptrTopRoute.Visible = false;
        }
        
    }
    protected void rptrTopRoute_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string current_date = DateTime.Now.AddDays(1).ToString("dd") + "/" + DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("yyyy");

        if (e.Item.ItemType == ListItemType.Item)
        {
            LinkButton lbtnroute = (LinkButton)e.Item.FindControl("lbtnroute");
            //HiddenField frmstation = (HiddenField)e.Item.FindControl("hddfromst");
            //HiddenField tostation = (HiddenField)e.Item.FindControl("hddtost");   //station wise search
            HiddenField frmstation = (HiddenField)e.Item.FindControl("hdfromcity");
            HiddenField tostation = (HiddenField)e.Item.FindControl("hdtocity");     //city wise search
            //string date = DateTime.Now.Date.ToString("dd/MM/yyyy");
            loadServices(frmstation.Value, tostation.Value, current_date);
        }
    }
    public static List<string> searchStations(string stationText, string fromTo)//M1
    {
        try
        {
            wsClass obj = new wsClass();
            return obj.search_station_web(stationText, fromTo);
            //return obj.search_station_web(stationText);
        }
        catch (Exception ex)
        {
            sbCommonFunc _common = new sbCommonFunc();
            _common.ErrorLog("DashWeb-M1", ex.Message);
            return null;
        }
    }

    protected void btntoday_Click(object sender, EventArgs e)
    {
       
        string date = DateTime.Now.Date.ToString("dd/MM/yyyy");
       
        string frmstation = tbFrom.Text;
        string tostation = tbTo.Text;


        if ((tbFrom.Text.Length == 0) | (_SecurityCheck.IsValidString(tbFrom.Text, 3, 50) == false))
        {
            Errormsg("Please check Source Station");
            return;

        }

        if ((tbTo.Text.Length == 0) | (_SecurityCheck.IsValidString(tbTo.Text, 3, 50) == false))
        {
            Errormsg("Please check Destination Station");
            return;

        }
        

        loadServices(frmstation, tostation, date);
    }

    protected void btntomorrow_Click(object sender, EventArgs e)
    {
        string date = DateTime.Now.Date.AddDays(1).ToString("dd/MM/yyyy");

        string frmstation = tbFrom.Text;
        string tostation = tbTo.Text;


        if ((tbFrom.Text.Length == 0) | (_SecurityCheck.IsValidString(tbFrom.Text, 3, 50) == false))
        {
            Errormsg("Please check Source Station");
            return;

        }

        if ((tbTo.Text.Length == 0) | (_SecurityCheck.IsValidString(tbTo.Text, 3, 50) == false))
        {
            Errormsg("Please check Destination Station");
            return;

        }


        loadServices(frmstation, tostation, date);
    }

    protected void lbtnsearch_Click(object sender, EventArgs e)
    {
        string date = tbDate.Text;

        string frmstation = tbFrom.Text;
        string tostation = tbTo.Text;

        if (_SecurityCheck.IsValidateDateTime(date)==false)
        {
            Errormsg("Please check Journey Date");
            return;
        }
        if ((tbFrom.Text.Length == 0) | (_SecurityCheck.IsValidString(tbFrom.Text, 3, 50) == false))
        {
            Errormsg("Please check Source Station");
            return;

        }

        if ((tbTo.Text.Length == 0) | (_SecurityCheck.IsValidString(tbTo.Text, 3, 50) == false))
        {
            Errormsg("Please check Destination Station");
            return;

        }


        loadServices(frmstation, tostation, date);
    }

    protected void rptOffers_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

    }
    private void loadFaq(string v)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            DataTable dt = new DataTable();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_faq");
            MyCommand.Parameters.AddWithValue("p_categorycode", v);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    rptfaqcategory.DataSource = dt;
                    rptfaqcategory.DataBind();
                    LinkButton lbtnfaq = new LinkButton();


                    foreach (RepeaterItem item in rptfaqcategory.Items)
                    {
                        LinkButton lbtnphotocatgy = (LinkButton)item.FindControl("lbtnfaq");
                        lbtnphotocatgy.CssClass = "btn btn-sm btn btn-warning ml-1";
                    }

                    LinkButton lbtnphotocatgy1 = (LinkButton)rptfaqcategory.Items[0].FindControl("lbtnfaq");
                    lbtnphotocatgy1.CssClass = "btn btn-sm btn-primary ml-1";


                    loadfaqs(Convert.ToInt32(dt.Rows[0]["faqid"].ToString()));
                }
            }
        }
        catch (Exception ex)
        {
        }
    }

    private void loadfaqs(int v)
    {
        try
        {
            rptfaq.Visible = false;
            lblNoFaq.Visible = true;
            MyCommand = new NpgsqlCommand();
            DataTable dt = new DataTable();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_getfaqquestion");
            MyCommand.Parameters.AddWithValue("categoryid", v);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    rptfaq.DataSource = dt;
                    rptfaq.DataBind();
                    rptfaq.Visible = true;
                    lblNoFaq.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            rptfaq.Visible = false;
            lblNoFaq.Visible = true;
        }
    }

    protected void rptfaqcategory_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "show")
        {
            HiddenField hffaqcategodyid = (HiddenField)e.Item.FindControl("hffaqcategodyid");
            //LinkButton lbtnfaq = (LinkButton)e.Item.FindControl("lbtnfaq");
            ////  lbtnfaq.CssClass = "btn btn-success";
            //loadfaq(Convert.ToInt32(hffaqcategodyid.Value.ToString()));
            foreach (RepeaterItem item in rptfaqcategory.Items)
            {
                LinkButton lbtnphotocatgy = (LinkButton)item.FindControl("lbtnfaq");
                if (item.Equals(e.Item))
                    lbtnphotocatgy.CssClass = "btn btn-sm btn-primary ml-1";
                else
                    lbtnphotocatgy.CssClass = "btn btn-sm btn btn-warning ml-1";
            }
            loadfaqs(Convert.ToInt32(hffaqcategodyid.Value));
        }

    }
}