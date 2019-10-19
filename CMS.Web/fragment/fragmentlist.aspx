<%@ Page Language="C#" Inherits="PageView<FragmentPageModel>" ValidateRequest="false"%>
<%@ Import Namespace="Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

 
<head runat="server">
    <title>碎片管理</title>
    <meta http-equiv="Content-Type" content="text/html;charset=utf-8" />
    <script src="../themes/default/scripts/ueditor/editor_config.js" type="text/javascript"></script>
    <script src="../themes/default/scripts/ueditor/editor_all.js" charset="utf-8" type="text/javascript"></script>
    <script src="../themes/default/scripts/ueditor/_src/plugins/image.js" type="text/javascript"></script>
    <link href="../themes/default/scripts/ueditor/themes/default/ueditor.css" rel="stylesheet" type="text/css" />
    <!-- #include file="/controls/header.htm" -->
    <script src="../themes/default/scripts/jquery-easyui-1.3/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <script src="../js/fragment.js?00992" type="text/javascript"></script>
    <style type="text/css">
        .GridView_FooterStyle {
            background-color: #F7F6F3;
            border-top: 1px solid #D0D0D5;
            color: #333333;
        }
       .pageNumber {
        background-color: #FFFFFF;
        border: 1px solid #CCCCCC;
        height: 18px;
        width: 35px;
        }
    </style>
</head>
<body>
 <input id="hidTypeId" type="hidden" value=" <%=Model.TypeId %>"/>
     <div class="con_p"> 
        <div class="cz_bk">
            <div class="lm_div">管理选项</div>
            <div class="cz_xx">
                <a id="add" href="#" class='easyui-linkbutton l-btn l-btn-plain' plain="true" iconCls='icon-add' >添加碎片</a>
            </div>
        </div>
          <div class="cz_bk">
            <div class="lm_div">搜索信息</div>
            <div class="cz_xx">
                        栏目名称:
                         <select id="searchdrpchannelID" class="easyui-validatebox" name="channelID" style="width: 145px;">
                            <option value=''>&nbsp;&nbsp;&nbsp;---请选择---</option>
                             <%=Model.strhtml %>
                          </select>
                        &nbsp;&nbsp;&nbsp;碎片内容:
                        <input type="text" id="searchContent" name="Content"/>
                         <a href="#" id="btnSearch" class="easyui-linkbutton" iconCls="icon-find">搜索信息</a>
            </div>
        </div>
        <div class="cz_bk">
                <table id="tblQueryResult"></table>
        </div>
    </div>
    <div id="divbody" title="碎片" style="display: none">
        <table cellpadding="4" border="0px">
            <tr>
                <td style="width: 80px">
                    栏目名称:
                </td>
                <td>
                    <select id="drpchannelID" class="easyui-validatebox" name="channelID" style="width: 145px;">
                        <option value=''>&nbsp;&nbsp;&nbsp;---请选择---</option>
                        <%=Model.strhtml %>
                    </select><samp style="color:Red;">*</samp>&nbsp;&nbsp;栏目编号：<label id="lbchannelID"></label>
                </td>
            </tr>
            <tr>
                <td style="width: 80px;">
                    排序号:
                </td>
                <td>
                    <input id="OrderNum" name="orderNum" onkeyup="this.value=this.value.replace(/\D/g,'')" onafterpaste="this.value=this.value.replace(/\D/g,'')" class="easyui-validatebox" style="width: 145px;"
                        type="text" value="0"/><samp style="color:Red;">*</samp>
                </td>
            </tr>
            <tr>
                <td style="width: 80px">
                    碎片内容:
                </td>
                <td>
                    <textarea id="Content" name="content" class="easyui-validatebox" cols="20" rows="2"
                        style="width: 700px; height: 360px;"></textarea><samp style="color:Red;">*</samp>
                   <input id="btnContent" type="button" value="文本编辑" style="cursor:pointer;" />
                </td>
            </tr>
            <tr>
                <td style="width: 80px; display:none;">
                    是否删除:
                </td>
                <td style="display:none;">
                    <input id="yes" type="radio" value="true" name="isDeleted" />是
                    <input id="no" type="radio" value="false" name="isDeleted" checked="checked" />否
                </td>
            </tr>
            
        </table>
        <table cellpadding="4" border="0px" id="tableHistory" style="display:block;">
            <tr>
                <td style="width: 80px;">
                    历史记录:
                </td>
                <td>
                     <textarea id="historyContent" readonly="readonly" name="content" class="easyui-validatebox" cols="20" rows="2"
                        style="width: 700px; height: 150px;"></textarea> 
                    <input id="hdfragmentId" type="hidden" />
                        <a href="#" id="backContent" class="easyui-linkbutton">恢复碎片内容</a>
                </td>
            </tr>
            <tr>
                <td style="width: 100px;">
                    查看更多历史记录:
                </td>
                <td>
                    <input id="hdChannelId" type="hidden" />
                    <a href="#" id="showHistory" class="easyui-linkbutton">更多历史记录</a>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
