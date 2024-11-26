using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_CntrBusPassesConfirmation : System.Web.UI.Page
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    protected void Page_Load(object sender, EventArgs e)
    {
        
       
        if (!IsPostBack)
        {
            Session["_moduleName"] = "Confirm Bus Pass Details";
            GetconfirmationDetails(Session["currtranrefno"].ToString());
        }
          
    }


    #region "Method"
    protected string GetconfirmationDetails(string currtranrefno)
    {
        try
        {
            DataTable dt;
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "buspass.getconfirmpassdetails");
            MyCommand.Parameters.AddWithValue("p_currentrefno", currtranrefno);

            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {


                    lblpassrequest.Text = "You are applying for " + dt.Rows[0]["cardtypename_"].ToString() + " (" + dt.Rows[0]["psngr_type_name"].ToString() + ") with the following details.";

                    //-------------------------------Personal Details
                    lblname.Text = dt.Rows[0]["ctz_name"].ToString();
                    lblfname.Text = dt.Rows[0]["f_name"].ToString();
                    lblgender.Text = dt.Rows[0]["gender_"].ToString();
                    lbldob.Text = dt.Rows[0]["dob_"].ToString();



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
                    lblAmountToRecieved.Text = (Convert.ToDouble(dt.Rows[0]["pass_amount"]) + Convert.ToDouble(dt.Rows[0]["extra_amt"]) + Convert.ToDouble(dt.Rows[0]["totaltax_"])).ToString();
                    Session["AMOUNT"] = lblAmountToRecieved.Text;
                    Session["MobileNo"] = dt.Rows[0]["mobile_no"].ToString();
                    Session["Emailid"] = dt.Rows[0]["email_id"].ToString();
                    Session["PassType"] = dt.Rows[0]["psngr_type_name"].ToString();

                }
                return "";
            }
            return "";
        }
        catch (Exception ex)
        {

            Console.WriteLine(ex.Message);
            return "";
        }
    }
    private void ConfirmMsg(string msg)
    {
        lblConfirmation.Text = msg;
        mpConfirmation.Show();
    }
    private bool UpdateCntrAccount()
    {
        try
        {
            string IPAddress = HttpContext.Current.Request.UserHostAddress;
            DataTable dt;
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "buspass.updatecntraccount");
            MyCommand.Parameters.AddWithValue("p_currentrefno", Session["currtranrefno"].ToString());
            MyCommand.Parameters.AddWithValue("p_cntrid", Session["_UserCntrID"].ToString());
            MyCommand.Parameters.AddWithValue("p_ipaddress", IPAddress);
            MyCommand.Parameters.AddWithValue("p_updatedby", Session["_UserCntrID"].ToString());
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {

                if (dt.Rows.Count > 0)
                {
                    Session["currtranrefno"] = dt.Rows[0]["sp_currentrefno"];
                    Session["IssuanceType"] = dt.Rows[0]["issuetype"];

                    if (Session["IssuanceType"].ToString() == "I")
                    {
                        //comm.BusPass_Approved_SMS(Session["currtranrefno"].ToString(), Session["MobileNo"], 22);
                    }
                    if (Session["IssuanceType"].ToString() == "A")
                    {
                        //comm.BusPass_Accept_SMS(Session["PassType"].ToString(), Session["currtranrefno"].ToString(), Session["MobileNo"], 21);
                    }

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

    #region "Event" 
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        if (UpdateCntrAccount() == true)
        {
            Session["_RNDIDENTIFIERCNTRAUTH"] = _validation.GeneratePassword(10, 5);
            Response.Redirect("CntrBusPassesStatus.aspx");
        }
    }  
    protected void lbtnproceed_Click(object sender, EventArgs e)
    {
        ConfirmMsg("I have Recieved Amount ₹ " + Session["AMOUNT"].ToString() + ". Do you want to proceed?");
    }
    protected  void lbtnback_Click(object sender, EventArgs e)
    {
        Session["_RNDIDENTIFIERCNTRAUTH"] = _validation.GeneratePassword(10, 5);
        Response.Redirect("CntrBusPasses.aspx");
    }
    #endregion
}