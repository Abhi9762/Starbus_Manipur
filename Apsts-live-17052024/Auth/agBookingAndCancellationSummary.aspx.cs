using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_agBookingAndCancellationSummary_aspx : System.Web.UI.Page
{
    sbSecurity _security = new sbSecurity();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
        tbfromdate.Text = DateTime.Now.Date.AddDays(-10).ToString("dd/MM/yyyy");
        tbtodate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
        grdmsg.Text = "Select Date For Search Records";
        }
    }

    private void ShowTransactionDetails(string agcode, string fdate, string tdate)
    {
        try
        {
            DataTable dt;
            DataTable myDataTable = new DataTable("TransactionDetails");
            myDataTable.Columns.Add("TotBooking", typeof(int));
            myDataTable.Columns.Add("TotBookingAmt", typeof(decimal));
            myDataTable.Columns.Add("TotCommissionAmt", typeof(decimal));
            myDataTable.Columns.Add("TotTaxAmt", typeof(decimal));
            myDataTable.Columns.Add("TotCancelAmt", typeof(decimal));
            myDataTable.Columns.Add("TransDate", typeof(string));
            myDataTable.Columns.Add("PassAmt", typeof(decimal));
            myDataTable.Columns.Add("PassTaxAmt", typeof(decimal));

            DateTime dtFrom = DateTime.ParseExact(fdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime dtTo = DateTime.ParseExact(tdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            while (dtFrom <= dtTo)
            {
                string dtTrans = dtFrom.ToString("dd/MM/yyyy");
                MyCommand = new NpgsqlCommand();
                MyCommand.Parameters.Clear();
                MyCommand.Parameters.Clear();
                MyCommand.Parameters.AddWithValue("@StoredProcedure", "agent.f_get_transactiondetailsforagent");
                MyCommand.Parameters.AddWithValue("p_usercode", "0");
                MyCommand.Parameters.AddWithValue("p_agentcode", agcode);
                MyCommand.Parameters.AddWithValue("p_datefrom", dtTrans);
                dt = bll.SelectAll(MyCommand);

                if (dt.TableName == "Success")
                {
                    if (dt.Rows.Count > 0)
                    {
                        if ((decimal)dt.Rows[0]["val_bookingamt"] != 0 || (decimal)dt.Rows[0]["val_cancelamt"] != 0)
                        {
                            myDataTable.Rows.Add(
                                dt.Rows[0]["val_booking"],
                                dt.Rows[0]["val_bookingamt"],
                                dt.Rows[0]["val_commission"],
                                dt.Rows[0]["val_taxamt"],
                                dt.Rows[0]["val_cancelamt"],
                                dtFrom.ToShortDateString(),
                                dt.Rows[0]["val_passamt"],
                                dt.Rows[0]["val_passtaxamt"]
                            );
                        }
                    }
                }

                dtFrom = dtFrom.AddDays(1);
            }

            if (myDataTable.Rows.Count > 0)
            {
                grdtransactionDetails.DataSource = myDataTable;
                grdtransactionDetails.DataBind();
                grdtransactionDetails.UseAccessibleHeader = true;
                grdtransactionDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
                grdmsg.Visible = false;
                grdtransactionDetails.Visible = true;
            }
            else
            {
                grdmsg.Visible = true;
                grdtransactionDetails.Visible = false;
            }
        }
        catch (Exception ex)
        {
            grdmsg.Visible = true;
            grdtransactionDetails.Visible = false;
        }
    }

    protected void grdtransactionDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.CssClass = "grRowStyle";
        }
    }

    protected void grdtransactionDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdtransactionDetails.PageIndex = e.NewPageIndex;
        ShowTransactionDetails(Session["agcode"].ToString(), tbfromdate.Text, tbtodate.Text);
    }

    protected void lbtnsearch_Click(object sender, EventArgs e)
    {
        ShowTransactionDetails(Session["_UserCode"].ToString(), tbfromdate.Text, tbtodate.Text);
    }


}