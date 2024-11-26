<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/ETMBranchMaster.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="ETMBranchWayBillGenerate.aspx.cs" Inherits="ETMBranchWayBillGenerate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .ModalPopupBG {
            background-color: #666699;
            filter: alpha(opacity=50);
            opacity: 0.7;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {

            var currDate = new Date().getDate();
            var preDate = new Date(new Date().setDate(currDate - 1));
            var nextDate = new Date(new Date().setDate(currDate + 1));
            var todayDate = new Date(new Date().setDate(currDate));

            $('[id*=tbDutyDate]').datepicker({
                startDate: todayDate,
                endDate: nextDate,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            });
            $('[id*=tbToDate]').datepicker({
                endDate: nextDate,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            });
            $('[id*=tbFromDate]').datepicker({
                endDate: nextDate,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            });
        });

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid pt-3 pb-5">
        <div class="row">
            <div class="col-lg-12">
                <asp:Panel runat="server" ID="pnlSummary" Visible="true">
                    <div class="card card-stats mb-2 m-0">
                        <div class="row m-0 py-1 mb-2 text-center">
                            <div class="col-lg-2 mt-2 border-right">
                                <div class="row m-0 text-center">
                                    <div class="col-md-12 pl-0">
                                        <h4 class="mb-0" runat="server" visible="true">Waybill's</h4>
                                        <div class="row">
                                            <div class="col-lg-6 border-right">
                                                <asp:Label ID="lblTotalSlip" runat="server" Text="0" CssClass="form-control-label"></asp:Label>
                                                <br />
                                                <asp:Label runat="server" CssClass="form-control-label text-sm"> Total</asp:Label>
                                            </div>
                                            <div class="col-lg-6">
                                                <asp:Label ID="lblPendingDutyaAllocation" runat="server" Text="0" CssClass="form-control-label"></asp:Label>
                                                <br />
                                                <asp:Label runat="server" CssClass="form-control-label text-sm"> Pending Duty Slip</asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-9 pt-2 border-right">
                                <div class="row m-0 text-center">
                                    <div class="col-md-3 pl-0 border-right">

                                        <h4 class="mb-0" runat="server" visible="true">Buses</h4>
                                        <div class="row">
                                            <div class="col-lg-6 border-right">
                                                <asp:Label ID="lblTotBus" runat="server" Text="0" CssClass="form-control-label"></asp:Label>
                                                <br />
                                                <asp:Label runat="server" CssClass="form-control-label text-sm"> Total Buses</asp:Label>
                                            </div>
                                            <div class="col-lg-6">
                                                <asp:Label ID="lblBusonDuty" runat="server" Text="0" CssClass="form-control-label"></asp:Label>
                                                <br />
                                                <asp:Label runat="server" CssClass="form-control-label text-sm"> Allocated to Duty</asp:Label>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="col-md-3 pl-0 border-right">
                                        <h4 class="mb-0" runat="server" visible="true">Drivers</h4>
                                        <div class="row">
                                            <div class="col-lg-6 border-right">
                                                <asp:Label ID="lbltotDrivers" runat="server" Text="0" CssClass="form-control-label"></asp:Label>
                                                <br />
                                                <asp:Label runat="server" CssClass="form-control-label text-sm"> Total Present </asp:Label>
                                            </div>
                                            <div class="col-lg-6">
                                                <asp:Label ID="lblOnDutyDriver" runat="server" Text="0" CssClass="form-control-label"></asp:Label>
                                                <br />
                                                <asp:Label runat="server" CssClass="form-control-label text-sm"> Allocated to Duty </asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3 pl-0 border-right">
                                        <h4 class="mb-0" runat="server" visible="true">Conductors</h4>
                                        <div class="row">
                                            <div class="col-lg-6" style="border-right: 1px solid #eee;">
                                                <asp:Label ID="lbltotConductors" runat="server" Text="0" CssClass="form-control-label"></asp:Label>
                                                <br />
                                                <asp:Label runat="server" CssClass="form-control-label text-sm">Total Present </asp:Label>

                                            </div>
                                            <div class="col-lg-6">
                                                <asp:Label ID="lblOnDutyConductor" runat="server" Text="0" CssClass="form-control-label"></asp:Label>
                                                <br />
                                                <asp:Label runat="server" CssClass="form-control-label text-sm">Allocated to Duty </asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3 pl-0">
                                        <h4 class="mb-0" runat="server" visible="true">ETM</h4>
                                        <div class="row">
                                            <div class="col-lg-6" style="border-right: 1px solid #eee;">
                                                <asp:Label ID="lblETMTotal" runat="server" Text="0" CssClass="form-control-label"></asp:Label>
                                                <br />
                                                <asp:Label runat="server" CssClass="form-control-label text-sm">Total ETM  </asp:Label>

                                            </div>
                                            <div class="col-lg-6">
                                                <asp:Label ID="lblETMAllocated" runat="server" Text="0" CssClass="form-control-label"></asp:Label>
                                                <br />
                                                <asp:Label runat="server" CssClass="form-control-label text-sm">Allocated to Duty </asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-1 mt-2 ">
                                <asp:LinkButton runat="server" ID="lbtnArchive" OnClick="lbtnArchive_Click" CssClass="btn btn-sm bg-gradient-orange text-white">Archive</asp:LinkButton>

                            </div>

                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel runat="server" ID="pnlUpdateDutySlips" Visible="false" Style="background: white; box-shadow: 2px 4px 25px 20px #596166c2; padding: 10px; width: 90%; margin-left: auto; margin-right: auto;">
                    <div class="col-lg-12" style="font-size: 10pt;">
                        <div class="col-lg-12">
                            <h4 class="text-center pt-1 pb-2" style="font-size: 15pt; font-weight: 700;">Generate Waybill</h4>
                            <asp:LinkButton ID="lbtnCancel" OnClick="lbtnCancel_Click" runat="server" CssClass="btn btn-danger btn-sm ml-2" Style="float: right; border-radius: 25px; top: -71px; margin-right: -30px"> <i class="fa fa-times"></i></asp:LinkButton>
                        </div>
                        <div class="col-lg-12 mb-3">
                            <div class="row m-0">
                                <div class="col-lg-8">
                                    <asp:Label runat="server" Text="Service" Font-Bold="true" CssClass="form-control-label"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblservicecode" Text="N/A"></asp:Label>
                                    |
                                    <asp:Label runat="server" ID="lblServiceName" Text="N/A"></asp:Label>
                                </div>
                                <div class="col-lg-4">
                                    <asp:Label runat="server" Text="Departure Date/Time" Font-Bold="true" CssClass="form-control-label"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblDeptTime" CssClass="form-control-label" Text="N/A"></asp:Label>
                                </div>
                            </div>
                            <div class="row m-0 mt-2">
                                <div class="col-lg-4">
                                    <asp:Label runat="server" Text="Route" Font-Bold="true" CssClass="form-control-label"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblRoute" CssClass="form-control-label" Text="N/A"></asp:Label>
                                </div>
                                <div class="col-lg-4">
                                    <asp:Label runat="server" Text="Bus" Font-Bold="true" CssClass="form-control-label"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblBus" CssClass="form-control-label text-uppercase" Text="N/A"></asp:Label>
                                </div>
                                <div class="col-lg-4">
                                    <asp:Label runat="server" Text="Bus Type" Font-Bold="true" CssClass="form-control-label"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblBusType" CssClass="form-control-label" Text="N/A"></asp:Label>
                                </div>
                            </div>
                            <div class="row m-0 mt-2">
                                <div class="col-lg-12 mb-2">
                                    <h4>
                                        <asp:Label runat="server">Crew Members</asp:Label></h4>
                                </div>
                                <div class="col-lg-4">
                                    <asp:Label runat="server" Text="Driver" Font-Bold="true" CssClass="form-control-label"></asp:Label><br />
                                    1-
                                                                                    <asp:Label runat="server" CssClass="form-control-label" ID="lblDriver" Text="N/A"></asp:Label><br />
                                    2-
                                                                                    <asp:Label runat="server" CssClass="form-control-label" ID="lblAltDriver" Text="N/A"></asp:Label>
                                </div>
                                <div class="col-lg-4">
                                    <asp:Label runat="server" Text="Conductor" Font-Bold="true" CssClass="form-control-label"></asp:Label><br />
                                    1-
                                                                                    <asp:Label runat="server" CssClass="form-control-label" ID="lblConductor" Text="N/A"></asp:Label><br />
                                    2-
                                                                                    <asp:Label runat="server" CssClass="form-control-label" ID="lblAltConductor" Text="N/A"></asp:Label>
                                </div>
                            </div>
                            <div class="row m-0 mt-2">
                                <div class="col-lg-12 mb-2">
                                    <h4>
                                        <asp:Label runat="server">Other Details</asp:Label></h4>
                                </div>
                                <div class="col-lg-2" runat="server" visible="true" id="trETM">
                                    <asp:Label runat="server" Text="Alloted ETM Serial No" Font-Bold="true" CssClass="form-control-label"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblAllotedEtm" CssClass="form-control-label" Text="N/A"></asp:Label>
                                </div>
                                <div class="col-lg-2">
                                    <asp:Label runat="server" Text="Change Station" Font-Bold="true" CssClass="form-control-label"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblChangeStation" CssClass="form-control-label" Text="N/A"></asp:Label>
                                </div>
                                <div class="col-lg-2">
                                    <asp:Label runat="server" Text="Odometer Reading" Font-Bold="true" CssClass="form-control-label"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblOdometerReading" CssClass="form-control-label" Text="N/A"></asp:Label>
                                </div>
                                <div class="col-lg-2">
                                    <asp:Label runat="server" Text="Schedule Km." Font-Bold="true" CssClass="form-control-label"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblSchKm" CssClass="form-control-label" Text="N/A"></asp:Label>
                                </div>
                                <div class="col-lg-2">
                                    <asp:Label runat="server" Text="Target Income" Font-Bold="true" CssClass="form-control-label"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblTargetIncome" CssClass="form-control-label" Text="N/A"></asp:Label>
                                </div>
                                <div class="col-lg-2">
                                    <asp:Label runat="server" Text="Target Diesel Average" Font-Bold="true" CssClass="form-control-label"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblTargetDieselAverage" CssClass="form-control-label" Text="N/A"></asp:Label>
                                </div>
                            </div>
                            <div class="row mt-3">
                                <div class="col-lg-12">
                                    <asp:GridView ID="gvServiceTrips" runat="server" AutoGenerateColumns="false" GridLines="None" CssClass="table table-hover" DataKeyNames="STRP_ID,DSVC_ID">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Trip Direction">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStartTime" runat="server" Text='<%#Eval("trip_direction") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Start Time">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStartTime" runat="server" Text='<%#Eval("START_TIME") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Station From">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFromStation" runat="server" Text='<%#Eval("fromStationName") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Station To">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblToStation" runat="server" Text='<%#Eval("toStationName") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Authorised Dhaba Amount" ItemStyle-CssClass="text-center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDhabaAmt" runat="server" Text='<%#Eval("DHABA_AMT") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                            <div class="row m-0 mt-2">
                                <div class="col-lg-12">
                                    <table class="table table2 mb-0">
                                        <tr runat="server" id="trManualTicketBook" visible="false">
                                            <td>Ticket Book Serial No </td>
                                            <td>
                                                <div class="input-group" runat="server" id="trDriver1" style="width: 280px;">
                                                    <div class="input-group-prepend" style="margin-right: 5px;">
                                                        <asp:TextBox runat="server" ID="tbFromSerialNo" Placeholder="From" CssClass="form-control" AutoPostBack="true" Width="120px"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender runat="server" ID="ajaxFt1" TargetControlID="tbFromSerialNo" FilterType="Numbers" />
                                                    </div>
                                                    To &nbsp;<asp:TextBox runat="server" ID="tbToSerialNo" Placeholder="To" CssClass="form-control" AutoPostBack="true" Width="120px"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender runat="server" ID="ajaxFt2" TargetControlID="tbToSerialNo" FilterType="Numbers" />
                                                </div>
                                            </td>
                                        </tr>
                                        <tr runat="server" id="trDenominationBook" visible="false">
                                            <td colspan="2">
                                                <table class="table table-striped">
                                                    <tr style="background: none; font-weight: 600;">
                                                        <td colspan="4" style="font-size: 12pt">Ticket Denominations Book</td>
                                                    </tr>
                                                    <tr>
                                                        <th>Denomination</th>
                                                        <th>From Serial No</th>
                                                        <th>To Serial No</th>
                                                        <th>Total Amount</th>
                                                    </tr>
                                                    <tr>
                                                        <td><b>100</b></td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="tb100StartNo" CssClass="form-control" AutoPostBack="true" placeholder="0"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender runat="server" ID="ajaxFt3" TargetControlID="tb100StartNo" FilterType="Numbers" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="tb100EndNo" CssClass="form-control" AutoPostBack="true" placeholder="0"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender runat="server" ID="ajaxFt4" TargetControlID="tb100EndNo" FilterType="Numbers" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="tb100Total" CssClass="form-control" AutoPostBack="true" placeholder="0" ReadOnly="true"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender runat="server" ID="ajaxFt5" TargetControlID="tb100Total" FilterType="Numbers" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td><b>50</b></td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="tb50StartNo" CssClass="form-control" AutoPostBack="true" placeholder="0"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender runat="server" ID="ajaxFt6" TargetControlID="tb50StartNo" FilterType="Numbers" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="tb50EndNo" CssClass="form-control" AutoPostBack="true" placeholder="0"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender runat="server" ID="ajaxFt7" TargetControlID="tb50EndNo" FilterType="Numbers" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="tb50Total" CssClass="form-control" AutoPostBack="true" placeholder="0" ReadOnly="true"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender runat="server" ID="ajaxFt8" TargetControlID="tb50Total" FilterType="Numbers" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td><b>20</b></td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="tb20StartNo" CssClass="form-control" AutoPostBack="true" placeholder="0"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender runat="server" ID="ajaxFt9" TargetControlID="tb20StartNo" FilterType="Numbers" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="tb20EndNo" CssClass="form-control" AutoPostBack="true" placeholder="0"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender runat="server" ID="ajaxFt10" TargetControlID="tb20EndNo" FilterType="Numbers" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="tb20Total" CssClass="form-control" AutoPostBack="true" placeholder="0" ReadOnly="true"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender runat="server" ID="ajaxFt11" TargetControlID="tb20Total" FilterType="Numbers" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td><b>10</b></td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="tb10StartNo" CssClass="form-control" AutoPostBack="true" placeholder="0"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender runat="server" ID="ajaxFt12" TargetControlID="tb10StartNo" FilterType="Numbers" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="tb10EndNo" CssClass="form-control" AutoPostBack="true" placeholder="0"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender runat="server" ID="ajaxFt13" TargetControlID="tb10EndNo" FilterType="Numbers" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="tb10Total" CssClass="form-control" AutoPostBack="true" placeholder="0" ReadOnly="true"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender runat="server" ID="ajaxFt14" TargetControlID="tb10Total" FilterType="Numbers" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td><b>5</b></td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="tb5StartNo" CssClass="form-control" AutoPostBack="true" placeholder="0"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender runat="server" ID="ajaxFt15" TargetControlID="tb5StartNo" FilterType="Numbers" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="tb5EndNo" CssClass="form-control" AutoPostBack="true" placeholder="0"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender runat="server" ID="ajaxFt16" TargetControlID="tb5EndNo" FilterType="Numbers" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="tb5Total" CssClass="form-control" AutoPostBack="true" placeholder="0" ReadOnly="true"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender runat="server" ID="ajaxFt17" TargetControlID="tb5Total" FilterType="Numbers" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td><b>2</b></td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="tb2StartNo" CssClass="form-control" AutoPostBack="true" placeholder="0"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender runat="server" ID="ajaxFt18" TargetControlID="tb2StartNo" FilterType="Numbers" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="tb2EndNo" CssClass="form-control" AutoPostBack="true" placeholder="0"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender runat="server" ID="ajaxFt19" TargetControlID="tb2EndNo" FilterType="Numbers" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="tb2Total" CssClass="form-control" AutoPostBack="true" placeholder="0" ReadOnly="true"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender runat="server" ID="ajaxFt20" TargetControlID="tb2Total" FilterType="Numbers" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td><b>1</b></td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="tb1StartNo" CssClass="form-control" AutoPostBack="true" placeholder="0"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender runat="server" ID="ajaxFt21" TargetControlID="tb1StartNo" FilterType="Numbers" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="tb1EndNo" CssClass="form-control" AutoPostBack="true" placeholder="0"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender runat="server" ID="ajaxFt22" TargetControlID="tb1EndNo" FilterType="Numbers" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="tb1Total" CssClass="form-control" AutoPostBack="true" placeholder="0" ReadOnly="true"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender runat="server" ID="ajaxFt23" TargetControlID="tb1Total" FilterType="Numbers" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <div class="row text-center">
                                <div class="col-lg-12">
                                    <asp:LinkButton runat="server" ID="lbtnGenerateWaybill" OnClick="lbtnGenerateWaybill_Click" CssClass="btn btn-warning" ToolTip="Generate Waybill"><i class="fa fa-edit"></i> Generate WayBill</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel runat="server" ID="pnlAddDutySlips" Visible="true">
                    <div class="card shadow" style="font-size: 12px; min-height: 500px;">
                        <div class="card-header p-2">
                            <div class="row m-0 mt-2">
                                <div class="col-lg-2">
                                </div>
                                <div class="col-lg-10">
                                    <div class="row">
                                        <div class="col-md-1 pl-0">
                                            Duty Date
                                            <asp:TextBox CssClass="form-control form-control-sm" runat="server" ID="tbDutyDate" MaxLength="10" placeholder="DD/MM/YYYY"
                                                Text="" Style="font-size: 9pt; padding: 2px 5px; height: 30px; width: 100%; margin-right: -10px;" autocomplete="off"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FeDutyDate" runat="server" FilterType="Numbers,Custom"
                                                TargetControlID="tbDutyDate" ValidChars="/" />
                                        </div>
                                        <div class="col-md-2">
                                            Bus Type
                                                                   <asp:DropDownList ID="ddlBusType" runat="server" CssClass="form-control form-control-sm" ToolTip="Bus Type">
                                                                   </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">
                                            Service Type
                                                                    <asp:DropDownList ID="ddlServiceType" runat="server" CssClass="form-control form-control-sm" ToolTip="Service Type">
                                                                    </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">
                                            Route
                                                                    <asp:DropDownList ID="ddlRoutes" runat="server" CssClass="form-control form-control-sm" ToolTip="Route">
                                                                    </asp:DropDownList>
                                        </div>
                                        <div class="col-md-3 pl-0 pt-3 pr-0">
                                            <asp:LinkButton ID="lbtnsearch" runat="server" ToolTip="Search Duty Slip" OnClick="lbtnsearch_Click" class="btn btn-sm btn-primary"> <i class="fa fa-search"></i></asp:LinkButton>
                                            <asp:LinkButton ID="lbtnResetFilter" runat="server" ToolTip="Reset Filters" OnClick="lbtnResetFilter_Click" class="btn btn-sm btn-warning"> <i class="fa fa-undo"></i></asp:LinkButton>
                                            <asp:LinkButton ID="lbtnview" ToolTip="View Instructions" OnClick="lbtnview_Click" runat="server" Width="30px" CssClass="btn btn bg-gradient-orange btn-sm  text-white">
                                    <i class="fa fa-info"></i>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="card-body">
                            <div class="row mt-2">
                                <div class="col-lg-6 border-right">
                                    <h2 class="m-0" runat="server" id="hdnGrdHeading">List of services for waybill generations
                                          <asp:LinkButton ID="lbtnDownload1" Visible="false" runat="server" OnClick="lbtnDownload1_Click" class="btn btn bg-gradient-green btn-sm text-white" Style="border-radius: 4px; height: 30px; font-size: 10pt; padding-top: 4px;" ToolTip="Download Excel"> <i class="fa fa-download"></i></asp:LinkButton>

                                    </h2>
                                    <asp:Panel runat="server" ID="pnlAllotDuty" Visible="true" CssClass="mt-1">
                                        <asp:GridView ID="gvAllotedDuties" ShowHeader="false" OnRowCommand="gvAllotedDuties_RowCommand" OnRowDataBound="gvAllotedDuties_RowDataBound" OnPageIndexChanging="gvAllotedDuties_PageIndexChanging" runat="server" AutoGenerateColumns="false" GridLines="None"
                                            AllowPaging="true" PageSize="10" CssClass="table table-hover" DataKeyNames="DUTYREFNO,DSVC_ID,SERVICEID,DUTYDATE,ROUTE,BUSNO,
                                                    DUTYDAYS,DUTYRESTDAYS,SERVICENAME,DRIVER1EMPCODE,DRIVER2EMPCODE,CONDUCTOR1EMPCODE,CONDUCTOR2EMPCODE,
                                                    EMPCONDUCTOR1,EMPCONDUCTOR2,EMPDRIVER1,EMPDRIVER2,Bustype,Service_Type_Name_En,DUTYSLIPDATETIME,DUTYSLIPGENERATEDBY,
                                                    CHANGESTATION,SCHEDULEKM,TARGETDIESELAVERAGE,TARGETINCOME,ODOMETERREADING">
                                         

                                            <Columns>
                                                <asp:TemplateField HeaderText="Service">
                                                    <ItemTemplate>
                                                        <div class="row">
                                                            <div class="col">
                                                                <div class="row">
                                                                    <div class="col-sm-12">


                                                                        <asp:Label runat="server" ID="Label4" Font-Bold="true" CssClass=""><%# Eval("DSVC_ID") %></asp:Label>
                                                                        |
                                                                        <asp:Label runat="server" ID="Label2" CssClass="text-uppercase" Font-Size="12px"><%# Eval("SERVICENAME") %></asp:Label>
                                                                    </div>
                                                                    <div class="col-sm-5">
                                                                        Duty Date/Time:
																	<asp:Label runat="server" ID="Label3" Font-Bold="true" CssClass="mr-4"><%# Eval("DUTYDATE") %></asp:Label>
                                                                    </div>
                                                                    <div class="col-sm-4">
                                                                        Duty Ref No:
															<span style="font-weight: bold; padding-left: 2px;"><%# Eval("DUTYREFNO") %></span>

                                                                    </div>
                                                                    <div class="col-sm-3 input-group-prepend">
                                                                        <asp:Label runat="server" CssClass="mr-1" Text="ETM:"></asp:Label>
                                                                        <asp:DropDownList Visible="true" ID="ddlETM" Style="height: 22px; font-size: 10pt; padding-bottom: 2px; padding-top: 0px; padding-left: 0px"
                                                                            runat="server" CssClass="float-right form-control ">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <div class="col-sm-5">
                                                                        Bus:
																<asp:Label runat="server" ID="lblBusNo" CssClass="form-control-label  text-sm" Font-Bold="true"><%# Eval("BUSNO") %></asp:Label>

                                                                    </div>
                                                                    <div class="col-sm-5">
                                                                        Driver:
																<asp:Label runat="server" ID="lblDriver1" CssClass="" Font-Bold="true"><%# Eval("EMPDRIVER1") %></asp:Label>
                                                                        <br />


                                                                        <asp:Label runat="server" Visible="false" ID="lblDriver2" CssClass="" Font-Bold="true"><%# Eval("EMPDRIVER2") %></asp:Label>
                                                                        Conductor:
																<asp:Label runat="server" ID="lblConductor1" CssClass="" Font-Bold="true"><%# Eval("EMPCONDUCTOR1") %></asp:Label><br />
                                                                        <asp:Label runat="server" Visible="false" Font-Bold="true" ID="lblConductor2" CssClass=""><%# Eval("EMPCONDUCTOR2") %></asp:Label>


                                                                    </div>
                                                                    <div class="col-sm-2 mt-1">
                                                                        <asp:LinkButton ID="lbtnCancelDuty" CommandName="cancelDutySlip" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                                            Visible="true" runat="server" CssClass="btn btn-sm btn-danger float-right" ToolTip="Cancel Waybill"> <i class="fa fa-times"></i></asp:LinkButton>

                                                                        <asp:LinkButton ID="lblGenerateWayBill" CommandName="generateWaybill" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                                            Visible="true" runat="server" CssClass="btn btn-sm btn-success float-right mr-1" ToolTip="Generate Waybill"> <i class="fa fa-check"></i></asp:LinkButton>

                                                                    </div>
                                                                </div>

                                                            </div>
                                                         
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>


                                            <PagerStyle HorizontalAlign="Right" CssClass="GridPager" ForeColor="Black" />
                                        </asp:GridView>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlNorecord1" runat="server" Width="100%" Visible="true">
                                        <div class="col-md-12 p-0" style="text-align: center;">
                                            <div class="col-md-12 busListBox" style="color: #e3e3e3; padding-top: 50px; padding-bottom: 50px; font-size: 33px; font-weight: bold;">
                                                No Record Available<br />
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                                <div class="col-lg-6 pl-3 border-right">
                                    <h2 class="" runat="server" id="h1">List of generated waybills
                                          <asp:LinkButton ID="lbtnDownload2" Visible="false" runat="server" OnClick="lbtnDownload2_Click" class="btn btn bg-gradient-green btn-sm text-white" Style="border-radius: 4px; height: 30px; font-size: 10pt; padding-top: 4px;" ToolTip="Download Excel"> <i class="fa fa-download"></i></asp:LinkButton>

                                    </h2>
                                    <asp:Panel runat="server" ID="pnlDutySlips" Visible="true">
                                        <asp:GridView ID="gvWaybillList" ShowHeader="false" runat="server" OnRowDataBound="gvWaybillList_RowDataBound" OnRowCommand="gvWaybillList_RowCommand" OnPageIndexChanging="gvWaybillList_PageIndexChanging" AutoGenerateColumns="false" GridLines="None"
                                            AllowPaging="true" PageSize="10" CssClass="table table-hover" DataKeyNames="DUTYREFNO">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Service">
                                                    <ItemTemplate>
                                                        <div class="row">
                                                            <div class="col">
                                                                <div class="row">
                                                                    <div class="col-sm-12">


                                                                        <asp:Label runat="server" ID="Label4" Font-Bold="true" CssClass=""><%# Eval("serviceid") %></asp:Label>
                                                                        |
                                                                        <asp:Label runat="server" ID="Label2" CssClass="text-uppercase" Font-Size="12px"><%# Eval("SERVICENAME") %></asp:Label>
                                                                    </div>
                                                                    <div class="col-sm-5">
                                                                        Duty Date/Time:
																	<asp:Label runat="server" ID="Label3" Font-Bold="true" CssClass="mr-4"><%# Eval("DUTYDATE") %></asp:Label>
                                                                    </div>
                                                                    <div class="col-sm-7">
                                                                        Duty Ref No:
															<span style="font-weight: bold; padding-left: 2px;"><%# Eval("DUTYREFNO") %></span>

                                                                    </div>
                                                                    <div class="col-sm-5">
                                                                        Bus:
																<asp:Label runat="server" ID="lblBusNo" CssClass="form-control-label  text-sm" Font-Bold="true"><%# Eval("BUSNO") %></asp:Label>

                                                                    </div>
                                                                    <div class="col-sm-5">
                                                                        Driver:
																<asp:Label runat="server" ID="lblDriver1" CssClass="" Font-Bold="true"><%# Eval("EMPDRIVER1") %></asp:Label>
                                                                        <br />


                                                                        <asp:Label runat="server" Visible="false" ID="lblDriver2" CssClass="" Font-Bold="true"><%# Eval("EMPDRIVER2") %></asp:Label>
                                                                        Conductor:
																<asp:Label runat="server" ID="lblConductor1" CssClass="" Font-Bold="true"><%# Eval("EMPCONDUCTOR1") %></asp:Label><br />
                                                                        <asp:Label runat="server" Visible="false" Font-Bold="true" ID="lblConductor2" CssClass=""><%# Eval("EMPCONDUCTOR2") %></asp:Label>


                                                                    </div>
                                                                    <div class="col-sm-2 mt-1">
                                                                        <asp:LinkButton ID="lbtnviewWayBill" CommandName="viewWaybill" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                                            Visible="true" runat="server" CssClass="btn btn-sm btn-warning"
                                                                            ToolTip="View Waybill"> <i class="fa fa-eye"></i></asp:LinkButton>
                                                                        <asp:LinkButton ID="lbtncancelWaybill" CommandName="cancelWaybill" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                                            Visible="true" runat="server" CssClass="btn btn-sm btn-danger"
                                                                            ToolTip="Cancel Waybill"> <i class="fa fa-times"></i></asp:LinkButton>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                            
                                            <PagerStyle HorizontalAlign="Right" CssClass="GridPager" ForeColor="Black" />
                                        </asp:GridView>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlNoRecord2" runat="server" Width="100%" Visible="true">
                                        <div class="col-md-12 p-0" style="text-align: center;">
                                            <div class="col-md-12 busListBox" style="color: #e3e3e3; padding-top: 50px; padding-bottom: 50px; font-size: 33px; font-weight: bold;">
                                                No Record Available<br />
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <div class="row">
                    <cc1:ModalPopupExtender ID="mpConfirmation" runat="server" PopupControlID="pnlConfirmation"
                        CancelControlID="lbtnNoConfirmation" TargetControlID="Button4" BackgroundCssClass="modalBackground">
                    </cc1:ModalPopupExtender>
                    <asp:Panel ID="pnlConfirmation" runat="server" Style="position: fixed;">
                        <div class="card" style="width: 350px;">
                            <div class="card-header">
                                <h4 class="card-title text-left mb-0">Please Confirm
                                </h4>
                            </div>
                            <div class="card-body text-left pt-2" style="min-height: 100px;">
                                <asp:Label ID="lblConfirmation" runat="server" Text="Do you want to save ?"></asp:Label>
                                <div style="width: 100%; margin-top: 20px; text-align: right;">
                                    <asp:LinkButton ID="lbtnYesConfirmation" OnClick="lbtnYesConfirmation_Click" runat="server" CssClass="btn btn-success btn-sm"> <i class="fa fa-check"></i> Yes </asp:LinkButton>
                                    <asp:LinkButton ID="lbtnNoConfirmation" runat="server" CssClass="btn btn-danger btn-sm ml-2"> <i class="fa fa-times"></i> No </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div style="visibility: hidden;">
                            <asp:Button ID="Button4" runat="server" Text="" />
                        </div>
                    </asp:Panel>
                </div>
                <div class="row">
                    <cc1:ModalPopupExtender ID="mpShowDuty" runat="server" PopupControlID="Panel1" TargetControlID="Button1"
                        CancelControlID="LinkButton2" BackgroundCssClass="ModalPopupBG">
                    </cc1:ModalPopupExtender>
                    <asp:Panel ID="Panel1" runat="server" Style="position: fixed;">
                        <div class="modal-content mt-5" style="width: 65vw;">
                            <div class="card w-100">
                                <div class="card-header py-3">
                                    <div class="row">
                                        <div class="col">
                                            <h3 class="m-0">Duty Waybill</h3>
                                        </div>
                                        <div class="col-auto">
                                            <asp:LinkButton ID="LinkButton2" runat="server" Style="color: Red; cursor: pointer;"><i class="fa fa-times fa-2x"></i></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <div class="card-body p-2">

                                    <asp:Literal ID="eSlip" runat="server"></asp:Literal>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div style="visibility: hidden;">
                            <asp:Button ID="Button1" runat="server" Text="" />
                            <asp:Button ID="Button2" runat="server" Text="" />
                        </div>
                    </asp:Panel>
                </div>
                <div class="row">
                    <cc1:ModalPopupExtender ID="mpDuty" runat="server" PopupControlID="pnlDuty" TargetControlID="btnOpen"
                        CancelControlID="lbtnClose" BackgroundCssClass="ModalPopupBG">
                    </cc1:ModalPopupExtender>
                    <asp:Panel ID="pnlDuty" runat="server" Style="position: fixed;">
                        <div class="modal-content mt-5" style="width: 90vw;">
                            <div class="card w-100">
                                <div class="card-header py-3">
                                    <div class="row">
                                        <div class="col">
                                            <h3 class="m-0">List Of Generated Waybill</h3>
                                        </div>
                                        <div class="col-auto">
                                            <asp:LinkButton ID="lbtnClose" runat="server" Style="color: Red; cursor: pointer;"><i class="fa fa-times fa-2x"></i></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <div class="card-body p-2">

                                    <asp:Literal ID="eDash" runat="server"></asp:Literal>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div style="visibility: hidden;">
                            <asp:Button ID="btnOpen" runat="server" Text="" />
                            <asp:Button ID="lbtnClosemp" runat="server" Text="" />
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

