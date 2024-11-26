<%@ Page Language="C#" AutoEventWireup="true" CodeFile="homee.aspx.cs" Inherits="homee" MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta name="description" content="Starbus online ticket booking" />
    <meta name="author" content="starbus" />
    <title>StarBus* 4.0</title>
    <!-- Favicon -->
    <link rel="icon" href="Logo/Favicon.png" type="image/png" />

    <!-- CSS FILES -->
    <link rel="preconnect" href="https://fonts.googleapis.com" />
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin />
    <link href="homeAssets/css/fonts.css" rel="stylesheet" />

    <link href="homeAssets/css/bootstrap.min.css" rel="stylesheet" />
    <link href="homeAssets/css/bootstrap-icons.css" rel="stylesheet" />
    <link href="homeAssets/css/templatemo-topic-listing.css" rel="stylesheet" />
    <link rel="stylesheet" href="assets/vendor/@fortawesome/fontawesome-free/css/all.min.css" type="text/css" />
    <link href="assets/vendor/bootstrap-datepicker/dist/css/bootstrap-datepicker.min.css" rel="stylesheet" />

    <script src="assets/js/jquery-n.js"></script>
    <link href="assets/css/jquery-ui.css" rel="stylesheet" />
    <script type="text/javascript">
        $(document).ready(function () {
            

            $('#polcy').click(function () {
                $('#CPModal1').modal('show');
            });
            $('#closepolcy').click(function () {
                $('#CPModal1').modal('hide');
            });
            

            $("[id$=tbFrom]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "Homee.aspx/searchStations",
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
                        url: "Homee.aspx/searchStations",
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
            $('[id*=tbDate]').datepicker({
                startDate: "dateToday",
                endDate: endD,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            });

            DetectAndServe();

        });
    </script>
    <script>
        function DetectAndServe() {
            let os = getMobileOperatingSystem();
            if (os == "Android") {
                document.getElementById('lbtndownload').style.display = "";
                document.getElementById('a_playstore').style.display = "none";
                document.getElementById('a_appstore').style.display = "none";
                $find("mpFirst").show();
            } else if (os == "iOS") {
                document.getElementById('a_playstore').style.display = "none";
                document.getElementById('a_appstore').style.display = "none";
                $find("mpFirst").hide();
            }
        }
    </script>
    <script>
        function getMobileOperatingSystem() {
            var userAgent = navigator.userAgent || navigator.vendor || window.opera;

            // Windows Phone must come first because its UA also contains "Android"
            if (/windows phone/i.test(userAgent)) {
                return "Windows Phone";
            }

            if (/android/i.test(userAgent)) {
                return "Android";
            }

            // iOS detection from: http://stackoverflow.com/a/9039885/177710
            if (/iPad|iPhone|iPod/.test(userAgent) && !window.MSStream) {
                return "iOS";
            }

            return "unknown";
        }


    


    </script>
    <style>
        .modalBackground {
            background-color: black;
            opacity: 0.6;
        }


    </style>
</head>
<body id="top">
    <form id="form1" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:HiddenField ID="hdmaxdate" runat="server" />
        <main>
            <nav class="navbar navbar-expand-lg" style="background: #021952;">
                <div class="container">
                    <a class="navbar-brand" href="#" style="line-height: 10px; color: white;">
                        <table>
                            <tr>
                                <td>
                                    <asp:Image runat="server" ID="ImgDepartmentLogo" Height="40px" ImageUrl="Logo/../Logo/DeptLogo.png" />
                                </td>
                                <td style="padding-left: 5px;">
                                    <asp:Label runat="server" ID="lblDeptName" CssClass="deptName" Text="Arunachal Pradesh State Transport Services" Font-Size="15px"> </asp:Label>
                                    <br />
                                    <span style="font-size: 12px">Version
                                        <asp:Label runat="server" ID="lblversion" Text="4.0"></asp:Label></span>
                                </td>
                            </tr>
                        </table>
                    </a>

                    <div class="d-lg-none ms-auto me-4">
                        <a href="#top" class="navbar-icon bi-person smoothscroll"></a>
                    </div>

                    <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                        <span class="navbar-toggler-icon"></span>
                    </button>

                    <div class="collapse navbar-collapse" id="navbarNav">
                        <ul class="navbar-nav ms-lg-5 me-lg-auto">
                            <li class="nav-item">
                                <a class="nav-link click-scroll" href="#section_1">Home</a>
                            </li>

                            <li class="nav-item dropdown">
                                <a class="nav-link dropdown-toggle" href="#" id="navbarAboutUs" role="button" data-bs-toggle="dropdown" aria-expanded="false">About Us</a>
                                <ul class="dropdown-menu dropdown-menu-light" aria-labelledby="navbarAboutUs">

                                    <li><a class="dropdown-item" href="History.aspx">History</a></li>
                                    <li><a class="dropdown-item" href="Routes.aspx">Routes</a></li>
                                    <li><a class="dropdown-item" href="Rti.aspx">RTI</a></li>
                                    <li><a class="dropdown-item" href="Pios.aspx">PIOS</a></li>
                                    <li><a class="dropdown-item" href="#dvmentor" runat="server" id="hrfMentors">Our Mentors</a></li>
                                    <li><a class="dropdown-item" href="Notification.aspx">Notifications</a></li>
                                    <li><a class="dropdown-item" id="polcy" style="cursor: pointer;" >Policies</a></li>

                                    <hr />
                                    <li><a class="dropdown-item" href="Agents.aspx" title="Click here to authorized agents">
                                        <asp:Label ID="lblMenuAgents" runat="server" Text="Agents" meta:resourcekey="lblMenuAgentsResource1"></asp:Label></a></li>
                                    <li><a class="dropdown-item" href="BookingCounters.aspx" title="Click here to Booking Counters">
                                        <asp:Label ID="lblBookingCounters" runat="server" Text="Booking Counters" meta:resourcekey="lblBookingCountersResource1"></asp:Label></a></li>
                                    <li><a class="dropdown-item" href="CommonServiceCentres.aspx" title="Click here to Common Service Centers">
                                        <asp:Label ID="lblCommonServiceCentres" runat="server" Text="Common Service Centers" meta:resourcekey="lblCommonServiceCentresResource1"></asp:Label></a></li>
                                    <hr />
                                    <li><a class="dropdown-item" href="Tenders.aspx" title="Click here to view published tenders">
                                        <asp:Label ID="lblMenuTender" runat="server" Text="Tenders" meta:resourcekey="lblMenuTenderResource1"></asp:Label></a></li>
                                    <hr />
                                    <li><a class="dropdown-item" href="#divbusservices" runat="server" id="hrfBusServices">Our Bus Services</a></li>

                                    <li><a class="dropdown-item" href="CourierBooking.aspx" title="Click here for Parcel Booking">
                                        <asp:Label ID="lblMenuCourierBooking" runat="server" Text="Parcel Booking" meta:resourcekey="lblMenuCourierBookingResource1"></asp:Label></a></li>

                                </ul>
                            </li>

                            <li class="nav-item dropdown">
                                <a class="nav-link dropdown-toggle" href="#" id="navbarAnyQuery" role="button" data-bs-toggle="dropdown" aria-expanded="false">Any Query</a>
                                <ul class="dropdown-menu dropdown-menu-light" aria-labelledby="navbarAnyQuery">


                                    <li><a class="dropdown-item" href="timetable.aspx">Timetable</a></li>
                                    <li><a class="dropdown-item" href="Helpdesk.aspx">HelpDesk</a></li>
                                    <li><a class="dropdown-item" href="Contact.aspx">Contact Us</a></li>
                                    <li><a class="dropdown-item" href="Grievance.aspx">Grievance</a></li>
                                    <li><a class="dropdown-item" href="Userguide.aspx">User Guide</a></li>
                                    <li><a class="dropdown-item" href="#divFaq">FAQ</a></li>


                                </ul>
                            </li>

                            <li class="nav-item dropdown">
                                <a class="nav-link dropdown-toggle" href="#" id="navbarOnlineServices" role="button" data-bs-toggle="dropdown" aria-expanded="false">Online Services</a>
                                <ul class="dropdown-menu dropdown-menu-light" aria-labelledby="navbarLightDropdownMenuLink">
                                    <li><a class="dropdown-item" href="traveller/trvlLogin.aspx">Traveller Login</a></li>
                                    <li><a class="dropdown-item" href="BookTicket.aspx">Book Ticket</a></li>
                                    <li><a class="dropdown-item" href="CancelBusTicket.aspx">Cancel Ticket</a></li>
                                    <li><a class="dropdown-item" href="DownloadE_ticket.aspx">Download e-Ticket</a></li>
                                    <hr />
                                    <li><a class="dropdown-item" href="BusPass.aspx">Appy For Pass</a></li>
                                    <li><a class="dropdown-item" href="BusPass.aspx">Download e-Pass</a></li>
                                    <hr />
                                    <li><a class="dropdown-item" href="AgentRegistrationForm.aspx">Become Agent</a></li>
                                    <li><a class="dropdown-item" href="OnlAgTrackApplication.aspx">Track Application Status</a></li>






                                </ul>
                            </li>
                        </ul>

                        <div class="d-none d-lg-block dropdown">
                            <a href="#" class="navbar-icon bi-person nav-link" id="navbarusers" role="button" data-bs-toggle="dropdown" aria-expanded="false"></a>
                            <ul class="dropdown-menu dropdown-menu-light" aria-labelledby="navbarusers">
                                <li><a class="dropdown-item" href="traveller/trvlLogin.aspx">Traveller Login</a></li>
                                <li><a class="dropdown-item" href="Login.aspx">Department Login</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
            </nav>
            <section class="hero-section d-flex justify-content-center align-items-center" id="section_1">
                <div class="container">
                    <div class="row mb-3">

                        <div class="col-lg-8 col-12 mx-auto">
                            <h2 class="text-center" style="color: var(--secondary-color);">Search & <span style="color: #f08701;">Book</span></h2>
                            <h6 class="text-white text-center">Search your bus for online ticket booking</h6>


                            <div class="input-group input-group-lg p-5 p-lg-3 p-md-3">
                                <div class="row">
                                    <div class="col-md form-floating">
                                        <asp:TextBox ID="tbFrom" runat="server" MaxLength="50" CssClass="form-control pl-0 text-uppercase" type="Search" placeholder="From" AutoComplete="off"
                                            Style="border: 0px solid #ddd9d9; border-right: 1px solid #ddd9d9;" />
                                        <label for="floatingInput">From</label>
                                    </div>

                                    <div class="col-md form-floating">
                                        <asp:TextBox ID="tbTo" runat="server" MaxLength="50" CssClass="form-control pl-0 text-uppercase" type="Search" placeholder="To" AutoComplete="off"
                                            Style="border: 0px solid #ddd9d9; border-right: 1px solid #ddd9d9;" />
                                        <label for="floatingInput">To</label>
                                    </div>

                                    <div class="col-md form-floating">
                                        <asp:TextBox ID="tbDate" runat="server" MaxLength="10" CssClass="form-control pl-0" type="Search" placeholder="DD/MM/YYYY" AutoComplete="off"
                                            Style="border: 0px solid #ddd9d9;" />
                                        <label for="floatingInput">Date</label>
                                    </div>
                                    <div class="col-md-auto text-center">
                                        <asp:LinkButton ID="lbtnSearchServices" runat="server" class="btn btn-lg text-center text-white" OnClick="lbtnSearchServices_Click"
                                            Style="background-color: #f08701; border-radius: var(--border-radius-large) !important; min-width: 100px !important; max-width: 150px !important; padding: 13px 30px;">Search</asp:LinkButton>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>

                    <div class="row">
                        <div class="col-lg-8 col-12 mx-auto">
                            <div class="input-group input-group-lg">
                                <div class="row">
                                    <div class="col-auto">
                                        <span class="input-group-text p-0 pl-2" id="basic-addon1" style="color: var(--primary-color); font-weight: bold;">Popular Routes
                                        </span>
                                    </div>

                                    <div class="col">
                                        <p class="text-center mb-0" style="line-height: 21px;">

                                            <marquee direction="left" scrollamount="2">
                                             <asp:Repeater ID="rptRoutes" runat="server" OnItemCommand="rptRoutes_ItemCommand">
                                                     <ItemTemplate>
                                                        <asp:HiddenField ID="hdfrmstation" runat="server" Value='<%#Eval("from_station_name")%>' />
                                                          <asp:HiddenField ID="hdtostation" runat="server" Value='<%#Eval("to_station_name")%>'/>
                                                         <asp:HiddenField ID="hddfromst" runat="server" Value='<%#Eval("fromst")%>' />
                                                          <asp:HiddenField ID="hddtost" runat="server" Value='<%#Eval("tost")%>'/>
                                                        <asp:LinkButton ID="lbtnroute" runat="server" Text='<%#Eval("route_name")%>'></asp:LinkButton>                                                        | 
                                                    </ItemTemplate>
                                                </asp:Repeater>

</marquee>
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </section>
            <section class="featured-section">
                <div class="container">
                    <div class="row justify-content-center">

                        <div class="col-lg-4 col-12 mb-4 mb-lg-0">
                            <div class="custom-block bg-white shadow-lg">

                                <div>
                                    <h6 class="mb-2" style="color: #b3b3b3 !important;">Latest</h6>
                                    <asp:Panel ID="pnlNoticeNews" runat="server" Visible="false">
                                        <ul class="data-list pb-2" data-autoscroll style="min-height: 300px; max-height: 310px;  overflow:auto;">
                                            <asp:ListView ID="lvnews" runat="server" OnItemCommand="lvnews_ItemCommand"
                                                DataKeyNames="notice_id,category_code,notice_category_name,subject,subject_local,description,description_local,image_1,image_2,doc,url,valid_fromdt,valid_todt">
                                                <ItemTemplate>
                                                    <li>
                                                        <div class="mt-2">
                                                            <asp:LinkButton ID="lbtnNewsSubject" CommandName="VIEWDETAILS" runat="server" style="font-size: 15px;"><%#Eval("subject")%></asp:LinkButton>
                                                        </div>
                                                    </li>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </ul>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlNoNoticeNews" runat="server" Style="min-height: 300px; text-align: center; padding: 40px 1px;">
                                        <i class="fa fa-newspaper fa-6x text-muted"></i>
                                        <p class="card-title text-muted ">
                                            Notice/News will be available Soon.
                                        </p>
                                    </asp:Panel>

                                    <div id="demo" class="carousel slide" data-ride="carousel">
                                        <!-- Indicators -->
                                        <!-- The slideshow -->
                                        <div class="carousel-inner">
                                            <div class="carousel-item active">
                                                <p class="text-center mb-0">
                                                    Welcome to APSTS Online Services
                                                            <asp:Label ID="lblServiceAlert" runat="server" Visible="false"></asp:Label>
                                                </p>
                                            </div>
                                            <asp:Repeater ID="rptservicealert" runat="server">
                                                <ItemTemplate>
                                                    <div class="carousel-item">
                                                        <div class="welcome1">'<%#Eval("description")%>'</div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                        <!-- Left and right controls -->
                                        <a class="carousel-control-prev" href="#demo" data-slide="prev" style="width: 5% !important;">
                                            <i class="fa fa-angle-double-left text-primary"></i>
                                        </a>
                                        <a class="carousel-control-next" href="#demo" data-slide="next" style="width: 5% !important;">
                                            <i class="fa fa-angle-double-right text-primary"></i>
                                        </a>
                                    </div>

                                </div>

                            </div>
                        </div>

                        <div class="col-lg-6 col-12">
                            <div class="custom-block custom-block-overlay">
                                <div class="d-flex flex-column h-100">
                                    <img src="homeAssets/images/road.jpg" class="custom-block-image img-fluid" alt="" />
                                    <div class="custom-block-overlay-text d-flex">

                                        <div class="w-100">
                                            <table class="w-100">
                                                <tr>
                                                    <td>
                                                        <h6 class="mb-2" style="color: #b3b3b3 !important;">Online Booking</h6>

                                                        <a href="BookTicket.aspx" class="text-white mb-1"><i class="fa fa-angle-double-right small-tx" aria-hidden="true"></i>&nbsp; Book Ticket</a><br />
                                                        <a href="CancelBusTicket.aspx" class="text-white mb-1"><i class="fa fa-angle-double-right small-tx" aria-hidden="true"></i>&nbsp; Cancel Ticket</a><br />
                                                        <a href="DownloadE_ticket.aspx" class="text-white mb-1"><i class="fa fa-angle-double-right small-tx" aria-hidden="true"></i>&nbsp; Download e-Ticket</a><br />
                                                        <a href="seatavailablity.aspx" class="text-white mb-1"><i class="fa fa-angle-double-right small-tx" aria-hidden="true"></i>&nbsp; Seat Availability</a><br />
                                                        <a href="#divFaq" class="text-white mb-1"><i class="fa fa-angle-double-right small-tx" aria-hidden="true"></i>&nbsp; FAQ</a>
                                                    </td>
                                                    <td>
                                                        <h6 class="mb-2" style="color: #b3b3b3 !important;">Other Services</h6>

                                                        <a href="timetable.aspx" class="text-white mb-1"><i class="fa fa-angle-double-right small-tx" aria-hidden="true"></i>&nbsp; Timetable </a>
                                                        <br />
                                                        <a href="TrackMyBus.aspx" class="text-white mb-1"><i class="fa fa-angle-double-right small-tx" aria-hidden="true"></i>&nbsp; Track My Bus </a>
                                                        <br />
                                                        <a href="BusPass.aspx" class="text-white mb-1"><i class="fa fa-angle-double-right small-tx" aria-hidden="true"></i>&nbsp; Bus  Passes</a><br />
                                                        <a href="Helpdesk.aspx" class="text-white mb-1"><i class="fa fa-angle-double-right small-tx" aria-hidden="true"></i>&nbsp; HelpDesk</a><br />
                                                        <a href="Grievance.aspx" class="text-white mb-1"><i class="fa fa-angle-double-right small-tx" aria-hidden="true"></i>&nbsp; Grievance</a><br />

                                                    </td>
                                                </tr>
                                            </table>
                                            <table class="w-100 mt-5">
                                                <tr>
                                                    <%--<td>
                                                        <img src="assets/img/cond.png" />
                                                    </td>--%>
                                                    <td>
                                                        <p class="mb-0" style="color: #b3b3b3 !important; font-weight: 600;">
                                                            Conductor of the Month -
                                                            <asp:Label ID="lblConductorMonth" runat="server" Text=""></asp:Label>
                                                        </p>
                                                        <p class="mb-0 text-white" style="font-size: 16px;">
                                                            <asp:Label ID="lblconductor" runat="server" Text=""></asp:Label>, Depot-
                                                            <asp:Label ID="lblConductorDepot" runat="server" Text=""></asp:Label>
                                                        </p>

                                                    </td>
                                                </tr>
                                            </table>
                                        </div>

                                    </div>
                                    <div class="social-share d-flex">

                                        <p class="text-white me-3">Follow us on</p>

                                        <ul class="social-icon">

                                            <li class="social-icon-item">
                                                <a id="facebook" runat="server" target="_blank" class="social-icon-link bi-facebook"></a>
                                            </li>
                                            <li class="social-icon-item">
                                                <a id="twitter" runat="server" target="_blank" class="social-icon-link bi-twitter"></a>
                                            </li>
                                            <li class="social-icon-item">
                                                <a id="instagram" runat="server" target="_blank" class="social-icon-link bi-instagram"></a>
                                            </li>
                                            <li class="social-icon-item">
                                                <a id="youtube" runat="server" target="_blank" class="social-icon-link bi-youtube"></a>
                                            </li>

                                        </ul>

                                        <div>
                                            <p class="text-white ms-5 " style="text-align: right;">
                                                Visitor Count 
                        <asp:Label runat="server" ID="lblCount" Style="background: #021952; padding: 1px 10px; border-radius: 7px;"></asp:Label>
                                            </p>
                                        </div>


                                    </div>

                                    <div class="section-overlay" style="background-image: linear-gradient(15deg, #042459ab 0%, #14367e 100%) !important;"></div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </section>

            <section class="explore-section section-padding" id="divbusservices">

                <div class="container-fluid">
                    <div class="row">
                        <ul class="nav nav-tabs">
                            <li class="nav-item">
                                <p class="nav-link active" style="font-size: 25px;">Bus Services</p>
                            </li>
                        </ul>
                    </div>
                </div>

                <div class="container">
                    <div class="row">
                        <asp:Repeater ID="rptServiceType" runat="server" OnItemDataBound="rptServiceType_ItemDataBound" OnItemCommand="rptServiceType_ItemCommand">
                            <ItemTemplate>
                                <asp:HiddenField runat="server" ID="hdnsrtpid" Value='<%#Eval("srtpid")%>' />
                                <div class="col-lg-3 col-md-4 col-sm-4 col-12 mb-4">
                                    <div class="custom-block bg-white shadow-lg p-3">
                                        <a href="topics-detail.html">
                                            <div class="d-flex">
                                                <div>
                                                    <h5 class="mb-2">
                                                        <asp:Label ID="servicetypeName" runat="server"><%#Eval("servicetype_name_en")%></asp:Label></h5>

                                                </div>

                                                <div class="ms-auto">
                                                    <asp:LinkButton ID="lbtnroute" runat="server" CssClass="btn btn-warning btn-sm" CommandName="viewserviceRoutes" ToolTip="Click here to Route"><i class="fa fa-road" title="Click here to Route"></i> </asp:LinkButton>
                                                    <asp:LinkButton ID="lbtntimetable" runat="server" CssClass="btn btn-warning btn-sm" CommandName="viewserviceTimeTable" ToolTip="Click here to Time Table"><i class="fa fa-clock" title="Click here to Time Table"></i> </asp:LinkButton>
                                                </div>

                                            </div>
                                            <asp:Image ID="imgServicetype" runat="server" CssClass="custom-block-image img-fluid" alt="" />

                                        </a>
                                    </div>
                                </div>


                            </ItemTemplate>
                        </asp:Repeater>

                    </div>

                </div>
            </section>

            <section class="explore-section section-padding" id="dvmentor">

                <div class="container-fluid">
                    <div class="row">
                        <ul class="nav nav-tabs">
                            <li class="nav-item">
                                <p class="nav-link active" style="font-size: 25px;">Our Mentors</p>
                            </li>
                        </ul>
                    </div>
                </div>

                <div class="container">
                    <div class="row">


                        <asp:Repeater ID="rptmentor" runat="server" OnItemDataBound="rptmentor_ItemDataBound">
                            <ItemTemplate>
                                <asp:HiddenField ID="hdimage" runat="server" Value='<%#Eval("mentorimage")%>' />
                                <div class="col-lg-3 col-md-3 col-sm-6 col-12 mb-4 mb-lg-0">
                                    <div class="custom-block bg-white shadow-lg p-3">

                                        <div class="d-flex">
                                            <div>
                                                <h5 class="mb-2">
                                                    <asp:Label ID="lblmentorname" runat="server"><%#Eval("mentorname")%></asp:Label></h5>
                                                <p class="mb-0">
                                                    <asp:Label ID="lblmentordesignation" runat="server"><%#Eval("mentordesignation")%></asp:Label>
                                                </p>
                                            </div>
                                        </div>
                                        <asp:Image ID="imgmentor" runat="server" CssClass="custom-block-image img-fluid" alt="" />

                                    </div>
                                </div>

                            </ItemTemplate>
                        </asp:Repeater>





                    </div>

                </div>
            </section>

            <%--<section class="explore-section section-padding" id="section_2">
                <div class="container">
                    <div class="row">

                        <div class="col-12 text-center">
                            <h2 class="mb-4">
                            Browse Topics</h1>
                        </div>

                    </div>
                </div>

                <div class="container-fluid">
                    <div class="row">
                        <ul class="nav nav-tabs" id="myTab" role="tablist">
                            <li class="nav-item" role="presentation">
                                <button class="nav-link active" id="design-tab" data-bs-toggle="tab" data-bs-target="#design-tab-pane" type="button" role="tab" aria-controls="design-tab-pane" aria-selected="true">Design</button>
                            </li>

                            <li class="nav-item" role="presentation">
                                <button class="nav-link" id="marketing-tab" data-bs-toggle="tab" data-bs-target="#marketing-tab-pane" type="button" role="tab" aria-controls="marketing-tab-pane" aria-selected="false">Marketing</button>
                            </li>

                            <li class="nav-item" role="presentation">
                                <button class="nav-link" id="finance-tab" data-bs-toggle="tab" data-bs-target="#finance-tab-pane" type="button" role="tab" aria-controls="finance-tab-pane" aria-selected="false">Finance</button>
                            </li>

                            <li class="nav-item" role="presentation">
                                <button class="nav-link" id="music-tab" data-bs-toggle="tab" data-bs-target="#music-tab-pane" type="button" role="tab" aria-controls="music-tab-pane" aria-selected="false">Music</button>
                            </li>

                            <li class="nav-item" role="presentation">
                                <button class="nav-link" id="education-tab" data-bs-toggle="tab" data-bs-target="#education-tab-pane" type="button" role="tab" aria-controls="education-tab-pane" aria-selected="false">Education</button>
                            </li>
                        </ul>
                    </div>
                </div>

                <div class="container">
                    <div class="row">

                        <div class="col-12">
                            <div class="tab-content" id="myTabContent">
                                <div class="tab-pane fade show active" id="design-tab-pane" role="tabpanel" aria-labelledby="design-tab" tabindex="0">
                                    <div class="row">
                                        <div class="col-lg-4 col-md-6 col-12 mb-4 mb-lg-0">
                                            <div class="custom-block bg-white shadow-lg">
                                                <a href="topics-detail.html">
                                                    <div class="d-flex">
                                                        <div>
                                                            <h5 class="mb-2">Web Design</h5>

                                                            <p class="mb-0">Topic Listing Template based on Bootstrap 5</p>
                                                        </div>

                                                        <span class="badge bg-design rounded-pill ms-auto">14</span>
                                                    </div>

                                                    <img src="homeAssets/images/topics/undraw_Remote_design_team_re_urdx.png" class="custom-block-image img-fluid" alt="">
                                                </a>
                                            </div>
                                        </div>

                                        <div class="col-lg-4 col-md-6 col-12 mb-4 mb-lg-0">
                                            <div class="custom-block bg-white shadow-lg">
                                                <a href="topics-detail.html">
                                                    <div class="d-flex">
                                                        <div>
                                                            <h5 class="mb-2">Graphic</h5>

                                                            <p class="mb-0">Lorem Ipsum dolor sit amet consectetur</p>
                                                        </div>

                                                        <span class="badge bg-design rounded-pill ms-auto">75</span>
                                                    </div>

                                                    <img src="homeAssets/images/topics/undraw_Redesign_feedback_re_jvm0.png" class="custom-block-image img-fluid" alt="">
                                                </a>
                                            </div>
                                        </div>

                                        <div class="col-lg-4 col-md-6 col-12">
                                            <div class="custom-block bg-white shadow-lg">
                                                <a href="topics-detail.html">
                                                    <div class="d-flex">
                                                        <div>
                                                            <h5 class="mb-2">Logo Design</h5>

                                                            <p class="mb-0">Lorem Ipsum dolor sit amet consectetur</p>
                                                        </div>

                                                        <span class="badge bg-design rounded-pill ms-auto">100</span>
                                                    </div>

                                                    <img src="homeAssets/images/topics/colleagues-working-cozy-office-medium-shot.png" class="custom-block-image img-fluid" alt="">
                                                </a>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="tab-pane fade" id="marketing-tab-pane" role="tabpanel" aria-labelledby="marketing-tab" tabindex="0">
                                    <div class="row">
                                        <div class="col-lg-4 col-md-6 col-12 mb-4 mb-lg-3">
                                            <div class="custom-block bg-white shadow-lg">
                                                <a href="topics-detail.html">
                                                    <div class="d-flex">
                                                        <div>
                                                            <h5 class="mb-2">Advertising</h5>

                                                            <p class="mb-0">Lorem Ipsum dolor sit amet consectetur</p>
                                                        </div>

                                                        <span class="badge bg-advertising rounded-pill ms-auto">30</span>
                                                    </div>

                                                    <img src="homeAssets/images/topics/undraw_online_ad_re_ol62.png" class="custom-block-image img-fluid" alt="">
                                                </a>
                                            </div>
                                        </div>

                                        <div class="col-lg-4 col-md-6 col-12 mb-4 mb-lg-3">
                                            <div class="custom-block bg-white shadow-lg">
                                                <a href="topics-detail.html">
                                                    <div class="d-flex">
                                                        <div>
                                                            <h5 class="mb-2">Video Content</h5>

                                                            <p class="mb-0">Lorem Ipsum dolor sit amet consectetur</p>
                                                        </div>

                                                        <span class="badge bg-advertising rounded-pill ms-auto">65</span>
                                                    </div>

                                                    <img src="homeAssets/images/topics/undraw_Group_video_re_btu7.png" class="custom-block-image img-fluid" alt="">
                                                </a>
                                            </div>
                                        </div>

                                        <div class="col-lg-4 col-md-6 col-12">
                                            <div class="custom-block bg-white shadow-lg">
                                                <a href="topics-detail.html">
                                                    <div class="d-flex">
                                                        <div>
                                                            <h5 class="mb-2">Viral Tweet</h5>

                                                            <p class="mb-0">Lorem Ipsum dolor sit amet consectetur</p>
                                                        </div>

                                                        <span class="badge bg-advertising rounded-pill ms-auto">50</span>
                                                    </div>

                                                    <img src="homeAssets/images/topics/undraw_viral_tweet_gndb.png" class="custom-block-image img-fluid" alt="">
                                                </a>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="tab-pane fade" id="finance-tab-pane" role="tabpanel" aria-labelledby="finance-tab" tabindex="0">
                                    <div class="row">
                                        <div class="col-lg-6 col-md-6 col-12 mb-4 mb-lg-0">
                                            <div class="custom-block bg-white shadow-lg">
                                                <a href="topics-detail.html">
                                                    <div class="d-flex">
                                                        <div>
                                                            <h5 class="mb-2">Investment</h5>

                                                            <p class="mb-0">Lorem Ipsum dolor sit amet consectetur</p>
                                                        </div>

                                                        <span class="badge bg-finance rounded-pill ms-auto">30</span>
                                                    </div>

                                                    <img src="homeAssets/images/topics/undraw_Finance_re_gnv2.png" class="custom-block-image img-fluid" alt="">
                                                </a>
                                            </div>
                                        </div>

                                        <div class="col-lg-6 col-md-6 col-12">
                                            <div class="custom-block custom-block-overlay">
                                                <div class="d-flex flex-column h-100">
                                                    <img src="homeAssets/images/businesswoman-using-tablet-analysis-graph-company-finance-strategy-statistics-success-concept-planning-future-office-room.jpg" class="custom-block-image img-fluid" alt="">

                                                    <div class="custom-block-overlay-text d-flex">
                                                        <div>
                                                            <h5 class="text-white mb-2">Finance</h5>

                                                            <p class="text-white">Lorem ipsum dolor, sit amet consectetur adipisicing elit. Sint animi necessitatibus aperiam repudiandae nam omnis</p>

                                                            <a href="topics-detail.html" class="btn custom-btn mt-2 mt-lg-3">Learn More</a>
                                                        </div>

                                                        <span class="badge bg-finance rounded-pill ms-auto">25</span>
                                                    </div>

                                                    <div class="social-share d-flex">
                                                        <p class="text-white me-4">Share:</p>

                                                        <ul class="social-icon">
                                                            <li class="social-icon-item">
                                                                <a href="#" class="social-icon-link bi-twitter"></a>
                                                            </li>

                                                            <li class="social-icon-item">
                                                                <a href="#" class="social-icon-link bi-facebook"></a>
                                                            </li>

                                                            <li class="social-icon-item">
                                                                <a href="#" class="social-icon-link bi-pinterest"></a>
                                                            </li>
                                                        </ul>

                                                        <a href="#" class="custom-icon bi-bookmark ms-auto"></a>
                                                    </div>

                                                    <div class="section-overlay"></div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="tab-pane fade" id="music-tab-pane" role="tabpanel" aria-labelledby="music-tab" tabindex="0">
                                    <div class="row">
                                        <div class="col-lg-4 col-md-6 col-12 mb-4 mb-lg-3">
                                            <div class="custom-block bg-white shadow-lg">
                                                <a href="topics-detail.html">
                                                    <div class="d-flex">
                                                        <div>
                                                            <h5 class="mb-2">Composing Song</h5>

                                                            <p class="mb-0">Lorem Ipsum dolor sit amet consectetur</p>
                                                        </div>

                                                        <span class="badge bg-music rounded-pill ms-auto">45</span>
                                                    </div>

                                                    <img src="homeAssets/images/topics/undraw_Compose_music_re_wpiw.png" class="custom-block-image img-fluid" alt="">
                                                </a>
                                            </div>
                                        </div>

                                        <div class="col-lg-4 col-md-6 col-12 mb-4 mb-lg-3">
                                            <div class="custom-block bg-white shadow-lg">
                                                <a href="topics-detail.html">
                                                    <div class="d-flex">
                                                        <div>
                                                            <h5 class="mb-2">Online Music</h5>

                                                            <p class="mb-0">Lorem Ipsum dolor sit amet consectetur</p>
                                                        </div>

                                                        <span class="badge bg-music rounded-pill ms-auto">45</span>
                                                    </div>

                                                    <img src="homeAssets/images/topics/undraw_happy_music_g6wc.png" class="custom-block-image img-fluid" alt="">
                                                </a>
                                            </div>
                                        </div>

                                        <div class="col-lg-4 col-md-6 col-12">
                                            <div class="custom-block bg-white shadow-lg">
                                                <a href="topics-detail.html">
                                                    <div class="d-flex">
                                                        <div>
                                                            <h5 class="mb-2">Podcast</h5>

                                                            <p class="mb-0">Lorem Ipsum dolor sit amet consectetur</p>
                                                        </div>

                                                        <span class="badge bg-music rounded-pill ms-auto">20</span>
                                                    </div>

                                                    <img src="homeAssets/images/topics/undraw_Podcast_audience_re_4i5q.png" class="custom-block-image img-fluid" alt="">
                                                </a>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="tab-pane fade" id="education-tab-pane" role="tabpanel" aria-labelledby="education-tab" tabindex="0">
                                    <div class="row">
                                        <div class="col-lg-6 col-md-6 col-12 mb-4 mb-lg-3">
                                            <div class="custom-block bg-white shadow-lg">
                                                <a href="topics-detail.html">
                                                    <div class="d-flex">
                                                        <div>
                                                            <h5 class="mb-2">Graduation</h5>

                                                            <p class="mb-0">Lorem Ipsum dolor sit amet consectetur</p>
                                                        </div>

                                                        <span class="badge bg-education rounded-pill ms-auto">80</span>
                                                    </div>

                                                    <img src="homeAssets/images/topics/undraw_Graduation_re_gthn.png" class="custom-block-image img-fluid" alt="">
                                                </a>
                                            </div>
                                        </div>

                                        <div class="col-lg-6 col-md-6 col-12">
                                            <div class="custom-block bg-white shadow-lg">
                                                <a href="topics-detail.html">
                                                    <div class="d-flex">
                                                        <div>
                                                            <h5 class="mb-2">Educator</h5>

                                                            <p class="mb-0">Lorem Ipsum dolor sit amet consectetur</p>
                                                        </div>

                                                        <span class="badge bg-education rounded-pill ms-auto">75</span>
                                                    </div>

                                                    <img src="homeAssets/images/topics/undraw_Educator_re_ju47.png" class="custom-block-image img-fluid" alt="">
                                                </a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
            </section>--%>

            <section class="timeline-section section-padding" id="section_3">
                <div class="section-overlay" style="background-image: linear-gradient(15deg, #13547a 0%, #537cad 100%) !important;"></div>

                <div class="container">

                    <div class="row">

                        <div class="col-12 text-center">
                            <h2 class="text-white mb-4">How to book online ticket ? </h2>
                        </div>

                        <div class="col-lg-10 col-12 mx-auto">
                            <div class="timeline-container">
                                <ul class="vertical-scrollable-timeline" id="vertical-scrollable-timeline">
                                    <div class="list-progress">
                                        <div class="inner"></div>
                                    </div>

                                    <li>
                                        <h4 class="text-white mb-3">Search your bus</h4>

                                        <p class="text-white">
                                            Enter <b>From station</b>, <b>To station</b> and <b>Journey Date</b> for search your bus. 
                                        </p>

                                        <div class="icon-holder">
                                            <i class="bi-search"></i>
                                        </div>
                                    </li>

                                    <li>
                                        <h4 class="text-white mb-3">Select Seat &amp; Enter Details</h4>

                                        <p class="text-white">
                                            After Searching, Select your favorite seat from bus layout and fill the passengers details against the seats. You can select multiple seat at a time.
                                        </p>

                                        <div class="icon-holder">
                                            <i class="bi-bookmark"></i>
                                        </div>
                                    </li>

                                    <li>
                                        <h4 class="text-white mb-3">Confirm &amp; Payment</h4>

                                        <p class="text-white">
                                            Confirm passenger details and select payment mode (Wallet or Payment Gateway). After payment done your ticket will be confirmed 
                                        </p>

                                        <div class="icon-holder">
                                            <i class="bi-book"></i>
                                        </div>
                                    </li>
                                </ul>
                            </div>
                        </div>

                    </div>
                </div>
            </section>

            <section class="explore-section section-padding" id="dvphoto">

                <div class="container-fluid">
                    <div class="row">
                        <ul class="nav nav-tabs">
                            <li class="nav-item">
                                <p class="nav-link active" style="font-size: 25px;">Photo Gallery</p>
                            </li>
                        </ul>
                    </div>
                </div>

                <div class="container">
                    <div class="row">
                        <div class="col-md-4">
                            <div class="card  img-wrapper-20 ">
                                <asp:HyperLink ID="hlinkPG1"
                                    ToolTip="Click to view"
                                    Target="_self"
                                    runat="server">

                                    <asp:Image runat="server" ID="imgPG1" class="img-fluid shadow" />
                                    <div class="p-2">
                                        <asp:Label ID="lblimgPG1" runat="server" Style="font-weight: 600;"></asp:Label>
                                    </div>

                                </asp:HyperLink>

                            </div>
                        </div>
                        <div class="col-md-4">

                            <div class="card  img-wrapper-20 ">
                                <asp:HyperLink ID="hlinkPG2"
                                    ToolTip="Click to view"
                                    Target="_self"
                                    runat="server">
                                    <asp:Image runat="server" ID="imgPG2" class="img-fluid shadow" />
                                    <div class="p-2">
                                        <asp:Label ID="lblimgPG2" runat="server" Style="font-weight: 600;"></asp:Label>
                                    </div>
                                </asp:HyperLink>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="card  img-wrapper-20">
                                <asp:HyperLink ID="hlinkPG3"
                                    ToolTip="Click to view"
                                    Target="_self"
                                    runat="server">
                                    <asp:Image runat="server" ID="imgPG3" class="img-fluid shadow" />
                                    <div class="p-2">
                                        <asp:Label ID="lblimgPG3" runat="server" Style="font-weight: 600;"></asp:Label>
                                    </div>
                                </asp:HyperLink>


                            </div>
                        </div>
                    </div>
                    <div class="row pt-2">
                        <div class="col-md-4">
                            <div class="card  img-wrapper-20">
                                <asp:HyperLink ID="hlinkPG4"
                                    ToolTip="Click to view"
                                    Target="_self"
                                    runat="server">
                                    <asp:Image runat="server" ID="imgPG4" class="img-fluid shadow" />
                                    <div class="p-2">
                                        <asp:Label ID="lblimgPG4" runat="server" Style="font-weight: 600;"></asp:Label>
                                    </div>
                                </asp:HyperLink>
                            </div>
                        </div>

                        <div class="col-md-4">
                            <div class="card  img-wrapper-20 ">
                                <asp:HyperLink ID="hlinkPG5"
                                    ToolTip="Click to view"
                                    Target="_self"
                                    runat="server">
                                    <asp:Image runat="server" ID="imgPG5" class="img-fluid shadow" />
                                    <div class="p-2">
                                        <asp:Label ID="lblimgPG5" runat="server" Style="font-weight: 600;"></asp:Label>
                                    </div>
                                </asp:HyperLink>
                            </div>
                        </div>

                        <div class="col-md-4">
                            <div class="card  img-wrapper-20 ">
                                <asp:HyperLink ID="hlinkPG6"
                                    ToolTip="Click to view"
                                    Target="_self"
                                    runat="server">
                                    <asp:Image runat="server" ID="imgPG6" class="img-fluid shadow" />
                                    <div class="p-2">
                                        <asp:Label ID="lblimgPG6" runat="server" Style="font-weight: 600;"></asp:Label>
                                    </div>
                                </asp:HyperLink>
                            </div>
                        </div>

                    </div>
                    <div class="row row-centered pos mt-4">
                        <div class="col-lg-12 col-xs-12 text-center ">
                            <asp:Button ID="btnPhotoMore" runat="server" Text="VIEW MORE IMAGES" Class="btn btn-primary" OnClick="btnPhotoMore_Click"></asp:Button>
                        </div>
                    </div>

                </div>
            </section>

            <section class="faq-section section-padding" id="divFaq">
                <div class="container">
                    <div class="row">

                        <div class="col-lg-12 col-12">
                            <h2 class="mb-4">Frequently Asked Questions</h2>
                        </div>

                        <div class="clearfix"></div>

                        <div class="col-lg-4 col-md-4 col-12">
                            <img src="homeAssets/images/faq_graphic.jpg" class="img-fluid" alt="FAQs" />
                        </div>

                        <div class="col-lg-8 col-md-8 col-12 m-auto">

                            <div class="row">
                                <asp:Repeater ID="rptfaqcategory" runat="server" OnItemCommand="rptfaqcategory_ItemCommand">
                                    <ItemTemplate>
                                        <div class="col-auto mb-3">
                                            <asp:HiddenField ID="hffaqcategodyid" runat="server" Value='<%#Eval("faqid")%>' />
                                            <asp:LinkButton ID="lbtnfaq" runat="server" CssClass="btn btn-sm btn-primary ml-1" CommandName="show"> <%#Eval("faqcategory")%></asp:LinkButton>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>

                            <div id="accordionExample" class="accordion">
                                <asp:Label ID="lblNoFaq" runat="server" Text="FAQ of this Category will be available very soon."></asp:Label>
                                <asp:Repeater ID="rptfaq" runat="server">
                                    <ItemTemplate>

                                        <div class="accordion-item">
                                            <h2 class="accordion-header" id='heading<%#Eval("faq_id")%>'>
                                                <button class="accordion-button" type="button" data-bs-toggle="collapse" data-bs-target='#<%#Eval("datatarget")%>'
                                                    aria-expanded="false" aria-controls='<%#Eval("datatarget")%>' style="font-size: 17px;">
                                                    <%#Eval("faqs")%>
                                                </button>
                                            </h2>

                                            <div id='<%#Eval("datatarget")%>' class="accordion-collapse collapse" aria-labelledby='heading<%#Eval("faq_id")%>'
                                                data-bs-parent="#accordionExample">
                                                <div class="accordion-body">
                                                    <%#Eval("faq_answer")%>
                                                </div>
                                            </div>
                                        </div>

                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>

                        </div>

                    </div>
                </div>
            </section>

        </main>

        <footer class="site-footer section-padding" style="background: var(--secondary-color);">
            <div class="container">
                <div class="row">

                    <div class="col-lg-3 col-12 mb-4 pb-2">

                        <h4 class="text-white">
                            <span>StarBus<sup><i class="fa fa-sm fa-star"></i></sup></span><br />
                            <span>Govt. of India</span>
                        </h4>
                        <p class="copyright-text text-white">
                            @ All Copyright
                            <script>
                                var CurrentYear = new Date().getFullYear()
                                document.write(CurrentYear)
                            </script>
                            StarBus*<br />
                            National Informatics Centre                        
                        </p>
                    </div>

                    <div class="col-lg-3 col-md-4 col-6">
                        <h6 class="site-footer-title mb-3" style="color: #b3b3b3 !important;">Usefull Link</h6>

                        <ul class="site-footer-links">
                            <li class="site-footer-link-item mb-2">
                                <a href="Webinfomgr.aspx" class="text-white">Web Information Manager</a>
                            </li>

                            <li class="site-footer-link-item mb-2">
                                <a href="Disclaimer.aspx" class="text-white">Disclaimer</a>
                            </li>

                            <li class="site-footer-link-item">
                                <a href="PrivacyPolicy.aspx" class="text-white">Privacy Policy</a>
                            </li>

                        </ul>
                    </div>

                    <div class="col-lg-3 col-md-4 col-6 mb-4 mb-lg-0">
                        <h6 class="site-footer-title mb-3" style="color: #b3b3b3 !important;">Contact Info</h6>
                        <p class="text-white d-flex mb-1">
                            <asp:Label runat="server" ID="lbladdress" Text="-NA-"></asp:Label>
                        </p>
                        <p class="text-white d-flex mb-1">
                            <asp:Label ID="lblcontact" runat="server" Text="-NA-"></asp:Label>
                        </p>

                        <p class="text-white d-flex">
                            <asp:Label runat="server" ID="lblemail" Text="-NA-"></asp:Label>
                        </p>
                    </div>

                    <div class="col-lg-3 col-md-4 col-12 mt-4 mt-lg-0 ms-auto">

                        <h6 class="site-footer-title mb-3" style="color: #b3b3b3 !important;">Powered By</h6>
                        <img src="assets/img/nic2.png" />
                        <img src="assets/img/f1.png" />

                    </div>

                </div>
            </div>
        </footer>


        <cc1:ModalPopupExtender ID="mpEventAlert" runat="server" PopupControlID="pnlEventAlert"
            CancelControlID="lbtnNoConfirmation" TargetControlID="Button4" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlEventAlert" runat="server" Style="position: fixed;">
            <div class="p-4" style="max-width: 80vw; max-height:90vh; overflow:auto;">
                <div class="row">
                    <div class="col"></div>
                    <div class="col-auto">
                        <asp:LinkButton ID="lbtnNoConfirmation" runat="server" CssClass="btn btn-danger" Style="margin-top: -29px; margin-right: -20px"> <i class="fa fa-times"></i> </asp:LinkButton>
                    </div>
                </div>

                <asp:Repeater ID="rpteventalert" runat="server" OnItemDataBound="rpteventalert_ItemDataBound" OnItemCommand="rpteventalert_ItemCommand">
                    <ItemTemplate>

                        <div class="custom-block custom-block-topics-listing bg-white shadow-lg mb-5">
                            <div class="row">
                                <div class="col-sm-6">
                                    <asp:Image ID="imgEventAlert" runat="server" Style="height: 250px; width: 100%;" />
                                </div>
                                <div class="col-sm-6">
                                    <div class="custom-block-topics-listing-info d-flex">
                                        <div>
                                            <h5 class="mb-2">
                                                <asp:HiddenField ID="hdnotice_id" runat="server" Value='<%#Eval("notice_id")%>' />
                                                <asp:Label ID="lblSubject" runat="server" Text='<%#Eval("subject")%>'>
                                                </asp:Label>
                                            </h5>

                                            <asp:LinkButton ID="lbtnViewMore1" CommandName="ViewDetails" CssClass="btn custom-btn mt-3 mt-lg-4" runat="server">READ MORE</asp:LinkButton>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>

                    </ItemTemplate>
                </asp:Repeater>


            </div>
            <div style="visibility: hidden;">
                <asp:Button ID="Button4" runat="server" Text="" />
            </div>
        </asp:Panel>

        <div class="modal fade" id="CPModal1" tabindex="-1" role="dialog" aria-labelledby="CPModalLabel" aria-hidden="true">
            <div class="modal-dialog  modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <div class="col-md">
                            <h4 class="m-0">Cancellation Policy</h4>
                        </div>
                        <div class="col-md-auto">
                            <a id="closepolcy" style="color: Red; cursor: pointer;"><i class="fa fa-times fa-2x"></i></a>
                        </div>
                    </div>
                    <div class="modal-body">
                        <ul class="list-group" style="">
                            <li class="list-group-item">Cancellation/Refund/Rescheduling Ticket booked through Online,
                                    refund will be done to their respective Credit Cards/Debit Cards/Bank Accounts according
                                    to the Bank procedure. No refund will be done at APSTS ticket booking counters
                                    
                            </li>
                            <li class="list-group-item">
                                <asp:Label ID="lblcancellationpolicy" runat="server"></asp:Label>
                            </li>
                            <li class="list-group-item">Cancellation is not allowed after Up to 2 hr before Schedule
                                    service start time at origin of the bus. From the date of journey.</li>
                            <li class="list-group-item">Reservation Fee is non-refundable except in case of 100%
                                    cancellation of tickets, if the service is cancelled by APSTS for operational or any
                                    other reasons.</li>
                            <li class="list-group-item">No refund will be given to “No-SHOW” passengers who do not report at the boarding point time.
                            </li>


                            <li class="list-group-item">Passengers will be given normally in one month, after the
                                    cancellation of ticket or receipt of e-mail. If refunds are delayed more than a
                                    month, passengers may contact helpdesk</li>
                            <li class="list-group-item">Payment Gateway Service charges will not be refunded for
                                    service cancellation/ failure transactions in e-ticketing.</li>
                            <li class="list-group-item">Partial cancellation is allowed for which cancellation terms
                                    & conditions will apply.</li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>

        <cc1:ModalPopupExtender ID="mpFirst" runat="server" PopupControlID="pnlmpFirst"
            CancelControlID="btnClosempFirst" TargetControlID="btnOpenmpFirst" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlmpFirst" runat="server">
            <div class="card">

                <div class="card-body overflow-auto" style="min-width: 35vw !important; max-width: 95vw !important; min-height: 20vh; max-height: 80vh;">

                    <div class="w-100 text-center p-3 pb-4">
                        <h1 class="text-center">StarBus* APSTS Traveller Mobile App is also available.</h1>
                    </div>
                    <div class="row">
                        <div class="col text-center">
                            <asp:LinkButton ID="lbtndownload" runat="server" OnClick="appdownload_ServerClick" class="btn btn-primary btn-sm" Style="font-size: 12pt; color: white;"> <i class="fa fa-mobile"></i> Download App </asp:LinkButton>

                        </div>
                        <div class="col text-center">
                            <a id="btnClosempFirst" class="btn btn-danger btn-sm" style="font-size: 12pt; color: white;"><i class="fa fa-times"></i>Not Now</a>
                        </div>
                    </div>

                    <div class="w-100 text-center pb-2">




                        <a id="a_playstore" runat="server" onserverclick="appdownload_ServerClick" class="w-100">
                            <img src="images/Android_logo.png" class="w-100" style="width: 20% !important;" />
                        </a>

                        <a id="a_appstore" runat="server" onserverclick="appdownload_ServerClick" class="w-100">
                            <img src="images/iphone-logo.pn" class="w-100" style="width: 20% !important;" />
                        </a>
                    </div>
                    <div class="w-100 text-center">
                    </div>
                </div>
            </div>
            <div style="visibility: hidden">
                <asp:Button ID="btnOpenmpFirst" runat="server" />
            </div>
        </asp:Panel>


        <!-- JAVASCRIPT FILES -->
        <script src="homeAssets/js/jquery.min.js"></script>
        <script src="assets/js/jquery-ui.js"></script>
        <script src="homeAssets/js/bootstrap.bundle.min.js"></script>
        <script src="homeAssets/js/jquery.sticky.js"></script>
        <script src="homeAssets/js/click-scroll.js"></script>
        <script src="homeAssets/js/custom.js"></script>
        <script src="assets/vendor/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js"></script>
      
    <script src="js/jquery.autoscroll.js"></script>
    </form>
</body>
</html>
