<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/AgentMaster.master" AutoEventWireup="true" CodeFile="AGPmtError.aspx.cs" Inherits="Auth_AGPmtError" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hidtoken" runat="server"></asp:HiddenField>
    <div class="row" style="margin-top: 25px; line-height: 24px;">
        <div class="col-lg-12">
            <center>
                <div class="card" style="width: 50%;">
                    <center>
                        <img src="assets/img/failed.jpg" style="width: 150px;" /></center>
                    <br />
                    <span style="color: Red; font-size: 10pt; font-weight: bold;">Transaction rejected/not
                        completed by Payment Gateway.</span> <span style="color: Red; font-size: 10pt; font-weight: bold;">Please try again.</span> <span style="color: Red; font-size: 10pt; font-weight: bold;">If any Amount deducted, please contact your Bank.</span>
                    <br />
                    <br />
                    <asp:Label ID="lblerror" runat="server"></asp:Label>
                    <br />
                    <br />
                    <center>
                        <asp:LinkButton ID="btnback" runat="server" OnClick="btnback_Click" Text="Go to Dashboard" CssClass="btn btn-success"
                            Style="width: 170px; margin-bottom: 12px; border-radius: 4px;"></asp:LinkButton>
                    </center>
                </div>
            </center>
        </div>
    </div>
</asp:Content>


