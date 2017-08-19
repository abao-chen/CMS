<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditorExt.ascx.cs" Inherits="CmsWeb.ControlExt.EditorExt" %>
<!-- 加载编辑器的容器 -->
<script id="container_<%=this.ID %>" type="text/plain"><%=this.Text %></script>
<asp:HiddenField ID="EditorValue" runat="server" />
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
