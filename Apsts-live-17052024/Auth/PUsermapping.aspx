<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/PortalAdmmaster.master" AutoEventWireup="true" CodeFile="PUsermapping.aspx.cs" Inherits="Auth_PUsermapping" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .modalBackground {
            background-color: black;
            opacity: 0.6;
        }

        .table td, .table th {
            padding: 3px 5px;
            vertical-align: top;
            border-top: 1px solid #dee2e6;
        }
    </style>
    <link rel="stylesheet" href="../RouteandFare/fleetAssests/assets/bootstrapselect/css/bootstrap-select.min.css" />
    <script src="../RouteandFare/fleetAssests/assets/bootstrapselect/js/bootstrap-select.min.js"
        type="text/javascript"></script>
    <script type="text/javascript">
        $(window).on('load', function () {
            HideLoading();
        });
        function ShowLoading() {
            var div = document.getElementById("loader");
            div.style.display = "block";
        }
        function HideLoading() {
            var div = document.getElementById("loader");
            div.style.display = "none";
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   
    <div class="container-fluid mt-3">
        <asp:HiddenField ID="hidtoken" runat="server" />
        <div class="row">
            <div class="col-lg-12">
                <div class="card" style="min-height: 400px">
                    <div class="card-header" style="padding: 5px;">
                        <div class="row">
                            <div class="col-lg-9"></div>
                            <div class="col-lg-1 text-right">
                                <span class="text-xs">Employee/Userid </span>
                                <asp:DropDownList ID="ddlemployee" runat="server" Style="" Visible="false" AutoPostBack="true"
                                    class="form-control">
                                </asp:DropDownList>
                                <asp:TextBox CssClass="form-control form-control-sm" runat="server" ID="txtempid" autocomplete="off" Text=""
                                    Style="font-size: 10pt; height: 30px; display: inline; text-transform: uppercase"></asp:TextBox>
                              
                            </div>
                            <div class="col-lg-2">
                                  <asp:LinkButton ID="btnsearch" OnClick="btnsearch_Click" runat="server" ToolTip="Click here to Search HQ Employee" OnClientClick="return ShowLoading()" CssClass="btn btn-sm btn-success mt-4" > <i class="fa fa-search" title="Click here to Search HQ Employee"></i>  </asp:LinkButton>
                                <asp:LinkButton ID="btnreset" OnClick="btnreset_Click" runat="server" ToolTip="Click here to Reset HQ Employee Details" OnClientClick="return ShowLoading()" CssClass="btn btn-sm btn-danger mt-4"> <i class="fa fa-undo" title="Click here to Reset HQ Employee Details"></i>  </asp:LinkButton>
                                <asp:LinkButton ID="LinkButtonInfo" runat="server" class="btn btn-sm btn-warning mt-4">
                                         <i class="fa fa-info" ></i> </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <asp:Panel ID="pnldetails" runat="server" Visible="false">
                        <div class="card-body" style="padding: 5px 20px;">

                            <div class="row mt-1">
                                <div class="col-lg-2 text-right pr-0">
                                    Employee Name
                                </div>
                                <div class="col-lg-2 pl-2">
                                    <asp:Label ID="lblempname" runat="server" Text="NA" Style="font-weight: bold;"></asp:Label>
                                </div>
                                <div class="col-lg-2 text-right pr-0">
                                    Office
                                </div>
                                <div class="col-lg-2 pl-2">
                                    <asp:Label ID="lblempoffice" runat="server" Text="NA" Style="font-weight: bold;"></asp:Label>
                                </div>
                                <div class="col-lg-2 text-right pr-0">
                                    Mobile Number
                                </div>
                                <div class="col-lg-2 pl-2">
                                    <asp:Label ID="lblempmobile" runat="server" Text="NA" Style="font-weight: bold;"></asp:Label>
                                </div>
                            </div>
                            <div class="row mt-1">
                                <div class="col-lg-2 text-right pr-0">
                                    Designation
                                </div>
                                <div class="col-lg-2 pl-2">
                                    <asp:Label ID="lblempdesignation" runat="server" Text="NA" Style="font-weight: bold;"></asp:Label>
                                </div>
                                <div class="col-lg-2 text-right pr-0">
                                    Email Id
                                </div>
                                <div class="col-lg-2 pl-2">
                                    <asp:Label ID="lblempemailid" runat="server" Text="NA" Style="font-weight: bold;"></asp:Label>
                                </div>
                                <div class="col-lg-2 text-right pr-0">
                                    Emergency Number
                                </div>
                                <div class="col-lg-2 pl-2">
                                    <asp:Label ID="lblempemrgencyno" runat="server" Text="NA" Style="font-weight: bold;"></asp:Label>
                                </div>
                            </div>
                            <hr />
                            <div class="row mt-3">
                                <div class="col-lg-6" style="border-right: 1px solid">
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <span style="font-size: 15pt; font-weight: bold; color: gray">Available for Assignment</span>
                                        </div>
                                        <div class="col-lg-4 offset-2 pl-1">
                                            <asp:DropDownList ID="ddlgroup" OnSelectedIndexChanged="ddlgroup_SelectedIndexChanged" runat="server" OnClientClick="return ShowLoading()" AutoPostBack="true" Style="font-size: 10pt; padding: 2px 5px; height: 30px"
                                                class="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <hr />
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <asp:GridView ID="grdavailableforassign" OnRowCommand="grdavailableforassign_RowCommand" runat="server" AutoGenerateColumns="False"
                                                GridLines="None" Font-Size="10pt" Visible="true" CssClass="table table-striped"
                                                DataKeyNames="module_id,module_name,module_url,about_module,group_id">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.N.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Module Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMODULENAME" runat="server" Text='<%# Eval("module_name") %>'></asp:Label>
                                                            (<asp:Label ID="lblMODULEURL" runat="server" Text='<%# Eval("module_url") %>'></asp:Label>)
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnassign" runat="server" OnClientClick="return ShowLoading()" CssClass="btn btn-success btn-sm" CommandName="Assign"
                                                                CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' ToolTip="Click here to Assign module"> <i class="fa fa-forward" title="Click here to Assign module"></i> Assign</asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:Panel ID="pnlavailableforassignNoRecord" runat="server" Width="100%" Visible="false">
                                                <center>
                                                    <i class="fa fa-window-close" style="font-size: 120px; color: #e4e4e6;"></i>
                                                    <p style="color: #e4e4e6; font-size: 25px; font-weight: 500;">
                                                        Module not available for assignment
                                                    </p>
                                                </center>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <span style="font-size: 15pt; font-weight: bold; color: gray">Assigned Module</span>
                                        </div>
                                    </div>
                                    <hr />
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <asp:GridView ID="grdassignedmodule"  OnRowCommand="grdassignedmodule_RowCommand" runat="server" AutoGenerateColumns="False" GridLines="None"
                                                Font-Size="10pt" Visible="true" CssClass="table table-striped" DataKeyNames="group_id,module_id,group_name,module_name,module_url">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.N.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Group Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGROUPNAME" runat="server" Text='<%# Eval("group_name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Module Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMODULENAME" runat="server" Text='<%# Eval("module_name") %>'></asp:Label><br />
                                                            (<asp:Label ID="lblMODULEURL" runat="server" Text='<%# Eval("module_url") %>'></asp:Label>)
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnassign" runat="server" OnClientClick="return ShowLoading()" CssClass="btn btn-warning btn-sm" CommandName="Remove"
                                                                CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' ToolTip="Click here to Remove Assigned Module"> <i class="fa fa-backward" title ="Click here to Remove Assigned Module"></i> Remove</asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:Panel ID="pnlassignedmoduleNoRecord" runat="server" Width="100%" Visible="false">
                                                <center>
                                                    <i class="fa fa-window-close" style="font-size: 120px; color: #e4e4e6;"></i>
                                                    <p style="color: #e4e4e6; font-size: 25px; font-weight: 500;">
                                                        Module not assign
                                                    </p>
                                                </center>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnldetailsnodata" CssClass="p-5" runat="server" Width="100%" Visible="true">
                        <center>
                            <p style="color: #e4e4e6; font-size: 24pt; font-weight: 700;">
                                You can assign module to only Head Quarter level employees.<br />
                                Please search employee with its userid to assign module
                            </p>
                        </center>
                    </asp:Panel>
                </div>
            </div>
        </div>
        <div class="row">
            <cc1:ModalPopupExtender ID="mpConfirmation" runat="server" PopupControlID="pnlConfirmation"
                CancelControlID="lbtnNoConfirmation" TargetControlID="Button4" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlConfirmation" runat="server" Style="position: fixed;">
                <div class="card" style="width: 350px;">
                    <div class="card-header">
                        <h4 class="card-title text-left mb-0">Please Confirm
                        </h4>
                    </div>
                    <div class="card-body text-left pt-2" style="min-height: 100px;">
                        <asp:Label ID="lblConfirmation" runat="server" Text="Do you want to save ?"></asp:Label>
                        <div style="width: 100%; margin-top: 20px; text-align: right;">
                            <asp:LinkButton ID="lbtnYesConfirmation" OnClick="lbtnYesConfirmation_Click" runat="server" CssClass="btn btn-success btn-sm"> <i class="fa fa-check"></i> Yes </asp:LinkButton>
                            <asp:LinkButton ID="lbtnNoConfirmation" OnClick="lbtnNoConfirmation_Click" runat="server" CssClass="btn btn-danger btn-sm ml-2"> <i class="fa fa-times"></i> No </asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div style="visibility: hidden;">
                    <asp:Button ID="Button4" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>
        <div class="row">
            <cc1:ModalPopupExtender ID="mpsuccess" runat="server" PopupControlID="pnlsuccess"
                CancelControlID="lbtnsuccessclose1" TargetControlID="Button6" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlsuccess" runat="server" Style="position: fixed;">
                <div class="card" style="width: 350px;">
                    <div class="card-header">
                        <h4 class="card-title text-left mb-0">Confirm
                        </h4>
                    </div>
                    <div class="card-body text-left pt-2" style="min-height: 100px;">
                        <asp:Label ID="lblsuccessmsg" runat="server" Text="Do you want to Update ?"></asp:Label>
                        <div style="width: 100%; margin-top: 20px; text-align: right;">
                            <asp:LinkButton ID="lbtnsuccessclose1" runat="server" CssClass="btn btn-success btn-sm ml-2"> <i class="fa fa-check"></i> OK </asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div style="visibility: hidden;">
                    <asp:Button ID="Button6" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>
        <div class="row">
            <cc1:ModalPopupExtender ID="mperror" runat="server" PopupControlID="pnlerror" CancelControlID="lbtnclose1"
                TargetControlID="Button1" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlerror" runat="server" Style="position: fixed;">
                <div class="card" style="width: 350px;">
                    <div class="card-header">
                        <h4 class="card-title text-left mb-0">Please Check
                        </h4>
                    </div>
                    <div class="card-body text-left pt-2" style="min-height: 100px;">
                        <asp:Label ID="lblerrormsg" runat="server" Text="Please Check entered values."></asp:Label>
                        <div style="width: 100%; margin-top: 20px; text-align: right;">
                            <asp:LinkButton ID="lbtnclose1" runat="server" CssClass="btn btn-danger btn-sm ml-2"> <i class="fa fa-times"></i> OK </asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div style="visibility: hidden;">
                    <asp:Button ID="Button1" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>
        <div class="row">
            <cc1:ModalPopupExtender ID="mpInfo" runat="server" PopupControlID="PanelInfo" CancelControlID="LinkButtonMpInfoClose"
                TargetControlID="LinkButtonInfo" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="PanelInfo" runat="server" Style="position: fixed;">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">About Module</h4>
                        </div>
                        <div class="modal-body">
                            <ol>
                                <li>Assign module to employees of HQ level</li>
                                <li>On login, only assigned module will be seen to HQ employees.</li>
                                <li>You can also remove module access from user by clicking on Remove button.</li>
                            </ol>
                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton ID="LinkButtonMpInfoClose" runat="server" CssClass="btn btn-success btn-sm ml-2"> <i class="fa fa-check"></i> OK </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
