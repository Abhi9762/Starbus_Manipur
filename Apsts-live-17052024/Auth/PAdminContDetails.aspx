<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/PortalAdmmaster.master" AutoEventWireup="true" CodeFile="PAdminContDetails.aspx.cs" Inherits="Auth_PAdminContDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid mt-1">
        <asp:HiddenField ID="hidtoken" runat="server" />
        <div class="col-md-12 col-lg-12 my-1 text-right">
            <span class="text-warning">All marked *&nbsp;fields are mandatory</span>
        </div>
        <div class="row my-2">
            <div class="col-lg-6 col-md-6 order-xl-1">
                <div class="card" style="min-height: 520px">
                    <div class="col-md-12 col-lg-12">
                        <div class="card-header">
                            <div class="row">
                                <div class="col-md-10 col-lg-10">
                                    <asp:Label runat="server" CssClass="form-control-label"><h2>Helpdesk/Support Contact Details</h2></asp:Label>
                                </div>
                                <div class="col-lg-2 col-md-2 float-left m-0 p-0">
                                    <asp:LinkButton ID="lbtnHelpConatct" OnClick="lbtnHelpConatct_Click" runat="server" ToolTip="View Instructions" CssClass="btn btn bg-gradient-orange btn-sm text-white">
                                    <i class="fa fa-info"></i>
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="lbtnViewHistoryContact" runat="server" OnClick="lbtnViewHistoryContact_Click" ToolTip="View History" CssClass="btn btn bg-gradient-primary btn-sm text-white">
                                    <i class="fa fa-history"></i>
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row m-0 align-items-center py-4 px-3">
                        <div class="col-md-12 col-lg-12">
                            <div class="row pb-3">
                                <div class="col-md-4 col-lg-4 text-left">
                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true">Email ID<span class="text-warning">*</span></asp:Label>
                                </div>
                                <div class="col-md-8 col-lg-8 text-center">
                                    <asp:TextBox Autocomplete="off" ID="tbEmailID" runat="server" CssClass="form-control form-control-sm" MaxLength="50" ToolTip="Enter Email ID" Placeholder="username@gmail.com"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="ajxFtEmailID" runat="server" FilterType=" LowercaseLetters,Numbers,Custom" ValidChars=".@-" TargetControlID="tbEmailID" />
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12 col-lg-12">
                            <div class="row pb-3">
                                <div class="col-md-4 col-lg-4 text-left">
                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true">Toll Free No</asp:Label>
                                </div>
                                <div class="col-md-8 col-lg-8 text-center">
                                    <asp:TextBox ID="tbTollFree" Autocomplete="off" runat="server" CssClass="form-control form-control-sm" MaxLength="12" ToolTip="Enter Toll Free Number" Placeholder="Max 12 digits"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="ajxFtTollFree" runat="server" FilterType="Numbers" TargetControlID="tbTollFree" />
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 col-lg-12">
                            <div class="row pb-3">
                                <div class="col-md-4 col-lg-4 text-left">
                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true">Landline No</asp:Label>

                                </div>
                                <div class="col-md-4 col-lg-4 text-center">
                                    <asp:TextBox ID="tbLandline" Autocomplete="off" runat="server" CssClass="form-control form-control-sm" MaxLength="13" ToolTip="Enter Landline Number" Placeholder="Max 13 digits"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="ajxFtLandline" runat="server" FilterType="Numbers" TargetControlID="tbLandline" />
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 col-lg-12">
                            <div class="row pb-3">
                                <div class="col-md-4 col-lg-4 text-left">
                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true">Fax No.</asp:Label>
                                </div>
                                <div class="col-md-8 col-lg-8 text-center">
                                    <asp:TextBox ID="tbFaxNo" Autocomplete="off" runat="server" CssClass="form-control form-control-sm" MaxLength="10" ToolTip="Enter Fax No." Placeholder="Max 10 digits"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="ajxFtFaxNo" runat="server" FilterType="Numbers" TargetControlID="tbFaxNo" />
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 col-lg-12">
                            <div class="row pb-3">
                                <div class="col-lg-12 col-md-12">
                                    <asp:Label runat="server" CssClass="form-control-label" Font-Size="Medium">Contact Detail</asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 col-lg-12">
                            <div class="row">
                                <div class="col-md-3 col-lg-3 offset-3">
                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="false">Contact Number</asp:Label>
                                </div>
                                <div class="col-md-3 col-lg-3">
                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="false">Office Name</asp:Label>
                                </div>
                                <div class="col-md-3 col-lg-3">
                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="false">Office Name(Local)</asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 col-lg-12">
                            <div class="row pb-3">
                                <div class="col-md-3 col-lg-3 text-left">
                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true">Primary<span class="text-warning">*</span> </asp:Label>
                                </div>
                                <div class="col-md-3 col-lg-3">
                                    <asp:TextBox ID="tbContact1" Autocomplete="off" runat="server" CssClass="form-control form-control-sm" MaxLength="10" ToolTip="Enter Contact 1" Placeholder="Max 10 digits"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="ajxFtContact1" runat="server" FilterType="Numbers" TargetControlID="tbContact1" />
                                </div>
                                <div class="col-md-3 col-lg-3">
                                    <asp:TextBox ID="tbOfficeNameContact1" Autocomplete="off" runat="server" CssClass="form-control form-control-sm" MaxLength="20" ToolTip="Enter Office Name" Placeholder="Max 20 chars"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="ajxFtOfficeName" runat="server" FilterType="Lowercaseletters,Uppercaseletters,Numbers,Custom" ValidChars="., " TargetControlID="tbOfficeNameContact1" />
                                </div>
                                <div class="col-md-3 col-lg-3">
                                    <asp:TextBox ID="tbOfficeNameContact1L" Autocomplete="off" runat="server" CssClass="form-control form-control-sm" MaxLength="20" ToolTip="Enter Office Name in Local" Placeholder="Max 20 chars"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 col-lg-12">
                            <div class="row mb-3">
                                <div class="col-md-3 col-lg-3 text-left">
                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true">Secondary</asp:Label>
                                </div>
                                <div class="col-md-3 col-lg-3">
                                    <asp:TextBox ID="tbContact2" Autocomplete="off" runat="server" CssClass="form-control form-control-sm" MaxLength="10" ToolTip="Enter Contact 2" Placeholder="Max 10 digits"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="ajxFtContact2" runat="server" FilterType="Numbers" TargetControlID="tbContact2" />
                                </div>
                                <div class="col-md-3 col-lg-3">
                                    <asp:TextBox ID="tbOfficeNameContact2" Autocomplete="off" runat="server" CssClass="form-control form-control-sm" MaxLength="50" ToolTip="Enter Office Name" Placeholder="Max 20 chars"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="ajxFTOfficeName1" runat="server" FilterType="Lowercaseletters,Uppercaseletters,Numbers" TargetControlID="tbOfficeNameContact2" />
                                </div>
                                <div class="col-md-3 col-lg-3">
                                    <asp:TextBox ID="tbOfficeNameContact2L" Autocomplete="off" runat="server" CssClass="form-control form-control-sm" MaxLength="20" ToolTip="Enter Office Name in Local" Placeholder="Max 20 chars"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 col-lg-12">
                            <div class="row pb-3">
                                <div class="col-lg-12 col-md-12">
                                    <asp:Label runat="server" CssClass="form-control-label" Font-Size="Medium">Web Info Manager</asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 col-lg-12">
                            <div class="row pb-3">
                                <div class="col-md-4 col-lg-4 text-left">
                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true">Name</asp:Label>

                                </div>
                                <div class="col-md-4 col-lg-4 text-center">
                                    <asp:TextBox ID="tbManagerName" Autocomplete="off" runat="server" CssClass="form-control form-control-sm" MaxLength="50" ToolTip="Enter Web Info Manager Name" Placeholder="Max 50 chars"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="UppercaseLetters,LowercaseLetters,custom" ValidChars=" " TargetControlID="tbManagerName" />
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 col-lg-12">
                            <div class="row pb-3">
                                <div class="col-md-4 col-lg-4 text-left">
                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true">Mobile Number</asp:Label>

                                </div>
                                <div class="col-md-4 col-lg-4 text-center">
                                    <asp:TextBox ID="tbManagerContact" Autocomplete="off" runat="server" CssClass="form-control form-control-sm" MaxLength="10" ToolTip="Enter Web Info Manager Contact Number" Placeholder="Max 10 digits"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers" TargetControlID="tbManagerContact" />
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 col-lg-12">
                            <div class="row pb-3">
                                <div class="col-md-4 col-lg-4 text-left">
                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true">Email</asp:Label>

                                </div>
                                <div class="col-md-8 col-lg-8 text-center">
                                    <asp:TextBox ID="tbManagermail" Autocomplete="off" runat="server" CssClass="form-control form-control-sm" MaxLength="50" ToolTip="Enter Web Info Manager Email" Placeholder="Max 50 chars"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="UppercaseLetters,LowercaseLetters,custom" ValidChars="@-." TargetControlID="tbManagermail" />
                                </div>
                            </div>
                        </div>



                        <div class="col-md-12 col-lg-12 text-center pt-3">
                            <asp:LinkButton ID="lbtnContactSave" runat="server" OnClick="lbtnContactSave_Click" CssClass="btn btn-success"><i class="fa fa-save"></i> Save</asp:LinkButton>
                            <asp:LinkButton ID="lbtnContactReset" runat="server" OnClick="lbtnContactReset_Click" CssClass="btn btn-warning"><i class="fa fa-undo"></i> Reset</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>


            <div class="col-md-6 col-lg-6 order-xl-2">
                <div class="card" style="min-height: 520px">
                    <div class="card-header">
                        <div class="row">
                            <div class="col-md-10 col-lg-10">
                                <asp:Label runat="server" CssClass="form-control-label"><h2>Social Media Links</h2></asp:Label>
                            </div>
                            <div class="col-lg-2 col-md-2 float-left m-0 p-0">
                                <asp:LinkButton ID="lbtnHelpSocial" runat="server" OnClick="lbtnHelpSocial_Click" ToolTip="View Instructions" CssClass="btn btn bg-gradient-orange btn-sm text-white">
                                    <i class="fa fa-info"></i>
                                </asp:LinkButton>

                                <asp:LinkButton ID="lbtnHistorySocial" runat="server" OnClick="lbtnHistorySocial_Click" ToolTip="View History" CssClass="btn btn bg-gradient-primary btn-sm text-white">
                                    <i class="fa fa-history"></i>
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="row m-0 align-items-center py-4 px-3">
                        <div class="col-md-12 col-lg-12">
                            <div class="row pb-3">
                                <div class="col-md-4 col-lg-4 text-left">
                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true">Facebook Link </asp:Label>
                                </div>
                                <div class="col-md-8 col-lg-8 text-center">
                                    <asp:TextBox ID="tbFacebooklink" Autocomplete="off" runat="server" CssClass="form-control form-control-sm" MaxLength="100" ToolTip="Enter Facebook Link" Placeholder="Max 100 Chars"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 col-lg-12">
                            <div class="row pb-3">
                                <div class="col-md-4 col-lg-4 text-left">
                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true">YouTube Link </asp:Label>
                                </div>
                                <div class="col-md-8 col-lg-8 text-center">
                                    <asp:TextBox ID="tbYoutubeLink" Autocomplete="off" runat="server" CssClass="form-control form-control-sm" MaxLength="100" ToolTip="Enter Youtube Link" Placeholder="Max 100 Chars"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 col-lg-12">
                            <div class="row pb-3">
                                <div class="col-md-4 col-lg-4 text-left">
                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true">Twitter Link </asp:Label>
                                </div>
                                <div class="col-md-8 col-lg-8 text-center">
                                    <asp:TextBox ID="tbtwitter" Autocomplete="off" runat="server" CssClass="form-control form-control-sm" MaxLength="100" ToolTip="Enter Twitter Link" Placeholder="Max 100 Chars"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 col-lg-12">
                            <div class="row pb-3">
                                <div class="col-md-4 col-lg-4 text-left">
                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true">Instagram Link </asp:Label>
                                </div>
                                <div class="col-md-8 col-lg-8 text-center">
                                    <asp:TextBox ID="tbinstagram" Autocomplete="off" runat="server" CssClass="form-control form-control-sm" MaxLength="100" ToolTip="Enter Twitter Link" Placeholder="Max 100 Chars"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 col-lg-12 text-center pt-3">
                            <asp:LinkButton ID="lbtnSaveSocial" runat="server" OnClick="lbtnSaveSocial_Click" CssClass="btn btn-success"><i class="fa fa-save"></i> Save </asp:LinkButton>
                            <asp:LinkButton ID="lbtnResetSocial" runat="server" OnClick="lbtnResetSocial_Click" CssClass="btn btn-warning"><i class="fa fa-undo"></i> Reset</asp:LinkButton>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <cc1:ModalPopupExtender ID="mpcontactHistory" runat="server" PopupControlID="pnlcontactHistory" TargetControlID="Button11" CancelControlID="btnclose3" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlcontactHistory" runat="server">
            <div class="card">
                <div class="card-header">
                    <strong class="card-title">Contact Information History</strong>
                </div>
                <div class="card-body">

                    <asp:GridView ID="gvcontactHistory" runat="server" AutoGenerateColumns="False" GridLines="None" PageSize="5" OnPageIndexChanging="gvcontactHistory_PageIndexChanging" AllowSorting="true" AllowPaging="true" CssClass="table table-flush table mar table-responsive"
                        HeaderStyle-CssClass="thead-light font-weight-bold" DataKeyNames="emailid,contact,tollfree,actionby,actiondate">
                        <Columns>
                            <asp:TemplateField HeaderText="Email">
                                <ItemTemplate>
                                    <asp:Label ID="EMAIL" CssClass="form-control-label text-center" runat="server" Text='<%# Eval("emailid") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Contact Number">
                                <ItemTemplate>
                                    <asp:Label ID="CONTACT" CssClass="form-control-label text-center" runat="server" Text='<%# Eval("contact") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Toll Free Number">
                                <ItemTemplate>
                                    <asp:Label ID="TOLLFREENO" CssClass="form-control-label text-center" runat="server" Text='<%# Eval("tollfree") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Updated By">
                                <ItemTemplate>
                                    <asp:Label ID="lblUPDATEDBY" CssClass="form-control-label text-center" runat="server" Text='<%# Eval("actionby") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Updation Date/Time">
                                <ItemTemplate>
                                    <asp:Label ID="lblUPDATEDON" CssClass="form-control-label text-center" runat="server" Text='<%# Eval("actiondate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                    </asp:GridView>

                    <asp:Label ID="lblNocontacthistory" runat="server" Text="Contact History not available"
                        Visible="true" Style="color: #d4cece; margin-top: 15px; font-size: 20px; font-weight: bold;"></asp:Label>
                </div>
                <div class="card-footer">
                    <asp:LinkButton ID="btnclose3" runat="server" CssClass="btn btn-success" Style="font-size: 11pt; float: right; border-radius: 4px;">OK</asp:LinkButton>
                </div>
            </div>
            <br />
            <div style="visibility: hidden;">
                <asp:Button ID="Button11" runat="server" Text="" />
            </div>
        </asp:Panel>
    </div>

    <div class="row">
        <cc1:ModalPopupExtender ID="mpsocialhistory" runat="server" PopupControlID="pnlsocial" TargetControlID="Button11" CancelControlID="btnclose3" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlsocial" runat="server">
            <div class="card">
                <div class="card-header">
                    <strong class="card-title">Social Information History</strong>
                </div>
                <div class="card-body">

                    <asp:GridView ID="gvSocialHistory" runat="server" AutoGenerateColumns="False" GridLines="None" PageSize="5" OnPageIndexChanging="gvSocialHistory_PageIndexChanging" AllowSorting="true" AllowPaging="true" CssClass="table table-flush table mar table-responsive"
                        HeaderStyle-CssClass="thead-light font-weight-bold" DataKeyNames="youtube,facebook,insta,twitter,actionby,actiondate">
                        <Columns>
                            <asp:TemplateField HeaderText="Youtube link">
                                <ItemTemplate>
                                    <asp:Label CssClass="form-control-label text-center" runat="server" Text='<%# Eval("youtube") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Facebooklink">
                                <ItemTemplate>
                                    <asp:Label CssClass="form-control-label text-center" runat="server" Text='<%# Eval("facebook") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Instagram">
                                <ItemTemplate>
                                    <asp:Label CssClass="form-control-label text-center" runat="server" Text='<%# Eval("insta") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Twitter">
                                <ItemTemplate>
                                    <asp:Label CssClass="form-control-label text-center" runat="server" Text='<%# Eval("twitter") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Updated By">
                                <ItemTemplate>
                                    <asp:Label CssClass="form-control-label text-center" runat="server" Text='<%# Eval("actionby") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Updation Date/Time">
                                <ItemTemplate>
                                    <asp:Label CssClass="form-control-label text-center" runat="server" Text='<%# Eval("actiondate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                    </asp:GridView>

                    <asp:Label ID="lblNosocialhistory" runat="server" Text="Social History not available"
                        Visible="true" Style="color: #d4cece; margin-top: 15px; font-size: 20px; font-weight: bold;"></asp:Label>
                </div>
                <div class="card-footer">
                    <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-success" Style="font-size: 11pt; float: right; border-radius: 4px;">OK</asp:LinkButton>
                </div>
            </div>
            <br />
            <div style="visibility: hidden;">
                <asp:Button ID="Button1" runat="server" Text="" />
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

