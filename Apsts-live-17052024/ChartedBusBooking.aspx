<%@ Page Title="" Language="C#" MasterPageFile="~/mainMasterPage.master" AutoEventWireup="true" CodeFile="ChartedBusBooking.aspx.cs" Inherits="ChartedBusBooking" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container-fluid">
            <div class="row no-gutters">               
                <div class="col-md-12 wrap-about ftco-animate">
                    <div class="heading-section pl-md-5 text-center p-5">
                        <h2 class="mb-4">
                            <asp:Label ID="lblHeading" runat="server" Text="Thanks for being here"></asp:Label></h2>
                        <div class="block-23 mb-3 ">
                            <ul>
                                <li>
                                    <asp:Label ID="lblText1" runat="server" CssClass="text text-black-50 font-weight-normal" Text="Services will be available very soon. Please keep visiting this section"></asp:Label>
                                </li>                             
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>

