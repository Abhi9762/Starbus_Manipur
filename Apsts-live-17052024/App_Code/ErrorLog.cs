using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Npgsql;
using System.Net;
using System.Data;
using System.IO;
using System.Globalization;

/// <summary>
/// Summary description for ErrorLog
/// </summary>
public class ErrorLog
{
    public ErrorLog()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public void Error_Log(string errcode, string errdesc)
    {
        try
        {
            string MpathFolder;
            MpathFolder = AppDomain.CurrentDomain.BaseDirectory + @"Errors\PageError\";
            // PathDirection = ApplicationException
            // Dim directory As New IO.DirectoryInfo("~/Errors/")
            var directory = new DirectoryInfo(MpathFolder);

           

            string path = "~/Errors/PageError/" + DateTime.Today.ToString("dd-mm-yy") + ".txt";
            if (!File.Exists(System.Web.HttpContext.Current.Server.MapPath(path)))
            {
                File.Create(System.Web.HttpContext.Current.Server.MapPath(path)).Close();
            }
            using (var w = File.AppendText(System.Web.HttpContext.Current.Server.MapPath(path)))
            {
                w.WriteLine(System.Environment.NewLine + "Log Entry : ");
                w.WriteLine("{0}", DateTime.Now.ToString(CultureInfo.InvariantCulture));
                string err = "Error In: " + System.Web.HttpContext.Current.Request.Url.ToString() + "|  Error Code :" + errcode + "| Error Message:" + errdesc;
                w.WriteLine(err);
                w.WriteLine("__________________________");
                w.Flush();
                w.Close();
                // WriteError("ErrorHandler", ex.Message)
            };


            //MyCommand = new NpgsqlCommand();
            //MyCommand.Parameters.Clear();
            // string Mresult = "";
            // MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.sperrorloginsert");
            // MyCommand.Parameters.AddWithValue("p_errorcode", errcode);
            // MyCommand.Parameters.AddWithValue("p_errordesc", errdesc);
            // Mresult = bll.UpdateAll(MyCommand);
            // if (Mresult == "Success")
            // {

            // }
        }
        catch (Exception ex)
        {

        }
    }

}