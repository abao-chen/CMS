<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditorExt.ascx.cs" Inherits="CmsWeb.ControlExt.EditorExt" %>


<div id="editor_<%=this.ID %>"></div>
<asp:HiddenField ID="EditorValue" runat="server" />

<!-- jQuery -->
<script src="/Scripts/bootstrap/vendor/jquery/jquery.min.js"></script>
<!-- Bootstrap Core JavaScript -->
<script src="/Scripts/bootstrap/vendor/bootstrap/js/bootstrap.min.js"></script>
<!-- summernote JavaScript -->
<script src="/Scripts/bootstrap/vendor/summernote/summernote.min.js"></script>
<script src="/Scripts/bootstrap/vendor/summernote/lang/summernote-zh-CN.min.js"></script>
<script type="text/javascript">
    $("#editor_<%=this.ID %>").summernote({
        lang: 'zh-CN',
        height: 300,
        minHeight: null,
        maxHeight: null,
        callbacks: {
            onChange: function (contents, $editable) {
                $("#<%=EditorValue.ClientID%>").val(contents);
            }
        }
    });


        function setEditorText() {
            var markupStr = $("#<%=EditorValue.ClientID%>").val();
        console.log(markupStr)
        $('#editor_<%=this.ID %>').summernote('code', markupStr);
        };
</script>
