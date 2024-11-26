using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_PAdminFAQ : BasePage
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
            Random randomclass = new Random();
            Session["rndNoCheck"] = randomclass.Next().ToString();
            hidtoken.Value = Session["rndNoCheck"].ToString();
            Session["_moduleName"] = "Frequently Asked Questions";
            loadFAQcategory();
            FAQList();
        }
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
    private void Errormsg(string msg)
    {

        string popup = _popup.modalPopupSmall("D", "Please Check", msg, "Close");
        Response.Write(popup);
    }
    private void Successmsg(string msg)
    {

        string popup = _popup.modalPopupSmall("S", "Congratulations", msg, "Close");
        Response.Write(popup);
    }
    private void ConfirmMsg(string msg)
    {
        lblConfirmation.Text = msg;
        mpConfirmation.Show();
    }
    private void resetcontrol()
    {
        tbFAQAns.Text = "";
        tbFAQAnsLocal.Text = "";
        tbFAQQues.Text = "";
        tbFAQQuesLocal.Text = "";
        ddlFAQcategory.SelectedValue = "0";
    }
    private bool validValue()//M1
    {
        try
        {
            int msgcount = 0;
            string msg = "";
            if (tbFAQQues.Text == "")
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Enter Valid FAQ question English.<br/>";
            }

            //if (tbFAQQuesLocal.Text == "")
            //{
            //    msgcount = msgcount + 1;
            //    msg = msg + msgcount.ToString() + ". Enter Valid FAQ question in Local language.<br/>";

            //}
            if (_validation.IsValidString(tbFAQAns.Text, 10, tbFAQAns.MaxLength) == false)
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Enter Valid FAQ answer in English<br/>";
            }
            //if (_validation.IsValidString(tbFAQAnsLocal.Text, 10, tbFAQAnsLocal.MaxLength) == false)
            //{
            //    msgcount = msgcount + 1;
            //    msg = msg + msgcount.ToString() + ". Enter Valid FAQ answer in Local<br/>";
            //}
            if (ddlFAQcategory.SelectedValue == "0")
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Select FAQ category<br/>";
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
            _common.ErrorLog("PAdminFAQ.aspx-0001", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
            return false;
        }
    }
    private void loadFAQcategory()//M2
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            DataTable dt = new DataTable();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_faqcategory");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {

                    ddlFAQcategory.DataSource = dt;
                    ddlFAQcategory.DataTextField = "faq_category_en";
                    ddlFAQcategory.DataValueField = "faq_category";
                    ddlFAQcategory.DataBind();
                    ddlFAQCategory1.DataSource = dt;
                    ddlFAQCategory1.DataTextField = "faq_category_en";
                    ddlFAQCategory1.DataValueField = "faq_category";
                    ddlFAQCategory1.DataBind();
                }
            }
            else
            {
                _common.ErrorLog("PAdminFAQ.aspx-0002", dt.TableName);
            }
            ddlFAQcategory.Items.Insert(0, "Select");
            ddlFAQcategory.Items[0].Value = "0";
            ddlFAQcategory.SelectedIndex = 0;
            ddlFAQCategory1.Items.Insert(0, "Select");
            ddlFAQCategory1.Items[0].Value = "0";
            ddlFAQCategory1.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminFAQ.aspx-0003", ex.Message.ToString());
            ddlFAQcategory.Items.Insert(0, "Select");
            ddlFAQcategory.Items[0].Value = "0";
            ddlFAQcategory.SelectedIndex = 0;
            ddlFAQCategory1.Items.Insert(0, "Select");
            ddlFAQCategory1.Items[0].Value = "0";
            ddlFAQCategory1.SelectedIndex = 0;
        }

    }
    private void saveFAQ()//M3
    {
        try
        {
            string FAQQuestionEn = "", FAQQuestionLocal = "", FAQAnsEn = "", FAQAnsL = "", FAQid = "0";
            int FAQcategory = 0;
            FAQQuestionEn = tbFAQQues.Text;
            FAQQuestionLocal = tbFAQQuesLocal.Text;
            FAQAnsEn = tbFAQAns.Text;
            FAQAnsL = tbFAQAnsLocal.Text;
            if (ddlFAQcategory.SelectedValue != "0")
            {
                FAQcategory = Convert.ToInt32(ddlFAQcategory.SelectedValue);
            }

            if (Session["_action"].ToString() == "U" || Session["_action"].ToString() == "D")
            {
                FAQid = Session["faqid"].ToString();
            }
            string IPAddress = HttpContext.Current.Request.UserHostAddress;
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_faq_insertupdate");
            MyCommand.Parameters.AddWithValue("p_action", Session["_action"].ToString());
            MyCommand.Parameters.AddWithValue("p_updatedby", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("p_ipaddress", IPAddress);
            MyCommand.Parameters.AddWithValue("p_id", Convert.ToInt64(FAQid));
            MyCommand.Parameters.AddWithValue("p_category", Convert.ToInt32(FAQcategory));
            MyCommand.Parameters.AddWithValue("p_question_en", FAQQuestionEn);
            MyCommand.Parameters.AddWithValue("p_question_local", FAQQuestionLocal);
            MyCommand.Parameters.AddWithValue("p_answer_en", FAQAnsEn);
            MyCommand.Parameters.AddWithValue("p_answer_local", FAQAnsL);

            string Mresult = bll.UpdateAll(MyCommand);
            if (Mresult == "Success")
            {

                if (Session["_action"].ToString() == "S")
                {
                    Successmsg("FAQ Added Successfully.");
                }
                if (Session["_action"].ToString() == "U")
                {
                    Successmsg("FAQ Updated Successfully.");
                }
                if (Session["_action"].ToString() == "D")
                {
                    Successmsg("FAQ Deleted.");
                }
                FAQList();
                resetcontrol();

                lblFAQUpdate.Visible = false;
                lblFAQHead.Visible = true;
                lbtnSave.Visible = true;
                lbtnUpdate.Visible = false;
                lbtnReset.Visible = true;
                lbtnCancel.Visible = false;
            }
            else
            {
                Errormsg(Mresult);
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminFAQ.aspx-0004", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
        }
    }
    protected void FAQList()//M4
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_faq");
            MyCommand.Parameters.AddWithValue("p_categorycode", Convert.ToInt32(ddlFAQCategory1.SelectedValue));
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                gvFAQ.DataSource = dt;
                gvFAQ.DataBind();
                gvFAQ.Visible = true;
                pnlnoRecordfound.Visible = false;
            }
            else
            {
                gvFAQ.Visible = false;
                pnlnoRecordfound.Visible = true;
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminFAQ.aspx-0005", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
        }
    }

    #endregion

    #region "Event"
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        saveFAQ();
    }
    protected void lbtnUpdate_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (validValue() == false)
        {
            return;
        }
        ConfirmMsg("Do you want to update FAQ Details");
        Session["_action"] = "U";

    }
    protected void lbtnSave_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (validValue() == false)
        {
            return;
        }
        ConfirmMsg("Do you want to save FAQ Details");
        Session["_action"] = "S";


    }
    protected void lbtnReset_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        resetcontrol();

    }
    protected void lbtnCancel_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        lblFAQUpdate.Visible = false;
        lblFAQHead.Visible = true;
        lbtnSave.Visible = true;
        lbtnUpdate.Visible = false;
        lbtnReset.Visible = true;
        lbtnCancel.Visible = false;

        resetcontrol();
    }
    protected void gvFAQ_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        CsrfTokenValidate();
        if (e.CommandName == "updateFAQ")
        {
            lblFAQUpdate.Visible = true;
            lblFAQHead.Visible = false;
            lbtnSave.Visible = false;
            lbtnUpdate.Visible = true;
            lbtnReset.Visible = false;
            lbtnCancel.Visible = true;
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            string FAQId = gvFAQ.DataKeys[row.RowIndex]["faqid"].ToString();
            string faqcategory = gvFAQ.DataKeys[row.RowIndex]["faqcategory"].ToString();
            string faq_question_en = gvFAQ.DataKeys[row.RowIndex]["faq_question_en"].ToString();
            string faq_answer_en = gvFAQ.DataKeys[row.RowIndex]["faq_answer_en"].ToString();
            string faqquestion_local = gvFAQ.DataKeys[row.RowIndex]["faqquestion_local"].ToString();
            string faq_answer_local = gvFAQ.DataKeys[row.RowIndex]["faq_answer_local"].ToString();
            ddlFAQcategory.SelectedValue = faqcategory;
            tbFAQQues.Text = faq_question_en;
            tbFAQAns.Text = faq_answer_en;
            tbFAQQuesLocal.Text = faqquestion_local;
            tbFAQAnsLocal.Text = faq_answer_local;
            Session["faqid"] = FAQId;
        }
        if (e.CommandName == "DeleteFAQ")
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            string FAQId = gvFAQ.DataKeys[row.RowIndex]["faqid"].ToString();
            ConfirmMsg("Do you want to delete FAQ?");
            Session["_action"] = "D";
            Session["faqid"] = FAQId;
        }

    }
    protected void gvFAQ_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvFAQ.PageIndex = e.NewPageIndex;
        FAQList();
    }
    protected void ddlFAQCategory1_SelectedIndexChanged(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        FAQList();
    }

    #endregion
}