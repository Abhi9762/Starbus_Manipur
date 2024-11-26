using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Npgsql;
using System.IO;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_DepBusServiceMapping : System.Web.UI.Page
{
	NpgsqlCommand MyCommand = new NpgsqlCommand();
	private sbBLL bll = new sbBLL();
	DataTable dt = new DataTable();
	sbValidation _validation = new sbValidation();
	sbLoaderNdPopup _popup = new sbLoaderNdPopup();
	private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    protected void Page_Load(object sender, EventArgs e)
    {
        checkForSecurity();
        Session["_moduleName"] = "Bus Service Mapping";
		if (!IsPostBack)
		{
			loadDepot();
			lblSummary.Text = "Summary as on Date " + DateTime.Now.ToString("dd/MM/yyyy hh:ss tt");
			ddlDepot.SelectedValue = Session["_LDepotCode"].ToString();
			ddlDepot.Enabled = false;
			loadDashCounts();
			LoadServices();
			LoadBuses();
		}
		loadDataTable(gvServices);
	}

    #region Methods
    private void checkForSecurity()
    {
        //if (Session.IsNewSession == false || Request.ServerVariables["HTTP_REFERER"].Length < 1)
        //{
        //    Response.Redirect("../Errorpage.aspx");
        //}

        if (_security.isSessionExist(Session["_UserCode"]) == true)
        {
            Session["_UserCode"] = Session["_UserCode"];
        }
        else
        {
            Response.Redirect("../sessionTimeout.aspx");
        }
        if (_security.isSessionExist(Session["_RNDIDENTIFIERDOPT"]) == true)
        {
            Session["_RNDIDENTIFIERDOPT"] = Session["_RNDIDENTIFIERDOPT"];
        }
        else
        {
            Response.Redirect("../sessionTimeout.aspx");
        }
        if (_security.checkvalidation() == false)
        {
            Response.Redirect("../Errorpage.aspx");
        }
        if (_security.CheckOldUserLogin(Session["_UserCode"].ToString()) == false)
        {
            Response.Redirect("../Errorpage.aspx");
        }
    }
    public void loadDepot()//m1
	{
		try
		{
			Int32 OfcLvl = 30;
			ddlDepot.Items.Clear();
			MyCommand = new NpgsqlCommand();
			MyCommand.Parameters.Clear();
			MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_ofclvlwiseoffice");
			MyCommand.Parameters.AddWithValue("p_ofclvlid", OfcLvl);
			dt = bll.SelectAll(MyCommand);
			if (dt.TableName == "Success")
			{
				if (dt.Rows.Count > 0)
				{
					ddlDepot.DataSource = dt;
					ddlDepot.DataTextField = "officename";
					ddlDepot.DataValueField = "officeid";
					ddlDepot.DataBind();
				}
			}
			else
			{
				_common.ErrorLog("DepBusServiceMapping-M1", dt.TableName);
				Errormsg(dt.TableName);
			}
			ddlDepot.Items.Insert(0, "SELECT");
			ddlDepot.Items[0].Value = "0";
			ddlDepot.SelectedIndex = 0;
		}
		catch (Exception ex)
		{
			ddlDepot.Items.Insert(0, "SELECT");
			ddlDepot.Items[0].Value = "0";
			ddlDepot.SelectedIndex = 0;
			_common.ErrorLog("DepBusServiceMapping-M1", ex.Message.ToString());
		}
	}
	public void LoadServices()//m2
	{
		try
		{
			pnlNoRecord.Visible = true;
			DataTable dt = new DataTable();
			NpgsqlCommand MyCommand = new NpgsqlCommand();
			MyCommand.Parameters.Clear();
			MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_mapping_getdepotserviceslist");
			MyCommand.Parameters.AddWithValue("p_office", ddlDepot.SelectedValue);
			dt = bll.SelectAll(MyCommand);
			if (dt.TableName == "Success")
			{
				if (dt.Rows.Count > 0)
				{
					gvServices.Visible = true;
					gvServices.DataSource = dt;
					gvServices.DataBind();
					loadDataTable(gvServices);
					pnlNoRecord.Visible = false;
				}
			}
			else
			{
				_common.ErrorLog("DepBusServiceMapping-M2", dt.TableName);
				Errormsg(dt.TableName);
			}
		}
		catch (Exception ex)
		{
			gvServices.Visible = false;
			_common.ErrorLog("DepBusServiceMapping-M2", ex.Message.ToString());
			Errormsg(ex.Message.ToString());
		}
	}
	public void LoadBuses()//m3
	{
		try
		{
            tbSearch.Visible = false;
            lblNoBus.Visible = true;
            rptbuslist.Visible = false;
			MyCommand = new NpgsqlCommand();
			MyCommand.Parameters.Clear();
			MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_mapping_getbuslist");
			MyCommand.Parameters.AddWithValue("p_office", ddlDepot.SelectedValue);
			dt = bll.SelectAll(MyCommand);
			if (dt.TableName == "Success")
			{
				if (dt.Rows.Count > 0)
				{
					
					lblNoBus.Visible = false;
                    tbSearch.Visible = true;
                    rptbuslist.DataSource = dt;
                    rptbuslist.DataBind();
                    rptbuslist.Visible = true;
                }
			}
			else
			{
				_common.ErrorLog("DepBusServiceMapping-M3", dt.TableName);
				Errormsg(dt.TableName);
			}
		}
		catch (Exception ex)
		{
			_common.ErrorLog("DepBusServiceMapping-M3", ex.Message.ToString());
			Errormsg(ex.Message.ToString());
		}
	}
	public void loadDashCounts()//m4
	{
		try
		{
			MyCommand = new NpgsqlCommand();
			MyCommand.Parameters.Clear();
			MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_mapping_getcount");
			MyCommand.Parameters.AddWithValue("p_office", ddlDepot.SelectedValue);
			dt = bll.SelectAll(MyCommand);
			if (dt.TableName == "Success")
			{
				if (dt.Rows.Count > 0)
				{
					lblTotalServices.Text = dt.Rows[0]["sptotalservice"].ToString();
					lblTotalBuses.Text = dt.Rows[0]["sptotalbus"].ToString();
					lblTotalMappedBuses.Text = dt.Rows[0]["sptotalbusmapped"].ToString();
					if (dt.Rows[0]["sptotalservice"].ToString() == "0")
					{
						lbtnDownloadService.Visible = false;
					}
					else
					{
						lbtnDownloadService.Visible = true;
					}
					if (dt.Rows[0]["sptotalbus"].ToString() == "0")
					{
						lbtnDownloadBus.Visible = false;
					}
					else
					{
						lbtnDownloadBus.Visible = true;
					}
					if (dt.Rows[0]["sptotalbusmapped"].ToString() == "0")
					{
						lbtnDownloadBusMapped.Visible = false;
					}
					else
					{
						lbtnDownloadBusMapped.Visible = true;
					}
				}
			}
			else
			{
				_common.ErrorLog("DepBusServiceMapping-M4", dt.TableName);
				Errormsg(dt.TableName);
			}
		}
		catch (Exception ex)
		{
			_common.ErrorLog("DepBusServiceMapping-M4", ex.Message.ToString());
			Errormsg(ex.Message.ToString());
		}
	}
	public void LoadMappedBuses()//m5
	{
		try
		{
			Int32 dsvcid = Convert.ToInt32(Session["dsvcid"].ToString());
			string office = Session["officeid"].ToString();
			lblNoData.Visible = true;
			gvMappedBuses.Visible = false;
			MyCommand = new NpgsqlCommand();
			MyCommand.Parameters.Clear();
			MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_mapping_getmappedbuses");
			MyCommand.Parameters.AddWithValue("dsvcid", dsvcid);
			MyCommand.Parameters.AddWithValue("p_office", office);
			dt = bll.SelectAll(MyCommand);
			if (dt.TableName == "Success")
			{
				if (dt.Rows.Count > 0)
				{
					gvMappedBuses.DataSource = dt;
					gvMappedBuses.DataBind();
					gvMappedBuses.Visible = true;
					lblNoData.Visible = false;
				}
			}

			else
			{
				_common.ErrorLog("DepBusServiceMapping-M5", dt.TableName);
				Errormsg(dt.TableName);
			}
		}
		catch (Exception ex)
		{
			_common.ErrorLog("DepBusServiceMapping-M5", ex.Message.ToString());
			Errormsg(ex.Message.ToString());
		}
	}
	public void updateRouteBus()//m6
	{
		try
		{
			string ipaddress = HttpContext.Current.Request.UserHostAddress;
			string UpdatedBy = Session["_UserCode"].ToString();
			string officeid = Session["officeid"].ToString();
			string busregno = Session["mappedbuses"].ToString();
			long routeid = Convert.ToInt64(Session["routeid"].ToString());
			long dsvcid = Convert.ToInt64(Session["dsvcid"].ToString());
			NpgsqlCommand MyCommand = new NpgsqlCommand();
			MyCommand.Parameters.Clear();
			string Mresult = "";
			MyCommand.Parameters.Clear();
			MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_mapping_servicebusinsert");
			MyCommand.Parameters.AddWithValue("p_officeid", officeid);
			MyCommand.Parameters.AddWithValue("p_routeid", routeid);
			MyCommand.Parameters.AddWithValue("p_dsvcid", dsvcid);
			MyCommand.Parameters.AddWithValue("p_busno", busregno);
			MyCommand.Parameters.AddWithValue("p_updatedby", UpdatedBy);
			MyCommand.Parameters.AddWithValue("p_ipaddress", ipaddress);
			Mresult = bll.UpdateAll(MyCommand);
			if (Mresult == "Success")
			{
				Successmsg("Service-Bus mapping updated successfully");
				LoadBuses();
				LoadMappedBuses();
				loadDashCounts();
				Session["mappedbuses"] = null;
			}
			else
			{
				_common.ErrorLog("DepBusServiceMapping-M6", Mresult);
				Errormsg(Mresult);
			}
		}
		catch (Exception ex)
		{
			_common.ErrorLog("DepBusServiceMapping-M6", ex.Message.ToString());
			Errormsg(ex.Message.ToString());
		}
	}
	public void deleteRouteBus()//m7
	{
		try
		{
			string ipaddress = HttpContext.Current.Request.UserHostAddress;
			string UpdatedBy = Session["_UserCode"].ToString();
			string officeid = Session["officeid"].ToString();
			string busregno = Session["busno"].ToString();
			long dsvcid = Convert.ToInt64(Session["dsvcid"].ToString());
			NpgsqlCommand MyCommand = new NpgsqlCommand();
			MyCommand.Parameters.Clear();
			string Mresult = "";
			MyCommand.Parameters.Clear();
			MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_mapping_servicebusdelete");
			MyCommand.Parameters.AddWithValue("p_dsvcid", dsvcid);
			MyCommand.Parameters.AddWithValue("p_busno", busregno);
			MyCommand.Parameters.AddWithValue("p_updatedby", UpdatedBy);
			MyCommand.Parameters.AddWithValue("p_ipaddress", ipaddress);
			Mresult = bll.UpdateAll(MyCommand);
			if (Mresult == "Success")
			{
				LoadBuses();
				LoadMappedBuses();
				loadDashCounts();
				Session["busno"] = null;
				Successmsg("Bus has been unmapped from this service");
			}
			else
			{
				_common.ErrorLog("DepBusServiceMapping-m7", Mresult);
				Errormsg(Mresult);
			}
		}
		catch (Exception ex)
		{
			_common.ErrorLog("DepBusServiceMapping-M7", ex.Message.ToString());
			Errormsg(ex.Message.ToString());
		}
	}
	public void loadDataTable(GridView grd)
	{
		if (grd.Rows.Count > 0)
		{
			grd.UseAccessibleHeader = true;
			grd.HeaderRow.TableSection = TableRowSection.TableHeader;
		}
	}
	public void downloadBuses()
	{
		try
		{
			
			MyCommand.Parameters.Clear();
			MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_mapping_getbuslistfordownload");
			MyCommand.Parameters.AddWithValue("p_office", ddlDepot.SelectedValue);
			dt = bll.SelectAll(MyCommand);
			if (dt.TableName == "Success")
			{

				if (dt.Rows.Count > 0)
				{
					dataTableToExcel(dt);
				}
			}
			else
			{
				_common.ErrorLog("DepBusServiceMapping-M", dt.TableName);
				Errormsg(dt.TableName);
			}
		}

		catch (Exception ex)
		{
			_common.ErrorLog("DepBusServiceMapping-M", ex.Message.ToString());
			Errormsg(ex.Message.ToString());
		}
	}
	private void dataTableToExcel(DataTable dt)
	{
		GridView gv = new GridView();
		gv.DataSource = dt;
		gv.DataBind();
		gv.GridLines = GridLines.Both;
		Response.Clear();
		Response.Buffer = true;
		Response.ClearContent();
		Response.ClearHeaders();
		Response.Charset = "";
		Response.Cache.SetCacheability(HttpCacheability.NoCache);
		Response.ContentType = "application/vnd.ms-excel";
		Response.AddHeader("content-disposition", "attachment;filename=Buses.xls");
		using (StringWriter sw = new StringWriter())
		{
			HtmlTextWriter hw = new HtmlTextWriter(sw);

			// To Export all pages
			gv.AllowPaging = false;

			gv.HeaderRow.BackColor = Color.White;
			foreach (TableCell cell in gv.HeaderRow.Cells)
				cell.BackColor = gv.HeaderStyle.BackColor;
			foreach (GridViewRow row in gv.Rows)
			{
				row.BackColor = Color.White;
				foreach (TableCell cell in row.Cells)
				{
					if (row.RowIndex % 2 == 0)
						cell.BackColor = gv.AlternatingRowStyle.BackColor;
					else
						cell.BackColor = gv.RowStyle.BackColor;
					cell.CssClass = "textmode";
				}
			}
			gv.RenderControl(hw);
			// style to format numbers to string
			string style = "<style> .textmode { } </style>";
			Response.Write(style);
			Response.Output.Write(sw.ToString());
			// Response.Flush()
			Response.End();
		}
	}
	private void Errormsg(string msg)
	{

		string popup = _popup.modalPopupSmall("W", "Please Check", msg, "Close");
		Response.Write(popup);
	}
	private void Successmsg(string msg)
	{
		string popup = _popup.modalPopupSmall("S", "Success", msg, "Close");
		Response.Write(popup);
	}
	public void confirmmsg(string message)
	{
		lblConfirmation.Text = message;
		mpConfirmation.Show();
	}
	private void InfoMsg(string msg)
	{
		string popup = _popup.modalPopupSmall("I", "Information", msg, "Close");
		Response.Write(popup);
	}
	public void downloadMappedBus()
	{
		try
		{
			Int32 dsvcid = Convert.ToInt32(Session["dsvcid"].ToString());
			string office = Session["officeid"].ToString();
			MyCommand.Parameters.Clear();
			MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_mapping_getmappedbuses");
			MyCommand.Parameters.AddWithValue("dsvcid", dsvcid);
			MyCommand.Parameters.AddWithValue("p_office", office);
			dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    dataTableToExcel(dt);
                }
                else
                {
                    Errormsg("No Data Found");
                }
            }
            else
            {
                _common.ErrorLog("DepBusServiceMapping-M", dt.TableName);
                Errormsg(dt.TableName);
            }
		}
		catch (Exception ex)
		{
			_common.ErrorLog("DepBusServiceMapping-M", ex.Message.ToString());
			Errormsg(ex.Message.ToString());
		}
	}
	#endregion

	#region Events
	protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
	{
		if (Session["_Action"].ToString() == "S")
		{
			updateRouteBus();

		}
		else if (Session["_Action"].ToString() == "D")
		{
			deleteRouteBus();

		}
	}
	protected void gvServices_RowCommand(object sender, GridViewCommandEventArgs e)
	{
		int index = Convert.ToInt32(e.CommandArgument);
		if (e.CommandName == "mapBus")
		{
			GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
			int i = oItem.RowIndex;
			Session["dsvcid"] = gvServices.DataKeys[i]["dsvc_id"].ToString();
			Session["officeid"] = gvServices.DataKeys[i]["officeid"].ToString();
			Session["routeid"] = gvServices.DataKeys[i]["routeid"].ToString();
			lblRouteName.Text = gvServices.DataKeys[i]["service_name_en"].ToString();
			LoadBuses();
			LoadMappedBuses();
			pnlAddDepotService.Visible = false;
			pnlMapBus.Visible = true;

		}
	}
	protected void lbtnCancel_Click(object sender, EventArgs e)
	{
		pnlAddDepotService.Visible = true;
		pnlMapBus.Visible = false;
		loadDashCounts();
		LoadServices();
	}
	protected void chkBoxBuses_SelectedIndexChanged(object sender, EventArgs e)
	{
		//if (chkBoxBuses.SelectedItem.Selected == true)
		//{
		//	confirmmsg("Do you want to map Bus- " + chkBoxBuses.SelectedItem.ToString() + " with this Service ?");
		//	Session["_Action"] = "S";
		//	Session["mappedbuses"] = chkBoxBuses.SelectedItem;
		//}
	}
	protected void ddlDepot_SelectedIndexChanged(object sender, EventArgs e)
	{
		loadDashCounts();
		LoadServices();
	}
	protected void gvMappedBuses_RowCommand(object sender, GridViewCommandEventArgs e)
	{
		int index = Convert.ToInt32(e.CommandArgument);
		if (e.CommandName == "deleteBus")
		{
			GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
			int i = oItem.RowIndex;
			Session["busno"] = gvMappedBuses.DataKeys[i]["busno"].ToString();
			Session["_Action"] = "D";
			confirmmsg("Do you want to Remove Bus- " + Session["busno"].ToString() + " mapped with this Service ?");
		}
	}
	protected void lbtnDownloadService_Click(object sender, EventArgs e)
	{
		Response.Clear();
		Response.Buffer = true;
		Response.AddHeader("content-disposition", "attachment;filename=ServiceList.xls");
		Response.Charset = "";
		Response.ContentType = "application/vnd.ms-excel";
		using (StringWriter sw = new StringWriter())
		{
			HtmlTextWriter hw = new HtmlTextWriter(sw);

			// To Export all pages
			gvServices.AllowPaging = false;
			this.LoadServices();

			gvServices.HeaderRow.BackColor = Color.White;
			foreach (TableCell cell in gvServices.HeaderRow.Cells)
				cell.BackColor = gvServices.HeaderStyle.BackColor;
			foreach (GridViewRow row in gvServices.Rows)
			{
				row.BackColor = Color.White;
				foreach (TableCell cell in row.Cells)
				{
					if (row.RowIndex % 2 == 0)
						cell.BackColor = gvServices.AlternatingRowStyle.BackColor;
					else
						cell.BackColor = gvServices.RowStyle.BackColor;
					cell.CssClass = "textmode";
				}
			}

			gvServices.RenderControl(hw);
			// style to format numbers to string
			string style = "<style> .textmode { } </style>";
			Response.Write(style);
			Response.Output.Write(sw.ToString());
			Response.Flush();
			Response.End();
		}
	}
	public override void VerifyRenderingInServerForm(Control control)
	{
	}
	protected void lbtnDownloadBus_Click(object sender, EventArgs e)
	{
		downloadBuses();
	}
	protected void lbtnDownloadBusMapped_Click(object sender, EventArgs e)
	{
		downloadMappedBus();
	}
	protected void lbtnview_Click(object sender, EventArgs e)
	{
		string msg = "";
		msg = msg + "1. Select depot for which you want to map bus with service.<br/>";
		msg = msg + "2. Bus can be mapped with only one service at a time.<br/>";
		msg = msg + "3. To map bus click on view/update button, select bus you want to map and click on yes button.<br/>";
		msg = msg + "4. To remove mapped bus , click on delete button and bus will delete from the mapped bus list.<br/>";
		InfoMsg(msg);

	}
    protected void lbtndownload_Click(object sender, EventArgs e)
    {
        Errormsg("Coming Soon");
    }
    protected void Unnamed_Click(object sender, EventArgs e)
    {
        Errormsg("Coming Soon");
    }
    protected void rptbuslist_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "ADDBUS")
        {
            string bus = e.CommandArgument.ToString();
            confirmmsg("Do you want to map Bus- " + bus + " with this Service ?");
            Session["_Action"] = "S";
            Session["mappedbuses"] = bus;
        }
    }
    #endregion
}