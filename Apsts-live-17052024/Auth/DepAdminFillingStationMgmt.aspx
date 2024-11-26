<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/Depotmaster.master" AutoEventWireup="true" CodeFile="DepAdminFillingStationMgmt.aspx.cs" Inherits="Auth_DepAdminFillingStationMgmt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="header pb-7">
    </div>
    <div class="container-fluid mt--6">
        <asp:HiddenField ID="hidtoken" runat="server" />
        <div class="row align-items-center">
            <div class="col-xl-12">
                <div class="card card-stats mb-3">
                    <div class="row m-0">
                        <div class="col-4 border-right">
                            <div class="card-body">
                                <div class="row">
                                    <h4 class="mb-1">
                                        <asp:Label ID="lblSummary" runat="server" CssClass="text-capitalize">Summary as on Date 01/12/2021 04:00pm</asp:Label></h4>
                                    <div class="col-md-6">
                                        <asp:Label runat="server" CssClass="form-control-label">Total Filling Stations: </asp:Label>

                                    </div>
                                    <div class="col-md-3 text-right">
                                        <asp:Label ID="lblFillingStationListCount" runat="server" CssClass="form-control-label" Font-Bold="true" Style="color: red;"><span style="color: red"></span></asp:Label>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <div class="col-3 border-right">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col">
                                        <div>
                                            <h4 class="mb-1">Generate Filling Station Report</h4>
                                        </div>
                                        <div class="input-group mb-3">
                                            <div class="input-group-prepend pr-2" style="width: 80%">
                                                <asp:DropDownList ID="ddlFillingReport" data-toggle="tooltip" data-placement="bottom" title="Filling Station" CssClass="form-control form-control-sm" runat="server">
                                                    <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <asp:LinkButton ID="lbtnDownloadFillingStRpt" data-toggle="tooltip" data-placement="bottom" title="Download" runat="server" CssClass="btn btn bg-gradient-green btn-sm text-white">
                                            <i class="fa fa-download"></i>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>

                        <div class="col-5">
                            <div class="card-body">
                                <div class="row mr-0">
                                    <div class="col">
                                        <div>
                                            <h4 class="mb-1">Instructions</h4>
                                        </div>
                                        <ul class="data-list" data-autoscroll>
                                            <li>
                                                <asp:Label runat="server" CssClass="form-control-label">• Add depot wise filling station</asp:Label><br />
                                            </li>
                                            <li>
                                                <asp:Label runat="server" CssClass="form-control-label">• Depot mananger can create filling station of its depot only.</asp:Label><br />
                                            </li>
                                            <li>
                                                <asp:Label runat="server" CssClass="form-control-label">• If filling station is used, it cannot be deleted.</asp:Label><br />
                                            </li>
                                            <li>
                                                <asp:Label runat="server" CssClass="form-control-label">• Except filling station name, other details can be updated.</asp:Label>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-auto">
                                        <asp:LinkButton ID="lbtnview" OnClick="lbtnview_Click" ToolTip="View Instructions" runat="server" CssClass="btn btn bg-gradient-orange btn-sm text-white">
                                    <i class="fa fa-eye"></i>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lbtnDownload" runat="server" class="btn btn bg-gradient-green btn-sm text-white" ToolTip="Download Instruction">
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
            <div class="col-xl-6 order-xl-1">
                <div class="card" style="min-height: 455px">
                    <div class="card-header border-bottom">
                        <div class="row m-0">
                            <div class="col-lg-8">
                                <asp:Label runat="server" Visible="true" Font-Bold="true" CssClass="form-control-label"><h3>Filling Stations List</h3></asp:Label>
                            </div>
                            <div class="col-lg-4 text-right">
                            </div>
                        </div>
                    </div>

                    <div class="card-body">
                        <div class="row m-0 align-items-center">
                            <div class="col-md-12">
                                <asp:GridView ID="gvFillingStation" runat="server" AutoGenerateColumns="False" PageSize="6" GridLines="None" AllowPaging="true"
                                    CssClass="table table-striped table-hover" OnRowCommand="gvFillingStation_RowCommand" PagerStyle-CssClass="GridPager" Font-Size="10pt" Width="100%" OnPageIndexChanging="gvFillingStation_PageIndexChanging"
                                    DataKeyNames="fillingstn_id,officeid,fillingstn_name,workshoppermises_yn,workshop_office_id,contactno1,contactno2,statecode,
                                    			  districtcode,adrs,longi_tude,lati_tude,officename,state_name,district_name">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No." ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderText="Station Name" ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblfillingstn_name" runat="server" Text='<%#Eval("fillingstn_name")%>'></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderText="Contact No." ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblContact" runat="server" Text='<%#Eval("contactno1")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderText="Address" ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvAddress" runat="server" Text='<%#Eval("adrs")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderText="Actions">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnEdit" runat="server" ToolTip="Click here for edit" CssClass="btn btn-sm btn-warning" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="gvEdit"><i class="fa fa-edit"></i></asp:LinkButton>
                                                <asp:LinkButton ID="lbtndelete" Visible="false" runat="server" ToolTip="Click here for delete" CssClass="btn btn-sm btn-danger" CommandName="gvDelete"><i class="fa fa-trash"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle BackColor="#ececec" Font-Size="9pt" />
									<PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                                </asp:GridView>

                                <asp:Panel runat="server" ID="pnlNoRecord" Visible="false" CssClass="text-center" Width="100%">
                                    <p class="text-center" style="font-size: 32px; font-weight: bold; color: #e3e3e3; margin-top: 50px;">
                                        No Record Available
                                    </p>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-xl-6 order-xl-2">
                <asp:Panel runat="server" ID="pnlAddFillingStation" Visible="true">
                    <div class="card" style="min-height: 430px">
                        <div class="card-header border-bottom">
                            <div class="row m-0">
                                <div class="col-md-7 text-left">
                                    <asp:Label ID="lblAddNewFS" runat="server" Visible="true" Font-Bold="true" CssClass="form-control-label"><h3>Add New Filling Station</h3></asp:Label>
                                    <asp:Label ID="lblUpdateFS" runat="server" Visible="false" Font-Bold="true" CssClass="form-control-label"><h3>Update Filling Station</h3></asp:Label>
                                </div>
                                <div class="col-md-5 text-right">
                                    <asp:Label runat="server" CssClass="form-control-label" Style="color: red;">All Marked * Fields are mandatory </asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="card-body">
                            <div class="row m-0 align-items-center">
                                <div class="col-lg-12">
                                    <div class="row m-0">
                                        <div class="col-lg-6">
                                            <asp:Label runat="server" Style="margin-top: 6px" Font-Bold="true" CssClass="form-control-label">Depot</asp:Label>
                                            &nbsp; &nbsp;
                                                <asp:DropDownList ID="ddlDepotfilling" ToolTip="Select Depot" Enabled="false" CssClass="form-control form-control-sm" runat="server"></asp:DropDownList>
                                        </div>
                                        <div class="col-lg-6">
                                            <asp:Label runat="server" CssClass="form-control-label"> Name<span style="color: red">*</span></asp:Label>
                                            <asp:TextBox class="form-control form-control-sm" runat="server" ID="tbName" MaxLength="50" ToolTip="Enter Filling Station Name" autocomplete="off"
                                                placeholder="Max 50 char" Text="" Style=""></asp:TextBox>
                                            <asp:HiddenField ID="hdnFillingSTNID" runat="server" />
                                        </div>


                                    </div>

                                </div>
                            </div>
                            <div class="row m-0 mt-2 ml-1 align-items-center">
                                <div class="col-lg-6">
                                    <asp:Label runat="server" CssClass="form-control-label">In the Premises if Workshop<span style="color: red">*</span></asp:Label>
                                    <asp:DropDownList ID="ddlWorkshop" AutoPostBack="true" OnSelectedIndexChanged="ddlWorkshop_SelectedIndexChanged" ToolTip="Select Premises Workshop" CssClass="form-control form-control-sm" runat="server">
                                        <asp:ListItem Text="Select" Value="0" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="N"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-6" id="divWorkshops" runat="server" visible="false">
                                    <asp:Label runat="server" CssClass="form-control-label">Workshop List<span style="color: red"></span></asp:Label>
                                    <asp:DropDownList ID="ddlWorkshoplist" ToolTip="list" Visible="true" CssClass="form-control form-control-sm" runat="server">
                                        <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Test1"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Test2"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="Test3"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-lg-12 mt-3">
                                <asp:Label ID="lblContactDetails" runat="server" CssClass="form-control-label" Font-Bold="true" Font-Size="Medium">Contacts Details</asp:Label>
                            </div>
                            <div class="row m-0 mt-2 align-items-center">
                                <div class="col-lg-12">
                                    <div class="row m-0">
                                        <div class="col-lg-4">
                                            <asp:Label runat="server" CssClass="form-control-label">Contact Number 1<span style="color: red">*</span></asp:Label>
                                            <asp:TextBox ID="tbContactNumber1" class="form-control form-control-sm" runat="server" MaxLength="11" ToolTip="Contact Number 1" autocomplete="off"
                                                placeholder="Landline/Mobile" Text="" Style=""></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="ajaxFttbContactNumber1" runat="server" FilterType="Numbers, Custom" TargetControlID="tbContactNumber1" />
                                        </div>
                                        <div class="col-lg-4">
                                            <asp:Label runat="server" CssClass="form-control-label">Contact Number 2</asp:Label>
                                            <asp:TextBox ID="tbContactNumber2" class="form-control form-control-sm" runat="server" MaxLength="11" ToolTip="Contact Number 2" autocomplete="off"
                                                placeholder="Landline/Mobile" Text="" Style=""></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="ajaxFttbContactNumber2" runat="server" FilterType="Numbers, Custom" TargetControlID="tbContactNumber2" />
                                        </div>
                                        <div class="col-lg-4">
                                            <asp:Label runat="server" CssClass="form-control-label">Longitude</asp:Label>
                                            <asp:TextBox ID="tbLongitude" class="form-control form-control-sm" runat="server" MaxLength="10" ToolTip=" Enter Longitude" autocomplete="off"
                                                placeholder="Longitude" Text="" Style=""></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="ajaxFTtbLongitude" runat="server" ValidChars="." FilterType="Numbers, Custom"
                                                TargetControlID="tbLongitude" />
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <div class="row m-0 mt-2 align-items-center">
                                <div class="col-lg-12">
                                    <div class="row m-0">
                                        <div class="col-lg-4">
                                            <asp:Label runat="server" CssClass="form-control-label">Latitude</asp:Label>
                                            <asp:TextBox ID="tbLatitude" class="form-control form-control-sm" runat="server" MaxLength="10" ToolTip="Enter Latitude" autocomplete="off"
                                                placeholder="Latitude" Text="" Style=""></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="ajaxFTtbLatitude" runat="server" FilterType="Numbers, Custom"
                                                TargetControlID="tbLatitude" ValidChars="." />
                                        </div>
                                        <div class="col-lg-4">
                                            <asp:Label runat="server" CssClass="form-control-label">State<span style="color: red">*</span></asp:Label>
                                            <asp:DropDownList ID="ddlstate" ToolTip="select State" CssClass="form-control form-control-sm" OnSelectedIndexChanged="ddlstate_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>


                                        </div>
                                        <div class="col-lg-4">
                                            <asp:Label runat="server" CssClass="form-control-label">District<span style="color: red">*</span></asp:Label>
                                            <asp:DropDownList ID="ddlDistrict" ToolTip="select District" CssClass="form-control form-control-sm" runat="server"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-12">
                                <div class="row m-0 mt-2 align-items-center">
                                    <div class="col-lg-12">
                                        <div class="row m-0">
                                            <div class="col-lg-6">
                                                <asp:Label runat="server" CssClass="form-control-label">Address<span style="color: red">*</span></asp:Label>
                                                <asp:TextBox ID="tbAddress" class="form-control form-control-sm" TextMode="MultiLine" runat="server" MaxLength="200" ToolTip="Enter Address" autocomplete="off"
                                                    placeholder="Max 200 Characters.." Text="" Style=""></asp:TextBox>
                                                <%--                                            <cc1:FilteredTextBoxExtender ID="ajaxFtAddress" runat="server" FilterType="UppercaseLetters, LowercaseLetters,Numbers, Custom" TargetControlID="tbAddress" />--%>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <div class="col-lg-12 mt-3 ml-1 mb-3 text-left">
                                    <asp:LinkButton ID="lbtnFSUpdate" runat="server" CommandName="Update" OnClick="lbtnFSUpdate_Click" CssClass="btn btn-primary" Visible="false" ToolTip="Click Here for Update">
                                    <i class="fa fa-save"></i>&nbsp; Update</asp:LinkButton>
                                    <asp:LinkButton ID="lbtnFSSave" Visible="true" runat="server" CommandName="Save" OnClick="lbtnFSSave_Click" CssClass="btn btn-success" ToolTip="Click Here for Save">
                                    <i class="fa fa-save"></i>&nbsp; Save</asp:LinkButton>
                                    <asp:LinkButton ID="lbtnFSReset" OnClick="lbtnFSReset_Click" CommandName="Reset" runat="server" CssClass="btn btn-danger" ToolTip="Click Here for Reset">
                                    <i class="fa fa-undo"></i>&nbsp; Reset</asp:LinkButton>

                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>

            </div>
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

