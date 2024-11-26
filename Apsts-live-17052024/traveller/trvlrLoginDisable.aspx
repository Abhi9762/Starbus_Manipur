<%@ Page Language="C#" AutoEventWireup="true" CodeFile="trvlrLoginDisable.aspx.cs" Inherits="traveller_trvlrLoginDisable" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="Start your development with a Dashboard for Bootstrap 4.">
    <meta name="author" content="Creative Tim">
    <title>OIOB-StarBus ver 5.0</title>
    <!-- Favicon -->
    <link rel="icon" href="../assets/img/brand/favicon.png" type="image/png">

    <!-- Icons -->
    <link href="../assets/vendor/nucleo/css/nucleo.css" rel="stylesheet" />
    <link href="../assets/vendor/@fortawesome/fontawesome-free/css/all.min.css" rel="stylesheet" />
    <!-- Page plugins -->
    <!-- Argon CSS -->
    <link rel="stylesheet" href="../assets/css/argon.css?v=1.2.0" type="text/css">
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
</head>
<body class="h-100 full-bg1">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="container-fluid full-bg1 h-100">
            <div class="container ">
                <div class="row no-margin">
                    <div class="col-lg-12">
                        <div class="row">                           
                            <div class="col-lg-6 pt11">
                                <a href="#">StarBus<sup><i class="fa fa-sm fa-star" style="font-size: 12px;"></i></sup> <span class="sty">Govt. of India</span></a><asp:Image runat="server" ID="ImgDepartmentLogo" Style="width: 55px; height: 45px;" />
                                <span class="utc"><a href="#" class=" pt12">
                                    <asp:Label runat="server" ID="lblDeptName"> </asp:Label>
                                    <span class="sty1">
                                        <asp:Label runat="server" ID="lblversion"></asp:Label></span></a></span>
                                </div>
                             <div class="col-lg-6 pt">For Any Help/Query contact to <a href="../helpdesk.aspx"" class="btn-link text-orange"><i class="fa fa-phone-alt"></i> helpdesk</a> </div>
                        </div>
                        <div class="col-lg-12 mt-3">
                            <div class="row">
                                <div class="col-lg-6 ">
                                    <div class="shadow1" style="min-height:80vh;">
                                        <div id="carousel-example-144" class="carousel slide " data-ride="carousel">
                                            <!--Indicators-->
                                            <ol class="carousel-indicators">
                                                <li data-target="#carousel-example-144" data-slide-to="0" class="active"></li>
                                                <li data-target="#carousel-example-144" data-slide-to="1" class=""></li>
                                                <li data-target="#carousel-example-144" data-slide-to="2" class=""></li>
                                            </ol>
                                            <!--/.Indicators-->
                                            <!--Slides-->
                                            <div class="carousel-inner" role="listbox">
                                                <!--First slide-->
                                                <div class="carousel-item text-center active">
                                                    <div class="pp5">
                                                        Travel with safety
                                                            <br>
                                                        travel with utc
                                                    </div>
                                                    <div>
                                                        <img src="../assets/img/mask.png">
                                                    </div>
                                                    <div class="no-c11">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Lorem ipsum dolor sit amet, consectetur adipiscing elit ipsum dolor sit amet, consectetur adipiscing elit. Lorem ipsum dolor sit amet, consectetur adipiscing elit..</div>
                                                </div>
                                                <!--/First slide-->
                                                <!--Second slide-->
                                                <div class="carousel-item text-center">
                                                    <div class="pp5">
                                                        Travel with safety
                                                            <br>
                                                        travel with utc
                                                    </div>
                                                    <div>
                                                        <img src="../assets/img/mask.png">
                                                    </div>
                                                    <div class="no-c11">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Lorem ipsum dolor sit amet, consectetur adipiscing elit ipsum dolor sit amet, consectetur adipiscing elit. Lorem ipsum dolor sit amet, consectetur adipiscing elit..</div>
                                                </div>
                                                <!--/Second slide-->
                                                <!--Third slide-->
                                                <div class="carousel-item text-center">
                                                    <div class="pp5">
                                                        Travel with safety
                                                            <br>
                                                        travel with utc
                                                    </div>
                                                    <div>
                                                        <img src="../assets/img/mask.png">
                                                    </div>
                                                    <div class="no-c11">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Lorem ipsum dolor sit amet, consectetur adipiscing elit ipsum dolor sit amet, consectetur adipiscing elit. Lorem ipsum dolor sit amet, consectetur adipiscing elit..</div>
                                                </div>
                                                <!--/Third slide-->
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
                                <div class="col-lg-6 ">
                                    <div class="shadow1" style="min-height:80vh;">

                                        <div class="pp5">Services of your account will be resumed very soon.</div>
                                        <div class="text-center">
                                            <img src="../assets/img/coming.png" />
                                        </div>
                                        <div class="no-c1 text-center">We Apologize for the inconvenience caused.</div>

                                        <div class="login-box1 row">
                                            <div class=" col-lg-12 text-center">
                                                <a href="../home.aspx" class="btn btn-default lg-bt">Please Visit Again</a>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="ftc">
                          Powered By <asp:Label ID="lblPoweredBy" runat="server"></asp:Label>
                        </div>
                    </div>
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
