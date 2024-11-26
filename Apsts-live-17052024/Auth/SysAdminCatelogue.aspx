<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/SysAdmmaster.master" AutoEventWireup="true" CodeFile="SysAdminCatelogue.aspx.cs" Inherits="Auth_SysAdminCatelogue" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid pt-top ">
        <asp:UpdatePanel runat="server" ID="UpdatePanelall" UpdateMode="Always">
            <ContentTemplate>
                <div class="row">
                    <div class="col-lg-12">
                        <asp:Repeater ID="rptgroup" runat="server" OnItemDataBound="rptgroup_ItemDataBound">
                            <ItemTemplate>
                                <div class="row">
                                    <div class="col-lg-12">
                                        <asp:Label ID="lblgroupname" runat="server" Text='<%#Eval("group_name") %>' Style="font-size: 16pt; font-weight: bold; color: #074886;"></asp:Label>
                                        <asp:Label ID="lblgroupid" runat="server" Text='<%#Eval("group_id") %>' Visible="false"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <asp:Repeater ID="rptgroupmodule" runat="server" OnItemCommand="rptgroupmodule_ItemCommand">
                                        <ItemTemplate>
                                            <div class="col-md-3  stretch-card transparent">
                                                <div class="card card-dark-blue">
                                                    <div class="card-body">
                                                        <asp:Label ID="lblmoduleurl" runat="server" Visible="false" Text='<%#Eval("module_url") %>'></asp:Label>
                                                        <div class="card-heading"><%# Eval("module_name")%></div>
                                                        <div class="total-tx mb-4"><%#Eval("about_module") %> </div>
                                                        <div class="col text-right">
                                                            <asp:LinkButton ID="LBGenconfigExplore" runat="server" class="btn btn-sm btn-primary" ToolTip="Explore" CommandArgument='<%# Eval("module_id")%>'
                                                                CommandName="View"> <i class="ni ni-cloud-download-95 mttop "></i>  Explore
                                                            </asp:LinkButton>
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

