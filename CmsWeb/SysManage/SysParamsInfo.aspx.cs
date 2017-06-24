using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CmsBAL;
using CmsCommon;
using CmsEntity;

namespace CmsWeb.SysManage
{
    public partial class SysParamsInfo : BasePage
    {
        protected TB_SysParams entity = new TB_SysParams();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                int iId;
                Int32.TryParse(Request.QueryString["ID"], out iId);
                if (iId != 0)
                {
                    entity = new SysParamsBal().SelectSingleById(s => s.ID.Equals(iId));
                }
            }
        }
    }
}