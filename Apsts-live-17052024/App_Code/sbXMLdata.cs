using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;

/// <summary>
/// Summary description for sbXMLdata
/// </summary>
public class sbXMLdata
{
    XmlDocument doc = new XmlDocument();
    private sbCommonFunc _common = new sbCommonFunc();
    public sbXMLdata()
    {
        doc.Load(HttpContext.Current.Server.MapPath("CommonData.xml"));
    }
    public string loadDeptLogo()//M1
    {
        try
        {
            string imgname = "";
            XmlNodeList deptlogo = doc.GetElementsByTagName("dept_logo_url");
            if (deptlogo.Item(0).InnerXml != "")
            {
                imgname = deptlogo.Item(0).InnerXml;
            }
            return imgname;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("sbXMLdata-M1", ex.Message.ToString());
            return "";

        }
    }
    public string loadDeptName()//M2
    {
        try
        {
            string DeptName = "";
            XmlNodeList dept_en = doc.GetElementsByTagName("dept_Name_en");
            if (dept_en.Item(0).InnerXml != "")
            {
                DeptName = dept_en.Item(0).InnerXml;
            }
            return DeptName;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("sbXMLdata-M2", ex.Message.ToString());
            return "";

        }
    }
    public string loadVersion()//M3
    {
        try
        {
            string ver = "";
            XmlNodeList Ver_Name = doc.GetElementsByTagName("Ver_Name");
            if (Ver_Name.Item(0).InnerXml != "")
            {
                ver = Ver_Name.Item(0).InnerXml;
            }
            return ver;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("sbXMLdata-M3", ex.Message.ToString());
            return "";

        }
    }
    public string loadEmail()//M4
    {
        try
        {
            string emailid = "";
            XmlNodeList email = doc.GetElementsByTagName("email");
            if (email.Item(0).InnerXml != "")
            {
                emailid = email.Item(0).InnerXml;
            }
            return emailid;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("sbXMLdata-M4", ex.Message.ToString());
            return "";

        }
    }
    public string loadContact()//M5
    {
        try
        {
            string contactNo = "";
            XmlNodeList contactNo1 = doc.GetElementsByTagName("contactNo1");
            if (contactNo1.Item(0).InnerXml != "")
            {
                contactNo = contactNo1.Item(0).InnerXml;
            }
            return contactNo;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("sbXMLdata-M5", ex.Message.ToString());
            return "";

        }
    }
    public string loadtollfree()//M6
    {
        try
        {
            string toll_freeno = "";
            XmlNodeList tollfreeno = doc.GetElementsByTagName("tollfreeno");
            if (tollfreeno.Item(0).InnerXml != "")
            {
                toll_freeno = tollfreeno.Item(0).InnerXml;
            }
            return toll_freeno;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("sbXMLdata-M6", ex.Message.ToString());
            return "";

        }
    }
    public string loadhelpdeskemail()//M6
    {
        try
        {
            string emailid = "";
            XmlNodeList email = doc.GetElementsByTagName("email");
            if (email.Item(0).InnerXml != "")
            {
                emailid = email.Item(0).InnerXml;
            }
            return emailid;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("sbXMLdata-M6", ex.Message.ToString());
            return "";

        }
    }
    public string loadhomepage(int i)//M7
    {
        try
        {
            string homepage = "";
            if (i==1)
            {
                XmlNodeList homepage_img1_url = doc.GetElementsByTagName("homepage_img1_url");
                homepage = homepage_img1_url.Item(0).InnerXml;
            }
            else if (i==2)
            {
                XmlNodeList homepage_img2_url = doc.GetElementsByTagName("homepage_img2_url");
                homepage = homepage_img2_url.Item(0).InnerXml;
            }
            else if (i == 3)
            {
                XmlNodeList homepage_img3_url = doc.GetElementsByTagName("homepage_img3_url");
                homepage = homepage_img3_url.Item(0).InnerXml;
            }
            else if (i == 4)
            {
                XmlNodeList homepage_img4_url = doc.GetElementsByTagName("homepage_img4_url");
                homepage = homepage_img4_url.Item(0).InnerXml;
            }
            else if (i == 5)
            {
                XmlNodeList homepage_img5_url = doc.GetElementsByTagName("homepage_img5_url");
                homepage = homepage_img5_url.Item(0).InnerXml;
            }
            else if (i == 6)
            {
                XmlNodeList homepage_img6_url = doc.GetElementsByTagName("homepage_img6_url");
                homepage = homepage_img6_url.Item(0).InnerXml;
            }
            return homepage;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("sbXMLdata-M6", ex.Message.ToString());
            return "";

        }
    }
    public string loadfacebooklink()//M8
    {
        try
        {
            string facebook = "0";
            XmlNodeList facebooklink = doc.GetElementsByTagName("facebooklink");
            if (facebooklink.Item(0).InnerXml != "")
            {
                facebook = facebooklink.Item(0).InnerXml;
            }
            return facebook;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("sbXMLdata-M8", ex.Message.ToString());
            return "0";

        }
    }
    public string loadtwiterlink()//M9
    {
        try
        {
            string twitter = "0";
            XmlNodeList twitterlink = doc.GetElementsByTagName("twitterlink");
            if (twitterlink.Item(0).InnerXml != "")
            {
                twitter = twitterlink.Item(0).InnerXml;
            }
            return twitter;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("sbXMLdata-M9", ex.Message.ToString());
            return "0";

        }
    }
    public string loadinstalink()//M10
    {
        try
        {
            string insta = "0";
            XmlNodeList instalink = doc.GetElementsByTagName("instalink");
            if (instalink.Item(0).InnerXml != "")
            {
                insta = instalink.Item(0).InnerXml;
            }
            return insta;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("sbXMLdata-M10", ex.Message.ToString());
            return "0";

        }
    }
    public string loadyoutubelink()//M11
    {
        try
        {
            string youtube = "0";
            XmlNodeList youtubelink = doc.GetElementsByTagName("youtubelink");
            if (youtubelink.Item(0).InnerXml != "")
            {
                youtube = youtubelink.Item(0).InnerXml;
            }
            return youtube;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("sbXMLdata-M11", ex.Message.ToString());
            return "0";

        }
    }
    public string loadtitle()//M12
    {
        try
        {
            string title_txt = "State1";
            XmlNodeList title_txt_en = doc.GetElementsByTagName("title_txt_en");
            if (title_txt_en.Item(0).InnerXml != "")
            {
                title_txt = title_txt_en.Item(0).InnerXml;
            }
            return title_txt;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("sbXMLdata-M12", ex.Message.ToString());
            return "State1";

        }
    }
    public string loadDeptNameAbbr()//M13
    {
        try
        {
            string dept_Abbr = "";
            XmlNodeList dept_Abbr_en = doc.GetElementsByTagName("dept_Abbr_en");
            if (dept_Abbr_en.Item(0).InnerXml != "")
            {
                dept_Abbr = dept_Abbr_en.Item(0).InnerXml;
            }
            return dept_Abbr;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("sbXMLdata-M13", ex.Message.ToString());
            return "";

        }
    }
    public DataTable loadmentors()//M14
    {
        DataTable dtmentors = new DataTable();
        try
        {
            dtmentors.Columns.Add("mentorimage", typeof(string));
            dtmentors.Columns.Add("mentorname", typeof(string));
            dtmentors.Columns.Add("mentordesignation", typeof(string));            

            XmlNodeList mentor1image = doc.GetElementsByTagName("mentor1image");
            XmlNodeList mentor1name = doc.GetElementsByTagName("mentor1name");
            XmlNodeList mentor1designation = doc.GetElementsByTagName("mentor1designation");
            if (mentor1image.Item(0).InnerXml != "" && mentor1name.Item(0).InnerXml != "" && mentor1designation.Item(0).InnerXml != "")
            {                
                dtmentors.Rows.Add(mentor1image.Item(0).InnerXml.ToString(), mentor1name.Item(0).InnerXml.ToString(), mentor1designation.Item(0).InnerXml.ToString());
            }

            XmlNodeList mentor2image = doc.GetElementsByTagName("mentor2image");
            XmlNodeList mentor2name = doc.GetElementsByTagName("mentor2name");
            XmlNodeList mentor2designation = doc.GetElementsByTagName("mentor2designation");
            if (mentor2image.Item(0).InnerXml != "" && mentor2name.Item(0).InnerXml != "" && mentor2designation.Item(0).InnerXml != "")
            {                
                dtmentors.Rows.Add(mentor2image.Item(0).InnerXml.ToString(), mentor2name.Item(0).InnerXml.ToString(), mentor2designation.Item(0).InnerXml.ToString());
            }
            
            XmlNodeList mentor3image = doc.GetElementsByTagName("mentor3image");
            XmlNodeList mentor3name = doc.GetElementsByTagName("mentor3name");
            XmlNodeList mentor3designation = doc.GetElementsByTagName("mentor3designation");
            if (mentor3image.Item(0).InnerXml != "" && mentor3name.Item(0).InnerXml != "" && mentor3designation.Item(0).InnerXml != "")
            {                
                dtmentors.Rows.Add(mentor3image.Item(0).InnerXml.ToString(), mentor3name.Item(0).InnerXml.ToString(), mentor3designation.Item(0).InnerXml.ToString());
            }
            
            XmlNodeList mentor4image = doc.GetElementsByTagName("mentor4image");
            XmlNodeList mentor4name = doc.GetElementsByTagName("mentor4name");
            XmlNodeList mentor4designation = doc.GetElementsByTagName("mentor4designation");
            if (mentor4image.Item(0).InnerXml != "" && mentor4name.Item(0).InnerXml != "" && mentor4designation.Item(0).InnerXml != "")
            {
                 dtmentors.Rows.Add(mentor4image.Item(0).InnerXml.ToString(), mentor4name.Item(0).InnerXml.ToString(), mentor4designation.Item(0).InnerXml.ToString());
            }
            
           
            return dtmentors;

        }
        catch(Exception ex)
        {
            _common.ErrorLog("sbXMLdata-M14", ex.Message.ToString());
            return dtmentors;
        }
    }
    public string loadpios()//M15
    {
        try
        {
            string pio;
            XmlNodeList pios = doc.GetElementsByTagName("pios");
            pio = pios.Item(0).InnerXml;
            return pio;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("sbXMLdata-M15", ex.Message.ToString());
            return "";

        }
    }
    public DataTable loadRti()//M16
    {
        DataTable dtrti = new DataTable();
        try
        {
            dtrti.Columns.Add("rti1", typeof(string));
            dtrti.Columns.Add("rti2", typeof(string));
            dtrti.Columns.Add("rti3", typeof(string));

            XmlNodeList manual1 = doc.GetElementsByTagName("rti_manual1");
            XmlNodeList manual2 = doc.GetElementsByTagName("rti_manual2");
            XmlNodeList manual3 = doc.GetElementsByTagName("rti_manual3");

           dtrti.Rows.Add(manual1.Item(0).InnerXml.ToString(), manual2.Item(0).InnerXml.ToString(), manual3.Item(0).InnerXml.ToString());
          
            return dtrti;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("sbXMLdata-M16", ex.Message.ToString());
            return dtrti;
        }
    }
    public static bool checkModuleCategory(string categoryid)
    {
        try
        {
            string xmlFilePath = HttpContext.Current.Server.MapPath("~/CommonData.xml");
            XDocument doc = XDocument.Load(xmlFilePath);
            XElement parent = doc.Descendants("Additional_modules").FirstOrDefault();
            if (parent != null)
            {
                foreach (XElement element in parent.Elements("Module_Id"))
                {
                    if (categoryid == element.Value)
                    {
                        return true;
                    }
                }
                return false;
            }
            return false;
        }
        catch (Exception ex)
        {
            return false;
        }
        
    }

}