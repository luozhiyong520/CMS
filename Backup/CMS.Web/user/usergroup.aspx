<%@ Page Language="C#" Inherits="PageView<UserManagePageModel>" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>功能管理</title>
    <!-- #include file="/controls/header.htm" -->  
    <script type="text/javascript" src="/js/usergroup.js"></script>
  
</head>

<body>
<div class="con_p">
    <div class="cz_bk">
      <div class="lm_div">管理选项</div>
        
        <div class="cz_xx">
            <a href="usergroup.aspx" class='easyui-linkbutton l-btn l-btn-plain' plain="true" iconCls='icon-edit' >用户组列表</a> | <a href="javascript:void(0)" id="addGroup" class='easyui-linkbutton l-btn l-btn-plain' plain="true" iconCls='icon-add' >添加用户组</a>
        </div>
    </div>
   

   <div class="cz_bk">
      <div class="lm_div">用户组列表</div>
  <table cellpadding="0" cellspacing="0" class="c_table">
        <TR>
        <TH width="10%" nowrap>用户组名</TH>
        <TH width="10%" nowrap>权限</TH>
        <TH width="20%" nowrap>组描述</TH>
        <TH width="20%" nowrap>操作</TH>
 
    </TR>
    <% foreach (AdminGroup adminGroup in Model.AdminGroupList)
         { %>
    <tr class="td_ys">
      <td width="10%" align="center"><%=adminGroup.GroupName %></td>
      <td width="10%" align="center"><a href="menupermission.aspx?ptype=Group&adminid=<%=adminGroup.GroupId %>">[分配]</a></td>
      <td width="20%" align="center"><%=adminGroup.Remark %></td>
      <td width="20%" align="center">  <a  href="javascript:void(0)" onclick="eidtGroup(<%= adminGroup.GroupId %>,'<%=adminGroup.GroupName%>','<%=adminGroup.Remark%>')"><img width="16"  height="16" border="0" src="/themes/default/images/admin/Item.Doc.Update.gif"></a>
		 <a href="javascript:void(0)" onclick="delGroup(<%=adminGroup.GroupId%>,'<%=adminGroup.GroupName%>')">
         <img width="16" height="16" border="0" src="/themes/default/images/admin/Item.Doc.Del.gif"></a></td>
    </tr>
  <%} %>
    
  </table>
  </div>
</div>


<div id="divAddItem" title="用户组" style="padding: 8px; display: none">
    <table cellpadding="0" border="0px">
        <tr>
            <td style="width:76px">用户组名:</td>
	        <td><input name="GroupName" type="text" maxlength="20" id="GroupName" class="w300 inTextbox" /></td>
        </tr>
        <tr>
            <td>用户组描述:</td>
	        <td><textarea name="Remark" style="height:60px;" id="Remark" class="w300 inTextbox"></textarea></td>
        </tr>


    </table>
</div>

</body>
</html>
