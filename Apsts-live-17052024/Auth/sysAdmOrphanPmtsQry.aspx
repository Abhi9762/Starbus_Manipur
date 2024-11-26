<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/SysAdmmaster.master" AutoEventWireup="true" CodeFile="sysAdmOrphanPmtsQry.aspx.cs" Inherits="Auth_sysAdmOrphanPmtsQry" Async="true" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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

            $('[id*=txttransdateOrphan]').datepicker({
                endDate: preDate,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            });
        });
        
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="container-fluid" style="padding-top: 20px; padding-bottom: 30px;">
        <div class="card card-stats">
            <div class="card-header">
                <div class="row">

                    <div class="col">
                        <h5 class="mb-0">Transaction Pending for Settlement </h5>
                        <p class="mb-0">Transaction Date - <span class="font-weight-bold">
                            <asp:Label ID="lblTxnDate" runat="server"></asp:Label>
                        </span>, Payment Gateway - <span class="font-weight-bold">
                            <asp:Label ID="lblPg" runat="server"></asp:Label>
                        </span></p>
                    </div>

                    <div class="col-auto text-right">
                        <asp:LinkButton ID="lbtnback" ToolTip="Back To PGMIS" OnClick="lbtnback_Click" runat="server" CssClass="btn btn-warning">
                                            <i class="fa fa-backward "></i> Back To PGMIS 
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
            <div class="card-body" style="min-height: 80vh;">
                <div class="row">
                    <div class="col-lg-6 border-right border-dark">
                        <asp:GridView ID="gvOrphanTrans" OnPageIndexChanging="gvOrphanTrans_PageIndexChanging" runat="server" PageSize="1500" AutoGenerateColumns="False" GridLines="None" AllowSorting="true" AllowPaging="False" CssClass="table table-striped table-hover"
                            HeaderStyle-CssClass="thead-light font-weight-bold" DataKeyNames="txndate,txn_refno,txnamount" ClientIDMode="Static">
                            <Columns>
                                <asp:TemplateField HeaderText="Transaction Number">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTXN_REF_NO" runat="server" Text='<%# Eval("txn_refno") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Transaction Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lbltxndate" runat="server" Text='<%# Eval("txndate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Transaction Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTXN_AMOUNT" runat="server" Text='<%# Eval("txnamount") %>'></asp:Label><i class="fa fa-rupee-sign ml-1"></i>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                        </asp:GridView>
                        <div class="text-center busListBox" id="dvOrphanTrans" runat="server"
                            style="padding-top: 50px; padding-bottom: 50px; font-size: 20px; font-weight: bold;"
                            visible="true">
                            
                           <h3 class="mb-0 text-muted">No Orphan Transaction Pending for Settlement </h3>
                        </div>
                    </div>
                    <div class="col-lg-6 text-center">

                        <div class="text-center busListBox" id="Div1" runat="server"
                            style="padding-top: 50px; padding-bottom: 50px; font-size: 20px; font-weight: bold;"
                            visible="true">
                            <h3 class="mb-0">Start Query & Process the Orphan Transaction</h3>
                            <span style="font-size: 8pt;">
                                <asp:Label Style="font-size:13px" ID="lblmsg" runat="server" Text="(Start Query Button will be available only if pending
                                transactions are there. To get the list of pending transactions please select date & Payment Gateway
                                and click on Search button. )"></asp:Label>
                            </span>
                        </div>
                        <asp:Button ID="btnStart" runat="server" Visible="false" OnClick="btnStart_Click" Text="Start Query Process" CssClass="btn btn-primary centerSc"
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
	
   <%-- <script type="text/javascript">
        $('#gvOrphanTrans').DataTable({
            "pageLength": 15,
            dom: 'Bfrtip',
            "bSort": false,
            buttons: [
                'csv', 'excel', 'pdf'
            ]
        });
    </script>--%>
</asp:Content>

