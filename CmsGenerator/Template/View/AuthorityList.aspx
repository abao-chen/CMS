<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="AuthorityList.aspx.cs" Inherits="CmsWeb.AuthorityList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                                <asp:TextBox runat="server" ID="txtAuthorType" searchattr="AuthorType|=|AuthorType" CssClass="form-control" placeholder="权限类型
            1：模块，2：页面，3：按钮"></asp:TextBox>
                            </div> 
<div class="col-lg-4 form-group">
                                <asp:TextBox runat="server" ID="txtParentID" searchattr="ParentID|=|ParentID" CssClass="form-control" placeholder="父级权限ID"></asp:TextBox>
                            </div> 
<div class="col-lg-4 form-group">
                                <asp:TextBox runat="server" ID="txtFullID" searchattr="FullID|=|FullID" CssClass="form-control" placeholder="权限全路径"></asp:TextBox>
                            </div> 

                                <div class="col-lg-4">
                                    <div class="pull-right">
                                        <input type="button" id="btnSearch" class="btn btn-default" value="查询" />
                                        <input type="button" id="btnClear" class="btn btn-default" value="重置" />
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
            <a id="btnAdd" class="btn btn-info" href="/#Folder#/AuthorityInfo.aspx"><span class="glyphicon glyphicon-plus"></span>新增</a>
            <a id="btnDelete" class="btn btn-danger"><span class="glyphicon glyphicon-trash"></span>删除</a>
            <a runat="server" class="btn btn-info" onserverclick="btnExport_OnClick" href="javascript:void(0);"><span class="glyphicon glyphicon-export"></span>导出</a>
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
                                <th>AuthorType</th>
<th>ParentID</th>
<th>FullID</th>

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
                "rowId": "#KeyId#",
                "order": [2, "desc"],
                "ajax": function (data, callback) {
                    var param = getSearchParams(data);
                    param["method"] = "GetPagerList";
                    //ajax请求数据
                    $.ajax({
                        type: "POST",
                        url: "/API/AuthorityApi.aspx",
                        cache: false,  //禁用缓存
                        data: param,  //传入组装的参数
                        dataType: "json",
                        success: function (result) {
                            if (result.result == 1) {//请求成功
                                callback(setDataTablesPagerParas(result, data));
                            }else if (result.result == 2) {//请求失败
                                toastr.error(result.message);
                            } else if (result.result == 3) {//登录超时
                                bootAlert.alert(result.message).on(function() {
                                    location.href = "/Login.aspx";
                                });
                            } else {//其他异常情况
                                toastr.error(result.message);
                            }
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
                            var result = "<a href=\"/#Folder#/AuthorityInfo.aspx?Id=" + data + "\" style='margin-left:10px;'><span class='glyphicon glyphicon-edit' title='编辑'></span></a>&nbsp;&nbsp;&nbsp;<a href=\"javascript:deleteRows('" + data + "');\"><span class='glyphicon glyphicon-trash' title='删除'></span></a>";
                            return result;
                        }
                    },
                    { "data": "AuthorType" },
                    { "data": "ParentID" },
                    { "data": "FullID" },

                ]
            });
        });

        $(function () {
            //检索
            $("#btnSearch").click(function () {
                reloadData();
            });
            //清除检索条件
            $("#btnClear").click(function () {
                clearSearchForm();
            });
            //删除选中
            $("#btnDelete").click(function () {
                var ids = getSelectedRowIds();
                if (ids != "") {
                    deleteRows(ids);
                } else {
                    toastr.warning("请选择你要删除的数据！");
                }
            });
        });

        //重新加载数据
        function reloadData() {
            tableObj.ajax.reload(null
                , false);
        };

        //删除行数据
        function deleteRows(data) {
            bootbox.confirm({
                size: "small",
                message: "确认要删除选中数据吗？",
                callback: function (result) {
                    if (result) {
                        var param = {};
                        param["method"] = "DeleteByIds";
                        param["Id"] = data;
                        $.ajax({
                            type: "POST",
                            url: "/API/AuthorityApi.aspx",
                            cache: false,  //禁用缓存
                            data: param,  //传入组装的参数
                            dataType: "json",
                            success: function () {
                                toastr.success("删除成功！");
                                reloadData();
                            }
                        });
                    }
                }
            });
        }
    </script>
</asp:Content>


