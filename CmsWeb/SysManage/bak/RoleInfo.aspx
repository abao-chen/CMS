<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="RoleInfo.aspx.cs" Inherits="CmsWeb.SysManage.RoleInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Scripts/bootstrap/vendor/zTree/css/metroStyle/metroStyle.css" rel="stylesheet" />
    <style type="text/css">
        .ztree {margin:0; padding:5px; color: #333;width:300px; word-break:break-all; word-wrap:break-word;}
        .ztree li{padding:0; margin:0; list-style:none; line-height:17px; text-align:left; white-space:nowrap; outline: 0;display:inline; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    角色编辑
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="form-group">
                                        <label>角色名称：</label>
                                        <asp:TextBox ID="txtRoleName" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-12">
                                    <div class="form-group">
                                        <label>是否启用：</label>
                                        <asp:CheckBox ID="cbxIsUsing" runat="server" CssClass="form-control" />
                                    </div>
                                </div>
                                <div class="col-lg-12">
                                    <ul id="dicTree" class="ztree"></ul>
                                    <asp:HiddenField ID="hidAuthorityIds" runat="server" />
                                </div>
                                <div class="col-lg-12" style="text-align: center;">
                                    <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="btn btn-default" Style="margin: 0 auto;" OnClick="btnSave_Click" OnClientClick="return setAuthorityIds();" />
                                    <a class="btn btn-default" href="/SysManage/RoleList.aspx" style="margin: 0 auto;">取消</a>
                                </div>
                            </div>
                        </div>
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
                url: "/API/RoleApi.aspx",
                cache: false, //禁用缓存
                data: param, //传入组装的参数
                dataType: "json",
                success: function (result) {
                    if (result.result == 1) { //请求成功
                        treeObj = $.fn.zTree.init($("#dicTree"),
                            {
                                check: {
                                    enable: true
                                },
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
                        checkTreeNodes();
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
                    <%=txtRoleName.UniqueID%>: {
                        validators: {
                            notEmpty: {}
                        }
                    }
                }
            });
        });

        //选中树节点
        function checkTreeNodes(){
            var checkIds = $("#<%=hidAuthorityIds.ClientID%>").val();
            var checkIdArray = checkIds.split(",");
            for(i=0;i<checkIdArray.length;i++){
                var node =  treeObj.getNodesByParam("ID", checkIdArray[i], null);
                if (node.length > 0) {
                    treeObj.checkNode(node[0],true,false);
                }
            }
        };

        //设置选中权限Id
        function setAuthorityIds() {
            var checkNodes = treeObj.getCheckedNodes(true);
            if (checkNodes.length > 0) {
                var checkIds = "";
                for (var i = 0; i < checkNodes.length; i++) {
                    if (checkIds == "") {
                        checkIds = checkNodes[i].ID+ "";
                    } else {
                        checkIds += "," + checkNodes[i].ID;
                    }
                }
                $("#<%=hidAuthorityIds.ClientID%>").val(checkIds);
                return true;
            } else {
                bootAlert.alert("请选择角色对应的权限");
                return false;
            }
        }
    </script>
</asp:Content>
