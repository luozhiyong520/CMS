$(function () {
    self.parent.document.body.style.overflow = ''; //隐藏浏览器右侧的滚动条
    //显示频道
    $("#add").click(function () {
        $("#lbChannelId").html('');
        var typeID = $("#drpTypeID option:selected").val();
        $.ajax({
            url: '/AjaxChannel/GetChannelNodeInit.cspx?typeID=' + typeID,
            success: function (shtml) {
                $("#drpparentID").html('');
                $("#drpparentID").append("<option value='0'>一级频道</option>" + shtml);
            }
        });
        $("#drpTypeID").removeAttr('disabled');
        $("#txtChanneName").val("");
        $("#txtChannelEnName").val("");
        $("#txtUrl").val("");
        $("#hideChannelID").val("");
        ShowEditItemDialogDel('', 'divBody', 630, 300, InsertChannel, DelChannel);
        $("#del").css("display", "none");
        return false;
    });
    $("#drpTypeID").change(function () {
        $("#drpparentID").html('');
        var typeID = $("#drpTypeID option:selected").val();
        if (typeID == 2) {
            $("#lbmsg").html('');
            $("#redid").html('');
            $("#EnName").html('');
        } else {
            $("#lbmsg").html('(说明请以http://开头)');
            $("#redid").html('*');
            $("#EnName").html('*');
        }
        $.ajax({
            url: '/AjaxChannel/GetChannelNodeInit.cspx?typeID=' + typeID,
            success: function (shtml) {
                $("#drpparentID").append("<option value='0'>一级频道</option>" + shtml);
            }
        });
    })
    //加载树TypeId=1为普通栏目
    $("#ChannelTree").tree({
        url: '/AjaxChannel/GetChannelNodes.cspx?TypeId=1',
        onClick: function (node) {
            ShowChannelDialog(node.id, 1);
            $("#hideChannelID").val(node.id);
        }
    });
    //加载树TypeId=2为碎片栏目
    $("#ChannelTreeFragment").tree({
        url: '/AjaxChannel/GetChannelNodes.cspx?TypeId=2',
        onClick: function (node) {
            ShowChannelDialog(node.id, 2);
            $("#hideChannelID").val(node.id);
        }
    });
});

