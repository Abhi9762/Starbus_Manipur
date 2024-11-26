using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_PAdminSMSConfig : BasePage
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
        foreach (GridViewRow row in gvSMS.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                ((CheckBox)row.FindControl("cbSMS")).Attributes.Add("onchange", "javascript:TextboxAutoEnableAndDisable(" + (row.RowIndex) + ");");
            }
        }
        if (!IsPostBack)
        {
            Random randomclass = new Random();
            Session["rndNoCheck"] = randomclass.Next().ToString();
            hidtoken.Value = Session["rndNoCheck"].ToString();

            loadSMS();
        }
        Session["_moduleName"] = "SMS Configuration";
    }

    #region "Method"
    private void CsrfTokenValidate()
    {
        if (hidtoken.Value != Session["rndNoCheck"].ToString())
        {
            Response.Redirect("errorpage.aspx");
            return;
        }
    }
    protected void loadSMS()//M1
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_sms_template");
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                gvSMS.DataSource = dt;
                gvSMS.DataBind();
                gvSMS.Visible = true;
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminSMSConfig.aspx-0001", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
        }

    }
    private bool validValueSMSconfig()//M2
    {
        try
        {
            int msgcount = 0;
            string msg = "";
            if (_validation.IsValidString(tbSenderID.Text, 1, tbSenderID.MaxLength) == false)
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Enter Valid Sender ID <br/>";
            }
            if (_validation.IsValidString(tbUserName.Text, 4, tbUserName.MaxLength) == false)
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Enter valid User Name <br/>";
            }
            if (tbPassword.Text == "")
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Enter valid password <br/>";
            }
            if (_validation.IsValidString(tbDLTid.Text, 4, 20) == false)
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Enter Valid DLT ID <br/>";
            }
            if (msgcount > 0)
            {
                Errormsg(msg);
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminSMSConfig.aspx-0002", ex.Message.ToString());
            return false;
            
        }
    }
    private void Errormsg(string msg)
    {
        string popup = _popup.modalPopupSmall("W", "Please Note", msg, "Close");
        Response.Write(popup);
    }
    private void Successmsg(string msg)
    {
        string popup = _popup.modalPopupSmall("S", "Success", msg, "Close");
        Response.Write(popup);

    }
    protected void insertSMS()//M3
    {
        try
        {
            string Mresult = "";
            string sender = tbSenderID.Text.Trim();
            string DLT = tbDLTid.Text.Trim();
            string Username = tbUserName.Text.Trim();
            string password = tbPassword.Text;
            string IPAddress = HttpContext.Current.Request.UserHostAddress;
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.portal_smsdatas");
            MyCommand.Parameters.AddWithValue("p_senderid", sender);
            MyCommand.Parameters.AddWithValue("p_dltid", DLT);
            MyCommand.Parameters.AddWithValue("p_username", Username);
            MyCommand.Parameters.AddWithValue("p_password", password);
            MyCommand.Parameters.AddWithValue("p_updatedby", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("p_ipaddress", IPAddress);

            Mresult = bll.UpdateAll(MyCommand);
            if (Mresult == "Success")
            {
                resetctrl();
                Successmsg("SMS Configuration inserted Successfully");
            }
            else
            {
                Errormsg("Error Occurred");
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminSMSConfig.aspx-0003", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
        }

    }
    private void resetctrl()
    {
        tbSenderID.Text = "";
        tbUserName.Text = "";
        tbPassword.Text = "";
        tbDLTid.Text = "";
    }
    private void insertEntityid()//M4
    {
        try
        {
            for (int i = 0; i < gvSMS.Rows.Count; i++)
            {
                string IPAddress = HttpContext.Current.Request.UserHostAddress;
                CheckBox chk = (CheckBox)gvSMS.Rows[i].FindControl("cbSMS");
                HiddenField moduleid = (HiddenField)gvSMS.Rows[i].FindControl("hdmoduleid");
                TextBox Entityid = (TextBox)gvSMS.Rows[i].FindControl("tbEntityid");
                if (chk.Checked == true)
                {
                    string Entity;
                    Entity = Entityid.Text.Trim().ToString();
                    NpgsqlCommand MyCommand = new NpgsqlCommand();
                    MyCommand.Parameters.Clear();
                    string Mresult = "";
                    MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.portal_sms_entityupdate_data");
                    MyCommand.Parameters.AddWithValue("p_moduleid", Convert.ToInt32(moduleid.Value.ToString()));
                    MyCommand.Parameters.AddWithValue("p_entity", Entity);
                    Mresult = bll.UpdateAll(MyCommand);
                    if (Mresult == "Success")
                    {
                        loadSMS();
                        Successmsg("Entity ID Successfully Saved");
                        return;
                    }
                    else
                    {
                        Errormsg("Error Ocurred !Please Check");
                        return;
                    }
                }


            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminSMSConfig.aspx-0004", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
        }
    }
    #endregion

    #region "Event"
    protected void lbtnSave_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (validValueSMSconfig() == false)
        {
            return;
        }
        Session["action"] = 'S';
        lblConfirmation.Text = "Do you want Update SMS config ?";
        mpConfirmation.Show();
    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (Session["action"].ToString() == "S")
        {
            insertSMS();
        }
        if (Session["action"].ToString() == "U")
        {
            insertEntityid();
        }
    }
    protected void lbtnAddNewTemplete_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        pnlSMS.Visible = false;
    }
    protected void lbtnSaveSMS_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        try
        {
            int count = 0;
            int b = 0;
            for (int i = 0; i < gvSMS.Rows.Count; i++)
            {
                TextBox Entityid = (TextBox)gvSMS.Rows[i].FindControl("tbEntityid");
                CheckBox cbSMS = (CheckBox)gvSMS.Rows[i].FindControl("cbSMS");
                string Entity;
                Entity = Entityid.Text.Trim().ToString();
                if (string.IsNullOrEmpty(Entityid.Text.Trim().ToString()) == true || Entityid.Text.Trim().ToString() == "")
                {
                    count = count + 1;
                }
                if (cbSMS.Checked == false)
                {
                    b = b + 1;
                }
            }
            if (b > 0)
            {
                Errormsg("At least select one checkbox to update entity ID");
            }
            else if (count > 0)
            {
                Errormsg("enter entity id cannot be blank");
            }
            else
            {
                Session["action"] = 'U';
                lblConfirmation.Text = "Are you sure you want to update Entity ID?";
                mpConfirmation.Show();
            }
        }
        catch (Exception ex)
        {

            _common.ErrorLog("PAdminSMSconfig-E1", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
        }
    }
    protected void lbtnViewConfig_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Errormsg("Coming Soon");
    }
    protected void lbtnHelpTemplate_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Errormsg("Coming Soon");
    }
    protected void lbtnReset_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        tbSenderID.Text = "";
        tbUserName.Text = "";
        tbPassword.Text = "";
        tbDLTid.Text = "";
    }
    protected void cbAll_CheckedChanged(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        CheckBox cbAll = (CheckBox)gvSMS.HeaderRow.FindControl("cbAll");
        if (cbAll.Checked == true)
        {
            foreach (GridViewRow row in gvSMS.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("cbSMS") as CheckBox);
                    TextBox tbEntityid = (row.Cells[0].FindControl("tbEntityid") as TextBox);
                    chkRow.Checked = true;
                    tbEntityid.Enabled = true;
                }
            }

        }
        else
        {
            foreach (GridViewRow row in gvSMS.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("cbSMS") as CheckBox);
                    TextBox tbEntityid = (row.Cells[0].FindControl("tbEntityid") as TextBox);
                    chkRow.Checked = false;
                    tbEntityid.Enabled = false;
                }
            }
        }
    }
    #endregion
}