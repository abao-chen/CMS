var tableUtils = {
    tableObj: null,
    settings: null,
    /**
     * datatables初始化
     */
    initTable: function (options) {
        var _this = this;
        var tId = "dataTables";
        var searchMethod = "GetPagerList";
        var delMethod = "DeleteByIds";
        var downloadMethod = "Download";
        var defaults =
            {
                "tId": "dataTables",
                "searchMethod": "GetPagerList",
                "delMethod": "DeleteByIds",
                "downloadMethod": "Download",
                "serverSide": true,
                "searching": true,
                "ordering": true,
                "orderMulti": false,
                "deferRender": true,
                "select": true,
                "scrollX": "100%",
                "isComplexSearch": true,
                "isAdd": true,
                "isDelete": true,
                "isExport":true,
                "bLengthChange": true, //去掉每页显示多少条数据方法
                "dom": "<'toolbar'><'top'f>t<'bottom'lip>",
                "aLengthMenu": [50, 100, 200, 500],
                "searchDelay": 500,
                "scrollY": _this.getHeight(),
                "scrollCollapse": false,
                "renderer": "bootstrap",
                "pagingType": "full_numbers",
                "language": {
                    "lengthMenu": "每页显示_MENU_条",
                    "search": _this.initSearchDom(options),
                    "paginate": {
                        previous: "上一页",
                        next: "下一页",
                        first: "首页",
                        last: "末页"
                    },
                    "zeroRecords": "没有数据",
                    "info": "共_PAGES_页，显示第_START_到第_END_，共_MAX_条 ",//左下角的信息显示，大写的词为关键字。
                    "infoEmpty": "0条记录",//筛选为空时左下角的显示。
                },
                "rowId": "ID",
                "order": [2, "desc"],
                "ajax": function (data, callback) {
                    var param = options.searchParas ? options.searchParas : _this.getSearchParams(data);
                    param["method"] = searchMethod;
                    //ajax请求数据
                    $.ajax({
                        type: "POST",
                        url: options.url,
                        cache: false, //禁用缓存
                        data: param, //传入组装的参数
                        dataType: "json",
                        success: function (result) {
                            ajaxSuccessDone(result, function () {
                                callback(_this.setPagerParas(result, data));
                            });
                        }
                    });
                }
            };
        _this.settings = $.extend({}, defaults, options);
        _this.tableObj = $('#' + _this.settings.tId).DataTable(_this.settings);
        _this.initToolbar(_this.settings);
        _this.initCheckAll();
        _this.initShowSearchEven();
        return _this;
    },
    /**
     * 获取列表的初始高度
     * @returns {列表的初始高度} 
     */
    getHeight: function () {
        return window.innerHeight - 232;
    },
    initSearchDom: function (options) {
        if (options.isComplexSearch === false) {
            return "筛选条件：_INPUT_";
        } else {
            return "筛选条件：_INPUT_<button id='btnComplexSearch' class='btn btn-link btn-sm'>高级筛选</button>";
        }        
    },
    /**
     * 设置datatables分页默认参数
     * @param {默认参数} result,{}data 
     * @returns {} 
     */
    setPagerParas: function (result, data) {
        var returnData = {};
        returnData.draw = data.draw;//这里直接自行返回了draw计数器,应该由后台返回
        returnData.recordsTotal = result.total;//返回数据全部记录
        returnData.recordsFiltered = result.total;//后台不实现过滤功能，每次查询均视作全部结果
        returnData.data = result.data;//返回的数据列表
        return returnData;
    },
    /**
     * 获取datatables选中行的ID
     */
    getSelectedRowIds: function () {
        var ids = "";
        $("input[name='tbCheckbox']").each(function () {
            if ($(this).is(":checked")) {
                if (ids === "") {
                    ids += $(this).attr("id");
                } else {
                    ids += "," + $(this).attr("id");
                }
            }
        });
        return ids;
    },
    initCheckAll: function () {
        //datatables 全选功能,全选ID必须：cbSelectAll，列表中的选择框name必须：tbCheckbox
        $("#cbSelectAll").click(function () {
            $("input[name='tbCheckbox']").each(function () {
                $(this).prop("checked", $("#cbSelectAll").is(":checked"));
            });
        });
    },
    initToolbar: function (options) {
        if (!!options.isAdd) {
            $("div.toolbar").append('<a class="btn btn-default btn-sm" href="' + options.editUrl + '" onclick="curTable.add();" title="新增"><span class="glyphicon glyphicon-plus"></span></a>');
        }
        if (!!options.isDelete) {
            $("div.toolbar").append('<a class="btn btn-default btn-sm" href="javascript:void(0);" onclick="curTable.delSelecteRows(\'' + options.delMethod + '\');"><span class="glyphicon glyphicon-trash" title="删除"></span></a>');
        } 
        if (!!options.isExport) {
            $("div.toolbar").append('<a runat="server" class="btn btn-default btn-sm" onclick="curTable.download();"><span class="glyphicon glyphicon-export" title="导出"></span></a>');
        }
    },
    reload: function () {
        this.tableObj.ajax.reload(null, false);
    },
    delSingleRow: function (action, ids) {
        bootAlert.confirm({
            message: "确认要删除选中数据吗？"
        }).on(function (result) {
            if (result) {
                $.ajax({
                    type: "POST",
                    url: curTable.settings.url,
                    cache: false, //禁用缓存
                    data: { "Id": ids, "method": action }, //传入组装的参数
                    dataType: "json",
                    success: function (result) {
                        ajaxSuccessDone(result, function () {
                            toastr.success("删除成功！");
                            tableUtils.reload();
                        });
                    }
                });
            }
        });
    },
    download: function () {
        var data = this.tableObj.ajax.params();
        var param = this.getSearchParams(data);
        param["method"] = "Download";
        $.ajax({
            type: "POST",
            url: curTable.settings.url,
            cache: false, //禁用缓存
            data: param, //传入组装的参数
            dataType: "json",
            success: function (result) {
                ajaxSuccessDone(result, function () {
                    if ($("#downloadFrame").length === 0) {
                        $("body").append("<iframe id='downloadFrame' style='display:none;' src='" + result.data + "'></iframe>")
                    } else {
                        $("#downloadFrame").attr("src", result.data);
                    }
                });
            }
        });


    },
    delSelecteRows: function (action) {
        var ids = tableUtils.getSelectedRowIds();
        if (ids !== "") {
            tableUtils.delSingleRow(action, ids);
        } else {
            toastr.warning("请选择你要删除的数据！");
        }
    },
    add: function () {
        window.location = this.settings.editUrl;
    },
    edit: function (id) {
        window.location = this.settings.editUrl + "?Id=" + id;

    },
    /**
     * 清除检索条件2
     */
    clearSearchForm: function () {
        var $formObj = $("#searchPanel .form-control");
        $formObj.each(function (index, inputObj) {
            if ($(this).attr("SearchAttr") && $(this).val() !== "") {
                switch ($(this)[0].tagName.toUpperCase()) {//表单元素类型
                    case "INPUT":
                        $(this).val("");
                        break;
                    case "SELECT":
                        $(this).val("");
                        break;
                    default:
                        break;
                }
            }
        });
    },
    /**
     * 获取检索列表参数
     * @param {datatables默认参数} data 
     * @returns {} 
     */
    getSearchParams: function (data) {
        var param = {};
        param.limit = data.length;//页面显示记录条数，在页面显示每页显示多少项的时候
        param.start = data.start;//开始的记录序号
        param.page = (data.start / data.length) + 1;//当前页码
        param.orderColunm = data.columns[parseInt(data.order[0].column)].data;//排序列名
        param.orderDir = data.order[0].dir;//排序方式DESC、ASC
        //模糊筛选条件
        param.keywords = data.search.value;
        param.searchColunms = this.settings.searchColunms;
        var $formObj = $("#searchPanel .form-control");
        $formObj.each(function (index, inputObj) {
            if ($(this).attr("SearchAttr") && $(this).val() !== "") {
                switch ($(this)[0].tagName.toUpperCase()) {//表单元素类型
                    case "INPUT":
                        param[$(inputObj).attr("SearchAttr")] = $(this).val();
                        break;
                    case "SELECT":
                        param[$(inputObj).attr("SearchAttr")] = $(this).val();
                        break;
                    default:
                        break;
                }
            }
        });
        return param;
    },
    /**
     * 初始化弹出高级筛选
     */
    initShowSearchEven: function () {
        $("#btnComplexSearch").click(function (event) {
            event.preventDefault();
            bootAlert.dialog({
                title: '高级筛选',
                targetId: "searchPanel",
                width: 700,
                height: 550,
            });
        });
        $("#btnSearch").click(function () {
            curTable.reload();
            closeModal();
        });
        $("#btnClear").click(function () {
            curTable.clearSearchForm();
        });
    }
}