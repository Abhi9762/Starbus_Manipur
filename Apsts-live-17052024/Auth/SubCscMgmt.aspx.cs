using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClosedXML.Excel;

public partial class Auth_SubCscMgmt : System.Web.UI.Page
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    string message = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        Session["moduleName"] = "Common Service Centre - Sub Csc Management";
        GetCscSubAgDetails();

        if (!IsPostBack)
        {
            pnlDetailsShowHide(pnlManualRegistraion);
            loadState(ddlStates);
            loadDistrict(ddlStates.SelectedValue, ddlDistricts);
        }
    }

    #region "Methods"
    private void Errormsg(string message)
    {
        lblerrormsg.Text = message;
        mperror.Show();
    }
    private void SuccessMesgBox(string message)
    {
        lblsuccessmsg.Text = message;
        mpsuccess.Show();
    }
    private void pnlDetailsShowHide(Panel pnl)
    {
        pnlAutoRegistraion.Visible = false;
        pnlManualRegistraion.Visible = false;
        pnl.Visible = true;
    }
    private void btnCssView(LinkButton btn)
    {
        lbtnManual.CssClass = "nav-item nav-link";
        lbtnAuto.CssClass = "nav-item nav-link";
        btn.CssClass = "nav-item nav-link active";
    }
    private void loadState(DropDownList ddlstate)//M3
    {
        try
        {
            ddlstate.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.f_get_states");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlstate.DataSource = dt;
                    ddlstate.DataTextField = "stname";
                    ddlstate.DataValueField = "stcode";
                    ddlstate.DataBind();
                }
            }
            else
            {
                _common.ErrorLog("AgentRegistration-M3", dt.TableName);
            }
            ddlstate.Items.Insert(0, "SELECT");
            ddlstate.Items[0].Value = "0";
            ddlstate.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlstate.Items.Insert(0, "SELECT");
            ddlstate.Items[0].Value = "0";
            ddlstate.SelectedIndex = 0;
            _common.ErrorLog("SubCscMgmt", ex.Message.ToString());
        }
    }
    private void loadDistrict(string State_code, DropDownList ddldistrict)
    {
        try
        {
            ddldistrict.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.f_get_district");
            MyCommand.Parameters.AddWithValue("p_statecode", State_code);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddldistrict.DataSource = dt;
                    ddldistrict.DataTextField = "distname";
                    ddldistrict.DataValueField = "distcode";
                    ddldistrict.DataBind();
                }
            }
            else
            {
                _common.ErrorLog("SubCscMgmt", dt.TableName);
            }

            ddldistrict.Items.Insert(0, "SELECT");
            ddldistrict.Items[0].Value = "0";
            ddldistrict.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddldistrict.Items.Insert(0, "SELECT");
            ddldistrict.Items[0].Value = "0";
            ddldistrict.SelectedIndex = 0;
            _common.ErrorLog("SubCscMgmt", ex.Message.ToString());
        }
    }
    private void InitControl()
    {
        Session["Agcode"] = null;
        btnSave.Visible = true;
        btnUpdate.Visible = false;
        txtcscid.Text = "";
        txtcscname.Text = "";
        txtcscpersonname.Text = "";
        txtemail.Text = "";
        txtmobileno.Text = "";
        txtAddress.Text = "";
        txtpincode.Text = "";
        loadState(ddlStates);
        loadDistrict(ddlStates.SelectedValue, ddlDistricts);
    }
    private void DisableFields()
    {
        txtemail.Enabled = false;
        txtmobileno.Enabled = false;
        txtcscpersonname.Enabled = false;
    }
    private void EnableFields()
    {
        txtcscid.Enabled = true;
        txtemail.Enabled = true;
        txtmobileno.Enabled = true;
        txtcscpersonname.Enabled = true;
    }
    private void Savedetails()
    {
        try
        {
            string mobile = txtmobileno.Text;
            string cscid = txtcscid.Text.ToString();

            string Maction = "";
            string Mresult = "";
            string strAgentCcode = "";
            if (btnSave.Visible == true)
            {
                MyCommand = new NpgsqlCommand();
                MyCommand.Parameters.Clear();
                MyCommand.Parameters.AddWithValue("@StoredProcedure", "csc.checkSubCSCMobile");
                MyCommand.Parameters.AddWithValue("@p_mobile", mobile);
                MyCommand.Parameters.AddWithValue("@p_cscid", cscid);

                dt = bll.SelectAll(MyCommand);
                if (dt.TableName == "Success")
                {
                    if (dt.Rows.Count > 0)
                    {

                        Errormsg("Mobile and CSC Id/Code Already Exist.");
                        return;
                    }
                    strAgentCcode = "";
                    Maction = "A";
                }
            }
            else if (btnSave.Visible == false)
            {
                strAgentCcode = Session["Agcode"].ToString();
                Maction = "U";
            }


            string IPAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(IPAddress))
            {
                IPAddress = Request.ServerVariables["REMOTE_ADDR"];
            }

            string Mpwd;
            Mpwd = "test@123";// RandomString(6);

            string MpwdEncrypted;
            MpwdEncrypted = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(Mpwd, "SHA1");
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "csc.agentinsertupdatenew");
            MyCommand.Parameters.AddWithValue("p_action", Maction);
            MyCommand.Parameters.AddWithValue("p_agentcode", strAgentCcode);
            MyCommand.Parameters.AddWithValue("p_agentname", txtcscname.Text);
            MyCommand.Parameters.AddWithValue("p_address", txtAddress.Text);
            MyCommand.Parameters.AddWithValue("p_statecode", Convert.ToInt32(ddlStates.SelectedValue));
            MyCommand.Parameters.AddWithValue("p_districtcode", Convert.ToInt32(ddlDistricts.SelectedValue));
            MyCommand.Parameters.AddWithValue("p_email", txtemail.Text);
            MyCommand.Parameters.AddWithValue("p_contactperson", txtcscpersonname.Text);
            MyCommand.Parameters.AddWithValue("p_mobileno", txtmobileno.Text);
            MyCommand.Parameters.AddWithValue("p_updatedby", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("p_ipaddress", IPAddress);
            MyCommand.Parameters.AddWithValue("p_maincscagent", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("p_pwd", MpwdEncrypted);
            MyCommand.Parameters.AddWithValue("p_pincode", txtpincode.Text);
            MyCommand.Parameters.AddWithValue("p_csccode", txtcscid.Text);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    if (Maction == "A")
                    {
                        //comm.sendSubagPWDSMS(txtcscpersonname.Text, spResult.Value.ToString(), Mpwd, txtmobileno.Text.Trim(), 18);
                        //comm.sendSubagPWDEMail(txtcscpersonname.Text, spResult.Value.ToString(), Mpwd, txtemail.Text.Trim());

                        SuccessMesgBox("CSC Details Successfully saved. Userid and Password sent by SMS and email ");
                    }
                    else if (Maction == "U")
                    {
                        SuccessMesgBox("CSC Details Successfully Update.");
                        EnableFields();
                    }
                    InitControl();
                    GetCscSubAgDetails();

                }
            }
        }

        catch (Exception ex)
        {
            //errormsg("There is some error.");
            _common.ErrorLog("SubCScMgmt", ex.Message.ToString());
        }
    }
    private void GetCscSubAgDetails()
    {
        try
        {

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "csc.subagentdetails");
            MyCommand.Parameters.AddWithValue("p_mainagent", Session["_UserCode"].ToString());
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {

                    grvCSCDeatails1.DataSource = dt;
                    grvCSCDeatails1.DataBind();
                    grvCSCDeatails1.ShowHeader = true;
                }
                else
                {
                    grvCSCDeatails1.ShowHeader = false;
                }
            }
        }
        catch (Exception ex)
        {
            string dd = ex.Message;
        }
    }
    private bool IsValidAgValue()
    {
        int msgcount = 0;
        string msg = "";

        if (!_validation.IsValidString(txtcscid.Text.Trim(), 10, txtcscid.MaxLength))
        {
            msgcount++;
            msg += msgcount.ToString() + ". Enter Valid CSC ID/Code.<br/> ";
        }
        if (!_validation.IsValidString(txtcscname.Text.Trim(), 1, 50))
        {
            msgcount++;
            msg += msgcount.ToString() + ". Enter Valid CSC Name.<br/> ";
        }
        if (!_validation.IsValidString(txtcscpersonname.Text.Trim(), 1, 50))
        {
            msgcount++;
            msg += msgcount.ToString() + ". Enter Valid CSC Contact Person Name.<br/> ";
        }

        if (string.IsNullOrEmpty(txtemail.Text.Trim()))
        {
            msgcount++;
            msg += msgcount.ToString() + ". Enter Valid e-Mail ID.<br/> ";
        }
        else if (!_validation.isValideMailID(txtemail.Text))
        {
            msgcount++;
            msg += msgcount.ToString() + ". Enter Valid e-Mail ID.<br/> ";
        }
        if (!_validation.IsValidInteger(txtmobileno.Text, 10, 10))
        {
            msgcount++;
            msg += msgcount.ToString() + ". Enter Valid Mobile Number.<br/> ";
        }
        if (!_validation.IsValidString(txtAddress.Text.Trim(), 1, 100))
        {
            msgcount++;
            msg += msgcount.ToString() + ". Enter Valid Address.<br/> ";
        }
        if (ddlStates.SelectedIndex <= 0)
        {
            msgcount++;
            msg += msgcount.ToString() + ". Select Valid State .<br/> ";
        }
        else
        {
            if (!_validation.IsValidString(ddlStates.SelectedValue, 1, 2))
            {
                msgcount++;
                msg += msgcount.ToString() + ". Select Valid State.<br/> ";
            }
        }

        if (ddlDistricts.SelectedIndex <= 0)
        {
            msgcount++;
            msg += msgcount.ToString() + ". Select Valid District.<br/> ";
        }
        else
        {
            if (!_validation.IsValidString(ddlDistricts.SelectedValue, 1, 5))
            {
                msgcount++;
                msg += msgcount.ToString() + ". Select Valid District.<br/> ";
            }
        }
        if (msgcount > 0)
        {
            Errormsg(msg);
            return false;
        }
        return true;
    }
    public string RandomString(int length)
    {
        Random random = new Random();
        char[] charOutput = new char[length];
        for (int i = 0; i < length; i++)
        {
            int selector = random.Next(65, 122);
            if ((selector > 90 && selector < 97) || (selector > 110 && selector < 121))
            {
                selector += 10;
            }
            else if (selector > 121)
            {
                selector = 64;
            }
            charOutput[i] = (char)selector;
        }
        //charOutput = "test@123";
        return new string(charOutput);
    }
    public DataTable getAlreadyExistRows(DataTable dTable, string colName)
    {
        ArrayList incorrectList = new ArrayList();

        foreach (DataRow dr in dTable.Rows)
        {
            incorrectList.Add(dr);
        }

        foreach (DataRow dtRow in incorrectList)
        {
            string mobile = dtRow["MOBILE"].ToString();
            string cscid = dtRow["CSCID"].ToString();



            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "csc.checkSubCSCMobile");
            MyCommand.Parameters.AddWithValue("@p_mobile", mobile);
            MyCommand.Parameters.AddWithValue("@p_cscid", cscid);

            dt = bll.SelectAll(MyCommand);


            if (dt.Rows.Count > 0)
            {
                
               
            }

            else
            {
                // CSCID and Mobile combination already exists
                dTable.Rows.Remove(dtRow);
            }
        }

        return dTable;
    }
    public DataTable getFilteredRows(DataTable dTable, string colName)
    {
        ArrayList incorrectList = new ArrayList();

        foreach (DataRow dr in dTable.Rows)
        {
            incorrectList.Add(dr);
        }

        foreach (DataRow dtRow in incorrectList)
        {
            string mobile = dtRow["MOBILE"].ToString();
            string cscid = dtRow["CSCID"].ToString();

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "csc.checkSubCSCMobile");
            MyCommand.Parameters.AddWithValue("@p_mobile", mobile);
            MyCommand.Parameters.AddWithValue("@p_cscid", cscid);

            dt = bll.SelectAll(MyCommand);


            if (dt.Rows.Count > 0)
            {
                
                dTable.Rows.Remove(dtRow);
            }
        }

        return dTable;
    }
    public DataTable RemoveDuplicateRows(DataTable dTable, string colName)
    {
        Hashtable hTable = new Hashtable();
        ArrayList duplicateList = new ArrayList();

        foreach (DataRow dtRow in dTable.Rows)
        {
            if (hTable.Contains(dtRow[colName]))
            {
                duplicateList.Add(dtRow);
            }
            else
            {
                hTable.Add(dtRow[colName], string.Empty);
            }
        }

        foreach (DataRow dtRow in duplicateList)
        {
            dTable.Rows.Remove(dtRow);
        }

        return dTable;
    }
    public DataTable getDuplicateRows(DataTable dTable, string colName)
    {
        Hashtable hTable = new Hashtable();
        ArrayList duplicateList = new ArrayList();
        ArrayList removeAccurateRow = new ArrayList();

        foreach (DataRow dtRow in dTable.Rows)
        {
            if (hTable.Contains(dtRow[colName]))
            {
                duplicateList.Add(dtRow);
            }
            else
            {
                hTable.Add(dtRow[colName], string.Empty);
                removeAccurateRow.Add(dtRow);
            }
        }

        foreach (DataRow dtRow in removeAccurateRow)
        {
            dTable.Rows.Remove(dtRow);
        }

        return dTable;
    }
    public DataTable getIncorrectRows(DataTable dTable, string colName)
    {
        ArrayList incorrectList = new ArrayList();

        foreach (DataRow dr in dTable.Rows)
        {
            incorrectList.Add(dr);
        }

        foreach (DataRow dtRow in incorrectList)
        {
            string mobile = dtRow["MOBILE"].ToString();
            string email = dtRow["EMAIL"].ToString();
            string district = dtRow["DISTRICT"].ToString();
            string state = dtRow["STATE"].ToString();

            DataTable dt = new DataTable();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.f_get_statesdetail");
            MyCommand.Parameters.AddWithValue("@p_statename", state);

            dt = bll.SelectAll(MyCommand);



            string statecode = dt.Rows[0]["state_code"].ToString();

            DataTable dt1 = new DataTable();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.f_get_district_by_districtname");
            MyCommand.Parameters.AddWithValue("@p_statecode", statecode);
            MyCommand.Parameters.AddWithValue("@p_districtname", district);

            dt1 = bll.SelectAll(MyCommand);

            if (mobile.Length == 10 && _validation.isValideMailID(email) && dt.Rows.Count > 0 && dt1.Rows.Count > 0)
            {
                // Row is considered correct, remove it from the DataTable
                dTable.Rows.Remove(dtRow);
            }
        }

        return dTable;
    }
    private DataTable getCorrectRows(DataTable dTable, string colName)
    {
        ArrayList incorrectList = new ArrayList();

        foreach (DataRow dr in dTable.Rows)
        {
            incorrectList.Add(dr);
        }

        foreach (DataRow dtRow in incorrectList)
        {
            string mobile = dtRow["MOBILE"].ToString();
            string email = dtRow["EMAIL"].ToString();
            string district = dtRow["DISTRICT"].ToString();
            string state = dtRow["STATE"].ToString();

            DataTable dt = new DataTable();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.f_get_statesdetail");
            MyCommand.Parameters.AddWithValue("@p_statename", state);

            dt = bll.SelectAll(MyCommand);
            string statecode = dt.Rows[0]["state_code"].ToString();

            DataTable dt1 = new DataTable();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.f_get_district_by_districtname");
            MyCommand.Parameters.AddWithValue("@p_statecode", statecode);
            MyCommand.Parameters.AddWithValue("@p_districtname", district);

            dt1 = bll.SelectAll(MyCommand);

            if (mobile.Length == 10 && _validation.isValideMailID(email) && dt.Rows.Count > 0 && dt1.Rows.Count > 0)
            {
                // Do nothing, the row is considered correct
            }
            else
            {
                dTable.Rows.Remove(dtRow);
            }
        }

        return dTable;
    }


    private void InsertRecords(DataTable accurateData, DataTable duplicateData, DataTable incorrectData, DataTable alreadyExistData, string insertedBy, string ipAddress, string fileName, int accurateRecordsCount, int duplicateRecordsCount, int incorrectRecordsCount, int alreadyExistRecordsCount)
    {
        StringWriter swStringWriter = new StringWriter();
        using (swStringWriter)
        using (StringWriter swStringWriterForAccurate = new StringWriter())
        {
            DataSet ds = new DataSet("Root");
            accurateData.TableName = "SUBCSCDATA";
            ds.Tables.Add(accurateData);
            ds.WriteXml(swStringWriterForAccurate);

            using (StringWriter swStringWriterForDuplicate = new StringWriter())
            {
                ds = new DataSet("Root");
                duplicateData.TableName = "SUBCSCDUPLICATEDATA";
                ds.Tables.Add(duplicateData);
                ds.WriteXml(swStringWriterForDuplicate);

                using (StringWriter swStringWriterForIncorrect = new StringWriter())
                {
                    ds = new DataSet("Root");
                    incorrectData.TableName = "SUBCSCINCORRECTDATA";
                    ds.Tables.Add(incorrectData);
                    ds.WriteXml(swStringWriterForIncorrect);

                    using (StringWriter swStringWriterForAlreadyExist = new StringWriter())
                    {
                        ds = new DataSet("Root");
                        alreadyExistData.TableName = "SUBCSCALRAEDYEXISTDATA";
                        ds.Tables.Add(alreadyExistData);
                        ds.WriteXml(swStringWriterForAlreadyExist);

                        // Insert Data

                        MyCommand = new NpgsqlCommand();
                        MyCommand.Parameters.Clear();
                        MyCommand.Parameters.AddWithValue("@StoredProcedure", "csc.insertsubcscdataindraft");
                        MyCommand.Parameters.AddWithValue("p_insertedby", insertedBy);
                        MyCommand.Parameters.AddWithValue("p_accuratedata", swStringWriterForAccurate.ToString());
                        MyCommand.Parameters.AddWithValue("p_duplicatedata", swStringWriterForDuplicate.ToString());
                        MyCommand.Parameters.AddWithValue("p_incorrectdata", swStringWriterForIncorrect.ToString());
                        MyCommand.Parameters.AddWithValue("p_alreadyexistdata", swStringWriterForAlreadyExist.ToString());
                        MyCommand.Parameters.AddWithValue("p_ipaddress", ipAddress);
                        MyCommand.Parameters.AddWithValue("p_filename", fileName);
                        MyCommand.Parameters.AddWithValue("p_correctdatacount", Convert.ToInt16(accurateRecordsCount));
                        MyCommand.Parameters.AddWithValue("p_incorrectdatacount", Convert.ToInt16(incorrectRecordsCount));
                        MyCommand.Parameters.AddWithValue("p_dupliactedatacount", Convert.ToInt16(duplicateRecordsCount));
                        MyCommand.Parameters.AddWithValue("p_alreadyexistdatacount", Convert.ToInt16(alreadyExistRecordsCount));
                        DataTable dtt2 = new DataTable();
                        dtt2 = bll.SelectAll(MyCommand);
                        if (dtt2.TableName == "Success")
                        {
                            string msg = "Agent List Excel Uploaded Successfully Please check the last uploaded excel <br/> Correct Records -" + accurateRecordsCount;
                            msg += "<br/> Incorrect Records -" + incorrectRecordsCount;
                            msg += "<br/> Duplicate Records -" + duplicateRecordsCount;
                            msg += "<br/> Already Exist Records -" + alreadyExistRecordsCount;

                            if (dtt2.Rows.Count > 0)
                            {
                                foreach (DataRow dr in dtt2.Rows)
                                {
                                    //comm.SendSubagPWDSMS(dr["NAME"].ToString(), dr["USER_CODE"].ToString(), dr["PWD"].ToString(), dr["MOBILE"].ToString(), 18);
                                    //comm.SendSubagPWDEMail(dr["NAME"].ToString(), dr["USER_CODE"].ToString(), dr["PWD"].ToString(), dr["EMAIL"].ToString());
                                    MyCommand = new NpgsqlCommand();
                                    MyCommand.Parameters.Clear();
                                    MyCommand.Parameters.AddWithValue("@StoredProcedure", "csc.updatepasswordstatus");
                                    MyCommand.Parameters.AddWithValue("p_agentcode", dr["usercode"].ToString());
                                    DataTable dtt3 = new DataTable();
                                    dtt3 = bll.SelectAll(MyCommand);
                                }
                            }

                            SuccessMesgBox(msg);
                            GetCscUploadSheetDetails();
                            GetCscSubAgDetails();
                        }
                        else
                        {
                            Errormsg("Something went wrong, please try again");
                        }
                    }
                }
            }
        }
    }

    private void UpdateCSCDetails()
    {
        try
        {
            string Mpwd = RandomString(6);

            string MpwdEncrypted = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(Mpwd, "SHA1");

            string IPAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(IPAddress))
            {
                IPAddress = Request.ServerVariables["REMOTE_ADDR"];
            }

            DataTable dt = new DataTable();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "csc.agentaction");
            MyCommand.Parameters.AddWithValue("@p_action", Session["Status"]);
            MyCommand.Parameters.AddWithValue("@p_agentcode", Session["Agcode"]);
            MyCommand.Parameters.AddWithValue("@p_pwd", MpwdEncrypted);
            MyCommand.Parameters.AddWithValue("@p_updateby", Session["_UserCode"]);
            MyCommand.Parameters.AddWithValue("@p_ipaddress", IPAddress);
            MyCommand.Parameters.AddWithValue("@p_statusreason", txtdeactivatereason.Text);

            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {

                    if (Session["Status"].ToString() == "A")
                    {
                        SuccessMesgBox("CSC Successfully Activate.");
                    }
                    if (Session["Status"].ToString() == "D")
                    {
                        SuccessMesgBox("CSC Successfully Deactivate.");

                    }
                    if (Session["Status"].ToString() == "C")
                    {
                        // comm.ChangedPassword_By_ADM_SMS(spResult.Value.ToString(), Mpwd, txtmobileno.Text.Trim(), 14);
                        // comm.ChangedPassword_By_ADM_Email(spResult.Value.ToString(), Mpwd, txtemail.Text.Trim());
                        SuccessMesgBox("CSC Password Successfully Changed. Userid and Password sent by SMS and email ");

                    }

                    InitControl();
                    GetCscSubAgDetails();
                    //SuccessMesgBox(message);
                }
            }
        }
        catch (Exception ex)
        {
            // Handle the exception
        }
    }

    private void GetCscUploadSheetDetails()
    {
        try
        {

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "csc.subcscuploaddatalogs");
            MyCommand.Parameters.AddWithValue("@p_uploadedby", Session["_UserCode"]);

            dt = bll.SelectAll(MyCommand);


            if (dt.Rows.Count > 0)
            {
                lbtnIncorrect.Text = dt.Rows[0]["val_incorrect_data"].ToString();
                lbtnDuplicate.Text = dt.Rows[0]["val_duplicate_data"].ToString();
                lbtnAlready.Text = dt.Rows[0]["val_already_exist"].ToString();
                lblCorrect.Text = dt.Rows[0]["val_correct_data"].ToString();
                lblFileName.Text = dt.Rows[0]["file_name"].ToString();
                lblDate.Text = dt.Rows[0]["val_upload_date"].ToString();
                lbldraftid.Text = dt.Rows[0]["val_id"].ToString();
                pnlExcelWithData.Visible = true;
                pnlExcelNoData.Visible = false;
            }
            else
            {
                pnlExcelWithData.Visible = false;
                pnlExcelNoData.Visible = true;
            }
        }

        catch (Exception ex)
        {
            // Handle the exception or log it as needed
        }
    }


    private void SaveAgentLimit()
    {

        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "csc.insertsubcsclimit");
        MyCommand.Parameters.AddWithValue("@p_csccode", Session["_UserCode"]);
        MyCommand.Parameters.AddWithValue("@p_agentcode", Session["Agcode"]);
        MyCommand.Parameters.AddWithValue("@p_amount", txtmaximum.Text);

        dt = bll.SelectAll(MyCommand);
        if (dt.Rows.Count > 0)
        {
            message = "Sub CSC Wallet Recharge Successfully";
            SuccessMesgBox(message);
            MpAgentLimit.Hide();

        }
        else
        {
            message = "Something went wrong please try again";
            Errormsg("message");
        }
        GetCscSubAgDetails();
    }



    private void loadAgentData()
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "csc.subagentdetailsbysearch");
        MyCommand.Parameters.AddWithValue("@p_search", txtSearch.Text);
        MyCommand.Parameters.AddWithValue("@p_mainagent", Session["_UserCode"]);
        dt = bll.SelectAll(MyCommand);
        if (dt.Rows.Count > 0)
        {
            grvCSCDeatails1.DataSource = dt;
            grvCSCDeatails1.DataBind();
            grvCSCDeatails1.UseAccessibleHeader = true;
            grvCSCDeatails1.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        else
        {

            Errormsg("No Data Found");
        }
    }
    #endregion

    #region "Events"
    protected void lbtnManual_Click(object sender, EventArgs e)
    {
        pnlDetailsShowHide(pnlManualRegistraion);
        btnCssView(lbtnManual);
    }
    protected void lbtnAuto_Click(object sender, EventArgs e)
    {
        pnlDetailsShowHide(pnlAutoRegistraion);
        btnCssView(lbtnAuto);
    }
    protected void grvCSCDeatails1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvCSCDeatails1.PageIndex = e.NewPageIndex;
        GetCscSubAgDetails();
    }
    protected void grvCSCDeatails1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbtnActive = (LinkButton)e.Row.FindControl("lbtnActive");
            LinkButton lbtnDeactive = (LinkButton)e.Row.FindControl("lbtnDeactive");

            lbtnActive.Visible = false;
            lbtnDeactive.Visible = false;

            DataRowView rowView = (DataRowView)e.Row.DataItem;
            if (rowView["val_status"].ToString() == "A")
            {
                lbtnDeactive.Visible = true;
            }
            else if (rowView["val_status"].ToString() == "D")
            {
                lbtnActive.Visible = true;
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (IsValidAgValue() == false)
        {
            return;
        }

        Session["ActionType"] = "SU";
        lblConfirmation.Text = "Do you want to save ?";
        mpConfirmation.Show();
    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        if (Session["ActionType"].ToString() == "Active")
        {
            Session["Status"] = "A";
            UpdateCSCDetails();
        }
        else if (Session["ActionType"].ToString() == "Deactive")
        {
            Session["Status"] = "D";
            UpdateCSCDetails();
        }
        else if (Session["ActionType"].ToString() == "ChgPwd")
        {
            Session["Status"] = "C";
            UpdateCSCDetails();
        }
        else if (Session["ActionType"].ToString() == "SU")
        {
            Savedetails();
        }

    }
    protected void ddlStates_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadDistrict(ddlStates.SelectedValue, ddlDistricts);
    }
    protected void grvCSCDeatails1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        txtmaximum.Text = "";
        // rdbdaily.Checked = false;
        // rdbmonthly.Checked = false;
        // rdbyearly.Checked = false;

        int index = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "UpdateDetails")
        {
            Session["Agcode"] = grvCSCDeatails1.DataKeys[index].Values["val_agent_code"].ToString();
            if (grvCSCDeatails1.DataKeys[index].Values["csc_id"] == DBNull.Value)
            {
                txtcscid.Text = "";
                txtcscid.Enabled = true;
            }
            else
            {
                txtcscid.Text = grvCSCDeatails1.DataKeys[index].Values["csc_id"].ToString();
                txtcscid.Enabled = false;
            }
            txtcscname.Text = grvCSCDeatails1.DataKeys[index].Values["ag_name"].ToString();
            txtcscpersonname.Text = grvCSCDeatails1.DataKeys[index].Values["contact_person"].ToString();
            txtemail.Text = grvCSCDeatails1.DataKeys[index].Values["email_id"].ToString();
            txtmobileno.Text = grvCSCDeatails1.DataKeys[index].Values["mobile_no"].ToString();
            txtAddress.Text = grvCSCDeatails1.DataKeys[index].Values["val_address"].ToString();
            txtpincode.Text = grvCSCDeatails1.DataKeys[index].Values["val_pincode"].ToString();
            //string stata = "0" + grvCSCDeatails1.DataKeys[index].Values["state_code"].ToString();
            //ddlStates.SelectedValue = "0" + grvCSCDeatails1.DataKeys[index].Values["state_code"].ToString();
           ddlStates.SelectedValue = grvCSCDeatails1.DataKeys[index].Values["state_code"].ToString();
            loadDistrict(ddlStates.SelectedValue, ddlDistricts);
            ddlDistricts.SelectedValue = grvCSCDeatails1.DataKeys[index].Values["district_code"].ToString();

            btnUpdate.Visible = true;
            btnSave.Visible = false;
            pnlDetailsShowHide(pnlManualRegistraion);
            btnCssView(lbtnManual);
            DisableFields();
            // Session("Agtype") = grvCSCDeatails1.DataKeys[index].Values["AGENTCODE"].ToString
            // Session("_RNDIDENTIFIERAGENTAUTH") = _SecurityCheck.GeneratePassword(10, 5)
            // Response.Redirect("CSCMainAgDailyTransaction.aspx")
        }

        if (e.CommandName == "Active")
        {
            Session["Agcode"] = grvCSCDeatails1.DataKeys[index].Values["val_agent_code"].ToString();
            Session["ActionType"] = "Active";
            lblConfirmation.Text = "Do you want to Activate CSC ?";
            mpConfirmation.Show();
        }

        if (e.CommandName == "Deactive")
        {
            Session["Agcode"] = grvCSCDeatails1.DataKeys[index].Values["val_agent_code"].ToString();
            Session["ActionType"] = "Deactive";
            lblConfirmation.Text = "Do you want to Deactivate CSC ?";
            mpDeactivation.Show();
        }

        if (e.CommandName == "ChngePwd")
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            Session["Agcode"] = grvCSCDeatails1.DataKeys[row.RowIndex]["val_agent_code"].ToString();
            Session["ActionType"] = "ChgPwd";
            lblConfirmation.Text = "Do you want to Change Password ?";
            mpConfirmation.Show();
        }

        if (e.CommandName == "Limit")
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            Session["Agcode"] = grvCSCDeatails1.DataKeys[row.RowIndex]["val_agent_code"].ToString();
            lblAgmimitmsg.Text = grvCSCDeatails1.DataKeys[index].Values["ag_name"].ToString() + "(" + grvCSCDeatails1.DataKeys[row.RowIndex]["val_agent_code"].ToString() + ") Amount Allocation";
            classWalletAgent obj = new classWalletAgent();
            DataTable dtWallet = obj.GetWalletDetailDt(Session["_UserCode"].ToString());

            if (dtWallet.Rows.Count > 0)
            {
                lblbalance.Text = "Your Wallet Balance <b>" + dtWallet.Rows[0]["currentbalanceamount"].ToString() + "</b>";

            }

            MpAgentLimit.Show();
        }

    }

    protected void lbtnUploadExcel_Click(object sender, EventArgs e)
    {
        try
        {
            string INSERTEDBY = Session["_UserCode"].ToString();
            string IPAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(IPAddress))
            {
                IPAddress = Request.ServerVariables["REMOTE_ADDR"];
            }

            // Save the uploaded Excel file.
            string filePath = Server.MapPath("~/Files/") + Path.GetFileName(FileUpload1.PostedFile.FileName);

            string fileName = FileUpload1.PostedFile.FileName;

            if (string.IsNullOrEmpty(fileName))
            {
     
                Errormsg("Please Choose Excel File!!");
                return;
            }

            FileUpload1.SaveAs(filePath);

            DataTable dt = new DataTable();

            // Open the Excel file using ClosedXML.
            using (var workBook = new XLWorkbook(filePath))
            {
                // Read the first Sheet from Excel file.
                var workSheet = workBook.Worksheet(1);
                bool firstRow = true;

                foreach (var row in workSheet.Rows())
                {
                    // Use the first row to add columns to DataTable.
                    if (firstRow)
                    {
                        foreach (var cell in row.Cells())
                        {
                            dt.Columns.Add(cell.Value.ToString());
                        }
                        firstRow = false;
                    }
                    else
                    {
                        // Add rows to DataTable.
                        dt.Rows.Add();
                        int i = 0;
                        foreach (var cell in row.Cells())
                        {
                            dt.Rows[dt.Rows.Count - 1][i] = cell.Value.ToString();
                            i++;
                        }
                    }
                }
            }

            // Remove Empty Rows From Excel
            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            {
                DataRow row = dt.Rows[i];
                if (row[0] == null || string.IsNullOrEmpty(row[0].ToString()))
                {
                    dt.Rows.Remove(row);
                }
            }

            // Checking DataTable is not empty
            if (dt.Rows.Count < 1)
            {
         
                Errormsg("Excel Sheet is Empty");
                return;
            }

            foreach (DataRow row in dt.Rows)
            {
                if (row["CSCID"].ToString().Length != 12)
                {
                
                    Errormsg("In excel sheet CSC ID should be 12 characters");
                    return;
                }
            }

            DataTable dtCopy = new DataTable();
            // Create copy of original datatable (structure)
            dtCopy = dt.Clone();
            // Copy data
            dtCopy = dt.Copy();

            DataTable getCorrectDt = new DataTable();
            getCorrectDt = getCorrectRows(dt, "MOBILE");

            DataTable getIncorrectDt = new DataTable();
            getIncorrectDt = getIncorrectRows(dtCopy, "MOBILE");

            DataTable AccurateDataDt = new DataTable();
            AccurateDataDt = getCorrectDt.Clone();
            AccurateDataDt = getCorrectDt.Copy();

            DataTable RemoveDuplicateDt = new DataTable();
            RemoveDuplicateDt = RemoveDuplicateRows(getCorrectDt, "MOBILE");

            DataTable getDuplicateDt = new DataTable();
            getDuplicateDt = getDuplicateRows(AccurateDataDt, "MOBILE");

            DataTable AccurateDataDt1 = new DataTable();
            AccurateDataDt1 = RemoveDuplicateDt.Clone();
            AccurateDataDt1 = RemoveDuplicateDt.Copy();

            DataTable getAlreadyExistData = new DataTable();
            getAlreadyExistData = getAlreadyExistRows(RemoveDuplicateDt, "MOBILE");

            DataTable getFilteredData = new DataTable();
            getFilteredData = getFilteredRows(AccurateDataDt1, "MOBILE");

            DataColumn newColumnPwd = new DataColumn("Pwd", typeof(string));
            getFilteredData.Columns.Add(newColumnPwd);
            DataColumn newColumnEncPwd = new DataColumn("EncPwd", typeof(string));
            getFilteredData.Columns.Add(newColumnEncPwd);

            string Mpwd;
            string MpwdEncrypted;
            foreach (DataRow dr in getFilteredData.Rows)
            {
                Mpwd = "";
                Mpwd = "test@123";// RandomString(6);
                MpwdEncrypted = "";
                MpwdEncrypted = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(Mpwd, "SHA1");
                dr["Pwd"] = Mpwd;
                dr["EncPwd"] = MpwdEncrypted;
            }

            int AccurateRecords = AccurateDataDt1.Rows.Count;
            int DuplicateRecords = getDuplicateDt.Rows.Count;
            int IncorrectRecords = getIncorrectDt.Rows.Count;
            int AlreadyExistRecords = getAlreadyExistData.Rows.Count;

            InsertRecords(getFilteredData, getDuplicateDt, getIncorrectDt, getAlreadyExistData, INSERTEDBY, IPAddress, fileName, AccurateRecords, DuplicateRecords, IncorrectRecords, AlreadyExistRecords);
        }
        catch (Exception ex)
        {
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "ErrorMessage('" + ex.Message + "');", true);

            Errormsg(ex.Message);
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {

        Response.Clear();
        Response.ContentType = "application/xlsx";
        Response.AppendHeader("Content-Disposition", "attachment; filename=Sample.xlsx");
        Response.TransmitFile("~/File/Sample.xlsx");
        Response.End();


    }

    protected void lbtnLimitSave_Click(object sender, EventArgs e)
    {
        decimal amtallocation = 0;

        if (!string.IsNullOrEmpty(txtmaximum.Text))
        {
            amtallocation = Convert.ToDecimal(txtmaximum.Text);
        }

        if (amtallocation == 0)
        {
            lbllimitmsg.Text = "Enter amount to be allocation";
            lbllimitmsg.Visible = true;
            MpAgentLimit.Show();
            return;
        }
        
        classWalletAgent obj = new classWalletAgent();
        DataTable dtWallet = obj.GetWalletDetailDt(Session["_UserCode"].ToString());

        if (dtWallet.Rows.Count > 0)
        {
            decimal curntblnce = Convert.ToDecimal(dtWallet.Rows[0]["currentbalanceamount"].ToString());



            if (amtallocation > curntblnce)
            {
                lbllimitmsg.Text = "Allocation amount should be less than current balance amount";
                lbllimitmsg.Visible = true;
                MpAgentLimit.Show();
                return;
            }
        }

        SaveAgentLimit();
    }

    protected void lbtnLimitReset_Click(object sender, EventArgs e)
    {
        txtmaximum.Text = "";
    }

    protected void lbtnChangepass_Click(object sender, EventArgs e)
    {

    }

    protected void lbtnResetChangepass_Click(object sender, EventArgs e)
    {
        txtnewpass.Text = "";
    }

    protected void lbtnSearch_Click(object sender, EventArgs e)
    {
        loadAgentData();
    }



    protected void lbtnIncorrect_Click(object sender, EventArgs e)
    {
        GetRecords(int.Parse(lbldraftid.Text), "INCORRECT");
        lblmsg.Text = "Incorrect Data";
        lblsumrymsg.Text = "(Please Check - Mobile,Email,State and District details)";
    }

    protected void lbtnDuplicate_Click(object sender, EventArgs e)
    {
        GetRecords(int.Parse(lbldraftid.Text), "DUPLICATE");
        lblmsg.Text = "Duplicate Data";
        lblsumrymsg.Text = "(Please Check - Mobile and CSCID details)";
    }

    protected void lbtnAlready_Click(object sender, EventArgs e)
    {
        GetRecords(int.Parse(lbldraftid.Text), "ALREADY EXIST");
        lblmsg.Text = "Already Exist Data";
        lblsumrymsg.Text = "(Please Check - Mobile and CSCID details)";
    }


    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (!IsValidAgValue())
        {
            return;
        }

        Session["ActionType"] = "SU";
        lblConfirmation.Text = "Do you want to Update ?";
        mpConfirmation.Show();

    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        InitControl();
        EnableFields();
    }

    private void GetRecords(int draftId, string type)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "csc.getdraftrecordsformaincsc");
            MyCommand.Parameters.AddWithValue("@p_drafttype", type);
            MyCommand.Parameters.AddWithValue("@p_draftid", draftId);
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                gvDraftData.DataSource = dt;
                gvDraftData.DataBind();
                mpDraftData.Show();
            }
            else
            {
                Errormsg("No Data Found");

            }
        
        }
        catch (Exception ex)
        {
            Errormsg("Something Went Wrong");
        }
    }

    

    protected void lbtnyes_Click(object sender, EventArgs e)
    {
        if (Session["ActionType"].ToString() == "Deactive")
        {
            if (string.IsNullOrEmpty(txtdeactivatereason.Text))
            {
                string msg = "Enter reason of deactivation";
                Errormsg(msg);
                return;
            }

            Session["Status"] = "D";
            UpdateCSCDetails();
        }

    }

    #endregion
}