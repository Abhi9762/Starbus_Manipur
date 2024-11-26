<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/mainMasterPage.master" CodeFile="PassStatus.aspx.cs" Inherits="PassStatus" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link href="../assets/css/travelllerStepProgressBar.css" rel="stylesheet" />
    <style>
         #progressbar li {
            width: 25% !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-content">
        <div class="container">
            <div class="row my-3">
                <div class="col-11 offset-1">
                    <ul id="progressbar">
                        <li class="text-center active" id="apply_one"><strong>Apply for New Pass</strong></li>
                        <li class="text-center active" id="confirm_two"><strong>Confirm Details</strong></li>
                        <li class="text-center active" id="confirm_three"><strong>Payment</strong></li>
                        <li class="text-center active" id="confirm_four"><strong>Download</strong></li>
                    </ul>
                </div>

            </div>
            <div class="row my-3" style="padding-bottom: 64px;">
                <div class="col-md-12">
                    <div class="card">
                        <div class="card-header">
                            <div class="card-title">
                                Pass Request Status <i class="fa fa-info-circle" style="font-size: 15px; color: #c6c2c2;"
                                    data-toggle="tooltip" data-placement="top" title="Your Pass Request process has been completed"></i>
                            </div>
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-12 text-center">
                                    <i class="fa fa-check-circle-o" style="font-size: 125px; color: green;"></i>
                                    <p>
                                        Thanks for using Our Online Booking Service.<br />
                                        Your Transaction has been completed successfully
                                    </p>
                                    <p>
                                        Please note your transaction reference number is - 
                                            <asp:Label ID="lblrefno" runat="server" Text="102J150520201001" Style="font-weight: bold;"></asp:Label>

                                    </p>

                                    <span>&nbsp;Please use this transaction reference number to track status of your pass request</span>
                                </div>
                            </div>
                            <div class="row mt-2">
                                <div class="col-md-12 text-center">
                                    <asp:LinkButton ID="btnprintpass" runat="server" OnClick="btnprintpass_Click" CssClass="submit-btn btn btn-success"><i class="fa fa-print"></i> Click Here to Print Pass</asp:LinkButton>
                                    <asp:LinkButton ID="btnprintrecipt" runat="server" OnClick="btnprintrecipt_Click" CssClass="submit-btn btn btn-warning"><i class="fa fa-comment"></i> Click Here to Print Reciept</asp:LinkButton>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <cc1:ModalPopupExtender ID="mpPass" runat="server" PopupControlID="pnlPass" TargetControlID="Button22"
                    CancelControlID="lbtnClose" BackgroundCssClass="modalBackground">
                </cc1:ModalPopupExtender>
                <asp:Panel ID="pnlPass" runat="server" Style="position: fixed;">
                    <div class="modal-content mt-5">
                        <div class="modal-header">
                            <div class="col-md-10">
                                <h3 class="m-0">Bus Pass Print</h3>
                            </div>

                                 <div class="col-md-2 text-right">
                            <asp:LinkButton ID="lbtnClose" runat="server" Style="color: Red; cursor: pointer;"><i class="fa fa-times" style="font-size:26pt;" ></i></asp:LinkButton>
                        </div>
                        </div>
                        <div class="modal-body p-0">
                            <embed src="Bus_Pass.aspx" style="height: 65vh; width: 85vw" />
                        </div>
                    </div>
                    <br />
                    <div style="visibility: hidden;">
                        <asp:Button ID="Button22" runat="server" Text="" />
                    </div>
                </asp:Panel>
            </div>
            <div class="row">
                <cc1:ModalPopupExtender ID="mppassreceipt" runat="server" PopupControlID="pnlpassreceipt" TargetControlID="Button1"
                    CancelControlID="lbtnreiptclose" BackgroundCssClass="modalBackground">
                </cc1:ModalPopupExtender>
                <asp:Panel ID="pnlpassreceipt" runat="server" Style="position: fixed;">
                    <div class="modal-content mt-5">
                        <div class="modal-header">
                            <div class="col-md-10">
                                <h3 class="m-0">Bus Pass Apply Receipt</h3>
                            </div>
                             <div class="col-md-2 text-right">
                            <asp:LinkButton ID="lbtnreiptclose" runat="server" Style="color: Red; cursor: pointer;"><i class="fa fa-times" style="font-size:26pt;" ></i></asp:LinkButton>
                        </div>
                        </div>
                        <div class="modal-body p-0">
                            <embed src="Pass_reciept.aspx" style="height:200px;width: 35vw;" />
                        </div>
                    </div>
                    <br />
                    <div style="visibility: hidden;">
                        <asp:Button ID="Button1" runat="server" Text="" />
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>

