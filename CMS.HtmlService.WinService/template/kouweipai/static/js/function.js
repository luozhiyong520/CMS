///清空搜索记录
function search_clear() {
    var action = "search_clear";
    tips_in();
    $.post('/ajax/tuan/product.php', { 'action': action }, function (msg) {//
        if ('1' == msg) {
            tips_out("搜索记录已清空", 1000);
            $('#my_search_record').fadeOut(1000);
        } else {
            tips_out_wrong(msg);
        }
    });
}

///团购运费查询
function tuan_ship_price(sid) {
    var action = "tuan_ship_price";
    $.post('/ajax/tuan/product.php', { 'action': action, 'sid': sid }, function (msg) {//
        popup_in(msg);
    });
}

///添加购物车
function add_car(pid, store_id, ptype) {
    var action = "add_car";
    var user_hart = parseInt($("#tuan_num").text());
    tips_in();
    $.post('/ajax/tuan/shopping_car.php', { 'pid': pid, 'action': action, 'ptype': ptype }, function (msg) {//
        if ('1' == msg) {
            $("#tuan_num").text(user_hart + 1);
            if (ptype == 1) {
                var content = "已放入购物车";
            } else {
                var content = "已放入购物车</p><p><a href=\"http://my.haitao.com/car/?store_id=" + store_id + "\"><span class=\"edit\">立即查看</span></a>";
            }
            tips_out(content, 3000);
        } else {
            tips_out(msg, 2000);
        }
    });
}

///收藏团购
function tuan_collect(pid, type) {
    var action = "tuan_collect";
    var user_hart = parseInt($("#tuan_collect_" + pid).text());
    tips_in();
    $.post('/ajax/tuan/shopping_car.php', { 'pid': pid, 'action': action, 'type': type }, function (msg) {//
        if ('1' == msg) {
            $("#tuan_collect_" + pid).text(user_hart + 1);
            tips_out("收藏成功", 1000);
        } else if ('2' == msg) {
            $("#tuan_collect_" + pid).text(user_hart - 1);
            tips_out("取消收藏成功", 1000);
        }
        else {
            tips_out(msg, 3000);
        }
    });
}

/////viewcount
function tuan_view_count(vid, type) {
    var action = 'tuan_view_count';
    $.post('/ajax/tuan/view_count.php', { 'vid': vid, 'type': type, 'action': action }, function (msg) {//
        var content = msg;
        $("#view_count").html(content);
    });
}


//添加评论
function add_review(ptype, pid, review_id) {
    var action = 'add_review';
    var review_info = $('#review_info').val();
    var pic = $("#myImage_100").attr("src");

    var review_info_pids_type = $('#review_info_pids_type').val();
    var review_info_pids = $('#review_info_pids').val();
    tips_in();
    $.post('/ajax/review.php', { 'ptype': ptype, 'pid': pid, 'review_id': review_id, 'action': action, 'review_info': review_info, 'pic': pic, 'review_info_pids_type': review_info_pids_type, 'review_info_pids': review_info_pids }, function (msg) {//
        if ('1' == msg) {
            tips_out("发布成功", 500);
            page_reload();
        } else {
            tips_out(msg, 2000);
        }
    });
};

//删除评论
function del_review(id, type) {
    var action = 'del_review';
    tips_in();
    $.post('/ajax/review.php', { 'action': action, 'id': id, 'ptype': type }, function (msg) {//
        if ('1' == msg) {
            tips_out("删除成功", 500);
            $('#review_item_' + type + '_' + id).fadeOut("slow", 0);
        } else {
            tips_out(msg, 2000);
        }
    });
};

//置顶评论
function top_review(id, type) {
    var action = 'top_review';
    tips_in();
    $.post('/ajax/review.php', { 'action': action, 'id': id, 'ptype': type }, function (msg) {//
        if ('1' == msg) {
            tips_out("成功", 500);
            page_reload();
        } else {
            tips_out(msg, 2000);
        }
    });
};


