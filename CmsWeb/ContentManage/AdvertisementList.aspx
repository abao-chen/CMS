<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="AdvertisementList.aspx.cs" Inherits="CmsWeb.AdvertisementList" %>

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
                                <label>广告类型ID</label>
                                <asp:TextBox runat="server" ID="txtAdTypeID" searchattr="AdTypeID|=|AdTypeID" CssClass="form-control" placeholder="广告类型ID"></asp:TextBox>
                            </div>
                            <div class="col-lg-6 form-group">
                                <label>广告名称</label>
                                <asp:TextBox runat="server" ID="txtAdName" searchattr="AdName|=|AdName" CssClass="form-control" placeholder="广告名称"></asp:TextBox>
                            </div>
                            <div class="col-lg-6 form-group">
                                <label>有效开始时间</label>
                                <Cms:DataPickerExt runat="server" ID="txtValidStartTime" searchattr="ValidStartTime|=|ValidStartTime" CssClass="form-control" placeholder="有效开始时间"></Cms:DataPickerExt>
                            </div>
                            <div class="col-lg-6 form-group">
                                <label>有效结束时间</label>
                                <Cms:DataPickerExt runat="server" ID="txtValidEndTime" searchattr="ValidEndTime|=|ValidEndTime" CssClass="form-control" placeholder="有效结束时间"></Cms:DataPickerExt>
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
                                <th>广告类型ID</th>
                                <th>广告名称</th>
                                <th>广告链接</th>
                                <th>有效开始时间</th>
                                <th>有效结束时间</th>
                                <th>是否启用</th>

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
                "url": "/API/AdvertisementApi.aspx",
                "editUrl": "/ContentManage/AdvertisementInfo.aspx",
                "aLengthMenu":<%= CmsUtils.Configs.GetValue("LengthMenu")%>,
                "searchColunms": "AdTypeName|AdName",
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
                    { "data": "AdTypeName" },
                    { "data": "AdName" },
                    { "data": "AdUrl" },
                    { "data": "ValidStartTime" },
                    { "data": "ValidEndTime" },
                    { "data": "IsUsing" },

                ]
            };
            curTable = tableUtils.initTable(options);
        });
    </script>
</asp:Content>


