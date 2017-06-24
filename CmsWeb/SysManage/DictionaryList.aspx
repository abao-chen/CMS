<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="DictionaryList.aspx.cs" Inherits="CmsWeb.DictionaryList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link href="/Scripts/bootstrap/vendor/zTree/css/zTreeStyle/zTreeStyle.css" rel="stylesheet" />--%>
    <%--<link href="../Scripts/bootstrap/vendor/zTree/css/metroStyle/metroStyle.css" rel="stylesheet" />--%>
    <link href="../Scripts/bootstrap/vendor/zTree/css/zTreeStyle/metro.css" rel="stylesheet" />
    <style type="text/css">
        .ztree li span.button.switch.level0 {
            visibility: hidden;
            width: 1px;
        }

        .ztree li ul.level0 {
            padding: 0;
            background: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="searchPanel" style="display: none;">
        <div class="col-lg-4 form-group">
            <asp:TextBox runat="server" ID="txtDicTypeCode" searchattr="DicTypeCode|=|DicTypeCode" CssClass="form-control" placeholder="字典类型" ClientIDMode="Static"></asp:TextBox>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-2">
            <ul id="dicTree" class="ztree"></ul>
        </div>
        <div class="col-lg-10">
            <div class="row" style="padding-bottom: 5px; padding-top: 5px;">
                <div class="col-lg-12">
                    <a id="btnAdd" class="btn btn-info" href="/SysManage/DictionaryInfo.aspx"><span class="glyphicon glyphicon-plus"></span>新增</a>
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
                                            <input id="cbSelectAll" type="checkbox" title="全选/取消" />
                                        </th>
                                        <th>操作</th>
                                        <th>字典类型</th>
                                        <th>字典名称</th>
                                        <th>字典编码</th>
                                        <th>是否启用</th>

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
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <%--<script src="/Scripts/bootstrap/vendor/zTree/js/jquery.ztree.all.min.js"></script>--%>
    <script src="../Scripts/bootstrap/vendor/zTree/js/jquery.ztree.core.js"></script>
    <%-- <script src="/Scripts/bootstrap/vendor/zTree/js/jquery.ztree.excheck.min.js"></script>
    <script src="/Scripts/bootstrap/vendor/zTree/js/jquery.ztree.exedit.min.js"></script>
    <script src="/Scripts/bootstrap/vendor/zTree/js/jquery.ztree.exhide.min.js"></script>--%>
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
                "bLengthChange": false, //去掉每页显示多少条数据方法
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
                        url: "/API/DictionaryApi.aspx",
                        cache: false, //禁用缓存
                        data: param, //传入组装的参数
                        dataType: "json",
                        success: function (result) {
                            if (result.result == 1) { //请求成功
                                callback(setDataTablesPagerParas(result, data));
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
                },
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
                            var result = "<a href=\"/SysManage/DictionaryInfo.aspx?Id=" +
                                data +
                                "\" style='margin-left:10px;'><span class='glyphicon glyphicon-edit' title='编辑'></span></a>&nbsp;&nbsp;&nbsp;<a href=\"javascript:deleteRows('" +
                                data +
                                "');\"><span class='glyphicon glyphicon-trash' title='删除'></span></a>";
                            return result;
                        }
                    },
                    { "data": "DicTypeCode" },
                    { "data": "DicName" },
                    { "data": "DicCode" },
                    { "data": "IsUsing" },
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

        //初始化树控件
        $(function () {
            var param = {};
            param["method"] = "GetTreeList";
            $.ajax({
                type: "POST",
                url: "/API/DicTypeApi.aspx",
                cache: false, //禁用缓存
                data: param, //传入组装的参数
                dataType: "json",
                success: function (result) {
                    if (result.result == 1) { //请求成功
                        console.log(result.data);
                        var treeObj = $.fn.zTree.init($("#dicTree"),
                            {
                                data: {
                                    simpleData: {
                                        enable: true,
                                        idKey: "ID",
                                        pidKey: "pId"
                                    },
                                    key: {
                                        name: "DicTypeName"
                                    }
                                },
                                callback: {
                                    onClick: onClickNode
                                }

                            }, result.data);
                        treeObj.expandAll(true);
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
        });

        function onClickNode(event, treeId, treeNode, clickFlag) {
            $("#txtDicTypeCode").val(treeNode.DicTypeCode);
            reloadData();
        }

        //重新加载数据
        function reloadData() {
            tableObj.ajax.reload(null, false);
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
                            url: "/API/DictionaryApi.aspx",
                            cache: false, //禁用缓存
                            data: param, //传入组装的参数
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
