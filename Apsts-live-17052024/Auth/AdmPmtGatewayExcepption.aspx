<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/SysAdmmaster.master" AutoEventWireup="true" CodeFile="AdmPmtGatewayExcepption.aspx.cs" Inherits="Auth_AdmPmtGatewayExcepption" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <script type="text/javascript">
        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            var todayDate = new Date().getDate();
            var endD = new Date(new Date().setDate(todayDate));
            var currDate = new Date();

            $('[id*=txtrefunddate]').datepicker({
                 startDate: "dateToday",
                endDate: endD,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            });
            
        });
    </script>
    <style type="text/css">
        /*.modalBackground {
            background-color: black;
            opacity: 0.6;
        }*/

        /*.border-right {
            border-right: 1px solid #050505;
        }

        .centerSc {
            width: 35%;
        }

        .table td, .table th {
            font-size: 10pt;
        }

        .table td {
            font-size: 11pt;
        }*/
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid" style="padding-top: 20px; padding-bottom: 30px;">

        <div class="row align-items-center">
            <div class="col-md-12">
                <div class="card p-2 mb-4">


                    <div class="row">
                        <div class="col-auto">
                            <h2 class="mb-0">Pending Payment Exceptions </h2>
                        </div>

                        <div class="col text-right">
                            <asp:LinkButton ID="lbtnback" ToolTip="Back To PGMIS" OnClick="lbtnback_Click" OnClientClick="return ShowLoading()" runat="server" CssClass="btn btn-warning">
                                            <i class="fa fa-backward "></i> Back To PGMIS 
                            </asp:LinkButton>
                        </div>
                    </div>

                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-4">
                <div class="card">
                    <div class="card-header">
                        <div class="row">
                            <div class="col-md-6">
                                <h4 class="mb-0">Pending Exceptions</h4>
                            </div>
                            <div class="col-md-6">
                                <div class="input-group mb-0">
                                    <asp:DropDownList ID="ddlExcpType" runat="server" CssClass="form-control p-1" Style="width: 100px;">
                                        <asp:ListItem Text="All" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Double Verification Failer" Value="D"></asp:ListItem>
                                        <asp:ListItem Text="Refund Initition Failer" Value="R"></asp:ListItem>
                                        <asp:ListItem Text="Refund Status Failer" Value="S"></asp:ListItem>
                                        <asp:ListItem Text="Payment" Value="P"></asp:ListItem>
                                    </asp:DropDownList>
                                    <div class="input-group-append">
                                        <asp:LinkButton ID="lbtnsearch" ToolTip="Search Orphan Transaction" OnClientClick="return ShowLoading()" OnClick="lbtnsearch_Click"
                                            runat="server" CssClass="btn btn-warning btn-sm" Style="padding: 6px 12px 4px 12px;">
                                            <i class="fa fa-search"></i> 
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                        <asp:GridView ID="gvExceptionCountList" runat="server" PageSize="10" AutoGenerateColumns="False" GridLines="None" AllowSorting="true" AllowPaging="true" CssClass="table table-striped table-hover"
                            HeaderStyle-CssClass="thead-light font-weight-bold" DataKeyNames="txnDate,gatewayName,pgid" OnRowCommand="gvExceptionCountList_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="Transaction<br> Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lbltxndate1" runat="server" Text='<%# Eval("txnDate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="No. Of <br>Transaction">
                                    <ItemTemplate>
                                        <asp:Label ID="lblorphantxn" runat="server" Text='<%# Eval("record_count") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Payment<br> Gateway">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPMTGATEWAYNAME" runat="server" Text='<%# Eval("gatewayName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnSettle" CommandName="VIEWDETAIL" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' runat="server" CssClass="btn btn-sm btn-warning"><i class="fa fa-eye">&nbsp;</i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" CssClass="GridPager" />
                        </asp:GridView>

                        <div class="text-center busListBox" id="dvNoExceptionCount" runat="server"
                            style="color: #e3e3e3; padding-top: 50px; padding-bottom: 50px; font-size: 20px; font-weight: bold;">
                            No Exception Pending for Settlement<br />
                            or Please check search parameters
                        </div>

                    </div>
                </div>
            </div>
            <div class="col-md-8">
                <h5 class="mb-4">
                    <asp:Label ID="lblExcDtlHeader" runat="server"></asp:Label>
                </h5>
                <asp:GridView ID="gvExceptionSattel" runat="server" AutoGenerateColumns="false" GridLines="None" Font-Size="10pt"
                    ClientIDMode="Static" CssClass="table table-bordered" OnRowDataBound="gvExceptionSattel_RowDataBound"
                    DataKeyNames="pgid,gatewayname,inserttype,txntype,exceptiontype,exceptiontypename,orderid,txnrefno,txnamt,txndate,lastexceptionstatuscode,
                            lastexception,lastexception_date,pgstatus,actionremark,refund_refnumber,refund_datetime,refundamount,updateby,updateddatetime"
                    OnRowCommand="gvExceptionSattel_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No." ItemStyle-CssClass="class-on-element">
                            <ItemTemplate>
                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Order Details">
                            <ItemTemplate>
                                <span class="text-muted">Order Id-</span> <%#Eval("orderid") %>
                                <br />
                                <span class="text-muted">Cancellaiton Ref No-</span> <%# Eval("txnrefno") %>
                                <br />
                                <span class="text-muted">Inserted By-</span> <%# Eval("inserttype1") %>
                                <br />
                                <span class="text-muted">Eligible Refund-</span> <%# Eval("txnamt") %> ₹
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Type" ItemStyle-CssClass="class-on-element">
                            <ItemTemplate>
                                <%# Eval("exceptiontypename") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Exception" ItemStyle-CssClass="class-on-element">
                            <ItemTemplate>
                                <%# Eval("lastexception") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Refund Status" ItemStyle-CssClass="class-on-element">
                            <ItemTemplate>
                                <div class="row">
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlRefund" runat="server" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlRefund_SelectedIndexChanged" CssClass="form-control px-1">
                                            <asp:ListItem Value="O" Text="Failed/Other" Selected="True"></asp:ListItem>
                                            <asp:ListItem Value="R" Text="Refunded"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtRefundamt" runat="server" Placeholder="Refund Amount" Text='<%# Eval("txnamt") %>' CssClass="form-control"
                                            Visible="true" AutoComplete="Off" MaxLength="6"></asp:TextBox>
                                       
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtrefunddate" runat="server" Placeholder="dd/MM/yyyy" CssClass="form-control" Text='<%# Eval("refund_datetime") %>'
                                            Visible="true" AutoComplete="Off" MaxLength="10"></asp:TextBox>
                                      
                                       
                                    </div>

                                </div>
                                <div class="row mt-1">
                                    <div class="col-md-8">
                                        <asp:TextBox ID="tbRemark" runat="server" CssClass="form-control" Text="" Placeholder="Remarks" MaxLength="100" AutoComplete="Off"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtrefundrefno" runat="server" Placeholder="Refund Ref Number" CssClass="form-control" Text='<%# Eval("refund_refnumber") %>'
                                            Visible="true" AutoComplete="Off" MaxLength="50"></asp:TextBox>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Action" ItemStyle-CssClass="class-on-element">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnSave" OnClientClick="return ShowLoading()" runat="server" CssClass="btn btn-icon btn-primary btn-sm "
                                    CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="VERIFY" data-toggle="tooltip" data-placement="bottom" title="Click here to Verify">
                                                   <i class="fa fa-check"></i> Verify
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <div class="text-center busListBox" id="dvNoExceptionSattel" runat="server"
                    style="color: #e3e3e3; padding-top: 50px; padding-bottom: 50px; font-size: 20px; font-weight: bold;">
                    <h4 class="mb-0">Click on 
                        <a class="btn btn-sm btn-warning disabled mr-1"><i class="fa fa-eye"></i></a>button for manual refund of the transaction

                    </h4>
                </div>
            </div>
        </div>
        <div class="row">
            <cc1:ModalPopupExtender ID="mpConfirmationsss" runat="server" PopupControlID="pnlConfirmationss"
                CancelControlID="lbtnNoConfirmationss" TargetControlID="Button4ayush" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlConfirmationss" runat="server" Style="position: fixed;">
                <div class="card" style="width: 450px;">
                    <div class="card-header">
                        <h2 class="card-title text-left mb-0">Please Confirm
                        </h2>
                    </div>
                    <div class="card-body text-left pt-2" style="min-height: 100px;">
                        <asp:Label ID="lblConfirmation" runat="server" Text="Do you want to save ?"></asp:Label>
                        <asp:HiddenField ID="hdgrdindex" runat="server" />
                        <div style="width: 100%; margin-top: 20px; text-align: right;">
                            <asp:LinkButton ID="lbtnYesConfirmation" runat="server" OnClick="lbtnYesConfirmation_Click" CssClass="btn btn-success btn-sm"> <i class="fa fa-check"></i> Yes </asp:LinkButton>
                            <asp:LinkButton ID="lbtnNoConfirmationss" runat="server" CssClass="btn btn-danger btn-sm ml-2"> <i class="fa fa-times"></i> No </asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div style="visibility: hidden;">
                    <asp:Button ID="Button4ayush" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>
    </div>

   
</asp:Content>

