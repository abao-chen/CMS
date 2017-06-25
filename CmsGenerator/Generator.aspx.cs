﻿using System;
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

        private string BalFilePath
        {
            get { return Request.MapPath("~/Template/Bal.cs"); }
        }
        private string DalFilePath
        {
            get { return Request.MapPath("~/Template/Dal.cs"); }
        }
        private string EntityFilePath
        {
            get { return Request.MapPath("~/Template/Entity.cs"); }
        }

        private string InfoFilePath
        {
            get { return Request.MapPath("~/Template/Info.aspx"); }
        }

        private string InfoCsFilePath
        {
            get { return Request.MapPath("~/Template/Info.aspx.cs"); }
        }

        private string InfoDesignerFilePath
        {
            get { return Request.MapPath("~/Template/Info.aspx.Designer.cs"); }
        }

        private string ListFilePath
        {
            get { return Request.MapPath("~/Template/List.aspx"); }
        }

        private string ListCsFilePath
        {
            get { return Request.MapPath("~/Template/List.aspx.cs"); }
        }

        private string ListDesignerFilePath
        {
            get { return Request.MapPath("~/Template/List.aspx.Designer.cs"); }
        }

        private string ApiFilePath
        {
            get { return Request.MapPath("~/Template/Api.aspx"); }
        }

        private string ApiCsFilePath
        {
            get { return Request.MapPath("~/Template/Api.aspx.cs"); }
        }

        private string ApiDesignerFilePath
        {
            get { return Request.MapPath("~/Template/Api.aspx.Designer.cs"); }
        }

        #endregion

        #region 文件输出路径

        private string EntityOutputPath
        {
            get { return Request.MapPath("~/Template/Entity/"); }
        }
        private string DalOutputPath
        {
            get { return Request.MapPath("~/Template/DAL/"); }
        }
        private string BalOutputPath
        {
            get { return Request.MapPath("~/Template/BAL/"); }
        }
        private string ViewOutputPath
        {
            get { return Request.MapPath("~/Template/View/"); }
        }
        private string ApiOutputPath
        {
            get { return Request.MapPath("~/Template/Api/"); }
        }

        public List<string> CommAtt1
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

        private List<string> CommAtt = new List<string>(new string[] { "IsDeleted", "CreateUser", "CreateTime", "UpdateUser", "UpdateTime" });

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
                    string balContent = FileUtil.ReadFile(string.Format(BalFilePath, string.Empty))
                        .Replace("#ClassName#", className)
                        .Replace("#TableName#", table.TABLE_NAME)
                        .Replace("#CnFileName#", cnFileName)
                        .Replace("#Date#", date);

                    FileUtil.WriteFile(BalOutputPath + className + "Bal.cs", balContent);

                    //生成DAL
                    string dalContent = FileUtil.ReadFile(string.Format(DalFilePath, string.Empty))
                        .Replace("#ClassName#", className)
                        .Replace("#TableName#", table.TABLE_NAME)
                        .Replace("#CnFileName#", cnFileName)
                        .Replace("#Date#", date);

                    FileUtil.WriteFile(DalOutputPath + className + "Dal.cs", dalContent);

                    //生成Entity
                    List<ColumnEntity> colList = ctx.Database.SqlQueryForDataTatable(string.Format(colSql, table.TABLE_NAME)).ToList<ColumnEntity>();
                    string entityContent = FileUtil.ReadFile(string.Format(EntityFilePath, string.Empty))
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
                                    dataType = "decimal{0}";
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
                    FileUtil.WriteFile(EntityOutputPath + table.TABLE_NAME + ".cs", entityContent.Replace("#Column#", attrSb.ToString()));
                }
            }
        }

        /// <summary>
        /// 生成页面文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGeneratorView_OnClick(object sender, EventArgs e)
        {
            #region 生成List文件
            //
            using (var ctx = new CmsEntities())
            {
                //查询所有表信息
                string tbSql =
                    @"select TABLE_NAME,TABLE_COMMENT from information_schema.`TABLES` WHERE TABLE_SCHEMA = '{0}'";
                //查询表中的所有字段
                string colSql =
                    @"select COLUMN_NAME,DATA_TYPE,COLUMN_COMMENT,IS_NULLABLE from information_schema.columns WHERE TABLE_NAME='{0}'";
                string cnFileName = string.Empty;
                string className = string.Empty;
                string date = DateTime.Now.ToString("yyyy/MM/dd");
                List<TableEntity> tbList = ctx.Database.SqlQueryForDataTatable(string.Format(tbSql, "cms"))
                    .ToList<TableEntity>();
                foreach (TableEntity table in tbList)
                {
                    if (this.hidTablesName.Value.ToLower().Split(new string[] { "," }, StringSplitOptions.None)
                        .Contains(table.TABLE_NAME.ToLower()))
                    {
                        className = table.TABLE_NAME.Remove(0, 3);
                        cnFileName = table.TABLE_COMMENT.Replace("表", "");

                        //List.aspx.designer.cs
                        string listDesignerContent = FileUtil.ReadFile(ListDesignerFilePath)
                            .Replace("#ClassName#", className);
                        FileUtil.WriteFile(ViewOutputPath + className + "List.aspx.designer.cs", listDesignerContent);

                        //List.aspx.cs
                        string listCsContent = FileUtil.ReadFile(ListCsFilePath)
                            .Replace("#ClassName#", className)
                            .Replace("#TableName# ", table.TABLE_NAME)
                            .Replace("#CnFileName#", cnFileName)
                            .Replace("#Date#", date);
                        FileUtil.WriteFile(ViewOutputPath + className + "List.aspx.cs", listCsContent);

                        //Api.aspx.designer.cs
                        string ApiDesignerContent = FileUtil.ReadFile(ApiDesignerFilePath).Replace("#ClassName#", className);
                        FileUtil.WriteFile(ApiOutputPath + className + "Api.aspx.designer.cs", ApiDesignerContent);

                        //Api.aspx.cs
                        string ApiCsContent = FileUtil.ReadFile(ApiCsFilePath)
                            .Replace("#ClassName#", className)
                            .Replace("#TableName#", table.TABLE_NAME)
                            .Replace("#CnFileName#", cnFileName)
                            .Replace("#Date#", date);
                        FileUtil.WriteFile(ApiOutputPath + className + "Api.aspx.cs", ApiCsContent);

                        //Api.aspx
                        string ApiContent = FileUtil.ReadFile(ApiFilePath).Replace("#ClassName#", className);
                        FileUtil.WriteFile(ApiOutputPath + className + "Api.aspx", ApiContent);


                        //infoContent.aspx.designer.cs
                        string infoDesignerContent = FileUtil.ReadFile(InfoDesignerFilePath).Replace("#ClassName#", className);
                        FileUtil.WriteFile(ViewOutputPath + className + "Info.aspx.designer.cs", infoDesignerContent);

                        //infoContent.aspx.cs
                        string infoCsContent = FileUtil.ReadFile(InfoCsFilePath)
                            .Replace("#ClassName#", className)
                            .Replace("#TableName#", table.TABLE_NAME)
                            .Replace("#CnFileName#", cnFileName)
                            .Replace("#Date#", date);
                        FileUtil.WriteFile(ViewOutputPath + className + "Info.aspx.cs", infoCsContent);
                        
                        //List.aspx
                        string listContent = FileUtil.ReadFile(ListFilePath)
                            .Replace("#ClassName#", className)
                            .Replace("#TableName# ", table.TABLE_NAME)
                            .Replace("#AjaxName#", className); //Ajax文件名
                        StringBuilder searchBuilder = new StringBuilder();
                        StringBuilder listHeadBuilder = new StringBuilder();
                        StringBuilder colConfigBuilder = new StringBuilder();
                        StringBuilder editFormBuilder = new StringBuilder();
                        List<ColumnEntity> colList = ctx.Database
                            .SqlQueryForDataTatable(string.Format(colSql, table.TABLE_NAME)).ToList<ColumnEntity>();
                        foreach (ColumnEntity colEntity in colList)
                        {
                            if (!CommAtt.Contains(colEntity.COLUMN_NAME) && colEntity.COLUMN_NAME != "ID")
                            {
                                string searchCols = "<div class=\"col-lg-4 form-group\">\r\n";
                                searchCols += "                                <asp:#ControlType# runat=\"server\" ID=\"#ControlAlisa##ColName#\" searchattr=\"#ColName#|LIKE|#ColName#\" CssClass=\"form-control\" placeholder=\"#ColCnName#\"></asp:#ControlType#>\r\n";
                                searchCols += "                            </div> ";
                                searchCols = searchCols.Replace("#ControlType#", "TextBox")
                                    .Replace("#ControlAlisa#", "txt")
                                    .Replace("#ColName#", colEntity.COLUMN_NAME)
                                    .Replace("#ColCnName#", colEntity.COLUMN_COMMENT);
                                searchBuilder.AppendLine(searchCols);

                                string editCols = "<div class=\"col-lg-6\">\r\n";
                                editCols += "             <div class=\"form-group\">\r\n";
                                editCols += "    <label>#ColCnName#：</label>\r\n";
                                editCols += "<asp:TextBox ID =\"txt#ColName#\" runat=\"server\" CssClass=\"form-control\"></asp:TextBox>\r\n";
                                editCols += "</div>\r\n";
                                editCols += "</div> ";

                                editCols = editCols.Replace("#ColName#", colEntity.COLUMN_NAME)
                                    .Replace("#ColCnName#", colEntity.COLUMN_COMMENT);
                                editFormBuilder.AppendLine(editCols);

                                listHeadBuilder.AppendLine("<th>" + colEntity.COLUMN_COMMENT + "</th>");

                                colConfigBuilder.AppendLine("                    { \"data\": \"" + colEntity.COLUMN_NAME + "\" },");
                            }
                        }

                        listContent = listContent.Replace("#SearchCols#", searchBuilder.ToString())
                            .Replace("#ListHead#", listHeadBuilder.ToString()) //datatables头部
                            .Replace("#ColConfig#", colConfigBuilder.ToString());//datatables列配置
                        FileUtil.WriteFile(ViewOutputPath + className + "List.aspx", listContent);

                        //infoContent.aspx
                        string infoContent = FileUtil.ReadFile(InfoFilePath)
                            .Replace("#EditForm#", editFormBuilder.ToString())
                            .Replace("#ClassName#", className)
                            .Replace("#CnFileName#", cnFileName);
                        FileUtil.WriteFile(ViewOutputPath + className + "Info.aspx", infoContent);
                    }
                }
            }

            #endregion

            #region 生成Info文件

            #endregion
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