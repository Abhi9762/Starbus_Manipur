<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/SysAdmmaster.master" AutoEventWireup="true" CodeFile="LiveDataDashboard.aspx.cs" Inherits="Auth_LiveDataDashboard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        $(window).on('load', function () {
            HideLoading();
        });
        function ShowLoading() {
            var div = document.getElementById("loader");
            div.style.display = "block";
        }
        function HideLoading() {
            var div = document.getElementById("loader");
            div.style.display = "none";
        }
    </script>


    <style>
        hr {
            margin-top: 6px;
            margin-bottom: 1rem;
        }

        .card {
            border: 1px solid #e6e6e6;
        }
    </style>

   <script>
       $(document).ready(function () {

           var currDate = new Date().getDate();
           var preDate = new Date(new Date().setDate(currDate - 1));
           var todayDate = new Date(new Date().setDate(currDate));
           var FutureDate = new Date(new Date().setDate(currDate + 30000));

           $('[id*=txttrnsdate]').datepicker({
               endDate: todayDate,
               changeMonth: true,
               changeYear: false,
               format: "dd/mm/yyyy",
               autoclose: true
           });
          
       });
       
        
   </script>
    <script type="text/javascript" src="assets/Charts/chart.js/chart.min.js"></script>
    <link rel="stylesheet" href="../style.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

   
    <div class="container-fluid pt-top">
        <div class="row">
            <div class="col-lg-9">
                <asp:Label ID="lblason" runat="server"></asp:Label>
            </div>
            <div class="col-lg-1 text-right">
                <label class="text-muted form-control-label" style="padding-top: 17px;">
                    Transaction Date</label>
            </div>
            <div class="col-lg-2 pt-2">
                <div class="input-group-prepend">
                    <asp:TextBox CssClass="form-control" runat="server" ID="txttrnsdate" MaxLength="10" ToolTip="Enter Transaction Date"
                        placeholder="DD/MM/YYYY" Text="" Style="display: inline;border-radius: 4px 0px 0px 4px;"></asp:TextBox>
                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers, Custom"
                        TargetControlID="txttrnsdate" ValidChars="/" />
                    <asp:LinkButton ID="lbtnshow" ToolTip="Show Live Summary" OnClientClick="return ShowLoading()" OnClick="lbtnshow_Click" runat="server" CssClass="btn btn-success btn-sm" Style="border-radius: 0px 4px 4px 0px;">
                                            <i class="fa fa-search" style="margin-top:2px;"></i> 
                </asp:LinkButton>
                </div>
            </div>
        </div>
    
       
	<div class="row">
            <div class="col-sm-12">
                <i class="fa fa-list-ol"></i>
                <asp:Label ID="Label4" runat="server" Text="En-Route Booking" Style="font-size: 16pt; font-weight: bold; color: #074886;"></asp:Label>
                <asp:LinkButton ID="lbtnenRouterefresh" runat="server" CssClass="btn btn-warning btn-sm"  OnClick="lbtnenRouterefresh_Click">
                    <i class="fa fa-recycle"></i> Refresh
                </asp:LinkButton>
                <hr />
            </div>

            <div class="col-md-12">
                <asp:Panel runat="server" ID="pnEnrouteData" Visible="false">
                    <div class="row">
                        <div class="col-md-5">

                            <div class="card">
                                <div class="card-body">

                                    <div class="row mb-3">
                                        <div class="col-sm-4">
                                            <h4 class="mb-0">Total Services <span class="badge badge-primary" style="font-size: 14px;">
                                                <asp:Label runat="server" ID="lbltotalservice"></asp:Label>
                                                                            </span> </h4>
                                        </div>
                                        <div class="col-sm-4">
                                            <h4 class="mb-0">Total Trips <span class="badge badge-primary" style="font-size: 14px;">
                                                <asp:Label runat="server" ID="lbltotaltrips"></asp:Label></span> </h4>
                                        </div>

                                    </div>
                                    <div class="row mb-3">
                                        <div class="col-sm-4">
                                            <h4 class="mb-0">Passenger <span class="badge badge-primary" style="font-size: 14px;">
                                                <asp:Label runat="server" ID="lbltotalpassenger"></asp:Label>
                                                                       </span> </h4>
                                            <p class="mb-0">Total <b><asp:Label runat="server" ID="lbltotalpassengeramt"></asp:Label> ₹</b> </p>
                                            <p class="mb-0">Adult <b><asp:Label runat="server" ID="lbladultamt"></asp:Label> ₹</b> </p>
                                            <p class="mb-0">Child <b><asp:Label runat="server" ID="lblchildamt"></asp:Label> ₹</b> </p>
                                        </div>
                                        <div class="col-sm-4">
                                            <h4 class="mb-0">Luggage <span class="badge badge-primary" style="font-size: 14px;">
                                                <asp:Label runat="server" ID="lblluggagecount"></asp:Label>
                                                                     </span></h4>
                                            <p class="mb-0">Total <b><asp:Label runat="server" ID="lblluggageamt"></asp:Label> ₹</b> </p>

                                        </div>
                                        <div class="col-sm-4">
                                            <h4 class="mb-0">Concession <span class="badge badge-primary" style="font-size: 14px;">
                                                <asp:Label runat="server" ID="lblconcessioncount"></asp:Label>
                                                                        </span></h4>
                                            <p class="mb-0">Total <b><asp:Label runat="server" ID="lblconcessionamt"></asp:Label> ₹</b> </p>

                                        </div>
                                    </div>

                                </div>
                            </div>

                        </div>
                        <div class="col-md-7">

                            <div class="card">
                                <div class="card-body pb-0">
                                    <asp:Repeater ID="rptEnroute" runat="server" OnItemCommand="rptEnroute_ItemCommand">
                                        <ItemTemplate>
 <asp:HiddenField runat="server" ID="hdDsvcId" Value='<%#Eval("dsvcid")%>' />
                                            <asp:HiddenField runat="server" ID="hdWaybillNo" Value='<%#Eval("waybill")%>' />
                                            <h5 class="mb-0"><%# Eval("servicename") %></h5>
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <h6 style="font-size: 12px;" class="mb-0">Passenger <span class="badge badge-primary" style="font-size: 14px;"><%# Eval("psng_count") %></span> </h6>
                                                    <p class="mb-1" style="font-size: 13px;">
                                                        <%# Eval("psng") %>
                                                    </p>
                                                </div>
                                                <div class="col-sm-3">
                                                    <h6 style="font-size: 12px;" class="mb-0">Luggage <span class="badge badge-primary" style="font-size: 14px;"><%# Eval("lugg_count") %></span></h6>
                                                    <p class="mb-1" style="font-size: 13px;">
                                                        <%# Eval("lugg") %>
                                                    </p>
                                                </div>
                                                <div class="col-sm-2">
                                                    <h6 style="font-size: 12px;" class="mb-0">Concession <span class="badge badge-primary" style="font-size: 14px;"><%# Eval("cnsn_count") %></span></h6>
                                                    <p class="mb-1" style="font-size: 13px;">
                                                        <%# Eval("cnsn") %>
                                                    </p>
                                                </div>
