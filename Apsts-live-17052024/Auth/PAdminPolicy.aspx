<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/PortalAdmmaster.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="PAdminPolicy.aspx.cs" Inherits="Auth_PAdminPolicy" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="header pb-3">
    </div>
    <div class="container-fluid">
        <div class="row">
            <div class="col-lg-3 col-md-3">
                <%-- <div class="row mt-2">
                    <div class="col-lg-12 col-md-12  stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">
                                <div class="card-heading">Agent Advertisement Date</div>
                                <div class="total-tx mb-2">Online Agent Request and Application Process Date</div>
                                <div class="col text-right">
                                    <a href="PAdminAgentAdvertisement.aspx" class="btn btn-sm btn-primary">Explore <i class="fa fa-arrow-circle-o-right"></i></a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>--%>
                <div class="row mt-2">
                    <div class="col-lg-12 col-md-12  stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">
                                <div class="card-heading">Agent Topup Limit</div>
                                <div class="total-tx mb-2">Agent Recharge account using online payment</div>
                                <div class="col text-right">
                                    <asp:LinkButton ID="lbtnAgentTopupLimit" runat="server" OnClick="lbtnAgentTopupLimit_Click" class="btn btn-sm btn-primary">Explore <i class="fa fa-arrow-circle-o-right"></i> </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row mt-2">
                    <div class="col-lg-12 col-md-12 stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">
                                <div class="card-heading">Agent Fee</div>
                                <div class="total-tx mb-2">Online Agent Request Security/Renew Fee</div>
                                <div class="col text-right">
                                    <asp:LinkButton ID="lbtnAgentSecurityFee" OnClick="lbtnAgentSecurityFee_Click" runat="server" class="btn btn-sm btn-primary">Explore <i class="fa fa-arrow-circle-o-right"></i> </asp:LinkButton>
                                </div>

                            </div>
                        </div>

                    </div>
                </div>
                <div class="row mt-2">
                    <div class="col-lg-12 col-md-12 stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">
                                <div class="card-heading">Agent Validity</div>
                                <div class="total-tx mb-2">Online Agent Account and Login Validity</div>
                                <div class="col text-right">
                                    <asp:LinkButton ID="lbtnagentValidity" OnClick="lbtnagentValidity_Click" runat="server" class="btn btn-sm btn-primary">Explore <i class="fa fa-arrow-circle-o-right"></i> </asp:LinkButton>
                                </div>

                            </div>
                        </div>

                    </div>
                </div>


                <div class="row mt-2">
                    <div class="col-lg-12 col-md-12 stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">
                                <div class="card-heading">Agent Commission</div>
                                <div class="total-tx mb-2">Agent online and current booking commission</div>
                                <div class="col text-right">
                                    <asp:LinkButton ID="lbtnagentcommission" runat="server" OnClick="lbtnagentcommission_Click" class="btn btn-sm btn-primary">Explore <i class="fa fa-arrow-circle-o-right"></i> </asp:LinkButton>
                                </div>

                            </div>
                        </div>

                    </div>
                </div>

                <div class="row mt-2">
                    <div class="col-lg-12 col-md-12 stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">
                                <div class="card-heading">Agent Quota</div>
                                <div class="total-tx mb-2">Station wise agent current booking quota configuration</div>
                                <div class="col text-right">
                                    <asp:LinkButton ID="lbtnagentquota" runat="server" OnClick="lbtnagentquota_Click" class="btn btn-sm btn-primary">Explore <i class="fa fa-arrow-circle-o-right"></i> </asp:LinkButton>
                                </div>

                            </div>
                        </div>

                    </div>
                </div>
                <div class="row mt-2">
                    <div class="col-lg-12 col-md-12 stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">
                                <div class="card-heading">Agent Login Configuration</div>
                                <div class="total-tx mb-2">Agent Login Enable and Disable</div>
                                <div class="col text-right">
                                    <asp:LinkButton ID="lbtnAgentloginconf" runat="server" OnClick="lbtnAgentloginconf_Click" class="btn btn-sm btn-primary">Explore <i class="fa fa-arrow-circle-o-right"></i> </asp:LinkButton>
                                </div>

                            </div>
                        </div>

                    </div>
                </div>
            </div>
            <div class="col-lg-9 col-md-9">

                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-lg-8">
                                <h4 class="text-left pt-2 pb-1" style="font-weight: bold; font-size: 15pt; color: #7b7474;">
                                    <asp:Label ID="lblmhd" runat="server" Text="Agent Topup limit"></asp:Label></h4>
                            </div>
                            <div class="col-lg-4 text-right">
                                <asp:LinkButton ID="LinkButtonInfo" runat="server" class="btn" Style="margin-top: 0px; font-size: 20px; margin-bottom: 4px; color: #dc3545; line-height: 19px;">
                                        help <i class="fa fa-info-circle" ></i> </asp:LinkButton>

                            </div>
                        </div>
                        <hr />


                        <asp:Panel ID="pnlAgentTopUpLimit" runat="server" Visible="true">


                            <div class="row mt-2 ml-2 ">
                                <div class="col-lg-6">



                                    <div class="row m-0 py-2">
                                        <div class="col-lg-5 text-right">
                                            <asp:Label runat="server" AssociatedControlID="ddlagenttype1" ID="Label1" CssClass="form-control-label" Font-Bold="true">Agent Type<span class="text-warning">*</span></asp:Label>
                                        </div>
                                        <div class="col-lg-5">
                                            <asp:DropDownList ID="ddlagenttype1" runat="server" ToolTip=" select Agent Type"
                                                Style="font-size: 10pt;" CssClass="form-control form-control-sm">
                                            </asp:DropDownList>
                                        </div>

                                    </div>
                                    <div class="row m-0">
                                        <div class="col-lg-5 text-right ">
                                            <asp:Label runat="server" AssociatedControlID="" ID="lblMaxAmount" CssClass="form-control-label" Font-Bold="true">Max Amount<span class="text-warning">*</span> </asp:Label>
                                        </div>
                                        <div class="col-lg-5">
                                            <asp:TextBox ID="tbMaxAmount" CssClass="form-control form-control-sm" placeholder="Minimum ₹1000" ToolTip=" Enter Maximum Amount" runat="server" MaxLength="5" autocomplete="off"
                                                Text=""></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="ajaxFtMaxAmount" runat="server" FilterType="Numbers, Custom" ValidChars="."
                                                TargetControlID="tbMaxAmount" />
                                        </div>
                                    </div>
                                    <div class="row m-0 mt-2">
                                        <div class="col-lg-5 text-right">
                                            <asp:Label runat="server" AssociatedControlID="tbAlertAmount" ID="lblAlertAmount" Width="60%" CssClass="form-control-label" Font-Bold="true">Alert Amount<span class="text-warning">*</span> </asp:Label>
                                        </div>
                                        <div class="col-lg-5">
                                            <asp:TextBox ID="tbAlertAmount" CssClass="form-control form-control-sm" placeholder="Minimum ₹1000" ToolTip=" Enter Alert Amount" runat="server" MaxLength="5" autocomplete="off"
                                                Text=""></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="ajaxFtAlertAmount" runat="server" FilterType="Numbers, Custom" ValidChars="."
                                                TargetControlID="tbAlertAmount" />
                                        </div>
                                    </div>
                                    <div class="row m-0 mt-4">
                                        <div class="col-lg-12 text-center">
                                            <asp:LinkButton ID="lbtnSaveAmount" OnClick="lbtnSaveAmount_Click" runat="server" class="btn btn-success"
                                                ToolTip="Click here to Save Amount"> <i class="fa fa-save" ></i> Save</asp:LinkButton>
                                            <asp:LinkButton ID="lbtnResetAmount" OnClick="lbtnResetAmount_Click" runat="server" class="btn btn-danger"
                                                ToolTip="Click here to reset Amount"> <i class="fa fa-undo" ></i> Reset</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-6 col-md-6 pl-3" style="border-left: 1px solid;">

                                    <div class="row m-0 mt-2">
                                        <div class="col-lg-12 col-md-12">

                                            <asp:GridView ID="gvAgentTopuplimit" runat="server" AutoGenerateColumns="False" GridLines="None"
                                                Visible="true" CssClass="table noborder" DataKeyNames="" ShowHeader="false">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>


                                                            <span style="font-weight: bold;">Max Limit.</span>
                                                            <asp:Label ID="lblMaxlimit" runat="server" Text='<%# Eval("agenttopup_limit") %>'
                                                                Style="color: Red;"></asp:Label>
                                                            <i class="fa fa-rupee-sign"></i>
                                                            <br />
                                                            <br />

                                                            <span style="font-weight: bold;">Amount to be alert</span>
                                                            <asp:Label ID="lblamtalert" runat="server" Text='<%# Eval("alert_amount") %>' Style="color: Red;"></asp:Label>
                                                            <i class="fa fa-rupee-sign"></i>
                                                            <br />
                                                            <br />


                                                            <span style="font-weight: bold;">Updation Date/Time</span>
                                                            <asp:Label ID="lblupdataiondatetime" runat="server" Text='<%# Eval("updatedon_") %>'
                                                                Style="color: Red;"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>

                                            <asp:Panel ID="pnlgvCurrentAgentTopUpNoRecord" runat="server" Width="100%" Visible="true">
                                                <div class="col-md-12 p-0" style="text-align: center; text-transform: uppercase;">
                                                    <div class="col-md-12 busListBox" style="color: #e3e3e3; padding-top: 50px; padding-bottom: 50px; font-size: 20px; font-weight: bold;">
                                                        Sorry No Record Found
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </div>
                            </div>



                        </asp:Panel>
                        <asp:Panel ID="pnlAgentSecurityFee" runat="server" Visible="false">


                            <div class="row mt-2 ml-2 m-0">
                                <div class="col-lg-6">


                                    <div class="row m-0">
                                        <div class="col-lg-4 text-right pr-0">
                                            <asp:Label runat="server" AssociatedControlID="ddlagenttype2" ID="lblAgentType" CssClass="form-control-label" Font-Bold="true">Agent Type<span class="text-warning">*</span></asp:Label>
                                        </div>
                                        <div class="col-lg-5 ml-2">
                                            <asp:DropDownList ID="ddlagenttype2" runat="server" ToolTip=" select Agent Type"
                                                Style="font-size: 10pt;" CssClass="form-control form-control-sm">
                                            </asp:DropDownList>
                                        </div>

                                    </div>
                                    <div class="row m-0 mt-2">
                                        <div class="col-lg-4 text-right pr-0">
                                            <asp:Label runat="server" AssociatedControlID="tbSecutityAmount" ID="lblSecutityAmount" CssClass="form-control-label" Font-Bold="true">Security Fee<span class="text-warning">*</span></asp:Label>
                                        </div>
                                        <div class="col-lg-5 ml-2 mt-2">
                                            <asp:TextBox ID="tbSecutityAmount" CssClass="form-control form-control-sm" placeholder="Minimum ₹ 1000" ToolTip="Enter Security Fee" runat="server" MaxLength="5" autocomplete="off"
                                                Text=""></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="ajaxFtAgentSecurity" runat="server" FilterType="Numbers, Custom" ValidChars="."
                                                TargetControlID="tbSecutityAmount" />

                                        </div>

                                    </div>

                                    <div class="row m-0 mt-2">
                                        <div class="col-lg-4 text-right pr-0">
                                            <asp:Label runat="server" AssociatedControlID="tbRenewalFee" ID="Label2" CssClass="form-control-label" Font-Bold="true">Renewal Fee<span class="text-warning">*</span></asp:Label>
                                        </div>
                                        <div class="col-lg-5 ml-2 mt-2">
                                            <asp:TextBox ID="tbRenewalFee" CssClass="form-control form-control-sm" placeholder="Minimum ₹ 1000" ToolTip="Enter Renewal Fee" runat="server" MaxLength="5" autocomplete="off"
                                                Text=""></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers, Custom" ValidChars="."
                                                TargetControlID="tbRenewalFee" />

                                        </div>

                                    </div>
                                    <div class="row m-0 mt-4">
                                        <div class="col-lg-12 text-center">
                                            <asp:LinkButton ID="lbtnSaveAgentSecurityFee" OnClick="lbtnSaveAgentSecurityFee_Click" runat="server" class="btn btn-success"
                                                ToolTip="Click here to save Agent Security Amount"> <i class="fa fa-save" ></i> Save</asp:LinkButton>
                                            <asp:LinkButton ID="lbtnResetAgentSecurityFee" OnClick="lbtnResetAgentSecurityFee_Click" runat="server" class="btn btn-danger"
                                                ToolTip="Click here to reset Agent Security Amount"> <i class="fa fa-undo" ></i> Reset</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-6 pl-3" style="border-left: 1px solid;">
                                    <div class="row m-0 mt-2">
                                        <div class="col-lg-12  pr-0">
                                            <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true"><h3>Current Value</h3></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row m-0 mt-2">
                                        <div class="col-lg-12 col-md-12">

                                            <asp:GridView ID="gvAgentSecurityFee" runat="server" AutoGenerateColumns="False" GridLines="None"
                                                Visible="true" CssClass="table noborder" DataKeyNames="" ShowHeader="false">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <span style="font-weight: bold;">Security Fee</span>
                                                            <asp:Label ID="lblMaxlimit" runat="server" Text='<%# Eval("securityamount_") %>' Style="color: Red;"></asp:Label>
                                                            <br />
                                                            <br />
                                                            <span style="font-weight: bold;">Renew Fee</span>
                                                            <asp:Label ID="lblamtalert" runat="server" Text='<%# Eval("renew_charge_") %>' Style="color: Red;"></asp:Label>
                                                            <i class="fa fa-rupee"></i>
                                                            <br />
                                                            <br />
                                                            <span style="font-weight: bold;">Updation Date/Time</span>
                                                            <asp:Label ID="lblupdataiondatetime" runat="server" Text='<%# Eval("updatedon_") %>'
                                                                Style="color: Red;"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>

                                            <asp:Panel ID="pnlAgentCurrentSecurityAmountNoRecord" runat="server" Width="100%" Visible="true">
                                                <div class="col-md-12 p-0" style="text-align: center; text-transform: uppercase;">
                                                    <div class="col-md-12 busListBox" style="color: #e3e3e3; padding-top: 50px; padding-bottom: 50px; font-size: 20px; font-weight: bold;">
                                                        Sorry No Record Found
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </div>

                            </div>

                        </asp:Panel>
                        <asp:Panel ID="pnlAgentValidity" runat="server" Visible="false">
                            <div class="row">
                                <div class="col-lg-6" style="border-right: 1px solid;">
                                    <div class="row">
                                        <div class="col-lg-6 text-right pr-0"><span>Agent type</span><span style="color: red;"> *</span></div>
                                        <div class="col-lg-6">
                                            <asp:DropDownList ID="ddlagenttype3" runat="server" Style="width: 75%;"
                                                class="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row mt-2">
                                        <div class="col-lg-6 text-right pr-0">Account Validity <span class="text-warning">(In Months)*</span></div>
                                        <div class="col-lg-6">
                                            <asp:TextBox class="form-control" runat="server" ID="tbaccountValidity" MaxLength="5" autocomplete="off" placeholder="In Month"
                                                Text="" Style="font-size: 10pt; height: 36px; width: 50%;"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers"
                                                TargetControlID="tbaccountValidity" />
                                        </div>
                                    </div>
                                    <div class="row mt-2">
                                        <div class="col-lg-6 text-right pr-0">Login Validity After Expire <span class="text-warning">(In Months)*</span></div>
                                        <div class="col-lg-6">
                                            <asp:TextBox class="form-control" runat="server" ID="tbafterexpire" MaxLength="5" autocomplete="off" placeholder="In Month"
                                                Text="" Style="font-size: 10pt; height: 36px; width: 50%;"></asp:TextBox>


                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers"
                                                TargetControlID="tbafterexpire" />
                                        </div>
                                    </div>
                                    <div class="row mt-2">
                                        <div class="col-lg-6 text-right pr-0">Renew Before <span class="text-warning">(In Days)*</span></div>
                                        <div class="col-lg-6">
                                            <asp:TextBox class="form-control" runat="server" ID="tbrenewdays" MaxLength="5" autocomplete="off" placeholder="Min 10 Days"
                                                Text="" Style="font-size: 10pt; height: 36px; width: 50%;"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers"
                                                TargetControlID="tbrenewdays" />
                                        </div>
                                    </div>
                                    <div class="row mt-2">
                                        <div class="col-lg-12 text-center">
                                            <asp:LinkButton ID="lbtnAgentValiditySave" runat="server" OnClick="lbtnAgentValiditySave_Click" OnClientClick="return ShowLoading()" class="btn btn-success" Style="margin-top: 9px; font-size: 10pt;"
                                                ToolTip="Click here to Save agent Validity"> <i class="fa fa-floppy-o" ></i> Save</asp:LinkButton>
                                            <asp:LinkButton ID="lbtnAgentValidityReset" runat="server" OnClientClick="return ShowLoading()" class="btn btn-danger" Style="margin-top: 9px; font-size: 10pt;"
                                                ToolTip="Click here to Reset agent Validity"> <i class="fa fa-undo" ></i> Reset</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <asp:GridView ID="grdAgentValidity" runat="server" AutoGenerateColumns="False" GridLines="None"
                                        Visible="true" CssClass="table noborder" DataKeyNames="" ShowHeader="false">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <span style="font-weight: bold;">Account Validity</span><span class="text-warning"> (In Month)</span>
                                                    <asp:Label ID="lblmaxlimit" runat="server" Text='<%# Eval("account_validity_inmonth_") %>' Style="color: Red;"></asp:Label>
                                                    <br />
                                                    <br />
                                                    <span style="font-weight: bold;">Login Validity</span><span class="text-warning"> (In Month)</span>
                                                    <asp:Label ID="lblamtalert" runat="server" Text='<%# Eval("login_validity_inmonth_") %>' Style="color: Red;"></asp:Label>
                                                    <br />
                                                    <br />
                                                    <span style="font-weight: bold;">Renew Before Validity</span><span class="text-warning"> (In Days)</span>
                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("renew_before_indays_") %>' Style="color: Red;"></asp:Label>
                                                    <br />
                                                    <br />
                                                    <span style="font-weight: bold;">Updation Date/Time</span>
                                                    <asp:Label ID="lblupdataiondatetime" runat="server" Text='<%# Eval("updatedon_") %>'
                                                        Style="color: Red;"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>

                        </asp:Panel>
                        <asp:Panel ID="pnlAgentCommission" runat="server" Visible="false">
                            <div class="row m-0">
                                <div class="col-sm-12">
                                    <asp:GridView ID="gvAgentCommission" runat="server" AutoGenerateColumns="false" GridLines="None"
                                        AllowPaging="true" PageSize="20" CssClass="table table-striped table-hover" DataKeyNames="srtp_id_" HeaderStyle-CssClass="thead-light font-weight-bold">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Service Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsrtp" runat="server" Visible="false" Text='<%# Eval("srtp_id_") %>'></asp:Label>
                                                    <asp:Label runat="server" Visible="true" Text='<%# Eval("service_type_name_") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Online Booking Commission">
                                                <ItemTemplate>
                                                    <asp:TextBox class="form-control form-control-sm text-right" runat="server" ID="tbOnlineBooking" MaxLength="50" autocomplete="off"
                                                        placeholder="" Text='<%# Eval("online_commission_") %>' Style="font-size: 10pt; width: 100px;" ToolTip="Enter Online Booking Agent Commission"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender32" runat="server" FilterType="Numbers,Custom" ValidChars="."
                                                        TargetControlID="tbOnlineBooking" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Current Booking Commission">
                                                <ItemTemplate>
                                                    <asp:TextBox class="form-control form-control-sm text-right" runat="server" ID="tbCurrentBooking" MaxLength="50" autocomplete="off"
                                                        placeholder="" Text='<%# Eval("current_commission_") %>' Style="font-size: 10pt; width: 100px;" ToolTip="Enter Current Booking Agent Commission"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender33" runat="server" FilterType="Numbers,Custom" ValidChars="."
                                                        TargetControlID="tbCurrentBooking" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Right" CssClass="GridPager" ForeColor="Black" />
                                    </asp:GridView>
                                </div>
                            </div>
                            <div class="row m-0 mt-4 mb-4">
                                <div class="col-lg-12 text-center">
                                    <asp:LinkButton ID="lbtnSaveAgentCommission" runat="server" OnClick="lbtnSaveAgentCommission_Click" OnClientClick="return ShowLoading()" class="btn btn-success"
                                        ToolTip="Click here to save Agent Commission Amount"> <i class="fa fa-save" ></i> Update</asp:LinkButton>
                                    <asp:LinkButton ID="lbtnResetAgentCOmmission" runat="server" OnClientClick="return ShowLoading()" class="btn btn-danger"
                                        ToolTip="Click here to reset Agent Commission Amount"> <i class="fa fa-undo" ></i> Reset</asp:LinkButton>
                                </div>
                            </div>

                        </asp:Panel>

                        <asp:Panel ID="pnlAgentQuota" runat="server" Visible="false">
                            <div class="row m-0">
                                <div class="col-lg-6" style="border-right: 1px solid #e6e6e6;">
                                    <div class="row">
                                        <div class="col-lg-4">
                                            <span style="font-size: 10pt;">Station list
                                                <br />
                                                for configured current booking quota</span>
                                        </div>
                                        <div class="col-lg-4">
                                            Select State 
                                            <asp:DropDownList ID="ddlstate" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlstate_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-lg-4">
                                            Select Distrcit 
                                            <asp:DropDownList ID="ddldistrict" runat="server" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged" CssClass="form-control" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <hr />
                                    <asp:GridView ID="grdstation" runat="server" AutoGenerateColumns="False" OnRowCommand="grdstation_RowCommand"
                                        GridLines="None" AllowSorting="true" AllowPaging="true" PageSize="20" class="table"
                                        DataKeyNames="stationcode_,stationname_,statecode_,districtcode_">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Name">
                                                <ItemTemplate>
                                                    <b><%# Eval("stationname_") %></b>
                                                    <br />
                                                    <%# Eval("districtname_") %>, <%# Eval("statename_") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="No of Quota">
                                                <ItemTemplate>
                                                    <asp:TextBox class="form-control form-control-sm text-right" runat="server" ID="tbQouta" MaxLength="2" autocomplete="off"
                                                        placeholder="" Text="0" Style="font-size: 10pt; width: 100px;" ToolTip="Enter Agent Quota"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender32" runat="server" FilterType="Numbers"
                                                        TargetControlID="tbQouta" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnaddconfig" runat="server" CommandName="ADDCONFIG"
                                                        CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' class="btn btn-info btn-sm">
                                            <i class="fa fa-forward"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:Panel ID="pnlnostation" runat="server" Width="100%" Visible="true">
                                        <div class="col-md-12 p-0" style="text-align: center; text-transform: uppercase;">
                                            <div class="col-md-12 busListBox" style="color: #e3e3e3; padding-top: 50px; padding-bottom: 50px; font-size: 33px; font-weight: bold;">
                                                Station List not available<br />
                                                Please add Stations
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                                <div class="col-lg-6">
                                    <div class="row">
                                        <div class="col-lg-4">
                                            <span style="font-size: 10pt;">Station list
                                                <br />
                                                current booking configured quota </span>
                                        </div>
                                        <div class="col-lg-4">
                                            Select State 
                                            <asp:DropDownList ID="ddlconfigstate" runat="server" OnSelectedIndexChanged="ddlconfigstate_SelectedIndexChanged" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-lg-4">
                                            Select Distrcit 
                                            <asp:DropDownList ID="ddlconfigdistrict" runat="server" OnSelectedIndexChanged="ddlconfigdistrict_SelectedIndexChanged" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <hr />
                                    <asp:GridView ID="grdconfigstation" runat="server" AutoGenerateColumns="False" OnRowCommand="grdconfigstation_RowCommand" OnRowDataBound="grdconfigstation_RowDataBound"
                                        GridLines="None" AllowSorting="true" AllowPaging="true" PageSize="20" class="table"
                                        DataKeyNames="stationcode_,stationname_,statecode_,districtcode_,no_of_quota_,in_process_quota_,assigned_quota_,remove_yn,update_yn">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Name">
                                                <ItemTemplate>
                                                    <b><%# Eval("stationname_") %></b>
                                                    <br />
                                                    <%# Eval("districtname_") %>, <%# Eval("statename_") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="No. of Quota">
                                                <ItemTemplate>
                                                    <asp:TextBox class="form-control form-control-sm text-right" runat="server" ID="tbQouta" MaxLength="2" autocomplete="off"
                                                        placeholder="" Text='<%# Eval("no_of_quota_") %>' Style="font-size: 10pt; width: 100px;" ToolTip="Enter Agent Quota"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender32" runat="server" FilterType="Numbers"
                                                        TargetControlID="tbQouta" />

                                                    <%--<asp:Label id="lblquota" runat ="server" Text='<%# Eval("no_of_quota_") %>' ></asp:Label>--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="In Process Quota">
                                                <ItemTemplate>
                                                    <%# Eval("in_process_quota_") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Processed Quota">
                                                <ItemTemplate>
                                                    <%# Eval("assigned_quota_") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnremoveconfig" runat="server" CommandName="REMOVECONFIG" ToolTip="Remove Quota Configuration"
                                                        CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' class="btn btn-info btn-sm">
                                            <i class="fa fa-backward"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnupdatequota" runat="server" CommandName="UPDATEQUOTA" ToolTip="Update Quota"
                                                        CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' class="btn btn-warning btn-sm">
                                            <i class="fa fa-edit"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:Panel ID="pnlconfigstation" runat="server" Width="100%" Visible="true">
                                        <div class="col-md-12 p-0" style="text-align: center; text-transform: uppercase;">
                                            <div class="col-md-12 busListBox" style="color: #e3e3e3; padding-top: 50px; padding-bottom: 50px; font-size: 33px; font-weight: bold;">
                                                Configured Station List not available<br />
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                            <div class="row m-0 mt-4 mb-4">
                            </div>

                        </asp:Panel>
                        <asp:Panel ID="pnlAgentLoginconfig" runat="server" Visible="false">
                            <div class="row m-0">
                                <div class="col-sm-12">
                                    <asp:GridView ID="grdagentlist" runat="server" AutoGenerateColumns="false" GridLines="None"
                                        AllowPaging="true" PageSize="10" CssClass="table table-striped table-hover" OnRowDataBound="grdagentlist_RowDataBound"
                                        DataKeyNames="agcode,agname,agcurrentstatus" HeaderStyle-CssClass="thead-light font-weight-bold" OnPageIndexChanging="grdagentlist_PageIndexChanging"
                                        OnRowCommand="grdagentlist_RowCommand">
                                        <Columns>

                                            <asp:TemplateField HeaderText="#">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Name">
                                                <ItemTemplate>
                                                    <%# Eval("agname") %> (<%# Eval("agcode") %>)
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Mobile/Email">
                                                <ItemTemplate>
                                                    <i class="fa fa-mobile"></i><%# Eval("agmobile") %>
                                                    <br />
                                                    <i class="fa fa-envelope"></i><%# Eval("agemail") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Address">
                                                <ItemTemplate>
                                                    <%# Eval("agaddress") %>, <%# Eval("agdistrict") %><br />
                                                    <%# Eval("agstate") %>,
                                                    <b>PINCODE</b> <%# Eval("agpincode") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Login Status">
                                                <ItemTemplate>
                                                    <%# Eval("agloginstatus") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Current Balance">
                                                <ItemTemplate>
                                                    <%# Eval("agcurrentbalance") %> <i class="fa fa-rupee"></i>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnEnableLogin" runat="server" CommandName="ENABLELOGIN" ToolTip="Click here to Enable Login"
                                                        CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' class="btn btn-success btn-sm">
                                            <i class="fa fa-check"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnDisableLogin" runat="server" CommandName="DISABLELOGIN" ToolTip="Click here to Disable Login"
                                                        CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' class="btn btn-danger btn-sm">
                                            <i class="fa fa-times"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Right" CssClass="GridPager" ForeColor="Black" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
            <div class="row">
                <cc1:ModalPopupExtender ID="mpInfo" runat="server" PopupControlID="PanelInfo" CancelControlID="LinkButtonMpInfoClose"
                    TargetControlID="LinkButtonInfo" BackgroundCssClass="modalBackground">
                </cc1:ModalPopupExtender>
                <asp:Panel ID="PanelInfo" runat="server" Style="position: fixed;">
                    <div class="card" style="width: 350px;">
                        <div class="card-header">
                            <h4 class="card-title text-left mb-0">About Module
                            </h4>
                        </div>
                        <div class="card-body text-left p-2" style="min-height: 100px;">
                            <ol class="p-3 pt-0" style="font-size: 10pt;">
                                <li>1. Set Agent Top Up Limit</li>
                                <li>2. Set Agent Security and Renew Fee</li>
                                <li>3. Set Agent Login and Account Validity</li>
                                <li>4. Set Agent Service Type Online and Current Booking Commission</li>
                            </ol>

                            <div style="width: 100%; margin-top: 20px; text-align: right;">
                                <asp:LinkButton ID="LinkButtonMpInfoClose" runat="server" CssClass="btn btn-success btn-sm ml-2"> <i class="fa fa-check"></i> OK </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>

            <div class="row">
                <cc1:ModalPopupExtender ID="mpConfirmation" runat="server" PopupControlID="pnlConfirmation"
                    CancelControlID="lbtnNoConfirmation" TargetControlID="Button4" BackgroundCssClass="modalBackground">
                </cc1:ModalPopupExtender>
                <asp:Panel ID="pnlConfirmation" runat="server" Style="position: fixed;">
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
</asp:Content>

