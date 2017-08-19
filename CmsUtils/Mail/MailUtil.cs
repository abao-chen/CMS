using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading;

namespace CmsUtils
{
    public class MailUtil
    {
        /// <summary>
        ///     邮件服务器地址
        /// </summary>
        private string _mailServer { get; set; }

        /// <summary>
        ///     用户名
        /// </summary>
        private string _mailUserName { get; set; }

        /// <summary>
        ///     密码
        /// </summary>
        private string _mailPassword { get; set; }

        /// <summary>
        ///     名称
        /// </summary>
        private string _mailName { get; set; }

        /// <summary>
        ///     端口号
        /// </summary>
        private int _mailPort { get; set; }

        public MailUtil(string mailServer, string mailUserName, string mailPassword, string mailName, int mailPort)
        {
            this._mailServer = mailServer;
            this._mailName = mailName;
            this._mailUserName = mailUserName;
            this._mailPassword = mailPassword;
            this._mailPort = mailPort;
        }

        /// <summary>
        ///     同步发送邮件
        /// </summary>
        /// <param name="to">收件人邮箱地址</param>
        /// <param name="subject">主题</param>
        /// <param name="body">内容</param>
        /// <param name="cc">cc</param>
        /// <param name="encoding">编码</param>
        /// <param name="attachFile">附件地址</param>
        /// <param name="isBodyHtml">是否Html</param>
        /// <param name="enableSsl">是否SSL加密连接</param>
        /// <returns>是否成功</returns>
        public bool Send(string to, string subject, string body, string cc = "", string encoding = "UTF-8", string attachFile = "", bool isBodyHtml = true,
            bool enableSsl = false)
        {
            try
            {
                var message = new MailMessage();
                // 接收人邮箱地址
                message.To.Add(to.Replace("；", ",").Replace(";", ",").Replace("，", ","));
                if (!cc.IsEmpty())
                {
                    message.CC.Add(cc.Replace("；", ",").Replace(";", ",").Replace("，", ","));
                }
                if (!attachFile.IsEmpty() && FileUtil.IsExistFile(attachFile))
                {
                    message.Attachments.Add(new Attachment(attachFile));
                }

                message.From = new MailAddress(_mailUserName, _mailName);
                message.BodyEncoding = Encoding.GetEncoding(encoding);
                message.Body = body;
                //GB2312
                message.SubjectEncoding = Encoding.GetEncoding(encoding);
                message.Subject = subject;
                message.IsBodyHtml = isBodyHtml;

                var smtpclient = new SmtpClient(_mailServer, _mailPort);
                //SSL连接
                smtpclient.UseDefaultCredentials = true;
                smtpclient.EnableSsl = enableSsl;
                smtpclient.Credentials = new NetworkCredential(_mailUserName, _mailPassword);
                smtpclient.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        ///     异步发送邮件 独立线程
        /// </summary>
        /// <param name="to">邮件接收人</param>
        /// <param name="title">邮件标题</param>
        /// <param name="body">邮件内容</param>
        /// <param name="port">端口号</param>
        /// <returns></returns>
        public void SendByThread(string to, string title, string body)
        {
            new Thread(new ThreadStart(delegate ()
            {
                try
                {
                    SmtpClient smtp = new SmtpClient();
                    //邮箱的smtp地址
                    smtp.Host = _mailServer;
                    //端口号
                    smtp.Port = _mailPort;
                    //构建发件人的身份凭据类
                    smtp.Credentials = new NetworkCredential(_mailUserName, _mailPassword);
                    //构建消息类
                    MailMessage objMailMessage = new MailMessage();
                    //设置优先级
                    objMailMessage.Priority = MailPriority.High;
                    //消息发送人
                    objMailMessage.From = new MailAddress(_mailUserName, _mailName, System.Text.Encoding.UTF8);
                    //收件人
                    objMailMessage.To.Add(to);
                    //标题
                    objMailMessage.Subject = title.Trim();
                    //标题字符编码
                    objMailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
                    //正文
                    objMailMessage.Body = body.Trim();
                    objMailMessage.IsBodyHtml = true;
                    //内容字符编码
                    objMailMessage.BodyEncoding = System.Text.Encoding.UTF8;
                    //发送
                    smtp.Send(objMailMessage);
                }
                catch (Exception)
                {
                    throw;
                }

            })).Start();
        }
    }
}