<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/SysAdmmaster.master" AutoEventWireup="true" CodeFile="SysAdmLayoutSeatsYN.aspx.cs" Inherits="Auth_SysAdmLayoutSeatsYN" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
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
        div.divToggleButton input[type=checkbox]:checked + label::before, div.divToggleButton input[type=checkbox]:not(:checked) + label::before, div.divToggleButton input[type=checkbox] + label {
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
            background: #f0f0f0; /* gradient style (optional)*/
            background-image: -moz-linear-gradient(top, #fdfdfd, #f9f9f9 50%, #e5e5e5 50%, #fdfdfd);
            background-image: -webkit-gradient(linear, center top, center bottom, from(#fdfdfd), color-stop(0.5, #f9f9f9), color-stop(0.5, #e5e5e5 ), to(#fdfdfd));
            background-image: linear-gradient(to bottom, #fdfdfd, #f9f9f9 50%, #e5e5e5 50%, #fdfdfd);
            height: 50px;
            width: 42px;
            padding: 0px;
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
            content: url("../assets/img/Seats/seatwhiteN.png");
            height: 50px;
            width: 42px; /** content: "\2714";**/
            color: #000090;
            font-weight: bold;
        }
        /************************************************************************/
        /************************************************************************/
        /* PSEUDO-TOGGLE BUTTON MADE OF ASP.NET CHECKBOX AND CSS3*/
        div.divToggleButtonU input[type=checkbox] {
            display: none;
            white-space: nowrap;
        }

        div.divToggleButtonU label {
            display: block;
            float: left;
            cursor: pointer;
        }
        /* set the size of the pseudo-toggle button control */
        div.divToggleButtonU input[type=checkbox]:checked + label::before, div.divToggleButtonU input[type=checkbox]:not(:checked) + label::before, div.divToggleButtonU input[type=checkbox] + label {
            width: 30pt;
            height: 30pt;
            line-height: 30pt;
        }
        /* additional styling: rounded border, gradient */
        div.divToggleButtonU input[type=checkbox] + label {
            vertical-align: middle;
            text-align: center;
            font-size: 16pt;
            font-family: Arial, Calibri;
            border: 1px solid #bdbdbd;
            border-radius: 5px;
            background: #f0f0f0; /* gradient style (optional)*/
            background-image: -moz-linear-gradient(top, #fdfdfd, #f9f9f9 50%, #e5e5e5 50%, #fdfdfd);
            background-image: -webkit-gradient(linear, center top, center bottom, from(#fdfdfd), color-stop(0.5, #f9f9f9), color-stop(0.5, #e5e5e5 ), to(#fdfdfd));
            background-image: linear-gradient(to bottom, #fdfdfd, #f9f9f9 50%, #e5e5e5 50%, #fdfdfd);
            height: 50px;
            width: 95px;
            padding: 0px;
        }
        /* content to display and style pertinent to unchecked state*/
        div.divToggleButtonU input[type=checkbox]:not(:checked) + label::before {
            content: "X";
            color: Red;
            opacity: 0.6;
        }
        /* content to display and style pertinent to checked state*/
        div.divToggleButtonU input[type=checkbox]:checked + label::before {
            /** content         : "A\2714";**/
            content: url("../assets/img/Seats/seatSleeper.png");
            height: 45px;
            width: 65px; /** content: "\2714";**/
            color: #000090;
            font-weight: bold;
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
                                        <li>To hide/show the seat, please click on the seat/cross button</li>
                                        <li>Next step is to change seat numbers</li>
                                        <li>In order to proceed for seat numbering , please finalising the seat arrangment and
                                        on Save & Proceed Button</li>
                                    </ul>
                                </span>
                            </div>
                            <div class="col-lg-9">
                                <div class="row px-4">
                                    <div class="col-lg-12">
                                        <h3 style="font-weight: bold">Bus Seat Layout Managment-Make Seating Arrangment</h3>
                                    </div>
                                </div>
                                <div class="row mt-2">
                                    <div class="col-lg-4">
                                    </div>
                                    <div class="col-lg-4 text-center">
                                        <asp:Panel ID="pnl1" runat="server" BackColor="White" Style="padding: 10px; text-align: center">

                                            <asp:Label runat="server" ID="lblTitleSeatsU" CssClass="form-control" Text="Upper Seats"></asp:Label>
                                            <div class="divToggleButtonU" runat="server" id="divToggleButtonU">
                                                <center>
                                        <asp:Table ID="Table2" runat="server" BorderColor="Black" Style="border: 1px Solid Black;"
                                            CellPadding="10">
                                        </asp:Table>
                                            </center>
                                            </div>

                                        </asp:Panel>
                                        <asp:Panel ID="pnl2" runat="server" BackColor="White">

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
                                <div class="row mt-2 text-center">
                                    <div class="col-lg-12">
                                          <asp:LinkButton ID="lbtnSave" Visible="true" runat="server" class="btn btn-success" ToolTip="Save Layout" OnClick="lbtnSave_Click">
                                    <i class="fa fa-save"></i>&nbsp; Save</asp:LinkButton>
                                    <asp:LinkButton ID="lbtnReset" runat="server" CssClass="btn btn-danger" ToolTip="Reset Layout" OnClick="lbtnReset_Click">
                                    <i class="fa fa-undo"></i>&nbsp; Reset</asp:LinkButton>

                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

