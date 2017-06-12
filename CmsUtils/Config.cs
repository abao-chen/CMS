using System;
using System.Configuration;
using System.Web;

namespace CmsUtils
{
	/// <summary>
	/// Config ��ժҪ˵����
	/// </summary>
	public class Config
	{
		public Config()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

        //public static string Root = ConfigurationSettings.AppSettings["Root"].ToString();
        //public static string MetaTitle = ConfigurationSettings.AppSettings["MetaTitle"].ToString();

		//public static string importantCate = ConfigurationSettings.AppSettings["importantCate"].ToString();
		//public static string picLink = ConfigurationSettings.AppSettings["picLink"].ToString();
		//public static string picText = ConfigurationSettings.AppSettings["picText"].ToString();
		//public static int MaxUpImgSize = Int32.Parse(ConfigurationSettings.AppSettings["MaxUpImgSize"].ToString()); // �ϴ�ͼƬ��С����
		//public static string UpImgType = ConfigurationSettings.AppSettings["UpImgType"].ToString(); // �ϴ�ͼƬ��ʽ

	

		public static void SetCookies(string username)
		{
			if (HttpContext.Current.Request.Browser.Cookies == true)
			{
				if (HttpContext.Current.Request.Cookies["yinibacom"] == null)
				{
					HttpCookie newCookie = new HttpCookie("yinibacom",username);
					newCookie.Expires = DateTime.Now.AddDays(1);
					HttpContext.Current.Response.Cookies.Add(newCookie);
				} 
				else
				{
					HttpCookie newCookie = new HttpCookie("yinibacom",username);
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
					HttpCookie newCookie = new HttpCookie("yinibacom",DateTime.Now.ToShortDateString());
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
			SetCookie(username,365);
		}
		/// <summary>
		/// ��Cookie��ֵ
		/// </summary>
		/// <param name="username"></param>
		public static void SetCookie(string username,int hours)
		{
			//System.Web.HttpCookie cookie=new HttpCookie("username",username);
			System.Web.HttpCookie cookie=new HttpCookie("username" , System.Web.HttpUtility.UrlEncode(username,System.Text.Encoding.GetEncoding("gb2312")));
			cookie.Expires=DateTime.Now.AddHours(hours);
			System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
		}
		
		/// <summary>
		/// ��Cookie��ֵ
		/// </summary>
		/// <param name="username"></param>
		public static void SetCookie(string username,int hours,string cookiename)
		{
			System.Web.HttpCookie cookie=new HttpCookie(cookiename , System.Web.HttpUtility.UrlEncode(username,System.Text.Encoding.GetEncoding("gb2312")));
			cookie.Expires=DateTime.Now.AddHours(hours);
			System.Web.HttpContext.Current.Response.Cookies.Add(cookie);

	
		}

		/// <summary>
		/// ����Ƿ���Cookieֵ����������Session��ֵ
		/// </summary>
		/// <returns></returns>
		public static void GetCookie()
		{
			if(""!=Convert.ToString(System.Web.HttpContext.Current.Request.Cookies["username"]))
			{
				//System.Web.HttpContext.Current.Session["username"]=System.Web.HttpContext.Current.Request.Cookies["username"].Value;
				
				System.Web.HttpContext.Current.Session["username"]=GetMyCookie("username");
			}
		}
		/// <summary>
		/// ����Ƿ���Cookieֵ����������Session��ֵ
		/// </summary>
		/// <returns></returns>
		public static void GetCookie(string SessionName,string CookieName)
		{
			if(""!=Convert.ToString(System.Web.HttpContext.Current.Request.Cookies[CookieName]))
			{
				//System.Web.HttpContext.Current.Session[SessionName]=System.Web.HttpContext.Current.Request.Cookies[CookieName].Value;
				System.Web.HttpContext.Current.Session[SessionName]=GetMyCookie(CookieName);
			}
		}
		/// <summary>
		/// ����Ƿ���Cookieֵ����������Session��ֵ
		/// </summary>
		/// <returns></returns>
		public static void ClearCookie()
		{			
			System.Web.HttpCookie cookie=new HttpCookie("username");
			cookie.Expires=DateTime.Now.AddDays(-1);
			System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
		}
		/// <summary>
		/// ����Ƿ���Cookieֵ����������Session��ֵ
		/// </summary>
		/// <returns></returns>
		public static void ClearCookie(string CookieName)
		{			
			System.Web.HttpCookie cookie=new HttpCookie(CookieName);
			cookie.Expires=DateTime.Now.AddDays(-1);
			System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
		}

		public static string GetMyCookie(string name)
		{
			if(HttpContext.Current.Request.Cookies[name]!=null)
				return System.Web.HttpUtility.UrlDecode(HttpContext.Current.Request.Cookies[name].Value,System.Text.Encoding.GetEncoding("gb2312"));
			return "�ο�";
		}


		/// <summary>
		/// ����������ȡ�������ʱ��
		/// </summary>
		/// <param name="days">�˺���</param>
		/// <param name="lang">cn���ģ�enӢ��</param>
		/// <returns></returns>
		public static string GetDateString(int days,string lang)
		{
			if(lang.ToLower() == "en")
			{
				DateTime dt = DateTime.Now.AddDays(days);
				return dt.Month.ToString()+"-"+dt.Day.ToString();
			}
			else
			{
				DateTime dt = DateTime.Now.AddDays(days);
				return dt.Month.ToString()+"��"+dt.Day.ToString()+"��";


			}
		}

		public static string GetWeekString(int days,string lang)
		{
			if(lang.ToLower() == "en")
			{
				DateTime dt = DateTime.Now.AddDays(days);
				return dt.Day.ToString()+"("+dt.ToString("ddd", new   System.Globalization.CultureInfo("en-US"))+")";
			}
			else
			{
				DateTime dt = DateTime.Now.AddDays(days);
				string day=string.Empty;
				;
				return dt.Day.ToString()+"��("+System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(dt.DayOfWeek).Substring(2)+")";
			}
		} 


		public static string GetWeekNav(int pValue,string p2)
		{
			DateTime dt = DateTime.Now.AddDays( pValue );
			if(p2 == dt.Month.ToString()+"-"+dt.Day.ToString())
			{
				return "weekCurrDay";
			}
			else
			{
				return "weekDay";
			}
		}
		public static string GetMyNav(string pUrl)
		{
			if(System.Web.HttpContext.Current.Request.Url.ToString().ToLower().IndexOf( pUrl.ToLower() )>0)
			{
				return "myCurrBanner";
			}
			else
			{
				return "myBanner";
			}
		}

		/// <summary>
		/// ˢ�¸����壬�رմ���
		/// </summary>
		/// <param name="pFlag">TrueΪ��ӣ�FalseΪ�޸�</param>
		public static void ReloadParent(bool pFlag)
		{
			string Js;
			if(pFlag)
			{
				Js = "alert('��Ϣ��ӳɹ�!');";
			}
			else
			{
				Js = "alert('��Ϣ�޸ĳɹ�!');";
			}
			Js += "window.opener.location.reload();window.opener=null;window.close();";

			System.Web.HttpContext.Current.Response.Write("<script language='javascript'>"+Js+"</script>");
		}
        public static bool CheckValidateUser()
        {
            if (HttpContext.Current.Session["username"] != null)
            {
                return true;
            }
            if (HttpContext.Current.Request.Cookies["username"] != null)
            {
                return true;
            }
            return false;
        }
	}
}
