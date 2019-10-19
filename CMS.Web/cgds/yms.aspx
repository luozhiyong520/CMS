<%@ Page Language="C#" Inherits="PageView<DsYmsUserPageModel>" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>云秒杀用户记录管理</title>
    <!-- #include file="/controls/header.htm" -->  
    <script type="text/javascript" src="/js/yms.js"></script>
  
</head>

<body>
<div class="con_p">
    <div class="cz_bk">
      <div class="lm_div">管理选项</div>
        
        <div class="cz_xx">
            <a href="javascript:void(0)" id="addYmsUser" class='easyui-linkbutton l-btn l-btn-plain' plain="true" iconCls='icon-add' >添加云秒杀用户记录</a>
        </div>
    </div>
   

   <div class="cz_bk">
      <div class="lm_div">云秒杀用户记录列表</div>
  <table cellpadding="0" cellspacing="0" class="c_table">
        <tr>
        <th width="10%" nowrap>用户名</th>
        <th width="10%" nowrap>显示的名称</th>
        <th width="10%" nowrap>上个月的战绩</th>
        <th width="10%" nowrap>排名</th>
        <th width="10%" nowrap>套餐类型</th>
        <th width="10%" nowrap>总资产</th>
        <th width="10%" nowrap>仓位</th>
        <th width="10%" nowrap>操作</th>
    </tr>
        <% foreach (CMS.Model.Oracle.DsYmsUserModel dsYmsUser in Model.DsYmsUserList)
         { %>
    <tr class="td_ys">
        <td width="10%" align="center"><%=dsYmsUser.UserName%></td>
        <td width="10%" align="center"><%=dsYmsUser.ShowName%></td>
        <td width="10%" align="center"><%=dsYmsUser.LastResults%></td>
        <td width="10%" align="center"><%=dsYmsUser.Sort%></td>
        <td width="10%" align="center"><%=dsYmsUser.Package%></td>
        <td width="10%" align="center"><%=dsYmsUser.Capital%></td>
        <td width="10%" align="center"><%=dsYmsUser.PosRatio%></td>
        <td width="20%" align="center">
            <a href="javascript:void(0)" onclick="EidtDsYmsUser('<%=dsYmsUser.UserName%>','<%=dsYmsUser.Package%>','<%=dsYmsUser.Capital%>','<%=dsYmsUser.PosRatio%>')"><img width="16"  height="16" border="0" src="/themes/default/images/admin/Item.Doc.Update.gif"></a>  
		    <a href="javascript:void(0)" onclick=" DeleteDsYmsUser('<%=dsYmsUser.UserName%>')"><img width="16" height="16" border="0" src="/themes/default/images/admin/Item.Doc.Del.gif"></a>
        </td>
    </tr>
  <%} %>
    
  </table>
  </div>
</div>
<div id="divAddItem" title="云秒杀用户记录" style="padding: 18px; display: none">
    <table cellpadding="0" border="0px">
        <tr>
            <td style="width:76px">用户名:</td>
	        <td><input type="text" maxlength="25" id="UserName" class="w300 inTextbox" /></td>
        </tr>
        <tr>
            <td style="width:76px">显示的名称:</td>
	        <td><input type="text" maxlength="25" id="ShowName" class="w300 inTextbox" /></td>
        </tr>
        <tr>
            <td style="width:76px">上个月的战绩:</td>
	        <td><input type="text" maxlength="25" id="LastResults" class="w300 inTextbox" /></td>
        </tr>
        <tr>
            <td style="width:76px">排名:</td>
	        <td><input type="text" maxlength="25" id="Sort" class="w300 inTextbox" /></td>
        </tr>
        <tr>
            <td style="width:76px">套餐类型:</td>
	        <td>&nbsp;&nbsp;&nbsp;&nbsp;a:<input type="radio" id="PackageA" name="Package" value="a"/>&nbsp;&nbsp;&nbsp;&nbsp;b:<input type="radio" id="PackageB" name="Package" value="b"/></td>
        </tr>
        <tr>
            <td style="width:76px">总资产:</td>
	        <td><input type="text" maxlength="25" id="Capital" class="w300 inTextbox" /></td>
        </tr>
        <tr>
            <td style="width:76px">仓位:</td>
	        <td><input type="text" maxlength="25" id="PosRatio" class="w300 inTextbox" /></td>
        </tr>
    </table>
</div>
</body>
</html>
