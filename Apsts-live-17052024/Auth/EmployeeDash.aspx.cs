using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class Auth_EmployeeDash : System.Web.UI.Page
{
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbValidation _validation = new sbValidation();
    String msg = "";
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup sbb = new sbLoaderNdPopup();
    private Hashtable RestrictedHolidayList;
    private Hashtable GazitedHolidayList;
    private Hashtable SimpleHolidayList;
    private Hashtable NationalHoliday;
    protected void Page_Load(object sender, EventArgs e)
    {
       

        if (!IsPostBack)
        {
            if (_security.isSessionExist(Session["_UserCode"]) == true)
            {

            }
            else
            {
                Response.Redirect("sessionTimeout.aspx");
            }
            // Session["_UserCode"] = "UTC07192";
            Session["_type"] = "Birthday";
            Session["_category"] = "7";
            
            LoadAllEmployees(Session["_UserCode"].ToString());

            
            Session["Module"] = "Dashboard";
            Session["MasterPageHeaderText"] = "Employee Profile";
            loadofficelvl();
            loadXML();
            LoadBirthday();
            lbtnOrderCirculars(lbtnOrder);
            lbtnWishes(lbtnBirthday);
            loadXML();
            GetHolidays();
            months();
            years();
            LoadleaveRegister();
            LoaddutyRegister();
            LoadCurrentduty();

            getOrderDetails();
        }

    }
    
    #region "Method"
    public void months()
    {
        for (int month = 1; month <= 12; month++)
        {
            string monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            string months = "";
            if (month < 10)
            {
                months = ("00" + month.ToString()).Substring(1, 2);
            }
            else
            {
                months = month.ToString();
            }

            ddlLeaveRegisterMonth.Items.Add(new ListItem(monthName, months));
            ddldutyregistermonth.Items.Add(new ListItem(monthName, months));
        }
        string monthId = DateTime.Now.Date.ToString("MM");
        ddlLeaveRegisterMonth.SelectedValue = monthId.Trim();
        ddldutyregistermonth.SelectedValue = monthId.Trim();
    }
    public void years()
    {
        var currentYear = DateTime.Now.Year;
        for (int year = 2023; year <= currentYear; year++)
        {
            ddlLeaveRegisterYear.Items.Add(new ListItem(year.ToString(), year.ToString()));
            ddldutyregisteryear.Items.Add(new ListItem(year.ToString(), year.ToString()));
        }
        ddlLeaveRegisterYear.SelectedValue = currentYear.ToString();
        ddldutyregisteryear.SelectedValue = currentYear.ToString();
    }
    private void loadXML()
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(Server.MapPath("../CommonData.xml"));

        XmlNodeList contactNo1 = doc.GetElementsByTagName("contactNo1");
        lblcontact1.Text = contactNo1.Item(0).InnerXml;

        XmlNodeList email = doc.GetElementsByTagName("email");
        lblemail.Text = email.Item(0).InnerXml;
    }
    private void LoadAllEmployees(string empcode)//M12
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_emp");

            MyCommand.Parameters.AddWithValue("p_ofclvlid", 0);
            MyCommand.Parameters.AddWithValue("p_ofcid", "0");
            MyCommand.Parameters.AddWithValue("p_designation", 0);

            MyCommand.Parameters.AddWithValue("p_lincensestatus", "0");
            MyCommand.Parameters.AddWithValue("p_employeename", empcode);

            dt = bll.SelectAll(MyCommand);

            if (dt.Rows.Count > 0)
            {

                lblEmpName.Text = dt.Rows[0]["empname"].ToString();
                Session["_UserName"] = dt.Rows[0]["empname"].ToString();
                if (dt.Rows[0]["e_email_id"].ToString() == "")
                {
                    lblEmpEmail.Text = "N/A";
                }
                else
                {
                    lblEmpEmail.Text = dt.Rows[0]["e_email_id"].ToString();
                }
                
                lblEmpMobile.Text = dt.Rows[0]["e_mobile_number"].ToString();
                lblOfcLvlName.Text = dt.Rows[0]["e_ofclvlname"].ToString();
                Session["_OFCLVL"] = dt.Rows[0]["e_ofclvl_id"].ToString();
                lblOfcName.Text = dt.Rows[0]["e_office_name"].ToString();
                lblDesignationName.Text = dt.Rows[0]["e_designation_name"].ToString();
                lblRole.Text = dt.Rows[0]["rolename"].ToString();
                Session["_OFCID"] = dt.Rows[0]["e_officeid"].ToString();
                Session["_LDepotCode"] = dt.Rows[0]["e_officeid"].ToString();

              
                if ((int)dt.Rows[0]["role_code"] != 99)
                {
                    quicklinks.Visible = false;
                    // pnlLandingPage.Visible = true;
                    //Session["_LandingPage"] = dt.Rows[0]["LANDINGPAGE"];
                
                }
                else
                {
                    pnlNoLandingPage.Visible = true;
                    GetassignModule(Session["_OFCLVL"].ToString(), Session["_UserCode"].ToString());
                }

                if ((int)dt.Rows[0]["role_code"] == 2 || (int)dt.Rows[0]["role_code"] == 3)
                {
                    
                    dvDutyRegister.Visible = true;
                    dvcurrentduty.Visible = true;
                }
                else
                {
                    dvDutyRegister.Visible = false;
                    dvcurrentduty.Visible = false;
                }


                

                


            }

        }



        catch (Exception ex)
        {




        }
    }
    private void GetassignModule(string ofclvlid, string Userid)//M12
    {
        try
        {
            rptModules.Visible = false;
            pnlNoModules.Visible = true;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_assigned_module");

            MyCommand.Parameters.AddWithValue("p_ofclvlid", ofclvlid);
            MyCommand.Parameters.AddWithValue("p_userid", Userid);

            dt = bll.SelectAll(MyCommand);

            if (dt.Rows.Count > 0)
            {

                rptModules.DataSource = dt;
                rptModules.DataBind();
                rptModules.Visible = true;
                pnlNoModules.Visible = false;



            }

        }



        catch (Exception ex)
        {




        }
    }
    public void ErrorModalPopup(string header, string message)
    {
        LabelModalErrorHeader.ForeColor = Color.Red;
        LabelModalErrorHeader.Text = header;
        LabelModalErrorMessage.Text = message;
        ModalPopupError.Show();
    }
    public void SuccessModalPopup(string header, string message)
    {
        LabelModalErrorHeader.ForeColor = System.Drawing.Color.Green;
        LabelModalErrorHeader.Text = header;
        LabelModalErrorMessage.Text = message;
        ModalPopupError.Show();
    }
    private void getEmployeeDetail()//M12
    {
        try
        {
            DataTable dt = new DataTable();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_empdetails");
            MyCommand.Parameters.AddWithValue("p_search", txtsearch.Text);
            
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                gvEmployeeSearchData.DataSource = dt;
                gvEmployeeSearchData.DataBind();
                mpEmployeeSearch.Show();
            }
            else
            {
                ErrorModalPopup("No Data Found", "Please input correct details");
            }

        }



        catch (Exception ex)
        {




        }
    }
    private void lbtnWishes(LinkButton lbtn)
    {
        lbtnBirthday.CssClass = "nav-item nav-link";
        lbtnRetirement.CssClass = "nav-item nav-link";
        lbtnAnniversary.CssClass = "nav-item nav-link";
        lbtn.CssClass = "nav-item nav-link active";
    }
    private void GetHolidays()
    {
        try
        {
            GazitedHolidayList = new Hashtable();
            RestrictedHolidayList = new Hashtable();
            NationalHoliday = new Hashtable();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_getholiday");
            //MyCommand.Parameters.AddWithValue("p_office_level", Convert.ToInt32(Session["_OFCLVL"].ToString()));
            MyCommand.Parameters.AddWithValue("@p_ofclvl", Convert.ToInt32(Session["_OfficeLevel"].ToString()));
            MyCommand.Parameters.AddWithValue("@p_ofc", Convert.ToInt32(Session["_OfficeId"].ToString()));
            DataTable dt = new DataTable();
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow drIn in dt.Rows)
                {
                    if (drIn["type_"].ToString() == "R")
                    {
                        string date1 = drIn["date_"].ToString();
                        DateTime holidate = DateTime.ParseExact(date1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        string holidate2 = holidate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        RestrictedHolidayList.Add(holidate2, drIn["occassion_"]);
                    }
                    else if (drIn["type_"].ToString() == "G")
                    {
                        string date1 = drIn["date_"].ToString();
                        DateTime holidate = DateTime.ParseExact(date1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        string holidate2 = holidate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        GazitedHolidayList.Add(holidate2, drIn["occassion_"]);
                    }
                    else if (drIn["type_"].ToString() == "N")
                    {
                        string date1 = drIn["date_"].ToString();
                        DateTime holidate = DateTime.ParseExact(date1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        string holidate2 = holidate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        NationalHoliday.Add(holidate2, drIn["occassion_"]);
                    }
                }
            }

        }
        catch (Exception ex)
        {
            ErrorModalPopup("Warning", ex.Message);
        }
    }
    private void LoadBirthday()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_getbirthday");
            //MyCommand.Parameters.AddWithValue("p_type", Session["_type"]);
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {

                DataList1.DataSource = dt;
                DataList1.DataBind();
                lblWishes.Visible = false;
            }
            else
            {
                DataList1.DataSource = null;
                DataList1.DataBind();
                lblWishes.Visible = true;
                if(Session["_type"] == "Birthday")
                {
                    lblWishes.Text = "There is No Birthday For Wishes. Please Visit Tomorrow";
                }
                else if(Session["_type"] == "Anniversary")
                {
                    lblWishes.Text = "There is No Anniversary For Wishes. Please Visit Tomorrow";
                }
                else if(Session["_type"] == "Retirement")
                {
                    lblWishes.Text = "There is No Retirement For Wishes. Please Visit Tomorrow";
                }
            }
        }
        catch (Exception ex)
        {
            ErrorModalPopup("Warning", ex.Message);
        }
    }
    private void LoadleaveRegister()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_getleaveregister");
            //MyCommand.Parameters.AddWithValue("p_empcode", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("p_empcode", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("p_year", ddlLeaveRegisterYear.SelectedValue);
            MyCommand.Parameters.AddWithValue("p_month", ddlLeaveRegisterMonth.SelectedValue);
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                grvleaveregister.DataSource = dt;
                grvleaveregister.DataBind();
                lblLeaveRegisterMsg.Visible = false;
            }
            else
            {
              
                grvleaveregister.DataSource = null;
                grvleaveregister.DataBind();
                lblLeaveRegisterMsg.Visible = true;
            }
        }
        catch (Exception ex)
        {
            ErrorModalPopup("Warning", ex.Message);
        }
    }

    private void LoadCurrentduty()
    {
        try
        {

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_current_duty");
            MyCommand.Parameters.AddWithValue("p_empcode", Session["_UserCode"].ToString());
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                gvCurrentDuty.DataSource = dt;
                gvCurrentDuty.DataBind();

                lblcurrentdutymsg.Visible = false;
            }
            else
            {

                gvCurrentDuty.DataSource = null;
                gvCurrentDuty.DataBind();
                lblcurrentdutymsg.Visible = true;
            }
        }
        catch (Exception ex)
        {
            ErrorModalPopup("Warning", ex.Message);
        }
    }
    private void LoaddutyRegister()
    {
        try
        {
           
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_getdutyregister");
            MyCommand.Parameters.AddWithValue("p_empcode", Session["_UserCode"].ToString());
            //MyCommand.Parameters.AddWithValue("p_empcode", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("p_year", ddldutyregisteryear.SelectedValue);
            MyCommand.Parameters.AddWithValue("p_month", ddldutyregistermonth.SelectedValue);
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                GVDutyRegister.DataSource = dt;
                GVDutyRegister.DataBind();
                
                lblDutyShiftRegistermsg.Visible = false;
            }
            else
            {
                
                GVDutyRegister.DataSource = null;
                GVDutyRegister.DataBind();
                lblDutyShiftRegistermsg.Visible = true;
            }
        }
        catch (Exception ex)
        {
            ErrorModalPopup("Warning", ex.Message);
        }
    }
    private void loadofficelvl()
    {
        try
        {
            ddlOffce.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_officelvl");
            MyCommand.Parameters.AddWithValue("@emplevel", Convert.ToInt32(Session["_OFCLVL"]));
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlOffce.DataSource = dt;
                    ddlOffce.DataTextField = "ofclvl_name";
                    ddlOffce.DataValueField = "ofclvl_id";
                    ddlOffce.DataBind();
                }
            }
            else
            {
                _common.ErrorLog("EmployeeDash-M4", dt.TableName);
            }

            ddlOffce.Items.Insert(0, "All");
            ddlOffce.Items[0].Value = "0";
            ddlOffce.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlOffce.Items.Insert(0, "SELECT");
            ddlOffce.Items[0].Value = "0";
            ddlOffce.SelectedIndex = 0;
            _common.ErrorLog("EmployeeDash-M4", ex.Message.ToString());
        }
    }
    private bool IsValidAgValueSearch()
    {
        if (!string.IsNullOrEmpty(txtsearch.Text))
        {
            if (!_validation.IsValidString(txtsearch.Text.Trim(), 1, 50))
            {
                ErrorModalPopup("Warning", msg);
                return false;
            }
        }
        return true;
    }
    private void lbtnOrderCirculars(LinkButton btn)
    {
        lbtnOrder.CssClass = "nav-item nav-link";
        lbtnCircular.CssClass = "nav-item nav-link";
        btn.CssClass = "nav-item nav-link active";
    }
    private void getOrderDetails()//M10
    {
        try
        {
            gvDraftData.Visible = false;
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_getordercircular");
            MyCommand.Parameters.AddWithValue("@p_category", Convert.ToInt16(Session["_category"]));
            DataTable MyTable = bll.SelectAll(MyCommand);
            if (MyTable.TableName == "Success")
            {
                if (MyTable.Rows.Count > 0)
                {
                    gvDraftData.DataSource = MyTable;
                    gvDraftData.DataBind();
                    gvDraftData.Visible = true;
                    lblCircularmsg.Visible = false;
                }

                else
                {
                    if (Session["_category"].ToString() == "7")
                    {
                         lblCircularmsg.Visible = false;
                        lblOrderMsg.Visible = true;

                    }

                    if (Session["_category"].ToString() == "8")
                    {
                        lblOrderMsg.Visible = false;
                        lblCircularmsg.Visible = true;

                    }
                }

            }
            else
            {
                

            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminAlertNoticePublishing.aspx-0015", ex.Message.ToString());

        }
    }
    #endregion
    
    #region "Event"
    protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
    {
        GetHolidays();

        if (GazitedHolidayList[e.Day.Date.ToString("dd/MM/yyyy")] != null)
        {
            Literal literal1 = new Literal();
            literal1.Text = "<br/>";
            e.Cell.Controls.Add(literal1);

            Label label1 = new Label();
            Label label2 = new Label();
            label1.Text = Convert.ToString(GazitedHolidayList[e.Day.Date.ToString("dd/MM/yyyy")]);
            label1.Font.Size = FontUnit.Small;
            e.Cell.Controls.Add(label2);

            e.Cell.BackColor = System.Drawing.Color.Green;
            e.Cell.ForeColor = System.Drawing.Color.White;
        }

        if (RestrictedHolidayList[e.Day.Date.ToString("dd/MM/yyyy")] != null)
        {
            Literal literal1 = new Literal();
            literal1.Text = "<br/>";
            e.Cell.Controls.Add(literal1);

            Label label1 = new Label();
            Label label2 = new Label();
            label1.Text = Convert.ToString(RestrictedHolidayList[e.Day.Date.ToString("dd/MM/yyyy")]);
            label1.Font.Size = FontUnit.Small;
            e.Cell.Controls.Add(label2);

            e.Cell.BackColor = System.Drawing.Color.DarkOrange;
            e.Cell.ForeColor = System.Drawing.Color.White;
        }

        if (NationalHoliday[e.Day.Date.ToString("dd/MM/yyyy")] != null)
        {
            Literal literal1 = new Literal();
            literal1.Text = "<br/>";
            e.Cell.Controls.Add(literal1);

            Label label1 = new Label();
            Label label2 = new Label();
            label1.Text = Convert.ToString(NationalHoliday[e.Day.Date.ToString("dd/MM/yyyy")]);
            label1.Font.Size = FontUnit.Small;
            e.Cell.Controls.Add(label2);

            e.Cell.BackColor = System.Drawing.Color.FromArgb(124, 252, 0);
            e.Cell.ForeColor = System.Drawing.Color.White;
        }

        if (e.Day.Date.DayOfWeek == DayOfWeek.Sunday)
        {
            e.Cell.BackColor = System.Drawing.Color.FromArgb(204, 51, 51);
            e.Cell.ForeColor = System.Drawing.Color.White;
        }

        if (e.Day.IsOtherMonth)
        {
            e.Cell.Controls.Clear();
            e.Cell.BackColor = System.Drawing.Color.White;
        }
    }
    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {
        try
        {
            string dt1 = Calendar1.SelectedDate.ToString("dd/MM/yyyy");

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_getholidaydetails");
            MyCommand.Parameters.AddWithValue("p_date", dt1);
           
            DataTable dt = new DataTable();
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {

                    lblHolidayDate.Text = dt.Rows[0]["date_"].ToString();
                    lblOccassion.Text = dt.Rows[0]["occassion_"].ToString();
                    lblDescription.Text = dt.Rows[0]["description_"].ToString();
                    lblholidaytype.Text = dt.Rows[0]["holiday_type"].ToString();
                    mpHolidayInfo.Show();
                }
                else
                {
                    lblHolidayDate.Text = dt1;
                    lblOccassion.Text = "N/A";
                    lblDescription.Text = "N/A";
                    lblholidaytype.Text = "N/A";
                    mpHolidayInfo.Show();
                }
            
        }
        catch (Exception ex)
        {
            ErrorModalPopup("Warning", ex.Message);
        }
    }
    protected void lbtnSearchemp_Click(object sender, EventArgs e)
    {
        if (!IsValidAgValueSearch())
        {
            ErrorModalPopup("Warning", msg);
            return;
        }
        getEmployeeDetail();
    }  
    protected void gvEmployeeSearchData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvEmployeeSearchData.PageIndex = e.NewPageIndex;
        getEmployeeDetail();
    }
    protected void rptModules_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Redirect")
        {
            string _Purl = e.CommandArgument.ToString();
            Response.Redirect(_Purl);
        }


    }
    protected void lbtnBirthday_Click(object sender, EventArgs e)
    {
        lbtnWishes(lbtnBirthday);
        Session["_type"] = "Birthday";
        LoadBirthday();
    }
    protected void lbtnAnniversary_Click(object sender, EventArgs e)
    {
        lbtnWishes(lbtnAnniversary);
        Session["_type"] = "Anniversary";
        LoadBirthday();
    }
    protected void lbtnRetirement_Click(object sender, EventArgs e)
    {
        lbtnWishes(lbtnRetirement);
        Session["_type"] = "Retirement";
        LoadBirthday();

    }
    protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "SendWishes")
            {
                Label mobile = (Label)DataList1.Items[e.Item.ItemIndex].FindControl("lblbirthdayMobile");
                Label email = (Label)DataList1.Items[e.Item.ItemIndex].FindControl("lblbirthdayEmail");
                Label name = (Label)DataList1.Items[e.Item.ItemIndex].FindControl("lblbirthdayName");
                lblwishingtype.Text = "Send Birthday Wishes" + " " + "to" + " " + name.Text;
                Session["_BdayMobile"] = mobile.Text; // mobile.Text;
                Session["_BdayName"] = name.Text;
                Session["WishingType"] = "Birthday";
                txtWishes.Text = "";
                mpBirthday.Show();
            }
        }
        catch (Exception ex)
        {
            ErrorModalPopup("Warning", ex.Message);
        }

    }
    protected void lbtninfo_Click(object sender, EventArgs e)
    {
        mpInfo.Show();
    }
    protected void Unnamed_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.ContentType = "application/pdf";
        Response.AppendHeader("Content-Disposition", "attachment; filename=Sample.pdf");
        Response.TransmitFile("../File/blank.pdf");
        Response.End();
    }
    protected void Unnamed_Click1(object sender, EventArgs e)
    {
        Response.Clear();
        Response.ContentType = "application/pdf";
        Response.AppendHeader("Content-Disposition", "attachment; filename=Sample.pdf");
        Response.TransmitFile("../File/blank.pdf");
        Response.End();
    }
    protected void lbtnOrder_Click(object sender, EventArgs e)
    {
        lbtnOrderCirculars(lbtnOrder);
        Session["_category"] = "7";
        getOrderDetails();
    }
    protected void lbtnCircular_Click(object sender, EventArgs e)
    {
        lbtnOrderCirculars(lbtnCircular);
        Session["_category"] = "8";
        getOrderDetails();
    }
    protected void grvleaveregister_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvleaveregister.PageIndex = e.NewPageIndex;
        LoadleaveRegister();
    }
    protected void ddlLeaveRegisterMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadleaveRegister();
    }
    protected void ddlLeaveRegisterYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadleaveRegister();
    }
    protected void GVDutyRegister_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVDutyRegister.PageIndex = e.NewPageIndex;
        LoaddutyRegister();
    }
    protected void ddldutyregisteryear_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoaddutyRegister();
    }
    protected void ddldutyregistermonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoaddutyRegister();
    }
    protected void gvDraftData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        byte[] FileInvoice;
        string base64String = "";
        if (e.CommandName == "View")
        {
            if (Convert.IsDBNull(gvDraftData.DataKeys[index].Values["documt"]) == true)
            {
                ErrorModalPopup("No Data Found", "File not found");
                return;
            }

            FileInvoice = (byte[])gvDraftData.DataKeys[index].Values["documt"];
            base64String = Convert.ToBase64String(FileInvoice, 0, FileInvoice.Length);
            Session["Base64String"] = base64String;

            openpage("../Pass/ViewDocument.aspx");
        }
    }
    private void openpage(string PageName)
    {
        tkt.Src = PageName;
        mpdocment.Show();
    }
    protected void lbtnsendWishes_Click(object sender, EventArgs e)
    {
        try
        {
            string text = Session["_BdayName"] + ", " + txtWishes.Text;
            string senderUser = Session["_UserCode"] + " - " + Session["_UserName"];
            // comnWhatsapp.sendBirthDayWishSms(Session["_BdayMobile"], text, senderUser);
            SuccessModalPopup("Confirmation", "Wishes Send Successfully");

            if (Session["WishingType"].ToString() == "Anniversary")
            {
                // Handle Anniversary logic here
            }

            if (Session["WishingType"].ToString() == "Retirement")
            {
                // Handle Retirement logic here
            }
        }
        catch (Exception ex)
        {
            ErrorModalPopup("Warning", ex.Message);
        }
    }
    protected void GVDutyRegister_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "viewDuty")
        {
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            Session["dutySlipRefno"] = GVDutyRegister.DataKeys[RowIndex].Values["waybillrefno_"];
            Session["_LDepotCode"] = GVDutyRegister.DataKeys[RowIndex].Values["office_id"];
            //openSubDetailsWindow("DutySlip.aspx");
            lblMpHeader.Text = "Duty Slip";
            eSlip.Text = "<embed src = \"DutySlip.aspx\" style=\"height: 80vh; width: 100%\" />";
            mpShowDuty.Show();
        }
        if (e.CommandName == "ExcessAmt")
        {
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            Session["dutySlipRefno"] = GVDutyRegister.DataKeys[RowIndex].Values["waybillrefno_"];
            Session["_LDepotCode"] = GVDutyRegister.DataKeys[RowIndex].Values["office_id"];
            //openSubDetailsWindow("DutySlip.aspx");
            lblMpHeader.Text = "Waybill Excess Payment Amount";
            eSlip.Text = "<embed src = \"dashwaybillexcessamount.aspx\" style=\"min-height: 70vh; width: 100%\" />";
            mpShowDuty.Show();
        }
    }
    protected void GVDutyRegister_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void gvCurrentDuty_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "viewCDuty")
        {
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            Session["dutySlipRefno"] = gvCurrentDuty.DataKeys[RowIndex].Values["waybillrefno_"];
            Session["_LDepotCode"] = gvCurrentDuty.DataKeys[RowIndex].Values["office_id"];
            //openSubDetailsWindow("DutySlip.aspx");
            lblMpHeader.Text = "Duty Slip";
            eSlip.Text = "<embed src = \"DutySlip.aspx\" style=\"height: 80vh; width: 100%\" />";
            mpShowDuty.Show();
        }
        if (e.CommandName == "ExcessAmt")
        {
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            Session["dutySlipRefno"] = gvCurrentDuty.DataKeys[RowIndex].Values["waybillrefno_"];
            Session["_LDepotCode"] = gvCurrentDuty.DataKeys[RowIndex].Values["office_id"];
            //openSubDetailsWindow("DutySlip.aspx");
            lblMpHeader.Text = "Waybill Excess Payment Amount";
            eSlip.Text = "<embed src = \"dashwaybillexcessamount.aspx\" style=\"min-height: 70vh; width: 100%\" />";
            mpShowDuty.Show();
        }
    }
    #endregion
    
}