using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

/// <summary>
/// Summary description for sbSecurity
/// </summary>
public class sbSecurity
{
    Cache cachechk = new Cache();
    private sbCommonFunc _common = new sbCommonFunc();
    HttpCookie cSccookie = HttpContext.Current.Request.Cookies.Get(".etktFormsAspx");
    public sbSecurity()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public Boolean alreadyLogin(string usercode)
    {
        try
        {
            // string key = cachechk["Login_" + usercode].ToString();
            if (cachechk["Login_" + usercode] != null)
            {
                return true;
            }
            else
            {
                cachechk.Add("Login_" + usercode, HttpContext.Current.Session.SessionID, null/* TODO Change to default(_) if this is not a reference type */, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.Default, null/* TODO Change to default(_) if this is not a reference type */);
                return false;
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("sbSecurity-M1", ex.Message.ToString());
            return true;
        }
    }
    public Boolean CheckOldUserLogin(string usercode)
    {
        try
        {
            if (cachechk["Login_" + usercode].ToString().ToUpper() != HttpContext.Current.Session.SessionID.ToUpper())
            {
                return false;
            }

            return true;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("sbSecurity-M2", ex.Message.ToString());
            return false;
        }
    }
    public void RemoveUserLogin(string usercode)
    {
        try
        {
            cachechk.Remove("Login_" + usercode);

        }
        catch (Exception ex)
        {
            _common.ErrorLog("sbSecurity-M3", ex.Message.ToString());

        }
    }
    public Boolean checkvalidation()
    {
        try
        {           
            //Cookie Security
            System.Security.Cryptography.MD5CryptoServiceProvider SecMD5 = new System.Security.Cryptography.MD5CryptoServiceProvider();

            if (!(HttpContext.Current.Session["_eTktTicketID"] == null || HttpContext.Current.Session["_eTktTicketID"].ToString() == ""))
            {
                if (cSccookie.Value.ToString() != HttpContext.Current.Session["_eTktTicketID"].ToString())
                {
                    return false;
                }
                HttpContext.Current.Session["_eTktTicketID"] = HttpContext.Current.Session["_eTktTicketID"].ToString();
            }
            else
            {
                return false;
            }


            return true ;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("sbSecurity-M4", ex.Message.ToString());
            return false;
        }
    }
   
    public string GeneratePassword(int length, int numberOfNonAlphanumericCharacters)
    {
        // Make sure length and numberOfNonAlphanumericCharacters are valid....
        if (((length < 1) || (length > 128)))
            throw new ArgumentException("Membership_password_length_incorrect");

        if (((numberOfNonAlphanumericCharacters > length) || (numberOfNonAlphanumericCharacters < 0)))
            throw new ArgumentException("Membership_min_required_non_alphanumeric_characters_incorrect");

        while (true)
        {
            int i;
            int nonANcount = 0;
            byte[] buffer1 = new byte[length - 1 + 1];

            // chPassword contains the password's characters as it's built up
            char[] chPassword = new char[length - 1 + 1];

            // chPunctionations contains the list of legal non-alphanumeric characters
            char[] chPunctuations = "!@@$%^^*()_-+=[{]};:>|./?".ToCharArray();

            // Get a cryptographically strong series of bytes
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(buffer1);

            for (i = 0; i <= length - 1; i++)
            {
                // Convert each byte into its representative character
                int rndChr = (buffer1[i] % 87);
                if ((rndChr < 10))
                    chPassword[i] = Convert.ToChar(Convert.ToUInt16(48 + rndChr));
                else if ((rndChr < 36))
                    chPassword[i] = Convert.ToChar(Convert.ToUInt16((65 + rndChr) - 10));
                else if ((rndChr < 62))
                    chPassword[i] = Convert.ToChar(Convert.ToUInt16((97 + rndChr) - 36));
                else
                {
                    chPassword[i] = chPunctuations[rndChr - 62];
                    nonANcount += 1;
                }
            }

            if (nonANcount < numberOfNonAlphanumericCharacters)
            {
                Random rndNumber = new Random();
                for (i = 0; i <= (numberOfNonAlphanumericCharacters - nonANcount) - 1; i++)
                {
                    int passwordPos;
                    do
                        passwordPos = rndNumber.Next(0, length);
                    while (!char.IsLetterOrDigit(chPassword[passwordPos]));
                    chPassword[passwordPos] = chPunctuations[rndNumber.Next(0, chPunctuations.Length)];
                }
            }

            return new string(chPassword);
        }
        return "";    // http://aspnet.4guysfromrolla.com/demos/GeneratePassword.aspx
    }
    public Boolean isSessionExist(object session)
    {
        try
        {
            if (session != null)
            {
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("sbSecurity-M5", ex.Message.ToString());
            return false ;
        }
    }

}