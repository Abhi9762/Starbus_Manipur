using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CaptchaDLL;
using Npgsql;
using System.Xml;

public partial class Auth_PAdminCatalogue : BasePage
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    string todayDate = DateTime.Now.Date.ToString("dd") + "/" + DateTime.Now.Date.ToString("MM") + "/" + DateTime.Now.Date.ToString("yyyy");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Random randomclass = new Random();
            Session["rndNoCheck"] = randomclass.Next().ToString();
            hidtoken.Value = Session["rndNoCheck"].ToString();
            Session["_moduleName"] = "Catalogue";
            checkAdditionalModules();
            //rbAttemptType.SelectedValue = "S";
            tbDateAuditLog.Text = todayDate;
            loadAuditTrail();
        }
    }
    private void checkAdditionalModules()
    {
        divconcession.Visible = false;
        divbuspass.Visible = false;
        divagentconf.Visible = false;

        if (sbXMLdata.checkModuleCategory("70") == true)
        {
            divagentconf.Visible = true;
        }
        if (sbXMLdata.checkModuleCategory("71") == true)
        {
            divbuspass.Visible = true;
            divconcession.Visible = true;
        }
    }
    private void loadAuditTrailAuditlog()
    {
        pnlNoRecord.Visible = true;
        gv1_6_Web.Visible = false;
        GVAuditLog.Visible = false;
        MyCommand = new NpgsqlCommand();
        DataTable dt;
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.fn_get_actionlog");
        MyCommand.Parameters.AddWithValue("spreport_date", tbDateAuditLog.Text.ToString());
        // MyCommand.Parameters.AddWithValue("spauth_yn", rbAttemptType.SelectedValue.ToString());
        dt = bll.SelectAll(MyCommand);
        if (dt.TableName == "Success")
        {
            if (dt.Rows.Count > 0)
            {
                GVAuditLog.DataSource = dt;
                GVAuditLog.DataBind();
                pnlNoRecord.Visible = false;
                GVAuditLog.Visible = true;
                gv1_6_Web.Visible = false;
            }
        }
    }
    private void loadAuditTrail()
    {
        try
        {
            if (isValidAuditValues() == false)
            {

                return;
            }
            pnlNoRecord.Visible = true;
            gv1_6_Web.Visible = false;
            MyCommand = new NpgsqlCommand();
            DataTable dt;
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.fn_get_loginlog");
            MyCommand.Parameters.AddWithValue("spreport_date", tbDateAuditLog.Text.ToString());
           // MyCommand.Parameters.AddWithValue("spauth_yn", rbAttemptType.SelectedValue.ToString());
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    gv1_6_Web.DataSource = dt;
                    gv1_6_Web.DataBind();
                    GVAuditLog.Visible = false;
                    pnlNoRecord.Visible = false;
                    gv1_6_Web.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
    private bool isValidAuditValues()
    {
        try
        {
            int msgcnt = 0;
            string msg = "";
            //if (!(rbAttemptType.SelectedValue == "S" | rbAttemptType.SelectedValue == "U"))
            //{
            //    msgcnt = msgcnt + 1;
            //    msg = msg + msgcnt.ToString() + ". Audit Trail Report To Be Generated for.<br>";
            //}
            if (_validation.IsValidString(tbDateAuditLog.Text.ToString(), 10, 10) == false)
            {
                msgcnt = msgcnt + 1;
                msg = msg + msgcnt.ToString() + ". Date.<br>";
            }
            //else if (Convert.ToDateTime(tbDateAuditLog.Text) > DateTime.Now.Date)
           // {
             //   msgcnt = msgcnt + 1;
             //   msg = msg + msgcnt.ToString() + ". Date.<br>";
           // }

            if (msgcnt > 0)
            {

                errorMassage(msg);
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
//Response.Write(ex.Message);
            errorMassage("Something went wrong. Please contact to tech team." + ex.Message);
            return false;
        }
    }
    public void errorMassage(string msg)
    {
        //    lblerrormsg.Text = msg;
        //    mperror.Show();
    }


    protected void lbtnSearchAuditTrail_Click(object sender, EventArgs e)
    {
        if (ddlaudittrailtype.SelectedValue == "1")
        {
            loadAuditTrail();
        }
        if (ddlaudittrailtype.SelectedValue == "2")
        {
            loadAuditTrailAuditlog();
        }
    }

    protected void lbtnDownloadAuditTrail_Click(object sender, EventArgs e)
    {
       
    }

    protected void gv1_6_Web_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv1_6_Web.PageIndex  = e.NewPageIndex;
        loadAuditTrail();
    }

    protected void GVAuditLog_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVAuditLog.PageIndex = e.NewPageIndex;
        loadAuditTrailAuditlog();
    }

   
}