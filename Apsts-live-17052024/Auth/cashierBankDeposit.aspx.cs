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
public partial class Auth_cashierBankDeposit : BasePage
{

    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
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
        lbldatesummary.Text = DateTime.Now.ToString();
        Session["_moduleName"] = "Bank Deposit Dashboard";
        llbDatetimetotaldeposit.Text = "(As On " + DateTime.Now.ToString() + ")";
        if (!IsPostBack)
        {
            Random randomclass = new Random();
            Session["rndNoCheck"] = randomclass.Next().ToString();
            hidtoken.Value = Session["rndNoCheck"].ToString();
            loadbank();
            loadOpeningClosingDetails();
            loadDepositDate();
            amountVerifyList("1");
            amountVerifyList("2");
            amountDetails();
            Session["bytes"] = null;
        }
    }

    #region "Common Method"
    private void CsrfTokenValidate()
    {
        if (hidtoken.Value != Session["rndNoCheck"].ToString())
        {
            Response.Redirect("errorpage.aspx");
            return;
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
        if (_security.isSessionExist(Session["_RNDIDENTIFIERCASHIER"]) == true)
        {
            Session["_RNDIDENTIFIERCASHIER"] = Session["_RNDIDENTIFIERCASHIER"];
        }
        else
        {
            Response.Redirect("../sessionTimeout.aspx");
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
    #endregion

    #region "Methods"
    private void loadbank()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.f_get_bank");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                ddlbank.DataSource = dt;
                ddlbank.DataTextField = "b_name";
                ddlbank.DataValueField = "id";
                ddlbank.DataBind();
            }
            else
            {
                Errormsg(dt.TableName);
            }
            ddlbank.Items.Insert(0, "Select");
            ddlbank.Items[0].Value = "0";
            ddlbank.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message.ToString());
            _common.ErrorLog("cashierBankDeposit.aspx-0001", ex.Message.ToString());
        }
    }
    private void loadOpeningClosingDetails()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_chest_opening_closing");
            MyCommand.Parameters.AddWithValue("p_chestid", Session["_ChestID"].ToString());
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                lblopenby.Text = dt.Rows[0]["open_by"].ToString();
                lblopenat.Text = dt.Rows[0]["open_datetime"].ToString();
                lblopenbal.Text = dt.Rows[0]["open_bal"].ToString() + " ₹";
                lblcloseby.Text = dt.Rows[0]["closed_by"].ToString();
                lblcloseat.Text = dt.Rows[0]["closed_datetime"].ToString();
                lblclosebal.Text = dt.Rows[0]["close_bal"].ToString() + " ₹";
                lblcurrentbal.Text = dt.Rows[0]["current_bal"].ToString();
                lbltotaldeposit.Text = dt.Rows[0]["deposit_bal"].ToString() + " ₹";
                lbltotalCollection.Text = dt.Rows[0]["collect_bal"].ToString() + " ₹";
            }
            else
            {
                Errormsg(dt.TableName);
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message.ToString());
            _common.ErrorLog("cashierBankDeposit.aspx-0002", ex.Message.ToString());
        }
    }
    private void loadDepositDate()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_depositdate");
            MyCommand.Parameters.AddWithValue("p_chestid", Session["_ChestID"].ToString());
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                hdstartdate.Value = dt.Rows[0]["ddate"].ToString();
            }
            else
            {
                Errormsg(dt.TableName);
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message.ToString());
            _common.ErrorLog("cashierBankDeposit.aspx-0003", ex.Message.ToString());
        }
    }
    private void amountVerifyList(string flag)
    {
        try
        {
            // gvPendingTransaction.Visible = false;
            //pnlPendingNodata.Visible = false;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_chest_amt_verify_not");
            MyCommand.Parameters.AddWithValue("p_chstid", Session["_ChestID"].ToString());
            MyCommand.Parameters.AddWithValue("p_flag", flag);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (flag == "1")
                {
                    if (dt.Rows.Count > 0)
                    {
                        gvPendingTransaction.DataSource = dt;
                        gvPendingTransaction.DataBind();
                        gvPendingTransaction.Visible = true;
                        pnlPendingNodata.Visible = false;
                        lblPendingamt.Text = Convert.ToString(dt.Compute("SUM(amt)", ""));
                    }
                    else
                    {
                        lblPendingamt.Text = "0";
                        gvPendingTransaction.Visible = false;
                        pnlPendingNodata.Visible = true;
                    }
                }
                if (flag == "2")
                {
                    if (dt.Rows.Count > 0)
                    {
                        gvVerifyList.DataSource = dt;
                        gvVerifyList.DataBind();
                        gvVerifyList.Visible = true;
                        pnlVerifyAmtNodata.Visible = false;
                    }
                    else
                    {
                        gvVerifyList.Visible = false;
                        pnlVerifyAmtNodata.Visible = true;
                    }
                }
            }
            else
            {
                Errormsg(dt.TableName);
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message.ToString());
            _common.ErrorLog("cashierBankDeposit.aspx-0004", ex.Message.ToString());
        }
    }
    private void amountDetails()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_chest_amt_details");
            MyCommand.Parameters.AddWithValue("p_chstid", Session["_ChestID"].ToString());
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    lblTotalAmountSum.Text = dt.Rows[0]["total_amt"].ToString();
                    //Session["CHEST_MANDATORY_AMT"] = dt.Rows[0]["mandatory_amt"].ToString();
                    // lblMandatoryAmountSum.Text = dt.Rows[0]["mandatory_amt"].ToString();
                    Session["CHEST_MANDATORY_AMT"] = dt.Rows[0]["total_amt"].ToString();
                    lblMandatoryAmountSum.Text = dt.Rows[0]["total_amt"].ToString();
                }
                else
                {
                    lblTotalAmountSum.Text = "0";
                    lblMandatoryAmountSum.Text = "0";
                }
            }
            else
            {
                Errormsg(dt.TableName);
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message.ToString());
            _common.ErrorLog("cashierBankDeposit.aspx-0005", ex.Message.ToString());
        }
    }
    private void savedetails()
    {
        try
        {
            byte[] documentfile = null;
            //if (fileupload.FileName == "")
            //{
            //    documentfile = null;
            //}
            //else
            //{
            //  Invalid file (Not a PDF)
            //  Session["pdf"] = fileupload.FileBytes;
            documentfile = (byte[])Session["bytes"];
            //}


            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_chest_bank_deposit");
            MyCommand.Parameters.AddWithValue("p_chstid", Session["_ChestID"].ToString());
            MyCommand.Parameters.AddWithValue("p_date", txtdate.Text);
            MyCommand.Parameters.AddWithValue("p_bank", Convert.ToInt64(ddlbank.SelectedValue));
            MyCommand.Parameters.AddWithValue("p_amount", Convert.ToDecimal(txtamt.Text));
            MyCommand.Parameters.AddWithValue("p_receiptno", txtreceiptno.Text);
            MyCommand.Parameters.AddWithValue("p_receipt", (object)documentfile ?? DBNull.Value);
            MyCommand.Parameters.AddWithValue("p_remark", txtremark.Text);
            MyCommand.Parameters.AddWithValue("p_depositby", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("p_updateby", Session["_UserCode"].ToString());
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows[0]["status"].ToString() == "ALREADY")
                {
                    Errormsg("Receipt No. Already Exits.");
                    return;
                }
                else if (dt.Rows[0]["status"].ToString() == "Done")
                {
                    Successmsg("Bank Deposit Details Have Successfully Been Saved & Submitted For Approval.");
                    loadOpeningClosingDetails();
                    amountVerifyList("1");
                    amountVerifyList("2");
                    amountDetails();
                    resetControl();
                    Session["bytes"] = null;
                }
                else
                {
                    Errormsg("Something Went Wrong");
                }
            }
            else
            {
                Errormsg(dt.TableName);
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message.ToString());
            _common.ErrorLog("cashierBankDeposit.aspx-0006", ex.Message.ToString());
        }
    }
    private void resetControl()
    {
        txtdate.Text = "";
        ddlbank.SelectedValue = "0";
        txtamt.Text = "0";
        txtreceiptno.Text = "";
        txtremark.Text = "";
        Session["pdf"] = null;
    }
    public bool IsValidPdf(FileUpload fileupload)
    {
        // string _fileFormat = GetMimeDataOfFile(fileupload.PostedFile);
        //if (((_fileFormat == "application/pdf")))
        //{
        //}
        //else
        //{
        //    // Errormsg("Invalid file (Not a PDF)");
        //    return false;
        //}
        return true;
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
    private void getDepositDetails(string receiptno, string receiptid, string chestid)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_chest_deposit_details");
            MyCommand.Parameters.AddWithValue("p_receiptno", receiptno);
            MyCommand.Parameters.AddWithValue("p_receiptid", receiptid);
            MyCommand.Parameters.AddWithValue("p_chestid", chestid);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    plblchtname.Text = dt.Rows[0]["ofc_name"].ToString();
                    plblbnkname.Text = dt.Rows[0]["bnk_name"].ToString();
                    plbldoptby.Text = dt.Rows[0]["depost_by"].ToString();
                    plbldoptdate.Text = dt.Rows[0]["deposit_date"].ToString();
                    plblamt.Text = dt.Rows[0]["amt"].ToString();
                    plblreceipt.Text = dt.Rows[0]["receipt_no"].ToString();
                    plbldoptentrydate.Text = dt.Rows[0]["entry_depositdate"].ToString();
                    plblverifyby.Text = dt.Rows[0]["verify_by"].ToString();
                    if (dt.Rows[0]["verifydate"].ToString() == "")
                    {
                        plblverifydate.Text = "NA";
                    }
                    else
                    {
                        plblverifydate.Text = dt.Rows[0]["verifydate"].ToString();
                    }
                    mpdepositdetails.Show();
                }
                else
                {
                    mpdepositdetails.Hide();
                }
            }
            else
            {
                Errormsg(dt.TableName);
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message.ToString());
            _common.ErrorLog("cashierBankDeposit.aspx-0007", ex.Message.ToString());
        }
    }
    #endregion

    #region "Events"
    protected void btnYes_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
    }
    protected void btnNo_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        savedetails();
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (txtdate.Text == "")
        {
            Errormsg("Please Select Valid Date");
            return;
        }
        if (ddlbank.SelectedValue == "0")
        {
            Errormsg("Please Select Valid Bank");
            return;
        }
        if (txtamt.Text == "")
        {
            Errormsg("Please Enter Valid Amount");
            return;
        }
        if (Convert.ToDecimal(txtamt.Text) <= 0)
        {
            Errormsg("Please Enter Valid Amount");
            return;
        }
        if (Convert.ToDecimal(txtamt.Text) > Convert.ToDecimal(Session["CHEST_MANDATORY_AMT"].ToString()))
        {
            Errormsg("Deposit Amount should not be Greater then Mandatory Amount");
            return;
        }
        if (txtreceiptno.Text == "")
        {
            Errormsg("Please Enter Valid Receipt No.");
            return;
        }

        if (Convert.ToInt32(fudocfile.FileBytes.Length) > 1048576)
        {
            Errormsg("Please select file less than 1 MB");
            return;
        }
        mpConfirmation.Show();

    }
    //protected void lbtnbankdeposit_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("CashierDashboard.aspx");
    //}
    protected void gvPendingTransaction_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvPendingTransaction.PageIndex = e.NewPageIndex;
        amountVerifyList("1");
    }
    protected void gvVerifyList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvVerifyList.PageIndex = e.NewPageIndex;
        amountVerifyList("2");
    }
    #endregion

    protected void btnUploadpdf_Click(object sender, EventArgs e)
    {
        
        string _fileFormat = GetMimeDataOfFile(fudocfile);

        if (_fileFormat == "image/pjpeg" )
        {

        }
        else
        {
            Errormsg("Invalid File");
            lblFileName.Visible = false;
            return;
        }

        if (fudocfile.HasFile == true)
        {
            if (Convert.ToInt32(fudocfile.FileBytes.Length) < 2097152)
            {
                if (fudocfile.FileName.Length <= 50)
                {
                    Session["bytes"] = fudocfile.FileBytes;
                    lblFileName.Text = fudocfile.FileName;
                    Session["sqlName"] = fudocfile.FileName;
                    lblFileName.Visible = true;
                }
            }
            else
            {
                Errormsg("File Should be less than 2 Mb.");
            }
        }
    }


    protected void gvPendingTransaction_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        CsrfTokenValidate();
        if (e.CommandName == "ViewDetails")
        {
            int rowIndex = int.Parse(e.CommandArgument.ToString());
            Session["RECEIPTNO_"] = gvPendingTransaction.DataKeys[rowIndex]["receipt_no"].ToString();
            Session["RECEIPTID_"] = gvPendingTransaction.DataKeys[rowIndex]["r_id"].ToString();
            Session["CHESTID_"] = Session["_ChestID"].ToString();
            getDepositDetails(Session["RECEIPTNO_"].ToString(), Session["RECEIPTID_"].ToString(), Session["CHESTID_"].ToString());
        }
    }

    protected void gvVerifyList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        CsrfTokenValidate();
        if (e.CommandName == "ViewDetails")
        {
            int rowIndex = int.Parse(e.CommandArgument.ToString());
            Session["RECEIPTNO_"] = gvVerifyList.DataKeys[rowIndex]["receipt_no"].ToString();
            Session["RECEIPTID_"] = gvVerifyList.DataKeys[rowIndex]["r_id"].ToString();
            Session["CHESTID_"] = Session["_ChestID"].ToString();
            getDepositDetails(Session["RECEIPTNO_"].ToString(), Session["RECEIPTID_"].ToString(), Session["CHESTID_"].ToString());
        }
    }
}