using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Npgsql;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing;

public partial class Auth_DepBusBusCrewMapping : System.Web.UI.Page
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
        Session["_moduleName"] = "Bus Crew Mapping";
		if (!IsPostBack)
		{
			loadDepot();
			lblSummary.Text = "Summary as on Date " + DateTime.Now.ToString("dd/MM/yyyy hh:ss tt");
			ddlDepot.SelectedValue = Session["_LDepotCode"].ToString();
			ddlDepot.Enabled = false;
			loadBusesList();
			loadCrewCounts();
		}
		loadDataTable(gvBuslist);

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
				_common.ErrorLog("DepBusCrewMapping-M1", dt.TableName);
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
			_common.ErrorLog("DepBusCrewMapping-M1", ex.Message.ToString());
		}
	}
	public void loadCrewCounts()//m2
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
					lblTotalBuses.Text = dt.Rows[0]["sptotalbus"].ToString();
					lblTotalDriver.Text = dt.Rows[0]["sptotaldriver"].ToString();
					lblTotalConductor.Text = dt.Rows[0]["sptotalconductor"].ToString();
					lblTotalMappedDriver.Text = dt.Rows[0]["sptotaldrivermapped"].ToString();
					if (dt.Rows[0]["sptotalbus"].ToString() == "0")
					{
						lbtnDownloadBuses.Visible = false;
					}
					else
					{
						lbtnDownloadBuses.Visible = true;
					}
					if (dt.Rows[0]["sptotaldriver"].ToString() == "0")
					{
						lbtnDownloadDriver.Visible = false;
					}
					else
					{
						lbtnDownloadDriver.Visible = true;
					}
					if (dt.Rows[0]["sptotalconductor"].ToString() == "0")
					{
						lbtnDownloadConductor.Visible = false;
					}
					else
					{
						lbtnDownloadConductor.Visible = true;
					}
					if (dt.Rows[0]["sptotaldrivermapped"].ToString() == "0")
					{
						lbtnDownloadMappedDriver.Visible = false;
					}
					else
					{
						lbtnDownloadMappedDriver.Visible = true;
					}
				}
			}
			else
			{
				_common.ErrorLog("DepBusCrewMapping-M2", dt.TableName);
				Errormsg(dt.TableName);
			}
		}
		catch (Exception ex)
		{
			_common.ErrorLog("DepBusCrewMapping-M2", ex.Message.ToString());
			Errormsg(ex.Message.ToString());
		}
	}
	public void loadBusesList()//m3
	{
		try
		{
			pnlNoRecord.Visible = true;
			gvBuslist.Visible = false;
            tbSearch.Visible = false;
            DataTable dt = new DataTable();
			NpgsqlCommand MyCommand = new NpgsqlCommand(); MyCommand.Parameters.Clear();
			MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_mappingcrew_getbuseslist");
			MyCommand.Parameters.AddWithValue("p_office", ddlDepot.SelectedValue);
			dt = bll.SelectAll(MyCommand);
			if (dt.TableName == "Success")
			{
				if (dt.Rows.Count > 0)
				{
					gvBuslist.DataSource = dt;
					gvBuslist.DataBind();
					loadDataTable(gvBuslist);
					gvBuslist.Visible = true;
					pnlNoRecord.Visible = false;
                    tbSearch.Visible = true;
                }
			}
			else
			{
				_common.ErrorLog("DepBusCrewMapping-M3", dt.TableName);
				Errormsg(dt.TableName);
			}
		}
		catch (Exception ex)
		{
			_common.ErrorLog("DepBusCrewMapping-M3", ex.Message.ToString());
			Errormsg(ex.Message);
		}
	}
	public void loadDrivers()//m4
	{
		try
		{
			lblNoCrew.Visible = true;
			int designation = Convert.ToInt32(ddlEmpType.SelectedValue);
            rptemplist.Visible = false;
            tbSearch.Visible = false;
            MyCommand = new NpgsqlCommand();
			MyCommand.Parameters.Clear();
			MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_mapping_getcrewlist");
			MyCommand.Parameters.AddWithValue("p_busno", Session["busno"].ToString());
			MyCommand.Parameters.AddWithValue("p_officeid", ddlDepot.SelectedValue);
			MyCommand.Parameters.AddWithValue("p_designation", designation);
			dt = bll.SelectAll(MyCommand);
			if (dt.TableName == "Success")
			{
				if (dt.Rows.Count > 0)
				{
                    rptemplist.DataSource = dt;
					
                    rptemplist.DataBind();
					lblNoCrew.Visible = false;
                    rptemplist.Visible = true;
                    tbSearch.Visible = true;
                }
			}
			else
			{
				_common.ErrorLog("DepBusCrewMapping-M4", dt.TableName);
				Errormsg(dt.TableName);
			}
		}
		catch (Exception ex)
		{
			_common.ErrorLog("DepBusCrewMapping-M4", ex.Message.ToString());
			Errormsg(ex.Message.ToString());
		}
	}
	public void LoadMappedDrivers()//m5
	{
		try
		{
			string busno = Session["busno"].ToString();
			string office = Session["depotcode"].ToString();
			lblNoData.Visible = true;
			gvMappedDrivers.Visible = false;
			MyCommand = new NpgsqlCommand();
			MyCommand.Parameters.Clear();
			MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_mapping_getmappedcrewlist");
			MyCommand.Parameters.AddWithValue("p_busno", busno);
			MyCommand.Parameters.AddWithValue("p_office", office);
			dt = bll.SelectAll(MyCommand);
			if (dt.TableName == "Success")
			{
				if (dt.Rows.Count > 0)
				{
					gvMappedDrivers.DataSource = dt;
					gvMappedDrivers.DataBind();
					gvMappedDrivers.Visible = true;
					lblNoData.Visible = false;
				}
			}
			else
			{
				_common.ErrorLog("DepBusCrewMapping-M5", dt.TableName);
				Errormsg(dt.TableName);
			}
		}
		catch (Exception ex)
		{
			_common.ErrorLog("DepBusCrewMapping-M5", ex.Message.ToString());
			Errormsg(ex.Message);
		}
	}
	public void updateRouteBus()//m6
	{
		try
		{
			string ipaddress = HttpContext.Current.Request.UserHostAddress;
			string UpdatedBy = Session["_UserCode"].ToString();
			string emp = Session["_MAPPINGDRIVER"].ToString();
			string busregno = Session["busno"].ToString();
			NpgsqlCommand MyCommand = new NpgsqlCommand();
			MyCommand.Parameters.Clear();
			string Mresult = "";
			MyCommand.Parameters.Clear();
			MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_mapping_buscrewinsert");
			MyCommand.Parameters.AddWithValue("p_empcode", emp);
			MyCommand.Parameters.AddWithValue("p_busno", busregno);
			MyCommand.Parameters.AddWithValue("p_updatedby", UpdatedBy);
			MyCommand.Parameters.AddWithValue("p_ipaddress", ipaddress);
			Mresult = bll.UpdateAll(MyCommand);
			if (Mresult == "Success")
			{
				Successmsg("Bus-Crew mapping updated successfully");
				LoadMappedDrivers();
				loadDrivers();
				loadCrewCounts();
				Session["_MAPPINGDRIVER"] = null;
			}
			else
			{
				_common.ErrorLog("DepBusCrewMapping-m6", Mresult);
				Errormsg(Mresult);
			}
		}
		catch (Exception ex)
		{
			_common.ErrorLog("DepBusCrewMapping-M6", ex.Message.ToString());
			Errormsg(ex.Message.ToString());
		}
	}
	public void deleteRouteBus()//m7
	{
		try
		{
			string ipaddress = HttpContext.Current.Request.UserHostAddress;
			string UpdatedBy = Session["_UserCode"].ToString();
			string busregno = Session["busno"].ToString();
			string empcode = Session["empcode"].ToString();
			NpgsqlCommand MyCommand = new NpgsqlCommand();
			MyCommand.Parameters.Clear();
			string Mresult = "";
			MyCommand.Parameters.Clear();
			MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_mapping_buscrewdelete");
			MyCommand.Parameters.AddWithValue("p_empcode", empcode);
			MyCommand.Parameters.AddWithValue("p_busno", busregno);
			MyCommand.Parameters.AddWithValue("p_updatedby", UpdatedBy);
			MyCommand.Parameters.AddWithValue("p_ipaddress", ipaddress);
			Mresult = bll.UpdateAll(MyCommand);
			if (Mresult == "Success")
			{
				Successmsg("Bus-Crew mapping updated successfully");
				LoadMappedDrivers();
				loadDrivers();
				loadCrewCounts();
				Session["empcode"] = null;
			}
			else
			{
				_common.ErrorLog("DepBusCrewMapping-m7", Mresult);
				Errormsg(Mresult);
			}
		}
		catch (Exception ex)
		{
			_common.ErrorLog("DepBusCrewMapping-M7", ex.Message.ToString());
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
	protected void ddlDepot_SelectedIndexChanged(object sender, EventArgs e)
	{
		loadBusesList();
		loadCrewCounts();
	}
	protected void gvBuslist_RowCommand(object sender, GridViewCommandEventArgs e)
	{

		int index = Convert.ToInt32(e.CommandArgument);
		if (e.CommandName == "mapBus")
		{
			GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
			int i = oItem.RowIndex;
			Session["busno"] = gvBuslist.DataKeys[i]["busno"].ToString();
			Session["depotcode"] = gvBuslist.DataKeys[i]["depotcode"].ToString();
			lblRouteName.Text = gvBuslist.DataKeys[i]["busno"].ToString();
			tbSearch.Text = "";
			loadDrivers();
			LoadMappedDrivers();
			pnlMapBus.Visible = true;
			pnlAddDepotService.Visible = false;
		}
	}
	protected void gvMappedDrivers_RowCommand(object sender, GridViewCommandEventArgs e)
	{
		int index = Convert.ToInt32(e.CommandArgument);
		if (e.CommandName == "deleteBus")
		{
			GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
			int i = oItem.RowIndex;
			Session["empcode"] = gvMappedDrivers.DataKeys[i]["empcode"].ToString();
			Session["empname"] = gvMappedDrivers.DataKeys[i]["empname"].ToString();
			Session["busno"] = gvMappedDrivers.DataKeys[i]["busno"].ToString();
			Session["_Action"] = "D";
			confirmmsg("Do you want to delete Driver- " + Session["empname"].ToString() + " mapped with this Bus?");
		}
	}
	protected void chkBoxDrivers_SelectedIndexChanged(object sender, EventArgs e)
	{
		//if (ddlEmpType.SelectedValue.ToString() == "2")
		//{
		//	if (chkBoxDrivers.SelectedItem.Selected == true)
		//	{
		//		confirmmsg("Do you want to map Driver- " + chkBoxDrivers.SelectedItem.ToString() + " with this BUS?");
		//		Session["_Action"] = "S";
		//		Session["_MAPPINGDRIVER"] = chkBoxDrivers.SelectedValue;
		//	}
		//}
		//else
		//{
		//	if (chkBoxDrivers.SelectedItem.Selected == true)
		//	{
		//		confirmmsg("Do you want to map Conductor- " + chkBoxDrivers.SelectedItem.ToString() + " with this BUS?");
		//		Session["_Action"] = "S";
		//		Session["_MAPPINGDRIVER"] = chkBoxDrivers.SelectedValue;
		//	}

		//}
	}
	protected void lbtnCancel_Click(object sender, EventArgs e)
	{
		pnlAddDepotService.Visible = true;
		pnlMapBus.Visible = false;
		loadBusesList();
		loadCrewCounts();
	}
	protected void ddlEmpType_SelectedIndexChanged(object sender, EventArgs e)
	{
		loadDrivers();
	}
	protected void lbtnDownloadBuses_Click(object sender, EventArgs e)
	{
		Response.Clear();
		Response.Buffer = true;
		Response.AddHeader("content-disposition", "attachment;filename=BusList.xls");
		Response.Charset = "";
		Response.ContentType = "application/vnd.ms-excel";
		using (StringWriter sw = new StringWriter())
		{
			HtmlTextWriter hw = new HtmlTextWriter(sw);

			// To Export all pages
			gvBuslist.AllowPaging = false;
			this.loadBusesList();

			gvBuslist.Columns[3].Visible = false;

			gvBuslist.HeaderRow.BackColor = Color.White;
			foreach (TableCell cell in gvBuslist.HeaderRow.Cells)
				cell.BackColor = gvBuslist.HeaderStyle.BackColor;
			foreach (GridViewRow row in gvBuslist.Rows)
			{
				row.BackColor = Color.White;
				foreach (TableCell cell in row.Cells)
				{
					if (row.RowIndex % 2 == 0)
						cell.BackColor = gvBuslist.AlternatingRowStyle.BackColor;
					else
						cell.BackColor = gvBuslist.RowStyle.BackColor;
					cell.CssClass = "textmode";
				}
			}

			gvBuslist.RenderControl(hw);
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
	protected void lbtnDownloadDriver_Click(object sender, EventArgs e)
	{
		downloadDriverConductor();
	}
	public void downloadDriverConductor()//m
	{
		try
		{
			int designation = Convert.ToInt32(ddlEmpType.SelectedValue);
			
			MyCommand = new NpgsqlCommand();
			MyCommand.Parameters.Clear();
			MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_mapping_getcrewlist");
			MyCommand.Parameters.AddWithValue("p_busno", Session["busno"].ToString());
			MyCommand.Parameters.AddWithValue("p_officeid", ddlDepot.SelectedValue);
			MyCommand.Parameters.AddWithValue("p_designation", designation);
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
				_common.ErrorLog("DepBusCrewMapping-M4", dt.TableName);
				Errormsg(dt.TableName);
			}
		}
		catch (Exception ex)
		{
			_common.ErrorLog("DepBusCrewMapping-M4", ex.Message.ToString());
			Errormsg(ex.Message.ToString());
		}
	}
	protected void lbtnDownloadConductor_Click(object sender, EventArgs e)
	{

	}
	protected void lbtnDownloadMappedDriver_Click(object sender, EventArgs e)
	{

	}
	protected void lbtnview_Click(object sender, EventArgs e)
	{
		string msg = "";
		msg = msg + "1. Select depot for which you want to map crew with bus.<br/>";
		msg = msg + "2. Crew can be mapped with only one bus at a time.<br/>";
		msg = msg + "3. To map crew click on view/update button, select driver or conductor you want to map and click on yes button.<br/>";
		msg = msg + "4. To remove mapped crew , click on delete button and crew will delete from the mapped crew list.<br/>";
		InfoMsg(msg);

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
        Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
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
    protected void rptemplist_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "ADDDRIVER")
        {
            if (ddlEmpType.SelectedValue.ToString() == "2")
            {
                string emp = e.CommandArgument.ToString();
                confirmmsg("Do you want to map Driver- " + emp + " with this BUS?");
                Session["_Action"] = "S";
                Session["_MAPPINGDRIVER"] = emp;
            }
            else
            {
                string emp = e.CommandArgument.ToString();
                confirmmsg("Do you want to map Conductor- " + emp + " with this Service ?");
                Session["_Action"] = "S";
                Session["_MAPPINGDRIVER"] = emp;
            }
        }
    }
    #endregion
}