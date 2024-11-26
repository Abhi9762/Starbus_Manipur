using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_AgConfirm : System.Web.UI.Page
{
    NpgsqlCommand MyCommand = new NpgsqlCommand();
    private sbBLL bll = new sbBLL();
    DataTable dt = new DataTable();
    sbValidation _validation = new sbValidation();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    private sbCommonFunc _common = new sbCommonFunc();
    protected void Page_Load(object sender, EventArgs e)
    {

        ticketDetails(Session["strTicketNoUp"].ToString());
        GetFareDetails(Session["strTicketNoUp"].ToString().Trim(), lblFareAmt, lblReservationCharge,lblcommission, lblTotal);
        FillTaxGrid(Session["strTicketNoUp"].ToString().Trim());

    }
    private void ticketDetails(string tcktno)
    {
        try
        {
            wsClass obj = new wsClass();
            DataTable dt = obj.getTicketDetails_byTicket(tcktno);

            if (dt.Rows.Count > 0)
            {
                lblJourneyDate.Text = dt.Rows[0]["journey_date"].ToString();
                lblJourneyTime.Text = dt.Rows[0]["departuretime"].ToString();
                lblSource.Text = dt.Rows[0]["src"].ToString();
                lblDestination.Text = dt.Rows[0]["destinatio"].ToString();
                lblBoarding.Text = dt.Rows[0]["board"].ToString();
                lblServiceType.Text = dt.Rows[0]["service_code"].ToString() + " " + dt.Rows[0]["tripdirection"].ToString() + " - " + dt.Rows[0]["service_type_name"].ToString();

                gvJourneyUp.DataSource = dt;
                gvJourneyUp.DataBind();
                // ApplicableCharges(dt.Rows[0]["SERVICECODE"].ToString(), dt.Rows[0]["TRIP_DIRECTION"].ToString());
            }
            else
            {
                Session["_ErrorMsg"] = "Something went wrong. <br/>Please contact the software admin with errorcode CntrTkctBkngPmtCnfrm03-";
                Response.Redirect("errorpage.aspx");
            }
        }
        catch (Exception ex)
        {
            Session["_ErrorMsg"] = "Something went wrong. <br/>Please contact the software admin with errorcode CntrTkctBkngPmtCnfrm03-";
            Response.Redirect("errorpage.aspx");
        }
    }

    private void ApplicableCharges(string servicecode, string servicetype)
    {
        //try
        //{
        //    wsClass obj = new wsClass();
        //    dt = obj.GetApplicableChargeYNByTicket(servicecode, servicetype);

        //    if (dt.Rows.Count > 0 && dt.Rows[0]["Fareyn"].ToString() == "N")
        //    {
        //        FareYNmsg = "(Fare will be collected by conductor in the bus)";
        //        fareYN.Visible = true;
        //    }
        //    else
        //    {
        //        FareYNmsg = "";
        //        fareYN.Visible = false;
        //    }
        //}
        //catch (Exception ex)
        //{

        //}
    }



    protected void GetFareDetails(string strTicketNo, Label lblFareAmt, Label lblRegAmt,Label lblcommission, Label lblNetAmt)
    {
        try
        {
            wsClass obj = new wsClass();
            dt = obj.getFareDetails_byTicket(strTicketNo);
            foreach (DataRow drow in dt.Rows)
            {
                lblFareAmt.Text = drow["total_fareamt"].ToString();
                lblRegAmt.Text = drow["busresrvation_amt"].ToString();
                lblcommission.Text = drow["commission"].ToString();
                Session["SMStotalfareamt"] = drow["total_fareamt"].ToString();
                Session["SMSbusrescharges"] = drow["busresrvation_amt"].ToString();
                lblNetAmt.Text = drow["net_fare"].ToString();
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void FillTaxGrid(string strTicketNo)
    {
        try
        {
            wsClass obj = new wsClass();
            dt = obj.getTaxDetails_byTicket(strTicketNo);
            if (dt.Rows.Count > 0)
            {
                grdtax.DataSource = dt;
                grdtax.DataBind();
            }
            else
            {
                grdtax.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Session["_ErrorMsg"] = "Something went wrong. <br/>Please contact the software admin with errorcode CntrTkctBkngPmtCnfrm03-";
            Response.Redirect("errorpage.aspx");
        }
    }

    protected bool IsValidBalance()
    {
        try
        {
            classWalletAgent obj = new classWalletAgent();
            DataTable dt_wallet = new DataTable();
            dt_wallet = obj.GetWalletDetailDt(Session["_UserCode"].ToString());

            if (dt_wallet.Rows.Count > 0)
            {
                decimal currentBalance = Convert.ToDecimal(dt_wallet.Rows[0]["currentbalanceamount"].ToString());

                if (currentBalance > Convert.ToDecimal(lblFareAmt.Text.Trim()))
                {
                    return true;
                }
                else if (currentBalance == 0)
                {
                    return false;
                }
            }
            return false;
        }
        catch (Exception ex)
        {
            Session["_ErrorMsg"] = "Something went wrong. <br/>Please contact the software admin with errorcode AgntCnfrm04-";
            Response.Redirect("errorpage.aspx");
            return false;
        }
    }

    protected void btnproceed_Click(object sender, EventArgs e)
    {
        if (!IsValidBalance())
        {
            return;
        }

        lblConfirmation.Text = "I Have Received Amount " + lblTotal.Text + ", Want To Proceed ?";
        mpConfirmation.Show();
        Session["_Type"] = "S";

    }
    private void OpenPage(string PageName)
    {
        tkt.Src = PageName;
        mpticket.Show();
    }

    private bool UpdateTransactionDetails()
    {
        try
        {
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "agent_wallet.sp_agentpaymentdone_new");
            MyCommand.Parameters.AddWithValue("@p_ticketno", Session["strTicketNoUp"]);
            MyCommand.Parameters.AddWithValue("@p_agentcode", Session["_usercode"]);
            string dt = bll.UpdateAll(MyCommand);
            if (dt == "Success")
            {

                return true;


            }
        }
        catch (Exception ex)
        {
            Session["_ErrorMsg"] = "Something went wrong. <br/>Please contact the software admin with error code AgntCnfrm05-";
            Response.Redirect("errorpage.aspx");
            return false;
        }
        return false;
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


    protected void btnyes_Click(object sender, EventArgs e)
    {
        pnldetails.Visible = true;
        pnlPrntYN.Visible = false;
        btnproceed.Visible = false;
        btnprint.Visible = true;

    }

    protected void btnno_Click(object sender, EventArgs e)
    {
        Response.Redirect("AgentDashboard.aspx");
    }

    protected void btnprint_Click(object sender, EventArgs e)
    {


        Session["p_ticketNo"] = Session["_ctzAuthTicketNoUp"];
        Session["_CalledBy"] = "TranComplete";
        OpenPage("../E_Ticket.aspx");
        pnldetails.Visible = false;
        pnlPrntYN.Visible = true;
    }




    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {

        if (Session["_Type"].ToString() == "S")
        {
            if (UpdateTransactionDetails() == true)
            {

                //comn.SendTicketConfirm_SMSnEMAIL(Session["strTicketNoUp"], "B", 2);
                Session["p_ticketNo"] = Session["strTicketNoUp"];
                OpenPage("../E_Ticket.aspx");


                pnldetails.Visible = false;
                pnlPrntYN.Visible = true;
                lblpnr.Text = " " + Session["strTicketNoUp"];
            }
            else
            {
                Session["_ErrorMsg"] = " <br/> Error, While Confirm Ticket Details";
                Response.Redirect("errorpage.aspx");
            }
        }
        
    }
}