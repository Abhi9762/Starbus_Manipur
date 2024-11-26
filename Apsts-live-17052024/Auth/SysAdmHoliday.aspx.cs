using System;
using Npgsql;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Drawing;
using System.Collections;

public partial class Auth_SysAdmHoliday : System.Web.UI.Page
{
    sbValidation _validation = new sbValidation();
    DataTable dt = new DataTable();
    private NpgsqlCommand MyCommand;
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    DataTable MyTable = new DataTable();
    Hashtable RestrictedHolidayList;
    Hashtable GazitedHolidayList;
    Hashtable NationalHoliday;
    string msg = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["_moduleName"] = "Add Holiday";
        if (!IsPostBack)
        {
            Random randomclass = new Random();
            Session["rndNoCheck"] = randomclass.Next().ToString();
            
            if (Session["_RoleCode"].ToString() == "1")
            {
                string user_id;
                user_id = Session["_UserCode"].ToString();
                loadofficelvl(ddlOfficeLevel);
                loadofc(ddlOfficeLevel.SelectedValue, ddlOfficeType);
                month_bind();
                Year_bind();
                Getholiday();
            }
            if (Session["_RoleCode"].ToString() == "5")
            {
                string user_id;
                user_id = Session["_UserCode"].ToString();
                loadofficelvl(ddlOfficeLevel);
                ddlOfficeLevel.SelectedValue = "30";
                ddlOfficeLevel.Enabled = false;
                loadofc(ddlOfficeLevel.SelectedValue, ddlOfficeType);
                ddlOfficeType.SelectedValue = Session["_LDepotCode"].ToString();
                ddlOfficeType.Enabled = false;
                month_bind();
                Year_bind();
                Getholiday();
            }
        }

