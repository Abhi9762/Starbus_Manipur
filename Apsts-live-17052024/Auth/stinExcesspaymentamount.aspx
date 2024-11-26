<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/StnInchgemaster.master" AutoEventWireup="true" CodeFile="stinExcesspaymentamount.aspx.cs" Inherits="Auth_stinExcesspaymentamount" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <style>
        .ModalPopupBG {
            background-color: #666699;
            filter: alpha(opacity=50);
            opacity: 0.7;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid pt-2 pb-5">
        <div class="row align-items-center">
            <div class="col-md-12 ">
                <div class="card card-stats mb-3">
                    <div class="row m-0">
                        <div class="col-6 border-right">
                            <div class="card-body">
                                <div class="row m-0">
                                    <div class="col-12">
                                        <h4 class="mb-1">
                                            <asp:Label ID="lblSummary" runat="server" CssClass="text-capitalize">Summary as on Date 01/12/2021 04:00pm</asp:Label></h4>
                                        <div class="row m-0">
                                            <div class="col-6 border-right">
                                                <div class="row m-0">
                                                    <div class="col-12">
                                                        <h5 class="card-title text-uppercase text-muted mb-0">Total Request:&nbsp;
                                <asp:Label ID="lblTotal" runat="server" ForeColor="gray" Font-Bold="true" Text="0" Font-Size="14pt" CssClass="h3 font-weight-bold mb-0 text-right float-right"></asp:Label></h5>
                                                    </div>
                                                    <div class="col-12">
                                                        <h5 class="card-title text-uppercase text-muted mb-0">Approved: &nbsp;
                                <asp:Label ID="lblApproved" runat="server" ForeColor="gray" Font-Bold="true" Text="0" Font-Size="14pt" CssClass="h3 font-weight-bold mb-0 text-right float-right"></asp:Label></h5>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-6">
                                                <div class="row m-0">
                                                    <div class="col-12">
                                                        <h5 class="card-title text-uppercase text-muted mb-0">Pending:&nbsp;
                                 <asp:Label ID="lblPending" runat="server" ForeColor="gray" Font-Bold="true" Text="0" Font-Size="14pt" CssClass="h3 font-weight-bold mb-0 text-right float-right"></asp:Label></h5>
                                                    </div>
                                                    <div class="col-12">
                                                        <h5 class="card-title text-uppercase text-muted mb-0">Reject: &nbsp;
															<asp:Label ID="lblreject" runat="server" ForeColor="gray" Font-Bold="true" Text="0" CssClass="h3 font-weight-bold mb-0 text-right float-right"></asp:Label></h5>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>

                                </div>

                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col">
                                        <div>
                                            <h4 class="mb-1">Instructions</h4>
                                        </div>
                                        <ul class="data-list" data-autoscroll>
                                            <li class="form-control-label">1. Verify Excess Payement Waybill here.
                                            </li>
                                            <li class="form-control-label">2. Reject Excess Payement Waybill here.
                                            </li>
                                        </ul>
                                    </div>
                                    <%--<div class="col-auto">
                                        <asp:LinkButton ID="lbtnViewInstruction" runat="server" OnClick="lbtnViewInstruction_Click" ToolTip="View Instructions" CssClass="btn btn bg-gradient-orange btn-sm text-white">
                                    <i class="fa fa-eye"></i>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lbtnDownloadUserManual" runat="server" ToolTip="Download user manual" CssClass="btn btn bg-gradient-green btn-sm text-white">
                                   <i class="fa fa-download"></i>
                                        </asp:LinkButton>

                                    </div>--%>
                                </div>

                            </div>
                        </div>

                    </div>

                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-xl-6 order-xl-1">
                <div class="card" style="min-height: 800px">
                    <div class="card-header border-bottom">

                        <h3 class="mb-3">Submitted Request</h3>
                        <div class="row">
                            <div class="col-md-7">
                                <div class="text-right">Enter Waybill No </div>
                            </div>
                            <div class="col-md-5">
                                <div class="form-group mb-0">
                                    <div class="input-group">
                                        <asp:TextBox ID="tbRecordwaybillno" runat="server" AutoComplete="Off" class="form-control form-control-sm text-uppercase" placeholder="Search text" MaxLength="20"></asp:TextBox>
                                        <div class="input-group-append">
                                            <asp:LinkButton ID="lbtnRecordsSearch" runat="server" CssClass="btn btn-success btn-icon-only btn-sm mr-1 " OnClick="lbtnRecordsSearch_Click" strle="z-i">
                                            <i class="fa fa-search mt-2"></i>
                                            </asp:LinkButton>
                                        </div>
                                        <div class="input-group-append">
                                            <asp:LinkButton ID="lbtnRecordsRest" runat="server" CssClass="btn btn-danger btn-icon-only btn-sm mr-1" OnClick="lbtnRecordsRest_Click">
                                            <i class="fa fa-undo mt-2"></i>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:GridView ID="gvRecords" OnRowCommand="gvRecords_RowCommand" runat="server" GridLines="None"
                        ShowHeader="false" AutoGenerateColumns="false" Width="100%" OnPageIndexChanging="gvRecords_PageIndexChanging"
                        AllowPaging="true" PageSize="10" DataKeyNames="uploadfile,refrenceid,waybillno">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <div class="card card-stats">
                                        <!-- Card body -->
                                        <div class="card-body">
                                            <div class="row">
                                                <div class="col-sm-10">
                                                    <div class="row">
                                                        <div class="col-sm-5">
                                                            <h5 class="text-uppercase mb-0 font-weight-bold text-xs">
                                                                <span class="text-gray text-xs">Waybill No - 
                                                                </span><%# Eval("waybillno") %><br />
                                                                <span class="text-gray text-xs">Conductor Code -</span>
                                                                <%# Eval("cond_code") %>
                                                            </h5>
                                                        </div>
                                                        <div class="col-sm-3">
                                                            <h5 class="text-uppercase mb-0 font-weight-bold text-xs">
                                                                <span class="text-gray text-xs">Status - 
                                                                </span><%# Eval("statusname") %>
                                                                <%# Eval("statusname").ToString() == "Approved" ? "<i class='fa fa-check-circle mr-2 text-success'></i>" : "<i class='fa fa-times-circle mr-2 text-danger'></i>" %>
                                                                <br />
                                                                <span class="text-gray text-xs">No. Of Txns. -</span>
                                                                <%# Eval("no_of_txns") %>
                                                            </h5>
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <h5 class="text-uppercase mb-0 font-weight-bold text-xs">
                                                                <span class="text-gray text-xs">Actual Amt. - 
                                                                </span><%# Eval("actual_amount") %><i class="fa fa-rupee-sign"></i><br />
                                                                <span class="text-gray text-xs">Excess Amt. -</span>
                                                                <%# Eval("excessamount") %><i class="fa fa-rupee-sign"></i>
                                                            </h5>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            <h5 class="text-uppercase mb-0 font-weight-bold text-xs">
                                                                <span class="text-gray text-xs">Remark -</span>
                                                                <%# Eval("remark_") %>  </h5>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2">
                                                    <asp:LinkButton ID="lbtnViewTxn" Visible="true" runat="server" CommandName="viewTXN" CssClass="btn btn-sm btn-primary mt-1" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' ToolTip="View Transaction Details"> <i class="fa fa-sort-numeric-down"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnViewDoc" Visible="true" runat="server" CommandName="viewDOC" CssClass="btn btn-sm btn-warning mt-1" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' ToolTip="View Uploaded Doucment"> <i class="fa fa-address-card"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnViewWaybill" CommandName="viewWaybill" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                        Visible="true" runat="server" CssClass="btn btn-sm btn-success"
                                                        ToolTip="View Collection & Expendiure Details">W</asp:LinkButton>
                                                </div>


                                            </div>

                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                    </asp:GridView>
                    <asp:Panel ID="pnlnoRecordfound" runat="server" Visible="true">
                        <div class="card card-stats">
                            <!-- Card body -->
                            <div class="card-body">
                                <div class="row">

                                    <div class="col-lg-12 text-center">
                                        <i class="fa fa-bus fa-5x"></i>
                                    </div>
                                    <div class="col-lg-12 text-center mt-4" runat="server" id="divnosearchrecord" visible="true">
                                        <span class="h2 font-weight-bold mb-0">No Record Found </span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
            <div class="col-xl-6 order-xl-1">
                <div class="card" style="min-height: 800px">
                    <div class="card-header border-bottom">

                        <h3 class="mb-3">Pending Request</h3>
                        <div class="row">
                            <div class="col-md-7">
                                <div class="text-right">Enter Waybill No </div>
                            </div>
                            <div class="col-md-5">
                                <div class="form-group mb-0">
                                    <div class="input-group">
                                        <asp:TextBox ID="txtPendingwaybillsearch" runat="server" AutoComplete="Off" class="form-control form-control-sm text-uppercase" placeholder="Search text" MaxLength="20"></asp:TextBox>
                                        <div class="input-group-append">
                                            <asp:LinkButton ID="lbtnPendingSearch" runat="server" CssClass="btn btn-success btn-icon-only btn-sm mr-1 " OnClick="lbtnPendingSearch_Click" strle="z-i">
                                            <i class="fa fa-search mt-2"></i>
                                            </asp:LinkButton>
                                        </div>
                                        <div class="input-group-append">
                                            <asp:LinkButton ID="lbtnPendingReset" runat="server" CssClass="btn btn-danger btn-icon-only btn-sm mr-1" OnClick="lbtnPendingReset_Click">
                                            <i class="fa fa-undo mt-2"></i>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:GridView ID="gvPendingRecords" OnRowCommand="gvPendingRecords_RowCommand" runat="server" GridLines="None"
                        ShowHeader="false" AutoGenerateColumns="false" Width="100%" OnPageIndexChanging="gvPendingRecords_PageIndexChanging"
                        AllowPaging="true" PageSize="10" DataKeyNames="uploadfile,refrenceid,waybillno">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <div class="card card-stats">
                                        <!-- Card body -->
                                        <div class="card-body">
                                            <div class="row">
                                                <div class="col-sm-10">
                                                    <div class="row">
                                                        <div class="col-sm-5">
                                                            <h5 class="text-uppercase mb-0 font-weight-bold text-xs">
                                                                <span class="text-gray text-xs">Waybill No - 
                                                                </span><%# Eval("waybillno") %><br />
                                                                <span class="text-gray text-xs">Conductor Code -</span>
                                                                <%# Eval("cond_code") %>
                                                            </h5>
                                                        </div>
                                                        <div class="col-sm-3">
                                                            <h5 class="text-uppercase mb-0 font-weight-bold text-xs">
                                                                <span class="text-gray text-xs">Status - 
                                                                </span>
                                                                <span class="text-primary "><%# Eval("statusname") %>
                                                                </span>

                                                                <br />
                                                                <span class="text-gray text-xs">No. Of Txns. -</span>
                                                                <%# Eval("no_of_txns") %>
                                                            </h5>
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <h5 class="text-uppercase mb-0 font-weight-bold text-xs">
                                                                <span class="text-gray text-xs">Actual Amt. - 
                                                                </span><%# Eval("actual_amount") %><i class="fa fa-rupee-sign"></i><br />
                                                                <span class="text-gray text-xs">Excess Amt. -</span>
                                                                <%# Eval("excessamount") %><i class="fa fa-rupee-sign"></i>
                                                            </h5>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            <h5 class="text-uppercase mb-0 font-weight-bold text-xs">
                                                                <span class="text-gray text-xs">Remark -</span>
                                                                <%# Eval("remark_") %>  </h5>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2">
                                                    <asp:LinkButton ID="lbtnViewTxn" Visible="true" runat="server" CommandName="viewTXN" CssClass="btn btn-sm btn-primary mt-1" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' ToolTip="View Transaction Details"> <i class="fa fa-sort-numeric-down"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnViewDoc" Visible="true" runat="server" CommandName="viewDOC" CssClass="btn btn-sm btn-warning mt-1" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' ToolTip="View Uploaded Doucment"> <i class="fa fa-address-card"></i></asp:LinkButton>
                                                     <asp:LinkButton ID="lbtnViewWaybilll" CommandName="viewWaybill" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                        Visible="true" runat="server" CssClass="btn btn-sm btn-success mt-1"
                                                        ToolTip="View Collection & Expendiure Details">W</asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnApprove" Visible="true" runat="server" CommandName="Approve" CssClass="btn btn-sm btn-success mt-1" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' ToolTip="Approve Request"> <i class="fa fa-check"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnReject" Visible="true" runat="server" CommandName="Reject" CssClass="btn btn-sm btn-danger mt-1" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' ToolTip="Reject Request"> <i class="fa fa-times"></i></asp:LinkButton>

                                                </div>


                                            </div>

                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                    </asp:GridView>
                    <asp:Panel ID="PnlNorecordPending" runat="server" Visible="true">
                        <div class="card card-stats">
                            <!-- Card body -->
                            <div class="card-body">
                                <div class="row">

                                    <div class="col-lg-12 text-center">
                                        <i class="fa fa-bus fa-5x"></i>
                                    </div>
                                    <div class="col-lg-12 text-center mt-4" runat="server" id="div1" visible="true">
                                        <span class="h2 font-weight-bold mb-0">No Record Found </span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <cc1:ModalPopupExtender ID="mpviewdocment" runat="server" PopupControlID="pnlviewdocment" CancelControlID="btnclose"
            TargetControlID="Button2" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlviewdocment" runat="server">
            <div class="card" style="margin-top: 100px;">
                <div class="card-header">
                    <div class="row">
                        <div class="col-lg-6">
                            <h4 class="card-title text-left mb-0">View Document
                            </h4>
                        </div>
                        <div class="col-lg-5  float-end">
                        </div>
                        <div class="col-lg-1  float-end">
                            <asp:LinkButton ID="btnclose" runat="server" CssClass="btn btn-danger"> <i class="fa fa-times"></i></asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div class="card-body text-left pt-2" style="min-height: 100px;">
                   <%-- <embed src="../Pass/ViewDocument.aspx" style="height: 75vh; width: 70vw" />--%>
                     <asp:Image ID="img" runat="server" Visible="false" Style="border-width: 0px; height: 400px; width: 600px; border: 2px solid #eaf4ff;" />
                                                      
                    <div style="visibility: hidden;">
                        <asp:Button ID="Button2" runat="server" Text="" />
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>
    <div class="row">
                    <cc1:ModalPopupExtender ID="mpShowWaybill" runat="server" PopupControlID="Panel3" TargetControlID="Button3"
                        CancelControlID="LinkButton3" BackgroundCssClass="ModalPopupBG">
                    </cc1:ModalPopupExtender>
                    <asp:Panel ID="Panel3" runat="server" Style="position: fixed;">
                        <div class="modal-content mt-5" style="width: 65vw;">
                            <div class="card w-100">
                                <div class="card-header py-3">
                                    <div class="row">
                                        <div class="col">
                                            <h3 class="m-0">Waybill</h3>
                                        </div>
                                        <div class="col-auto">
                                            <asp:LinkButton ID="LinkButton3" runat="server" Style="color: Red; cursor: pointer;"><i class="fa fa-times fa-2x"></i></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <div class="card-body p-2">

                                    <asp:Literal ID="eWaybill" runat="server"></asp:Literal>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div style="visibility: hidden;">
                            <asp:Button ID="Button3" runat="server" Text="" />
                            <asp:Button ID="Button1" runat="server" Text="" />
                        </div>
                    </asp:Panel>
                </div>
    <div class="row">
        <cc1:ModalPopupExtender ID="MpTxns" runat="server" PopupControlID="PnlTxns" CancelControlID="lbtntxnclose"
            TargetControlID="btnTraget" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="PnlTxns" runat="server">
            <div class="card" style="margin-top: 100px;">
                <div class="card-header">
                    <div class="row">
                        <div class="col-lg-6">
                            <h4 class="card-title text-left mb-0">View Transactions
                            </h4>
                        </div>
                        <div class="col-lg-5  float-end">
                        </div>
                        <div class="col-lg-1  float-end">
                            <asp:LinkButton ID="lbtntxnclose" runat="server" CssClass="btn btn-danger btn-sm"> <i class="fa fa-times"></i></asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div class="card-body text-left pt-2" style="min-height: 100px;">
                    <div class="row">
                        <div class="col-12">
                            <asp:GridView ID="gvTransaction" runat="server" EnableModelValidation="True"
                                AllowPaging="false" Font-Size="12pt" GridLines="None" class="table table-striped table-responsive-sm"
                                AutoGenerateColumns="False" DataKeyNames=""
                                PageSize="10">
                                <Columns>
                                    <asp:TemplateField HeaderText="Ticket No." HeaderStyle-Font-Size="12pt" ItemStyle-Font-Size="12pt">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lbltxn" Text='<%#Bind("txnno") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Actual Amount" HeaderStyle-Font-Size="12pt" ItemStyle-Font-Size="12pt">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblact" Text='<%#Bind("actamt") %>' /><i class="fa fa-rupee-sign ml-1"></i>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Excess Amount" HeaderStyle-Font-Size="12pt" ItemStyle-Font-Size="12pt">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblexcs" Text='<%#Bind("excsamt") %>' /><i class="fa fa-rupee-sign ml-1"></i>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                </Columns>
                                <AlternatingRowStyle BackColor="#eaf4ff" />
                                <HeaderStyle BackColor="aliceblue" ForeColor="black" VerticalAlign="Top" CssClass="table_head" />
                                <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div style="visibility: hidden;">
                    <asp:Button ID="btnTraget" runat="server" Text="" />
                </div>

            </div>
        </asp:Panel>
    </div>
    <div class="row">
        <cc1:ModalPopupExtender ID="mpConfirmation" runat="server" PopupControlID="pnlConfirmation"
            CancelControlID="lbtnNoConfirmation" TargetControlID="Button4" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlConfirmation" runat="server" Style="position: fixed;">
            <div class="card" style="width: 450px;">
                <div class="card-header">
                    <h2 class="card-title text-left mb-0">Please Confirm
                    </h2>
                </div>
                <div class="card-body text-left pt-2" style="min-height: 100px;">
                    <asp:Label ID="lblConfirmation" runat="server" Text="Do you want to save ?"></asp:Label>
                    <div style="width: 100%; margin-top: 20px; text-align: right;">
                        <asp:LinkButton ID="lbtnYesConfirmation" runat="server" OnClick="lbtnYesConfirmation_Click" CssClass="btn btn-success btn-sm"> <i class="fa fa-check"></i> Yes </asp:LinkButton>
                        <asp:LinkButton ID="lbtnNoConfirmation" runat="server" CssClass="btn btn-danger btn-sm ml-2"> <i class="fa fa-times"></i> No </asp:LinkButton>
                    </div>
                </div>
            </div>
            <div style="visibility: hidden;">
                <asp:Button ID="Button4" runat="server" Text="" />
            </div>
        </asp:Panel>
    </div>
</asp:Content>

