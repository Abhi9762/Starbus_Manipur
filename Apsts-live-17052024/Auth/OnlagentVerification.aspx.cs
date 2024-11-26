using System;
using Npgsql;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CaptchaDLL;

public partial class Auth_OnlagentVerification : System.Web.UI.Page
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
        Session["_moduleName"] = "Online Agent Verification";
        if (!IsPostBack)
        {
         
            LoadCountAgents();
            Session["Status"] = "E";
            loadPendingAgents("1", "E");
            lblpendingheading.Text = "Pending For Verification <br/><span style='font-size:10pt;'>(Online Request Submitted ) </span>";
            Session["Base64String"] = null;
        }
    }

    #region Method


    private void loadrejectReason(DropDownList ddlreject)//M3
    {
        try
        {
            ddlreject.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "agent.get_OnlAgRejreson");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlreject.DataSource = dt;
                    ddlreject.DataTextField = "val_reason";
                    ddlreject.DataValueField = "reason_id";
                    ddlreject.DataBind();
                }
            }
            else
            {
                _common.ErrorLog("OnlAgentVerification", dt.TableName);
            }
            ddlreject.Items.Insert(0, "SELECT");
            ddlreject.Items[0].Value = "0";
            ddlreject.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlreject.Items.Insert(0, "SELECT");
            ddlreject.Items[0].Value = "0";
            ddlreject.SelectedIndex = 0;
            _common.ErrorLog("OnlAgentVerification", ex.Message.ToString());
        }
    }
    private void LoadCountAgents()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "agent.fun_agsmrycount");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {

                    lbtnpendverify.Text = dt.Rows[0]["pendforc_verification"].ToString();
                    lbtnpendcomplition.Text = dt.Rows[0]["pendfor_compilition"].ToString();
                    lbtnloginexpire.Text = dt.Rows[0]["login_expired"].ToString();
                    lbtnallagnt.Text = dt.Rows[0]["val_allag"].ToString();
                    lbtnPendingrefund.Text = dt.Rows[0]["Pnding_Refund"].ToString();

                }

                if (dt.Rows[0]["pendforc_verification"].ToString() == "0")
                {
                    lbtnpendverify.Enabled = false;
                }

                if (dt.Rows[0]["pendfor_compilition"].ToString() == "0")
                {
                    lbtnpendcomplition.Enabled = false;
                }

                if (dt.Rows[0]["login_expired"].ToString() == "0")
                {
                    lbtnloginexpire.Enabled = false;
                }

                if (dt.Rows[0]["val_allag"].ToString() == "0")
                {
                    lbtnallagnt.Enabled = false;
                }

                if (dt.Rows[0]["Pnding_Refund"].ToString() == "0")
                {
                    lbtnPendingrefund.Enabled = false;
                }
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("OnlagentVerification", ex.Message.ToString());
        }
    }
    public void messageBox(string header, string msg)
    {
        lblHeadermpError.Text = header;
        lblMessagempError.Text = msg;
        mpError.Show();
    }
    private void loadPendingAgents(string type, string status)//M3
    {
        try
        {
            pnlNoPendingAgent.Visible = false;
            grvAgentsPending.Visible = false;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "agent.fun_get_agpendingrequest");
            MyCommand.Parameters.AddWithValue("@p_status", status);
            MyCommand.Parameters.AddWithValue("@p_requesttype", ddlRequestType.SelectedValue.ToString());
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    grvAgentsPending.Visible = true;
                    grvAgentsPending.DataSource = dt;
                    grvAgentsPending.DataBind();
                }
                else
                {
                    lblNoPendingAgent.Text = "No Pending Agent";
                    pnlNoPendingAgent.Visible = true;
                }
            }
            else
            {
                lblNoPendingAgent.Text = "Error while loading pending agent list";
                pnlNoPendingAgent.Visible = true;
            }


        }
        catch (Exception ex)
        {
            _common.ErrorLog("OnlagentVerification", ex.Message.ToString());
        }
    }
    private void openpage(string PageName)
    {
        tkt.Src = PageName;
        mpdocment.Show();
    }
    private string LoadDocument(string Refno, string Doctype)
    {
        try
        {

            byte[] FileInvoice = new byte[0];
            string base64String = "";
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "agent.fun_allagentrequest");
            MyCommand.Parameters.AddWithValue("@p_searchtext", Refno);
            MyCommand.Parameters.AddWithValue("@p_status", Session["Status"].ToString());
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    if (Doctype == "B")
                    {
                        FileInvoice = (byte[])dt.Rows[0]["registration_file"];
                    }
                    if (Doctype == "I")
                    {
                        FileInvoice = (byte[])dt.Rows[0]["idproof_file"];
                    }
                    if (Doctype == "A")
                    {
                        FileInvoice = (byte[])dt.Rows[0]["address_proof_file"];
                    }
                    if (Doctype == "L")
                    {
                        FileInvoice = (byte[])dt.Rows[0]["legal_statusfile"];
                    }
                    base64String = Convert.ToBase64String(FileInvoice, 0, FileInvoice.Length);
                }
            }

            return base64String;


        }
        catch (Exception ex)
        {
            _common.ErrorLog("OnlagentVerification", ex.Message.ToString());
            return "";
        }
    }
    private void AgentVerification()
    {
        try
        {
            string IPAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(IPAddress))
            {
                IPAddress = Request.ServerVariables["REMOTE_ADDR"];
            }

            if (Session["Status"].ToString() == "R")
            {
                Session["StatusReason"] = ddlrejectreason.SelectedValue.ToString();
            }
            else if (Session["Status"].ToString() == "C")
            {
                Session["StatusReason"] = txtcancelremark.Text.ToString();
            }
            else
            {
                Session["StatusReason"] = "";
            }
            
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "agent.fun_agrequestchngestatus");
            MyCommand.Parameters.AddWithValue("@p_refno", Session["Agcode"].ToString());
            MyCommand.Parameters.AddWithValue("@p_verifyby", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("@p_ipaddress", IPAddress);
            MyCommand.Parameters.AddWithValue("@p_status", Session["Status"].ToString());
            MyCommand.Parameters.AddWithValue("@p_statusreason", Session["StatusReason"].ToString());
            dt = bll.SelectAll(MyCommand);
            pnlNoAgent.Visible = false;
            grvAgents.Visible = false;
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    string msg = " The Registration Request Of " + dt.Rows[0]["agent_name"].ToString();
                    msg += "(" + dt.Rows[0]["reference_no"].ToString() + ") has been";
                    if (dt.Rows[0]["current_status"].ToString() == "V")
                    {
                        msg += "Successfully Verified.";
                    }
                    else if (dt.Rows[0]["current_status"].ToString() == "R")
                    {
                        msg += " Successfully Rejected.";
                    }
                    else if (dt.Rows[0]["current_status"].ToString() == "A")
                    {
                        msg += "Successfully Approved.";
                    }
                    else if (dt.Rows[0]["current_status"].ToString() == "C")
                    {
                        msg += "Successfully Cancelled.";
                    }

                    messageBox("Information", msg);
                    loadPendingAgents("1", "E");
                    LoadCountAgents();
                    Allagents();
                    pnlview.Visible = false;
                }
                else
                {
                    messageBox("Error", "Sorry, Agent verification failed. Please try again.");
                }
            }
            else
            {
                messageBox("Error", "Sorry, Agent verification failed. Please try again.");
            }
        }
        catch (Exception ex)
        {
            messageBox("Exception", "Error, Something went wrong error code AdmAgntMIS19" + ex.Message);

        }
    }
    private void Allagents()
    {
        try
        {
            String searchVal = txtSearchAgentMIS.Text;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "agent.fun_allagentrequest");
            MyCommand.Parameters.AddWithValue("@p_searchtext", searchVal);
            MyCommand.Parameters.AddWithValue("@p_status", Session["Status"].ToString());
            dt = bll.SelectAll(MyCommand);
            pnlNoAgent.Visible = false;
            grvAgents.Visible = false;
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    grvAgents.Visible = true;
                    grvAgents.DataSource = dt;
                    grvAgents.DataBind();
                }
                else
                {
                    lblNoAgent.Text = "No Agent Available";
                    pnlNoAgent.Visible = true;
                }
            }
            else
            {
                lblNoAgent.Text = "Error while loading agent's list";
                pnlNoAgent.Visible = true;
            }


        }
        catch (Exception ex)
        {
            _common.ErrorLog("OnlagentVerification", ex.Message.ToString());
        }
    }
    private void UpdateLoginValidity(string p_Agcode, string p_validto)
    {
        try
        {
            string IPAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(IPAddress))
            {
                IPAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "agent.fun_agupdateLvalidity");
            MyCommand.Parameters.AddWithValue("@p_agcode", p_Agcode);
            MyCommand.Parameters.AddWithValue("@p_validto", p_validto);
            MyCommand.Parameters.AddWithValue("@p_ipaddress", IPAddress);
            MyCommand.Parameters.AddWithValue("@p_updatedby", Session["_UserCode"].ToString());
            dt = bll.SelectAll(MyCommand);
            pnlNoAgent.Visible = false;
            grvAgents.Visible = false;
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    LoadCountAgents();
                    Session["Allagtype"] = "A";
                    Allagents();
                    messageBox("Information", "Update Login Validity of Agent Successfully");
                }
                else
                {
                    messageBox("Information", "Something Went Wrong");
                }
            }
            else
            {
                messageBox("Information", "Something Went Wrong");
            }
        }
        catch (Exception ex)
        {

        }
    }
    private void AgentDeactivate(string p_Agcode)
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
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "agent.agdeactivate");
            MyCommand.Parameters.AddWithValue("@p_agcode", p_Agcode);
            MyCommand.Parameters.AddWithValue("@p_ipaddress", IPAddress);
            MyCommand.Parameters.AddWithValue("@p_updatedby", Session["_UserCode"].ToString());
            dt = bll.SelectAll(MyCommand);
            pnlNoAgent.Visible = false;
            grvAgents.Visible = false;
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    LoadCountAgents();
                    Allagents();
                    messageBox("Information", "Agent Deactivated Successfully");
                }
            }
        }
        catch (Exception ex)
        {

        }

    }
    private void AgentRefund(string p_Agcode, string p_Ponumber)
    {
        try
        {
            string IPAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(IPAddress))
            {
                IPAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "agent.agRefundAmt");
            MyCommand.Parameters.AddWithValue("@p_agcode", p_Agcode);
            MyCommand.Parameters.AddWithValue("@p_ponumber", p_Ponumber);
            MyCommand.Parameters.AddWithValue("@p_ipaddress", IPAddress);
            MyCommand.Parameters.AddWithValue("@p_updatedby", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("@p_bankid", Convert.ToInt32(ddlbank.SelectedValue));
            MyCommand.Parameters.AddWithValue("@p_bankrefno", txtbankrefno.Text);
            MyCommand.Parameters.AddWithValue("@p_refunddate", txtrefunddate.Text);
            dt = bll.SelectAll(MyCommand);
            pnlNoAgent.Visible = false;
            grvAgents.Visible = false;
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    Session["Allagtype"] = "A";
                    Session["Status"] = "A";
                    LoadCountAgents();
                    Allagents();
                    messageBox("Information", "Refund of Agent Amount (Security amount + wallet balance) Refund Successfully");
                }
            }
        }
        catch (Exception ex)
        {
       
        }
    }


    #endregion

    #region Events
    protected void grvAgentsPending_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "ViewRegistrationProof")
        {
            Session["Agcode"] = grvAgentsPending.DataKeys[index].Values["reference_no"];
            Session["Base64String"] = LoadDocument(Session["Agcode"].ToString(), "B");
            openpage("../Pass/ViewDocument.aspx");


        }
        if (e.CommandName == "ViewIDProof")
        {
            Session["Agcode"] = grvAgentsPending.DataKeys[index].Values["reference_no"];
            Session["Base64String"] = LoadDocument(Session["Agcode"].ToString(), "I");
            openpage("../Pass/ViewDocument.aspx");

        }
        if (e.CommandName == "ViewAddressProof")
        {
            Session["Agcode"] = grvAgentsPending.DataKeys[index].Values["reference_no"];
            Session["Base64String"] = LoadDocument(Session["Agcode"].ToString(), "A");
            openpage("../Pass/ViewDocument.aspx");
        }
        if (e.CommandName == "ViewCertifiedCopy")
        {
            Session["Agcode"] = grvAgentsPending.DataKeys[index].Values["reference_no"];
            Session["Base64String"] = LoadDocument(Session["Agcode"].ToString(), "L");
            openpage("../Pass/ViewDocument.aspx");
        }


        if (e.CommandName == "ViewRequest")
        {
            //int index = e.Item.ItemIndex; // Assuming this is within an event handler for a data-bound control like DataGrid or GridView

            lblname.Text = grvAgentsPending.DataKeys[index].Values["agent_name"] + "(" + grvAgentsPending.DataKeys[index].Values["reference_no"] + ")";
            lblpanno.Text = grvAgentsPending.DataKeys[index].Values["pan_no"].ToString();
            lblbookingexp.Text = grvAgentsPending.DataKeys[index].Values["val_experience"].ToString();
            lbllegalstatus.Text = grvAgentsPending.DataKeys[index].Values["l_status"].ToString();
            lblpersonname.Text = grvAgentsPending.DataKeys[index].Values["contact_person"].ToString();
            lblmobileno.Text = grvAgentsPending.DataKeys[index].Values["mobile_no"].ToString();
            lblemail.Text = grvAgentsPending.DataKeys[index].Values["val_email"].ToString();
            lbladdress.Text = grvAgentsPending.DataKeys[index].Values["val_address"].ToString();
            Session["Status"] = "E";
            Session["Base64String"] = "";
            Session["MobileNo"] = grvAgentsPending.DataKeys[index].Values["mobile_no"];
            Session["EMAIL"] = grvAgentsPending.DataKeys[index].Values["val_email"];

            Session["Agcode"] = grvAgentsPending.DataKeys[index].Values["reference_no"];
            Session["Base64String"] = LoadDocument(Session["Agcode"].ToString(), "I");
            ltControl_idproof.Text = "<embed src = \"../Pass/ViewDocument.aspx\" style=\"height: 50vh; width: 100%\" />";


            Session["Base64String1"] = "";

            Session["Base64String1"] = LoadDocument(Session["Agcode"].ToString(), "A");
            ltControl_addressproof.Text = "<embed src = \"ViewDocument.aspx\" style=\"height: 50vh; width: 100%\" />";

            pnlview.Visible = true;
            grvAgentsPending.Visible = false;
        }

        //if (e.CommandName == "VerifyAgent")
        //{
        //    Session["Agcode"] = grvAgentsPending.DataKeys[index].Values["reference_no"];
        //    Session["Status"] = "V";
        //    string msg = "Do you want to verify the details of agent " + grvAgentsPending.DataKeys[index].Values["agent_name"] + "(" + grvAgentsPending.DataKeys[index].Values["reference_no"] + ")";
        //    Label1.Text = msg;
        //    dvreject.Visible = false;
        //    mpAgentVerification.Show();
        //}
        //if (e.CommandName == "RejectAgent")
        //{
        //    Session["Agcode"] = grvAgentsPending.DataKeys[index].Values["reference_no"];
        //    Session["Status"] = "R";
        //    string msg = "Do you want to reject the details of agent " + grvAgentsPending.DataKeys[index].Values["agent_name"] + "(" + grvAgentsPending.DataKeys[index].Values["reference_no"] + ")";
        //    Label1.Text = msg;
        //    loadrejectReason(ddlrejectreason);
        //    dvreject.Visible = true;
        //    mpAgentVerification.Show();
        //}
        if (e.CommandName == "CancelRequest")
        {
            Session["Agcode"] = grvAgentsPending.DataKeys[index].Values["reference_no"];
            Session["Status"] = "C";
            string msg = "Do you want to cancel the agent request for " + grvAgentsPending.DataKeys[index].Values["agent_name"] + "(" + grvAgentsPending.DataKeys[index].Values["reference_no"] + ")";
            Label1.Text = msg;
            //loadrejectReason(ddlrejectreason);
            ddlrejectreason.Visible = false;
            txtcancelremark.Visible = true;
            dvreject.Visible = true;
            mpAgentVerification.Show();
        }
        if (e.CommandName == "SMS")
        {
            messageBox("Information","Coming Soon");
        }
        if (e.CommandName == "Mail")
        {
            messageBox("Information", "Coming Soon");
        }
    }
    protected void grvAgentsPending_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbtnViewRegistrationProof = (LinkButton)e.Row.FindControl("lbtnViewRegistrationProof");
            LinkButton lbtnViewCertifiedCopy = (LinkButton)e.Row.FindControl("lbtnViewCertifiedCopy");
            lbtnViewCertifiedCopy.Visible = false;
            lbtnViewRegistrationProof.Visible = false;

            DataRowView rowView = (DataRowView)e.Row.DataItem;

            if (rowView["legal_status"].ToString() == "P")
            {
                lbtnViewCertifiedCopy.Visible = true;
            }

            if (rowView["experience_yn"].ToString() == "Y")
            {
                lbtnViewRegistrationProof.Visible = true;
            }
            LinkButton lbtnview = e.Row.FindControl("lbtnview") as LinkButton;
            //LinkButton lbtnVerify = e.Row.FindControl("lbtnVerify") as LinkButton;
            //LinkButton lbtnReject = e.Row.FindControl("lbtnReject") as LinkButton;
            LinkButton lbtnsms = e.Row.FindControl("lbtnsms") as LinkButton;
            LinkButton lbtnemail = e.Row.FindControl("lbtnemail") as LinkButton;
            LinkButton lbtncancel = e.Row.FindControl("lbtncancel") as LinkButton;
            lbtnview.Visible = false;
            //lbtnVerify.Visible = false;
            //lbtnReject.Visible = false;
            lbtnsms.Visible = false;
            lbtnemail.Visible = false;
            lbtncancel.Visible = false;

            if (rowView["current_status"].ToString() == "E")
            {
                lbtnview.Visible = true;
                //lbtnVerify.Visible = true;
                //lbtnReject.Visible = true;
            }

            if (rowView["current_status"].ToString() == "V")
            {
                lbtnsms.Visible = true;
                lbtnemail.Visible = true;
                lbtncancel.Visible = true;
            }
        }

    }
    protected void grvAgents_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "ViewRegistrationProof")
        {
            Session["Agcode"] = grvAgents.DataKeys[index].Values["reference_no"];
            Session["Base64String"] = LoadDocument(Session["Agcode"].ToString(), "B");
            openpage("../Pass/ViewDocument.aspx");
        }
        if (e.CommandName == "ViewIDProof")
        {
            Session["Agcode"] = grvAgents.DataKeys[index].Values["reference_no"];
            Session["Base64String"] = LoadDocument(Session["Agcode"].ToString(), "I");
            openpage("../Pass/ViewDocument.aspx");
        }
        if (e.CommandName == "ViewAddressProof")
        {
            Session["Agcode"] = grvAgents.DataKeys[index].Values["reference_no"];
            Session["Base64String"] = LoadDocument(Session["Agcode"].ToString(), "A");
            openpage("../Pass/ViewDocument.aspx");
        }
        if (e.CommandName == "ViewCertifiedCopy")
        {
            Session["Agcode"] = grvAgents.DataKeys[index].Values["reference_no"];
            Session["Base64String"] = LoadDocument(Session["Agcode"].ToString(), "L");
            openpage("../Pass/ViewDocument.aspx");
        }
        if (e.CommandName == "Validity")
        {
            Session["Agcode"] = grvAgents.DataKeys[index].Values["agent_code"];
            lblagcode.Text = grvAgents.DataKeys[index].Values["agent_code"].ToString();
            lblagname.Text = grvAgents.DataKeys[index].Values["agent_name"].ToString();
            //lblagmobile.Text = grvAgents.DataKeys[index].Values["MOBILENO"];
            //lblagemail.Text = grvAgents.DataKeys[index].Values["EMAIL"];
            lblvalidto.Text = grvAgents.DataKeys[index].Values["login_validity"].ToString();
            mpUpdateValidity.Show();
        }
        if (e.CommandName == "ChndPwd")
        {
            Session["Agcode"] = grvAgents.DataKeys[index].Values["agent_code"];
            lblagcode.Text = grvAgents.DataKeys[index].Values["agent_code"].ToString();
            lblagname.Text = grvAgents.DataKeys[index].Values["agent_name"].ToString();
            //lblagmobile.Text = grvAgents.DataKeys[index].Values["MOBILENO"];
            //lblagemail.Text = grvAgents.DataKeys[index].Values["EMAIL"];
            lblvalidto.Text = grvAgents.DataKeys[index].Values["login_validity"].ToString();
            mpchangepassowrd.Show();
        }
        
        if (e.CommandName == "Deactivate")
        {
            Session["Agcode"] = grvAgents.DataKeys[index].Values["agent_code"];
            mpDeactivation.Show();
        }
        if (e.CommandName == "Refund")
        {
            Session["Agcode"] = grvAgents.DataKeys[index].Values["agent_code"];
            Session["CancelRefno"] = grvAgents.DataKeys[index].Values["cancel_refno"];
            lblponumber.Text = grvAgents.DataKeys[index].Values["payment_orderno"].ToString();
            lbldactivatedt.Text = grvAgents.DataKeys[index].Values["statuson_datetime"].ToString();
            lblrefundamt.Text = grvAgents.DataKeys[index].Values["val_amount"].ToString();
            mpRefund.Show();
        }

    }
    protected void grvAgents_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbtnViewRegistrationProof = (LinkButton)e.Row.FindControl("lbtnViewRegistrationProof");
            LinkButton lbtnViewCertifiedCopy = (LinkButton)e.Row.FindControl("lbtnViewCertifiedCopy");
            lbtnViewCertifiedCopy.Visible = false;
            lbtnViewRegistrationProof.Visible = false;

            DataRowView rowView = (DataRowView)e.Row.DataItem;
            if (rowView["legal_status"].ToString() == "P")
            {
                lbtnViewCertifiedCopy.Visible = true;
            }
            if (rowView["legal_status"].ToString() == "Y")
            {
                lbtnViewRegistrationProof.Visible = true;
            }

            LinkButton lbtnDeactivate = (LinkButton)e.Row.FindControl("lbtnDeactivate");
            LinkButton lbtnValidity = (LinkButton)e.Row.FindControl("lbtnValidity");
            LinkButton lbtnRefund = (LinkButton)e.Row.FindControl("lbtnRefund");
            LinkButton lbtnpwd = (LinkButton)e.Row.FindControl("lbtnpwd");

            lbtnDeactivate.Visible = false;
            lbtnValidity.Visible = false;
            lbtnRefund.Visible = false;
            lbtnpwd.Visible = false;

            if (Session["Allagtype"] == "L")
            {
                if (rowView["expired_yn"].ToString() == "Y")
                {
                    e.Row.Visible = true;
                }
                else
                {
                    e.Row.Visible = false;
                }
            }
            if (Session["Allagtype"] == "A")
            {
                if (rowView["expired_yn"].ToString() == "Y")
                {
                    e.Row.Visible = false;
                }
                else
                {
                    if (rowView["vaal_status"].ToString() == "A")
                    {
                        e.Row.Visible = true;
                    }
                    else
                    {
                        e.Row.Visible = false;
                    }
                }
            }
            if (rowView["expired_yn"].ToString() == "Y")
            {
                lbtnValidity.Visible = true;
                lbtnDeactivate.Visible = true;
            }
            else
            {
                lbtnDeactivate.Visible = true;
                lbtnpwd.Visible = true;
            }
            if (Session["Allagtype"] == "D")
            {
                if (rowView["vaal_status"].ToString() == "A")
                {
                    e.Row.Visible = false;
                }
                else if (rowView["vaal_status"].ToString() == "D" && !Convert.IsDBNull(rowView["refund_refno"]))
                {
                    e.Row.Visible = false;
                }
                else
                {
                    lbtnDeactivate.Visible = false;
                    lbtnValidity.Visible = false;
                    lbtnRefund.Visible = true;
                    lbtnpwd.Visible = false;
                    e.Row.Visible = true;
                }
            }
        }

    }
    protected void ddlRequestType_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadPendingAgents("1", "E");
    }

    protected void lbtnVerificationYes_Click(object sender, EventArgs e)
    {
        if (Session["Status"].ToString() == "R")
        {
            //if (ddlrejectreason.SelectedValue == "0")
            //{
            //    messageBox("Please Check", "Select Valid Reject Reason");
            //    return;
            //}

            if (string.IsNullOrEmpty(txtrejectreason.Text) == true)
            {
                messageBox("Please Check", "Enter Valid Reject Reason");
                return;
            }

        }
        if (Session["Status"].ToString() == "C")
        {
            if (string.IsNullOrEmpty(txtcancelremark.Text))
            {
                messageBox("Please Check", "Enter Valid Cancel Reason");
                return;
            }
        }
        AgentVerification();
    }
    protected void lbtnpendverify_Click(object sender, EventArgs e)
    {
        pnlview.Visible = false;
        pnlallag.Visible = false;
        pnlpendingag.Visible = true;
        ddlRequestType.Visible = true;
        Session["Status"] = "E";
        loadPendingAgents("1", "E");
        lblpendingheading.Text = "Pending For Verification <br /><span style='font-size:10pt;'>(Online Request Submitted ) </span>";
    }
    protected void lbtnpendcomplition_Click(object sender, EventArgs e)
    {
        ;
        pnlallag.Visible = false;
        pnlpendingag.Visible = true;
        ddlRequestType.SelectedValue = "0";
        ddlRequestType.Visible = false;
        loadPendingAgents("1", "V");
        Session["Status"] = "V";
        lblpendingheading.Text = "Pending For Compilition <br /><span style='font-size:10pt;'>(Online Request Verified ) </span>";
    }
    protected void lbtnloginexpire_Click(object sender, EventArgs e)
    {
        pnlallag.Visible = true;
        pnlpendingag.Visible = false;
        Session["Allagtype"] = "L";
        Session["Status"] = "A";
        Allagents();
        lblalagheading.Text = "Login Expired <br /><span style='font-size:10pt;'>(Login and account validity both are expired ) </span>";
        if (grvAgents.Rows.Count == 0)
        {
            pnlNoAgent.Visible = true;
            grvAgents.Visible = false;
        }


    }
    protected void lbtnVerify_Click(object sender, EventArgs e)
    {
        Session["Status"] = "V";
        String msg = " Do you want To verify the details Of agent " + lblname.Text;
        Label1.Text = msg;
        dvreject.Visible = false;
        mpAgentVerification.Show();
    }
    protected void lbtnallagnt_Click(object sender, EventArgs e)
    {
        pnlallag.Visible = true;
        pnlpendingag.Visible = false;
        Session["Allagtype"] = "A";
        Session["Status"] = "A";
        Allagents();
        lblalagheading.Text = "All Agent List <br /><span style='font-size:10pt;'>(Approved agent and validity is valid) </span>";
        if (grvAgents.Rows.Count == 0)
        {
            pnlNoAgent.Visible = true;
            grvAgents.Visible = false;
        }
    }
    protected void lbtnPendingrefund_Click(object sender, EventArgs e)
    {
        pnlallag.Visible = true;
        pnlpendingag.Visible = false;
        Session["Allagtype"] = "D";
        Session["Status"] = "D";
        Allagents();
        lblalagheading.Text = "Deactivated Agent List <br /><span style='font-size:10pt;'>(Pending for refund (security+wallet)) </span>";
        if (grvAgents.Rows.Count == 0)
        {

            pnlNoAgent.Visible = true;
            grvAgents.Visible = false;

        }
    }
    protected void lbtnvalidityyes_Click(object sender, EventArgs e)
    {
        if (txtvalidto.Text == "")
        {
           
            messageBox("Please Check","Please Enter Date");
            
            return;
        }
        UpdateLoginValidity(Session["Agcode"].ToString(), txtvalidto.Text);

    }
    protected void lbtnyes_Click(object sender, EventArgs e)
    {
        AgentDeactivate(Session["Agcode"].ToString());
    }
    protected void linkbtnSearchAgentMIS_Click(object sender, EventArgs e)
    {
        string val = txtSearchAgentMIS.Text;
        if (val.Trim().Length < 3)
        {
            messageBox("Please Check", "Enter Minimum 3 characters for search");
            return;
        }

        Allagents();

    }
    protected void linkbtnAllAgentMIS_Click(object sender, EventArgs e)
    {
        txtSearchAgentMIS.Text = "";
        Allagents();
    }
    protected void lbtnrefundyes_Click(object sender, EventArgs e)
    {
        AgentRefund(Session["Agcode"].ToString(), Session["CancelRefno"].ToString());

    }
    
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        messageBox("Information","Coming Soon");
    }

    protected void lbtnReject_Click(object sender, EventArgs e)
    {
        Session["Status"] = "R";
        String msg = " Do you want To reject the details Of agent " + lblname.Text;
        Label1.Text = msg;
        txtrejectreason.Visible = true;
        dvreject.Visible = true;
        mpAgentVerification.Show();
    }

    protected void lbtncancel_Click(object sender, EventArgs e)
    {
        pnlview.Visible = false;
        grvAgentsPending.Visible = true;
        LoadCountAgents();
    }

    #endregion
}