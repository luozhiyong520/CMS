
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Page Language="C#" Inherits="PageView<AnalystLivePageModel>" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
            title: "直播列表",
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
			 { title: "<b>内容</b>", field: "Info", align: 'left', width: fillSize(0.6),
			     formatter: function (value, row) {
			         var LiveType;
			         if (row.LiveType == 1) {
			             LiveType = "【开仓】";
			         } else if (row.LiveType == 2) {
			             LiveType = "【平仓】";
			         } else {
			             LiveType = "【观点】";
			         }
			         var info;
			         if (row.Info == null) {
			             info = "";
			         } else {
			             info = row.Info;
			         }
			         return value = "<span style=\"color:red\">" + LiveType + " &nbsp;&nbsp;&nbsp; " + row.ActualName + " &nbsp;&nbsp;&nbsp; " + row.TransPrice + " 元 &nbsp;&nbsp;&nbsp; " + row.TransType + " &nbsp;&nbsp;&nbsp; " + getLocalTime(row.CreateTime.replace("/Date(", "").replace(")/", "").substr(0, 10)) + " &nbsp;&nbsp;&nbsp; </span>" + info;
			     }

			 },
             { title: "<b>操作</b>", field: "xx", align: 'left', width: fillSize(0.2),
                 formatter: function (value, row) {
                     if (row.LiveType == 1 && row.IsSell == 0) {
                         return value = "<a  href=\"pcedit.aspx?LiveId=" + row.LiveId + "&AnalystId=<%=Model.AnalystId%>&AnalystName="+encodeURI('<%=Model.AnalystName%>')+"&AnalystType="+<%=Model.AnalystType%>+" \"  class=\"easyui-linkbutton\" plain=\"true\">" +
                               "平仓</a>&nbsp;&nbsp;<a  href=\"zbedit.aspx?LiveId=" + row.LiveId + "&AnalystId=<%=Model.AnalystId%>&AnalystName="+encodeURI('<%=Model.AnalystName%>')+"&AnalystType="+<%=Model.AnalystType%>+" \" class=\"easyui-linkbutton\" name=\"editzb\" rowId=" + row.LiveId + " plain=\"true\" style=\"display:none\" >" +
                               "修改</a>&nbsp;&nbsp;<a href=\"javascript:void(0)\" onclick=\"delInfo(" + row.LiveId + ",'" + row.TransType + "'," + row.BuyLiveId + ",<%=Model.AnalystId%>,'<%=Model.AnalystName%>',<%=Model.AnalystType%>)\"  class=\"easyui-linkbutton \" plain=\"true\"  name=\"delzb\" style=\"display:none\">" +
                               "删除</a>";
                     } else if(row.LiveType==3)
                     {
                      return value = "<a  href=\"zbedit.aspx?LiveId=" + row.LiveId + "&AnalystId=<%=Model.AnalystId%>&AnalystName="+encodeURI('<%=Model.AnalystName%>')+"&AnalystType="+<%=Model.AnalystType%>+" \" class=\"easyui-linkbutton\"  rowId=" + row.LiveId + " plain=\"true\">" +
                               "修改</a>&nbsp;&nbsp;<a href=\"javascript:void(0)\" onclick=\"delInfo(" + row.LiveId + ",'" + row.TransType + "'," + row.BuyLiveId + ",<%=Model.AnalystId%>,'<%=Model.AnalystName%>',<%=Model.AnalystType%>)\"  class=\"easyui-linkbutton\" plain=\"true\">" +
                               "删除</a>";
                     }
                     else{
                         return value = "<a  href=\"zbedit.aspx?LiveId=" + row.LiveId + "&AnalystId=<%=Model.AnalystId%>&AnalystName="+encodeURI('<%=Model.AnalystName%>')+"&AnalystType="+<%=Model.AnalystType%>+" \" class=\"easyui-linkbutton\" name=\"editzb\" rowId=" + row.LiveId + " plain=\"true\" style=\"display:none\">" +
                               "修改</a>&nbsp;&nbsp;<a href=\"javascript:void(0)\" onclick=\"delInfo(" + row.LiveId + ",'" + row.TransType + "'," + row.BuyLiveId + ",<%=Model.AnalystId%>,'<%=Model.AnalystName%>',<%=Model.AnalystType%>)\"  class=\"easyui-linkbutton\" plain=\"true\" name=\"delzb\" style=\"display:none\">" +
                               "删除</a>";
                     }
                 }

             }
		    ]],
            onLoadSuccess: function () {
                $($('#tblQueryResult').datagrid("getPanel")).find('a.easyui-linkbutton').linkbutton()
			   .filter(g_deleteButtonFilter).click(CommonDeleteRecord).end()
                   loadAuthority();
            }
        });
    }
    function delInfo(liveId, transType, buyLiveId,AnalystId,AnalystName,AnalystType) {
        if (confirm('你确定删除该直播？')) {
            $.ajax({
                type: "GET",
                url: "/AjaxAnalystLive/DelAnalystLive.cspx?liveId=" + liveId + "&transType=" + encodeURI(transType) + "&buyLiveId=" + buyLiveId+"&AnalystId="+AnalystId+"&AnalystName="+encodeURI(AnalystName)+"&AnalystType="+AnalystType,
                success: function (responseText) {
                    if (responseText == "000000") {
                        alert("删除成功！");
                        //$('#tblQueryResult').datagrid("reload");
                        window.location = window.location;
                    }
                }
            });
           
        }    
    }

    function loadAuthority() {
    $.ajax({
        url: '/AjaxAuthorityDot/GetAuthorityDotAdmin.cspx?parentId=100',
        type: 'POST',
        success: function (json) {
            for(var i=0;i<json.length; i++)
            {
                if (json[i].Text == "修改") {
                    $("a[name='editzb']").show();
                }
                if(json[i].Text == "删除") {
                  $("a[name='delzb']").show();
                  }
           }
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
