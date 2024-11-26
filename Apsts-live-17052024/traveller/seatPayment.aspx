<%@ Page Title="" Language="C#" MasterPageFile="~/traveller/trvlrMaster.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeFile="seatPayment.aspx.cs" Inherits="traveller_seatPayment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../css/travelllerStepProgressBar.css" rel="stylesheet" />
    <style>
        .ModalPopupBackground {
            height: 100%;
            background-color: #EBEBEB;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }

        input[type="checkbox"] {
            width: 15px; /*Desired width*/
            height: 15px; /*Desired height*/
        }

        input[type="radio"] {
            width: 15px; /*Desired width*/
            height: 15px; /*Desired height*/
        }

        .input-group-prepend .btn, .input-group-append .btn {
            position: relative;
            z-index: 0;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
        <asp:HiddenField ID="hidtoken" runat="server" />
    <div class="container-fluid py-3">

        <div class="row shadow mb-2">
            <div class="col-10 offset-1 pt-3">
                <ul id="progressbar">
                    <li class="text-center active" id="search_pr"><strong>Search</strong></li>
                    <li class="text-center active" id="seat_pr"><strong>Seat Selection</strong></li>
                    <li class="text-center active" id="confirm_pr"><strong>Confirmation</strong></li>
                    <li class="text-center active" id="payment_pr"><strong>Payment</strong></li>
                    <li class="text-center" id="status_pr"><strong>Finish</strong></li>
                </ul>
            </div>
        </div>

        <div class="card">
            <div class="row">
                <div class="col-lg-3 py-3" style="background: #f3fff1;">
                    <h3 class="mb-1 text-center">You are booking for</h3>
                    <p class="mb-0 text-left" style="font-size: 13px;">
                        <span class="text-muted">Source </span><br />
                        <asp:Label ID="lblFromStation" runat="server" CssClass="text-uppercase h4" Text="NA"></asp:Label><br />
                        <span class="text-muted">Destination</span><br />
                        <asp:Label ID="lblToStation" runat="server" CssClass="text-uppercase h4" Text="NA"></asp:Label><br />
                        <span class="text-muted">Service </span><br />
                        <asp:Label ID="lblServiceType" runat="server" CssClass="text-uppercase h4" Text="NA"></asp:Label><br />
                        <span class="text-muted">Date </span><br />
                        <asp:Label ID="lblDate" runat="server" Text="NA" CssClass="text-uppercase h4">
                        </asp:Label><asp:Label ID="lblDeparture" runat="server" CssClass="text-uppercase pl-2 h4" Text="NA"></asp:Label><br />
                        <span class="text-muted">Boarding  </span><br />
                        <asp:Label ID="lblBoardingStation" runat="server" CssClass="text-uppercase h4" Text="NA"></asp:Label>
                    </p>
                    <h3 class="mb-1 mt-2">Passengers</h3>
                    <asp:ListView ID="gvPassengers" runat="server">
                        <ItemTemplate>
                            <p class="mb-0 text-left" style="font-size: 13px;">
                                Seat No. <%# Eval("seatno") %> : <%# Eval("travellername") %>, <%# Eval("travellergender") %>, <%# Eval("travellerage") %> Year
                            </p>
                        </ItemTemplate>
                    </asp:ListView>
                    <h3 class="mb-1 mt-2">Discount</h3>
                    <p class="mb-0 text-left" style="font-size: 13px;">
                        Amount <span class="font-weight-600">
                            <asp:Label ID="lblDiscountAmnt" runat="server" Text="0"></asp:Label>
                            <i class="fa fa-rupee-sign"></i>
                        </span>
                        <span class="float-right">
                            <asp:LinkButton ID="lbtnViewOffers" runat="server" CssClass="text-danger text-underline" OnClick="lbtnViewOffers_Click">Apply Coupon</asp:LinkButton>
                            <asp:Label ID="lblOfferCode" CssClass="text-danger mb-0 font-weight-300" runat="server" Text=""></asp:Label>
                            <asp:LinkButton ID="lbtnRemoveOffers" runat="server" Visible="false" CssClass="text-danger ml-2" OnClick="lbtnRemoveOffers_Click"><i class="fa fa-times-circle" style="font-size: 16px;"></i></asp:LinkButton>
                        </span>
                    </p>
                    <h3 class="mb-1 mt-2">Total Fare </h3>
                    <p class="mb-0 text-left" style="font-size: 13px;">
                        Payable Amount <br /><span class="font-weight-600">
                            <asp:Label ID="lblTotal" runat="server" Text="NA"></asp:Label>
                            <asp:HiddenField ID="hfTotal" runat="server" />
                            <i class="fa fa-rupee-sign"></i></span>
                    </p>
                    <div class="dropdown">
                        <button class="btn p-0 text-danger font-weight-300 text-underline" style="font-size: 13px;" type="button" id="multiDropdownMenu" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                            View Breakup
                                    <div class="ripple-container"></div>
                        </button>
                        <div class="dropdown-menu pl-3" aria-labelledby="multiDropdownMenu">
                            <p class="mb-0 text-left" style="font-size: 13px;">
                                Fare
                                <span class="font-weight-bold ">
                                    <asp:Label ID="lblFareAmt" runat="server" Text="NA"></asp:Label>
                                    <i class="fa fa-rupee-sign"></i>
                                </span>
                            </p>
                            <p class="mb-0 text-left" style="font-size: 13px;">
                                Reservation
                                <span class="font-weight-bold ">
                                    <asp:Label ID="lblReservationCharge" runat="server" Text="NA"></asp:Label>
                                    <i class="fa fa-rupee-sign"></i>
                                </span>
                            </p>

                            <asp:GridView ID="grdtax" runat="server" ShowHeader="false" GridLines="None" Width="100%" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <p class="mb-0 text-left" style="font-size: 13px;">
                                                <%# Eval("taxname")%>

                                                <span class="font-weight-bold">
                                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("taxamt")%>'></asp:Label>
                                                    <i class="fa fa-rupee-sign"></i>
                                                </span>
                                            </p>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <p class="mb-0 text-left" style="font-size: 13px;">
                        <asp:Label ID="fareYN" runat="server" Text="(Fare will be collected by conductor in the bus)"
                            Style="color: Red;" Visible="false"></asp:Label>
                    </p>
                </div>
                <div class="col-lg-9 py-3 pl-3" style="min-height: 50vh;">

                    <h5 class="h3 text-primary mb-0">You can pay using one of the following mode</h5>
                    <div class="row align-items-center">
                        <div class="col-lg-12 pb-3">
                            <p class="mb-1 text-sm"><span class="font-weight-600">1. Wallet</span> is your personal account in this portal. You can pay ticket amount using your wallet. it is fast, Secure and Easy way to make your booking completed.</p>
                            <p class="mb-1 text-sm"><span class="font-weight-600">2. Payment Gateway</span> is an online payment system in which you can use Debit, Credit and Internet Banking to pay the ticket amount.</p>
                        </div>
                        <div class="row w-100 pb-3">
                            <div class="col-lg-12">
                                <div style="background: #f4f5f8; padding: 5px 10px; width: 100%; border: solid 1px #e2e7f8;">
                                    <p class="text-lg text-center mb-0">
                                        <asp:CheckBox ID="chkTOC" runat="server" Text="&nbsp; I Accept" />
                                         <a class="text-blue text-lg text-underline" id="lbtntermsNcondition" runat="server" onserverclick="lbtntermsNcondition_ServerClick" style="cursor: pointer;">Terms & Conditions</a>
                                    </p>
                                </div>
                            </div>
                        </div>
                        <div class="row w-100">
                            <div class="col-lg-6">
                                <div style="background: #f4f5f8; padding: 15px; width: 100%; border: solid 1px #e2e7f8; min-height: 40vh !important">
                                    <h3 class="bg-transparent">Wallet <i class="fa fa-info-circle text-gray ml-1" data-toggle="tooltip" data-placement="top" title="Wallet is your personal account in this portal. You can pay ticket amount using your wallet. it is fast, Secure and Easy way to make your booking completed."></i>
                                    </h3>
                                    <p class="text-md">
                                        Available Balance <span class="font-weight-600"><i class="fa fa-rupee-sign mr-2"></i>
                                            <asp:Label ID="lblWalletBalance" runat="server" Text="0"></asp:Label>
                                            <asp:HiddenField ID="hfWalletBalance" runat="server" />
                                        </span>
                                    </p>
                                    <asp:LinkButton ID="lbtnProceedWallet" runat="server" CssClass="btn btn-icon btn-success" OnClick="lbtnProceedWallet_Click">
                                                        <i class="fa fa-check"></i> Proceed To Pay
                                    </asp:LinkButton>
                                </div>
                            </div>
                            <div class="col-lg-6">
                                <div style="background: #f4f5f8; padding: 15px; width: 100%; border: solid 1px #e2e7f8; min-height: 40vh !important">
                                    <h3 class="bg-transparent">Payment Gateway <i class="fa fa-info-circle text-gray ml-1" data-toggle="tooltip" data-placement="top" title="Payment Gateway is an online payment system in which you can use Debit, Credit and Internet Banking to pay the ticket amount."></i>
                                    </h3>
                                    <div class="row mt-2 text-center">
                                <div class="col-md-12">
                                    <asp:Label ID="lblNoPG" runat="server" Text="Payment Gateways are not available.<br>Please contact to helpdesk." Visible="false" Style="font-size: 23px; color: #919691; font-weight: bold;"></asp:Label>
                                </div>
                                <asp:Repeater ID="rptrPG" runat="server" OnItemCommand="rptrPG_ItemCommand">
                                    <ItemTemplate>
                                        <div class="col-md-6" style="border: 1px solid #e6e6e6; padding: 9px;">
                                            <asp:HiddenField ID="rptHdPGId" runat="server" Value='<%# Eval("gateway_id") %>' />
                                            <asp:HiddenField ID="hd_pgurl" runat="server" Value='<%# Eval("req_url") %>' />
                                            <img src="../Dbimg/PG/<%# Eval("gateway_name")%>_W.png" style="height: 30px; width: 50%;" />
                                            <h3 class="mb-0">
                                                <%# Eval("gateway_name")%>
                                            </h3>
                                            <p>
                                                <%# Eval("description")%>
                                            </p>
                                            <asp:Button ID="btnproceed" runat="server" Text="Proceed to Pay" CssClass="btn mb-2"
                                                Style="background-color: #00baf2; color: White; font-weight: bold;" CommandName="PAYNOW" />
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                                </div>
                            </div>
                        </div>
                    </div>


                </div>
            </div>
        </div>


        <div class="row">
            <cc1:ModalPopupExtender ID="mpOffers" runat="server" CancelControlID="btnClosempOffers"
                TargetControlID="btnOpenmpOffers" PopupControlID="pnlOffers" BackgroundCssClass="ModalPopupBackground">
            </cc1:ModalPopupExtender>

            <asp:Panel ID="pnlOffers" Style="display: none;" runat="server">
                <div class="modal-dialog modal- modal-dialog-centered modal-" role="document">
                    <div class="modal-content">
                        <div class="modal-header p-0">
                            <div class="col text-right">
                                <button type="button" id="btnClosempOffers" class="btn p-0 text-danger" data-dismiss="modal"><i class="fa fa-times fa-2x"></i></button>
                            </div>
                        </div>
                        <div class="modal-body bg-white">
                            <asp:ListView ID="lvOffers" runat="server" OnItemDataBound="lvOffers_ItemDataBound" OnItemCommand="lvOffers_ItemCommand"
                                DataKeyNames="couponid, couponcode, coupontitle, discountdescription, discounttype, discounton, discountamount, maxdiscount_amount, validfrom_date, validto_date">
                                <ItemTemplate>
                                    <div class="row mb-4 shadow">
                                        <div class="col-lg-12">
                                            <asp:Image ID="imgWeb" runat="server" CssClass="card-img" Style="border-top-right-radius: 0; border-bottom-right-radius: 0;" />
                                        </div>
                                        <div class="col-lg-6">
                                            <h3 class="mb-0 text-blue"><%# Eval("coupontitle")%></h3>
                                            <h5 class="text-black-50 mb-0">Code : <%# Eval("couponcode")%></h5>
                                            <h5 class="text-black-50 mb-0"><%# Eval("discountamount")%>  <%# Eval("discounttype").ToString().Trim()== "A" ? "₹" : "%"%> <%# Eval("discounton").ToString().Trim()== "S" ? "Per Seat" : "Per Ticket"%></h5>
                                        </div>
                                        <div class="col-lg-3 text-right pt-2">
                                            <asp:LinkButton ID="lbtnViewDetails" runat="server" CssClass="btn btn-success" CommandName="VIEWDETAILS">Apply</asp:LinkButton>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:ListView>
                            <asp:Panel ID="pnlNoOffer" runat="server" Width="100%" Visible="false">
                                <div class="col-lg-12 col-md-12 col-sm-12 text-center">
                                    <h1 class="text-muted">Offer will be available Soon.</h1>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <div style="visibility: hidden;">
                <asp:Button ID="btnOpenmpOffers" runat="server" />
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
    </div>
    <!--Model Pop-->

    <cc1:ModalPopupExtender ID="mpePage" runat="server" PopupControlID="pnlPage" TargetControlID="Button21"
        CancelControlID="LinkButton71" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlPage" runat="server" Style="position: fixed;">
        <div class="modal-content mt-5" style="width: 98%; margin-left: 2%;">
            <div class="modal-header" >
                <div class="col-md-7">
                    <h3 id="lblTitle" runat="server" class="m-0"> </h3>
                </div>
                <div class="col-md-4 text-right" style="text-align: end;">
                </div>
                <div class="col-md-1 text-right" style="text-align: end;">
                    <asp:LinkButton ID="LinkButton71" runat="server" ForeColor="Red" Font-Bold="true" Font-Size="24px">X</asp:LinkButton>
                </div>
            </div>
            <div class="modal-body" style="background-color: #ffffff; overflow-y: auto; padding: 1px">
                <embed src="" style="height: 85vh; width: 90vw;" runat="server" id="embedPage" />
            </div>
        </div>
        <br />
        <div style="visibility: hidden;">
            <asp:Button ID="Button21" runat="server" Text="" />
        </div>
    </asp:Panel>

    <!-- ModalPopupExtender -->

</asp:Content>

