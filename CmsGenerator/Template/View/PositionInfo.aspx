﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="PositionInfo.aspx.cs" Inherits="CmsWeb.PositionInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    省市县镇村数据信息
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-6">
             <div class="form-group">
    <label>：</label>
<asp:TextBox ID ="txtProvinceId" runat="server" CssClass="form-control"></asp:TextBox>
</div>
</div> 
<div class="col-lg-6">
             <div class="form-group">
    <label>：</label>
<asp:TextBox ID ="txtProvinceName" runat="server" CssClass="form-control"></asp:TextBox>
</div>
</div> 
<div class="col-lg-6">
             <div class="form-group">
    <label>：</label>
<asp:TextBox ID ="txtCityID" runat="server" CssClass="form-control"></asp:TextBox>
</div>
</div> 
<div class="col-lg-6">
             <div class="form-group">
    <label>：</label>
<asp:TextBox ID ="txtCityName" runat="server" CssClass="form-control"></asp:TextBox>
</div>
</div> 
<div class="col-lg-6">
             <div class="form-group">
    <label>：</label>
<asp:TextBox ID ="txtCountyID" runat="server" CssClass="form-control"></asp:TextBox>
</div>
</div> 
<div class="col-lg-6">
             <div class="form-group">
    <label>：</label>
<asp:TextBox ID ="txtCountyName" runat="server" CssClass="form-control"></asp:TextBox>
</div>
</div> 
<div class="col-lg-6">
             <div class="form-group">
    <label>：</label>
<asp:TextBox ID ="txtTownID" runat="server" CssClass="form-control"></asp:TextBox>
</div>
</div> 
<div class="col-lg-6">
             <div class="form-group">
    <label>：</label>
<asp:TextBox ID ="txtTownName" runat="server" CssClass="form-control"></asp:TextBox>
</div>
</div> 
<div class="col-lg-6">
             <div class="form-group">
    <label>：</label>
<asp:TextBox ID ="txtVillageID" runat="server" CssClass="form-control"></asp:TextBox>
</div>
</div> 
<div class="col-lg-6">
             <div class="form-group">
    <label>：</label>
<asp:TextBox ID ="txtVillageName" runat="server" CssClass="form-control"></asp:TextBox>
</div>
</div> 

                        <div class="col-lg-12" style="text-align: center;">
                            <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="btn btn-default" Style="margin: 0 auto;" OnClick="btnSave_OnClick" />
                            <a class="btn btn-default" href="~/SysManage/PositionList.aspx" style="margin: 0 auto;">取消</a>
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
