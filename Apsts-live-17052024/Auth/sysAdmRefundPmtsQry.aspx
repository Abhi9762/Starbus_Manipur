<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/SysAdmmaster.master" AutoEventWireup="true" CodeFile="sysAdmRefundPmtsQry.aspx.cs" Inherits="Auth_sysAdmRefundPmtsQry" Async="true" %>

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
    <script>
        $(document).ready(function () {

            var currDate = new Date().getDate();
            var preDate = new Date(new Date().setDate(currDate - 1));
            var todayDate = new Date(new Date().setDate(currDate));
            var FutureDate = new Date(new Date().setDate(currDate + 30000));

            $('[id*=txttransdateRefund]').datepicker({
                endDate: preDate,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            });
        });


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid" style="padding-top: 0px; padding-bottom: 30px;">
        <div class="card card-stats">
            <div class="card-header">
                <div class="row">

                    <div class="col">
                        <h5 class="mb-0">Transaction Pending for Refund </h5>
                        <p class="mb-0">
                            Cancellation Date - <span class="font-weight-bold">
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
                    <div class="col-lg-6 border-right">
                        <div class="col-lg-12">
                            <h4 class="mb-0">Transaction Pending for Refund </h4>
                        </div>
                        <hr />
                        <asp:GridView ID="gvRefundTrans" runat="server" PageSize="500" OnPageIndexChanging="gvRefundTrans_PageIndexChanging" OnRowCommand="gvRefundTrans_RowCommand" AutoGenerateColumns="False" GridLines="None" AllowSorting="true" AllowPaging="false" CssClass="table table-striped table-hover"
                            HeaderStyle-CssClass="thead-light font-weight-bold" DataKeyNames="txndate,txn_refno,txn_amt,cancellation_date,pg_name,pg_id,txn_cancel_refno,bank_txn_refno,txndatee,txn_actual_amt" ClientIDMode="Static">
                            <Columns>
                                <asp:TemplateField HeaderText="Transaction">
                                    <ItemTemplate>
                                        <asp:Label ID="lbltxn_refno" runat="server" Text='<%# Eval("txn_refno") %>'></asp:Label>
                                        <br />
                                        <span class="text-muted" style="font-size: 8pt;">Date</span>
                                        <asp:Label ID="lbltxndate" runat="server" Text='<%# Eval("txndate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cancellation">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTRANS_CANCEL_REFNO" runat="server" Text='<%# Eval("txn_cancel_refno") %>'></asp:Label>
                                        <br />
                                        <span class="text-muted" style="font-size: 8pt;">Date</span>
                                        <asp:Label ID="lblCANCELLATION_DATETIME" runat="server" Text='<%# Eval("cancellation_date") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cancellation Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTRANS_AMOUNT" runat="server" Text='<%# Eval("txn_amt") %>'></asp:Label>
                                        <i class="fa fa-rupee-sign ml-0"></i>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnrefund" ToolTip="Refund Transaction" runat="server" CssClass="btn btn-success btn-sm" OnClientClick="return ShowLoading()" CommandName="Refund" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'> <i class="fa fa-forward"></i> </asp:LinkButton>
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
                    <div class="col-lg-6 text-center border-right">

                        <div class="text-center busListBox" id="Div1" runat="server"
                            style="padding-top: 50px; padding-bottom: 50px; font-size: 20px; font-weight: bold;"
                            visible="true">
                            <h4 class="mb-0">Start Query & Process the Pending Refund Transaction</h4>
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

