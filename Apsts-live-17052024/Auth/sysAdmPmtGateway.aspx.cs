using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_sysAdmPmtGateway : BasePage
{
    private NpgsqlCommand MyCommand;
    sbValidation _SecurityCheck = new sbValidation();
    sbSecurity _security = new sbSecurity();
    HDFC hdfc = new HDFC();
    sbValidation _validation = new sbValidation();
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    ClassDoubleVerification_Refund obj = new ClassDoubleVerification_Refund();
    string current_date = DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("yyyy");
    string current_date_1 = DateTime.Now.AddDays(-1).ToString("dd") + "/" + DateTime.Now.AddDays(-1).ToString("MM") + "/" + DateTime.Now.AddDays(-1).ToString("yyyy");

    protected void Page_Load(object sender, EventArgs e)
    {
        checkForSecurity();
        Session["_moduleName"] = "Payment Gateway MIS";
        if (!IsPostBack)
        {
            //  txtsummarydate.Text = current_date;
            // txtscrolldate.Text = current_date_1;
            loadPG();
            loadOrphanTxn();
            loadRefundTxn();
            loadRefundInitiatedTxn();
            loadExceptionCount();
        }
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
            Response.Redirect("sessionTimeout.aspx");
        }
        if (_security.isSessionExist(Session["_RNDIDENTIFIERSADM"]) == true)
        {
            Session["_RNDIDENTIFIERSADM"] = Session["_RNDIDENTIFIERSADM"];
        }
        else
        {
            Response.Redirect("sessionTimeout.aspx");
        }
        if (_security.checkvalidation() == false)
        {
            Response.Redirect("Errorpage.aspx");
        }
        if (_security.CheckOldUserLogin(Session["_UserCode"].ToString()) == false)
        {
            Response.Redirect("Errorpage.aspx");
        }
    }
    private void Successmsg(string msg)
    {
        string popup = _popup.modalPopupSmall("S", "Success", msg, "Close");
        Response.Write(popup);
    }
    private void Errormsg(string msg)
    {
        string popup = _popup.modalPopupSmall("D", "Please Check", msg, "Close");
        Response.Write(popup);
    }

    #region "Methods"
    private void loadPG()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = hdfc.getactiveAll_PG();
            if (dt.Rows.Count > 0)
            {
                ddlpmtgatewayorphan.DataSource = dt;
                ddlpmtgatewayorphan.DataTextField = "gateway_name";
                ddlpmtgatewayorphan.DataValueField = "gateway_id";
                ddlpmtgatewayorphan.DataBind();
                ddlpmtgatewayorphan.Items.Insert(0, "All");
                ddlpmtgatewayorphan.Items[0].Value = "0";
                ddlpmtgatewayorphan.SelectedIndex = 0;

                ddlpmtgatewayrefund.DataSource = dt;
                ddlpmtgatewayrefund.DataTextField = "gateway_name";
                ddlpmtgatewayrefund.DataValueField = "gateway_id";
                ddlpmtgatewayrefund.DataBind();
                ddlpmtgatewayrefund.Items.Insert(0, "All");
                ddlpmtgatewayrefund.Items[0].Value = "0";
                ddlpmtgatewayrefund.SelectedIndex = 0;

                ddlpmtgatewayrefunded.DataSource = dt;
                ddlpmtgatewayrefunded.DataTextField = "gateway_name";
                ddlpmtgatewayrefunded.DataValueField = "gateway_id";
                ddlpmtgatewayrefunded.DataBind();
                ddlpmtgatewayrefunded.Items.Insert(0, "All");
                ddlpmtgatewayrefunded.Items[0].Value = "0";
                ddlpmtgatewayrefunded.SelectedIndex = 0;


                ddlpayment.DataSource = dt;
                ddlpayment.DataTextField = "gateway_name";
                ddlpayment.DataValueField = "gateway_id";
                ddlpayment.DataBind();
                ddlpayment.Items.Insert(0, "SELECT");
                ddlpayment.Items[0].Value = "0";
                ddlpayment.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ddlpmtgatewayorphan.Items.Insert(0, "SELECT");
            ddlpmtgatewayorphan.Items[0].Value = "0";
            ddlpmtgatewayorphan.SelectedIndex = 0;

            ddlpmtgatewayrefund.Items.Insert(0, "SELECT");
            ddlpmtgatewayrefund.Items[0].Value = "0";
            ddlpmtgatewayrefund.SelectedIndex = 0;

            ddlpayment.Items.Insert(0, "SELECT");
            ddlpayment.Items[0].Value = "0";
            ddlpayment.SelectedIndex = 0;
        }
    }
    private void loadExceptionCount()
    {
        try
        {
            gvPGCounts.Visible = false;
            dt = obj.ExceptionCounts();
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    gvPGCounts.DataSource = dt;
                    gvPGCounts.DataBind();
                    gvPGCounts.Visible = false;
                    gvPGCounts.Visible = true;
                }
                else
                {

                    gvPGCounts.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            Errormsg("Something Went Wrong");
        }
    }
    private void loadOrphanTxn()
    {
        try
        {
            dvOrphanTrans.Visible = true;
            gvOrphanTrans.Visible = false;
            dt = obj.getOrphanTransactionCount(ddlpmtgatewayorphan.SelectedValue.ToString(), txttransdateOrphan.Text);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    gvOrphanTrans.DataSource = dt;
                    gvOrphanTrans.DataBind();
                    dvOrphanTrans.Visible = false;
                    gvOrphanTrans.Visible = true;
                }
                else
                {
                    dvOrphanTrans.Visible = true;
                    gvOrphanTrans.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            Errormsg("Something Went Wrong");
        }
    }
    private void loadRefundTxn()
    {
        try
        {
            dvRefundTrans.Visible = true;
            gvRefundTrans.Visible = false;

            dt = obj.getRefundTxnCount(ddlpmtgatewayrefund.SelectedValue.ToString(), txttransdateRefund.Text);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    gvRefundTrans.DataSource = dt;
                    gvRefundTrans.DataBind();
                    dvRefundTrans.Visible = false;
                    gvRefundTrans.Visible = true;
                }
                else
                {
                    dvRefundTrans.Visible = true;
                    gvRefundTrans.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            Errormsg("Something Went Wrong");
        }
    }
    private void loadRefundInitiatedTxn()
    {
        try
        {
            dvRefundedTrans.Visible = true;
            gvRefundedTrans.Visible = false;

            dt = obj.getRefundedTxnCount(ddlpmtgatewayrefunded.SelectedValue.ToString(), txttransdateRefunded.Text);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    gvRefundedTrans.DataSource = dt;
                    gvRefundedTrans.DataBind();
                    dvRefundedTrans.Visible = false;
                    gvRefundedTrans.Visible = true;
                }
                else
                {
                    dvRefundedTrans.Visible = true;
                    gvRefundedTrans.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            Errormsg("Something Went Wrong");
        }
    }
    #endregion

    #region "Events"
    protected void lbtnsearchOrphan_Click(object sender, EventArgs e)
    {
        loadOrphanTxn();
    }
    protected void lbtnResetOrphan_Click(object sender, EventArgs e)
    {
        txttransdateOrphan.Text = "";
        ddlpmtgatewayorphan.SelectedValue = "0";
        loadOrphanTxn();
    }
    protected void gvOrphanTrans_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvOrphanTrans.PageIndex = e.NewPageIndex;
        loadOrphanTxn();
    }
    protected void gvOrphanTrans_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Settle")
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int i = row.RowIndex;
            Session["MPMTGATEWAYID"] = gvOrphanTrans.DataKeys[i]["gatewayid"].ToString();
            Session["Mtxndate"] = gvOrphanTrans.DataKeys[i]["txndate"].ToString();
            Session["MPMTGATEWAYNAME"] = gvOrphanTrans.DataKeys[i]["gateway"].ToString();
            Response.Redirect("sysAdmOrphanPmtsQry.aspx");
        }
    }
    protected void lbtnsearchRefund_Click(object sender, EventArgs e)
    {
        loadRefundTxn();
    }
    protected void lbtnResetRefund_Click(object sender, EventArgs e)
    {
        txttransdateRefund.Text = "";
        ddlpmtgatewayrefund.SelectedValue = "0";
        loadRefundTxn();
    }
    protected void gvRefundTrans_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvRefundTrans.PageIndex = e.NewPageIndex;
        loadRefundTxn();
    }
    protected void gvRefundTrans_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Refund")
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int i = row.RowIndex;
            Session["MPMTGATEWAYID"] = gvRefundTrans.DataKeys[i]["gatewayid"].ToString();
            Session["Mtxndate"] = gvRefundTrans.DataKeys[i]["txndate"].ToString();
            Session["MPMTGATEWAYNAME"] = gvRefundTrans.DataKeys[i]["gateway"].ToString();
            Response.Redirect("sysAdmRefundPmtsQry.aspx");
        }
    }
    protected void lbtnSearchRefunded_Click(object sender, EventArgs e)
    {
        loadRefundInitiatedTxn();
    }
    protected void lbtnResetRefunded_Click(object sender, EventArgs e)
    {
        txttransdateRefunded.Text = "";
        ddlpmtgatewayrefunded.SelectedValue = "0";
        loadRefundInitiatedTxn();
    }
    protected void gvRefundedTrans_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvRefundedTrans.PageIndex = e.NewPageIndex;
        loadRefundInitiatedTxn();
    }
    protected void gvRefundedTrans_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "RefundStatus")
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int i = row.RowIndex;
            Session["MPMTGATEWAYID"] = gvRefundedTrans.DataKeys[i]["payment_gateway_id"].ToString();
            Session["Mtxndate"] = gvRefundedTrans.DataKeys[i]["txndate"].ToString();
            Session["MPMTGATEWAYNAME"] = gvRefundedTrans.DataKeys[i]["pmtgatewayname"].ToString();
            Response.Redirect("sysAdmRefundStatusPmtsQry.aspx");
        }
    }
    protected void lbtndownload_Click(object sender, EventArgs e)
    {
        Errormsg("Coming Soon");
    }
    protected void gvPGCounts_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "COUNTVIEW")
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int i = row.RowIndex;
            Session["MPMTGATEWAYID"] = gvPGCounts.DataKeys[i]["gateway_id"].ToString();
            Session["MPMTGATEWAYNAME"] = gvPGCounts.DataKeys[i]["gatewayname"].ToString();
            Response.Redirect("AdmPmtGatewayExcepption.aspx");
        }
    }
    #endregion
}