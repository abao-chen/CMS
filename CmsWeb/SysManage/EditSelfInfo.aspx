<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="EditSelfInfo.aspx.cs" Inherits="Cms.Web.Admin.EditSelfInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    用户信息
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>用户账号：</label>
                                <asp:TextBox ID="txtUserAccount" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>用户名称：</label>
                                <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>用户状态：</label>
                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" Enabled="false"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>用户类型：</label>
                                <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control" Enabled="false"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>最后一次登录时间：</label>
                                <asp:Label ID="lbLastLoginTime" runat="server" Text="" CssClass="form-control"></asp:Label>
                            </div>
                        </div>
                        <div class="col-lg-12">
                            <div class="form-group">
                                <label>用户角色：</label>
                                <Cms:CheckBoxListExt runat="server" ID="cblRole" IsInline="True" Name="cblRole" Enabled="false"></Cms:CheckBoxListExt>
                            </div>
                        </div>
                        <div class="col-lg-12" style="text-align: center;">
                            <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="btn btn-default" Style="margin: 0 auto;" OnClick="btnSave_OnClick" />
                            <a class="btn btn-default" href="/Index.aspx" style="margin: 0 auto;">取消</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div id="updatePasswordPanel" class="panel panel-default hide">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-12 form-group">
                            <label>新密码：</label>
                            <input type="password" name="newPassword" value="" class="form-control" />
                        </div>
                        <div class="col-lg-12 form-group">
                            <label>确认密码：</label>
                            <input type="password" name="confirmPassword" value="" class="form-control" />
                        </div>
                    </div>
                </div>
                <div class="panel-footer navbar-fixed-bottom">
                    <div class="row" style="text-align: center;">
                        <input type="button" onclick="updatePassword();" class="btn btn-default" style="margin: 0 auto;" value="保存" />
                        <input type="button" onclick="closeModal();" class="btn btn-default" style="margin: 0 auto;" value="关闭" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
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
                    "<%= txtName.UniqueID %>": { validators: { notEmpty: {} } },
                    "<%= ddlStatus.UniqueID %>": { validators: { notEmpty: {} } },
                    "<%= ddlType.UniqueID %>": { validators: { notEmpty: {} } },
                    "cblRole": { validators: { notEmpty: {} } }
                }
            });
        });

        /**
         * 更新密码
         */
        function updatePassword() {
            var newPwd = $("input[name='newPassword']").val();
            var confirmPwd = $("input[name='confirmPassword']").val();
            if (newPwd == "") {
                toastr.error("密码不能为空，请输入密码！");
                return;
            }
            if (confirmPwd == "") {
                toastr.error("确认密码不能为空，请输入确认密码！");
                return;
            }
            if (confirmPwd != newPwd) {
                toastr.error("两次密码输入不一致，请重新输入！");
                return;
            }
            var param = {};
            param.id = getUrlParams("Id");
            param.newPwd = newPwd;
            param.confirmPwd = confirmPwd;
            param.method = "UpdatePassword";
            $.ajax({
                type: "POST",
                url: "/API/SysManage/UserApi.aspx",
                cache: false, //禁用缓存
                data: param, //传入组装的参数
                dataType: "json",
                success: function (result) {
                    ajaxSuccessDone(result, function () {
                        toastr.success("密码修改成功。");
                        closeModal();
                    });
                }
            });
        }
    </script>
</asp:Content>
