<%@ Page Title="" Language="C#" MasterPageFile="~/traveller/trvlrMaster.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeFile="dashboard.aspx.cs" Inherits="traveller_dashboard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../assets/js/jquery-n.js"></script>
    <script src="../assets/js/jquery-ui.js"></script>
    <link href="../assets/css/jquery-ui.css" rel="stylesheet" />

    <script type="text/javascript">
        $(document).ready(function () {
            $("[id$=tbFrom]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "dashboard.aspx/searchStations",
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
            $("[id$=tbTo]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "dashboard.aspx/searchStations",
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
            var endD = new Date(new Date().setDate(todayDate +  parseInt(endDate-1)));
            var currDate = new Date();
            $('[id*=tbDate]').datepicker({
                startDate: "dateToday",
                endDate: endD,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            });

            
        });
    </script>
    <style>
        input[type="checkbox"] {
            width: 22px; /*Desired width*/
            height: 18px; /*Desired height*/
        }

        input[type="radio"] {
            width: 22px; /*Desired width*/
            height: 18px; /*Desired height*/
        }

        .input-group-prepend .btn, .input-group-append .btn {
            position: relative;
            z-index: 0;
        }
    </style>
    <style type="text/css">
        .ratingEmpty {
            background-image: url(ratingStars/staroutline.png);
            width: 25px;
            height: 25px;
        }

        .ratingFilled {
            background-image: url(ratingStars/starFilled.png);
            width: 25px;
            height: 25px;
        }

        .ratingSaved {
            background-image: url(ratingStars/staroutline.png);
            width: 25px;
            height: 25px;
        }
    </style>
    <link href="../css/paging.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <asp:HiddenField ID="hdmaxdate" runat="server"  />
     <asp:HiddenField ID="hidtoken" runat="server"  />
    <div class="header">
        <div class="container-fluid">
            <div class="header-body pt-4 pb-3">
                <div class="row">
                    <div class="col-xl-7">
                        <div class="row">
                            <div class="col-xl-3 pr-2">
                                <div class="card">
                                    <div class="card-header bg-transparent1 p-3">
                                        <div class="row text-center">
                                            <div class="col-lg-12">
                                                <div class="col-auto text-center">
                                                    <i class="fa fa-user fa-2x text-primary"></i>
                                                    <h5 class="h3 text-primary mb-0">
                                                        <asp:Label ID="lblUser" runat="server"></asp:Label></h5>
                                                </div>
                                            </div>
                                            <div class="col-lg-12">
                                                <div class=" mb-0 text-xs1 text-gray1">
                                                    <asp:Label ID="lblLoginTime" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xl-9 pr-2">
                                <div class="card">
                                    <div class="card-header bg-transparent1 p-1">
                                        <div class="row ">
                                            <div class="col-xl-4 pr-2 text-center">
                                                <div class="card mb-0">
                                                    <div class="card-body p-1" style="background: #f3fff1;">
                                                        <div class="row">
                                                            <div class="col-lg-12">
                                                                <div class="col-auto text-center border-right" style="border-color: #f7f7f7 !important;">
                                                                    <i class="fa fa-wallet fa-2x text-primary"></i>
                                                                    <h5 class="h3 text-primary mb-0">Wallet</h5>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-12">
                                                                <div class=" mb-0 text-xs1 text-gray1">Total Balance</div>
                                                                <h5 class=" mb-1 mt-1">
                                                                    <i class="fa fa-rupee-sign"></i>
                                                                    <asp:Label ID="lblWalletBalance" runat="server"></asp:Label>
                                                                </h5>
                                                                <div class="text-success text-sm ">
                                                                    <a href="wallet.aspx">Recharge/View Detail</a>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xl-4 pr-2 text-center">
                                                <div class="card mb-0">
                                                    <div class="card-body p-1" style="background: #f3fff1;">
                                                        <div class="row">
                                                            <div class="col-lg-12">
                                                                <div class="col-auto text-center border-right" style="border-color: #f7f7f7 !important;">
                                                                    <i class="fa fa-address-book fa-2x text-primary"></i>
                                                                    <h5 class="h3 text-primary mb-0">Grievance</h5>

                                                                </div>
                                                            </div>
                                                            <div class="col-lg-12">
                                                                <div class=" mb-0 text-xs1 text-gray1">
                                                                    Register your Complaint
                                                                </div>
                                                                <div class="text-success text-sm">
                                                                    <a href="grievance.aspx">Register/View Detail</a>
                                                                </div>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xl-4 pr-2 text-center">
                                                <div class="card mb-0">
                                                    <div class="card-body p-1" style="background: #f3fff1;">
                                                        <div class="row">
                                                            <div class="col-lg-12">
                                                                <div class="col-auto text-center border-right" style="border-color: #f7f7f7 !important;">
                                                                    <i class="fa fa-money-bill fa-2x text-primary"></i>
                                                                    <h5 class="h3 text-primary mb-0">Cancellation/Refund</h5>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-12">
                                                                <div class=" mb-0 text-xs1 text-gray1">Total Balance</div>
                                                                <div class="text-success text-sm">
                                                                    <asp:LinkButton ID="lbtnCancelTkt" runat="server" OnClick="lbtnCancelTkt_Click">Cancel/Refund</asp:LinkButton>                                                                  
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
                    <div class="col-xl-5">
                        <div class="row">
                            <div class="col-xl-7">
                                <div class="card mb-0">
                                    <div class="card-header bg-transparent1 py-1">
                                        <div class="row align-items-center">
                                            <div class="col">
                                                <h5 class="h3 text-primary mb-0">Offer</h5>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="card-body p-1 mb-0">

                                        <div id="carouselExampleIndicators" class="carousel slide" data-ride="carousel">
                                            <%-- <ol class="carousel-indicators ">
                                                <li data-target="#carouselExampleIndicators" data-slide-to="0" class="bg-primary"></li>
                                                <li data-target="#carouselExampleIndicators" data-slide-to="1" class="bg-primary active"></li>
                                            </ol>--%>
                                            <div class="carousel-inner ">

                                              <%--  <div class="carousel-item active">
                                                    <div class="card card-stats  bg-gradient-info">
                                                        <!-- Card body -->
                                                        <div class="card-body mb-0 p-2">
                                                            <div class="row">
                                                                <div class="col ">
                                                                    <h5 class="card-title text-uppercase text-white mb-0">Use Code - NEWYEAR01</h5>
                                                                    <span class="h2 text-white font-weight-bold mb-0">20 % OFF</span>
                                                                    <p class="mt-1 mb-0 text-sm text-white">
                                                                        <span id="ContentPlaceHolder1_Label1">New Year celebration with </span>
                                                                    </p>
                                                                </div>
                                                                <div class="col-auto text-right">
                                                                    <i class="ni ni-basket ni-3x text-white"></i>
                                                                    <p class="mt-0 mb-0 text-sm">
                                                                        <a id="ContentPlaceHolder1_LinkButton1" class="text-success" href="javascript:__doPostBack('ctl00$ContentPlaceHolder1$LinkButton1','')">View Detail</a>
                                                                    </p>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>--%>
                                                

                                                <asp:ListView ID="lvOffers" runat="server" OnItemDataBound="lvOffers_ItemDataBound" OnItemCommand="lvOffers_ItemCommand"
                                                    DataKeyNames="couponid, couponcode, coupontitle, discountdescription, discounttype, discounton, discountamount, maxdiscount_amount, validfrom_date, validto_date">
                                                    <ItemTemplate>

                                                        <asp:Panel id="pnlInnerOffer" runat="server" CssClass="carousel-item">
                                                            <div class="card card-stats shadow-none">
                                                                <!-- Card body -->
                                                                <div class="card-body mb-0 p-2">
                                                                    <div class="row">
                                                                        <div class="col-auto">
                                                                            <asp:Image ID="imgWeb" runat="server" CssClass="card-img" Style="height:75px; width:75px;" />
                                                                            </div>
                                                                        <div class="col">
                                                                            <h5 class="card-title text-uppercase text-dark mb-0">Use Code - <%# Eval("couponcode")%></h5>
                                                                            <%-- <span class="h2 text-white font-weight-bold mb-0">50 % OFF</span>--%>
                                                                            <p class="mt-1 mb-0 text-md text-dark">
                                                                                <span id="ContentPlaceHolder1_lbl1"><%# Eval("coupontitle")%></span>
                                                                            </p>
                                                                            <asp:LinkButton ID="lbtnViewDetails" runat="server" CssClass="btn p-0 float-right" CommandName="VIEWDETAILS">View Detail</asp:LinkButton>
                                                                        </div>
                                                                        
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </asp:Panel>
                                                        


                                                        <%--                                    <div class="col-lg-4 col-md-12 col-sm-12">
                                                            <div class="card shadow">
                                                                
                                                                <div class="card-footer p-2">
                                                                    <div class="row">
                                                                        <div class="col">
                                                                            <h3 class="mb-0 text-blue"><%# Eval("coupontitle")%></h3>
                                                                            <h5 class="text-black-50 mb-0">Code : <%# Eval("couponcode")%></h5>
                                                                        </div>
                                                                        <div class="col-auto text-right">
                                                                            
                                                                        </div>
                                                                    </div>

                                                                </div>
                                                            </div>
                                                        </div>--%>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </div>
                                        </div>



                                        <asp:Panel ID="pnlNoOffer" runat="server" Width="100%" Visible="false">
                                            <div class="col-lg-12 col-md-12 col-sm-12 text-center">
                                                <h1 class="text-muted">Offer will be available Soon.</h1>
                                            </div>
                                        </asp:Panel>





                                    </div>
                                </div>
                            </div>
                            <div class="col-xl-5">
                                <div class="card ">
                                    <div class="card-body p-1">
                                        <div class="col-lg-12 text-center w-100 ">
                                            <i class="fa fa-ad fa-5x mt-3"></i>
                                            <h2>Advertisement Space</h2>
                                            <%--                                            <img src="../assets/img/add.png">--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-xl-2">
                        <div class="card">
                            <div class="card-body">
                                <div class="col-lg-12 text-center ">
                                    <marquee direction="left" width="100%" scrollamount="2">
                                    <span class="h2 font-weight-bold mb-0 text-warning">Thanks for being here Thanks for being here Thanks for being here Thanks for being here</span>
                                </marquee>
                                </div>
                            </div>
                        </div>
                    </div>
 <div class="col-xl-2">
                        <asp:LinkButton href="../Auth/UserManuals/Traveller/Help Document for Traveller.pdf" target="_blank" runat="server" CssClass="btn btn-success btn-sm float-right ml-1" ToolTip="Click here for manual."><i class="fa fa-download"></i></asp:LinkButton>
                        <asp:Label runat="server" Text="Download Manual" CssClass="float-right"></asp:Label>

                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="container-fluid">
        <div class="row">
            <div class="col-xl-6">
                <div class="card">
                    <div class="card-header bg-transparent py-1">
                        <div class="row align-items-center">
                            <div class="col">
                                <h5 class="h3 text-primary mb-0">Tickets</h5>
                            </div>
                            <div class="col-auto text-right">
                                <h3 class="mb-0 pb-2">Confirm</h3>
                            </div>
                            <div class="col-auto text-right">
                                <label class="custom-toggle">
                                    <asp:CheckBox ID="cbConfirmTickets" AutoPostBack="true" runat="server" OnCheckedChanged="cbConfirmTickets_CheckedChanged" />
                                    <span class="custom-toggle-slider rounded-circle" data-label-off="No" data-label-on="Yes"></span>
                                </label>
                            </div>
                        </div>
                    </div>

                    <div class="card-body" style="min-height: 50vh !important">
                        <asp:GridView ID="gvTickets" runat="server" GridLines="None" ShowHeader="false" AutoGenerateColumns="false" Width="100%"
                            DataKeyNames="_ticketno,_tripcode, service_name,service_type_name,bookingdatetime,journeydate,fromstn_name,tostn_name,amount_fare,amount_tax,amount_concession,amount_onl_reservation,amount_total,total_seats_booked,traveller_mobile_no,traveller_email_id,boardingstn_name,depot_servicecode,trip_time,booked_by,total_distance,tripend_time,current_status,for_cancel"
                            OnRowCommand="gvTickets_RowCommand" OnPageIndexChanging="gvTickets_PageIndexChanging" AllowPaging="true" PageSize="5" OnRowDataBound="gvTickets_RowDataBound">
                            <PagerStyle CssClass="pagination-ys" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <div class="row px-2 shadow-lg--hover border-bottom pb-2 mb-2">
                                            <div class="col">
                                                <span class="h3 font-weight-bold mb-0"><%# Eval("_ticketno") %><%# Eval("current_status").ToString() == "A" ? "<i class='fa fa-check-circle ml-2 text-success'></i>" : "<i class='fa fa-times-circle ml-2 text-danger'></i>"  %> </span>
                                                <h5 class="card-title text-xs text-uppercase text-muted mb-0"><%# Eval("fromstn_name") %> - <%# Eval("tostn_name") %></h5>
                                                <%-- <p class="mb-0 text-xs">Ticket Amount ₹ <%# Eval("Amount") %> </p>--%>
                                            </div>
                                            <div class="col text-left">
                                                <p class="mb-0 text-xs">Booking <%# Eval("bookingdatetime") %> </p>
                                                <p class="mb-0 text-xs">Journey <%# Eval("journeydate") %>&nbsp;<%# Eval("trip_time") %></p>
                                            </div>
                                            <div class="col-auto text-right ">
                                                <asp:LinkButton ID="lbtnCancel" runat="server" CssClass="btn btn-icon btn-danger btn-sm"
                                                    CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="CANCELLATION" data-toggle="tooltip" data-placement="bottom" title="Cancel Ticket">
                                                    C
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="lbtnView" runat="server" CssClass="btn btn-icon btn-primary btn-sm"
                                                    CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="VIEWDETAIL" data-toggle="tooltip" data-placement="bottom" title="View Details and Actions">
                                                    <i class="fa fa-eye"></i>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:Panel ID="pnlNoTickets" runat="server">
                            <div class="row">
                                <div class="col-lg-12 text-center mt-4">
                                    <i class="ni ni-bus-front-12 ni-5x text-muted"></i>
                                </div>
                                <div class="col-lg-12 text-center mt-4">
                                    <span class="h2 font-weight-bold mb-0">Thanks for being here</span>
                                    <h5 class="card-title text-uppercase text-muted mb-0">
                                        <asp:Label ID="lblNoTicketsMsg" runat="server"></asp:Label>
                                    </h5>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
            <div class="col-xl-6">
                <div class="card">
                    <div class="card-header bg-transparent py-1">
                        <div class="row align-items-center">
                            <div class="col">
                                <h5 class="h3 text-primary mb-0">Booking</h5>
                            </div>
                            <div class="col">
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row align-items-center px-2 pb-3">
                            <div class="col-xl-6 col-md-6">
                                <label>From</label>
                                <asp:TextBox ID="tbFrom" runat="server" MaxLength="50" CssClass="form-control text-uppercase px-1" placeholder="City Name" aria-describedby="basic-addon1" AutoComplete="off"></asp:TextBox>
                            </div>
                            <div class="col-xl-6 col-md-6">
                                <label>To</label>
                                <asp:TextBox ID="tbTo" runat="server" MaxLength="50" CssClass="form-control text-uppercase px-1" placeholder="City Name" aria-describedby="basic-addon2" AutoComplete="off"></asp:TextBox>
                            </div>
                            <div class="col-xl-6 col-md-6 mt-2">
                                <label>Date</label>
                                <asp:TextBox ID="tbDate" runat="server" MaxLength="10" CssClass="form-control px-1" placeholder="DD/MM/YYYY" AutoComplete="off"></asp:TextBox>
                            </div>
                             <div class="col-xl-6 col-md-6 mt-2">
                                <label>Services</label><span class="text-muted text-sm"> (Optional)</span>
                                 <asp:DropDownList runat="server" ID="ddlservices" CssClass="form-control"></asp:DropDownList>
                             </div>
                            <div class="col-xl-12 col-md-12 mt-3">
                               
                                <asp:LinkButton ID="lbtnSearchServices" runat="server" CssClass="btn btn-warning w-100" OnClick="lbtnSearchServices_Click">
                                            Show Buses
                                </asp:LinkButton>
                            </div>
                        </div>
                     
                        <asp:Panel ID="pnlServices" runat="server" Visible="false">
                            <div class="row align-items-center py-1">
                                <div class="col">
                                    <p class="mb-0 text-xs">Search result for</p>
                                    <h5 class="h5 text-black-50 mb-0">
                                        <asp:Label ID="lblSearchStations" runat="server"></asp:Label></h5>
                                </div>
                                <div class="col-auto text-right">
                                    <h5 class="h5 text-black-50 mb-0">
                                        <asp:Label ID="lblSearchTotalRecord" runat="server"></asp:Label></h5>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:GridView ID="gvService" runat="server" GridLines="None" ShowHeader="false" AutoGenerateColumns="false" Width="100%" OnRowDataBound="gvService_RowDataBound" OnRowCommand="gvService_RowCommand"
                                        DataKeyNames="openclose,dsvcid,servicename,strpid,frstonid,tostonid,depttime,arrtime,tripdirection,srtpid,servicetypename,layout,totalavailablseats,totalseat,routeid,routename,s_code,distance,fare,midstations,from_station_name,to_station_name">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <div class="card p-2 border ">
                                                        <div class="row px-3 pt-1">
                                                            <div class="col">
                                                                <span class="h3 font-weight-bold mb-0"><%# Eval("dsvcid") %><%# Eval("tripdirection") %><%# Eval("strpid") %> | <%# Eval("servicetypename") %> </span>
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
                                                                <span class="h5 font-weight-bold mb-0"><span class="h6 text-muted">Arrival </span><%# Eval("arrtime") %>- <%# Eval("openclose") %></span>
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
                                                                    CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                                      CommandName="BOOKTICKET" >
                                                    <i class="fa fa-check" ></i> BOOK
                                                                </asp:LinkButton>
                                                                   <asp:LinkButton ID="lbtnclose" runat="server" CssClass="btn btn-icon btn-secondary btn-sm mb-2" Enabled="false" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                                 CommandName="BookingClosed"> Closed</asp:LinkButton>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="pnlNoService" runat="server" Visible="false">
                            <div class="card">
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-lg-12 text-center mt-2">
                                            <i class="ni ni-bus-front-12 ni-5x text-muted"></i>
                                        </div>
                                        <div class="col-lg-12 text-center mt-2">
                                            <span class="h2 font-weight-bold mb-0">
                                                <asp:Label ID="lblNoServiceMsg" runat="server"></asp:Label></span>
                                            <h5 class="card-title text-uppercase text-muted mb-0"></h5>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
                <div class="row pl-2 pr-2">
                    <div class="col-xl-4 pr-2">
                        <div class="card">
                            <div class="card-body">
                                <div class="row px-2">
                                    <div class="col-lg-12">
                                        <h4 class="text-primary mb-1"><i class="fa fa-map-marker-alt text-gray"></i>Track My Bus</h4>
                                    </div>
                                    <div class="col-lg-12">
                                        <asp:TextBox ID="tbs" runat="server" CssClass="form-control form-control-sm" MaxLength="20" placeholder="Ticket Number"></asp:TextBox>
                                        <asp:Button ID="btn" runat="server" OnClick="btn_Click" CssClass="btn btn-info btn-sm mt-2 float-right" Text="Click Here" />
                                    </div>
                                </div>
                            </div>
                        </div>
<div class="card">
                            <div class="card-body">
                                <div class="row px-2">
                                    <div class="col-lg-12">
                                        <h4 class="text-primary mb-1"><i class="fa fa-user"></i>Traveller Rating</h4>

                                        <asp:LinkButton runat="server" CssClass="btn btn-success btn-sm mt-2 float-right" OnClick="Unnamed_Click">Rate Us</asp:LinkButton>

                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-xl-4 pr-2">
                        <div class="card">
                            <div class="card-header bg-transparent1">
                                <div class="row px-2">
                                    <div class="col-lg-12">
                                        <h4 class="text-primary mb-1">Frequently Searches</h4>
                                    </div>
                                    <div class="col-lg-12">
                                        <asp:GridView runat="server" ID="gvroutes" AutoGenerateColumns="false" ShowHeader="false" GridLines="none">
                                            <Columns>
                                              <%--  <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <div class="col-xl-12 text-sm text-default font-weight-300">
                                                            <%# Container.DataItemIndex + 1 %> -
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <div class="col-xl-12 mt-1">
                                                            <span class="text-sm text-default mb-0 font-weight-300"><%# Eval("route_name")%>
                                                            </span>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>

                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-xl-4 pr-2">
                        <div class="card">
                            <div class="card-header bg-transparent1">
                                <div class="row px-2">
                                    <div class="col-lg-12">
                                        <h4 class="text-primary mb-1">Payment Partners</h4>
                                    </div>
                                    <div class="col-lg-12">
                                        <asp:GridView runat="server" ID="gvPaymentGateway" AutoGenerateColumns="false" ShowHeader="false" GridLines="none">
                                            <Columns>
                                               <%-- <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <div class="col-xl-12 text-sm text-default font-weight-300">
                                                            <%# Container.DataItemIndex + 1 %> -
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <div class="col-xl-12">
                                                            <span class="text-lg text-default mb-0 font-weight-300 "><%# Eval("gateway_name")%>
                                                             <img src="../Dbimg/PG/<%# Eval("gateway_name")%>_W.png" style="height: 40px; width: 50%;" /></span>

                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>

                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <cc1:ModalPopupExtender ID="mpOfferDetail" runat="server" CancelControlID="btnClosempOfferDetail"
                TargetControlID="btnOpenmpOfferDetail" PopupControlID="pnlOfferDetail" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>

            <asp:Panel ID="pnlOfferDetail" Style="display: none;" runat="server">
                <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <div class="row w-100">
                                <div class="col">
                                    <h3 class="modal-title" id="modal-title-default">Offer Detail</h3>
                                </div>
                                <div class="col-auto text-right">
                                    <button type="button" id="btnClosempOfferDetail" class="btn btn-outline-danger btn-sm btn-icon-only" data-dismiss="modal"><i class="fa fa-times"></i></button>
                                </div>
                            </div>

                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-lg-6">
                                    <asp:Image ID="img" runat="server" CssClass="card-img" Style="width: 100%;" />
                                </div>
                                <div class="col-lg-6">
                                    <h2 class="mb-0">
                                        <asp:Label ID="lblTitle" runat="server"></asp:Label>
                                    </h2>
                                    <h4>
                                        <asp:Label ID="lblDescription" runat="server"></asp:Label>
                                    </h4>
                                    <h4>
                                        <span class="text-muted text-xs">Coupon Code : </span>
                                        <asp:Label ID="lblCode" runat="server"></asp:Label>
                                    </h4>
                                    <h4>
                                        <span class="text-muted text-xs">Discount : </span>
                                        <asp:Label ID="lblDiscountAmount" runat="server"></asp:Label>
                                        <asp:Label ID="lblDiscountOn" runat="server"></asp:Label>
                                        (Max
                                        <asp:Label ID="lblDiscountAmountMax" runat="server"></asp:Label>)
                                    </h4>
                                    <h4>
                                        <span class="text-muted text-xs">Valid Upto : </span>
                                        <asp:Label ID="lblValidUpto" runat="server"></asp:Label>
                                    </h4>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <div style="visibility: hidden;">
                <asp:Button ID="btnOpenmpOfferDetail" runat="server" />
            </div>
        </div>
    </div>



</asp:Content>

