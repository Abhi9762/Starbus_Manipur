<%@ Page Title="" Language="C#" MasterPageFile="~/mainMasterPage.master" AutoEventWireup="true" CodeFile="BookingCounters.aspx.cs" Inherits="BookingCounters" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div class="row no-gutters">
            <div class="col-md-12">
                <div class="heading-section pl-md-5">
                    <div class="block-23 mb-3 ">
                        <ul>
                            <li>
                                <span class="text text-dark" style="font-size:15px">APSTS is offering services through booking counters setup in bus stands in Uttarakhand and Adjoining States.
                                </span></li>
                        </ul>
                        <h3 class="mb-4">Services offered at Booking Counters</h3>

                        <h5>One can visit booking counters for </h5>
                        <ul style="font-size:15px">
                            <li><span class="text text-dark">1. Enquiries </span></li>
                            <li><span class="text text-dark">2. Ticket Booking / Cancellations</span></li>
                            <li><span class="text text-dark">3.	Bus Passes</span></li>
                            <li><span class="text text-dark">4.	Courier Booking</span></li>
                        </ul>
                        <h3 class="mb-4">List of Booking Counters</h3>
                        <asp:GridView ID="grdbookingcntr" runat="server" AutoGenerateColumns="false" Visible="false"
                            GridLines="None" CssClass="table" DataKeyNames="" AllowPaging="true">
                            <Columns>
                                <asp:TemplateField HeaderText="Depot/Station">
                                    <ItemTemplate>
                                        <span class="text text-dark font-weight-bold"><%#Eval("depo") %></span><br />
                                        <asp:Label ID="lblstationname" runat="server" Text='<%#Eval("station") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Counter">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldepotname" runat="server" Text='<%#Eval("titl") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

