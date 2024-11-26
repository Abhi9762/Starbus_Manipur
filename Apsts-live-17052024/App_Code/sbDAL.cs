using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Npgsql;
using System.Data;
using System.Configuration;

/// <summary>
/// Summary description for sbDAL
/// </summary>
public class sbDAL
{
    public NpgsqlConnection connection = new NpgsqlConnection();
    private NpgsqlCommand cmd = new NpgsqlCommand();

    public void openconn()
    {
        try
        {
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["constr"].ToString();
            if (!(connection.State == ConnectionState.Open))
            {
                connection.Open();
            }
            else
            {
                connection.Close();
                connection.Open();
            }
        }
        catch (Exception ex)
        {
            if (connection.State != ConnectionState.Closed)
                connection.Close();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["constr"].ToString();
            connection.Open();
        }
    }

    public void closeconn()
    {
        if (connection.State != ConnectionState.Closed)
            connection.Close();
    }
}