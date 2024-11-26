using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class Auth_SysAdmLayoutMain : BasePage
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
        Session["_moduleName"] = "Create Bus Layout";
        lblSummary.Text = "Summary As on Date " + DateTime.Now;
        if (!IsPostBack)
        {
            Random randomclass = new Random();
            Session["rndNoCheck"] = randomclass.Next().ToString();
            hidtoken.Value = Session["rndNoCheck"].ToString();
            loadLayouts();
            layoutcount();
        }
    }

    #region "Method"
    private bool validvalue()//M1
    {
        try
        {
            int msgcount = 0;
            string msg = "";

            if (rblType.SelectedValue != "G")
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Select Layout Type.<br/>";
            }
            if (_validation.IsValidString(tbLayoutName.Text, 4, tbLayoutName.MaxLength) == false)
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Please check layout name should not contain any reserved words like 'Select','Delete','Drop' and sprcial characters.<br/>";
            }
            if (_validation.IsValidInteger(tbrows.Text, 1, tbrows.MaxLength) == false)
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Please check number of rows, number of rows should be less than 13.<br/>";
            }
            else
            {
                if (Convert.ToInt16(tbrows.Text.ToString()) > 13)
                {
                    msgcount = msgcount + 1;
                    msg = msg + msgcount.ToString() + ". Please check number of rows, number of rows should be less than 13.<br/>";
                }
            }
            if (_validation.IsValidInteger(tbcolumns.Text, 1, tbcolumns.MaxLength) == false)
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Please check number of columns, number of columns should be less than 7.<br/>";
            }
            else
            {
                if (Convert.ToInt16(tbcolumns.Text.ToString()) > 7)
                {
                    msgcount = msgcount + 1;
                    msg = msg + msgcount.ToString() + ". Please check number of columns, columns of columns should be less than 7.<br/>";
                }
            }
            //if (rblType.SelectedValue != "S")
            //{
            //    if (_validation.IsValidInteger(tbrowsU.Text, 2, tbrowsU.MaxLength) == false)
            //    {
            //        msgcount = msgcount + 1;
            //        msg = msg + msgcount.ToString() + ". Please check number of Upper rows, number of Upper rows should be less than 13.<br/>";
            //    }
            //    else
            //    {
            //        if (Convert.ToInt16(tbrowsU.Text.ToString()) > 13)
            //        {
            //            msgcount = msgcount + 1;
            //            msg = msg + msgcount.ToString() + ". Please check number of Upper rows, number of Upper rows should be less than 13.<br/>";
            //        }
            //    }
            //    if (_validation.IsValidInteger(tbcolumnsU.Text, 2, tbcolumnsU.MaxLength) == false)
            //    {
            //        msgcount = msgcount + 1;
            //        msg = msg + msgcount.ToString() + ". Please check number of Upper columns, number of Upper columns should be less than 7.<br/>";
            //    }
            //    else
            //    {
            //        if (Convert.ToInt16(tbcolumnsU.Text.ToString()) > 7)
            //        {
            //            msgcount = msgcount + 1;
            //            msg = msg + msgcount.ToString() + ". Please check number of Upper columns, columns of Upper columns should be less than 7.<br/>";
            //        }
            //    }
            //}
            if (isDuplcateLayoutName(tbLayoutName.Text) == true)
            {

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
            _common.ErrorLog("SysAdmLayoutMain-M1", ex.Message.ToString());
            return false;
        }
    }
    private bool isDuplcateLayoutName(string layoutname)//M2
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.f_get_Layout");
            MyCommand.Parameters.AddWithValue("p_layout", layoutname);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    return true;
                }
            }
            else
            {
                _common.ErrorLog("SysAdmLayoutMain.aspx-0001", dt.TableName);
            }

            return false;
        }
        catch (Exception ex)
        {
            Errormsg("There is some error" + ex.Message);
            _common.ErrorLog("SysAdmLayoutMain.aspx-0002", ex.Message.ToString());
            return true;
        }
    }
    private void loadLayouts()//M3
    {
        try
        {
            string searchText = tbSearch.Text.Trim();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_bus_layout_get_list");
            MyCommand.Parameters.AddWithValue("p_layoutname", searchText);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    gvLayout.DataSource = dt;
                    gvLayout.DataBind();
                    gvLayout.Visible = true;
                    pnlNoRecord.Visible = false;
                }
                else
                {
                    gvLayout.Visible = false;
                    pnlNoRecord.Visible = true;
                }
            }
            else
            {
                _common.ErrorLog("SysAdmLayoutMain.aspx-0004", dt.TableName);
                gvLayout.Visible = false;
                pnlNoRecord.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Errormsg("There is some error" + ex.Message);
            _common.ErrorLog("SysAdmLayoutMain.aspx-0004", ex.Message.ToString());
        }
    }
    private void resetctrl()
    {
        rblType.SelectedValue = "G";
        rblType.Enabled = false;
        tbLayoutName.Text = "";
        tbrows.Text = "";
        tbcolumns.Text = "";
        pnlUpper.Visible = false;
        tbrowsU.Text = "";
        tbcolumnsU.Text = "";
    }
    private void deletelayout()//M4
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_bus_layout_delete");
            MyCommand.Parameters.AddWithValue("p_layoutcode", Convert.ToInt32(Session["_LayoutCode"].ToString()));
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    Successmsg("Layout Successfully Deleted");
                    loadLayouts();
                }
            }
            else
            {
                _common.ErrorLog("SysAdmLayoutMain.aspx-0005", dt.TableName);
            }
        }
        catch (Exception ex)
        {
            Errormsg("There is some error" + ex.Message);
            _common.ErrorLog("SysAdmLayoutMain.aspx-0006", ex.Message.ToString());
        }
    }
    private void locklayout()//M5
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_bus_layout_lock");
            MyCommand.Parameters.AddWithValue("p_layoutcode", Convert.ToInt32(Session["_LayoutCode"].ToString()));
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    Successmsg("Layout has successfully been locked, Now You can attach this layout with a bus");
                }
            }
            else
            {
                _common.ErrorLog("SysAdmLayoutMain.aspx-0007", dt.TableName);
            }
        }
        catch (Exception ex)
        {
            Errormsg("There is some error" + ex.Message);
            _common.ErrorLog("SysAdmLayoutMain.aspx-0008", ex.Message.ToString());
        }
    }
    private void layoutcount()//M6
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_bus_layout_get_count");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    lblTotalLayout.Text = dt.Rows[0]["tot_layout"].ToString();
                    lblPendingLayout.Text = dt.Rows[0]["pending_layout"].ToString();
                    lblLockLayout.Text = dt.Rows[0]["lock_layout"].ToString();
                }
            }
            else
            {
                _common.ErrorLog("SysAdmLayoutMain.aspx-0009", dt.TableName);
            }
        }
        catch (Exception ex)
        {
            Errormsg("There is some error" + ex.Message);
            _common.ErrorLog("SysAdmLayoutMain.aspx-0010", ex.Message.ToString());
        }
    }
    private void InfoMsg(string msg)
    {
        string popup = _popup.modalPopupSmall("I", "Information", msg, "Close");
        Response.Write(popup);
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
    protected void lbtndwnldinst_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        string msg = "";
        msg = msg + "Coming Soon";
        InfoMsg(msg);
    }
    protected void lbtnSave_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (validvalue() == false)
        {
            return;
        }
        lblConfirmation.Text = "Do you want to create Layout?";
        mpConfirmation.Show();
        Session["_Action"] = "C";
    }
    protected void lbtnReset_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        resetctrl();
    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (Session["_Action"].ToString() == "D")
        {
            deletelayout();
        }
        else if (Session["_Action"].ToString() == "L")
        {
            locklayout();
        }
        else
        {
            Session["_rows"] = Convert.ToInt16(tbrows.Text.ToString());
            Session["_cols"] = Convert.ToInt16(tbcolumns.Text.ToString());
            Session["_layoutName"] = tbLayoutName.Text.ToString().Trim();
            Session["_layoutCategoryName"] = rblType.SelectedItem.Text;
            if (rblType.SelectedValue == "S")
            {
                Session["_rowsU"] = Convert.ToInt16(tbrowsU.Text.ToString());
                Session["_colsU"] = Convert.ToInt16(tbcolumnsU.Text.ToString());
                Session["_layoutCategory"] = "S";
            }
            else
            {
                Session["_rowsU"] = 0;
                Session["_colsU"] = 0;
                Session["_layoutCategory"] = "G";
            }
            Response.Redirect("SysAdmLayoutSeatsYN.aspx");
        }
    }
    protected void gvLayout_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvLayout.PageIndex = e.NewPageIndex;
        loadLayouts();
    }
    protected void gvLayout_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton ClbtnView;
            LinkButton CibtnChangeSeatNo;
            LinkButton CibtnSeatType;
            LinkButton ClbtnLock;
            LinkButton ClbtnDelete;
            Label lblcategory;

            ClbtnView = (LinkButton)e.Row.FindControl("lbtnView");
            ClbtnDelete = (LinkButton)e.Row.FindControl("lbtnDelete");
            CibtnChangeSeatNo = (LinkButton)e.Row.FindControl("lbtnibtnChangeSeatNo");
            CibtnSeatType = (LinkButton)e.Row.FindControl("lbtnibtnSeatType");
            ClbtnLock = (LinkButton)e.Row.FindControl("lbtnLock");

            lblcategory = (Label)e.Row.FindControl("lbllayoutcategory");

            ClbtnLock.Enabled = false;
            string MCurrentstatus, layoutcategory;
            MCurrentstatus = gvLayout.DataKeys[e.Row.RowIndex]["sta"].ToString();
            layoutcategory = gvLayout.DataKeys[e.Row.RowIndex]["layoutcategory"].ToString();
            if (layoutcategory == "G")
            {
                lblcategory.Text = "General";
            }
            else if (layoutcategory == "S")
            {
                lblcategory.Text = "Sleeper";
            }

            ClbtnLock.Text = "<span class=\"fa fa-unlock\"></span>";
            ClbtnLock.BackColor = System.Drawing.Color.Green;
            ClbtnLock.ForeColor = System.Drawing.Color.White;
            ClbtnDelete.BackColor = System.Drawing.Color.Red;
            ClbtnDelete.ForeColor = System.Drawing.Color.White;
            ClbtnView.BackColor = System.Drawing.Color.Yellow;
            if (MCurrentstatus == "D")
            {
                CibtnChangeSeatNo.Enabled = false;
                CibtnSeatType.Enabled = false;
                ClbtnLock.Enabled = false;
            }
            else if (MCurrentstatus == "L")
            {
                ClbtnLock.Text = "<span class=\"fa fa-lock\"></span>";
                CibtnChangeSeatNo.Enabled = false;
                CibtnSeatType.Enabled = false;
                ClbtnLock.Enabled = false;
                ClbtnDelete.Enabled = false;
                ClbtnLock.BackColor = System.Drawing.Color.Gray;
                CibtnChangeSeatNo.BackColor = System.Drawing.Color.Gray;
                CibtnSeatType.BackColor = System.Drawing.Color.Gray;
                ClbtnDelete.BackColor = System.Drawing.Color.Gray;
                ClbtnLock.ForeColor = System.Drawing.Color.Black;
                //  e.Row.BackColor = System.Drawing.Color.Beige;
            }
            else
            {
                CibtnChangeSeatNo.Enabled = true;
                CibtnSeatType.Enabled = true;
                ClbtnLock.Enabled = true;
            }
        }
    }
    protected void gvLayout_RowCommand(object sender, GridViewCommandEventArgs e)//E1
    {
        CsrfTokenValidate();
        try
        {
            if (e.CommandName == "gvLayoutView")
            {
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                int i = row.RowIndex;
                Session["_layoutName"] = gvLayout.DataKeys[i]["layoutname"].ToString();
                Session["_LayoutCode"] = gvLayout.DataKeys[i]["layoutcode"].ToString();
                Session["_rows"] = gvLayout.DataKeys[i]["noof_rows"].ToString();
                Session["_cols"] = gvLayout.DataKeys[i]["noof_column"].ToString();
                Session["_rowsU"] = gvLayout.DataKeys[i]["noof_rowsu"].ToString();
                Session["_colsU"] = gvLayout.DataKeys[i]["noof_columnu"].ToString();
                Session["_layoutCategory"] = gvLayout.DataKeys[i]["layoutcategory"].ToString();
                Session["_layoutCategoryName"] = "GENERAL";
                if (Session["_layoutCategory"].ToString() == "S")
                {
                    Session["_layoutCategoryName"] = "SLEEPER";
                }
                Response.Redirect("SysAdmLayoutSeatView.aspx");
            }
            if (e.CommandName == "gvLayoutDelete")
            {
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                int i = row.RowIndex;
                Session["_LayoutCode"] = gvLayout.DataKeys[i]["layoutcode"].ToString();
                lblConfirmation.Text = "Do you want to Delete Bus Layout ?";
                Session["_Action"] = "D";
                mpConfirmation.Show();
            }
            if (e.CommandName == "gvLayoutChangeSeatNo")
            {
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                int i = row.RowIndex;
                Session["_layoutName"] = gvLayout.DataKeys[i]["layoutname"].ToString();
                Session["_LayoutCode"] = gvLayout.DataKeys[i]["layoutcode"].ToString();
                Session["_rows"] = gvLayout.DataKeys[i]["noof_rows"].ToString();
                Session["_cols"] = gvLayout.DataKeys[i]["noof_column"].ToString();
                Session["_rowsU"] = gvLayout.DataKeys[i]["noof_rowsu"].ToString();
                Session["_colsU"] = gvLayout.DataKeys[i]["noof_columnu"].ToString();
                Session["_layoutCategory"] = gvLayout.DataKeys[i]["layoutcategory"].ToString();
                Session["_layoutCategoryName"] = "GENERAL";
                if (Session["_layoutCategory"].ToString() == "S")
                {
                    Session["_layoutCategoryName"] = "SLEEPER";
                }
                Response.Redirect("SysAdmLayoutSeatNumbering.aspx");
            }
            if (e.CommandName == "gvLayoutChangeSeatType")
            {
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                int i = row.RowIndex;
                Session["_layoutName"] = gvLayout.DataKeys[i]["layoutname"].ToString();
                Session["_LayoutCode"] = gvLayout.DataKeys[i]["layoutcode"].ToString();
                Session["_rows"] = gvLayout.DataKeys[i]["noof_rows"].ToString();
                Session["_cols"] = gvLayout.DataKeys[i]["noof_column"].ToString();
                Session["_rowsU"] = gvLayout.DataKeys[i]["noof_rowsu"].ToString();
                Session["_colsU"] = gvLayout.DataKeys[i]["noof_columnu"].ToString();
                Session["_layoutCategory"] = gvLayout.DataKeys[i]["layoutcategory"].ToString();
                Session["_layoutCategoryName"] = "GENERAL";
                if (Session["_layoutCategory"].ToString() == "S")
                {
                    Session["_layoutCategoryName"] = "SLEEPER";
                }
                Response.Redirect("SysAdmLayoutSeatTypes.aspx");

            }
            if (e.CommandName == "gvLayoutSLock")
            {
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                int i = row.RowIndex;
                Session["_LayoutCode"] = gvLayout.DataKeys[i]["layoutcode"].ToString();
                lblConfirmation.Text = "Do you want to Lock Bus Layout ?";
                Session["_Action"] = "L";
                mpConfirmation.Show();
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("SysAdmLayoutMain-E1", ex.Message.ToString());
        }
    }
    protected void lbtnview_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        string msg = "";
        msg = msg + "1. Give Row & Column .<br/>";
        msg = msg + "2. Select/Deselect Seats as per .<br/>";
        msg = msg + "3. Change Seat Number .<br/>";
        msg = msg + "4. Decide Seat Type .<br/>";
        InfoMsg(msg);
    }
    protected void lbtnSearchLayout_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        loadLayouts();
    }
    protected void lbtnResetSearchLayout_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        tbSearch.Text = "";
        loadLayouts();
    }
    #endregion





}