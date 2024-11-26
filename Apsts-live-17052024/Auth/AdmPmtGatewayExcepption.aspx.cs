using AjaxControlToolkit.HtmlEditor.Popups;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_AdmPmtGatewayExcepption : System.Web.UI.Page
{
    sbValidation _validation = new sbValidation();
    DataTable dt = new DataTable();
    private NpgsqlCommand MyCommand;
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    DataTable MyTable = new DataTable();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["_moduleName"] = "Transaction Exception Settlement";
        if (!IsPostBack)
        {
            if (Session["MPMTGATEWAYID"] != null && Session["MPMTGATEWAYID"].ToString() != "")
            {
                pendingException(Session["MPMTGATEWAYID"].ToString(), "", "0");
            }
            else
            {
                pendingException("0", "", "0");
            }
        }
    }

    #region Methods
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
    private void pendingException(string pgid, string transdate, string type)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "pg.fn_pendingExceptions");
            MyCommand.Parameters.AddWithValue("p_transdate", transdate);
            MyCommand.Parameters.AddWithValue("p_pmtgateway", Convert.ToInt32(pgid));
            MyCommand.Parameters.AddWithValue("p_excp_type", type);
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                gvExceptionCountList.Visible = true;
                dvNoExceptionCount.Visible = false;
                gvExceptionCountList.DataSource = dt;
                gvExceptionCountList.DataBind();
            }
            else
            {
                gvExceptionCountList.Visible = false;
                dvNoExceptionCount.Visible = true;
            }
        }
        catch (Exception ex)
        {

        }
    }
    public DataTable ExcpSattelment(int p_pgId, string p_ExcpType, string p_orderId, string p_ref_no, string p_status, string p_remark, string p_refund_AMT, string p_refund_REF_NO, string p_refundDate, string p_Updatedby)
    {
        DataTable dt = new DataTable();
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "pg.fnException_sattelment");
        MyCommand.Parameters.AddWithValue("p_pg_id", p_pgId);
        MyCommand.Parameters.AddWithValue("p_excp_type", p_ExcpType);
        MyCommand.Parameters.AddWithValue("p_orderid", p_orderId);
        MyCommand.Parameters.AddWithValue("p_ref_no", p_ref_no);
        MyCommand.Parameters.AddWithValue("p_status", p_status);
        MyCommand.Parameters.AddWithValue("p_remark", p_remark);
        MyCommand.Parameters.AddWithValue("p_refund_amt", p_refund_AMT);
        MyCommand.Parameters.AddWithValue("p_refund_ref_no", p_refund_REF_NO);
        MyCommand.Parameters.AddWithValue("p_refunddate", p_refundDate);
        MyCommand.Parameters.AddWithValue("p_updatedby", p_Updatedby);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }
    private string RefundTransactionValidation(string refundAmt, string refundRefNo, string refundDate)
    {
        string msg = string.Empty;
        double refund_amt;

        if (!double.TryParse(refundAmt, out refund_amt))
        {
            msg = msg + "Enter Valid Refund Amount.<br>";
        }

        if (string.IsNullOrWhiteSpace(refundRefNo))
        {
            msg = msg + "Enter Valid Refund Ref No.<br>";
        }

        if (string.IsNullOrWhiteSpace(refundDate))
        {
            msg = msg + "Enter Valid Refund Date.";
        }
        return msg;
    }
    private void Settlement()
    {
        try
        {
            int selectedIndex = int.Parse(hdgrdindex.Value);

            int p_pgId = int.Parse(gvExceptionSattel.DataKeys[selectedIndex]["pgid"].ToString());
            string p_ExcpType = gvExceptionSattel.DataKeys[selectedIndex]["exceptiontype"].ToString();
            string p_orderId = gvExceptionSattel.DataKeys[selectedIndex]["orderid"].ToString();
            string p_ref_no = gvExceptionSattel.DataKeys[selectedIndex]["txnrefno"].ToString();

            DropDownList ddl = gvExceptionSattel.Rows[selectedIndex].FindControl("ddlRefund") as DropDownList;
            TextBox tbRemark = gvExceptionSattel.Rows[selectedIndex].FindControl("tbRemark") as TextBox;
            TextBox tbRefundAmt = gvExceptionSattel.Rows[selectedIndex].FindControl("txtRefundamt") as TextBox;
            TextBox tbRefundDate = gvExceptionSattel.Rows[selectedIndex].FindControl("txtrefunddate") as TextBox;
            TextBox tbRefundRefNo = gvExceptionSattel.Rows[selectedIndex].FindControl("txtrefundrefno") as TextBox;

            string p_remark = "";
            string p_refund_AMT = "";
            string p_refund_REF_NO = "";
            string p_refundDate = "";

            string p_status = ddl.SelectedValue;
            if (p_status == "R")
            {
                p_remark = tbRemark.Text;
                p_refund_AMT = tbRefundAmt.Text;
                p_refund_REF_NO = tbRefundRefNo.Text;
                p_refundDate = tbRefundDate.Text;

                string msg = RefundTransactionValidation(p_refund_AMT, p_refund_REF_NO, p_refundDate);
                if (msg.Length > 0)
                {
                    Errormsg("Please Check, " + msg + "");
                    return;
                }
            }
            else
            {
                p_remark = tbRemark.Text;
                if (p_remark.Length < 5)
                {
                    Errormsg("Please Check, Enter valid remark (minimum 5 char).");
                    return;
                }
            }

            string p_Updatedby = "xyz";

            DataTable dt = ExcpSattelment(p_pgId, p_ExcpType, p_orderId, p_ref_no, p_status, p_remark, p_refund_AMT, p_refund_REF_NO, p_refundDate, p_Updatedby);

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["status"].ToString().ToUpper() == "DONE")
                {
                    Errormsg("Done, Transaction has been settled successfully.");
                    pendingExceptionDetails();
                }
                else
                {
                    Errormsg("Error, " + dt.TableName + "");
                }
            }
            else
            {
                Errormsg("Error, " + dt.TableName + "");
            }
        }
        catch (Exception ex)
        {
            Errormsg("Error, " + ex.ToString() + "");
        }
    }
    public void pendingExceptionDetails()
    {
        try
        {
            dvNoExceptionSattel.Visible = true;
            gvExceptionSattel.Visible = false;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "pg.fnpendingException_dtls");
            MyCommand.Parameters.AddWithValue("p_transdate", ViewState["_TXNDATE"].ToString());
            MyCommand.Parameters.AddWithValue("p_pmtgateway", int.Parse(ViewState["_PGID"].ToString()));
            MyCommand.Parameters.AddWithValue("p_excp_type", ViewState["_EXCPTYPE"].ToString());
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                gvExceptionSattel.Visible = true;
                dvNoExceptionCount.Visible = false;
                gvExceptionSattel.DataSource = dt;
                gvExceptionSattel.DataBind();
                dvNoExceptionSattel.Visible = false;
            }
            else
            {
                gvExceptionSattel.Visible = false;
                dvNoExceptionCount.Visible = true;
            }
        }
        catch (Exception ex)
        {

        }
    }
    #endregion

    #region Events
    protected void lbtnsearch_Click(object sender, EventArgs e)
    {
        pendingException(Session["MPMTGATEWAYID"].ToString(), "", ddlExcpType.SelectedValue.ToString());
    }
    protected void ddlRefund_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlRefund = (DropDownList)sender;

        GridViewRow gridrow = (GridViewRow)ddlRefund.NamingContainer;

        TextBox tbRemark = (TextBox)gridrow.FindControl("tbRemark");
        tbRemark.Text = "";
        TextBox txtRefundamt = (TextBox)gridrow.FindControl("txtRefundamt");
        TextBox txtrefunddate = (TextBox)gridrow.FindControl("txtrefunddate");
        TextBox txtrefundrefno = (TextBox)gridrow.FindControl("txtrefundrefno");
        txtrefunddate.Text = "";
        txtrefundrefno.Text = "";
        
        if (ddlRefund.SelectedValue == "R")
        {
            txtRefundamt.Visible = true;
            txtrefunddate.Visible = true;
            txtrefundrefno.Visible = true;
        }
        else
        {
            txtRefundamt.Visible = false;
            txtrefunddate.Visible = false;
            txtrefundrefno.Visible = false;
        }
    }
    protected void lbtnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("sysAdmPmtGateway.aspx");
    }
    protected void gvExceptionCountList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "VIEWDETAIL")
        {
            string txndate = gvExceptionCountList.DataKeys[index]["txnDate"].ToString();
            string pgID = gvExceptionCountList.DataKeys[index]["pgid"].ToString();
            string pgName = gvExceptionCountList.DataKeys[index]["gatewayName"].ToString();
            lblExcDtlHeader.Text = "Transactions For - Date <b>" + txndate + "</b>, Payment Gateway <b>" + pgName + "</b>, Exception Types <b>" + ddlExcpType.SelectedItem.ToString() + "</b>";

            ViewState["_PGID"] = pgID;
            ViewState["_TXNDATE"] = txndate;
            ViewState["_EXCPTYPE"] = ddlExcpType.SelectedValue;
            pendingExceptionDetails();
        }
    }
    protected void gvExceptionSattel_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "VERIFY")
        {
            string pgID = gvExceptionSattel.DataKeys[index]["pgid"].ToString();
            string pgName = gvExceptionSattel.DataKeys[index]["gatewayname"].ToString();
            string ORDER_ID = gvExceptionSattel.DataKeys[index]["orderid"].ToString();

            DropDownList ddl = gvExceptionSattel.Rows[index].FindControl("ddlRefund") as DropDownList;
            TextBox tbRemark = gvExceptionSattel.Rows[index].FindControl("tbRemark") as TextBox;
            TextBox tbRefundAmt = gvExceptionSattel.Rows[index].FindControl("txtRefundamt") as TextBox;
            TextBox tbRefundDate = gvExceptionSattel.Rows[index].FindControl("txtrefunddate") as TextBox;
            TextBox tbRefundRefNo = gvExceptionSattel.Rows[index].FindControl("txtrefundrefno") as TextBox;

            string p_remark = "";
            string p_refund_AMT = "";
            string p_refund_REF_NO = "";
            string p_refundDate = "";

            string p_status = ddl.SelectedValue;
            if (p_status == "R")
            {
                p_remark = tbRemark.Text;
                p_refund_AMT = tbRefundAmt.Text;
                p_refund_REF_NO = tbRefundRefNo.Text;
                p_refundDate = tbRefundDate.Text;
                string msg = RefundTransactionValidation(p_refund_AMT, p_refund_REF_NO, p_refundDate);
                if (msg.Length > 0)
                {
                    Errormsg("Please Check: " + msg + "");
                    return;
                }
            }
            else
            {
                p_remark = tbRemark.Text;
                if (p_remark.Length < 5)
                {
                    Errormsg("Please Check\", \"Enter valid remark (minimum 5 char).");
                    return;
                }
            }

            hdgrdindex.Value = index.ToString();
            lblConfirmation.Text = "Order Id : " + ORDER_ID + "<br>Do you want to settle transactions?";
            mpConfirmationsss.Show();
        }
    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        Settlement();
    }
    protected void gvExceptionSattel_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtRefundamt = (TextBox)e.Row.FindControl("txtRefundamt");
            TextBox txtrefunddate = (TextBox)e.Row.FindControl("txtrefunddate");
            TextBox txtrefundrefno = (TextBox)e.Row.FindControl("txtrefundrefno");
            DataRowView rowView = (DataRowView)e.Row.DataItem;
            txtRefundamt.Visible = false;
            txtrefunddate.Visible = false;
            txtrefundrefno.Visible = false;
            if (rowView["pgstatus"].ToString().ToUpper() == "R".ToUpper())
            {
                txtRefundamt.Visible = true;
                txtrefunddate.Visible = true;
                txtrefundrefno.Visible = true;
                txtRefundamt.Enabled = false;
                txtrefunddate.Enabled = false;
                txtrefundrefno.Enabled = false;
            }
        }
    }
    #endregion

}