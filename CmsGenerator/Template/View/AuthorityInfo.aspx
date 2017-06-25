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
    <label>权限类型
            1：模块，2：页面，3：按钮：</label>
<asp:TextBox ID ="txtAuthorType" runat="server" CssClass="form-control"></asp:TextBox>
</div>
</div> 
<div class="col-lg-6">
             <div class="form-group">
    <label>：</label>
<asp:TextBox ID ="txtAuthorName" runat="server" CssClass="form-control"></asp:TextBox>
</div>
</div> 
<div class="col-lg-6">
             <div class="form-group">
    <label>权限标识，为权限按钮使用：</label>
<asp:TextBox ID ="txtAuthorFlag" runat="server" CssClass="form-control"></asp:TextBox>
</div>
</div> 
<div class="col-lg-6">
             <div class="form-group">
    <label>父级权限ID：</label>
<asp:TextBox ID ="txtParentID" runat="server" CssClass="form-control"></asp:TextBox>
</div>
</div> 
<div class="col-lg-6">
             <div class="form-group">
    <label>权限全路径：</label>
<asp:TextBox ID ="txtFullID" runat="server" CssClass="form-control"></asp:TextBox>
</div>
</div> 
<div class="col-lg-6">
             <div class="form-group">
    <label>页面URL：</label>
<asp:TextBox ID ="txtPageUrl" runat="server" CssClass="form-control"></asp:TextBox>
</div>
</div> 

                        <div class="col-lg-12" style="text-align: center;">
                            <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="btn btn-default" Style="margin: 0 auto;" OnClick="btnSave_OnClick" />
                            <a class="btn btn-default" href="~/SysConfig/AuthorityList.aspx" style="margin: 0 auto;">取消</a>
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
            });
        });
    </script>
</asp:Content>

