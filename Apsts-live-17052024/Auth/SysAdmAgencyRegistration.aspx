<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/SysAdmmaster.master" AutoEventWireup="true" CodeFile="SysAdmAgencyRegistration.aspx.cs" Inherits="Auth_SysAdmAgencyRegistration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../assets/multiSelect/example-styles.css" rel="stylesheet" />

    <script src="../assets/multiSelect/jquery.multi-select.js"></script>
    <script src="../assets/multiSelect/jquery.multi-select.min.js"></script>
    <%--<script type="text/javascript">
        $(function () {
            $('#ContentPlaceHolder1_ddlItemsProvided').multiSelect();
            $('#ContentPlaceHolder1_ddlServicesProvided').multiSelect();
        });
    </script>--%>

     <script type="text/javascript">
        $(function () {
            $('#<%= ddlItemsProvided.ClientID %>').multiSelect();
        });
          $(function () {
            $('#<%= ddlServicesProvided.ClientID %>').multiSelect();
        });
    </script>
	
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid pt-2 pb-5">
          <asp:HiddenField ID="hidtoken" runat="server" />
        <div class="row align-items-center">
            <div class="col-xl-12">
                <div class="card card-stats mb-3">
                    <div class="row">
                        <div class="col-4 border-right">
                            <div class="card-body">
                                <div class="row m-0">
                                    <div class="col-12">
                                        <h4 class="mb-1">
                                            <asp:Label ID="lblSummary" runat="server" CssClass="text-capitalize" style="font-family:">Summary as on Date 01/12/2021 04:00pm</asp:Label></h4>
                                        <div class="row m-0">
                                            <div class="col-6 border-right">
                                                <div class="row m-0">
                                                    <div class="col-12">
                                                        <h5 class="card-title text-uppercase text-muted mb-0">Total Agency:&nbsp;
                                 <asp:Label ID="lblTotalAgency" runat="server" data-toggle="tooltip" data-placement="bottom" title="Total Agencies" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-6">
                                                <div class="row m-0">
                                                    <div class="col-12">
                                                        <h5 class="card-title text-uppercase text-muted mb-0">Active: &nbsp;
                                <asp:Label ID="lblActivateAgency" runat="server" data-toggle="tooltip" data-placement="bottom" title="Active Agency" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>
                                                    </div>
                                                    <div class="col-12">
                                                        <h5 class="card-title text-uppercase text-muted mb-0">Discontinue:&nbsp;
                                 <asp:Label ID="lblDeactAgency" runat="server" data-toggle="tooltip" data-placement="bottom" title="Deactive Agency" class="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>

                                </div>

                            </div>
                        </div>
                        <div class="col-3 border-right">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col">
                                        <div class="input-group-prepend">
                                            <h4 class="mb-1">Generate Agency Report</h4>
                                      
                                            <asp:LinkButton ID="lbtndownload" data-toggle="tooltip" data-placement="bottom" title="Download Report" runat="server" CssClass="btn btn bg-gradient-green btn-sm text-white ml-2">
                                            <i class="fa fa-download"></i>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <div class="col-5">
                            <div class="card-body pb-0">
                                <div class="row ">
                                    <div class="col">
                                        <div>
                                            <h4 class="mb-1">Instructions</h4>
                                        </div>
                                        <ul class="data-list pb-0" data-autoscroll>
                                            <li>
                                                <asp:Label runat="server" CssClass="form-control-label"> The user can Add a new Agency here</asp:Label><br />
                                            </li>
                                            <li>
                                                <asp:Label runat="server" CssClass="form-control-label"> A list of Agencies defined in the system are shown here.</asp:Label><br />
                                            </li>
                                            <li>
                                                <asp:Label runat="server" CssClass="form-control-label"> The user can Edit an existing record of Agency here.</asp:Label><br />
                                            </li>
                                            <li>
                                                <asp:Label runat="server" CssClass="form-control-label"> An existing record of Agency can be Activate/Deactivated here.</asp:Label>
                                            </li>
                        
                                        </ul>
                                    </div>
                                    <div class="col-auto">
                                        <asp:LinkButton ID="lbtnview" OnClick="lbtnview_Click" data-toggle="tooltip" data-placement="bottom" title="View Instructions" runat="server" Width="30px" CssClass="btn btn bg-gradient-orange btn-sm text-white">
                                    <i class="fa fa-info"></i>
                                        </asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="lbtndwnldinst" OnClick="lbtndwnldinst_Click"  class="btn btn bg-gradient-green btn-sm text-white" Width="30px" data-toggle="tooltip" data-placement="bottom" title="Download Instructions">
                                            <i class="fa fa-download"></i>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-xl-4 order-xl-1">
                <div class="card" style="min-height: 800px">
                    <div class="card-header border-bottom">
                        <div class="float-left">
                            <h3 class="mb-0">Agencies List</h3>
                        </div>
                        <div class="float-right">
                            <div class="input-group">
                                <asp:TextBox ID="tbSearch" data-toggle="tooltip" data-placement="bottom" title="Enter Agency Name" CssClass="form-control form-control-sm mr-1" MaxLength="20" runat="server" Width="200px" placeholder="Search Agency" autocomplete="off"></asp:TextBox>

                                <asp:LinkButton ID="lbtnSearchAgency" OnClick="lbtnSearchAgency_Click" data-toggle="tooltip" data-placement="bottom" title="Search Agency" runat="server" CssClass="btn bg-success btn-sm text-white mr-1">
                                            <i class="fa fa-search"></i>
                                </asp:LinkButton>
                                <asp:LinkButton ID="lbtnResetSearchAgency" OnClick="lbtnResetAgency_Click" data-toggle="tooltip" data-placement="bottom" title="Reset Agency" runat="server" CssClass="btn bg-warning btn-sm text-white">
                                            <i class="fa fa-undo"></i>
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <asp:GridView ID="gvAgencies" runat="server" GridLines="None" ShowHeader="false" AutoGenerateColumns="false" Width="100%"
                        OnRowCommand="gvAgencies_RowCommand" OnRowDataBound="gvAgencies_RowDataBound" OnPageIndexChanging="gvAgencies_PageIndexChanging"
                        AllowPaging="true" PageSize="10" DataKeyNames="agencyid, itemid, agencyserviceprovide, name, address, email, contactno1, contactno2, contactname, contactemail, contactmobile, contactllno, esc1name, esc1email, esc1mobile, esc1landlineno, esc2name, esc2email, esc2mobile, esc2landlineno, esc3name, esc3email, esc3mobile, esc3landlineno, status, deleteyn">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <div class="card card-stats">
                                        <!-- Card body -->
                                        <div class="card-body">
                                            <div class="row">
                                                <div class="col">
                                                    <%--<span class="h3 font-weight-bold mb-0"><%# Eval("Agencyname") %> </span>--%>
                                                    <h5 class="text-uppercase mb-0 font-weight-bold text-xs">
                                                        <%# Eval("name") %><br />
                                                        <span class="text-gray text-xs">Provide Items- 
                                                            <asp:Label runat="server" ID="lblGvAgencyItems"></asp:Label>
                                                        </span>
                                                    </h5>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-lg-8 pt-2">
                                                    <asp:LinkButton ID="lbtnActivate" Visible="true" runat="server" CommandName="activate" CssClass="btn btn-sm btn-success" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' Style="border-radius: 4px;" title="Activate Agency" data-toggle="tooltip" data-placement="bottom"> <i class="fa fa-check"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnDiscontinue" Visible="true" runat="server" CommandName="deactivate" CssClass="btn btn-sm btn-warning" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' Style="border-radius: 4px;" data-toggle="tooltip" data-placement="bottom" title="Deactivate Agency"> <i class="fa fa-ban"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnViewAgency" Visible="true" runat="server" CommandName="viewAgency" CssClass="btn btn-sm btn-default" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' Style="border-radius: 4px;" title="View Agency Details" data-toggle="tooltip" data-placement="bottom"> <i class="fa fa-eye"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnUpdategency" Visible="true" runat="server" CommandName="updateAgency" CssClass="btn btn-sm btn-info" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' Style="border-radius: 4px;" title="Update Agency Details" data-toggle="tooltip" data-placement="bottom"> <i class="fa fa-edit"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnDeletegency" Visible="true" runat="server" CommandName="deleteAgency" CssClass="btn btn-sm btn-danger" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' Style="border-radius: 4px;" title="Delete Agency" data-toggle="tooltip" data-placement="bottom"> <i class="fa fa-trash"></i></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
						<PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                    </asp:GridView>
                    <asp:Panel ID="pnlNoAgency" runat="server">
                        <div class="card card-stats">
                            <!-- Card body -->
                            <div class="card-body">
                                <div class="row">

                                    <div class="col-lg-12 text-center">
                                        <i class="fa fa-bus fa-5x"></i>
                                    </div>
                                    <div class="col-lg-12 text-center mt-4" runat="server" id="divNoRecord">
                                        <span class="h2 font-weight-bold mb-0">Start Agency Creation </span>
                                        <h5 class="card-title text-uppercase text-muted mb-0">No Agency has been created yet</h5>
                                    </div>
                                    <div class="col-lg-12 text-center mt-4" runat="server" id="divNoSearchRecord" visible="false">
                                        <span class="h2 font-weight-bold mb-0">No Record Found </span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
            <div class="col-xl-8 order-xl-2">
                <asp:Panel ID="pnlAddAgency" runat="server" Visible="true">
                    <div class="card" style="min-height: 800px">
                        <div class="card-header">
                            <div class="row align-items-center ml-2">
                                <div class="col-md-8">
                                    <h3 class="mb-0">
                                        <asp:Label runat="server" ID="lblAgencyHead" Text="Add Agency"></asp:Label></h3>
                                </div>
                                <div class="col-md-4 text-right">
                                    <p style="font-size: small; margin: 0px;" class="text-danger font-weight-bold">All marked * Fields are mandatory</p>
                                </div>
                            </div>
                        </div>
                        <div class="card-body">
                            <h6 class="heading-small my-0 ml-3">1. Agency Details</h6>

                            <div class="pl-lg-4 ">
                                <div class="row p-1">
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label"> Agency Name<span style="color: red">*</span></asp:Label>
                                        <asp:TextBox ID="tbAgencyName" MaxLength="100" autocomplete="off" placeholder="Max 100 Characters" data-toggle="tooltip" data-placement="bottom" title="Agency Name" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" FilterType="Numbers, UppercaseLetters, LowercaseLetters,Custom" TargetControlID="tbAgencyName" ValidChars=" _/." />
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label"> Item Provided<span style="color: red">*</span></asp:Label><br />
                                        <asp:ListBox ID="ddlItemsProvided" runat="server" ToolTip="Item Provided" CssClass="form-control form-control-sm w-100" SelectionMode="Multiple" data-toggle="tooltip" data-placement="bottom" title="Item Provided"></asp:ListBox>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label"> Services Provided<span style="color: red">*</span></asp:Label><br />
                                        <asp:ListBox ID="ddlServicesProvided" runat="server" ToolTip="Services Provided" Width="100%" CssClass="form-control form-control-sm" SelectionMode="Multiple" data-toggle="tooltip" data-placement="bottom" title="Item Provided"></asp:ListBox>
                                    </div>
                                </div>
                                <div class="row p-1">
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label">Email ID<span style="color: red">*</span></asp:Label>
                                        <asp:TextBox ID="tbEmailID" MaxLength="50" autocomplete="off" placeholder="Max 50 Characters" data-toggle="tooltip" data-placement="bottom" title="Email ID" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers, UppercaseLetters, LowercaseLetters,Custom" TargetControlID="tbEmailID" ValidChars=" _@." />
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label"> Contact No. 1<span style="color: red">*</span></asp:Label>
                                        <asp:TextBox ID="tbContact1" MaxLength="10" autocomplete="off" placeholder="Max 10 Characters" data-toggle="tooltip" data-placement="bottom" title="Contact No. 1" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers" TargetControlID="tbContact1" ValidChars="-" />
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label"> Contact No. 2</asp:Label>
                                        <asp:TextBox ID="tbContact2" MaxLength="10" autocomplete="off" placeholder="Max 10 Characters" data-toggle="tooltip" data-placement="bottom" title="Contact No. 2" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender20" runat="server" FilterType="Numbers" TargetControlID="tbContact2" ValidChars="-" />
                                    </div>
                                </div>
                                <div class="row p-1">
                                    <div class="col-lg-6">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label">Address<span style="color: red">*</span></asp:Label>
                                        <asp:TextBox ID="tbAddress" MaxLength="200" autocomplete="off" placeholder="Max 200 Characters" data-toggle="tooltip" data-placement="bottom" title="Address" CssClass="form-control form-control-sm" runat="server" TextMode="MultiLine" Height="60px" Style="resize: none;"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender21" runat="server" FilterType="Numbers, UppercaseLetters, LowercaseLetters,Custom" TargetControlID="tbAddress" ValidChars=" _@." />
                                    </div>
                                </div>
                            </div>

                            <h6 class="heading-small  my-0 mt-2 ml-3">2. Contact Person Details</h6>
                            <div class="pl-lg-4">
                                <div class="row p-1">
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label">Name<span style="color: red">*</span></asp:Label>
                                        <asp:TextBox ID="tbCPName" MaxLength="50" data-toggle="tooltip" autocomplete="off" placeholder="Max 50 Characters" data-placement="bottom" title="Contact Person Name" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="UppercaseLetters, LowercaseLetters,Custom" TargetControlID="tbCPName" ValidChars=" ." />
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label"> Email ID<span style="color: red">*</span></asp:Label>
                                        <asp:TextBox ID="tbCPEmailID" MaxLength="50" autocomplete="off" placeholder="Max 50 Characters" data-toggle="tooltip" data-placement="bottom" title="Contact Person Email ID" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers, UppercaseLetters, LowercaseLetters,Custom" TargetControlID="tbCPEmailID" ValidChars=" _@." />
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label"> Mobile No<span style="color: red">*</span></asp:Label>
                                        <asp:TextBox ID="tbCPMobileNo" MaxLength="10" autocomplete="off" placeholder="Max 10 Characters" data-toggle="tooltip" data-placement="bottom" title="Contact Person Mobile No" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender19" runat="server" FilterType="Numbers" TargetControlID="tbCPMobileNo" />
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label"> Landline No</asp:Label>
                                        <asp:TextBox ID="tbCPLLNo" MaxLength="13" autocomplete="off" placeholder="Max 13 Characters" data-toggle="tooltip" data-placement="bottom" title="Contact Person Landline No" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender18" runat="server" FilterType="Numbers" TargetControlID="tbCPLLNo" ValidChars="-" />
                                    </div>
                                </div>
                            </div>

                            <h6 class="heading-small  my-0 mt-3 ml-3">3. Call Reportings</h6>
                            <div class="pl-lg-4 mt-1">
                                <div class="row p-1">
                                    <div class="col-lg-12">
                                        <h6 class="heading-small  my-0">Escalation Level-1</h6>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label">Name</asp:Label>
                                        <asp:TextBox ID="tbE1Name" MaxLength="50" autocomplete="off" placeholder="Max 50 Characters" data-toggle="tooltip" data-placement="bottom" title="Escalation Level-1 Name" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="UppercaseLetters, LowercaseLetters,Custom" TargetControlID="tbE1Name" ValidChars=" ." />
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label"> Email ID</asp:Label>
                                        <asp:TextBox ID="tbE1EmailID" MaxLength="50" autocomplete="off" placeholder="Max 50 Characters" data-toggle="tooltip" data-placement="bottom" title="Escalation Level-1 Email ID" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" FilterType="Numbers, UppercaseLetters, LowercaseLetters,Custom" TargetControlID="tbE1EmailID" ValidChars=" _@." />
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label"> Mobile No</asp:Label>
                                        <asp:TextBox ID="tbE1MobileNo" MaxLength="10" autocomplete="off" placeholder="Max 10 Characters" data-toggle="tooltip" data-placement="bottom" title="Escalation Level-1 Mobile No" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender17" runat="server" FilterType="Numbers" TargetControlID="tbE1MobileNo" />
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label"> Landline No</asp:Label>
                                        <asp:TextBox ID="tbE1LLNo" MaxLength="13" autocomplete="off" placeholder="Max 13 Characters" data-toggle="tooltip" data-placement="bottom" title="Escalation Level-1 Landline No" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" runat="server" FilterType="Numbers" TargetControlID="tbE1LLNo" ValidChars="-" />
                                    </div>
                                </div>
                                <div class="row p-1">
                                    <div class="col-lg-12">
                                        <h6 class="heading-small  my-0">Escalation Level-2</h6>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label">Name</asp:Label>
                                        <asp:TextBox ID="tbE2Name" MaxLength="50" autocomplete="off" placeholder="Max 50 Characters" data-toggle="tooltip" data-placement="bottom" title="Escalation Level-2 Name" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" FilterType="UppercaseLetters, LowercaseLetters,Custom" TargetControlID="tbE2Name" ValidChars=" ." />
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label"> Email ID</asp:Label>
                                        <asp:TextBox ID="tbE2EmailID" MaxLength="50" autocomplete="off" placeholder="Max 50 Characters" data-toggle="tooltip" data-placement="bottom" title="Escalation Level-2 Email ID" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" FilterType="Numbers, UppercaseLetters, LowercaseLetters,Custom" TargetControlID="tbE2EmailID" ValidChars=" _@." />
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label"> Mobile No</asp:Label>
                                        <asp:TextBox ID="tbE2MobileNo" MaxLength="10" autocomplete="off" placeholder="Max 10 Characters" data-toggle="tooltip" data-placement="bottom" title="Escalation Level-2 Mobile No" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" runat="server" FilterType="Numbers" TargetControlID="tbE2MobileNo" />
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label"> Landline No</asp:Label>
                                        <asp:TextBox ID="tbE2LLNo" MaxLength="13" autocomplete="off" placeholder="Max 13 Characters" data-toggle="tooltip" data-placement="bottom" title="Escalation Level-2 Landline No" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server" FilterType="Numbers" TargetControlID="tbE2LLNo" ValidChars="-" />
                                    </div>
                                </div>
                                <div class="row p-1">
                                    <div class="col-lg-12">
                                        <h6 class="heading-small my-0">Escalation Level-3</h6>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label">Name</asp:Label>
                                        <asp:TextBox ID="tbE3Name" MaxLength="50" autocomplete="off" placeholder="Max 50 Characters" data-toggle="tooltip" data-placement="bottom" title="Escalation Level-3 Name" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" FilterType="UppercaseLetters, LowercaseLetters,Custom" TargetControlID="tbE3Name" ValidChars=" ." />
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label"> Email ID</asp:Label>
                                        <asp:TextBox ID="tbE3EmailID" MaxLength="50" autocomplete="off" placeholder="Max 50 Characters" data-toggle="tooltip" data-placement="bottom" title="Escalation Level-3 Email ID" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" FilterType="Numbers, UppercaseLetters, LowercaseLetters,Custom" TargetControlID="tbE3EmailID" ValidChars=" _@." />
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label"> Mobile No</asp:Label>
                                        <asp:TextBox ID="tbE3MobileNo" MaxLength="10" autocomplete="off" placeholder="Max 10 Characters" data-toggle="tooltip" data-placement="bottom" title="Escalation Level-3 Mobile No" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" FilterType="Numbers" TargetControlID="tbE3MobileNo" />
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label"> Landline No</asp:Label>
                                        <asp:TextBox ID="tbE3LLNo" MaxLength="13" autocomplete="off" placeholder="Max 13 Characters" data-toggle="tooltip" data-placement="bottom" title="Escalation Level-3 Landline No" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" FilterType="Numbers" TargetControlID="tbE3LLNo" ValidChars="-" />
                                    </div>
                                </div>
                            </div>
                            <div class="pl-lg-4 mt-3 mb-2">
                                <div class="row">
                                    <div class="col-lg-12 text-left">
                                        <asp:LinkButton ID="lbtnUpdate" runat="server" class="btn btn-success" OnClick="updateAgency_Click" Visible="false" data-toggle="tooltip" data-placement="bottom" title="Update Agency Details">
                                    <i class="fa fa-save"></i>&nbsp; Update</asp:LinkButton>
                                        <asp:LinkButton ID="lbtnSave" Visible="true" runat="server" class="btn btn-success" OnClick="saveAgency_Click" data-toggle="tooltip" data-placement="bottom" title="Save Agency Details">
                                    <i class="fa fa-save"></i>&nbsp; Save</asp:LinkButton>
                                        <asp:LinkButton ID="lbtnReset" runat="server" CssClass="btn btn-danger" OnClick="resetAgency_Click" data-toggle="tooltip" data-placement="bottom" title="Reset Agency Details">
                                    <i class="fa fa-undo"></i>&nbsp; Reset</asp:LinkButton>
                                        <asp:LinkButton ID="lbtnCancel" Visible="false" runat="server" CssClass="btn btn-danger" OnClick="resetAgency_Click" data-toggle="tooltip" data-placement="bottom" title="Cancel Agency Details">
                                    <i class="fa fa-times"></i>&nbsp; Cancel</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlViewAgency" runat="server" Visible="false">
                    <div class="card" style="min-height: 800px">
                        <div class="card-header">
                            <div class="row m-0 align-items-center">
                                <div class="col-lg-12">
                                    <h3 class="mb-0 float-left">
                                        <asp:Label ID="lblAgencyName" Font-Bold="true" runat="server" Text=""></asp:Label></h3>
                                    <asp:LinkButton ID="lbtnViewAgencyCancel" runat="server" class="btn btn-sm btn-danger float-right" Style="border-radius: 25px; left: 31px; margin-top: -22px;" OnClick="resetAgency_Click"> <i class="fa fa-times" ></i> </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="card-body">
                            <h5 class="heading-small  my-0 ml-3">1. Agency Details</h5>
                            <div class="pl-lg-4">
                                <div class="row p-2">
                                    <div class="col-lg-6 ">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label"> Item Provided</asp:Label><br />
                                        <asp:Label runat="server" ID="lblProvidedItems" Text="N/A" CssClass="form-control-label"></asp:Label>
                                    </div>
                                    <div class="col-lg-6">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label"> Services Provided</asp:Label><br />
                                        <asp:Label runat="server" ID="lblProvidedServices" Text="N/A" CssClass="form-control-label" Font-Bold="true"></asp:Label>
                                    </div>
                                </div>
                                <div class="row p-2">
                                    <div class="col-lg-3 ">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label">Email ID</asp:Label><br />
                                        <asp:Label runat="server" ID="lblEmailID" Text="N/A" CssClass="form-control-label" Font-Bold="true"></asp:Label>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label"> Contact No.</asp:Label><br />
                                        <asp:Label runat="server" ID="lblContact1" Text="N/A" CssClass="form-control-label" Font-Bold="true"></asp:Label>
                                        <asp:Label runat="server" ID="lblContact2" Text="N/A" CssClass="form-control-label" Font-Bold="true"></asp:Label>
                                    </div>
                                    <div class="col-lg-6">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label">Address</asp:Label><br />
                                        <asp:Label runat="server" ID="lblAddress" Text="N/A" CssClass="form-control-label" Font-Bold="true"></asp:Label>
                                    </div>
                                </div>
                            </div>

                            <h5 class="heading-small my-0 mt-3 ml-3">2. Contact Person Details</h5>
                            <div class="pl-lg-4">
                                <div class="row p-2">
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label">Name</asp:Label><br />
                                        <asp:Label runat="server" ID="lblCPName" Text="N/A" CssClass="form-control-label" Font-Bold="true"></asp:Label>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label"> Email ID</asp:Label><br />
                                        <asp:Label runat="server" ID="lblCPEmailID" Text="N/A" CssClass="form-control-label" Font-Bold="true"></asp:Label>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label"> Mobile No</asp:Label><br />
                                        <asp:Label runat="server" ID="lblCPMobileNo" Text="N/A" CssClass="form-control-label" Font-Bold="true"></asp:Label>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label"> Landline No</asp:Label><br />
                                        <asp:Label runat="server" ID="lblCPLLNo" Text="N/A" CssClass="form-control-label" Font-Bold="true"></asp:Label>
                                    </div>
                                </div>
                            </div>

                            <h5 class="heading-small  my-0 mt-3 ml-3">3. Call Reportings</h5>
                            <div class="pl-lg-4">
                                <div class="row p-2">
                                    <div class="col-lg-12">
                                        <h6 class="heading-small my-0">Escalation Level-1</h6>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label">Name</asp:Label><br />
                                        <asp:Label runat="server" ID="lblE1Name" Text="N/A" CssClass="form-control-label" Font-Bold="true"></asp:Label>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label"> Email ID</asp:Label><br />
                                        <asp:Label runat="server" ID="lblE1EmailID" Text="N/A" CssClass="form-control-label" Font-Bold="true"></asp:Label>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label"> Mobile No</asp:Label><br />
                                        <asp:Label runat="server" ID="lblE1MobileNo" Text="N/A" CssClass="form-control-label" Font-Bold="true"></asp:Label>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label"> Landline No</asp:Label><br />
                                        <asp:Label runat="server" ID="lblE1LLNo" Text="N/A" CssClass="form-control-label" Font-Bold="true"></asp:Label>
                                    </div>
                                </div>
                                <div class="row p-2">
                                    <div class="col-lg-12">
                                        <h6 class="heading-small  my-0">Escalation Level-2</h6>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label">Name</asp:Label><br />
                                        <asp:Label runat="server" ID="lblE2Name" Text="N/A" CssClass="form-control-label" Font-Bold="true"></asp:Label>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label"> Email ID</asp:Label><br />
                                        <asp:Label runat="server" ID="lblE2EmailID" Text="N/A" CssClass="form-control-label" Font-Bold="true"></asp:Label>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label"> Mobile No</asp:Label><br />
                                        <asp:Label runat="server" ID="lblE2MobileNo" Text="N/A" CssClass="form-control-label" Font-Bold="true"></asp:Label>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label"> Landline No</asp:Label><br />
                                        <asp:Label runat="server" ID="lblE2LLNo" Text="N/A" CssClass="form-control-label" Font-Bold="true"></asp:Label>
                                    </div>
                                </div>
                                <div class="row p-2">
                                    <div class="col-lg-12">
                                        <h6 class="heading-small  my-0">Escalation Level-3</h6>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label">Name</asp:Label><br />
                                        <asp:Label runat="server" ID="lblE3Name" Text="N/A" CssClass="form-control-label" Font-Bold="true"></asp:Label>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label"> Email ID</asp:Label><br />
                                        <asp:Label runat="server" ID="lblE3EmailID" Text="N/A" CssClass="form-control-label" Font-Bold="true"></asp:Label>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label"> Mobile No</asp:Label><br />
                                        <asp:Label runat="server" ID="lblE3MobileNo" Text="N/A" CssClass="form-control-label" Font-Bold="true"></asp:Label>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label"> Landline No</asp:Label><br />
                                        <asp:Label runat="server" ID="lblE3LLNo" Text="N/A" CssClass="form-control-label" Font-Bold="true"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
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
                            <asp:LinkButton ID="lbtnYesConfirmation" runat="server" CssClass="btn btn-success btn-sm" OnClick="lbtnYesConfirmation_Click"> <i class="fa fa-check"></i> Yes </asp:LinkButton>
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

