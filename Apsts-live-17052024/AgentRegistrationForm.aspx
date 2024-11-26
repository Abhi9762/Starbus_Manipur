<%@ Page Title="" Language="C#" MasterPageFile="~/mainMasterPage.master" AutoEventWireup="true" CodeFile="AgentRegistrationForm.aspx.cs" Inherits="AgentRegistrationForm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        ol, ul, dl{
            font-size:12pt;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="ftco-section" style="min-height: 70vh; padding-top: 2em;">
        <div class="container">
            <div class="row">
                <div class="col-md-12 wrap-about ftco-animate text text-dark p-4">
                    <h3>An opportunity to start your own travel agency  </h3>
                    <h3>1. Benefits:-</h3>
                    <ul>
                        <li>Unlimited number of tickets can be booked </li>
                        <li>Earn huge commissions on every booking</li>
                        <li>Book all type of Tickets </li>
                        <li>Earn regular Income  </li>
                        <li>No requirement of Trade License to become an MSTS authorized agent </li>
                        <li>Online wallet top-up facility.</li>
                        <li>Instant agent booking commission</li>
                        <li>Faster & hassle-free ticket booking system as the amount will get directly deducted from the wallet</li>
                    </ul>
                    <h3>2. Pre-requisites/Eligibility Criterion:-</h3>
                    <ul>
                        <li>The booking agent should be a citizen of India. Must not be lunatic or insolvent and no criminal case against him should be pending before the court </li>
                        <li>The booking agent must have an office, computer and internet connection at the place where the reservation is made. The Corporation reserves the right to appoint more than one booking agent at one place if it so desires. </li>
                        <li>Preference will be given to advance reservation booking agent having experience of doing any type of booking related to Rail/Air etc and valid agent of IRCTC.</li>
                    </ul>
                    <h3>Documents Required:-</h3>
                    <ul>
                        <li><b>ID Proof</b> (Aadhar Card/PAN Card/Voter Card/Driving License)</li>
                        <li><b>Address Proof </b>(Aadhar Card/PAN Card/Voter Card/Driving License/Ration Card)</li>
                        <li><b>Experience Documents</b></li>
                        <li><b>Legal Status Document</b>(if in partnership)</li>
                    </ul>
                    <h3>3. Simple steps to become an Agent:-</h3>
                    <ol>
                        <li><b>Online Application submission :</b>
                            <br />
                            Fulfill all pre-requisites and Apply here 
                        </li>
                        <li><b>Application Scrutiny </b>
                            <br />
                            After receiving application form our team will complete KYC and inform you   about the next step
                        </li>
                        <li><b>Scrutiny  Fee  Deposit an Creation of Account </b>
                            <br />
                            All successful KYC, you have will deposit the fee online. Soon after receiving the fee, you will become our agent and system will create an account for you.
                        </li>
                        <li><b>Start Business</b>
                            <br />
                            After getting account credentials you are ready for business.
                        </li>
                    </ol>

                      <div class="row mb-2 mt-4 offset-2">
                       

                            <span style="color: #0d558d;">&nbsp;&nbsp;
                            <asp:CheckBox ID="chkTOC" runat="server" Text=" I have read about Pre-requisites/Eligibility Criterion/Documents Required for applying as a agent" /></span>
                            
                    
                    </div>
                    <div class="w-100 mb-5 text-center">
                        <asp:LinkButton runat="server" ID="lbtnProceed" OnClick="lbtnProceed_Click" CssClass="btn btn-primary py-3 px-4" Text="Proceed" Font-Size="12pt"></asp:LinkButton>
                    </div>
                </div>
            </div>

                <div class="row">
            <cc1:ModalPopupExtender ID="mpError" runat="server" PopupControlID="pnlError" CancelControlID="LinkButton1"
                TargetControlID="Button3" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlError" runat="server" Style="position: fixed;">
                <div class="card" style="width: 400px; border-radius: 4px;">
                    <div class="card-header">
                        <h5 class="mt-1 mb-2">Information
                        </h5>
                    </div>
                    <div class="card-body text-left pt-2" style="min-height: 100px;">

                        <asp:Label ID="lblerrmsg" runat="server" ForeColor="Black" Style="font-size: 17px; line-height: 23px;"></asp:Label>
                        <div style="width: 100%; margin-top: 20px; text-align: right;">
                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-success" Style="height: 30px; width: 90px; padding-top: 4px; font-size: 10pt; border-radius: 4px;"> <b>OK</b></asp:LinkButton>
                        </div>
                    </div>
                </div>
                <br />
                <div style="visibility: hidden;">
                    <asp:Button ID="Button3" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>
        </div>
    </section>
</asp:Content>



