﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" ValidateRequest="false" AutoEventWireup="true" CodeBehind="BasicContentInfo.aspx.cs" Inherits="CmsWeb.BasicContentInfo" %>

<%@ Register TagPrefix="Cms" Namespace="CmsWeb.ControlExt" Assembly="CmsWeb" %>
<%@ Register Src="~/ControlExt/UploadExt.ascx" TagPrefix="Cms" TagName="UploadExt" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    基础内容信息
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>内容类型：</label>
                                <asp:DropDownList ID="ddlContentType" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>来源：</label>
                                <asp:TextBox ID="txtSource" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>标题：</label>
                                <asp:TextBox ID="txtContentTitle" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>子标题：</label>
                                <asp:TextBox ID="txtContentSubTitle" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>封面图：</label>
                                <asp:TextBox ID="txtCoverPictureUrl" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>有效开始时间：</label>
                                <Cms:DataPicker ID="txtValidStartTime" runat="server" Name="ValidStartTime" Format="yyyy/MM/dd"></Cms:DataPicker>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>有效结束时间：</label>
                                <Cms:DataPicker ID="txtValidEndTime" runat="server" Name="ValidEndTime" Format="yyyy/MM/dd"></Cms:DataPicker>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>展示顺序：</label>
                                <asp:TextBox ID="txtOrderNO" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>浏览量：</label>
                                <asp:TextBox ID="txtPageViewQua" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>转发量：</label>
                                <asp:TextBox ID="txtForwardQua" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>点赞量：</label>
                                <asp:TextBox ID="txtPointQua" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>评论量：</label>
                                <asp:TextBox ID="txtCommentQua" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-12">
                            <div class="form-group">
                                <label>附件：</label>
                                <Cms:UploadExt runat="server" ID="UploadExtAttachmentUrl" ></Cms:UploadExt>
                            </div>
                        </div>
                        <div class="col-lg-12">
                            <div class="form-group">
                                <label>内容：</label>
                                <Cms:EditorExt runat="server" ID="edtContent"></Cms:EditorExt>
                            </div>
                        </div>

                        <div class="col-lg-12" style="text-align: center;">
                            <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="btn btn-default" Style="margin: 0 auto;" OnClick="btnSave_OnClick" />
                            <a class="btn btn-default" href="/ContentManage/BasicContentList.aspx" style="margin: 0 auto;">取消</a>
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
            //表单验证
            $('#form').bootstrapValidator({
                message: 'This value is not valid',
                feedbackIcons: {
                    valid: 'glyphicon glyphicon-ok',
                    invalid: 'glyphicon glyphicon-remove',
                    validating: 'glyphicon glyphicon-refresh'
                },
                fields: {
                    <%=ddlContentType.UniqueID%>: {
                        validators: {
                            notEmpty: {},
                        }
                    },
                    <%=txtContentTitle.UniqueID%>: {
                        validators: {
                            notEmpty: {},
                        }
                    },
                    ValidStartTime: {
                        validators: {
                            date: {
                                format:"YYYY/MM/DD"
                            }
                        }
                    },
                    ValidEndTime: {
                        validators: {
                            date: {
                                format:"YYYY/MM/DD"
                            }
                        }
                    },
                    <%=txtOrderNO.UniqueID%>: {
                        validators: {
                            digits: {},
                        }
                    },
                    <%=txtPageViewQua.UniqueID%>: {
                        validators: {
                            digits: {},
                        }
                    },
                    <%=txtForwardQua.UniqueID%>: {
                        validators: {
                            digits: {},
                        }
                    },
                    <%=txtPointQua.UniqueID%>: {
                        validators: {
                            digits: {},
                        }
                    },
                    <%=txtCommentQua.UniqueID%>: {
                        validators: {
                            digits: {},
                        }
                    }
                }
            });

           
        });
    </script>
</asp:Content>

