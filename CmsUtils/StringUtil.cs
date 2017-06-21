using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Configuration;

namespace CmsUtils
{
    /// <summary>
    ///     The class to be used to process strings.
    /// </summary>
    public class StringUtil
    {
        private const int SINGLE = 1;
        private const int DOUBLE = 2;

        /// <summary>
        ///     The method to be used Replace the {n} strings with the specified string collection
        /// </summary>
        /// <param name="str">The string before replacing</param>
        /// <param name="sPara">Replaced string cellection</param>
        /// <returns>string after repalced</returns>
        /// <remarks>
        ///     ex. str = This is a {0} test message. sPara={"sample"} return value = This
        ///     is a sample test message.
        /// </remarks>
        public static string Replace(string str, string[] sPara)
        {
            var lsTmp = "";
            var li = 0;
            if (str != null && sPara != null)
            {
                foreach (var lsPara in sPara)
                {
                    if (lsPara != null)
                    {
                        lsTmp = "{" + li + "}";
                        str = str.Replace(lsTmp, lsPara);
                    }
                    li++;
                }

                return str;
            }
            return string.Empty;
        }

        /// <summary>
        ///     check if the object is null
        /// </summary>
        /// <param name="objParam">object</param>
        /// <returns>
        ///     true:if the object is null. false: if the object is not null
        /// </returns>
        public static bool IsNull(object objParam)
        {
            // if the object is null
            if (objParam == null)
                return true;
            // if the object is empty
            if (IsNull(objParam.ToString()))
                return true;

            return false;
        }

        /// <summary>
        ///     check if the string is empty
        /// </summary>
        /// <param name="param">string</param>
        /// <returns>
        ///     true: if the string is empty. false: if the string is not empty
        /// </returns>
        public static bool IsNull(string param)
        {
            // if the string is null
            if (param == null)
                return true;
            // if the string is empty
            if (param.Equals(string.Empty))
                return true;
            // if the string is empty string
            if (param.Equals(""))
                return true;

            return false;
        }

        /// <summary>
        ///     split a string to string array with a character
        /// </summary>
        /// <param name="lsStr">string to be splitted</param>
        /// <param name="lnFlag">flag for splitting</param>
        /// <returns>the string array after splitted</returns>
        public static string[] SplitStringByChar(string lsStr, int lnFlag)
        {
            // if the flag is single
            if (lnFlag == SINGLE)
            {
                // split the string with '|' and save to string array
                var lsResult = lsStr.Split('|');
                return lsResult;
            }
            // if the flag is double
            if (lnFlag == DOUBLE)
            {
                // split the string with '|' and save to a temporary string array
                var lsTmp = lsStr.Split('|');
                // calculate the half of the array size
                var n = lsTmp.Length / 2 * 2;
                // create a string array
                var lsResult = new string[n / 2];
                var i = 0;
                // save the data to the array from temporary array
                for (var j = 0; j < n; j = j + 2)
                {
                    lsResult[i] = lsTmp[j] + "|" + lsTmp[j + 1];
                    i++;
                }
                return lsResult;
            }
            return null;
        }