///收藏导购商品
function faxian_product_collect(pid, type) {
    var action = "faxian_product_collect";
    var user_hart = parseInt($("#product_dig_" + pid).text());
    tips_in();
    $.post('/ajax/faxian/product.php', { 'pid': pid, 'action': action, 'type': type }, function (msg) {//
        if ('1' == msg) {
            $("#product_dig_" + pid).text(user_hart + 1);
            tips_out("收藏成功", 1000);
        } else if ('2' == msg) {
            $("#product_dig_" + pid).text(user_hart - 1);
            tips_out("取消收藏成功", 1000);
        }
        else {
            tips_out(msg, 2000);
        }
    });
}

///收藏导购促销
function faxian_promo_collect(pid, type) {
    var action = "faxian_promo_collect";
    var user_hart = parseInt($("#promo_collect_" + pid).text());
    tips_in();
    $.post('/ajax/faxian/promo.php', { 'pid': pid, 'action': action, 'type': type }, function (msg) {//
        if ('1' == msg) {
            $("#promo_collect_" + pid).text(user_hart + 1);
            tips_out("收藏成功", 1000);
        } else if ('2' == msg) {
            $("#promo_collect_" + pid).text(user_hart - 1);
            tips_out("取消收藏成功", 1000);
            page_reload();
        }
        else {
            tips_out(msg, 2000);
        }
    });
}

///评论dig
function faxian_review_dig(id, type) {
    var action = "faxian_review_dig";
    var user_hart = parseInt($("#review_dig_" + id).text());
    if (isNaN(user_hart)) { user_hart = 0; }
    tips_in();
    $.post('/ajax/faxian/product.php', { 'id': id, 'type': type, 'action': action }, function (msg) {//
        if ('1' == msg) {
            $("#review_dig_" + id).text(user_hart + 1);
            tips_out("点赞成功", 1000);
        } else {
            tips_out(msg, 2000);
        }
    });
}

///发布分享
function share_edit(id, type) {
    if (type == 0) { window.EditorObject.sync(); }
    var action = "share_edit";
    tips_in();
    var info = $('#info').val();
    var title = $('#title').val();
    var remark = $('#remark').val();
    var tags = $('#tags').val();
    var class_id = $('#class_id').val();
    var status = $('#status').val();
    var pic = $("#myImage_1").attr("src");
    var asso_type = $('#asso_type').val();
    var asso_id = $('#asso_id').val();
    var url = $('#url').val();
    var sku_id = $('#sku_id').val();
    var product_id = $('#product_id').val();
    var upc_id = $('#upc_id').val();
    var store_id = $('#store_id').val();
    var promo = $('#promo').val();
    ////var selectedItems = new Array();
    ///$("input[@name='share_items[]']:checked").each(function() {selectedItems.push($(this).val());});
    $.post('/ajax/share/edit.php', { 'id': id, 'action': action, 'title': title, 'remark': remark, 'pic': pic, 'info': info, 'tags': tags, 'class_id': class_id, 'status': status, 'asso_type': asso_type, 'asso_id': asso_id, 'url': url, 'sku_id': sku_id, 'product_id': product_id, 'upc_id': upc_id, 'store_id': store_id, 'promo': promo }, function (msg) {//
        if ('1' == msg) {
            tips_out("修改成功", 1000);
            page_reload();
        } else if ('0' == msg) {
            tips_out_wrong("发布失败 请重试");
        } else if (msg > 1) {
            tips_out("发布成功", 500);
            var url = "edit.php?id=" + msg;
            window.location = url;
        }
        else {
            tips_out(msg, 2000);
        }
    });
};


///公开分享
function share_edit_publish(id) {
    var action = "share_edit_publish";
    tips_in();
    $.post('/ajax/share/edit.php', { 'id': id, 'action': action }, function (msg) {//
        if ('1' == msg) {
            tips_out("发布成功", 1000);
            page_reload();
        } else {
            tips_out(msg, 2000);
        }
    });
};

