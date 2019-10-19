<%@ Page Language="C#" Inherits="PageView<AnalystPageModel>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head >
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
           <!-- #include file="/controls/header.htm" -->  
 
</head>
<body>
<div class="con_p" style="padding-bottom:100px;">
     

      <div class="cz_bk">
                  <div class="lm_div">搜索信息</div>
                 <div class="cz_xx">
                     <span>直播类型：</span>
                     <select id="AnalysType" name="AnalysType" style="width:140px;">
                       <option value="0">--请选择--</option>
                       <option value="1">现货分析师</option>
                        <option value="2">贵金属分析师</option>
                       </select>
                      <%-- <span>Vip类型:</span><input type="text" id="txtlikeVipType"  maxlength="50" />--%>
                     <a href="#" id="btnSearch" class="easyui-linkbutton" iconCls="icon-find">搜索信息</a>
                  </div>
                  </div>
                     <div class="cz_bk">
      <div class="lm_div">分析师列表</div>
  <table cellpadding="0" cellspacing="0" class="c_table">
         <TR>
        <TH width="10%" nowrap>直播类型</TH>
        <TH width="20%" nowrap>分析师名称</TH>
         <TH width="20%" nowrap>分析师类型</TH>
          <TH width="20%" nowrap>软件版本</TH>
         <TH width="10%" nowrap>权值</TH>
           <TH width="30%" nowrap>操作</TH>
    </TR>
    
     <%foreach (Analyst analyst in Model.AnalystList)
    { %>
    <tr class="td_ys">
     <td width="10%" align=center><%=analyst.AnalystType==1?"现货分析师":analyst.AnalystType==2?"贵金属分析师":"其他"%></td>
      <td width="20%" align=center><%=analyst.AnalystName %></td>
       <td width="20%" align=center><%=analyst.VipType==1?"白金版"
                                        : analyst.VipType == 2 ? "钻石版"
                                        : analyst.VipType == 3 ? "至尊版"
                                        : analyst.VipType == 4 ? "银利阁"
                                        : analyst.VipType == 5 ? "淘金殿"
                                        : "普通版"%></td>
        <td width="20%" align=center><%=analyst.SoftVersion == 1 ? "金蝴蝶"
                                          : analyst.SoftVersion == 5001 ? "金牡丹"
                                          : "默认版"%></td>
      <td width="10%" align=center><%=analyst.AnalystSort %></td>
      <td width="30%" align=center>
       <a  href="javascript:void(0)" onclick="eidtSort('<%= analyst.AnalystId %>','<%=analyst.AnalystName%>','<%=analyst.AnalystSort%>')"><img width="16"  height="16" border="0" src="/themes/default/images/admin/Item.Doc.Update.gif"></a></td>
    </tr>
 <%} %> 
  
    
  </table>
   <div class="cz_bk">
         <table id="pgAnalyst"></table>
        </div>
  </div>
</div>
<div id="divAddItem" title="分析师权值" style="padding: 18px; display: none">
    <table cellpadding="0" border="0px">
     <tr>
            <td style="width:76px">分析师名称:</td>
	        <td><input name="AnalystName" type="text" maxlength="20" id="AnalystName" class="w300 inTextbox" readonly=readonly /></td>
        </tr>
        <tr>
            <td>权值:</td>
	        <td><input name="Sort"  id="Sort" class="w300 inTextbox"/></td>
        </tr>
        <br />
        <br />
        <tr ><td colspan=2>注：权值的数值范围为1-100的整数，数值越大排名靠前。</td></tr>
        </table>
</div>
<script type="text/javascript">

    $("#btnSearch").click(function () {
        var AnalysType = $('#AnalysType').val();
        AnalysType = AnalysType == 0 ? 2 : AnalysType;
        window.location = "/cpzb/analystrank.aspx?analystType=" + AnalysType;

    })
    function eidtSort(analystId, analystName, sort) {
        $("#AnalystName").val(analystName);
        $("#Sort").val($.trim(sort));
            // 显示编辑对话框
        ShowEditItemDialog(analystName, 'divAddItem', 500, 250, function (j_dialog) {
            var j_waitDialog = ShowWaitMessageDialog();
            var sort = $("#Sort").val();
            $.ajax({
                type: "POST",
                url: "/AjaxAnalyst/EditAnalystForSort.cspx",
                data: $.param({ analystId: analystId }) + "&" + $.param({ sort: sort }),
                complete: function () { HideWaitMessageDialog(j_waitDialog); },
                success: function (responseText) {
                    $.messager.alert(g_MsgBoxTitle, "操作成功。", "info", function () {
                        // 直接修改页面中的文字
                        window.location = window.location;
                    });
                }
            });
        });

        return false;
}
</script>
</body>
</html>
