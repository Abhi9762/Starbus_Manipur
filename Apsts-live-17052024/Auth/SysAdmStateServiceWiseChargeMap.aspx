<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/SysAdmmaster.master" AutoEventWireup="true" CodeFile="SysAdmStateServiceWiseChargeMap.aspx.cs" Inherits="Auth_SysAdmServiceWiseChargeMap" %>


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
                                            <div class="col-6 border-right">
                                                <div class="row m-0">
                                                    <div class="col-12">

                                                        <h5 class="card-title text-uppercase text-muted mb-0">Total States:
                                 <asp:Label ID="lblTotalFare" runat="server" data-toggle="tooltip" data-placement="bottom" title="Total States" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>
                                                    </div>
                                                    <div class="col-12">
                                                        <h5 class="card-title text-uppercase text-muted mb-0">Merged States:
                                 <asp:Label ID="lblMergeState" runat="server" data-toggle="tooltip" data-placement="bottom" title="Merged States" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-6">
                                                <div class="row m-0">
                                                    <div class="col-12">
                                                        <h5 class="card-title text-uppercase text-muted mb-0">Active: &nbsp;
                                <asp:Label ID="lblActivateFare" runat="server" data-toggle="tooltip" data-placement="bottom" title="Active Fare" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>
                                                    </div>
                                                    <div class="col-12">
                                                        <h5 class="card-title text-uppercase text-muted mb-0">Discontinue:&nbsp;
                                 <asp:Label ID="lblDeactFare" runat="server" data-toggle="tooltip" data-placement="bottom" title="Deactive Fare" class="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>
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
                                            <h4 class="mb-1">Download Fare Report</h4>
                                             <asp:LinkButton ID="lbtndownload" data-toggle="tooltip" OnClick="lbtndownload_Click" data-placement="bottom" title="Download" runat="server" CssClass="btn btn bg-gradient-green btn-sm text-white ml-1">
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
                                        <asp:Label runat="server" CssClass="form-control-label">1. Here you can add state wise charge map.</asp:Label>
                                    </div>
                                    <div class="col-auto">
                                        <asp:LinkButton ID="lbtnview" OnClick="lbtnview_Click" data-toggle="tooltip" data-placement="bottom" title="View Instructions" runat="server" Width="30px" CssClass="btn btn bg-gradient-orange btn-sm text-white">
                                    <i class="fa fa-info"></i>
                                        </asp:LinkButton>
                                        <asp:LinkButton runat="server" OnClick="Unnamed_Click" class="btn btn bg-gradient-green btn-sm text-white" Width="30px" data-toggle="tooltip" data-placement="bottom" title="Download Instruction">
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
                <div class="card" style="min-height: 70vh">
                    <div class="card-header border-bottom">
                        <div class="float-left">
                            <h3 class="mb-0">Fare List</h3>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row pt-2">
                            <div class="col-md-12">
                                <asp:GridView ID="gvServiceWiseChargeMap" runat="server" GridLines="None" ShowHeader="false" AutoGenerateColumns="false" Width="100%"
                                    OnRowCommand="gvServiceWiseChargeMap_RowCommand" OnRowDataBound="gvServiceWiseChargeMap_RowDataBound" OnPageIndexChanging="gvServiceWiseChargeMap_PageIndexChanging"
                                    AllowPaging="true" PageSize="10" DataKeyNames="scsmm,from_s,to_s,chargeid ,charge_type ,combinedflag, 
				  statusyn ,up_datedt,combinedyn,fromst ,tost">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <div class="card card-stats">
                                                    <!-- Card body -->
                                                    <div class="card-body">
                                                        <div class="row">
                                                            <div class="col">
                                                                <h5 class=" mb-0 font-weight-bold text-xs">
                                                                    <%# Eval("fromst") %> To  <%# Eval("tost") %>
                                                                    <h6><i><span class="text-gray">Charge Type- 
                                                            <%# Eval("charge_type") %>,
                                                                    Combined Flag -
                                                            <%# Eval("combinedyn") %> </i></h6>


                                                                </h5>
                                                            </div>
                                                            <div class="col-right">
                                                                <asp:LinkButton ID="lbtnActivate" Visible="true" runat="server" CommandName="activate" CssClass="btn btn-sm btn-success" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' Style="border-radius: 4px;" title="Activate Fare" data-toggle="tooltip" data-placement="bottom"> <i class="fa fa-check"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lbtnDiscontinue" Visible="true" runat="server" CommandName="deactivate" CssClass="btn btn-sm btn-warning" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' Style="border-radius: 4px;" data-toggle="tooltip" data-placement="bottom" title="Deactivate Fare"> <i class="fa fa-ban"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lbtnUpdateChargeMap" Visible="true" runat="server" CommandName="updateFare" CssClass="btn btn-sm btn-info" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' Style="border-radius: 4px;" title="Update Fare Details" data-toggle="tooltip" data-placement="bottom"> <i class="fa fa-edit"></i></asp:LinkButton>
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
                <div class="card" style="min-height: 70vh;">
                    <div class="card-header">
                        <div class="float-left">
                            <h3 class="mb-0">
                                <asp:Label runat="server" ID="lblFareHead" Text="Add State Service Wise Charge Map"></asp:Label>
                                <asp:Label runat="server" ID="lblUpdate" Visible="false" Text="Update State Service Wise Charge Map"></asp:Label></h3>

                        </div>
                        <div class="float-right">
                            <p style="font-size: small; margin: 0px;" class="text-danger font-weight-bold">All marked * Fields are mandatory</p>
                        </div>
                    </div>

                    <div class="card-body">
                        <div class="pl-lg-4 my-2">
                            <div class="row">
                                <div class="col-lg-3">
                                    <asp:Label runat="server" CssClass="form-control-label">From State<span style="color: red">*</span></asp:Label>
                                    <asp:DropDownList ID="ddlFromState" AutoPostBack="true" OnSelectedIndexChanged="ddlFromState_SelectedIndexChanged" CssClass="form-control form-control-sm" runat="server">
                                        <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-3">
                                    <asp:Label runat="server" CssClass="form-control-label">To State<span style="color: red">*</span></asp:Label><br />
                                    <asp:DropDownList ID="ddlToState" AutoPostBack="true" CssClass="form-control form-control-sm" runat="server">
                                        <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                            </div>
                        </div>
                        <div class="pl-lg-4 my-2">
                            <div class="row">
                                <div class="col-lg-3">
                                    <asp:Label runat="server" CssClass="form-control-label">Charge Type<span style="color: red">*</span></asp:Label>
                                    <asp:DropDownList ID="ddlChargeType" AutoPostBack="true" CssClass="form-control form-control-sm" runat="server">
                                        <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-3 mt-4">
                                    <asp:Label runat="server" CssClass="form-control-label">Combined</asp:Label>
                                    <asp:CheckBox ID="cbCombined" runat="server" AutoPostBack="true" />
                                </div>
                            </div>
                        </div>
                        <div class="pl-lg-4 my-2">
                            <div class="row">
                                <div class="col-lg-3">
                                    <asp:Label runat="server" CssClass="form-control-label"> Effective From Date<span style="color: red">*</span></asp:Label>
                                    <asp:TextBox AutoComplete="off" ID="tbDate" runat="server" CssClass="form-control form-control-sm text-uppercase" MaxLength="10" placeholder="DD/MM/YYYY"></asp:TextBox>

                                </div>
                            </div>
                        </div>

                        <div class="row">
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

</asp:Content>


