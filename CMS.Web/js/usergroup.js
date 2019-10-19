$(function () {
    $("#addGroup").click(function () {
        $("#divAddItem :text").val("");
         $("#Remark :text").val("");
        ShowEditItemDialog('', 'divAddItem', 500, 200, InsertGroup);
        return false;
    });
  
});

function eidtGroup(groupId, groupName, remark) {

    $("#GroupName").val(groupName);
    $("#Remark").val($.trim(remark));
            // 显示编辑对话框
    ShowEditItemDialog(groupName, 'divAddItem', 500, 200, function (j_dialog) {
        var j_waitDialog = ShowWaitMessageDialog();
        var remark = $("#Remark").val();
        var groupName = $("#GroupName").val();
        $.ajax({
            type: "POST",
            url: "/AjaxUserManage/UpdateAdminGroup.cspx",
            data: $.param({ groupid: groupId }) + "&" + $.param({ groupname: groupName })+ "&" + $.param({ remark: remark }),
            complete: function () { HideWaitMessageDialog(j_waitDialog); },
            success: function (responseText) {
                $.messager.alert(g_MsgBoxTitle, "操作成功。", "info", function () {
                    // 直接修改页面中的文字
                    window.location = window.location;
                });
            }
        });
    });

        return false;
}

function InsertGroup(j_dialog) {
    var groupName = $("#GroupName").val();
    var reMark = $("#Remark").val();
    if (groupName == '') { alert('用户组名不能为空'); return false; }
    if (reMark == '') { alert('用户组描述不能为空'); return false; }
    var j_waitDialog = ShowWaitMessageDialog();
    $.ajax({
        url: "/AjaxUserManage/AddGroup.cspx?groupname=" + encodeURIComponent(groupName) + "&remark=" + encodeURIComponent(reMark) + "",
        type: "GET",
        complete: function () { HideWaitMessageDialog(j_waitDialog); },
        success: function (responseText) {
            if (responseText == "000000") {
                $.messager.alert(g_MsgBoxTitle, "用户组名不能为空。", "info", function () {
                    j_dialog.hide().dialog('close');
                    window.location = window.location;
                });

            } else {
                $.messager.alert(g_MsgBoxTitle, "操作成功。", "info", function () {
                    j_dialog.hide().dialog('close');
                    window.location = window.location;
                });
            }
        }
    });
}

//删除用户组
function delGroup(groupId,groupName) {
    if (confirm('你确定删除类别' + groupName + '吗？')) {
        var j_waitDialog = ShowWaitMessageDialog();
        $.ajax({
            url: "/AjaxUserManage/DelGroup.cspx?groupid=" + groupId,
            type: "GET",
            complete: function () { HideWaitMessageDialog(j_waitDialog); },
            success: function (responseText) {
                $.messager.alert(g_MsgBoxTitle, "删除成功!", "info", function () {
                    window.location = window.location;
                });
            }
        })
    }
}
 


