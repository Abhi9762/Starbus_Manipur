<%@ Page Language="C#" AutoEventWireup="true" CodeFile="dashEtmCollectionNew.aspx.cs" MaintainScrollPositionOnPostback="true" Inherits="Auth_dashEtmCollectionNew" %>

<!DOCTYPE html>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="icon" href="../assets/img/brand/favicon.png" type="image/png" />
    <!-- Fonts -->
    <!-- Icons -->
    <link rel="stylesheet" href="../assets/vendor/nucleo/css/nucleo.css" type="text/css" />
    <link rel="stylesheet" href="../assets/vendor/@fortawesome/fontawesome-free/css/all.min.css" type="text/css" />
    <!-- Page plugins -->
    <!-- Argon CSS -->
    <link rel="stylesheet" href="../assets/css/argon.css?v=1.2.0" type="text/css" />
    <link href="../assets/css/jquery.dataTables.min.css" rel="stylesheet" />

    <script type="text/javascript" src="../assets/js/jquery-n.js"></script>
    <script src="../assets/js/jquery-ui.js" type="text/javascript"></script>
    <script src="../assets/vendor/bootstrap/dist/js/bootstrap.bundle.min.js"></script>
    <script src="../assets/vendor/js-cookie/js.cookie.js"></script>
    <script src="../assets/vendor/jquery.scrollbar/jquery.scrollbar.min.js"></script>
    <script src="../assets/vendor/jquery-scroll-lock/dist/jquery-scrollLock.min.js"></script>
    <script src="../assets/vendor/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js"></script>


    <script src="../DataTables/js/jquery.dataTables.min.js"></script>
    <script src="../DataTables/js/dataTables.buttons.min.js"></script>
    <script src="../DataTables/js/buttons.print.min.js"></script>
    <script src="../DataTables/js/buttons.html5.min.js"></script>
    <script src="../DataTables/js/pdfmake.min.js"></script>
    <script src="../DataTables/js/vfs_fonts.js"></script>
    <script src="../DataTables/js/jszip.min.js"></script>
    <!-- Optional JS -->
    <script src="../assets/vendor/chart.js/dist/Chart.min.js"></script>
    <script src="../assets/vendor/chart.js/dist/Chart.extension.js"></script>
    <link href="../css/paging.css" rel="stylesheet" />
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
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="container-fluid watermark" id="maindiv">
            <div class="card">
                <div class="card-header">
                    <div class="row">
                        <div class="col-lg-12 text-center">
                            <h4>
                                <asp:Label runat="server" class="" Style="font-size: 13pt;" ID="lblHeading"></asp:Label>
                            </h4>
                            <h2 style="font-size: 10pt" class="text-center">Waybill Collection & Expenditure Details</h2>
                            <asp:LinkButton ID="lbtnPrint" CssClass="btn btn-danger btn-sm float-right" Style="margin-top: -40px;" runat="server" OnClientClick="printDiv('maindiv')"><i class="fa fa-print"></i> Print</asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div class="card-body">
                    <div class="row mt-2">
                        <div class="col-sm-4">
                            <i class="fa fa-road"></i>
                            <asp:Label runat="server" Text="Waybill No" Font-Bold="true" CssClass="form-control-label"></asp:Label>
                            -
                            <asp:Label runat="server" ID="tbWaybillNo" CssClass="form-control-label"></asp:Label>
                        </div>
                        <div class="col-sm-4">
                            <i class="fa fa-bus mr-1"></i>
                            <asp:Label runat="server" Text="Bus No" Font-Bold="true" CssClass="form-control-label"></asp:Label>-
                            <asp:Label runat="server" ID="lblBusNo" CssClass="form-control-label text-uppercase"></asp:Label>
                        </div>
                        <div class="col-sm-4">
                            <i class="fa fa-mobile mr-1"></i>
                            <asp:Label runat="server" Text="ETM Serial No" Font-Bold="true" CssClass="form-control-label"></asp:Label>-
                            <asp:Label runat="server" ID="lblETMNo" CssClass="form-control-label text-uppercase"></asp:Label>
                        </div>
                        <div class="col-sm-12">
                            <i class="fa fa-server mr-1"></i>
                            <asp:Label runat="server" Text="Service Name" Font-Bold="true" CssClass="form-control-label"></asp:Label>-
                            <asp:Label runat="server" ID="lblServiceName" CssClass="form-control-label text-uppercase"></asp:Label>
                        </div>
                    </div>
                    <hr style="margin: 10px" />
                    <div class="row">
                        <div class="col-sm-3">
                            <h4 class="mb-0">
                                <i class="fa fa-users mr-1"></i>
                                <asp:Label runat="server">Crew Details</asp:Label></h4>
                        </div>
                        <div class="col-sm-4">
                            <i class="fa fa-user mr-1"></i>
                            <asp:Label runat="server" Text="Driver" Font-Bold="true" CssClass="form-control-label"></asp:Label>-
                            <asp:Label runat="server" CssClass="form-control-label text-uppercase" ID="lblDriver"></asp:Label>
                        </div>
                        <div class="col-sm-4">
                            <i class="fa fa-user mr-1"></i>
                            <asp:Label runat="server" Text="Conductor" Font-Bold="true" CssClass="form-control-label"></asp:Label>-
                            <asp:Label runat="server" CssClass="form-control-label text-uppercase" ID="lblConductor"></asp:Label>
                        </div>
                    </div>
                    <hr style="margin: 10px" />
                    <div class="row">
                        <div class="col-sm-12">
                            <h4>
                                <asp:Label runat="server">1. Collection Details</asp:Label></h4>
                        </div>
                        <div class="col-sm">
                            <asp:Label runat="server" Text="Online Ticketsaa " CssClass="form-control-label">Online Tickets</asp:Label>
                            <asp:Label runat="server" Font-Size="16px" Font-Bold="true" Text="0 ₹" ID="tbOnlineTktamt" CssClass="form-control-label"></asp:Label>
                        </div>
                        <div class="col-sm">
                            <asp:Label runat="server" Text="Enroute Tickets" CssClass="form-control-label">Enroute Tickets</asp:Label>
                            <asp:Label runat="server" Font-Size="16px" Font-Bold="true" ID="tbEnrouteTktamt" Text="0 ₹" CssClass="form-control-label"></asp:Label>
                            <hr style="margin-top: 0px; margin-bottom: 0px" />
                            <asp:Label runat="server" Font-Size="10px" Text="Cash" CssClass="form-control-label"></asp:Label>
                            <asp:Label runat="server" Font-Size="12px" Font-Bold="true" ID="tbenroutecash" Text="0 ₹" CssClass="form-control-label"></asp:Label>
                            <asp:Label runat="server" Font-Size="10px" Text="QR" CssClass="form-control-label ml-3"></asp:Label>
                            <asp:Label runat="server" Font-Size="12px" Font-Bold="true" ID="tbenrouteqr" Text="0 ₹" CssClass="form-control-label"></asp:Label>

                        </div>
                        <div class="col-sm">
                            <asp:Label runat="server" Text="Luggage" CssClass="form-control-label">Luggage</asp:Label>
                            <asp:Label runat="server" Font-Size="16px" ID="tbLuggageAmt" Font-Bold="true" Text="0 ₹" CssClass="form-control-label"></asp:Label>
                            <hr style="margin-top: 0px; margin-bottom: 0px" />
                            <asp:Label runat="server" Font-Size="10px" Text="Cash" CssClass="form-control-label"></asp:Label>
                            <asp:Label runat="server" Font-Size="12px" Font-Bold="true" ID="tbluggagecash" Text="0 ₹" CssClass="form-control-label"></asp:Label>
                            <asp:Label runat="server" Font-Size="10px" Text="QR" CssClass="form-control-label ml-3"></asp:Label>
                            <asp:Label runat="server" Font-Size="12px" Font-Bold="true" ID="tbluggageqr" Text="0 ₹" CssClass="form-control-label"></asp:Label>

                        </div>
                        <div class="col-sm">
                            <asp:Label runat="server" Text="Dhaba" CssClass="form-control-label"></asp:Label>
                            <asp:Label runat="server" Font-Size="16px" ID="tbDhabaAmt" Font-Bold="true" Text="0 ₹" CssClass="form-control-label"></asp:Label>
                        </div>
                        <div class="col-sm">
                            <asp:Label runat="server" Text="Other Earning" CssClass="form-control-label"></asp:Label>
                            <asp:Label runat="server" Font-Size="16px" ID="tbOtherEarningAmt" Font-Bold="true" Text="0 ₹" CssClass="form-control-label"></asp:Label>
                            <hr style="margin-top: 0px; margin-bottom: 0px" />
                            <asp:Label runat="server" Font-Size="10px" Text="Cash" CssClass="form-control-label"></asp:Label>
                            <asp:Label runat="server" Font-Size="12px" Font-Bold="true" ID="tbotherEarningcash" Text="0 ₹" CssClass="form-control-label"></asp:Label>
                            <asp:Label runat="server" Font-Size="10px" Text="QR" CssClass="form-control-label ml-3"></asp:Label>
                            <asp:Label runat="server" Font-Size="12px" Font-Bold="true" ID="tbotherEarningqr" Text="0 ₹" CssClass="form-control-label"></asp:Label>

                        </div>
                        <div class="col-sm">
                            <asp:Label runat="server" Text="Refunded Tickets" Font-Bold="true" CssClass="form-control-label">Refund Ticket</asp:Label>
                            <asp:Label runat="server" Font-Size="16px" ID="tbRefundedTickets" Font-Bold="true" Text="0 ₹" CssClass="form-control-label"></asp:Label>
                        </div>
                        <div class="col-sm">
                            <asp:Label runat="server" Text="Total " CssClass="form-control-label"></asp:Label>
                            <asp:Label runat="server" Font-Size="16px" Font-Bold="true" ID="tbTotalEarningAmt" Text="0 ₹" CssClass="form-control-label"></asp:Label>
                            <hr style="margin-top: 0px; margin-bottom: 0px" />
                            <asp:Label runat="server" Font-Size="10px" Text="Cash" CssClass="form-control-label"></asp:Label>
                            <asp:Label runat="server" Font-Size="12px" Font-Bold="true" ID="tbtotalcollectioncash" Text="0 ₹" CssClass="form-control-label"></asp:Label>
                            <asp:Label runat="server" Font-Size="10px" Text="QR" CssClass="form-control-label ml-3"></asp:Label>
                            <asp:Label runat="server" Font-Size="12px" Font-Bold="true" ID="tbtotalcollectionqr" Text="0 ₹" CssClass="form-control-label"></asp:Label>

                        </div>
                    </div>
                    <hr style="margin: 10px" />
                    <div class="row">
                        <div class="col-sm-12">
                            <h4>
                                <asp:Label runat="server">2. Expenditure Details</asp:Label></h4>
                        </div>
                        <div class="col-sm">
                            <asp:Label runat="server" Text="Toll Paid" CssClass="form-control-label">Toll Paid </asp:Label>

                            <asp:Label class="form-control-label" Font-Size="16px" runat="server" ID="tbTollpaid" Text="0 ₹"
                                placeholder="Max 6 Digit"></asp:Label>
                        </div>
                        <div class="col-sm">
                            <asp:Label runat="server" Text="Parking" CssClass="form-control-label">Parking</asp:Label>

                            <asp:Label class="form-control-label" runat="server" Font-Bold="true" ID="tbParking" Text="0 ₹" Font-Size="16px"></asp:Label>
                        </div>
                        <div class="col-sm">
                            <asp:Label runat="server" Text="Other Expense" CssClass="form-control-label">Other Expense</asp:Label>

                            <asp:Label class="form-control-label" runat="server" Font-Bold="true" ID="tbOtherExp" Text="0 ₹" Font-Size="16px"></asp:Label>
                        </div>
                        <div class="col-sm">
                            <asp:Label runat="server" Text="Total " CssClass="form-control-label"> Total</asp:Label>

                            <asp:Label class="form-control-label" runat="server" Font-Bold="true" ID="tbTotalExpenses" Text="0 ₹" Font-Size="16px"
                                placeholder="Max 6 Digit"></asp:Label>
                        </div>
                    </div>
                    <hr style="margin: 10px" />
                    <div class="row" runat="server" id="div2">
                        <div class="col-lg-6">
                            <h4>
                                <asp:Label runat="server">3. Inspection Details</asp:Label></h4>
                        </div>
                        <div class="col-lg-6">
                            <h4>
                                <asp:Label runat="server">4. Concessional Journey Details</asp:Label></h4>
                        </div>
                        <div class="col-lg-6">
                            <div class="row">
                                <div class="col-sm">
                                    <asp:Label runat="server" Text="" Font-Bold="true" CssClass="form-control-label">Total Inspections</asp:Label>
                                    <asp:Label class="form-control-label" Font-Size="16px" runat="server" ID="lblNoofInspection" Text="0"
                                        placeholder="Max 6 Digit"></asp:Label>
                                </div>
                                <div class="col-sm">
                                    <asp:Label runat="server" Text="Parking" Font-Bold="true" CssClass="form-control-label">OK Inspections</asp:Label>

                                    <asp:Label class="form-control-label" runat="server" Font-Bold="true" ID="lblokOnspection" Text="0" Font-Size="16px"></asp:Label>
                                </div>
                                <div class="col-sm">
                                    <asp:Label runat="server" Text="Other Expense" Font-Bold="true" CssClass="form-control-label">WT Inspections</asp:Label>

                                    <asp:Label class="form-control-label " runat="server" Font-Bold="true" ID="lblwtInspections" Text="0" Font-Size="16px"></asp:Label>

                                </div>

                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="row">
                                <div class="col-sm">
                                    <asp:Label runat="server" Text="" Font-Bold="true" CssClass="form-control-label">Total Concessional Tickets</asp:Label>
                                    <asp:Label class="form-control-label" Font-Size="16px" runat="server" ID="lbltotalConcession" Text="0"
                                        placeholder="Max 6 Digit"></asp:Label>
                                </div>
                                <div class="col-sm">
                                    <asp:Label runat="server" Text="Parking" Font-Bold="true" CssClass="form-control-label">Concessional Amount</asp:Label>

                                    <asp:Label class="form-control-label" runat="server" Font-Bold="true" ID="lblConcessiontktamt" Text="0 ₹" Font-Size="16px"></asp:Label>
                                </div>
                                <div class="col-sm">
                                    <asp:Label runat="server" Text="Other Expense" Font-Bold="true" CssClass="form-control-label">Discount Amount</asp:Label>

                                    <asp:Label class="form-control-label" runat="server" Font-Bold="true" ID="lblconcessiondiscountamt" Text="0 ₹" Font-Size="16px"></asp:Label>

                                </div>

                            </div>
                        </div>
                    </div>
                    <hr style="margin: 10px" />
                    <div class="row">
                        <div class="col-sm-12">
                            <h4>
                                <asp:Label runat="server">5. Trip Details</asp:Label>(<i class="fa fa-rupee-sign"></i>)</h4>
                        </div>
                        <div class="col-lg-12">
                            <asp:GridView ID="gvServiceTrips" runat="server" AutoGenerateColumns="false" GridLines="None"
                                AllowPaging="true" PageSize="10" CssClass="table table-bordered text-center" DataKeyNames="">
                                <Columns>
                                    <asp:TemplateField HeaderText="Trip No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTripNumber" Text='<%# Eval("tripno") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="In Waybill">
                                        <ItemTemplate>
                                            <asp:Label ID="lblwayb" Text='<%# Eval("in_waybill") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="No. of Passenger">
                                        <ItemTemplate>
                                            <asp:Label class="form-control-label" runat="server" ID="tbPassengerNo" Text='<%# Eval("passenger") %>'></asp:Label>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount(₹)">
                                        <ItemTemplate>
                                            <asp:Label class="form-control-label" runat="server" ID="tbAmount" Text='<%# Eval("amt") %>'></asp:Label>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Subjected Km">
                                        <ItemTemplate>
                                            <asp:Label class="form-control-label" runat="server" ID="tbSubKm" MaxLength="50" Text='<%# Eval("sub_km") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Actual Km">
                                        <ItemTemplate>
                                            <asp:Label class="form-control-label" runat="server" Text='<%# Eval("act_km") %>' ID="tbActualKm"></asp:Label>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle HorizontalAlign="Right" CssClass="GridPager" ForeColor="Black" />
                            </asp:GridView>
                            <asp:Panel ID="pnlNoRecordTrip" runat="server" Width="100%" Visible="true">
                                <div class="col-md-12 p-0" style="text-align: center;">
                                    <div class="col-md-12 busListBox" style="color: #e3e3e3; padding-top: 50px; padding-bottom: 50px; font-size: 33px; font-weight: bold;">
                                        Trips Not Available<br />
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                    <div runat="server" id="dvPrematureWaybill" visible="true">
                        <hr style="margin: 10px" />
                        <div class="row">
                            <div class="col-lg-12">
                                <h4>
                                    <asp:Label runat="server">6. Premature Waybill Earning & Expenses</asp:Label></h4>
                            </div>
                            <div class="col-lg-12">
                                <div class="row">
                                    <div class="col-sm">
                                        <asp:Label runat="server" Font-Bold="true" CssClass="form-control-label">Enroute Tickets </asp:Label>
                                        <br />
                                        <asp:Label class="form-control-label" Font-Size="14px" runat="server" ID="tbpEnroteTickets" Text="0"></asp:Label>

                                    </div>
                                    <div class="col-sm">
                                        <asp:Label runat="server" Font-Bold="true" CssClass="form-control-label">Enroute Luggage </asp:Label><br />
                                        <asp:Label class="form-control-label" Font-Size="14px" runat="server" ID="tbpEnrouteLuggage" Text="0"></asp:Label>
                                    </div>
                                    <div class="col-sm">
                                        <asp:Label runat="server" Font-Bold="true" CssClass="form-control-label">Dhaba</asp:Label><br />
                                        <asp:Label class="form-control-label" Font-Size="14px" runat="server" ID="tbpDhaba" Text="0"></asp:Label>
                                    </div>
                                    <div class="col-sm">
                                        <asp:Label runat="server" Font-Bold="true" CssClass="form-control-label">Other Earning</asp:Label><br />
                                        <asp:Label class="form-control-label" Font-Size="14px" runat="server" ID="tbpOtherEarning" Text="0"></asp:Label>

                                    </div>
                                    <div class="col-sm">
                                        <asp:Label runat="server" Font-Bold="true" CssClass="form-control-label">Toll Paid </asp:Label><br />
                                        <asp:Label class="form-control-label" Font-Size="14px" runat="server" ID="tbpTollpaid" Text="0"></asp:Label>

                                    </div>
                                    <div class="col-sm">
                                        <asp:Label runat="server" Font-Bold="true" CssClass="form-control-label">Parking</asp:Label><br />

                                        <asp:Label class="form-control-label" Font-Size="14px" runat="server" ID="tbpParking" Text="0"></asp:Label>

                                    </div>
                                    <div class="col-sm">
                                        <asp:Label runat="server" Font-Bold="true" CssClass="form-control-label">Other Expense</asp:Label><br />

                                        <asp:Label class="form-control-label" Font-Size="14px" runat="server" ID="tbpOtherExpense" Text="0"></asp:Label>
                                    </div>
                                    <div class="col-sm">
                                        <asp:Label runat="server" Font-Bold="true" CssClass="form-control-label"> Total</asp:Label><br />

                                        <asp:Label class="form-control-label" Font-Size="14px" runat="server" Enabled="false" ID="tbpTotal" Text="0"></asp:Label>


                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div runat="server" id="dvExcessWavierAmount" visible="true">
                        <hr style="margin: 10px" />
                        <div class="row">
                            <div class="col-lg-12">
                                <h4>
                                    <asp:Label runat="server">6. Excess Amount Wavier In Waybill</asp:Label></h4>
                            </div>
                            <div class="col-lg-8">
                                <div class="row">
                                    <div class="col-sm">
                                        <asp:Label runat="server" Text="Refrence No." CssClass="form-control-label">Refrence No.</asp:Label>
                                        <asp:Label runat="server" Font-Size="16px" Font-Bold="true" Text="0" ID="lblRefrenecno" CssClass="form-control-label"></asp:Label>
                                    </div>
                                    <div class="col-sm">
                                        <asp:Label runat="server" Text="Refrence No." CssClass="form-control-label">Actual Amount</asp:Label>
                                        <asp:Label runat="server" Font-Size="16px" Font-Bold="true" Text="0 ₹" ID="lblActualAmt" CssClass="form-control-label"></asp:Label>
                                    </div>
                                    <div class="col-sm">
                                        <asp:Label runat="server" Text="Refrence No." CssClass="form-control-label">Excess Amount</asp:Label>
                                        <asp:Label runat="server" Font-Size="16px" Font-Bold="true" Text="0 ₹" ID="lblExcessAmt" CssClass="form-control-label"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row m-0">
                        <div class="col-lg-12 pt-4 ml-2 pb-2 text-center">
                            <asp:Label runat="server" ID="lblDepositedAmt" Font-Size="Medium" Text="Amount To Be Deposited By Conductor " Font-Bold="true" CssClass="form-control-label mr-1"> Amount To Be Deposited By Conductor(<i class="fa fa-rupee-sign"></i>)</asp:Label>


                        </div>
                    </div>
                    <%--                            
                            
                            <div class="row" runat="server" id="divtextBox">
                                
                                <div class="col-lg-12" style="border-right-style: solid; border-color: lightgray; border-width: 1px;">
                                    <div class="row">
                                        12">
                                    <h4>
                                        <asp:Label runat="server">2. Expenditure Details</asp:Label>(<i class="fa fa-rupee-sign"></i>)</h4>
                                </div>
                                <div class="col-lg-5">
                                    <div class="row">
                                        <div class="col-lg">
                                            <asp:Label runat="server" Text="Toll Paid" Font-Bold="true" CssClass="form-control-label">Toll Paid (<i class="fa fa-rupee-sign"></i>)</asp:Label>

                                            <br />
                                            <asp:TextBox class="form-control" runat="server" ReadOnly="true" ID="tbTollpaid" Text="0" MaxLength="6" autocomplete="off" ToolTip="Enter Dhaba Collection" AutoPostBack="true" 
                                                placeholder="Max 6 Digit" Style="font-size: 10pt; height: 30px;"></asp:TextBox>
                                            &nbsp;<asp:Label ID="Label7" Text="N/A" CssClass="form-control-label" Visible="false" runat="server" />

                                        </div>
                                        <div class="col-lg">
                                            <asp:Label runat="server" Text="Parking" Font-Bold="true" CssClass="form-control-label">Parking(<i class="fa fa-rupee-sign"></i>)</asp:Label>

                                            <br />
                                            <asp:TextBox class="form-control" runat="server" ID="tbParking" ReadOnly="true" Text="0" MaxLength="10" autocomplete="off" ToolTip="Enter Total Amount"
                                                 Style="font-size: 10pt; height: 30px;"></asp:TextBox>
                                            &nbsp;
										<asp:Label ID="Label8" Text="N/A" CssClass="form-control-label" Visible="false" runat="server" />

                                        </div>
                                        <div class="col-lg">
                                            <asp:Label runat="server" Text="Other Expense" Font-Bold="true" CssClass="form-control-label">Other Expense(<i class="fa fa-rupee-sign"></i>)</asp:Label>

                                            <br />
                                            <asp:TextBox class="form-control" runat="server" ID="tbOtherExp" ReadOnly="true" Text="0" MaxLength="10" autocomplete="off" ToolTip="Enter Total Amount"
                                                 Style="font-size: 10pt; height: 30px;"></asp:TextBox>
                                            &nbsp;
										<asp:Label ID="Label9" Text="N/A" CssClass="form-control-label" Visible="false" runat="server" />

                                        </div>
                                        <div class="col-lg">
                                            <asp:Label runat="server" Text="Total " Font-Bold="true" CssClass="form-control-label"> Total(<i class="fa fa-rupee-sign"></i>)</asp:Label>

                                            <br />
                                            <asp:TextBox class="form-control" runat="server" ReadOnly="true" ID="tbTotalExpenses" Text="0" MaxLength="6" autocomplete="off" ToolTip="Enter Toll Paid" AutoPostBack="true" 
                                                placeholder="Max 6 Digit" Style="font-size: 10pt; height: 30px;"></asp:TextBox>
                                            &nbsp;
										<asp:Label ID="Label10" Text="N/A" CssClass="form-control-label" Visible="false" runat="server" />

                                        </div>
                                    </div>
                                </div>

                            </div>

                            <hr style="margin: 10px" />
                            <div class="row">
                                <div class="col-lg-12">
                                    <h4>
                                        <asp:Label runat="server">3. Trip Details</asp:Label></h4>
                                </div>
                            </div>
                            <div class="row mt-1">
                                <div class="col-lg-12">
                                    <asp:GridView ID="gvServiceTrips" runat="server" AutoGenerateColumns="false" GridLines="None"
                                        AllowPaging="true" PageSize="10" CssClass="table table-bordered text-center" DataKeyNames="">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Trip No" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTripNumber" Text='<%# Eval("tripno") %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="No of Passenger">
                                                <ItemTemplate>
                                                    <asp:TextBox class="form-control" ReadOnly="true" runat="server" ID="tbPassengerNo" Text='<%# Eval("passenger") %>' MaxLength="50" autocomplete="off"
                                                        placeholder="Enter No." Style="font-size: 10pt; height: 30px; width: 100px; display: inline;"></asp:TextBox>
                                                    <asp:Label ID="lblPassengerNo" Text="N/A" CssClass="form-control-label" Visible="false" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount">
                                                <ItemTemplate>
                                                    <asp:TextBox class="form-control" runat="server" ID="tbAmount" Text='<%# Eval("amt") %>' MaxLength="10" autocomplete="off" AutoPostBack="true" 
                                                        placeholder="Enter Amount" ReadOnly="true" Style="font-size: 10pt; height: 30px; width: 100px; display: inline;"></asp:TextBox>
                                                    <i class="fa fa-rupee" style="font-size: 12pt;"></i>
                                                    <asp:Label ID="lblAmount" Text="N/A" CssClass="form-control-label" Visible="false" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Subjected Km">
                                                <ItemTemplate>
                                                    <asp:TextBox class="form-control" runat="server" ID="tbSubKm" MaxLength="50" Text='<%# Eval("distancekm") %>' autocomplete="off" ReadOnly="true"
                                                        placeholder="" Style="font-size: 10pt; height: 30px; display: inline; width: 60px;"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Actual Km">
                                                <ItemTemplate>
                                                    <asp:TextBox class="form-control" runat="server" ID="tbActualKm" MaxLength="50" autocomplete="off"
                                                        placeholder="" Style="font-size: 10pt; height: 30px; display: inline; width: 60px;"></asp:TextBox>
                                                 
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Right" CssClass="GridPager" ForeColor="Black" />
                                    </asp:GridView>
                                </div>
                            </div>--%>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
