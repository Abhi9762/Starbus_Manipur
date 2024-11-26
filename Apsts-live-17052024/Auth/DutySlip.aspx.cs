using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.Data;
using System.Runtime.InteropServices;
using System.Net;
using System.Drawing.Printing;
using KeepAutomation.Barcode.Crystal;
using QRCoder;
using System.Drawing;
using System.Security.Cryptography;
using System.Xml;
using Npgsql;
using System.Configuration;
using AjaxControlToolkit;

partial class Auth_DutySlip : System.Web.UI.Page
{
	private NpgsqlCommand MyCommand;
	DataTable dt = new DataTable();
	private sbBLL bll = new sbBLL();
	protected void Page_Load(object sender, System.EventArgs e)
	{
		//if (Session["_RNDIDENTIFIERTK"] == null || Session["_RNDIDENTIFIERTK"].ToString() == "")
		//{
		//	Response.Redirect("../Errorpage.aspx");
		//}
		//if (Session["_RoleCode"].ToString() != "9")
		//{
		//	Response.Redirect("../Errorpage.aspx");
		//}
		//if (Session["dutySlipRefno"] == null | Session["dutySlipRefno"].ToString() == "")
		//{
		//	Response.Redirect("errorpage.aspx", true);
		//}

		XmlDocument doc = new XmlDocument();
		doc.Load(Server.MapPath("../CommonData.xml"));
		XmlNodeList logoUrl = doc.GetElementsByTagName("dept_logo_url");
		imgLogo.ImageUrl = logoUrl.Item(0).InnerXml;

		XmlNodeList dept_Name = doc.GetElementsByTagName("dept_Name_en");
		lblHeading.InnerText = System.Web.HttpUtility.HtmlDecode(dept_Name.Item(0).InnerXml);

		lblHelpdeskdept.Text = "For any query please contact " + dept_Name.Item(0).InnerXml + " helpdesk";

		LoadDutyAllotmentSlip();
	}

