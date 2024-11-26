<%@ Page Title="" Language="C#" MasterPageFile="~/mainMasterPage.master" AutoEventWireup="true" CodeFile="Notification.aspx.cs" Inherits="Notification" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div class="row">
            <div class="col-7">
                <div class="card">
                    <div class="card-body" style="min-height: 600px">
                        <div class="card-header">
                            Latest
                        </div>
                        <asp:GridView ID="grdLatest" OnPageIndexChanging="grdLatest_PageIndexChanging" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                            GridLines="None" DataKeyNames=""
                            class="table table-hover mb-0 mt-2" PageSize="8" Font-Size="13px">
                            <Columns>
                                <asp:TemplateField HeaderText="Category">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDepot" runat="server" Text='<%#  Eval("noticecategory_name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Subject">
                                    <ItemTemplate>
                                        <asp:Label ID="lblServiceCode" runat="server" Text='<%#  Eval("sub_ject") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="From Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTripCode" runat="server" Text='<%#  Eval("valid_fromdt") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="To Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFrom" runat="server" Text='<%#  Eval("valid_todt") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <%--<asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnproceed" runat="server" CssClass="btn btn-primary btn-sm"
                                            CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="proceed"
                                            Style="padding: 1px 6px; border-radius: 5px; font-size: 12px;"><i class="fa fa-arrow-right"></i></asp:LinkButton>
                                        <asp:LinkButton ID="bntView" runat="server" CssClass="btn btn-success btn-sm"
                                            CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="view"
                                            Style="padding: 1px 6px; border-radius: 5px; font-size: 12px;"><i class="fa fa-eye"></i></asp:LinkButton>


                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                            </Columns>
                            <PagerStyle HorizontalAlign="Left" CssClass="gridview" />
                        </asp:GridView>
                        <div class="Row" id="divLatest" runat="server" visible="true">
                            <div class="col-12 text-center mt-9">
                              <span style="font-size:40px;color:lightgray;font-weight:bold">No Record Found</span>  
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-5">
                <div class="card">
                    <div class="card-body" style="min-height: 600px">
                        <div class="card-header">
                            Archives
                        </div>
                        <asp:GridView ID="grdArchive" OnPageIndexChanging="grdArchive_PageIndexChanging" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                            GridLines="None" DataKeyNames=""
                            class="table table-hover mb-0 mt-2" PageSize="8" Font-Size="13px">
                            <Columns>
                                <asp:TemplateField HeaderText="Category">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDepot" runat="server" Text='<%#  Eval("noticecategory_name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Subject">
                                    <ItemTemplate>
                                        <asp:Label ID="lblServiceCode" runat="server" Text='<%#  Eval("sub_ject") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                             
                                <%--<asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnproceed" runat="server" CssClass="btn btn-primary btn-sm"
                                            CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="proceed"
                                            Style="padding: 1px 6px; border-radius: 5px; font-size: 12px;"><i class="fa fa-arrow-right"></i></asp:LinkButton>
                                        <asp:LinkButton ID="bntView" runat="server" CssClass="btn btn-success btn-sm"
                                            CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="view"
                                            Style="padding: 1px 6px; border-radius: 5px; font-size: 12px;"><i class="fa fa-eye"></i></asp:LinkButton>


                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                            </Columns>
                            <PagerStyle HorizontalAlign="Left" CssClass="gridview" />
                        </asp:GridView>
                         <div class="Row" id="divArchive" runat="server" visible="true">
                            <div class="col-12 text-center mt-9">
                              <span style="font-size:40px;color:lightgray;font-weight:bold">No Record Found</span>  
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>
</asp:Content>

