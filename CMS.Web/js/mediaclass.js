$(function () {
    $("#addMediaClass").click(function () {
        $("#MediaClassName").val("");
        ShowEditItemDialog('', 'MediaClass', 500, 200, InsertMedia);
        return false;
    });

});


function editMediaClass(MediaClassId, MediaClassName, SortId) {
 
    $("#MediaClassName").val(MediaClassName);
    $("#Sort").val(SortId);

    // 显示编辑对话框
    ShowEditItemDialog(MediaClassName, 'MediaClass', 500, 200, function (j_dialog) {
        var j_waitDialog = ShowWaitMessageDialog();
        var mediaClassName = $("#MediaClassName").val();
        var sort = $("#Sort").val();
        if (isNaN(sort)) { alert('排序填写不正确，请填数字'); return false; }
     
        $.ajax({
            type: "GET",
            url: "/AjaxMedia/UpdateMediaClass.cspx",
            data: $.param({ mediaclassid: MediaClassId }) + "&" + $.param({ mediaclassname: encodeURIComponent(mediaClassName) }) + "&" + $.param({ sort: sort }),
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

function InsertMedia(j_dialog) {
    var mediaClassName = $("#MediaClassName").val();
    var sort = $("#Sort").val();
    if (mediaClassName == '') { alert('文件名不能为空'); return false; }
    if (isNaN(sort)) { alert('排序填写不正确，请填数字'); return false; }
    var j_waitDialog = ShowWaitMessageDialog();
 
    $.ajax({
        url: "/AjaxMedia/AddMediaClass.cspx?mediaclassname=" + encodeURIComponent(mediaClassName) + "&sort=" + sort + "",
        type: "POST",
        complete: function () { HideWaitMessageDialog(j_waitDialog); },
        success: function (responseText) {
            if (responseText == "000000") {
                $.messager.alert(g_MsgBoxTitle, "文件名不能为空。", "info", function () {
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
function delMediaClass(MediaClassId, MediaClassName) {
    if (confirm('你确定删除类别' + MediaClassName + '吗？')) {
        var j_waitDialog = ShowWaitMessageDialog();
        $.ajax({
            url: "/AjaxMedia/DelMediaClass.cspx?mediaclassid=" + MediaClassId,
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
