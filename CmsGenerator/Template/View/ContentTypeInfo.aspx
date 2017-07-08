<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="ContentTypeInfo.aspx.cs" Inherits="CmsWeb.ContentTypeInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    内容类型信息
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-6">
             <div class="form-group">
    <label>类型名称：</label>
<asp:TextBox ID ="txtTypeName" runat="server" CssClass="form-control"></asp:TextBox>
</div>
</div> 
<div class="col-lg-6">
             <div class="form-group">
    <label>类型别名：</label>
<asp:TextBox ID ="txtTypeAlias" runat="server" CssClass="form-control"></asp:TextBox>
</div>
</div> 
<div class="col-lg-6">
             <div class="form-group">
    <label>是否启用            1:启用,0不启用：</label>
<asp:TextBox ID ="txtIsUse" runat="server" CssClass="form-control"></asp:TextBox>
</div>
</div> 

                        <div class="col-lg-12" style="text-align: center;">
                            <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="btn btn-default" Style="margin: 0 auto;" OnClick="btnSave_OnClick" />
                            <a class="btn btn-default" href="/ContentManage/ContentTypeList.aspx" style="margin: 0 auto;">取消</a>
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
                    
                }
            });
        });
    </script>
</asp:Content>

