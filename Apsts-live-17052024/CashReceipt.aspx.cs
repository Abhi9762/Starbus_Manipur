using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class CashReceipt : System.Web.UI.Page
{
    sbSecurity _security = new sbSecurity();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    string html = "";
    protected void Page_Load(object sender, EventArgs e)//M1
    {
        //Session["TransRefno"] = "C31102023_16";

        if (Session["TransRefno"] != null || Session["TransRefno"] != "")
        {
            LoadCashReceipt();
            loadXml();
        }
        else
        {
            details.Visible = true;
            pnlerror.Visible = false;
        }
    }

    private void loadXml()
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(Server.MapPath("CommonData.xml"));
        XmlNodeList deptname = doc.GetElementsByTagName("dept_Name_en");
        lbldepart.Text = deptname.Item(0).InnerXml;
        lbldepart1.Text = deptname.Item(0).InnerXml;
    }

    private void LoadCashReceipt()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_cash_receipt");
            MyCommand.Parameters.AddWithValue("p_transrefno", Session["TransRefno"].ToString());
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    details.Visible = true;
                    pnlerror.Visible = false;
                    lblTransactionNo.Text = dt.Rows[0]["txn_id"].ToString();
                    lblTransactionNo1.Text = dt.Rows[0]["txn_id"].ToString();
                    lbltransdate.Text = dt.Rows[0]["txn_datetime"].ToString();
                    lbltransdate1.Text = dt.Rows[0]["txn_datetime"].ToString();
                    lbloffice.Text = dt.Rows[0]["office_name"].ToString() + " (" + dt.Rows[0]["ofclvlname"].ToString() + ")";
                    lbloffice1.Text = dt.Rows[0]["office_name"].ToString() + " (" + dt.Rows[0]["ofclvlname"].ToString() + ")";
                    // lblhead.Text = dt.Rows[0]["HEADNAME")
                    if (dt.Rows[0]["head_id"].ToString() == "2" && dt.Rows[0]["subhead_id"].ToString() == "1" || dt.Rows[0]["subhead_id"].ToString() == "2")
                    {
                        lblIncomeSource.Text = "Income Source";
                        lblsubhead.Text = dt.Rows[0]["sub_head_name"].ToString();
                        lblIncomeSource1.Text = "Income Source";
                        lblsubhead1.Text = dt.Rows[0]["sub_head_name"].ToString();
                    }
                    else
                    {
                        if (dt.Rows[0]["subhead_id"].ToString() == "4")
                        {
                            lblIncomeSource.Text = "Income Source <br/> Old Receipt Number";
                            lblsubhead.Text = dt.Rows[0]["sub_head_name"].ToString() + "<br/>" + dt.Rows[0]["OLDRECEIPTNO"].ToString();
                            lblIncomeSource1.Text = "Income Source <br/> Old Receipt Number";
                            lblsubhead1.Text = dt.Rows[0]["sub_head_name"].ToString() + "<br/>" + dt.Rows[0]["OLDRECEIPTNO"].ToString();
                        }
                        if (dt.Rows[0]["subhead_id"].ToString() == "5")
                        {
                            lblIncomeSource.Text = "Income Source <br/> Maxi Cab";
                            lblsubhead.Text = dt.Rows[0]["sub_head_name"].ToString() + "<br/>" + dt.Rows[0]["MAXICAB"].ToString();
                            lblIncomeSource1.Text = "Income Source <br/> Maxi Cab";
                            lblsubhead1.Text = dt.Rows[0]["sub_head_name"].ToString() + "<br/>" + dt.Rows[0]["MAXICAB"].ToString();
                        }
                        if (dt.Rows[0]["subhead_id"].ToString() == "6" || dt.Rows[0]["subhead_id"].ToString() == "8")
                        {
                            lblIncomeSource.Text = "Income Source <br/> Bus Number <br/>Waybill Number";
                            lblsubhead.Text = dt.Rows[0]["sub_head_name"].ToString() + "<br/>" + dt.Rows[0]["BUSNUMBER"].ToString() + "<br/>" + dt.Rows[0]["WAYBILLNO"].ToString();
                            lblIncomeSource1.Text = "Income Source <br/> Bus Number <br/>Waybill Number";
                            lblsubhead1.Text = dt.Rows[0]["sub_head_name"].ToString() + "<br/>" + dt.Rows[0]["BUSNUMBER"].ToString() + "<br/>" + dt.Rows[0]["WAYBILLNO"].ToString();
                        }
                        if (dt.Rows[0]["subhead_id"].ToString() == "3")
                        {
                            lblIncomeSource.Text = "Income Source <br/> Bus Number";
                            lblsubhead.Text = dt.Rows[0]["sub_head_name"].ToString() + "<br/>" + dt.Rows[0]["BUSNUMBER"].ToString();
                            lblIncomeSource1.Text = "Income Source <br/> Bus Number";
                            lblsubhead1.Text = dt.Rows[0]["sub_head_name"].ToString() + "<br/>" + dt.Rows[0]["BUSNUMBER"].ToString();
                        }
                        if (dt.Rows[0]["subhead_id"].ToString() == "12")
                        {
                            lblIncomeSource.Text = "Income Source <br/> ID Proof";
                            lblsubhead.Text = dt.Rows[0]["sub_head_name"].ToString() + "<br/>" + dt.Rows[0]["IDNAME"].ToString() + "(" + dt.Rows[0]["IDPROOFNO"].ToString() + ")";
                            lblIncomeSource1.Text = "Income Source <br/> ID Proof";
                            lblsubhead1.Text = dt.Rows[0]["sub_head_name"].ToString() + "<br/>" + dt.Rows[0]["IDNAME"].ToString() + "(" + dt.Rows[0]["IDPROOFNO"].ToString() + ")";
                        }
                        if (dt.Rows[0]["subhead_id"].ToString() == "14")
                        {

                            lblIncomeSource.Text = "Income Source <br/> Bus Number <br/>Waybill Number";
                            lblsubhead.Text = dt.Rows[0]["sub_head_name"].ToString() + "<br/>" + dt.Rows[0]["BUSNUMBER"].ToString() + "<br/>" + dt.Rows[0]["WAYBILLNO"].ToString();
                            lblIncomeSource1.Text = "Income Source <br/> Bus Number <br/>Waybill Number";
                            lblsubhead1.Text = dt.Rows[0]["sub_head_name"].ToString() + "<br/>" + dt.Rows[0]["BUSNUMBER"].ToString() + "<br/>" + dt.Rows[0]["WAYBILLNO"].ToString();
                        }
                    }
                    lbldepositamt.Text = dt.Rows[0]["amount"].ToString();
                    lbldepositamt1.Text = dt.Rows[0]["amount"].ToString();
                    lbldepositby.Text = dt.Rows[0]["credit_by"].ToString() + "<br/>" + dt.Rows[0]["mobileno"].ToString();
                    lbldepositby1.Text = dt.Rows[0]["credit_by"].ToString() + "<br/>" + dt.Rows[0]["mobileno"].ToString();
                    lblprintby.Text = dt.Rows[0]["emp_name"].ToString();
                    lblprintby1.Text = dt.Rows[0]["emp_name"].ToString();
                    lblprintdatetime.Text = DateTime.Now.ToString();
                    lblprintdatetime1.Text = DateTime.Now.ToString();
                }
                else
                {
                    details.Visible = true;
                    pnlerror.Visible = false;
                }
            }
            else
            {
                details.Visible = true;
                pnlerror.Visible = false;
            }
        }
        catch (Exception ex)
        {
            details.Visible = true;
            pnlerror.Visible = false;
        }
    }
}