///更新分享主图
function share_logo_edit(id) {
    var action = "share_logo_edit";
    tips_in();
    var pic = $("#myImage_1").attr("src");
    $.post('/ajax/share/edit.php', { 'id': id, 'action': action, 'pic': pic }, function (msg) {//
        if ('1' == msg) {
            tips_out("修改成功", 1000);
            page_reload();
        } else {
            tips_out(msg, 2000);
        }
    });
};
///手机追加内容
function share_edit_add(id, type) {
    if (type == 0) { window.EditorObject.sync(); }
    var action = "share_edit_add";
    tips_in();
    var info = $('#info').val();
    var pic = $("#myImage_1").attr("src");

    var info_2 = $('#info_2').val();
    var pic_2 = $("#myImage_2").attr("src");
    var info_3 = $('#info_3').val();
    var pic_3 = $("#myImage_3").attr("src");

    $.post('/ajax/share/edit.php', { 'id': id, 'action': action, 'pic': pic, 'info': info, 'info_2': info_2, 'pic_2': pic_2, 'info_3': info_3, 'pic_3': pic_3 }, function (msg) {//
        if ('1' == msg) {
            tips_out("添加成功", 1000);
            var url = "edit.php?id=" + id;
            window.location = url;
        }
        else {
            tips_out(msg, 2000);
        }
    });
};
///发布分享时的搜索
function share_upc_search(id) {
    var action = "share_upc_search";
    var keyword = $('#keyword').val();
    $.post('/ajax/share/edit.php', { 'action': action, 'keyword': keyword, 'id': id }, function (msg) {//
        if ('1' == msg) {
            tips_out_wrong("请输入关键词");
        } else {
            $('#share_search_result').html(msg);
        }
    });
}
///添加分享关联upc
function share_upc_add(upc_id, id) {
    var action = "share_upc_add";
    tips_in();
    $.post('/ajax/share/edit.php', { 'action': action, 'id': id, 'upc_id': upc_id }, function (msg) {//
        if ('1' == msg) {
            tips_out("添加成功", 1000);
            ///page_reload();
            ////$('#upc_item_' + id).fadeOut("slow", 0);
        } else {
            tips_out(msg, 2000);
        }
    });
}
///删除分享关联upc
function del_share_upc(id) {
    var action = "del_share_upc";
    tips_in();
    $.post('/ajax/share/edit.php', { 'action': action, 'id': id }, function (msg) {//
        if ('1' == msg) {
            tips_out("", 0);
            ///page_reload();
            $('#share_asso_' + id).fadeOut("slow", 0);
        } else {
            tips_out(msg, 2000);
        }
    });
}

//收藏通用
function collect(ptype, id, type) {
    tips_in();
    $.post('/ajax/collect.php', { 'ptype': ptype, 'id': id, 'type': type }, function (msg) {//
        if ('1' == msg) {
            if (type == 1) {
                tips_out("收藏成功");
                $("#collect_" + ptype + "_" + id).removeClass("f");
            }
            if (type == 2) {
                tips_out("取消成功");
                $("#collect_" + ptype + "_" + id).addClass("f");
            }
        } else {
            tips_out(msg, 2000);
        }
    });
}

///点赞通用
function dig(ptype, id) {
    var dig_num = parseInt($("#dig_" + ptype + "_" + id).text());
    if (isNaN(dig_num)) { dig_num = 0; }
    tips_in();
    $.post('/ajax/dig.php', { 'id': id, 'ptype': ptype }, function (msg) {//
        if ('1' == msg) {
            tips_out('', 0);
            $("#dig_" + ptype + "_" + id).text(dig_num + 1);
            ///tips_out("点赞成功",1000);
        } else {
            tips_out(msg);
        }
    });
}


