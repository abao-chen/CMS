<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="ContentList.aspx.cs" Inherits="TestApplication.Content.ContentList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="page-wrapper">
        <!-- /.row -->
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        DataTables Advanced Tables
                    </div>
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
        $(function () {
            console.log("ready");
        });

        //初始化表格
        $(document).ready(function () {
            var objData = {"method":"getlist", "id": "1", "where": "2" };
            $('#dataTables-example').DataTable({
                "processing": true,
                "serverSide": true,
                "ordering": true,
                "orderMulti": false,
                "order":[1,"desc"],
                "ajax": { "url": "/API/Content.aspx", "type": "post", "data": objData },
                "columns": [
                    { "data": "ContentTitle" },
                    { "data": "ContentSubTitle" },
                    { "data": "CreateTime" }
                ]
            });
        });
    </script>
</asp:Content>
