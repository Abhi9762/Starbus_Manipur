using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using KeepAutomation.Barcode.Crystal;
using QRCoder;
using System.Drawing;
using System.IO;
using System.Xml;


/// <summary>
/// Summary description for sbCancellationVoucher
/// </summary>
public class sbCancellationVoucher
{
    NpgsqlCommand MyCommand = new NpgsqlCommand();
    private sbBLL bll = new sbBLL();
    DataTable MyTable = new DataTable();
    sbValidation _validation = new sbValidation();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    private sbCommonFunc _common = new sbCommonFunc();
    string html = "";

    

    public sbCancellationVoucher()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    public string GetCancellationVoucher(string Cancl_refno)
    {
        try
        {

            sbXMLdata data = new sbXMLdata();
            string deptname = data.loadDeptName();
            string host = HttpContext.Current.Request.Url.Host;

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_get_cancellationDetails");
            MyCommand.Parameters.AddWithValue("p_cancl_refno", Cancl_refno);
            MyTable = bll.SelectAll(MyCommand);
            if (MyTable.TableName == "Success")
            {
                if (MyTable.Rows.Count > 0)
                {
                    html = "<!DOCTYPE html PUBLIC ' -//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'><html xmlns='http://www.w3.org/1999/xhtml'>";
                    html += "<head><title>Cancellation Voucher</title>";
                    html += "<script language='javascript' type='text/javascript'>";
                    html += " function printDiv(divID) {";
                    html += " var divElements = document.getElementById(divID).innerHTML;";
                    html += "var oldPage = document.body.innerHTML;";
                    html += " document.body.innerHTML ='<html><head></head><body>'+divElements + '</body>';";
                    html += "window.print();";
                    html += "document.body.innerHTML = oldPage;";
                    html += "}";
                    html += "</script>";
                    html += "<style> .button1 {border - radius: 4px;width: 200px;height: 27px; font-size: 14px;padding: 0;font-weight: bold;}</style>";
                    html += "</head>";
                    html += "<body><form name='form1' method='post' action='' id='form1' style='width: 820px;height:450px;'>";
                    html += "<div style='float:none;text-align: center;padding-bottom: 4px;'>";
                    html += "<input type ="+"button"+" onclick="+"printDiv('tnap_Div')"+" value='Print' class='button1' /></div>";
                    html += "<div id='tnap_Div'  style=' border: thin solid #000000;'>";

                    html += " <table style='width:800px;'><tr>";
                    html += "<td align='left' style='width:20%;'><img src='Logo/DeptLogo.png' width='54%'/>";
                    html += "</td>";
                    html += "<td align='center' style='width:60%;'>";
                    html += "<span id='Label1' style='font-family:Verdana;font-size:Large;font-weight:bold;'>"+ deptname + "<br/>Cancellation Voucher</span>";
                    html += "</td>";
                    html += "<td align='right' style='width:20%;'>";
                    html += "<img src='Logo/GovtLogo.png' width='54%'/></td>";
                    html += "</tr> </table>";

                    html += "<table style='border: thin solid #000000; width: 800px; margin-left: 8px;line-height: 25px;'>";
                    html += "<tr>";
                    html += "<td class='style3' align='right'>";
                    html += "<span Font-Size = '8pt' Font-Names = 'Verdana' >PNR NO.</ span >";
                    html += "</td>";
                    html += "<td class='style4'>";
                    html += "<span Font-Size='8pt' Font-Names='Verdana'>" + MyTable.Rows[0]["ticket_no"].ToString() + "</span>";
                    html += "</td>";
                    html += "<td class='width: 15px;'>";
                    html += "</td>";
                    html += "<td class='style4' align ='right'>";
                    html += "<span Font-Size='8pt' Font-Names='Verdana'>Journey Date</span>";
                    html += "</td>";
                    html += "<td>";
                    html += "<span Font-Size='8pt' Font-Names='Verdana'>" + MyTable.Rows[0]["journey_date"].ToString() + "</span>";
                    html += "</td>";
                    html += "</tr>";
                    html += "<tr>";
                    html += "<td class='style3' align='right'>";
                    html += "<span Font-Size='8pt' Font-Names='Verdana'>Source</span>";
                    html += "</td>";
                    html += "<td class='style4'>";
                    html += "<span Font-Size='8pt' Font-Names='Verdana'>" + MyTable.Rows[0]["stnsource"].ToString() + "</span>";
                    html += "</td>";
                    html += "<td style = 'width: 15px;'>";
                    html += "</td>";
                    html += "<td class='style4' align='right'>";
                    html += "<span Font-Size='8pt' Font-Names='Verdana'>Destination</span>";
                    html += "</td>";
                    html += "<td>";
                    html += "<span Font-Size='8pt' Font-Names='Verdana'>" + MyTable.Rows[0]["stndestination"].ToString() + "</span>";
                    html += "<td>";
                    html += "</tr>";
                    html += "<tr>";
                    html += "<td class='style3' align='right'>";
                    html += "<span Font-Size='8pt' Font-Names='Verdana'>Service</span>";
                    html += "</td>";
                    html += "<td class='style4'>";
                    html += "<span Font-Size='8pt' Font-Names='Verdana'>" + MyTable.Rows[0]["service_type_name_en"].ToString() + "</span>";
                    html += "</td>";
                    html += "<td style = 'width: 15px;'>";
                    html += "</td>";
                    html += "<td class='style4' align='right'>";
                    html += "<span Font-Size='8pt' Font-Names='Verdana'>Refund Amount(Rs.)</span>";
                    html += "</td>";
                    html += "<td>";
                    html += "<span Font-Size='8pt' Font-Names='Verdana'>" + MyTable.Rows[0]["cancellation_amt"].ToString() + "</span>";
                    html += "</td>";
                    html += "</tr>";
                    html += "<tr>";
                    html += "<td class='style3' align='right'>";
                    html += "<span Font-Size='8pt' Font-Names='Verdana'>Cancellation Date</span>";
                    html += "</td>";
                    html += "<td class='style4'>";
                    html += "<span Font-Size='8pt' Font-Names='Verdana'>" + MyTable.Rows[0]["cancellation_date"].ToString() + "</span>";
                    html += "</td>";
                    html += "<td style = 'width: 15px;'>";
                    html += "</td>";
                    html += "<td class='style4' align='right'>";
                    html += "<span Font-Size='8pt' Font-Names='Verdana'>Cancellation Ref No.</span>";
                    html += "</td>";
                    html += "<td>";
                    html += "<span Font-Size='8pt' Font-Names='Verdana'>" + MyTable.Rows[0]["cancellation_ref_no"].ToString() + "</span>";
                    html += "</td>";
                    html += "</tr>";
                    html += "<tr>";
                    html += "<td class='style3' align='right'>";
                    html += "<span Font-Size='8pt' Font-Names='Verdana'>Cancelled By</span>";
                    html += "</td>";
                    html += "<td class='style4'>";
                    html += "<span  Font-Size='8pt' Font-Names='Verdana'>" + MyTable.Rows[0]["cancelled_by_refno"].ToString() + "</span>";
                    html += "</td>";
                    html += "</tr>";
                    html += "</table>";

                    html += "<div style='text-align:center;'><span id='Label10' style='font-family:Verdana;font-size:Small;font-weight:bold;'>Detail of Seat(s)</span></div>";
                    html += "<table class='style1' style='text-align: center;border: thin solid Black;font-family: Verdana;font-size: 8pt;text-align: left;table-layout: auto;border-collapse: collapse;border-spacing: 1px;width: 800px;margin-left: 8px;' border='1' cellspacing='1'>";
                    html += "<tr style = 'background -color: #CCCCCC; border: thin solid #000000; line-height: normal; vertical-align: baseline; text-align: center;'>";
                    html += "<td style ='width: 50px;' >"; 
                    html += "<span style = 'font-family:Verdana;font-size:8pt;font-weight:bold;'> Sl No. </ span >";
                    html += "</td>";
                    html += "<td style = 'width: 200px;'>";
                    html += "<span style = 'font-family:Verdana;font-size:8pt;font-weight:bold;'> Name of Passenger </ span >";
                    html += "</td>";
                    html += "<td style = 'width: 100px;'>";
                    html += "<span style = 'font-family:Verdana;font-size:8pt;font-weight:bold;'> Gender  </ span >";
                    html += "</td>";
                    html += "<td style = 'width: 100px;'>";
                    html += "<span style = 'font-family:Verdana;font-size:8pt;font-weight:bold;'> Age  </ span >";
                    html += "</td>";
                    html += "<td style = 'width: 100px;'>";
                    html += "<span style = 'font-family:Verdana;font-size:8pt;font-weight:bold;'> Seat No.  </ span >";
                    html += "</td>";
                    html += "</tr>";
                    string cc = cancelseats(MyTable.Rows[0]["cancellation_ref_no"].ToString());
                    html += cc;
                    html += "</table>";
                    html += "<div style='text-align: center;'><br /><br />";
                    html += "<span  style = 'font-family:Verdana;font-size:10pt;' >  Please visit us again, and feel to contact us for your ticket booking requirements in future.  </ span >";
                    html += "<br />";
                    html += "<span  style = 'font-family:Verdana;font-size:10pt;' >  We welcome your comments, suggestions and feedback. Thanking you.  </ span >";
                    html += "<br /><br />";
                    html += "<span  style = 'font-family:Verdana;font-size:10pt;' > "+ host + " </ span >";
                    html += "<br />";
                    html += "<br />";
                    html += "<br /></ div >";
                    html += "</form></body>";
                    return html;
                }
            }

            return "";
        }
        catch (Exception ex)
        {
            return "";
        }
    }

    

