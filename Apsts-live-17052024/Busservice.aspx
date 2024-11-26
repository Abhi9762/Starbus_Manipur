<%@ Page Title="" Language="C#" MasterPageFile="~/mainMasterPage.master" AutoEventWireup="true" CodeFile="Busservice.aspx.cs" Inherits="Busservice" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">


    <link href="assets/css/jquery.dataTables.min.css" rel="stylesheet" />
    <script src="DataTables/js/jquery.dataTables.min.js"></script>
    <script src="DataTables/js/dataTables.buttons.min.js"></script>
    <script src="DataTables/js/buttons.print.min.js"></script>
    <script src="DataTables/js/buttons.html5.min.js"></script>
    <script src="DataTables/js/pdfmake.min.js"></script>
    <script src="DataTables/js/vfs_fonts.js"></script>
    <script src="DataTables/js/jszip.min.js"></script>

    <style>
        .modalBackground {
            background-color: black;
            opacity: 0.6;
        }

        .table {
            width: 100%;
        }

            .table th, .table td {
                padding: .5rem 0.75rem;
                vertical-align: top;
                border-top: 1px solid #dce1e3;
                font-size: 13px;
            }

        table.dataTable thead th, table.dataTable thead td {
            padding: 10px 18px;
            border-bottom: 1px solid #c4c1c1;
        }

        table.dataTable.no-footer {
            border-bottom: 1px solid #c7c2c2;
        }

        table.dataTable {
            text-transform: uppercase;
        }

        .node {
            height: 10px;
            width: 10px;
            border-radius: 50%;
            display: inline-block;
            transition: all 1000ms ease;
        }

        .activated {
            box-shadow: 0px 0px 3px 2px rgba(194, 255, 194, 0.8);
        }

        .divider {
            height: 40px;
            width: 2px;
            margin-left: 4px;
            transition: all 800ms ease;
        }

        li p {
            display: inline-block;
            margin-left: 25px;
        }
        
        .green {
            background-color: rgba(92, 184, 92, 1);
        }
        .grey {
            background-color: rgba(201, 201, 201, 1);
        }


    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="midd-bg">
        <div class="container">
            <div class="row ">
                <div class="col-lg-5 text-center">
                    <asp:Image ID="imgbuservice" runat="server" Style="height: 320px; width: 350px; border-radius: 100%;" />
                </div>

                <div class="col-lg-7">
                    <h3>
                        <asp:Label ID="lblservice" runat="server"></asp:Label></h3>
                    <div class="heading-top2">Overview</div>
                    <div class="hd-text2 ">
                        <asp:Label ID="lbloverview" runat="server"></asp:Label>
                    </div>
                    <div class="heading-top2">Amenities</div>
                    <asp:Repeater ID="rptaminities" runat="server" Visible="false">
                        <ItemTemplate>
                            <asp:Image ID="imgami" runat="server" ToolTip='<%# Eval("Aminity") %>' ImageUrl='<%# Eval("AminityIcon") %>' />
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <div class="row mt-3">
                <div class="col-lg-12">
                    <h2 class="text-center">Timetable</h2>
                    <div class="heading-section mb-8">
                        <asp:GridView ID="gvTiming" runat="server" GridLines="None" CssClass="table table-hover" ClientIDMode="Static"
                            AutoGenerateColumns="false" DataKeyNames="strpid,dsvcid,srtpid,frm_station,to_station,strt_time,online_booking"
                            OnRowCommand="gvTiming_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="#">
                                    <HeaderTemplate>
                                        <div class="row">
                                            <div class="col-sm-1 col-1 d-none d-sm-block d-md-block d-lg-block">
                                                #
                                            </div>
                                            <div class="col-sm-3 col-5">
                                                From Station
                                            </div>
                                            <div class="col-sm-3 col-5">
                                                To Station
                                            </div>
                                            <div class="col-sm-2 col-2">
                                                Time
                                            </div>
                                            <div class="col-sm-2  d-none d-sm-block d-md-block d-lg-block">
                                                Online Booking
                                            </div>
                                            <div class="col-sm-1  d-none d-sm-block d-md-block d-lg-block">
                                                Route
                                            </div>
                                        </div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                       
                                        <div class="row">
                                            <div class="col-sm-1 col-1 d-none d-sm-block d-md-block d-lg-block">
                                                 <%#Container.DataItemIndex + 1 %>
                                            </div>
                                            <div class="col-sm-3 col-5">
                                                 <%#Eval("frm_station") %>
                                            </div>
                                            <div class="col-sm-3 col-5">
                                                 <%#Eval("to_station") %>
                                            </div>
                                            <div class="col-sm-2 col-2">
                                                <%#Eval("strt_time") %>
                                            </div>
                                            <div class="col-sm-2 col-10">
                                              <p class="d-block d-sm-none d-md-none d-lg-none mt-2 mb-0" style="font-size:12px;">Online Booking - <b><%#Eval("online_booking") %> </b></p>  
                                               <span class="d-none d-sm-block d-md-block d-lg-block"> <%#Eval("online_booking") %></span>
                                            </div>
                                            <div class="col-sm-1 col-2">
                                                <asp:LinkButton ID="lbtnView" runat="server" CssClass="btn btn-warning btn-sm"
                                            CommandName="VIEWDETAILS"
                                            CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'><i class="fa fa-road"></i></asp:LinkButton>
                                            </div>

                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>

                        </asp:GridView>
                        <asp:Panel ID="pnlNoservice" runat="server" CssClass="services w-100">
                            <div class="text w-100">
                                <p class="mt-2">
                                    <asp:Label ID="lblNoNotice" runat="server" Text="No Service available"></asp:Label>
                                </p>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>

            <div class="row">
            <cc1:ModalPopupExtender ID="mpDetail" runat="server" PopupControlID="pnlmpDetail"
                CancelControlID="lbtnCancempDetail" TargetControlID="btnOpenmpDetail" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlmpDetail" runat="server" Style="position: fixed;">
                <div class="card" style="min-width: 40vw; max-width: 90vw; overflow: auto;">
                    <div class="card-header">
                        <div class="row">
                            <div class="col">
                                <asp:Label ID="lblHeader" runat="server" CssClass="text-uppercase"></asp:Label>
                            </div>
                            <div class="col-auto">
                                <asp:LinkButton ID="lbtnCancempDetail" runat="server" CssClass="btn btn-danger btn-sm">
                                        <i class="fa fa-times"></i>
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="card-body text-left overflow-auto p-4" style="min-height: 300px; max-height: 75vh;">
                        <ul id="progress mb-0">
                            <asp:ListView ID="lvTimetableView" runat="server">
                                <ItemTemplate>
                                    <li>
                                        <div class="node green"></div>
                                        <p class="mb-0"><%# Eval("ston_name") %><br /><%#Eval("outtime") %></p>
                                    </li>
                                    <li>
                                        <div class="divider grey"></div>
                                    </li>
                                </ItemTemplate>
                            </asp:ListView>
                            <li>
                                <div class="node grey"></div>
                                <p>approximate time!</p>
                            </li>
                        </ul>
                        <asp:Panel ID="PanelNoRecordTimeTableView" runat="server" Width="100%" Visible="true">
                            <div class="col-md-12 p-0" style="text-align: center; text-transform: uppercase;">
                                <div class="col-md-12 busListBox" style="color: #e3e3e3; padding-top: 35px; padding-bottom: 35px; font-size: 25px; font-weight: bold;">
                                    Details are not available<br />
                                    Please contact to helpdesk
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                   
                </div>
                <div style="visibility: hidden;">
                    <asp:Button ID="btnOpenmpDetail" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>

        </div>
    </div>

    <script type="text/javascript">
        $('#gvTiming').DataTable({
            "pageLength": 15,
            dom: 'Bfrtip',
            "bSort": true,
            buttons: [
               'print'
            ]
        });
    </script>

</asp:Content>

