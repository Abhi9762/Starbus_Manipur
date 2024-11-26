using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for classAlarm
/// </summary>
public class classAlarm
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    public classAlarm()
    {
        
    }

    //Alarm
    public DataTable getAlarmCounts_CR()
    {
        MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "travelleralarm.GET_ALARMS_COUNTS_CR");
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    public DataTable getAlarm_CR(int startMinutes,int endMinutes)
    {
        MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "travelleralarm.GET_ALARMS_CR");
        MyCommand.Parameters.AddWithValue("p_start_minutes", startMinutes);
        MyCommand.Parameters.AddWithValue("p_end_minutes", endMinutes);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }

    //Grievance
    public DataTable getGrievanceCount_CR()
    {
        MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "traveller_grievance.GET_GRIEVANCE_COUNTS_CR");
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    public DataTable getGrievances_CR(string status)
    {
        MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "traveller_grievance.GET_GRIEVANCES_CR");
        MyCommand.Parameters.AddWithValue("p_status", status);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    public DataTable getGrievanceDetail(string refno)
    {
        MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "traveller_grievance.GET_GRIEVANCE_DETAIL");
        MyCommand.Parameters.AddWithValue("p_refno", refno);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    public DataTable GetGRefTrackDetails(string refno)
    {
        MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "traveller_grievance.GRI_Track");
        MyCommand.Parameters.AddWithValue("p_refno", refno);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
}