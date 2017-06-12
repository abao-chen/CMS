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
    /// 常用函数
    /// </summary>
    public class Fun
    {
        #region 页面提示-跳转

        /// <summary>
        /// 直接返回
        /// </summary>
        public static void Back()
        {
            HttpContext.Current.Response.Write("<script language='JavaScript'>");
            HttpContext.Current.Response.Write(" history.go(-1);");
            HttpContext.Current.Response.Write("</script>");

        }

        /// <summary>
        /// 直接返回
        /// </summary>
        public static string SubMemberCode(int Code)
        {
            return "G" + Code;
        }

        /// <summary>
        /// 提示后返回
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
        /// 提示
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
        /// 页面提示
        /// </summary>
        /// <param name="str"></param>
        public static void ScriptAlert(string str)
        {
            ClientScriptManager JsManager = ((Page)HttpContext.Current.Handler).ClientScript;
            JsManager.RegisterStartupScript(JsManager.GetType(), "myscript", "<script>Alter('"+str+"');</script>");
            return;
        }

        /// <summary>
        /// 提示后跳转到url页面
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
        /// 提示后关闭
        /// </summary>
        /// <param name="str"></param>
        public static void AlertClose(string str)
        {
            HttpContext.Current.Response.Write("<script language='JavaScript'>");
            HttpContext.Current.Response.Write("alert('" + str + "');window.opener=null;window.close();");
            HttpContext.Current.Response.Write("</script/>");
        }

        #endregion

        #region 字符串处理

        /// <summary>
        /// 过滤危险字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string SafeData(string str) //　数据过滤
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
        /// 过滤危险字符，HTML
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string SafeDataHTML(string str) //　数据过滤
        {
            string tempStr = str;
            if (tempStr != null)
            {
                tempStr = tempStr.Replace("'", "''");
            }
            return tempStr;
        }

        /// <summary>
        /// 移除HTML代码
        /// </summary>
        /// <param name="Htmlstring">字符串</param>
        /// <returns>返回</returns>
        public static string RemoveHTML(string Htmlstring)
        {
            //删除脚本   
            Htmlstring =
                Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML   
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
        /// 验证传递过来的参数是否是数字
        /// </summary>
        /// <param name="objName"></param>
        /// <returns>true为数字</returns>
        public static bool RequestIsNumber(string objName)
        {
            object obj = HttpContext.Current.Request.QueryString[objName];
            if (obj != null && IsNumberR(obj.ToString()))
                return true;
            else
                return false;
        }

        /// <summary>
        /// 是否数字
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
        /// 是否整数
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
        /// 是否时间类型
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
        /// 时间转换成SN
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
        /// 随机数
        /// </summary>
        static Random ra = new Random(unchecked((int)DateTime.Now.Ticks));
        protected static int RdNum()
        {
            int tmp = 0;
            int minValue = 1000;
            int maxValue = 9999;
            tmp = ra.Next(minValue, maxValue); //随机取数
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
        /// md5加密
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
        /// 字符串截取
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
            //如果截过则加上半个省略号   
            byte[] mybyte = Encoding.Default.GetBytes(inputString);
            if (mybyte.Length > len)
                tempString += "...";

            return tempString;
        }

        /// <summary>
        /// 检测obj是否为空
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
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public static string GetTimestamp()
        {
            Random ro = new Random();
            return DateTime.Now.ToString("yyyyMMddHHmmss"+ro.Next(1000000));
        }

        /// <summary>
        /// 得到文件后缀名
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <returns></returns>
        public static string GetExName(string fileName)
        {
            int dotPos = fileName.LastIndexOf(".");
            return fileName.Substring(dotPos);
        }

        /// <summary>
        /// 获取表单参数
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Form(string key)
        {
            return HttpContext.Current.Request.Form[key];
        }
        /// <summary>
        /// 获取URL参数
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
        /// 获取中文字符串首字母(存在获取错误的情况)
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
                //传进来的英文或是繁体的中文则从这里返回.或者这与 return cnChar.SubString(0,1);
                //判断首字母是否是英文 using System.Text.RegularExpressions;
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
        /// 生成随机数字字符串
        /// </summary>
        /// <param name="codeCount">待生成的位数</param>
        /// <returns>生成的数字字符串</returns>
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
        /// 给Cookie赋值
        /// </summary>
        /// <param name="username"></param>
        public static void SetCookie(string username)
        {
            SetCookie(username, 365);
        }

        /// <summary>
        /// 给Cookie赋值
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
        /// 给Cookie赋值
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
        /// 检查是否有Cookie值，如果有则给Session赋值
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
        /// 检查是否有Cookie值，如果有则给Session赋值
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
        /// 检查是否有Cookie值，如果有则给Session赋值
        /// </summary>
        /// <returns></returns>
        public static void ClearCookie()
        {
            HttpCookie cookie = new HttpCookie("username");
            cookie.Expires = DateTime.Now.AddDays(-1);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// 检查是否有Cookie值，如果有则给Session赋值
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
            return "游客";
        }

        #endregion

    }

    public class PageValidate
    {
        private static Regex RegNumber = new Regex("^[0-9]+$");
        private static Regex RegNumberSign = new Regex("^[+-]?[0-9]+$");
        private static Regex RegDecimal = new Regex("^[0-9]+[.]?[0-9]+$");
        private static Regex RegDecimalSign = new Regex("^[+-]?[0-9]+[.]?[0-9]+$"); //等价于^[+-]?\d+[.]?\d+$
        private static Regex RegEmail = new Regex("^[\\w-]+@[\\w-]+\\.(com|net|org|edu|mil|tv|biz|info)$");//w 英文字母或数字的字符串，和 [a-zA-Z0-9] 语法一样 
        private static Regex RegCHZN = new Regex("[\u4e00-\u9fa5]");

        public PageValidate()
        {
        }


        #region 数字字符串检查

        /// <summary>
        /// 检查Request查询字符串的键值，是否是数字，最大长度限制
        /// </summary>
        /// <param name="req">Request</param>
        /// <param name="inputKey">Request的键值</param>
        /// <param name="maxLen">最大长度</param>
        /// <returns>返回Request查询字符串</returns>
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
        /// 是否数字字符串
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsNumber(string inputData)
        {
            Match m = RegNumber.Match(inputData);
            return m.Success;
        }

        /// <summary>
        /// 是否数字字符串 可带正负号
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsNumberSign(string inputData)
        {
            Match m = RegNumberSign.Match(inputData);
            return m.Success;
        }
        /// <summary>
        /// 是否是浮点数
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsDecimal(string inputData)
        {
            Match m = RegDecimal.Match(inputData);
            return m.Success;
        }
        /// <summary>
        /// 是否是浮点数 可带正负号
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsDecimalSign(string inputData)
        {
            Match m = RegDecimalSign.Match(inputData);
            return m.Success;
        }

        #endregion

        #region 中文检测

        /// <summary>
        /// 检测是否有中文字符
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static bool IsHasCHZN(string inputData)
        {
            Match m = RegCHZN.Match(inputData);
            return m.Success;
        }

        #endregion

        #region 邮件地址
        /// <summary>
        /// 是否邮件地址
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsEmail(string inputData)
        {
            Match m = RegEmail.Match(inputData);
            return m.Success;
        }

        #endregion

        #region 时间
        /// <summary>
        /// 是否是时间格式
        /// </summary>
        /// <param name="StrDate">时间字符串</param>
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

        #region 其他
        /// <summary>
        /// 检查字符串最大长度，返回指定长度的串
        /// </summary>
        /// <param name="sqlInput">输入字符串</param>
        /// <param name="maxLength">最大长度</param>
        /// <returns></returns>			
        public static string SqlText(string sqlInput, int maxLength)
        {
            if (sqlInput != null && sqlInput != string.Empty)
            {
                sqlInput = sqlInput.Trim();
                if (sqlInput.Length > maxLength)//按最大长度截取字符串
                    sqlInput = sqlInput.Substring(0, maxLength);
            }
            return sqlInput;
        }


        /// <summary>
        /// 字符串编码
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static string HtmlEncode(string inputData)
        {
            return HttpUtility.HtmlEncode(inputData);
        }
        /// <summary>
        /// 设置Label显示Encode的字符串
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
        /// MD5加密算法
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
