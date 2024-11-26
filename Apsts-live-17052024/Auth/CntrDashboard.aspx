<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/Cntrmaster.master" AutoEventWireup="true" CodeFile="CntrDashboard.aspx.cs" Inherits="Auth_CntrDashboard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .btn-QuickLinks {
            color: #1d4ab1;
            background-color: transparent;
            background-image: none;
            border-width: 2px;
            border-color: #236bb3;
            font-weight: 600 !important;
            border-radius: 5px !important;
            font-size: 14px;
        }

        .loadertxtbox {
            background: url(../assets/img/tbloader.gif);
            background-repeat: no-repeat;
            background-position: right;
        }

        .srch-card {
            display: flex;
            flex-wrap: wrap;
            margin: 10px auto;
            color: #000;
            background: #fff;
            line-height: 1.55;
            box-shadow: 0 1px 2px rgb(0 0 0 / 20%);
        }

            .srch-card .srch-names {
                width: 30%;
                padding: 25px 0 25px 25px;
            }

            .srch-card .srch-times {
                display: flex;
                justify-content: space-around;
                width: 50%;
                padding: 25px 5%;
            }

            .srch-card .srch-seats {
                width: 10%;
                width: calc(20% - 110px);
                font-size: .8rem;
                padding: 25px 0;
            }

            .srch-card .srch-fares {
                width: 110px;
                font-size: .8rem;
                padding: 25px 25px 25px 0;
            }
    </style>
    <link rel="stylesheet" href="../assets/css/jquery-ui.css" type="text/css" />

    <script type="text/javascript">
        $(document).ready(function () {
            $("[id$=tbfromstation]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "CntrDashboard.aspx/source_destination",
                        data: "{'stationText':'" + request.term + "','fromTo':'F'}",
                        dataType: "json",
                        success: function (data) {
                            response(data.d);
                        },
                        error: function (result) {

                        }
                    });
                }
            });
            $("[id$=tbtostation]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "CntrDashboard.aspx/source_destination",
                        data: "{'stationText':'" + request.term + "','fromTo':'T'}",
                        dataType: "json",
                        success: function (data) {
                            response(data.d);
                        },
                        error: function (result) {

                        }
                    });
                }
            });

            var todayDate = new Date().getDate();
            var endDate = $("[id$=hdmaxdate]").val();
            var endD = new Date(new Date().setDate(todayDate + parseInt(endDate - 1)));
            var currDate = new Date();
            $('[id*=tbjourneydate]').datepicker({
                startDate: "dateToday",
                endDate: endD,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            });

        });
        function close() {
            alert('ok');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hdmaxdate" runat="server" />
    <asp:HiddenField ID="hidtoken" runat="server" />
    <div class="container-fluid pt-2">
        <div class="row align-items-center">
            <div class="col-xl-12">
                <div class="card card-stats mb-3">
                    <div class="row">
                        <div class="col-4 border-right">
                            <div class="card-body">
                                <div class="row m-0">
                                    <div class="col-12">
                                        <h4 class="mb-1">Amount Summary</h4>
                                        <div class="row m-0">
                                            <div class="col-6 border-right">
                                                <div class="row m-0">
                                                    <div class="col-12">
                                                        <h5 class="card-title text-muted mb-0">
                                                            <asp:Label ID="lblmanamtdate" runat="server" Text="Mandatory Amount Pending for Deposit upto 24/01/2022"></asp:Label>&nbsp;
                                 <asp:Label ID="lblmandatoryamt" runat="server" data-toggle="tooltip" data-placement="bottom" title="Mandatory Amount Pending for Deposit" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>
                                                    </div>
                                                    <div class="col-12">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-6">
                                                <div class="row m-0">
                                                    <div class="col-12">
                                                        <h5 class="card-title text-muted mb-0">
                                                            <asp:Label ID="lblpndgdepodate" runat="server" Text="Total Pending Amount To Deposit Till 26/01/2022 09:10 PM"></asp:Label>&nbsp;
                                 <asp:Label ID="lbltotpendingamt" runat="server" data-toggle="tooltip" data-placement="bottom" title="Total Pending Amount To Deposit" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>
                                                    </div>
                                                    <div class="col-12">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-8 border-right">
                            <div class="card-body">
                                <div class="row m-0">
                                    <div class="col-12">
                                        <h4 class="mb-1">
                                            <asp:Label ID="lbltodaysummary" runat="server"></asp:Label>
                                        </h4>
                                        <div class="row m-0">
                                            <div class="col-4 border-right">
                                                <div class="row m-0">
                                                    <div class="col-12">
                                                        <h6 class="mb-1">Online Booking</h6>
                                                        <div class="row m-0">
                                                            <div class="col-6 border-right">
                                                                <h5 class="card-title text-muted mb-0">Ticket&nbsp;<br />
                                                                    <asp:Label ID="lbltotalticket" runat="server" data-toggle="tooltip" data-placement="bottom" title="tatal online ticket" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>

                                                            </div>
                                                            <div class="col-6 border-right">
                                                                <h5 class="card-title text-muted mb-0">Amount &nbsp;<br />
                                                                    <asp:Label ID="lblticketamount" runat="server" data-toggle="tooltip" data-placement="bottom" title="total online ticket amount" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>

                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-12">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-4 border-right">
                                                <div class="row m-0">
                                                    <div class="col-12">
                                                        <h6 class="mb-1">Current Booking</h6>
                                                        <div class="row m-0">
                                                            <div class="col-6 border-right">
                                                                <h5 class="card-title text-muted mb-0">Trip&nbsp;<br />
                                                                    <asp:Label ID="lbltotaltrip" runat="server" data-toggle="tooltip" data-placement="bottom" title="Total Current Booking Trips" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>

                                                            </div>
                                                            <div class="col-6 border-right">
                                                                <h5 class="card-title text-muted mb-0">Amount &nbsp;<br />
                                                                    <asp:Label ID="lbltotaltripamt" runat="server" data-toggle="tooltip" data-placement="bottom" title="Total Current Booking Amount" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>

                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-12">
                                                    </div>
                                                </div>
                                            </div>
                                            <%-- <div class="col-3 border-right">
                                                <div class="row m-0">
                                                    <div class="col-12">
                                                        <h6 class="mb-1">Bus Passes</h6>
                                                        <div class="row m-0">
                                                            <div class="col-6 ">
                                                                <h5 class="card-title text-muted mb-0">Coming Soon&nbsp;<br />
                                                                   <asp:Label ID="Label4" runat="server" data-toggle="tooltip" data-placement="bottom" title="Total New Bus Passes Generated" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="Coming Soon"></asp:Label></h5>

                                                            </div>
                                                            <div class="col-6 border-right">
                                                                <h5 class="card-title text-muted mb-0">Amount &nbsp;<br />
                                                                    <asp:Label ID="Label7" runat="server" data-toggle="tooltip" data-placement="bottom" title="Total Amount New Bus Passes Generated" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="Coming Soon"></asp:Label></h5>

                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-12">
                                                    </div>
                                                </div>
                                            </div>--%>
                                            <div class="col-4">
                                                <div class="row m-0">
                                                    <div class="col-12">
                                                        <h6 class="mb-1">Trip Chart</h6>
                                                        <div class="row m-0">
                                                            <div class="col-6 border-right">
                                                                <h5 class="card-title text-muted mb-0">Ready To prepare&nbsp;
                                 <asp:Label ID="Label2" runat="server" data-toggle="tooltip" data-placement="bottom" title="Ready to prepare trips" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>

                                                            </div>
                                                            <div class="col-6">
                                                                <h5 class="card-title text-muted mb-0">Up Comming &nbsp;
                                 <asp:Label ID="Label8" runat="server" data-toggle="tooltip" data-placement="bottom" title="Up comming trips" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>

                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-12">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-xl-4 order-xl-1">
                <div class="card">
                    <div class="card-header border-bottom">
                        <div class="row pr-5 mr-5">
                            <div class="col-lg-1">
                                <i class="fa fa-list-ul text-muted "></i>
                            </div>
                         <div class="col-lg-11">
                             <asp:Label runat="server" Text="Quick Actions" Font-Bold="true" Font-Size="Large"></asp:Label>
  <asp:LinkButton href="../Auth/UserManuals/Booking_Counter/Help Document for Booking Counter.pdf" target="_blank" runat="server" CssClass="btn btn-success btn-sm" ToolTip="Click here for manual."><i class="fa fa-download"></i></asp:LinkButton>
                           
                         </div>
                        
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-lg-6">
                                <asp:LinkButton ID="lbtnCurrentbooking" runat="server" OnClick="lbtnCurrentbooking_Click"
                                     CssClass="btn btn-QuickLinks" Style="width: 100%; box-shadow: rgb(0 0 0 / 24%) 0px 3px 8px;" OnClientClick="return ShowLoading()">
                                    Current Booking &nbsp; <i class="fa fa-forward"></i>
                                </asp:LinkButton>
                            </div>
                            <div class="col-lg-6">
                                <asp:LinkButton ID="lbtnquery" OnClientClick="return ShowLoading()" runat="server" CssClass="btn btn-QuickLinks" OnClick="lbtnquery_Click" Style="width: 100%; box-shadow: rgb(0 0 0 / 24%) 0px 3px 8px;">
                                    Ticket query &nbsp; <i class="fa fa-forward"></i>
                                </asp:LinkButton>
                            </div>
                          
                        </div>
                        <div class="row mt-2">
                            <div class="col-lg-6">
                                <asp:LinkButton ID="lbtncanceltkt" OnClientClick="return ShowLoading()" runat="server" OnClick="lbtncanceltkt_Click" CssClass="btn btn-QuickLinks" Style="width: 100%; box-shadow: rgb(0 0 0 / 24%) 0px 3px 8px;">
                                    Cancel Tickets &nbsp; <i class="fa fa-forward"></i>
                                </asp:LinkButton>
                            </div>
                            <div class="col-lg-6">
                                <asp:LinkButton ID="lbtnspcialcancel" OnClientClick="return ShowLoading()" runat="server" OnClick="lbtnspcialcancel_Click" CssClass="btn btn-QuickLinks" Style="width: 100%; box-shadow: rgb(0 0 0 / 24%) 0px 3px 8px;">
                                    Special Cancellation &nbsp; <i class="fa fa-forward"></i>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="row mt-2">
                            <div class="col-lg-6">
                                <asp:LinkButton ID="lbtncashregister" OnClientClick="return ShowLoading()" runat="server" OnClick="lbtncashregister_Click" CssClass="btn btn-QuickLinks" Style="width: 100%; box-shadow: rgb(0 0 0 / 24%) 0px 3px 8px;">
                                    Cash Register &nbsp; <i class="fa fa-forward"></i>
                                </asp:LinkButton>
                            </div>
                            <div class="col-lg-6">
                                <asp:LinkButton ID="lbtntripchart" OnClientClick="return ShowLoading()" Enabled="true" runat="server" OnClick="lbtntripchart_Click" CssClass="btn btn-QuickLinks" Style="width: 100%; box-shadow: rgb(0 0 0 / 24%) 0px 3px 8px;">
                                    Trip Chart &nbsp; <i class="fa fa-forward"></i>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <%--<div class="row mt-2">
                            
                            <div class="col-6">
                                <asp:LinkButton ID="lbtnpassqry" Enabled="false" Visible="false" runat="server" CssClass="btn btn-QuickLinks" Style="width: 100%; box-shadow: rgb(0 0 0 / 24%) 0px 3px 8px;">
                                    Pass query &nbsp; <i class="fa fa-forward"></i>
                                </asp:LinkButton>
                            </div>
                        </div>--%>
                        <div class="row mt-2">
                            <div class="col-lg-6">
                                <asp:LinkButton ID="lbtntranreport" OnClientClick="return ShowLoading()" runat="server" CssClass="btn btn-QuickLinks" OnClick="lbtntranreport_Click" Style="width: 100%; box-shadow: rgb(0 0 0 / 24%) 0px 3px 8px;">
                                    Transaction Report &nbsp; <i class="fa fa-forward"></i>
                                </asp:LinkButton>
                            </div>
                              <div class="col-lg-6" runat="server" id="divbuspass">
                                <asp:LinkButton ID="lbtnbuspasses" OnClientClick="return ShowLoading()" runat="server" OnClick="lbtnbuspasses_Click" CssClass="btn btn-QuickLinks" Style="width: 100%; box-shadow: rgb(0 0 0 / 24%) 0px 3px 8px;">
                                    Bus Passes &nbsp; <i class="fa fa-forward"></i>
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card">
                    <div class="card-header">
                        <div class="row pr-5 mr-5">
                               <div class="col-lg-1">
                                <i class="fa fa-file-alt  text-muted "></i>
                            </div>
                         <div class="col-lg-11">
                             <asp:Label runat="server" Text="Ticket Query" Font-Bold="true" Font-Size="Large"></asp:Label>
                         </div>

                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-8">
                                <div class="form-group mb-0">
                                    <div class="input-group">
                                        <asp:TextBox ID="tbticketno" runat="server" AutoComplete="Off" class="form-control form-control-sm text-uppercase" placeholder="Search ticket number" MaxLength="20"></asp:TextBox>
                                        <div class="input-group-append">
                                            <asp:LinkButton ID="lbtnSearchTicketNo" runat="server" OnClick="lbtnSearchTicketNo_Click" CssClass="btn btn-success btn-icon-only btn-sm mr-1 " strle="z-i">
                                            <i class="fa fa-search mt-2"></i>
                                            </asp:LinkButton>
                                        </div>
                                        <div class="input-group-append">
                                            <asp:LinkButton ID="lbtnRestTicketNo" runat="server" OnClick="lbtnRestTicketNo_Click" CssClass="btn btn-danger btn-icon-only btn-sm mr-1">
                                            <i class="fa fa-undo mt-2"></i>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
                <div class="card" runat="server" visible="false">
                    <div class="card-header">
                        <div class="row">
                            <div class="col-md-12">
                                <h3 class="mb-0">Pass Query</h3>
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-8">
                                <div class="form-group mb-0">
                                    <div class="input-group">
                                        <asp:TextBox ID="tbpassnumber" runat="server" Enabled="false" AutoComplete="Off" class="form-control form-control-sm text-uppercase" placeholder="Search pass number" MaxLength="20"></asp:TextBox>
                                        <div class="input-group-append">
                                            <asp:LinkButton ID="lbtnSearchPassNo" Enabled="false" runat="server" CssClass="btn btn-success btn-icon-only btn-sm mr-1 " strle="z-i">
                                            <i class="fa fa-search mt-2"></i>
                                            </asp:LinkButton>
                                        </div>
                                        <div class="input-group-append">
                                            <asp:LinkButton ID="lbtnRestPassNo" Enabled="false" runat="server" CssClass="btn btn-danger btn-icon-only btn-sm mr-1">
                                            <i class="fa fa-undo mt-2"></i>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-xl-8 order-xl-2">
                <div class="card" style="min-height: 490px">
                    <div class="card-header">
                        <div class="row align-items-center">
                            <div class="col-8">
                                <h3 class="mb-0">Online Ticket Booking</h3>
                            </div>
                            <div class="col-md-4 text-right">
                                <p style="font-size: small; margin: 0px;" class="text-danger font-weight-bold"><span class="text-warning">* &nbsp; Fields are mandatory</span></p>
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row pl-lg-3 pr-lg-3">
                            <div class="col-lg-3">
                                <span class="form-control-label">From Station <span class="text-danger">*</span></span>
                                <asp:TextBox ID="tbfromstation" CssClass="form-control form-control-sm search-box" AutoComplete="Off" Placeholder="From Station" runat="server" MaxLength="20"></asp:TextBox>

                                <cc1:FilteredTextBoxExtender ID="ft_tbfromstation" runat="server" FilterType="UppercaseLetters, LowercaseLetters,Custom" TargetControlID="tbfromstation" ValidChars=" ()" />

                            </div>
                            <div class="col-lg-3">
                                <span class="form-control-label">To Station <span class="text-danger">*</span></span>
                                <asp:TextBox ID="tbtostation" CssClass="form-control form-control-sm" AutoComplete="Off" Placeholder="To Station" runat="server" MaxLength="20"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="ft_tbtostation" runat="server" FilterType="UppercaseLetters, LowercaseLetters,Custom" TargetControlID="tbtostation" ValidChars=" ()" />
                            </div>
                            <div class="col-lg-2">
                                <span class="form-control-label">Journey Date </span>
                                <asp:TextBox ID="tbjourneydate" CssClass="form-control form-control-sm" AutoComplete="Off" Placeholder="DD/MM/YYYY" runat="server" MaxLength="10"></asp:TextBox>


                            </div>
                            <div class="col-lg-3">
                                <span class="form-control-label">Service Type</span>
                                <asp:DropDownList ID="ddlServiceType" CssClass="form-control form-control-sm" runat="server">
                                </asp:DropDownList>
                            </div>
                            <div class="col-lg-1 pt-4">
                                <asp:LinkButton ID="lbtnSearchService" OnClientClick="return ShowLoading()" runat="server" data-toggle="tooltip" data-placement="top" OnClick="lbtnSearchService_Click"
                                     ToolTip="Click here to search services" CssClass="btn btn-success btn-icon-only btn-sm" strle="z-i">
       
                         <i class="fa fa-search mt-2"></i>
                                </asp:LinkButton>
                                <asp:LinkButton ID="lbtnRestSearchService" OnClick="lbtnRestSearchService_Click" runat="server" data-toggle="tooltip" data-placement="top" ToolTip="Click here to reset search values" CssClass="btn btn-danger btn-icon-only btn-sm float-right">
                                            <i class="fa fa-undo mt-2"></i>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="row px-3">
                            <div class="col-lg-12">


                                <asp:GridView ID="gvService" runat="server" GridLines="None" ShowHeader="false" AutoGenerateColumns="false" Width="100%" OnRowDataBound="gvService_RowDataBound" OnRowCommand="gvService_RowCommand"
                                    DataKeyNames="openclose,dsvcid,servicename,strpid,frstonid,tostonid,depttime,arrtime,tripdirection,srtpid,servicetypename,layout,totalavailablseats,totalseat,routeid,routename,s_code,distance,fare,midstations,from_station_name,to_station_name">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <div class="card p-2 border ">
                                                    <div class="row px-3 pt-1">
                                                        <div class="col">
                                                            <span class="h3 font-weight-bold mb-0"><%# Eval("dsvcid") %><%# Eval("tripdirection") %><%# Eval("strpid") %> | <%# Eval("servicetypename") %>  </span>
                                                        </div>
                                                        <div class="col-auto text-right ">
                                                            <span class="h5 font-weight-bold mb-0"><span class="h6 text-muted">Departure </span><%# Eval("depttime") %></span>
                                                        </div>
                                                    </div>
                                                    <div class="row px-3 pb-2">
                                                        <div class="col">
                                                            <span class="h5 font-weight-bold mb-0"><%# Eval("totalavailablseats") %> <span class="h6 text-muted">seats available out of </span><%# Eval("totalseat") %><span class="h6 text-muted"> seats</span> </span>
                                                        </div>
                                                        <div class="col-auto text-right ">
                                                            <span class="h5 font-weight-bold mb-0"><span class="h6 text-muted">Arrival </span><%# Eval("arrtime") %></span>
                                                        </div>
                                                    </div>
                                                    <div class="row px-3 pb-1">
                                                        <div class="col">
                                                            <button type="button" class="btn btn-sm" data-container="body" data-toggle="popover" data-color="default" data-placement="top" data-content='<%# Eval("midstations") %>'>
                                                                <i class="fa fa-building mr-1"></i>Mid Stations
                                                            </button>
                                                            <button type="button" class="btn btn-sm" data-container="body" data-toggle="popover" data-color="default" data-placement="top" data-content='<%# Eval("amenity") %>'>
                                                                <i class="fa fa-beer mr-1"></i>Amenities
                                                            </button>
                                                        </div>
                                                        <div class="col-auto text-right ">
                                                            <span class="card-title text-default mr-3 font-weight-bold"><i class="fa fa-rupee-sign mr-1"></i><%# Eval("fare") %> Per Seat</span>
                                                            <asp:LinkButton ID="lbtnOrgView" runat="server" CssClass="btn btn-icon btn-primary btn-sm"
                                                                CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="BOOKTICKET" data-toggle="tooltip" data-placement="bottom"
                                                                title="Proceed to booking" OnClientClick="return ShowLoading()"><i class="fa fa-check"></i> BOOK
                                                            </asp:LinkButton>

                                                            <asp:LinkButton ID="lbtnclose" runat="server" Enabled="false" CssClass="btn btn-icon btn-secondary btn-sm mb-2"
                                                                CommandName="Booking Closed"> Closed</asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                                <asp:Panel ID="pnlNoService" runat="server">
                                    <div class="row">
                                        <div class="col-lg-12 text-center mt-4">
                                            <i class="ni ni-bus-front-12 ni-5x text-muted"></i>
                                        </div>
                                        <div class="col-lg-12 text-center mt-4">
                                            <span class="h2 font-weight-bold mb-0">Thanks for being here</span>
                                            <h5 class="card-title text-muted mb-0">
                                                <asp:Label ID="lblNoServiceMsg" runat="server"></asp:Label>
                                            </h5>
                                        </div>
                                    </div>
                                </asp:Panel>

                            </div>
                        </div>


                    </div>

                </div>
            </div>
        </div>


        <div class="row">

            <cc1:ModalPopupExtender ID="mpFirst" runat="server" CancelControlID="btnMPfirstClose"
                TargetControlID="Button1" PopupControlID="pnlfirst" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlfirst" Style="display: none" runat="server">
                <div class="card">
                    <div class="card-header">
                        <h4 class="card-title m-0" style="font-size: 20px;">Information
                        </h4>
                    </div>
                    <div class="card-body">
                        <div class="col-lg-12 text-center">
                            <i class="fa fa-rupee" style="font-size: 100px;"></i>
                            <p style="font-size: 14pt; color: #dc3535; font-weight: bold;">
                                <asp:Label ID="lblfirstmsg" runat="server" Text=""></asp:Label>
                            </p>
                        </div>
                    </div>
                    <div class="card-footer" style="text-align: right;">
                        <asp:LinkButton ID="btnMPfirstClose" runat="server" CssClass="btn btn-danger btn-sm font-weight-bold px-3" Style="border-radius: 4px;"> <i class="fa fa-times"></i> OK </asp:LinkButton>
                    </div>
                </div>
                <div style="visibility: hidden; height: 0px;">
                    <asp:Button ID="Button1" runat="server" />
                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>

