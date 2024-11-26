<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/SysAdmmaster.master" AutoEventWireup="true" CodeFile="SysAdmLayoutSeatView.aspx.cs" Inherits="Auth_SysAdmLayoutSeatView" %>

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
        .shadow {
    box-shadow: 15px 15px 15px 15px rgb(1 1 1 / 20%) !important;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="header pb-7">
    </div>
    <div class="container-fluid mt--6" style="min-height: 100vh;">
        <div class="row align-items-center">
             <div class="col-lg-1"></div>
            <div class="col-lg-10">
                <div class="card card-stats mb-3 shadow">
                    <div class="card-header">
                        <div class="row">
                            <div class="col-lg-8 p-2">
                               <h1><asp:Label runat="server" ID="lblLayoutName" ></asp:Label></h1> </div>
                            <div class="col-lg-4 text-right">
                                <asp:LinkButton ID="lbtnBack" runat="server" ToolTip="Close and Return to Layout Creation" CssClass="btn btn-outline-danger" OnClick="lbtnBack_Click" BackColor="Red" ForeColor="White"><i class="fa fa-2x fa-times"></i>  </asp:LinkButton></div>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-lg-12">
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

                        </div>
                    </div>
                </div>
            </div>
               <div class="col-lg-1"></div>
        </div>
    </div>




</asp:Content>

