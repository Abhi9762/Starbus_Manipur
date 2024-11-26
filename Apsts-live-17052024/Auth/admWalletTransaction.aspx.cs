using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Npgsql;
using System.Data;
using System.IO;
using System.Drawing;

public partial class Auth_admWalletTransaction : System.Web.UI.Page
{
    private NpgsqlCommand MyCommand;
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    private sbBLL bll = new sbBLL();
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["_moduleName"] = "Wallet Transactions";
        if (!IsPostBack)
        {
            lblstakHolder.Text = "Traveller User Id";
        }

    }

    #region Methods
    private void Errormsg(string msg)
    {

        string popup = _popup.modalPopupSmall("D", "Please Check", msg, "Close");
        Response.Write(popup);
    }
    private void loadwallettopuptransaction()
    {
        try
        {
            string ptype = "";
            if (rbtnAgentWallet.Checked == true)
            {
                ptype = "A";
            }
            if (rbtnTravellerWallet.Checked == true)
            {
                ptype = "T";
            }
            pnlNowallettopup.Visible = true;
            gvwallettopup.Visible = false;
            lbtndownloadWallettopup.Visible = false;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.fn_get_topup_txns");
            MyCommand.Parameters.AddWithValue("p_type", ptype);
            MyCommand.Parameters.AddWithValue("p_fromdate", txtFromDate.Text);
            MyCommand.Parameters.AddWithValue("p_todate", txttodate.Text);
            MyCommand.Parameters.AddWithValue("p_userid", txtWalletRefNo.Text);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    lblwallettopuptrns.Text = "Wallet Topup Transaction <br/> " + dt.Rows[0]["username_"].ToString() + "(" + txtWalletRefNo.Text + ") <br/> Current Balance <b> " + dt.Rows[0]["currntbal"].ToString() + " ₹ </b>";
                    gvwallettopup.DataSource = dt;
                    gvwallettopup.DataBind();
                    gvwallettopup.Visible = true;
                    pnlNowallettopup.Visible = false;
                    lbtndownloadWallettopup.Visible = true;
                }
                else
                {
                    Errormsg("Wallet Topup Details Not Available");
                }
            }
            else
            {
                Errormsg("Wallet Topup Details Not Available");
            }
        }
        catch
        {
            Errormsg("Wallet Topup Details Not Available");
        }
    }
    private void loadwallettransaction()
    {
        string ptype = "";
        if (rbtnAgentWallet.Checked == true)
        {
            ptype = "A";
        }
        if (rbtnTravellerWallet.Checked == true)
        {
            ptype = "T";
        }
        pnlNowallettransaction.Visible = true;
        gvwallettransaction.Visible = false;
        lbtndownloadWallettransaction.Visible = false;
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.fn_get_wallet_txns");
        MyCommand.Parameters.AddWithValue("p_type", ptype);
        MyCommand.Parameters.AddWithValue("p_fromdate", txtFromDate.Text);
        MyCommand.Parameters.AddWithValue("p_todate", txttodate.Text);
        MyCommand.Parameters.AddWithValue("p_userid", txtWalletRefNo.Text);
        dt = bll.SelectAll(MyCommand);
        if (dt.TableName == "Success")
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    lblwallettrns.Text = "Wallet Transaction <br/> " + dt.Rows[0]["user_name_"].ToString() + "(" + txtWalletRefNo.Text + ") <br/> Current Balance <b> " + dt.Rows[0]["currntbal"].ToString() + " ₹ </b>";
                    gvwallettransaction.DataSource = dt;
                    gvwallettransaction.DataBind(); ;
                    gvwallettransaction.Visible = true;
                    pnlNowallettransaction.Visible = false;
                    lbtndownloadWallettransaction.Visible = true;
                }
                else
                {
                    Errormsg("Wallet Topup Details Not Available");
                }
            }
            else
            {
                Errormsg("Wallet Topup Details Not Available");
            }
    }
    private void resetctrl()
    {
        pnlNowallettopup.Visible = true;
        gvwallettopup.Visible = false;
        lbtndownloadWallettopup.Visible = false;
        pnlNowallettransaction.Visible = true;
        gvwallettransaction.Visible = false;
        lbtndownloadWallettransaction.Visible = false;
        txtFromDate.Text = "";
        txttodate.Text = "";
        txtWalletRefNo.Text = "";
        lblwallettopuptrns.Text = "Wallet Topup Transaction ";
        lblwallettrns.Text = "Wallet Transaction ";
    }
    private void ExportGridToExcel(string type, GridView gv)
    {
        string filename = "";
        if (type == "TP")
        {
            filename = "WalletTopDetails";
        }
        if (type == "T")
        {
            filename = "WalletTransactionDetails";
        }

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename="+filename+ ".xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            // To Export all pages
            gv.AllowPaging = false;
            if (type == "TP")
            {
                this.loadwallettopuptransaction();
            }
            if (type == "T")
            {
                this.loadwallettransaction();
            }
            gv.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in gv.HeaderRow.Cells)
                cell.BackColor = gv.HeaderStyle.BackColor;
            foreach (GridViewRow row in gv.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                        cell.BackColor = gv.AlternatingRowStyle.BackColor;
                    else
                        cell.BackColor = gv.RowStyle.BackColor;
                    cell.CssClass = "Textmode";
                }
            }

            gv.RenderControl(hw);
            // style to format numbers to string
            string style = "<style> .Textmode { } </style>";
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
    protected void rbtnTravellerWallet_CheckedChanged(object sender, EventArgs e)
    {
        if (rbtnTravellerWallet.Checked == true)
        {
            lblstakHolder.Text = "Traveller User Id";
        }
    }
    protected void rbtnAgentWallet_CheckedChanged(object sender, EventArgs e)
    {
        if (rbtnAgentWallet.Checked == true)
        {
            lblstakHolder.Text = "Agent/CSC Code";
        }
    }
    protected void lbtnsearch_Click(object sender, EventArgs e)
    {
        if (txtWalletRefNo.Text == "")
        {
            Errormsg("Please Enter Wallet Refrence No.");
            return;
        }
        loadwallettransaction();
        loadwallettopuptransaction();
    }
    protected void lbtnReset_Click(object sender, EventArgs e)
    {
        resetctrl();
    }
    protected void lbtndownloadWallettopup_Click(object sender, EventArgs e)
    {
        ExportGridToExcel("TP", gvwallettopup);
    }
    protected void gvwallettopup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvwallettopup.PageIndex = e.NewPageIndex;
        loadwallettopuptransaction();
    }
    protected void lbtndownloadWallettransaction_Click(object sender, EventArgs e)
    {
        ExportGridToExcel("T", gvwallettransaction);
    }
    protected void gvwallettransaction_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvwallettransaction.PageIndex = e.NewPageIndex;
        loadwallettransaction();
    }
    #endregion
}