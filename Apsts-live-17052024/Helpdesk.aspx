<%@ Page Title="" Language="C#" MasterPageFile="~/mainMasterPage.master" AutoEventWireup="true" CodeFile="Helpdesk.aspx.cs" Inherits="Helpdesk" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="midd-bg">

        <div class="container">
            <div class="row">
                <div class="col-lg-12">
                    <div class="left-bg3">
                        <div class="row ">
                            <div class="col-lg-6 help-space ">

                                <div class="help-bg1">
                                    <div class="row ">
                                        <div class="col-lg-6 ">

                                            <div class="blu-hd white stc ">Any Query/Help ?</div>
                                            <div class="stc1">You can contact us : </div>
                                            <div class="stc">
                                                Call Us:
                                                <br />
                                                <i class="fa fa-phone-volume fav-color"></i>
                                                <asp:Label ID="lblhelp1" runat="server"></asp:Label>
                                                <br />

                                                <i class="fa fa-envelope fav-color"></i>
                                                <asp:Label ID="lblemailid" runat="server"></asp:Label>
                                            </div>

                                        </div>
                                        <div class="col-lg-2 gg">
                                            <img src="assets/img/contact.png">
                                        </div>
                                        <div class="col-lg-4 arro">
                                            <img src="assets/img/bg-right.png">
                                        </div>
                                    </div>


                                </div>
                            </div>

                            <div class="col-lg-6 space2 col-auto">
                                <div class="hd-text text-center ">For Bus Station enquiries reached out to us at no we are help us to assight you. </div>
                                <div class="row">

                                    <div class="col-lg-5">
                                        <div class="heading-top">Bus Station  Enquiry</div>
                                    </div>
                                    <div class="col-lg-5">
                                        <asp:TextBox ID="txtsearch" CssClass="form-control" runat="server" placeholder="Station Name"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-2">
                                        <asp:LinkButton runat="server" ID="lbtnsearch" OnClick="lbtnsearch_Click" CssClass="btn btn-sm btn-warning mt-1"><i class="fa fa-search"></i></asp:LinkButton>
                                    </div>
                                    <br>
                                    
                                        <table width="90%" style="margin-top: 10px;">
                                            <tr class="table-warning">
                                                <th style="padding: 6px;" width="49%">Station</th>
                                                <th width="30%">Contact No.</th>
                                            </tr>
                                            <asp:Repeater ID="rptstationcontact" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td><%#Eval("officename")%><br />
                                                        </td>
                                                        <td><%#Eval("mob")%><br />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </table>
                                    

                                </div>
                             


                            </div>
                        </div>




                    </div>
                    <div class="click-bg" style="display: none">
                        <div class="col-lg-6 float-right">

                            <div class="">If your enquiry is not get resolved in a timely. you may contact Division and HeadQuarter </div>
                            <div class="text-center">
                                <a href="">
                                    <img src="assets/img/click.png"></a>
                            </div>


                        </div>

                    </div>







                    <!-- End bordered tabs -->
                </div>
            </div>


        </div>
    </div>
</asp:Content>

