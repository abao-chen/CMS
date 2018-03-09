<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="CmsWeb.UserList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <div id="searchPanel" class="panel panel-default hide">
                <div id="searchBody" class="panel-body">
                    <div class="row">
                        <div class="col-lg-6 form-group">
                            <label>用户账号</label>
                            <asp:TextBox runat="server" ID="txtUserAccount" searchattr="UserAccount|LIKE|UserAccount" CssClass="form-control" placeholder="用户账号"></asp:TextBox>
                        </div>
                        <div class="col-lg-6 form-group">
                            <label>用户名称</label>
                            <asp:TextBox runat="server" ID="txtUserName" searchattr="UserName|LIKE|UserName" CssClass="form-control" placeholder="用户名称"></asp:TextBox>
                        </div>
                        <div class="col-lg-6 form-group">
                            <label>用户状态</label>
                            <asp:DropDownList runat="server" ID="ddlUserStatus" searchattr="UserStatus|=|UserStatus" CssClass="form-control" placeholder="用户状态"></asp:DropDownList>
                        </div>
                        <div class="col-lg-6 form-group">
                            <label>用户类型</label>
                            <asp:DropDownList runat="server" ID="ddlUserType" searchattr="UserType|=|UserType" CssClass="form-control" placeholder="用户类型"></asp:DropDownList>
                        </div>
                        <div class="col-lg-6 form-group">
                            <label>创建开始日期</label>
                            <Cms:DataPickerExt ID="txtCreateTimeBegin" runat="server" Name="CreateTimeBegin" Format="yyyy/MM/dd" PlaceHolder="创建开始日期" SearchAttr="CreateTime|>=|CreateTimeBegin"></Cms:DataPickerExt>
                        </div>
                        <div class="col-lg-6 form-group">
                            <label>创建结束日期</label>
                            <Cms:DataPickerExt ID="txtCreateTimeEnd" runat="server" Name="CreateTimeEnd" Format="yyyy/MM/dd" PlaceHolder="创建结束日期" SearchAttr="CreateTime|<=|CreateTimeBegin"></Cms:DataPickerExt>
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
                    <table cellspacing="0" width="100%" class="table table-striped table-bordered table-hover table-condensed" id="dataTables">
                        <thead>
                            <tr>
                                <th>
                                    <input id="cbSelectAll" type="checkbox" title="全选/取消" />
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
        var curTable;
        //初始化表格
        $(function () {
            var options = {
                "url": "/API/UserApi.aspx",
                "editUrl": "/SysManage/UserInfo.aspx",
                "aLengthMenu":<%= CmsUtils.Configs.GetValue("LengthMenu")%>,
                "searchColunms":"UserAccount|UserName",
                "columns": [
                    {
                        "data": "ID",
                        "orderable": false,
                        "render": function (data, type, row, meta) {
                            var result = "<input id=\"" +
                                data +
                                "\" name=\"tbCheckbox\" type=\"checkbox\" title=\"全选/取消\" />";
                            return result;
                        }
                    },
                    {
                        "data": "ID",
                        "orderable": false,
                        "render": function (data, type, row, meta) {
                            var result = "<a href=\"javascript:curTable.edit(" + row.ID +
                                ")\" style='margin-left:10px;'><span class='glyphicon glyphicon-edit' title='编辑'></span></a>&nbsp;&nbsp;&nbsp;<a href=\"javascript:curTable.delSingleRow(curTable.settings.delMethod,'" +
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
                    { "data": "LastLoginTime" },
                    { "data": "CreateTime" }
                ]
            };
            curTable = tableUtils.initTable(options);
        });
    </script>
</asp:Content>
