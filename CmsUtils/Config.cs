using System;
using System.Configuration;
using System.Web;

namespace CmsUtils
{
	/// <summary>
	/// Config 的摘要说明。
	/// </summary>
	public class Config
	{
		public Config()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

        //public static string Root = ConfigurationSettings.AppSettings["Root"].ToString();
        //public static string MetaTitle = ConfigurationSettings.AppSettings["MetaTitle"].ToString();

		//public static string importantCate = ConfigurationSettings.AppSettings["importantCate"].ToString();
		//public static string picLink = ConfigurationSettings.AppSettings["picLink"].ToString();
		//public static string picText = ConfigurationSettings.AppSettings["picText"].ToString();
		//public static int MaxUpImgSize = Int32.Parse(ConfigurationSettings.AppSettings["MaxUpImgSize"].ToString()); // 上传图片大小限制
		//public static string UpImgType = ConfigurationSettings.AppSettings["UpImgType"].ToString(); // 上传图片格式

	

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
		/// 给Cookie赋值
		/// </summary>
		/// <param name="username"></param>
		public static void SetCookie(string username)
		{
			SetCookie(username,365);
		}
		/// <summary>
		/// 给Cookie赋值
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
		/// 给Cookie赋值
		/// </summary>
		/// <param name="username"></param>
		public static void SetCookie(string username,int hours,string cookiename)
		{
			System.Web.HttpCookie cookie=new HttpCookie(cookiename , System.Web.HttpUtility.UrlEncode(username,System.Text.Encoding.GetEncoding("gb2312")));
			cookie.Expires=DateTime.Now.AddHours(hours);
			System.Web.HttpContext.Current.Response.Cookies.Add(cookie);

	
		}

		/// <summary>
		/// 检查是否有Cookie值，如果有则给Session赋值
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
		/// 检查是否有Cookie值，如果有则给Session赋值
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
		/// 检查是否有Cookie值，如果有则给Session赋值
		/// </summary>
		/// <returns></returns>
		public static void ClearCookie()
		{			
			System.Web.HttpCookie cookie=new HttpCookie("username");
			cookie.Expires=DateTime.Now.AddDays(-1);
			System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
		}
		/// <summary>
		/// 检查是否有Cookie值，如果有则给Session赋值
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
			return "游客";
		}


		/// <summary>
		/// 根据日期来取出后几天的时间
		/// </summary>
		/// <param name="days">退后几天</param>
		/// <param name="lang">cn中文，en英文</param>
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
				return dt.Month.ToString()+"月"+dt.Day.ToString()+"日";


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
				return dt.Day.ToString()+"日("+System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(dt.DayOfWeek).Substring(2)+")";
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
		/// 刷新父窗体，关闭窗体
		/// </summary>
		/// <param name="pFlag">True为添加，False为修改</param>
		public static void ReloadParent(bool pFlag)
		{
			string Js;
			if(pFlag)
			{
				Js = "alert('信息添加成功!');";
			}
			else
			{
				Js = "alert('信息修改成功!');";
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
