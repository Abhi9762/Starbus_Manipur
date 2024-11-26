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
/// Summary description for sbCommonFunc
/// </summary>
public class sbCommonFunc
{
    sbBLL bll = new sbBLL();
    // NpgsqlCommand MyCommand = new NpgsqlCommand();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    public sbCommonFunc()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public void ErrorLog(string errcode, string errdesc)
    {
        try
        {
            string MpathFolder;
            MpathFolder = AppDomain.CurrentDomain.BaseDirectory + @"Errors\";
            // PathDirection = ApplicationException
            // Dim directory As New IO.DirectoryInfo("~/Errors/")
            var directory = new DirectoryInfo(MpathFolder);

            //foreach (FileInfo file in directory.GetFiles())
            //{
            //    if (file.Extension.Equals(".txt") && (DateTime.Now - file.CreationTime).Days > 7)
            //    {
            //        file.Delete();
            //    }
            //}
            string current_date = DateTime.Now.ToString("dd") + "-" + DateTime.Now.ToString("MM") + "-" + DateTime.Now.ToString("yyyy");

            string path = "~/Errors/" + current_date + ".txt";
	   
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

    public string getCancellationpolicy()
    {
        try
        {


            string html = "";
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_get_cancellation_policy");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    html += "<ul class='list-group' style='font-size: 13px; margin-left:20px; font-family: verdana; color: #b10021;margin-bottom: 0px;'>";
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        if ((dt.Rows[i]["refund_type"].ToString() == "P") && (!DBNull.Value.Equals(dt.Rows[i]["hrs_to"])))
                        {
                            html += "<li> From " + dt.Rows[i]["hrs_to"].ToString() + " Hrs To " + dt.Rows[i]["hrs_from"].ToString() + " Hrs before the scheduled departure time " + dt.Rows[i]["deductamount"].ToString() + "% amount will be deducted.</li> ";
                        }
                        else if ((dt.Rows[i]["refund_type"].ToString() == "R") && (!DBNull.Value.Equals(dt.Rows[i]["hrs_to"])))
                        {
                            html += "<li> From " + dt.Rows[i]["hrs_to"].ToString() + " Hrs To " + dt.Rows[i]["hrs_from"].ToString() + " Hrs before the scheduled departure time " + dt.Rows[i]["deductamount"].ToString() + "₹ amount will be deducted from total amount.</li> ";
                        }
                        else if ((dt.Rows[i]["refund_type"].ToString() == "P") && (DBNull.Value.Equals(dt.Rows[i]["hrs_to"])))
                        {
                            html += "<li> After " + dt.Rows[i]["hrs_from"].ToString() + " Hrs from the scheduled departure time " + dt.Rows[i]["deductamount"].ToString() + "% amount will be deducted from total amount.</li> ";
                        }
                        else if ((dt.Rows[i]["refund_type"].ToString() == "R") && (DBNull.Value.Equals(dt.Rows[i]["hrs_to"])))
                        {
                            html += "<li> After " + dt.Rows[i]["hrs_from"].ToString() + " Hrs from the scheduled departure time " + dt.Rows[i]["deductamount"].ToString() + "₹ amount will be deducted from total amount.</li> ";
                        }
                        html += "<br/>";
                    }
                    html += "</ul>";
                    return html;
                }
                else
                {
                    return "Cancellation slabs are not available. Please contact to helpdesk.";
                }
            }
            else
            {
                return "Cancellation slabs are not available. Please contact to helpdesk."+ dt.TableName;
            }
        }
        catch (Exception ex)
        {
            return "Cancellation slabs are not available. Please contact to helpdesk."+ex.Message;
        }
    }
    
    public DataTable getofficecontact(int ofclvl)
    {
        DataTable dt = new DataTable();
        try
        {

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_officecontact");
            MyCommand.Parameters.AddWithValue("p_ofclvl", ofclvl);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    return dt;
                }
            }
            return dt;
        }
        catch(Exception ex)
        {
            return dt;
        }

    }
}