using System.Net.Mail;
using System.Net;
using System.Web.Configuration;

namespace CmsUtils
{
    public static class EmailUtil
    {
        private static readonly string UserName = WebConfigurationManager.AppSettings["EmailUserName"];
        private static readonly string Password = WebConfigurationManager.AppSettings["EmailPassword"];
        private static readonly string Host = WebConfigurationManager.AppSettings["EmailHost"];
        private static readonly int Port = int.Parse(WebConfigurationManager.AppSettings["EmailPort"]);

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="to">收件人</param>
        /// <param name="subject">邮件主题</param>
        /// <param name="body">邮件内容</param>
        public static void SendEmail(string to, string subject, string body)
        {
            //如果未配置,直接跳过
            if (string.IsNullOrEmpty(to) || string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Host) || Port == 0)
                return;
            SmtpClient client = new SmtpClient(Host, Port)
            {
                EnableSsl = false,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(UserName, Password),
                DeliveryMethod = SmtpDeliveryMethod.Network
            };
            var msg = new MailMessage(UserName, to, subject, body) { BodyEncoding = System.Text.Encoding.UTF8, IsBodyHtml = true };
            client.Send(msg);
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="to">收件人</param>
        /// <param name="subject">邮件主题</param>
        /// <param name="body">邮件内容</param>
        /// <param name="type">类型（预约提交，订单提交，用户注册）</param>
        /// <param name="refId">关联Id</param>
        public static void SendEmail(string to, string subject, string body, int type, int refId)
        {
            //如果未配置,直接跳过
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Host) || Port == 0)
                return;
            SmtpClient client = new SmtpClient(Host, Port)
            {
                EnableSsl = false,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(UserName, Password),
                DeliveryMethod = SmtpDeliveryMethod.Network
            };
            var msg = new MailMessage(UserName, to, subject, body) { BodyEncoding = System.Text.Encoding.UTF8, IsBodyHtml = true };
            client.Send(msg);
        }
    }
}
