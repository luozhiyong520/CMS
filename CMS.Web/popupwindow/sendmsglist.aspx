<%@ Page Language="C#" AutoEventWireup="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head >
    <title>用户留言管理</title>
    <!-- #include file="/controls/header.htm" --> 
    <script src="../js/jsontimeconvertor.js" type="text/javascript"></script>
     <script src="../js/sendmsg.js" type="text/javascript"></script>  
</head>
<body>
    
    <div class="con_p">
         <div class="cz_bk">
            <div class="lm_div">搜索信息</div>
            <div class="cz_xx">
                        问题类型:
                         <select id="typeContent" class="easyui-validatebox" name="TypeContent" style="width: 145px;">
                          </select>
                          问题类别:
                         <select id="MsgType" class="easyui-validatebox" name="MsgType" style="width: 145px;">
                            <option value="All">---请选择---</option>
                            <option value="Tel">手机问题</option>
                            <option value="PC">PC问题</option>
                          </select>
                          问题内容：
                          <input type="text" id="txtMsgContent" class="easyui-validatebox" name="MsgContent"/>
                        &nbsp;&nbsp;&nbsp;提问时间从:
                        <input type="text" id="tuTimeStart" class="myTextbox easyui-datebox" name="TuTimeStart"/>
                        到<input type="text" id="tuTimeEnd" class="myTextbox easyui-datebox" name="TuTimeEnd"/>
                        <br />
                        是否回复：
                        <select id="YesOrNo" name="YesOrNo" class="easyui-validatebox" style="width: 145px;">
                            <option value="">&nbsp;&nbsp;&nbsp;---请选择---</option>
                            <option value="yes">是</option>
                            <option value="no">否</option>
                          </select>
                           <span class="hh">
                           回复时间从:
                        <input type="text" id="ReplyTimeStart" class="myTextbox easyui-datebox" name="ReplyTimeStart"/>
                        到<input type="text" id="ReplyTimeEnd" class="myTextbox easyui-datebox" name="ReplyTimeEnd"/>
                          回复人：
                          <input type="text" id="Editor" name="Editor"/>
                          </span>
                         <a href="#" id="btnSearch" class="easyui-linkbutton" iconCls="icon-find">搜索信息</a>
            </div>
        </div>

        <div class="cz_bk" style="position:relative">
        <table id="SendMsgInfo">     
        </table>
        </div>
    </div>
</body>
</html>
