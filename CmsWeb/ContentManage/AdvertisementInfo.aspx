﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="AdvertisementInfo.aspx.cs" Inherits="Cms.Web.Admin.AdvertisementInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    广告信息
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>广告类型ID：</label>
                                <asp:TextBox ID="txtAdTypeID" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>广告名称：</label>
                                <asp:TextBox ID="txtAdName" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-12">
                            <div class="form-group">
                                <label>广告图片：</label>
                                <Cms:UploadExt ID="uplAdDescription" runat="server"></Cms:UploadExt>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>广告链接：</label>
                                <asp:TextBox ID="txtAdUrl" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>有效开始时间：</label>
                                <Cms:DatePickerExt ID="txtValidStartTime" runat="server" Name="ValidStartTime" Format="yyyy/MM/dd"></Cms:DatePickerExt>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>有效结束时间：</label>
                                <Cms:DatePickerExt ID="txtValidEndTime" runat="server" Name="ValidEndTime" Format="yyyy/MM/dd"></Cms:DatePickerExt>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>备注：</label>
                                <asp:TextBox ID="txtAdTypeComment" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>是否启用：</label>
                                <asp:TextBox ID="txtIsUsing" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-lg-12" style="text-align: center;">
                            <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="btn btn-default" Style="margin: 0 auto;" OnClick="btnSave_OnClick" />
                            <a class="btn btn-default" href="/ContentManage/AdvertisementList.aspx" style="margin: 0 auto;">取消</a>
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
                                        <%=txtAdTypeID.UniqueID%>: {
                    validators: {
                        notEmpty: {},
                    }
                },                    <%=txtAdName.UniqueID%>: {
                    validators: {
                        notEmpty: {},
                    }
                },                    <%=uplAdDescription.UniqueID%>: {
                                            validators: {
                                                notEmpty: {},
                                            }
                                        },                    <%=txtValidStartTime.UniqueID%>: {
                                            validators: {
                                                date: { format: "YYYY/MM/DD" },
                                            }
                                        },                    <%=txtValidEndTime.UniqueID%>: {
                                            validators: {
                                                date: { format: "YYYY/MM/DD" },
                                            }
                                        },
                }
            });

            
        });
    </script>
</asp:Content>

