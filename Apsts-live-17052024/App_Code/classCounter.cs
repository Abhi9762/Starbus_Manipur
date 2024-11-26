using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// Summary description for classCounter
/// </summary>
public class classCounter
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    string e_key = "Utc#ver4@2022Key";
    public classCounter()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public DataTable GetDepotYN(string counterid, string action)
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        DataTable dt;
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "current_booking.f_get_depotcurntyn");
        MyCommand.Parameters.AddWithValue("spcntrid", counterid);
        MyCommand.Parameters.AddWithValue("spaction", "C");
        dt = bll.SelectAll(MyCommand);

        return dt;
    }
    public DataTable GetServiceType()
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        DataTable dt;
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_servicetype");
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    public DataTable GetDepotList()
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        DataTable dt;
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_depot_list");
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    public DataTable GetBusList(string depotid, string servicetypeid)
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        DataTable dt;
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "current_booking.get_buses");
        MyCommand.Parameters.AddWithValue("p_depot", depotid);
        MyCommand.Parameters.AddWithValue("p_servicetypeid", Convert.ToInt32(servicetypeid));
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    public DataTable GetBusRoute(string servicetypeid, string stationid)
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        DataTable dt;
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "current_booking.get_buses_route");
        MyCommand.Parameters.AddWithValue("p_servicetypeid", Convert.ToInt32(servicetypeid));
        MyCommand.Parameters.AddWithValue("p_stationid", Convert.ToInt32(stationid));
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    public DataTable GetTrips(string servicetypeid, string stationid, string routeid)
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        DataTable dt;
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "current_booking.get_route_trips");
        MyCommand.Parameters.AddWithValue("p_stationid", Convert.ToInt32(stationid));
        MyCommand.Parameters.AddWithValue("p_routeid", Convert.ToInt32(routeid));
        MyCommand.Parameters.AddWithValue("p_servicetypeid", Convert.ToInt32(servicetypeid));
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    public DataTable GetConductor(string depotid)
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        DataTable dt;
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "current_booking.get_conductor");
        MyCommand.Parameters.AddWithValue("p_depotid", depotid);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    public DataTable GetDriver(string depotid)
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        DataTable dt;
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "current_booking.get_driver");
        MyCommand.Parameters.AddWithValue("p_depotid", depotid);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    public DataTable GetConcession(string servicetypeid, string stationid, string routeid)
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        DataTable dt;
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "current_booking.get_concession");
        MyCommand.Parameters.AddWithValue("p_servicetypecode", Convert.ToInt32(servicetypeid));
        MyCommand.Parameters.AddWithValue("p_routeid", Convert.ToInt32(routeid));
        MyCommand.Parameters.AddWithValue("p_fromstation", Convert.ToInt32(stationid));
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    public DataTable GetStations(string strpid, string stationid, Int64 routeid, string concessiontype)
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        DataTable dt;
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "current_booking.f_get_waybills_station");
        MyCommand.Parameters.AddWithValue("p_strp_id", Convert.ToInt64(strpid));
        MyCommand.Parameters.AddWithValue("p_ston_id", Convert.ToInt64(stationid));
        MyCommand.Parameters.AddWithValue("p_rout_id", routeid);
        MyCommand.Parameters.AddWithValue("p_concession_id", Convert.ToInt64(concessiontype));
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    public DataTable GetDepotServiceStations(string strpid, string stationid, Int64 dsvcid, string concessiontype)
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        DataTable dt;
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "current_booking.f_get_depotservice_trip_station");
        MyCommand.Parameters.AddWithValue("p_strp_id", Convert.ToInt64(strpid));
        MyCommand.Parameters.AddWithValue("p_ston_id", Convert.ToInt64(stationid));
        MyCommand.Parameters.AddWithValue("p_dsvc_id", dsvcid);
        MyCommand.Parameters.AddWithValue("p_concession_id", Convert.ToInt64(concessiontype));
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    public DataTable GetRowColumn(string registration_no, string tripdate)
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        DataTable dt;
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_get_rowcolumn_cntr");
        MyCommand.Parameters.AddWithValue("p_registration_no", registration_no);
        MyCommand.Parameters.AddWithValue("p_tripdate", tripdate);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    public DataTable GetTotalRowColumn(string registration_no)
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        DataTable dt;
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_get_totrowcolumn_cntr");
        MyCommand.Parameters.AddWithValue("p_registration_no", registration_no);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    public DataTable GetIdType()
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        DataTable dt;
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "current_booking.get_idtype");
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    public DataTable CheckBusPass(string p_concession, string _passnumber, string _journeyDate)
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        DataTable dt;
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_check_passnumber");
        MyCommand.Parameters.AddWithValue("p_passnumber", _passnumber);
        MyCommand.Parameters.AddWithValue("p_journeydate", _journeyDate);
        MyCommand.Parameters.AddWithValue("p_concession", Convert.ToInt64(p_concession));
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    public DataTable TirpOpenYN(string waybillno)
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        DataTable dt;
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_tripopenyn");
        MyCommand.Parameters.AddWithValue("p_waybillno", waybillno);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    public DataTable CloseTrip(string tripcode, string counterid)
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        DataTable dt;
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_close_trip");
        MyCommand.Parameters.AddWithValue("p_tripcode", tripcode);
        MyCommand.Parameters.AddWithValue("p_cntrid", counterid);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    public DataTable GetOpenTripDetails(string tripcode, string counterid)
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        DataTable dt;
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "current_booking.get_opentripDetails");
        MyCommand.Parameters.AddWithValue("p_cntrid", counterid);
        MyCommand.Parameters.AddWithValue("p_tripcode", tripcode);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    public DataTable GetWaybillList(string stationid)
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        DataTable dt;
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_get_gen_waybills");
        MyCommand.Parameters.AddWithValue("p_staionid", Convert.ToInt64(stationid));
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    public DataTable GetWaybillDetails(string waybillno)
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        DataTable dt;
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "current_booking.get_waybilldetails");
        MyCommand.Parameters.AddWithValue("p_waybillno", waybillno);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    public DataTable GetWaybillTrips(string waybillno,string stationid)
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        DataTable dt;
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "current_booking.fn_get_waybill_trips");
        MyCommand.Parameters.AddWithValue("p_waybillno", waybillno);
        MyCommand.Parameters.AddWithValue("p_stationid", stationid);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    public DataTable GetDepotServiceTrips(string dsvcid, string stationid)
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        DataTable dt;
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "current_booking.fn_get_depotservice_trips");
        MyCommand.Parameters.AddWithValue("p_dsvcid", dsvcid);
        MyCommand.Parameters.AddWithValue("p_stationid", stationid);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    public DataTable GetDepotServices(string servicetype, string stationid)
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        DataTable dt;
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "current_booking.fn_get_depot_service_list");
        MyCommand.Parameters.AddWithValue("p_servicetype", servicetype);
        MyCommand.Parameters.AddWithValue("p_stationid", stationid);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    public DataTable SaveTicket(string waybillno, Int64 servicetypeid, string busregistrationno, string conductorid,
        string departime, Int64 routeid, Int64 tripid, Int64 from_station_code, Int64 to_station_code,
        string trip_date, decimal received_amt, decimal refund_amt, decimal total_fare,
        decimal fare_per_seat, string booked_by_type_code, string booked_by_user_code, string booking_mode,
        Int64 boarding_station_code, int total_seats_booked, string passenger, string updatedby, string ip_imei,
        string concessionId, string onlineVerificationYN, string custpassNumber, string idVerificationYN, string idVerification,
        string documentVerificationYN, string documentVerification, string specialYn, string Book_sub_mode, string payment_mode)
    {
        try
        {
            string[] CustomerList = passenger.Split('|');

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "current_booking.insert_curnt_tkt");
            MyCommand.Parameters.AddWithValue("p_waybillno", waybillno);
            MyCommand.Parameters.AddWithValue("p_servicetypeid", servicetypeid);
            MyCommand.Parameters.AddWithValue("p_busregistrationno", busregistrationno);
            MyCommand.Parameters.AddWithValue("p_conductorid", conductorid);
            MyCommand.Parameters.AddWithValue("p_departime", departime);
            MyCommand.Parameters.AddWithValue("p_routeid", routeid);
            MyCommand.Parameters.AddWithValue("p_tripid", tripid);
            MyCommand.Parameters.AddWithValue("p_from_station_code", from_station_code);
            MyCommand.Parameters.AddWithValue("p_to_station_code", to_station_code);
            MyCommand.Parameters.AddWithValue("p_trip_date", trip_date);
            MyCommand.Parameters.AddWithValue("p_received_amt", received_amt);
            MyCommand.Parameters.AddWithValue("p_refund_amt", refund_amt);
            MyCommand.Parameters.AddWithValue("p_total_fare", total_fare);
            MyCommand.Parameters.AddWithValue("p_fare_per_seat", fare_per_seat);
            MyCommand.Parameters.AddWithValue("p_booked_by_type_code", booked_by_type_code);
            MyCommand.Parameters.AddWithValue("p_booked_by_user_code", booked_by_user_code);
            MyCommand.Parameters.AddWithValue("p_booking_mode", booking_mode);
            MyCommand.Parameters.AddWithValue("p_boarding_station_code", boarding_station_code);
            MyCommand.Parameters.AddWithValue("p_total_seats_booked", total_seats_booked);

            for (int i = 0; i < total_seats_booked; i++)
            {
                string[] customerDetail = CustomerList[i].Split(',');
                MyCommand.Parameters.AddWithValue("p_seatno_" + (i + 1), Convert.ToInt64(customerDetail[0].ToString()));
                if (!string.IsNullOrEmpty(customerDetail[1]))
                {
                    string idnumbermask = "";

                    if (String.IsNullOrEmpty(customerDetail[4].ToString()) == false)
                    {
                        idnumbermask = customerDetail[4].Substring(customerDetail[4].Length - 4).ToString();
                    }
                    MyCommand.Parameters.AddWithValue("p_name_" + (i + 1), customerDetail[1].ToString());
                    MyCommand.Parameters.AddWithValue("p_idtype_" + (i + 1), Convert.ToInt32(customerDetail[2]));
                    MyCommand.Parameters.AddWithValue("p_idnumber_" + (i + 1), Encrypt(customerDetail[3].ToString()));
                    MyCommand.Parameters.AddWithValue("p_idnumbermask_" + (i + 1), idnumbermask);
                    MyCommand.Parameters.AddWithValue("p_spclrefnumber_" + (i + 1), customerDetail[5].ToString());
                }
                else
                {
                    MyCommand.Parameters.AddWithValue("p_name_" + (i + 1), "");
                    MyCommand.Parameters.AddWithValue("p_idtype_" + (i + 1), 0);
                    MyCommand.Parameters.AddWithValue("p_idnumber_" + (i + 1), "");
                    MyCommand.Parameters.AddWithValue("p_idnumbermask_" + (i + 1), "");
                    MyCommand.Parameters.AddWithValue("p_spclrefnumber_" + (i + 1), "");
                }
            }

            for (int i = total_seats_booked; i <= 5; i++)
            {
                MyCommand.Parameters.AddWithValue("p_seatno_" + Convert.ToInt64(i + 1), Convert.ToInt64(0));
                MyCommand.Parameters.AddWithValue("p_name_" + (i + 1), "");
                MyCommand.Parameters.AddWithValue("p_idtype_" + (i + 1), 0);
                MyCommand.Parameters.AddWithValue("p_idnumber_" + (i + 1), "");
                MyCommand.Parameters.AddWithValue("p_idnumbermask_" + (i + 1), "");
                MyCommand.Parameters.AddWithValue("p_spclrefnumber_" + (i + 1), "");
            }

            MyCommand.Parameters.AddWithValue("p_updatedby", updatedby.ToString());
            MyCommand.Parameters.AddWithValue("p_ipaddress", ip_imei);

            MyCommand.Parameters.AddWithValue("p_concessionid", concessionId);
            MyCommand.Parameters.AddWithValue("p_onlineverificationyn", onlineVerificationYN);
            MyCommand.Parameters.AddWithValue("p_custpassnumber", custpassNumber);
            MyCommand.Parameters.AddWithValue("p_idverificationyn", idVerificationYN);
            MyCommand.Parameters.AddWithValue("p_idverification", idVerification);
            MyCommand.Parameters.AddWithValue("p_documentverificationyn", documentVerificationYN);
            MyCommand.Parameters.AddWithValue("p_documentverification", documentVerification);
            MyCommand.Parameters.AddWithValue("p_specialyn", specialYn);
            MyCommand.Parameters.AddWithValue("p_book_sub_mode", Book_sub_mode);
            MyCommand.Parameters.AddWithValue("p_payment_mode", payment_mode);
           

            dt = bll.SelectAll(MyCommand);
            sbCommonFunc _common = new sbCommonFunc();
            _common.ErrorLog("Table Name", dt.TableName);
            if (dt.TableName == "Success" && dt.Rows.Count > 0)
            {
                return dt;
            }
        }
        catch (Exception ex)
        {
            sbCommonFunc _common = new sbCommonFunc();
            _common.ErrorLog("Table Name", ex.Message);
        }
        return dt;
    }
    public DataTable SaveTicketWaybill(string waybillno, Int64 servicetypeid, string busregistrationno, string conductorid,
        string conductorid2, string driverid, string driverid2,
        string departime, Int64 routeid, Int64 tripid, Int64 from_station_code, Int64 to_station_code,
        string trip_date, decimal received_amt, decimal refund_amt, decimal total_fare,
        decimal fare_per_seat, string booked_by_type_code, string booked_by_user_code, string booking_mode,
        Int64 boarding_station_code, int total_seats_booked, string passenger, string updatedby, string ip_imei,
        string concessionId, string onlineVerificationYN, string custpassNumber, string idVerificationYN, string idVerification,
        string documentVerificationYN, string documentVerification, string specialYn, string Book_sub_mode, string payment_mode, string withwaybill)
    {
        try
        {
            string[] CustomerList = passenger.Split('|');

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "current_booking.insert_curnt_tkt_waybill");
            MyCommand.Parameters.AddWithValue("p_waybillno", waybillno);
            MyCommand.Parameters.AddWithValue("p_servicetypeid", servicetypeid);
            MyCommand.Parameters.AddWithValue("p_busregistrationno", busregistrationno);
            MyCommand.Parameters.AddWithValue("p_conductorid", conductorid);
            MyCommand.Parameters.AddWithValue("p_conductorid2", conductorid2);
            MyCommand.Parameters.AddWithValue("p_driver", driverid);
            MyCommand.Parameters.AddWithValue("p_driver2", driverid2);
            MyCommand.Parameters.AddWithValue("p_departime", departime);
            MyCommand.Parameters.AddWithValue("p_routeid", routeid);
            MyCommand.Parameters.AddWithValue("p_tripid", tripid);
            MyCommand.Parameters.AddWithValue("p_from_station_code", from_station_code);
            MyCommand.Parameters.AddWithValue("p_to_station_code", to_station_code);
            MyCommand.Parameters.AddWithValue("p_trip_date", trip_date);
            MyCommand.Parameters.AddWithValue("p_received_amt", received_amt);
            MyCommand.Parameters.AddWithValue("p_refund_amt", refund_amt);
            MyCommand.Parameters.AddWithValue("p_total_fare", total_fare);
            MyCommand.Parameters.AddWithValue("p_fare_per_seat", fare_per_seat);
            MyCommand.Parameters.AddWithValue("p_booked_by_type_code", booked_by_type_code);
            MyCommand.Parameters.AddWithValue("p_booked_by_user_code", booked_by_user_code);
            MyCommand.Parameters.AddWithValue("p_booking_mode", booking_mode);
            MyCommand.Parameters.AddWithValue("p_boarding_station_code", boarding_station_code);
            MyCommand.Parameters.AddWithValue("p_total_seats_booked", total_seats_booked);

            for (int i = 0; i < total_seats_booked; i++)
            {
                string[] customerDetail = CustomerList[i].Split(',');
                MyCommand.Parameters.AddWithValue("p_seatno_" + (i + 1), Convert.ToInt64(customerDetail[0].ToString()));
                if (!string.IsNullOrEmpty(customerDetail[1]))
                {
                    string idnumbermask = "";

                    if (String.IsNullOrEmpty(customerDetail[4].ToString()) == false)
                    {
                        idnumbermask = customerDetail[4].Substring(customerDetail[4].Length - 4).ToString();
                    }
                    MyCommand.Parameters.AddWithValue("p_name_" + (i + 1), customerDetail[1].ToString());
                    MyCommand.Parameters.AddWithValue("p_idtype_" + (i + 1), Convert.ToInt32(customerDetail[2]));
                    MyCommand.Parameters.AddWithValue("p_idnumber_" + (i + 1), Encrypt(customerDetail[3].ToString()));
                    MyCommand.Parameters.AddWithValue("p_idnumbermask_" + (i + 1), idnumbermask);
                    MyCommand.Parameters.AddWithValue("p_spclrefnumber_" + (i + 1), customerDetail[5].ToString());
                }
                else
                {
                    MyCommand.Parameters.AddWithValue("p_name_" + (i + 1), "");
                    MyCommand.Parameters.AddWithValue("p_idtype_" + (i + 1), 0);
                    MyCommand.Parameters.AddWithValue("p_idnumber_" + (i + 1), "");
                    MyCommand.Parameters.AddWithValue("p_idnumbermask_" + (i + 1), "");
                    MyCommand.Parameters.AddWithValue("p_spclrefnumber_" + (i + 1), "");
                }
            }

            for (int i = total_seats_booked; i <= 5; i++)
            {
                MyCommand.Parameters.AddWithValue("p_seatno_" + Convert.ToInt64(i + 1), Convert.ToInt64(0));
                MyCommand.Parameters.AddWithValue("p_name_" + (i + 1), "");
                MyCommand.Parameters.AddWithValue("p_idtype_" + (i + 1), 0);
                MyCommand.Parameters.AddWithValue("p_idnumber_" + (i + 1), "");
                MyCommand.Parameters.AddWithValue("p_idnumbermask_" + (i + 1), "");
                MyCommand.Parameters.AddWithValue("p_spclrefnumber_" + (i + 1), "");
            }

            MyCommand.Parameters.AddWithValue("p_updatedby", updatedby.ToString());
            MyCommand.Parameters.AddWithValue("p_ipaddress", ip_imei);

            MyCommand.Parameters.AddWithValue("p_concessionid", concessionId);
            MyCommand.Parameters.AddWithValue("p_onlineverificationyn", onlineVerificationYN);
            MyCommand.Parameters.AddWithValue("p_custpassnumber", custpassNumber);
            MyCommand.Parameters.AddWithValue("p_idverificationyn", idVerificationYN);
            MyCommand.Parameters.AddWithValue("p_idverification", idVerification);
            MyCommand.Parameters.AddWithValue("p_documentverificationyn", documentVerificationYN);
            MyCommand.Parameters.AddWithValue("p_documentverification", documentVerification);
            MyCommand.Parameters.AddWithValue("p_specialyn", specialYn);
            MyCommand.Parameters.AddWithValue("p_book_sub_mode", Book_sub_mode);
            MyCommand.Parameters.AddWithValue("p_payment_mode", payment_mode);
            MyCommand.Parameters.AddWithValue("p_with_waybill", withwaybill);
            dt = bll.SelectAll(MyCommand);
            sbCommonFunc _common = new sbCommonFunc();
            _common.ErrorLog("Table Name", dt.TableName);
            if (dt.TableName == "Success" && dt.Rows.Count > 0)
            {
                return dt;
            }
        }
        catch (Exception ex)
        {
            sbCommonFunc _common = new sbCommonFunc();
            _common.ErrorLog("Table Name", ex.Message);
        }
        return dt;
    }
    public DataTable ConfirmTicket(string ticket_no, decimal received_amt, decimal refund_amt)
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        DataTable dt;
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "current_booking.confirm_curnt_tkt");
        MyCommand.Parameters.AddWithValue("p_ticketno", ticket_no);
        MyCommand.Parameters.AddWithValue("p_received_amt", received_amt);
        MyCommand.Parameters.AddWithValue("p_refund_amt", refund_amt);
        dt = bll.SelectAll(MyCommand);
        return dt;
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
    public DataTable matchLayout(string busNo, string strpId)
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        DataTable dt;
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "current_booking.f_match_layout_srv_bus");
        MyCommand.Parameters.AddWithValue("p_bus_no", busNo);
        MyCommand.Parameters.AddWithValue("p_strp_id", Int64.Parse(strpId));
        dt = bll.SelectAll(MyCommand);
        return dt;
    }

}

