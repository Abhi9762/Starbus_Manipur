using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

/// <summary>
/// Summary description for ClassETM
/// </summary>
public class ClassETM
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    public ClassETM()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #region "Update Action Log"
    public DataTable updateActionlog(int actioncategory, int actiontype, string updateby, string ipaddress, string deviceid, string latitude, string longitude, string mode)
    {
        MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.fun_action_log_insert");
        MyCommand.Parameters.AddWithValue("p_action_code", actioncategory);
        MyCommand.Parameters.AddWithValue("p_actiontype_code", actiontype);
        MyCommand.Parameters.AddWithValue("p_updatedby", updateby);
        MyCommand.Parameters.AddWithValue("p_ipaddress", ipaddress);
        MyCommand.Parameters.AddWithValue("p_deviceid", deviceid);
        MyCommand.Parameters.AddWithValue("p_latitude", latitude);
        MyCommand.Parameters.AddWithValue("p_longitude", longitude);
        MyCommand.Parameters.AddWithValue("p_mode", mode);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }

    public DataTable uploadActionlog(DataTable dt_data)
    {
        string p_log_id, p_action_code, p_action_by, p_action_date_time, p_action_lat,
    p_action_long, p_action_remark, p_uploaded_lat,
    p_uploaded_long, p_waybill_no, p_device_id;

        p_log_id = dt_data.Rows[0]["p_log_id"].ToString();
        p_action_code = dt_data.Rows[0]["p_action_code"].ToString();
        p_action_by = dt_data.Rows[0]["p_action_by"].ToString();
        p_action_date_time = dt_data.Rows[0]["p_action_date_time"].ToString();
        p_action_lat = dt_data.Rows[0]["p_action_lat"].ToString();
        p_action_long = dt_data.Rows[0]["p_action_long"].ToString();
        p_action_remark = dt_data.Rows[0]["p_action_remark"].ToString();
        p_uploaded_lat = dt_data.Rows[0]["p_uploaded_lat"].ToString();
        p_uploaded_long = dt_data.Rows[0]["p_uploaded_long"].ToString();
        p_waybill_no = dt_data.Rows[0]["p_waybill_no"].ToString();
        p_device_id = dt_data.Rows[0]["p_device_id"].ToString();
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "etm.f_upload_action_log");
        MyCommand.Parameters.AddWithValue("p_log_id",Convert.ToInt64( p_log_id));
        MyCommand.Parameters.AddWithValue("p_action_code", Convert.ToInt32(p_action_code));
        MyCommand.Parameters.AddWithValue("p_action_by", p_action_by);
        MyCommand.Parameters.AddWithValue("p_action_date_time", p_action_date_time);
        MyCommand.Parameters.AddWithValue("p_action_lat", p_action_lat);
        MyCommand.Parameters.AddWithValue("p_action_long", p_action_long);
        MyCommand.Parameters.AddWithValue("p_action_remark", p_action_remark);
        MyCommand.Parameters.AddWithValue("p_uploaded_lat", p_uploaded_lat);
        MyCommand.Parameters.AddWithValue("p_uploaded_long", p_uploaded_long);
        MyCommand.Parameters.AddWithValue("p_waybill_no", p_waybill_no);
        MyCommand.Parameters.AddWithValue("p_device_id", p_device_id);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    #endregion

    #region "App is Activate or Not"
    public DataTable isappactive()
    {
        MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.isappactive");
        MyCommand.Parameters.AddWithValue("p_appid", 2);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    public DataTable isDeviceRegistered(string imeino)
    {
        MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "etm.get_etm_details");
        MyCommand.Parameters.AddWithValue("p_imie", imeino);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    #endregion

    #region "Last Update"
    public DataTable checkLastUpdate(string officeId)
    {
        MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "etm.f_get_last_updates");
        MyCommand.Parameters.AddWithValue("p_officeid", officeId);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    #endregion

    #region "Get Route"
    public DataTable getRoute(string officeid, string deviceid, string latitude, string longitude, string mode)
    {
        MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "etm.f_get_routes");
        MyCommand.Parameters.AddWithValue("p_officeid", officeid);
        MyCommand.Parameters.AddWithValue("p_deviceid", deviceid);
        MyCommand.Parameters.AddWithValue("p_latitude", latitude);
        MyCommand.Parameters.AddWithValue("p_longitude", longitude);
        MyCommand.Parameters.AddWithValue("p_mode", mode);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    public DataTable getRouteStations(string officeid, string deviceid, string latitude, string longitude, string mode)
    {
        MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "etm.f_get_routes_stations");
        MyCommand.Parameters.AddWithValue("p_officeid", officeid);
        MyCommand.Parameters.AddWithValue("p_deviceid", deviceid);
        MyCommand.Parameters.AddWithValue("p_latitude", latitude);
        MyCommand.Parameters.AddWithValue("p_longitude", longitude);
        MyCommand.Parameters.AddWithValue("p_mode", mode);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
public DataTable getdepotservicestation(string dsvcid)
    {
        MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "etm.f_get_depot_service_stations");
        MyCommand.Parameters.AddWithValue("p_dsvc_id", Convert.ToInt64(dsvcid));
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    public DataTable getdepotserviceOnlineStation(string dsvcid)
    {
        MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "etm.f_get_depot_service_online_stations");
        MyCommand.Parameters.AddWithValue("p_dsvc_id", Convert.ToInt32(dsvcid));
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    #endregion

    #region "get Fare"
    
public DataTable getFareStations(string officeId, string deviceid,string srtpid, string latitude, string longitude, string mode)
    {
        MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "etm.f_get_fare_stations");
        MyCommand.Parameters.AddWithValue("p_officeid", officeId);
        MyCommand.Parameters.AddWithValue("p_deviceid", deviceid);
MyCommand.Parameters.AddWithValue("p_srtpid", srtpid);
        MyCommand.Parameters.AddWithValue("p_latitude", latitude);
        MyCommand.Parameters.AddWithValue("p_longitude", longitude);
        MyCommand.Parameters.AddWithValue("p_mode", mode);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    #endregion

    #region "get Concessions"
    public DataTable getConcessions(string deviceid, string latitude, string longitude, string mode)
    {
        MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "etm.f_get_concessions");
        MyCommand.Parameters.AddWithValue("p_deviceid", deviceid);
        MyCommand.Parameters.AddWithValue("p_latitude", latitude);
        MyCommand.Parameters.AddWithValue("p_longitude", longitude);
        MyCommand.Parameters.AddWithValue("p_mode", mode);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    #endregion

    #region "get PG"
    public DataTable getQRpg()
    {
        MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "pg.f_get_qr_pg");
       
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    #endregion

    #region "Expenses And Extra Earning"
    public DataTable getExpensesExtraEarning(string deviceid, string latitude, string longitude, string mode)
    {
        MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "etm.f_get_expenses_earnings");
        MyCommand.Parameters.AddWithValue("p_deviceid", deviceid);
        MyCommand.Parameters.AddWithValue("p_latitude", latitude);
        MyCommand.Parameters.AddWithValue("p_longitude", longitude);
        MyCommand.Parameters.AddWithValue("p_mode", mode);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    public DataTable uploadExpensesExtraEarning(DataTable dt_data)
    {
        string p_waybill_no, p_trip_no, p_expense_earning_id, p_remark, p_amount, p_record_stage_id, p_record_latt, p_record_long, p_record_datetime, p_upload_latt, p_upload_long, p_log_id;
        p_waybill_no = dt_data.Rows[0]["p_waybill_no"].ToString();
        p_trip_no = dt_data.Rows[0]["p_trip_no"].ToString();
        p_expense_earning_id = dt_data.Rows[0]["p_expense_earning_id"].ToString();
        p_remark = dt_data.Rows[0]["p_remark"].ToString();
        p_amount = dt_data.Rows[0]["p_amount"].ToString();
        p_record_stage_id = dt_data.Rows[0]["p_record_stage_id"].ToString();
        p_record_latt = dt_data.Rows[0]["p_record_latt"].ToString();
        p_record_long = dt_data.Rows[0]["p_record_long"].ToString();
        p_record_datetime = dt_data.Rows[0]["p_record_datetime"].ToString();
        p_upload_latt = dt_data.Rows[0]["p_upload_latt"].ToString();
        p_upload_long = dt_data.Rows[0]["p_upload_long"].ToString();
        p_log_id = dt_data.Rows[0]["p_log_id"].ToString();
        MyCommand = new NpgsqlCommand();

        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "etm.f_upload_expenses_earnings");
        MyCommand.Parameters.AddWithValue("p_waybill_no", p_waybill_no);
        MyCommand.Parameters.AddWithValue("p_trip_no", Convert.ToInt32(p_trip_no));
        MyCommand.Parameters.AddWithValue("p_expense_earning_id", Convert.ToInt32(p_expense_earning_id));
        MyCommand.Parameters.AddWithValue("p_remark", p_remark);
        MyCommand.Parameters.AddWithValue("p_amount", Convert.ToDecimal(p_amount));
        MyCommand.Parameters.AddWithValue("p_record_stage_id", p_record_stage_id);
        MyCommand.Parameters.AddWithValue("p_record_latt", p_record_latt);
        MyCommand.Parameters.AddWithValue("p_record_long", p_record_long);
        MyCommand.Parameters.AddWithValue("p_record_datetime", p_record_datetime);
        MyCommand.Parameters.AddWithValue("p_upload_latt", p_upload_latt);
        MyCommand.Parameters.AddWithValue("p_upload_long", p_upload_long);
        MyCommand.Parameters.AddWithValue("p_log_id", p_log_id);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    #endregion

    #region "User"
    public DataTable userdetail(string userid)
    {
        MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_userdetails_etm");
        MyCommand.Parameters.AddWithValue("p_userid", userid);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    #endregion

    #region "Waybill"
    public DataTable getWaybilDetails(string dutyrefno, string deviceid, string latitude, string longitude, string mode,string depotid)
    {
        MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "etm.f_get_waybill_details");
        MyCommand.Parameters.AddWithValue("p_waybill", dutyrefno);
        MyCommand.Parameters.AddWithValue("p_deviceid", deviceid);
        MyCommand.Parameters.AddWithValue("p_latitude", latitude);
        MyCommand.Parameters.AddWithValue("p_longitude", longitude);
        MyCommand.Parameters.AddWithValue("p_mode", mode);
MyCommand.Parameters.AddWithValue("p_depotid", depotid);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    public DataTable assignWaybill(string DUTYREFNO, string etmrefno, int ManualTicketStart, int ManualTicketEnd, string denominationBook, int Deno100Start, int Deno100End, int Deno100Total, int Deno50Start, int Deno50End, int Deno50Total, int Deno20Start, int Deno20End, int Deno20Total, int Deno10Start, int Deno10End, int Deno10Total, int Deno5Start, int Deno5End, int Deno5Total, int Deno2Start, int Deno2End, int Deno2Total, int Deno1Start, int Deno1End, int Deno1Total, string UpdatedBy, string IpAddress, string latitude, string longitude, string mode, string deviceid)
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_generatewaybill_new");
        MyCommand.Parameters.AddWithValue("p_dutyrefno", DUTYREFNO);
        MyCommand.Parameters.AddWithValue("p_etmno", etmrefno);
        MyCommand.Parameters.AddWithValue("p_manualticketstartno", ManualTicketStart);
        MyCommand.Parameters.AddWithValue("p_manualticketendno", ManualTicketEnd);
        MyCommand.Parameters.AddWithValue("p_denominationbook", denominationBook);
        MyCommand.Parameters.AddWithValue("p_100denostart", Deno100Start);
        MyCommand.Parameters.AddWithValue("p_100denoend", Deno100End);
        MyCommand.Parameters.AddWithValue("p_100denototal", Deno100Total);
        MyCommand.Parameters.AddWithValue("p_50denostart", Deno50Start);
        MyCommand.Parameters.AddWithValue("p_50denoend", Deno50End);
        MyCommand.Parameters.AddWithValue("p_50denototal", Deno50Total);
        MyCommand.Parameters.AddWithValue("p_20denostart", Deno20Start);
        MyCommand.Parameters.AddWithValue("p_20denoend", Deno20End);
        MyCommand.Parameters.AddWithValue("p_20denototal", Deno20Total);
        MyCommand.Parameters.AddWithValue("p_10denostart", Deno10Start);
        MyCommand.Parameters.AddWithValue("p_10denoend", Deno10End);
        MyCommand.Parameters.AddWithValue("p_10denototal", Deno10Total);
        MyCommand.Parameters.AddWithValue("p_5denostart", Deno5Start);
        MyCommand.Parameters.AddWithValue("p_5denoend", Deno5End);
        MyCommand.Parameters.AddWithValue("p_5denototal", Deno5Total);
        MyCommand.Parameters.AddWithValue("p_2denostart", Deno2Start);
        MyCommand.Parameters.AddWithValue("p_2denoend", Deno2End);
        MyCommand.Parameters.AddWithValue("p_2denototal", Deno2Total);
        MyCommand.Parameters.AddWithValue("p_1denostart", Deno1Start);
        MyCommand.Parameters.AddWithValue("p_1denoend", Deno1End);
        MyCommand.Parameters.AddWithValue("p_1denototal", Deno1Total);
        MyCommand.Parameters.AddWithValue("p_updatedby", UpdatedBy);
        MyCommand.Parameters.AddWithValue("p_ipaddress", IpAddress);
        MyCommand.Parameters.AddWithValue("p_deviceid", deviceid);
        MyCommand.Parameters.AddWithValue("p_latitude", latitude);
        MyCommand.Parameters.AddWithValue("p_longitude", longitude);
        MyCommand.Parameters.AddWithValue("p_mode", mode);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    public DataTable cancelWaybill(string dutyrefno, string deviceid, string latitude, string longitude, string mode)
    {
        MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "etm.f_cancelwaybill_from_etm");
        MyCommand.Parameters.AddWithValue("p_dutyrefno", dutyrefno);
        MyCommand.Parameters.AddWithValue("p_deviceid", deviceid);
        MyCommand.Parameters.AddWithValue("p_latitude", latitude);
        MyCommand.Parameters.AddWithValue("p_longitude", longitude);
        MyCommand.Parameters.AddWithValue("p_mode", mode);
        MyCommand.Parameters.AddWithValue("p_cancelby", mode);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    #endregion

    #region "get TIs"
    public DataTable getTIs()
    {
        MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "etm.f_get_tis");
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    #endregion

    #region "get Waybill Trips"
    public DataTable getWaybillTrips(string dsvcid)
    {
        MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "etm.f_get_waybill_trips");
        MyCommand.Parameters.AddWithValue("p_dsvcid", Convert.ToInt32(dsvcid));
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    #endregion

    #region "Upload Loginlog"
    public DataTable uploadloginlog(DataTable dt_data)
    {
        string p_log_id, p_user_id, p_roll, p_login_date_time, p_login_lat, p_login_long, p_logout_date_time, p_logout_lat,
               p_logout_long, p_uploaded_lat, p_uploaded_long, p_action_log_id, p_device_id;

        p_log_id = dt_data.Rows[0]["p_log_id"].ToString();
        p_user_id = dt_data.Rows[0]["p_user_id"].ToString();
        p_roll = dt_data.Rows[0]["p_roll"].ToString();
        p_login_date_time = dt_data.Rows[0]["p_login_date_time"].ToString();
        p_login_lat = dt_data.Rows[0]["p_login_lat"].ToString();
        p_login_long = dt_data.Rows[0]["p_login_long"].ToString();
        p_logout_date_time = dt_data.Rows[0]["p_logout_date_time"].ToString();
        p_logout_lat = dt_data.Rows[0]["p_logout_lat"].ToString();
        p_logout_long = dt_data.Rows[0]["p_logout_long"].ToString();
        p_uploaded_lat = dt_data.Rows[0]["p_uploaded_lat"].ToString();
        p_uploaded_long = dt_data.Rows[0]["p_uploaded_long"].ToString();
        p_action_log_id = dt_data.Rows[0]["p_action_log_id"].ToString();
        p_device_id = dt_data.Rows[0]["p_device_id"].ToString();
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "etm.f_upload_login_log");
        MyCommand.Parameters.AddWithValue("p_log_id",Convert.ToInt64(p_log_id));
        MyCommand.Parameters.AddWithValue("p_user_id", p_user_id);
        MyCommand.Parameters.AddWithValue("p_roll", p_roll);
        MyCommand.Parameters.AddWithValue("p_login_date_time", p_login_date_time);
        MyCommand.Parameters.AddWithValue("p_login_lat", p_login_lat);
        MyCommand.Parameters.AddWithValue("p_login_long", p_login_long);
        MyCommand.Parameters.AddWithValue("p_logout_date_time", p_logout_date_time);
        MyCommand.Parameters.AddWithValue("p_logout_lat", p_logout_lat);
        MyCommand.Parameters.AddWithValue("p_logout_long", p_logout_long);
        MyCommand.Parameters.AddWithValue("p_uploaded_lat", p_uploaded_lat);
        MyCommand.Parameters.AddWithValue("p_uploaded_long", p_uploaded_long);
        MyCommand.Parameters.AddWithValue("p_action_log_id", Convert.ToInt64(p_action_log_id));
        MyCommand.Parameters.AddWithValue("p_device_id", p_device_id);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    #endregion

    #region "Upload Trips"
    public DataTable uploadTrips(string waybill, string tripno, DataTable dt_data)
    {
        string p_waybill_no, p_trip_no, p_strp_id, p_trip_direction, p_route_id, p_from_station_id, p_to_station_id, p_in_waybill_yn,
    p_start_complete_by, p_start_complete_datetime, p_start_complete_lat, p_start_complete_long, p_start_complete, p_uploaded_lat,
    p_uploaded_long, p_action_log_id, p_device_id,p_remark;

        p_trip_no = tripno;
        p_waybill_no = waybill;

        p_strp_id = dt_data.Rows[0]["p_strp_id"].ToString();
        p_trip_direction = dt_data.Rows[0]["p_trip_direction"].ToString();
        p_route_id = dt_data.Rows[0]["p_route_id"].ToString();
        p_from_station_id = dt_data.Rows[0]["p_from_station_id"].ToString();
        p_to_station_id = dt_data.Rows[0]["p_to_station_id"].ToString();
        p_in_waybill_yn = dt_data.Rows[0]["p_in_waybill_yn"].ToString();
        p_start_complete_by = dt_data.Rows[0]["p_start_complete_by"].ToString();
        p_start_complete_datetime = dt_data.Rows[0]["p_start_complete_datetime"].ToString();
        p_start_complete_lat = dt_data.Rows[0]["p_start_complete_lat"].ToString();
        p_start_complete_long = dt_data.Rows[0]["p_start_complete_long"].ToString();
        p_start_complete = dt_data.Rows[0]["p_start_complete"].ToString();
        p_uploaded_lat = dt_data.Rows[0]["p_uploaded_lat"].ToString();
        p_uploaded_long = dt_data.Rows[0]["p_uploaded_long"].ToString();
        p_action_log_id = dt_data.Rows[0]["p_action_log_id"].ToString();
        p_device_id = dt_data.Rows[0]["p_device_id"].ToString();
        p_remark = dt_data.Rows[0]["p_remark"].ToString();

        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "etm.f_upload_trips");
        MyCommand.Parameters.AddWithValue("p_waybill_no", p_waybill_no);
        MyCommand.Parameters.AddWithValue("p_trip_no", Convert.ToInt32(p_trip_no));
        MyCommand.Parameters.AddWithValue("p_strp_id", Convert.ToInt32(p_strp_id));
        MyCommand.Parameters.AddWithValue("p_trip_direction", p_trip_direction);
        MyCommand.Parameters.AddWithValue("p_route_id", Convert.ToInt32(p_route_id));
        MyCommand.Parameters.AddWithValue("p_from_station_id", Convert.ToInt32(p_from_station_id));
        MyCommand.Parameters.AddWithValue("p_to_station_id", Convert.ToInt32(p_to_station_id));
        MyCommand.Parameters.AddWithValue("p_in_waybill_yn", p_in_waybill_yn);
        MyCommand.Parameters.AddWithValue("p_start_complete_by", p_start_complete_by);
        MyCommand.Parameters.AddWithValue("p_start_complete_datetime", p_start_complete_datetime);
        MyCommand.Parameters.AddWithValue("p_start_complete_lat", p_start_complete_lat);
        MyCommand.Parameters.AddWithValue("p_start_complete_long", p_start_complete_long);
        MyCommand.Parameters.AddWithValue("p_start_complete", p_start_complete);
        MyCommand.Parameters.AddWithValue("p_uploaded_lat", p_uploaded_lat);
        MyCommand.Parameters.AddWithValue("p_uploaded_long", p_uploaded_long);
        MyCommand.Parameters.AddWithValue("p_action_log_id", Convert.ToInt64(p_action_log_id));
        MyCommand.Parameters.AddWithValue("p_device_id", p_device_id);
        MyCommand.Parameters.AddWithValue("p_remark", p_remark);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    #endregion

    #region "Upload Stage Change"
    public DataTable uploadStageChanges(string waybill, string tripno, DataTable dt_data)
    {
        string p_waybill_no, p_trip_no, p_strp_id, p_old_stage, p_new_stage, p_uploaded_lat, p_uploaded_long, p_action_log_id,
                p_change_by, p_change_datetime, p_change_lat, p_change_long, p_inspectionid, p_device_id;


        p_trip_no = tripno;
        p_waybill_no = waybill;
      

        p_old_stage = dt_data.Rows[0]["p_old_stage"].ToString();
        p_new_stage = dt_data.Rows[0]["p_new_stage"].ToString();

        p_uploaded_lat = dt_data.Rows[0]["p_uploaded_lat"].ToString();
        p_uploaded_long = dt_data.Rows[0]["p_uploaded_long"].ToString();
        p_action_log_id = dt_data.Rows[0]["p_action_log_id"].ToString();
        p_change_by = dt_data.Rows[0]["p_change_by"].ToString();
        p_change_datetime = dt_data.Rows[0]["p_change_datetime"].ToString();
        p_change_lat = dt_data.Rows[0]["p_change_lat"].ToString();
        p_change_long = dt_data.Rows[0]["p_change_long"].ToString();
        p_inspectionid = dt_data.Rows[0]["p_inspectionid"].ToString();
        p_device_id = dt_data.Rows[0]["p_device_id"].ToString();

        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "etm.f_upload_stage_changes");
        MyCommand.Parameters.AddWithValue("p_waybill_no", p_waybill_no);
        MyCommand.Parameters.AddWithValue("p_trip_no", Convert.ToInt32(p_trip_no));
        
        MyCommand.Parameters.AddWithValue("p_old_stage", Convert.ToInt32(p_old_stage));
        MyCommand.Parameters.AddWithValue("p_new_stage", Convert.ToInt32(p_new_stage));
        MyCommand.Parameters.AddWithValue("p_uploaded_lat", p_uploaded_lat);
        MyCommand.Parameters.AddWithValue("p_uploaded_long", p_uploaded_long);
        MyCommand.Parameters.AddWithValue("p_action_log_id", Convert.ToInt64(p_action_log_id));
        MyCommand.Parameters.AddWithValue("p_change_by", p_change_by);
        MyCommand.Parameters.AddWithValue("p_change_datetime", p_change_datetime);
        MyCommand.Parameters.AddWithValue("p_change_lat", p_change_lat);
        MyCommand.Parameters.AddWithValue("p_change_long", p_change_long);
        MyCommand.Parameters.AddWithValue("p_device_id", p_device_id);
        MyCommand.Parameters.AddWithValue("p_inspectionid", Convert.ToInt32(p_inspectionid));
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    #endregion

    #region "Upload Ticket"
    public DataTable uploadTicket(string waybill, string tripno, DataTable dt_data)
    {
        string p_waybill_no, p_trip_no, p_dsvc_id, p_strp_id, p_srtp_id, p_ticket_no, p_ticket_type, p_ticket_by, p_amount_total,
            p_quantity_total, p_payment_mode, p_payment_refno, p_ticket_by_code, p_ticket_status, p_from_station_code, p_to_station_code,
            p_inspection_id, p_count_adult, p_amount_adult, p_count_child, p_amount_child, p_luggage_weight, p_concession_type,
            p_discount_on_tax, p_discount_on_pass, p_concession_doctype, p_concession_proffno, p_penelty_amount, p_start_by, p_start_time,
            p_start_lat, p_start_long, p_start_remark, p_completed_by, p_completed_time, p_completed_lat, p_completed_long,
            p_completed_remark, p_uploaded_lat, p_uploaded_long, p_action_log_id, p_device_id, p_pg_id, p_pg_refno;

        p_trip_no = tripno;
        p_waybill_no = waybill;

        // p_waybill_no = dt_data.Rows[0]["p_waybill_no"].ToString();
        p_dsvc_id = dt_data.Rows[0]["p_dsvc_id"].ToString();
       
        p_srtp_id = dt_data.Rows[0]["p_srtp_id"].ToString();
        p_ticket_no = dt_data.Rows[0]["p_ticket_no"].ToString();
        p_ticket_type = dt_data.Rows[0]["p_ticket_type"].ToString();
        p_ticket_by = dt_data.Rows[0]["p_ticket_by"].ToString();
        p_amount_total = dt_data.Rows[0]["p_amount_total"].ToString();
        p_quantity_total = dt_data.Rows[0]["p_quantity_total"].ToString();
        p_payment_mode = dt_data.Rows[0]["p_payment_mode"].ToString();
        p_payment_refno = dt_data.Rows[0]["p_payment_refno"].ToString();
        p_ticket_by_code = dt_data.Rows[0]["p_ticket_by_code"].ToString();
        p_ticket_status = dt_data.Rows[0]["p_ticket_status"].ToString();
        p_from_station_code = dt_data.Rows[0]["p_from_station_code"].ToString();
        p_to_station_code = dt_data.Rows[0]["p_to_station_code"].ToString();
        p_inspection_id = dt_data.Rows[0]["p_inspection_id"].ToString();
        p_count_adult = dt_data.Rows[0]["p_count_adult"].ToString();
        p_amount_adult = dt_data.Rows[0]["p_amount_adult"].ToString();
        p_count_child = dt_data.Rows[0]["p_count_child"].ToString();
        p_amount_child = dt_data.Rows[0]["p_amount_child"].ToString();
        p_luggage_weight = dt_data.Rows[0]["p_luggage_weight"].ToString();
        p_concession_type = dt_data.Rows[0]["p_concession_type"].ToString();
        p_discount_on_tax = dt_data.Rows[0]["p_discount_on_tax"].ToString();
        p_discount_on_pass = dt_data.Rows[0]["p_discount_on_pass"].ToString();
        p_concession_doctype = dt_data.Rows[0]["p_concession_doctype"].ToString();
        p_concession_proffno = dt_data.Rows[0]["p_concession_proffno"].ToString();
        p_penelty_amount = dt_data.Rows[0]["p_penelty_amount"].ToString();
        p_start_by = dt_data.Rows[0]["p_start_by"].ToString();
        p_start_time = dt_data.Rows[0]["p_start_time"].ToString();
        p_start_lat = dt_data.Rows[0]["p_start_lat"].ToString();
        p_start_long = dt_data.Rows[0]["p_start_long"].ToString();
        p_start_remark = dt_data.Rows[0]["p_start_remark"].ToString();
        p_completed_by = dt_data.Rows[0]["p_completed_by"].ToString();
        p_completed_time = dt_data.Rows[0]["p_completed_time"].ToString();
        p_completed_lat = dt_data.Rows[0]["p_completed_lat"].ToString();
        p_completed_long = dt_data.Rows[0]["p_completed_long"].ToString();
        p_completed_remark = dt_data.Rows[0]["p_completed_remark"].ToString();
        p_uploaded_lat = dt_data.Rows[0]["p_uploaded_lat"].ToString();
        p_uploaded_long = dt_data.Rows[0]["p_uploaded_long"].ToString();
        p_action_log_id = dt_data.Rows[0]["p_action_log_id"].ToString();
        p_device_id = dt_data.Rows[0]["p_device_id"].ToString();
        p_pg_id = dt_data.Rows[0]["p_pg_id"].ToString();
        p_pg_refno = dt_data.Rows[0]["p_pg_refno"].ToString();

        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "etm.f_upload_ticket");
        MyCommand.Parameters.AddWithValue("p_waybill_no", p_waybill_no);
        MyCommand.Parameters.AddWithValue("p_trip_no", Convert.ToInt32(p_trip_no));
        MyCommand.Parameters.AddWithValue("p_dsvc_id", Convert.ToInt32(p_dsvc_id));
       
        MyCommand.Parameters.AddWithValue("p_srtp_id", Convert.ToInt32(p_srtp_id));
        MyCommand.Parameters.AddWithValue("p_ticket_no", p_ticket_no);
        MyCommand.Parameters.AddWithValue("p_ticket_type", p_ticket_type);
        MyCommand.Parameters.AddWithValue("p_ticket_by", p_ticket_by);
        MyCommand.Parameters.AddWithValue("p_amount_total", Convert.ToDecimal(p_amount_total));
        MyCommand.Parameters.AddWithValue("p_quantity_total", Convert.ToInt32(p_quantity_total));
        MyCommand.Parameters.AddWithValue("p_payment_mode", p_payment_mode);
        MyCommand.Parameters.AddWithValue("p_payment_refno", p_payment_refno);
        MyCommand.Parameters.AddWithValue("p_ticket_by_code", p_ticket_by_code);
        MyCommand.Parameters.AddWithValue("p_ticket_status", p_ticket_status);
        MyCommand.Parameters.AddWithValue("p_from_station_code", Convert.ToInt32(p_from_station_code));
        MyCommand.Parameters.AddWithValue("p_to_station_code", Convert.ToInt32(p_to_station_code));
        MyCommand.Parameters.AddWithValue("p_inspection_id", Convert.ToInt32(p_inspection_id));
        MyCommand.Parameters.AddWithValue("p_count_adult", Convert.ToInt32(p_count_adult));
        MyCommand.Parameters.AddWithValue("p_amount_adult", Convert.ToDecimal(p_amount_adult));
        MyCommand.Parameters.AddWithValue("p_count_child", Convert.ToInt32(p_count_child));
        MyCommand.Parameters.AddWithValue("p_amount_child", Convert.ToDecimal(p_amount_child));
        MyCommand.Parameters.AddWithValue("p_luggage_weight", Convert.ToDecimal(p_luggage_weight));
        MyCommand.Parameters.AddWithValue("p_concession_type", Convert.ToInt32(p_concession_type));
        MyCommand.Parameters.AddWithValue("p_discount_on_tax", Convert.ToDecimal(p_discount_on_tax));
        MyCommand.Parameters.AddWithValue("p_discount_on_pass", Convert.ToDecimal(p_discount_on_pass));
        MyCommand.Parameters.AddWithValue("p_concession_doctype", p_concession_doctype);
        MyCommand.Parameters.AddWithValue("p_concession_proffno", p_concession_proffno);
        MyCommand.Parameters.AddWithValue("p_penelty_amount", Convert.ToDecimal(p_penelty_amount));
        MyCommand.Parameters.AddWithValue("p_start_by", p_start_by);
        MyCommand.Parameters.AddWithValue("p_start_time", p_start_time);
        MyCommand.Parameters.AddWithValue("p_start_lat", p_start_lat);
        MyCommand.Parameters.AddWithValue("p_start_long", p_start_long);
        MyCommand.Parameters.AddWithValue("p_start_remark", p_start_remark);
        MyCommand.Parameters.AddWithValue("p_completed_by", p_completed_by);
        MyCommand.Parameters.AddWithValue("p_completed_time", p_completed_time);
        MyCommand.Parameters.AddWithValue("p_completed_lat", p_completed_lat);
        MyCommand.Parameters.AddWithValue("p_completed_long", p_completed_long);
        MyCommand.Parameters.AddWithValue("p_completed_remark", p_completed_remark);
        MyCommand.Parameters.AddWithValue("p_uploaded_lat", p_uploaded_lat);
        MyCommand.Parameters.AddWithValue("p_uploaded_long", p_uploaded_long);
        MyCommand.Parameters.AddWithValue("p_action_log_id", Convert.ToInt32(p_action_log_id));
        MyCommand.Parameters.AddWithValue("p_device_id", p_device_id);
        MyCommand.Parameters.AddWithValue("p_pg_id", p_pg_id);
        MyCommand.Parameters.AddWithValue("p_refno", p_pg_refno);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    #endregion

    #region "Upload inspection"
    public DataTable uploadInspection(string waybill, string inspectionid, DataTable dt_data)
    {
        string p_waybill_no, p_dsvc_id, p_trip_no, p_srtp_id, p_inspection_id, p_current_stage, p_new_stage,
    p_inspection_status, p_inspection_remark, p_count_tickets, p_inspection_collection,
    p_total_amount, p_penalty_amount, p_start_complete_by, p_start_complete_time, p_start_complete_lat,
    p_start_complete_long, p_start_complete_remark, p_start_complete,
    p_uploaded_lat, p_uploaded_long, p_action_log_id, p_device_id,pic1,pic2,pic3;

        p_waybill_no = waybill;
        p_dsvc_id = dt_data.Rows[0]["p_dsvc_id"].ToString();
        p_trip_no = dt_data.Rows[0]["p_trip_no"].ToString();
        p_srtp_id = dt_data.Rows[0]["p_srtp_id"].ToString();
        p_inspection_id = inspectionid;
        p_current_stage = dt_data.Rows[0]["p_current_stage"].ToString();
        p_new_stage = dt_data.Rows[0]["p_new_stage"].ToString();
        p_inspection_status = dt_data.Rows[0]["p_inspection_status"].ToString();
        p_inspection_remark = dt_data.Rows[0]["p_inspection_remark"].ToString();
        p_count_tickets = dt_data.Rows[0]["p_count_tickets"].ToString();
        p_inspection_collection = dt_data.Rows[0]["p_inspection_collection"].ToString();

        p_total_amount = dt_data.Rows[0]["p_total_amount"].ToString();
        p_penalty_amount = dt_data.Rows[0]["p_penalty_amount"].ToString();
        p_start_complete_by = dt_data.Rows[0]["p_start_complete_by"].ToString();
        p_start_complete_time = dt_data.Rows[0]["p_start_complete_time"].ToString();
        p_start_complete_lat = dt_data.Rows[0]["p_start_complete_lat"].ToString();
        p_start_complete_long = dt_data.Rows[0]["p_start_complete_long"].ToString();
        p_start_complete_remark = dt_data.Rows[0]["p_start_complete_remark"].ToString();
        p_start_complete = dt_data.Rows[0]["p_start_complete"].ToString();
        p_uploaded_lat = dt_data.Rows[0]["p_uploaded_lat"].ToString();
        p_uploaded_long = dt_data.Rows[0]["p_uploaded_long"].ToString();
        p_action_log_id = dt_data.Rows[0]["p_action_log_id"].ToString();
        p_device_id = dt_data.Rows[0]["p_device_id"].ToString();

pic1= dt_data.Rows[0]["p_pic1"].ToString();
        pic2= dt_data.Rows[0]["p_pic2"].ToString();
        pic3 = dt_data.Rows[0]["p_pic3"].ToString();
        byte[] pic1Bytes = Convert.FromBase64String(pic1);
        byte[] pic2Bytes = Convert.FromBase64String(pic2);
        byte[] pic3Bytes = Convert.FromBase64String(pic3);

        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "etm.f_upload_inspection");
        MyCommand.Parameters.AddWithValue("p_waybill_no", p_waybill_no);
        MyCommand.Parameters.AddWithValue("p_dsvc_id", Convert.ToInt32(p_dsvc_id));
        MyCommand.Parameters.AddWithValue("p_trip_no", Convert.ToInt32(p_trip_no));
        MyCommand.Parameters.AddWithValue("p_srtp_id", Convert.ToInt32(p_srtp_id));
        MyCommand.Parameters.AddWithValue("p_inspection_id", Convert.ToInt32(p_inspection_id));
        MyCommand.Parameters.AddWithValue("p_current_stage", Convert.ToInt32(p_current_stage));
        MyCommand.Parameters.AddWithValue("p_new_stage", Convert.ToInt32(p_new_stage));
        MyCommand.Parameters.AddWithValue("p_inspection_status", p_inspection_status);
        MyCommand.Parameters.AddWithValue("p_inspection_remark", p_inspection_remark);
        MyCommand.Parameters.AddWithValue("p_count_tickets", Convert.ToInt32(p_count_tickets));
        MyCommand.Parameters.AddWithValue("p_inspection_collection", Convert.ToDecimal(p_inspection_collection));
        MyCommand.Parameters.AddWithValue("p_total_amount", Convert.ToDecimal(p_total_amount));
        MyCommand.Parameters.AddWithValue("p_penalty_amount", Convert.ToDecimal(p_penalty_amount));
        MyCommand.Parameters.AddWithValue("p_start_complete_by", p_start_complete_by);
        MyCommand.Parameters.AddWithValue("p_start_complete_time", p_start_complete_time);
        MyCommand.Parameters.AddWithValue("p_start_complete_lat", p_start_complete_lat);
        MyCommand.Parameters.AddWithValue("p_start_complete_long", p_start_complete_long);
        MyCommand.Parameters.AddWithValue("p_start_complete_remark", p_start_complete_remark);
        MyCommand.Parameters.AddWithValue("p_start_complete", p_start_complete);
        MyCommand.Parameters.AddWithValue("p_uploaded_lat", p_uploaded_lat);
        MyCommand.Parameters.AddWithValue("p_uploaded_long", p_uploaded_long);
        MyCommand.Parameters.AddWithValue("p_action_log_id", Convert.ToInt64(p_action_log_id));
        MyCommand.Parameters.AddWithValue("p_device_id", p_device_id);
        
        MyCommand.Parameters.AddWithValue("p_pic1", (object)pic1Bytes ?? DBNull.Value);
        MyCommand.Parameters.AddWithValue("p_pic2", (object)pic2Bytes ?? DBNull.Value);
        MyCommand.Parameters.AddWithValue("p_pic3", (object)pic3Bytes ?? DBNull.Value);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    #endregion

    #region "Upload Scan Online Tickets"
    public DataTable uploadScanOnlineTicket(string waybill, string tripno, DataTable dt_data)
    {
        string p_waybill_no, p_qr_trip_no, p_qr_pnr, p_qr_journeydate, p_qr_amount, p_qr_totalseat, p_qr_dsvc_id,
    p_qr_srtp_id, p_scan_by, p_scan_datetime, p_scan_lat, p_scan_long, p_uploaded_by,
    p_uploaded_lat, p_uploaded_long, p_action_log_id, p_device_id,p_manual_entry;

        p_waybill_no = waybill;
        p_qr_trip_no = tripno;
        p_qr_pnr = dt_data.Rows[0]["p_qr_pnr"].ToString();
        p_qr_journeydate = dt_data.Rows[0]["p_qr_journeydate"].ToString();
        p_qr_amount = dt_data.Rows[0]["p_qr_amount"].ToString();
        p_qr_totalseat = dt_data.Rows[0]["p_qr_totalseat"].ToString();
        p_qr_dsvc_id = dt_data.Rows[0]["p_qr_dsvc_id"].ToString();
        p_qr_srtp_id = dt_data.Rows[0]["p_qr_srtp_id"].ToString();
        p_scan_by = dt_data.Rows[0]["p_scan_by"].ToString();
        p_scan_datetime = dt_data.Rows[0]["p_scan_datetime"].ToString();
        p_scan_lat = dt_data.Rows[0]["p_scan_lat"].ToString();
        p_scan_long = dt_data.Rows[0]["p_scan_long"].ToString();
        p_uploaded_by = dt_data.Rows[0]["p_uploaded_by"].ToString();

        p_uploaded_lat = dt_data.Rows[0]["p_uploaded_lat"].ToString();
        p_uploaded_long = dt_data.Rows[0]["p_uploaded_long"].ToString();
        p_action_log_id = dt_data.Rows[0]["p_action_log_id"].ToString();
        p_device_id = dt_data.Rows[0]["p_device_id"].ToString();
        p_manual_entry = dt_data.Rows[0]["p_manual_entry"].ToString();

        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "etm.f_upload_scan_online_ticket");
        MyCommand.Parameters.AddWithValue("p_waybill_no", p_waybill_no);
        MyCommand.Parameters.AddWithValue("p_qr_trip_no", Convert.ToInt32(p_qr_trip_no));
        MyCommand.Parameters.AddWithValue("p_qr_pnr", p_qr_pnr);
        MyCommand.Parameters.AddWithValue("p_qr_journeydate", p_qr_journeydate);
        MyCommand.Parameters.AddWithValue("p_qr_amount", Convert.ToDecimal(p_qr_amount));
        MyCommand.Parameters.AddWithValue("p_qr_totalseat", Convert.ToInt32(p_qr_totalseat));
        MyCommand.Parameters.AddWithValue("p_qr_dsvc_id", Convert.ToInt32(p_qr_dsvc_id));
        MyCommand.Parameters.AddWithValue("p_qr_srtp_id", Convert.ToInt32(p_qr_srtp_id));
        MyCommand.Parameters.AddWithValue("p_scan_by", p_scan_by);
        MyCommand.Parameters.AddWithValue("p_scan_datetime", p_scan_datetime);
        MyCommand.Parameters.AddWithValue("p_scan_lat", p_scan_lat);
        MyCommand.Parameters.AddWithValue("p_scan_long", p_scan_long);
        MyCommand.Parameters.AddWithValue("p_uploaded_by", p_uploaded_by);
        MyCommand.Parameters.AddWithValue("p_uploaded_lat", p_uploaded_lat);
        MyCommand.Parameters.AddWithValue("p_uploaded_long", p_uploaded_long);
        MyCommand.Parameters.AddWithValue("p_action_log_id", Convert.ToInt64(p_action_log_id));
        MyCommand.Parameters.AddWithValue("p_deviceid", p_device_id);
        MyCommand.Parameters.AddWithValue("p_manual_entry", p_manual_entry);

        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    #endregion

    #region "Upload Refunded Tickets"
    public DataTable uploadRefundedTickets(string waybill, string tripno, DataTable dt_data)
    {
        string p_waybill_no, p_trip_no,  p_ticket_no, p_refund_amount, p_refund_status, p_refund_datetime, p_refund_lat, p_refund_long,
    p_refund_remark, p_uploaded_lat, p_uploaded_long, p_action_log_id, p_transfer_yn, p_bus_no, p_device_id,p_payment_method,p_pg_id,p_pg_refno;

        p_waybill_no = waybill;
        p_trip_no = tripno;
        
        p_ticket_no = dt_data.Rows[0]["p_ticket_no"].ToString();
        p_refund_amount = dt_data.Rows[0]["p_refund_amount"].ToString();
        p_refund_status = dt_data.Rows[0]["p_refund_status"].ToString();
        p_refund_datetime = dt_data.Rows[0]["p_refund_datetime"].ToString();
        p_refund_lat = dt_data.Rows[0]["p_refund_lat"].ToString();
        p_refund_long = dt_data.Rows[0]["p_refund_long"].ToString();
        p_refund_remark = dt_data.Rows[0]["p_refund_remark"].ToString();
        p_uploaded_lat = dt_data.Rows[0]["p_uploaded_lat"].ToString();
        p_uploaded_long = dt_data.Rows[0]["p_uploaded_long"].ToString();
        p_action_log_id = dt_data.Rows[0]["p_action_log_id"].ToString();
        p_transfer_yn = dt_data.Rows[0]["p_transfer_yn"].ToString();
        p_bus_no = dt_data.Rows[0]["p_bus_no"].ToString();
        p_device_id = dt_data.Rows[0]["p_device_id"].ToString();
        p_payment_method= dt_data.Rows[0]["p_payment_method"].ToString();
        p_pg_id = dt_data.Rows[0]["p_pg_id"].ToString();
        p_pg_refno = dt_data.Rows[0]["p_pg_refno"].ToString();

        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "etm.f_upload_refund_tickets");
        MyCommand.Parameters.AddWithValue("p_waybill_no", p_waybill_no);
        
        MyCommand.Parameters.AddWithValue("p_ticket_no", p_ticket_no);
        MyCommand.Parameters.AddWithValue("p_refund_amount", Convert.ToDecimal(p_refund_amount));
        MyCommand.Parameters.AddWithValue("p_refund_status", p_refund_status);
        MyCommand.Parameters.AddWithValue("p_refund_datetime", p_refund_datetime);
        MyCommand.Parameters.AddWithValue("p_refund_lat", p_refund_lat);
        MyCommand.Parameters.AddWithValue("p_refund_long", p_refund_long);
        MyCommand.Parameters.AddWithValue("p_refund_remar", p_refund_remark);
        MyCommand.Parameters.AddWithValue("p_uploaded_lat", p_uploaded_lat);
        MyCommand.Parameters.AddWithValue("p_uploaded_long", p_uploaded_long);
        MyCommand.Parameters.AddWithValue("p_action_log_id", Convert.ToInt64(p_action_log_id));
        MyCommand.Parameters.AddWithValue("p_transfer_yn", p_transfer_yn);
        MyCommand.Parameters.AddWithValue("p_bus_no", p_bus_no);
        MyCommand.Parameters.AddWithValue("p_device_id", p_device_id);
        MyCommand.Parameters.AddWithValue("p_trip_no", Convert.ToInt32(tripno));
        MyCommand.Parameters.AddWithValue("p_payment_mode", p_payment_method);
        MyCommand.Parameters.AddWithValue("p_pg_id", p_pg_id);
        MyCommand.Parameters.AddWithValue("p_pg_refno", p_pg_refno);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    #endregion

    #region "Upload Action log"
    public DataTable getActionLogMaster(string deviceid, string latitude, string longitude, string mode)
    {
        MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "etm.f_get_actionlog_master");
        MyCommand.Parameters.AddWithValue("p_deviceid", deviceid);
        MyCommand.Parameters.AddWithValue("p_latitude", latitude);
        MyCommand.Parameters.AddWithValue("p_longitude", longitude);
        MyCommand.Parameters.AddWithValue("p_mode", mode);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    #endregion

    #region "get upload data count"
    public DataTable getuploaddatacount(string waybill)
    {
        MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "etm.f_get_upload_count");
        MyCommand.Parameters.AddWithValue("p_waybill_no", waybill);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    #endregion

    #region "get Online Tickets"
    public DataTable onlineTickets(string journey_date,string strp_id,string dsvc_id,string waybill,string downloadedby,string deviceid,string latitude,string longitude,string mode)
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "etm.f_get_tickets_for_etm");
        MyCommand.Parameters.AddWithValue("p_journey_date", journey_date);
        MyCommand.Parameters.AddWithValue("p_dsvc_id",Convert.ToInt64( dsvc_id));
        MyCommand.Parameters.AddWithValue("p_strp_id", Convert.ToInt64(strp_id));
        MyCommand.Parameters.AddWithValue("p_waybill", waybill);
        MyCommand.Parameters.AddWithValue("p_downloadedby", downloadedby);
        MyCommand.Parameters.AddWithValue("p_deviceid", deviceid);
        MyCommand.Parameters.AddWithValue("p_lat", latitude);
        MyCommand.Parameters.AddWithValue("p_long", longitude);
        MyCommand.Parameters.AddWithValue("p_mode", mode);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    #endregion

    #region "Data Upload"
    public DataTable allDataUpload(string waybillno)
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "etm.f_update_waybill_datacheck");
        MyCommand.Parameters.AddWithValue("p_waybill_no", waybillno);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
 public DataTable checkWaybillDataUpload(string waybillno)
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "etm.f_check_waybill_data_upload");
        MyCommand.Parameters.AddWithValue("p_dutyrefno", waybillno);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    #endregion
 public DataTable etmfree(string imeino)
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "etm.f_etm_free");
        MyCommand.Parameters.AddWithValue("imieno", imeino);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
public DataTable checketmstatus(string imeino)
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "etm.f_check_etm_status");
        MyCommand.Parameters.AddWithValue("imieno", imeino);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
public DataTable uploadetmdbfile(string data, string imei,string lat,string longi,string waybill,string filename,string contenttype,string updatedby)
    {
        byte[] datas = Convert.FromBase64String(data);
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "etm.fn_insertdbfile");
        MyCommand.Parameters.AddWithValue("datafile", datas);
        MyCommand.Parameters.AddWithValue("etm_imei", imei);
        MyCommand.Parameters.AddWithValue("lat", lat);
        MyCommand.Parameters.AddWithValue("longi", longi);
        MyCommand.Parameters.AddWithValue("waybill", waybill);
        MyCommand.Parameters.AddWithValue("filename", filename);
        MyCommand.Parameters.AddWithValue("contenttype", contenttype);
        MyCommand.Parameters.AddWithValue("updateby", updatedby);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
}