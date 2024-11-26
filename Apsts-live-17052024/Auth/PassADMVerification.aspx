<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Auth/PassAdminmaster.master" CodeFile="PassADMVerification.aspx.cs" Inherits="Auth_PassADMVerification" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };
    </script>
    <script type="text/javascript">
        $(window).on('load', function () {
            HideLoading();
        });
        function ShowLoading() {
            var div = document.getElementById("loader");
            div.style.display = "block";
        }
        function HideLoading() {
            var div = document.getElementById("loader");
            div.style.display = "none";
        }
    </script>
    <style>
        .border-right {
            border-right: 1px solid #e6e6e6;
        }

        .modalBackground {
            background-color: black;
            opacity: 0.6;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   

    <div class="container-fluid" style="padding-top:20px; padding-bottom: 30px;">
        <div class="row align-items-center">
            <div class="col-lg-12">
                <div class="card" style="min-height: 50vh">
                    <div class="card-header">
                        <div class="row">
                            <div class="col-md-6">
                                <h4>Pass Request For
                                    <asp:Label ID="lblpass" runat="server"></asp:Label>
                                </h4>
                            </div>
                            <div class="col-md-6 text-right">
                                <asp:LinkButton ID="lbtnback" runat="server" OnClick="lbtnback_Click" CssClass="btn btn-warning" OnClientClick="return ShowLoading();"> <i class="fa fa-backward"></i>  Back To Dashboard</asp:LinkButton>
                            </div>
                        </div>

                    </div>
                    <div class="card-body pt-2">

                        <div class="row">
                            <div class="col-lg-9">
                                <div class="row">
                                    <div class="col-md-6 border-right">
                                        <h5 class="text-danger font-weight-bold mt-3">Personal Details</h5>
                                        <div class="pass-info row">
                                            <div class="col-6">
                                                <p class="mb-1" style="font-size: 14px; line-height: 14px;">
                                                    Traveller Name
                                                </p>
                                                <h5>
                                                    <asp:Label ID="lblname" runat="server" Text="NA"></asp:Label></h5>

                                                <p class="mb-1 mt-3" style="font-size: 14px; line-height: 14px;">
                                                    Father's Name
                                                </p>
                                                <h5>
                                                    <asp:Label ID="lblfname" runat="server" Text="NA"></asp:Label>
                                                </h5>
                                                <p class="mb-1 mt-3" style="font-size: 14px; line-height: 14px;">
                                                    Address
                                                </p>
                                                <h5>
                                                    <asp:Label ID="lbladdress" runat="server" Text="NA"></asp:Label>
                                                </h5>
                                            </div>
                                            <div class="col-6">
                                                <p class="mb-1" style="font-size: 14px; line-height: 14px;">
                                                    Gender/Age
                                                </p>
                                                <h5>
                                                    <asp:Label ID="lblgender" runat="server" Text="NA"></asp:Label>
                                                    ,
                                                    <asp:Label ID="lblage" runat="server" Text="NA"></asp:Label>
                                                </h5>
                                                <p class="mb-1 mt-3" style="font-size: 14px; line-height: 14px;">
                                                    Mobile/Email
                                                </p>
                                                <h5>

                                                    <asp:Label ID="lblmobile" runat="server" Text="NA"></asp:Label>
                                                    ,
                                                    <asp:Label ID="lblemail" runat="server" Text="NA"></asp:Label>
                                                </h5>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6 border-right">
                                        <h5 class="text-danger font-weight-bold mt-3">Amount Details</h5>
                                        <div class="Amount-info row">
                                            <div class="col-6">
                                                <p class="mb-1" style="font-size: 14px; line-height: 14px;">
                                                    Pass Amount
                                                </p>
                                                <h5>
                                                    <asp:Label ID="lblpassamt" runat="server" Text="0"></asp:Label>
                                                    <i class="fa fa-rupee"></i>
                                                </h5>
                                                <p class="mb-1 mt-3" style="font-size: 14px; line-height: 14px;">
                                                    Extra Charges
                                                </p>
                                                <h5>
                                                    <asp:Label ID="lblextrachrge" runat="server" Text="0"></asp:Label>
                                                    <i class="fa fa-rupee"></i>
                                                </h5>

                                            </div>
                                            <div class="col-6">
                                                <p class="mb-1" style="font-size: 14px; line-height: 14px;">
                                                    Tax Amount
                                                </p>
                                                <h5>
                                                    <asp:Label ID="lblTax" runat="server" Text="0"></asp:Label>
                                                    <i class="fa fa-rupee"></i>
                                                </h5>
                                                <p class="mb-1 mt-3" style="font-size: 14px; line-height: 14px;">
                                                    <asp:Label ID="lblrenew" runat="server" Text=""></asp:Label>
                                                </p>
                                                <h5>
                                                    <asp:Label ID="lblrenewamt" runat="server" Font-Bold="True" Text=""></asp:Label>
                                                </h5>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6 border-right">
                                        <h5 class="text-danger font-weight-bold mt-3">Application Details</h5>
                                        <div class="pass-info row">
                                            <div class="col-6">
                                                <p class="mb-1" style="font-size: 14px; line-height: 14px;">
                                                    Transaction Ref No
                                                </p>
                                                <h5>
                                                    <asp:Label ID="lbltransrefno" runat="server" Text="NA"></asp:Label>
                                                </h5>
                                                <p class="mb-1 mt-3" style="font-size: 14px; line-height: 14px;">
                                                    Pass Category
                                                </p>
                                                <h5>
                                                    <asp:Label ID="lblPassType" runat="server" Text="NA"></asp:Label>
                                                    (<asp:Label ID="lblPassenger" runat="server" Text="NA"></asp:Label>)
                                                </h5>
                                            </div>
                                            <div class="col-6">
                                                <p class="mb-1" style="font-size: 14px; line-height: 14px;">
                                                    Stations
                                                </p>
                                                <h5>
                                                    <asp:Label ID="lblStation" runat="server" Text="NA"></asp:Label>
                                                </h5>
                                                <p class="mb-1 mt-3" style="font-size: 14px; line-height: 14px;">
                                                    Valid
                                                </p>
                                                <h5>
                                                    <asp:Label ID="lblValidFrom" runat="server" Text="NA"></asp:Label>
                                                    to
                                                    <asp:Label ID="lblValidUpto" runat="server" Text="NA"></asp:Label>
                                                </h5>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6 border-right">
                                        <h5 class="text-danger font-weight-bold mt-3 mb-0">Upload Details</h5>
                                        <span style="font-size: 10pt;">(You can download/View uploaded document for verification)</span>
                                        <div class="Upload-info row mt-3">
                                            <div class="col-md-6 border-right">

                                                <p class="mb-1" style="font-size: 14px; line-height: 14px;">
                                                    Document
                                                </p>
                                                <div class="row">
                                                    <div class="col-md-6 border-right">
                                                        <p class="mb-0">
                                                            <span class="pr-1 text-muted" style="font-size: 14px;">
                                                                <asp:Label ID="lbladdressproof" runat="server" Text="Address Proof"></asp:Label>
                                                            </span>
                                                        </p>
                                                        <asp:HiddenField ID="hdaddressproof" runat="server" />
                                                        <asp:LinkButton ID="lbtnviewaddressproof" runat="server" OnClick="lbtnviewaddressproof_Click" CssClass="btn btn-success btn-sm"> View </asp:LinkButton>
                                                        <asp:LinkButton ID="lbtndownloadaddressproof" runat="server" OnClick="lbtndownloadaddressproof_Click" CssClass="btn btn-warning btn-sm"> Download </asp:LinkButton>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <p class="mb-0">
                                                            <span class="pr-1 text-muted" style="font-size: 14px;">
                                                                <asp:Label ID="lblidproof" runat="server" Text="Id Proof"></asp:Label>
                                                            </span>
                                                        </p>
                                                        <asp:HiddenField ID="hdidproof" runat="server" />
                                                        <asp:LinkButton ID="lbtnviewidproof" runat="server" OnClick="lbtnviewidproof_Click" CssClass="btn btn-success btn-sm"> View </asp:LinkButton>
                                                        <asp:LinkButton ID="lbtndownloadproof" runat="server" OnClick="lbtndownloadproof_Click" CssClass="btn btn-warning btn-sm"> Download </asp:LinkButton>
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="col-md-4">
                                                <p class="mb-1" style="font-size: 14px; line-height: 14px;">
                                                    Photo
                                                </p>
                                                <asp:Image ID="img" class="mt-2" runat="server" ImageUrl="~/PassUTC/assets/img/UTC-LOGO-WaterMark.png" Style="border-width: 0px; width: 80px; border: 1px solid;" />
                                                <asp:Label ID="lblimg" runat="server" Font-Bold="True" Text="NA" Visible="false"></asp:Label>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <div class="row mt-5">
                                    <div class="col-md-12 text-center">
                                        <asp:LinkButton ID="lbtnverify" runat="server" OnClick="lbtnverify_Click" CssClass="btn btn-success" OnClientClick="return ShowLoading();"> <i class="fa fa-check" ></i> Verify Pass Request </asp:LinkButton>
                                        <asp:LinkButton ID="lbtnreject" runat="server" OnClick="lbtnreject_Click" CssClass="btn btn-danger" OnClientClick="return ShowLoading();"> <i class="fa fa-times"></i> Reject Pass Request </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-3">
                                <h5 class="text-danger font-weight-bold mt-3">Transaction Status Log</h5>

                                <asp:Repeater ID="rptTxnLog" runat="server">
                                    <ItemTemplate>
                                        <div class="row mb-0">
                                            <div class="col-md-6">
                                                <p class="mb-2" style="font-size: 13px; line-height: 14px;">
                                                    <%# Eval("txtstatusname")%>
                                                </p>
                                                <p style="font-size: 13px; line-height: 14px; text-transform: initial;">
                                                    <%# Eval("update_on")%>
                                                </p>
                                            </div>
                                            <div class="col-md-3">
                                                <p style="font-size: 13px; line-height: 14px;">
                                                    <%# Eval("updated_by")%>
                                                </p>
                                            </div>
                                            <div class="col-md-3">
                                                <p style="font-size: 13px; line-height: 14px;">
                                                    <%# Eval("ip_address")%>
                                                </p>
                                            </div>
                                        </div>
                                        <hr style="margin-top: 0; margin-bottom: 7px;" />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>


        <div class="row">
            <cc1:ModalPopupExtender ID="mpConfirmation" runat="server" PopupControlID="pnlConfirmation"
                CancelControlID="lbtnNoConfirmation" TargetControlID="Button4" BackgroundCssClass="modalBackground" BehaviorID="bvConfirm">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlConfirmation" runat="server" Style="position: fixed;">
                <div class="card">
                    <div class="card-header">
                        <h6 class="card-title text-left mb-0">Please Confirm
                        </h6>
                    </div>
                    <div class="card-body text-left pt-2" style="min-height: 100px;">
                        <div class="row" id="dvreason" runat="server" visible="false">
                            <div class="col-lg-6">Reason of Rejection <span class="text-danger">*</span> </div>
                            <div class="col-lg-6">
                                <asp:TextBox CssClass="form-control" runat="server" ID="txtreason" Height="50px" TextMode="MultiLine" MaxLength="100" ToolTip="Enter Rejection Reason"
                                    Text="" Style="display: inline;"></asp:TextBox>
                            </div>
                        </div>
                        <asp:Label ID="lblConfirmation" runat="server" Text="Do you want to save ?"></asp:Label>
                        <div style="width: 100%; margin-top: 20px; text-align: right;">
                            <asp:LinkButton ID="lbtnYesConfirmation" runat="server" OnClick="lbtnYesConfirmation_Click" CssClass="btn btn-success btn-sm" OnClientClick="$find('bvConfirm').hide();ShowLoading();"> <i class="fa fa-check"></i> Yes </asp:LinkButton>
                            <asp:LinkButton ID="lbtnNoConfirmation" runat="server" CssClass="btn btn-danger btn-sm ml-2"> <i class="fa fa-times"></i> No </asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div style="visibility: hidden;">
                    <asp:Button ID="Button4" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>
        <div class="row">
            <cc1:ModalPopupExtender ID="mpsuccess" runat="server" PopupControlID="pnlsuccess"
                CancelControlID="Button1" TargetControlID="Button6" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlsuccess" runat="server" Style="position: fixed;">
                <div class="card" style="min-width: 350px;">
                    <div class="card-header">
                        <h6 class="card-title">Confirm
                        </h6>
                    </div>
                    <div class="card-body" style="min-height: 100px;">
                        <asp:Label ID="lblsuccessmsg" runat="server"></asp:Label>
                        <div style="width: 100%; margin-top: 20px; text-align: right;">
                            <asp:LinkButton ID="lbtnsuccessclose1" runat="server" OnClick="lbtnsuccessclose1_Click" CssClass="btn btn-success"> <i class="fa fa-check"></i> OK </asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div style="visibility: hidden;">
                    <asp:Button ID="Button6" runat="server" Text="" />
                    <asp:Button ID="Button1" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>
        <div class="row">
            <cc1:ModalPopupExtender ID="mperror" runat="server" PopupControlID="pnlerror" CancelControlID="Button3"
                TargetControlID="Button2" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlerror" runat="server" Style="width: 500px !important">
                <div class="card">
                    <div class="card-header">
                        <h4 class="card-title text-left mb-0">Please Check
                        </h4>
                    </div>
                    <div class="card-body text-left pt-2" style="min-height: 100px;">
                        <asp:Label ID="lblerrormsg" runat="server"></asp:Label>
                        <div style="margin-top: 20px; text-align: right;">
                            <asp:LinkButton ID="lbtnclose" runat="server" OnClick="lbtnclose_Click" CssClass="btn btn-success btn-sm ml-2"> <i class="fa fa-check"></i> OK </asp:LinkButton>
                        </div>
                        <div style="visibility: hidden;">
                            <asp:Button ID="Button2" runat="server" Text="" />
                            <asp:Button ID="Button3" runat="server" Text="" />
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>
        <div class="row">
            <cc1:ModalPopupExtender ID="mpviewdocment" runat="server" PopupControlID="pnlviewdocment" CancelControlID="btnclose"
                TargetControlID="Button5" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlviewdocment" runat="server">
                <div class="card" style="margin-top: 100px;">
                    <div class="card-header">

                        <div class="row">
                            <div class="col-lg-6">
                                <h4 class="card-title text-left mb-0">View Document
                                </h4>
                            </div>
                            <div class="col-lg-6 text-right">
                                <asp:LinkButton ID="btnclose" runat="server" CssClass="btn btn-danger"> <i class="fa fa-times"></i></asp:LinkButton>

                            </div>
                        </div>

                    </div>
                    <div class="card-body text-left pt-2" style="min-height: 100px;">
                        <embed src="ViewDocument.aspx" style="height: 85vh; width: 80vw" />
                        <div style="visibility: hidden;">
                            <asp:Button ID="Button5" runat="server" Text="" />
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>

</asp:Content>


