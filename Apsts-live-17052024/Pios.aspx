<%@ Page Title="" Language="C#" MasterPageFile="~/mainMasterPage.master" AutoEventWireup="true" CodeFile="Pios.aspx.cs" Inherits="Pios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="midd-bg">

        <div class="container">
            <div class="row">
                <div class="col-lg-12 text-center">
                     <embed id="empios" runat="server" width="100%" height="450px"></embed>
                     <asp:Image ID="imgpios" runat="server" ImageUrl="~/images/coming-soon.png" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

