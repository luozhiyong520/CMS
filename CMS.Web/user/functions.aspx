<%@ Page Language="C#" Inherits="PageView<UserManagePageModel>" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>功能管理</title>
    <!-- #include file="/controls/header.htm" -->  
</head>

<body>
<div class="con_p">
    <div class="cz_bk">
      <div class="lm_div">管理选项</div>
        
        <div class="cz_xx">
            <a href="functions.aspx">功能列表</a> | <a href="javascript:void(0)" id="addfunction">添加功能</a>
        </div>
    </div>
   

   <div class="cz_bk">
      <div class="lm_div">功能列表</div>

  <div class="lm_name">
  <table  cellpadding="0" cellspacing="0" class="t_table">
    <tr class="tr_bg">
      <td width="10%">功能名</td>
      <td width="20%">操作</td>
    </tr>
  </table>
  </div>
  

  <table cellpadding="0" cellspacing="0" class="c_table">
     <% foreach (Functions function in Model.FunctionsList)
         { %>
    <tr class="td_ys">
      <td width="10%" align=center><%=function.FunctionName %></td>
      <td width="20%" align=center>编辑 删除</td>
    </tr>
   
    <%} %>
  </table>
  </div>
</div>

<div id="divAddItem" title="添加功能名" style="padding: 8px; display: none">
    <table cellpadding="4" border="0px">
        <tr>
            <td style="width: 80px">功能名:</td>
	        <td><input name="functionName" type="text" maxlength="20" id="functionName" class="w300" /></td>
        </tr>
    </table>
</div>
</body>
