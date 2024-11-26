<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/SysAdmmaster.master" AutoEventWireup="true" CodeFile="RatingDetails.aspx.cs" Inherits="Auth_RatingDetails" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../assets/js/jquery.min.js" type="text/javascript"></script>
    <link href="../assets/css/UIMin.css" rel="stylesheet" type="text/css" />
    <script src="../assets/js/jqueryuimin.js" type="text/javascript"></script>
    <script type="text/javascript">
</script>
    <style>
        .modalBackground {
            background-color: black;
            opacity: 0.6;
        }

        .card-body {
            padding: 8px;
        }
    </style>
    <style>
        .gvTable {
            width: 100%;
            max-width: 100%;
            margin-bottom: 1rem;
            background-color: transparent;
            font-size: 10pt;
            margin-top: 5px;
        }

            .gvTable td, .gvTable th {
                padding: 3px;
                vertical-align: top;
                border-top: 1px solid #dee2e6;
            }



        .GridPager td {
            padding-top: 2px;
            padding-left: 3px;
        }

        .GridPager a, .GridPager span {
            display: block;
            height: 22px;
            min-width: 17px;
            font-weight: bold;
            text-align: center;
            text-decoration: none;
        }

        .GridPager a {
            background-color: #f5f5f5;
            color: #969696;
            border: 1px solid #969696;
            border-radius: 0px;
        }

            .GridPager a .hover {
                background-color: red;
                color: #969696;
                border: 1px solid #969696;
            }

        .GridPager span {
            background-color: #A1DCF2;
            color: #000;
            border: 1px solid #3AC0F2;
            border-radius: 0px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hidtoken" runat="server" />
    <asp:HiddenField ID="HiddenFieldTopConductorCode" Value="0" runat="server" />
    <asp:HiddenField ID="HiddenFieldTopBusServiceCode" Value="0" runat="server" />
    <asp:HiddenField ID="HiddenFieldBottomConductorCode" Value="0" runat="server" />
    <asp:HiddenField ID="HiddenFieldBottomBusServiceCode" Value="0" runat="server" />
    <div class="container-fluid" style="padding-top: 15px; padding-bottom: 30px;">

        <div class="row">
            <div class="col-lg-12">
                <div class="card">
                    <div class="card-body" style="height: 500px;">
                        <asp:Panel runat="server" ID="pnlMsg" Visible="true">
                            <div class="row">
                                <div class="col-12 mt-5">
                                    <center>
                                        <asp:Label runat="server" Text="Record Not Found" Font-Bold="true" Font-Size="40px" ForeColor="LightGray"></asp:Label>

                                    </center>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="pnlReport" Visible="false">
                            <div class="row">
                                <div class="col-md-9">
                                    <asp:label runat="server" ID="lbldate"></asp:label>
                                </div>
                                <div class="col-md-3">
                                    <asp:LinkButton ID="lbtnback" runat="server" OnClick="lbtnback_Click" CssClass="btn btn-danger"  Visible="false" Style="float: right;"> <i class="fa fa-backward"> </i> Back To Rating Dashboard</asp:LinkButton>
                                </div>
                            </div>
                            
                            
                            <div class="card-body table-responsive" style="min-height: 320px;">
                                <asp:GridView ID="gvRating" runat="server" CssClass="gvTable" AutoGenerateColumns="false"
                                    GridLines="None" AllowPaging="true" PageSize="15" OnRowDataBound="gvRating_RowDataBound" OnRowCommand="gvRating_RowCommand" DataKeyNames="ticketno,bookedbyusercode">
                                    <Columns>

                                        <asp:TemplateField HeaderText="#">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Bookd BY" Visible="false">
                                            <ItemTemplate>
                                                  <asp:Label runat="server" Text='<%# Eval("bookedbyusercode") %>'></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Email" Visible="false">
                                            <ItemTemplate>
                                                  <asp:Label runat="server" Text='<%# Eval("travelleremail") %>'></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FEEDBACK BY">
                                            <ItemTemplate>
                                                 <asp:Label runat="server" Text='<%# Eval("travellermobileno") %>'></asp:Label>
                                               
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FEEDBACK DATETIME">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Eval("feedback_datetime") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="JOURNEY DETAILS">
                                            <ItemTemplate>
                                                 PNR : <asp:Label runat="server" Text='<%# Eval("ticketno") %>'></asp:Label>
                                                  <br />
                                                <asp:Label runat="server" Text='<%# Eval("servicenameen") %>'></asp:Label>
                                                  <br />
                                                <asp:Label runat="server" Text='<%# Eval("from_station") %>'></asp:Label> - <asp:Label runat="server" Text='<%# Eval("to_station") %>'></asp:Label>
                                                
                                                
                                             
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="BUS SERVICE RATING">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Eval("bus_rating") %>'></asp:Label>
                                                <i class="fa fa-star text-success"></i>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CONDUCTOR RATING">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Eval("conductor_rating") %>'></asp:Label>
                                                <i class="fa fa-star text-success"></i>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PORTAL RATING">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Eval("portal_rating") %>'></asp:Label>
                                                <i class="fa fa-star text-success"></i>
                                            </ItemTemplate>
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="ACTION">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="lbtnsms" CssClass="btn btn-primary btn-sm" CommandName="Sms" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' ToolTip="View Details" Text="View"><i class="fa fa-sms"> Sms</i></asp:LinkButton>
                                                <asp:LinkButton runat="server" ID="lbtnemail" CssClass="btn btn-danger btn-sm" CommandName="Email" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' ToolTip="View Details" Text="View"><i class="fa fa-envelope"> Email</i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Right" CssClass="GridPager" />
                                </asp:GridView>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
        </div>


      <div class="row">
        <cc1:ModalPopupExtender ID="mpConfirmation" runat="server" PopupControlID="pnlConfirmation"
            CancelControlID="LinkButton1" TargetControlID="Button4" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlConfirmation" runat="server" Style="position: fixed;">
            <div class="card" style="width: 350px;">
                <%--<div class="card-header">
                    <h4 class="card-title text-left mb-0">Please Confirm
                    </h4>
                </div>--%>
                 <div class="col-lg-4 text-right pr-0">
                            <asp:LinkButton ID="LinkButton1" runat="server" Text="OK" CssClass="btn btn-danger"
                                UseSubmitBehavior="false" data-dismiss="modal"><i class="fa fa-times"></i></asp:LinkButton>
                        </div>
                <div class="card-body text-left pt-2" style="min-height: 100px;">
                    <asp:Label ID="lblConfirmation" runat="server" Text="Mesaage Sent Succesfully"></asp:Label>
                   <%-- <div style="width: 100%; margin-top: 20px; text-align: right;">
                        <asp:LinkButton ID="lbtnYesConfirmation" runat="server" OnClick="lbtnYesConfirmation_Click" CssClass="btn btn-success btn-sm"> <i class="fa fa-check"></i> Yes </asp:LinkButton>
                        <asp:LinkButton ID="lbtnNoConfirmation" runat="server" OnClick="lbtnNoConfirmation_Click" CssClass="btn btn-danger btn-sm ml-2"> <i class="fa fa-times"></i> No </asp:LinkButton>
                    </div>--%>
                </div>
            </div>
            <div style="visibility: hidden;">
                <asp:Button ID="Button4" runat="server" Text="" />
            </div>
        </asp:Panel>
    </div>
       <%-- Error Message Box--%>
     <div class="row">
        <cc1:ModalPopupExtender ID="ModalPopupMessage" runat="server" PopupControlID="PanelMessage"
            TargetControlID="ButtonMessageOpen" CancelControlID="LinkButtonMessageClose"
            BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="PanelMessage" runat="server" Width="50%" Style="position: fixed;">
            <div class="card">
                <div class="modal-content" style="min-width: 300px; text-align: left;">
                    <div class="modal-header p-2" style="background-color: #1a4a84;">
                        <div class="col-lg-8">
                            <h4 style="line-height: 25px; color: White;">Send Message</h4>
                        </div>
                        <div class="col-lg-4 text-right pr-0">
                            <asp:LinkButton ID="LinkButtonMessageClose" runat="server" Text="OK" CssClass="btn btn-danger"
                                UseSubmitBehavior="false" data-dismiss="modal"><i class="fa fa-times"></i></asp:LinkButton>
                        </div>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-lg-12 pb-1 pt-1" style="border-bottom: 1px solid #d6cece; font-weight: bold; background-color: #efebeb;">
                                To
                                <asp:Label ID="LabelMpMsgTo" runat="server" Text="N/A" Font-Bold="false"></asp:Label>
                            </div>
                            <div class="col-lg-12 pb-1 pt-1">
                                <asp:HiddenField ID="HiddenFieldTicketNoForSMS" runat="server" />
                                <asp:TextBox ID="TextBoxMpMessage" runat="server" TextMode="MultiLine" Style="width: 100%; border: none; max-height: 125px; min-height: 125px;"
                                    placeholder="Message type here (max 200 character) . ."
                                    MaxLength="200"></asp:TextBox>

                            </div>
                        </div>
                        <div style="text-align: right; margin-top: 10px;">
                            <asp:LinkButton ID="LinkButtonMpMsgSend" runat="server" OnClick="LinkButtonMpMsgSend_Click" Text="OK" CssClass="btn btn-primary"
                                UseSubmitBehavior="false" data-dismiss="modal"><i class="fa fa-send"></i> Send</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <div style="visibility: hidden;">
                <asp:Button ID="ButtonMessageOpen" runat="server" Text="" />
            </div>
        </asp:Panel>
    </div>


           <%-- Email Message Box--%>
    <div class="row">
        <cc1:ModalPopupExtender ID="ModalPopupEmail" runat="server" PopupControlID="PanelEmail"
            TargetControlID="ButtonEmailOpen" CancelControlID="ButtonEmailClose" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="PanelEmail" runat="server" Width="50%" Style="position: fixed;">
            <div class="card">
                <div class="modal-content" style="min-width: 300px; text-align: left;">
                    <div class="modal-header p-2" style="background-color: #1a4a84;">
                        <div class="col-lg-8">
                            <h4 style="line-height: 25px; color: White;">Send Message</h4>
                        </div>
                        <div class="col-lg-4 text-right pr-0">
                            <asp:LinkButton ID="ButtonEmailClose" runat="server" Text="OK" CssClass="btn btn-danger"
                                UseSubmitBehavior="false" data-dismiss="modal"><i class="fa fa-times"></i></asp:LinkButton>
                        </div>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-lg-12 pb-1 pt-1" style="border-bottom: 1px solid #d6cece; font-weight: bold; background-color: #efebeb;">
                                To
                                <asp:Label ID="LabelMpEmlTo" runat="server" Text="N/A" Font-Bold="false"></asp:Label>
                            </div>
                            <div class="col-lg-12 pb-1 pt-1" style="border-bottom: 1px solid #d6cece; font-weight: bold;">
                                Subject
                                <asp:TextBox ID="TextBoxMpEmlSubject" runat="server" Text="" Font-Bold="false" MaxLength="80"
                                    Style="width: 90%; border: none;" placeholder="Subject type here (max 80 character) . ."></asp:TextBox>
                            </div>
                            <div class="col-lg-12 pb-1 pt-1">
                                <asp:TextBox ID="TextBoxMpEmlMessage" runat="server" TextMode="MultiLine" Style="width: 100%; border: none; max-height: 125px; min-height: 125px;"
                                    placeholder="Message type here (max 200 character) . ."
                                    MaxLength="200"></asp:TextBox>
                            </div>
                        </div>
                        <div style="text-align: right; margin-top: 10px;">
                            <asp:LinkButton ID="LinkButtonMpEmlSend" runat="server" Text="OK" CssClass="btn btn-primary"
                                UseSubmitBehavior="false" data-dismiss="modal"><i class="fa fa-send"></i> Send</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <div style="visibility: hidden;">
                <asp:Button ID="ButtonEmailOpen" runat="server" Text="" />
            </div>
        </asp:Panel>
    </div>
    
       
</asp:Content>




