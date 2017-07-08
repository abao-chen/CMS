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
                                <label>父级权限ID：</label>
                                <asp:TextBox ID="txtParentID" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>页面URL：</label>
                                <asp:TextBox ID="txtPageUrl" runat="server" CssClass="form-control"></asp:TextBox>
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
        });
    </script>
</asp:Content>

