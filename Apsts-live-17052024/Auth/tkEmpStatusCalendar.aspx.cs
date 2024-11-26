using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Collections;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
//using CrystalDecisions.CrystalReports.Engine;
//using CrystalDecisions.Shared;

public partial class Auth_tkEmpStatusCalendar : System.Web.UI.Page
{
	sbValidation _validation = new sbValidation();
	private NpgsqlCommand MyCommand;
	DataTable dt = new DataTable();
	Hashtable htEmpStatus;
	private sbBLL bll = new sbBLL();
	private sbCommonFunc _common = new sbCommonFunc();
	sbSecurity _security = new sbSecurity();
	sbLoaderNdPopup _popup = new sbLoaderNdPopup();
	protected void Page_Load(object sender, EventArgs e)
	{
		Session["moduleName"] = "Employee Duty Status Calander";
		if (!IsPostBack)
		{
			if (Session["empCode_chartDash"] == null || Session["empCode_chartDash"].ToString() == "")
			{
				Session["_ErrorMsg"] = " <br/> Something went wrong.";
				Response.Redirect("../Errorpage.aspx");
			}
			hdnempcode.Value = Session["empCode_chartDash"].ToString();
			lblEmpName.Text = Session["empName_chartDash"].ToString();
			lblEmpDesignation.Text = Session["empDesigName_chartDash"].ToString();


			Calendar1.FirstDayOfWeek = System.Web.UI.WebControls.FirstDayOfWeek.Sunday;
			Calendar1.ShowNextPrevMonth = false;
			Calendar1.TitleFormat = TitleFormat.MonthYear;
			Calendar1.TitleStyle.Height = new Unit(90);
			Calendar1.ShowGridLines = true;
			Calendar1.DayStyle.Height = new Unit(55);
			Calendar1.DayStyle.Width = new Unit(140);
			Calendar1.SelectorStyle.Height = new Unit(60);
			Calendar1.DayStyle.HorizontalAlign = HorizontalAlign.Center;
			Calendar1.DayStyle.VerticalAlign = VerticalAlign.Middle;
			Calendar1.OtherMonthDayStyle.BackColor = System.Drawing.Color.AliceBlue;

			months();
			years();
		}

		loadEmployeeStatus();
	}
	public static string DataTableToJSONWithJavaScriptSerializer(DataTable table)
	{
		JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
		List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
		Dictionary<string, object> childRow;

		foreach (DataRow row in table.Rows)
		{
			childRow = new Dictionary<string, object>();

			foreach (DataColumn col in table.Columns)
				childRow.Add(col.ColumnName, row[col]);

			parentRow.Add(childRow);
		}

		return jsSerializer.Serialize(parentRow);
	}
	public void months()
	{
		for (int month = 1; month <= 12; month++)
		{
			string monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
			string months = ("00" + month.ToString()).Substring(1, 2);
			ddlMonthNames.Items.Add(new ListItem(monthName, months));
		}
		string monthId = DateTime.Now.Date.ToString("MM");
		ddlMonthNames.SelectedValue = monthId.Trim();
	}
	public void years()
	{
		var currentYear = DateTime.Now.Year;
		for (int year = 2021; year <= currentYear; year++)
		{
			ddlYear.Items.Add(new ListItem(year.ToString(), year.ToString()));
		}
		ddlYear.SelectedValue = currentYear.ToString();
	}
	public void loadEmployeeStatus()
	{
		try
		{
			DataTable dt = new DataTable();

			MyCommand = new NpgsqlCommand();
			string empcode = Session["empCode_chartDash"].ToString();
			string attendancedate = "01/" + ddlMonthNames.SelectedValue.ToString() + "/" + ddlYear.SelectedValue.ToString();
			MyCommand.Parameters.Clear();
			MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_empmonthlydutystatus");
			MyCommand.Parameters.AddWithValue("p_empcode", empcode);
			MyCommand.Parameters.AddWithValue("p_date", attendancedate);
			dt = bll.SelectAll(MyCommand);


			htEmpStatus = new Hashtable();
			htEmpStatus = convertDataTableToHashTable(dt, "status_date", "duty_status");
			DataTable dtChart = new DataTable();
			dtChart.Columns.Add("status", typeof(string));
			dtChart.Columns.Add("count", typeof(int));

			int presents = 0;
			int leaves = 0;
			int absents = 0;
			int duties = 0;

			foreach (DataRow drIn in dt.Rows)
			{
				if (drIn["duty_status"].ToString() == "P")
				{
					presents = presents + 1;
				}
				if (drIn["duty_status"].ToString() == "L")
				{
					leaves = leaves + 1;
				}
				if (drIn["duty_status"].ToString() == "A")
				{
					absents = absents + 1;
				}
				if (drIn["duty_status"].ToString() == "D")
				{
					duties = duties + 1;
				}
			}

			int year = Convert.ToInt16(ddlYear.SelectedValue.ToString());
			int month = Convert.ToInt16(ddlMonthNames.SelectedValue.ToString());

			int monthDays = DateTime.DaysInMonth(year, month);
			var notMarkedDays = monthDays - (presents + leaves + duties + absents);

			dtChart.Rows.Add("Present", presents);
			dtChart.Rows.Add("Absent", absents);
			dtChart.Rows.Add("Leave", leaves);
			dtChart.Rows.Add("Duty", duties);
			dtChart.Rows.Add("Not Marked", notMarkedDays);

			lblTotalCount.Text = monthDays.ToString();
			lblNotMarkedCount.Text = notMarkedDays.ToString();
			lblPresentCount.Text = presents.ToString();
			lblDutyCount.Text = duties.ToString();
			lblLeaveCount.Text = leaves.ToString();
			lblAbsentCount.Text = absents.ToString();


			//Chart

			string chart = "";

			chart = "<canvas id='bookpiechartmode' width='100%' height='90' ></canvas><script>";

			chart = chart + "new Chart(document.getElementById('bookpiechartmode'),   { type: 'pie', data: {labels: [ ";


			for (int i = 0; i <= dtChart.Rows.Count - 1; i++)
				chart += "'" + dtChart.Rows[i]["status"].ToString() + "',";
			chart = chart.Substring(0, chart.Length - 1);
			chart = chart + "],datasets: [{ data: [";
			string value = "";

			for (int i = 0; i <= dtChart.Rows.Count - 1; i++)
				value += dtChart.Rows[i]["count"].ToString() + ",";
			value = value.Substring(0, value.Length - 1);
			chart = chart + value;
			chart = chart + "],backgroundColor: ['#11af4b','#f3545d','#fdaf4b',  '#1d7af3'],borderWidth: 0}";
			chart = chart + "]},options : {responsive: true, maintainAspectRatio: false, legend: {position: 'bottom',labels : {fontColor:'rgb(0, 0, 0)', fontSize: 11, usePointStyle : true, padding: 8	}},";
			chart = chart + "pieceLabel: {render: 'percentage',fontColor: 'white',fontSize: 11,},tooltips: {bodySpacing: 4,mode: 'nearest', intersect: 0, position: 'nearest', xPadding: 10, yPadding: 10, caretPadding: 10 }, layout: { padding: {left: 0, right: 0, top: 10, bottom: 0 } } }	});";
			chart = chart + "</script>";
			ImgpieChartBookingModeNOdata.Visible = false;
			ltpieChartBookingMode.Text = chart;
		}
		catch (Exception ex)
		{
			Errormsg("Something went wrong. Employee's data can not be load. Please contact to Admin");
		}
	}
	public static Hashtable convertDataTableToHashTable(DataTable dtIn, string keyField, string valueField)
	{
		Hashtable htOut = new Hashtable();
		foreach (DataRow drIn in dtIn.Rows)
		{
			htOut.Add(drIn[keyField].ToString(), drIn[valueField].ToString());
		}
		return htOut;
	}
	protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
	{
		if (e.Day.IsOtherMonth)
		{
			e.Cell.Controls.Clear();
			e.Cell.Text = string.Empty;
		}
		else
		{
			string keyy = e.Day.Date.ToString("dd")+"/"+ e.Day.Date.ToString("MM") + "/" + e.Day.Date.ToString("yyyy");

           

			if (htEmpStatus[keyy] != null/* TODO Change to default(_) if this is not a reference type */ )
			{
				//string val = htEmpStatus[e.Day.Date.ToShortDateString()].ToString();
				string val = htEmpStatus[keyy].ToString();

				Literal literal1 = new Literal();
				literal1.Text = "<br/>";
				e.Cell.Controls.Add(literal1);
				Label label1 = new Label();
				label1.Font.Size = new FontUnit(FontSize.Small);
				if (val == "D")
				{
					label1.Text = "Duty";
					e.Cell.Controls.Add(label1);
					e.Cell.BackColor = System.Drawing.Color.FromArgb(29, 122, 243);
					e.Cell.ForeColor = System.Drawing.Color.White;
				}
				else if (val == "P")
				{
					label1.Text = "Present";
					e.Cell.Controls.Add(label1);
					e.Cell.BackColor = System.Drawing.Color.FromArgb(17, 175, 75);
					e.Cell.ForeColor = System.Drawing.Color.White;
				}
				else if (val == "L")
				{
					label1.Text = "Leave";
					e.Cell.Controls.Add(label1);
					e.Cell.BackColor = System.Drawing.Color.FromArgb(253, 175, 75);
					e.Cell.ForeColor = System.Drawing.Color.White;
				}
				else if (val == "A")
				{
					label1.Text = "Absent";
					e.Cell.Controls.Add(label1);
					e.Cell.BackColor = System.Drawing.Color.FromArgb(243, 84, 93);
					e.Cell.ForeColor = System.Drawing.Color.Black;
				}
                else if (val == "R")
                {
                    label1.Text = "Rest";
                    e.Cell.Controls.Add(label1);
                    e.Cell.BackColor = System.Drawing.Color.MediumTurquoise;
                    e.Cell.ForeColor = System.Drawing.Color.Black;
                }
                else
				{
					label1.Text = "Not Marked";
					e.Cell.Controls.Add(label1);
					e.Cell.BackColor = System.Drawing.Color.FromArgb(229, 229, 229);
					e.Cell.ForeColor = System.Drawing.Color.Black;
				}
			}
			else
			{
				Literal literal1 = new Literal();
				literal1.Text = "<br/>";
				e.Cell.Controls.Add(literal1);
				Label label1 = new Label();
				label1.Font.Size = new FontUnit(FontSize.Small);
				label1.Text = "Not Marked";
				e.Cell.Controls.Add(label1);
				e.Cell.BackColor = System.Drawing.Color.FromArgb(229, 229, 229);
				e.Cell.ForeColor = System.Drawing.Color.Black;
			}
		}
	}
	protected void lbtnSearch_Click(object sender, System.EventArgs e)
	{
		DateTime dt = new DateTime(Convert.ToInt16(ddlYear.SelectedItem.Text), Convert.ToInt16(ddlMonthNames.SelectedItem.Value), 1);
		Calendar1.VisibleDate = dt;
		loadEmployeeStatus();
	}
	protected void lbtnDownload_Click(object sender, System.EventArgs e)
	{
		//DataTable dtFullCalender = new DataTable();
		//dtFullCalender.Columns.Add("dates", typeof(string));
		//dtFullCalender.Columns.Add("status", typeof(string));

		//// Dim keyy As String = DateTime.Now.Month.
		//// For Each ht As DictionaryEntry In htEmpStatus
		//// Dim val As String = ht.Value
		//// dtFullCalender.Rows.Add(ht.Key, ht.Value)
		//// Next

		//DateTime datee = new DateTime(ddlYear.SelectedValue, ddlMonthNames.SelectedValue, 1);
		//DateTime firstDate = FirstDayOfMonth(datee);
		//var lastDate = LastDayOfMonth(datee);
		//while (firstDate.Date <= lastDate.Date)
		//{
		//	var fDate = firstDate.ToString("dd/MM/yyyy");
		//	string val;
		//	if (htEmpStatus(fDate) != null/* TODO Change to default(_) if this is not a reference type */ )
		//	{
		//		val = htEmpStatus(fDate).ToString();
		//		if (val == "D")
		//			val = "Duty";
		//		else if (val == "P")
		//			val = "Present";
		//		else if (val == "L")
		//			val = "Leave";
		//		else if (val == "A")
		//			val = "Absent";
		//		else
		//			val = "Not Marked";
		//	}
		//	else
		//		val = "Not Marked";
		//	dtFullCalender.Rows.Add(fDate, val);
		//	firstDate = firstDate.AddDays(1);
		//}

		//cryRpt.Load(rpt);
		//cryRpt.SetDataSource(dtFullCalender);

		//string emppp = Session["empName_chartDash"].ToString() + " - " + Session["empDesigName_chartDash"].ToString();
		//string datettet = "Monthly Attendance Register - " + ddlMonthNames.SelectedItem.ToString() + ", " + ddlYear.SelectedValue.ToString();

		//CrystalDecisions.CrystalReports.Engine.TextObject monyear = cryRpt.ReportDefinition.Sections(1).ReportObjects("textMonthYear");
		//monyear.Text = datettet;

		//CrystalDecisions.CrystalReports.Engine.TextObject objEmp = cryRpt.ReportDefinition.Sections(1).ReportObjects("textEmp");
		//objEmp.Text = emppp;

		//cryRpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "DutyStatus" + emppp);


		//string vadsl = "";
	}
	public DateTime FirstDayOfMonth(DateTime sourceDate)
	{
		return new DateTime(sourceDate.Year, sourceDate.Month, 1);
	}
	public DateTime LastDayOfMonth(DateTime sourceDate)
	{
		DateTime lastDay = new DateTime(sourceDate.Year, sourceDate.Month, 1);
		return lastDay.AddMonths(1).AddDays(-1);
	}
	public void lbtnBack_Click(object sender, EventArgs e)
	{
		Response.Redirect("tkAttendanceManagement.aspx");
	}	
	private void Errormsg(string msg)
	{
		string popup = _popup.modalPopupSmall("W", "Please Note", msg, "Close");
		Response.Write(popup);
	}
}