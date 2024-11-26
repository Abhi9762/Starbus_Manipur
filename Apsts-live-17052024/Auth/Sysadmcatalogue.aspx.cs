using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class CitizenAuth_Sysadmcatalogue : BasePage
{
    sbValidation _SecurityCheck = new sbValidation();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            checkAdditionalModules();
            checkForSecurity();
            if (_security.isSessionExist(Session["_RoleCode"]) == true)
            {
                Session["_RoleCode"] = Session["_RoleCode"];
            }
            else
            {
                Response.Redirect("sessionTimeout.aspx");
            }

            if (Session["_RoleCode"].ToString() == "1")
            {
                Session["MasterPageHeaderText"] = "System Admin |";
            }
            else
            {
                Session["MasterPageHeaderText"] = "HQ Employee |";
            }
            Session["_moduleName"] = "Catalogue";
            Session["_OfficeLevel"] = "10";
            Session["_OfficeId"] = "1000000000";
        }
    }
    private void checkAdditionalModules()
    {
        divcsc.Visible = false;
        divagents.Visible = false;
        //divpassPerformance.Visible = false;
        divcsclogo.Visible = false;
        if (sbXMLdata.checkModuleCategory("70") == true)
        {
            divagents.Visible = true;
        }
        if (sbXMLdata.checkModuleCategory("71") == true)
        {
           // divpassPerformance.Visible = true;
        }
        if (sbXMLdata.checkModuleCategory("72") == true)
        {
            divcsc.Visible = true;
            divcsclogo.Visible = true;
        }
    }
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
            Response.Redirect("sessionTimeout.aspx");
        }

        if (_security.checkvalidation() == false)
        {
            Response.Redirect("Errorpage.aspx");
        }
        if (_security.CheckOldUserLogin(Session["_UserCode"].ToString()) == false)
        {
            Response.Redirect("Errorpage.aspx");
        }
        if (Session["_RoleCode"].ToString() == "1")
        {
            if (_security.isSessionExist(Session["_RNDIDENTIFIERSADM"]) == true)
            {
                Session["_RNDIDENTIFIERSADM"] = Session["_RNDIDENTIFIERSADM"];
            }
            else
            {
                Response.Redirect("sessionTimeout.aspx");
            }
        }
        if (Session["_RoleCode"].ToString() == "4")
        {
            if (_security.isSessionExist(Session["_RNDIDENTIFIERCNTR"]) == true)
            {
                Session["_RNDIDENTIFIERCNTR"] = Session["_RNDIDENTIFIERCNTR"];
            }
            else
            {
                Response.Redirect("sessionTimeout.aspx");
            }
        }
        if (Session["_RoleCode"].ToString() == "5")
        {
            if (_security.isSessionExist(Session["_RNDIDENTIFIERDOPT"]) == true)
            {
                Session["_RNDIDENTIFIERDOPT"] = Session["_RNDIDENTIFIERDOPT"];
            }
            else
            {
                Response.Redirect("sessionTimeout.aspx");
            }
        }
    }
    private void Errormsg(string msg)
    {
        string popup = _popup.modalPopupSmall("W", "Information", msg, "Close");
        Response.Write(popup);
    }

    protected void lbtnState_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Auth/SysAdmState.aspx");
    }

    protected void lbtnCity_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Auth/SysAdminCity.aspx");
    }

    protected void lbtnOffice_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Auth/SysAdmOfficeMgmt.aspx");
    }

    protected void lbtnBusLayout_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Auth/SysAdmLayoutMain.aspx");
    }

    protected void lbtnAgencyRegistration_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Auth/SysAdmAgencyRegistration.aspx");
    }

    protected void lbtnEtmRegistartion_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Auth/SysAdmETMRegistration.aspx");
    }

    protected void lbtnEtmMgmt_Click(object sender, EventArgs e)
    {
        Errormsg("Module Not Created");
    }

    protected void lbtnBusRegistration_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Auth/SysAdmBusRegistration.aspx");
    }

    protected void lbtnEmployeeRegistration_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Auth/SysadmEmployeeDetails.aspx");
    }

    protected void lbtnUserMgmt_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Auth/SysAdmUserManagement.aspx");
    }

    protected void lbtnAddAttendance_Click(object sender, EventArgs e)
    {
        Errormsg("Module Not Created");
    }

    protected void lbtnLeaveEntry_Click(object sender, EventArgs e)
    {
        Errormsg("Module Not Created");
    }

    protected void lbtnLeaveApproval_Click(object sender, EventArgs e)
    {
        Errormsg("Module Not Created");
    }

    protected void lbtnShiftAllocation_Click(object sender, EventArgs e)
    {
        Errormsg("Module Not Created");
    }

    protected void lbtnModuleAssignment_Click(object sender, EventArgs e)
    {
        // Errormsg("Module Not Created");
        Response.Redirect("../Auth/SysAdmUserMapping.aspx");
    }

    protected void lbtnBusServiceType_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Auth/Sysadmservicetype.aspx");
    }

    protected void lbtnStateServiceWiseFare_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Auth/SysAdmStateServiceWiseFare.aspx");
    }

    protected void lbtnStateServiceWiseCharge_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Auth/SysAdmStateServiceWiseCharge.aspx");
    }

    protected void lbtnServiceWiseChargeMap_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Auth/SysAdmStateServiceWiseChargeMap.aspx");
    }

    protected void lbtnStateWiseTaxRate_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Auth/SysAdmStateServiceWiseTax.aspx");
    }

    protected void lbtnStation_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Auth/SysAdmStationMgmt.aspx");
    }

    protected void lbtnStationDistance_Click(object sender, EventArgs e)
    {
        Errormsg("Module Not Created");
    }

    protected void lbtnRoute_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Auth/SysAdmRouteMgmt.aspx");
    }

    protected void lbtnDepotService_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Auth/SysAdmDepotService.aspx");
    }

    protected void lbtnFareGeneration_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Auth/SysAdmFareGeneration.aspx");
    }

    protected void lbtnFciGeneration_Click(object sender, EventArgs e)
    {
        Response.Redirect("sysAdmFCIGenaration.aspx");
    }

    protected void lbtnSpecialTktCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("sysAdmSpecialTicketCancellation.aspx");
    }



    protected void lbntBlockSeat_Click(object sender, EventArgs e)
    {
        Response.Redirect("sysadmblockseats.aspx");
    }

    protected void lbntPgMis_Click(object sender, EventArgs e)
    {
        Errormsg("Module Not Created");
    }

    protected void lbtnPnrHelp_Click(object sender, EventArgs e)
    {
        //Errormsg("Module Not Created");
        Response.Redirect("helpdesk.aspx");

    }

    protected void lbtnRating_Click(object sender, EventArgs e)
    {
        Response.Redirect("SysAdmFeedbackRating1.aspx");

        //Errormsg("Module Not Created");
    }

    protected void lbtnComplaint_Click(object sender, EventArgs e)
    {

        Response.Redirect("complaintDashboard.aspx");



        //Errormsg("Module Not Created");
    }

    protected void lbtnTrackBus_Click(object sender, EventArgs e)
    {
        Errormsg("Module Not Created");
    }

    protected void lbtnPgPerformance_Click(object sender, EventArgs e)
    {
        Errormsg("Module Not Created");
    }

    protected void lbtnPassPerformance_Click(object sender, EventArgs e)
    {
        Errormsg("Module Not Created");
    }

    protected void lbtnReport_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Auth/Reports/rpt_m1.aspx");
    }

    protected void lbtntripChart_Click(object sender, EventArgs e)
    {
        Response.Redirect("tripchartdash.aspx");
    }




}

