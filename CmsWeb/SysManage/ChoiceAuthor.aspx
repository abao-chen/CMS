<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Content.Master" AutoEventWireup="true" CodeBehind="ChoiceAuthor.aspx.cs" Inherits="CmsWeb.SysManage.ChoiceAuthor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Scripts/bootstrap/vendor/zTree/css/metroStyle/metroStyle.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="col-lg-12">
                        <ul id="dicTree" class="ztree"></ul>
                    </div>
                    <div class="col-lg-12" style="text-align: center;">
                        <a id="btnSave" class="btn btn-default" style="margin: 0 auto;">确定</a>
                        <a id="btnCancel" class="btn btn-default" style="margin: 0 auto;">取消</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script src="/Scripts/bootstrap/vendor/zTree/js/jquery.ztree.all.js"></script>
    <script type="text/javascript">
        var treeObj;
        //初始化树控件
        $(function () {
            var param = {};
            param["method"] = "GetTreeList";
            $.ajax({
                type: "POST",
                url: "/API/SysManage/RoleApi.aspx",
                cache: false, //禁用缓存
                data: param, //传入组装的参数
                dataType: "json",
                success: function (result) {
                    if (result.result == 1) { //请求成功
                        treeObj = $.fn.zTree.init($("#dicTree"),
                            {
                                data: {
                                    simpleData: {
                                        enable: true,
                                        idKey: "ID",
                                        pIdKey: "ParentID"
                                    },
                                    key: {
                                        name: "AuthorName"
                                    }
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

        //关闭dialog
        $("#btnSave").click(function () {
            var selectNodes = treeObj.getSelectedNodes();
            if (selectNodes.length > 0) {
                parent.setParentValue(selectNodes[0].ID,selectNodes[0].AuthorName);
                parent.closeModal();
            } else {
                toastr.info("请选择父级权限节点！");
            }
        });

        //关闭dialog
        $("#btnCancel").click(function () {
            parent.closeModal();
        });
    </script>
</asp:Content>
