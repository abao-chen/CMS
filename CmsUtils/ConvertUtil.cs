using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.IO;

namespace CmsUtils
{
    /// <summary>
    /// The class to be used to convert object.
    /// </summary>
    public class ConvertUtil
    {

        /// <summary>
        /// Last day
        /// </summary>
        private const string ONE_DAY = "001";
        /// <summary>
        /// Three days ago
        /// </summary>
        private const string THREE_DAYS = "002";
        /// <summary>
        /// one week ago
        /// </summary>
        private const string ONE_WEEK = "003";
        /// <summary>
        /// one month ago
        /// </summary>
        private const string ONE_MONTH = "004";
        /// <summary>
        /// three months ago
        /// </summary>
        private const string THREE_MONTHS = "005";
        /// <summary>
        ///  negative one
        /// </summary>
        private const int NEGATIVE_ONE = -1;
        /// <summary>
        /// negative three
        /// </summary>
        private const int NEGATIVE_THREE = -3;
        /// <summary>
        /// negative seven
        /// </summary>
        private const int NEGATIVE_SEVEN = -7;
        /// <summary>
        /// date format
        /// </summary>
        private const string DATE_FORMAT = "yyyy/MM/dd";

        /// <summary>
        /// The method to be used to convert the ID to startdate  
        /// </summary>
        /// <param name="lsSelectedID">StartDateID</param>
        /// <returns>Start date, format : yyyy/Mm/dd</returns>
        public static string ConvertToStartDate(string lsSelectedID)
        {
            // initial the startdate
            string lsStartDate = DateTime.Today.ToString(DATE_FORMAT);
            switch (lsSelectedID)
            {
                // one day ago
                case ONE_DAY:
                    lsStartDate = DateTime.Today.AddDays(NEGATIVE_ONE).ToString(DATE_FORMAT);
                    break;
                // three days ago
                case THREE_DAYS:
                    lsStartDate = DateTime.Today.AddDays(NEGATIVE_THREE).ToString(DATE_FORMAT);
                    break;
                // one week ago
                case ONE_WEEK:
                    lsStartDate = DateTime.Today.AddDays(NEGATIVE_SEVEN).ToString(DATE_FORMAT);
                    break;
                // one month ago
                case ONE_MONTH:
                    lsStartDate = DateTime.Today.AddMonths(NEGATIVE_ONE).ToString(DATE_FORMAT);
                    break;
                // three months ago
                case THREE_MONTHS:
                    lsStartDate = DateTime.Today.AddMonths(NEGATIVE_THREE).ToString(DATE_FORMAT);
                    break;
                default:
                    break;
            }

            return lsStartDate;
        }

        /// <summary>
        /// The method to be used to convert the ID to startdate  
        /// </summary>
        /// <param name="lsSelectedID">StartDateID</param>
        /// <param name="lsDateFormat">Date format</param>
        /// <returns>Start date, format : yyyy/Mm/dd</returns>
        public static string ConvertToStartDate(string lsSelectedID, string lsDateFormat)
        {
            // initial the startdate
            string lsStartDate = DateTime.Today.ToString(lsDateFormat);
            switch (lsSelectedID)
            {
                // one day ago
                case ONE_DAY:
                    lsStartDate = DateTime.Today.AddDays(NEGATIVE_ONE).ToString(lsDateFormat);
                    break;
                // three days ago
                case THREE_DAYS:
                    lsStartDate = DateTime.Today.AddDays(NEGATIVE_THREE).ToString(lsDateFormat);
                    break;
                // one week ago
                case ONE_WEEK:
                    lsStartDate = DateTime.Today.AddDays(NEGATIVE_SEVEN).ToString(lsDateFormat);
                    break;
                // one month ago
                case ONE_MONTH:
                    lsStartDate = DateTime.Today.AddMonths(NEGATIVE_ONE).ToString(lsDateFormat);
                    break;
                // three months ago
                case THREE_MONTHS:
                    lsStartDate = DateTime.Today.AddMonths(NEGATIVE_THREE).ToString(lsDateFormat);
                    break;
                default:
                    break;
            }

            return lsStartDate;
        }

