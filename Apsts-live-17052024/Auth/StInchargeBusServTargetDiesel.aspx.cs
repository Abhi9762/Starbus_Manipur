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


public partial class Auth_StInchargeBusServTargetDiesel : BasePage
{
	sbValidation _validation = new sbValidation();
	private NpgsqlCommand MyCommand;
	DataTable dt = new DataTable();
	private sbBLL bll = new sbBLL();
	private sbCommonFunc _common = new sbCommonFunc();
	sbLoaderNdPopup _popup = new sbLoaderNdPopup();
	protected void Page_Load(object sender, EventArgs e)
	{
		lblSummary.Text = "Summary as on Date " + DateTime.Now.ToString("dd/MM/yyyy hh:ss tt");
		Session["_moduleName"] = "Bus Service Target Diesel Income Entry";
		if (!IsPostBack)
		{
			Session["_entryStatus"] = "O";
			lblHead.Text = "Add Entry Details";
			loadTDServicesummary();
			loadTDServices();
			loadServiceType();
			LoadOwnershipType();
			LoadBusLayout();
			loadRoutes();
			loadBusLoadFactors();
			loadWheelBase();
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

    #region "methods"
    public void loadServiceType()
	{
		try
		{
			ddlServiceType.Items.Clear();

			MyCommand = new NpgsqlCommand();
			MyCommand.Parameters.Clear();
			MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_servicetype");

			dt = bll.SelectAll(MyCommand);
			if (dt.Rows.Count > 0)
			{
				ddlServiceType.DataSource = dt;
				ddlServiceType.DataTextField = "service_type_nameen";
				ddlServiceType.DataValueField = "srtpid";
				ddlServiceType.DataBind();
			}
			ddlServiceType.Items.Insert(0, "SELECT");
			ddlServiceType.Items[0].Value = "0";
			ddlServiceType.SelectedIndex = 0;
		}
		catch (Exception ex)
		{
			ddlServiceType.Items.Insert(0, "SELECT");
			ddlServiceType.Items[0].Value = "0";
			ddlServiceType.SelectedIndex = 0;
			_common.ErrorLog("SysAdmBusRegistration-M11", ex.Message.ToString());
		}
	}
	public void LoadOwnershipType()
	{
		try
		{
			ddlBusOwnerType.Items.Clear();

			MyCommand = new NpgsqlCommand();
			MyCommand.Parameters.Clear();
			MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_bustype");

			dt = bll.SelectAll(MyCommand);
			if (dt.Rows.Count > 0)
			{
				ddlBusOwnerType.DataSource = dt;
				ddlBusOwnerType.DataTextField = "bustype_name";
				ddlBusOwnerType.DataValueField = "bustype_id";
				ddlBusOwnerType.DataBind();
			}
			ddlBusOwnerType.Items.Insert(0, "SELECT");
			ddlBusOwnerType.Items[0].Value = "0";
			ddlBusOwnerType.SelectedIndex = 0;
		}
		catch (Exception ex)
		{
			ddlBusOwnerType.Items.Insert(0, "SELECT");
			ddlBusOwnerType.Items[0].Value = "0";
			ddlBusOwnerType.SelectedIndex = 0;
			_common.ErrorLog("SysAdmBusRegistation-M6", ex.Message.ToString());
		}
	}
	public void LoadBusLayout()
	{
		try
		{
			ddlLayout.Items.Clear();

			MyCommand = new NpgsqlCommand();
			MyCommand.Parameters.Clear();
			MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_buslayout");

			dt = bll.SelectAll(MyCommand);
			if (dt.Rows.Count > 0)
			{
				ddlLayout.DataSource = dt;
				ddlLayout.DataTextField = "layoutname";
				ddlLayout.DataValueField = "layoutcode";
				ddlLayout.DataBind();
			}
			ddlLayout.Items.Insert(0, "SELECT");
			ddlLayout.Items[0].Value = "0";
			ddlLayout.SelectedIndex = 0;
		}
		catch (Exception ex)
		{
			ddlLayout.Items.Insert(0, "SELECT");
			ddlLayout.Items[0].Value = "0";
			ddlLayout.SelectedIndex = 0;
			_common.ErrorLog("SysAdmBusRegistration-M8", ex.Message.ToString());
		}

	}
	public void loadRoutes()//M4
	{
		try
		{
			ddlRoute.Items.Clear();
			MyCommand = new NpgsqlCommand();
			MyCommand.Parameters.Clear();
			MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_routes");
			dt = bll.SelectAll(MyCommand);
			if (dt.Rows.Count > 0)
			{
				ddlRoute.DataSource = dt;
				ddlRoute.DataTextField = "routename";
				ddlRoute.DataValueField = "routeid";
				ddlRoute.DataBind();
			}
			ddlRoute.Items.Insert(0, "SELECT");
			ddlRoute.Items[0].Value = "0";
			ddlRoute.SelectedIndex = 0;
		}
		catch (Exception ex)
		{
			ddlRoute.Items.Insert(0, "SELECT");
			ddlRoute.Items[0].Value = "0";
			ddlRoute.SelectedIndex = 0;
			_common.ErrorLog("DS mgmt-M4", ex.Message.ToString());
		}
	}
	public void loadDepotServices()//M4
	{
		try
		{
			int servicetype = Convert.ToInt16(ddlServiceType.SelectedValue);
			int routeid = Convert.ToInt16(ddlRoute.SelectedValue);
			string officeid = Session["_LDepotCode"].ToString();
			string bustype = ddlBusOwnerType.SelectedValue.ToString();
			ddlDepotService.Items.Clear();
			MyCommand = new NpgsqlCommand();
			MyCommand.Parameters.Clear();
			MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_getdepotservicesfortargetdiesel");
			MyCommand.Parameters.AddWithValue("p_depotid", officeid);
			MyCommand.Parameters.AddWithValue("p_sericetype", servicetype);
			MyCommand.Parameters.AddWithValue("p_routeid", routeid);
			MyCommand.Parameters.AddWithValue("p_bustype", bustype);
			dt = bll.SelectAll(MyCommand);
			//Errormsg(dt.TableName);
			if (dt.Rows.Count > 0)
			{
				ddlDepotService.DataSource = dt;
				ddlDepotService.DataTextField = "service_name";
				ddlDepotService.DataValueField = "dsvc_id";
				ddlDepotService.DataBind();
			}
			ddlDepotService.Items.Insert(0, "SELECT");
			ddlDepotService.Items[0].Value = "0";
			ddlDepotService.SelectedIndex = 0;
		}
		catch (Exception ex)
		{
			ddlDepotService.Items.Insert(0, "SELECT");
			ddlDepotService.Items[0].Value = "0";
			ddlDepotService.SelectedIndex = 0;
			_common.ErrorLog("DS mgmt-M4", ex.Message.ToString());
		}
	}
	public void loadSeatingCapacity()
	{
		try
		{
			tbSeatingCapacity.Text = "0";
			int layout = Convert.ToInt16(ddlLayout.SelectedValue.ToString());
			MyCommand = new NpgsqlCommand();
			MyCommand.Parameters.Clear();
			MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_busseatingcapacity");
			MyCommand.Parameters.AddWithValue("p_layoutcode", layout);
			dt = bll.SelectAll(MyCommand);
			if (dt.Rows.Count > 0)
			{
				tbSeatingCapacity.Text = dt.Rows[0]["total_seats_forbooking"].ToString();
			}
		}
		catch (Exception ex)
		{
		}
	}
	public void loadBusLoadFactors()
	{
		try
		{
			ddlLoadFactor.Items.Clear();
			MyCommand = new NpgsqlCommand();
			MyCommand.Parameters.Clear();
			MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_busloadfactor");
			dt = bll.SelectAll(MyCommand);
			if (dt.Rows.Count > 0)
			{
				ddlLoadFactor.DataSource = dt;
				ddlLoadFactor.DataTextField = "loadfactor";
				ddlLoadFactor.DataValueField = "loadfactorid";
				ddlLoadFactor.DataBind();
			}
			ddlLoadFactor.Items.Insert(0, "-SELECT-");
			ddlLoadFactor.Items[0].Value = "0";
			ddlLoadFactor.SelectedIndex = 0;
		}
		catch (Exception ex)
		{
			ddlLoadFactor.Items.Insert(0, "-SELECT-");
			ddlLoadFactor.Items[0].Value = "0";
			ddlLoadFactor.SelectedIndex = 0;
		}
	}
	public void loadWheelBase()
	{
		try
		{
			ddlWheelBase.Items.Clear();
			MyCommand = new NpgsqlCommand();
			MyCommand.Parameters.Clear();
			MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_buswheelbase");
			dt = bll.SelectAll(MyCommand);
			if (dt.Rows.Count > 0)
			{
				ddlWheelBase.DataSource = dt;
				ddlWheelBase.DataTextField = "wheelbase";
				ddlWheelBase.DataValueField = "wheelbaseid";
				ddlWheelBase.DataBind();
			}
			ddlWheelBase.Items.Insert(0, "-SELECT-");
			ddlWheelBase.Items[0].Value = "0";
			ddlWheelBase.SelectedIndex = 0;
		}
		catch (Exception ex)
		{
			ddlWheelBase.Items.Insert(0, "-SELECT-");
			ddlWheelBase.Items[0].Value = "0";
			ddlWheelBase.SelectedIndex = 0;
		}
	}
	public void loadTDServicesummary()
	{
		try
		{
			string officeid = Session["_LDepotCode"].ToString();
			string status = Session["_entryStatus"].ToString();
			pnlnoRecordfound.Visible = true;
			gvFillingStation.Visible = false;
			MyCommand = new NpgsqlCommand();
			MyCommand.Parameters.Clear();
			MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_bustargetincomesummary");
			MyCommand.Parameters.AddWithValue("p_depotcode", officeid);
			MyCommand.Parameters.AddWithValue("p_status", status);
			dt = bll.SelectAll(MyCommand);
			if (dt.TableName == "Success")
			{
				lbtnTotalService.Text = dt.Rows[0]["totalservice"].ToString();
				lbtnTotalActiveEntries.Text = dt.Rows[0]["activeentry"].ToString();
				lbtnToExpireEntries.Text = dt.Rows[0]["toexpireentry"].ToString();
				lbtnTotalExpireEntries.Text = dt.Rows[0]["expiredentry"].ToString();
				lbtnTotalActiveEntries.Enabled = true;
				lbtnToExpireEntries.Enabled = true;
				lbtnTotalExpireEntries.Enabled = true;
				if (lbtnTotalActiveEntries.Text == "0")
				{
					lbtnTotalActiveEntries.Enabled = false;
				}
				if (lbtnToExpireEntries.Text == "0")
				{
					lbtnToExpireEntries.Enabled = false;
				}
				if (lbtnTotalExpireEntries.Text == "0")
				{
					lbtnTotalExpireEntries.Enabled = false;
				}
			}
		}
		catch (Exception ex)
		{
		}
	}
	public void loadTDServices()
	{
		try
		{
			string officeid = Session["_LDepotCode"].ToString();
			string status = Session["_entryStatus"].ToString();
			string searchtext = tbSearchDepotService.Text.ToString();
			pnlnoRecordfound.Visible = true;
			gvFillingStation.Visible = false;
			MyCommand = new NpgsqlCommand();
			MyCommand.Parameters.Clear();
			MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_getbustargetincome");
			MyCommand.Parameters.AddWithValue("p_depotcode", officeid);
			MyCommand.Parameters.AddWithValue("p_status", status);
			MyCommand.Parameters.AddWithValue("p_searchtext", searchtext);
			dt = bll.SelectAll(MyCommand);
			if (dt.TableName == "Success")
			{
				if (dt.Rows.Count > 0)
				{
					gvFillingStation.DataSource = dt;
					gvFillingStation.DataBind();
					pnlnoRecordfound.Visible = false;
					gvFillingStation.Visible = true;
					divNoEntry.Visible = true;
					divnosearchrecord.Visible = false;
				}
				else
				{
					divNoEntry.Visible = false;
					divnosearchrecord.Visible = true;
				}
			}
		}
		catch (Exception ex)
		{
		}
	}
	public void resetdata()
	{
		ddlRoute.SelectedIndex = 0;
		this.ddlRoute_SelectedIndexChanged(this.ddlRoute, System.EventArgs.Empty);
		ddlServiceType.SelectedIndex = 0;
		ddlBusOwnerType.SelectedIndex = 0;
		ddlDepotService.SelectedIndex = 0;
		ddlLayout.SelectedIndex = 0;
		ddlLoadFactor.SelectedIndex = 0;
		ddlWheelBase.SelectedIndex = 0;
		tbSeatingCapacity.Text = "";
		tbTargetIncome.Text = "";
		tbTargetIncomePerKm.Text = "";
		tbDieselAvgHill.Text = "";
		tbDieselAvgPlain.Text = "";
		tbValidFrom.Text = "";
		tbValidTo.Text = "";
		lblHead.Text = "Add Entry Details";
		lbtnSave.Visible = true;
		lbtnUpdate.Visible = false;
		lbtnCancel.Visible = false;
		lbtnReset.Visible = true;
		pnlAddDetails.Visible = true;
		pnlViewDetails.Visible = false;
	}
	private bool IsValidValues()
	{
		try
		{
			int msgcnt = 0;
			string msg = "";
			if (ddlServiceType.SelectedValue == "0")
			{
				msgcnt = msgcnt + 1;
				msg = msg + msgcnt.ToString() + ". Bus Service Type.<br>";
			}
			if (ddlRoute.SelectedValue == "0")
			{
				msgcnt = msgcnt + 1;
				msg = msg + msgcnt.ToString() + ". Route.<br>";
			}
			if (ddlBusOwnerType.SelectedValue == "0")
			{
				msgcnt = msgcnt + 1;
				msg = msg + msgcnt.ToString() + ". Bus Owner type.<br>";
			}
			if (ddlDepotService.SelectedValue == "0")
			{
				msgcnt = msgcnt + 1;
				msg = msg + msgcnt.ToString() + ". Depot Service.<br>";
			}
			if (ddlLayout.SelectedValue == "0")
			{
				msgcnt = msgcnt + 1;
				msg = msg + msgcnt.ToString() + ". Layout.<br>";
			}
			if ((_validation.IsValidString(tbSeatingCapacity.Text, 1, tbSeatingCapacity.MaxLength)) == false)
			{
				msgcnt = msgcnt + 1;
				msg = msg + msgcnt.ToString() + ". Seating Capacity.<br>";
			}
			if (ddlLoadFactor.SelectedValue == "0")
			{
				msgcnt = msgcnt + 1;
				msg = msg + msgcnt.ToString() + ". Load Factor.<br>";
			}
			if (ddlWheelBase.SelectedValue == "0")
			{
				msgcnt = msgcnt + 1;
				msg = msg + msgcnt.ToString() + ". Wheelbase.<br>";
			}
			if ((_validation.IsValidString(tbTargetIncomePerKm.Text, 1, tbTargetIncomePerKm.MaxLength)) == false)
			{
				msgcnt = msgcnt + 1;
				msg = msg + msgcnt.ToString() + ". Income Per KiloMeter (IPKM).<br>";
			}
			if ((_validation.IsValidString(tbTargetIncome.Text, 1, tbTargetIncome.MaxLength)) == false)
			{
				msgcnt = msgcnt + 1;
				msg = msg + msgcnt.ToString() + ". Income.<br>";
			}
			if (_validation.IsValidString(tbDieselAvgHill.Text, 1, tbDieselAvgHill.MaxLength) == false)
			{
				msgcnt = msgcnt + 1;
				msg = msg + msgcnt.ToString() + ". Diesel Average Hill.<br>";
			}
			if (_validation.IsValidString(tbDieselAvgPlain.Text, 1, tbDieselAvgPlain.MaxLength) == false)
			{
				msgcnt = msgcnt + 1;
				msg = msg + msgcnt.ToString() + ". Diesel Average Plain.<br>";
			}
			if (_validation.IsDate(tbValidFrom.Text) == false)
			{
				msgcnt++;
				msg = msg + msgcnt + ".  Enter Valid From Date<br/>";
			}
			if (_validation.IsDate(tbValidTo.Text) == false)
			{
				msgcnt++;
				msg = msg + msgcnt + ". Enter Valid To Date <br/>";
			}
			if (_validation.IsDate(tbValidFrom.Text) == true)
			{
				DateTime receivedOnDate = DateTime.ParseExact(tbValidFrom.Text, "dd/MM/yyyy", null);
				if (receivedOnDate > DateTime.Now.Date)
				{
					msgcnt = msgcnt + 1;
					msg = msg + msgcnt.ToString() + ". Valid From Date should be less than today's date.<br/>";
				}
			}
			if (_validation.IsDate(tbValidFrom.Text) == true && _validation.IsDate(tbValidTo.Text) == true)
			{
				DateTime validfromdate = DateTime.ParseExact(tbValidFrom.Text, "dd/MM/yyyy", null);
				DateTime validtodate = DateTime.ParseExact(tbValidTo.Text, "dd/MM/yyyy", null);
				if (validfromdate > validtodate)
				{
					msgcnt = msgcnt + 1;
					msg = msg + msgcnt.ToString() + ". Valid From should be less than Valid To Date.<br/>";
				}
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
			return false;
		}
	}
	public void saveValues()
	{
		try
		{
			string officeid = Session["_LDepotCode"].ToString();

			int routeid = Convert.ToInt16(ddlRoute.SelectedValue);
			int servicetype = Convert.ToInt16(ddlServiceType.SelectedValue);
            string busownertype = ddlBusOwnerType.SelectedValue;
			int depotService = Convert.ToInt16(ddlDepotService.SelectedValue.ToString());
			int layout = Convert.ToInt16(ddlLayout.SelectedValue.ToString());
			int seatingcapacity = Convert.ToInt16(tbSeatingCapacity.Text.ToString());
			int loadfactor = Convert.ToInt16(ddlLoadFactor.SelectedValue.ToString());
			int wheelbase = Convert.ToInt16(ddlWheelBase.SelectedValue.ToString());
			string targetincome = tbTargetIncome.Text.ToString();
			string targetincomeperkm = tbTargetIncomePerKm.Text.ToString();
			string dieselavghill = tbDieselAvgHill.Text.ToString();
			string dieselavgplain = tbDieselAvgPlain.Text.ToString();
			string validfrom = tbValidFrom.Text.ToString();
			string validto = tbValidTo.Text.ToString();
			string ipaddress = HttpContext.Current.Request.UserHostAddress;
			string UpdatedBy = Session["_UserCode"].ToString();
			MyCommand = new NpgsqlCommand();
			MyCommand.Parameters.Clear();

			MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_insertupdate_bustargetincome");
			MyCommand.Parameters.AddWithValue("p_officecode", officeid);
			MyCommand.Parameters.AddWithValue("p_routecode", routeid);
			MyCommand.Parameters.AddWithValue("p_serviceownertype", busownertype);
			MyCommand.Parameters.AddWithValue("p_bustype", servicetype);
			MyCommand.Parameters.AddWithValue("p_dsvc_id", depotService);
			MyCommand.Parameters.AddWithValue("p_buslayoutcode", layout);
			MyCommand.Parameters.AddWithValue("p_buswheelbase", wheelbase);
			MyCommand.Parameters.AddWithValue("p_loadfactor", loadfactor);
			MyCommand.Parameters.AddWithValue("p_buscapacity", seatingcapacity);
			MyCommand.Parameters.AddWithValue("p_ipkm", targetincomeperkm);
			MyCommand.Parameters.AddWithValue("p_income", targetincome);
			MyCommand.Parameters.AddWithValue("p_dieselaverage_hill", dieselavghill);
			MyCommand.Parameters.AddWithValue("p_dieselaverage_plain", dieselavgplain);
			MyCommand.Parameters.AddWithValue("p_validityfrom", validfrom);
			MyCommand.Parameters.AddWithValue("p_validityto", validto);
			MyCommand.Parameters.AddWithValue("p_updatedby", UpdatedBy);
			MyCommand.Parameters.AddWithValue("p_ipaddress", ipaddress);
			dt = bll.SelectAll(MyCommand);
			if (dt.TableName == "Success")
			{
				string result = dt.Rows[0]["p_result"].ToString();
				if (result == "EXCEPTION")
				{
					Errormsg("Error occurred while Updation.");
					return;
				}
				else
				{
					loadTDServices();
					resetdata();
 loadTDServicesummary();
					Successmsg("Record has succefully saved");
				}
			}
			else
			{
				Errormsg("Error occurred while Updation. " + dt.TableName);
				return;
			}
		}
		catch (Exception ex)
		{
			Errormsg("Error occurred while Updation. " + ex.Message);
			return;
		}
	}

	#endregion
	
	#region "Events"
	protected void lbtnTotalExpireEntries_Click(object sender, EventArgs e)
	{
		LinkButton btn = (LinkButton)sender;
		string status = btn.CommandArgument;
		Session["_entryStatus"] = status;
		loadTDServices();
		//if (status == "O")
		//{
		//	lblheadServiceTDEntry.InnerText = "List of services whose entries are done";
		//}
		//else if (status == "T")
		//{
		//	lblheadServiceTDEntry.InnerText = "List of services whose entries are going to expire";
		//}
		//else if (status == "E")
		//{
		//	lblheadServiceTDEntry.InnerText = "List of services whose entries are expired";
		//}
	}
	protected void lbtnSearchDepotService_Click(object sender, EventArgs e)
	{
		loadTDServices();
	}
	protected void lbtnRestSearchDepotService_Click(object sender, EventArgs e)
	{
		tbSearchDepotService.Text = "";
		loadTDServices();
	}
	protected void gvFillingStation_RowCommand(object sender, GridViewCommandEventArgs e)
	{
		try
		{
			int index = Convert.ToInt32(e.CommandArgument);
			GridViewRow row = gvFillingStation.Rows[index];
			if (e.CommandName == "updateTD")
			{
				lblHead.Text = "Update Entry Details";
				if (gvFillingStation.DataKeys[index].Values["routeid"].ToString() != "")
				{
					ddlRoute.SelectedValue = gvFillingStation.DataKeys[index].Values["routeid"].ToString();
				}
				if (gvFillingStation.DataKeys[index].Values["srtp_id"].ToString() != "")
				{
					ddlServiceType.SelectedValue = gvFillingStation.DataKeys[index].Values["srtp_id"].ToString();
				}
				if (gvFillingStation.DataKeys[index].Values["serviceownertype"].ToString() != "")
				{
					ddlBusOwnerType.SelectedValue = gvFillingStation.DataKeys[index].Values["serviceownertype"].ToString();
					this.ddlRoute_SelectedIndexChanged(this.ddlRoute, System.EventArgs.Empty);
				}
				if (gvFillingStation.DataKeys[index].Values["dsvc_id"].ToString() != "")
				{
					ddlDepotService.SelectedValue = gvFillingStation.DataKeys[index].Values["dsvc_id"].ToString();
				}
				if (gvFillingStation.DataKeys[index].Values["buslayoutcode"].ToString() != "")
				{
					ddlLayout.SelectedValue = gvFillingStation.DataKeys[index].Values["buslayoutcode"].ToString();
				}
				if (gvFillingStation.DataKeys[index].Values["buswheelbase"].ToString() != "")
				{
					ddlWheelBase.SelectedValue = gvFillingStation.DataKeys[index].Values["buswheelbase"].ToString();
				}
				if (gvFillingStation.DataKeys[index].Values["busloadfactor"].ToString() != "")
				{
					ddlLoadFactor.SelectedValue = gvFillingStation.DataKeys[index].Values["busloadfactor"].ToString();
				}
				if (gvFillingStation.DataKeys[index].Values["busseatingcapacity"].ToString() != "")
				{
					tbSeatingCapacity.Text = gvFillingStation.DataKeys[index].Values["busseatingcapacity"].ToString();
				}
				if (gvFillingStation.DataKeys[index].Values["targetincomeperkm"].ToString() != "")
				{
					tbTargetIncomePerKm.Text = gvFillingStation.DataKeys[index].Values["targetincomeperkm"].ToString();
				}
				if (gvFillingStation.DataKeys[index].Values["targetincome"].ToString() != "")
				{
					tbTargetIncome.Text = gvFillingStation.DataKeys[index].Values["targetincome"].ToString();
				}
				if (gvFillingStation.DataKeys[index].Values["tdieselaverage_hill"].ToString() != "")
				{
					tbDieselAvgHill.Text = gvFillingStation.DataKeys[index].Values["tdieselaverage_hill"].ToString();
				}
				if (gvFillingStation.DataKeys[index].Values["tdieselaverage_plain"].ToString() != "")
				{
					tbDieselAvgPlain.Text = gvFillingStation.DataKeys[index].Values["tdieselaverage_plain"].ToString();
				}
				if (gvFillingStation.DataKeys[index].Values["validityfrom"].ToString() != "")
				{
					tbValidFrom.Text = gvFillingStation.DataKeys[index].Values["validityfrom"].ToString();
				}
				if (gvFillingStation.DataKeys[index].Values["validityto"].ToString() != "")
				{
					tbValidTo.Text = gvFillingStation.DataKeys[index].Values["validityto"].ToString();
				}
				lbtnSave.Visible = false;
				lbtnUpdate.Visible = true;
				lbtnCancel.Visible = true;
				lbtnReset.Visible = false;
				pnlAddDetails.Visible = true;
				pnlViewDetails.Visible = false;
			}
			else if (e.CommandName == "viewTD")
			{
				if (gvFillingStation.DataKeys[index].Values["service_name_en"].ToString() != "")
				{
					lblViewServicehead.Text = gvFillingStation.DataKeys[index].Values["service_name_en"].ToString();
				}
				else
				{
					lblViewServicehead.Text = "Bus Service Target Diesel Entry Details";
				}
				if (gvFillingStation.DataKeys[index].Values["layout_name"].ToString() != "")
				{
					lblLayout.Text = gvFillingStation.DataKeys[index].Values["layout_name"].ToString();
				}
				if (gvFillingStation.DataKeys[index].Values["wheelbase"].ToString() != "")
				{
					lblWheelbase.Text = gvFillingStation.DataKeys[index].Values["wheelbase"].ToString();
				}
				if (gvFillingStation.DataKeys[index].Values["loadfactor"].ToString() != "")
				{
					lblLoadFactor.Text = gvFillingStation.DataKeys[index].Values["loadfactor"].ToString();
				}
				if (gvFillingStation.DataKeys[index].Values["busseatingcapacity"].ToString() != "")
				{
					lblSeatingCapacity.Text = gvFillingStation.DataKeys[index].Values["busseatingcapacity"].ToString();
				}
				if (gvFillingStation.DataKeys[index].Values["targetincomeperkm"].ToString() != "")
				{
					lblTargetIncomePerKm.Text = gvFillingStation.DataKeys[index].Values["targetincomeperkm"].ToString();
				}
				if (gvFillingStation.DataKeys[index].Values["targetincome"].ToString() != "")
				{
					lblTargetIncome.Text = gvFillingStation.DataKeys[index].Values["targetincome"].ToString();
				}
				if (gvFillingStation.DataKeys[index].Values["tdieselaverage_hill"].ToString() != "")
				{
					lblDieselAvgHill.Text = gvFillingStation.DataKeys[index].Values["tdieselaverage_hill"].ToString();
				}
				if (gvFillingStation.DataKeys[index].Values["tdieselaverage_plain"].ToString() != "")
				{
					lblDieselAvgPlain.Text = gvFillingStation.DataKeys[index].Values["tdieselaverage_plain"].ToString();
				}
				if (gvFillingStation.DataKeys[index].Values["validityfrom"].ToString() != "")
				{
					lblValidFromDate.Text = gvFillingStation.DataKeys[index].Values["validityfrom"].ToString();
				}
				if (gvFillingStation.DataKeys[index].Values["validityto"].ToString() != "")
				{
					lblValidToDate.Text = gvFillingStation.DataKeys[index].Values["validityto"].ToString();
				}
				pnlAddDetails.Visible = false;
				pnlViewDetails.Visible = true;
			}
		}
		catch (Exception ex)
		{
			Errormsg(ex.Message);
		}
	}
	protected void ddlRoute_SelectedIndexChanged(object sender, EventArgs e)
	{
		loadDepotServices();
	}
	protected void ddlLayout_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (ddlLayout.SelectedIndex > 0)
			loadSeatingCapacity();
	}
	protected void saveDetails_Click(object sender, EventArgs e)
	{
		if (IsValidValues() == false)
		{
			return;
		}
		ConfirmMsg("Do you want to save details?");
	}
	protected void updateDetails_Click(object sender, EventArgs e)
	{
		if (IsValidValues() == false)
		{
			return;
		}
		ConfirmMsg("Do you want to update details?");

	}
	protected void resetDetails_Click(object sender, EventArgs e)
	{
		resetdata();
	}
	protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
	{
		saveValues();
	}
	protected void lbtnViewInstruction_Click(object sender, EventArgs e)
	{
		string msg = "Coming Soon";
		//msg = msg + "1.Tank refueling details are entered here.<br/>";
		//msg = msg + "2. Refueling quantity shoundn't be greater than tha tank capacity or available quantity.<br/>";
		InfoMsg(msg);
	}
	#endregion

}