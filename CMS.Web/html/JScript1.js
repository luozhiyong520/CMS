$(function () {
    $("#znzul li").mouseover(function () {
        $(".m-znz-con").hide();
        $("#znzul li").attr("class", "");
        $(this).attr("class", "cur");
        $("#div-" + $(this).attr("t")).show();
    });
    $(".m-nav ul li").click(function () {
        $(".m-nav ul li").attr("class", "");
        $(this).attr("class", "cur");
    });
})