$(function () {
    //显示数据信息
    BataSearchActualsGoods();

    //添加事件
    $("#addActualsType").click(function () {
        $("#txtActualsCode").val('');
        $("#txtActualsName").val('');
        ShowEditItemDialog('', 'divbody', 560, 470, InsertActualsGoodsType);
        return false;
    });

    //查询
    $("#btnSearch").click(function () {
        //$("#pgActualsAssociate").html('');
        $('.datagrid-btable').html('');
        BataSearchActualsGoods();
    });
    $("table a.linkbutton[rowId]").click(BataSearchActualsGoods);
})

//显示数据信息
function BataSearchActualsGoods() {
    var actualsName = $('#txtserachActualsName').val();
    var stockName = $('#txtserachStockName').val();
    $('#pgActualsGoods').datagrid({
        title: "现货列表",
        height: getHeight(),
        fitColumns: true,
        nowrap: true, //设置为true，当数据长度超出列宽时将会自动截取。
        striped: true, //隔行变色
        collapsible: false,   //是否可折叠的,           
        idField: 'Id',
        pageSize: 30, //每页显示的记录条数，默认为10 
        pageNumber: 1,
        pageList: [10, 30, 50], //可以设置每页记录条数的列表  
        pagination: true, //设置true将在数据表格底部显示分页工具栏   
        rownumbers: true, //行号  
        collapsible: false,
        border: false, //显示面板的边界
        singleSelect: true, //是否单选 
        showPageList: false,
        fitColumns: true,
        url: 'AjaxActualsGoods/SelectActualsGoods.cspx?code=' + encodeURIComponent(actualsName) + '&name=' + encodeURIComponent(stockName),
        columns: [[
                { title: "<b>渤海现货代码</b>", field: "FCODE", width: fillSize(0.1) },
			    { title: "<b>渤海现货名称</b>", field: "FNAME", width: fillSize(0.2) },

                { title: "<b>操作</b>", field: "xx", align: 'center', width: fillSize(0.10),
                    formatter: function (value, row) {
                        return value = '<a href="javascript:void();"  class="easyui-linkbutton" rowId="" plain="true"><img width="16"  height="16" border="0" src="/themes/default/images/admin/Item.Doc.Update.gif"></a>&nbsp;&nbsp;<a href="javascript:void();" linkurl="/AjaxActualsGoods/DeleteActualsGoods.cspx?code=' + encodeURIComponent(actualsName) + '" title="删除" class="easyui-linkbutton" plain="true"><img width="16" height="16" border="0" src="/themes/default/images/admin/Item.Doc.Del.gif"></a>';
                    }
                }
		    ]],
        onLoadSuccess: function (ss) {
            $($('#pgActualsGoods').datagrid("getPanel")).find('a.easyui-linkbutton').linkbutton()
				.filter(g_deleteButtonFilter).click(DelelteActualsGoods).end()
				.filter("a[rowId]").click(ShowActualsGoods);
        }
    });
}



//插入现货
function InsertActualsGoodsType(j_dialog) {
    var grid = $('#pgActualsGoods');
    if (ValidateForm() == false) return;
    var actualsCode = $('#txtActualsCode').val();
    var actualsName = $('#txtActualsName').val();
    $.ajax({
        url: "/AjaxActualsGoods/AddActualsGoods.cspx",
        type: "POST",
        data: { 'code': actualsCode, 'name': actualsName },
        complete: function () { HideWaitMessageDialog(j_waitDialog); },
        success: function (responseText) {
            $.messager.alert(g_MsgBoxTitle, "操作成功。", "info", function () {
                j_dialog.hide().dialog('close');
                grid.datagrid('reload');
            });
        }
    });
}

//显示现货关联信息
function ShowActualsGoods() {
    var dom = this;
    //var Id = $(this).attr("rowId");
    var Id = $('#id').html();
    alert(Id);
    var grid = $('#pgActualsGoods');
    // 首先获取指定的现货关联信息
    $.ajax({
        url: "/AjaxActualsGoods/SelectActualsGoods.cspx?code=" + code,
        success: function (json) {
            $("#sltTypeId").val(json.TypeId);
            $("#txtActualsCode").val(json.ActualsCode);
            $("#txtActualsName").val(json.ActualsName);
            // 显示编辑对话框
            ShowEditItemDialog(Id, 'divbody', 560, 470, function (j_dialog) {
                // 验证输入
                if (ValidateForm()) {
                    var j_waitDialog = ShowWaitMessageDialog();
                    var actualsCode = $("#txtActualsCode").val();
                    var actualsName = $("#txtActualsName").val();
                    $.ajax({
                        type: "POST",
                        url: "/AjaxActualsGoods/updateActualsAssociate.cspx",
                        data: { 'Id': Id, 'actualsCode': actualsCode, 'actualsName': actualsName, 'typeId': typeId, 'exchange': exchange, 'stockCode': stockCode, 'stockName': stockName },
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

//删除现货信息
function DelelteActualsGoods() {
    var grid = $('#pgActualsGoods');
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
    if (ValidateControl("#txtActualsCode", "渤海现货代码 不能为空。") == false) return false;
    if (ValidateControl("#txtActualsName", "渤海现货名称 不能为空。") == false) return false;
    return true;
}