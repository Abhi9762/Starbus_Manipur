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

public partial class Auth_WayBill : System.Web.UI.Page
{
	private NpgsqlCommand MyCommand;
	DataTable dt = new DataTable();
	private sbBLL bll = new sbBLL();
	protected void Page_Load(object sender, EventArgs e)
    {
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
			MyCommand = new NpgsqlCommand();
			MyCommand.Parameters.Clear();
			MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_dutywaybilldetails");
			MyCommand.Parameters.AddWithValue("p_waybillrefno", Session["DUTYREFNO"].ToString());
			dt = bll.SelectAll(MyCommand);
			if (dt.TableName == "Success")
			{
				if (dt.Rows.Count > 0)
				{
					details.Visible = true;
					msg.Visible = false;
					if (DBNull.Value.Equals(dt.Rows[0]["WAYBILLID"]) == false)
					{
						lblRefNo.Text = dt.Rows[0]["WAYBILLID"].ToString();
					}
					if (DBNull.Value.Equals(dt.Rows[0]["DEPARTURETIME"]) == false)
					{
						lblDutyDate.Text = dt.Rows[0]["DEPARTURETIME"].ToString();
					}
					if (DBNull.Value.Equals(dt.Rows[0]["OFFICE_NAME"]) == false)
					{
						lblDepot.Text = dt.Rows[0]["OFFICE_NAME"].ToString();
					}
					if (DBNull.Value.Equals(dt.Rows[0]["ROUTE"]) == false)
					{
						lblRoute.Text = dt.Rows[0]["ROUTE"].ToString();
					}
					if (DBNull.Value.Equals(dt.Rows[0]["BUSNO"]) == false)
					{
						lblBusNo.Text = dt.Rows[0]["BUSNO"].ToString();
					}
					if (DBNull.Value.Equals(dt.Rows[0]["ODOMETERREADING"]) == false)
					{
						lblOdometerReading.Text = dt.Rows[0]["ODOMETERREADING"].ToString();
					}
					if (DBNull.Value.Equals(dt.Rows[0]["INSURANCENO"]) == false)
					{
						lblInsuranceNo.Text = dt.Rows[0]["INSURANCENO"].ToString();
					}
					if (DBNull.Value.Equals(dt.Rows[0]["INSURANCEVALIDITY"]) == false)
					{
						lblInsuranceValidity.Text = dt.Rows[0]["INSURANCEVALIDITY"].ToString();
					}
					if (DBNull.Value.Equals(dt.Rows[0]["POLLUTIONCERTIFICATENO"]) == false)
					{
						lblPollutionNo.Text = dt.Rows[0]["POLLUTIONCERTIFICATENO"].ToString();
					}
					if (DBNull.Value.Equals(dt.Rows[0]["POLLUTIONVALIDITY"]) == false)
					{
						lblPollutionValidity.Text = dt.Rows[0]["POLLUTIONVALIDITY"].ToString();
					}
					if (DBNull.Value.Equals(dt.Rows[0]["FITNESSCERTIFICATENO"]) == false)
					{
						lblFitnessNo.Text = dt.Rows[0]["FITNESSCERTIFICATENO"].ToString();
					}
					if (DBNull.Value.Equals(dt.Rows[0]["FITNESSVALIDITY"]) == false)
					{
						lblFitnessValidity.Text = dt.Rows[0]["FITNESSVALIDITY"].ToString();
					}
					if (DBNull.Value.Equals(dt.Rows[0]["DRIVER1NAME"]) == false)
					{
						lblDriver1.Text = dt.Rows[0]["DRIVER1NAME"].ToString();
					}
					if (DBNull.Value.Equals(dt.Rows[0]["CONDUCTOR1NAME"]) == false)
					{
						lblConductor1.Text = dt.Rows[0]["CONDUCTOR1NAME"].ToString();
					}
					if (DBNull.Value.Equals(dt.Rows[0]["DRIVER1LICENSENO"]) == false)
					{
						lblDriver1LicenseNo.Text = dt.Rows[0]["DRIVER1LICENSENO"].ToString();
					}
					if (DBNull.Value.Equals(dt.Rows[0]["CONDUCTOR1LICENSENO"]) == false)
					{
						lblCond1LicenseNo.Text = dt.Rows[0]["CONDUCTOR1LICENSENO"].ToString();
					}
					if (DBNull.Value.Equals(dt.Rows[0]["DRIVER1LICENSEVALIDITY"]) == false)
					{
						lblDriver1LicenseValidity.Text = dt.Rows[0]["DRIVER1LICENSEVALIDITY"].ToString();
					}
					if (DBNull.Value.Equals(dt.Rows[0]["COND1LICENSEVALIDITY"]) == false)
					{
						lblCond1LicenseValidity.Text = dt.Rows[0]["COND1LICENSEVALIDITY"].ToString();
					}
					if (DBNull.Value.Equals(dt.Rows[0]["DRIVER2NAME"]) == false)
					{
						lblDriver2.Text = dt.Rows[0]["DRIVER2NAME"].ToString();
					}
					if (DBNull.Value.Equals(dt.Rows[0]["CONDUCTOR2NAME"]) == false)
					{
						lblConductor2.Text = dt.Rows[0]["CONDUCTOR2NAME"].ToString();
					}
					if (DBNull.Value.Equals(dt.Rows[0]["DRIVER2LICENSENO"]) == false)
					{
						lblDriver2LicenseNo.Text = dt.Rows[0]["DRIVER2LICENSENO"].ToString();
					}
					if (DBNull.Value.Equals(dt.Rows[0]["CONDUCTOR2LICENSENO"]) == false)
					{
						lblConductor2LicenseNo.Text = dt.Rows[0]["CONDUCTOR2LICENSENO"].ToString();
					}
					if (DBNull.Value.Equals(dt.Rows[0]["DRIVER2LICENSEVALIDITY"]) == false)
					{
						lblDriver2LicenseValidity.Text = dt.Rows[0]["DRIVER2LICENSEVALIDITY"].ToString();
					}
					if (DBNull.Value.Equals(dt.Rows[0]["COND2LICENSEVALIDITY"]) == false)
					{
						lblCond2LicenseValidity.Text = dt.Rows[0]["COND2LICENSEVALIDITY"].ToString();
					}
					if (DBNull.Value.Equals(dt.Rows[0]["TARGETINCOME"]) == false)
					{
						lblTargetIncome.Text = dt.Rows[0]["TARGETINCOME"].ToString();
					}
					if (DBNull.Value.Equals(dt.Rows[0]["TARGETDIESELAVERAGE"]) == false)
					{
						lblTargetDieselAvg.Text = dt.Rows[0]["TARGETDIESELAVERAGE"].ToString();
					}
					if (DBNull.Value.Equals(dt.Rows[0]["SCHEDULEKM"]) == false)
					{
						lblScheduleKm.Text = dt.Rows[0]["SCHEDULEKM"].ToString();
					}
					if (DBNull.Value.Equals(dt.Rows[0]["CHANGESTATION"]) == false)
					{
						lblChangeStation.Text = dt.Rows[0]["CHANGESTATION"].ToString();
					}
					if (DBNull.Value.Equals(dt.Rows[0]["SERVICENAME"]) == false)
					{
						lblServiceName.Text = dt.Rows[0]["SERVICENAME"].ToString();
					}
					if (DBNull.Value.Equals(dt.Rows[0]["PERMITNO"]) == false)
					{
						lblPermitNo.Text = dt.Rows[0]["PERMITNO"].ToString();
					}
					if (DBNull.Value.Equals(dt.Rows[0]["PERMITVALIDITY"]) == false)
					{
						lblPermitValidity.Text = dt.Rows[0]["PERMITVALIDITY"].ToString();
					}
					lblJIName.Text = dt.Rows[0]["JUNIORINCHARGENAME"].ToString();
					if (DBNull.Value.Equals(dt.Rows[0]["JIDESIGNATIONNAME"]) == false)
					{
						lblJIDesignation.Text = dt.Rows[0]["JIDESIGNATIONNAME"].ToString();
					}
					if (DBNull.Value.Equals(dt.Rows[0]["ETMSERIALNO"]) == false)
					{
						lblETM.Text = dt.Rows[0]["ETMSERIALNO"].ToString();
					}
					if (dt.Rows[0]["DENOMINATIONBOOK"].ToString() == "Y")
					{
						lblTicketBook.Text = "Yes";
					}
					else
					{
						lblTicketBook.Text = "No";
					}
					// If Not IsDBNull(dt.Rows[0]["MANUALTICKETSTART")) Then
					// lblTicketBook.Text = dt.Rows[0]["MANUALTICKETSTART").ToString + " To " + dt.Rows[0]["MANUALTICKETEND").ToString
					// End If
					lblprintdatetime.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"); // DateTime.Now
					if (dt.Rows[0]["DENOMINATIONBOOK"].ToString() == "Y")
					{
						trDenominationBook.Visible = true;
						gvDenomination.DataSource = dt;
						gvDenomination.DataBind();
					}
					// -----------------QR CODE STARTS
					string key = ConfigurationManager.AppSettings["QRCodehashKey"];
					string waybillNo = dt.Rows[0]["WAYBILLID"].ToString();
					string service = dt.Rows[0]["SERVICENAME"].ToString();
					string DutyDate = dt.Rows[0]["DUTYDATE"].ToString();
					string etm = dt.Rows[0]["ETMSERIALNO"].ToString();
					string qrcodeString = waybillNo +  "|" + service + "|" + etm + "|" + DutyDate;
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