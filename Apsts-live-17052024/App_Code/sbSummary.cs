using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Npgsql;
using System.Net;
using System.Data;

/// <summary>
/// Summary description for sbSummary
/// </summary>
public class sbSummary
{
    sbBLL bll = new sbBLL();
    // NpgsqlCommand MyCommand = new NpgsqlCommand();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    public sbSummary()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public DataTable getRouteBooking(int no_of_record, string recordtype)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "summary.f_get_route_booking");
            MyCommand.Parameters.AddWithValue("p_noofbooking", no_of_record);
            MyCommand.Parameters.AddWithValue("p_recordtype", recordtype);
            DataTable dtt = new DataTable();
            dtt = bll.SelectAll(MyCommand);
            return dtt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public DataTable getMonthofconductor(string year, string month)
    {
        try
        {
           MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.conductor_rating");
            MyCommand.Parameters.AddWithValue("p_month",Convert.ToInt32( month));
            MyCommand.Parameters.AddWithValue("p_year", Convert.ToInt32(year));
            DataTable dtt = new DataTable();
            dtt = bll.SelectAll(MyCommand);
            return dtt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}