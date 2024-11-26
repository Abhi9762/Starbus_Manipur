using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class Auth_CashierDashboard : BasePage
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    string current_date = DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("yyyy");
    ReportDocument cryrpt = new ReportDocument();
    string rpt = System.Web.HttpContext.Current.Server.MapPath("~/Auth/Reports/ChestDailyTransaction.rpt");

    protected void Page_Load(object sender, EventArgs e)
    {


        checkForSecurity();
        Session["_moduleName"] = "Cashier Dashboard";
        lbldatesummary.Text = DateTime.Now.ToString();
        if (!IsPostBack)
        {
            Random randomclass = new Random();
            Session["rndNoCheck"] = randomclass.Next().ToString();
            hidtoken.Value = Session["rndNoCheck"].ToString();
            if (Session["_Count"] == null)
            {
                checkChestOpenClose();
            }
            txttransdate.Text = current_date;
            loadHead(ddhead);
            loadSubHead(ddhead.SelectedValue, ddsubhead);
            loadOpeningClosingDetails();
            //waybill
            loadWaybillNoList(ddlWaybillNo);
            loadtodayTransactionsAmt();
            loadtodayTransactiondetails();
        }
    }

    private void checkForSecurity()
    {
        //if (Session.IsNewSession == false || Request.ServerVariables["HTTP_REFERER"].Length < 1)
        //{
        //    Response.Redirect("../Errorpage.aspx");
        //}

        if (_security.isSessionExist(Session["_UserCode"]) == true)
        {
            Session["_UserCode"] = Session["_UserCode"];
        }
        else
        {
            Response.Redirect("../sessionTimeout.aspx");
        }
        if (_security.isSessionExist(Session["_RNDIDENTIFIERCASHIER"]) == true)
        {
            Session["_RNDIDENTIFIERCASHIER"] = Session["_RNDIDENTIFIERCASHIER"];
        }
        else
        {
            Response.Redirect("../sessionTimeout.aspx");
        }
        if (_security.checkvalidation() == false)
        {
            Response.Redirect("../Errorpage.aspx");
        }
        if (_security.CheckOldUserLogin(Session["_UserCode"].ToString()) == false)
        {
            Response.Redirect("../Errorpage.aspx");
        }
    }

    #region "Common Method"
    private void CsrfTokenValidate()
    {
        if (hidtoken.Value != Session["rndNoCheck"].ToString())
        {
            Response.Redirect("errorpage.aspx");
            return;
        }
    }
    private void Errormsg(string msg)
    {

        string popup = _popup.modalPopupSmall("W", "Please Check", msg, "Close");
        Response.Write(popup);
    }
    private void Successmsg(string msg)
    {
        string popup = _popup.modalPopupSmall("S", "Success", msg, "OK");
        Response.Write(popup);
    }
    private void loadHead(DropDownList ddhead)//M1
    {
        try
        {
            ddhead.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_head");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddhead.DataSource = dt;
                    ddhead.DataTextField = "head_name";
                    ddhead.DataValueField = "head_id";
                    ddhead.DataBind();
                }
            }
            else
            {
                _common.ErrorLog("CashierDashboard-M1", dt.TableName);
                Errormsg(dt.TableName);
            }

            ddhead.SelectedValue = "2";
            ddhead.Enabled = false;

        }
        catch (Exception ex)
        {
            Errormsg(ex.Message.ToString());
            _common.ErrorLog("CashierDashboard.aspx-0001", ex.Message.ToString());
            ddhead.Items.Insert(0, "SELECT");
            ddhead.Items[0].Value = "0";
            ddhead.SelectedIndex = 0;
        }
    }
    private void loadSubHead(string headvalue, DropDownList ddsubhead)//M2
    {
        try
        {
            ddsubhead.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_subhead");
            MyCommand.Parameters.AddWithValue("p_head", Convert.ToInt16(headvalue));
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddsubhead.DataSource = dt;
                    ddsubhead.DataTextField = "sub_head_name";
                    ddsubhead.DataValueField = "sub_head_id";
                    ddsubhead.DataBind();
                }
            }
            else
            {
                _common.ErrorLog("CashierDashboard.aspx-0003", dt.TableName);
                Errormsg(dt.TableName);
            }
            ddsubhead.Items.Insert(0, "SELECT");
            ddsubhead.Items[0].Value = "0";
            ddsubhead.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message.ToString());
            _common.ErrorLog("CashierDashboard.aspx-0002", ex.Message.ToString());
            ddsubhead.Items.Insert(0, "SELECT");
            ddsubhead.Items[0].Value = "0";
            ddsubhead.SelectedIndex = 0;
        }
    }
    private void loadtodayTransactionsAmt()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_today_collection");
            MyCommand.Parameters.AddWithValue("p_date", txttransdate.Text);
            MyCommand.Parameters.AddWithValue("p_chestid", Session["_ChestID"].ToString());
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    lbtodayamt.Text = dt.Rows[0]["amt"].ToString();
                }
                else
                {
                    lbtodayamt.Text = "0";
                }
            }
            else
            {
                Errormsg(dt.TableName);
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message.ToString());
            _common.ErrorLog("CashierDashboard.aspx-0004", ex.Message.ToString());
        }
    }
    private void loadtodayTransactiondetails()
    {
        try
        {
            gvTodayTransaction.Visible = false;
            pnlNodata.Visible = true;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_today_collection_details");
            MyCommand.Parameters.AddWithValue("p_date", txttransdate.Text);
            MyCommand.Parameters.AddWithValue("p_chestid", Session["_ChestID"].ToString());
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    gvTodayTransaction.DataSource = dt;
                    gvTodayTransaction.DataBind();
                    gvTodayTransaction.Visible = true;
                    pnlNodata.Visible = false;
                }
                else
                {
                    gvTodayTransaction.Visible = false;
                    pnlNodata.Visible = true;
                }
            }
            else
            {
                Errormsg(dt.TableName);
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message.ToString());
            _common.ErrorLog("CashierDashboard.aspx-0005", ex.Message.ToString());
        }
    }
    private void downloadReport()
    {
        try
        {

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_cashier_txn_report");
            MyCommand.Parameters.AddWithValue("p_date", txttransdate.Text);
            MyCommand.Parameters.AddWithValue("p_chestid", Session["_ChestID"].ToString());
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                cryrpt.Load(rpt);
                cryrpt.SetDataSource(dt);
                XmlDocument doc = new XmlDocument();
                doc.Load(Server.MapPath("../CommonData.xml"));
                XmlNodeList title = doc.GetElementsByTagName("dept_Name_en");
                CrystalDecisions.CrystalReports.Engine.TextObject objtxtorgname = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txtorgname"];
                objtxtorgname.Text = title.Item(0).InnerXml;

                CrystalDecisions.CrystalReports.Engine.TextObject objtxtdate = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txtDate"];
                objtxtdate.Text = txttransdate.Text;

                CrystalDecisions.CrystalReports.Engine.TextObject objopbal = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txtopenBlnc"];
                objopbal.Text = " Opening Balance of the day (A) " + dt.Rows[0]["op_bal"].ToString() + " ₹";

                CrystalDecisions.CrystalReports.Engine.TextObject objclobale = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txtcloseBlnc"];
                objclobale.Text = " Closing Balance of the day (A+B) " + dt.Rows[0]["clo_bal"].ToString() + " ₹";

                CrystalDecisions.CrystalReports.Engine.TextObject objurl = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txturl"];
                objurl.Text = "Downloaded From -" + Request.Url.AbsoluteUri;



                cryrpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Context.Response, true, "DailyTansaction-" + DateTime.Now.ToString() + DateTime.Now); ;

            }
            else
            {
                Errormsg(dt.TableName);
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message.ToString());
            _common.ErrorLog("CashierDashboard.aspx-0006", ex.Message.ToString());
        }
    }
    #endregion

    #region "Chest Open/Close"
    private void checkChestOpenClose()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_chest_check_open_close");
            MyCommand.Parameters.AddWithValue("p_userid", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("p_chestid", Session["_ChestID"].ToString());
            dt = bll.SelectAll(MyCommand);

            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {

                    Session["_Count"] = "1";
                    lblMsg.Text = Session["_ChestEmp"].ToString() + " " + dt.Rows[0]["messages"].ToString();
                    // lblOpenBlnc.Text = dt.Rows(0)("openblnc")
                    lblCloseBlnc.Text = dt.Rows[0]["closing_bal"].ToString();
                    //Errormsg(dt.Rows[0]["closing_bal"].ToString());
                    //return;
                    if (Convert.ToDecimal(dt.Rows[0]["closing_bal"].ToString()) > 0)
                        trChestAmt.Visible = true;
                    else
                        trChestAmt.Visible = false;
                    string st = dt.Rows[0]["status_"].ToString();


                    Session["Status"] = st;
                    if (st.Trim() == "O")
                    {
                        if (Session["_UserCode"].ToString() == dt.Rows[0]["open_by"].ToString())
                        {
                            Session["SameUser"] = "Y";
                            lblMsg.Text = Session["_ChestEmp"].ToString() + " You have already opened this chest.";
                            lblmsg1.Text = "";
                            btnYes.Visible = false;
                            btnNo.Text = "OK";
                        }
                        else
                        {
                            Session["SameUser"] = "N";
                            btnsave.Visible = true;
                            btnNo.Text = "NO";
                            lblmsg1.Text = "Do you Want to Close ?";
                        }
                    }
                    if (st.Trim() == "C")
                    {
                        Session["SameUser"] = "";
                        lblmsg1.Text = "Do you Want to Open ?";
                    }

                    mpOpenClose.Show();
                }
            }
            else
            {

                Errormsg(dt.TableName);
            }


        }
        catch (Exception ex)
        {
            Errormsg(ex.Message.ToString());
            _common.ErrorLog("CashierDashboard.aspx-0007", ex.Message.ToString());
        }
    }
    protected void btnYes_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_chest_insert_update");
            MyCommand.Parameters.AddWithValue("p_userid", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("p_chestid", Session["_ChestID"].ToString());
            MyCommand.Parameters.AddWithValue("p_status", Session["Status"].ToString());
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                loadOpeningClosingDetails();
            }
            else
            {

                Errormsg(dt.TableName);
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message.ToString());

        }
    }
    protected void btnNo_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (Session["Status"].ToString() == "O")
        {
            if (Session["SameUser"].ToString() == "Y")
            {
                mpOpenClose.Hide();
                return;
            }
            else
                Response.Redirect("logout.aspx");
        }
        if (Session["Status"].ToString() == "C")
        {
            if (Session["SameUser"].ToString() == "Y")
            {
                mpOpenClose.Hide();
                return;
            }
            else
                Response.Redirect("logout.aspx");
        }
    }
    private void loadOpeningClosingDetails()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_chest_opening_closing");
            MyCommand.Parameters.AddWithValue("p_chestid", Session["_ChestID"].ToString());
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                lblopenby.Text = dt.Rows[0]["open_by"].ToString();
                lblopenat.Text = dt.Rows[0]["open_datetime"].ToString();
                lblopenbal.Text = dt.Rows[0]["open_bal"].ToString() + " ₹";
                lblcloseby.Text = dt.Rows[0]["closed_by"].ToString();
                lblcloseat.Text = dt.Rows[0]["closed_datetime"].ToString();
                lblclosebal.Text = dt.Rows[0]["close_bal"].ToString() + " ₹";
                lblcurrentbal.Text = dt.Rows[0]["current_bal"].ToString();
                lbltotaldeposit.Text = dt.Rows[0]["deposit_bal"].ToString() + " ₹";
                lbltotalCollection.Text = dt.Rows[0]["collect_bal"].ToString() + " ₹";
            }
            else
            {
                Errormsg(dt.TableName);
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message.ToString());
            _common.ErrorLog("CashierDashboard.aspx-0008", ex.Message.ToString());
        }
    }

    #endregion

    #region "Counter Deposit"
    private void loadcounter(DropDownList ddcounter)//M4
    {
        try
        {
            ddcounter.Items.Clear();
            ddemp.Items.Clear();
            totalpayamt.Text = "0";
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_get_chest_counter");
            MyCommand.Parameters.AddWithValue("p_ofcid", Session["_ChestOfcid"]);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddcounter.DataSource = dt;
                    ddcounter.DataTextField = "counter_name";
                    ddcounter.DataValueField = "counter_id";
                    ddcounter.DataBind();
                }
            }
            else
            {
                _common.ErrorLog("CashierDashboard-M4", dt.TableName);
                Errormsg(dt.TableName);
            }
            ddcounter.Items.Insert(0, "SELECT");
            ddcounter.Items[0].Value = "0";
            ddcounter.SelectedIndex = 0;

            ddemp.Items.Insert(0, "SELECT");
            ddemp.Items[0].Value = "0";
            ddemp.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message.ToString());
            _common.ErrorLog("CashierDashboard.aspx-0008", ex.Message.ToString());
            ddcounter.Items.Insert(0, "SELECT");
            ddcounter.Items[0].Value = "0";
            ddcounter.SelectedIndex = 0;
            ddemp.Items.Insert(0, "SELECT");
            ddemp.Items[0].Value = "0";
            ddemp.SelectedIndex = 0;
        }
    }
    protected void ddcounter_SelectedIndexChanged(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        LoadEmployee(ddcounter.SelectedValue, ddemp);
    }
    private void LoadEmployee(string counterid, DropDownList ddemp)//M5
    {
        try
        {
            ddemp.Items.Clear();
            totalpayamt.Text = "0";
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_counter_employee");
            MyCommand.Parameters.AddWithValue("p_counterid", counterid);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddemp.DataSource = dt;
                    ddemp.DataTextField = "emp_f_name";
                    ddemp.DataValueField = "attachedempcode";
                    ddemp.DataBind();
                    totalpayamt.Text = dt.Rows[0]["balanceamt"].ToString();
                    if (Convert.ToDecimal(totalpayamt.Text.ToString()) == 0)
                    {
                        btnsave.Enabled = false;
                    }
                }
            }
            else
            {
                _common.ErrorLog("CashierDashboard.aspx-0009", dt.TableName);
                Errormsg(dt.TableName);
            }
            ddemp.Items.Insert(0, "SELECT");
            ddemp.Items[0].Value = "0";
            ddemp.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message.ToString());
            _common.ErrorLog("CashierDashboard.aspx-0010", ex.Message.ToString());
            ddemp.Items.Insert(0, "SELECT");
            ddemp.Items[0].Value = "0";
            ddemp.SelectedIndex = 0;
        }
    }

    #endregion

    #region "Cash Deposit"
    protected void btnsave_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (isvalidvalue() == false)
        {
            return;
        }
        if (ddsubhead.SelectedValue == "14")
        {
            if (isExcessPaymentVerified() == false)
            {
                Errormsg("Excess Payment Not Verified. Please Contact Station Incharge.");
                return;
            }
            if (isExtraKmVerified() == false)
            {
                Errormsg("Extra Km. Not Verified.Please Contact Station Incharge.");
                return;
            }
            Session["Action"] = "Waybill";
            lblConfirmation.Text = "You are receiving ₹" + txtWaybillTotAmt.Text.ToString() + ". Please confirm and proceed for receipt printing.";
        }
        else
        {
            Session["Action"] = "Others";
            lblConfirmation.Text = "You are receiving ₹" + tbamount.Text.ToString() + ". Please confirm and proceed for receipt printing.";
        }
        mpConfirmation.Show();
    }
    private bool isvalidvalue()
    {
        try
        {
            int msgcnt = 0;
            string msg = "";
            if (ddhead.SelectedValue.ToString() == "0")
            {
                msgcnt = msgcnt + 1;
                msg = msg + msgcnt.ToString() + ". Select Valid Head<br>";
            }
            if (ddsubhead.SelectedValue.ToString() == "0")
            {
                msgcnt = msgcnt + 1;
                msg = msg + msgcnt.ToString() + ". Select Valid Sub Head<br>";
            }
            else if (ddsubhead.SelectedValue == "14")
            {
                if (ddlWaybillNo.SelectedIndex == 0)
                {
                    msgcnt = msgcnt + 1;
                    msg = msg + msgcnt.ToString() + ". Select Waybill No<br>";
                }
                if (txtWaybillTotAmt.Text == "0")
                {
                    msgcnt = msgcnt + 1;
                    msg = msg + msgcnt.ToString() + ". Enter Valid Amount<br>";
                }
                if (txtWEmpMobile.Text.Length < 10)
                {
                    msgcnt = msgcnt + 1;
                    msg = msg + msgcnt.ToString() + ". Invalid Employee Mobile Number<br>";
                }
            }
            else if (ddhead.SelectedValue == "2")
            {
                if (ddsubhead.SelectedValue == "1" | ddsubhead.SelectedValue == "2")
                {
                    if (ddcounter.SelectedValue.ToString() == "0")
                    {
                        msgcnt = msgcnt + 1;
                        msg = msg + msgcnt.ToString() + ". Select Valid Counter<br>";
                    }
                    if (ddemp.SelectedValue.ToString() == "0")
                    {
                        msgcnt = msgcnt + 1;
                        msg = msg + msgcnt.ToString() + ". Select Valid Employee<br>";
                    }
                    if (tbamount.Text == "0")
                    {
                        msgcnt = msgcnt + 1;
                        msg = msg + msgcnt.ToString() + ". Enter Valid Amount<br>";
                    }
                    else if (Convert.ToDecimal(tbamount.Text.ToString()) > Convert.ToDecimal(totalpayamt.Text.ToString()))
                    {
                        msgcnt = msgcnt + 1;
                        msg = msg + msgcnt.ToString() + ". Amount should be less then total payable amount<br>";
                    }
                }
                else
                {
                    if (ddsubhead.SelectedValue == "4")
                    {
                        if (txtOldReceiptNumber.Text.Length <= 0)
                        {
                            msgcnt = msgcnt + 1;
                            msg = msg + msgcnt.ToString() + ". Invalid Old Receipt Number<br>";
                        }
                    }
                    if (ddsubhead.SelectedValue == "5")
                    {
                        if (ddlmaxicab.SelectedValue.ToString() == "0")
                        {
                            msgcnt = msgcnt + 1;
                            msg = msg + msgcnt.ToString() + ". Select Valid Maxi Cab<br>";
                        }
                    }

                    if (ddsubhead.SelectedValue == "6" | ddsubhead.SelectedValue == "8")
                    {
                        if (txtBusNumber.Text.Length <= 0)
                        {
                            msgcnt = msgcnt + 1;
                            msg = msg + msgcnt.ToString() + ". Invalid Bus Number<br>";
                        }
                        if (txtWaybillnumber.Text.Length <= 0)
                        {
                            msgcnt = msgcnt + 1;
                            msg = msg + msgcnt.ToString() + ". Invalid Waybill Number<br>";
                        }
                    }
                    if (ddsubhead.SelectedValue == "3")
                    {
                        if (txtBusNumber.Text.Length <= 0)
                        {
                            msgcnt = msgcnt + 1;
                            msg = msg + msgcnt.ToString() + ". Invalid Bus Number<br>";
                        }
                    }
                    if (rbtnEmployee.SelectedValue == "U")
                    {
                        if (ddlutcemp.SelectedValue.ToString() == "0")
                        {
                            msgcnt = msgcnt + 1;
                            msg = msg + msgcnt.ToString() + ". Select Valid Employee<br>";
                        }
                    }
                    if (rbtnEmployee.SelectedValue == "O")
                    {
                        if (txtname.Text.Length <= 0)
                        {
                            msgcnt = msgcnt + 1;
                            msg = msg + msgcnt.ToString() + ". Invalid Employee Name<br>";
                        }
                    }
                    if (txtmobileno.Text.Length < 10)
                    {
                        msgcnt = msgcnt + 1;
                        msg = msg + msgcnt.ToString() + ". Invalid Employee Mobile Number<br>";
                    }

                    if (txtAmount.Text == "0")
                    {
                        msgcnt = msgcnt + 1;
                        msg = msg + msgcnt.ToString() + ". Enter Valid Amount<br>";
                    }
                    else if (Convert.ToDecimal(txtAmount.Text.ToString()) <= 0)
                    {
                        msgcnt = msgcnt + 1;
                        msg = msg + msgcnt.ToString() + ". Enter Valid Amount<br>";
                    }
                    if (ddsubhead.SelectedValue == "12")
                    {
                        if (ddlIDProofType.SelectedValue.ToString() == "0")
                        {
                            msgcnt = msgcnt + 1;
                            msg = msg + msgcnt.ToString() + ". Select Valid ID Proof Type<br>";
                        }
                        if (txtIDProofNumber.Text.Length <= 0)
                        {
                            msgcnt = msgcnt + 1;
                            msg = msg + msgcnt.ToString() + ". Invalid ID Proof Number<br>";
                        }
                    }
                }
            }
            if (msgcnt > 0)
            {
                Errormsg(msg);
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message);
            return false;
        }
    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (Session["Action"].ToString() == "Waybill")
        {
            SaveWaybillCashDeposit();
        }
        else
        {
            SaveCashDeposit();
        }
    }
    private void SaveCashDeposit()//M8
    {
        try
        {
            int P_Head = Convert.ToInt32(ddhead.SelectedValue.ToString());
            int P_SubHead = Convert.ToInt32(ddsubhead.SelectedValue.ToString());
            string P_Counter = ddcounter.SelectedValue;
            decimal P_CntrAmt = Convert.ToDecimal(tbamount.Text.ToString());
            string P_CntrEmp = ddemp.SelectedValue;
            string P_Name = "";
            if (ddhead.SelectedValue == "2" && ddsubhead.SelectedValue == "1" || ddsubhead.SelectedValue == "2")
                P_Name = ddemp.SelectedItem.Text;
            else if (rbtnEmployee.SelectedValue == "U")
                P_Name = ddlutcemp.SelectedItem.Text;
            else
                P_Name = txtname.Text;

            string P_mobileno = txtmobileno.Text;
            decimal P_Amt = Convert.ToDecimal(txtAmount.Text.ToString());


            string P_oldReceiptNo = txtOldReceiptNumber.Text;
            string P_busnumber = txtBusNumber.Text;
            string P_waybillno = txtWaybillnumber.Text;
            string P_MaxiCab = ddlmaxicab.SelectedValue;

            string proofid = ddlIDProofType.SelectedValue.ToString();
            int P_idProofid = 0;
            if (proofid == "")
            {
                P_idProofid = 0;
            }
            else
            {
                P_idProofid = Convert.ToInt32(ddlIDProofType.SelectedValue.ToString());
            }


            string P_idproofno = txtIDProofNumber.Text;

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_cash_deposit");
            MyCommand.Parameters.AddWithValue("sp_mode", "C");
            MyCommand.Parameters.AddWithValue("p_headid", P_Head);
            MyCommand.Parameters.AddWithValue("p_subheadid", P_SubHead);
            MyCommand.Parameters.AddWithValue("p_counterid", P_Counter);
            MyCommand.Parameters.AddWithValue("p_amount", P_CntrAmt);
            MyCommand.Parameters.AddWithValue("p_empcode", P_CntrEmp);
            MyCommand.Parameters.AddWithValue("p_creditbyname", P_Name);
            MyCommand.Parameters.AddWithValue("p_creditbymobilenumber", P_mobileno);
            MyCommand.Parameters.AddWithValue("p_creditamount", P_Amt);
            MyCommand.Parameters.AddWithValue("p_chstid", Session["_ChestID"].ToString());
            MyCommand.Parameters.AddWithValue("p_usercode", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("p_oldreceiptno", P_oldReceiptNo);
            MyCommand.Parameters.AddWithValue("p_busnumber", lblBusNo.Text.ToString());
            MyCommand.Parameters.AddWithValue("p_waybillno", P_waybillno);
            MyCommand.Parameters.AddWithValue("p_maxicab", P_MaxiCab);
            MyCommand.Parameters.AddWithValue("p_idproofid", P_idProofid);
            MyCommand.Parameters.AddWithValue("p_idproofno", P_idproofno);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    Session["TransRefno"] = dt.Rows[0]["refno"].ToString();

                    //SuccessMesgBox("Successfully Submitted");
                    loadOpeningClosingDetails();
                    loadtodayTransactionsAmt();
                    loadtodayTransactiondetails();
                    resetControl();
                    eSlip.Text = "<embed src = \"../CashReceipt.aspx\" style=\"height: 80vh; width: 100%\" />";
                    mpShowCashslip.Show();
                }
            }
            else
                Errormsg("There Is some Error While saving - " + dt.TableName);
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message);
            _common.ErrorLog("CashierDashboard.aspx-0011", ex.Message.ToString());
        }
    }
    #endregion

    #region "Waybill"
    protected void loadWaybillNoList()
    {
        try
        {
            ddlWaybillNo.Items.Clear();

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_getwaybillsforcashier");
            MyCommand.Parameters.AddWithValue("p_office", Session["_LDepotCode"].ToString());
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlWaybillNo.DataSource = dt;
                    ddlWaybillNo.DataTextField = "waybillid";
                    ddlWaybillNo.DataValueField = "waybillid";
                    ddlWaybillNo.DataBind();
                }
            }
            ddlWaybillNo.Items.Insert(0, "Select");
            ddlWaybillNo.Items[0].Value = "0";
            ddlWaybillNo.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("CashierDashboard.aspx-0012", ex.Message.ToString());
            ddlWaybillNo.Items.Insert(0, "Select");
            ddlWaybillNo.Items[0].Value = "0";
            ddlWaybillNo.SelectedIndex = 0;
            return;
        }
    }
    protected void ddlWaybillNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (ddlWaybillNo.SelectedIndex > 0)
        {
            LoadWaybillEmployee(ddlWEmpName);
            loadWaybillDetails();

        }
        else
        {
            ddlWaybillNo.SelectedIndex = 0;
            divWServices.Visible = false;
            divWDepot.Visible = false;
            divWServCategory.Visible = false;
        }
    }
    private void LoadWaybillEmployee(DropDownList ddl)
    {
        try
        {
            txtWEmpMobile.Enabled = true;
            ddl.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_getwaybillcondtlist");
            MyCommand.Parameters.AddWithValue("sp_dutyrefno", ddlWaybillNo.SelectedValue);
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                ddl.DataSource = dt;
                ddl.DataTextField = "empname";
                ddl.DataValueField = "empcode";
                ddl.DataBind();
                txtWEmpMobile.Text = dt.Rows[0]["empmobileno"].ToString();
                txtWEmpMobile.Enabled = false;
            }
            else
            {
                ddl.Items.Insert(0, "Select Employee");
                ddl.Items[0].Value = "0";
                ddl.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("CashierDashboard.aspx-0013", ex.Message.ToString());
            ddl.Items.Insert(0, "Select Employee");
            ddl.Items[0].Value = "0";
            ddl.SelectedIndex = 0;
        }
    }
    protected void loadWaybillDetails()
    {
        try
        {
            lbtnViewWaybill.Visible = false;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_getwaybilldetailsforcashier");
            MyCommand.Parameters.AddWithValue("sp_waybillid", ddlWaybillNo.SelectedValue.Trim());
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    divWServices.Visible = true;
                    divWDepot.Visible = true;
                    divWServCategory.Visible = true;
                    lbtnViewWaybill.Visible = true;
                    lblServiceName.Text = dt.Rows[0]["service_name"].ToString();
                    lblDutyTime.Text = dt.Rows[0]["dutydate"].ToString();
                    lblBusNo.Text = dt.Rows[0]["busno"].ToString();
                    lblETM.Text = dt.Rows[0]["etmserialno"].ToString();
                    lblTotayWaybillAmount.Text = Convert.ToDecimal(dt.Rows[0]["totalamount"].ToString()).ToString("0.##");
                    txtWaybillTotAmt.Text = Convert.ToDecimal(dt.Rows[0]["totalamount"].ToString()).ToString("0.##");
                    try
                    {
                        ddlWEmpName.SelectedValue = dt.Rows[0]["cond1empcode"].ToString();
                        this.ddlWEmpName_SelectedIndexChanged(this.ddlWEmpName, System.EventArgs.Empty);
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("CashierDashboard.aspx-0013", ex.Message.ToString());
            return;
        }
    }
    protected void ddlWEmpName_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        CsrfTokenValidate();
        if (ddlWEmpName.SelectedValue != "0")
        {
            try
            {
                MyCommand = new NpgsqlCommand();
                MyCommand.Parameters.Clear();
                MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_getempdetails");
                MyCommand.Parameters.AddWithValue("p_empcode", ddlWEmpName.SelectedValue.Trim());
                dt = bll.SelectAll(MyCommand);
                if (dt.TableName == "Success")
                {
                    if (dt.Rows.Count > 0)
                    {
                        txtWEmpMobile.Text = dt.Rows[0]["mobilenumber"].ToString();
                        txtWEmpMobile.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                txtWEmpMobile.Text = "";
                txtWEmpMobile.Enabled = true;
            }
        }
        else
        {
            txtWEmpMobile.Text = "";
            txtWEmpMobile.Enabled = true;
        }
    }
    private void SaveWaybillCashDeposit()//M7
    {
        try
        {
            string p_ipaddress = HttpContext.Current.Request.UserHostAddress;
            int P_Head = Convert.ToInt32(ddhead.SelectedValue.ToString());
            int P_SubHead = Convert.ToInt32(ddsubhead.SelectedValue.ToString());
            string P_Counter = ddcounter.SelectedValue;
            if (tbamount.Text == "")
                tbamount.Text = "0";
            decimal P_CntrAmt = Convert.ToDecimal(tbamount.Text.ToString());
            string P_CntrEmp = ddemp.SelectedValue;
            string P_Name = "";
            P_Name = ddlWEmpName.SelectedItem.Text;
            string P_mobileno = txtWEmpMobile.Text;
            decimal P_Amt = Convert.ToDecimal(txtWaybillTotAmt.Text.ToString());
            string P_oldReceiptNo = txtOldReceiptNumber.Text;
            string P_waybillno = ddlWaybillNo.SelectedValue;
            string P_MaxiCab = ddlmaxicab.SelectedValue;
            int P_idProofid = 0; //Convert.ToInt16(ddlIDProofType.SelectedValue.ToString());
            string P_idproofno = ""; // txtIDProofNumber.Text;
            string UPDATEDBY = Session["_UserCode"].ToString();

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_waybillcashdeposit_new");
            MyCommand.Parameters.AddWithValue("p_mode", "C");
            MyCommand.Parameters.AddWithValue("p_headid", P_Head);
            MyCommand.Parameters.AddWithValue("p_subheadid", P_SubHead);
            MyCommand.Parameters.AddWithValue("p_counterid", P_Counter);
            MyCommand.Parameters.AddWithValue("p_amount", P_CntrAmt);
            MyCommand.Parameters.AddWithValue("p_empcode", P_CntrEmp);
            MyCommand.Parameters.AddWithValue("p_creditbyname", P_Name);
            MyCommand.Parameters.AddWithValue("p_creditbymobilenumber", P_mobileno);
            MyCommand.Parameters.AddWithValue("p_creditamount", P_Amt);
            MyCommand.Parameters.AddWithValue("p_chstid", Session["_ChestID"].ToString());
            MyCommand.Parameters.AddWithValue("p_usercode", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("p_oldreceiptno", P_oldReceiptNo);
            MyCommand.Parameters.AddWithValue("p_busnumber", lblBusNo.Text.ToString());
            MyCommand.Parameters.AddWithValue("p_waybillno", P_waybillno);
            MyCommand.Parameters.AddWithValue("p_maxicab", P_MaxiCab);
            MyCommand.Parameters.AddWithValue("p_idproofid", P_idProofid);
            MyCommand.Parameters.AddWithValue("p_idproofno", P_idproofno);
            MyCommand.Parameters.AddWithValue("p_remark", txtRemark.Text.ToString());
            MyCommand.Parameters.AddWithValue("p_updatedby", UPDATEDBY);
            MyCommand.Parameters.AddWithValue("p_ipaddress", p_ipaddress);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    Session["TransRefno"] = dt.Rows[0]["refno"].ToString();
                    // Successmsg("Successfully Submitted");
                    loadOpeningClosingDetails();
                    loadtodayTransactionsAmt();
                    loadtodayTransactiondetails();
                    resetControl();
                    eSlip.Text = "<embed src = \"../CashReceipt.aspx\" style=\"height: 80vh; width: 100%\" />";
                    mpShowCashslip.Show();
                }
            }
            else
                Errormsg("There Is some Error While saving - " + dt.TableName);
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message);
            _common.ErrorLog("CashierDashboard.aspx-0014", ex.Message.ToString());
        }
    }
    private bool isExtraKmVerified()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "etm.f_get_extra_waybill_km");
            MyCommand.Parameters.AddWithValue("p_waybill_no", ddlWaybillNo.SelectedValue);//
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    private bool isExcessPaymentVerified()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "etm.f_check_excess_amount_waybill");
            MyCommand.Parameters.AddWithValue("p_waybill_no", ddlWaybillNo.SelectedValue);//
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["currentstatus"].ToString() == "0")
                    {
                        return false;
                    }
                    else if (dt.Rows[0]["currentstatus"].ToString() == "1")
                    {
                        return true;
                    }
                    else if (dt.Rows[0]["currentstatus"].ToString() == "2")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    #endregion

    #region "Event"
    protected void ddhead_SelectedIndexChanged(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        loadSubHead(ddhead.SelectedValue, ddsubhead);
    }
    protected void ddsubhead_SelectedIndexChanged(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (ddsubhead.SelectedValue == "1" || ddsubhead.SelectedValue == "14")
        {
            lbtnViewWaybill.Visible = false;
            rbtnEmployee.SelectedValue = "O";
            ddlutcemp.Visible = false;
            txtname.Visible = true;
            rbtnEmployee.Enabled = true;
            pnlWaybillCash.Visible = false;
            ddlWaybillNo.SelectedIndex = 0;
            this.ddlWaybillNo_SelectedIndexChanged(this.ddlWaybillNo, System.EventArgs.Empty);
            ddlWEmpName.Visible = false;
            txtWEmpName.Visible = true;
            if (ddsubhead.SelectedValue == "1" | ddsubhead.SelectedValue == "2")
            {
                loadcounter(ddcounter);
                pnlCanteen.Visible = false;
                pnlcounter.Visible = true;
            }
            else if (ddsubhead.SelectedValue == "14")
            {
                loadWaybillNoList();
                pnlcounter.Visible = false;
                pnlCanteen.Visible = false;
                pnlWaybillCash.Visible = true;
                divWServices.Visible = false;
                divWDepot.Visible = false;
                divWServCategory.Visible = false;
                ddlWEmpName.Visible = true;
                txtWEmpName.Visible = false;
            }
            else
            {
                pnlcounter.Visible = false;
                pnlCanteen.Visible = true;

                Initc();
                if (ddsubhead.SelectedValue == "4")
                {
                    oldreeiptno.Visible = true;
                    rbtnEmployee.SelectedValue = "U";
                    //  allemployee(ddlutcemp);
                    ddlutcemp.Visible = true;
                    txtname.Visible = false;
                    rbtnEmployee.Enabled = false;
                }
                if (ddsubhead.SelectedValue == "5")
                    maxicab.Visible = true;
                if (ddsubhead.SelectedValue == "6" | ddsubhead.SelectedValue == "8")
                {
                    busno.Visible = true;
                    waybillno.Visible = true;
                    rbtnEmployee.SelectedValue = "U";
                    // allemployee(ddlutcemp);
                    ddlutcemp.Visible = true;
                    txtname.Visible = false;
                    rbtnEmployee.Enabled = false;
                }
                if (ddsubhead.SelectedValue == "12")
                {
                    idproof.Visible = true;
                    idproofno.Visible = true;
                }
                if (ddsubhead.SelectedValue == "3")
                {
                    busno.Visible = true;
                    rbtnEmployee.SelectedValue = "U";
                    // allemployee(ddlutcemp);
                    ddlutcemp.Visible = true;
                    txtname.Visible = false;
                    rbtnEmployee.Enabled = false;
                }
            }
        }
        else
        {
            Errormsg("Coming Soon");
        }

    }
    protected void lbtnbankdeposit_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Response.Redirect("cashierBankDeposit.aspx");
    }
    protected void lbtnsearch_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (txttransdate.Text == "")
        {
            Errormsg("Please Select Valid Date");
            return;
        }

        loadtodayTransactionsAmt();
        loadtodayTransactiondetails();
    }
    protected void gvTodayTransaction_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        CsrfTokenValidate();
        if (e.CommandName == "viewTrans")
        {
            int rowIndex = int.Parse(e.CommandArgument.ToString());
            string tripcode = gvTodayTransaction.DataKeys[rowIndex]["txnid"].ToString();

            Session["TransRefno"] = tripcode;

            eSlip.Text = "<embed src = \"../CashReceipt.aspx\" style=\"height: 80vh; width: 100%\" />";
            mpShowCashslip.Show();
        }
    }
    protected void gvTodayTransaction_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTodayTransaction.PageIndex = e.NewPageIndex;
        loadtodayTransactiondetails();
    }
    protected void lbtnDownloadExcel_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        downloadReport();
    }
    protected void lbtnViewWaybill_Click(object sender, EventArgs e)
    {
        Session["DUTYREFNO"] = ddlWaybillNo.SelectedValue.ToString();
        eJourneyDetails.Text = "<embed src = \"dashEtmCollectionNew.aspx\" style=\"height: 80vh; width: 100%\" />";
        mpExpenditureDetails.Show();
    }
    #endregion
    private void Initc()
    {
        oldreeiptno.Visible = false;
        maxicab.Visible = false;
        busno.Visible = false;
        waybillno.Visible = false;
        idproof.Visible = false;
        idproofno.Visible = false;
        txtmobileno.Text = "";
        txtmobileno.Enabled = true;
        rbtnEmployee.SelectedValue = "O";
        ddlutcemp.Visible = false;
        txtname.Visible = true;
    }
    private void loadWaybillNoList(DropDownList ddlWaybillNo)//M3
    {
        ddlWaybillNo.Items.Clear();
        ddlWaybillNo.Items.Insert(0, "SELECT");
        ddlWaybillNo.Items[0].Value = "0";
        ddlWaybillNo.SelectedIndex = 0;
    }
    public void resetControl()
    {
        tbamount.Text = "";
        loadHead(ddhead);
        loadSubHead(ddhead.SelectedValue, ddsubhead);
        loadcounter(ddcounter);
        LoadEmployee(ddcounter.SelectedValue, ddemp);
        pnlcounter.Visible = false;
        pnlCanteen.Visible = false;
        pnlWaybillCash.Visible = false;
    }
    public void openSubDetailsWindow(string MModuleName)
    {
        try
        {
            string murl;
            murl = MModuleName + "?rt=" + DateTime.Now.ToString();
            string b = Request.Browser.Type.Substring(0, 2);
            if (b.ToUpper().Trim() == "IE")
                Response.Write("<SCRIPT language='javascript'>window.showModalDialog('" + murl + "','name','dialogWidth:830px;dialogHeight:900px');</script>");
            else
            {
                // Dim url As String = "GenQrySchStages.aspx"
                string fullURL = "window.open('" + murl + "', '_blank', 'height=900,width=830,status=no,toolbar=no,menubar=no,location=no,scrollbars=no,resizable=no,titlebar=no' );";
                // ScriptManager.RegisterStartupScript(Me, GetType(String), "OPEN_WINDOW", fullURL, True)
                string script = "window.open('" + fullURL + "','')";
                if ((ClientScript.IsClientScriptBlockRegistered("NewWindow") == false))
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "NewWindow", fullURL, true); // 
            }
        }
        catch (Exception ex)
        {
        }
    }
    
}