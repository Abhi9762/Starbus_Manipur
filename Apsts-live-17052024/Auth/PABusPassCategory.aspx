<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Auth/PortalAdmmaster.master" AutoEventWireup="true" CodeFile="PABusPassCategory.aspx.cs" Inherits="Auth_PABusPassCategory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
       
         .modalBackground{
            background-color:black;
            opacity :0.6;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid pt-2">
        <div class="row align-items-center" style="padding-top: 20px">
            <div class="col-xl-12">
                <div class="card card-stats mb-3">
                    <div class="row">
                        <div class="col-4 border-right">
                            <div class="card-body">
                                <div class="row m-0">
                                    <div class="col-12">
                                        <h6 class="mb-1">
                                            <asp:Label ID="lblSummary" runat="server" CssClass="text-capitalize">Summary as on Date 01/12/2021 04:00pm</asp:Label></h6>
                                        <div class="col-12">
                                            <asp:Label runat="server" CssClass="card-title text-uppercase">Total</asp:Label>
                                            <asp:Label ID="lblTotal" runat="server" data-toggle="tooltip" data-placement="bottom" title="Total Bus Pass  Category" CssClass="font-weight-bold float-right" Text="0"></asp:Label>
                                        </div>
                                        <div class="col-12">
                                            <asp:Label runat="server" class="card-title text-uppercase">Active</asp:Label>
                                            <asp:Label ID="lblActivate" runat="server" data-toggle="tooltip" data-placement="bottom" title="Active Bus Pass Category" CssClass="font-weight-bold float-right" Text="0"></asp:Label>
                                        </div>
                                        <div class="col-12">
                                            <asp:Label runat="server" CssClass="card-title text-uppercase">Discontinue</asp:Label>
                                            <asp:Label ID="lblDeactive" runat="server" data-toggle="tooltip" data-placement="bottom" title="Deactive Bus Pass Category" class="font-weight-bold float-right" Text="0"></asp:Label>
                                        </div>
                                    </div>

                                </div>

                            </div>
                        </div>
                        <div class="col-4 border-right">
                            <div class="card-body">
                                <div class="row m-0">
                                    <div class="col-12">
                                        <h6 class="mb-1">
                                            <asp:Label runat="server" CssClass="text-capitalize">Generate Report</asp:Label></h6>
                                        <div class="mt-3">
                                            <label class="form-control-label">Select Bus Pass Category</label>
                                            <div class="input-group">
                                                <div class="input-group-prepend pr-2" style="width: 80%">

                                                    <asp:DropDownList ID="ddlBusPassCategory" ToolTip="Bus Pass Category Available" CssClass="form-control form-control-sm" Width="100%" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                                <asp:LinkButton ID="lbtndownloadReport" runat="server" ToolTip="Download Office Report"
                                                    CssClass="btn btn bg-gradient-green btn-sm text-white" Visible="false">
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
                                        <h6 class="mb-1 float-left">Instructions</h6>
                                        <div class="float-right">
                                            <asp:LinkButton ID="lbtnview" data-toggle="tooltip" OnClick="lbtnview_Click" data-placement="bottom" title="View Instructions" runat="server" Width="30px" CssClass="btn btn bg-gradient-orange btn-sm text-white">
                                    <i class="fa fa-info"></i>
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="lbtnHistory" runat="server" Visible="false" class="btn btn bg-gradient-green btn-sm text-white" Width="30px" data-toggle="tooltip" data-placement="bottom" title="History of Bus Pass Category">
                                            <i class="fa fa-history"></i>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="row m-0">
                                        <div class="col-12">
                                            <div class="row m-0">
                                                <asp:Label ID="lblinstruction" runat="server" CssClass="form-control-label">Bus Pass Category Name and remark can be edited if it is Active</asp:Label>
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
                <div class="card" style="min-height: 350px">
                    <div class="card-header">
                        <div class="row border-bottom m-0 p-0">
                           <div class="col-lg-3" style="text-align: right; padding-top: 14px;">
                                <p class="text-sm font-weight-bold text-primary">Action</p>

                            </div>
                            <div class="col-lg-3  m-0 p-0">
                                <a href="PABusPassCharges.aspx" class="btn btn-sm btn-secondary w-100">Bus Pass
                                    <br />
                                    Charges</a>
                            </div>
                            <div class="col-lg-3  m-0 p-0">
                                <h6>
                                    <a href="PABusPassCategory.aspx" class="btn btn-sm btn-primary w-100">Bus Pass
                                        <br />
                                        Categories</a>
                                </h6>
                            </div>
                            <div class="col-lg-3  m-0 p-0">
                                <a href="PABusPassTypes.aspx" class="btn btn-sm btn-secondary w-100">Bus Pass
                                    <br />
                                    Types</a>
                            </div>
                        </div>
                    </div>

                    <div class="card-body">
                        <div class="row">
                            <div class="col-lg-12">
                                <asp:GridView ID="gvBusPassCategories" runat="server" GridLines="None" ShowHeader="false" AutoGenerateColumns="false" Width="100%"
                                    AllowPaging="true" PageSize="5" OnPageIndexChanging="gvBusPassCategories_PageIndexChanging" OnRowCommand="gvBusPassCategories_RowCommand" OnRowDataBound="gvBusPassCategories_RowDataBound"
                                    DataKeyNames=" buspass_categoryid,buspass_categoryname,buspass_category_namelocal,remark_,currentstatus">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <div class="row pr-3 pl-3">
                                                    <div class="col-lg-9">

                                                        <p class="mb-0 text-xs">
                                                            <%# Eval("buspass_categoryname") %>
                                                     
                                                     
                                                        </p>
                                                    </div>

                                                    <div class="col-lg-3 text-right m-0 p-0">
                                                        <asp:LinkButton ID="lbtnUpdateBusPassCategory" Visible="true" runat="server" CommandName="updateBusPassCategory" CssClass="btn btn-sm btn-primary" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' ToolTip="Update Bus Pass Category Details"> <i class="fa fa-edit"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="lbtnDeactivate" Visible="true" runat="server" CommandName="Deactivate" CssClass="btn btn-sm btn-danger" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' ToolTip="Deactivate Bus Pass Category"> <i class="fa fa-ban"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="lbtnActivate" Visible="true" runat="server" CommandName="Activate" CssClass="btn btn-sm btn-success" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' ToolTip="Activate Bus Pass Category"> <i class="fa fa-check"></i></asp:LinkButton>
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
                                                    <span class="h6 font-weight-bold mb-0">Start Bus Pass category Creation </span>
                                                    <h5 class="card-title text-uppercase text-muted mb-0">No Bus Pass category has been created yet</h5>
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

                <div class="card" style="min-height: 350px">
                    <div class="card-header border-bottom">
                        <div class="float-left">
                            <h4 class="mb-0">New Bus Pass Category</h4>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row">


                            <div class="col-lg-12">

                                <div class="form-control-label">1. Name</div>
                                <div class="row border-bottom">

                                    <div class="col-lg-2">
                                        <asp:Label runat="server" CssClass="form-control-label">English<span style="color: red">*</span></asp:Label>
                                    </div>
                                    <div class="col-lg-4">
                                        <asp:TextBox ID="tbName" runat="server" CssClass="form-control form-control-sm" placeholder="Max 50 chars" MaxLength="50" AutoComplete="off" data-toggle="tooltip" data-placement="bottom" title="Category Type Name"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers,UppercaseLetters,LowercaseLetters,Custom" TargetControlID="tbName" ValidChars=" " />
                                    </div>
                                    <div class="col-lg-2">
                                        <asp:Label runat="server" CssClass="form-control-label">Local</asp:Label>
                                    </div>
                                    <div class="col-lg-4">
                                        <asp:TextBox ID="tbNameLocal" runat="server" CssClass="form-control form-control-sm" placeholder="Max 50 chars" MaxLength="50" AutoComplete="off" data-toggle="tooltip" data-placement="bottom" title="Category Type Name(Local)"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row my-2">
                                    <div class="col-lg-2">
                                        <asp:Label runat="server" CssClass="form-control-label">2. Remark<span style="color: red">*</span></asp:Label>
                                    </div>
                                    <div class="col-lg-10">
                                        <asp:TextBox ID="tbRemark" runat="server" TextMode="MultiLine" Height="60px" Style="resize: none" CssClass="form-control form-control-sm" placeholder="Max 200 chars" MaxLength="200" AutoComplete="off" data-toggle="tooltip" data-placement="bottom" title="Category Type Remark"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers,UppercaseLetters,LowercaseLetters,Custom" TargetControlID="tbRemark" ValidChars="  ," />
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-lg-12 text-center">
                                        <asp:LinkButton ID="lbtnUpdate" runat="server" class="btn btn-success" OnClick="lbtnUpdate_Click" Visible="false" data-toggle="tooltip" data-placement="bottom" title="Update Concession Details">
                                    <i class="fa fa-save"></i>&nbsp; Update</asp:LinkButton>
                                        <asp:LinkButton ID="lbtnSave" Visible="true" runat="server" class="btn btn-success" OnClick="lbtnSave_Click" data-toggle="tooltip" data-placement="bottom" title="Save Concession Details">
                                    <i class="fa fa-save"></i>&nbsp; Save</asp:LinkButton>
                                        <asp:LinkButton ID="lbtnReset" runat="server" CssClass="btn btn-danger" OnClick="lbtnReset_Click" data-toggle="tooltip" data-placement="bottom" title="Reset Concession Details">
                                    <i class="fa fa-undo"></i>&nbsp; Reset</asp:LinkButton>
                                        <asp:LinkButton ID="lbtnCancel" Visible="false" runat="server" OnClick="lbtnCancel_Click" CssClass="btn btn-danger" data-toggle="tooltip" data-placement="bottom" title="Cancel Concession Details">
                                    <i class="fa fa-times"></i>&nbsp; Cancel</asp:LinkButton>
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
                        <h6 class="card-title text-left mb-0">Please Confirm
                        </h6>
                    </div>
                    <div class="card-body text-left pt-2" style="min-height: 100px;">
                        <asp:Label ID="lblConfirmation" runat="server" Text="Do you want to save ?"></asp:Label>
                        <div style="width: 100%; margin-top: 20px; text-align: right;">
                            <asp:LinkButton ID="lbtnYesConfirmation" runat="server" Onclick="lbtnYesConfirmation_Click" CssClass="btn btn-success btn-sm"> <i class="fa fa-check"></i> Yes </asp:LinkButton>
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
                        <h6 class="card-title text-left mb-0">
                            <asp:Label ID="Label1" runat="server" Text="Please Check & Correct"></asp:Label>
                        </h6>
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
                        <h6 class="card-title">Confirm
                        </h6>
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
                        <h6 class="card-title text-left mb-0">About Module
                        </h6>
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
    </div>
</asp:Content>


