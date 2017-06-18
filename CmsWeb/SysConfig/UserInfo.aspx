<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="UserInfo.aspx.cs" Inherits="CmsWeb.UserInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="page-wrapper">
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
                                    <asp:TextBox ID="txtAccount" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-6">
                                <div class="form-group">
                                    <label>用户密码：</label>
                                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
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
                                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-lg-6">
                                <div class="form-group">
                                    <label>用户类型：</label>
                                    <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-lg-6">
                                <div class="form-group">
                                    <label>最后一次登录时间：</label>
                                    <asp:Label ID="lbLastLoginTime" runat="server" Text="" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-lg-12" style="text-align: center;">
                                <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="btn btn-default" Style="margin: 0 auto;" OnClick="btnSave_OnClick" />
                                <a class="btn btn-default" href="~/SysConfig/UserList.aspx" style="margin: 0 auto;">取消</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script type="text/javascript">
        $(function () {
            $('#form').bootstrapValidator({
                message: 'This value is not valid',
                feedbackIcons: {
                    valid: 'glyphicon glyphicon-ok',
                    invalid: 'glyphicon glyphicon-remove',
                    validating: 'glyphicon glyphicon-refresh'
                },
                fields: {
                    <%=txtName.UniqueID%>: {
                        validators: {
                            notEmpty: {}
                        }
                    },
                    <%=txtAccount.UniqueID%>: {
                        validators: {
                            notEmpty: {}
                        }
                    },
                    <%=ddlStatus.UniqueID%>: {
                        validators: {
                            notEmpty: {}
                        }
                    },
                    <%=ddlType.UniqueID%>: {
                        validators: {
                            notEmpty: {}
                        }
                    },
                    <%=txtPassword.UniqueID%>: {
                        validators: {
                            enabled:false,
                            notEmpty: {}
                        }
                    }
                }
            }).on('success.form.bv', function (e) {
                console.log("表单验证通过");
                // Prevent form submission
                //e.preventDefault();

                // Get the form instance
                //var $form = $(e.target);

                // Get the BootstrapValidator instance
                //var bv = $form.data('bootstrapValidator');
            }).on('error.form.bv', function (e) {
                console.log("表单验证不通过");
                //阻止表单提交
                //e.preventDefault();
            }).on('error.field.bv', function(e, data) {
                console.log('error.field.bv -->', data);
            }).on('success.field.bv', function(e, data) {
                console.log('success.field.bv -->', data);
            });
        });
    </script>
</asp:Content>
