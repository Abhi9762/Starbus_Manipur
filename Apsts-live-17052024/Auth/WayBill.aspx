<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WayBill.aspx.cs" Inherits="Auth_WayBill" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
	<link href="../assets/css/bootstrap/bootstrap.css" rel="stylesheet" />
	<link href="../assets/css/bootstrap/bootstrap.min.css" rel="stylesheet" />
     <link rel="stylesheet" href="../assets/vendor/@fortawesome/fontawesome-free/css/all.min.css" type="text/css" />
	<style>
		.table td, .table th {
			padding: 3px;
			font-size: 14px !important;
		}
	</style>
    <script language="javascript" type="text/javascript">
        function printDiv(divID) {
            var divElements = document.getElementById(divID).innerHTML;
            var oldPage = document.body.innerHTML;
            document.body.innerHTML =
                "<html><head></head><body>" +
                divElements + "</body>";
            window.print();
            document.body.innerHTML = oldPage;
        }
    </script>
</head>
<body>
	<form id="form1" runat="server">
		<div style="width: 100%;" id="details" runat="server">
            <center>
			<%--<asp:Button ID="btnPrint" Style="border-radius: 10px; margin-top: 10px; background-color: aliceblue; font-size: 16px;margin-bottom:5px"
				runat="server" Text="Print" OnClientClick="printDiv('maindiv')"
				></asp:Button>--%>

                 <asp:LinkButton ID="Button1" 
				runat="server" Text="Print" OnClientClick="printDiv('maindiv')" CssClass="btn btn-primary mb-2"
				><i class="fa fa-print"></i> Print</asp:LinkButton>
            </center>
			<div class=" row m-0" id="maindiv" style="border: 1px solid;">
				<div class="col-lg-12">
					<table id="divprint" style="margin: auto" class="mt-2 w-100" border=1 frame=hsides rules=rows>
						<tr style="vertical-align: top;">
							<td>
								<asp:Image runat="server" ID="imgLogo" AlternateText="Logo" Width="90px" /></td>
							<td style="vertical-align: middle;">
								<center>
                                    <h2 runat="server" id="lblHeading" style="font-size: 18pt;">State Road Transport Corporation</h2>
                                    <h3 style="font-size: 14pt">Duty Waybill</h3>
                                </center>
							</td>
							<td>
								<img alt="" src="../images/Self.png" runat="server" class="igte_rPayNet_ButtonImg" id="imgQRCode" style="border-width: 0px; width: 90px; border: 2px solid #eaf4ff;" />
							</td>
						</tr>
					</table>
					<br />
					<table class="table table-borderless" style="margin: auto">
                        <tr>
                            <td colspan="12" >
                                <asp:Label ID="Label1" runat="server" Text="1. Duty Details" Font-Underline="true" Font-Size="Medium" CssClass="form-control-label ml--10"
                                    Font-Bold="true"  class="text-center"></asp:Label>
                            </td>
                        </tr>
						<tr>
							<td style="width: 25%">
								<asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Text="Reference No"></asp:Label>
							</td>
							<td>

								<asp:Label ID="lblRefNo" runat="server" Text="-" CssClass="form-control-label"></asp:Label>
							</td>
							<td style="width: 25%">
								<asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Text="Duty Date/Time"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lblDutyDate" runat="server" CssClass="form-control-label" Text="-"></asp:Label>
							</td>
						</tr>
						<tr>
							<td>
								<asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Text="Depot"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lblDepot" runat="server" Text="-" CssClass="form-control-label"></asp:Label>
							</td>
							<td>
								<asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Text="Route"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lblRoute" runat="server" Text="-" CssClass="form-control-label"></asp:Label>
							</td>
						</tr>
						<tr>
                            <td colspan="12" >
                                <asp:Label ID="Label2" runat="server" Text="2. Bus Details" Font-Underline="true" Font-Size="Medium" CssClass="form-control-label ml--10"
                                    Font-Bold="true"  class="text-center"></asp:Label>
                            </td>
                        </tr>
						<tr>
							<td>
								<asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Text="Bus No"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lblBusNo" runat="server" CssClass="form-control-label" Text="-"></asp:Label>
							</td>
							<td>
								<asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Text="Odometer Reading"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lblOdometerReading" runat="server" Text="-" CssClass="form-control-label"></asp:Label>
							</td>
						</tr>
						<tr>
							<td>
								<asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Text="Insurane No"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lblInsuranceNo" runat="server" Text="-" CssClass="form-control-label"></asp:Label>
							</td>
							<td>
								<asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Text="Insurane Validity"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lblInsuranceValidity" runat="server" Text="-" CssClass="form-control-label"></asp:Label>
							</td>
						</tr>
						<tr>
							<td>
								<asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Text="Pollution Certificate No"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lblPollutionNo" runat="server" Text="-" CssClass="form-control-label"></asp:Label>
							</td>
							<td>
								<asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Text="Pollution Validity"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lblPollutionValidity" runat="server" Text="-" CssClass="form-control-label"></asp:Label>
							</td>
						</tr>
						<tr>
							<td>
								<asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Text="Fitness Certificate No"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lblFitnessNo" runat="server" Text="-" CssClass="form-control-label"></asp:Label>
							</td>
							<td>
								<asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Text="Fitness Validity"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lblFitnessValidity" runat="server" Text="-" CssClass="form-control-label"></asp:Label>
							</td>
						</tr>
						<tr>
                            <td colspan="12" >
                                <asp:Label ID="Label3" runat="server" Text="3. Crew Details" Font-Underline="true" Font-Size="Medium" CssClass="form-control-label ml--10"
                                    Font-Bold="true"  class="text-center"></asp:Label>
                            </td>
                        </tr>
						<tr>
							<td>
								<asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Text="Driver Name"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lblDriver1" runat="server" Text="-" CssClass="form-control-label"></asp:Label>
							</td>
							<td>
								<asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Text="Condutor Name"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lblConductor1" runat="server" Text="-" CssClass="form-control-label"></asp:Label>
							</td>
						</tr>
						<tr>
							<td>
								<asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Text="Driver License No"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lblDriver1LicenseNo" runat="server" Text="-" CssClass="form-control-label"></asp:Label>
							</td>
							<td>
								<asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Text="Condutor License No"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lblCond1LicenseNo" runat="server" Text="-" CssClass="form-control-label"></asp:Label>
							</td>
						</tr>
						<tr>
							<td>
								<asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Text="Driver License Validity"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lblDriver1LicenseValidity" runat="server" Text="-" CssClass="form-control-label"></asp:Label>
							</td>
							<td>
								<asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Text="Condutor License Validity"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lblCond1LicenseValidity" runat="server" Text="-" CssClass="form-control-label"></asp:Label>
							</td>
						</tr>
						<tr>
							<td colspan="4"></td>
						</tr>
						<tr>
							<td>
								<asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Text="Second Driver Name"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lblDriver2" runat="server" Text="-" CssClass="form-control-label"></asp:Label>
							</td>
							<td>
								<asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Text="Second Conductor Name"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lblConductor2" runat="server" Text="-" CssClass="form-control-label"></asp:Label>
							</td>
						</tr>
						<tr>
							<td>
								<asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Text="Second Driver License No"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lblDriver2LicenseNo" runat="server" Text="-" CssClass="form-control-label"></asp:Label>
							</td>
							<td>
								<asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Text="Second Condutor License No"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lblConductor2LicenseNo" runat="server" Text="-" CssClass="form-control-label"></asp:Label>
							</td>
						</tr>
						<tr>
							<td>
								<asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Text="Second Driver License Validity"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lblDriver2LicenseValidity" runat="server" Text="-" CssClass="form-control-label"></asp:Label>
							</td>
							<td>
								<asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Text="Second Condutor License Validity"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lblCond2LicenseValidity" runat="server" Text="-" CssClass="form-control-label"></asp:Label>
							</td>
						</tr>
						<tr>
							<td colspan="4"></td>
						</tr>
						<tr>
							<td>
								<asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Text="Target Income"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lblTargetIncome" runat="server" Text="-" CssClass="form-control-label"></asp:Label>
							</td>
							<td>
								<asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Text="Target Diesel Average"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lblTargetDieselAvg" runat="server" Text="-" CssClass="form-control-label"></asp:Label>
							</td>
						</tr>
						<tr>
							<td>
								<asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Text="Schedule Km."></asp:Label>
							</td>
							<td>
								<asp:Label ID="lblScheduleKm" runat="server" Text="-" CssClass="form-control-label"></asp:Label>
							</td>
							<td>
								<asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Text="Change Station"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lblChangeStation" runat="server" Text="-" CssClass="form-control-label"></asp:Label>
							</td>
						</tr>
						<tr>
							<td>
								<asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Text="Service Name"></asp:Label>
							</td>
							<td colspan="3" style="text-align: left;">
								<asp:Label ID="lblServiceName" runat="server" Text="-" CssClass="form-control-label"></asp:Label>
							</td>
						</tr>
						<tr>
							<td>
								<asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Text="Permit No"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lblPermitNo" runat="server" CssClass="form-control-label" Text="-"></asp:Label>
							</td>
							<td>
								<asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Text="Permit Validity"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lblPermitValidity" runat="server" Text="-" CssClass="form-control-label"></asp:Label>
							</td>
						</tr>
						<tr>
							<td>
								<asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Text="Alloted ETM Serial No"></asp:Label>
							</td>
							<td>
								<asp:Label ID="lblETM" runat="server" Text="-" CssClass="form-control-label"></asp:Label>
							</td>
							<td>
								<asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Text="Manual Ticket Book"></asp:Label>
							</td>
							<td colspan="2">
								<asp:Label ID="lblTicketBook" runat="server" CssClass="form-control-label" Text="-"></asp:Label>
							</td>
						</tr>
						<tr runat="server" id="trDenominationBook" visible="false">
							<td colspan="4">
								<h4>
									<asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Text="Ticket Denominations Book"></asp:Label>
								</h4>
								<asp:GridView ID="gvDenomination" runat="server" AutoGenerateColumns="false" GridLines="None"
									CssClass="table table-bordered">
									<Columns>
										<asp:BoundField DataField="DENOMINATIONID" HeaderText="Denomination" />
										<asp:BoundField DataField="STARTSERIALNO" HeaderText="From Serial No" />
										<asp:BoundField DataField="ENDSERIALNO" HeaderText="To Serial No" />
										<asp:BoundField DataField="TOTALAMOUNT" HeaderText="Total Amount" />
									</Columns>
								</asp:GridView>
							</td>
						</tr>
						<tr>
							<td colspan="4">
								<br />
								<br />
							</td>
						</tr>
						<tr>
							<td><b><span>Prepared By:</span></b></td>
							<td colspan="2">
								<asp:Label ID="lblJIName" runat="server" Font-Size="9pt" Text="N/A" Font-Names="Verdana"></asp:Label>
								(<asp:Label ID="lblJIDesignation" runat="server" Font-Size="9pt" Text="N/A" Font-Names="Verdana"></asp:Label>)
							</td>
							<td colspan="2"></td>
						</tr>
						<tr>
							<td><b><span>Duty Slip Date/Time:</span></b></td>
							<td colspan="2">
								<asp:Label ID="lblprintdatetime" runat="server" Font-Size="9pt" Text="-" Font-Names="Verdana"></asp:Label></td>
							<td colspan="2"></td>
						</tr>
					</table>
					<br />
					<br />
					<center>
                        <asp:Label ID="lblHelpdeskdept" runat="server" Text="For any query please contact StarBus helpdesk"
                            Font-Bold="true" ></asp:Label></center>
					<br />
				</div>
				<center>
                    <asp:Label ID="msg" runat="server" Text="Sorry Something went wrong. please contact StarBus helpdesk"
                        Visible="false"></asp:Label></center>
			</div>
		</div>
		<asp:HiddenField ID="hidtoken" runat="server"></asp:HiddenField>
	</form>
</body>
</html>
