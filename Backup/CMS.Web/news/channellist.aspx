<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <!-- #include file="/controls/header.htm" -->  
    <script src="../js/channelifo.js" type="text/javascript"></script>
    <title></title>
</head>
<body>
<div style="padding:10px;">
<input id="hideChannelID" type="hidden"/>
<div class="con">
    <div class="cz_bk">
      <div class="lm_div">管理选项</div>
        <div class="cz_xx">
            <a id="add" href="#" class='easyui-linkbutton l-btn l-btn-plain' plain="true" iconCls='icon-add' >新增频道</a>
        </div>
    </div>

    <div style="width:50%;float:left;">
    <div class="cz_bk">
      <div class="lm_div">普通栏目</div>
        <div class="lm_name">
            <div style="width:100%; height:620px; overflow-y:scroll;" id="divTree">
                <ul id="ChannelTree"></ul>
            </div>
        </div>
    </div>
</div>
<div style="width:49%; float:left; margin-left:10px;">
    <div id="cz_bk" class="cz_bk">
      <div id="lm_div" class="lm_div">碎片栏目</div>
        <div id="lm_name" class="lm_name">
            <div style="width:100%; height:620px; overflow-y:scroll;" id="divTreeFragment">
                <ul id="ChannelTreeFragment"></ul>
            </div>
        </div>
    </div>
</div>
</div>
 </div>

 <div id="divBody" style="padding: 8px; display:none;">
            <table cellpadding="0" cellspacing="0" style="text-align:center;">
                 <tr>
                    <td style="width: 80px;padding:2px; text-align:right;">
                        频道编码：
                    </td>
                    <td style="text-align:left;padding:2px;">
                        <label id="lbChannelId"></label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 80px;padding:2px; text-align:right;">
                        频道名称：
                    </td>
                    <td style="text-align:left;padding:2px;">
                        <input name="channelName" type="text" maxlength="50" style="width:280px;" id="txtChanneName"
                            class="easyui-validatebox" value="" /><samp style="color:Red;">*</samp>
                    </td>
                </tr>
                <tr>
                    <td style="width: 150px;padding:2px; text-align:right;">
                        频道英文名称：
                    </td>
                    <td style="text-align:left;padding:2px;">
                        <input name="channelEnName" style="  width:280px;" type="text" maxlength="10" id="txtChannelEnName" class="easyui-validatebox"
                            value="" /><samp id="EnName" style="color:Red;">*</samp>
                    </td>
                </tr>
                <tr>
                    <td style="width: 80px; padding:2px; text-align:right;">
                        所属类型：
                    </td>
                    <td style="text-align:left; padding:2px;">
                        <select id="drpTypeID" name="typeID" class="easyui-validatebox" style=" width:280px;">
                             <%--<option value="">--请选择--</option>--%>
                            <option value="1">普通栏目</option>
                            <option value="2">碎片栏目</option>
                        </select><samp style="color:Red;">*</samp>
                    </td>
                </tr>
                <tr>
                    <td style="width: 80px; padding:2px; text-align:right;">
                       父栏目：
                    </td>
                    <td style="text-align:left;padding:2px;">
                       <select id="drpparentID"  name="parentID" class="easyui-validatebox" style=" width:280px;">
                       <option value='0'>一级频道</option>
                        </select><samp style="color:Red;">*</samp>
                    </td>
                </tr>
                <tr>
                    <td style="width: 80px; padding:2px; text-align:right;">
                        路径：
                    </td>
                    <td style="text-align:left;padding:2px;">
                        <input name="url" style="  width:280px;" type="text"  id="txtUrl" class="easyui-validatebox"
                            value="http://" /><samp id="redid" style="color:Red;">*</samp><label id="lbmsg" style="color:Red;">(说明请以http://开头)</label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 80px; padding:2px; text-align:right;">
                       是否公开：
                    </td>
                    <td style="text-align:left;padding:2px;">
                       <input type="radio" name="status" value="true" checked="checked" id="yes" />
                        是<input type="radio" name="status" value="false" id="no"/>否
                    </td>
                </tr>
            </table>
        </div>
</body>
</html>
