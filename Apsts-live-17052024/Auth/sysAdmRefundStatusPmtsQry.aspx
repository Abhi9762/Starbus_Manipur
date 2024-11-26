<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/SysAdmmaster.master" AutoEventWireup="true" CodeFile="sysAdmRefundStatusPmtsQry.aspx.cs" Inherits="Auth_sysAdmRefundStatusPmtsQry" Async="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .modalBackground {
            background-color: black;
            opacity: 0.6;
        }

        .border-right {
            border-right: 1px solid #050505;
        }

        .centerSc {
            width: 35%;
        }

        .table td, .table th {
            padding: 0.80rem;
        }
    </style>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid" style="padding-top: 0px; padding-bottom: 30px;">
        <div class="card card-stats">
            <div class="card-header">
                <div class="row">

                    <div class="col">
                        <h5 class="mb-0">Transaction Pending for Refund Status </h5>
                        <p class="mb-0">
                            Refund Initiation Date - <span class="font-weight-bold">
                                <asp:Label ID="lblTxnDate" runat="server"></asp:Label>
                            </span>, Payment Gateway - <span class="font-weight-bold">
                                <asp:Label ID="lblPg" runat="server"></asp:Label>
                            </span>
                        </p>
                    </div>

                    <div class="col-auto text-right">
                        <asp:LinkButton ID="lbtnback" ToolTip="Back To PGMIS" runat="server" OnClick="lbtnback_Click" CssClass="btn btn-warning">
                                            <i class="fa fa-backward "></i> Back To PGMIS 
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
            <div class="card-body" style="min-height: 80vh;">
                <div class="row">
                    <div class="col-md-2 border-right" style="min-height: 80vh;">
                        <h5>Please Note</h5>
                        <p>
                           All transactions eligible for check refund status can be viewed here.
                        </p>
                        <p>
                            You can manually initiate the check refund status process of a single transaction by 
                            clicking on <a class="btn btn-success btn-sm disabled"><i class="fa fa-send text-white"></i></a>  button
                            and to initiate all transactions by 
                            clicking on <a class="btn btn-danger btn-sm disabled text-white py-2 px-1" style="height:40px; width:40px; border-radius:50%;">Start</a> button.
                        </p>
                        <p>
                            Check refund status can be success, pending and exception (these kind of transaction neither setteled nor pending).
                        </p>
                        <p>
                            Exceptional transactions will be addressed or setteled using manual process. 
                            <a href="AdmPmtGatewayExcepption.aspx" class="btn btn-outline-info btn-sm">Click here</a> to view exception list.
                        </p>
                        <p>
                            You can download pending check refund status transaction detail in CSV, EXCEL and PDF document.
                        </p>

                    </div>
                    <div class="col-lg-6 border-right">
                        <div class="col-lg-12">
                            <h4 class="mb-0">Transaction Pending for Refund Status </h4>
                        </div>
                        <hr />
                        <asp:GridView ID="gvRefundStatusPending" runat="server" PageSize="500" OnPageIndexChanging="gvRefundStatusPending_PageIndexChanging" OnRowCommand="gvRefundStatusPending_RowCommand" AutoGenerateColumns="False" GridLines="None" AllowSorting="true" AllowPaging="false" CssClass="table table-striped table-hover"
                            HeaderStyle-CssClass="thead-light font-weight-bold" DataKeyNames="trans_refno,trans_date,trans_amount,trans_cancel_refno,cancellation_date,cancel_amount,refund_ref_no,refund_date,refund_amount" ClientIDMode="Static">
                            <Columns>
                               <asp:TemplateField HeaderText="Transaction">
                                    <ItemTemplate>
                                        <asp:Label ID="lbltrans_refno" runat="server" Text='<%# Eval("trans_refno") %>'></asp:Label>
                                        <br />
                                        <i class="fa fa-clock-o"></i>
                                        <asp:Label ID="lbltrans_date" runat="server" Text='<%# Eval("trans_date") %>'></asp:Label>,
                                        <i class="fa fa-inr"></i>
                                        <asp:Label ID="lbltrans_amount" runat="server" Text='<%# Eval("trans_amount") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cancellation">
                                    <ItemTemplate>
                                        <asp:Label ID="lbltrans_cancel_refno" runat="server" Text='<%# Eval("trans_cancel_refno") %>'></asp:Label>
                                        <br />
                                        <i class="fa fa-clock-o"></i>
                                        <asp:Label ID="lblcancellation_date" runat="server" Text='<%# Eval("cancellation_date") %>'></asp:Label>,
                                        <i class="fa fa-inr"></i>
                                        <asp:Label ID="lblcancel_amount" runat="server" Text='<%# Eval("cancel_amount") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Refund Initiation">
                                    <ItemTemplate>
                                        <asp:Label ID="lblrefund_ref_no" runat="server" Text='<%# Eval("refund_ref_no") %>'></asp:Label>
                                        <br />
                                        <i class="fa fa-clock-o"></i>
                                        <asp:Label ID="lblrefund_date" runat="server" Text='<%# Eval("refund_date") %>'></asp:Label>,
                                        <i class="fa fa-inr"></i>
                                        <asp:Label ID="lblrefund_amount" runat="server" Text='<%# Eval("refund_amount") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnrefunded" ToolTip="Refund Status Transaction" runat="server" CssClass="btn btn-success btn-sm" OnClientClick="return ShowLoading()" CommandName="RefundStatus" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'> <i class="fa fa-forward"></i> </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                        </asp:GridView>
                        <div class="text-center busListBox" id="dvRefund" runat="server"
                            style="padding-top: 50px; padding-bottom: 50px; font-size: 20px; font-weight: bold;"
                            visible="false">

                            <h3 class="mb-0 text-muted">No Refund Transaction Pending for Refund</h3>
                        </div>
                    </div>
                    <div class="col-lg-4 text-center border-right">

                        <div class="text-center busListBox" id="Div1" runat="server"
                            style="padding-top: 50px; padding-bottom: 50px; font-size: 20px; font-weight: bold;"
                            visible="true">
                            <h4 class="mb-0">Start Query & Process the Pending Refund Initiated Transaction</h4>
                            <span>
                                <label>At a time refund of single transaction can be initiated.<br />
                                    Please click on <a class="btn btn-success btn-sm disabled"><i class="fa fa-forward text-white"></i></a> against the transaction</label>
                            </span>
                        </div>
                        <asp:Button ID="btnStart" runat="server" Visible="false" OnClick="btnStart_Click" OnClientClick="return ShowLoading()" Text="Start Query Process" CssClass="btn btn-primary"
                            Font-Size="18px" />
                    </div>

                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <cc1:ModalPopupExtender ID="mpConfirmation" runat="server" PopupControlID="pnlConfirmation"
            CancelControlID="lbtnNoConfirmation" TargetControlID="Button2" BackgroundCssClass="modalBackground" BehaviorID="bvConfirm">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlConfirmation" runat="server" Style="position: fixed;">
            <div class="card" style="width: 350px;">
                <div class="card-header">
                    <h4 class="card-title text-left mb-0">Confirmation
                    </h4>
                </div>

                <div class="card-body text-left pt-2" style="min-height: 100px;">
                     <asp:HiddenField ID="hdgrdindex" runat="server" />
                    <asp:Label ID="lblConfirmation" runat="server" Text="Do you want to save ?"></asp:Label>
                    <div style="width: 100%; margin-top: 20px; text-align: right;">
                        <asp:LinkButton ID="lbtnYesConfirmation" runat="server" OnClick="lbtnYesConfirmation_Click" CssClass="btn btn-sm btn-success btn-sm"> <i class="fa fa-check"></i> Yes </asp:LinkButton>
                        <asp:LinkButton ID="lbtnNoConfirmation" runat="server" CssClass="btn btn-sm btn-danger btn-sm ml-2"> <i class="fa fa-times"></i> No </asp:LinkButton>
                    </div>
                </div>
            </div>
            <div style="visibility: hidden;">
                <asp:Button ID="Button2" runat="server" Text="" />
            </div>
        </asp:Panel>
    </div>
    <div class="row">
        <cc1:ModalPopupExtender ID="mperror" runat="server" PopupControlID="pnlerror" CancelControlID="lbtnclose1"
            TargetControlID="Button1" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlerror" runat="server" Style="position: fixed;">
            <div class="card" style="width: 350px;">
                <div class="card-header">
                    <h4 class="card-title text-left mb-0">Please Check
                    </h4>
                </div>
                <div class="card-body text-left pt-2" style="min-height: 100px;">
                    <asp:Label ID="lblerrormsg" runat="server" Text="Please Check entered values."></asp:Label>
                    <div style="width: 100%; margin-top: 20px; text-align: right;">
                        <asp:LinkButton ID="lbtnclose1" runat="server" CssClass="btn btn-danger btn-sm ml-2"> <i class="fa fa-times"></i> OK </asp:LinkButton>
                    </div>
                </div>
            </div>
            <div style="visibility: hidden;">
                <asp:Button ID="Button1" runat="server" Text="" />
            </div>
        </asp:Panel>
    </div>
</asp:Content>

