<%@ Page Title="" Language="C#" MasterPageFile="~/mainMasterPage.master" AutoEventWireup="true" CodeFile="offersAll.aspx.cs" Inherits="offersAll" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   
    <div class="container">
        <div class="row py-4">
            <div class="col-lg-3">
                <h3>The festive season is right around the corner and that can only mean one thing: discounts, discounts and discounts. Renovate your home, upgrade your wardrobe or gift a loved one: our special discounts will make it hard for you to resist picking more than just one product. What will be part of the offers?
                </h3>
            </div>
            <div class="col-lg-9">
                <div class="row">
                    <asp:ListView ID="lvOffers" runat="server" OnItemDataBound="lvOffers_ItemDataBound" OnItemCommand="lvOffers_ItemCommand"
                        DataKeyNames="couponid, couponcode, coupontitle, discountdescription, discounttype, discounton, discountamount, maxdiscount_amount, validfrom_date, validto_date">
                        <ItemTemplate>
                            <div class="col-lg-4 col-md-12 col-sm-12">
                                <div class="card shadow">
                                    <asp:Image ID="imgWeb" runat="server" CssClass="card-img" Style="border-bottom-right-radius: 0; border-bottom-left-radius: 0;" />
                                    <div class="card-footer p-2">
                                        <div class="row">
                                            <div class="col">
                                                <h3 class="mb-0 text-blue"><%# Eval("coupontitle")%></h3>
                                                <h5 class="text-black-50 mb-0">Code : <%# Eval("couponcode")%></h5>
                                            </div>
                                            <div class="col-auto text-right">
                                                <asp:LinkButton ID="lbtnViewDetails" runat="server" CssClass="btn btn-outline-primary btn-icon-only" CommandName="VIEWDETAILS"><i class="fa fa-eye"></i> </asp:LinkButton>
                                            </div>
                                        </div>

                                    </div>
                                </div>

                            </div>
                        </ItemTemplate>
                    </asp:ListView>
                    <asp:Panel ID="pnlNoOffer" runat="server" Width="100%" Visible="false">
                        <div class="col-lg-12 col-md-12 col-sm-12 text-center">
                            <h1 class="text-muted">Offer will be available Soon.</h1>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
        <div class="row">
            <cc1:ModalPopupExtender ID="mpOfferDetail" runat="server" CancelControlID="btnClosempOfferDetail"
                TargetControlID="btnOpenmpOfferDetail" PopupControlID="pnlOfferDetail" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>

            <asp:Panel ID="pnlOfferDetail" Style="display: none;" runat="server">
                <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <div class="row w-100">
                                <div class="col">
                                    <h3 class="modal-title" id="modal-title-default">Offer Detail</h3>
                                </div>
                                <div class="col-auto text-right">
                                    <button type="button" id="btnClosempOfferDetail" class="btn btn-outline-danger btn-sm btn-icon-only" data-dismiss="modal"><i class="fa fa-times"></i></button>
                                </div>
                            </div>

                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-lg-4">
                                    <asp:Image ID="img" runat="server" CssClass="card-img" Style="width: 100%;" />
                                </div>
                                <div class="col-lg-8">
                                    <h2 class="mb-0">
                                        <asp:Label ID="lblTitle" runat="server"></asp:Label>
                                    </h2>
                                    <h4>
                                        <asp:Label ID="lblDescription" runat="server"></asp:Label>
                                    </h4>
                                    <h4>
                                        <span class="text-muted text-xs">Coupon Code : </span>
                                        <asp:Label ID="lblCode" runat="server"></asp:Label>
                                    </h4>
                                    <h4>
                                        <span class="text-muted text-xs">Discount : </span>
                                        <asp:Label ID="lblDiscountAmount" runat="server"></asp:Label>
                                        <asp:Label ID="lblDiscountOn" runat="server"></asp:Label>
                                        (Max
                                        <asp:Label ID="lblDiscountAmountMax" runat="server"></asp:Label>)
                                    </h4>
                                    <h4>
                                        <span class="text-muted text-xs">Valid Upto : </span>
                                        <asp:Label ID="lblValidUpto" runat="server"></asp:Label>
                                    </h4>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <div style="visibility: hidden;">
                <asp:Button ID="btnOpenmpOfferDetail" runat="server" />
            </div>
        </div>
    </div>
</asp:Content>

