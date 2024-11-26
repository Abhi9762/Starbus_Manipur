using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class Auth_DutyAllotmentSlip : System.Web.UI.Page
{
	private NpgsqlCommand MyCommand;
	DataTable dt = new DataTable();
	private sbBLL bll = new sbBLL();
	protected void Page_Load(object sender, EventArgs e)
	{
		if (Session["_RNDIDENTIFIERTK"] == null | Session["_RNDIDENTIFIERTK"].ToString() == "")
		{
			Response.Redirect("../Errorpage.aspx");
		}
		if (Session["_RoleCode"].ToString() != "9")
		{
			Response.Redirect("../Errorpage.aspx");
		}
		if (Session["dutyRefno"] == null | Session["dutyRefno"].ToString() == "")
		{
			Response.Redirect("errorpage.aspx", true);
		}
		if (IsPostBack == false)
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(Server.MapPath("../CommonData.xml"));
			XmlNodeList logoUrl = doc.GetElementsByTagName("dept_logo_url");
			imgLogo.ImageUrl = logoUrl.Item(0).InnerXml;

			XmlNodeList dept_Name = doc.GetElementsByTagName("dept_Name_en");
			lblHeading.InnerText = System.Web.HttpUtility.HtmlDecode(dept_Name.Item(0).InnerXml);

			lblHelpdeskdept.Text = "For any query please contact " + dept_Name.Item(0).InnerXml + " helpdesk";

			LoadDutyAllotmentSlip();
		}
	}
	private void LoadDutyAllotmentSlip()
	{
		try
		{
			string dutyrefno = Session["dutyRefno"].ToString();
			MyCommand = new NpgsqlCommand();

			MyCommand.Parameters.Clear();
			MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_dutyallotmentslip");
			MyCommand.Parameters.AddWithValue("p_refno", dutyrefno);
			dt = bll.SelectAll(MyCommand);
			if (dt.TableName == "Success")
			{
				details.Visible = true;
				lblMessage.Visible = false;
				lbldutyRefNo.Text = dt.Rows[0]["dutyrefno"].ToString();
				lblServiceCode.Text = dt.Rows[0]["serviceid"].ToString();
				lblServiceName.Text = dt.Rows[0]["service_name_en"].ToString();
				// lblDepartureTime.Text = dt.Rows[0]["DEPARTURETIME")
				lblBusNo.Text = dt.Rows[0]["busno"].ToString();
				lblConductor1.Text = dt.Rows[0]["empconductor1"].ToString();
				lblConductor2.Text = dt.Rows[0]["empconductor2"].ToString();
				lblDriver1.Text = dt.Rows[0]["empdriver1"].ToString();
				lblDriver2.Text = dt.Rows[0]["empdriver2"].ToString();
				lblDutyDays.Text = dt.Rows[0]["dutydays"].ToString();
				lblRestDays.Text = dt.Rows[0]["dutyrestdays"].ToString();
				lblTripDateTime.Text = dt.Rows[0]["departuretime"].ToString();
				lblBustype.Text = dt.Rows[0]["bustype"].ToString();
				lblPrintBy.Text = dt.Rows[0]["updatedby"].ToString();
				lblPrintDateTime.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");
			}
			else
			{
				details.Visible = true;
				lblMessage.Visible = false;
			}
		}
		catch (Exception ex)
		{
			details.Visible = true;
			lblMessage.Visible = false;
		}
	}

}