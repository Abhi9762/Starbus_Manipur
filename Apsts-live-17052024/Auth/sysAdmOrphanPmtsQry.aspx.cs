using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_sysAdmOrphanPmtsQry : System.Web.UI.Page
{
    private NpgsqlCommand MyCommand;
    sbValidation _SecurityCheck = new sbValidation();
    sbSecurity _security = new sbSecurity();
    HDFC hdfc = new HDFC();
    sbValidation _validation = new sbValidation();
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    ClassDoubleVerification_Refund obj = new ClassDoubleVerification_Refund();

    protected void Page_Load(object sender, EventArgs e)
    {
        Session["_moduleName"] = "Orphan Transaction Settlement";
        checkForSecurity();
        if (!IsPostBack)
        {
            if (Session["MPMTGATEWAYID"] != null && Session["MPMTGATEWAYID"].ToString() != "" && Session["Mtxndate"] != null && Session["Mtxndate"].ToString() != "")
            {
                lblTxnDate.Text = Session["Mtxndate"].ToString();
                lblPg.Text = Session["MPMTGATEWAYNAME"].ToString();
                OrphanTransactions(Session["MPMTGATEWAYID"].ToString(), Session["Mtxndate"].ToString());
            }
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
            Response.Redirect("sessionTimeout.aspx");
        }
        if (_security.isSessionExist(Session["_RNDIDENTIFIERSADM"]) == true)
        {
            Session["_RNDIDENTIFIERSADM"] = Session["_RNDIDENTIFIERSADM"];
        }
        else
        {
            Response.Redirect("sessionTimeout.aspx");
        }
        if (_security.checkvalidation() == false)
        {
            Response.Redirect("Errorpage.aspx");
        }
        if (_security.CheckOldUserLogin(Session["_UserCode"].ToString()) == false)
        {
            Response.Redirect("Errorpage.aspx");
        }
    }
    private void Successmsg(string msg)
    {
        string popup = _popup.modalPopupSmall("S", "Success", msg, "Close");
        Response.Write(popup);
    }
    private void Errormsg(string msg)
    {
        string popup = _popup.modalPopupSmall("D", "Please Check", msg, "Close");
        Response.Write(popup);
    }

    #region "Methods"
    private void OrphanTransactions(string pmtid, string trnsdate)
    {
        try
        {
            gvOrphanTrans.Visible = false;
            dvOrphanTrans.Visible = true;
            btnStart.Visible = false;
            lblmsg.Text = "(Start Query Button will be available only if pending transactions are there. To get the list of pending transactions please select date & Payment Gateway and click on Search button. )";

            dt = obj.getOrphanTxnDtl(pmtid, trnsdate);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    gvOrphanTrans.DataSource = dt;
                    gvOrphanTrans.DataBind();
                    gvOrphanTrans.Visible = true;
                    dvOrphanTrans.Visible = false;
                    btnStart.Visible = true;
                    lblmsg.Text = "Please start the process by searching the orphan transactions.";
                    //forDatatable();
                }

            }
        }
        catch (Exception ex)
        {

        }
    }

    private async void startQuery()
    {
        if (gvOrphanTrans.Rows.Count <= 0)
        {
            Errormsg("Sorry, no transaction is available for verification");
            return;
        }
        gvOrphanTrans.AllowPaging = false;
        Int32 nooftrasaction = gvOrphanTrans.Rows.Count;
        Int64 MDBLVERIFICATIONLOGID;
        // -----------------Step 1 Record Count to Double Verification Hit a day

        DataTable dt;
        dt = obj.dblstartday(Session["Mtxndate"].ToString(), Session["MPMTGATEWAYID"].ToString(), nooftrasaction, Session["_UserCode"].ToString(), "U");
        if (dt.Rows.Count > 0)
            MDBLVERIFICATIONLOGID = Convert.ToInt64(dt.Rows[0]["logid"].ToString());
        else
        {
            Errormsg("Sorry due to connectivity /other problem process can not be completed for " + Session["Mtxndate"].ToString() + "  You can verify by searching again.1");
            return;
        }

        int i, j, k = 0;
        string Mtranno = "";
        string MfinalStatus = "";
        string Db_verification_refNo = "";
        string Doubleverifyby = Session["_UserCode"].ToString();
        j = gvOrphanTrans.Rows.Count;
        for (i = 0; i <= gvOrphanTrans.Rows.Count - 1; i++)
        {
            Mtranno = "";
            Db_verification_refNo = "";
            MfinalStatus = "";
            Mtranno = gvOrphanTrans.DataKeys[i].Values["txn_refno"].ToString();
            Db_verification_refNo = obj.dblRequest(Mtranno, MDBLVERIFICATIONLOGID, "U");

            if (Session["MPMTGATEWAYID"].ToString() == "3")
            {
                // Billdesk Double Verification
                MfinalStatus = obj.BillDeskStatusAPI(Mtranno, MDBLVERIFICATIONLOGID, Db_verification_refNo, Doubleverifyby);
            }

            if (Session["MPMTGATEWAYID"].ToString() == "4")
            {
                // Billdesk Double Verification
                MfinalStatus = await obj.BillDeskStatusAPINew(Mtranno, MDBLVERIFICATIONLOGID.ToString(), Db_verification_refNo, Doubleverifyby);
            }

            if (Session["MPMTGATEWAYID"].ToString() == "1")
            {
                // Phonepe Double Verification
                MfinalStatus = obj.PhonePeStatusAPI(Mtranno, MDBLVERIFICATIONLOGID.ToString(), Db_verification_refNo, Doubleverifyby, Session["Mtxndate"].ToString());
            }



            if (MfinalStatus != "Error" && MfinalStatus != "0002" && MfinalStatus != "0399" && MfinalStatus != "" && MfinalStatus != null)
                k = k + 1;
        }
        string mm;
        // --------------------------------------------------
        if (k == j)
        {
            mm = "Congratulation, all transasctions have been verified for " + Session["Mtxndate"].ToString() + "<BR/> You can verify by searching again.";
            // ---------------------------------------------------
            Errormsg(mm);
            OrphanTransactions(Session["MPMTGATEWAYID"].ToString(), Session["Mtxndate"].ToString());
        }
        else
        {
            mm = "Sorry due to connectivity /other problem process can not be completed for " + Session["Mtxndate"].ToString() + "  You can verify by searching again.2" + Mtranno + "----" + MfinalStatus;
            Errormsg(mm);
            OrphanTransactions(Session["MPMTGATEWAYID"].ToString(), Session["Mtxndate"].ToString());
        }
    }


    #endregion

    #region "Events"
    protected void lbtnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("sysAdmPmtGateway.aspx");
    }
    protected void gvOrphanTrans_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvOrphanTrans.PageIndex = e.NewPageIndex;
        OrphanTransactions(Session["MPMTGATEWAYID"].ToString(), Session["Mtxndate"].ToString());
    }
    protected void btnStart_Click(object sender, EventArgs e)
    {
        if (gvOrphanTrans.Rows.Count <= 0)
        {
            Errormsg("Sorry, no transaction is available for verification");
            return;
        }
        lblConfirmation.Text = "Do you want to proceed for query with Payment Gateway and settle the transactions?";
        mpConfirmation.Show();
    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        startQuery();
    }


    #endregion
}