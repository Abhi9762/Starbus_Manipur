using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_LiveDataDashboard : System.Web.UI.Page
{
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    sbValidation _validation = new sbValidation();
    private sbCommonFunc _common = new sbCommonFunc();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["_moduleName"] = "Live Booking & Bus Pass Summary";

            txttrnsdate.Text = DateTime.Now.Date.ToString("dd") + "/" + DateTime.Now.ToString("MM") + "/" + DateTime.Now.Year.ToString();
            loadEnrouteSummary();
            loadenRoute();
            LoadTicketBooked();
            LoadSeatBooked();
            LoadRevenue();
            LoadTravellerOnlineBooking();
            LoadCounterOnlineBooking();
            LoadAgentOnlineBooking();
            loadTicketseats();
            loadCntrticket();
            loadAgentticket();
            //LoadBusPasses();
        }
    }

    #region Methods
    private void loadEnrouteSummary()
    {try
        {
            pnEnrouteData.Visible = false;
            pnNoData.Visible = true;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "livedata.f_enroute_summary");
            MyCommand.Parameters.AddWithValue("@p_search", txttrnsdate.Text);
            dt = bll.SelectAll(MyCommand);

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["servicecount"].ToString() != "0" && dt.Rows[0]["tripcount"].ToString() != "0")
                {
                    pnEnrouteData.Visible = true;
                    pnNoData.Visible = false;
                    lbltotalservice.Text = dt.Rows[0]["servicecount"].ToString();
                    lbltotaltrips.Text = dt.Rows[0]["tripcount"].ToString();
                    lbltotalpassenger.Text = dt.Rows[0]["Passengercount"].ToString();
                    lbladultamt.Text =Convert.ToDecimal( dt.Rows[0]["amountadult"].ToString()).ToString("0.##");
                    lblchildamt.Text = Convert.ToDecimal(dt.Rows[0]["amountchild"].ToString()).ToString("0.##");
                    lbltotalpassengeramt.Text = Convert.ToDecimal(dt.Rows[0]["total_Passengeramount"].ToString()).ToString("0.##");
                    lblluggagecount.Text = dt.Rows[0]["Luggagecount"].ToString();
                    lblluggageamt.Text = Convert.ToDecimal(dt.Rows[0]["total_Luggageamount"].ToString()).ToString("0.##");
                    lblconcessioncount.Text = dt.Rows[0]["concessioncount"].ToString();
                    lblconcessionamt.Text = Convert.ToDecimal(dt.Rows[0]["total_Concessionamount"].ToString()).ToString("0.##");
                }
                else
                {
                    pnEnrouteData.Visible = false;
                    pnNoData.Visible = true;
                }
            }
            else
            {
                pnEnrouteData.Visible = false;
                pnNoData.Visible = true;
            }

        }
        catch (Exception ex)
        {

        }
    }
    private void loadenRoute()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "livedata.f_enroute");
            MyCommand.Parameters.AddWithValue("@p_date", txttrnsdate.Text);
            dt = bll.SelectAll(MyCommand);

            if (dt.Rows.Count > 0)
            {
                rptEnroute.DataSource = dt;
                rptEnroute.DataBind();
                rptEnroute.Visible = true;
            }

        }
        catch (Exception ex)
        {

        }
    }



    private void LoadTicketBooked()//M6
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "livedata.f_tktbooked");
            MyCommand.Parameters.AddWithValue("@p_date", txttrnsdate.Text);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    lbltotaltktbooked.Text = dt.Rows[0]["total_tkt"].ToString();
                    rpttktbooked.DataSource = dt;
                    rpttktbooked.DataBind();
                    rpttktbooked.Visible = true;
                }
            }


        }
        catch (Exception ex)
        {

        }
    }

    private void LoadSeatBooked()//M6
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "livedata.f_seatbooked");
            MyCommand.Parameters.AddWithValue("@p_date", txttrnsdate.Text);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    lbltotalseatbooked.Text = dt.Rows[0]["total_bookedseat"].ToString() + "/" + dt.Rows[0]["ttotal_seat"].ToString();
                    rptseatbooked.DataSource = dt;
                    rptseatbooked.DataBind();
                    rptseatbooked.Visible = true;
                }
            }


        }
        catch (Exception ex)
        {

        }
    }


    private void LoadRevenue()//M6
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "livedata.f_revenue");
            MyCommand.Parameters.AddWithValue("@p_date", txttrnsdate.Text);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    lbltotalRevenue.Text = dt.Rows[0]["val_totalrevenue"].ToString() + " ₹";
                    lbtnfare.Text = dt.Rows[0]["val_fare"].ToString() + " ₹ <br /><span style='font-size: 14px;'>FARE</span>";
                    lbtntax.Text = dt.Rows[0]["val_tax"].ToString() + " ₹ <br /><span style='font-size: 14px;'>TAX</span>";
                    lbtnreservation.Text = dt.Rows[0]["val_reservation"].ToString() + " ₹ <br /><span style='font-size: 14px;'>RESERVATION</span>";
                }
            }


        }
        catch (Exception ex)
        {

        }
    }

    private void LoadCounterOnlineBooking()//M6
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "livedata.f_totalcountertkt");
            MyCommand.Parameters.AddWithValue("@p_date", txttrnsdate.Text);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    lblcntrtkt.Text = dt.Rows[0]["total_countertkt"].ToString();
                    rptcounter.DataSource = dt;
                    rptcounter.DataBind();
                    rptcounter.Visible = true;
                }
            }


        }
        catch (Exception ex)
        {

        }
    }

    private void LoadTravellerOnlineBooking()//M6
    {
        try
        {
            rptwebapp.Visible = false;
            lbltotalwebapp.Text = "0";
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "livedata.f_tvlrtktbooked");
            MyCommand.Parameters.AddWithValue("@p_date", txttrnsdate.Text);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    lbltotalwebapp.Text = dt.Rows[0]["totalwebtkt"].ToString() + "/" + dt.Rows[0]["totalapptkt"].ToString();
                    rptwebapp.DataSource = dt;
                    rptwebapp.DataBind();
                    rptwebapp.Visible = true;
                }
            }


        }
        catch (Exception ex)
        {

        }
    }


    private void LoadAgentOnlineBooking()//M6
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "livedata.f_agnttktbooked");
            MyCommand.Parameters.AddWithValue("@p_date", txttrnsdate.Text);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    lblagnttkt.Text = dt.Rows[0]["total_agenttkt"].ToString();
                    rptagent.DataSource = dt;
                    rptagent.DataBind();
                    rptagent.Visible = true;
                }
            }


        }
        catch (Exception ex)
        {

        }
    }


    private void loadTicketseats()//M6
    {
        try
        {
            rptcurnttktseat.Visible = false;
            lblcurnttottktseats.Text = "0";
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "livedata.f_crnttktbooked");
            MyCommand.Parameters.AddWithValue("@p_date", txttrnsdate.Text);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    lblcurnttottktseats.Text = dt.Rows[0]["total_tkt"].ToString() + "/" + dt.Rows[0]["total_bookedseat"].ToString() + "/" + dt.Rows[0]["total_buses"].ToString();
                    rptcurnttktseat.DataSource = dt;
                    rptcurnttktseat.DataBind();
                    rptcurnttktseat.Visible = true;
                }
            }


        }
        catch (Exception ex)
        {

        }
    }


    private void loadCntrticket()//M6
    {
        try
        {
            rptcurntcntrtktrenue.Visible = false;
            lblcurntcntrtktrenue.Text = "0";
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "livedata.f_CrntCntrTktBooked");
            MyCommand.Parameters.AddWithValue("@p_date", txttrnsdate.Text);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    lblcurntcntrtktrenue.Text = dt.Rows[0]["total_tkt"].ToString() + "/" + dt.Rows[0]["total_revenue"].ToString() + "₹" + "/" + dt.Rows[0]["total_buses"].ToString();
                    rptcurntcntrtktrenue.DataSource = dt;
                    rptcurntcntrtktrenue.DataBind();
                    rptcurntcntrtktrenue.Visible = true;
                }
            }


        }
        catch (Exception ex)
        {

        }
    }

    private void loadAgentticket()//M6
    {
        try
        {
            rptagntcntrtktrenuecomi.Visible = false;
            lblagntcntrtktrenuecomi.Text = "0";
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "livedata.f_CrntAgntTktBooked");
            MyCommand.Parameters.AddWithValue("@p_date", txttrnsdate.Text);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    lblagntcntrtktrenuecomi.Text = dt.Rows[0]["total_tkt"].ToString() + "/" + dt.Rows[0]["total_revenue"].ToString() + "₹" + "/" + dt.Rows[0]["total_commission"].ToString() + "₹" + "/" + dt.Rows[0]["total_buses"].ToString();
                    rptagntcntrtktrenuecomi.DataSource = dt;
                    rptagntcntrtktrenuecomi.DataBind();
                    rptagntcntrtktrenuecomi.Visible = true;
                }
            }


        }
        catch (Exception ex)
        {

        }
    }

    //private void LoadBusPasses()//M6
    //{
    //    try
    //    {

    //        MyCommand = new NpgsqlCommand();
    //        MyCommand.Parameters.Clear();
    //        MyCommand.Parameters.AddWithValue("@StoredProcedure", "livedata.f_BusPasses");
    //        MyCommand.Parameters.AddWithValue("@p_date", txttrnsdate.Text);
    //        dt = bll.SelectAll(MyCommand);
    //        if (dt.TableName == "Success")
    //        {
    //            if (dt.Rows.Count > 0)
    //            {
    //                lbltotalApplied.Text = dt.Rows[0]["apply_mst"] + dt.Rows[0]["apply_other"].ToString();
    //                lbtnAppliedMST.Text = dt.Rows[0]["apply_mst"].ToString() + "<br/>MST";
    //                lbtnAppliedohter.Text = dt.Rows[0]["apply_other"].ToString() + "<br/>Other";

    //                lbltotalIssued.Text = dt.Rows[0]["issue_mst"] + dt.Rows[0]["issue_other"].ToString();
    //                lbtnIssuedMST.Text = dt.Rows[0]["issue_mst"].ToString() + "<br/>MST";
    //                lbtnIssuedOther.Text = dt.Rows[0]["issue_other"].ToString() + "<br/>Other";

    //                lbltotalrevenueamt.Text = dt.Rows[0]["revenue_mst"] + dt.Rows[0]["revenue_other"].ToString();
    //                lbtnrevenueamtMST.Text = dt.Rows[0]["revenue_mst"].ToString() + "<br/>MST";
    //                lbtnrevenueamtOther.Text = dt.Rows[0]["revenue_other"].ToString() + "<br/>Other";
    //            }
    //        }


    //    }
    //    catch (Exception ex)
    //    {

    //    }
    //}


    #endregion
    protected void lbtnshow_Click(object sender, EventArgs e)
    {
        loadEnrouteSummary();
        loadenRoute();
        LoadTicketBooked();
        LoadSeatBooked();
        LoadRevenue();
        LoadTravellerOnlineBooking();
        LoadCounterOnlineBooking();
        LoadAgentOnlineBooking();
        loadTicketseats();
        loadCntrticket();
        loadAgentticket();
        // LoadBusPasses();
    }
    protected void lbtnenRouterefresh_Click(object sender, EventArgs e)
    {
        loadEnrouteSummary();
        loadenRoute();
    }
    protected void rptEnroute_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "ViewSummary")
        {
            HiddenField hdDsvcId = (HiddenField)e.Item.FindControl("hdDsvcId");
            HiddenField hdWaybillNo = (HiddenField)e.Item.FindControl("hdWaybillNo");
            Session["_TransactionDate"] = txttrnsdate.Text;
            Session["_DsvcId"] = hdDsvcId.Value;
            Session["_WaybillNo"] = hdWaybillNo.Value;
            Response.Redirect("EnrouteDataSummary.aspx");
        }
    }
}