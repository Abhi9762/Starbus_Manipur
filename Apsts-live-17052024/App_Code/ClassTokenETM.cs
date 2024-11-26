using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ClassTokenETM
/// </summary>
public class ClassTokenETM
{
    private string uID; // field
    public string uIMEI;
    public string uAuthenticationToken;
    public string UserId   // property
    {
        get { return uID; }   // get method
        set { uID = value; }  // set method
    }

    public string UserIMEI
    {
        get
        {
            return uIMEI;
        }
        set
        {
            uIMEI = value;
        }
    }
    public string UserAuthenticationToken
    {
        get
        {
            return uAuthenticationToken;
        }
        set
        {
            uAuthenticationToken = value;
        }
    }
    public bool IsUserCredentialsValid(string UserID, string UserIMEI)
    {
        if (UserID == "8A5CE72749C4FEAAB9D5D1EBD34735D0FD32442F" & UserIMEI.Equals("8A5CE72749C4FEAAB9D5D1EBD34735D0FD32442F"))
            return true;
        else
            return false;
    }
    public bool IsUserCredentialsValid(ClassToken_pathik SoapHeader)
    {
        if (SoapHeader == null)
            return false;
        if (!(string.IsNullOrEmpty(SoapHeader.UserAuthenticationToken)))
            return (HttpRuntime.Cache[SoapHeader.UserAuthenticationToken] != null);
        return false;
    }
}