<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/ETMBranchMaster.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="ETMBranchSubmission.aspx.cs" Inherits="ETMBranchSubmission" MaintainScrollPositionOnPostback="true" %>

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
            var todayDate = new Date(new Date().setDate(currDate));
            var nextDate = new Date(new Date().setDate(currDate + 1));

            $('[id*=tbDutyDate]').datepicker({
                endDate: todayDate,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            });
            $('[id*=tbToDate]').datepicker({
                startDate: preDate,
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
                <asp:Panel runat="server" ID="pnlWaybillDetails" Visible="true">
                    <div class="card card-stats mb-2 m-0">
                        <div class="row m-0 align-items-center">
                            <div class="col-xl-12">
                                <div class="row m-0">
                                    <div class="col-4 border-right">
                                        <div class="card-body">
                                            <div class="row">
                                                <h4 class="mb-1">
                                                    <asp:Label ID="lblSummary" runat="server" CssClass="text-capitalize">Summary as on Date 01/12/2021 04:00pm</asp:Label></h4>
                                                <div class="col-md-4">
                                                    <asp:Label runat="server" Font-Bold="true" CssClass="form-control-label">Total ETM </asp:Label>

                                                </div>
                                                <div class="col-md-2 text-right border-right">
                                                    <asp:Label ID="lblTotalETM" runat="server" Font-Bold="true" Text="0" CssClass="form-control-label"></asp:Label>

                                                </div>
                                                <div class="col-md-4">
                                                    <asp:Label runat="server" Font-Bold="true" CssClass="form-control-label">Allocated to Duty </asp:Label>

                                                </div>
                                                <div class="col-md-2 text-right ">
                                                    <asp:Label ID="lblOnDutyETM" Font-Bold="true" runat="server" Text="0" CssClass="form-control-label"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="row">

                                                <div class="col-md-4">
                                                    <asp:Label runat="server" Font-Bold="true" CssClass="form-control-label">Total ETM Submitted </asp:Label>

                                                </div>
                                                <div class="col-md-2 text-right border-right">
                                                    <asp:Label ID="lblTotalETMSubmitted" Font-Bold="true" runat="server" Text="0" CssClass="form-control-label"></asp:Label>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:Label runat="server" Font-Bold="true" CssClass="form-control-label">Pending For Submittion </asp:Label>

                                                </div>
                                                <div class="col-md-2 text-right">
                                                    <asp:Label ID="lblETMSubmissionPending" Font-Bold="true" runat="server" Text="0" CssClass="form-control-label"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3 border-right">
                                        <div class="card-body">
                                            <div class="row">
                                                <div class="col">
                                                    <div>
                                                        <h4 class="mb-1">Generate ETM Submission Report</h4>
                                                    </div>
                                                    <div class="input-group mb-3">
                                                        <div class="input-group-prepend pr-2" style="width: 80%">
                                                            <asp:DropDownList ID="ddlETMSubReport" data-toggle="tooltip" data-placement="bottom" title="ETM Submission" CssClass="form-control form-control-sm" runat="server">
                                                                <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <asp:LinkButton ID="lbtnDownloadETMSubRpt" data-toggle="tooltip" data-placement="bottom" title="Download" runat="server" CssClass="btn btn bg-gradient-green btn-sm text-white">
                                            <i class="fa fa-download"></i>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="col-md-5">
                                        <div class="card-body">
                                            <div class="row mr-0">
                                                <div class="col">
                                                    <div>
                                                        <h4 class="mb-1">Instructions</h4>
                                                    </div>
                                                    <ul class="data-list mb-0" data-autoscroll>
                                                        <li>
                                                            <asp:Label runat="server" CssClass="form-control-label">After completion of journey, trip details will be saved here.</asp:Label><br />
                                                        </li>
                                                        <li>
                                                            <asp:Label runat="server" CssClass="form-control-label">If there is any change in subjected and actual KM then it will be sent for verification to Station Incharge</asp:Label><br />
                                                        </li>

                                                    </ul>
                                                </div>
                                                <div class="col-auto">
                                                    <asp:LinkButton ID="lbtnViewinst" ToolTip="View Instructions" OnClick="lbtnview_Click" runat="server" Width="30px" CssClass="btn btn bg-gradient-orange btn-sm  text-white">
                                    <i class="fa fa-eye"></i>
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnDownload" runat="server" class="btn btn bg-gradient-green btn-sm text-white" ToolTip="Download Instruction">
                                            <i class="fa fa-download"></i>
                                                    </asp:LinkButton><br />
                                                    <asp:LinkButton runat="server" ID="lbtnArchive" OnClick="lbtnArchive_Click" CssClass="btn btn-sm btn-primary text-white mt-2">Archive</asp:LinkButton>

                                                </div>
                                            </div>

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
                            <div class="row m-0 mt-3">
                                <div class="col-lg-2">
                                </div>
                                <div class="col-md-10">
                                    <div class="row">
                                        <div class="col-md-1 pl-0">
                                            <asp:Label ID="Label1" runat="server" CssClass="form-control-label">Duty Date</asp:Label>

                                            <asp:TextBox CssClass="form-control form-control-sm" runat="server" ID="tbDutyDate" MaxLength="10" placeholder="DD/MM/YYYY"
                                                Text="" Style="font-size: 9pt; padding: 2px 5px; height: 30px; width: 100%; margin-right: -10px;" autocomplete="off"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers,Custom"
                                                TargetControlID="tbDutyDate" ValidChars="/" />
                                        </div>
                                        <div class="col-md-2">
                                            Bus Type
                                                                   <asp:DropDownList ID="ddlBusType" runat="server" CssClass="form-control form-control-sm" ToolTip="Bus Type">
                                                                   </asp:DropDownList>
                                        </div>

                                        <div class="col-md-2 pr-0">
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
                                            <asp:LinkButton ID="lbtnsearch" runat="server" ToolTip="Search ETM Submission" OnClick="lbtnsearch_Click" class="btn btn-sm btn-primary"> <i class="fa fa-search"></i></asp:LinkButton>
                                            <asp:LinkButton ID="lbtnResetFilter" runat="server" ToolTip="Reset Filters" OnClick="lbtnResetFilter_Click" class="btn btn-sm btn-warning"> <i class="fa fa-undo"></i></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="card-body p-2">
                            <div class="row mt-2">
                                <div class="col-lg-6 border-right">
                                    <h2 class="m-0" runat="server" id="hdnGrdHeading">List of Pending ETM
                                          <asp:LinkButton ID="lbtnDownload1" Visible="false" runat="server" OnClick="lbtnDownload1_Click" class="btn btn bg-gradient-green btn-sm text-white" Style="border-radius: 4px; height: 30px; font-size: 10pt; padding-top: 4px;" ToolTip="Download Excel"> <i class="fa fa-download"></i></asp:LinkButton>

                                    </h2>
                                    <asp:GridView ID="gvWaybills" runat="server" AutoGenerateColumns="false" GridLines="None"
                                        AllowPaging="true" PageSize="20" ShowHeader="false" OnRowCommand="gvWaybills_RowCommand" OnRowDataBound="gvWaybills_RowDataBound" CssClass="table table-hover table-striped mt-1"
                                        DataKeyNames="dutyrefno,etmrefno">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Service">
                                                <ItemTemplate>


                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            <asp:Label runat="server" ID="Label4" Font-Bold="true" CssClass=""><%# Eval("serviceid") %></asp:Label>
                                                            |
                                                             <asp:Label runat="server" ID="Label2" CssClass="text-uppercase" Font-Size="12px"><%# Eval("servicename") %></asp:Label>
                                                        </div>
                                                        <div class="col-sm-10">
                                                            <div class="row">
                                                                <div class="col-sm-6">
                                                                    Duty Date/Time:
																	<asp:Label runat="server" ID="Label3" Font-Bold="true" CssClass="mr-4"><%# Eval("dutydate") %></asp:Label>

                                                                </div>
                                                                <div class="col-sm-6">
                                                                    Duty Ref No:
															<span style="font-weight: bold; padding-left: 2px;"><%# Eval("dutyrefno") %></span>

                                                                </div>
                                                                <div class="col-sm-6">
                                                                    Bus:
																<asp:Label runat="server" ID="lblBusNo" CssClass="form-control-label  text-sm" Font-Bold="true"><%# Eval("BUSNO") %></asp:Label>

                                                                </div>
                                                                <div class="col-sm-6">
                                                                    Driver:
																<asp:Label runat="server" ID="lblDriver1" CssClass="" Font-Bold="true"><%# Eval("EMPDRIVER1") %></asp:Label>
                                                                    <br />
                                                                    <asp:Label runat="server" Visible="false" ID="lblDriver2" CssClass="" Font-Bold="true"><%# Eval("EMPDRIVER2") %></asp:Label>
                                                                    Conductor:
																<asp:Label runat="server" ID="lblConductor1" CssClass="" Font-Bold="true"><%# Eval("EMPCONDUCTOR1") %></asp:Label><br />
                                                                    <asp:Label runat="server" Visible="false" Font-Bold="true" ID="lblConductor2" CssClass=""><%# Eval("EMPCONDUCTOR2") %></asp:Label>

                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-2 mt-1">
                                                            <asp:LinkButton ID="LinkButton2" CommandName="ValidateWaybill" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                                Visible="true" runat="server" CssClass="btn btn-sm btn-primary mb-1"
                                                                ToolTip="View Waybill">Validate</asp:LinkButton><br />
                                                            <asp:LinkButton ID="lbtnviewWayBill" CommandName="viewWaybill" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                                Visible="true" runat="server" CssClass="btn btn-sm btn-warning"
                                                                ToolTip="View Waybill"> W</asp:LinkButton>
                                                            <asp:LinkButton ID="lbtnviewDutyslip" CommandName="viewdutyslip" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                                Visible="true" runat="server" CssClass="btn btn-sm btn-danger"
                                                                ToolTip="View Dutyslip">D</asp:LinkButton>
                                                        </div>

                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                        <PagerStyle HorizontalAlign="Right" CssClass="GridPager" ForeColor="Black" />
                                    </asp:GridView>
                                    <asp:Panel ID="pnlNoRecord" runat="server" Width="100%" Visible="true">
                                        <div class="col-md-12 p-0" style="text-align: center; text-transform: uppercase;">
                                            <div class="col-md-12 busListBox" style="color: #e3e3e3; padding-top: 50px; padding-bottom: 50px; font-size: 33px; font-weight: bold;">
                                                No Record Available<br />
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                                <div class="col-lg-6">
                                    <h2 class="m-0" runat="server" id="h1">List of Submitted ETM
                                          <asp:LinkButton ID="lbtnDownload2" Visible="false" runat="server" OnClick="lbtnDownload2_Click" class="btn btn bg-gradient-green btn-sm text-white" Style="border-radius: 4px; height: 30px; font-size: 10pt; padding-top: 4px;" ToolTip="Download Excel"> <i class="fa fa-download"></i></asp:LinkButton>

                                    </h2>
                                    <asp:GridView ID="gvSubmittedWaybills" runat="server" AutoGenerateColumns="false" GridLines="None"
                                        AllowPaging="true" PageSize="20" ShowHeader="false" OnRowCommand="gvSubmittedWaybills_RowCommand" OnRowDataBound="gvSubmittedWaybills_RowDataBound" CssClass="table table-hover table-striped mt-1"
                                        DataKeyNames="dutyrefno,etmrefno">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Service">
                                                <ItemTemplate>


                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            <asp:Label runat="server" ID="Label4" Font-Bold="true" CssClass=""><%# Eval("serviceid") %></asp:Label>
                                                            |
                                                             <asp:Label runat="server" ID="Label2" CssClass="text-uppercase" Font-Size="12px"><%# Eval("servicename") %></asp:Label>
                                                        </div>
                                                        <div class="col-sm-10">
                                                            <div class="row">
                                                                <div class="col-lg-6">
                                                                    Duty Date/Time:
																	<asp:Label runat="server" ID="Label3" Font-Bold="true" CssClass="mr-4"><%# Eval("dutydate") %></asp:Label>

                                                                </div>
                                                                <div class="col-lg-6">
                                                                    Duty Ref No:
															<span style="font-weight: bold; padding-left: 2px;"><%# Eval("dutyrefno") %></span>

                                                                </div>
                                                                <div class="col-lg-6">
                                                                    Bus:
																<asp:Label runat="server" ID="lblBusNo" CssClass="form-control-label  text-sm" Font-Bold="true"><%# Eval("BUSNO") %></asp:Label>
                                                                    <br />
                                                                    <div runat="server" id="divCashStatus">
                                                                        Cash Status:
																<asp:Label runat="server" ID="lblCashStatus" CssClass="form-control-label  text-sm" Font-Bold="true"></asp:Label>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-6">
                                                                    Driver:
																<asp:Label runat="server" ID="lblDriver1" CssClass="" Font-Bold="true"><%# Eval("EMPDRIVER1") %></asp:Label>
                                                                    <br />
                                                                    <asp:Label runat="server" Visible="false" ID="lblDriver2" CssClass="" Font-Bold="true"><%# Eval("EMPDRIVER2") %></asp:Label>
                                                                    Conductor:
																<asp:Label runat="server" ID="lblConductor1" CssClass="" Font-Bold="true"><%# Eval("EMPCONDUCTOR1") %></asp:Label><br />
                                                                    <asp:Label runat="server" Visible="false" Font-Bold="true" ID="lblConductor2" CssClass=""><%# Eval("EMPCONDUCTOR2") %></asp:Label>

                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-2 mt-1">

                                                            <asp:LinkButton ID="lbtnviewWayBill" CommandName="viewWaybill" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                                Visible="true" runat="server" CssClass="btn btn-sm btn-warning"
                                                                ToolTip="View Waybill"> W</asp:LinkButton>
                                                            <asp:LinkButton ID="lbtnviewDutyslip" CommandName="viewdutyslip" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                                Visible="true" runat="server" CssClass="btn btn-sm btn-danger"
                                                                ToolTip="View Dutyslip">D</asp:LinkButton>
                                                            <asp:LinkButton ID="LinkButton2" CommandName="ViewExpenditure" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                                Visible="true" runat="server" CssClass="btn btn-sm btn-primary"
                                                                ToolTip="View Collection & Expendiure Details">E</asp:LinkButton>
                                                            <asp:LinkButton ID="LinkButton5" CommandName="viewcashslip" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                                Visible="true" runat="server" CssClass="btn btn-sm btn-success"
                                                                ToolTip="View Cash Deposit Slip">C</asp:LinkButton>
                                                        </div>

                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                        <PagerStyle HorizontalAlign="Right" CssClass="GridPager" ForeColor="Black" />
                                    </asp:GridView>
                                    <asp:Panel ID="pnlNoRecord2" runat="server" Width="100%" Visible="true">
                                        <div class="col-md-12 p-0" style="text-align: center; text-transform: uppercase;">
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
                <asp:Panel runat="server" ID="pnlEtmSubmitDetails" Visible="false" CssClass="mt-4" Style="box-shadow: 2px 4px 25px 20px #596166c2; background: #fff; padding: 10px; width: 100%; margin-left: auto; margin-right: auto;">
                    <div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="card-header p-0">
                                    <h3 runat="server" class="mb-0 text-center" id="lblTxtHead">ETM Submission Details</h3>
                                </div>
                                <asp:LinkButton ID="lbtnCancel" OnClick="lbtnCancel_Click" runat="server" CssClass="btn btn-danger btn-sm ml-2" Style="float: right; border-radius: 25px; top: -60px; margin-right: -25px"> <i class="fa fa-times"></i></asp:LinkButton>
                            </div>
                        </div>
                        <div class="row mt-2 ml-2 m-0">
                            <div class="col-lg-12">
                                <div class="row">
                                    <div class="col-lg-2">
                                        <i class="fa fa-road mr-1"></i>
                                        <asp:Label runat="server" Text="Waybill No" Font-Bold="true" CssClass="form-control-label"></asp:Label><br />

                                        <asp:Label runat="server" ID="tbWaybillNo" CssClass="form-control-label"></asp:Label>
                                    </div>
                                    <div class="col-lg-2">
                                        <i class="fa fa-bus mr-1"></i>
                                        <asp:Label runat="server" Text="Bus No" Font-Bold="true" CssClass="form-control-label"></asp:Label><br />
                                        <asp:Label runat="server" ID="lblBusNo" CssClass="form-control-label text-uppercase"></asp:Label>
                                    </div>
                                    <div class="col-lg-2">
                                        <i class="fa fa-mobile mr-1"></i>
                                        <asp:Label runat="server" Text="ETM Serial No" Font-Bold="true" CssClass="form-control-label"></asp:Label><br />
                                        <asp:Label runat="server" ID="lblETMNo" CssClass="form-control-label text-uppercase"></asp:Label>
                                    </div>
                                    <div class="col-lg-6">
                                        <i class="fa fa-server mr-1"></i>
                                        <asp:Label runat="server" Text="Service Name" Font-Bold="true" CssClass="form-control-label"></asp:Label><br />
                                        <asp:Label runat="server" ID="lblServiceName" CssClass="form-control-label text-uppercase"></asp:Label>
                                    </div>
                                </div>

                                <hr style="margin: 10px" />
                                <div class="row">
                                    <div class="col-lg-2">
                                        <h4 class="mb-0">
                                            <i class="fa fa-users mr-1"></i>
                                            <asp:Label runat="server">Crew Details</asp:Label></h4>
                                    </div>
                                    <div class="col-lg-2">
                                        <i class="fa fa-user mr-1"></i>
                                        <asp:Label runat="server" Text="Driver" Font-Bold="true" CssClass="form-control-label"></asp:Label><br />
                                        <asp:Label runat="server" CssClass="form-control-label text-uppercase" ID="lblDriver"></asp:Label>
                                    </div>
                                    <div class="col-lg-2">
                                        <i class="fa fa-user mr-1"></i>
                                        <asp:Label runat="server" Text="Conductor" Font-Bold="true" CssClass="form-control-label"></asp:Label><br />
                                        <asp:Label runat="server" CssClass="form-control-label text-uppercase" ID="lblConductor"></asp:Label>
                                    </div>
                                </div>
                                <hr style="margin: 10px" />
                                <div class="row" runat="server" id="divtextBox">
                                    <div class="col-lg-7" style="border-right-style: solid; border-color: lightgray; border-width: 1px">
                                        <h4>
                                            <asp:Label runat="server">1. Collection Details</asp:Label></h4>
                                    </div>
                                    <div class="col-lg-5">
                                        <h4>
                                            <asp:Label runat="server">2. Expenditure Details</asp:Label></h4>
                                    </div>
                                    <div class="col-lg-7" style="border-right-style: solid; border-color: lightgray; border-width: 1px;">
                                        <div class="row">
                                            <div class="col-lg">
                                                <asp:Label runat="server" Text="Online Tickets " Font-Bold="true" CssClass="form-control-label">Online Tickets(<i class="fa fa-rupee-sign"></i>)</asp:Label>

                                                <br />
                                                <asp:TextBox class="form-control" runat="server" ReadOnly="true" ID="tbOnlineTktamt" Text="0" MaxLength="6" autocomplete="off" ToolTip="Enter Toll Paid" AutoPostBack="true" OnTextChanged="tbAmount_TextChanged"
                                                    placeholder="Max 6 Digit" Style="font-size: 10pt; height: 30px;"></asp:TextBox>
                                                &nbsp;
										<asp:Label ID="lblTollPaid" Text="N/A" CssClass="form-control-label" Visible="false" runat="server" />

                                            </div>
                                            <div class="col-lg">
                                                <asp:Label runat="server" Text="Enroute Tickets" Font-Bold="true" CssClass="form-control-label">Enroute Tickets (<i class="fa fa-rupee-sign"></i>)</asp:Label>

                                                <br />
                                                <asp:TextBox class="form-control" runat="server" ReadOnly="true" ID="tbEnrouteTktamt" Text="0" MaxLength="6" autocomplete="off" ToolTip="Enter Dhaba Collection" AutoPostBack="true" OnTextChanged="tbAmount_TextChanged"
                                                    placeholder="Max 6 Digit" Style="font-size: 10pt; height: 30px;"></asp:TextBox>
                                                &nbsp;<asp:Label ID="lblDhabaCollection" Text="N/A" CssClass="form-control-label" Visible="false" runat="server" />

                                            </div>
                                            <div class="col-lg">
                                                <asp:Label runat="server" Text="Luggage" Font-Bold="true" CssClass="form-control-label">Luggage(<i class="fa fa-rupee-sign"></i>)</asp:Label>

                                                <br />
                                                <asp:TextBox class="form-control" runat="server" ID="tbLuggageAmt" ReadOnly="true" Text="0" MaxLength="10" autocomplete="off" ToolTip="Enter Total Amount"
                                                    placeholder="Max 10 Digit" Style="font-size: 10pt; height: 30px;"></asp:TextBox>
                                                &nbsp;
										<asp:Label ID="lblTotalAmount" Text="N/A" CssClass="form-control-label" Visible="false" runat="server" />

                                            </div>
                                            <div class="col-lg">
                                                <asp:Label runat="server" Text="Dhaba" Font-Bold="true" CssClass="form-control-label">Dhaba(<i class="fa fa-rupee-sign"></i>)</asp:Label>

                                                <br />
                                                <asp:TextBox class="form-control" runat="server" ID="tbDhabaAmt" ReadOnly="true" Text="0" MaxLength="10" autocomplete="off" ToolTip="Enter Total Amount"
                                                    placeholder="Max 10 Digit" Style="font-size: 10pt; height: 30px;"></asp:TextBox>
                                                &nbsp;
										<asp:Label ID="Label5" Text="N/A" CssClass="form-control-label" Visible="false" runat="server" />

                                            </div>
                                            <div class="col-lg">
                                                <asp:Label runat="server" Text="Other Earning" Font-Bold="true" CssClass="form-control-label">Other Earning(<i class="fa fa-rupee-sign"></i>)</asp:Label>

                                                <br />
                                                <asp:TextBox class="form-control" runat="server" ID="tbOtherEarningAmt" ReadOnly="true" Text="0" MaxLength="10" autocomplete="off" ToolTip="Enter Total Amount"
                                                    placeholder="Max 10 Digit" Style="font-size: 10pt; height: 30px;"></asp:TextBox>
                                                &nbsp;
										<asp:Label ID="Label11" Text="N/A" CssClass="form-control-label" Visible="false" runat="server" />

                                            </div>
                                            <div class="col-lg">
                                                <asp:Label runat="server" Text="Total " Font-Bold="true" CssClass="form-control-label"> Total(<i class="fa fa-rupee-sign"></i>)</asp:Label>

                                                <br />
                                                <asp:TextBox class="form-control" runat="server" ReadOnly="true" ID="tbTotalEarningAmt" Text="0" MaxLength="6" autocomplete="off" ToolTip="Enter Toll Paid" AutoPostBack="true" OnTextChanged="tbAmount_TextChanged"
                                                    placeholder="Max 6 Digit" Style="font-size: 10pt; height: 30px;"></asp:TextBox>
                                                &nbsp;
										<asp:Label ID="Label6" Text="N/A" CssClass="form-control-label" Visible="false" runat="server" />

                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-5">
                                        <div class="row">
                                            <div class="col-lg">
                                                <asp:Label runat="server" Text="Toll Paid" Font-Bold="true" CssClass="form-control-label">Toll Paid (<i class="fa fa-rupee-sign"></i>)</asp:Label>

                                                <br />
                                                <asp:TextBox class="form-control" runat="server" ReadOnly="true" ID="tbTollpaid" Text="0" MaxLength="6" autocomplete="off" ToolTip="Enter Dhaba Collection" AutoPostBack="true" OnTextChanged="tbAmount_TextChanged"
                                                    placeholder="Max 6 Digit" Style="font-size: 10pt; height: 30px;"></asp:TextBox>
                                                &nbsp;<asp:Label ID="Label7" Text="N/A" CssClass="form-control-label" Visible="false" runat="server" />

                                            </div>
                                            <div class="col-lg">
                                                <asp:Label runat="server" Text="Parking" Font-Bold="true" CssClass="form-control-label">Parking(<i class="fa fa-rupee-sign"></i>)</asp:Label>

                                                <br />
                                                <asp:TextBox class="form-control" runat="server" ID="tbParking" ReadOnly="true" Text="0" MaxLength="10" autocomplete="off" ToolTip="Enter Total Amount"
                                                    placeholder="Max 10 Digit" Style="font-size: 10pt; height: 30px;"></asp:TextBox>
                                                &nbsp;
										<asp:Label ID="Label8" Text="N/A" CssClass="form-control-label" Visible="false" runat="server" />

                                            </div>
                                            <div class="col-lg">
                                                <asp:Label runat="server" Text="Other Expense" Font-Bold="true" CssClass="form-control-label">Other Expense(<i class="fa fa-rupee-sign"></i>)</asp:Label>

                                                <br />
                                                <asp:TextBox class="form-control" runat="server" ID="tbOtherExp" ReadOnly="true" Text="0" MaxLength="10" autocomplete="off" ToolTip="Enter Total Amount"
                                                    placeholder="Max 10 Digit" Style="font-size: 10pt; height: 30px;"></asp:TextBox>
                                                &nbsp;
										<asp:Label ID="Label9" Text="N/A" CssClass="form-control-label" Visible="false" runat="server" />

                                            </div>
                                            <div class="col-lg">
                                                <asp:Label runat="server" Text="Total " Font-Bold="true" CssClass="form-control-label"> Total(<i class="fa fa-rupee-sign"></i>)</asp:Label>

                                                <br />
                                                <asp:TextBox class="form-control" runat="server" ReadOnly="true" ID="tbTotalExpenses" Text="0" MaxLength="6" autocomplete="off" ToolTip="Enter Toll Paid" AutoPostBack="true" OnTextChanged="tbAmount_TextChanged"
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
                                        <asp:GridView ID="gvServiceTrips" OnRowDataBound="gvServiceTrips_RowDataBound" runat="server" AutoGenerateColumns="false" GridLines="None"
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
                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="tbPassengerNo" />
                                                        <asp:Label ID="lblPassengerNo" Text="N/A" CssClass="form-control-label" Visible="false" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount">
                                                    <ItemTemplate>
                                                        <asp:TextBox class="form-control" runat="server" ID="tbAmount" Text='<%# Eval("amt") %>' MaxLength="10" autocomplete="off" AutoPostBack="true" OnTextChanged="tbAmount_TextChanged"
                                                            placeholder="Enter Amount" ReadOnly="true" Style="font-size: 10pt; height: 30px; width: 100px; display: inline;"></asp:TextBox>
                                                        <i class="fa fa-rupee" style="font-size: 12pt;"></i>
                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" FilterType="Custom,Numbers" ValidChars="." TargetControlID="tbAmount" />
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
                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender31" runat="server" FilterType="Numbers" ValidChars=""
                                                            TargetControlID="tbActualKm" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Right" CssClass="GridPager" ForeColor="Black" />
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div class="row mt-1" runat="server">
                                    <asp:Panel ID="pnlNoRecordtrip" runat="server" Width="100%" Visible="true">
                                        <div class="col-md-12 p-0" style="text-align: center;">
                                            <div class="col-md-12 busListBox" style="color: #e3e3e3; padding-top: 50px; padding-bottom: 50px; font-size: 33px; font-weight: bold;">
                                                Trips Not Available<br />
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                        <div class="row m-0">
                            <div class="col-lg-12 pt-4 ml-2 pb-2 input-group-prepend">
                                <asp:Label runat="server" Font-Size="Medium" Text="Amount To Be Deposited By Conductor " Font-Bold="true" CssClass="form-control-label mr-1"> Amount To Be Deposited By Conductor(<i class="fa fa-rupee-sign"></i>)</asp:Label>
                                <asp:TextBox class="form-control" Width="100px" runat="server" ReadOnly="true" ID="tbAmountDeposited" Text="0" MaxLength="6" autocomplete="off" ToolTip="Enter Toll Paid" AutoPostBack="true" OnTextChanged="tbAmount_TextChanged"
                                    placeholder="Max 6 Digit" Style="font-size: 10pt; height: 30px;"></asp:TextBox>

                            </div>
                            <div class="col-lg-12 pt-4 ml-2 pb-2">
                                <center>
                                <asp:LinkButton ID="lbtnSave" runat="server" OnClick="lbtnSave_Click" class="btn btn-success" Style="border-radius: 4px;" ToolTip="Save"
                                    OnClientClick="return ShowLoading();">
                                        <i class="fa fa-save"></i> Save</asp:LinkButton>
                                <asp:LinkButton ID="lbtnCancelSave" runat="server" OnClick="lbtnCancel_Click" class="btn btn-danger" Style="border-radius: 4px;" ToolTip="Cancel" OnClientClick="return ShowLoading();"><i class="fa fa-times"></i> Cancel</asp:LinkButton>
                            </center>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <div class="row">
                    <cc1:ModalPopupExtender ID="mpShowDutySlip" runat="server" PopupControlID="Panel2" TargetControlID="Button1"
                        CancelControlID="LinkButton2" BackgroundCssClass="ModalPopupBG">
                    </cc1:ModalPopupExtender>
                    <asp:Panel ID="Panel2" runat="server" Style="position: fixed;">
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
                <div class="row">
                    <cc1:ModalPopupExtender ID="mpShowWaybill" runat="server" PopupControlID="Panel3" TargetControlID="Button3"
                        CancelControlID="LinkButton3" BackgroundCssClass="ModalPopupBG">
                    </cc1:ModalPopupExtender>
                    <asp:Panel ID="Panel3" runat="server" Style="position: fixed;">
                        <div class="modal-content mt-5" style="width: 65vw;">
                            <div class="card w-100">
                                <div class="card-header py-3">
                                    <div class="row">
                                        <div class="col">
                                            <h3 class="m-0">Waybill</h3>
                                        </div>
                                        <div class="col-auto">
                                            <asp:LinkButton ID="LinkButton3" runat="server" Style="color: Red; cursor: pointer;"><i class="fa fa-times fa-2x"></i></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <div class="card-body p-2">

                                    <asp:Literal ID="eWaybill" runat="server"></asp:Literal>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div style="visibility: hidden;">
                            <asp:Button ID="Button3" runat="server" Text="" />
                            <asp:Button ID="Button5" runat="server" Text="" />
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
                    <cc1:ModalPopupExtender ID="mpDepositedConductor" runat="server" PopupControlID="Panel4" TargetControlID="Button6"
                        CancelControlID="LinkButton4" BackgroundCssClass="ModalPopupBG">
                    </cc1:ModalPopupExtender>
                    <asp:Panel ID="Panel4" runat="server" Style="position: fixed;">
                        <div class="modal-content mt-5" style="width: 40vw;">
                            <div class="card w-100">
                                <div class="card-header py-3">
                                    <div class="row">
                                        <div class="col">
                                            <h3 class="m-0">Cash Deposit Slip</h3>
                                        </div>
                                        <div class="col-auto">
                                            <asp:LinkButton ID="LinkButton4" runat="server" Style="color: Red; cursor: pointer;"><i class="fa fa-times fa-2x"></i></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <div class="card-body p-2">

                                    <asp:Literal ID="eDepositedSlip" runat="server"></asp:Literal>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div style="visibility: hidden;">
                            <asp:Button ID="Button6" runat="server" Text="" />
                            <asp:Button ID="Button7" runat="server" Text="" />
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
                                            <h3 class="m-0">List Of Submitted ETM</h3>
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
                    <cc1:ModalPopupExtender ID="mpExpenditureDetails" runat="server" PopupControlID="pnlExpenditureDetails" TargetControlID="Button8"
                        CancelControlID="LinkButton1" BackgroundCssClass="ModalPopupBG">
                    </cc1:ModalPopupExtender>
                    <asp:Panel ID="pnlExpenditureDetails" runat="server" Style="position: fixed;">
                        <div class="modal-content mt-5" style="width: 60vw;">
                            <div class="card w-100">
                                <div class="card-header">
                                    <div class="row">
                                        <div class="col">
                                            <h3 class="m-0">Waybill Collection & Expenditure Details</h3>
                                        </div>
                                        <div class="col-auto">
                                            <asp:LinkButton ID="LinkButton1" runat="server" Style="color: Red; cursor: pointer;"><i class="fa fa-times fa-2x"></i></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <div class="card-body p-2">
                                    <asp:Literal ID="eJourneyDetails" runat="server"></asp:Literal>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div style="visibility: hidden;">
                            <asp:Button ID="Button8" runat="server" Text="" />
                            <asp:Button ID="Button9" runat="server" Text="" />
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

