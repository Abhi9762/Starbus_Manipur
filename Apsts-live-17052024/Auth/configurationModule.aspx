<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/ProjectAdmmaster.master" AutoEventWireup="true" CodeFile="configurationModule.aspx.cs" Inherits="ApplicantDetails_configurationModule" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .table th, .table td {
            padding: 0.50rem;
            vertical-align: top;
            border-top: 1px solid #e9ecef;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <div class="container-fluid mt-1">
        <div class="row my-2">
            <div class="col-lg-6 col-md-6 order-xl-1">
                <div class="card" style="min-height: 610px">
                    <div class="card-header">
                        <div class="row">
                            <div class="col-md-6 col-lg-6">
                                <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true"><h2 class="text-warning">Please Note</h2></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-lg-12">
                                <asp:Label runat="server" Font-Size="15px">
                                            1. You can activate & deactivate the modules from here.<br/>
                                   2. If you activate the module then related module pages will be available for operations. <br/>
                                     3. If you deactivate the module then related module pages not available for operations.<br/>
                                   <%-- 4. <br/>
                                    5. <br/>--%>
                                           
                                </asp:Label>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-6 col-lg-6 order-xl-2">
                <div class="card" style="min-height: 610px">
                    <div class="card-header">
                        <div class="row">
                            <div class="col-md-10 col-lg-10">
                                <asp:Label runat="server" CssClass="form-control-label"><h2>Module Configuration</h2></asp:Label>

                            </div>
                            <div class="col-lg-2 col-md-2 float-left m-0 p-0">
                                <asp:LinkButton ID="lbtnViewHistory" runat="server" ToolTip="View History" OnClick="btnLogHistry_Click" Visible="true" CssClass="btn btn bg-gradient-primary btn-sm text-white">
                                    <i class="fa fa-history"></i>
                                </asp:LinkButton>
                            </div>

                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-12 col-lg-12">
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CssClass="table table-striped"
                                    OnPageIndexChanging="gvmoduleCategory_PageIndexChanging" Visible="true" DataKeyNames="id" AllowPaging="true"
                                    PageSize="50" GridLines="None" OnRowCommand="GridView1_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="category_name" HeaderText="Module Name" />
                                        <asp:TemplateField HeaderText="ID" Visible="false">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hiddenId" runat="server" Value='<%# Eval("id") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:Button ID="btnStatus" runat="server" Text='<%# Eval("active_yn").ToString() == "Y" ? "Stop" : "Start" %>'
                                                    CommandName="ChangeStatus" CommandArgument='<%# Eval("id") + ";" + Eval("category_name") %>'
                                                    CssClass='<%# Eval("active_yn").ToString() == "Y" ? "btn btn-success btn-sm" : "btn btn-danger btn-sm" %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:BoundField DataField="runningstatus"  />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%--modal popup--%>
    <div class="row">
        <cc1:ModalPopupExtender ID="mpConfirmation" runat="server" PopupControlID="pnlConfirmation"
            CancelControlID="lbtnNoConfirmation" TargetControlID="Button4" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlConfirmation" runat="server" Style="position: fixed;">
            <div class="card">
                <div class="card-header">
                    <h4 class="card-title text-left mb-0">Please Confirm
                    </h4>
                </div>
                <div class="card-body text-left pt-2" style="min-height: 100px;">
                    <asp:Label ID="lblConfirmation" runat="server" Text="Do you want to save ?"></asp:Label>
                    <div style="width: 100%; margin-top: 20px; text-align: right;">
                        <asp:LinkButton ID="lbtnYesConfirmation" runat="server" OnClick="ChangeStatus_Click" CssClass="btn btn-success btn-sm"> <i class="fa fa-check"></i> Yes </asp:LinkButton>
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
        <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel1"
            CancelControlID="lbtnNo" TargetControlID="Button1" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="Panel1" runat="server" Style="position: fixed;">
            <div class="card" >
                <div class="card-header">
                    <h4 class="card-title text-left mb-0">Configuration Module Action History
                    </h4>
                    <asp:LinkButton runat="server" ID="lbtnNo" Style="float: inline-end; margin-top: -27px;"><i class="fa fa-close"></i>Close</asp:LinkButton>

                </div>
                <div class="card-body text-left pt-2" style="min-height: 100px;">
                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" CssClass="table table-striped"
                        OnPageIndexChanging="gvmoduleHistory_PageIndexChanging" Visible="true" DataKeyNames="" AllowPaging="true"
                        PageSize="10" GridLines="None">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="category_name" HeaderText="Category Name" />
                            
                            <asp:BoundField DataField="sp_updation_datetime" HeaderText="Updated On" />
                            <asp:BoundField DataField="status" HeaderText="Action" />
                        </Columns>
                        <HeaderStyle BackColor="#ececec" Font-Size="9pt" />
                        <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                    </asp:GridView>
                    <asp:Panel ID="pnlModuleConfigNoRecord" runat="server" Width="100%" Visible="true">
                        <div class="col-md-12 p-0" style="text-align: center; text-transform: uppercase;">
                            <div class="col-md-12 busListBox" style="color: #e3e3e3; padding-top: 50px; padding-bottom: 50px; font-size: 20px; font-weight: bold;">
                                Sorry No Record Found
                            </div>
                        </div>
                    </asp:Panel>

                </div>
            </div>
            <div style="visibility: hidden;">
                <asp:Button ID="Button1" runat="server" Text="" />
            </div>
        </asp:Panel>
    </div>

</asp:Content>
