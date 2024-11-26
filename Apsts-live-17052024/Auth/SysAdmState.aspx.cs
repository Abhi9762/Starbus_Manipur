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

public partial class Auth_SysAdmState : BasePage
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
        lblSummary.Text = "Summary as on Date " + DateTime.Now.ToString("dd/MM/yyyy hh:ss tt");
        Session["_moduleName"] = "State";
        if (!IsPostBack)
        {
            Random randomclass = new Random();
            Session["rndNoCheck"] = randomclass.Next().ToString();
            hidtoken.Value = Session["rndNoCheck"].ToString();
            StateCount();
            allStateList();
        }
    }

    #region "Method"
    private void allStateList()//M0
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.f_get_all_state");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    gvStateList.DataSource = dt;
                    gvStateList.DataBind();
                    gvStateList.Visible = true;
                    pnlNoState.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            Errormsg("Something Went Wrong");
            _common.ErrorLog("SysAdmState.aspx-0001", ex.Message.ToString());
        }
    }
    private void StateCount()//M1
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.f_get_state_count");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    lblTotalState.Text = dt.Rows[0]["total"].ToString();
                    lblActive.Text = dt.Rows[0]["active"].ToString();
                    lblDiscontinue.Text = dt.Rows[0]["discontinue"].ToString();
                    lblConfiguared.Text = dt.Rows[0]["conf"].ToString();

                }
            }
            else
            {
              
                Errormsg(dt.TableName);
            }
        }
        catch (Exception ex)
        {

            Errormsg("Something Went Wrong");
            _common.ErrorLog("SysAdmState.aspx-0002", ex.Message.ToString());
        }
    }
    public void getSearchStateList()//M2
    {
        try
        {
            gvStateList.Visible = false;
            pnlNoState.Visible = true;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.f_get_statesdetail");
            MyCommand.Parameters.AddWithValue("p_statename", tbstatesearch.Text.ToString());
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    gvStateList.DataSource = dt;
                    gvStateList.DataBind();
                    gvStateList.Visible = true;
                    pnlNoState.Visible = false;
                }
            }
            else
            {
            }

        }
        catch (Exception ex)
        {


            Errormsg("Something Went Wrong");
            _common.ErrorLog("SysAdmState.aspx-0003", ex.Message.ToString());
        }
    }
    private void saveState()//M4
    {
        try
        {

            string statecode = "", StateLocal = "", FareType = "", Status = "", IPAddress, accident_charge = "",
            it_charge = "", passenger_charge = "", other_charge = "";
            if (Session["Action"].ToString() == "D")
            {
                statecode = Session["statecode"].ToString();
            }
            if (Session["Action"].ToString() == "Activate")
            {
                statecode = Session["statecode"].ToString();
            }
            if (Session["Action"].ToString() == "Deactive")
            {
                statecode = Session["statecode"].ToString();
            }
            if (Session["Action"].ToString() == "S")
            {
                statecode = Session["statecode"].ToString();

                FareType = Session["faretype"].ToString();
                accident_charge = Session["accident_charge"].ToString();
                it_charge = Session["it_charge"].ToString();
                passenger_charge = Session["Passenger_charge"].ToString();
                other_charge = Session["other_charge"].ToString();
                Status = "A";
            }
            if (Session["Action"].ToString() == "U")
            {
                statecode = Session["statecode"].ToString();
                StateLocal = tbUpdateStateNameL.Text;
                FareType = ddlUpdateFareType.SelectedValue;
                if (cbAccidentSurChrg.Checked == true)
                    accident_charge = "Y";
                else
                    accident_charge = "";

                if (cbItSurChrg.Checked == true)
                    it_charge = "Y";
                else
                    it_charge = "";

                if (cbPassengerSurChrg.Checked == true)
                    passenger_charge = "Y";
                else
                    passenger_charge = "";

                if (cbOtherSurChrg.Checked == true)
                    other_charge = "Y";
                else
                    other_charge = "";



            }
            statecode = Session["statecode"].ToString();
            IPAddress = HttpContext.Current.Request.UserHostAddress;
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.f_state_insertupdate");
            MyCommand.Parameters.AddWithValue("p_action", Session["Action"].ToString());
            MyCommand.Parameters.AddWithValue("p_statecode", statecode);
            MyCommand.Parameters.AddWithValue("p_statelocal", StateLocal);
            MyCommand.Parameters.AddWithValue("p_faretype", FareType);
            MyCommand.Parameters.AddWithValue("p_status", Status);
            MyCommand.Parameters.AddWithValue("p_accident_surcharge", accident_charge);
            MyCommand.Parameters.AddWithValue("p_it_surcharge", it_charge);
            MyCommand.Parameters.AddWithValue("p_passenger_surcharge", passenger_charge);
            MyCommand.Parameters.AddWithValue("p_other_surcharge", other_charge);
            MyCommand.Parameters.AddWithValue("p_updatedby", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("p_ipaddress", IPAddress);
            //string Mresult = bll.UpdateAll(MyCommand);

            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count>0)
            {
                if (dt.Rows[0]["f_state_insertupdate"].ToString()== "Office Created")
                {
                    Errormsg("You can not delete this state because you already create offices under this state.");
                }
                else if (dt.Rows[0]["f_state_insertupdate"].ToString() == "Station Created")
                {
                    Errormsg("You can not delete this state because you already create Stations under this state.");
                }
                else
                {
                    if (Session["Action"].ToString() == "U")
                    {
                        StateCount();
                        allStateList();
                        Successmsg("State Details Updated Successfully !");
                    }
                    if (Session["Action"].ToString() == "S")
                    {
                        StateCount();
                        allStateList();
                        Successmsg("State Details Saved Successfully !");
                    }

                    if (Session["Action"].ToString() == "Active")
                    {
                        StateCount();

                        allStateList();
                        Successmsg("State Activated !");
                    }
                    if (Session["Action"].ToString() == "D")
                    {
                        StateCount();

                        allStateList();
                        Successmsg("State Deleted Successfully !");
                    }
                    if (Session["Action"].ToString() == "Deactive")
                    {
                        StateCount();

                        allStateList();
                        Successmsg("State Deactivated  Successfully!");
                    }
                }
            }
            else
            {
                Errormsg("Something Went Wrong.");
            }

        }
        catch (Exception ex)
        {

            Errormsg("Something Went Wrong");
            _common.ErrorLog("SysAdmState.aspx-0004", ex.Message.ToString());
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
    private void InfoMsg(string msg)
    {
        string popup = _popup.modalPopupSmall("I", "Information", msg, "Close");
        Response.Write(popup);
    }
    private Boolean Validaion()
    {
        try
        {
            int count = 0;
            string msg = "";



            if ((ddlUpdateFareType.SelectedValue) == "0")
            {
                count++;
                msg = msg + count + ".  Select Fare Type <br/>";
            }
            if (count > 0)
            {
                Errormsg(msg);
                return false;
            }
            return true;
        }

        catch (Exception)
        {
            return false;
        }

    }
    private void CsrfTokenValidate()
    {
        if (hidtoken.Value != Session["rndNoCheck"].ToString())
        {
            Response.Redirect("errorpage.aspx");
            return;
        }
    }
    #endregion

    #region "Event"
    protected void gvStateList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        CsrfTokenValidate();
        int rowIndex = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = gvStateList.Rows[rowIndex];
        if (e.CommandName == "UpdateState")
        {
            cbItSurChrg.Checked = false;
            cbOtherSurChrg.Checked = false;
            cbPassengerSurChrg.Checked = false;
            cbAccidentSurChrg.Checked = false;
            pnlUpdateState.Visible = true;
            pnlStateAll.Visible = true;

            string statecode = gvStateList.DataKeys[row.RowIndex]["stat_code"].ToString();
            string StateName = gvStateList.DataKeys[row.RowIndex]["state_name"].ToString();
            string StateAbbr = gvStateList.DataKeys[row.RowIndex]["state_abbr"].ToString();
            string faretype = gvStateList.DataKeys[row.RowIndex]["faretype"].ToString();
            string Accident = gvStateList.DataKeys[row.RowIndex]["accident_charge"].ToString();
            if (Accident == "Y")
                cbAccidentSurChrg.Checked = true;
            string passenger = gvStateList.DataKeys[row.RowIndex]["passenger_charge"].ToString();
            if (passenger == "Y")
                cbPassengerSurChrg.Checked = true;
            string Other = gvStateList.DataKeys[row.RowIndex]["other_charge"].ToString();
            if (Other == "Y")
                cbOtherSurChrg.Checked = true;
            string it = gvStateList.DataKeys[row.RowIndex]["it_charge"].ToString();
            if (it == "Y")
                cbItSurChrg.Checked = true;

            string status = gvStateList.DataKeys[row.RowIndex]["status"].ToString();

            Session["statecode"] = statecode;
            tbUpdateStateNameEn.Text = StateName;
            //  tbUpdateStateAbbName.Text = StateAbbr;
            ddlUpdateFareType.SelectedValue = faretype;
        }

        if (e.CommandName == "SaveState")
        {
            string Faretype = (row.FindControl("ddlFareType") as DropDownList).SelectedValue;
            if (Faretype == "0")
            {
                Errormsg("Fare Type Field cannot be empty");
            }
            else
            {
                bool cbAccidentSurChrggv = (row.FindControl("cbAccidentSurChrg") as CheckBox).Checked;
                bool cbPassengerSurChrggv = (row.FindControl("cbPassengerSurChrg") as CheckBox).Checked;
                bool cbItSurChrggv = (row.FindControl("cbItSurChrg") as CheckBox).Checked;
                bool cbOtherSurChrggv = (row.FindControl("cbOtherSurChrg") as CheckBox).Checked;
                GridViewRow r = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                string statecode = gvStateList.DataKeys[r.RowIndex]["stat_code"].ToString();
                Session["statecode"] = statecode;

                Session["faretype"] = Faretype;

                if (cbAccidentSurChrggv == true)
                    Session["accident_charge"] = "Y";
                else
                    Session["accident_charge"] = "";

                if (cbPassengerSurChrggv == true)
                    Session["Passenger_charge"] = "Y";
                else
                    Session["Passenger_charge"] = "";

                if (cbItSurChrggv == true)
                    Session["it_charge"] = "Y";
                else
                    Session["it_charge"] = "";

                if (cbOtherSurChrggv == true)
                    Session["other_charge"] = "Y";
                else
                    Session["other_charge"] = "";
                Session["Action"] = "S";
                ConfirmMsg("Do you want save State Details?");
            }


        }

        if (e.CommandName == "DeleteState")
        {
            Session["Action"] = "D";

            string statecode = gvStateList.DataKeys[row.RowIndex]["stat_code"].ToString();
            Session["statecode"] = statecode;
            ConfirmMsg("If you Delete this state then all stations,routes and other things will be delete realted to this state.");
        }
        if (e.CommandName == "ActivateState")
        {
            string statecode = gvStateList.DataKeys[row.RowIndex]["stat_code"].ToString();
            Session["Action"] = "Active";
            Session["statecode"] = statecode;
            ConfirmMsg("Do you want to Activate State?");
        }
        if (e.CommandName == "DiscontinueState")
        {
            string statecode = gvStateList.DataKeys[row.RowIndex]["stat_code"].ToString();
            Session["Action"] = "Deactive";
            Session["statecode"] = statecode;
            ConfirmMsg("If you discontinue this state then all stations,routes and other things will be discontinued realted to this state.");
        }
    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        pnlStateAll.Visible = true;
        pnlUpdateState.Visible = false;
        saveState();
    }
    protected void lbtnUpdate_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (Validaion() == false)
        {
            return;
        }
        ConfirmMsg("Do you want to Update State Detail?");
        Session["Action"] = "U";
    }
    protected void lbtnCancel_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        pnlUpdateState.Visible = false;
        pnlStateAll.Visible = true;
    }
    protected void lbtnsearch_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (tbstatesearch.Text == "")
        {

            Errormsg("Please Enter State Name");
        }
        else
        { getSearchStateList(); }

    }
    protected void lbtnReset_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        tbstatesearch.Text = "";
        allStateList();
    }
    protected void lbtnview_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        string msg = "";
        msg = msg + "1.  Here you can add state along with its Fare Type.<br/>";
        msg = msg + "2.  You can also update state details like State Name(Hi), Abbreviation, Fare Type and status.<br/>";
        msg = msg + "3.  Added state can be delete.<br/>";
        InfoMsg(msg);

    }
    protected void gvStateList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblStatus = e.Row.FindControl("LabelStateStatusSaved") as Label;
            Label lblStateId = e.Row.FindControl("LabelStateIdSaved") as Label;
            Label labelStateFareTypeSaved = e.Row.FindControl("LabelStateFareTypeSaved") as Label;
            DropDownList ddlFareType = e.Row.FindControl("ddlFareType") as DropDownList;
            LinkButton btnSave = e.Row.FindControl("lbtnSaveStatee") as LinkButton;
            LinkButton btnUpdate = e.Row.FindControl("lbtnUpdateStatee") as LinkButton;
            LinkButton btnDelete = e.Row.FindControl("lbtndeleteStatee") as LinkButton;
            LinkButton btnActive = e.Row.FindControl("lbtnActivate") as LinkButton;
            LinkButton btnDiscontinue = e.Row.FindControl("lbtnDiscontinue") as LinkButton;

            Label accSC = e.Row.FindControl("ACCIDENT_SURCHARGE") as Label;

            Label psgrSC = e.Row.FindControl("PASSENGER_SURCHARGE") as Label;
            Label itSC = e.Row.FindControl("IT_SURCHARGE") as Label;
            Label othrSC = e.Row.FindControl("OTHER_SURCHARGE") as Label;

            CheckBox cbAcc = e.Row.FindControl("cbAccidentSurChrg") as CheckBox;
            CheckBox cbPsng = e.Row.FindControl("cbPassengerSurChrg") as CheckBox;
            CheckBox cbIt = e.Row.FindControl("cbItSurChrg") as CheckBox;
            CheckBox cbOthr = e.Row.FindControl("cbOtherSurChrg") as CheckBox;
            DataRowView rowView = (DataRowView)e.Row.DataItem;
            if (accSC.Text == "Y")
                cbAcc.Checked = true;
            if (psgrSC.Text == "Y")
                cbPsng.Checked = true;
            if (itSC.Text == "Y")
                cbIt.Checked = true;
            if (othrSC.Text == "Y")
                cbOthr.Checked = true;

            if (lblStateId.Text.Length <= 0)
            {
                e.Row.BackColor = Color.White;
                ddlFareType.Enabled = true;
                btnSave.Visible = true;
                btnActive.Visible = false;
                btnDiscontinue.Visible = false;
                btnUpdate.Visible = false;
                btnDelete.Visible = false;
                cbAcc.Enabled = true;
                cbPsng.Enabled = true;
                cbIt.Enabled = true;
                cbOthr.Enabled = true;

            }
            else
            {
                e.Row.BackColor = Color.FromArgb(221, 230, 222);
                ddlFareType.SelectedValue = labelStateFareTypeSaved.Text;
                ddlFareType.Enabled = false;
                btnSave.Visible = false;
                btnDiscontinue.Visible = false;
                btnUpdate.Visible = true;
                btnDelete.Visible = false;
                cbAcc.Enabled = false;
                btnActive.Visible = false;
                cbPsng.Enabled = false;
                cbIt.Enabled = false;
                cbOthr.Enabled = false;
                if (rowView["is_discontinue"].ToString() == "Y")
                {
                    btnDiscontinue.Visible = true;
                }
                if (rowView["status"].ToString() == "D")
                {
                    btnActive.Visible = true;
                    btnDiscontinue.Visible = false;
                }
               
                if (rowView["is_delete"].ToString() == "Y")
                {
                    btnActive.Visible = false;
                    btnDiscontinue.Visible = false;
                    btnDelete.Visible = true;
                }

            }
        }
    }
    protected void lbtndwnldinst_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        string msg = "";
        msg = msg + "Coming Soon";
        InfoMsg(msg);
    }

   
    #endregion




}