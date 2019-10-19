$(function () {
    self.parent.document.body.style.overflow = 'hidden';
    //初始化数据
    $('#tblQueryResult').html('');
    BataSearchCollectSiteFolder();
    //添加事件
    $("#add").click(function () {
        $("#txtSiteFolderName").val('');
        //        $("#drpChannelId options:selected").val('');
        $.ajax({
            url: '/AjaxChannel/GetChannelNodeInit.cspx?typeID=1',
            success: function (shtml) {
                $("#drpChannelId").html('');
                $("#drpChannelId").append(shtml);
            }
        });
        $("#txtSiteFolderDetail").val('');
        ShowEditItemDialog('', 'divbody', 720, 380, InsertCollectSiteFolder);
        return false;
    });
    //查询
    $("#btnSearch").click(function () {
        $('#tblQueryResult').html('');
        BataSearchCollectSiteFolder();
    });
    $("table a.linkbutton[rowId]").click(ShowCollectSiteFolder);
});
//绑定模糊查询数据
function BataSearchCollectSiteFolder() {
    var searchSiteFolderName = $("#searchSiteName").val();
    // var j_waitDialog = ShowWaitMessageDialog();

    $('#tblQueryResult').datagrid({
        title: "采集栏目列表",
        height: getHeight(),
        fitColumns: true,
        nowrap: true, //设置为true，当数据长度超出列宽时将会自动截取。
        striped: true, //隔行变色
        collapsible: false,   //是否可折叠的,           
        idField: 'SiteFolderId',
        pageSize: 30, //每页显示的记录条数，默认为10 
        pageNumber: 1,
        pageList: [10, 30, 50], //可以设置每页记录条数的列表  
        pagination: true, //设置true将在数据表格底部显示分页工具栏   
        rownumbers: true, //行号  
        collapsible: false,
        border: false, //显示面板的边界
        singleSelect: true, //是否单选  
        url: '/AjaxCollectSiteFolder/SearchCollectSiteFolder.cspx?keywords=' + encodeURIComponent(searchSiteFolderName),
        columns: [[
             { title: "<b>采集栏目名称</b>", field: "SiteFolderName", width: fillSize(0.30) },
			 { title: "<b>采集栏目简介</b>", field: "SiteFolderDetail", width: fillSize(0.50) },
             { title: "<b>操作</b>", field: "xx", align: 'center', width: fillSize(0.20),
                 formatter: function (value, row) {
                     return value = '<a href="javascript:void();"  class="easyui-linkbutton" rowId="' + row.SiteFolderId + '" plain="true"><img width="16"  height="16" border="0" src="/themes/default/images/admin/Item.Doc.Update.gif"></a>&nbsp;&nbsp;<a href="javascript:void();" linkurl="/AjaxCollectSiteFolder/DeleteCollectSiteFolder.cspx?siteFolderId=' + row.SiteFolderId + 'title="删除" class="easyui-linkbutton" plain="true"><img width="16" height="16" border="0" src="/themes/default/images/admin/Item.Doc.Del.gif"></a>';
                 }
             }
		    ]],
        onLoadSuccess: function () {
            $($('#tblQueryResult').datagrid("getPanel")).find('a.easyui-linkbutton').linkbutton()
				.filter(g_deleteButtonFilter).click(DelelteCollectSiteFolder).end()
				.filter("a[rowId]").click(ShowCollectSiteFolder);
        }
    });
}
//新增采集栏目
function InsertCollectSiteFolder(j_dialog) {
    var grid = $('#tblQueryResult');
    if (ValidateForm() == false) return;
    var siteFolderName = $("#txtSiteFolderName").val();
    var siteFolderDetail = $("#txtSiteFolderDetail").val();

    var channelId = $("#drpChannelId option:selected").val();
    var j_waitDialog = ShowWaitMessageDialog();
    $.ajax({
        url: "/AjaxCollectSiteFolder/InsertCollectSiteFolder.cspx",
        type: "POST",
        data: { 'channelId': channelId, 'siteFolderName': siteFolderName, 'siteFolderDetail': siteFolderDetail },
        complete: function () { HideWaitMessageDialog(j_waitDialog); },
        success: function (responseText) {
            $.messager.alert(g_MsgBoxTitle, "操作成功。", "info", function () {
                j_dialog.hide().dialog('close');
                grid.datagrid('reload');
            });
        }
    });
}
//显示采集栏目信息
function ShowCollectSiteFolder() {
    var dom = this;
    var siteFolderId = $(this).attr("rowId");
    var grid = $('#tblQueryResult');
    $("#tableHistory").css("display", "block");
    // 首先获取指定的采集栏目信息
    $.ajax({
        url: "/AjaxCollectSiteFolder/GetCollectSiteFolderById.cspx?siteFolderId=" + siteFolderId, dataType: "json",
        success: function (json) {
            $("#txtSiteFolderName").val(json.SiteFolderName);
            $("#txtSiteFolderDetail").val(json.SiteFolderDetail);
            $.ajax({
                url: '/AjaxChannel/GetChannelNodeInit.cspx?typeID=1',
                success: function (shtml) {
                    $("#drpChannelId").html('');
                    $("#drpChannelId").append(shtml);
                    $("#drpChannelId").val(json.ChannelId);
                }
            });
            // 显示编辑对话框
            ShowEditItemDialog(siteFolderId, 'divbody', 730, 380, function (j_dialog) {
                // 验证输入
                if (ValidateForm()) {
                    var j_waitDialog = ShowWaitMessageDialog();
                    var channelId = $("#drpChannelId option:selected").val();
                    var siteFolderName = $("#txtSiteFolderName").val();
                    var siteFolderDetail = $("#txtSiteFolderDetail").val();
                    $.ajax({
                        type: "POST",
                        url: "/AjaxCollectSiteFolder/UpdateCollectSiteFolder.cspx",
                        data: { 'siteFolderId': siteFolderId, 'channelId': channelId, 'siteFolderName': siteFolderName, 'siteFolderDetail': siteFolderDetail },
                        complete: function () { HideWaitMessageDialog(j_waitDialog); },
                        success: function (responseText) {
                            $.messager.alert(g_MsgBoxTitle, "操作成功。", "info", function () {
                                j_dialog.hide().dialog('close');
                                grid.datagrid('reload');
                            });
                        }
                    });
                }
            });
        }
    });

    return false;
}

//删除采集栏目信息
function DelelteCollectSiteFolder() {
    var grid = $('#tblQueryResult');
    if (confirm('确定要删除此记录吗？')) {
        $.ajax({
            url: $(this).attr("linkurl"),
            success: function (responseText) {
                if (responseText > 0) {
                    $.messager.alert(g_MsgBoxTitle, "删除记录成功!", "info", function () {
                        grid.datagrid('reload');
                    });

                }
                else {
                    $.messager.alert(g_MsgBoxTitle, "记录删除失败!", "info", function () {
                        grid.datagrid('reload');
                    });
                }
            }
        });
    }
    // 无论如何，都返回false
    return false;
}
//验证方法
function ValidateForm() {
    if (ValidateControl("#drpChannelId", "栏目名称 不能为空。") == false) return false;
    if (ValidateControl("#txtSiteFolderName", "采集栏目名称 不能为空。") == false) return false;
    return true;
}
