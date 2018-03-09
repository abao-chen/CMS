<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Content.Master" AutoEventWireup="true" CodeBehind="SysParamsInfo.aspx.cs" Inherits="CmsWeb.SysManage.SysParamsInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="col-lg-6">
                        <div class="form-group">
                            <label>参数名称：</label>
                            <asp:TextBox ID="txtParamName" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-lg-6">
                        <div class="form-group">
                            <label>参数值：</label>
                            <asp:TextBox ID="txtParamValue" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-lg-12">
                        <div class="form-group">
                            <label>参数描述：</label>
                            <asp:TextBox ID="txtParamDesc" CssClass="form-control" TextMode="MultiLine" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-lg-12" style="text-align: center;">
                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-default" Style="margin: 0 auto;" Text="保存" OnClick="btnSave_OnClick" />
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
                    <%=txtParamName.UniqueID%>: {
                        validators: {
                            notEmpty: {},
                            remote: {
                                type: 'POST',
                                url: '/API/SysParamsApi.aspx',
                                data: { "method": "ValidateParamsName", "ID": getUrlParams("ID") },
                                message: '参数名称已存在',
                                delay: 1000
                            }
                        }
                    },
                    <%=txtParamValue.UniqueID%>: {
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
        //$(function () {
        //    $("#btnSave").click(function (e) {
        //        $('#form1').data('bootstrapValidator').validate();
        //        if (!$('#form1').data('bootstrapValidator').isValid()) {
        //            return;
        //        }
        //        var postData = getFormPostData("form1");
        //        postData["method"] = "Save";
        //        $.ajax({
        //            type: "POST",
        //            url: "/API/SysParamsApi.aspx",
        //            cache: false, //禁用缓存
        //            data: postData, //传入组装的参数
        //            dataType: "json",
        //            success: function (result) {
        //                if (result.result == 1) { //请求成功
        //                    toastr.success("保存成功！");
        //                    parent.closeModal();
        //                    parent.reloadData();
        //                } else if (result.result == 2) { //请求失败
        //                    toastr.error(result.message);
        //                } else if (result.result == 3) { //登录超时
        //                    bootAlert.alert(result.message).on(function () {
        //                        location.href = "/Login.aspx";
        //                    });
        //                } else { //其他异常情况
        //                    toastr.error(result.message);
        //                }
        //            }
        //        });
        //    });
    </script>
</asp:Content>
