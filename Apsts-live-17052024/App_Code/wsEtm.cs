using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

/// <summary>
/// Summary description for wsEtm
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class wsEtm : System.Web.Services.WebService
{
    sbValidation _validation = new sbValidation();
    public ClassTokenETM SoapHeader = new ClassTokenETM();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    public wsClass wsclass = new wsClass();

    private sbBLL bll = new sbBLL();
    CommonSMSnEmail sms = new CommonSMSnEmail();
    public wsEtm()
    {

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

            if(dt_token.Rows.Count > 0)
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
    public void isActiveApp(string deviceid, string latitude, string longitude, string token)
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
            ClassETM obj = new ClassETM();
            DataTable dtlog = new DataTable();
            dtlog = obj.updateActionlog(57, 8, "ETM", "", deviceid, latitude, longitude, "E");

            dt = new DataTable();
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
                msg = "";
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

    #region"Check ETM"
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void checkETM(string etmVersion, string IMEINo, string etmOfficeId, string routeLastUpdate, string routeStationLastUpdate, string fareStationLastUpdate, string concessionLastUpdate, string deviceid, string latitude, string longitude, string token)
    {
        try
        {


            DataTable dt_output = new DataTable("dtOutput");
            dt_output.Columns.Add("appActiveYN", typeof(string));
            dt_output.Columns.Add("versionSameYN", typeof(string));
            dt_output.Columns.Add("ETMRegistrationYN", typeof(string));
            dt_output.Columns.Add("officeId", typeof(string));
            dt_output.Columns.Add("officeIdSameYN", typeof(string));
            dt_output.Columns.Add("routeUpdatedYN", typeof(string));
            dt_output.Columns.Add("routeStationUpdatedYN", typeof(string));
            dt_output.Columns.Add("fareStationUpdatedYN", typeof(string));
            dt_output.Columns.Add("concessionUpdatedYN", typeof(string));
            var appActiveYN = "N";
            var versionSameYN = "N";
            var ETMRegistrationYN = "N";
            var officeId = "";
            var officeIdSameYN = "N";
            var routeUpdatedYN = "N";
            var routeStationUpdatedYN = "N";
            var fareStationUpdatedYN = "N";
            var concessionUpdatedYN = "N";

            if (token.ToUpper() != "C7B21607E40B96E142889B56354089DA92A12A34")
            {
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                string jsonstrr = finalJSON("999", "Result", DataTableToJSON(dt), "Invalid Token.");
                Context.Response.Write(jsonstrr);
                return;
            }

            ClassETM obj = new ClassETM();
            DataTable dt_reg = new DataTable();
            dt_reg = obj.isDeviceRegistered(IMEINo);

            if (dt_reg.Rows.Count > 0)
            {
                ETMRegistrationYN = "Y";
                if (etmOfficeId.Length > 0)
                {
                    if (etmOfficeId == dt_reg.Rows[0]["depotid"].ToString())
                    {
                        officeIdSameYN = "Y";
                        officeId = etmOfficeId;
                    }
                    else
                        officeId = dt_reg.Rows[0]["depotid"].ToString();
                }
                else
                    officeId = dt_reg.Rows[0]["depotid"].ToString();
            }

            DataTable dt_app = new DataTable();
            dt_app = obj.isappactive();
            if (dt_app.Rows.Count > 0)
            {
                appActiveYN = "Y";
                string versionCode = dt_app.Rows[0]["version_name"].ToString();
                if (etmVersion == versionCode)
                    versionSameYN = "Y";
            }

            DataTable dt_update = new DataTable();
            dt_update = obj.checkLastUpdate(officeId);
            if (dt_update.Rows.Count > 0)
            {
                string routeLastDate = dt_update.Rows[0]["routelastupdate"].ToString();
                string routeStationLastDate = dt_update.Rows[0]["routeStationLastDate"].ToString();
                string concessionLastDate = dt_update.Rows[0]["concessionLastDate"].ToString();
                string fareStationLastDate = dt_update.Rows[0]["fareStationLastDate"].ToString();

                if (routeLastUpdate == routeLastDate)
                    routeUpdatedYN = "Y";
                if (routeStationLastUpdate == routeStationLastDate)
                    routeStationUpdatedYN = "Y";
                if (fareStationLastUpdate == fareStationLastDate)
                    fareStationUpdatedYN = "Y";
                if (concessionLastUpdate == concessionLastDate)
                    concessionUpdatedYN = "Y";
            }
            dt_output.Rows.Add(appActiveYN, versionSameYN, ETMRegistrationYN, officeId, officeIdSameYN, routeUpdatedYN, routeStationUpdatedYN, fareStationUpdatedYN, concessionUpdatedYN);

            DataTable dtlog = new DataTable();
            dtlog = obj.updateActionlog(57, 9, "ETM", "", deviceid, latitude, longitude, "E");

            string code = "";
            string msg = "";
            if (dt_output.Rows.Count > 0)
            {
                code = "100";
                msg = "";
            }
            else
            {
                code = "101";
                msg = "Invalid Token";
            }
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Result", DataTableToJSON(dt_output), msg);
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

[WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void isETMDeactivate(string imieno)
    {
        try
        {
            dt = new DataTable();
            ClassETM obj = new ClassETM();
            dt = obj.checketmstatus(imieno);

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
            string jsonstr = finalJSON(code, "ETM", DataTableToJSON(dt), msg);
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

    #region"Update Master"
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void get_route(string officeId, string deviceid, string latitude, string longitude, string token)
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
            ClassETM obj = new ClassETM();
            dt = obj.getRoute(officeId, deviceid, latitude, longitude, "E");

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
            string jsonstr = finalJSON(code, "Route", DataTableToJSON(dt), msg);
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
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void get_routeStations(string officeId, string deviceid, string latitude, string longitude, string token)
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
            ClassETM obj = new ClassETM();
            dt = obj.getRouteStations(officeId, deviceid, latitude, longitude, "E");

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
                msg = "Invalid Token";
            }
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "RouteStations", DataTableToJSON(dt), msg);
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
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void get_fareStations(string officeId, string deviceid,string srtp_id , string latitude, string longitude, string token)
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
            ClassETM obj = new ClassETM();
            dt = obj.getFareStations(officeId, deviceid,srtp_id, latitude, longitude, "E");

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
            string jsonstr = finalJSON(code, "FareStations", DataTableToJSON(dt), msg);
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
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void get_concession(string deviceid, string latitude, string longitude, string token)
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
            ClassETM obj = new ClassETM();
            dt = obj.getConcessions(deviceid, latitude, longitude, "E");

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
                msg = "Invalid Token";
            }
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Concessions", DataTableToJSON(dt), msg);
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

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void get_qr_pg(string token)
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
            ClassETM obj = new ClassETM();
            dt = obj.getQRpg();

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
                msg = "Invalid Token";
            }
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "PG", DataTableToJSON(dt), msg);
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

  [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void get_luggageFare(string deviceid, string latitude, string longitude, string token)
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
            dt.Columns.Add("fare", typeof(double));
            dt.Columns.Add("perkg", typeof(double));
            dt.Columns.Add("perkm", typeof(double));
            dt.Rows.Add(1.50, 1,1);

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
                msg = "Invalid Token";
            }
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Fare", DataTableToJSON(dt), msg);
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

    #region"Login"
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void operatorLogin(string userId, string userPwd, string imei, string officeId, string latitude, string longitude, string token)
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
            ClassETM obj = new ClassETM();
            dt = obj.userdetail(userId);

            string code = "";
            string msg = "";
            if (dt.Rows.Count > 0)
            {
                if (checkpassword(dt.Rows[0]["upwd"].ToString(), userPwd) == true)
                {
                    wsclass.UpdateLoginLog(userId, "", "E", imei, latitude, longitude);
                    if (Convert.ToInt16(dt.Rows[0]["role_code"].ToString().Trim()) == 491)
                    {
                        if (dt.Rows[0]["depot_id"].ToString().Trim() == officeId)
                        {
                            code = "100";
                            msg = "";
                        }
                        else
                        {
                            code = "101";
                            msg = "Invalid Office";
                        }
                    }
                    else
                    {
                        code = "101";
                        msg = "Invalid Role";
                    }
                }
                else
                {
                    wsclass.UpdateUnAuthLoginLog(userId, "", "E", imei, latitude, longitude);
                    code = "101";
                    msg = "Invalid Login Credentials";
                }
            }
            else
            {
                wsclass.UpdateUnAuthLoginLog(userId, "", "E", imei, latitude, longitude);
                code = "101";
                msg = "Invalid Login Credentials";
            }
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Result", DataTableToJSON(dt), msg);
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
    private bool checkpassword(string passworddatabase, string MuserPassword)
    {
        return (0 == string.Compare(passworddatabase.ToUpper(), MuserPassword.ToUpper(), false));
    }
    #endregion

    #region"login Logs"
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void upload_loginlog(string json, string token)
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

            DataTable dt1 = new DataTable();
            dt1 = JSONToDataTable(json);

            ClassETM obj = new ClassETM();
            dt = obj.uploadloginlog(dt1);

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
                msg = "Data Not Uploaded";
            }
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "LoginLog", DataTableToJSON(dt), msg);
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

    #region"Waybill"
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void get_waybillDeatils(string waybillNo, string deviceid, string latitude, string longitude, string token, string depotid)
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
            ClassETM obj = new ClassETM();
            DataTable dt_waybill = new DataTable();
            DataTable dt_Conductor1 = new DataTable();
            DataTable dt_Conductor2 = new DataTable();
            DataTable dt_Driver1 = new DataTable();
            DataTable dt_Driver2 = new DataTable();
            DataTable dt_TIs = new DataTable();
            DataTable dt_Trips = new DataTable();
	    DataTable dt_DepotServiceRouteStation = new DataTable();
            DataTable dt_DepotServiceOnlineStation = new DataTable();

            dt_waybill = obj.getWaybilDetails(waybillNo, deviceid, latitude, longitude, "E", depotid);

            string code = "";
            string msg = "";
            if (dt_waybill.Rows.Count > 0)
            {
                code = "100";
                msg = "";

                string cond1 = dt_waybill.Rows[0]["conductor1emp_code"].ToString();
                string cond2 = dt_waybill.Rows[0]["conductor2emp_code"].ToString();
                string driver1 = dt_waybill.Rows[0]["driver1emp_code"].ToString();
                string driver2 = dt_waybill.Rows[0]["driver2emp_code"].ToString();
                string dsvcId = dt_waybill.Rows[0]["service_id"].ToString();
                dt_Conductor1 = obj.userdetail(cond1);
                dt_Conductor2 = obj.userdetail(cond2);
                dt_Driver1 = obj.userdetail(driver1);
                dt_Driver2 = obj.userdetail(driver2);
                dt_TIs = obj.getTIs();
                dt_Trips = obj.getWaybillTrips(dsvcId);
dt_DepotServiceRouteStation = obj.getdepotservicestation(dsvcId);
                dt_DepotServiceOnlineStation = obj.getdepotserviceOnlineStation(dsvcId);
            }
            else
            {
                code = "101";
                msg = "waybill details are not available";
            }
             Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Waybill", DataTableToJSON(dt_waybill), "Conductor1", DataTableToJSON(dt_Conductor1),
            "Conductor2", DataTableToJSON(dt_Conductor2), "Driver1", DataTableToJSON(dt_Driver1), "Driver2", DataTableToJSON(dt_Driver2),
            "TIS", DataTableToJSON(dt_TIs), "Trips", DataTableToJSON(dt_Trips), "DepotServiceRouteStation", DataTableToJSON(dt_DepotServiceRouteStation),
            "DepotServiceOnlineStation", DataTableToJSON(dt_DepotServiceOnlineStation), msg);
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
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void assign_waybill(string assignby, string dutyrefno, string deviceid, string latitude, string longitude, string token)
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

            string denominationBook = "N";
            int ManualTicketStart = 0;
            int ManualTicketEnd = 0;
            int Deno100Start = 0;
            int Deno100End = 0;
            int Deno100Total = 0;
            int Deno50Total = 0;
            int Deno50Start = 0;
            int Deno50End = 0;
            int Deno20Total = 0;
            int Deno20Start = 0;
            int Deno20End = 0;
            int Deno10Start = 0;
            int Deno10End = 0;
            int Deno10Total = 0;
            int Deno5Total = 0;
            int Deno5Start = 0;
            int Deno5End = 0;
            int Deno2Total = 0;
            int Deno2Start = 0;
            int Deno2End = 0;
            int Deno1Start = 0;
            int Deno1End = 0;
            int Deno1Total = 0;

            ClassETM obj = new ClassETM();
            DataTable dt_reg = new DataTable();
            dt_reg = obj.isDeviceRegistered(deviceid);
            string etmrefno = dt_reg.Rows[0]["etm_id"].ToString();

            dt = new DataTable();
            dt = obj.assignWaybill(dutyrefno, etmrefno, ManualTicketStart, ManualTicketEnd, denominationBook, Deno100Start, Deno100End, Deno100Total,
                Deno50Start, Deno50End, Deno50Total, Deno20Start, Deno20End, Deno20Total, Deno10Start, Deno10End, Deno10Total,
                Deno5Start, Deno5End, Deno5Total, Deno2Start, Deno2End, Deno2Total, Deno1Start, Deno1End, Deno1Total, assignby, "", latitude,
                longitude, "E", deviceid);

            string code = "";
            string msg = "";
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["sprefno"].ToString() == "Y")
                {
                    code = "100";
                    msg = "";
                }
                 else if (dt.Rows[0]["sprefno"].ToString() == "Already")
                {
                    code = "101";
                    msg = "Waybill Already Generated For This Dutyslip";
                }
                else if(dt.Rows[0]["sprefno"].ToString() == "N" && dt.Rows[0]["etm_free"].ToString() == "N")
                {
                    code = "101";
                    msg = "This ETM Is Already Assigned To Other Waybill.";
                }
                else
                {
                    code = "101";
                    msg = "Waybill Not Assigned";
                }
            }
            else
            {
                code = "101";
                msg = "Something Went Wrong";
            }
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Result", DataTableToJSON(dt), msg);
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
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void cancel_waybill(string cancelby, string dutyrefno, string deviceid, string latitude, string longitude, string token)
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

            ClassETM obj = new ClassETM();
            dt = new DataTable();

            dt = obj.cancelWaybill(dutyrefno, deviceid, latitude, longitude, "E");
            string code = "";
            string msg = "";
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["p_result"].ToString() == "Y")
                {
                    code = "100";
                    msg = "";
                }
                else if (dt.Rows[0]["p_result"].ToString() == "A")
                {
                    code = "101";
                    msg = "Waybill Not Cancelled, Trip Already Started";
                }
                else
                {
                    code = "101";
                    msg = "Waybill Not Cancelled";
                }
            }
            else
            {
                code = "101";
                msg = "Something Went Wrong";
            }
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Result", DataTableToJSON(dt), msg);
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

    #region "Expenses And Extra Earning"
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void get_expenses_earnings(string deviceid, string latitude, string longitude, string token)
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
            ClassETM obj = new ClassETM();
            dt = obj.getExpensesExtraEarning(deviceid, latitude, longitude, "E");

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
            string jsonstr = finalJSON(code, "ExpensesEarnings", DataTableToJSON(dt), msg);
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

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void upload_expenses_earnings(string json, string token)
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

            DataTable dt1 = new DataTable();
            dt1 = JSONToDataTable(json);

            ClassETM obj = new ClassETM();
            dt = obj.uploadExpensesExtraEarning(dt1);

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
            string jsonstr = finalJSON(code, "ExpensesEarnings", DataTableToJSON(dt), msg);
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

    #region"Trips"
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void trip_start(string json, string token)
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
            string code = "";
            string msg = "";

            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();

            JsonStringToDatabase js = new JsonStringToDatabase();
            dt1 = js.getDataSet(json);

            if (dt1.Rows.Count > 0)
            {
                string waybill_no = dt1.Rows[0]["value1"].ToString();
                string trip_no = dt1.Rows[0]["value2"].ToString();
                string tripstartdetail = dt1.Rows[0]["json1"].ToString();

                dt1 = new DataTable();
                if (tripstartdetail != "")
                {
                    dt1 = JSONToDataTable(tripstartdetail);
                }


                ClassETM obj = new ClassETM();
                if (dt1.Rows.Count > 0)
                {
                    dt = obj.uploadTrips(waybill_no, trip_no, dt1);
                }

                if (dt.Rows.Count > 0)
                {
                    code = "100";
                    msg = "";
                }
                else
                {
                    code = "101";
                    msg = "Data Not Uploaded";
                }
            }
            else
            {
                code = "101";
                msg = "No Data Found";
            }
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Trip", DataTableToJSON(dt), msg);
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

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void trip_complete(string json, string token)
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
            string code = "";
            string msg = "";
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            JsonStringToDatabase js = new JsonStringToDatabase();
            dt1 = js.getDataSet(json);

            if (dt1.Rows.Count > 0)
            {
                string waybill_no = dt1.Rows[0]["value1"].ToString();
                string trip_no = dt1.Rows[0]["value2"].ToString();
                string tripstartdetail = dt1.Rows[0]["json1"].ToString();
                string tripcompletedetail = dt1.Rows[0]["json2"].ToString();

                dt1 = new DataTable();
                if (tripstartdetail != "")
                {
                    dt1 = JSONToDataTable(tripstartdetail);
                }
                

                dt2 = new DataTable();
                if (tripcompletedetail != "")
                {
                    dt2 = JSONToDataTable(tripcompletedetail);
                }
               

                ClassETM obj = new ClassETM();
                if (dt1.Rows.Count > 0)
                {
                    dt1 = obj.uploadTrips(waybill_no, trip_no, dt1);
                }
                if (dt2.Rows.Count > 0)
                {
                    dt = obj.uploadTrips(waybill_no, trip_no, dt2);
                }



                if (dt.Rows.Count > 0)
                {
                    code = "100";
                    msg = "";
                }
                else
                {
                    code = "101";
                    msg = "Data Not Uploaded";
                }
            }
            else
            {
                code = "101";
                msg = "No Data Found";
            }
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Trip", DataTableToJSON(dt), msg);
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

    #region"Stage Changes"
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void upload_stageChanges(string json, string token)
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
            string code = "";
            string msg = "";
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            JsonStringToDatabase js = new JsonStringToDatabase();
            dt1 = js.getDataSet(json);

            if (dt1.Rows.Count > 0)
            {
                string waybill_no = dt1.Rows[0]["value1"].ToString();
                string trip_no = dt1.Rows[0]["value2"].ToString();
                string tripstartdetail = dt1.Rows[0]["json1"].ToString();
                string stagechangedetail = dt1.Rows[0]["json2"].ToString();

                dt1 = new DataTable();
                if (tripstartdetail != "")
                {
                    dt1 = JSONToDataTable(tripstartdetail);
                }
                

                dt2 = new DataTable();
                if (stagechangedetail != "")
                {
                    dt2 = JSONToDataTable(stagechangedetail);
                }
                

                ClassETM obj = new ClassETM();
                if (dt1.Rows.Count > 0)
                {
                    dt1 = obj.uploadTrips(waybill_no, trip_no, dt1);
                }
                if (dt2.Rows.Count > 0)
                {
                    dt = obj.uploadStageChanges(waybill_no, trip_no, dt2);
                }


                if (dt.Rows.Count > 0)
                {
                    code = "100";
                    msg = "";
                }
                else
                {
                    code = "101";
                    msg = "Data Not Uploaded";
                }
            }
            else
            {
                code = "101";
                msg = "No Data Found";
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "StageChanges", DataTableToJSON(dt), "Trip", DataTableToJSON(dt1), msg);
            Context.Response.Write(jsonstr);
        }
        catch (Exception ex)
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON("900", "Stage", DataTableToJSON(dt), "Something went wrong. Please try again. " + ex.ToString());
            Context.Response.Write(jsonstr);
        }
    }
    #endregion

    #region"Tickets"
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void upload_tickets(string json, string token)
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

            string code = "";
            string msg = "";
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            JsonStringToDatabase js = new JsonStringToDatabase();
            dt1 = js.getDataSet(json);

            if (dt1.Rows.Count > 0)
            {
                string waybill_no = dt1.Rows[0]["value1"].ToString();
                string trip_no = dt1.Rows[0]["value2"].ToString();
                string tripstartdetail = dt1.Rows[0]["json1"].ToString();
                string ticketdetail = dt1.Rows[0]["json2"].ToString();

                dt1 = new DataTable();
                if (tripstartdetail != "")
                {
                    dt1 = JSONToDataTable(tripstartdetail);
                }
                

                dt2 = new DataTable();
                if (ticketdetail != "")
                {
                    dt2 = JSONToDataTable(ticketdetail);
                }
                

                ClassETM obj = new ClassETM();

                if (dt1.Rows.Count > 0)
                {
                    dt1 = obj.uploadTrips(waybill_no, trip_no, dt1);
                }
                if (dt2.Rows.Count > 0)
                {
                    dt = obj.uploadTicket(waybill_no, trip_no, dt2);
                }

                if (dt.Rows.Count > 0)
                {
                    code = "100";
                    msg = "";
                }
                else
                {
                    code = "101";
                    msg = "Data Not Uploaded";
                }
            }
            else
            {
                code = "101";
                msg = "No Data Uploaded";
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Ticket", DataTableToJSON(dt),"Trip", DataTableToJSON(dt1), msg);
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

    #region"Inspection"
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void inspection_start(string json, string token)
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
            string code = "";
            string msg = "";
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            JsonStringToDatabase js = new JsonStringToDatabase();
            dt1 = js.getDataSet(json);

            if (dt1.Rows.Count > 0)
            {
                string waybill_no = dt1.Rows[0]["value1"].ToString();
                string inspectionid = dt1.Rows[0]["value2"].ToString();
                string inspectionstartdetail = dt1.Rows[0]["json1"].ToString();

                dt1 = new DataTable();
                if (inspectionstartdetail != "")
                {
                    dt1 = JSONToDataTable(inspectionstartdetail);
                }

                ClassETM obj = new ClassETM();
                if (dt1.Rows.Count > 0)
                {
                    dt = obj.uploadInspection(waybill_no, inspectionid, dt1);
                }

                if (dt.Rows.Count > 0)
                {
                    code = "100";
                    msg = "";
                }
                else
                {
                    code = "101";
                    msg = "Data Not Uploaded";
                }
            }
            else
            {
                code = "101";
                msg = "No Data Found";
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Inspection", DataTableToJSON(dt), msg);
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

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void inspection_complete(string json, string token)
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
            string code = "";
            string msg = "";
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            JsonStringToDatabase js = new JsonStringToDatabase();
            dt1 = js.getDataSet(json);

            if (dt1.Rows.Count > 0)
            {
                string waybill_no = dt1.Rows[0]["value1"].ToString();
                string inspectionid = dt1.Rows[0]["value2"].ToString();
                string inspectionstartdetail = dt1.Rows[0]["json1"].ToString();
                string inspectioncompletedetail = dt1.Rows[0]["json2"].ToString();

                dt1 = new DataTable();
                if (inspectionstartdetail != "")
                {
                    dt1 = JSONToDataTable(inspectionstartdetail);
                }
                

                dt2 = new DataTable();
                if (inspectioncompletedetail != "")
                {
                    dt2 = JSONToDataTable(inspectioncompletedetail);
                }
                

                ClassETM obj = new ClassETM();
                if (dt1.Rows.Count > 0)
                {
                    dt = obj.uploadInspection(waybill_no, inspectionid, dt1);
                }
                if (dt2.Rows.Count > 0)
                {
                    dt = obj.uploadInspection(waybill_no, inspectionid, dt2);
                }

                if (dt.Rows.Count > 0)
                {
                    code = "100";
                    msg = "";
                }
                else
                {
                    code = "101";
                    msg = "Data Not Uploaded";
                }
            }
            else
            {
                code = "101";
                msg = "No Data Found";
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Inspection", DataTableToJSON(dt), msg);
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

    #region"Scan Online Tickets"
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void upload_ScanOnlTicket(string json, string token)
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

            string code = "";
            string msg = "";
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            JsonStringToDatabase js = new JsonStringToDatabase();
            dt1 = js.getDataSet(json);

            if (dt1.Rows.Count > 0)
            {
                string waybill_no = dt1.Rows[0]["value1"].ToString();
                string trip_no = dt1.Rows[0]["value2"].ToString();
                string tripstartdetail = dt1.Rows[0]["json1"].ToString();
                string onlineticketdetail = dt1.Rows[0]["json2"].ToString();

                dt1 = new DataTable();
                if (tripstartdetail != "")
                {
                    dt1 = JSONToDataTable(tripstartdetail);
                }


                dt2 = new DataTable();
                if (onlineticketdetail != "")
                {
                    dt2 = JSONToDataTable(onlineticketdetail);
                }


                ClassETM obj = new ClassETM();

                if (dt1.Rows.Count > 0)
                {
                    dt = obj.uploadTrips(waybill_no, trip_no, dt1);
                }
                if (dt2.Rows.Count > 0)
                {
                    dt = obj.uploadScanOnlineTicket(waybill_no, trip_no, dt2);
                }

                if (dt.Rows.Count > 0)
                {
                    code = "100";
                    msg = "";
                }
                else
                {
                    code = "101";
                    msg = "Data Not Uploaded";
                }
            }
            else
            {
                code = "101";
                msg = "No Data Uploaded";
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "OnlineTicket", DataTableToJSON(dt), "Trip", DataTableToJSON(dt1), msg);
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

    #region"Refunded Tickets"
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void upload_refundedTickets(string json, string token)
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

            string code = "";
            string msg = "";
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            JsonStringToDatabase js = new JsonStringToDatabase();
            dt1 = js.getDataSet(json);

            if (dt1.Rows.Count > 0)
            {
                string waybill_no = dt1.Rows[0]["value1"].ToString();
                string trip_no = dt1.Rows[0]["value2"].ToString();
                string tripstartdetail = dt1.Rows[0]["json1"].ToString();
                string refundticketdetail = dt1.Rows[0]["json2"].ToString();

                dt1 = new DataTable();
                if (tripstartdetail != "")
                {
                    dt1 = JSONToDataTable(tripstartdetail);
                }


                dt2 = new DataTable();
                if (refundticketdetail != "")
                {
                    dt2 = JSONToDataTable(refundticketdetail);
                }


                ClassETM obj = new ClassETM();

                if (dt1.Rows.Count > 0)
                {
                    dt1 = obj.uploadTrips(waybill_no, trip_no, dt1);
                }
                if (dt2.Rows.Count > 0)
                {
                    dt = obj.uploadRefundedTickets(waybill_no, trip_no, dt2);
                }

                if (dt.Rows.Count > 0)
                {
                    code = "100";
                    msg = "";
                }
                else
                {
                    code = "101";
                    msg = "Data Not Uploaded";
                }
            }
            else
            {
                code = "101";
                msg = "No Data Uploaded";
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "Ticket", DataTableToJSON(dt),"Trip", DataTableToJSON(dt1), msg);
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

    #region"Online Tickets"
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void getOnlineTickets(string waybillno,string journeydate,string strp_id,string dsvc_id,string downloadedby, string deviceid, string latitude, string longitude, string token)
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

            ClassETM obj = new ClassETM();
            dt = obj.onlineTickets(journeydate,strp_id,dsvc_id,waybillno,downloadedby,deviceid,latitude,longitude,"ETM");

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
                msg = "Data Not Uploaded";
            }
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "OnlineTickets", DataTableToJSON(dt), msg);
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

    #region "Action Log"
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void get_actionlogMaster(string deviceid, string latitude, string longitude, string token)
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
            ClassETM obj = new ClassETM();
            dt = obj.getActionLogMaster(deviceid, latitude, longitude, "E");

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
            string jsonstr = finalJSON(code, "Actions", DataTableToJSON(dt), msg);
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

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void upload_actionlog(string json, string token)
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
            string code = "";
            string msg = "";

            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();

            JsonStringToDatabase js = new JsonStringToDatabase();
            dt1 = js.getDataSet(json);

            if (dt1.Rows.Count > 0)
            {
                string waybill_no = dt1.Rows[0]["value1"].ToString();
                string trip_no = dt1.Rows[0]["value2"].ToString();
                string actionlogdetail = dt1.Rows[0]["json1"].ToString();

                dt1 = new DataTable();
                if (actionlogdetail != "")
                {
                    dt1 = JSONToDataTable(actionlogdetail);
                }


                ClassETM obj = new ClassETM();
                if (dt1.Rows.Count > 0)
                {
                    dt = obj.uploadActionlog( dt1);
                }

                if (dt.Rows.Count > 0)
                {
                    code = "100";
                    msg = "";
                }
                else
                {
                    code = "101";
                    msg = "Data Not Uploaded";
                }
            }
            else
            {
                code = "101";
                msg = "No Data Found";
            }
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string jsonstr = finalJSON(code, "ActionLog", DataTableToJSON(dt), msg);
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

    #region"is Data Uploaded"
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void isDataUploaded(string waybill_no, int tickets, int refundTickets, int onlineTickets, int inspection, int stage, int trip, int exp_ear, int actionlogs, string token)
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
            ClassETM obj = new ClassETM();

            dt = new DataTable();
            dt = obj.getuploaddatacount(waybill_no);

            int ticketcount = Convert.ToInt32(dt.Rows[0]["ticket_count"].ToString());
            int refundticketcount = Convert.ToInt32(dt.Rows[0]["refundticket_count"].ToString());
            int onlineticketcount = Convert.ToInt32(dt.Rows[0]["onlineticket_count"].ToString());
            int tripcount = Convert.ToInt32(dt.Rows[0]["trip_count"].ToString());
            int inspectioncount = Convert.ToInt32(dt.Rows[0]["inspection_count"].ToString());
            int stagechangecount = Convert.ToInt32(dt.Rows[0]["stagechange_count"].ToString());
            int expense_earningcount = Convert.ToInt32(dt.Rows[0]["exp_ear_count"].ToString());
            int actionlogscount = Convert.ToInt32(dt.Rows[0]["actionlog_count"].ToString());

            string ticketcountyn = "N", refundticketcountyn = "N", onlineticketcountyn = "N", tripcountyn = "N",
                inspectioncountyn = "N", stagechangecountyn = "N", expense_earningcountyn = "N", actionlogscountyn = "N";

            if (ticketcount == tickets)
                ticketcountyn = "Y";
            if (refundticketcount == refundTickets)
                refundticketcountyn = "Y";
            if (onlineticketcount == onlineTickets)
                onlineticketcountyn = "Y";
            if (tripcount == trip)
                tripcountyn = "Y";
            if (stagechangecount == stage)
                stagechangecountyn = "Y";
            if (inspectioncount == inspection)
                inspectioncountyn = "Y";
            if (expense_earningcount == exp_ear)
                expense_earningcountyn = "Y";
            if (actionlogscount == actionlogs)
                actionlogscountyn = "Y";

            sbXMLdata objXML = new sbXMLdata();
            DataTable dt_count = new DataTable("dt_count");
            dt_count.Columns.Add("ticket", typeof(string));
            dt_count.Columns.Add("refundticket", typeof(string));
            dt_count.Columns.Add("onlineticket", typeof(string));
            dt_count.Columns.Add("trip", typeof(string));
            dt_count.Columns.Add("inspection", typeof(string));
            dt_count.Columns.Add("stagechange", typeof(string));
            dt_count.Columns.Add("expense_earning", typeof(string));
            dt_count.Columns.Add("actionlogs", typeof(string));

            dt_count.Rows.Add(ticketcountyn, refundticketcountyn, onlineticketcountyn, tripcountyn, inspectioncountyn, stagechangecountyn, expense_earningcountyn, actionlogscountyn);
            
            string code = "";
            string msg = "";
            if (dt_count.Rows.Count > 0)
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
            string jsonstr = finalJSON(code, "Counting", DataTableToJSON(dt_count),"Counts",DataTableToJSON(dt), msg);
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

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void allDataUpload(string waybillno,string deviceid, string latitude, string longitude, string token)
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
            ClassETM obj = new ClassETM();
            dt = obj.allDataUpload(waybillno);

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
            string jsonstr = finalJSON(code, "Actions", DataTableToJSON(dt), msg);
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
	[WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void checkWaybillDataUpload(string waybillno,  string token)
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
            ClassETM obj = new ClassETM();
            dt = obj.checkWaybillDataUpload(waybillno);

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
            string jsonstr = finalJSON(code, "Waybill", DataTableToJSON(dt), msg);
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
#region"ETM Free"
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void freeETM(string imieno, string token)
    {
        try
        {
            if (token.ToUpper() != "TOKENCHANGE12345QWERTY")
            {
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                string jsonstrr = finalJSON("999", "Result", DataTableToJSON(dt), "Invalid Token.");
                Context.Response.Write(jsonstrr);
                return;
            }

            dt = new DataTable();
            ClassETM obj = new ClassETM();
            dt = obj.etmfree(imieno);

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
            string jsonstr = finalJSON(code, "ETM", DataTableToJSON(dt), msg);
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
#region"ETM DB File Upload"
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void upload_dbfile(string token, string data, string imei, string lat, string longi, string waybill, string updatedby, string filename, string contenttype)
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
            ClassETM obj = new ClassETM();
            dt = obj.uploadetmdbfile(data, imei,  lat, longi,  waybill,  filename,  contenttype,  updatedby);

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
            string jsonstr = finalJSON(code, "ETMFILE", DataTableToJSON(dt), msg);
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
    public DataTable JSONToDataTable(string jsonString)
    {
        JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
        DataTable dataTable = new DataTable();
        List<Dictionary<string, object>> rows = jsSerializer.Deserialize<List<Dictionary<string, object>>>(jsonString);

        if (rows.Count > 0)
        {
            foreach (string columnName in rows[0].Keys)
                dataTable.Columns.Add(columnName, typeof(string));
        }

        foreach (var row in rows)
        {
            DataRow dataRow = dataTable.NewRow();

            foreach (string columnName in row.Keys)
            {
                dataRow[columnName] = row[columnName];
            }
            dataTable.Rows.Add(dataRow);
        }
        return dataTable;
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
        return "123456";//random.Next(0, 999999).ToString("D6");
    }
    #endregion
}
