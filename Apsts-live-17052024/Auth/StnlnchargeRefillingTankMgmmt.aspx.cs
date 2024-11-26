using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Npgsql;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;

public partial class Auth_StnlnchargeRefillingTankMgmmt : System.Web.UI.Page
{
	sbValidation _validation = new sbValidation();
	private NpgsqlCommand MyCommand;
	DataTable dt = new DataTable();
	private sbBLL bll = new sbBLL();
	private sbCommonFunc _common = new sbCommonFunc();
	sbLoaderNdPopup _popup = new sbLoaderNdPopup();
	byte[] PhotoImage = null;
	[System.Runtime.InteropServices.DllImport("urlmon.dll")]
	public static extern int FindMimeFromData(IntPtr pBC, [MarshalAs(UnmanagedType.LPWStr)] string pwzUrl, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1, SizeParamIndex = 3)] byte[] pBuffer, int cbSize, [MarshalAs(UnmanagedType.LPWStr)] string pwzMimeProposed, int dwMimeFlags, [MarshalAs(UnmanagedType.U4)] ref int ppwzMimeOut, int dwReserved);
	protected void Page_Load(object sender, EventArgs e)
	{
		Session["_moduleName"] = "Refueling Tank Management";
		{
			if (IsPostBack == false)
			{
				lblDateTime.Text = "Summary as on Date : " + DateTime.Now.ToString("dd-MM-yyyy h:mmtt");
				LoadSummary();
				LoadFillingStation(ddlFillingStation);
				loadTank(Convert.ToInt16(ddlFillingStation.SelectedValue.ToString()), ddlTank);
				BindGridFillingStation();
			}
		}
	}

	#region Methods
	public void LoadSummary()
	{
		try
		{
			string officeid = Session["_LDepotCode"].ToString();
			ddlFillingStation.Items.Clear();
			MyCommand = new NpgsqlCommand();
			MyCommand.Parameters.Clear();
			MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_masterscount");
			MyCommand.Parameters.AddWithValue("p_depotcode", officeid);
			dt = bll.SelectAll(MyCommand);
			if (dt.TableName == "Success")
			{
				lblTotalFillingSt.Text = dt.Rows[0]["fillingst"].ToString();
				lblTotalTanks.Text = dt.Rows[0]["tank"].ToString();
				lblTotalPumps.Text = dt.Rows[0]["pump"].ToString();
			}
			else
			{
				_common.ErrorLog("TankFillingStnMgmt-M1", dt.TableName);
			}
		}
		catch (Exception ex)
		{
			_common.ErrorLog("TankFillingStnMgmt-M1", ex.Message.ToString());
		}
	}
	public void LoadFillingStation(DropDownList ddlFillingStation)
	{
		try
		{
			string officeid = Session["_LDepotCode"].ToString();
			ddlFillingStation.Items.Clear();
			MyCommand = new NpgsqlCommand();
			MyCommand.Parameters.Clear();
			MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_tank_filling_station");
			MyCommand.Parameters.AddWithValue("p_officeid", officeid);
			dt = bll.SelectAll(MyCommand);
			if (dt.TableName == "Success")
			{
				if (dt.Rows.Count > 0)
				{
					ddlFillingStation.DataSource = dt;
					ddlFillingStation.DataTextField = "fillingstn_name";
					ddlFillingStation.DataValueField = "fillingstn_id";
					ddlFillingStation.DataBind();
				}
			}
			else
			{
				_common.ErrorLog("TankFillingStnMgmt-M2", dt.TableName.ToString());
				Errormsg(dt.TableName);
			}
			ddlFillingStation.Items.Insert(0, "SELECT");
			ddlFillingStation.Items[0].Value = "0";
			ddlFillingStation.SelectedIndex = 0;
		}
		catch (Exception ex)
		{
			ddlFillingStation.Items.Insert(0, "SELECT");
			ddlFillingStation.Items[0].Value = "0";
			ddlFillingStation.SelectedIndex = 0;
			_common.ErrorLog("TankFillingStnMgmt-M2", ex.Message.ToString());
		}
	}
	public void loadTank(int fillingstn, DropDownList ddlTank)
	{
		try
		{
			ddlTank.Items.Clear();
			MyCommand = new NpgsqlCommand();
			MyCommand.Parameters.Clear();
			MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_tank");
			MyCommand.Parameters.AddWithValue("p_fillingstn", fillingstn);
			dt = bll.SelectAll(MyCommand);
			if (dt.TableName == "Success")
			{

				if (dt.Rows.Count > 0)
				{
					ddlTank.DataSource = dt;
					ddlTank.DataTextField = "tank_no";
					ddlTank.DataValueField = "tank_no";
					ddlTank.DataBind();
				}
			}
			else
			{
				_common.ErrorLog("TankFillingStnMgmt-M3", dt.TableName.ToString());
				Errormsg(dt.TableName);
			}

			ddlTank.Items.Insert(0, "SELECT");
			ddlTank.Items[0].Value = "0";
			ddlTank.SelectedIndex = 0;
		}
		catch (Exception ex)
		{
			ddlTank.Items.Insert(0, "SELECT");
			ddlTank.Items[0].Value = "0";
			ddlTank.SelectedIndex = 0;
			_common.ErrorLog("TankFillingStnMgmt-M3", ex.Message.ToString());
		}
	}
	public void gettankavailablefuel()
	{
		try
		{
			string tank = ddlTank.SelectedValue.ToString();
			MyCommand = new NpgsqlCommand();
			MyCommand.Parameters.Clear();
			MyCommand.Parameters.AddWithValue("@storedprocedure", "dutyallocation.f_get_tankavailablequantity");
			MyCommand.Parameters.AddWithValue("p_tankno", tank);
			dt = bll.SelectAll(MyCommand);
			if (dt.TableName == "Success")
			{
				tbAvailableQty.Text = dt.Rows[0]["availableqty"].ToString();
			}
			else
			{
				_common.ErrorLog("TankFillingStnMgmt-M4", dt.TableName.ToString());
			}
		}
		catch (Exception ex)
		{
			_common.ErrorLog("TankFillingStnMgmt-M4", ex.Message.ToString());
		}
	}
	public void ResetData()
	{
		lblAddRefuelingDetails.Visible = true;
		lblUpdateRefuelingDetails.Visible = false;
		lbtnSave.Visible = true;
		lbtnUpdate.Visible = false;
		ddlFillingStation.SelectedIndex = 0;
		ddlTank.SelectedIndex = 0;
		tbAvailableQty.Text = "";
		tbReceiptDate.Text = "";
		tbReceiptNumber.Text = "";
		tbRefuelledDate.Text = "";
		tbRefuelledQuantity.Text = "";
		hdnReFillingSTNID.Value = "0";
		tbTankerNo.Text = "";
		tbTemperature.Text = "";
		tbActualDensity.Text = "";
		tbCheckedDensity.Text = "";
	}
	protected bool IsValidValues()
	{
		try
		{
			int msgcnt = 0;
			string msg = "";
			if (ddlFillingStation.SelectedValue == "0")
			{
				msgcnt = msgcnt + 1;
				msg = msg + msgcnt.ToString() + ". Filling Station.<br>";
			}
			if (ddlTank.SelectedValue == "0")
			{
				msgcnt = msgcnt + 1;
				msg = msg + msgcnt.ToString() + ". Tank.<br>";
			}
			if (_validation.IsValidString(tbAvailableQty.Text.Trim(), 1, tbAvailableQty.MaxLength) == false)
			{
				msgcnt = msgcnt + 1;
				msg = msg + msgcnt.ToString() + ". Available Quantity.<br>";
			}
			if (tbAvailableQty.Text == "0")
			{
				msgcnt = msgcnt + 1;
				msg = msg + msgcnt.ToString() + ". Available Quantity.<br>";
			}
			if (_validation.IsValidString(tbRefuelledQuantity.Text.Trim(), 1, tbRefuelledQuantity.MaxLength) == false)
			{
				msgcnt = msgcnt + 1;
				msg = msg + msgcnt.ToString() + ". Refuelling Quantity.<br>";
			}
			if (tbRefuelledDate.Text.Length == 0 | tbRefuelledDate.Text.ToString() == "")
			{
				msgcnt = msgcnt + 1;
				msg = msg + msgcnt.ToString() + ". Refuelling Date.<br>";
			}

			if (tbReceiptDate.Text.Length > 0)
			{
				if (tbReceiptDate.Text.Length == 0 | tbReceiptDate.Text.ToString() == "")
				{
					msgcnt = msgcnt + 1;
					msg = msg + msgcnt.ToString() + ". Receipt Date.<br>";
				}

			}
			if (_validation.IsValidString(tbReceiptNumber.Text.Trim(), 1, tbReceiptNumber.MaxLength) == false)
			{
				msgcnt = msgcnt + 1;
				msg = msg + msgcnt.ToString() + ". Receipt Number.<br>";
			}
			if (_validation.IsValidString(tbTankerNo.Text.Trim(), 0, tbTankerNo.MaxLength) == false)
			{
				msgcnt = msgcnt + 1;
				msg = msg + msgcnt.ToString() + ". Tanker Number.<br>";
			}
			if (_validation.isValideDecimalNumber(tbTemperature.Text.Trim(), 0, tbTemperature.MaxLength) == false)
			{
				msgcnt = msgcnt + 1;
				msg = msg + msgcnt.ToString() + ". Enter  Decimal Values Only(Temperature).<br>";
			}
			if (_validation.isValideDecimalNumber(tbActualDensity.Text.Trim(), 0, tbActualDensity.MaxLength) == false)
			{
				msgcnt = msgcnt + 1;
				msg = msg + msgcnt.ToString() + ".Enter  Decimal Values Only(Actual Density).<br>";
			}
			if (_validation.isValideDecimalNumber(tbCheckedDensity.Text.Trim(), 0, tbCheckedDensity.MaxLength) == false)
			{
				msgcnt = msgcnt + 1;
				msg = msg + msgcnt.ToString() + ".Enter  Decimal Values Only(Checked Density).<br>";
			}
			if (msgcnt > 0)
			{
				Errormsg(msg);
				return false;
			}
			return true;
		}
		catch (Exception ex)
		{
			_common.ErrorLog("TankFillingStnMgmt-M5", ex.Message.ToString());
			return false;
		}
	}
	public bool IsValidPdf(FileUpload fileupload)
	{
		string _fileFormat = GetpdfMimeDataOfFile(fileupload.PostedFile);
		if (((_fileFormat == "application/pdf")))
		{
		}
		else
		{
			Errormsg("Invalid file (Not a PDF)");
			return false;
		}
		return true;
	}
	public void saveValues()
	{
		try
		{
			string officeid = Session["_LDepotCode"].ToString();
			string Maction = Session["Action"].ToString();
			int fillingst = Convert.ToInt16(ddlFillingStation.SelectedValue);
			string tank = ddlTank.SelectedValue.ToString();
			string reFillingSTNID = hdnReFillingSTNID.Value.ToString();
			string availableqty = tbAvailableQty.Text.ToString();
			string refuelledQuantity = tbRefuelledQuantity.Text.ToString();
			string receiptno = tbReceiptNumber.Text.ToString();
			string tankerno = tbTankerNo.Text.ToString();
			string actualdensity = tbActualDensity.Text.ToString();
            if (actualdensity == "")
            {
                actualdensity = "0";
            }
			string checkeddensity = tbCheckedDensity.Text.ToString();
            if (checkeddensity == "")
            {
                checkeddensity = "0";
            }
            string temperature = tbTemperature.Text.ToString();
			string receiptdate = tbReceiptDate.Text.ToString();
			string refuelleddate = tbRefuelledDate.Text.ToString();
			string ipaddress = HttpContext.Current.Request.UserHostAddress;
			string UpdatedBy = Session["_UserCode"].ToString();
			byte[] FileInvoice = (byte[])Session["pdf"];
			MyCommand = new NpgsqlCommand();
			MyCommand.Parameters.Clear();
			MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_insertupdate_dieselrefuelingtank");
			MyCommand.Parameters.AddWithValue("p_action", Maction);
			MyCommand.Parameters.AddWithValue("p_depot ", officeid);
			MyCommand.Parameters.AddWithValue("p_fillingstation", fillingst);
			MyCommand.Parameters.AddWithValue("p_tank", tank);
			MyCommand.Parameters.AddWithValue("p_refillingno ", reFillingSTNID);
			MyCommand.Parameters.AddWithValue("p_availablequantity ", availableqty);
			MyCommand.Parameters.AddWithValue("p_refilledquantity ", refuelledQuantity);
			MyCommand.Parameters.AddWithValue("p_refuelledondate", refuelleddate);
			MyCommand.Parameters.AddWithValue("p_receiptbigint", receiptno);
			MyCommand.Parameters.AddWithValue("p_receiptdate", receiptdate);
			MyCommand.Parameters.AddWithValue("p_tankerno", tankerno);
			MyCommand.Parameters.AddWithValue("p_actualdensity", actualdensity);
			MyCommand.Parameters.AddWithValue("p_checkeddensity", checkeddensity);
			MyCommand.Parameters.AddWithValue("p_temperature", temperature);
			MyCommand.Parameters.AddWithValue("p_updatedby", UpdatedBy);
			MyCommand.Parameters.AddWithValue("p_ipaddress", ipaddress);
			MyCommand.Parameters.AddWithValue("p_uploadedinvoice", (object)FileInvoice ?? DBNull.Value);
			dt = bll.SelectAll(MyCommand);
			if (dt.TableName == "Success")
			{
				Session["_tankReFillRefNo"] = dt.Rows[0]["sprefno"].ToString(); 
				Successmsg("Record has succefully been saved");
				Session["pdf"] = null;
				lblPDF.Text = "";
				lbtnInvoice.Visible = false;
				BindGridFillingStation();
				ResetData();
			}
			else
			{
				_common.ErrorLog("TankFillingStnMgmt-M6",dt.TableName.ToString());
				Errormsg("Error occurred while Updation. " + dt.TableName);
				return;
			}
		}
		catch (Exception ex)
		{
			_common.ErrorLog("TankFillingStnMgmt-M6", ex.Message.ToString());
			Errormsg("Error occurred while Updation. " + ex.Message);
			return;
		}
	}
	private void updatePDF()
	{
		try
		{
			if ((Session["pdf"] != null | Session["pdf"].ToString() == ""))
			{
				string saveDirectory = "Invoice";
				string fileName = Session["_tankReFillRefNo"].ToString() + ".pdf";
				string fileSavePath = Path.Combine(saveDirectory, fileName);
				byte[] invoice = (byte[])Session["pdf"];
				System.IO.File.WriteAllBytes(Server.MapPath(fileSavePath), invoice);
				Session["pdf"] = null;
				lblPDF.Text = "";
			}
		}
		catch (Exception ex)
		{
			_common.ErrorLog("TankFillingStnMgmt-M7", ex.Message.ToString());
			Errormsg("Error,while saving Uploaded File");
			return;
		}
	}
	public void getDocument(String docType)
	{
		try
		{
			byte[] FileInvoice = (byte[])Session["pdf"];
			string base64String = Convert.ToBase64String(FileInvoice, 0, FileInvoice.Length);
			Response.ContentType = "application/pdf";
			Response.AddHeader("Content-Disposition", "attachment;filename=" + hdnReFillingSTNID.Value.ToString() + ".pdf");
			Response.BinaryWrite(FileInvoice);
			Response.End();
			//ScriptManager.RegisterStartupScript(Page, Page.GetType(), "applicantDetailModel", "$('#applicantDetailModel').modal();", true);
		}
		catch (Exception ex)
		{
			_common.ErrorLog("TankFillingStnMgmt-M8", ex.Message.ToString());
		}
	}
	protected void InfoMsg(string msg)
	{
		string popup = _popup.modalPopupSmall("I", "Information", msg, "Close");
		Response.Write(popup);
	}
	protected void ConfirmMsg(string msg)
	{
		lblConfirmation.Text = msg;
		mpConfirmation.Show();
	}
	protected void Errormsg(string msg)
	{

		string popup = _popup.modalPopupSmall("D", "Please Check", msg, "Close");
		Response.Write(popup);
	}
	protected void Successmsg(string msg)
	{

		string popup = _popup.modalPopupSmall("S", "Congratulations", msg, "Close");
		Response.Write(popup);
	}
	public void BindGridFillingStation()
	{
		try
		{
			string officeid = Session["_LDepotCode"].ToString();
			pnlNoRecord.Visible = true;
			gvFillingStation.Visible = false;
			MyCommand = new NpgsqlCommand();
			MyCommand.Parameters.Clear();
			MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_refillingtank");
			MyCommand.Parameters.AddWithValue("p_officeid", officeid);
			dt = bll.SelectAll(MyCommand);
			if (dt.TableName == "Success")
			{
				if (dt.Rows.Count > 0)
				{
					gvFillingStation.DataSource = dt;
					gvFillingStation.DataBind();
					gvFillingStation.Visible = true;
					pnlNoRecord.Visible = false;
				}
			}
			else
			{
				_common.ErrorLog("TankFillingStnMgmt-M9",dt.TableName.ToString());
				Errormsg(dt.TableName);
			}
		}
		catch (Exception ex)
		{
			_common.ErrorLog("TankFillingStnMgmt-M9", ex.Message.ToString());
			gvFillingStation.Visible = false;
		}
	}
	protected void ExportToExcel()
	{
		Response.Clear();
		Response.Buffer = true;
		gvFillingStation.AllowPaging = false;
		this.BindGridFillingStation();
		if (gvFillingStation.Rows.Count > 0)
		{
			Response.AddHeader("content-disposition", "attachment;filename=RefuelingTankEntry.xls");
			Response.Charset = "";
			Response.ContentType = "application/vnd.ms-excel";
			using (StringWriter sw = new StringWriter())
			{
				HtmlTextWriter hw = new HtmlTextWriter(sw);

				// To Export all pages

				gvFillingStation.HeaderRow.BackColor = Color.White;
				foreach (TableCell cell in gvFillingStation.HeaderRow.Cells)
					cell.BackColor = gvFillingStation.HeaderStyle.BackColor;
				foreach (GridViewRow row in gvFillingStation.Rows)
				{
					row.BackColor = Color.White;
					foreach (TableCell cell in row.Cells)
					{
						if (row.RowIndex % 2 == 0)
							cell.BackColor = gvFillingStation.AlternatingRowStyle.BackColor;
						else
							cell.BackColor = gvFillingStation.RowStyle.BackColor;
						cell.CssClass = "textmode";
					}
				}

				gvFillingStation.RenderControl(hw);
				// style to format numbers to string
				string style = "<style> .textmode { } </style>";
				Response.Write(style);
				Response.Output.Write(sw.ToString());
				Response.Flush();
				Response.End();
			}
		}
		else
		{
			Errormsg("Sorry, no record is available.");
			return;
		}
	}
	public override void VerifyRenderingInServerForm(Control control)
	{
	}
	public static string GetpdfMimeDataOfFile(HttpPostedFile file)
	{
		IntPtr mimeout = default(IntPtr);
		int MaxContent = Convert.ToInt32(file.ContentLength);
		if (MaxContent > 4096)
		{
			MaxContent = 4096;
		}

		byte[] buf = new byte[MaxContent - 1 + 1];
		file.InputStream.Read(buf, 0, MaxContent);
		int MimeSampleSize = 256;


		string mimeType = System.Web.MimeMapping.GetMimeMapping(file.FileName);
		return mimeType;
	}
	#endregion

	#region Events
	protected void lbtnViewInstruction_Click(object sender, EventArgs e)
	{
		string msg = "";
		msg = msg + "1.Tank refueling details are entered here.<br/>";
		msg = msg + "2. Refueling quantity shoundn't be greater than tha tank capacity or available quantity.<br/>";
		InfoMsg(msg);
	}
	protected void lbtnSave_Click(object sender, EventArgs e)
	{
		if (IsValidValues() == false)
		{
			return;
		}
		Session["Action"] = "S";
		ConfirmMsg("Do you want to save refuelling tank details?");
	}
	protected void lbtnUpdate_Click(object sender, EventArgs e)
	{
		if (IsValidValues() == false)
		{ return; }
		Session["Action"] = "U";
		ConfirmMsg("Do you want to update refilling tank details?");
	}
	protected void lbtnReset_Click(object sender, EventArgs e)
	{
		ResetData();
	}
	protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
	{
		saveValues();
	}
	protected void gvFillingStation_RowCommand(object sender, GridViewCommandEventArgs e)
	{
		ResetData();
		int index = Convert.ToInt32(e.CommandArgument);
		GridViewRow row = gvFillingStation.Rows[index];
		string StationID = "";
		if (e.CommandName == "gvEdit")
		{
			try
			{
				ddlFillingStation.SelectedValue = gvFillingStation.DataKeys[index].Values["fillingstationid"].ToString();
				this.ddlFillingStation_SelectedIndexChanged(this.ddlFillingStation, System.EventArgs.Empty);
			}
			catch (Exception ex)
			{

			}
			ddlTank.SelectedValue = gvFillingStation.DataKeys[index].Values["tankno"].ToString();
			tbAvailableQty.Text = gvFillingStation.DataKeys[index].Values["availableqty"].ToString();
			hdnReFillingSTNID.Value = gvFillingStation.DataKeys[index].Values["refuelingid"].ToString();
			tbRefuelledQuantity.Text = gvFillingStation.DataKeys[index].Values["refuelledqty"].ToString();
			tbRefuelledDate.Text = gvFillingStation.DataKeys[index].Values["refuelleddate"].ToString();
			tbReceiptNumber.Text = gvFillingStation.DataKeys[index].Values["receiptnumber"].ToString();
			tbReceiptDate.Text = gvFillingStation.DataKeys[index].Values["receiptdate"].ToString();
			tbTankerNo.Text = gvFillingStation.DataKeys[index].Values["tankernumber"].ToString();
			tbTemperature.Text = gvFillingStation.DataKeys[index].Values["temperature"].ToString();
			tbActualDensity.Text = gvFillingStation.DataKeys[index].Values["actualdensity"].ToString();
			tbCheckedDensity.Text = gvFillingStation.DataKeys[index].Values["checkeddensity"].ToString();
			if (gvFillingStation.DataKeys[index].Values["uploadedinvoice"].ToString() != "" && gvFillingStation.DataKeys[index].Values["uploadedinvoice"].ToString() != null)
			{
				byte[] bytes = (byte[])gvFillingStation.DataKeys[index].Values["uploadedinvoice"];
				Session["pdf"] = bytes;
				lbtnInvoice.Visible = true;
				lbtnInvoice.Text = gvFillingStation.DataKeys[index].Values["refuelingid"].ToString() + ".pdf";
			}
			lbtnSave.Visible = false;
			lbtnUpdate.Visible = true;
		}
	}
	protected void gvFillingStation_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		gvFillingStation.PageIndex = e.NewPageIndex;
		BindGridFillingStation();
	}
	protected void ddlFillingStation_SelectedIndexChanged(object sender, EventArgs e)
	{
		loadTank(Convert.ToInt16(ddlFillingStation.SelectedValue.ToString()), ddlTank);
	}
	protected void viewdoc_click(object sender, EventArgs e)
	{
		LinkButton btn = (LinkButton)(sender);
		var id = btn.CommandArgument;
		getDocument(id);
	}
	protected void ddlTank_SelectedIndexChanged(object sender, EventArgs e)
	{
		gettankavailablefuel();
	}
	protected void lbtnExport_Click(object sender, EventArgs e)
	{
		ExportToExcel();
	}
	protected void btnUploadpdf_Click(object sender, EventArgs e)
	{
		if (IsValidPdf(fudocfile) == true)
		{
			if (fudocfile.HasFile == true)
			{
				if (Convert.ToInt32(fudocfile.FileBytes.Length) < 2097152 & fudocfile.FileName.Length > 2)
				{
					if (fudocfile.FileName.Length <= 50)
					{
						Session["pdf"] = fudocfile.FileBytes;
						lblPDF.Text = fudocfile.FileName;
						Session["pdfName"] = fudocfile.FileName;
					}
				}
				else
				{
					Errormsg("Please select file less than 2 MB");
				}
			}
		}
	}
	#endregion
}