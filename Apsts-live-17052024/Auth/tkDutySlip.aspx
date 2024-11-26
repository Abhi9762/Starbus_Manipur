<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/TimeKeeperMaster.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="tkDutySlip.aspx.cs" Inherits="Auth_tkDutySlip" %>

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
        function DisableBackButton() {
            window.history.forward()
        }
        DisableBackButton();
        window.onload = DisableBackButton;
        window.onpageshow = function (evt) { if (evt.persisted) DisableBackButton() }
        window.onunload = function () { void (0) }

        $(document).ready(function () {

            var currDate = new Date().getDate();
            var nextDate = new Date(new Date().setDate(currDate + 1));
            var prevDate = new Date(new Date().setDate(currDate - 1));
            var todayDate = new Date(new Date().setDate(currDate ));

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
        <asp:HiddenField runat="server" ID="hdnRowIndex" />
        <div class="row">
            <div class="col-lg-12">
                <asp:Panel runat="server" ID="pnlSummary" Visible="true">
                    <div class="card card-stats mb-2 m-0">
                        <div class="row m-0 py-1 mb-2 text-center">
                            <div class="col-lg-2 mt-2 border-right">
                                <div class="row m-0 text-center">
                                    <div class="col-md-12 pl-0">
                                        <h4 class="mb-0" runat="server" visible="true">Duty Slip</h4>
                                        <div class="row">
                                            <div class="col-lg-6 border-right">
                                                <asp:Label ID="lblTotalSlip" runat="server" Text="0" CssClass="form-control-label"></asp:Label>
                                                <br />
                                                <asp:Label runat="server" CssClass="form-control-label text-sm"> Total</asp:Label>
                                            </div>
                                            <div class="col-lg-6">
                                                <asp:Label ID="lblPendingDutyaAllocation" runat="server" Text="0" CssClass="form-control-label"></asp:Label>
                                                <br />
                                                <asp:Label runat="server" CssClass="form-control-label text-sm"> Pending Duty Allocation</asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-9 pt-2">
                                <div class="row m-0 text-center">
                                    <div class="col-md-4 pl-0 border-right">

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
                                    <div class="col-md-4 pl-0 border-right">
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
                                    <div class="col-md-4 pl-0 border-right">

                                        <div class="row">
                                            <div class="col-lg-12">
                                                <h4 class="mb-0" runat="server" visible="true">Conductors
                                           
                                                </h4>
                                            </div>

                                        </div>

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
                                </div>
                            </div>
                            <div class="col-lg-1 pt-2 ">
                                <asp:LinkButton runat="server" ID="lbtnarchive" OnClick="lbtnarchive_Click" CssClass="btn btn-sm bg-gradient-orange text-white">Archive</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <div class="card card-stats mb-3 m-0">
                    <div class="row m-0">
                        <div class="col-lg-12 mt-1">
                            <asp:Panel runat="server" ID="pnlUpdateDutyList" Visible="false" Style="box-shadow: 2px 4px 25px 20px #596166c2">

                                <div class="card" style="font-size: 12px; min-height: 500px; border: none;">
                                    <div class="card-body p-0">
                                        <div class="col-lg-12" style="font-size: 13px;">
                                            <div class="col-lg-12 mb-3">
                                                <div class="card-header">
                                                    <h3 runat="server" class="mb-0 text-center" id="lblTxtHead">Provisional Duty Allocation Details</h3>
                                                </div>
                                                <asp:LinkButton ID="lbtnCancel" OnClick="lbtnCancel_Click" runat="server" CssClass="btn btn-danger btn-sm ml-2" Style="float: right; border-radius: 25px; top: -62px; margin-right: -25px"> <i class="fa fa-times"></i></asp:LinkButton>
                                            </div>
                                            <div class="col-lg-12 mb-3 p-3">
                                                <div class="row m-0">
                                                    <div class="col-lg-8">
                                                        <asp:Label runat="server" Text="Service Name" Font-Bold="true" CssClass="form-control-label"></asp:Label>
                                                        <br />
                                                        <asp:Label runat="server" ID="lblServiceName" CssClass="form-control-label text-uppercase" Text="N/A"></asp:Label>
                                                    </div>
                                                    <div class="col-lg-2">
                                                        <asp:Label runat="server" Text="Duty Reference No" Font-Bold="true" CssClass="form-control-label"></asp:Label>
                                                        <br />
                                                        <asp:Label runat="server" ID="lblRefNo" CssClass="form-control-label" Text="N/A"></asp:Label>
                                                    </div>
                                                    <div class="col-lg-2">
                                                        <asp:Label runat="server" Text="Departure Date/Time" Font-Bold="true" CssClass="form-control-label"></asp:Label>
                                                        <br />
                                                        <asp:Label runat="server" ID="lblDeptTime" CssClass="form-control-label" Text="N/A"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row mt-2 m-0" runat="server">
                                                    <div class="col-lg-12 mb-2">
                                                        <h4>
                                                            <asp:Label runat="server">Crew Members</asp:Label></h4>
                                                    </div>
                                                    <div class="col-lg-4">
                                                        <asp:Label runat="server" Text="Driver" Font-Bold="true" CssClass="form-control-label"></asp:Label>
                                                        <br />
                                                        1-
                                                                                    <asp:Label runat="server" ID="lblDSDriver" CssClass="form-control-label" Text="N/A"></asp:Label><br />
                                                        2-
                                                                                    <asp:Label runat="server" ID="lblDSAltDriver" CssClass="form-control-label" Text="N/A"></asp:Label>
                                                    </div>
                                                    <div class="col-lg-4">
                                                        <asp:Label runat="server" Text="Conductor" Font-Bold="true" CssClass="form-control-label"></asp:Label>
                                                        <br />
                                                        1-
                                                                                    <asp:Label runat="server" CssClass="form-control-label" ID="lblDSConductor" Text="N/A"></asp:Label><br />
                                                        2-
                                                                                    <asp:Label runat="server" CssClass="form-control-label" ID="lblDSAltConductor" Text="N/A"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row mt-2 m-0" runat="server" id="tblGenerateDuty" visible="false">
                                                    <div class="col-lg-12 mb-2">
                                                        <h4>
                                                            <asp:Label runat="server">Other Details</asp:Label></h4>
                                                    </div>
                                                    <div class="col-lg-3">
                                                        <asp:Label runat="server" Text="Change Station" Font-Bold="true" CssClass="form-control-label"></asp:Label>
                                                        <br />
                                                        <asp:Label runat="server" ID="lblChangeStation" CssClass="form-control-label" Text="N/A"></asp:Label>
                                                    </div>
                                                    <div class="col-lg-2">
                                                        <asp:Label runat="server" Text="Odometer Reading" Font-Bold="true" CssClass="form-control-label"></asp:Label>
                                                        <br />
                                                        <asp:Label runat="server" ID="lblOdometerReading" CssClass="form-control-label" Text="N/A"></asp:Label>
                                                    </div>
                                                    <div class="col-lg-2">
                                                        <asp:Label runat="server" Text="Schedule Km." Font-Bold="true" CssClass="form-control-label"></asp:Label>
                                                        <br />
                                                        <asp:Label runat="server" ID="lblScheduleKM" CssClass="form-control-label" Text="N/A"></asp:Label>
                                                    </div>
                                                    <div class="col-lg-2">
                                                        <asp:Label runat="server" Text="" Font-Bold="true" CssClass="form-control-label">Target Income (₹)</asp:Label><br />
                                                        <asp:Label runat="server" ID="lblTargetIncome" CssClass="form-control-label" Text="N/A"></asp:Label>
                                                    </div>
                                                    <div class="col-lg-3">
                                                        <asp:Label runat="server" Text="Target Diesel Average" Font-Bold="true" CssClass="form-control-label"></asp:Label><br />
                                                        <asp:Label runat="server" ID="lblTargetDieselAvg" CssClass="form-control-label" Text="N/A"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row mt-3 m-0">
                                                    <div class="col-lg-12">
                                                        <asp:GridView ID="gvServiceTrips" runat="server" AutoGenerateColumns="false" GridLines="None" CssClass="table table-bordered" DataKeyNames="">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="S.No.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Trip Direction">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbltripd" runat="server" Text='<%#Eval("trip_direction") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Start Time">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStartTime" runat="server" Text='<%#Eval("start_time") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Station From">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblFromStation" runat="server" Text='<%#Eval("fromstationname") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Station To">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblToStation" runat="server" Text='<%#Eval("tostationname") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Authorised Dhaba Amount (₹)" ItemStyle-CssClass="text-center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDhabaAmt" runat="server" Text='<%#Eval("dhaba_amt") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                                <div class="row mt-3">
                                                    <div class="col-lg-12 text-center">
                                                        <asp:LinkButton runat="server" ID="lbtnGenerateDutySlip" OnClick="lbtnGenerateDutySlip_Click" Visible="false" CssClass="btn btn-success" ToolTip="Generate Duty Slip"><i class="fa fa-edit"></i> Generate Duty Slip</asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel runat="server" ID="pnlAddDutySlips" Visible="true">
                                <div class="card shadow" style="font-size: 12px; min-height: 500px;">
                                    <div class="card-header p-2">
                                        <div class="row m-0">
                                         
                                            <div class="col-lg-12">
                                                <div class="row">
                                                    <div class="col-md-2">
                                                    </div>
                                                    <div class="col-md-1">
                                                        Duty Date
                                                            <asp:TextBox CssClass="form-control form-control-sm" runat="server" ID="tbDutyDate" MaxLength="10" placeholder="DD/MM/YYYY"
                                                                Text="" Style="font-size: 9pt; padding: 2px 5px; height: 30px; width: 100%; margin-right: -10px;" autocomplete="off"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="FeDutyDate" runat="server" FilterType="Numbers,Custom"
                                                            TargetControlID="tbDutyDate" ValidChars="/" />
                                                        <asp:TextBox CssClass="form-control form-control-sm" runat="server" ID="tbFromDate" MaxLength="10" placeholder="DD/MM/YYYY" Visible="false"
                                                            Text="" Style="font-size: 9pt; padding: 2px 5px; height: 30px; width: 100%; margin-right: -10px;" autocomplete="off"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers,Custom"
                                                            TargetControlID="tbDutyDate" ValidChars="/" />
                                                    </div>


                                                    <div class="col-md-2">
                                                        Bus Type
                                                            <asp:DropDownList ID="ddlBusType" runat="server" Style="height: 30px; font-size: 9pt;" ToolTip="Bus Type"
                                                                CssClass="form-control form-control-sm">
                                                                <asp:ListItem>Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-2">
                                                        Service Type
                                                                    <asp:DropDownList ID="ddlServiceType" runat="server" ToolTip="Service Type" class="form-control form-control-sm">
                                                                    </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-2">
                                                        Route
                                                                    <asp:DropDownList ID="ddlRoutes" runat="server" class="form-control form-control-sm" ToolTip="Route">
                                                                    </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-2 pl-0 pt-3 pr-0">
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
                                                <h3 class="m-0" runat="server" id="hdnGrdHeading">List of provisional duty allotment pending for duty slip
                                                     <asp:LinkButton ID="lbtnDownload1" runat="server" OnClick="lbtnDownloadExcel_Click" class="btn btn bg-gradient-green btn-sm text-white" Style="border-radius: 4px; height: 30px; font-size: 10pt; padding-top: 4px;" ToolTip="Download Excel"> <i class="fa fa-download"></i></asp:LinkButton>
                                                </h3>
                                                <asp:Panel runat="server" ID="pnlAllotDuty" Visible="true">
                                                    <asp:GridView ID="gvAllotedDuties" ShowHeader="false" runat="server" OnRowCommand="gvAllotedDuties_RowCommand" AutoGenerateColumns="false" GridLines="None"
                                                        AllowPaging="false" PageSize="50" CssClass="table table-borderless table-hover" DataKeyNames="dutyrefno, deletedutyallocationyn, serviceid, fromdate, todate, route_name, no_of_driver, no_of_conductor, departuretime, dutytime, dsvc_id, busno, conductor1, conductor2, driver1, driver2, dutydays, dutyrestdays, dutystatus, generatedby, generationdate, service_name_en, empconductor1, empconductor2, empdriver1, empdriver2, bustype, service_type_name_en, drvr1lcenseno, drvr2lcenseno, cond1lcenseno, cond2lcenseno, drvr1licensedate, drvr2licensedate, cond1licensedate, cond2licensedate, fitnessno, permitno, pollutioncheckno, rout_id, fitnessvalidity, permitvalidity, pollutionvalidity, insuranceno, insurancevalidupto,driver,conductor">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Service">
                                                                <ItemTemplate>
                                                                    <div class="row">
                                                                        <div class="col">
                                                                            <div class="row">
                                                                                <div class="col-sm-12">
                                                                                    <asp:Label runat="server" ID="Label1" Font-Bold="true" CssClass=""><%# Eval("dsvc_id") %></asp:Label> |
                                                                                    <asp:Label runat="server" ID="Label2" CssClass="text-uppercase" Font-Size="12px"><%# Eval("service_name_en") %></asp:Label><br />
                                                                                    Duty Date/Time:
																	<asp:Label runat="server" ID="Label3" Font-Bold="true" CssClass="mr-5"><%# Eval("departuretime") %></asp:Label>
                                                                                    Duty Ref No:
															<span style="font-weight: bold; padding-left: 2px;"><%# Eval("DUTYREFNO") %></span>

                                                                                </div>
                                                                                <div class="col-sm-4">
                                                                                    Bus:
																<asp:Label runat="server" ID="lblBusNo" CssClass="form-control-label  text-sm" Font-Bold="true"><%# Eval("busno") %></asp:Label>

                                                                                </div>
                                                                                <div class="col-sm-4">
                                                                                    Driver:
																<asp:Label runat="server" ID="lblDriver1" CssClass="" Font-Bold="true"><%# Eval("empdriver1") %></asp:Label>
                                                                                    <br />
                                                                                    <asp:Label runat="server" Visible="false" ID="lblDriver2" CssClass="" Font-Bold="true"><%# Eval("empdriver2") %></asp:Label>


                                                                                </div>
                                                                                <div class="col-sm-4">
                                                                                    Conductor:
																<asp:Label runat="server" ID="lblConductor1" CssClass="" Font-Bold="true"><%# Eval("empconductor1") %></asp:Label><br />
                                                                                    <asp:Label runat="server" Visible="false" Font-Bold="true" ID="lblConductor2" CssClass=""><%# Eval("empconductor2") %></asp:Label>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-auto text-right">
                                                                            <asp:LinkButton ID="lbtnGenerateDutySlip" CommandName="generateDutySlip" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                                                Visible="true" runat="server" CssClass="btn btn-sm btn-success"
                                                                                ToolTip="Generate Duty Slip"> <i class="fa fa-check"></i></asp:LinkButton>
                                                                            <asp:LinkButton ID="lbtnCancelDutySlip" CommandName="cancelDutySlip" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                                                Visible="true" runat="server" CssClass="btn btn-sm btn-danger"
                                                                                ToolTip="Cancel Duty"> <i class="fa fa-times"></i></asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <PagerStyle HorizontalAlign="Right" CssClass="GridPager" ForeColor="Black" />
                                                    </asp:GridView>
                                                </asp:Panel>
                                                <asp:Panel ID="pnlNoRecord1" runat="server" Width="100%" Visible="true">
                                                    <div class="col-md-12 p-0" style="text-align: center; ">
                                                        <div class="col-md-12 busListBox" style="color: #e3e3e3; padding-top: 50px; padding-bottom: 50px; font-size: 33px; font-weight: bold;">
                                                            No Record Available<br />
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                            <div class="col-lg-6">
                                                <h3 class="ml-2" runat="server" id="h1">List of generated duty slips
                                                     <asp:LinkButton ID="lbtndownload2" runat="server" Visible="false" OnClick="lbtndownload2_Click" class="btn btn bg-gradient-green btn-sm text-white" Style="border-radius: 4px; height: 30px; font-size: 10pt; padding-top: 4px;" ToolTip="Download Excel"> <i class="fa fa-download"></i></asp:LinkButton>
                                                </h3>
                                                <asp:Panel runat="server" ID="pnlDutySlips" Visible="false">
                                                    <asp:GridView ID="gvDutySlips" OnRowCommand="gvDutySlips_RowCommand" runat="server" HeaderStyle-CssClass="thead-light font-weight-bold" AutoGenerateColumns="false" GridLines="None"
                                                        AllowPaging="false" ShowHeader="false" PageSize="50" CssClass="table table-borderless table-hover mt--2" DataKeyNames="DUTYREFNO">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Service">
                                                                <ItemTemplate>
                                                                    <div class="row">
                                                                        <div class="col">
                                                                            <div class="row">
                                                                                <div class="col-sm-12">
                                                                                     <asp:Label runat="server" ID="Label1" Font-Bold="true" CssClass=""><%# Eval("dsvc_id") %></asp:Label> |
                                                                                    <asp:Label runat="server" ID="Label2" CssClass="text-uppercase" Font-Size="12px"><%# Eval("SERVICENAME") %></asp:Label><br />
                                                                                    Duty Date/Time:
																	<asp:Label runat="server" ID="Label3" Font-Bold="true" CssClass="mr-5"><%# Eval("dutydatetime") %></asp:Label>
                                                                                    Duty Ref No:
															<span style="font-weight: bold; padding-left: 2px;"><%# Eval("DUTYREFNO") %></span>

                                                                                </div>
                                                                                <div class="col-sm-4">
                                                                                    Bus:
																<asp:Label runat="server" ID="lblBusNo" CssClass="form-control-label  text-sm" Font-Bold="true"><%# Eval("BUSNO") %></asp:Label>

                                                                                </div>
                                                                                <div class="col-sm-4">
                                                                                    Driver:
																<asp:Label runat="server" ID="lblDriver1" CssClass="" Font-Bold="true"><%# Eval("DRIVER1NAME") %></asp:Label>
                                                                                    <br />

                                                                                    <asp:Label runat="server" Visible="false" ID="lblDriver2" CssClass="" Font-Bold="true"><%# Eval("DRIVER2NAME") %></asp:Label>


                                                                                </div>
                                                                                <div class="col-sm-4">
                                                                                    Conductor:
																<asp:Label runat="server" ID="lblConductor1" CssClass="" Font-Bold="true"><%# Eval("CONDUCTOR1NAME") %></asp:Label><br />
                                                                                    <asp:Label runat="server" Visible="false" Font-Bold="true" ID="lblConductor2" CssClass=""><%# Eval("CONDUCTOR2NAME") %></asp:Label>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-auto text-right">
                                                                            <asp:LinkButton ID="lbtnCancelDuty" CommandName="cancelDuty" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                                                Visible="true" runat="server" CssClass="btn btn-sm btn-danger"
                                                                                ToolTip="Cancel Duty Slip"> <i class="fa fa-times"></i></asp:LinkButton>
                                                                            <asp:LinkButton ID="lbtnViewDuty" CommandName="viewDuty" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                                                Visible="true" runat="server" CssClass="btn btn-sm btn-warning"
                                                                                ToolTip="View Duty Slip"> <i class="fa fa-eye"></i></asp:LinkButton>

                                                                        </div>
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <PagerStyle HorizontalAlign="Right" CssClass="GridPager" ForeColor="Black" />
                                                    </asp:GridView>
                                                </asp:Panel>
                                                <asp:Panel ID="pnlNoRecord2" runat="server" Width="100%" Visible="true">
                                                    <div class="col-md-12 p-0" style="text-align: center; ">
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
                        </div>
                    </div>
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
                                            <h3 class="m-0">List Of Generated Duty Slip</h3>
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
                    <cc1:ModalPopupExtender ID="mpShowStatus" runat="server" PopupControlID="pnlShowStatus" CancelControlID="Button8"
                        TargetControlID="Button7" BackgroundCssClass="modalBackground">
                    </cc1:ModalPopupExtender>
                    <asp:Panel ID="pnlShowStatus" runat="server" Style="position: fixed;">
                        <div class="card" style="width: 350px;">
                            <div class="card-body text-center pt-2" style="min-height: 100px;">
                                <h4>Please Note</h4>
                                <asp:Label ID="lblStatus" runat="server" Text="" Font-Size="13pt"></asp:Label>
                                <div style="width: 100%; margin-top: 20px; text-align: right;">
                                    <asp:LinkButton ID="lbtnFreeBusCrew" OnClick="lbtnFreeBusCrew_Click" runat="server" CssClass="btn btn-success btn-sm ml-2"> <i class="fa fa-check"></i> OK </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div style="visibility: hidden;">
                            <asp:Button ID="Button7" runat="server" Text="" />
                            <asp:Button ID="Button8" runat="server" Text="" />
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
                                            <h3 class="m-0">Duty Slip</h3>
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
            </div>
        </div>
    </div>
</asp:Content>

