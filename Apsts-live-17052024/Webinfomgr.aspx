<%@ Page Title="" Language="C#" MasterPageFile="~/mainMasterPage.master" AutoEventWireup="true" CodeFile="Webinfomgr.aspx.cs" Inherits="Webinfomgr" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="midd-bg">

        <div class="container">
            <div class="row">
                <div class="col-lg-12">
                    <div class="row ">
                        <div class="col-lg-12">
                            <div class="">
                                <div class="white-bg">
                                    <div class="row pt-3">
                                        <div class="col-lg-12 ">
                                            For any query/suggestion/help regarding this website please feel free to contact.
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-4 pt-5 pb-5">
                                            <img src="images/infomgr.png" style="width: 100%;" />
                                        </div>
                                        <div class="col-lg-8 pt-6 pb-5 pl-6">
                                            <div style="line-height: 45px;">
                                                                                           <i class="fa fa-user"></i>&nbsp;  <asp:Label ID="lblname" runat="server" Text="-NA-"></asp:Label><br />
                                           

                                                <i class="fa fa-phone-volume"></i>&nbsp;<asp:Label ID="lblcontact" runat="server" Text="-NA-"></asp:Label><br />
                                                <i class="fas fa-envelope"></i>&nbsp;<asp:Label runat="server" ID="lblemail" Text="-NA-"></asp:Label><br />
                                                
                                            </div>
                                        </div>
                                    </div>
                                </div>
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

