using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_tripchartgenerate : System.Web.UI.Page
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (Session["p_tripcode"] == null)
            {
                //Response.Redirect("errorpage.aspx");
            }
            else
            {
                loadTripDetails(Session["p_tripcode"].ToString());
                if (Session["currentyn"].ToString() == "Y")
                {
                    ddlwaybill.Visible = true;
                    txtwaybill.Visible = false;
                }
                else
                {
                    ddlwaybill.Visible = false; ;
                    txtwaybill.Visible = true;
                }
                loadBus();
                loadDriver();
                loadConductor();
                loadDeposervice();
                lbltripcode.Text = "Trip Code " + Session["p_tripcode"].ToString();
            }
        }
    }



    #region "Methods"
    private void Errormsg(string msg)
    {
        string popup = _popup.modalPopupSmall("D", "Please Check", msg, "Close");
        Response.Write(popup);
    }
    private void Successmsg(string msg)
    {
        string popup = _popup.modalPopupSmall("S", "Congratulations", msg, "Close");
        Response.Write(popup);
    }

    private void InfoMsg(string msg)
    {
        string popup = _popup.modalPopupSmall("I", "Information", msg, "Close");
        Response.Write(popup);
    }
    private void loadConductor()
    {
        try
        {
            ddlconductor.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();

            DataTable dt;
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "current_booking.get_conductor");
            MyCommand.Parameters.AddWithValue("p_depotid", Session["p_depocode"].ToString());
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlconductor.DataSource = dt;
                    ddlconductor.DataTextField = "EMPNAME";
                    ddlconductor.DataValueField = "EMPCODE";
                    ddlconductor.DataBind();

                    ddlconductor2.DataSource = dt;
                    ddlconductor2.DataTextField = "EMPNAME";
                    ddlconductor2.DataValueField = "EMPCODE";
                    ddlconductor2.DataBind();
                }
            }
            ddlconductor.Items.Insert(0, "Select");
            ddlconductor.Items[0].Value = "0";
            ddlconductor.SelectedIndex = 0;

            ddlconductor2.Items.Insert(0, "Select");
            ddlconductor2.Items[0].Value = "0";
            ddlconductor2.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlconductor.Items.Insert(0, "Select");
            ddlconductor.Items[0].Value = "0";
            ddlconductor.SelectedIndex = 0;

            ddlconductor2.Items.Insert(0, "Select");
            ddlconductor2.Items[0].Value = "0";
            ddlconductor2.SelectedIndex = 0;
        }
    }
    private void loadDriver()
    {
        try
        {
            ddldriver.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();

            DataTable dt;
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "current_booking.get_driver");
            MyCommand.Parameters.AddWithValue("p_depotid", Session["p_depocode"].ToString());
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddldriver.DataSource = dt;
                    ddldriver.DataTextField = "EMPNAME";
                    ddldriver.DataValueField = "EMPCODE";
                    ddldriver.DataBind();

                    ddldriver2.DataSource = dt;
                    ddldriver2.DataTextField = "EMPNAME";
                    ddldriver2.DataValueField = "EMPCODE";
                    ddldriver2.DataBind();
                }
            }
            ddldriver.Items.Insert(0, "Select");
            ddldriver.Items[0].Value = "0";
            ddldriver.SelectedIndex = 0;

            ddldriver2.Items.Insert(0, "Select");
            ddldriver2.Items[0].Value = "0";
            ddldriver2.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddldriver.Items.Insert(0, "Select");
            ddldriver.Items[0].Value = "0";
            ddldriver.SelectedIndex = 0;

            ddldriver2.Items.Insert(0, "Select");
            ddldriver2.Items[0].Value = "0";
            ddldriver2.SelectedIndex = 0;
        }
    }
    private void loadBus()
    {
        try
        {
            ddlbus.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();

            DataTable dt;
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "current_booking.get_buses");
            MyCommand.Parameters.AddWithValue("p_depot", Session["p_depocode"].ToString());
            MyCommand.Parameters.AddWithValue("p_servicetypeid", Convert.ToInt32(Session["p_busservicecode"].ToString()));
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlbus.DataSource = dt;
                    ddlbus.DataTextField = "busregistrationNo";
                    ddlbus.DataValueField = "busregistrationNo";
                    ddlbus.DataBind();
                }
            }
            ddlbus.Items.Insert(0, "Select");
            ddlbus.Items[0].Value = "0";
            ddlbus.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlbus.Items.Insert(0, "Select");
            ddlbus.Items[0].Value = "0";
            ddlbus.SelectedIndex = 0;
        }
    }
    private void loadDeposervice()
    {
        try
        {

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();

            DataTable dt;
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_depotservice");
            MyCommand.Parameters.AddWithValue("p_search_text", Session["p_deposervicecode"].ToString());
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    Session["driver2"] = "N";
                    Session["conductor2"] = "N";
                    Session["noConductor"] = "N";
                    if (dt.Rows[0]["noofdriver"].ToString() == "2")
                    {
                        Session["driver2"] = "Y";
                        pnldriver.Visible = true;
                    }
                    if (dt.Rows[0]["noofconductor"].ToString() == "2")
                    {
                        Session["conductor2"] = "Y";
                        pnlconductor.Visible = true;
                    }
                    pnlNoconductor.Visible = true;
                    if (dt.Rows[0]["noofconductor"].ToString() == "0")
                    {
                        Session["noConductor"] = "Y";
                        pnlNoconductor.Visible = false;
                    }
                }
            }

        }
        catch (Exception ex)
        {

        }
    }
    private void generateTripChart()
    {
        try
        {
            wsClass ws = new wsClass();
            DataTable dt = ws.generate_trip_chart("Web", Session["p_tripcode"].ToString(), txtwaybill.Text, "0", DateTime.Now.ToString("dd/mm/yyyy"), ddlbus.SelectedValue,
                ddldriver.SelectedValue, ddldriver2.SelectedValue, ddlconductor.SelectedValue, ddlconductor2.SelectedValue, "", "", "", Session["_UserCode"].ToString());
            if (dt.Rows.Count > 0)
            {
                Session["conductorno"] = dt.Rows[0]["conductormob"].ToString();
                Response.Redirect("tripChartGenerateSuccess.aspx");
            }
            else
            {
                Errormsg("Something Went Wrong");
            }
        }
        catch (Exception ex)
        {
            Errormsg("Something Went Wrong");
        }
    }
    private void loadTripDetails(string tripcode)
    {
        wsClass obj = new wsClass();
        DataTable dtt = obj.gettrip_psngr_details(tripcode);
        if (dtt.Rows.Count > 0)
        {
            grdtripdetails.DataSource = dtt;
            grdtripdetails.DataBind();
            pnltripdetails.Visible = false;
        }
        else
        {
            grdtripdetails.Visible = false;
            pnltripdetails.Visible = true;
        }
    }
    #endregion
    #region "Events"
    protected void lbtngenerate_Click(object sender, EventArgs e)
    {


        if (Session["noConductor"].ToString() == "N")
        {
            if (ddlconductor.SelectedValue == "0")
            {
                Errormsg("Please Select Conductor");
                return;
            }
        }
        if (Session["conductor2"].ToString() == "Y")
        {
            if (ddlconductor2.SelectedValue == "0")
            {
                Errormsg("Please Select Second Conductor");
                return;
            }
        }
        if (ddldriver.SelectedValue == "0")
        {
            Errormsg("Please Select Driver");
            return;
        }
        if (Session["driver2"].ToString() == "Y")
        {
            if (ddldriver2.SelectedValue == "0")
            {
                Errormsg("Please Select Second Driver");
                return;
            }
        }
        if (ddlbus.SelectedValue == "0")
        {
            Errormsg("Please Select Bus");
            return;
        }
        generateTripChart();
    }
    #endregion
}