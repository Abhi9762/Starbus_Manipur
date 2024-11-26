using AjaxControlToolkit;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;

public partial class ApplicantDetails_configurationModule : System.Web.UI.Page
{
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    private NpgsqlCommand MyCommand = new NpgsqlCommand();
    DataTable MyTable = new DataTable();
    sbBLL bll = new sbBLL();
    sbCommonFunc clscommon = new sbCommonFunc();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getModuleCategory();

        }
    }
   
    #region"Methods"
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
    private void ConfirmMsg(string msg, string id, string newStatus)
    {
        lblConfirmation.Text = msg;
        lbtnYesConfirmation.CommandArgument = id + ";" + newStatus;
        mpConfirmation.Show();
    }
    protected void loadModuleServiceHistory()
    {
        try
        {
            using (NpgsqlCommand MyCommand = new NpgsqlCommand())
            {
                MyCommand.Parameters.Clear();
                MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.fn_get_module_configuration_log");
                MyTable = bll.SelectAll(MyCommand);

                if (MyTable.TableName == "Success")
                {
                    if (MyTable.Rows.Count > 0)
                    {
                        GridView2.DataSource = MyTable;
                        GridView2.DataBind();
                        ModalPopupExtender1.Show();
                        pnlModuleConfigNoRecord.Visible = false;
                    }
                    else
                    {
                        ModalPopupExtender1.Show();
                        pnlModuleConfigNoRecord.Visible = true;
                        GridView2.Visible = true;
                    }
                }
                else
                {
                    ModalPopupExtender1.Show();
                    pnlModuleConfigNoRecord.Visible = true;
                    GridView2.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message);
        }
    }
    private void UpdateXml(string categoryName, string id, string newStatus)
    {
        string xmlFilePath = Server.MapPath("~/CommonData.xml");
        XmlDocument xmlDoc = new XmlDocument();
        if (System.IO.File.Exists(xmlFilePath))
        {
            xmlDoc.Load(xmlFilePath);
        }
        else
        {
            xmlDoc.AppendChild(xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null));
            XmlNode dataNode = xmlDoc.CreateElement("Data");
            xmlDoc.AppendChild(dataNode);
        }

        XmlNode pageNode = xmlDoc.SelectSingleNode("/Data/Page");
        XmlNode configNode = pageNode.SelectSingleNode("Additional_modules");
        if (configNode == null)
        {
            configNode = xmlDoc.CreateElement("Additional_modules");
            pageNode.AppendChild(configNode);
        }

        if (newStatus == "N")
        {
            //RemoveXmlElement(xmlDoc, configNode, categoryName.ToUpper());
            DeleteXmlNode(id);
        }
        else if (newStatus == "Y")
        {
            UpdateOrAddXmlElement(xmlDoc, configNode, "Module Id", id);
            xmlDoc.Save(xmlFilePath);
        }

       
    }
    private void UpdateOrAddXmlElement(XmlDocument xmlDoc, XmlNode parentNode, string elementName, string elementValue)
    {
        elementName = elementName.Replace(" ", "_");

     
                XmlNode newNode = xmlDoc.CreateElement(elementName);
                newNode.InnerText = elementValue;
                parentNode.AppendChild(newNode);
          
        
    }
    
    private void DeleteXmlNode(string categoryid)
    {
        string xmlFilePath = Server.MapPath("~/CommonData.xml");

        XDocument doc = XDocument.Load(xmlFilePath);

        // Find the parent node containing the nodes to be removed
        XElement parent = doc.Descendants("Additional_modules").FirstOrDefault();

        if (parent != null)
        {
            // Find the nodes with the specified attribute value
            var nodesToRemove = parent.Elements("Module_Id")
                                      .Where(node => (int)node == Convert.ToInt32( categoryid)) // Change 72 to the desired attribute value
                                      .ToList();

            // Remove the found nodes
            foreach (var node in nodesToRemove)
            {
                node.Remove();
            }

            // Save the changes to the XML file
            doc.Save(xmlFilePath);
        }
        
    }
    protected void getModuleCategory()
    {
        try
        {
            using (NpgsqlCommand MyCommand = new NpgsqlCommand())
            {
                MyCommand.Parameters.Clear();
                MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.fn_getmodule_category");
                MyTable = bll.SelectAll(MyCommand);

                if (MyTable.Rows.Count > 0)
                {
                    GridView1.DataSource = MyTable;
                    GridView1.DataBind();
                }
            }

        }
        catch (Exception ex)
        {
            Errormsg(ex.Message);
        }
    }
    #endregion

    #region"Events"
    protected void gvmoduleCategory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        getModuleCategory();

    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ChangeStatus")
        {
            string[] arguments = e.CommandArgument.ToString().Split(';');
            Session["id"] = arguments[0];
            Session["categoryName"] = arguments[1];
            Session["newStatus"] = (((Button)e.CommandSource).Text == "Stop") ? "N" : "Y";

            if (Session["newStatus"].ToString() == "Y")
            {
                ConfirmMsg("Do you want to Activate the service module for category " + Session["categoryName"].ToString() + "?", Session["id"].ToString(), Session["newStatus"].ToString());
            }
            else
            {
                ConfirmMsg("Do you want to Deactivate the service module for category " + Session["categoryName"].ToString() + "?", Session["id"].ToString(), Session["newStatus"].ToString());
            }

        }
    }
    protected void ChangeStatus_Click(object sender, EventArgs e)
    {

        LinkButton lbtnYes = (LinkButton)sender;
        string[] arguments = lbtnYes.CommandArgument.ToString().Split(';');
        string id = arguments[0];
        string newStatus = arguments[1];
        using (NpgsqlCommand MyCommand = new NpgsqlCommand())
        {

            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.fn_update_module_category");
            MyCommand.Parameters.AddWithValue("@p_id", int.Parse(id));
            MyCommand.Parameters.AddWithValue("@p_active_yn", newStatus);
            DataTable dt = bll.SelectAll(MyCommand);

            if (dt.Rows.Count > 0 && dt.ToString() == "Success")
            {
                UpdateXml(Session["categoryName"].ToString(), Session["id"].ToString(), Session["newStatus"].ToString());
                Successmsg("Service Module has been Successfully Updated.");
            }
            else
            {
                Errormsg("Error occurred while updating the service module.");
            }
            getModuleCategory();
        }
    }
    protected void btnLogHistry_Click(object sender, EventArgs e)
    {
        try
        {
            using (NpgsqlCommand MyCommand = new NpgsqlCommand())
            {
                MyCommand.Parameters.Clear();
                MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.fn_get_module_configuration_log");
                MyTable = bll.SelectAll(MyCommand);

                if (MyTable.TableName == "Success")
                {
                    if (MyTable.Rows.Count > 0)
                    {
                        GridView2.DataSource = MyTable;
                        GridView2.DataBind();
                        ModalPopupExtender1.Show();
                        pnlModuleConfigNoRecord.Visible = false;
                    }
                    else
                    {
                        ModalPopupExtender1.Show();
                        pnlModuleConfigNoRecord.Visible = true;
                        GridView2.Visible = true;
                    }
                }
                else
                {
                    ModalPopupExtender1.Show();
                    pnlModuleConfigNoRecord.Visible = true;
                    GridView2.Visible = false;
                }

            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message);
        }
    }
    protected void gvmoduleHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        loadModuleServiceHistory();
        ModalPopupExtender1.Show();
    }
    #endregion
}