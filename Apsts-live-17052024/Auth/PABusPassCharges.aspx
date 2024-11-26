<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Auth/PortalAdmmaster.master" AutoEventWireup="true" CodeFile="PABusPassCharges.aspx.cs" Inherits="Auth_PABusPassCharges" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .modalBackground {
            background-color: black;
            opacity: 0.6;
        }
  </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid" style="padding-top: 20px">
        <div class="row align-items-center">
            <div class="col-xl-12">
                <div class="card card-stats mb-3">
                    <div class="row">
                        <div class="col-4 border-right">
                            <div class="card-body">
                                <div class="row m-0">
                                    <div class="col-12">
                                        <h5 class="mb-1">
                                            <asp:Label ID="lblSummary" runat="server" CssClass="text-capitalize">Summary as on Date 01/12/2021 04:00pm</asp:Label></h5>
                                        <div class="col-12">
                                            <asp:Label runat="server" CssClass="card-title text-uppercase">Total</asp:Label>
                                            <asp:Label ID="lblTotal" runat="server" data-toggle="tooltip" data-placement="bottom" title="Total Bus Pass Charges" CssClass="font-weight-bold float-right" Text="0"></asp:Label>
                                        </div>
                                        <div class="col-12">
                                            <asp:Label runat="server" class="card-title text-uppercase">Active</asp:Label>
                                            <asp:Label ID="lblActivate" runat="server" data-toggle="tooltip" data-placement="bottom" title="Active Bus Pass Charges" CssClass="font-weight-bold float-right" Text="0"></asp:Label>
                                        </div>
                                        <div class="col-12">
                                            <asp:Label runat="server" CssClass="card-title text-uppercase">Discontinue</asp:Label>
                                            <asp:Label ID="lblDeactive" runat="server" data-toggle="tooltip" data-placement="bottom" title="Deactive Bus Pass Charges" class="font-weight-bold float-right" Text="0"></asp:Label>
                                        </div>
                                    </div>

                                </div>

                            </div>
                        </div>
                        <div class="col-4 border-right">
                            <div class="card-body">
                                <div class="row m-0">
                                    <div class="col-12">
                                        <h5 class="mb-1">
                                            <asp:Label runat="server" CssClass="text-capitalize">Generate Report</asp:Label></h5>
                                        <div class="mt-3">
                                            <label class="form-control-label">Select Bus Pass Charges Type</label>
                                            <div class="input-group">
                                                <div class="input-group-prepend pr-2" style="width: 80%">

                                                    <asp:DropDownList ID="ddlBusPassCharges" ToolTip="Bus Pass Charges Available" CssClass="form-control form-control-sm" Width="100%" runat="server">
                                                        <asp:ListItem Value="0" Text="Select"></asp:ListItem>

                                                    </asp:DropDownList>
                                                </div>
                                                <asp:LinkButton ID="lbtndownloadReport" runat="server" Visible="false"  ToolTip="Download Office Report"
                                                    CssClass="btn btn bg-gradient-green btn-sm text-white">
                                             <i class="fa fa-download"></i>
                                                </asp:LinkButton>
                                            </div>

                                        </div>



                                    </div>

                                </div>

                            </div>
                        </div>

                        <div class="col-4">
                            <div class="card-body">
                                <div class="row mr-0">
                                    <div class="row m-0 col-12" style="display: inline-block">
                                        <h5 class="mb-1 float-left">Instructions</h5>
                                        <div class="float-right">
                                            <asp:LinkButton ID="lbtnview" data-toggle="tooltip" OnClick="lbtnview_Click" data-placement="bottom" title="View Instructions" runat="server" Width="30px" CssClass="btn btn bg-gradient-orange btn-sm text-white">
                                    <i class="fa fa-info"></i>
                                            </asp:LinkButton>

                                        </div>
                                    </div>
                                    <div class="row m-0">
                                        <div class="col-12">
                                            <div class="row m-0">
                                                <asp:Label ID="lblinstruction" runat="server" CssClass="form-control-label">Bus Pass Charges name,remark,abbrevation cannot be edited after creation</asp:Label>
                                            </div>
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
                <div class="card" style="min-height: 400px">
                    <div class="card-header">
                        <div class="row border-bottom m-0 p-0">
                            <div class="col-lg-3" style="text-align: right; padding-top: 14px;">
                                <p class="text-sm font-weight-bold text-primary">Action</p>

                            </div>
                            <div class="col-lg-3  m-0 p-0">
                                <a href="PABusPassCharges.aspx" class="btn btn-sm btn-primary w-100">Bus Pass<br />
                                    Charges</a>
                            </div>
                            <div class="col-lg-3  m-0 p-0">
                                <h6>
                                    <a href="PABusPassCategory.aspx" class="btn btn-sm btn-secondary w-100">Bus Pass<br />
                                        Categories</a>
                                </h6>
                            </div>
                            <div class="col-lg-3  m-0 p-0">
                                <a href="PABusPassTypes.aspx" class="btn btn-sm btn-secondary w-100">Bus Pass<br />
                                    Types</a>
                            </div>
                        </div>

                    </div>
                    <div class="card-body">

                        <div class="row">
                            <div class="col-lg-12">
                                <asp:GridView ID="gvBusPassCharges" runat="server" GridLines="None" ShowHeader="false" AutoGenerateColumns="false" Width="100%"
                                    AllowPaging="true" PageSize="5" OnRowCommand="gvBusPassCharges_RowCommand" OnRowDataBound="gvBusPassCharges_RowDataBound"
                                    DataKeyNames="charge_typeid,chargetype_name,chargetype_name_local,chargetype_abbr,charge_type_abbrlocal,remarks,chargeamt,currentstatus" OnPageIndexChanging="gvBusPassCharges_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <div class="row pr-3 pl-3">
                                                    <div class="col-lg-9">
                                                        <p class="mb-0 text-xs">
                                                            <%# Eval("chargetype_name") %>(<%# Eval("chargetype_abbr") %>)
                                                        <br />
                                                            <span class="text-gray text-xs">Charge Amount-
                                                          ₹  <%# Eval("chargeamt") %>
                                                            </span>
                                                        </p>
                                                    </div>

                                                    <div class="col-lg-3 text-right m-0 p-0">
                                                        <asp:LinkButton ID="lbtnUpdateBusPassCharges" Visible="true" runat="server" CommandName="updateBusPassCharges" CssClass="btn btn-sm btn-primary" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' ToolTip="Update Bus Pass Charges Details"> <i class="fa fa-edit"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="lbtnDeactivate" Visible="true" runat="server" CommandName="Deactivate" CssClass="btn btn-sm btn-danger" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' ToolTip="Deactivate Bus Pass Charges"> <i class="fa fa-ban"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="lbtnActivate" Visible="true" runat="server" CommandName="Activate" CssClass="btn btn-sm btn-success" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' ToolTip="Activate Bus Pass Charges"> <i class="fa fa-check"></i></asp:LinkButton>

                                                    </div>
                                                </div>
                                                <hr />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                                </asp:GridView>
                                <asp:Panel ID="pnlnoRecordfound" runat="server">
                                    <div class="card card-stats">
                                        <div class="card-body">
                                            <div class="row">
                                                <div class="col-lg-12 text-center mt-4" runat="server" id="div1">
                                                    <span class="h6 font-weight-bold mb-0">Start Bus Pass Charges Creation</span>
                                                    <h5 class="card-title text-uppercase text-muted mb-0">No Bus Pass Charges has been created yet</h5>
                                                </div>
                                                <div class="col-lg-12 text-center mt-4" runat="server" id="div2" visible="false">
                                                    <span class="h6 font-weight-bold mb-0">No Record Found </span>
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

                <div class="card" style="min-height: 400px">
                    <div class="card-header border-bottom">
                        <div class="float-left">
                            <h4 class="mb-0">New Bus Pass Charges</h4>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-lg-12">
                                <label class="form-control-label text-md">1. Name</label>
                            </div>
                            <div class="col-lg-12">
                                <div class="row border-bottom">
                                    <div class="col-lg-2">
                                        <asp:Label runat="server" CssClass="form-control-label text-right">English<span style="color: red">*</span></asp:Label>
                                    </div>
                                    <div class="col-lg-4">
                                        <asp:TextBox ID="tbName" runat="server" CssClass="form-control form-control-sm" placeholder="Max 50 chars" MaxLength="50" AutoComplete="off" data-toggle="tooltip" data-placement="bottom" title="Charge Type Name"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers,UppercaseLetters,LowercaseLetters,Custom" TargetControlID="tbName" ValidChars=" " />
                                    </div>
                                    <div class="col-lg-2">
                                        <asp:Label runat="server" CssClass="form-control-label">Local</asp:Label>
                                    </div>
                                    <div class="col-lg-4">
                                        <asp:TextBox ID="tbNameLocal" runat="server" CssClass="form-control form-control-sm" placeholder="Max 50 chars" MaxLength="50" AutoComplete="off" data-toggle="tooltip" data-placement="bottom" title="Charge Type Name(Local)"></asp:TextBox>
                                    </div>
                                </div>

                                <label class="form-control-label text-md">2. Abbreviation</label>
                                <div class="row my-2">
                                    <div class="col-lg-2">
                                        <asp:Label runat="server" CssClass="form-control-label">English<span style="color: red">*</span></asp:Label>
                                    </div>
                                    <div class="col-lg-4">
                                        <asp:TextBox ID="tbAbbreviation" runat="server" CssClass="form-control form-control-sm text-uppercase" placeholder="Max 10 chars" MaxLength="10" AutoComplete="off" data-toggle="tooltip" data-placement="bottom" title="Charge Type Abbreviation"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender runat="server" FilterType="UppercaseLetters,LowercaseLetters" TargetControlID="tbAbbreviation" ValidChars=" " />

                                    </div>
                                    <div class="col-lg-2">
                                        <asp:Label runat="server" CssClass="form-control-label">Local</asp:Label>
                                    </div>
                                    <div class="col-lg-4">
                                        <asp:TextBox ID="tbAbbrLocal" runat="server" CssClass="form-control form-control-sm" placeholder="Max 10 chars" MaxLength="10" AutoComplete="off" data-toggle="tooltip" data-placement="bottom" title="Charge Type Abbreviation"></asp:TextBox>

                                    </div>

                                </div>


                                <div class="row my-2">
                                    <div class="col-lg-2">
                                        <asp:Label runat="server" CssClass="form-control-label">3.Remark<span style="color: red">*</span></asp:Label>
                                    </div>
                                    <div class="col-lg-10">
                                        <asp:TextBox ID="tbRemark" runat="server" TextMode="MultiLine" Height="60px" Style="resize: none" CssClass="form-control form-control-sm" placeholder="Max 200 chars" MaxLength="200" AutoComplete="off" data-toggle="tooltip" data-placement="bottom" title="Charge Type Remark"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers,UppercaseLetters,LowercaseLetters,Custom" TargetControlID="tbRemark" ValidChars=" " />
                                    </div>

                                </div>

                                <div class="row my-2">
                                    <div class="col-lg-2">
                                        <asp:Label runat="server" CssClass="form-control-label">Charge Amount (₹)<span style="color: red">*</span></asp:Label>
                                    </div>
                                    <div class="col-lg-4">

                                        <div class="row m-0">
                                            <div class="col-lg-6">
                                                <asp:TextBox ID="tbChargeAmt" runat="server" CssClass="form-control form-control-sm" placeholder="Max 5 digits" MaxLength="5" AutoComplete="off" data-toggle="tooltip" data-placement="bottom" title="Charge Amount"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers" TargetControlID="tbChargeAmt" ValidChars=" " />
                                            </div>
                                            <div class="col-lg-2 pr-0 text-left">
                                                Rs.
                                            </div>
                                        </div>

                                    </div>
                                </div>


                                <div class="row">
                                    <div class="col-lg-12 text-center">
                                        <asp:LinkButton ID="lbtnUpdate" runat="server" class="btn btn-success" Visible="false" OnClick="lbtnUpdate_Click" data-toggle="tooltip" data-placement="bottom" title="Update Bus Pass Charges Details">
                                    <i class="fa fa-save"></i>&nbsp; Update</asp:LinkButton>
                                        <asp:LinkButton ID="lbtnSave" Visible="true" runat="server" OnClick="lbtnSave_Click" class="btn btn-success" data-toggle="tooltip" data-placement="bottom" title="Save Bus Pass Charges Details">
                                    <i class="fa fa-save"></i>&nbsp; Save</asp:LinkButton>
                                        <asp:LinkButton ID="lbtnReset" runat="server" OnClick="lbtnReset_Click" CssClass="btn btn-danger" data-toggle="tooltip" data-placement="bottom" title="Reset Bus Pass Charges Details">
                                    <i class="fa fa-undo"></i>&nbsp; Reset</asp:LinkButton>
                                        <asp:LinkButton ID="lbtnCancel" Visible="false" OnClick="lbtnCancel_Click" runat="server" CssClass="btn btn-danger" data-toggle="tooltip" data-placement="bottom" title="Cancel Bus Pass Charges Details">
                                    <i class="fa fa-times"></i>&nbsp; Cancel</asp:LinkButton>
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
                        <asp:LinkButton ID="lbtnYesConfirmation" runat="server" OnClick="lbtnYesConfirmation_Click" CssClass="btn btn-success btn-sm"><i class="fa fa-check"></i> Yes </asp:LinkButton>
                        <asp:LinkButton ID="lbtnNoConfirmation" runat="server" CssClass="btn btn-danger btn-sm ml-2"> <i class="fa fa-times"></i> No </asp:LinkButton>
                    </div>
                </div>
            </div>
            <div style="visibility: hidden;">
                <asp:Button ID="Button4" runat="server" Text="" />
            </div>
        </asp:Panel>
    </div>

    <div class="row">
        <cc1:ModalPopupExtender ID="mperror" runat="server" PopupControlID="pnlerror" CancelControlID="lbtnclose1"
            TargetControlID="Button2" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlerror" runat="server" Style="position: fixed;">
            <div class="card" style="min-width: 350px;">
                <div class="card-header">
                    <h4 class="card-title text-left mb-0">
                        <asp:Label ID="Label1" runat="server" Text="Please Check & Correct"></asp:Label>
                    </h4>
                </div>
                <div class="card-body text-left pt-2" style="min-height: 100px;">
                    <asp:Label ID="lblerrormsg" runat="server" Text="Do you want to save Route ?"></asp:Label>
                    <div style="width: 100%; margin-top: 20px; text-align: right;">
                        <asp:LinkButton ID="lbtnclose1" runat="server" CssClass="btn btn-danger btn-sm ml-2"> <i class="fa fa-times"></i> OK </asp:LinkButton>
                    </div>
                </div>
            </div>
            <div style="visibility: hidden;">
                <asp:Button ID="Button2" runat="server" Text="" />
            </div>
        </asp:Panel>
    </div>

    <div class="row">
        <cc1:ModalPopupExtender ID="mpsuccess" runat="server" PopupControlID="pnlsuccess"
            CancelControlID="lbtnsuccessclose1" TargetControlID="Button6" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlsuccess" runat="server" Style="position: fixed;">
            <div class="card" style="min-width: 350px;">
                <div class="card-header">
                    <h4 class="card-title">Confirm
                    </h4>
                </div>
                <div class="card-body" style="min-height: 100px;">
                    <asp:Label ID="lblsuccessmsg" runat="server"></asp:Label>
                    <div style="width: 100%; margin-top: 20px; text-align: right;">
                        <asp:LinkButton ID="lbtnsuccessclose1" runat="server" CssClass="btn btn-success"> <i class="fa fa-check"></i> OK </asp:LinkButton>
                    </div>
                </div>
            </div>
            <div style="visibility: hidden;">
                <asp:Button ID="Button6" runat="server" Text="" />
            </div>
        </asp:Panel>
    </div>

    <div class="row">
        <cc1:ModalPopupExtender ID="mpInfo" runat="server" PopupControlID="PanelInfo" CancelControlID="LinkButtonMpInfoClose"
            TargetControlID="lbtnview" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="PanelInfo" runat="server" Style="position: fixed;">
            <div class="card" style="min-width: 350px;">
                <div class="card-header">
                    <h4 class="card-title text-left mb-0">About Module
                    </h4>
                </div>
                <div class="card-body text-left pt-2" style="min-height: 100px;">
                    <ol>
                        <li>Here you can Add/Update Bus Service Type and its other details like charges and speed limit.</li>
                        <li>Service Type Fare type wise details will be added here along with effective date.</li>
                        <li>We can discontinue service type if it's currently not in use.</li>
                    </ol>
                    <div style="width: 100%; margin-top: 20px; text-align: right;">
                        <asp:LinkButton ID="LinkButtonMpInfoClose" runat="server" CssClass="btn btn-success btn-sm ml-2"> <i class="fa fa-check"></i> OK </asp:LinkButton>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>

