$(function () {
    //分页列表
    $('#tblQueryResult').html('');
    BataSearchArticle();

});

//绑定模糊查询数据
function BataSearchArticle() {
    $('#tblQueryResult').datagrid({
        title: "SSO推送列表",
        height: self.parent.document.body.clientHeight - 100,
        fitColumns: true,
        nowrap: true, //设置为true，当数据长度超出列宽时将会自动截取。
        striped: true, //隔行变色
        collapsible: false,   //是否可折叠的,           
        idField: '',
        pageSize: 30, //每页显示的记录条数，默认为10 
        pageNumber: 1,
        pageList: [10, 30, 50], //可以设置每页记录条数的列表  
        pagination: true, //设置true将在数据表格底部显示分页工具栏   
        rownumbers: true, //行号  
        collapsible: false,
        border: false, //显示面板的边界
        singleSelect: true, //是否单选  
        type: "POST",
        url: '/AjaxSSOResult/SearchSSOResultList.cspx',
        columns: [[
             { title: "<b>PlanId</b>", field: "PlanId", align: 'center', width: fillSize(0.05) },
			 { title: "<b>成功用户</b>", field: "SuccessUser", align: 'center', width: fillSize(0.27),
			     formatter: function (value, row) {
			         var scount;
			         scount = row.SuccessUser;
			         return value = scount
			     }
			 },
             { title: "<b>失败用户</b>", field: "ErrorUser", align: 'center', width: fillSize(0.1),
                 formatter: function (value, row) {
                     var ecount;
                     ecount = row.ErrorUser;
                     return value = ecount;
                 }
             },
             { title: "<b>重复用户</b>", field: "RepeatUser", align: 'center', width: fillSize(0.1),
                 formatter: function (value, row) {
                     var rcount;
                     rcount = row.RepeatUser;
                     return value = rcount;
                 }
             },
             { title: "<b>不存在用户</b>", field: "UndefinedUser", align: 'center', width: fillSize(0.1),
                 formatter: function (value, row) {
                     var ucount;
                     ucount = row.UndefinedUser;
                     return value = ucount;
                 }
             },
             { title: "<b>开始时间</b>", field: "BeginTime", align: 'center', width: fillSize(0.1),
                 formatter: function (value, row) {
                     return value = ConertJsonTimeAndFormat(row.BeginTime, 'yyyy-MM-dd hh:mm:ss');
                 }
             },
             { title: "<b>结束时间</b>", field: "EndTime", align: 'center', width: fillSize(0.1),
                 formatter: function (value, row) {
                     return value = ConertJsonTimeAndFormat(row.EndTime, 'yyyy-MM-dd hh:mm:ss');
                 }
             },
             { title: "<b>弹窗类型</b>", field: "PopupType", align: 'center', width: fillSize(0.08),
                 formatter: function (value, row) {
                     if (row.PopupType == 0) {
                         value = "中间弹窗";
                     } else if (row.PopupType == 1) {
                         value = "右下角弹窗";
                     }
                     return value;
                 }
             },
             { title: "<b>状态</b>", field: "Status", align: 'center', width: fillSize(0.05),
                 formatter: function (value, row) {
                     if (row.Status == 0) {
                         value = "成功";
                     } else if (row.Status == 1) {
                         value = "该消息id 已存在";
                     } else if (row.Status == 2) {
                         value = "链接地址不存在";
                     } else if (row.Status == 3) {
                         value = "用户群组不存在";
                     } else if (row.Status == 4) {
                         value = "开始时间与结束时间不匹配";
                     } else if (row.Status == 9) {
                         value = "其它异常";
                     } else if (row.Status == 5) {
                         value = "开始时间与结束时间不匹配";
                     }
                     return value;
                 }
             },
                     { title: "<b>操作</b>", field: "xx", align: 'center', width: fillSize(0.05),
                         formatter: function (value, row) {
                             var now = new Date();
                       
                             if (row.ErrorUser != "" && Date.parse(row.EndTime.replace(/-/g, "/")) > now) {
                                 return value = '<a href="javascript:void(0)" onclick="ReSsoPush(' + row.Id + ')"  class="easyui-linkbutton" plain="true">重新推送</a>';
                             }
                         }
                     }
		    ]],
        onLoadSuccess: function () {
            $($('#tblQueryResult').datagrid("getPanel")).find('a.easyui-linkbutton').linkbutton()
        }
    });
}

function ReSsoPush(Id) {
    $.ajax({
        url: "/AjaxSSOResult/ReSsoPush.cspx?Id=" + Id,
        success: function (responseText) {
                $.messager.alert(g_MsgBoxTitle, "操作成功!", "info", function () {
                    $('#tblQueryResult').datagrid("reload");
                });
            }
    });
}


 

 