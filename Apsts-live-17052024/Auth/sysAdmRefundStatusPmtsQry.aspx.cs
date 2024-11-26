using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_sysAdmRefundStatusPmtsQry : System.Web.UI.Page
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
    ClassDoubleVerification_Refund dbl_refund = new ClassDoubleVerification_Refund();
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["_moduleName"] = "Refund Transaction Settlement";
        checkForSecurity();
        if (!IsPostBack)
        {
            Session["Query"] = "";
            if (Session["MPMTGATEWAYID"] != null && Session["MPMTGATEWAYID"].ToString() != "" && Session["Mtxndate"] != null && Session["Mtxndate"].ToString() != "")
            {
                lblTxnDate.Text = Session["Mtxndate"].ToString();
                lblPg.Text = Session["MPMTGATEWAYNAME"].ToString();
                RefundInitiatedTran(Session["MPMTGATEWAYID"].ToString(), Session["Mtxndate"].ToString());
            }

        }
    }

    #region "Common Methods"
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
    #endregion

    #region "Methods"   
    private void RefundInitiatedTran(string pgid, string date)
    {
        try
        {
            gvRefundStatusPending.Visible = false;
            dvRefund.Visible = true;
            btnStart.Visible = false;

            dt = dbl_refund.getRefundedTxnDtls(pgid, date);
            if (dt.Rows.Count > 0)
            {
                gvRefundStatusPending.DataSource = dt;
                gvRefundStatusPending.DataBind();
                gvRefundStatusPending.Visible = true;
                dvRefund.Visible = false;
                btnStart.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Errormsg("Something Went Wrong");
        }
    }
    private async void StartQuery()
    {
        string MfinalStatus = "Error";
        string Mtranno, MCancelRef, MRefundRef, Mtranamt, Mtxn_date, Mrefamt;
        try
        {
            int selectedIndex = int.Parse(hdgrdindex.Value.ToString());
            Mtranno = (gvRefundStatusPending.DataKeys[selectedIndex]["trans_refno"]).ToString();
            MCancelRef = (gvRefundStatusPending.DataKeys[selectedIndex]["trans_cancel_refno"]).ToString();
            MRefundRef = (gvRefundStatusPending.DataKeys[selectedIndex]["refund_ref_no"]).ToString();
            Mtranamt = (gvRefundStatusPending.DataKeys[selectedIndex]["trans_amount"]).ToString();
            Mtxn_date= (gvRefundStatusPending.DataKeys[selectedIndex]["trans_date"]).ToString();
            Mrefamt = (gvRefundStatusPending.DataKeys[selectedIndex]["refund_amount"]).ToString();

            if (Session["MPMTGATEWAYID"].ToString() == "1")
            {
                //***** Phone Pay Refund
                MfinalStatus = dbl_refund.PhonePeRefundStatusAPI(Mtranno, MCancelRef, MRefundRef, Mrefamt, Session["_UserCode"].ToString());

                if (MfinalStatus.ToUpper() == "SUCCESS")
                {
                    Errormsg("Refund Status request has successfully been processed.");
                }
                else
                {
                    Errormsg("Refund Status Not Processed"+ MfinalStatus);
                }
            }
            if (Session["MPMTGATEWAYID"].ToString() == "4")
            {
                //***** Phone Pay Refund
                 MfinalStatus = await dbl_refund.RefundStatusResponseAPI(MCancelRef,Mtranno,Mtxn_date,Mtranamt,Mrefamt, Session["_UserCode"].ToString());

                if (MfinalStatus.ToUpper() == "SUCCESS")
                {
                    Errormsg("Refund Status request has successfully been processed.");
                }
                else
                {
                    Errormsg("Refund Status Not Processed" + MfinalStatus);
                }
            }
            RefundInitiatedTran(Session["MPMTGATEWAYID"].ToString(), Session["Mtxndate"].ToString());
        }
        catch (Exception ex)
        {
            Errormsg("Refund Not Initiated" + ex.Message);
        }
    }
    private async void StartQueryProcess()
    {
        string MfinalStatus = "Error";
        string Mtranno, MCancelRef, MRefundRef, Mtranamt, Mtxn_date, Mrefamt;

        if (gvRefundStatusPending.Rows.Count <= 0)
        {
            Errormsg("Sorry, no transaction is available for verification");
            return;
        }
        int j = gvRefundStatusPending.Rows.Count;
        int k = 0;
        for (var i = 0; i <= gvRefundStatusPending.Rows.Count - 1; i++)
        {

            Mtranno = (gvRefundStatusPending.DataKeys[i]["trans_refno"]).ToString();
            MCancelRef = (gvRefundStatusPending.DataKeys[i]["trans_cancel_refno"]).ToString();
            MRefundRef = (gvRefundStatusPending.DataKeys[i]["refund_ref_no"]).ToString();
            Mtranamt = (gvRefundStatusPending.DataKeys[i]["trans_amount"]).ToString();
            Mtxn_date = (gvRefundStatusPending.DataKeys[i]["trans_date"]).ToString();
            Mrefamt = (gvRefundStatusPending.DataKeys[i]["refund_amount"]).ToString();


            


            if (Session["MPMTGATEWAYID"].ToString() == "1")
            {
                //***** Phone Pay Refund
                MfinalStatus = dbl_refund.PhonePeRefundStatusAPI(Mtranno, MCancelRef, MRefundRef, Mrefamt, Session["_UserCode"].ToString());

                if (MfinalStatus.ToUpper() == "SUCCESS")
                {
                    k = k + 1;
                }
            }
            if (Session["MPMTGATEWAYID"].ToString() == "4")
            {
                //***** Billdesk Refund
                MfinalStatus = await dbl_refund.RefundStatusResponseAPI(MCancelRef, Mtranno, Mtxn_date, Mtranamt, Mrefamt, Session["_UserCode"].ToString());

                if (MfinalStatus.ToUpper() == "SUCCESS")
                {
                    k = k + 1;
                }
            }
        }

        RefundInitiatedTran(Session["MPMTGATEWAYID"].ToString(), Session["Mtxndate"].ToString());
        string mm = "<b>" + k.ToString() + "</b> out of <b>" + j.ToString() + "</b> transasctions Refund Staus has successfully been Proceed.";
        Errormsg(mm);
    }


    #endregion

    #region "Events"
    protected void lbtnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("sysAdmPmtGateway.aspx");
    }
    protected void gvRefundStatusPending_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvRefundStatusPending.PageIndex = e.NewPageIndex;
        RefundInitiatedTran(Session["MPMTGATEWAYID"].ToString(), Session["Mtxndate"].ToString());
    }
    protected void gvRefundStatusPending_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "RefundStatus")
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int i = row.RowIndex;
            hdgrdindex.Value = i.ToString();
            Session["Query"] = "";
            lblConfirmation.Text = "Do you want to proceed for Refund Status the transactions ?";
            mpConfirmation.Show();
        }
    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        if (Session["Query"].ToString() == "Q")
        {
            StartQueryProcess();
        }
        else
        {
            StartQuery();
        }
    }
    protected void btnStart_Click(object sender, EventArgs e)
    {
        Session["Query"] = "Q";
        lblConfirmation.Text = "Do you want to proceed for Refund the transactions ?";
        mpConfirmation.Show();
    }
   
   



    #endregion








}