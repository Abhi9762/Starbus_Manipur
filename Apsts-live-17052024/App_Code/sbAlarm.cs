using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for sbAlarm
/// </summary>
public class sbAlarm
{
    private DataTable dtNothing = new DataTable();
    private NpgsqlCommand MyCommand;
    private sbBLL bll = new sbBLL();
    
    public DataTable getAlarmCategories()
    {
        try
        {

            MyCommand = new NpgsqlCommand();
            DataTable dt = new DataTable();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.alarm_get_categories");
            dt = bll.SelectAll(MyCommand);
            return dt;
            
        }
        catch (Exception ex)
        {
            return dtNothing;
        }
    }
    public DataTable saveAlarm_dt(string alarmTypeId, string reportedBy, string latt, string longg, string ticketNo)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            DataTable dt = new DataTable();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.alarm_report");
            MyCommand.Parameters.AddWithValue("p_alarmtypeid", int.Parse( alarmTypeId));
            MyCommand.Parameters.AddWithValue("p_reportedby", reportedBy );
            MyCommand.Parameters.AddWithValue("p_latt", latt );
            MyCommand.Parameters.AddWithValue("p_longg", longg );
            MyCommand.Parameters.AddWithValue("p_ticketno", ticketNo );
            dt = bll.SelectAll(MyCommand);
            return dt;

        }
        catch (Exception ex)
        {
            return dtNothing;
        }
    }
    public DataTable getAlarm()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            DataTable dt = new DataTable();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.alarm_get");
            dt = bll.SelectAll(MyCommand);
            return dt;
        }
        catch (Exception ex)
        {
            return dtNothing;
        }
    }
    public DataTable getAlarmCounts()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            DataTable dt = new DataTable();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.alarm_get_counts");
            dt = bll.SelectAll(MyCommand);
            return dt;
        }
        catch (Exception ex)
        {
            return dtNothing;
        }
    }
    
}