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
    public partial class RoleInfo : BasePage
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
            TB_Role entity = new RoleBal().SelectSingleById(u => u.ID.Equals(Id));
            entity.RoleAuthorityList = new RoleAuthorityBal().SelectList(r => r.RoleID == Id);
            string authorityIds = string.Empty;
            if (entity.RoleName != null)
            {
                txtRoleName.Text = entity.RoleName.ToString();
            }
            if (entity.IsUsing != null)
            {
                cbIsUsing.Checked = entity.IsUsing == Constants.IS_YES;
            }
            foreach (TB_RoleAuthority roleAuthority in entity.RoleAuthorityList)
            {
                if (authorityIds.IsEmpty())
                {
                    authorityIds = roleAuthority.AuthorityID.ToString();
                }
                else
                {
                    authorityIds += "," + roleAuthority.AuthorityID;
                }
            }
            hidAuthorityIds.Value = authorityIds;
        }

        /// <summary>
        /// 绑定下拉框数据源
        /// </summary>
        private void BindData()
        {
            //ControlUtil.BindListControl(this.ddlAdTypeID, new AdTypeBal().SelectList(a => a.IsDeleted != Constants.IS_NO && a.IsUsing == Constants.IS_YES).ToList(), "AdTypeName", "ID", true);
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            TB_Role entity;
            List<TB_RoleAuthority> roleAuthorities;
            if (Id != 0)
            {
                entity = new RoleBal().SelectSingleById(u => u.ID.Equals(Id));
                roleAuthorities = new List<TB_RoleAuthority>();
                entity.RoleName = txtRoleName.Text.Trim();
                entity.IsUsing = cbIsUsing.Checked ? Constants.IS_YES : Constants.IS_NO;
                if (!hidAuthorityIds.Value.Trim().IsEmpty())
                {
                    string[] authories = hidAuthorityIds.Value.Trim()
                        .Split(new string[] { "," }, StringSplitOptions.None);
                    foreach (string authory in authories)
                    {
                        TB_RoleAuthority roleAuthority = new TB_RoleAuthority();
                        roleAuthority.AuthorityID = int.Parse(authory);
                        roleAuthority.RoleID = entity.ID;
                        roleAuthorities.Add(roleAuthority);
                    }
                }
                new RoleBal().UpdateSingle(entity, roleAuthorities);
            }
            else
            {
                entity = new TB_Role();
                roleAuthorities = new List<TB_RoleAuthority>();
                entity.RoleName = txtRoleName.Text.Trim();
                entity.IsUsing = cbIsUsing.Checked ? Constants.IS_YES : Constants.IS_NO;
                if (!hidAuthorityIds.Value.Trim().IsEmpty())
                {
                    string[] authories = hidAuthorityIds.Value.Trim()
                        .Split(new string[] { "," }, StringSplitOptions.None);
                    foreach (string authory in authories)
                    {
                        TB_RoleAuthority roleAuthority = new TB_RoleAuthority();
                        roleAuthority.AuthorityID = int.Parse(authory);
                        roleAuthorities.Add(roleAuthority);
                    }
                    new RoleBal().InsertSingle(entity, roleAuthorities);
                }
            }
            Response.Redirect("~/SysManage/RoleList.aspx");
        }

    }
}
