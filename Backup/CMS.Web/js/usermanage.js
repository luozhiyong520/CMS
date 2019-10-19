$(function () {
    self.parent.document.body.style.overflow = 'hidden';
    $("#AddUser").click(function () {
        $("#AddEditUser :text").val("");
        ShowEditItemDialog('', 'AddEditUser', 500, 200, InsertUser);
        return false;
    });
});

 
function editAdmin(adminId,adminName,groupId){
    $("#adminname").val(adminName);
    $("#groupid option[value=" + groupId + "]").attr("selected", "selected");
    // 显示编辑对话框
    ShowEditItemDialog(adminName, 'AddEditUser', 500, 200, function (j_dialog) {
        var j_waitDialog = ShowWaitMessageDialog();
        var password = $("#password").val();
        var groupid = $("#groupid").val();
        var adminname = $("#adminname").val();
        if (password == "") {
            password = "null";
        }
        $.ajax({
            type: "GET",
            url: "/AjaxUserManage/UpdateAdministrator.cspx",
            data: $.param({ groupid: groupid }) + "&" + $.param({ adminid: adminId }) + "&" + $.param({ password: password }) + "&" + $.param({ adminname: adminname }),
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

function InsertUser(j_dialog) {
    var adminName = $("#adminname").val();
    var password = $("#password").val();
    var groupid = $("#groupid").val();
    if (adminName == '') { alert('用户名不能为空'); return false; }
    if (password == '') { alert('用户密码不能为空'); return false; }
    if (groupid == '') { alert('用户密码不能为空'); return false; }
    var j_waitDialog = ShowWaitMessageDialog();

    $.ajax({
        url: "/AjaxUserManage/AddAdministrator.cspx?adminname=" + encodeURIComponent(adminName) + "&password=" + encodeURIComponent(password) + "&groupid=" + groupid,
        type: "GET",
        complete: function () { HideWaitMessageDialog(j_waitDialog); },
        success: function (responseText) {
            if (responseText == "000000") {
                $.messager.alert(g_MsgBoxTitle, "用户名不能为空!", "info", function () {
                    j_dialog.hide().dialog('close');
                    window.location = window.location;
                });

            } else if (responseText == "000001") {
                $.messager.alert(g_MsgBoxTitle, "用户密码不能为空!", "info", function () {
                    j_dialog.hide().dialog('close');
                    window.location = window.location;
                });

            }

            else {
                $.messager.alert(g_MsgBoxTitle, "操作成功!", "info", function () {
                    j_dialog.hide().dialog('close');
                    window.location = window.location;
                });
            }
        }
    });
}

function delAdminuser(adminid, adminname) {
    if (confirm('你确定删除类别' + adminname + '吗？')) {
        var j_waitDialog = ShowWaitMessageDialog();
        $.ajax({
            url: "/AjaxUserManage/DelAdministrator.cspx?adminid=" + adminid,
           type:"GET",
           complete: function () { HideWaitMessageDialog(j_waitDialog); },
           success: function (responseText) {        
                   $.messager.alert(g_MsgBoxTitle, "删除成功!", "info", function () {
                        window.location = window.location;
                   });   
           }
       })
    }
    }

    function EditAuthorityDot(adminId, edit) {
        //显示权限
        $.ajax({
            type: "GET",
            url: "/AjaxAuthorityDot/ShwowAuthorityDotAdmin.cspx",
            data: { "adminId": adminId },
            success: function (responseText) {
                if (responseText.length == 0) {
                    $(":checkbox").removeAttr("checked");
                } else {
                    for (var i = 0; i < responseText.length; i++) {
                        $("#" + responseText[i].AuthorityDotId).attr("checked", "checked");
                    }
                }
            }
        });
        // 显示编辑对话框
        ShowEditItemDialog(edit, 'EditAuthorityDot', 700, 400, function (j_dialog) {
            var j_waitDialog = ShowWaitMessageDialog();
            var checks = "";
//            var type = $("input[type='radio']:checked").val();
//            alert(type)
            var checkboxs = $("#EditAuthorityDot input:checked").each(function () {
                if (checks == "") {
                    checks += $(this).val();
                } else {
                    checks += "," + $(this).val();
                }
            })
            $.ajax({
                type: "GET",
                url: "/AjaxAuthorityDot/UpdateAuthorityDotAdmin.cspx",
                data: { "checks": checks, "adminId": adminId },
                complete: function () { HideWaitMessageDialog(j_waitDialog); },
                success: function (responseText) {
                    if (responseText != "") {
                        $.messager.alert(g_MsgBoxTitle, "功能点权限设置成功！", "info", function () {
                            // 直接修改页面中的文字
                            window.location = window.location;
                        });
                    } else {
                        $.messager.alert(g_MsgBoxTitle, "功能点权限设置失败！", "info", function () {
                            // 直接修改页面中的文字
                            window.location = window.location;
                        });
                    }
                }
            });
        });

        return false;
    }
 