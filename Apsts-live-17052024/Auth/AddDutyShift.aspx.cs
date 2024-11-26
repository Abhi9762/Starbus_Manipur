using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_AddDutyShift : System.Web.UI.Page
{
    sbValidation _validation = new sbValidation();
    DataTable dt = new DataTable();
    private NpgsqlCommand MyCommand;
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    DataTable MyTable = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["_moduleName"] = "Shift Allocation";
        if (!IsPostBack)
        {
            lbtnSaveNProceed.Visible = true;
            loadmonth();
            loadyear();
            ddlYear.SelectedValue = DateTime.Now.ToString("yyyy");
            ddlMonth.SelectedValue = DateTime.Now.ToString("MM");
            Session["Year"] = ddlYear.SelectedValue;
            Session["Month"] = ddlMonth.SelectedValue;
            LoadEmployee(Session["_OfficeLevel"].ToString(), Session["_OfficeId"].ToString(), Session["Year"].ToString(), Session["Month"].ToString());
            LoadEmployeeCount(Session["_OfficeLevel"].ToString(), Session["_OfficeId"].ToString(), Session["Year"].ToString(), Session["Month"].ToString());
            if (CheckShiftAllocation(Session["_OfficeLevel"].ToString(), Session["_OfficeId"].ToString(), Session["Year"].ToString(), Session["Month"].ToString()))
            {
                Errormsg("Shift Allocation not allowed for selected month, please select next month");
                lbtnSaveNProceed.Visible = false;
                return;
            }

        }
        Test();
    }

    #region "Method"
    private void loadmonth()
    {
        for (int month = 1; month <= 12; month++)
        {
            string monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            ddlMonth.Items.Add(new ListItem(monthName, month.ToString().PadLeft(2, '0')));
            ddlMonth.SelectedIndex = DateTime.Now.Month - 1;
        }
    }
    public void loadyear()
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
    private void LoadEmployee(string OfcLvl, string Ofc, string year, string month)
    {
        try
        {
            lblmsg.Visible = false;
            grdemployee.Visible = false;
            lbtnSaveNProceed.Visible = false;
            lblNoEmp.Visible = true;

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_getshiftemployeelist");
            MyCommand.Parameters.AddWithValue("@p_ofclvl", Convert.ToInt32(OfcLvl));
            MyCommand.Parameters.AddWithValue("@p_ofc", Ofc);
            MyCommand.Parameters.AddWithValue("@p_year", year);
            MyCommand.Parameters.AddWithValue("@p_month", month);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {

                    lblmsg.Visible = true;
                    lblNoEmp.Visible = false;
                    grdemployee.Visible = true;
                    //lbtnSaveNProceed.Visible = true;
                    if (Session["Count"].ToString() == "Y")
                    {
                        lbtnSaveNProceed.Visible = false;
                    }
                    else
                    {
                        lbtnSaveNProceed.Visible = true;
                    }

                    grdemployee.DataSource = dt;
                    grdemployee.DataBind();
                    Test();
                }
            }
        }
        catch (Exception ex)
        {
            grdemployee.Visible = false;
            lbtnSaveNProceed.Visible = false;
            lblNoEmp.Visible = true;
            return;
        }
    }
    private void Test()
    {
        if (grdemployee.Rows.Count > 0)
        {
            grdemployee.UseAccessibleHeader = true;
            grdemployee.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
    private void CheckDays(string year, string month)
    {
        lbtnSaveNProceed.Visible = true;
        Session["Count"] = "N";
        string mm = month;
        string current_date = DateTime.Now.ToString("dd/MM/yyyy");
        string monthfirstday = "01/" + mm + "/" + year;
        DateTime dateOne; 
        DateTime.TryParseExact(current_date.Trim(), "dd/MM/yyyy", null, DateTimeStyles.None, out dateOne);
        // DateTime.Parse(current_date);
        DateTime dateTwo;
        DateTime.TryParseExact(monthfirstday.Trim(), "dd/MM/yyyy", null, DateTimeStyles.None, out dateTwo);
        //DateTime.Parse(monthfirstday);
        TimeSpan diff = dateTwo.Subtract(dateOne);
        int totaldays = Convert.ToInt32(diff.TotalDays);

        if (totaldays > 2)
        {
            Session["Count"] = "Y";
            Errormsg("Shift allocation not applicable before month last 2 days, please visit again in the last 2 days of the month.");
            lbtnSaveNProceed.Visible = false;
            return;
        }
    }

    private void Errormsg(string _msg)
    {
        lblerrmsg.Text = _msg;
        mpError.Show();
    }
    private void Successmsg(string _msg)
    {
        lblsuccessmsg.Text = _msg;
        mpsuccess.Show();
    }
    private void LoadEmployeeCount(string OfcLvl, string Ofc, string year, string month)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_getshiftemployeecount");
            MyCommand.Parameters.AddWithValue("@p_ofclvl", OfcLvl);
            MyCommand.Parameters.AddWithValue("@p_ofc", Ofc);
            MyCommand.Parameters.AddWithValue("@p_year", year);
            MyCommand.Parameters.AddWithValue("@p_month", month);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {

                    lbtntotalemp.Text = dt.Rows[0]["totalemp_"].ToString();
                    lbtnShifted.Text = dt.Rows[0]["shiftemployee_"].ToString();
                    lbtnpendingshift.Text = dt.Rows[0]["pendingemp_"].ToString();

                    lbtn1shift.Text = dt.Rows[0]["shift1_"].ToString();
                    lbtn2shift.Text = dt.Rows[0]["shift2_"].ToString();
                    lbtn3shift.Text = dt.Rows[0]["shift3_"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            // Handle exception if needed
        }
    }
    private void LoadShift(DropDownList ddl)
    {
        try
        {
            ddl.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_getshift");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddl.DataSource = dt;
                    ddl.DataValueField = "shift_id_";
                    ddl.DataTextField = "shift_";
                    ddl.DataBind();
                }
            }

            ddl.Items.Insert(0, "No Shift");
            ddl.Items[0].Value = "0";
            ddl.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddl.Items.Insert(0, "-Select-");
            ddl.Items[0].Value = "0";
            ddl.SelectedIndex = 0;
        }
    }
    private bool CheckShiftAllocation(string OfcLvl, string Ofc, string year, string month)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_checkshiftallocation");
            MyCommand.Parameters.AddWithValue("@p_ofc", Ofc);
            MyCommand.Parameters.AddWithValue("@p_year", year);
            MyCommand.Parameters.AddWithValue("@p_month", month);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {

                    lblmsg.Text = "Select Duty Before Proceeding";
                    return false;
                }
            }

            lblmsg.Text = "Shift Allocation not allowed for selected month, please select next month";
            return true;
        }
        catch (Exception ex)
        {
            lblmsg.Text = "Shift Allocation not allowed for selected month, please select next month";
            return true;
        }
    }

    private void AddDutyShift()
    {

            try
            {
                string year = ddlYear.SelectedValue;
                string mm = ddlMonth.SelectedValue;

                DateTime firstDay;
                if (DateTime.Now.ToString("yyyy") == year && DateTime.Now.ToString("MM") == mm)
                {
                    firstDay = DateTime.Now;
                }
                else
                {
                    firstDay = new DateTime(int.Parse(year), int.Parse(mm), 1);
                }

                DateTime lastDay = new DateTime(int.Parse(year), int.Parse(mm), 1);
                lastDay = lastDay.AddMonths(1).AddDays(-1);

                string IPAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (string.IsNullOrEmpty(IPAddress))
                {
                    IPAddress = Request.ServerVariables["REMOTE_ADDR"];
                }

                int totalCount = grdemployee.Rows.Count;
                int successCount = 0;

                foreach (GridViewRow row in grdemployee.Rows)
                {
                    string shiftType = ((DropDownList)row.FindControl("ddlshifttype")).SelectedValue;
                    string empCode = ((Label)row.FindControl("lblEMPCODE")).Text;


                MyCommand = new NpgsqlCommand();
                MyCommand.Parameters.Clear();
                MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_addshift");
                MyCommand.Parameters.AddWithValue("@p_year", year);
                MyCommand.Parameters.AddWithValue("@p_month", mm);
                MyCommand.Parameters.AddWithValue("@p_officeid", Session["_OfficeId"]);
                MyCommand.Parameters.AddWithValue("@p_empcode", empCode);
                MyCommand.Parameters.AddWithValue("@p_shifttype", Convert.ToDecimal(shiftType));
                MyCommand.Parameters.AddWithValue("@p_shiftfromdate", firstDay);
                MyCommand.Parameters.AddWithValue("@p_shifttodate", lastDay);
                MyCommand.Parameters.AddWithValue("@p_totalemp", totalCount);
                MyCommand.Parameters.AddWithValue("@p_updatedby", Session["_UserCode"]);
                MyCommand.Parameters.AddWithValue("@p_ipaddress", IPAddress);
                dt = bll.SelectAll(MyCommand);
                if (dt.TableName == "Success")
                {

                    successCount++;
                    LoadEmployee(Session["_OfficeLevel"].ToString(), Session["_OfficeId"].ToString(), Session["Year"].ToString(), Session["Month"].ToString());
                    LoadEmployeeCount(Session["_OfficeLevel"].ToString(), Session["_OfficeId"].ToString(), Session["Year"].ToString(), Session["Month"].ToString());

                }
                }

                if (totalCount == successCount)
                {
                    Successmsg("All Employee Shift Successfully Updated");
                }
                else
                {
                    Errormsg("Error Updating Employee Shift");
                }
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately
            }
        }


    private bool ValidValue()
    {
        try
        {
            foreach (GridViewRow row in grdemployee.Rows)
            {
                string shifttype = ((DropDownList)row.FindControl("ddlshifttype")).SelectedValue;
                string empname = ((Label)row.FindControl("lblEMPNAME")).Text + "/" + ((Label)row.FindControl("lblEMPCODE")).Text;

                if (shifttype == "0")
                {
                    Errormsg("Select Valid Shift For Employee " + empname);
                    return false;
                }
            }
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }


    private void ConfirmDutyShift()
    {
        try
        {
            string year = Session["Year"].ToString();
            string mm = Session["Month"].ToString();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_getshift");
            dt = bll.SelectAll(MyCommand);

            DataTable table = new DataTable();
            table.Columns.Add("Date", typeof(string));
            foreach (DataRow dtrow in dt.Rows)
            {
                table.Columns.Add(dtrow["shift_"].ToString());
            }
            DateTime firstDay = new DateTime(Convert.ToInt16(year), Convert.ToInt16(mm), 1);
            DateTime lastDay = new DateTime(Convert.ToInt16(year), Convert.ToInt16(mm), 1);
            lastDay = firstDay.AddMonths(1).AddDays(-1);
            string name = "";
            string empCode = "";
            while (firstDay <= lastDay)
            {
                DataRow dr = table.NewRow();
                dr["Date"] = firstDay.ToString("dd/MM/yyyy");

                foreach (DataColumn column in table.Columns)
                {
                    name = "";
                    empCode = "";
                    string columnName = column.ColumnName;

                    if (columnName != "Date")
                    {
                        foreach (GridViewRow row in grdemployee.Rows)
                        {
                            string shiftType = ((DropDownList)row.FindControl("ddlshifttype")).SelectedItem.ToString();
                            empCode = ((Label)row.FindControl("lblEMPNAME")).Text;
                            if (columnName == shiftType)
                            {
                                name += empCode + ", ";
                                name = name.Substring(0, name.Length - 2);
                            }
                        }

                        dr[columnName] = name;
                    }
                }

                table.Rows.Add(dr);
                firstDay = firstDay.AddDays(1);
            }

            GridView1.DataSource = table;
            GridView1.DataBind();
            mpShift.Show();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }


    #endregion


    #region "Event"
    protected void lbtnerrorclose_Click(object sender, EventArgs e)
    {
        mpError.Hide();
        // mpShift.Show();
    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadmonth();
        CheckDays(ddlYear.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString());
        LoadEmployee(Session["_OfficeLevel"].ToString(), Session["_OfficeId"].ToString(), ddlYear.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString());
        LoadEmployeeCount(Session["_OfficeLevel"].ToString(), Session["_OfficeId"].ToString(), ddlYear.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString());


    }
    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        CheckDays(ddlYear.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString());
        LoadEmployee(Session["_OfficeLevel"].ToString(), Session["_OfficeId"].ToString(), ddlYear.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString());
        LoadEmployeeCount(Session["_OfficeLevel"].ToString(), Session["_OfficeId"].ToString(), ddlYear.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString());

        //if (CheckShiftAllocation(Session["_ofcLevelID"].ToString(), Session["_ofcID"].ToString(), Session["Year"].ToString(), Session["Month"].ToString()))
        //{
        //    Errormsg("Shift Allocation not allowed for selected month, please select next month");
        //    lbtnSaveNProceed.Visible = false;
        //}

    }
    protected void grdemployee_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView rowView = (DataRowView)e.Row.DataItem;
            if (Session["_UserCode"].ToString() == rowView["emp_code_"].ToString())
            {
                e.Row.Visible = false;
            }

            DropDownList ddlshifttype = (DropDownList)e.Row.FindControl("ddlshifttype");
            LoadShift(ddlshifttype);
            // ddlshifttype.SelectedValue = rowView["SHIFT_ID"].ToString();
        }

    }
    protected void grdemployee_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void lbtnSaveNProceed_Click(object sender, EventArgs e)
    {
        if (!ValidValue())
        {
            return;
        }
        ConfirmDutyShift();

    }

    protected void lbtnsave_Click(object sender, EventArgs e)
    {
        AddDutyShift();
    }


    #endregion
}