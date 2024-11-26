<%@ Page Title="" Language="C#" MasterPageFile="~/mainMasterPage.master" AutoEventWireup="true" CodeFile="passpmterror.aspx.cs" Inherits="passpmterror" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-content">
        <div class="container">
            <div class="row my-3">
                <div class="col-lg-3"></div>
                <div class="col-lg-6 text-center">
                    <div class="card pt-3">
                        <div class="text-center">
                            <img src="Auth/dashAssests/img/failed.jpg" />
                        </div>
                        <span style="color: Red; font-size: 10pt; font-weight: bold;">Transaction rejected/not
                        completed by Payment Gateway.
                            Please try again.</span> <span style="color: Red; font-size: 10pt; font-weight: bold;">If any Amount deducted, please contact your Bank.</span>
                        <asp:Label ID="lblerror" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="col-lg-3"></div>
            </div>
        </div>
    </div>

</asp:Content>





