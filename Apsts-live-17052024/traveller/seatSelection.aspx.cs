using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Npgsql;

public partial class traveller_seatSelection : BasePage 
{
    NpgsqlCommand MyCommand;
    sbBLL bll = new sbBLL();
    sbValidation _SecurityCheck = new sbValidation();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    private sbCommonFunc _common = new sbCommonFunc();
    wsClass obj = new wsClass();
    DataTable dt = new DataTable();
    sbValidation _validation = new sbValidation();
    int intPassengerNo = 0;
    string commonerror = "There is some error. Please contact the administrator or try again after some time.";
    Dictionary<string, string> dct;


    protected void Page_Load(object sender, EventArgs e)
    {
        checkUser();
        if (!IsPostBack)
        {
            Random randomclass = new Random();
            Session["rndNoCheck"] = randomclass.Next().ToString();
            hidtoken.Value = Session["rndNoCheck"].ToString();

            Session["_moduleName"] = "Seat Selection & Passenger Details";
           
            setLayout_and_boardings();
            showSearchParameters();
        }
    }
    
    #region "Methods"
    private void maxseatavailble(string srtp_id)
    {
        try
        {
            DataTable dt = obj.getMaxSeats(srtp_id);
            if (dt.Rows.Count > 0)
                hfNoOfPassanger.Value = dt.Rows[0]["currentseat"].ToString();
            else
                hfNoOfPassanger.Value = "6";
        }
        catch (Exception ex)
        {
            hfNoOfPassanger.Value = "6";
            _common.ErrorLog("seatSelection.aspx-0001", ex.Message);
        }
    }
    private void checkUser()
    {
        if (Session["_RoleCode"] == null)
        {
            Response.Redirect("../errorpage.aspx");
            return;
        }
        if (_security.isSessionExist(Session["_UserCode"]) == true)
        {
            Session["_UserCode"] = Session["_UserCode"];
        }
        else
        {
            Response.Redirect("errorpage.aspx");
        }
    }
    private void CheckTokan()
    {
        if (Session["rndNoCheck"] == null || Session["rndNoCheck"].ToString() == "")
        {
            Response.Redirect("../errorpage.aspx");
        }

        if (Session["rndNoCheck"].ToString() != hidtoken.Value.ToString())
        {
            Response.Redirect("../errorpage.aspx");
        }
    }
    private void Errormsg(string msg)
    {
        string popup = _popup.modalPopupSmall("D", "Please Check", msg, "Close");
        Response.Write(popup);
    }
    private void Successmsg(string msg)
    {
        string popup = _popup.modalPopupSmall("S", "Success", msg, "Close");
        Response.Write(popup);
    }
    public void showSearchParameters() 
    {
        try
        {
            if (_security.isSessionExist(Session["SearchParameters"]) == true)
            {
                dct = new Dictionary<string, string>();
                dct = (Dictionary<string, string>)Session["SearchParameters"];
                string dsvcid = dct["dsvcid"];
                string strpid = dct["strpid"];
                string depttime = dct["depttime"];
                string arrtime = dct["arrtime"];
                string tripdirection = dct["tripdirection"];
                string servicetypename = dct["servicetypename"];
                string totalfare = dct["totalfare"];
                string layout = dct["layout"];
                string midstations = dct["midstations"];
                string fromstation = dct["fromstation"];
                string tostation = dct["tostation"];
                string date = dct["date"];

                lblFromStation.Text = fromstation;
                lblToStation.Text = tostation;
                lblDate.Text = date;
                lblDeparture.Text = depttime;
                lblServiceType.Text = servicetypename;
            }
            else
            {
                Response.Redirect("errorpage.aspx");
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.ToString());
            _common.ErrorLog("seatSelection.aspx-0002", ex.Message);
        }
    }
    public void setLayout_and_boardings() 
    {
        try
        {
            
            string StrSeatStructDown = "";
            if (_security.isSessionExist(Session["SearchParameters"]) == true)
            {
                dct = new Dictionary<string, string>();
                dct = (Dictionary<string, string>)Session["SearchParameters"];

                string dsvcid = dct["dsvcid"];
                string strpid = dct["strpid"];
                string tripdirection = dct["tripdirection"];
                string layout = dct["layout"];
                string date = dct["date"];
                string fromStonId = dct["frstonid"];
                string toStonId = dct["tostonid"];

                maxseatavailble(strpid);

                DataTable dtTotalRowCol = obj.getLayoutTotRowColumn(dsvcid);
                if (dtTotalRowCol.Rows.Count > 0)
                {
                    foreach (DataRow dRowCol in dtTotalRowCol.Rows)
                    {
                        StrSeatStructDown = dRowCol["noofrows"] + "-" + dRowCol["noofcolumns"] + "-500";
                    }
                }
                DataTable dtLayout = obj.getLayoutRowColumn(strpid, tripdirection, dsvcid, date);
                if (dtLayout.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtLayout.Rows)
                    {
                        StrSeatStructDown = StrSeatStructDown + "," + drow["colnumber"].ToString() + "-" + drow["rownumber"].ToString() + "-" + drow["seatno"].ToString() + "-" + drow["seatyn"] + "-" + drow["travellertypecode"] + "-" + drow["seatavailforonlbooking"] + "-" + drow["status"];
                    }
                }

                hfSeatStructUp.Value = StrSeatStructDown;

                string strCat = getconcession(dsvcid, fromStonId, toStonId);
                hfCategoryList.Value = strCat.Substring(1, strCat.Length - 1);

                BindBoardingStations(ddlBoardingfrom, dsvcid, strpid);

            }
            else
            {
                Response.Redirect("errorpage.aspx");
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.ToString());
            _common.ErrorLog("seatSelection.aspx-0003", ex.Message);
        }
    }
    private void BindBoardingStations(DropDownList ddlBoardingfrom, string ServiceCode, string ServiceTripCode)
    {
        try
        {
            wsClass obj = new wsClass();
            ddlBoardingfrom.Items.Clear();
            dt = obj.get_boardingStations_dt(ServiceCode, ServiceTripCode);
            if (dt.Rows.Count > 0)
            {
                ddlBoardingfrom.DataSource = dt;
                ddlBoardingfrom.DataTextField = "p_stname";
                ddlBoardingfrom.DataValueField = "p_stcode";
                ddlBoardingfrom.DataBind();
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.ToString());
            _common.ErrorLog("seatSelection.aspx-0004", ex.Message);
        }
    }
    private void SaveSeats() 
    {
        try
        {
            if (_security.isSessionExist(Session["SearchParameters"]) == true)
            {
                dct = new Dictionary<string, string>();
                dct = (Dictionary<string, string>)Session["SearchParameters"];
                string IPAddress = HttpContext.Current.Request.UserHostAddress;

                string dsvcid = dct["dsvcid"];
                string strpid = dct["strpid"];
                string depttime = dct["depttime"];
                string arrtime = dct["arrtime"];
                string tripdirection = dct["tripdirection"];
                string servicetypename = dct["servicetypename"];
                string totalfare = dct["totalfare"];
                string layout = dct["layout"];
                string midstations = dct["midstations"];
                string frstonid = dct["frstonid"];
                string fromstation = dct["fromstation"];
                string tostonid = dct["tostonid"];
                string tostation = dct["tostation"];
                string date = dct["date"];
                string boardingStations = dct["boardingId"];
                string psngr = dct["passengers"];

                string userId = Session["_UserCode"].ToString(); 
                string Mobile = txtMobileNo.Text.Trim();
                string email = txtemialid.Text.Trim();
                

                wsClass obj = new wsClass();
                DataTable dtp = obj.SaveSeats(dsvcid, tripdirection, strpid, date, frstonid, tostonid, "T", userId, Mobile, email, boardingStations, psngr, IPAddress,"W");
               

                if (dtp.TableName == "Success")
                {
                    string ticketNo = dtp.Rows[0]["p_ticketnumber"].ToString();
                    if (ticketNo == "ERROR" || ticketNo == "EXCEPTION")
                    {
                        Errormsg(commonerror);
                    }
                    else
                    {
                        Session["TicketNumber"] = ticketNo;
                        Session["_RNDIDENTIFIERSTRVL"] = _security.GeneratePassword(10, 5);
                        Response.Redirect("seatPayment.aspx");
                    }
                }
                else
                {
                    Errormsg(commonerror);
                }
            }
            else
            {
                Response.Redirect("errorpage.aspx");
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.ToString());
            _common.ErrorLog("seatSelection.aspx-0005", ex.Message);
        }
    }
    private string getconcession(string dsvcid, string fromStation, string toStation)
    {
        string strCat = "";
        try
        {
            wsClass obj = new wsClass();
            DataTable dtConcession = obj.getConcessionCategory(dsvcid, fromStation, toStation);

            if (dtConcession.Rows.Count > 0)
            {
                foreach (DataRow drow in dtConcession.Rows)
                {
                    strCat = strCat + "," + drow["categorycode"].ToString() + "-" + drow["categoryname"].ToString();
                }
            }
            return strCat;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("seatSelection.aspx-0006", ex.Message);
            return "";
        }
    }

    #endregion

    #region "Events"
    protected void lbtnProceed_Click(object sender, EventArgs e)
    {
        CheckTokan();
       
        string userId = "";
        string userMobile = txtMobileNo.Text.Trim();
        Session["_pmtCTZMobile"] = userMobile;
        string userEmail = txtemialid.Text.Trim();
        
        string boardingStationId = ddlBoardingfrom.SelectedValue;
        string boardingStationName = ddlBoardingfrom.SelectedItem.Text;

        if (_security.isSessionExist(Session["_UserCode"]) == true)
        {
            Session["_UserCode"] = Session["_UserCode"];
            userId = Session["_UserCode"].ToString();
        }
        else
        {
            Response.Redirect("errorpage.aspx");
            return;
        }

        if (_validation.IsValidInteger(userMobile, 10, 10) == false)
        {
            Errormsg("Please enter correct mobile number and select the seat(s) again");
            return;
        }

        if (userEmail.Length > 0)
        {
            if (_validation.isValideMailID(userEmail) == false)
            {
                Errormsg("Please enter valid email id and select the seat(s) again");
                return;
            }
        }

        if (_validation.IsValidInteger(boardingStationId, 1, 5) == false)
        {
            Errormsg("Please select Boarding Station for Journey");
            return;
        }

        if (_security.isSessionExist(Session["SearchParameters"]) == true)
        {

            Dictionary<string, string> dct = new Dictionary<string, string>();
            dct = (Dictionary<string, string>)Session["SearchParameters"];
                        

            if ((dct.ContainsKey("boardingId")))
                dct.Remove("boardingId");
            dct.Add("boardingId", boardingStationId);

            if ((dct.ContainsKey("boardingName")))
                dct.Remove("boardingName");
            dct.Add("boardingName", boardingStationName);

            Session["SearchParameters"] = dct;


            string passengers = hfCustomerData.Value;

            int resultcount = 0;
            string result = "";

		var strpidd = dct["strpid"];

            string journeyDatee = dct["date"];

            if (obj.checkTripGenerate(journeyDatee, strpidd) == false)
            {
                mpTripOver.Show();
                return;
            }

            if (obj.IsTicketStillAvaialable(passengers, journeyDatee, "UP", strpidd) == false)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "getSeatStruct('up','" + hfSeatStructUp.Value + "');", true);
                Errormsg("You are late, Booking has already been made for the selected seat(s)");
                return;
            }

            int concession_count = 0;
            int Index = 0;

            DataTable table = new DataTable();
            table.Columns.Add("SeatNo", typeof(string));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Age", typeof(string));
            table.Columns.Add("Gender", typeof(string));
            table.Columns.Add("Concession", typeof(string));
            table.Columns.Add("JourneyType", typeof(string));
            table.Columns.Add("Fare", typeof(string));
            table.Columns.Add("OnlineVerificationYN", typeof(string));
            table.Columns.Add("IdVerificationYN", typeof(string));
            table.Columns.Add("Idverification", typeof(string));
            table.Columns.Add("DocumentVerificationYN", typeof(string));
            table.Columns.Add("DocumentVerification", typeof(string));
            table.Columns.Add("ConcessionName", typeof(string));

            string[] CustomerList = passengers.Split('|');
            foreach (string customer in CustomerList)
            {
                string[] customerDetail = customer.Split(',');
                string SeatNo = customerDetail[0];
                string custName = customerDetail[1].ToUpper().Trim();
                string custGender = customerDetail[2];
                Int16 custAge = Convert.ToInt16(customerDetail[3].Trim());
                string cusConcession = customerDetail[4];
                string cusJourneyType = customerDetail[5];
                string cusFare = customerDetail[6];

                string OnlineVerificationYN = "N";
                string IdVerificationYN = "N";
                string Idverification = "";
                string DocumentVerificationYN = "N";
                string DocumentVerification = "";
                string ConcessionName = "No Concession";

                if (cusConcession != "1")
                {
                    concession_count = concession_count + 1;
                    DataTable dtcheckconcession = obj.CheckConcessionCategory(cusConcession, custGender, custAge.ToString());
                    if (dtcheckconcession.Rows.Count > 0)
                    {
                        ConcessionName = dtcheckconcession.Rows[0]["sp_CONCESSION_NAME"].ToString();
                        OnlineVerificationYN = dtcheckconcession.Rows[0]["sp_ONLINEVERIFICATION_YN"].ToString();
                        IdVerificationYN = dtcheckconcession.Rows[0]["sp_IDVERIFICATION_YN"].ToString();
                        if (DBNull.Value.Equals(dtcheckconcession.Rows[0]["sp_IDVERIFICATION"]) == true)
                            Idverification = "";
                        else
                            Idverification = dtcheckconcession.Rows[0]["sp_IDVERIFICATION"].ToString();
                        DocumentVerificationYN = dtcheckconcession.Rows[0]["sp_DOCUMENTVERIFICATION_YN"].ToString();
                        if (DBNull.Value.Equals(dtcheckconcession.Rows[0]["sp_DOCUMENTVERIFICATION"]) == true)
                            DocumentVerification = "";
                        else
                            DocumentVerification = dtcheckconcession.Rows[0]["sp_DOCUMENTVERIFICATION"].ToString();

                        if (dtcheckconcession.Rows[0]["p_Gender_Result"].ToString().ToUpper() != "SUCCESS")
                        {
                            resultcount = resultcount + 1;
                            result = result + "Seat No " + SeatNo + " Invalid Gender Selection For Concession <br/>";
                        }
                        else if (dtcheckconcession.Rows[0]["p_Age_Result"].ToString().ToUpper() != "SUCCESS")
                        {
                            resultcount = resultcount + 1;
                            result = result + "Seat No " + SeatNo + " Invalid Age For Concession <br/>";
                        }
                        else
                        {
                            if (dtcheckconcession.Rows[0]["sp_ONLINEVERIFICATION_YN"].ToString() == "Y")
                            {
                            }
                            if (dtcheckconcession.Rows[0]["sp_IDVERIFICATION_YN"].ToString() == "Y")
                            {
                            }
                            if (dtcheckconcession.Rows[0]["sp_DOCUMENTVERIFICATION_YN"].ToString() == "Y")
                            {
                            }
                        }
                    }
                }
                table.Rows.Add(SeatNo, custName, custAge, custGender, cusConcession, cusJourneyType, cusFare, OnlineVerificationYN, IdVerificationYN, Idverification, DocumentVerificationYN, DocumentVerification, ConcessionName);
                Index = Index + 1;
            }

            if (resultcount > 0)
            {
               // ScriptManager.RegisterStartupScript(this, this.GetType(), "", "getSeatStruct('up','" + hfSeatStructUp.Value + "');", true);
                Errormsg(result);
                return;
            }
            if (concession_count >= 1 && table.Rows.Count > 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "getSeatStruct('up','" + hfSeatStructUp.Value + "');", true);
                Errormsg("Sorry booking is not allowed as in a ticket with concession only one seat can be booked at a time");
                return;
            }
            gvSeats.DataSource = table;
            gvSeats.DataBind();
            if (concession_count > 0)
            {
                string dsvcid = dct["dsvcid"];
                string tripdirection = dct["tripdirection"];
                var strpid = dct["strpid"];
                var depttime = dct["depttime"];
                var arrtime = dct["arrtime"];
                var servicetypename = dct["servicetypename"];
                var fromstation = dct["fromstation"];
                var tostation = dct["tostation"];
                string journeyDate = dct["date"];
                

                if ((dct.ContainsKey("passengers")))
                    dct.Remove("passengers");
                dct.Add("passengers", passengers);
                Session["SearchParameters"] = dct;

                lblServiceType2.Text = dsvcid + "" + tripdirection + "" + strpid + " | " + servicetypename;
                lblFromStationName2.Text = fromstation;
                lblToStationName2.Text = tostation;
                lblDateTime2.Text = journeyDate + " " + depttime;

                pnlConcession.Visible = true;
                pnlLayout.Visible = false;
            }
            else
                lbtnConfirmConcession_Click(lbtnProceed, null/* TODO Change to default(_) if this is not a reference type */);
        }
        else
            Response.Redirect("errorpage.aspx");


    }
    protected void lbtnConfirmConcession_Click(object sender, EventArgs e)
    {
        CheckTokan();
        if (Session["SearchParameters"] != null)
        {
            string passengers;
            passengers = "";
            Dictionary<string, string> dct = new Dictionary<string, string>();
            dct = (Dictionary<string, string>)Session["SearchParameters"];
            string journeyDate = dct["date"];
            var dsvcid = dct["dsvcid"];
            var strpid = dct["strpid"];
            var tripdirection = dct["tripdirection"];
            var fromstationId = dct["frstonid"];
            var tostationId = dct["tostonid"];

            string msg = "Please Check <br/>";
            int msgcount = 0;
            if (gvSeats.Rows.Count > 0)
            {
                foreach (GridViewRow row in gvSeats.Rows)
                {
                    string seatno = gvSeats.DataKeys[row.RowIndex]["SeatNo"].ToString();
                    string Name = gvSeats.DataKeys[row.RowIndex]["Name"].ToString();
                    string Gender = gvSeats.DataKeys[row.RowIndex]["Gender"].ToString();
                    string Age = gvSeats.DataKeys[row.RowIndex]["Age"].ToString();
                    string Concession = gvSeats.DataKeys[row.RowIndex]["Concession"].ToString();
                    string JourneyType = gvSeats.DataKeys[row.RowIndex]["JourneyType"].ToString();
                    string Fare = gvSeats.DataKeys[row.RowIndex]["Fare"].ToString();
                    TextBox tbpass_docid = row.FindControl("tbpass_docid") as TextBox;

                    string OnlineVerificationYN = gvSeats.DataKeys[row.RowIndex]["OnlineVerificationYN"].ToString();
                    string Passnumber = "";
                    if (gvSeats.DataKeys[row.RowIndex]["OnlineVerificationYN"].ToString() == "Y")
                        Passnumber = tbpass_docid.Text.ToString().Trim();
                    string IdVerificationYN = gvSeats.DataKeys[row.RowIndex]["IdVerificationYN"].ToString();
                    string Idverification = "";
                    if (gvSeats.DataKeys[row.RowIndex]["IdVerificationYN"].ToString() == "Y")
                        Idverification = tbpass_docid.Text.ToString().Trim();
                    string DocumentVerificationYN = gvSeats.DataKeys[row.RowIndex]["DocumentVerificationYN"].ToString();
                    string DocumentVerification = gvSeats.DataKeys[row.RowIndex]["DocumentVerification"].ToString();

                    if (tbpass_docid.Text == "")
                    {
                        if (gvSeats.DataKeys[row.RowIndex]["OnlineVerificationYN"].ToString() == "Y")
                        {
                            msgcount = msgcount + 1;
                            msg = msg + "Enter Pass Number For seat No " + seatno + "<br/>";
                        }
                        if (gvSeats.DataKeys[row.RowIndex]["IdVerificationYN"].ToString() == "Y")
                        {
                            msgcount = msgcount + 1;
                            msg = msg + "Enter Id For Selected Document ID For seat No " + seatno + "<br/>";
                        }
                    }
                    else if (gvSeats.DataKeys[row.RowIndex]["OnlineVerificationYN"].ToString() == "Y")
                    {
                        DataTable result = obj.CheckBusPassNew(Concession, tbpass_docid.Text.ToString().Trim(), journeyDate);
                        if (result.Rows.Count > 0)
                        {
                            if (result.Rows[0]["p_result"].ToString() != "Success")
                            {
                                msgcount = msgcount + 1;
                                msg = msg + "Enter Valid Pass Number For concession " + gvSeats.DataKeys[row.RowIndex]["ConcessionName"].ToString() + " on seat number " + seatno + "<br/>";
                            }
                            else
                            {
                                Session["MobileNo"] = result.Rows[0]["mob"].ToString();
                            }
                        }
                        else
                        {
                            msgcount = msgcount + 1;
                            msg = msg + "Enter Valid Pass Number For concession " + gvSeats.DataKeys[row.RowIndex]["ConcessionName"].ToString() + " on seat number " + seatno + "<br/>";
                        }
                    }
                    if (gvSeats.DataKeys[row.RowIndex]["OnlineVerificationYN"].ToString() == "Y")
                    {
                        if (Session["_pmtCTZMobile"].ToString() != Session["MobileNo"].ToString())
                        {
                            Errormsg("Sorry booking is not allowed as the discounted ticket for online booking should only have the mobile number registered with the bus pass");
                            return;
                        }
                    }
                    passengers = passengers + seatno + "," + Name + "," + Gender + "," + Age + "," + Concession + "," + JourneyType + "," + Fare + "," + OnlineVerificationYN + "," + Passnumber + "," + IdVerificationYN + "," + Idverification + "," + DocumentVerificationYN + "," + DocumentVerification + "|";
                    tbpass_docid.Text = "";
                }

                if (msgcount > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "getSeatStruct('up','" + hfSeatStructUp.Value + "');", true);
                    Errormsg(msg);
                    return;
                }

                passengers = passengers.Substring(0, passengers.Length - 1);

                if ((dct.ContainsKey("passengers")))
                    dct.Remove("passengers");
                dct.Add("passengers", passengers);
                Session["SearchParameters"] = dct;

                SaveSeats();
            }
            else
            {
                Errormsg("Please Select Seats First");
                return;
            }
        }
        else
        {
            Errormsg("You are late, Booking has already been made for the selected seat(s). ");
            return;
        }
    }
    protected void gvSeats_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblheading = (Label)e.Row.FindControl("lblheading");
            TextBox tbpass_docid = (TextBox)e.Row.FindControl("tbpass_docid");
            lblheading.Visible = false;
            tbpass_docid.Visible = false;

            DataRowView rowView = (DataRowView)e.Row.DataItem;
            if (rowView["OnlineVerificationYN"].ToString() == "Y")
            {
                lblheading.ForeColor = System.Drawing.Color.Red;
                lblheading.Visible = true;
                lblheading.Text = "Enter Pass Number Provided by Department";
                tbpass_docid.Visible = true;
            }
            else if (rowView["IdVerificationYN"].ToString() == "Y")
            {
                lblheading.Visible = true;
                lblheading.ForeColor = System.Drawing.Color.Red;
                lblheading.Text = "Enter the ID of any one of these documents - " + rowView["Idverification"].ToString();
                tbpass_docid.Visible = true;
            }
            else if (rowView["DocumentVerificationYN"].ToString() == "Y")
            {
                lblheading.ForeColor = System.Drawing.Color.Green;
                lblheading.Visible = true;
                lblheading.Text = "Keep any one of these documents ready at the time of jouney - " + rowView["DocumentVerification"].ToString();
            }
        }
    }
    protected void lbtnclose_Click(object sender, EventArgs e)
    {
        gvSeats.DataSource = null;
        gvSeats.DataBind();
        Session["_RNDIDENTIFIERSTRVL"] = _security.GeneratePassword(10, 5);
        Response.Redirect("dashboard.aspx");
    }
    #endregion



}