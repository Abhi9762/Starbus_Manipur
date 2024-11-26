<%@ Page Language="C#" AutoEventWireup="true" CodeFile="trvlLoginOTP.aspx.cs" Inherits="traveller_trvlLoginOTP" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta name="description" content="Starbus APSTS online ticket booking service of Arunachal Pradesh State Transport Services, Govt of Arunachal Pradesh" />
    <meta name="author" content="Starbus* APSTS" />
    <meta name="keywords" content="Starbus apsts, APSTS, Arunachal Pradesh State Transport Services, Book Ticket ,Cancel Ticket , Govt of Arunachal Pradesh , Online Bus Services, Seatavailablity, Bus Pass, Agent booking, Common service Center Booking" />
    <title>Starbus* APSTS</title>
    <!-- Favicon -->
    <link rel="icon" href="../Logo/Favicon.png" type="image/png" />

    <!-- Icons -->
    <link href="../assets/vendor/nucleo/css/nucleo.css" rel="stylesheet" />
    <link href="../assets/vendor/@fortawesome/fontawesome-free/css/all.min.css" rel="stylesheet" />
    <!-- Page plugins -->
    <!-- Argon CSS -->
    <link rel="stylesheet" href="../assets/css/argon.css?v=1.2.0" type="text/css" />
    <link href="../assets/css/flaticon.css" rel="stylesheet" />
    <script type="text/javascript">
        function DisableBackButton() {
            window.history.forward()
        }
        DisableBackButton();
        window.onload = DisableBackButton;
        window.onpageshow = function (evt) { if (evt.persisted) DisableBackButton() }
        window.onunload = function () { void (0) }

    </script>
    <script type="text/javascript">
        window.onload = function () {
            resendfunc();
        };
        function resendfunc() {
            var timeleft = 120;
            var downloadTimer = setInterval(function () {
                var resendcount = document.getElementById("hfResendCount").value;
                if (resendcount < 2) {
                    timeleft--;
                    document.getElementById("countdowntimer").textContent = "you can resend OTP after " + timeleft + " seconds";//+ ", Resend Count2 - " + myHidden;
                    if (timeleft <= 0) {
                        clearInterval(downloadTimer);
                        document.getElementById("countdowntimer").textContent = "";
                        document.getElementById("lbtnResendOTP").style.display = "block";
                    }
                    else {
                        document.getElementById("lbtnResendOTP").style.display = "none";
                    }
                }
                else {
                    document.getElementById("countdowntimer").textContent = "you have crossed resend OTP limit.";
                    document.getElementById("lbtnResendOTP").style.display = "none";
                }
            }, 1000);
        }
    </script>

    <style>
        .vertical-center {
            min-height: 100%; /* Fallback for browsers do NOT support vh unit */
            min-height: 100vh; /* These two lines are counted as one :-)       */
            display: flex;
            align-items: center;
        }

        #ImgDepartmentLogo {
            background-color: white !important;
            border-radius: 50% !important;
        }
    </style>
