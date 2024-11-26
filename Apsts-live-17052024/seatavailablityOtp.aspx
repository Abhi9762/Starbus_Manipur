<%@ Page Language="C#" AutoEventWireup="true" CodeFile="seatavailablityOtp.aspx.cs" Inherits="seatavailablityOtp" %>

<!DOCTYPE html>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta name="description" content="Starbus APSTS online ticket booking service of Arunachal Pradesh State Transport Services, Govt of Arunachal Pradesh" />
    <meta name="author" content="Starbus* MSTS" />
    <meta name="keywords" content="Starbus apsts, APSTS, Arunachal Pradesh State Transport Services, Book Ticket ,Cancel Ticket , Govt of Arunachal Pradesh , Online Bus Services, Seatavailablity, Bus Pass, Agent booking, Common service Center Booking" />
    <title>Starbus* MSTS</title>
    <!-- Favicon -->
    <link rel="icon" href="Logo/Favicon.png" type="image/png" />
    <!-- Fonts -->
    <!-- Icons -->
    <link rel="stylesheet" href="assets/vendor/nucleo/css/nucleo.css" type="text/css" />
    <link rel="stylesheet" href="assets/vendor/@fortawesome/fontawesome-free/css/all.min.css" type="text/css" />
    <!-- Page plugins -->
    <!-- Argon CSS -->
    <link rel="stylesheet" href="assets/css/argon.css?v=1.2.0" type="text/css" />
    <link href="css/travelllerStepProgressBar.css" rel="stylesheet" />
    <script type="text/javascript">

        $(document).ready(function () {

            $("#tbOTP").keypress(function (e) {
                //if the letter is not digit then display error and don't type anything
                if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                    return false;
                }
            });

            $("#tbName").keypress(function (e) {
                var key = e.keyCode;
                if (key >= 48 && key <= 57) {
                    return false;
                }
            });
        });
    </script>

    <style>
        .input-group-prepend .btn, .input-group-append .btn {
            position: relative;
            z-index: 0;
        }
            #ImgDepartmentLogo {
            background-color: white !important;
            border-radius: 50% !important;
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
    </script>
    <script>
        function onlyNumberKey(evt) {
            // Only ASCII character in that range allowed
            var ASCIICode = (evt.which) ? evt.which : evt.keyCode
            if (ASCIICode > 31 && (ASCIICode < 48 || ASCIICode > 57))
                return false;
            return true;
        };

        function onlyNumberMobile(e) {
            e.target.value = e.target.value.replace(/[^\d]/g, '');
            return false;
        };

        function onlyName(evt) {
            var ASCIICode = (evt.which) ? evt.which : evt.keyCode
            if ((ASCIICode == 32) || (ASCIICode > 64 && ASCIICode < 91) || (ASCIICode > 96 && ASCIICode < 123))
                return true;
            return false;
        };

        function onlyNameMobile(evt) {
            var key = evt.key;

            // Check if the pressed key is a space or an alphabetical character
            if (key === " " || /^[a-zA-Z]$/.test(key)) {
                // Allow the input for space or alphabetical characters
                return true;
            }

            // Prevent input for other characters
            evt.preventDefault();
            return false;
        };


    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <!-- Sidenav -->
        <div class="top-bg">
            <div class="container-fluid ">
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-lg-6 pt11">
                            <a href="#" style="font-size: 20px;">StarBus* <span class="sty">Govt. of India</span></a><asp:Image runat="server" ID="ImgDepartmentLogo" Style="width: 55px; height: 45px;" />
                            <span class="utc"><a href="#" class=" pt12">
                                <asp:Label runat="server" ID="lblDeptName"> </asp:Label>
                                <span class="sty1">
                                    <asp:Label runat="server" ID="lblversion"></asp:Label></span></a></span>
                        </div>
                        <div class="col-lg-6 pt d-block d-sm-none d-md-none d-lg-none">
                            <a href="Home.aspx" class="home"><i class="fa fa-home"></i></a>
                        </div>
                        <div class="col-lg-6 pt d-none d-sm-block d-md-block d-lg-block">
                            <a href="Home.aspx" class="home"><i class="fa fa-home"></i></a>
                            <a href="Helpdesk.aspx" class="org1"><i class="fa fa-phone-volume"></i>Helpdesk</a>
                            <a href="traveller/trvlLogin.aspx" class="org2"><i class="fa fa-user"></i>Traveller Login</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:Panel ID="pnlLayout" runat="server" Visible="true">
            <asp:HiddenField ID="hfIsRoundTrip" runat="server" />
            <asp:HiddenField ID="hfSeatStructUp" runat="server" />
            <asp:HiddenField ID="hfSeatStructDown" runat="server" />
            <asp:HiddenField ID="hfCustomerData" runat="server" />
            <asp:HiddenField ID="hfCategoryList" runat="server" />
            <asp:HiddenField ID="hfNoOfPassanger" runat="server" />
            <asp:HiddenField ID="hidtoken" runat="server" />
            <asp:HiddenField ID="hdnname" runat="server" />
            <asp:HiddenField ID="hdnage" runat="server" />
            <asp:HiddenField ID="hdngender" runat="server" />
            <asp:HiddenField ID="hdngender1" runat="server" />
            <asp:HiddenField ID="hdnchkvalue" runat="server" />
            <asp:HiddenField ID="hdncheck" runat="server" />
            <asp:HiddenField ID="hfPassangertype" runat="server" />
            <asp:HiddenField ID="hfTotalFareDown" Value="0" runat="server" />
            <asp:HiddenField ID="hdfare" Value="0" runat="server" />
            <div class="container-fluid my-3">
                <div class="row shadow mb-2">
                    <div class="col-10 offset-1 pt-3">
                        <ul id="progressbar">
                            <li class="text-center active" id="search_pr"><strong>Search</strong></li>
                            <li class="text-center active" id="seat_pr"><strong>Seat Selection</strong></li>
                            <li class="text-center active" id="confirm_pr"><strong>Confirmation</strong></li>
                            <li class="text-center" id="payment_pr"><strong>Payment</strong></li>
                            <li class="text-center" id="status_pr"><strong>Finish</strong></li>
                        </ul>
                    </div>
                </div>
                <div class="card">
                    <div class="row">
                        <div class="col-lg-3 py-3" style="background: #f3fff1;">
                            <h3 class="mb-1 text-center">You are booking for</h3>
                            <p class="mb-0 text-left" style="font-size: 13px;">
                                <span class="text-muted">Source </span>
                                <br />
                                <asp:Label ID="lblFromStationName" runat="server" Text="NA" CssClass="h4 text-uppercase"></asp:Label><br />
                                <span class="text-muted">Destination</span><br />
                                <asp:Label ID="lblToStationName" runat="server" Text="NA" CssClass="h4 text-uppercase"></asp:Label><br />
                                <span class="text-muted">Service </span>
                                <br />
                                <asp:Label ID="lblServiceType" runat="server" Text="NA" CssClass="h4 text-uppercase"></asp:Label><br />
                                <span class="text-muted">Date </span>
                                <br />
                                <asp:Label ID="lblDateTime" runat="server" Text="NA" CssClass="h4 text-uppercase"></asp:Label><br />
                                <span class="text-muted">Boarding  </span>
                                <br />
                                <asp:Label ID="lblBoarding" runat="server" Text="NA" CssClass="h4 text-uppercase"></asp:Label>
                            </p>
                            <h3 class="mb-1 mt-2">Passengers</h3>
                            <asp:ListView ID="gvPassengers" runat="server">
                                <ItemTemplate>
                                    <p class="mb-0 text-left" style="font-size: 13px;">
                                        <span class="font-weight-600">Seat No. <%# Eval("seatno") %></span> :
                                                    <%# Eval("travellername") %>, <%# Eval("travellergender") %>, <%# Eval("travellerage") %> Year
                                    </p>
                                </ItemTemplate>
                            </asp:ListView>
                            <h3 class="mb-1 mt-2">Total Fare</h3>
                            <p class="mb-0 text-left" style="font-size: 13px;">

                                <span class="font-weight-bold">
                                    <asp:Label ID="lblFareAmt" runat="server" Text="NA"></asp:Label>
                                    <i class="fa fa-rupee-sign"></i>
                                </span>
                            </p>
                        </div>
                        <div class="col-lg-9 py-3 pl-3" style="min-height: 50vh;">
                            <p class="mb-0 text-left" style="font-size: 12pt;">For e-Verification, please keep ready a valid and working mobile number</p>
                            <p class="mb-0 text-left" style="font-size: 12pt;">For receiving e-Ticket on eMail, please keep ready a valid and working email id.</p>
                            <p>
                                <asp:Label ID="lblmsg" runat="server" Style="color: red;"></asp:Label></p>
                            <div class="row pt-3">
                                <div class="col-md-3">
                                    <div class="form-group p-0" style="font-size: 13px;">
                                        <span class="form-label">Mobile No <i class="fa fa-info-circle text-gray ml-1" data-toggle="tooltip" data-placement="top" title="Keep ready mobile no for e-verification. "></i></span>
                                        <asp:TextBox ID="tbMobileNo" runat="server" CssClass="form-control form-control-sm" Style="text-transform: uppercase"
                                            placeholder="Mobile No" MaxLength="10" onkeypress="return onlyNumberKey(event)"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group p-0" style="font-size: 13px;">
                                        <span class="form-label">Email Id<span style="color: #c6c2c2;"> (Optional)</span> <i class="fa fa-info-circle text-gray ml-1" data-toggle="tooltip" data-placement="top" title="Enter your email id to receive ticket on Email"></i></span>
                                        <asp:TextBox ID="tbEmailid" runat="server" CssClass="form-control form-control-sm"
                                            placeholder="Email Id" MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row align-items-center pt-5">
                                <div class="col-xl-12 text-right pr-3">
                                    <asp:LinkButton ID="lbtnCancel" runat="server" CssClass="btn btn-danger btn-icon" OnClick="lbtnCancel_Click"><i class="fa fa-times"></i> Cancel</asp:LinkButton>
                                    <asp:LinkButton ID="lbtnProceed" runat="server" CssClass="btn btn-icon btn-success" OnClick="lbtnProceed_Click">
                                            <i class="fa fa-check"></i> Proceed
                                    </asp:LinkButton>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>

        </asp:Panel>
        <asp:Panel ID="pnlOTP" runat="server" Visible="false" Width="100%" Style="top: 0 !important; position: absolute; left: 0; background-color: #000000cf;">
            <div class="container-fluid my-4">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="modal-dialog modal-dialog-centered" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h6 class="modal-title" id="modal-title-default">Mobile No. Verification</h6>
                                </div>
                                <div class="modal-body py-2">
                                    <p>
                                        <asp:Label ID="lblOtpMsg" runat="server" Text="Coupons"></asp:Label>
                                    </p>
                                    <div>
                                        <asp:TextBox ID="tbName" runat="server" CssClass="form-control text-lg text-uppercase mb-3" type="Search" placeholder="Enter Name" MaxLength="50" AutoComplete="off">
                                        </asp:TextBox>
                                    </div>
                                    <div>
                                        <asp:TextBox ID="tbOTP" runat="server" MaxLength="6" CssClass="form-control text-lg text-uppercase mb-3" type="Search" placeholder="Enter OTP" AutoComplete="off"></asp:TextBox>
                                    </div>
                                    <div class="row w-100">
                                        <div class="col-lg-6 ">
                                            <asp:TextBox ID="tbcaptchacode" runat="server" MaxLength="6" CssClass="form-control text-lg text-uppercase" type="Search" placeholder="Enter Text" AutoComplete="off"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-6 ">
                                            <div class="form-group w-100">
                                                <div class="input-group">
                                                    <img alt="Visual verification" title="Please enter the security code as shown in the image."
                                                        src="CaptchaImage.aspx" style="width: 80%; border: 1px solid #b2f0be; border-radius: 5px 0px 0px 5px;" />
                                                    <div class="input-group-append">
                                                        <asp:LinkButton runat="server" ID="lbtnRefresh" CssClass=" btn btn-outline-primary p-2 btn-refresh" OnClick="lbtnRefresh_Click"><i class="fa fa-recycle" ></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <div class="row w-100">
                                        <div class="col-lg-6">
                                            <asp:LinkButton ID="lbtnClosepnlOTP" runat="server" CssClass="btn btn-link" OnClick="lbtnClosepnlOTP_Click" data-toggle="tooltip" data-placement="top" title="Cancel booking process and back to home.">Cancel</asp:LinkButton>
                                        </div>
                                        <div class="col-lg-6 text-right">
                                            <asp:LinkButton ID="lbtnVerifypnlOTP" runat="server" CssClass="btn btn-success" OnClick="lbtnVerifypnlOTP_Click">Verify & Proceed</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <div class="modal fade" id="CPModal" tabindex="-1" role="dialog" aria-labelledby="CPModalLabel"
            aria-hidden="true">
            <div class="modal-dialog  modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <div class="col-md-10">
                            <h3 class="m-0">Cancellation Policy</h3>
                        </div>
                        <div class="col-md-2 text-right">
                            <a usesubmitbehavior="false" data-dismiss="modal" tooltip="Close" style="color: Red; cursor: pointer;"><i class="fa fa-times fa-2x"></i></a>
                        </div>
                    </div>
                    <div class="modal-body">
                        <ul class="list-group" style="font-size: 13px; font-family: verdana; color: #b10021;">
                            <li class="list-group-item">Cancellation/Refund/Rescheduling Ticket booked through Online,
                                    refund will be done to their respective Credit Cards/Debit Cards/Bank Accounts according
                                    to the Bank procedure. No refund will be done at UTC ticket booking counters
                                    
                            </li>
                            <li class="list-group-item">
                                <asp:Label ID="lblcancellationpolicy" runat="server"></asp:Label>
                            </li>
                            <li class="list-group-item">Cancellation is not allowed after Up to 2 hr before Schedule
                                    service start time at origin of the bus. From the date of journey.</li>
                            <li class="list-group-item">Reservation Fee is non-refundable except in case of 100%
                                    cancellation of tickets, if the service is cancelled by UTC for operational or any
                                    other reasons.</li>
                            <li class="list-group-item">Passengers will be given normally in one month, after the
                                    cancellation of ticket or receipt of e-mail. If refunds are delayed more than a
                                    month, passengers may contact helpline telephone number at 8476007605 E-Mail help[dot]UTConline[at]gmail[dot]com.</li>
                            <li class="list-group-item">Payment Gateway Service charges will not be refunded for
                                    service cancellation/ failure transactions in e-ticketing.</li>
                            <li class="list-group-item">Partial cancellation is allowed for which cancellation terms
                                    & conditions will apply.</li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade bd-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body overflow-auto">
                        <asp:Label ID="lblTermsConditions" runat="server"></asp:Label>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
        <!-- Argon Scripts -->
        <!-- Core -->
        <script src="assets/js/jquery-n.js"></script>
        <script src="assets/vendor/jquery/dist/jquery.min.js"></script>
        <script src="assets/js/jquery-ui.js"></script>
        <script src="assets/vendor/bootstrap/dist/js/bootstrap.bundle.min.js"></script>
        <script src="assets/vendor/js-cookie/js.cookie.js"></script>
        <script src="assets/vendor/jquery.scrollbar/jquery.scrollbar.min.js"></script>
        <script src="assets/vendor/jquery-scroll-lock/dist/jquery-scrollLock.min.js"></script>
        <!-- Optional JS -->
        <script src="assets/vendor/chart.js/dist/Chart.min.js"></script>
        <script src="assets/vendor/chart.js/dist/Chart.extension.js"></script>
        <script src="assets/vendor/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js"></script>
        <!-- Argon JS -->
        <script src="assets/js/argon.js?v=1.2.0"></script>

    </form>
</body>
</html>
