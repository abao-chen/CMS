﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="ContentTypeList.aspx.cs" Inherits="Cms.Web.Admin.ContentTypeList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <div id="searchPanel" class="panel panel-default hide">
                <div id="searchBody" class="panel-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="col-lg-6 form-group">
                                <label>类型名称</label>
                                <asp:TextBox runat="server" ID="txtTypeName" searchattr="TypeName|=|TypeName" CssClass="form-control" placeholder="类型名称"></asp:TextBox>
                            </div>
                            <div class="col-lg-6 form-group">
                                <label>类型别名</label>
                                <asp:TextBox runat="server" ID="txtTypeAlias" searchattr="TypeAlias|=|TypeAlias" CssClass="form-control" placeholder="类型别名"></asp:TextBox>
                            </div>

                        </div>
                    </div>
                </div>
                <div class="panel-footer navbar-fixed-bottom">
                    <div class="row" style="text-align: center;">
                        <input type="button" id="btnSearch" class="btn btn-default" style="margin: 0 auto;" value="查询" />
                        <input type="button" id="btnClear" class="btn btn-default" style="margin: 0 auto;" value="重置" />
                    </div>
                </div>
            </div>
        </div>
    </div>
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
                                <th>类型名称</th>
                                <th>类型别名</th>
                                <th>是否启用</th>
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
                "url": "/API/ContentManage/ContentTypeApi.aspx",
                "editUrl": "/ContentManage/ContentTypeInfo.aspx",
                "isExport": false,
                "isComplexSearch":false,
                "aLengthMenu":<%= CmsUtils.Configs.GetValue("LengthMenu")%>,
                "searchColunms": "TypeName|TypeAlias",
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
                    { "data": "TypeName" },
                    { "data": "TypeAlias" },
                    { "data": "IsUse" },
                    { "data": "UpdateTime" },

                ]
            };
            curTable = new TableUtils(options).initTable();
        });
    </script>
</asp:Content>


