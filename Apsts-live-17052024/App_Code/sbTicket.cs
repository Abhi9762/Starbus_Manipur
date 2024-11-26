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
//using Grpc.Core;

/// <summary>
/// Summary description for sbTicket
/// </summary>
public class sbTicket
{
    NpgsqlCommand MyCommand = new NpgsqlCommand();
    private sbBLL bll = new sbBLL();
    DataTable MyTable = new DataTable();
    sbValidation _validation = new sbValidation();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    private sbCommonFunc _common = new sbCommonFunc();

    string html = "";
    string gstn = "04AAALC1790P1ZZ";
    decimal decFareAmount = 0;
    decimal decResrvCharges = 0;
    decimal decRefundAmt = 0;
    decimal decTotalFare = 0;
    decimal decTaxamt = 0;
    public sbTicket()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public string GetTicket(string p_ticketno)
    {
        //ImgDepartmentLogo.ImageUrl = "Logo/" + obj.loadDeptLogo();
        //lblDeptName.Text = obj.loadDeptNameAbbr();
        //lblversion.Text = obj.loadVersion();
        ////lblemail.Text = obj.loadEmail();
        ////lblcontact.Text = obj.loadContact();
        ////lblHelpine.Text = obj.loadtollfree();
        //lbldepartmentname.Text = obj.loadDeptName();
        string host = HttpContext.Current.Request.Url.Host;


        try
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(HttpContext.Current.Server.MapPath("CommonData.xml"));
            XmlNodeList deptlogo = doc.GetElementsByTagName("dept_logo_url");
            sbXMLdata obj1 = new sbXMLdata();
            string deptname = obj1.loadDeptName(); ;
            wsClass obj = new wsClass();
            DataTable MyTable = obj.getJourneyDetails(p_ticketno, "A");
            if (MyTable.Rows.Count > 0)
            {
                html = "<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'><html xmlns='http://www.w3.org/1999/xhtml'>";
                html += "<head><title>Ticket(Print)</title>";
                html += "<script language='javascript' type='text/javascript'>";
                html += " function printDiv(divID) {";
                html += " var divElements = document.getElementById(divID).innerHTML;";
                html += "var oldPage = document.body.innerHTML;";
                html += " document.body.innerHTML ='<html><head></head><body>'+divElements + '</body>';";
                html += "window.print();";
                html += "document.body.innerHTML = oldPage;";
                html += "}";
                html += "</script> ";
                html += "<style> .button1 {border-radius: 4px;background-color: aliceblue;font-family: verdana;font-size: 16px;width: 52%;}";
                html += " table td {padding-left:10px;}";
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
                html += "<body><form name='form1' method='post' action='e_Ticket.aspx' id='form1' style='  width:820px; height:450px;'>";
                html += "<div style='text-align: center;'>";
                html += "<input type =" + "button" + " onclick=" + "printDiv('tnap_Div')" + " value='Print' class='button1' style='width:100px;' /></div>";
                html += " <div id='tnap_Div'  style=' border: thin solid #000000;'>";
                html += " <table style='width:100%;'><tr>";

                List<string> splitList = new List<string>();
                splitList = deptlogo[0].InnerXml.Split('/').ToList();
                splitList.RemoveAt(0);
                string outputString = string.Join("/", splitList);



                //html += "<td align='left' style='width:20%;'><img src=" + outputString + " style=""width:50%;border-right: 1px solid;"" />";
                html += "<td align='left' style='width:20%;'><img src='" + outputString + "' style='width:50%;' />";


                if (MyTable.Rows[0]["thirdpartyagent_"].ToString() != "0")
                {
                    string thirdprtyname = "Third_Party_" + MyTable.Rows[0]["thirdpartyagent_"].ToString().ToUpper().Replace(" ", "");
                    XmlNodeList thirdprtylogo = doc.GetElementsByTagName(thirdprtyname);

                    List<string> splitthirdprtylogo = thirdprtylogo.Item(0).InnerXml.Split('/').ToList();
                    splitthirdprtylogo.RemoveAt(0);
                    string outputthirdprtylogo = string.Join("/", splitthirdprtylogo);

                    html += "<img src=" + outputthirdprtylogo + " style=\"width: 49%; height: 64px; border-left:1px solid;\" />";
                }

                html += "<td align='center' style='width:60%;'>";
                html += "<span id='Label1' style='font-family:Verdana;font-size:Large;font-weight:bold;'>" + deptname + "<br/>E-Ticket</span>";
                html += "</td>";
                html += "<td align='right' style='width:20%;'>";
                string key = System.Configuration.ConfigurationManager.AppSettings["TicketQRCodeKey"].ToString();
                string ticketno = MyTable.Rows[0]["_ticketno"].ToString();
                string fStation = MyTable.Rows[0]["fr_ston"].ToString();
                string tStation = MyTable.Rows[0]["to_ston"].ToString();
                string bStation = MyTable.Rows[0]["bor_ston"].ToString();
                string dsvcid = MyTable.Rows[0]["depot_servicecode"].ToString();
                string strpid = MyTable.Rows[0]["strpid"].ToString();
                string totalfareamt = MyTable.Rows[0]["amount_total"].ToString();
                string totalseats = MyTable.Rows[0]["total_seats_booked"].ToString();
                string jdate = MyTable.Rows[0]["journeydate"].ToString();
                string jtime = MyTable.Rows[0]["trip_time"].ToString();
                //EncryptData(MyTable.Rows(0)("sourceabbr").ToString + MyTable.Rows(0)("destinationabbr").ToString, key)
                //Dim Service As String = EncryptData(MyTable.Rows(0)("SERVICECODE").ToString + MyTable.Rows(0)("triptype").ToString, key)
                // Dim JOURNEYDATE As String = EncryptData(MyTable.Rows(0)("JOURNEYDATE").ToString, key)
                // 'EncryptData(MyTable.Rows(0)("totalfareamt").ToString, key)
                // EncryptData(MyTable.Rows[0]["totalseats").ToString.ToString, key)



                string qrcodeString = ticketno+"|"+dsvcid + "|" +strpid + "|" +jdate + "|" +jtime + "|" +fStation + "|" +tStation  + "|" +totalseats + "|" +totalfareamt + "|" + bStation;
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
                string ss = GetImage(byteImage);
                html += "<img src='" + ss + "'width='54%' />";
                //html += "<img src='Logo/GovtLogo.png' style='width:50%;' />";
                html += "</td>";
                html += "</tr> </table>";
                //***************Neeraj Chnage***************************
                html += " <table style='width:100%;'><tr>";
                html += "<td align='left' style='width:40%;'><span id='Label35' style='font-family:Verdana;font-size:8pt;font-weight:bold;'>GSTN.-</span>";
                html += "<span id='Label34' style='font-family:Verdana;font-size:8pt;font-weight:bold;'>" + gstn + "</span>";
                html += "</td>";
                html += "<td align='center' style='width:20%;'>";
                //html += "<span id='Label1' style='font - family:Verdana; font - size:Large; font - weight:bold; '>Ticket</span>";
                html += "</td>";
                html += "<td align='right' style='width:40%;'>";
                html += "<span id='lblBookedBy' style='font-family:Verdana;font-size:8pt;font-weight:bold;'> Booked By " + MyTable.Rows[0]["booked_by"].ToString() + "(" + MyTable.Rows[0]["book_by_type"].ToString() + ")" + "</span>";
                html += "</td>";
                html += "</tr> </table>";
                //****************End*************************************
                html += "<div><span id='Label2' style='font-family:Verdana;font-size:8pt;font-weight:bold;margin-left:10px;'>";
                html += " Your ticket request has been successfully processed. Thank you for availing our services. Your ticket and its details are as follows- </span> </div>";
                html += "<table style='border: thin solid #000000; width:800px; margin-left:10px;' > ";
                html += "<tr>";
                html += "<td class='style3' align='right'><span id='Label3' style='font-family:Verdana;font-size:8pt;'>PNR NO.</span></td>";
                html += "<td align='left' class='style6'><span id='lblPnr' style='font-family:Verdana;font-size:8pt;'>" + MyTable.Rows[0]["_ticketno"].ToString() + "</span></td>";
                html += "<td class='style4' align='right'><span id='Label7' style='font-family:Verdana;font-size:8pt;'>Service Code</span></td>";
                html += "<td class='style4' align='left'><span id='lblServiceCode' style='font-family:Verdana;font-size:8pt;'>" + MyTable.Rows[0]["depot_servicecode"].ToString() + "</span></td>";
                html += "<td class='style7' align='right'><span id='Label5' style='font-family:Verdana;font-size:8pt;'>Date of Booking</span></td>";
                html += "<td class='style5' align='left'><span id='lblBookingDt' style='font-family:Verdana;font-size:8pt;'>" + MyTable.Rows[0]["bookingdatetime"].ToString() + "</span></td>";
                html += "</tr>";
                html += "<tr>";
                html += "<td class='style3' align='right'><span id='Label4' style='display:inline-block;font-family:Verdana;font-size:8pt;width:100%;'>Scheduled Departure</span></td>";
                html += "<td class='style6'><span id='lblDeparture' style='font-family:Verdana;font-size:8pt;'>" + MyTable.Rows[0]["trip_time"].ToString() + "</span></td>";
                html += "<td class='style4' align='right'><span id='Label6' style='font-family:Verdana;font-size:8pt;'>Quota</span></td>";
                html += "<td class='style4' align='left'><span id='lblQuota' style='font-family:Verdana;font-size:8pt;'>General</span></td>";

                html += "<td class='style7' align='right'><span id='Label9' style='font-family:Verdana;font-size:8pt;'>Bus Type</span></td>";
                html += "<td class='style5'><span id='lblBustype' style='font-family:Verdana;font-size:8pt;'>" + MyTable.Rows[0]["service_type_name"].ToString() + "</span></td>";
                html += "</tr>";
                html += "<tr>";
                html += "<td class='style3' align='right'><span id='Label11' style='font-family:Verdana;font-size:8pt;'>From</span></td>";
                html += "<td class='style6'><span id='lblSource' style='font-family:Verdana;font-size:8pt;'>" + MyTable.Rows[0]["fromstn_name"].ToString() + "</span></td>";
                html += "<td class='style4' align='right'><span id='Label13' style='font-family:Verdana;font-size:8pt;'>Date of Journey</span></td>";
                html += "<td class='style4' align='left'><span id='lblJourneyDt' style='font-family:Verdana;font-size:8pt;'>" + MyTable.Rows[0]["journeydate"].ToString() + "</span></td>";
                html += "<td class='style7' align='right'><span id='Label12' style='font-family:Verdana;font-size:8pt;'>To</span></td>";
                html += "<td class='style5'><span id='lblDestination' style='font-family:Verdana;font-size:8pt;'>" + MyTable.Rows[0]["tostn_name"].ToString() + "</span></td>";
                html += "</tr>";
                html += "<tr>";
                html += "<td class='style3' align='right'><span id='Label15' style='font-family:Verdana;font-size:8pt;'>Boarding</span> </td>";
                html += "<td class='style6'><span id='lblBoarding' style='font-family:Verdana;font-size:8pt;'>" + MyTable.Rows[0]["boardingstn_name"].ToString() + "</span></td>";
                html += "<td class='style4' align='right'><span id='lblRefNoH' style='font-family:Verdana;font-size:8pt;'>No of Passenger</span></td>";
                html += "<td class='style4' align='left'><span id='lblNoPsngr' style='font-family:Verdana;font-size:8pt;'>" + MyTable.Rows[0]["total_seats_booked"].ToString() + "</span></td>";
                html += "<td class='style7' align='right'><span id='Label22' style='font-family:Verdana;font-size:8pt;'>Distance(in KM)</span></td>";
                html += "<td class='style5'><span id='lblDistance' style='font-family:Verdana;font-size:8pt;'>" + MyTable.Rows[0]["total_distance"].ToString() + "</span></td>";
                html += "</tr> ";

                html += "<tr>";
                html += "<td class='style3' align='right'> <span id='Label24' style='font-family:Verdana;font-size:8pt;'>Reservation upto</span></td>";
                html += "<td class='style6'><span id='lblResUpto' style='font-family:Verdana;font-size:8pt;'>" + MyTable.Rows[0]["tostn_name"].ToString() + "</span></td>";
                html += "<td class='style4' align='right'><span id='Label26' style='font-family:Verdana;font-size:8pt;'>Scheduled Arrival</span></td>";
                html += "<td class='style4' align='left'><span id='lblArrival' style='font-family:Verdana;font-size:8pt;'></span></td>";
                html += "<td class='style7' align='right'><span id='Label28' style='font-family:Verdana;font-size:8pt;'>Passenger Mob.No</span></td>";
                html += "<td class='style5'><span id='lblMobNo' style='font-family:Verdana;font-size:8pt;'>" + MyTable.Rows[0]["traveller_mobile_no"].ToString() + "</span></td>";
                html += "</tr>";
                html += "<tr>";
                html += "<td class='style3' align='right'><span id='lblPsngrAdd' style='display:inline-block;font-family:Verdana;font-size:8pt;width:100%;'>Passenger Address</span></td> ";
                html += "<td colspan='5' > <span id='lblPassngrAddress' style='display:inline-block;font-family:Verdana;font-size:8pt;width:100%;'>NA</span></td>";
                html += " </tr> ";
                html += " </table>";
                html += "<span id='Label10' style='font-family:Verdana;font-size:Small;font-weight:bold;margin-left:365px;'>Fare Detail</span>";
                html += "<table id='Table1' class='style1' style='text-align: center;margin-left:10px;width: 800px;  border: thin solid Black; font-family:Verdana; font-size:8pt; text-align:left; table-layout: auto; border-collapse: collapse; border-spacing: 1px;' border='1' cellspacing='1'>";
                html += "<tr style=' background-color:#CCCCCC; border: thin solid #000000; line-height: normal; vertical-align: baseline; text-align: center; ' >";
                html += "<td style='width:150px;'><span id='Label23' style='font-family:Verdana;font-size:8pt;font-weight:bold;'>Description</span></td>";
                html += "<td style='width:100px; text-align:right;'><span id='Label25' style='display:inline-block;font-family:Verdana;font-size:8pt;font-weight:bold;width:120px;'>Amount(in rupee)</span></td>";
                html += "<td style='width:350px;'><span id='Label27' style='font-family:Verdana;font-size:8pt;font-weight:bold;'>Amount(in words)</span></td>";
                html += "</tr>";

                //--------------------------Fare Amount
                html += "<tr>";
                html += "<td align='center'><span id='Label29' style='font-family:Verdana;font-size:8pt;'>Ticket Amt:</span></td>";
                html += "  <td align='right'><span id='lblTicket' class='lblAlign' style='font-family:Verdana;font-size:8pt;'>" + MyTable.Rows[0]["amount_fare"].ToString() + "</span></td>";
                html += "<td align='left'><span id='lblTicketWord' style='font-family:Verdana;font-size:8pt;'>" + Number2Word(MyTable.Rows[0]["amount_fare"].ToString()) + "</span></td>";
                decFareAmount = Convert.ToDecimal(MyTable.Rows[0]["amount_fare"].ToString());
                //-------------------------Reservation Amt
                html += "</tr><tr><td align='center'><span id='Label14' style='font-family:Verdana;font-size:8pt;'>Reservation Charge:</span></td>";
                html += " <td align='right'><span id='lblResrCharge' class='lblAlign' style='font-family:Verdana;font-size:8pt;'>" + MyTable.Rows[0]["amount_onl_reservation"].ToString() + "</span></td>";
                html += "<td align='left'><span id='lblReschrgWord' style='font-family:Verdana;font-size:8pt;'>" + Number2Word(MyTable.Rows[0]["amount_onl_reservation"].ToString()) + "</span></td></tr>";
                decResrvCharges = Convert.ToDecimal(MyTable.Rows[0]["amount_onl_reservation"].ToString());
                //-------------------------Tax Amt
                html += "</tr><tr><td align='center'><span id='Label14' style='font-family:Verdana;font-size:8pt;'>Tax Amt:</span></td>";
                html += " <td align='right'><span id='lblResrCharge' class='lblAlign' style='font-family:Verdana;font-size:8pt;'>" + MyTable.Rows[0]["amount_tax"].ToString() + "</span></td>";
                html += "<td align='left'><span id='lblReschrgWord' style='font-family:Verdana;font-size:8pt;'>" + Number2Word(MyTable.Rows[0]["amount_tax"].ToString()) + "</span></td></tr>";
                decTaxamt = Convert.ToDecimal(MyTable.Rows[0]["amount_tax"].ToString());

                //-------------------------Refund Amt
                // decRefundAmt = GetRefundAmt(tktno)
                if (decRefundAmt > 0)
                {
                    html += "<tr><td align='center'><span id='lblRefund' style='font-family:Verdana;font-size:8pt;'>Refund Amt:</span></td>";
                    html += "<td align='right'><span id='lblRefndAmt' style='font-family:Verdana;font-size:8pt;'>" + decRefundAmt + "</span></td>";
                    html += "<td align='left'><span id='lblRefndWord' style='font-family:Verdana;font-size:8pt;'>" + Number2Word(decRefundAmt.ToString()) + "</span></td></tr>";
                }
                //------------------------Tax Amt
                //Dim oParameterListtax(2) As OracleParameter
                //oParameterListtax(0) = New OracleParameter("P_TICKETNO", OracleDbType.Varchar2, tktno, Data.ParameterDirection.Input)
                //oParameterListtax(1) = New OracleParameter("P_BOOKEDBYUSERID", OracleDbType.Varchar2, MyTable.Rows(0)("bookedbyuserid").ToString, Data.ParameterDirection.Input)
                //oParameterListtax(2) = New OracleParameter("REFCUR", OracleDbType.RefCursor, Data.ParameterDirection.Output)
                //Dim dtTaxDetails As DataTable = OracleHelper.GetDataTableBySP("SPTICKETDETAILS", oParameterListtax)
                //If dtTaxDetails.Rows.Count > 0 Then
                //    If Not(IsDBNull(dtTaxDetails.Rows(0)("Taxid")) = True) Or(dtTaxDetails.Rows(0)("TOTALTAX") = 0) Then
                //        If((dtTaxDetails.Rows(0)("Taxid").ToString) = 3) Then
                //            decTaxamt = decTaxamt + Convert.ToDecimal(dtTaxDetails.Rows(0)("TAXAMT").ToString)
                //            html += "<tr><td align='center'><span id='Label29' style='font-family:Verdana;font-size:8pt;'>IGST Amt.</span></td>";
                //    html += "  <td align='right'><span id='lblTicket' class='lblAlign' style='font-family:Verdana;font-size:8pt;'>"; &dtTaxDetails.Rows(0)("TAXAMT").ToString & "</span></td>";
                //    html += "<td align='left'><span id='lblTicketWord' style='font-family:Verdana;font-size:8pt;'>"; &Number2Word(Convert.ToDecimal(dtTaxDetails.Rows(0)("TAXAMT"))).ToString & "</span></td>";
                //    html += "</tr>";
                //    Else
                //        If(dtTaxDetails.Rows.Count = 2) Then
                //                If((dtTaxDetails.Rows(0)("Taxid").ToString) = 1) Then
                //                    decTaxamt = decTaxamt + Convert.ToDecimal(dtTaxDetails.Rows(0)("TAXAMT").ToString)
                //                    html += "<tr><td align='center'><span id='Label29' style='font-family:Verdana;font-size:8pt;'>CGST Amt. </span></td>";
                //    html += "<td align='right'><span id='lblTicket' class='lblAlign' style='font-family:Verdana;font-size:8pt;'>"; &dtTaxDetails.Rows(0)("TAXAMT").ToString & "</span></td>";
                //    html += "<td align='left'><span id='lblTicketWord' style='font-family:Verdana;font-size:8pt;'>"; &Number2Word(dtTaxDetails.Rows(0)("TAXAMT")).ToString & "</span></td>";
                //    html += "</tr>";
                //    End If
                //                If((dtTaxDetails.Rows(1)("Taxid").ToString) = 2) Then
                //                    decTaxamt = decTaxamt + Convert.ToDecimal(dtTaxDetails.Rows(1)("TAXAMT").ToString)
                //                    html += "<tr><td align='center'><span id='Label29' style='font-family:Verdana;font-size:8pt;'>SGST Amt. </span></td>";
                //    html += "<td align='right'><span id='lblTicket' class='lblAlign' style='font-family:Verdana;font-size:8pt;'>"; &dtTaxDetails.Rows(1)("TAXAMT").ToString & "</span></td>";
                //    html += "<td align='left'><span id='lblTicketWord' style='font-family:Verdana;font-size:8pt;'>"; &Number2Word(dtTaxDetails.Rows(1)("TAXAMT")).ToString & "</span></td>";
                //    html += "</tr>";
                //    End If

                //            End If
                //        End If
                //    End If
                //End If
                //--------------------------TotalAmt
                decTotalFare = decFareAmount + decResrvCharges + decTaxamt - decRefundAmt;
                html += "<tr><td align='center'><span id='Label30' style='font-family:Verdana;font-size:8pt;'>Total Amt:</span></td>";
                html += "<td align='right'><span id='lblTotal' style='font-family:Verdana;font-size:8pt;'>" + decTotalFare + "</span></td>";
                html += "<td align='left'><span id='lblTotalWord' style='font-family:Verdana;font-size:8pt;'>" + Number2Word(decTotalFare.ToString()) + "</span></td> </tr></table><div>";

                //--------------------Seat Details
                string pp = getpassengerDetails(ticketno);
                html += "<span id='Label16' style='font-family:Verdana;font-size:Small;font-weight:bold;margin-left:345px;'>Detail of Seat(s)</span>";
                html += " </div><div class='watermark'><table id='tbl'  style='text-align: center;width: 800px; margin-left:10px;  border: thin solid Black; font-family:Verdana; font-size:8pt; text-align:left; table-layout: auto; border-collapse: collapse; border-spacing: 1px;' border='1' cellspacing='1' > ";
                html += "<tr style=' background-color:#CCCCCC; border: thin solid #000000; line-height: normal; vertical-align: baseline; text-align: center; ' > ";

                html += "<td style='width:50px;'><span id='Label17' style='font-family:Verdana;font-size:8pt;font-weight:bold;'>S.No.</span></td>";
                html += "<td style='width:200px;'><span id='Label21' style='font-family:Verdana;font-size:8pt;font-weight:bold;'>Name of Passenger</span></td>";
                html += "<td style='width:100px;'><span id='Label18' style='font-family:Verdana;font-size:8pt;font-weight:bold;'>Gender</span></td>";
                html += "<td style='width:100px;'><span id='Label19' style='font-family:Verdana;font-size:8pt;font-weight:bold;'>Age</span></td>";
                html += "<td style='width:100px;'><span id='Label20' style='font-family:Verdana;font-size:8pt;font-weight:bold;'>Seat No.</span></td>";
                html += "<td style='width:100px;'><span id='Label31' style='font-family:Verdana;font-size:8pt;font-weight:bold;'>Status</span> </td>";
                html += "<td style='width:100px;'><span id='Label20' style='font-family:Verdana;font-size:8pt;font-weight:bold;'>Quota</span></td></tr>";
                html += pp;

                html += "</table><div>";
                html += "<div><p style='color: red;margin-left:5px;margin-right:5px'>";
                html += "Passengers are advised to carry Inner Line Permit(ILP) on visit to any of the place in Arunachal Pradesh which is checked at designated Check Gates on entry to Arunachal Pradesh";
                html += "</p></div>";

                wsClass objj = new wsClass();
                DataTable dtt = obj.gettermsCondition();
                html += HttpUtility.HtmlDecode(dtt.Rows[0]["termconditiondtls"].ToString());


                html += "<table style='width:800px; margin-left:10px;' > <tr> ";
                html += "<td><span id='Label8' style='font-family:Verdana;font-size:10pt;font-weight:bold;'>Cancellation/Refund/Rescheduling</span>";
                html += "</td></tr><tr><td style='font-family: Verdana; font-size: 8pt;'>";
                html += "Ticket booked through Online, refund will be done to their respective Credit Cards/Debit Cards/Bank Accounts according to the Bank procedure. No refund will be done at MSTS ticket booking counters.<br />";
                html += "Detailed Cancellation Policy is available under About Us Policies  of Home Page.<br />";
                html += "The cancellation slabs (excluding the reservation charges, transaction charges and service tax which are non-refundable) are as below:-";
                html += "<br />";
                html += _common.getCancellationpolicy();
                html += "<br />";
                html += "For any query regarding bus services please contact enquiry offices.";
                html += "</td></tr>";


                //Extra Text on ticket start *******************************************
                string extraText = "* Happy Journey *";

                html += "<tr style='background: gainsboro;'> <td style='padding:5px; text-align: center;'> ";
                html += "<span style='font-family:Verdana; font-size:8pt; '>" + extraText + "</span> </td></tr>";
                // Extra Text on ticket end *******************************************

                html += "<tr style='background: gainsboro;'> <td style='padding:10px'> ";
                html += "<span id='lblNotice1' style='font-family:Verdana;font-size:8pt;padding-top: 10px;'>Visit Us <b>" + host + "</b></span>";
                html += "<span id='lblNotice2' style='font-family:Verdana;font-size:8pt;float: right;'><p style='margin: 0px;'>Powered By</p> <img src='images/nic.png' style='height: 30px;' /></span> </td></tr>";

                html += "</table></div> <br /></div> </div>";
                html += "</div></form></body></html>";
            }


            return html;
        }
        catch (Exception ex)
        {
            return html;
        }
    }


    private string getpassengerDetails(string tktno)
    {
        try
        {
            wsClass obj = new wsClass();
            DataTable dtPassengerDetails = obj.getPassengerDetails(tktno, "A");
            if (dtPassengerDetails.Rows.Count > 0)
            {
                int i = 0;
                string html1 = "";
                foreach (DataRow drow in dtPassengerDetails.Rows)
                {
                    string status = "";
                    if (drow["status"].ToString() == "A")
                        status = "Confirmed";
                    else if (drow["status"].ToString() == "C")
                    {
                        status = "Cancelled";
                        //if (!IsDBNull(dtPassengerDetails.Rows(i)("amtrefunded").ToString))
                        //    decRefundAmt = decRefundAmt + Convert.ToDecimal(dtPassengerDetails.Rows[i]["amtrefunded"].ToString());
                    }
                    i = i + 1;
                    html1 += "<tr style='border: thin solid #000000; line-height: normal; vertical-align: baseline; text-align: center;'> ";
                    html1 += "<td ><span id='Label17' style='font-family:Verdana;font-size:8pt;'>" + i + "</span></td>";
                    html1 += "<td ><span id='Label21' style='font-family:Verdana;font-size:8pt;'>" + drow["travellername"] + "</span></td>";



                    if (drow["travellergender"].ToString() == "F")
                        html1 += "<td ><span id='Label18' style='font-family:Verdana;font-size:8pt;'>Female</span></td>";
                    else if (drow["travellergender"].ToString() == "M")
                        html1 += "<td ><span id='Label18' style='font-family:Verdana;font-size:8pt;'>Male</span></td>";
                    else
                        html1 += "<td ><span id='Label18' style='font-family:Verdana;font-size:8pt;'>" + drow["travellergender"] + "</span></td>";

                    html1 += "<td ><span id='Label19' style='font-family:Verdana;font-size:8pt;'>" + drow["travellerage"] + "</span></td>";
                    html1 += "<td ><span id='Label20' style='font-family:Verdana;font-size:8pt;'>" + drow["seatno"] + "</span></td>";
                    html1 += "<td ><span id='Label31' style='font-family:Verdana;font-size:8pt;'>" + status + "</span> </td>";
                    html1 += "<td ><span id='Label21' style='font-family:Verdana;font-size:8pt;'>" + drow["concessiontype_name"] + "</span></td></tr>";


                }
                return html1;

            }
            return "";


        }
        catch (Exception ex)
        {
            return "";
        }
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
    private static String ones(String Number)
    {
        int _Number = Convert.ToInt32(Number);
        String name = "";
        switch (_Number)
        {

            case 1:
                name = "One";
                break;
            case 2:
                name = "Two";
                break;
            case 3:
                name = "Three";
                break;
            case 4:
                name = "Four";
                break;
            case 5:
                name = "Five";
                break;
            case 6:
                name = "Six";
                break;
            case 7:
                name = "Seven";
                break;
            case 8:
                name = "Eight";
                break;
            case 9:
                name = "Nine";
                break;
        }
        return name;
    }
    private static String tens(String Number)
    {
        int _Number = Convert.ToInt32(Number);
        String name = null;
        switch (_Number)
        {
            case 10:
                name = "Ten";
                break;
            case 11:
                name = "Eleven";
                break;
            case 12:
                name = "Twelve";
                break;
            case 13:
                name = "Thirteen";
                break;
            case 14:
                name = "Fourteen";
                break;
            case 15:
                name = "Fifteen";
                break;
            case 16:
                name = "Sixteen";
                break;
            case 17:
                name = "Seventeen";
                break;
            case 18:
                name = "Eighteen";
                break;
            case 19:
                name = "Nineteen";
                break;
            case 20:
                name = "Twenty";
                break;
            case 30:
                name = "Thirty";
                break;
            case 40:
                name = "Fourty";
                break;
            case 50:
                name = "Fifty";
                break;
            case 60:
                name = "Sixty";
                break;
            case 70:
                name = "Seventy";
                break;
            case 80:
                name = "Eighty";
                break;
            case 90:
                name = "Ninety";
                break;
            default:
                if (_Number > 0)
                {
                    name = tens(Number.Substring(0, 1) + "0") + " " + ones(Number.Substring(1));
                }
                break;
        }
        return name;
    }
    private static String Number2Word(String Number)
    {
        string word = "";
        try
        {
            bool beginsZero = false;//tests for 0XX    
            bool isDone = false;//test if already translated    
            double dblAmt = (Convert.ToDouble(Number));
            //if ((dblAmt > 0) && number.StartsWith("0"))   
            word = "Zero";
            if (dblAmt > 0)
            {//test for zero or digit zero in a nuemric    
                beginsZero = Number.StartsWith("0");

                int numDigits = Number.Length;
                int pos = 0;//store digit grouping    
                String place = "";//digit grouping name:hundres,thousand,etc...    

                switch (numDigits)
                {

                    case 1://ones' range    

                        word = ones(Number);
                        isDone = true;
                        break;
                    case 2://tens' range    
                        word = tens(Number);
                        isDone = true;
                        break;
                    case 3://hundreds' range    
                        pos = (numDigits % 3) + 1;
                        place = " Hundred ";
                        break;
                    case 4://thousands' range    
                    case 5:
                    case 6:
                        pos = (numDigits % 4) + 1;
                        place = " Thousand ";
                        break;
                    case 7://millions' range    
                    case 8:
                    case 9:
                        pos = (numDigits % 7) + 1;
                        place = " Million ";

                        break;
                    case 10://Billions's range    
                    case 11:
                    case 12:

                        pos = (numDigits % 10) + 1;
                        place = " Billion ";
                        break;

                    //add extra case options for anything above Billion...    
                    default:
                        isDone = true;
                        break;
                }
                if (!isDone)
                {//if transalation is not done, continue...(Recursion comes in now!!)    
                    if (Number.Substring(0, pos) != "0" && Number.Substring(pos) != "0")
                    {
                        try
                        {
                            word = Number2Word(Number.Substring(0, pos)) + place + Number2Word(Number.Substring(pos));
                        }
                        catch { }
                    }
                    else
                    {
                        word = Number2Word(Number.Substring(0, pos)) + Number2Word(Number.Substring(pos));
                    }

                    //check for trailing zeros    
                    //if (beginsZero) word = " and " + word.Trim();    
                }
                //ignore digit grouping names    
                if (word.Trim().Equals(place.Trim()))
                    word = "";
            }
        }
        catch { }
        return word.Trim();
    }


}