<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="ContentList.aspx.cs" Inherits="CmsWeb.Content.ContentList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="page-wrapper">
        <div class="row">
            <div class="col-lg-12">
                <div id="searchPanel" class="panel panel-default">
                    <div class="panel-heading">
                        <a data-toggle="collapse" data-parent="#searchPanel" href="#searchBody">检索区域</a>
                    </div>
                    <div id="searchBody" class="panel-body collapse in">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="col-lg-4 form-group">
                                    <label>内容标题:</label>
                                    <asp:TextBox runat="server" ID="txtTitle" searchattr="ContentTitle|LIKE|ContentTitle" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-lg-4 form-group">
                                    <label>内容子标题:</label>
                                    <asp:TextBox runat="server" ID="txtSubTitle" searchattr="ContentSubTitle|LIKE|ContentSubTitle" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-lg-4 form-group">
                                    <label>创建时间:</label>
                                    <asp:TextBox runat="server" ID="txtCreateTime" searchattr="CreateTime|<=|CreateTime" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-lg-4 searchPanel">
                                    <input type="button" id="btnClear" class="btn btn-default" value="Clear" />
                                    <input type="button" id="btnSearch" class="btn btn-default" value="Search" />
                                </div>
                            </div>
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
                        <table width="100%" class="table table-striped table-bordered table-hover table-condensed" id="dataTables-example">
                            <thead>
                                <tr>
                                    <th>
                                        <input id="cbSelectAll" type="checkbox" title="全选/取消" /></th>
                                    <th>内容标题</th>
                                    <th>内容子标题</th>
                                    <th>创建时间</th>
                                    <th>内容标题2</th>
                                    <th>内容子标题2</th>
                                    <th>创建时间2</th>
                                    <th>内容标题3</th>
                                    <th>内容子标题3</th>
                                    <th>创建时间3</th>
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
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script type="text/javascript">
        var tableObj;
        //初始化表格
        $(function () {
            var objData = { "method": "getlist", "id": "1", "where": "2" };
            tableObj = $('#dataTables-example').DataTable({
                "processing": true,
                "serverSide": true,
                "searching": false,
                "ordering": true,
                "orderMulti": false,
                "select": true,
                "scrollX": true,
                //"fixedHeader": true,
                "bLengthChange": false,   //去掉每页显示多少条数据方法
                "aLengthMenu": [50, 100, 200],
                "scrollY": "600px",
                "renderer": "bootstrap",
                "pagingType": "full_numbers",
                "rowId": "ID",
                "order": [1, "desc"],
                "ajax": function (data, callback, settings) {
                    //封装请求参数
                    var param = {};
                    param.limit = data.length;//页面显示记录条数，在页面显示每页显示多少项的时候
                    param.start = data.start;//开始的记录序号
                    param.page = (data.start / data.length) + 1;//当前页码
                    param.orderColunm = data.columns[parseInt(data.order[0].column)].data;//排序列名
                    param.orderDir = data.order[0].dir;//排序方式DESC、ASC
                    var $searchForm = $("#searchPanel input[type='text']");
                    $searchForm.each(function (index, inputObj) {
                        if ($(inputObj).attr("SearchAttr") && $(inputObj).val() != "") {
                            param[$(inputObj).attr("SearchAttr")] = $(inputObj).val();
                        }
                    });
                    param["method"] = "GetContentPageList";
                    //ajax请求数据
                    $.ajax({
                        type: "POST",
                        url: "/API/ContentApi.aspx",
                        cache: false,  //禁用缓存
                        data: param,  //传入组装的参数
                        dataType: "json",
                        success: function (result) {
                            var returnData = {};
                            returnData.draw = data.draw;//这里直接自行返回了draw计数器,应该由后台返回
                            returnData.recordsTotal = result.total;//返回数据全部记录
                            returnData.recordsFiltered = result.total;//后台不实现过滤功能，每次查询均视作全部结果
                            returnData.data = result.data;//返回的数据列表
                            //调用DataTables提供的callback方法，代表数据已封装完成并传回DataTables进行渲染
                            callback(returnData);
                        }
                    });
                },
                "columns": [
                    {
                        "data": "ID",
                        "orderable": false,
                        "render": function (data, type, row, meta) {
                            var result = "<input id=\"cb-" + data + "\" name=\"tbCheckbox\" type=\"checkbox\" title=\"全选/取消\" />";
                            return result;
                        }
                    },
                    { "data": "ContentTitle" },
                    { "data": "ContentSubTitle" },
                    {
                        "data": "CreateTime",
                        "render": function (data, type, row, meta) {
                            return (new Date(data)).Format("yyyy/MM/dd hh:mm:ss");
                        }
                    },
                    { "data": "ContentTitle" },
                    { "data": "ContentSubTitle" },
                    {
                        "data": "CreateTime",
                        "render": function (data, type, row, meta) {
                            return (new Date(data)).Format("yyyy/MM/dd hh:mm:ss");
                        }
                    },
                    { "data": "ContentTitle" },
                    { "data": "ContentSubTitle" },
                    {
                        "data": "CreateTime",
                        "render": function (data, type, row, meta) {
                            return (new Date(data)).Format("yyyy/MM/dd hh:mm:ss");
                        }
                    }
                ]
            });
        });

        $(function () {
            //检索
            $("#btnSearch").click(function () {
                reloadData({ "where": "1", "keys": "2" });
            });

            //清除检索条件
            $("#btnClear").click(function () {
                clearSearchForm();
            });

            //初始化日期控件
            initDateControl("<%=txtCreateTime.ClientID%>");
        });

        //重新加载数据
        function reloadData() {
            tableObj.ajax.reload(function () {
            }, false);
        };
    </script>
</asp:Content>
