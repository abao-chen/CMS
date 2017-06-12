using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace CmsUtils
{
    /// <summary>
    /// ���ú���
    /// </summary>
    public class Fun
    {
        #region ҳ����ʾ-��ת

        /// <summary>
        /// ֱ�ӷ���
        /// </summary>
        public static void Back()
        {
            HttpContext.Current.Response.Write("<script language='JavaScript'>");
            HttpContext.Current.Response.Write(" history.go(-1);");
            HttpContext.Current.Response.Write("</script>");

        }

        /// <summary>
        /// ֱ�ӷ���
        /// </summary>
        public static string SubMemberCode(int Code)
        {
            return "G" + Code;
        }

        /// <summary>
        /// ��ʾ�󷵻�
        /// </summary>
        /// <param name="str"></param>
        public static void Back(string str)
        {
            HttpContext.Current.Response.Write("<script language='JavaScript'>");
            if (str != null)
            {
                HttpContext.Current.Response.Write(" alert('" + str + "');");
            }
            HttpContext.Current.Response.Write(" history.go(-1);");
            HttpContext.Current.Response.Write("</script>");

        }

        /// <summary>
        /// ��ʾ
        /// </summary>
        /// <param name="str"></param>
        public static void Alert(string str)
        {
            HttpContext.Current.Response.Write("<script language='JavaScript'>");
            HttpContext.Current.Response.Write("alert('" + str + "');");
            HttpContext.Current.Response.Write("</script/>");
        }

        public static void Alert(Page p, string pText)
        {
            p.RegisterStartupScript("message", "<script type='text/javascript'>alert('" + pText + "')</script>");
        }

        /// <summary>
        /// ҳ����ʾ
        /// </summary>
        /// <param name="str"></param>
        public static void ScriptAlert(string str)
        {
            ClientScriptManager JsManager = ((Page)HttpContext.Current.Handler).ClientScript;
            JsManager.RegisterStartupScript(JsManager.GetType(), "myscript", "<script>Alter('"+str+"');</script>");
            return;
        }

        /// <summary>
        /// ��ʾ����ת��urlҳ��
        /// </summary>
        /// <param name="str"></param>
        /// <param name="url"></param>
        public static void Alert(string str, string url)
        {
            HttpContext.Current.Response.Write("<script language='JavaScript'>");
            if (str != null)
            {
                HttpContext.Current.Response.Write(" alert('" + str + "');");
            }
            if (url != null)
            {
                HttpContext.Current.Response.Write(" location.href='" + url + "';");
            }
            HttpContext.Current.Response.Write("</script>");
        }

        /// <summary>
        /// ��ʾ��ر�
        /// </summary>
        /// <param name="str"></param>
        public static void AlertClose(string str)
        {
            HttpContext.Current.Response.Write("<script language='JavaScript'>");
            HttpContext.Current.Response.Write("alert('" + str + "');window.opener=null;window.close();");
            HttpContext.Current.Response.Write("</script/>");
        }

        #endregion

        #region �ַ�������

        /// <summary>
        /// ����Σ���ַ�
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string SafeData(string str) //�����ݹ���
        {
            string tempStr = str;
            if (tempStr != null)
            {
                tempStr = tempStr.Replace("<", "");
                tempStr = tempStr.Replace(">", "");
                tempStr = tempStr.Replace("'", "\"");
            }
            return tempStr;
        }

        /// <summary>
        /// ����Σ���ַ���HTML
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string SafeDataHTML(string str) //�����ݹ���
        {
            string tempStr = str;
            if (tempStr != null)
            {
                tempStr = tempStr.Replace("'", "''");
            }
            return tempStr;
        }

        /// <summary>
        /// �Ƴ�HTML����
        /// </summary>
        /// <param name="Htmlstring">�ַ���</param>
        /// <returns>����</returns>
        public static string RemoveHTML(string Htmlstring)
        {
            //ɾ���ű�   
            Htmlstring =
                Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //ɾ��HTML   
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", "   ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);
            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("\r\n", "");
            Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();

            return Htmlstring;
        }

        /// <summary>
        /// ��֤���ݹ����Ĳ����Ƿ�������
        /// </summary>
        /// <param name="objName"></param>
        /// <returns>trueΪ����</returns>
        public static bool RequestIsNumber(string objName)
        {
            object obj = HttpContext.Current.Request.QueryString[objName];
            if (obj != null && IsNumberR(obj.ToString()))
                return true;
            else
                return false;
        }

        /// <summary>
        /// �Ƿ�����
        /// </summary>
        /// <param name="lstr"></param>
        /// <returns></returns>
        public static bool IsNumber(string lstr)
        {
            bool isDecimal = false;
            for (int i = 0; i < lstr.Length; i++)
            {
                char ochar = lstr[i];
                if (i == 0 && ochar == '-')
                    continue;
                if (ochar == '.' && !isDecimal)
                {
                    isDecimal = true;
                    continue;
                }
                if (ochar < '0' || ochar > '9')
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// �Ƿ�����
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsInt(string str)
        {
            try
            {
                Convert.ToInt32(str);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// �Ƿ�ʱ������
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsDateTime(string str)
        {
            try
            {
                Convert.ToDateTime(str);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// ʱ��ת����SN
        /// </summary>
        /// <returns></returns>
        public static string GetSN()
        {
            string _sn = (DateTime.Now).ToString("yyyyMMddHHmm");
            _sn = _sn.Replace("-", "");
            _sn = _sn.Replace(":", "");
            _sn = _sn.Replace(" ", "");
            _sn = _sn.Replace("/", "");
            _sn += RdNum();
            return _sn;

        }
        /// <summary>
        /// �����
        /// </summary>
        static Random ra = new Random(unchecked((int)DateTime.Now.Ticks));
        protected static int RdNum()
        {
            int tmp = 0;
            int minValue = 1000;
            int maxValue = 9999;
            tmp = ra.Next(minValue, maxValue); //���ȡ��
            return tmp;
        }

        protected static byte[] C2B(string str)
        {
            char[] arrChar;
            arrChar = str.ToCharArray();
            byte[] arrByte = new byte[arrChar.Length];
            for (int i = 0; i < arrChar.Length; i++)
            {
                arrByte[i] = Convert.ToByte(arrChar[i]);
            }
            return arrByte;
        }
     
        /// <summary>
        /// md5����
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetMD5(string str)
        {
            byte[] arrHashInput;
            byte[] arrHashOutput;
            //MD5CryptoServiceProvider objMD5  = new MD5CryptoServiceProvider();
            MD5 objMD5 = new MD5CryptoServiceProvider();
            arrHashInput = C2B(str);
            arrHashOutput = objMD5.ComputeHash(arrHashInput);
            return BitConverter.ToString(arrHashOutput);
        }

        /// <summary>
        /// �ַ�����ȡ
        /// </summary>
        /// <param name="input"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string CutString(object input, int len)
        {
            string inputString = input.ToString();
            ASCIIEncoding ascii = new ASCIIEncoding();
            int tempLen = 0;
            string tempString = "";
            byte[] s = ascii.GetBytes(inputString);
            for (int i = 0; i < s.Length; i++)
            {
                if ((int) s[i] == 63)
                {
                    tempLen += 2;
                }
                else
                {
                    tempLen += 1;
                }
                try
                {
                    tempString += inputString.Substring(i, 1);
                }
                catch
                {
                    break;
                }

                if (tempLen > len)
                    break;
            }
            //����ع�����ϰ��ʡ�Ժ�   
            byte[] mybyte = Encoding.Default.GetBytes(inputString);
            if (mybyte.Length > len)
                tempString += "...";

            return tempString;
        }

        /// <summary>
        /// ���obj�Ƿ�Ϊ��
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNull(object obj)
        {
            if (obj == null)
                return true;
            else
                return false;
        }




        public static bool IsNumberR(string lstr)
        {
            bool IsNum = Regex.IsMatch(lstr, @"^\d+$");
            return IsNum;
        }

        

        public static string ShowTextArea(object str)
        {
            string _str = Convert.ToString(str);
            _str = _str.Replace("\r\n", "<br>");
            return _str;
        }

        public static void SetOption(DropDownList ddl, int s, int e, string tip)
        {
            if (s > e)
            {
                for (int i = e; i <= s; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
            }
            else
            {
                for (int i = e; i >= s; i--)
                {
                    ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
            }
            if (tip != null)
            {
                ddl.Items.Insert(0, new ListItem(tip, ""));
            }
        }

        /// <summary>
        /// ��ȡʱ���
        /// </summary>
        /// <returns></returns>
        public static string GetTimestamp()
        {
            Random ro = new Random();
            return DateTime.Now.ToString("yyyyMMddHHmmss"+ro.Next(1000000));
        }

        /// <summary>
        /// �õ��ļ���׺��
        /// </summary>
        /// <param name="fileName">�ļ�����</param>
        /// <returns></returns>
        public static string GetExName(string fileName)
        {
            int dotPos = fileName.LastIndexOf(".");
            return fileName.Substring(dotPos);
        }

        /// <summary>
        /// ��ȡ������
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Form(string key)
        {
            return HttpContext.Current.Request.Form[key];
        }
        /// <summary>
        /// ��ȡURL����
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Query(string key)
        {
            if (HttpContext.Current.Request.QueryString[key] == null)
                return "";
            else
                return HttpContext.Current.Request.QueryString[key].ToString();
        }

        /// <summary>
        /// ��ȡ�����ַ�������ĸ(���ڻ�ȡ��������)
        /// </summary>
        /// <param name="cnChar"></param>
        /// <returns></returns>
        public static string GetSpell(string cnChar)
        {
            byte[] arrCN = Encoding.Default.GetBytes(cnChar);
            if (arrCN.Length > 1)
            {
                int area = (short)arrCN[0];
                int pos = (short)arrCN[1];
                int code = (area << 8) + pos;
                int[] areacode = { 45217, 45253, 45761, 46318, 46826, 47010, 47297, 47614, 48119, 48119, 49062, 49324, 49896, 50371, 50614, 50622, 50906, 51387, 51446, 52218, 52698, 52698, 52698, 52980, 53689, 54481 };
                for (int i = 0; i < 26; i++)
                {
                    int max = 55290;
                    if (i != 25)
                        max = areacode[i + 1];
                    if (areacode[i] <= code && code < max)
                    {
                        return Encoding.Default.GetString(new byte[] { (byte)(97 + i) });
                    }
                }
                //��������Ӣ�Ļ��Ƿ��������������ﷵ��.�������� return cnChar.SubString(0,1);
                //�ж�����ĸ�Ƿ���Ӣ�� using System.Text.RegularExpressions;
                string ZhenZe = @"^[A-Za-z].*?";
                Regex obj = new Regex(ZhenZe);
                bool flag = obj.IsMatch(cnChar);
                if (flag)
                {
                    return cnChar.Substring(0, 1);
                }
                return "*";
            }
            else
                return cnChar;
        }

        /// <summary>
        /// ������������ַ���
        /// </summary>
        /// <param name="codeCount">�����ɵ�λ��</param>
        /// <returns>���ɵ������ַ���</returns>
        public static string GenerateCheckCodeNum(int codeCount)
        {
            int rep = 0;
            string str = string.Empty;
            long num2 = DateTime.Now.Ticks + rep;
            rep++;
            Random random = new Random(((int)(((ulong)num2) & 0xffffffffL)) | ((int)(num2 >> rep)));
            for (int i = 0; i < codeCount; i++)
            {
                int num = random.Next();
                str = str + ((char)(0x30 + ((ushort)(num % 10)))).ToString();
            }
            return str;
        }

        #endregion

        #region Cookies Operation

        public static void SetCookies(string username)
        {
            if (HttpContext.Current.Request.Browser.Cookies == true)
            {
                if (HttpContext.Current.Request.Cookies["yinibacom"] == null)
                {
                    HttpCookie newCookie = new HttpCookie("yinibacom", username);
                    newCookie.Expires = DateTime.Now.AddDays(1);
                    HttpContext.Current.Response.Cookies.Add(newCookie);
                }
                else
                {
                    HttpCookie newCookie = new HttpCookie("yinibacom", username);
                    newCookie.Expires = DateTime.Now.AddDays(1);
                    HttpContext.Current.Response.Cookies.Add(newCookie);
                }
            }
        }



        public static void ClearCookies()
        {
            if (HttpContext.Current.Request.Browser.Cookies == true)
            {
                if (HttpContext.Current.Request.Cookies["yinibacom"] != null)
                {
                    HttpCookie newCookie = new HttpCookie("yinibacom", DateTime.Now.ToShortDateString());
                    newCookie.Expires = DateTime.Now.AddDays(-1);
                    HttpContext.Current.Response.Cookies.Add(newCookie);

                }
            }
        }


        /// <summary>
        /// ��Cookie��ֵ
        /// </summary>
        /// <param name="username"></param>
        public static void SetCookie(string username)
        {
            SetCookie(username, 365);
        }

        /// <summary>
        /// ��Cookie��ֵ
        /// </summary>
        /// <param name="username"></param>
        public static void SetCookie(string username, int hours)
        {
            //System.Web.HttpCookie cookie=new HttpCookie("username",username);
            HttpCookie cookie = new HttpCookie("username",
                                                          HttpUtility.UrlEncode(username,
                                                                                           Encoding.
                                                                                               GetEncoding("gb2312")));
            cookie.Expires = DateTime.Now.AddHours(hours);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// ��Cookie��ֵ
        /// </summary>
        /// <param name="username"></param>
        public static void SetCookie(string username, int hours, string cookiename)
        {
            HttpCookie cookie = new HttpCookie(cookiename,
                                                          HttpUtility.UrlEncode(username,
                                                                                           Encoding.
                                                                                               GetEncoding("gb2312")));
            cookie.Expires = DateTime.Now.AddHours(hours);
            HttpContext.Current.Response.Cookies.Add(cookie);


        }

        /// <summary>
        /// ����Ƿ���Cookieֵ����������Session��ֵ
        /// </summary>
        /// <returns></returns>
        public static void GetCookie()
        {
            if ("" != Convert.ToString(HttpContext.Current.Request.Cookies["username"]))
            {
                //System.Web.HttpContext.Current.Session["username"]=System.Web.HttpContext.Current.Request.Cookies["username"].Value;

                HttpContext.Current.Session["username"] = GetMyCookie("username");
            }
        }

        /// <summary>
        /// ����Ƿ���Cookieֵ����������Session��ֵ
        /// </summary>
        /// <returns></returns>
        public static void GetCookie(string SessionName, string CookieName)
        {
            if ("" != Convert.ToString(HttpContext.Current.Request.Cookies[CookieName]))
            {
                //System.Web.HttpContext.Current.Session[SessionName]=System.Web.HttpContext.Current.Request.Cookies[CookieName].Value;
                HttpContext.Current.Session[SessionName] = GetMyCookie(CookieName);
            }
        }

        /// <summary>
        /// ����Ƿ���Cookieֵ����������Session��ֵ
        /// </summary>
        /// <returns></returns>
        public static void ClearCookie()
        {
            HttpCookie cookie = new HttpCookie("username");
            cookie.Expires = DateTime.Now.AddDays(-1);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// ����Ƿ���Cookieֵ����������Session��ֵ
        /// </summary>
        /// <returns></returns>
        public static void ClearCookie(string CookieName)
        {
            HttpCookie cookie = new HttpCookie(CookieName);
            cookie.Expires = DateTime.Now.AddDays(-1);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static string GetMyCookie(string name)
        {
            if (HttpContext.Current.Request.Cookies[name] != null)
                return HttpUtility.UrlDecode(HttpContext.Current.Request.Cookies[name].Value,
                                                        Encoding.GetEncoding("gb2312"));
            return "�ο�";
        }

        #endregion

    }

    public class PageValidate
    {
        private static Regex RegNumber = new Regex("^[0-9]+$");
        private static Regex RegNumberSign = new Regex("^[+-]?[0-9]+$");
        private static Regex RegDecimal = new Regex("^[0-9]+[.]?[0-9]+$");
        private static Regex RegDecimalSign = new Regex("^[+-]?[0-9]+[.]?[0-9]+$"); //�ȼ���^[+-]?\d+[.]?\d+$
        private static Regex RegEmail = new Regex("^[\\w-]+@[\\w-]+\\.(com|net|org|edu|mil|tv|biz|info)$");//w Ӣ����ĸ�����ֵ��ַ������� [a-zA-Z0-9] �﷨һ�� 
        private static Regex RegCHZN = new Regex("[\u4e00-\u9fa5]");

        public PageValidate()
        {
        }


        #region �����ַ������

        /// <summary>
        /// ���Request��ѯ�ַ����ļ�ֵ���Ƿ������֣���󳤶�����
        /// </summary>
        /// <param name="req">Request</param>
        /// <param name="inputKey">Request�ļ�ֵ</param>
        /// <param name="maxLen">��󳤶�</param>
        /// <returns>����Request��ѯ�ַ���</returns>
        public static string FetchInputDigit(HttpRequest req, string inputKey, int maxLen)
        {
            string retVal = string.Empty;
            if (inputKey != null && inputKey != string.Empty)
            {
                retVal = req.QueryString[inputKey];
                if (null == retVal)
                    retVal = req.Form[inputKey];
                if (null != retVal)
                {
                    retVal = SqlText(retVal, maxLen);
                    if (!IsNumber(retVal))
                        retVal = string.Empty;
                }
            }
            if (retVal == null)
                retVal = string.Empty;
            return retVal;
        }
        /// <summary>
        /// �Ƿ������ַ���
        /// </summary>
        /// <param name="inputData">�����ַ���</param>
        /// <returns></returns>
        public static bool IsNumber(string inputData)
        {
            Match m = RegNumber.Match(inputData);
            return m.Success;
        }

        /// <summary>
        /// �Ƿ������ַ��� �ɴ�������
        /// </summary>
        /// <param name="inputData">�����ַ���</param>
        /// <returns></returns>
        public static bool IsNumberSign(string inputData)
        {
            Match m = RegNumberSign.Match(inputData);
            return m.Success;
        }
        /// <summary>
        /// �Ƿ��Ǹ�����
        /// </summary>
        /// <param name="inputData">�����ַ���</param>
        /// <returns></returns>
        public static bool IsDecimal(string inputData)
        {
            Match m = RegDecimal.Match(inputData);
            return m.Success;
        }
        /// <summary>
        /// �Ƿ��Ǹ����� �ɴ�������
        /// </summary>
        /// <param name="inputData">�����ַ���</param>
        /// <returns></returns>
        public static bool IsDecimalSign(string inputData)
        {
            Match m = RegDecimalSign.Match(inputData);
            return m.Success;
        }

        #endregion

        #region ���ļ��

        /// <summary>
        /// ����Ƿ��������ַ�
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static bool IsHasCHZN(string inputData)
        {
            Match m = RegCHZN.Match(inputData);
            return m.Success;
        }

        #endregion

        #region �ʼ���ַ
        /// <summary>
        /// �Ƿ��ʼ���ַ
        /// </summary>
        /// <param name="inputData">�����ַ���</param>
        /// <returns></returns>
        public static bool IsEmail(string inputData)
        {
            Match m = RegEmail.Match(inputData);
            return m.Success;
        }

        #endregion

        #region ʱ��
        /// <summary>
        /// �Ƿ���ʱ���ʽ
        /// </summary>
        /// <param name="StrDate">ʱ���ַ���</param>
        public static bool IsDateTime(string StrDate)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(StrDate);
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region ����
        /// <summary>
        /// ����ַ�����󳤶ȣ�����ָ�����ȵĴ�
        /// </summary>
        /// <param name="sqlInput">�����ַ���</param>
        /// <param name="maxLength">��󳤶�</param>
        /// <returns></returns>			
        public static string SqlText(string sqlInput, int maxLength)
        {
            if (sqlInput != null && sqlInput != string.Empty)
            {
                sqlInput = sqlInput.Trim();
                if (sqlInput.Length > maxLength)//����󳤶Ƚ�ȡ�ַ���
                    sqlInput = sqlInput.Substring(0, maxLength);
            }
            return sqlInput;
        }


        /// <summary>
        /// �ַ�������
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static string HtmlEncode(string inputData)
        {
            return HttpUtility.HtmlEncode(inputData);
        }
        /// <summary>
        /// ����Label��ʾEncode���ַ���
        /// </summary>
        /// <param name="lbl"></param>
        /// <param name="txtInput"></param>
        public static void SetLabel(Label lbl, string txtInput)
        {
            lbl.Text = HtmlEncode(txtInput);
        }
        public static void SetLabel(Label lbl, object inputObj)
        {
            SetLabel(lbl, inputObj.ToString());
        }

        /// <summary>
        /// MD5�����㷨
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetMD5String(string str)
        {
            MD5 md5 = MD5.Create();
            byte[] b = Encoding.UTF8.GetBytes(str);
            byte[] md5b = md5.ComputeHash(b);
            md5.Clear();
            StringBuilder sb = new StringBuilder();
            foreach (var item in md5b)
            {
                sb.Append(item.ToString("x2"));
            }
            return sb.ToString();
        }

        #endregion


    }

}
