<%@ Page Language="C#" AutoEventWireup="true" Inherits="PageView<SendMsgPageModel>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <!-- #include file="/controls/header.htm" -->  
    <script src="../js/sendmsgedit.js" type="text/javascript"></script>
<script src="../js/jsontimeconvertor.js" type="text/javascript"></script>
<style>
.con_p tr {
line-height: 20px;
height: 20px;
}
</style>
</head>
<body>
    <div class="con_p">
       <div class="cz_bk">
                <div class="lm_div">留言回复</div>
                <div class="cz_xx">
                    <table cellpadding="4" border="0px">
                       <tr>
                        <td>
                            硬件版本：
                        </td>
                        <td>
                           <label id="lbHardwareVersion"><%=Model.SendMsg.HardwareVersion %></label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            软件版本：
                        </td>
                        <td>
                           <label id="lbSoftwareVersion"><%=Model.SendMsg.SoftwareVersion %></label>
                        </td>
                    </tr>
                       <tr>
                        <td>
                            提问类型：
                        </td>
                        <td>
                           <label id="lbTypeContent"><%=Model.SendMsg.TypeContent %></label>
                        </td>
                    </tr>
            <tr>
                <td>
                    提问时间：
                </td>
                <td>
                   <label id="lbQuTime"><%=Model.SendMsg.QuTime %></label>
                   <input id="hdMsgId" type="hidden" value="<%=Model.SendMsg.MsgId %>" />
                </td>
            </tr>
            <tr>
                <td>
                    提问者:
                </td>
                <td>
                    <label id="lbQuCustomerName"><%=Model.SendMsg.QuCustomerName %></label>
                </td>
            </tr>
            <tr>
                <td>
                    提问内容:
                </td>
                <td>
                    <label id="lbMsgContent"><%=Model.SendMsg.MsgContent %></label>
                </td>
            </tr>
            <tr>
                <td>
                    图片:
                </td>
                <td>
                    <%
                        if (!string.IsNullOrEmpty(Model.SendMsg.ImgUrl))
                        {
                            %>
                             <img id="ImgUrl" src="http://img.upchina.com<%=Model.SendMsg.ImgUrl %>" />
                       <% }else
                            %>
                             <img id="ImgUrl" src="" />
                        <% %>
                  
                </td>
            </tr>
            <tr>
                <td>
                    联系电话:
                </td>
                <td>
                    <label id="lbTel">
                    
                     <%
                     if (Model.SendMsg.MsgTypeId == 8 && Model.SendMsg.PhoneOrEmail!=null)
                     {
                         if (Model.SendMsg.PhoneOrEmail.IndexOf('@') > 0)
                         {
                               %>
                               <%=Model.SendMsg.PhoneOrEmail%> 
                            <%}
                         else
                         {%><%= Model.SendMsg.PhoneOrEmail.Substring(0, Model.SendMsg.PhoneOrEmail.Length - 4) + "****"%>
                       <%}
                     }
                     else
                     {%>
                       
                        <%=string.IsNullOrEmpty(Model.SendMsg.Tel) ? "" : Model.SendMsg.Tel.Substring(0, Model.SendMsg.Tel.Length-4)+"****"%>
                     <%} %>
                    </label>
                </td>
            </tr>
            <tr>
                <td>
                    回复内容:
                </td>
                <td>
                     <textarea id="txtReplyContent" <%=Model.ViewText %> cols="20" rows="2"
                        style="width:480px; height: 100px;"><%=Model.SendMsg.ReplyContent %></textarea>
                </td>
            </tr>
            <tr>
                <td>
                    回复人:
                </td>
                <td>
                    <input type="text" id="txtReplier" disabled="disabled" value="<%=Model.SendMsg.Replier %>" />
                </td>
            </tr>
            <tr>
                <td>
                    回复时间:
                </td>
                <td>
                    <input type="text" id="txtReplyTime" disabled="disabled" value="<%=Model.SendMsg.ReplyTime %>" />
                </td>
            </tr>
            <tr>
                <td colspan="2" >
                    <input type="button" id="btnReply" value="回 复" style="margin-left:200px;" />
                </td>
            </tr>
            <tr>
                <td>
                 <input id="hdParamsList" type="hidden" name="ParamsList" value="<%=Model.ParamsList %>" />
                <%-- <div class="hd" style=" display:none">
                    <input id="hdParamsList" type="hidden" name="ParamsList" value="<%=Model.ParamsList %>" />
                   <input id="Hidden2" type="hidden" name="MsgType" value="<%=Model.MsgType %>" />
                    <input id="Hidden3" type="hidden" name="TuTimeStart" value="<%=Model.TuTimeStart %>" />
                    <input id="Hidden4" type="hidden" name="TuTimeEnd" value="<%=Model.TuTimeEnd %>" />
                    <input id="Hidden5" type="hidden" name="ReplyTimeStart" value="<%=Model.ReplyTimeStart %>" />
                    <input id="Hidden6" type="hidden" name="ReplyTimeEnd" value="<%=Model.ReplyTimeEnd %>" />
                    <input id="Hidden7" type="hidden" name="YesOrNo" value="<%=Model.YesOrNo %>" />
                    <input id="Hidden8" type="hidden" name="Editor" value="<%=Model.Editor %>" />
                </div>--%>
                    <input type="button" id="btnback" value="上一问题" />
                </td>
                <td style=" padding-left:300px;">
                    <input type="button" id="btngo" value="下一问题" />
                </td>
            </tr>
        </table>
                </div>
            </div>
    </div>
</body>
</html>
