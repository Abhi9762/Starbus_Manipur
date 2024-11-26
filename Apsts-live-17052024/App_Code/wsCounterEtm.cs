using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

/// <summary>
/// Summary description for wsCounterEtm
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class wsCounterEtm : System.Web.Services.WebService
{   //8A5CE72749C4FEAAB9D5D1EBD34735D0FD32442F
    sbValidation _validation = new sbValidation();
    public classTokenCounterEtm SoapHeader = new classTokenCounterEtm();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private readonly object JSONConvert;
    CommonSMSnEmail sms = new CommonSMSnEmail();
    wsCounterEtmClass obj = new wsCounterEtmClass();
    DataTable dtnothing = new DataTable();
    sbSecurity _security = new sbSecurity();
    Cache cachechk = new Cache();
    wsClass trvlclass = new wsClass();
    classCounter clscounter = new classCounter();
    private sbCommonFunc _common = new sbCommonFunc();
    public wsCounterEtm()
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
                return;
            }
            if ((string.IsNullOrEmpty(SoapHeader.UserIMEI)))
            {
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                string jsonstr1 = finalJSON("900", "Result", DataTableToJSON(dt), "Something went wrong. Please try again. ");
                Context.Response.Write(jsonstr1);
                return;
            }
            if ((SoapHeader.IsUserCredentialsValid(SoapHeader.UserId, SoapHeader.UserIMEI) == false))
            {
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                string jsonstr1 = finalJSON("900", "Result", DataTableToJSON(dt), "Something went wrong. Please try again. ");
                Context.Response.Write(jsonstr1);
                return;
            }
            // Create and store the AuthenticatedToken before running it
            string token = Guid.NewGuid().ToString();
            HttpRuntime.Cache.Add(token, SoapHeader.UserIMEI, null/* TODO Change to default(_) if this is not a reference type */, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(30), System.Web.Caching.CacheItemPriority.NotRemovable, null/* TODO Change to default(_) if this is not a reference type */);

            DataTable dt_token = new DataTable("dtToken");
            dt_token.Columns.Add("token", typeof(string));
            dt_token.Rows.Add(token);

            sbCommonFunc _common = new sbCommonFunc();
            _common.ErrorLog(dt_token.TableName, dt_token.Rows.Count.ToString());
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
            exceptionMessage("Something went wrong. Please try again.");
        }
    }
    #endregion

    #region"App Active Or Not"
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void isActiveApp(string imei, string token)
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
            if ((SoapHeader.IsUserCredentialsValid(SoapHeader) == false))
            {
                invalidTokenMessage();
                return;
            }

            if (obj.isDeviceRegistered(imei) == false)
            {
                invalidDeviceMessage();
                return;
            }

            dt = new DataTable();
            dt = obj.isappactive();
            string code = "";
            string msg = "";
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
            string jsonstr = finalJSON(code, "Result", DataTableToJSON(dt), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            exceptionMessage("Something went wrong. Please try again.");
        }
    }
    #endregion

    #region"Login & Logout"
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void counterLogin(string userId, string userPwd, string logindatetime, string imei, string latt, string longg, string token)
    {
        try
        {
            SoapHeader.UserAuthenticationToken = token;
            if (SoapHeader == null)
            {
                invalidTokenMessage();
                return;
            }
            string code = "";
            string msg = "";
            // check user Credential valid
            if ((SoapHeader.IsUserCredentialsValid(SoapHeader) == false))
            {
                invalidTokenMessage();
                return;
            }

            //check already login
            if (CheckAlreadyLogin(userId, logindatetime) == "Y")
            {
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                string jsonstrr = finalJSON("103", "Result", DataTableToJSON(dtnothing), "User is already logged in, Do you want to logged out the user first ?");
                Context.Response.Write(jsonstrr);
                return;
            }

            dt = new DataTable();
            dt = obj.userdetail(userId);
            if (dt.Rows.Count > 0)
            {

                if (dt.Rows[0]["userlogintype"].ToString().Trim() == "W")
                {
                    code = "101";
                    msg = "You are not eligible to login on the ETM machine. You can login only Web.";
                    Context.Response.Clear();
                    Context.Response.ContentType = "application/json";
                    string jsonstr = finalJSON(code, "Result", DataTableToJSON(dtnothing), msg);
                    Context.Response.Write(jsonstr);
                    return;
                }

                //if (dt.Rows[0]["device_id"].ToString().ToUpper() != imei.ToUpper())
                //{
                //    Context.Response.Clear();
                //    Context.Response.ContentType = "application/json";
                //    string jsonstrr = finalJSON("101", "Result", DataTableToJSON(dtnothing), "This device is not allocated to you. Please contact helpdesk");
                //    Context.Response.Write(jsonstrr);
                //    return;
                //}

                dt.Columns.Add("LOGID", typeof(string));
                if (checkPassword(dt.Rows[0]["upwd"].ToString(), userPwd) == true)
                {
                    string logid = trvlclass.UpdateLoginLog(userId, imei, "E", imei, latt, longg);
                    foreach (DataRow row in dt.Rows)
                        row["LOGID"] = logid;
                    if (Convert.ToInt16(dt.Rows[0]["urole"].ToString().Trim()) == 4)
                    {
                        if (logid == "0")
                        {
                            code = "101";
                            msg = "Invalid login credentials1";
                            Context.Response.Clear();
                            Context.Response.ContentType = "application/json";
                            string jsonstr = finalJSON(code, "Result", DataTableToJSON(dtnothing), msg);
                            Context.Response.Write(jsonstr);
                            return;
                        }
                        else
                        {
                            code = "100";
                            msg = "";
                            Context.Response.Clear();
                            Context.Response.ContentType = "application/json";
                            string jsonstr = finalJSON(code, "Result", DataTableToJSON(dt), msg);
                            Context.Response.Write(jsonstr);
                        }
                    }
                    else
                    {
                        code = "102";
                        msg = "Invalid user role";
                        Context.Response.Clear();
                        Context.Response.ContentType = "application/json";
                        string jsonstr = finalJSON(code, "Result", DataTableToJSON(dtnothing), msg);
                        Context.Response.Write(jsonstr);
                        return;
                    }
                }
                else
                {
                    code = "101";
                    msg = "Invalid login credentials2.";
                    Context.Response.Clear();
                    Context.Response.ContentType = "application/json";
                    string jsonstr = finalJSON(code, "Result", DataTableToJSON(dtnothing), msg);
                    Context.Response.Write(jsonstr);
                    return;
                }
            }
            else
            {
                code = "101";
                msg = "Invalid login credentials3";
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                string jsonstr = finalJSON(code, "Result", DataTableToJSON(dtnothing), msg);
                Context.Response.Write(jsonstr);
                return;
            }
        }
        catch (Exception ex)
        {
            exceptionMessage("Something went wrong. Please try again.");
        }
    }
    private string CheckAlreadyLogin(string userId, string logindatetime)
    {
        string alreadyYN = "N";
        try
        {
            if (cachechk["Login_" + userId] != null)
            {
                alreadyYN = "Y";
                return alreadyYN;
            }
            cachechk.Add("Login_" + userId, logindatetime, null/* TODO Change to default(_) if this is not a reference type */, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.Default, null/* TODO Change to default(_) if this is not a reference type */);
            return alreadyYN;
        }
        catch (Exception ex)
        {
            return alreadyYN;
        }
    }
    private bool checkPassword(string passworddatabase, string userpassword)
    {
        if (passworddatabase.ToUpper() == userpassword.ToUpper())
        {
            return true;
        }
        else
        { return false; }
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void RemoveLogin(string userId, string logid, string token)
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
            if ((SoapHeader.IsUserCredentialsValid(SoapHeader) == false))
            {
                invalidTokenMessage();
                return;
            }

            obj.updateLogoutDetails(logid);
            _security.RemoveUserLogin(userId);
            string code = "100";
            string msg = "You are succesfully logout";

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Result", DataTableToJSON(dtnothing), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            exceptionMessage("Something went wrong. Please try again.");
        }
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void CheckUser(string userId, string logindatetime, string token)
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
            if ((SoapHeader.IsUserCredentialsValid(SoapHeader) == false))
            {
                invalidTokenMessage();
                return;
            }
            string code = "";
            string msg = "";
            if (cachechk["Login_" + userId] == null)
            {
                code = "101";
                msg = "You account is logout";
            }
            if (cachechk["Login_" + userId] != null)
            {
                if (cachechk["Login_" + userId].ToString() != logindatetime)
                {
                    code = "101";
                    msg = "You account is logout";
                }
                else
                {
                    code = "100";
                    msg = "You are Loged in";
                }
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Result", DataTableToJSON(dtnothing), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            exceptionMessage("Something went wrong. Please try again.");
        }
    }
    #endregion

    #region"Dashboard"
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void dashboard(string userId, string cntrId, string action, string date, string token)
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
            if ((SoapHeader.IsUserCredentialsValid(SoapHeader) == false))
            {
                invalidTokenMessage();
                return;
            }

            DataTable dtdetail = new DataTable();
            dtdetail = obj.loadCountorUserDetails(userId, cntrId);

            DataTable dt = new DataTable();
            dt = clscounter.GetDepotYN(cntrId, action);

            DataTable dtonline = new DataTable();
            dtonline = obj.loadOnltktpasstoday(cntrId);

            DataTable cntraccountsmry = new DataTable();
            cntraccountsmry = obj.loadAccountSummary(cntrId);

            DataTable dtcurrnt = new DataTable();
            dtcurrnt = obj.loadOfflinetkttoday(cntrId, date);

            DataTable dtServiceType = new DataTable();
            dtServiceType = trvlclass.get_serviceType_dt();

            DataTable dtOpenTrips = new DataTable();
            dtOpenTrips = obj.load_OpenTrips(cntrId);

            DataTable dt_tripOpenClose = new DataTable();
            dt_tripOpenClose = obj.loadTrip_OpenClose(userId, cntrId, "0", "0", "0", DateTime.Now.Date.AddDays(-7).ToString("dd/MM/yyyy"), DateTime.Now.Date.ToString("dd/MM/yyyy"));

            DataTable dtadvancedays = new DataTable();
            dtadvancedays = obj.getAdvanceDays();

            string code = "100";
            string msg = "";
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "User", DataTableToJSON(dtdetail), "Depot", DataTableToJSON(dt), "Account", DataTableToJSON(cntraccountsmry), "Onlinetkt", DataTableToJSON(dtonline), "Currnttkt", DataTableToJSON(dtcurrnt), "ServiceType", DataTableToJSON(dtServiceType), "Trips", DataTableToJSON(dt_tripOpenClose), "OpenTrips", DataTableToJSON(dtOpenTrips), "Days", DataTableToJSON(dtadvancedays), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            exceptionMessage("Something went wrong. Please try again.");
        }
    }

    #endregion

    #region"Check Ticket Status"
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void CheckTicketStatus(string ticketno, string bookingmode, string token)
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
            if ((SoapHeader.IsUserCredentialsValid(SoapHeader) == false))
            {
                invalidTokenMessage();
                return;
            }

            dt = new DataTable();
            dt = obj.CheckTicketStaus(ticketno, bookingmode);
            string code = "";
            string msg = "";
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
            string jsonstr = finalJSON(code, "Status", DataTableToJSON(dt), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            exceptionMessage("Something went wrong. Please try again.");
        }
    }
    #endregion

    #region"Days"
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void advancedBookingDays(string token)
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
            if ((SoapHeader.IsUserCredentialsValid(SoapHeader) == false))
            {
                invalidTokenMessage();
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
            exceptionMessage("Something went wrong. Please try again.");
        }
    }
    #endregion

    #region "Search Stations for App and Web"
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void search_station_app(string stationText, string flag_F_T, string otherValue, string token)
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
            if ((SoapHeader.IsUserCredentialsValid(SoapHeader) == false))
            {
                invalidTokenMessage();
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

            dt = obj.search_station_app(stationText, flag_F_T);

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
            exceptionMessage("Something went wrong. Please try again.");
        }
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void search_services(string fromStationName, string toStationName, string serviceTypeId, string date, string token)
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
            if ((SoapHeader.IsUserCredentialsValid(SoapHeader) == false))
            {
                invalidTokenMessage();
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
            dt = trvlclass.search_services_dt(fromStationName, toStationName, serviceTypeId, date);

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
            exceptionMessage("Something went wrong. Please try again.");
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
            SoapHeader.UserAuthenticationToken = token;
            if (SoapHeader == null)
            {
                invalidTokenMessage();
                return;
            }

            // check user Credential valid
            if ((SoapHeader.IsUserCredentialsValid(SoapHeader) == false))
            {
                invalidTokenMessage();
                return;
            }

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

            string triptype = "";

            DataTable dt_up_rowColCount = new DataTable();
            dt_up_rowColCount = trvlclass.getLayoutTotRowColumn(dsvcId);
            DataTable dt_up_layout = new DataTable();
            dt_up_layout = trvlclass.getLayoutRowColumn(strpId, triptype, dsvcId, journeyDate);
            DataTable dt_down_rowColCount = new DataTable();
            dt_down_rowColCount = trvlclass.getLayoutTotRowColumn(dsvcId);
            DataTable dt_down_layout = new DataTable();
            dt_down_layout = trvlclass.getLayoutRowColumn(strpId, triptype, dsvcId, journeyDate);


            DataTable dt_boarding = new DataTable();
            dt_boarding = trvlclass.get_boardingStations_dt(dsvcId, strpId);

            string code = "100", msg = "";

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "LowerLayoutRowCol", DataTableToJSON(dt_down_rowColCount), "LowerLayout", DataTableToJSON(dt_up_layout), "UpperLayoutRowCol", DataTableToJSON(dt_up_rowColCount), "UpperLayout", DataTableToJSON(dt_up_layout), "Boarding", DataTableToJSON(dt_boarding), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            exceptionMessage("Something went wrong. Please try again.");
        }
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void getLayout(string stationId, string BusRegnumber, string Tripdate, string token)
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
            if ((SoapHeader.IsUserCredentialsValid(SoapHeader) == false))
            {
                invalidTokenMessage();
                return;
            }

            DataTable dt_rowCol = new DataTable();
            dt_rowCol = obj.GetRowColumn(BusRegnumber, Tripdate, stationId);
            DataTable dt_totalrowCol = new DataTable();
            dt_totalrowCol = clscounter.GetTotalRowColumn(BusRegnumber);

            string code = "100", msg = "";

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "TotalRowColumn", DataTableToJSON(dt_totalrowCol), "RowColumn", DataTableToJSON(dt_rowCol), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            exceptionMessage("Something went wrong. Please try again.");
        }
    }
    #endregion

    #region "Concession"
    [WebMethod()]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void checkConcession(string concession, string gender, string age, string token)
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

            DataTable dt_concessions = new DataTable();
            dt_concessions = trvlclass.CheckConcessionCategory(concession, gender, age);
            string code = "", msg = "";
            if (dt_concessions.Rows.Count > 0)
            {
                code = "100"; msg = "";
            }
            else
            {
                code = "101"; msg = "Data Not Found";
            }
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Concession", DataTableToJSON(dt_concessions), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            exceptionMessage("Something went wrong. Please try again.");
        }
    }
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

            DataTable dt_concessions = new DataTable();
            dt_concessions = trvlclass.getConcessionCategory(dsvcid, fromstationId, tostationId);
            string code = "", msg = "";
            if (dt_concessions.Rows.Count > 0)
            {
                code = "100"; msg = "";
            }
            else
            {
                code = "101"; msg = "Data Not Found";
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Concession", DataTableToJSON(dt_concessions), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            exceptionMessage("Something went wrong. Please try again.");
        }
    }
    [WebMethod()]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void getConcession(string serviceTypeId, string routeId, string stationId, string token)
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

            DataTable dt_concessions = new DataTable();
            dt_concessions = clscounter.GetConcession(serviceTypeId, stationId, routeId);
            string code = "", msg = "";
            if (dt_concessions.Rows.Count > 0)
            {
                code = "100"; msg = "";
            }
            else
            {
                code = "101"; msg = "Data Not Found";
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Concession", DataTableToJSON(dt_concessions), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            exceptionMessage("Something went wrong. Please try again.");
        }
    }
    [WebMethod()]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void getConcessionDtls(string p_Concession, string token)
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

            DataTable dt_concessions = new DataTable();
            dt_concessions = trvlclass.getConcessionDtls(p_Concession);
            string code = "", msg = "";
            if (dt_concessions.Rows.Count > 0)
            {
                code = "100"; msg = "";
            }
            else
            {
                code = "101"; msg = "Data Not Found";
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Concession", DataTableToJSON(dt_concessions), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            exceptionMessage("Something went wrong. Please try again.");
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
            dt_concessions = obj.CheckBusPassNew(concession, passno, journeyDate);
            string code = "", msg = "";
            if (dt_concessions.Rows.Count > 0)
            {
                code = "100"; msg = "";
            }
            else
            {
                code = "101"; msg = "Data Not Found";
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Concession", DataTableToJSON(dt_concessions), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            exceptionMessage("Something went wrong. Please try again.");
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


            if (trvlclass.checkTripGenerate(journeyDate, strpid) == false)
            {
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                string jsonstrr = finalJSON("200", "Result", DataTableToJSON(dt), "You are late, Booking has already been made for the selected seat(s)");
                Context.Response.Write(jsonstrr);
                return;
            }
            if (trvlclass.IsTicketStillAvaialable(passengers, journeyDate, "UP", strpid) == false)
            {
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                string jsonstrr = finalJSON("200", "Result", DataTableToJSON(dt), "You are late, Booking has already been made for the selected seat(s)");
                Context.Response.Write(jsonstrr);
                return;
            }
            DataTable dttt = new DataTable();
            dttt = trvlclass.SaveSeats(depotServiceCode, tripType, strpid, journeyDate, fromStationId, toStationId, booked_by_type_code, userId, userMobile, userEmail, bordeingStationId, passengers, ip_imei, "E");


            string code = "100", msg = "";



            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Result", DataTableToJSON(dttt), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            exceptionMessage("Something went wrong. Please try again.");
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
            if (_validation.IsValidString(ticketNumber, 12, 30) == false)
            {
                invalidparameters("Invalid Ticket No.");
                return;
            }

            DataTable dt_TicketDetail = new DataTable();
            dt_TicketDetail = trvlclass.getTicketDetails_byTicket(ticketNumber);

            DataTable dt_FareDetail = new DataTable();
            dt_FareDetail = trvlclass.getFareDetails_byTicket(ticketNumber);

            DataTable dt_TaxDetail = new DataTable();
            dt_TaxDetail = trvlclass.getTaxDetails_byTicket(ticketNumber);

            string code = "100", msg = "";

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "ticketDetails", DataTableToJSON(dt_TicketDetail), "ticketFare", DataTableToJSON(dt_FareDetail), "ticketTax", DataTableToJSON(dt_TaxDetail), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            exceptionMessage("Something went wrong. Please try again.");
        }
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void updateTicket(string ticketno, string counterid, string usercode, string pmt_mode, string ip_imei, string token)
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
            if (_validation.IsValidString(ticketno, 12, 30) == false)
            {
                invalidparameters("Invalid Ticket No.");
                return;
            }

            DataTable dt_ = new DataTable();
            dt_ = obj.updateTkt(ticketno, counterid, usercode, pmt_mode);

            string code = "100", msg = "";

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "ticketUpdate", DataTableToJSON(dt_), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            exceptionMessage("Something went wrong. Please try again.");
        }
    }
    #endregion

    #region"Trip"
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void getOpenTrips(string cntrId, string token)
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

            DataTable dtOpenTrips = new DataTable();
            dtOpenTrips = obj.load_OpenTrips(cntrId);

            string code = "100", msg = "";

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "OpenTrips", DataTableToJSON(dtOpenTrips), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            exceptionMessage("Something went wrong. Please try again.");
        }
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void getTripChart(string tripcode, string updatedby, string actionType, string token)
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

            DataTable dtOpenTrips = new DataTable();
            dtOpenTrips = obj.loadTripChart(tripcode, updatedby, actionType);

            DataTable dtTripChartSeat = new DataTable();
            dtTripChartSeat = obj.loadTripChartSeat(tripcode);

            string code = "100", msg = "";

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "TripChart", DataTableToJSON(dtOpenTrips), "TripChartSeat", DataTableToJSON(dtTripChartSeat), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            exceptionMessage("Something went wrong. Please try again.");
        }
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void OnlineTripGenerateDtls(string ServiceTypeid, string depotcode, string TripStatus, string stationId, string token)
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

            DataTable dtbus = new DataTable();
            dtbus = clscounter.GetBusList(depotcode, ServiceTypeid);

            DataTable dtconductor = new DataTable();
            dtconductor = clscounter.GetConductor(depotcode);

            DataTable dtdriver = new DataTable();
            dtdriver = clscounter.GetDriver(depotcode);

            string code = "100", msg = "";

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Buses", DataTableToJSON(dtbus), "Driver", DataTableToJSON(dtdriver), "Conductor", DataTableToJSON(dtconductor), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            exceptionMessage("Something went wrong. Please try again.");
        }
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void TripChartGenerateEBTM(string mode, string tripCode, string p_waybill, string strp_id, string srtp_date,
        string busno, string driver_code, string driver2_code, string conductor_code, string conductor2_code, string latt,
        string longg, string parking_place, string updated_by, string token)
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
            DataTable dtTripChart = new DataTable();
            dtTripChart = trvlclass.generate_trip_chart(mode, tripCode, p_waybill, strp_id, srtp_date, busno, driver_code, driver2_code,
                conductor_code, conductor2_code, latt, longg, parking_place, updated_by);

            string code = "100", msg = "";

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Trip_Status", DataTableToJSON(dtTripChart), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            exceptionMessage("Something went wrong. Please try again.");
        }
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void TripChartDtls(string tripCode, string tripDate, string token)
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

            DataTable dtsourcedest = new DataTable();
            dtsourcedest = trvlclass.gettrip_details(tripCode);

            DataTable dtpsngrdetail = new DataTable();
            dtpsngrdetail = obj.loadtrip_Dtls(tripCode, tripDate);

            string code = "0", msg = "";
            if (dtsourcedest.Rows.Count > 0 )
            { code = "100"; msg = ""; }
            else
            { code = "101"; msg = "Data Not Found"; }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "SOURCE_DEST", DataTableToJSON(dtsourcedest), "TRIP_DETAILS", DataTableToJSON(dtpsngrdetail), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            exceptionMessage("Something went wrong.Please try again.");
        }
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void OnlineTripChart(string StationCode, string dattime, string TripStatus, string token)
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

            DataTable bookingChart_dt = new DataTable();
            bookingChart_dt = trvlclass.gettripagent("0", StationCode, "O", TripStatus, dattime);

            string code = "";
            string msg = "";
            if (bookingChart_dt.Rows.Count > 0)
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
            string jsonstr = finalJSON(code, "OnlineTripChart", DataTableToJSON(bookingChart_dt), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            exceptionMessage("Something went wrong.Please try again.");
        }
    }
    #endregion

    #region"Depot,Buses,Routes,Driver,Conductor,RouteTrip,Idtype"
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void getDepot(string token)
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

            DataTable dt = new DataTable();
            dt = clscounter.GetDepotList();

            string code = "", msg = "";
            if (dt.Rows.Count > 0)
            {
                code = "100"; msg = "";
            }
            else
            {
                code = "101"; msg = "Data Not Found";
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Depot", DataTableToJSON(dt), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            exceptionMessage("Something went wrong. Please try again.");
        }
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void getBusesRoutes(string depotId, string serviceTypeId, string cntrId, string stationId, string token)
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

            DataTable dtbus = new DataTable();
            dtbus = clscounter.GetBusList(depotId, serviceTypeId);

            DataTable dtconductor = new DataTable();
            dtconductor = clscounter.GetConductor(depotId);

            DataTable dtdriver = new DataTable();
            dtdriver = clscounter.GetDriver(depotId);

            DataTable dtroutes = new DataTable();
            dtroutes = clscounter.GetBusRoute(serviceTypeId, stationId);

            DataTable dtservices = new DataTable();
            dtservices = clscounter.GetDepotServices(serviceTypeId, stationId);

            string code = "100", msg = "";

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Buses", DataTableToJSON(dtbus), "Conductors", DataTableToJSON(dtconductor), "Routes", DataTableToJSON(dtroutes), "Services", DataTableToJSON(dtservices), "Driver", DataTableToJSON(dtdriver), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            exceptionMessage("Something went wrong. Please try again.");
        }
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void getTrips(string stationId, string routeId, string serviceTypeId, string token)
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

            DataTable dtroutetrip = new DataTable();
            dtroutetrip = clscounter.GetTrips(serviceTypeId, stationId, routeId);

            string code = "", msg = "";
            if (dtroutetrip.Rows.Count > 0)
            {
                code = "100"; msg = "";
            }
            else
            {
                code = "101"; msg = "Data Not Found";
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Trips", DataTableToJSON(dtroutetrip), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            exceptionMessage("Something went wrong. Please try again.");
        }
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void getDepotServiceTrips(string stationId, string dsvc_id, string token)
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

            DataTable dtservicetrip = new DataTable();
            dtservicetrip = clscounter.GetDepotServiceTrips(dsvc_id, stationId);

            string code = "", msg = "";
            if (dtservicetrip.Rows.Count > 0)
            {
                code = "100"; msg = "";
            }
            else
            {
                code = "101"; msg = "Data Not Found";
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Trips", DataTableToJSON(dtservicetrip), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            exceptionMessage("Something went wrong. Please try again.");
        }
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void getLoadIdType(string token)
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

            DataTable dt = new DataTable();
            dt = clscounter.GetIdType();
            string code = "", msg = "";
            if (dt.Rows.Count > 0)
            {
                code = "100"; msg = "";
            }
            else
            {
                code = "101"; msg = "Data Not Found";
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "IDType", DataTableToJSON(dt), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            exceptionMessage("Something went wrong. Please try again.");
        }
    }
    #endregion

    #region"Ticket"
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void getTicket(string ticketno, string token)
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

            DataTable dt = new DataTable();
            dt = obj.loadTicket(ticketno);
            string code = "", msg = "";
            if (dt.Rows.Count > 0)
            {
                code = "100"; msg = "";
            }
            else
            {
                code = "101"; msg = "Data Not Found";
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Ticket", DataTableToJSON(dt), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            exceptionMessage("Something went wrong. Please try again.");
        }
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void InsertTicket(string waybillno, string servicetypeid, string busregistrationno, string conductorid, string conductorid2,
        string driver, string driver2, string departime, string routeid, string tripid, string from_station_code, string to_station_code, string trip_date, string received_amt, string refund_amt, string total_fare, string fare_per_seat, string booked_by_type_code, string booked_by_user_code, string booking_mode, string boarding_station_code, string total_seats_booked, string Passenger, string updatedby, string ip_imei, string ConcessionId, string OnlineVerificationYN, string CustpassNumber, string IdVerificationYN, string IdVerification, string DocumentVerificationYN, string DocumentVerification, string SpecialYn, string Book_sub_mode, string Payment_mode,string dsvc_id,string with_waybill, string token)
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

            if (OnlineVerificationYN == "Y")
            {
                if (CustpassNumber.Length < 2)
                {
                    invalidparameters("Enter Pass Number Provided by Department");
                    return;
                }
                else
                {
                    var result = trvlclass.CheckBusPass(ConcessionId, CustpassNumber.ToString().Trim(), trip_date.ToString());
                    if (result != "Success")
                    {
                        invalidparameters("Enter Valid Pass Number For concession ");
                        return;
                    }
                }
            }
            else if (IdVerificationYN == "Y")
            {
                if (CustpassNumber.Length < 2)
                {
                    invalidparameters("Enter the ID of any one of these documents ");
                    return;
                }
                else
                {
                    IdVerification = CustpassNumber.ToString();
                }

            }


            DataTable dt_ticket = new DataTable();
            dt_ticket = clscounter.SaveTicketWaybill(waybillno, Convert.ToInt64(servicetypeid), busregistrationno, conductorid, conductorid2,driver,driver2, departime, Convert.ToInt64(routeid)
                , Convert.ToInt64(tripid), Convert.ToInt64(from_station_code), Convert.ToInt64(to_station_code), trip_date,
               Convert.ToDecimal(received_amt), Convert.ToDecimal(refund_amt), Convert.ToDecimal(total_fare), Convert.ToDecimal(fare_per_seat),
               booked_by_type_code, booked_by_user_code, booking_mode, Convert.ToInt64(boarding_station_code), Convert.ToInt32(total_seats_booked)
               , Passenger, updatedby, ip_imei, ConcessionId, OnlineVerificationYN, CustpassNumber,
               IdVerificationYN, IdVerification, DocumentVerificationYN, DocumentVerification, SpecialYn, Book_sub_mode, Payment_mode,with_waybill);
            string code = "", msg = "";

            sbCommonFunc _common = new sbCommonFunc();
            _common.ErrorLog("wsCounterETM",dt_ticket.Rows.Count.ToString());

            if (dt_ticket.Rows.Count > 0)
            {
                code = "100"; msg = "";
            }
            else
            {
                code = "101"; msg = "Data Not Found";
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Result", DataTableToJSON(dt_ticket), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            exceptionMessage("Something went wrong. Please try again.");
        }
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void ConfirmCurrntTicket(string ticket_no, string received_amt, string refund_amt, string token)
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

            DataTable dt1 = new DataTable();
            dt1 = clscounter.ConfirmTicket(ticket_no, Convert.ToDecimal(received_amt), Convert.ToDecimal(refund_amt));
            string code = "", msg = "";
            if (dt1.Rows.Count > 0)
            {
                code = "100"; msg = "";
            }
            else
            {
                code = "101"; msg = "Data Not Found";
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "ConfirmTicket", DataTableToJSON(dt1), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            exceptionMessage("Something went wrong. Please try again.");
        }
    }
    [WebMethod()]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void getTktQuery(string booking_date, string journey_date, string search, string bookbytype, string token)
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

            DataTable dt_ = new DataTable();
            dt_ = obj.loadTktQuery(booking_date, journey_date, search, bookbytype);

            string code = "", msg = "";
            if (dt_.Rows.Count > 0)
            {
                code = "100"; msg = "";
            }
            else
            {
                code = "101"; msg = "Data Not Found";
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "TicketQuery", DataTableToJSON(dt_), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            exceptionMessage("Something went wrong. Please try again.");
        }
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void getTicketDetail(string ticketno, string token)
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

            wsClass obj = new wsClass();
            DataTable dt_TicketDetail = new DataTable();
            dt_TicketDetail = obj.getTicketDetails_byTicket(ticketno);
            string code = "", msg = "";
            if (dt_TicketDetail.Rows.Count > 0)
            {
                code = "100"; msg = "";
            }
            else
            {
                code = "101"; msg = "Data Not Found";
            }
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Ticket", DataTableToJSON(dt_TicketDetail), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            exceptionMessage("Something went wrong. Please try again.");
        }
    }
    #endregion

    #region"Trip Station"
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void getTripStations(string stationId, string dsvcid, string strpId, string conId, string token)
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

            DataTable dtstations = new DataTable();
            dtstations = clscounter.GetDepotServiceStations(strpId, stationId, Convert.ToInt64(dsvcid), conId);

            string code = "100", msg = "";

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Stations", DataTableToJSON(dtstations), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            exceptionMessage("Something went wrong. Please try again.");
        }
    }
    #endregion

    #region"Cash Register"
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void getOnlCashRegister(string Cntrid, string transdate, string token)
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
            if ((SoapHeader.IsUserCredentialsValid(SoapHeader) == false))
            {
                invalidTokenMessage();
                return;
            }

            dt = new DataTable();
            dt = obj.loadOnlCashtregister(Cntrid, transdate);
            string code = "";
            string msg = "";
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
            string jsonstr = finalJSON(code, "CashRegister", DataTableToJSON(dt), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            exceptionMessage("Something went wrong. Please try again.");
        }
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void getCrntCashRegister(string Cntrid, string transdate, string token)
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
            if ((SoapHeader.IsUserCredentialsValid(SoapHeader) == false))
            {
                invalidTokenMessage();
                return;
            }

            dt = new DataTable();
            dt = obj.getCrntCashRegister(Cntrid, transdate);
            string code = "";
            string msg = "";
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
            string jsonstr = finalJSON(code, "CashRegister", DataTableToJSON(dt), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            exceptionMessage("Something went wrong. Please try again.");
        }
    }
    #endregion

    #region "Special Cancellation"
    [WebMethod()]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void SpclCancelAvailableTicket(string ticketno, string token)
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

            DataTable dt_ = new DataTable();
            dt_ = trvlclass.getSplCancelledTicketsNew("0", ticketno, "C");

            DataTable dt_psngr = new DataTable();
            dt_psngr = trvlclass.getPassengerDetails(ticketno, "C");

            string code = "", msg = "";

            if (dt_psngr.Rows.Count > 0 && dt_.Rows.Count > 0)
            {
                code = "100"; msg = "";
            }
            else
            {
                code = "101"; msg = "Data Not Found";
            }


            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Ticket", DataTableToJSON(dt_), "Passengers", DataTableToJSON(dt_psngr), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            exceptionMessage("Something went wrong. Please try again.");
        }
    }
    [WebMethod()]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void SpclCancelRefund(string m_cancellationrefno, string ticketno, string m_Refundbytype, string m_Refundedby, string m_Updatedby, string token)
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

            DataTable dt_ = new DataTable();
            dt_ = trvlclass.refundtransactionnew(m_cancellationrefno, ticketno, m_Refundbytype, m_Refundedby, m_Updatedby);

            string code = "", msg = "";
            if (dt_.Rows.Count > 0)
            {
                code = "100"; msg = "";
            }
            else
            {
                code = "101"; msg = "Data Not Found";
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Ticket_Refund", DataTableToJSON(dt_), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            exceptionMessage("Something went wrong. Please try again.");
        }
    }
    #endregion

    #region"Ticket Cancellation"
    [WebMethod()]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void getAvailableTicket(string UserID, string ticketno, string book_by, string token)
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

            DataTable dt_ = new DataTable();
            dt_ = trvlclass.getAvailableTicket(UserID, ticketno, book_by);

            DataTable dt_psngr = new DataTable();
            dt_psngr = trvlclass.getPassengerDetails(ticketno, "A");

            string code = "100", msg = "";

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Ticket", DataTableToJSON(dt_), "Passenger", DataTableToJSON(dt_psngr), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            exceptionMessage("Something went wrong. Please try again.");
        }
    }
    [WebMethod()]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void CancelTicket(string TicketNo, string seatNo, string NoOfSeat, string CANCELLEDBY, string cancel_by_type, string token)
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

            DataTable dt_ = new DataTable();
            dt_ = trvlclass.cancelTicket(TicketNo, seatNo, "0", Convert.ToInt32(NoOfSeat), CANCELLEDBY, "", cancel_by_type, "E");

            string code = "", msg = "";
            if (dt_.Rows.Count > 0)
            {
                code = "100"; msg = "";
            }
            else
            {
                code = "101"; msg = "Data Not Found";
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Cancellation", DataTableToJSON(dt_), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            exceptionMessage("Something went wrong. Please try again.");
        }
    }
    [WebMethod()]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void RefundTicket(string m_cancellationrefno, string ticketno, string Refundbytype, string Refundedby, string Updatedby, string token)
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

            DataTable dt_ = new DataTable();
            dt_ = trvlclass.refundtransactionnew(m_cancellationrefno, ticketno, Refundbytype, Refundedby, Updatedby);

            string code = "", msg = "";
            if (dt_.Rows.Count > 0)
            {
                code = "100"; msg = "";
            }
            else
            {
                code = "101"; msg = "Data Not Found";
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Refund", DataTableToJSON(dt_), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            exceptionMessage("Something went wrong. Please try again.");
        }
    }
    [WebMethod()]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void CancellationVoucher(string cancellationrefno, string ticketno, string token)
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

            DataTable dt_ = new DataTable();
            dt_ = obj.get_cancel_tkt_details(cancellationrefno, ticketno);

            DataTable dt_seats = new DataTable();
            dt_seats = obj.get_cancel_tkt_seats(cancellationrefno, ticketno);

            string code = "100", msg = "";

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Ticket", DataTableToJSON(dt_), "Passenger", DataTableToJSON(dt_seats), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            exceptionMessage("Something went wrong. Please try again.");
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
            exceptionMessage("Something went wrong. Please try again.");
        }
    }
    #endregion

    #region"Waybill"
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void get_waybilldetails(string waybillno, string stationid, string counterid, string token)
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

            DataTable dt_waybill = new DataTable();
            DataTable dt_waybill_trips = new DataTable();
            dt_waybill = clscounter.GetWaybillDetails(waybillno);
            

            string dsvcid = "0";
            string code = "", msg = "";
            if (dt_waybill.Rows.Count > 0)
            {
                string istripchartopen = dt_waybill.Rows[0]["is_tripchart_open"].ToString();
                string tripbyuserid = dt_waybill.Rows[0]["tripbyuserid"].ToString();
                string tripbycntrname = dt_waybill.Rows[0]["tripcounter_name"].ToString();
                if (istripchartopen == "N")
                {
                    dsvcid = dt_waybill.Rows[0]["dsvc_id"].ToString();
                    dt_waybill_trips = clscounter.GetDepotServiceTrips(dsvcid, stationid);
                    if (dt_waybill.Rows.Count > 0 && dt_waybill_trips.Rows.Count > 0)
                    {
                        code = "100"; msg = "";
                    }
                    else
                    {
                        code = "101"; msg = "Trip Not Found";
                    }
                }
                else
                {
                    if (tripbyuserid == counterid)
                    {
                        code = "102"; msg = "This waybill under progress please close the trip & come again";
                    }
                    else
                    {
                        code = "102"; msg = "Trip on this waybill is open at " + tripbycntrname + ". Please close the trip chart and come again";
                    }
                }
            }
            else
            {
                code = "101"; msg = "Invalid Waybill Detail";
            }
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Waybill", DataTableToJSON(dt_waybill), "Trips", DataTableToJSON(dt_waybill_trips), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            exceptionMessage("Something went wrong. Please try again.");
        }
    }
    #endregion  

    #region "Sms Email"
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void send_ticket_confirmation(string ticketNo, string token)
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

            CommonSMSnEmail obj = new CommonSMSnEmail();
            DataTable dt_rslt = new DataTable();
            obj.sendTicketConfirm_SMSnEMAIL(ticketNo, "B", "2");

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
            exceptionMessage("Something went wrong. Please try again.");
        }
    }
    #endregion

    #region "Error Log"
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void Error_log(string activityname, string Errordesc, string token)
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
            sbCommonFunc _common = new sbCommonFunc();
            _common.ErrorLog(activityname, Errordesc);

            DataTable dt_Error = new DataTable("dtError");
            dt_Error.Columns.Add("Error", typeof(string));
            dt_Error.Rows.Add(activityname);

            string code = "";
            string msg = "";

            code = "100";
            msg = "";

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Error", DataTableToJSON(dt_Error), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            exceptionMessage("Something went wrong. Please try again.");
        }
    }
    #endregion

    #region "Remove Token"
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public string removeToken( string token)
    {
        try
        {
            SoapHeader.UserAuthenticationToken = token;
            if (SoapHeader == null)
            {
                return "1";
            }

            // check user Credential valid
            if (!(SoapHeader.IsUserCredentialsValid(SoapHeader)))
            {
                return "1";
            }
            HttpRuntime.Cache.Remove(token);
            return "Y";
        }
        catch (Exception ex)
        {
            return "0";
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
    public string finalJSON(string code, string data1Name, string data1, string data2Name, string data2, string data3Name, string data3, string data4Name, string data4, string data5Name, string data5, string data6Name, string data6, string data7Name, string data7, string errorMessage)
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
        final = final + "\"" + data7Name + "\":" + data7 + ",";
        final = final + "\"msg\":\"" + errorMessage + "\"";
        final = final + "}";
        return final;
    }
    public string finalJSON(string code, string data1Name, string data1, string data2Name, string data2, string data3Name, string data3, string data4Name, string data4, string data5Name, string data5, string data6Name, string data6, string data7Name, string data7, string data8Name, string data8, string errorMessage)
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
        final = final + "\"" + data7Name + "\":" + data7 + ",";
        final = final + "\"" + data8Name + "\":" + data8 + ",";
        final = final + "\"msg\":\"" + errorMessage + "\"";
        final = final + "}";
        return final;
    }
    public string finalJSON(string code, string data1Name, string data1, string data2Name, string data2, string data3Name, string data3, string data4Name, string data4, string data5Name, string data5, string data6Name, string data6, string data7Name, string data7, string data8Name, string data8, string data9Name, string data9, string errorMessage)
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
        final = final + "\"" + data7Name + "\":" + data7 + ",";
        final = final + "\"" + data8Name + "\":" + data8 + ",";
        final = final + "\"" + data9Name + "\":" + data9 + ",";
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
    private void invalidDeviceMessage()
    {
        DataTable dttkn = new DataTable();
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        string jsonstr = finalJSON("800", "Result", DataTableToJSON(dttkn), "This Device is not registered.<br>Please contact helpdesk");
        Context.Response.Write(jsonstr);
    }
    private void exceptionMessage(string msg)
    {
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        string jsonstr = finalJSON("900", "Result", DataTableToJSON(dtnothing), msg);
        Context.Response.Write(jsonstr);
    }

    #endregion

}
