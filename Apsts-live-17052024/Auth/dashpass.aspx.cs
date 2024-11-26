
using System;
using Npgsql;
using System.Data;


public partial class Auth_dashpass : System.Web.UI.Page
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
        //Session["_RefNo"] = "APSTS001122024241";
        if (!IsPostBack)
        {
            try
            {
                if (Session["_RefNo"] != null && !string.IsNullOrEmpty(Session["_RefNo"].ToString()))
                {
                    lblhd.Text = "About Transaction";
                    string RefNo = Session["_RefNo"].ToString();
                    PassDetails(RefNo);
                }
                else
                {
                    return; 
                }
            }
            catch (Exception ex)
            {
                return; 
            }
        }

    }


    #region "Method"
    private void PassDetails(string refNo)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            DataTable dt;
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "buspass.passtxn_detail");
            MyCommand.Parameters.AddWithValue("@p_refno", refNo);

            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {

                    lblhd.Text = "About " + dt.Rows[0]["psngrtypename_"] + "(" + dt.Rows[0]["cardtypename_"] + ") Transaction" + "<br/><span style='font-size: 10pt;color: green;'>" + dt.Rows[0]["currtranrefno_"] + " / " + dt.Rows[0]["passstatus_"] + "</span>";
                    //-----------------Personal Details
                    lblName.Text = dt.Rows[0]["ctzname_"].ToString();
                    lblfname.Text = dt.Rows[0]["fname_"].ToString();
                    lblGender.Text = dt.Rows[0]["gender_"].ToString();
                    lblDOB.Text = dt.Rows[0]["dob_"].ToString();
                    //-----------------Personal Details
                    lblMobno.Text = dt.Rows[0]["mobileno_"].ToString();
                    lblemail.Text = dt.Rows[0]["emailid_"].ToString();
                    lbladdress.Text = dt.Rows[0]["address_"].ToString() + ", " + dt.Rows[0]["city_"].ToString() + ", " + dt.Rows[0]["distname_"].ToString() + " " + dt.Rows[0]["statename_"].ToString() + ", " + dt.Rows[0]["pincode_"].ToString();
                    //-----------------Pass Requested For
                    lblroute.Text = dt.Rows[0]["routenameen"].ToString();
                    lblservicetype.Text = dt.Rows[0]["servicetypename"].ToString();
                    if (DBNull.Value.Equals(dt.Rows[0]["startstationname_"]) && DBNull.Value.Equals(dt.Rows[0]["endstationname_"]))
                    {
                        lblstation.Text = "All Station";
                    }
                    else
                    {
                        lblstation.Text = dt.Rows[0]["startstationname_"].ToString() + " - " + dt.Rows[0]["endstationname_"].ToString();
                    }
                    //-----------------Pass Validity
                    lblfrom.Text = dt.Rows[0]["periodfrom_"].ToString();
                    lblto.Text = dt.Rows[0]["periodto_"].ToString();
                    //-----------------Fare Details
                    lblpassamt.Text = dt.Rows[0]["passamount_"].ToString();
                    lblextracharge.Text = dt.Rows[0]["extramt_"].ToString();
                    lbltaxamount.Text = dt.Rows[0]["totaltax_"].ToString();
                    lblAmountTotal.Text = dt.Rows[0]["totalamount_"].ToString();
                    //-----------------Uploaded Photo
                    if (!DBNull.Value.Equals(dt.Rows[0]["photo_"]))
                    {
                        byte[] photoImage = (byte[])dt.Rows[0]["photo_"];
                        img.ImageUrl = GetImage(photoImage);
                        img.Visible = true;
                    }
                    else
                    {
                        img.Visible = false;
                    }
                    //-----------------Uploaded Document
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
                        lbtndownloidproof.Visible = true;
                    }
                    else
                    {
                        hdidproof.Value = "";
                        lblidproof.Text = "ID Proof Document Not Available";
                        lbtnviewidproof.Visible = false;
                        lbtndownloidproof.Visible = false;
                    }
                    lblmsg.Text = "Sorry, No Journey Performed";
                    pnldrnydtls.Visible = false;
                    pnlNoRecord.Visible = true;
                    GetTransactionLog(dt.Rows[0]["currtranrefno_"].ToString());
                    GetPassLog(dt.Rows[0]["currtranrefno_"].ToString());
                    if (!DBNull.Value.Equals(dt.Rows[0]["passnumber_"]))
                    {
                        Session["PASSNUMBER"] = dt.Rows[0]["passnumber_"];
                        LoadJrny(dt.Rows[0]["passnumber_"].ToString());
                        LoadJrnyYear(dt.Rows[0]["passnumber_"].ToString());
                    }
                    else
                    {
                        pnldrnydtls.Visible = false;
                        pnlNoRecord.Visible = true;
                        lblmsg.Text = "Sorry, No Journey Performed";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

    private void LoadJrnyYear(string passnumber)
    {
        try
        {

            MyCommand = new NpgsqlCommand();
            ddlyear.Items.Clear();
            DataTable dt;
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "summary.smry_passjournyyear");
            MyCommand.Parameters.AddWithValue("@p_concession", passnumber);

            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {

                    ddlyear.DataSource = dt;
                    ddlyear.DataTextField = "yy";
                    ddlyear.DataValueField = "yy";
                    ddlyear.DataBind();
                }
            }

            ddlyear_SelectedIndexChanged(ddlyear, EventArgs.Empty);
        }
        catch (Exception ex)
        {
            ddlyear.Items.Insert(0, "Select");
            ddlyear.Items[0].Value = "0";
            ddlyear.SelectedIndex = 0;
        }
    }

    private void LoadJrny(string passnumber)
    {
        try
        {

            MyCommand = new NpgsqlCommand();
            DataTable dt;
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "summary.smry_passjourny");
            MyCommand.Parameters.AddWithValue("@p_concession", passnumber);

            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {

                    pnldrnydtls.Visible = true;
                    pnlNoRecord.Visible = false;
                    lbltotalkm.Text = dt.Rows[0]["totalkm_"].ToString();
                    lbltotfare.Text = dt.Rows[0]["fareamt_"].ToString();
                    lblconcession.Text = dt.Rows[0]["concessionamt_"].ToString();
                    // lblpanelty.Text = dt.Rows[0]["PANELTY"].ToString
                    //lbltotamtcharged.Text = dt.Rows[0]["AMTCHARGED"].ToString
                }
            }
        }
        catch (Exception ex)
        {
            // Handle the exception
        }
    }


    private void LoadJourneyDetails(string passNumber, string journeyYear)
    {
        try
        {

            MyCommand = new NpgsqlCommand();
            DataTable dt;
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "summary.smry_passjournydtls");
            MyCommand.Parameters.AddWithValue("@p_concession", passNumber);
            MyCommand.Parameters.AddWithValue("@p_year", journeyYear);

            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    rptjournydtlsmonth.DataSource = dt;
                    rptjournydtlsmonth.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            // Handle the exception
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


    private void GetTransactionLog(string p_CURRTRANREFNO)
    {
        try
        {
            rptTxnLog.Visible = false;
            pnlnoTxnLog.Visible = true;
            DataTable dttransLog;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "buspass.gettrns_log");
            MyCommand.Parameters.AddWithValue("@p_currtranrefno", p_CURRTRANREFNO);

            dttransLog = bll.SelectAll(MyCommand);
            if (dttransLog.TableName == "Success")
            {
                if (dttransLog.Rows.Count > 0)
                {

                    rptTxnLog.DataSource = dttransLog;
                    rptTxnLog.DataBind();
                    rptTxnLog.Visible = true;
                    pnlnoTxnLog.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            rptTxnLog.Visible = false;
            pnlnoTxnLog.Visible = true;
        }
    }

    private void GetPassLog(string p_CURRTRANREFNO)
    {
        try
        {
            rptpassLog.Visible = false;
            pnlnopassLog.Visible = true;



            DataTable dtpassLog;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "buspass.getpass_log");
            MyCommand.Parameters.AddWithValue("@p_currtranrefno", p_CURRTRANREFNO);

            dtpassLog = bll.SelectAll(MyCommand);
            if (dtpassLog.TableName == "Success")
            {
                if (dtpassLog.Rows.Count > 0)
                {
                    rptpassLog.DataSource = dtpassLog;
                    rptpassLog.DataBind();
                    rptpassLog.Visible = true;
                    pnlnopassLog.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            rptpassLog.Visible = false;
            pnlnopassLog.Visible = true;
        }
    }


    private void GetDocumentID(string docType, string viewType)
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
                filename = hdaddressproof.Value.ToString();
                fileInvoice = (byte[])Session["ADDPROOOF"];
            }
            else
            {
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
            
        }
    }

    #endregion
    #region "Event"
    protected void lbtnviewidproof_Click(object sender, EventArgs e)
    {
        GetDocumentID("ID Proof", "V");
    }

    protected void lbtndownloidproof_Click(object sender, EventArgs e)
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

    protected void ddlyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadJourneyDetails(Session["PASSNUMBER"].ToString(), ddlyear.SelectedValue);
    }
    #endregion





}