<%@ Page Title="" Language="C#" MasterPageFile="~/mainMasterPage.master" AutoEventWireup="true" CodeFile="DownloadE_ticket.aspx.cs" Inherits="DownloadE_ticket" EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="midd-bg">

        <div class="container">
           
                   <div class="row no-gutters">
                <div class="col-md-12">
                    

                        <div class="tx5">For Kind  Attention <span class="org">Please</span></div>
                        <ol >


                                <li class="list-group-item">Tickets of upcoming journey booked online or from MSTS Booking counters can directly be downloaded here.</li>
                                <li class="list-group-item">Tickets of past  journeys booked online, can be download by login to respective Traveler account.</li>
                            </ol>


                   
                      <h3 class="mb-1">
                        <div class="tx5">To Download eTicket Please<span class="org"> Enter Your PNR(Ticket) No./Valid Mobile number.</span></div>
                    </h3>
                        
                        <div class="card py-2 px-3" style="box-shadow: 0px 5px 12px -1px rgb(0 0 0 / 6%);">
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group mb-3">
                                        <asp:Label ID="lblPNRNumberHeader" runat="server" Text="PNR Number" CssClass="label font-weight-light mb-0" Style="font-size: 12px; text-transform: uppercase;"></asp:Label>
                                        <asp:TextBox ID="tbPnr" runat="server" MaxLength="20" class="form-control form-control-sm"
                                            Style="text-transform: uppercase; font-size: 16px;" placeholder="PNR Number" autocomplete="off"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group ">
                                        <asp:Label ID="lblMobileNumberHeader" runat="server" Text="Mobile Number" CssClass="label font-weight-light mb-0" Style="font-size: 12px; text-transform: uppercase;"></asp:Label>
                                        <asp:TextBox ID="tbMob" runat="server" MaxLength="10" class="form-control form-control-sm"
                                            Style="text-transform: uppercase; font-size: 16px;" placeholder="Mobile Number" autocomplete="off"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group" style="padding-top: 28px;">
                                        <div class="input-group" style="box-shadow: 0px 0px rgb(53 52 92 / 20%), 0 0px 0 rgb(0 0 0 / 1%);">
                                            <img alt="Visual verification" title="Please enter the security code as shown in the image."
                                                src="CaptchaImage.aspx" style="width: 80%; border: 1px solid #e6e6e6; margin-top:-5px;" class="form-control" />
                                            <div class="input-group-append">
                                                <asp:LinkButton runat="server" ID="lbtnRefresh"  CssClass="btn btn-icon btn-primary btn-sm" OnClick="lbtnRefresh_Click" style="margin-top:-5px;">
                                                    
                                                   
                                                    <i class="fa  fa-2x fa-recycle" ></i></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <div class="col-md-2">
                                    <div class="form-group ">
                                        <asp:Label ID="Label1" runat="server" Text="Image Text" CssClass="label font-weight-light mb-0" Style="font-size: 12px; text-transform: uppercase;"></asp:Label>
                                        <asp:TextBox ID="tbcaptchacode" runat="server" MaxLength="6" class="form-control"
                                            Style="text-transform: uppercase; font-size: 16px;" placeholder="Text" autocomplete="off"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-1 mt-1">
                                    <div class="form-group mt-4">
                                        <asp:LinkButton ID="search" runat="server" CssClass="btn btn-icon btn-primary btn-sm"  OnClick="lbtntdownload_Click" ToolTip="Click here to Download Ticket" style="margin-top:-5px"> <i class="fa fa-2x fa-download" title="Click here to Download Ticket" ></i></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                    
                       </div>
                

            

            </div>
        </div>
    <br />  <br /><br />
     <cc1:ModalPopupExtender ID="mpePage" runat="server" PopupControlID="pnlPage" TargetControlID="Button21"
        CancelControlID="LinkButton71" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlPage" runat="server" Style="position: fixed;">
        <div class="modal-content mt-5" style="width: 98%; margin-left: 2%; text-align:center">
            <div class="modal-header" >
                <div class="col-md-7">
                    <h3 id="lblTitle" runat="server" class="m-0 text-left"> </h3>
                </div>
                <div class="col-md-4 text-right" style="text-align: end;">
                </div>
                <div class="col-md-1 text-right" style="text-align: end;">
                    <asp:LinkButton ID="LinkButton71" runat="server" ForeColor="Red" Font-Bold="true" Font-Size="24px">X</asp:LinkButton>
                </div>
            </div>
            <div class="modal-body" style="background-color: #ffffff; overflow-y: auto; padding: 1px; text-align:center">
                <embed src="" style="height: 90vh; width: 55vw;" runat="server" id="embedPage" />
            </div>
        </div>
        <br />
        <div style="visibility: hidden;">
            <asp:Button ID="Button21" runat="server" Text="" />
        </div>
    </asp:Panel>
   
</asp:Content>

