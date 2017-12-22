using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CmsEntity;
using CmsGenerator.Entity;
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
            get { return CommAtt; }

            set { CommAtt = value; }
        }


        #endregion

        private List<string> CommAtt = new List<string>(new string[]
            {"IsDeleted", "CreateUser", "CreateTime", "UpdateUser", "UpdateTime"});

        private DataTable dicTypeDt = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindTablesData();
            }
            using (var ctx = new CmsEntities())
            {
                dicTypeDt = ctx.Database.SqlQueryForDataTatable(
                    "Select DicTypeCode,DicTypeName from tb_dicType where isdeleted=0 and isusing=1");
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
                string tbSql =
                    @"select TABLE_NAME,TABLE_COMMENT from information_schema.`TABLES` WHERE TABLE_SCHEMA='{0}'";
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
            #region 清空原有文件

            if (Directory.Exists(EntityOutputPath))
            {
                FileUtil.DeleteFolderFiles(EntityOutputPath, true);
            }
            else
            {
                Directory.CreateDirectory(EntityOutputPath);
            }
            if (Directory.Exists(DalOutputPath))
            {
                FileUtil.DeleteFolderFiles(DalOutputPath, true);
            }
            else
            {
                Directory.CreateDirectory(DalOutputPath);
            }
            if (Directory.Exists(BalOutputPath))
            {
                FileUtil.DeleteFolderFiles(BalOutputPath, true);
            }
            else
            {
                Directory.CreateDirectory(BalOutputPath);
            }

            #endregion

            string tablesWhere = String.Empty;
            if (this.cbTables.SelectedValueArray != null)
            {
                for (int i = 0; i < this.cbTables.SelectedValueArray.Length; i++)
                {
                    if (string.IsNullOrEmpty(tablesWhere))
                    {
                        tablesWhere = "'" + cbTables.SelectedValueArray[i] + "'";
                    }
                    else
                    {
                        tablesWhere += ",'" + cbTables.SelectedValueArray[i] + "'";
                    }
                }
            }

            using (var ctx = new CmsEntities())
            {
                //查询所有表信息
                string tbSql =
                    @"select TABLE_NAME,TABLE_COMMENT from information_schema.`TABLES` WHERE binary TABLE_NAME IN (" + tablesWhere + ")";
                //查询表中的所有字段
                string colSql =
                    @"select COLUMN_NAME,DATA_TYPE,COLUMN_COMMENT,IS_NULLABLE from information_schema.columns WHERE binary TABLE_NAME='{0}'";
                string cnFileName = string.Empty;
                string className = string.Empty;
                string date = DateTime.Now.ToString("yyyy/MM/dd");
                List<TableEntity> tbList = ctx.Database.SqlQueryForDataTatable(string.Format(tbSql, "cms"))
                    .ToList<TableEntity>();
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
                    List<ColumnEntity> colList = ctx.Database
                        .SqlQueryForDataTatable(string.Format(colSql, table.TABLE_NAME)).ToList<ColumnEntity>();
                    string entityContent = FileUtil.ReadFile(string.Format(EntityFilePath, string.Empty))
                        .Replace("#TableName#", table.TABLE_NAME)
                        .Replace("#CnFileName#", cnFileName)
                        .Replace("#Date#", date);
                    StringBuilder attrSb = new StringBuilder();
                    foreach (ColumnEntity col in colList)
                    {
                        string dataType = string.Empty;
                        if (!CommAtt1.Contains(col.COLUMN_NAME))
                        {
                            //通用字段过滤
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
                    FileUtil.WriteFile(EntityOutputPath + table.TABLE_NAME + ".cs",
                        entityContent.Replace("#Column#", attrSb.ToString()));
                }
            }
        }

        /// <summary>
        /// 绑定页面配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnShowPageConfig_Click(object sender, EventArgs e)
        {
            string tablesWhere = String.Empty;
            if (this.cbTables.SelectedValueArray != null)
            {
                for (int i = 0; i < this.cbTables.SelectedValueArray.Length; i++)
                {
                    if (string.IsNullOrEmpty(tablesWhere))
                    {
                        tablesWhere = "'" + cbTables.SelectedValueArray[i] + "'";
                    }
                    else
                    {
                        tablesWhere += ",'" + cbTables.SelectedValueArray[i] + "'";
                    }
                }
            }
            
            if (!string.IsNullOrEmpty(tablesWhere))
            {
                using (var ctx = new CmsEntities())
                {
                    string tbSql =
                        @"select TABLE_NAME,TABLE_COMMENT from information_schema.`TABLES` WHERE binary TABLE_NAME IN (" +
                        tablesWhere + ")";
                    DataTable tbDt = ctx.Database.SqlQueryForDataTatable(tbSql);
                    rpTables.DataSource = tbDt;
                    rpTables.DataBind();
                }
            }
            else
            {
                rpTables.DataSource = null;
                rpTables.DataBind();
            }
        }

        /// <summary>
        /// 生成页面文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGeneratorView_OnClick(object sender, EventArgs e)
        {
            #region 清空原有文件

            if (Directory.Exists(ViewOutputPath))
            {
                FileUtil.DeleteFolderFiles(ViewOutputPath, true);
            }
            else
            {
                Directory.CreateDirectory(ViewOutputPath);
            }
            if (Directory.Exists(ApiOutputPath))
            {
                FileUtil.DeleteFolderFiles(ApiOutputPath, true);
            }
            else
            {
                Directory.CreateDirectory(ApiOutputPath);
            }

            #endregion
            string formDataStr = hidFormData.Value;
            List<GeneraEntity> list = formDataStr.ToObject<List<GeneraEntity>>();
            string date = DateTime.Now.ToString("yyyy/MM/dd");
            foreach (GeneraEntity entity in list)
            {
                string className = entity.tableName.Replace("TB_", string.Empty);
                string cnFileName = entity.tableComment.Replace("表", string.Empty);

                //生成Api文件
                CreateApiFile(className, entity, cnFileName, date); CreateApiFile(className, entity, cnFileName, date);
                //生成List文件
                CreateListFile(className, entity, cnFileName, date);
                //生成Info文件
                CreateInfoFile(entity, className, cnFileName, date);
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
                        string sql =
                            @"select COLUMN_NAME,DATA_TYPE,COLUMN_COMMENT,IS_NULLABLE from information_schema.columns WHERE TABLE_NAME='" +
                            tbName + "'";
                        DataTable colDt = ctx.Database.SqlQueryForDataTatable(sql);
                        rpCols.DataSource = colDt;
                        rpCols.DataBind();
                    }
                }
            }
        }

        protected void btnTest_OnClick(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 创建编辑页面
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="className">类名</param>
        /// <param name="cnFileName">文件名</param>
        /// <param name="date">日期</param>
        private void CreateInfoFile(GeneraEntity entity, string className, string cnFileName,
            string date)
        {
            StringBuilder editFormBuilder = new StringBuilder();
            StringBuilder validatorBuilder = new StringBuilder();
            //初始化数据
            StringBuilder initDataBuilder = new StringBuilder();
            //初始化日期控件
            StringBuilder initDateBuilder = new StringBuilder();
            //保存数据
            StringBuilder saveBuilder = new StringBuilder();
            //designer.cs
            StringBuilder designerBuilder = new StringBuilder();
            foreach (GeneraColunm colEntity in entity.columns)
            {
                if (colEntity.isEdit == 1)
                {//是否可编辑
                    string controlAlisa;
                    string controlType;
                    string saveCol;
                    string editCols = "                        <div class=\"col-lg-6\">\r\n";
                    editCols += "                            <div class=\"form-group\">\r\n";
                    editCols += "                                <label>#ColCnName#：</label>\r\n";
                    editCols += "                                <#tagPrefix#:#ControlType# ID =\"#ControlAlisa##ColName#\" runat=\"server\" CssClass=\"form-control\"></#tagPrefix#:#ControlType#>\r\n";
                    switch (colEntity.controlType)
                    {
                        case 1://文本框
                            controlAlisa = "txt";
                            controlType = "TextBox";
                            initDataBuilder.AppendLine("            if(entity.#ColName# != null )");
                            initDataBuilder.AppendLine("            {");
                            initDataBuilder.AppendLine("                #ControlAlisa##ColName#.Text = entity.#ColName#.ToString();");
                            initDataBuilder.AppendLine("            }");
                            saveCol = "            entity.#ColName# = #ControlAlisa##ColName#.Text.Trim();";
                            editCols = editCols.Replace("#tagPrefix#", "asp");
                            designerBuilder.AppendLine("        protected global::System.Web.UI.WebControls.TextBox #ControlAlisa##ColName#;");
                            break;
                        case 2://下拉框
                            controlAlisa = "ddl";
                            controlType = "DropDownList";
                            initDataBuilder.AppendLine("            if(entity.#ColName# != null )");
                            initDataBuilder.AppendLine("            {");
                            initDataBuilder.AppendLine("                #ControlAlisa##ColName#.SelectedValue = entity.#ColName#.ToString();");
                            initDataBuilder.AppendLine("            }");
                            saveCol = "            entity.#ColName# = #ControlAlisa##ColName#.SelectedValue;";
                            editCols = editCols.Replace("#tagPrefix#", "asp");
                            designerBuilder.AppendLine("        protected global::System.Web.UI.WebControls.DropDownList #ControlAlisa##ColName#;");
                            break;
                        case 3://日期
                            controlAlisa = "txt";
                            controlType = "DataPickerExt";
                            initDataBuilder.AppendLine("            if(entity.#ColName# != null )");
                            initDataBuilder.AppendLine("            {");
                            initDataBuilder.AppendLine("                #ControlAlisa##ColName#.DateValue = entity.#ColName#;");
                            initDataBuilder.AppendLine("            }");
                            saveCol = "            if(!#ControlAlisa##ColName#.TextValue.IsEmpty())\r\n";
                            saveCol += "            {\r\n";
                            saveCol += "                entity.#ColName# = #ControlAlisa##ColName#.DateValue;\r\n";
                            saveCol += "            }\r\n";
                            editCols = "                        <div class=\"col-lg-6\">\r\n";
                            editCols += "                            <div class=\"form-group\">\r\n";
                            editCols += "                                <label>#ColCnName#：</label>\r\n";
                            editCols += "                                <Cms:DataPicker ID=\"#ControlAlisa##ColName#\" runat=\"server\" Name=\"#ColName#\" Format=\"yyyy/MM/dd\"></Cms:DataPicker>";
                            designerBuilder.AppendLine("        protected global::CmsWeb.ControlExt.DataPickerExt #ControlAlisa##ColName#;");
                            break;
                        case 4://多选框 checkboxlist
                            controlAlisa = "cbl";
                            controlType = "CheckBoxListExt";
                            initDataBuilder.AppendLine("            if(entity.#ColName# != null )");
                            initDataBuilder.AppendLine("            {");
                            initDataBuilder.AppendLine("                #ControlAlisa##ColName#.SelectedValue = entity.#ColName#.ToString();");
                            initDataBuilder.AppendLine("            }");
                            saveCol = "            entity.#ColName# = #ControlAlisa##ColName#.SelectedValue;";
                            editCols = editCols.Replace("#tagPrefix#", "Cms");
                            designerBuilder.AppendLine("        protected global::CmsWeb.ControlExt.CheckBoxListExt #ControlAlisa##ColName#;");
                            break;
                        case 5://单选框 radioboxlist
                            controlAlisa = "rbl";
                            controlType = "RadioBoxListExt";
                            initDataBuilder.AppendLine("            if(entity.#ColName# != null )");
                            initDataBuilder.AppendLine("            {");
                            initDataBuilder.AppendLine("                #ControlAlisa##ColName#.SelectedValue = entity.#ColName#.ToString();");
                            initDataBuilder.AppendLine("            }");
                            saveCol = "            entity.#ColName# = #ControlAlisa##ColName#.SelectedValue;";
                            editCols = editCols.Replace("#tagPrefix#", "Cms");
                            designerBuilder.AppendLine("        protected global::CmsWeb.ControlExt.RadioBoxListExt #ControlAlisa##ColName#;");
                            break;
                        case 6://多选框 checkbox
                            controlAlisa = "cb";
                            controlType = "CheckBox";
                            initDataBuilder.AppendLine("            if(entity.#ColName# != null )");
                            initDataBuilder.AppendLine("            {");
                            initDataBuilder.AppendLine("                #ControlAlisa##ColName#.Checked = entity.#ColName#  == Constants.IS_YES;");
                            initDataBuilder.AppendLine("            }");
                            saveCol = "            entity.#ColName# = #ControlAlisa##ColName#.Checked ? Constants.IS_YES : Constants.IS_NO;";
                            editCols = editCols.Replace("#tagPrefix#", "asp");
                            designerBuilder.AppendLine("        protected global::System.Web.UI.WebControls.CheckBox #ControlAlisa##ColName#;");
                            break;
                        case 7://单选框 radiobox
                            controlAlisa = "rb";
                            controlType = "RadioBox";
                            initDataBuilder.AppendLine("            if(entity.#ColName# != null )");
                            initDataBuilder.AppendLine("            {");
                            initDataBuilder.AppendLine("                #ControlAlisa##ColName#.Checked = entity.#ColName#  == Constants.IS_YES;");
                            initDataBuilder.AppendLine("            }");
                            saveCol = "            entity.#ColName# = #ControlAlisa##ColName#.SelectedValue;";
                            editCols = editCols.Replace("#tagPrefix#", "asp");
                            designerBuilder.AppendLine("        protected global::System.Web.UI.WebControls.RadioBox #ControlAlisa##ColName#;");
                            break;
                        case 8://文件上传
                            controlAlisa = "upl";
                            controlType = "UploadExt";
                            initDataBuilder.AppendLine("            #ControlAlisa##ColName#.Value = entity.#ColName#;");
                            saveCol = "            entity.#ColName# = #ControlAlisa##ColName#.Value;";
                            editCols = "                        <div class=\"col-lg-12\">\r\n";
                            editCols += "                            <div class=\"form-group\">\r\n";
                            editCols += "                                <label>#ColCnName#：</label>\r\n";
                            editCols += "                                <Cms:UploadExt ID=\"#ControlAlisa##ColName#\" runat=\"server\"></Cms:UploadExt>";
                            designerBuilder.AppendLine("        protected global::CmsWeb.ControlExt.UploadExt #ControlAlisa##ColName#;");
                            break;
                        case 9://富文本
                            controlAlisa = "edt";
                            controlType = "EditorExt";
                            initDataBuilder.AppendLine("            #ControlAlisa##ColName#.Text = entity.#ColName#;");
                            saveCol = "            entity.#ColName# = #ControlAlisa##ColName#.Text;";
                            editCols = "                        <div class=\"col-lg-12\">\r\n";
                            editCols += "                            <div class=\"form-group\">\r\n";
                            editCols += "                                <label>#ColCnName#：</label>\r\n";
                            editCols += "                                <Cms:EditorExt ID=\"#ControlAlisa##ColName#\" runat=\"server\"></Cms:EditorExt>";
                            designerBuilder.AppendLine("        protected global::CmsWeb.ControlExt.EditorExt #ControlAlisa##ColName#;");
                            break;
                        default://文本框
                            controlAlisa = "txt";
                            controlType = "TextBox";
                            initDataBuilder.AppendLine("            if(entity.#ColName# != null )");
                            initDataBuilder.AppendLine("            {");
                            initDataBuilder.AppendLine("                #ControlAlisa##ColName#.Text = entity.#ColName#.ToString();");
                            initDataBuilder.AppendLine("            }");
                            saveCol = "            entity.#ColName# = #ControlAlisa##ColName#.Text.Trim();";
                            editCols = editCols.Replace("#tagPrefix#", "asp");
                            designerBuilder.AppendLine("        protected global::System.Web.UI.WebControls.TextBox #ControlAlisa##ColName#;");
                            break;
                    }
                    editCols += "                            </div>\r\n";
                    editCols += "                        </div> ";
                    editCols = editCols.Replace("#ColName#", colEntity.colCode)
                                        .Replace("#ColCnName#", colEntity.colComment)
                                        .Replace("#ControlType#", controlType)
                                        .Replace("#ControlAlisa#", controlAlisa);
                    editFormBuilder.AppendLine(editCols);

                    initDataBuilder = initDataBuilder.Replace("#ControlAlisa#", controlAlisa).Replace("#ColName#", colEntity.colCode);

                    saveCol = saveCol.Replace("#ColName#", colEntity.colCode).Replace("#ControlAlisa#", controlAlisa);
                    saveBuilder.AppendLine(saveCol);

                    initDateBuilder = initDateBuilder.Replace("#ColName#", colEntity.colCode).Replace("#ControlAlisa#", controlAlisa);

                    designerBuilder = designerBuilder.Replace("#ColName#", colEntity.colCode).Replace("#ControlAlisa#", controlAlisa); ;
                    //表单校验
                    string validateFileds = @"                    <%=#ControlAlisa##ColName#.UniqueID%>: {
                                            validators: {
                                                #Validators#
                                            }
                                        },";
                    if (!colEntity.validate.IsEmpty())
                    {
                        string validators = string.Empty;
                        string[] validates = colEntity.validate.Split(new[] { "," }, StringSplitOptions.None);
                        foreach (string v in validates)
                        {
                            switch (v)
                            {
                                case "1"://不能为空
                                    validators += "notEmpty: {},";
                                    break;
                                case "2"://数字
                                    validators += "digits: {},";
                                    break;
                                case "3"://日期
                                    validators += "date: {format:\"YYYY/MM/DD\"},";
                                    break;
                                default://more
                                    break;
                            }
                        }
                        validateFileds = validateFileds.Replace("#ControlAlisa#", controlAlisa)
                            .Replace("#ColName#", colEntity.colCode)
                            .Replace("#Validators#", validators);
                        validatorBuilder.AppendLine(validateFileds);
                    }
                }
            }

            //infoContent.aspx
            string infoContent = FileUtil.ReadFile(InfoFilePath)
                .Replace("#EditForm#", editFormBuilder.ToString())
                .Replace("#ValidateFileds#", validatorBuilder.ToString())
                .Replace("#ClassName#", className)
                .Replace("#CnFileName#", cnFileName)
                .Replace("#InitDateControl#", initDateBuilder.ToString())
                .Replace("#FolderName#", entity.folderName);
            FileUtil.WriteFile(ViewOutputPath + className + "Info.aspx", infoContent);

            //infoContent.aspx.cs
            string infoCsContent = FileUtil.ReadFile(InfoCsFilePath)
                .Replace("#ClassName#", className)
                .Replace("#TableName#", entity.tableName)
                .Replace("#CnFileName#", cnFileName)
                .Replace("#Date#", date)
                .Replace("#FolderName#", entity.folderName)
                .Replace("#SaveData#", saveBuilder.ToString())
                .Replace("#InitData#", initDataBuilder.ToString());
            FileUtil.WriteFile(ViewOutputPath + className + "Info.aspx.cs", infoCsContent);

            //infoContent.aspx.designer.cs
            //固定添加保存按钮
            string infoDesignerContent = FileUtil.ReadFile(InfoDesignerFilePath)
                                                 .Replace("#ClassName#", className)
                                                 .Replace("#Designer#", designerBuilder.ToString());
            FileUtil.WriteFile(ViewOutputPath + className + "Info.aspx.designer.cs", infoDesignerContent);
        }

        /// <summary>
        /// 生成List文件
        /// </summary>
        /// <param name="className">类名</param>
        /// <param name="entity">实体</param>
        /// <param name="cnFileName">文件名</param>
        /// <param name="date">日期</param>
        private void CreateListFile(string className, GeneraEntity entity, string cnFileName, string date)
        {
            StringBuilder searchBuilder = new StringBuilder();
            StringBuilder listHeadBuilder = new StringBuilder();
            StringBuilder colConfigBuilder = new StringBuilder();
            StringBuilder designerBuilder = new StringBuilder();
            StringBuilder initDateBuilder = new StringBuilder();
            //List.aspx
            string listContent = FileUtil.ReadFile(ListFilePath)
                .Replace("#ClassName#", className)
                .Replace("#TableName# ", entity.tableName)
                .Replace("#AjaxName#", className)
                .Replace("#FolderName#", entity.folderName); //Ajax文件名
            foreach (GeneraColunm colEntity in entity.columns)
            {
                //搜索条件
                if (colEntity.isSelect == 1)
                {
                    string searchCols = "<div class=\"col-lg-4 form-group\">\r\n";
                    searchCols +=
                        "                                <asp:#ControlType# runat=\"server\" ID=\"#ControlAlisa##ColName#\" searchattr=\"#ColName#|LIKE|#ColName#\" CssClass=\"form-control\" placeholder=\"#ColCnName#\"></asp:#ControlType#>\r\n";
                    searchCols += "                            </div> ";
                    switch (colEntity.controlType)
                    {
                        case 2:
                            searchCols = searchCols.Replace("#ControlType#", "DropDownList").Replace("#ControlAlisa#", "ddl");
                            designerBuilder = designerBuilder.AppendLine("        protected global::System.Web.UI.WebControls.DropDownList ddl#ColName#;");
                            break;
                        case 3:
                            searchCols = searchCols.Replace("#ControlType#", "TextBox").Replace("#ControlAlisa#", "txt");
                            designerBuilder = designerBuilder.AppendLine("        protected global::System.Web.UI.WebControls.TextBox txt#ColName#;");
                            initDateBuilder.AppendLine("initDateControl(\"<%=txt#ColName#.ClientID%>\");");
                            break;
                        default:
                            searchCols = searchCols.Replace("#ControlType#", "TextBox").Replace("#ControlAlisa#", "txt");
                            designerBuilder = designerBuilder.AppendLine("        protected global::System.Web.UI.WebControls.TextBox txt#ColName#;");
                            break;
                    }
                    searchCols = searchCols.Replace("#ColName#", colEntity.colCode)
                        .Replace("#ColCnName#", colEntity.colComment);
                    searchBuilder.AppendLine(searchCols);
                    designerBuilder = designerBuilder.Replace("#ColName#", colEntity.colCode);
                    initDateBuilder = initDateBuilder.Replace("#ColName#", colEntity.colCode);

                }

                //表头级datatables列配置
                if (colEntity.isShowList == 1)
                {
                    listHeadBuilder.AppendLine("<th>" + colEntity.colComment + "</th>");
                    colConfigBuilder.AppendLine("                    { \"data\": \"" + colEntity.colCode + "\" },");
                }
            }
            listContent = listContent.Replace("#SearchCols#", searchBuilder.ToString())
                .Replace("#InitDateControl#", initDateBuilder.ToString())
                .Replace("#ListHead#", listHeadBuilder.ToString()) //datatables头部
                .Replace("#ColConfig#", colConfigBuilder.ToString()); //datatables列配置
            FileUtil.WriteFile(ViewOutputPath + className + "List.aspx", listContent);

            //List.aspx.designer.cs

            string listDesignerContent = FileUtil.ReadFile(ListDesignerFilePath)
                                                 .Replace("#ClassName#", className)
                                                 .Replace("#Designer#", designerBuilder.ToString());
            FileUtil.WriteFile(ViewOutputPath + className + "List.aspx.designer.cs", listDesignerContent);

            //List.aspx.cs
            string listCsContent = FileUtil.ReadFile(ListCsFilePath)
                .Replace("#ClassName#", className)
                .Replace("#TableName# ", entity.tableName)
                .Replace("#CnFileName#", cnFileName)
                .Replace("#Date#", date);
            FileUtil.WriteFile(ViewOutputPath + className + "List.aspx.cs", listCsContent);
        }

        /// <summary>
        /// 生成Api文件
        /// </summary>
        /// <param name="className">类名</param>
        /// <param name="entity">生成实体</param>
        /// <param name="cnFileName">文件名</param>
        /// <param name="date">日期</param>
        private void CreateApiFile(string className, GeneraEntity entity, string cnFileName, string date)
        {
            //Api.aspx.cs
            string ApiCsContent = FileUtil.ReadFile(ApiCsFilePath)
                .Replace("#ClassName#", className)
                .Replace("#TableName#", entity.tableName)
                .Replace("#CnFileName#", cnFileName)
                .Replace("#Date#", date);
            FileUtil.WriteFile(ApiOutputPath + className + "Api.aspx.cs", ApiCsContent);

            //Api.aspx
            string ApiContent = FileUtil.ReadFile(ApiFilePath).Replace("#ClassName#", className);
            FileUtil.WriteFile(ApiOutputPath + className + "Api.aspx", ApiContent);

            //Api.aspx.designer.cs
            string ApiDesignerContent = FileUtil.ReadFile(ApiDesignerFilePath)
                .Replace("#ClassName#", className);
            FileUtil.WriteFile(ApiOutputPath + className + "Api.aspx.designer.cs", ApiDesignerContent);
        }

    }
}