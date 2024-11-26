using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

/// <summary>
/// Summary description for wsTraveller
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class wsTraveller : System.Web.Services.WebService
{
    sbValidation _validation = new sbValidation();
    public ClassToken_pathik SoapHeader = new ClassToken_pathik();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private readonly object JSONConvert;
CommonSMSnEmail sms = new CommonSMSnEmail();
    public wsTraveller()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    #region "Token Related"
    [WebMethod()]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void AuthenticationMethod(string userId, string IMEI)
    {
        try
        {
            SoapHeader.UserId = userId;
            SoapHeader.UserIMEI = IMEI;

            if (SoapHeader == null)
            {
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                string jsonstr1 = finalJSON("900", "Result", DataTableToJSON(dt), "Something went wrong. Please try again. ");
                Context.Response.Write(jsonstr1);
            }
            if ((string.IsNullOrEmpty(SoapHeader.UserIMEI)))
            {
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                string jsonstr1 = finalJSON("900", "Result", DataTableToJSON(dt), "Something went wrong. Please try again. ");
                Context.Response.Write(jsonstr1);
            }
            if (!(SoapHeader.IsUserCredentialsValid(SoapHeader.UserId, SoapHeader.UserIMEI)))
            {
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                string jsonstr1 = finalJSON("900", "Result", DataTableToJSON(dt), "Something went wrong. Please try again. ");
                Context.Response.Write(jsonstr1);
            }
            // Create and store the AuthenticatedToken before running it
            string token = Guid.NewGuid().ToString();
            HttpRuntime.Cache.Add(token, SoapHeader.UserIMEI, null/* TODO Change to default(_) if this is not a reference type */, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(30), System.Web.Caching.CacheItemPriority.NotRemovable, null/* TODO Change to default(_) if this is not a reference type */);

            DataTable dt_token = new DataTable("dtToken");
            dt_token.Columns.Add("token", typeof(string));
            dt_token.Rows.Add(token);


            string code = "";
            string msg = "";

            if (dt_token.Rows.Count > 0)
            {
                code = "100";
                msg = "Valid Token";
            }
            else
            {
                code = "101";
                msg = "Invalid Token";
            }
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Result", DataTableToJSON(dt_token), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON("900", "Result", DataTableToJSON(dt), "Something went wrong. Please try again. " + ex.ToString());
            Context.Response.Write(jsonstr);
        }
    }
    #endregion

    #region"App is Active or Not"
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void isActiveApp(string token)
    {
        try
        {
            if (token.ToUpper() != "C7B21607E40B96E142889B56354089DA92A12A34")
            {
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                string jsonstrr = finalJSON("999", "Result", DataTableToJSON(dt), "Invalid Token.");
                Context.Response.Write(jsonstrr);
                return;
            }

            dt = new DataTable();
            wsClass obj = new wsClass();
            dt = obj.isappactive();

            sbXMLdata objXML = new sbXMLdata();
            DataTable dt_helpdesk = new DataTable("dthelpdesk");
            dt_helpdesk.Columns.Add("mobileNo", typeof(string));
            dt_helpdesk.Columns.Add("emailId", typeof(string));
            dt_helpdesk.Rows.Add(objXML.loadtollfree(), objXML.loadEmail());
            string code = "";
            string msg = "";
            if (dt.Rows.Count > 0)
            {
                code = "100";
                msg = "Valid Token";
            }
            else
            {
                code = "101";
                msg = "Invalid Token";
            }
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Result", DataTableToJSON(dt), "Helpdesk", DataTableToJSON(dt_helpdesk), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON("900", "Result", DataTableToJSON(dt), "Something went wrong. Please try again. " + ex.ToString());
            Context.Response.Write(jsonstr);
        }
    }
    #endregion

    #region "Traveller Login"
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void trvl_checkMobileNo(string mobileNo)
    {
        try
        {
            if (_validation.isValidMobileNumber(mobileNo) == false)
            {
                invalidparameters("Invalid Mobile No.");
                return;
            }

            dt = new DataTable();
            wsClass obj = new wsClass();
            dt = obj.traveller_dt(mobileNo);
            string code = "", msg = "";
            if (dt.Rows.Count > 0)
            {
                code = "100";
                msg = "";

                 //   9774681734 send SMS Code here
                if (mobileNo == "9999990099")
                {
                    string otp = "123456";
                    string EncryptOTP = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(otp, "SHA512");
                    dt.Columns.Add("EncryptOTP");
                    dt.Rows[0]["EncryptOTP"] = EncryptOTP;
                }
                else
                {
                    string otp = get6DigitRandomNumber();
                    sms.sendOtp(otp, mobileNo);

                    string EncryptOTP = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(otp, "SHA512");

                    dt.Columns.Add("EncryptOTP");
                    dt.Rows[0]["EncryptOTP"] = EncryptOTP;
                }
            }
            else
            {
                code = "101";
                msg = "Data Not Found";
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Traveller", DataTableToJSON(dt), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON("900", "Traveller", "NA", "Something went wrong. Please try again.");
            Context.Response.Write(jsonstr);
        }
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void trvl_loginFirstTime(string mobileNo, string userName, string login_app_web, string ip_imei)
    {
        try
        {
            if (_validation.isValidMobileNumber(mobileNo) == false)
            {
                invalidparameters("Invalid Mobile No.");
                return;
            }
            if (_validation.IsValidString(userName, 2, 50) == false)
            {
                invalidparameters("Invalid User Name");
                return;
            }
            if (_validation.IsValidString(login_app_web, 1, 1) == false)
            {
                invalidparameters("");
                return;
            }


            dt = new DataTable();
            wsClass obj = new wsClass();
            dt = obj.traveller_loginFirst_dt(mobileNo, userName, login_app_web, ip_imei);
            string code = "", msg = "";
            if (dt.Rows.Count > 0)
            {
                code = "100";
                msg = "";
            }
            else
            {
                code = "101";
                msg = "Data Not Found";
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Traveller", DataTableToJSON(dt), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON("900", "Traveller", "NA", "Something went wrong. Please try again.");
            Context.Response.Write(jsonstr);
        }
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void trvl_loginSuccess(string mobileNo, string login_app_web, string ip_imei)//M3
    {
        try
        {

            if (_validation.isValidMobileNumber(mobileNo) == false)
            {
                invalidparameters("Invalid Mobile No.");
                return;
            }

            if (_validation.IsValidString(login_app_web, 1, 1) == false)
            {
                invalidparameters("");
                return;
            }

            dt = new DataTable();
            wsClass obj = new wsClass();
            dt = obj.traveller_loginSuccess_dt(mobileNo, login_app_web, ip_imei);
            string code = "", msg = "";
            if (dt.Rows.Count > 0)
            {
                code = "100";
                msg = "";
            }
            else
            {
                code = "101";
                msg = "Data Not Found";
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Traveller", DataTableToJSON(dt), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON("900", "Traveller", "NA", "Something went wrong. Please try again.");
            Context.Response.Write(jsonstr);
        }
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void trvl_loginFail(string mobileNo, string login_app_web, string ip_imei)//M4
    {
        try
        {
            if (_validation.isValidMobileNumber(mobileNo) == false)
            {
                invalidparameters("Invalid Mobile No.");
                return;
            }

            if (_validation.IsValidString(login_app_web, 1, 1) == false)
            {
                invalidparameters("");
                return;
            }

            dt = new DataTable();
            wsClass obj = new wsClass();
            dt = obj.traveller_loginFail_dt(mobileNo, login_app_web, ip_imei);
            string code = "", msg = "";
            if (dt.Rows.Count > 0)
            {
                code = "100";
                msg = "";
            }
            else
            {
                code = "101";
                msg = "Data Not Found";
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Traveller", DataTableToJSON(dt), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON("900", "Traveller", "NA", "Something went wrong. Please try again.");
            Context.Response.Write(jsonstr);
        }
    }
    #endregion

    #region "Search Stations for App and Web"
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void advancedBookingDays(string token)
    {
        try
        {
            if (token.ToUpper() != "C7B21607E40B96E142889B56354089DA92A12A34")
            {
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                string jsonstrr = finalJSON("999", "Result", DataTableToJSON(dt), "Invalid Token.");
                Context.Response.Write(jsonstrr);
                return;
            }

            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_advance_days");
            DataTable MyTable = bll.SelectAll(MyCommand);
            string code = "";
            string msg = "";
            if (MyTable.Rows.Count > 0)
            {
                code = "100";
                msg = "";
            }
            else
            {
                code = "101";
                msg = "Data Not Found";
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Days", DataTableToJSON(MyTable), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception e)
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON("900", "Station", "NA", "Something went wrong. Please try again.");
            Context.Response.Write(jsonstr);
        }
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void search_station_app(string stationText, string flag_F_T, string otherValue, string token)
    {
        try
        {
            if (token.ToUpper() != "C7B21607E40B96E142889B56354089DA92A12A34")
            {
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                string jsonstrr = finalJSON("999", "Result", DataTableToJSON(dt), "Invalid Token.");
                Context.Response.Write(jsonstrr);
                return;
            }
            if (_validation.IsValidString(stationText, 1, 50) == false)
            {
                invalidparameters("Invalid Station Name");
                return;
            }
            if (_validation.IsValidString(flag_F_T, 1, 1) == false)
            {
                invalidparameters("Invalid Flag");
                return;
            }
            sbBLL bll = new sbBLL();
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_search_stations");
            MyCommand.Parameters.AddWithValue("p_search_text", stationText);
            MyCommand.Parameters.AddWithValue("p_from_to", flag_F_T);
            dt = bll.SelectAll(MyCommand);

            string code = "", msg = "";
            if (dt.Rows.Count > 0)
            {
                code = "100";
                msg = "";
            }
            else
            {
                code = "101";
                msg = "Data Not Found";
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Station", DataTableToJSON(dt), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception e)
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON("900", "Station", "NA", "Something went wrong. Please try again.");
            Context.Response.Write(jsonstr);
        }
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void search_services(string fromStationName, string toStationName, string serviceTypeId, string date, string token)
    {
        try
        {
            if (token.ToUpper() != "C7B21607E40B96E142889B56354089DA92A12A34")
            {
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                string jsonstrr = finalJSON("999", "Result", DataTableToJSON(dt), "Invalid Token.");
                Context.Response.Write(jsonstrr);
                return;
            }
            if (_validation.IsValidString(fromStationName, 2, 50) == false)
            {
                invalidparameters("Invalid From Station Name");
                return;
            }
            if (_validation.IsValidString(toStationName, 2, 50) == false)
            {
                invalidparameters("Invalid To Station Name");
                return;
            }
            if (_validation.IsValidString(serviceTypeId, 1, 5) == false)
            {
                invalidparameters("Invalid Service");
                return;
            }
            if (_validation.IsValidString(date, 10, 10) == false)
            {
                invalidparameters("Invalid Date");
                return;
            }


            dt = new DataTable();
            wsClass obj = new wsClass();
            dt = obj.search_services_dt(fromStationName, toStationName, serviceTypeId, date);

            string code = "", msg = "";
            if (dt.Rows.Count > 0)
            {
                DataRow[] dr = dt.Select("openclose='O'");
                if (dr.Length > 0)
                {
                    dt = dr.CopyToDataTable();
                    code = "100";
                    msg = "";
                }
                else
                {
                    code = "101";
                    msg = "Data Not Found";
                }
            }
            else
            {
                code = "101";
                msg = "Data Not Found";
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Services", DataTableToJSON(dt), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception e)
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON("900", "Services", "NA", "Something went wrong. Please try again.");
            Context.Response.Write(jsonstr);
        }
    }
    #endregion

    #region "Traveller Dashboard"
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void trvl_Dashboard(string mobileNo, string token)
    {
        try
        {
            if (token.ToUpper() != "C7B21607E40B96E142889B56354089DA92A12A34")
            {
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                string jsonstrr = finalJSON("999", "Result", DataTableToJSON(dt), "Invalid Token.");
                Context.Response.Write(jsonstrr);
                return;
            }

            if (_validation.isValidMobileNumber(mobileNo) == false)
            {
                invalidparameters("Invalid Mobile No.");
                return;
            }

            wsClass obj = new wsClass();
            DataTable dt_serviceType = new DataTable();
            dt_serviceType = obj.get_serviceType_dt();

            sbXMLdata objXML = new sbXMLdata();
            DataTable dt_helpdesk = new DataTable("dthelpdesk");
            dt_helpdesk.Columns.Add("mobileNo", typeof(string));
            dt_helpdesk.Columns.Add("emailId", typeof(string));
            dt_helpdesk.Rows.Add(objXML.loadtollfree(), objXML.loadEmail());

            //getWalletDetail_dt
            DataTable objWlt = new DataTable();
            objWlt = obj.getWalletDetail_dt(mobileNo);

            sbOffers objOffer = new sbOffers();
            DataTable dt_Offerscount = new DataTable();
            dt_Offerscount = objOffer.getOffers_count();


            DataTable dt_Offers = new DataTable();
            string date = DateTime.Now.Date.ToString("dd/MM/yyyy");
            dt_Offers = objOffer.getOffers(date, "100", "L");

            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_advance_days");
            DataTable dt_AdvanceDays = bll.SelectAll(MyCommand);

            string code = "100", msg = "";
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "ServiceTypes", DataTableToJSON(dt_serviceType), "Helpdesk", DataTableToJSON(dt_helpdesk), "Wallet", DataTableToJSON(objWlt), "offercount", DataTableToJSON(dt_Offerscount), "offer", DataTableToJSON(dt_Offers), "advancedays", DataTableToJSON(dt_AdvanceDays), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON("900", "ServiceTypes", "NA", "Something went wrong. Please try again.");
            Context.Response.Write(jsonstr);
        }
    }
    #endregion

    #region "Service Type"
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void bus_serviceType(string srtpId, string token)
    {
        try
        {
            if (_validation.IsValidString(srtpId, 1, 5) == false)
            {
                invalidparameters("Invalid Srtpid");
                return;
            }

            SoapHeader.UserAuthenticationToken = token;
            if (SoapHeader == null)
            {
                invalidTokenMessage();
                return;
            }

            // check user Credential valid
            if (!(SoapHeader.IsUserCredentialsValid(SoapHeader)))
            {
                invalidTokenMessage();
                return;
            }


            wsClass obj = new wsClass();
            DataTable dt_rslt = new DataTable();
            dt_rslt = obj.busservicetype(srtpId);


            DataTable dt_charge = new DataTable();
            dt_charge = obj.servicereservationcharge(srtpId);

            string code = "100", msg = "";

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "ServiceTypeMaxSeat", DataTableToJSON(dt_rslt), "ReservationCharge", DataTableToJSON(dt_charge), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON("900", "Result", "NA", "Something went wrong. Please try again.");
            Context.Response.Write(jsonstr);
        }
    }
    #endregion

    #region "Layout and Other"
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void layout_boarding(string dsvcId, string journeyDate, string strpId, string toStationId, string token)
    {
        try
        {
            if (_validation.IsValidInteger(dsvcId, 1, 8) == false)
            {
                invalidparameters("Invalid Service Code");
                return;
            }
            if (_validation.IsValidString(journeyDate, 10, 80) == false)
            {
                invalidparameters("Invalid Journey Date");
                return;
            }
            if (_validation.IsValidInteger(strpId, 1, 8) == false)
            {
                invalidparameters("Invalid Parameters");
                return;
            }
            if (_validation.IsValidInteger(toStationId, 1, 8) == false)
            {
                invalidparameters("Invalid To Station ID");
                return;
            }

            SoapHeader.UserAuthenticationToken = token;
            if (SoapHeader == null)
            {
                invalidTokenMessage();
                return;
            }

            // check user Credential valid
            if (!(SoapHeader.IsUserCredentialsValid(SoapHeader)))
            {
                invalidTokenMessage();
                return;
            }

            string triptype = "";
            wsClass obj = new wsClass();
            DataTable dt_up_rowColCount = new DataTable();
            dt_up_rowColCount = obj.getLayoutTotRowColumn(dsvcId);
            DataTable dt_up_layout = new DataTable();
            dt_up_layout = obj.getLayoutRowColumn(strpId, triptype, dsvcId, journeyDate);
            DataTable dt_down_rowColCount = new DataTable();
            dt_down_rowColCount = obj.getLayoutTotRowColumn(dsvcId);
            DataTable dt_down_layout = new DataTable();
            dt_down_layout = obj.getLayoutRowColumn(strpId, triptype, dsvcId, journeyDate);


            DataTable dt_boarding = new DataTable();
            dt_boarding = obj.get_boardingStations_dt(dsvcId, strpId);

            string code = "100", msg = "";

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "LowerLayoutRowCol", DataTableToJSON(dt_down_rowColCount), "LowerLayout", DataTableToJSON(dt_up_layout), "UpperLayoutRowCol", DataTableToJSON(dt_up_rowColCount), "UpperLayout", DataTableToJSON(dt_up_layout), "Boarding", DataTableToJSON(dt_boarding), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON("900", "ServiceTypes", "NA", "Something went wrong. Please try again.");
            Context.Response.Write(jsonstr);
        }
    }
    #endregion

    #region "Concession"
    [WebMethod()]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void getConcessionTypes(string dsvcid, string fromstationId, string tostationId, string token)
    {
        try
        {
            if (_validation.IsValidInteger(dsvcid, 1, 8) == false)
            {
                invalidparameters("Invalid Service Code");
                return;
            }
            if (_validation.IsValidInteger(fromstationId, 1, 8) == false)
            {
                invalidparameters("Invalid From Station ID");
                return;
            }

            if (_validation.IsValidInteger(tostationId, 1, 8) == false)
            {
                invalidparameters("Invalid To Station ID");
                return;
            }

            SoapHeader.UserAuthenticationToken = token;
            if (SoapHeader == null)
            {
                invalidTokenMessage();
                return;
            }

            // check user Credential valid
            if (!(SoapHeader.IsUserCredentialsValid(SoapHeader)))
            {
                invalidTokenMessage();
                return;
            }

            wsClass obj = new wsClass();
            DataTable dt_concessions = new DataTable();
            dt_concessions = obj.getConcessionCategory(dsvcid, fromstationId, tostationId);
            string code = "100";
            string msg = "";

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Concession", DataTableToJSON(dt_concessions), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON("900", "Concession", "NA", "Something went wrong. Please try again.");
            Context.Response.Write(jsonstr);
        }
    }
    [WebMethod()]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void checkConcession(string concession, string gender, string age, string token)
    {
        try
        {
            if (_validation.IsValidInteger(concession, 1, 8) == false)
            {
                invalidparameters("Invalid Concession Code");
                return;
            }
            if (_validation.IsValidString(gender, 1, 1) == false)
            {
                invalidparameters("Invalid Gender");
                return;
            }

            if (_validation.IsValidInteger(age, 1, 2) == false)
            {
                invalidparameters("Invalid Age");
                return;
            }

            SoapHeader.UserAuthenticationToken = token;
            if (SoapHeader == null)
            {
                invalidTokenMessage();
                return;
            }

            // check user Credential valid
            if (!(SoapHeader.IsUserCredentialsValid(SoapHeader)))
            {
                invalidTokenMessage();
                return;
            }


            wsClass obj = new wsClass();
            DataTable dt_concessions = new DataTable();
            dt_concessions = obj.CheckConcessionCategory(concession, gender, age);
            string code = "100";
            string msg = "";
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Concession", DataTableToJSON(dt_concessions), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON("900", "Concession", "NA", "Something went wrong. Please try again.");
            Context.Response.Write(jsonstr);
        }
    }
    [WebMethod()]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void checkConcessionPass(string concession, string passno, string journeyDate, string token)
    {
        try
        {
            if (_validation.IsValidInteger(concession, 1, 8) == false)
            {
                invalidparameters("Invalid Concession Code");
                return;
            }
            if (_validation.IsValidString(journeyDate, 10, 10) == false)
            {
                invalidparameters("Invalid Date");
                return;
            }

            SoapHeader.UserAuthenticationToken = token;
            if (SoapHeader == null)
            {
                invalidTokenMessage();
                return;
            }

            // check user Credential valid
            if (!(SoapHeader.IsUserCredentialsValid(SoapHeader)))
            {
                invalidTokenMessage();
                return;
            }


            wsClass obj = new wsClass();
            DataTable dt_concessions = new DataTable();
            string ss = obj.CheckBusPass(concession, passno, journeyDate);
            string code = "100";
            string msg = "";
            dt_concessions.Columns.Add("result");
            dt_concessions.Rows[0]["result"] = ss;

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Concession", DataTableToJSON(dt_concessions), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON("900", "Concession", "NA", "Something went wrong. Please try again.");
            Context.Response.Write(jsonstr);
        }
    }
    #endregion

    #region "Passengers Insertion"
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void savePassengers(string depotServiceCode, string tripType, string strpid, string journeyDate, string fromStationId, string toStationId, string booked_by_type_code, string userId, string userMobile, string userEmail, string bordeingStationId, string passengers, string ip_imei, string token)
    {
        try
        {
            if (_validation.IsValidInteger(depotServiceCode, 1, 8) == false)
            {
                invalidparameters("Invalid Service Code");
                return;
            }
            if (_validation.IsValidString(tripType, 1, 1) == false)
            {
                invalidparameters("Invalid Trip Type");
                return;
            }
            if (_validation.IsValidInteger(strpid, 1, 8) == false)
            {
                invalidparameters("Invalid Strpid");
                return;
            }
            if (_validation.IsValidString(journeyDate, 10, 10) == false)
            {
                invalidparameters("Invalid Journey Date");
                return;
            }
            if (_validation.IsValidInteger(fromStationId, 1, 8) == false)
            {
                invalidparameters("Invalid From Station");
                return;
            }
            if (_validation.IsValidInteger(toStationId, 1, 8) == false)
            {
                invalidparameters("Invalid To Station");
                return;
            }
            if (_validation.IsValidString(booked_by_type_code, 1, 1) == false)
            {
                invalidparameters("Invalid Booked By Type Code");
                return;
            }
            if (_validation.IsValidString(userId, 1, 50) == false)
            {
                invalidparameters("Invalid User ID");
                return;
            }
            if (_validation.isValidMobileNumber(userMobile) == false)
            {
                invalidparameters("Invalid Mobile No.");
                return;
            }
            if (_validation.IsValidInteger(bordeingStationId, 1, 8) == false)
            {
                invalidparameters("Invalid Boarding Station");
                return;
            }
            SoapHeader.UserAuthenticationToken = token;
            if (SoapHeader == null)
            {
                invalidTokenMessage();
                return;
            }

            // check user Credential valid
            if (!(SoapHeader.IsUserCredentialsValid(SoapHeader)))
            {
                invalidTokenMessage();
                return;
            }

            wsClass obj = new wsClass();
            if (obj.IsTicketStillAvaialable(passengers, journeyDate, "UP", strpid) == false)
            {
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                string jsonstrr = finalJSON("200", "Result", DataTableToJSON(dt), "You are late, Booking has already been made for the selected seat(s)");
                Context.Response.Write(jsonstrr);
                return;
            }
            DataTable dttt = new DataTable();
            dttt = obj.SaveSeats(depotServiceCode, tripType, strpid, journeyDate, fromStationId, toStationId, booked_by_type_code, userId, userMobile, userEmail, bordeingStationId, passengers, ip_imei, "M");


            string code = "100", msg = "";



            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Result", DataTableToJSON(dttt), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON("900", "Result", "NA", "Something went wrong. Please try again." + ex.ToString());
            Context.Response.Write(jsonstr);
        }
    }
    #endregion

    #region "Passengers Confirmation Details"
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void passengerConfirmDetails(string ticketNumber, string token)
    {
        try
        {
            if (_validation.IsValidString(ticketNumber, 12, 30) == false)
            {
                invalidparameters("Invalid Ticket No.");
                return;
            }

            SoapHeader.UserAuthenticationToken = token;
            if (SoapHeader == null)
            {
                invalidTokenMessage();
                return;
            }

            // check user Credential valid
            if (!(SoapHeader.IsUserCredentialsValid(SoapHeader)))
            {
                invalidTokenMessage();
                return;
            }

            wsClass obj = new wsClass();
            DataTable dt_TicketDetail = new DataTable();
            dt_TicketDetail = obj.getTicketDetails_byTicket(ticketNumber);

            DataTable dt_FareDetail = new DataTable();
            dt_FareDetail = obj.getFareDetails_byTicket(ticketNumber);

            DataTable dt_TaxDetail = new DataTable();
            dt_TaxDetail = obj.getTaxDetails_byTicket(ticketNumber);

            //   DataTable dt_passengerDetails = new DataTable();
            //   dt_passengerDetails = obj.getPassengerDetails(ticketNumber, "0");
            //   DataTable dt_taxDetails = new DataTable();
            //   dt_taxDetails = obj.getTaxDetails(ticketNumber);
            //   DataTable dt_cancelled = new DataTable();
            //  dt_cancelled = obj.getCancelledTickets(userId, ticketNumber);

            string code = "100", msg = "";

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "ticketDetails", DataTableToJSON(dt_TicketDetail), "ticketFare", DataTableToJSON(dt_FareDetail), "ticketTax", DataTableToJSON(dt_TaxDetail), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON("900", "JourneyDetail", "NA", "Something went wrong. Please try again.");
            Context.Response.Write(jsonstr);
        }
    }
    #endregion

    #region "Wallet"
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void walletDetails(string userId, string token)
    {
        try
        {
            if (_validation.isValidMobileNumber(userId) == false)
            {
                invalidparameters("Invalid Mobile No.");
                return;
            }

            SoapHeader.UserAuthenticationToken = token;
            if (SoapHeader == null)
            {
                invalidTokenMessage();
                return;
            }

            // check user Credential valid
            if (!(SoapHeader.IsUserCredentialsValid(SoapHeader)))
            {
                invalidTokenMessage();
                return;
            }

            wsClass obj = new wsClass();
            DataTable dt_wallet = new DataTable();
            dt_wallet = obj.getWalletDetail_dt(userId);

            string code = "100", msg = "";

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Wallet", DataTableToJSON(dt_wallet), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON("900", "JourneyDetail", "NA", "Something went wrong. Please try again.");
            Context.Response.Write(jsonstr);
        }
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void wallet_Ticket_Confirm(string userId, string ticketNo, string token)
    {
        try
        {
            if (_validation.isValidMobileNumber(userId) == false)
            {
                invalidparameters("Invalid Mobile No.");
                return;
            }

            SoapHeader.UserAuthenticationToken = token;
            if (SoapHeader == null)
            {
                invalidTokenMessage();
                return;
            }

            // check user Credential valid
            if (!(SoapHeader.IsUserCredentialsValid(SoapHeader)))
            {
                invalidTokenMessage();
                return;
            }

            wsClass obj = new wsClass();
            DataTable dt_wallet = new DataTable();
            dt_wallet = obj.wallet_ticket_confirm(userId, ticketNo);

            string code = "100", msg = "";

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Wallet", DataTableToJSON(dt_wallet), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON("900", "JourneyDetail", "NA", "Something went wrong. Please try again.");
            Context.Response.Write(jsonstr);
        }
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void wallet_Details_Transactions(string userId, string recordCount, string last_first_LF, string token)
    {
        try
        {
            if (_validation.isValidMobileNumber(userId) == false)
            {
                invalidparameters("Invalid Mobile No.");
                return;
            }

            SoapHeader.UserAuthenticationToken = token;
            if (SoapHeader == null)
            {
                invalidTokenMessage();
                return;
            }

            // check user Credential valid
            if (!(SoapHeader.IsUserCredentialsValid(SoapHeader)))
            {
                invalidTokenMessage();
                return;
            }

            wsClass obj = new wsClass();
            DataTable dt_wallet_txn = new DataTable();
            dt_wallet_txn = obj.getWalletTransactions_dt(userId, recordCount, last_first_LF);

            DataTable dt_wallet1 = new DataTable();
            dt_wallet1 = obj.getWalletDetail_dt(userId);


            string code = "100", msg = "";

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Wallet", DataTableToJSON(dt_wallet1), "WalletTransaction", DataTableToJSON(dt_wallet_txn), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON("900", "Wallet", "NA", "Something went wrong. Please try again.");
            Context.Response.Write(jsonstr);
        }
    }
    [WebMethod()]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void wallet_Topup_start(string userId, string amount, string token)
    {
        try
        {
            if (_validation.isValidMobileNumber(userId) == false)
            {
                invalidparameters("Invalid Mobile No.");
                return;
            }
            SoapHeader.UserAuthenticationToken = token;
            if (SoapHeader == null)
            {
                invalidTokenMessage();
                return;
            }

            // check user Credential valid
            if (!(SoapHeader.IsUserCredentialsValid(SoapHeader)))
            {
                invalidTokenMessage();
                return;
            }
            if (Convert.ToDecimal(amount) < 1)
            {
                invalidparameters("Invalid Amount");
                return;
            }


            wsClass objWallet = new wsClass();
            DataTable dtWlt = objWallet.walletTopup_start_completed("0", userId, amount, "F");
            string code = "100";
            string msg = "";
            if (dtWlt.Rows.Count > 0)
            {
                code = "100";
                msg = "";
            }
            else
            {
                code = "101";
                msg = "Data Not Found";
            }


            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Wallet", DataTableToJSON(dtWlt), msg);
            Context.Response.Write(jsonstr);

        }
        catch (Exception ex)
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON("900", "Result", DataTableToJSON(dtNothing), "Something went wrong. Please try again.");
            Context.Response.Write(jsonstr);
        }
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void walletTopupTxnStatus(string txnRefrence, string userId, string token)
    {
        try
        {
            if (_validation.isValidMobileNumber(userId) == false)
            {
                invalidparameters("Invalid Mobile No.");
                return;
            }

            SoapHeader.UserAuthenticationToken = token;
            if (SoapHeader == null)
            {
                invalidTokenMessage();
                return;
            }

            // check user Credential valid
            if (!(SoapHeader.IsUserCredentialsValid(SoapHeader)))
            {
                invalidTokenMessage();
                return;
            }

            wsClass obj = new wsClass();
            DataTable dttt = new DataTable();
            dttt = obj.walletTopup_txn_status(txnRefrence, userId);

            string code = "", msg = "";
            if (dttt.Rows.Count > 0)
            {
                code = "100"; msg = "";
            }
            else
            {
                code = "101"; msg = "No Data Found";
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "WalletStatus", DataTableToJSON(dttt), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON("900", "ServiceTypes", "NA", "Something went wrong. Please try again.");
            Context.Response.Write(jsonstr);
        }
    }

    #endregion

    #region "PG"
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void getPaymentGateways(string userId, string token)
    {
        try
        {
            SoapHeader.UserAuthenticationToken = token;
            if (SoapHeader == null)
            {
                invalidTokenMessage();
                return;
            }

            // check user Credential valid
            if (!(SoapHeader.IsUserCredentialsValid(SoapHeader)))
            {
                invalidTokenMessage();
                return;
            }

            HDFC obj = new HDFC();
            DataTable dt_rslt = new DataTable();
            dt_rslt = obj.get_PG();

            string code = "100", msg = "";

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Result", DataTableToJSON(dt_rslt), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON("900", "Result", "NA", "Something went wrong. Please try again.");
            Context.Response.Write(jsonstr);
        }
    }
    #endregion

    #region "Alarm"
    private DataTable dtNothing = new DataTable();
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void saveAlarm(string alarmTypeId, string reportedBy, string latt, string longg, string ticketNo, string token)
    {
        try
        {

            if (_validation.IsValidInteger(alarmTypeId, 1, 8) == false)
            {
                invalidparameters("Invalid Alarm Type Id.");
                return;
            }


            if (_validation.IsValidString(reportedBy, 5, 30) == false)
            {
                invalidparameters("Invalid Parameters.");
                return;
            }

            SoapHeader.UserAuthenticationToken = token;
            if (SoapHeader == null)
            {
                invalidTokenMessage();
                return;
            }

            // check user Credential valid
            if (!(SoapHeader.IsUserCredentialsValid(SoapHeader)))
            {
                invalidTokenMessage();
                return;
            }

            sbAlarm obj = new sbAlarm();
            DataTable dt_ = new DataTable();
            dt_ = obj.saveAlarm_dt(alarmTypeId, reportedBy, latt, longg, ticketNo);
            string code = "100";
            string msg = "";
            if (dt_.Rows.Count > 0)
            {
                code = "100";
                msg = "";
            }
            else
            {
                code = "101";
                msg = "Data Not Found";
            }


            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Alarm", DataTableToJSON(dt_), msg);
            Context.Response.Write(jsonstr);

        }
        catch (Exception ex)
        {

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON("900", "Alarm", DataTableToJSON(dtNothing), "Something went wrong. Please try again.");
            Context.Response.Write(jsonstr);
        }
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void getAlarmCategories(string userId, string token)
    {
        try
        {
            if (_validation.isValidMobileNumber(userId) == false)
            {
                invalidparameters("Invalid Mobile No.");
                return;
            }
            SoapHeader.UserAuthenticationToken = token;
            if (SoapHeader == null)
            {
                invalidTokenMessage();
                return;
            }

            // check user Credential valid
            if (!(SoapHeader.IsUserCredentialsValid(SoapHeader)))
            {
                invalidTokenMessage();
                return;
            }
            sbAlarm obj = new sbAlarm();
            DataTable dt_ = new DataTable();
            dt_ = obj.getAlarmCategories();
            string code = "100";
            string msg = "";
            if (dt_.Rows.Count > 0)
            {
                code = "100";
                msg = "";
            }
            else
            {
                code = "101";
                msg = "Data Not Found";
            }
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Alarm", DataTableToJSON(dt_), msg);
            Context.Response.Write(jsonstr);

        }
        catch (Exception ex)
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON("900", "Alarm", DataTableToJSON(dtNothing), "Something went wrong. Please try again.");
            Context.Response.Write(jsonstr);
        }
    }
    #endregion

    #region "Grievance"
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void getGrievanceCategories(string userId, string token)
    {
        try
        {
            if (_validation.isValidMobileNumber(userId) == false)
            {
                invalidparameters("Invalid Mobile No.");
                return;
            }

            SoapHeader.UserAuthenticationToken = token;
            if (SoapHeader == null)
            {
                invalidTokenMessage();
                return;
            }

            // check user Credential valid
            if (!(SoapHeader.IsUserCredentialsValid(SoapHeader)))
            {
                invalidTokenMessage();
                return;
            }

            wsClass obj = new wsClass();
            DataTable dt_rslt = new DataTable();
            dt_rslt = obj.getGrievanceCategories();
            string code = "";
            string msg = "";
            if (dt_rslt.Rows.Count > 0)
            {
                code = "100";
                msg = "";
            }
            else
            {
                code = "101";
                msg = "Data Not Found";
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Grievance", DataTableToJSON(dt_rslt), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON("900", "Result", "NA", "Something went wrong. Please try again.");
            Context.Response.Write(jsonstr);
        }
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void saveGrievance(string category, string subcategory, string busno, string ticketno, string description, string pic1, string pic2, string latt, string longg, string userId, string ip_imei, string token)
    {
        try
        {
            if (_validation.isValidMobileNumber(userId) == false)
            {
                invalidparameters("Invalid Mobile No.");
                return;
            }

            if (_validation.IsValidInteger(category, 1, 8) == false)
            {
                invalidparameters("Invalid Category");
                return;
            }
            if (_validation.IsValidString(subcategory, 1, 1) == false)
            {
                invalidparameters("Invalid Sub Category");
                return;
            }

            if (_validation.isValidMobileNumber(userId) == false)
            {
                invalidparameters("Invalid Mobile No.");
                return;
            }

            SoapHeader.UserAuthenticationToken = token;
            if (SoapHeader == null)
            {
                invalidTokenMessage();
                return;
            }

            // check user Credential valid
            if (!(SoapHeader.IsUserCredentialsValid(SoapHeader)))
            {
                invalidTokenMessage();
                return;
            }


            wsClass obj = new wsClass();
            DataTable dt_rslt = new DataTable();
            dt_rslt = obj.saveGrievances(category, subcategory, busno, ticketno, description, pic1, pic2, latt, longg, userId, ip_imei);

            string code = "";
            string msg = "";
            if (dt_rslt.Rows.Count > 0)
            {
                code = "100";
                msg = "";
            }
            else
            {
                code = "101";
                msg = "Data Not Found";
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Grievance", DataTableToJSON(dt_rslt), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON("900", "Result", "NA", "Something went wrong. Please try again.");
            Context.Response.Write(jsonstr);
        }
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void getGrievances(string userId, string token)
    {
        try
        {
            if (_validation.isValidMobileNumber(userId) == false)
            {
                invalidparameters("Invalid Mobile No.");
                return;
            }
            SoapHeader.UserAuthenticationToken = token;
            if (SoapHeader == null)
            {
                invalidTokenMessage();
                return;
            }

            // check user Credential valid
            if (!(SoapHeader.IsUserCredentialsValid(SoapHeader)))
            {
                invalidTokenMessage();
                return;
            }

            wsClass obj = new wsClass();
            DataTable dt_rslt = new DataTable();
            dt_rslt = obj.getGrievances(userId);

            string code = "";
            string msg = "";
            if (dt_rslt.Rows.Count > 0)
            {
                code = "100";
                msg = "";
            }
            else
            {
                code = "101";
                msg = "Data Not Found";
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Grievance", DataTableToJSON(dt_rslt), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON("900", "Result", "NA", "Something went wrong. Please try again.");
            Context.Response.Write(jsonstr);
        }
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void getGrievanceDetail(string userId, string RefNo, string token)
    {
        try
        {
            if (_validation.isValidMobileNumber(userId) == false)
            {
                invalidparameters("Invalid Mobile No.");
                return;
            }
            SoapHeader.UserAuthenticationToken = token;
            if (SoapHeader == null)
            {
                invalidTokenMessage();
                return;
            }

            // check user Credential valid
            if (!(SoapHeader.IsUserCredentialsValid(SoapHeader)))
            {
                invalidTokenMessage();
                return;
            }


            wsClass obj = new wsClass();
            DataTable dt_rslt = new DataTable();
            dt_rslt = obj.getGrievanceDetails(RefNo);

            string code = "";
            string msg = "";
            if (dt_rslt.Rows.Count > 0)
            {
                code = "100";
                msg = "";
            }
            else
            {
                code = "101";
                msg = "Data Not Found";
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Grievance", DataTableToJSON(dt_rslt), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON("900", "Result", "NA", "Something went wrong. Please try again.");
            Context.Response.Write(jsonstr);
        }
    }
    #endregion

    #region "Offers"
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void OffersCount(string userId, string token)
    {
        try
        {
            if (_validation.isValidMobileNumber(userId) == false)
            {
                invalidparameters("Invalid Mobile No.");
                return;
            }

            SoapHeader.UserAuthenticationToken = token;
            if (SoapHeader == null)
            {
                invalidTokenMessage();
                return;
            }

            // check user Credential valid
            if (!(SoapHeader.IsUserCredentialsValid(SoapHeader)))
            {
                invalidTokenMessage();
                return;
            }

            sbOffers obj = new sbOffers();
            DataTable dt_rslt = new DataTable();
            dt_rslt = obj.getOffers_count();
            string code = "";
            string msg = "";
            if (dt_rslt.Rows.Count > 0)
            {
                code = "100";
                msg = "";
            }
            else
            {
                code = "101";
                msg = "Data Not Found";
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Offers", DataTableToJSON(dt_rslt), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON("900", "Result", "NA", "Something went wrong. Please try again.");
            Context.Response.Write(jsonstr);
        }
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void offers(string userId, string token)
    {
        try
        {
            if (_validation.isValidMobileNumber(userId) == false)
            {
                invalidparameters("Invalid Mobile No.");
                return;
            }

            SoapHeader.UserAuthenticationToken = token;
            if (SoapHeader == null)
            {
                invalidTokenMessage();
                return;
            }

            // check user Credential valid
            if (!(SoapHeader.IsUserCredentialsValid(SoapHeader)))
            {
                invalidTokenMessage();
                return;
            }

            sbOffers objOffer = new sbOffers();
            DataTable dt_rslt = new DataTable();
            string date = DateTime.Now.Date.ToString("dd/MM/yyyy");
            dt_rslt = objOffer.getOffers(date, "10", "L");
            string code = "";
            string msg = "";
            if (dt_rslt.Rows.Count > 0)
            {
                code = "100";
                msg = "";
            }
            else
            {
                code = "101";
                msg = "Data Not Found";
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Offers", DataTableToJSON(dt_rslt), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON("900", "Result", "NA", "Something went wrong. Please try again.");
            Context.Response.Write(jsonstr);
        }
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void offerDetails(string userId, string offerId, string token)
    {
        try
        {
            if (_validation.isValidMobileNumber(userId) == false)
            {
                invalidparameters("Invalid Mobile No.");
                return;
            }

            if (_validation.IsValidInteger(offerId, 1, 8) == false)
            {
                invalidparameters("Invalid Offer Id.");
                return;
            }

            SoapHeader.UserAuthenticationToken = token;
            if (SoapHeader == null)
            {
                invalidTokenMessage();
                return;
            }

            // check user Credential valid
            if (!(SoapHeader.IsUserCredentialsValid(SoapHeader)))
            {
                invalidTokenMessage();
                return;
            }

            sbOffers objOffer = new sbOffers();
            DataTable dt_rslt = new DataTable();
            string date = DateTime.Now.Date.ToString("dd/MM/yyyy");
            dt_rslt = objOffer.getOfferDetail(offerId);
            string code = "";
            string msg = "";
            if (dt_rslt.Rows.Count > 0)
            {
                code = "100";
                msg = "";
            }
            else
            {
                code = "101";
                msg = "Data Not Found";
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Offers", DataTableToJSON(dt_rslt), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON("900", "Result", "NA", "Something went wrong. Please try again.");
            Context.Response.Write(jsonstr);
        }
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void offerApply(string userId, string ticketNo, string offerId, string token)
    {
        try
        {
            if (_validation.isValidMobileNumber(userId) == false)
            {
                invalidparameters("Invalid Mobile No.");
                return;
            }
            if (_validation.IsValidInteger(ticketNo, 12, 30) == false)
            {
                invalidparameters("Invalid Ticket.");
                return;
            }
            if (_validation.IsValidInteger(offerId, 1, 8) == false)
            {
                invalidparameters("Invalid Offer Id.");
                return;
            }

            SoapHeader.UserAuthenticationToken = token;
            if (SoapHeader == null)
            {
                invalidTokenMessage();
                return;
            }

            // check user Credential valid
            if (!(SoapHeader.IsUserCredentialsValid(SoapHeader)))
            {
                invalidTokenMessage();
                return;
            }

            sbOffers objOffer = new sbOffers();
            DataTable dt_rslt = new DataTable();
            string date = DateTime.Now.Date.ToString("dd/MM/yyyy");
            dt_rslt = objOffer.applyOffer(userId, ticketNo, offerId);
            string code = "";
            string msg = "";
            if (dt_rslt.Rows.Count > 0)
            {
                code = "100";
                msg = "";
            }
            else
            {
                code = "101";
                msg = "Data Not Found";
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Offers", DataTableToJSON(dt_rslt), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON("900", "Result", "NA", "Something went wrong. Please try again.");
            Context.Response.Write(jsonstr);
        }
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void offerRemove(string userId, string ticketNo, string offerId, string token)
    {
        try
        {
            if (_validation.isValidMobileNumber(userId) == false)
            {
                invalidparameters("Invalid Mobile No.");
                return;
            }
            if (_validation.IsValidInteger(ticketNo, 12, 30) == false)
            {
                invalidparameters("Invalid Ticket.");
                return;
            }
            if (_validation.IsValidInteger(offerId, 1, 8) == false)
            {
                invalidparameters("Invalid Offer Id.");
                return;
            }

            SoapHeader.UserAuthenticationToken = token;
            if (SoapHeader == null)
            {
                invalidTokenMessage();
                return;
            }

            // check user Credential valid
            if (!(SoapHeader.IsUserCredentialsValid(SoapHeader)))
            {
                invalidTokenMessage();
                return;
            }

            sbOffers objOffer = new sbOffers();
            DataTable dt_rslt = new DataTable();
            string date = DateTime.Now.Date.ToString("dd/MM/yyyy");
            dt_rslt = objOffer.removeOffer(ticketNo, offerId);
            string code = "";
            string msg = "";
            if (dt_rslt.Rows.Count > 0)
            {
                code = "100";
                msg = "";
            }
            else
            {
                code = "101";
                msg = "Data Not Found";
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Offers", DataTableToJSON(dt_rslt), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON("900", "Result", "NA", "Something went wrong. Please try again.");
            Context.Response.Write(jsonstr);
        }
    }
    #endregion

    #region "Ticket Cancellation"
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void getCancelledtxns(string userId, string ticketNo, string token)
    {
        try
        {
            if (_validation.isValidMobileNumber(userId) == false)
            {
                invalidparameters("Invalid Mobile No.");
                return;
            }
            //if (_validation.IsValidString(ticketNo, 12, 30) == false)
            //{
            //    invalidparameters("Invalid Ticket No.");
            //    return;
            //}


            SoapHeader.UserAuthenticationToken = token;
            if (SoapHeader == null)
            {
                invalidTokenMessage();
                return;
            }

            // check user Credential valid
            if (!(SoapHeader.IsUserCredentialsValid(SoapHeader)))
            {
                invalidTokenMessage();
                return;
            }

            wsClass obj = new wsClass();
            DataTable dt_rslt = new DataTable();
            dt_rslt = obj.getCancelledtxns(userId, ticketNo);
            string code = "";
            string msg = "";
            if (dt_rslt.Rows.Count > 0)
            {
                code = "100";
                msg = "";
            }
            else
            {
                code = "101";
                msg = "Data Not Found";
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Ticket", DataTableToJSON(dt_rslt), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON("900", "Result", "NA", "Something went wrong. Please try again.");
            Context.Response.Write(jsonstr);
        }
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void getCancelAvailableTicketsPsngr(string userId, string ticketNo, string bookedByType, string token)
    {
        try
        {
            if (_validation.isValidMobileNumber(userId) == false)
            {
                invalidparameters("Invalid Mobile No.");
                return;
            }
            if (_validation.IsValidString(ticketNo, 12, 30) == false)
            {
                invalidparameters("Invalid Ticket No.");
                return;
            }
            if (_validation.IsValidString(bookedByType, 1, 1) == false)
            {
                invalidparameters("Invalid Bookby Type");
                return;
            }

            SoapHeader.UserAuthenticationToken = token;
            if (SoapHeader == null)
            {
                invalidTokenMessage();
                return;
            }

            // check user Credential valid
            if (!(SoapHeader.IsUserCredentialsValid(SoapHeader)))
            {
                invalidTokenMessage();
                return;
            }

            wsClass obj = new wsClass();
            DataTable dt_rslt = new DataTable();
            dt_rslt = obj.getPassengerDetailsForCancel(ticketNo, "A");
            string code = "";
            string msg = "";
            if (dt_rslt.Rows.Count > 0)
            {
                code = "100";
                msg = "";
            }
            else
            {
                code = "101";
                msg = "Data Not Found";
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Ticket", DataTableToJSON(dt_rslt), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON("900", "Result", "NA", "Something went wrong. Please try again.");
            Context.Response.Write(jsonstr);
        }
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void saveCancelTicket(string userId, string ticketNo, string seatNos, string seatCounts, string cancellededByType, string token)
    {
        try
        {
            if (_validation.isValidMobileNumber(userId) == false)
            {
                invalidparameters("Invalid Mobile No.");
                return;
            }
            if (_validation.IsValidString(ticketNo, 12, 30) == false)
            {
                invalidparameters("Invalid Ticket No.");
                return;
            }
            if (_validation.IsValidString(seatCounts, 1, 2) == false)
            {
                invalidparameters("Invalid Seat Count");
                return;
            }

            SoapHeader.UserAuthenticationToken = token;
            if (SoapHeader == null)
            {
                invalidTokenMessage();
                return;
            }

            // check user Credential valid
            if (!(SoapHeader.IsUserCredentialsValid(SoapHeader)))
            {
                invalidTokenMessage();
                return;
            }

            wsClass obj = new wsClass();
            DataTable dt_rslt = new DataTable();
            dt_rslt = obj.cancelTicket(ticketNo, seatNos, "0", Convert.ToInt32(seatCounts), userId, "", cancellededByType, "M");
            string code = "";
            string msg = "";
            if (dt_rslt.Rows.Count > 0)
            {
                string amount = dt_rslt.Rows[0]["cancel_amount"].ToString();
                string refno = dt_rslt.Rows[0]["cancellationrefno"].ToString();
                sms.sendTicketCancel_SMSnEMAIL(ticketNo, userId, seatNos,amount,refno);
                code = "100";
                msg = "";

            }
            else
            {
                code = "101";
                msg = "Data Not Found";
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Ticket", DataTableToJSON(dt_rslt), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON("900", "Result", "NA", "Something went wrong. Please try again.");
            Context.Response.Write(jsonstr);
        }
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void getCancelAvailableTickets(string userId, string ticketNo, string bookedByType, string token)
    {
        try
        {
            if (_validation.isValidMobileNumber(userId) == false)
            {
                invalidparameters("Invalid Mobile No.");
                return;
            }

            if (_validation.IsValidString(bookedByType, 1, 1) == false)
            {
                invalidparameters("Invalid Book By Type.");
                return;
            }

            SoapHeader.UserAuthenticationToken = token;
            if (SoapHeader == null)
            {
                invalidTokenMessage();
                return;
            }

            // check user Credential valid
            if (!(SoapHeader.IsUserCredentialsValid(SoapHeader)))
            {
                invalidTokenMessage();
                return;
            }

            wsClass obj = new wsClass();
            DataTable dt_rslt = new DataTable();
            dt_rslt = obj.getAvailableTicket(userId, ticketNo, bookedByType);
            string code = "";
            string msg = "";
            if (dt_rslt.Rows.Count > 0)
            {
                code = "100";
                msg = "";
            }
            else
            {
                code = "101";
                msg = "Data Not Found";
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Ticket", DataTableToJSON(dt_rslt), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON("900", "Result", "NA", "Something went wrong. Please try again.");
            Context.Response.Write(jsonstr);
        }
    }
    #endregion

    #region "Refund Status"
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void getRefundTickets(string userId, string token)
    {
        try
        {
            if (_validation.isValidMobileNumber(userId) == false)
            {
                invalidparameters("Invalid Mobile No.");
                return;
            }

            SoapHeader.UserAuthenticationToken = token;
            if (SoapHeader == null)
            {
                invalidTokenMessage();
                return;
            }

            // check user Credential valid
            if (!(SoapHeader.IsUserCredentialsValid(SoapHeader)))
            {
                invalidTokenMessage();
                return;
            }

            wsClass obj = new wsClass();
            DataTable dt_rslt = new DataTable();
            dt_rslt = obj.getRefundTickets(userId);
            string code = "";
            string msg = "";
            if (dt_rslt.Rows.Count > 0)
            {
                code = "100";
                msg = "";
            }
            else
            {
                code = "101";
                msg = "Data Not Found";
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Ticket", DataTableToJSON(dt_rslt), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON("900", "Result", "NA", "Something went wrong. Please try again.");
            Context.Response.Write(jsonstr);
        }
    }
    #endregion

    #region "Ticket History"
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void getActiveTickets(string userId, string token)
    {
        try
        {
            if (_validation.isValidMobileNumber(userId) == false)
            {
                invalidparameters("Invalid Mobile No.");
                return;
            }

            SoapHeader.UserAuthenticationToken = token;
            if (SoapHeader == null)
            {
                invalidTokenMessage();
                return;
            }

            // check user Credential valid
            if (!(SoapHeader.IsUserCredentialsValid(SoapHeader)))
            {
                invalidTokenMessage();
                return;
            }

            wsClass obj = new wsClass();
            DataTable dt_rslt = new DataTable();
            dt_rslt = obj.getActiveTickets(userId);
            string code = "";
            string msg = "";
            if (dt_rslt.Rows.Count > 0)
            {
                code = "100";
                msg = "";
            }
            else
            {
                code = "101";
                msg = "Data Not Found";
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Ticket", DataTableToJSON(dt_rslt), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON("900", "Result", "NA", "Something went wrong. Please try again.");
            Context.Response.Write(jsonstr);
        }
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void getTickets(string userId, string transactiontype, string token)
    {
        try
        {
            if (_validation.isValidMobileNumber(userId) == false)
            {
                invalidparameters("Invalid Mobile No.");
                return;
            }

            SoapHeader.UserAuthenticationToken = token;
            if (SoapHeader == null)
            {
                invalidTokenMessage();
                return;
            }

            // check user Credential valid
            if (!(SoapHeader.IsUserCredentialsValid(SoapHeader)))
            {
                invalidTokenMessage();
                return;
            }

            wsClass obj = new wsClass();
            DataTable dt_TicketDetail = new DataTable();
            dt_TicketDetail = obj.getTickets(userId);
            string code = "";
            string msg = "";
            if (dt_TicketDetail.Rows.Count > 0)
            {
                code = "100";
                msg = "";
            }
            else
            {
                code = "101";
                msg = "Data Not Found";
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Ticket", DataTableToJSON(dt_TicketDetail), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON("900", "Result", "NA", "Something went wrong. Please try again.");
            Context.Response.Write(jsonstr);
        }
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void getTicketDetails(string ticketNo, string token)
    {
        try
        {
            if (_validation.IsValidString(ticketNo, 12, 30) == false)
            {
                invalidparameters("Invalid Ticket No.");
                return;
            }

            SoapHeader.UserAuthenticationToken = token;
            if (SoapHeader == null)
            {
                invalidTokenMessage();
                return;
            }

            // check user Credential valid
            if (!(SoapHeader.IsUserCredentialsValid(SoapHeader)))
            {
                invalidTokenMessage();
                return;
            }

            wsClass obj = new wsClass();
            DataTable dt_TicketDetail = new DataTable();
            dt_TicketDetail = obj.getTicketDetails_byTicket(ticketNo);

            DataTable dt_FareDetail = new DataTable();
            dt_FareDetail = obj.getFareDetails_byTicket(ticketNo);

            DataTable dt_TaxDetail = new DataTable();
            dt_TaxDetail = obj.getTaxDetails_byTicket(ticketNo);

            DataTable dt_ticketlog = new DataTable();
            dt_ticketlog = obj.getTicketLog_byTicket(ticketNo);


            DataTable dt_cncl = new DataTable();
            dt_cncl = obj.getCancelledtxns("0", ticketNo);

            string code = "100", msg = "";

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "ticketDetails", DataTableToJSON(dt_TicketDetail), "ticketFare", DataTableToJSON(dt_FareDetail), "ticketTax", DataTableToJSON(dt_TaxDetail), "ticketLog", DataTableToJSON(dt_ticketlog), "cancel", DataTableToJSON(dt_cncl), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON("900", "JourneyDetail", "NA", "Something went wrong. Please try again.");
            Context.Response.Write(jsonstr);
        }
    }
    #endregion

    #region "Rating"
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void getRatingTickets(string userId, string token)
    {
        try
        {
            if (_validation.isValidMobileNumber(userId) == false)
            {
                invalidparameters("Invalid Mobile No.");
                return;
            }

            SoapHeader.UserAuthenticationToken = token;
            if (SoapHeader == null)
            {
                invalidTokenMessage();
                return;
            }

            // check user Credential valid
            if (!(SoapHeader.IsUserCredentialsValid(SoapHeader)))
            {
                invalidTokenMessage();
                return;
            }

            wsClass obj = new wsClass();
            DataTable dt_rslt = new DataTable();
            dt_rslt = obj.getRatingTicketsNew(userId);
            string code = "";
            string msg = "";
            if (dt_rslt.Rows.Count > 0)
            {
                code = "100";
                msg = "";
            }
            else
            {
                code = "101";
                msg = "Data Not Found";
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Ticket", DataTableToJSON(dt_rslt), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON("900", "Result", "NA", "Something went wrong. Please try again.");
            Context.Response.Write(jsonstr);
        }
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void saveRating(string userId, string tktNo, string bookingRate, string conductorRate, string busRate, string bookingFeedback, string conductorFeedback, string busFeedback, string IMEI, string token)
    {
        try
        {
            if (_validation.isValidMobileNumber(userId) == false)
            {
                invalidparameters("Invalid Mobile No.");
                return;
            }
            if (_validation.IsValidString(tktNo, 12, 30) == false)
            {
                invalidparameters("Invalid Ticket No.");
                return;
            }

            SoapHeader.UserAuthenticationToken = token;
            if (SoapHeader == null)
            {
                invalidTokenMessage();
                return;
            }

            // check user Credential valid
            if (!(SoapHeader.IsUserCredentialsValid(SoapHeader)))
            {
                invalidTokenMessage();
                return;
            }

            wsClass obj = new wsClass();
            DataTable dt_rslt = new DataTable();
            dt_rslt = obj.saveRatingsNew(userId, tktNo, bookingRate, conductorRate, busRate, bookingFeedback, conductorFeedback, busFeedback, IMEI);
            string code = "";
            string msg = "";
            if (dt_rslt.Rows.Count > 0)
            {
                code = "100";
                msg = "";
            }
            else
            {
                code = "101";
                msg = "Data Not Found";
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Ticket", DataTableToJSON(dt_rslt), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON("900", "Result", "NA", "Something went wrong. Please try again.");
            Context.Response.Write(jsonstr);
        }
    }
    #endregion

    #region "Sms Email"
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void send_ticket_confirmation(string ticketNo,string token)
    {
        try
        {
            if (token.ToUpper() != "C7B21607E40B96E142889B56354089DA92A12A34")
            {
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                string jsonstrr = finalJSON("999", "Result", DataTableToJSON(dt), "Invalid Token.");
                Context.Response.Write(jsonstrr);
                return;
            }

            CommonSMSnEmail obj = new CommonSMSnEmail();
            DataTable dt_rslt = new DataTable();
            obj.sendTicketConfirm_SMSnEMAIL(ticketNo,"B","2");
 
            string code = "";
            string msg = "";

            code = "100";
            msg = "";

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Result", DataTableToJSON(dt), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON("900", "Result", "NA", "Something went wrong. Please try again.");
            Context.Response.Write(jsonstr);
        }
    }
    #endregion

    #region "QR Code"
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void getQRTextEn(string ticketNo, string token)
    {
        try
        {
            AesEncrptDecrpt aes = new AesEncrptDecrpt();

            wsClass obj = new wsClass();
            DataTable dt_rslt = new DataTable();
            dt_rslt = obj.getTicketDetails_byTicket(ticketNo);
            //  string text1 = aes.DecryptStringAES("CpCf280aP6ujM/SmBCXFXQ==");
            string code = "";
            string msg = "";
            string text = "0";
            if (dt_rslt.Rows.Count > 0)
            {
                // ticket_no status

                // text = aes.AESE(dt_rslt.Rows[0]["ticket_no"].ToString() + " - " + dt_rslt.Rows[0]["status"].ToString());
                code = "101";
                msg = "Data Not Found";
            }
            else
            {
                text = "";
                code = "100";
                msg = "";
            }
            string final = "";
            final = final + "{";
            final = final + "\"code\":\"" + code + "\",";
            final = final + "\"Text\":\"" + text + "\",";
            final = final + "\"msg\":\"" + msg + "\"";
            final = final + "}";

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";

            Context.Response.Write(final);
        }
        catch (Exception ex)
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON("900", "Result", "NA", "Something went wrong. Please try again.");
            Context.Response.Write(jsonstr);
        }
    }
    #endregion


    #region "Confirm Ticket"
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void confirm_ticket_enc(string ticketNo, string token)
    {
        try
        {
            if (token.ToUpper() != "C7B21607E40B96E142889B56354089DA92A12A34")
            {
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                string jsonstrr = finalJSON("999", "Result", DataTableToJSON(dt), "Invalid Token.");
                Context.Response.Write(jsonstrr);
                return;
            }


            AesEncrptDecrpt aes = new AesEncrptDecrpt();

            sbTicket dd = new sbTicket();
            string ss = dd.GetTicket(ticketNo);
            string encrypt_byte = "";
            byte[] pdfBytes;
            if (ss.Trim().Length > 0)
            {
                NReco.PdfGenerator.HtmlToPdfConverter htmlToPdf = new NReco.PdfGenerator.HtmlToPdfConverter();
                pdfBytes = htmlToPdf.GeneratePdf(ss);

                encrypt_byte = aes.Enc(Convert.ToBase64String(pdfBytes));
            }

            DataTable dt_t = new DataTable("dt");
            dt_t.Columns.Add("pdfByte_base64", typeof(string));
            dt_t.Rows.Add(encrypt_byte);

            string code = "";
            string msg = "";

            code = "100";
            msg = "";

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Result", DataTableToJSON(dt_t), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON("900", "Result", "NA", "Something went wrong. Please try again.");
            Context.Response.Write(jsonstr);
        }
    }
    #endregion
#region "Confirm Ticket Byte"
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void confirm_ticket_byte(string ticketNo, string token)
    {
        try
        {
            if (token.ToUpper() != "C7B21607E40B96E142889B56354089DA92A12A34")
            {
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                string jsonstrr = finalJSON("999", "Result", DataTableToJSON(dt), "Invalid Token.");
                Context.Response.Write(jsonstrr);
                return;
            }


            AesEncrptDecrpt aes = new AesEncrptDecrpt();

            sbTicket dd = new sbTicket();
            string ss = dd.GetTicket(ticketNo);
            string encrypt_byte = "";
            byte[] pdfBytes;
            if (ss.Trim().Length > 0)
            {
                NReco.PdfGenerator.HtmlToPdfConverter htmlToPdf = new NReco.PdfGenerator.HtmlToPdfConverter();
                pdfBytes = htmlToPdf.GeneratePdf(ss);

                encrypt_byte = Convert.ToBase64String(pdfBytes);
            }

            DataTable dt_t = new DataTable("dt");
            dt_t.Columns.Add("pdfByte_base64", typeof(string));
            dt_t.Rows.Add(encrypt_byte);

            string code = "";
            string msg = "";

            code = "100";
            msg = "";

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Result", DataTableToJSON(dt_t), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON("900", "Result", "NA", "Something went wrong. Please try again.");
            Context.Response.Write(jsonstr);
        }
    }
    #endregion
    #region "Json Create Functions"
    public string DataTableToJSON(DataTable table)
    {
        JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
        List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
        Dictionary<string, object> childRow;
        foreach (DataRow row in table.Rows)
        {
            childRow = new Dictionary<string, object>();
            foreach (DataColumn col in table.Columns)
            {
                childRow.Add(col.ColumnName, row[col]);
            }
            parentRow.Add(childRow);
        }
        return jsSerializer.Serialize(parentRow);
    }
    public string finalJSON(string code, string data1Name, string data1, string errorMessage)
    {
        string final = "";
        final = final + "{";
        final = final + "\"code\":\"" + code + "\",";
        final = final + "\"" + data1Name + "\":" + data1 + ",";
        final = final + "\"msg\":\"" + errorMessage + "\"";
        final = final + "}";
        return final;
    }
    public string finalJSON(string code, string data1Name, string data1, string data2Name, string data2, string errorMessage)
    {
        string final = "";
        final = final + "{";
        final = final + "\"code\":\"" + code + "\",";
        final = final + "\"" + data1Name + "\":" + data1 + ",";
        final = final + "\"" + data2Name + "\":" + data2 + ",";
        final = final + "\"msg\":\"" + errorMessage + "\"";
        final = final + "}";
        return final;
    }
    public string finalJSON(string code, string data1Name, string data1, string data2Name, string data2, string data3Name, string data3, string errorMessage)
    {
        string final = "";
        final = final + "{";
        final = final + "\"code\":\"" + code + "\",";
        final = final + "\"" + data1Name + "\":" + data1 + ",";
        final = final + "\"" + data2Name + "\":" + data2 + ",";
        final = final + "\"" + data3Name + "\":" + data3 + ",";
        final = final + "\"msg\":\"" + errorMessage + "\"";
        final = final + "}";
        return final;
    }
    public string finalJSON(string code, string data1Name, string data1, string data2Name, string data2, string data3Name, string data3, string data4Name, string data4, string errorMessage)
    {
        string final = "";
        final = final + "{";
        final = final + "\"code\":\"" + code + "\",";
        final = final + "\"" + data1Name + "\":" + data1 + ",";
        final = final + "\"" + data2Name + "\":" + data2 + ",";
        final = final + "\"" + data3Name + "\":" + data3 + ",";
        final = final + "\"" + data4Name + "\":" + data4 + ",";
        final = final + "\"msg\":\"" + errorMessage + "\"";
        final = final + "}";
        return final;
    }
    public string finalJSON(string code, string data1Name, string data1, string data2Name, string data2, string data3Name, string data3, string data4Name, string data4, string data5Name, string data5, string errorMessage)
    {
        string final = "";
        final = final + "{";
        final = final + "\"code\":\"" + code + "\",";
        final = final + "\"" + data1Name + "\":" + data1 + ",";
        final = final + "\"" + data2Name + "\":" + data2 + ",";
        final = final + "\"" + data3Name + "\":" + data3 + ",";
        final = final + "\"" + data4Name + "\":" + data4 + ",";
        final = final + "\"" + data5Name + "\":" + data5 + ",";
        final = final + "\"msg\":\"" + errorMessage + "\"";
        final = final + "}";
        return final;
    }
    public string finalJSON(string code, string data1Name, string data1, string data2Name, string data2, string data3Name, string data3, string data4Name, string data4, string data5Name, string data5, string data6Name, string data6, string errorMessage)
    {
        string final = "";
        final = final + "{";
        final = final + "\"code\":\"" + code + "\",";
        final = final + "\"" + data1Name + "\":" + data1 + ",";
        final = final + "\"" + data2Name + "\":" + data2 + ",";
        final = final + "\"" + data3Name + "\":" + data3 + ",";
        final = final + "\"" + data4Name + "\":" + data4 + ",";
        final = final + "\"" + data5Name + "\":" + data5 + ",";
        final = final + "\"" + data6Name + "\":" + data6 + ",";
        final = final + "\"msg\":\"" + errorMessage + "\"";
        final = final + "}";
        return final;
    }
    #endregion

    #region"Common Function"
    private void invalidparameters(string msg)
    {
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        string jsonstr = finalJSON("888", "Result", DataTableToJSON(dt), "Invalid Parameters. " + msg);
        Context.Response.Write(jsonstr);
    }
    private void invalidTokenMessage()
    {
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        string jsonstr = finalJSON("999", "Result", DataTableToJSON(dt), "Invalid Token. ");
        Context.Response.Write(jsonstr);
    }
    public string get6DigitRandomNumber()
    {
        Random random = new Random();
        return random.Next(0, 999999).ToString("D6");

    }
    #endregion

}
