<%@ Page Title="" Language="C#" MasterPageFile="~/mainMasterPage.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="NoticNewsAllDetails.aspx.cs" Inherits="Auth_NoticNewsAllDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div class="row mt-2 mb-1">
            <div class="col-lg-12">
                <div class="item">
                    <div class="route-bg">
                        <div class="row p-2">
                            <div class="my-3">
                                <asp:Label ID="lblNewsSubject" CssClass="text-white " runat="server"><%#Eval("sub_ject")%></asp:Label>
                            </div>

                            <div class="col-lg-12">
                                <h1>
                                    <asp:Label ID="lblTitle" runat="server"></asp:Label>
                                </h1>
                                <h4>
                                    <asp:Label ID="lblSubject" runat="server"></asp:Label>
                                </h4>
                                <h4>
                                    <asp:Label ID="lblDescription" runat="server"></asp:Label>
                                </h4>

                                <h4>
                                    <span class="text-muted ">Valid From</span><br />
                                    <asp:Label ID="lblValidFromDt" runat="server"></asp:Label>
                                </h4>
                                <h4>
                                    <span class="text-muted ">Valid To</span><br />
                                    <asp:Label ID="lblValidToDt" runat="server"></asp:Label>
                                </h4>
                                <h3 class="mt-1">
                                    <asp:Label ID="lblDocument" CssClass="text-dark" runat="server"></asp:Label>
                                    <asp:LinkButton ID="lbtnDocument" OnClick="lbtnDocument_Click" CssClass="btn-link" runat="server">Document</asp:LinkButton>
                                </h3>
                                <h4 class="mt-1">
                                    <asp:Label ID="lblURL" CssClass="text-dark" runat="server"></asp:Label>
                                    <asp:Label ID="lblURLLink" CssClass="btn-link" runat="server"></asp:Label>
                                </h4>
                            </div>
                            <div class="col-lg-6">
                                <asp:ImageButton ID="img" runat="server" CssClass="d-block w-100" Style="border-radius: 7px;" />

                            </div>
                           

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

