<%@ Page Title="" Language="C#" MasterPageFile="~/mainMasterPage.master" AutoEventWireup="true" CodeFile="TrackMyBus.aspx.cs" Inherits="TrackMyBus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="midd-bg">
        <div class="container">
            <div class="row no-gutters">
                <div class="col-md-12">

                    <asp:Panel ID="pnlsearch" runat="server" Visible="true">
                        <div class="tx5">For Kind  Attention <span class="org">Please</span></div>
                        <ol>


                            <li class="list-group-item">To track a bus , please enter an  enter an active PNR Number.  </li>
                            <li class="list-group-item">Buses fitted with GPS device can only be tracked. Please chcek status of your PNR for GPS tracking information.</li>
                        </ol>


                        <div class="tx5 mb-1">To Track a Bus Service Please<span class="org"> Enter Valid  PNR(Ticket) No. & Mobile No.</span></div>
                        <div class="card py-3 px-5" style="box-shadow: 0px 5px 12px -1px rgb(0 0 0 / 6%);" visible="true">
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group mb-2">
                                        <asp:Label runat="server" Text="PNR No." CssClass="label font-weight-light mb-0" Style="font-size: 12px;"></asp:Label>
                                        <asp:TextBox ID="tbpnr" runat="server" MaxLength="20" class="form-control form-control-sm"
                                            Style="font-size: 16px;" placeholder="Enter PNR No." autocomplete="off"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-2">
                                    <div class="form-group mb-2">
                                        <asp:Label runat="server" Text="Mobile No." CssClass="label font-weight-light mb-0" Style="font-size: 12px;"></asp:Label>
                                        <asp:TextBox ID="tbmobile" runat="server" MaxLength="10" class="form-control form-control-sm"
                                            Style="font-size: 16px;" placeholder="Enter Mobile No." autocomplete="off"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="ajxFtMobileNumber" runat="server" FilterType="Numbers" ValidChars=""
                                            TargetControlID="tbmobile" />
                                    </div>
                                </div>
                                <div class="col-sm-3 mb-2">
                                    <div class="form-group mt-4 input-group-prepend">
                                        <div class="input-group" style="box-shadow: 0px 0px rgb(53 52 92 / 20%), 0 0px 0 rgb(0 0 0 / 1%);">
                                            <img alt="Visual verification" title="Please enter the security code as shown in the image."
                                                src="CaptchaImage.aspx" style="width: 80%; border: 1px solid #e6e6e6; margin-top: -2px;" class="form-control" />
                                            <div class="input-group-append">
                                                <asp:LinkButton runat="server" ID="lbtnRefresh" CssClass="btn-primary btn-sm" OnClick="lbtnRefresh_Click" Style="margin-top: -2px;">
                                                    <i class="fa fa-1x fa-recycle mt-2" ></i></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-2">
                                    <div class="form-group mb-2 mt-4">

                                        <asp:TextBox ID="tbcaptchacode" runat="server" MaxLength="6" class="form-control form-control-sm text-uppercase"
                                            Style="font-size: 16px;" placeholder="Enter Text" autocomplete="off"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-2 mt-4">
                                    <div class="form-group">
                                        <asp:LinkButton ID="lbtntrackbus" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lbtntrackbus_Click"> <i class="fa fa-map-marked"></i>  Search </asp:LinkButton>

                                    </div>
                                </div>

                            </div>
                        </div>

                    </asp:Panel>
                    <asp:Panel ID="pnldetails" runat="server" Visible="false">
                        <div class="row no-gutters">
                            <div class="col-md-12">


                                <div class="tx5">You Are Tracking The Bus<span class="org"> With Following Details</span></div>
                                <div class="tk-bg mt-1 p-0">
                                    <div class="row pl-3 ">
                                        <div class="col-lg-7 pt-2">
                                            <asp:Label runat="server" Text="PNR -"> </asp:Label>
                                            <asp:Label ID="lblpnr" runat="server" Text=""> </asp:Label>

                                            <asp:Label runat="server" Text="Bus -" CssClass="ml-5"> </asp:Label>
                                            <asp:Label ID="lblbusno" runat="server" Text=""> </asp:Label>

                                            <asp:Label runat="server" Text="Date -" CssClass="ml-5"> </asp:Label>
                                            <asp:Label ID="lbljourneydate" runat="server" Text=""> </asp:Label>
                                        </div>
                                        <div class="col-lg-4 pt-2">
                                            <asp:Label runat="server" Text="Route -"> </asp:Label>
                                            <asp:Label ID="lblsrc" runat="server" Text=""> </asp:Label>
                                        </div>
                                        <div class="col-lg-1">
                                            <asp:LinkButton runat="server" ID="lbtnchange" OnClick="lbtnchange_Click" CssClass="btn btn-primary float-right">Change</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">

                                <div class="card" id="div1" style="border: solid; border-color: lightgray; padding: 4px; border-width: 1px" runat="server">
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>

    <br />
    <br />
    <br />

</asp:Content>

