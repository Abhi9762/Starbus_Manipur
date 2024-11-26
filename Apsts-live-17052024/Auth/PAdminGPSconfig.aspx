<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/PortalAdmmaster.master" AutoEventWireup="true" CodeFile="PAdminGPSconfig.aspx.cs" Inherits="Auth_PAdminGPSconfig" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        $(function () {           

            //Enable Disable TextBoxes in a Row when the Row CheckBox is checked.
            $("[id*=cbGPSurls]").bind("click", function () {

                //Find and reference the GridView.
                var grid = $(this).closest("table");

                

                //If the CheckBox is Checked then enable the TextBoxes in thr Row.
                if (!$(this).is(":checked")) {
                    var td = $("td", $(this).closest("tr"));
                    td.css({ "background-color": "#FFF" });
                    $("input[type=text]", td).attr("disabled", "disabled");
                } else {
                    var td = $("td", $(this).closest("tr"));
                    td.css({ "background-color": "#D8EBF2" });
                    $("input[type=text]", td).removeAttr("disabled");
                }

                //Enable Header Row CheckBox if all the Row CheckBoxes are checked and vice versa.
                if ($("[id*=cbGPSurls]", grid).length == $("[id*=cbGPSurls]:checked", grid).length) {
                    chkHeader.attr("checked", "checked");
                } else {
                    chkHeader.removeAttr("checked");
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid mt-1">
        <asp:HiddenField ID="hidtoken" runat="server" />
        <div class="row my-2">
            <div class="col-lg-4 col-md-4 order-xl-1">
                <div class="card" style="min-height: 460px">

                    <div class="card-header">

                        <div class="row m-0 align-items-center">

                            <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true"><h2><span style="color: #ff5e00;">Please Note</span></h2></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-8 col-lg-8 order-xl-2">
                <div class="card" style="min-height: 460px">
                    <div class="card-header">
                        <div class="row">
                        <div class="col-md-11 col-lg-11">
                            <asp:Label runat="server" CssClass="form-control-label"><h2>GPS Configuration</h2></asp:Label>
                        </div>
                        <div class="col-lg-1 col-md-1 float-left m-0 p-0">
                            <asp:LinkButton ID="lbtnHelp" runat="server" ToolTip="View Instructions" OnClick="lbtnHelp_Click" CssClass="btn btn bg-gradient-orange btn-sm text-white">
                                    <i class="fa fa-info"></i>
                            </asp:LinkButton>
                            <asp:LinkButton ID="lbtnViewHistory" runat="server" ToolTip="View History" OnClick="lbtnViewHistory_Click" CssClass="btn btn bg-gradient-primary btn-sm text-white">
                                    <i class="fa fa-history"></i>
                            </asp:LinkButton>
                        </div></div>
                    </div>


                     <asp:GridView ID="gvGPSUrls" runat="server" AutoGenerateColumns="False" GridLines="None"
                        AllowSorting="true" AllowPaging="true" PageSize="5" OnPageIndexChanging="gvGPSUrls_PageIndexChanging"  CssClass="table table-striped table-hover"
                         OnRowDataBound="gvGPSUrls_RowDataBound"
                        DataKeyNames="agency_code, gpsurl,itemid" HeaderStyle-CssClass="thead-light font-weight-bold" Font-Size="11pt">
                        <Columns>
                             <asp:TemplateField  HeaderText="S. No." ItemStyle-Width="5%" ItemStyle-VerticalAlign="Top">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                       
                                    </ItemTemplate>
                                </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:CheckBox ID="cbGPSurls" runat="server" CssClass="form-check" ClientIDMode="Static"></asp:CheckBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Agency">
                                <ItemTemplate>
                                    <asp:Label runat="server" CssClass="form-check-label" Text='<%# Eval("agency_code") %>'></asp:Label>
                                     <asp:HiddenField ID="hdnAgencyCode" runat="server" Value='<%#Eval("itemid") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="GPS URL">
                                <ItemTemplate>
                                    <asp:TextBox ID="tbGpsUrls" Enabled="false" CssClass="form-control form-control-sm" runat="server" Text='<%# Eval("gpsurl") %>' ClientIDMode="Static"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                    </asp:GridView>
                    <asp:Label ID="lblNoData"  runat="server" Visible="false"  CssClass="font-bold text-lg text-center text-gray mt-3 text-uppercase ">no gps url available </asp:Label>
                      <div class="col-md-12 col-lg-12 text-center pt-3">
                            <asp:LinkButton ID="lbtnSave" OnClick="lbtnSave_Click" runat="server" CssClass="btn btn-success"><asp:Label runat="server" CssClass="form-control-label1"><i class="fa fa-save">&nbsp;Save</i> </asp:Label> </asp:LinkButton>
                            
                        </div>
                </div>
            </div>
        </div>
    </div>
    
    <%-- //PopUp--%>
    
    <div class="row">
        <cc1:ModalPopupExtender ID="mplimithistory" runat="server" PopupControlID="pnllimithistory"
            TargetControlID="Button9" CancelControlID="btnclose" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnllimithistory" runat="server" Style="position: fixed; top: 30.5px; max-width: 800px; min-width: 400px; display: none;">
            <div class="card">
                <div class="card-header" style="border-color: #2dce89; background-color: #2dce89; color: white">
                    <strong class="card-title">General Configuration History</strong>
                </div>

                <div class="card-body" style="padding: 15px !important;">

                    <asp:Label runat="server" ID="noHistoryLbl" Text="No Record Found" Visible="false"></asp:Label>
                    <asp:GridView ID="gvHistory" runat="server" AutoGenerateColumns="False" GridLines="None"
                        AllowSorting="true" AllowPaging="true" PageSize="5" OnPageIndexChanging="gvHistory_PageIndexChanging" CssClass="table table-flush table mar table-responsive"
                        DataKeyNames="agency_code,gpsurl,actionby,actiondate" HeaderStyle-CssClass="thead-light font-weight-bold" Font-Size="11pt">
                        <Columns>
                            <asp:TemplateField HeaderText="Agency Code">
                                <ItemTemplate>
                                    <asp:Label  runat="server" Text='<%# Eval("agency_code") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="GPS URL">
                                <ItemTemplate>
                                    <asp:Label  runat="server" Text='<%# Eval("gpsurl") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                          
                            <asp:TemplateField HeaderText="Updated By">
                                <ItemTemplate>
                                    <asp:Label  runat="server" Text='<%# Eval("actionby") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Updation Date/Time">
                                <ItemTemplate>
                                    <asp:Label  runat="server" Text='<%# Eval("actiondate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                    </asp:GridView>
                    <asp:label ID="lblNoHistory" runat="server" Visible="false" CssClass="text-uppercase text-muted font-weight-bold">No GPS url History Found</asp:label>

                </div>
                <div class="card-footer">
                    <asp:LinkButton ID="btnclose" runat="server" CssClass="btn btn-success" Style="font-size: 11pt; float: right; border-radius: 4px;"><i class="fa fa-times"></i>&nbsp;&nbsp;Close</asp:LinkButton>
                </div>
            </div>
            <br />
            <div style="visibility: hidden;">
                <asp:Button ID="Button9" runat="server" Text="" />
            </div>
        </asp:Panel>
    </div>
    <cc1:ModalPopupExtender ID="mpConfirmation" runat="server" PopupControlID="pnlConfirmation"
        CancelControlID="lbtnNoConfirmation" TargetControlID="Button4" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlConfirmation" runat="server" Style="position: fixed;">
        <div class="card" style="min-width: 350px;">
            <div class="card-header">
                <h4 class="card-title">Please Confirm
                </h4>
            </div>
            <div class="card-body" style="min-height: 100px;">
                <asp:Label ID="lblConfirmation" runat="server"></asp:Label>
                <div style="width: 100%; margin-top: 20px; text-align: right;">
                    <asp:LinkButton ID="lbtnYesConfirmation" runat="server" CssClass="btn btn-success btn-sm" OnClick="lbtnYesConfirmation_Click"> <i class="fa fa-check"></i> Yes </asp:LinkButton>
                    <asp:LinkButton ID="lbtnNoConfirmation" runat="server" CssClass="btn btn-danger btn-sm ml-3"> <i class="fa fa-times"></i> No </asp:LinkButton>
                </div>
            </div>
        </div>
        <div style="visibility: hidden;">
            <asp:Button ID="Button4" runat="server" Text="" />
        </div>
    </asp:Panel>

</asp:Content>