	private void LoadDutyAllotmentSlip()
	{
		try
		{
           // Session["dutySlipRefno"]

            string dutyrefno = Session["dutySlipRefno"].ToString();
			string officeid = Session["_LDepotCode"].ToString();
			MyCommand = new NpgsqlCommand();
			MyCommand.Parameters.Clear();

			MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_dutyslipslist");
			MyCommand.Parameters.AddWithValue("p_busdepotcode", officeid);
			MyCommand.Parameters.AddWithValue("p_dutysliprefno", dutyrefno);
			MyCommand.Parameters.AddWithValue("p_service", 0);
			MyCommand.Parameters.AddWithValue("p_routeid", 0);
			MyCommand.Parameters.AddWithValue("p_bustype", "0");
			MyCommand.Parameters.AddWithValue("p_fromdate", "0");
			MyCommand.Parameters.AddWithValue("p_todate", "0");

			dt = bll.SelectAll(MyCommand);
			if (dt.TableName == "Success")
			{
				if (dt.Rows.Count > 0)
				{
					details.Visible = true;
					msg.Visible = false;
					lblRefNo.Text = dutyrefno.ToString();
					if (!DBNull.Value.Equals(dt.Rows[0]["dutydatetime"]))
					{
						lblDutyDate.Text = dt.Rows[0]["dutydatetime"].ToString();
					}
					if (!DBNull.Value.Equals(dt.Rows[0]["office_name"]))
					{
						lblDepot.Text = dt.Rows[0]["office_name"].ToString();
					}
					if (!DBNull.Value.Equals(dt.Rows[0]["route_name"]))
					{
						lblRoute.Text = dt.Rows[0]["route_name"].ToString();
					}
					if (!DBNull.Value.Equals(dt.Rows[0]["BUSNO"]))
					{
						lblBusNo.Text = dt.Rows[0]["BUSNO"].ToString();
					}
					if (!DBNull.Value.Equals(dt.Rows[0]["ODOMETERREADING"]))
					{
						lblOdometerReading.Text = dt.Rows[0]["ODOMETERREADING"].ToString();
					}
					if (!DBNull.Value.Equals(dt.Rows[0]["INSURANCENO"]))
					{
						lblInsuranceNo.Text = dt.Rows[0]["INSURANCENO"].ToString();
					}
					if (!DBNull.Value.Equals(dt.Rows[0]["INSURANCEVALIDITY"]))
					{
						lblInsuranceValidity.Text = dt.Rows[0]["INSURANCEVALIDITY"].ToString();
					}
					if (!DBNull.Value.Equals(dt.Rows[0]["POLLUTIONCERTIFICATENO"]))
					{
						lblPollutionNo.Text = dt.Rows[0]["POLLUTIONCERTIFICATENO"].ToString();
					}
					if (!DBNull.Value.Equals(dt.Rows[0]["POLLUTIONVALIDITY"]))
					{
						lblPollutionValidity.Text = dt.Rows[0]["POLLUTIONVALIDITY"].ToString();
					}
					if (!DBNull.Value.Equals(dt.Rows[0]["FITNESSCERTIFICATENO"]))
					{
						lblFitnessNo.Text = dt.Rows[0]["FITNESSCERTIFICATENO"].ToString();
					}
					if (!DBNull.Value.Equals(dt.Rows[0]["FITNESSVALIDITY"]))
					{
						lblFitnessValidity.Text = dt.Rows[0]["FITNESSVALIDITY"].ToString();
					}
					if (!DBNull.Value.Equals(dt.Rows[0]["DRIVER1NAME"]))
					{
						lblDriver1.Text = dt.Rows[0]["DRIVER1NAME"].ToString();
					}
					if (!DBNull.Value.Equals(dt.Rows[0]["CONDUCTOR1NAME"]))
					{
						lblConductor1.Text = dt.Rows[0]["CONDUCTOR1NAME"].ToString();
					}
					if (!DBNull.Value.Equals(dt.Rows[0]["DRIVER1LICENSENO"]))
					{
						lblDriver1LicenseNo.Text = dt.Rows[0]["DRIVER1LICENSENO"].ToString();
					}
					if (!DBNull.Value.Equals(dt.Rows[0]["CONDUCTOR1LICENSENO"]))
					{
						lblCond1LicenseNo.Text = dt.Rows[0]["CONDUCTOR1LICENSENO"].ToString();
					}
					if (!DBNull.Value.Equals(dt.Rows[0]["DRIVER1LICENSEVALIDITY"]))
					{
						lblDriver1LicenseValidity.Text = dt.Rows[0]["DRIVER1LICENSEVALIDITY"].ToString();
					}
					if (!DBNull.Value.Equals(dt.Rows[0]["COND1LICENSEVALIDITY"]))
					{
						lblCond1LicenseValidity.Text = dt.Rows[0]["COND1LICENSEVALIDITY"].ToString();
					}
					if (!DBNull.Value.Equals(dt.Rows[0]["DRIVER2NAME"]))
					{
						lblDriver2.Text = dt.Rows[0]["DRIVER2NAME"].ToString();
					}
					if (!DBNull.Value.Equals(dt.Rows[0]["CONDUCTOR2NAME"]))
					{
						lblConductor2.Text = dt.Rows[0]["CONDUCTOR2NAME"].ToString();
					}
					if (!DBNull.Value.Equals(dt.Rows[0]["DRIVER2LICENSENO"]))
					{
						lblDriver2LicenseNo.Text = dt.Rows[0]["DRIVER2LICENSENO"].ToString();
					}
					if (!DBNull.Value.Equals(dt.Rows[0]["CONDUCTOR2LICENSENO"]))
					{
						lblConductor2LicenseNo.Text = dt.Rows[0]["CONDUCTOR2LICENSENO"].ToString();
					}
					if (!DBNull.Value.Equals(dt.Rows[0]["DRIVER2LICENSEVALIDITY"]))
					{
						lblDriver2LicenseValidity.Text = dt.Rows[0]["DRIVER2LICENSEVALIDITY"].ToString();
					}
					if (!DBNull.Value.Equals(dt.Rows[0]["COND2LICENSEVALIDITY"]))
					{
						lblCond2LicenseValidity.Text = dt.Rows[0]["COND2LICENSEVALIDITY"].ToString();
					}
					if (!DBNull.Value.Equals(dt.Rows[0]["TARGETINCOME"]))
					{
						lblTargetIncome.Text = dt.Rows[0]["TARGETINCOME"].ToString();
					}
					if (!DBNull.Value.Equals(dt.Rows[0]["TARGETDIESELAVERAGE"]))
					{
						lblTargetDieselAvg.Text = dt.Rows[0]["TARGETDIESELAVERAGE"].ToString();
					}
					if (!DBNull.Value.Equals(dt.Rows[0]["SCHEDULEKM"]))
					{
						lblScheduleKm.Text = dt.Rows[0]["SCHEDULEKM"].ToString();
					}
					if (!DBNull.Value.Equals(dt.Rows[0]["CHANGESTATION"]))
					{
						lblChangeStation.Text = dt.Rows[0]["CHANGESTATION"].ToString();
					}
					if (!DBNull.Value.Equals(dt.Rows[0]["SERVICENAME"]))
					{
						lblServiceName.Text = dt.Rows[0]["SERVICENAME"].ToString();
					}
					if (!DBNull.Value.Equals(dt.Rows[0]["PERMITNO"]))
					{
						lblPermitNo.Text = dt.Rows[0]["PERMITNO"].ToString();
					}
					if (!DBNull.Value.Equals(dt.Rows[0]["PERMITVALIDITY"]))
					{
						lblPermitValidity.Text = dt.Rows[0]["PERMITVALIDITY"].ToString();
					}
					lblJIName.Text = dt.Rows[0]["JUNIORINCHARGENAME"].ToString();
					lblJIDesignation.Text = dt.Rows[0]["JIDESIGNATIONNAME"].ToString();
					lblprintdatetime.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"); // DateTime.Now
					Session["_DSVC_ID"] = dt.Rows[0]["DSVC_ID"].ToString();
					LoadServiceTripDetails();
					// -----------------QR CODE STARTS
					string key = ConfigurationManager.AppSettings["QRCodehashKey"];
					string waybillNo = dutyrefno.ToString();
					string service = dt.Rows[0]["servicename"].ToString();
					string DutyDate = dt.Rows[0]["dutydate"].ToString();
					string qrcodeString =  waybillNo + "|" + service + "|" + DutyDate;
					QRCodeGenerator qrGenerator = new QRCodeGenerator();
					QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(qrcodeString, QRCodeGenerator.ECCLevel.Q);
					System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
					imgBarCode.Height = 150;
					imgBarCode.Width = 150;
					byte[] byteImage;
					using (Bitmap bitMap = qrCode.GetGraphic(20))
					{
						using (MemoryStream ms = new MemoryStream())
						{
							bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
							byteImage = ms.ToArray();
						}
					}
					imgQRCode.Src = GetImage(byteImage);
					imgQRCode.Visible = true;
				}
				else
				{
					details.Visible = true;
					msg.Visible = false;
				}
			}
		}
		catch (Exception ex)
		{
			details.Visible = true;
			msg.Visible = false;
		}
	}
	public void LoadServiceTripDetails()
	{
		try
		{
			DataTable dt1 = new DataTable();
			MyCommand = new NpgsqlCommand();
			int serviceID = Convert.ToInt32(Session["_DSVC_ID"].ToString());
			string dutyrefno = Session["dutySlipRefno"].ToString();

			MyCommand.Parameters.Clear();
			MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_serviceodrdtlsbydsvcid");
			MyCommand.Parameters.AddWithValue("p_dsvcid", serviceID);
			MyCommand.Parameters.AddWithValue("p_dutyrefno", dutyrefno);
			dt1 = bll.SelectAll(MyCommand);
			if (dt1.TableName == "Success")
			{
				if (dt1.Rows.Count > 0)
				{
					gvServiceTrips.DataSource = dt1;
					gvServiceTrips.DataBind();
				}
			}
		}
		catch (Exception ex)
		{
		}
	}
	public string GetImage(object img)
	{
		try
		{
			return "data:image/jpg;base64," + Convert.ToBase64String((byte[])img);
		}
		catch (Exception ex)
		{
			return "";
		}
	}
	public byte[] QRimageBytes(string value)
	{
		byte[] imageData1 = null;
		try
		{
			BarCode barcode = new BarCode();
			barcode.Symbology = KeepAutomation.Barcode.Symbology.QRCode;
			barcode.ImageFormat = System.Drawing.Imaging.ImageFormat.Png;
			barcode.CodeToEncode = value;
			imageData1 = barcode.generateBarcodeToByteArray();
		}
		catch (Exception ex)
		{
			imageData1 = null;
		}
		return imageData1;
	}
}
