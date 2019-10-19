<%@ Page Language="C#" Inherits="PageView<UserManagePageModel>" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>功能管理</title>
    <!-- #include file="/controls/header.htm" -->  
</head>
 <%
     string listname = "";
     if (Model.PermissionType == "Single")
     {
         listname = "用户";
     }
     else
     {
         listname = "用户组";
     }
      %>
<body>
<div class="con_p">
    <div class="cz_bk">
      <div class="lm_div">管理选项</div>
       
        <div class="cz_xx">
         <%if (Model.PermissionType == "Single")
           { %>
            <a href="userlist.aspx"><%=listname%>列表</a> 
            <%}
           else
           { %>
          <a href="usergroup.aspx"><%=listname%>列表</a> 
          <%} %>
        </div>
    </div>
   

   <div class="cz_bk">
      <div class="lm_div"><%=listname%>权限分配</div>


  
  
  <table cellpadding="0" cellspacing="0" class="c_table">

   <TR>
      <th width="10%">序号</th>
      <th width="10%">模块名称</th>
      <th width="20%">权限</th>
    </TR>

 <%
     int i = 1;
     foreach (AdminMenuModule menumodule in Model.AdminMenuList)
     {
       %>
    <tr class="td_ys">
      <td width="10%" align="center"><span name="GroupName"> <%=i%></span></td>
      <td width="10%" align="center"><%=menumodule.ModuleName%></td>      
      <td width="20%" align="center">
      <%if (Model.PermissionType == "Single")
        { %>
      <a href="permission.aspx?moduleid=<%=menumodule.ModuleId %>&adminid=<%=Model.AdminId%>&ptype=<%=Model.PermissionType%>">[分配]</a>  
      <%}
        else
        { %><a href="permission.aspx?moduleid=<%=menumodule.ModuleId %>&adminid=<%=Model.GroupId%>&ptype=<%=Model.PermissionType %>">[分配]</a><%} %>
        </td>       
    </tr>
    <%
        i++;
   } %>
  </table>
  </div>
</div>


</body>
</html>
