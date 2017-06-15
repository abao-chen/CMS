<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="CmsWeb.SysConfig.UserList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="page-wrapper">
        <div class="row">
            <div class="col-lg-12">
                <div id="searchPanel" class="panel panel-default">
                    <div class="panel-heading">
                        <a data-toggle="collapse" data-parent="#searchPanel" href="#searchBody">查询条件<span class="glyphicon glyphicon-menu-up"></span></a>
                    </div>
                    <div id="searchBody" class="panel-body collapse in">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="col-lg-4 form-group">
                                    <label>用户账号:</label>
                                    <asp:TextBox runat="server" ID="txtUserAccount" searchattr="UserAccount|LIKE|UserAccount" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-lg-4 form-group">
                                    <label>用户名称:</label>
                                    <asp:TextBox runat="server" ID="txtUserName" searchattr="UserName|LIKE|UserName" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-lg-4 form-group">
                                    <label>用户状态:</label>
                                    <asp:DropDownList runat="server" ID="ddlUserStatus" searchattr="UserStatus|=|UserStatus" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-lg-4 form-group">
                                    <label>用户类型:</label>
                                    <asp:DropDownList runat="server" ID="ddlUserType" searchattr="UserType|=|UserType" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-lg-4 searchPanel">
                                    <input type="button" id="btnClear" class="btn btn-default" value="Clear" />
                                    <input type="button" id="btnSearch" class="btn btn-default" value="Search" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <a id="btnAdd" class="btn btn-default" href="/SysConfig/UserInfo.aspx">新增</a>
                <input type="button" id="btnDelete" class="btn btn-default" value="删除" />
            </div>
        </div>
        <!-- /.row -->
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <!-- /.panel-heading -->
                    <div class="panel-body">
                        <table width="100%" class="table table-striped table-bordered table-hover" id="dataTables">
                            <thead>
                                <tr>
                                    <th>
                                        <input id="cbSelectAll" type="checkbox" title="全选/取消" /></th>
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
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script type="text/javascript">
        var tableObj;
        //初始化表格
        $(function () {
            tableObj = $('#dataTables').DataTable({
                "processing": true,
                "serverSide": true,
                "searching": false,
                "ordering": true,
                "orderMulti": false,
                "select": true,
                "scrollX": true,
                "bLengthChange": false,   //去掉每页显示多少条数据方法
                "aLengthMenu": [50, 100, 200],
                //"scrollY": "500px",
                "renderer": "bootstrap",
                "pagingType": "full_numbers",
                "rowId": "ID",
                "order": [1, "desc"],
                "ajax": function (data, callback) {
                    var param = getSearchParams(data);
                    param["method"] = "GetPagerList";
                    //ajax请求数据
                    $.ajax({
                        type: "POST",
                        url: "/API/SystemApi.aspx",
                        cache: false,  //禁用缓存
                        data: param,  //传入组装的参数
                        dataType: "json",
                        success: function (result) {
                            callback(setDataTablesPagerParas(result, data));
                        }
                    });
                },
                "columns": [
                    {
                        "data": "ID",
                        "orderable": false,
                        "render": function (data, type, row, meta) {
                            var result = "<input id=\"" + data + "\" name=\"tbCheckbox\" type=\"checkbox\" title=\"全选/取消\" />";
                            return result;
                        }
                    },
                    {
                        "data": "ID",
                        "orderable": false,
                        "render": function (data, type, row, meta) {
                            var result = "<a class=\"btn btn-primary btn-xs\" href=\"/SysConfig/UserInfo.aspx?Id=" + data + "\">编辑</a>　<a class=\"btn btn-danger btn-xs\" href=\"javascript:deleteRows('" + data + "');\">删除</a>　　";
                            return result;
                        }
                    },
                    { "data": "UserAccount" },
                    { "data": "UserName" },
                    { "data": "UserType" },
                    { "data": "UserStatus" },
                    { "data": "UserType" },
                    {
                        "data": "LastLoginTime",
                        "render": function (data) {
                            if (data != null) {
                                return (new Date(data)).Format("yyyy/MM/dd hh:mm:ss");
                            } else {
                                return "";
                            }
                        }
                    },
                    {
                        "data": "CreateTime",
                        "render": function (data) {
                            return (new Date(data)).Format("yyyy/MM/dd hh:mm:ss");
                        }
                    }
                ]
            });
        });

        //重新加载数据
        function reloadData() {
            tableObj.ajax.reload(null
                , false);
        };

        $(function () {
            $("#btnSearch").click(function () {
                reloadData();
            });
            $("#btnClear").click(function () { });
        });

        //删除行数据
        function deleteRows(data) {
            var param = {};
            param["method"] = "DeleteUser";
            param["Id"] = data;
            $.ajax({
                type: "POST",
                url: "/API/SystemApi.aspx",
                cache: false,  //禁用缓存
                data: param,  //传入组装的参数
                dataType: "json",
                success: function () {
                    reloadData();
                }
            });
        }
    </script>
</asp:Content>

