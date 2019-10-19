$(function () {
    $("#addGroup").click(function () {
        $("#divAddItem :text").val("");
        $("#Remark :text").val("");
        ShowEditItemDialog('', 'divAddItem', 500, 350, InsertAuthorityDot);
        return false;
    });

});

function eidtGroup(Id, Text) {
    //绑定数据
    $("#AuthorityName").val(Text);
    $("#AuthorityName").attr("disabled", "disabled");
    var authorityDot = $("#AuthorityDot").find("li");
    $.ajax({
        type: "POST",
        url: "/AjaxAuthorityDot/ShowAjaxAuthorityDotById.cspx",
        data: { "Id": Id },
        success: function (responseText) {
            $("#AuthorityDot").html("");
            if (responseText.length == 0) {
                $("#AuthorityDot").html("");
            } else {
                for (var i = 0; i < responseText.length; i++) {
                    AddAuthorityDotLi(responseText[i].Text, responseText[i].RelevanceId, responseText[i].Id);
                    if (responseText[i].Type == 2) {
                        $("#no").attr("checked", "");
                        $("#yes").attr("checked", "checked");
                    } else {
                        $("#yes").attr("checked", "");
                        $("#no").attr("checked", "checked");
                    }
                }
            }
        }
    });
    // 显示编辑对话框
    ShowEditItemDialog(Text, 'divAddItem', 500, 350, function (j_dialog) {
        var j_waitDialog = ShowWaitMessageDialog();
        var authorityName = $("#AuthorityName").val();
        var type = $("input[name='type']:checked").val();
        var authorityDot = $("#AuthorityDot").find("li");
        var authorityDots = "";
        authorityDot.each(function () {
            if ($(this).find("input:first").val() != "") {
                if (authorityDots == "")
                    authorityDots = $(this).find("input:first").val() + "," + $(this).find("input:last").val() + "," + $(this).attr("id");
                else
                    authorityDots += ";" + $(this).find("input:first").val() + "," + $(this).find("input:last").val() + "," + $(this).attr("id");
            }
        })
        if (authorityName == '') { alert('功能点名不能为空'); return false; }
        if (authorityDots == '') { alert('功能权限不能为空'); return false; }

        $.ajax({
            type: "POST",
            url: "/AjaxAuthorityDot/UpdateAjaxAuthorityDotById.cspx?Id=" + Id + "&dots=" +authorityDots + "&type=" + type,
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

function InsertAuthorityDot(j_dialog) {
    var authorityName = $("#AuthorityName").val();
    var authorityDot = $("#AuthorityDot").find("li");
    var type = $("input[name='type']:checked").val()
    var authorityDots = "";
    authorityDot.each(function () {
        if ($(this).find("input:first").val() != "") {
            if (authorityDots == "")
                authorityDots = $(this).find("input:first").val() + "," + $(this).find("input:last").val();
            else
                authorityDots += ";" + $(this).find("input:first").val() + "," + $(this).find("input:last").val();
        }
    })
    if (authorityName == '') { alert('功能点名不能为空'); return false; }
    if (authorityDots == '') { alert('功能权限不能为空'); return false; }
    var j_waitDialog = ShowWaitMessageDialog();
    $.ajax({
        url: "/AjaxAuthorityDot/AddAuthority.cspx?dotName=" + authorityName + "&dot=" + authorityDots + "&type=" + type + "",
        type: "GET",
        complete: function () { HideWaitMessageDialog(j_waitDialog); },
        success: function (responseText) {
            if (responseText == 0) {
                $.messager.alert(g_MsgBoxTitle, "信息不全", "info", function () {
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
function DelAuthorityDot(authorityDotId, authorityDotName) {
    if (confirm('你确定删除类别' + authorityDotName + '吗？')) {
        var j_waitDialog = ShowWaitMessageDialog();
        $.ajax({
            url: "/AjaxAuthorityDot/DelAuthorityDot.cspx?authorityid=" + authorityDotId,
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

function AddAuthorityDotLi(a1, a2, id) {
    var authorityDotLi = "<li id=\"" + id + "\"><input type=\"text\" style=\"width:190px;\" class=\"inTextbox\" value=\"" + a1 + "\" /> 关联Id:<input type=\"text\" style=\"width:60px;\" class=\"inTextbox\" value=\"" + a2 + "\" /><a style=\"float:right; padding:10px 0 0 10px;\" id=\"" + id + "\"  href=\"javascript:void(0)\" onclick=\"DelAuthorityDotLi(this)\"> 删除 </a></li>";
    $("#AuthorityDot").append(authorityDotLi);
}

function DelAuthorityDotLi(o) {
    var Ids = $(o).attr("id");
    if (Ids !="undefined") {
        alert(Ids)
            $.ajax({
                url: '/AjaxAuthorityDot/DelAjaxAuthorityDotById.cspx?id=' + Ids,
                success: function (shtml) {
                    if (shtml > 0) {
                        $(o).parent().replaceWith("");
                        alert('删除成功！')
                    }
                }
            });
        } else {
            $(o).parent().replaceWith("");
        }
}

function SetStatus(id, status) {
    $.ajax({
        url: '/AjaxAuthorityDot/SetStatus.cspx?id=' + id + "&status=" + status,
        success: function (shtml) {
            window.location = window.location;
        }
    });
}