</head>
<body class="h-100 full-bg1">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="container-fluid full-bg1 h-100">
            <div class="container ">
                <div class="row no-margin">
                    <div class="col-lg-12">
                        <div class="row">
                            <div class="col-lg-8 pt11">
                                <a href="../home.aspx" style="font-size: 20px">
                                    <asp:Image runat="server" ID="ImgDepartmentLogo" Style="width: 55px; height: 45px;" /></a>
                                <span class="utc"><a href="../home.aspx">
                                    <asp:Label runat="server" Font-Size="20px" ID="lblDeptName"> </asp:Label>
                                    <span class="sty1">Version
                                    <asp:Label runat="server" ID="lblversion"></asp:Label></span></a></span>
                            </div>
                            <div class="col-lg-4 pt">For Any Help/Query contact to <a href="../helpdesk.aspx" class="btn-link text-orange"><i class="fa fa-phone-alt"></i>helpdesk</a> </div>
                        </div>
                        <div class="col-lg-12 mt-6">
                            <div class="row">
                                <div class="col-lg-6 ">
                                    <div class="shadow1 vertical-center" style="min-height: 75vh;">
                                        <div style="display: block; width: 100%;">
                                            <div id="carousel-example-144" class="carousel slide " data-ride="carousel">
                                                <!--Indicators-->
                                                <ol class="carousel-indicators">
                                                    <li data-target="#carousel-example-144" data-slide-to="0" class="active"></li>
                                                    <li data-target="#carousel-example-144" data-slide-to="1" class=""></li>
                                                    <li data-target="#carousel-example-144" data-slide-to="2" class=""></li>
                                                    <li data-target="#carousel-example-144" data-slide-to="3" class=""></li>
                                                </ol>
                                                <!--/.Indicators-->
                                                <!--Slides-->
                                                <div class="carousel-inner" role="listbox">
                                                    <!--First slide-->
                                                    <div class="carousel-item text-center active">
                                                        <div class="pp5 pb-4" style="text-transform: none;">
                                                            What's New
                                                        </div>
                                                        <div class="mt-4 text-primary">
                                                            <i class="fa fa-wallet"></i>WALLET
                                                        </div>
                                                        <div class="no-c11">For ease of booking traveler, wallet facility is provided to our travelers. Now you can top up your wallet once and use it whenever you are booking a ticket on portal/mobile app.</div>
                                                    </div>
                                                    <!--/First slide-->
                                                    <!--Second slide-->
                                                    <div class="carousel-item text-center">
                                                        <div class="pp5 pb-4" style="text-transform: none;">
                                                            What's New
                                                        </div>
                                                        <div class="mt-4 text-primary">
                                                            <i class="fa fa-boxes"></i>OFFERS
                                                        </div>
                                                        <div class="no-c11">Offers and Discounts on using online services may be provided time to time. Offers & discounts are subjected to administrative decision and cannot be challenged.</div>
                                                    </div>
                                                    <!--/Second slide-->
                                                    <!--Third slide-->
                                                    <div class="carousel-item text-center">
                                                        <div class="pp5 pb-4" style="text-transform: none;">
                                                            What's New
                                                        </div>
                                                        <div class="mt-4 text-primary">
                                                            <i class="fa fa-map-marked-alt"></i>TRACK BUS
                                                        </div>
                                                        <div class="no-c11">Track my bus feature enables our on-the-go customers and their families to keep track of the bus location. You can track your bus on a map and use the information to plan your commute to the boarding point and to get off at the right stop. Family members and friends can also check the bus location to coordinate pick-ups and rest assured about your safety.</div>
                                                    </div>
                                                    <!--/Third slide-->
                                                    <!--Forth slide-->
                                                    <div class="carousel-item text-center">
                                                        <div class="pp5 pb-4" style="text-transform: none;">
                                                            What's New
                                                        </div>
                                                        <div class="mt-4 text-primary">
                                                            <i class="fa fa-mobile-alt"></i>E-TICKET
                                                        </div>
                                                        <div class="no-c11">Now e-ticket, which is digital ticket, will be valid to travel in our buses. Permission to travel will be subjected to verification of identity of the travelers by APSTS staff.</div>
                                                    </div>
                                                    <!--/Forth slide-->
                                                </div>
                                                <!--/.Slides-->
                                                <!--Controls-->

                                                <!--/.Controls-->
                                            </div>
                                            <div class="row">
                                                <div class="col-lg-12 pt-3">
                                                    <asp:Panel ID="pnlOffers" runat="server">
                                                        <asp:ImageButton ID="imgOffer" runat="server" CssClass="w-100" Style="border-radius: 7px; height: 100px;" OnClick="imgOffer_Click" />
                                                    </asp:Panel>
                                                    <asp:Panel ID="pnlNoOffer" runat="server" Style="min-height: 100px">
                                                    </asp:Panel>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <div class="col-lg-6 ">
                                    <div class="shadow1 vertical-center" style="min-height: 75vh;">
                                        <div style="display: block; width: 100%;">
                                            <div class="ppv pb-4" style="text-transform: none;">e-Verification</div>
                                            <div class="text-dark text-center mt-4">
                                                <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                            </div>
                                            <div class="login-box1 row pt-3">
                                                <asp:Panel ID="pnlName" runat="server" Visible="true" Width="100%">
                                                    <div class="form-group w-100">
                                                        <div class="input-group input-group-alternative">
                                                            <div class="input-group-prepend">
                                                                <span class="input-group-text"><i class="fa fa-user"></i></span>
                                                            </div>
                                                            <asp:TextBox ID="tbName" runat="server" CssClass="form-control text-lg text-uppercase" type="Search" placeholder="Name" MaxLength="50" AutoComplete="off">
                                                            </asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Custom" ValidChars="abcdefghijklmnopqrstuvwxyz ABCDEFGHIJKLMNOPQRSTUVWXYZ"
                                                                TargetControlID="tbName" />
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                                <div class="form-group w-100 mt-3">
                                                    <div class="input-group input-group-alternative">
                                                        <div class="input-group-prepend">
                                                            <span class="input-group-text"><i class="fa fa-lock"></i></span>
                                                        </div>
                                                        <asp:TextBox ID="tbOTP" runat="server" MaxLength="6" CssClass="form-control text-lg" type="Search" placeholder="Enter OTP" AutoComplete="off"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                                                            TargetControlID="tbOTP" />
                                                    </div>
                                                </div>

                                                <div class="row w-100">
                                                    <div class="col-lg-6 ">
                                                        <div class="form-group w-100 mt-3 mb-5">
                                                            <div class="input-group input-group-alternative">
                                                                <div class="input-group-prepend">
                                                                    <span class="input-group-text"><i class="fa fa-credit-card"></i></span>
                                                                </div>
                                                                <asp:TextBox ID="tbcaptchacode" runat="server" MaxLength="6" CssClass="form-control text-lg text-uppercase" type="Search" placeholder="Enter Text" AutoComplete="off"></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers"
                                                                    TargetControlID="tbOTP" />

                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6 ">
                                                        <div class="form-group w-100 mt-3">
                                                            <div class="input-group" style="box-shadow: 0 1px 3px rgb(50 50 93 / 15%), 0 1px 0 rgb(0 0 0 / 2%);">
                                                                <img alt="Visual verification" title="Please enter the security code as shown in the image."
                                                                    src="../CaptchaImage.aspx" style="width: 80%; height: 40px;" />
                                                                <div class="input-group-append">
                                                                    <asp:LinkButton runat="server" ID="lbtnRefresh" CssClass=" btn btn-outline-primary p-2 btn-refresh" OnClick="lbtnRefresh_Click"><i class="fa fa-recycle" ></i></asp:LinkButton>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class=" col-lg-12 text-center">
                                                    <asp:LinkButton ID="lbtnVerify" runat="server" CssClass="btn btn-default lg-bt" OnClick="lbtnVerify_Click">Verify</asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="text-black-50 text-center">
                                                <p class="mb-0 mt-4">If you still haven’t received the OTP</p>
                                                <p>
                                                    <span id="countdowntimer"></span>
                                                    <asp:LinkButton ID="lbtnResendOTP" runat="server" CssClass="btn btn-link py-0" Style="display: none;" OnClick="lbtnResendOTP_Click">Resend OTP</asp:LinkButton>
                                                    <asp:HiddenField ID="hfResendCount" runat="server" Value="0" />
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="ftc">
                            <span class="float-left" style="font-size: 20px;">StarBus*<br />
                                <span class="" style="font-size: 14px;">Govt. of India</span>
                            </span>
                            Powered By
                            <img src="../assets/img/nic2.png" style="height: 35px;" />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <cc1:ModalPopupExtender ID="mpOtpLimit" runat="server" PopupControlID="pnlDhaba"
                        CancelControlID="lbtnCancempOtpLimit" TargetControlID="btnOpenmpOtpLimit" BackgroundCssClass="modalBackground">
                    </cc1:ModalPopupExtender>
                    <asp:Panel ID="pnlDhaba" runat="server" Style="position: fixed;">
                        <div class="card" style="min-width: 350px; max-width: 850px;">
                            <div class="card-header">
                                <h4 class="card-title text-uppercase mb-0">
                                    <asp:Label ID="lblDhabaHeaderTripDetails" runat="server"></asp:Label>
                                    <asp:Label ID="lblDhabaStrpID" runat="server" Visible="false"></asp:Label>
                                </h4>
                            </div>
                            <div class="card-body text-left pt-0 overflow-auto">
                                <h6 class="text-uppercase text-dark mb-0 border-bottom text-sm py-1">Please Note</h6>
                                <asp:Label ID="lblMsg" runat="server" CssClass="text-muted font-weight-800 text-sm"></asp:Label>
                            </div>
                            <div class="card-footer text-right p-2">
                                <asp:LinkButton ID="lbtnSaveDhaba" runat="server" OnClick="lbtnRedirect_Click" CssClass="btn btn-success btn-sm"> <i class="fa fa-check"></i> OK </asp:LinkButton>
                            </div>
                        </div>
                        <div style="visibility: hidden;">
                            <asp:Button ID="btnOpenmpOtpLimit" runat="server" Text="" />
                            <asp:Button ID="lbtnCancempOtpLimit" runat="server" Text="" />
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </form>
    <!-- Argon Scripts -->
    <!-- Core -->
    <script src="../assets/vendor/jquery/dist/jquery.min.js"></script>
    <script src="../assets/vendor/bootstrap/dist/js/bootstrap.bundle.min.js"></script>
    <script src="../assets/vendor/js-cookie/js.cookie.js"></script>
    <script src="../assets/vendor/jquery.scrollbar/jquery.scrollbar.min.js"></script>
    <script src="../assets/vendor/jquery-scroll-lock/dist/jquery-scrollLock.min.js"></script>
    <!-- Optional JS -->
    <script src="../assets/vendor/chart.js/dist/Chart.min.js"></script>
    <script src="../assets/vendor/chart.js/dist/Chart.extension.js"></script>
    <!-- Argon JS -->
    <script src="../assets/js/argon.js?v=1.2.0"></script>
</body>
</html>
