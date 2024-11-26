<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/SysAdmmaster.master" AutoEventWireup="true" CodeFile="OnlagentVerification.aspx.cs" Inherits="Auth_OnlagentVerification" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .card .table td, .card .table th {
            padding: 10px 5px;
        }
    </style>
    <script type="text/javascript">
        $(window).on('load', function () {
            HideLoading();
        });
        function ShowLoading() {
            var div = document.getElementById("loader");
            div.style.display = "block";
        }
        function HideLoading() {
            var div = document.getElementById("loader");
            div.style.display = "none";
        }
    </script>

    
    <script type="text/javascript">
        $(document).ready(function () {
            var todayDate = new Date().getDate();
            var endD = new Date(new Date().setDate(todayDate));
            var currDate = new Date();

            $('[id*=txtvalidto]').datepicker({
                 startDate: "dateToday",
               // endDate: endD,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            });
        });
    </script>
    <link rel="stylesheet" href="../style.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container-fluid" style="padding-top: 25px; padding-bottom: 20px;">
        <div class="row">
            <div class="col-lg text-center">
                <div class="card">
                    <div class="card-body p-3" style="line-height: 20px;">
                        <label>
                            Pending For Verification
                            <br />
                            <span style='font-size: 9pt;'>(Online Request Submitted ) </span>
                        </label>
                        <br />
                        <asp:LinkButton ID="lbtnpendverify" runat="server" OnClick="lbtnpendverify_Click" Text="0" Style="font-size: 12pt; font-weight: 600;"></asp:LinkButton>
                    </div>
                </div>
            </div>
            <div class="col-lg text-center">
                <div class="card">
                    <div class="card-body p-3" style="line-height: 20px;">
                        <label>
                            Pending For Completion<br />
                            <span style='font-size: 9pt;'>(Online Request Verified ) </span>
                        </label>
                        <br />
                        <asp:LinkButton ID="lbtnpendcomplition" runat="server" OnClick="lbtnpendcomplition_Click" Text="0" Style="font-size: 12pt; font-weight: 600;"></asp:LinkButton>
                    </div>
                </div>
            </div>
            <div class="col-lg-auto text-center">
                <div class="card">
                    <div class="card-body p-3" style="line-height: 20px;">
                        <label>
                            Login Expired
                            <br />
                            <span style='font-size: 9pt;'>(Login and account validity both are expired ) </span>
                        </label>
                        <br />
                        <asp:LinkButton ID="lbtnloginexpire" runat="server" Text="0" OnClick="lbtnloginexpire_Click" Style="font-size: 12pt; font-weight: 600;"></asp:LinkButton>
                    </div>
                </div>
            </div>
            <div class="col-lg-auto text-center">
                <div class="card">
                    <div class="card-body p-3" style="line-height: 20px;">
                        <label>
                            All Agent List
                            <br />
                            <span style='font-size: 9pt;'>(Approved agent and validity is valid) </span>
                        </label>
                        <br />
                        <asp:LinkButton ID="lbtnallagnt" runat="server" Text="0" OnClick="lbtnallagnt_Click" Style="font-size: 12pt; font-weight: 600;"></asp:LinkButton>
                    </div>
                </div>
            </div>
            <div class="col-lg text-center">
                <div class="card">
                    <div class="card-body p-3" style="line-height: 20px;">
                        <label>
                            Deactivated Agent List
                            <br />
                            <span style='font-size: 9pt;'>(Pending for refund (security+wallet)) </span>
                        </label>
                        <br />
                        <asp:LinkButton ID="lbtnPendingrefund" runat="server" Text="0" OnClick="lbtnPendingrefund_Click" Style="font-size: 12pt; font-weight: 600;"></asp:LinkButton>
                    </div>
                </div>
            </div>
            
        </div>

        <div class="row">
            <div class="col-lg-12">
                <asp:Panel ID="pnlallag" runat="server" Visible="false">
                    <div class="card" style="min-height: 50vh;">
                        <div class="card-header p-2">
                            <div class="row">
                                <div class="col" style="font-size: 13pt; line-height: 19px;">
                                    <asp:Label ID="lblalagheading" runat="server" Text=""></asp:Label>
                                </div>
                                <div class="col-auto">
                                    <p class="mb-0 text-sm">Search for Agent Name/Agent Code/Mobile No.</p>
                                    <div class="form-group">
                                        <div class="input-group mb-0">
                                            <asp:TextBox ID="txtSearchAgentMIS" runat="server" AutoComplete="Off" CssClass="form-control" type="search"
                                                Placeholder="Type here. . .(min 3 char)" MaxLength="20"></asp:TextBox>
                                            <span class="input-group-text p-0">
                                                <asp:LinkButton ID="linkbtnSearchAgentMIS" runat="server" OnClick="linkbtnSearchAgentMIS_Click" OnClientClick="return ShowLoading();" class="btn btn-success py-1 px-3" Style="height: 100% !important; width: 100% !important;">
                                                       <i class="fa fa-search"></i></asp:LinkButton>
                                            </span>
                                            <span class="input-group-text p-0">
                                                <asp:LinkButton ID="linkbtnAllAgentMIS" runat="server" OnClick="linkbtnAllAgentMIS_Click" OnClientClick="return ShowLoading();" class="btn btn-warning py-1 px-3" Style="height: 100% !important; width: 100% !important;">
                                                    <i class="fa fa-sync-alt"></i></asp:LinkButton></span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="card-body">
                            <asp:GridView ID="grvAgents" runat="server" AutoGenerateColumns="False" GridLines="None" AllowSorting="true" AllowPaging="true"
                                PageSize="10" class="table" ShowHeader="false" OnRowCommand="grvAgents_RowCommand" OnRowDataBound="grvAgents_RowDataBound" DataKeyNames="reference_no,agent_name,contact_person,mobile_no,val_email,state_name,district_name,city_name,val_address,val_pincode,
                                pan_no,legal_status,l_status,experience_yn,val_experience,no_of_years,address_proof_type,id_proof_type,current_status,agent_code,valid_to,login_validity,expired_yn,vaal_status,cancel_refno,payment_orderno,statuson_datetime,val_amount,refund_refno">
                                <Columns>

                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <div class="row border-bottom pb-1">
                                                <div class="col">
                                                    <asp:Label ID="lblAGENTNAME" runat="server" Text='<%# Eval("agent_name") %>' Style="text-transform: capitalize;"></asp:Label>
                                                    (<asp:Label ID="lblREFERNCENO" runat="server" Font-Bold="true" Text='<%# Eval("reference_no") %>'></asp:Label>)
                                          <br />
                                                    <span>PAN No</span>
                                                    <asp:Label ID="Label6" runat="server" Text='<%# Eval("pan_no") %>' Style="text-transform: capitalize;" Font-Bold="true"></asp:Label>
                                                    <br />
                                                    <span>Booking Experiance </span>
                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("val_experience") %>' Style="text-transform: capitalize;" Font-Bold="true"></asp:Label>
                                                    <asp:LinkButton ID="lbtnViewRegistrationProof" runat="server" CommandName="ViewRegistrationProof" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                        class="btn-link" ToolTip="View Registration Of Proof"> View Proof </asp:LinkButton>
                                                </div>
                                                <div class="col">
                                                    <i class="fa fa-user"></i>
                                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("contact_person") %>' Style="text-transform: capitalize;"></asp:Label>
                                                    <asp:LinkButton ID="lbtnViewIDProof" runat="server" CommandName="ViewIDProof" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                        class="btn-link" ToolTip="View ID Proof"> View ID Proof </asp:LinkButton>
                                                    <br />
                                                    <i class="fa fa-mobile"></i>
                                                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("mobile_no") %>' Style="text-transform: capitalize;"></asp:Label>
                                                    <i class="fa fa-envelope ml-2"></i>
                                                    <asp:Label ID="Label5" runat="server" Text='<%# Eval("val_email") %>' Style="text-transform: capitalize;"></asp:Label>
                                                    <br />
                                                    <i class="fa fa-home"></i><%# Eval("city_name") %> ,<%# Eval("state_name") %>
                                                    <asp:LinkButton ID="lbtnViewAddressProof" runat="server" CommandName="ViewAddressProof" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                        class="btn-link" ToolTip="View Address Proof"> View Proof </asp:LinkButton>
                                                </div>
                                                <div class="col">
                                                    <span>legal Status</span><br />
                                                    <asp:Label ID="Label7" runat="server" Text='<%# Eval("l_status") %>' Style="text-transform: capitalize;" Font-Bold="true"></asp:Label><br />
                                                    <asp:LinkButton ID="lbtnViewCertifiedCopy" runat="server" CommandName="ViewCertifiedCopy" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                        class="btn-link" ToolTip="View Certified Copy Of Legal Status"> View Certified Copy</asp:LinkButton>
                                                </div>
                                                <div class="col">
                                                    <span>Facility</span><br />
                                                    <asp:Label ID="lblfacility" runat="server" Text='<%# Eval("val_facility") %>' Style="text-transform: capitalize;" Font-Bold="true"></asp:Label>
                                                </div>
                                                <div class="col">
                                                    <span>Agent Code</span>
                                                    <asp:Label ID="lblAGENTCODE" runat="server" Text='<%# Eval("agent_code") %>' Style="text-transform: capitalize;" Font-Bold="true"></asp:Label>
                                                    <br />
                                                    <span>Account Validity</span>
                                                    <asp:Label ID="lblVALIDTO" runat="server" Text='<%# Eval("valid_to") %>' Style="text-transform: capitalize;" Font-Bold="true"></asp:Label>
                                                    <br />
                                                    <span>Login Validity</span>
                                                    <asp:Label ID="Label8" runat="server" Text='<%# Eval("login_validity") %>' Style="text-transform: capitalize;" Font-Bold="true"></asp:Label>
                                                </div>
                                                <div class="col-auto text-right">
                                                    <span>Current Status</span>
                                                    <asp:Label ID="lblCURRENTSTATUS" runat="server" Text='<%# Eval("current_status") %>' Style="text-transform: capitalize;" Font-Bold="true"></asp:Label>
                                                    <br />
                                                    <asp:LinkButton ID="lbtnDeactivate" runat="server" CommandName="Deactivate" OnClientClick="return ShowLoading();"
                                                        CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' class="btn btn-danger btn-sm"
                                                        ToolTip="Deactivate Agent "> <i class="fa fa-times" title="Deactivate Agent "></i> Deactivate</asp:LinkButton>

                                                    <asp:LinkButton ID="lbtnValidity" Visible="false" runat="server" CommandName="Validity" OnClientClick="return ShowLoading();"
                                                        CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' class="btn btn-success btn-sm"
                                                        ToolTip="Update Login Validity">  <i class="fa fa-check" title="Update Login Validity"></i> Update Login Validity</asp:LinkButton>

                                                    <asp:LinkButton ID="lbtnRefund" runat="server" Visible="false" CommandName="Refund" OnClientClick="return ShowLoading();"
                                                        CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' class="btn btn-primary btn-sm"
                                                        ToolTip="Refund Agent Security and Wallet Balance">  <i class="fa fa-rupee-sign" title="Refund Agent Security and Wallet Balance"></i> Refund</asp:LinkButton>

                                                    <asp:LinkButton ID="lbtnpwd" runat="server" CommandName="ChndPwd" OnClientClick="return ShowLoading();"
                                                        CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' class="btn btn-warning btn-sm"
                                                        ToolTip="Change Agnet password">  <i class="fa fa-lock" title="Change Agnet password"></i> Change Password</asp:LinkButton>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

                            <asp:Panel ID="pnlNoAgent" runat="server" CssClass="w-100 p-5 text-center text-black-50">
                                <i class="fa fa-user-lock fa-5x"></i>
                                <h2 class="mt-2 text-black-50">
                                    <asp:Label ID="lblNoAgent" runat="server"></asp:Label></h2>
                            </asp:Panel>

                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlpendingag" runat="server" Visible="true">
                    <div class="card" style="min-height: 50vh;">
                        <div class="card-header p-2">
                            <div class="row">
                                <div class="col" style="font-size: 13pt; line-height: 19px;">
                                    <asp:Label ID="lblpendingheading" runat="server" Text=""></asp:Label>

                                </div>
                                <div class="col-auto" style="font-size: 13pt; font-weight: 600;">
                                    <p class="mb-0 text-sm">Request Type</p>
                                    <asp:DropDownList ID="ddlRequestType" runat="server" OnSelectedIndexChanged="ddlRequestType_SelectedIndexChanged" CssClass="form-control" AutoPostBack="true">
                                        <asp:ListItem Value="0" Text="All Request" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="N" Text="New Request"></asp:ListItem>
                                        <asp:ListItem Value="R" Text="Renew Request"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="card-body p-1">
                            <asp:GridView ID="grvAgentsPending" runat="server" AutoGenerateColumns="False" GridLines="None" AllowSorting="true" AllowPaging="true" ShowHeader="true"
                                PageSize="15" class="table" OnRowCommand="grvAgentsPending_RowCommand" OnRowDataBound="grvAgentsPending_RowDataBound" DataKeyNames="reference_no,agent_name,contact_person,mobile_no,val_email,state_name,district_name,city_name,val_address,val_pincode,pan_no,legal_status,l_status,experience_yn,val_experience,no_of_years,address_proof_type,id_proof_type,current_status,pending_since">
                                <Columns>

                                    <asp:TemplateField HeaderText="Application Deatils">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAGENTNAME" runat="server" Text='<%# Eval("agent_name") %>' Style="text-transform: capitalize;"></asp:Label>
                                            (<asp:Label ID="lblREFERNCENO" runat="server" Font-Bold="true" Text='<%# Eval("reference_no") %>'></asp:Label>)
                                          <br />
                                            <span>PAN No</span>
                                            <asp:Label ID="Label6" runat="server" Text='<%# Eval("PAN_NO") %>' Style="text-transform: capitalize;" Font-Bold="true"></asp:Label>
                                            <br />
                                            <span>Booking Experiance </span>
                                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("val_experience") %>' Style="text-transform: capitalize;" Font-Bold="true"></asp:Label>
                                            <br />
                                            <span>legal Status</span>
                                            <asp:Label ID="Label7" runat="server" Text='<%# Eval("l_status") %>' Style="text-transform: capitalize;" Font-Bold="true"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Contact Deatils">
                                        <ItemTemplate>
                                            <i class="fa fa-user"></i>
                                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("contact_person") %>' Style="text-transform: capitalize;"></asp:Label>
                                            <br />
                                            <i class="fa fa-mobile"></i>
                                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("mobile_no") %>' Style="text-transform: capitalize;"></asp:Label>
                                            <i class="fa fa-envelope ml-2"></i>
                                            <asp:Label ID="Label5" runat="server" Text='<%# Eval("val_email") %>' Style="text-transform: capitalize;"></asp:Label>
                                            <br />
                                            <i class="fa fa-home"></i><%# Eval("city_name") %> ,<%# Eval("state_name") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Documents">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnViewIDProof" runat="server" CommandName="ViewIDProof" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                class="btn-link" ToolTip="View ID Proof">
                                    ID Proof
                                            </asp:LinkButton>
                                            <br />
                                            <asp:LinkButton ID="lbtnViewAddressProof" runat="server" CommandName="ViewAddressProof" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                class="btn-link" ToolTip="View Address Proof">
                                   Address Proof
                                            </asp:LinkButton>
                                            <br />
                                            <asp:LinkButton ID="lbtnViewRegistrationProof" runat="server" CommandName="ViewRegistrationProof" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                class="btn-link" ToolTip="View Registration Of Proof">
                                        Registration Proof
                                            </asp:LinkButton>
                                            <br />
                                               <asp:LinkButton ID="lbtnViewCertifiedCopy" runat="server" CommandName="ViewCertifiedCopy" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                class="btn-link" ToolTip="View Certified Copy Of Legal Status">
                                    Legal Status Proof</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Facility Applicable">
                                        <ItemTemplate>

                                            <asp:Label ID="lblfacility" runat="server" Text='<%# Eval("val_facility") %>' Style="text-transform: capitalize;" Font-Bold="true"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Pending Days">
                                        <ItemTemplate>
                                            <span>Pending Since</span><br />
                                            <asp:Label ID="lblpendingsince" runat="server" Text='<%# Eval("pending_since") %>' Style="text-transform: capitalize;" Font-Bold="true"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>


                                              <asp:LinkButton ID="lbtnview" runat="server" CommandName="ViewRequest" OnClientClick="return ShowLoading();"
                                                CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' class="btn btn-success btn-sm"
                                                ToolTip="Details Online Request"> <i class="fa fa-eye" title="Verify Agent Request"></i> View</asp:LinkButton>
                                            <%--<asp:LinkButton ID="lbtnVerify" runat="server" CommandName="VerifyAgent" OnClientClick="return ShowLoading();"
                                                CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' class="btn btn-success btn-sm"
                                                ToolTip="Vrify Agent Request"> <i class="fa fa-check" title="Verify Agent Request"></i> Approve</asp:LinkButton>

                                            <asp:LinkButton ID="lbtnReject" runat="server" CommandName="RejectAgent" OnClientClick="return ShowLoading();"
                                                CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' class="btn btn-danger btn-sm"
                                                ToolTip="Reject Agent Request">  <i class="fa fa-times" title="Reject Agent Request"></i> Reject</asp:LinkButton>--%>

                                            <asp:LinkButton ID="lbtnsms" runat="server" CommandName="SMS" OnClientClick="return ShowLoading();"
                                                CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' class="btn btn-success btn-sm"
                                                ToolTip="SMS for Request Complition">  <i class="fa fa-mobile" title="SMS for Request Complition"></i> SMS</asp:LinkButton>

                                            <asp:LinkButton ID="lbtnemail" runat="server" CommandName="Mail" OnClientClick="return ShowLoading();"
                                                CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' class="btn btn-danger btn-sm"
                                                ToolTip="Email for Request Complition">  <i class="fa fa-envelope" title="Email for Request Complition"></i> Email</asp:LinkButton>

                                            <asp:LinkButton ID="lbtncancel" runat="server" CommandName="CancelRequest" OnClientClick="return ShowLoading();"
                                                CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' class="btn btn-warning btn-sm"
                                                ToolTip="Cancellation Online Request">  <i class="fa fa-envelope" title="Cancellation Online Request"></i> Cancel Request</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle BackColor="#e6e6e6" />
                            </asp:GridView>
                            <asp:Panel ID="pnlNoPendingAgent" runat="server" CssClass="w-100 p-5 text-center text-black-50">
                                <i class="fa fa-user-cog fa-5x"></i>
                                <h2 class="mt-2 text-black-50">
                                    <asp:Label ID="lblNoPendingAgent" runat="server"></asp:Label>
                                </h2>
                            </asp:Panel>

                                 <asp:Panel ID="pnlview" runat="server" Visible="false">

                                <div class="row">
                                    <div class="col-lg-12">
                                        <h3>Application Deatils</h3>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-3">
                                        <span>Name</span><br />
                                        <asp:Label ID="lblname" runat="server" Text="Name(1234)"></asp:Label>
                                    </div>
                                    <div class="col-lg-3">
                                        <span>PAN No.</span><br />
                                        <asp:Label ID="lblpanno" runat="server" Text="Name(1234)"></asp:Label>
                                    </div>
                                    <div class="col-lg-3">
                                        <span>Booking Experiance</span><br />
                                        <asp:Label ID="lblbookingexp" runat="server" Text="Name(1234)"></asp:Label>
                                    </div>
                                    <div class="col-lg-3">
                                        <span>legal Status</span><br />
                                        <asp:Label ID="lbllegalstatus" runat="server" Text="Name(1234)"></asp:Label>
                                    </div>
                                </div>
                                <div class="row mt-3">
                                    <div class="col-lg-12">
                                        <h3>Contact Deatils</h3>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-3">
                                        <span>Person Name</span><br />
                                        <asp:Label ID="lblpersonname" runat="server" Text="Name(1234)"></asp:Label>
                                    </div>
                                    <div class="col-lg-3">
                                        <span>Mobile No.</span><br />
                                        <asp:Label ID="lblmobileno" runat="server" Text="Name(1234)"></asp:Label>
                                    </div>
                                    <div class="col-lg-3">
                                        <span>Eamil ID</span><br />
                                        <asp:Label ID="lblemail" runat="server" Text="Name(1234)"></asp:Label>
                                    </div>
                                    <div class="col-lg-3">
                                        <span>Address</span><br />
                                        <asp:Label ID="lbladdress" runat="server" Text="Name(1234)"></asp:Label>
                                    </div>
                                </div>
                                <div class="row mt-3">
                                    <div class="col-lg-12">
                                        <h3>Documents</h3>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-6">
                                        <h5>ID Proof</h5>
                                        <asp:Literal ID="ltControl_idproof" runat="server"></asp:Literal>
                                    </div>
                                    <div class="col-lg-6">
                                        <h5>Address Proof</h5>
                                        <asp:Literal ID="ltControl_addressproof" runat="server"></asp:Literal>
                                    </div>
                                </div>

                                <div class="row mt-3 mb-4">
                                    <div class="col-lg-12 text-right">
                                        <asp:LinkButton ID="lbtnVerify" runat="server" OnClick="lbtnVerify_Click" OnClientClick="return ShowLoading();" class="btn btn-success"
                                            ToolTip="Vrify Agent Request"> <i class="fa fa-check" title="Verify Agent Request"></i> Approve</asp:LinkButton>

                                        <asp:LinkButton ID="lbtnReject" runat="server" OnClick="lbtnReject_Click" OnClientClick="return ShowLoading();" class="btn btn-danger"
                                            ToolTip="Reject Agent Request">  <i class="fa fa-times" title="Reject Agent Request"></i> Reject</asp:LinkButton>

                                        <asp:LinkButton ID="lbtncancel" runat="server" OnClick="lbtncancel_Click" OnClientClick="return ShowLoading();" class="btn btn-warning"
                                            ToolTip="Cancel View Details">  <i class="fa fa-backward" title="Cancel View Details"></i> Cancel</asp:LinkButton>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>
    <div class="row">
        <cc1:ModalPopupExtender ID="mpError" runat="server" PopupControlID="pnlmpError" CancelControlID="lbtnClosempError"
            TargetControlID="btnOpenmpError" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlmpError" runat="server" Style="position: fixed; display: none;">
            <div class="card" style="min-width: 350px; max-width: 90vw; max-height: 90vh;">
                <div class="card-header">
                    <div class="card-title m-0">
                        <asp:Label ID="lblHeadermpError" runat="server" CssClass="font-weight-600"></asp:Label>
                    </div>
                </div>
                <div class="card-body text-left overflow-auto" style="min-height: 100px;">
                    <asp:Label ID="lblMessagempError" runat="server"></asp:Label>
                </div>
                <div class="card-footer text-right">
                    <asp:LinkButton ID="lbtnClosempError" runat="server" CssClass="btn btn-warning btn-sm"> <i class="fa fa-check"></i> OK </asp:LinkButton>
                </div>

            </div>
            <div style="visibility: hidden; height: 0px;">
                <asp:Button ID="btnOpenmpError" runat="server" Text="" />
            </div>
        </asp:Panel>
    </div>

    <div class="row">
        <cc1:ModalPopupExtender ID="mpAgentVerification" runat="server" PopupControlID="PanelAgentVerification"
            CancelControlID="lbtnVerificationNo" TargetControlID="Button1" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="PanelAgentVerification" runat="server" Style="position: fixed;">
            <div class="card" style="min-width: 300px; max-width: 90vw; max-height: 90vh;">
                <div class="card-header">
                    <div class="row">
                        <div class="col-md-12 text-left" style="font-size: 18px; font-weight: 600;">
                            Please Confirm
                        </div>
                    </div>
                </div>

                <div class="card-body text-left overflow-auto" style="min-height: 100px; max-width: 600px;">
                    <div class="row pb-3 mt-2" id="dvreject" runat="server" visible="false">
                        <div class="col-lg-2"></div>
                        <div class="col-lg-4 text-right">Reject Reason<span style="color: Red;">*</span> </div>
                        <div class="col-lg-4">
                              <asp:DropDownList ID="ddlrejectreason" runat="server" CssClass="form-control" Visible="false">
                            </asp:DropDownList>
                          <asp:TextBox ID="txtrejectreason" runat="server" TextMode="MultiLine" CssClass="form-control" Visible="false" style="resize:none;height:65px;"></asp:TextBox>
                            <asp:TextBox ID="txtcancelremark" runat="server" TextMode="MultiLine" CssClass="form-control" Visible="false"></asp:TextBox>
                        </div>
                        <div class="col-lg-2"></div>
                    </div>
                    <div class="row pb-3 mt-2">
                        <div class="col-lg-2"></div>
                        <div class="col-lg-8 text-center">
                            <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="You are verifying an agent."
                                Style="font-weight: 600;"></asp:Label>
                        </div>
                        <div class="col-lg-2"></div>
                        <div class="col-lg-2"></div>
                        <div class="col-lg-8 mt-2 text-center">
                            <asp:LinkButton ID="lbtnVerificationNo" runat="server" CssClass="btn btn-danger"> <i class="fa fa-times"></i> No </asp:LinkButton>
                            <asp:LinkButton ID="lbtnVerificationYes" runat="server" OnClick="lbtnVerificationYes_Click" CssClass="btn btn-success"> <i class="fa fa-check"></i> Yes </asp:LinkButton>
                        </div>
                        <div class="col-lg-2"></div>
                    </div>
                </div>

            </div>
            <br />
            <div style="visibility: hidden; height: 0px;">
                <asp:Button ID="Button1" runat="server" Text="" />
            </div>
        </asp:Panel>
    </div>

    <div class="row">
        <cc1:ModalPopupExtender ID="mpdocment" runat="server" PopupControlID="pnlviewdocment" CancelControlID="btnclose"
            TargetControlID="Button5" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlviewdocment" runat="server" Style="display: none;">
            <div class="card" style="margin-top: 100px;">
                <div class="card-header">

                    <div class="row">
                        <div class="col-lg-6">
                            <h4 class="card-title text-left mb-0">View Document
                            </h4>
                        </div>
                        <div class="col-lg-6 text-right">
                            <asp:LinkButton ID="btnclose" runat="server" CssClass="btn btn-danger"> <i class="fa fa-times"></i></asp:LinkButton>

                        </div>
                    </div>

                </div>
                <div class="card-body text-left pt-2" style="min-height: 100px;">
                    <embed id="tkt" runat="server" src="" style="height: 85vh; width: 45vw" />
                    <%-- <embed src="../PassUTC/ViewDocument.aspx" style="height: 85vh; width: 80vw" />--%>
                    <div style="visibility: hidden;">
                        <asp:Button ID="Button5" runat="server" Text="" />
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>

    <div class="row">
        <cc1:ModalPopupExtender ID="mpUpdateValidity" runat="server" PopupControlID="pnlUpdateValidity"
            CancelControlID="lbtnvalidityno" TargetControlID="Button2" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlUpdateValidity" runat="server" Style="position: fixed;">
            <div class="card" style="width: 600px; max-height: 90vh;">
                <div class="card-header">
                    <h4>Update Agent Login Validity</h4>
                </div>
                <div class="card-body text-left overflow-auto" style="min-height: 100px; max-width: 90vw;">
                    <div class="row pb-2 mt-2">
                        <div class="col-lg-12 text-center">
                            <span class="text-muted">Login validity of agent </span>
                            <asp:Label ID="lblagname" runat="server" Text="" Style="font-weight: 600;"></asp:Label>
                            (
                            <asp:Label ID="lblagcode" runat="server" Text="" Style="font-weight: 600;"></asp:Label>
                            ) is 
                            <asp:Label ID="lblvalidto" runat="server" Text="" Style="font-weight: 600;"></asp:Label>
                        </div>
                    </div>
                    <div class="row pb-2 mt-2">
                        <div class="col-lg-4 text-center"></div>
                        <div class="col-lg-4 text-center">
                            <span class="text-muted">Update Login Validity <span class="text-danger">*</span></span>
                            <asp:TextBox ID="txtvalidto" placeholder="DD/MM/YYYY" CssClass="form-control" runat="server"></asp:TextBox>
                            

                        </div>
                        <div class="col-lg-4 text-center"></div>
                    </div>
                    <div class="row">

                        <div class="col-lg-12 text-center">
                            <asp:LinkButton ID="lbtnvalidityno" runat="server" CssClass="btn btn-danger"> <i class="fa fa-times"></i> No </asp:LinkButton>
                            <asp:LinkButton ID="lbtnvalidityyes" runat="server" OnClick="lbtnvalidityyes_Click" CssClass="btn btn-success"> <i class="fa fa-check"></i> Yes </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <div style="visibility: hidden; height: 0px;">
                <asp:Button ID="Button2" runat="server" Text="" />
            </div>
        </asp:Panel>
    </div>
    <div class="row">
        <cc1:ModalPopupExtender ID="mpchangepassowrd" runat="server" PopupControlID="pnlchangepassowrd"
            CancelControlID="lbtnNoConfirmation" TargetControlID="Button4" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlchangepassowrd" runat="server" Style="position: fixed;">
            <div class="card" style="width: 350px;">
                <div class="card-header">
                    <h4 class="card-title text-left mb-0">Confirmation
                    </h4>
                </div>
                <div class="card-body text-left pt-2" style="min-height: 100px;">
                    <asp:Label ID="lblConfirmation" runat="server" Text="Do you want to Change Agent Password ?"></asp:Label>
                    <div style="width: 100%; margin-top: 20px; text-align: right;">
                        <asp:LinkButton ID="lbtnYesConfirmation" runat="server" OnClick="lbtnYesConfirmation_Click" CssClass="btn btn-success btn-sm"> <i class="fa fa-check"></i> Yes </asp:LinkButton>
                        <asp:LinkButton ID="lbtnNoConfirmation" runat="server" CssClass="btn btn-danger btn-sm ml-2"> <i class="fa fa-times"></i> No </asp:LinkButton>
                    </div>
                </div>
            </div>
            <div style="visibility: hidden;">
                <asp:Button ID="Button6" runat="server" Text="" />
            </div>
        </asp:Panel>
    </div>







    <div class="row">
        <cc1:ModalPopupExtender ID="mpDeactivation" runat="server" PopupControlID="pnlDeactivation" CancelControlID="lbtnNo"
            TargetControlID="Button3" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlDeactivation" runat="server" Style="position: fixed; display: none;">
            <div class="card" style="min-width: 350px; max-width: 90vw; max-height: 90vh;">
                <div class="card-header">
                    <div class="card-title m-0">
                        <asp:Label ID="Label9" runat="server" Text="Deactivation of agent " CssClass="font-weight-600"></asp:Label>
                    </div>
                </div>
                <div class="card-body text-left overflow-auto" style="min-height: 100px;">
                    <p class="font-weight-600">Read the following terms and condition for deactivation</p>
                    <p>1. If you deactivate the agent, you will have to refund the amount (security and wallet) to the agent.</p>
                    <p>2. If you deactivate the agent, you cannot activate the agent again.</p>
                    <p>3. After deactivating the agent, you will have to enter the refund like on which date and from which reference number etc...</p>



                    <p class="font-weight-600 text-danger">Do you want to deactivate agent ?</p>
                </div>
                <div class="card-footer text-right">
                    <asp:LinkButton ID="lbtnyes" runat="server" OnClick="lbtnyes_Click" CssClass="btn btn-success"> <i class="fa fa-check"></i> Yes </asp:LinkButton>
                    <asp:LinkButton ID="lbtnNo" runat="server" CssClass="btn btn-warning"> <i class="fa fa-times"></i> No </asp:LinkButton>
                </div>

            </div>
            <div style="visibility: hidden; height: 0px;">
                <asp:Button ID="Button3" runat="server" Text="" />
            </div>
        </asp:Panel>
    </div>

    <div class="row">
        <cc1:ModalPopupExtender ID="mpRefund" runat="server" PopupControlID="pnlRefund" CancelControlID="lbtnrefundno"
            TargetControlID="Button4" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlRefund" runat="server" Style="position: fixed; display: none;">
            <div class="card" style="min-width: 350px; max-width: 90vw; max-height: 90vh;">
                <div class="card-header">
                    <div class="card-title m-0">
                        <asp:Label ID="Label10" runat="server" Text="Reufnd of agent deactivation" CssClass="font-weight-600"></asp:Label>
                    </div>
                </div>
                <div class="card-body text-left overflow-auto" style="min-height: 100px;">
                    <p class="font-weight-600">Agent has been deactivated and generated payment order for Refund amount (security amount and wallet balance)</p>

                    <div class="row">
                        <div class="col-lg-4">
                            <span class="text-muted">Payment Order Number </span>
                            <br />
                            <asp:Label ID="lblponumber" runat="server" Text="" Style="font-weight: 600;"></asp:Label>
                        </div>
                        <div class="col-lg-4">
                            <span class="text-muted">Deactivation Date/Time </span>
                            <br />
                            <asp:Label ID="lbldactivatedt" runat="server" Text="" Style="font-weight: 600;"></asp:Label>
                        </div>
                        <div class="col-lg-4">
                            <span class="text-muted">Refund Amount</span>
                            <br />
                            <asp:Label ID="lblrefundamt" runat="server" Text="" Style="font-weight: 600;"></asp:Label>
                        </div>
                    </div>
                    <div class="row mt-2">
                        <div class="col-lg-4">
                            <span class="text-muted">Bank <span class="text-danger">*</span></span>
                            <br />
                            <asp:DropDownList ID="ddlbank" runat="server" CssClass="form-control">
                                <asp:ListItem Value="1" Text="SBI"></asp:ListItem>

                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-4">
                            <span class="text-muted">Bank Ref. No. <span class="text-danger">*</span></span>
                            <br />
                            <asp:TextBox ID="txtbankrefno" placeholder="Max 20 Char." CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-lg-4">
                            <span class="text-muted">Refund Date <span class="text-danger">*</span></span>
                            <br />
                            <asp:TextBox ID="txtrefunddate" placeholder="DD/MM/YYYY" CssClass="form-control" runat="server"></asp:TextBox>
                            <cc1:CalendarExtender ID="CErefunddate" runat="server" CssClass="black"
                                Format="dd/MM/yyyy" PopupButtonID="txtrefunddate" TargetControlID="txtrefunddate"></cc1:CalendarExtender>
                        </div>
                    </div>

                    <p class="font-weight-600 text-danger">Do you want to refund agent amount ?</p>
                </div>
                <div class="card-footer text-right">
                    <asp:LinkButton ID="lbtnrefundyes" runat="server" OnClick="lbtnrefundyes_Click" CssClass="btn btn-success"> <i class="fa fa-check"></i> Yes </asp:LinkButton>
                    <asp:LinkButton ID="lbtnrefundno" runat="server" CssClass="btn btn-warning"> <i class="fa fa-times"></i> No </asp:LinkButton>
                </div>

            </div>
            <div style="visibility: hidden; height: 0px;">
                <asp:Button ID="Button4" runat="server" Text="" />
            </div>
        </asp:Panel>
    </div>


</asp:Content>



