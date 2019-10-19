$(function () {
    self.parent.document.body.style.overflow = 'hidden';
    $("#addMenuModule").click(function () {
        $("#OneMenuModuleItem :text").val("");
        ShowEditItemDialog('', 'OneMenuModuleItem', 500, 200, InsertMenuModule);
        return false;
    });
});


function editMenuModule(moduleId,moduleName) {
            $("#modulename").val(moduleName);
            // 显示编辑对话框
            ShowEditItemDialog(moduleName, 'OneMenuModuleItem', 500, 200, function (j_dialog) {
                var j_waitDialog = ShowWaitMessageDialog();
                var modulename = $("#modulename").val();
                $.ajax({
                    type: "POST",
                    url: "/AjaxUserManage/UpdateMenuModule.cspx",
                    data: $.param({ moduleid: moduleId }) + "&" + $.param({ modulename: modulename }),
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


function editTwoMenuModule(moduleId) {
    $.ajax({
        url: "/AjaxUserManage/GetByTwoModuleId.cspx?moduleid=" + moduleId,
        success: function (json) {
            $("#moduletwoname").val(json.ModuleName);
            $("#targeturl").val(json.TargetUrl);
            var parentid = json.ParentId;
            // 显示编辑对话框
            ShowEditItemDialog(json.ModuleName, 'TwoMenuModuleItem', 500, 200, function (j_dialog) {
                var j_waitDialog = ShowWaitMessageDialog();
                var moduletwoname = $("#moduletwoname").val();
                var targeturl=$("#targeturl").val();
                $.ajax({
                    type: "POST",
                    url: "/AjaxUserManage/UpdateTwoMenuModule.cspx",
                    data: $.param({ moduleid: moduleId }) + "&" + $.param({ parentid: parentid }) + "&" + $.param({ modulename: moduletwoname }) +"&"+$.param({ targeturl: targeturl }) ,
                    complete: function () { HideWaitMessageDialog(j_waitDialog); },
                    success: function (responseText) {
                        $.messager.alert(g_MsgBoxTitle, "操作成功。", "info", function () {
                            // 直接修改页面中的文字
                            window.location = window.location;
                        });
                    }
                });
            });
        }
    });

    return false;
}
 
function InsertMenuModule(j_dialog) {
    if (ValidateControl("#modulename", "菜单名不能为空") == false) return false;
    var j_waitDialog = ShowWaitMessageDialog();
    var ModuleName = $("#modulename").val();
    $.ajax({
        url: "/AjaxUserManage/AddMenuModule.cspx?modulename=" + encodeURIComponent(ModuleName) + "",
        type: "GET",
        complete: function () { HideWaitMessageDialog(j_waitDialog); },
        success: function (responseText) {
            if (responseText == "000000") {
                $.messager.alert(g_MsgBoxTitle, "菜单名不能为空。", "info", function () {
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

function addTwoMenuModule(ModuleId) {
    $("#TwoMenuModuleItem :text").val("");
    ShowAddItemDialog('', 'TwoMenuModuleItem', 500, 200, InsertTwoMenuModule, ModuleId);
    return false;
}

function InsertTwoMenuModule(j_dialog, ModuleId) {
    var ModuleTwoName = $("#moduletwoname").val();
    var TargetUrl = $("#targeturl").val();
    if (ModuleId == '') { alert('父菜单不能为空'); return false; }
    if (ModuleTwoName == '') { alert('菜单名不能为空'); return false; }
    if (TargetUrl == '') { alert('目标地址不能为空'); return false; }
    var j_waitDialog = ShowWaitMessageDialog();

    $.ajax({
        url: "/AjaxUserManage/AddTwoMenuModule.cspx?moduletwoname=" + encodeURIComponent(ModuleTwoName) + "&targeturl=" + encodeURIComponent(TargetUrl) + "&moduleid=" + ModuleId + "",
        type: "GET",
        complete: function () { HideWaitMessageDialog(j_waitDialog); },
        success: function (responseText) {
            if (responseText == "000000") {
                $.messager.alert(g_MsgBoxTitle, "菜单名不能为空。", "info", function () {
                    window.location = window.location;
                });

            } else {
                $.messager.alert(g_MsgBoxTitle, "操作成功。", "info", function () {
                    window.location = window.location;
                });
            }
        }
    });
}


//删除一级菜单
function delOneModule(moduleid, modulename) {

    if (confirm('你确定删除类别' + modulename + '吗？')) {
        var j_waitDialog = ShowWaitMessageDialog();
        $.ajax({
            url: "/AjaxUserManage/DelOneModule.cspx?moduleid=" + moduleid,
            type: "GET",
            complete: function () { HideWaitMessageDialog(j_waitDialog); },
            success: function (responseText) {
                if (responseText == '000000') {
                    $.messager.alert(g_MsgBoxTitle, "删除失败,请先删除父菜单下的子菜单!", "info", function () {
                        window.location = window.location;
                    });
                }
                else {
                    $.messager.alert(g_MsgBoxTitle, "删除成功!", "info", function () {
                        window.location = window.location;
                    });
                }
            }
        })
    }
}

//删除二级菜单
function delTwoModule(moduleid, modulename) {

    if (confirm('你确定删除类别' + modulename + '吗？')) {
        var j_waitDialog = ShowWaitMessageDialog();
        $.ajax({
            url: "/AjaxUserManage/DelTwoModule.cspx?moduleid=" + moduleid,
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


 
 
