<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/RoleMaster.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeFile="EmployeeDash.aspx.cs" Inherits="Auth_EmployeeDash" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style>
        .modalBackground {
            background-color: black;
            opacity: 0.6;
        }

        .ModalPopupBG {
            background-color: #666699;
            filter: alpha(opacity=50);
            opacity: 0.7;
        }

        .grRowStyle {
            border-bottom: solid 1px #007bff;
        }

        .upda_link {
            color: black;
        }

            .upda_link:hover {
                color: limegreen;
            }

        .card .table td, .card .table th {
            padding-right: 1rem;
            padding-left: 1rem;
            padding: 7px;
        }
    </style>
    <style>
        .pagination-ys {
            /*display: inline-block;*/
            padding-left: 0;
            margin: 20px 0;
            border-radius: 4px;
            /*//height:auto*/
        }

            .pagination-ys table > tbody > tr > td {
                display: inline;
            }

                .pagination-ys table > tbody > tr > td > a, .pagination-ys table > tbody > tr > td > span {
                    position: relative;
                    float: left;
                    padding: 8px 12px;
                    line-height: 1.42857143;
                    text-decoration: none;
                    color: #dd4814;
                    background-color: #ffffff;
                    border: 1px solid #dddddd;
                    margin-left: -1px;
                    height:auto;
                }

                .pagination-ys table > tbody > tr > td > span {
                    position: relative;
                    float: left;
                    padding: 8px 12px;
                    line-height: 1.42857143;
                    text-decoration: none;
                    margin-left: -1px;
                    z-index: 2;
                    color: #aea79f;
                    background-color: #f5f5f5;
                    border-color: #dddddd;
                    cursor: default;
                }

                .pagination-ys table > tbody > tr > td:first-child > a, .pagination-ys table > tbody > tr > td:first-child > span {
                    margin-left: 0;
                    border-bottom-left-radius: 4px;
                    border-top-left-radius: 4px;
                }

                .pagination-ys table > tbody > tr > td:last-child > a, .pagination-ys table > tbody > tr > td:last-child > span {
                    border-bottom-right-radius: 4px;
                    border-top-right-radius: 4px;
                }

                .pagination-ys table > tbody > tr > td > a:hover, .pagination-ys table > tbody > tr > td > span:hover, .pagination-ys table > tbody > tr > td > a:focus, .pagination-ys table > tbody > tr > td > span:focus {
                    color: #97310e;
                    background-color: #eeeeee;
                    border-color: #dddddd;
                }
    </style>
   <%-- <script>
        var map;

        function initMap1() {
            HideLoading();
        }
    </script>--%>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<%
        sbLoaderNdPopup dd = new sbLoaderNdPopup();
        string ss = dd.getLoaderHtml();
        Response.Write(ss);
    %>--%>
    <div class="container-fluid" style="background-color: #FAF9F6">
        <div class="row" style="padding-top: 0px">
            <div class="col-3 mt-3">
                <div class="row">
                    <div class="col-12">
                        <div class="card pt-2 pb-2 pl-3 pr-3">
                            <div class="row">
                                <div class="col-5 pt-2 text-left">
                                    <img src="dashAssests/img/user-icon.png" width="110px" />
                                </div>
                                <div class="col-7 pt-2 text-right">
                                    <asp:Label runat="server" ForeColor="Green" ID="lblOfcLvlName" Text=""></asp:Label>-
                                    <asp:Label runat="server" ForeColor="red" ID="lblOfcName" Text=""></asp:Label><br />
                                    <%-- <asp:Label runat="server" ID="lblPostingOfc" Text=""></asp:Label><br />--%>
                                   Designation-
                                    <asp:Label runat="server" ForeColor="Green" ID="lblDesignationName" Text=""></asp:Label><br />
                                    Role-
                                    <asp:Label runat="server" ForeColor="Green" ID="lblRole" Text=""></asp:Label><br />
                                </div>
                                <br />
                                <div class="col-12 pl-0 pt-2 text-left ml-3">
                                    <i class="fa fa-user-alt"></i>
                                    <asp:Label runat="server" ID="lblEmpName" Font-Bold="true" Text=""></asp:Label><br />
                                    <i class="fa fa-envelope"></i>
                                    <asp:Label runat="server" ID="lblEmpEmail" Text=""></asp:Label>
                                    <br />
                                    <i class="fa fa-mobile "></i>
                                    <asp:Label runat="server" ID="lblEmpMobile" Text=""></asp:Label>
                                    <%--    <br />

                                        <asp:LinkButton ID="lbtnLogout" OnClick="lbtnLogout_Click" runat="server"><i class="fa fa-sign-out"></i>Logout</asp:LinkButton>
                                    --%>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 mt-2">
                        <div class="card  pt-2 pb-2 pl-3 pr-3" style="min-height: 200px" id="quicklinks" runat="server">

                            <div class="col-12">
                                <asp:Label runat="server" ID="Label2" Font-Bold="true" Text="Quick Links"></asp:Label>
                            </div>
                            <asp:Panel runat="server" ID="pnlLandingPage" Visible="false">
                                <div class="row">
                                    <div class="col-auto py-1 px-1">
                                        <asp:LinkButton runat="server" ID="lbtnViewCatalogue" CssClass="btn-sm btn-primary text-center" Text="View</br>Catalogue" Height="50px"></asp:LinkButton>
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel runat="server" ID="pnlNoLandingPage" Visible="false">
                                <div class="row">
                                    <asp:Repeater runat="server" ID="rptModules" OnItemCommand="rptModules_ItemCommand">
                                        <ItemTemplate>
                                            <div class="col-auto">
                                                <asp:LinkButton runat="server" CommandArgument='<%# Eval("moduleurl") %>' CommandName="Redirect" CssClass="btn-sm btn-primary text-center mt-1" Width="90px" Height="50px"><%# Eval("val_modulename") %></asp:LinkButton>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <asp:Panel ID="pnlNoModules" runat="server" Width="100%" Visible="true">
                                        <div class="col-md-12 p-0" style="text-align: center;">
                                            <div class="col-md-12 busListBox" style="color: #e3e3e3; padding-bottom: 50px; font-size: 18px; font-weight: bold;">
                                                No Any One Module Assign<br />
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                                <%--<div class="row">
                                    <div class="col-auto py-1 px-1">
                                        <asp:LinkButton runat="server" CssClass="btn-sm btn-primary text-center" Text="Attendance</br>Marking" Height="50px"></asp:LinkButton>
                                    </div>
                                    <div class="col-auto py-1 px-1">
                                        <asp:LinkButton runat="server" CssClass="btn-sm btn-primary text-center" Text="Order &</br>Circular" ID="btnaddNotice" Height="50px" OnClick="btnaddNotice_Click1"></asp:LinkButton>
                                    </div>
                                    <div class="col-auto py-1 px-1">
                                        <asp:LinkButton runat="server" CssClass="btn-sm btn-primary text-center" Text="Add</br>Holiday" Height="50px"> </asp:LinkButton>
                                    </div>
                                </div>--%>
                            </asp:Panel>
                        </div>
                    </div>
                    <div class="col-12 mt-2">
                        <div class="card  pt-2 pb-2 pl-3 pr-3" style="min-height: 100px">
                            <div class="col-12">
                                <asp:Label runat="server" ID="Label15" Font-Bold="true" Text="Download Forms"></asp:Label>
                            </div>
                            <hr style="margin-top: 5px; margin-bottom: 10px" />
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-12">
                                        1. Leave Application 
                                        <asp:LinkButton runat="server" Visible="true" Enabled="false" OnClick="Unnamed_Click" CssClass="mt-1"><i class="fa fa-download "></i></asp:LinkButton><br />
                                    </div>

                                    <div class="col-12">
                                        2. GPF Advance 
                                        <asp:LinkButton runat="server" Visible="true" Enabled="false" OnClick="Unnamed_Click1" CssClass="mt-1"><i class="fa fa-download "></i></asp:LinkButton><br />
                                    </div>
                                </div>






                            </div>
                        </div>
                    </div>
                    <div class="col-12 mt-0">
                        <div class="card  pt-2 pb-2 pl-3 pr-3" style="min-height: 170px">
                            <div class="col-12">
                                <asp:Label runat="server" ID="Label6" Font-Bold="true" Text="Helpdesk"></asp:Label>
                            </div>
                            <hr style="margin-top: 5px; margin-bottom: 5px" />
                            <div class="col-12 mt-2">
                                <asp:Label runat="server" ID="Label11" Text="For any help & query please feel free to contact us at -"></asp:Label>
                            </div>
                            <div class="col-12 mt-2 ml-2 mb-2">
                                <i class="fa fa-mobile mr-2"></i>
                                <asp:Label runat="server" ID="lblcontact1" Text=""></asp:Label>
                                <br />
                                <i class="fa fa-envelope mr-2"></i>
                                <asp:Label runat="server" ID="lblemail" Text=""></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-5 mt-3">
                <div class="row">
                    <div class="col-12 ">
                        <div class="card  pt-2" style="min-height: 500px">
                            <%--  <div class="card-header">
                                    <asp:Label runat="server" ID="Label6" Font-Bold="true" ForeColor="#B2BEB5" Text="Office Notices"></asp:Label>
                                </div>--%>
                            <div class="col-12">
                                <div class="nav nav-tabs">
                                    <asp:LinkButton runat="server" ID="lbtnOrder" OnClientClick="return ShowLoading()" OnClick="lbtnOrder_Click" CssClass="nav-item nav-link active" Font-Size="14px" Font-Bold="true">Order</asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="lbtnCircular" OnClientClick="return ShowLoading()" OnClick="lbtnCircular_Click" CssClass="nav-item nav-link" Font-Size="14px" Font-Bold="true">Circular</asp:LinkButton>
                                </div>
                            </div>


                            <div class="col-12">
                                <div class="row">


                                    <div class="card-body">
                                        <asp:GridView ID="gvDraftData" runat="server" GridLines="None" CssClass="w-100" AllowPaging="true"
                                            PageSize="5" AutoGenerateColumns="false" ShowHeader="false" OnRowCommand="gvDraftData_RowCommand" DataKeyNames="documt">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <div class="row pb-1 pt-1" style="border-bottom: 1px solid Black;">
                                                            <div class="col-12">
                                                                <asp:LinkButton ID="lbtnview" runat="server" Font-Size="Small" Font-Bold="true" CommandName="View" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' Text='<%# Eval("sub_ject") %>'></asp:LinkButton>
                                                                <br />

                                                                <p class="mb-0">
                                                                    <asp:Label runat="server" Font-Size="Small" Text="Uploaded By"></asp:Label>
                                                                    <asp:Label runat="server" Font-Size="Small" CssClass="text-muted " Text='<%# Eval("updateby") %>'></asp:Label>
                                                                </p>
                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--                                  <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <div class="row">
                                                                         <asp:LinkButton ID="lbtnview" runat="server" CommandName="View" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                class="btn btn-primary btn-sm" ToolTip="View Order/Circular"><i class="fa fa-eye"> View</i>
                                   
                                            </asp:LinkButton>
                                                            </div>
                                              
                                                            
                                                        
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                            </Columns>
                                            <PagerStyle CssClass="pagination-ys" />
                                        </asp:GridView>

                                    </div>
                                    <div class="col-12 text-center mt-5">
                                        <asp:Label ID="lblOrderMsg" Style="font-size: 30px;" Visible="false" Font-Bold="true" ForeColor="LightGray" runat="server" Text="No Order Found"></asp:Label>
                                        <asp:Label ID="lblCircularmsg" Style="font-size: 30px;" Visible="false" Font-Bold="true" ForeColor="LightGray" runat="server" Text="No Circular Found"></asp:Label>

                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>

                    <div class="col-12 mt-2">
                        <div class="card  pt-2 pb-2 pl-3 pr-3" style="min-height: 250px">
                            <%-- <div class="card-header">
                                    <asp:Label runat="server" ID="Label11" Font-Bold="true" ForeColor="#B2BEB5" Text="Birthday Wishes"></asp:Label>
                                </div>--%>
                            <div class="col-12">
                                <div class="nav nav-tabs">
                                    <asp:LinkButton runat="server" OnClientClick="return ShowLoading()" ID="lbtnBirthday" OnClick="lbtnBirthday_Click" CssClass="nav-item nav-link active" Font-Size="14px" Font-Bold="true">Birthday</asp:LinkButton>
                                    <asp:LinkButton runat="server" OnClientClick="return ShowLoading()" ID="lbtnAnniversary" OnClick="lbtnAnniversary_Click" CssClass="nav-item nav-link" Font-Size="14px" Font-Bold="true">Work Anniversary</asp:LinkButton>
                                    <asp:LinkButton runat="server" OnClientClick="return ShowLoading()" ID="lbtnRetirement" OnClick="lbtnRetirement_Click" CssClass="nav-item nav-link" Font-Size="14px" Font-Bold="true">Retirement</asp:LinkButton>
                                </div>
                            </div>

                            <div class="col-12 text-center">
                                <div class="row">
                                    <div class="col-sm-12 flex-column d-flex stretch-card ">
                                        <div class="card-body table table-responsive">
                                            <asp:DataList ID="DataList1" Width="100%" runat="server" RepeatDirection="Horizontal" OnItemCommand="DataList1_ItemCommand">
                                                <ItemTemplate>
                                                    <img src="dashAssests/img/user-icon.png" width="70px" /><br />
                                                    <asp:Label ID="lblbirthdayName" CssClass="text-primary " Font-Size="Small" runat="server" Text='<%#Eval("emp_name") %>'></asp:Label><br />
                                                    <asp:Label ID="lblbirthdayMobile" Visible="true" runat="server" Text='<%#Eval("mobile_number_") %>'></asp:Label><br />
                                                    <%--<asp:Label ID="lblbirthdayEmail" Visible="false" runat="server" Text='<%#Eval("emailid_") %>'></asp:Label>--%>
                                                    <asp:Label ID="Label13" Font-Size="Small" Font-Bold="true" ForeColor="Black" runat="server" Text='<%#Eval("designation_") %>'></asp:Label><br />
                                                    <%--<asp:Label ID="Label19" Visible="false" CssClass="text-primary " runat="server" Text='<%#Eval("address_") %>'></asp:Label><br />--%>
                                                    <asp:LinkButton ID="LinkButton2" OnClientClick="return ShowLoading()" CommandName="SendWishes" Font-Size="Small" CssClass="btn btn-success btn-sm" Style="margin-right: 15px; margin-left: 15px" runat="server">Send Wishes</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:DataList>
                                        </div>
                                    </div>
                                    <div class="col-12">
                                        <asp:Label runat="server" ID="lblWishes" Font-Size="25px" Style="margin-top: 0px" Font-Bold="false" ForeColor="#B2BEB5" Visible="false" Text=""></asp:Label>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-4 mt-3">
                <div class="row">
                    <div class="col-12">
                        <div class="card  pt-2 pb-3 pl-3 pr-3">
                            <div class="row">
                                <div class="col-12">

                                    <asp:Label runat="server" ID="Label3" Font-Bold="true" Text="Search Employee"></asp:Label>
                                    <hr style="margin-top: 5px; margin-bottom: 5px" />
                                    <%-- <div class="card-header">
                                               </div>--%>
                                </div>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-7">
                                            <asp:Label ID="Label18" runat="server" Visible="true" Text="Enter Details" Style="color: #a8a8a8; font-size: 14px;"></asp:Label>
                                        </div>
                                        <div class="col-5">
                                            <asp:Label ID="lblOffice" runat="server" Visible="false" Text="Office level" Style="color: #a8a8a8; font-size: 14px;"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-9">
                                            <asp:TextBox ID="txtsearch" placeholder="Name/Email/Mobile/Emp Code" autocomplete="off" MaxLength="50" runat="server" CssClass="form-control "></asp:TextBox>
                                        </div>
                                        <div class="col-3 input-group-prepend ">
                                            <asp:DropDownList ID="ddlOffce" CssClass="form-control" Visible="false" runat="server" AutoPostBack="true"></asp:DropDownList>
                                            <asp:LinkButton OnClientClick="return ShowLoading()" ID="lbtnSearchemp" CssClass="btn btn-sm btn-warning ml-1" runat="server" OnClick="lbtnSearchemp_Click"><i class="fa fa-search mt-2"></i></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 mt-2">
                        <div class="card  pt-2 pb-3 pl-3 pr-3">
                            <div class="row">
                                <div class="col-12">
                                    <div class="input-group-prepend ">
                                        <asp:Label runat="server" ID="Label5" Font-Bold="true" Text="Holiday Calender"></asp:Label>
                                        <asp:LinkButton runat="server" ID="lbtninfo" OnClick="lbtninfo_Click"><i class="fa fa-info-circle ml-2"></i></asp:LinkButton>
                                    </div>
                                    <hr style="margin-top: 5px; margin-bottom: 10px" />
                                </div>

                                <div class="col-12">

                                    <asp:Calendar ID="Calendar1" runat="server" BorderColor="#c5d1d1" BorderWidth="2px"
                                        DayNameFormat="Short" Width="100%" OnDayRender="Calendar1_DayRender"
                                        Font-Names="Verdana" Font-Size="10pt" ForeColor="#663399" OnSelectionChanged="Calendar1_SelectionChanged"
                                        ShowGridLines="True">
                                        <SelectedDayStyle BackColor="#1abc9c" Font-Bold="True" ForeColor="White" />
                                        <SelectorStyle BackColor="#0df2f2" Height="20px" />
                                        <TodayDayStyle BackColor="#007bff" ForeColor="White" />
                                        <OtherMonthDayStyle ForeColor="#CC9966" />
                                        <NextPrevStyle Font-Size="9pt" ForeColor="#007bff" />
                                        <DayHeaderStyle BackColor="#e3e5e5" Font-Bold="True" Height="10px" />
                                        <TitleStyle BackColor="#ffffff" Font-Bold="True" Font-Size="14pt" ForeColor="Black" />
                                    </asp:Calendar>





                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 mt-2">
                        <div class="card  pt-2 pb-2 pl-3 pr-3" style="min-height: 150px">
                            <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server" ChildrenAsTriggers="true" UpdateMode="Always">
                                    <ContentTemplate>--%>
                            <div class="row">
                                <div class="col-4">
                                    <div class="input-group-prepend ">
                                        <asp:Label runat="server" ID="Label1" Font-Bold="true" Text="Leave Register"></asp:Label>
                                    </div>
                                    <%--<hr style="margin-top: 5px;margin-bottom:10px" />--%>
                                </div>
                                <div class="col-8 input-group-prepend">
                                    <asp:DropDownList ID="ddlLeaveRegisterMonth" OnClientClick="return ShowLoading()" AutoPostBack="true" OnSelectedIndexChanged="ddlLeaveRegisterMonth_SelectedIndexChanged" runat="server" CssClass="form-control"></asp:DropDownList>
                                    <asp:DropDownList ID="ddlLeaveRegisterYear" OnClientClick="return ShowLoading()" AutoPostBack="true" OnSelectedIndexChanged="ddlLeaveRegisterYear_SelectedIndexChanged" runat="server" CssClass="form-control ml-1"></asp:DropDownList>

                                </div>
                                <div class="col-12">
                                    <hr style="margin-top: 5px; margin-bottom: 10px" />
                                </div>
                                <div class="col-12 mt-2">


                                    <asp:GridView ID="grvleaveregister" runat="server" AllowPaging="true" PageSize="5" CssClass="table table-hover table-striped"
                                        AutoGenerateColumns="False"
                                        ForeColor="#333333" Font-Size="14px"
                                        GridLines="None" ShowHeader="true" Font-Bold="false" OnPageIndexChanging="grvleaveregister_PageIndexChanging"
                                        Width="100%">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No." ItemStyle-CssClass="class-on-element">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="From date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAGENTNAME1" runat="server" Text='<%# Eval("from_date") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="To date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblADDRESS1" runat="server" Text='<%# Eval("to_date") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Leave Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblADDRESS1" runat="server" Text='<%# Eval("leavetypename_") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle CssClass="pagination-ys" />
                                    </asp:GridView>
                                    <div class="text-center mt-2">
                                        <asp:Label runat="server" ID="lblLeaveRegisterMsg" Font-Size="20px" Font-Bold="true" ForeColor="#B2BEB5" Visible="false" CssClass="text-center" Text="Record Not Found"></asp:Label>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 mt-2" runat="server" id="dvcurrentduty">
                        <div class="card  pt-2 pb-2 pl-3 pr-3" style="min-height: 150px">
                            <div class="row">
                                <div class="col-12">
                                    <div class="input-group-prepend ">
                                        <asp:Label runat="server" ID="Label8" Font-Bold="true" Visible="true" Text="Current Duty/Pending For Completion"></asp:Label>
                                        <%-- <asp:LinkButton runat="server" ID="lbtnShiftDutyArchive" Font-Size="Small" CssClass="btn-sm btn-primary  float-right">Archive <i class="fa fa-share "></i></asp:LinkButton>--%>
                                    </div>

                                </div>

                                <div class="col-12">
                                    <hr style="margin-top: 5px; margin-bottom: 10px" />
                                </div>
                                <div class="col-12 mt-2">

                                    <asp:GridView ID="gvCurrentDuty" runat="server" OnRowCommand="gvCurrentDuty_RowCommand" OnPageIndexChanging="GVDutyRegister_PageIndexChanging"
                                        OnRowDataBound="GVDutyRegister_RowDataBound"
                                        AllowPaging="true" PageSize="5" CssClass="table table-hover table-striped" AutoGenerateColumns="False"
                                        ForeColor="#333333" Font-Size="14px" DataKeyNames="waybillrefno_,office_id"
                                        GridLines="None" ShowHeader="true" Font-Bold="false" Width="100%">

                                        <Columns>
                                            <asp:TemplateField HeaderText="#" ItemStyle-CssClass="class-on-element">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Duty No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblwaybillno" runat="server" Text='<%# Eval("waybillrefno_") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Duty Date" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldutystartdate" runat="server" Text='<%# Eval("dutystartdate_") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Status" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldutyenddate" runat="server" Text='<%# Eval("dutystatusname") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lbtCnview" OnClientClick="return ShowLoading()" CommandName="viewCDuty" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CssClass="btn btn-primary btn-sm" ToolTip="View Duty Slip"> <i class="fa fa-eye"></i></asp:LinkButton>
                                                    <asp:LinkButton runat="server" ID="lbtnExcessAmt" OnClientClick="return ShowLoading()" CommandName="ExcessAmt" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CssClass="btn btn-success btn-sm" ToolTip="Excess Amount Wavier In Waybill"> <i class="fa fa-rupee-sign"></i></asp:LinkButton>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle CssClass="pagination-ys" />
                                    </asp:GridView>
                                    <div class="text-center mt-2">
                                        <asp:Label runat="server" ID="lblcurrentdutymsg" Font-Size="20px" Font-Bold="true" ForeColor="#B2BEB5" Visible="false" CssClass="text-center" Text="Record Not Found"></asp:Label>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 mt-2" runat="server" id="dvDutyRegister">
                        <div class="card  pt-2 pb-2 pl-3 pr-3" style="min-height: 150px">
                            <div class="row">
                                <div class="col-4">
                                    <div class="input-group-prepend ">
                                        <asp:Label runat="server" ID="lblDutyShiftHeader" Font-Bold="true" Visible="true" Text="Duty Register"></asp:Label>
                                        <%-- <asp:LinkButton runat="server" ID="lbtnShiftDutyArchive" Font-Size="Small" CssClass="btn-sm btn-primary  float-right">Archive <i class="fa fa-share "></i></asp:LinkButton>--%>
                                    </div>

                                </div>
                                <div class="col-8">
                                    <div class="input-group-prepend ">
                                        <asp:DropDownList ID="ddldutyregistermonth"  AutoPostBack="true" OnSelectedIndexChanged="ddldutyregistermonth_SelectedIndexChanged" runat="server" CssClass="form-control"></asp:DropDownList>
                                        <asp:DropDownList ID="ddldutyregisteryear" AutoPostBack="true" OnSelectedIndexChanged="ddldutyregisteryear_SelectedIndexChanged" runat="server" CssClass="form-control ml-1"></asp:DropDownList>

                                    </div>
                                </div>
                                <div class="col-12">
                                    <hr style="margin-top: 5px; margin-bottom: 10px" />
                                </div>
                                <div class="col-12 mt-2">

                                    <asp:GridView ID="GVDutyRegister" runat="server" OnRowCommand="GVDutyRegister_RowCommand" OnPageIndexChanging="GVDutyRegister_PageIndexChanging"
                                        OnRowDataBound="GVDutyRegister_RowDataBound"
                                        AllowPaging="true" PageSize="5" CssClass="table table-hover table-striped" AutoGenerateColumns="False"
                                        ForeColor="#333333" Font-Size="14px" DataKeyNames="waybillrefno_,office_id"
                                        GridLines="None" ShowHeader="true" Font-Bold="false" Width="100%">

                                        <Columns>
                                            <asp:TemplateField HeaderText="#" ItemStyle-CssClass="class-on-element">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Duty No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblwaybillno" runat="server" Text='<%# Eval("waybillrefno_") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Duty Date" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldutystartdate" runat="server" Text='<%# Eval("dutystartdate_") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Duty End Date" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldutyenddate" runat="server" Text='<%# Eval("dutyenddate_") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" OnClientClick="return ShowLoading()" ID="lbtnview" CommandName="viewDuty" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CssClass="btn btn-primary btn-sm" ToolTip="View Duty Slip"> <i class="fa fa-eye"></i></asp:LinkButton>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle CssClass="pagination-ys" />
                                    </asp:GridView>
                                    <div class="text-center mt-2">
                                        <asp:Label runat="server" ID="lblDutyShiftRegistermsg" Font-Size="20px" Font-Bold="true" ForeColor="#B2BEB5" Visible="false" CssClass="text-center" Text="Record Not Found"></asp:Label>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- Duty Details -->
        <div class="row">
            <cc1:ModalPopupExtender ID="mpShowDuty" runat="server" PopupControlID="Panel1" TargetControlID="Button1"
                CancelControlID="LinkButton2" BackgroundCssClass="ModalPopupBG">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="Panel1" runat="server" Style="position: fixed;">
                <div class="modal-content mt-5" style="width: 85vw;">
                    <div class="card w-100">
                        <div class="card-header py-3">
                            <div class="row">
                                <div class="col">
                                    <h3 class="m-0">
                                        <asp:Label runat="server" ID="lblMpHeader"></asp:Label></h3>
                                </div>
                                <div class="col-auto">
                                    <asp:LinkButton ID="LinkButton2" runat="server" Style="color: Red; cursor: pointer;"><i class="fa fa-times fa-2x"></i></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="card-body p-2">

                            <asp:Literal ID="eSlip" runat="server"></asp:Literal>
                        </div>
                    </div>
                </div>
                <br />
                <div style="visibility: hidden;">
                    <asp:Button ID="Button1" runat="server" Text="" />
                    <asp:Button ID="Button2" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>


        <div class="row">
            <cc1:ModalPopupExtender ID="mpdocment" runat="server" PopupControlID="pnlviewdocment" CancelControlID="btnclose"
                TargetControlID="Button5" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlviewdocment" runat="server" Style="display: none;">
                <div class="card" style="margin-top: 100px;">
                    <div class="card-header">

                        <div class="row">
                            <div class="col-lg-6">
                                <h4 class="card-title text-left mb-0">View Document
                                </h4>
                            </div>
                            <div class="col-lg-6 text-right">
                                <asp:LinkButton ID="btnclose" runat="server" CssClass="btn btn-danger"> <i class="fa fa-times"></i></asp:LinkButton>

                            </div>
                        </div>

                    </div>
                    <div class="card-body text-left pt-2" style="min-height: 100px;">
                        <embed id="tkt" runat="server" src="" style="height: 85vh; width: 45vw" />
                        <%-- <embed src="../PassUTC/ViewDocument.aspx" style="height: 85vh; width: 80vw" />--%>
                        <div style="visibility: hidden;">
                            <asp:Button ID="Button5" runat="server" Text="" />
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>


        <!-- Error Popup  -->
        <div class="row">
            <cc1:ModalPopupExtender ID="ModalPopupError" runat="server" PopupControlID="PanelModalError"
                TargetControlID="ButtonOpenModalError" CancelControlID="LinkButtonCloseModalError"
                BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="PanelModalError" runat="server" Style="position: fixed;">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <div class="col-md-10">
                                <h4 class="m-0">
                                    <asp:Label ID="LabelModalErrorHeader" runat="server" ForeColor="Black"></asp:Label>
                                </h4>
                            </div>
                            <div class="col-md-2 text-right">
                                <asp:LinkButton ID="LinkButtonCloseModalError" runat="server" UseSubmitBehavior="false"
                                    Style="color: Red; cursor: pointer;"><i class="fa fa-times fa-2x"></i></asp:LinkButton>
                            </div>
                        </div>
                        <div class="modal-body">
                            <p class="full-width-separator text-center" style="font-size: 17px;">
                                <asp:Label ID="LabelModalErrorMessage" runat="server"></asp:Label>
                            </p>
                        </div>
                    </div>
                </div>
                <br />
                <div style="visibility: hidden;">
                    <asp:Button ID="ButtonOpenModalError" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>

        <!--Employee Search Deatils-->
        <div class="row">
            <cc1:ModalPopupExtender ID="mpEmployeeSearch" runat="server" PopupControlID="pnlEmp" TargetControlID="Button11111"
                CancelControlID="LinkButton12233" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlEmp" runat="server" Style="position: fixed;">
                <div class="modal-content mt-5">
                    <div class="modal-header">
                        <div class="col-md-10">
                            <h5 class="m-0">Employee Details</h5>
                        </div>
                        <div class="col-md-2 text-right">
                            <asp:LinkButton ID="LinkButton12233" runat="server" UseSubmitBehavior="false" Style="color: Red; cursor: pointer;"><i class="fa fa-times fa-2x"></i></asp:LinkButton>
                        </div>
                    </div>
                    <div class="modal-body p-0">
                        <div class="card" style="font-size: 12px; min-height: 450px; min-width: 1100px">
                            <div class="row mt-0">
                                <div class="col-sm-12 flex-column d-flex stretch-card ">
                                    <div class="card-body table table-responsive">
                                        <asp:GridView ID="gvEmployeeSearchData" runat="server" GridLines="None" CssClass="w-100" AllowPaging="true"
                                            PageSize="5" AutoGenerateColumns="false" ShowHeader="false" OnPageIndexChanging="gvEmployeeSearchData_PageIndexChanging">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <div class="row pb-0">
                                                            <div class="col">
                                                                <div class="row">
                                                                    <div class="col">
                                                                        <p class="mb-0">
                                                                            <asp:Label ID="Label14" runat="server" Font-Bold="true" ForeColor="Green" Text='<%#Eval("e_fname") %> '></asp:Label>
                                                                            <asp:Label ID="Label23" runat="server" Font-Bold="true" ForeColor="Green" Text='<%#Eval("e_mname") %> '></asp:Label>
                                                                            <asp:Label ID="Label24" runat="server" Font-Bold="true" ForeColor="Green" Text='<%#Eval("e_lname") %> '></asp:Label>
                                                                            (<asp:Label ID="Label20" runat="server" ForeColor="Red" Font-Bold="true" Text=' <%#Eval("e_code") %>'></asp:Label>)                                               
                                                                           <asp:Label ID="Label3" Font-Bold="true" Visible='<%# Eval("e_gender").ToString() == "M" ? true : false %>' runat="server" Text="Male"></asp:Label>

                                                                        </p>
                                                                        <p class="mb-0">
                                                                            <asp:Label ID="Label17" runat="server" Font-Bold="false" Text="Designation-"></asp:Label>

                                                                            (<asp:Label ID="Label46" runat="server" ForeColor="red" Font-Bold="true" Text='<%#Eval("e_designation_name") %>'></asp:Label>)
                                                                        </p>
                                                                    </div>
                                                                    <div class="col-auto">
                                                                        <p class="mb-0 font-weight-bold">
                                                                        </p>

                                                                        <p class="mb-0">
                                                                            <asp:Label ID="Label16" runat="server" Font-Bold="false" Text="DOJ-"><i class="fa fa-mobile "></i></asp:Label>
                                                                            <asp:Label ID="Label15" CssClass="" Font-Bold="false" runat="server" Text=' <%#Eval("e_mobile_number") %> '></asp:Label>,
                                                                         <asp:Label ID="Label47" runat="server" Font-Bold="true" Text="DOJ-"><i class="fa fa-envelope "></i></asp:Label>
                                                                            <asp:Label ID="Label48" CssClass="" Font-Bold="false" runat="server" Text=' <%#Eval("e_email_id") %> '></asp:Label><br />


                                                                        </p>
                                                                        <p class="mb-0">
                                                                            <asp:Label ID="Label29" runat="server" Font-Bold="false" Text="Address-"></asp:Label>
                                                                            <%#Eval("e_address") %>
                                                                        </p>
                                                                    </div>
                                                                    <div class="col text-right">
                                                                        <p class="mb-0">
                                                                        </p>
                                                                        <p class="mb-0">


                                                                            <asp:Label ID="Label26" runat="server" Font-Bold="false" Text="Office -"></asp:Label>
                                                                            <asp:Label ID="Label30" runat="server" CssClass="text-primary" Font-Bold="true" Text='<%#Eval("e_office_name") %>'></asp:Label>
                                                                            <br />

                                                                            <asp:Label ID="Label39" runat="server" Font-Bold="false" Text="Current Status -"></asp:Label>
                                                                            <asp:Label ID="Label40" runat="server" ForeColor="Green" Font-Bold="true" Text=' <%#Eval("status") %>'></asp:Label>
                                                                        </p>
                                                                    </div>
                                                                </div>

                                                            </div>


                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle CssClass="pagination-ys" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
                <br />
                <div style="visibility: hidden;">
                    <asp:Button ID="Button11111" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>

        <!--Birthday Wish-->
        <div class="row">
            <cc1:ModalPopupExtender ID="mpBirthday" runat="server" PopupControlID="pnlBirthday" TargetControlID="Button11fzdbg111"
                CancelControlID="LinkButton6DSAFC" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlBirthday" runat="server" Style="position: fixed;">
                <div class="modal-content mt-1">
                    <div class="modal-header">
                        <div class="col-md-10">
                            <h5 class="m-0">
                                <asp:Label runat="server" ID="lblwishingtype"></asp:Label>
                            </h5>
                        </div>
                        <div class="col-md-2 text-right">
                            <asp:LinkButton ID="LinkButton6DSAFC" runat="server" UseSubmitBehavior="false" Style="color: Red; cursor: pointer;"><i class="fa fa-times fa-2x"></i></asp:LinkButton>
                        </div>
                    </div>
                    <div class="modal-body p-0">
                        <div class="card" style="font-size: 12px; min-height: 50px; min-width: 600px">
                            <div class="row p-3">
                                <div class="col-12">
                                    <asp:TextBox runat="server" placeholder="Enter Text...." ID="txtWishes" CssClass="form-control" TextMode="MultiLine" Height="110px"></asp:TextBox>
                                </div>
                                <div class="col-12 mt-2">
                                    <asp:LinkButton runat="server" ID="lbtnsendWishes" OnClientClick="return ShowLoading()" OnClick="lbtnsendWishes_Click" CssClass="btn btn-primary float-right ">Send Wishes</asp:LinkButton>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
                <br />
                <div style="visibility: hidden;">
                    <asp:Button ID="Button11fzdbg111" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>

        <div class="row">
            <cc1:ModalPopupExtender ID="mpInfo" runat="server" PopupControlID="PanelInfo" CancelControlID="LinkButtonMpInfoClose"
                TargetControlID="lbtninfo" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="PanelInfo" runat="server" Style="position: fixed;">
                <div class="card" style="width: 350px;">
                    <div class="card-header">
                        <h4 class="card-title text-left mb-0">About Holidays
                        </h4>
                    </div>
                    <div class="card-body text-left pt-2" style="min-height: 100px;">
                        <div class="row">
                            <div class="col-12 input-group-prepend ">
                                <div style="width: 15px; height: 15px; background-color: Red; margin-right: 10px">
                                </div>
                                Weekend
                            </div>
                            <div class="col-12 input-group-prepend ">
                                <div style="width: 15px; height: 15px; background-color: Green; margin-right: 10px">
                                </div>
                                Gazetted Holiday
                            </div>
                            <div class="col-12 input-group-prepend ">
                                <div style="width: 15px; height: 15px; background-color: DarkOrange; margin-right: 10px">
                                </div>
                                Restricted Holiday
                            </div>
                            <div class="col-12 input-group-prepend ">
                                <div style="width: 15px; height: 15px; background-color: #7cfc00; margin-right: 10px">
                                </div>
                                National Holiday
                            </div>
                        </div>

                        <div style="width: 100%; margin-top: 20px; text-align: right;">
                            <asp:LinkButton ID="LinkButtonMpInfoClose" runat="server" CssClass="btn btn-success btn-sm ml-2"> <i class="fa fa-check"></i> OK </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>

        <!--Duty Slip Deatils-->
        <div class="row">
            <cc1:ModalPopupExtender ID="mpDutySlip" runat="server" PopupControlID="pnlDuty" TargetControlID="Buttfzdbgsfon1"
                CancelControlID="sfveswfgegv" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlDuty" runat="server" Style="position: fixed;">
                <div class="modal-content mt-1">
                    <div class="modal-header">
                        <div class="col-md-10">
                            <h5 class="m-0">
                                <asp:Label runat="server" ID="Label4" Text="Duty Register"></asp:Label>
                            </h5>
                        </div>
                        <div class="col-md-2 text-right">
                            <asp:LinkButton ID="sfveswfgegv" runat="server" UseSubmitBehavior="false" Style="color: Red; cursor: pointer;"><i class="fa fa-times fa-2x"></i></asp:LinkButton>
                        </div>
                    </div>
                    <div class="modal-body p-0">
                        <div class="card" style="font-size: 15px; min-height: 50px; min-width: 800px">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-6">
                                        <asp:Label runat="server" Text="Refrence No."></asp:Label><asp:Label ID="lbldutyrefno" Font-Bold="true" CssClass="ml-2" ForeColor="red" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div class="col-6">
                                        <asp:Label runat="server" Text="Days"></asp:Label><asp:Label ID="lbldutyDate" Font-Bold="true" CssClass="ml-2" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                                <div class="row mt-2">
                                    <div class="col-6">
                                        <asp:Label runat="server" Text="Start Date"></asp:Label><asp:Label ID="lblDutystrtdate" Font-Bold="true" CssClass="ml-2" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div class="col-6">
                                        <asp:Label runat="server" Text="End Date"></asp:Label><asp:Label ID="lbldutyenddate" Font-Bold="true" CssClass="ml-2" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                                <div class="row mt-2">
                                    <div class="col-12">
                                        <asp:Label runat="server" Text="Service "></asp:Label><asp:Label ID="lblServiceName" ForeColor="green" CssClass="ml-2" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                                <hr />
                                <div class="row mt-2">
                                    <div class="col-6">
                                        <asp:Label runat="server" Text="Current Service"></asp:Label><asp:Label ID="lblCurrentService" CssClass="ml-2" ForeColor="green" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div class="col-6">
                                        <asp:Label runat="server" Text="Duty Time"></asp:Label><asp:Label ID="lblCurrentDutyTime" CssClass="ml-2" ForeColor="green" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div style="visibility: hidden;">
                    <asp:Button ID="Buttfzdbgsfon1" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>

        <%-- Holiday Info--%>

        <div class="row">
            <cc1:ModalPopupExtender ID="mpHolidayInfo" runat="server" PopupControlID="pnlhh" TargetControlID="bbbbju677"
                CancelControlID="lbtCAncell" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlhh" runat="server" Style="position: fixed;">
                <div class="modal-content mt-1">
                    <div class="modal-header">
                        <div class="col-md-10">
                            <h4 class="m-0">
                                <asp:Label runat="server" ID="Label7" Text="About Holiday"></asp:Label>
                            </h4>
                        </div>
                        <div class="col-md-2 text-right">
                            <asp:LinkButton ID="lbtCAncell" runat="server" UseSubmitBehavior="false" Style="color: Red; cursor: pointer;"><i class="fa fa-times fa-2x"></i></asp:LinkButton>
                        </div>
                    </div>
                    <div class="modal-body p-0">
                        <div class="card" style="font-size: 15px; min-height: 200px; min-width: 20vw">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-12 input-group-prepend ">
                                        <asp:Label runat="server" Text="Date" CssClass="text-muted"></asp:Label>
                                        <asp:Label runat="server" ID="lblHolidayDate" CssClass="ml-2"></asp:Label>
                                    </div>
                                    <div class="col-12 input-group-prepend ">
                                        <asp:Label runat="server" Text="Occassion" CssClass="text-muted"></asp:Label>
                                        <asp:Label runat="server" ID="lblOccassion" CssClass="ml-2"></asp:Label>
                                    </div>
                                    <div class="col-12 input-group-prepend ">
                                        <asp:Label runat="server" Text="Description" CssClass="text-muted"></asp:Label>
                                        <asp:Label runat="server" ID="lblDescription" CssClass="ml-2"></asp:Label>
                                    </div>
                                    <div class="col-12 input-group-prepend ">
                                        <asp:Label runat="server" Text="Type" CssClass="text-muted"></asp:Label>
                                        <asp:Label runat="server" ID="lblholidaytype" CssClass="ml-2"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div style="visibility: hidden;">
                    <asp:Button ID="bbbbju677" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>

        <%-- Holiday Info--%>
    </div>
</asp:Content>


