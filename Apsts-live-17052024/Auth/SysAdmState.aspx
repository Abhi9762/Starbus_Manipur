<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/SysAdmmaster.master"  EnableEventValidation="false"  AutoEventWireup="true" MaintainScrollPositionOnPostback="false" CodeFile="SysAdmState.aspx.cs" Inherits="Auth_SysAdmState" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container-fluid pb-5">
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
                                            <asp:Label ID="lblSummary" runat="server" CssClass="text-capitalize">Summary as on Date 01/12/2021 04:00pm</asp:Label></h4>
                                        <div class="row m-0">
                                            <div class="col-6 border-right">
                                                <div class="row m-0">
                                                    <div class="col-12">
                                                        <h5 class="card-title text-uppercase text-muted mb-0">Total State:&nbsp;
                                 <asp:Label ID="lblTotalState" runat="server" ToolTip="Total States Available" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>
                                                    </div>
                                                    <div class="col-12">
                                                        <h5 class="card-title text-uppercase text-muted mb-0">Configured State: &nbsp;
                                <asp:Label ID="lblConfiguared" runat="server" ToolTip="Configured State" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>
                                                    </div>
                                                    

                                                </div>
                                            </div>
                                            <div class="col-6">
                                                <div class="row m-0">
                                                    <div class="col-12">
                                                        <h5 class="card-title text-uppercase text-muted mb-0">Discontinue: &nbsp;
                                <asp:Label ID="lblDiscontinue" runat="server" ToolTip="Discontinue" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>
                                                    </div>
                                                    <div class="col-12">
                                                        <h5 class="card-title text-uppercase text-muted mb-0">Active: &nbsp;
                                <asp:Label ID="lblActive" runat="server" ToolTip="Active State" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>

                                </div>

                            </div>
                        </div>
                        <div class="col-4 border-right">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col">
                                        <div class="input-group-prepend">
                                           
                                            <h4>Generate State Report</h4>
                                             <asp:LinkButton ID="lbtndownload"  ToolTip="Download Report" runat="server" CssClass="btn btn bg-gradient-green btn-sm text-white ml-2">
                                            <i class="fa fa-download"></i>
                                            </asp:LinkButton>
                                        </div>
                                        
                                       
                                    </div>
                                </div>

                            </div>
                        </div>
                        <div class="col-4">
                            <div class="card-body">
                                <div class="row mr-0">
                                    <div class="col">
                                        <div>
                                            <h4 class="mb-1">Instructions</h4>
                                        </div>
                                        <asp:Label runat="server" CssClass="form-control-label">1. Here you can add state along with its Fare Type.</asp:Label><br />
                                        <asp:Label runat="server" CssClass="form-control-label">2. You can also update state details like State Name(Hi), Abbreviation, Fare Type and status.</asp:Label>
                                    <br /> <asp:Label runat="server" CssClass="form-control-label">3. Added state can be delete.</asp:Label>
                                 
                                    </div>
                                    <div class="col-auto">

                                        <asp:LinkButton ID="lbtnview" OnClick="lbtnview_Click" ToolTip="View Instructions" runat="server" CssClass="btn btn bg-gradient-orange btn-sm text-white">
                                    <i class="fa fa-info"></i>
                                        </asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="lbtndwnldinst" OnClick="lbtndwnldinst_Click" class="btn btn bg-gradient-green btn-sm text-white" ToolTip="Download Instructions">
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
            
            <div class="col-xl-12 order-xl-2">
                <asp:Panel runat="server" ID="pnlUpdateState" Visible="false">
                <div class="card" style="min-height: 10px">
                    <div class="card-header border-bottom">
                        <div class="row">
                            <div class="col-lg-12">
                                <h3 class="mb-0">Update State Detail</h3>
                            </div>
                            
                       
                        </div>
                       
                    </div>
                    <div class="card-body">

                                 
                                        <div class="row m-0 align-items-center">
                                                            <div class="col-lg-12"">
                                                                <div class="row">
                                                                    <div class="col-lg-3">
                                                                        <label class="form-control-label">Name (En)</label>
                                                                        <br />
                                                                        <asp:TextBox CssClass="form-control form-control-sm" Enabled="false" runat="server" ID="tbUpdateStateNameEn" MaxLength="50"
                                                                            autocomplete="off" placeholder="Max 50 Characters" ToolTip="Sate Name In English" Text="" Style=""></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-lg-3">
                                                                       <label class="form-control-label"> State Name (Local) <span style="color: Green">(Optional)</span></label>
                                                                        <br />
                                                                        <asp:TextBox CssClass="form-control form-control-sm" ToolTip="State Name in Local" runat="server" ID="tbUpdateStateNameL" MaxLength="50"
                                                                            autocomplete="off" placeholder="Max 50 Characters" Text="" ></asp:TextBox>
                                                                    </div>
                                                                 
                                                                    <div class="col-lg-3">
                                                                        <label class="form-control-label">Fare Type</label><br />
                                                                        <asp:DropDownList ID="ddlUpdateFareType" ToolTip="Fare Type" runat="server" Style="padding: .175rem .75rem;"
                                                                            CssClass="form-control form-control-sm">
                                                                             <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                                            <asp:ListItem Text="Per Km" Value="P"></asp:ListItem>
                                                                            <asp:ListItem Text="Slab" Value="S"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                     
                                                                </div>
                                                               <div class="row mt-4">
                                                                    <div class="col-lg-3">
                                                                        <asp:CheckBox ID="cbAccidentSurChrg" runat="server" /> <span style="font-size:16px; padding-left:5px;"> Accident Surcharge</span>
                                                                    </div>
                                                                    <div class="col-lg-3">
                                                                        <asp:CheckBox ID="cbPassengerSurChrg" runat="server" /><span style="font-size:16px; padding-left:5px;">Passenger Surcharge</span>
                                                                    </div>
                                                                    <div class="col-lg-3">
                                                                        <asp:CheckBox ID="cbItSurChrg" runat="server" /> <span style="font-size:16px; padding-left:5px;">IT Surcharge</span>
                                                                    </div>
                                                                    <div class="col-lg-3">
                                                                        <asp:CheckBox ID="cbOtherSurChrg" runat="server" /> <span style="font-size:16px; padding-left:5px;">Other Surcharge</span>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-lg-12 mt-3 text-center">
                                                              
                                                                    <asp:LinkButton ID="lbtnUpdate" runat="server" CssClass="btn btn-sm btn-success" OnClick="lbtnUpdate_Click"  > <i class="fa fa-check" ></i> Update</asp:LinkButton>
                                                                    <asp:LinkButton ID="lbtnCancel" runat="server" CssClass="btn btn-sm btn-danger ml-3" OnClick="lbtnCancel_Click"> <i class="fa fa-times" ></i> Cancel</asp:LinkButton>
                                                               
                                                            </div>
                                                        </div>
                                                  
                                                </div>
                </div></asp:Panel>
                 <asp:Panel ID="pnlStateAll" runat="server">
                <div class="card" style="min-height: 470px">
                    <div class="card-header border-bottom">
                        <div class="row">
                            <div class="col-lg-8">
                                <h3 class="mb-0">State List (View/Update/Delete)</h3>
                            </div>
                             <div class="col-lg-4">
                                  <div class="float-right">
                            <div class="input-group">
                                <asp:TextBox ID="tbstatesearch" runat="server" CssClass="form-control form-control-sm mr-1" MaxLength="50" AutoComplete="off" placeholder="Enter State Name"></asp:TextBox>
                                            
                                            <asp:LinkButton ID="lbtnsearch" OnClick="lbtnsearch_Click" runat="server" class="btn btn-sm btn-primary mr-1" ToolTip="Search"> <i class="fa fa-search"></i></asp:LinkButton>
                                                 <asp:LinkButton ID="lbtnReset" OnClick="lbtnReset_Click" runat="server" class="btn btn-sm btn-danger mr-1" ToolTip="Reset"> <i class="fa fa-undo"></i></asp:LinkButton>
                                      
                            

                            </div>
                                     </div>  
                             </div>
                       
                        </div>
                       
                    </div>
                    <div class="card-body">
                        
                           
                                <div class="row m-0 align-items-center">
                                  
                                 <div class="col-lg-12">
                                <asp:GridView ID="gvStateList" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover" OnRowDataBound="gvStateList_RowDataBound" GridLines="None" 
                                    HeaderStyle-CssClass="thead-light font-weight-bold" OnRowCommand="gvStateList_RowCommand" 
                                    DataKeyNames="stat_code,state_name,state_abbr,faretype,status,accident_charge,passenger_charge,it_charge,other_charge">
                                    <Columns>
                                       <asp:TemplateField  HeaderText="Sr. No">
            <ItemTemplate>
                <%#Container.DataItemIndex+1 %>
            </ItemTemplate>
        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="State">
                                            <ItemTemplate>
                                                 <%# Eval("status").ToString() == "A" ? "<i class='fa fa-check-circle mr-2 text-success'></i>" : "<i class='fa fa-times-circle mr-2 text-danger'></i>" %>
                                                <asp:Label CssClass="form-control-label" runat="server" Text='<%#Eval("state_name") %>' />
                                                (  <asp:Label CssClass="form-control-label" runat="server" Text='<%#Eval("state_abbr") %>' />)
                                                 <asp:Label ID="LabelStateIdSaved" runat="server" Visible="false" Text='<%#Eval("state_id") %>' />
                                                                <asp:Label ID="LabelStateFareTypeSaved" runat="server" Visible="false" Text='<%#Eval("faretype") %>' />
                                                                <asp:Label ID="LabelStateStatusSaved" runat="server" Visible="false" Text='<%#Eval("status") %>' />

                                                <asp:Label ID="ACCIDENT_SURCHARGE" runat="server" Visible="false" Text='<%#Eval("accident_charge") %>' />
                                                                <asp:Label ID="PASSENGER_SURCHARGE" runat="server" Visible="false" Text='<%#Eval("passenger_charge") %>' />
                                                                <asp:Label ID="IT_SURCHARGE" runat="server" Visible="false" Text='<%#Eval("it_charge") %>' />
                                                <asp:Label ID="OTHER_SURCHARGE" runat="server" Visible="false" Text='<%#Eval("other_charge") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                          <asp:TemplateField HeaderText="Accident Surcharge">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="cbAccidentSurChrg" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Passenger Surcharge">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="cbPassengerSurChrg" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="IT Surcharge">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="cbItSurChrg" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Other Surcharge">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="cbOtherSurChrg" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                     
                                        <asp:TemplateField HeaderText="Fare Type" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlFareType" Style="width: 125px; font-size: 14px;" CssClass="form-control form-control-sm" runat="server">
                                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Slab" Value="S"></asp:ListItem>
                                                    <asp:ListItem Text="Per Km" Value="P"></asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnSaveStatee" Visible="true" ToolTip="Save State" runat="server" CommandName="SaveState" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CssClass="btn btn-sm btn-success" Style="border-radius: 4px;"> <i class="fa fa-save"></i> Save</asp:LinkButton>
                                             <asp:LinkButton ID="lbtnUpdateStatee" Visible="true" ToolTip="Update State" runat="server" CommandName="UpdateState" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CssClass="btn btn-sm btn-primary " Style="border-radius: 4px;"> <i class="fa fa-edit"></i> Update</asp:LinkButton>
                                         <asp:LinkButton ID="lbtndeleteStatee" Visible="true" runat="server" ToolTip="Delete State" CommandName="DeleteState" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CssClass="btn btn-sm btn-danger" Style="border-radius: 4px;"> <i class="fa fa-trash "></i> Delete</asp:LinkButton>
                                    <asp:LinkButton ID="lbtnActivate" Visible="true" runat="server" ToolTip="Activate State" CommandName="ActivateState" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CssClass="btn btn-sm btn-success" Style="border-radius: 4px;"> <i class="fa fa-check  "></i> Activate</asp:LinkButton>
                                    <asp:LinkButton ID="lbtnDiscontinue" Visible="true" runat="server" ToolTip="Discontinue State" CommandName="DiscontinueState" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CssClass="btn btn-sm btn-warning" Style="border-radius: 4px;"> <i class="fa fa-times-circle "></i> Discontinue</asp:LinkButton>
                                   
                                                </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <p style="color: red; text-align: center; font-weight: bold; font-size: 12pt;">No Records Found.</p>
                                    </EmptyDataTemplate>
                                    <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                                </asp:GridView>
                                <asp:Panel runat="server" ID="pnlNoState" Visible="true" CssClass="text-center" Width="100%">
                                    <p class="text-center" style="font-size: 32px; font-weight: bold; color: #e3e3e3; margin-top: 50px;">
                                        State Not Available
                                    </p>
                                </asp:Panel>
                            </div></div>
                            
                           
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
                        <asp:LinkButton ID="lbtnYesConfirmation" runat="server" OnClick="lbtnYesConfirmation_Click" CssClass="btn btn-success btn-sm"> <i class="fa fa-check"></i> Yes </asp:LinkButton>
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

