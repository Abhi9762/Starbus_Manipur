<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/ForemanMaster.master" AutoEventWireup="true" CodeFile="bus_inspection.aspx.cs" Inherits="Auth_bus_inspection" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid">
        <div class="row" style="margin-top: 10px">
<div class="col-12">
                <asp:LinkButton href="../Auth/UserManuals/Foreman/Help Document for Foreman.pdf" target="_blank" runat="server" CssClass="btn btn-success btn-sm float-right ml-1" ToolTip="Click here for manual."><i class="fa fa-download"></i></asp:LinkButton>
                <asp:Label runat="server" Text="Download Manual" CssClass="float-right"></asp:Label>
            </div>
            <div class="col-4">
                <div class="card">
                    <div class="card-body" style="min-height: 700px">
                        <div class="card-header">
                            Buses Pending For Verification
                        </div>
                        <asp:GridView ID="grdPending" OnPageIndexChanging="grdPending_PageIndexChanging" OnRowCommand="grdPending_RowCommand" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                            GridLines="None" DataKeyNames="busno,cashdeposityn"
                            class="table table-hover mb-0 mt-2" PageSize="8" Font-Size="13px">
                            <Columns>
                                <asp:TemplateField HeaderText="Bus No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDepot" runat="server" Text='<%#  Eval("busno") %>'></asp:Label><br />
                                        (<asp:Label ID="lblServiceCode" runat="server" Font-Size="Smaller" Text='<%#  Eval("bustype") %>'></asp:Label>)
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Service Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTripCode" runat="server" Text='<%#  Eval("serivename") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnproceed" runat="server" CssClass="btn btn-primary btn-sm"
                                            CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="verify"
                                            Style="padding: 1px 6px; border-radius: 5px; font-size: 12px;" ToolTip="Verify Bus"><i class="fa fa-arrow-right"></i></asp:LinkButton>
                                        <asp:LinkButton ID="bntView" runat="server" CssClass="btn btn-success btn-sm"
                                            CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="view"
                                            Style="padding: 1px 6px; border-radius: 5px; font-size: 12px;" ToolTip="View Bus Details"><i class="fa fa-eye"></i></asp:LinkButton>


                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle HorizontalAlign="Left" CssClass="gridview" />
                        </asp:GridView>
                        <div class="Row" id="divPending" runat="server" visible="true">
                            <div class="col-12 text-center mt-9">
                                <span style="font-size: 40px; color: lightgray; font-weight: bold">No Record Found</span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-4">
                <div class="card">
                    <div class="card-body" style="min-height: 700px">
                        <div class="card-header">
                            Verified Buses
                        </div>
                        <asp:GridView ID="grdVerified" OnRowCommand="grdVerified_RowCommand" OnPageIndexChanging="grdVerified_PageIndexChanging" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                            GridLines="None" DataKeyNames="busno"
                            class="table table-hover mb-0 mt-2" PageSize="8" Font-Size="13px">
                            <Columns>
                                <asp:TemplateField HeaderText="Bus No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDepot" runat="server" Text='<%#  Eval("busno") %>'></asp:Label><br />
                                        <asp:Label ID="lblServiceCode" runat="server" Text='<%#  Eval("bustype") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Service Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTripCode" runat="server" Text='<%#  Eval("serivename") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <ItemTemplate>
                                             <asp:LinkButton ID="bntView" runat="server" CssClass="btn btn-success btn-sm"
                                            CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="view"
                                            Style="padding: 1px 6px; border-radius: 5px; font-size: 12px;" ToolTip="View Bus Details"><i class="fa fa-eye"></i></asp:LinkButton>


                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle HorizontalAlign="Left" CssClass="gridview" />
                        </asp:GridView>
                        <div class="Row" id="divVerified" runat="server" visible="true">
                            <div class="col-12 text-center mt-9">
                                <span style="font-size: 40px; color: lightgray; font-weight: bold">No Record Found</span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-4">
                <div class="card">
                    <div class="card-body" style="min-height: 700px">
                        <div class="card-header">
                            Faulty Buses
                        </div>
                        <asp:GridView ID="grdFaulty" OnRowCommand="grdFaulty_RowCommand" OnPageIndexChanging="grdFaulty_PageIndexChanging" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                            GridLines="None" DataKeyNames="busno"
                            class="table table-hover mb-0 mt-2" PageSize="8" Font-Size="13px">
                            <Columns>
                                <asp:TemplateField HeaderText="Bus No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDepot" runat="server" CssClass="text-uppercase" Text='<%#  Eval("busno") %>'></asp:Label><br />
                                        <asp:Label ID="lblServiceCode" runat="server" Text='<%#  Eval("bustype") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Service Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTripCode" runat="server" Text='<%#  Eval("serivename") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="bntView" runat="server" CssClass="btn btn-success btn-sm"
                                            CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="view"
                                            Style="padding: 1px 6px; border-radius: 5px; font-size: 12px;" ToolTip="View Bus Details"><i class="fa fa-eye"></i></asp:LinkButton>


                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle HorizontalAlign="Left" CssClass="gridview" />
                        </asp:GridView>
                        <div class="Row" id="divFaulty" runat="server" visible="true">
                            <div class="col-12 text-center mt-9">
                                <span style="font-size: 40px; color: lightgray; font-weight: bold">No Record Found</span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
            <div class="row">
        <cc1:ModalPopupExtender ID="mpBusData" runat="server" PopupControlID="pnlBusData" TargetControlID="Button146456"
            CancelControlID="LinkButton19898" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlBusData" runat="server" Style="position: fixed;">
            <div class="modal-content mt-1">
                <div class="modal-header">
                    <div class="col-md-10">
                        <h3 class="m-0">Bus Details</h3>
                    </div>
                    <div class="col-md-2 text-right">
                        <asp:LinkButton ID="LinkButton19898" runat="server" UseSubmitBehavior="false" Style="color: Red; cursor: pointer;"><i class="fa fa-times fa-2x"></i></asp:LinkButton>
                    </div>
                </div>
                <div class="modal-body p-0">
                    <div class="card" style="font-size: 12px; min-height: 100px; min-width: 800px">
                        <div class="row mt-0">
                            <div class="col-sm-12 flex-column d-flex stretch-card ">
                                <div class="card-body table table-responsive">
                                    <asp:GridView ID="gvBusSearch" runat="server" GridLines="None" CssClass="w-100" AllowPaging="true"
                                        PageSize="4"  AutoGenerateColumns="false" ShowHeader="false">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <div class="row pb-2" style="border-bottom: 1px solid #f3eaea;">
                                                        <div class="col">
                                                            <div class="row">
                                                                <div class="col">
                                                                    <p class="mb-0">
                                                                        <asp:Label ID="Label18" runat="server" Font-Bold="true" Text="Bus No. -"></asp:Label>
                                                                        <asp:Label ID="Label14" runat="server" Font-Bold="true" CssClass="text-uppercase" ForeColor="Green" Text='<%#Eval("busregistration_no") %> '></asp:Label><br />
                                                                        <asp:Label ID="Label23" runat="server" Font-Bold="true" Text="Depot -"></asp:Label>
                                                                        <asp:Label ID="Label25" runat="server" Font-Bold="true" Text='  <%#Eval("officename") %>'></asp:Label>

                                                                        (<asp:Label ID="Label24" runat="server" Font-Bold="true" ForeColor="RED" Text='<%#Eval("officeid") %> '></asp:Label>)
                                                                       
                                                                    </p>

                                                                </div>
                                                                <div class="col-auto">
                                                                    <p class="mb-0 font-weight-bold">
                                                                        <asp:Label ID="Label27" runat="server" Font-Bold="true" Text="Service Type-"></asp:Label>


                                                                        <asp:Label ID="Label28" CssClass="text-primary" Font-Bold="true" runat="server" Text=' <%#Eval("servicetype_nameen") %> '></asp:Label>
                                                                        <br />
                                                                        <asp:Label ID="Label49" runat="server" Font-Bold="true" Text="Wheelbase -"></asp:Label>
                                                                        <asp:Label ID="Label50" runat="server" Font-Bold="true" ForeColor="RED" Text='<%#Eval("wheel_base") %> '></asp:Label>

                                                                    </p>


                                                                </div>
                                                                <div class="col text-right">
                                                                    <p class="mb-0">
                                                                        <asp:Label ID="Label17" runat="server" Font-Bold="true" Text="Chasis -"></asp:Label>


                                                                        <asp:Label ID="Label15" CssClass="text-primary" Font-Bold="true" runat="server" Text=' <%#Eval("chasis_no") %> '></asp:Label><br />
                                                                        <asp:Label ID="Label32" runat="server" Font-Bold="true" Text="Current Status -"></asp:Label>


                                                                        <asp:Label ID="Label33" ForeColor="Green" Font-Bold="true" runat="server" Text=' <%#Eval("current_status") %> '></asp:Label>

                                                                    </p>
                                                                </div>
                                                            </div>
                                                            <%--  <div class="row ">
                                                                <div class="col">
                                                                   
                                                                    <asp:Label ID="Label43" runat="server" Font-Bold="true" ForeColor="Orange" Text='<%#Eval("servicetype_nameen") %> '></asp:Label><br />

                                                                </div>
                                                            </div>--%>
                                                        </div>
                                                    </div>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle CssClass="pagination-ys" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <div style="visibility: hidden;">
                <asp:Button ID="Button146456" runat="server" Text="" />
            </div>
        </asp:Panel>
    </div>
        <div class="row">
            <cc1:ModalPopupExtender ID="mpVerifybus" runat="server" PopupControlID="pnlverifybus"
                CancelControlID="lbtnclose" TargetControlID="Button8" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlverifybus" runat="server" Style="position: fixed;">
                <div class="card" style="min-height: 300px; width: 500px">
                    <div class="card-header">
                        <div class="row">
                            <div class="col-lg-6">
                                <h3 class="card-title text-left mb-0">Bus Verification
                                </h3>
                            </div>
                            <div class="col-lg-6">
                                <asp:LinkButton ID="lbtnclose" runat="server" CssClass="text-danger float-right"> <i class="fa fa-times"></i> </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="card-body text-left pt-2" style="overflow: auto;">
                        <div class="row ">
                            <div class="col-lg-12 ">
                                 <asp:Label runat="server" ForeColor="red" Text="Please Note:"></asp:Label>
                                
                                <p>1- When Bus status marked to Okay, then bus will be available for duties.</p>
                                <p>2-When Bus status marked to Faulty, then bus will not be available for duties.</p>
                            </div>
                            
                        </div>
                        <div class="row mt-3">
                            <div class="col-lg-4 text-right">
                                <asp:Label runat="server" Text="Bus Status"></asp:Label>
                            </div>
                            <div class="col-lg-6">
                                <asp:DropDownList runat="server" ID="ddlStatus" CssClass="form-control">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                    <asp:ListItem Value="2">Okay</asp:ListItem>
                                    <asp:ListItem Value="6">Faulty</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row mt-3">
                            <div class="col-lg-4">
                                </div>
                            <div class="col-lg-8">
                                <%--<center>--%>
                                <asp:LinkButton runat="server" ID="lbtnVerify" OnClick="lbtnVerify_Click" CssClass="btn btn-primary btn-sm text-center">Verify</asp:LinkButton>
                                <asp:LinkButton runat="server" CssClass="btn btn-danger btn-sm text-center">Close</asp:LinkButton>
                                    <%--</center>--%>
                            </div>
                        </div>
                    </div>
                <div style="visibility: hidden;">
                    <asp:Button ID="Button8" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>
        <div class="row">
            <cc1:ModalPopupExtender ID="mpConfirmation" runat="server" PopupControlID="pnlConfirmation"
                CancelControlID="lbtnNoConfirmation" TargetControlID="Button4" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlConfirmation" runat="server" Style="position: fixed; display: none">
                <div class="card" style="width: 350px;">
                    <div class="card-header">
                        <h4 class="card-title text-left mb-0">Please Confirm
                        </h4>
                    </div>
                    <div class="card-body text-left pt-2" style="min-height: 100px;">
                        <asp:Label ID="lblConfirmation" runat="server" Text="Do you want to save ?"></asp:Label>
                        <div style="width: 100%; margin-top: 20px; text-align: right;">
                            <asp:LinkButton ID="lbtnYesConfirmation" OnClick="lbtnYesConfirmation_Click" runat="server" CssClass="btn btn-success btn-sm"> <i class="fa fa-check"></i> Yes </asp:LinkButton>
                            <asp:LinkButton ID="lbtnNoConfirmation" runat="server" CssClass="btn btn-danger btn-sm ml-2"> <i class="fa fa-times"></i> No </asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div style="visibility: hidden;">
                    <asp:Button ID="Button4" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>

