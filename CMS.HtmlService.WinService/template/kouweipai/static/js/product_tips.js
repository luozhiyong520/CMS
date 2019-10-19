jQuery(document).ready(
function ($) {
    $(".pids").each(function () {
        var sku_ids = $(this).attr("data");
        var th = $(this);
        if (sku_ids) {
            $(th).html("<div class='loader '></div>");
            $.post('/ajax/tuan/sku_intro.php', { 'sku_ids': sku_ids }, function (msg) {//
                $(th).html(msg);
                $("img").lazyload({});
            });
        }
    })
    /////share
    $(".shareids").each(function () {
        var share_ids = $(this).attr("data");
        var th = $(this);
        if (share_ids) {
            $(th).html("<div class='loader '></div>");
            var action = "1";
            $.post('/ajax/tuan/sku_intro.php', { 'share_ids': share_ids, 'action': action }, function (msg) {//
                $(th).html(msg);
                $("img").lazyload({});
            });
        }
    })
    ///
    /////share
    $(".faxian_pids").each(function () {
        var faxian_pids = $(this).attr("data");
        var th = $(this);
        if (faxian_pids) {
            var action = "2";
            $(th).html("<div class='loader '></div>");
            $.post('/ajax/tuan/sku_intro.php', { 'faxian_pids': faxian_pids, 'action': action }, function (msg) {//
                $(th).html(msg);
                $("img").lazyload({});
            });
        }
    })
    ///

    ////mini 
    $(".mini_tuan_pids").each(function () {
        var sku_ids = $(this).attr("data");
        if (!sku_ids) { var sku_ids = $(this).attr("id"); }
        var th = $(this);
        var them = "1";
        if (sku_ids) {
            $(th).html("<div class='loader '></div>");
            $.post('/ajax/tuan/sku_intro.php', { 'sku_ids': sku_ids, 'them': them }, function (msg) {//
                $(th).html(msg);
                $("img").lazyload({});
            });
        }
    })

    ////mini 
    $(".mini_faxian_pids").each(function () {
        var faxian_pids = $(this).attr("data");
        if (!faxian_pids) { var faxian_pids = $(this).attr("id"); }
        var th = $(this);
        var them = "1";
        if (faxian_pids) {
            var action = "2";
            $(th).html("<div class='loader '></div>");
            $.post('/ajax/tuan/sku_intro.php', { 'faxian_pids': faxian_pids, 'them': them, 'action': action }, function (msg) {//
                $(th).html(msg);
                $("img").lazyload({});
            });
        }
    })

    ////mini 
    $(".mini_share_pids").each(function () {
        var share_ids = $(this).attr("data");
        if (!share_ids) { var share_ids = $(this).attr("id"); }
        var th = $(this);
        var them = "1";
        if (share_ids) {
            var action = "1";
            $(th).html("<div class='loader '></div>");
            $.post('/ajax/tuan/sku_intro.php', { 'share_ids': share_ids, 'them': them, 'action': action }, function (msg) {//
                $(th).html(msg);
                $("img").lazyload({});
            });
        }
    })

    ///
});