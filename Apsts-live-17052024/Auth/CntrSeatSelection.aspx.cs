using System;
using System.Data;
using Npgsql;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

public partial class Auth_CntrSeatSelection : BasePage
{
    NpgsqlCommand MyCommand = new NpgsqlCommand();
    private sbBLL bll = new sbBLL();
    DataTable dt = new DataTable();
    sbValidation _validation = new sbValidation();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    int intPassengerNo = 0;
    Dictionary<string, string> dct;
    string commonerror = "There is some error. Please contact the administrator or try again after some time.";
   
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["_moduleName"] = "Seat Selection";

        if (!IsPostBack)
        {
            Random randomclass = new Random();
            Session["rndNoCheck"] = randomclass.Next().ToString();
            hidtoken.Value = Session["rndNoCheck"].ToString();
            if ( Convert.ToInt32( Session["lblmandatoryamt"].ToString()) > 0)
            {
                Response.Redirect("Cntrdashboard.aspx");
                return;
            }
            //hdncheck.Value = "check";
            hfIsRoundTrip.Value = "N";
            hfNoOfPassanger.Value = "6";
            showSearchParameters();
        }
    }
    public void showSearchParameters() // M1
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
                string fromStonId = dct["frstonid"];
                string toStonId = dct["tostonid"];

                lblservice.Text = dsvcid + "" + tripdirection + " " + servicetypename;
                lbljourneydatetime.Text = date;
                lbldepaturetime.Text = depttime;
                lblarrivaltime.Text = arrtime;
                lblstations.Text = fromstation + " - " + tostation;
                lblbookingdatetime.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");

                string strCat = getconcession(dsvcid, fromStonId, toStonId);
                hfCategoryList.Value = strCat.Substring(1, strCat.Length - 1);


                string StrSeatStructDown = "";
                wsClass obj = new wsClass();
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
                BindBoardingStations(ddlBoardingfrom, dsvcid, strpid);


            }
            else
            {
                Response.Redirect("../sessionTimeout.aspx");
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.ToString());
            _common.ErrorLog("CntrSeatSelection.aspx-0001", ex.Message.ToString());
        }
    }
    private void Errormsg(string msg)
    {
        string popup = _popup.modalPopupSmall("D", "Please Check", msg, "Close");
        Response.Write(popup);
    }

    #region "Method"
    private void CsrfTokenValidate()
    {
        if (hidtoken.Value != Session["rndNoCheck"].ToString())
        {
            Response.Redirect("errorpage.aspx");
            return;
        }
    }
    private string getconcession(string dsvcid, string fromStation, string toStation)//M5
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
            _common.ErrorLog("CntrSeatSelection.aspx-0002", ex.Message.ToString());
            return "";
        }
    }
    private void BindBoardingStations(DropDownList ddlBoardingfrom, string ServiceCode, string ServiceTripCode)//M2
    {
        try
        {
            ddlBoardingfrom.Items.Clear();
            wsClass obj = new wsClass();
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
            _common.ErrorLog("CntrSeatSelection.aspx-0003", ex.Message.ToString());
        }
    }
    public string getRandom()
    {
        string numbers = "1234567890";
        string characters = numbers;
        characters += numbers;
        int length = 6;
        string otp = string.Empty;
        for (int i = 0; i < length; i++)
        {
            string character = string.Empty;
            do
            {
                int index = new Random().Next(0, characters.Length);
                character = characters.ToCharArray()[index].ToString();
            } while (otp.IndexOf(character) != -1);
            otp += character;
        }
        return "123456";// otp;
    }
    private void SaveSeats()
    {
        try
        {

            if (_security.isSessionExist(Session["SearchParameters"]) == true)
            {
                string passengers = "";
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

                        passengers = passengers + seatno + "," + Name + "," + Gender + "," + Age + "," + Concession + "," + JourneyType + "," + Fare + "," + OnlineVerificationYN + "," + Passnumber + "," + IdVerificationYN + "," + Idverification + "," + DocumentVerificationYN + "," + DocumentVerification + "|";

                        tbpass_docid.Text = "";


                    }
                }

                passengers = passengers.Substring(0, passengers.Length - 1);
                wsClass obj = new wsClass();
                DataTable dtp = obj.SaveSeats(dsvcid, tripdirection, strpid, date, frstonid, tostonid, "C", Session["_UserCntrID"].ToString(), Session["_pmtCTZMobile"].ToString(), txtemialid.Text.Trim(), ddlBoardingfrom.SelectedValue.ToString(), passengers, IPAddress, "W");

                if (dtp.TableName == "Success")
                {
                    string ticketNo = dtp.Rows[0]["p_ticketnumber"].ToString();
                    if (ticketNo == "ERROR" || ticketNo == "EXCEPTION")
                    {
                        Errormsg(commonerror);
                    }
                    else
                    {
                        Session["p_ticketNo"] = ticketNo;
                        Response.Redirect("CntrTicketConfirmaton.aspx");
                    }
                }
                else
                {
                    Errormsg(commonerror);
                }
            }
            else
            {
                Response.Redirect("../sessionTimeout.aspx");
            }



        }
        catch (Exception ex)
        {
            Errormsg(ex.Message);
            _common.ErrorLog("CntrSeatSelection.aspx-0004", ex.Message.ToString());
        }
    }
    private void Successmsg(string msg)
    {
        string popup = _popup.modalPopupSmall("S", "Success", msg, "Close");
        Response.Write(popup);
    }
    #endregion

    #region "Event"
    protected void btnProceed_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        wsClass obj = new wsClass();
        if (_validation.IsValidInteger(txtMobileNo.Text, 10, 10) == false)
        {
            Errormsg("Please enter correct mobile number and select the seat(s) again");
            return;
        }
        Session["_pmtCTZMobile"] = txtMobileNo.Text.Trim();
        if (txtemialid.Text.Trim().Length > 0)
        {
            if (_validation.isValideMailID(txtemialid.Text.Trim()) == false)
            {
                Errormsg("Please enter valid email id and select the seat(s) again");
                return;
            }
            Session["_pmtCTZEMAIL"] = txtemialid.Text.Trim();
        }
        else
        {
            Session["_pmtCTZEMAIL"] = "utconline.utc@gmail.com";
        }
        if (_validation.IsValidInteger(ddlBoardingfrom.SelectedValue, 1, 5) == false)
        {
            Errormsg("Please select Boarding Station for Journey and select the seat(s) again");
            return;
        }
        string boardingStationId = ddlBoardingfrom.SelectedValue;
        string boardingStationName = ddlBoardingfrom.SelectedItem.Text;
        //SaveSeats();

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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "getSeatStruct('up','" + hfSeatStructUp.Value + "');", true);
                Errormsg("Sorry, Booking has been closed for selected service. Please change your selection and try again.");
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


                Int16 custAge = 0;

                Boolean bb = Int16.TryParse(customerDetail[3].Trim(), out custAge);
                if (bb = false || custAge <= 0)
                {
                    Errormsg("Age Should Be Greater Than 0.<br>And Please Select The Seat Again");
                    return;
                }


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

                //lblServiceType2.Text = dsvcid + "" + tripdirection + "" + strpid + " | " + servicetypename;
                //lblFromStationName2.Text = fromstation;
                //lblToStationName2.Text = tostation;
                //lblDateTime2.Text = journeyDate + " " + depttime;

                pnlConcession.Visible = true;
                pnlLayout.Visible = false;
            }
            else
                lbtnConfirmConcession_Click(btnProceed, null);/* TODO Change to default(_) if this is not a reference type */
        }
        else
            Response.Redirect("../sessionTimeout.aspx");
    }
    protected void lbtnConfirmConcession_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (Session["SearchParameters"] != null)
        {
            wsClass obj = new wsClass();
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
                                Session["_pmtCTZMobile"] = result.Rows[0]["mob"].ToString();
                            }
                        }
                        else
                        {
                            msgcount = msgcount + 1;
                            msg = msg + "Enter Valid Pass Number For concession " + gvSeats.DataKeys[row.RowIndex]["ConcessionName"].ToString() + " on seat number " + seatno + "<br/>";
                        }
                    }

                    if (msgcount > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "getSeatStruct('up','" + hfSeatStructUp.Value + "');", true);
                        Errormsg(msg);
                        return;
                    }



                    if (gvSeats.DataKeys[row.RowIndex]["OnlineVerificationYN"].ToString() == "Y")
                    {
                        CommonSMSnEmail sms = new CommonSMSnEmail();
                        string otp = getRandom();
                        sms.sendOtp(otp, Session["_pmtCTZMobile"].ToString());
                        Session["_otp"] = otp;
                        string text = Session["_pmtCTZMobile"].ToString();
                        string submob = text.Substring(text.Length - 3);
                        string mm = "XXXXXXX" + submob;
                        tbOTP.Text = "";
                        lblOtpMsg.Text = "You have opted for <b>" + gvSeats.DataKeys[row.RowIndex]["ConcessionName"].ToString() + "</b> Concession, Please enter 6 digit OTP sent to registered Mobile No.  ( " + mm + " ) linked with your bus pass.";
                        //RefreshCaptcha()

                        lblotperror.Visible = false;
                        mpotp.Show();
                        return;
                    }
                }

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
        CsrfTokenValidate();
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
        CsrfTokenValidate();
        gvSeats.DataSource = null;
        gvSeats.DataBind();

        Response.Redirect("CntrDashboard.aspx");
    }
    protected void lbtnClosepnlOTP_Click(object sender, EventArgs e)
    {
        mpotp.Hide();
    }
    protected void lbtnVerifypnlOTP_Click(object sender, EventArgs e)
    {
        if (_validation.IsValidInteger(tbOTP.Text, 6, 6) == false)
        {
            lblotperror.Text = "Enter Valid 6 Digit OTP.";
            lblotperror.Visible = true;
            mpotp.Show();
            return;
        }
        if (Session["_otp"].ToString() == tbOTP.Text)
        {
            SaveSeats();
        }
        else
        {
            lblotperror.Text = "Invalid OTP";
            lblotperror.Visible = true;
            mpotp.Show();
            return;
        }
    }
    #endregion



}