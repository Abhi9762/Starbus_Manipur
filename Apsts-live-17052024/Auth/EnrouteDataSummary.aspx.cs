using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Auth_EnrouteDataSummary : System.Web.UI.Page
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
        Session["_moduleName"] = "En-Route Booking Summary";
        // 137 dsvcid
        // 200220240001 waybill no
        if (!IsPostBack)
        {
            //Session["_WaybillNo"] = "270220240007";
            LoadWaybillDetails(Session["_WaybillNo"].ToString());
            loadAmountFields(Session["_WaybillNo"].ToString());
            loadWaybillTrips(Session["_WaybillNo"].ToString());
            
        }
    }

    private void loadTicketsOnMap(string waybillno, string tripno)
    {
        try
        {

            dvmap.Visible = false;
            dvnomap.Visible = true;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "livedata.f_get_enroute_tickets_latlong");
            MyCommand.Parameters.AddWithValue("p_waybill_no", waybillno);
            MyCommand.Parameters.AddWithValue("p_trip_no", Convert.ToInt32(tripno));
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    loadmapview(dt);
                    dvmap.Visible = true;
                    dvnomap.Visible = false;
                }
                else
                {
                    dvmap.Visible = false;
                    dvnomap.Visible = true;
                }
            }
            else
            {
                dvmap.Visible = false;
                dvnomap.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message);
        }
    }

    private void loadmapview(DataTable dt_output)
    {
        string mapstring = "";
        for (int i = 0; i < dt_output.Rows.Count; i++)
        {
            mapstring = mapstring + @"{
                      type: 'Feature',
                       properties:
                      {
                        htmlPopup: '" + dt_output.Rows[i]["pnr"].ToString() + @"',
                      },
                      geometry:
                        {
                     type: 'Point',
                                              coordinates: [" + dt_output.Rows[i]["lat"].ToString() + @",
                                                            " + dt_output.Rows[i]["long"].ToString() + @"],
                                          }},
                                      ";
        }

        // Register the startup script to load the external script
        //ayush key ClientScript.RegisterStartupScript(this.GetType(), "MapScript", "<script src='https://apis.mappls.com/advancedmaps/api/eafa9040d3fbf1a180dff2b963a2927c/map_sdk?layer=vector&v=3.0&callback=initMap1'></script>");
        ClientScript.RegisterStartupScript(this.GetType(), "MapScript", "<script src='https://apis.mappls.com/advancedmaps/api/9479070429bebdb58d2b038648fbc539/map_sdk?layer=vector&v=3.0&callback=initMap1'></script>");

        // Embed the JavaScript code within the code-behind
        ClientScript.RegisterStartupScript(this.GetType(), "MapInitScript", @"
                    <script>
                        var map;

                        function initMap1() {
                            HideLoading();
                            map = new mappls.Map('map', {
                                //center: [28.61, 77.23],
                                zoomControl: true,
                                location: true,
                            });
                            map.addListener('load', function () {
                                var geoData = {
                                    type: 'FeatureCollection',
                                    features: [

                                        " + mapstring + @"
                                     
                                    ],
                                };
                                var marker = mappls.Marker({
                                    map: map,
                                    position: geoData,
                                    icon_url: 'https://apis.mapmyindia.com/map_v3/2.png',
                                    fitbounds: true,
                                    clusters: true,
                                    clustersIcon: 'https://mappls.com/images/2.png',
                                    fitboundOptions: {
                                        padding: 120,
                                        duration: 1000,
                                    },
                                    popupOptions: {
                                        offset: { bottom: [0, -20] },
                                    },
                                });
                            });
                        }
                        // Call the function when the page loads
                        window.onload = initMap1;
                    </script>
                ");
    }

    #region"Methods"
    private void Errormsg(string msg)
    {
        string popup = _popup.modalPopupSmall("W", "Please Note", msg, "Close");
        Response.Write(popup);
    }
    private void Successmsg(string msg)
    {
        string popup = _popup.modalPopupSmall("S", "Success", msg, "OK");
        Response.Write(popup);
    }
    private void ConfirmMsg(string msg)
    {
        //lblConfirmation.Text = msg;
        //mpConfirmation.Show();
    }
    private void InfoMsg(string msg)
    {
        string popup = _popup.modalPopupLarge("I", "Information", msg, "Close");
        Response.Write(popup);
    }
    private void LoadWaybillDetails(string waybillno)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_dutytripforetmsubmission");
            MyCommand.Parameters.AddWithValue("p_waybillno", waybillno);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    tbWaybillNo.Text = dt.Rows[0]["DUTYREFNO"].ToString();
                    lblServiceName.Text = dt.Rows[0]["service_name"].ToString();
                    lblBusNo.Text = dt.Rows[0]["BUSNO"].ToString();
                    //lblETMNo.Text = dt.Rows[0]["etmserialno"].ToString();
                    lblDriver.Text = dt.Rows[0]["DRIVER"].ToString();
                    lblConductor.Text = dt.Rows[0]["CONDUCTOR"].ToString();
                    //pnlNoRecord.Visible = false;
                    //loadAmountFields(tbWaybillNo.Text);
                    //loadWaybillTrips(tbWaybillNo.Text);
                }
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message);
        }
    }
    private void loadAmountFields(string waybill)
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "livedata.f_get_waybill_summary");
        MyCommand.Parameters.AddWithValue("p_waybill_no", waybill);
        dt = bll.SelectAll(MyCommand);

        if (dt.TableName == "Success")
        {
            if (dt.Rows.Count > 0)
            {
                tbOnlineTktamt.Text = Convert.ToDecimal(dt.Rows[0]["online_ticket_amount"].ToString()).ToString("0.##") + " ₹";

                tbEnrouteTktamt.Text = Convert.ToDecimal(dt.Rows[0]["enroute_amount"].ToString()).ToString("0.##") + " ₹";
                tbenroutecash.Text = Convert.ToDecimal(dt.Rows[0]["cashticket_amt"].ToString()).ToString("0.##") + " ₹";
                tbenrouteqr.Text = Convert.ToDecimal(dt.Rows[0]["qrticket_amt"].ToString()).ToString("0.##") + " ₹";
                tbRefundedTickets.Text = Convert.ToDecimal(dt.Rows[0]["refundamount"].ToString()).ToString("0.##") + " ₹";
                tbDhabaAmt.Text = Convert.ToDecimal(dt.Rows[0]["dhaba_amount"].ToString()).ToString("0.##") + " ₹";

                tbOtherEarningAmt.Text = Convert.ToDecimal(dt.Rows[0]["extra_earning"].ToString()).ToString("0.##") + " ₹";
                tbotherEarningcash.Text = Convert.ToDecimal(dt.Rows[0]["other_earning_cash_amt"].ToString()).ToString("0.##") + " ₹";
                tbotherEarningqr.Text = Convert.ToDecimal(dt.Rows[0]["other_earning_qr_amt"].ToString()).ToString("0.##") + " ₹";

                tbTotalEarningAmt.Text = Convert.ToDecimal(dt.Rows[0]["total_earning"].ToString()).ToString("0.##") + " ₹";
                tbtotalcollectioncash.Text = Convert.ToDecimal(Convert.ToDecimal(dt.Rows[0]["total_earning"].ToString()) - (Convert.ToDecimal(dt.Rows[0]["qrticket_amt"].ToString()) + Convert.ToDecimal(dt.Rows[0]["other_earning_qr_amt"].ToString()) + Convert.ToDecimal(dt.Rows[0]["qrluggage_amt"].ToString()))).ToString("0.##") + " ₹";
                tbtotalcollectionqr.Text = (Convert.ToDecimal(dt.Rows[0]["qrticket_amt"].ToString()) + Convert.ToDecimal(dt.Rows[0]["other_earning_qr_amt"].ToString()) + Convert.ToDecimal(dt.Rows[0]["qrluggage_amt"].ToString())).ToString() + " ₹";

                tbLuggageAmt.Text = Convert.ToDecimal(dt.Rows[0]["luggage_amount"].ToString()).ToString("0.##") + " ₹";
                tbluggagecash.Text = Convert.ToDecimal(dt.Rows[0]["cashluggage_amt"].ToString()).ToString("0.##") + " ₹";
                tbluggageqr.Text = Convert.ToDecimal(dt.Rows[0]["qrluggage_amt"].ToString()).ToString("0.##") + " ₹";

                tbTollpaid.Text = Convert.ToDecimal(dt.Rows[0]["toll_amount"].ToString()).ToString("0.##") + " ₹";
                tbParking.Text = Convert.ToDecimal(dt.Rows[0]["parking_amount"].ToString()).ToString("0.##") + " ₹";
                tbOtherExp.Text = Convert.ToDecimal(dt.Rows[0]["other_exp_amount"].ToString()).ToString("0.##") + " ₹";
                tbTotalExpenses.Text = Convert.ToDecimal(dt.Rows[0]["total_expense"].ToString()).ToString("0.##") + " ₹";

                lblNoofInspection.Text = Convert.ToInt32(dt.Rows[0]["no_of_inspections"].ToString()).ToString();
                lblokOnspection.Text = Convert.ToInt32(dt.Rows[0]["no_of_ok_inspections"].ToString()).ToString();
                lblwtInspections.Text = Convert.ToInt32(dt.Rows[0]["no_of_wt_inspections"].ToString()).ToString();

                lbltotalConcession.Text = Convert.ToInt32(dt.Rows[0]["no_of_concessions"].ToString()).ToString();
                lblConcessiontktamt.Text = Convert.ToDecimal(dt.Rows[0]["concessionamount"].ToString()).ToString("0.##") + " ₹";
                lblconcessiondiscountamt.Text = Convert.ToDecimal(dt.Rows[0]["discountamount"].ToString()).ToString("0.##") + " ₹";


            }
        }
    }
    private void loadWaybillTrips(string waybill)
    {
        gvServiceTrips.Visible = false;
        pnlNoRecordTrip.Visible = false;
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "livedata.f_get_waybilltrips_summary");
        MyCommand.Parameters.AddWithValue("p_waybill_no", waybill);
        dt = bll.SelectAll(MyCommand);

        if (dt.TableName == "Success")
        {
            if (dt.Rows.Count > 0)
            {
                gvServiceTrips.DataSource = dt;
                gvServiceTrips.DataBind();
                gvServiceTrips.Visible = true;
                pnlNoRecordTrip.Visible = false;
            }
            else
            {
                gvServiceTrips.Visible = false;
                pnlNoRecordTrip.Visible = true;
            }
        }
    }
    #endregion

    #region"Events"
    protected void lbtnGoback_Click(object sender, EventArgs e)
    {
        Response.Redirect("LiveDataDashboard.aspx");
    }
    protected void gvServiceTrips_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label tbEnrouteTktamt = (Label)e.Row.FindControl("tbEnrouteTktamt");
            Label tbenroutecasht = (Label)e.Row.FindControl("tbenroutecasht");
            Label tbenrouteqrt = (Label)e.Row.FindControl("tbenrouteqrt");
            Label lbltotluggaget = (Label)e.Row.FindControl("lbltotluggaget");
            Label lblcashloggaget = (Label)e.Row.FindControl("lblcashloggaget");
            Label lblqrluggaget = (Label)e.Row.FindControl("lblqrluggaget");
            Label lblotherearningt = (Label)e.Row.FindControl("lblotherearningt");
            Label lblcashearningt = (Label)e.Row.FindControl("lblcashearningt");
            Label lblqrearningt = (Label)e.Row.FindControl("lblqrearningt");
            Label lblonlinetickett = (Label)e.Row.FindControl("lblonlinetickett");
            Label lblrefundedt = (Label)e.Row.FindControl("lblrefundedt");
            Label lbltotexpenset = (Label)e.Row.FindControl("lbltotexpenset");

            DataRowView rowView = (DataRowView)e.Row.DataItem;
            tbEnrouteTktamt.Text = Convert.ToDecimal(rowView["total_ticket"].ToString()).ToString("0.##");
            tbenroutecasht.Text = Convert.ToDecimal(rowView["cash_ticket"].ToString()).ToString("0.##");
            tbenrouteqrt.Text = Convert.ToDecimal(rowView["qr_ticket"].ToString()).ToString("0.##");
            lbltotluggaget.Text = Convert.ToDecimal(rowView["total_luggage"].ToString()).ToString("0.##");
            lblcashloggaget.Text = Convert.ToDecimal(rowView["cash_luggage"].ToString()).ToString("0.##");
            lblqrluggaget.Text = Convert.ToDecimal(rowView["qr_luggage"].ToString()).ToString("0.##");
            lblotherearningt.Text = Convert.ToDecimal(rowView["totalother_earning"].ToString()).ToString("0.##");
            lblcashearningt.Text = Convert.ToDecimal(rowView["cash_earning"].ToString()).ToString("0.##");
            lblqrearningt.Text = Convert.ToDecimal(rowView["qr_earning"].ToString()).ToString("0.##");
            lblonlinetickett.Text = Convert.ToDecimal(rowView["online_tickets"].ToString()).ToString("0.##");
            lblrefundedt.Text = Convert.ToDecimal(rowView["refunded_ticket"].ToString()).ToString("0.##");
            lbltotexpenset.Text = Convert.ToDecimal(rowView["total_exp"].ToString()).ToString("0.##");
        }
    }
    protected void gvServiceTrips_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "view")
        {
            string waybill = gvServiceTrips.DataKeys[index].Values["waybill"].ToString();
            string tripno = gvServiceTrips.DataKeys[index].Values["tripno"].ToString();
           // loadTicketsOnMap("270220240007", 1.ToString());
            loadTicketsOnMap(waybill, tripno);
        }
    }
    #endregion





}