<%@ Page Title="" Language="C#" MasterPageFile="~/mainMasterPage.master" AutoEventWireup="true" CodeFile="BookTicket.aspx.cs" Inherits="BookTicket" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="assets/vendor/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js"></script>
    <script type="text/javascript">



        (function ($) {
            $(document).ready(function () {

                $("[id$=tbFrom]").autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            url: "Home.aspx/searchStations",
                            data: "{'stationText':'" + request.term + "','fromTo':'F'}",
                            dataType: "json",
                            success: function (data) {
                                response(data.d);
                            },
                            error: function (result) {
                                // alert('sdss');
                            }
                        });
                    }
                });
                $("[id$=tbTo]").autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            url: "Home.aspx/searchStations",
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

            });
        })(jQuery);
    </script>
    <style>
        ul {
            list-style-type: square;
        }

        .form-control {
            height: 37px !important;
        }

        .car-wrap .img {
            width: 50%;
            height: 50%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="midd-bg">
        <asp:HiddenField ID="hdmaxdate" runat="server" />
        <%--    <div class="container">
            <div class="row">
                <div class="col-lg-5">
                    <div class="ctx">All e-Ticket cancellation refunds may process through payment gateway, i.e. passenger may get refund amount in their bank account/card /wallet through which they book ticket.</div>

                    <div class="">
                        <!-- Bordered tabs-->
                        <ul id="myTab1" role="tablist" class="nav nav-tabs nav-pills with-arrow flex-column flex-sm-row text-center">
                            <li class="nav-item flex-sm-fill">
                                <a id="home1-tab" data-toggle="tab" href="#home1" role="tab" aria-controls="home1" aria-selected="true" class="nav-link   rounded-0 border active">Reservations </a>
                            </li>
                            <li class="nav-item flex-sm-fill">
                                <a id="profile1-tab" data-toggle="tab" href="#profile1" role="tab" aria-controls="profile1" aria-selected="false" class="nav-link   rounded-0 border">Payment </a>
                            </li>
                            <li class="nav-item flex-sm-fill">
                                <a id="contact1-tab" data-toggle="tab" href="#contact1" role="tab" aria-controls="contact1" aria-selected="false" class="nav-link  rounded-0 border">Cancellations </a>
                            </li>
                            <li class="nav-item flex-sm-fill">
                                <a id="contact1-tab" data-toggle="tab" href="#contact2" role="tab" aria-controls="contact1" aria-selected="false" class="nav-link  rounded-0 border">Refund </a>
                            </li>
                        </ul>
                        <div id="myTab1Content" class="tab-content">
                            <div id="home1" role="tabpanel" aria-labelledby="home-tab" class="tab-pane  show active">
                                <div class="leade ">

                                    <div class="left-bg">Can I cancel the Reservations?</div>
                                    <div class="ctx1">Lorem ipsum dolor sit amet, consectetur adipiscing eliteius mod tempor incididunt ut labore et dolore magna.Lorem ipsum dolor sit amet, consectetur adipiscing eliteius mod tempor incididunt ut </div>
                                    <div class="text-right">
                                        <button type="button" class="btn btn-outline-warning1">User Guide <i aria-hidden="true" class="fa fa-angle-right"></i></button>
                                    </div>

                                    <div class="left-bg">When I cancel my ticket?</div>
                                    <div class="ctx1">Lorem ipsum dolor sit amet, consectetur adipiscing eliteius mod tempor incididunt ut labore et dolore magna.Lorem ipsum dolor sit amet, consectetur adipiscing eliteius mod tempor incididunt ut </div>
                                    <div class="text-right">
                                        <button type="button" class="btn btn-outline-warning1">User Guide <i aria-hidden="true" class="fa fa-angle-right"></i></button>
                                    </div>

                                    <div class="left-bg">How to Reschedule Ticket?</div>
                                    <div class="ctx1">Lorem ipsum dolor sit amet, consectetur adipiscing eliteius mod tempor incididunt ut labore et dolore magna.Lorem ipsum dolor sit amet, consectetur adipiscing eliteius mod tempor incididunt ut </div>
                                    <div class="text-right">
                                        <button type="button" class="btn btn-outline-warning1">User Guide <i aria-hidden="true" class="fa fa-angle-right"></i></button>
                                    </div>

                                </div>

                            </div>
                            <div id="profile1" role="tabpanel" aria-labelledby="profile-tab" class="tab-pane fade">
                                <div class="leade ">

                                    <div class="left-bg">Can I cancel the Payment?</div>
                                    <div class="ctx1">Lorem ipsum dolor sit amet, consectetur adipiscing eliteius mod tempor incididunt ut labore et dolore magna.Lorem ipsum dolor sit amet, consectetur adipiscing eliteius mod tempor incididunt ut </div>
                                    <div class="text-right">
                                        <button type="button" class="btn btn-outline-warning1">User Guide <i aria-hidden="true" class="fa fa-angle-right"></i></button>
                                    </div>

                                    <div class="left-bg">When I cancel my ticket?</div>
                                    <div class="ctx1">Lorem ipsum dolor sit amet, consectetur adipiscing eliteius mod tempor incididunt ut labore et dolore magna.Lorem ipsum dolor sit amet, consectetur adipiscing eliteius mod tempor incididunt ut </div>
                                    <div class="text-right">
                                        <button type="button" class="btn btn-outline-warning1">User Guide <i aria-hidden="true" class="fa fa-angle-right"></i></button>
                                    </div>

                                    <div class="left-bg">How to Reschedule Ticket?</div>
                                    <div class="ctx1">Lorem ipsum dolor sit amet, consectetur adipiscing eliteius mod tempor incididunt ut labore et dolore magna.Lorem ipsum dolor sit amet, consectetur adipiscing eliteius mod tempor incididunt ut </div>
                                    <div class="text-right">
                                        <button type="button" class="btn btn-outline-warning1">User Guide <i aria-hidden="true" class="fa fa-angle-right"></i></button>
                                    </div>

                                </div>
                            </div>
                            <div id="contact1" role="tabpanel" aria-labelledby="contact-tab" class="tab-pane fade  ">
                                <div class="leade ">

                                    <div class="left-bg">Can I cancel the Cancellations?</div>
                                    <div class="ctx1">Lorem ipsum dolor sit amet, consectetur adipiscing eliteius mod tempor incididunt ut labore et dolore magna.Lorem ipsum dolor sit amet, consectetur adipiscing eliteius mod tempor incididunt ut </div>
                                    <div class="text-right">
                                        <button type="button" class="btn btn-outline-warning1">User Guide <i aria-hidden="true" class="fa fa-angle-right"></i></button>
                                    </div>

                                    <div class="left-bg">When I cancel my ticket?</div>
                                    <div class="ctx1">Lorem ipsum dolor sit amet, consectetur adipiscing eliteius mod tempor incididunt ut labore et dolore magna.Lorem ipsum dolor sit amet, consectetur adipiscing eliteius mod tempor incididunt ut </div>
                                    <div class="text-right">
                                        <button type="button" class="btn btn-outline-warning1">User Guide <i aria-hidden="true" class="fa fa-angle-right"></i></button>
                                    </div>

                                    <div class="left-bg">How to Reschedule Ticket?</div>
                                    <div class="ctx1">Lorem ipsum dolor sit amet, consectetur adipiscing eliteius mod tempor incididunt ut labore et dolore magna.Lorem ipsum dolor sit amet, consectetur adipiscing eliteius mod tempor incididunt ut </div>
                                    <div class="text-right">
                                        <button type="button" class="btn btn-outline-warning1">User Guide <i aria-hidden="true" class="fa fa-angle-right"></i></button>
                                    </div>

                                </div>
                            </div>

                            <div id="contact2" role="tabpanel" aria-labelledby="contact-tab" class="tab-pane fade  ">
                                <div class="leade ">


                                    <div class="left-bg">Can I cancel the Refund?</div>
                                    <div class="ctx1">Lorem ipsum dolor sit amet, consectetur adipiscing eliteius mod tempor incididunt ut labore et dolore magna.Lorem ipsum dolor sit amet, consectetur adipiscing eliteius mod tempor incididunt ut </div>
                                    <div class="text-right">
                                        <button type="button" class="btn btn-outline-warning1">User Guide <i aria-hidden="true" class="fa fa-angle-right"></i></button>
                                    </div>

                                    <div class="left-bg">When I cancel my ticket?</div>
                                    <div class="ctx1">Lorem ipsum dolor sit amet, consectetur adipiscing eliteius mod tempor incididunt ut labore et dolore magna.Lorem ipsum dolor sit amet, consectetur adipiscing eliteius mod tempor incididunt ut </div>
                                    <div class="text-right">
                                        <button type="button" class="btn btn-outline-warning1">User Guide <i aria-hidden="true" class="fa fa-angle-right"></i></button>
                                    </div>

                                    <div class="left-bg">How to Reschedule Ticket?</div>
                                    <div class="ctx1">Lorem ipsum dolor sit amet, consectetur adipiscing eliteius mod tempor incididunt ut labore et dolore magna.Lorem ipsum dolor sit amet, consectetur adipiscing eliteius mod tempor incididunt ut </div>
                                    <div class="text-right">
                                        <button type="button" class="btn btn-outline-warning1">User Guide <i aria-hidden="true" class="fa fa-angle-right"></i></button>
                                    </div>

                                </div>
                            </div>

                        </div>
                        <!-- End bordered tabs -->
                    </div>







                </div>

                <div class="col-lg-7">

                    <div class="row">
                        <div class="col-lg-3">
                            <div class="org-bg6">Serch and Book </div>
                        </div>
                        <div class="col-lg-9 no-space">
                            <div class="ctx1">Please select From, To station for departure and arrival Stations. Next select the jounary date.</div>
                        </div>

                    </div>
                    <div class="tk-bg">
                        <div class="row">
                            <div class="col-lg-10 ">
                                <div class="row">
                                    <div class="col-lg-6 ">
                                        <div class="form-group">
                                            <label>From Station</label>
                                            <div class="input-group input-group-alternative">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text"><i class="fa fa-building"></i></span>
                                                </div>
                                                <asp:TextBox ID="tbFrom" runat="server" MaxLength="50" CssClass="form-control pl-0" type="Search" placeholder="From" AutoComplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-6 ">
                                        <div class="form-group">
                                            <label>To Station</label>
                                            <div class="input-group input-group-alternative">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text"><i class="fa fa-building"></i></span>
                                                </div>
                                                <asp:TextBox ID="tbTo" runat="server" MaxLength="50" CssClass="form-control pl-0" type="Search" placeholder="To" AutoComplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row mt-2">
                                    <div class="col-lg-6 ">
                                        <div class="form-group">
                                            <label>Journey Date</label>
                                            <div class="input-group input-group-alternative">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text"><i class="fa fa-calendar-alt"></i></span>
                                                </div>
                                                <asp:TextBox ID="tbDate" runat="server" MaxLength="10" CssClass="form-control pl-0" type="Search" placeholder="DD/MM/YYYY" AutoComplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-6 ">
                                        <div class="form-group">
                                            <label>Service (Optional)</label>
                                            <asp:DropDownList ID="ddlserviretype" runat="server" CssClass="form-control">
                                            </asp:DropDownList>

                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-2 no-space ">
                                <div class="show1 ">
                                    <asp:LinkButton ID="lbtnSearchServices" runat="server" CssClass="btn btn-primary" Style="text-align: center; padding: 30px; padding-left: 10px; padding-right: 10px;"
                                        OnClick="lbtnSearchServices_Click">
                                            Check Availability
                                    </asp:LinkButton>

                                </div>
                            </div>
                        </div>



                    </div>

                    <div class="row">
                        <div class="col-lg-6">
                            <div class="tk-bg">
                                <div class="tx4">Popular <span class="org">Routes</span></div>
                                <asp:Repeater ID="rptRoutes" runat="server" OnItemCommand="rptRoutes_ItemCommand">
                                    <ItemTemplate>
                                        <div class="blu-hd1">
                                            <asp:HiddenField ID="hdfrmstation" runat="server" Value='<%#Eval("from_station_name")%>' />
                                            <asp:HiddenField ID="hdtostation" runat="server" Value='<%#Eval("to_station_name")%>' />
                                            <asp:LinkButton ID="lbtnroute" runat="server"><i class="fa fa-angle-double-right" aria-hidden="true"></i> <%#Eval("from_station_name")%> - <%#Eval("to_station_name")%></asp:LinkButton>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>

                        <div class="col-lg-6">
                            <div class="tk-bg">
                                <div class="tx4">Exclusive <span class="org">Offers</span></div>
                                <div>
                                    <img src="assets/img/offer1.png">
                                </div>
                                <div>
                                    <img src="assets/img/offer2.png">
                                </div>
                            </div>
                        </div>

                    </div>



                </div>
            </div>
        </div>--%>
        <div class="container">
            <div class="row" id="dvbook1" runat="server" visible="false" >
                <div class="col-md-12">
                    <%--<asp:Label ID="Label2" runat="server" CssClass="text text-black-50 font-weight-bold" Text="Search & Book"></asp:Label>--%>
                    <h3 class="mb-1">
                        <div class="tx5">Search & <span class="org">Book</span></div>
                    </h3>
                </div>
            </div>
            <div class="card py-3 px-5" style="box-shadow: 0px 5px 12px -1px rgb(0 0 0 / 6%);" id="dvbook" runat="server" visible="false" >
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group mb-2">
                            <asp:Label ID="lblFromStationHeader" runat="server" Text="From Station" CssClass="label font-weight-light mb-0" Style="font-size: 12px;"></asp:Label>
                            <asp:TextBox ID="tbFrom" runat="server" MaxLength="50" class="form-control form-control-sm"
                                Style="text-transform: uppercase; font-size: 16px;" placeholder="Station Name" autocomplete="off"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group mb-2">
                            <asp:Label ID="lblToStationHeader" runat="server" Text="To Station" CssClass="label font-weight-light mb-0" Style="font-size: 12px;"></asp:Label>
                            <asp:TextBox ID="tbTo" runat="server" MaxLength="50" class="form-control"
                                Style="text-transform: uppercase; font-size: 16px;" placeholder="Station Name" autocomplete="off"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3 mt-1 text-center">
                        <div class="form-group mt-3">
                            <asp:Button ID="btntoday" runat="server" CssClass="btn btn-primary" Text="Today" OnClick="btntoday_Click" />
                            <asp:Button ID="btntomorrow" runat="server" CssClass="btn btn-primary" Text="Tomorrow" OnClick="btntomorrow_Click" />
                        </div>
                    </div>
                    <div class="col-md-3">
                        <%-- <div class="form-group">--%>


                        <%--  <cc1:CalendarExtender ID="cetxtDateSeatAvailability" runat="server" Enabled="True" CssClass="black"
                                Format="dd/MM/yyyy" PopupButtonID="tbDate" PopupPosition="TopRight" TargetControlID="tbDate"></cc1:CalendarExtender>--%>
                        <%-- </div>
                        <br />--%>
                        <asp:Label ID="lblDateHeader" runat="server" Text="Journey Date" CssClass="label font-weight-light mb-0" Style="font-size: 12px;"></asp:Label>
                        <div class="input-group mb-3">
                            <asp:TextBox ID="tbDate" runat="server" MaxLength="10" class="form-control"
                                Style="text-transform: uppercase; font-size: 16px;" placeholder="DD/MM/YYYY" autocomplete="off"></asp:TextBox>
                            <div class="input-group-append">
                                <asp:LinkButton ID="lbtnsearch" runat="server" CssClass="btn btn-danger" OnClick="lbtnsearch_Click"> <i class="fa fa-search"></i> </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <%-- <div class="col-md-1 mt-1">
                        <div class="form-group mt-4">
                         
                        </div>
                    </div>--%>
                </div>
            </div>
            <div class="row mt-3">
                <div class="col-md-7">

                    <h3 class="mb-1">
                        <div class="tx5">Popular  <span class="org">Routes</span></div>
                    </h3>
                    <div class="card px-5" style="box-shadow: 0px 5px 12px -1px rgb(0 0 0 / 6%); min-height: 240px;">
                        <ul>
                            <asp:Repeater ID="rptrTopRoute" runat="server" OnItemCommand="rptrTopRoute_ItemCommand">
                                <ItemTemplate>
                                    <li>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:HiddenField ID="hddfromst" runat="server" Value='<%#Eval("fromst")%>' />
                                                          <asp:HiddenField ID="hddtost" runat="server" Value='<%#Eval("tost")%>'/>

                                                <asp:LinkButton ID="lbtnroute" runat="server" Text='<%#Eval("route_name")%>' ToolTip="Click to go for Bus Services of the route" CssClass="btn"></asp:LinkButton>
                                                <asp:HiddenField ID="hdfrmstation" runat="server" Value='<%#Eval("from_station_name")%>' />
                                                <asp:HiddenField ID="hdtostation" runat="server" Value='<%#Eval("to_station_name")%>' />
                                                 <asp:HiddenField ID="hdfromcity" runat="server" Value='<%#Eval("fromcity")%>' />
                                                          <asp:HiddenField ID="hdtocity" runat="server" Value='<%#Eval("tocity")%>'/>
                                            </div>
                                        </div>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                      <asp:Panel ID="pnlNoRoute" runat="server" Width="100%" Visible="true">
                            <div class="col-lg-12 col-md-12 col-sm-12 text-center">
                                <h2 class="text-muted pt-4">
                                    <asp:Label ID="Label1" runat="server" Text="Popular Routes will be available soon."></asp:Label>
                                </h2>
                            </div>
                        </asp:Panel>
                    </div>

                </div>
                <div class="col-md-5">

                    <h3 class="mb-1">
                        <div class="tx5">Exclusive  <span class="org">Offers</span></div>
                    </h3>
                    <div class="card py-2 px-3" style="box-shadow: 0px 5px 12px -1px rgb(0 0 0 / 6%); min-height: 240px;">
                        <asp:Repeater ID="rptOffers" runat="server" Visible="false" OnItemDataBound="rptOffers_ItemDataBound">
                            <ItemTemplate>
                                <div class="car-wrap rounded ftco-animate fadeInUp ftco-animated text-center pt-2 pb-0">
                                <%--    <asp:Image ID="imgWeb" runat="server" CssClass="img rounded" Style="width: 200px; height: 135px;" />--%>
                                    <div class="text pb-0">
                                        <h6 class="heading mb-0"><%# Eval("COUPONTITLE")%></h6>
                                        <p class="text-black-50 mb-0">Code : <%# Eval("COUPONCODE")%></p>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        <asp:Panel ID="pnlNoOffer" runat="server" Width="100%" Visible="true">
                            <div class="col-lg-12 col-md-12 col-sm-12 text-center">
                                <h2 class="text-muted pt-4">
                                    <asp:Label ID="lblNoOffers" runat="server" Text="Discount/Offers will be available soon."></asp:Label>
                                </h2>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
            <br />

            <div class="row mt-3">
                <div class="col-md-12">

                    <h3 class="mb-1">
                        <div class="tx5">Frequently   <span class="org">Asked Questions?</span></div>
                    </h3>
                    <div class="card py-2 px-5" style="box-shadow: 0px 5px 12px -1px rgb(0 0 0 / 6%); min-height: 240px;">
                        <div class="ctx">Have More Questions? <span class="org"><a href="Helpdesk.aspx">Visit Our Helpdesk </a></span></div>
                        <div class="container">
                            <!-- For demo purpose -->
                            <div class="row mt-3">

                                <asp:Repeater ID="rptfaqcategory" runat="server" OnItemCommand="rptfaqcategory_ItemCommand">
                                    <ItemTemplate>
                                        <div class="col-auto">
                                            <asp:HiddenField ID="hffaqcategodyid" runat="server" Value='<%#Eval("faqid")%>' />
                                            <asp:LinkButton ID="lbtnfaq" runat="server" CssClass="btn btn-sm btn-primary ml-1" CommandName="show"> <%#Eval("faqcategory")%></asp:LinkButton>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                            <div class="row">
                                <div class="col-lg-12 mx-2 my-2 space5">
                                    <!-- Accordion -->
                                    <div id="accordionExample" class="accordion shadow">
                                        <asp:Label ID="lblNoFaq" runat="server" Text="FAQ of this Category will be available very soon."></asp:Label>
                                        <asp:Repeater ID="rptfaq" runat="server">
                                            <ItemTemplate>
                                                <div class="card">
                                                    <div id="headingOne" class=" bg-gray1 shadow-sm border-0">
                                                        <div class="mb-0 "><a href="#" data-toggle="collapse" data-target='#<%#Eval("datatarget")%>' aria-expanded="true" aria-controls='<%#Eval("faq_id")%>' class="d-block position-relative text-dark  collapsible-link py-2"><%#Eval("faqs")%></a></div>
                                                    </div>
                                                    <div id='<%#Eval("datatarget")%>' aria-labelledby="headingOne" data-parent="#accordionExample" class="collapse">
                                                        <div class="card-body ">
                                                            <div class="hd-text25"><%#Eval("faq_answer")%></div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <!-- Accordion item 1 -->
                                    </div>
                                </div>
                            </div>
                            <%-- <div class="row">
                                    <div class="col-lg-12 mx-auto my-4 space5">
                                        <!-- Accordion -->
                                        <div id="accordionExample" class="accordion shadow">
                                            <asp:Label ID="lblNoFaq" runat="server" Text="FAQ of this Category will be available very soon."></asp:Label>
                                            <asp:Repeater ID="rptfaq" runat="server">
                                                <ItemTemplate>
                                                    <div class="card">
                                                        <div id="headingOne" class=" bg-gray1 shadow-sm border-0">
                                                            <div class="mb-0 "><a href="#" data-toggle="collapse" data-target='<%#Eval("faq_id")%>' aria-expanded="true" aria-controls='<%#Eval("faq_id")%>' class="d-block position-relative text-dark  collapsible-link py-2"><%#Eval("faqs")%></a></div>
                                                        </div>
                                                        <div id='<%#Eval("faq_id")%>' aria-labelledby="headingOne" data-parent="#accordionExample" class="collapse">
                                                            <div class="card-body ">
                                                                <div class="hd-text25"><%#Eval("faq_answer")%></div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <!-- Accordion item 1 -->
                                        </div>
                                    </div>
                                </div>--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

