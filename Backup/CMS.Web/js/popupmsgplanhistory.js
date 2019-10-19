$(function () {
    
    //初始化数据
    self.parent.document.body.style.overflow = 'hidden';
    SetGroup();
    $('#PopupMsgPlanInfo').html('');
    BataSearchPopupMsgPlanInfo();

    $("#popupMsg").click(function () {
        $("#PopupMsgInfo").css("display", "block");
        $("#changepopup").html("<iframe src='popupadd.aspx?PopupType=add' width='798px'  frameborder='0' height='100%'></iframe>");
        
    });

    $("#closepic").click(function () {
        $("#PopupMsgInfo").css("display", "none");
        $('#PopupMsgPlanInfo').datagrid("reload");
    });




});



//绑定模糊查询数据
function BataSearchPopupMsgPlanInfo() {
    var bgitme='', endtime='';
    var values = "";
    $('#PopupMsgPlanInfo').datagrid({
        title: "弹窗消息留痕记录",
        height: self.parent.document.body.clientHeight - 150,
        nowrap: true, //设置为true，当数据长度超出列宽时将会自动截取。
        striped: true, //隔行变色
        collapsible: false,   //是否可折叠的,           
        idField: 'PlanId',
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
        url: '/AjaxPopupMsgPlan/SearchPopupMsgPlan.cspx',
        columns: [[
             { title: "<b>PlanId</b>", field: "PlanId", align: 'center', width: fillSize(0.05) },
             { title: "<b>开始时间</b>", field: "BeginTime", width: fillSize(0.05),
                 formatter: function (value) {
                     if (value != null) {
                         bgitme = ConertJsonTimeAndFormat(value, 'yyyy-MM-dd hh:mm:ss');
                     }
                     return bgitme;
                 }
             },
			 { title: "<b>结束时间</b>", field: "EndTime", width: fillSize(0.05),
			     formatter: function (value) {
			         if (value != null) {
			             endtime = ConertJsonTimeAndFormat(value, 'yyyy-MM-dd hh:mm:ss');
			         }
			         return endtime;
			     }
			 },
              { title: "<b>标题</b>", field: "Title", width: fillSize(0.1) },
			 { title: "<b>内容</b>", field: "Content", align: 'center', width: fillSize(0.15) },
              { title: "<b>接收对象</b>", field: "Receiver", align: 'center', width: fillSize(0.1),
                  formatter: function (value, row) {
                      if (row.ReceiverType == 0) {
                          value = "用户组：" + row.Receiver;
                      } else if (row.ReceiverType == 1) {
                          value = "用户：" + row.Receiver;
                      } else if (row.ReceiverType == 2) {
                          value = "CRM组：" + row.Receiver;
                      }
                      return value;
                  }

              },
             { title: "<b>计划接收人数</b>", field: "ReceiverCount", align: 'center', width: fillSize(0.075) },
             { title: "<b>实际接收人数</b>", field: "ViewCount", align: 'center', width: fillSize(0.075) },
              { title: "<b>点击链接用户数</b>", field: "ClickCount", align: 'center', width: fillSize(0.05) },
              { title: "<b>发送版本</b>", field: "PushVersion", align: 'center', width: fillSize(0.05) },
              { title: "<b>平台</b>", field: "PushPlatform", align: 'center', width: fillSize(0.05) },
             { title: "<b>操作人</b>", field: "Editor", align: 'center', width: fillSize(0.05) },
              { title: "<b>状态</b>", field: "Status", align: 'center', width: fillSize(0.05),
                  formatter: function (value) {
                      if (value == 3) {
                          return values = "已取消发送";
                      } else if (value == 4) {
                          return values = "发送中被中止";
                      } else {
                          var now = new Date();

                          if (now - (Date.parse(bgitme.replace(/-/g, "/"))) < 0) {
                              return values = "待发送";
                          } else if (now - (Date.parse(endtime.replace(/-/g, "/"))) > 0) {
                              return values = "发送完成";
                          } else {
                              return values = "正在发送";
                          }
                      }
                  }
              },
               { title: "<b>数据类型</b>", field: "DataType", align: 'center', width: fillSize(0.05)},
            { title: "<b>操作</b>", field: "xx1", align: 'center', width: fillSize(0.1),
                formatter: function (value, row) {
                    var returnvalue = '<a  href="javascript:void(0);"  class="easyui-linkbutton" rowId=' + row.PlanId + ' rowId2=' + row.DataType + ' plain="true">查看</a>';
                    if (values == "待发送") {
                        returnvalue += '<a herf="javascript:void(0)" onclick=CancelPlan(' + row.PlanId + ')  class="easyui-linkbutton"  plain="true">取消</a>';
                    } else if (values == "正在发送") {
                        returnvalue += '<a herf="javascript:void(0)" onclick=StopPlan(' + row.PlanId + ') class="easyui-linkbutton"  plain="true">中止</a>';
                    }
                    return returnvalue;
                }
            }
		     ]],
            onLoadSuccess: function () {
                $($('#PopupMsgPlanInfo').datagrid("getPanel")).find('a.easyui-linkbutton').linkbutton()
                .filter("a[rowId]").click(lookpopup)
            
        }
    });
    }


 function CancelPlan(planId) {   
    $.ajax({
        url: "/AjaxPopupMsgPlan/CancelPlan.cspx?PlanId=" + planId,
        success: function (responseText) {
            if (responseText == "000000") {
                $.messager.alert(g_MsgBoxTitle, "操作成功!", "info", function () {
                    $('#PopupMsgPlanInfo').datagrid("reload");
                });
            }
            else {
                $.messager.alert(g_MsgBoxTitle, "操作失败!", "info", function () {
                    $('#PopupMsgPlanInfo').datagrid("reload");
                });
            }
        }
    });
}

function StopPlan(planId) {
    $.ajax({
        url: "/AjaxPopupMsgPlan/StopPlan.cspx?PlanId=" + planId,
        success: function (responseText) {
            if (responseText == "000000") {
                $.messager.alert(g_MsgBoxTitle, "操作成功!", "info", function () {
                    $('#PopupMsgPlanInfo').datagrid("reload");
                });
            }
            else {
                $.messager.alert(g_MsgBoxTitle, "操作失败!", "info", function () {
                    $('#PopupMsgPlanInfo').datagrid("reload");
                });
            }
        }
    });
}

function lookpopup() {
   var dom = this;
   var PlanId = $(this).attr("rowId");
   var datatype = $(this).attr("rowId2");
   $("#PopupMsgInfo").css("display", "block");
   if (datatype == "资讯弹窗") {
       $("#changepopup").html("<iframe src='popupzx.aspx?PopupType=edit&PlanId=" + PlanId + "&datatype=look&PopupMsgInfo=PopupMsgInfo&PopupMsgPlanInfo=PopupMsgPlanInfo' width='798px'  frameborder='0' height='100%'></iframe>");
   }else
     $("#changepopup").html("<iframe src='popupadd.aspx?PopupType=edit&PlanId=" + PlanId + "' width='798px'  frameborder='0' height='100%'></iframe>");
}

function SetGroup() {
    $.ajax({
        url: "/AjaxPopup/GetGroup.cspx",
        data: {},
        type: "POST",
        success: function (res) {
            var groups = res.split(",");
            var groupHtml = "";
            for (var i = 0; i < groups.length; i++) {
                groupHtml += "<li><label><input type=\"checkbox\" name=\"ReceiverGroup\" value=\"" + groups[i] + "\" /> " + groups[i] + "</label></li>";
            }
            $("#ulReceiverGroup").html(groupHtml);
        }
    });
}

 