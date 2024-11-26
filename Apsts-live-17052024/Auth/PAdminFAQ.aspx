<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/PortalAdmmaster.master" AutoEventWireup="true" CodeFile="PAdminFAQ.aspx.cs" Inherits="Auth_PAdminFAQ" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid pt-2">
        <asp:HiddenField ID="hidtoken" runat="server" />
        <div class="row">
            <div class="col-xl-4 order-xl-1">
                <div class="card" style="min-height: 450px">
                    <div class="card-header border-bottom">
                        <div class="float-left">
                            <h3 class="mb-0">FAQ List</h3>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row mb-2">
                            <div class="col-lg-5 offset-7">
                                <asp:Label runat="server" CssClass="form-control-label">FAQ category</asp:Label>
                                <asp:DropDownList ID="ddlFAQCategory1" OnSelectedIndexChanged="ddlFAQCategory1_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control form-control-sm"></asp:DropDownList>


                            </div>
                        </div>
                        <asp:GridView ID="gvFAQ" runat="server" GridLines="None" ShowHeader="false" AutoGenerateColumns="false" Width="100%"
                            OnRowCommand="gvFAQ_RowCommand" OnPageIndexChanging="gvFAQ_PageIndexChanging"
                            AllowPaging="true" PageSize="4" DataKeyNames="faqid,faqcategory,faq_categoryen,faq_question_en,faq_answer_en,faqquestion_local,faq_answer_local">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <div class="card card-stats">
                                            <!-- Card body -->
                                            <div class="card-body">
                                                <div class="row">
                                                    <div class="col-lg-8">

                                                        <h5 class="text-uppercase mb-0 font-weight-bold text-xs">
                                                            <%# Eval("faq_question_en") %><br />
                                                            <span class="text-uppercase mb-0 font-weight-normal text-xs">
                                                                <%# Eval("faq_answer_en") %></span>
                                                            <br />
                                                            <span class="text-gray text-xs">FAQ CATEGORY-
                                                            <%# Eval("faq_categoryen") %>
                                                            </span>
                                                        </h5>
                                                    </div>

                                                    <div class="col-lg-4">
                                                        <asp:LinkButton ID="lbtnUpdateFAQ" Visible="true" runat="server" CommandName="updateFAQ" CssClass="btn btn-sm btn-primary" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' ToolTip="Update FAQ Details"> <i class="fa fa-edit"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="lbtnDelete" Visible="true" runat="server" CommandName="DeleteFAQ" CssClass="btn btn-sm btn-danger" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' ToolTip="Delete FAQ Details"> <i class="fa fa-trash"></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                        </asp:GridView>
                        <asp:Panel ID="pnlnoRecordfound" runat="server">
                            <div class="card card-stats">
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-lg-12 text-center mt-4" runat="server" id="div1">
                                            <span class="h2 font-weight-bold mb-0">Start FAQ Creation </span>
                                            <h5 class="card-title text-uppercase text-muted mb-0">No FAQ has been created yet</h5>
                                        </div>
                                        <div class="col-lg-12 text-center mt-4" runat="server" id="div2" visible="false">
                                            <span class="h2 font-weight-bold mb-0">No Record Found </span>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </asp:Panel>

                    </div>

                </div>
            </div>
            <div class="col-xl-8 order-xl-2">
                <asp:Panel ID="pnlAddFAQ" runat="server" Visible="true">
                    <div class="card" style="min-height: 450px">
                        <div class="card-header">
                            <div class="row align-items-center m-0">
                                <div class="col-8">
                                    <h3 class="mb-0">
                                        <asp:Label runat="server" ID="lblFAQHead" Text="Add FAQ Details"></asp:Label>
                                        <asp:Label runat="server" ID="lblFAQUpdate" Text="Update FAQ Details" Visible="false"></asp:Label>
                                    </h3>
                                </div>
                                <div class="col-md-4 text-right">
                                    <p style="font-size: small; margin: 0px;" class="text-danger font-weight-bold">All marked * Fields are mandatory</p>
                                </div>
                            </div>
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-lg-4">
                                    <asp:Label runat="server" CssClass="form-control-label">FAQ category<span style="color: red">*</span></asp:Label>
                                    <asp:DropDownList ID="ddlFAQcategory" runat="server" CssClass="form-control form-control-sm">
                                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                            </div>

                            <h6 class="heading-small my-0">1. FAQ Question</h6>
                            <div class="row">
                                <div class="col-lg-6">
                                    <asp:Label runat="server" CssClass="form-control-label">English<span style="color: red">*</span></asp:Label>
                                    <asp:TextBox ID="tbFAQQues" TextMode="MultiLine" resize="none" runat="server" CssClass="form-control form-control-sm" placeholder="max 500 chars" MaxLength="500" AutoComplete="off" data-toggle="tooltip" data-placement="bottom" title="FAQ Question"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender runat="server" FilterType="Custom,Uppercaseletters,Lowercaseletters,Numbers" ValidChars="?,., ,-" TargetControlID="tbFAQQues" />

                                </div>
                                <div class="col-lg-6">
                                    <asp:Label runat="server" CssClass="form-control-label">Local</asp:Label>
                                    <asp:TextBox ID="tbFAQQuesLocal" TextMode="MultiLine" resize="none" runat="server" CssClass="form-control form-control-sm" placeholder="max 500 chars" MaxLength="500" AutoComplete="off" data-toggle="tooltip" data-placement="bottom" title="FAQ Question(Local)"></asp:TextBox>
                                </div>
                            </div>

                            <h6 class="heading-small my-0">2. FAQ Answer</h6>
                            <div class="row">
                                <div class="col-lg-6">
                                    <asp:Label runat="server" CssClass="form-control-label">English<span style="color: red">*</span></asp:Label>
                                    <asp:TextBox ID="tbFAQAns" TextMode="MultiLine" resize="none" runat="server" CssClass="form-control form-control-sm" placeholder="max 500 chars" MaxLength="500" AutoComplete="off" data-toggle="tooltip" data-placement="bottom" title="FAQ Question"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender runat="server" FilterType="Custom,Uppercaseletters,Lowercaseletters,Numbers" ValidChars=".,-, " TargetControlID="tbFAQAns" />

                                </div>
                                <div class="col-lg-6">
                                    <asp:Label runat="server" CssClass="form-control-label">Local</asp:Label>
                                    <asp:TextBox ID="tbFAQAnsLocal" TextMode="MultiLine" resize="none" runat="server" CssClass="form-control form-control-sm" placeholder="max 500 chars" MaxLength="500" AutoComplete="off" data-toggle="tooltip" data-placement="bottom" title="FAQ Question(Local)"></asp:TextBox>
                                </div>
                            </div>


                            <div class="pl-lg-4 mt-3 mb-2">
                                <div class="row">
                                    <div class="col-lg-12 text-right">
                                        <asp:LinkButton ID="lbtnUpdate" runat="server" class="btn btn-success" OnClick="lbtnUpdate_Click" Visible="false" data-toggle="tooltip" data-placement="bottom" title="Update FAQ Details">
                                    <i class="fa fa-save"></i>&nbsp; Update</asp:LinkButton>
                                        <asp:LinkButton ID="lbtnSave" Visible="true" runat="server" class="btn btn-success" OnClick="lbtnSave_Click" data-toggle="tooltip" data-placement="bottom" title="Save FAQ Details">
                                    <i class="fa fa-save"></i>&nbsp; Save</asp:LinkButton>
                                        <asp:LinkButton ID="lbtnReset" runat="server" CssClass="btn btn-danger" OnClick="lbtnReset_Click" data-toggle="tooltip" data-placement="bottom" title="Reset FAQ Details">
                                    <i class="fa fa-undo"></i>&nbsp; Reset</asp:LinkButton>
                                        <asp:LinkButton ID="lbtnCancel" Visible="false" runat="server" CssClass="btn btn-danger" OnClick="lbtnCancel_Click" data-toggle="tooltip" data-placement="bottom" title="Cancel FAQ Details">
                                    <i class="fa fa-times"></i>&nbsp; Cancel</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
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
                            <h4 class="card-title text-left mb-0">Please Confirm
                            </h4>
                        </div>
                        <div class="card-body text-left pt-2" style="min-height: 100px;">
                            <asp:Label ID="lblConfirmation" runat="server" Text="Do you want to save ?"></asp:Label>
                            <div style="width: 100%; margin-top: 20px; text-align: right;">
                                <asp:LinkButton ID="lbtnYesConfirmation" runat="server" CssClass="btn btn-success btn-sm" OnClick="lbtnYesConfirmation_Click"> <i class="fa fa-check"></i> Yes </asp:LinkButton>
                                <asp:LinkButton ID="lbtnNoConfirmation" runat="server" CssClass="btn btn-danger btn-sm ml-2"> <i class="fa fa-times"></i> No </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div style="visibility: hidden;">
                        <asp:Button ID="Button4" runat="server" Text="" />
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>

