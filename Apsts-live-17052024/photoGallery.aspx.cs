using System;
using System.Xml;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Data;
using System.IO;
using System.Xml.XPath;

public partial class photoGallary : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["Heading"] = "Photo Gallery";
        Session["Heading1"] = "Lets see what is going on";
        if (!IsPostBack)
        {
            pnlMain.Visible = false;
            pnlNoRecord.Visible = true;
            loadCategory();
        }
    }

    void loadCategory()
    {
        string albumName;
        Int16 albumID;
        DataTable MyTable = new DataTable();
        MyTable.Columns.Add("categoryId", typeof(int));
        MyTable.Columns.Add("categoryName", typeof(string));
        //--------------------------------------------//
        XmlDocument xDoc1 = new XmlDocument();
        xDoc1.Load(Server.MapPath("CommonDataPhoto.xml"));
        //    XmlNode CategotyNode = xDoc1.SelectSingleNode("//Category[@Id='" + catgoryID.ToString() + "']//Photo[@Id='" + photoid.ToString() + "']") as XmlNode;
        XmlNodeList nodes = xDoc1.SelectNodes("//Category");
        foreach (XmlNode node in nodes)
        {
            albumName = node["NameEng"].InnerText;
            var attribute = node.Attributes["Id"];
            albumID =Int16.Parse(attribute.Value.ToString());

            DataRow row = MyTable.NewRow();
            row["categoryId"] = albumID;
            row["categoryName"] = albumName;
            MyTable.Rows.Add(row);

          

            
        }
        if (MyTable.Rows.Count>0)
        {
           
            pnlMain.Visible = true;
            pnlNoRecord.Visible = false;

            gvCategoryList.DataSource = MyTable;
            gvCategoryList.DataBind();
            gvCategoryList.Visible = true;
            lblAlbumName.Text = MyTable.Rows[0][1].ToString();
            loadPhotographs(Int16.Parse(MyTable.Rows[0][0].ToString()));
        }
    }
    //protected void rptrAlbum_ItemCommand(object source, RepeaterCommandEventArgs e)
    //{
    //    if (e.CommandName == "loadphoto")
    //    {
    //        HiddenField hndcategoryId = (HiddenField)e.Item.FindControl("hndcategoryId");
    //        Int16 categoryID=Int16.Parse(hndcategoryId.Value.ToString());

    //        loadPhotographs(categoryID);
    //    }
    //}
    void loadPhotographs(Int16 categoryID)
    {

        string albumName;
        Int16 albumID;
        DataTable MyTable = new DataTable();
        MyTable.Columns.Add("photoId", typeof(int));
        MyTable.Columns.Add("photo_name", typeof(string));
        MyTable.Columns.Add("photoURL", typeof(string));

        XmlDocument xDoc1 = new XmlDocument();
        xDoc1.Load(Server.MapPath("CommonDataPhoto.xml"));
      //  XmlNode CategotyNode = xDoc1.SelectSingleNode("//Category[@Id='" + categoryID.ToString() + "']//Photo") as XmlNode;
        XmlNodeList nodes = xDoc1.SelectNodes("//Category[@Id='" + categoryID.ToString() + "']//Photo");
        foreach (XmlNode node in nodes)
        {
            var attribute1 = node.Attributes["Id"];
            var attribute2 = node.Attributes["title"];


            DataRow row = MyTable.NewRow();
            row["photoId"] = Int16.Parse(attribute1.Value.ToString());
            row["photo_name"] = attribute2.Value.ToString();
            row["photoURL"] = node.InnerText;
            MyTable.Rows.Add(row);


        }
        if (MyTable.Rows.Count > 0)
        {
            rptPhotos.DataSource = MyTable;
            rptPhotos.DataBind();
            rptPhotos.Visible = true;
           

        }
    }



    protected void gvCategoryList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            e.Row.ToolTip = "Click to view photograph ";
            e.Row.Attributes.Add("onMouseOver", "this.className='Rowhighlight';");
            e.Row.Attributes.Add("onMouseout", "this.className='Rowdefaultcolor';");
            e.Row.Attributes["onclick"]=Page.ClientScript.GetPostBackClientHyperlink((GridView)sender, "Select$" + e.Row.RowIndex);
        }
    }

    protected void gvCategoryList_SelectedIndexChanged(object sender, EventArgs e)
    {

        int rowindex;
        string albumname = gvCategoryList.SelectedRow.Cells[0].Text;
        rowindex = gvCategoryList.SelectedRow.RowIndex;
        Int16 categoryID = Convert.ToInt16(gvCategoryList.DataKeys[rowindex].Values["categoryId"].ToString());


        lblAlbumName.Text = albumname;

         loadPhotographs(categoryID);
        
    }
}