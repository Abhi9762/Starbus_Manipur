<%@ Page Title="" Language="C#" MasterPageFile="~/mainMasterPage.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="NoticeNewsDetails.aspx.cs" Inherits="NoticeNewsDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container" style="min-height: 50vh;">
        <div class="row mt-2 mb-1">
            <div class="col-lg-12">
                <h2 class="mt-2">
                    <asp:Label ID="lblTitle" CssClass="text-primary" runat="server"></asp:Label>
                </h2>
                <h2>
                    <asp:Label ID="lblSubject" Font-Bold="true" runat="server"></asp:Label>
                </h2>
                <h3>
                    <asp:Label ID="lblDescription" runat="server"></asp:Label>
                </h3>
                <h3 class="mt-3">
                    <asp:Label ID="lblDocument" CssClass="text-dark" runat="server"></asp:Label>
                    <asp:LinkButton ID="lbtnDocument" OnClick="lbtnDocument_Click" CssClass="btn-link" runat="server">Document</asp:LinkButton>
                </h3>
                <h4 class="mt-3">
                    <asp:Label ID="lblURL" CssClass="text-dark" runat="server"></asp:Label>
                    <asp:Label ID="lblURLLink" CssClass="btn-link" runat="server"></asp:Label>
                </h4>

            </div>
        </div>
        <div class="row">
            <cc1:ModalPopupExtender ID="mpsuccess" runat="server" PopupControlID="pnlsuccess"
                CancelControlID="lbtnsuccessclose1" TargetControlID="Button6" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlsuccess" runat="server" Style="position: fixed;">
                <div class="card" style="width: 350px;">
                    <div class="card-header">
                        <h4 class="card-title text-left mb-0">Information
                        </h4>
                    </div>
                    <div class="card-body text-left pt-2" style="min-height: 100px;">
                        <asp:Label ID="lblsuccessmsg" runat="server" Text="Do you want to Update ?"></asp:Label>
                        <div style="width: 100%; margin-top: 20px; text-align: right;">
                            <asp:LinkButton ID="lbtnsuccessclose1" runat="server" CssClass="btn btn-success btn-sm ml-2"> <i class="fa fa-check"></i> Close </asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div style="visibility: hidden;">
                    <asp:Button ID="Button6" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>

