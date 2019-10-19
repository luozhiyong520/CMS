<%@ Page Language="C#" AutoEventWireup="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>贵金属分析师列表</title>
     <!-- #include file="/controls/header.htm" -->
     <script type="text/javascript">
         $(function () {
             //初始化数据
             self.parent.document.body.style.overflow = 'hidden';
             BataDataList();
             //查询
         });

         //绑定查询数据
         function BataDataList() {
             $('#pgAnalyst').datagrid({
                 title: "贵金属分析师列表",
                 height: getHeight(),
                 nowrap: true, //设置为true，当数据长度超出列宽时将会自动截取。
                 striped: true, //隔行变色
                 collapsible: false,   //是否可折叠的,           
                 idField: 'AnalystId',
                 pageNumber: 1,
                 pagination: false, //设置true将在数据表格底部显示分页工具栏   
                 rownumbers: true, //行号  
                 collapsible: false,
                 border: false, //显示面板的边界
                 singleSelect: true, //是否单选  
                 showPageList: true,
                 fitColumns: true,
                 url: '/AjaxAnalyst/Analystlist.cspx?AnalystType=2',
                 columns: [[
             { title: "<b>分析师名称</b>", field: "AnalystName", align: 'center', width: fillSize(0.1) },
			 { title: "<b>粉丝数</b>", field: "FansNum", align: 'center', width: fillSize(0.05) },

              { title: "<b>公告</b>", field: "Notice", align: 'center', width: fillSize(0.15) },
			 { title: "<b>简介</b>", field: "Intro", align: 'center', width: fillSize(0.2) },
             { title: "<b>分析师类型</b>", field: "VipType", align: 'center', width: fillSize(0.075),
                 formatter: function (value, row) {
                     if (row.VipType == 1)
                         return value = "白金版";
                     else if (row.VipType == 2)
                         return value = "钻石版";
                     else if (row.VipType == 3)
                         return value = "至尊版";
                     else (row.VipType == 0)
                         return value = "普通版"

                 }
             },
             { title: "<b>软件版本</b>", field: "SoftVersion", align: 'center', width: fillSize(0.075),
                 formatter: function (value, row) {
                     if (row.SoftVersion == 1)
                         return value = "金蝴蝶";
                     else if (row.VipType == "50001")
                         return value = "金牡丹";
                     else (row.VipType == 0)
                         return value = "默认版"
                 }
             },
              { title: "<b>图片</b>", field: "ImgUrl", align: 'center', width: fillSize(0.1),
                  formatter: function (value, row) {
                      return "<img src=\"http://img.upchina.com" + value + "\" width=\"80px\" height=\"80px\" />";
                  }
              },
               { title: "<b>状态</b>", field: "AnalystStatus", align: 'center', width: fillSize(0.05),
                   formatter: function (value, row) {
                       if (row.AnalystStatus == 1) {
                           return value = "正常";
                       } else {
                           return value = "停用";
                       }

                   }
               },
             { title: "<b>操作</b>", field: "xx2", align: 'center', width: fillSize(0.2),
                 formatter: function (value, row) {
                     var productButtonHtml = '<a href="analystinfo.aspx?type=3&AnalystId=' + row.AnalystId + '&AnalystType=' + row.AnalystType + '"  class="easyui-linkbutton l-btn l-btn-plain" plain="true" iconCls="icon-edit">个人管理</a>&nbsp;&nbsp;';
                     return productButtonHtml + '<a  href="zbadd.aspx?AnalystId=' + row.AnalystId + '&AnalystName=' + encodeURI(row.AnalystName) + '&AnalystType=' + row.AnalystType + '"  class="easyui-linkbutton l-btn l-btn-plain" plain="true" iconCls="icon-edit">直播管理</a><a href="http://app.upchina.com/sasweb/xysidkdydnhensydn_cdhds.dyshg/dsfyewlrndsfpoidsfewlkdsnf.cxgdsf_hdsfnew_gz/analystlive/gjs/exclusive.html?AnalystId=' + row.AnalystId + '"   class="easyui-linkbutton l-btn l-btn-plain" plain="true" iconCls="icon-edit" target="_blank">我的个人页</a>&nbsp;&nbsp;';

                 }
             }
		    ]],
                 onLoadSuccess: function () {
                     $($('#pgAnalyst').datagrid("getPanel")).find('a.easyui-linkbutton');
                 }
             });
         }
     </script>
</head>
<body>
    <div class="con_p"> 
        <div class="cz_bk">
      <div class="lm_div">管理选项</div>
        <div class="cz_xx">
           <a href="gjsanalystlist.aspx" class='easyui-linkbutton l-btn l-btn-plain' plain="true" iconCls='icon-add'>贵金属分析师列表</a>
     
        </div>
    </div>

        <div class="cz_bk">
         <table id="pgAnalyst"></table>
        </div>
    </div>
</body>
</html>
