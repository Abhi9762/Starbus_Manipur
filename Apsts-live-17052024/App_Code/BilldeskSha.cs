using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

/// <summary>
/// Summary description for Billdesk
/// </summary>
public class BilldeskSha
{
    public BilldeskSha()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public string GetSHA256(string text)
    {
        UTF8Encoding encoder = new UTF8Encoding();

        byte[] hashValue;
        byte[] message = encoder.GetBytes(text);

        SHA256Managed hashString = new SHA256Managed();
        string hex = "";

        hashValue = hashString.ComputeHash(message);
        foreach (byte x in hashValue)
            hex += String.Format("{0:x2}", x);
        return hex;
    }
    public string GetHMACSHA256(string text, string key)
    {
        UTF8Encoding encoder = new UTF8Encoding();
        byte[] hashValue;
        byte[] keybyt = encoder.GetBytes(key);
        byte[] message = encoder.GetBytes(text);
        HMACSHA256 hashString = new HMACSHA256(keybyt);
        string hex = "";
        hashValue = hashString.ComputeHash(message);

        foreach (byte x in hashValue)
            hex += string.Format("{0:x2}", x);

        return hex;
    }
}