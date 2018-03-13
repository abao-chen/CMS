<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="BasicContentList.aspx.cs" Inherits="CmsWeb.BasicContentList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <div id="searchPanel" class="panel panel-default hide">
                <div id="searchBody" class="panel-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="col-lg-6 form-group">
                                <label>内容类型</label>
                                <asp:DropDownList runat="server" ID="ddlContentType" searchattr="ContentType|=|ContentType" CssClass="form-control" placeholder="内容类型"></asp:DropDownList>
                            </div>
                            <div class="col-lg-6 form-group">
                                <label>标题</label>
                                <asp:TextBox runat="server" ID="txtContentTitle" searchattr="ContentTitle|=|ContentTitle" CssClass="form-control" placeholder="标题"></asp:TextBox>
                            </div>
                            <div class="col-lg-6 form-group">
                                <label>子标题</label>
                                <asp:TextBox runat="server" ID="txtContentSubTitle" searchattr="ContentSubTitle|=|ContentSubTitle" CssClass="form-control" placeholder="子标题"></asp:TextBox>
                            </div>
                            <div class="col-lg-6 form-group">
                                <label>封面图</label>
                                <asp:TextBox runat="server" ID="txtCoverPictureUrl" searchattr="CoverPictureUrl|=|CoverPictureUrl" CssClass="form-control" placeholder="封面图"></asp:TextBox>
                            </div>
                            <div class="col-lg-6 form-group">
                                <label>有效开始时间</label>
                                <Cms:DataPickerExt runat="server" ID="txtValidStartTime" searchattr="ValidStartTime|=|ValidStartTime" CssClass="form-control" placeholder="有效开始时间"></Cms:DataPickerExt>
                            </div>
                            <div class="col-lg-6 form-group">
                                <label>有效结束时间</label>
                                <Cms:DataPickerExt runat="server" ID="txtValidEndTime" searchattr="ValidEndTime|=|ValidEndTime" CssClass="form-control" placeholder="有效结束时间"></Cms:DataPickerExt>
                            </div>
                            <div class="col-lg-6 form-group">
                                <label>内容</label>
                                <asp:TextBox runat="server" ID="txtContent" searchattr="Content|=|Content" CssClass="form-control" placeholder="内容"></asp:TextBox>
                            </div>

                        </div>
                    </div>
                </div>
                <div class="panel-footer navbar-fixed-bottom">
                    <div class="row" style="text-align: center;">
                        <input type="button" id="btnSearch" class="btn btn-default" style="margin: 0 auto;" value="查询" />
                        <input type="button" id="btnClear" class="btn btn-default" style="margin: 0 auto;" value="重置" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- /.row -->
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <!-- /.panel-heading -->
                <div class="panel-body">
                    <table width="100%" class="table table-striped table-bordered table-hover table-condensed" id="dataTables">
                        <thead>
                            <tr>
                                <th>
                                    <input id="cbSelectAll" type="checkbox" title="全选/取消" /></th>
                                <th>操作</th>
                                <th>内容类型</th>
                                <th>来源</th>
                                <th>标题</th>
                                <th>有效开始时间</th>
                                <th>有效结束时间</th>
                                <th>展示顺序</th>
                                <th>更新时间</th>

                            </tr>
                        </thead>
                    </table>
                </div>
                <!-- /.panel-body -->
            </div>
            <!-- /.panel -->
        </div>
        <!-- /.col-lg-12 -->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script type="text/javascript">
        var curTable;
        //初始化表格
        $(function () {
            var options = {
                "url": "/API/ContentManage/BasicContentApi.aspx",
                "editUrl": "/ContentManage/BasicContentInfo.aspx",
                "aLengthMenu":<%= CmsUtils.Configs.GetValue("LengthMenu")%>,
                "searchColunms": "ContentTitle|ContentSubTitle|CoverPictureUrl|Content",
                "columns": [
                    {
                        "data": "ID",
                        "width": "20px",
                        "class": "center",
                        "orderable": false,
                        "render": function (data, type, row, meta) {
                            var result = "<input id=\"" + data + "\" name=\"tbCheckbox\" type=\"checkbox\" title=\"全选/取消\" />";
                            return result;
                        }
                    },
                    {
                        "data": "ID",
                        "width": "40px",
                        "class": "center",
                        "orderable": false,
                        "render": function (data, type, row, meta) {
                            var result = "<a href=\"javascript:curTable.edit(" + row.ID +
                                ")\"><span class='glyphicon glyphicon-edit' title='编辑'></span></a>&nbsp;&nbsp;&nbsp;<a href=\"javascript:curTable.delSingleRow(curTable.settings.delMethod,'" +
                                row.ID +
                                "');\"><span class='glyphicon glyphicon-trash' title='删除'></span></a>";
                            return result;
                        }
                    },
                    { "data": "ContentType" },
                    { "data": "Source" },
                    { "data": "ContentTitle" },
                    { "data": "ValidStartTime" },
                    { "data": "ValidEndTime" },
                    { "data": "OrderNO" },
                    { "data": "UpdateTime" },

                ]
            };
            curTable = tableUtils.initTable(options);
        });
    </script>
</asp:Content>


