$(function () {
    $("#createdgroup").click(function () {
        $("#AddEditPopUserGroup :text").val("");
        ShowEditItemDialog('', 'AddEditPopUserGroup', 500, 200, InsertUser);
    });

    $.ajax({
        url: '/AjaxAuthorityDot/GetAuthoritydot.cspx',
        success: function (shtml) {
            $("#RelevanceId").html('');
            $("#RelevanceId").append("<option value=\"请选择\">请选择</option>" + shtml);
        }
    });

    $("#checkgroup").click(function () {

        if ($("#GroupName").val() == "" || $("#GroupName").val() == null) {
            $.messager.alert(g_MsgBoxTitle, "用户组名不能为空!", "info", function () {
                return false;
            });
        } else {
            $("form").submit();
        }
    });
});


function InsertUser(j_dialog) {
    var groupName = $("#AddGroupName").val();
    var gcount = $("#GroupName option").length;
    if (groupName.replace(/[ ]/g, "") != "") {
        for (var i = 0; i < gcount; i++) {
            if (groupName == $("#GroupName").get(0).options[i].text) {
                $.messager.alert(g_MsgBoxTitle, "该用户组已存在!", "info", function () {
                });
                return false;
            }
        }
        $("#GroupName").append("<option selected>" + groupName + "</option>");
        j_dialog.hide().dialog('close');
        return true;
    } else {
        $.messager.alert(g_MsgBoxTitle, "用户组名不能为空!", "info", function () {
        });
    }
}