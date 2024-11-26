using System;
using System;
using System.Data;
using Npgsql;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;


public partial class Auth_CntrTripassignment : System.Web.UI.Page
{
    NpgsqlCommand MyCommand = new NpgsqlCommand();
    private sbBLL bll = new sbBLL();
    DataTable dt = new DataTable();
    sbValidation _validation = new sbValidation();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    private sbCommonFunc _common = new sbCommonFunc();
    protected void Page_Init(object sender, System.EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.AddHeader("Cache-control", "no-store, must-revalidate,private,no-cache");
        Response.AddHeader("PRAGMA", "NO-Cache");
        Response.Expires = -1;
        Response.Expires = 0;
        Response.Cache.SetNoStore();
        Response.Cache.SetExpires(DateTime.Now.AddDays(-2));
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["_moduleName"] = "Trip Assignment";
            loadTrip(gvtrip, "R");
        }
    }
    private void loadTrip(GridView gvtrip, string TripStatus)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_get_TripDetails");
            MyCommand.Parameters.AddWithValue("p_action", TripStatus);
            MyCommand.Parameters.AddWithValue("p_tripcode", tbtripcode.Text);
            MyCommand.Parameters.AddWithValue("p_cntr_id", Session["_CntrCode"].ToString());
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    gvtrip.DataSource = dt;
                    gvtrip.DataBind();
                    gvtrip.Visible = true;
                    pnlNoRecord.Visible = false;
                }
                else
                {
                    gvtrip.Visible = false;
                    pnlNoRecord.Visible = true;
                }
            }
            else
            {
                gvtrip.Visible = false;
                pnlNoRecord.Visible = true;
            }

        }
        catch (Exception ex)
        {
            gvtrip.Visible = false;
            pnlNoRecord.Visible = true;
        }
    }
    protected void lbtnReadyToprepare_Click(object sender, EventArgs e)
    {
        loadTrip(gvtrip, "R");
        lbltriplist.Text = "Ready To prepare Trip List";
    }
    protected void lbtnUpcommingTrip_Click(object sender, EventArgs e)
    {
        loadTrip(gvtrip, "U");
        lbltriplist.Text = "Upcomming Trip List";
    }
    protected void lbtnPreparedTrip_Click(object sender, EventArgs e)
    {
        loadTrip(gvtrip, "P");
        lbltriplist.Text = "Prepared Trip List";
    }
    protected void lbtnTimeOverTrip_Click(object sender, EventArgs e)
    {
        loadTrip(gvtrip, "T");
        lbltriplist.Text = "Time Over Trip List";
    }
    protected void gvtrip_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView rowView = (DataRowView)e.Row.DataItem;
            LinkButton lbtnview = (LinkButton)e.Row.FindControl("lbtnview");
            LinkButton lbtngeneratetrip = (LinkButton)e.Row.FindControl("lbtngeneratetrip");
            lbtnview.Visible = false;
            lbtngeneratetrip.Visible = false;
            if (rowView["sp_action"].ToString() == "R")
            {
                lbtngeneratetrip.Visible = true;
            }
            else if (rowView["sp_action"].ToString() == "U" || rowView["sp_action"].ToString() == "P" || rowView["sp_action"].ToString() == "T")
            {
                lbtnview.Visible = true;
            }
        }
    }

    protected void gvtrip_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "viewtrip")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string p_trip_code = gvtrip.DataKeys[index].Values["trip_code"].ToString();
            Session["trip_code"] = p_trip_code;
            // openSubDetailsWindow("../E_cancellationvoucher.aspx");

        }
        if (e.CommandName == "generatetrip")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string p_trip_code = gvtrip.DataKeys[index].Values["trip_code"].ToString();
            Session["trip_code"] = p_trip_code;
            LoadTripDetails(Session["trip_code"].ToString());
            pnlassigntrip.Visible = true;
            pnltripdetails.Visible = false;
        }
    }

    private void LoadTripDetails(string tripcode)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_get_assignTripDetails");
            MyCommand.Parameters.AddWithValue("p_tripcode", tripcode);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    lbltrip.Text = "Assign Bus/Conductor and Driver in Trip <b>" + dt.Rows[0]["trip_code"].ToString() + "</b>";
                    lblservice.Text = dt.Rows[0]["service_name_en"].ToString();
                    lblservicetype.Text = dt.Rows[0]["servicetype"].ToString();
                    lbljourneydatetime.Text = dt.Rows[0]["trip_date"].ToString() + " " + dt.Rows[0]["trip_time"].ToString();
                    lblstations.Text = dt.Rows[0]["frstationname_name"].ToString() + "-" + dt.Rows[0]["tostationname_name"].ToString();
                    lbldepot.Text = dt.Rows[0]["office_name"].ToString();
                    loadTripPassengerDetails(dt.Rows[0]["trip_code"].ToString());
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    private void loadTripPassengerDetails(string tripcode)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_get_TrippassengerDetails");
            MyCommand.Parameters.AddWithValue("p_tripcode", tripcode);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    gvTicketseatDetails.DataSource = dt;
                    gvTicketseatDetails.DataBind();
                    lbltotseats.Text = dt.Rows.Count.ToString();                    
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void gvTicketseatDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView rowView = (DataRowView)e.Row.DataItem;
            decimal fareamt = Convert.ToDecimal(rowView["amount_total"].ToString());
            lbltotfare.Text = (Convert.ToDecimal(lbltotfare.Text.ToString()) + fareamt).ToString();
        }
    }

    protected void lbtnCancel_Click(object sender, EventArgs e)
    {
        pnltripdetails.Visible = true;
        pnlassigntrip.Visible = false;
    }
}