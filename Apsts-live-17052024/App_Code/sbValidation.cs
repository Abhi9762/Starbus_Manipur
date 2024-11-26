
using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
/// <summary>
/// Summary description for sbValidation
/// </summary>
public class sbValidation
{
    private Regex _alphaNumericPattern;
    private string[] _reservedWords = new[] { "WAITFOR", "DELAY", "SCRIPT", "CREATE", "INSERT", "SELECT", "DELETE", "DROP", "UPDATE", "WAIT" };

    char[] illeChars = "!@#$%^&*{}[]\"_+!<>?;~`|".ToCharArray();
    public sbValidation()
    {
        
    }
 public bool IsValidPnr(string valChk, int strLengthMin, int strLengthMax)
    {
        // -----------'-----------
        if (((valChk.Trim().Length > strLengthMax)
                    || (valChk.Trim().Length < strLengthMin)))
        {
            return false;
        }

        // If isInvalidCharacters(valChk) = False Then
        //     Return False
        // End If
        int i;
        for (i = 1; (i <= (illeChars.Length - 1)); i++)
        {


            if (valChk.Trim().Contains(illeChars[i].ToString()))
            {
                return false;
            }

        }
        return true;
        //if ((this.IfExistsReservewords(valChk) == true)) {
        //    return false;
        //}


    }
    public bool IfExistsReservewords(string strValue)
    {
        int i;
        for (i = 0; (i <= 6); i++)
        {
            if (((strValue.IndexOf(_reservedWords[i]) + 1) == 0))
            {

            }
            else
            {
                return true;
                // TODO: Exit Function: Warning!!! Need to return the value

            }

        }

        return false;
    }
    public bool IsDate(string tempDate)
    {
        DateTime fromDateValue;
        var formats = new[] { "dd/MM/yyyy", "yyyy-MM-dd" };
        if (DateTime.TryParseExact(tempDate, formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out fromDateValue))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool IsValidateDateTime(string txtDate)
    {
        DateTime tempDate;
        return DateTime.TryParse(txtDate, out tempDate);
    }
    public bool IsValidInteger(string valChk, int strLengthMin, int strLengthMax)
    {
        // -----------'-----------


        if (((valChk.Trim().Length > strLengthMax)
                    || (valChk.Trim().Length < strLengthMin)))
        {
            return false;
        }

        // -----------'-----------
       // if ((strLengthMin > 0))
       // {
         //   if ((Convert.ToInt64(valChk.Trim()) == 0))
          //  {
            //    return false;
           // }
//        }

        // -----------'-----------
        string strChk;
        int iLoop;
        // ****Number Test**************
        for (iLoop = 1; (iLoop <= valChk.Length); iLoop++)
        {
            strChk = valChk.Substring((iLoop - 1), 1);
            if ((("0123456789".IndexOf(strChk) + 1)
                        <= 0))
            {
                return false;
            }

        }

        // **Zero value/space test*********
        for (iLoop = 1; (iLoop <= valChk.Length); iLoop++)
        {
            strChk = valChk.Substring((iLoop - 1), 1);
            if ((strChk == " "))
            {
                return false;
            }

        }

        // **********************
        return true;
    }

    public bool isValideMailID(string valCheck)
    {
        try
        {
            if ((valCheck.Length == 0))
            {
                return false;
            }

            if ((valCheck.Length > 50))
            {
                return false;
            }

            _alphaNumericPattern = new Regex("\\w+([-+.\']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*");
            return _alphaNumericPattern.IsMatch(valCheck);
        }
        catch (Exception ex)
        {
            return false;
        }

    }

    public bool isValideDecimalNumber(string valCheck, int strLengthMin, int strLengthMax)
    {
        try
        {
            if (((valCheck.Trim().Length < strLengthMin)
                        || (valCheck.Length > strLengthMax)))
            {
                return false;
            }

            if ((strLengthMin == 1))
            {
                if ((Convert.ToDecimal(valCheck) == 0))
                {
                    return false;
                }

            }

            //  _alphaNumericPattern = New Regex("^([0-9]+)[.]([0-9]{2})$")
            _alphaNumericPattern = new Regex("^\\d{0,9}(\\.\\d{1,2})?$");
            // _alphaNumericPattern = New Regex("\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")
            return _alphaNumericPattern.IsMatch(valCheck);
        }
        catch (Exception ex)
        {
            return false;
        }

    }

    public bool isValideDecimalNumberWithSign(string valCheck, int strLengthMin, int strLengthMax)
    {
        try
        {
            if ((valCheck.Trim().Substring(0, 1) == "-"))
            {
                strLengthMax = (strLengthMax - 1);
            }

            if (((valCheck.Trim().Length < strLengthMin)
                        || (valCheck.Length > strLengthMax)))
            {
                return false;
            }

            // _alphaNumericPattern = New Regex("^([0-9]+)[.]([0-9]{2})$")
            _alphaNumericPattern = new Regex("^[-]?([1-9]{1}[0-9]{0,}(\\.[0-9]{0,2})?|0(\\.[0-9]{0,2})?|\\.[0-9]{1,2})$");
            return _alphaNumericPattern.IsMatch(valCheck);
        }
        catch (Exception ex)
        {
            return false;
        }

    }

    public bool isValidURL(string valCheck)
    {
        try
        {
            if ((valCheck.Trim().Length == 0))
            {
                return false;
            }

            // _alphaNumericPattern = New Regex("http://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?")
            _alphaNumericPattern = new Regex("http(s)?://([\\w-]+\\.)+[\\w-]+(/[\\w- ./?%&=]*)?");
            return _alphaNumericPattern.IsMatch(valCheck);
        }
        catch (Exception ex)
        {
            return false;
        }

    }

    public bool IsValidString(string valChk, int strLengthMin, int strLengthMax)
    {
        // -----------'-----------
        if (((valChk.Trim().Length > strLengthMax)
                    || (valChk.Trim().Length < strLengthMin)))
        {
            return false;
        }

        // If isInvalidCharacters(valChk) = False Then
        //     Return False
        // End If
        int i;
        for (i = 1; (i <= (illeChars.Length - 1)); i++)
        {


            if (valChk.Trim().Contains(illeChars[i].ToString()))
            {
                return false;
            }

        }
        return true;
        //if ((this.IfExistsReservewords(valChk) == true)) {
        //    return false;
        //}


    }

    public bool IsValidAddress(string valChk, int strLengthMin, int strLengthMax)
    {
        // -----------'-----------
        if (((valChk.Trim().Length > strLengthMax)
                    || (valChk.Trim().Length < strLengthMin)))
        {
            return false;
        }

        int i;
        for (i = 1; (i
                    <= (illeChars.Length - 1)); i++)
        {
            if (valChk.Trim().Contains(illeChars[i].ToString()))
            {
                return false;
            }

        }

        int dIndex = valChk.IndexOf(";");
        if ((dIndex > -1))
        {
            return false;
        }

        int dIndex1 = valChk.IndexOf("\'");
        if ((dIndex1 > -1))
        {
            return false;
        }

        int dIndex3 = valChk.IndexOf("{");
        if ((dIndex3 > -1))
        {
            return false;
        }

        int dIndex4 = valChk.IndexOf("}");
        if ((dIndex4 > -1))
        {
            return false;
        }

        int dIndex5 = valChk.IndexOf("(");
        if ((dIndex5 > -1))
        {
            return false;
        }

        int dIndex6 = valChk.IndexOf(")");
        if ((dIndex6 > -1))
        {
            return false;
        }

        // Dim dIndex1 = valChk.IndexOf(":")
        // If (dIndex > -1) Then
        //     Return False
        // End If
        if ((this.IfExistsReservewords(valChk) == true))
        {
            return false;
        }

        return true;
    }

    public bool isValidMobileNumber(string valCheck)
    {
        try
        {
            if ((valCheck.Length == 0))
            {
                return false;
            }

            _alphaNumericPattern = new Regex("^([1-9]{1})([0-9]{1})([0-9]{3,11})$");
            return _alphaNumericPattern.IsMatch(valCheck);
        }
        catch (Exception ex)
        {
            return false;
        }

    }
   
        // Regular expression used to validate a phone number.
        public const string motif = @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$";

        public  bool isValidLandLineNo(string number)
        {
            if (number != null) return Regex.IsMatch(number, motif);
            else return false;
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
public bool IsValidAmount(string valCheck)
	{
		try
		{
			if (valCheck.Trim().Length == 0)
			{
				return false;
			}
			_alphaNumericPattern = new Regex(@"^\d+(\.\d\d)");
			return _alphaNumericPattern.IsMatch(valCheck);
		}
		catch (Exception ex)
		{
			return false;
		}
	}
}