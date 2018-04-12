<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="DictionaryInfo.aspx.cs" Inherits="Cms.Web.Admin.DictionaryInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    字典信息
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>字典类型编码：</label>
                                <asp:DropDownList ID="ddlDicTypeCode" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>字典名称：</label>
                                <asp:TextBox ID="txtDicName" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>字典编码：</label>
                                <asp:TextBox ID="txtDicCode" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>是否启用：</label>
                                <asp:CheckBox ID="cbIsUsing" runat="server" CssClass="form-control"></asp:CheckBox>
                            </div>
                        </div>

                        <div class="col-lg-12" style="text-align: center;">
                            <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="btn btn-default" Style="margin: 0 auto;" OnClick="btnSave_OnClick" />
                            <a class="btn btn-default" href="/SysManage/DictionaryList.aspx" style="margin: 0 auto;">取消</a>
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
                        notEmpty: {}, notEmpty: {},
                    }
                },                    <%=txtDicName.UniqueID%>: {
                    validators: {
                        notEmpty: {},
                    }
                },                    <%=txtDicCode.UniqueID%>: {
                                            validators: {
                                                notEmpty: {},
                                            }
                                        },
                }
            });

            
        });
    </script>
</asp:Content>

