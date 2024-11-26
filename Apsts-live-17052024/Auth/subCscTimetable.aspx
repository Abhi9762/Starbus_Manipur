<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/subCscMaster.Master" AutoEventWireup="true" CodeFile="subCscTimetable.aspx.cs" Inherits="Auth_subCscTimetable" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

  <script src="../assets/js/argon.js?v=1.2.0"></script>
   <%-- <script src="../assets/js/jquery-n.js"></script>--%>
    <link href="../assets/css/jquery-ui.css" rel="stylesheet" />
    <script type="text/javascript">
        $(document).ready(function () {
            $("[id$=tbFrom]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "subCscTimetable.aspx/searchStations",
                        data: "{'stationText':'" + request.term + "','fromTo':'F'}",
                        dataType: "json",
                        success: function (data) {
                            response(data.d);
                        },
                        error: function (result) {
                        }
                    });
                }
            });
            $("[id$=tbTo]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "subCscTimetable.aspx/searchStations",
                        data: "{'stationText':'" + request.term + "','fromTo':'T'}",
                        dataType: "json",
                        success: function (data) {
                            response(data.d);
                        },
                        error: function (result) {
                        }
                    });
                }
            });
        });
    </script>
    <style>
        .table th, .table td {
            padding: 0rem;
            vertical-align: top;
            border-top: none;
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

    <div class="container pt-2">
      
        <div class="card py-3 px-5" style="box-shadow: 0px 5px 12px -1px rgb(0 0 0 / 6%);">
            <div class="row">
                <div class="col-md-3">
                    <div class="form-group mb-2">
                        <asp:Label ID="lblFromStationHeader" runat="server" Text="From Station" CssClass="label font-weight-light mb-0" Style="font-size: 12px;"></asp:Label>
                        <asp:TextBox ID="tbFrom" runat="server" MaxLength="50" class="form-control ui-autocomplete-input text-uppercase" type="Search"
                            placeholder="Station Name" autocomplete="off"></asp:TextBox>
                    </div>
                </div>

                <div class="col-md-3">
                    <div class="form-group mb-2">
                        <asp:Label ID="lblToStationHeader" runat="server" Text="To Station" CssClass="label font-weight-light mb-0" Style="font-size: 12px;"></asp:Label>
                        <asp:TextBox ID="tbTo" runat="server" MaxLength="50" class="form-control ui-autocomplete-input text-uppercase" type="Search" placeholder="Station Name" autocomplete="off"></asp:TextBox>
                    </div>
                </div>

                <div class="col-md-3">
                    <div class="form-group mb-2">
                        <asp:Label ID="Label1" runat="server" Text="Service Type" CssClass="label font-weight-light mb-0" Style="font-size: 12px;"></asp:Label>
                        <asp:DropDownList runat="server" ID="ddlservices" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div>

                <div class="col-md-3 mt-1 text-center">
                    <div class="form-group mt-3">
                        <asp:LinkButton ID="lbtnSearchServices" runat="server" CssClass="btn btn-primary w-100 py-2"
                            OnClick="lbtnSearchServices_Click">
                            <i class="fa fa-search"></i> Search
                        </asp:LinkButton>

                    </div>
                </div>

            </div>
        </div>
        <div class="row mb-8 mt-5">
            <div class="col-md-12">
                <asp:GridView ID="gvlist" runat="server" GridLines="None" CssClass="table table-hover" ClientIDMode="Static" ShowHeader="false"
                    OnRowCommand="gvlist_RowCommand"
                    AutoGenerateColumns="false" DataKeyNames="dsvcid,servicename,strpid,fr_city,to_city,depttime, arrtime,tripdirection,srtpid, servicetypename,routename,tripname,amenity,amenityurl,is_online_booking">
                    <Columns>

                        <asp:TemplateField HeaderText="#">
                            <ItemTemplate>
                                <div class="card" style="border-radius: 8px;">

                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col">
                                                <div class="row">
                                                    <div class="col">
                                                        <h3 class="mb-3">
                                                            <%# Eval("dsvcid") %><%# Eval("tripdirection") %><%# Eval("strpid") %> |  <%# Eval("servicetypename") %>
                                                            <span class="text-sm text-gray ml-3"><%# Eval("is_online_booking").ToString().ToUpper() == "YES" ? "<i class='fa fa-check'></i> Online Booking available" : "<i class='fa fa-times'></i> Online Booking not available" %></span>
                                                        </h3>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-6">

                                                        <h3 class="mb-0"><%#Eval("depttime") %></h3>
                                                        <p class="mb-0 text-sm"><%#Eval("fr_city") %></p>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <h3 class="mb-0"><%#Eval("arrtime") %></h3>
                                                        <p class="mb-0 text-sm"><%#Eval("to_city") %></p>
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="col-auto">
                                                <asp:LinkButton ID="lbtnView" runat="server" CssClass="btn btn-warning mt-3"
                                                    CommandName="VIEWDETAILS"
                                                    CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'>
                                                    <i class="fa fa-road"></i><br />
                                                    Show Route
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>
                <asp:Panel ID="pnlNoservice" runat="server" CssClass="services w-100">
                    <div class="text w-100">
                        <p class="mt-2">
                            <asp:Label ID="lblNoService" runat="server" Text="No Service available"></asp:Label>
                        </p>
                    </div>
                </asp:Panel>
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

        <div class="row">
            <cc1:ModalPopupExtender ID="mpMsg" runat="server" PopupControlID="pnlmpMsg"
                CancelControlID="lbtnCancempMsg" TargetControlID="btnOpenmpMsg" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlmpMsg" runat="server" Style="position: fixed;">
                <div class="card" style="min-height: 200px; max-height: 85vh; min-width: 450px; max-width: 90vw; overflow: auto;">
                    <div class="card-header">
                        <asp:Label ID="lblmpMsgHeader" runat="server" CssClass="text-uppercase"></asp:Label>
                    </div>
                    <div class="card-body text-left overflow-auto">
                        <asp:Label ID="lblmpMsgText" runat="server"></asp:Label>
                    </div>
                    <div class="card-footer p-2 text-right">
                        <asp:LinkButton ID="lbtnCancempMsg" runat="server" CssClass="btn btn-danger py-1">
                                       Ok
                        </asp:LinkButton>
                    </div>
                </div>
                <div style="visibility: hidden;">
                    <asp:Button ID="btnOpenmpMsg" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>

    </div>
</asp:Content>



