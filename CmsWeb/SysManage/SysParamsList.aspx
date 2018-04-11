<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="SysParamsList.aspx.cs" Inherits="CmsWeb.SysParamsList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- /.row -->
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <!-- /.panel-heading -->
                <div class="panel-body">
                    <table width="100%" class="table table-striped table-bordered table-hover table-condensed" id="dataTables">
                        <thead>
                            <tr>
                                <th>
                                    <input id="cbSelectAll" type="checkbox" title="全选/取消" /></th>
                                <th>操作</th>
                                <th>参数编码</th>
                                <th>参数描述</th>
                                <th>参数值</th>
                                <th>更新时间</th>

                            </tr>
                        </thead>
                    </table>
                </div>
                <!-- /.panel-body -->
            </div>
            <!-- /.panel -->
        </div>
        <!-- /.col-lg-12 -->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script type="text/javascript">
        var curTable;
        //初始化表格
        $(function () {
            var options = {
                "url": "/API/SysManage/SysParamsApi.aspx",
                "editUrl": "/SysManage/SysParamsInfo.aspx",
                "isExport": false,
                "isComplexSearch": false,
                "aLengthMenu":<%= CmsUtils.Configs.GetValue("LengthMenu")%>,
                "searchColunms": "ParamName|ParamValue",
                "columns": [
                    {
                        "data": "ID",
                        "width": "20px",
                        "class": "center",
                        "orderable": false,
                        "render": function (data, type, row, meta) {
                            var result = "<input id=\"" + data + "\" name=\"tbCheckbox\" type=\"checkbox\" title=\"全选/取消\" />";
                            return result;
                        }
                    },
                    {
                        "data": "ID",
                        "width": "40px",
                        "class": "center",
                        "orderable": false,
                        "render": function (data, type, row, meta) {
                            var result = "<a href=\"javascript:curTable.edit(" + row.ID +
                                ")\"><span class='glyphicon glyphicon-edit' title='编辑'></span></a>&nbsp;&nbsp;&nbsp;<a href=\"javascript:curTable.delSingleRow('" +
                                row.ID +
                                "');\"><span class='glyphicon glyphicon-trash' title='删除'></span></a>";
                            return result;
                        }
                    },
                    { "data": "ParamName" },
                    { "data": "ParamDesc" },
                    { "data": "ParamValue" },
                    { "data": "UpdateTime" },

                ]
            };
            curTable = new TableUtils(options).initTable();
        });
    </script>
</asp:Content>


