<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Content.Master" AutoEventWireup="true" CodeBehind="DictionaryInfo.aspx.cs" Inherits="CmsWeb.SysManage.DictionaryInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="col-lg-6">
                        <div class="form-group">
                            <label>字典类型：</label>
                            <asp:dropdownlist runat="server" id="ddlDicTypeCode" cssclass="form-control"></asp:dropdownlist>
                        </div>
                    </div>
                    <div class="col-lg-6">
                        <div class="form-group">
                            <label>字典编码：</label>
                            <asp:textbox id="txtDicCode" cssclass="form-control" runat="server"></asp:textbox>
                        </div>
                    </div>
                    <div class="col-lg-12">
                        <div class="form-group">
                            <label>字典名称：</label>
                            <asp:textbox id="txtDicName" cssclass="form-control" runat="server"></asp:textbox>
                        </div>
                    </div>
                    <div class="col-lg-12">
                        <div class="form-group">
                            <label>是否启用：</label>
                            <asp:checkbox id="cbxIsUsing" runat="server"></asp:checkbox>
                        </div>
                    </div>
                    <div class="col-lg-12" style="text-align: center;">
                        <asp:button id="btnSave" runat="server" cssclass="btn btn-default" style="margin: 0 auto;" text="保存" onclick="btnSave_OnClick" />
                        <a id="btnCancel" class="btn btn-default" style="margin: 0 auto;">取消</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script type="text/javascript">
       
        $(function () {
            //表单验证
            $('#form1').bootstrapValidator({
                message: 'This value is not valid',
                feedbackIcons: {
                    valid: 'glyphicon glyphicon-ok',
                    invalid: 'glyphicon glyphicon-remove',
                    validating: 'glyphicon glyphicon-refresh'
                },
                fields: {
                    <%=txtDicCode.UniqueID%>: {
                    validators: {
                        notEmpty: {},
                        remote: {
                            type: 'POST',
                            url: '/API/DictionaryApi.aspx',
                            data: { "method": "ValidateDicCode", "ID": getUrlParams("ID"),"DicTypeCode":$("#<%=ddlDicTypeCode.ClientID%>").val() },
                            message: '参数名称已存在',
                            delay: 1000
                        }
                    }
                },
                <%=txtDicName.UniqueID%>: {
                    validators: {
                        notEmpty: {}
                    }
                }
            }
        });

        //关闭dialog
        $("#btnCancel").click(function () {
            parent.closeModal();
        });
    });
        
    //保存回调
    function saveCallback(result) {
        if (result == 1) {
            parent.reloadData();
            parent.closeModal();
        } else {
            toastr.error("保存失败！");
        }
    }
    </script>
</asp:Content>

