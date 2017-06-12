using System.Net.Mail;
using System.Net;

namespace CmsUtils
{
    public class EmailUtil
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="to">收件人</param>
        /// <param name="subject">邮件主题</param>
        /// <param name="body">邮件内容</param>
        public static void SendEmail(string to, string subject, string body)
        {
            string userName = ConfigUtilify.GetEmailUserName();
            string password = ConfigUtilify.GetEmailPassword();
            string host = ConfigUtilify.GetEmailHost();
            int port = ConfigUtilify.GetEmailPort();
            //如果未配置,直接跳过
            if (string.IsNullOrEmpty(to) || string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(host) || port == 0)
                return;
            SmtpClient client = new SmtpClient(host, port)
                                    {
                                        EnableSsl = false,
                                        UseDefaultCredentials = false,
                                        Credentials = new NetworkCredential(userName, password),
                                        DeliveryMethod = SmtpDeliveryMethod.Network
                                    };
            var msg = new MailMessage(userName, to, subject, body) { BodyEncoding = System.Text.Encoding.UTF8, IsBodyHtml = true };
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
            string userName = ConfigUtilify.GetEmailUserName();
            string password = ConfigUtilify.GetEmailPassword();
            string host = ConfigUtilify.GetEmailHost();
            int port = ConfigUtilify.GetEmailPort();
            //如果未配置,直接跳过
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(host) || port == 0)
                return;
            SmtpClient client = new SmtpClient(host, port)
            {
                EnableSsl = false,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(userName, password),
                DeliveryMethod = SmtpDeliveryMethod.Network
            };
            var msg = new MailMessage(userName, to, subject, body) { BodyEncoding = System.Text.Encoding.UTF8, IsBodyHtml = true };
            client.Send(msg);
        }
    }
}
