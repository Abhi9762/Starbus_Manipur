<%@ Page Language="C#" AutoEventWireup="true" CodeFile="changepass.aspx.cs" Inherits="Auth_changepass" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="Start your development with a Dashboard for Bootstrap 4.">
    <meta name="author" content="Creative Tim">
    <title>StarBus ver 4.0</title>
    <link rel="stylesheet" href="../assets/vendor/@fortawesome/fontawesome-free/css/all.min.css" type="text/css">
    <link rel="stylesheet" href="../assets/css/argon.css?v=1.2.0" type="text/css">
    <script src="../js/sha1.js"></script>
    <script type="text/javascript">
        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };
    </script>
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
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="hdoldpass" runat="server" />
        <asp:HiddenField ID="hdnewpass" runat="server" />
        <asp:HiddenField ID="hdconfirmnewpass" runat="server" />
        <div class="container-fluid">
            <div class="empty">
                <%--<div class="empty-header">Change Password</div>--%>
                <div class="row mb-1">
                    <div class="col-md-12 text-center">
                        <asp:Label runat="server" Font-Size="25px" ID="lblusermsg" Text="To ensure your account's security, please take a moment to update your password before continuing."></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6 " style="min-height: 350px;">
                        <div class="card card-dash" style="min-height: 350px;">

                            <div class="card-body">
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
                        </div>
                    </div>
                    <div class="col-md-6" style="min-height: 350px;">
                        <div class="card card-dash" style="min-height: 350px;">

                            <div class="card-body" style="text-align: left;">

                                <div class="row">
                                    <div class="col-md-6 col-xl-12">

                                        <div class="mb-3">
                                            <label class="form-label">Enter Current Password</label>
                                            <asp:TextBox runat="server" ID="tbOldPwd" CssClass="form-control" placeholder="Enter Password" TextMode="Password" autocomplete="off" MaxLength="15"></asp:TextBox>
                                        </div>
                                        <div class="mb-3">
                                            <label class="form-label">Enter New Password</label>
                                            <asp:TextBox runat="server" ID="tbNewPwd" CssClass="form-control" placeholder="Enter New Password" MaxLength="15" TextMode="Password" autocomplete="off"></asp:TextBox>
                                        </div>
                                        <div class="mb-3">
                                            <label class="form-label">Confirm New Password</label>
                                            <asp:TextBox runat="server" ID="tbConfirmNewPwd" CssClass="form-control" placeholder="Confirm Password" MaxLength="15" TextMode="Password" autocomplete="off"></asp:TextBox>
                                        </div>


                                        <div class="form-group">
                                            <asp:LinkButton ID="lbtnChangePwd" runat="server" CssClass="btn btn-success" OnClick="lbtnChangePwd_Click" OnClientClick="return setSHANew();">Change Password</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>


                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
