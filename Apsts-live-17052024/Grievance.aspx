<%@ Page Title="" Language="C#" MasterPageFile="~/mainMasterPage.master" AutoEventWireup="true" CodeFile="Grievance.aspx.cs" Inherits="Grievance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="midd-bg">

        <div class="container">

            <h3 class="mb-1">
                <div class="tx5"> You can submit your<span class="org"> grievance related to </span>Our Online Service</div>


                
            </h3>
            <div class="tk-bg ">
                <div class="row">
                    <div class="col-lg-9">

                      
                        <p class="hd-text ">If you are a user of Our Online Service please submit your feedback/complaint/grievance through user dashboard(Available after successful login) In case you are not satisfied with the action, you can send your feedback/complaint/grievance to Our <a href="helpdesk.aspx">Help Desk</a></p>

                        <p class="hd-text ">Grievance Redressal Officer is the final authority for addressing grievances related to Our Online Services. The contact details of Grievance Redressal officer. </p>
                        <div class="col-lg-3">
                            <div class="org-bg6">General Manager</div>
                        </div>
                        <div class="row">

                            <div class="col-lg-8 ">
                                <div class="left-bg ">



                                    <div class="col-lg-12 ">
                                        <p class="hd-text "><i class="fas fa-envelope fav-icon"></i>&nbsp;<asp:Label runat="server" ID="lblemail" Text="-NA-"></asp:Label></p>
                                        <p class="hd-text "><i class="fa fa-map-marked fav-icon"></i>&nbsp;<asp:Label runat="server" ID="lbladdress" Text="-NA-"></asp:Label></p>

                                    </div>
                                </div>
                                <p class="text-dark">
                                    You can register your grievance
                                    <asp:LinkButton ID="lbtnRegister" runat="server" CssClass="btn btn-link btn-sm" OnClick="lbtnRegister_Click">Register Here</asp:LinkButton>
                                </p>
                            </div>



                        </div>
                    </div>
                    <div class="col-lg-3">
                        <img src="assets/img/cont1.png">
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

