
//初始化树形
$(function () {
    var templateId = $("#hdtemplateId").val();
    var templeteType = $("#hdtempleteType").val();
    //配置树形
    var setting = {
        check: {
            enable: true,
            chkStyle: "checkbox",
            chkboxType: { "Y": "", "N": "" }
        },
        data: {
            simpleData: {
                enable: true
            }
        },
        async: {//异步加载节点数据
            enable: true,
            otherParam: { "templateId": templateId, "templeteType": templeteType },
            url: "/Ajaxtemplatedetail/GetzTreeData.cspx" //返回一个json对象
        },
        callback: {
            onClick: zTreeBeforeClick
        }

    };
    $.fn.zTree.init($("#xmtree"), setting);

    //保存勾选项
    $("#btnSave").click(function () {
        var channelIds = GetchannelIdList();
        var templateId = $("#hdtemplateId").val();
        var templeteType = $("#hdtempleteType").val();
        //alert('channelIds:' + channelIds + 'templateId:' + templateId)
        var j_waitDialog = ShowWaitMessageDialog();
        $.ajax({
            url: "/Ajaxtemplatedetail/IsertTemplatedetail.cspx",
            data: { 'templateId': templateId, 'channelIds': channelIds, 'templeteType': templeteType },
            type: "POST",
            complete: function () { HideWaitMessageDialog(j_waitDialog); },
            success: function (msg) {

                    $.messager.alert(g_MsgBoxTitle, "操作成功。", "info", function () {
                        window.location = window.location;
                    });
            }
        });
    });
});

//获取节点名被单击时输入框为勾选
function zTreeBeforeClick() {
    var treeObj = $.fn.zTree.getZTreeObj("xmtree");
    var nodes = treeObj.getSelectedNodes();
    for (var i = 0, l = nodes.length; i < l; i++) {
        treeObj.checkNode(nodes[i], true, true);
        // alert(nodes[i].id);
    }
}
//获取输入框被勾选 或 未勾选的节点集合
function GetchannelIdList() {
    var menuIdArray = '';
    var treeObj = $.fn.zTree.getZTreeObj("xmtree");
    var nodes = treeObj.getCheckedNodes(true);
    if (nodes.length > 0) {
        $.each(nodes, function (index) {
            if (nodes[index].id != null) {
                menuIdArray += nodes[index].id;
                menuIdArray += ',';
            }
        });
    }
    return menuIdArray;
}