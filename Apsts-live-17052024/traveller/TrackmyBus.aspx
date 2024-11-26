<%@ Page Title="" Language="C#" MasterPageFile="~/traveller/trvlrMaster.master" AutoEventWireup="true" CodeFile="TrackmyBus.aspx.cs" Inherits="traveller_TrackmyBus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid pt-4">
        <div class="row">
            <div class="col-lg-12">
                <div id="div1" runat="server">
                </div>

            </div>
            <div class="col-lg-12 text-center">
                <br />
                <br />
                <br />
                <br />
                <br />
                
                <asp:Label ID="lblgpsyn" runat="server"  Text="Sorry, Bus Details Not Available"
                    Style="color: red; font-size: 12pt;" Visible="false"></asp:Label>

            </div>
        </div>
    </div>
</asp:Content>

