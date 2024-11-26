using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DLDDefault : System.Web.UI.Page
{
    sbSecurity _security = new sbSecurity();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();

    protected void Page_Load(object sender, EventArgs e)//M1
    {
        loadDeatils();
        checkPortalBlock();
        if (IsPostBack == false)
        {
            string usrtype = Session["_UserType"].ToString();
            int usrrole = Convert.ToInt16(Session["_RoleCode"].ToString());
            switch (usrtype)
            {
                case "U":
                    {
                        switch (usrrole)
                        {
                            case 100: //HQ Employee
                                {
                                    Session["_RNDIDENTIFIERSADM"] = _security.GeneratePassword(10, 5);
                                    Response.Redirect("SysAdminCatelogue.aspx");
                                    break;
                                }
                            case 98: //Project Admin
                                {
                                    Session["_RNDIDENTIFIERPROADM"] = _security.GeneratePassword(10, 5);
                                    Response.Redirect("ProjectAdmCatalogue.aspx");
                                    break;
                                }
                            case 0: //Poratl Admin
                                {
                                    Session["_RNDIDENTIFIERPADM"] = _security.GeneratePassword(10, 5);
                                    Response.Redirect("PAdminCatalogue.aspx");
                                    break;
                                }
                            case 1: //System Admin
                                {
                                    Session["_RNDIDENTIFIERSADM"] = _security.GeneratePassword(10, 5);
                                    Response.Redirect("SysAdmCatalogue.aspx");
                                    break;
                                }
                            case 4: //Counter User
                                {

                                    Session["_RNDIDENTIFIERCNTR"] = _security.GeneratePassword(10, 5);
                                    Response.Redirect("CntrDashboard.aspx");
                                    break;
                                }
                            case 5: //Depot Manager
                                {

                                    Session["_RNDIDENTIFIERDOPT"] = _security.GeneratePassword(10, 5);
                                    Response.Redirect("DepotDashboard.aspx");
                                    break;
                                }
                            case 7: //Cashier
                                {
                                    loadCashierdetails(Session["_UserCode"].ToString());
                                    Session["_RNDIDENTIFIERCASHIER"] = _security.GeneratePassword(10, 5);
                                    Response.Redirect("CashierDashboard.aspx");
                                    break;
                                }
                            case 9: //Time Keeper
                                {

                                    Session["_RNDIDENTIFIERTK"] = _security.GeneratePassword(10, 5);
                                    Response.Redirect("TimeKeeperCatalogue.aspx");
                                    break;
                                }
                            case 12: //ETM Branch
                                {

                                    Session["_RNDIDENTIFIEREB"] = _security.GeneratePassword(10, 5);
                                    Response.Redirect("ETMBranchCatalogue.aspx");
                                    break;
                                }
                            case 13: //Store
                                {
                                    loadStoredetails();
                                    Session["_RNDIDENTIFIERSTORE"] = _security.GeneratePassword(10, 5);
                                    Response.Redirect("StoreCatalogue.aspx");
                                    break;
                                }
                            case 11: //Diesel
                                {
                                    loadDieselDetails();
                                    Session["_RNDIDENTIFIERDIESEL"] = _security.GeneratePassword(10, 5);
                                    Response.Redirect("DieselFillEntry.aspx");

                                    break;
                                }
                            case 8: //St. Incharge
                                {

                                    Session["_RNDIDENTIFIERSTINCHARGE"] = _security.GeneratePassword(10, 5);
                                    Response.Redirect("StInchargeCatalogue.aspx");
                                    break;
                                }
                            case 10: //Foreman
                                {
                                    GetForemanLoggedUsrDetails();
                                    Session["_RNDIDENTIFIERFOREMAN"] = _security.GeneratePassword(10, 5);
                                    Response.Redirect("Bus_Inspection.aspx");
                                    break;
                                }
                            case 14: //PassAdmin
                                {

                                    Session["_RNDIDENTIFIERPSADM"] = _security.GeneratePassword(10, 5);
                                    Response.Redirect("PassADMDashboard.aspx");
                                    break;
                                }

                            case 6: //Helpdesk
                                {

                                    Session["_RNDIDENTIFIERHELPDESK"] = _security.GeneratePassword(10, 5);
                                    Response.Redirect("helpdesk.aspx");
                                    break;
                                }
                            case 15: //Control Room
                                {

                                    Session["_RNDIDENTIFIERCONTROLROOM"] = _security.GeneratePassword(10, 5);
                                    Response.Redirect("ControlRoomDash.aspx");
                                    break;
                                }
                            case 99: //Employee Dash
                            case 2: //Driver
                            case 3: //Conductor
                                {
                                    Session["_RNDIDENTIFIEREMPDASH"] = _security.GeneratePassword(10, 5);
                                    Response.Redirect("EmployeeDash.aspx");
                                    break;

                                }
                            
                            case 105: //PG
                                {
                                    Session["_RNDIDENTIFIERSPG"] = _security.GeneratePassword(10, 5);
                                    Response.Redirect("PgRefundTransaction.aspx");
                                    break;
                                }

 			case 500: //Dhaba Login
                                {
                                    Session["_RNDIDENTIFIERSPG"] = _security.GeneratePassword(10, 5);
                                    Response.Redirect("dhabhaDashboard.aspx");
                                    break;
                                }
                            case 19: //Agent
                                {
                                    if (agentvalidation() == 0)
                                    {
                                        if (Session["_Agtype"].ToString() == "1")
                                        {
                                            Session["_RNDIDENTIFIERAGENTAUTH"] = _security.GeneratePassword(10, 5);
                                            Response.Redirect("AgentDashboard.aspx", true);
                                        }
                                        else if (Session["_Agtype"].ToString() == "3" || Session["_Agtype"].ToString() == "4")
                                        {
                                            if (Session["_CSCMainAg"].ToString() != "0")
                                            {
                                                Session["_authUModName"] = "~ CSC  ~";
                                                Session["_RNDIDENTIFIERAGENTAUTH"] = _security.GeneratePassword(10, 5);
                                                Response.Redirect("subCscDash.aspx");
                                            }
                                            else
                                            {
                                                Session["_authUModName"] = "~ CSC  ~";
                                                Session["_RNDIDENTIFIERAGENTAUTH"] = _security.GeneratePassword(10, 5);
                                                Response.Redirect("SubCscMgmt.aspx");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Session["_RNDIDENTIFIERAGENTAUTH"] = _security.GeneratePassword(10, 5);
                                        Response.Redirect("AgentValidityExpire.aspx", true);

                                    }
                                    break;
                                }
                            default:
                                //Error Paege
                                break;
                        }
                        break;
                    }
                default:
                    break;

            }
        }
    }





    #region "Methods"
    private void checkPortalBlock()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_check_portal_block");
            MyCommand.Parameters.AddWithValue("proleid", Convert.ToInt32(Session["_RoleCode"].ToString()));
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["status"].ToString() == "BLOCK")
                {
                    Response.Redirect("../portalblock.aspx");
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    private int agentvalidation()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "agent.getagentvalid");
            MyCommand.Parameters.AddWithValue("p_agentcode", Session["_UserCode"].ToString());
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                Session["_ValidTo"] = dt.Rows[0]["valid_to"];
                Session["_Agtype"] = dt.Rows[0]["agent_type"];
                Session["_CSCMainAg"] = dt.Rows[0]["csp_agent"];
                Session["_Username"] = dt.Rows[0]["agent_name"];
                return Convert.ToInt16(dt.Rows[0]["Sp_cnt"].ToString());
            }
            else
            {
                return 0;
            }
        }
        catch (Exception ex)
        {
            return 0;
        }
    }
    private void loadDieselDetails()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_dieseldetails");
            MyCommand.Parameters.AddWithValue("p_usercode", Session["_UserCode"].ToString());
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                Session["OfficeName"] = dt.Rows[0]["office_names"].ToString();

                Session["_LDepotCode"] = dt.Rows[0]["depotid"].ToString();

                Session["InchargeEmp"] = dt.Rows[0]["employeename"].ToString();
                Session["Ofcid"] = dt.Rows[0]["officeid_"].ToString();
            }
            else
            {
            }
        }
        catch (Exception ex)
        {
            Session["_ErrorMsg"] = "Something went wrong.";
            Response.Redirect("errorpage.aspx");
        }
    }
    private void GetForemanLoggedUsrDetails()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_foremandetails");
            MyCommand.Parameters.AddWithValue("p_usercode", Session["_UserCode"].ToString());
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                Session["OfficeName"] = dt.Rows[0]["officename"].ToString();
                Session["Ofcid"] = dt.Rows[0]["officeid"].ToString();
                Session["_LDepotCode"] = dt.Rows[0]["depotcode"].ToString();
                Session["_WORKSHOPID"] = dt.Rows[0]["workshopid"].ToString();
                Session["InchargeEmp"] = dt.Rows[0]["empname"].ToString();
                Session["Ofcid"] = dt.Rows[0]["officeid"].ToString();
            }
            else
            {
            }
        }
        catch (Exception ex)
        {
            Session["_ErrorMsg"] = "Something went wrong.";
            Response.Redirect("errorpage.aspx");
        }
    }
    private void GetStoreLoggedUsrDetails()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_storedetails");
            MyCommand.Parameters.AddWithValue("p_usercode", Session["_UserCode"].ToString());
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                Session["StoreName"] = dt.Rows[0]["storename"].ToString();
                Session["_LDepotCode"] = dt.Rows[0]["officeid"].ToString();
                Session["_storeid"] = dt.Rows[0]["storeid"].ToString();
                Session["StoreEmp"] = dt.Rows[0]["nameprefix"].ToString() + " " + dt.Rows[0]["empname"].ToString();
                Session["StoreOfcid"] = dt.Rows[0]["officeid"].ToString();
            }
            else
            {
            }
        }
        catch (Exception ex)
        {
            Session["_ErrorMsg"] = "Something went wrong.";
            Response.Redirect("errorpage.aspx");
        }
    }
    private void loadCashierdetails(string usercode)//M2
    {
        try
        {
            int role = Convert.ToInt16(Session["_RoleCode"].ToString());
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_load_chest_detail");
            MyCommand.Parameters.AddWithValue("p_userid", usercode);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    Session["_ChestName"] = dt.Rows[0]["chest_name"].ToString();
                    Session["_ChestOfcid"] = dt.Rows[0]["office_id"].ToString();
                    Session["_ChestEmp"] = dt.Rows[0]["emp_name"].ToString();
                    Session["_ChestID"] = dt.Rows[0]["chest_code"].ToString();
                    Session["emp_name"] = dt.Rows[0]["name_pf"].ToString() + " " + dt.Rows[0]["emp_name"].ToString();
                    Session["depot_name"] = dt.Rows[0]["chest_name"].ToString();
                    Session["_LDepotCode"] = dt.Rows[0]["depot_code"].ToString();
                    
                }
                else
                {

                    _common.ErrorLog("DieselMaster-M1", "No record Found!!");
                    Response.Redirect("../Errorpage.aspx");
                }
            }
            else
            {
                _common.ErrorLog("DieselMaster-M2", dt.TableName.ToString());
                Response.Redirect("../Errorpage.aspx");
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("DieselFill-M1", ex.Message.ToString());
            Response.Redirect("Errorpage.aspx");
        }
    }
    private void loadDeatils()//M4
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_UserDetails");
            MyCommand.Parameters.AddWithValue("p_userid", Session["_UserCode"].ToString());
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                Session["_RoleCode"] = dt.Rows[0]["urole"];
                Session["_UserType"] = dt.Rows[0]["utype"];
                Session["_OfficeId"] = dt.Rows[0]["uofficeid"];
                Session["_OfficeLevel"] = dt.Rows[0]["uofclvlid"];
                Session["_UserName"] = dt.Rows[0]["uname"];
                Session["_UserDesignation"] = dt.Rows[0]["udesignationcode"];
                Session["_UserDesignationName"] = dt.Rows[0]["udesignationname"];

                if (Session["_RoleCode"].ToString() == "4")
                {
                    Session["_UserCntrID"] = dt.Rows[0]["cntr_office_id"];
                }

            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("Login-M4", ex.Message.ToString());
        }
    }
    private void loadStoredetails()
    {
        try
        {
            Int32 role = Convert.ToInt32(Session["_RoleCode"].ToString()); ;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_loggedstoredetails");
            MyCommand.Parameters.AddWithValue("p_userid", Session["_UserCode"].ToString());
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    Session["StoreEmp"] = dt.Rows[0]["empname"].ToString();
                    Session["StoreName"] = dt.Rows[0]["store_name"].ToString();
                    Session["StoreOfficeid"] = dt.Rows[0]["ofc_id"].ToString();
                    Session["StoreId"] = dt.Rows[0]["storeid"].ToString();
                    Session["StoreOfficeLvl"] = dt.Rows[0]["ofc_lvlid"].ToString();
                }
                else
                {
                    Response.Redirect("../Errorpage.aspx");
                }
            }
            else
            {
                Response.Redirect("../Errorpage.aspx");
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("../Errorpage.aspx");
        }
    }
    #endregion

}