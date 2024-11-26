using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_PAdminAgentAdvertisement : BasePage 
{
    NpgsqlCommand MyCommand = new NpgsqlCommand();
    private sbBLL bll = new sbBLL();
    DataTable dt = new DataTable();
    sbValidation _validation = new sbValidation();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    private sbCommonFunc _common = new sbCommonFunc();
    byte[] PhotoImage = null;
    [System.Runtime.InteropServices.DllImport("urlmon.dll")]
    public static extern int FindMimeFromData(IntPtr pBC, [MarshalAs(UnmanagedType.LPWStr)] string pwzUrl, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1, SizeParamIndex = 3)] byte[] pBuffer, int cbSize, [MarshalAs(UnmanagedType.LPWStr)] string pwzMimeProposed, int dwMimeFlags, [MarshalAs(UnmanagedType.U4)] ref int ppwzMimeOut, int dwReserved);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["_moduleName"] = "Agent Advertisement";
            loadAgentAdvertisement();
            Session["pdf"] = null;


        }

    }


    #region Methods
    private void ResetValue()
    {
        tbRegStartDate.Text = "";
        tbRegEndDate.Text = "";
        tbProStartDate.Text = "";
        tbProEndDate.Text = "";
        tbAdvDetailsOrderDate.Text = "";
        tbAdvDetailsOfficeOrderID.Text = "";
        tbRemarks.Text = "";
        Session["pdf"] = null;
        lblPDF.Visible = false;

    }
    public bool IsValidPdf(FileUpload fileupload)
    {
        string _fileFormat = GetpdfMimeDataOfFile(fileupload.PostedFile);
        if (((_fileFormat == "application/pdf")))
        {
        }
        else
        {
            Errormsg("Invalid file (Not a PDF)");
            return false;
        }
        return true;
    }
    public static string GetpdfMimeDataOfFile(HttpPostedFile file)
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
    private void Errormsg(string msg)
    {

        string popup = _popup.modalPopupSmall("W", "Please Check", msg, "Close");
        Response.Write(popup);
    }
    private void Successmsg(string msg)
    {
        string popup = _popup.modalPopupSmall("S", "Success", msg, "Close");
        Response.Write(popup);
    }
    public void confirmmsg(string message)
    {
        lblConfirmation.Text = message;
        mpConfirmation.Show();
    }
    private void InfoMsg(string msg)
    {
        string popup = _popup.modalPopupSmall("I", "Information", msg, "Close");
        Response.Write(popup);
    }
    private bool validvalue()
    {
        int msgcount = 0;
        string msg = "";
        if (tbRegStartDate.Text.Trim().Length > 0)
        {

        }
        else
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Please Enter Start Date of Registration.<br/>";
        }
        if (tbRegEndDate.Text.Trim().Length > 0)
        {

        }
        else
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Please Enter End Date of Registration.<br/>";
        }

        if (tbProStartDate.Text.Trim().Length > 0)
        {

        }
        else
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Please Enter Start Date of Processing.<br/>";
        }
        if (tbProEndDate.Text.Trim().Length > 0)
        {

        }
        else
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Please Enter End Date of Processing.<br/>";
        }
        if (_validation.IsValidString(tbRemarks.ToString(), 0, tbRemarks.MaxLength) == false)
        {
            Errormsg("Please Enter Valid Remark");
            return false;
        }
        if (msgcount > 0)
        {
            Errormsg(msg);
            return false;
        }
        return true;
    }
    private void SaveValue()
    {
        try
        {

            string stnid = "0";
            string UpdatedBy = Session["_UserCode"].ToString();
            string IPAddress = HttpContext.Current.Request.UserHostAddress;
            byte[] FileInvoice = (byte[])Session["pdf"];
            string Mresult = "";
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_agent_registration_openclose_insertupdate");
            MyCommand.Parameters.AddWithValue("p_advt_id", Convert.ToDecimal(stnid));
            MyCommand.Parameters.AddWithValue("p_reg_from_dt", tbRegStartDate.Text.ToString());
            MyCommand.Parameters.AddWithValue("p_reg_to_date", tbRegEndDate.Text.ToString());
            MyCommand.Parameters.AddWithValue("p_proc_from_dt", tbProStartDate.Text.ToString());
            MyCommand.Parameters.AddWithValue("p_proc_to_dt", tbProEndDate.Text.ToString());
            MyCommand.Parameters.AddWithValue("p_order_id", tbAdvDetailsOfficeOrderID.Text.ToString());
            MyCommand.Parameters.AddWithValue("p_order_dt", tbAdvDetailsOrderDate.Text.ToString());
            MyCommand.Parameters.AddWithValue("p_remark", tbRemarks.Text.ToString());
            MyCommand.Parameters.AddWithValue("p_updatedby", UpdatedBy);
            MyCommand.Parameters.AddWithValue("p_ipaddress", IPAddress);
            MyCommand.Parameters.AddWithValue("p_doc_file", (object)FileInvoice ?? DBNull.Value);
            Mresult = bll.UpdateAll(MyCommand);
            if (Mresult == "Success")
            {
                Successmsg("Agent Registration open/close Value Successfully Save");
                loadAgentAdvertisement();
                ResetValue();
            }
            else
            {
                _common.ErrorLog("PadminAgentAdvertisement- M", Mresult);
                Errormsg("Oops! You're data not saved." + Mresult);
            }

        }
        catch (Exception ex)
        {
            _common.ErrorLog("PadminAgentAdvertisement- M", ex.Message.ToString());
            Errormsg(ex.Message);
        }


    }
    private void loadAgentAdvertisement()
    {
        try
        {
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_agent_registration_openclose");
            DataTable MyTable = bll.SelectAll(MyCommand);
            if (MyTable.TableName == "Success")
            {
                if (MyTable.Rows.Count > 0)
                {
                    gvAgentRegistrationdate.DataSource = MyTable;
                    gvAgentRegistrationdate.DataBind();
                    PanelNoRecordCurrentAdvertisement.Visible = false;
                    gvAgentRegistrationdate.Visible = true;
                    lbtnAgentAdvertisementDownload.Visible = true;
                }

            }
            else
            {
                _common.ErrorLog("PadminAgentAdvertisement- M", MyTable.TableName);
                Errormsg(MyTable.TableName);

            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminAgentAdvertisement-M", ex.Message.ToString());
            Errormsg(ex.Message);

        }
    }
    private void loadAgentAdvertisementHistory()
    {
        try
        {
            lbtnAgentAdvertisementDownload.Visible = false;
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_agent_registration_openclose_history");
            DataTable MyTable = bll.SelectAll(MyCommand);
            if (MyTable.TableName == "Success")
            {
                if (MyTable.Rows.Count > 0)
                {
                    gvAgentRegDatesHistory.DataSource = MyTable;
                    gvAgentRegDatesHistory.DataBind();
                    pnlNoRecordAgentAdvertisementsHistory.Visible = false;
                    gvAgentRegDatesHistory.Visible = true;
                    
                }
            }
            else
            {
                _common.ErrorLog("PadminAgentAdvertisement- M", MyTable.TableName);
                Errormsg(MyTable.TableName);

            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminAgentAdvertisement-M", ex.Message.ToString());
            Errormsg(ex.Message);
        }
    }
    private void ExportRegistrationDateHistory()
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=AgentRegistrationDate.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            // To Export all pages
            gvAgentRegDatesHistory.AllowPaging = false;
            this.loadAgentAdvertisementHistory();
            gvAgentRegDatesHistory.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in gvAgentRegDatesHistory.HeaderRow.Cells)
                cell.BackColor = gvAgentRegDatesHistory.HeaderStyle.BackColor;
            foreach (GridViewRow row in gvAgentRegDatesHistory.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                        cell.BackColor = gvAgentRegDatesHistory.AlternatingRowStyle.BackColor;
                    else
                        cell.BackColor = gvAgentRegDatesHistory.RowStyle.BackColor;
                    cell.CssClass = "textmode";
                }
            }

            gvAgentRegDatesHistory.RenderControl(hw);
            // style to format numbers to string
            string style = "<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }
    #endregion
    #region Events
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        SaveValue();
    }
    protected void lbtnSaveAgentAdvertisementRegistration_Click(object sender, EventArgs e)
    {
        if (validvalue() == false)
        {
            return;
        }
        confirmmsg("Do you want to Save Agent Advertisement Value?");

    }
    protected void lbtnResetAgentAdvertisementRegistration_Click(object sender, EventArgs e)
    {
        ResetValue();
    }
    protected void btnUploadpdf_Click(object sender, EventArgs e)
    {
        if (IsValidPdf(fudocfile) == true)
        {
            if (fudocfile.HasFile == true)
            {
                if (Convert.ToInt32(fudocfile.FileBytes.Length) < 2097152 & fudocfile.FileName.Length > 2)
                {
                    if (fudocfile.FileName.Length <= 50)
                    {
                        Session["pdf"] = fudocfile.FileBytes;
                        lblPDF.Text = fudocfile.FileName;
                        Session["pdfName"] = fudocfile.FileName;
                    }
                }
                else
                {
                    Errormsg("Please select file less than 2 MB");
                }
            }
        }

    }
 
    protected void lbtnHistory_Click(object sender, EventArgs e)
    {
        mpAgentAdverisementHistory.Show();
        loadAgentAdvertisementHistory();
    }
    protected void gvAgentRegistrationdate_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvAgentRegistrationdate.PageIndex = e.NewPageIndex;
        loadAgentAdvertisement();
        mpAgentAdverisementHistory.Show();
    }
    protected void lbtnViewHelp_Click(object sender, EventArgs e)
    {
        InfoMsg("1. Agent online application registration open / close date <br>2. Set Agent Top Up Limit<br> 3. Set Agent security fee");
    }

    protected void gvAgentRegDatesHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvAgentRegDatesHistory.PageIndex = e.NewPageIndex;
        mpAgentAdverisementHistory.Show();
        loadAgentAdvertisementHistory();
    }

    protected void lbtnAgentAdvertisementDownload_Click(object sender, EventArgs e)
    {
        ExportRegistrationDateHistory();
    }

    #endregion
}