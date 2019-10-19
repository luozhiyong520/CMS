<%@ Page Language="C#" Inherits="PageView<ArticlePageModel>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>添加空文章</title>
     <!-- #include file="/controls/header.htm" -->  
     <script type="text/javascript" src="/js/addarticle.js"></script>
     <script type="text/javascript" src="/js/globalarticle.js"></script>
     <script type="text/javascript" src="/themes/default/scripts/ueditor/editor_config.js"></script>
    <script type="text/javascript" src="/themes/default/scripts/ueditor/editor_all.js"></script>
    <link rel="stylesheet" href="/themes/default/scripts/ueditor/themes/default/ueditor.css"> 
     <script language="javascript">
         $.ajax({
             url: '/AjaxChannel/GetChannelNodeInit.cspx?typeID=1',
             success: function (shtml) {
                 $("#ChannelId").html('');
                 $("#ChannelId").append("<option value='0'>-请选择类别-</option>" + shtml);
             }
         });
     </script>
</head>
<body>
        <input id="hiddenTitle" type="hidden" />
        <input id="hiddentUrl" type="hidden" />
        <input id="hiddenColor" type="hidden"/>
        <input id="hiddenJC" type="hidden"/>
        
     
        <iframe width="260" height="165" id="frmColorPalette" src="/js/ColorPalette.htm"
            style="visibility: hidden; position: absolute; border: 1px gray solid; left: 297px;
            top: -50px;" frameborder="0" scrolling="no"></iframe>

     <div class="con_p"> 
 
        <div class="cz_bk">
 
        <div class="lm_div">添加空文章</div>
  
        <table cellspacing="0" cellpadding="0" width="100%" border="0" class="t_table" id="emptynews"> 
           
            <tr bgcolor="DFEEFF">
                <td colspan="5">
                    <table  cellpadding="0" cellspacing="0" id="listtable" class="c_table">
                        <tr align="left" height="20" bgcolor="#DFEEFF">
                           <td width="35%" align=center>
                                <b>标题</b></td>
                            <td width="35%" align=center>
                                <b>url</b></td>
                            <td width="10%" align=center>
                                <b>颜色</b></td>
                            <td width="10%" align=center>
                                <b>加粗</b></td>
                            <td width="10%" align=center>
                                <b>删除</b></td>
                        </tr>

                         <tr style="background: rgb(255, 255, 255);" id="0" align="center" bgcolor="#ffffff">
                             <td><input style="width:400px; background: url(/themes/default/images/admin/titlerule.gif) repeat-x scroll 0 2px #EFF0F0;" class="inTextbox" name="title"  id="title0" size="10" type="text"  /></td>
                             <td><input style="width: 260px;" class="inTextbox" name="url" size="10" type="text" value="http://" /></td>
                             <td> <input id="txtColorValue0" name="color" value="" type="hidden"/>
                             <img onclick="getvalue(this,'hiddenOneColor','txtColorValue0')" border="0"  src="/themes/default/images/admin/Gcolor.gif" style="cursor:pointer;background-Color:#fff;" align="absmiddle"/></td>
                             <td><input name="JC"  type="checkbox"/></td>
                             <td id="line1"><input class="inTextbox" onclick="delline(0)" value="删除" type="button"/></td>
                          </tr>
                    </table>
                </td>
            </tr>
           <tr bgcolor="#ffffff">
                <td colspan="5" style="text-align: left">
                    <input id="btnAddTitle" type="button" value="增加一条" class="inTextbox" onclick="addline()" />
                </td>
            </tr>
           <tr>
              <td  width="10%" align="right" nowrap> 所属栏目：</td>
              <td colspan="3">&nbsp;&nbsp;<img height="16" src="/themes/default/images/admin/Item.Option.gif" width="16" align="absMiddle">&nbsp;
                <select id="ChannelId" class="inTextbox" name="ChannelId"  style="width:160px">                            
                        </select>
                <font color="#FF0000">*</font>
               </td>
            </tr>
            <tr>
              <td align="right" nowrap>权重：</td>
              <td colspan="3">&nbsp;&nbsp;<img height="16" src="/themes/default/images/admin/Item.Doc.Update.gif" width="16" align="absMiddle">
               <input name="OrderNum"  size="12" maxlength="50" class= "inTextbox"  value="60"/>&nbsp;&nbsp;只能填写60--100之间的数字，数字越大，前台页面排位越靠前
               </td>
            </tr>
            <tr>
              <td align="right" nowrap>责任编辑：</td>
              <td colspan="3">&nbsp;&nbsp;<img height="16" src="/themes/default/images/admin/Item.Doc.Update.gif" width="16" align="absMiddle">
              <input name="CreatedUser" size="12" maxlength="50" class= "inTextbox"  value="<%=CMS.BLL.UserCookies.AdminName%>"/>
               </td>
            </tr>
            <tr>
              <td align="right" nowrap>发布时间：</td>
              <td colspan="3">&nbsp;&nbsp;<img height="16" src="/themes/default/images/admin/Item.Doc.Update.gif" width="16" align="absMiddle">
               <input name="CreatedTime" size="20" maxlength="50" class= "inTextbox" id="CreatedTime"  value="<%=DateTime.Now.ToString()%>"/>&nbsp;&nbsp;
               </td>
            </tr>
              <tr>
                <td align="right" nowrap>是否通过审核：</td>
                <td colspan="3">&nbsp;&nbsp;<img height="16" src="/themes/default/images/admin/Item.Doc.Update.gif" width="16" align="absMiddle"> 是:
                <input type="radio" name="status" id="status"  value="1" checked="checked"/>否:<input value="0" id="status" type="radio" name="status" /></td>
            </tr>
              <tr>
                <td align="right" nowrap>摘要：</td>
                <td colspan="3">&nbsp;&nbsp;<img height="16" src="/themes/default/images/admin/Item.Doc.Update.gif" width="16" align="absMiddle">
                 <textarea style="margin-left:10px; margin-bottom:6px; width:500px; height:60px;" class="inTextbox" id="newsabstract" name="newsabstract"></textarea></td>
            </tr>
            <tr>
                <td></td>
                <td colspan="3" >
                <input type="hidden" value="2" name="Sort" />
                 <input type="hidden" value="add" name="operateType" />
                <input class="inButton" type="button" value="添  加" name="addemptynews" id="addemptynews" ></td>
            </tr>
        </table>     
    </div>
 </div>
</body>
</html>
