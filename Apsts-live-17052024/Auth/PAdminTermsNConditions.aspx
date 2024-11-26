<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/PortalAdmmaster.master" AutoEventWireup="true" CodeFile="PAdminTermsNConditions.aspx.cs" Inherits="Auth_PAdminTermsNConditions" EnableEventValidation="false" ValidateRequest="false"  %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="tiny_mce_4.5.5/tinymce.min.js"></script>
    <script src="tiny_mce_4.5.5/tinyInitNoting_4.5.5.js"></script>
    <style type="text/css">
        textarea {
            resize: none;
        }
    </style>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid pt-2">
        <asp:HiddenField ID="hidtoken" runat="server" />
        <%--<div class="row align-items-center">
            <div class="col-xl-12">
                <div class="card card-stats mb-3">
                    <div class="row">
                        <div class="col-4 border-right">
                            <div class="card-body">
                                <div class="row m-0">
                                    <div class="col-4">
                                          
                                  
                                        </div>
                                        <div class="col-8">
                                          
                                            </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>--%>

        <div class="row">

            <div class="col-xl-4 order-xl-1" style="padding: 1px;">
                <div class="card p-1" style="min-height: 600px">
                    <div class="card-header border-bottom">
                        <h4 class="mb-1 mt-2">Please Note
                        </h4>

                    </div>
                    <div class="card-body p-0 text-left p-2">
                        <ul style="text-align: left;">
                            <li>Here contents to be printed as Terms and Condition on Ticket can be created
                            </li>
                            <li>Formating done here is WYSWYG. The similar contents will be visible on ticket.
                            </li>
                            <li>If new Terms and conditions have been created. Old tickets will have the old terms and conditions printed on them
                            </li>

                        </ul>
                    </div>
                </div>
            </div>
            <div class="col-xl-8 order-xl-1">
                <div class="row mb-3">

				<div class="col-lg-12 col-md-12 text-center form-check">
					<asp:RadioButton runat="server" ID="rbtntermsncndi" CssClass="form-check-input pr-2" OnCheckedChanged="rbtntermsncndi_CheckedChanged" GroupName="radio" Checked="true" AutoPostBack="true" />
					<span class="checkmarkRadio"></span>
					<asp:Label runat="server" For="rbtntermsncndi" Font-Bold="true" ToolTip="Update Terms & Condition" CssClass="ml-2" Font-Size="small">Terms & Condition &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
					</asp:Label>

					<asp:RadioButton runat="server" ID="rbtnpripolcy" CssClass="form-check-input" OnCheckedChanged="rbtnpripolcy_CheckedChanged" GroupName="radio" AutoPostBack="true" />
					<span class="checkmarkRadio"></span>
					<asp:Label runat="server" For="rbtnpripolcy" Font-Bold="true" ToolTip="Update Privacy Policy" CssClass="ml-2" Font-Size="small">Privacy Policy&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
					</asp:Label>

					<asp:RadioButton runat="server" ID="rbtndisc" CssClass="form-check-input" ToolTip="Update Disclaimer" OnCheckedChanged="rbtndisc_CheckedChanged" GroupName="radio" AutoPostBack="true" />
					<span class="checkmarkRadio"></span>
					<asp:Label runat="server" For="rbtndisc" CssClass="ml-2" Font-Bold="true" ToolTip="Update Disclaimer" Font-Size="small">Disclaimer&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
					</asp:Label>
                    
					<asp:RadioButton runat="server" ID="rbtnLabour" CssClass="form-check-input" ToolTip="Update Labour Welfare" OnCheckedChanged="rbtnLabour_CheckedChanged"  GroupName="radio" AutoPostBack="true" />
					<span class="checkmarkRadio"></span>
					<asp:Label runat="server" For="rbtndisc" CssClass="ml-2" Font-Bold="true" ToolTip="Update Labour Welfare" Font-Size="small">Labour Welfare&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
					</asp:Label>
                    
					<asp:RadioButton runat="server" ID="rbtnhistory" CssClass="form-check-input" ToolTip="Update History" OnCheckedChanged="rbtnhistory_CheckedChanged"  GroupName="radio" AutoPostBack="true" />
					<span class="checkmarkRadio"></span>
					<asp:Label runat="server" For="rbtndisc" CssClass="ml-2" Font-Bold="true" ToolTip="Update History" Font-Size="small">History&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
					</asp:Label>
				</div>
			</div>
                <div class="card p-1" style="min-height: 600px">
                    <div class="card-header border-bottom">
                        <div class="row">
                            <div class="col-xl-4">
                                <h4 class="mb-1 mt-2"><asp:Label ID="lblupdatemsg" runat="server" Text="Update Terms and Conditions for Ticket"></asp:Label>
                                    <asp:Label ID="lblUpdationDate" runat="server" CssClass="small text-danger"></asp:Label>
                                </h4>
                            </div>
                            <div class="col-xl-8" style="text-align: right;">
                                <asp:LinkButton ID="lbtnResetT" runat="server" CssClass="btn btn-danger" ToolTip="Reset City" OnClick="lbtnResetT_Click">
                                    <i class="fa fa-undo"></i>&nbsp; Reset</asp:LinkButton>
                                <asp:LinkButton ID="lbtnSaveT" Visible="true" runat="server" class="btn btn-success" ToolTip="Save " OnClick="lbtnSave_Click">
                                    <i class="fa fa-save"></i>&nbsp; Save</asp:LinkButton>
                                <asp:LinkButton ID="lbtnViewHistoryT" Visible="true" runat="server" class="btn btn-success" ToolTip="Save " OnClick="lbtnView_History">
                                    <i class="fa fa-history"></i></asp:LinkButton>
                            </div>
                        </div>
                    </div>

                    <div class="card-body p-2">

                        <asp:TextBox ID="tbTermsNCondition" runat="server" TextMode="MultiLine" MaxLength="20000" Style="resize: none; width: 100%; height: 90%;" Height="500px"> </asp:TextBox>
                    </div>
                    <div class="card-footer border-top" style="text-align: right;">
                        <asp:LinkButton ID="lbtnResetB" runat="server" CssClass="btn btn-danger" ToolTip="Reset City" OnClick="lbtnResetT_Click">
                                    <i class="fa fa-undo"></i>&nbsp; Reset</asp:LinkButton>
                        <asp:LinkButton ID="lbtnSaveB" Visible="true" runat="server" class="btn btn-success" ToolTip="Save " OnClick="lbtnSave_Click">
                                    <i class="fa fa-save"></i>&nbsp; Save</asp:LinkButton>

                        <asp:LinkButton ID="lbtnViewHistoryB" Visible="true" runat="server" class="btn btn-success" ToolTip="Save " OnClick="lbtnView_History">
                                    <i class="fa fa-history"></i></asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <cc1:ModalPopupExtender ID="mpConfirmation" runat="server" PopupControlID="pnlConfirmation"
            CancelControlID="lbtnNoConfirmation" TargetControlID="Button4" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlConfirmation" runat="server" Style="position: fixed;">
            <div class="card" style="width: 350px;">
                <div class="card-header">
                    <h4 class="card-title text-left mb-0">Please Confirm
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
    <div class="row">
        <cc1:ModalPopupExtender ID="mpViewHistory" runat="server" PopupControlID="PanelViewHistory"
            TargetControlID="Button8" CancelControlID="LinkButton4" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="PanelViewHistory" runat="server" Style="position: fixed; top: 30.5px; min-width: 400px; max-width: 800px;">
            <div class="card">
                <div class="card-header">
                    <strong class="card-title">Terms and Condition Updation History</strong>
                </div>
                <div class="card-body" style="padding: 15px !important;">

                    <asp:GridView ID="gvHistory" OnPageIndexChanging="gvHistory_PageIndexChanging" runat="server" AutoGenerateColumns="False"
                        GridLines="None" AllowSorting="true" AllowPaging="true" PageSize="3" CssClass="table table-striped">
                        <Columns>
                            <asp:TemplateField HeaderText="Sr.No." ItemStyle-Width="100">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Updated On">
                                <ItemTemplate>
                                    <%# Eval("action_date") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Updated By">
                                <ItemTemplate>
                                    <%# Eval("action_by") %>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                        <PagerStyle HorizontalAlign="Left" CssClass="gridview" />
                    </asp:GridView>

                </div>
                <div class="card-footer">
                    <asp:LinkButton ID="LinkButton4" runat="server" CssClass="btn btn-success" Style="font-size: 11pt; float: right; border-radius: 4px;">OK</asp:LinkButton>
                </div>
            </div>
            <br />
            <div style="visibility: hidden;">
                <asp:Button ID="Button8" runat="server" Text="" />
            </div>
        </asp:Panel>
    </div>
</asp:Content>

