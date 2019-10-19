$(function () {
    self.parent.document.body.style.overflow = 'hidden';
    //初始化数据
    $('.datagrid-btable').html('');
    BataSearchActualsAssociate();
    //添加事件
    $("#addActualsAssociate").click(function () {
        $("#txtActualsCode").val('');
        $("#txtActualsName").val('');
        $("#txtExchange").val('');
        $("#txtStockCode").val('');
        $("#txtStockName").val('');
        ShowEditItemDialog('', 'divbody', 560, 470, InsertActualsAssociate);
        return false;
    });

    //查询
    $("#btnSearch").click(function () {
        //$("#pgActualsAssociate").html('');
        $('.datagrid-btable').html('');
        BataSearchActualsAssociate();
    });
    $("table a.linkbutton[rowId]").click(BataSearchActualsAssociate);
});

//绑定模糊查询数据
function BataSearchActualsAssociate() {
    var actualsName = $('#txtserachActualsName').val();
    var stockName = $('#txtserachStockName').val();
    var typeId = $('#drpTypeId option:selected').val();
    $('#pgActualsAssociate').datagrid({
        title: "现货关联列表",
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
        url: 'AjaxActualsAssociate/SearchActualsAssociate.cspx?actualsName=' + encodeURIComponent(actualsName) + '&stockName=' + encodeURIComponent(stockName) + '&typeId=' + typeId,
        columns: [[
             { title: "<b>渤海现货代码</b>", field: "ActualsCode", width: fillSize(0.1) },
			 { title: "<b>渤海现货名称</b>", field: "ActualsName", width: fillSize(0.2) },
             { title: "<b>关联品种代码</b>", field: "StockCode", width: fillSize(0.1) },
			 { title: "<b>关联品种名称</b>", field: "StockName", width: fillSize(0.2) },
             { title: "<b>关联品种类型</b>", field: "TypeId", width: fillSize(0.15),
                 formatter: function (value) {
                     var TypeVale = "";
                     if (value == 1) {
                         TypeVale = "A股";
                     } else if (value == 2) {
                         TypeVale = "国内期货";
                     } else if (value == 3) {
                         TypeVale = "外盘";
                     }
                     return TypeVale;
                 }
             },
			 { title: "<b>交易所</b>", field: "Exchange", width: fillSize(0.2) },
			 { title: "<b>创建人</b>", field: "CreatedUser", align: 'center', width: fillSize(0.10) },
             { title: "<b>创建时间</b>", field: "CreatedTime", align: 'center', width: fillSize(0.15),
                 formatter: function (value) {
                     return ConertJsonTimeAndFormat(value, 'yyyy-MM-dd hh:mm:ss')
                 }
             },
             { title: "<b>操作</b>", field: "xx", align: 'center', width: fillSize(0.10),
                 formatter: function (value, row) {
                     return value = '<a href="javascript:void();"  class="easyui-linkbutton" rowId="' + row.Id + '" plain="true"><img width="16"  height="16" border="0" src="/themes/default/images/admin/Item.Doc.Update.gif"></a>&nbsp;&nbsp;<a href="javascript:void();" linkurl="/AjaxActualsAssociate/DeleteActualsAssociate.cspx?Id=' + row.Id + '&returnUrl=/ActualsAssociate/ActualsAssociatelist.aspx" title="删除" class="easyui-linkbutton" plain="true"><img width="16" height="16" border="0" src="/themes/default/images/admin/Item.Doc.Del.gif"></a>';
                 }
             }
		    ]],
             onLoadSuccess: function (ss) {
            $($('#pgActualsAssociate').datagrid("getPanel")).find('a.easyui-linkbutton').linkbutton()
				.filter(g_deleteButtonFilter).click(DelelteActualsAssociate).end()
				.filter("a[rowId]").click(ShowActualsAssociate);
        }
    });
 }
//新增现货关联
 function InsertActualsAssociate(j_dialog) {
     var grid = $('#pgActualsAssociate');
     if (ValidateForm() == false) return;
     var actualsCode=$('#txtActualsCode').val();
     var actualsName = $('#txtActualsName').val();
     var typeId = $('#sltTypeId option:selected').val();
     var exchange = $('#txtExchange').val();
     var stockCode = $('#txtStockCode').val();
     var stockName = $('#txtStockName').val();
     if (typeId==2) {
         if (ValidateControl("#txtExchange", "交易所 不能为空。") == false) {
             HideWaitMessageDialog(j_waitDialog);
             return;
         }
         if (exchange.indexOf('郑州') < 0 && exchange.indexOf('上海') < 0 && exchange.indexOf('大连') < 0) {
             $.messager.alert(g_MsgBoxTitle, "必须是郑州、上海、大连交易所的其一个！", "warning");
             HideWaitMessageDialog(j_waitDialog);
             return;
         }
     }
    var j_waitDialog = ShowWaitMessageDialog();
    $.ajax({
        url: "/AjaxActualsAssociate/InsertActualsAssociate.cspx",
        type: "POST",
        data: {'actualsCode': actualsCode, 'actualsName': actualsName, 'typeId': typeId, 'exchange': exchange, 'stockCode': stockCode, 'stockName': stockName },
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
function ShowActualsAssociate() {
    var dom = this;
    var Id = $(this).attr("rowId");
    var grid = $('#pgActualsAssociate');
    // 首先获取指定的现货关联信息
    $.ajax({
        url: "/AjaxActualsAssociate/GetActualsAssociateById.cspx?Id=" + Id, dataType: "json",
        success: function (json) {
            $("#sltTypeId").val(json.TypeId);
            $("#txtActualsCode").val(json.ActualsCode);
            $("#txtActualsName").val(json.ActualsName);
            $('#txtExchange').val(json.Exchange);
            $('#txtStockCode').val(json.StockCode);
            $('#txtStockName').val(json.StockName);
            // 显示编辑对话框
            ShowEditItemDialog(Id, 'divbody', 560, 470, function (j_dialog) {
                // 验证输入
                if (ValidateForm()) {
                    var j_waitDialog = ShowWaitMessageDialog();
                    var typeId = $("#sltTypeId option:selected").val();
                    var actualsCode = $("#txtActualsCode").val();
                    var actualsName = $("#txtActualsName").val();
                    var exchange = $('#txtExchange').val();
                    var stockCode = $('#txtStockCode').val();
                    var stockName = $('#txtStockName').val();
                    if (typeId == 2) {
                        if (ValidateControl("#txtExchange", "交易所 不能为空。") == false) {
                            HideWaitMessageDialog(j_waitDialog);
                            return;
                        }
                        if (exchange.indexOf('郑州') < 0 && exchange.indexOf('上海') < 0 && exchange.indexOf('大连') < 0) {
                            $.messager.alert(g_MsgBoxTitle, "必须是郑州、上海、大连交易所的其一个！", "warning");
                            HideWaitMessageDialog(j_waitDialog);
                            return;
                        }
                    }
                    $.ajax({
                        type: "POST",
                        url: "/AjaxActualsAssociate/updateActualsAssociate.cspx",
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

//删除现货关联信息
function DelelteActualsAssociate() {
    var grid = $('#pgActualsAssociate');
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
    if (ValidateControl("#sltTypeId", "关联品种类型 不能为空。") == false) return false;
    if (ValidateControl("#txtStockCode", "关联品种代码 不能为空。") == false) return false;
    if (ValidateControl("#txtStockName", "关联品种名称 不能为空。") == false) return false;
    return true;
}
