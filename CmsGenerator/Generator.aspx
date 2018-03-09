<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Generator.aspx.cs" Inherits="CmsGenerator.Generator" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />

    <title></title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server">
        <div class="row">
            <asp:Button ID="btnShowPageConfig" runat="server" Text="显示页面配置" CssClass="btn btn-default" OnClick="btnShowPageConfig_Click" />
            <asp:Button ID="btnGeneratorView" runat="server" Text="生成页面" CssClass="btn btn-default" OnClick="btnGeneratorView_OnClick" />
            <asp:Button ID="btnGeneratorBDE" runat="server" Text="生成三层" CssClass="btn btn-default" OnClick="btnGeneratorBDE_OnClick" />

            <asp:HiddenField ID="hidTablesName" runat="server" />
            <asp:HiddenField ID="hidFormData" runat="server" />
        </div>
        <div class="row">
            <input id="selectAll" type="checkbox" /><label for="selectAll">全选</label>
            <Cms:CheckBoxListExt ID="cbTables" Name="cbtbl" runat="server"></Cms:CheckBoxListExt>
        </div>
        <div class="row">
            <table>
                <asp:Repeater ID="rpTables" runat="server" OnItemDataBound="rpTables_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <table id="<%#Eval("TABLE_NAME") %>" name="parentTable">
                                    <thead>
                                        <tr>
                                            <td colspan="8"><%#Eval("TABLE_NAME") %><input type="text" name="tableComment" value="<%#Eval("TABLE_COMMENT") %>" />文件夹名称：<input type="text" name="folderName" value="" /></td>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>字段Code</td>
                                            <td>字段名称</td>
                                            <td>是否筛选</td>
                                            <td>是否编辑</td>
                                            <td>列表显示</td>
                                            <td>控件类型</td>
                                            <td>字典类型</td>
                                            <td>验证</td>
                                        </tr>
                                        <!--各表中的字段显示-->
                                        <asp:Repeater ID="rpCols" runat="server" OnItemDataBound="rpCols_ItemDataBound">
                                            <ItemTemplate>
                                                <tr id="<%#Eval("COLUMN_NAME") %>">
                                                    <td><%#Eval("COLUMN_NAME") %></td>
                                                    <td>
                                                        <input name="colComment" type="text" value="<%#Eval("COLUMN_COMMENT") %>" /></td>
                                                    <td>
                                                        <input name="isSelect" type="checkbox" value="1" /></td>
                                                    <td>
                                                        <input name="isEdit" type="checkbox" value="1" /></td>
                                                    <td>
                                                        <input name="isShowList" type="checkbox" value="1" /></td>
                                                    <td>
                                                        <select name="controlType">
                                                            <option value="1">文本框</option>
                                                            <option value="2">下拉框</option>
                                                            <option value="3">日历框</option>
                                                            <option value="4">多选组</option>
                                                            <option value="5">单选组</option>
                                                            <option value="6">多选</option>
                                                            <option value="7">单选</option>
                                                            <option value="8">文件上传</option>
                                                            <option value="9">富文本</option>
                                                        </select>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="dicType" runat="server"></asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <input name="validate" type="checkbox" value="1" />必填
                                                    <input name="validate" type="checkbox" value="2" />数字
                                                    <input name="validate" type="checkbox" value="3" />日期
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                    </FooterTemplate>
                </asp:Repeater>
            </table>
        </div>

        <script src="Scripts/jquery-1.9.1.min.js"></script>
        <script src="Scripts/bootstrap.min.js"></script>
        <script src="Scripts/JSON2.js"></script>
        <script type="text/javascript">
            $(function () {
                $("#<%=btnGeneratorView.ClientID%>").click(function () {
                    createFormJsonData();
                });

                $("#selectAll").click(function () {
                    var selectAll = this;
                    $("input[name='cbtbl']").each(function () {
                        $(this).prop("checked", $(selectAll).is(":checked"));
                        //$(this).click();
                    });
                });
            });

            function createFormJsonData() {
                var tableObjArray = [];
                var parentTable = $("table[name='parentTable']");
                var tablesName;
                $(parentTable).each(function () {
                    var _thisTb = this;
                    var tableObj = {};
                    tableObj["columns"] = [];
                    tableObj["tableName"] = $(_thisTb).attr("id");
                    tableObj["folderName"] = $("input[name='folderName']", _thisTb).val();
                    tableObj["tableComment"] = $("input[name='tableComment']", _thisTb).val();
                    if (tablesName) {
                        tablesName += "," + $(_thisTb).attr("id") + "";
                    } else {
                        tablesName = "" + $(_thisTb).attr("id") + "";
                    }
                    $("#" + tableObj["tableName"] + " tr").each(function () {
                        var _thisCol = this;
                        var colId = $(_thisCol).attr("id");
                        if (colId) {
                            var colObj = {};
                            colObj["colCode"] = $(_thisCol).attr("id");
                            $($("#" + colId + " input," + "#" + colId + " select")).each(function () {
                                var colName;
                                var colNameArray = $(this).attr("name").split("$");
                                if (colNameArray.length > 0) {
                                    colName = colNameArray[colNameArray.length - 1];
                                } else {
                                    colName = $(this).attr("name");
                                }
                                var ctrlType = $(this)[0].type;
                                if (ctrlType == "text" || ctrlType == "select-one") {
                                    colObj[colName] = $(this).val();
                                } else if (ctrlType == "checkbox") {
                                    if ($(this).is(":checked")) {
                                        if ($(this).attr("name") == "validate") {
                                            if (colObj[colName]) {
                                                colObj[colName] += "," + $(this).val();
                                            } else {
                                                colObj[colName] = $(this).val();
                                            }
                                        } else {
                                            colObj[colName] = $(this).val();
                                        }
                                    }
                                }
                            });
                            tableObj.columns.push(colObj);
                        }
                    });
                    tableObjArray.push(tableObj);
                });
                console.log(JSON.stringify(tableObjArray))
                $("#<%=hidFormData.ClientID%>").val(JSON.stringify(tableObjArray));
                $("#<%=hidTablesName.ClientID%>").val(tablesName);
            }
        </script>
    </form>
</body>
</html>
