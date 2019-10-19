<%@ Page Language="C#" Inherits="PageView<UserManagePageModel>" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>功能管理</title>
       <!-- #include file="/controls/header.htm" -->  
</head>

<body>
<div class="con_p" style="padding-bottom:100px;">
     
   <div class="cz_bk">
      <div class="lm_div">用户列表</div>
  <table cellpadding="0" cellspacing="0" class="c_table">
         <TR>
        <TH width="10%" nowrap>用户名</TH>
        <TH width="20%" nowrap>权限</TH>
    </TR>
    
     <%foreach (Administrator admin in Model.AdministratorList)
    { %>
    <tr class="td_ys">
      <td width="10%" align=center><%=admin.AdminName %></td>
      <td width="20%" align=center><a href="analystrelative.aspx?adminid=<%=admin.AdminId%>">[分配分析师]</a>
      </td>
    </tr>
 <%} %> 
  
    
  </table>
  </div>
</div>

 
</body>
</html>
