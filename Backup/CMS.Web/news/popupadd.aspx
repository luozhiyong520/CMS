<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>推送弹窗消息</title>   
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
 </head>
<body>
<table  width="100%" border="0" cellpadding="4" cellspacing="1" class="tdBorder" id="LookPopu">
 <%-- <tr class="tdc2">
    <td  width="15%" height="27" align="right">标题：</td>
    <td width="50%"><label><input type="text" name="textfield" id="Title" class="w300 inTextbox" maxlength="20" /></label></td>
    <td width="35%">&nbsp;</td>
  </tr>

  <tr  class="tdc1">
    <td align="right">
      正文：</td>
    <td><label>
      <textarea name="textarea" id="Content" style="width:360px; height:100px" class="w300 inTextbox" maxlength="200" onpropertychange="if(value.length>200) value=value.substr(0,200)"></textarea>
    </label></td>
    <td valign="bottom" class="red">200字以内</td>
  </tr>

   <tr class="tdc2">
    <td height="7"></td>
    <td></td>
    <td></td>
  </tr>

  <tr class="tdc1">
    <td height="103" >&nbsp;</td>
    <td><div><img src="/themes/default/images/u0_normal.png" width="103" height="106" alt="" id="inputImgUrl" /></div></td>
    <td>&nbsp;</td>
  </tr>

  <tr id="upImg" class="tdc2">
    <td height="40" align="right">图片：</td>
    <td><label>
        <span><input type="file" name="file" id="upfile" class="w300 inTextbox" /></span>
        <input type="hidden" value="" id="ImgUrl"class="w300 inTextbox"  />
    </label></td>
    <td><span class="red">大图：580*440px<br/>小图：90*70px</span></td>
  </tr>

  <tr class="tdc1">
    <td height="40" align="right">链接：</td>
    <td><input name="" type="text" id="PageUrl"  value=""  class="w300 inTextbox"  /></td>
    <td class="red">链接地址请以http://开头</td>
  </tr>--%>

   <tr class="tdc1">
    <td  width="15%" height="27" align="right">投放版本：</td>
    <td width="50%"><select id="slctPushVersion" style=" width:130px;">
    </select></td>
    <td width="35%">&nbsp;</td>
  </tr>

   <tr class="tdc2">
    <td  width="15%" height="27" align="right">推送到栏目：</td>
    <td width="50%"><select id="slctPushColumn" style=" width:130px;">
    <option value="">请选择</option>
    <option value="公告">公告</option>
    <option value="解盘">解盘</option>  
    </select></td>
    <td width="35%">&nbsp;</td>
  </tr>

  <tr class="tdc1">
    <td  width="15%" height="27" align="right">平台：</td>
    <td width="50%" id="PushPlatform">
        <input id="chboxPushPlatformPC" name="PushPlatform" type="checkbox" value="pc"/>PC&nbsp;&nbsp;&nbsp;&nbsp;<input name="PushPlatform" id="chboxPushPlatformMove" type="checkbox" value="android" />android&nbsp;&nbsp;&nbsp;&nbsp;<input name="PushPlatform" id="Checkbox1" type="checkbox" value="ios" />ios
    </td>
    <td width="35%">&nbsp;</td>
  </tr>

  <tr class="tdc2">
    <td height="40" align="right">接收对象：</td>
    <td><table width="280" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td><label><input checked="checked" type="radio" name="ReceiverTypeSel" value="0" id="RadioGroup1_0"/>用户组</label></td>
    <td><label><input type="radio" name="ReceiverTypeSel" value="1" id="RadioGroup1_1"  />指定用户</label></td>
     <td><label><input type="radio" name="ReceiverTypeSel" value="2" id="RadioGroup1_2"  />从CRM系统获取群组名单</label></td>
      <input type="hidden" id="ReceiverType" value="0" />
  </tr>
</table></td>
    <td>&nbsp;</td>
  </tr>

  <tr style="display:none;">
    <td height="35">&nbsp;</td>
    <td><label>
      <input type="text" name="textfield3" id="a" class="input1"  value="接收对象描述" class="w300 inTextbox" />
    </label></td>
    <td>&nbsp;</td>
  </tr>

  <tr class="tdc1">
    <td width="15%" >&nbsp;</td>
    <td width="50%" >
    <!--用户组内容-->
    <table id="ReceiverType0" width="400px" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td height="30"><label><input type="checkbox" name="checkbox" id="GroupAll" />全部用户</label></td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
      </tr>
      <tr height="20">
        <td><ul class="in-row" id="ulReceiverGroup" style="white-space:nowrap; width:400px;">正在加载分组...</ul></td>
      </tr>
    </table>

     

      <!--指定用户内容-->
      <label id="ReceiverType1" style="display:none;"><textarea id="Receiver"  style="width:360px; height:100px"  class="textarea2">请输入用户名，多个用户名请使用,分隔</textarea></label>
       
        <table id="ReceiverType2" width="400" border="0" cellspacing="0" cellpadding="0" style="display:none;">
        <tr height="20">
            <td><select id="ulReceiverGroup1"></select></td>
        </tr>
        </table>
        <input type="button" value="同步" class="inButton" id="tongbu" style="display:none"/>

</td>
    <td width="15%"  class="red">如果选择从CRM系统获取群组名单，请点击同步。</td>
 </tr>

  <tr class="tdc2">
    <td height="40" align="right">发送时间：</td>
    <td><table width="400" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td><label><input type="radio" name="PlanTypeSel" value="0" checked="checked" id="PlanType1" />立刻</label></td>
        <td> <label><input type="radio" name="PlanTypeSel" value="1" id="PlanType2"/>定时</label></td> <td width="210"><input type="text" id="BeginTime" class="w300 inTextbox"  style="display:none;"/>
            <input id="PlanType" type="hidden" value="0" /></td>
      </tr>
    </table></td>
    <td>&nbsp;</td>
  </tr>

  <tr class="tdc1">
    <td height="40" align="right">
       时效至：</td>
    <td><label>
      <input type="text" name="textfield4" id="EndTime" class="w300 inTextbox" />
    </label></td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td height="45">&nbsp;</td>
    <td><label>
      <input type="button" name="button2" id="submit" value="提 交" class="inButton" />
    </label></td>
    <td>&nbsp;</td>
  </tr>
</table>
</body>
</html>
