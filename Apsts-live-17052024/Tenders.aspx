<%@ Page Title="" Language="C#" MasterPageFile="~/mainMasterPage.master" AutoEventWireup="true" CodeFile="Tenders.aspx.cs" Inherits="Tenders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div class="row mt-2 mb-1">

            <div class="col-lg-12">
                <div class="row p-2">
                   
                    <asp:Repeater ID="rpttender" runat="server" OnItemDataBound="rpttender_ItemDataBound" OnItemCommand="rpttender_ItemCommand">
                        <ItemTemplate>
                            <div class="col-8 mt-3">
                                 <asp:HiddenField ID="docs" runat="server" Value='<%#Eval("document_")%>' />
                                <asp:Label ID="lblCategory" Visible="false" runat="server" Text='<%#Eval("ctegorycode")%>'></asp:Label>
                                <asp:Label ID="lblNotice" Visible="false" runat="server" Text='<%#Eval("noticeid")%>'></asp:Label>
                                <h1>
                                    <asp:Label ID="lblTitle" runat="server"><%#Eval("sub")%></asp:Label>
                                </h1>
                                <h4>
                                    <asp:Label ID="lblDescription" runat="server"><%#Eval("descr")%></asp:Label>
                                </h4>

                                <h4>
                                    <span class="text-muted ">Valid From</span><br />
                                    <asp:Label ID="lblValidFromDt" runat="server"><%#Eval("validfrom")%></asp:Label>
                                </h4>
                                <h4>
                                    <span class="text-muted ">Valid To</span><br />
                                    <asp:Label ID="lblValidToDt" runat="server"><%#Eval("validto")%></asp:Label>
                                </h4>
                                 <h3 class="mt-1">
                                    <asp:Label ID="lblDocument" CssClass="text-dark"  Text="For more details you may download this" runat="server"></asp:Label>
                                    <asp:LinkButton ID="lbtnDocument" CommandName="Document"  CssClass="btn-link" runat="server">Document</asp:LinkButton>
                                </h3>
                                <h4 class="mt-1">
                                    <asp:Label ID="lblURL" CssClass="text-dark" runat="server"></asp:Label>
                                    <asp:Label ID="lblURLLink" CssClass="btn-link" runat="server" Text='<%#Eval("url_")%>'></asp:Label>
                                </h4>
                                
                            </div>
                              <div class="col-4 mt-3">
                                  <asp:ImageButton ID="img" runat="server" CssClass="d-block" Width="50%" Style="border-radius: 7px;" />
                            </div>
                           
                             <%--     <h4 class="mt-1">
                                    <asp:Label ID="lblURL" CssClass="text-dark" runat="server"></asp:Label>
                                    <asp:Label ID="lblURLLink" CssClass="btn-link" runat="server"></asp:Label>
                                </h4>
                            </div>
                            <div class="col-lg-3">
                                <asp:ImageButton ID="img" runat="server" CssClass="d-block w-100" Style="border-radius: 7px;" />

                            </div>--%>
                        </ItemTemplate>
                    </asp:Repeater>
                     <hr />
                </div>
                <div id="divnodata" runat="server" visible="false">
                    <div class="row">
                        <div class="col-12 text-center">
                            <h1>Tender Not Available</h1>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>
</asp:Content>

