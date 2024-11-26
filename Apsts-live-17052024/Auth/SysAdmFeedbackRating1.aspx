<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/SysAdmmaster.master" AutoEventWireup="true" CodeFile="SysAdmFeedbackRating1.aspx.cs" Inherits="Auth_SysAdmFeedbackRating1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../assets/js/jquery.min.js" type="text/javascript"></script>
    <link href="../assets/css/UIMin.css" rel="stylesheet" type="text/css" />
    <script src="../assets/js/jqueryuimin.js" type="text/javascript"></script>
    <script type="text/javascript">
</script>
    <style>
        .modalBackground {
            background-color: black;
            opacity: 0.6;
        }

        .card-body {
            padding: 8px;
        }
    </style>
    <style>
        .gvTable {
            width: 100%;
            max-width: 100%;
            margin-bottom: 1rem;
            background-color: transparent;
            font-size: 10pt;
            margin-top: 5px;
        }

            .gvTable td, .gvTable th {
                padding: 3px;
                vertical-align: top;
                border-top: 1px solid #dee2e6;
            }



        .GridPager td {
            padding-top: 2px;
            padding-left: 3px;
        }

        .GridPager a, .GridPager span {
            display: block;
            height: 22px;
            min-width: 17px;
            font-weight: bold;
            text-align: center;
            text-decoration: none;
        }

        .GridPager a {
            background-color: #f5f5f5;
            color: #969696;
            border: 1px solid #969696;
            border-radius: 0px;
        }

            .GridPager a .hover {
                background-color: red;
                color: #969696;
                border: 1px solid #969696;
            }

        .GridPager span {
            background-color: #A1DCF2;
            color: #000;
            border: 1px solid #3AC0F2;
            border-radius: 0px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hidtoken" runat="server" />
    <asp:HiddenField ID="HiddenFieldTopConductorCode" Value="0" runat="server" />
    <asp:HiddenField ID="HiddenFieldTopBusServiceCode" Value="0" runat="server" />
    <asp:HiddenField ID="HiddenFieldBottomConductorCode" Value="0" runat="server" />
    <asp:HiddenField ID="HiddenFieldBottomBusServiceCode" Value="0" runat="server" />
    <div class="container-fluid" style="padding-top: 15px; padding-bottom: 30px;">

        <div class="row">
            <div class="col-lg-12">
                <div class="card">
                    <div class="card-body" style="padding: 8px;">

                        <div class="row">
                             <div class="col-md-7">
                                <h3 class="mt-1 float-right ">Years</h3>
                            </div>

                            <div class="col-md-2">
                                <asp:DropDownList ID="ddlyears" runat="server" AutoPostBack="true" data-toggle="tooltip" data-placement="bottom" OnSelectedIndexChanged="ddlyears_SelectedIndexChanged" title="Fare" CssClass="form-control form-control-sm" runat="server">
                                    <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                   
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-1">
                                <h3 class="mt-1 float-right ">Month</h3>
                            </div>

                            <div class="col-md-2">
                                <asp:DropDownList ID="ddlmonth" data-toggle="tooltip" AutoPostBack="true" data-placement="bottom" title="Fare" OnSelectedIndexChanged="ddlmonth_SelectedIndexChanged" CssClass="form-control form-control-sm" runat="server">
                                    <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                   
                                </asp:DropDownList>
                            </div>
                        </div>

                    </div>

                </div>
            </div>
        </div>
        <div class="row">

            <div class="col-lg-3">
                <div class="card mb-2" style="border-color: #28a745;">
                    <div class="card-body" style="padding: 4px;height: 190px;">
                      
                        <p class="text-dark text-center mb-1" style="font-weight: bold;">
                            Conductor Of The Month - <asp:Label ID="lblconductormonth" runat="server" Text="N/A"></asp:Label> <br />(<asp:Label ID="LabelConductorName" runat="server" Text="N/A"></asp:Label>)
                        </p>
                        <div class="row">
                          
                            <div class="col-lg-6 pl-0">
                                <center>
                                   <p class="text-dark mb-1" style="font-weight: bold;">
                                        <i class="fa fa-star text-success"></i><i class="fa fa-star text-success" style="margin-left: 5px; font-size: 25px; margin-right: 5px;"></i><i class="fa fa-star text-success"></i>
                                    </p>
                                   
                                    <p class="text-dark mb-1" style="font-size: 12px; text-decoration: underline;">
                                         <asp:LinkButton ID="lbtnconductor" runat="server" Visible="false" OnClick="lbtnconductor_Click" ForeColor="Orange"
                                            Text="Download Certificate"></asp:LinkButton>
                                        <asp:Label ID="lblconductorofmonth" runat="server" Visible="false" Text="Not Available"></asp:Label>
                                    </p>
                                   
                                      
                                </center>
                            </div>
                            <div class="col-lg-6 pr-0" style="border-left: 1px solid #ece6e6; height: 100%;">
                                <p class="text-dark mb-0" style="font-weight: bold; font-size: 13px;">
                                    Depot
                                </p>
                                <p class="text-dark mb-0" style="font-weight: bold;">
                                    <asp:Label ID="LabelConductorDepot" runat="server" Text="N/A" Font-Bold="false" Font-Size="13px"></asp:Label>
                                </p>
                                <p class="text-dark mb-0" style="font-weight: bold; font-size: 13px;">
                                    Mobile No.
                                </p>
                                <p class="text-dark mb-0" style="font-weight: bold;">
                                    <asp:Label ID="LabelConductorMobile" runat="server" Text="N/A" Font-Bold="false"
                                        Font-Size="13px"></asp:Label>
                                </p>
                            </div>
                        </div>
                    </div>
                </div>


            </div>
            <div class="col-lg-3">
                <div class="card mb-2" style="border-color: #28a745;">
                    <div class="card-body" style="padding: 4px;height: 190px;">
                        <p class="text-dark text-center mb-1" style="font-weight: bold;">
                            Conductor with lowest rating - <asp:Label ID="lblconductorlowestratingmonth" runat="server" Text="N/A"></asp:Label> <br />(<asp:Label ID="lblconductorlowestratingname" runat="server" Text="N/A"></asp:Label>)
                        </p>
                        <div class="row">
                            
                            <div class="col-lg-5 pl-0">
                                <center>
                                    <p class="text-dark mb-1" style="font-weight: bold;">
                                        <i class="fa fa-star text-success"></i><i class="fa fa-star text-success" style="margin-left: 5px; font-size: 25px; margin-right: 5px;"></i><i class="fa fa-star text-success"></i>
                                    </p>
                                    
                                    <p class="text-dark mb-1" style="font-size: 12px; text-decoration: underline;">
                                        <asp:LinkButton ID="LBInferiorConductorDetail" Visible="false" OnClick="LBInferiorConductorDetail_Click" runat="server" ForeColor="Orange"
                                            Text="Download Certificate" ></asp:LinkButton>
                                    <asp:Label ID="lblconductorlowestrating" runat="server" Visible="false" Text="Not Available"></asp:Label>

                                     </p>
                                </center>
                            </div>
                            <div class="col-lg-6 pr-0" style="border-left: 1px solid #ece6e6; height: 100%;">
                              
                                <p class="text-dark mb-0" style="font-weight: bold; font-size: 13px;">
                                    Depot
                                </p>
                                <p class="text-dark mb-0" style="font-weight: bold;">
                                    <asp:Label ID="depotname" runat="server" Text="N/A" Font-Bold="false" Font-Size="13px"></asp:Label>
                                </p>
                                <p class="text-dark mb-0" style="font-weight: bold; font-size: 13px;">
                                    Mobile No.
                                </p>
                                <p class="text-dark mb-0" style="font-weight: bold;">
                                    <asp:Label ID="lblmobile" runat="server" Text="N/A" Font-Bold="false"
                                        Font-Size="13px"></asp:Label>
                                </p>
                            </div>
                        </div>
                    </div>
                </div>


            </div>
            <div class="col-lg-3">
                <div class="card mb-2" style="border-color: #28a745;">
                    <div class="card-body" style="padding: 4px;height: 190px;">
                        <p class="text-dark text-center mb-1" style="font-weight: bold;">
                            Bus Service Of The Month - <asp:Label ID="lbltopbusmonth" runat="server" Text="N/A"></asp:Label> <br />(<asp:Label ID="lbltopbusservicename" runat="server" Text="Not Available"></asp:Label>)
                        </p>
                        <div class="row">
                            <div class="col-lg-3 p-0">
                                <div class="card-img text-center">
                                    <asp:Image ID="Image2" runat="server" Style="border: 1px solid white; border-radius: 50%;" />
                                </div>
                            </div>
                            <div class="col-lg-5 pl-0">
                                <center>
                                    <p class="text-dark mb-1" style="font-weight: bold;">
                                        <i class="fa fa-star text-success"></i><i class="fa fa-star text-success" style="margin-left: 5px; font-size: 25px; margin-right: 5px;"></i><i class="fa fa-star text-success"></i>
                                    </p>
                                    <p class="text-dark mb-1" style="font-size: 12px; text-decoration: underline;">
                                        <asp:LinkButton ID="LinkButton2" Visible="false" OnClick="LinkButton2_Click" runat="server" ForeColor="Orange"
                                            Text="Download Certificate"></asp:LinkButton>
                                     <asp:Label ID="lblbusservice" runat="server" Visible="false" Text="Not Available"></asp:Label>
                                </p>
                                        </center>
                            </div>
                            <div class="col-lg-4 pr-0" style="border-left: 1px solid #ece6e6; height: 100%;">
                                <p class="text-dark mb-0" style="font-weight: bold; font-size: 13px;">
                                    Depot
                                </p>
                                <p class="text-dark mb-0" style="font-weight: bold;">
                                    <asp:Label ID="lblbustopdepot" runat="server" Text="Not Available" Font-Bold="false" Font-Size="13px"></asp:Label>
                                </p>
                                <p class="text-dark mb-0" style="font-weight: bold; font-size: 13px;">
                                    Mobile No.
                                </p>
                                <p class="text-dark mb-0" style="font-weight: bold;">
                                    <asp:Label ID="lblbusmobile" runat="server" Text="Not Available" Font-Bold="false"
                                        Font-Size="13px"></asp:Label>
                                </p>
                            </div>
                        </div>
                    </div>
                </div>


            </div>
            <div class="col-lg-3">
                <div class="card mb-2" style="border-color: #28a745;">
                    <div class="card-body" style="padding: 4px;height: 190px;">
                        <p class="text-dark text-center mb-1" style="font-weight: bold;">
                            Bus Service with lowest rating - <asp:Label ID="lblbuslowestratingmonth" runat="server" Text="N/A"></asp:Label><br />(<asp:Label ID="lblbusservicelowestratingname" runat="server" Text="Not Available"></asp:Label>)
                        </p>
                        <div class="row">
                            <div class="col-lg-3 p-0">
                                <div class="card-img text-center">
                                    <asp:Image ID="Image3" runat="server" Style="border: 1px solid white; border-radius: 50%;" />
                                </div>
                            </div>
                            <div class="col-lg-5 pl-0">
                                <center>
                                    <p class="text-dark mb-1" style="font-weight: bold;">
                                        <i class="fa fa-star text-success"></i><i class="fa fa-star text-success" style="margin-left: 5px; font-size: 25px; margin-right: 5px;"></i><i class="fa fa-star text-success"></i>
                                    </p>
                               <p class="text-dark mb-1" style="font-size: 12px; text-decoration: underline;">
                                        <asp:LinkButton ID="LinkButton3" Visible="false" OnClick="LinkButton3_Click" runat="server" ForeColor="Orange"
                                            Text="Download Certificate"></asp:LinkButton>
                                    <asp:Label ID="lblbuslowestrating" runat="server" Visible="false" Text="Not Available"></asp:Label>
                               </p>
                                </center>
                            </div>
                            <div class="col-lg-4 pr-0" style="border-left: 1px solid #ece6e6; height: 100%;">
                                <p class="text-dark mb-0" style="font-weight: bold; font-size: 13px;">
                                    Depot
                                </p>
                                <p class="text-dark mb-0" style="font-weight: bold;">
                                    <asp:Label ID="lblbuslowestdepo" runat="server" Text="Not Available" Font-Bold="false" Font-Size="13px"></asp:Label>
                                </p>
                                <p class="text-dark mb-0" style="font-weight: bold; font-size: 13px;">
                                    Mobile No.
                                </p>
                                <p class="text-dark mb-0" style="font-weight: bold;">
                                    <asp:Label ID="lblbusmob" runat="server" Text="Not Available" Font-Bold="false"
                                        Font-Size="13px"></asp:Label>
                                </p>
                            </div>
                        </div>
                    </div>
                </div>


            </div>
        </div>

        <div class="row">
            <div class="col-lg-12">
                <div class="card">
                    <div class="card-body" style="height: 500px;">
                         <asp:Panel runat="server" ID="pnlMsg" Visible="true">
            <div class="row">
                <div class="col-12 mt-5">
                    <center>
                          <asp:Label runat="server" Text="Record Not Found" Font-Bold="true" Font-Size="40px" ForeColor="LightGray" ></asp:Label>
     
                    </center>
                </div>
            </div>
        </asp:Panel>
                          <asp:Panel runat="server" ID="pnlReport" Visible="false">
           
                        <div class="card-body table-responsive" style="min-height: 320px;">
                         <asp:GridView ID="gvRating" runat="server" CssClass="gvTable" AutoGenerateColumns="false"
                                            GridLines="None" AllowPaging="true" PageSize="15" OnRowCommand="gvRating_RowCommand" DataKeyNames="journey_date">
                                            <Columns>
                                                <asp:TemplateField HeaderText="JOURNEY DATE">
                                                    <ItemTemplate>
                                                        <asp:Label  runat="server" Text='<%# Eval("journey_date") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="No. OF TICKETS">
                                                    <ItemTemplate>
                                                        <asp:Label  runat="server" Text='<%# Eval("nooftickets") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="BUS SERVICE RATING">
                                                    <ItemTemplate>
                                                        <asp:Label  runat="server" Text='<%# Eval("bus_rating") %>'></asp:Label>
                                                         <i class="fa fa-star text-success"></i>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CONDUCTOR RATING">
                                                    <ItemTemplate>
                                                        <asp:Label  runat="server" Text='<%# Eval("conductor_rating") %>'></asp:Label>
                                                        <i class="fa fa-star text-success"></i>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PORTAL RATING">
                                                    <ItemTemplate>
                                                        <asp:Label  runat="server" Text='<%# Eval("portal_rating") %>'></asp:Label>
                                                         <i class="fa fa-star text-success"></i>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               


                                                   <asp:TemplateField HeaderText="ACTION">
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="lbtnaction" CssClass="btn btn-primary btn-sm" CommandName="View" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' ToolTip="View Details" Text="View"><i class="fa fa-eye">View</i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Right" CssClass="GridPager" />
                                        </asp:GridView>
                    </div>
                              </asp:Panel>
                </div>
            </div>
        </div>
    </div>

        </div>
     
</asp:Content>