        /// <summary>
        ///     split a string to string array with a character
        /// </summary>
        /// <param name="lsStr">string to be splitted</param>
        /// <param name="lnCount">return how many elements of array</param>
        /// <param name="lsChar">character used in splitting</param>
        /// <param name="lbSplitOptions">need empty elements or not</param>
        /// <returns>the string array after splitted</returns>
        public static string[] SplitStringByString(string lsStr, int lnCount, string lsChar, bool lbDeleteEmptyElements)
        {
            var lsProAndCom = new string[lnCount];
            if (lsStr != null)
                if (lsStr.StartsWith(lsChar))
                {
                    lsProAndCom[0] = string.Empty;
                    lsProAndCom[1] = lsStr.Substring(lsChar.Length);
                }
                else if (lsStr.EndsWith(lsChar))
                {
                    var lsInTro = lsStr.Substring(0, lsStr.LastIndexOf(lsChar));
                    var lnSubIndex = lsInTro.IndexOf(lsChar);
                    if (lnSubIndex == -1)
                    {
                        lsProAndCom[0] = lsStr.Substring(0, lsStr.LastIndexOf(lsChar));
                        lsProAndCom[1] = string.Empty;
                    }
                    else
                    {
                        lsProAndCom[0] = lsStr.Substring(0, lsStr.IndexOf(lsChar));
                        lsProAndCom[1] = lsStr.Substring(lsStr.IndexOf(lsChar) + lsChar.Length);
                    }
                }
                else
                {
                    if (lbDeleteEmptyElements)
                        lsProAndCom = lsStr.Split(new[] {lsChar}, lnCount, StringSplitOptions.RemoveEmptyEntries);
                    else
                        lsProAndCom = lsStr.Split(new[] {lsChar}, lnCount, StringSplitOptions.None);
                }
            return lsProAndCom;
        }

        /// <summary>
        ///     split a string to string array with a character
        /// </summary>
        /// <param name="lsStr">string to be splitted</param>
        /// <param name="lnFlag">flag for splitting</param>
        /// <param name="lcChar">character used in splitting</param>
        /// <returns>the string array after splitted</returns>
        public static string[] SplitStringByChar(string lsStr, int lnFlag, char lcChar)
        {
            // if the flag is single
            if (lnFlag == 1)
            {
                // split the string with lcChar and save to string array
                var lsResult = lsStr.Split(lcChar);
                return lsResult;
            }
            // if the flag is double
            if (lnFlag == 2)
            {
                // split the string with lcChar and save to a temporary string array
                var lsTmp = lsStr.Split(lcChar);
                // calculate the half of the array size
                var n = lsTmp.Length / 2 * 2;
                // create a string array
                var lsResult = new string[n / 2];
                var i = 0;
                // save the data to the array from temporary array
                for (var j = 0; j < n; j = j + 2)
                {
                    lsResult[i] = lsTmp[j] + lcChar.ToString().Trim() + lsTmp[j + 1];
                    i++;
                }
                return lsResult;
            }
            return null;
        }

        /// <summary>
        ///     DBC case convert to CBC case.
        ///     SBC 'space' code is 12288, DBC 'space' code is 32
        ///     Other SBC characters (65281 - 65374), other DBC characters (33- 126)
        ///     Difference is 65248.
        /// </summary>
        /// <param name="lsInput">input string</param>
        /// <returns>SBC string</returns>
        public static string ToSBC(string lsInput)
        {
            var lsC = lsInput.ToCharArray();

            for (var i = 0; i < lsC.Length; i++)
            {
                if (lsC[i] == 32)
                {
                    lsC[i] = (char) 12288;
                    continue;
                }
                if (lsC[i] < 127)
                    lsC[i] = (char) (lsC[i] + 65248);
            }

            return new string(lsC);
        }

        /// <summary>
        ///     SBC case convert to DBC case.
        ///     SBC 'space' code is 12288, DBC 'space' code is 32
        ///     Other SBC characters (65281 - 65374), other DBC characters (33- 126)
        ///     Difference is 65248.
        /// </summary>
        /// <param name="lsInput">input string</param>
        /// <returns>DBC string</returns>
        public static string ToDBC(string lsInput)
        {
            var lsC = lsInput.ToCharArray();

            for (var i = 0; i < lsC.Length; i++)
            {
                if (lsC[i] == 12288)
                {
                    lsC[i] = (char) 32;
                    continue;
                }
                if (lsC[i] > 65280 && lsC[i] < 65375)
                    lsC[i] = (char) (lsC[i] - 65248);
            }

            return new string(lsC);
        }

