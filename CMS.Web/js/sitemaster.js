$(function () {
    $.ajax({
        url: "/controls/pageheader.aspx",
        type: "GET",
        success: function (responseText) {
            if (responseText != null) {
                $("#pageheader").html(responseText);
            }
        }
    });
});

$(function () {
    $.ajax({
        url: "/controls/mainmenu.aspx",
        type: "GET",
        success: function (responseText) {
            if (responseText != null) {
                $("#left_menu").html(responseText);
            }
        }
    });
})