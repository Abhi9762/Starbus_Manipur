<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/SysAdmmaster.master" AutoEventWireup="true" CodeFile="SysAdmStateServiceWiseFare.aspx.cs" Inherits="Auth_SysAdmStateServiceWiseFare" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function validateFloatKeyPress(el, evt) {

            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (charCode != 46 && charCode > 31
                && (charCode < 48 || charCode > 57)) {
                return false;
            }

            if (charCode == 46 && el.value.indexOf(".") !== -1) {
                return false;
            }

            return true;
        }
        
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
    <asp:HiddenField ID="hidtoken" runat="server" />
    <div class="container-fluid pt-2 pb-5">
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
                                            <div class="col-12">
                                                <div class="row m-0">
                                                    <div class="col-12">
                                                        <h5 class="card-title text-uppercase text-muted mb-0">Total State:&nbsp;
                                 <asp:Label ID="lblTotalFare" runat="server" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>
                                                    </div>
                                                    <div class="col-12">
                                                        <h5 class="card-title text-uppercase text-muted mb-0">Per KM: &nbsp;
                                <asp:Label ID="lblFare" runat="server" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>
                                                    </div>
                                                    <div class="col-12">
                                                        <h5 class="card-title text-uppercase text-muted mb-0">Slab:&nbsp;
                                 <asp:Label ID="lblSlab" runat="server" class="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>
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
                                        <div>
                                            <h4 class="mb-1">Generate Fare Report</h4>
                                        </div>
                                        <br />
                                        <div class="input-group mt-2">
                                            <div class="input-group-prepend pr-2" style="width: 60%">
                                                <asp:DropDownList ID="ddlFare" data-toggle="tooltip" data-placement="bottom" title="Fare" CssClass="form-control form-control-sm" runat="server">
                                                    <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                    <asp:ListItem Value="P" Text="Slab"></asp:ListItem>
                                                    <asp:ListItem Value="S" Text="Per Km"></asp:ListItem>
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
        <div class="row">
            <div class="col-xl-4 order-xl-1">
                <div class="card" style="min-height: 470px">
                    <div class="card-header border-bottom">
                        <div class="float-left">
                            <h3 class="mb-0">Fare List</h3>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="input-group">
                                    <asp:DropDownList ID="ddlSState" runat="server" CssClass="form-control form-control-sm mr-1">
                                        <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlSServiceType" runat="server" CssClass="form-control form-control-sm mr-1">
                                        <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:LinkButton ID="lbtnSearch" OnClick="lbtnSearch_Click" runat="server" CssClass="btn bg-success btn-sm text-white mr-1">
                                            <i class="fa fa-search"></i>
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="lbtnResetSearch" OnClick="lbtnResetSearch_Click" runat="server" CssClass="btn bg-warning btn-sm text-white">
                                            <i class="fa fa-undo"></i>
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>

                        <div class="row pt-3">
                            <div class="col-md-12">
                                <asp:GridView ID="gvServiceWiseFare" runat="server" GridLines="None" ShowHeader="false" AutoGenerateColumns="false" Width="100%"
                                    OnRowCommand="gvServiceWiseFare_RowCommand" OnRowDataBound="gvServiceWiseFare_RowDataBound" OnPageIndexChanging="gvServiceWiseFare_PageIndexChanging"
                                    AllowPaging="true" PageSize="10" DataKeyNames="faretype,fares,statusyn,stateid,statename,srtp,srtpname,eff,fare">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>

                                                <!-- Card body -->
                                                <div class="card mb-0">
                                                    <div class="row px-3 pt-2">
                                                        <div class="col">
                                                            <h5 class="text-uppercase mb-0 font-weight-bold text-xs">
                                                                <%# Eval("statename") %> - <%# Eval("fare") %> <span class="text-gray"><%# Eval("srtpname") %></span></h5>
                                                            <h6><span class="text-gray"><%# Eval("fares") %></span></h6>

                                                        </div>

                                                        <div class="col-right">
                                                            <asp:LinkButton ID="lbtnActivate" Visible="true" runat="server" CommandName="activate" CssClass="btn btn-sm btn-success" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' Style="border-radius: 4px;" title="Activate Fare" data-toggle="tooltip" data-placement="bottom"> <i class="fa fa-check"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="lbtnDiscontinue" Visible="true" runat="server" CommandName="deactivate" CssClass="btn btn-sm btn-warning" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' Style="border-radius: 4px;" data-toggle="tooltip" data-placement="bottom" title="Deactivate Fare"> <i class="fa fa-ban"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="lbtnUpdateFare" Visible="true" runat="server" CommandName="updateFare" CssClass="btn btn-sm btn-info" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' Style="border-radius: 4px;" title="Update Fare Details" data-toggle="tooltip" data-placement="bottom"> <i class="fa fa-edit"></i></asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
									<PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                                </asp:GridView>
                                <asp:Panel ID="pnlNoFare" runat="server">
                                    <div class="card card-stats">
                                        <!-- Card body -->
                                        <div class="card-body">
                                            <div class="row">

                                                <div class="col-lg-12 text-center">
                                                    <i class="fa fa-bus fa-5x"></i>
                                                </div>
                                                <div class="col-lg-12 text-center mt-4" runat="server" id="divNoRecord">
                                                    <span class="h2 font-weight-bold mb-0">Start Fare Creation </span>
                                                    <h5 class="card-title text-uppercase text-muted mb-0">No Fare has been created yet</h5>
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
                    </div>
                </div>
            </div>
            <div class="col-xl-8 order-xl-2">
                <asp:Panel ID="pnlAddFare" runat="server" Visible="true">
                    <div class="card" style="min-height: 470px">
                        <div class="card-header">
                            <div class="float-left">
                                <h3 class="mb-0">
                                    <asp:Label runat="server" ID="lblFareHead" Text="Add State service wise fare"></asp:Label>
                                    <asp:Label runat="server" Visible="false" ID="lblFareUpdate" Text="Update State service wise fare"></asp:Label>
                                </h3>
                            </div>
                            <div class="float-right">
                                <p style="font-size: small; margin: 0px;" class="text-danger font-weight-bold">All marked * Fields are mandatory</p>
                            </div>
                        </div>
                        <div class="card-body">
                            <asp:Panel ID="pnlSearch" runat="server" Style="background-color: #efeff4">
                                <div class="pl-lg-4">
                                    <div class="row">
                                        <div class="col-lg-3">
                                            <asp:Label runat="server" CssClass="form-control-label"> State<span style="color: red">*</span></asp:Label>
                                            <asp:DropDownList ID="ddlState" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control form-control-sm" runat="server">
                                                <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-lg-3">
                                            <asp:Label runat="server" CssClass="form-control-label">Service Type<span style="color: red">*</span></asp:Label><br />
                                            <asp:DropDownList ID="ddlServiceType" AutoPostBack="true" OnSelectedIndexChanged="ddlServiceType_SelectedIndexChanged" CssClass="form-control form-control-sm" runat="server">
                                                <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-lg-3">
                                            <asp:Label runat="server" CssClass="form-control-label"> Fare Type<span style="color: red">*</span></asp:Label><br />
                                            <asp:DropDownList ID="ddlFareType" AutoPostBack="true" CssClass="form-control form-control-sm" runat="server">
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
                                <h6 class="heading-small text-muted my-0">1. Fare Details</h6>
                                <div class="pl-lg-4">
                                    <div class="row">
                                        <div class="col-lg-3">
                                            <asp:Label runat="server" CssClass="form-control-label">Hill Rate(₹)<span style="color: red">*</span></asp:Label>
                                            <asp:TextBox ID="tbHillRate" placeholder="Max 5 digit" AutoComplete="off" MaxLength="5" CssClass="form-control form-control-sm" runat="server">
                                           
                                            </asp:TextBox>
                                            <cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers,Custom" ValidChars="." TargetControlID="tbHillRate" />

                                        </div>
                                        <div class="col-lg-3">
                                            <asp:Label runat="server" CssClass="form-control-label">Plain Rate(₹)<span style="color: red">*</span></asp:Label><br />
                                            <asp:TextBox ID="tbPlainRate" placeholder="Max 5 digit" AutoComplete="off" MaxLength="5" CssClass="form-control form-control-sm" runat="server">
                                           
                                            </asp:TextBox>
                                            <cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers,custom" ValidChars="." TargetControlID="tbPlainRate" />
                                        </div>

                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnlSlab" Visible="false" runat="server" CssClass="my-2">
                                <h6 class="heading-small text-muted my-0">1. Fare Details</h6>
                                <asp:GridView ID="gvSlab" runat="server" ShowFooter="true" OnRowDataBound="gvSlab_RowDataBound" OnRowCommand="gvSlab_RowCommand" CssClass="table table-striped table-hover" GridLines="None"
                                    HeaderStyle-CssClass="thead-light font-weight-bold" DataKeyNames="fr_km,to_km,rate_hill,rate_plain" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="From Km">
                                            <ItemTemplate>
                                                <asp:TextBox ID="tbFromkm" Enabled="false" placeholder="Max 5 digit" AutoComplete="off" MaxLength="5" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender runat="server" FilterType="Custom" ValidChars="1234567890" TargetControlID="tbFromkm" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="To Km">
                                            <ItemTemplate>
                                                <asp:TextBox ID="tbtokm" placeholder="Max 5 digit" AutoComplete="off" MaxLength="5" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender runat="server" FilterType="Custom" ValidChars="1234567890" TargetControlID="tbtokm" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Hill Rate">
                                            <ItemTemplate>
                                                <asp:TextBox ID="tbHillRateSlab" placeholder="Max 5 digit" AutoComplete="off" MaxLength="5" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender runat="server" FilterType="Custom" ValidChars="1234567890." TargetControlID="tbHillRateSlab" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Plain Rate">
                                            <ItemTemplate>
                                                <asp:TextBox ID="tbPlainRateslab" placeholder="Max 5 digit" AutoComplete="off" MaxLength="5" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender runat="server" FilterType="Custom" ValidChars="1234567890." TargetControlID="tbPlainRateslab" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnAddNewSlab" Visible="true" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="Add" runat="server" ToolTip="Add New Slab" CssClass="btn btn-sm btn-success"><i class="fa fa-plus"></i></asp:LinkButton>
                                                <asp:LinkButton ID="lbtnRemoveNewSlab" Visible="false" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="Remove" runat="server" ToolTip="Remove New Slab" CssClass="btn btn-sm btn-warning"><i class="fa fa-minus"></i> </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
									<PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                                </asp:GridView>
                                <label class="mt--3 text-muted text-xs">Last Slab will not be saved. Please leave it blank.</label>
                            </asp:Panel>
                            <asp:Panel runat="server" ID="pnlShow" Visible="false">
                                <h6 class="heading-small text-muted my-0">2. Effective From Date</h6>
                                <div class="pl-lg-4">
                                    <div class="row">
                                        <div class="col-lg-3">
                                            <asp:Label runat="server" CssClass="form-control-label"> Effective From Date<span style="color: red">*</span></asp:Label>
                                            <asp:TextBox AutoComplete="off" ID="tbDate" runat="server" CssClass="form-control form-control-sm text-uppercase" MaxLength="10" placeholder="DD/MM/YYYY"></asp:TextBox>

                                        </div>
                                    </div>
                                </div>
                                <br />

                                <div class="row border-top">
                                    <div class="col-lg-12 text-right">
                                        <br />
                                        <asp:LinkButton ID="lbtnUpdate" runat="server" class="btn btn-success" OnClick="lbtnUpdate_Click" Visible="false" data-toggle="tooltip" data-placement="bottom" title="Update Fare Details">
                                    <i class="fa fa-save"></i>&nbsp; Update</asp:LinkButton>
                                        <asp:LinkButton ID="lbtnSave" Visible="true" runat="server" class="btn btn-success" OnClick="lbtnSave_Click" data-toggle="tooltip" data-placement="bottom" title="Save Fare Details">
                                    <i class="fa fa-save"></i>&nbsp; Save</asp:LinkButton>
                                        <asp:LinkButton ID="lbtnReset" runat="server" CssClass="btn btn-danger" OnClick="lbtnReset_Click" data-toggle="tooltip" data-placement="bottom" title="Reset Fare Details">
                                    <i class="fa fa-undo"></i>&nbsp; Reset</asp:LinkButton>
                                        <asp:LinkButton ID="lbtnCancel" Visible="false" runat="server" CssClass="btn btn-danger" OnClick="lbtnCancel_Click" data-toggle="tooltip" data-placement="bottom" title="Cancel Fare Details">
                                    <i class="fa fa-times"></i>&nbsp; Cancel</asp:LinkButton>
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

