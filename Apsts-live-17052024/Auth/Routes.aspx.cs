using System;
using System.Collections.Generic;

using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Npgsql;
using System.IO;
using System.Xml;
using System.Collections;
public partial class Routes : System.Web.UI.Page
{
    sbSummary obj = new sbSummary();
    private NpgsqlCommand MyCommand;

    DataTable dt = new DataTable();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["heading"] = "Routes";
        if (!IsPostBack)
        {

            loadPopularRoutes();
            getRoutes();
        }
    }
    private void loadPopularRoutes()
    {
        sbSummary obj = new sbSummary();
        DataTable dt = obj.getRouteBooking(5, "T");
        if (dt.Rows.Count > 0)
        {
            rptTopRoutes.DataSource = dt;
            rptTopRoutes.DataBind();
        }
    }
    protected void rptTopRoutes_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item)
        {
            LinkButton lbtnroute = (LinkButton)e.Item.FindControl("lbtnroute");
            HiddenField frmstation = (HiddenField)e.Item.FindControl("hdfromStation1");
            HiddenField tostation = (HiddenField)e.Item.FindControl("hdfromStation2");
            loadServices(frmstation.Value, tostation.Value);
        }
    }
    private void loadServices(string fromStationName, string toStationName)
    {
        try
        {
            string current_date = DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("yyyy");

            wsClass obj = new wsClass();
            DataTable dt = new DataTable();
            dt = obj.search_services_dt(fromStationName, toStationName, "0", current_date);
            if (dt.Rows.Count > 0)
            {
                Session["fromStationName"] = fromStationName;
                Session["toStationName"] = toStationName;
                Session["date"] = current_date;
                Response.Redirect("seatavailablity.aspx");
            }
            else
            {
                Errormsg("Sorry, Services are not available now for selected route");
            }
        }
        catch (Exception ex)
        {
            Errormsg("Sorry, Services are not available now for selected route");
        }
    }
    private void Errormsg(string msg)
    {
        string popup = _popup.modalPopupSmall("S", "Information", msg, "Close");
        Response.Write(popup);
    }
    private void getRoutes()//M4
    {
        try
        {

            gvRoutes.Visible = false;

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_routesfor_homepage");

            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                gvRoutes.DataSource = dt;
                gvRoutes.DataBind();

                gvRoutes.Visible = true;



            }
            else
            {
                gvRoutes.DataSource = dt;
                gvRoutes.DataBind();
            }
        }
        catch (Exception ex)
        {
            gvRoutes.DataSource = dt;
            gvRoutes.DataBind();
            Errormsg("There is some error" + ex.Message);
            _common.ErrorLog("Route-P1", ex.Message.ToString());
        }
    }

  
    protected void gvRoutes_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
          

            
                e.Row.ToolTip = "Click to find Bus Services of the route";
                e.Row.Attributes.Add("onMouseOver", "this.className='Rowhighlight';");
                e.Row.Attributes.Add("onMouseout", "this.className='Rowdefaultcolor';");
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink((GridView)sender, "Select$" + e.Row.RowIndex);
            
        }
    }

    protected void gvRoutes_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Label lblfromStationName = (Label)gvRoutes.SelectedRow.FindControl("lblfromstation");
            Label lbltoStationName = (Label)gvRoutes.SelectedRow.FindControl("lbltostation");
            string frmstation = lblfromStationName.Text;
            string tostation = lbltoStationName.Text;

            loadServices(frmstation, tostation);
        }
        catch (Exception ex)
        { }



        

    }
    
}