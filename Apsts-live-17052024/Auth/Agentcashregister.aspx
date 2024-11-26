<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/AgentMaster.master" AutoEventWireup="true" CodeFile="Agentcashregister.aspx.cs" Inherits="Auth_Agentcashregister" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        .tblgrid
        {
            line-height: 30px;
            width: 100%;
        }
        .tblgrid tr
        {
            border-bottom: 1px solid #70d1f4;
        }
        .tblgrid th, td
        {
            padding: 4px;
            text-align: center;
        }
        
        .tblgrid th
        {
            line-height: 16px;
        }
        .GridPager table tr
        {
            border-bottom: 1px solid #ffffff;
        }
        .GridPager a
        {
            background-color: #f5f5f5;
            color: #969696;
            border: 1px solid #969696;
            border-radius: 10px;
            padding: 3px 5px 3px 5px;
        }
        .GridPager a .hover
        {
            background-color: red;
            color: #969696;
            border: 1px solid #969696;
        }
        .GridPager span
        {
            background-color: #A1DCF2;
            color: #000;
            border: 1px solid #3AC0F2;
            border-radius: 0px;
            padding: 3px 5px 3px 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content" style="width: 100%;">
        <div class="card">
            <div class="card-body" style="line-height: 18px; font-size: 11pt;">
                <br />
                <div class="row">
                    <div class="col-lg-3" style="border-right: 1px solid">
                        <table class="table" style="border: none;">
                            <tr>
                                <td style="text-align: right; vertical-align: middle; font-weight: bold; border-top: none;">
                                    Year <span style="color: Red;">*</span>
                                </td>
                                <td style="border-top: none;">
                                    <asp:DropDownList ID="ddlyear" runat="server" AutoPostBack="true" CssClass="form-control">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; vertical-align: middle; font-weight: bold; border-top: none;">
                                    Month <span style="color: Red;">*</span>
                                </td>
                                <td style="border-top: none;">
                                    <asp:DropDownList ID="ddlmonth" runat="server" AutoPostBack="true" CssClass="form-control">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; vertical-align: middle; font-weight: bold; border-top: none;">
                                    Date <span style="color: Red;">*</span>
                                </td>
                                <td style="border-top: none;">
                                    <asp:DropDownList ID="ddldate" runat="server" AutoPostBack="true" CssClass="form-control">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center;" colspan="2">
                                    <asp:Button ID="btnshow" runat="server" Text="Show" OnClick="btnshow_Click" CssClass="btn btn-success" Style="border-radius: 4px;" />
                                    
                                </td>
                            </tr>
                        </table>
                        <strong>Account Details</strong>
                        <table class="table" id="tblaccdetails" runat="server">
                            <tr>
                                <td>
                                    Opening Balance
                                </td>
                                <td>
                                    <asp:Label ID="lblopenblnc" runat="server" Text="0"></asp:Label>
                                    <i class="fa fa-rupee"></i>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Transaction Amount
                                </td>
                                <td>
                                    <asp:Label ID="lbltrnsamt" runat="server" Text="0"></asp:Label>
                                    <i class="fa fa-rupee"></i>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Deposit amount
                                </td>
                                <td>
                                    <asp:Label ID="lbldepositamt" runat="server" Text="0"></asp:Label>
                                    <i class="fa fa-rupee"></i>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Closing Balance
                                </td>
                                <td>
                                    <asp:Label ID="lblcloseblnc" runat="server" Text="0"></asp:Label>
                                    <i class="fa fa-rupee"></i>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <br />
                        <br />
                        <br />
                        <center>
                            <asp:Label ID="lblacdetailsmsg" runat="server" Text="Account Details appear here"
                                Style="color: Red;" Visible="false"></asp:Label></center>
                    </div>
                    <div class="col-lg-9">
                        <div>
                            <strong> 
                            <asp:Label ID="lbldate" runat="server" Text=""></asp:Label></strong>
                           
                            <asp:LinkButton ID="btnback" OnClick="btnback_Click" runat="server" CssClass="btn btn-success" Style="border-radius: 4px;
                                float: right;"> <i class="fa fa-backward"></i> Back</asp:LinkButton>
                        </div>
                        <br />
                        <hr />
                        <div id="bokcandetails" runat="server">
                            <div class="row">
                                <div class="col-lg-4" style="border-right: 1px solid orange;">
                                    <center>
                                        <b>
                                            <asp:Label ID="Label2" runat="server" Font-Size="15px" Text="Booking Details"></asp:Label></b></center>
                                    <hr />
                                    <center>
                                        <asp:GridView ID="grdagbooking" runat="server" AutoGenerateColumns="False" GridLines="None"
                                            AllowSorting="true" AllowPaging="true" PageSize="7" Font-Size="9pt" Width="100%"
                                            class="tblgrid" DataKeyNames="" OnPageIndexChanging="grdagbooking_PageIndexChanging">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Ticket No ">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblagtktno" runat="server" Text='<%# Eval("ticketno") %>'></asp:Label></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Fare Amt.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblagfareamt" runat="server" Text='<%# Eval("amt_tot") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Tax Amt.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblagtaxamt" runat="server" Text='<%# Eval("amt_tax") %>'></asp:Label></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total Seats">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblagtotseats" runat="server" Text='<%# Eval("tot_seat") %>'></asp:Label></ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <RowStyle BackColor="White" BorderColor="#333333" />
                                            <AlternatingRowStyle BackColor="#eaf4ff" VerticalAlign="Middle" />
                                            <HeaderStyle BackColor="#6699FF" ForeColor="White" VerticalAlign="Middle" />
                                            <PagerStyle HorizontalAlign="Left" CssClass="GridPager" />
                                        </asp:GridView>
                                        <asp:Label ID="grdagbookingmsg" runat="server" Text="Agent Booking Details Select Particular Date"
                                            Style="color: Red;"></asp:Label>
                                    </center>
                                </div>
                                <div class="col-lg-4" style="border-right: 1px solid orange;">
                                    <center>
                                        <b>
                                            <asp:Label ID="Label3" runat="server" Font-Size="15px" Text="Cancellation Details"></asp:Label></b></center>
                                    <hr />
                                    <center>
                                        <asp:GridView ID="grdagcancel" runat="server" AutoGenerateColumns="False" GridLines="None"
                                            AllowSorting="true" AllowPaging="true" PageSize="7" Font-Size="9pt" Width="100%"
                                            class="tblgrid" DataKeyNames="" OnPageIndexChanging="grdagcancel_PageIndexChanging">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Cancel Ref. No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblagcancelrefno" runat="server" Text='<%# Eval("agcancelrefno") %>'></asp:Label></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="UTC Ref. No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblagUtcrefno" runat="server" Text='<%# Eval("txn_refno") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Refund Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblagtransamt" runat="server" Text='<%# Eval("amt") %>'></asp:Label></ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <RowStyle BackColor="White" BorderColor="#333333" />
                                            <AlternatingRowStyle BackColor="#eaf4ff" VerticalAlign="Middle" />
                                            <HeaderStyle BackColor="#6699FF" ForeColor="White" VerticalAlign="Middle" />
                                            <PagerStyle HorizontalAlign="Left" CssClass="GridPager" />
                                        </asp:GridView>
                                        <asp:Label ID="grdagcancelmsg" runat="server" Text="Agent Cancellation Details Select Particular Date"
                                            Style="color: Red;"></asp:Label>
                                    </center>
                                </div>
                                <div class="col-lg-4">
                                    <center>
                                        <b>
                                            <asp:Label ID="Label1" runat="server" Font-Size="15px" Text="Deposit Details"></asp:Label></b></center>
                                    <hr />
                                    <center>
                                        <asp:GridView ID="grdagdeposit" runat="server" AutoGenerateColumns="False" GridLines="None"
                                            AllowSorting="true" AllowPaging="true" PageSize="7" Font-Size="9pt" Width="100%"
                                            class="tblgrid" DataKeyNames="" OnPageIndexChanging="grdagdeposit_PageIndexChanging">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Transaction <br/>Ref. No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAGENTUTCREFNO" runat="server" Text='<%# Eval("AGENTUTCREFNO") %>'></asp:Label></ItemTemplate>
                                                </asp:TemplateField>
                                                                                         
                                                <asp:TemplateField HeaderText="Deposit<br/> Amount (Rs.) ">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAMOUNT" runat="server" Text='<%# Eval("AMOUNT") %>'></asp:Label></ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <RowStyle BackColor="White" BorderColor="#333333" />
                                            <AlternatingRowStyle BackColor="#eaf4ff" VerticalAlign="Middle" />
                                            <HeaderStyle BackColor="#6699FF" ForeColor="White" VerticalAlign="Middle" />
                                            <PagerStyle HorizontalAlign="Left" CssClass="GridPager" />
                                        </asp:GridView>
                                        <asp:Label ID="grdagDepositmsg" runat="server" Text="Sorry,You haven't made any Deposit Transaction"
                                            Style="color: Red;"></asp:Label>
                                    </center>
                                </div>
                            </div>
                        </div>
                        <center>
                            <asp:Label ID="lblbokcandetails" runat="server" Text="Booking/Cancellation/Deposit Details appear here"
                                Style="color: Red;" Visible="false"></asp:Label></center>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

