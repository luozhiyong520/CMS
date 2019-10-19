$(function () {
    $("#addpermission").click(function () {
        var pstr = $(".c_table :input").fieldSerialize();
        $.ajax({
            type: "GET",
            url: "/AjaxUserManage/UpdatePermission.cspx",
            data: { "key": pstr },
            success: function (responseText) {
                if (responseText == "000000") {
                    alert("权限设置成功");
                    window.location = window.location;
                }
            }
        });

    });
});



 