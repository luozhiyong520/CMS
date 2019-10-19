<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Page Language="C#" Inherits="PageView<AnalystLivePageModel>" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>直播列表</title>
     <!-- #include file="/controls/header.htm" -->  
</head>
<script language="javascript" type="text/javascript">
    $(function () {
        //分页列表
        $('#tblQueryResult').html('');

        BataSearchArticle();
    });

    //绑定模糊查询数据
    function BataSearchArticle() {

        $('#tblQueryResult').datagrid({
            title: "最新操盘",
            height: getHeight(),
            fitColumns: true,
            nowrap: true, //设置为true，当数据长度超出列宽时将会自动截取。
            striped: true, //隔行变色
            collapsible: false,   //是否可折叠的,           
            idField: 'NewsId',
            pageSize: 30, //每页显示的记录条数，默认为10 
            pageNumber: 1,
            pageList: [10, 30, 50], //可以设置每页记录条数的列表  
            pagination: true, //设置true将在数据表格底部显示分页工具栏   
            rownumbers: true, //行号  
            collapsible: false,
            border: false, //显示面板的边界
            singleSelect: true, //是否单选  
            type: "POST",
            url: '/AjaxAnalystLive/SearchAnalystLive.cspx?AnalystId='+<%=Model.AnalystId %>+"&AnalystType="+<%=Model.AnalystType%>,
            columns: [[
             { title: "<b>时间</b>", field: "CreateTime", align: 'center', width: fillSize(0.2),
                 formatter: function (value, row) {

                     if (row.CreateTime != "" && row.CreateTime != null) {
                         var UploadDate = row.CreateTime.replace("/Date(", "").replace(")/", "");
                         return value = getLocalTime(UploadDate.substr(0, 10));
                     } else {
                         return value = "";
                     }
                 }
             },
			 { title: "<b>品种</b>", field: "ActualName", align: 'left', width: fillSize(0.2) },
             { title: "<b>价格</b>", field: "TransPrice", align: 'left', width: fillSize(0.1) },
              { title: "<b>类型</b>", field: "TransType", align: 'left', width: fillSize(0.1) },
               { title: "<b>止盈价</b>", field: "StopProfit", align: 'left', width: fillSize(0.1) },
                { title: "<b>止损价</b>", field: "StopLoss", align: 'left', width: fillSize(0.1) },
                 { title: "<b>操作</b>", field: "IsSell", align: 'left', width: fillSize(0.2),
                     formatter: function (value, row) {
                         if (row.IsSell == 0) {
                             return value = '<a href="pcedit.aspx?LiveId=' + row.LiveId + '&AnalystId=<%=Model.AnalystId%>&AnalystName='+encodeURI('<%=Model.AnalystName%>')+'&AnalystType='+<%=Model.AnalystType%>+' "  class="easyui-linkbutton" plain="true">平仓</a>';
                         } else {
                             return value = '';
                         }
                     }
                 }
		    ]],
            onLoadSuccess: function () {
                $($('#tblQueryResult').datagrid("getPanel")).find('a.easyui-linkbutton').linkbutton()
			   .filter(g_deleteButtonFilter).click(CommonDeleteRecord).end()
            }
        });

    }
</script>
<body>
     <div class="con_p"> 

    <div class="cz_bk">
      <div class="lm_div">管理选项</div>
        <div class="cz_xx">
            <a href="zbadd.aspx?AnalystId=<%=Model.AnalystId%>&AnalystName=<%=Common.StringHelper.EncodeURI(Model.AnalystName)%>&AnalystType=<%=Model.AnalystType%>"  class='easyui-linkbutton l-btn l-btn-plain' plain="true" iconCls='icon-add' >添加直播</a> 
           | <a href="zblist.aspx?AnalystId=<%=Model.AnalystId%>&AnalystName=<%=Common.StringHelper.EncodeURI(Model.AnalystName)%>&AnalystType=<%=Model.AnalystType%>"  class='easyui-linkbutton l-btn l-btn-plain' plain="true" iconCls='icon-add'>直播列表</a> 
           | <a href="cprecord.aspx?AnalystId=<%=Model.AnalystId%>&AnalystName=<%=Common.StringHelper.EncodeURI(Model.AnalystName)%>&AnalystType=<%=Model.AnalystType%>"   class='easyui-linkbutton l-btn l-btn-plain' plain="true" iconCls='icon-add'>操盘记录</a> 
        <span style="color:Red">你现在操作的分析师：<%=Model.AnalystName%></span>
        </div>
    </div>

     <div class="cz_bk" style="position:relative">
      
         <table id="tblQueryResult"></table>

    </div>

    </div>
</body>
</html>
