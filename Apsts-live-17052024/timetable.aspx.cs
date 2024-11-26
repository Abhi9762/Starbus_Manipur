using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class timetable : System.Web.UI.Page
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    sbValidation _SecurityCheck = new sbValidation();
    sbSummary obj = new sbSummary();

    protected void Page_Load(object sender, EventArgs e)
    {
        Session["heading"] = "Timetable";
        if (!IsPostBack)
        {
            lblNoService.Text = "";
            loadBusServiceType();
        }
    }

    #region "Methods"
    private void loadBusServiceType()
    {
        try
        {
            DataTable dt = new DataTable();
            ddlservices.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_servicetype_getlist");
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                ddlservices.DataSource = dt;
                ddlservices.DataTextField = "servicetype_name_en";
                ddlservices.DataValueField = "srtpid";
                ddlservices.DataBind();
            }
            ddlservices.Items.Insert(0, "All");
            ddlservices.Items[0].Value = "0";
            ddlservices.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlservices.Items.Insert(0, "All");
            ddlservices.Items[0].Value = "0";
            ddlservices.SelectedIndex = 0;

        }
    }
    [WebMethod()]
    public static List<string> searchStations(string stationText, string fromTo)//M1
    {
        try
        {
            wsClass obj = new wsClass();
            return obj.timetable_station_web(stationText, fromTo);
        }
        catch (Exception ex)
        {
            sbCommonFunc _common = new sbCommonFunc();
            _common.ErrorLog("seatavailablity-M1", ex.Message);
            return null;
        }
    }

    public void timetable_dt(string fromStationName, string toStationName, string serviceTypeId)
    {
        lblNoService.Text = "No Service available for selected parameters";
        gvlist.Visible = false;
        pnlNoservice.Visible = true;
        MyCommand = new NpgsqlCommand();
        dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_service_timetable");//City Based Search
        MyCommand.Parameters.AddWithValue("p_from_city", fromStationName);
        MyCommand.Parameters.AddWithValue("p_to_city", toStationName);
        MyCommand.Parameters.AddWithValue("p_servicetype_id", Convert.ToInt16(serviceTypeId));
        dt = bll.SelectAll(MyCommand);
        if (dt.Rows.Count > 0)
        {
            gvlist.DataSource = dt;
            gvlist.DataBind();
            gvlist.Visible = true;
            pnlNoservice.Visible = false;
        }

    }
    private void Errormsg(string header, string msg)
    {
        lblmpMsgHeader.Text = header;
        lblmpMsgText.Text = msg;
        mpMsg.Show();
    }

    public void getTimetable(string dsvcID, string strpID)
    {
        try
        {
            lvTimetableView.DataSource = null;
            lvTimetableView.DataBind();
            PanelNoRecordTimeTableView.Visible = true;
            lvTimetableView.Visible = false;

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_depotservice_get_timetable_view");
            MyCommand.Parameters.AddWithValue("p_dsvcid", Convert.ToInt64(dsvcID));
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {

                DataRow[] result = dt.Select("strpid = " + strpID);
                DataTable dt1 = result.CopyToDataTable();
                if (dt1.Rows.Count > 0)
                {
                    lvTimetableView.DataSource = dt1;
                    lvTimetableView.DataBind();
                    PanelNoRecordTimeTableView.Visible = false;
                    lvTimetableView.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("timetable.aspx-001", ex.Message);
        }
    }

    #endregion
    protected void gvlist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "VIEWDETAILS")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string dsvcid = gvlist.DataKeys[index].Values["dsvcid"].ToString();
            string strpid = gvlist.DataKeys[index].Values["strpid"].ToString();
            string fr_city = gvlist.DataKeys[index].Values["fr_city"].ToString();
            string to_city = gvlist.DataKeys[index].Values["to_city"].ToString();

            lblHeader.Text = fr_city + " - " + to_city;

            getTimetable(dsvcid, strpid);
            mpDetail.Show();

        }
    }

    protected void lbtnSearchServices_Click(object sender, EventArgs e)
    {
        string frSton = tbFrom.Text.Trim().ToUpper();
        string toSton = tbTo.Text.Trim().ToUpper();
        string secviceTypeId = ddlservices.SelectedValue.ToString(); 
        if (_SecurityCheck.IsValidString(frSton, 2, 50) == false)
        {
            Errormsg("Please Check", "Enter valid From Station Name");
            return;
        }
        if (_SecurityCheck.IsValidString(toSton, 2, 50) == false)
        {
            Errormsg("Please Check", "Enter valid To Station Name");
            return;
        }

        if (frSton == toSton)
        {
            Errormsg("Please Check", "Station Name Cannot Be Same");
            return;
        }

        timetable_dt(frSton, toSton, secviceTypeId);
    }

}