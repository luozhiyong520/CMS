$(function () {
    self.parent.document.body.style.overflow = 'hidden';
    //初始化数据
    $('#tblQueryResult').html('');
    BataSearchCollectSite();
    //添加事件
    $("#add").click(function () {
        window.open('collect_site_edit.aspx?siteId=', target = 'main');
    });
    //查询
    $("#btnSearch").click(function () {
        $('#tblQueryResult').html('');
        BataSearchCollectSite();
    });
    $("table a.linkbutton[rowId]").click(ShowCollectSite);
});
//绑定模糊查询数据
function BataSearchCollectSite() {
    var searchSiteName = $("#searchSiteName").val();
    // var j_waitDialog = ShowWaitMessageDialog();

    $('#tblQueryResult').datagrid({
        title: "采集站点列表",
        height: getHeight(),
        fitColumns: true,
        nowrap: true, //设置为true，当数据长度超出列宽时将会自动截取。
        striped: true, //隔行变色
        collapsible: false,   //是否可折叠的,           
        idField: 'SiteId',
        pageSize: 30, //每页显示的记录条数，默认为10 
        pageNumber: 1,
        pageList: [10, 30, 50], //可以设置每页记录条数的列表  
        pagination: true, //设置true将在数据表格底部显示分页工具栏   
        rownumbers: true, //行号  
        collapsible: false,
        border: false, //显示面板的边界
        singleSelect: true, //是否单选  
        url: '/AjaxCollectSite/SearchCollectSite.cspx?keywords=' + encodeURIComponent(searchSiteName),
        columns: [[
             { title: "<b>采集站点名称</b>", field: "SiteName", width: fillSize(0.40) },
			 { title: "<b>状态</b>", field: "LockState", width: fillSize(0.20) },
             { title: "<b>采集对象页</b>", field: "objURL", width: fillSize(0.20) },
             { title: "<b>操作</b>", field: "xx", align: 'center', width: fillSize(0.20),
                 formatter: function (value, row) {
                     var retrunvalue = '<a href="javascript:void();"  class="easyui-linkbutton" rowId="' + row.SiteId + '" plain="true">向导</a>&nbsp;┊&nbsp;<a href="javascript:void();" linkurl="/AjaxCollectSite/DeleteCollectSite.cspx?siteId=' + row.SiteId + 'title="删除" class="easyui-linkbutton" plain="true"><img width="16" height="16" border="0" src="/themes/default/images/admin/Item.Doc.Del.gif"></a>&nbsp;┊&nbsp;';
                     retrunvalue += '<a href="javascript:Collect("' + row.SiteId + '","' + row.LockState + '");"  class="easyui-linkbutton" plain="true">采集</a>';
                     return value = retrunvalue;
                 }
             }
		    ]],
        onLoadSuccess: function () {
            $($('#tblQueryResult').datagrid("getPanel")).find('a.easyui-linkbutton').linkbutton()
				.filter(g_deleteButtonFilter).click(DelelteCollectSite).end()
				.filter("a[rowId]").click(ShowCollectSite);
        }
    });
}
//显示采集站点信息
function ShowCollectSite() {
    var dom = this;
    var siteId = $(this).attr("rowId");
    window.open('collect_site_edit.aspx?siteId=' + siteId, target = 'main');
    return false;
}

//删除采集站点信息
function DelelteCollectSite() {
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
//
function Collect(id, flag) {
    if (flag != '有效') {
        alert('该站点参数没有设置完成,不能采集!\r请点击"向导"设置参数...');
        return;
    }

    var WWidth = (window.screen.width - 500) / 2;
    var Wheight = (window.screen.height - 150) / 2;
    document.getElementById('HidClNum').value = 0;
    //alert('Collect_NumSet.aspx?SiteId='+id);
    window.open('Collect_NumSet.aspx?SiteId=' + id, '采集设置', 'status=0,directories=0,resizable=0,top=' + Wheight + ', left=' + WWidth + ',toolbar=0,location=0,scrollbars=0,width=360px,height=165px');
}