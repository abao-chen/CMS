<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="DicTypeInfo.aspx.cs" Inherits="Cms.Web.Admin.DicTypeInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    字典类型信息
                </div>
                <div class="panel-body">
                    <div class="row">
                                                <div class="col-lg-6">
                            <div class="form-group">
                                <label>字典类型编码：</label>
                                <asp:DropDownList ID ="ddlDicTypeCode" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div> 
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>字典类型名称：</label>
                                <asp:TextBox ID ="txtDicTypeName" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div> 
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>描述：</label>
                                <asp:TextBox ID ="txtDescription" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div> 
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>是否启用：</label>
                                <asp:CheckBox ID ="cbIsUsing" runat="server" CssClass="form-control"></asp:CheckBox>
                            </div>
                        </div> 

                        <div class="col-lg-12" style="text-align: center;">
                            <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="btn btn-default" Style="margin: 0 auto;" OnClick="btnSave_OnClick" />
                            <a class="btn btn-default" href="/SysManage/DicTypeList.aspx" style="margin: 0 auto;">取消</a>
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
                                        <%=ddlDicTypeCode.UniqueID%>: {
                                            validators: {
                                                notEmpty: {},notEmpty: {},
                                            }
                                        },                    <%=txtDicTypeName.UniqueID%>: {
                                            validators: {
                                                notEmpty: {},
                                            }
                                        },
                }
            });

            
        });
    </script>
</asp:Content>

