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

public partial class Auth_Thirdpartylogoconfig : System.Web.UI.Page
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
            Session["_moduleName"] = "Third Party Logo Configuration";

            LoadAgType(ddlagtype);


        }
        

    }


    #region "Methods"

    public void Errormsg(string msg)
    {
        lblerrormsg.Text = msg;
        mperror.Show();
    }

    public void SuccessMessage(string msg)
    {
        lblsuccessmsg.Text = msg;
        mpsuccess.Show();
    }

    private void LoadAgType(DropDownList ddl)
    {
        try
        {
            ddl.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_thirdparty_agtype");
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                    ddl.DataSource = dt;
                    ddl.DataTextField = "typename_";
                    ddl.DataValueField = "typeid_";
                    ddl.DataBind();
            
            }

            ddl.Items.Insert(0, "Select");
            ddl.Items[0].Value = "0";
            ddl.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddl.Items.Insert(0, "Select");
            ddl.Items[0].Value = "0";
            ddl.SelectedIndex = 0;
        }
    }

    public bool CheckFileExtension(System.Web.UI.WebControls.FileUpload fuFileUpload)
    {
        try
        {
            decimal size = Math.Round((decimal)fuFileUpload.PostedFile.ContentLength / 1024, 2);
            System.Drawing.Image img = System.Drawing.Image.FromStream(fuFileUpload.PostedFile.InputStream);
            int height = img.Height;
            int width = img.Width;

            if (size > 100)
            {
                Errormsg("Logo size must not exceed 100 KB.");
                return false;
            }

            bool fileExtensionOK = false;
            string fileExtension = System.IO.Path.GetExtension(fuFileUpload.FileName).ToLower();
            string[] allowedExtensions = { ".png" };

            foreach (string ext in allowedExtensions)
            {
                if (fileExtension == ext)
                {
                    fileExtensionOK = true;
                    break;
                }
            }

            return fileExtensionOK;
        }
        catch (Exception ex)
        {
            Errormsg("Please upload only .png images.");
            return false;
        }
    }

    public byte[] ConvertByteFile(System.Web.UI.WebControls.FileUpload fuFileUpload)
    {
        int intFileLength;
        byte[] byteData;

        if (fuFileUpload.HasFile)
        {
            if (CheckFileExtension(fuFileUpload))
            {
                intFileLength = fuFileUpload.PostedFile.ContentLength;
                byteData = fuFileUpload.FileBytes;
            }
            else
            {
                return null;
            }
        }
        else
        {
            intFileLength = fuFileUpload.PostedFile.ContentLength;
            byteData = fuFileUpload.FileBytes;
        }

        return byteData;
    }

    public string GetImage(object img)
    {
        try
        {
            return "data: Image/png;base64," + Convert.ToBase64String((byte[])img);
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    private bool ValidValue()
    {
        try
        {
            int msgcnt = 0;
            string msg = "";

            if (Convert.ToInt32(Session["DeptLogocount"].ToString()) == 0)
            {
                if (Session["DeptLogoImg"] == null)
                {
                    msgcnt++;
                    msg += msgcnt.ToString() + ". Department Logo.<br>";
                }
            }

            if (ddlagtype.SelectedValue == "0")
            {
                msgcnt++;
                msg += msgcnt.ToString() + ". Select Valid Agent Type.<br>";
            }

            if (msgcnt > 0)
            {
                Errormsg(msg);
                return false;
            }

            return true;
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message.ToString());
            return false;
        }
    }

    private void SaveUpdateLogo()
    {
        try
        {
            string ipaddress = HttpContext.Current.Request.UserHostAddress;
            string deptlogo = "";

            if (Convert.ToInt32(Session["DeptLogocount"].ToString()) == 0)
            {
                deptlogo = "../Logo/" + ddlagtype.SelectedValue + ".png";
            }
            else
            {
                deptlogo = Session["DeptLogoImgURL"].ToString();
            }

            string agtype = ddlagtype.SelectedValue.ToString();

            string saveDirectory = "../Logo";
            string fileName = "";
            string fileSavePath = "";

            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("../CommonData.xml"));

            XmlNode parNode;
            XmlNode newChild;

            parNode = doc.SelectSingleNode("/Data/Page");

            if (Convert.ToInt32(Session["DeptLogocount"].ToString()) == 0)
            {
                fileName = ddlagtype.SelectedValue.ToString() + ".png";
                fileSavePath = Path.Combine(saveDirectory, fileName);
                byte[] deptBytes = (byte[])Session["DeptLogoImg"];
                System.IO.File.WriteAllBytes(Server.MapPath(fileSavePath), deptBytes);

                XmlNodeList dlogo = doc.GetElementsByTagName("Third_Party_" + ddlagtype.SelectedItem.ToString().ToUpper().Replace(" ", ""));
                XmlNode xmlnode = parNode.SelectSingleNode("Third_Party_" + ddlagtype.SelectedItem.ToString().ToUpper().Replace(" ", ""));

                if (xmlnode == null)
                {
                    newChild = doc.CreateNode(XmlNodeType.Element, "Third_Party_" + ddlagtype.SelectedItem.ToString().ToUpper().Replace(" ", ""), "");
                    newChild.InnerText = saveDirectory + "/" + fileName;
                    parNode.AppendChild(newChild);
                }
                else
                {
                    dlogo[0].InnerXml = saveDirectory + "/" + fileName;
                }
            }

            doc.Save(Server.MapPath("../CommonData.xml"));
            SuccessMessage(ddlagtype.SelectedItem.ToString() + " Logo has been successfully updated.");
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message.ToString());
        }
    }


    #endregion


    #region "Events"
    protected void btnDeotLogo_Click(object sender, EventArgs e)
    {
        if (!CheckFileExtension(fuDeptLogo))
        {
            Errormsg("Department logo must be in .png format and size must not exceed 100 KB.");
            return;
        }
        byte[] PhotoImage = ConvertByteFile(fuDeptLogo);
        ImgDepartmentLogo.ImageUrl = GetImage(PhotoImage);
        Session["DeptLogoImg"] = PhotoImage;
        HttpPostedFile file = fuDeptLogo.PostedFile;
        System.Drawing.Image image = System.Drawing.Image.FromStream(file.InputStream);
        Session["DEPTLOGO"] = image;
        Session["DeptLogocount"] = 0;
        ImgDepartmentLogo.Visible = true;

    }

    protected void lbtnSave_Click(object sender, EventArgs e)
    {
        if (!ValidValue())
        {
            return;
        }
        lblConfirmation.Text = "Do you want to update " + ddlagtype.SelectedItem.ToString() + " logo ?";
        mpConfirmation.Show();
        Session["Action"] = "S";
    }

    protected void lbtnReset_Click(object sender, EventArgs e)
    {
        Session["DeptLogocount"] = 0;
        Session["DeptLogoImg"] = null;
        Session["DEPTLOGO"] = null;
        ddlagtype.SelectedValue = "0";
        ImgDepartmentLogo.Visible = false;
    }

    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        if (Session["Action"].ToString() == "S")
        {
            SaveUpdateLogo();
        }
    }

    #endregion
}