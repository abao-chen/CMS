/**
 * 日期格式化
 * @param {日期格式} fmt
 * @returns {返回已格式化日期结果} 
 */
Date.prototype.Format = function (fmt) {
    var o = {
        "M+": this.getMonth() + 1,
        //月份 
        "d+": this.getDate(),
        //日 
        "h+": this.getHours(),
        //小时 
        "m+": this.getMinutes(),
        //分 
        "s+": this.getSeconds(),
        //秒 
        "q+": Math.floor((this.getMonth() + 3) / 3),
        //季度 
        "S": this.getMilliseconds() //毫秒 
    };
    if (/(y+)/.test(fmt)) {
        fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    }
    for (var k in o) {
        if (new RegExp("(" + k + ")").test(fmt)) {
            fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
        }
    }
    return fmt;
};

/**
 * 获取检索列表参数
 * @param {datatables默认参数} data 
 * @returns {} 
 */
function getSearchParams(data) {
    var param = {};
    param.limit = data.length;//页面显示记录条数，在页面显示每页显示多少项的时候
    param.start = data.start;//开始的记录序号
    param.page = (data.start / data.length) + 1;//当前页码
    param.orderColunm = data.columns[parseInt(data.order[0].column)].data;//排序列名
    param.orderDir = data.order[0].dir;//排序方式DESC、ASC
    var $formObj = $("#searchPanel .form-control");
    $formObj.each(function (index, inputObj) {
        if ($(this).attr("SearchAttr") && $(this).val() != "") {
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
};

/**
 * 清除检索条件
 * @returns {} 
 */
function clearSearchForm() {
    var $formObj = $("#searchPanel .form-control");
    $formObj.each(function (index, inputObj) {
        if ($(this).attr("SearchAttr") && $(this).val() != "") {
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
}

/**
 * 设置datatables分页默认参数
 * @param {默认参数} result,{}data 
 * @returns {} 
 */
function setDataTablesPagerParas(result, data) {
    var returnData = {};
    returnData.draw = data.draw;//这里直接自行返回了draw计数器,应该由后台返回
    returnData.recordsTotal = result.total;//返回数据全部记录
    returnData.recordsFiltered = result.total;//后台不实现过滤功能，每次查询均视作全部结果
    returnData.data = result.data;//返回的数据列表
    return returnData;
};

/**
 * datatables 全选功能,全选ID必须：cbSelectAll，列表中的选择框name必须：tbCheckbox
 */
$(function () {
    $("#cbSelectAll").click(function () {
        $("input[name='tbCheckbox']").each(function () {
            if ($("#cbSelectAll").is(":checked")) {
                $(this).attr("checked", true);
            } else {
                $(this).attr("checked", false);
            }
        });
    });
});

/**
 * 获取datatables选中行的ID
 */
function getSelectedRowIds() {;
    var ids = "";
    $("input[name='tbCheckbox']").each(function () {
        if ($(this).is(":checked")) {
            if (ids == "") {
                ids += $(this).attr("id");
            } else {
                ids += "," + $(this).attr("id");
            }
        }
    });
    return ids;
}

/**
 * 初始化日期控件
 * @param {} inputId 
 * @returns {} 
 */
function initDateControl(inputId) {
    $("#" + inputId).datetimepicker({
        weekStart: 1,
        todayBtn: 1,
        autoclose: 1,
        todayHighlight: 1,
        startView: 2,
        forceParse: 0,
        minView: 2,
        format: "yyyy/mm/dd"
    });
}

/**
 * toastr 初始化
 */
toastr.options = {
    closeButton: true,
    debug: true,
    progressBar: true,
    positionClass: "toast-top-center",
    onclick: null,
    showDuration: "300",
    hideDuration: "1000",
    timeOut: "3000",
    extendedTimeOut: "1000",
    showEasing: "swing",
    hideEasing: "linear",
    showMethod: "fadeIn",
    hideMethod: "fadeOut"
};