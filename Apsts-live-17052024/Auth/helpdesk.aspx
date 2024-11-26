<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/sysadmmaster.master" AutoEventWireup="true" CodeFile="helpdesk.aspx.cs" Inherits="Auth_helpdesk" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">


    <script src="../assets/js/jquery-n.js"></script>
    <script src="../assets/js/jquery-ui.js"></script>
    <link href="../assets/css/jquery-ui.css" rel="stylesheet" />
    <script type="text/javascript">
        $(document).ready(function () {
            $("[id$=ddlPlaceFrom]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "helpdesk.aspx/searchStations",
                        data: "{'stationText':'" + request.term + "','fromTo':'F'}",
                        dataType: "json",
                        success: function (data) {
                            response(data.d);
                        },
                        error: function (result) {
                            alert('sdss');
                        }
                    });
                }
            });
            $("[id$=ddlPlaceTo]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "helpdesk.aspx/searchStations",
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
            var endD = new Date(new Date().setDate(todayDate + parseInt(endDate)));
            var currDate = new Date();

            //$('[id*=gmdpJDate]').datepicker({
            //    startDate: "dateToday",
            //    endDate: endD,
            //    changeMonth: true,
            //    changeYear: false,
            //    format: "dd/mm/yyyy",
            //    autoclose: true
            //});
            // $('[id*=tbTicketJourneyDate]').datepicker({
            //    startDate: "dateToday",
            //    endDate: endD,
            //    changeMonth: true,
            //    changeYear: false,
            //    format: "dd/mm/yyyy",
            //    autoclose: true
            //});
            // $('[id*=tbTicketBookingDate]').datepicker({
            //    startDate: "dateToday",
            //    endDate: endD,
            //    changeMonth: true,
            //    changeYear: false,
            //    format: "dd/mm/yyyy",
            //    autoclose: true
            //});

            // $('#CPModal').modal('show');

        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hdmaxdate" runat="server" />
    <asp:HiddenField ID="hidtoken" runat="server" />
    <div class="container-fluid" style="padding-top: 20px;">
        <div class="row">
            <div class="col-lg-12" style="min-height: 600px">
                <div class="card" style="font-size: 12px; min-height: 600px;">
                    <div class="card-body px-0 pt-0">
                        <div class="custom-tab">
                            <nav>
                                <div class="row mr-0">
                                    <div class="col pr-0">
                                        <div class="nav nav-tabs">
                                            <asp:LinkButton runat="server" ID="lbtnTicket" OnClick="lbtnTicket_Click" CssClass="nav-item nav-link active" Font-Size="14px" Font-Bold="true">Ticket</asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="lbtnBusPass" CssClass="nav-item nav-link" OnClick="lbtnBusPass_Click" Font-Size="14px" Font-Bold="true">Bus Passes</asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="lbtnCourier" CssClass="nav-item nav-link" Font-Size="14px" OnClick="lbtnCourier_Click" Font-Bold="true">Courier</asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="lbtnChartedBus" CssClass="nav-item nav-link" Font-Size="14px" OnClick="lbtnChartedBus_Click" Font-Bold="true">Charted Bus</asp:LinkButton>

                                        </div>
                                    </div>
                                    <div class="col-auto" style="border-bottom: 1px solid #dee2e6; padding-top: 2px;">
                                        <div class="input-group float-right" style="width: auto;">
                                            <asp:Label ID="Label22" runat="server" Font-Bold="true" Text="Search" CssClass="mt-2 mr-2"></asp:Label>
                                            <asp:Label ID="Label21" runat="server" Text=""
                                                Style="border-right: 2px solid #e6e9ec; color: #dc3545; line-height: 10px;"
                                                CssClass="mt-0"></asp:Label>

                                            <asp:RadioButton ID="rdbEmployeeSearch" AutoPostBack="true" OnCheckedChanged="rdbEmployeeSearch_CheckedChanged" Text="Employee" CssClass="mt-2 ml-2" Style="color: #a8a8a8; font-weight: bold;" GroupName="Search" runat="server" />
                                            <asp:RadioButton ID="rdbOfficeSearch" AutoPostBack="true" Text="Office" CssClass="ml-3 mt-2" OnCheckedChanged="rdbOfficeSearch_CheckedChanged" Style="color: #a8a8a8; font-weight: bold;" GroupName="Search" runat="server" />
                                            <asp:RadioButton ID="rdbBusSearch" AutoPostBack="true" Text="Bus" CssClass="ml-3 mt-2" Style="color: #a8a8a8; font-weight: bold;" OnCheckedChanged="rdbBusSearch_CheckedChanged" GroupName="Search" runat="server" />
                                            <asp:TextBox ID="txtAllSearch" placeholder="Enter Name/Mobile/Email Id" runat="server" CssClass="form-control ml-2 " Style="max-width: 350px; min-width: 200px;"></asp:TextBox>


                                            <asp:Label ID="lblOffice" runat="server" Visible="false" Text="Office level" Style="margin-bottom: 1px; color: #a8a8a8; font-weight: bold; font-size: 14px; margin-top: 3px; margin-left: 20px"></asp:Label>
                                            <asp:DropDownList ID="ddlOffce" CssClass="form-control ml-1" Visible="false" runat="server"></asp:DropDownList>
                                            <asp:LinkButton ID="lbtnSearchOffice" OnClick="lbtnSearchOffice_Click" ToolTip="Click here for Search" runat="server" CssClass="btn btn-sm btn-warning input-group-append ml-1">
                                                                    <i class="fa fa-search" ></i></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </nav>
                            <%--<nav>
                                <div class="row mr-0">
                                    <div class="col pr-0">
                                        <div class="nav nav-tabs">
                                            <asp:LinkButton runat="server" ID="lbtnTicket" CssClass="nav-item nav-link active" Font-Size="14px" Font-Bold="true">Ticket</asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="lbtnBusPass" CssClass="nav-item nav-link" Font-Size="14px" Font-Bold="true">Bus Passes</asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="lbtnCourier" CssClass="nav-item nav-link" Font-Size="14px" Font-Bold="true">Courier</asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="lbtnChartedBus" CssClass="nav-item nav-link" Font-Size="14px" Font-Bold="true">Charted Bus</asp:LinkButton>

                                        </div>
                                    </div>
                                    <div class="col-auto" style="border-bottom: 1px solid #dee2e6; padding-top: 2px;">
                                        <div class="input-group float-right" style="width: auto;">
                                            <asp:TextBox ID="TextBox2" placeholder="Enter Name/Mobile/Email Id" runat="server" Width="" CssClass="form-control " Style="max-width: 350px; min-width: 250px;"></asp:TextBox>
                                            <asp:LinkButton ID="lbtnSearchEmp" ToolTip="Click here for Search" runat="server" CssClass="btn btn-sm btn-warning input-group-append ml-1">
                                                                    <i class="fa fa-search" ></i>Employee</asp:LinkButton>
                                        
                                        </div>
                                    </div>
                                </div>
                            </nav>--%>
                            <div class="tab-content pl-3 pr-3 pt-0" id="nav-tabContent">
                                <div class="row">
                                    <div class="col-md-12 text-right pl-0">
                                        <asp:LinkButton ID="lbtnFaqs" OnClick="lbtnFaqs_Click" ToolTip="Click here for Search" runat="server" CssClass="btn btn-sm btn-primary mt-1 mb-1">Frequently Asked Questions</asp:LinkButton>
<asp:LinkButton href="../Auth/UserManuals/Help Desk/Help Document for Help Desk.pdf" target="_blank" runat="server" CssClass="btn btn-success btn-sm float-right ml-1 mt-1 mb-1" ToolTip="Click here for manual."><i class="fa fa-download"></i></asp:LinkButton>
                                    
</div>
                                </div>
                                <asp:Panel ID="pnlTicket" Visible="True" runat="server">
                                    <div class="row">
                                        <div class="col-lg-6 shadow" style="min-height: 500px">
                                            <div class="card" style="font-size: 12px; min-height: 530px;">
                                                <div class="row mt-2">
                                                    <div class="col-lg-12">
                                                        <h6 style="color: black; font-size: 13pt; margin-left: 5px">Search Service</h6>
                                                        <div class="row">
                                                            <div class="col-lg-12">
                                                                <div class="row p-2">
                                                                    <div class="col-md-4">
                                                                        <p style="margin-bottom: 1px; color: #a8a8a8; font-weight: bold; font-size: 14px;">
                                                                            From Station
                                                                        </p>
                                                                        <asp:TextBox ID="ddlPlaceFrom" runat="server" autocomplete="off" MaxLength="100" CssClass="form-control ml-1" Style="text-transform: uppercase"
                                                                            placeholder="From Station"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-md-4">
                                                                        <p style="margin-bottom: 1px; color: #a8a8a8; font-weight: bold; font-size: 14px;">
                                                                            To Station
                                                                        </p>
                                                                        <asp:TextBox ID="ddlPlaceTo" runat="server" autocomplete="off" MaxLength="100" CssClass="form-control ml-1" Style="text-transform: uppercase"
                                                                            placeholder="To Station"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-md-4">
                                                                        <p style="margin-bottom: 1px; color: #a8a8a8; font-weight: bold; font-size: 14px;">
                                                                            Service Type
                                                                        </p>
                                                                        <asp:DropDownList ID="ddlServiceType" CssClass="form-control  ml-1 mr-1" runat="server">

                                                                            <asp:ListItem>Service Type</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-lg-9">
                                                                <div class="row p-2">
                                                                    <div class="row">
                                                                        <div class="col-auto" style="padding-top: 2px;">
                                                                            <div class="input-group float-right" style="width: auto;">

                                                                                <asp:LinkButton ID="btntoday" runat="server" OnClick="btntoday_Click" CssClass="btn btn-sm btn-success ml-4">Today
                                                                                </asp:LinkButton>
                                                                                <asp:LinkButton ID="btntomorrow" runat="server" OnClick="btntomorrow_Click" CssClass="btn btn-sm btn-primary ml-1">Tomorrow
                                                                                </asp:LinkButton>
                                                                                <%-- <asp:TextBox ID="" Width="110px" placeholder="DD/MM/YYYY" CssClass="form-control ml-1" runat="server"></asp:TextBox>
                                                                                --%>
                                                                                <asp:TextBox ID="gmdpJDate" runat="server" MaxLength="10" CssClass="form-control ml-1 mr-1" type="Search" placeholder="DD/MM/YYYY" AutoComplete="off"></asp:TextBox>
                                                                                <cc1:CalendarExtender ID="cetxtDateSeatAvailability" runat="server" CssClass="black"
                                                                                    Format="dd/MM/yyyy" PopupButtonID="gmdpJDate" TargetControlID="gmdpJDate"></cc1:CalendarExtender>
                                                                                <asp:LinkButton ID="btnsearch" OnClick="btnsearch_Click" ToolTip="Click here for Search" runat="server" CssClass="btn btn-sm btn-warning ml-1 mr-1">
                                                                    <i class="fa fa-search" ></i>Search </asp:LinkButton>
                                                                                <asp:LinkButton ID="lbtnReset" ToolTip="Click here for Reset" OnClick="lbtnReset_Click" runat="server" CssClass="btn btn-sm btn-danger mr-2">
                                                                    <i class="fa fa-close " ></i>Reset </asp:LinkButton>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <asp:Panel ID="SearchTicketDetails" Visible="false" runat="server">
                                                            <div class="card-body pb-0 px-2" style="padding-top: 1em;">
                                                                <div class="row">
                                                                    <div class="col-lg-12">
                                                                        <div class="col-lg-8">
                                                                            <p class="m-0 text-warning" style="line-height: 18px;">
                                                                                <asp:Label ID="LabelSearchStations" runat="server" Text="" Style="font-weight: bold;"></asp:Label>
                                                                            </p>
                                                                            <p class="m-0 text-warning" style="line-height: 18px;">
                                                                                <asp:Label ID="LabelSearchDateService" runat="server" Text="" Style="font-weight: bold;"></asp:Label>
                                                                            </p>
                                                                            <p class="m-0 text-warning" style="line-height: 18px;">
                                                                                <asp:Label ID="busfund" runat="server" Text="" Style="font-weight: bold;"></asp:Label>
                                                                            </p>
                                                                        </div>
                                                                        <asp:Repeater ID="RepterDetails" runat="server">
                                                                            <ItemTemplate>
                                                                                <div class="card-body pt-2 pb-2">
                                                                                    <div class="row" style="border-top: 1px solid #f3eaea;">
                                                                                        <div class="col-md-5 pr-0 mt-1">
                                                                                            <asp:Label ID="LabelRepteFromStationCode" runat="server" Visible="false" Text='<%#Eval("frstonid") %>' />
                                                                                            <asp:Label ID="LabelRepteToStationCode" runat="server" Visible="false" Text='<%#Eval("tostonid") %>' />
                                                                                            <asp:Label ID="lblopenClose" runat="server" Visible="false" />
                                                                                            <%--   <asp:Label ID="lblservicetypecode" Visible="false" runat="server" Text='<%#Eval("SRTP_ID") %>' />
                                                                                            <asp:Label ID="lblservicetripcode" Visible="false" runat="server" Text='<%#Eval("STRP_ID") %>' />--%>
                                                                                            <i class="fa fa-bus" aria-hidden="true"></i>&nbsp;
                                                                    <asp:Label ID="lblservicetypename" runat="server" Text='<%#Eval("servicetypename") %>'
                                                                        Style="font-size: 14px; font-weight: bold; font-family: verdana;" /><br />
                                                                                            <span style="color: #000066; font-size: 15px">Bus Service Code </span>
                                                                                            <asp:Label ID="lblservicecode" runat="server" Font-Size="15px" Text='<%#Eval("dsvcid") %>' /><asp:Label Font-Size="15px" ID="lblserviceRorJ" runat="server" Text='<%#Eval("tripdirection") %>' /><asp:Label Font-Size="15px" ID="Label1" runat="server" Text='<%#Eval("strpid") %>' /><br />
                                                                                            <span style="color: #000066; font-size: 15px">Total Seats </span>
                                                                                            <asp:Label ID="lblseattobook" runat="server" CssClass="text-primary font-weight-bold " Font-Size="15px" Text='<%#Eval("totalseat") %>' />
                                                                                        </div>
                                                                                        <div class="col-md-4 mt-1 text-center">
                                                                                            <%-- <p class="full-width-separator m-0">
                                                                                                Distance
                                                                        <asp:Label ID="lbldistance" runat="server" Font-Bold="true" ForeColor="#1d4a84" Text='<%#Eval("TOTAL_DIST_KM") %>' />
                                                                                                KM
                                                                                            </p>--%>
                                                                                            <p class="full-width-separator m-0">
                                                                                                Departure Time
                                                                         <asp:Label ID="Label45" ForeColor="#1d4a84" runat="server" Text='<%#Eval("depttime") %>' />

                                                                                            </p>
                                                                                            <p class="full-width-separator m-0">
                                                                                                Arrival Time
                                                                                              <%--  &nbsp;<i class="fa fa-arrow-right" style="font-size: 16px; color: #e6e4e4;"></i>&nbsp;--%>
                                                                                                <asp:Label ID="lblarritime" runat="server" ForeColor="#1d4a84" Text='<%#Eval("arrtime") %>' />
                                                                                            </p>
                                                                                        </div>
                                                                                        <div class="col-md-3 mt-1 text-right">
                                                                                            <p class="full-width-separator m-0" style="font-size: 21px; line-height: 21px;">
                                                                                                <asp:Label ID="Label44" Font-Size="14px" Font-Bold="true" runat="server" Text="Fare" />
                                                                                                <asp:Label ID="lblfare" runat="server" Text='<%#Eval("fare") %> ' />&nbsp;<i class="fa fa-rupee"
                                                                                                    aria-hidden="true"></i>
                                                                                            </p>
                                                                                            <p class="full-width-separator m-0 mt-1">
                                                                                                <span style="font-size: 15px; padding-right: 6px;">Available Seats</span><asp:Label ID="lbltotseats"
                                                                                                    runat="server" Text='<%#Eval("totalavailablseats") %>' Style="font-size: 21px; font-weight: bold; color: green;" />

                                                                                            </p>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                            <SeparatorTemplate>
                                                                            </SeparatorTemplate>
                                                                        </asp:Repeater>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </asp:Panel>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-6 pl-0 shadow" style="min-height: 500px">
                                            <div class="card" style="font-size: 12px; min-height: 530px;">
                                                <div class="row mt-2">
                                                    <div class="col-lg-12">
                                                        <h6 style="color: black; font-size: 13pt; margin-left: 5px">Ticket Details</h6>
                                                        <div class="row p-2">
                                                            <div class="col-md-5">
                                                                <p style="margin-bottom: 1px; color: #a8a8a8; font-weight: bold; font-size: 14px;">
                                                                    Enter Details 
                                                                </p>
                                                                <asp:TextBox ID="tbTicketDetailMobile" autocomplete="off" MaxLength="20"
                                                                    placeholder="Name/Mobile/Email Id/Ticket No." ToolTip="Enter Mobile" CssClass="form-control" runat="server"></asp:TextBox>

                                                            </div>
                                                            <div class="col-md-7">
                                                                <div class="row">
                                                                    <div class="col-md-4">
                                                                        <p style="margin-bottom: 1px; color: #a8a8a8; font-weight: bold; font-size: 14px;">
                                                                            Journey Date
                                                                        </p>
                                                                        <asp:TextBox ID="tbTicketJourneyDate" autocomplete="off" Width="120px"
                                                                            placeholder="DD/MM/YYYY" ToolTip="Select Journey Date" CssClass="form-control" runat="server"></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="black"
                                                                            Format="dd/MM/yyyy" PopupButtonID="tbTicketJourneyDate" TargetControlID="tbTicketJourneyDate"></cc1:CalendarExtender>

                                                                    </div>
                                                                    <div class="col-md-4">
                                                                        <p style="margin-bottom: 1px; color: #a8a8a8; font-weight: bold; font-size: 14px;">
                                                                            Booking Date
                                                                        </p>
                                                                        <asp:TextBox ID="tbTicketBookingDate" autocomplete="off" Width="120px"
                                                                            placeholder="DD/MM/YYYY" ToolTip="Select Booking date" CssClass="form-control" runat="server"></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="black"
                                                                            Format="dd/MM/yyyy" PopupButtonID="tbTicketBookingDate" TargetControlID="tbTicketBookingDate"></cc1:CalendarExtender>

                                                                    </div>
                                                                    <div class="col-md-4">
                                                                        <asp:LinkButton ID="lbtnTicketdetailsSearch" OnClick="lbtnTicketdetailsSearch_Click" ToolTip="Click here for Search" runat="server" CssClass="btn btn-sm btn-warning mt-4">
                                                                    <i class="fa fa-search" >Search</i></asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row mt-0">
                                                    <div class="col-sm-12 flex-column d-flex stretch-card ">
                                                        <div class="card-body table table-responsive" style="min-height: 300px;">
                                                            <asp:Label ID="totserach" runat="server" Style="font-size: 14px; margin-top: 4px;"></asp:Label>
                                                            <asp:GridView ID="gvTicketDetails" runat="server" GridLines="None" CssClass="w-100" AllowPaging="true"
                                                                PageSize="4" AutoGenerateColumns="false" OnRowDataBound="gvTicketDetails_RowDataBound" ShowHeader="false" OnPageIndexChanging="gvTicketDetails_PageIndexChanging" OnRowCommand="gvTicketDetails_RowCommand" DataKeyNames="ticket_no,is_print, departuretime,trvlr_email,trip_status">
                                                                <Columns>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <div class="row pb-2" style="border-bottom: 1px solid #f3eaea;">
                                                                                <div class="col">
                                                                                    <div class="row">
                                                                                        <div class="col">
                                                                                            <h5 class="mb-0">
                                                                                                <%-- <%# If(Eval("STATUS").ToString() = "CONFIRMED", "<i class='fa fa-check-circle-o text-success'></i>", "<i class='fa fa-times-circle-o text-danger'></i>") %>--%>
                                                                                                <asp:Label ID="lblticket" runat="server" Text='<%#Eval("ticket_no") %>'></asp:Label>
                                                                                                <asp:Label ID="lblprint" runat="server" Visible="false" Text='<%#Eval("is_print") %>'></asp:Label>
                                                                                                <asp:Label ID="lbltripstatus" runat="server" Visible="false" Text='<%#Eval("trip_status") %>'></asp:Label>
                                                                                                <asp:Label ID="lblemail" runat="server" Visible="false" Text='<%#Eval("trvlr_email") %>'></asp:Label>
                                                                                            </h5>
                                                                                            <p class="mb-0 text-primary font-weight-bold"><%#Eval("service_type_name") %></p>
<p class="mb-0 mt-1 text-success font-weight-bold "><%#Eval("src") %> - <%#Eval("destinatio") %></p>

                                                                                        </div>
                                                                                        <div class="col-auto">
 <p class="mb-0"><span class="font-weight-bold">Seat No </span> <%#Eval("seatno") %></p>
                                                                                            <p class="mb-0 font-weight-bold"><%#Eval("travellername") %>, <%#Eval("trvlr_gender") %>, <%#Eval("trvl_age") %>Y</p>
                                                                                            <p class="mb-0">
                                                                                                <%#Eval("trvlr_mob") %>
                                                                                            </p>
                                                                                        </div>
                                                                                        <div class="col text-right">
                                                                                            <p class="mb-0">
                                                                                                <asp:Label ID="Label7" runat="server" Font-Bold="true" Text="Booking "></asp:Label><%#Eval("bookingdate") %>
                                                                                            </p>
                                                                                            <p class="mb-0">
                                                                                                <asp:Label ID="Label2" runat="server" Font-Bold="true" Text="Journey "></asp:Label>
                                                                                                <asp:Label ID="lbljouyneydate" runat="server" Text=' <%#Eval("journey_date") %>'></asp:Label>

                                                                                            </p>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>



                                                                                <div class="col-auto">
                                                                                    <p class=" text-right mb-1"><b>Status -</b> <%#Eval("status") %></p>
                                                                                    <asp:LinkButton ID="lbtnPrint" runat="server"
                                                                                        class="btn btn-primary" Style="width: 25px; padding: 5px;" CommandName="print" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' ToolTip="Print Ticket"><i class="fa fa-print" title="Print Ticket"></i></asp:LinkButton>
                                                                                    <asp:LinkButton ID="lbtnresendsms" runat="server" CommandName="resendsms" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                                                        class="btn btn-warning" Style="width: 25px; padding: 5px;" ToolTip="Resend Ticket SMS"><i class="fa fa-comment" title="Resend SMS"></i></asp:LinkButton>
                                                                                    <asp:LinkButton ID="lbtnresendmail" runat="server" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="resendemail"
                                                                                        class="btn btn-danger" Style="width: 25px; padding: 5px;" ToolTip="Resend Ticket Email"><i class="fa fa-envelope" title="Resend Ticket Email" style="font-size: 9pt;padding: 1px;"></i> </asp:LinkButton>

                                                                                    <asp:LinkButton ID="lbtnInfo" runat="server"
                                                                                        class="btn btn-warning" Style="width: 25px; padding: 5px;" CommandName="view" ToolTip="View Detail" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'><i class="fa fa-info-circle" title="View Detail" style="font-size: 9pt; padding: 1px;"></i> </asp:LinkButton>
                                                                                </div>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <PagerStyle CssClass="GridPager" />
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pnlBusPasses" Visible="False" runat="server">
                                    <div class="row">
                                        <h6 style="color: darkgrey; font-size: 30pt; margin-left: 40%; margin-top: 15%">Coming Soon</h6>
                                        <hr style="margin-top: -5px;" />
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pnlCourier" Visible="False" runat="server">
                                    <div class="row">
                                        <h6 style="color: darkgrey; font-size: 30pt; margin-left: 40%; margin-top: 15%">Coming Soon</h6>
                                        <hr style="margin-top: -5px;" />
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pnlChartedBus" Visible="False" runat="server">
                                    <div class="row">
                                        <h6 style="color: darkgrey; font-size: 30pt; margin-left: 40%; margin-top: 15%">Coming Soon</h6>
                                        <hr style="margin-top: -5px;" />
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-12 mt-4" style="min-height: 0px">
                <asp:Panel ID="pnlFaq" Visible="false" runat="server">
                    <div class="card" style="font-size: 12px; min-height: 295px;">
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>
    <!--FAQ Details-->
    <div class="row">
        <cc1:ModalPopupExtender ID="mpFaq" runat="server" PopupControlID="PanelInfo" CancelControlID="LinkButtonMpInfoClose"
            TargetControlID="Button1363456" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="PanelInfo" runat="server" Style="position: fixed;">
            <div class="card" style="width: 850px;">
                <div class="modal-header">
                    <div class="col-md-10">
                        <h3 class="m-0">Frequently Asked Questions</h3>
                    </div>
                    <div class="col-md-2 text-right">
                        <asp:LinkButton ID="LinkButtonMpInfoClose" runat="server" UseSubmitBehavior="false" Style="color: Red; cursor: pointer;"><i class="fa fa-times fa-2x"></i></asp:LinkButton>
                    </div>
                </div>

                <div class="card-body text-left pt-2" style="min-height: 100px;">
                    <div class="row mt-0">
                        <div class="col-lg-12">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-lg-11 wrap-about ftco-animate">
                                            <div class="heading-section">

                                                <div class="row mb-1">
                                                    <div class="col-md-12 m-1 input-group-prepend">
                                                        <asp:Repeater ID="rptfaqcategory" runat="server" OnItemCommand="rptfaqcategory_ItemCommand" OnItemDataBound="rptfaqcategory_ItemDataBound">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdFAQCATID" runat="server" Value='<%# Eval("faqid")%>' />
                                                                <asp:LinkButton ID="lbtnFAQCATNAME" runat="server" CssClass="btn btn-sm btn-warning  ml-1 mt-0" CommandName="FAQ">  <%# Eval("faqcategory")%> </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </div>
                                                </div>
                                                <div class="row mb-3">
                                                    <div id="accordion" role="tablist" class="o-accordion">
                                                        <asp:Repeater ID="rptrFAQ" runat="server">
                                                            <ItemTemplate>
                                                                <h5 class="mt-0 py-2  mb-0 ml-4" style="border-top: 2px solid #f3eaea;"><i class="fa fa-hand-o-right "></i>
                                                                    <a data-toggle="collapse" style="font-family: Poppins" data-parent="#accordion" href="#collapse<%# Eval("faq_id")%>" class="mt-2" aria-expanded="false" aria-controls="collapse<%# Eval("faq_id")%>"><%# Eval("faqS")%>
                                                                    </a>
                                                                </h5>
                                                                <div id="collapse<%# Eval("faq_id")%>" class="collapse text-justify" role="tabpanel" aria-labelledby="headingOne">
                                                                    <div class="card-block ml-5 pb-3">
                                                                        <asp:Label ID="Label41" ForeColor="#999999" CssClass="" Font-Size="14px" runat="server" Text=' <%# Eval("faq_answer")%>'></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <%-- <div style="width: 100%; margin-top: 20px; text-align: right;">
                        <asp:LinkButton ID="LinkButtonMpInfoClose" runat="server" CssClass="btn btn-success btn-sm ml-2"> <i class="fa fa-close"></i> Close </asp:LinkButton>
                    </div>--%>
                </div>
                <div style="visibility: hidden;">
                    <asp:Button ID="Button1363456" runat="server" Text="" />
                </div>
            </div>
        </asp:Panel>
    </div>
    <!--Office Search Details-->
    <div class="row">
        <cc1:ModalPopupExtender ID="mpOfficeData" runat="server" PopupControlID="pnlOfficeData" TargetControlID="Button7676456"
            CancelControlID="LinkButton16798" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlOfficeData" runat="server" Style="position: fixed;">
            <div class="modal-content mt-1">
                <div class="modal-header">
                    <div class="col-md-10">
                        <h3 class="m-0">Office Details</h3>
                    </div>
                    <div class="col-md-2 text-right">
                        <asp:LinkButton ID="LinkButton16798" runat="server" UseSubmitBehavior="false" Style="color: Red; cursor: pointer;"><i class="fa fa-times fa-2x"></i></asp:LinkButton>
                    </div>
                </div>
                <div class="modal-body p-0">
                    <div class="card" style="font-size: 12px; min-height: 150px; min-width: 800px">
                        <div class="row mt-0">
                            <div class="col-sm-12 flex-column d-flex stretch-card ">
                                <div class="card-body table table-responsive">
                                    <asp:GridView ID="gvOfficeSearch" runat="server" GridLines="None" CssClass="w-100" AllowPaging="true"
                                        PageSize="4" OnPageIndexChanging="gvOfficeSearch_PageIndexChanging" AutoGenerateColumns="false" ShowHeader="false">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <div class="row pb-2" style="border-bottom: 1px solid #f3eaea;">
                                                        <div class="col">
                                                            <p class="mb-0">
                                                                <asp:Label ID="Label18" runat="server" Font-Bold="true" Text="Office Name -"></asp:Label>


                                                                <asp:Label ID="Label14" runat="server" Font-Bold="true" ForeColor="Green" Text='<%#Eval("officename") %> '></asp:Label><br />
                                                                <asp:Label ID="Label23" runat="server" Font-Bold="true" Text="Level -"></asp:Label>
                                                                <asp:Label ID="Label24" runat="server" Font-Bold="true" ForeColor="Green" Text='<%#Eval("ofclvl_name") %> '></asp:Label>
                                                                <br />
                                                                <asp:Label ID="Label34" runat="server" Font-Bold="true" Text="Reporting Office -"></asp:Label>
                                                                <asp:Label ID="Label35" runat="server" Font-Bold="true" ForeColor="RED" Text='<%#Eval("reporingoffice_name") %> '></asp:Label>
                                                            </p>

                                                        </div>
                                                        <div class="col-auto">
                                                            <asp:Label ID="Label36" runat="server" Font-Bold="true" Text="Office Id-"></asp:Label>


                                                            <asp:Label ID="Label37" CssClass="text-primary" Font-Bold="true" runat="server" Text=' <%#Eval("officeid_") %> '></asp:Label><br />

                                                            <asp:Label ID="Label38" runat="server" Font-Bold="true" Text="Mobile-"></asp:Label>
                                                            <%#Eval("mobile_") %>
                                                            <p class="mb-0">
                                                                <asp:Label ID="Label25" runat="server" Font-Bold="true" Text="Email -"></asp:Label>
                                                                <%#Eval("email_") %>
                                                            </p>

                                                        </div>
                                                        <div class="col text-right">
                                                            <p class="mb-0">
                                                                <asp:Label ID="Label17" runat="server" Font-Bold="true" Text="Total Under Office -"></asp:Label>


                                                                <asp:Label ID="Label15" CssClass="text-primary" Font-Bold="true" runat="server" Text=' <%#Eval("underofficescount_") %> '></asp:Label><br />
                                                                <asp:Label ID="Label27" runat="server" Font-Bold="true" Text="Total Unit -"></asp:Label>


                                                                <asp:Label ID="Label28" CssClass="text-primary" Font-Bold="true" runat="server" Text=' <%#Eval("unitscount_") %> '></asp:Label><br />
                                                                <asp:Label ID="Label32" runat="server" Font-Bold="true" Text="Total Employee -"></asp:Label>


                                                                <asp:Label ID="Label33" CssClass="text-success" Font-Bold="true" runat="server" Text=' <%#Eval("employeescount_") %> '></asp:Label>

                                                            </p>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle CssClass="pagination-ys" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <div style="visibility: hidden;">
                <asp:Button ID="Button7676456" runat="server" Text="" />
            </div>
        </asp:Panel>
    </div>
    <!--Bus Search Details-->
    <div class="row">
        <cc1:ModalPopupExtender ID="mpBusData" runat="server" PopupControlID="pnlBusData" TargetControlID="Button146456"
            CancelControlID="LinkButton19898" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlBusData" runat="server" Style="position: fixed;">
            <div class="modal-content mt-1">
                <div class="modal-header">
                    <div class="col-md-10">
                        <h3 class="m-0">Bus Details</h3>
                    </div>
                    <div class="col-md-2 text-right">
                        <asp:LinkButton ID="LinkButton19898" runat="server" UseSubmitBehavior="false" Style="color: Red; cursor: pointer;"><i class="fa fa-times fa-2x"></i></asp:LinkButton>
                    </div>
                </div>
                <div class="modal-body p-0">
                    <div class="card" style="font-size: 12px; min-height: 100px; min-width: 800px">
                        <div class="row mt-0">
                            <div class="col-sm-12 flex-column d-flex stretch-card ">
                                <div class="card-body table table-responsive">
                                    <asp:GridView ID="gvBusSearch" runat="server" GridLines="None" CssClass="w-100" AllowPaging="true"
                                        PageSize="4" OnPageIndexChanging="gvBusSearch_PageIndexChanging" AutoGenerateColumns="false" ShowHeader="false">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <div class="row pb-2" style="border-bottom: 1px solid #f3eaea;">
                                                        <div class="col">
                                                            <div class="row">
                                                                <div class="col">
                                                                    <p class="mb-0">
                                                                        <asp:Label ID="Label18" runat="server" Font-Bold="true" Text="Bus No. -"></asp:Label>
                                                                        <asp:Label ID="Label14" runat="server" Font-Bold="true" ForeColor="Green" Text='<%#Eval("busregistration_no") %> '></asp:Label><br />
                                                                        <asp:Label ID="Label23" runat="server" Font-Bold="true" Text="Depot -"></asp:Label>
                                                                        <asp:Label ID="Label25" runat="server" Font-Bold="true" Text='  <%#Eval("officename") %>'></asp:Label>

                                                                        (<asp:Label ID="Label24" runat="server" Font-Bold="true" ForeColor="RED" Text='<%#Eval("officeid") %> '></asp:Label>)
                                                                       
                                                                    </p>

                                                                </div>
                                                                <div class="col-auto">
                                                                    <p class="mb-0 font-weight-bold">
                                                                        <asp:Label ID="Label27" runat="server" Font-Bold="true" Text="Service Type-"></asp:Label>


                                                                        <asp:Label ID="Label28" CssClass="text-primary" Font-Bold="true" runat="server" Text=' <%#Eval("servicetype_nameen") %> '></asp:Label>
                                                                        <br />
                                                                        <asp:Label ID="Label49" runat="server" Font-Bold="true" Text="Wheelbase -"></asp:Label>
                                                                        <asp:Label ID="Label50" runat="server" Font-Bold="true" ForeColor="RED" Text='<%#Eval("wheel_base") %> '></asp:Label>

                                                                    </p>


                                                                </div>
                                                                <div class="col text-right">
                                                                    <p class="mb-0">
                                                                        <asp:Label ID="Label17" runat="server" Font-Bold="true" Text="Chasis -"></asp:Label>


                                                                        <asp:Label ID="Label15" CssClass="text-primary" Font-Bold="true" runat="server" Text=' <%#Eval("chasis_no") %> '></asp:Label><br />
                                                                        <asp:Label ID="Label32" runat="server" Font-Bold="true" Text="Current Status -"></asp:Label>


                                                                        <asp:Label ID="Label33" ForeColor="Green" Font-Bold="true" runat="server" Text=' <%#Eval("current_status") %> '></asp:Label>

                                                                    </p>
                                                                </div>
                                                            </div>
                                                            <%--  <div class="row ">
                                                                <div class="col">
                                                                   
                                                                    <asp:Label ID="Label43" runat="server" Font-Bold="true" ForeColor="Orange" Text='<%#Eval("servicetype_nameen") %> '></asp:Label><br />

                                                                </div>
                                                            </div>--%>
                                                        </div>
                                                    </div>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle CssClass="pagination-ys" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <div style="visibility: hidden;">
                <asp:Button ID="Button146456" runat="server" Text="" />
            </div>
        </asp:Panel>
    </div>

    <!-- ticket Details -->
    <div class="row">
        <cc1:ModalPopupExtender ID="mpTicket" runat="server" PopupControlID="pnlMPEmail" TargetControlID="Button21"
            CancelControlID="LinkButton71" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlMPEmail" runat="server" Style="position: fixed;">
            <div class="modal-content mt-5">
                <div class="modal-header">
                    <div class="col-md-10">
                        <h3 class="m-0">Ticket Details</h3>
                    </div>
                    <div class="col-md-2 text-right">
                        <asp:LinkButton ID="LinkButton71" runat="server" UseSubmitBehavior="false" Style="color: Red; cursor: pointer;"><i class="fa fa-times fa-2x"></i></asp:LinkButton>
                    </div>
                </div>
                <div class="modal-body p-0">
                    <embed src="dashticket.aspx" style="height: 85vh; width: 80vw" />
                </div>
            </div>
            <br />
            <div style="visibility: hidden;">
                <asp:Button ID="Button21" runat="server" Text="" />
            </div>
        </asp:Panel>
    </div>

    <!--Employee Search Deatils-->
    <div class="row">
        <cc1:ModalPopupExtender ID="mpEmployeeSearch" runat="server" PopupControlID="pnlEmp" TargetControlID="Button11111"
            CancelControlID="LinkButton12233" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlEmp" runat="server" Style="position: fixed;">
            <div class="modal-content mt-1">
                <div class="modal-header">
                    <div class="col-md-10">
                        <h3 class="m-0">Employee Details</h3>
                    </div>
                    <div class="col-md-2 text-right">
                        <asp:LinkButton ID="LinkButton12233" runat="server" UseSubmitBehavior="false" Style="color: Red; cursor: pointer;"><i class="fa fa-times fa-2x"></i></asp:LinkButton>
                    </div>
                </div>
                <div class="modal-body p-0">
                    <div class="card" style="font-size: 12px; min-height: 150px; min-width: 800px">
                        <div class="row mt-0">
                            <div class="col-sm-12 flex-column d-flex stretch-card ">
                                <div class="card-body table table-responsive">
                                    <asp:GridView ID="gvEmployeeSearchData" runat="server" OnPageIndexChanging="gvEmployeeSearchData_PageIndexChanging" GridLines="None" CssClass="w-100" AllowPaging="true"
                                        PageSize="4" AutoGenerateColumns="false" ShowHeader="false">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <div class="row pb-2" style="border-bottom: 1px solid #f3eaea;">
                                                        <div class="col">
                                                            <div class="row">
                                                                <div class="col">
                                                                    <p class="mb-0">



                                                                        <asp:Label ID="Label14" runat="server" Font-Bold="true" ForeColor="Green" Text='<%#Eval("e_fname") %> '></asp:Label>
                                                                        <asp:Label ID="Label23" runat="server" Font-Bold="true" ForeColor="Green" Text='<%#Eval("e_mname") %> '></asp:Label>
                                                                        <asp:Label ID="Label24" runat="server" Font-Bold="true" ForeColor="Green" Text='<%#Eval("e_lname") %> '></asp:Label>
                                                                        (<asp:Label ID="Label20" runat="server" ForeColor="Red" Font-Bold="true" Text=' <%#Eval("e_code") %>'></asp:Label>)

                                                                       <%-- ,<asp:Label ID="Label5" runat="server" Font-Bold="true" Text='<%#Eval("AGE") %>'></asp:Label>
                                                                        Yrs--%>
                                                                    </p>
                                                                    <p class="mb-0">
                                                                        <asp:Label ID="Label17" runat="server" Font-Bold="true" Text="Designation-"></asp:Label>
                                                                        <%-- <asp:Label ID="Label31" runat="server" CssClass="text-success" Font-Bold="true" Text='<%#Eval("ROLE") %>'></asp:Label>--%>
                                                                        <asp:Label ID="Label46" runat="server" ForeColor="red" Font-Bold="true" Text='<%#Eval("e_designation_name") %>'></asp:Label>




                                                                    </p>
                                                                </div>
                                                                <div class="col-auto">
                                                                    <p class="mb-0 font-weight-bold">
                                                                    </p>

                                                                    <p class="mb-0">
                                                                        <asp:Label ID="Label16" runat="server" Font-Bold="true" Text="DOJ-"><i class="fa fa-mobile "></i></asp:Label>
                                                                        <asp:Label ID="Label15" CssClass="text-primary" Font-Bold="true" runat="server" Text=' <%#Eval("e_mobile_number") %> '></asp:Label>,
                                                                         <asp:Label ID="Label47" runat="server" Font-Bold="true" Text="DOJ-"><i class="fa fa-envelope "></i></asp:Label>
                                                                        <asp:Label ID="Label48" CssClass="text-success " Font-Bold="true" runat="server" Text=' <%#Eval("e_email_id") %> '></asp:Label><br />


                                                                    </p>
                                                                    <p class="mb-0">
                                                                        <asp:Label ID="Label29" runat="server" Font-Bold="true" Text="Address-"></asp:Label>
                                                                        <%#Eval("e_address") %>
                                                                    </p>
                                                                </div>
                                                                <div class="col text-right">
                                                                    <p class="mb-0">
                                                                    </p>
                                                                    <p class="mb-0">


                                                                        <asp:Label ID="Label26" runat="server" Font-Bold="true" Text="Office -"></asp:Label>
                                                                        <asp:Label ID="Label30" runat="server" CssClass="text-primary" Font-Bold="true" Text='<%#Eval("e_office_name") %>'></asp:Label>
                                                                        <br />

                                                                        <asp:Label ID="Label39" runat="server" Font-Bold="true" Text="Current Status -"></asp:Label>
                                                                        <asp:Label ID="Label40" runat="server" ForeColor="Green" Font-Bold="true" Text=' <%#Eval("status") %>'></asp:Label>
                                                                    </p>
                                                                </div>
                                                            </div>
                                                            <%-- <div class="row">
                                                                <div class="col">

                                                                    <asp:Label ID="Label43" runat="server" Font-Bold="true" ForeColor="Orange" Text='<%#Eval("SERVICENAME") %> '></asp:Label><br />

                                                                </div>
                                                            </div>--%>
                                                        </div>


                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle CssClass="pagination-ys" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
            <br />
            <div style="visibility: hidden;">
                <asp:Button ID="Button11111" runat="server" Text="" />
            </div>
        </asp:Panel>
    </div>

    <%--    print ticket--%>
    <cc1:ModalPopupExtender ID="mpePage" runat="server" PopupControlID="pnlPage" TargetControlID="Button21"
        CancelControlID="LinkButton71" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlPage" runat="server" Style="position: fixed;">
        <div class="modal-content mt-5" style="width: 98%; margin-left: 2%; text-align: center">
            <div class="modal-header">
                <div class="col-md-7">
                    <h3 id="lblTitle" runat="server" class="m-0 text-left"></h3>
                </div>
                <div class="col-md-4 text-right" style="text-align: end;">
                </div>
                <div class="col-md-1 text-right" style="text-align: end;">
                    <asp:LinkButton ID="LinkButton1" runat="server" ForeColor="Red" Font-Bold="true" Font-Size="24px">X</asp:LinkButton>
                </div>
            </div>
            <div class="modal-body" style="background-color: #ffffff; overflow-y: auto; padding: 1px; text-align: center">
                <embed src="" style="height: 90vh; width: 55vw;" runat="server" id="embedPage" />
            </div>
        </div>
        <br />
        <div style="visibility: hidden;">
            <asp:Button ID="Button1" runat="server" Text="" />
        </div>
    </asp:Panel>
</asp:Content>

