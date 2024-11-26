using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

/// <summary>
/// Summary description for wsClass
/// </summary>
public class wsClass
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    public wsClass()
    {
        //
        // TODO: Add constructor logic here
        //
    }
  #region"login logs"
    public string UpdateLoginLog(string mloginID, string ipaddress, string mode, string deviceid, string latitude, string longitude)
    {
        try
        {
            string result = "";
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_insert_Success_log");
            MyCommand.Parameters.AddWithValue("p_userid", mloginID);
            MyCommand.Parameters.AddWithValue("p_ipaddress", ipaddress);
            MyCommand.Parameters.AddWithValue("p_deviceid", deviceid);
            MyCommand.Parameters.AddWithValue("p_latitude", latitude);
            MyCommand.Parameters.AddWithValue("p_longitude", longitude);
            MyCommand.Parameters.AddWithValue("p_mode", mode);
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                string logid = dt.Rows[0]["p_logid"].ToString();
                return logid;
            }
            result = bll.InsertAll(MyCommand);
            if (result == "Success")
            {

            }
            return "";
        }
        catch (Exception ex)
        {
            return "";
        }
    }

    public bool UpdateUnAuthLoginLog(string mloginID, string ipaddress, string mode, string deviceid, string latitude, string longitude)
    {
        try
        {
            string result = "";
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_insert_UnSuccess_log");
            MyCommand.Parameters.AddWithValue("p_userid", mloginID);
            MyCommand.Parameters.AddWithValue("p_ipaddress", ipaddress);
            MyCommand.Parameters.AddWithValue("p_deviceid", deviceid);
            MyCommand.Parameters.AddWithValue("p_latitude", latitude);
            MyCommand.Parameters.AddWithValue("p_longitude", longitude);
            MyCommand.Parameters.AddWithValue("p_mode", mode);
            result = bll.InsertAll(MyCommand);
            if (result == "Success")
            {
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    #endregion
    #region "App is Activate or Not"
    public DataTable isappactive()
    {
        MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.isappactive");
        MyCommand.Parameters.AddWithValue("p_appid", 1);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    #endregion

    #region "bus service type"
    public DataTable busservicetype(string id)
    {
        MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_max_seatavailable");
        MyCommand.Parameters.AddWithValue("p_serviceid", Convert.ToInt32(id));
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    #endregion

    #region "service reservation charge"
    public DataTable servicereservationcharge(string id)
    {
        MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_reservation_charge");
        MyCommand.Parameters.AddWithValue("p_serviceid", Convert.ToInt32(id));
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    #endregion


    #region "Traveller Login"
    public DataTable traveller_dt(string mobileNo)
    {
        MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.fun_trvlr_mobile_check");
        MyCommand.Parameters.AddWithValue("p_mobileno", mobileNo);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    public DataTable traveller_loginFirst_dt(string mobileNo, string userName, string login_app_web, string ip_imei)
    {
        MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.fun_trvlr_registration");
        MyCommand.Parameters.AddWithValue("p_mobileno", mobileNo);
        MyCommand.Parameters.AddWithValue("p_username", userName);
        MyCommand.Parameters.AddWithValue("p_reg_from", login_app_web);
        MyCommand.Parameters.AddWithValue("p_ip_imei", ip_imei);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    public DataTable traveller_loginSuccess_dt(string mobileNo, string login_app_web, string ip_imei)
    {
        MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.fun_trvlr_login_success");
        MyCommand.Parameters.AddWithValue("p_mobileno", mobileNo);
        MyCommand.Parameters.AddWithValue("p_reg_from", login_app_web);
        MyCommand.Parameters.AddWithValue("p_ip_imei", ip_imei);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    public DataTable traveller_loginFail_dt(string mobileNo, string login_app_web, string ip_imei)
    {
        MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.fun_trvlr_login_fail");
        MyCommand.Parameters.AddWithValue("p_mobileno", mobileNo);
        MyCommand.Parameters.AddWithValue("p_reg_from", login_app_web);
        MyCommand.Parameters.AddWithValue("p_ip_imei", ip_imei);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    #endregion

    #region "Search Stations for App and Web"    
    public List<string> search_station_web(string stationText)
    {
        List<string> empResult = new List<string>();
        try
        {
            sbBLL bll = new sbBLL();
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            DataTable dt = new DataTable();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_search_stations");
            MyCommand.Parameters.AddWithValue("p_search_text", stationText);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        string M = dt.Rows[i]["stonname"].ToString();
                        empResult.Add(M);
                    }
                }
            }
            return empResult;
        }
        catch (Exception ex)
        {
            sbCommonFunc _common = new sbCommonFunc();
            _common.ErrorLog("CntrDashboard-M1", ex.Message);
            return null;
        }
    }

    public List<string> search_station_web(string stationText, string FromTo_FT)
    {
        List<string> empResult = new List<string>();
        try
        {
            sbBLL bll = new sbBLL();
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            DataTable dt = new DataTable();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_search_city");//city based search
            //MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_search_stations");//station based search
            MyCommand.Parameters.AddWithValue("p_search_text", stationText);
            MyCommand.Parameters.AddWithValue("p_from_to", FromTo_FT);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        string M = dt.Rows[i]["cityname"].ToString();
                        empResult.Add(M);
                    }
                }
            }
            return empResult;
        }
        catch (Exception ex)
        {
            sbCommonFunc _common = new sbCommonFunc();
            _common.ErrorLog("CntrDashboard-M1", ex.Message);
            return null;
        }
    }


    public List<string> timetable_station_web(string stationText, string FromTo_FT)
    {
        List<string> empResult = new List<string>();
        try
        {
            sbBLL bll = new sbBLL();
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            DataTable dt = new DataTable();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_timetable_stations");
            MyCommand.Parameters.AddWithValue("p_search_text", stationText);
            MyCommand.Parameters.AddWithValue("p_from_to", FromTo_FT);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        string M = dt.Rows[i]["stonname"].ToString();
                        empResult.Add(M);
                    }
                }
            }
            return empResult;
        }
        catch (Exception ex)
        {
            sbCommonFunc _common = new sbCommonFunc();
            _common.ErrorLog("CntrDashboard-M1", ex.Message);
            return null;
        }
    }



    #endregion

    #region "Search Services for App and Web"
    public DataTable search_services_dt(string fromStationName, string toStationName, string serviceTypeId, string date)
    {
        MyCommand = new NpgsqlCommand();
        dt = new DataTable();
        MyCommand.Parameters.Clear();
        //MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_search_depotservices_new");//Station Based Search
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_search_depotservices_by_city");//City Based Search
        MyCommand.Parameters.AddWithValue("p_from_city", fromStationName);
        MyCommand.Parameters.AddWithValue("p_to_city", toStationName);
        MyCommand.Parameters.AddWithValue("p_servicetype_id", Convert.ToInt16(serviceTypeId));
        MyCommand.Parameters.AddWithValue("p_date", date);
        dt = bll.SelectAll(MyCommand);
        DataRow[] dr = dt.Select("minutes>0");
        int count = dr.Length;
        if (count == 0)
        {
            DataTable dttt = new DataTable();
            return dttt;
        }
        else
        {
            dt=dr.CopyToDataTable();
            return dt;
        }
        
    }
    #endregion

    #region "Traveller Dashboard"
    public DataTable get_serviceType_dt()
    {
        MyCommand = new NpgsqlCommand();
        dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.sp_servicetype_getdetails");
        dt = bll.SelectAll(MyCommand);

        //foreach()

        return dt;
    }

    #endregion

    #region "Layout and Other"
    public DataTable getLayoutRowColumn(string tripid, string triptype, string servicecode, string tripdate)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_rowcolumn");
            MyCommand.Parameters.AddWithValue("p_tripid", Convert.ToInt16(tripid));
            MyCommand.Parameters.AddWithValue("p_triptype", triptype);
            MyCommand.Parameters.AddWithValue("p_servicecode", Convert.ToInt16(servicecode));
            MyCommand.Parameters.AddWithValue("p_tripdate", tripdate);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                return dt;
            }
            else
            {
                return null;
            }

            //if (dt.Rows.Count > 0)
            //{
            //    foreach (DataRow drow in dt.Rows)
            //    {
            //        strseatstruct = strseatstruct + "," + drow["colnumber"].ToString() + "-" + drow["rownumber"].ToString() + "-" + drow["seatno"].ToString() + "-" + drow["seatyn"] + "-" + drow["travellertypecode"] + "-" + drow["seatavailforonlbooking"] + "-" + drow["status"];
            //    }
            //}


        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public DataTable getLayoutTotRowColumn(string servicecode)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_totrowcolumn");
            MyCommand.Parameters.AddWithValue("p_servicecode", Convert.ToInt16(servicecode));
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                return dt;
            }
            else
            {
                return null;
            }

            //if (dt.Rows.Count > 0)
            //{
            //    foreach (DataRow dRowCol in dt.Rows)
            //    {
            //        strseatstruct = dRowCol["noofrows"] + "-" + dRowCol["noofcolumns"] + "-500";
            //    }
            //}
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public DataTable get_boardingStations_dt(string ServiceCode, string ServiceTripCode)
    {

        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_bordingstn");
        MyCommand.Parameters.AddWithValue("p_servicecode", Convert.ToInt64(ServiceCode));
        MyCommand.Parameters.AddWithValue("p_servicetripcode", Convert.ToInt64(ServiceTripCode));
        DataTable dtt = new DataTable();
        dtt = bll.SelectAll(MyCommand);
        return dtt;
    }
    #endregion

    #region "Concession" 

