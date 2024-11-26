<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/subCscMaster.Master" AutoEventWireup="true" CodeFile="CSCConfirm.aspx.cs" Inherits="Auth_CSCConfirm" %>



<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };
    </script>
    <script type="text/javascript">
        $('.collapse').on('shown.bs.collapse', function () {
            $(this).parent().find(".glyphicon-plus").removeClass("glyphicon-plus").addClass("glyphicon-minus");

        }).on('hidden.bs.collapse', function () {
            $(this).parent().find(".glyphicon-minus").removeClass("glyphicon-minus").addClass("glyphicon-plus");

        });
    </script>
    <style type="text/css">
        .modalBackground{
            background-color:black;
            opacity:0.6;
        }
        fieldset {
            min-width: 10px;
            padding: 10px;
            margin: 10px;
            border: 5px;
        }

        .well {
            min-height: 20px;
            padding: 19px;
            width: 100%;
            margin-bottom: 20px;
            background-color: #f5f5f5;
            border: 1px solid #e3e3e3;
            border-radius: 4px !important;
            -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, .05) !important;
            box-shadow: inset 0 1px 1px rgba(0, 0, 0, .05);
        }

        .well-legend {
            width: 100px;
            height: 30px;
            display: block;
            font-size: 11pt;
            width: auto;
            padding: 2px 7px 2px 5px;
            margin-bottom: 20px;
            line-height: inherit;
            color: White !importanat;
            background: #d9edf7;
            ;
            border: 1px solid #e3e3e3;
            border-radius: 4px;
            box-shadow: inset 0 1px 1px rgba(0, 0, 0, .05);
            -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, .05);
        }
    </style>
    <style type="text/css">
        .accordion-toggle.collapsed:after {
            /* symbol for "collapsed" panels */ /* adjust as needed, taken from bootstrap.css */
        }

        .accordion-toggle:after {
            /* symbol for "opening" panels */
            font-family: 'Glyphicons Halflings'; /* essential for enabling glyphicon */
            font-size: 15pt float: left; /* adjust as needed */
            color: Blue; /* adjust as needed */
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hidtoken" runat="server" />
    <asp:Label ID="lbltkt" runat="server" Text=""></asp:Label>
    <div class="content mt-3" style="width: 100%;">
        <asp:Panel ID="pnldetails" runat="server">
            <div class="row">
                <div class="col-lg-4">
                    <div class="card">
                        <div class="card-header">
                            <p class="card-title m-0 text-dark">Journey Details</p>
                        </div>
                        <div class="card-body">
                            <p class="text-sm-left mb-2">
                                Service
                                <asp:Label ID="lblServiceType" runat="server" ForeColor="Black"></asp:Label>
                            </p>
                            <p class="text-sm-left mb-2">
                                From Station
                                <asp:Label ID="lblSource" runat="server" Text="" ForeColor="Black"></asp:Label>
                            </p>
                            <p class="text-sm-left mb-2">
                                To Station
                                <asp:Label ID="lblDestination" runat="server" ForeColor="Black"></asp:Label>
                            </p>
                            <p class="text-sm-left mb-2">
                                Departure 
                                <asp:Label ID="lblJourneyDate" runat="server" Text="" ForeColor="Black"></asp:Label>
                                <asp:Label ID="lblJourneyTime" runat="server" ForeColor="Black"></asp:Label>
                            </p>
                            <p class="text-sm-left mb-2">
                                Boarding
                                <asp:Label ID="lblBoarding" runat="server" ForeColor="Black"></asp:Label>
                            </p>
                        </div>
                    </div>
                </div>
                <div class="col-lg-4">
                    <div class="card">
                        <div class="card-header">
                            <p class="card-title m-0 text-dark">Passengers</p>
                        </div>
                        <div class="card-body" style="min-height: 190px;">
                            <asp:Repeater ID="gvJourneyUp" runat="server">
                                <ItemTemplate>
                                    <p class="text-dark mb-2 text-sm-left">
                                        <span class="font-weight-bold pr-2">Seat No. <%# Eval("seatno") %></span> <%# Eval("travellername") %>, <%# Eval("trvlr_gender") %>, <%# Eval("trvl_age") %>Y
                                    </p>

                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
                <div class="col-lg-4">
                    <div class="card">
                        <div class="card-header">
                            <p class="card-title m-0 text-dark">Fare</p>
                        </div>
                        <div class="card-body" style="min-height: 190px;">
                            <p class="text-dark mb-2 text-sm-left">
                                Booking amount<span class="font-weight-bold float-right">
                                    <asp:Label ID="lblFareAmt" runat="server"></asp:Label><i class="fa fa-rupee pl-2"></i></span>
                            </p>
                            <p class="text-dark mb-2 text-sm-left">
                                Reservartion charges<span class="font-weight-bold float-right">
                                    <asp:Label ID="lblReservationCharge" runat="server"></asp:Label><i class="fa fa-rupee pl-2"></i></span>
                            </p>
                            <p class="text-dark mb-2 text-sm-left">
                                Extra Commission<span class="font-weight-bold float-right">
                                    <asp:Label ID="lblcommission" runat="server"></asp:Label><i class="fa fa-rupee pl-2"></i></span>
                            </p>
                            <asp:GridView Style="width: 100%;" ID="grdtax" runat="server" ShowHeader="false" AutoGenerateColumns="False" GridLines="None">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <p class="text-dark mb-2 text-sm-left">
                                                <%# Eval("taxname")%><span class="font-weight-bold float-right">
                                                    <%# Eval("amt")%><i class="fa fa-rupee pl-2"></i></span>
                                            </p>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-body text-center">

                            <strong style="color: #507cd1; font-size: 12pt; font-family: verdana;">Total Amount
                                    to Collect</strong>
                            <asp:Label ID="lblTotal" Style="font-size: 12pt; font-family: verdana;" runat="server"
                                ForeColor="black"></asp:Label>
                            <i class="fa fa-rupee"></i>
                            <br />
                            <asp:Label ID="fareYN" runat="server" Text="(Fare will be collected by conductor in the bus)" Style="color: Red; font-size: 10pt;" Visible="false"></asp:Label>
                            <br />
                            <asp:Button ID="btnproceed" runat="server" OnClick="btnproceed_Click" OnClientClick="return ShowLoading()" Style="font-size: 11pt; margin-top: 15px; font-family: verdana;"
                                data-toggle="collapse" data-parent="#accordion" href="#collapse2"
                                Text="Confirm and Proceed For Printing" CssClass="btn btn-warning" />
                            <asp:Button ID="btnprint" runat="server" onclick="btnprint_Click" Visible="false" Style="font-size: 11pt; margin-top: 15px; font-family: verdana;"
                                data-toggle="collapse" data-parent="#accordion" href="#collapse2"
                                Text="Print" CssClass="btn btn-warning" />

                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlPrntYN" runat="server" Visible="false">
            <div class="row">
                <div class="col-lg-3"></div>
                <div class="col-lg-6">
                    <div class="card">
                        <div class="card-body">
                            <center>
                                <span style="font-size: 15pt;">Ticket vide PNR No.
                                    <asp:Label ID="lblpnr" runat="server"></asp:Label>
                                    <br />
                                    has been booked successfull. </span>
                                <br />
                                <br />
                                <asp:Label ID="Label13" Text="Do you want print the ticket again ?" Style="font-size: 15pt; font-weight: bold; color: red;"
                                    runat="server" /><br />
                                <br />
                                <asp:Button ID="btnyes" runat="server" Onclick="btnyes_Click" CssClass="btn btn-success" Text="Yes" Style="border-radius: 4px;" />
                                <asp:Button ID="btnno" CssClass="btn btn-warning" Onclick="btnno_Click" runat="server" on Text="No" Style="border-radius: 4px;" />
                            </center>
                        </div>
                    </div>
                </div>
                <div class="col-lg-3"></div>
            </div>
        </asp:Panel>

        <div class="row">
            <cc1:ModalPopupExtender ID="mpConfirmation" runat="server" PopupControlID="pnlConfirmation"
                CancelControlID="Button3" TargetControlID="Button4" BackgroundCssClass="modalBackground"  BehaviorID="bvConfirm">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlConfirmation" runat="server" Style="position: fixed;">
                <div class="card" style="width: 350px;">
                    <div class="card-header">
                        <h4 class="card-title text-left mb-0">Please Confirm
                        </h4>
                    </div>
                    <div class="card-body text-center pt-2" style="min-height: 100px;">
                        <asp:Label ID="lblConfirmation" runat="server" Text="Do you want to save ?"></asp:Label>
                        <div style="width: 100%; margin-top: 20px; text-align: right;">
                            <asp:LinkButton ID="lbtnYesConfirmation" OnClick="lbtnYesConfirmation_Click" OnClientClick="$find('bvConfirm').hide();ShowLoading();" runat="server" CssClass="btn btn-success btn-sm"> <i class="fa fa-check"></i> Yes </asp:LinkButton>
                            <asp:LinkButton ID="lbtnNoConfirmation" runat="server" CssClass="btn btn-danger btn-sm ml-2"> <i class="fa fa-times"></i> No </asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div style="visibility: hidden;">
                    <asp:Button ID="Button1" runat="server" Text="" />
                    <asp:Button ID="Button3" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>
        <div class="row">
            <cc1:ModalPopupExtender ID="mpticket" runat="server" PopupControlID="pnlticket"
                CancelControlID="lbtnclose" TargetControlID="Button4" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlticket" runat="server" Style="position: fixed;">
                <div class="card">
                    <div class="card-header">
                        <div class="row">
                            <div class="col-lg-6">
                                <h5 class="card-title text-left mb-0">Ticket Details
                                </h5>
                            </div>
                            <div class="col-lg-6">
                                <asp:LinkButton ID="lbtnclose" runat="server" CssClass="text-danger float-right"> <i class="fa fa-times"></i> </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="card-body text-left pt-2" style="overflow: auto;">
                        <embed id="tkt" runat="server" src="" style="height: 85vh; width: 45vw" />
                    </div>
                </div>
                <div style="visibility: hidden;">
                    <asp:Button ID="Button4" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>




