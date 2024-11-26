using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;


public partial class Busservice : System.Web.UI.Page
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

        Session["heading"] = "Bus Service";
        int srvicetypeid = Convert.ToInt32(Request.QueryString["servicetypeid"].ToString());
        loadService(srvicetypeid);
        loadfrmstation(srvicetypeid);
        fordatatable();
    }
    private void fordatatable()
    {
        if (gvTiming.Rows.Count > 0)
        {
            gvTiming.UseAccessibleHeader = true;
            gvTiming.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
    private void loadfrmstation(int servicetypeid)
    {
        gvTiming.Visible = false;
        pnlNoservice.Visible = true;

        MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_get_service_timetable");
        MyCommand.Parameters.AddWithValue("p_srtpid", Convert.ToInt64(servicetypeid));
        dt = bll.SelectAll(MyCommand);
        if (dt.TableName == "Success")
        {
            if (dt.Rows.Count > 0)
            {
                gvTiming.DataSource = dt;
                gvTiming.DataBind();
                gvTiming.Visible = true;
                pnlNoservice.Visible = false;
            }
        }
    }

    private void loadService(int servicetypeid)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            DataTable dt = new DataTable();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_servicetype_getlist");
            MyCommand.Parameters.AddWithValue("p_srtp_id", servicetypeid);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    byte[] bytes = (byte[])dt.Rows[0]["img_web"];
                    string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                    imgbuservice.ImageUrl = "data:image/jpg;base64," + base64String;
                    lbloverview.Text = dt.Rows[0]["des_cription"].ToString();
                    lblservice.Text = dt.Rows[0]["servicetype_name_en"].ToString() + " Bus Services";
                    if (!DBNull.Value.Equals(dt.Rows[0]["amenitiesa"]))
                    {
                        DataTable dtami = new DataTable();
                        dtami.Columns.Add("Aminity", typeof(string));
                        dtami.Columns.Add("AminityIcon", typeof(string));
                        string[] AmitiesList = dt.Rows[0]["ami_name"].ToString().Split(',');
                        string[] Amitiesurl = dt.Rows[0]["aminities_url"].ToString().Split(',');

                        for (int i = 0; i < AmitiesList.Length; i++)
                        {
                            dtami.Rows.Add(AmitiesList[i].ToString(), "assets/img/amenity/" + Amitiesurl[i].ToString().Trim());
                        }

                        if (dtami.Rows.Count > 0)
                        {
                            rptaminities.DataSource = dtami;
                            rptaminities.DataBind();
                            rptaminities.Visible = true;
                        }
                        else
                        {
                            rptaminities.Visible = false;
                        }
                    }
                    else
                    {
                        rptaminities.Visible = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
        }

    }

    protected void gvTiming_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "VIEWDETAILS")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string dsvcid = gvTiming.DataKeys[index].Values["dsvcid"].ToString();
            string strpid = gvTiming.DataKeys[index].Values["strpid"].ToString();
            string frm_station = gvTiming.DataKeys[index].Values["frm_station"].ToString();
            string to_station = gvTiming.DataKeys[index].Values["to_station"].ToString();
            string strt_time = gvTiming.DataKeys[index].Values["strt_time"].ToString();

            lblHeader.Text = frm_station + " - " + to_station;

            getTimetable(dsvcid, strpid);
            mpDetail.Show();

        }
    }


    public void getTimetable(string dsvcID, string strpID) //M17
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
            _common.ErrorLog("SysAdmDepotService.aspx-0028", ex.Message);
        }
    }

    protected void lvTimetableView_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ListViewDataItem dataItem = (ListViewDataItem)e.Item;
        DataRowView drv = (DataRowView)dataItem.DataItem;
        if (lvTimetableView.EditItem == null)
        {
            Label lblInTime = (Label)e.Item.FindControl("lblInTime");
            Label lblOutTime = (Label)e.Item.FindControl("lblOutTime");
            if (lblInTime.Text.Length <= 0)
            {
                lblInTime.Text = "Trip Start";
                lblInTime.ForeColor = System.Drawing.Color.Green;
                lblInTime.BackColor = System.Drawing.Color.Yellow;
            }
            else
            {
                lblInTime.Text = "<span class='font-weight-600' style='font-size:.5rem !important'>IN </span>" + lblInTime.Text;
            }
        }
    }
}