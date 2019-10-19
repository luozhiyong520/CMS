<%@ Page Language="C#"  Inherits="PageView<QuestionnairePageModel>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
         <!-- #include file="/controls/header.htm" -->  
</head>
<body>
<div class="con_p" style="padding-bottom:100px;">
   <div class="cz_bk" style="overflow: hidden;*zoom: 1;">
    <div class="lm_div">管理选项</div>
    <div class="cz_xx" style="float:left;">
           <a href="questionnaireAdd.aspx"  class='easyui-linkbutton l-btn l-btn-plain' plain="true" iconCls='icon-add' >添加问卷</a>
    </div>
</div>
<div class="cz_bk">
  <div class="lm_div">问卷列表</div>
  <table cellpadding="0" cellspacing="0" class="c_table">
         <TR>
        <TH width="40%" nowrap>问卷标题</TH>
        <TH width="20%" nowrap>说明</TH>
         <TH width="20%" nowrap>操作</TH>
    </TR>
    <%foreach (Questionnaires obj in Model.Questions)
      { %>
       <tr class="td_ys">
        <td width="10%" align=center><%=obj.Title %></td>
        <td width="10%" align=center><%=obj.Description %></td>
        <td width="10%" align=center>
         <a  href="/statistics/questionnaireEdit.aspx?pid=<%=obj.QId %>"><img width="16"  height="16" border="0" src="/themes/default/images/admin/Item.Doc.Update.gif"></a></td>
       </tr>
      <%} %>
    </table>
</div>
</div>
</body>
</html>
