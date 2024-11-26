<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/Cntrmaster.master" AutoEventWireup="true" CodeFile="CntrBusPassesConfirmation.aspx.cs" Inherits="Auth_CntrBusPassesConfirmation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        body {
            font-size: 0.8rem;
        }

        h6 {
            font-size: 0.8rem;
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
    </style>

    <script type="text/javascript">
        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hidtoken" runat="server"></asp:HiddenField>
    <div class="content mt-3">
        <div class="animated fadeIn">
            <div class="row">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-header">
                            <div class="row">
                                <div class="col-lg-6">
                                    <h5><asp:Label ID="lblcardhd" runat="server" Text="New Pass - Confirmation"></asp:Label></h5>
                                     <h6>
                                        <asp:Label ID="lblpassrequest" runat="server" Text="" Font-Bold="True"></asp:Label></h6>
                                </div>
                                <div class="col-lg-6 text-right">
                                    <asp:LinkButton ID="lbtnback" OnClick="lbtnback_Click" runat="server" CssClass="btn btn-warning btn-sm sm-4"> <i class="fa fa-backward"></i> Back To Pass Dashboard</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="card-body">
                            <div class="row mt-2">
                                <div class="col-lg-7 border-right" style="line-height: 26px;">
                                   
                                    <h6 class="heading-small my-0">Personal Details</h6>
                                    <div class="row p-2">
                                        <div class="col-lg-6">
                                            <span class="form-control-label  font-weight-normal">Name</span>
                                            <asp:Label ID="lblname" runat="server" CssClass="lbld" Text="NA"></asp:Label>
                                        </div>
                                        <div class="col-lg-6">
                                            <span class="form-control-label  font-weight-normal">Father Name</span>
                                            <asp:Label ID="lblfname" CssClass="lbld" runat="server" Text="NA"></asp:Label>
                                        </div>
                                        <div class="col-lg-6">
                                            <span class="form-control-label  font-weight-normal">Gender</span>
                                            <asp:Label ID="lblgender" CssClass="lbld" runat="server" Text="NA"></asp:Label>

                                        </div>
                                        <div class="col-lg-6">
                                            <span class="form-control-label  font-weight-normal">Date of Birth</span>
                                            <asp:Label ID="lbldob" CssClass="lbld" runat="server" Text="NA"></asp:Label>

                                        </div>
                                    </div>
                                    <hr />
                                    <h6 class="heading-small my-0">Contact Details</h6>
                                    <div class="row p-2">
                                        <div class="col-lg-6">
                                            <span class="form-control-label  font-weight-normal">Mobile No.</span>
                                            <asp:Label ID="lblmobileno" runat="server" CssClass="lbld" Text="NA"></asp:Label>
                                        </div>
                                        <div class="col-lg-6">
                                            <span class="form-control-label  font-weight-normal">Email</span>
                                            <asp:Label ID="lblemail" CssClass="lbld" runat="server" Text="NA"></asp:Label>
                                        </div>
                                        <div class="col-lg-6">
                                            <span class="form-control-label  font-weight-normal">State</span>
                                            <asp:Label ID="lblstate" CssClass="lbld" runat="server" Text="NA"></asp:Label>
                                        </div>
                                        <div class="col-lg-6">
                                            <span class="form-control-label  font-weight-normal">District</span>
                                            <asp:Label ID="lbldistrict" CssClass="lbld" runat="server" Text="NA"></asp:Label>
                                        </div>
                                        <div class="col-lg-8">
                                            <span class="form-control-label  font-weight-normal">Address</span>
                                            <asp:Label ID="lbladdress" CssClass="lbld" runat="server" Text="NA"></asp:Label>

                                        </div>
                                    </div>
                                         <hr />
                                    <h6 class="heading-small my-0">Journey Details</h6>
                                    <div class="row p-2">
                                         <div class="col-lg-8">
                                            <span class="form-control-label  font-weight-normal">Transaction Ref No</span>
                                            <asp:Label ID="lbltransrefno" runat="server" CssClass="lbld" Text="NA"></asp:Label>
                                        </div>
                                        <div class="col-lg-8">
                                            <span class="form-control-label  font-weight-normal">Route</span>
                                            <asp:Label ID="lblRoute" runat="server" CssClass="lbld" Text="NA"></asp:Label>
                                        </div>
                                        <div class="col-lg-4"></div>
                                        <div class="col-lg-8">
                                            <span class="form-control-label  font-weight-normal">Stations</span>
                                            <asp:Label ID="lblstations" CssClass="lbld" runat="server" Text="NA"></asp:Label>

                                        </div>
                                        <div class="col-lg-4"></div>
                                        <div class="col-lg-8">
                                            <span class="form-control-label  font-weight-normal">Service Type</span>
                                            <asp:Label ID="lblservicetype" CssClass="lbld" runat="server" Text="NA"></asp:Label>
                                        </div>
                                        <div class="col-lg-4"></div>
                                        <div class="col-lg-6">
                                            <span class="form-control-label  font-weight-normal">Validity</span>
                                            <asp:Label ID="lblvalidity" CssClass="lbld" runat="server" Text="NA"></asp:Label>
                                        </div>

                                    </div>
                                         <hr />
                                    <h6 class="heading-small my-0">Applicable Charges</h6>
                                    <div class="row p-2">
                                        <div class="col-lg-4">
                                            <span class="form-control-label  font-weight-normal">Pass Amount</span>
                                            <asp:Label ID="lblPassamount" runat="server" CssClass="lbld" Text="NA"></asp:Label>
                                        </div>
                                        <div class="col-lg-4">
                                            <span class="form-control-label  font-weight-normal" style="line-height: 4px;">
                                                <asp:Label ID="lblExtra_Charges" runat="server"></asp:Label></span>
                                            <asp:Label ID="lblExtrachrge" CssClass="lbld" runat="server" Text="NA"></asp:Label>

                                        </div>
                                        <div class="col-lg-4">
                                            <span class="form-control-label  font-weight-normal">Tax Amount</span>
                                            <asp:Label ID="lbltaxamt" CssClass="lbld" runat="server" Text="NA"></asp:Label>
                                        </div>
                                    </div>
                                   
                                </div>
                                <div class="col-lg-5 text-center">
                                    <div class="row mt-5 pt-5">
                                        <div class="col-md-12" style="text-align: center;">
                                            <h3 class="text-danger font-weight-bold">Total Amount To Be Recieved<br />
                                            <asp:Label ID="lblAmountToRecieved" runat="server" Font-Bold="True" Text="NA"></asp:Label>
                                                <i class="fa fa-rupee"></i>
                                            </h3>
                                        </div>
                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-lg-12">
                                              <asp:LinkButton ID="lbtnproceed" OnClick="lbtnproceed_Click" runat="server" CssClass="btn btn-success" CommandName="PAYNOW"> <i class="fa fa-rupee" ></i> Payment Recieved</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    
                </div>
            </div>
        </div>
        <div class="row">
            <cc1:ModalPopupExtender ID="mpConfirmation" runat="server" PopupControlID="pnlConfirmation" CancelControlID="lbtnNoConfirmation"
                TargetControlID="Button3" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlConfirmation" runat="server" Style="width: 500px !important">
                <div class="card">
                    <div class="card-header">
                        <h4 class="card-title text-left mb-0">Please Confirm
                        </h4>
                    </div>
                    <div class="card-body text-left pt-2" style="min-height: 100px;">
                        <asp:Label ID="lblConfirmation" runat="server" Text="Do you want to save ?"></asp:Label>

                        <div style="margin-top: 20px; text-align: right;">
                            <asp:LinkButton ID="lbtnYesConfirmation" OnClick="lbtnYesConfirmation_Click" runat="server" CssClass="btn btn-success btn-sm"> <i class="fa fa-check"></i> Yes </asp:LinkButton>
                            <asp:LinkButton ID="lbtnNoConfirmation" runat="server" CssClass="btn btn-danger btn-sm ml-2"> <i class="fa fa-times"></i> No </asp:LinkButton>
                        </div>
                        <div style="visibility: hidden;">
                            <asp:Button ID="Button3" runat="server" Text="" />
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>



</asp:Content>





