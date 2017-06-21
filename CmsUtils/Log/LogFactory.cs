using System;
using System.IO;
using System.Web;
using log4net;
using log4net.Config;

namespace CmsUtils
{
    public class LogFactory
    {
        static LogFactory()
        {
            var configFile = new FileInfo(HttpContext.Current.Server.MapPath("/Configs/log4net.config"));
            XmlConfigurator.Configure(configFile);
        }

        public static Log GetLogger(Type type)
        {
            return new Log(LogManager.GetLogger(type));
        }

        public static Log GetLogger(string str)
        {
            return new Log(LogManager.GetLogger(str));
        }
    }
}