        /// <summary>
        /// Convert image to blob
        /// </summary>
        /// <param name="lfuFile">fileupload control</param>
        /// <returns>blob object</returns>
        public static object ConvertImageToBlob(FileUpload lfuFile)
        {
            // create variable, record length of the file.
            int lnFileLen = lfuFile.PostedFile.ContentLength;
            // save bytes.
            byte[] lbInput = new byte[lnFileLen];
            // get upload file. 
            Stream lImageStream = lfuFile.PostedFile.InputStream;
            // read file to bytes.
            lImageStream.Read(lbInput, 0, lnFileLen);

            return lbInput;
        }

        /// <summary>
        /// Get Absoluteness url
        /// </summary>
        /// <param name="lsURL"></param>
        /// <returns></returns>
        public static string ConvertAbsolutenessURL(object loURL)
        {
            string lsAbltURL = string.Empty;
            if (loURL != null)
            {
                //如果地址以“http://”或“https://”开头
                if (loURL.ToString().ToLower().StartsWith("http://") || loURL.ToString().ToLower().StartsWith("https://"))
                {
                    //直接赋字段里的值
                    lsAbltURL = loURL.ToString().Trim();
                }
                //如果地址不以“http://”或“https://”开头，也不为空
                else if (!string.IsNullOrEmpty(loURL.ToString()))
                {
                    //赋“http://”加上字段里的值
                    lsAbltURL = "http://" + loURL.ToString().Trim();
                }
            }
            return lsAbltURL;
        }
        
        /// <summary>
        /// Init link
        /// </summary>
        /// <returns></returns>
        public static string InitLink(string lsJobIntro)
        {
            //Get html code
            string lsHTMLCode = TransferHTMLCode(lsJobIntro);
            //tag and link content
            string lsTagAndContent = string.Empty;
            //just link content
            string lsContent = string.Empty;
            //last link string
            string lsLink = string.Empty;
            //get the index of "[link]"
            int lnStartIndex = lsHTMLCode.IndexOf("[link]", 0);
            //get the index of "[/link]"
            int lnEndIndex = 0;
            if (lnStartIndex != -1)
            {
                lnEndIndex = lsHTMLCode.IndexOf("[/link]", lnStartIndex);
            }
            while (lnStartIndex != -1 && lnEndIndex != -1)
            {
                //tag and link content
                lsTagAndContent = lsHTMLCode.Substring(lnStartIndex, lnEndIndex - lnStartIndex + 7);
                //just link content
                lsContent = lsHTMLCode.Substring(lnStartIndex + 6, lnEndIndex - lnStartIndex - 6);
                //last link string
                lsLink = "<a href=\"" + ConvertAbsolutenessURL(lsContent) + "\" target= \"_blank\">"
                        + lsContent + "</a>";
                //Replace the input link string to html link
                lsHTMLCode = lsHTMLCode.Substring(0, lnStartIndex) + lsLink
                           + lsHTMLCode.Substring(lnEndIndex + 7);
                //get the next index of "[link]"
                lnStartIndex = lsHTMLCode.IndexOf("[link]", lnStartIndex + lsLink.Length);
                //get the next index of "[/link]"
                if (lnStartIndex != -1)
                {
                    lnEndIndex = lsHTMLCode.IndexOf("[/link]", lnStartIndex);
                }
            }
            return lsHTMLCode;
        }

        /// <summary>
        /// Transfer Html Code
        /// </summary>
        /// <param name="lsCode"></param>
        /// <returns></returns>
        public static string TransferHTMLCode(string lsCode)
        {
            try
            {
                if (lsCode != null)
                {
                    lsCode = lsCode.Replace(" ", "&nbsp;&nbsp;");
                    lsCode = lsCode.Replace("　", "&nbsp;&nbsp;&nbsp;&nbsp;");
                    lsCode = lsCode.Replace("\r\n", "<br>");
                    return lsCode;
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}
