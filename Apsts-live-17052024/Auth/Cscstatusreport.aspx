<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/CscMasterPage.master" AutoEventWireup="true" CodeFile="Cscstatusreport.aspx.cs" Inherits="Auth_Cscstatusreport" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };
    </script>
    <style>
        .pagination-ys {
            /*display: inline-block;*/
            padding-left: 0;
            margin: 20px 0;
            border-radius: 4px;
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-12 ">
                <div class="card mt-2">
                    <div class="card-header">
                        <div class="row ">
                            <div class="col-4">
                                <strong>CSC details</strong><br />
                                <asp:Label ID="Label3" runat="server" ForeColor="Green" Font-Size="small" Font-Bold="false" Text="(Details will be Available All/Activate/Deactivate)"></asp:Label>
                            </div>

                            <div class="col-3">
                                <asp:Label ID="Label1" runat="server" Font-Size="small" CssClass="ml-2" Font-Bold="false" Text="Status"></asp:Label>
                                <br />
                                <asp:DropDownList ID="ddlcscstatus" runat="server" CssClass="form-control form-control-sm ml-2">
                                    <asp:ListItem Value="0">All</asp:ListItem>
                                    <asp:ListItem Value="A">Active</asp:ListItem>
                                    <asp:ListItem Value="D">Deactive</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div class="col-2" style="padding: 0px; padding-top: 20px;">
                                <asp:LinkButton ID="lbtnSearch" OnClientClick="return ShowLoading();" ToolTip="Click here for Search" Style="padding: 7px;" runat="server" OnClick="lbtnSearch_Click" CssClass="btn btn-success btn-sm">
                                                                    <i class="fa fa-search" ></i></asp:LinkButton>

                            </div>
                        </div>

                    </div>
                    <div class="card-body" style="min-height: 80vh;">
                        <asp:Panel ID="pnlreport" runat="server" Visible="false">
                            <div class="row mb-2">
                                <div class="col-lg-4">
                                    <span><b>Summary | </b>
                                        <asp:Label ID="lblsmry" runat="server" Text=""></asp:Label>
                                    </span>
                                </div>
                                <div class="col-lg-8 text-right">
                                    <asp:LinkButton ID="lbtndownload" ToolTip="Click here for Download" Visible="false" Style="padding: 7px;" runat="server" OnClick="lbtndownload_Click" CssClass="btn btn-danger btn-sm">
                                                                    <i class="fa fa-file-pdf-o" ></i> PDF</asp:LinkButton>
                                    <asp:LinkButton ID="lbtnexcel" ToolTip="Click here for Download" Visible="false" Style="padding: 7px;" runat="server" OnClick="lbtnexcel_Click" CssClass="btn btn-warning btn-sm">
                                                                    <i class="fa fa-file-excel-o" ></i> EXCEL</asp:LinkButton>
                                </div>
                            </div>
                            <asp:GridView ID="grdcscDetails" runat="server" AllowPaging="true" PageSize="10" AutoGenerateColumns="False" ForeColor="#333333" Font-Size="14px" OnRowCommand="grdcscDetails_RowCommand" OnRowDataBound="grdcscDetails_RowDataBound" OnPageIndexChanging="grdcscDetails_PageIndexChanging" DataKeyNames="status_"
                                CssClass="table" GridLines="None" Font-Bold="false"  Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="CSC Name/Code">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblAGENTNAME" Text='<%# Eval("agent_name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Address">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblADDRESS" Text='<%# Eval("address_") %>'></asp:Label><br />
                                            <asp:Label ID="lblDISTRICTNAME" runat="server" Text='<%# Eval("districtname_") %>'></asp:Label>
                                            , 
                                         <asp:Label ID="lblSTATENAME" runat="server" Text='<%# Eval("statename_") %>'></asp:Label>
                                            <asp:Label ID="lblPINCODE" runat="server" Text='<%# Eval("pincode_") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Contact Info.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCONTACTPERSON" runat="server" Text='<%# Eval("contactperson_") %>'></asp:Label>
                                            <br />
                                            <i class="fa fa-mobile-phone"></i>
                                            <asp:Label ID="lblMOBILENO" runat="server" Text='<%# Eval("mobileno_") %>'></asp:Label>
                                            <br />
                                            <i class="fa fa-envelope-o"></i>
                                            <asp:Label ID="lblEMAIL" runat="server" Text='<%# Eval("emailid_") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Registration On">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblREGISTRATIONDATE" Text='<%# Eval("registrationdate_") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Current Wallet Balance">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblCurrntBalnce" Text='<%# Eval("currentbalance_") %>'></asp:Label>
                                            <i class="fa fa-inr"></i>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Current Status">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblcurrentStatus" Text='<%# Eval("currentstatus_") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                                <PagerStyle CssClass="pagination-ys" />
                            </asp:GridView>
                        </asp:Panel>
                        <div class="row">
                            <div class="col-12 mb-2 mt-5">
                                <center>
                                    <asp:Label ID="grdmsg" runat="server" Text="Details Not Available for selected perameter"
                                        Style="color: #DDDDDD; font-size: xx-large" CssClass="mt-5"></asp:Label>
                                </center>
                            </div>

                        </div>

                    </div>
                </div>
            </div>
        </div>


        <div class="row">
            <cc1:ModalPopupExtender ID="mpError" runat="server" PopupControlID="pnlError" CancelControlID="lbtnerrorclose"
                TargetControlID="Button3" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlError" runat="server" Style="position: fixed; display: none">
                <div class="card" style="min-width: 350px; max-width: 650px;">
                    <div class="card-header">
                        <h4 class="card-title m-0">Please Check
                        </h4>
                    </div>
                    <div class="card-body py-2 px-3" style="min-height: 100px; max-height: 70vh; overflow: auto;">
                        <asp:Label ID="lblerrmsg" runat="server" Font-Size="18px"></asp:Label>
                    </div>
                    <div class="card-footer text-right ">
                        <asp:LinkButton ID="lbtnerrorclose" runat="server" CssClass="btn btn-danger"> OK </asp:LinkButton>
                    </div>
                </div>
                <br />
                <div style="visibility: hidden;">
                    <asp:Button ID="Button3" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>
    </div>


</asp:Content>



