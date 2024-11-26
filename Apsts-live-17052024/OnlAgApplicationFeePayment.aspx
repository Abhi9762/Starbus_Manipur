<%@ Page Title="" Language="C#" MasterPageFile="~/mainMasterPage.master" AutoEventWireup="true" CodeFile="OnlAgApplicationFeePayment.aspx.cs" Inherits="OnlAgApplicationFeePayment" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .table th, .table td {
            padding: 0.5rem !important;
        }

        .table th {
            background: #e6e6e6;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="ftco-section" style="min-height: 70vh; padding-top: 4em;">
        <div class="container">
            <asp:HiddenField ID="hidtoken" runat="server" />
            <div class="row">

                <div class="col-md-12 text text-dark px-4 py-3">
                    <div class="row">
                        <div class="col-md">
                            <h3 class="mb-0">SECURITY FEE PAYMENT</h3>
                       <p><b>To become Arunachal Pradesh State Transport Agent</b></p>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="row">
                            <div class="col-md-12 pt-3">
                                <h3>Applicant Details</h3>
                                <span style="font-size: 15px;">Reference No. </span>
                                <asp:Label ID="lblReferenceNo" runat="server" Style="font-size: 12px;" Font-Bold="true" Text=""></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 ">
                                <asp:GridView ID="grvRequest" runat="server" AutoGenerateColumns="False" GridLines="None" AllowSorting="true" AllowPaging="true" ShowHeader="true"
                                    Font-Size="10pt" class="table table-bordered table-hover">
                                    <Columns>

                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAGENTNAME" runat="server" Text='<%# Eval("agent_name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PAN No.">
                                            <ItemTemplate>
                                                <asp:Label ID="Label6" runat="server" Text='<%# Eval("pan_no") %>' Style="text-transform: capitalize;" Font-Bold="true"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Mobile">
                                            <ItemTemplate>
                                                <i class="fa fa-mobile"></i>
                                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("mobile_no") %>' Style="text-transform: capitalize;"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Email">
                                            <ItemTemplate>
                                                <i class="fa fa-envelope ml-2"></i>
                                                <asp:Label ID="Label5" runat="server" Text='<%# Eval("val_email") %>' Style="text-transform: capitalize;"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Address">
                                            <ItemTemplate>
                                                <asp:Label ID="lblADDRESS" runat="server" Font-Bold="true" Text='<%# Eval("val_address") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 ">
                                <p class="mb-0" style="line-height: 15px;">
                                    <asp:Label ID="Label7" runat="server" Text="Amount to Pay" Style="font-size: 15px;"></asp:Label>
                                </p>
                                <asp:Label ID="lblSecurityAmt" runat="server" CssClass="font-weight-bold "></asp:Label>&nbsp;<i class="icon icon-rupee"></i>
                            </div>
                        </div>
                        <hr />
                        <h5 class="text-left" style="margin-bottom: 5px;">Payment (All Banks Credit/Debit Card accepted)</h5>
                        <div class="card-body" style="margin-top: 10px;">
                            <div class="col-md-12">
                                <asp:Label ID="lblNoPG" runat="server" Text="Payment Gateways are not available.<br>Please contact to helpdesk." Visible="false" Style="font-size: 23px; color: #919691; font-weight: bold;"></asp:Label>
                            </div>
                            <div class="row pt-3">
                                <asp:Repeater ID="rptrPG" runat="server" OnItemCommand="rptrPG_ItemCommand">
                                    <ItemTemplate>
                                        <div class="col-md-6">
                                            <asp:HiddenField ID="rptHdPGId" runat="server" Value='<%# Eval("gateway_id") %>' />
                                                <asp:HiddenField ID="hd_pgurl" runat="server" Value='<%# Eval("req_url") %>' />
                                            <img src="../Dbimg/PG/<%# Eval("gateway_name")%>_W.png" style="height: 30px;" />
                                            <h4 class="mt-2 mb-1">
                                                <%# Eval("gateway_name")%>
                                            </h4>
                                            <p>
                                                <%# Eval("description")%>
                                            </p>
                                            <asp:Button ID="btnpaytm" runat="server" Text="Proceed to Pay" CssClass="btn mb-2"
                                                Style="background-color: #00baf2; color: White; font-weight: bold;" CommandName="PAYNOW" />
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div style="visibility: hidden;">
                    <asp:Button ID="Button3" runat="server" Text="" />
                </div>
                <cc1:ModalPopupExtender ID="mpError" runat="server" PopupControlID="pnlError" CancelControlID="LinkButton1"
                    TargetControlID="Button3" BackgroundCssClass="modalBackground">
                </cc1:ModalPopupExtender>
                <asp:Panel ID="pnlError" runat="server" Style="position: fixed;">
                    <div class="modal-content" style="min-width: 400px; background: white; border-radius: 4px;">
                        <div class="modal-header">
                            <h4 class="mb-0">Please Check & Correct</h4>
                        </div>
                        <div class="modal-body text-left" style="min-height: 100px;">
                            <asp:Label ID="lblerrmsg" runat="server" ForeColor="Black" Style="font-size: 17px; line-height: 23px;"></asp:Label>
                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-success"> OK </asp:LinkButton>
                        </div>
                    </div>

                </asp:Panel>
            </div>
                  <div class="row">
            <!-- Modal -->
            <cc1:ModalPopupExtender ID="mpconfirmation" runat="server" PopupControlID="pnlconfirmation"
                TargetControlID="Button1345" CancelControlID="lbtnno"
                BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlconfirmation" runat="server" Style="position: fixed; display: none;">
                <div class="modal-dialog">
                    <div class="modal-content " style="min-width: 400px;">
                        <div class="modal-header">
                            <div class="col-md-10">
                                <h3 class="m-0">
                                    <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="Please Confirm"></asp:Label>
                                </h3>
                            </div>
                        </div>
                        <div class="modal-body">
                            <p class="full-width-separator mb-2" style="font-size: 17px; line-height: 24px;">
                                <asp:HiddenField ID="hdpgid" runat="server" />
                                <asp:HiddenField ID="hdpgurl" runat="server" />
                                <asp:Label ID="lblmsg" runat="server"></asp:Label>
                            </p>

                        </div>
                        <div class="modal-footer text-right">
                            <div class="row w-100">
                                <div class="col-lg-6 mt-1">
                                    <h4 class="text-danger">Do you want to proceed ?</h4>
                                </div>
                                <div class="col-lg-4 text-right">
                                    <asp:LinkButton ID="lbtnyes" OnClick="lbtnyes_Click" runat="server" UseSubmitBehavior="false" CssClass="btn btn-success btn-sm"
                                        Style="cursor: pointer; font-weight: bold; font-size: 10pt;"><i class="fa fa-check"></i> Yes</asp:LinkButton>
                                    <asp:LinkButton ID="lbtnno" runat="server" UseSubmitBehavior="false" CssClass="btn btn-danger btn-sm"
                                        Style="cursor: pointer; font-weight: bold; font-size: 10pt;"><i class="fa fa-times"></i> No</asp:LinkButton>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
                <br />
                <div style="visibility: hidden;">
                    <asp:Button ID="Button1345" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>
        </div>
    </section>
</asp:Content>



