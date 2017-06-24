<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Content.Master" AutoEventWireup="true" CodeBehind="SysParamsInfo.aspx.cs" Inherits="CmsWeb.SysManage.SysParamsInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>参数名称：</label>
                                <input type="hidden" id="hidID" name="ID" value="<%=entity.ID %>" />
                                <input type="text" id="txtParamName" name="ParamName" class="form-control" value="<%=entity.ParamName %>" />
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>参数值：</label>
                                <input type="text" id="txtParamValue" name="ParamValue" class="form-control" value="<%=entity.ParamValue %>" />
                            </div>
                        </div>
                        <div class="col-lg-12">
                            <div class="form-group">
                                <label>参数描述：</label>
                                <textarea id="txtParamDesc" name="ParamDesc" class="form-control" style="height: 100px;"><%=entity.ParamDesc %></textarea>
                            </div>
                        </div>
                        <div class="col-lg-12" style="text-align: center;">
                            <a id="btnSave" class="btn btn-default" style="margin: 0 auto;">保存</a>
                            <a id="btnCancel" class="btn btn-default" style="margin: 0 auto;">取消</a>
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
            $('#form1').bootstrapValidator({
                message: 'This value is not valid',
                feedbackIcons: {
                    valid: 'glyphicon glyphicon-ok',
                    invalid: 'glyphicon glyphicon-remove',
                    validating: 'glyphicon glyphicon-refresh'
                },
                fields: {
                    ParamName: {
                        validators: {
                            notEmpty: {},
                            remote: {
                                type: 'POST',
                                url: '/API/SysParamsApi.aspx',
                                data: { "method": "ValidateParamsName", "ID": $("#hidID").val() },
                                message: '参数名称已存在',
                                delay: 1000
                            }
                        }
                    },
                    ParamValue: {
                        validators: {
                            notEmpty: {}
                        }
                    }
                }
            });
        });

        $(function () {
            $("#btnSave").click(function (e) {
                $('#form1').data('bootstrapValidator').validate();
                if (!$('#form1').data('bootstrapValidator').isValid()) {
                    return;
                }
                var postData = getFormPostData("form1");
                postData["method"] = "Save";
                $.ajax({
                    type: "POST",
                    url: "/API/SysParamsApi.aspx",
                    cache: false, //禁用缓存
                    data: postData, //传入组装的参数
                    dataType: "json",
                    success: function (result) {
                        if (result.result == 1) { //请求成功
                            toastr.success("保存成功！");
                            closeModal();
                            reloadData();
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

            $("#btnCancel").click(function () {
                closeModal();
            });
        });
    </script>
</asp:Content>
