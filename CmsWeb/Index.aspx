<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Cms.Web.Admin.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h3 class="page-header">欢迎进入后台管理系统</h3>
        </div>
        <div class="col-lg-12">
            <div>系统时间：<span id="sysTime"><%= DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")%></span></div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
<script>
$(function(){
	var updateTime = function(){
		var orgValue = $("#sysTime").text();
		var orgDateValue = new Date(orgValue);
		var curDateValue = new Date(orgDateValue.getTime() + 1000)
		console.log(curDateValue.Format("yyyy-MM-dd hh:mm:ss"));
		$("#sysTime").text(curDateValue.Format("yyyy-MM-dd hh:mm:ss"));
	};
	setInterval(updateTime,1000);
});
</script>
</asp:Content>