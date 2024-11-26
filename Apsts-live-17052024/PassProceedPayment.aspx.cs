using System;
using Npgsql;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Globalization;

public partial class PassProceedPayment : System.Web.UI.Page
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    HDFC hdfc = new HDFC();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["headerText"] = "Proceed to Payment";
            LoadTransDetails((string)Session["currtranrefno"]);
            loadpaymentgateway();
        }

    }

    #region "Methods"
    private void LoadTransDetails(string CurrentRefNo)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "buspass.getconfirmpassdetails");
            MyCommand.Parameters.AddWithValue("@p_currentrefno", CurrentRefNo);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {

                    // lbltransrefno.Text = dt.Rows[0]["CURRTRANREFNO"].ToString();
                    //lblPassType.Text = dt.Rows[0]["cardtypename"].ToString();
                    //lblPassenger.Text = dt.Rows[0]["psngrtypename"].ToString();
                    lblpassrequest.Text = "You are applying for " + dt.Rows[0]["cardtypename_"].ToString() + " (" + dt.Rows[0]["psngr_type_name"].ToString() + ") with the following details.";

                    //-------------------------------Personal Details
                    lblname.Text = dt.Rows[0]["ctz_name"].ToString();
                    lblfname.Text = dt.Rows[0]["f_name"].ToString();
                    lblgender.Text = dt.Rows[0]["gender_"].ToString();
                    lbldob.Text = dt.Rows[0]["dob_"].ToString();

                    //--------------------------Journey Details
                    if (Convert.IsDBNull(dt.Rows[0]["routename"]) == true)
                    {
                        lblRoute.Text = "All Routes";
                    }
                    else
                    {
                        lblRoute.Text = dt.Rows[0]["routename"].ToString();
                    }

                    if (Convert.IsDBNull(dt.Rows[0]["start_station_name"]) == true)
                    {
                        lblstations.Text = "All Stations";
                    }
                    else
                    {
                        lblstations.Text = dt.Rows[0]["start_station_name"].ToString() + " to " + dt.Rows[0]["end_station_name"].ToString();
                    }
                    if (Convert.IsDBNull(dt.Rows[0]["service_type_name"]) == true)
                    {
                        lblservicetype.Text = "All Services";
                    }
                    else
                    {
                        lblservicetype.Text = dt.Rows[0]["service_type_name"].ToString();
                    }

                    lblvalidity.Text = dt.Rows[0]["period_from"].ToString() + " to " + dt.Rows[0]["period_to"].ToString();

                    //----------------------------Contact Details
                    lblmobileno.Text = dt.Rows[0]["mobile_no"].ToString();
                    string mask = new string('*', dt.Rows[0]["mobile_no"].ToString().Length - 4);
                    //lblMobileOTP.Text = "Enter 6 digit OTP(One Time Password) sent to your Mobile Number " + mask + dt.Rows[0]["mobile_no"].ToString().Substring(dt.Rows[0]["mobile_no"].ToString().Length - 4, 4) + "<br/>";

                    lblemail.Text = dt.Rows[0]["email_id"].ToString();
                    lblstate.Text = dt.Rows[0]["statename_"].ToString();
                    lbldistrict.Text = dt.Rows[0]["distname_"].ToString();
                    lbladdress.Text = dt.Rows[0]["address_"].ToString() + ", " + dt.Rows[0]["pincode_"].ToString();

                    //-------------------------------Amount Details
                    lblPassamount.Text = dt.Rows[0]["pass_amount"].ToString() + " <i class='fa fa-rupee' ></i>";
                    if (Convert.ToInt32(dt.Rows[0]["extra_amt"]) == 0)
                    {
                        lblExtra_Charges.Text = "Extra_Charges";
                        lblExtrachrge.Text = dt.Rows[0]["extra_amt"].ToString() + " <i class='fa fa-rupee' ></i>";
                    }
                    else
                    {
                        lblExtra_Charges.Text = "Extra_Charges <br/> <span style='font-size:9pt;'>(" + dt.Rows[0]["p_extrachrgname_"].ToString() + ")</span>";
                        lblExtrachrge.Text = dt.Rows[0]["extra_amt"].ToString() + " <i class='fa fa-rupee' ></i>";
                    }
                    lbltaxamt.Text = dt.Rows[0]["totaltax_"].ToString() + " <i class='fa fa-rupee' ></i>";
                    lblAmountToPay.Text = (Convert.ToDouble(dt.Rows[0]["pass_amount"]) + Convert.ToDouble(dt.Rows[0]["extra_amt"]) + Convert.ToDouble(dt.Rows[0]["totaltax_"])).ToString();
                    Session["AMOUNT"] = lblAmountToPay.Text;
                    Session["MobileNo"] = dt.Rows[0]["mobile_no"].ToString();
                    Session["Emailid"] = dt.Rows[0]["email_id"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }


    private void Errormsg(string msg)
    {
        lblerrormsg.Text = msg;
        mperror.Show();
    }


    private void savetxn_status()
    {

        string IPAddress = HttpContext.Current.Request.UserHostAddress;
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "buspass.insert_t_pass_txn_log");
        MyCommand.Parameters.AddWithValue("@p_tranrefno", Session["currtranrefno"]);
        MyCommand.Parameters.AddWithValue("@p_updateby", Session["MobileNo"]);
        MyCommand.Parameters.AddWithValue("@p_ipaddress", IPAddress);
        MyCommand.Parameters.AddWithValue("@p_txn_status", 2);
        dt = bll.SelectAll(MyCommand);
        if (dt.TableName == "Success")
        {
            if (dt.Rows.Count > 0)
            {
                Session["_RNDIDENTIFIERMSTAUTH"] = _security.GeneratePassword(10, 5);
                Session["PageStep1"] = 3;
            }
            else
            {

            }
        }

    }
    #endregion


    #region "Events"
    private void loadpaymentgateway()
    {
        try
        {
            lblNoPG.Visible = true;
            rptrPG.Visible = false;
            DataTable dt_pg = new DataTable();
            dt_pg = hdfc.get_PG();

            if (dt_pg.Rows.Count > 0)
            {
                rptrPG.DataSource = dt_pg;
                rptrPG.DataBind();
                lblNoPG.Visible = false;
                rptrPG.Visible = true;
            }
        }
        catch (Exception ex)
        {
            lblNoPG.Visible = true;
            rptrPG.Visible = false;
        }
    }


    protected void rptrPG_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

        if (e.CommandName == "PAYNOW")
        {
            //double totalAmount = Convert.ToDouble("30");
            double totalAmount = Convert.ToDouble(Session["AMOUNT"].ToString());
            if (totalAmount <= 0)
            {
                Errormsg("Invalid Payable amount.");
                return;
            }
            savetxn_status();
            HiddenField rptHdPGId = e.Item.FindControl("rptHdPGId") as HiddenField;
            HiddenField REQURL = e.Item.FindControl("hd_pgurl") as HiddenField;
            string resposeurl = REQURL.Value.ToString();
            byte[] base64str = System.Text.Encoding.UTF8.GetBytes("3");
            Response.Redirect("PG/" + resposeurl + "?RequestId=" + System.Convert.ToBase64String(base64str));
        }
        else
        {
            Errormsg("Something went wrong with Payment Gateway redirection. Please contact the helpdesk.");
        }
    }

    #endregion

}



