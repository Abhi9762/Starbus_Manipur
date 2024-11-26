using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Auth_dashWaybillExcessAmount : System.Web.UI.Page
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
        if (!IsPostBack)
        {
            hdnTotSlabs.Value = "0";
            loadwaybillNo(Session["dutySlipRefno"].ToString(), Session["_LDepotCode"].ToString());
            SetInitialRow();
        }
    }

    #region"Methods"
    private void loadwaybillNo(string waybillno, string officeid)
    {
        pnldata.Visible = false;
        pnlNodata.Visible = true;
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_waybill_detail_for_conductor");
        MyCommand.Parameters.AddWithValue("p_waybillno", waybillno);
        MyCommand.Parameters.AddWithValue("p_officeid", officeid);
        dt = bll.SelectAll(MyCommand);

        if (dt.TableName == "Success")
        {
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["excess_amt_status"].ToString() == "")
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
                if (dt.Rows[0]["excess_amt_status"].ToString() == "0")
                {
                    pnldata.Visible = false;
                    pnlNodata.Visible = true;
                    lblnodata.Text = "Excess Payment Request Initiated. Please Contact Station Incharge For Further Information";
                    lblnodata.CssClass = "text-primary";
                }
                if (dt.Rows[0]["excess_amt_status"].ToString() == "1")
                {
                    pnldata.Visible = false;
                    pnlNodata.Visible = true;
                    lblnodata.Text = "Excess Payment Request Approved. Now You Can Submit The Cash";
                    lblnodata.CssClass = "text-success";
                }
                if (dt.Rows[0]["excess_amt_status"].ToString() == "2")
                {
                    pnldata.Visible = false;
                    pnlNodata.Visible = true;
                    lblnodata.Text = "Excess Payment Request Rejected. Please Contact Station Incharge For Further Information";
                    lblnodata.CssClass = "text-danger";
                }
                
            }
            else
            {
                lblnodata.Text = "Waybill Details Not Found";
                pnldata.Visible = false;
                pnlNodata.Visible = true;
            }
        }
    }
    private void SetInitialRow()

    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("txn_no", typeof(string)));
        dt.Columns.Add(new DataColumn("actual_amt", typeof(string)));
        dt.Columns.Add(new DataColumn("excess_amt", typeof(string)));

        dr = dt.NewRow();
        dr["RowNumber"] = 1;
        dr["txn_no"] = string.Empty;
        dr["actual_amt"] = string.Empty;
        dr["excess_amt"] = string.Empty;

        dt.Rows.Add(dr);

        //dr = dt.NewRow();
        //Store the DataTable in ViewState

        ViewState["CurrentTable"] = dt;
        gvTxnDetails.DataSource = dt;
        gvTxnDetails.DataBind();
    }
    private void AddNewRowToGrid()

    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)

        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)

                {
                    //extract the TextBox values

                    TextBox box1 = (TextBox)gvTxnDetails.Rows[rowIndex].Cells[1].FindControl("tbtxn");
                    TextBox box2 = (TextBox)gvTxnDetails.Rows[rowIndex].Cells[2].FindControl("tbactulamt");
                    TextBox box3 = (TextBox)gvTxnDetails.Rows[rowIndex].Cells[3].FindControl("tbexcessamt");

                    drCurrentRow = dtCurrentTable.NewRow();

                    drCurrentRow["RowNumber"] = i + 1;
                    dtCurrentTable.Rows[i - 1]["txn_no"] = box1.Text;
                    dtCurrentTable.Rows[i - 1]["actual_amt"] = box2.Text;
                    dtCurrentTable.Rows[i - 1]["excess_amt"] = box3.Text;

                    rowIndex++;

                }
                dtCurrentTable.Rows.Add(drCurrentRow);
                ViewState["CurrentTable"] = dtCurrentTable;
                gvTxnDetails.DataSource = dtCurrentTable;
                gvTxnDetails.DataBind();
                hdnTotSlabs.Value = gvTxnDetails.Rows.Count.ToString();

            }

        }
        else
        {
            Errormsg("ViewState is null");
        }
        //Set Previous Data on Postbacks

        SetPreviousData();

    }
    private void RemoveRowToGrid(int rowindex)
    {
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            if (dtCurrentTable.Rows.Count > 0)
            {
                dtCurrentTable.Rows.RemoveAt(rowindex);
                ViewState["CurrentTable"] = dtCurrentTable;
                gvTxnDetails.DataSource = dtCurrentTable;
                gvTxnDetails.DataBind();

                hdnTotSlabs.Value = gvTxnDetails.Rows.Count.ToString();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        //Set Previous Data on Postbacks
        SetPreviousData();
    }
    private void SetPreviousData()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TextBox box1 = (TextBox)gvTxnDetails.Rows[rowIndex].Cells[1].FindControl("tbtxn");
                    TextBox box2 = (TextBox)gvTxnDetails.Rows[rowIndex].Cells[2].FindControl("tbactulamt");
                    TextBox box3 = (TextBox)gvTxnDetails.Rows[rowIndex].Cells[3].FindControl("tbexcessamt");
                    box1.Text = dt.Rows[i]["txn_no"].ToString();
                    box2.Text = dt.Rows[i]["actual_amt"].ToString();
                    box3.Text = dt.Rows[i]["excess_amt"].ToString();
                    rowIndex++;
                }
            }
        }
    }
    private void InfoMsg(string msg)
    {
        string popup = _popup.modalPopupSmall("I", "Information", msg, "Close");
        Response.Write(popup);
    }
    private void Errormsg(string msg)
    {

        string popup = _popup.modalPopupSmall("D", "Please Check", msg, "Close");
        Response.Write(popup);
    }
    private void Successmsg(string msg)
    {

        string popup = _popup.modalPopupSmall("S", "Congratulations", msg, "Close");
        Response.Write(popup);
    }
    private void ConfirmMsg(string msg)
    {
        lblConfirmation.Text = msg;
        mpConfirmation.Show();
    }
    private bool ValidaionSlab(string txnno, string actualamt, string excessamt)
    {
        try
        {
            int count = 0;
            string msg = "";


            if (_validation.IsValidString(txnno, 1, 20) == false)
            {
                count = count + 1;
                msg = msg + count.ToString() + ". Invalid Transaction No.<br/>";
            }
            if (_validation.IsValidString(actualamt, 1, 20) == false)
            {
                count = count + 1;
                msg = msg + count.ToString() + ". Invalid Actual Amt.<br/>";
            }
            if (_validation.IsValidString(excessamt, 1, 20) == false)
            {
                count = count + 1;
                msg = msg + count.ToString() + ". Invalid Excess Amt.<br/>";
            }
            int actualamts = Convert.ToInt32(actualamt);
            int excessamts = Convert.ToInt32(excessamt);
            if (excessamts < actualamts)
            {
                count = count + 1;
                msg = msg + count.ToString() + ". Excess Amount Must Be Greater Than Actual Amount.<br/>";
            }

            if (_validation.isValideDecimalNumber(actualamt, 1, 20) == false)
            {
                count = count + 1;
                msg = msg + count.ToString() + ". Invalid Actual Amt.<br/>";
            }
            if (_validation.isValideDecimalNumber(excessamt, 1, 20) == false)
            {
                count = count + 1;
                msg = msg + count.ToString() + ". Invalid Excess Amt.<br/>";
            }
            if (count > 0)
            {
                Errormsg(msg);
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {

            Errormsg("There is invalid values in slab. Please enter valid values.");
            return false;
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
    private bool CheckFileExtentionWeb(FileUpload fu)
    {
        try
        {
            bool fileExtensionOK = false;
            string fileExtension;
            fileExtension = System.IO.Path.GetExtension(fu.FileName).ToLower();
            string[] allowedExtensions = new[] { ".pdf" };
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
    public DataTable getSlab_table()
    {

        // Create a new DataTable.
        DataTable dtTable = new DataTable("txnTable");
        dtTable.Columns.Add("txn", typeof(string));
        dtTable.Columns.Add("actual", typeof(string));
        dtTable.Columns.Add("excess", typeof(string));

        string tbtxn, tbactual, tbexcess;
        // Loop through the GridView and copy rows.
        foreach (GridViewRow row in gvTxnDetails.Rows)
        {
            TextBox tbFromkm1 = row.FindControl("tbtxn") as TextBox;
            tbtxn = tbFromkm1.Text;
            TextBox tbtokm1 = row.FindControl("tbactulamt") as TextBox;
            tbactual = tbtokm1.Text;
            TextBox tbAccedentSCSlab1 = row.FindControl("tbexcessamt") as TextBox;
            tbexcess = tbAccedentSCSlab1.Text;

            if (!(tbtxn.Trim().Length <= 0 || tbactual.Trim().Length <= 0 || tbexcess.Trim().Length <= 0))
            {
                dtTable.Rows.Add(tbtxn, tbactual, tbexcess);
            }
        }

        return dtTable;
    }
    private void SaveDetails()
    {
        StringWriter swStringWriter = new StringWriter();
        var dt = getSlab_table();
        dt.TableName = "txnTable";
        DataSet ds = new DataSet("Root");
        ds.Tables.Add(dt);
        ds.WriteXml(swStringWriter);
        byte[] documentfile = (byte[])Session["sql"];
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "etm.f_save_waybill_excess_amount");
        MyCommand.Parameters.AddWithValue("p_conductor_code", Session["_UserCode"].ToString());
        MyCommand.Parameters.AddWithValue("p_waybill_no", lblwaybillno.Text);

        MyCommand.Parameters.AddWithValue("p_remark", txtremark.Text);
        MyCommand.Parameters.AddWithValue("p_txns", swStringWriter.ToString());
        MyCommand.Parameters.AddWithValue("p_file", (object)documentfile ?? DBNull.Value);
        dt = bll.SelectAll(MyCommand);
       
        if (dt.TableName == "Success")
        {
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["status"].ToString() == "DONE")
                {
                    Successmsg("Status Updated Successfully");
                    txtremark.Text = "";
                    SetInitialRow();
                    lblFileName.Text = "";
                    Session["sql"] = null;
                    pnldata.Visible = false;
                    pnlNodata.Visible = true;
                    lblnodata.Text = "Excess Payment Request Initiated";
                }
                else
                {
                    Errormsg("Something Went Wrong");
                }
            }
            else
            {
                Errormsg("Something Went Wrong");
            }
        }
        else
        {
            Errormsg("Something Went Wrong");
        }
    }
    #endregion

    #region"Events"
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        if (Session["Action"].ToString() == "S")
        {
            SaveDetails();
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        if (_validation.IsValidString(txtremark.Text, 1, 200) == false)
        {
            Errormsg("Invalid Remark");
            return;
        }
        StringWriter swStringWriter = new StringWriter();

        var dt = getSlab_table();
        dt.TableName = "txnTable";
        DataSet ds = new DataSet("Root");
        ds.Tables.Add(dt);
        ds.WriteXml(swStringWriter);
        string checkstring = swStringWriter.ToString();
        if (checkstring == "<Root />")
        {
            Errormsg("Please Add Transaction");
            return;
        }
        if (Session["sql"] == null)
        {
            Errormsg("Please Choose Pdf File.");
            return;
        }

        Session["Action"] = "S";
        ConfirmMsg("Do you want to Save Details ?");
    }
    protected void ddlclosure_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void gvTxnDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "Add")
        {
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            TextBox txttxnno = (TextBox)gvTxnDetails.Rows[RowIndex].FindControl("tbtxn");
            TextBox txtactualamt = (TextBox)gvTxnDetails.Rows[RowIndex].FindControl("tbactulamt");
            TextBox txtexcessamt = (TextBox)gvTxnDetails.Rows[RowIndex].FindControl("tbexcessamt");
            bool validation = ValidaionSlab(txttxnno.Text, txtactualamt.Text, txtexcessamt.Text);

            if (validation == true)
            {
                if (gvTxnDetails.Rows.Count > 1 && RowIndex >= 1)
                {

                    Int32 actualamt = Convert.ToInt32(txtactualamt.Text.ToString());
                    Int32 excessamt = Convert.ToInt32(txtexcessamt.Text.ToString());
                    if (excessamt < actualamt)
                    {
                        Errormsg("Excess Amount shouldnt be less than Actual amount");
                        txtactualamt.Text = "";
                        txtexcessamt.Text = "";

                        return;
                    }
                }
                AddNewRowToGrid();
                txtactualamt.Enabled = true;
                txtexcessamt.Enabled = true;
                txttxnno.Enabled = true;

            }
            else
            {
                return;
            }

        }
        if (e.CommandName == "Remove")
        {
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            RemoveRowToGrid(index);
            //makeDataTable(RowIndex);
        }
    }
    protected void gvTxnDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            LinkButton lbtnAddNewSlab = (LinkButton)e.Row.FindControl("lbtnAddNewSlab");
            LinkButton lbtnRemoveNewSlab = (LinkButton)e.Row.FindControl("lbtnRemoveNewSlab");

            TextBox tbtxn = (TextBox)e.Row.FindControl("tbtxn") as TextBox;
            TextBox tbactual = (TextBox)e.Row.FindControl("tbactulamt") as TextBox;
            TextBox tbexcess = (TextBox)e.Row.FindControl("tbexcessamt") as TextBox;

            int rindex = int.Parse(hdnTotSlabs.Value.ToString());
            lbtnAddNewSlab.Visible = false;
            lbtnRemoveNewSlab.Visible = false;
            tbtxn.Enabled = false;
            tbactual.Enabled = false;
            tbexcess.Enabled = false;



            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            if (e.Row.RowIndex == dtCurrentTable.Rows.Count - 1)
            {
                lbtnAddNewSlab.Visible = true;
                lbtnRemoveNewSlab.Visible = false;
                tbtxn.Enabled = true;
                tbactual.Enabled = true;
                tbexcess.Enabled = true;


            }
            else if (e.Row.RowIndex == dtCurrentTable.Rows.Count - 2)
            {
                lbtnAddNewSlab.Visible = false;
                lbtnRemoveNewSlab.Visible = true;

            }

        }
    }
    protected void btnUploadpdf_Click(object sender, EventArgs e)
    {
        string _fileFormat = GetMimeDataOfFile(fudocfile);

        if (_fileFormat == "image/pjpeg")
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
                    Session["sql"] = fudocfile.FileBytes;
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
    #endregion




}