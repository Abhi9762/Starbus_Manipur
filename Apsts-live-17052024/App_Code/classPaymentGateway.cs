using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for classPaymentGateway
/// </summary>
public class classPaymentGateway
{
    private NpgsqlCommand MyCommand;
    DataTable dtNothing = new DataTable();
    private sbBLL bll = new sbBLL();
    public DataTable getActivePaymentGateways_dt()
    {
        try
        {
            DataTable dt;
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "PAYMENT_GATEWAY.GET_active_PG");
            dt = bll.SelectAll(MyCommand);


            return dt;
        }
        catch (Exception ex)
        {
            return dtNothing;
        }
    }
    public DataTable getPGResponseURL(string pgId)
    {
        try
        {
            DataTable dt;
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "PAYMENT_GATEWAY.GET_PG_RESPONSEPAGE");
            MyCommand.Parameters.AddWithValue("p_pgId", pgId);
            dt = bll.SelectAll(MyCommand);
            return dt;
        }
        catch (Exception ex)
        {
            return dtNothing;
        }
    }
}