using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class Auth_agtripchart : System.Web.UI.Page
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    string current_date = DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("yyyy");
    protected void Page_Load(object sender, EventArgs e)
    {

        if (_security.isSessionExist(Session["_RoleCode"]) == true)
        {

        }
        else
        {
            Response.Redirect("sessionTimeout.aspx");
        }
        if (!IsPostBack)
        {
            Random randomclass = new Random();
            Session["rndNoCheck"] = randomclass.Next().ToString();
            hidtoken.Value = Session["rndNoCheck"].ToString();
            Session["MasterPageHeaderText"] = "Agent |";
            Session["_moduleName"] = "Trip Chart Dashoard";
            txtdate.Text = current_date;
            loadDepot();
            loadStation();
            lbtnreset.Visible = true;
            if (Session["_RoleCode"].ToString() == "1")
            {
                Session["currentyn"] = "N";
            }
            if (Session["_RoleCode"].ToString() == "5")
            {
                lbtnreset.Visible = false;
                GetDepoYN(Session["_LDepotCode"].ToString());
                ddldepot.Enabled = false;
                ddldepot.SelectedValue = Session["_LDepotCode"].ToString();
            }
            if (Session["_RoleCode"].ToString() == "19")
            {
                Session["currentyn"] = "N";
                lbtnreset.Visible = false;
                //GetDepoYN(Session["_UserCntrID"].ToString());
                LoadAgStation();
                ddldepot.Enabled = false;
                ddlstation.Enabled = false;
                ddldepot.SelectedValue = Session["_LDepotCode"].ToString();
                ddlstation.SelectedValue = Session["_LStationCode"].ToString();
            }
            

            loadReadyTrip();
            loadUpcoming();
            loadPrepared();
            loadUnprepared();
        }
    }

    #region "Methods"
    private bool LoadAgStation()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "agent.getagentstation");
            MyCommand.Parameters.AddWithValue("p_usercode", Session["_UserCode"].ToString());
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    Session["_LStationCode"] = dt.Rows[0]["station_code"];
                    Session["depot_code"] = dt.Rows[0]["depot_code"];
                    return true;
                }


            }
            return false;
        }
        catch (Exception ex)
        {
            return false;
        }

    }
    private void CsrfTokenValidate()
    {
        if (hidtoken.Value != Session["rndNoCheck"].ToString())
        {
            Response.Redirect("errorpage.aspx");
            return;
        }
    }
    private void loadDepot()
    {
        try
        {
            ddldepot.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_depot_list");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddldepot.DataSource = dt;
                    ddldepot.DataTextField = "depot_name";
                    ddldepot.DataValueField = "depot_code";
                    ddldepot.DataBind();
                }
            }

            ddldepot.Items.Insert(0, "All");
            ddldepot.Items[0].Value = "0";
            ddldepot.SelectedIndex = 0;


        }
        catch (Exception ex)
        {
            ddldepot.Items.Insert(0, "SELECT");
            ddldepot.Items[0].Value = "0";
            ddldepot.SelectedIndex = 0;
            _common.ErrorLog("tripchartdash.aspx-0001", ex.Message.ToString());
        }
    }
    public void loadStation()
    {
        try
        {
            ddlstation.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.fn_get_depot_bus_station");
            MyCommand.Parameters.AddWithValue("p_depotcode", ddldepot.SelectedValue);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlstation.DataSource = dt;
                    ddlstation.DataTextField = "stationname";
                    ddlstation.DataValueField = "stationid";
                    ddlstation.DataBind();
                }
            }
            ddlstation.Items.Insert(0, "All");
            ddlstation.Items[0].Value = "0";
            ddlstation.SelectedIndex = 0;

        }
        catch (Exception ex)
        {
            ddlstation.Items.Insert(0, "All");
            ddlstation.Items[0].Value = "0";
            ddlstation.SelectedIndex = 0;
            _common.ErrorLog("tripchartdash.aspx-0002", ex.Message.ToString());
        }
    }
    private void loadReadyTrip()
    {
        try
        {
            pnltodayDate.Visible = false;
            wsClass obj = new wsClass();
            DataTable dt = obj.gettripagent(ddldepot.SelectedValue, ddlstation.SelectedValue, "O", "A", txtdate.Text);
            DataRow[] dtrow = dt.Select("minutes>=startminute and minutes <=endminute");
            //DataRow[] dtrow = dt.Select();
            if (dtrow.Length > 0)
            {
                grdready.Visible = true;
                grdready.DataSource = dtrow.CopyToDataTable();
                grdready.DataBind();
                pnlReady.Visible = false;
                lblreadyTotalcount.Text = "Total " + dtrow.Length;
                lblreadyTotalcount.Visible = true;
                pnltodayDate.Visible = true;
            }
            else
            {
                lblreadyTotalcount.Visible = false;
                pnlReady.Visible = true;
                grdready.Visible = false;
            }
        }
        catch (Exception ex)
        { _common.ErrorLog("tripchartdash.aspx-0003", ex.Message.ToString()); }
    }
    private void loadUpcoming()
    {
        try
        {
            wsClass obj = new wsClass();
            DataTable dt = obj.gettripagent(ddldepot.SelectedValue, ddlstation.SelectedValue, "O", "U", txtdate.Text);
            DataRow[] dtrow = dt.Select("minutes>=startminute and minutes <=endminute");
            if (dtrow.Length > 0)
            {
                grdUpcoming.Visible = true;
                grdUpcoming.DataSource = dtrow.CopyToDataTable();
                grdUpcoming.DataBind();
                pnlUpcoming.Visible = false;
                lbltotalCountupcoming.Text = "Total " + dtrow.Length;
                lbltotalCountupcoming.Visible = true;
                pnltodayDate.Visible = true;
            }
            else
            {
                lbltotalCountupcoming.Visible = false;
                pnlUpcoming.Visible = true;
                grdUpcoming.Visible = false;
            }
        }
        catch (Exception ex)
        { _common.ErrorLog("tripchartdash.aspx-0004", ex.Message.ToString()); }
    }
    private void loadPrepared()
    {
        try
        {
            wsClass obj = new wsClass();
            DataTable dt = obj.gettripagent(ddldepot.SelectedValue, ddlstation.SelectedValue, "O", "P", txtdate.Text);
            DataRow[] dtrow = dt.Select("minutes>=startminute and minutes <=endminute");
            if (dtrow.Length > 0)
            {
                grdPepared.Visible = true;
                grdPepared.DataSource = dtrow.CopyToDataTable();
                grdPepared.DataBind();
                pnlPrepared.Visible = false;
                lbltotalcountprepared.Text = "Total " + dtrow.Length;
                lbltotalcountprepared.Visible = true;
            }
            else
            {
                lbltotalcountprepared.Visible = false;
                pnlPrepared.Visible = true;
                grdPepared.Visible = false;
            }
        }
        catch (Exception ex)
        { _common.ErrorLog("tripchartdash.aspx-0005", ex.Message.ToString()); }
    }
    private void loadUnprepared()
    {
        try
        {
            wsClass obj = new wsClass();
            DataTable dt = obj.gettripagent(ddldepot.SelectedValue, ddlstation.SelectedValue, "O", "T", txtdate.Text);
            DataRow[] dtrow = dt.Select("minutes>=startminute and minutes <=endminute");
            if (dtrow.Length > 0)
            {
                grdUnprepared.Visible = true;
                grdUnprepared.DataSource = dtrow.CopyToDataTable();
                grdUnprepared.DataBind();
                pnlUnprepared.Visible = false;
                lbltotalcountunprepared.Text = "Total " + dtrow.Length;
                lbltotalcountunprepared.Visible = true;
            }
            else
            {
                lbltotalcountunprepared.Visible = false;
                pnlUnprepared.Visible = true;
                grdUnprepared.Visible = false;
            }
        }
        catch (Exception ex)
        { _common.ErrorLog("tripchartdash.aspx-0006", ex.Message.ToString()); }
    }
    private void openpage(string pagename)
    {
        tkt.Src = pagename;
        mpTripchart.Show();
    }
    private void GetDepoYN(string ofcid)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_currentyn");
            MyCommand.Parameters.AddWithValue("p_ofcid", ofcid);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["current_yn"].ToString() == "Y")
                    {
                        Session["currentyn"] = "Y";
                    }
                    else
                    {
                        Session["currentyn"] = "N";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("tripchartdash.aspx-0007", ex.Message.ToString());
        }
    }
    #endregion

    #region "Events"
    protected void Unnamed_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        mpgeneratechart.Show();
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        loadReadyTrip();
        loadUpcoming();
        loadPrepared();
        loadUnprepared();
    }
    protected void lbtnreset_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        ddlstation.SelectedValue = "0";
        ddldepot.SelectedValue = "0";
        txtdate.Text = "";
    }
    protected void grdready_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdready.PageIndex = e.NewPageIndex;
        loadReadyTrip();
    }
    protected void grdUpcoming_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdUpcoming.PageIndex = e.NewPageIndex;
        loadUpcoming();
    }
    protected void grdPepared_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdPepared.PageIndex = e.NewPageIndex;
        loadPrepared();
    }
    protected void grdUnprepared_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdUnprepared.PageIndex = e.NewPageIndex;
        loadUnprepared();
    }
    protected void grdready_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        CsrfTokenValidate();
        if (e.CommandName == "proceed")
        {
            int rowIndex = int.Parse(e.CommandArgument.ToString());
            string tripcode = grdready.DataKeys[rowIndex]["trip_code"].ToString();
            string depocode = grdready.DataKeys[rowIndex]["depo_code"].ToString();
            string busservicecodecode = grdready.DataKeys[rowIndex]["busservicetype_code"].ToString();
            string deposervicecode = grdready.DataKeys[rowIndex]["service_code"].ToString();
            Session["p_tripcode"] = tripcode;
            Session["p_depocode"] = depocode;
            Session["p_busservicecode"] = busservicecodecode;
            Session["p_deposervicecode"] = deposervicecode;

            //Response.Write(depocode);
            mpgeneratechart.Show();

        }
        if (e.CommandName == "view")
        {
            int rowIndex = int.Parse(e.CommandArgument.ToString());
            string tripcode = grdready.DataKeys[rowIndex]["trip_code"].ToString();
            Session["p_tripcode"] = tripcode;
            getTitle(Session["p_tripcode"].ToString());
            openpage("tripchart.aspx");
        }
    }
    protected void grdUpcoming_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        CsrfTokenValidate();
        if (e.CommandName == "view")
        {
            int rowIndex = int.Parse(e.CommandArgument.ToString());
            string tripcode = grdUpcoming.DataKeys[rowIndex]["trip_code"].ToString();
            Session["p_tripcode"] = tripcode;
            getTitle(Session["p_tripcode"].ToString());
            openpage("tripchart.aspx");
        }
    }
    protected void grdPepared_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        CsrfTokenValidate();
        if (e.CommandName == "VIEWDETAIL")
        {
            int rowIndex = int.Parse(e.CommandArgument.ToString());
            string tripcode = grdPepared.DataKeys[rowIndex]["trip_code"].ToString();
            Session["p_tripcode"] = tripcode;
            getTitle(Session["p_tripcode"].ToString());
            openpage("tripchart.aspx");
        }
    }
    protected void grdUnprepared_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        CsrfTokenValidate();
        if (e.CommandName == "VIEWDETAIL")
        {
            int rowIndex = int.Parse(e.CommandArgument.ToString());
            string tripcode = grdUnprepared.DataKeys[rowIndex]["trip_code"].ToString();
            Session["p_tripcode"] = tripcode;
            getTitle(Session["p_tripcode"].ToString());
            openpage("tripchart.aspx");
        }

    }
    private void getTitle(string tripcode)
    {
        wsClass obj = new wsClass();
        DataTable dt = obj.gettrip_details(tripcode);
        if (dt.Rows.Count > 0)
        {

            string servicecode = dt.Rows[0]["service_code"].ToString();
            string jdate = dt.Rows[0]["j_date"].ToString();

            string src = dt.Rows[0]["fstation_name"].ToString();
            string dest = dt.Rows[0]["tstation_name"].ToString();
            this.Title = servicecode + "_" + src + "_" + dest + "_" + jdate;

        }
    }
    protected void lbtnclosechart_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Response.Redirect("tripchartdash.aspx");
    }
    protected void grdready_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbtnproceed = (LinkButton)e.Row.FindControl("btnproceed");
            LinkButton lbtnview = (LinkButton)e.Row.FindControl("bntView");
            if (Session["_RoleCode"].ToString() == "1")
            {
                lbtnproceed.Visible = false;
                lbtnview.Visible = true;
            }
            else
            {
                lbtnproceed.Visible = true;
                lbtnview.Visible = false;
            }
        }
    }
    protected void grdUpcoming_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbtnview = (LinkButton)e.Row.FindControl("bntView");
            lbtnview.Visible = true;
            //if (Session["_RoleCode"].ToString() == "1")
            //{
            //    lbtnview.Visible = true;
            //}
            //else
            //{
            //    lbtnview.Visible = false;
            //}
        }
    }
    protected void ddldepot_SelectedIndexChanged(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        loadStation();
    }
    #endregion
}