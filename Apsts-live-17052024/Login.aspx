<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="Start your development with a Dashboard for Bootstrap 4.">
    <meta name="author" content="Creative Tim">
     <title>StarBus* | MSTS ver 1.0</title>
<link rel="icon" href="Logo/Favicon.png" type="image/png" />
    <link rel="stylesheet" href="assets/vendor/@fortawesome/fontawesome-free/css/all.min.css" type="text/css">
    <link rel="stylesheet" href="assets/css/argon.css?v=1.2.0" type="text/css">
    <script src="js/sha1.js"></script>
    <script type="text/javascript" src="assets/js/jquery-n.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script type="text/javascript" language="javascript">
        //function setfocusSHA11(seed) {

        //    var password = document.getElementById('tbpassword').value
        //    var seedpwd = seed + password;
        //    var hash = SHA1(seedpwd);
        //    document.getElementById('hidHash').value = hash;
        //    document.getElementById('tbpassword').value = hash
        //    return true;
        //}

        //function setSHA11() {

        //    var password = document.getElementById('tbpassword').value
        //    var pwd = SHA1(password);
        //    document.getElementById('tbpassword').value = pwd
        //    return true;
        //}

        function setSHANew() {
            
            var authseed = document.getElementById('hidSeed').value
            var password = document.getElementById('tbpassword').value
            password = SHA1(password);
            var seedpwd = authseed + password;
            var hash = SHA1(seedpwd);
            document.getElementById('hidHash').value = hash;
            document.getElementById('tbpassword').value = hash
            //alert(hash);
        }
function emptyTBpass() {
           
            var hash = "";
            
            document.getElementById('tbpassword').value = hash
            //alert(hash);
        }
    </script>
    <script type="text/javascript">
        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };
    </script>
</head>
<body class="h-100" onload="HideLoading()">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:HiddenField ID="hidtoken" runat="server" />
        <asp:HiddenField ID="hidHash" runat="server" />
        <asp:HiddenField ID="hidSeed" runat="server" />

        <div class="container-fluid full-bg h-100">
            <div class="container min-vh-100">
                <%
                    sbLoaderNdPopup dd = new sbLoaderNdPopup();
                    string ss = dd.getLoaderHtml();
                    Response.Write(ss);
                %>
                <div class="row no-margin">
                    <div class="col-lg-3">

                        <div class="bg-layer  ">

                            <div class="login-box row">
                                 <a href="home.aspx" class="pp">MSTS</a>
                                <h3>User login</h3>
                                <div class="input-group mb-33">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text" id="basic-addon1"><i class="fas fa-at"></i></span>
                                    </div>
                                    <asp:TextBox ID="tbuserid" runat="server" oncopy="return false" onpaste="return false"

            oncut="return false" class="form-control" placeholder="Enter User Id" Style="border-radius: 0px 4px 4px 0px;" autocomplete="off" MaxLength="15" ToolTip="Enter User Id"></asp:TextBox>
                                </div>
                                <div class="input-group mb-33">
                                    <div class="input-group-prepend">

                                        <span class="input-group-text" id="basic-addon1"><i class="fas fa-key"></i></span>
                                    </div>
                                    <asp:TextBox ID="tbpassword" runat="server" oncopy="return false" onpaste="return false"

            oncut="return false" class="form-control" placeholder="Enter Password" Style="border-radius: 0px 4px 4px 0px;"  onBlur="return setSHA1();" TextMode="Password" autocomplete="off" MaxLength="20" ToolTip="Enter Password"></asp:TextBox>
                                
                                    </div>
                                <div class="input-group mb-33">
                                      <div class="input-group-prepend">
                                    <img alt="Visual verification" title="Please enter the security code as shown in the image."
                                        src="CaptchaImage.aspx" class="input-group-text"  style="width: 87%;height:85%; border: 1px solid #808080;" />
                                          
                                    <asp:LinkButton runat="server" ID="lbtnRefresh" OnClientClick="return emptyTBpass();" CssClass=" btn btn-primary p-2 btn-refresh" Style="margin-top: 1px; width: 13%;height:85%; border-radius: 0px 4px 4px 0px;" OnClick="lbtnRefresh_Click"><i class="fa fa-recycle" style="padding-top: 4px;"></i></asp:LinkButton>
                                </div>
                                    </div> 
                                <div class="input-group mb-33">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text" id="basic-addon1"><i class="fas fa-user-shield"></i></span>
                                    </div>
                                    <asp:TextBox ID="tbcaptchacode" runat="server" Style="border-radius: 0px 4px 4px 0px;"  placeholder="Enter Text" autocomplete="off" class="form-control text-uppercase" MaxLength="6"></asp:TextBox>
                                </div>
                                <div class="input-group mb-33">
                                    <div class="col-lg-6 text-center">
                                        <asp:LinkButton ID="lbtnlogin" runat="server" CssClass="btn btn-success" ToolTip="Click here to login"
                                            OnClick="lbtnlogin_Click" OnClientClick="return setSHANew();"><i class="fa fa-check"> Login </i></asp:LinkButton>
                                    </div>
                                    <div class="col-lg-6 text-center">
                                        <asp:LinkButton ID="lbtnreset" runat="server" CssClass="btn btn-warning" ToolTip="Click here to reset entered value"
                                            OnClick="lbtnreset_Click"> <i class="fa fa-undo"> Reset </i> </asp:LinkButton>
                                    </div>
                                </div>
                                 <div class="rt">
                                            <a href="forgetpwd.aspx" style="cursor: pointer;color:white" title="Forget Password"> How to Recover Your Password?</a>
                      
                                </div>

                            </div>

                        </div>
                    </div>
                </div>
                <div class="foter-credit">
                    <a href="#">© 2021 National Informatics Centre. All rights reserved. </a>
                </div>

                <div class="row">
                    <cc1:ModalPopupExtender ID="mpConfirmation" runat="server" PopupControlID="pnlConfirmation"
                        CancelControlID="lbtnNoConfirmation" TargetControlID="Button4" BackgroundCssClass="modalBackground">
                    </cc1:ModalPopupExtender>
                    <asp:Panel ID="pnlConfirmation" runat="server" Style="position: fixed;">
                        <div class="card">
                            <div class="card-header">
                                <h4 class="card-title text-left mb-0">Please Confirm
                                </h4>
                            </div>
                            <div class="card-body text-center pt-2" style="min-height: 100px; color: black;">
                                <asp:Label ID="lblConfirmation" runat="server" Text="Do you want to save ?"></asp:Label>
                                <div class="row mt-1 p-2">
                                    <div class="col-lg-3"></div>
                                    <div class="col-lg-3 text-center">
                                        <asp:LinkButton ID="lbtnYesConfirmation" OnClick="lbtnYesConfirmation_Click" runat="server" CssClass="btn btn-success btn-sm"> <i class="fa fa-check"></i> Yes </asp:LinkButton>
                                    </div>
                                    <div class="col-lg-3 text-center">
                                        <asp:LinkButton ID="lbtnNoConfirmation" runat="server" CssClass="btn btn-danger btn-sm ml-2"> <i class="fa fa-times"></i> No </asp:LinkButton>
                                    </div>
                                    <div class="col-lg-3"></div>
                                </div>
                            </div>
                        </div>
                        <div style="visibility: hidden;">
                            <asp:Button ID="Button4" runat="server" Text="" />
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>



    </form>
</body>
</html>
