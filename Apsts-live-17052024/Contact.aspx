<%@ Page Title="" Language="C#" MasterPageFile="~/mainMasterPage.master" AutoEventWireup="true" CodeFile="Contact.aspx.cs" Inherits="Contact" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <div class="midd-bg">
                     <div class="container">
                        <div class="tk-bg text-center">
                          <div class="blu-hd "><asp:Label runat="server" ID="lbldepartmentname"></asp:Label> Headquarters</div>
                           <div class="col-lg-10 offset-2 "> 
                            <div class="text-center">  <div class="row ">
                           <div class="col-lg-6">   
                            <div class="hd-text text-center"><asp:Label runat="server" ID="lbladdress"></asp:Label>, <br><asp:Label runat="server" ID="lblstatedistrict"></asp:Label><br>
                              Phone : <asp:Label runat="server" ID="lblcontact"></asp:Label><br />
                              Email : <asp:Label runat="server" ID="lblemail"></asp:Label>
                            </div>
                              </div>
                            <div class="col-lg-3">   <img src="assets/img/contact.png"> </div>
                    
                        </div></div>
                          


                        
  
                                <!-- End bordered tabs -->
                              </div>



                   
                      


                      </div>

     
                      </div>
                    </div>
</asp:Content>