        /// <summary>
        ///     Cut String
        /// </summary>
        /// <param name="lsInputString">InputString</param>
        /// <param name="lsMaxLength">MaxLength</param>
        /// <param name="lsLastLength">LastLength</param>
        /// <returns>CutString</returns>
        public static string CutString(string lsInputString, int lsMaxLength, int lsLastLength)
        {
            // initialize TempString
            var lsTempString = string.Empty;
            // initialize LastString
            var lsLastString = string.Empty;

            // if InputString is null, return empty
            if (lsInputString == null)
                return string.Empty;

            // if MaxLength is not lesser than length of InputString
            if (lsMaxLength >= lsInputString.Length)
                return lsInputString;

            // if LastLength is not lesser than lsMaxLength, return empty
            if (lsLastLength >= lsMaxLength)
                return string.Empty;

            // get TempString
            lsTempString = lsInputString.Substring(0, lsMaxLength - lsLastLength - 1) + "...";

            // get LastString
            lsLastString = lsInputString.Substring(lsInputString.Length - lsLastLength, lsLastLength);

            // get result
            //lsTempString += Constants.THREE_POINT + lsLastString;

            // return result
            return lsTempString;
        }

        /// <summary>
        ///     Check list have data
        /// </summary>
        /// <param name="loList">List including data</param>
        /// <returns></returns>
        public static bool HaveData(IList loList)
        {
            var loEnumerator = loList.GetEnumerator();

            while (loEnumerator.MoveNext())
                if (!string.IsNullOrEmpty(Convert.ToString(loEnumerator.Current).Trim()))
                    return true;
            return false;
        }

        /// <summary>
        ///     Check all elements are false
        /// </summary>
        /// <param name="loList"></param>
        public static bool WholeFalse(IList loList)
        {
            var loEnumerator = loList.GetEnumerator();

            while (loEnumerator.MoveNext())
                if ("true".Equals(Convert.ToString(loEnumerator.Current).ToLower()))
                    return false;
            return true;
        }

        /// <summary>
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string[] SplitString(string str)
        {
            if (string.IsNullOrEmpty(str))
                return null;
            if (string.IsNullOrEmpty(str.TrimEnd(',')))
                return null;
            str = str.TrimEnd(',');
            return str.Split(',');
        }

        /// <summary>
        ///     转换成List
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static List<string> SplitStringToList(string str)
        {
            var args = SplitString(str);
            var list = new List<string>();
            if (args == null)
                return list;
            for (var i = 0; i < args.Length; i++)
                list.Add(args[i]);
            return list;
        }

        /// <summary>
        ///     获取资讯详情
        /// </summary>
        /// <param name="img">资讯详情</param>
        /// <returns></returns>
        public static string GetInfomationPath(string img)
        {
            return WebConfigurationManager.AppSettings["InfoMation"] + img;
        }

        /// <summary>
        ///     获取资讯详情
        /// </summary>
        /// <param name="img">资讯详情</param>
        /// <returns></returns>
        public static string GetHealthNewsList()
        {
            return WebConfigurationManager.AppSettings["HealthNewsList"];
        }

        /// <summary>
        ///     获取资讯详情
        /// </summary>
        /// <param name="img">资讯详情</param>
        /// <returns></returns>
        public static string GetNewsLetterInfomationPath(string img)
        {
            return WebConfigurationManager.AppSettings["NewsLetterInfoMation"] + img;
        }

        /// <summary>
        ///     获取资讯详情
        /// </summary>
        /// <param name="img">资讯详情</param>
        /// <returns></returns>
        public static string GetHealthNewsLetterDetailUrl()
        {
            return WebConfigurationManager.AppSettings["NewsLetterHealthNewsList"];
        }

        /// <summary>
        ///     获取配置Value
        /// </summary>
        /// <param name="img">节点名称</param>
        /// <returns></returns>
        public static string GetConfigurationValue(string str)
        {
            return WebConfigurationManager.AppSettings[str];
        }

        /// <summary>
        ///     获取密码显示****
        /// </summary>
        /// <param name="passwordLength"></param>
        /// <returns></returns>
        public static string GetDisplayPassword(int passwordLength)
        {
            var displayPwd = string.Empty;
            for (var i = 0; i < passwordLength; i++)
                displayPwd += "*";
            return displayPwd;
        }
    }
}