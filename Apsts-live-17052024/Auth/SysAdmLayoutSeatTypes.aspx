<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/SysAdmmaster.master" AutoEventWireup="true" CodeFile="SysAdmLayoutSeatTypes.aspx.cs" Inherits="Auth_SysAdmLayoutSeatTypes" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        input[type="radio"] {
            -ms-transform: scale(2.0);
            -webkit-transform: scale(2.0);
            transform: scale(2.0);
            width: 50px;
        }


        /************************************************************************/
        /* PSEUDO-TOGGLE BUTTON MADE OF ASP.NET CHECKBOX AND CSS3*/
        div.divToggleButton input[type=checkbox] {
            display: none;
            white-space: nowrap;
        }

        div.divToggleButton label {
            display: block;
            float: left;
            cursor: pointer;
        }

        /* set the size of the pseudo-toggle button control */
        div.divToggleButton input[type=checkbox]:checked + label::before,
        div.divToggleButton input[type=checkbox]:not(:checked) + label::before,
        div.divToggleButton input[type=checkbox] + label {
            width: 30pt;
            height: 30pt;
            line-height: 30pt;
        }

        /* additional styling: rounded border, gradient */
        div.divToggleButton input[type=checkbox] + label {
            vertical-align: middle;
            text-align: center;
            font-size: 16pt;
            font-family: Arial, Calibri;
            border: 1px solid #bdbdbd;
            border-radius: 5px;
            background: #f0f0f0;
            /* gradient style (optional)*/
            background-image: -moz-linear-gradient(top, #fdfdfd, #f9f9f9 50%, #e5e5e5 50%, #fdfdfd);
            background-image: -webkit-gradient(linear, center top, center bottom, from(#fdfdfd), color-stop(0.5, #f9f9f9), color-stop(0.5, #e5e5e5 ), to(#fdfdfd));
            background-image: linear-gradient(to bottom, #fdfdfd, #f9f9f9 50%, #e5e5e5 50%, #fdfdfd);
        }

        /* content to display and style pertinent to unchecked state*/
        div.divToggleButton input[type=checkbox]:not(:checked) + label::before {
            content: "X";
            color: Red;
            opacity: 0.6;
        }

        /* content to display and style pertinent to checked state*/
        div.divToggleButton input[type=checkbox]:checked + label::before {
            /** content         : "A\2714";**/
            content: "\2714";
            color: #000090;
            font-weight: bold;
        }
        /************************************************************************/
        .seatTextbox {
            background-color: White;
            /**background-image:url('img/seatWhiteN.png');
background-repeat:no-repeat;**/
            padding: 10px;
            height: 41px;
        }

        .seatTextboxDriver {
            background-image: url('../assets/img/Seats/seatDriver.png');
            background-repeat: no-repeat;
            padding: 10px;
            height: 41px;
        }

        .seatTextboxWomen {
            background-color: Purple;
            /**background-image:url('img/seatpink.png');
background-repeat:no-repeat;**/
            padding: 10px;
            height: 41px;
            color: White;
        }

        .seatTextboxConductor {
            background-color: Yellow;
            /**background-image:url('img/seatConductorN.png');
background-repeat:no-repeat;**/
            padding: 10px;
            height: 41px;
        }

        .seatTextboxRes {
            background-color: Gray;
            /**background-image:url('img/seatgray.png');
background-repeat:no-repeat;**/
            padding: 10px;
            height: 41px;
            color: White;
        }

        .seatTextboxMale {
            /**background-image:url('img/seatMale.png');
background-repeat:no-repeat;**/
            background-color: Blue;
            padding: 10px;
            height: 41px;
            color: White;
        }

        .seatTextboxSleeper {
            background-image: url('../assets/img/Seats/seatSleeper.png');
            background-repeat: no-repeat;
            padding: 10px;
            height: 41px;
        }

        .seatTextboxDisable {
            background-image: url('../assets/img/Seats/seatgray.png');
            background-repeat: no-repeat;
            padding: 10px;
            height: 41px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="header pb-7">
    </div>
    <div class="container-fluid mt--6">
        <div class="row align-items-center">
            <div class="col-xl-12">
                <div class="card card-stats mb-3">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-lg-3 border-right">
                                <h4>You are creating layout for</h4>
                                <label>
                                    Layout Category</label>
                                <asp:Label runat="server" ID="lblLayoutCategory" CssClass="form-control"></asp:Label>
                                <label>
                                    Layout Name</label>
                                <asp:Label runat="server" ID="lblLayoutName" CssClass="form-control"></asp:Label>
                                <label>
                                    Rows</label>
                                <asp:Label runat="server" ID="lblNoOfRows" CssClass="form-control"></asp:Label>
                                <label>
                                    Columns</label>
                                <asp:Label runat="server" ID="lblNoOfColumns" CssClass="form-control"></asp:Label>
                                <hr />
                                <span style="color: Red; text-align: left;"><b>Instructions</b>
                                    <ul style="padding-left: 20px">
                                      <li>By default all seats (except driver seat) are marked general and will be available
                                        for booking.</li>
                                    <li>In order to change seat type please click on seat and select seat type.</li>
                                    <li>After completing the seat type selection, please click on Save & Lock button.</li>
                                    <li>Once locked changes in this layout will not be possible.</li>
                                    </ul>
                                </span>
                            </div>
                            <div class="col-lg-9">
                                <div class="row px-4">
                                    <div class="col-lg-12">
                                        <h3 style="font-weight: bold">Bus Seat Layout Managment-Seat Type Configuration</h3>
                                    </div>
                                </div>
                                <div class="row mt-2">
                                    <div class="col-lg-4">
                                    </div>
                                    <div class="col-lg-4 text-center">
                                        <asp:Panel ID="pnlUpperSeats" runat="server" BackColor="White" Style="padding: 10px; text-align: center">

                                            <asp:Label runat="server" ID="lblTitleSeatsU" CssClass="form-control" Text="Upper Seats"></asp:Label>
                                            <div class="divToggleButtonU" runat="server" id="divToggleButtonU">
                                                <center>
                                        <asp:Table ID="Table2" runat="server" BorderColor="Black" Style="border: 1px Solid Black;"
                                            CellPadding="10">
                                        </asp:Table>
                                            </center>
                                            </div>

                                        </asp:Panel>
                                        <asp:Panel ID="pnlLowerSeats" runat="server" BackColor="White">

                                            <asp:Label runat="server" ID="lblTitleSeatsL" CssClass="form-control" Text="Lower Seats"></asp:Label>
                                            <div class="divToggleButton" runat="server" id="divToggleButton">
                                                <center>  
                                            <asp:Table ID="Table1" runat="server" BorderColor="Black" Style="border: 1px Solid Black; padding: 10px;">
                                        </asp:Table>
                                             </center>
                                            </div>

                                            <asp:Panel ID="pnlCntrl" runat="server">
                                            </asp:Panel>

                                        </asp:Panel>
                                    </div>
                                    <div class="col-lg-4">
                                    </div>
                                </div>
                                <div class="row mt-1 p-4">
                                    <div class="col-lg-2">
                                        <img src="../assets/img/Seats/seatDriver.png" alt="Driver Seat" />
                                        <br />
                                        Driver
                                    </div>
                                    <div class="col-lg-2">
                                        <img src="../assets/img/Seats/seatConductorN.png" alt="Conductor Seat" />
                                        <br />
                                        Conductor
                                    </div>
                                    <div class="col-lg-2">
                                        <img src="../assets/img/Seats/seatwhiteN.png" alt="General Seat" />
                                        <br />
                                        General
                                    </div>
                                    <div class="col-lg-2">
                                        <img src="../assets/img/Seats/seatpink.png" alt="Woman Only Seat" />
                                        <br />
                                        Woman
                                    </div>
                                    <div class="col-lg-2">
                                        <img src="../assets/img/Seats/seatMale.png" alt="Male Only Seat" />
                                        <br />
                                        Male
                                    </div>
                                    <div class="col-lg-2">
                                        <img src="../assets/img/Seats/seatSleeper.png" alt="Sleeper" />
                                        <br />
                                        Sleeper
                                    </div>
                                </div>

                                <div class="row mt-2 text-center">
                                    <div class="col-lg-12">
                                        <asp:LinkButton ID="lbtnSaveNLock" Visible="true" runat="server" class="btn btn-success" ToolTip="Save And Lock" OnClick="lbtnSaveNLock_Click">
                                    <i class="fa fa-"></i>&nbsp; Save & Lock</asp:LinkButton>

                                        <asp:LinkButton ID="lbtnBack" runat="server" CssClass="btn btn-danger" ToolTip="Reset Layout" OnClick="lbtnBack_Click">
                                    <i class="fa fa-undo"></i>&nbsp; Back to Layout Creation</asp:LinkButton>

                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <cc1:ModalPopupExtender ID="mpConfirmation" runat="server" PopupControlID="pnlConfirmation"
            CancelControlID="lbtnNoConfirmation" TargetControlID="Button4" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlConfirmation" runat="server" Style="position: fixed; display: none;">
            <div class="card" style="min-width: 350px;">
                <div class="card-header">
                    <h4 class="card-title">Please Confirm
                    </h4>
                </div>
                <div class="card-body" style="min-height: 100px;">
                    <asp:Label ID="lblConfirmation" runat="server"></asp:Label>
                    <div style="width: 100%; margin-top: 20px; text-align: right;">
                        <asp:LinkButton ID="lbtnYesConfirmation" runat="server" CssClass="btn btn-success btn-sm" OnClick="lbtnYesConfirmation_Click"> <i class="fa fa-check"></i> Yes </asp:LinkButton>
                        <asp:LinkButton ID="lbtnNoConfirmation" runat="server" CssClass="btn btn-danger btn-sm ml-3"> <i class="fa fa-times"></i> No </asp:LinkButton>
                    </div>
                </div>
            </div>
            <div style="visibility: hidden;">
                <asp:Button ID="Button4" runat="server" Text="" />
            </div>
        </asp:Panel>

        <cc1:ModalPopupExtender ID="mpseatconfig" runat="server" PopupControlID="pnlseatconfig"
            CancelControlID="lbtnno" TargetControlID="Button1" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlseatconfig" runat="server" Style="position: fixed; display: none;width: 37%;">
            <div class="card p-3">
                <div class="card-header card-header pb-1">
                    <h3 class="card-title">
                        <asp:Label runat="server" ID="lblSelectedSeat"></asp:Label>
                    </h3>
                </div>
                <div class="card-body" style="min-height: 100px;">
                    <div class="row" style="margin-bottom: 20px">
                        <div class="col-4">
                            Traveller Type
                        </div>
                        <div class="col-8">
                            <asp:DropDownList ID="ddlSeatType" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlSeatType_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <!---------------------Payment Category Type------------------------------->
                    <div class="row" style="margin-bottom: 20px">
                        <div class="col-4">
                            Applicable Payment
                        </div>
                        <div class="col-8">
                            <asp:DropDownList ID="ddlPaymentCategory" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <!------------------------Availablity Status---------------------------------->
                    <div class="row" style="margin-bottom: 20px">
                        <div class="col-4">
                            Available for Booking
                        </div>
                        <div class="col-8">
                            <asp:RadioButtonList ID="rblAvailabilityStatus" runat="server" RepeatDirection="Horizontal"
                                RepeatLayout="Table">
                                <asp:ListItem Value="Y" Selected="True">Yes</asp:ListItem>
                                <asp:ListItem Value="N">No</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <!----------------------------------------------------------------------------->
                    <div class="row" style="margin-bottom: 20px">
                        <div class="col-12 text-center">
                            <h3>Please confirm, do yout want to save ?</h3>
                        </div>
                    </div>
                    <div class="row" style="margin-bottom: 20px">
                        <div class="col-12 text-center">
                            <asp:LinkButton ID="lbtnyes" runat="server" CssClass="btn btn-success" OnClick="lbtnyes_Click"> <i class="fa fa-check"></i> Yes </asp:LinkButton>
                            <asp:LinkButton ID="lbtnno" runat="server" CssClass="btn btn-danger ml-3"> <i class="fa fa-times"></i> No </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
            <div style="visibility: hidden;">
                <asp:Button ID="Button1" runat="server" Text="" />
            </div>
        </asp:Panel>

    </div>
</asp:Content>

