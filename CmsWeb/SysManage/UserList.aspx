﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="CmsWeb.UserList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <div id="searchPanel" class="panel panel-default">
                <div class="panel-heading">
                    查询条件
                    <div class="pull-right">
                        <a data-toggle="collapse" data-parent="#searchPanel" href="#searchBody">
                            <span class="glyphicon glyphicon-menu-up"></span>
                        </a>
                    </div>
                </div>
                <div id="searchBody" class="panel-body collapse in">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="col-lg-4 form-group">
                                <%--<label>用户账号</label>--%>
                                <asp:TextBox runat="server" ID="txtUserAccount" searchattr="UserAccount|LIKE|UserAccount" CssClass="form-control" placeholder="用户账号"></asp:TextBox>
                            </div>
                            <div class="col-lg-4 form-group">
                                <%--<label>用户名称</label>--%>
                                <asp:TextBox runat="server" ID="txtUserName" searchattr="UserName|LIKE|UserName" CssClass="form-control" placeholder="用户名称"></asp:TextBox>
                            </div>
                            <div class="col-lg-4 form-group">
                                <%--<label>用户状态</label>--%>
                                <asp:DropDownList runat="server" ID="ddlUserStatus" searchattr="UserStatus|=|UserStatus" CssClass="form-control" placeholder="用户状态"></asp:DropDownList>
                            </div>
                            <div class="col-lg-4 form-group">
                                <%--<label>用户类型</label>--%>
                                <asp:DropDownList runat="server" ID="ddlUserType" searchattr="UserType|=|UserType" CssClass="form-control" placeholder="用户类型"></asp:DropDownList>
                            </div>
                            <div class="col-lg-4 form-group">
                                <%--<label>创建日期</label>--%>
                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="col-lg-6">
                                            <div class="input-group">
                                                <asp:TextBox runat="server" ID="txtCreateTimeBegin" searchattr="CreateTime|>=|CreateTimeBegin" ReadOnly="True" CssClass="form-control" placeholder="创建开始日期"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-6">
                                            <asp:TextBox runat="server" ID="txtCreateTimeEnd" searchattr="CreateTime|<=|CreateTimeEnd" ReadOnly="True" CssClass="form-control" placeholder="创建开始结束"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <div class="pull-right">
                                    <input type="button" id="btnSearch" class="btn btn-default" value="查询"/>
                                    <input type="button" id="btnClear" class="btn btn-default" value="重置"/>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row" style="padding-bottom: 5px;">
        <div class="col-lg-12">
            <a id="btnAdd" class="btn btn-info" href="/SysManage/UserInfo.aspx"><span class="glyphicon glyphicon-plus"></span>新增</a>
            <a id="btnDelete" class="btn btn-danger"><span class="glyphicon glyphicon-trash"></span>删除</a>
            <a runat="server" class="btn btn-primary" onserverclick="btnExport_OnClick" href="javascript:void(0);"><span class="glyphicon glyphicon-export"></span>导出</a>
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
                                <input id="cbSelectAll" type="checkbox" title="全选/取消"/>
                            </th>
                            <th>操作</th>
                            <th>用户账号</th>
                            <th>用户名称</th>
                            <th>用户角色</th>
                            <th>用户状态</th>
                            <th>用户类型</th>
                            <th>最后一次登录时间</th>
                            <th>创建时间</th>
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
        var tableObj;
        //初始化表格
        $(function() {
            tableObj = $('#dataTables').DataTable({
                "processing": true,
                "serverSide": true,
                "searching": false,
                "ordering": true,
                "orderMulti": false,
                "select": true,
                "scrollX": true,
                "bLengthChange": false, //去掉每页显示多少条数据方法
                "aLengthMenu": [50, 100, 200],
                //"scrollY": "500px",
                "renderer": "bootstrap",
                "pagingType": "full_numbers",
                "rowId": "ID",
                "order": [2, "desc"],
                "ajax": function(data, callback) {
                    var param = getSearchParams(data);
                    param["method"] = "GetPagerList";
                    //ajax请求数据
                    $.ajax({
                        type: "POST",
                        url: "/API/UserApi.aspx",
                        cache: false, //禁用缓存
                        data: param, //传入组装的参数
                        dataType: "json",
                        success: function(result) {
                            if (result.result == 1) { //请求成功
                                callback(setDataTablesPagerParas(result, data));
                            } else if (result.result == 2) { //请求失败
                                toastr.error(result.message);
                            } else if (result.result == 3) { //登录超时
                                bootAlert.alert(result.message).on(function() {
                                    location.href = "/Login.aspx";
                                });
                            } else { //其他异常情况
                                toastr.error(result.message);
                            }
                        }
                    });
                },
                "columns": [
                    {
                        "data": "ID",
                        "orderable": false,
                        "render": function(data, type, row, meta) {
                            var result = "<input id=\"" +
                                data +
                                "\" name=\"tbCheckbox\" type=\"checkbox\" title=\"全选/取消\" />";
                            return result;
                        }
                    },
                    {
                        "data": "ID",
                        "orderable": false,
                        "render": function(data, type, row, meta) {
                            var result = "<a href=\"/SysManage/UserInfo.aspx?Id=" +
                                row.ID +
                                "\" style='margin-left:10px;'><span class='glyphicon glyphicon-edit' title='编辑'></span></a>&nbsp;&nbsp;&nbsp;<a href=\"javascript:deleteRows('" +
                                row.ID +
                                "');\"><span class='glyphicon glyphicon-trash' title='删除'></span></a>";
                            return result;
                        }
                    },
                    { "data": "UserAccount" },
                    { "data": "UserName" },
                    { "data": "UserType" },
                    { "data": "UserStatusName" },
                    { "data": "UserTypeName" },
                    {
                        "data": "LastLoginTime",
                        "render": function(data) {
                            if (data != null) {
                                return (new Date(data)).Format("yyyy/MM/dd hh:mm:ss");
                            } else {
                                return "";
                            }
                        }
                    },
                    {
                        "data": "CreateTime",
                        "render": function(data) {
                            return (new Date(data)).Format("yyyy/MM/dd hh:mm:ss");
                        }
                    }
                ]
            });
        });

        $(function() {
            //检索
            $("#btnSearch").click(function() {
                reloadData();
            });
            //清除检索条件
            $("#btnClear").click(function() {
                clearSearchForm();
            });
            //删除选中
            $("#btnDelete").click(function() {
                var ids = getSelectedRowIds();
                if (ids != "") {
                    deleteRows(ids);
                } else {
                    toastr.warning("请选择你要删除的数据！");
                }
            });

            initDateControl("<%= txtCreateTimeBegin.ClientID %>");
            initDateControl("<%= txtCreateTimeEnd.ClientID %>");
        });

        //重新加载数据
        function reloadData() {
            tableObj.ajax.reload(null, false);
        };

        //删除行数据
        function deleteRows(data) {
            bootAlert.confirm({
                message: "确认要删除选中数据吗？"
            }).on(function(result) {
                if (result) {
                    var param = {};
                    param["method"] = "DeleteByIds";
                    param["Id"] = data;
                    $.ajax({
                        type: "POST",
                        url: "/API/UserApi.aspx",
                        cache: false, //禁用缓存
                        data: param, //传入组装的参数
                        dataType: "json",
                        success: function (result) {
                            if (result.result == 1) { //请求成功
                                toastr.success("删除成功！");
                                reloadData();
                            } else if (result.result == 2) { //请求失败
                                toastr.error(result.message);
                            } else if (result.result == 3) { //登录超时
                                bootAlert.alert(result.message).on(function () {
                                    location.href = "/Login.aspx";
                                });
                            } else { //其他异常情况
                                toastr.error(result.message);
                            }
                        }
                    });
                }
            });
        }
    </script>
</asp:Content>