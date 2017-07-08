using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CmsBAL;
using CmsCommon;
using CmsEntity;
using CmsUtils;

namespace CmsWeb
{
    public partial class UserInfo : BasePage
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        private int Id
        {
            get
            {
                int id = 0;
                int.TryParse(Request.QueryString["Id"], out id);
                return id;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
                if (Id != 0)
                {
                    InitData();
                }
            }
        }

        /// <summary>
        /// 初始页面数据
        /// </summary>
        private void InitData()
        {
            TB_BasicUser userInfo = new BasicUserBal().SelectSingleById(u => u.ID.Equals(Id));
            if (userInfo != null)
            {

                txtAccount.Text = userInfo.UserAccount;
                txtName.Text = userInfo.UserName;
                txtPassword.Enabled = false;
                txtPassword.Text = userInfo.UserPassword;
                lbLastLoginTime.Text = userInfo.LastLoginTime == null
                    ? string.Empty
                    : Convert.ToDateTime(userInfo.LastLoginTime).ToString("yyyy/mm/dd hh:mm:ss");
                ddlStatus.SelectedValue = userInfo.UserStatus;
                ddlType.SelectedValue = userInfo.UserType;
            }
        }

        /// <summary>
        /// 绑定下拉框数据源
        /// </summary>
        private void BindData()
        {
            ControlUtil.BindDropDownList(this.ddlStatus,
                new DictionaryBal().GetDictionaryList(Constants.DIC_TYPE_USERSTATUS), true);
            ControlUtil.BindDropDownList(this.ddlType,
                new DictionaryBal().GetDictionaryList(Constants.DIC_TYPE_USERTYPE), true);
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        { 
            TB_BasicUser userInfo;
            if (Id != 0)
            {
                userInfo = new BasicUserBal().SelectSingleById(u => u.ID.Equals(Id));
                userInfo.UserAccount = txtAccount.Text;
                userInfo.UserName = txtName.Text;
                userInfo.UserStatus = ddlStatus.SelectedValue;
                userInfo.UserType = ddlType.SelectedValue;
                new BasicUserBal().UpdateSingle(userInfo);
            }
            else
            {
                userInfo = new TB_BasicUser();
                userInfo.UserAccount = txtAccount.Text;
                userInfo.UserName = txtName.Text;
                userInfo.PasswordSalt = SecurityUtil.RandomCode(Constants.RANDOM_MODEL_MIXED, 10);
                userInfo.UserPassword = SecurityUtil.Md5Encrypt64(txtPassword.Text + userInfo.PasswordSalt);
                userInfo.UserStatus = ddlStatus.SelectedValue;
                userInfo.UserType = ddlType.SelectedValue;
                new BasicUserBal().InsertSingle(userInfo);
            }

            Response.Redirect("~/SysManage/UserList.aspx");
        }
    }
}