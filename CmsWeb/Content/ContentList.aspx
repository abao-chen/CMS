<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="ContentList.aspx.cs" Inherits="TestApplication.Content.ContentList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="page-wrapper">
        <div class="row">
            <div class="col-lg-12">
                <div class="col-lg-1">title1:</div>
                <div class="col-lg-3">
                    <asp:TextBox runat="server" ID="txtTitle1" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-lg-1">title2:</div>
                <div class="col-lg-3">
                    <asp:TextBox runat="server" ID="txtTitle2" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-lg-1">title3:</div>
                <div class="col-lg-3">
                    <asp:TextBox runat="server" ID="txtTitle3" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
        </div>
        <!-- /.row -->
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <!-- /.panel-heading -->
                    <div class="panel-body">
                        <table width="100%" class="table table-striped table-bordered table-hover" id="dataTables-example">
                            <thead>
                                <tr>
                                    <th>ContentTitle</th>
                                    <th>ContentSubTitle</th>
                                    <th>CreateTime(s)</th>
                                </tr>
                            </thead>
                            <tfoot>
                                <tr>
                                    <th>ContentTitle</th>
                                    <th>ContentSubTitle</th>
                                    <th>CreateTime(s)</th>
                                </tr>
                            </tfoot>
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
        //初始化表格
        $(document).ready(function () {
            var objData = { "method": "getlist", "id": "1", "where": "2" };
            $('#dataTables-example').DataTable({
                "processing": true,
                "serverSide": true,
                "searching": false,
                "ordering": true,
                "orderMulti": false,
                "select": true,
                "renderer": "bootstrap",
                "pagingType": "full_numbers",
                "rowId": "ID",
                "order": [1, "desc"],
                "ajax": function (data, callback, settings) {
                    console.log(data);
                    //封装请求参数
                    var param = {};
                    param.limit = data.length;//页面显示记录条数，在页面显示每页显示多少项的时候
                    param.start = data.start;//开始的记录序号
                    param.page = (data.start / data.length) + 1;//当前页码
                    console.log(parseInt(data.order[0].column));
                    param.orderColunm = data.columns[parseInt(data.order[0].column)].data;//排序列名
                    param.orderDir = data.order[0].dir;//排序方式DESC、ASC
                    //ajax请求数据
                    $.ajax({
                        type: "POST",
                        url: "/API/Content.aspx",
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
                    { "data": "ContentTitle" },
                    { "data": "ContentSubTitle" },
                    { "data": "CreateTime" }
                ]
            });
        });
    </script>
</asp:Content>