///发布分享时的导购商家搜索
function share_store_search(id) {
    var action = "share_store_search";
    var keyword = $('#store_keyword').val();

    $.post('/ajax/share/edit.php', { 'action': action, 'keyword': keyword, 'id': id }, function (msg) {//
        if ('1' == msg) {
            tips_out("请输入关键词", 2000);
        } else {
            $('#share_store_search_result').html(msg);
        }
    });
}
///添加分享关联商家
function share_store_add(store_id, id) {
    var action = "share_store_add";
    tips_in();
    $.post('/ajax/share/edit.php', { 'action': action, 'id': id, 'store_id': store_id }, function (msg) {//
        if ('1' == msg) {
            tips_out("添加成功", 1000);
        } else {
            tips_out(msg, 2000);
        }
    });
}
///删除分享关联商家
function del_share_store(id) {
    var action = "del_share_store";
    tips_in();
    $.post('/ajax/share/edit.php', { 'action': action, 'id': id }, function (msg) {//
        if ('1' == msg) {
            tips_out("", 0);
            $('#store_item_' + id).fadeOut("slow", 0);
        } else {
            tips_out(msg, 2000);
        }
    });
}
///pricerenew
function price_renew(id, huilv) {
    var action = "price_renew";
    var seller_price = $("#seller_price").text();
    var china_price = $("#china_price").text();
    //$('#seller_price').html("Updating Price Now…");
    $('#price_renew_load').html("更新价格…");
    $.post('/ajax/faxian/price_renew.php', { 'action': action, 'id': id }, function (msg) {//
        if ('' == msg) {
            $('#seller_price').html(seller_price);
            $('#china_price').html(china_price);
            $('#price_renew_load').html("");
        }
        if (0 == msg) {
            ////$('#seller_price').html(seller_price);
            $('#price_renew_load').html("");
        }
        else {
            $('#seller_price').html(msg);
            $('#china_price').html(Math.round(msg * huilv, 2));
            $('#price_renew_load').html("");
        }
    });
}
///SKU相关商品
function sku_pop_more(id) {
    var action = "sku_pop_more";
    var data = "action=sku_pop_more&id=" + id;
    $("#more_sku a").removeClass();
    $("#pop_sku_" + id).addClass("on");
    $.getJSON("/ajax/tuan/sku_more.php", data, function (json) {
        var new_pic = json.pic;
        var new_sku_id = json.id;
        $("#pop_sku_id").html(new_sku_id);
        $("#pop_sku_price").html(json.price);
        $("#pop_sku_pic").attr("src", new_pic);
        $("#pop_sku_add_car").attr("onclick", "add_car(" + new_sku_id + ");return false;");
        $("#pop_sku_url").attr("href", "/tuan/view.php?id=" + new_sku_id);
    });
}

///弹窗DIV展现
function popup_in_tips(action, id) {
    var msg = $("#" + action + '_' + id).html();
    popup_in(msg);
}

