<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>发布弹窗消息</title>   
    <link href="../themes/default/styles/upload.css" rel="stylesheet" type="text/css"/>
    <link href="/themes/default/styles/manage.css" type="text/css" rel="stylesheet"/> 
    <link type="text/css" href="../js/jquerydatatime/css/jquery-ui-1.8.17.custom.css" rel="stylesheet" />
    <link type="text/css" href="../js/jquerydatatime/css/jquery-ui-timepicker-addon.css" rel="stylesheet" />
    <script type="text/javascript" src="../js/jquerydatatime/js/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" src="../js/jquery.form.js"></script>
    <script src="/js/jsontimeconvertor.js" type="text/javascript"></script>
	<script type="text/javascript" src="../js/jquerydatatime/js/jquery-ui-1.8.17.custom.min.js"></script>
	<script type="text/javascript" src="../js/jquerydatatime/js/jquery-ui-timepicker-addon.js"></script>
    <script type="text/javascript" src="../js/jquerydatatime/js/jquery-ui-timepicker-zh-CN.js"></script>
    <script type="text/javascript" src="../js/popupadd.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#btncolse").click(function () {
                var PopupMsgInfo = getQueryString("PopupMsgInfo");
                var PopupMsgPlanInfo = getQueryString("PopupMsgPlanInfo");
                ("#" + PopupMsgInfo).css("display", "none");
                $('#' + PopupMsgPlanInfo).datagrid("reload");
            })
        })
        function getQueryString(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
            var r = window.location.search.substr(1).match(reg);
            if (r != null) return unescape(r[2]); return null;
        }
    </script>
 </head>
<body>
<table  width="100%" border="0" cellpadding="4" cellspacing="1" class="tdBorder" id="LookPopu">
  <tr class="tdc2">
    <td width="35%" align="right">标题：</td>
    <td width="50%"><label><input disabled="disabled" type="text" name="textfield" id="Title" class="w300 inTextbox" maxlength="20" /></label></td>
  </tr>

  <tr  class="tdc1">
    <td align="right">
      正文：</td>
    <td><label>
      <textarea name="textarea" disabled="disabled" id="Content" style="width:360px; height:100px" class="w300 inTextbox" maxlength="200" onpropertychange="if(value.length>200) value=value.substr(0,200)"></textarea>
    </label></td>
  </tr>
  <tr id="imgurlId" class="tdc2">
    <td align="right"></td>
    <td></td>
  </tr>
   <tr class="tdc1">
    <td align="right">投放版本:</td>
    <td><select id="slctPushVersionLook" disabled="disabled" style=" width:130px;">
    </select></td>
  </tr>
  <tr class="tdc2">
    <td align="right">推送到栏目:</td>
    <td><select id="slctPushColumn" disabled="disabled" style=" width:130px;">
     <option value="">请选择</option>
    <option value="公告">公告</option>
    <option value="解盘">解盘</option> </select></td>
  </tr>
  <tr class="tdc1">
    <td align="right">平台:</td>
    <td><input id="chboxPushPlatformPC" name="PushPlatform" disabled="disabled"  type="checkbox" value="PC"/>PC&nbsp;&nbsp;&nbsp;&nbsp;<input disabled="disabled" name="PushPlatform" id="chboxPushPlatformMove" type="checkbox" value="Move" />移动设备</td>
  </tr>
  <tr class="tdc2">
    <td height="40" align="right">接收对象：</td>
    <td><table width="280" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td><label><input type="radio" disabled="disabled" name="ReceiverTypeSel" value="0" id="RadioGroup1_0" checked="checked" />用户组</label></td>
    <td><label><input type="radio" disabled="disabled" name="ReceiverTypeSel" value="1" id="RadioGroup1_1"  />指定用户</label></td>
     <td><label><input type="radio" disabled="disabled" name="ReceiverTypeSel" value="2" id="RadioGroup1_2"  />从CRM系统获取群组名单</label></td>
      <input type="hidden" id="ReceiverType" value="0" />
  </tr>
</table></td>
  </tr>

  <tr style="display:none;">
    <td height="35">&nbsp;</td>
    <td><label>
      <input type="text" disabled="disabled" name="textfield3" id="a" class="input1"  value="接收对象描述" class="w300 inTextbox" />
    </label></td>
  </tr>

  <tr class="tdc1">
    <td>&nbsp;</td>
    <td>
    <!--用户组内容-->
    <table id="ReceiverType0" width="400" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td height="30"><label><input type="checkbox" disabled="disabled" name="checkbox" id="GroupAll" />全部用户</label></td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
      </tr>
      <tr height="20">
        <td><ul class="in-row" id="ulReceiverGroup">正在加载分组...</ul></td>
      </tr>
    </table>

     

      <!--指定用户内容-->
      <label id="ReceiverType1" style="display:none;"><textarea id="Receiver" disabled="disabled"  style="width:360px; height:100px"  class="textarea2">请输入用户名，多个用户名请使用,分隔</textarea></label>
       
        <table id="ReceiverType2" width="400" border="0" cellspacing="0" cellpadding="0" style="display:none;">
        <tr height="20">
            <td><select id="ulReceiverGroup1" disabled="disabled"></select></td>
        </tr>
        </table>
        <input type="button" value="同步" disabled="disabled" class="inButton" id="tongbu" style="display:none"/>

</td>
 </tr>

  <tr class="tdc2">
    <td height="40" align="right">发送时间：</td>
    <td><table width="400" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td><label><input type="radio" disabled="disabled" name="PlanTypeSel" value="0" checked="checked" id="PlanType1" />立刻</label></td>
        <td> <label><input type="radio" disabled="disabled" name="PlanTypeSel" value="1" id="PlanType2"/>定时</label></td> <td width="210"><input type="text" id="BeginTime" class="w300 inTextbox" disabled="disabled" style="display:none;"/>
            <input id="PlanType" type="hidden" value="0" /></td>
      </tr>
    </table></td>
  </tr>

  <tr class="tdc1">
    <td height="40" align="right">
       时效至：</td>
    <td><label>
      <input type="text" disabled="disabled" name="textfield4" id="EndTime" class="w300 inTextbox" />
    </label></td>
  </tr>
  <tr class="tdc2">
    <td height="40" align="right">
       </td>
    <td><label>
      <input type="button" id="btncolse"  style=" width:50px; height:30px; display:none;" value="关闭" />
    </label></td>
  </tr>
</table>

</body>
</html>
