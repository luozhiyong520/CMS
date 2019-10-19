$(function () {
    self.parent.document.body.style.overflow = 'hidden';
    //初始化数据
    $('#tblQueryResult').html('');
    $('#lbchannelID').html("");
    BataSearchFragment();
    //栏目名称改变事件
    $("#drpchannelID").change(function () {
        $('#lbchannelID').html("");
        var channelID = $("#drpchannelID option:selected").val();
        $('#lbchannelID').html(channelID);
    });
    //添加事件
    $("#add").click(function () {
        $("#OrderNum").val('');
        $("#Content").val('');
        $("#OrderNum").val('0');
        $("#tableHistory").css("display", "none");
        ShowEditItemDialog('', 'divbody', 1000, 630, Insertfragment);
        return false;
    });
    //恢复更多
    $("#showHistory").click(function () {
        var grid = $('#tblQueryResult');
        var channelId = $('#hdChannelId').val();
        var person = new Object();
        person.Content = $("#Content").val();
        var returnValue = window.showModalDialog("fragment_history.aspx?channelId=" + channelId, person, "dialogHeight=800px;dialogWidth=1000px;");
        if (returnValue != null && returnValue != "") {
            $("#Content").val(returnValue);
            grid.datagrid('reload');
        }
    });
    //恢复
    $("#backContent").click(function () {
        // 验证输入
        if (confirm('确定要恢复数据吗?')) {
            var grid = $('#tblQueryResult');
            var j_waitDialog = ShowWaitMessageDialog();
            if (ValidateForm()) {
                $("#Content").val($("#historyContent").val());
                var fragmentId = $("#hdfragmentId").val();
                var content = $("#historyContent").val();
                var channelID = $("#drpchannelID option:selected").val();
                var orderNum = $("#OrderNum").val();
                var content = $("#Content").val();
                var TypeId = $('#hidTypeId').val();
                var isDelete = '';
                if ($("#no").attr("checked") == "checked") {
                    $("#yes").attr("checked", "");
                    isDelete = $("#no").val();
                } else {
                    $("#no").attr("checked", "");
                    $("#yes").attr("checked", "checked");
                    isDelete = $("#yes").val();
                }
                $.ajax({
                    type: "POST",
                    url: "/AjaxFragment/UpdateFragmentBack.cspx",
                    data: { 'fragmentId': fragmentId, 'channelID': channelID, 'orderNum': orderNum, 'content': content, 'isDelete': isDelete, 'TypeId': TypeId },
                    complete: function () { HideWaitMessageDialog(j_waitDialog); },
                    success: function (responseText) {
                        $("#Content").val($("#historyContent").val());
                        grid.datagrid('reload');
                        //                                $.messager.alert(g_MsgBoxTitle, "恢复成功。", "info", function () {
                        //                                    // j_dialog.hide().dialog('close');
                        //                                    // grid.datagrid('reload');
                        //                                });
                    }
                });
            }
        }
    });

    //查询
    $("#btnSearch").click(function () {
        $('#tblQueryResult').html('');
        BataSearchFragment();
    });
    //获取ueditor编辑器的内容
    $("#btnContent").click(function () {
        var person = new Object();
        person.Content = $("#Content").val();
        var returnValue = window.showModalDialog("fckeidtor.aspx", person, "dialogHeight=500px;dialogWidth=830px;");
        if (returnValue != null && returnValue != "") {
            $("#Content").val(returnValue);
        }

    });
    $("table a.linkbutton[rowId]").click(Showfragment);
});
//绑定模糊查询数据
function BataSearchFragment() {
    var typeId = $("#hidTypeId").val();
    var searchdrpchannelID = $("#searchdrpchannelID option:selected").val();
    var searchContent = $("#searchContent").val();
    // var j_waitDialog = ShowWaitMessageDialog();
 
    $('#tblQueryResult').datagrid({
        title: "碎片列表",
        height: getHeight(),
        fitColumns: true,
        nowrap: true, //设置为true，当数据长度超出列宽时将会自动截取。
        striped: true, //隔行变色
        collapsible: false,   //是否可折叠的,           
        idField: 'FragmentId',
        pageSize: 30, //每页显示的记录条数，默认为10 
        pageNumber: 1,
        pageList: [10, 30, 50], //可以设置每页记录条数的列表  
        pagination: true, //设置true将在数据表格底部显示分页工具栏   
        rownumbers:true, //行号  
        collapsible: false,
        border: false, //显示面板的边界
        singleSelect: true, //是否单选  
        url: '/AjaxFragment/SearchFragment.cspx?typeId='+ typeId + '&channelId='+ searchdrpchannelID +'&keywords='+ encodeURIComponent(searchContent),
        columns: [[
             { title: "<b>栏目名称</b>", field: "ChannelName", width: fillSize(0.20) },
			 { title: "<b>碎片内容</b>", field: "Content", width: fillSize(0.45) },
			 { title: "<b>创建人</b>", field: "CreatedUser", align: 'center', width: fillSize(0.10) },
//             { title: "<b>是否删除</b>", field: "IsDeleted", align: 'center', width: fillSize(0.15),
//                 formatter: function (value, row) {
//                     return value == true ? "是" : "否";
//                 }
//             },
             { title: "<b>操作</b>", field: "xx", align:'center', width: fillSize(0.10),
                 formatter: function (value, row) {
                     return value = '<a href="javascript:void();"  class="easyui-linkbutton" rowId="' + row.FragmentId + '" channelId="' + row.ChannelId + '" plain="true"><img width="16"  height="16" border="0" src="/themes/default/images/admin/Item.Doc.Update.gif"></a>&nbsp;&nbsp;<a href="javascript:void();" linkurl="/AjaxFragment/DeleteFragment.cspx?fragmentId=' + row.FragmentId + '&returnUrl=/fragment/fragmentlist.aspx" title="删除" class="easyui-linkbutton" plain="true"><img width="16" height="16" border="0" src="/themes/default/images/admin/Item.Doc.Del.gif"></a>';
                 }
             }
		    ]],
        onLoadSuccess: function () {
            $($('#tblQueryResult').datagrid("getPanel")).find('a.easyui-linkbutton').linkbutton()
				.filter(g_deleteButtonFilter).click(DelelteFragment).end()
				.filter("a[rowId]").click(Showfragment);
        }
    });
 }
