<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/PortalAdmmaster.master" AutoEventWireup="true" CodeFile="PAdminCancPolicy.aspx.cs" Inherits="Auth_PAdminCancPolicy" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .form-control-label1 {
            font-size: .75rem;
            font-weight: 600;
            color: white;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid mt-1">
        <asp:HiddenField ID="hidtoken" runat="server" />
        <div class="row my-2">
            <div class="col-lg-5 col-md-5 order-xl-1">
                <div class="card" style="min-height: 470px">
                    <div class="col-md-12 col-lg-12">
                        <div class="card-header">
                            <div class="row mr--5">
                                <div class="col-md-10 col-lg-10">
                                    <asp:Label runat="server" CssClass="form-control-label"><h2>Current Policy</h2></asp:Label>
                                </div>
                                <div class="col-lg-2 col-md-2 float-left m-0 p-0">
                                    <asp:LinkButton ID="lbtnHelppolicy" OnClick="lbtnHelppolicy_Click" runat="server" ToolTip="View Help" CssClass="btn btn bg-gradient-orange btn-sm text-white">
                                    <i class="fa fa-info"></i>
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="lbtnViewHistorypolicy" runat="server" OnClick="lbtnViewHistorypolicy_Click" ToolTip="View History" CssClass="btn btn bg-gradient-primary btn-sm text-white">
                                    <i class="fa fa-history"></i>
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row m-0 align-items-center py-4 px-3">
                        <div class="col-md-12 col-lg-12">
                            <asp:GridView ID="gvCurrentPolicy" runat="server" AutoGenerateColumns="False"
                                GridLines="None" class="table" Font-Size="10pt">
                                <Columns>
                                    <asp:TemplateField HeaderText="From (Hrs)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblhrsfrom" runat="server" Text='<%# Eval("hrsfrom") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="To (Hrs)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblhrsto" runat="server" Text='<%# Eval("hrsto") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Deduct">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldeductamt" runat="server" Text='<%#Eval("deductamt")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Refund">
                                        <ItemTemplate>
                                            <asp:Label ID="lblrefundamt" runat="server" Text='<%# Eval("refundamt")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
								<PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                            </asp:GridView>

                            <asp:Panel runat="server" ID="pnlNoRecord" Visible="true" CssClass="text-center" Width="100%">
                                <p class="text-center" style="font-size: 32px; font-weight: bold; color: #e3e3e3; margin-top: 50px;">
                                    Cacnellation Not Generated 
                                </p>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-7 col-lg-7 order-xl-2">
                <div class="card" style="min-height: 470px">
                    <div class="card-header">
                        <div class="row">
                            <div class="col" style="line-height: 0;">
                                <h2>New Policy</h2>
                                <span style="font-size: small; margin: 0px;" class="text-danger font-weight-bold">* Fields are mandatory</span>
                            </div>
                            <div class="col-auto text-right text-danger">
                                You can enter max
                                <asp:Label ID="lblMaxHours" runat="server" Text="Not Available" Style="font-weight: bold;"></asp:Label>
                                Hours [in To (Hours)]
                                                <br />
                                <span style="font-size: 12px; color: gray;">from 'Advance Booking Days' </span>
                            </div>
                        </div>
                    </div>
                    <div class="row m-0 align-items-center py-4 px-3">
                        <div class="col-md-12 col-lg-12">
                            <asp:GridView ID="gvnewcancpolicy" runat="server" AutoGenerateColumns="False" GridLines="None"
                                class="table" ShowFooter="true" OnRowCreated="gvnewcancpolicy_RowCreated">
                                <Columns>
                                    <asp:BoundField DataField="RowNumber" HeaderText="S. No." />
                                    <asp:TemplateField HeaderText="From (Hours) <span class='text-danger font-weight-bold'>*</span>">
                                        <ItemTemplate>
                                            <asp:TextBox ID="tbfromHrs" runat="server" Enabled="false" MaxLength="3" CssClass="form-control" autocomplete="off"
                                                Height="30px" Width="100px"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxFromHrs" runat="server" FilterType="Numbers"
                                                TargetControlID="tbfromHrs" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="To (Hours) <span class='text-danger font-weight-bold'>*</span>">
                                        <ItemTemplate>
                                            <asp:TextBox ID="tbtoHrs" runat="server" MaxLength="3" CssClass="form-control" autocomplete="off"
                                                Height="30px" Width="100px"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxToHrs" runat="server" FilterType="Numbers"
                                                TargetControlID="tbtoHrs" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Deduction In <span class='text-danger font-weight-bold'>*</span>" HeaderStyle-CssClass="text-center">
                                        <ItemTemplate>
                                            <label class="containerRadio">
                                                <asp:RadioButton runat="server" ID="rbtnPercentage" GroupName="radio" Checked="true" AutoPostBack="true" />
                                                <span class="checkmarkRadio"></span>Percentage
                                            </label>
                                            &nbsp;
                                           <label class="containerRadio">
                                               <asp:RadioButton runat="server" ID="rbtnRupee" GroupName="radio" AutoPostBack="true" />
                                               <span class="checkmarkRadio"></span>Rupees
                                           </label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Deduct <span class='text-danger font-weight-bold'>*</span>">
                                        <ItemTemplate>
                                            <asp:TextBox ID="tbrefund" runat="server" MaxLength="3" CssClass="form-control" autocomplete="off"
                                                Height="30px" Width="100px"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxValue" runat="server" FilterType="Numbers"
                                                TargetControlID="tbrefund" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnadd" runat="server" OnClick="lbtnadd_Click" CssClass="btn btn-success btn-sm"><i class="fa fa-plus-circle"></i> </asp:LinkButton>
                                            <asp:LinkButton ID="lbtnremove" runat="server" OnClick="lbtnremove_Click" CssClass="btn btn-danger btn-sm"><i class="fa fa-minus-circle"></i></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle BackColor="#ececec" Font-Size="10pt" />
								<PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                            </asp:GridView>
                        </div>
                        <div class="col-sm-12 text-center">
                            <asp:LinkButton ID="lbCnclPlcySave" runat="server" OnClick="lbCnclPlcySave_Click" class="btn btn-success" Style="border-radius: 4px; font-size: 11pt;"
                                ToolTip="Save"> <i class="fa fa-check" ></i> Save & Lock</asp:LinkButton>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <cc1:ModalPopupExtender ID="mppolicyHistory" runat="server" PopupControlID="pnlpolicyHistory" TargetControlID="Button11" CancelControlID="lbtnClose" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlpolicyHistory" runat="server">
            <div class="card">
                <div class="card-header" style="border-color: #2dce89; background-color: #2dce89; color: white">
                    <strong class="card-title">Cancellation Policy History</strong>
                </div>
                <div class="card-body">



                    <asp:Label ID="lblNocontacthistory" runat="server" Text="Cancellaltion policy not available"
                        Visible="true" Style="color: #d4cece; margin-top: 15px; font-size: 20px; font-weight: bold;"></asp:Label>
                </div>
                <div class="card-footer">
                    <asp:LinkButton ID="lbtnClose" runat="server" CssClass="btn btn-success" Style="font-size: 11pt; float: right; border-radius: 4px;">Close</asp:LinkButton>
                </div>
            </div>
            <br />
            <div style="visibility: hidden;">
                <asp:Button ID="Button11" runat="server" Text="" />
            </div>
        </asp:Panel>
    </div>

    <div class="row">
        <cc1:ModalPopupExtender ID="mpConfirmation" runat="server" PopupControlID="pnlConfirmation"
            CancelControlID="lbtnNoConfirmation" TargetControlID="Button4" BackgroundCssClass="modalBackground">
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
                        <asp:LinkButton ID="lbtnYesConfirmation" OnClick="lbtnYesConfirmation_Click" runat="server" CssClass="btn btn-success btn-sm"> <i class="fa fa-check"></i> Yes </asp:LinkButton>
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