    private string cancelseats(string cancl_refno)
    {
        try
        {
            DataTable dtPassengerDetails = new DataTable();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_get_cancel_seats_Details");
            MyCommand.Parameters.AddWithValue("p_cancl_refno", cancl_refno);
            dtPassengerDetails = bll.SelectAll(MyCommand);
            if (dtPassengerDetails.TableName == "Success")
            {
                if (dtPassengerDetails.Rows.Count > 0)
                {
                    int i = 0;
                    string html1 = "";
                    foreach (DataRow drow in dtPassengerDetails.Rows)
                    {
                        i = i + 1;

                        html1 += "<tr style = 'background -color: #CCCCCC; border: thin solid #000000; line-height: normal; vertical-align: baseline; text-align: center;'>";
                        html1 += "<td style ='width: 50px;'>";
                        html1 += "<span style='font-family:Verdana;font-size:8pt;'> " + i + " </ span >";
                        html1 += "</td>";
                        html1 += "<td style = 'width: 200px;'>";
                        html1 += "<span style='font-family:Verdana;font-size:8pt;'> " + drow["traveller_name"] + " </ span >";
                        html1 += "</td>";
                        html1 += "<td style = 'width: 100px;'>";
                        if (drow["traveller_gender"].ToString() == "F")
                            html1 += "<span style='font-family:Verdana;font-size:8pt;'> Female  </ span >";
                        else if (drow["traveller_gender"].ToString() == "M")
                            html1 += "<span style='font-family:Verdana;font-size:8pt;'> Male  </ span >";
                        else
                            html1 += "<span style='font-family:Verdana;font-size:8pt;'>" + drow["traveller_gender"] + "</span></td>";
                      
                        html1 += "</td>";
                        html1 += "<td style = 'width: 100px;'>";
                        html1 += "<span style='font-family:Verdana;font-size:8pt;'> " + drow["traveller_age"] + "</ span >";
                        html1 += "</td>";
                        html1 += "<td style = 'width: 100px;'>";
                        html1 += "<span style='font-family:Verdana;font-size:8pt;'> " + drow["seat_no"] + "</ span >";
                        html1 += "</td>";
                        html1 += "</tr>";
                        
                    }
                    return html1;
                }
            }

            return "";
        }
        catch (Exception ex)
        {
            return "";
        }
    }
}