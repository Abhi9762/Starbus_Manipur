using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OnlAgApplicationFeePayment : System.Web.UI.Page
{
    sbValidation _SecurityCheck = new sbValidation();
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
            Session["Heading"] = "Agent Application";
            Session["Heading1"] = "You can register, as an agent to become part of APSTS family";
            loadRegistrtaionDateFeeDetails();
            loadApplicantDetails();
            loadpaymentStatus();
        }
    }

    #region "Method" 
    public void errormsg(string errormsg)
    {
        lblerrmsg.Text = errormsg;
        mpError.Show();
    }

    private void loadpaymentStatus()
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
 


    public void loadRegistrtaionDateFeeDetails()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "agent.fun_get_onlagentregsdatefee");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    // lblSecurityFee.Text = dt.Rows[0]["securityFee"].ToString();
                    // lblFromdate.Text = dt.Rows[0]["frDate"].ToString();
                    // lblToDate.Text = dt.Rows[0]["todate"].ToString();
                    // lblProcFromdate.Text = dt.Rows[0]["procfrDate"].ToString();
                    // lblProcTodate.Text = dt.Rows[0]["proctodate"].ToString();
                    //lblAmount.Text = dt.Rows[0]["security_Fee"].ToString();

                }
            }
        }
        catch (Exception ex)
        {
            // Handle the exception
        }
    }

    private void loadApplicantDetails()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "agent.fun_AgRequestStatus");
            MyCommand.Parameters.AddWithValue("@p_referenceno", Session["referenceNo"].ToString());
            MyCommand.Parameters.AddWithValue("@p_mobile", Session["MobileNo"].ToString());
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    lblReferenceNo.Text = dt.Rows[0]["reference_no"].ToString();
                    Session["referenceNo"] = dt.Rows[0]["reference_no"].ToString();
                    grvRequest.DataSource = dt;
                    grvRequest.DataBind();
                    Session["MobileNo"] = dt.Rows[0]["mobile_no"].ToString();
                    Session["Emailid"] = dt.Rows[0]["val_email"].ToString();
                    lblSecurityAmt.Text = dt.Rows[0]["security_fee"].ToString();
                    Session["securityFee"] = dt.Rows[0]["security_fee"].ToString();
                }
                else
                {
                    errormsg("Invalid Reference/Mobile No");
                }
            }
        }
        catch (Exception ex)
        {
            // Handle exception
        }
    }

    private bool SaveDetails(string agRefNo)
    {
        try
        {
            string IPAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(IPAddress))
            {
                IPAddress = Request.ServerVariables["REMOTE_ADDR"];
            }

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "agent.fun_get_AgPaymentFee");
            MyCommand.Parameters.AddWithValue("@p_referenceno", agRefNo);
            MyCommand.Parameters.AddWithValue("@p_ipaddress", IPAddress);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    Session["transrefno"] = dt.Rows[0]["p_txn_ref_no"];
                    Session["depositamt"] = dt.Rows[0]["p_deposit_amount"];
                    return true;
                }
            }

            return false;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    #endregion


    #region "Events"


    protected void rptrPG_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "PAYNOW")
        {
            string amount = Session["securityFee"].ToString();
            if (_SecurityCheck.IsValidInteger(amount, 1, 5) == false)
            {
                errormsg("Enter valid Amount");
                return;
            }
            wsClass obj = new wsClass();
            DataTable dttt = new DataTable();
            string userId = Session["referenceno"].ToString();
            dttt = obj.walletTopup_start_completed("0", userId, amount, "F");
            if (dttt.Rows[0]["p_rslt"].ToString() != "EXCEPTION")
            {
                Session["transrefno"] = dttt.Rows[0]["p_rslt"].ToString();
                Session["depositamt"] = amount;
                if (SaveDetails(Session["referenceno"].ToString()) == true)
                    {
                    HiddenField rptHdPGId = e.Item.FindControl("rptHdPGId") as HiddenField;
                    hdpgid.Value = rptHdPGId.Value.ToString();
                    lblmsg.Text = "You have initiated a topup request of Rs <b>" + Session["depositamt"].ToString() + "</b>.<br/> Please note this Reference number for this transaction is <b>" + Session["transrefno"].ToString() + "</b> for all future needs";
                    mpconfirmation.Show();
                    HiddenField REQURL = e.Item.FindControl("hd_pgurl") as HiddenField;
                    hdpgurl.Value = REQURL.Value.ToString();
                }
            }
            else
            {
                errormsg("Something went wrong. Try again. If you get this again and again feel free to contact helpdesk.");
                return;
            }
        }
        else
        {
            errormsg("Something went wrong with Payment Gateway redirection. Please contact to helpdesk.");
        }




    }

    protected void lbtnyes_Click(object sender, EventArgs e)
    {
        string userId = Session["referenceno"].ToString();
        string resposeurl = hdpgurl.Value.ToString();
        Session["_RNDIDENTIFIERCTZAUTHC"] = _SecurityCheck.GeneratePassword(10, 5);
        byte[] base64str = System.Text.Encoding.UTF8.GetBytes("22");
       //Response.Redirect("/starbus_manipur/PG/" + resposeurl + "?RequestId=" + System.Convert.ToBase64String(base64str));
        Response.Redirect("/PG/" + resposeurl + "?RequestId=" + System.Convert.ToBase64String(base64str));
    }



    #endregion



}