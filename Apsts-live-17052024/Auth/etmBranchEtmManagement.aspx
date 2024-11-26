<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/ETMBranchMaster.master" AutoEventWireup="true" CodeFile="etmBranchEtmManagement.aspx.cs" Inherits="Auth_etmBranchEtmManagement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .form-control-label1 {
            font-size: .75rem;
            font-weight: 600;
            color: white;
        }
    </style>
    <script type="text/javascript">

        $(document).ready(function () {

            var todayDate = new Date().getDate();
            var endD = new Date();
            var currDate = new Date();

            $('[id*=tbStatusDate]').datepicker({
                endDate: endD,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            });
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid pt-2">
        <div class="row mt-2">
            <div class="col-lg-3 col-md-3 order-xl-1">
                <div class="card" style="min-height: 490px">
                    <div class="card-header border-bottom">
                        <div class="row m-0">
                            <h3 class="mb-0">Status So Far</h3>
                        </div>
                    </div>
                    <div class="card-body p-0">
                        <div class="mb-2" style="font-size: 11pt; width: 100%">
                            <h4 class="text-left pt-0 pb-0 pl-3" style="font-weight: bold; font-size: 13pt; color: #7b7474;">Status Counts</h4>
                            <div class="row m-0">
                                <div class="col-md-6 pr-1">
                                    <div class="card" style="min-height: 10vh; margin-bottom: 10px">
                                        <div class="card-body" style="padding: 10px; text-align: center; height: 80px">
                                            <div class="col-lg-12 p-0">
                                                <asp:Label ID="lblTotalETM" CssClass="text-success" runat="server" CommandArgument="0" Text="0" Font-Size="16pt" Font-Bold="true" CommandName="O"></asp:Label>
                                                <br />
                                                <label for="ddhead" style="line-height: 18px;">
                                                    Total ETM's
                                <br />
                                                    <br />
                                                </label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6 pl-1">
                                    <div class="card" style="min-height: 10vh; margin-bottom: 10px">
                                        <div class="card-body" style="padding: 10px; text-align: center; height: 80px">

                                            <div class="col-lg-12 p-0">
                                                <asp:Label ID="lblfreeetm" runat="server" CommandArgument="3" Text="0" CssClass="text-success" Font-Size="16pt" Font-Bold="true" CommandName="N"></asp:Label>
                                                <br />
                                                <label for="ddhead" style="line-height: 18px;">
                                                    Free ETM
                                <br />
                                                    <br />
                                                </label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6 pl-1">
                                    <div class="card" style="min-height: 10vh; margin-bottom: 10px">
                                        <div class="card-body" style="padding: 10px; text-align: center; height: 80px">

                                            <div class="col-lg-12 p-0">
                                                <asp:Label ID="lblondutyetm" runat="server" CommandArgument="3" Text="0" CssClass="text-success" Font-Size="16pt" Font-Bold="true" CommandName="N"></asp:Label>
                                                <br />
                                                <label for="ddhead" style="line-height: 18px;">
                                                    On Duty ETM
                                <br />
                                                    <br />
                                                </label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="clearfix"></div>




                    </div>
                </div>
            </div>
            <div class="col-lg-9 col-md-9 order-xl-1">
                <div class="card" style="min-height: 490px">
                    <div class="card-header border-bottom">
                        <div class="row m-0">
                            <div class="col-lg-8">
                                <div class="row m-0">
                                    <div class="col-lg-4">
                                        <h6 class="heading-small text-muted my-0">ETM Serial No</h6>
                                        <asp:TextBox ID="tbETMSerialNumber" ToolTip="ETM Serial No." CssClass="form-control form-control-sm" MaxLength="20" runat="server" placeholder="Max 20 chars" autocomplete="off"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers, UppercaseLetters" TargetControlID="tbETMSerialNumber" />
                                    </div>

                                    <div class="col-lg-4">
                                        <h6 class="heading-small text-muted my-0">ETM Type</h6>
                                        <asp:DropDownList ID="ddlSETMType" runat="server" CssClass="form-control form-control-sm" ToolTip="Select ETM Type">
                                            <asp:ListItem Value="0" Text="All"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-lg-4">
                                        <h6 class="heading-small text-muted my-0">ETM Status</h6>
                                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control form-control-sm" ToolTip="Select Status">
                                            <asp:ListItem Value="100" Text="All"></asp:ListItem>
                                            <asp:ListItem Value="0" Text="Free"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="On Duty"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-4 mt-3 pt-2 float-left!important">
                                <asp:LinkButton ID="lbtnSearch" OnClick="lbtnSearch_Click" data-toggle="tooltip" data-placement="bottom" title="Search ETM" runat="server" CssClass="btn bg-success btn-sm text-white">
                                            <i class="fa fa-search"></i>
                                </asp:LinkButton>
                                <asp:LinkButton ID="lbtnResetSearch" OnClick="lbtnResetSearch_Click" data-toggle="tooltip" data-placement="bottom" title="Reset ETM" runat="server" CssClass="btn bg-warning btn-sm text-white">
                                            <i class="fa fa-undo"></i>
                                </asp:LinkButton>
                               
                                <asp:LinkButton ID="lbtnview" OnClick="lbtnview_Click" data-toggle="tooltip" data-placement="bottom" title="View Instructions" runat="server" Width="30px" CssClass="btn btn bg-gradient-orange btn-sm text-white">
                                    <i class="fa fa-info"></i>
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="card-body p-0">
                        <asp:GridView ID="gvETMDetails" runat="server" CssClass="table table-hover table-striped" GridLines="None" ShowHeader="true" AutoGenerateColumns="false" Width="100%"
                            OnRowCommand="gvETMDetails_RowCommand" OnRowDataBound="gvETMDetails_RowDataBound" OnPageIndexChanging="gvETMDetails_PageIndexChanging"
                            AllowPaging="true" PageSize="10" DataKeyNames="etm_id,etm_refno,etmserialno,imeino_1,
				 imeino_2,assigned_office,emttype_name,etmmakemodel,etm_status,agency_name">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No." ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ETM Type- Serial No">
                                    <ItemTemplate>
                                        <b>
                                            <asp:Label ID="Label22" runat="server" Text='<%#Eval("emttype_name") %>' Style="font-size: 9pt" /></b>-
                                                                    <asp:Label ID="Label25" runat="server" Text='<%#Eval("etmserialno") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Imei No">
                                    <ItemTemplate>
                                        <asp:Label ID="Label6sed" runat="server" Text='<%#Eval("imeino_1") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Agency">
                                    <ItemTemplate>
                                        <asp:Label ID="Label26sa" runat="server" Text='<%#Eval("agency_name") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>

                                        <asp:Label ID="lblETMStatus" runat="server" Text='<%#Eval("etm_status") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField ItemStyle-CssClass="text-right">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnETMIssue" CommandName="ReturnETM" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                            Visible="true" runat="server" CssClass="btn btn-sm btn-primary" Style="border-radius: 4px;"
                                            data-toggle="tooltip" data-placement="bottom" title="Update ETM Status"> <i class="fa fa-tasks"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                        </asp:GridView>
                        <asp:Panel ID="pnlNoETM" runat="server">
                            <div class="card card-stats">
                                <!-- Card body -->
                                <div class="card-body">
                                    <div class="row">

                                        <div class="col-lg-12 text-center">
                                            <i class="fa fa-bus fa-5x"></i>
                                        </div>
                                        <div class="col-lg-12 text-center mt-4" runat="server" id="divNoRecord">
                                            <span class="h2 font-weight-bold mb-0">Start ETM Registration </span>
                                            <h5 class="card-title text-uppercase text-muted mb-0">No ETM has been registered yet</h5>
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
    <div class="row">
        <cc1:ModalPopupExtender ID="mpConfirmation" runat="server" PopupControlID="pnlConfirmation"
            CancelControlID="lbtnNoConfirmation" TargetControlID="Button4" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlConfirmation" runat="server" Style="position: fixed;">
            <div class="card" style="width: 350px;">
                <div class="card-header">
                    <h4 class="card-title text-left mb-0">Change Validity
                    </h4>
                </div>

                <div class="card-body text-left pt-2" style="min-height: 100px;">

                    <asp:Label ID="lblConfirmation" runat="server" Text="Do you want to save ?"></asp:Label>
                    <div style="width: 100%; margin-top: 20px; text-align: right;">
                        <asp:LinkButton ID="lbtnYesConfirmation" OnClick="lbtnYesConfirmation_Click" runat="server" CssClass="btn btn-sm btn-success btn-sm"> <i class="fa fa-check"></i> Yes </asp:LinkButton>
                        <asp:LinkButton ID="lbtnNoConfirmation" runat="server" CssClass="btn btn-sm btn-danger btn-sm ml-2"> <i class="fa fa-times"></i> No </asp:LinkButton>
                    </div>
                </div>
            </div>
            <div style="visibility: hidden;">
                <asp:Button ID="Button4" runat="server" Text="" />
            </div>
        </asp:Panel>
    </div>
    <div class="row">
        <cc1:ModalPopupExtender ID="mpETMAllot" runat="server" PopupControlID="pnlETMAllot"
            CancelControlID="Button5" TargetControlID="Button3" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlETMAllot" runat="server" Style="position: fixed; display: none">
            <center>
                <div class="card" style="width: 800px;">
                    <div class="card-header border-bottom">
                       <h3 class="mb-0">Update Status of ETM <asp:Label runat="server" ID="lblSerialNo" Text="N/A"></asp:Label></h3>
                    </div>
                    <div class="card-body text-left pt-2" style="min-height: 100px;height: 250px">
                       <div class="row">
                                <div class="col-lg-6 px-3" style="border-right: 2px solid #eee">

									<div class="row px-2 mb-3">
                                <div class="col-lg-12">
									 <span class="text-xs font-weight-normal text-gray">Agency</span><br />
										<asp:Label runat="server" ID="lblAgency" Text="N/A" CssClass="text-sm text-default"></asp:Label>
									</div>
										</div>


									<div class="row px-2 mb-3">
                                <div class="col-lg-6"><span class="text-xs font-weight-normal text-gray">Make Model</span><br />
									<asp:Label runat="server" ID="lblMakeModel" Text="N/A" CssClass="text-sm text-default"></asp:Label>								

                                </div>
                                <div class="col-lg-6">
										<span class="text-xs font-weight-normal text-gray">IMEI No</span><br />
										<asp:Label runat="server" ID="lblIMEINo" Text="N/A" CssClass="text-sm text-default"></asp:Label>
									</div>
										</div>
									<div class="row px-2 mb-3">
                                <div class="col-lg-6"><span class="text-xs font-weight-normal text-gray">Current Status</span><br />
										<asp:Label runat="server" ID="lblStatus" Text="N/A" CssClass="text-sm text-default"></asp:Label>
									</div>
                                <div class="col-lg-6">
									<span class="text-xs font-weight-normal text-gray">Current Status Date</span><br />
										<asp:Label runat="server" ID="lblStatusDate" Text="N/A" CssClass="text-sm text-default"></asp:Label>

									</div>
										</div>
									
										
                                </div>
                                <div class="col-lg-6 px-5">
                                    <div runat="server" id="divAllotment" visible="true">
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>

												<span class="text-xs font-weight-normal">Status</span>
												<asp:DropDownList ID="ddlETMStatus" runat="server" ToolTip="Office Level" AutoPostBack="true" OnSelectedIndexChanged="ddlETMStatus_SelectedIndexChanged" CssClass="form-control form-control-sm mb-2"
                                                                >
                                                            </asp:DropDownList>

												


												
												<span class="text-xs font-weight-normal">Remark</span>
												 <asp:TextBox runat="server" ID="tbRemark" CssClass="form-control mb-2" TextMode="MultiLine" Style="resize: none"></asp:TextBox>
                                                
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <center>
                                            <div style="width: 100%; margin-top: 20px;">
                                                <asp:LinkButton ID="lbtnAllotETM" runat="server" OnClick="lbtnAllotETM_Click" CssClass="btn btn-success btn-sm ml-2"> <i class="fa fa-check"></i> Save </asp:LinkButton>
                                                <asp:LinkButton ID="lbtnAllotCancel" runat="server" CssClass="btn btn-danger btn-sm ml-2"> <i class="fa fa-times"></i> Cancel </asp:LinkButton>
                                            </div>
                                        </center>
                                    </div>
                                </div>
                            </div>
                    </div>
                </div>

                <div style="visibility: hidden;">
                    <asp:Button ID="Button3" runat="server" Text="" />
                    <asp:Button ID="Button5" runat="server" Text="" />
                </div>
            </center>
        </asp:Panel>
    </div>
</asp:Content>

