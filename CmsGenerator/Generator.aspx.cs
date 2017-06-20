using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CmsEntity;
using CmsUtils;

namespace CmsGenerator
{
    public partial class Generator : System.Web.UI.Page
    {

        #region 模板文件路径

        private static string BalFilePath
        {
            get { return "D:\\My Doc\\Project\\CMS\\Document\\Template\\cs\\Bal.cs"; }
        }
        private static string DalFilePath
        {
            get { return "D:\\My Doc\\Project\\CMS\\Document\\Template\\cs\\Dal.cs"; }
        }
        private static string EntityFilePath
        {
            get { return "D:\\My Doc\\Project\\CMS\\Document\\Template\\cs\\Entity.cs"; }
        }

        private static string InfoFilePath
        {
            get { return "D:\\My Doc\\Project\\CMS\\Document\\Template\\cs\\Info.aspx"; }
        }

        private static string InfoCsFilePath
        {
            get { return "D:\\My Doc\\Project\\CMS\\Document\\Template\\cs\\Info.aspx.cs"; }
        }

        private static string InfoDesignerFilePath
        {
            get { return "D:\\My Doc\\Project\\CMS\\Document\\Template\\cs\\Info.aspx.Designer.cs"; }
        }

        private static string ListFilePath
        {
            get { return "D:\\My Doc\\Project\\CMS\\Document\\Template\\cs\\List.aspx"; }
        }

        private static string ListCsFilePath
        {
            get { return "D:\\My Doc\\Project\\CMS\\Document\\Template\\cs\\List.aspx.cs"; }
        }

        private static string ListDesignerFilePath
        {
            get { return "D:\\My Doc\\Project\\CMS\\Document\\Template\\cs\\List.aspx.Designer.cs"; }
        }

        #endregion

        #region 文件输出路径

        private static string EntityOutputPath
        {
            get { return "D:\\My Doc\\Project\\CMS\\Document\\Template\\cs\\Entity\\"; }
        }
        private static string DalOutputPath
        {
            get { return "D:\\My Doc\\Project\\CMS\\Document\\Template\\cs\\DAL\\"; }
        }
        private static string BalOutputPath
        {
            get { return "D:\\My Doc\\Project\\CMS\\Document\\Template\\cs\\BAL\\"; }
        }
        private static string ViewOutputPath
        {
            get { return "D:\\My Doc\\Project\\CMS\\Document\\Template\\cs\\View\\"; }
        }

        public static List<string> CommAtt1
        {
            get
            {
                return CommAtt;
            }

            set
            {
                CommAtt = value;
            }
        }


        #endregion

        private static List<string> CommAtt = new List<string>(new string[] { "IsDeleted", "CreateUser", "CreateTime", "UpdateUser", "UpdateTime" });

