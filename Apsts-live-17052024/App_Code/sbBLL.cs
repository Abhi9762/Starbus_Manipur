using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Npgsql;
using System.Data;

/// <summary>
/// Summary description for sbBLL
/// </summary>
public class sbBLL
{
    private sbDAL MyConnection = new sbDAL();
    private NpgsqlCommand MyCommand = new NpgsqlCommand();
    private NpgsqlDataAdapter MydataAdaptor = new NpgsqlDataAdapter();

    public DataTable SelectAll(NpgsqlCommand cmdParameters)
    {
        DataTable SelectAllRet;
        MyCommand.Parameters.Clear();
        MyCommand = cmdParameters;
        MyCommand.CommandType = CommandType.StoredProcedure;
        MyCommand.CommandText = MyCommand.Parameters["StoredProcedure"].Value.ToString();
        MyCommand.Parameters.RemoveAt(MyCommand.Parameters.IndexOf("@StoredProcedure"));
        MyCommand.CommandTimeout = 500;
        MyCommand.Connection = MyConnection.connection;
        var ds = new DataSet();
        MydataAdaptor.SelectCommand = MyCommand;
        SelectAllRet = new DataTable();
        try
        {
            MyConnection.openconn();
            MydataAdaptor.Fill(SelectAllRet);
            SelectAllRet.TableName = "Success";
        }
        catch (Exception e)
        {
            // SelectAll.TableName = "Error"
            SelectAllRet.TableName = e.Message;
        }
        finally
        {
            // Cleanup command and connection objects.
            MyCommand.Dispose();
            MyConnection.closeconn();
        }

        return SelectAllRet;
    }

    public DataTable InsertandReturn(NpgsqlCommand cmdParameters)
    {
        DataTable InsertandReturnRet;
        MyCommand = cmdParameters;
        MyCommand.CommandType = CommandType.StoredProcedure;
        MyCommand.CommandText = MyCommand.Parameters["StoredProcedure"].Value.ToString();
        MyCommand.Parameters.RemoveAt(MyCommand.Parameters.IndexOf("@StoredProcedure"));
        MyCommand.CommandTimeout = 500;
        MyCommand.Connection = MyConnection.connection;
        var ds = new DataSet();
        MydataAdaptor.SelectCommand = MyCommand;
        InsertandReturnRet = new DataTable();
        try
        {
            MyConnection.openconn();
            MydataAdaptor.Fill(InsertandReturnRet);
            InsertandReturnRet.TableName = "Success";
        }
        catch (Exception e)
        {
            // SelectAll.TableName = "Error"
            InsertandReturnRet.TableName = e.Message;
        }
        finally
        {
            // Cleanup command and connection objects.
            MyCommand.Dispose();
            MyConnection.closeconn();
        }

        return InsertandReturnRet;
    }

    public string InsertAll(NpgsqlCommand cmdParameters)
    {
        MyCommand = cmdParameters;
        MyCommand.CommandType = CommandType.StoredProcedure;
        MyCommand.CommandText = MyCommand.Parameters["StoredProcedure"].Value.ToString();
        MyCommand.Parameters.RemoveAt(MyCommand.Parameters.IndexOf("@StoredProcedure"));
        string result = string.Empty;
        MyCommand.CommandTimeout = 500;
        MyCommand.Connection = MyConnection.connection;
        try
        {
            MyConnection.openconn();
            result = Convert.ToString(MyCommand.ExecuteScalar());
            return "Success";
        }
        catch (Exception e)
        {
            result = e.Message.ToString();
        }
        finally
        {
            // Cleanup command and connection objects.
            MyCommand.Dispose();
            MyConnection.closeconn();
        }

        return result;
    }

    public string UpdateAll(NpgsqlCommand cmdParameters)
    {
        string result = string.Empty;
        MyCommand = cmdParameters;
        MyCommand.CommandType = CommandType.StoredProcedure;
        MyCommand.CommandText = MyCommand.Parameters["StoredProcedure"].Value.ToString();
        MyCommand.Parameters.RemoveAt(MyCommand.Parameters.IndexOf("@StoredProcedure"));
        MyCommand.CommandTimeout = 500;
        MyCommand.Connection = MyConnection.connection;
        try
        {
            MyConnection.openconn();
            result = Convert.ToString(MyCommand.ExecuteScalar());
            return "Success";
        }
        catch (Exception e)
        {
            result = e.Message;
        }
        // result = "error"
        finally
        {
            // Cleanup command and connection objects.
            MyCommand.Dispose();
            MyConnection.closeconn();
        }

        return result;
    }

    public string DeleteAll(NpgsqlCommand cmdParameters)
    {
        string result = string.Empty;
        MyCommand = cmdParameters;
        MyCommand.CommandType = CommandType.StoredProcedure;
        switch (MyCommand.Parameters["StoredProcedure"].Value.ToString())
        {
            case "spVehicleRegistrationDelete":
                {
                    MyCommand.Parameters.RemoveAt(MyCommand.Parameters.IndexOf("@StoredProcedure"));
                    MyCommand.CommandText = "spVehicleRegistrationDelete";
                    break;
                }
        }

        MyCommand.CommandTimeout = 500;
        MyCommand.Connection = MyConnection.connection;
        try
        {
            MyConnection.openconn();
            result = Convert.ToString(MyCommand.ExecuteScalar());
            return result;
        }
        catch (Exception e)
        {
            result = "error";
        }
        finally
        {
            // Cleanup command and connection objects.
            MyCommand.Dispose();
            MyConnection.closeconn();
        }

        return result;
    }
}