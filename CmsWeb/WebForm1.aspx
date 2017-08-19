<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="CmsWeb.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- 加载编辑器的容器 -->
    <script id="container_<%=this.ID %>" type="text/plain">
    </script>
    <asp:HiddenField ID="EditorValue" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <!-- 配置文件 -->
    <script src="/Scripts/bootstrap/vendor/ueditor/ueditor.config.js"></script>
    <!-- 编辑器源码文件 -->
    <script src="/Scripts/bootstrap/vendor/ueditor/ueditor.all.min.js"></script>
    <!-- 实例化编辑器 -->
    <script type="text/javascript">
        UE.getEditor('container_<%=this.ID%>').addListener('contentChange', function (editor) {
            $("#<%=EditorValue.ClientID%>").val(UE.getEditor('container_<%=this.ID%>').getContent());
        });
    </script>
</asp:Content>
