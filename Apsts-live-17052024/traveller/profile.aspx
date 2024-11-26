<%@ Page Title="" Language="C#" MasterPageFile="~/traveller/trvlrMaster.master" AutoEventWireup="true" CodeFile="profile.aspx.cs" Inherits="traveller_profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid mt-4">
        <div class="row">
            <div class="col-xl-4">
                <div class="card">
                    <div class="card-body" style="min-height: 50vh !important">
                        <div class="row">
                            <div class="col-lg-12 text-center mt-4">
                                <i class="fa fa-user-circle fa-8x text-primary"></i>
                            </div>
                            <div class="col-lg-12 text-center mt-4">
                                <h2 class="font-weight-bold mb-0">
                                    <asp:Label ID="lblUserID" runat="server"></asp:Label>
                                </h2>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-xl-8">
                <div class="card">
                    <div class="card-body" style="min-height: 50vh !important">
                        <asp:Panel ID="Panel1" runat="server">
                            <div class="row">
                                <div class="col-lg-12 text-center mt-4">
                                <i class="fa fa-user-circle fa-8x text-muted"></i>
                                </div>
                                <div class="col-lg-12 text-center mt-4">
                                    <p class="h2 font-weight-bold mb-0 mt-2">
                                       Coming Soon
                                    </p>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

