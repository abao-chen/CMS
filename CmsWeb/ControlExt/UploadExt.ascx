<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UploadExt.ascx.cs" Inherits="CmsWeb.ControlExt.UploadExt" %>
<script src="/Scripts/bootstrap/vendor/plupload-2.3.1/plupload.full.min.js"></script>
<style type="text/css">
    .selectFile {
        float: left;
        background: url("/Css/Image/upload-img.png") no-repeat center;
        width: 110px;
        height: 110px;
        cursor: pointer;
    }

        .selectFile .showText {
            position: absolute;
            margin: 0;
            padding: 0;
            top: 90px;
            height: 10px;
            width: 105px;
            text-align: center;
            font-size: 12px;
        }

    .showFile {
        float: left;
        background: url("/Css/Image/upload-img.png") no-repeat center;
        width: 110px;
        height: 110px;
        cursor: pointer;
        margin-right: 20px;
        background-size: 100% 100%;
    }

        .showFile .showText {
            position: absolute;
            margin: 0;
            padding: 0;
            top: 90px;
            height: 20px;
            width: 110px;
            text-align: center;
            font-size: 12px;
            background-color: #525151;
            color: #FFFFFF;
        }

        .showFile .showDel {
            position: relative;
            top: 0;
            left: 85px;
            font-size: 12px;
            background-color: #525151;
            color: #FFFFFF;
            width: 25px;
            cursor: pointer;
            z-index: 99;
        }
</style>
<div id="container_<%=this.ID %>">
    <div id="showList_<%=this.ID %>" style="position: relative">
    </div>
    <div id="uploader_<%=this.ID %>">
        <div id="pickfiles_<%=this.ID %>" class="selectFile">
            <div class="showText">选择文件</div>
        </div>
    </div>
    <asp:HiddenField ID="hidFilePath" runat="server" />
</div>
<script type="text/javascript">
    $(function () {
        <%=this.ID %>Obj.init();
    });

    var <%=this.ID %>Obj = {
        init: function () {
            new plupload.Uploader({
                runtimes: 'html5,flash,silverlight,html4',
                browse_button: 'pickfiles_<%=this.ID %>', // you can pass in id...
                //container: document.getElementById('container'), // ... or DOM Element itself
                url: "/Api/UploadApi.aspx",
                multipart_params: { "method": "UploadFile", "FolderPath": "<%=FolderPath%>" },
                file_data_name: "FileData",
                multi_selection: <%=this.Single=="1"?"false":"true"%>,
                filters: {
                    max_file_size: '10mb',
                    mime_types: [
                        { title: "files extensions", extensions: "<%=Extensions%>" }
                    ]
                },
                flash_swf_url: '/Scripts/bootstrap/vendor/plupload-2.3.1/Moxie.swf',
                silverlight_xap_url: '/Scripts/bootstrap/vendor/plupload-2.3.1/Moxie.xap',
                init: {
                    FilesAdded: function (up, files) {
                        plupload.each(files, function (file) {
                            $("#showList_<%=this.ID %>", "#container_<%=this.ID %>").append("<div id=\"file_" + file.id + "\" class=\"showFile\"><div class=\"showText\">上传中... </div></div>");
                        });
                        up.start();
                    },
                    UploadProgress: function (up, file) {
                        $("#file_" + file.id, "#container_<%=this.ID %>").html("<div class=\"showText\">上传中... " + file.percent + "% </div>");
                        //document.getElementById(file.id).getElementsByTagName('b')[0].innerHTML = '<span>' + file.percent + "%</span>";
                    },
                    FileUploaded: function (up, file, result) {
                        //"/Css/Image/file_ico.png"
                        var options = up.getOption();
                        var imgSrc;
                        if ("<%=this.UploadType%>" == "1") {
                            imgSrc = result.response.replace(new RegExp("\"", 'gm'), "").split("|")[1];
                        } else {
                            imgSrc = "/Css/Image/file_ico.png";
                        }
                        
                        if (options.multi_selection) {//多文件
                            $("#file_" + file.id, "#container_<%=this.ID %>").css("background-image", "url(" + imgSrc + ")");
                            $("#file_" + file.id, "#container_<%=this.ID %>").data("url", imgSrc);
                            $("#file_" + file.id, "#container_<%=this.ID %>").html("<div class=\"showText\">上传完成</div><div class=\"showDel\">删除</div>");
                            var orgValue = $("#<%= hidFilePath.ClientID%>").val();
                            var curValue = orgValue + imgSrc + ";";
                            $("#<%= hidFilePath.ClientID%>").val(curValue);
                        } else {//单文件
                            $("#file_" + file.id, "#container_<%=this.ID %>").prev().remove();
                            $("#file_" + file.id, "#container_<%=this.ID %>").css("background-image", "url(" + imgSrc + ")");
                            $("#file_" + file.id, "#container_<%=this.ID %>").data("url", imgSrc);
                            $("#file_" + file.id, "#container_<%=this.ID %>").html("<div class=\"showText\">上传完成</div><div class=\"showDel\">删除</div>");
                            $("#<%= hidFilePath.ClientID%>").val(imgSrc);
                        }
                    },
                    Error: function (up, err) {
                        console.log(err);
                    }
                }
            }).init();

            $(document).on("click", ".showFile .showDel", function () {
                var orgValue = $("#<%= hidFilePath.ClientID%>").val();
                var imgSrc = $(this).parent().data("url");
                var curValue = orgValue.replace(imgSrc + ";", "");
                $("#<%= hidFilePath.ClientID%>").val(curValue);
                $(this).parent().remove();
            });

            this.initValue();
        },
        initValue: function () {
            //初始化已有图片/文件显示
            var initValue = $("#<%= hidFilePath.ClientID%>").val();
            if (initValue !== "") {
                var valueArray = initValue.replace(new RegExp("\"", 'gm'), "").split(";");
                $(valueArray).each(function (index, value) {
                    if (value !== "") {
                        $("#showList_<%=this.ID%>").append('<div id="' + uuid() + '" class="showFile" data-url="' + value + '" style="background-image: url(&quot;' + value + '&quot;);"><div class="showText">上传完成</div><div class="showDel">删除</div></div>');
                    }
                });
            }
        },
        isEmpty: function () {
            if ($("#<%= hidFilePath.ClientID%>").val() !== "") {
                return false;
            } else {
                return true;
            }
        }
    };
</script>
