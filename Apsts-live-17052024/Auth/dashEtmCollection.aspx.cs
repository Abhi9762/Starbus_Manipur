using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class Auth_dashEtmCollection : System.Web.UI.Page
{
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {

            LoadWaybillDetails();
            loadGeneralData();
        }
    }

    protected void LoadWaybillDetails()
    {
        try
        {
           // gvServiceTrips.Visible = false;
            string waybillid = Session["DUTYREFNO"].ToString();

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_dutytripforetmsubmission");
            MyCommand.Parameters.AddWithValue("p_waybillno", waybillid);
            dt = bll.SelectAll(MyCommand);

            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    tbWaybillNo.Text = dt.Rows[0]["DUTYREFNO"].ToString();
                    lblServiceName.Text = dt.Rows[0]["service_name"].ToString();
                    lblBusNo.Text = dt.Rows[0]["BUSNO"].ToString();
                    lblETMNo.Text = dt.Rows[0]["etmserialno"].ToString();
                    lblDriver.Text = dt.Rows[0]["DRIVER"].ToString();
                    lblConductor.Text = dt.Rows[0]["CONDUCTOR"].ToString();
                    
                    loadAmountFields(waybillid);
                    loadWaybillTrips(tbWaybillNo.Text);
                }
            }
        }
        catch (Exception ex)
        {
            //Errormsg(ex.Message);
        }
    }
    private void loadAmountFields(string waybill)
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "etm.f_get_validate_waybill_details");
        MyCommand.Parameters.AddWithValue("p_waybill_no", waybill);
        dt = bll.SelectAll(MyCommand);

        if (dt.TableName == "Success")
        {
            if (dt.Rows.Count > 0)
            {
                tbOnlineTktamt.Text = dt.Rows[0]["online_ticket_amount"].ToString();
                tbEnrouteTktamt.Text = dt.Rows[0]["enroute_amount"].ToString();
                tbDhabaAmt.Text = dt.Rows[0]["dhaba_amount"].ToString();
                tbOtherEarningAmt.Text = dt.Rows[0]["extra_earning"].ToString();
                tbTotalEarningAmt.Text = dt.Rows[0]["total_earning"].ToString();
                tbLuggageAmt.Text = dt.Rows[0]["luggage_amount"].ToString();
                tbTollpaid.Text = dt.Rows[0]["toll_amount"].ToString();
                tbParking.Text = dt.Rows[0]["parking_amount"].ToString();
                tbOtherExp.Text = dt.Rows[0]["other_exp_amount"].ToString();
                tbTotalExpenses.Text = dt.Rows[0]["total_expense"].ToString();
                decimal amt = Convert.ToDecimal(tbTotalEarningAmt.Text) - Convert.ToDecimal(tbTotalExpenses.Text);
                lblDepositedAmt.Text = "Amount To Be Deposited By Conductor ₹" + amt.ToString();
            }
        }
    }
    private void loadWaybillTrips(string waybill)
    {
        gvServiceTrips.Visible = false;
        pnlNoRecordTrip.Visible = false;
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "etm.f_get_waybilltrips_info");
        MyCommand.Parameters.AddWithValue("p_waybill_no", waybill);
        dt = bll.SelectAll(MyCommand);

        if (dt.TableName == "Success")
        {
            if (dt.Rows.Count > 0)
            {
                gvServiceTrips.DataSource = dt;
                gvServiceTrips.DataBind();
                gvServiceTrips.Visible = true;
                pnlNoRecordTrip.Visible = false;
            }
            else
            {
                gvServiceTrips.Visible = false;
                pnlNoRecordTrip.Visible = true;
            }
        }
    }
    private void loadGeneralData()
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(Server.MapPath("../CommonData.xml"));
        XmlNodeList deptname = doc.GetElementsByTagName("dept_Name_en");
        lblHeading.Text = deptname.Item(0).InnerXml;
    }
}