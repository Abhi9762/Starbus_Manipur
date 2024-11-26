<%@ Page Title="" Language="C#" MasterPageFile="~/mainMasterPage.master" AutoEventWireup="true" CodeFile="Routes.aspx.cs" Inherits="Routes" ValidateRequest="false" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="assets/js/jquery-3.3.1.js"></script>
    <link href="assets/css/jquery-ui.css" rel="stylesheet" />

    <script src="assets/vendor/jquery/dist/jquery.min.js"></script>


    <script src="assets/js/jquery.dataTables.min.js"></script>

    <link href="assets/css/jquery.dataTables.min.css" rel="stylesheet" />
    <script type="text/javascript">
        //$(function () {
        //    $("[id*=gvRoutes]").DataTable(
        //        {
        //            bLengthChange: true,
        //            lengthMenu: [[10, 20, -1], [10, 20, "All"]],
        //            bFilter: true,
        //            bSort: true,
        //            bPaginate: true
        //        });
        //    });


        $(document).ready(function () {
            $.noConflict();
            $("[id*=gvRoutes]").DataTable(
                {
                    bLengthChange: true,
                    lengthMenu: [[10, 20, -1], [10, 20, "All"]],
                    bFilter: true,
                    bSort: true,
                    bPaginate: true
                });
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="midd-bg">

        <%-- <div class="container">
            <div class="row">
                <div class="col-lg-12 text-center">
                    <asp:Image ID="imgcommingsoon" runat="server" ImageUrl="~/images/coming-soon.png" />
                </div>
            </div>
        </div>--%>
        <div class="container">
            <div class="row no-gutters">

                <div class="col-md-12 wrap-about ftco-animate">
                    <div class="heading-section pl-md-5">
                        <h3 class="mb-4">
                            <div class="tx5" style="text-decoration: underline; padding-bottom: 2px">Popular <span class="org">Routes</span></div>

                            <div class="col-lg-9 col-md-12 col-sm-12 ">
                                <marquee direction="right" width="100%" scrollamount="8">
                                                <asp:Repeater ID="rptTopRoutes" runat="server"  OnItemCommand="rptTopRoutes_ItemCommand">
                                                    <ItemTemplate>
                                                         <asp:HiddenField ID="hdfromStation1" runat="server" Value='<%#Eval("fromst")%>' />
                                                          <asp:HiddenField ID="hdfromStation2" runat="server" Value='<%#Eval("tost")%>'/>
                                                        <asp:HiddenField ID="hdfrmstation" runat="server" Value='<%#Eval("from_station_name")%>' />
                                                          <asp:HiddenField ID="hdtostation" runat="server" Value='<%#Eval("to_station_name")%>'/>
                                                        <asp:LinkButton ID="lbtnroute" runat="server" Text='<%#Eval("route_name")%>' ToolTip="Click to go for Bus Services of the route"></asp:LinkButton>                                                        | 
                                                    </ItemTemplate>
                                                </asp:Repeater>


                                            </marquee>
                            </div>
                    </div>
                    <div class="heading-section pl-md-5  overflow-auto">
                        <div class="row">
                            <div class="col-3">
                                <h3 class="mb-4">
                                    <div class="tx5" style="text-decoration: underline; padding-bottom: 2px">All <span class="org">Routes</span></div>
                                </h3>
                            </div>
                            <div class="col-9 text-right text-sm mt-4 text-danger">
                                Click The Route To Find Available Bus Service.
                            </div>

                        </div>

                        <div class="block-23 mb-3 ">
                            <asp:GridView ID="gvRoutes" runat="server" AutoGenerateColumns="false" EnableModelValidation="True"
                                GridLines="None" CssClass="table" DataKeyNames="" ClientIDMode="Static" AllowPaging="false" OnRowDataBound="gvRoutes_RowDataBound" OnSelectedIndexChanged="gvRoutes_SelectedIndexChanged">
                                <Columns>
                                    <asp:TemplateField HeaderText="#" ItemStyle-Width="50">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Route">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRoute_Name_En" runat="server" Text='<%#Eval("routename") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="From Station">
                                        <ItemTemplate>
                                            <asp:Label ID="lblfromStationName" runat="server" Text='<%#Eval("fromstation") %>'></asp:Label>
                                            <asp:Label ID="lblfromstation" Visible="false" runat="server" Text='<%#Eval("frston") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="To Station">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltoStationName" runat="server" Text='<%#Eval("tostation") %>' />
                                            <asp:Label ID="lbltostation" Visible="false" runat="server" Text='<%#Eval("toston") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

