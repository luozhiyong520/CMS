<%@ Page Language="C#" Inherits="PageView<AnalystPageModel>" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>分析师关联管理</title>
    <!-- #include file="/controls/header.htm" -->  
    <style type="text/css">
        /*.table-c table{border-right:1px solid #99BBE8;border-bottom:1px #99BBE8 solid}
        .table-c table td{border-left:1px solid #99BBE8;border-top:1px solid #99BBE8}*/
    </style>
    
 <script language="javascript" type="text/javascript">
     $(function () {
         $("#relativeanalyst").click(function () {
             var str = "";
             $('input:checked').each(function () {
                 str += (str == "" ? "" : ",") + $(this).val();
             });
           
             $.ajax({
                 type: "POST",
                 url: "AjaxAnalyst/AnalystRelative.cspx?AnalystId="+str+"&adminid="+<%=Model.adminId%>,
                 success: function (responseText) {
                     alert("操作成功");
                     window.location=window.location;
                 }
             });
         })
     });
 </script>
</head>

<body>
<div class="con_p" style="padding-bottom:100px;">
    <div class="cz_bk">
    <div class="lm_div">分析师列表</div>
    

     <table cellpadding="0" cellspacing="0" class="c_table">
         <TR>
        <TH width="30%" nowrap>分析师名</TH>
        <TH width="30%" nowrap>分析师类型</TH>
        <TH width="40%" nowrap>选择</TH>
    </TR>
    
     <%
             if (Model.AnalystList != null)
             {
                 foreach (Analyst analyst in Model.AnalystList)
                 {  %>
                <tr class="td_ys" >
                    <td width="40%" align="center"><%=analyst.AnalystName%></td>
                    <%if (analyst.AnalystType == 2)
                      { %>
                           <td width="40%" align="center">贵金属分析师</td>
                    <%}
                      else
                      { %>
                      <td width="40%" align="center">现货分析师</td>
                           <%} %>
                    <td width="10%" align="center" ><input type="checkbox" <% if(analyst.AdminId != null && analyst.AdminId != 0) {%>checked<%} %>  name="AnalystId" value="<%=analyst.AnalystId %>" /></td>
                </tr>
         <%}
             } %>
             <tr class="td_ys" style=" border-bottom:1px #99BBE8 solid;">
                    
                    <td align="right" colspan="3" style=" padding-right:13.5%;"><input type="button" value="提 交" class="inButton"  id="relativeanalyst" /></td>
             </tr>
       
  </table>




 </div>


</div>



 

</body>