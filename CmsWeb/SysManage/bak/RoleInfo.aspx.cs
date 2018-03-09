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

namespace CmsWeb.SysManage
{
    public partial class RoleInfo : BasePage
    {
        /// <summary>
        /// 角色ID
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
                if (Id != 0)
                {
                    InitData();
                }
            }
        }


        /// <summary>
        /// 表单数据初始化
        /// </summary>
        private void InitData()
        {
            TB_Role entity = new RoleBal().SelectSingleById(u => u.ID.Equals(Id));
            if (entity != null)
            {
                entity.RoleAuthorityList = new RoleAuthorityBal().SelectList(r => r.RoleID == Id);
                string authorityIds = string.Empty;
                txtRoleName.Text = entity.RoleName;
                cbxIsUsing.Checked = entity.IsUsing == 1;
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
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            TB_Role entity;
            List<TB_RoleAuthority> roleAuthorities;
            if (Id != 0)
            {
                entity = new RoleBal().SelectSingleById(u => u.ID.Equals(Id));
                roleAuthorities = new List<TB_RoleAuthority>();
                entity.RoleName = txtRoleName.Text.Trim();
                entity.IsUsing = cbxIsUsing.Checked ? Constants.IS_YES : Constants.IS_NO;
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
                entity.IsUsing = cbxIsUsing.Checked ? Constants.IS_YES : Constants.IS_NO;
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