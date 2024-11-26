<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/SysAdmmaster.master" AutoEventWireup="true" CodeFile="SysAdmLayoutMain.aspx.cs" Inherits="Auth_SysAdmLayoutMain" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="header pb-7">
    </div>
    <asp:HiddenField ID="hidtoken" runat="server" />
    <div class="container-fluid mt--6">
        <div class="row align-items-center">
            <div class="col-xl-12">
                <div class="card card-stats mb-3">
                    <div class="row">
                        <div class="col-4 border-right">
                            <div class="card-body">
                                <div class="row m-0">
                                    <div class="col-12">
                                        <h4 class="mb-1">
                                            <asp:Label ID="lblSummary" runat="server" CssClass="text-capitalize">Summary as on Date 01/12/2021 04:00pm</asp:Label></h4>
                                        <div class="row m-0">
                                            <div class="col-6 border-right">
                                                <div class="row m-0">
                                                    <div class="col-12">
                                                        <h5 class="card-title text-uppercase text-muted mb-0">Total Layout:&nbsp;
                                 <asp:Label ID="lblTotalLayout" runat="server" ToolTip="Total Cities Available" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>
                                                    </div>
                                                    <div class="col-12">
                                                        <h5 class="card-title text-uppercase text-muted mb-0">Locked Layout: &nbsp;
                                <asp:Label ID="lblLockLayout" runat="server" ToolTip="Active Cities" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-6">
                                                <div class="row m-0">
                                                    <div class="col-12">
                                                        <h5 class="card-title text-uppercase text-muted mb-0">Pending Layout:&nbsp;
                                 <asp:Label ID="lblPendingLayout" runat="server" ToolTip="Deactive Cities" class="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>
                                                    </div>
                                                    <div class="col-12">
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-4 border-right">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col">
                                        <div class="input-group-prepend">
                                            <h4 class="mb-1">Generate Bus Layout Report</h4>
                                       
                                            <asp:LinkButton ID="lbtndownload" ToolTip="Download Report" runat="server" CssClass="btn btn bg-gradient-green btn-sm text-white ml-2">
                                            <i class="fa fa-download"></i>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <div class="col-4">
                            <div class="card-body">
                                <div class="row mr-0">
                                    <div class="col">
                                        <div>
                                            <h4 class="mb-1">Instructions</h4>
                                        </div>

                                        <ul>
                                            <li>Step 1..Give Row & Column</li>
                                            <li>Step 2..Select/Deselect Seats as per</li>
                                            <li>Step 3..Change Seat Number</li>
                                            <li>Step 4..Decide Seat Type</li>
                                        </ul>

                                    </div>
                                    <div class="col-auto">
                                        <asp:LinkButton ID="lbtnview" OnClick="lbtnview_Click" ToolTip="View Instructions" runat="server" Width="30px" CssClass="btn btn bg-gradient-orange btn-sm text-white">
                                    <i class="fa fa-info"></i>
                                        </asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="lbtndwnldinst" OnClick="lbtndwnldinst_Click" class="btn btn bg-gradient-green btn-sm text-white" Width="30px" ToolTip="Download Instructions">
                                            <i class="fa fa-download"></i>
                                        </asp:LinkButton>
                                    </div>
                                </div>

                            </div>
                        </div>

                    </div>

                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-xl-6 order-xl-1">
                <div class="card" style="min-height: 470px">
                    <div class="card-header border-bottom">
                        <div class="float-left">
                            <h3 class="mb-0">Layout List</h3>
                        </div>
                        <div class="float-right">
                            <div class="input-group">
                                <asp:TextBox ID="tbSearch" ToolTip="Enter Layout" CssClass="form-control form-control-sm mr-1" MaxLength="20" runat="server" Width="200px" placeholder="Search Layout" autocomplete="off"></asp:TextBox>
                                <asp:LinkButton ID="lbtnSearchLayout" ToolTip="Search Layout" runat="server" CssClass="btn bg-success btn-sm text-white mr-1" OnClick="lbtnSearchLayout_Click">
                                            <i class="fa fa-search"></i>
                                </asp:LinkButton>
                                <asp:LinkButton ID="lbtnResetSearchLayout" ToolTip="Reset Layout" runat="server" CssClass="btn bg-warning btn-sm text-white" OnClick="lbtnResetSearchLayout_Click">
                                            <i class="fa fa-undo"></i>
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="card-body p-0">
                        <div class="row m-0 align-items-center">
                            <div class="col-lg-12">
                                <asp:GridView ID="gvLayout" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover" GridLines="None"
                                    AllowPaging="true" PageSize="10" OnPageIndexChanging="gvLayout_PageIndexChanging"
                                    HeaderStyle-CssClass="thead-light font-weight-bold" OnRowCommand="gvLayout_RowCommand" Width="100%" OnRowDataBound="gvLayout_RowDataBound"
                                    DataKeyNames="layoutcode,layoutname,noof_rows,noof_column,totalseats,sta,layoutcategory,noof_rowsu,noof_columnu,totalseats_forbooking">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Layout" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLayout" ToolTip="Layout" CssClass="form-control-label" runat="server" Text='<%#Eval("layoutname") %>' />
                                                <br />
                                                (<span><%#Eval("noof_rows") %></span> X <span><%#Eval("noof_column") %></span>)
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Layout Category" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                            <ItemTemplate>
                                               <asp:Label ID="lbllayoutcategory" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action" HeaderStyle-CssClass="text-left" >
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnView" runat="server" CommandName="gvLayoutView" ToolTip="Click to Vew"
                                                    CssClass="btn btn-sm btn-primary" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'><i class="fa fa-eye"></i>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="lbtnDelete" runat="server" CommandName="gvLayoutDelete" ToolTip="Click to Delete"
                                                    CssClass="btn btn-sm btn-danger" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'><i class="fa fa-trash"></i>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="lbtnibtnChangeSeatNo" runat="server" CommandName="gvLayoutChangeSeatNo" ToolTip="Change Seat Numbering"
                                                    CssClass="btn btn-sm btn-primary" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'><i class="fa fa-cog"></i>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="lbtnibtnSeatType" runat="server" CommandName="gvLayoutChangeSeatType" ToolTip="Change Seat Types"
                                                    CssClass="btn btn-sm btn-success" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'><i class="fa fa-cogs"></i>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="lbtnLock" runat="server" CommandName="gvLayoutSLock" ToolTip="Lock Layout"
                                                    CssClass="btn btn-sm btn-warning" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'><i class="fa fa-lock"></i>
                                                </asp:LinkButton>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                                </asp:GridView>
                                <asp:Panel runat="server" ID="pnlNoRecord" Visible="true" CssClass="text-center" Width="100%">
                                    <p class="text-center" style="font-size: 32px; font-weight: bold; color: #e3e3e3; margin-top: 50px;">
                                        Layout Not Available
                                    </p>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-xl-6 order-xl-2">
                <div class="card" style="min-height: 470px">
                    <div class="card-header border-bottom">
                        <div class="row m-0">
                            <div class="col-md-6 text-left">
                                <h3 class="mb-0" runat="server" id="lblAddLayoutHeader" visible="true">Add New Layout</h3>
                                <h3 class="mb-0" runat="server" id="lblUpdateLayoutHeader" visible="false">Update Layout</h3>
                            </div>
                            <div class="col-md-6 text-right">
                                <p style="font-size: small; margin: 0px;" class="text-danger font-weight-bold">All marked * Fields are mandatory</p>
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row m-0">
                            <div class="col-lg-12">
                                <h5 style="margin-top: 5px; text-align: left;">1. Layout Type <span style="color: red">*</span></h5>
                            </div>
                            <div class="col-lg-12">
                                <asp:RadioButtonList ID="rblType" runat="server" RepeatDirection="Horizontal" CssClass="form-check"
                                    AutoPostBack="true">
                                    <asp:ListItem Value="G" Selected="True" Text="General"></asp:ListItem>
<%--                                    <asp:ListItem Value="S" Text="Sleeper"></asp:ListItem>--%>
                                </asp:RadioButtonList>
                            </div>
                        </div>

                        <div class="row m-0">
                            <div class="col-lg-12">
                                <h5 style="margin-top: 5px; text-align: left;">2. Layout Name<span style="color: red">*</span> </h5>
                            </div>
                            <div class="col-lg-6">
                                <asp:TextBox class="form-control form-control-sm" runat="server" ID="tbLayoutName" MaxLength="20" ToolTip="Layout Name" autocomplete="off"
                                    placeholder="Max 20 Characters" Text="" Style=""></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="ajxFtLayoutName" runat="server" FilterType="Numbers, UppercaseLetters, LowercaseLetters,Custom" TargetControlID="tbLayoutName" ValidChars=" " />
                            </div>
                        </div>

                        <div class="row m-0">
                            <div class="col-lg-12">
                                <h5 style="margin-top: 5px; text-align: left;">3. Rows & Columns in Vehicle </h5>

                            </div>
                            <div class="col-lg-6">
                                <label class="form-control-label">
                                    Rows<span style="color: red">*</span></label>
                                <asp:TextBox class="form-control form-control-sm" runat="server" ID="tbrows" MaxLength="2" ToolTip="No of rows" autocomplete="off"
                                    placeholder="Only number" Text="" Style=""></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="ajxFtrows" runat="server" FilterType="Numbers" TargetControlID="tbrows" />
                            </div>
                            <div class="col-lg-6">
                                <label class="form-control-label">
                                    Columns<span style="color: red">*</span></label>
                                <asp:TextBox class="form-control form-control-sm" runat="server" ID="tbcolumns" MaxLength="2" ToolTip="No of Columns" autocomplete="off"
                                    placeholder="Only number" Text="" Style=""></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="ajxFtcolumns" runat="server" FilterType="Numbers" TargetControlID="tbcolumns" />
                            </div>

                        </div>
                        <div class="row m-0">
                            <asp:Panel ID="pnlUpper" runat="server" Visible="false">
                                <div class="col-lg-12">
                                    <div class="row m-0">
                                        <div class="col-lg-6">
                                            <label style="font-weight: bold">
                                                Rows</label>
                                            <asp:TextBox class="form-control form-control-sm" runat="server" ID="tbrowsU" MaxLength="2" ToolTip="No of rows" autocomplete="off"
                                                placeholder="Only number" Text="" Style=""></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="ajxFttbrowsU" runat="server" FilterType="Numbers" TargetControlID="tbrowsU" />
                                        </div>
                                        <div class="col-lg-6">
                                            <label style="font-weight: bold">
                                                Columns</label>
                                            <asp:TextBox class="form-control form-control-sm" runat="server" ID="tbcolumnsU" MaxLength="2" ToolTip="No of Columns" autocomplete="off"
                                                placeholder="Only number" Text="" Style=""></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="ajxFttbcolumnsU" runat="server" FilterType="Numbers" TargetControlID="tbcolumnsU" />
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                        <div class="row m-0">
                            <div class="col-lg-12 mt-3 text-center">
                                <asp:LinkButton ID="lbtnSave" Visible="true" runat="server" class="btn btn-success" ToolTip="Save Layout" OnClick="lbtnSave_Click">
                                    <i class="fa fa-save"></i>&nbsp; Save</asp:LinkButton>
                                <asp:LinkButton ID="lbtnReset" runat="server" CssClass="btn btn-danger" ToolTip="Reset Layout" OnClick="lbtnReset_Click">
                                    <i class="fa fa-undo"></i>&nbsp; Reset</asp:LinkButton>

                            </div>
                        </div>

                    </div>
                </div>
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
                        <asp:LinkButton ID="lbtnNoConfirmation" runat="server" CssClass="btn btn-danger btn-sm ml-2"> <i class="fa fa-times"></i> No </asp:LinkButton>
                    </div>
                </div>
            </div>
            <div style="visibility: hidden;">
                <asp:Button ID="Button4" runat="server" Text="" />
            </div>
        </asp:Panel>
    </div>


</asp:Content>

