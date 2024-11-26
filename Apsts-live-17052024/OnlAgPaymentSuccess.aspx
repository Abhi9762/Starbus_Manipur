<%@ Page Title="" Language="C#" MasterPageFile="~/mainMasterPage.master" AutoEventWireup="true" CodeFile="OnlAgPaymentSuccess.aspx.cs" Inherits="OnlAgPaymentSuccess" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="ftco-section" style="min-height: 70vh; padding-top: 4em;">
        <div class="container">
            <div class="row no-gutters">

                <div class="col-md-12 wrap-about text text-dark shadow px-4 py-3">
                    <div class="row">
                        <div class="col-md text-center">
                            <h3 class="mb-0">AGENT REGISTRATION SECURITY FEE PAYMENT STATUS</h3>
                            <p><b>To become Arunachal Pradesh State Transport Agent</b></p>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="row">
                            <div class="col-md-12 text-center">
                                <img src="images/successful.png" style="width: 100px;" />
                                <br />
                                <p class="mt-4" style="font-size: 22px; margin: 30px 0px;">
                                    <b>Dear
                                        <asp:Label runat="server" ID="lblName"></asp:Label>,</b> Your Transaction has been completed successfully.<br />
                                    Your request has been approved. your login credential send by registered Email and Mobile number.
                                </p>
                            </div>
                        </div>
                        <div class="row mt-4">
                            <div class="col-md-12 text-center">
                                <asp:LinkButton ID="btnprint" runat="server" OnClick="btnprint_Click" Visible ="false"  CssClass="submit-btn btn btn-success mr-2"><i class="fa fa-print"></i> Print Receipt</asp:LinkButton>
                                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="submit-btn btn btn-primary" PostBackUrl="~/Home.aspx"><i class="fa fa-home "></i> Back to Home</asp:LinkButton>
                            </div>
                        </div>

                    </div>
                </div>
            </div>


        </div>
    </section>
</asp:Content>



