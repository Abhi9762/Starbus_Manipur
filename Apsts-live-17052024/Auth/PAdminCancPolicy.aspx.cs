using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

public partial class Auth_PAdminCancPolicy : BasePage 
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
        Session["_moduleName"] = "Cancellaiton Policy Configuration";
        if (!IsPostBack)
        {
            Random randomclass = new Random();
            Session["rndNoCheck"] = randomclass.Next().ToString();
            hidtoken.Value = Session["rndNoCheck"].ToString();
            SetInitialRowForCnclPlcy();
            getCancellationPolicy();

            lblMaxHours.Text = getdays().ToString();
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
    private void getCancellationPolicy()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_policy");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    gvCurrentPolicy.DataSource = dt;
                    gvCurrentPolicy.DataBind();
                    gvCurrentPolicy.Visible = true;
                    pnlNoRecord.Visible = false;
                }
                else
                {
                    gvCurrentPolicy.Visible = false;
                    pnlNoRecord.Visible = true;
                }
            }
            else
            {
                gvCurrentPolicy.Visible = false;
                pnlNoRecord.Visible = true;
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminCancPolicy.aspx-0001", ex.Message.ToString());
            gvCurrentPolicy.Visible = false;
            pnlNoRecord.Visible = true;
        }
    }
    private void SetInitialRowForCnclPlcy()
    {
        DataTable dt = new DataTable();
        DataRow dr = null/* TODO Change to default(_) if this is not a reference type */;
        dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("ColFrom", typeof(string)));
        dt.Columns.Add(new DataColumn("ColTo", typeof(string)));
        dt.Columns.Add(new DataColumn("ColDeduct", typeof(string)));
        dt.Columns.Add(new DataColumn("ColDeductInType", typeof(string)));

        dr = dt.NewRow();
        dr["RowNumber"] = 1;
        dr["ColFrom"] = "0";
        dr["ColTo"] = string.Empty;
        dr["ColDeduct"] = string.Empty;
        dr["ColDeductInType"] = string.Empty;
        dt.Rows.Add(dr);
        ViewState["CurrentTable"] = dt;
        gvnewcancpolicy.DataSource = dt;
        gvnewcancpolicy.DataBind();

        TextBox tbFrom = (TextBox)gvnewcancpolicy.Rows[0].Cells[1].FindControl("tbfromHrs");
        tbFrom.Text = "0";
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
    private void AddNewRowToGrid()//M1
    {
        try
        {
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null/* TODO Change to default(_) if this is not a reference type */;

                if (dtCurrentTable.Rows.Count > 0)
                {
                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["RowNumber"] = dtCurrentTable.Rows.Count + 1;
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;

                    for (int i = 0; i < dtCurrentTable.Rows.Count - 1; i++)
                    {
                        TextBox tbFrom = (TextBox)gvnewcancpolicy.Rows[i].Cells[1].FindControl("tbfromHrs");
                        TextBox tbTo = (TextBox)gvnewcancpolicy.Rows[i].Cells[2].FindControl("tbtoHrs");
                        TextBox tbRefund = (TextBox)gvnewcancpolicy.Rows[i].Cells[4].FindControl("tbrefund");
                        CheckBox chkPer = (CheckBox)gvnewcancpolicy.Rows[i].Cells[3].FindControl("rbtnPercentage");
                        CheckBox chkRup = (CheckBox)gvnewcancpolicy.Rows[i].Cells[3].FindControl("rbtnRupee");

                        string fromDuretion = tbFrom.Text.Trim();
                        string toDuretion = tbTo.Text.Trim();
                        string refundPercent = tbRefund.Text.Trim();


                        // ************* from Validation start ***********************
                        if (fromDuretion.Length == 0)
                        {
                            drCurrentRow["RowNumber"] = dtCurrentTable.Rows.Count - 1;
                            dtCurrentTable.Rows.Remove(drCurrentRow);
                            Errormsg("Enter valid From(Hours)");
                            return;
                        }
                        int fromm = Convert.ToInt32(fromDuretion.ToString());


                        // ************* from Validation end ***********************

                        // ************* to Validation start ***********************
                        if (toDuretion.Length == 0)
                        {
                            drCurrentRow["RowNumber"] = dtCurrentTable.Rows.Count - 1;
                            dtCurrentTable.Rows.Remove(drCurrentRow);
                            Errormsg("Enter valid To(Hours)");
                            return;
                        }
                        int too = Convert.ToInt32(toDuretion.ToString());

                        if (too <= fromm & !(too == 0))
                        {
                            drCurrentRow["RowNumber"] = dtCurrentTable.Rows.Count - 1;
                            dtCurrentTable.Rows.Remove(drCurrentRow);
                            Errormsg("Enter valid To(Hours). To(Hours) can not be equal or less then From(Hours)");
                            return;
                        }

                        int days = getdays();
                        if ((too <= 0 | too > days) & !(too == 0))
                        {
                            drCurrentRow["RowNumber"] = dtCurrentTable.Rows.Count - 1;
                            dtCurrentTable.Rows.Remove(drCurrentRow);
                            Errormsg("Enter valid To(Hours). To(Hours) can not be greater then " + days.ToString() + " days or not be equal, less then 0");
                            return;
                        }

                        if (too == 0)
                        {
                            drCurrentRow["RowNumber"] = dtCurrentTable.Rows.Count - 1;
                            dtCurrentTable.Rows.Remove(drCurrentRow);
                            Errormsg("You can not add more row because To(Hours) is empty. Please click on Save button to save new Cancellation Policy.");
                            return;
                        }

                        if (too == days)
                        {
                            drCurrentRow["RowNumber"] = dtCurrentTable.Rows.Count - 1;
                            dtCurrentTable.Rows.Remove(drCurrentRow);
                            Errormsg("You can Not add more row. You already entered max " + days.ToString() + " hours");
                            return;
                        }

                        // ************* to Validation end ***********************

                        string decuteType = "";
                        if (chkPer.Checked == true)
                            decuteType = "P";
                        else if (chkRup.Checked == true)
                            decuteType = "R";

                        // ************* refund Validation start ***********************
                        if (refundPercent.Length == 0)
                        {
                            drCurrentRow["RowNumber"] = dtCurrentTable.Rows.Count - 1;
                            dtCurrentTable.Rows.Remove(drCurrentRow);
                            Errormsg("Enter valid Deduct");
                            return;
                        }
                        int deduct = Convert.ToInt32(refundPercent.ToString());

                        if (deduct < 0 | (decuteType == "P" & deduct > 100))
                        {
                            drCurrentRow["RowNumber"] = dtCurrentTable.Rows.Count - 1;
                            dtCurrentTable.Rows.Remove(drCurrentRow);
                            Errormsg("Enter valid Deduct. Deduct(%) can Not be greater Then 100.");
                        }
                        // ************* refund Validation end ***********************


                        dtCurrentTable.Rows[i]["ColFrom"] = fromDuretion;
                        dtCurrentTable.Rows[i]["ColTo"] = toDuretion;
                        dtCurrentTable.Rows[i]["ColDeduct"] = refundPercent;
                        dtCurrentTable.Rows[i]["ColDeductInType"] = decuteType;
                    }

                    gvnewcancpolicy.DataSource = dtCurrentTable;
                    gvnewcancpolicy.DataBind();
                }
            }
            else
                Errormsg("Error- ViewState Is null");

            SetPreviousData();
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminCancPolicy.aspx-0002", ex.Message.ToString());
        }
    }
    private void SetPreviousData()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TextBox tbFrom = (TextBox)gvnewcancpolicy.Rows[i].Cells[1].FindControl("tbfromHrs");
                    TextBox tbTo = (TextBox)gvnewcancpolicy.Rows[i].Cells[2].FindControl("tbtoHrs");
                    TextBox tbferund = (TextBox)gvnewcancpolicy.Rows[i].Cells[4].FindControl("tbrefund");
                    CheckBox chkPer = (CheckBox)gvnewcancpolicy.Rows[i].Cells[3].FindControl("rbtnPercentage");
                    CheckBox chkRup = (CheckBox)gvnewcancpolicy.Rows[i].Cells[3].FindControl("rbtnRupee");

                    if (i < dt.Rows.Count - 1)
                    {
                        tbFrom.Text = dt.Rows[i]["ColFrom"].ToString();
                        tbTo.Text = dt.Rows[i]["ColTo"].ToString();
                        tbferund.Text = dt.Rows[i]["ColDeduct"].ToString();
                        if (dt.Rows[i]["ColDeductInType"].ToString() == "P")
                            chkPer.Checked = true;
                        else if (dt.Rows[i]["ColDeductInType"].ToString() == "R")
                            chkRup.Checked = true;
                    }

                    if (i == 0)
                    {
                        tbFrom.Text = "0";
                    }
                    if (i > 0)
                    {
                        TextBox tb = (TextBox)gvnewcancpolicy.Rows[i - 1].Cells[2].FindControl("tbtoHrs");
                        tbFrom.Text = tb.Text;
                    }

                    rowIndex += 1;
                }
            }
        }
    }
    private int getdays()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_advancedays");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                }
            }
            return (30 * 24);
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminCancPolicy.aspx-0003", ex.Message.ToString());
            return (30 * 24);
        }

    }
    private void ResetRowID(DataTable dt)
    {
        int rowNumber = 1;

        if (dt.Rows.Count > 0)
        {
            foreach (DataRow row in dt.Rows)
            {
                row[0] = rowNumber;
                rowNumber += 1;
            }
        }
    }
    private bool validcancelpolicy()
    {
        try
        {
            DataTable dtt = getCnclPlcy_table();
            ViewState["cnclPlcyTable"] = dtt;
            if (ViewState["cnclPlcyTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["cnclPlcyTable"];
                DataRow drCurrentRow = null/* TODO Change to default(_) if this is not a reference type */;
                for (int i = 0; i < dt.Rows.Count - 1; i++)
                {
                    TextBox tbFrom = (TextBox)gvnewcancpolicy.Rows[i].Cells[1].FindControl("tbfromHrs");
                    TextBox tbTo = (TextBox)gvnewcancpolicy.Rows[i].Cells[2].FindControl("tbtoHrs");
                    TextBox tbRefund = (TextBox)gvnewcancpolicy.Rows[i].Cells[4].FindControl("tbrefund");
                    CheckBox chkPer = (CheckBox)gvnewcancpolicy.Rows[i].Cells[3].FindControl("rbtnPercentage");
                    CheckBox chkRup = (CheckBox)gvnewcancpolicy.Rows[i].Cells[3].FindControl("rbtnRupee");

                    string fromDuretion = tbFrom.Text.Trim();
                    string toDuretion = tbTo.Text.Trim();
                    string refundPercent = tbRefund.Text.Trim();


                    // ************* from Validation start ***********************
                    if (fromDuretion.Length == 0)
                    {
                        drCurrentRow["RowNumber"] = dt.Rows.Count - 1;
                        dt.Rows.Remove(drCurrentRow);
                        Errormsg("Enter valid From(Hours)");
                        return false;
                    }
                    int fromm = Convert.ToInt32(fromDuretion.ToString());
                    // ************* from Validation end ***********************


                    // ************* to Validation start ***********************
                    if (toDuretion.Length == 0)
                    {
                        drCurrentRow["RowNumber"] = dt.Rows.Count - 1;
                        dt.Rows.Remove(drCurrentRow);
                        Errormsg("Enter valid To(Hours)");
                        return false;
                    }
                    int too = Convert.ToInt32(toDuretion.ToString());
                    if (too <= fromm)
                    {
                        drCurrentRow["RowNumber"] = dt.Rows.Count - 1;
                        dt.Rows.Remove(drCurrentRow);
                        Errormsg("Enter valid To(Hours). To(Hours) can not be equal or less then From(Hours)");
                        return false;
                    }

                    int days = getdays();
                    if (too <= 0 | too > days)
                    {
                        drCurrentRow["RowNumber"] = dt.Rows.Count - 1;
                        dt.Rows.Remove(drCurrentRow);
                        Errormsg("Enter valid To(Hours). To(Hours) can not be greater then " + days.ToString() + " days or not be equal, less then 0");
                        return false;
                    }

                    if (too == days)
                    {
                        drCurrentRow["RowNumber"] = dt.Rows.Count - 1;
                        dt.Rows.Remove(drCurrentRow);
                        Errormsg("You can not add more row. You already entered max " + days.ToString() + " hours");
                        return false;
                    }

                    // ************* to Validation end ***********************

                    string decuteType = "";
                    if (chkPer.Checked == true)
                        decuteType = "P";
                    else if (chkRup.Checked == true)
                        decuteType = "R";

                    // ************* refund Validation start ***********************
                    if (refundPercent.Length == 0)
                    {
                        drCurrentRow["RowNumber"] = dt.Rows.Count - 1;
                        dt.Rows.Remove(drCurrentRow);
                        Errormsg("Enter valid Deduct");
                        return false;
                    }
                    int deduct = Convert.ToInt32(refundPercent.ToString());

                    if (deduct < 0 | (decuteType == "P" & deduct > 100))
                    {
                        drCurrentRow["RowNumber"] = dt.Rows.Count - 1;
                        dt.Rows.Remove(drCurrentRow);
                        Errormsg("Enter valid Deduct. Deduct(%) can not be greater then 100.");
                        return false;
                    }
                    return true;
                }
                Errormsg("Add Cancellation hours and deductions before saving");
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminCancPolicy.aspx-0004", ex.Message.ToString());
            return false;
        }
    }
    private DataTable getCnclPlcy_table()
    {
        // Create a new DataTable.
        DataTable dt = new DataTable("cnclPlcyTable");
        dt.Columns.Add(new DataColumn("ColFrom", typeof(string)));
        dt.Columns.Add(new DataColumn("ColTo", typeof(string)));
        dt.Columns.Add(new DataColumn("ColDeduct", typeof(string)));
        dt.Columns.Add(new DataColumn("ColRefund", typeof(string)));
        dt.Columns.Add(new DataColumn("ColDeductInType", typeof(string)));

        string fromHrs, toHrs, refundPercent, deductPercent, duductType;
        foreach (GridViewRow row in gvnewcancpolicy.Rows)
        {
            TextBox tbFrom = (TextBox)row.FindControl("tbfromHrs");
            TextBox tbTo = (TextBox)row.FindControl("tbtoHrs");
            TextBox tbRefund = (TextBox)row.FindControl("tbrefund");
            CheckBox chkPer = (CheckBox)row.FindControl("rbtnPercentage");
            CheckBox chkRup = (CheckBox)row.FindControl("rbtnRupee");

            fromHrs = tbFrom.Text.Trim();
            toHrs = tbTo.Text.Trim();
            deductPercent = tbRefund.Text.Trim();
            duductType = "";
            int deduct;

            if (chkPer.Checked == true)
            {
                duductType = "P";
            }
            else if (chkRup.Checked == true)
            {
                duductType = "R";
            }
            if (duductType == "P")
            {
                if (int.TryParse(tbRefund.Text.Trim(), out deduct))
                {
                    refundPercent = (100 - deduct).ToString();
                }
                else
                {
                    refundPercent = "0";
                }

            }
            else
            {
                refundPercent = "0";
            }
            if (deductPercent.Length > 0)
            {
                dt.Rows.Add(fromHrs, toHrs, deductPercent, refundPercent, duductType);
            }
        }


        return dt;
    }
    private void SaveCancellationpolicy()
    {
        try
        {
            DataTable dttp = getCnclPlcy_table();
            ViewState["cnclPlcyTable"] = dttp;
            if (ViewState["cnclPlcyTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["cnclPlcyTable"];
                ArrayList policyArr = new ArrayList();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TextBox tbFrom = (TextBox)gvnewcancpolicy.Rows[i].Cells[1].FindControl("tbfromHrs");
                    TextBox tbTo = (TextBox)gvnewcancpolicy.Rows[i].Cells[2].FindControl("tbtoHrs");
                    TextBox tbRefund = (TextBox)gvnewcancpolicy.Rows[i].Cells[4].FindControl("tbrefund");
                    CheckBox chkPer = (CheckBox)gvnewcancpolicy.Rows[i].Cells[3].FindControl("rbtnPercentage");
                    CheckBox chkRup = (CheckBox)gvnewcancpolicy.Rows[i].Cells[3].FindControl("rbtnRupee");

                    string fromHrs = tbFrom.Text.Trim();
                    string toHrs = tbTo.Text.Trim();
                    string deductPercent = tbRefund.Text.Trim();
                    string duductType = "";
                    int deduct;
                    string refundPercent = "";

                    if (chkPer.Checked == true)
                    {
                        duductType = "P";
                    }
                    else if (chkRup.Checked == true)
                    {
                        duductType = "R";
                    }
                    if (duductType == "P")
                    {
                        if (int.TryParse(tbRefund.Text.Trim(), out deduct))
                        {
                            refundPercent = (100 - deduct).ToString();
                        }
                        else
                        {
                            refundPercent = "0";
                        }

                    }
                    else
                    {
                        refundPercent = "0";
                    }
                    if (deductPercent.Length > 0)
                    {
                        policyArr.Add(fromHrs + "_" + toHrs + "_" + deductPercent + "_" + refundPercent + "_" + duductType);
                    }
                }
                string[] cncpArr = (string[])policyArr.ToArray(typeof(string));
                MyCommand = new NpgsqlCommand();
                DataTable dtt = new DataTable();
                string ipaddress = HttpContext.Current.Request.UserHostAddress;
                MyCommand.Parameters.Clear();
                MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_insert_cancpolicy");
                MyCommand.Parameters.AddWithValue("p_policy", cncpArr);
                MyCommand.Parameters.AddWithValue("p_updatedby", Session["_UserCode"].ToString());
                MyCommand.Parameters.AddWithValue("p_ipaddress", ipaddress);
                dt = bll.SelectAll(MyCommand);
                if (dt.TableName == "Success")
                {
                    Successmsg("Cancellation Policy Successfully Updated");
                    SetInitialRowForCnclPlcy();
                    getCancellationPolicy();
                }
                else
                {
                    Errormsg("Cancellation Policy not Updated due to a technical issue" + dt.TableName.ToString());
                    //_common.ErrorLog("PAOrgReg-M8", dt.TableName.ToString());
                }



            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminCancPolicy.aspx-0005", ex.Message.ToString());
        }
    }

    #endregion

    #region "Event"
    protected void lbtnadd_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        AddNewRowToGrid();
    }
    protected void gvnewcancpolicy_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            LinkButton lbtnadd = (LinkButton)e.Row.FindControl("lbtnadd");
            LinkButton lbtnremove = (LinkButton)e.Row.FindControl("lbtnremove");

            if (lbtnremove != null)
            {
                if (dt.Rows.Count > 1)
                {
                    lbtnadd.Visible = false;
                    if (e.Row.RowIndex == dt.Rows.Count - 1)
                    {
                        lbtnremove.Visible = false;
                        lbtnadd.Visible = true;
                    }
                }
                else
                {
                    lbtnremove.Visible = false;
                    lbtnadd.Visible = true;
                }
            }
        }
    }
    protected void lbtnremove_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];

            if (dt.Rows.Count > 1)
            {
                if (gvRow.RowIndex < dt.Rows.Count - 1)
                {
                    dt.Rows.Remove(dt.Rows[rowID]);
                    ResetRowID(dt);
                }
            }

            ViewState["CurrentTable"] = dt;
            gvnewcancpolicy.DataSource = dt;
            gvnewcancpolicy.DataBind();
        }

        SetPreviousData();
    }
    protected void lbCnclPlcySave_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (validcancelpolicy() == false)
        {
            return;
        }
        // SaveCancellationpolicy();
        lblConfirmation.Text = "Do you want to Save Cancellation Policy ?";
        mpConfirmation.Show();
    }
    protected void lbtnHelppolicy_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
    }
    protected void lbtnViewHistorypolicy_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        SaveCancellationpolicy();
    }
    #endregion





}