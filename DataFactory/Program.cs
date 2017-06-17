using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CmsUtils;
using CmsEntity;

namespace DataFactory
{
    class Program
    {
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
        private static string EntityOutputPath
        {
            get { return "D:\\My Doc\\Project\\CMS\\Document\\Template\\cs\\Entity\\{0}.cs"; }
        }
        private static string DalOutputPath
        {
            get { return "D:\\My Doc\\Project\\CMS\\Document\\Template\\cs\\DAL\\{0}Dal.cs"; }
        }
        private static string BalOutputPath
        {
            get { return "D:\\My Doc\\Project\\CMS\\Document\\Template\\cs\\BAL\\{0}Bal.cs"; }
        }

        private static List<string> CommAtt = new List<string>(new string[] { "IsDeleted", "CreateUser", "CreateTime", "UpdateUser", "UpdateTime" });

        static void Main(string[] args)
        {
            //# TableColumnsConfig#	列表列名及格式配置
            //# TableName#	表名称
            //# CnFileName#	表说明
            //# ClassName#	类名(默认是表名去掉前缀)
            //# Date#	日期（yyyy/mm/dd)
            using (var ctx = new CmsEntities())
            {
                //查询所有表信息
                string tbSql = @"select TABLE_NAME,TABLE_COMMENT from information_schema.`TABLES` WHERE TABLE_SCHEMA='{0}'";
                //查询表中的所有字段
                string colSql = @"select COLUMN_NAME,DATA_TYPE,COLUMN_COMMENT,IS_NULLABLE from information_schema.columns WHERE TABLE_NAME='{0}';";
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

                    IoUtil.WriteFile(string.Format(BalOutputPath, className), balContent);

                    //生成DAL
                    string dalContent = IoUtil.ReadFile(string.Format(DalFilePath, string.Empty))
                        .Replace("#ClassName#", className)
                        .Replace("#TableName#", table.TABLE_NAME)
                        .Replace("#CnFileName#", cnFileName)
                        .Replace("#Date#", date);

                    IoUtil.WriteFile(string.Format(DalOutputPath, className), dalContent);

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
                        if (!CommAtt.Contains(col.COLUMN_NAME))
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
                    IoUtil.WriteFile(string.Format(EntityOutputPath, table.TABLE_NAME), entityContent.Replace("#Column#", attrSb.ToString()));
                }
            }
        }

        private void TestMethod()
        {
            using (var ctx = new CmsEntities())
            {
                //List<TB_BasicUser> list = new List<TB_BasicUser>();
                //for (int i = 0; i < 1024; i++)
                //{
                //    TB_BasicContent bcModel = new TB_BasicContent();
                //    bcModel.ContentTitle = "Title" + i;
                //    bcModel.ContentSubTitle = "SubTitle" + i;
                //    list.Add(bcModel);
                //}
                //ctx.TB_BasicContent.AddRange(list);
                //ctx.SaveChanges();
                string sql = @"SELECT
	                            u.*, d1.DicName UserStatusName
                            FROM
	                            TB_BasicUser u
                            LEFT JOIN TB_Dictionary d1 ON d1.IsDeleted = 0
                            AND d1.DicTypeCode = 'U02000'
                            AND d1.DicCode = u.UserStatus
                            WHERE
	                            u.IsDeleted = 0";

                List<TB_BasicUser> list = ctx.Database.SqlQueryForDataTatable(sql).ToList<TB_BasicUser>();
            }
        }
    }
}