        Calendar1.FirstDayOfWeek = FirstDayOfWeek.Sunday;
        Calendar1.NextPrevFormat = NextPrevFormat.CustomText;
        Calendar1.TitleFormat = TitleFormat.MonthYear;
        Calendar1.TitleStyle.Height = new Unit(90);
        Calendar1.ShowGridLines = true;
        Calendar1.DayStyle.Height = new Unit(55);
        Calendar1.DayStyle.Width = new Unit(140);
        Calendar1.SelectorStyle.Height = new Unit(60);
        Calendar1.DayStyle.HorizontalAlign = HorizontalAlign.Center;
        Calendar1.DayStyle.VerticalAlign = VerticalAlign.Middle;
        Calendar1.OtherMonthDayStyle.BackColor = System.Drawing.Color.Violet;
    }

    #region "Method"
    private void loadofficelvl(DropDownList ddllofficeLevel)//M4
    {
        try
        {
            ddllofficeLevel.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_officelvl");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddllofficeLevel.DataSource = dt;
                    ddllofficeLevel.DataTextField = "ofclvl_name";
                    ddllofficeLevel.DataValueField = "ofclvl_id";
                    ddllofficeLevel.DataBind();
                }
            }
            else
            {
                _common.ErrorLog("SysadmEmployeeDetails.aspx-0007", dt.TableName);
            }

            ddllofficeLevel.Items.Insert(0, "All");
            ddllofficeLevel.Items[0].Value = "0";
            ddllofficeLevel.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddllofficeLevel.Items.Insert(0, "SELECT");
            ddllofficeLevel.Items[0].Value = "0";
            ddllofficeLevel.SelectedIndex = 0;
           
        }
    }
    private void loadofc(string ofclvlid, DropDownList ddlOffice)//M5
    {
        try
        {
            ddlOffice.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_ofclvlwiseoffice");
            MyCommand.Parameters.AddWithValue("p_ofclvlid", Convert.ToInt32(ofclvlid));
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlOffice.DataSource = dt;
                    ddlOffice.DataTextField = "officename";
                    ddlOffice.DataValueField = "officeid";
                    ddlOffice.DataBind();
                }
            }
            else
            {
                _common.ErrorLog("SysadmEmployeeDetails.aspx-0009", dt.TableName);
            }

            ddlOffice.Items.Insert(0, "All");
            ddlOffice.Items[0].Value = "0";
            ddlOffice.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlOffice.Items.Insert(0, "SELECT");
            ddlOffice.Items[0].Value = "0";
            ddlOffice.SelectedIndex = 0;
           
        }
    }
    private void month_bind()
    {
        for (int month = 1; month <= 12; month++)
        {
            string monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            ddlMonthNames.Items.Add(new ListItem(monthName, month.ToString().PadLeft(2, '0')));
            ddlMonthNames.SelectedIndex = DateTime.Now.Month - 1;
        }
    }
    public void Year_bind()
    {
        try
        {
            for (int i = 2024; i <= 2030; i++)
            {
                ddlYear.Items.Add(i.ToString());
                ddlYear.SelectedValue = DateTime.Now.Year.ToString();
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    public void ErrorMessage(string message)
    {
        lblerrormsg.Text = message;
        mperror.Show();
    }
    public void SuccessMessage(string message)
    {
        lblsuccessmsg.Text = message;
        mpsuccess.Show();
    }
    private bool IsValidValue()
    {
        string msg = "";
        int msgcount = 0;


        if (ddl_Holidaytype.SelectedIndex <= 0)
        {
            msgcount++;
            msg += msgcount.ToString() + ". Holiday Type <br/>";
        }
        if (!_validation.IsValidString(txt_holidayName.Text.Trim(), 1, 50))
        {
            msgcount++;
            msg += msgcount.ToString() + ". Occassion Name <br/>";
        }

        if (!_validation.IsValidString(txtdesc.Text.Trim(), 1, 50))
        {
            msgcount++;
            msg += msgcount.ToString() + ". Description Name<br/>";
        }

        if (msgcount > 0)
        {
            ErrorMessage(msg);
            return false;
        }

        return true;
    }
    private void Getholiday()
    {

        try
        {
            string user_id;
            user_id = Session["_UserCode"].ToString();
            GazitedHolidayList = new Hashtable();
            RestrictedHolidayList = new Hashtable();
            NationalHoliday = new Hashtable();
            string yrr;
            if (ddlYear.SelectedIndex == 0)
            {
                yrr = DateTime.Now.Year.ToString();
            }
            else
            {
                yrr = ddlYear.SelectedValue; // "2023";// ddlYear.SelectedItem.Text;
            }
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_getholiday");
            MyCommand.Parameters.AddWithValue("@p_ofclvl", Convert.ToInt32(ddlOfficeLevel.SelectedValue));
            MyCommand.Parameters.AddWithValue("@p_ofc", Convert.ToInt32(ddlOfficeType.SelectedValue));
            DataTable dt = new DataTable();
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                //string GZ = MyTable.Rows[0]["p_holiday_type"].ToString();

                foreach (DataRow drIn in dt.Rows)
                {
                    if (drIn["type_"].ToString() == "R")
                    {
                        string Date1 = Convert.ToDateTime(drIn["holidaydate_"]).ToString("dd/MM/yyyy");
                        RestrictedHolidayList.Add(Date1, drIn["occassion_"]);
                    }
                    else if (drIn["type_"].ToString() == "G")
                    {
                        string Date1 = Convert.ToDateTime(drIn["holidaydate_"]).ToString("dd/MM/yyyy");
                        GazitedHolidayList.Add(Date1, drIn["occassion_"]);
                    }
                    else if (drIn["type_"].ToString() == "N")
                    {
                        string Date1 = Convert.ToDateTime(drIn["holidaydate_"]).ToString("dd/MM/yyyy");
                        NationalHoliday.Add(Date1, drIn["occassion_"]);
                    }

                }
            }


        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }

    }
    #endregion

    #region "Event"


    protected void ddlOfficeLevel_SelectedIndexChanged(object sender, EventArgs e)
    {     
        loadofc(ddlOfficeLevel.SelectedValue, ddlOfficeType);
        Getholiday();

    }
  
    protected void ddlMonthSelectedIndexChangrd(object sender, EventArgs e)
    {
        SetCalendarDate();
        //DateTime dt = new DateTime(Convert.ToInt16(ddlYear.SelectedItem.Text), Convert.ToInt16(ddlMonthNames.SelectedItem.Value), 1);
        //Calendar1.VisibleDate = dt;

    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetCalendarDate();
    }
    void SetCalendarDate()
    {
        DateTime dt = new DateTime(Convert.ToInt16(ddlYear.SelectedItem.Text), Convert.ToInt16(ddlMonthNames.SelectedItem.Value), 1);
        Calendar1.VisibleDate = dt;
    }
    protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
    {

        try
        {
            Getholiday();
            if (e.Day.Date.CompareTo(DateTime.Today) < 0)
            {
                e.Day.IsSelectable = false;
                //e.Cell.BackColor = System.Drawing.Color.LightGray;
                //e.Cell.ForeColor = System.Drawing.Color.White;
            }
            if (GazitedHolidayList[e.Day.Date.ToString("dd/MM/yyyy")] != null)
            {
                //lbtnDelete.Visible = false;
                Literal literal1 = new Literal();
                literal1.Text = "<br/>";
                e.Cell.Controls.Add(literal1);
                Label label1 = new Label();
                // label1.Text = HttpUtility.HtmlEncode((string)GazitedHolidayList[e.Day.Date.ToShortDateString()]);
                label1.Text = HttpUtility.HtmlEncode((string)GazitedHolidayList[e.Day.Date.ToString("dd/MM/yyyy")]);
                label1.Font.Size = new FontUnit(FontSize.Small);
                e.Cell.Controls.Add(label1);
                e.Cell.BackColor = System.Drawing.Color.Red;
                e.Cell.ForeColor = System.Drawing.Color.White;

            }
            if (RestrictedHolidayList[e.Day.Date.ToString("dd/MM/yyyy")] != null)
            {
                Literal literal1 = new Literal();
                literal1.Text = "<br/>";
                e.Cell.Controls.Add(literal1);
                Label label1 = new Label();
                label1.Text = HttpUtility.HtmlEncode((string)RestrictedHolidayList[e.Day.Date.ToString("dd/MM/yyyy")]);
                label1.Font.Size = new FontUnit(FontSize.Small);
                e.Cell.Controls.Add(label1);
                e.Cell.BackColor = System.Drawing.Color.Green;
                e.Cell.ForeColor = System.Drawing.Color.White;
            }
            if (NationalHoliday[e.Day.Date.ToString("dd/MM/yyyy")] != null)
            {
                Literal literal1 = new Literal();
                literal1.Text = "<br/>";
                e.Cell.Controls.Add(literal1);
                Label label1 = new Label();
                label1.Text = HttpUtility.HtmlEncode((string)NationalHoliday[e.Day.Date.ToString("dd/MM/yyyy")]);
                label1.Font.Size = new FontUnit(FontSize.Small);
                e.Cell.Controls.Add(label1);
                e.Cell.BackColor = System.Drawing.Color.FromArgb(124, 252, 0);
                e.Cell.ForeColor = System.Drawing.Color.White;
            }
            
            if (e.Day.Date.DayOfWeek == DayOfWeek.Saturday || e.Day.Date.DayOfWeek == DayOfWeek.Sunday)
            {
                e.Cell.BackColor = System.Drawing.Color.FromArgb(204, 51, 51);
                e.Cell.ForeColor = System.Drawing.Color.White;
                e.Day.IsSelectable = false;
                e.Cell.Enabled = false;
            }
            if (e.Day.IsOtherMonth)
            {
                //lbtnDelete.Visible = false;
                e.Cell.Controls.Clear();
                e.Cell.BackColor = System.Drawing.Color.White;

            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    protected void Calendar1_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
    {
        
    }
    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
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
            lbtnReset.Visible = false;
            txtDate.Enabled = false;
            txt_holidayName.Enabled = false;
            lbtnSaveHoliday.Visible = false;
            txt_holidayName.Text = dt.Rows[0]["occassion_"].ToString();
            txtdesc.Text = dt.Rows[0]["description_"].ToString();
            ddl_Holidaytype.SelectedValue = dt.Rows[0]["type_"].ToString();
            txtDate.Text = Calendar1.SelectedDate.ToShortDateString();
            txtHolidayDay.Text = Calendar1.SelectedDate.DayOfWeek.ToString();
            mpaddHoliday.Show();

        }
        else
        {
            txtDate.Text = Calendar1.SelectedDate.ToShortDateString();
            txtHolidayDay.Text = Calendar1.SelectedDate.DayOfWeek.ToString();      
            txtdesc.Text = "";
            txtdesc.Enabled = true;
            txt_holidayName.Text = "";
            txt_holidayName.Enabled = true;
            lbtnReset.Visible = false;
            lbtnSaveHoliday.Visible = true;
            lbtnReset.Visible = true;
            mpaddHoliday.Show();
        }
    }
    protected void lbtnSaveHoliday_Click(object sender, EventArgs e)
    {
        try
        {
            if (IsValidValue() == false)
            {
                return;
            }

            lblConfirmation.Text = "Do you want to Register Holiday ?";
            mpConfirmation.Show();
        }
        catch (Exception ex)
        {
            ErrorMessage("Error, Something went wrong errorcode Save 1" + ex.Message);
            return;
        }

    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        string user_id;
        user_id = Session["_UserCode"].ToString();
        string result = "";

        try
        {
            DateTime p_holiday_date = Convert.ToDateTime(txtDate.Text);
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_insert_holiday");
            MyCommand.Parameters.AddWithValue("p_holiday_date", p_holiday_date);
            MyCommand.Parameters.AddWithValue("p_occassion", txt_holidayName.Text);
            MyCommand.Parameters.AddWithValue("p_day", txtHolidayDay.Text);
            MyCommand.Parameters.AddWithValue("p_holiday_type", ddl_Holidaytype.SelectedItem.Value);
            MyCommand.Parameters.AddWithValue("p_description", txtdesc.Text);
            MyCommand.Parameters.AddWithValue("p_update_by", user_id);
            MyCommand.Parameters.AddWithValue("p_ofclvl_id", Convert.ToInt32(ddlOfficeLevel.SelectedValue));
            MyCommand.Parameters.AddWithValue("p_ofc_id", Convert.ToInt32(ddlOfficeType.SelectedValue));

            result = bll.InsertAll(MyCommand);
            if (result == "Success")
            {
                msg = "Holiday Add Successfully";
                SuccessMessage(msg);
                mpaddHoliday.Hide();
            }
            else
            {
                msg = "Something went wrong please try again";
                ErrorMessage(msg);
            }
        }
        catch (Exception ex)
        {

        }

    }
    protected void lbtnReset_Click(object sender, EventArgs e)
    {
        txt_holidayName.Text = "";
        txtdesc.Text = "";
    }

    

    protected void ddlOfficeType_SelectedIndexChanged(object sender, EventArgs e)
    {
        Getholiday();
    }

    #endregion
}