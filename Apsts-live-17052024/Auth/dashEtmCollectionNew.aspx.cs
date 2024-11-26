using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class Auth_dashEtmCollectionNew : System.Web.UI.Page
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
            dvExcessWavierAmount.Visible = false;
            dvPrematureWaybill.Visible = false;
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
                    string waybill_Status = dt.Rows[0]["pstatus"].ToString();
                    if (waybill_Status == "L" || waybill_Status == "T" || waybill_Status == "D")
                    {
                        dvPrematureWaybill.Visible = true;
                    }
                    else
                    {
                        dvPrematureWaybill.Visible = false;
                    }
                    if (dt.Rows[0]["sp_excess_amt_wavier_yn"].ToString()=="Y"&& dt.Rows[0]["sp_excess_amt_wavier_status"].ToString()== "1")
                    {
                        dvExcessWavierAmount.Visible = true;
                    }
                    else
                    {
                        dvExcessWavierAmount.Visible = false;
                    }
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
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "etm.f_get_submitted_waybill_details");
        MyCommand.Parameters.AddWithValue("p_waybill_no", waybill);
        dt = bll.SelectAll(MyCommand);

        if (dt.TableName == "Success")
        {
            if (dt.Rows.Count > 0)
            {
                tbOnlineTktamt.Text = Convert.ToDecimal(dt.Rows[0]["online_ticket_amount"].ToString()).ToString("0.##") + " ₹";

                tbEnrouteTktamt.Text = Convert.ToDecimal(dt.Rows[0]["enroute_amount"].ToString()).ToString("0.##") + " ₹";
                tbenroutecash.Text = Convert.ToDecimal(dt.Rows[0]["cashticket_amt"].ToString()).ToString("0.##") + " ₹";
                tbenrouteqr.Text = Convert.ToDecimal(dt.Rows[0]["qrticket_amt"].ToString()).ToString("0.##") + " ₹";
                tbRefundedTickets.Text = Convert.ToDecimal(dt.Rows[0]["refundamount"].ToString()).ToString("0.##") + " ₹";
                tbDhabaAmt.Text =Convert.ToDecimal( dt.Rows[0]["dhaba_amount"].ToString()).ToString("0.##")+ " ₹";

                tbOtherEarningAmt.Text = Convert.ToDecimal(dt.Rows[0]["extra_earning"].ToString()).ToString("0.##") + " ₹";
                tbotherEarningcash.Text = Convert.ToDecimal(dt.Rows[0]["other_earning_cash_amt"].ToString()).ToString("0.##") + " ₹";
                tbotherEarningqr.Text = Convert.ToDecimal(dt.Rows[0]["other_earning_qramt"].ToString()).ToString("0.##") + " ₹";


                tbTotalEarningAmt.Text = Convert.ToDecimal(dt.Rows[0]["total_earning"].ToString() ).ToString("0.##") + " ₹";
                tbtotalcollectioncash.Text = (Convert.ToDecimal(dt.Rows[0]["total_earning"].ToString()) - (Convert.ToDecimal(dt.Rows[0]["qrticket_amt"].ToString()) + Convert.ToDecimal(dt.Rows[0]["other_earning_qramt"].ToString()) + Convert.ToDecimal(dt.Rows[0]["qrluggage_amt"].ToString()))).ToString("0.##");
                tbtotalcollectionqr.Text = (Convert.ToDecimal(dt.Rows[0]["qrticket_amt"].ToString()) + Convert.ToDecimal(dt.Rows[0]["other_earning_qramt"].ToString()) + Convert.ToDecimal(dt.Rows[0]["qrluggage_amt"].ToString())).ToString("0.##") + " ₹";

                tbLuggageAmt.Text = Convert.ToDecimal(dt.Rows[0]["luggage_amount"].ToString()).ToString("0.##") + " ₹";
                tbluggagecash.Text = Convert.ToDecimal(dt.Rows[0]["cashluggage_amt"].ToString()).ToString("0.##") + " ₹";
                tbluggageqr.Text = Convert.ToDecimal(dt.Rows[0]["qrluggage_amt"].ToString()).ToString("0.##") + " ₹";


                tbTollpaid.Text = Convert.ToDecimal(dt.Rows[0]["toll_amount"].ToString()).ToString("0.##") + " ₹";
                tbParking.Text = Convert.ToDecimal(dt.Rows[0]["parking_amount"].ToString()).ToString("0.##") + " ₹";
                tbOtherExp.Text = Convert.ToDecimal(dt.Rows[0]["other_exp_amount"].ToString()).ToString("0.##") + " ₹";
                tbTotalExpenses.Text = Convert.ToDecimal(dt.Rows[0]["totalexpenses"].ToString()).ToString("0.##") + " ₹";
                //decimal amt = Convert.ToDecimal(dt.Rows[0]["total_earning"].ToString()) - (Convert.ToDecimal(dt.Rows[0]["total_expense"].ToString()) + Convert.ToDecimal(dt.Rows[0]["qrticket_amt"].ToString()) + Convert.ToDecimal(dt.Rows[0]["other_earning_qramt"].ToString()) + Convert.ToDecimal(dt.Rows[0]["qrluggage_amt"].ToString()) );
                tbpEnroteTickets.Text= Convert.ToDecimal(dt.Rows[0]["p_enroute_amount"].ToString()).ToString("0.##") + " ₹";
                tbpEnrouteLuggage.Text = Convert.ToDecimal(dt.Rows[0]["p_luggage_amount"].ToString()).ToString("0.##") + " ₹";
                tbpDhaba.Text = Convert.ToDecimal(dt.Rows[0]["p_dhaba_amount"].ToString()).ToString("0.##") + " ₹";
                tbpOtherEarning.Text = Convert.ToDecimal(dt.Rows[0]["p_other_earning_amount"].ToString()).ToString("0.##") + " ₹";
                tbpTollpaid.Text = Convert.ToDecimal(dt.Rows[0]["p_toll_amount"].ToString()).ToString("0.##") + " ₹";
                tbpParking.Text = Convert.ToDecimal(dt.Rows[0]["p_parking_amount"].ToString()).ToString("0.##") + " ₹";
                tbpOtherExpense.Text = Convert.ToDecimal(dt.Rows[0]["p_other_expense_amount"].ToString()).ToString("0.##") + " ₹";
                tbpTotal.Text = Convert.ToDecimal(dt.Rows[0]["p_balance_amount"].ToString()).ToString("0.##") + " ₹";


                lblNoofInspection.Text = Convert.ToInt32(dt.Rows[0]["no_of_inspections"].ToString()).ToString();
                lblokOnspection.Text = Convert.ToInt32(dt.Rows[0]["no_of_ok_inspections"].ToString()).ToString();
                lblwtInspections.Text = Convert.ToInt32(dt.Rows[0]["no_of_wt_inspections"].ToString()).ToString();

                lbltotalConcession.Text = Convert.ToInt32(dt.Rows[0]["no_of_concessions"].ToString()).ToString();
                lblConcessiontktamt.Text = Convert.ToDecimal(dt.Rows[0]["concessionamount"].ToString()).ToString("0.##") + " ₹";
                lblconcessiondiscountamt.Text = Convert.ToDecimal(dt.Rows[0]["discountamount"].ToString()).ToString("0.##") + " ₹";

                lblRefrenecno.Text = dt.Rows[0]["refrence_no_"].ToString();
                lblActualAmt.Text= Convert.ToDecimal(dt.Rows[0]["actual_amt_"].ToString()).ToString("0.##") + " ₹";
                lblExcessAmt.Text = Convert.ToDecimal(dt.Rows[0]["excess_amt_"].ToString()).ToString("0.##") + " ₹";

                lblDepositedAmt.Text = "Amount To Be Deposited By Conductor ₹" + Convert.ToDecimal(dt.Rows[0]["total_cash_amtount"].ToString()).ToString("0.##");
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