<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/SysAdmmaster.master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Auth_Dashboard" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid pt-top ">
        <asp:UpdatePanel runat="server" ID="UpdatePanelall" UpdateMode="Always">
            <ContentTemplate>
                <div class="row mt-2">
                    <div class="col-lg-12">
                       
                        <asp:Label ID="Label5" runat="server" Text="Dashboard" Style="font-size: 16pt; font-weight: bold; color: #074886;"></asp:Label>
                     
                    </div>
                </div>
                <div class="row">
                       <div class="col-md-3  stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">
                                <div class="card-heading">Live Data </div>
                                <div class="total-tx mb-4">View Live Ticket Booking & Bus Passes</div>
                                <div class="col text-right">
                                    <asp:LinkButton ID="lbtnlivedata" runat="server" onclick="lbtnlivedata_click" class="btn btn-sm btn-primary" ToolTip="Explore"> <i class="ni ni-cloud-download-95 mttop "></i>  Explore
                                    </asp:LinkButton>
                                </div>

                            </div>
                        </div>
                    </div>
                 
                </div>
           <div class="row mt-2" runat="server" visible="true">
                    <div class="col-lg-12">
                        <asp:Label ID="Label1" runat="server" Text="Information" Style="font-size: 16pt; font-weight: bold; color: #074886;"></asp:Label>
                    </div>
                </div>
                <div class="row" runat="server" visible="true">
                    <div class="col-md-3  stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">
                                <div class="card-heading">Wallet Information System </div>
                                <div class="total-tx mb-4">Traveller,Agent and CSC wallet Transaction details</div>
                                <div class="col text-right">
                                    <asp:LinkButton ID="lbtnWalletInfo" runat="server" OnClick="lbtnWalletInfo_Click" class="btn btn-sm btn-primary" ToolTip="Explore"> <i class="ni ni-cloud-download-95 mttop "></i>  Explore
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
       


               



            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>



