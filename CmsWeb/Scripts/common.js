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
 * 字符串全部替换
 * @param {原要替换的字符串} source 
 * @param {替换为的字符串} target 
 * @returns {} 
 */
String.prototype.replaceAll = function (source, target) {
    return this.replace(new RegExp(source, 'gm'), target);
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
            $(this).prop("checked", $("#cbSelectAll").is(":checked"));
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
 * Ajax请求成功处理
 * @param {} func 
 * @returns {} 
 */
function ajaxSuccessDone(ajaxResult, successFun, failFun) {
    if (ajaxResult.result == 1) { //请求成功
        if (typeof successFun === "function") {
            successFun();
        }
    } else if (ajaxResult.result == 2) { //请求失败
        if (typeof failFun === "function") {
            failFun();
        } else {
            toastr.error(ajaxResult.message);
        }
    } else if (ajaxResult.result == 3) {//登录超时
        top.bootAlert.alert(ajaxResult.message).on(function () {
            location.href = "/Login.aspx";
        });
    } else { //其他异常情况
        top.toastr.error(ajaxResult.message);
    }
}

/**
 * 获取UUID
 */
function uuid() {
    var s = [];
    var hexDigits = "0123456789abcdef";
    for (var i = 0; i < 36; i++) {
        s[i] = hexDigits.substr(Math.floor(Math.random() * 0x10), 1);
    }
    s[14] = "4";  
    s[19] = hexDigits.substr((s[19] & 0x3) | 0x8, 1);  
    s[8] = s[13] = s[18] = s[23] = "-";
    var uuid = s.join("");
    return uuid;
}