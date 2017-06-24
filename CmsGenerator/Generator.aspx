<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Generator.aspx.cs" Inherits="CmsGenerator.Generator" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Button ID="btnGeneratorView" runat="server" Text="生成页面" OnClick="btnGeneratorView_OnClick" />
        <asp:HiddenField ID="hidTablesName" runat="server" />
        <asp:Button ID="btnGeneratorBDE" runat="server" Text="生成三层" OnClick="btnGeneratorBDE_OnClick" />
        <asp:CheckBoxList ID="cbTables" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" RepeatLayout="Flow" OnSelectedIndexChanged="cbTables_SelectedIndexChanged"></asp:CheckBoxList>
        <table>
            <asp:Repeater ID="rpTables" runat="server" OnItemDataBound="rpTables_ItemDataBound">
                <ItemTemplate>
                    <tr>
                        <td>
                            <table id="<%#Eval("TABLE_NAME") %>" name="parentTable">
                                <thead>
                                    <tr>
                                        <td><%#Eval("TABLE_NAME") %>(<%#Eval("TABLE_COMMENT") %>)文件夹名称：<input type="text" value="" /></td>
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
                                                        <option value="3">日期</option>
                                                    </select>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="dicType" runat="server"></asp:DropDownList>
                                                </td>
                                                <td>
                                                    <input name="validate" type="checkbox" value="1" />必填
                                                    <input name="validate" type="checkbox" value="2" />数字
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

        <script src="Script/jquery.min.js"></script>
        <script src="Script/JSON2.js"></script>
        <script type="text/javascript">
            $(function () {
                $("#<%=btnGeneratorView.ClientID%>").click(function () {
                    var tableObjArray = [];
                    var parentTable = $("table[name='parentTable']");
                    var tablesName;
                    $(parentTable).each(function () {
                        var tableObj = {};
                        tableObj["columns"] = [];
                        tableObj["tableName"] = $(this).attr("id");
                        if (tablesName) {
                            tablesName += "," + $(this).attr("id") + "";
                        } else {
                            tablesName = "" + $(this).attr("id") + "";
                        }
                        $("#" + tableObj["tableName"] + " tr").each(function () {
                            if ($(this).attr("id")) {
                                var colObj = {};
                                colObj["colCode"] = $(this).attr("id");
                                $("#" + $(this).attr("id") + " input,select").each(function () {
                                    var colName = $(this).attr("name");
                                    if ($(this)[0].type == "text" || $(this)[0].type == "select-one") {
                                        colObj[colName] = $(this).val();
                                    } else if ($(this)[0].type == "checkbox") {
                                        if ($(this).is(":checked")) {
                                            colObj[colName] = $(this).val();
                                        }
                                    }
                                });
                                tableObj.columns.push(colObj);
                            }
                        });
                        tableObjArray.push(tableObj);
                    });

                    $("#<%=hidTablesName.ClientID%>").val(tablesName);
                });
            })
        </script>
    </form>
</body>
</html>
