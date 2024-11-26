<%@ Page Language="C#" AutoEventWireup="true" CodeFile="changepassfirsttime.aspx.cs" Inherits="Auth_changepassfirsttime" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="Start your development with a Dashboard for Bootstrap 4.">
    <meta name="author" content="Creative Tim">
    <title>StarBus* | APSTS ver 4.0</title>
    <!-- Favicon -->
    <link rel="icon" href="../assets/img/brand/favicon.png" type="image/png">
    <!-- Icons -->
    <link rel="stylesheet" href="../assets/vendor/nucleo/css/nucleo.css" type="text/css">
    <link rel="stylesheet" href="../assets/vendor/@fortawesome/fontawesome-free/css/all.min.css" type="text/css">
    <!-- Page plugins -->
    <!-- Argon CSS -->
    <link rel="stylesheet" href="../assets/css/argon.css?v=1.2.0" type="text/css">
  <script src="../js/sha1.js"></script>
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
        function setSHANew() {
            if (validfields() == false) {
                //alert("false");
            }
            else {
                //alert("true");
                var tbOldPwd = document.getElementById('tbOldPwd').value
                var tbNewPwd = document.getElementById('tbNewPwd').value
                var tbConfirmNewPwd = document.getElementById('tbConfirmNewPwd').value
                tbOldPwd = SHA1(tbOldPwd);

                tbNewPwd = SHA1(tbNewPwd);
                tbConfirmNewPwd = SHA1(tbConfirmNewPwd);

                document.getElementById('tbOldPwd').value = tbOldPwd;
                document.getElementById('tbNewPwd').value = tbNewPwd;
                document.getElementById('tbConfirmNewPwd').value = tbConfirmNewPwd

                document.getElementById('hdoldpass').value = tbOldPwd;
                document.getElementById('hdnewpass').value = tbNewPwd;
                document.getElementById('hdconfirmnewpass').value = tbConfirmNewPwd


                //return true;
            }

        }
        function ClearValues() {

            document.getElementById("tbOldPwd").value = "";
            document.getElementById("tbNewPwd").value = "";
            document.getElementById("tbConfirmNewPwd").value = "";
        }

        //----------------------------------------------------------------//
        //function setfocusSHA256(seed) {
        //    if (validfields() == false)
        //    { return false; }
        //    else
        //    {
        //        var password = document.getElementById('tbOldPwd').value;
        //        var olPWD = sha256(password);
        //        var seedpwd = seed + olPWD;
        //        var hash = sha256(seedpwd);
        //        document.getElementById("hidHashOld").value = hash;

        //        var Confirmnewpass = sha256(document.getElementById("tbConfirmNewPwd").value);
        //        document.getElementById("hidHashConfirm").value = Confirmnewpass;
        //        ClearValues();
        //        return true;
        //    }
        //    //----------------------------------------------------------------//
        //}
        function validfields() {
            var isValid = true;
            var errorMsg = '';
            var space = '';
            var cnt = 0;
            var strOldPwd = document.getElementById("tbOldPwd").value;
            //----------------------------------------------------------------//
            errorMsg = "Please check:-";
            if (strOldPwd.length <= 0) {
                errorMsg += "\n Old Password has not been entered.";
                cnt++;
            }
            var strNewPwd = document.getElementById("tbNewPwd").value;
            var strConfirmPwd = document.getElementById("tbConfirmNewPwd").value;
            var Lnewpwd = strNewPwd.length;
            var Lconfirmpwd = strConfirmPwd.length;





            //----------------------------------------------------------------//
            //password length
            if (Lnewpwd < 8 || Lnewpwd > 10 || Lconfirmpwd < 8 || Lconfirmpwd > 10) {
                errorMsg += "\n Password length must be minimum 8 charecter and maximum 10 charecters.";
                cnt++;
            }



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

            //if (!(strNewPwd.match(/^(?=.[A-Z])(?=.[a-z])(?=.\d)(?=.[@$!%?&])(?!.(\d)\d*\1)(?!.(012|123|234|345|456|567|678|789|890|210|321|432|543|654|765|876|987))[A-Za-z\d@$!%?&]{8,10}$/)) && !(strConfirmPwd.match(/^(?=.[A-Z])(?=.[a-z])(?=.\d)(?=.[@$!%?&])(?!.(\d)\d*\1)(?!.(012|123|234|345|456|567|678|789|890|210|321|432|543|654|765|876|987))[A-Za-z\d@$!%?&]{8,10}$/))) {
            //    errorMsg += "* Password should not be contain consecutive numbers. \n";
            //    cnt++;
            //}

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
<body style="background-color: darkblue;">
    <form id="form1" runat="server">
        <asp:HiddenField ID="hdoldpass" runat="server" />
        <asp:HiddenField ID="hdnewpass" runat="server" />
        <asp:HiddenField ID="hdconfirmnewpass" runat="server" />
        <div id="container-fluid" style="width: 99%;">
            <div class="row">
                <div class="col-2"></div>
                <div class="col-8">
                    <div class="card shadow center1">
                        <div class="row mb-1">
                            <div class="col-md-8 text-center">
                                <table>
                                    <tr>
                                        <td>
                                           <a href="../home.aspx"> <asp:Image runat="server" ID="ImgDepartmentLogo" Height="50px" ImageUrl="" /></a>
                                        </td>
                                        <td style="padding-left: 5px;">
                                             <a href="../home.aspx"><asp:Label runat="server" ID="lblDeptName1" CssClass="deptName" Text="" Font-Size="18px"> </asp:Label>
                                            <br />
                                            
                                            <span style="font-size: 15px;float:left;" class="text-left">Version
                                        <asp:Label runat="server" ID="lblversion" Text=""></asp:Label></span></a>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="col-md-4 text-right">
                                <asp:Label runat="server" Text="Change Password" Font-Bold="true" Font-Size="X-Large"></asp:Label>
                            </div>
                            <div class="col-md-12 text-center mt-2">
                                <asp:Label runat="server" Font-Size="25px" ID="lblusermsg" Text="To ensure your account's security, please take a moment to update your password before continuing."></asp:Label>
                            </div>
                        </div>
                        <div class="row mt-2">
                            <div class="col-lg-5 p-3" style="font-size: 13px; padding-bottom: 10px; border-right: 1px solid #f9eaea;">
                                <h2 class="card-category font-weight-bold " style="text-align: left;">Please Note 
                                </h2>
                                <ol style="padding-left: 20px; font-size: 12pt; color: red; text-align: left;">
                                    <li>Password should not be contain consecutive numbers.</li>
                                    <li>Password length must be minimum 8 charecter and maximum 10 charecter .</li>
                                    <li>Password should contaion At least one special characters (!*#$%&()+,-/;<=>?@[\]^_{|}~).</li>
                                    <li>Password should not consist solely of numbers</li>
                                    <li>Password should not consist solely of letters.</li>
                                    <li>Password must include a combination of Uppercase and lowercase letters.  </li>
                                    <li>Password should not consist solely of special characters. </li>
                                    <li>All fields are manadatory.</li>
                                </ol>
                            </div>
                            <div class="col-7 p-3">
                                <div class="row">
                                    <div class="col-md-6 col-xl-12 p-3">

                                        <div class="mb-3">
                                            <label class="form-label">Enter Current Password</label>
                                            <asp:TextBox runat="server" ID="tbOldPwd" CssClass="form-control" placeholder="Enter Password" Style="width: 40%;" TextMode="Password" autocomplete="off" MaxLength="15"></asp:TextBox>
                                        </div>
                                        <div class="mb-3">
                                            <label class="form-label">Enter New Password</label>
                                            <asp:TextBox runat="server" ID="tbNewPwd" CssClass="form-control" placeholder="Enter New Password" Style="width: 40%;" MaxLength="15" TextMode="Password" autocomplete="off"></asp:TextBox>
                                        </div>
                                        <div class="mb-3">
                                            <label class="form-label">Confirm New Password</label>
                                            <asp:TextBox runat="server" ID="tbConfirmNewPwd" CssClass="form-control" placeholder="Confirm Password" Style="width: 40%;" MaxLength="15" TextMode="Password" autocomplete="off"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <asp:LinkButton ID="lbtnChangePwd" runat="server" CssClass="btn btn-success" OnClick="lbtnChangePwd_Click" OnClientClick="return setSHANew();">Change Password</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <div class="row" style="width: 100%;">
                            <div style="width: 100%; border-top: 1px solid #0169b7; font-size: 9pt; margin-top: 10px; padding-top: 5px; text-align: center;">

                                <strong>Contents Published ,Managed and Maintained </strong>
                                <asp:Label runat="server" ID="lbldeptname" Text=""></asp:Label>
                                <br />
                                <strong>Design & Developed by </strong>
                                <asp:Label runat="server" ID="lbldevelopedby" Text=""></asp:Label>


                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-2"></div>
            </div>
        </div>

    </form>
</body>
</html>
