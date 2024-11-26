<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/SysAdmmaster.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeFile="SysAdmStateServiceWiseTax.aspx.cs" Inherits="Auth_SysAdmServicewiseTax" %>

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
                                                        <h5 class="card-title text-uppercase text-muted mb-0">Total State&nbsp;
                                 <asp:Label ID="lblTotalFare" runat="server" data-toggle="tooltip" data-placement="bottom" title="Total Fare" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>
                                                    </div>
                                                    <div class="col-12">
                                                        <h5 class="card-title text-uppercase text-muted mb-0">Per Km &nbsp;
                                <asp:Label ID="lblFare" runat="server" data-toggle="tooltip" data-placement="bottom" title="Fare" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>
                                                    </div>
                                                    <div class="col-12">
                                                        <h5 class="card-title text-uppercase text-muted mb-0">Slab&nbsp;
                                 <asp:Label ID="lblSlab" runat="server" data-toggle="tooltip" data-placement="bottom" title="Slab" class="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>
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
                            <h3 class="mb-0">Tax List</h3>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="input-group">
                                    <asp:DropDownList ID="ddlSState" runat="server" ToolTip="Select From State" CssClass="form-control form-control-sm mr-1">
                                        <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlSServiceType" runat="server" ToolTip="Select Service Type" CssClass="form-control form-control-sm mr-1">
                                        <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:LinkButton ID="lbtnSearch" OnClick="lbtnSearch_Click" data-toggle="tooltip" data-placement="bottom" title="Search" runat="server" CssClass="btn bg-success btn-sm text-white mr-1">
                                            <i class="fa fa-search"></i>
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="lbtnResetSearch" OnClick="lbtnResetSearch_Click" data-toggle="tooltip" data-placement="bottom" title="Reset Fare" runat="server" CssClass="btn bg-warning btn-sm text-white">
                                            <i class="fa fa-undo"></i>
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>

                        <div class="row pt-2">
                            <div class="col-md-12">
                                <asp:GridView ID="gvServiceWiseTax" runat="server" GridLines="None" ShowHeader="false" AutoGenerateColumns="false" Width="100%"
                                    OnRowCommand="gvServiceWiseTax_RowCommand" OnRowDataBound="gvServiceWiseTax_RowDataBound" OnPageIndexChanging="gvServiceWiseTax_PageIndexChanging"
                                    AllowPaging="true" PageSize="10" DataKeyNames="faretype,stateid,state_name ,taxbasedon , taxes,statusyn,eff ,taxtype,srtpid,servicetypename,taxon,taxtyp,fare_name,fromkm,tokm,val,id">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <div class="card card-stats mb-0">
                                                    <!-- Card body -->
                                                    <div class="card-body px-3 py-2">
                                                        <div class="row">
                                                            <div class="col">
                                                                <h5 class="text-uppercase mb-0 font-weight-bold text-xs">
                                                                    <%# Eval("state_name") %> - <%# Eval("fare_name") %> <span class="text-gray"><%# Eval("servicetypename") %></span></h5>
                                                                <h6 class="mb-0"><span class="text-gray"><%# Eval("taxes") %></span></h6>
                                                            </div>
                                                            <div class="col-right text-right">
                                                                <asp:LinkButton ID="lbtnActivate" Visible="true" runat="server" CommandName="activate" CssClass="btn btn-sm btn-success" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' Style="border-radius: 4px;" title="Activate Tax" data-toggle="tooltip" data-placement="bottom"> <i class="fa fa-check"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lbtnDiscontinue" Visible="true" runat="server" CommandName="deactivate" CssClass="btn btn-sm btn-warning" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' Style="border-radius: 4px;" data-toggle="tooltip" data-placement="bottom" title="Deactivate Tax"> <i class="fa fa-ban"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lbtnUpdateTax" Visible="true" runat="server" CommandName="updateTax" CssClass="btn btn-sm btn-info" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' Style="border-radius: 4px;" title="Update Tax Details" data-toggle="tooltip" data-placement="bottom"> <i class="fa fa-edit"></i></asp:LinkButton>
                                                                <div class="col-12 text-right pt-2">
                                                                    <span class="text-gray text-xs">Tax Type </span><%# Eval("taxtyp") %>
                                                                </div>
                                                            </div>
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
                                                    <span class="h2 font-weight-bold mb-0">Start Tax Creation </span>
                                                    <h5 class="card-title text-uppercase text-muted mb-0">No Tax has been created yet</h5>
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
                                    <asp:Label runat="server" ID="lblTaxHead" Text="Add State Service wise Tax"></asp:Label>
                                    <asp:Label runat="server" ID="lblTaxUpdate" Visible="false" Text="Update State Service wise Tax"></asp:Label>

                                </h3>
                            </div>
                            <div class="float-right">
                                <p style="font-size: small; margin: 0px;" class="text-danger font-weight-bold">All marked * Fields are mandatory</p>
                            </div>
                        </div>
                        <div class="card-body">
                            <asp:Panel ID="pnlSearch" runat="server" Style="background-color: #efeff4">
                                <div class="pl-lg-4 pr-lg-4">
                                    <div class="row">
                                        <div class="col-lg-3 mr-2">
                                            <asp:Label runat="server" CssClass="form-control-label"> State<span style="color: red">*</span></asp:Label>
                                            <asp:DropDownList ID="ddlState" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control form-control-sm text-uppercase" runat="server">
                                                <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-lg-2 mr-2">
                                            <asp:Label runat="server" CssClass="form-control-label">Service Type<span style="color: red">*</span></asp:Label><br />
                                            <asp:DropDownList ID="ddlServiceType" AutoPostBack="true" CssClass="form-control form-control-sm text-uppercase" runat="server">
                                                <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                       

                                        <div class="col-lg-2 mr-2">
                                            <asp:Label runat="server" CssClass="form-control-label"> Tax Based On<span style="color: red">*</span></asp:Label><br />
                                            <asp:DropDownList ID="ddlTaxBasedOn" AutoPostBack="true" CssClass="form-control form-control-sm text-uppercase" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-lg-2">
                                            <asp:Label runat="server" CssClass="form-control-label"> Tax Type<span style="color: red">*</span></asp:Label>
                                            <asp:DropDownList ID="ddlTaxType" AutoPostBack="true" CssClass="form-control form-control-sm text-uppercase" runat="server">
                                                <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                <asp:ListItem Value="H" Text="Hill"></asp:ListItem>
                                                <asp:ListItem Value="P" Text="Plain"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <br />

                            </asp:Panel>
                            <asp:Panel ID="pnlPerKm" Visible="true" runat="server" CssClass="my-2">
                                <h6 class="heading-small text-muted my-0">1. Fare Details</h6>
                                
                                <div class="pl-lg-4 pr-lg-4">
                                    <div class="row">
                                        <div class="col-lg-3">
                                            <asp:Label runat="server" CssClass="form-control-label"> From Km<span style="color: red">*</span></asp:Label>
                                            <asp:TextBox ID="tbFromkm" AutoComplete="off" placeholder="Max Length 5" MaxLength="5" CssClass="form-control form-control-sm" runat="server">                                     
                                            </asp:TextBox>
                                            <cc1:FilteredTextBoxExtender runat="server" FilterType="custom" ValidChars="1234567890." TargetControlID="tbFromkm" />
                                        </div>
                                         <div class="col-lg-3">
                                            <asp:Label runat="server" CssClass="form-control-label"> To Km<span style="color: red">*</span></asp:Label>
                                            <asp:TextBox ID="tbtokm" AutoComplete="off" placeholder="Max Length 5" MaxLength="5" CssClass="form-control form-control-sm" runat="server">                                     
                                            </asp:TextBox>
                                            <cc1:FilteredTextBoxExtender runat="server" FilterType="custom" ValidChars="1234567890." TargetControlID="tbtokm" />
                                        </div>
                                        <div class="col-lg-3">
                                            <asp:Label runat="server" CssClass="form-control-label"> Tax Value<span style="color: red">*</span></asp:Label>
                                            <asp:TextBox ID="tbTaxValue" AutoComplete="off" placeholder="Max Length 5" MaxLength="5" CssClass="form-control form-control-sm" runat="server">                                     
                                            </asp:TextBox>
                                            <cc1:FilteredTextBoxExtender runat="server" FilterType="custom" ValidChars="1234567890." TargetControlID="tbTaxValue" />
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                          
                            <asp:Panel runat="server" ID="pnlShow" Visible="true">
                                <h6 class="heading-small text-muted my-0">2. Effective From Date</h6>
                                <div class="pl-lg-4">
                                    <div class="row">
                                        <div class="col-lg-3">
                                            <asp:Label runat="server" CssClass="form-control-label"> Effective From Date<span style="color: red">*</span></asp:Label>
                                            <asp:TextBox AutoComplete="off" ID="tbDate" ToolTip="Enter Effective From Date" runat="server" CssClass="form-control form-control-sm text-uppercase" MaxLength="10" placeholder="DD/MM/YYYY"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-12 text-right pr-2">
                                        <asp:LinkButton ID="lbtnUpdate" runat="server" class="btn btn-success" OnClick="lbtnUpdate_Click" Visible="false" data-toggle="tooltip" data-placement="bottom" title="Update Tax Details">
                                    <i class="fa fa-save"></i>&nbsp; Update</asp:LinkButton>
                                        <asp:LinkButton ID="lbtnSave" Visible="true" runat="server" class="btn btn-success" OnClick="lbtnSave_Click" data-toggle="tooltip" data-placement="bottom" title="Save Tax Details">
                                    <i class="fa fa-save"></i>&nbsp; Save</asp:LinkButton>
                                        <asp:LinkButton ID="lbtnReset" runat="server" CssClass="btn btn-danger" OnClick="lbtnReset_Click" data-toggle="tooltip" data-placement="bottom" title="Reset Tax Details">
                                    <i class="fa fa-undo"></i>&nbsp; Reset</asp:LinkButton>
                                        <asp:LinkButton ID="lbtnCancel" Visible="false" runat="server" CssClass="btn btn-danger" OnClick="lbtnCancel_Click" data-toggle="tooltip" data-placement="bottom" title="Cancel Tax Updation">
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

