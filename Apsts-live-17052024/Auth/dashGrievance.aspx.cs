using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_dashGrievance : BasePage
{
    sbValidation _SecurityCheck = new sbValidation();
    NpgsqlCommand MyCommand;
    sbBLL bll = new sbBLL();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    classAlarm obj = new classAlarm();
    sbSecurity _security = new sbSecurity();
    protected void Page_Load(object sender, EventArgs e)
    {
       
        //if (_security.isSessionExist(Session["_RoleCode"]) == true)
        //{
        //    Session["_RoleCode"] = Session["_RoleCode"];
        //}
        //else
        //{
        //    Response.Redirect("../sessionTimeout.aspx");
        //}
        if (IsPostBack == false)
        {
            if (Session["_LOGINUSER"].ToString() == "T")
            {
                Session["_RoleCode"] = "0";
                pnlAction.Visible = false;
            }
            if (Session["_LOGINUSER"].ToString() == "C")
            {
                pnlAction.Visible = true;
                roleWiseAction();
            }

            if (Session["_grRefNo"].ToString() == "" || Session["_grRefNo"].ToString() == null)
            {

            }
            else
            {
                Session["_fileUpload"] = null;
                string refNo = Session["_grRefNo"].ToString();
                loadGrievanceDetail(refNo);
                GetGRefTrackDetails(refNo);
                ininall();
               
            }
            
        }
    }

    #region"Methods"
    private void ininall()
    {
        tr1.Visible = false;
        trattachfile.Visible = false;
        Session["fileUp1"] = null;
        lblfileUp1.Visible = false;

        trremark.Visible = false;
        btnassign.Visible = false;
        btnReset.Visible = false;

        txtRemark.Text = "";
        ddAcceptReject1.SelectedValue = "0";
    }
    private void roleWiseAction()
    {
        switch (Convert.ToInt32(Session["_RoleCode"].ToString()))
        {
            case 1: //    Admin
                {
                    ddAcceptReject1.Items.FindByValue("1").Enabled = false;
                    ddAcceptReject1.Items.FindByValue("5").Enabled = false;
                    Session["_RoleName_"] = "SYSADM";
                    break;
                }
            case 5: //    Depot
                {
                    ddAcceptReject1.Items.FindByValue("1").Enabled = false;
                    ddAcceptReject1.Items.FindByValue("4").Enabled = false;
                    Session["_RoleName_"] = "Depot Manager";
                    break;
                }
            case 6: //Helpdesk
                {
                    ddAcceptReject1.Items.FindByValue("4").Enabled = false;
                    break;
                }
            case 15: //CONTROL ROOM
                {
                    ddAcceptReject1.Items.FindByValue("4").Enabled = false;
                    ddAcceptReject1.Items.FindByValue("5").Enabled = false;
                    Session["_RoleName_"] = "CR";
                    break;
                }
            case 16: //MD
                {

                    break;
                }
            case 17: //MD
                {

                    break;
                }
            case 18: //MD
                {

                    break;
                }
            default:
                ddAcceptReject1.Items.FindByValue("4").Enabled = false;
                break;
        }
    }
    private void GetGRefTrackDetails(string refNo)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = obj.GetGRefTrackDetails(refNo);
            if (dt.TableName == "Success")
            {
                grTransactions.DataSource = dt;
                grTransactions.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }
    private void loadGrievanceDetail(string refNo)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = obj.getGrievanceDetail(refNo);
            if (dt.TableName == "Success")
            {
                string refno = dt.Rows[0]["g_refno"].ToString();
                string catname = dt.Rows[0]["category_name"].ToString();
                string subcatname = dt.Rows[0]["sub_categoryname"].ToString();
                string remark = dt.Rows[0]["g_remark"].ToString();
                string datetime = dt.Rows[0]["g_datetime"].ToString();
                string bus_no = dt.Rows[0]["bus_no"].ToString();
                string ticketno = dt.Rows[0]["ticket_no"].ToString();
                // Dim  As Byte() pic1 = CType(dt.Rows(0)("gpic1"), Byte())
                // Dim  As Byte() pic2 = CType(dt.Rows(0)("gpic2"), Byte())
                byte[] pic1 = (byte[])dt.Rows[0]["gpic_1"];
                byte[] pic2 = (byte[])dt.Rows[0]["gpic_2"];
                lblRefNo.Text = refno;
                lblCategory_subCategory.Text = catname + " - " + subcatname;
                lblGrievanceRemark.Text = remark;
                lblBusNo.Text = bus_no;
                lblTicketNo.Text = ticketno;
                lblDateTime.Text = datetime;
                lblLatt.Text = dt.Rows[0]["lat"].ToString();
                lblLongg.Text = dt.Rows[0]["long"].ToString();
                // img1.ImageUrl = GetImage(pic1)
                // img2.ImageUrl = GetImage(pic2)

                img1.ImageUrl = GetImage(pic1);
                img2.ImageUrl = GetImage(pic2);

                lblReportedByName.Text = dt.Rows[0]["user_name"].ToString();
                lblReportedByMobile.Text = dt.Rows[0]["g_byuser"].ToString();
            }
        }
        catch (Exception ex)
        {


        }
    }
    private void loaddepot(int ofc_lvl)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            DataTable dt = new DataTable();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_office_details");
            MyCommand.Parameters.AddWithValue("p_ofclevelid", ofc_lvl);
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                ddDepot.DataSource = dt;
                ddDepot.DataTextField = "officename";
                ddDepot.DataValueField = "officeid_";
                ddDepot.DataBind();
            }
            ddDepot.Items.Insert(0, "Select");
            ddDepot.Items[0].Value = "0";
            ddDepot.SelectedIndex = 0;
        }
        catch (Exception ex)
        {

        }
    }
    public string GetImage(object img)
    {
        try
        {
            return "data:image/jpg;base64," + Convert.ToBase64String((byte[])img);
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    private void Errormsg(string msg)
    {
        string popup = _popup.modalPopupSmall("D", "Please Check", msg, "Close");
        Response.Write(popup);
    }
    private bool savedetails(string refNo)
    {
        try
        {
            byte[] bytes = (byte[])Session["_fileUpload"];
            MyCommand = new NpgsqlCommand();
            DataTable dt = new DataTable();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "traveller_grievance.SP_INSERT_GRI_TRANS_DTL");
            MyCommand.Parameters.AddWithValue("p_refno", refNo);
            MyCommand.Parameters.AddWithValue("p_updatedby", Session["_RoleName_"].ToString());
            MyCommand.Parameters.AddWithValue("p_remark", txtRemark.Text);
            if (ddAcceptReject1.SelectedValue == "2")
            {
                MyCommand.Parameters.AddWithValue("p_assignto", "0");
                MyCommand.Parameters.AddWithValue("p_status", "X");
                MyCommand.Parameters.AddWithValue("p_attachfile", (object)bytes ?? DBNull.Value);
            }
            if (ddAcceptReject1.SelectedValue == "1")
            {
                MyCommand.Parameters.AddWithValue("p_assignto", ddDepot.SelectedValue);
                MyCommand.Parameters.AddWithValue("p_status", "A");
                MyCommand.Parameters.AddWithValue("p_attachfile", (object)bytes ?? DBNull.Value);
            }
            if (ddAcceptReject1.SelectedValue == "3")
            {
                MyCommand.Parameters.AddWithValue("p_assignto","0");
                MyCommand.Parameters.AddWithValue("p_status", "D");
                MyCommand.Parameters.AddWithValue("p_attachfile", (object)bytes ?? DBNull.Value);
            }
            if (ddAcceptReject1.SelectedValue == "4")
            {
                MyCommand.Parameters.AddWithValue("p_assignto", "0");
                MyCommand.Parameters.AddWithValue("p_status", "I");
                MyCommand.Parameters.AddWithValue("p_attachfile", (object)bytes ?? DBNull.Value);
            }
            if (ddAcceptReject1.SelectedValue == "5")
            {
                MyCommand.Parameters.AddWithValue("p_assignto", "0");
                MyCommand.Parameters.AddWithValue("p_status", "R");
                MyCommand.Parameters.AddWithValue("p_attachfile", (object)bytes ?? DBNull.Value);
            }
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                
                return true;
            }
            else
            {
                return false;
            }
            
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    private void Successmsg(string msg)
    {
        string popup = _popup.modalPopupSmall("S", "Success", msg, "Close");
        Response.Write(popup);
    }

    public static string GetMimeDataOfFile(HttpPostedFile file)
    {
        IntPtr mimeout = default(IntPtr);
        int MaxContent = Convert.ToInt32(file.ContentLength);
        if (MaxContent > 4096)
        {
            MaxContent = 4096;
        }

        byte[] buf = new byte[MaxContent - 1 + 1];
        file.InputStream.Read(buf, 0, MaxContent);
        int MimeSampleSize = 256;


        string mimeType = System.Web.MimeMapping.GetMimeMapping(file.FileName);
        return mimeType;
    }
    private bool CheckFileExtention(FileUpload FileInvoice)
    {
        try
        {
            bool fileExtensionOK = false;
            string fileExtension;
            fileExtension = System.IO.Path.GetExtension(FileInvoice.FileName).ToLower();
            string[] allowedExtensions = new[] { ".jpg", ".png", ".jpeg" };
            for (int i = 0; i <= allowedExtensions.Length - 1; i++)
            {
                if (fileExtension == allowedExtensions[i])
                {
                    fileExtensionOK = true;
                }
            }
            return fileExtensionOK;
        }
        catch (Exception ex)
        {
            Errormsg("Something went wrong with image (allow only .jpg, .png, .jpeg image). ");
            return default(Boolean);
        }
    }
    public byte[] convertByteFile(System.Web.UI.WebControls.FileUpload fuFileUpload)
    {
        int intFileLength;
        byte[] byteData = null;
        if (fuFileUpload.HasFile)
        {

            // Check File Extention
            if (CheckFileExtention(fuFileUpload) == true)
            {
                intFileLength = fuFileUpload.PostedFile.ContentLength;
                byteData = new byte[intFileLength + 1];
                byteData = fuFileUpload.FileBytes;
            }
        }
        else
        {
            intFileLength = fuFileUpload.PostedFile.ContentLength;
            byteData = new byte[intFileLength + 1];
            byteData = fuFileUpload.FileBytes;
        }
        return byteData;
    }
    #endregion

    #region"Events"
    protected void ddAcceptReject1_SelectedIndexChanged(object sender, EventArgs e)
    {

        btnassign.Visible = true;
        btnReset.Visible = true;
        trattachfile.Visible = false;
        tr1.Visible = false;
        trremark.Visible = false;
        trbutton.Visible = false;
        trattachfile.Visible = false;

        // pnlgAssign.Visible = True
        if (ddAcceptReject1.SelectedValue == "1")
        {
            lblRemark.Text = "Description";
            tr1.Visible = true;
            trremark.Visible = true;
            trbutton.Visible = true;
            trattachfile.Visible = false;
            Session["fileUp1"] = null;
            lblfileUp1.Visible = false;
            loaddepot(30);
            btnassign.Text = "Assign";
        }
        else if (ddAcceptReject1.SelectedValue == "2")
        {
            lblRemark.Text = "Reason";
            tr1.Visible = false;
            trremark.Visible = true;
            trattachfile.Visible = false;
            Session["fileUp1"] = null;
            lblfileUp1.Visible = false;
            trbutton.Visible = true;
            btnassign.Text = "Reject";
        }
        else if (ddAcceptReject1.SelectedValue == "3")
        {
            Session["fileUp1"] = null;
            lblfileUp1.Visible = false;
            lblRemark.Text = "Dispose Remark";
            tr1.Visible = false;
            trremark.Visible = true;
            trbutton.Visible = true;
            trattachfile.Visible = true;
            btnassign.Text = "Dispose";
        }
        else if (ddAcceptReject1.SelectedValue == "4")
        {
            Session["fileUp1"] = null;
            lblfileUp1.Visible = false;
            lblRemark.Text = "Instrucation";
            tr1.Visible = false;
            trremark.Visible = true;
            trbutton.Visible = true;
            trattachfile.Visible = false;
            btnassign.Text = "Submit";
        }
        else if (ddAcceptReject1.SelectedValue == "5")
        {
            Session["fileUp1"] = null;
            lblfileUp1.Visible = false;
            lblRemark.Text = "Instrucation";
            tr1.Visible = false;
            trremark.Visible = true;
            trbutton.Visible = true;
            trattachfile.Visible = false;
            btnassign.Text = "Submit";
        }
        else
        { }

    }
    protected void btnassign_Click(object sender, EventArgs e)
    {
        string refNo = Session["_grRefNo"].ToString();
        if (ddAcceptReject1.SelectedValue == "0")
        {
            Errormsg("Invalid Action Select");
            return;
        }
        if (ddAcceptReject1.SelectedValue == "1")
        {
            if (ddDepot.SelectedValue == "0")
            {
                Errormsg("Invalid Depot Select");
                return;
            }
        }
        if (txtRemark.Text == "")
        {
            Errormsg("Please enter the Remark !");
            return;
        }
        if (ddAcceptReject1.SelectedValue == "2")
        {
            if (savedetails(refNo) == true)
            {
                Successmsg("Complaints Rejected Successfully ");
                ininall();
            }
        }
        if (ddAcceptReject1.SelectedValue == "1")
        {
            if (savedetails(refNo) == true)
            {
                Successmsg("Complaints Assigned Successfully");
                ininall();
            }
        }
        if (ddAcceptReject1.SelectedValue == "3")
        {
            if (savedetails(refNo) == true)
            {
                Successmsg("Complaints Disposed Successfully");
                ininall();
            }
        }
        if (ddAcceptReject1.SelectedValue == "4")
        {
            if (savedetails(refNo) == true)
            {
                Successmsg("Instruction Saved Successfully");
                ininall();
            }
        }
        if (ddAcceptReject1.SelectedValue == "5")
        {
            if (savedetails(refNo) == true)
            {
                Successmsg("Complain Return Successfully");
                ininall();
            }
        }


        loadGrievanceDetail(refNo);
        GetGRefTrackDetails(refNo);
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {

    }
    #endregion





    protected void btnFile_Click(object sender, EventArgs e)
    {
        if (!FileUpload1.HasFile)
        {
            Errormsg("Please select invoice first");
            return;
        }
        string _fileFormat = GetMimeDataOfFile(FileUpload1.PostedFile);
        if ((_fileFormat == "image/png") || (_fileFormat == "image/jpeg"))
        {
        }
        else
        {
            Errormsg("File must png, jpg, jpeg type");
            return;
        }
        if (!CheckFileExtention(FileUpload1))
        {
            Errormsg("File must be png, jpg, jpeg type");

            return;
        }

        decimal size = Math.Round((Convert.ToDecimal(FileUpload1.PostedFile.ContentLength) / Convert.ToDecimal(1024)), 2);
        if (size > 1024)
        {
            Errormsg("File size must be less than 1MB");
            return;
        }
        byte[] PhotoImage = null;
        PhotoImage = convertByteFile(FileUpload1);
      
        Session["_fileUpload"] = FileUpload1.FileBytes;
    }
}