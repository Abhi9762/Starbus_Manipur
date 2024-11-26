<%@ Page Title="" Language="C#" MasterPageFile="~/traveller/trvlrMaster.master" AutoEventWireup="true" CodeFile="seatConfirmation.aspx.cs" MaintainScrollPositionOnPostback="true" Inherits="traveller_seatConfirmation" %>

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

    <div class="container-fluid py-3">

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
                    <h4 class="mb-1">You are booking for</h4>
                    <p class="mb-0 text-left" style="font-size: 13px;">
                        <span class="text-muted">From </span>
                        <asp:Label ID="lblFromStation" runat="server" Text="NA"></asp:Label><br />
                        <span class="text-muted">To</span>
                        <asp:Label ID="lblToStation" runat="server" Text="NA"></asp:Label><br />
                        <span class="text-muted">Service </span>
                        <asp:Label ID="lblServiceType" runat="server" Text="NA"></asp:Label><br />
                        <span class="text-muted">Date </span>
                        <asp:Label ID="lblDate" runat="server" Text="NA"></asp:Label><asp:Label ID="lblDeparture" runat="server" CssClass="text-uppercase pl-2" Text="NA"></asp:Label><br />
                        <span class="text-muted">Boarding  </span>
                        <asp:Label ID="lblBoardingStation" runat="server" Text="NA"></asp:Label>
                    </p>
                    <h4 class="mb-1 mt-2">Passengers</h4>
                    <asp:ListView ID="gvPassengers" runat="server">
                        <ItemTemplate>
                            <p class="mb-0 text-left" style="font-size: 13px;">
                                Seat No. <%# Eval("seatno") %></span> : <%# Eval("travellername") %>, <%# Eval("travellergender") %>, <%# Eval("travellerage") %> Year
                            </p>
                        </ItemTemplate>
                    </asp:ListView>

                    <p class="mb-0 text-left" style="font-size: 13px;">
                        <span class="text-danger">Applicable Fare
                        </span>
                        <span class="font-weight-bold">
                            <asp:Label ID="lblFareAmt" runat="server" Text="NA"></asp:Label>
                            <i class="fa fa-rupee-sign"></i>
                        </span>
                    </p>
                </div>
                <div class="col-lg-9 py-3 pl-3" style="min-height: 50vh;">
                    <p class="mb-0 text-left" style="font-size: 13px;">Please confirm the travel and ticket details given on the left hand side<br />and proceed for payment confirmation.</p>
                    <div class="row align-items-center pt-5">
                        <div class="col-xl-12 text-right pr-3">
                            <asp:LinkButton ID="lbtnCancel" runat="server" CssClass="btn btn-danger btn-icon" OnClick="lbtnCancel_Click"><i class="fa fa-times"></i> Cancel</asp:LinkButton>
                            <asp:LinkButton ID="lbtnProceed" runat="server" CssClass="btn btn-success float-right" OnClick="lbtnProceed_Click">
                                                <i class="fa fa-check"></i> Proceed
                            </asp:LinkButton>
                        </div>
                    </div>

                </div>
            </div>
        </div>


    </div>

</asp:Content>

