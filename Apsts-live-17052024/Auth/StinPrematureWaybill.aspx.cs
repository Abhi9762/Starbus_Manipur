using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_StinPrematureWaybill : System.Web.UI.Page
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    DataTable dt1 = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    string current_date = DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("yyyy");
    [DllImport("urlmon.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = false)]
    static extern int FindMimeFromData(IntPtr pBC,
    [MarshalAs(UnmanagedType.LPWStr)] string pwzUrl,
    [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1, SizeParamIndex = 3)] byte[] pBuffer,
    int cbSize,
    [MarshalAs(UnmanagedType.LPWStr)] string pwzMimeProposed,
    int dwMimeFlags, out IntPtr ppwzMimeOut, int dwReserved);
    protected void Page_Load(object sender, EventArgs e)
    {
        checkForSecurity();
        if (!IsPostBack)
        {
            lblnodata.Text = "Please Enter Waybill No. For Closure";
            loadClosuretype(ddlclosure);
        }
    }

    #region "Common Method"
    private void checkForSecurity()
    {
        //if (Session.IsNewSession == false || Request.ServerVariables["HTTP_REFERER"].Length < 1)
        //{
        //    Response.Redirect("../Errorpage.aspx");
        //}

        if (_security.isSessionExist(Session["_UserCode"]) == true)
        {
            Session["_UserCode"] = Session["_UserCode"];
        }
        else
        {
            Response.Redirect("../sessionTimeout.aspx");
        }

        if (Session["_RoleCode"].ToString() == "99")
        {
            if (_security.isSessionExist(Session["_RNDIDENTIFIEREMPDASH"]) == true)
            {
                Session["_RNDIDENTIFIEREMPDASH"] = Session["_RNDIDENTIFIEREMPDASH"];
            }
            else
            {
                Response.Redirect("sessionTimeout.aspx");
            }
        }
        if (Session["_RoleCode"].ToString() == "8")
        {
            if (_security.isSessionExist(Session["_RNDIDENTIFIERSTINCHARGE"]) == true)
            {
                Session["_RNDIDENTIFIERSTINCHARGE"] = Session["_RNDIDENTIFIERSTINCHARGE"];
            }
            else
            {
                Response.Redirect("../sessionTimeout.aspx");
            }
        }
        if (Session["_RoleCode"].ToString() == "5")
        {
            if (_security.isSessionExist(Session["_RNDIDENTIFIERDOPT"]) == true)
            {
                Session["_RNDIDENTIFIERDOPT"] = Session["_RNDIDENTIFIERDOPT"];
            }
            else
            {
                Response.Redirect("sessionTimeout.aspx");
            }
        }




        if (_security.checkvalidation() == false)
        {
            Response.Redirect("../Errorpage.aspx");
        }
        if (_security.CheckOldUserLogin(Session["_UserCode"].ToString()) == false)
        {
            Response.Redirect("../Errorpage.aspx");
        }
    }
    private void Errormsg(string msg)
    {

        string popup = _popup.modalPopupSmall("W", "Please Check", msg, "Close");
        Response.Write(popup);
    }
    private void Successmsg(string msg)
    {
        string popup = _popup.modalPopupSmall("S", "Success", msg, "OK");
        Response.Write(popup);
    }
    #endregion

    #region "Methods"
    private void loadClosuretype(DropDownList ddl)
    {
        try
        {
            ddl.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();

            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_waybill_closure_type");
            MyCommand.Parameters.AddWithValue("p_status_code", "P");
            dt = bll.SelectAll(MyCommand);
            dt1 = dt;
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddl.DataSource = dt;
                    ddl.DataTextField = "sub_statusname";
                    ddl.DataValueField = "sub_statuscode";
                    ddl.DataBind();
                }
            }
            ddl.Items.Insert(0, "Select");
            ddl.Items[0].Value = "0-N";
            ddl.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddl.Items.Insert(0, "Select");
            ddl.Items[0].Value = "0-N";
            ddl.SelectedIndex = 0;
        }
    }
    private void loadwaybillNo(string waybillno)
    {
        pnldata.Visible = false;
        pnlNodata.Visible = true;
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_premature_waybill_detail");
        MyCommand.Parameters.AddWithValue("p_waybillno", waybillno);
        MyCommand.Parameters.AddWithValue("p_officeid", Session["_LDepotCode"].ToString());
        dt = bll.SelectAll(MyCommand);

        if (dt.TableName == "Success")
        {
            if (dt.Rows.Count > 0)
            {
                lblwaybillno.Text = dt.Rows[0]["dutyrefno"].ToString();
                lblservicename.Text = dt.Rows[0]["service_name"].ToString();
                lblroute.Text = dt.Rows[0]["route"].ToString();
                lbldriver.Text = dt.Rows[0]["driver"].ToString();
                lbldeparturedate.Text = dt.Rows[0]["departure_time"].ToString();
                lblconductor.Text = dt.Rows[0]["conductor"].ToString();
                lblbusno.Text = dt.Rows[0]["busno"].ToString();
                lbletmserialno.Text = dt.Rows[0]["etmserialno"].ToString();

                pnldata.Visible = true;
                pnlNodata.Visible = false;
            }
            else
            {
                lblnodata.Text = "Invalid Waybill Details.";
                pnldata.Visible = false;
                pnlNodata.Visible = true;
            }
        }
    }
    public static string GetMimeDataOfFile(FileUpload file1)
    {
        HttpPostedFile file = file1.PostedFile;
        IntPtr mimeout;

        int MaxContent = (int)file.ContentLength;
        if (MaxContent > 4096) MaxContent = 4096;

        byte[] buf = new byte[MaxContent];
        file.InputStream.Read(buf, 0, MaxContent);
        int result = FindMimeFromData(IntPtr.Zero, file.FileName, buf, MaxContent, null, 0, out mimeout, 0);

        if (result != 0)
        {
            Marshal.FreeCoTaskMem(mimeout);
            return "";
        }

        string mime = Marshal.PtrToStringUni(mimeout);
        Marshal.FreeCoTaskMem(mimeout);

        return mime.ToLower();
    }
    private bool CheckFileExtentionWeb(FileUpload fuimage2)
    {
        try
        {
            bool fileExtensionOK = false;
            string fileExtension;
            fileExtension = System.IO.Path.GetExtension(fuimage2.FileName).ToLower();
            string[] allowedExtensions = new[] { ".db" };
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
            return default(Boolean);
        }
    }
    private void submitPrematureClosure()
    {
        string UpdatedBy = Session["_UserCode"].ToString();
        byte[] documentfile = (byte[])Session["sql"];
        string[] param = ddlclosure.SelectedValue.Split('-');
        string ddlclosurevalue = param[0].ToString().Trim();
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "etm.f_save_premature_waybill_closure");
        MyCommand.Parameters.AddWithValue("p_waybill_no", txtwaybillNo.Text);
        MyCommand.Parameters.AddWithValue("p_closure_status_code", ddlclosurevalue);
        MyCommand.Parameters.AddWithValue("p_closure_remark", txtremark.Text);
        MyCommand.Parameters.AddWithValue("p_update_by", UpdatedBy);
        MyCommand.Parameters.AddWithValue("p_db_file", (object)documentfile ?? DBNull.Value);
        dt = bll.SelectAll(MyCommand);
        if (dt.TableName == "Success")
        {
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["status"].ToString() == "EXCEPTION")
                {
                    Errormsg("Something Went Wrong Please Try Again.");
                }
                else
                {
                    Successmsg(dt.Rows[0]["status"].ToString());
                    resetcontrol();
                    txtwaybillNo.Text = "";
                    pnldata.Visible = false;
                    pnlNodata.Visible = true;
                }
            }
            else
            {
                Errormsg("Something Went Wrong Please Try Again.");
            }
        }
        else
        {
            Errormsg("Something Went Wrong Please Try Again.");
        }
    }
    #endregion

    #region "Events"
    protected void lbtnserachwaybill_Click(object sender, EventArgs e)
    {
        resetcontrol();
        if (txtwaybillNo.Text == "")
        {
            Errormsg("Please Enter Waybill No.");
            return;
        }
        else
        {
            loadwaybillNo(txtwaybillNo.Text);
        }
    }

    private void resetcontrol()
    {
        ddlclosure.SelectedValue = "0-N";
        txtremark.Text = "";
        dvfile.Visible = false;
        dvfileupload.Visible = false;
        lblFileName.Text = "";
    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        if (ddlclosure.SelectedValue == "0-N")
        {
            Errormsg("Please Select Closure Reason.");
            return;
        }
        if (txtremark.Text == "")
        {
            Errormsg("Please Enter Remark.");
            return;
        }

        string[] param = ddlclosure.SelectedValue.Split('-');
        string ddlclosurevalue = param[0].ToString().Trim();
        string isFileUpload = param[1].ToString().Trim();
        if (isFileUpload == "Y")
        {
            if (Session["sql"] == null)
            {
                Errormsg("Please Choose Sql File.");
                return;
            }
        }
        else
        {
            Session["sql"] = null;
        }
        lblConfirmation.Text = "Are You Sure? You Want To Submit";
        mpConfirmation.Show();
    }
    protected void ddlclosure_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["sql"] = null;
        
        dvfile.Visible = false;
        dvfileupload.Visible = false;
        string[] param = ddlclosure.SelectedValue.Split('-');
        string ddlclosurevalue = param[0].ToString().Trim();
        string isFileUpload = param[1].ToString().Trim();
        if (isFileUpload == "Y")
        {
            dvfile.Visible = true;
            dvfileupload.Visible = true;
        }
        else
        {
            dvfile.Visible = false;
            dvfileupload.Visible = false;
        }
    }
    protected void btnUploadpdf_Click(object sender, EventArgs e)
    {
        string _fileFormat = GetMimeDataOfFile(fudocfile);

        if (_fileFormat == "application/octet-stream")
        {
            if (!CheckFileExtentionWeb(fudocfile))
           {
                Errormsg("File Must Be Db Type.");
		lblFileName.Visible = false;
                return;
            }
        }
        else
        {
            Errormsg("Invalid File");
	    lblFileName.Visible = false;
            return;
       }

        if (fudocfile.HasFile == true)
        {
            if (Convert.ToInt32(fudocfile.FileBytes.Length) < 2097152 & fudocfile.FileName.Length > 2)
            {
                if (fudocfile.FileName.Length <= 50)
                {
                    Session["sql"] = fudocfile.FileBytes;
                    lblFileName.Text = fudocfile.FileName;
                    Session["sqlName"] = fudocfile.FileName;
                    lblFileName.Visible = true;
                }
            }
        }
    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        submitPrematureClosure();
    }
    #endregion

}