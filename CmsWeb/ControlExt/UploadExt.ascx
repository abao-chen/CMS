<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UploadExt.ascx.cs" Inherits="CmsWeb.ControlExt.UploadExt" %>
<div id="uploadfile_<%=this.ID %>"></div>
<div id="showFile_<%=this.ID %>" style="float: left;">
</div>
<asp:HiddenField ID="hidFilePath" runat="server" />

<!-- jQuery -->
<script src="/Scripts/bootstrap/vendor/jquery/jquery.min.js"></script>
<!-- uploadify JavaScript -->
<script src="/Scripts/bootstrap/vendor/uploadify/jquery.uploadify.min.js"></script>
<script type="text/javascript">
    $(function () {
        var orgValue = $("#<%=hidFilePath.ClientID%>").val();
        if (orgValue != "") {
            var imgArray = orgValue.split(";");
            for (var i = 0; i < imgArray.length; i++) {
                if (imgArray[i] != "") {
                    var dataArray = imgArray[i].split("|");
                    $("#showFile_<%=this.ID %>").append("<div class=\"showFileBox\">" +
                        "<div style=\"float: left;\">" + dataArray[0] + "</div>" +
                        "<div style=\"float: left; padding-left: 10px;\"><a href=\"" + dataArray[1] + "\" target=\"_blank\">下载</a></div>" +
                        "<div style=\"float: left; padding-left: 10px;\"><a style=\"cursor: pointer;\" class=\"delFile\" data-filepath=" + imgArray[i] + ">删除</a></div>" +
                        "</div>");
                }
            }
        }

        //删除文件
        $(document).on("click", "a.delFile", function () {
            $(this).parents("div.showFileBox").remove();
            var orgValue = $("#<%=hidFilePath.ClientID%>").val();
            $("#<%=hidFilePath.ClientID%>").val(orgValue.replace($(this).data("filepath"), ''));
        });
    });

    //上传控件
    $("#uploadfile_<%=this.ID %>").uploadify({
        'uploader': '/Api/UploadApi.aspx',
        'swf': '/Scripts/bootstrap/vendor/uploadify/uploadify.swf',
        'formData': { "method": "UploadFile", "FolderPath": "/Upload/" },
        'buttonText': '选择文件',
        'auto': true,
        'multi': false,
        onUploadSuccess: function (file, data, response) {
            if (data) {
                data = data.replaceAll("\"", "");
                var dataArray = data.split("|");
                var orgValue = $("#<%=hidFilePath.ClientID%>").val();
                if (orgValue == "" || "1" == "<%= Single%>") {
                    $("#<%=hidFilePath.ClientID%>").val(data + ";");
                } else {
                    $("#<%=hidFilePath.ClientID%>").val(orgValue + ";" + data);
                }
                $("#showFile_<%=this.ID %>").append("<div class=\"showFileBox\">" +
                                                        "<div style=\"float: left;\">" + dataArray[0] + "</div>" +
                                                        "<div style=\"float: left; padding-left: 10px;\"><a href=\"" + dataArray[1] + "\" target=\"_blank\">下载</a></div>" +
                                                        "<div style=\"float: left; padding-left: 10px;\"><a style=\"cursor: pointer;\" class=\"delFile\" data-filepath=" + data + ">删除</a></div>" +
                                                    "</div>");
            }
        }, onUploadError: function (file, errorCode, errorMsg) {
            bootAlert.error("文件上传失败，请重新上传！");
        }
    });

</script>
