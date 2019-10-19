$(function () {
    $("#addYmsUser").click(function () {
        $("#divAddItem :text").val("");
        $("#UserName").attr("disabled", false);
        $("#PackageA").attr("disabled", false);
        $("#PackageB").attr("disabled", false);
        $("#PackageB").attr("checked", ""); 
        $("#PackageA").attr("checked", "checked");               
        $("#Capital").attr("disabled", true);
        $("#PosRatio").attr("disabled", true);
        $("#Capital").val("无需填写");
        $("#PosRatio").val("无需填写");
        ShowEditItemDialog('', 'divAddItem', 500, 350, InsertDsYmsUser);
        return false;
    });

});

function EidtDsYmsUser(UserName, Package, Capital, PosRatio) {
    //绑定数据
    $("#UserName").val(UserName);
    $("#UserName").attr("disabled", true);
    $("#ShowName").val("");
    $("#LastResults").val("");
    $("#Sort").val("");
    if (Package == "a") {
        $("#PackageB").attr("checked", "");
        $("#PackageA").attr("checked", "checked");
    }
    else {
        $("#PackageA").attr("checked", "");
        $("#PackageB").attr("checked", "checked");   
    }
    $("#PackageA").attr("disabled", true);
    $("#PackageB").attr("disabled", true);
    $("#Capital").val(Capital);
    $("#Capital").attr("disabled", true);
    $("#PosRatio").val(PosRatio);
    $("#PosRatio").attr("disabled", true);
    // 显示编辑对话框
    ShowEditItemDialog('1', 'divAddItem', 500, 350, function (j_dialog) {
        var j_waitDialog = ShowWaitMessageDialog();
        var showname = $("#ShowName").val();
        var lastresults = $("#LastResults").val();
        var sort = $("#Sort").val();
        $.ajax({
            url: "/cgds/updateyms.aspx?UserName=" + escape(UserName) + "&ShowName=" + escape(showname) + "&LastResults=" + escape(lastresults) + "&Sort=" + escape(sort),
            type: "GET",
            complete: function () { HideWaitMessageDialog(j_waitDialog); },
            success: function (response) {
                if (response != 0) {
                    $.messager.alert(g_MsgBoxTitle, "修改成功。", "info", function () {
                        // 直接修改页面中的文字
                        window.location = window.location;
                    });
                }
                else {
                    $.messager.alert(g_MsgBoxTitle, "修改失败。", "info", function () {
                        // 直接修改页面中的文字
                        window.location = window.location;
                    });
                }
            }
        });
    });

    return false;
}

function InsertDsYmsUser(j_dialog) {
    var UserName = $("#UserName").val();
    var ShowName = $("#ShowName").val();
    var LastResults = $("#LastResults").val();
    var Sort = $("#Sort").val();
    var Package = $("input[name='Package']:checked").val();
    var j_waitDialog = ShowWaitMessageDialog();
    $.ajax({
        url: "/cgds/insertyms.aspx?UserName=" + escape(UserName) + "&ShowName=" + escape(ShowName) + "&LastResults=" + escape(LastResults) + "&Sort=" + escape(Sort) + "&Package=" + escape(Package),
        type: "GET",
        complete: function () { HideWaitMessageDialog(j_waitDialog); },
        success: function (response) {
            if (response != 0) {
                $.messager.alert(g_MsgBoxTitle, "添加成功", "info", function () {
                    j_dialog.hide().dialog('close');
                    window.location = window.location;
                });

            } else {
                $.messager.alert(g_MsgBoxTitle, "添加失败。", "info", function () {
                    j_dialog.hide().dialog('close');
                    window.location = window.location;
                });
            }
        }
    });
}

//删除用户组
function DeleteDsYmsUser(UserName) {
    if (confirm('你确定删除用户' + UserName + '的记录吗？')) {
        var j_waitDialog = ShowWaitMessageDialog();
        $.ajax({
            url: "/cgds/deleteyms.aspx?UserName=" + escape(UserName),
            type: "GET",
            complete: function () { HideWaitMessageDialog(j_waitDialog); },
            success: function (response) {
                if (response != 0) {
                    $.messager.alert(g_MsgBoxTitle, "删除成功!", "info", function () {
                        window.location = window.location;
                    });
                }
                else {
                    $.messager.alert(g_MsgBoxTitle, "删除失败!", "info", function () {
                        window.location = window.location;
                    });
                }
            }
        })
    }
}