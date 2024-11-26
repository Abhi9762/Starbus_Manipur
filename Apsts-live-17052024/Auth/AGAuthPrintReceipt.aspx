<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/AgentMaster.master" AutoEventWireup="true" CodeFile="AGAuthPrintReceipt.aspx.cs" Inherits="Auth_AGAuthPrintReceipt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hidtoken" runat="server" />
    <div class="content mt-3" style="width: 100%;">
    <center>
        <div class="panel">
            <div class="panel-body" style="width: 60%; box-shadow: 0px 5px 16px 0px #8E8E8E;
                min-height: 200px;">
                
                <div class="row" style="margin-top: 25px; line-height:38px;" id="dvprint" runat ="server">
                    <div class="col-lg-12">
                    <center>
                    <asp:Label ID="Label12" runat="server" CssClass="lblMessage" Font-Bold="True" Font-Size="10pt"
                        Text="Thanks for using Online Recharge Service. Your Transaction has been completed successfully"></asp:Label>
                    <br />
                    <asp:Label ID="lblTripType" runat="server" CssClass="lblMessage" Font-Bold="True"
                        Font-Size="10pt" Text="Please Print your receipt"></asp:Label><br />
                    <asp:Label ID="lblJourney" runat="server" Text="Your receipt For Online payment is "
                        Style="font-family: verdana; font-size: 10pt; color: #1e4a84;"></asp:Label><br />
                    <asp:LinkButton ID="lbtnprntreceipt" runat="server" OnClick="lbtnprntreceipt_Click" CssClass="btn btn-success" Style="font-family: verdana;
                        font-size: 10pt; margin-top: 9px;">Click Here to Print Your Receipt</asp:LinkButton>
                </center>
                    </div>
                </div>
                 <div class="row" style="margin-top: 25px; line-height:38px;" id="dvprntYN" runat ="server" visible ="false">
                    <div class="col-lg-12">
                    <center>
                    <asp:Label ID="Label1" runat="server" CssClass="lblMessage" Font-Bold="True" Font-Size="10pt"
                        Text="Print Receipt Successfully ? "></asp:Label><br /><br />
                         <asp:LinkButton ID="lbtnyes" runat="server" OnClick="lbtnyes_Click" CssClass="btn btn-success" Style="font-family: verdana;
                        font-size: 10pt; margin-top: 9px;">Yes</asp:LinkButton>
                         <asp:LinkButton ID="lbtnno" runat="server" OnClick="lbtnno_Click" CssClass="btn btn-warning" Style="font-family: verdana;
                        font-size: 10pt; margin-top: 9px;">No</asp:LinkButton>

                        </center>
                    </div>
                    </div>
            </div>
        </div>
        </center>
    </div>
</asp:Content>


