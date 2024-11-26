using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_PassADMVerification : System.Web.UI.Page
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
            Session["MasterPageHeaderText"] = "Pass Admin";
            Session["ModuleName"] = "Pass Request Verification";
            LoadPassTransactionDetails();
        }

    }

    #region "Method"

    private void LoadPassTransactionDetails()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            DataTable dt;
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "buspass.getconfirmpassdetails");
            MyCommand.Parameters.AddWithValue("@p_currentrefno", Session["currtranrefno"].ToString());

            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {

                    // ----------------------Personal Details
                    lblpass.Text = dt.Rows[0]["passenger_type"].ToString();
                    lblname.Text = dt.Rows[0]["ctz_name"].ToString();
                    lblfname.Text = dt.Rows[0]["f_name"].ToString();
                    lblgender.Text = dt.Rows[0]["gender_"].ToString();
                    lblage.Text = dt.Rows[0]["age_"].ToString() + " Years";
                    lblmobile.Text = dt.Rows[0]["mobile_no"].ToString();
                    lblemail.Text = dt.Rows[0]["email_id"].ToString();
                    lbladdress.Text = dt.Rows[0]["address_"].ToString();

                    // ------------------Amount Details
                    lblpassamt.Text = dt.Rows[0]["pass_amount"].ToString();
                    lblextrachrge.Text = dt.Rows[0]["extra_amt"].ToString();
                    lblTax.Text = dt.Rows[0]["totaltax_"].ToString();
                    if (dt.Rows[0]["transtype_"].ToString() == "R")
                    {
                        lblrenew.Text = dt.Rows[0]["Renew Charges"].ToString();
                        lblrenewamt.Text = dt.Rows[0]["renewamount_"].ToString();
                    }

                    // ----------------------Application Details
                    lbltransrefno.Text = dt.Rows[0]["currtranref_no"].ToString();
                    lblPassType.Text = dt.Rows[0]["cardtypename_"].ToString();
                    lblPassenger.Text = dt.Rows[0]["psngr_type_name"].ToString();

                    if (DBNull.Value.Equals(dt.Rows[0]["start_station_name"]))
                    {
                        lblStation.Text = "All Stations";
                    }
                    else
                    {
                        lblStation.Text = dt.Rows[0]["start_station_name"].ToString() + " to " + dt.Rows[0]["end_station_name"].ToString();
                    }
                    lblValidFrom.Text = dt.Rows[0]["period_from"].ToString();
                    lblValidUpto.Text = dt.Rows[0]["period_to"].ToString();

                    // ----------------------UploadDetails Details
                    if (!DBNull.Value.Equals(dt.Rows[0]["addproof_"]))
                    {
                        Session["ADDPROOOF"] = dt.Rows[0]["addproof_"];
                        hdaddressproof.Value = DBNull.Value.Equals(dt.Rows[0]["addressproofnumber_"]) ? "ADDPROOOF" : dt.Rows[0]["addressproofnumber_"].ToString();
                        lbladdressproof.Text = "Address Proof";
                        lbtnviewaddressproof.Visible = true;
                        lbtndownloadaddressproof.Visible = true;
                    }
                    else
                    {
                        hdaddressproof.Value = "";
                        lbladdressproof.Text = "Address Proof Document Not Available";
                        lbtnviewaddressproof.Visible = false;
                        lbtndownloadaddressproof.Visible = false;
                    }

                    if (!DBNull.Value.Equals(dt.Rows[0]["idproof_"]))
                    {
                        Session["IDPROOOF"] = dt.Rows[0]["idproof_"];
                        hdidproof.Value = DBNull.Value.Equals(dt.Rows[0]["idproofnumber_"]) ? "IDPROOOF" : dt.Rows[0]["idproofnumber_"].ToString();
                        lblidproof.Text = "Id Proof";
                        lbtnviewidproof.Visible = true;
                        lbtndownloadproof.Visible = true;
                    }
                    else
                    {
                        hdidproof.Value = "";
                        lblidproof.Text = "ID Proof Document Not Available";
                        lbtnviewidproof.Visible = false;
                        lbtndownloadproof.Visible = false;
                    }

                    if (!DBNull.Value.Equals(dt.Rows[0]["photo_"]))
                    {
                        byte[] photoImage = (byte[])dt.Rows[0]["photo_"];
                        img.ImageUrl = GetImage(photoImage);
                        lblimg.Visible = false;
                        img.Visible = true;
                    }
                    else
                    {
                        lblimg.Visible = true;
                        img.Visible = false;
                    }

                    GetTransactionLog(dt.Rows[0]["currtranref_no"].ToString());
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
            return;
        }
    }

    private void GetTransactionLog(string p_CURRTRANREFNO)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            DataTable dtLog;
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "buspass.gettrns_log");
            MyCommand.Parameters.AddWithValue("@p_currentrefno", p_CURRTRANREFNO);

            dtLog = bll.SelectAll(MyCommand);
            if (dtLog.TableName == "Success")
            {
                if (dtLog.Rows.Count > 0)
                {

                    rptTxnLog.DataSource = dtLog;
                    rptTxnLog.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            
        }

    }

    public string GetImage(object img)
    {
        try
        {
            return "data:image/jpg;base64," + Convert.ToBase64String((byte[])img);
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public void GetDocumentID(string docType, string viewType)
    {
        try
        {
            string filename;
            byte[] fileInvoice;

            if (docType == "ID Proof")
            {
                fileInvoice = (byte[])Session["IDPROOOF"];
                filename = hdidproof.Value.ToString();
            }
            else if (docType == "Address Proof")
            {
                fileInvoice = (byte[])Session["ADDPROOOF"];
                filename = hdaddressproof.Value.ToString();
            }
            else
            {
                // Handle other document types if needed
                return;
            }

            string base64String = Convert.ToBase64String(fileInvoice, 0, fileInvoice.Length);

            if (viewType == "V")
            {
                Session["Base64String"] = base64String;
                mpviewdocment.Show();
            }
            else if (viewType == "D")
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + filename + ".pdf");
                Response.BinaryWrite(fileInvoice);
                Response.End();
            }
        }
        catch (Exception ex)
        {
            // Handle the exception or log it as needed
        }
    }


    private bool ChangePassRequest()
    {
        try
        {


            MyCommand = new NpgsqlCommand();
            DataTable dt;
            string IPAddress = HttpContext.Current.Request.UserHostAddress;
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "buspass.change_pass_request");
            MyCommand.Parameters.AddWithValue("@p_currentrefno", Session["currtranrefno"]);
            MyCommand.Parameters.AddWithValue("@p_status", Session["P_Action"]);
            MyCommand.Parameters.AddWithValue("@p_rejectreason", txtreason.Text);
            MyCommand.Parameters.AddWithValue("@p_ipaddress", IPAddress);
            MyCommand.Parameters.AddWithValue("@p_usercode", "PASSADMIN");

            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {

                    return true;
                }
            }

            Response.Write(dt.TableName);
            return false;
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
            return false;
        }
    }

    #endregion
    #region "Event"
    protected void lbtnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("PassADMDashboard.aspx");
    }

    protected void lbtnviewidproof_Click(object sender, EventArgs e)
    {
        GetDocumentID("ID Proof", "V");
    }

    protected void lbtndownloadproof_Click(object sender, EventArgs e)
    {
        GetDocumentID("ID Proof", "D");
    }

    protected void lbtnviewaddressproof_Click(object sender, EventArgs e)
    {
        GetDocumentID("Address Proof", "V");
    }

    protected void lbtndownloadaddressproof_Click(object sender, EventArgs e)
    {
        GetDocumentID("Address Proof", "D");
    }

    protected void lbtnverify_Click(object sender, EventArgs e)
    {
        lblConfirmation.Text = "Do you want to verify Pass Request ?";
        dvreason.Visible = false;
        Session["P_Action"] = "A";
            mpConfirmation.Show();
        }
    

    protected void lbtnreject_Click(object sender, EventArgs e)
    {
            dvreason.Visible = true;
            lblConfirmation.Text = "Do you want to Reject Pass Request ?";
            Session["P_Action"] = "C";
            mpConfirmation.Show();
    }



    protected void lbtnsuccessclose1_Click(object sender, EventArgs e)
    {
        Response.Redirect("PassADMDashboard.aspx");
    }

    protected void lbtnclose_Click(object sender, EventArgs e)
    {
        mperror.Hide();
        mpConfirmation.Show();
    }

    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        if (ChangePassRequest() == true)
        {
            if (Session["P_Action"].ToString() == "A")
            {
                // Approved SMS
               // comm.BusPass_Approved_SMS(Session["currtranrefno"].ToString(), lblmobile.Text.ToString(), 22);
                lblsuccessmsg.Text = "New Pass Request Successfully Approved";
            }
            else if (Session["P_Action"].ToString() == "C")
            {
                if (txtreason.Text.Length < 5)
                {
                    mpConfirmation.Hide();
                    lblerrormsg.Text = "Please Enter Reason of Rejection";
                    mperror.Show();
                    return;
                }

                // Rejected SMS
               // comm.BusPass_Rejec_SMS(Session["currtranrefno"].ToString(), DateTime.Now, txtreason.Text.ToString(), lblmobile.Text.ToString(), 23);
                lblsuccessmsg.Text = "New Pass Request Successfully Rejected";
            }

            mpsuccess.Show();
        }
        else
        {
            lblsuccessmsg.Text = "Error";
            mpsuccess.Show();
        }

    }

  

    #endregion
}