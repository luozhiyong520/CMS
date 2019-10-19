$(function () {
    //初始化数据
    self.parent.document.body.style.overflow = 'hidden';
    $('#pgnewstitleprefix').html('');
    BataNewsTitlePrefix();
    //添加
    $("#addNewsTitlePrefix").click(function () {
        //重置
        $("#divbody").find("input:text").val("");
        ShowEditItemDialog('', 'divbody', 525, 200, function (j_dialog) { InsertNewsTitlePrefix(j_dialog); });
        return false;
    });
    $("table a.linkbutton[rowId]").click(ShowNewsTitlePrefix);
});

//绑定模糊查询数据
function BataNewsTitlePrefix() {

    $('#pgnewstitleprefix').datagrid({
        title: "标题前标签列表",
        height: getHeight(),
        nowrap: true, //设置为true，当数据长度超出列宽时将会自动截取。
        striped: true, //隔行变色
        collapsible: false,   //是否可折叠的,           
        idField: 'Id',
        pageSize: 30, //每页显示的记录条数，默认为10 
        pageNumber: 1,
        pagination: true, //设置true将在数据表格底部显示分页工具栏   
        rownumbers: true, //行号  
        collapsible: false,
        border: false, //显示面板的边界
        singleSelect: true, //是否单选  
        pageList: [10, 30, 50], //可以设置每页记录条数的列表 
        showPageList: false,
        fitColumns: true,
        url: '/AjaxNewsTitlePrefix/SearchNewsTitlePrefix.cspx',
        columns: [[
             { title: "<b>标题前标签名称</b>", field: "Prefix", width: fillSize(0.8) },
             { title: "<b>操作</b>", field: "xx2", align: 'center', width: fillSize(0.2),
                 formatter: function (value, row) {
                     return value = '<a href="javascript:void();"  class="easyui-linkbutton" rowId="' + row.Id + '" plain="true"><img width="16"  height="16" border="0" src="/themes/default/images/admin/Item.Doc.Update.gif"></a>&nbsp;&nbsp;<a href="javascript:void();" linkurl="/AjaxNewsTitlePrefix/DeleteNewsTitlePrefix.cspx?Id=' + row.Id + '&returnUrl=/news/newstitleprefixlist.aspx" title="删除" class="easyui-linkbutton" plain="true"><img width="16" height="16" border="0" src="/themes/default/images/admin/Item.Doc.Del.gif"></a>';
                 }
             }
		    ]],
        onLoadSuccess: function () {
            $($('#pgnewstitleprefix').datagrid("getPanel")).find('a.easyui-linkbutton')
                 .filter(g_deleteButtonFilter).click(deleteTemplate).end()
                 .filter("a[rowId]").click(ShowNewsTitlePrefix).end();
        }
    });
}



//删除模板记录
function deleteTemplate() {
    var grid=$('#pgnewstitleprefix');
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

//添加方法
 function InsertNewsTitlePrefix(j_dialog) {
    if (ValidateForm() == false) return;
    var prefix = $("#txtPrefix").val();
    var j_waitDialog = ShowWaitMessageDialog();
    $.ajax({
        url: '/AjaxNewsTitlePrefix/InsertNewsTitlePrefix.cspx',
        data: { 'prefix': prefix },
        type: 'POST',
        complete: function () { HideWaitMessageDialog(j_waitDialog); },
        success: function () {
            $.messager.alert(g_MsgBoxTitle, "操作成功。", "info", function () {
                j_dialog.dialog('close');
                $('#pgnewstitleprefix').datagrid('reload');	            
            });   
         }
    })
}

//数据显示的方法
function ShowNewsTitlePrefix() {
    var dom = this;
    var Id = $(this).attr("rowId");
    $.ajax({
        url: '/AjaxNewsTitlePrefix/GetNewsTitlePrefixById.cspx?Id=' + Id,
        dataType: "json",
        success: function (json) {

            $("#txtPrefix").val(json.Prefix);
            // 显示编辑对话框
            ShowEditItemDialog(Id, 'divbody', 525, 200, function (j_dialog) {
                // 验证输入
                if (ValidateForm()) {
                    var j_waitDialog = ShowWaitMessageDialog();
                    var prefix = $("#txtPrefix").val();
                    $.ajax({
                        type: "POST",
                        url: "/AjaxNewsTitlePrefix/UpdateNewsTitlePrefix.cspx",
                        data: { 'Id':Id,'prefix': prefix
                        },
                        complete: function () { HideWaitMessageDialog(j_waitDialog); },
                        success: function (responseText) {
                            $.messager.alert(g_MsgBoxTitle, "操作成功。", "info", function () {
                                j_dialog.hide().dialog('close');
                                $('#pgnewstitleprefix').datagrid('reload');
                            });
                        }
                    });
                }
            });
        }
    });
}

//验证方法
function ValidateForm() {
    if (ValidateControl("#txtPrefix", "标题前标签名称 不能为空。") == false) return false;
    return true;
}
