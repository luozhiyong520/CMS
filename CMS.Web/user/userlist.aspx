<%@ Page Language="C#" Inherits="PageView<UserManagePageModel>" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>功能管理</title>
    <!-- #include file="/controls/header.htm" -->  
    <script type="text/javascript" src="/js/usermanage.js"></script>
</head>

<body>
<div class="con_p" style="padding-bottom:100px;">
    <div class="cz_bk">
      <div class="lm_div">管理选项</div>
        <div class="cz_xx">
            <a href="userlist.aspx" class='easyui-linkbutton l-btn l-btn-plain' plain="true" iconCls='icon-edit' >用户列表</a> | <a href="javascript:void(0)" class='easyui-linkbutton l-btn l-btn-plain' plain="true" iconCls='icon-add'  id="AddUser">添加用户</a>
        </div>
    </div>
   

   <div class="cz_bk">
      <div class="lm_div">用户列表</div>
  <table cellpadding="0" cellspacing="0" class="c_table">
    <TR>
        <TH width="10%" nowrap>用户名</TH>
        <TH width="10%" nowrap>所属组</TH>
        <TH width="20%" nowrap>权限</TH>
        <TH width="20%" nowrap>状态</TH>
        <TH width="10%" nowrap>操作</TH>
        <TH width="10%">最后登录时间</TH>
    </TR>

  <%foreach (Administrator admin in Model.AdministratorList)
    { %>
    <tr class="td_ys">
      <td width="10%" align=center><%=admin.AdminName %></td>
      <td width="10%" align=center><%=admin.GroupName%></td>
      <td width="20%" align=center><a href="menupermission.aspx?ptype=Single&adminid=<%=admin.AdminId%>">[分配]</a>
      <a href="javascript:void(0)" onclick="EditAuthorityDot(<%=admin.AdminId %>,'edit')">[功能点]</a>
      </td>
      <%if (admin.Status ==1)
        { %>
      <td width="20%" align=center>正常</td>
      <%}
        else
        { %>
       <td width="20%" align=center>停用</td>
       <%} %>
      <td width="10%" align=center><a href="javascript:void(0)" onclick=editAdmin(<%=admin.AdminId %>,'<%=admin.AdminName %>',<%=admin.GroupId%>)><img width="16"  height="16" border="0" src="/themes/default/images/admin/Item.Doc.Update.gif"></a>
			    <a href="javascript:void(0)" onclick="delAdminuser(<%=admin.AdminId%>,'<%=admin.AdminName%>')"><img width="16" height="16" border="0" src="/themes/default/images/admin/Item.Doc.Del.gif"></a></td>
      <td width="10%" align=center><%=admin.LastLoginDate%></td>
    </tr>
 <%} %> 
    

  </table>
  </div>
</div>



<div id="AddEditUser" title="用户" style="padding: 8px; display: none">
<table cellpadding="4" border="0px">
    <tr>
        <td style="width:56px">用户名称:</td>
	    <td><input name="adminname" type="text" maxlength="20" id="adminname" class="w300 inTextbox" /></td>
    </tr>
    <tr>
        <td style="width:56px">用户密码:</td>
	    <td><input name="password" type="text" maxlength="20" id="password" class="w300 inTextbox" /></td>
    </tr>
    <tr>
        <td style="width:56px">所属组:</td>
	    <td><select id="groupid" name="groupid">
         <% foreach (AdminGroup adminGroup in Model.AdminGroupList)
         { %>
           <option value="<%=adminGroup.GroupId%>"><%=adminGroup.GroupName %></option>
    
          <%} %>
        </select></td>
    </tr>
</table>
</div>


<div id="EditAuthorityDot" title="功能点权限分配" style="padding: 8px; display: none">
<table cellpadding="4" border="0px">
    <% foreach (MyAuthorityDot myAuthorityDot in Model.AuthorityDotList)
         { %>
           <tr>
            <td style="width:100px"><%=myAuthorityDot.DotName %></td>
            <td>
                <%foreach (var item in myAuthorityDot.AuthorityDots)
                  {%>
                      <input type="<%=item.Type == 1 ? "radio" : "checkbox"%>" id="<%=item.Id %>" value="<%=item.Id %>" name="<%=item.ParentId %>" /> <%=item.Text %>  
 
                  <%} %>
            </td>
           </tr>
    
          <%} %>
</table>
</div>

</body>