using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using ClosedXML.Excel;

public partial class Auth_ModuleAssignment : System.Web.UI.Page
{
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    sbValidation _validation = new sbValidation();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    ReportDocument cryrpt = new ReportDocument();
    string ModuleAssignmetrpt = System.Web.HttpContext.Current.Server.MapPath("~/Auth/Reports/ModuleAssignment.rpt");
    protected void Page_Load(object sender, EventArgs e)
    {
        checkForSecurity();
        if (!IsPostBack)
        {
            
            Session["_moduleName"] = "Module Assignment";
            lblSummary.Text = "Summary as on Date " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");
            lblempofc.Text = "Select employee and click on map button for module assignment/removal";
            LoadEmployee(Session["uofclvlid"].ToString(), Session["_LDepotCode"].ToString(), "0");
            loadCountsmry(Session["uofclvlid"].ToString(), Session["_LDepotCode"].ToString());
        }
    }


    #region "Method"

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
            Response.Redirect("../sessionTimeout.aspx");
        }
        //if (_security.isSessionExist(Session["_RNDIDENTIFIERDOPT"]) == true)
        //{
        //    Session["_RNDIDENTIFIERDOPT"] = Session["_RNDIDENTIFIERDOPT"];
        //}
        //else
        //{
        //    Response.Redirect("../sessionTimeout.aspx");
        //}
        //if (_security.checkvalidation() == false)
        //{
        //    Response.Redirect("../Errorpage.aspx");
        //}
        //if (_security.CheckOldUserLogin(Session["_UserCode"].ToString()) == false)
        //{
        //    Response.Redirect("../Errorpage.aspx");
        //}
    }
    private void loadCountsmry(String OfcLv, String Ofc)
    {
        try
        {
          
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_getcountsummary");

            MyCommand.Parameters.AddWithValue("p_ofclvlid", Convert.ToInt32(OfcLv));
            MyCommand.Parameters.AddWithValue("p_ofcid", Ofc);

            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {

                    lbltotemp.Text = dt.Rows[0]["p_totalemp"].ToString();
                    lbltotmodule.Text = dt.Rows[0]["p_totalmodule"].ToString();
                    lblassignmodule.Text = dt.Rows[0]["p_assignedmodule"].ToString();


                }

            }
            else
            {

                _common.ErrorLog("SysadmEmployeeDetails-M12", dt.TableName);
            }

        }
        catch (Exception ex)
        {
            _common.ErrorLog("SysadmEmployeeDetails-M12", ex.Message.ToString());
        }
    }
    private void LoadEmployee(String OfcLv, String Ofc, String Postingofc)
    {
        try
        {
            lblempofc.Visible = true;
            lbtnback.Visible = false;
            grdemployee.Visible = false;
            dvnoemp.Visible = true;
            pnldetails.Visible = false;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_employeelist");

            MyCommand.Parameters.AddWithValue("p_ofclvlid", Convert.ToInt32(OfcLv));
            MyCommand.Parameters.AddWithValue("p_ofcid", Ofc);
            MyCommand.Parameters.AddWithValue("p_postingofc", Postingofc);

            dt = bll.SelectAll(MyCommand);

            if (dt.TableName == "Success")
            {


                if (dt.Rows.Count > 0)
                {

                    grdemployee.Visible = true;
                    grdemployee.DataSource = dt;
                    grdemployee.DataBind();
                    grdemployee.UseAccessibleHeader = true;
                    grdemployee.HeaderRow.TableSection = TableRowSection.TableHeader;
                    dvnoemp.Visible = false;


                }

            }
            else
            {

                _common.ErrorLog("SysadmEmployeeDetails-M12", dt.TableName);
            }

        }
        catch (Exception ex)
        {
            _common.ErrorLog("SysadmEmployeeDetails-M12", ex.Message.ToString());
        }
    }
    private void validuser(string empcode, string _ofcID, string _ofcLevelID)
    {
        try
        {
            DataTable dt = new DataTable();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_emp");
	   MyCommand.Parameters.AddWithValue("p_ofclvlid", Convert.ToInt32(_ofcLevelID));
            MyCommand.Parameters.AddWithValue("p_ofcid", _ofcID);
            MyCommand.Parameters.AddWithValue("p_designation", Convert.ToInt32("0"));
            MyCommand.Parameters.AddWithValue("p_lincensestatus", "0");
            MyCommand.Parameters.AddWithValue("p_employeename", empcode);

            dt = bll.SelectAll(MyCommand);
//errorMassage(dt.TableName);
               //return;
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    grdemployee.Visible = false;
                    lblempname.Text = dt.Rows[0]["e_fname"] + "(" + dt.Rows[0]["e_empcode"] + ")";
                    lblempdob.Text = dt.Rows[0]["e_dob"].ToString();
                    lblempgender.Text = dt.Rows[0]["e_gender"].ToString();
                    lblempoffice.Text = dt.Rows[0]["e_office_name"].ToString();
                    lblemppostingofc.Text = dt.Rows[0]["e_posting_ofc"].ToString();
                    lblempdesignation.Text = dt.Rows[0]["e_designation_name"].ToString();
                    lblempemailid.Text = dt.Rows[0]["e_email_id"].ToString();
                    lblempmobile.Text = dt.Rows[0]["e_mobile_number"].ToString();
                    lblemptype.Text = dt.Rows[0]["e_emp_type"].ToString();
                    Session["_EMPCODE"] = empcode;
                    pnldetails.Visible = true;
                    lblempofc.Visible = false;
                    lbtnback.Visible = true;
                    grdemployee.Visible = false;
                    getmodule(_ofcLevelID, empcode);
                    getAssignedmodule(_ofcLevelID, empcode);

                }
                else
                {
                    pnldetails.Visible = false;
                    grdemployee.Visible = false;

                }
            }
            else
            {
                pnldetails.Visible = false;
                grdemployee.Visible = false;

            }
        }
        catch (Exception ex)
        {

        }
    }
    private void getAssignedmodule(string _ofcLevelID, string _Userid)
    {
        try
        {
            grdassignedmodule.Visible = false;
            pnlassignedmoduleNoRecord.Visible = true;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_assign_module");
            MyCommand.Parameters.AddWithValue("p_ofclvlid", _ofcLevelID);
            MyCommand.Parameters.AddWithValue("p_userid", _Userid);

            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
                if (dt.Rows.Count > 0)
                {

                    grdassignedmodule.DataSource = dt;
                    grdassignedmodule.DataBind();
                    grdassignedmodule.Visible = true;
                    pnlassignedmoduleNoRecord.Visible = false;
                }
        }
        catch (Exception ex)
        {

        }
    }
    private void getmodule(string _ofcLevelID, string _Userid)
    {
        try
        {


            grdavailableforassign.Visible = false;
            pnlavailableforassignNoRecord.Visible = true;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_module");
            MyCommand.Parameters.AddWithValue("p_ofclvlid", _ofcLevelID);
            MyCommand.Parameters.AddWithValue("p_userid", _Userid);

            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
                if (dt.Rows.Count > 0)
                {
                    grdavailableforassign.DataSource = dt;
                    grdavailableforassign.DataBind();
                    grdavailableforassign.Visible = true;
                    pnlavailableforassignNoRecord.Visible = false;
                }
        }
        catch (Exception ex)
        {
            //return false;
        }
    }
    private string checkmoduleassign(string moduleid)
    {

        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.f_get_user_assignmodule");
        MyCommand.Parameters.AddWithValue("p_moduleid", Convert.ToInt32(moduleid));
        MyCommand.Parameters.AddWithValue("p_userid", Session["_EMPCODE"]);

        dt = bll.SelectAll(MyCommand);

        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["user_id"] == DBNull.Value)
            {
                return "Module not assigned to any other user <br/>";
            }
            else
            {
                return dt.Rows[0]["val_assign"] + "<br/>" + dt.Rows[0]["user_id"].ToString();
            }
        }
        else
        {
            return "Module not assigned to any other user <br/>";
        }
    }
    public void ExportGridToExcel()
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_module");
        MyCommand.Parameters.AddWithValue("p_ofclvlid", Session["uofclvlid"]);
        MyCommand.Parameters.AddWithValue("p_userid", "0");

        dt = bll.SelectAll(MyCommand);
        if (dt.TableName == "Success")
            if (dt.Rows.Count > 0)

            {

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt, "Module");

                    HttpResponse Response = HttpContext.Current.Response;
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=ModuleList.xlsx");
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }
            }
            else
            {
                errorMassage("Module Not Available For Assignment");
                return;
            }

        else
        {

            errorMassage("Module Not Available For Assignment");
            return;
        }
    }
    private void LoadModuleAssignmentReport()
    {
        try
        {

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_rpt_assign_module");
            MyCommand.Parameters.AddWithValue("p_ofclvlid", Convert.ToInt32(Session["uofclvlid"]));
            MyCommand.Parameters.AddWithValue("p_ofcid", Session["_LDepotCode"]);

            dt = bll.SelectAll(MyCommand);

            if (dt.Rows.Count > 0)
            {
                cryrpt.Load(ModuleAssignmetrpt);
                cryrpt.SetDataSource(dt);
                XmlDocument doc = new XmlDocument();
                doc.Load(Server.MapPath("../CommonData.xml"));
                XmlNodeList title = doc.GetElementsByTagName("dept_Name_en");

                CrystalDecisions.CrystalReports.Engine.TextObject objtxtorgname = cryrpt.ReportDefinition.Sections[0].ReportObjects["txtorgname"] as CrystalDecisions.CrystalReports.Engine.TextObject;
                objtxtorgname.Text = title.Item(0).InnerXml;
                CrystalDecisions.CrystalReports.Engine.TextObject objtxtRPTNUMBER = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txtreportnumber"];
                objtxtRPTNUMBER.Text = "1";
                CrystalDecisions.CrystalReports.Engine.TextObject objtxtRPTNAME = (TextObject)cryrpt.ReportDefinition.Sections[0].ReportObjects["txtreportname"];
                objtxtRPTNAME.Text = "Module Assignment";
                CrystalDecisions.CrystalReports.Engine.TextObject txtCenterReportName = cryrpt.ReportDefinition.Sections[0].ReportObjects["txtCenterReportName"] as CrystalDecisions.CrystalReports.Engine.TextObject;
                txtCenterReportName.Text = "Module Assigned  List";

                CrystalDecisions.CrystalReports.Engine.TextObject objtxtRPTURL = cryrpt.ReportDefinition.Sections[1].ReportObjects["txturl"] as CrystalDecisions.CrystalReports.Engine.TextObject;
                objtxtRPTURL.Text = "Downloaded From -" + Request.Url.AbsoluteUri;

                cryrpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Context.Response, true, "Module_Assignment_List-" + DateTime.Now.ToString("yyyyMMdd_HHmmss"));
            }
            else
            {
                errorMassage("Module Not Assigned any Employee");
            }


        }
        catch (Exception ex)
        {
            errorMassage("Module Not Assigned any Employee" + ex.Message);
        }
    }
    private void errorMassage(string msg)
    {
        lblerrmsg.Text = msg;
        mpError.Show();
    }
    #endregion

    #region "Events"
    protected void grdemployee_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "View")
        {
            Session["_ofcLevelID"] = grdemployee.DataKeys[index].Values["e_ofclvlid"];
            Session["_ofcID"] = grdemployee.DataKeys[index].Values["e_officeid"];
            validuser(grdemployee.DataKeys[index].Values["emp_code"].ToString(), Session["_ofcID"].ToString(), Session["_ofcLevelID"].ToString());
        }

    }
    protected void grdavailableforassign_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "Assign")
        {
            Session["MODULEID"] = grdavailableforassign.DataKeys[index].Values["val_moduleid"];
            string user = checkmoduleassign(Session["MODULEID"].ToString());

            lblConfirmation.Text = user + "<br/><span style='color:red;'> Do you want to Assign module ? </sapn>";
            Session["ModuleAction"] = "A";

            mpConfirmation.Show();
        }

    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        try
        {

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            string IPAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(IPAddress))
            {
                IPAddress = Request.ServerVariables["REMOTE_ADDR"];
            }

            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_module_mapping");
            MyCommand.Parameters.AddWithValue("p_moduleaction", Session["ModuleAction"]);
            MyCommand.Parameters.AddWithValue("p_groupid", 0);
            MyCommand.Parameters.AddWithValue("p_moduleid", Convert.ToInt32(Session["MODULEID"]));
            MyCommand.Parameters.AddWithValue("p_userid", Session["_EMPCODE"]);
            MyCommand.Parameters.AddWithValue("p_assignby", Session["_UserCode"]);
            MyCommand.Parameters.AddWithValue("p_ipaddress", IPAddress);

            dt = bll.SelectAll(MyCommand);

            if (dt.Rows.Count > 0)
                if (Session["ModuleAction"].ToString() == "A")
                {
                    lblsuccessmsg.Text = "module assignment Sucessfully ";
                }
            if (Session["ModuleAction"].ToString() == "R")
            {
                lblsuccessmsg.Text = "module Remove Sucessfully ";
            }

            mpsuccess.Show();
            getmodule(Session["_ofcLevelID"].ToString(), Session["_EMPCODE"].ToString());
            getAssignedmodule(Session["_ofcLevelID"].ToString(), Session["_EMPCODE"].ToString());
            loadCountsmry(Session["uofclvlid"].ToString(), Session["_LDepotCode"].ToString());

        }
        catch (Exception ex)
        {
            // Handle the exception here
        }

    }
    protected void grdassignedmodule_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        int index = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "Remove")
        {
            Session["MODULEID"] = grdassignedmodule.DataKeys[index].Values["val_moduleid"];
            string user = checkmoduleassign(Session["MODULEID"].ToString());

            lblConfirmation.Text = "Do you want Remove Assigned Module ?";
            Session["ModuleAction"] = "R";

            mpConfirmation.Show();
        }
    }
    protected void lbtnback_Click(object sender, EventArgs e)
    {
        LoadEmployee(Session["uofclvlid"].ToString(), Session["_LDepotCode"].ToString(), "0");
    }
    protected void lbtnNoConfirmation_Click(object sender, EventArgs e)
    {
        mpConfirmation.Hide();
    }
    protected void lbtnmoduleassignment_Click(object sender, EventArgs e)
    {
        LoadModuleAssignmentReport();
    }
    protected void lbtnmodulelist_Click(object sender, EventArgs e)
    {
        ExportGridToExcel();
    }
    #endregion

}