//新增碎片
 function Insertfragment(j_dialog) {
     var grid = $('#tblQueryResult');
    if (ValidateForm() == false) return;
    var channelID = $("#drpchannelID option:selected").val();
    var orderNum = $("#OrderNum").val();
    var content = $("#Content").val();
    var TypeId = $('#hidTypeId').val();
    var isDelete = $("input[type=radio][name=isDeleted]:checked").val();
    var j_waitDialog = ShowWaitMessageDialog();
    $.ajax({
        url: "/AjaxFragment/InsertFragment.cspx",
        type: "POST",
        data: { 'channelID': channelID, 'orderNum': orderNum, 'content': content, 'isDelete': isDelete, 'TypeId': TypeId },
        complete: function () { HideWaitMessageDialog(j_waitDialog); },
        success: function (responseText) {
            $.messager.alert(g_MsgBoxTitle, "操作成功。", "info", function () {
                j_dialog.hide().dialog('close');
                grid.datagrid('reload');
            });
        }
    });
}
//显示碎片信息
function Showfragment() {
    var dom = this;
    var fragmentId = $(this).attr("rowId");
    var channelId = $(this).attr("channelId");
    $('#hdChannelId').val(channelId);
    $('#lbchannelID').html(channelId);
    var grid = $('#tblQueryResult');
    $("#tableHistory").css("display", "block");
 
    // 首先获取指定的碎片信息
    $.ajax({
        url: "/AjaxFragment/GetFragmentById.cspx?fragmentId=" + fragmentId, dataType: "json",
        success: function (json) {
            $("#OrderNum").val(json.OrderNum);
            $("#drpchannelID").val(json.ChannelId);
            $("#Content").val(json.Content);
            $.ajax({
                url: "/AjaxFragment/GetFragmentHistory.cspx?channelId=" + json.ChannelId, dataType: "json",
                success: function (jsonhistory) {
                    $("#historyContent").val(jsonhistory.Content);
                }
            });

            if (json.IsDeleted == true) {
                $("#no").attr("checked", "");
                $("#yes").attr("checked", "checked");
            } else {
                $("#yes").attr("checked", "");
                $("#no").attr("checked", "checked");
            }

            $("#hdfragmentId").val(fragmentId);
            // 显示编辑对话框
            ShowEditItemDialog(fragmentId, 'divbody', 900, 600, function (j_dialog) {
                // 验证输入
                if (ValidateForm()) {
                    var j_waitDialog = ShowWaitMessageDialog();
                    var channelID = $("#drpchannelID option:selected").val();
                    var orderNum = $("#OrderNum").val();
                    var content = $("#Content").val();
                    var TypeId = $('#hidTypeId').val();
                    var isDelete = '';
                    if ($("#no").attr("checked") == "checked") {
                        $("#yes").attr("checked", "");
                        isDelete = $("#no").val();
                    } else {
                        $("#no").attr("checked", "");
                        $("#yes").attr("checked", "checked");
                        isDelete = $("#yes").val();
                    }
                 
                    $.ajax({
                        type: "POST",
                        url: "/AjaxFragment/UpdateFragment.cspx",
                        data: { 'channelID': channelID, 'orderNum': orderNum,'content':content,'isDelete': isDelete, 'TypeId': TypeId },
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

//删除碎片信息
function DelelteFragment() {
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
    if (ValidateControl("#drpchannelID", "栏目名称 不能为空。") == false) return false;
    if (ValidateControl("#Content", "碎片内容名称 不能为空。") == false) return false;
    if (ValidateControl("#OrderNum", "排序值 不能为空。") == false) return false;
    return true;
}