///商家申请
function tuan_store_add() {
    var action = "tuan_store_add";
    var area_id = $('#area_id').val();
    var type = $('#type').val();
    var name = $('#store_name').val();
    var manager_name = $('#manager_name').val();
    var tel = $('#tel').val();
    var intro = $('#intro').val();
    $.post('/ajax/tuan/store.php', { 'action': action, 'area_id': area_id, 'type': type, 'name': name, 'manager_name': manager_name, 'tel': tel, 'intro': intro }, function (msg) {//
        if ('1' == msg) {
            tips_out("提交成功", 2000);
            $('#store_check_info').html("<p>商家入驻申请已提交。</p><p>商务专员会尽快与您联系。谢谢。</p>");
        }
        else {
            tips_out_wrong(msg);
        }
    });
}
///app tips
function app_down_tips_out() {
    document.cookie = 'app_down_tips=1;expires=360000;domain=.haitao.com;'
    $('#app_down_tips').slideUp();
}
///获取分享清单
function get_share_qingdan(type, id) {
    var action = "get_share_qingdan";
    tips_in();
    $.post('/ajax/share/qingdan.php', { 'action': action, 'type': type, 'id': id }, function (msg) {//
        if ('1' == msg) {
            tips_out("提交成功", 2000);
            $('#store_check_info').html("<p>商家入驻申请已提交。</p><p>商务专员会尽快与您联系。谢谢。</p>");
        }
        else {
            popup_in(msg);
        }
    });
}
///添加分享清单商品
function share_qingdan_add(type, id) {
    var action = "share_qingdan_add";
    var share_id = $("input[name='share_radio']:checked").val();
    var intro = $('#share_qindan_intro').val();
    tips_in();
    $.post('/ajax/share/qingdan.php', { 'action': action, 'type': type, 'id': id, 'share_id': share_id, 'intro': intro }, function (msg) {//
        if ('1' == msg) {
            tips_out("提交成功", 2000);
            popup_close();
        }
        else {
            tips_out_wrong(msg);
        }
    });
}
///降价提醒
function price_notic_edit(id, type) {
    var action = 'price_notic_edit';
    var price = $('#price_notic').val();
    tips_in();
    $.post('/ajax/faxian/product.php', { 'id': id, 'action': action, 'type': type, 'price': price }, function (msg) {//
        if ('1' == msg) {
            tips_out("提交成功<br>降价自动提醒您", 1000);
            page_reload();
        }
        else if ('2' == msg) {
            tips_out("取消成功", 1000);
            page_reload();
        } else {
            tips_out_wrong(msg);
        }
    });
};
//////固定fix
$(function () {
    var nav = $(".add_fix"); //得到导航对象
    var win = $(window); //得到窗口对象
    var sc = $(document);//得到document文档对象
    win.scroll(function () {
        if (sc.scrollTop() >= 160) {
            nav.addClass("fixed");
            ////$("#header").fadeIn(); 
        } else {
            nav.removeClass("fixed");
            //// $("#header").fadeOut();
        }

    })

    //鼠标经过li对象时，查找li下的ul.
    $(".div_hide").hover(function () {
        $(this).find("ul").show();
    }, function () {
        $(this).find("ul").hide();
    })
})

///l
function coupon_add() {
    tips_in();
    var action = "coupon_add";
    var coupon_title = $('#coupon_title').val();
    var coupon_url = $('#coupon_url').val();
    var coupon_code = $('#coupon_code').val();
    var coupon_info = $('#coupon_info').val();
    $.post('/ajax/faxian/coupon.php', { 'coupon_title': coupon_title, 'coupon_url': coupon_url, 'coupon_code': coupon_code, 'action': action, 'coupon_info': coupon_info }, function (msg) {
        if ('1' == msg) {
            tips_out("分享成功", 2000);
            page_reload();
        } else {
            tips_out_wrong(msg);
        }
    });
}

/*right*/
function htmlScroll() {
    var top = document.body.scrollTop || document.documentElement.scrollTop;
    if (elFix.data_top < top) {
        elFix.style.position = 'fixed';
        elFix.style.top = 0;
        elFix.style.left = elFix.data_left;
    }
    else {
        elFix.style.position = 'static';
    }
}

function htmlPosition(obj) {
    var o = obj;
    var t = o.offsetTop;
    var l = o.offsetLeft;
    while (o = o.offsetParent) {
        t += o.offsetTop;
        l += o.offsetLeft;
    }
    obj.data_top = t;
    obj.data_left = l;
}

var oldHtmlWidth = document.documentElement.offsetWidth;
window.onresize = function () {
    var newHtmlWidth = document.documentElement.offsetWidth;
    if (oldHtmlWidth == newHtmlWidth) {
        return;
    }
    oldHtmlWidth = newHtmlWidth;
    elFix.style.position = 'static';
    htmlPosition(elFix);
    htmlScroll();
}
window.onscroll = htmlScroll;

var elFix = document.getElementById('fixedRight');
htmlPosition(elFix);