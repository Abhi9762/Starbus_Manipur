<%@ Page Title="" Language="C#" MasterPageFile="~/mainMasterPage.master" AutoEventWireup="true" CodeFile="Disclaimer.aspx.cs" Inherits="Disclaimer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="">
        <div class="container">
            <div class="row">
                <div class="col-lg-12">
                    <div class="row ">
                        <div class="col-lg-12">
                            <div style="line-height:35px;">
                               <asp:Label ID="lbldisclaimer" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <!-- End bordered tabs -->
                </div>
                <br>
            </div>
        </div>
    </div>
</asp:Content>

