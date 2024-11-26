<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/SysAdmmaster.master" AutoEventWireup="true" CodeFile="SysAdmStateServiceWiseCharge.aspx.cs" Inherits="Auth_SysAdminStateServiceWiseCharge" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {

            var todayDate = new Date().getDate();
            var endD = new Date(new Date().setDate(todayDate + 10));
            var currDate = new Date();

            $('[id*=tbDate]').datepicker({
                startDate: "dateToday",
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField runat="server" ID="hdnTotSlabs" />
    <div class="container-fluid pt-2 pb-5">
        <div class="row align-items-center">
            <div class="col-xl-12">
                <div class="card card-stats mb-3">
                    <div class="card card-stats mb-3">
                        <div class="row">
                            <div class="col-4 border-right">
                                <div class="card-body">
                                    <div class="row m-0">
                                        <div class="col--lg-12">
                                            <h4 class="mb-1">
                                                <asp:Label ID="lblSummary" runat="server" CssClass="text-capitalize">Summary as on Date 01/12/2021 04:00pm</asp:Label></h4>

                                            <div class="row m-0">
                                                <div class="col-12">
                                                    <h5 class="card-title text-uppercase text-muted mb-0">Total State:&nbsp;
                                 <asp:Label ID="lblTotalCharge" runat="server" data-toggle="tooltip" data-placement="bottom" title="Total Charge" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>
                                                </div>
                                            </div>
                                            <div class="row m-0">
                                                <div class="col-12">
                                                    <h5 class="card-title text-uppercase text-muted mb-0">Active: &nbsp;
                                <asp:Label ID="lblActivateCharge" runat="server" data-toggle="tooltip" data-placement="bottom" title="Active Charge" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>
                                                </div>
                                            </div>
                                            <div class="row m-0">
                                                <div class="col-12">
                                                    <h5 class="card-title text-uppercase text-muted mb-0">Discontinue:&nbsp;
                                 <asp:Label ID="lblDeactCharge" runat="server" data-toggle="tooltip" data-placement="bottom" title="Deactive Charge" class="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>
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
                                            <div>
                                                <h4 class="mb-1">Generate Charge Report</h4>
                                            </div>
                                            <div class="input-group mb-3">
                                                <div class="input-group-prepend pr-2" style="width: 60%">
                                                    <asp:DropDownList ID="ddlCharges" data-toggle="tooltip" data-placement="bottom" title="Charge" CssClass="form-control form-control-sm" runat="server">
                                                        <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>
                                                <asp:LinkButton ID="lbtndownload" data-toggle="tooltip" data-placement="bottom" title="Download" runat="server" CssClass="btn btn bg-gradient-green btn-sm text-white">
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
                                            <asp:Label runat="server" CssClass="form-control-label">Coming Soon</asp:Label>
                                        </div>
                                        <div class="col-auto">
                                            <asp:LinkButton ID="lbtnview" OnClick="lbtnview_Click" data-toggle="tooltip" data-placement="bottom" title="View Instructions" runat="server" Width="30px" CssClass="btn btn bg-gradient-orange btn-sm text-white">
                                    <i class="fa fa-info"></i>
                                            </asp:LinkButton>
                                            <asp:LinkButton runat="server" class="btn btn bg-gradient-green btn-sm text-white" Width="30px" data-toggle="tooltip" data-placement="bottom" title="Download Instruction">
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
        </div>
        <div class="row">
            <div class="col-xl-4 order-xl-1">
                <div class="card" style="min-height: 470px">
                    <div class="card-header border-bottom">
                        <div class="row ">
                            <div class="col-lg-3 ">
                                <h3 class="mb-0">Charge List</h3>
                            </div>

                            <div class="col-lg-3 ">

                                <asp:DropDownList ID="ddlstateSearch" CssClass="form-control form-control-sm" runat="server">
                                    <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-lg-3 ">
                                <asp:DropDownList ID="ddlServiceTypeSearch" CssClass="form-control form-control-sm" runat="server">
                                    <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-lg-3 ">
                                <asp:LinkButton ID="lbtnSearchCharge" OnClick="lbtnSearchCharge_Click" runat="server" CssClass="btn bg-success btn-sm text-white mr-1">
                                            <i class="fa fa-search"></i>
                                </asp:LinkButton>
                                <asp:LinkButton ID="lbtnResetSearchCharge" OnClick="lbtnResetSearchCharge_Click" runat="server" CssClass="btn bg-warning btn-sm text-white">
                                            <i class="fa fa-undo"></i>
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <asp:GridView ID="gvCharge" runat="server" GridLines="None" ShowHeader="false" AutoGenerateColumns="false" Width="100%"
                        OnRowCommand="gvCharge_RowCommand" OnRowDataBound="gvCharge_RowDataBound" OnPageIndexChanging="gvCharge_PageIndexChanging"
                        AllowPaging="true" PageSize="10" DataKeyNames="faretype,satus,state_name,stateid,srtpid,servicetype_name_en,efffrom_dt,createby,createdt,farename">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <div class="card card-stats">
                                        <!-- Card body -->
                                        <div class="card-body">
                                            <div class="row">
                                                <div class="col">
                                                    <h5 class="text-uppercase mb-0 font-weight-bold text-xs">
                                                        <%# Eval("state_name") %> - <%# Eval("farename") %> <span class="text-gray"><%# Eval("servicetype_name_en") %></span></h5>
                                                    <h6><span class="text-gray"><%# Eval("charges") %></span></h6>
                                                </div>
                                                <div class="col-right">
                                                    <asp:LinkButton ID="lbtnUpdategency" Visible="true" runat="server" CommandName="updateCharge" CssClass="btn btn-sm btn-info" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' Style="border-radius: 4px;" title="Update Fare Details" data-toggle="tooltip" data-placement="bottom"> <i class="fa fa-edit"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnActivate" Visible="true" runat="server" CommandName="activate" CssClass="btn btn-sm btn-success" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' ToolTip="Activate Service Charge"> <i class="fa fa-check"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnDiscontinue" Visible="true" runat="server" CommandName="deactivate" CssClass="btn btn-sm btn-warning" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' ToolTip="Deactivate Service Charge"> <i class="fa fa-ban"></i></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
						<PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                    </asp:GridView>
                    <asp:Panel ID="pnlNoCharge" runat="server">
                        <div class="card card-stats">
                            <!-- Card body -->
                            <div class="card-body">
                                <div class="row">

                                    <div class="col-lg-12 text-center">
                                        <i class="fa fa-bus fa-5x"></i>
                                    </div>
                                    <div class="col-lg-12 text-center mt-4" runat="server" id="divNoRecord">
                                        <span class="h2 font-weight-bold mb-0">Start Charge Creation </span>
                                        <h5 class="card-title text-uppercase text-muted mb-0">No Charge has been created yet</h5>
                                    </div>
                                    <div class="col-lg-12 text-center mt-4" runat="server" id="divNoSearchRecord" visible="false">
                                        <span class="h2 font-weight-bold mb-0">No Record Found </span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
            <div class="col-xl-8 order-xl-2">
                <asp:Panel ID="pnlAddFare" runat="server" Visible="true">
                    <div class="card" style="min-height: 470px">
                        <div class="card-header">
                            <div class="row m-0 align-items-center">
                                <div class="col-8 ">
                                    <h3 class="mb-0">
                                        <asp:Label runat="server" ID="lblChargeHead" Text="Add State service wise Charge"></asp:Label>
                                        <asp:Label runat="server" Visible="false" ID="lblChargeUpdate" Text="Update State service wise Charge"></asp:Label></h3>
                                </div>
                                <div class="col-md-4 text-right">
                                    <p style="font-size: small; margin: 0px;" class="text-danger font-weight-bold">All marked * Fields are mandatory</p>
                                </div>
                            </div>
                        </div>

                        <div class="card-body">
                            <asp:Panel ID="pnlSearch" runat="server" Style="background-color: #efeff4">
                                <div class="pl-lg-4">
                                    <div class="row">
                                        <div class="col-lg-3">
                                            <asp:Label runat="server" CssClass="form-control-label"> State<span style="color: red">*</span></asp:Label>
                                            <asp:DropDownList ID="ddlState" CssClass="form-control form-control-sm" runat="server">
                                                <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-lg-3">
                                            <asp:Label runat="server" CssClass="form-control-label">Service Type<span style="color: red">*</span></asp:Label><br />
                                            <asp:DropDownList ID="ddlServiceType" CssClass="form-control form-control-sm" runat="server">
                                                <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-lg-3">
                                            <asp:Label runat="server" CssClass="form-control-label"> Fare Type<span style="color: red">*</span></asp:Label><br />
                                            <asp:DropDownList ID="ddlFareType" AutoPostBack="true" CssClass="form-control form-control-sm" runat="server" OnSelectedIndexChanged="ddlFareType_SelectedIndexChanged">
                                                <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                <asp:ListItem Value="S" Text="Slab"></asp:ListItem>
                                                <asp:ListItem Value="P" Text="Per Km"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <br />
                            </asp:Panel>
                            <asp:Panel ID="pnlPerKM" Visible="false" runat="server" CssClass="my-2">
                                <h6 class="heading-small text-muted my-0">1.Charges Details</h6>
                                <div class="pl-lg-4">
                                    <div class="row">
                                        <div class="col-lg-3">
                                            <asp:Label runat="server" CssClass="form-control-label">Accedent SC<span style="color: red">*</span></asp:Label>
                                            <asp:TextBox ID="tbAccedentSc" placeholder="Accedent Sc" AutoComplete="off" MaxLength="4" CssClass="form-control form-control-sm" runat="server">
                                           
                                            </asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="ajaxFTtbAccedentSc" runat="server" ValidChars="1234567890." FilterType="Custom" TargetControlID="tbAccedentSc" />

                                        </div>
                                        <div class="col-lg-3">
                                            <asp:Label runat="server" CssClass="form-control-label">Passenger SC<span style="color: red">*</span></asp:Label><br />
                                            <asp:TextBox ID="tbPassengerSC" placeholder="Passenger SC" AutoComplete="off" MaxLength="4" CssClass="form-control form-control-sm" runat="server">
                                           
                                            </asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="ajaxFTtbPassengerSC" runat="server" ValidChars="1234567890." FilterType="Custom" TargetControlID="tbPassengerSC" />

                                        </div>
                                        <div class="col-lg-3">
                                            <asp:Label runat="server" CssClass="form-control-label">IT SC<span style="color: red">*</span></asp:Label><br />
                                            <asp:TextBox ID="tbITSc" placeholder="IT SC" AutoComplete="off" MaxLength="4" CssClass="form-control form-control-sm" runat="server">
                                           
                                            </asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="ajaxFTtbITSc" runat="server" ValidChars="1234567890." FilterType="Custom" TargetControlID="tbITSc" />

                                        </div>
                                        <div class="col-lg-3">
                                            <asp:Label runat="server" CssClass="form-control-label">Other SC<span style="color: red">*</span></asp:Label><br />
                                            <asp:TextBox ID="tbOtherSc" placeholder="Other SC" AutoComplete="off" MaxLength="4" CssClass="form-control form-control-sm" runat="server">
                                           
                                            </asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="ajaxFTtbOtherSc" runat="server" ValidChars="1234567890." FilterType="Custom" TargetControlID="tbOtherSc" />

                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnlSlab" Visible="false" runat="server" CssClass="my-2">
                                <div class="row m-0">
                                    <div class="col-lg-8">
                                        <h6 class="heading-small text-muted my-0">1.Charges Details</h6>
                                    </div>
                                    <div class="col-lg-4 text-right">
                                        <h6 class="heading-small text-muted my-0">Surcharges</h6>
                                    </div>
                                </div>
                                <div class="col-lg-12">
                                    <asp:GridView ID="gvaddSlab" Visible="true" OnRowCommand="gvaddSlab_RowCommand" OnRowDataBound="gvaddSlab_RowDataBound"
                                        HeaderStyle-CssClass="thead-light font-weight-bold" GridLines="None" runat="server"
                                        CssClass="table table-striped table-hover" AutoGenerateColumns="false" DataKeyNames="fr_km,to_km,adnt_s_charge,pngr_s_charge,it_s_charge,othr_s_charge">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Font-Size="Small" HeaderText="From km">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="tbFromkm1" placeholder="From km" AutoComplete="off" MaxLength="4" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="ajaxFttbFromkm1" runat="server" ValidChars="1234567890" FilterType="Custom" TargetControlID="tbFromkm1" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Font-Size="Small" HeaderText="To km">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="tbtokm1" placeholder="To km" AutoComplete="off" MaxLength="4" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="ajaxFTtbtokm1" ValidChars="1234567890" runat="server" FilterType="Custom" TargetControlID="tbtokm1" />

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Font-Size="Small" HeaderText="Accedent Sc">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="tbAccedentSCSlab1" placeholder="Accedent Sc" AutoComplete="off" MaxLength="4" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="ajaxFTtbAccedentSCSlab1" ValidChars="1234567890." runat="server" FilterType="Custom" TargetControlID="tbAccedentSCSlab1" />

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Font-Size="Small" HeaderText="Passenger Sc">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="tbPassengerSCslab1" placeholder="Passenger Sc" AutoComplete="off" MaxLength="4" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="ajaxFTtbPassengerSCslab1" ValidChars="1234567890." runat="server" FilterType="Custom" TargetControlID="tbPassengerSCslab1" />

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Font-Size="Small" HeaderText="It Sc">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="tbItScSlab1" placeholder="It Sc" AutoComplete="off" MaxLength="4" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="ajaxFTtbItScSlab1" ValidChars="1234567890." runat="server" FilterType="Custom" TargetControlID="tbItScSlab1" />

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Font-Size="Small" HeaderText="Other Sc">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="tbOtherScSlab1" placeholder="Other Sc" AutoComplete="off" MaxLength="4" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="ajaxFTtbOtherScSlab1" ValidChars="1234567890." runat="server" FilterType="Custom" TargetControlID="tbOtherScSlab1" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnAddNewSlab" Visible="true" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="Add" runat="server" CssClass="btn btn-sm btn-success"><i class="fa fa-plus"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnRemoveNewSlab" Visible="false" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="Remove" runat="server" CssClass="btn btn-sm btn-warning"><i class="fa fa-minus"></i> </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
										<PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                                    </asp:GridView>
                                    <label class="text-muted text-xs">Last Slab will not be saved. Please leave it blank.</label>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnlEffectiveDAte" Visible="false" runat="server" CssClass="my-2">

                                <h6 class="heading-small text-muted my-0">2.Effective from Date</h6>
                                <div class="pl-lg-4">
                                    <div class="row">
                                        <div class="col-lg-3">
                                            <asp:Label runat="server" CssClass="form-control-label"> Effective Date<span style="color: red">*</span></asp:Label>
                                            <asp:TextBox AutoComplete="off" ID="tbDate" runat="server" CssClass="form-control form-control-sm text-uppercase" MaxLength="10" placeholder="DD/MM/YYYY"></asp:TextBox>

                                        </div>
                                    </div>
                                </div>

                                <div class="pl-lg-4 mt-2">
                                    <div class="row">
                                        <div class="col-lg-12 text-right">
                                            <asp:LinkButton ID="lbtnUpdate" runat="server" class="btn btn-success" OnClick="lbtnUpdate_Click" Visible="false" data-toggle="tooltip" data-placement="bottom" title="Update State service wise Charge Details">
                                    <i class="fa fa-save"></i>&nbsp; Update</asp:LinkButton>
                                            <asp:LinkButton ID="lbtnSave" Visible="true" runat="server" class="btn btn-success" OnClick="lbtnSave_Click" data-toggle="tooltip" data-placement="bottom" title="Save State service wise Charge Details">
                                    <i class="fa fa-save"></i>&nbsp; Save</asp:LinkButton>
                                            <asp:LinkButton ID="lbtnReset" runat="server" Visible="true" CssClass="btn btn-danger" OnClick="lbtnReset_Click" data-toggle="tooltip" data-placement="bottom" title="Reset State service wise Charge Details">
                                    <i class="fa fa-undo"></i>&nbsp; Reset</asp:LinkButton>
                                            <asp:LinkButton ID="lbtnCancel" Visible="false" runat="server" CssClass="btn btn-danger" OnClick="lbtnCancel_Click" data-toggle="tooltip" data-placement="bottom" title="Cancel State service wise Charge Details">
                                    <i class="fa fa-times"></i>&nbsp; Cancel</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </asp:Panel>

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
                            <asp:LinkButton ID="lbtnYesConfirmation" runat="server" CssClass="btn btn-success btn-sm" OnClick="lbtnYesConfirmation_Click"> <i class="fa fa-check"></i> Yes </asp:LinkButton>
                            <asp:LinkButton ID="lbtnNoConfirmation" runat="server" CssClass="btn btn-danger btn-sm ml-2"> <i class="fa fa-times"></i> No </asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div style="visibility: hidden;">
                    <asp:Button ID="Button4" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>

    </div>
</asp:Content>

