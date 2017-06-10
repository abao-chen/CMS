﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="CmsWeb.SystemApi.UserList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="page-wrapper">
        <div class="row">
            <div class="col-lg-12">
                <div id="searchPanel" class="panel panel-default">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="col-lg-4 form-group">
                                    <label>用户账号:</label>
                                    <asp:textbox runat="server" id="txtUserAccount" searchattr="UserAccount|LIKE|UserAccount" cssclass="form-control"></asp:textbox>
                                </div>
                                <div class="col-lg-4 form-group">
                                    <label>用户名称:</label>
                                    <asp:textbox runat="server" id="txtUserName" searchattr="UserName|LIKE|UserName" cssclass="form-control"></asp:textbox>
                                </div>
                                <div class="col-lg-4 form-group">
                                    <label>用户状态:</label>
                                    <asp:dropdownlist runat="server" id="ddlUserStatus" searchattr="UserStatus|=|UserStatus"></asp:dropdownlist>
                                </div>
                                <div class="col-lg-4 form-group">
                                    <label>用户类型:</label>
                                    <asp:dropdownlist runat="server" id="ddlUserType" searchattr="UserType|=|UserType"></asp:dropdownlist>
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

        <!-- /.row -->
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <!-- /.panel-heading -->
                    <div class="panel-body">
                        <table width="100%" class="table table-striped table-bordered table-hover" id="dataTables">
                            <thead>
                                <tr>
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
                "scrollX": false,
                "aLengthMenu": [50, 100, 200],
                "scrollY": "500px",
                "renderer": "bootstrap",
                "pagingType": "full_numbers",
                "rowId": "ID",
                "order": [1, "desc"],
                "ajax": function (data, callback, settings) {
                    var param = getSearchParams(data);
                    //ajax请求数据
                    $.ajax({
                        type: "POST",
                        url: "/API/SystemApi.aspx",
                        cache: false,  //禁用缓存
                        data: param,  //传入组装的参数
                        dataType: "json",
                        success: function (result) {
                            callback(setDataTablesPagerParas(result));
                        }
                    });
                },
                "columns": [
                    { "data": "UserAccount" },
                    { "data": "UserName" },
                    { "data": "RoleName" },
                    { "data": "UserStatus" },
                    {
                        "data": "LastLoginTime",
                        "render": function (data, type, row, meta) {
                            return (new Date(data)).Format("yyyy/MM/dd hh:mm:ss");
                        }
                    },
                    { "data": "UserType" }
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


    </script>
</asp:Content>
