<%@ Page Title="" Language="C#" MasterPageFile="~/traveller/trvlrMaster.master" AutoEventWireup="true" CodeFile="walletTopupStatus.aspx.cs" Inherits="traveller_walletTopupStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid mt-3">
        <div class="row">
            <div class="col-xl-2">
            </div>
            <div class="col-xl-8">
                <div class="card">
                    <div class="card-body py-4" style="min-height: 70vh !important;">
                        <div class="col-lg-12 text-center mt-4">
                            <i class="fa fa-rupee-sign fa-6x text-muted"></i>
                        </div>
                        <div class="col-lg-12 text-center mt-4">
                            <span class="h1 font-weight-bold">
                                <asp:Label ID="lblStatus" runat="server" Text="Congratulations"></asp:Label></span>
                            <h3 class="card-title text-muted mt-3">
                                <asp:Label ID="lblStatusMsg" runat="server" Text=""></asp:Label>
                            </h3>
                            <h3 class="card-title text-muted mt-2">
                                <asp:Label ID="lblWalletStatusMsg" runat="server" Text=""></asp:Label>
                            </h3>
                        </div>
                        <div class="col-lg-12 text-center mt-4">
                            <a href="dashboard.aspx" class="btn btn-outline-success"> <i class="fa fa-home"></i> Traveller Dashboard</a>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-xl-2">
            </div>
        </div>
    </div>
</asp:Content>

