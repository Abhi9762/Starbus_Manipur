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


public partial class Auth_stinExcesspaymentamount : System.Web.UI.Page
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    DataTable dt1 = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblSummary.Text = "Summary as on Date " + DateTime.Now.ToString("dd/MM/yyyy hh:ss tt");
            Session["_moduleName"] = "Excess Payment Amount Waybills";
            LoadGrid("S");
            LoadGrid("P");
            loadsummary();
            Session["_LDepotCode"] = Session["_LDepotCode"].ToString();
        }
    }

    #region"Methods"
    private void LoadGrid(string action)
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "etm.f_get_submitted_request_for_excess_payment");
        MyCommand.Parameters.AddWithValue("p_depotid", Session["_LDepotCode"].ToString());
        if (action == "S")
        {
            MyCommand.Parameters.AddWithValue("p_action", action);
            MyCommand.Parameters.AddWithValue("p_waybill_no", tbRecordwaybillno.Text);
        }
        else if (action == "P")
        {
            MyCommand.Parameters.AddWithValue("p_action", action);
            MyCommand.Parameters.AddWithValue("p_waybill_no", txtPendingwaybillsearch.Text);
        }
        dt = bll.SelectAll(MyCommand);
        if (dt.TableName == "Success")
        {
            if (dt.Rows.Count > 0)
            {
                if (action == "P")
                {
                    DataRow[] dtPending = dt.Select("statuscode=0");
                    if (dtPending.Length > 0)
                    {
                        gvPendingRecords.Visible = true;
                        gvPendingRecords.DataSource = dtPending.CopyToDataTable();
                        gvPendingRecords.DataBind();
                        PnlNorecordPending.Visible = false;
                    }
                    else
                    {
                        gvPendingRecords.Visible = false;
                        PnlNorecordPending.Visible = true;
                    }

                }
                else if (action == "S")
                {
                    DataRow[] dtSubmitted = dt.Select("statuscode in (1,2)");
                    if (dtSubmitted.Length > 0)
                    {
                        gvRecords.Visible = true;
                        gvRecords.DataSource = dtSubmitted.CopyToDataTable();
                        gvRecords.DataBind();
                        pnlnoRecordfound.Visible = false;
                    }
                    else
                    {
                        gvRecords.Visible = false;
                        pnlnoRecordfound.Visible = true;
                    }
                }
            }
            else
            {
                if (action == "P")
                {
                    PnlNorecordPending.Visible = true;
                    gvPendingRecords.Visible = false;
                }
                if (action == "S")
                {
                    gvRecords.Visible = false;
                    pnlnoRecordfound.Visible = true;
                }

            }
        }
        else
        {
            gvPendingRecords.Visible = false;
            gvRecords.Visible = false;
            PnlNorecordPending.Visible = true;
            pnlnoRecordfound.Visible = true;
        }
    }
    private void loadsummary()
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "etm.f_get_submitted_request_for_excess_payment");
        MyCommand.Parameters.AddWithValue("p_depotid", Session["_LDepotCode"].ToString());
        MyCommand.Parameters.AddWithValue("p_action", "");
        MyCommand.Parameters.AddWithValue("p_waybill_no", "");
        dt = bll.SelectAll(MyCommand);
        DataRow[] dtPending = dt.Select("statuscode=0");
        DataRow[] dtApproved = dt.Select("statuscode =1");
        DataRow[] dtReject = dt.Select("statuscode =2");
        lblTotal.Text = dt.Rows.Count.ToString();
        lblApproved.Text = dtApproved.Length.ToString();
        lblPending.Text = dtPending.Length.ToString();
        lblreject.Text = dtReject.Length.ToString();
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
    private void GetTxns(string txnid)
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "etm.f_get_excess_payment_txns");
        MyCommand.Parameters.AddWithValue("p_id", txnid);
        dt = bll.SelectAll(MyCommand);
        if (dt.TableName == "Success")
        {
            if (dt.Rows.Count > 0)
            {
                gvTransaction.DataSource = dt;
                gvTransaction.DataBind();
            }
            else
            {
                MpTxns.Hide();
            }
        }
    }
    private void updateTxnStatus(string txnid)
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", " etm.f_update_excess_payment_status");
        MyCommand.Parameters.AddWithValue("p_id", txnid);
        MyCommand.Parameters.AddWithValue("p_action", Session["_Action"].ToString());
        MyCommand.Parameters.AddWithValue("p_update_by", Session["_UserCode"].ToString());
        dt = bll.SelectAll(MyCommand);
        if (dt.TableName == "Success")
        {
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["status"].ToString() == "DONE")
                {
                    Successmsg("Request Update Successfully");
                    LoadGrid("S");
                    LoadGrid("P");
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

    #region"Events"
    protected void gvRecords_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "viewDOC")
        {
            Session["_DOC"] = gvRecords.DataKeys[index].Values["uploadfile"];
            
            img.ImageUrl = GetImage(gvRecords.DataKeys[index].Values["uploadfile"]);
            if (img.ImageUrl == "")
            {
                Errormsg("Image Not Uploaded");
                return;
            }
            img.Visible = true;
            mpviewdocment.Show();
        }
        if (e.CommandName == "viewTXN")
        {
            string txnid = gvRecords.DataKeys[index].Values["refrenceid"].ToString();
            GetTxns(txnid);
            MpTxns.Show();
        }
        if (e.CommandName == "viewWaybill")
        {
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            Session["DUTYREFNO"] = gvRecords.DataKeys[index].Values["waybillno"].ToString();
            //openSubDetailsWindow("Waybill.aspx");
            eWaybill.Text = "<embed src = \"Waybill.aspx\" style=\"height: 80vh; width: 100%\" />";
            mpShowWaybill.Show();
        }
    }
    protected void gvRecords_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvRecords.PageIndex = e.NewPageIndex;
        LoadGrid("S");
    }
    protected void lbtnRecordsSearch_Click(object sender, EventArgs e)
    {
        if (tbRecordwaybillno.Text == "")
        {
            Errormsg("Please Enter Waybill No.");
            return;
        }
        LoadGrid("S");
    }
    protected void lbtnRecordsRest_Click(object sender, EventArgs e)
    {
        tbRecordwaybillno.Text = "";
        LoadGrid("S");
    }
    protected void lbtnPendingSearch_Click(object sender, EventArgs e)
    {
        if (txtPendingwaybillsearch.Text == "")
        {
            Errormsg("Please Enter Waybill No.");
            return;
        }
        LoadGrid("P");
    }
    protected void lbtnPendingReset_Click(object sender, EventArgs e)
    {
        txtPendingwaybillsearch.Text = "";
        LoadGrid("P");
    }
    protected void gvPendingRecords_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "viewDOC")
        {
           Session["_DOC"] = gvPendingRecords.DataKeys[index].Values["uploadfile"];
           
            img.ImageUrl = GetImage(gvPendingRecords.DataKeys[index].Values["uploadfile"]);
            if (img.ImageUrl == "")
            {
                Errormsg("Image Not Uploaded");
                return;
            }
            img.Visible = true;
            mpviewdocment.Show();
        }
        if (e.CommandName == "viewTXN")
        {
            string txnid = gvPendingRecords.DataKeys[index].Values["refrenceid"].ToString();
            GetTxns(txnid);
            MpTxns.Show();
        }
        if (e.CommandName == "Approve")
        {
            string txnid = gvPendingRecords.DataKeys[index].Values["refrenceid"].ToString();
            Session["_txnid"] = txnid;
            lblConfirmation.Text = "Are You Sure ? You Want To Approve This Request";
            mpConfirmation.Show();
            Session["_Action"] = "A";
        }
        if (e.CommandName == "Reject")
        {
            string txnid = gvPendingRecords.DataKeys[index].Values["refrenceid"].ToString();
            Session["_txnid"] = txnid;
            lblConfirmation.Text = "Are You Sure ? You Want To Reject This Request";
            mpConfirmation.Show();
            Session["_Action"] = "R";
        }
        if (e.CommandName == "viewWaybill")
        {
            GridViewRow oItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int RowIndex = oItem.RowIndex;
            Session["DUTYREFNO"] = gvPendingRecords.DataKeys[index].Values["waybillno"].ToString();
            //openSubDetailsWindow("Waybill.aspx");
            eWaybill.Text = "<embed src = \"Waybill.aspx\" style=\"height: 80vh; width: 100%\" />";
            mpShowWaybill.Show();
        }
    }
    protected void gvPendingRecords_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvPendingRecords.PageIndex = e.NewPageIndex;
        LoadGrid("P");
    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        if (Session["_Action"].ToString() == "A")
        {
            updateTxnStatus(Session["_txnid"].ToString());
        }
        if (Session["_Action"].ToString() == "R")
        {
            updateTxnStatus(Session["_txnid"].ToString());
        }
    }
    #endregion

}