<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/Depotmaster.master" AutoEventWireup="true" CodeFile="DepAdminPumpMgmt.aspx.cs" Inherits="Auth_DepAdminPumpMgmt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        $(document).ready(function () {

            var currDate = new Date().getDate();
            var preDate = new Date(new Date().setDate(currDate - 1));
            var todayDate = new Date(new Date().setDate(currDate));

            $('[id*=tbStartedOnDate]').datepicker({
                endDate: todayDate,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="header pb-7">
    </div>
    <div class="container-fluid mt--6">
        <div class="row align-items-center">
            <div class="col-xl-12">
                <div class="card card-stats mb-3">
                    <div class="row m-0">
                        <div class="col-md-4 border-right">
                            <div class="card-body">
                                <div class="row">
                                    <h4 class="mb-1">
                                        <asp:Label ID="lblSummary" runat="server" CssClass="text-capitalize">Summary as on Date 01/12/2021 04:00pm</asp:Label></h4>
                                    <div class="col-md-6">
                                        <asp:Label runat="server" CssClass="form-control-label">Total Pump: </asp:Label>
                                    </div>
                                    <div class="col-md-3 text-right">
                                        <asp:Label ID="lblPumpListCount" runat="server" CssClass="form-control-label" Font-Bold="true" Style="color: red;"><span style="color: red"></span></asp:Label>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <div class="col-3 border-right">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col">
                                        <div>
                                            <h4 class="mb-1">Generate Pump Managemet Report</h4>
                                        </div>
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend pr-2" style="width: 80%">
                                                <asp:DropDownList ID="ddlPumpReport" data-toggle="tooltip" data-placement="bottom" title="Agency" CssClass="form-control form-control-sm" runat="server">
                                                    <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <asp:LinkButton ID="lbtnDownloadPumpRpt" data-toggle="tooltip" data-placement="bottom" title="Download" runat="server" CssClass="btn btn bg-gradient-green btn-sm text-white">
                                            <i class="fa fa-download"></i>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>

                        <div class="col-md-5">
                            <div class="card-body">
                                <div class="row mr-0">
                                    <div class="col">
                                        <div>
                                            <h4 class="mb-1">Instructions</h4>
                                        </div>
                                        <ul class="data-list" data-autoscroll>
                                            <li>
                                                <asp:Label runat="server" CssClass="form-control-label">• To create pump,first select depot, filling station and tank and then enter pump details.</asp:Label><br />
                                            </li>
                                            <li>
                                                <asp:Label runat="server" CssClass="form-control-label">• Depot mananger can create pump of its depot only.</asp:Label><br />
                                            </li>
                                            <li>
                                                <asp:Label runat="server" CssClass="form-control-label">• If pump is used then it cannot be deleted.</asp:Label><br />
                                            </li>
                                            <li>
                                                <asp:Label runat="server" CssClass="form-control-label">• Except tank number, other details can be updated.</asp:Label>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-auto">
                                        <asp:LinkButton ID="lbtnview" OnClick="lbtnview_Click" ToolTip="View Instructions" runat="server" CssClass="btn btn bg-gradient-orange btn-sm text-white">
                                    <i class="fa fa-eye"></i>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lbtnDownload" runat="server" class="btn btn bg-gradient-green btn-sm text-white" ToolTip="Download Instruction">
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
                <div class="card" style="min-height: 430px">
                    <div class="card-header border-bottom">
                        <div class="row m-0">
                            <div class="col-lg-9">
                                <asp:Label runat="server" Visible="true" Font-Bold="true" CssClass="form-control-label"><h3>Pumps List</h3></asp:Label>
                            </div>
                            <div class="col-lg-3 text-right">
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row m-0 align-items-center">
                            <div class="col-12">
                                <asp:GridView ID="gvPumpFillingStation" OnRowCommand="gvPumpFillingStation_RowCommand" runat="server" OnPageIndexChanging="gvPumpFillingStation_PageIndexChanging" AutoGenerateColumns="False" PageSize="6" GridLines="None" AllowPaging="true"
                                    CssClass="table table-striped" PagerStyle-CssClass="GridPager" Font-Size="10pt" Width="100%"
                                    DataKeyNames="officeid,tanknumber,pumpnumber,installedon_date,initialreading,fillingstn,currentstatus,currentstatus_ondate">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No." ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Pump Number" ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvName" runat="server" Text='<%#Eval("pumpnumber")%>'></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Installed on Date" ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvAddress" runat="server" Text='<%#Eval("installedon_date")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Initial Reading" ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvInitialReading" runat="server" Text='<%#Eval("initialreading")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="gvlnkbtnEdit" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' runat="server" ToolTip="Click here for edit" CssClass="btn btn-sm btn-warning" CommandName="gvEdit"><i class="fa fa-edit"></i></asp:LinkButton>
                                                <asp:LinkButton Visible="false" ID="gvlnkbtndelete" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' runat="server" ToolTip="Click here for delete" CssClass="btn btn-sm btn-danger" CommandName="gvDelete"><i class="fa fa-trash"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
									<PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                                </asp:GridView>
                                <asp:Panel runat="server" ID="pnlNoRecord" Visible="false" CssClass="text-center" Width="100%">
                                    <p class="text-center" style="font-size: 32px; font-weight: bold; color: #e3e3e3; margin-top: 50px;">
                                        No Record Available
                                    </p>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-xl-6 order-xl-2">
                <asp:Panel runat="server" ID="pnlAddPump" Visible="true">
                    <div class="card" style="min-height: 430px">
                        <div class="card-header border-bottom">
                            <div class="row m-0">
                                <div class="col-md-7 text-left">
                                    <asp:Label ID="lblAddPump" runat="server" Visible="true" Font-Bold="true" CssClass="form-control-label"><h3>Add New Pump Entry</h3></asp:Label>
                                    <asp:Label ID="lblUpdatePump" runat="server" Visible="false" Font-Bold="true" CssClass="form-control-label"><h3>Update Pump Entry</h3></asp:Label>
                                </div>
                                <div class="col-md-5 text-right">
                                    <asp:Label runat="server" CssClass="form-control-label" Style="color: red;">All Marked * Fields are mandatory </asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="card-body">
                            <div class="row m-0 mt-1 align-items-center">
                                <div class="col-lg-6">
                                    <asp:Label runat="server" Style="margin-top: 6px" Font-Bold="true" CssClass="form-control-label">Depot</asp:Label>
                                    <asp:DropDownList ID="ddlDepot" ToolTip="Select Depot" AutoPostBack="true" Enabled="false" OnSelectedIndexChanged="ddlDepot_SelectedIndexChanged" CssClass="form-control form-control-sm" runat="server"></asp:DropDownList>
                                </div>
                                <div class="col-lg-6">
                                    <asp:Label runat="server" Style="margin-top: 6px" Font-Bold="true" CssClass="form-control-label">Filling Station <span style="color: red">*</span></asp:Label>
                                    <asp:DropDownList ID="ddlFillingStation" ToolTip="Select Filling Station" AutoPostBack="true" OnSelectedIndexChanged="ddlFillingStation_SelectedIndexChanged" CssClass="form-control form-control-sm" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="row m-0 mt-2 align-items-center">
                                <div class="col-lg-6">
                                    <asp:Label runat="server" Style="margin-top: 6px" Font-Bold="true" CssClass="form-control-label">Tank<span style="color: red">*</span></asp:Label>
                                    <asp:DropDownList ID="ddlTank" ToolTip="Select Tank" CssClass="form-control form-control-sm" runat="server"></asp:DropDownList>
                                </div>
                                <div class="col-lg-6">
                                    <asp:Label runat="server" CssClass="form-control-label">Pump Number<span style="color: red">*</span></asp:Label>
                                    <asp:TextBox ID="tbPumpNumber" class="form-control form-control-sm" runat="server" MaxLength="5" ToolTip="Enter Pump Number" autocomplete="off"
                                        placeholder="Max length 5" Text=""></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="ajaxFTtbPumpNumber" runat="server" FilterType="Numbers, Custom" TargetControlID="tbPumpNumber" />
                                </div>
                            </div>
                            <div class="row m-0 mt-2">
                                <div class="col-lg-6">
                                    <asp:Label runat="server" CssClass="form-control-label">Started on Date<span style="color: red">*</span></asp:Label>
                                    <asp:TextBox ID="tbStartedOnDate" ToolTip="Enter Started On Date" runat="server" autocompletee="off" CssClass="form-control form-control-sm" MaxLength="10" placeholder="DD/MM/YYYY"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers,Custom" TargetControlID="tbStartedOnDate" ValidChars="/" />
                                </div>
                                <div class="col-lg-6">
                                    <asp:Label runat="server" CssClass="form-control-label">Initial Reading<span style="color: red">*</span> </asp:Label>
                                    <asp:TextBox ID="tbInitialReading" class="form-control form-control-sm" runat="server" MaxLength="10" ToolTip="Enter Initial Reading" autocomplete="off"
                                        placeholder="Max length 10" Text=""></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="ajaxFttbNoOfPumps" runat="server" FilterType="Numbers, Custom" TargetControlID="tbInitialReading" />
                                </div>
                            </div>
                            <div class="row m-0 mt-2 align-items-center">

                                <div class="col-lg-12 mt-4 text-left">
                                    <asp:LinkButton ID="lbtnPMUpdate" CommandName="Update" runat="server" OnClick="lbtnPMUpdate_Click" class="btn btn-primary" Visible="false" ToolTip="Click Here For Update ">
                                    <i class="fa fa-save"></i>&nbsp; Update</asp:LinkButton>
                                    <asp:LinkButton ID="lbtnPMSave" CommandName="Save" OnClick="lbtnPMSave_Click" Visible="true" runat="server" class="btn btn-success" ToolTip="Click Here For Save">
                                    <i class="fa fa-save"></i>&nbsp; Save</asp:LinkButton>
                                    <asp:LinkButton ID="lbtnPMReset" CommandName="Reset" OnClick="lbtnPMReset_Click" runat="server" CssClass="btn btn-danger" ToolTip="Click Here For Reset">
                                    <i class="fa fa-undo"></i>&nbsp; Reset</asp:LinkButton>

                                </div>
                            </div>

                        </div>
                    </div>

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

