<%@ Page Language="C#" Inherits="PageView<ArticlePageModel>" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>新闻管理</title>
     <!-- #include file="/controls/header.htm" -->  
     <script type="text/javascript" src="/js/article.js?20140402"></script>
     <script type="text/javascript" src="/js/globalarticle.js"></script>
     <script language="javascript">
         $(document).ready(function () {
             $.ajax({
                 url: '/AjaxChannel/GetChannelNodeInit.cspx?typeID=1',
                 success: function (shtml) {
                     $("#ChannelId").html('');
                     $("#ChannelId").append("<option value='0'>-请选择类别-</option>" + shtml);
                 }
             });
         })
     </script>
</head>
<body>
 <iframe width="260" height="165" id="frmColorPalette" src="/js/ColorPalette.htm" style="visibility: hidden;
    position: absolute; border: 1px gray solid; left: 297px; top: -20px; z-index:10001" frameborder="0"
    scrolling="no"></iframe>
   <div class="con_p"> 

    <div class="cz_bk">
      <div class="lm_div">管理选项</div>
        <div class="cz_xx">
         
           <a href="addarticle.aspx" target="_blank" class='easyui-linkbutton l-btn l-btn-plain' plain="true" iconCls='icon-add' >添加新文章</a> 
           | <a href="addemptyarticle.aspx"  target="_blank"  class='easyui-linkbutton l-btn l-btn-plain' plain="true" iconCls='icon-add'>添加空文章</a> 
           | <a href="addpicarticle.aspx"  target="_blank"  class='easyui-linkbutton l-btn l-btn-plain' plain="true" iconCls='icon-add'>添加图片文章</a> 
           | <a href="addtitlearticle.aspx"  target="_blank"  class='easyui-linkbutton l-btn l-btn-plain' plain="true" iconCls='icon-add'>添加标题文章</a> 
           | <a href="#"  class='easyui-linkbutton l-btn l-btn-plain' plain="true" iconCls='icon-add'>添加文件下载</a> 
            | <a name="Button1" id="Button1"  class='easyui-linkbutton l-btn l-btn-plain' plain="true" iconCls='icon-edit' onclick="setChecked();"/>全选</a>
             | <a name="Button2" id="Button2"  class='easyui-linkbutton l-btn l-btn-plain' plain="true" iconCls='icon-edit' onclick="setInverseChecked();"/>反选</a> 
             | <a name="Submit2" id="Submit2"  class='easyui-linkbutton l-btn l-btn-plain' plain="true" iconCls='icon-edit'/>批量公开</a> 
             | <a name="Submit3" id="Submit3"  class='easyui-linkbutton l-btn l-btn-plain' plain="true" iconCls='icon-edit'/>批量拒绝</a> 
             | <a name="Submit1" id="Submit1"  class='easyui-linkbutton l-btn l-btn-plain' plain="true" iconCls='icon-cancel'/>批量删除<a></a> 
      
     
        </div>
    </div>

    <div class="cz_bk">
            <div class="lm_div">搜索信息</div>
   	        <div class="cz_xx">
            <table cellspacing="0" width="100%">
                    <tr>
                    <td><table><tr>
                    <td>
                    按类别：
                         <select id="ChannelId" class="inTextbox" name="ChannelId"  style="width:160px">    
                            <option value='0'>-请选择类别-</option>                        
                        </select>
                   </td>

                     <td>标题：<input class="inTextbox" type="text" name="txtKeyword" id="txtKeyword" maxlength="50" size="25" value=""/></td>
                    <td class="p10">信息日期 从 </td>
                        <td><input type="text" id="txtStartDate" name="StartDate" class="myTextbox easyui-datebox" style="width: 120px" /></td>
                        <td style="width: 35px; text-align: right"> 到 </td>
                        <td><input type="text" id="txtEndDate" name="EndDate" class="myTextbox easyui-datebox" style="width: 120px" /></td>
                        <td><a href="#" id="btnQuery" class="easyui-linkbutton" iconCls="icon-find">查找信息</a></td>
                    </tr></table></td>

                    </tr>
                </table>
            </div>
        </div>



     <div class="cz_bk" style="position:relative">
      
         <table id="tblQueryResult"></table>

  </div>


    </div>

  <div id="SortingNum" style="display: none; width: 150px;">
            <table width="150" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#000000">
                <tr style="background-color:#86B2D1">
                    <td width="150px" colspan="2">更改权重</td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" style="padding: 5px" width="70px">权重:</td>
                    <td align="left" valign="top" bgcolor="#FFFFFF" style="padding: 5px" width="80px">
                          <input id="orderNum" Width="80px"  Style="margin: 0 5px;border: #999 solid 1px; font-size: 12px;"/>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center" valign="middle" bgcolor="#FFFFFF" style="padding: 5px">
                        <input type="button"  id="btnChange" value="更改权重"  />&nbsp;&nbsp;
                        <input type="button" value="取  消"  onclick="Hide();" /></td>
                </tr>
                <input id="newsId" type="hidden" />
            </table>
  </div>


   <div id="AddOtherTitles" style="display: none; width: 900px;"  >
            <table width="98%" border="0" cellpadding="5" cellspacing="1" bgcolor="#C9D0D5">
                <tr bgcolor="DFEEFF"> 
                    <td>
                        <table width="100%" height="20" border="0" align="left" cellpadding="5" cellspacing="1" bgcolor="#C9D0D5" id="listtable">
                            <tr align="left" height="20" bgcolor="#DFEEFF">
                                <td width="30%" align="center">
                                    <b>标题</b></td>
                                <td width="30%" align="center">
                                    <b>url</b></td>
                                <td width="15%" align="center">
                                    <b>颜色</b></td>
                                <td width="15%" align="center">
                                    <b>加粗</b></td>
                                <td width="10%" align="center">
                                    <b>删除</b></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr bgcolor="#ffffff">
                    <td style="text-align: left">
                        <input id="btnAddTitle" type="button" value="再增加一条" onclick="addline()" />&nbsp;&nbsp;
                        <input ID="btnAdd"  type="button"  value="确  定" OnClientClick="return CheckInput()"/>&nbsp;&nbsp;
                        <input id="btnCancle" type="button" value="取  消" onclick="HideAddTitles()" />
                          
                         <input id="hiddenNewsId" type="hidden"  style="width:80px"/>
                         <input id="hiddenChannelId" type="hidden"  style="width:80px"/>
                        <input id="hiddenTitle" type="hidden"  style="width:80px" />
                        <input id="hiddentUrl" type="hidden"  style="width:80px"/>
                        <input id="hiddenColor" type="hidden"  style="width:80px"/>
                        <input id="hiddenJC" type="hidden"  style="width:80px"/>
         
                    </td>
                </tr>
            </table>
        </div>

    <%--资讯弹窗--%>
    <div id="PopupMsgInfo" title="文件" class="easyui-draggable" data-options="handle:'#title'" style=" display:none; position:absolute; width:800px;  top:40px; left:200px; border:1px solid #99BBE8;background:transparent url(/themes/default/scripts/jquery-easyui-1.3/themes/default/images/panel_title.png); padding:5px; "> 
        <div style="height:32px; line-height:24px; font-weight:bold;" id="title">
            <div style="float:left; padding-left:5px; color:#15428b">资讯弹窗</div>
            <div style="float:right; height:16px; width:20px; margin-top:2px; background:url('/themes/default/scripts/jquery-easyui-1.3/themes/default/images/panel_tools.gif') no-repeat -16px 0px; cursor:pointer;" id="closepic"></div>
        </div>
        <div style="clear:both"></div>
        <div style="height:410px; overflow:auto; background:#ffffff; overflow:hidden" id="changepopup">
        <iframe src="popupadd.aspx" width="798px"  frameborder="0" height="100%"></iframe>
        </div>
    </div>

</body>
</html>
