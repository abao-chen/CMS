<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="AuthorityInfo.aspx.cs" Inherits="CmsWeb.AuthorityInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    权限信息
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>权限类型</label>
                                <asp:DropDownList ID="ddlAuthorType" runat="server" CssClass="form-control">
                                    <Items>
                                        <asp:ListItem Text="模块" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="页面" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="按钮" Value="3"></asp:ListItem>
                                    </Items>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>权限名称：</label>
                                <asp:TextBox ID="txtAuthorName" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>按钮ID：</label>
                                <asp:TextBox ID="txtAuthorFlag" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>父级权限：</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtParent" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    <span class="input-group-btn">
                                        <button id="choiceParent" class="btn btn-default" type="button">选择</button>
                                    </span>
                                    <asp:HiddenField ID="hidParentID" runat="server" />
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>页面URL：</label>
                                <asp:TextBox ID="txtPageUrl" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>是否为菜单：</label>
                                <asp:CheckBox ID="cbxIsMenu" runat="server" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="col-lg-12" style="text-align: center;">
                            <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="btn btn-default" Style="margin: 0 auto;" OnClick="btnSave_OnClick" />
                            <a class="btn btn-default" href="/SysManage/AuthorityList.aspx" style="margin: 0 auto;">取消</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="ChoiceAuthor" class="row hide">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="col-lg-12">
                        <ul id="dicTree" class="ztree"></ul>
                    </div>

                </div>
                <div class="panel-footer navbar-fixed-bottom">
                    <a class="btn btn-default" onclick="selectParent();" style="margin: 0 auto;">确定</a>
                    <a class="btn btn-default" style="margin: 0 auto;">取消</a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <link href="/Scripts/bootstrap/vendor/zTree/css/metroStyle/metroStyle.css" rel="stylesheet" />
    <script src="/Scripts/bootstrap/vendor/zTree/js/jquery.ztree.all.js"></script>
    <script type="text/javascript">
        //表单验证
        $(function () {
            $('#form').bootstrapValidator({
                message: 'This value is not valid',
                feedbackIcons: {
                    valid: 'glyphicon glyphicon-ok',
                    invalid: 'glyphicon glyphicon-remove',
                    validating: 'glyphicon glyphicon-refresh'
                },
                fields: {
                    <%=ddlAuthorType.UniqueID%>: {
                    validators: {
                        notEmpty: {}
                    }
                },
                    <%=txtAuthorName.UniqueID%>: {
                        validators: {
                            notEmpty: {}
                        }
                    }
                }
            });

            //弹出选择父级权限
            $("#choiceParent").click(function () {
                bootAlert.dialog({
                    "targetId":"ChoiceAuthor",
                    "title": "选择父级权限",
                    width: 700,
                    "height": 550
                });
            });
        });

        //设置父级权限
        function setParentValue(parentId, parentValue) {
            $("#<%= hidParentID.ClientID%>").val(parentId);
            $("#<%= txtParent.ClientID%>").val(parentValue);
        }
    </script>
    <script type="text/javascript">
        var treeObj;
        //初始化树控件
        $(function () {
            var param = {};
            param["method"] = "GetTreeList";
            $.ajax({
                type: "POST",
                url: "/API/RoleApi.aspx",
                cache: false, //禁用缓存
                data: param, //传入组装的参数
                dataType: "json",
                success: function (result) {
                    ajaxSuccessDone(result, function () {
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
                    });
                }
            });
        });

        //关闭dialog
        function selectParent () {
            var selectNodes = treeObj.getSelectedNodes();
            if (selectNodes.length > 0) {
                setParentValue(selectNodes[0].ID, selectNodes[0].AuthorName);
                closeModal();
            } else {
                toastr.info("请选择父级权限节点！");
            }
        }

        //关闭dialog
        $("#btnCancel").click(function () {
            closeModal();
        });
    </script>
</asp:Content>

