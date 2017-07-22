using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CmsCommon;
using CmsUtils;

namespace CmsWeb.SysManage
{
    public partial class SendEmail : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSend_OnClick(object sender, EventArgs e)
        {
            try
            {
                MailUtil email = new MailUtil(GlobalConfig.MailServer, GlobalConfig.MailUserName, GlobalConfig.MailPassword,
                    GlobalConfig.MailDisplayName, GlobalConfig.MailPort);
                if (email.Send(txtAddressee.Text.Trim(), txtSubject.Text, txtBody.Text))
                {
                    WebHelper.ClientToast(this, "邮件发送成功！");
                }
                else
                {
                    WebHelper.ClientToast(this, "邮件发送失败！");
                }
            }
            catch (Exception ex)
            {
                WebHelper.ClientToast(this, ex.Message);
            }
        }
    }
}