using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

/// <summary>
/// Summary description for AesEncrptDecrpt
/// </summary>
public class AesEncrptDecrpt
{
    static string key = ConfigurationManager.AppSettings["AdharEncKey"].ToString();
    public static string Decrypt(string data)
    {
        RijndaelManaged rijndaelCipher = new RijndaelManaged();
        rijndaelCipher.Mode = CipherMode.CBC;
        rijndaelCipher.Padding = PaddingMode.PKCS7;
        rijndaelCipher.KeySize = 0x80;
        rijndaelCipher.BlockSize = 0x80;
        byte[] encryptedData = Convert.FromBase64String(data);
        byte[] pwdBytes = Encoding.UTF8.GetBytes(key);
        byte[] keyBytes = new byte[16];
        int len = pwdBytes.Length;

        if (len > keyBytes.Length)
            len = keyBytes.Length;

        Array.Copy(pwdBytes, keyBytes, len);
        rijndaelCipher.Key = keyBytes;
        rijndaelCipher.IV = keyBytes;
        byte[] plainText = rijndaelCipher.CreateDecryptor().TransformFinalBlock(encryptedData, 0, encryptedData.Length);
        return Encoding.UTF8.GetString(plainText);
    }
    public static string Encrypt(string data)
    {
        RijndaelManaged rijndaelCipher = new RijndaelManaged();
        rijndaelCipher.Mode = CipherMode.CBC;
        rijndaelCipher.Padding = PaddingMode.PKCS7;
        rijndaelCipher.KeySize = 0x80;
        rijndaelCipher.BlockSize = 0x80;
        byte[] pwdBytes = Encoding.UTF8.GetBytes(key);
        byte[] keyBytes = new byte[16];
        int len = pwdBytes.Length;
        if (len > keyBytes.Length)
            len = keyBytes.Length;
        Array.Copy(pwdBytes, keyBytes, len);
        rijndaelCipher.Key = keyBytes;
        rijndaelCipher.IV = keyBytes;
        ICryptoTransform transform = rijndaelCipher.CreateEncryptor();
        byte[] plainText = Encoding.UTF8.GetBytes(data);
        return Convert.ToBase64String(transform.TransformFinalBlock(plainText, 0, plainText.Length));
    }


    protected static byte[] keybytes = Encoding.UTF8.GetBytes(key);
    protected static byte[] iv = { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };


    #region "Enc"
    public string Enc(string str)
    {
        str = EncryptStringAES(str);
        str = str.Replace('+', ' ');
        return str;
    }
    public static string EncryptStringAES(string plainText)
    {
        var encryptedCipherText = Encrypt(plainText, keybytes, iv);
        var encryptedBase64Text = Convert.ToBase64String(encryptedCipherText);
        return string.Format(encryptedBase64Text);
    }
    private static byte[] Encrypt(string plainText, byte[] Key, byte[] IV)
    {
        byte[] encrypted;
        using (AesManaged aes = new AesManaged())
        {
            ICryptoTransform encryptor = aes.CreateEncryptor(Key, IV);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter sw = new StreamWriter(cs))
                        sw.Write(plainText);
                    encrypted = ms.ToArray();
                }
            }
        }
        return encrypted;
    }
    #endregion

    #region "Dec"
    private static string DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iv)
    {
        if (cipherText == null || cipherText.Length <= 0)
        {
            throw new ArgumentNullException("cipherText");
        }
        if (key == null || key.Length <= 0)
        {
            throw new ArgumentNullException("key");
        }
        if (iv == null || iv.Length <= 0)
        {
            throw new ArgumentNullException("key");
        }
        string plaintext = null;
        using (var rijAlg = new RijndaelManaged())
        {
            rijAlg.Mode = CipherMode.ECB;
            //rijAlg.Padding = PaddingMode.PKCS7;
            rijAlg.FeedbackSize = 128;
            rijAlg.Key = key;
            rijAlg.IV = iv;
            var decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);
            try
            {
                using (var msDecrypt = new MemoryStream(cipherText))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new System.IO.StreamReader(csDecrypt))
                        {
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            catch
            {
                plaintext = "keyError";
            }
        }
        return plaintext;
    }
    public string DecryptStringAES(string cipherText)
    {

        cipherText= cipherText.Replace(' ', '+');

        var encrypted = Convert.FromBase64String(cipherText);
        var decriptedFromJavascript = DecryptStringFromBytes(encrypted, keybytes, iv);
        return string.Format(decriptedFromJavascript);
    }

    #endregion


}