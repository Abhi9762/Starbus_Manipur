<%@ Page Title="" Language="C#" MasterPageFile="~/mainMasterPage.master" AutoEventWireup="true" CodeFile="Rti.aspx.cs" Inherits="Rti" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="midd-bg">

        <div class="container">
            <div class="row">
                <div class="col-lg-4 text-center">
                    <span>RTI Manual Part-1</span>
                     <asp:Image ID="imgrti1" runat="server" ImageUrl="~/images/coming-soon.png" />
                  <embed id="emrti1" runat="server" width="100%" height="450px" />
                    
                </div>
                 <div class="col-lg-4 text-center">
                        <span>RTI Manual Part-2</span>
                      <asp:Image ID="imgrti2" runat="server" ImageUrl="~/images/coming-soon.png" />
                    <embed id="emrti2" runat="server" width="100%" height="450px"/>
                     
                </div>
                 <div class="col-lg-4 text-center">
                        <span>RTI Manual Part-3</span>
                      <asp:Image ID="imgrti3" runat="server" ImageUrl="~/images/coming-soon.png" />
                    <embed id="emrti3" runat="server" width="100%" height="450px"/>
                     
                </div>
            </div>
        </div>
    </div>
</asp:Content>

