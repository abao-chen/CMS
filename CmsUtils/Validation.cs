/************************************************************************
* FileName:	Validation.cs
* ---------------------------------------------------------------------
* File Comment:
* 	function validation
*
* Details:
* 	Validate date and number.
*
* History:
*	SIRID		When		Who					Why
*	Create  	2004/12/21	Tadashi Shioi		Create
*	BR50SIR0000	2006/09/20	Tetsuya NAGASHIMA	Add comments is used by NDoc
*
************************************************************************/

using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CmsUtils
{
    /// <summary>
    ///     Validate date and number
    /// </summary>
    public class Validation
    {
        /// <summary>
        ///     constructor
        /// </summary>
        private Validation()
        {
        }

        /// <summary>
        ///     check if parameters 'year','month' and 'day' is date.
        /// </summary>
        /// <param name="year">year</param>
        /// <param name="month">month</param>
        /// <param name="day">day</param>
        /// <returns>true:is date; false:is not date</returns>
        public static bool IsDate(int year, int month, int day)
        {
            // check input value
            // year
            if (year <= 0 || 10000 <= year)
                return false;

            // month
            if (month < 1 || 12 < month)
                return false;

            // day
            if (day < 1 || 31 < day)
                return false;

            return day <= DateTime.DaysInMonth(year, month);
        }

        /// <summary>
        ///     check if the string is digit
        /// </summary>
        /// <param name="chkString">string need to check</param>
        /// <returns>true:is digit false:is not digit</returns>
        public static bool IsDigit(string chkString)
        {
            var result = true;
            if (!StringUtil.IsNull(chkString))
            {
                var checkChar = chkString.ToCharArray();
                foreach (var ch in checkChar)
                    if (!char.IsDigit(ch))
                    {
                        result = false;
                        break;
                    }
            }
            return result;
        }

        /// <summary>
        ///     check page is valid
        /// </summary>
        /// <param name="page">current page(this)</param>
        /// <param name="sender">object</param>
        /// <returns>true:is valid; false:invalid</returns>
        public static bool IsValid(Page page, object sender)
        {
            page.Validate();
            return page.IsValid;
        }

        /// <summary>
        ///     set validator's property 'Enabled', when it contains object's id
        /// </summary>
        /// <param name="page">current page(this)</param>
        /// <param name="sender">object: webcontrol</param>
        /// <returns>void</returns>
        private static void ValidMapping(Page page, object sender)
        {
            var webControl = (WebControl) sender;
            // get validators from page.
            var validators = page.Validators.GetEnumerator();
            while (validators.MoveNext())
            {
                var validator = (BaseValidator) validators.Current;
                validator.Enabled = validator.ID.IndexOf(webControl.ID + "_") != -1;
            }
        }

        /// <summary>
        ///     set validator's property 'enabled' to false
        /// </summary>
        /// <param name="page">current page(this)</param>
        /// <returns>void</returns>
        public static void ValidatorDisabled(Page page)
        {
            var validators = page.Validators.GetEnumerator();
            while (validators.MoveNext())
                ((BaseValidator) validators.Current).Enabled = false;
        }

        /// <summary>
        ///     convert string to bytes, and check bytes' length is between maxlength and minlength
        /// </summary>
        /// <param name="chkString">string need to convert</param>
        /// <param name="maxLength">max length</param>
        /// <param name="minLength">min length</param>
        /// <returns>
        ///     true:bytes' length between maxlength and minlength
        ///     false:bytes' length less than maxlength or more than minlength.
        /// </returns>
        public static bool StringToBytes(string chkString, int maxLength, int minLength)
        {
            var sjisEnc = Encoding.GetEncoding("Shift_JIS");
            // get the string's length
            var iLength = sjisEnc.GetByteCount(chkString);
            // check the length is between maxlength and minlength.
            return minLength <= iLength && iLength <= maxLength;
        }

        /// <summary>
        ///     check the string is number
        /// </summary>
        /// <param name="value">value</param>
        /// <param name="maxlen">length</param>
        /// <returns>
        ///     true:is number
        ///     false:is not number
        /// </returns>
        public static bool IsNumber(string value, int maxlen)
        {
            return value.Length <= maxlen && !Regex.IsMatch(value, "[^0-9]");
        }

        /// <summary>
        ///     check 'year','month'and 'day' dropdownlists' selectindex is not zero.
        /// </summary>
        /// <param name="ddly">DropDownList:year</param>
        /// <param name="ddlm">DropDownList:month</param>
        /// <param name="ddld">DropDownList:day</param>
        /// <returns>true:OK false:NG</returns>
        public static bool DateDropDownValidate(DropDownList ddly, DropDownList ddlm, DropDownList ddld)
        {
            var ret = false;
            if (ddly.SelectedIndex == 0 && ddlm.SelectedIndex == 0 && ddld.SelectedIndex == 0)
                ret = true;
            else if (ddly.SelectedIndex != 0 && ddlm.SelectedIndex == 0 && ddld.SelectedIndex == 0)
                ret = true;
            else if (ddly.SelectedIndex != 0 && ddlm.SelectedIndex != 0 && ddld.SelectedIndex == 0)
                ret = true;
            else if (ddly.SelectedIndex != 0 && ddlm.SelectedIndex != 0 && ddld.SelectedIndex != 0)
                ret = true;
            return ret;
        }

        /// <summary>
        ///     check string is accord with datatime type
        /// </summary>
        /// <param name="yearParam">year</param>
        /// <param name="monthParam">month</param>
        /// <param name="dateParam">day</param>
        /// <returns>
        ///     true:is datatime type; false:is not datetime type
        /// </returns>
        public static bool IsDateTimeString(string yearParam, string monthParam, string dateParam)
        {
            return IsDateTimeString(yearParam + "/" + monthParam + "/" + dateParam);
        }

        /// <summary>
        ///     check string is accord with datatime type
        /// </summary>
        /// <param name="strParam">string of datatime</param>
        /// <returns>
        ///     true:is datatime type; false:is not datetime type
        /// </returns>
        public static bool IsDateTimeString(string strParam)
        {
            try
            {
                var date = DateTime.Parse(strParam);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        /// <summary>
        ///     ÅÐ¶Ï×Ö·û´®ÖÐÊÇ·ñ°üº¬´óÐ´×ÖÄ¸
        /// </summary>
        /// <param name="loginPwd">ÒªÅÐ¶ÏµÄ×Ö·û´®</param>
        public static bool IsContainUpperCase(string loginPwd)
        {
            try
            {
                if (string.IsNullOrEmpty(loginPwd))
                    return false;
                var ZipRegex = @"^.*[A-Z]+.*$";
                if (Regex.IsMatch(loginPwd, ZipRegex))
                    return true;
                return false;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        /// <summary>
        ///     ÅÐ¶Ï×Ö·û´®ÖÐÊÇ·ñ°üº¬Ð¡Ð´×ÖÄ¸
        /// </summary>
        /// <param name="loginPwd">ÒªÅÐ¶ÏµÄ×Ö·û´®</param>
        public static bool IsContainLowerCase(string loginPwd)
        {
            try
            {
                if (string.IsNullOrEmpty(loginPwd))
                    return false;
                var ZipRegex = @"^.*[a-z]+.*$";
                if (Regex.IsMatch(loginPwd, ZipRegex))
                    return true;
                return false;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        /// <summary>
        ///     ÅÐ¶Ï×Ö·û´®ÖÐÊÇ·ñ°üº¬×ÖÄ¸(²»Çø·Ö´óÐ¡Ð´)
        /// </summary>
        /// <param name="loginPwd">ÒªÅÐ¶ÏµÄ×Ö·û´®</param>
        public static bool IsContainCase(string loginPwd)
        {
            try
            {
                if (string.IsNullOrEmpty(loginPwd))
                    return false;
                var ZipRegex = @"^.*[A-Za-z]+.*$";
                if (Regex.IsMatch(loginPwd, ZipRegex))
                    return true;
                return false;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        /// <summary>
        ///     ÅÐ¶Ï×Ö·û´®ÖÐÊÇ·ñ°üº¬Êý×Ö
        /// </summary>
        /// <param name="loginPwd">ÒªÅÐ¶ÏµÄ×Ö·û´®</param>
        public static bool IsContainNumber(string loginPwd)
        {
            try
            {
                if (string.IsNullOrEmpty(loginPwd))
                    return false;
                var ZipRegex = @"^.*[0-9]+.*$";
                if (Regex.IsMatch(loginPwd, ZipRegex))
                    return true;
                return false;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        /// <summary>
        ///     ÅÐ¶Ï×Ö·û´®ÖÐÊÇ·ñ°üº¬ÌØÊâ×Ö·û
        /// </summary>
        /// <param name="loginPwd">ÒªÅÐ¶ÏµÄ×Ö·û´®</param>
        public static bool IsContainSymbol(string loginPwd)
        {
            try
            {
                if (string.IsNullOrEmpty(loginPwd))
                    return false;
                var ZipRegex = @"^.*[^A-Za-z0-9]+.*$";
                if (Regex.IsMatch(loginPwd, ZipRegex))
                    return true;
                return false;
            }
            catch (FormatException)
            {
                return false;
            }
        }


        /// <summary>
        ///     ÓÊÏä¸ñÊ½ÑéÖ¤
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool IsEmail(string email)
        {
            return Regex.IsMatch(email,
                @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }
    }
}