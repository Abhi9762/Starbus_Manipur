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
using System.Xml.Linq;

public partial class homee : System.Web.UI.Page
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    sbValidation _SecurityCheck = new sbValidation();
    sbSummary obj = new sbSummary();
   
    private bool DBConnectionStatus()
    {
        bool b = false;
        try
        {
            string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["constr"].ToString();
            using (NpgsqlConnection sqlConn = new NpgsqlConnection(conStr))
            {
                sqlConn.Open();
                if ((sqlConn.State == ConnectionState.Open))
                    b = true;
                else
                    b = false;
                sqlConn.Close();
            }
            return b;
        }
        catch (Exception e2)
        {
            return false;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (DBConnectionStatus() == false)
            {
                Response.Redirect("DBError.html");
            }
            checkAdditionalModules();
            GetServiceType();
            loadGenralData();
            loadFaqCategory("TICKET");
            loadofcwisecontact(10);
            loadCategory();


            lblCount.Text = Session["_vstrCntr"].ToString();

            loadEventnotice();
            getAdvanceDaysBooking();
            loadPopularRoutes();
            loadCondictorMonth();
            //offers();

            lblcancellationpolicy.Text = _common.getCancellationpolicy();
            //CheckTravellerBooking();


        }
    }
    private void checkAdditionalModules()
    {
        lblagents.Visible = false;
        lblcsccentres.Visible = false;
        lblparcelbooking.Visible = false;
        divbuspass.Visible = false;
        divagent.Visible = false;
        lblbuspassahref.Visible = false;
        if (sbXMLdata.checkModuleCategory("70") == true)
        {
            lblagents.Visible = true;
            divagent.Visible = true;
        }
        if (sbXMLdata.checkModuleCategory("71") == true)
        {
            divbuspass.Visible = true;
            lblbuspassahref.Visible = true;
        }
        if (sbXMLdata.checkModuleCategory("72") == true)
        {
            lblcsccentres.Visible = true;
        }
    }

    #region "Serach"
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
            _common.ErrorLog("DashWeb-M1", ex.Message);
            return null;
        }
    }

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
            Errormsg("Enter valid Date");
            return;
        }
        //if (frSton == toSton)
        //{
        //    Errormsg("Station Name Cannot Be Same");
        //    return;
        //}
        loadServices(frSton, toSton, date);
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
                Errormsg("Sorry, Services are not available now");
            }
        }
        catch (Exception ex)
        {
            Errormsg("No Service");
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
        { }
    }
    #endregion

    #region "Populer Route"
    private void loadPopularRoutes()
    {

        DataTable dt = obj.getRouteBooking(5, "T");
        rptRoutes.DataSource = dt;
        rptRoutes.DataBind();
    }

    protected void rptRoutes_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item)
        {
            string current_date = DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("yyyy");

            LinkButton lbtnroute = (LinkButton)e.Item.FindControl("lbtnroute");
            HiddenField frmstation = (HiddenField)e.Item.FindControl("hddfromst");
            HiddenField tostation = (HiddenField)e.Item.FindControl("hddtost");
            string date = DateTime.Now.Date.ToString("dd/MM/yyyy");
            loadServices(frmstation.Value, tostation.Value, current_date);
        }
    }
    #endregion

    #region "Event Alert"
    private void loadEventnotice()//M2
    {
        try
        {
            pnlNoticeNews.Visible = false;
            pnlNoNoticeNews.Visible = true;
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_notice_dtls");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {

                    DataTable dtnews = new DataTable();
                    DataRow[] drrnews = dt.Select("category_code=3");
                    if (drrnews.Length > 0)
                    {
                        dtnews = drrnews.CopyToDataTable();
                    }

                    if (dtnews.Rows.Count > 0)
                    {
                        lvnews.DataSource = dtnews;
                        lvnews.DataBind();
                        pnlNoticeNews.Visible = true;
                        pnlNoNoticeNews.Visible = false;
                    }

                    DataTable dteventalert = new DataTable();
                    DataRow[] dreventalert = dt.Select("category_code in (1,2)");
                    if (dreventalert.Length > 0)
                    {
                        dteventalert = dreventalert.CopyToDataTable();
                    }
                    if (dteventalert.Rows.Count > 0)
                    {
                        rpteventalert.DataSource = dteventalert;
                        rpteventalert.DataBind();
                        mpEventAlert.Show();
                    }
                    DataTable dtservicealert = new DataTable();
                    DataRow[] drservicealert = dt.Select("category_code in (4,5)");
                    if (drservicealert.Length > 0)
                    {
                        dtservicealert = drservicealert.CopyToDataTable();
                    }
                    if (dtservicealert.Rows.Count > 0)
                    {
                        rptservicealert.DataSource = dtservicealert;
                        rptservicealert.DataBind();
                    }


                }
            }
        }
        catch (Exception ex)
        {
            lblDeptName.Text = ex.Message.ToString();
            _common.ErrorLog("Home-M2", ex.Message.ToString());
        }
    }
    protected void rpteventalert_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Image imgEventAlert = (Image)e.Item.FindControl("imgEventAlert");
            //Panel pnl = (Panel)e.Item.FindControl("pnlEventAlertcarousel");
            //if (e.Item.ItemIndex == 0)
            //{
            //    pnl.CssClass = "item active";
            //}
            DataRowView datar = (DataRowView)e.Item.DataItem;
            {
                byte[] bytes = (byte[])datar["image_1"];
                string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                imgEventAlert.ImageUrl = "data:image/jpg;base64," + base64String;
            }
        }
    }
    protected void rpteventalert_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "ViewDetails")
        {
            HiddenField hdnotice_id = (HiddenField)e.Item.FindControl("hdnotice_id");
            Response.Redirect("NoticNewsAllDetails.aspx?typeid=" + hdnotice_id.Value);
        }
    }
    protected void lvnews_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        if (String.Equals(e.CommandName, "VIEWDETAILS"))
        {
            ListViewDataItem dataItem = (ListViewDataItem)e.Item;
            string couponID = lvnews.DataKeys[dataItem.DataItemIndex]["notice_id"].ToString();
            Response.Redirect("NoticeNewsDetails.aspx?typeid=" + couponID);
        }
    }

    #endregion




    #region "Conductor Of The Month"
    private void loadCondictorMonth()
    {
        try
        {
            string month = DateTime.Now.AddMonths(-1).ToString("MMMM");
            string mon_no = DateTime.Now.AddMonths(-1).ToString("MM");
            string year = mon_no == "12" ? DateTime.Now.AddYears(-1).ToString("yyyy") : DateTime.Now.ToString("yyyy");
            lblConductorMonth.Text = month + ", " + year;
            DataTable dt = obj.getMonthofconductor(year, mon_no);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count == 1)
                {
                    lblConductorDepot.Text = ", Depot - " + dt.Rows[0]["officename"].ToString();
                    lblconductor.Text = dt.Rows[0]["emp_name"].ToString();
                    lblConductorDepot.Visible = true;
                    lblconductor.Visible = true;
                    lblNoconductormsg.Visible = false;

                }
                else
                {
                    lblConductorDepot.Text = ", Depot - " + dt.Rows[1]["officename"].ToString();
                    lblconductor.Text = dt.Rows[1]["emp_name"].ToString();
                    lblConductorDepot.Visible = true;
                    lblconductor.Visible = true;
                    lblNoconductormsg.Visible = false;
                }
            }
            else
            {
                lblNoconductormsg.Text = "Details will be available soon.";
                lblNoconductormsg.Visible = true;
                lblConductorDepot.Visible = false;
                lblconductor.Visible = false;
            }

        }
        catch (Exception ex)
        {

        }
    }


    #endregion



    #region "Bus Service Types"
    private void GetServiceType()
    {
        try
        {
            //divbusservices.Visible = false;
            //hrfBusServices.Visible = false;
            MyCommand = new NpgsqlCommand();
            DataTable dt = new DataTable();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_servicetype_getlist_forhome");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {

                    rptServiceType.DataSource = dt;
                    rptServiceType.DataBind();
                    //divbusservices.Visible = true;
                    //hrfBusServices.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            //divbusservices.Visible = false;
            //hrfBusServices.Visible = false;
        }
    }
    protected void rptServiceType_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Image imgServicetype = (Image)e.Item.FindControl("imgServicetype");
            HiddenField srtp = (HiddenField)e.Item.FindControl("srtpid");
            //Panel pnl = (Panel)e.Item.FindControl("pnl");
            //if (e.Item.ItemIndex == 0)
            //{
            //    pnl.CssClass = "carousel-item active";
            //}

            DataRowView datar = (DataRowView)e.Item.DataItem;
            {
                byte[] bytes = (byte[])datar["img_web"];
                string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                imgServicetype.ImageUrl = "data:image/jpg;base64," + base64String;
            }
        }
    }
    protected void rptServiceType_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "viewservice" || e.CommandName == "viewserviceRoutes" || e.CommandName == "viewserviceTimeTable")
        {
            HiddenField hdnsrtpid = (HiddenField)e.Item.FindControl("hdnsrtpid");
            Response.Redirect("Busservice.aspx?servicetypeid=" + hdnsrtpid.Value.ToString());
        }
    }
    #endregion

    #region "Our Mentors"
    protected void rptmentor_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Image imgmentor = (Image)e.Item.FindControl("imgmentor");
            HiddenField hdimage = (HiddenField)e.Item.FindControl("hdimage");
            imgmentor.ImageUrl = "HomeImage/mentors/" + hdimage.Value.ToString();
        }
    }
    #endregion

    #region "FAQ"
    private void loadFaqCategory(string v)
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
    #endregion

    #region "Photo Gallery"
    DataTable loadPhotographs(int categoryID)
    {

        string albumName;
        Int16 albumID;
        DataTable MyTable = new DataTable();
        MyTable.Columns.Add("photoId", typeof(int));
        MyTable.Columns.Add("photo_name", typeof(string));
        MyTable.Columns.Add("photoURL", typeof(string));

        XmlDocument xDoc1 = new XmlDocument();
        xDoc1.Load(Server.MapPath("CommonDataPhoto.xml"));
        //  XmlNode CategotyNode = xDoc1.SelectSingleNode("//Category[@Id='" + categoryID.ToString() + "']//Photo") as XmlNode;
        XmlNodeList nodes = xDoc1.SelectNodes("//Category[@Id='" + categoryID.ToString() + "']//Photo");
        foreach (XmlNode node in nodes)
        {
            var attribute1 = node.Attributes["Id"];
            var attribute2 = node.Attributes["title"];


            DataRow row = MyTable.NewRow();
            row["photoId"] = Int16.Parse(attribute1.Value.ToString());
            row["photo_name"] = attribute2.Value.ToString();
            row["photoURL"] = node.InnerText;
            MyTable.Rows.Add(row);


        }
        return MyTable;
    }
    void loadCategory()
    {
        string albumName;
        Int16 albumID;
        DataTable MyTableP = new DataTable();
        DataTable MyTable = new DataTable();
        MyTable.Columns.Add("categoryId", typeof(int));
        MyTable.Columns.Add("categoryName", typeof(string));
        //--------------------------------------------//
        XmlDocument xDoc1 = new XmlDocument();
        xDoc1.Load(Server.MapPath("CommonDataPhoto.xml"));
        //    XmlNode CategotyNode = xDoc1.SelectSingleNode("//Category[@Id='" + catgoryID.ToString() + "']//Photo[@Id='" + photoid.ToString() + "']") as XmlNode;
        XmlNodeList nodes = xDoc1.SelectNodes("//Category");
        foreach (XmlNode node in nodes)
        {
            albumName = node["NameEng"].InnerText;
            var attribute = node.Attributes["Id"];
            albumID = Int16.Parse(attribute.Value.ToString());

            DataRow row = MyTable.NewRow();
            row["categoryId"] = albumID;
            row["categoryName"] = albumName;
            MyTable.Rows.Add(row);
        }
        int cntPhoto = 0;

        if (MyTable.Rows.Count > 0)
        {

            for (int i = 0; i < MyTable.Rows.Count; i++)
            {
                int catgeoryid = int.Parse(MyTable.Rows[i]["categoryId"].ToString());
                MyTableP = loadPhotographs(catgeoryid);

                for (int j = 0; j < MyTableP.Rows.Count; j++)
                {
                    if (cntPhoto == 7)
                    {
                        break;
                    }
                    cntPhoto++;
                    try
                    {
                        Image imgphoto = (Image)Page.FindControl("imgPG" + cntPhoto.ToString());
                        Label lblPhoto = (Label)Page.FindControl("lblimgPG" + cntPhoto.ToString());
                        HyperLink hlinkPG = (HyperLink)Page.FindControl("hlinkPG" + cntPhoto.ToString());

                        imgphoto.ImageUrl = MyTableP.Rows[j]["photoURL"].ToString();
                        lblPhoto.Text = MyTableP.Rows[j]["photo_name"].ToString();
                        hlinkPG.NavigateUrl = MyTableP.Rows[j]["photoURL"].ToString();
                        // aImgPg1.
                    }
                    catch (Exception ex)
                    { return; }

                }
                if (cntPhoto > 7)
                {
                    break;
                }



            }
        }
    }

    protected void btnPhotoMore_Click(object sender, EventArgs e)
    {
        Response.Redirect("photogallery.aspx", false);
    }
    #endregion

    #region "Download APK"
    protected void appdownload_ServerClick(object sender, EventArgs e)
    {
        Downlaodfile();

    }
    private void Downlaodfile()
    {
        //File to be downloaded.
        string fileName = "StarbusAPSTS_traveller_Ver1.apk";

        //Path of the File to be downloaded.
        string filePath = Server.MapPath(string.Format("~/APK/{0}", fileName));

        //Content Type and Header.
        Response.ContentType = "application/apk";
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);

        //Writing the File to Response Stream.
        Response.WriteFile(filePath);

        //Flushing the Response.
        Response.Flush();
        Response.End();
    }
    #endregion

    private void loadGenralData()//M1
    {
        try
        {
            //////////// Additional Modules code
            
            ////////////
            int count = 0;

            sbXMLdata obj = new sbXMLdata();
            this.Title = obj.loadtitle();
            ImgDepartmentLogo.ImageUrl = "Logo/" + obj.loadDeptLogo();
            lblDeptName.Text = obj.loadDeptNameAbbr();
            lblversion.Text = obj.loadVersion();
            //lblemail.Text = obj.loadEmail();
            //lblcontact.Text = obj.loadContact();
            //lblHelpine.Text = obj.loadtollfree();
            //imghome1.ImageUrl = "HomeImage/" + obj.loadhomepage(1);
            //imghome2.ImageUrl = "HomeImage/" + obj.loadhomepage(2);
            //imghome3.ImageUrl = "HomeImage/" + obj.loadhomepage(3);


            if (obj.loadfacebooklink() != "0")
            {
                facebook.HRef = obj.loadfacebooklink();
                count = count + 1;
                facebook.Visible = true;
            }
            else
            {
                facebook.Visible = false;
            }
            if (obj.loadtwiterlink() != "0")
            {
                twitter.HRef = obj.loadtwiterlink();
                count = count + 1; ;
                twitter.Visible = true;
            }
            else
            {
                twitter.Visible = false;
            }
            if (obj.loadinstalink() != "0")
            {
                instagram.HRef = obj.loadinstalink();
                count = count + 1; ;
                instagram.Visible = true;
            }
            else
            {
                instagram.Visible = false;
            }
            if (obj.loadyoutubelink() != "0")
            {
                youtube.HRef = obj.loadyoutubelink();
                count = count + 1; ;
                youtube.Visible = true;
            }
            else
            {
                youtube.Visible = false;
            }
            //if (count > 0)
            //{
            //    dvsocial.Visible = true;
            //}
            //else
            //{
            //    dvsocial.Visible = false;
            //}

            DataTable dtmentors = new DataTable();
            dtmentors = obj.loadmentors();
            if (dtmentors.Rows.Count > 0)
            {
                dvmentor.Visible = true;
                rptmentor.DataSource = dtmentors;
                rptmentor.DataBind();
            }
            else
            {
                dvmentor.Visible = false;
            }


        }
        catch (Exception ex)
        {
            _common.ErrorLog("Home-M1", ex.Message.ToString());
            // Errormsg(ex.Message.ToString());
        }
    }
    private void loadofcwisecontact(int ofclvl)//M2
    {
        DataTable dt = new DataTable();
        dt = _common.getofficecontact(ofclvl);
        lbladdress.Text = "-NA-";

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            lbladdress.Text = dt.Rows[i]["officename"].ToString() + "<br>" + dt.Rows[i]["adrs"].ToString() + ", " + dt.Rows[i]["distname"].ToString() + ",<br>" + dt.Rows[i]["stname"].ToString();
            lblcontact.Text = dt.Rows[i]["mob"].ToString();
            lblemail.Text = dt.Rows[i]["eml"].ToString().Replace("@", "[at]").Replace(".", "[dot]");

        }

    }


    private void Errormsg(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "alert('" + msg + " ');", true);

        //string popup = _popup.modalPopupSmall("S", "Information", msg, "Close");
        //Response.Write(popup);
    }

}