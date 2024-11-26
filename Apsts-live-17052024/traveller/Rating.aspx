<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rating.aspx.cs" Inherits="traveller_Rating" %>

<!DOCTYPE html>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta name="description" content="Starbus online ticket booking" />
    <meta name="author" content="Creative Tim" />
    <title>Starbus</title>
    <!-- Favicon -->
    <link rel="icon" href="../assets/img/brand/favicon.png" type="image/png" />

    <!-- Icons -->
    <link rel="stylesheet" href="../assets/vendor/nucleo/css/nucleo.css" type="text/css" />
    <link rel="stylesheet" href="../assets/vendor/@fortawesome/fontawesome-free/css/all.min.css" type="text/css" />
    <!-- Page plugins -->
    <!-- Argon CSS -->
    <link rel="stylesheet" href="../assets/css/argon.css?v=1.2.0" type="text/css" />
    <link href="../assets/vendor/bootstrap-datepicker/dist/css/bootstrap-datepicker.min.css" rel="stylesheet" />
    <%--<link href="../assets/css/common.css" rel="stylesheet" />--%>
    <style>
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
</head>
<body onload="HideLoading()">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="main-content" id="panel">

            <%
                sbLoaderNdPopup dd = new sbLoaderNdPopup();
                string loader = dd.getLoaderHtml();
                Response.Write(loader);
            %>


            <!-- Topnav -->
            <nav class="navbar navbar-top navbar-expand navbar-dark bg-primary border-bottom">
                <div class="container-fluid">
                    <div class="collapse navbar-collapse" id="navbarSupportedContent">
                        <div class="d-flex justify-content-between align-items-center">
                            <div class="border-right pr-2" style="border-color: rgb(255 255 225 / 59%) !important">
                                <h3 class="mb-0 text-white">StarBus</h3>
                            </div>
                            <div class="pl-2">
                                <h3 class="mb-0 text-white">
                                    <asp:Label runat="server" ID="lblModuleName"></asp:Label></h3>
                            </div>
                        </div>

                        <!-- Navbar links -->
                        <ul class="navbar-nav align-items-center  ml-md-auto ">
                            <li class="nav-item ">
                                <a class="nav-link pr-0" href="../Logout.aspx" role="button" data-toggle="tooltip" data-placement="bottom" title="Logout">
                                    <div class="media align-items-center">
                                        <span class="avatar avatar-sm rounded-circle bg-transparent">
                                            <i class="fa fa-sign-out-alt"></i>
                                        </span>
                                    </div>
                                </a>
                            </li>
                        </ul>

                    </div>
                </div>
            </nav>
            <!-- Header -->
            <div class="container-fluid mt-4">
                <div class="row">
                    <div class="col-xl-4">
                        <div class="card">
                            <div class="card-header bg-transparent border-0">
                                <div class="row align-items-center">
                                    <div class="col">
                                        <h5 class="h3 text-black-50 mb-0">Rate us</h5>
                                    </div>
                                    <div class="col-auto text-right">
                                        <h5 class="h3 text-black-50 mb-0"></h5>
                                    </div>
                                </div>
                            </div>
                            <div class="card-body" style="min-height: 50vh !important">
                                <p class="mb-0">You are here to help us.</p>
                                <p class="mb-0">We are working on improving our services.</p>
                                <p class="mb-0">kindly give us rating for your previous journey. it helps us to improve our services.</p>
                                <p class="mb-0">Rating is mandatory after complation your journey.</p>
                                <p class="mb-0">Your rating is for the servious depending on staff (Driver/Conductor), bus condition and booking portal.</p>
                            </div>
                        </div>
                    </div>
                    <div class="col-xl-8">
                        <div class="card">
                            <div class="card-header bg-transparent border-0">
                                <div class="row align-items-center">
                                    <div class="col">
                                        <h5 class="h3 text-black-50 mb-0">Journey Completed Tickets</h5>
                                    </div>
                                    <div class="col-auto text-right">
                                        <h5 class="h3 text-black-50 mb-0"></h5>
                                    </div>
                                </div>
                            </div>
                            <div class="card-body" style="min-height: 50vh !important">
                                <asp:GridView ID="gvRatingTickets" runat="server" GridLines="None" ShowHeader="false" AutoGenerateColumns="false" Width="100%"
                                    OnRowCommand="gvRatingTickets_RowCommand" DataKeyNames="_ticketno,booked_by">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <div class="row px-4 py-2 border-top">
                                                    <div class="col">
                                                        <span class="h3 font-weight-bold mb-0"><%# Eval("_ticketno") %> <span class="mb-0 text-xs text-muted">( Journey <%# Eval("journeydate") %>)</span></span>
                                                        <h5 class="card-title text-xs text-uppercase text-muted mb-0"><%# Eval("fromstn_name") %> - <%# Eval("tostn_name") %></h5>
                                                    </div>
                                                    <div class="col-auto text-right ">
                                                        <asp:LinkButton ID="lbtnRate" runat="server" CssClass="btn btn-icon btn-primary btn-sm"
                                                            CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="VIEWDETAIL">
                                                    <i class="fa fa-star-half-alt"></i> Rate Us
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                                <asp:Panel ID="pnlRate" runat="server" Visible="false" Style="border-bottom: 4px solid #eedada;">
                                                    <div class="row px-4 py-1">
                                                        <div class="col" style="min-width: 200px;">
                                                            <h5 class="mb-0">Driver/Conductor</h5>
                                                            <cc1:Rating ID="rcStaff" AutoPostBack="true" OnChanged="rcStaff_Changed" runat="server" StarCssClass="ratingEmpty" WaitingStarCssClass="ratingSaved" EmptyStarCssClass="ratingEmpty" FilledStarCssClass="ratingFilled">
                                                            </cc1:Rating>
                                                        </div>
                                                        <div class="col" style="min-width: 200px;">
                                                            <asp:TextBox ID="tbRateStaff" runat="server" Visible="false" CssClass="form-control p-1" TextMode="MultiLine" placeholder="Enter Staff related feedback here.. (Max 100 chars)"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="row px-4 py-1">
                                                        <div class="col" style="min-width: 200px;">
                                                            <h5 class="mb-0">Bus</h5>
                                                            <cc1:Rating ID="rcBus" AutoPostBack="true" OnChanged="rcBus_Changed" runat="server" StarCssClass="ratingEmpty" WaitingStarCssClass="ratingSaved" EmptyStarCssClass="ratingEmpty" FilledStarCssClass="ratingFilled">
                                                            </cc1:Rating>
                                                        </div>
                                                        <div class="col">
                                                            <asp:TextBox ID="tbRateBus" runat="server" Visible="false" CssClass="form-control p-1" TextMode="MultiLine" placeholder="Enter Bus related feedback here.. (Max 100 chars)"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="row px-4 py-1">
                                                        <div class="col" style="min-width: 200px;">
                                                            <h5 class="mb-0">Booking Portal</h5>
                                                            <cc1:Rating ID="rcPortal" AutoPostBack="true" OnChanged="rcPortal_Changed" runat="server" StarCssClass="ratingEmpty" WaitingStarCssClass="ratingSaved" EmptyStarCssClass="ratingEmpty" FilledStarCssClass="ratingFilled">
                                                            </cc1:Rating>
                                                        </div>
                                                        <div class="col" style="min-width: 200px;">
                                                            <asp:TextBox ID="tbRatePortal" runat="server" Visible="false" CssClass="form-control p-1" TextMode="MultiLine" placeholder="Enter Portal related feedback here.. (Max 100 chars)"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="row px-4 py-2 pb-5">
                                                        <div class="col" style="min-width: 200px;">
                                                            <asp:LinkButton ID="lbtnSaveRate" runat="server" CssClass="btn btn-icon btn-success"
                                                                CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="SAVERATING">
                                                    <i class="fa fa-check"></i> Submit
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
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
                                            <p class="h2 font-weight-bold mb-0">
                                                Thanks for being here.<br />
                                                No ticket available for rating.
                                            </p>
                                            <a href="dashboard.aspx" class="btn btn-outline-primary my-5"><i class="fa fa-home"></i>Home</a>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>

                <%--   <div class="alert alert-default alert-dismissible fade show" role="alert">
                    <span class="alert-icon"><i class="ni ni-like-2"></i></span>
                    <span class="alert-text"><strong>Default!</strong> This is a default alert—check it out!</span>
                    <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>--%>
            </div>
        </div>
    </form>

    <script src="../assets/js/jquery-n.js"></script>
    <script src="../assets/vendor/jquery/dist/jquery.min.js"></script>
    <script src="../assets/js/jquery-ui.js"></script>
    <script src="../assets/vendor/bootstrap/dist/js/bootstrap.bundle.min.js"></script>
    <script src="../assets/vendor/js-cookie/js.cookie.js"></script>
    <script src="../assets/vendor/jquery.scrollbar/jquery.scrollbar.min.js"></script>
    <script src="../assets/vendor/jquery-scroll-lock/dist/jquery-scrollLock.min.js"></script>
    <!-- Optional JS -->
    <script src="../assets/vendor/chart.js/dist/Chart.min.js"></script>
    <script src="../assets/vendor/chart.js/dist/Chart.extension.js"></script>
    <script src="../assets/vendor/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js"></script>
    <!-- Argon JS -->
    <script src="../assets/js/argon.js?v=1.2.0"></script>
</body>
</html>
