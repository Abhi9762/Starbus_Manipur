
Imports System.Data
Imports System.Runtime.InteropServices
Imports Oracle.DataAccess.Client
Imports CaptchaDLL
Partial Class AgentRegistration
    Inherits System.Web.UI.Page
    Dim MyCommand As New OracleCommand
    Public _SecurityCheck As New eTCKTVal
    Dim msg As String = ""
    Dim comm As New CommonSMSnEmail
    Dim attempt As Integer = 0
    Dim BLLNEW As New BLLMTRENTRY
    Public Declare Function FindMimeFromData Lib "urlmon.dll" (ByVal pBC As IntPtr, <MarshalAs(UnmanagedType.LPWStr)> ByVal pwzUrl As String, <MarshalAs(UnmanagedType.LPArray, ArraySubType:=UnmanagedType.I1, SizeParamIndex:=3)> ByVal pBuffer As Byte(), ByVal cbSize As Integer, <MarshalAs(UnmanagedType.LPWStr)> ByVal pwzMimeProposed As String, ByVal dwMimeFlags As Integer, <MarshalAs(UnmanagedType.U4)> ByRef ppwzMimeOut As Integer, ByVal dwReserved As Integer) As Integer
    Private Sub AgentRegistration_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Session("headerText") = "Agent Registration"
            Session("headerTextMessage") = "About grievance testing text here About grievance testing About grievance testing text here About grievance testing text here 1"
            RefreshCaptcha()
            LoadStates()
            loadDistrict()
            loadCity()
            LoadProofTypes(ddlAddressProofType)
            LoadProofTypes(ddlIdProofType)
            loadRegistrtaionDateFeeDetails()
            Session("Statusfile") = Nothing
            Session("RegCopyfile") = Nothing
            Session("Addressprooffile") = Nothing
            Session("Idprooffile") = Nothing
            lblTC.Text = LoadTersnCndition()


            loadstation(ddlstation)
        End If
        'If Session("_RNDIDENTIFIERONLAG") Is Nothing Or Session("_RNDIDENTIFIERONLAG") = "" Then
        '    Session("_ErrorMsg") = ""
        '    Response.Redirect("errorpage.aspx")
        'End If
    End Sub
    Public Function LoadTersnCndition() As String
        MyCommand.Parameters.Clear()
        MyCommand.Parameters.Add("@StoredProcedure", Oracle.DataAccess.Client.OracleDbType.Varchar2, "p_get_AG_termscondition", ParameterDirection.Input)
        MyCommand.Parameters.Add("refcur", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output
        Dim MyTable As DataTable = BLLNEW.SelectAll(MyCommand)
        If MyTable.TableName = "Success" Then
            If MyTable.Rows.Count > 0 Then
                Return HttpUtility.HtmlDecode(MyTable.Rows(0)("TERMCONDITIONDTLS").ToString())
            End If
        End If
        Return ""
    End Function

#Region "Method"
    Public Sub RefreshCaptcha()
        tbcaptchacode.Text = ""
        Session("CaptchaImageText") = CaptchaImage.GenerateRandomCode(CaptchaType.AlphaNumeric, 6)
    End Sub

    Private Sub errormsg(ByVal errormsg As String)
        lblerrmsg.Text = errormsg
        mpError.Show()
    End Sub
    Private Sub successmsg(ByVal sucessmsg As String)
        lblsucessmsg.Text = sucessmsg
        mpconfirm.Show()
    End Sub
    Private Sub loadstation(ddlstn As DropDownList)
        Try
            point3.Visible = False
            point7.Visible = False
            rbtno.Visible = False
            rbtyes.Visible = False
            ddlstn.Items.Clear()
            Dim MyTable As DataTable
            MyCommand.Parameters.Clear()
            MyCommand.Parameters.Add("@StoredProcedure", Oracle.DataAccess.Client.OracleDbType.Varchar2, "AGENTCONFIGSTATIONS", ParameterDirection.Input)
            MyCommand.Parameters.Add("refcur", Oracle.DataAccess.Client.OracleDbType.RefCursor, ParameterDirection.Output)
            MyTable = BLLNEW.SelectAll(MyCommand)
            If MyTable.TableName = "Success" Then
                If MyTable.Rows.Count > 0 Then
                    ddlstn.DataSource = MyTable
                    ddlstn.DataTextField = "STATION_NAME_EN"
                    ddlstn.DataValueField = "STON_ID"
                    ddlstn.DataBind()
                    point3.Visible = True
                    point7.Visible = True
                    rbtno.Visible = True
                    rbtyes.Visible = True
                End If
            End If
            ddlstn.Items.Insert(0, "Select")
            ddlstn.Items(0).Value = "0"
            ddlstn.SelectedIndex = 0
        Catch ex As Exception
            ddlstn.Items.Insert(0, "Select")
            ddlstn.Items(0).Value = "0"
            ddlstn.SelectedIndex = 0
        End Try
    End Sub
    Public Sub loadRegistrtaionDateFeeDetails()
        Try
            Dim dt As DataTable
            MyCommand.Parameters.Clear()
            MyCommand.Parameters.Add("@StoredProcedure", Oracle.DataAccess.Client.OracleDbType.Char, "SPGETOnlAgentRegsdateFee", ParameterDirection.Input)
            MyCommand.Parameters.Add("refcur", Oracle.DataAccess.Client.OracleDbType.RefCursor, ParameterDirection.Output)
            dt = BLLNEW.SelectAll(MyCommand)
            If dt.TableName = "Success" Then
                If dt.Rows.Count > 0 Then

                End If
            End If
        Catch ex As Exception
        End Try
    End Sub
    Public Sub LoadStates()
        Try
            ddlstate.Items.Clear()
            Dim dt As DataTable
            MyCommand.Parameters.Clear()
            MyCommand.Parameters.Add("@StoredProcedure", Oracle.DataAccess.Client.OracleDbType.Char, "mStateGetList_nee", ParameterDirection.Input)
            MyCommand.Parameters.Add("spcountryCode", OracleDbType.Int32, 99, ParameterDirection.Input)
            MyCommand.Parameters.Add("refcur", Oracle.DataAccess.Client.OracleDbType.RefCursor, ParameterDirection.Output)
            dt = BLLNEW.SelectAll(MyCommand)
            If dt.Rows.Count > 0 Then
                If dt.TableName = "Success" Then
                    ddlstate.DataSource = dt
                    ddlstate.DataTextField = "stateName"
                    ddlstate.DataValueField = "stateCode"
                    ddlstate.DataBind()
                End If
            End If
            ddlstate.Items.Insert(0, "Select")
            ddlstate.Items(0).Value = "0"
            ddlstate.SelectedIndex = 0
        Catch ex As Exception
            ddlstate.Items.Insert(0, "Select")
            ddlstate.Items(0).Value = ""
            ddlstate.SelectedIndex = 0
        End Try

    End Sub
    Public Sub loadDistrict()
        Try
            ddlDistrict.Items.Clear()
            MyCommand.Parameters.Clear()
            MyCommand.Parameters.Add("@StoredProcedure", Oracle.DataAccess.Client.OracleDbType.Varchar2, "MDISTRICTGETLIST", ParameterDirection.Input)
            MyCommand.Parameters.Add("spStateCode", OracleDbType.Int32, ddlstate.SelectedValue, ParameterDirection.Input)
            MyCommand.Parameters.Add("refcur", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output
            Dim MyTable As DataTable = BLLNEW.SelectAll(MyCommand)
            If MyTable.TableName = "Success" Then
                If MyTable.Rows.Count > 0 Then
                    ddlDistrict.DataSource = MyTable
                    ddlDistrict.DataTextField = "DISTRICTNAME"
                    ddlDistrict.DataValueField = "DISTRICTCODE"
                    ddlDistrict.DataBind()
                End If
            End If
            ddlDistrict.Items.Insert(0, "Select")
            ddlDistrict.Items(0).Value = 0
            ddlDistrict.SelectedIndex = 0
        Catch ex As Exception
            ddlDistrict.Items.Insert(0, "-Select-")
            ddlDistrict.Items(0).Value = 0
            ddlDistrict.SelectedIndex = 0
        End Try
    End Sub
    Public Sub loadCity()
        Try
            ddlcity.Items.Clear()
            MyCommand.Parameters.Clear()
            MyCommand.Parameters.Add("@StoredProcedure", Oracle.DataAccess.Client.OracleDbType.Varchar2, "PROC_MST_GETCITY", ParameterDirection.Input)
            MyCommand.Parameters.Add("P_StateCode", Oracle.DataAccess.Client.OracleDbType.Int32, ddlstate.SelectedValue, ParameterDirection.Input)
            MyCommand.Parameters.Add("P_DistrictCode", Oracle.DataAccess.Client.OracleDbType.Int32, ddlDistrict.SelectedValue, ParameterDirection.Input)
            MyCommand.Parameters.Add("refcur", Oracle.DataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output
            Dim MyTable As DataTable = BLLNEW.SelectAll(MyCommand)
            If MyTable.TableName = "Success" Then
                If MyTable.Rows.Count > 0 Then
                    ddlcity.DataSource = MyTable
                    ddlcity.DataTextField = "Cityname"
                    ddlcity.DataValueField = "Cityid"
                    ddlcity.DataBind()
                End If
            End If
            ddlcity.Items.Insert(0, "Select")
            ddlcity.Items(0).Value = 0
            ddlcity.SelectedIndex = 0
        Catch ex As Exception
            ddlcity.Items.Insert(0, "-Select-")
            ddlcity.Items(0).Value = 0
            ddlcity.SelectedIndex = 0
        End Try
    End Sub
    Public Sub LoadProofTypes(ddlType As DropDownList)
        Try
            ddlType.Items.Clear()
            Dim dt As DataTable
            MyCommand.Parameters.Clear()
            MyCommand.Parameters.Add("@StoredProcedure", Oracle.DataAccess.Client.OracleDbType.Char, "mGetAddressProofTypes", ParameterDirection.Input)
            MyCommand.Parameters.Add("refcur", Oracle.DataAccess.Client.OracleDbType.RefCursor, ParameterDirection.Output)
            dt = BLLNEW.SelectAll(MyCommand)
            If dt.Rows.Count > 0 Then
                If dt.TableName = "Success" Then
                    ddlType.DataSource = dt
                    ddlType.DataTextField = "PROOFNAME"
                    ddlType.DataValueField = "PROOFID"
                    ddlType.DataBind()
                End If
            End If
            ddlType.Items.Insert(0, "Select")
            ddlType.Items(0).Value = "0"
            ddlType.SelectedIndex = 0
        Catch ex As Exception
            ddlType.Items.Insert(0, "Select")
            ddlType.Items(0).Value = "0"
            ddlType.SelectedIndex = 0
        End Try

    End Sub
    Public Function IsValidPdf(ByVal fileupload As FileUpload) As Boolean
        Dim _fileFormat As String = GetMimeDataOfFile(fileupload.PostedFile)
        If _fileFormat = "application/pdf" Then
            Return True
        Else
            errormsg("Invalid file (Not a PDF)")
            Return False
        End If
        Return True
    End Function
    Public Shared Function GetMimeDataOfFile(ByVal file As HttpPostedFile) As String
        Dim mimeout As IntPtr = Nothing
        Dim MaxContent As Integer = Convert.ToInt32(file.ContentLength)
        If MaxContent > 4096 Then
            MaxContent = 4096
        End If
        Dim buf As Byte() = New Byte(MaxContent - 1) {}
        file.InputStream.Read(buf, 0, MaxContent)
        Dim MimeSampleSize As Integer = 256
        Dim DefaultMimeType As String = "application/octet-stream"
        If buf Is Nothing Then
            Throw New ArgumentNullException("data", "Hey, data is null.")
        End If
        Dim mimeTypePointer As IntPtr = IntPtr.Zero
        Try
            FindMimeFromData(IntPtr.Zero, Nothing, buf, MimeSampleSize, Nothing, 0, mimeTypePointer, 0)
            Dim mime = Marshal.PtrToStringUni(mimeTypePointer)
            Return If(mime, DefaultMimeType)
        Catch e As AccessViolationException
            Return DefaultMimeType
        Finally

            If mimeTypePointer <> IntPtr.Zero Then
                Marshal.FreeCoTaskMem(mimeTypePointer)
            End If
        End Try
    End Function
    Public Function convertByteFilePDF(ByVal fuFileUpload As System.Web.UI.WebControls.FileUpload) As Byte()
        Dim intFileLength As Integer, byteData() As Byte
        If fuFileUpload.HasFile Then 'File Selected or Not
            'Check File Extention
            If checkFileExtentionPDF(fuFileUpload, ".pdf") = True Then
                intFileLength = fuFileUpload.PostedFile.ContentLength
                ReDim byteData(intFileLength)
                byteData = fuFileUpload.FileBytes
            End If
        Else
            intFileLength = fuFileUpload.PostedFile.ContentLength
            ReDim byteData(intFileLength)
            byteData = fuFileUpload.FileBytes
        End If
        Return byteData
    End Function
    Public Function checkFileExtentionPDF(ByVal fuFileUpload As System.Web.UI.WebControls.FileUpload, ByVal allowedExtention As String) As Boolean
        Dim fileExtensionOK As Boolean = False
        Dim fileExtension As String
        fileExtension = System.IO.Path.GetExtension(fuFileUpload.FileName).ToLower()
        Dim allowedExtensions As String() = {".pdf", ".PDF"}
        For i As Integer = 0 To allowedExtensions.Length - 1
            If fileExtension = allowedExtensions(i) Then
                fileExtensionOK = True
            End If
        Next
        Return fileExtensionOK
    End Function
    Public Function IsValidValues() As Boolean
        Try
            If chkTOC.Checked = False Then
                errormsg("Please Check Terms & Conditon")
                Return False
            End If
            If Not (Session("CaptchaImageText") IsNot Nothing AndAlso tbcaptchacode.Text.ToLower() = Session("CaptchaImageText").ToString.ToLower()) Then
                errormsg("Invalid Security Code(Shown in Image). Please Try Again")
                RefreshCaptcha()
                Return False
            End If

            Dim msgcount As Integer = 0
            Dim msg As String = "Enter Valid <br/>"

            If _SecurityCheck.IsValidString(txtname.Text.Trim, 1, txtname.MaxLength) = False Then
                msgcount = msgcount + 1
                msg = msg + msgcount.ToString + ". Agency/Agent Name.<br/>"
            End If
            If _SecurityCheck.IsValidString(txtContactName.Text.Trim, 1, txtContactName.MaxLength) = False Then
                msgcount = msgcount + 1
                msg = msg + msgcount.ToString + ". Contact Person Name.<br/>"
            End If
            If _SecurityCheck.IsValidInteger(txtmobileno.Text, 10, 10) = False Then
                msgcount = msgcount + 1
                msg = msg + msgcount.ToString + ". Mobile Number.<br/>"
            End If
            If _SecurityCheck.isValideMailID(txtemail.Text.Trim) = False Then
                msgcount = msgcount + 1
                lblerrmsg.Text = ". Email ID."
            End If
            If _SecurityCheck.IsValidInteger(ddlstate.SelectedValue, 1, 2) = False Then
                msgcount = msgcount + 1
                msg = msg + msgcount.ToString + ". State.<br/>"
            End If
            If _SecurityCheck.IsValidInteger(ddlDistrict.SelectedValue, 1, 5) = False Then
                msgcount = msgcount + 1
                msg = msg + msgcount.ToString + ". District.<br/>"
            End If
            If _SecurityCheck.IsValidInteger(ddlcity.SelectedValue, 1, 5) = False Then
                msgcount = msgcount + 1
                msg = msg + msgcount.ToString + ". City.<br/>"
            End If
            If _SecurityCheck.IsValidAddress(txtaddress.Text.Trim, 1, txtaddress.MaxLength) = False Then
                msgcount = msgcount + 1
                msg = msg + msgcount.ToString + ". Address.<br/>"
            End If
            If _SecurityCheck.IsValidInteger(txtPinCode.Text, 6, 6) = False Then
                msgcount = msgcount + 1
                msg = msg + msgcount.ToString + ". Pincode.<br/>"
            End If
            If _SecurityCheck.IsValidString(txtPanNo.Text.Trim, 1, txtname.MaxLength) = False Then
                msgcount = msgcount + 1
                msg = msg + msgcount.ToString + ". PAN No.<br/>"
            Else
                If isAlphanumericCharacters(txtPanNo.Text.ToUpper.Trim) = False Then
                    msgcount = msgcount + 1
                    msg = msg + msgcount.ToString + ". PAN No.<br/>"
                End If
            End If
            If ddlstatus.SelectedValue = "P" Then
                If Session("Statusfile") Is Nothing Then
                    msgcount = msgcount + 1
                    msg = msg + msgcount.ToString + ". Attach certified copy for Status.<br/>"
                End If
            End If
            If rbtexperience.SelectedValue = "Y" Then
                If _SecurityCheck.IsValidInteger(txtnoofyear.Text, 1, 2) = False Then
                    msgcount = msgcount + 1
                    msg = msg + msgcount.ToString + ". Number Of Years Experience.<br/>"
                End If
                If Session("RegCopyfile") Is Nothing Then
                    msgcount = msgcount + 1
                    msg = msg + msgcount.ToString + ". Copy of registration for proof.<br/>"
                End If
            End If
            If _SecurityCheck.IsValidInteger(ddlAddressProofType.SelectedValue, 1, 2) = False Then
                msgcount = msgcount + 1
                msg = msg + msgcount.ToString + ". Address Proof.<br/>"
            Else
                If ddlAddressProofType.SelectedValue = "0" Then
                    msgcount = msgcount + 1
                    msg = msg + msgcount.ToString + ". Address Proof.<br/>"
                Else
                    If Session("Addressprooffile") Is Nothing Then
                        msgcount = msgcount + 1
                        msg = msg + msgcount.ToString + ". Copy of Address for proof.<br/>"
                    End If
                End If
            End If
            If _SecurityCheck.IsValidInteger(ddlIdProofType.SelectedValue, 1, 2) = False Then
                msgcount = msgcount + 1
                msg = msg + msgcount.ToString + ". ID Proof.<br/>"
            Else
                If ddlIdProofType.SelectedValue = "0" Then
                    msgcount = msgcount + 1
                    msg = msg + msgcount.ToString + ". ID Proof.<br/>"
                Else
                    If Session("Idprooffile") Is Nothing Then
                        msgcount = msgcount + 1
                        msg = msg + msgcount.ToString + ". Copy of ID for proof.<br/>"
                    End If
                End If
            End If
            If rbtyes.Checked = True Then
                If ddlstation.SelectedValue = "0" Then
                    msgcount = msgcount + 1
                    msg = msg + msgcount.ToString + ". Station for Current Booking Facility.<br/>"
                End If
            End If

            If msgcount > 0 Then
                errormsg(msg)
                Return False
            End If
            Return True

        Catch ex As Exception
            errormsg("Please check values Errorcode admagentdetails05")
            Return False
        End Try
    End Function
    Public Sub Reset()
        txtname.Text = ""
        txtContactName.Text = ""
        txtmobileno.Text = ""
        txtemail.Text = ""
        ddlstate.SelectedValue = 0
        ddlDistrict.SelectedValue = 0
        ddlcity.SelectedValue = 0
        txtaddress.Text = ""
        txtPinCode.Text = ""
        txtPanNo.Text = ""
        ddlstatus.SelectedValue = "I"
        dvcopy.Visible = False
        lblPDF.Text = ""
        Session("Statusfile") = Nothing
        rbtexperience.SelectedValue = "Y"
        dvexperience.Visible = True
        txtnoofyear.Text = ""
        lblPDFRegCopy.Text = ""
        Session("RegCopyfile") = Nothing
        ddlAddressProofType.SelectedValue = 0
        lblPDFAddressproof.Text = ""
        Session("Addressprooffile") = Nothing
        ddlIdProofType.SelectedValue = 0
        lblPDFIdproof.Text = ""
        Session("Idprooffile") = Nothing
        RefreshCaptcha()
        chkTOC.Checked = False
    End Sub
    Private Sub saveDetails()
        Try
            Dim IPAddress As String = Request.ServerVariables("HTTP_X_FORWARDED_FOR")
            If IPAddress = "" Then
                IPAddress = Request.ServerVariables("REMOTE_ADDR")
            End If
            Dim name As String = txtname.Text.ToString
            Dim contactPerName As String = txtContactName.Text.ToString
            Dim MobileNo As String = txtmobileno.Text.ToString
            Dim emailID As String = txtemail.Text.ToString
            Dim state As Int32 = Convert.ToInt32(ddlstate.SelectedValue.ToString.Trim)
            Dim district As Int32 = Convert.ToInt32(ddlDistrict.SelectedValue.ToString.Trim)
            Dim city As Int32 = Convert.ToInt32(ddlcity.SelectedValue.ToString.Trim)
            Dim address As String = txtaddress.Text.ToString
            Dim pincode As String = txtPinCode.Text.ToString
            Dim panNo As String = txtPanNo.Text.ToString
            Dim legalstatus As String = ddlstatus.SelectedValue.ToString
            'Session("Statusfile") = Nothing
            Dim bookingexperience As String = rbtexperience.SelectedValue.ToString
            Dim noofyear As Int32
            If txtnoofyear.Text.ToString.Length = 0 Then
                noofyear = 0
            Else
                noofyear = Convert.ToInt32(txtnoofyear.Text.ToString)
            End If

            'Session("RegCopyfile") = Nothing
            Dim addressprooftype As Int32 = Convert.ToInt32(ddlAddressProofType.SelectedValue.ToString.Trim)
            'Session("Addressprooffile") = Nothing
            Dim idprooftype As Int32 = Convert.ToInt32(ddlIdProofType.SelectedValue.ToString.Trim)
            'Session("Idprooffile") = Nothing
            Dim Facility As String = ""
            Dim stoncode As Int32 = 0
            If rbtyes.Checked = True Then
                Facility = "B"
                stoncode = ddlstation.SelectedValue
            End If


            MyCommand.Parameters.Clear()
            MyCommand.Parameters.Add("@StoredProcedure", Oracle.DataAccess.Client.OracleDbType.Varchar2, "Pkg_AgentProcess.Proc_agrequestinsert", ParameterDirection.Input)
            MyCommand.Parameters.Add("P_NAME", OracleDbType.Varchar2, name, ParameterDirection.Input)
            MyCommand.Parameters.Add("P_CONTACTPERNAME", OracleDbType.Varchar2, contactPerName, ParameterDirection.Input)
            MyCommand.Parameters.Add("P_MOBILE", OracleDbType.Varchar2, MobileNo, ParameterDirection.Input)
            MyCommand.Parameters.Add("P_EMAIL", OracleDbType.Varchar2, emailID, ParameterDirection.Input)
            MyCommand.Parameters.Add("P_STATE", OracleDbType.Int32, state, ParameterDirection.Input)
            MyCommand.Parameters.Add("P_DISTRICT", OracleDbType.Int32, district, ParameterDirection.Input)
            MyCommand.Parameters.Add("P_CITY", OracleDbType.Int32, city, ParameterDirection.Input)
            MyCommand.Parameters.Add("P_ADDRESS", OracleDbType.Varchar2, address, ParameterDirection.Input)
            MyCommand.Parameters.Add("P_PINCODE", OracleDbType.Varchar2, pincode, ParameterDirection.Input)
            MyCommand.Parameters.Add("P_PANNO", OracleDbType.Varchar2, panNo, ParameterDirection.Input)
            MyCommand.Parameters.Add("P_LEGALSTATUS", OracleDbType.Varchar2, legalstatus, ParameterDirection.Input)
            MyCommand.Parameters.Add("P_STATUSDOC", OracleDbType.Blob, Session("Statusfile"), ParameterDirection.Input)
            MyCommand.Parameters.Add("P_EXPERIENCE", OracleDbType.Varchar2, bookingexperience, ParameterDirection.Input)
            MyCommand.Parameters.Add("P_NOOFYEAR", OracleDbType.Int32, noofyear, ParameterDirection.Input)
            MyCommand.Parameters.Add("P_REGDOC", OracleDbType.Blob, Session("RegCopyfile"), ParameterDirection.Input)
            MyCommand.Parameters.Add("P_ADDPROOFTYPE", OracleDbType.Int32, addressprooftype, ParameterDirection.Input)
            MyCommand.Parameters.Add("P_ADDPROOFDOC", OracleDbType.Blob, Session("Addressprooffile"), ParameterDirection.Input)
            MyCommand.Parameters.Add("P_IDDPROOFTYPE", OracleDbType.Int32, idprooftype, ParameterDirection.Input)
            MyCommand.Parameters.Add("P_IDPROOFDOC", OracleDbType.Blob, Session("Idprooffile"), ParameterDirection.Input)
            MyCommand.Parameters.Add("P_IPADDRESS", OracleDbType.Varchar2, IPAddress, ParameterDirection.Input)
            MyCommand.Parameters.Add("P_UPDATEDBY", OracleDbType.Varchar2, MobileNo, ParameterDirection.Input)
            MyCommand.Parameters.Add("P_FACILITY", OracleDbType.Varchar2, Facility, ParameterDirection.Input)
            MyCommand.Parameters.Add("P_STONCODE", OracleDbType.Int32, stoncode, ParameterDirection.Input)
            MyCommand.Parameters.Add("refcur", OracleDbType.RefCursor, ParameterDirection.Output)
            Dim dt As DataTable = BLLNEW.SelectAll(MyCommand)
            If dt.TableName = "Success" Then
                If dt.Rows.Count > 0 Then
                    Session("referenceNo") = dt.Rows(0)("referenceno").ToString
                    successmsg("Application details has been successfully Saved. Please note Reference No <b>" + Session("referenceNo").ToString() + "</b> for future reference")
                    comm.AgentRequest_SMS(Session("referenceNo"), MobileNo, 27)
                    comm.AgentRequest_Email(Session("referenceNo"), emailID)
                    Reset()
                Else
                    errormsg("Error occer while Saving Details.")
                End If
            Else
                errormsg("Error occer while Saving Details." + dt.TableName)
            End If
        Catch ex As Exception
            errormsg("Error occer while Saving Details." + ex.Message)
        End Try
    End Sub
#End Region
#Region "Event"
    Private Sub ddlstate_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlstate.SelectedIndexChanged
        If ddlstate.SelectedIndex > 0 Then
            loadDistrict()
        End If
    End Sub
    Private Sub ddlDistrict_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDistrict.SelectedIndexChanged
        If ddlDistrict.SelectedIndex > 0 Then
            loadCity()
        End If
    End Sub
    Private Sub ddlstatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlstatus.SelectedIndexChanged
        dvcopy.Visible = False
        If ddlstatus.SelectedValue = "I" Then
            dvcopy.Visible = False
        ElseIf ddlstatus.SelectedValue = "P" Then
            dvcopy.Visible = True
        End If
    End Sub
    Protected Sub btnUploadpdf_Click(sender As Object, e As EventArgs)
        If IsValidPdf(fudocfile) = True Then
            If fudocfile.HasFile Then
                Dim _fileFormat As String = GetMimeDataOfFile(fudocfile.PostedFile)
                If fudocfile.FileName.Length <= 50 Then
                    If Convert.ToInt32(fudocfile.FileBytes.Length) < 2097152 And fudocfile.FileName.Length > 2 Then

                        Dim _NewFileName = fudocfile.FileName
                        If _fileFormat = "application/pdf" Then
                            _NewFileName += ".pdf"
                        ElseIf _fileFormat = "application/octet-stream" Then
                            _NewFileName += ".pdf"
                        Else
                            errormsg("File format not allowed.")
                            Exit Sub
                        End If
                    Else
                        errormsg("Attach certified copy file less than 2 MB")
                        Exit Sub
                    End If
                End If
            End If
        Else
            errormsg("Invalid Attach certified copy file (Either not a pdf file or file size is more than 2 MB")
            Exit Sub
        End If
        Session("Statusfile") = convertByteFilePDF(fudocfile)
        lblPDF.Text = fudocfile.FileName
        lblPDF.Visible = True
    End Sub
    Private Sub rbtexperience_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rbtexperience.SelectedIndexChanged
        dvexperience.Visible = True
        If rbtexperience.SelectedValue = "Y" Then
            dvexperience.Visible = True
        Else
            dvexperience.Visible = False
        End If
    End Sub
    Protected Sub btnUploadpdfRegCopy_Click(sender As Object, e As EventArgs)
        If IsValidPdf(fudocfileRegCopy) = True Then
            If fudocfileRegCopy.HasFile Then
                Dim _fileFormat As String = GetMimeDataOfFile(fudocfileRegCopy.PostedFile)
                If fudocfileRegCopy.FileName.Length <= 50 Then
                    If Convert.ToInt32(fudocfileRegCopy.FileBytes.Length) < 2097152 And fudocfileRegCopy.FileName.Length > 2 Then

                        Dim _NewFileName = fudocfileRegCopy.FileName
                        If _fileFormat = "application/pdf" Then
                            _NewFileName += ".pdf"
                        ElseIf _fileFormat = "application/octet-stream" Then
                            _NewFileName += ".pdf"
                        Else
                            errormsg("File format not allowed.")
                            Exit Sub
                        End If
                    Else
                        errormsg("Copy of registration for proof file less than 2 MB")
                        Exit Sub
                    End If
                End If
            End If
        Else
            errormsg("Invalid Copy of registration for proof file (Either not a pdf file or file size is more than 2 MB")
            Exit Sub
        End If
        Session("RegCopyfile") = convertByteFilePDF(fudocfileRegCopy)
        lblPDFRegCopy.Text = fudocfileRegCopy.FileName
        lblPDFRegCopy.Visible = True
    End Sub
    Protected Sub btnUploadpdfAddressproof_Click(sender As Object, e As EventArgs)
        If IsValidPdf(fudocfileAddressproof) = True Then
            If fudocfileAddressproof.HasFile Then
                Dim _fileFormat As String = GetMimeDataOfFile(fudocfileAddressproof.PostedFile)
                If fudocfileAddressproof.FileName.Length <= 50 Then
                    If Convert.ToInt32(fudocfileAddressproof.FileBytes.Length) < 2097152 And fudocfileAddressproof.FileName.Length > 2 Then

                        Dim _NewFileName = fudocfileAddressproof.FileName
                        If _fileFormat = "application/pdf" Then
                            _NewFileName += ".pdf"
                        ElseIf _fileFormat = "application/octet-stream" Then
                            _NewFileName += ".pdf"
                        Else
                            errormsg("File format not allowed.")
                            Exit Sub
                        End If
                    Else
                        errormsg("Address Proof file less than 2 MB")
                        Exit Sub
                    End If
                End If
            End If
        Else
            errormsg("Invalid address proof file (Either not a pdf file or file size is more than 2 MB")
            Exit Sub
        End If
        Session("Addressprooffile") = convertByteFilePDF(fudocfileAddressproof)
        lblPDFAddressproof.Text = fudocfileAddressproof.FileName
        lblPDFAddressproof.Visible = True
    End Sub
    Protected Sub btnUploadpdfIDproof_Click(sender As Object, e As EventArgs)
        If IsValidPdf(fudocfileIdproof) = True Then
            If fudocfileIdproof.HasFile Then
                Dim _fileFormat As String = GetMimeDataOfFile(fudocfileIdproof.PostedFile)
                If fudocfileIdproof.FileName.Length <= 50 Then
                    If Convert.ToInt32(fudocfileIdproof.FileBytes.Length) < 2097152 And fudocfileIdproof.FileName.Length > 2 Then

                        Dim _NewFileName = fudocfileIdproof.FileName
                        If _fileFormat = "application/pdf" Then
                            _NewFileName += ".pdf"
                        ElseIf _fileFormat = "application/octet-stream" Then
                            _NewFileName += ".pdf"
                        Else
                            errormsg("File format not allowed.")
                            Exit Sub
                        End If
                    Else
                        errormsg("Id Proof file less than 2 MB")
                        Exit Sub
                    End If
                End If
            End If
        Else
            errormsg("Invalid id proof file (Either not a pdf file or file size is more than 2 MB")
            Exit Sub
        End If
        Session("Idprooffile") = convertByteFilePDF(fudocfileIdproof)
        lblPDFIdproof.Text = fudocfileIdproof.FileName
        lblPDFIdproof.Visible = True
    End Sub
    Private Sub lbtnRefresh_Click(sender As Object, e As EventArgs) Handles lbtnRefresh.Click
        RefreshCaptcha()
    End Sub
    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        Try
            If IsValidValues() = False Then
                Exit Sub
            End If
            lblConfirmation.Text = "Do you want Submit Details ?"
            mpConfirmation.Show()

        Catch ex As Exception
            errormsg("Error,while saving1")
        End Try
    End Sub
    Private Sub lbtnYesConfirmation_Click(sender As Object, e As EventArgs) Handles lbtnYesConfirmation.Click
        saveDetails()
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub


    Private Sub ddlProofType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAddressProofType.SelectedIndexChanged
        'divAadharBox.Visible = False
        'If ddlProofType.SelectedValue = 2 Then
        '    divAadharBox.Visible = True
        'End If
    End Sub



    Protected Sub rbtno_CheckedChanged(sender As Object, e As EventArgs)
        loadstation(ddlstation)
        dvstation.Visible = False
    End Sub
    Protected Sub rbtyes_CheckedChanged(sender As Object, e As EventArgs)
        loadstation(ddlstation)
        dvstation.Visible = True
    End Sub


#End Region
    Public Function getRandom() As String
        Dim random As New Random()
        Dim i As Integer
        Dim _random As String
        For i = 0 To 5
            _random += random.Next(0, 9).ToString
        Next
        Return "123456" '_random
    End Function
    Public Function isAlphanumericCharacters(ByVal valChk As String) As Boolean
        Dim iLoop As Integer
        Dim strChk As String
        Dim strKey As String
        iLoop = 1

        While (iLoop <= valChk.Length)
            strChk = valChk.Substring((iLoop - 1), 1)
            strKey = Asc(strChk)

            If Not (((strKey >= 65) AndAlso (strKey <= 90)) OrElse (((strKey >= 97) AndAlso (strKey <= 122)) OrElse (((strKey >= 48) AndAlso (strKey <= 57))))) Then
                Return False
                Exit Function
            End If
            iLoop += 1
        End While
        Return True
    End Function
End Class




