using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using KeepAutomation.Barcode.Crystal;
using QRCoder;
using System.Configuration;
using System.Drawing;
using System.IO;

/// <summary>
/// Summary description for BusPass
/// </summary>
public class BusPass
{
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    string html = "";
    public BusPass()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public string GetReceiptHTML(string currtranrefno)
    {
        try
        {
            // ReplicationID = ConfigurationManager.AppSettings("ReplicationID")
            // getdenralconfig()

            DataTable dt;
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "buspass.Pass_pmt_receipt");
            MyCommand.Parameters.AddWithValue("p_currtranrefno", currtranrefno);
            
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    html = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"><html xmlns=\"http://www.w3.org/1999/xhtml\">";
                    html += "<head><title>Bus Pass Payment Receipt</title>";
                    html += "<script language=\"javascript\" type=\"text/javascript\">";
                    html += " function printDiv(divID) {";
                    html += " var divElements = document.getElementById(divID).innerHTML;";
                    html += "var oldPage = document.body.innerHTML;";
                    html += " document.body.innerHTML =\"<html><head></head><body>\"+divElements + \"</body>\";";
                    html += "window.print();";
                    html += "document.body.innerHTML = oldPage;";
                    html += "}";
                    html += "</script> ";
                    html += "<style> .button1 {background: transparent; border: 1px solid #d80d0d; border-radius: 4px; color: #d80d0d; font-size: 19px; cursor: pointer;}";


                    html += ".style1 {margin-left:10px; width: 800px;}";
                    html += ".style3 {width: 130px;}";
                    html += ".style4 {width: 110px;}";
                    html += ".style5 {width:160px;}";
                    html += "tr.example td {order-style:solid;border-color:red;border-width: 1px;padding: 10px;}";
                    html += "tr.test td {border-style:solid;border-color:Black;border-width: 1px;text-align:left;}";
                    html += ".lblAlign{	text-align:right;}";
                    html += ".style6{width: 110px;}";
                    html += ".style7{width: 115px;}";
                    html += ".style8{width: 127px;}";
                    html += ".style9{width: 279px;}";
                    html += "</style>";
                    html += "</head>";
                    html += "<body><form name=\"form1\" method=\"post\" action=\"e_Ticket.aspx\" id=\"form1\" style=\"  width:500px; height:450px;\">";
                    html += "<div style=\"font-size: 20px; padding-bottom: 5px; text-align: right;\">";
                    html += "<input type =\"button\" onclick=\"printDiv('tnap_Div')\" value=\"Print Receipt\" class=\"button1\" /></div>";
                    html += "<div id=\"tnap_Div\"  style=\" border: thin solid #000000;\">";
                    html += "<table style=\"width:500px;\"><tr>";
                    html += "<td align=\"left\" style=\"width:50px;\"><img src=\"../Logo/DeptLogo.png\" style=\"width:50px;\" />";
                    html += "<td align=\"center\" style=\"width:400px;\">";
                    html += "<span id=\"Label1\" style=\"font-family:Verdana;line-height: 19px;\"><span style='font-size: 9pt;'>ARUNACHAL PRADESH STATE TRANSPORT SERVICES</span><br/><span  style='font-size: 10pt;font-weight: bold;'>PAYMENT RECEIPT<span></span>";
                    html += "</td>";
                    html += "<td align=\"right\" style=\"width:50px;\">";
                    html += "";
                    html += "</td>";
                    html += "</tr></table>";

                    // html += " <div><img src=""http://utconline.uk.gov.in/images/ticket_new.jpg"" style=""width:100%;"" /> </div>"

                    // ***************Neeraj Chnage***************************

                    html += "<table style=\"width:100%;\"><tr>";
                    html += "<td align=\"left\" style=\"width:5%;\">";
                    html += "<td align=\"center\" style=\"width:90%;\">";
                    html += "<span id=\"Label1\" style=\"font-family:Verdana;\">Rs. <b>" + dt.Rows[0]["TXN_AMOUNT"].ToString() + "</b> has been received as fare/payment towards your application Number <b>" + dt.Rows[0]["CURRTRANREFNO"].ToString() + "</b> for <b>" + dt.Rows[0]["buspasstype_name"].ToString() + "</b>.</span>";
                    html += "</td>";
                    html += "<td align=\"right\" style=\"width:5%;\">";
                    html += "";
                    html += "</td>";
                    html += "</tr>";
                    html += "<tr>";
                    html += "<td align=\"left\" style=\"width:5%;\">";
                    html += "<td align=\"center\" style=\"width:90%;\">";
                    html += "<span id='lblNotice1' style='font-family:Verdana;font-size:8pt;padding-top: 10px;'>For Check Your Application Status Please Visit <b> apstsarn.gov.in</b></span>";
                    html += "</td>";
                    html += "<td align=\"left\" style=\"width:5%;\">";
                    html += "</tr>";
                    html += "</table>";

                    // ****************End*************************************

                    html += "</div></form></body></html>";
                    return html;
                }
            }
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
        return "";
    }

    public string GetReceiptHTMLOutside(string currtranrefno)
    {
        try
        {
            // ReplicationID = ConfigurationManager.AppSettings("ReplicationID")
            // getdenralconfig()

            DataTable dt;
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "buspass.Pass_pmt_receipt");
            MyCommand.Parameters.AddWithValue("p_currtranrefno", currtranrefno);

            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    html = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"><html xmlns=\"http://www.w3.org/1999/xhtml\">";
                    html += "<head><title>Bus Pass Payment Receipt</title>";
                    html += "<script language=\"javascript\" type=\"text/javascript\">";
                    html += " function printDiv(divID) {";
                    html += " var divElements = document.getElementById(divID).innerHTML;";
                    html += "var oldPage = document.body.innerHTML;";
                    html += " document.body.innerHTML =\"<html><head></head><body>\"+divElements + \"</body>\";";
                    html += "window.print();";
                    html += "document.body.innerHTML = oldPage;";
                    html += "}";
                    html += "</script> ";
                    html += "<style> .button1 {background: transparent; border: 1px solid #d80d0d; border-radius: 4px; color: #d80d0d; font-size: 19px; cursor: pointer;}";


                    html += ".style1 {margin-left:10px; width: 800px;}";
                    html += ".style3 {width: 130px;}";
                    html += ".style4 {width: 110px;}";
                    html += ".style5 {width:160px;}";
                    html += "tr.example td {order-style:solid;border-color:red;border-width: 1px;padding: 10px;}";
                    html += "tr.test td {border-style:solid;border-color:Black;border-width: 1px;text-align:left;}";
                    html += ".lblAlign{	text-align:right;}";
                    html += ".style6{width: 110px;}";
                    html += ".style7{width: 115px;}";
                    html += ".style8{width: 127px;}";
                    html += ".style9{width: 279px;}";
                    html += "</style>";
                    html += "</head>";
                    html += "<body><form name=\"form1\" method=\"post\" action=\"e_Ticket.aspx\" id=\"form1\" style=\"  width:500px; height:450px;\">";
                    html += "<div style=\"font-size: 20px; padding-bottom: 5px; text-align: right;\">";
                    html += "<input type =\"button\" onclick=\"printDiv('tnap_Div')\" value=\"Print Receipt\" class=\"button1\" /></div>";
                    html += "<div id=\"tnap_Div\"  style=\" border: thin solid #000000;\">";
                    html += "<table style=\"width:500px;\"><tr>";
                    html += "<td align=\"left\" style=\"width:50px;\"><img src=\"Logo/DeptLogo.png\" style=\"width:50px;\" />";
                    html += "<td align=\"center\" style=\"width:400px;\">";
                    html += "<span id=\"Label1\" style=\"font-family:Verdana;line-height: 19px;\"><span style='font-size: 9pt;'>ARUNACHAL PRADESH STATE TRANSPORT SERVICES</span><br/><span  style='font-size: 10pt;font-weight: bold;'>PAYMENT RECEIPT<span></span>";
                    html += "</td>";
                    html += "<td align=\"right\" style=\"width:50px;\">";
                    html += "";
                    html += "</td>";
                    html += "</tr></table>";

                    // html += " <div><img src=""http://utconline.uk.gov.in/images/ticket_new.jpg"" style=""width:100%;"" /> </div>"

                    // ***************Neeraj Chnage***************************

                    html += "<table style=\"width:100%;\"><tr>";
                    html += "<td align=\"left\" style=\"width:5%;\">";
                    html += "<td align=\"center\" style=\"width:90%;\">";
                    html += "<span id=\"Label1\" style=\"font-family:Verdana;\">Rs. <b>" + dt.Rows[0]["TXN_AMOUNT"].ToString() + "</b> has been received as fare/payment towards your application Number <b>" + dt.Rows[0]["CURRTRANREFNO"].ToString() + "</b> for <b>" + dt.Rows[0]["buspasstype_name"].ToString() + "</b>.</span>";
                    html += "</td>";
                    html += "<td align=\"right\" style=\"width:5%;\">";
                    html += "";
                    html += "</td>";
                    html += "</tr>";
                    html += "<tr>";
                    html += "<td align=\"left\" style=\"width:5%;\">";
                    html += "<td align=\"center\" style=\"width:90%;\">";
                    html += "<span id='lblNotice1' style='font-family:Verdana;font-size:8pt;padding-top: 10px;'>For Check Your Application Status Please Visit <b> apstsarn.gov.in</b></span>";
                    html += "</td>";
                    html += "<td align=\"left\" style=\"width:5%;\">";
                    html += "</tr>";
                    html += "</table>";

                    // ****************End*************************************

                    html += "</div></form></body></html>";
                    return html;
                }
            }
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
        return "";
    }
    public string GetPassHTML(string Passno)
    {
        try
        {

            DataTable dt;
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "buspass.Pass_Print");
            MyCommand.Parameters.AddWithValue("p_passno", Passno);
            
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    html = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"><html xmlns=\"http://www.w3.org/1999/xhtml\">";
                    html += "<head><title>Bus Pass</title>";
                    html += "<script language=\"javascript\" type=\"text/javascript\">";
                    html += " function printDiv(divID) {";
                    html += " var divElements = document.getElementById(divID).innerHTML;";
                    html += "var oldPage = document.body.innerHTML;";
                    html += " document.body.innerHTML =\"<html><head></head><body>\"+divElements + \"</body>\";";
                    html += "window.print();";
                    html += "document.body.innerHTML = oldPage;";
                    html += "}";
                    html += "</script> ";
                    html += "<style> .button1 { background: transparent; border: 1px solid #d80d0d;border-radius: 4px;color: #d80d0d;  font-size: 19px;cursor: pointer;}";

                    html += ".style1 {margin-left:10px; width: 800px;}";
                    html += ".style3 {width: 130px;}";
                    html += ".style4 {width: 110px;}";
                    html += ".style5 {width:160px;}";
                    html += "tr.example td {order-style:solid;border-color:red;border-width: 1px;padding: 10px;}";
                    html += "tr.test td {border-style:solid;border-color:Black;border-width: 1px;text-align:left;}";
                    html += ".lblAlign{	text-align:right;}";
                    html += ".style6{width: 110px;}";
                    html += ".style7{width: 115px;}";
                    html += ".style8{width: 127px;}";
                    html += ".style9{width: 279px;}";
                    html += "</style>";
                    html += "</head>";
                    html += "<body>";
                    html += " <form name = \"form1\" method=\"post\" action=\"Bus_Pass.aspx\" id=\"form1\" style=\"width: 1000px; height: 450px;\">";
                    html += "<div style = \"font-size: 20px; padding-bottom: 5px; text-align: Right();\" >";
                    html += " <input type=\"button\" onclick=\"printDiv('tnap_Div')\" value=\"Print Pass\" class=\"button1\">";
                    html += " </div>";
                    html += "<div id = \"tnap_Div\" Class=\"watermark\">";
                    html += "<table style = \"border: thin solid #000000; width: 1000px;\">";
                    html += "<tr>";
                    html += "<td style=\"width: 480px;\">";
                    html += "<table style=\"width: 500px;\">";
                    html += "<tr>";
                    html += "<td align=\"center\" style=\"width: 100px;\">";
                    html += "<img id=\"imggovt\" src=\"../Logo/DeptLogo.png\" style=\"width: 70px;\">";
                    html += "</td>";
                    html += "<td align=\"center\" style=\"width: 400px;\"><span id=\"Label1\" style=\"font-family: Verdana; line-height: 19px;\"><span style=\"font-size: 11pt;\">ARUNACHAL PRADESH STATE TRANSPORT SERVICES</span><br>";
                    html += "<span style=\"font-size: 10pt; font-weight: bold;\">";
                    html += "<span id=\"lblpasstype\">" + dt.Rows[0]["BUSPASS_CATEGORY_NAME"].ToString() + " - " + dt.Rows[0]["buspasstype_name"].ToString() + "</span>";
                    html += "</span></span>";
                    html += "<br>";
                    html += "<span style=\"font-size: 10pt; font-weight: bold;\">";
                    html += "<span id=\"lblpassno\">Pass Number - " + dt.Rows[0]["PASSNUMBER"].ToString() + "</span>";
                    html += "</span>";
                    html += "</td>";
                    html += "</tr>";
                    html += "</table>";
                    html += "<table style=\"width: 500px;\">";
                    html += "<tr>";
                    html += "<td align=\"center\" style=\"width: 100px;\">";
                    html += "<table>";
                    html += "<tr>";
                    html += "<td align=\"center\">";
                    string PersonImg = GetImage(dt.Rows[0]["PHOTO"]);
                    html += "<img id=\"imgphoto\" src=" + PersonImg + " style=\"border-width: 0px; border: 1px solid; width: 100px; height: 80px;\">";
                    html += "</td>";
                    html += "</tr>";
                    html += "<tr>";
                    html += "<td align=\"center\">";
                    html += "<br />";
                    html += "</td>";
                    html += "</tr>";
                    html += "<tr>";
                    html += "<td align=\"center\">";
                    string qrcodeString = "Pass Number " + dt.Rows[0]["PASSNUMBER"].ToString() + "\n";
                    qrcodeString = qrcodeString + "Validity " + dt.Rows[0]["PERIODFROM"].ToString() + " To " + dt.Rows[0]["PERIODTO"].ToString() + "\n";
                    // qrcodeString = qrcodeString + "Valid To " + dt.Rows(0)("PERIODTO").ToString + vbCrLf
                    qrcodeString = qrcodeString + "Traveller Name " + dt.Rows[0]["pname"].ToString() + "\n";
                    if (DBNull.Value.Equals(dt.Rows[0]["fromstation"]))
                        qrcodeString = qrcodeString + "Stations All Station";
                    else
                        qrcodeString = qrcodeString + "Stations " + dt.Rows[0]["fromstation"].ToString() + " To " + dt.Rows[0]["tostation"].ToString();

                    // qrcodeString = qrcodeString + "To Station " + dt.Rows(0)("tostation").ToString

                    string QrCodeImg = GenerateQRCode(qrcodeString);
                    html += "<img id=\"Image1\" src=" + QrCodeImg + " style=\"border-width: 0px; border: 1px solid; width: 100px; height: 80px;\"></td>";
                    html += "</tr>";
                    html += "</table>";
                    html += "</td>";
                    html += "<td align=\"center\" style=\"width: 400px;\">";
                    html += "<table style=\"width: 500px; line-height: 25px; margin-left: 6px;\">";
                    html += "<tr>";
                    html += "<td>Name</td>";
                    html += "<td>";
                    html += "<span id=\"Label2\">" + dt.Rows[0]["pname"].ToString() + "/" + dt.Rows[0]["gender"].ToString() + "/" + dt.Rows[0]["age"].ToString() + "Year</span>";
                    html += "</td>";
                    html += "</tr>";
                    html += "<tr>";
                    html += "<td>Allowed Station</td>";
                    html += "<td>";
                    if (DBNull.Value.Equals(dt.Rows[0]["fromstation"]))
                        html += "<span id=\"Label3\">All Station</span>";
                    else
                        html += "<span id=\"Label3\">" + dt.Rows[0]["fromstation"].ToString() + " to " + dt.Rows[0]["tostation"].ToString() + "</span>";

                    html += "</td>";
                    html += "</tr>";
                    html += "<tr>";
                    html += "<td>Allowed Service</td>";
                    html += "<td>";
                    if (DBNull.Value.Equals(dt.Rows[0]["service_type_name"]))
                        html += "<span id=\"Label4\">All Services</span>";
                    else
                        html += "<span id=\"Label4\">" + dt.Rows[0]["service_type_name"] + "</span>";
                    html += "</td>";
                    html += "</tr>";
                    html += " <tr>";
                    html += "<td>Validity</td>";
                    html += "<td>";
                    html += "<span id=\"Label5\">" + dt.Rows[0]["PERIODFROM"] + " to " + dt.Rows[0]["PERIODTO"] + "</span>";
                    html += "</td>";
                    html += "</tr>";
                    html += "<tr>";
                    html += "<td>Mobile Number</td>";
                    html += "<td>";
                    html += "<span id=\"Label6\">" + dt.Rows[0]["MOBILENO"] + "</span>";
                    html += "</td>";
                    html += "</tr>";
                    html += "<tr>";
                    html += "<td>Email</td>";
                    html += "<td>";
                    html += "<span id=\"Label7\">" + dt.Rows[0]["EMAILID"] + "</span>";
                    html += "</td>";
                    html += "</tr>";
                    html += "<tr>";
                    html += "<td>Issuance Date</td>";
                    html += "<td>";
                    html += "<span id=\"Label8\">" + dt.Rows[0]["issuence_date"]+ "</span>";
                    html += "</td>";
                    html += "</tr>";
                    html += "</table>";
                    html += "</td>";
                    html += "</tr>";
                    html += "</table>";
                    html += "</td>";
                    html += "<td style=\"width: 20px; border-left: 1px solid;\"></td>";
                    html += "<td style=\"width: 480px; padding-left: 10px;\">";
                    html += "<Table style=\"width: 500px;\">";
                    html += "<tr>";
                    html += "<td colspan=\"2\"><b>Instruction</b></td>";
                    html += "</tr>";
                    html += "<tr>";
                    html += "<td>1. This smart E-pass is, non-transferable and non-refundable.</td>";
                    html += "</tr>";
                    html += "<tr>";
                    html += "<td>2. The pass can be apply or download from the link apstsarn.gov.in</td>";
                    html += "</tr>";
                    html += "<tr>";
                    html += "<td>3. During inspection smart E-pass holder manadatory to show valid original photo id proof (Aadhar Card/School ID Card/Valid Govt. Issue ID Card).</td>";
                    html += "</tr>";
                    html += "<tr>";
                    html += "<td>4. During Journey smart E-Pass holder mandatory show QR code to conductor (Soft or Hard Copy).</td>";
                    html += "</tr>";
                    html += "<tr>";
                    html += "<td>5. In case of smart E-Pass QR code not scan by system journey not allowed.</td>";
                    html += "</tr>";
                    html += "<tr>";
                    html += "<td>6. Legal action taken againt use fraud of APSTS smart E-Pass.</td>";
                    html += "</tr>";
                    html += "<tr>";
                    html += "<td style=\"padding-bottom: 15px;\">7. Smoking is prohibited on the bus.";
                    html += "</td>";
                    html += "</tr>";
                    html += "<tr>";
                    html += "<td style=\"border-top: 1px solid; text-align: center;\">The corporation's buses are a state heritage. their safety is our duty</td>";
                    html += "</tr>";
                    html += "</table>";
                    html += "</td>";
                    html += "</tr>";
                    html += "</table>";
                    html += "</div>";
                    html += "</form>";
                    html += "</body></html>";
                }

            }
            return html;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public string GetPassHTMLOutside(string Passno)
    {
        try
        {

            DataTable dt;
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "buspass.Pass_Print");
            MyCommand.Parameters.AddWithValue("p_passno", Passno);

            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    html = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"><html xmlns=\"http://www.w3.org/1999/xhtml\">";
                    html += "<head><title>Bus Pass</title>";
                    html += "<script language=\"javascript\" type=\"text/javascript\">";
                    html += " function printDiv(divID) {";
                    html += " var divElements = document.getElementById(divID).innerHTML;";
                    html += "var oldPage = document.body.innerHTML;";
                    html += " document.body.innerHTML =\"<html><head></head><body>\"+divElements + \"</body>\";";
                    html += "window.print();";
                    html += "document.body.innerHTML = oldPage;";
                    html += "}";
                    html += "</script> ";
                    html += "<style> .button1 { background: transparent; border: 1px solid #d80d0d;border-radius: 4px;color: #d80d0d;  font-size: 19px;cursor: pointer;}";

                    html += ".style1 {margin-left:10px; width: 800px;}";
                    html += ".style3 {width: 130px;}";
                    html += ".style4 {width: 110px;}";
                    html += ".style5 {width:160px;}";
                    html += "tr.example td {order-style:solid;border-color:red;border-width: 1px;padding: 10px;}";
                    html += "tr.test td {border-style:solid;border-color:Black;border-width: 1px;text-align:left;}";
                    html += ".lblAlign{	text-align:right;}";
                    html += ".style6{width: 110px;}";
                    html += ".style7{width: 115px;}";
                    html += ".style8{width: 127px;}";
                    html += ".style9{width: 279px;}";
                    html += "</style>";
                    html += "</head>";
                    html += "<body>";
                    html += " <form name = \"form1\" method=\"post\" action=\"Bus_Pass.aspx\" id=\"form1\" style=\"width: 1000px; height: 450px;\">";
                    html += "<div style = \"font-size: 20px; padding-bottom: 5px; text-align: Right();\" >";
                    html += " <input type=\"button\" onclick=\"printDiv('tnap_Div')\" value=\"Print Pass\" class=\"button1\">";
                    html += " </div>";
                    html += "<div id = \"tnap_Div\" Class=\"watermark\">";
                    html += "<table style = \"border: thin solid #000000; width: 1000px;\">";
                    html += "<tr>";
                    html += "<td style=\"width: 480px;\">";
                    html += "<table style=\"width: 500px;\">";
                    html += "<tr>";
                    html += "<td align=\"center\" style=\"width: 100px;\">";
                    html += "<img id=\"imggovt\" src=\"Logo/DeptLogo.png\" style=\"width: 70px;\">";
                    html += "</td>";
                    html += "<td align=\"center\" style=\"width: 400px;\"><span id=\"Label1\" style=\"font-family: Verdana; line-height: 19px;\"><span style=\"font-size: 11pt;\">ARUNACHAL PRADESH STATE TRANSPORT SERVICES</span><br>";
                    html += "<span style=\"font-size: 10pt; font-weight: bold;\">";
                    html += "<span id=\"lblpasstype\">" + dt.Rows[0]["BUSPASS_CATEGORY_NAME"].ToString() + " - " + dt.Rows[0]["buspasstype_name"].ToString() + "</span>";
                    html += "</span></span>";
                    html += "<br>";
                    html += "<span style=\"font-size: 10pt; font-weight: bold;\">";
                    html += "<span id=\"lblpassno\">Pass Number - " + dt.Rows[0]["PASSNUMBER"].ToString() + "</span>";
                    html += "</span>";
                    html += "</td>";
                    html += "</tr>";
                    html += "</table>";
                    html += "<table style=\"width: 500px;\">";
                    html += "<tr>";
                    html += "<td align=\"center\" style=\"width: 100px;\">";
                    html += "<table>";
                    html += "<tr>";
                    html += "<td align=\"center\">";
                    string PersonImg = GetImage(dt.Rows[0]["PHOTO"]);
                    html += "<img id=\"imgphoto\" src=" + PersonImg + " style=\"border-width: 0px; border: 1px solid; width: 100px; height: 80px;\">";
                    html += "</td>";
                    html += "</tr>";
                    html += "<tr>";
                    html += "<td align=\"center\">";
                    html += "<br />";
                    html += "</td>";
                    html += "</tr>";
                    html += "<tr>";
                    html += "<td align=\"center\">";
                    string qrcodeString = "Pass Number " + dt.Rows[0]["PASSNUMBER"].ToString() + "\n";
                    qrcodeString = qrcodeString + "Validity " + dt.Rows[0]["PERIODFROM"].ToString() + " To " + dt.Rows[0]["PERIODTO"].ToString() + "\n";
                    // qrcodeString = qrcodeString + "Valid To " + dt.Rows(0)("PERIODTO").ToString + vbCrLf
                    qrcodeString = qrcodeString + "Traveller Name " + dt.Rows[0]["pname"].ToString() + "\n";
                    if (DBNull.Value.Equals(dt.Rows[0]["fromstation"]))
                        qrcodeString = qrcodeString + "Stations All Station";
                    else
                        qrcodeString = qrcodeString + "Stations " + dt.Rows[0]["fromstation"].ToString() + " To " + dt.Rows[0]["tostation"].ToString();

                    // qrcodeString = qrcodeString + "To Station " + dt.Rows(0)("tostation").ToString

                    string QrCodeImg = GenerateQRCode(qrcodeString);
                    html += "<img id=\"Image1\" src=" + QrCodeImg + " style=\"border-width: 0px; border: 1px solid; width: 100px; height: 80px;\"></td>";
                    html += "</tr>";
                    html += "</table>";
                    html += "</td>";
                    html += "<td align=\"center\" style=\"width: 400px;\">";
                    html += "<table style=\"width: 500px; line-height: 25px; margin-left: 6px;\">";
                    html += "<tr>";
                    html += "<td>Name</td>";
                    html += "<td>";
                    html += "<span id=\"Label2\">" + dt.Rows[0]["pname"].ToString() + "/" + dt.Rows[0]["gender"].ToString() + "/" + dt.Rows[0]["age"].ToString() + "Year</span>";
                    html += "</td>";
                    html += "</tr>";
                    html += "<tr>";
                    html += "<td>Allowed Station</td>";
                    html += "<td>";
                    if (DBNull.Value.Equals(dt.Rows[0]["fromstation"]))
                        html += "<span id=\"Label3\">All Station</span>";
                    else
                        html += "<span id=\"Label3\">" + dt.Rows[0]["fromstation"].ToString() + " to " + dt.Rows[0]["tostation"].ToString() + "</span>";

                    html += "</td>";
                    html += "</tr>";
                    html += "<tr>";
                    html += "<td>Allowed Service</td>";
                    html += "<td>";
                    if (DBNull.Value.Equals(dt.Rows[0]["service_type_name"]))
                        html += "<span id=\"Label4\">All Services</span>";
                    else
                        html += "<span id=\"Label4\">" + dt.Rows[0]["service_type_name"] + "</span>";
                    html += "</td>";
                    html += "</tr>";
                    html += " <tr>";
                    html += "<td>Validity</td>";
                    html += "<td>";
                    html += "<span id=\"Label5\">" + dt.Rows[0]["PERIODFROM"] + " to " + dt.Rows[0]["PERIODTO"] + "</span>";
                    html += "</td>";
                    html += "</tr>";
                    html += "<tr>";
                    html += "<td>Mobile Number</td>";
                    html += "<td>";
                    html += "<span id=\"Label6\">" + dt.Rows[0]["MOBILENO"] + "</span>";
                    html += "</td>";
                    html += "</tr>";
                    html += "<tr>";
                    html += "<td>Email</td>";
                    html += "<td>";
                    html += "<span id=\"Label7\">" + dt.Rows[0]["EMAILID"] + "</span>";
                    html += "</td>";
                    html += "</tr>";
                    html += "<tr>";
                    html += "<td>Issuance Date</td>";
                    html += "<td>";
                    html += "<span id=\"Label8\">" + dt.Rows[0]["issuence_date"] + "</span>";
                    html += "</td>";
                    html += "</tr>";
                    html += "</table>";
                    html += "</td>";
                    html += "</tr>";
                    html += "</table>";
                    html += "</td>";
                    html += "<td style=\"width: 20px; border-left: 1px solid;\"></td>";
                    html += "<td style=\"width: 480px; padding-left: 10px;\">";
                    html += "<Table style=\"width: 500px;\">";
                    html += "<tr>";
                    html += "<td colspan=\"2\"><b>Instruction</b></td>";
                    html += "</tr>";
                    html += "<tr>";
                    html += "<td>1. This smart E-pass is, non-transferable and non-refundable.</td>";
                    html += "</tr>";
                    html += "<tr>";
                    html += "<td>2. The pass can be apply or download from the link apstsarn.gov.in</td>";
                    html += "</tr>";
                    html += "<tr>";
                    html += "<td>3. During inspection smart E-pass holder manadatory to show valid original photo id proof (Aadhar Card/School ID Card/Valid Govt. Issue ID Card).</td>";
                    html += "</tr>";
                    html += "<tr>";
                    html += "<td>4. During Journey smart E-Pass holder mandatory show QR code to conductor (Soft or Hard Copy).</td>";
                    html += "</tr>";
                    html += "<tr>";
                    html += "<td>5. In case of smart E-Pass QR code not scan by system journey not allowed.</td>";
                    html += "</tr>";
                    html += "<tr>";
                    html += "<td>6. Legal action taken againt use fraud of APSTS smart E-Pass.</td>";
                    html += "</tr>";
                    html += "<tr>";
                    html += "<td style=\"padding-bottom: 15px;\">7. Smoking is prohibited on the bus.";
                    html += "</td>";
                    html += "</tr>";
                    html += "<tr>";
                    html += "<td style=\"border-top: 1px solid; text-align: center;\">The corporation's buses are a state heritage. their safety is our duty</td>";
                    html += "</tr>";
                    html += "</table>";
                    html += "</td>";
                    html += "</tr>";
                    html += "</table>";
                    html += "</div>";
                    html += "</form>";
                    html += "</body></html>";
                }

            }
            return html;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
    private string GenerateQRCode(string p_String)
    {
        classAES passaes = new classAES();
        string qrtring = "";
        string key = ConfigurationManager.AppSettings["QRCodehashKey"];
        string qrcodeString = p_String;

        QRCodeGenerator qrGenerator = new QRCodeGenerator();
        QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(qrcodeString, QRCodeGenerator.ECCLevel.Q);
        System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
        imgBarCode.Height = 150;
        imgBarCode.Width = 150;
        byte[] byteImage;
        using (Bitmap bitMap = qrCode.GetGraphic(20))
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byteImage = ms.ToArray();
            }
        }
        qrtring = GetImage(byteImage);
        return qrtring;
    }
    public string GetImage(object img)
    {
        try
        {
            return "data:image/jpg;base64," + Convert.ToBase64String((byte[])img);
        }
        catch (Exception ex)
        {
            return "";
        }
    }
}