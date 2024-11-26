<%@ Page Title="" Language="C#" MasterPageFile="~/mainMasterPage.master" AutoEventWireup="true" CodeFile="photoGallery.aspx.cs" Inherits="photoGallary" EnableEventValidation="false" %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
    <style>
        .hr{

            margin-top:-10px;
            margin-bottom:10px;
        }
        .nav-pills-background {
            background: transparent linear-gradient(0deg, #1A2300 0%, #565D04 100%) 0% 0% no-repeat padding-box;
            height: 55px;
            padding-top: 10px;
            border-radius: 25px 25px 0px 0px;
            opacity: 1;
        }

        .rounded1 {
            border-radius: 5px 5px 40px 40px;
            box-shadow: 0 3px 10px rgb(0 0 0 / 0.2);
        }

        .pp4 {
            color: #fff;
            padding: 4px;
            /*border-radius: 0px 0px 600px 600px;*/
            font-size: 18px;
            text-align: center;
            background: #051643;
            margin-top: -36px;
            position: relative;
            line-height: 30px;
        }

        .closeBtn {
            color: rgba(255, 255, 255, 0.87);
            font-size: 25px;
            position: absolute;
            top: 0;
            right: 0;
            margin: 20px;
            cursor: pointer;
            transition: 0.2s ease-in-out;
        }

            .closeBtn:hover {
                color: rgb(255, 255, 255);
            }



        .myImg {
            border-radius: 5px;
            cursor: pointer;
            transition: 0.3s;
        }

            .myImg:hover {
                opacity: 0.7;
            }

        .img {
        }

        /* The Modal (background) */
        .modal {
            display: none; /* Hidden by default */
            position: fixed; /* Stay in place */
            z-index: 1; /* Sit on top */
            padding-top: 100px; /* Location of the box */
            left: 0;
            top: 0;
            width: 100%; /* Full width */
            height: 100%; /* Full height */
            overflow: auto; /* Enable scroll if needed */
            background-color: rgb(0,0,0); /* Fallback color */
            background-color: rgba(0,0,0,0.9); /* Black w/ opacity */
        }

        /* Modal Content (image) */
        .modal-content {
            margin: auto;
            display: block;
            width: 80%;
            max-width: 700px;
        }

        /* Caption of Modal Image */
        .caption {
            margin: auto;
            display: block;
            width: 80%;
            max-width: 700px;
            text-align: center;
            color: #ccc;
            padding: 10px 0;
            height: 150px;
        }

        /* Add Animation */
        .modal-content caption {
            -webkit-animation-name: zoom;
            -webkit-animation-duration: 0.6s;
            animation-name: zoom;
            animation-duration: 0.6s;
        }

        @-webkit-keyframes zoom {
            from {
                -webkit-transform: scale(0);
            }

            to {
                -webkit-transform: scale(1);
            }
        }

        @keyframes zoom {
            from {
                transform: scale(0);
            }

            to {
                transform: scale(1);
            }
        }

        /* The Close Button */
        .close {
            position: absolute;
            top: 15px;
            right: 35px;
            color: #f1f1f1;
            font-size: 40px;
            font-weight: bold;
            transition: 0.3s;
        }

            .close:hover,
            .close:focus {
                color: #bbb;
                text-decoration: none;
                cursor: pointer;
            }

        /* 100% Image Width on Smaller Screens */
        @media only screen and (max-width: 700px) {
            .modal-content {
                width: 100%;
            }
        }

        
    </style>

   <%-- <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>--%>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  
        <div class="container-fluid">
            <div class="row row-centered pos g-0">

                <div class="col-lg-12 col-xs-12 col-centered " >

             
                  

                  
                        <asp:Panel runat="server" ID="pnlNoRecord" Visible="false" CssClass="text-center" Width="100%" Style="padding: 200px">
                            <p class="text-center" style="font-size: 30px; font-weight: bold; color: #e3e3e3;">
                                Photograph will be available very soon. Please keep visiting this section                           
                            </p>
                        </asp:Panel>
                        <asp:Panel ID="pnlMain" runat="server">


                            <div class="row row-centered pos g-0">

                                <div class="col-lg-3" style="border-right: inset; padding: 20px; min-height: 90vh;">
                                    <h4 style="text-align: center; color: #EE6F2D;">SELECT ALBUM <label class="small" style="color:#EE6F2D; text-align:center">(Please click on album to view photo gallery)</label></h4>
                                   
                                    <hr class="hr"/>

                                    <asp:GridView ID="gvCategoryList" OnRowDataBound="gvCategoryList_RowDataBound" OnSelectedIndexChanged="gvCategoryList_SelectedIndexChanged" ShowHeader="false" runat="server" AutoGenerateColumns="False" GridLines="None"
                                        Visible="true" CssClass="table table-bordered" DataKeyNames="categoryId" RowStyle-ForeColor="Black" Font-Size="12pt">
                                        <Columns>

                                            <asp:BoundField HeaderStyle-Font-Bold="true" DataField="categoryName" HeaderText="Category" ItemStyle-Font-Size="12pt" />

                                        </Columns>

                                    </asp:GridView>



                                </div>
                                <div class="col-lg-9 p-4" style="padding: 20px; min-height: 90vh;">
                                    <center>
                                    <h4 style="text-align: center; color: #EE6F2D ;">ALBUM :
                                        <asp:Label ID="lblAlbumName" runat="server"></asp:Label> <br /> <label class="small" style="color:#EE6F2D; text-align:center">(Please click on photgraph to view Enlarged photgraph)</label></h4>
                                  
                                    <hr class="hr" />
                                        </center>
                                    <div class="row">
                                        <asp:Repeater ID="rptPhotos" runat="server">
                                            <ItemTemplate>
                                                <div class="col-lg-3">


                                                    <img src='<%#Eval("photoURL")%>' class="rounded1 d-block w-100  myImg" id='myImg<%#Eval("photoId")%>' alt='<%#Eval("photo_name")%>' style="height: 80%" />

                                                    <asp:HiddenField runat="server" ID="hdphotourl" Value='<%#Eval("photoURL")%>' />
                                                    <div class="pp4">
                                                        <div><%#Eval("photo_name")%></div>
                                                    </div>
                                                </div>

                                                <div id='myModal<%#Eval("photoId")%>' class="modal">
                                                    <span class="close" id='closeBtn<%#Eval("photoId")%>'>×</span>

                                                    <img src='<%#Eval("photoURL")%>' id='img<%#Eval("photoId")%>' alt='<%#Eval("photo_name")%>' class="modal-content" />
                                                    <div id='caption<%#Eval("photoId")%>' class="caption"></div>
                                                </div>

                                                <script>
                                                    // Get the modal
                                                    var modal = document.getElementById('myModal<%#Eval("photoId")%>');

                                                    // Get the image and insert it inside the modal - use its "alt" text as a caption
                                                    var img = document.getElementById('myImg<%#Eval("photoId")%>');
                                                    var modalImg = document.getElementById('img<%#Eval("photoId")%>');
                                                    var captionText = document.getElementById('caption<%#Eval("photoId")%>');
                                                    img.onclick = function () {
                                                        modal.style.display = "block";
                                                        modalImg.src = this.src;
                                                        modalImg.alt = this.alt;
                                                        captionText.innerHTML = this.alt;
                                                    }

                                                    // Get the <span> element that closes the modal
                                                    var span = document.getElementById('closeBtn<%#Eval("photoId")%>');

                                                // When the user clicks on <span> (x), close the modal
                                                span.onclick = function () {
                                                    modal.style.display = "none";
                                                }
                                                </script>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                    <%-- </div>--%>
                                </div>
                            </div>
                        </asp:Panel>
                   

                </div>
            </div>

        </div>
    


</asp:Content>

