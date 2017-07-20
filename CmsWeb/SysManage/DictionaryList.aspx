<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="DictionaryList.aspx.cs" Inherits="CmsWeb.DictionaryList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Scripts/bootstrap/vendor/zTree/css/metroStyle/metroStyle.css" rel="stylesheet" />
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
                    <a id="btnAdd" class="btn btn-info"><span class="glyphicon glyphicon-plus"></span>新增</a>
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
    <script src="/Scripts/bootstrap/vendor/zTree/js/jquery.ztree.all.js"></script>
    <script type="text/javascript">
        var tableObj;
        var treeObj;
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
                        "width": "4%",
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
                        "width": "8%",
                        "orderable": false,
                        "render": function (data, type, row, meta) {
                            var result = "<a href=\"javascript:update(" + data +
                                ")\" style='margin-left:10px;'><span class='glyphicon glyphicon-edit' title='编辑'></span></a>&nbsp;&nbsp;&nbsp;<a href=\"javascript:deleteRows('" +
                                data + "');\"><span class='glyphicon glyphicon-trash' title='删除'></span></a>";
                            return result;
                        }
                    },
                    { "data": "DicTypeName" },
                    { "data": "DicName" },
                    { "data": "DicCode" },
                    {
                        "data": "IsUsing",
                        "orderable": false,
                        "render": function (data, type, row, meta) {
                            if (data == 1) {
                                return "是";
                            } else {
                                return "否";
                            }
                        }
                    }
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
                        treeObj = $.fn.zTree.init($("#dicTree"),
                            {
                                edit: {
                                    enable: true,
                                    showRemoveBtn: true,
                                    showRenameBtn: true,
                                    removeTitle: "删除",
                                    renameTitle: "编辑"
                                },
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
                                    onClick: onClickNode,
                                    onRename: editNode,
                                    beforeRemove: deleteConfirm
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

        //点击字典类型筛选列表
        function onClickNode(event, treeId, treeNode, clickFlag) {
            $("#txtDicTypeCode").val(treeNode.DicTypeCode);
            reloadData();
        }

        //编辑字典类型
        function editNode(event, treeId, treeNode, isCancel) {
            if (!isCancel) {
                toastr.info("更新成功");
            }

        };

        //删除节点确认
        function deleteConfirm(treeId, treeNode) {
            if (treeNode.ID !== 0) {
                bootAlert.confirm().on(function (confirmResult) {
                    if (confirmResult) {
                        deleteNode(treeNode,false);
                    }
                });
            }
            return false;
        }

        //删除字典类型
        function deleteNode(treeNode) {
            var param = {};
            param["method"] = "DeleteByIds";
            param["Id"] = treeNode.ID;
            $.ajax({
                type: "POST",
                url: "/API/DictionaryApi.aspx",
                cache: false, //禁用缓存
                data: param, //传入组装的参数
                dataType: "json",
                success: function () {
                    treeObj.removeNode(treeNode);
                    toastr.success("删除成功！");
                    reloadData();
                }
            });
        };

        //重新加载数据
        function reloadData() {
            tableObj.ajax.reload(null, false);
        };

        //删除行数据
        function deleteRows(data) {
            bootAlert.confirm({
                message: "确认要删除选中数据吗？"
            }).on(function (result) {
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
            });
        }

        //添加
        $("#btnAdd").click(function () {
            bootAlert.dialog({
                "title": "添加字典",
                "height": 355,
                "url": "/SysManage/DictionaryInfo.aspx"
            });
        });

        //更新
        function update(id) {
            bootAlert.dialog({
                "title": "编辑字典",
                "height": 355,
                "url": "/SysManage/DictionaryInfo.aspx?ID=" + id
            });
        }
    </script>
</asp:Content>
