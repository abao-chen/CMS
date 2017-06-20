using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CmsBAL;
using CmsCommon;
using CmsEntity;

namespace CmsWeb.API
{
    public partial class SystemApi : APIBase
    {
        public DataTablesResultModel<TB_BasicUser> GetPagerList()
        {
            DataTablesResultModel<TB_BasicUser> resultModel = new DataTablesResultModel<TB_BasicUser>();
            SearchModel searchModel = GetPostParams();
            string sql = @"SELECT
					            u.*,
					            d1.DicName UserStatusName,
					            d2.DicName UserTypeName
				            FROM
					            tb_basicuser u
				            LEFT JOIN tb_dictionary d1 ON d1.isdeleted = 0
				            AND d1.DicTypeCode = '{0}'
				            AND d1.DicCode = u.UserStatus
				            LEFT JOIN tb_dictionary d2 ON d2.isdeleted = 0
				            AND d2.DicTypeCode = '{1}'
				            AND d2.DicCode = u.UserType
				            WHERE
					            u.isdeleted = 0 ";
            
            sql = string.Format(sql, Constants.DIC_TYPE_USERSTATUS, Constants.DIC_TYPE_USERTYPE);
            new BasicUserBal().GetPagerList(resultModel, searchModel, sql);
            return resultModel;
        }

        public DataTablesResultModel<TB_BasicUser> DeleteUser()
        {
            DataTablesResultModel<TB_BasicUser> resultModel = new DataTablesResultModel<TB_BasicUser>();
            SearchModel searchModel = GetPostParams();
            new BasicUserBal().DeleteByIds(resultModel, searchModel);
            return resultModel;
        }
    }
}