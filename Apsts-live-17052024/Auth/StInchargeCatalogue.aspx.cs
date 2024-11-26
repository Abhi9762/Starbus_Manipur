using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_StInchargeCatalogue : BasePage
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    string current_date = DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("yyyy");

    protected void Page_Load(object sender, EventArgs e)
    {
        checkForSecurity();
        if (!IsPostBack)
        {
            Random randomclass = new Random();
            Session["rndNoCheck"] = randomclass.Next().ToString();
            hidtoken.Value = Session["rndNoCheck"].ToString();
            loadDepotdetails(Session["_UserCode"].ToString());
            loadssioffice();
            Session["_moduleName"] = "Catalogue";
            txtDepositDate.Text = current_date;
            if (Session["_RoleCode"].ToString() == "8")
            {
                ddlssioffice.SelectedValue = Session["_LOfficeid"].ToString();
                ddlssioffice.Enabled = false;
                loadData(ddlstatus.SelectedValue);
                loadAmtCount();
                loadWaybillCount();
                loadWaybillKm();
                loadTDServicesummary();
                pnlNoData.Visible = false;
                pnlData.Visible = true;
            }
            else
            {
                pnlNoData.Visible = true;
                pnlData.Visible = false;
            }
           
        }
        lbldatetime.Text = DateTime.Now.ToString();
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
    private void loadWaybillCount()
    {
        try
        {
            // gvPendingTransaction.Visible = false;
            //pnlPendingNodata.Visible = false;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_waybill_verification_count");
            MyCommand.Parameters.AddWithValue("p_officeid", Session["_LDepotCode"].ToString());

            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                decimal xx = Convert.ToDecimal(dt.Rows[0]["pending_waybill"].ToString()) + Convert.ToDecimal(dt.Rows[0]["verified_waybill"].ToString());
                lbltotWayBill.Text = xx.ToString();
                lblverifyWayBill.Text = dt.Rows[0]["verified_waybill"].ToString();
                lblpendingWayBill.Text = dt.Rows[0]["pending_waybill"].ToString();
            }
            else
            {
                Errormsg(dt.TableName);
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("StInchargeCatalogue.aspx-00022", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
        }
    }
    public void loadTDServicesummary()
    {
        try
        {
            string officeid = Session["_LDepotCode"].ToString();


            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_bustargetincomesummary");
            MyCommand.Parameters.AddWithValue("p_depotcode", officeid);
            MyCommand.Parameters.AddWithValue("p_status", "O");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                lbltotservice.Text = dt.Rows[0]["totalservice"].ToString();
                lblactiveservice.Text = dt.Rows[0]["activeentry"].ToString();
                lblexpire2dayservice.Text = dt.Rows[0]["toexpireentry"].ToString();
                lblexpireservice.Text = dt.Rows[0]["expiredentry"].ToString();
            }
        }
        catch (Exception ex)
        {
        }
    }
    private void loadWaybillKm()
    {
        try
        {
            pnlNorecordwaybill.Visible = true;
            grdWaybillVerified.Visible = false;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", " etm.f_get_extended_km_wabill");
            MyCommand.Parameters.AddWithValue("p_officeid", Session["_LDepotCode"].ToString());

            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    grdWaybillVerified.DataSource = dt;
                    grdWaybillVerified.DataBind();
                    grdWaybillVerified.Visible = true;
                    pnlNorecordwaybill.Visible = false;
                }
                else
                {
                    grdWaybillVerified.Visible = false;
                    pnlNorecordwaybill.Visible = true;
                }
            }
            else
            {
                Errormsg(dt.TableName);
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("StInchargeCatalogue.aspx-0011", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
        }
    }
    private void loadssioffice()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_ssi_office");
            MyCommand.Parameters.AddWithValue("sp_officeid", Session["_LDepotCode"].ToString());
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlssioffice.DataSource = dt;
                    ddlssioffice.DataTextField = "sp_officename";
                    ddlssioffice.DataValueField = "sp_office";
                    ddlssioffice.DataBind();

                }
                else
                {
                   
                }
                ddlssioffice.Items.Insert(0, "Select");
                ddlssioffice.Items[0].Value = "0";
                ddlssioffice.SelectedIndex = 0;
            }
            else
            {
                Response.Redirect("../Errorpage.aspx");
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("../Errorpage.aspx");
        }
    }
    private void loadDepotdetails(string usercode)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_logged_st_incharge_details");
            MyCommand.Parameters.AddWithValue("p_userid", usercode);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    Session["_LDepotCode"] = dt.Rows[0]["depot_code"].ToString();
                    Session["_LOfficeid"] = dt.Rows[0]["officeid"].ToString();
                    if (Session["_RoleCode"].ToString() == "99")
                    {
                        Session["_LDepotCode"] = dt.Rows[0]["officeid"].ToString();
                    }
                    if (Session["_RoleCode"].ToString() == "5")
                    {
                        Session["_LDepotCode"] = Session["_DepotCodeM"].ToString();
                    }
                }
                else
                {
                    Response.Redirect("../Errorpage.aspx");
                }
            }
            else
            {
                Response.Redirect("../Errorpage.aspx");
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("StInchargeCatalogue.aspx-0001", ex.Message.ToString());
            Response.Redirect("../Errorpage.aspx");
        }
    }
    private void loadAmtCount()
    {
        try
        {
            // gvPendingTransaction.Visible = false;
            //pnlPendingNodata.Visible = false;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_chest_amt_count");
            //MyCommand.Parameters.AddWithValue("p_officeid", Session["_LDepotCode"].ToString());
            MyCommand.Parameters.AddWithValue("p_officeid", ddlssioffice.SelectedValue.ToString());

            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                decimal xx = Convert.ToDecimal(dt.Rows[0]["pending_amt"].ToString()) + Convert.ToDecimal(dt.Rows[0]["verified_amt"].ToString());
                lbltotamt.Text = xx.ToString();
                lblVerifyamt.Text = dt.Rows[0]["verified_amt"].ToString();
                lblpendingamt.Text = dt.Rows[0]["pending_amt"].ToString();
            }
            else
            {
                Errormsg(dt.TableName);
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("StInchargeCatalogue.aspx-0002", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
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
    private void CsrfTokenValidate()
    {
        if (hidtoken.Value != Session["rndNoCheck"].ToString())
        {
            Response.Redirect("errorpage.aspx");
            return;
        }
    }
    private void loadData(string status)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            //oldcode
            //MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_cahsier_amt_verify_not");
            //MyCommand.Parameters.AddWithValue("p_empcode", Session["_UserCode"].ToString());
            //MyCommand.Parameters.AddWithValue("p_flag", status);
            //MyCommand.Parameters.AddWithValue("p_date", txtDepositDate.Text);

            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_cahsier_amt_verify_not_new");
            MyCommand.Parameters.AddWithValue("p_officeid", ddlssioffice.SelectedValue.ToString());
            MyCommand.Parameters.AddWithValue("p_flag", status);
            MyCommand.Parameters.AddWithValue("p_date", txtDepositDate.Text);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    grdAmtVerifyornot.DataSource = dt;
                    grdAmtVerifyornot.DataBind();
                    grdAmtVerifyornot.Visible = true;
                    pnlNoRecord.Visible = false;
                }
                else
                {
                    grdAmtVerifyornot.Visible = false;
                    pnlNoRecord.Visible = true;
                }
            }
            else
            {
                Errormsg(dt.TableName);
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("StInchargeCatalogue.aspx-0003", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
        }
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
            _common.ErrorLog("StInchargeCatalogue.aspx-0004", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
        }
    }
    #endregion

    #region "Events"
    protected void lnkbtnSearch_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (txtDepositDate.Text == "")
        {
            Errormsg("Please Enter Valid Date");
            return;
        }
        loadData(ddlstatus.SelectedValue);
    }
    protected void lnkbtnReset_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        txtDepositDate.Text = current_date;
        ddlstatus.SelectedValue = "3";
        loadData(ddlstatus.SelectedValue);
    }
    protected void grdAmtVerifyornot_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdAmtVerifyornot.PageIndex = e.NewPageIndex;
        loadData(ddlstatus.SelectedValue);
    }
    protected void grdAmtVerifyornot_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        CsrfTokenValidate();
        if (e.CommandName == "ViewDetails")
        {
            int rowIndex = int.Parse(e.CommandArgument.ToString());
            Session["RECEIPTNO_"] = grdAmtVerifyornot.DataKeys[rowIndex]["receipt_no"].ToString();
            Session["RECEIPTID_"] = grdAmtVerifyornot.DataKeys[rowIndex]["idd"].ToString();
            Session["CHESTID_"] = grdAmtVerifyornot.DataKeys[rowIndex]["chst_id"].ToString();
            getDepositDetails(Session["RECEIPTNO_"].ToString(), Session["RECEIPTID_"].ToString(), Session["CHESTID_"].ToString());
        }
        if (e.CommandName == "RejectAmt")
        {
            int rowIndex = int.Parse(e.CommandArgument.ToString());
            Session["RECEIPTNO_"] = grdAmtVerifyornot.DataKeys[rowIndex]["receipt_no"].ToString();
            Session["RECEIPTID_"] = grdAmtVerifyornot.DataKeys[rowIndex]["idd"].ToString();
            Session["CHESTID_"] = grdAmtVerifyornot.DataKeys[rowIndex]["chst_id"].ToString();
            Session["AmountAction"] = "R";
            lblConfirmation.Text = "Do you want to Reject Bank Deposit Amount ?";
            mpConfirmation.Show();
        }
        if (e.CommandName == "ApproveAmt")
        {
            int rowIndex = int.Parse(e.CommandArgument.ToString());
            Session["RECEIPTNO_"] = grdAmtVerifyornot.DataKeys[rowIndex]["receipt_no"].ToString();
            Session["RECEIPTID_"] = grdAmtVerifyornot.DataKeys[rowIndex]["idd"].ToString();
            Session["CHESTID_"] = grdAmtVerifyornot.DataKeys[rowIndex]["chst_id"].ToString();
            Session["AmountAction"] = "V";
            lblConfirmation.Text = "Do you want to Approved Bank Deposit Amount ?";
            mpConfirmation.Show();
        }
        if (e.CommandName == "viewReceipt")
        {
            int rowIndex = int.Parse(e.CommandArgument.ToString());
            Session["_DOC"] = grdAmtVerifyornot.DataKeys[rowIndex]["filebytes"];
            img.ImageUrl = GetImage(grdAmtVerifyornot.DataKeys[rowIndex]["filebytes"]);
            img.Visible = true;
            mpviewdocment.Show();
         //   GetDocumentID();
        }
    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        try
        {
            if (Session["AmountAction"].ToString() == "V" || Session["AmountAction"].ToString() == "R")
            {
                MyCommand = new NpgsqlCommand();
                MyCommand.Parameters.Clear();
                MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_deposit_amt_verify");
                MyCommand.Parameters.AddWithValue("p_receiptno", Session["RECEIPTNO_"].ToString());
                MyCommand.Parameters.AddWithValue("p_chestid", Session["CHESTID_"].ToString());
                MyCommand.Parameters.AddWithValue("p_id", Convert.ToInt64(Session["RECEIPTID_"].ToString()));
                MyCommand.Parameters.AddWithValue("p_action", Session["AmountAction"].ToString());
                MyCommand.Parameters.AddWithValue("p_updateby", Session["_UserCode"].ToString());
                dt = bll.SelectAll(MyCommand);
                if (dt.TableName == "Success")
                {
                    if (dt.Rows[0]["status"].ToString() == "DONE")
                    {
                        if (Session["AmountAction"].ToString() == "V")
                        {
                            Successmsg("Successfully Approved Deposit Amount");
                        }
                        if (Session["AmountAction"].ToString() == "R")
                        {
                            Successmsg("Successfully Reject Deposit Amount");
                        }
                        ddlstatus.SelectedValue = "3";
                        txtDepositDate.Text = current_date;
                        loadData(ddlstatus.SelectedValue);
                    }
                    else if (dt.Rows[0][""].ToString() == "ERROR")
                    {
                        Errormsg("Something Went Wrong");
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
            if (Session["AmountAction"].ToString() == "W")
            {
                approvedWaybillKm();
            }

        }
        catch (Exception ex)
        {
            _common.ErrorLog("StInchargeCatalogue.aspx-0005", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
        }
    }

    private void approvedWaybillKm()
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "etm.f_verify_waybillkm");
        MyCommand.Parameters.AddWithValue("p_dutyrefno", Session["dutyrefno"].ToString());
        MyCommand.Parameters.AddWithValue("p_updateby", Session["_UserCode"].ToString());
        MyCommand.Parameters.AddWithValue("p_officeid", Session["_LDepotCode"].ToString());
        dt = bll.SelectAll(MyCommand);
        if (dt.TableName == "Success")
        {
            if (dt.Rows[0]["result_"].ToString() == "DONE")
            {
                Successmsg("Waybill Km. Verified Successfully");
                loadWaybillKm();
                loadWaybillCount();
            }
            else if (dt.Rows[0][""].ToString() == "ERROR")
            {
                Errormsg("Something Went Wrong");
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

    protected void grdAmtVerifyornot_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            LinkButton btnapprove = (LinkButton)e.Row.FindControl("btnapprove");
            LinkButton btnreceipt = (LinkButton)e.Row.FindControl("btnViewReceipt");
            string receipt = grdAmtVerifyornot.DataKeys[e.Row.RowIndex].Values[4].ToString();
            string verifiedby = grdAmtVerifyornot.DataKeys[e.Row.RowIndex].Values[6].ToString();

            if (receipt == "")
            {
                btnreceipt.Text = "Not Available";
                btnreceipt.Enabled = false;
            }
            else
            {
                btnreceipt.Enabled = true;
            }

            if (verifiedby == "")
            {
                btnapprove.Visible = true;
            }
            else
            {
                btnapprove.Visible = false;
            }
        }
    }
    protected void Unnamed_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Errormsg("Coming Soon");
    }
    protected void lbtnDieselRefuel_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        //Errormsg("Coming Soon");
        Response.Redirect("StnlnchargeRefillingTankMgmmt.aspx");
    }

    protected void grdWaybillVerified_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdWaybillVerified.PageIndex = e.NewPageIndex;
        loadWaybillKm();
    }

    protected void grdWaybillVerified_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        CsrfTokenValidate();
        if (e.CommandName == "ApproveWaybill")
        {
            int rowIndex = int.Parse(e.CommandArgument.ToString());
            Session["dutyrefno"] = grdWaybillVerified.DataKeys[rowIndex]["dutyrefno_"].ToString();
            Session["AmountAction"] = "W";
            //Successmsg("Coming Soon");
            lblConfirmation.Text = "Do you want to Approve Actual Km ?";
            mpConfirmation.Show();

        }
    }
    protected void lbtnExcessPayment_Click1(object sender, EventArgs e)
    {
        Response.Redirect("stinExcesspaymentamount.aspx");
    }
    #endregion

    protected void ddlssioffice_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlssioffice.SelectedValue == "0")
        {
            pnlNoData.Visible = true;
            pnlData.Visible = false;
        }
        else
        {
            loadAmtCount();
            loadData(ddlstatus.SelectedValue);
            loadData(ddlstatus.SelectedValue);
            loadAmtCount();
            loadWaybillCount();
            loadWaybillKm();
            loadTDServicesummary();
            pnlNoData.Visible = false;
            pnlData.Visible = true;
        }
    }

    protected void lbtntargetdieselentry_Click(object sender, EventArgs e)
    {
        Response.Redirect("StInchargeBusServTargetDiesel.aspx");
    }
    protected void lbtnpremature_Click(object sender, EventArgs e)
    {
        
        eDepositedSlip.Text = "<embed src = \"STinprematurewaybill.aspx\" style=\"min-height: 70vh; width: 100%\" />";
        mpPrematureWaybillClosure.Show();
        //Response.Redirect("StInchargeBusServTargetDiesel.aspx");
    }

   


}