public DataTable CheckBusPassNew(string p_concession, string _passnumber, string _journeyDate)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.check_passnumber");
            MyCommand.Parameters.AddWithValue("p_passnumber", _passnumber);
            MyCommand.Parameters.AddWithValue("p_journeydate", _journeyDate);
            MyCommand.Parameters.AddWithValue("p_concession", Convert.ToInt64(p_concession));
            DataTable dtt = new DataTable();
            dtt = bll.SelectAll(MyCommand);
            if (dtt.Rows.Count > 0)
            {
                return dtt;
            }
            return dt;
        }
        catch (Exception ex)
        {
            return dt;
        }
    }

    public DataTable getConcessionCategory(string dsvcid, string fromstationId, string tostationId)
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.get_concession");
        MyCommand.Parameters.AddWithValue("p_dsvcid", Convert.ToInt64(dsvcid));
        MyCommand.Parameters.AddWithValue("p_fromstation", Convert.ToInt64(fromstationId));
        MyCommand.Parameters.AddWithValue("p_tostation", Convert.ToInt64(tostationId));
        DataTable dtt = new DataTable();
        dtt = bll.SelectAll(MyCommand);
        return dtt;
    }
    public DataTable CheckConcessionCategory(string concession, string gender, string age)
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.check_concession");
        MyCommand.Parameters.AddWithValue("p_concession", Convert.ToInt64(concession));
        MyCommand.Parameters.AddWithValue("p_gender", gender);
        MyCommand.Parameters.AddWithValue("p_age", Convert.ToInt64(age));
        DataTable dtt = new DataTable();
        dtt = bll.SelectAll(MyCommand);
        return dtt;
    }
    public string CheckBusPass(string p_concession, string _passnumber, string _journeyDate)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.check_passnumber");
            MyCommand.Parameters.AddWithValue("p_passnumber", _passnumber);
            MyCommand.Parameters.AddWithValue("p_journeydate", _journeyDate);
            MyCommand.Parameters.AddWithValue("p_concession", Convert.ToInt64(p_concession));
            DataTable dtt = new DataTable();
            dtt = bll.SelectAll(MyCommand);
            if (dtt.Rows.Count > 0)
            {
                return dtt.Rows[0]["p_Result"].ToString();
            }
            return "Error";
        }
        catch (Exception ex)
        {
            return "Error";
        }
    }

    //----------------------------Current Booking
    public DataTable getConcession(string dsvcid, int routid, int frstationid)
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.get_concession");
        MyCommand.Parameters.AddWithValue("p_dsvcid", Convert.ToInt64(dsvcid));
        MyCommand.Parameters.AddWithValue("p_routid", Convert.ToInt64(routid));
        MyCommand.Parameters.AddWithValue("p_frstationid", Convert.ToInt64(frstationid));
        DataTable dtt = new DataTable();
        dtt = bll.SelectAll(MyCommand);
        return dtt;
    }
    public DataTable getConcessionDtls(string concession)
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.get_concessiondtls");
        MyCommand.Parameters.AddWithValue("p_concession", Convert.ToInt64(concession));
        DataTable dtt = new DataTable();
        dtt = bll.SelectAll(MyCommand);
        return dtt;
    }




    #endregion

    #region "Passengers Insertion"
 public bool checkTripGenerate(string date, string strpid)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.fun_checktriptime");
            MyCommand.Parameters.AddWithValue("sp_journeydate", date);
            MyCommand.Parameters.AddWithValue("sp_strpid", strpid);
           
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows[0]["sp_status"].ToString() == "Y")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    public bool IsTicketStillAvaialable(string passengers, string MjourneyDate, string MjourneyType, string MserviceTripCode)
    {
        try
        {
            int[] MseatNo = new int[7];
            Int16 i;
            for (i = 0; i <= 5; i++)
            {
                MseatNo[i] = 0;
            }

            int j = 0;
            string[] CustomerList = passengers.Split('|');
            int Index = 1;
            foreach (string customer in CustomerList)
            {
                string[] customerDetail = customer.Split(',');
                if (MjourneyType == "UP")
                {
                    MseatNo[j] = Convert.ToInt16(customerDetail[0]);
                    j = j + 1;
                }
            }
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_check_ticketstillavailable");
            MyCommand.Parameters.AddWithValue("spservicetripcode", Convert.ToInt32(MserviceTripCode.Trim()));
            MyCommand.Parameters.AddWithValue("spjourneydate", MjourneyDate);
            MyCommand.Parameters.AddWithValue("spseatno1", Convert.ToInt64(MseatNo[0]));
            MyCommand.Parameters.AddWithValue("spseatno2", Convert.ToInt64(MseatNo[1]));
            MyCommand.Parameters.AddWithValue("spseatno3", Convert.ToInt64(MseatNo[2]));
            MyCommand.Parameters.AddWithValue("spseatno4", Convert.ToInt64(MseatNo[3]));
            MyCommand.Parameters.AddWithValue("spseatno5", Convert.ToInt64(MseatNo[4]));
            MyCommand.Parameters.AddWithValue("spseatno6", Convert.ToInt64(MseatNo[5]));
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows[0]["sp_result"].ToString() == "Y")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public DataTable SaveSeats(string depotServiceCode, string tripType, string tripId, string journeyDate, string fromStationId, string toStationId, string booked_by_type_code, string userId, string userMobile, string userEmail, string bordeingStationId, string passengers, string ip_imei,string bookingmode)
    {
        try
        {
            int intPassengerNo = 0;
            //check Seat Availablity
            string[] CustomerList = passengers.Split('|');

            Int16 MintSeatNo1 = 0;
            string McustName1 = "";
            string McustGender1 = "";
            Int16 McustAge1 = 0;
            Int16 MP_SEATQUOTACODE1 = 0;
            string McustOnlineVerificationYN1 = "N";
            string McustPassnumber1 = "";
            string McustIdVerificationYN1 = "N";
            string McustIdVerification1 = "";
            string McustDocumentVerificationYN1 = "N";
            string McustDocumentVerification1 = "";

            Int16 MintSeatNo2 = 0;
            string McustName2 = "";
            string McustGender2 = "";
            Int16 McustAge2 = 0;
            Int16 MP_SEATQUOTACODE2 = 0;
            string McustOnlineVerificationYN2 = "N";
            string McustPassnumber2 = "";
            string McustIdVerificationYN2 = "N";
            string McustIdVerification2 = "";
            string McustDocumentVerificationYN2 = "N";
            string McustDocumentVerification2 = "";

            Int16 MintSeatNo3 = 0;
            string McustName3 = "";
            string McustGender3 = "";
            Int16 McustAge3 = 0;
            Int16 MP_SEATQUOTACODE3 = 0;
            string McustOnlineVerificationYN3 = "N";
            string McustPassnumber3 = "";
            string McustIdVerificationYN3 = "N";
            string McustIdVerification3 = "";
            string McustDocumentVerificationYN3 = "N";
            string McustDocumentVerification3 = "";


            Int16 MintSeatNo4 = 0;
            string McustName4 = "";
            string McustGender4 = "";
            Int16 McustAge4 = 0;
            Int16 MP_SEATQUOTACODE4 = 0;
            string McustOnlineVerificationYN4 = "N";
            string McustPassnumber4 = "";
            string McustIdVerificationYN4 = "N";
            string McustIdVerification4 = "";
            string McustDocumentVerificationYN4 = "N";
            string McustDocumentVerification4 = "";

            Int16 MintSeatNo5 = 0;
            string McustName5 = "";
            string McustGender5 = "";
            Int16 McustAge5 = 0;
            Int16 MP_SEATQUOTACODE5 = 0;
            string McustOnlineVerificationYN5 = "N";
            string McustPassnumber5 = "";
            string McustIdVerificationYN5 = "N";
            string McustIdVerification5 = "";
            string McustDocumentVerificationYN5 = "N";
            string McustDocumentVerification5 = "";

            Int16 MintSeatNo6 = 0;
            string McustName6 = "";
            string McustGender6 = "";
            Int16 McustAge6 = 0;
            Int16 MP_SEATQUOTACODE6 = 0;
            string McustOnlineVerificationYN6 = "N";
            string McustPassnumber6 = "";
            string McustIdVerificationYN6 = "N";
            string McustIdVerification6 = "";
            string McustDocumentVerificationYN6 = "N";
            string McustDocumentVerification6 = "";

            int Index = 1;

            foreach (string customer in CustomerList)
            {
                string[] customerDetail = customer.Split(',');
                string SeatNo = customerDetail[0];
                string custName = customerDetail[1].ToUpper().Trim();
                string custGender = customerDetail[2];
                Int16 custAge = Convert.ToInt16(customerDetail[3].Trim());

                Int16 custConcession = Convert.ToInt16(customerDetail[4].Trim());
                string custOnlineVerificationYN = customerDetail[7].Trim();
                string custPassnumber = customerDetail[8].Trim();
                string custIdVerificationYN = customerDetail[9].Trim();
                string custIdverification = customerDetail[10].Trim();
                string custDocumentVerificationYN = customerDetail[11].Trim();
                string custDocumentVerification = customerDetail[12].Trim();

                Int16 intSeatNo;
                intSeatNo = Convert.ToInt16(SeatNo);

                switch (Index)
                {
                    case 1:
                        {
                            MintSeatNo1 = intSeatNo;
                            McustName1 = custName;
                            McustGender1 = custGender;
                            McustAge1 = custAge;
                            MP_SEATQUOTACODE1 = custConcession;
                            McustOnlineVerificationYN1 = custOnlineVerificationYN;
                            McustPassnumber1 = custPassnumber;
                            McustIdVerificationYN1 = custIdVerificationYN;
                            McustIdVerification1 = custIdverification;
                            McustDocumentVerificationYN1 = custDocumentVerificationYN;
                            McustDocumentVerification1 = custDocumentVerification;

                            intPassengerNo = intPassengerNo + 1;
                            break;
                        }

                    case 2:
                        {
                            MintSeatNo2 = intSeatNo;
                            McustName2 = custName;
                            McustGender2 = custGender;
                            McustAge2 = custAge;
                            MP_SEATQUOTACODE2 = custConcession;
                            McustOnlineVerificationYN2 = custOnlineVerificationYN;
                            McustPassnumber2 = custPassnumber;
                            McustIdVerificationYN2 = custIdVerificationYN;
                            McustIdVerification2 = custIdverification;
                            McustDocumentVerificationYN2 = custDocumentVerificationYN;
                            McustDocumentVerification2 = custDocumentVerification;
                            intPassengerNo = intPassengerNo + 1;
                            break;
                        }

                    case 3:
                        {
                            MintSeatNo3 = intSeatNo;
                            McustName3 = custName;
                            McustGender3 = custGender;
                            McustAge3 = custAge;
                            MP_SEATQUOTACODE3 = custConcession;
                            McustOnlineVerificationYN3 = custOnlineVerificationYN;
                            McustPassnumber3 = custPassnumber;
                            McustIdVerificationYN3 = custIdVerificationYN;
                            McustIdVerification3 = custIdverification;
                            McustDocumentVerificationYN3 = custDocumentVerificationYN;
                            McustDocumentVerification3 = custDocumentVerification;
                            intPassengerNo = intPassengerNo + 1;
                            break;
                        }

                    case 4:
                        {
                            MintSeatNo4 = intSeatNo;
                            McustName4 = custName;
                            McustGender4 = custGender;
                            McustAge4 = custAge;
                            MP_SEATQUOTACODE4 = custConcession;
                            McustOnlineVerificationYN4 = custOnlineVerificationYN;
                            McustPassnumber4 = custPassnumber;
                            McustIdVerificationYN4 = custIdVerificationYN;
                            McustIdVerification4 = custIdverification;
                            McustDocumentVerificationYN4 = custDocumentVerificationYN;
                            McustDocumentVerification4 = custDocumentVerification;
                            intPassengerNo = intPassengerNo + 1;
                            break;
                        }

                    case 5:
                        {
                            MintSeatNo5 = intSeatNo;
                            McustName5 = custName;
                            McustGender5 = custGender;
                            McustAge5 = custAge;
                            MP_SEATQUOTACODE5 = custConcession;
                            McustOnlineVerificationYN5 = custOnlineVerificationYN;
                            McustPassnumber5 = custPassnumber;
                            McustIdVerificationYN5 = custIdVerificationYN;
                            McustIdVerification5 = custIdverification;
                            McustDocumentVerificationYN5 = custDocumentVerificationYN;
                            McustDocumentVerification5 = custDocumentVerification;
                            intPassengerNo = intPassengerNo + 1;
                            break;
                        }

                    case 6:
                        {
                            MintSeatNo6 = intSeatNo;
                            McustName6 = custName;
                            McustGender6 = custGender;
                            McustAge6 = custAge;
                            MP_SEATQUOTACODE6 = custConcession;
                            McustOnlineVerificationYN6 = custOnlineVerificationYN;
                            McustPassnumber6 = custPassnumber;
                            McustIdVerificationYN6 = custIdVerificationYN;
                            McustIdVerification6 = custIdverification;
                            McustDocumentVerificationYN6 = custDocumentVerificationYN;
                            McustDocumentVerification6 = custDocumentVerification;
                            intPassengerNo = intPassengerNo + 1;
                            break;
                        }
                }

                Index = Index + 1;
            }


            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_insert_onl_ticket_start");
            MyCommand.Parameters.AddWithValue("p_depot_service_code", Convert.ToInt64(depotServiceCode));
            MyCommand.Parameters.AddWithValue("p_triptype", tripType);
            MyCommand.Parameters.AddWithValue("p_trip", Convert.ToInt32(tripId));
            MyCommand.Parameters.AddWithValue("p_journey_date", journeyDate);
            MyCommand.Parameters.AddWithValue("p_from_station_code", Convert.ToInt32(fromStationId));
            MyCommand.Parameters.AddWithValue("p_to_station_code", Convert.ToInt32(toStationId));

            MyCommand.Parameters.AddWithValue("p_booked_by_type_code", booked_by_type_code);
            MyCommand.Parameters.AddWithValue("p_booked_by_user_code", userId);
            MyCommand.Parameters.AddWithValue("p_booking_mode", "O");
            MyCommand.Parameters.AddWithValue("p_totalseatbooked", Convert.ToInt64(0));
            MyCommand.Parameters.AddWithValue("p_traveller_mobile_no", userMobile);
            MyCommand.Parameters.AddWithValue("p_traveller_email_id", userEmail);
            MyCommand.Parameters.AddWithValue("p_boarding_station_code", Convert.ToInt32(bordeingStationId));

            //-------------Seat 1
            MyCommand.Parameters.AddWithValue("p_seatno_1", MintSeatNo1);
            MyCommand.Parameters.AddWithValue("p_seatconcessiontypecode_1", MP_SEATQUOTACODE1);
            MyCommand.Parameters.AddWithValue("p_travellername_1", McustName1);
            MyCommand.Parameters.AddWithValue("p_gender_1", McustGender1);
            MyCommand.Parameters.AddWithValue("p_age_1", McustAge1);

            MyCommand.Parameters.AddWithValue("p_onlineverificationyn_1", McustOnlineVerificationYN1);
            MyCommand.Parameters.AddWithValue("p_passnumber_1", McustPassnumber1);
            MyCommand.Parameters.AddWithValue("p_idverificationyn_1", McustIdVerificationYN1);
            MyCommand.Parameters.AddWithValue("p_idverification_1", McustIdVerification1);
            MyCommand.Parameters.AddWithValue("p_documentverificationyn_1", McustDocumentVerificationYN1);
            MyCommand.Parameters.AddWithValue("p_documentverification_1", McustDocumentVerification1);


            //-------------Seat 2
            MyCommand.Parameters.AddWithValue("p_seatno_2", MintSeatNo2);
            MyCommand.Parameters.AddWithValue("p_seatconcessiontypecode_2", MP_SEATQUOTACODE2);
            MyCommand.Parameters.AddWithValue("p_travellername_2", McustName2);
            MyCommand.Parameters.AddWithValue("p_gender_2", McustGender2);
            MyCommand.Parameters.AddWithValue("p_age_2", McustAge2);

            MyCommand.Parameters.AddWithValue("p_onlineverificationyn_2", McustOnlineVerificationYN2);
            MyCommand.Parameters.AddWithValue("p_passnumber_2", McustPassnumber2);
            MyCommand.Parameters.AddWithValue("p_idverificationyn_2", McustIdVerificationYN2);
            MyCommand.Parameters.AddWithValue("p_idverification_2", McustIdVerification2);
            MyCommand.Parameters.AddWithValue("p_documentverificationyn_2", McustDocumentVerificationYN2);
            MyCommand.Parameters.AddWithValue("p_documentverification_2", McustDocumentVerification2);

            //-------------Seat 3
            MyCommand.Parameters.AddWithValue("p_seatno_3", MintSeatNo3);
            MyCommand.Parameters.AddWithValue("p_seatconcessiontypecode_3", MP_SEATQUOTACODE3);
            MyCommand.Parameters.AddWithValue("p_travellername_3", McustName3);
            MyCommand.Parameters.AddWithValue("p_gender_3", McustGender3);
            MyCommand.Parameters.AddWithValue("p_age_3", McustAge3);
            MyCommand.Parameters.AddWithValue("p_onlineverificationyn_3", McustOnlineVerificationYN3);
            MyCommand.Parameters.AddWithValue("p_passnumber_3", McustPassnumber3);
            MyCommand.Parameters.AddWithValue("p_idverificationyn_3", McustIdVerificationYN3);
            MyCommand.Parameters.AddWithValue("p_idverification_3", McustIdVerification3);
            MyCommand.Parameters.AddWithValue("p_documentverificationyn_3", McustDocumentVerificationYN3);
            MyCommand.Parameters.AddWithValue("p_documentverification_3", McustDocumentVerification3);

            //-------------Seat 4
            MyCommand.Parameters.AddWithValue("p_seatno_4", MintSeatNo4);
            MyCommand.Parameters.AddWithValue("p_seatconcessiontypecode_4", MP_SEATQUOTACODE4);
            MyCommand.Parameters.AddWithValue("p_travellername_4", McustName4);
            MyCommand.Parameters.AddWithValue("p_gender_4", McustGender4);
            MyCommand.Parameters.AddWithValue("p_age_4", McustAge4);

            MyCommand.Parameters.AddWithValue("p_onlineverificationyn_4", McustOnlineVerificationYN4);
            MyCommand.Parameters.AddWithValue("p_passnumber_4", McustPassnumber4);
            MyCommand.Parameters.AddWithValue("p_idverificationyn_4", McustIdVerificationYN4);
            MyCommand.Parameters.AddWithValue("p_idverification_4", McustIdVerification4);
            MyCommand.Parameters.AddWithValue("p_documentverificationyn_4", McustDocumentVerificationYN4);
            MyCommand.Parameters.AddWithValue("p_documentverification_4", McustDocumentVerification4);

            //-------------Seat 5
            MyCommand.Parameters.AddWithValue("p_seatno_5", MintSeatNo5);
            MyCommand.Parameters.AddWithValue("p_seatconcessiontypecode_5", MP_SEATQUOTACODE5);
            MyCommand.Parameters.AddWithValue("p_travellername_5", McustName5);
            MyCommand.Parameters.AddWithValue("p_gender_5", McustGender5);
            MyCommand.Parameters.AddWithValue("p_age_5", McustAge5);
            MyCommand.Parameters.AddWithValue("p_onlineverificationyn_5", McustOnlineVerificationYN5);
            MyCommand.Parameters.AddWithValue("p_passnumber_5", McustPassnumber5);
            MyCommand.Parameters.AddWithValue("p_idverificationyn_5", McustIdVerificationYN5);
            MyCommand.Parameters.AddWithValue("p_idverification_5", McustIdVerification5);
            MyCommand.Parameters.AddWithValue("p_documentverificationyn_5", McustDocumentVerificationYN5);
            MyCommand.Parameters.AddWithValue("p_documentverification_5", McustDocumentVerification5);

            //-------------Seat 6
            MyCommand.Parameters.AddWithValue("p_seatno_6", MintSeatNo6);
            MyCommand.Parameters.AddWithValue("p_seatconcessiontypecode_6", MP_SEATQUOTACODE6);
            MyCommand.Parameters.AddWithValue("p_travellername_6", McustName6);
            MyCommand.Parameters.AddWithValue("p_gender_6", McustGender6);
            MyCommand.Parameters.AddWithValue("p_age_6", McustAge6);
            MyCommand.Parameters.AddWithValue("p_onlineverificationyn_6", McustOnlineVerificationYN6);
            MyCommand.Parameters.AddWithValue("p_passnumber_6", McustPassnumber6);
            MyCommand.Parameters.AddWithValue("p_idverificationyn_6", McustIdVerificationYN6);
            MyCommand.Parameters.AddWithValue("p_idverification_6", McustIdVerification6);
            MyCommand.Parameters.AddWithValue("p_documentverificationyn_6", McustDocumentVerificationYN6);
            MyCommand.Parameters.AddWithValue("p_documentverification_6", McustDocumentVerification6);

            MyCommand.Parameters.AddWithValue("p_updatedby", userId);
            MyCommand.Parameters.AddWithValue("p_ipaddress", ip_imei);
            MyCommand.Parameters.AddWithValue("p_booking_submode", bookingmode);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                return dt;
            }
            else
            {

                return null;
            }

        }
        catch (Exception ex)
        {
            return null;
        }
    }



    public DataTable SaveSeats_old(string depotServiceCode, string tripType, string tripId, string journeyDate, string fromStationId, string toStationId, string booked_by_type_code, string userId, string userMobile, string userEmail, string bordeingStationId, string passengers, string ip_imei)
    {
        try
        {
            int intPassengerNo = 0;
            //check Seat Availablity
            string[] CustomerList = passengers.Split('|');

            //---------------Seat No1
            Int16 MintSeatNo1 = 0;
            Int16 MP_SEATQUOTACODE1 = 1;
            string McustName1 = "";
            string McustGender1 = "";
            Int16 McustAge1 = 0;
            //---------------Seat No2
            Int16 MintSeatNo2 = 0;
            Int16 MP_SEATQUOTACODE2 = 1;
            string McustName2 = "";
            string McustGender2 = "";
            Int16 McustAge2 = 0;
            //---------------Seat No3
            Int16 MintSeatNo3 = 0;
            Int16 MP_SEATQUOTACODE3 = 1;
            string McustName3 = "";
            string McustGender3 = "";
            Int16 McustAge3 = 0;
            //---------------Seat No4
            Int16 MintSeatNo4 = 0;
            Int16 MP_SEATQUOTACODE4 = 1;
            string McustName4 = "";
            string McustGender4 = "";
            Int16 McustAge4 = 0;
            //---------------Seat No5
            Int16 MintSeatNo5 = 0;
            Int16 MP_SEATQUOTACODE5 = 1;
            string McustName5 = "";
            string McustGender5 = "";
            Int16 McustAge5 = 0;
            //---------------Seat No6
            Int16 MintSeatNo6 = 0;
            Int16 MP_SEATQUOTACODE6 = 1;
            string McustName6 = "";
            string McustGender6 = "";
            Int16 McustAge6 = 0;

            int Index = 1;
            foreach (string customer in CustomerList)
            {
                //e.g. seatNo,PassengerName,gender,age
                string[] customerDetail = customer.Split(',');
                string SeatNo = customerDetail[0];
                string custName = customerDetail[1].ToUpper().Trim();
                string custGender = customerDetail[2];
                Int16 custAge = Convert.ToInt16(customerDetail[3].Trim());

                // Dim intSeatNo As UInteger
                Int16 intSeatNo;
                intSeatNo = Convert.ToInt16(SeatNo);

                switch (Index)
                {
                    case 1:
                        {
                            MintSeatNo1 = intSeatNo;
                            MP_SEATQUOTACODE1 = 1;
                            McustName1 = custName;
                            McustGender1 = custGender;
                            McustAge1 = custAge;
                            intPassengerNo = intPassengerNo + 1;
                            break;
                        }
                    case 2:
                        {
                            MintSeatNo2 = intSeatNo;
                            MP_SEATQUOTACODE2 = 1;
                            McustName2 = custName;
                            McustGender2 = custGender;
                            McustAge2 = custAge;
                            intPassengerNo = intPassengerNo + 1;
                            break;
                        }
                    case 3:
                        {
                            MintSeatNo3 = intSeatNo;
                            MP_SEATQUOTACODE3 = 1;
                            McustName3 = custName;
                            McustGender3 = custGender;
                            McustAge3 = custAge;
                            intPassengerNo = intPassengerNo + 1;
                            break;
                        }
                    case 4:
                        {
                            MintSeatNo4 = intSeatNo;
                            MP_SEATQUOTACODE4 = 1;
                            McustName4 = custName;
                            McustGender4 = custGender;
                            McustAge4 = custAge;
                            intPassengerNo = intPassengerNo + 1;
                            break;
                        }
                    case 5:
                        {
                            MintSeatNo5 = intSeatNo;
                            MP_SEATQUOTACODE5 = 1;
                            McustName5 = custName;
                            McustGender5 = custGender;
                            McustAge5 = custAge;
                            intPassengerNo = intPassengerNo + 1;
                            break;
                        }
                    case 6:
                        {
                            MintSeatNo6 = intSeatNo;
                            MP_SEATQUOTACODE6 = 1;
                            McustName6 = custName;
                            McustGender6 = custGender;
                            McustAge6 = custAge;
                            intPassengerNo = intPassengerNo + 1;
                            break;
                        }
                }
                Index = Index + 1;
            }

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_insert_onl_ticket_start");
            MyCommand.Parameters.AddWithValue("p_depot_service_code", Convert.ToInt64(depotServiceCode));
            MyCommand.Parameters.AddWithValue("p_triptype", tripType);
            MyCommand.Parameters.AddWithValue("p_trip", Convert.ToInt32(tripId));
            MyCommand.Parameters.AddWithValue("p_journey_date", journeyDate);
            MyCommand.Parameters.AddWithValue("p_from_station_code", Convert.ToInt32(fromStationId));
            MyCommand.Parameters.AddWithValue("p_to_station_code", Convert.ToInt32(toStationId));

            MyCommand.Parameters.AddWithValue("p_booked_by_type_code", booked_by_type_code);
            MyCommand.Parameters.AddWithValue("p_booked_by_user_code", userId);
            MyCommand.Parameters.AddWithValue("p_booking_mode", "O");
            MyCommand.Parameters.AddWithValue("p_totalseatbooked", Convert.ToInt64(0));
            MyCommand.Parameters.AddWithValue("p_traveller_mobile_no", userMobile);
            MyCommand.Parameters.AddWithValue("p_traveller_email_id", userEmail);
            MyCommand.Parameters.AddWithValue("p_boarding_station_code", Convert.ToInt32(bordeingStationId));

            //-------------Seat 1
            MyCommand.Parameters.AddWithValue("p_seatno_1", MintSeatNo1);
            MyCommand.Parameters.AddWithValue("p_seatconcessiontypecode_1", MP_SEATQUOTACODE1);
            MyCommand.Parameters.AddWithValue("p_travellername_1", McustName1);
            MyCommand.Parameters.AddWithValue("p_gender_1", McustGender1);
            MyCommand.Parameters.AddWithValue("p_age_1", McustAge1);

            //-------------Seat 2
            MyCommand.Parameters.AddWithValue("p_seatno_2", MintSeatNo2);
            MyCommand.Parameters.AddWithValue("p_seatconcessiontypecode_2", MP_SEATQUOTACODE2);
            MyCommand.Parameters.AddWithValue("p_travellername_2", McustName2);
            MyCommand.Parameters.AddWithValue("p_gender_2", McustGender2);
            MyCommand.Parameters.AddWithValue("p_age_2", McustAge2);

            //-------------Seat 3
            MyCommand.Parameters.AddWithValue("p_seatno_3", MintSeatNo3);
            MyCommand.Parameters.AddWithValue("p_seatconcessiontypecode_3", MP_SEATQUOTACODE3);
            MyCommand.Parameters.AddWithValue("p_travellername_3", McustName3);
            MyCommand.Parameters.AddWithValue("p_gender_3", McustGender3);
            MyCommand.Parameters.AddWithValue("p_age_3", McustAge3);

            //-------------Seat 4
            MyCommand.Parameters.AddWithValue("p_seatno_4", MintSeatNo4);
            MyCommand.Parameters.AddWithValue("p_seatconcessiontypecode_4", MP_SEATQUOTACODE4);
            MyCommand.Parameters.AddWithValue("p_travellername_4", McustName4);
            MyCommand.Parameters.AddWithValue("p_gender_4", McustGender4);
            MyCommand.Parameters.AddWithValue("p_age_4", McustAge4);

            //-------------Seat 5
            MyCommand.Parameters.AddWithValue("p_seatno_5", MintSeatNo5);
            MyCommand.Parameters.AddWithValue("p_seatconcessiontypecode_5", MP_SEATQUOTACODE5);
            MyCommand.Parameters.AddWithValue("p_travellername_5", McustName5);
            MyCommand.Parameters.AddWithValue("p_gender_5", McustGender5);
            MyCommand.Parameters.AddWithValue("p_age_5", McustAge5);

            //-------------Seat 6
            MyCommand.Parameters.AddWithValue("p_seatno_6", MintSeatNo6);
            MyCommand.Parameters.AddWithValue("p_seatconcessiontypecode_6", MP_SEATQUOTACODE6);
            MyCommand.Parameters.AddWithValue("p_travellername_6", McustName6);
            MyCommand.Parameters.AddWithValue("p_gender_6", McustGender6);
            MyCommand.Parameters.AddWithValue("p_age_6", McustAge6);

            MyCommand.Parameters.AddWithValue("p_updatedby", userId);
            MyCommand.Parameters.AddWithValue("p_ipaddress", ip_imei);

            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                return dt;
            }
            else
            {
                return null;
            }

        }
        catch (Exception ex)
        {
            return null;
        }
    }
    #endregion

    #region "Passengers Confirmation Details"
    public DataTable getTicketDetails_byTicket(string p_ticketno)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_get_ticket_history");
            MyCommand.Parameters.AddWithValue("p_ticket", p_ticketno);
            DataTable dtt = new DataTable();
            dtt = bll.SelectAll(MyCommand);
            return dtt;

           
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public DataTable getFareDetails_byTicket(string p_ticketno)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_get_fare_detail");
            MyCommand.Parameters.AddWithValue("p_ticket", p_ticketno);
            DataTable dtt = new DataTable();
            dtt = bll.SelectAll(MyCommand);
            return dtt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public DataTable getTaxDetails_byTicket(string p_ticketno)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_get_tax_detail");
            MyCommand.Parameters.AddWithValue("p_ticket", p_ticketno);
            DataTable dtt = new DataTable();
            dtt = bll.SelectAll(MyCommand);
            return dtt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

 public DataTable getAvailableTicketForCancel(string p_ticketno, string status,string p_bookedby )
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_get_available_tickets_for_cancel");
            MyCommand.Parameters.AddWithValue("p_ticketno", p_ticketno);
            MyCommand.Parameters.AddWithValue("p_status", status);
            MyCommand.Parameters.AddWithValue("p_bookedby", p_bookedby);
            DataTable dtt = bll.SelectAll(MyCommand);
            DataRow[] dr = dtt.Select("minutes >= 120");
            int noofrows = dr.Length;
            if (noofrows > 0)
            {
                DataTable dttt = dr.CopyToDataTable();
                return dttt;

            }
            else
            {
                return dt;
            }

        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public DataTable getJourneyDetails(string p_ticketno, string status)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_get_journeyDetails");
            MyCommand.Parameters.AddWithValue("p_ticketno", p_ticketno);
            MyCommand.Parameters.AddWithValue("p_status", status);
            DataTable dtt = new DataTable();
            dtt = bll.SelectAll(MyCommand);
            return dtt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public DataTable getTaxDetails(string p_ticketno)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_get_taxDetails");
            MyCommand.Parameters.AddWithValue("p_ticketno", p_ticketno);
            DataTable dtt = new DataTable();
            dtt = bll.SelectAll(MyCommand);
            return dtt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public DataTable getPassengerDetails(string p_ticketno, string status)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_get_passengerDetails");
            MyCommand.Parameters.AddWithValue("p_ticketno", p_ticketno);
            MyCommand.Parameters.AddWithValue("p_status", status);
            DataTable dtt = new DataTable();
            dtt = bll.SelectAll(MyCommand);
            return dtt;
        }
        catch (Exception ex)
        {
            return dt;
        }
    }
    public DataTable getTicketLog_byTicket(string p_ticketno)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_sp_ticket_transactions");
            MyCommand.Parameters.AddWithValue("p_ticket", p_ticketno);
            DataTable dtt = new DataTable();
            dtt = bll.SelectAll(MyCommand);
          
            return dtt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    #endregion

    #region "Update Ticket"
    public DataTable updateTicket(string p_ticketno, string p_paymentMode)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_insert_onl_ticket_confirm");
            MyCommand.Parameters.AddWithValue("p_ticketno", p_ticketno);
            MyCommand.Parameters.AddWithValue("p_pmtmode", p_paymentMode);
            DataTable dtt = new DataTable();
            dtt = bll.SelectAll(MyCommand);
            return dtt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    #endregion

    #region "User Tickets"
    public DataTable getTickets(string p_userId)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_get_tickets");
            MyCommand.Parameters.AddWithValue("p_userid", p_userId);
            DataTable dtt = new DataTable();
            dtt = bll.SelectAll(MyCommand);
            return dtt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public DataTable getTickets(string p_userId, Int64 get_record_count)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_get_tickets");
            MyCommand.Parameters.AddWithValue("p_userid", p_userId);
            MyCommand.Parameters.AddWithValue("p_get_count_no", get_record_count);
            DataTable dtt = new DataTable();
            dtt = bll.SelectAll(MyCommand);
            return dtt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public DataTable getLastTicketLog(string p_ticketNo)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_get_ticket_last_log");
            MyCommand.Parameters.AddWithValue("p_ticketno", p_ticketNo);
            DataTable dtt = new DataTable();
            dtt = bll.SelectAll(MyCommand);
            return dtt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    #endregion

    #region "Ticket Cancel"
    public DataTable cancelTicket(string ticketNo, string seatNo, string seatamt, int selectedSeatsCount, string userId, string ip_imei, string cancelbytype, string cancel_mode)
    {

        char[] charSeparators = new[] { ',' };
        string[] strSeatNo = seatNo.Split(charSeparators, StringSplitOptions.None);
        string[] strSeatAmt = seatamt.Split(charSeparators, StringSplitOptions.None);
        int intNoOfSeat = strSeatNo.Length;

        Int16 seatNo1 = 0;
        Int16 seatNo2 = 0;
        Int16 seatNo3 = 0;
        Int16 seatNo4 = 0;
        Int16 seatNo5 = 0;
        Int16 seatNo6 = 0;
        decimal seatNo1_amt = 0;
        decimal seatNo2_amt = 0;
        decimal seatNo3_amt = 0;
        decimal seatNo4_amt = 0;
        decimal seatNo5_amt = 0;
        decimal seatNo6_amt = 0;

        for (int i = 0; i <= selectedSeatsCount - 1; i++)
        {
            switch (i)
            {
                case 0:
                    {
                        seatNo1 = Convert.ToInt16(strSeatNo[i]);
                       // seatNo1_amt = Convert.ToDecimal(strSeatAmt[i]);
                        break;
                    }
                case 1:
                    {
                        seatNo2 = Convert.ToInt16(strSeatNo[i]);
                        // seatNo2_amt = Convert.ToDecimal(strSeatAmt[i]);
                        break;
                    }
                case 2:
                    {
                        seatNo3 = Convert.ToInt16(strSeatNo[i]);
                       // seatNo3_amt = Convert.ToDecimal(strSeatAmt[i]);
                        break;
                    }
                case 3:
                    {
                        seatNo4 = Convert.ToInt16(strSeatNo[i]);
                       // seatNo4_amt = Convert.ToDecimal(strSeatAmt[i]);
                        break;
                    }
                case 4:
                    {
                        seatNo5 = Convert.ToInt16(strSeatNo[i]);
                       // seatNo5_amt = Convert.ToDecimal(strSeatAmt[i]);
                        break;
                    }
                case 5:
                    {
                        seatNo6 = Convert.ToInt16(strSeatNo[i]);
                        // seatNo6_amt = Convert.ToDecimal(strSeatAmt[i]);
                        break;
                    }
            }
        }
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_ticket_cancellation");
        MyCommand.Parameters.AddWithValue("p_ticketno", ticketNo);
        MyCommand.Parameters.AddWithValue("p_seatno1", seatNo1);
        MyCommand.Parameters.AddWithValue("p_seatno2", seatNo2);
        MyCommand.Parameters.AddWithValue("p_seatno3", seatNo3);
        MyCommand.Parameters.AddWithValue("p_seatno4", seatNo4);
        MyCommand.Parameters.AddWithValue("p_seatno5", seatNo5);
        MyCommand.Parameters.AddWithValue("p_seatno6", seatNo6);
        MyCommand.Parameters.AddWithValue("p_cancelbytype", cancelbytype);
        MyCommand.Parameters.AddWithValue("p_cancelledby", userId);
        MyCommand.Parameters.AddWithValue("p_updatedby", userId);
        MyCommand.Parameters.AddWithValue("p_ipaddress", ip_imei);
        MyCommand.Parameters.AddWithValue("p_mode", cancel_mode);
        DataTable dt_ = bll.SelectAll(MyCommand);

        return dt_;
        //string p_cancellationrefno = dt.Rows[0]["cancellationrefno"].ToString();                      

    }
      public DataTable getAvailableTicket(string userId, string ticketNo, string p_book_by)
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_get_ticket_list_for_cancel");
        MyCommand.Parameters.AddWithValue("p_userid", userId);
        MyCommand.Parameters.AddWithValue("p_ticketno", ticketNo);
        MyCommand.Parameters.AddWithValue("p_book_by", p_book_by);
        DataTable dt_ = bll.SelectAll(MyCommand);
        return dt_;
             
    }
    public DataTable getPassengerDetailsForCancel(string p_ticketno, string status)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_get_ticket_details_for_cancel");
            MyCommand.Parameters.AddWithValue("p_ticketno", p_ticketno);
            MyCommand.Parameters.AddWithValue("p_status", status);
            DataTable dtt = new DataTable();
            dtt = bll.SelectAll(MyCommand);
            return dtt;
        }
        catch (Exception ex)
        {
            return dt;
        }
    }

    public DataTable getCancelledTickets(string userId, string ticketNo)
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_get_cancelledticket");
        MyCommand.Parameters.AddWithValue("p_cancelledby", userId);
        MyCommand.Parameters.AddWithValue("p_pnrno", ticketNo);
        DataTable dt_ = bll.SelectAll(MyCommand);
        return dt_;
    }
    public DataTable getActiveTickets(string userId)
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.get_active_tickets");
        MyCommand.Parameters.AddWithValue("p_userid", userId);
        DataTable dt_ = bll.SelectAll(MyCommand);
        DataRow[] dr = dt_.Select("minutes>0");
        int noofrows = dr.Length;
        if (noofrows > 0)
        {
            dt = dr.CopyToDataTable();
            return dt;
        }
        else
        {
            DataTable dtt = new DataTable();
            return dtt;
        }
    }

    public DataTable getCancelledtxns(string userId, string ticketNo)
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_cancelled_txns");
        MyCommand.Parameters.AddWithValue("p_usercode", userId);
        MyCommand.Parameters.AddWithValue("p_ticketno", ticketNo);
        DataTable dt_ = bll.SelectAll(MyCommand);
        return dt_;
    }
    #endregion

    #region "Transaction Refund"
    public DataTable refundtransactionnew(string refno, string tkt_no, string book_by, string cntrid, string userid)
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.tkt_Refund_insert");
        MyCommand.Parameters.AddWithValue("p_cancel_refno", refno);
        MyCommand.Parameters.AddWithValue("p_tktno", tkt_no);
        MyCommand.Parameters.AddWithValue("p_bookby", book_by);
        MyCommand.Parameters.AddWithValue("p_cntrid", cntrid);
        MyCommand.Parameters.AddWithValue("p_userid", userid);
        DataTable dt_ = bll.SelectAll(MyCommand);
        return dt_;
    }
    public DataTable refundtransaction(string cancelrefno, string refundedby, string updatedby, string ip_address)
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_ticket_refund");
        MyCommand.Parameters.AddWithValue("p_cancel_refno", cancelrefno);
        MyCommand.Parameters.AddWithValue("p_refundedby", refundedby);
        MyCommand.Parameters.AddWithValue("p_updatedby", updatedby);
        MyCommand.Parameters.AddWithValue("p_ipaddress", ip_address);
        DataTable dt_ = bll.SelectAll(MyCommand);
        return dt_;
    }
    public DataTable getRefundedTicket(string refundedby, string ticketno)
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_get_refunded_ticket");
        MyCommand.Parameters.AddWithValue("p_refunded_by", refundedby);
        MyCommand.Parameters.AddWithValue("p_ticketno", ticketno);
        DataTable dt_ = bll.SelectAll(MyCommand);
        return dt_;
    }
    public DataTable getRefundTickets(string userid)
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_get_refund_status_tickets");
        MyCommand.Parameters.AddWithValue("p_userid", userid);

        DataTable dt_ = bll.SelectAll(MyCommand);
        return dt_;
    }

    #endregion

    #region "Grievance"

    public DataTable saveGrievances(string category, string subcategory, string busno, string ticketno, string description, string pic1, string pic2, string latt, string longg, string userId, string ip_imei)
    {
        byte[] pic1Bytes = Convert.FromBase64String(pic1);
        byte[] pic2Bytes = Convert.FromBase64String(pic2);

        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_grievance_insert");
        MyCommand.Parameters.AddWithValue("p_gcatid", Convert.ToInt32(category));
        MyCommand.Parameters.AddWithValue("p_gremark", description);
        MyCommand.Parameters.AddWithValue("p_gbyuser", userId);
        MyCommand.Parameters.AddWithValue("p_gbyimei", ip_imei);
        MyCommand.Parameters.AddWithValue("p_gsubcatid", Convert.ToInt32(subcategory));
        MyCommand.Parameters.AddWithValue("p_busno", busno);
        MyCommand.Parameters.AddWithValue("p_ticketno", ticketno);
        MyCommand.Parameters.AddWithValue("p_latt", latt);
        MyCommand.Parameters.AddWithValue("p_longg", longg);

        MyCommand.Parameters.AddWithValue("p_pic", (object)pic1Bytes ?? DBNull.Value);
        MyCommand.Parameters.AddWithValue("p_picc", (object)pic2Bytes ?? DBNull.Value);
        DataTable dt = new DataTable();
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    public DataTable getGrievances(string userId)
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_grievance_get");
        MyCommand.Parameters.AddWithValue("p_userid", userId);
        DataTable dt_ = bll.SelectAll(MyCommand);
        return dt_;
    }

    public DataTable getGrievances(string userId, Int64 get_record_count, string record_first_last_FL)
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_grievance_get");
        MyCommand.Parameters.AddWithValue("p_userid", userId);
        MyCommand.Parameters.AddWithValue("p_get_count_no", get_record_count);
        MyCommand.Parameters.AddWithValue("p_get_order_fl", record_first_last_FL);
        DataTable dt_ = bll.SelectAll(MyCommand);
        return dt_;
    }

    public DataTable getGrievanceDetails(string grievanceRefNo)
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "traveller_grievance.GET_GRIEVANCE_DETAIL");
        MyCommand.Parameters.AddWithValue("p_refno", grievanceRefNo);
        DataTable dt_ = bll.SelectAll(MyCommand);
        return dt_;
    }
    public DataTable getGrievanceCategories()
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_grievance_category_get");
        DataTable dt_ = bll.SelectAll(MyCommand);
        return dt_;
    }
    #endregion

    #region "Rating"
    public DataTable saveRatings(string ticketNo, string userId, Int16 portalRating, Int16 staffRating, Int16 busRating, string portalFeedback, string staffFeedback, string busFeedback, string ip_imei)
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_rating_insert");
        MyCommand.Parameters.AddWithValue("p_ticketno", ticketNo);
        MyCommand.Parameters.AddWithValue("p_userid", userId);
        MyCommand.Parameters.AddWithValue("p_portalrating", portalRating);
        MyCommand.Parameters.AddWithValue("p_staffrating", staffRating);
        MyCommand.Parameters.AddWithValue("p_busrating", busRating);
        MyCommand.Parameters.AddWithValue("p_portalfeedback", portalFeedback);
        MyCommand.Parameters.AddWithValue("p_stafffeedback", staffFeedback);
        MyCommand.Parameters.AddWithValue("p_busfeedback", busFeedback);
        MyCommand.Parameters.AddWithValue("ip_imei", ip_imei);
        DataTable dt = new DataTable();
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    public DataTable getRatingTickets(string userId)
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_rating_tickets_get");
        MyCommand.Parameters.AddWithValue("p_userid", userId);
        DataTable dt_ = bll.SelectAll(MyCommand);
        return dt_;
    }
    public DataTable getRatingTicketsNew(string userId)
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_get_feedback_ticket");
        MyCommand.Parameters.AddWithValue("p_userid", userId);
        DataTable dt_ = bll.SelectAll(MyCommand);
        return dt_;
    }
    public DataTable saveRatingsNew(string userId , string tktNo, string bookingRate , string conductorRate , string busRate , string bookingFeedback , string conductorFeedback , string busFeedback , string IMEI )
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_save_feedback_ticket");
        MyCommand.Parameters.AddWithValue("p_userid", userId);
        MyCommand.Parameters.AddWithValue("p_ticketno", tktNo);
        MyCommand.Parameters.AddWithValue("p_or",Convert.ToDecimal( bookingRate));
        MyCommand.Parameters.AddWithValue("p_cr", Convert.ToDecimal(conductorRate));
        MyCommand.Parameters.AddWithValue("p_br", Convert.ToDecimal(busRate));
        MyCommand.Parameters.AddWithValue("p_of", bookingFeedback);
        MyCommand.Parameters.AddWithValue("p_cf", conductorFeedback);
        MyCommand.Parameters.AddWithValue("p_bf", busFeedback);
        MyCommand.Parameters.AddWithValue("p_ip", IMEI);
        DataTable dt = new DataTable();
        dt = bll.SelectAll(MyCommand);
        return dt;
    }

    #endregion

    #region "Wallet"
    public DataTable saveWalletTxn(string userId, string txnRefrence, string txnId, string txnTypeCode, string refrence, string amount)
    {
        MyCommand = new NpgsqlCommand();
        dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_wallet_txn_update");
        MyCommand.Parameters.AddWithValue("p_userid", userId);
        MyCommand.Parameters.AddWithValue("p_txnrefrence", txnRefrence);
        MyCommand.Parameters.AddWithValue("p_txnid", txnId);
        MyCommand.Parameters.AddWithValue("p_txntypecode", txnTypeCode);
        MyCommand.Parameters.AddWithValue("p_refrence", refrence);
        MyCommand.Parameters.AddWithValue("p_amount", amount);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    public DataTable getWalletDetail_dt(string userId)
    {
        MyCommand = new NpgsqlCommand();
        dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_wallet_detail");
        MyCommand.Parameters.AddWithValue("p_userid", userId);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    public DataTable wallet_ticket_confirm(string userId, string ticket)
    {
        MyCommand = new NpgsqlCommand();
        dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.wallet_ticket_confirm");
        MyCommand.Parameters.AddWithValue("p_userid", userId);
        MyCommand.Parameters.AddWithValue("p_ticketno", ticket);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    public DataTable getWalletTransactions_dt(string userId, string txnCounts, string txnOrder_FL)
    {
        MyCommand = new NpgsqlCommand();
        dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_wallet_transactions");
        MyCommand.Parameters.AddWithValue("p_userid", userId);
        MyCommand.Parameters.AddWithValue("p_txn_count", Convert.ToInt64(txnCounts));
        MyCommand.Parameters.AddWithValue("p_txn_order_fl", txnOrder_FL);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    public DataTable walletTopup_start_completed(string txnRefrence, string userId, string amount, string first_last)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            dt = new DataTable();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_wallet_topup_transactions");
            MyCommand.Parameters.AddWithValue("p_refno", txnRefrence);
            MyCommand.Parameters.AddWithValue("p_userid", userId);
            MyCommand.Parameters.AddWithValue("p_amount", amount);
            MyCommand.Parameters.AddWithValue("p_txn_fl", first_last);
            dt = bll.SelectAll(MyCommand);
            return dt;
        }
        catch (Exception e)
        {
            string err = e.Message;
            return dt;
        }
    }
    public DataTable walletTopup_txn_status(string txnRefrence, string userId)
    {
        MyCommand = new NpgsqlCommand();
        dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_wallet_topup_txn_status_get");
        MyCommand.Parameters.AddWithValue("p_refno", txnRefrence);
        MyCommand.Parameters.AddWithValue("p_userid", userId);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    public DataTable walletTopup_txn(string userId)
    {
        MyCommand = new NpgsqlCommand();
        dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_topup_transactions");
        MyCommand.Parameters.AddWithValue("p_userid", userId);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    #endregion

    #region "PG HDFC"
    public DataTable pgRequestHDFC(string p_requesttype, string p_merchantcode, string p_merchanttxnrefno, string p_itc, string p_amount, string p_currencycode, string p_uniquecustomerid, string p_returnurl, string p_s2sreturnurl, string p_tpsltxnid, string p_shoppingcartdetails, string p_txndate, string p_email, string p_mobileno, string p_bankcode, string p_coustomername, string p_cardid, string p_accountno, string p_iskey, string p_isiv)
    {
        MyCommand = new NpgsqlCommand();
        dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_pg_hdfc_request_insert");
        MyCommand.Parameters.AddWithValue("p_requesttype", p_requesttype);
        MyCommand.Parameters.AddWithValue("p_merchantcode", p_merchantcode);
        MyCommand.Parameters.AddWithValue("p_merchanttxnrefno", p_merchanttxnrefno);
        MyCommand.Parameters.AddWithValue("p_itc", p_itc);
        MyCommand.Parameters.AddWithValue("p_amount", p_amount);
        MyCommand.Parameters.AddWithValue("p_currencycode", p_currencycode);
        MyCommand.Parameters.AddWithValue("p_uniquecustomerid", p_uniquecustomerid);
        MyCommand.Parameters.AddWithValue("p_returnurl", p_returnurl);
        MyCommand.Parameters.AddWithValue("p_s2sreturnurl", p_s2sreturnurl);
        MyCommand.Parameters.AddWithValue("p_tpsltxnid", p_tpsltxnid);
        MyCommand.Parameters.AddWithValue("p_shoppingcartdetails", p_shoppingcartdetails);
        MyCommand.Parameters.AddWithValue("p_txndate", p_txndate);
        MyCommand.Parameters.AddWithValue("p_email", p_email);
        MyCommand.Parameters.AddWithValue("p_mobileno", p_mobileno);
        MyCommand.Parameters.AddWithValue("p_bankcode", p_bankcode);
        MyCommand.Parameters.AddWithValue("p_coustomername", p_coustomername);
        MyCommand.Parameters.AddWithValue("p_cardid", p_cardid);
        MyCommand.Parameters.AddWithValue("p_accountno", p_accountno);
        MyCommand.Parameters.AddWithValue("p_iskey", p_iskey);
        MyCommand.Parameters.AddWithValue("p_isiv", p_isiv);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    public DataTable pgResponseHDFC(string userId, string txnRefrence, string txnId, string txnTypeCode, string refrence, string amount)
    {
        MyCommand = new NpgsqlCommand();
        dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_wallet_txn_update");
        MyCommand.Parameters.AddWithValue("p_userid", userId);
        MyCommand.Parameters.AddWithValue("p_txnrefrence", txnRefrence);
        MyCommand.Parameters.AddWithValue("p_txnid", txnId);
        MyCommand.Parameters.AddWithValue("p_txntypecode", txnTypeCode);
        MyCommand.Parameters.AddWithValue("p_refrence", refrence);
        MyCommand.Parameters.AddWithValue("p_amount", amount);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }

    #endregion

    #region "Terms and Condition"
    public DataTable gettermsCondition()
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_terms_condition_getdetails");
        DataTable dt_ = bll.SelectAll(MyCommand);
        return dt_;
    }
    #endregion
    #region "Alert"
    public DataTable getservicealert(string mobno, int flag)
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_servicealert_getdetails");
        MyCommand.Parameters.AddWithValue("p_mobileno", mobno);
        MyCommand.Parameters.AddWithValue("p_flag", flag);
        DataTable dt_ = bll.SelectAll(MyCommand);
        return dt_;
    }
    #endregion

    #region "Trips"
    public DataTable gettrip(string depot, string counter, string mode, string status, string date)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_get_triplist");
            MyCommand.Parameters.AddWithValue("p_depotcode", depot);
            MyCommand.Parameters.AddWithValue("p_cntrcode", counter);
            MyCommand.Parameters.AddWithValue("p_mode", mode);
            MyCommand.Parameters.AddWithValue("p_status", status);
            MyCommand.Parameters.AddWithValue("p_date", date);
            DataTable dtt = new DataTable();
            dtt = bll.SelectAll(MyCommand);
            return dtt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
public DataTable gettripagent(string depot, string station, string mode, string status, string date)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "agent.f_get_triplist");
            MyCommand.Parameters.AddWithValue("p_depotcode", depot);
            MyCommand.Parameters.AddWithValue("p_station", station);
            MyCommand.Parameters.AddWithValue("p_mode", mode);
            MyCommand.Parameters.AddWithValue("p_status", status);
            MyCommand.Parameters.AddWithValue("p_date", date);
            DataTable dtt = new DataTable();
            dtt = bll.SelectAll(MyCommand);
            return dtt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public DataTable gettrip_details(string tripcode)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_get_tripdetails");
            MyCommand.Parameters.AddWithValue("p_tripcode", tripcode);
            
            DataTable dtt = new DataTable();
            dtt = bll.SelectAll(MyCommand);
            return dtt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public DataTable gettrip_psngr_details(string tripcode)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_get_trippassengerdetails");
            MyCommand.Parameters.AddWithValue("p_tripcode", tripcode);

            DataTable dtt = new DataTable();
            dtt = bll.SelectAll(MyCommand);
            return dtt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public DataTable generate_trip_chart(string mode,string trip_code,string waybill,string strp_id,string srtp_date,
       string busno,string driver,string driver2,string conductor,string conductor2,string latt,string longitude,string parking,string updateby)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_generate_tripchart");
            MyCommand.Parameters.AddWithValue("p_mode", mode);
            MyCommand.Parameters.AddWithValue("p_trip_code", trip_code);
            MyCommand.Parameters.AddWithValue("p_waybill", waybill);
            MyCommand.Parameters.AddWithValue("p_strp_id",Convert.ToInt64(strp_id));
            MyCommand.Parameters.AddWithValue("p_srtp_date", srtp_date);
            MyCommand.Parameters.AddWithValue("p_busno", busno);
            MyCommand.Parameters.AddWithValue("p_driver_code", driver);
            MyCommand.Parameters.AddWithValue("p_driver_code2", driver2);
            MyCommand.Parameters.AddWithValue("p_conductor_code", conductor);
            MyCommand.Parameters.AddWithValue("p_conductor_code2", conductor2);
            MyCommand.Parameters.AddWithValue("p_latt", latt);
            MyCommand.Parameters.AddWithValue("p_long", longitude);
            MyCommand.Parameters.AddWithValue("p_parking_place", parking);
            MyCommand.Parameters.AddWithValue("p_updated_by", updateby);
            DataTable dtt = new DataTable();
            dtt = bll.SelectAll(MyCommand);
            return dtt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    #endregion

    public static string getIPAddress()
    {
        return System.Web.HttpContext.Current.Request.UserHostAddress;
    }
    public DataTable getSplCancelledTickets(string bookby, string ticketno)
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_get_splcancelledticket");
        MyCommand.Parameters.AddWithValue("p_bookby", bookby);
        MyCommand.Parameters.AddWithValue("p_pnrno", ticketno);
        DataTable dt_ = bll.SelectAll(MyCommand);
        return dt_;
    }
    public DataTable getSplCancelledTicketsNew(string userid, string ticketno, string bookby)
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_get_splcancelledticketdetails");
        MyCommand.Parameters.AddWithValue("p_userid", userid);
        MyCommand.Parameters.AddWithValue("p_bookby", bookby);
        MyCommand.Parameters.AddWithValue("p_pnrno", ticketno);
        DataTable dt_ = bll.SelectAll(MyCommand);
        return dt_;
    }

#region "Service Type Max Seat At A Time"
    public DataTable getMaxSeats(string srtp_id)
    {
        MyCommand = new NpgsqlCommand();
        dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_getmaxseat");
        MyCommand.Parameters.AddWithValue("p_srtp", srtp_id);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    #endregion


}