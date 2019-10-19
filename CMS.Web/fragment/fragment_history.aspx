<%@ Page Language="C#" AutoEventWireup="true" Inherits="PageView<FragmentPageModel>" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <!-- #include file="/controls/header.htm" -->
    <script src="../js/fragmenthistory.js" type="text/javascript"></script>
</head>
<body>
    <table cellpadding="4" border="0px" id="tableHistory">
    <%
        for (int i =0; i <Model.DataTableHistory.Rows.Count; i++)
        {
            %>
            <tr>
                <td style="width: 80px;">
                    历史记录<%=i+1 %>:
                </td>
                <td>
                     <textarea id="historyContent<%=i+1 %>" readonly="readonly" name="content" class="easyui-validatebox" cols="20" rows="2"
                        style="width: 700px; height: 150px;"><%=Model.DataTableHistory.Rows[i]["Content"]%></textarea> 
                        <input id="hdFragmentId<%=i+1 %>" type="hidden" value='<%=Model.DataTableHistory.Rows[i]["FragmentId"]%>' />
                         <input id="hdContent<%=i+1 %>" type="hidden" value='<%=Model.DataTableHistory.Rows[i]["Content"]%>' />
                         <input id="hdChannelID<%=i+1 %>" type="hidden" value='<%=Model.DataTableHistory.Rows[i]["ChannelId"]%>' />
                         <input id="hdOrderNum<%=i+1 %>" type="hidden" value='<%=Model.DataTableHistory.Rows[i]["OrderNum"]%>' />
                         <input id="hdTypeId<%=i+1 %>" type="hidden" value='<%=Model.DataTableHistory.Rows[i]["TypeId"]%>' />
                         <input id="hdIsDelete<%=i+1 %>" type="hidden" value='<%=Model.DataTableHistory.Rows[i]["IsDeleted"]%>' />
                        <a href="#" id="backHistory&<%=i+1 %>" class="easyui-linkbutton">恢复碎片内容</a>
                </td>
            </tr>
       <% }
         %>
        </table>
        <div style="text-align:center;"> 
        <a href="javascript:window.close();" id="ColseHistory" class="easyui-linkbutton" isoc="icon-no">关闭</a>
        </div>
</body>
</html>
