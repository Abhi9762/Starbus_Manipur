<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/mainMasterPage.master" CodeFile="PassProceedPayment.aspx.cs" Inherits="PassProceedPayment" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };
    </script>
    <link href="../assets/css/travelllerStepProgressBar.css" rel="stylesheet" />
    <style>
        body {
            font-size: 0.8rem;
        }

        h6 {
            font-size: 1.0rem;
            font-weight: bold;
        }

        .border-right {
            border-right: 1px solid;
        }

        .lbld {
            font-weight: bold;
        }

        .modalBackground {
            background-color: black;
            opacity: 0.6;
        }

        .cd {
            min-height: 22vh !important;
        }

        #progressbar li {
            width: 25% !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-content">
        <div class="container">
            <div class="row mb-2 my-3">
                <div class="col-12">
                    <ul id="progressbar">
                        <li class="text-center active" id="apply_one"><strong>Apply for New Pass</strong></li>
                        <li class="text-center active" id="confirm_two"><strong>Confirm Details</strong></li>
                        <li class="text-center active" id="confirm_three"><strong>Payment</strong></li>
                        <li class="text-center" id="confirm_four"><strong>Download</strong></li>
                    </ul>
                </div>
            </div>
            <div class="row my-3" style="padding-bottom: 64px;">
                <div class="col-12">
                    <div class="card">
                        <div class="card-header pt-0 pb-0"></div>
                        <div class="card-body">
                            <div class="row mt-2">
                                <div class="col-lg-6 border-right" style="line-height: 26px;">
                                    <h6>
                                        <asp:Label ID="lblpassrequest" runat="server" Text="" Font-Bold="True"></asp:Label></h6>
                                    <br />

                                    <h6 class="heading-small my-0">Personal Details</h6>
                                    <div class="row p-2">
                                        <div class="col-lg-6">
                                            <span class="form-control-label text-muted font-weight-normal">Name</span>
                                            <asp:Label ID="lblname" runat="server" CssClass="lbld" Text="NA"></asp:Label>
                                        </div>
                                        <div class="col-lg-6">
                                            <span class="form-control-label text-muted font-weight-normal">Father Name</span>
                                            <asp:Label ID="lblfname" CssClass="lbld" runat="server" Text="NA"></asp:Label>
                                        </div>
                                        <div class="col-lg-6">
                                            <span class="form-control-label text-muted font-weight-normal">Gender</span>
                                            <asp:Label ID="lblgender" CssClass="lbld" runat="server" Text="NA"></asp:Label>

                                        </div>
                                        <div class="col-lg-6">
                                            <span class="form-control-label text-muted font-weight-normal">Date of Birth</span>
                                            <asp:Label ID="lbldob" CssClass="lbld" runat="server" Text="NA"></asp:Label>

                                        </div>
                                    </div>
                                    <h6 class="heading-small my-0">Contact Details</h6>
                                    <div class="row p-2">
                                        <div class="col-lg-6">
                                            <span class="form-control-label text-muted font-weight-normal">Mobile No.</span>
                                            <asp:Label ID="lblmobileno" runat="server" CssClass="lbld" Text="NA"></asp:Label>
                                        </div>
                                        <div class="col-lg-6">
                                            <span class="form-control-label text-muted font-weight-normal">Email</span>
                                            <asp:Label ID="lblemail" CssClass="lbld" runat="server" Text="NA"></asp:Label>
                                        </div>
                                        <div class="col-lg-6">
                                            <span class="form-control-label text-muted font-weight-normal">State</span>
                                            <asp:Label ID="lblstate" CssClass="lbld" runat="server" Text="NA"></asp:Label>
                                        </div>
                                        <div class="col-lg-6">
                                            <span class="form-control-label text-muted font-weight-normal">District</span>
                                            <asp:Label ID="lbldistrict" CssClass="lbld" runat="server" Text="NA"></asp:Label>
                                        </div>
                                        <div class="col-lg-8">
                                            <span class="form-control-label text-muted font-weight-normal">Address</span>
                                            <asp:Label ID="lbladdress" CssClass="lbld" runat="server" Text="NA"></asp:Label>

                                        </div>
                                    </div>
                                    <h6 class="heading-small my-0">Journey Details</h6>
                                    <div class="row p-2">
                                        <div class="col-lg-8">
                                            <span class="form-control-label text-muted font-weight-normal">Route</span>
                                            <asp:Label ID="lblRoute" runat="server" CssClass="lbld" Text="NA"></asp:Label>
                                        </div>
                                        <div class="col-lg-4"></div>
                                        <div class="col-lg-8">
                                            <span class="form-control-label text-muted font-weight-normal">Stations</span>
                                            <asp:Label ID="lblstations" CssClass="lbld" runat="server" Text="NA"></asp:Label>

                                        </div>
                                        <div class="col-lg-4"></div>
                                        <div class="col-lg-8">
                                            <span class="form-control-label text-muted font-weight-normal">Service Type</span>
                                            <asp:Label ID="lblservicetype" CssClass="lbld" runat="server" Text="NA"></asp:Label>
                                        </div>
                                        <div class="col-lg-4"></div>
                                        <div class="col-lg-6">
                                            <span class="form-control-label text-muted font-weight-normal">Validity</span>
                                            <asp:Label ID="lblvalidity" CssClass="lbld" runat="server" Text="NA"></asp:Label>
                                        </div>

                                    </div>

                                    <h6 class="heading-small my-0">Applicable Charges</h6>
                                    <div class="row p-2">
                                        <div class="col-lg-4">
                                            <span class="form-control-label text-muted font-weight-normal">Pass Amount</span>
                                            <asp:Label ID="lblPassamount" runat="server" CssClass="lbld" Text="NA"></asp:Label>
                                        </div>
                                        <div class="col-lg-4">
                                            <span class="form-control-label text-muted font-weight-normal">
                                                <asp:Label ID="lblExtra_Charges" runat="server"></asp:Label></span>
                                            <asp:Label ID="lblExtrachrge" CssClass="lbld" runat="server" Text="NA"></asp:Label>

                                        </div>
                                        <div class="col-lg-4">
                                            <span class="form-control-label text-muted font-weight-normal">Tax Amount</span>
                                            <asp:Label ID="lbltaxamt" CssClass="lbld" runat="server" Text="NA"></asp:Label>
                                        </div>
                                    </div>
                                    <hr />
                                    <div class="row">

                                        <div class="col-md-12" style="text-align: center;">
                                            <h3 class="text-danger font-weight-bold">Total Amount To Pay
                                            <asp:Label ID="lblAmountToPay" runat="server" Font-Bold="True" Text="NA"></asp:Label>
                                                <i class="fa fa-rupee"></i>
                                            </h3>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <h6 class="card-title text-left">Please Proceed To Payment</h6>
                                        </div>
                                    </div>
                                    <div class="row mt-3 text-center">
                                        <div class="col-md-12">
                                            <asp:Label ID="lblNoPG" runat="server" Text="Payment Gateways are not available.<br>Please contact to helpdesk." Visible="false" Style="font-size: 23px; color: #919691; font-weight: bold;"></asp:Label>
                                        </div>
                                        <asp:Repeater ID="rptrPG" runat="server" OnItemCommand="rptrPG_ItemCommand" >
                                            <ItemTemplate>
                                                <div class="col-md-12">
                                                    <asp:HiddenField ID="rptHdPGId" runat="server" Value='<%# Eval("gateway_id") %>' />
                                                      <asp:HiddenField ID="hd_pgurl" runat="server" Value='<%# Eval("req_url") %>' />
                                                    <img src="../Dbimg/PG/<%# Eval("gateway_name")%>_W.png" style="height: 30px;" />
                                                    <h3 class="mt-2 mb-1">
                                                        <%# Eval("gateway_name")%>
                                                    </h3>
                                                    <p>
                                                        <%# Eval("description")%>
                                                    </p>
                                                    <asp:Button ID="btnpaytm" runat="server" Text="Proceed to Pay" CssClass="btn mb-2"
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
           
            <div class="row">
                <cc1:ModalPopupExtender ID="mperror" runat="server" PopupControlID="pnlerror" CancelControlID="LinkButton1"
                    TargetControlID="Button1" BackgroundCssClass="modalBackground">
                </cc1:ModalPopupExtender>
                <asp:Panel ID="pnlerror" runat="server" Style="width: 500px !important">
                    <div class="card cd">
                        <div class="card-header">
                            <h4 class="card-title text-left mb-0">Please Check
                            </h4>
                        </div>
                        <div class="card-body text-left">
                            <asp:Label ID="lblerrormsg" runat="server" Style="font-size: 12pt;"></asp:Label>
                            <div style="margin-top: 20px; text-align: right;">
                                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-success btn-sm ml-2"> <i class="fa fa-check"></i> OK </asp:LinkButton>
                            </div>
                            <div style="visibility: hidden;">
                                <asp:Button ID="Button1" runat="server" Text="" />
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>


