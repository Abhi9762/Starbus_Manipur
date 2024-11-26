using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class seatavailablity : System.Web.UI.Page
{
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    private sbCommonFunc _common = new sbCommonFunc();
    DataTable dtbusservice = new DataTable();
    DataTable dtdepature = new DataTable();
    DataTable dtarrival = new DataTable();
    int countBefore4AM = 0;
    int count4AM_8AM = 0;
    int count8AM_12PM = 0;
    int count12PM_4PM = 0;
    int count4PM_8PM = 0;
    int count8PM_12AM = 0;

    int acountBefore4AM = 0;
    int acount4AM_8AM = 0;
    int acount8AM_12PM = 0;
    int acount12PM_4PM = 0;
    int acount4PM_8PM = 0;
    int acount8PM_12AM = 0;
    DataTable dtfare = new DataTable();

    int sleepercount = 0;
    int nightservice = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getAdvanceDaysBooking();
            loadxml();
            lblcancellationpolicy.Text = _common.getCancellationpolicy();
            loadGenralData();
            if (Session["fromStationName"] != null && Session["toStationName"] != null && Session["date"] != null)
            {
                tbFrom.Text = Session["fromStationName"].ToString();
                tbTo.Text = Session["toStationName"].ToString();
                tbDate.Text = Session["date"].ToString();
                lbtnSearchServices_Click(sender, e);
                Session["fromStationName"] = null;
                Session["toStationName"] = null;
                Session["date"] = null;
            }
        }
    }


    #region "Method"
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
    private void loadxml()
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(Server.MapPath("CommonData.xml"));
        XmlNodeList managedby = doc.GetElementsByTagName("managed_by");
        //   txtpoweredby.Text = managedby.Item(0).InnerXml;
        //  XmlNodeList deptname = doc.GetElementsByTagName("dept_Name_en");
        //  lblcopyright.Text = deptname.Item(0).InnerXml;
    }
    private void loadGenralData()//M1
    {
        try
        {
            sbXMLdata obj = new sbXMLdata();
            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("CommonData.xml"));
            XmlNodeList deptlogo = doc.GetElementsByTagName("dept_logo_url");
            if (deptlogo.Item(0).InnerXml != "")
            {
                ImgDepartmentLogo.ImageUrl = "Logo/" + obj.loadDeptLogo();
                ImgDepartmentLogo.Visible = true;
            }

            XmlNodeList dept_en = doc.GetElementsByTagName("title_txt_en");
            lblDeptName.Text = dept_en.Item(0).InnerXml;
            XmlNodeList version = doc.GetElementsByTagName("Ver_Name");
            lblversion.Text = version.Item(0).InnerXml;
            //sbXMLdata obj = new sbXMLdata();
            lbldepartmentname.Text = obj.loadDeptName();


        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminGenInfo-M1", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
        }
    }
    private void Errormsg(string msg)
    {
        string popup = _popup.modalPopupSmall("D", "Please Check", msg, "Close");
        Response.Write(popup);
    }

    [WebMethod()]
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
            _common.ErrorLog("seatavailablity-M1", ex.Message);
            return null;
        }
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

                dtbusservice.Columns.Add("bustype", typeof(string));
                dtdepature.Columns.Add("Depature", typeof(string));
                dtdepature.Columns.Add("Depaturecount", typeof(int));
                dtarrival.Columns.Add("Arrival", typeof(string));
                dtarrival.Columns.Add("Arrivalcount", typeof(int));
                dtfare.Columns.Add("fare", typeof(string));

                rptservices.DataSource = dt;
                rptservices.DataBind();


                // ----------------Bus Type filter
                rptbustype.DataSource = dtbusservice;
                rptbustype.DataBind();

                // --------------Departure Filter
                if (countBefore4AM > 0)
                    dtdepature.Rows.Add("Before 4 AM (" + countBefore4AM.ToString() + ")", 1);
                if (count4AM_8AM > 0)
                    dtdepature.Rows.Add("4 AM to 8 AM (" + count4AM_8AM.ToString() + ")", 2);
                if (count8AM_12PM > 0)
                    dtdepature.Rows.Add("8 AM to 12 PM (" + count8AM_12PM.ToString() + ")", 3);
                if (count12PM_4PM > 0)
                    dtdepature.Rows.Add("12 PM to 4 PM (" + count12PM_4PM.ToString() + ")", 4);
                if (count4PM_8PM > 0)
                    dtdepature.Rows.Add("4 PM to 8 PM (" + count4PM_8PM.ToString() + ")", 5);
                if (count8PM_12AM > 0)
                    dtdepature.Rows.Add("8 PM to 12 AM (" + count8PM_12AM.ToString() + ")", 6);

                rptdeparture.DataSource = dtdepature;
                rptdeparture.DataBind();

                // --------------Arrival Filter
                if (acountBefore4AM > 0)
                    dtarrival.Rows.Add("Before 4 AM (" + acountBefore4AM.ToString() + ")", 1);
                if (acount4AM_8AM > 0)
                    dtarrival.Rows.Add("4 AM to 8 AM (" + acount4AM_8AM.ToString() + ")", 2);
                if (acount8AM_12PM > 0)
                    dtarrival.Rows.Add("8 AM to 12 PM (" + acount8AM_12PM.ToString() + ")", 3);
                if (acount12PM_4PM > 0)
                    dtarrival.Rows.Add("12 PM to 4 PM (" + acount12PM_4PM.ToString() + ")", 4);
                if (acount4PM_8PM > 0)
                    dtarrival.Rows.Add("4 PM to 8 PM (" + acount4PM_8PM.ToString() + ")", 5);
                if (acount8PM_12AM > 0)
                    dtarrival.Rows.Add("8 PM to 12 AM (" + acount8PM_12AM.ToString() + ")", 6);

                rptarrival.DataSource = dtarrival;
                rptarrival.DataBind();


                // ---------------Fare Filter
                ddlminfare.Items.Clear();
                ddlmaxfare.Items.Clear();
                ddlminfare.DataSource = dtfare;
                ddlminfare.DataTextField = "fare";
                ddlminfare.DataValueField = "fare";
                ddlminfare.DataBind();
                ddlminfare.Items.Insert(0, "Min");
                ddlminfare.Items[0].Value = "0";
                ddlminfare.SelectedIndex = 0;
                ddlmaxfare.DataSource = dtfare;
                ddlmaxfare.DataTextField = "fare";
                ddlmaxfare.DataValueField = "fare";
                ddlmaxfare.DataBind();

                pnlsearch.Visible = false;
                pnlaftersearch.Visible = true;
                pnldetails.Visible = true;
                pnlNoService.Visible = false;

            }
            else
            {
                lblNoServiceMsg.Text = "Currently No Service is Available for Online Booking , Between Selected Stations.";
                pnlsearch.Visible = true;
                pnlaftersearch.Visible = false;
                pnldetails.Visible = false;
                pnlNoService.Visible = true;
            }
        }
        catch (Exception ex)
        {
            lblNoServiceMsg.Text = "Oops! Something happened with your service loading process.<br> Please feel free to contact the helpdesk";
            pnldetails.Visible = false;
            pnlNoService.Visible = true;
            _common.ErrorLog("seatavailablity-M2", ex.Message);
        }
    }

    #endregion

    #region "Event"
    protected void lbtnSearchServices_Click(object sender, EventArgs e)
    {

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
            Errormsg("Enter valid date");
            return;
        }
        //if (frSton == toSton)
        //{
        //    Errormsg("Station Name Cannot Be Same");
        //    return;
        //}
        lblfromstaion.Text = frSton;
        lbltostation.Text = toSton;
        lbljourneydate.Text = date;
        loadServices(frSton, toSton, date);
        lbtnforwarddate.Visible = true;
        lbtnbackdate.Visible = true;

        DateTime oDate = DateTime.ParseExact(date, "dd/MM/yyyy", null);
        if (oDate == DateTime.Now.Date)
        {
            lbtnbackdate.Visible = false;
        }
        if (oDate == DateTime.Now.Date.AddDays(Convert.ToInt16(hdmaxdate.Value.ToString()) - 1))
        {
            lbtnforwarddate.Visible = false;
        }
        //pnlsearch.Visible = false;
        //pnlaftersearch.Visible = true;

    }
    protected void lbtnmodify_Click(object sender, EventArgs e)
    {
        pnlsearch.Visible = true;
        pnlaftersearch.Visible = false;
    }
    protected void lbtnbackdate_Click(object sender, EventArgs e)
    {
        lbtnforwarddate.Visible = true;
        DateTime oDate = DateTime.ParseExact(tbDate.Text, "dd/MM/yyyy", null);
        if (oDate.AddDays(-1) == DateTime.Now.Date)
        {
            lbtnbackdate.Visible = false;
        }
        tbDate.Text = oDate.AddDays(-1).ToString("dd") + "/" + oDate.AddDays(-1).ToString("MM") + "/" + oDate.AddDays(-1).ToString("yyyy");  //oDate.AddDays(-1).ToString("dd/MM/yyyy");
        lbljourneydate.Text = oDate.AddDays(-1).ToString("dd") + "/" + oDate.AddDays(-1).ToString("MM") + "/" + oDate.AddDays(-1).ToString("yyyy");
        loadServices(tbFrom.Text.Trim().ToUpper(), tbTo.Text.Trim().ToUpper(), tbDate.Text);
    }
    protected void lbtnforwarddate_Click(object sender, EventArgs e)
    {
        lbtnbackdate.Visible = true;
        DateTime oDate = DateTime.ParseExact(tbDate.Text, "dd/MM/yyyy", null);
        if (oDate.AddDays(1) == DateTime.Now.Date.AddDays(Convert.ToInt16(hdmaxdate.Value.ToString()) - 1))
        {
            lbtnforwarddate.Visible = false;
        }
        tbDate.Text = oDate.AddDays(1).ToString("dd") + "/" + oDate.AddDays(1).ToString("MM") + "/" + oDate.AddDays(1).ToString("yyyy");  //oDate.AddDays(-1).ToString("dd/MM/yyyy");
        lbljourneydate.Text = oDate.AddDays(1).ToString("dd") + "/" + oDate.AddDays(1).ToString("MM") + "/" + oDate.AddDays(1).ToString("yyyy");
        loadServices(tbFrom.Text.Trim().ToUpper(), tbTo.Text.Trim().ToUpper(), tbDate.Text);
    }
    protected void rptservices_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {


            HiddenField aminity = (HiddenField)e.Item.FindControl("hdaminity");
            HiddenField aminityurl = (HiddenField)e.Item.FindControl("hdaminityurl");
            Repeater rptaminities = (Repeater)e.Item.FindControl("rptaminities");
            HiddenField totalavablseat = (HiddenField)e.Item.FindControl("totalavablseat");

            LinkButton lbtnbook = (LinkButton)e.Item.FindControl("lbtnbook");
            if (totalavablseat.Value.ToString() == "0")
            {
                lbtnbook.Visible = false;
            }

            if (aminity.Value.ToString() != "0")
            {
                DataTable dtami = new DataTable();
                dtami.Columns.Add("Aminity", typeof(string));
                dtami.Columns.Add("AminityIcon", typeof(string));
                string[] AmitiesList = aminity.Value.Split(',');
                string[] Amitiesurl = aminityurl.Value.Split(',');

                for (int i = 0; i < AmitiesList.Length; i++)
                {
                    dtami.Rows.Add(AmitiesList[i].ToString(), "assets/img/amenity/" + Amitiesurl[i].ToString().Trim());
                }

                if (dtami.Rows.Count > 0)
                {
                    rptaminities.DataSource = dtami;
                    rptaminities.DataBind();
                    rptaminities.Visible = true;
                }

            }
            else
            {
                rptaminities.Visible = false;
            }
            // ---------------Bus Type Filter
            HiddenField lblservicetypename = (HiddenField)e.Item.FindControl("hdservicetypename");
            int countbustype = 0;
            if (dtbusservice.Rows.Count > 0)
            {
                foreach (DataRow row in dtbusservice.Rows)
                {
                }
                for (int i = 0; i <= dtbusservice.Rows.Count - 1; i++)
                {
                    if (dtbusservice.Rows[i]["bustype"].ToString().Trim() == lblservicetypename.Value.Trim())
                        countbustype = 1;
                }
                if (countbustype == 0)
                    dtbusservice.Rows.Add(lblservicetypename.Value.Trim());
            }
            else
            { dtbusservice.Rows.Add(lblservicetypename.Value.Trim()); }

            // -------------------departure filter
            HiddenField diparturetime = (HiddenField)e.Item.FindControl("hddiparturetime");

            DateTime T4AM = DateTime.Parse("4 AM");
            DateTime T8AM = DateTime.Parse("8 AM");
            DateTime T12PM = DateTime.Parse("12:01 PM");
            DateTime T4PM = DateTime.Parse("4 PM");
            DateTime T8PM = DateTime.Parse("8 PM");
            DateTime T12AM = DateTime.Parse("11:59 PM");

            DateTime dFrom;
            DateTime.TryParse(diparturetime.Value.ToString(), out dFrom);
            if (dFrom <= T4AM)
            {
                countBefore4AM = countBefore4AM + 1;
            }
            if (dFrom <= T8AM && dFrom > T4AM)
            {
                count4AM_8AM = count4AM_8AM + 1;
            }
            if (dFrom <= T12PM && dFrom > T8AM)
            {
                count8AM_12PM = count8AM_12PM + 1;
            }
            if (dFrom <= T4PM && dFrom > T12PM)
            {
                count12PM_4PM = count12PM_4PM + 1;
            }
            if (dFrom <= T8PM && dFrom > T4PM)
            {
                count4PM_8PM = count4PM_8PM + 1;
            }
            if (dFrom <= T12AM && dFrom > T8PM)
            {
                count8PM_12AM = count8PM_12AM + 1;
            }


            // -------------------Arrival filter
            HiddenField arrivaltime = (HiddenField)e.Item.FindControl("hdarrivaltime");

            DateTime aT4AM = DateTime.Parse("4 AM");
            DateTime aT8AM = DateTime.Parse("8 AM");
            DateTime aT12PM = DateTime.Parse("12:01 PM");
            DateTime aT4PM = DateTime.Parse("4 PM");
            DateTime aT8PM = DateTime.Parse("8 PM");
            DateTime aT12AM = DateTime.Parse("11:59 PM");

            DateTime dTo;
            DateTime.TryParse(arrivaltime.Value.ToString(), out dTo);
            if (dTo <= T4AM)
            {
                acountBefore4AM = acountBefore4AM + 1;
            }
            if (dTo <= aT8AM && dTo > aT4AM)
            {
                acount4AM_8AM = acount4AM_8AM + 1;
            }
            if (dTo <= aT12PM && dTo > aT8AM)
            {
                acount8AM_12PM = acount8AM_12PM + 1;
            }
            if (dTo <= aT4PM && dTo > aT12PM)
            {
                acount12PM_4PM = acount12PM_4PM + 1;
            }
            if (dTo <= aT8PM && dTo > aT4PM)
            {
                acount4PM_8PM = acount4PM_8PM + 1;
            }
            if (dTo <= aT12AM && dTo > aT8PM)
            {
                acount8PM_12AM = acount8PM_12AM + 1;
            }

            // -------------------fare filter
            HiddenField fare = (HiddenField)e.Item.FindControl("hdfare");
            int countfare = 0;
            if (dtfare.Rows.Count > 0)
            {
                for (int i = 0; i <= dtfare.Rows.Count - 1; i++)
                {
                    if (dtfare.Rows[i]["fare"].ToString().Trim() == fare.Value.Trim())
                        countfare = 1;
                }
                if (countfare == 0)
                    dtfare.Rows.Add(fare.Value.Trim());
            }
            else
                dtfare.Rows.Add(fare.Value.Trim());

            //--------------------Sleeper Filters
            HiddenField layoutcategory = (HiddenField)e.Item.FindControl("hdlayoutcategory");
            if (layoutcategory.Value.ToString().Trim() == "S")
            {
                sleepercount = sleepercount + 1;

            }
            if (sleepercount > 0)
            {
                chksleeper.Checked = true;
                lblfsleeper.Text = "Sleeper (" + sleepercount.ToString() + ")";
                dvsleeper.Visible = true;
            }
            else
            {
                dvsleeper.Visible = false;
            }

            //-----------------------Night Service Filters

            if (dFrom <= T12AM && dFrom > T8PM)
            {
                nightservice = nightservice + 1;
            }
            if (nightservice > 0)
            {
                chknightservice.Checked = true;
                lblfnightservice.Text = "Night Service (" + nightservice.ToString() + ")";
                dvnightservice.Visible = true;
            }
            else
            {
                dvnightservice.Visible = false;
            }


        }
    }
    protected void chksleeper_CheckedChanged(object sender, EventArgs e)
    {
        if (sender != null)
        {
            if (((CheckBox)sender).Checked)
            {
                foreach (RepeaterItem ri in rptservices.Items)
                {
                    if (ri.ItemType == ListItemType.Item || ri.ItemType == ListItemType.AlternatingItem)
                    {
                        HiddenField layoutcategory = (HiddenField)ri.FindControl("hdlayoutcategory");
                        if (layoutcategory.Value.ToString().Trim() == "S")
                        {

                            ri.Visible = true;
                        }
                    }
                }
            }
            else
            {
                foreach (RepeaterItem ri in rptservices.Items)
                {
                    if (ri.ItemType == ListItemType.Item || ri.ItemType == ListItemType.AlternatingItem)
                    {
                        HiddenField layoutcategory = (HiddenField)ri.FindControl("hdlayoutcategory");
                        if (layoutcategory.Value.ToString().Trim() == "S")
                        {
                            ri.Visible = false;
                        }
                    }
                }
            }
        }
    }
    protected void chknightservice_CheckedChanged(object sender, EventArgs e)
    {
        if (sender != null)
        {
            if (((CheckBox)sender).Checked)
            {
                foreach (RepeaterItem ri in rptservices.Items)
                {
                    if (ri.ItemType == ListItemType.Item || ri.ItemType == ListItemType.AlternatingItem)
                    {
                        HiddenField diparturetime = (HiddenField)ri.FindControl("hddiparturetime");
                        DateTime T8PM = DateTime.Parse("8 PM");
                        DateTime T12AM = DateTime.Parse("11:59 PM");
                        DateTime dFrom;
                        DateTime.TryParse(diparturetime.Value.ToString(), out dFrom);
                        if (dFrom <= T12AM && dFrom > T8PM)
                        {
                            ri.Visible = true;
                        }
                    }
                }
            }
            else
            {
                foreach (RepeaterItem ri in rptservices.Items)
                {
                    if (ri.ItemType == ListItemType.Item || ri.ItemType == ListItemType.AlternatingItem)
                    {
                        HiddenField diparturetime = (HiddenField)ri.FindControl("hddiparturetime");
                        DateTime T8PM = DateTime.Parse("8 PM");
                        DateTime T12AM = DateTime.Parse("11:59 PM");
                        DateTime dFrom;
                        DateTime.TryParse(diparturetime.Value.ToString(), out dFrom);
                        if (dFrom <= T12AM && dFrom > T8PM)
                        {
                            ri.Visible = false;
                        }
                    }
                }
            }
        }
    }
    protected void chkDepature_CheckedChanged(object sender, EventArgs e)
    {
        if (sender != null)
        {
            DateTime T4AM = DateTime.Parse("4 AM");
            DateTime T8AM = DateTime.Parse("8 AM");
            DateTime T12PM = DateTime.Parse("12:01 PM");
            DateTime T4PM = DateTime.Parse("4 PM");
            DateTime T8PM = DateTime.Parse("8 PM");
            DateTime T12AM = DateTime.Parse("11:59 PM");
            HiddenField depaturetime = (HiddenField)((CheckBox)sender).Parent.FindControl("depaturetime");

            if (((CheckBox)sender).Checked)
            {
                foreach (RepeaterItem ri in rptservices.Items)
                {
                    if (ri.ItemType == ListItemType.Item || ri.ItemType == ListItemType.AlternatingItem)
                    {
                        HiddenField diparturetime = (HiddenField)ri.FindControl("hddiparturetime");
                        DateTime dFrom;
                        DateTime.TryParse(diparturetime.Value, out dFrom);
                        if (depaturetime.Value.ToString() == "1" && dFrom <= T4AM)
                            ri.Visible = true;
                        else if (depaturetime.Value.ToString() == "2" && dFrom <= T8AM && dFrom > T4AM)
                            ri.Visible = true;
                        else if (depaturetime.Value.ToString() == "3" && dFrom <= T12PM && dFrom > T8AM)
                            ri.Visible = true;
                        else if (depaturetime.Value.ToString() == "4" && dFrom <= T4PM && dFrom > T12PM)
                            ri.Visible = true;
                        else if (depaturetime.Value.ToString() == "5" && dFrom <= T8PM && dFrom > T4PM)
                            ri.Visible = true;
                        else if (depaturetime.Value.ToString() == "6" && dFrom <= T12AM && dFrom > T8PM)
                            ri.Visible = true;
                    }
                }
            }
            else
            {
                foreach (RepeaterItem ri in rptservices.Items)
                {
                    if (ri.ItemType == ListItemType.Item || ri.ItemType == ListItemType.AlternatingItem)
                    {
                        HiddenField diparturetime = (HiddenField)ri.FindControl("hddiparturetime");
                        DateTime dFrom;
                        DateTime.TryParse(diparturetime.Value, out dFrom);
                        if (depaturetime.Value.ToString() == "1" && dFrom <= T4AM)
                            ri.Visible = false;
                        else if (depaturetime.Value.ToString() == "2" && dFrom <= T8AM && dFrom > T4AM)
                            ri.Visible = false;
                        else if (depaturetime.Value.ToString() == "3" && dFrom <= T12PM && dFrom > T8AM)
                            ri.Visible = false;
                        else if (depaturetime.Value.ToString() == "4" && dFrom <= T4PM && dFrom > T12PM)
                            ri.Visible = false;
                        else if (depaturetime.Value.ToString() == "5" && dFrom <= T8PM && dFrom > T4PM)
                            ri.Visible = false;
                        else if (depaturetime.Value.ToString() == "6" && dFrom <= T12AM && dFrom > T8PM)
                            ri.Visible = false;
                    }
                }
            }
        }
    }
    protected void chkArrival_CheckedChanged(object sender, EventArgs e)
    {
        if (sender != null)
        {
            DateTime aT4AM = DateTime.Parse("4 AM");
            DateTime aT8AM = DateTime.Parse("8 AM");
            DateTime aT12PM = DateTime.Parse("12:01 PM");
            DateTime aT4PM = DateTime.Parse("4 PM");
            DateTime aT8PM = DateTime.Parse("8 PM");
            DateTime aT12AM = DateTime.Parse("11:59 PM");
            HiddenField harrivaltime = (HiddenField)((CheckBox)sender).Parent.FindControl("arrivaltime");

            if (((CheckBox)sender).Checked)
            {
                foreach (RepeaterItem ri in rptservices.Items)
                {
                    if (ri.ItemType == ListItemType.Item || ri.ItemType == ListItemType.AlternatingItem)
                    {
                        HiddenField arrivaltime = (HiddenField)ri.FindControl("hdarrivaltime");
                        DateTime dTo;
                        DateTime.TryParse(arrivaltime.Value, out dTo);
                        if (harrivaltime.Value.ToString() == "1" && dTo <= aT4AM)
                            ri.Visible = true;
                        else if (harrivaltime.Value.ToString() == "2" && dTo <= aT8AM && dTo > aT4AM)
                            ri.Visible = true;
                        else if (harrivaltime.Value.ToString() == "3" && dTo <= aT12PM && dTo > aT8AM)
                            ri.Visible = true;
                        else if (harrivaltime.Value.ToString() == "4" && dTo <= aT4PM && dTo > aT12PM)
                            ri.Visible = true;
                        else if (harrivaltime.Value.ToString() == "5" && dTo <= aT8PM && dTo > aT4PM)
                            ri.Visible = true;
                        else if (harrivaltime.Value.ToString() == "6" && dTo <= aT12AM && dTo > aT8PM)
                            ri.Visible = true;
                    }
                }
            }
            else
            {
                foreach (RepeaterItem ri in rptservices.Items)
                {
                    if (ri.ItemType == ListItemType.Item || ri.ItemType == ListItemType.AlternatingItem)
                    {
                        HiddenField arrivaltime = (HiddenField)ri.FindControl("hdarrivaltime");
                        DateTime dTo;
                        DateTime.TryParse(arrivaltime.Value, out dTo);
                        if (harrivaltime.Value.ToString() == "1" && dTo <= aT4AM)
                            ri.Visible = false;
                        else if (harrivaltime.Value.ToString() == "2" && dTo <= aT8AM && dTo > aT4AM)
                            ri.Visible = false;
                        else if (harrivaltime.Value.ToString() == "3" && dTo <= aT12PM && dTo > aT8AM)
                            ri.Visible = false;
                        else if (harrivaltime.Value.ToString() == "4" && dTo <= aT4PM && dTo > aT12PM)
                            ri.Visible = false;
                        else if (harrivaltime.Value.ToString() == "5" && dTo <= aT8PM && dTo > aT4PM)
                            ri.Visible = false;
                        else if (harrivaltime.Value.ToString() == "6" && dTo <= aT12AM && dTo > aT8PM)
                            ri.Visible = false;
                    }
                }
            }
        }
    }
    protected void chkbustype_CheckedChanged(object sender, EventArgs e)
    {
        if (sender != null)
        {
            HiddenField service = (HiddenField)((CheckBox)sender).Parent.FindControl("hdservice");

            if (((CheckBox)sender).Checked)
            {
                foreach (RepeaterItem ri in rptservices.Items)
                {
                    if (ri.ItemType == ListItemType.Item || ri.ItemType == ListItemType.AlternatingItem)
                    {
                        HiddenField servicetypename = (HiddenField)ri.FindControl("hdservicetypename");
                        if (service.Value.ToUpper() == servicetypename.Value.ToUpper())
                        {
                            ri.Visible = true;
                        }
                    }
                }
            }
            else
            {
                foreach (RepeaterItem ri in rptservices.Items)
                {
                    if (ri.ItemType == ListItemType.Item || ri.ItemType == ListItemType.AlternatingItem)
                    {
                        HiddenField servicetypename = (HiddenField)ri.FindControl("hdservicetypename");
                        if (service.Value.ToUpper() == servicetypename.Value.ToUpper())
                        {
                            ri.Visible = false;
                        }
                    }
                }
            }
        }
    }
    protected void ddlminfare_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (RepeaterItem ri in rptservices.Items)
        {
            if (ri.ItemType == ListItemType.Item || ri.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField fare = (HiddenField)ri.FindControl("hdfare");
                if (Convert.ToDecimal(fare.Value.ToString()) >= Convert.ToDecimal(ddlminfare.SelectedValue.ToString()) && Convert.ToDecimal(fare.Value.ToString()) <= Convert.ToDecimal(ddlmaxfare.SelectedValue.ToString()))
                {
                    ri.Visible = true;
                }
                else
                {
                    ri.Visible = false;
                }
            }
        }
    }
    protected void ddlmaxfare_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (RepeaterItem ri in rptservices.Items)
        {
            if (ri.ItemType == ListItemType.Item || ri.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField fare = (HiddenField)ri.FindControl("hdfare");
                if (Convert.ToDecimal(fare.Value.ToString()) >= Convert.ToDecimal(ddlminfare.SelectedValue.ToString()) && Convert.ToDecimal(fare.Value.ToString()) <= Convert.ToDecimal(ddlmaxfare.SelectedValue.ToString()))
                {
                    ri.Visible = true;
                }
                else
                {
                    ri.Visible = false;
                }
            }
        }
    }
    protected void lbtnexchange_Click(object sender, EventArgs e)
    {
        string frmstation = tbFrom.Text;
        string tostation = tbTo.Text;
        tbTo.Text = frmstation;
        tbFrom.Text = tostation;
        pnlsearch.Visible = true;
        pnlaftersearch.Visible = false;
        pnldetails.Visible = false;
        pnlNoService.Visible = true;
    }
    #endregion

    protected void rptservices_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "BOOKNOW")
        {
            HiddenField hddiparturetime = (HiddenField)e.Item.FindControl("hddiparturetime");
            HiddenField hdarrivaltime = (HiddenField)e.Item.FindControl("hdarrivaltime");
            HiddenField hdfare = (HiddenField)e.Item.FindControl("hdfare");
            HiddenField hdservicetypename = (HiddenField)e.Item.FindControl("hdservicetypename");

            HiddenField rptHdDsvcid = (HiddenField)e.Item.FindControl("rptHdDsvcid");
            HiddenField rptHdStrpid = (HiddenField)e.Item.FindControl("rptHdStrpid");
            HiddenField rptHdTripDirection = (HiddenField)e.Item.FindControl("rptHdTripDirection");
            HiddenField rptHdLayoutCode = (HiddenField)e.Item.FindControl("rptHdLayoutCode");

            HiddenField rptHdFromStonId = (HiddenField)e.Item.FindControl("rptHdFromStonId");
            HiddenField rptHdFromStonName = (HiddenField)e.Item.FindControl("rptHdFromStonName");
            HiddenField rptHdToStonId = (HiddenField)e.Item.FindControl("rptHdToStonId");
            HiddenField rptHdToStonName = (HiddenField)e.Item.FindControl("rptHdToStonName");


            Dictionary<string, string> dct = new Dictionary<string, string>();
            dct.Add("dsvcid", rptHdDsvcid.Value);
            dct.Add("strpid", rptHdStrpid.Value);
            dct.Add("depttime", hddiparturetime.Value);
            dct.Add("arrtime", hdarrivaltime.Value);
            dct.Add("tripdirection", rptHdTripDirection.Value);
            dct.Add("servicetypename", hdservicetypename.Value);
            dct.Add("totalfare", hdfare.Value);
            dct.Add("layout", rptHdLayoutCode.Value);
            dct.Add("frstonid", rptHdFromStonId.Value);
            dct.Add("fromstation", rptHdFromStonName.Value);
            dct.Add("tostonid", rptHdToStonId.Value);
            dct.Add("tostation", rptHdToStonName.Value);
            dct.Add("date", tbDate.Text);

            Session["SearchParameters"] = dct;
            setLayout_and_boardings(rptHdDsvcid.Value, rptHdStrpid.Value, rptHdTripDirection.Value, rptHdLayoutCode.Value, tbDate.Text, rptHdFromStonId.Value, rptHdToStonId.Value);

            lblServiceType.Text = rptHdDsvcid.Value + "" + rptHdTripDirection.Value + "" + rptHdStrpid.Value + " | " + hdservicetypename.Value;
            lblFromStationName.Text = rptHdFromStonName.Value;
            lblToStationName.Text = rptHdToStonName.Value;
            lblDateTime.Text = tbDate.Text + " " + hddiparturetime.Value;

            resetLayoutPnl();
            pnlSearchList.Visible = false;
            pnlLayout.Visible = true;
            pnlConcession.Visible = false;

        }
    }

    #region "Layout Related"

    NpgsqlCommand MyCommand;
    sbBLL bll = new sbBLL();
    sbValidation _SecurityCheck = new sbValidation();
    sbSecurity _security = new sbSecurity();

    DataTable dt = new DataTable();
    sbValidation _validation = new sbValidation();
    int intPassengerNo = 0;
    string commonerror = "There is some error. Please contact the administrator or try again after some time.";
    public void resetLayoutPnl()
    {

    }
    private void Successmsg(string msg)
    {
        string popup = _popup.modalPopupSmall("S", "Success", msg, "Close");
        Response.Write(popup);
    }

    public void setLayout_and_boardings(string dsvcid, string strpid, string tripdirection, string layout, string date, string fromStonId, string toStonId) //M2
    {
        try
        {
            wsClass obj = new wsClass();
            string StrSeatStructDown = "";

            DataTable dtTotalRowCol = obj.getLayoutTotRowColumn(dsvcid);
            if (dtTotalRowCol.Rows.Count > 0)
            {
                foreach (DataRow dRowCol in dtTotalRowCol.Rows)
                {
                    StrSeatStructDown = dRowCol["noofrows"] + "-" + dRowCol["noofcolumns"] + "-500";
                }
            }
            DataTable dtLayout = obj.getLayoutRowColumn(strpid, tripdirection, dsvcid, date);
            if (dtLayout.Rows.Count > 0)
            {
                foreach (DataRow drow in dtLayout.Rows)
                {
                    StrSeatStructDown = StrSeatStructDown + "," + drow["colnumber"].ToString() + "-" + drow["rownumber"].ToString() + "-" + drow["seatno"].ToString() + "-" + drow["seatyn"] + "-" + drow["travellertypecode"] + "-" + drow["seatavailforonlbooking"] + "-" + drow["status"];
                }
            }
            hfSeatStructUp.Value = StrSeatStructDown;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "getSeatStruct('up','" + StrSeatStructDown + "');", true);

            string strCat = getconcession(dsvcid, fromStonId, toStonId);
            hfCategoryList.Value = strCat.Substring(1, strCat.Length - 1);

            BindBoardingStations(ddlBoardingfrom, dsvcid, strpid);
            DataTable dtt = obj.gettermsCondition();
            lblTermsConditions.Text = dtt.Rows[0]["termconditiondtls"].ToString();
        }
        catch (Exception ex)
        {
            Errormsg(ex.ToString());
            _common.ErrorLog("trvlrSeatslct-M2", ex.ToString());
        }
    }
    private void BindBoardingStations(DropDownList ddlBoardingfrom, string ServiceCode, string ServiceTripCode)//M3
    {
        try
        {
            wsClass obj = new wsClass();
            ddlBoardingfrom.Items.Clear();
            dt = obj.get_boardingStations_dt(ServiceCode, ServiceTripCode);
            if (dt.Rows.Count > 0)
            {
                ddlBoardingfrom.DataSource = dt;
                ddlBoardingfrom.DataTextField = "p_stname";
                ddlBoardingfrom.DataValueField = "p_stcode";
                ddlBoardingfrom.DataBind();
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.ToString());
            _common.ErrorLog("trvlrSeatslct-M3", ex.ToString());
        }
    }

    private string getconcession(string dsvcid, string fromStation, string toStation)//M5
    {
        string strCat = "";
        try
        {
            wsClass obj = new wsClass();
            DataTable dtConcession = obj.getConcessionCategory(dsvcid, fromStation, toStation);

            if (dtConcession.Rows.Count > 0)
            {
                foreach (DataRow drow in dtConcession.Rows)
                {
                    strCat = strCat + "," + drow["categorycode"].ToString() + "-" + drow["categoryname"].ToString();
                }
            }
            return strCat;
        }
        catch (Exception ex)
        {
            return "";
        }
    }

    protected void lbtnCloseLayoutPnl_Click(object sender, EventArgs e)
    {
        pnlSearchList.Visible = true;
        pnlLayout.Visible = false;
        pnlConcession.Visible = false;
    }
    //protected void lbtnProceed_Click_(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        
    //        string boardingStationId = ddlBoardingfrom.SelectedValue;
    //        string boardingStationName = ddlBoardingfrom.SelectedItem.Text;

    //        if (_validation.IsValidInteger(boardingStationId, 1, 5) == false)
    //        {
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "getSeatStruct('up','" + hfSeatStructUp.Value + "');", true);
    //            Errormsg("Please select Boarding Station for Journey");
    //            return;
    //        }

    //        if (_security.isSessionExist(Session["SearchParameters"]) == true)
    //        {
    //            Dictionary<string, string> dct = new Dictionary<string, string>();
    //            dct = (Dictionary<string, string>)Session["SearchParameters"];


    //            string psngr = hfCustomerData.Value.ToString();
    //            dct.Add("passengers", psngr);
    //            dct.Add("boardingId", boardingStationId);
    //            dct.Add("boardingName", boardingStationName);

    //            Session["SearchParameters"] = dct;
    //            Response.Redirect("seatavailablityOtp.aspx");

    //        }

    //        else
    //        {
    //            Response.Redirect("errorpage.aspx");
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Errormsg(ex.ToString());
    //        _common.ErrorLog("trvlrSeatslct-M4", ex.ToString());
    //    }
    //}

    protected void lbtnProceed_Click(object sender, EventArgs e)
    {
        try
        {
            Session["MobileNo"] = "";
            wsClass obj = new wsClass();
            string boardingStationId = ddlBoardingfrom.SelectedValue;
            string boardingStationName = ddlBoardingfrom.SelectedItem.Text;

            if (int.Parse(boardingStationId) <= 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "getSeatStruct('up','" + hfSeatStructUp.Value + "');", true);
                Errormsg("Please select Boarding Station for Journey");
                return;
            }

            if (_security.isSessionExist(Session["SearchParameters"]) == true)
            {

                Dictionary<string, string> dct = new Dictionary<string, string>();
                dct = (Dictionary<string, string>)Session["SearchParameters"];

                string psngr = hfCustomerData.Value.ToString();

                if ((dct.ContainsKey("boardingId")))
                    dct.Remove("boardingId");
                dct.Add("boardingId", boardingStationId);

                if ((dct.ContainsKey("boardingName")))
                    dct.Remove("boardingName");
                dct.Add("boardingName", boardingStationName);

                Session["SearchParameters"] = dct;

                // New Neeraj Concession Start
                var strpid = dct["strpid"];
                var depttime = dct["depttime"];
                var arrtime = dct["arrtime"];
                var servicetypename = dct["servicetypename"];
                var fromstation = dct["fromstation"];
                var tostation = dct["tostation"];
                string journeyDate = dct["date"];

                string passengers = hfCustomerData.Value;

                int resultcount = 0;
                string result = "";

                if (obj.checkTripGenerate(journeyDate, strpid) == false)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "getSeatStruct('up','" + hfSeatStructUp.Value + "');", true);
                    Errormsg("Sorry, Booking has been closed for selected service. Please change your selection and try again.");
                    return;
                }

                if (obj.IsTicketStillAvaialable(psngr, journeyDate, "UP", strpid) == false)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "getSeatStruct('up','" + hfSeatStructUp.Value + "');", true);
                    Errormsg("You are late, Booking has already been made for the selected seat(s)");
                    return;
                }


                int concession_count = 0;
                int Index = 0;

                DataTable table = new DataTable();
                table.Columns.Add("SeatNo", typeof(string));
                table.Columns.Add("Name", typeof(string));
                table.Columns.Add("Age", typeof(string));
                table.Columns.Add("Gender", typeof(string));
                table.Columns.Add("Concession", typeof(string));
                table.Columns.Add("JourneyType", typeof(string));
                table.Columns.Add("Fare", typeof(string));
                table.Columns.Add("OnlineVerificationYN", typeof(string));
                table.Columns.Add("IdVerificationYN", typeof(string));
                table.Columns.Add("Idverification", typeof(string));
                table.Columns.Add("DocumentVerificationYN", typeof(string));
                table.Columns.Add("DocumentVerification", typeof(string));
                table.Columns.Add("ConcessionName", typeof(string));

                string[] CustomerList = passengers.Split('|');
                foreach (string customer in CustomerList)
                {
                    string[] customerDetail = customer.Split(',');
                    string SeatNo = customerDetail[0];
                    string custName = customerDetail[1].ToUpper().Trim();
                    string custGender = customerDetail[2];
                    Int16 custAge = Convert.ToInt16(customerDetail[3].Trim());
if (custAge > 99)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "getSeatStruct('up','" + hfSeatStructUp.Value + "');", true);
                        Errormsg("Invalid Age");
                        return;
                    }
                    string cusConcession = customerDetail[4];
                    string cusJourneyType = customerDetail[5];
                    string cusFare = customerDetail[6];

                    string OnlineVerificationYN = "N";
                    string IdVerificationYN = "N";
                    string Idverification = "";
                    string DocumentVerificationYN = "N";
                    string DocumentVerification = "";
                    string ConcessionName = "No Concession";

                    if (cusConcession != "1")
                    {
                        concession_count = concession_count + 1;
                        DataTable dtcheckconcession = obj.CheckConcessionCategory(cusConcession, custGender, custAge.ToString());
                        if (dtcheckconcession.Rows.Count > 0)
                        {
                            ConcessionName = dtcheckconcession.Rows[0]["sp_CONCESSION_NAME"].ToString();
                            OnlineVerificationYN = dtcheckconcession.Rows[0]["sp_ONLINEVERIFICATION_YN"].ToString();
                            IdVerificationYN = dtcheckconcession.Rows[0]["sp_IDVERIFICATION_YN"].ToString();
                            if (DBNull.Value.Equals(dtcheckconcession.Rows[0]["sp_IDVERIFICATION"]) == true)
                                Idverification = "";
                            else
                                Idverification = dtcheckconcession.Rows[0]["sp_IDVERIFICATION"].ToString();

                            DocumentVerificationYN = dtcheckconcession.Rows[0]["sp_DOCUMENTVERIFICATION_YN"].ToString();
                            if (DBNull.Value.Equals(dtcheckconcession.Rows[0]["sp_DOCUMENTVERIFICATION"]) == true)
                                DocumentVerification = "";
                            else
                                DocumentVerification = dtcheckconcession.Rows[0]["sp_DOCUMENTVERIFICATION"].ToString();

                            if (dtcheckconcession.Rows[0]["p_Gender_Result"].ToString().ToUpper() != "SUCCESS")
                            {
                                resultcount = resultcount + 1;
                                result = result + "Seat No " + SeatNo + " Invalid Gender Selection For Concession <br/>";
                            }
                            else if (dtcheckconcession.Rows[0]["p_Age_Result"].ToString().ToUpper() != "SUCCESS")
                            {
                                resultcount = resultcount + 1;
                                result = result + "Seat No " + SeatNo + " Invalid Age For Concession <br/>";
                            }
                            else
                            {
                                if (dtcheckconcession.Rows[0]["sp_ONLINEVERIFICATION_YN"].ToString() == "Y")
                                {
                                }
                                if (dtcheckconcession.Rows[0]["sp_IDVERIFICATION_YN"].ToString() == "Y")
                                {
                                }
                                if (dtcheckconcession.Rows[0]["sp_DOCUMENTVERIFICATION_YN"].ToString() == "Y")
                                {
                                }
                            }
                        }
                    }
                    table.Rows.Add(SeatNo, custName, custAge, custGender, cusConcession, cusJourneyType, cusFare, OnlineVerificationYN, IdVerificationYN, Idverification, DocumentVerificationYN, DocumentVerification, ConcessionName);
                    Index = Index + 1;
                }

                if (resultcount > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "getSeatStruct('up','" + hfSeatStructUp.Value + "');", true);
                    Errormsg(result);
                    return;
                }
                if (concession_count >= 1 && table.Rows.Count > 1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "getSeatStruct('up','" + hfSeatStructUp.Value + "');", true);
                    Errormsg("Sorry booking is not allowed as in a ticket with concession only one seat can be booked at a time");
                    return;
                }
                gvSeats.DataSource = table;
                gvSeats.DataBind();
                if (concession_count > 0)
                {
                    string dsvcid = dct["dsvcid"];
                    string tripdirection = dct["tripdirection"];

                    lblServiceType2.Text = dsvcid + "" + tripdirection + "" + strpid + " | " + servicetypename;
                    lblFromStationName2.Text = fromstation;
                    lblToStationName2.Text = tostation;
                    lblDateTime2.Text = tbDate.Text + " " + depttime;

                    pnlConcession.Visible = true;
                    pnlLayout.Visible = false;
                    pnlSearchList.Visible = false;
                }
                else
                    lbtnConfirmConcession_Click(lbtnProceed, null/* TODO Change to default(_) if this is not a reference type */);
            }
            else
                Response.Redirect("sessionTimeout.aspx");
        }
        catch (Exception ex)
        {
            Errormsg(ex.ToString());
        }
    }

    protected void lbtnConfirmConcession_Click(object sender, EventArgs e)
    {
        if (Session["SearchParameters"] != null)
        {
            
            wsClass obj = new wsClass();
            string passengers;
            passengers = "";
            Dictionary<string, string> dct = new Dictionary<string, string>();
            dct = (Dictionary<string, string>)Session["SearchParameters"];
            string journeyDate = dct["date"];
            var dsvcid = dct["dsvcid"];
            var strpid = dct["strpid"];
            var tripdirection = dct["tripdirection"];
            var fromstationId = dct["frstonid"];
            var tostationId = dct["tostonid"];

            string msg = "";
            int msgcount = 0;
            if (gvSeats.Rows.Count > 0)
            {
                foreach (GridViewRow row in gvSeats.Rows)
                {
                    string seatno = gvSeats.DataKeys[row.RowIndex]["SeatNo"].ToString();
                    string Name = gvSeats.DataKeys[row.RowIndex]["Name"].ToString();
                    string Gender = gvSeats.DataKeys[row.RowIndex]["Gender"].ToString();
                    string Age = gvSeats.DataKeys[row.RowIndex]["Age"].ToString();
                    string Concession = gvSeats.DataKeys[row.RowIndex]["Concession"].ToString();
                    string JourneyType = gvSeats.DataKeys[row.RowIndex]["JourneyType"].ToString();
                    string Fare = gvSeats.DataKeys[row.RowIndex]["Fare"].ToString();
                    TextBox tbpass_docid = row.FindControl("tbpass_docid") as TextBox;

                    string OnlineVerificationYN = gvSeats.DataKeys[row.RowIndex]["OnlineVerificationYN"].ToString();
                    string Passnumber = "";
                    if (gvSeats.DataKeys[row.RowIndex]["OnlineVerificationYN"].ToString() == "Y")
                        Passnumber = tbpass_docid.Text.ToString().Trim();
                    string IdVerificationYN = gvSeats.DataKeys[row.RowIndex]["IdVerificationYN"].ToString();
                    string Idverification = "";
                    if (gvSeats.DataKeys[row.RowIndex]["IdVerificationYN"].ToString() == "Y")
                        Idverification = tbpass_docid.Text.ToString().Trim();
                    string DocumentVerificationYN = gvSeats.DataKeys[row.RowIndex]["DocumentVerificationYN"].ToString();
                    string DocumentVerification = gvSeats.DataKeys[row.RowIndex]["DocumentVerification"].ToString();

                    if (tbpass_docid.Text == "")
                    {
                        if (gvSeats.DataKeys[row.RowIndex]["OnlineVerificationYN"].ToString() == "Y")
                        {
                            msgcount = msgcount + 1;
                            msg = msg + "Enter Pass Number For seat No " + seatno + "<span style=" + "font-size:14px;color:red" + "> (If you are not having a valid pass please visit our Bus Pass section <a href=" + "busPass.aspx" + ">Bus Pass Section</a>)</span>" + "<br/>";
                        }
                        if (gvSeats.DataKeys[row.RowIndex]["IdVerificationYN"].ToString() == "Y")
                        {
                            msgcount = msgcount + 1;
                            msg = msg + "Enter Id For Selected Document ID For seat No " + seatno + "<br/>";
                        }
                    }
                    else if (gvSeats.DataKeys[row.RowIndex]["OnlineVerificationYN"].ToString() == "Y")
                    {
                        dt = obj.CheckBusPassNew(Concession, tbpass_docid.Text.ToString().Trim(), journeyDate);
                        if (dt.Rows[0]["p_result"].ToString() != "Success")
                        {
                            msgcount = msgcount + 1;
                            msg = msg + "Enter Pass Number For seat No " + seatno + "<span style=" + "font-size:14px;color:red" + "> (If you are not having a valid pass please visit our Bus Pass section <a href=" + "busPass.aspx" + ">Bus Pass Section</a>)</span>" + "<br/>";
                        }
                        else
                        {
                            Session["MobileNo"] = dt.Rows[0]["mob"].ToString();
                        }
                    }
                    passengers = passengers + seatno + "," + Name + "," + Gender + "," + Age + "," + Concession + "," + JourneyType + "," + Fare + "," + OnlineVerificationYN + "," + Passnumber + "," + IdVerificationYN + "," + Idverification + "," + DocumentVerificationYN + "," + DocumentVerification + "|";
                    tbpass_docid.Text = "";
                }

                if (msgcount > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "getSeatStruct('up','" + hfSeatStructUp.Value + "');", true);
                    Errormsg(msg);
                    return;
                }

                passengers = passengers.Substring(0, passengers.Length - 1);

                if ((dct.ContainsKey("passengers")))
                    dct.Remove("passengers");
                dct.Add("passengers", passengers);
                Session["SearchParameters"] = dct;
                Response.Redirect("seatavailablityOtp.aspx");
            }
            else
            {
                Errormsg("Please Select Seats First");
                return;
            }
        }
        else
        {
            Errormsg("You are late, Booking has already been made for the selected seat(s). ");
            return;
        }
    }

    protected void gvSeats_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblheading = (Label)e.Row.FindControl("lblheading");
            TextBox tbpass_docid = (TextBox)e.Row.FindControl("tbpass_docid");
            lblheading.Visible = false;
            tbpass_docid.Visible = false;

            DataRowView rowView = (DataRowView)e.Row.DataItem;
            if (rowView["OnlineVerificationYN"].ToString() == "Y")
            {
                lblheading.ForeColor = System.Drawing.Color.Red;
                lblheading.Visible = true;
                lblheading.Text = "Enter Pass Number Provided by Department";
                tbpass_docid.Visible = true;
            }
            else if (rowView["IdVerificationYN"].ToString() == "Y")
            {
                lblheading.Visible = true;
                lblheading.ForeColor = System.Drawing.Color.Red;
                lblheading.Text = "Enter the ID of any one of these documents - " + rowView["Idverification"].ToString();
                tbpass_docid.Visible = true;
            }
            else if (rowView["DocumentVerificationYN"].ToString() == "Y")
            {
                lblheading.ForeColor = System.Drawing.Color.Green;
                lblheading.Visible = true;
                lblheading.Text = "Keep any one of these documents ready at the time of jouney - " + rowView["DocumentVerification"].ToString();
            }
        }
    }
    protected void lbtnclose_Click(object sender, EventArgs e)
    {
        gvSeats.DataSource = null;
        gvSeats.DataBind();

        pnlSearchList.Visible = true;
        pnlLayout.Visible = false;
        pnlConcession.Visible = false;
    }


    #endregion

    protected void cbACOnly_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void cbACNonOnly_CheckedChanged(object sender, EventArgs e)
    {

    }
}