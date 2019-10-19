function setupWebViewJavascriptBridge(callback) {
    if (window.WebViewJavascriptBridge) { return callback(WebViewJavascriptBridge); }
    if (window.WVJBCallbacks) { return window.WVJBCallbacks.push(callback); }
    window.WVJBCallbacks = [callback];
    var WVJBIframe = document.createElement('iframe');
    WVJBIframe.style.display = 'none';
    WVJBIframe.src = 'wvjbscheme://__BRIDGE_LOADED__';
    document.documentElement.appendChild(WVJBIframe);
    setTimeout(function () { document.documentElement.removeChild(WVJBIframe) }, 0);
}
function callAPP(data) {
    setupWebViewJavascriptBridge(function (bridge) {
        bridge.callHandler('HTAPP_native', data, function responseCallback(responseData) {
            console.log("JS received response:", responseData)
        });
    });
    WebViewJavascriptBridge.callHandler(
        'submitFromWeb'
        , data
        , function (responseData) {
        }
    );
}
//返回顶部
function backToTop() {
    var $backToTopEle = $('<div class="backToTop"></div>').appendTo($("body")).click(function () { $("html, body").animate({ scrollTop: 0 }, 120); });
    var $backToTopFun = function () {
        var st = $(document).scrollTop(), winh = $(window).height();
        (st > 0) ? $backToTopEle.show() : $backToTopEle.hide();
        //IE6下的定位
        if (!window.XMLHttpRequest) {
            $backToTopEle.css("top", st + winh - 166);
        }
    };
    $(window).bind("scroll", $backToTopFun);
    $backToTopFun();
}

//JS翻页
function page_pre(num) {
    document.onkeydown = nextpage;
    var pre = num - 1;
    var next = num + 1;
    var url = window.location.href;
    if (url.split("?").length > 1) {
        url = url.replace(/&page=\d+/g, "");
        var prevpage = url + "&page=" + pre;
        var nextpage = url + "&page=" + next;
    }
    else {
        var prevpage = url + "?page=" + pre;
        var nextpage = url + "?page=" + next;
    }
    function nextpage(event) {
        event = event ? event : (window.event ? window.event : null);
        if (event.keyCode == 37) location = prevpage;//向左
        if (event.keyCode == 39) location = nextpage;//向右
    }
}
///鼠标延迟
(function ($) {
    $.fn.hoverDelay = function (options) {
        var defaults = {
            hoverDuring: 500,
            outDuring: 500,
            hoverEvent: function () {
                $.noop();
            },
            outEvent: function () {
                $.noop();
            }
        };
        var sets = $.extend(defaults, options || {});
        var hoverTimer, outTimer, that = this;
        return $(this).each(function () {
            $(this).hover(function () {
                clearTimeout(outTimer);
                hoverTimer = setTimeout(function () { sets.hoverEvent.apply(that) }, sets.hoverDuring);
            }, function () {
                clearTimeout(hoverTimer);
                outTimer = setTimeout(function () { sets.outEvent.apply(that) }, sets.outDuring);
            });
        });
    }
})(jQuery);


///延迟刷新页面
function page_reload() {
    setTimeout(function () {
        document.location.reload();
    }, 1000);
}

///显示导航分类div
function nav_class_show() {
    $(".header a").each(function () {
        $(this).removeClass("on");
    })
    $('.nav_class').addClass("on");
    nav_class_info(0);
    tips_out("", 0);
    $('.p_nav_info').css('display', 'block');
    $('.p_nav_info').fadeIn();
}

///显示导航分类content
function nav_class_info(id) {
    $.post("/ajax/tuan/nav_class.php", { id: id }, function (data) {
        $("#nav_info").html(data);
    })
}
//显示分类导航
$(".nav_class").mouseover(function () {
    tips_in();
    tips_out('', 500);
});

$(".nav_class").hoverDelay({
    hoverEvent: function () {
        nav_class_show();
    }
});
////关闭导航
$('.p_nav_c').mouseleave(function () {
    $('.nav_class').removeClass("on");
    $('.p_nav_info').css('display', 'none');
});
jQuery(document).ready(
function ($) {
    $(".p_nav_l a").mouseover(function () {
        var id = $(this).attr("data");
        $(".p_nav_l a").each(function () {
            $(this).removeClass("selected");
        })
        $(this).addClass("selected");
        nav_class_info(id);
    });
});

///提示进入
function tips_in() {
    var content = "<div class='loader'></div>";
    $("#web_back_tips").html(content);
    $('#web_back_tips').fadeIn();
}
///关闭
function tips_out(msg, time, pid) {
    if (!time) { var time = "500"; }
    if (!pid) { var pid = "web_back_tips"; }
    if (msg) {
        $("#" + pid).html("<p>" + msg + "</p>");
        $("#" + pid).fadeIn();
    }
    $("#" + pid).fadeOut(time);
}

///需手动关闭
function tips_out_wrong(msg) {
    $("#web_back_tips").html("<p>" + msg + "</p><p><a href=\"javascript:void(0)\"  onclick=\"tips_out();return false;\" class='edit'>知道了</a></p>");
    $('#web_back_tips').fadeIn();
}

///大窗
function tips_out_big(msg) {
    $("#web_tips_big").html(msg + "<p class='icons fr'><a href=\"javascript:void(0)\"  onclick=\"tips_out('','','web_tips_big');return false;\"><i class='del'>知道了</i></a></p>");
    $('#web_tips_big').fadeIn();
}


////遮罩//////
function popup_in(msg) {
    tips_in();
    $('#cd-popup').addClass('is-visible');
    tips_out('', 0);
    if (msg) {
        $("#cd-popup-container").html(msg);
    } else {
    };

}
function popup_close() {
    $('#cd-popup').removeClass('is-visible');
}

/*描点*/
function goto2(elements) {
    var pos = 0;
    if (elements !== "") {
        pos = $("#" + elements).offset().top;
    };
    $("html,body").animate({ scrollTop: pos - 20 }, 300);
}
$(function () {
    $("#ship_left_service a").click(function () {
        $("#ship_left_service a").removeClass("on");
        $(this).addClass("on");
    });
});


function nofind() {
    var img = event.srcElement;
    img.src = "http://static.haitao.com/img/no.jpg";
    img.onerror = null;
}

function no_user_logo() {
    var img = event.srcElement;
    img.src = "http://static.haitao.com/img/my/pic/noavatar_small.jpg?v=3";
    img.onerror = null;
}