<%@ Page Title="" Language="C#" MasterPageFile="~/traveller/trvlrMaster.master" AutoEventWireup="true" CodeFile="travellerPmtError.aspx.cs" Inherits="traveller_travellerPmtError" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="main-panel">
        <div class="content">
            <div class="panel-header bg-primary-gradient">
                <div class="row" style="height: 65px;">
                </div>
            </div>
            <div class="page-inner mt--5">
                <div class="row mt--2">
                    <div class="col-md-12">
                        <div class="card">
                            <div class="card-header">
                                <div class="card-title">
                                    Payment Failed <i class="fa fa-info-circle" style="font-size: 15px; color: #c6c2c2;"
                                        data-toggle="tooltip" data-placement="top" title="Your Payment Transaction failed"></i>
                                </div>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-12 text-center pt-3">
                                        <i class="fa fa-exclamation-triangle text-danger" style="font-size: 125px;"></i>
                                        <p class="text-danger mb-0 mt-4" style="font-size:22px;">Transaction rejected/not completed by Payment Gateway.</p>
                                        <p class="text-danger mb-0" style="font-size:22px;">Please try again.</p>
                                        <p class="text-danger mb-0" style="font-size:22px;">If any Amount deducted, please contact your Bank.</p>

                                        <asp:Label ID="lblerrormsg" runat="server" CssClass="lblMessage" Font-Bold="True"
                                            Font-Size="19pt" Style="color: #c85353; padding: 35px;" Text="Transaction  rejected/not completed by Payment Gateway. Please try again.<br/> If any Amount deducted, please contact your Bank."></asp:Label>
                                    </div>
                                </div>
                                <br />
                                <br />

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- Modal -->
    </div>
</asp:Content>

