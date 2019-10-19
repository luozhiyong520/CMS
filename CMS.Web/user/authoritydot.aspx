<%@ Page Language="C#" Inherits="PageView<AuthoritydotPageModel>" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>功能管理</title>
    <!-- #include file="/controls/header.htm" -->  
    <script type="text/javascript" src="/js/authoritydot.js"></script>
  
</head>

<body>
<div class="con_p">
    <div class="cz_bk">
      <div class="lm_div">管理选项</div>
        
        <div class="cz_xx">
            <a href="javascript:void(0)" id="addGroup" class='easyui-linkbutton l-btn l-btn-plain' plain="true" iconCls='icon-add' >添加功能点权限</a>
        </div>
    </div>
   

   <div class="cz_bk">
      <div class="lm_div">功能点权限列表</div>
  <table cellpadding="0" cellspacing="0" class="c_table">
        <tr>
        <th width="5%" nowrap>Id</th>
        <th width="20%" nowrap>功能点名</th>
        <th width="20%" nowrap>选项</th>
        <th width="5%" nowrap>状态</th>
        <th width="20%" nowrap>操作</th>
 
    </tr>
        <% foreach (AuthorityDot authorityDot in Model.AuthorityDots)
         { %>
    <tr class="td_ys">
      <td width="5%" align="center"><%=authorityDot.Id%></td>
      <td width="20%" align="center"><%=authorityDot.Text%></td>
      <td width="20%" align="center"><%=authorityDot.Options%></td>
      <td width="5%" align="center"><%=authorityDot.Status == true ? "启用" : "停用"%></td>
      <td width="20%" align="center">  <a  href="javascript:void(0)" onclick="eidtGroup(<%= authorityDot.Id %>,'<%=authorityDot.Text%>')"><img width="16"  height="16" border="0" src="/themes/default/images/admin/Item.Doc.Update.gif"></a>
		 <a href="javascript:void(0)" onclick="DelAuthorityDot(<%=authorityDot.Id%>,'<%=authorityDot.Text%>')">
         <img width="16" height="16" border="0" src="/themes/default/images/admin/Item.Doc.Del.gif"></a>
         <%=authorityDot.Status == true ? "<a href=\"javascript:void(0)\" onclick=\"SetStatus(" + authorityDot.Id + ",0)\" class=\"easyui-linkbutton\" plain=\"true\">禁用</a>" : "<a href=\"javascript:void(0)\" onclick=\"SetStatus(" + authorityDot.Id + ",1)\" class=\"easyui-linkbutton\" plain=\"true\">启用</a>"%>
         </td>
    </tr>
  <%} %>
    
  </table>
  </div>
</div>


<div id="divAddItem" title="功能点权限" style="padding: 18px; display: none">
    <table cellpadding="0" border="0px">
        <tr>
            <td style="width:76px">功能点名:</td>
	        <td><input type="text" maxlength="25" id="AuthorityName" class="w300 inTextbox" /></td>
        </tr>
        <tr>
            <td>权限类型:</td>
            <td>多选:<input type="radio" name="type" value="2" id="yes" checked="checked" />单选:<input type="radio" id="no" name="type" value="1" /></td>
        </tr>
        <tr>
            <td valign="top">功能权限:</td>
            <td id="AuthorityDot">
                <li><input type="text" style="width:190px;" class="inTextbox" /> 关联Id:<input type="text" style="width:60px;" class="inTextbox" /><a style="float:right; padding:10px 0 0 10px;" href="javascript:void(0)" onclick="DelAuthorityDotLi(this)"> 删除 </a></li>
                <li><input type="text" style="width:190px;" class="inTextbox" /> 关联Id:<input type="text" style="width:60px;" class="inTextbox" /><a style="float:right; padding:10px 0 0 10px;"  href="javascript:void(0)" onclick="DelAuthorityDotLi(this)"> 删除 </a></li>
            </td>
        </tr>
        <tr>
            <td></td>
            <td><a style="float:right; padding:10px 0 0 10px;" href="javascript:void(0)" onclick="AddAuthorityDotLi('','')"> 添加 </a></td>
        </tr>


    </table>
</div>

</body>
</html>
