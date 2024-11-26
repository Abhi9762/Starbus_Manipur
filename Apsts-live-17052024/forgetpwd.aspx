<%@ Page Language="C#" AutoEventWireup="true" CodeFile="forgetpwd.aspx.cs" Inherits="forgetpwd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="Start your development with a Dashboard for Bootstrap 4.">
    <meta name="author" content="Creative Tim">
    <title>StarBus* | APSTS ver 4.0</title>
    <!-- Favicon -->
    <link rel="icon" href="assets/img/brand/favicon.png" type="image/png">
    <!-- Icons -->
    <link rel="stylesheet" href="assets/vendor/nucleo/css/nucleo.css" type="text/css">
    <link rel="stylesheet" href="assets/vendor/@fortawesome/fontawesome-free/css/all.min.css" type="text/css">
    <!-- Page plugins -->
    <!-- Argon CSS -->
    <link rel="stylesheet" href="assets/css/argon.css?v=1.2.0" type="text/css">
    <script type="text/javascript">
        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };
    </script>
    <style>
    </style>
    <style>
        .center1 {
            margin-top: 8%;
            width: 100%;
            padding: 20px;
        }
    </style>
    <script type="text/javascript">

        function ClearValues() {

            
            document.getElementById("tbpasss").value = "";
            document.getElementById("tbconfirmpass").value = "";
        }

        //----------------------------------------------------------------//
        function setfocusSHA256(seed) {
            if (validfields() == false)
            { return false; }
            else
            {
              
                return true;
            }
            //----------------------------------------------------------------//
        }
        function validfields() {
            var isValid = true;
            var errorMsg = '';
            var space = '';
            var cnt = 0;
          
            //----------------------------------------------------------------//
          
            var strNewPwd = document.getElementById("tbpasss").value;
            var strConfirmPwd = document.getElementById("tbconfirmpass").value;
            var Lnewpwd = strNewPwd.length;
            var Lconfirmpwd = strConfirmPwd.length;
            


           

            //----------------------------------------------------------------//
            //password length
            if (Lnewpwd < 10 || Lnewpwd>15 || Lconfirmpwd < 10 || Lconfirmpwd>15) {
                errorMsg += "\n Password length must be minimum 10 charecter and maximum 15 charecters.";
                cnt++;
            }


            //if (!((Lnewpwd >= 10 && Lnewpwd <= 15 && Lconfirmpwd >= 10 && Lconfirmpwd <= 10))) {
            //    errorMsg += "\n Password length must be minimum 10 charecter and maximum 15 charecters.";
            //    cnt++;
            //}

            //It must not contain a space
            if (/\s/g.test(strNewPwd) && /\s/g.test(strConfirmPwd)) {
                errorMsg += "\n Passwords cannot include a space.";
                cnt++;
            }

            //It must contain at least one number character
            if (!(strNewPwd.match(/\d/)) && !(strConfirmPwd.match(/\d/))) {
                errorMsg += "\n Password must include at least one number.";
                cnt++;
            }

            //It must contain at least one special character
            if (!(strNewPwd.match(/\W+/)) && !(strConfirmPwd.match(/[#@%!]/))) {
                errorMsg += "\n Password must include at least one special character - #,@,%,!";
                cnt++;
            }

            //new password and confirm new password must be same    
            if (strNewPwd != strConfirmPwd) {
                errorMsg += "\n New password and confirm new password must be same. ";
                cnt++;
            }
            //----------------------------------------------------------------//
            if (cnt > 0) {
                alert(errorMsg);
                ClearValues();
                return false;
            }
            else {

                return true;
            }

        }


        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };


    </script>
</head>
<body  style="background-color:darkblue;">
    <form id="form1" runat="server">
        <div id="container-fluid">
            <div class="row">
                <div class="col-2"></div>
                <div class="col-8">
                    <div class="card shadow center1">
                        <div class="row">
                            <div class="col">
                                <a href="Home.aspx" style="cursor: pointer;" title="Home"><i class="fa fa-home fa-2x homeIcon"></i></a>
                            </div>
                            <div class="col">
                                <asp:Image ID="ImgDepartmentLogo" runat="server" Visible="true" Style="border-width: 0px; height: 75px; width: 75px; border: 2px solid #eaf4ff;" />
                            </div>
                            <div class="col-auto">
                                <a href="login.aspx" style="cursor: pointer; float: right;" title="Login">
                                    <i class="fa fa-arrow-left fa-2x homeIcon"></i></a>
                            </div>
                        </div>
                        <h1 class="text-center p-1" style="margin-left: 40px;">Recover Your Password</h1>
                        <asp:Panel runat="server" ID="pnlDetails" Visible="true">
                            <div class="row mt-1">

                           
                                   <div class="col-lg-4" style="font-size: 13px; padding-bottom: 10px; border-right: 1px solid #f9eaea;">
                            <div style="margin-bottom: 0px; margin-top: 0px;">
                                <span style="color: Red; font-size:larger;font-weight:bold;">Kindly Check</span><br />
                                1. To recover password, 6 digit  OTP (One Time Password) will be delievered to your registered mobile as SMS and whatsapp. You are requested to keep ready your registred mobile.
                                                        <br />
                                <br />
                                2.  To protect your account from unauthorised access keep your password safe.<br /> <br />
                                3.  <a href="../HelpDesk.aspx" style="text-decoration: none;" title="Click to get Helpdesk details"><span style="font-size: 13px; color: Green">In case of any Query/Help Please Feel Free to Contact Helpdesk </span></a>
                            </div>
                        </div>

                              
                                <div class="col-8 p-3">
                                    <p class="text-center text-danger bold"> Please enter User Id and registered mobile number.</p>

                                    <div class="row pl-4 pr-4 mt-2">
                                        <div class="col-4 text-left">
                                            User Id
                                        </div>
                                        <div class="col-7">
                                            <asp:TextBox ID="tbuserid" runat="server" AutoComplete="off" MaxLength="20" placeholder="Enter User Id" CssClass="form-control"> </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row pl-4 pr-4 mt-2">
                                        <div class="col-4 text-left">
                                            Mobile No.
                                        </div>
                                        <div class="col-7">
                                            <asp:TextBox runat="server" ID="tbmobile" AutoComplete="off" MaxLength="10" placeholder="Enter Mobile No." CssClass="form-control "> </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row pl-4 pr-4 mt-2">
                                        <div class="col-4 text-left">
                                        </div>
                                        <div class="col-7 input-group mb-33">
                                         
                                            <img alt="Visual verification" title="Please enter the security code as shown in the image."
                                                src="CaptchaImage.aspx" style="border: 1px solid #808080;border-radius: 4px 0px 0px 4px; width: 88%;height:90%" />
                                            <asp:LinkButton runat="server" ID="lbtnRefresh" CssClass=" btn btn-primary btn-sm p-2 btn-refresh" Style=" width: 12%;border-radius: 0px 4px 4px 0px;height:90%;" OnClick="lbtnRefresh_Click"><i class="fa fa-recycle" style="padding-top: 4px;"></i></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="row pl-4 pr-4 mt-2">
                                        <div class="col-4 text-left">
                                            Captcha
                                        </div>
                                        <div class="col-7">
                                            <asp:TextBox runat="server" AutoComplete="off" ID="tbcaptchacode" placeholder="Enter Captcha Code" CssClass="form-control"> </asp:TextBox>
                                        </div>
                                    </div>

                                       <div class="row mt-4">

                                           <div class="col-lg-5"></div>
                                    <div class="col-lg-3 text-center">
                                        <asp:LinkButton runat="server" ID="lbtncontinue" ToolTip="Click here to continue" OnClick="Continue_Click" CssClass="btn btn-success"> <i class="fa fa-check"> Continue </i></asp:LinkButton>
                                    </div>
                                    <div class="col-lg-2 text-center">
                                          <asp:LinkButton ID="lbtnreset" runat="server" CssClass="btn btn-warning" OnClick="lbtnreset_Click" ToolTip="Click here to reset entered value"> <i class="fa fa-undo"> Reset </i> </asp:LinkButton>
                                    </div>
                                           <div class="col-lg-2"></div>
                                </div>
                                    <div class="row mt-3">
                                        <div class="col-12">

                                            <div class="col-6 text-center">
                                                
                                            </div>
                                          <div class="col-6 text-center">
                                      
                                    </div>


                                        </div>
                                    </div>


                                </div>


                            </div>

                              <div class="row" style="width: 100%;">
                        <div style="width: 100%; border-top: 1px solid #0169b7; font-size: 9pt; margin-top: 10px; padding-top: 5px; text-align: center;">

                            <strong>Contents Published ,Managed and Maintained </strong><asp:label runat="server" ID="lbldeptname" Text=""></asp:label>
                        <br />
                            <strong>Design & Developed by </strong><asp:label runat="server" ID="lbldevelopedby" Text=""></asp:label>
                    
               
                        </div>
                    </div>
                        </asp:Panel>

                        <asp:Panel runat="server" ID="pnlOtp" Visible="false">
                            <div class="row mt-3">
                                <div class="col-4 border-right border-dark p-3">
                                    <h4 class="text-danger bold">Please Note</h4>
                                    <p class="text-sm text-justify ml-2">
                                        <span class="font-weight-bolder">1. </span>Password should not be less than 10 characters or greater than 15 characters.
                                    </p>
                                    <p class="text-sm text-justify ml-2">
                                        <span class="font-weight-bolder">2. </span>
                                        Password should contain At least one numeric value.
                                    </p>
                                    <p class="text-sm text-justify ml-2">
                                        <span class="font-weight-bolder">2. </span>
                                        Password should contain At least one special case characters ( @,$,!,%,*,#,?,& ).
                                    </p>
                                    <p class="text-sm text-justify ml-2">
                                        <span class="font-weight-bolder">2. </span>
                                        Passwords cannot include a space.
                                    </p>

                                </div>
                                <div class="col-8 p-3">
                                    <p class="text-center text-danger bold">Please enter OTP , Which is sent to your mobile number.</p>

                                    <div class="row pl-4 pr-4 mt-2">
                                        <div class="col-4 text-left">
                                            OTP
                                        </div>
                                        <div class="col-7">
                                            <asp:TextBox ID="tbotp" runat="server" AutoComplete="off" MaxLength="6" placeholder="Enter OTP" CssClass="form-control" Text=""> </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row pl-4 pr-4 mt-2">
                                        <div class="col-4 text-left">
                                            New Password
                                        </div>
                                        <div class="col-7">
                                            <asp:TextBox runat="server" TextMode="Password" ID="tbpasss" AutoComplete="off" MaxLength="15" placeholder="Enter New Password" CssClass="form-control"  Text=""> </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row pl-4 pr-4 mt-2">
                                        <div class="col-4 text-left">
                                            Confirm New Password
                                        </div>
                                        <div class="col-7">
                                            <asp:TextBox runat="server" TextMode="Password" ID="tbconfirmpass" AutoComplete="off" MaxLength="15" placeholder="Enter Confirm Password" CssClass="form-control "> </asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="row pl-4 pr-4 mt-3">
                                        <div class="col-12">
                                            <center>
                                              <asp:LinkButton runat="server" ID="lbtnchangepass" OnClick="lbtnchangepass_Click" CssClass="btn btn-success">Change Password</asp:LinkButton>
                                       </center>


                                        </div>
                                    </div>


                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
                <div class="col-3"></div>
            </div>
        </div>
    </form>
</body>
</html>
