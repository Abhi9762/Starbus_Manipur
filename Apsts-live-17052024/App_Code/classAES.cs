using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class classAES
{
    private string ticketkey = System.Configuration.ConfigurationManager.AppSettings["TicketQRCodeKey"].ToString();
    private string passkey = System.Configuration.ConfigurationManager.AppSettings["PassQRCodeKey"].ToString();

    public string AESE(string plaintext)
    {
        System.Security.Cryptography.RijndaelManaged AES = new System.Security.Cryptography.RijndaelManaged();
        System.Security.Cryptography.SHA256Cng SHA256 = new System.Security.Cryptography.SHA256Cng();
        string ciphertext = "";
        try
        {
            AES.GenerateIV();
            AES.Key = SHA256.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(ticketkey));

            AES.Mode = System.Security.Cryptography.CipherMode.CBC;
            System.Security.Cryptography.ICryptoTransform DESEncrypter = AES.CreateEncryptor();
            byte[] Buffer = System.Text.ASCIIEncoding.ASCII.GetBytes(plaintext);
            ciphertext = Convert.ToBase64String(DESEncrypter.TransformFinalBlock(Buffer, 0, Buffer.Length));

            return Convert.ToBase64String(AES.IV) + Convert.ToBase64String(DESEncrypter.TransformFinalBlock(Buffer, 0, Buffer.Length));
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
    public string AESD(string ciphertext)
    {
        System.Security.Cryptography.RijndaelManaged AES = new System.Security.Cryptography.RijndaelManaged();
        System.Security.Cryptography.SHA256Cng SHA256 = new System.Security.Cryptography.SHA256Cng();
        string plaintext = "";
        string iv = "";
        try
        {
            var ivct = ciphertext.Split(new[] { "==" }, StringSplitOptions.None);
            iv = ivct[0] + "==";
            ciphertext = ivct.Length == 3 ? ivct[1] + "==" : ivct[1];

            AES.Key = SHA256.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(ticketkey));
            AES.IV = Convert.FromBase64String(iv);
            AES.Mode = System.Security.Cryptography.CipherMode.CBC;
            System.Security.Cryptography.ICryptoTransform DESDecrypter = AES.CreateDecryptor();
            byte[] Buffer = Convert.FromBase64String(ciphertext);
            plaintext = System.Text.ASCIIEncoding.ASCII.GetString(DESDecrypter.TransformFinalBlock(Buffer, 0, Buffer.Length));
            return plaintext;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
    public string PASS_AESE(string plaintext)
    {
        System.Security.Cryptography.RijndaelManaged AES = new System.Security.Cryptography.RijndaelManaged();
        System.Security.Cryptography.SHA256Cng SHA256 = new System.Security.Cryptography.SHA256Cng();
        string ciphertext = "";
        try
        {
            AES.GenerateIV();
            AES.Key = SHA256.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(passkey));

            AES.Mode = System.Security.Cryptography.CipherMode.CBC;
            System.Security.Cryptography.ICryptoTransform DESEncrypter = AES.CreateEncryptor();
            byte[] Buffer = System.Text.ASCIIEncoding.ASCII.GetBytes(plaintext);
            ciphertext = Convert.ToBase64String(DESEncrypter.TransformFinalBlock(Buffer, 0, Buffer.Length));

            return Convert.ToBase64String(AES.IV) + Convert.ToBase64String(DESEncrypter.TransformFinalBlock(Buffer, 0, Buffer.Length));
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
    public string PASS_AESD(string ciphertext)
    {
        System.Security.Cryptography.RijndaelManaged AES = new System.Security.Cryptography.RijndaelManaged();
        System.Security.Cryptography.SHA256Cng SHA256 = new System.Security.Cryptography.SHA256Cng();
        string plaintext = "";
        string iv = "";
        try
        {
            var ivct = ciphertext.Split(new[] { "==" }, StringSplitOptions.None);
            iv = ivct[0] + "==";
            ciphertext = ivct.Length == 3 ? ivct[1] + "==" : ivct[1];

            AES.Key = SHA256.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(passkey));
            AES.IV = Convert.FromBase64String(iv);
            AES.Mode = System.Security.Cryptography.CipherMode.CBC;
            System.Security.Cryptography.ICryptoTransform DESDecrypter = AES.CreateDecryptor();
            byte[] Buffer = Convert.FromBase64String(ciphertext);
            plaintext = System.Text.ASCIIEncoding.ASCII.GetString(DESDecrypter.TransformFinalBlock(Buffer, 0, Buffer.Length));
            return plaintext;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
}