<div class="col-sm-1">
                                                    <asp:LinkButton runat="server" ToolTip="View Summary" CommandName="ViewSummary" CssClass="btn btn-primary btn-sm">
                                                        <i class="fa fa-eye"></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            <hr class="my-2" />

                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>


                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel runat="server" ID="pnNoData" Visible="true">
                    <div class="row">
                        <div class="col-12 mb-4">
                            <asp:Label runat="server" ID="lblnoData" ForeColor="LightGray" Font-Bold="true" Font-Size="X-Large" Text="En-Route Booking Data Not Available For Selected Date"></asp:Label>
                        </div>
                    </div>
                </asp:Panel>
            </div>



        </div>
     

        <div class="row">
            <div class="col-lg-12">
                <i class="fa fa-list-ol"></i>
                <asp:Label ID="Label1" runat="server" Text="Online Booking" Style="font-size: 16pt; font-weight: bold; color: #074886;"></asp:Label>
                <hr />
            </div>
        </div>
        <div class="row">
            <div class="col-md-4  stretch-card transparent">
                <div class="card card-dark-blue">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-lg-12 text-center">
                                <h4>Ticket Booked</h4>
                                <h2>
                                    <asp:Label ID="lbltotaltktbooked" runat="server" Text="0" CssClass="text-muted "></asp:Label></h2>
                            </div>
                        </div>
                        <div class="row">
                            <asp:Repeater ID="rpttktbooked" runat="server">
                                <ItemTemplate>
                                    <div class="col-md-3 mt-2 text-center" style="border-right: 1px solid #e6e6e6; border-radius: 4px;">
                                        <%# Eval("tkt_count") %><br />
                                        <span style="font-size: 10px;"><%# Eval("service_type") %></span>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>

                    </div>
                </div>
            </div>
            <div class="col-md-4  stretch-card transparent">
                <div class="card card-dark-blue">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-lg-12 text-center">
                                <h4>Seat's Booked</h4>
                                <h2>
                                    <asp:Label ID="lbltotalseatbooked" runat="server" Text="0" CssClass="text-muted "></asp:Label></h2>
                            </div>
                        </div>
                        <div class="row">
                            <asp:Repeater ID="rptseatbooked" runat="server">
                                <ItemTemplate>
                                    <div class="col-md-3 mt-2 text-center" style="border-right: 1px solid #e6e6e6; border-radius: 4px;">
                                        <%# Eval("seat_count") %>/<%# Eval("total_seat") %><br />
                                        <span style="font-size: 10px;"><%# Eval("service_type") %></span>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>

                    </div>
                </div>
            </div>
            <div class="col-md-4  stretch-card transparent">
                <div class="card card-dark-blue">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-lg-12 text-center">
                                <h4>Revenue</h4>
                                <h2>
                                    <asp:Label ID="lbltotalRevenue" runat="server" Text="0" CssClass="text-muted "></asp:Label></h2>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4 mt-2 text-center" style="border-right: 1px solid #e6e6e6; border-radius: 4px;">
                                <asp:Label ID="lbtnfare" runat="server" Text="0"></asp:Label>
                            </div>
                            <div class="col-md-4 mt-2 text-center" style="border-right: 1px solid #e6e6e6; border-radius: 4px;">
                                <asp:Label ID="lbtntax" runat="server" Text="0"></asp:Label>
                            </div>
                            <div class="col-md-4 mt-2 text-center" style="border-right: 1px solid #e6e6e6; border-radius: 4px;">
                                <asp:Label ID="lbtnreservation" runat="server" Text="0"></asp:Label>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4  stretch-card transparent">
                <div class="card card-dark-blue">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-lg-12 text-center">
                                <h4>Web/App</h4>
                                <h2>
                                    <asp:Label ID="lbltotalwebapp" runat="server" Text="0" CssClass="text-muted "></asp:Label></h2>
                            </div>
                        </div>
                        <div class="row">
                            <asp:Repeater ID="rptwebapp" runat="server">
                                <ItemTemplate>
                                    <div class="col-md-3 mt-2 text-center" style="border-right: 1px solid #e6e6e6; border-radius: 4px;">
                                        <%# Eval("val_totalwebtkt") %>/<%# Eval("val_totalapptkt") %><br />
                                        <span style="font-size: 10px;"><%# Eval("service_type") %></span>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>

                    </div>
                </div>
            </div>
            <div class="col-md-4  stretch-card transparent">
                <div class="card card-dark-blue">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-lg-12 text-center">
                                <h4>Counter</h4>
                                <h2>
                                    <asp:Label ID="lblcntrtkt" runat="server" Text="0" CssClass="text-muted "></asp:Label></h2>
                            </div>
                        </div>
                        <div class="row">
                            <asp:Repeater ID="rptcounter" runat="server">
                                <ItemTemplate>
                                    <div class="col-md-3 mt-2 text-center" style="border-right: 1px solid #e6e6e6; border-radius: 4px;">
                                        <%# Eval("totalcntrtkt") %><br />
                                        <span style="font-size: 10px;"><%# Eval("service_type") %></span>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>

                    </div>
                </div>
            </div>
            <div class="col-md-4  stretch-card transparent">
                <div class="card card-dark-blue">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-lg-12 text-center">
                                <h4>Agent</h4>
                                <h2>
                                    <asp:Label ID="lblagnttkt" runat="server" Text="0" CssClass="text-muted "></asp:Label></h2>
                            </div>
                        </div>
                        <div class="row">
                            <asp:Repeater ID="rptagent" runat="server">
                                <ItemTemplate>
                                    <div class="col-md-3 mt-2 text-center" style="border-right: 1px solid #e6e6e6; border-radius: 4px;">
                                        <%# Eval("totalagnttkt") %><br />
                                        <span style="font-size: 10px;"><%# Eval("service_type") %></span>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>

                    </div>
                </div>
            </div>
        </div>
        <div class="row mt-2">
            <div class="col-lg-12">
                <i class="fa fa-list-ol"></i>
                <asp:Label ID="Label2" runat="server" Text="Current Booking" Style="font-size: 16pt; font-weight: bold; color: #074886;"></asp:Label>
                <hr />
            </div>
        </div>
        <div class="row">
            <div class="col-md-4  stretch-card transparent">
                <div class="card card-dark-blue">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-lg-12 text-center">
                                <h4>Ticket/Seat(s) Booked/Buses</h4>
                                <h2>
                                    <asp:Label ID="lblcurnttottktseats" runat="server" Text="0" CssClass="text-muted "></asp:Label></h2>
                            </div>
                        </div>
                        <div class="row">
                            <asp:Repeater ID="rptcurnttktseat" runat="server">
                                <ItemTemplate>
                                    <div class="col-md-3 mt-2 text-center" style="border-right: 1px solid #e6e6e6; border-radius: 4px;">
                                        <%# Eval("tkt_count") %>/<%# Eval("booked_seat") %>/<%# Eval("val_buses") %><br />
                                        <span style="font-size: 10px;"><%# Eval("service_type") %></span>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>

                    </div>
                </div>
            </div>
            <div class="col-md-4  stretch-card transparent">
                <div class="card card-dark-blue">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-lg-12 text-center">
                                <h4>Counter Booking<br />
                                    <span style="font-size: 10pt; color: green;">(Ticket/Revenue/Buses)</span></h4>
                                <h2>
                                    <asp:Label ID="lblcurntcntrtktrenue" runat="server" Text="0" CssClass="text-muted "></asp:Label></h2>
                            </div>
                        </div>
                        <div class="row">
                            <asp:Repeater ID="rptcurntcntrtktrenue" runat="server">
                                <ItemTemplate>
                                    <div class="col-md-3 mt-2 text-center" style="border-right: 1px solid #e6e6e6; border-radius: 4px;">
                                        <%# Eval("tkt_count") %>/<%# Eval("val_revenue") %>₹/<%# Eval("val_buses") %><br />
                                        <span style="font-size: 10px;"><%# Eval("service_type") %></span>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>

                    </div>
                </div>
            </div>
            <div class="col-md-4  stretch-card transparent">
                <div class="card card-dark-blue">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-lg-12 text-center">
                                <h4>Agent Booking<span style="font-size: 10pt; color: green;">(Ticket/Revenue/Commission/Buses)</span></h4>
                                <h2>
                                    <asp:Label ID="lblagntcntrtktrenuecomi" runat="server" Text="0" CssClass="text-muted "></asp:Label></h2>
                            </div>
                        </div>
                        <div class="row">
                            <asp:Repeater ID="rptagntcntrtktrenuecomi" runat="server">
                                <ItemTemplate>
                                    <div class="col-md-3 mt-2 text-center" style="border-right: 1px solid #e6e6e6; border-radius: 4px;">
                                        <%# Eval("tkt_count") %>/<%# Eval("val_revenue") %>₹/<%# Eval("val_commission") %>₹/<%# Eval("val_buses") %><br />
                                        <span style="font-size: 10px;"><%# Eval("service_type") %></span>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>

                    </div>
                </div>
            </div>
        </div>

        <div class="row mt-2">
            <div class="col-lg-12">
                <i class="fa fa-list-ol"></i>
                <asp:Label ID="Label3" runat="server" Text="Bus Passes" Style="font-size: 16pt; font-weight: bold; color: #074886;"></asp:Label>
                <hr />
            </div>
        </div>
        <div class="row">
            <div class="col-md-4  stretch-card transparent">
                <div class="card card-dark-blue">
                    <div class="card-body">
                         <div class="row">
                             
                            <div class="col-lg-12 mt-2 text-center">
                                 <h4>Applied</h4>
                                
                         <h2>
                                    <asp:Label  runat="server" Text="Coming Soon" CssClass="text-muted "></asp:Label></h2>
                                </div>
                                </div>
                  <%--      <div class="row">
                            <div class="col-lg-12 text-center">
                                <h4>Applied</h4>
                                <h2>
                                    <asp:Label ID="lbltotalApplied" runat="server" Text="0" CssClass="text-muted "></asp:Label></h2>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6 mt-2 text-center" style="border-right: 1px solid #e6e6e6; border-radius: 4px;">
                                <asp:Label ID="lbtnAppliedMST" runat="server" Text="0"></asp:Label>
                            </div>
                            <div class="col-md-6 mt-2 text-center" style="border-right: 1px solid #e6e6e6; border-radius: 4px;">
                                <asp:Label ID="lbtnAppliedohter" runat="server" Text="0"></asp:Label>
                            </div>
                        </div>--%>

                    </div>
                </div>
            </div>
            <div class="col-md-4  stretch-card transparent">
                <div class="card card-dark-blue">
                    <div class="card-body">
                         <div class="row">
                            <div class="col-lg-12 mt-2 text-center">
                                <h4>Issued</h4>
                         <h2>
                                    <asp:Label  runat="server" Text="Coming Soon" CssClass="text-muted "></asp:Label></h2>
                                </div>
                                </div>
                  <%--      <div class="row">
                            <div class="col-lg-12 text-center">
                                <h4>Issued</h4>
                                <h2>
                                    <asp:Label ID="lbltotalIssued" runat="server" Text="0" CssClass="text-muted "></asp:Label></h2>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6 mt-2 text-center" style="border-right: 1px solid #e6e6e6; border-radius: 4px;">
                                <asp:Label ID="lbtnIssuedMST" runat="server" Text="0"></asp:Label>
                            </div>
                            <div class="col-md-6 mt-2 text-center" style="border-right: 1px solid #e6e6e6; border-radius: 4px;">
                                <asp:Label ID="lbtnIssuedOther" runat="server" Text="0"></asp:Label>
                            </div>
                        </div>--%>

                    </div>
                </div>
            </div>
            <div class="col-md-4  stretch-card transparent">
                <div class="card card-dark-blue">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-lg-12 mt-2 text-center">
                                <h4>Revenue</h4>
                         <h2>
                                    <asp:Label  runat="server" Text="Coming Soon" CssClass="text-muted "></asp:Label></h2>
                                </div>
                                </div>
                        <%--<div class="row">
                            <div class="col-lg-12 text-center">
                                <h4>Revenue</h4>
                                <h2>
                                    <asp:Label ID="lbltotalrevenueamt" runat="server" Text="0" CssClass="text-muted "></asp:Label></h2>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6 mt-2 text-center" style="border-right: 1px solid #e6e6e6; border-radius: 4px;">
                                <asp:Label ID="lbtnrevenueamtMST" runat="server" Text="0"></asp:Label>
                            </div>
                            <div class="col-md-6 mt-2 text-center" style="border-right: 1px solid #e6e6e6; border-radius: 4px;">
                                <asp:Label ID="lbtnrevenueamtOther" runat="server" Text="0"></asp:Label>
                            </div>
                        </div>--%>

                    </div>
                </div>
            </div>
        </div>

    </div>
</asp:Content>