//新增频道
function InsertChannel(j_dialog) {
    if (ValidateForm() == false) return;
    var typeID = $("#drpTypeID option:selected").val();
    if (typeID == 1) {
        if (ValidateUrl() == false) return;
    }
    var j_waitDialog = ShowWaitMessageDialog();
    var channelName = $("#txtChanneName").val();
    var channelEnName = $("#txtChannelEnName").val();
    var parentID = $("#drpparentID option:selected").val();
    var url = $("#txtUrl").val();
    if (typeID == 1) {
        if (url.indexOf('http://') < 0) {
            alert('路径请以http://形式开头。');
            HideWaitMessageDialog(j_waitDialog);
            return;
        }
    }
    $("#drpTypeID").removeAttr('disabled');
    var status = $("input[type=radio][name=status]:checked").val();
    $.ajax({
        url: "/AjaxChannel/InsertChannel.cspx",
        type: "POST",
        data: { "channelName": channelName,
            "channelEnName": channelEnName,
            "parentID": parentID,
            "typeID": typeID,
            "url": url,
            "status": status
        },
        complete: function () { HideWaitMessageDialog(j_waitDialog); },
        success: function (responseText) {
            if (responseText != '-1') {
                $.messager.alert(g_MsgBoxTitle, "操作成功。", "info", function () {
                    window.location = window.location;
                });
            }
            else {
                alert('已经存在相同的英文名称，请重新输入！！！');
                return;
            }
        }
    });
}
//显示数据
function ShowChannelDialog(ID, TypeId) {
    var channelID = ID;
    var parms = 'channelID=' + channelID + '&TypeId=' + TypeId;
    $.ajax({
        url: "/AjaxChannel/getChannelByID.cspx?" + parms, type: "POST",
        success: function (json) {
            var json = eval(json);
            $("#drpparentID").html('');
            $("#drpparentID").append("<option value='0'>一级频道</option>" + json.Shtml);
            $("#txtChanneName").val(json.ChannelName);
            $("#txtChannelEnName").val(json.ChannelEnName);
            $("#drpparentID").val(json.ParentId);
            $("#drpTypeID").val(json.TypeId);
            $("#lbChannelId").html(channelID);
            if (json.TypeId == 2) {
                $("#lbmsg").html('');
                $("#redid").html('');
                $("#EnName").html('');
            } else {
                $("#lbmsg").html('(说明请以http://开头)');
                $("#redid").html('*');
                $("#EnName").html('*');
            }
            $("#txtUrl").val(json.Url);
            if (json.Status == true) {
                $("#no").attr("checked", "");
                $("#yes").attr("checked", "checked");
            } else {

                $("#yes").attr("checked", "");
                $("#no").attr("checked", "checked");

            }
            $("#drpTypeID").attr('disabled', 'true');
            ShowEditItemDialogDel(channelID, 'divBody', 630, 300, function (j_dialog) {
                var dom = this;
                if (ValidateForm() == false) return;

                var j_waitDialog = ShowWaitMessageDialog();
                var channelName = $("#txtChanneName").val();
                var channelEnName = $("#txtChannelEnName").val();
                var parentID = $("#drpparentID option:selected").val();
                var typeID = $("#drpTypeID option:selected").val();
                var url = $("#txtUrl").val();
                if (typeID == 1) {
                    if (ValidateUrl() == false) return;
                    if (url.indexOf('http://') < 0) {
                        alert('路径请以http://形式开头。');
                        HideWaitMessageDialog(j_waitDialog);
                        return;
                    }
                }
                var status = '';
                if ($("#no").attr("checked") == "checked") {
                    $("#yes").attr("checked", "");
                    status = $("#no").val();
                } else {
                    $("#no").attr("checked", "");
                    $("#yes").attr("checked", "checked");
                    status = $("#yes").val();
                }
                // var parms = 'channelID=' + channelID + '&channelName=' + encodeURIComponent(channelName) + '&channelEnName=' + encodeURIComponent(channelEnName) + '&parentID=' + parentID + '&typeID=' + typeID + '&url=' + url;
                // parms += '&status=' + status;
                $.ajax({
                    url: "/AjaxChannel/UpdateChannel.cspx",
                    type: "POST",
                    data: { "channelID": channelID, "channelName": channelName,
                        "channelEnName": channelEnName,
                        "parentID": parentID,
                        "typeID": typeID,
                        "url": url,
                        "status": status
                    },
                    complete: function () { HideWaitMessageDialog(j_waitDialog); },
                    success: function (responseText) {
                        if (responseText != '-1') {
                            $.messager.alert(g_MsgBoxTitle, "操作成功。", "info", function () {
                                window.location = window.location;
                            });
                        }
                        else {
                            alert('已经存在相同的英文名称，请重新输入！！！');
                            return;
                        }
                    }
                });

            }, DelChannel);
            return false;
        }
    });

}
function DelChannel() {
    $("#del").click(function () {
        var j_dialog = $("#divBody");
        if (confirm('你确定删这数据吗？') == false) {
            return;
        }
        var id = $("#hideChannelID").val();
        var parms = 'channelID=' + id;
        $.ajax({
            url: "/AjaxChannel/deleteChannelByID.cspx?" + parms, type: "POST",
            complete: function () { },
            success: function (responseText) {
                if (responseText != '还有子节点，不能直接删除！！！') {
                    $.messager.alert(g_MsgBoxTitle, "删除成功。", "info", function () {
                        window.location = window.location;
                    });
                } else {
                    $.messager.alert(g_MsgBoxTitle, responseText, "info", function () {
                        window.location = window.location;
                    });
                }
            }
        });
    });
}
function ValidateForm() {
    if (ValidateControl("#txtChanneName", "频道名称 不能为空。") == false) return false;
    if (ValidateControl("#drpparentID", "频道分类 不能为空。") == false) return false;
    if (ValidateControl("#drpTypeID", "所属类型 不能为空。") == false) return false;
    return true;
}
function ValidateUrl() {
    if (ValidateControl("#txtChannelEnName", "频道英文名称 不能为空。") == false) return false;
    if (ValidateControl("#txtUrl", "路径 不能为空。") == false) return false;
    return true;
}