        private DataTable dicTypeDt = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindTablesData();
            }
            using (var ctx = new CmsEntities())
            {
                dicTypeDt = ctx.Database.SqlQueryForDataTatable("Select DicTypeCode,DicTypeName from tb_dicType where isdeleted=0 and isusing=1");
            }
        }

        /// <summary>
        /// 绑定Table数据
        /// </summary>
        private void BindTablesData()
        {
            using (var ctx = new CmsEntities())
            {
                //查询所有表信息
                string tbSql = @"select TABLE_NAME,TABLE_COMMENT from information_schema.`TABLES` WHERE TABLE_SCHEMA='{0}'";
                DataTable tbDt = ctx.Database.SqlQueryForDataTatable(string.Format(tbSql, "cms"));
                cbTables.DataSource = tbDt;
                cbTables.DataTextField = "TABLE_NAME";
                cbTables.DataValueField = "TABLE_NAME";
                cbTables.DataBind();
            }
        }

        /// <summary>
        /// 生成三层文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGeneratorBDE_OnClick(object sender, EventArgs e)
        {
            using (var ctx = new CmsEntities())
            {
                //查询所有表信息
                string tbSql = @"select TABLE_NAME,TABLE_COMMENT from information_schema.`TABLES` WHERE TABLE_SCHEMA='{0}'";
                //查询表中的所有字段
                string colSql = @"select COLUMN_NAME,DATA_TYPE,COLUMN_COMMENT,IS_NULLABLE from information_schema.columns WHERE TABLE_NAME='{0}'";
                string cnFileName = string.Empty;
                string className = string.Empty;
                string date = DateTime.Now.ToString("yyyy/MM/dd");
                List<TableEntity> tbList = ctx.Database.SqlQueryForDataTatable(string.Format(tbSql, "cms")).ToList<TableEntity>();
                foreach (TableEntity table in tbList)
                {
                    className = table.TABLE_NAME.Remove(0, 3);
                    cnFileName = table.TABLE_COMMENT.Replace("表", "");
                    //生成BAL
                    string balContent = IoUtil.ReadFile(string.Format(BalFilePath, string.Empty))
                        .Replace("#ClassName#", className)
                        .Replace("#TableName#", table.TABLE_NAME)
                        .Replace("#CnFileName#", cnFileName)
                        .Replace("#Date#", date);

                    IoUtil.WriteFile(BalOutputPath + className + "Bal.cs", balContent);

                    //生成DAL
                    string dalContent = IoUtil.ReadFile(string.Format(DalFilePath, string.Empty))
                        .Replace("#ClassName#", className)
                        .Replace("#TableName#", table.TABLE_NAME)
                        .Replace("#CnFileName#", cnFileName)
                        .Replace("#Date#", date);

                    IoUtil.WriteFile(DalOutputPath + className + "Dal.cs", dalContent);

                    //生成Entity
                    List<ColumnEntity> colList = ctx.Database.SqlQueryForDataTatable(string.Format(colSql, table.TABLE_NAME)).ToList<ColumnEntity>();
                    string entityContent = IoUtil.ReadFile(string.Format(EntityFilePath, string.Empty))
                        .Replace("#TableName#", table.TABLE_NAME)
                        .Replace("#CnFileName#", cnFileName)
                        .Replace("#Date#", date);
                    StringBuilder attrSb = new StringBuilder();
                    foreach (ColumnEntity col in colList)
                    {
                        string dataType = string.Empty;
                        if (!CommAtt1.Contains(col.COLUMN_NAME))
                        {//通用字段过滤
                            col.COLUMN_COMMENT = Regex.Replace(col.COLUMN_COMMENT, "\\r\\n", "");
                            attrSb.AppendLine("        /// <summary>");
                            attrSb.AppendLine("        /// " + col.COLUMN_COMMENT);
                            attrSb.AppendLine("        /// </summary>");
                            switch (col.DATA_TYPE)
                            {
                                case "int":
                                    dataType = "int{0}";
                                    break;
                                case "datetime":
                                    dataType = "DateTime{0}";
                                    break;
                                case "timestamp":
                                    dataType = "TimeSpan{0}";
                                    break;
                                case "bigint":
                                    dataType = "long{0}";
                                    break;
                                case "double":
                                    dataType = "double{0}";
                                    break;
                                case "decimal":
                                    dataType = "decimal{0}";
                                    break;
                                default:
                                    dataType = "string{0}";
                                    break;
                            }
                            if (col.IS_NULLABLE == "YES" && !dataType.StartsWith("string"))
                            {
                                dataType = string.Format(dataType, "?");
                            }
                            else
                            {
                                dataType = string.Format(dataType, "");
                            }
                            attrSb.AppendLine("        public " + dataType + " " + col.COLUMN_NAME + " { get; set; }");
                        }
                    }
                    IoUtil.WriteFile(EntityOutputPath + table.TABLE_NAME + ".cs", entityContent.Replace("#Column#", attrSb.ToString()));
                }
            }
        }

        /// <summary>
        /// 绑定表中的字段数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cbTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tablesWhere = String.Empty;
            for (int i = 0; i < this.cbTables.Items.Count; i++)
            {
                if (cbTables.Items[i].Selected)
                {
                    if (string.IsNullOrEmpty(tablesWhere))
                    {
                        tablesWhere = "'" + cbTables.Items[i].Value + "'";
                    }
                    else
                    {
                        tablesWhere += ",'" + cbTables.Items[i].Value + "'";
                    }
                }
            }
            if (!string.IsNullOrEmpty(tablesWhere))
            {
                using (var ctx = new CmsEntities())
                {
                    string tbSql = @"select TABLE_NAME,TABLE_COMMENT from information_schema.`TABLES` WHERE TABLE_NAME IN (" + tablesWhere + ")";
                    DataTable tbDt = ctx.Database.SqlQueryForDataTatable(tbSql);
                    rpTables.DataSource = tbDt;
                    rpTables.DataBind();
                }
            }
        }

        /// <summary>
        /// 绑定字典类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rpCols_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DropDownList dicType = e.Item.FindControl("dicType") as DropDownList;
                if (dicType != null)
                {
                    dicType.DataSource = dicTypeDt;
                    dicType.DataTextField = "DicTypeName";
                    dicType.DataValueField = "DicTypeCode";
                    dicType.DataBind();
                }
            }
        }

        /// <summary>
        /// 绑定列表字段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rpTables_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rpCols = e.Item.FindControl("rpCols") as Repeater;
                if (rpCols != null)
                {
                    DataRowView rowv = (DataRowView)e.Item.DataItem;
                    string tbName = rowv["TABLE_NAME"].ToString();
                    using (var ctx = new CmsEntities())
                    {
                        string sql = @"select COLUMN_NAME,DATA_TYPE,COLUMN_COMMENT,IS_NULLABLE from information_schema.columns WHERE TABLE_NAME='" + tbName + "'";
                        DataTable colDt = ctx.Database.SqlQueryForDataTatable(sql);
                        rpCols.DataSource = colDt;
                        rpCols.DataBind();
                    }
                }
            }
        }
    }
}