<%@ Page Title="" Language="C#" MasterPageFile="~/traveller/trvlrMaster.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeFile="grievance.aspx.cs" Inherits="traveller_grievance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function uploadPic1(fileUpload) {
            if ($('#fuPic1').value != '') {
                document.getElementById("<%=btnfuPic1.ClientID %>").click();
            }
        }
        function uploadPic2(fileUpload) {
            if ($('#fuPic2').value != '') {
                document.getElementById("<%=btnfuPic2.ClientID %>").click();
            }
        }
    </script>
     <style>
        .ModalPopupBG {
            background-color: #666699;
            filter: alpha(opacity=50);
            opacity: 0.7;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <asp:HiddenField ID="hidtoken" runat="server" />
    <div class="container-fluid mt-3">
        <div class="row">
            <div class="col-xl-5 order-xl-1">
                <div class="card card-profile">
                    <div class="card-header">
                        <div class="row align-items-center">
                            <div class="col-12">
                                <h3 class="mb-0">Register Your Grievance</h3>
                            </div>
                        </div>
                    </div>
                    <div class="card-body" style="">
                        <div class="row px-2 py-2">
                            <div class="col-xl-6">
                                <label class="text-sm">Issue Reported</label>
                                <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control form-control-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="col-xl-6">
                                <label class="text-sm">Want to report upon</label>
                                <asp:DropDownList ID="ddlSubCategory" runat="server" CssClass="form-control form-control-sm">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row px-2 py-2">
                            <div class="col-xl-6 col-md-6">
                                <asp:Label ID="lblBus_Ticket_No_header" runat="server" Text="Bus No" CssClass="text-sm"></asp:Label>
                                <asp:TextBox ID="tbBusTicketNo" runat="server" MaxLength="20" CssClass="form-control form-control-sm" placeholder="" AutoComplete="off"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row px-2 py-2">
                            <div class="col-xl-12 col-md-12">
                                <label class="text-sm">Description</label>
                                <asp:TextBox ID="tbDescription" runat="server" MaxLength="200" CssClass="form-control form-control-sm" placeholder="Minimum 10 characters and Maximum 200 characters" TextMode="MultiLine" AutoComplete="off"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row px-2 py-2">
                            <div class="col-xl-6 col-md-6">
                                <div class="form-group mb-2">
                                    <label class="text-sm">Pic 1</label>
                                    <asp:FileUpload ID="fuPic1" runat="server" CssClass="form-control" onchange="uploadPic1(this);" accept="image/*" />
                                    <asp:Button ID="btnfuPic1" runat="server" OnClick="btnfuPic1_Click" CausesValidation="False" Style="display: none"
                                        Text="Upload Image" />
                                </div>
                                <asp:Image ID="imgPic1" runat="server" Visible="false" Style="width: 100px; height: 100px; border: 1px solid; border-radius: 50%;" />
                            </div>
                            <div class="col-xl-6 col-md-6">
                                <div class="form-group mb-2">
                                    <label class="text-sm">Pic 2</label>
                                    <asp:FileUpload ID="fuPic2" runat="server" CssClass="form-control" onchange="uploadPic2(this);" accept="image/*" />
                                    <asp:Button ID="btnfuPic2" runat="server" OnClick="btnfuPic2_Click" CausesValidation="False" Style="display: none"
                                        Text="Upload Image" />
                                </div>
                                <asp:Image ID="imgPic2" runat="server" Visible="false" Style="width: 100px; height: 100px; border: 1px solid; border-radius: 50%;" />
                            </div>
                        </div>
                    </div>
                    <div class="card-footer text-right py-2">
                        <asp:LinkButton ID="lbtnReset" runat="server" CssClass="btn btn-danger mr-2" OnClick="btnReset_Click"><i class="fa fa-times"></i> Reset</asp:LinkButton>
                        <asp:LinkButton ID="lbtnProceed" runat="server" CssClass="btn btn-success" OnClick="btnProceed_Click"><i class="fa fa-check"></i> Proceed</asp:LinkButton>
                    </div>
                </div>
            </div>
            <div class="col-xl-7 order-xl-2">
                <div class="card">
                    <div class="card-header">
                        <div class="row align-items-center">
                            <div class="col-12">
                                <h3 class="mb-0">Last Grievances </h3>
                            </div>
                        </div>
                    </div>
                    <div class="card-body" style="min-height: 70vh !important;">
                        <asp:GridView ID="gvTickets" runat="server" GridLines="None" ShowHeader="false" AutoGenerateColumns="false" Width="100%"
                            DataKeyNames="refno,catid,catname,subcatid,subcatname,remark, userid, status, datetime,assign_to, bus_no, ticketno"
                            OnRowCommand="gvTickets_RowCommand" OnPageIndexChanging="gvTickets_PageIndexChanging" AllowPaging="true" PageSize="6">
                            <PagerStyle CssClass="pagination-ys" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <div class="row px-2 shadow-lg--hover border-bottom pb-2 mb-2">
                                            <div class="col">
                                                <span class="h3 font-weight-bold mb-0"><%# Eval("catname") %> - <%# Eval("subcatname") %> <span class="mb-0 text-xs text-muted">(<%# Eval("datetime") %>)</span></span>
                                                <h5 class="card-title text-xs text-uppercase text-muted mb-0"><%# Eval("remark") %></h5>
                                            </div>
                                            <div class="col-auto text-right ">
                                                <h5 class="card-title text-xs text-uppercase text-muted mb-0">Ref No. <%# Eval("refno") %></h5>
                                                <asp:LinkButton ID="lbtnView" runat="server" CssClass="btn btn-icon btn-primary btn-sm"
                                                    CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="VIEWDETAIL" data-toggle="tooltip" data-placement="bottom" title="View Details">
                                                    <i class="fa fa-eye"></i>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:Panel ID="pnlNoGrivance" runat="server">
                            <div class="row py-4">
                                <div class="col-lg-12 text-center mt-4">
                                    <i class="ni ni-bus-front-12 ni-5x text-muted"></i>
                                </div>
                                <div class="col-lg-12 text-center mt-4">
                                    <span class="h2 font-weight-bold mb-0">Grievance History</span>
                                    <h5 class="card-title text-uppercase text-muted mb-0">There is no grievance registered by you.
                                    </h5>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <cc1:ModalPopupExtender ID="mpConfirmation" runat="server" PopupControlID="pnlConfirmation"
                CancelControlID="lbtnNoConfirmation" TargetControlID="Button4" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlConfirmation" runat="server" Style="position: fixed;">
                <div class="card" style="min-width: 350px; max-width: 850px;">
                    <div class="card-header">
                        <h4 class="card-title text-left mb-0">Please Confirm
                        </h4>
                    </div>
                    <div class="card-body text-left pt-2" style="min-height: 100px;">
                        <asp:Label ID="lblConfirmation" runat="server" CssClass="text-uppercase" Text="Do you want to save ?"></asp:Label>
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
        <div class="row">
        <cc1:ModalPopupExtender ID="mpGrievance" runat="server" PopupControlID="pnlmpGrievance" TargetControlID="btnOpenmpGrievance"
            CancelControlID="lbtnClosempGrievance" BackgroundCssClass="ModalPopupBG">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlmpGrievance" runat="server" Style="position: fixed;">
            <div class="modal-content  modal-dialog mt-3" style="width: 90vw;">
                <div class="card">
                    <div class="card-header py-3">
                        <div class="row">
                            <div class="col">
                                <h3 class="m-0">Grievance</h3>
                            </div>
                            <div class="col-auto">
                                <asp:LinkButton ID="lbtnClosempGrievancee" runat="server" OnClick="lbtnClosempGrievancee_Click" Style="color: Red; cursor: pointer;"><i class="fa fa-times fa-2x"></i></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="modal-body p-2">
                  <%--     <embed src = "dashGrievance.aspx" style="height: 80vh; width: 100%"  />--%>
                         <asp:Literal id="eDash" runat="server" ></asp:Literal>
                    </div>
                </div>
            </div>
            <br />
            <div style="visibility: hidden;">
                <asp:Button ID="btnOpenmpGrievance" runat="server" Text="" />
                <asp:Button ID="lbtnClosempGrievance" runat="server" Text="" />
            </div>
        </asp:Panel>
    </div>
    </div>
</asp:Content>

