using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_etmBranchEtmManagement : BasePage
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
        //	Session["_UserCode"] = "Admin";
        //lblSummary.Text = "Summary as on Date " + DateTime.Now.ToString("dd/MM/yyyy hh:ss tt");
        Session["_moduleName"] = "ETM Management";
        if (IsPostBack == false)
        {
            int usrrole = Convert.ToInt16(Session["_RoleCode"].ToString());
            switch (usrrole)
            {
                case 5:
                    {
                        this.Title = "Depot Manager";
                        break;
                    }
                case 12:
                    {
                        this.Title = "ETM Branch";
                        break;
                    }
                case 13:
                    {
                        this.Title = "Store Manager";
                        break;
                    }
                default:
                    {
                        this.Title = "Depot Manager";
                        break;
                    }
            }
            LoadETMtype();
            //LoadETMStatus(ddlStatus);
            LoadETMStatus(ddlETMStatus);
            loadEtmDetails();
        }
    }
    #region "Methods"
    
   
    public void LoadETMtype()//M2
    {
        try
        {

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_etmtype");

            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                ddlSETMType.DataSource = dt;
                ddlSETMType.DataTextField = "etmtype_name";
                ddlSETMType.DataValueField = "etmtype_id";
                ddlSETMType.DataBind();
            }

            ddlSETMType.Items.Insert(0, "All");
            ddlSETMType.Items[0].Value = "0";
            ddlSETMType.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlSETMType.Items.Insert(0, "All");
            ddlSETMType.Items[0].Value = "0";
            ddlSETMType.SelectedIndex = 0;
            _common.ErrorLog("SysAdmETM-M2", ex.Message.ToString());
        }
    }
    public void LoadETMStatus(DropDownList ddl)//M3
    {
        try
        {
            int usrrole = Convert.ToInt16(Session["_RoleCode"].ToString());
            string statustype = "O";
            if (usrrole == 12)
            {
                statustype = "E";
            }
            ddl.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_etmstatus");
            MyCommand.Parameters.AddWithValue("sp_statustype", statustype);

            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                ddl.DataSource = dt;
                ddl.DataTextField = "status_name";
                ddl.DataValueField = "status_id";
                ddl.DataBind();
            }
            ddl.Items.Insert(0, "SELECT");
            ddl.Items[0].Value = "100";
            ddl.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddl.Items.Insert(0, "SELECT");
            ddl.Items[0].Value = "100";
            ddl.SelectedIndex = 0;
            _common.ErrorLog("SysAdmETM-M3", ex.Message.ToString());
        }
    }
    public void loadEtmDetails()//M6
    {
        try
        {
            int usrrole = Convert.ToInt16(Session["_RoleCode"].ToString());
            string assignedOfcType = "";
            int office = 1;
            if (usrrole == 12)
            {
                office = Convert.ToInt32(Session["_etmbranchid"]);
                assignedOfcType = "E";
            }
            else if (usrrole == 13)
            {
                office = Convert.ToInt32(Session["_storeid"]);
                assignedOfcType = "S";
            }
            else if (usrrole == 5)
            {
                office = Convert.ToInt32(Session["_LDepotCode"]);
                assignedOfcType = "D";
            }



            string serialNo = tbETMSerialNumber.Text.ToString();
            Int32 etmType = Convert.ToInt32(ddlSETMType.SelectedValue);
            Int32 etmStatus = Convert.ToInt32(ddlStatus.SelectedValue);
            pnlNoETM.Visible = true;
            gvETMDetails.Visible = false;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_etmdetails_for_etmbranch");
            MyCommand.Parameters.AddWithValue("p_assignedoffice", office.ToString());
            MyCommand.Parameters.AddWithValue("p_serialno", serialNo);
            MyCommand.Parameters.AddWithValue("p_etmtype", etmType);
            MyCommand.Parameters.AddWithValue("p_etmstatus", etmStatus);

            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                gvETMDetails.DataSource = dt;
                gvETMDetails.DataBind();
                pnlNoETM.Visible = false;
                gvETMDetails.Visible = true;

                lblTotalETM.Text = dt.Rows[0]["totaletm"].ToString();
                lblfreeetm.Text = dt.Rows[0]["freeetm"].ToString();
                lblondutyetm.Text = dt.Rows[0]["ondutyetm"].ToString();
                //lblLockedETM.Text = dt.Rows[0]["lockedetm"].ToString();

                //lblIssuedToStore.Text = dt.Rows[0]["issuedtostore"].ToString();
                // lblPendingReceiving.Text = dt.Rows[0]["pendingreceive"].ToString();
                // lblPending.Text = dt.Rows[0]["pendingissuetobranch"].ToString();

            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message);
            _common.ErrorLog("SysAdmETM-M6", ex.Message.ToString());
        }
    }
    public void updateETMStatus()
    {
        try
        {
            string etmID = Session["_etmRefNo"].ToString();
            string UpdatedBy = Session["_UserCode"].ToString();
            string IpAddress = HttpContext.Current.Request.UserHostAddress;
            string statusDate = "";//tbStatusDate.Text.ToString();
            Int32 status = Convert.ToInt32(ddlETMStatus.SelectedValue.ToString());
            Int32 issuedofc = 1;// Convert.ToInt32(ddlIssueOffice2.SelectedValue.ToString());
            string remark = tbRemark.Text.ToString();
            MyCommand = new NpgsqlCommand();

            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_etm_allotment");
            MyCommand.Parameters.AddWithValue("p_etmid", etmID);
            MyCommand.Parameters.AddWithValue("p_status", status);
            MyCommand.Parameters.AddWithValue("p_issueoffice", issuedofc);
            MyCommand.Parameters.AddWithValue("p_statusdate", statusDate);
            MyCommand.Parameters.AddWithValue("p_remark", remark);
            MyCommand.Parameters.AddWithValue("p_updatedby", UpdatedBy);
            MyCommand.Parameters.AddWithValue("p_ipaddress", IpAddress);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    string Maction = dt.Rows[0]["p_successyn"].ToString();
                    if (Maction == "Y")
                    {
                        if (ddlETMStatus.SelectedValue.ToString() == "2")
                        {
                            Successmsg("ETM has been Issued to : " );
                        }
                        else
                        {
                            Successmsg("ETM status has been changed successfully");
                        }
                        ddlETMStatus.SelectedIndex = 0;
                        loadEtmDetails();
                        Session["_etmRefNo"] = "";
                        Session["_action"] = "";
                    }
                    else
                    {
                        mpETMAllot.Hide();
                        Errormsg("ETM cannot be Issued to Depot as it is allocated to current trip");
                    }
                }
            }
            else
            {
                Errormsg("Error occurred while Updation. " + dt.TableName);
                return;
            }
        }
        catch (Exception ex)
        {
            Errormsg("Error occurred while Updation. " + ex.Message);
            return;
        }
    }
    protected void receiveETM()
    {
        try
        {
            string etmID = Session["_etmRefNo"].ToString();
            string UpdatedBy = Session["_UserCode"].ToString();
            string IpAddress = HttpContext.Current.Request.UserHostAddress;

            string Mresult = "";
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_etm_receive");
            MyCommand.Parameters.AddWithValue("sp_etmid", etmID);
            MyCommand.Parameters.AddWithValue("p_updatedby", UpdatedBy);
            MyCommand.Parameters.AddWithValue("p_ipaddress", IpAddress);
            Mresult = bll.UpdateAll(MyCommand);
            if (Mresult == "Success")
            {
                Successmsg("ETM has been received");
                loadEtmDetails();
                Session["_etmRefNo"] = "";
                Session["_action"] = "";
            }
            else
            {
                Errormsg("Error occurred while Updation. " + Mresult);
                return;
            }
        }
        catch (Exception ex)
        {
            Errormsg("Error occurred while Updation. " + ex.Message);
            return;
        }
    }
    private void Errormsg(string msg)
    {
        string popup = _popup.modalPopupSmall("W", "Please Note", msg, "Close");
        Response.Write(popup);
    }
    private void Successmsg(string msg)
    {
        string popup = _popup.modalPopupSmall("S", "Success", msg, "OK");
        Response.Write(popup);
    }
    private void ConfirmMsg(string msg)
    {
        lblConfirmation.Text = msg;
        mpConfirmation.Show();
    }
    private void InfoMsg(string msg)
    {
        string popup = _popup.modalPopupLarge("I", "Information", msg, "Close");
        Response.Write(popup);
    }
    private void returnToStore()
    {
        try {
            string etmID = Session["etm_refno"].ToString();
            string UpdatedBy = Session["_UserCode"].ToString();
            string IpAddress = HttpContext.Current.Request.UserHostAddress;

            string Mresult = "";
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_etm_return_store");
            MyCommand.Parameters.AddWithValue("sp_etmid", etmID);
            MyCommand.Parameters.AddWithValue("p_updatedby", UpdatedBy);
            MyCommand.Parameters.AddWithValue("p_ipaddress", IpAddress);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                Successmsg("ETM Return To Store Successfully");
                loadEtmDetails();
                Session["etm_refno"] = "";
                Session["_action"] = "";
            }
            else
            {
                Errormsg("Error occurred while Updation. " + Mresult);
                return;
            }
        }
        catch (Exception ex)
        {
            Errormsg("Error occurred while Updation. " );
            return;
        }
    }
    #endregion
    #region "Events"
    protected void gvETMDetails_RowCommand(object sender, GridViewCommandEventArgs e)//E1
    {
        try
        {
            int index = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "ReturnETM")
            {
                try
                {
                    tbRemark.Text = "";
                   // trOfcDetails.Visible = false;
                    string etmRefNo = gvETMDetails.DataKeys[index].Values["etm_id"].ToString();
                    Session["etm_refno"] = etmRefNo;
                    if (gvETMDetails.DataKeys[index].Values["etmmakemodel"].ToString() != "")
                    {
                        lblMakeModel.Text = gvETMDetails.DataKeys[index].Values["etmmakemodel"].ToString();
                    }
                    else
                    {
                        lblMakeModel.Text = "";
                    }
                    if (gvETMDetails.DataKeys[index].Values["etmserialno"].ToString() != "")
                    {
                        lblSerialNo.Text = gvETMDetails.DataKeys[index].Values["emttype_name"].ToString() + "-" + gvETMDetails.DataKeys[index].Values["etmserialno"].ToString();
                    }
                    else
                    {
                        lblSerialNo.Text = "";
                    }
                    if (gvETMDetails.DataKeys[index].Values["etm_status"].ToString() != "")
                    {
                        lblStatus.Text = gvETMDetails.DataKeys[index].Values["etm_status"].ToString();
                        Session["etmCurrentStatus"]= gvETMDetails.DataKeys[index].Values["etm_status"].ToString();
                    }
                    else
                    {
                        lblStatus.Text = "";
                    }
                    if (gvETMDetails.DataKeys[index].Values["imeino_1"].ToString() != "" && gvETMDetails.DataKeys[index].Values["imeino_2"].ToString() != "")
                    {
                        lblIMEINo.Text = gvETMDetails.DataKeys[index].Values["imeino_1"].ToString() + "," + gvETMDetails.DataKeys[index].Values["imeino_2"].ToString();
                    }
                    else if (gvETMDetails.DataKeys[index].Values["imeino_1"].ToString() != "" && gvETMDetails.DataKeys[index].Values["imeino_2"].ToString() == "")
                    {
                        lblIMEINo.Text = gvETMDetails.DataKeys[index].Values["imeino_1"].ToString();
                    }
                    else
                    {
                        lblIMEINo.Text = "N/A";
                    }
                    if (gvETMDetails.DataKeys[index].Values["agency_name"].ToString() != "")
                    {
                        lblAgency.Text = gvETMDetails.DataKeys[index].Values["agency_name"].ToString();
                    }
                    else
                    {
                        lblAgency.Text = "";
                    }

                    if (gvETMDetails.DataKeys[index].Values["etm_status"].ToString() !="")
                    {
                        LoadETMStatus(ddlETMStatus);
                        ddlETMStatus.Items.Remove(ddlETMStatus.Items.FindByValue("0"));
                        ddlETMStatus.Items.Remove(ddlETMStatus.Items.FindByValue("1"));
                        ddlETMStatus.Items.Remove(ddlETMStatus.Items.FindByValue("2"));
                        ddlETMStatus.Items.Remove(ddlETMStatus.Items.FindByValue("3"));
                        ddlETMStatus.Items.Remove(ddlETMStatus.Items.FindByValue("4"));
                        ddlETMStatus.Items.Remove(ddlETMStatus.Items.FindByValue("6"));
                        ddlETMStatus.Items.Remove(ddlETMStatus.Items.FindByValue("7"));
                    }
                    mpETMAllot.Show();
                   
                }
                catch (Exception ex)
                {
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("SysAdmETM-E1", ex.Message.ToString());
        }
    }
    protected void ddlETMStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        //trOfcDetails.Visible = false;
        //if (ddlETMStatus.SelectedValue.ToString() == "2" | ddlETMStatus.SelectedValue.ToString() == "4" | ddlETMStatus.SelectedValue.ToString() == "6")
        //{
        //    LoadStore();
        //    trOfcDetails.Visible = true;
        //}
        //else if (ddlETMStatus.SelectedValue.ToString() == "3")
        //{
        //    loadETMBranch();
        //    trOfcDetails.Visible = true;
        //}
    }
    protected void gvETMDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    int usrrole = Convert.ToInt16(Session["_RoleCode"].ToString());
        //    string assignedOfcType = "";
        //    string office = "";
        //    string storeid = "0";
        //    if (usrrole == 12)
        //    {
        //        office = Session["_etmbranchid"].ToString();
        //        assignedOfcType = "E";
        //    }
        //    else if (usrrole == 13)
        //    {
        //        storeid = Session["_storeid"].ToString();
        //        office = Session["_storeid"].ToString();
        //        assignedOfcType = "S";
        //    }
        //    else if (usrrole == 5)
        //    {
        //        office = Session["_LDepotCode"].ToString();
        //        assignedOfcType = "D";
        //    }

        //    DataRowView rowView = (DataRowView)e.Row.DataItem;
        //    LinkButton lbtnreceiveETM = (LinkButton)e.Row.FindControl("lbtnreceiveETM");
        //    LinkButton lbtnETMIssue = (LinkButton)e.Row.FindControl("lbtnETMIssue");
        //    Label lblETMStatus = (Label)e.Row.FindControl("lblETMStatus");
        //    lbtnreceiveETM.Visible = false;
        //    lbtnETMIssue.Enabled = true;
        //    lbtnETMIssue.CssClass = "btn btn-sm btn-primary";
        //    if (rowView["statustypeid"].ToString() == "2" && rowView["assignedoffice"].ToString() == storeid.ToString())
        //    {
        //        lbtnETMIssue.CssClass = "btn btn-sm btn-gray btn-outline-dark";
        //        lbtnreceiveETM.Visible = true;
        //        lblETMStatus.Text = "Pending for Receive";
        //        lbtnETMIssue.Enabled = false;
        //    }
        //    if (rowView["assignedofficetype"].ToString() != assignedOfcType || rowView["assignedoffice"].ToString() != office)
        //    {
        //        lbtnETMIssue.CssClass = "btn btn-sm btn-gray btn-outline-dark";
        //        lbtnETMIssue.Enabled = false;
        //    }
        //}
    }
    protected void gvETMDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvETMDetails.PageIndex = e.NewPageIndex;
        loadEtmDetails();
    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        //if (Session["_action"].ToString() == "U")
        //{
        //    updateETMStatus();
        //}
        //else if (Session["_action"].ToString() == "R")
        //{
        //    receiveETM();
        //}
        if (Session["_Action"].ToString() == "RS")//Etm return to store
        {
            returnToStore();
        }
    }

    protected void lbtnSearch_Click(object sender, EventArgs e)
    {
        loadEtmDetails();
    }
    protected void lbtnResetSearch_Click(object sender, EventArgs e)
    {

        tbETMSerialNumber.Text = "";
        ddlSETMType.SelectedValue = "0";
        ddlStatus.SelectedValue = "0";
        loadEtmDetails();
    }
    protected void lbtnAllotETM_Click(object sender, EventArgs e)
    {
        if (ddlETMStatus.SelectedValue == "100")
        {
            Errormsg("Please Select ETM Status");
            return;
        }

        if (Session["etmCurrentStatus"].ToString() == "On Duty")
        {
            //mpETMAllot.Show();
            Errormsg("ETM is busy.");
            return;
        }
        Session["_action"] = "RS";
        ConfirmMsg("Do you want to update ETM Status");
    }
    protected void lbtnview_Click(object sender, EventArgs e)
    {
        string msg = "";
        msg = msg + "1.  Add ETM details along with its receiving office.<br/>";
        msg = msg + "2.  If ETM is issued to ETM Branch then it will be available for waybill issuance but if ETM is issued to Store then Store will first receive it then issue it to another store or ETM Branch.<br/>";
        msg = msg + "3.  View/Update/Delete ETM Details<br/>";
        msg = msg + "4.  Only Drafts ETM can be updated or deleted. Once Locked you cannot update or delete the ETM<br/>";
        msg = msg + "5.  Lock drafts ETM<br/>";
        InfoMsg(msg);
    }
    protected void lbtnDownloadExcel_Click(object sender, EventArgs e)
    {
        ExportGridToExcel();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }
    private void ExportGridToExcel()
    {
        Response.Clear();
        Response.Buffer = true;
        gvETMDetails.AllowPaging = false;
        this.loadEtmDetails();
        if (gvETMDetails.Rows.Count > 0)
        {
            Response.AddHeader("content-disposition", "attachment;filename=EtmList.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                // To Export all pages

                gvETMDetails.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in gvETMDetails.HeaderRow.Cells)
                    cell.BackColor = gvETMDetails.HeaderStyle.BackColor;
                foreach (GridViewRow row in gvETMDetails.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                            cell.BackColor = gvETMDetails.AlternatingRowStyle.BackColor;
                        else
                            cell.BackColor = gvETMDetails.RowStyle.BackColor;
                        cell.CssClass = "textmode";
                    }
                }

                gvETMDetails.RenderControl(hw);
                // style to format numbers to string
                string style = "<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
        else
        {
            Errormsg("Sorry, no record is available.");
            return;
        }
    }
    #endregion
}