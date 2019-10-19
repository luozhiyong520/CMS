<%@ Page Language="C#" Inherits="PageView<CustomerGroupPageModel>" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
        <title>用户分组导入</title>
<!-- #include file="/controls/header.htm" -->  
     <script type="text/javascript" src="/js/customergroup.js"></script>  
</head>
<body>
    <div class="con_p"> 

    <div class="cz_bk">
      <div class="lm_div">管理选项</div>
        <div class="cz_xx">
           <a href="customergroup.aspx" class='easyui-linkbutton l-btn l-btn-plain' plain="true" iconCls='icon-add' >用户分组导入</a> 
        </div>
    </div>

    

     <form id="form1" method ="post" enctype ="multipart/form-data" action="/AjaxCustomerGroup/ImportData.cspx">
        <div class="cz_bk" style="position:relative">
      
            <table cellSpacing="0" cellPadding="0" width="100%" border="0" class="t_table">

              <TR>
                  <TD width="10%" align="right" nowrap>数据文件导入：</TD>
                  <TD colspan="3" style="padding-left:10px">
                    <input type="file" name="UploadPath" id="UploadPath" class="inTextbox" size="40" />
                    <span class="red">请导入txt文件格式，txt内容格式一个ID,一个用户名。如：13511406,yOjrWUyIx</span>
                   </TD>
              </TR>

              <TR>
              <TD align="right" nowrap>所属用户组：</TD>
              <TD colspan="3" style="padding-left:10px;">
                
                <div>
                 <select id="GroupName" name="GroupName" style="width:120px" class="inTextbox">
                 
                  <%
                      if (Model.CustomerGroupList != null)
                      {
                          foreach (CustomerGroup customerGroup in Model.CustomerGroupList)
                          {
                   %>
                        <option value="<%=customerGroup.GroupName %>"><%=customerGroup.GroupName%></option>
                    <% }
                      } %>
                 </select>
                <a href="javascript:void(0)" id="createdgroup" class="crgoup">添加自定义组</a></div>
                 
               </TD>
            </TR>
             <TR>
              <TD align="right" nowrap>所属用户组：</TD>
              <TD colspan="3" style="padding-left:10px;">
                
                <div>
                <select id="RelevanceId" style=" width:130px;" name="RelevanceId"></select>
               </div>
                 
               </TD>
            </TR>

             <TR>
                <TD></TD>
                <TD colspan="3" style="height:40px">
                <INPUT class="inButton"  type="button" value="确 定" id="checkgroup" style="margin-left:60px"></TD>
            </TR>
            </table> 

        </div>
    </form>

    </div>


<div id="AddEditPopUserGroup" title="用户组" style="padding: 8px; display: none">
<table cellpadding="4" border="0px">
    <tr>
        <td style="width:56px">用户组名:</td>
	    <td><input name="AddGroupName" type="text" maxlength="20" id="AddGroupName" class="w300 inTextbox" /></td>
    </tr>
</table>
</div>

   
</body>
</html>
