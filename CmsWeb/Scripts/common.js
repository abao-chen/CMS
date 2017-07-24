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

/**
 * 初始化
 */
$(function () {
    //菜单
    $('#sideMenu').metisMenu();

    //datatables 全选功能,全选ID必须：cbSelectAll，列表中的选择框name必须：tbCheckbox
    $("#cbSelectAll").click(function () {
        $("input[name='tbCheckbox']").each(function () {
            if ($("#cbSelectAll").is(":checked")) {
                $(this).attr("checked", true);
            } else {
                $(this).attr("checked", false);
            }
        });
    });

    //初始化查询条件的折叠样式
    if ($("#searchBody")) {
        $("#searchBody").on("hidden.bs.collapse", function () {
            $(".glyphicon.glyphicon-menu-up").removeClass("glyphicon-menu-up").addClass("glyphicon-menu-down");
        }).on("shown.bs.collapse", function () {
            $(".glyphicon.glyphicon-menu-down").removeClass("glyphicon-menu-down").addClass("glyphicon-menu-up");
        });
    };

    //模板布局
    $(window).bind("load resize", function () {
        var topOffset = 50;
        var width = (this.window.innerWidth > 0) ? this.window.innerWidth : this.screen.width;
        if (width < 768) {
            $('div.navbar-collapse').addClass('collapse');
            topOffset = 100; // 2-row-menu
        } else {
            $('div.navbar-collapse').removeClass('collapse');
        }

        var height = ((this.window.innerHeight > 0) ? this.window.innerHeight : this.screen.height) - 1;
        height = height - topOffset;
        if (height < 1) height = 1;
        if (height > topOffset) {
            $("#page-wrapper").css("min-height", (height) + "px");
        }
    });

    //菜单选中效果
    var currentPath = window.location.pathname;
    if (currentPath.indexOf("Info.aspx") > 0) {
        var parentPath = currentPath.replace("Info.aspx", "List.aspx");
        $("a[href='" + parentPath + "']").addClass("active").parent().parent().addClass("in");
    } else {
        $("a[href='" + currentPath + "']").addClass("active").parent().parent().addClass("in");
    }
});

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
 * 关闭模态窗口
 * @returns {} 
 */
function closeModal() {
    if ($(".modal")) {
        $($(".modal")).modal("hide");
    }
}

/**
 * 获取form提交数据
 * @param {form ID} formId 
 * @returns {} 
 */
function getFormPostData(formId) {
    var formArray = $("#" + formId).serializeArray();
    var postData = {};
    var contrlName;
    for (var i = 0; i < formArray.length; i++) {
        if (formArray[i].value != "") {
            if (formArray[i].name.split("$").length == 3) {
                contrlName = formArray[i].name.split("$")[2];
            } else {
                contrlName = formArray[i].name;
            }
            postData[contrlName] = formArray[i].value;
        }
    }
    return postData;
};

/**
 * 获取URL参数值
 * @param {参数名} name 
 * @returns {} 
 */
function getUrlParams(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) {
        return unescape(r[2]);
    } else {
        return null;
    }
};

/**
 * 获取列表的初始高度
 * @returns {列表的初始高度} 
 */
function getTableHeight() {
    if ($("#searchPanel:visible").length > 0) {
        return window.innerHeight - $("#searchPanel").height() - 249;
    } else {
        return window.innerHeight - 232;
    }
}

/**
 * ajax 全局设置
 * @returns {} 
 */
$(document).ajaxStart(function () {
    showLoading(true);
});
$(document).ajaxSuccess(function () {
    showLoading(false);
});

/**
 * 显示loading遮罩层
 * @param {是否显示} bool 
 * @param {显示文字} text 
 * @returns {} 
 */
function showLoading(bool, text) {
    var $loadingpage = $("#loading");
    var $loadingtext = $loadingpage.find('.loading-content');
    if (bool) {
        $loadingpage.show();
    } else {
        if ($loadingtext.attr('istableloading') == undefined) {
            $loadingpage.hide();
        }
    }
    if (!!text) {
        $loadingtext.html(text);
    } else {
        $loadingtext.html("数据加载中，请稍后…");
    }
    $loadingtext.css("left", (top.$('body').width() - $loadingtext.width()) / 2 - 50);
    $loadingtext.css("top", (top.$('body').height() - $loadingtext.height()) / 2);
}

/**
 * 打开Tab
 * @param {} tabId 
 * @param {} tabName 
 * @param {} url 
 * @returns {} 
 */
function openTab(tabId, tabName, url) {
    //是否已经打开Tab
    var isOpen = false;
    $("#myTab a[role='tab']").each(function () {
        if ($(this).attr("href") == ("#" + tabId)) {
            isOpen = true;
        }
    });
    if (!isOpen) {//不存在已经打开的Tab
        $("#myTab").append('<li role="presentation" class=""><a href="#' +
            tabId + '" role="tab" data-toggle="tab" aria-controls="' +
            tabId + '" >' + tabName + '</a></li>');
        //'<iframe  role="tabpanel" class="tab-pane" width="100%" height="100%" src="' + url + '" frameborder="0" id="' + tabId + '" seamless></iframe>';
        //$("#myTabContent").append('<div role="tabpanel" class="tab-pane" id="' + tabId + '"></div>');
        $("#myTabContent").append('<iframe role="tabpanel" class="tab-pane" width="100%" height="500px" src="' + url + '" frameborder="0" id="' + tabId + '" seamless></iframe>');
        $("#" + tabId).load(function () {
            var mainheight = $(this).contents().find("body").height() + 30;
            $(this).height(mainheight);
        });
    }
    $("#myTab a[href='#" + tabId + "]'").tab("show");
};