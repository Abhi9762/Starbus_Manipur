

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

public partial class Auth_AgCurrentBooking : BasePage
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    string e_key = "Utc#ver4@2022Key";
    classCounter clsCounter = new classCounter();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

                string ss = "<span style='font-size: 14px;line-height: 20px;'> Agent <br/> " + Session["_UserName"].ToString() + Session["_UserCode"].ToString() + " <br/> " + Session["_LStationName"].ToString() + "<br/><i class='fa fa-envelope'></i>" + Session["Emailid"].ToString() + "<br/> <i class='fa fa-mobile'></i> " + Session["MobileNo"].ToString() + "</span>";
                lblusr.Text = ss;

                lblpndgdepodate.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");

                loadWallet(Session["_UserCode"].ToString());
                loadservicetype(ddlservicetype);
                loadCntrOfflinetkttoday();

                loaddepot(ddldepot);
                loadbuses(ddlservicetype.SelectedValue, ddlbus);
                loadbusroute(ddlservicetype.SelectedValue, ddlroute);
                loadTrips(ddlservicetype.SelectedValue, ddlroute.SelectedValue, ddljtype);
                loadConductor(ddlbus.SelectedValue, ddlconductor);
                txtdepartdate.Text = DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("yyyy");
                resetctrl();
                Session["depotCurrentyn"] = GetDepotYN();
                if (Session["depotCurrentyn"].ToString() == "Y")
                {
                    getWaybillsList(ddlWaybills);
                    getWaybillDetails(ddlWaybills.SelectedValue);
                    ddlWaybills.Visible = true;
                    txtwaybills.Visible = false;
                    pnlnowaybill.Visible = false;
                    //pnltype.Visible = false;
                }
                else
                {
                    ddlWaybills.Visible = false;
                    txtwaybills.Visible = true;
                }

                calendarDateF.StartDate = DateTime.Now;
                calendarDateF.EndDate = DateTime.Now.AddDays(1);

                Session["_SeatLayout"] = "12-4-0";
                if (!(Session["OpenTripID"] == null && Session["OpenSERVICE_CODE"] == null && Session["OpenREGISTRATIONNUMBER"] == null))
                {
                    CheckTripOpenorNot();
                }
                if (Session["p_tripcode"] == null)
                {

                }
                else
                {
                    if (Session["p_tripcode"].ToString() != null && Session["p_tripcode"].ToString() != "")
                    {
                        OpenTripDetails(Session["p_tripcode"].ToString());
                    }
                }




                loadConcession(ddlconcession, ddlservicetype.SelectedValue, ddlroute.SelectedValue, Session["_LStationCode"].ToString());
            }
            hfSeatStructUp.Value = Session["_SeatLayout"].ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "TicketLoad();", true);

        }
        catch (Exception ex)
        {
            //Response.Redirect("sessionTimeout.aspx");
        }
    }
    private void loadAgentCommission(string servicetype)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "current_booking.f_get_agentCommission");
            MyCommand.Parameters.AddWithValue("p_strpid", Convert.ToInt64( servicetype));
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    string amount = dt.Rows[0]["commission_amt"].ToString();
                    hfAgentCommission.Value = amount;
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    private void loadCntrOfflinetkttoday()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "current_booking.f_get_trip_summary");
            MyCommand.Parameters.AddWithValue("sp_date", DateTime.Now.Date.ToString("dd/MM/yyyy"));
            MyCommand.Parameters.AddWithValue("sp_cntr_id", Session["_UserCode"].ToString());
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    lbltodayttkt.Text = dt.Rows[0]["sp_TodayTrip"].ToString();
                    lbltodaytktamt.Text = dt.Rows[0]["sp_TodayFare"].ToString();
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    private void loadWallet(string userId)
    {
        try
        {
            classWalletAgent obj = new classWalletAgent();
            DataTable dtWallet = obj.GetWalletDetailDt(userId);
            if (dtWallet.Rows.Count > 0)
            {
                lblbalance.Text = "Your Wallet Balance <b>" + dtWallet.Rows[0]["currentbalanceamount"].ToString() + "</b>";
                //if ((int)dtWallet.Rows[0]["val_amount"] == 0)
                //{
                //    lblWalletLastUpdate.Text = "( Recharge your wallet first time )";
                //}
                //else
                //{
                //    lblWalletLastUpdate.Text = "( Last Wallet Recharge " + dtWallet.Rows[0]["d_date"].ToString() + " with ₹" + dtWallet.Rows[0]["val_amount"].ToString() + " <i class='fa fa-rupee-sign'></i> )";
                //}
            }
            else
            {
                lblbalance.Text = "NA";
                lblbalance.Text = "NA (Please refresh page)";
            }
        }
        catch (Exception)
        {
            // Handle the exception here if needed
        }
    }
    private void CheckTripOpenorNot()
    {
        try
        {
            if (!(Session["OpenWaybillnumber"] == null))
            {
                ddlWaybills.SelectedValue = Session["OpenWaybillnumber"].ToString();
                getWaybillDetails(ddlWaybills.SelectedValue);

                if (!(Session["OpenTripID"] == null))
                {
                    //ddltrip.SelectedValue = Session["OpenTripID"].ToString();
                    //ddltrip.Enabled = false;
                }
                else
                {
                    // ddltrip.Enabled = true;
                }
                loadRoute();
                if (hfRouteId.Value == "0")
                {
                    btnPrintTicket.Visible = false;
                    btnTripChart.Visible = false;
                }
                else
                    btnPrintTicket.Visible = true;
                ddlWaybills.Enabled = false;
            }
            else
            {
                ddlWaybills.SelectedValue = "0";
                ddlWaybills.Enabled = true;
            }
            //  TirpOpenYN(Session["OpenWaybillnumber"].ToString());
        }
        catch (Exception ex)
        {
        }
    }
    private void Errormsg(string msg)
    {
        string popup = _popup.modalPopupSmall("D", "Please Check", msg, "Close");
        Response.Write(popup);
    }

    #region "Method"
    private void OpenTripDetails(string v)
    {
        try
        {
            DataTable dt1;
            dt1 = clsCounter.GetOpenTripDetails(Session["p_tripcode"].ToString(), Session["_UserCode"].ToString());
            if (dt1.TableName == "Success")
            {
                if (dt1.Rows.Count > 0)
                {
                    txtwaybills.Text = dt1.Rows[0]["waybillno"].ToString();
                    txtwaybills.Enabled = false;
                    loadservicetype(ddlservicetype);
                    ddlservicetype.SelectedValue = dt1.Rows[0]["srtpid"].ToString();
                    loadAgentCommission(ddlservicetype.SelectedValue);
                    ddlservicetype.Enabled = false;
                    loaddepot(ddldepot);
                    ddldepot.SelectedValue = dt1.Rows[0]["officeid"].ToString();
                    ddldepot.Enabled = false;
                    if (txtwaybills.Text != "")
                    { loadbuses("0", ddlbus); }
                    else
                    { loadbuses(ddlservicetype.SelectedValue, ddlbus); }
                    ddlbus.SelectedValue = dt1.Rows[0]["bus_no"].ToString();
                    ddlbus.Enabled = false;
                    loadbusroute(ddlservicetype.SelectedValue, ddlroute);
                    ddlroute.SelectedValue = dt1.Rows[0]["routeid"].ToString();
                    ddlroute.Enabled = false;
                    loadTrips(ddlservicetype.SelectedValue, ddlroute.SelectedValue, ddljtype);
                    ddljtype.SelectedValue = dt1.Rows[0]["tripid"].ToString();
                    ddljtype.Enabled = false;
                    loadConductor(ddlbus.SelectedValue, ddlconductor);
                    ddlconductor.SelectedValue = dt1.Rows[0]["conductor_empcode"].ToString();
                    ddlconductor.Enabled = false;
                    txtdepartdate.Text = dt1.Rows[0]["tripdate"].ToString();
                    txtdepartdate.Enabled = false;

                    loadConcession(ddlconcession, ddlservicetype.SelectedValue, ddlroute.SelectedValue, Session["_LStationCode"].ToString());
                    loadRoute();
                    txtdeparttime.Text = dt1.Rows[0]["triptime"].ToString();
                    txtdeparttime.Enabled = false;
                    Session["SPECIALTRIPYN"] = dt1.Rows[0]["specialtrip_yn"].ToString();
                    if (dt1.Rows[0]["specialtrip_yn"].ToString() == "Y")
                    {
                        chkspcl.Checked = true;
                        chkspcl.Enabled = false;
                    }
                    else
                    {
                        chkspcl.Checked = false;
                        chkspcl.Enabled = false;
                    }

                    btnTripChart.Visible = true;
                    btnTripChart.Enabled = true;
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
    private void getWaybillsList(DropDownList ddlwaybillno)//M1
    {
        try
        {
            ddlwaybillno.Items.Clear();
            dt = clsCounter.GetWaybillList(Session["_LStationCode"].ToString());
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlwaybillno.DataSource = dt;
                    ddlwaybillno.DataTextField = "dutyrefno";
                    ddlwaybillno.DataValueField = "dutyrefno";
                    ddlwaybillno.DataBind();
                }
            }

            ddlwaybillno.Items.Insert(0, "select");
            ddlwaybillno.Items[0].Value = "0";
            ddlwaybillno.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlwaybillno.Items.Insert(0, "select");
            ddlwaybillno.Items[0].Value = "0";
            ddlwaybillno.SelectedIndex = 0;
        }
    }
    private void getWaybillDetails(string waybillno)//M2
    {
        try
        {
            dt = clsCounter.GetWaybillDetails(waybillno);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    txtwaybills.Enabled = false;
                    loadservicetype(ddlservicetype);
                    ddlservicetype.SelectedValue = dt.Rows[0]["srtpid"].ToString();
                    ddlservicetype.Enabled = false;
                    loaddepot(ddldepot);
                    ddldepot.SelectedValue = dt.Rows[0]["office_id"].ToString();
                    ddldepot.Enabled = false;
                    loadbuses("0", ddlbus);
                    ddlbus.SelectedValue = dt.Rows[0]["bus_no"].ToString();
                    ddlbus.Enabled = false;
                    loadbusroute(ddlservicetype.SelectedValue, ddlroute);
                    ddlroute.SelectedValue = dt.Rows[0]["routeid"].ToString();
                    ddlroute.Enabled = false;
                    loadTrips(ddlservicetype.SelectedValue, ddlroute.SelectedValue, ddljtype);
                    //ddljtype.SelectedValue = dt.Rows[0]["tripid"].ToString();
                    //ddljtype.Enabled = false;
                    loadConductor(ddlbus.SelectedValue, ddlconductor);
                    ddlconductor.SelectedValue = dt.Rows[0]["conductor1"].ToString();
                    ddlconductor.Enabled = false;
                    txtdepartdate.Text = dt.Rows[0]["dutystart_date"].ToString();
                    txtdepartdate.Enabled = false;

                    loadConcession(ddlconcession, ddlservicetype.SelectedValue, ddlroute.SelectedValue, Session["_LStationCode"].ToString());
                    //loadRoute();
                    //txtdeparttime.Text = dt.Rows[0]["departure"].ToString();
                    //txtdeparttime.Enabled = true;


                    btnTripChart.Visible = true;
                    btnTripChart.Enabled = true;
                }
                else
                {
                    Errormsg("Invalid Waybill No.");
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message);
        }
    }
    //---------------------New Code By Neeraj 04/May/2023
    private string GetDepotYN()
    {
        try
        {
            dt = clsCounter.GetDepotYN(Session["_UserCntrID"].ToString(), "C");
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                    return dt.Rows[0]["currentyn"].ToString();
            }
            return "N";
        }
        catch (Exception ex)
        {
            return "N";
        }
    }
    private void loadservicetype(DropDownList ddlservicetype)
    {
        try
        {
            ddlservicetype.Items.Clear();
            dt = clsCounter.GetServiceType();
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlservicetype.DataSource = dt;
                    ddlservicetype.DataTextField = "service_type_nameen";
                    ddlservicetype.DataValueField = "srtpid";
                    ddlservicetype.DataBind();
                }
            }
            ddlservicetype.Items.Insert(0, "Select");
            ddlservicetype.Items[0].Value = "0";
            ddlservicetype.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlservicetype.Items.Insert(0, "Select");
            ddlservicetype.Items[0].Value = "0";
            ddlservicetype.SelectedIndex = 0;
        }
    }
    private void loaddepot(DropDownList ddldepot)
    {
        try
        {
            ddldepot.Items.Clear();
            dt = clsCounter.GetDepotList();
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
            ddldepot.Items.Insert(0, "Select");
            ddldepot.Items[0].Value = "0";
            ddldepot.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddldepot.Items.Insert(0, "Select");
            ddldepot.Items[0].Value = "0";
            ddldepot.SelectedIndex = 0;
        }
    }
    private void loadbuses(string servicetypeid, DropDownList ddlbus)
    {
        try
        {
            ddlbus.Items.Clear();
            string depotcode = ddldepot.SelectedValue.ToString();
            dt = clsCounter.GetBusList(depotcode, servicetypeid);
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
    private void loadbusroute(string servicetypeid, DropDownList ddlroute)
    {
        try
        {
            ddlroute.Items.Clear();
            dt = clsCounter.GetBusRoute(servicetypeid, Session["_LStationCode"].ToString());
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlroute.DataSource = dt;
                    ddlroute.DataTextField = "routename";
                    ddlroute.DataValueField = "busrouteid";
                    ddlroute.DataBind();
                }
            }
            ddlroute.Items.Insert(0, "Select");
            ddlroute.Items[0].Value = "0";
            ddlroute.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlroute.Items.Insert(0, "Select");
            ddlroute.Items[0].Value = "0";
            ddlroute.SelectedIndex = 0;
        }
    }
    private void loadTrips(string servicetypeid, string routeid, DropDownList ddljtype)
    {
        try
        {
            ddljtype.Items.Clear();
            dt = clsCounter.GetTrips(servicetypeid, Session["_LStationCode"].ToString(), routeid);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddljtype.DataSource = dt;
                    ddljtype.DataTextField = "trip";
                    ddljtype.DataValueField = "strp_id";
                    ddljtype.DataBind();
                }
            }
            ddljtype.Items.Insert(0, "Select");
            ddljtype.Items[0].Value = "0";
            ddljtype.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddljtype.Items.Insert(0, "Select");
            ddljtype.Items[0].Value = "0";
            ddljtype.SelectedIndex = 0;
        }
    }
    private void loadConductor(string busregnumber, DropDownList ddlconductor)
    {
        try
        {
            ddlconductor.Items.Clear();
            string depotcode = ddldepot.SelectedValue.ToString();
            dt = clsCounter.GetConductor(depotcode);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlconductor.DataSource = dt;
                    ddlconductor.DataTextField = "EMPNAME";
                    ddlconductor.DataValueField = "EMPCODE";
                    ddlconductor.DataBind();
                }
            }
            ddlconductor.Items.Insert(0, "Select");
            ddlconductor.Items[0].Value = "0";
            ddlconductor.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlconductor.Items.Insert(0, "Select");
            ddlconductor.Items[0].Value = "0";
            ddlconductor.SelectedIndex = 0;
        }
    }
    private void loadConcession(DropDownList ddlconcession, string servicetypeid, string routeid, string fStationCode)
    {
        try
        {
            ddlconcession.Items.Clear();
            string depotcode = ddldepot.SelectedValue.ToString();
            dt = clsCounter.GetConcession(servicetypeid, fStationCode, routeid);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlconcession.DataSource = dt;
                    ddlconcession.DataTextField = "categoryname";
                    ddlconcession.DataValueField = "categorycode";
                    ddlconcession.DataBind();
                }
                else
                {
                    ddlconcession.Items.Insert(0, "Select");
                    ddlconcession.Items[0].Value = "0";
                    ddlconcession.SelectedIndex = 0;
                }
            }
            else
            {
                ddlconcession.Items.Insert(0, "Select");
                ddlconcession.Items[0].Value = "0";
                ddlconcession.SelectedIndex = 0;
            }

        }
        catch (Exception ex)
        {
            ddlconcession.Items.Insert(0, "Select");
            ddlconcession.Items[0].Value = "0";
            ddlconcession.SelectedIndex = 0;
        }
    }
    private void loadRoute()
    {
        try
        {
            hfRouteId.Value = ddlroute.SelectedValue;
            if (hfRouteId.Value == "0")
            {

                pnlSeatLayout.Visible = false;
            }
            else
            {
                hfNoOfPassanger.Value = "6";

                pnlSeatLayout.Visible = true;
                btnPrintTicket.Visible = true;
                btnPrintTicket.Enabled = true;
                btnPrintTicket.Text = "Print Ticket";
                lblToStation.Text = String.Empty;
                txtSeatNumbers.Text = String.Empty;
                txtFarePerSeat.Text = String.Empty;
                txtTotalNoOfSeat.Text = String.Empty;
                txtTotalFare.Text = String.Empty;
                

                Int64 RouteID = Convert.ToInt64(hfRouteId.Value.ToString());
                loadStation(RouteID, ddljtype.SelectedValue);
                string StrSeatStructDown = "";
                StrSeatStructDown = getRowColumn(ddlbus.SelectedValue, txtdepartdate.Text.ToString());
                StrSeatStructDown = getTotRowColumn(ddlbus.SelectedValue) + StrSeatStructDown;
                hfSeatStructUp.Value = StrSeatStructDown;
                Session["_SeatLayout"] = StrSeatStructDown;
                if (!(this.Page.ClientScript.IsStartupScriptRegistered("TicketLoad();")))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "TicketLoad();", true);
                    // ScriptManager.RegisterStartupScript(this, this.GetType(), "", "TicketLoad();", true);
                }

                btnTripChart.Visible = false;
            }

        }
        catch (Exception ex)
        {
        }
    }
    private void loadStation(Int64 routid, string tripid)
    {
        try
        {
            dt = clsCounter.GetStations(tripid, Session["_LStationCode"].ToString(), routid, ddlconcession.SelectedValue.ToString());
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    btnTripChart.Visible = true;
                    Session["_FromStationID"] = dt.Rows[0]["ston_id"];
                    txtdeparttime.Text = dt.Rows[0]["starttime"].ToString();
                    lblFromStation.Text = dt.Rows[0]["from_stationname"].ToString();
                    gvRouteStationsList.DataSource = dt;
                    gvRouteStationsList.DataBind();
                    pnlNotrip.Visible = false;
                    pnlSeatLayout.Visible = true;
                    gvRouteStationsList.UseAccessibleHeader = true;
                    gvRouteStationsList.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                else
                {
                    pnlNotrip.Visible = true;
                    pnlSeatLayout.Visible = false;
                    gvRouteStationsList.DataSource = dt;
                    gvRouteStationsList.DataBind();
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    private string getRowColumn(string registration_no, string tripdate)//M3
    {
        string strseatstruct = "";
        try
        {
            dt = clsCounter.GetRowColumn(registration_no, tripdate);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow drow in dt.Rows)
                    {
                        string status = "";
                        if (DBNull.Value.Equals(drow["seat_no"]))
                        {
                            status = "";
                        }
                        else
                        {
                            status = "A";
                        }
                        strseatstruct = strseatstruct + "," + drow["colnumber"].ToString() + "-" + drow["rownumber"].ToString() + "-" + drow["seatno"].ToString() + "-" + drow["seatyn"] + "-" + drow["travellertypecode"] + "-" + drow["seatavailforonlbooking"] + "-" + status;
                    }
                }
            }
            return strseatstruct;
        }
        catch (Exception ex)
        {
            return "";
        }
    }
    private string getTotRowColumn(string registration_no)//M4
    {
        string strseatstruct = "";
        try
        {
            dt = clsCounter.GetTotalRowColumn(registration_no);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dRowCol in dt.Rows)
                    {
                        strseatstruct = dRowCol["noofrows"] + "-" + dRowCol["noofcolumns"] + "-0";
                    }
                    //  hdfare.Value = "500";
                }
            }
            return strseatstruct;
        }
        catch (Exception ex)
        {
            return "";
        }
    }
    public bool isvalidValues()
    {
        try
        {
            if (_validation.IsValidString(Session["_UserCode"].ToString().Trim(), 1, 10) == false)
            {
                pnlSeatLayout.Visible = false;
                Errormsg("Invalid Values, Please correct.");
                return false;
            }

            // DateTime dtFrom = DateTime.ParseExact(txtdepartdate.Text.Trim(), "dd/MM/yyyy", null/* TODO Change to default(_) if this is not a reference type */);
            //if (IsDate(txtdepartdate.Text) == false)
            //{
            //    ErrorMesgBox("Invalid Journey Date");
            //    return false;
            //}
            //if (dtFrom > DateTime.Now.AddDays(1) || dtFrom < DateTime.Now.AddDays(-1))
            //{
            //    Errormsg("Invalid Journey Date");
            //    return false;
            //}
            decimal p_Min;
            p_Min = getjourneymint();
            //if (p_Min > 1440 | p_Min == 0)
            //{
            //    Errormsg("Invalid Journey Date/Time, Booking allowed Only 24 Hrs before from depature.");
            //    return false;
            //}


            //if (_validation.IsValidString(ddlwaybillno.SelectedValue, 10, 10) == false)
            //{
            //    pnlSeatLayout.Visible = false;
            //    Errormsg("Invalid Waybill number, Please correct.");
            //    return false;
            //}

            if (_validation.IsValidInteger(txtTotalNoOfSeat.Text.Trim(), 1, 2) == false)
            {
                Errormsg("Please Select Seat");
                return false;
            }

            //if (_validation.IsValidInteger(txtFarePerSeat.Text.Trim(), 1, 5) == false)
            //{
            //    Errormsg("Please Select Seat");
            //    return false;
            //}

            //if (_validation.IsValidInteger(txtTotalFare.Text.Trim(), 1, 6) == false)
            //{
            //    Errormsg("Please Select Seat");
            //    return false;
            //}
           

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    private decimal getjourneymint()
    {
        decimal _min = 0;
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            DataTable dt;
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.sp_get_booking_hrs");
            MyCommand.Parameters.AddWithValue("p_journeydate", txtdepartdate.Text);
            MyCommand.Parameters.AddWithValue("p_depttime", txtdeparttime.Text);
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                _min = Convert.ToDecimal(dt.Rows[0]["sp_minute_"].ToString());
            }
            //if (_min < 0)
            //{
            //    _min = 0;
            //}
        }
        catch (Exception ex)
        {

            return 0;
        }

        return _min;
    }
    private void loadIDType(DropDownList ddlidtype)
    {
        try
        {
            ddlidtype.Items.Clear();
            string depotcode = ddldepot.SelectedValue.ToString();
            dt = clsCounter.GetIdType();
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlidtype.DataSource = dt;
                    ddlidtype.DataTextField = "id_name";
                    ddlidtype.DataValueField = "proof_id";
                    ddlidtype.DataBind();
                }
            }
            ddlidtype.Items.Insert(0, "Select");
            ddlidtype.Items[0].Value = "0";
            ddlidtype.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlidtype.Items.Insert(0, "Select");
            ddlidtype.Items[0].Value = "0";
            ddlidtype.SelectedIndex = 0;
        }
    }
    private void GenerateTicket()
    {
        try
        {

            string[] customerConcession = Session["Concession"].ToString().Split(',');

            string OnlineVerificationYN = customerConcession[0].ToString();
            string CustpassNumber = "";
            string IdVerificationYN = customerConcession[1].ToString();
            string IdVerification = "";
            string DocumentVerificationYN = customerConcession[3].ToString();
            string DocumentVerification = customerConcession[4].ToString();

            if (OnlineVerificationYN == "Y")
            {
                if (txtverificationid.Text.Length < 2)
                {
                    Errormsg("Enter Pass Number Provided by Department");
                    return;
                }
                else
                {
                    var result = CheckBusPass(ddlconcession.SelectedValue, txtverificationid.Text.ToString(), hfDepartureDate.Value.ToString());
                    if (result != "Success")
                    {
                        Errormsg("Enter Valid Pass Number For concession " + ddlconcession.SelectedItem.ToString());
                        return;
                    }
                    CustpassNumber = txtverificationid.Text.ToString();
                }
            }
            else if (IdVerificationYN == "Y")
            {
                if (txtverificationid.Text.Length < 2)
                {
                    Errormsg("Enter the ID of any one of these documents - " + customerConcession[2].ToString());
                    return;
                }
                else
                {
                    IdVerification = txtverificationid.Text.ToString();
                }

            }

           

            btnPrintTicket.Enabled = false;
            string IPAddress = HttpContext.Current.Request.UserHostAddress;
            Int32 intTotSeats = Convert.ToInt16(txtTotalNoOfSeat.Text);
            string[] seatNolist = txtSeatNumbers.Text.Split(',');

            string specialYn = "N";
            if (chkspcl.Checked == true)
            {
                specialYn = "Y";
            }
            else
            {
                Session["Passenger"] = "";
                for (int i = 0; i < intTotSeats; i++)
                {
                    Session["Passenger"] += seatNolist[i].ToString() + ",,,,,|";
                }
                Session["Passenger"] = Session["Passenger"].ToString().Substring(0, Session["Passenger"].ToString().Length - 1);
            }


            
            string p_waybill = "";
            Int64 p_servicetypeid = Convert.ToInt64(ddlservicetype.SelectedValue.ToString());
            string p_busregistrationno = ddlbus.SelectedValue.ToString();
            string p_conductorid = ddlconductor.SelectedValue.ToString();
            string p_departime = txtdeparttime.Text.ToString();
            Int64 p_routeid = Convert.ToInt64(ddlroute.SelectedValue.ToString());
            Int64 p_tripid = Convert.ToInt64(ddljtype.SelectedValue.ToString());
            Int64 p_from_station_code = Convert.ToInt64(Session["_FromStationID"].ToString());
            Int64 p_to_station_code = Convert.ToInt64(Session["_ToStationID"].ToString());
            string p_trip_date = txtdepartdate.Text.ToString();
            decimal p_received_amt = Convert.ToDecimal(txtTotalFare.Text.ToString());
            decimal p_refund_amt = Convert.ToDecimal("0");
            decimal p_total_fare = Convert.ToDecimal(txtTotalFare.Text.ToString());
            decimal p_fare_per_seat = Convert.ToDecimal(txtFarePerSeat.Text.ToString());

            
            if (Session["depotCurrentyn"].ToString() == "Y")
            {
                p_waybill = ddlWaybills.SelectedValue.ToString();
            }
            else
            {
                p_waybill = txtwaybills.Text.ToString();
            }

            dt = clsCounter.SaveTicket(p_waybill, p_servicetypeid, p_busregistrationno, p_conductorid, p_departime,
                p_routeid, p_tripid, p_from_station_code, p_to_station_code, p_trip_date, p_received_amt, p_refund_amt,
                p_total_fare, p_fare_per_seat, "A", Session["_UserCode"].ToString(), "C", p_from_station_code, intTotSeats,
                Session["Passenger"].ToString(), Session["_UserCode"].ToString(), IPAddress, ddlconcession.SelectedValue.ToString(),
                OnlineVerificationYN, CustpassNumber, IdVerificationYN, IdVerification, DocumentVerificationYN, DocumentVerification,
                specialYn, "W", "C");
            if (dt.Rows.Count > 0)
            {
                Session["p_tripcode"] = dt.Rows[0]["trip_code_"].ToString();
                Session["p_ticketno"] = dt.Rows[0]["p_ticketnumber_"].ToString();
                DataTable dtt1 = new DataTable();
                dtt1 = clsCounter.ConfirmTicket(Session["p_ticketno"].ToString(), p_received_amt, p_refund_amt);
                if (dtt1.Rows.Count > 0)
                {
                    btnTripChart.Visible = true;
                    btnTripChart.Enabled = true;
                    openSubDetailsWindow("../e_ticket_current_agent.aspx");
                    loadWallet(Session["_UserCode"].ToString());
                    loadCntrOfflinetkttoday();
                }
                else
                {
                    Errormsg("Unable to Generate Ticket. Please Contact APSTS Helpdesk");
                }
            }
            else
            {
                Errormsg("Unable to Generate Ticket. Please Contact APSTS Helpdesk ");
            }
            ddlconcession.SelectedIndex = 0;
            ddlConcession_SelectedIndexChanged(ddlconcession, EventArgs.Empty);
            OpenTripDetails(Session["p_tripcode"].ToString());
            
        }
        // ***********
        catch (Exception ex)
        {
            Errormsg(ex.Message);
            Response.Write(ex.Message);
        }
    }
    public string Encrypt(string data)
    {
        RijndaelManaged rijndaelCipher = new RijndaelManaged();
        rijndaelCipher.Mode = CipherMode.CBC;
        rijndaelCipher.Padding = PaddingMode.PKCS7;
        rijndaelCipher.KeySize = 0x80;
        rijndaelCipher.BlockSize = 0x80;
        byte[] pwdBytes = Encoding.UTF8.GetBytes(e_key);
        byte[] keyBytes = new byte[16];
        int len = pwdBytes.Length;
        if (len > keyBytes.Length)
            len = keyBytes.Length;
        Array.Copy(pwdBytes, keyBytes, len);
        rijndaelCipher.Key = keyBytes;
        rijndaelCipher.IV = keyBytes;
        ICryptoTransform transform = rijndaelCipher.CreateEncryptor();
        byte[] plainText = Encoding.UTF8.GetBytes(data);
        return Convert.ToBase64String(transform.TransformFinalBlock(plainText, 0, plainText.Length));
    }
    public string Decrypt(string data)
    {
        RijndaelManaged rijndaelCipher = new RijndaelManaged();
        rijndaelCipher.Mode = CipherMode.CBC;
        rijndaelCipher.Padding = PaddingMode.PKCS7;
        rijndaelCipher.KeySize = 0x80;
        rijndaelCipher.BlockSize = 0x80;
        byte[] encryptedData = Convert.FromBase64String(data);
        byte[] pwdBytes = Encoding.UTF8.GetBytes(e_key);
        byte[] keyBytes = new byte[16];
        int len = pwdBytes.Length;
        if (len > keyBytes.Length)
            len = keyBytes.Length;
        Array.Copy(pwdBytes, keyBytes, len);
        rijndaelCipher.Key = keyBytes;
        rijndaelCipher.IV = keyBytes;
        int s = encryptedData.Length;
        byte[] plainText = rijndaelCipher.CreateDecryptor().TransformFinalBlock(encryptedData, 0, encryptedData.Length);
        return Encoding.UTF8.GetString(plainText);
    }
    public string CheckBusPass(string p_concession, string _passnumber, string _journeyDate)
    {
        try
        {
            dt = clsCounter.CheckBusPass(p_concession, _passnumber, _journeyDate);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["presult"].ToString();
                }
            }
            return "Error";
        }
        catch (Exception ex)
        {
            return "Error";
        }
    }
    private void resetctrl()
    {
        Session["Concession"] = "N,N," + "" + ",N," + "";
        pnlSeatLayout.Visible = false;
        btnTripChart.Visible = false;
        // ---------------------------------------
        // ddlWaybills.Visible = False

       // pnlWaybillDetails.Visible = false;
        // --------------------------------------

        ddlservicetype.SelectedIndex = 0;
        ddlservicetype.Enabled = true;
        ddldepot.SelectedIndex = 0;
        ddldepot.Enabled = true;
        ddlbus.SelectedIndex = 0;
        ddlbus.Enabled = true;
        ddlconductor.SelectedIndex = 0;
        ddlconductor.Enabled = true;
        txtdeparttime.Text = "";
        txtdeparttime.Enabled = true;
        ddlroute.SelectedIndex = 0;
        ddlroute.Enabled = true;
        ddljtype.SelectedIndex = 0;
        ddljtype.Enabled = true;
        txtwaybills.Text = "";
        txtwaybills.Enabled = true;
    }
    private void TirpOpenYN(string waybillno)
    {
        try
        {
            dt = clsCounter.TirpOpenYN(waybillno);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {

                    Session["p_tripcode"] = dt.Rows[0]["trip_code"].ToString();
                    btnTripChart.Visible = true;
                    btnTripChart.Enabled = true;
                }
                else
                {
                    btnTripChart.Visible = false;
                    btnTripChart.Enabled = true;
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    public void openSubDetailsWindow(string MModuleName)
    {
        try
        {
            string murl;
            murl = MModuleName + "?rt=" + DateTime.Now.ToString();
            string b = Request.Browser.Type.Substring(0, 2);
            if (b.ToUpper().Trim() == "IE")
                Response.Write("<SCRIPT language='javascript'>window.showModalDialog('" + murl + "','name','dialogWidth:830px;dialogHeight:900px');</script>");
            else
            {
                // Dim url As String = "GenQrySchStages.aspx"
                string fullURL = "window.open('" + murl + "', '_blank', 'height=900,width=830,status=no,toolbar=no,menubar=no,location=no,scrollbars=no,resizable=no,titlebar=no' );";
                // ScriptManager.RegisterStartupScript(Me, GetType(String), "OPEN_WINDOW", fullURL, True)
                string script = "window.open('" + fullURL + "','')";
                if ((ClientScript.IsClientScriptBlockRegistered("NewWindow") == false))
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "NewWindow", fullURL, true); // 
            }
        }
        catch (Exception ex)
        {
        }
    }
    private void CloseTrip(string tripcode)
    {
        try
        {
            dt = clsCounter.CloseTrip(tripcode, Session["_UserCode"].ToString());
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    //mpsucsess.Show();
                    openSubDetailsWindow("../E_tripchart_current.aspx");
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected bool IsValidBalance(string strAgentCode, string fare)
    {
        try
        {
            classWalletAgent obj = new classWalletAgent();
            DataTable dt_wallet = new DataTable();
            dt_wallet = obj.GetWalletDetailDt(strAgentCode);

            if (dt_wallet.Rows.Count > 0)
            {
                decimal currentBalance = Convert.ToDecimal(dt_wallet.Rows[0]["currentbalanceamount"].ToString());

                if (currentBalance > Convert.ToDecimal(fare) && currentBalance != 0)
                {
                    return true;
                }

                else

                {
                    return false;
                }
            }

            return false;
        }
        catch (Exception ex)
        {
            Errormsg("Something went wrong. <br/>Please contact the software admin with error code AgntSeatSlctn10-");
            return false;
        }
    }
    #endregion

    #region "Event"
    protected void lbtnwaybillnosearch_Click(object sender, EventArgs e)
    {
        if (txtwaybills.Text == "")
        {
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Enter Waybill No.')", true);
            Errormsg("Enter Waybill No.");
            return;
        }
        getWaybillDetails(txtwaybills.Text);
    }
    protected void lbtnwaybillnoreset_Click(object sender, EventArgs e)
    {
        resetctrl();
    }
   
    //-----------------------New Code By Neeraj 04/May/2023
    protected void ddlservicetype_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadbusroute(ddlservicetype.SelectedValue, ddlroute);
        loadConcession(ddlconcession, ddlservicetype.SelectedValue, ddlroute.SelectedValue, Session["_LStationCode"].ToString());
        loadAgentCommission(ddlservicetype.SelectedValue);
        pnlSeatLayout.Visible = false;
        btnTripChart.Visible = false;
        //pnlWaybillDetails.Visible = false;
        ddlbus.SelectedIndex = 0;
        ddlbus.Enabled = true;
        ddlconductor.SelectedIndex = 0;
        ddlconductor.Enabled = true;
        txtdeparttime.Text = "";
        txtdeparttime.Enabled = true;
        ddlroute.SelectedIndex = 0;
        ddlroute.Enabled = true;
        ddljtype.SelectedIndex = 0;
        ddljtype.Enabled = true;
    }
    protected void ddldepot_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadbuses(ddlservicetype.SelectedValue, ddlbus);
        loadbusroute(ddlservicetype.SelectedValue, ddlroute);
        loadConcession(ddlconcession, ddlservicetype.SelectedValue, ddlroute.SelectedValue, Session["_LStationCode"].ToString());
        pnlSeatLayout.Visible = false;
        btnTripChart.Visible = false;
        //pnlWaybillDetails.Visible = false;
        ddlbus.SelectedIndex = 0;
        ddlbus.Enabled = true;
        ddlconductor.SelectedIndex = 0;
        ddlconductor.Enabled = true;
        txtdeparttime.Text = "";
        txtdeparttime.Enabled = true;
        ddlroute.SelectedIndex = 0;
        ddlroute.Enabled = true;
        ddljtype.SelectedIndex = 0;
        ddljtype.Enabled = true;
    }
    protected void ddlbus_SelectedIndexChanged(object sender, EventArgs e)
    {

        loadConductor(ddlbus.SelectedValue, ddlconductor);
        loadConcession(ddlconcession, ddlservicetype.SelectedValue, ddlroute.SelectedValue, Session["_LStationCode"].ToString());
        pnlSeatLayout.Visible = false;
        btnTripChart.Visible = false;
       // pnlWaybillDetails.Visible = false;

        ddlconductor.SelectedIndex = 0;
        ddlconductor.Enabled = true;
        txtdeparttime.Text = "";
        txtdeparttime.Enabled = true;
        ddlroute.SelectedIndex = 0;
        ddlroute.Enabled = true;
        ddljtype.SelectedIndex = 0;
        ddljtype.Enabled = true;
    }
    protected void ddlroute_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadTrips(ddlservicetype.SelectedValue, ddlroute.SelectedValue, ddljtype);
        loadConcession(ddlconcession, ddlservicetype.SelectedValue, ddlroute.SelectedValue, Session["_LStationCode"].ToString());
        pnlSeatLayout.Visible = false;
        btnTripChart.Visible = false;
       // pnlWaybillDetails.Visible = false;
        txtdeparttime.Text = "";
        txtdeparttime.Enabled = true;
        ddljtype.SelectedIndex = 0;
        ddljtype.Enabled = true;
    }
    protected void ddljtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddljtype.SelectedIndex == 0)
        {
            pnlSeatLayout.Visible = false;
            txtdeparttime.Text = "";
            return;
        }
        loadRoute();
    }
    protected void ddlConcession_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadRoute();
        string lblmsg = "";
        int lblmsgCount = 0;


        if (ddlconcession.SelectedValue == "0" || ddlconcession.SelectedValue == "1")
        {
            
            lblverificationmsg.Text = "";
            lblverificationmsg.Visible = false;
            txtverificationid.Visible = false;
            txtverificationid.Text = "";
        }
        else
        {
            DataTable dt;
            wsClass obj = new wsClass();
            DataTable dtConcessionDtls = obj.getConcessionDtls(ddlconcession.SelectedValue.ToString());
            if (dtConcessionDtls.Rows.Count > 0)
            {
                hfNoOfPassanger.Value = "1";
                if (dtConcessionDtls.Rows[0]["gender_yn"].ToString() == "Y")
                {
                    lblmsgCount = lblmsgCount + 1;
                    lblmsg = lblmsg + lblmsgCount.ToString() + ". This Concession Only " + dtConcessionDtls.Rows[0]["GENDER"] + " Gender. <br/>";
                }
                if (dtConcessionDtls.Rows[0]["no_of_kms_yn"].ToString() == "Y")
                {
                    lblmsgCount = lblmsgCount + 1;
                    lblmsg = lblmsg + lblmsgCount.ToString() + ". This Concession Valid Only " + dtConcessionDtls.Rows[0]["NO_OF_KMS"].ToString() + " Km. <br/>";
                }
                if (dtConcessionDtls.Rows[0]["agegroup_yn"].ToString() == "Y")
                {
                    if (dtConcessionDtls.Rows[0]["min_age"].ToString() != "0")
                    {
                        lblmsgCount = lblmsgCount + 1;
                        lblmsg = lblmsg + lblmsgCount.ToString() + ". This Concession Only Minimum " + dtConcessionDtls.Rows[0]["min_age"].ToString() + " years. <br/>";
                    }
                    if (dtConcessionDtls.Rows[0]["max_age"].ToString() != "0")
                    {
                        lblmsgCount = lblmsgCount + 1;
                        lblmsg = lblmsg + lblmsgCount.ToString() + ". This Concession Only Maximum " + dtConcessionDtls.Rows[0]["max_age"].ToString() + " years. <br/>";
                    }
                }
                if (dtConcessionDtls.Rows[0]["state_yn"].ToString() == "Y")
                {
                    if (dtConcessionDtls.Rows[0]["withinstate_yn"].ToString() == "Y" & dtConcessionDtls.Rows[0]["otherstate_yn"].ToString() == "N")
                    {
                        lblmsgCount = lblmsgCount + 1;
                        lblmsg = lblmsg + lblmsgCount.ToString() + ". This Concession Only Own State. <br/>";
                    }
                    else if (dtConcessionDtls.Rows[0]["withinstate_yn"].ToString() == "N" & dtConcessionDtls.Rows[0]["otherstate_yn"].ToString() == "Y")
                    {
                        lblmsgCount = lblmsgCount + 1;
                        lblmsg = lblmsg + lblmsgCount.ToString() + ". This Concession Only Outside State. <br/>";
                    }
                    else
                    {
                        lblmsgCount = lblmsgCount + 1;
                        lblmsg = lblmsg + lblmsgCount.ToString() + ". This Concession All States. <br/>";
                    }
                }
                if (dtConcessionDtls.Rows[0]["additionalattendent_yn"].ToString() == "Y")
                {
                    hfNoOfPassanger.Value = "2";
                }
                if (dtConcessionDtls.Rows[0]["no_of_kms_yn"].ToString() == "Y")
                {
                    int _km = int.Parse(dtConcessionDtls.Rows[0]["no_of_kms"].ToString());
                    int index = 0;
                    foreach (GridViewRow row in gvRouteStationsList.Rows)
                    {
                        int _distance = 0;
                        _distance = int.Parse(gvRouteStationsList.DataKeys[row.RowIndex]["total_dist_km"].ToString());
                        RadioButton rblYesNo = row.FindControl("rblYesNo") as RadioButton;
                        if (_distance > _km)
                        {
                            rblYesNo.Enabled = false;
                        }
                        else
                        {
                            rblYesNo.Enabled = true;
                        }
                    }
                }
                Session["Concession"] = dtConcessionDtls.Rows[0]["onlineverification_yn"].ToString() + "," + dtConcessionDtls.Rows[0]["idverification_yn"].ToString() + "," + dtConcessionDtls.Rows[0]["id_name"].ToString() + "," + dtConcessionDtls.Rows[0]["documentverification_yn"].ToString() + "," + dtConcessionDtls.Rows[0]["document_name"].ToString();
                if (dtConcessionDtls.Rows[0]["onlineverification_yn"].ToString() == "Y")
                {
                    lblverificationmsg.Text = "Enter Pass Number Provided by Department";
                    lblverificationmsg.Visible = true;
                    txtverificationid.Visible = true;
                    txtverificationid.Text = "";
                }
                else if (dtConcessionDtls.Rows[0]["idverification_yn"].ToString() == "Y")
                {
                    lblverificationmsg.Text = "Enter the ID of any one of these documents - " + dtConcessionDtls.Rows[0]["id_name"].ToString();
                    lblverificationmsg.Visible = true;
                    txtverificationid.Visible = true;
                    txtverificationid.Text = "";
                }
                else if (dtConcessionDtls.Rows[0]["documentverification_yn"].ToString() == "Y")
                {
                    lblverificationmsg.Text = "Keep any one of these documents ready at the time of jouney - " + dtConcessionDtls.Rows[0]["document_name"].ToString();
                    lblverificationmsg.Visible = true;
                    txtverificationid.Visible = false;
                    txtverificationid.Text = "";
                }
                
            }
            else
            {
               
                ddlconcession.SelectedValue = "1";
            }
        }
    }
    protected void rblYesNo1_CheckedChanged(object sender, EventArgs e)
    {
        RadioButton Crbl = sender as RadioButton;
        GridViewRow parentRow = Crbl.NamingContainer as GridViewRow;

        int i, j;
        for (i = 0; i <= gvRouteStationsList.Rows.Count - 1; i++)
        {
            if (parentRow.RowIndex != i)
            {
                Crbl = (RadioButton)gvRouteStationsList.Rows[i].FindControl("rblYesNo");
                Crbl.Checked = false;
                j = i;
            }
        }

        //if (ddlwaybillno.SelectedValue == "0")
        //{
        //    Errormsg("Please Select Waybill Number");
        //    btnPrintTicket.Visible = false;
        //    loadRoute();
        //    hfSeatStructUp.Value = Session["_SeatLayout"].ToString();
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "TicketLoad();", true);
        //    return;
        //}

        lblToStation.Text = gvRouteStationsList.Rows[parentRow.RowIndex].Cells[1].Text;

        txtFarePerSeat.Text = gvRouteStationsList.Rows[parentRow.RowIndex].Cells[3].Text;
        Session["_Distance"] = gvRouteStationsList.Rows[parentRow.RowIndex].Cells[2].Text;
        Session["_ToStationID"] = gvRouteStationsList.DataKeys[parentRow.RowIndex].Values["route_station_id"];

        Session["_EndStationHindi"] = gvRouteStationsList.DataKeys[parentRow.RowIndex].Values["station_name"];
        string output = "";
        string errormsg = "";
        Session["_DisplayStationNames"] = lblToStation.Text;
        hdnToStation.Value = Session["_DisplayStationNames"].ToString();
        hfDepartureDate.Value = txtdeparttime.Text.ToString();
        // ----------------------------Display on POS
        // Dim msgToDisplay As String = lblFromStation.Text & "-" & lblToStation.Text
        // ExecuteShellCommand("cls", " >com1", output, errormsg)
        // ExecuteShellCommand("echo", msgToDisplay & " >com1", output, errormsg)
        // msgToDisplay = "Fare Per seat " & txtFarePerSeat.Text
        // ExecuteShellCommand("echo", msgToDisplay & " >com1", output, errormsg)
        txtSeatNumbers.Text = "";
        txtTotalFare.Text = "";
        txtTotalNoOfSeat.Text = "";
        
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "TicketLoad();", true);
        btnPrintTicket.Enabled = false;
    }
    protected void btnPrintTicket_Click(object sender, EventArgs e)
    {

        if (!isvalidValues())
        {
            return;
        }

        if (!IsValidBalance(Session["_UserCode"].ToString(), txtTotalFare.Text))
        {
            Errormsg("Balance is not sufficient; please deposit amount");
            return;
        }
        if (chkspcl.Checked == true)
        {
            lbltotalseats.Text = txtTotalNoOfSeat.Text.ToString();
            lbltotalfare.Text = txtTotalFare.Text.ToString();
            
            lblstations.Text = "(" + lblFromStation.Text.ToString() + " To " + lblToStation.Text.ToString() + ")";
            DataTable dt = new DataTable("Pessenger");
            // Dim dr As DataRow = dt.NewRow()
            dt.Columns.Add("seatNO", typeof(Int32));
            // dt.Columns.Add("PessengerName", GetType(String))
            // dt.Columns.Add("IDType", GetType(String))
            // dt.Columns.Add("IDNo", GetType(String))
            string[] seatNolist = txtSeatNumbers.Text.Split(',');
            // End Datatable For Sr. Citizen Seats
            foreach (string customer in seatNolist)
            {
                string SeatNo = customer;
                DataRow dr = dt.NewRow(); //dt.NewRow;
                dt.Rows.Add(SeatNo);
            }
            Session["Type"] = "ST";
            grvPessenger.DataSource = dt;
            grvPessenger.DataBind();
            lblerrormsg.Text = "";
            mpPessenger.Show();
        }
        else
        {
            Session["P_Name"] = "";
            Session["P_IDType"] = "";
            Session["P_IdNumber"] = "";
            Session["P_SPLRefNo"] = "";
            Session["Type"] = "T";
            lblConfirmation.Text = "Do you want to book ticket";
            mpConfirmation.Show();
        }

    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        if (Session["Type"].ToString() == "T")
        {
            btnPrintTicket.Enabled = false;
            GenerateTicket();
        }
        if (Session["Type"].ToString() == "TP")
        {
            if (Session["SPECIALTRIPYN"].ToString() == "Y")
            {
                Session["_pUpdatedby"] = Session["_UserCode"].ToString();
                openSubDetailsWindow("../E_Spcltripchart_current.aspx");
            }
            else
            {
                Session["_pUpdatedby"] = Session["_UserCode"].ToString();
                openSubDetailsWindow("../E_tripchart_current.aspx");
            }
            resetctrl();
        }
    }
    protected void lbtnNoConfirmation_Click(object sender, EventArgs e)
    {
        mpConfirmation.Hide();
        Session["Type"] = "";
        loadRoute();
    }
    protected void lbtnspecialyes_Click(object sender, EventArgs e)
    {
        Session["P_Name"] = "";
        Session["P_IDType"] = "";
        Session["P_IdNumber"] = "";
        Session["P_SPLRefNo"] = "";
        lblerrormsg.Text = "";
        foreach (GridViewRow row in grvPessenger.Rows)
        {
            TextBox txtname = (TextBox)row.FindControl("txtname");
            DropDownList ddlidtype = (DropDownList)row.FindControl("ddlidtype");
            TextBox txtidnumber = (TextBox)row.FindControl("txtidnumber");
            TextBox txtSpclRefno = (TextBox)row.FindControl("txtSpclRefno");

            if (txtname.Text.Length <= 0 || ((ddlidtype.SelectedValue == "0" || txtidnumber.Text.Length <= 0) && txtSpclRefno.Text.Length <= 0))
            {
                lblerrormsg.Text = "Enter Valid/Select Passenger Name, ID type And ID number Or Special Reference Number.";
                mpPessenger.Show();
                // Dim msg As String = "Enter Valid/Select Passenger Name, ID type And ID number."
                // ErrorMesgBox(msg)
                return;
            }
            Session["P_Name"] = Session["P_Name"].ToString() + txtname.Text.ToString() + ",";
            Session["P_IDType"] = Session["P_IDType"].ToString() + ddlidtype.SelectedValue.ToString() + ",";
            Session["P_IdNumber"] = Session["P_IdNumber"].ToString() + txtidnumber.Text.ToString() + ",";
            Session["P_SPLRefNo"] = Session["P_SPLRefNo"].ToString() + txtSpclRefno.Text.ToString() + ",";
        }
        Session["P_Name"] = Session["P_Name"].ToString().Substring(0, Session["P_Name"].ToString().Length);
        Session["P_IDType"] = Session["P_IDType"].ToString().Substring(0, Session["P_IDType"].ToString().Length);
        Session["P_IdNumber"] = Session["P_IdNumber"].ToString().Substring(0, Session["P_IdNumber"].ToString().Length);
        Session["P_SPLRefNo"] = Session["P_SPLRefNo"].ToString().Substring(0, Session["P_SPLRefNo"].ToString().Length);
        btnPrintTicket.Enabled = false;
        GenerateTicket();
    }
    protected void lbtnspecialno_Click(object sender, EventArgs e)
    {
        mpPessenger.Hide();
        Session["Type"] = "";
        loadRoute();
    }
    protected void grvPessenger_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlidtype = (DropDownList)e.Row.FindControl("ddlidtype");
            loadIDType(ddlidtype);
        }
    }
    protected void btnTripChart_Click(object sender, EventArgs e)
    {
        lblConfirmation.Text = "Do you want to generate a trip chart ? once the trip chart is generated, the booking for this trip will be closed.";
        mpConfirmation.Show();
        Session["Type"] = "TP";
    }
    protected void lbtnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("AgCurrentBookingDashboard.aspx");
    }
    protected void lbtnok_Click(object sender, EventArgs e)
    {
        Response.Redirect("AgCurrentBookingDashboard.aspx");
    }
    protected void btnDashboard_Click(object sender, EventArgs e)
    {
        Response.Redirect("AgCurrentBookingDashboard.aspx");
    }

    #endregion

}