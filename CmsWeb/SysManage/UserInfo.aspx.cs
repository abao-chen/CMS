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

                txtUserAccount.Text = userInfo.UserAccount;
                txtName.Text = userInfo.UserName;
                txtPassword.Attributes["value"] =userInfo.UserPassword;
                lbLastLoginTime.Text = userInfo.LastLoginTime == null
                    ? string.Empty
                    : Convert.ToDateTime(userInfo.LastLoginTime).ToString("yyyy/MM/dd HH:mm:ss");
                ddlStatus.SelectedValue = userInfo.UserStatus;
                ddlType.SelectedValue = userInfo.UserType;
                List<TB_UserRole> userRoleList = new UserRoleBal().SelectList(ur => ur.UserID == Id);
                string selectedValue = string.Empty;
                foreach (TB_UserRole userRole in userRoleList)
                {
                    if (selectedValue.IsEmpty())
                    {
                        selectedValue = userRole.RoleID.ToString();
                    }
                    else
                    {
                        selectedValue += "," + userRole.RoleID;
                    }
                }
                cblRole.SelectedValue = selectedValue;
            }
        }

        /// <summary>
        /// 绑定下拉框数据源
        /// </summary>
        private void BindData()
        {
            ControlUtil.BindListControl(this.ddlStatus,
                new DictionaryBal().GetDictionaryList(Constants.DIC_TYPE_USERSTATUS), true);
            ControlUtil.BindListControl(this.ddlType,
                new DictionaryBal().GetDictionaryList(Constants.DIC_TYPE_USERTYPE), true);
            cblRole.DataSource = new RoleBal().SelectList(r => r.IsDeleted == Constants.IS_NO && r.IsUsing == Constants.IS_YES);
            cblRole.DataTextField = "RoleName";
            cblRole.DataValueField = "ID";
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            TB_BasicUser userInfo;
            if (Id != 0)
            {
                userInfo = new BasicUserBal().SelectSingleById(u => u.ID.Equals(Id));
                userInfo.UserAccount = txtUserAccount.Text;
                if (txtPassword.Text.IsNotEmpty() && txtPassword.Text != userInfo.UserPassword)
                {
                    userInfo.UserPassword = SecurityUtil.Md5Encrypt64(txtPassword.Text + userInfo.PasswordSalt);
                }
                userInfo.UserName = txtName.Text;
                userInfo.UserStatus = ddlStatus.SelectedValue;
                userInfo.UserType = ddlType.SelectedValue;
                new BasicUserBal().UpdateSingle(userInfo);
                List<TB_UserRole> userRoleList = new List<TB_UserRole>();
                if (cblRole.SelectedValueArray != null)
                {
                    foreach (string selectedValue in cblRole.SelectedValueArray)
                    {
                        userRoleList.Add(new TB_UserRole
                        {
                            UserID = userInfo.ID,
                            RoleID = Convert.ToInt32(selectedValue)
                        });
                    }
                }
                new UserRoleBal().DeleteListByUserId(userInfo.ID);
                new UserRoleBal().InsertList(userRoleList);
            }
            else
            {
                userInfo = new TB_BasicUser();
                userInfo.UserAccount = txtUserAccount.Text;
                userInfo.UserName = txtName.Text;
                userInfo.PasswordSalt = SecurityUtil.RandomCode(Constants.RANDOM_MODEL_MIXED, 10);
                userInfo.UserPassword = SecurityUtil.Md5Encrypt64(txtPassword.Text + userInfo.PasswordSalt);
                userInfo.UserStatus = ddlStatus.SelectedValue;
                userInfo.UserType = ddlType.SelectedValue;
                new BasicUserBal().InsertSingle(userInfo);
                List<TB_UserRole> userRoleList = new List<TB_UserRole>();
                if (cblRole.SelectedValueArray != null)
                {
                    foreach (string selectedValue in cblRole.SelectedValueArray)
                    {
                        userRoleList.Add(new TB_UserRole
                        {
                            UserID = userInfo.ID,
                            RoleID = Convert.ToInt32(selectedValue)
                        });
                    }
                }
                new UserRoleBal().InsertList(userRoleList);
            }

            Response.Redirect("~/SysManage/UserList.aspx");
        }
    }
}