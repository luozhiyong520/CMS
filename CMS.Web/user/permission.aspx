<%@ Page Language="C#" Inherits="PageView<UserManagePageModel>" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>功能管理</title>
    <!-- #include file="/controls/header.htm" -->  
    <script type="text/javascript" src="/js/permission.js"></script>
    <script language="javascript" type="text/javascript">
        function setChecked(selectid) {
            var checklength = $("#" + selectid + " input:checkbox").length-1;
                for (var i = 0; i < checklength; i++) {
                if ($("#" + selectid + " input:checkbox")[i].checked == false) {
                    $("#" + selectid + " input:checkbox")[i].checked = "checked";
                } else{
                    $("#" + selectid + " input:checkbox")[i].checked = "";
                }
             };
        }

        function SelectAll() {
            var ids = $("input[type='checkbox']");
            for (var i = 0; i < ids.length; i++) {
                if (ids[i].checked == true) {
                    ids[i].checked = "";
                } else {
                    ids[i].checked = "checked";
                }  
            }
        }
 
 </script>

</head>
<% 
   int i = 0;
   string type = Model.PermissionType;
   int adminid = 0;
   int groupid = 0;
   string listname = "";
   if (type == "Single")
   {
       adminid = Model.AdminId;
       listname = "用户";
   }
   else
   {
       groupid = Model.GroupId;
       listname = "用户组";
   }
  %>
<body>
<div class="con_p">
    <div class="cz_bk">
      <div class="lm_div">管理选项</div>
        
        <div class="cz_xx">
           <%if (type == "Single")
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
      <div class="lm_div">设置<%=listname%>权限</div>

  
  <table cellpadding="0" cellspacing="0" class="c_table">
    
     <TR>
        <th width="10%">模块名称</th>
      <th width="10%">菜单名</th>
      <th width="10%">增加</th>
      <th width="10%">修改</th>
      <th width="10%">删除</th>
      <th width="10%">查看</th>
      <th width="20%">全选</th>
    </TR>

     <%
        
       foreach (AdminMenuModule twomenumodule in Model.AdminMenuList)
     {   
        %>
    <tr class="td_ys" id="select<%=i%>">
     <%if (i == 0){ %>           
      <td width="10%" align="center" rowspan="<%=Model.AdminMenuList.Count %>"><span name="GroupName"> <%=Model.ModuleName%></span></td>
      <%} %>
        <td width="10%" align="center"><%=twomenumodule.ModuleName %></td>
        <td width="10%" align="center"><input type="checkbox" value="1" name="add<%=twomenumodule.ModuleId%>"  <%=Model.GetFunctionCheckedInfo(1,twomenumodule.ModuleId) %>/></td> 
        <td width="10%" align="center"><input type="checkbox" value="2" name="edit<%=twomenumodule.ModuleId%>" <%=Model.GetFunctionCheckedInfo(2,twomenumodule.ModuleId) %>/></td> 
        <td width="10%" align="center"><input type="checkbox" value="3" name="del<%=twomenumodule.ModuleId%>"  <%=Model.GetFunctionCheckedInfo(3,twomenumodule.ModuleId) %>/></td> 
        <td width="10%" align="center"><input type="checkbox" value="4" name="look<%=twomenumodule.ModuleId%>"  <%=Model.GetFunctionCheckedInfo(4,twomenumodule.ModuleId) %>/></td> 
        <td width="20%" align="center"><input type="checkbox" value="" name="checkbox<%=i%>" id="checkbox<%=i%>" onclick="setChecked('select<%=i%>');" /></td>  
                 
    </tr>
    <% i++;} %>
       
    <%if (type == "Single")
      { %>
      <input type="hidden" value="<%=adminid %>"  name="adminid" />
    <%}
      else
      { %>
    <input type="hidden" value="<%=groupid%>" name="groupid" />
    <%} %>
    <input type="hidden" value="<%=Model.PermissionType %>"  name="m_type" />
    <input type="hidden" value="<%=Model.ParentId %>"  name="parentid" />
    <tr class="td_ys">
       
      <td align="right"  colspan="7" style=" height:40px; line-height:40px; padding-right:52px;">
      <input type="checkbox" value="" name="checkall" id="checkall" onclick="SelectAll()" style="margin-right:36px;" />
      <input type=button value="设置权限" id="addpermission" />
      
      </td>   
       
    </tr>
    
  </table>

  </div>
</div>



</body>
</html>
